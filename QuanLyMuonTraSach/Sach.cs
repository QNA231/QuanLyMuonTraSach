using Microsoft.Data.SqlClient;
using QuanLyMuonTraSach.Helpers;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyMuonTraSach
{
    public partial class Sach : Form
    {
        SqlConnection con = new SqlConnection(Connection.ConString);
        private string formMode = "default";
        private int? selectedMaSach = null;
        private DataTable dtSach;
        private string placeholderTimKiem = "Nhập để tìm kiếm...";

        public Sach()
        {
            InitializeComponent();

            this.Load += new System.EventHandler(this.Sach_Load);
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            this.gridSach.SelectionChanged += new System.EventHandler(this.GridSach_SelectionChanged);
        }

        // --- CÁC HÀM XỬ LÝ SỰ KIỆN ---

        private void Sach_Load(object sender, EventArgs e)
        {
            LoadDataFromDB();
            SetFormState("default");
            txtTimKiem.Text = placeholderTimKiem;
            txtTimKiem.ForeColor = Color.Gray;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            SetFormState("add");
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (gridSach.SelectedRows.Count > 0 && selectedMaSach.HasValue)
            {
                SetFormState("edit");
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một cuốn sách để sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (gridSach.SelectedRows.Count > 0 && selectedMaSach.HasValue)
            {
                var result = MessageBox.Show("Bạn có chắc chắn muốn xóa cuốn sách này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    // Gọi hàm xóa CSDL với ID (int) đã chọn
                    DeleteSachFromDB(selectedMaSach.Value);

                    // Tải lại dữ liệu và đặt lại form
                    LoadDataFromDB();
                    SetFormState("default");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một cuốn sách để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            // --- 1. Kiểm tra dữ liệu (Validate) ---
            if (string.IsNullOrWhiteSpace(txtTenSach.Text))
            {
                MessageBox.Show("Tên sách không được để trống.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtTenSach.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtTacGia.Text))
            {
                MessageBox.Show("Tác giả không được để trống.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtTacGia.Focus();
                return;
            }

            // --- 2. Xử lý Lưu (Thêm hoặc Sửa) vào CSDL ---
            try
            {
                if (formMode == "add")
                {
                    // Gọi hàm INSERT 
                    InsertSachToDB(txtTenSach.Text, txtTacGia.Text);
                }
                else if (formMode == "edit")
                {
                    // Gọi hàm UPDATE (truyền ID (int) đã chọn)
                    if (selectedMaSach.HasValue)
                    {
                        UpdateSachInDB(selectedMaSach.Value, txtTenSach.Text, txtTacGia.Text);
                    }
                }

                // --- 3. Tải lại dữ liệu và đưa form về trạng thái ban đầu ---
                LoadDataFromDB();
                SetFormState("default");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu dữ liệu: " + ex.Message, "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            SetFormState("default");
        }

        private void txtTimKiem_Enter(object sender, EventArgs e)
        {
            if (txtTimKiem.Text == placeholderTimKiem)
            {
                txtTimKiem.Text = "";
                txtTimKiem.ForeColor = Color.Black;
            }
        }

        private void txtTimKiem_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTimKiem.Text))
            {
                txtTimKiem.Text = placeholderTimKiem;
                txtTimKiem.ForeColor = Color.Gray;
            }
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            string searchText = txtTimKiem.Text;
            if (searchText == placeholderTimKiem)
            {
                searchText = "";
            }

            SearchHelper.ApplyFilter(dtSach, searchText, "TenSach", "TacGia", "TrangThai");
        }

        private void GridSach_SelectionChanged(object sender, EventArgs e)
        {
            if (formMode == "default" && gridSach.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = gridSach.SelectedRows[0];

                // Lấy giá trị MaSach
                object maSachValue = selectedRow.Cells["MaSach"].Value;
                if (maSachValue != null && maSachValue != DBNull.Value)
                {
                    selectedMaSach = Convert.ToInt32(maSachValue);
                }
                else
                {
                    selectedMaSach = null;
                }

                // Đẩy dữ liệu lên textbox
                txtTenSach.Text = selectedRow.Cells["TenSach"].Value?.ToString();
                txtTacGia.Text = selectedRow.Cells["TacGia"].Value?.ToString();

                // Cho phép Sửa/Xóa
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
            }
        }

        // --- CÁC HÀM XỬ LÝ LOGIC VÀ DATABASE ---

        private void SetFormState(string state)
        {
            formMode = state;
            switch (state)
            {
                case "default":
                    txtTenSach.Enabled = false;
                    txtTacGia.Enabled = false;
                    btnThem.Enabled = true;
                    btnSua.Enabled = false;
                    btnXoa.Enabled = false;
                    btnLuu.Enabled = false;
                    gridSach.Enabled = true;

                    txtTenSach.Clear();
                    txtTacGia.Clear();
                    gridSach.ClearSelection();
                    selectedMaSach = null; // Reset ID đang chọn
                    break;

                case "add":
                    txtTenSach.Enabled = true;
                    txtTacGia.Enabled = true;
                    btnThem.Enabled = false;
                    btnSua.Enabled = false;
                    btnXoa.Enabled = false;
                    btnLuu.Enabled = true;
                    gridSach.Enabled = false;

                    txtTenSach.Clear();
                    txtTacGia.Clear();
                    txtTenSach.Focus();
                    selectedMaSach = null; // Đảm bảo không giữ ID cũ
                    break;

                case "edit":
                    txtTenSach.Enabled = true;
                    txtTacGia.Enabled = true;
                    btnThem.Enabled = false;
                    btnSua.Enabled = false;
                    btnXoa.Enabled = false;
                    btnLuu.Enabled = true;
                    gridSach.Enabled = false;
                    txtTenSach.Focus();
                    break;
            }
        }

        private void LoadDataFromDB()
        {
            try
            {
                if (con.State == ConnectionState.Closed) con.Open();

                dtSach = new DataTable();
                string query = "SELECT MaSach, TenSach, TacGia, TrangThai FROM Sach";
                SqlDataAdapter adapter = new SqlDataAdapter(query, con);
                adapter.Fill(dtSach);
               
                gridSach.DataSource = dtSach;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (con.State == ConnectionState.Open) con.Close();
            }
        }

        private void InsertSachToDB(string tenSach, string tacGia)
        {
            try
            {
                if (con.State == ConnectionState.Closed) con.Open();
                string query = "INSERT INTO Sach (TenSach, TacGia, TrangThai) VALUES (@TenSach, @TacGia, @TrangThai)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@TenSach", tenSach);
                cmd.Parameters.AddWithValue("@TacGia", tacGia);
                cmd.Parameters.AddWithValue("@TrangThai", TrangThaiSach.Available);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Thêm sách mới thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                if (con.State == ConnectionState.Open) con.Close();
            }
        }

        private void UpdateSachInDB(int maSach, string tenSach, string tacGia)
        {
            try
            {
                if (con.State == ConnectionState.Closed) con.Open();

                string query = "UPDATE Sach SET TenSach = @TenSach, TacGia = @TacGia WHERE MaSach = @MaSach";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@TenSach", tenSach);
                cmd.Parameters.AddWithValue("@TacGia", tacGia);
                cmd.Parameters.AddWithValue("@MaSach", maSach);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Cập nhật sách thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                if (con.State == ConnectionState.Open) con.Close();
            }
        }

        private void DeleteSachFromDB(int maSach)
        {
            try
            {
                if (con.State == ConnectionState.Closed) con.Open();

                string query = "DELETE FROM Sach WHERE MaSach = @MaSach";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@MaSach", maSach);
                cmd.ExecuteNonQuery();
            }
            finally
            {
                if (con.State == ConnectionState.Open) con.Close();
            }
        }
    }
}