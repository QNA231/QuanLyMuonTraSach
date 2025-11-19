using Microsoft.Data.SqlClient;
using QuanLyMuonTraSach.Helpers;
using System;
using System.Data;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace QuanLyMuonTraSach
{
    public partial class DocGia : Form
    {
        SqlConnection con = new SqlConnection(Connection.ConString);
        private string formMode = "default";
        private int? selectedId = null; 
        private DataTable dtDocGia;
        private string placeholderTimKiem = "Nhập để tìm kiếm...";

        public DocGia()
        {
            InitializeComponent();

            this.Load += new System.EventHandler(this.DocGia_Load);
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            this.txtTimKiem.Enter += new System.EventHandler(this.txtTimKiem_Enter);
            this.txtTimKiem.Leave += new System.EventHandler(this.txtTimKiem_Leave);
            this.txtTimKiem.TextChanged += new System.EventHandler(this.txtTimKiem_TextChanged);
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            this.gridDocGia.SelectionChanged += new System.EventHandler(this.GridDocGia_SelectionChanged);
        }

        // --- CÁC HÀM XỬ LÝ SỰ KIỆN ---

        private void DocGia_Load(object sender, EventArgs e)
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
            if (gridDocGia.SelectedRows.Count > 0 && selectedId.HasValue)
            {
                SetFormState("edit");
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một độc giả để sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (gridDocGia.SelectedRows.Count > 0 && selectedId.HasValue)
            {
                var result = MessageBox.Show("Bạn có chắc chắn muốn xóa độc giả này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    DeleteDocGiaFromDB(selectedId.Value);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một độc giả để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            // --- 1. Kiểm tra dữ liệu (Validate) ---

            if (formMode == "add" && string.IsNullOrWhiteSpace(txtMaDocGia.Text))
            {
                MessageBox.Show("Mã độc giả không được để trống.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMaDocGia.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtHoTen.Text))
            {
                MessageBox.Show("Tên độc giả không được để trống.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtHoTen.Focus(); 
                return;
            }
            string soDienThoai = txtSoDienThoai.Text.Trim();
            if (string.IsNullOrWhiteSpace(soDienThoai))
            {
                MessageBox.Show("Số điện thoại không được để trống.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSoDienThoai.Focus();
                return;
            }
            string phonePattern = @"^0\d{9}$";

            if (!Regex.IsMatch(soDienThoai, phonePattern))
            {
                MessageBox.Show("Số điện thoại không hợp lệ.\nPhải có đúng 10 chữ số và bắt đầu bằng số 0.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSoDienThoai.Focus();
                return;
            }

            // --- 2. Xử lý Lưu (Thêm hoặc Sửa) vào CSDL ---
            try
            {
                if (formMode == "add")
                {
                    // Gọi hàm INSERT với MaDocGia, Tên (từ txtHoTen), SĐT
                    InsertDocGiaToDB(txtMaDocGia.Text, txtHoTen.Text, soDienThoai);
                }
                else if (formMode == "edit")
                {
                    // Gọi hàm UPDATE với Id 
                    if (selectedId.HasValue)
                    {
                        UpdateDocGiaInDB(selectedId.Value, txtHoTen.Text, soDienThoai);
                    }
                }

                // --- 3. Tải lại dữ liệu và đưa form về trạng thái ban đầu ---
                LoadDataFromDB();
                SetFormState("default");
            }
            // Bắt lỗi SQL cụ thể (ví dụ: trùng lặp MaDocGia)
            catch (SqlException ex)
            {
                if (ex.Number == 2627 || ex.Number == 2601) // Lỗi vi phạm UNIQUE constraint
                {
                    MessageBox.Show($"Mã độc giả '{txtMaDocGia.Text}' đã tồn tại. Vui lòng nhập mã khác.", "Lỗi Trùng Lặp", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtMaDocGia.Focus();
                }
                else
                {
                    MessageBox.Show("Lỗi khi lưu dữ liệu CSDL: " + ex.Message, "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu dữ liệu: " + ex.Message, "Lỗi Chung", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            SearchHelper.ApplyFilter(dtDocGia, searchText, "MaDocGia", "TenDocGia", "SoDienThoai");
        }

        private void GridDocGia_SelectionChanged(object sender, EventArgs e)
        {
            if (formMode == "default" && gridDocGia.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = gridDocGia.SelectedRows[0];

                // Lấy giá trị Id (PK)
                object idValue = selectedRow.Cells["Id"].Value;
                if (idValue != null && idValue != DBNull.Value)
                {
                    selectedId = Convert.ToInt32(idValue);
                }
                else
                {
                    selectedId = null;
                }

                // Đẩy dữ liệu lên textbox
                txtMaDocGia.Text = selectedRow.Cells["MaDocGia"].Value?.ToString();
                txtHoTen.Text = selectedRow.Cells["TenDocGia"].Value?.ToString();
                txtSoDienThoai.Text = selectedRow.Cells["SoDienThoai"].Value?.ToString();

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
                    txtMaDocGia.Enabled = false; 
                    txtHoTen.Enabled = false;
                    txtSoDienThoai.Enabled = false;

                    btnThem.Enabled = true;
                    btnSua.Enabled = false;
                    btnXoa.Enabled = false;
                    btnLuu.Enabled = false;
                    gridDocGia.Enabled = true;

                    txtMaDocGia.Clear(); 
                    txtHoTen.Clear();
                    txtSoDienThoai.Clear();
                    gridDocGia.ClearSelection();
                    selectedId = null; // Reset Id
                    break;

                case "add":
                    txtMaDocGia.Enabled = true; 
                    txtHoTen.Enabled = true;
                    txtSoDienThoai.Enabled = true;

                    btnThem.Enabled = false;
                    btnSua.Enabled = false;
                    btnXoa.Enabled = false;
                    btnLuu.Enabled = true;
                    gridDocGia.Enabled = false;

                    txtMaDocGia.Clear();
                    txtHoTen.Clear();
                    txtSoDienThoai.Clear();
                    txtMaDocGia.Focus(); // Focus vào MaDocGia
                    selectedId = null;
                    break;

                case "edit":
                    txtMaDocGia.Enabled = false;
                    txtHoTen.Enabled = true;
                    txtSoDienThoai.Enabled = true;

                    btnThem.Enabled = false;
                    btnSua.Enabled = false;
                    btnXoa.Enabled = false;
                    btnLuu.Enabled = true;
                    gridDocGia.Enabled = false;
                    txtHoTen.Focus();
                    break;
            }
        }

        private void LoadDataFromDB()
        {
            try
            {
                if (con.State == ConnectionState.Closed) con.Open();

                dtDocGia = new DataTable();
                string query = "SELECT Id, MaDocGia, TenDocGia, SoDienThoai FROM DocGia";
                SqlDataAdapter adapter = new SqlDataAdapter(query, con);
                adapter.Fill(dtDocGia);
                gridDocGia.DataSource = dtDocGia;

                if (gridDocGia.Columns["Id"] != null)
                {
                    gridDocGia.Columns["Id"].Visible = false;
                }
                //if (gridDocGia.Columns["TenDocGia"] != null)
                //{
                //    gridDocGia.Columns["TenDocGia"].HeaderText = "Tên độc giả";
                //}
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

        private void InsertDocGiaToDB(string maDocGia, string tenDocGia, string soDienThoai)
        {
            try
            {
                if (con.State == ConnectionState.Closed) con.Open();

                // Cập nhật query
                string query = "INSERT INTO DocGia (MaDocGia, TenDocGia, SoDienThoai) VALUES (@MaDocGia, @TenDocGia, @SoDienThoai)";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@MaDocGia", maDocGia);
                cmd.Parameters.AddWithValue("@TenDocGia", tenDocGia); 
                cmd.Parameters.AddWithValue("@SoDienThoai", soDienThoai);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Thêm độc giả mới thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                if (con.State == ConnectionState.Open) con.Close();
            }
        }

        private void UpdateDocGiaInDB(int id, string tenDocGia, string soDienThoai)
        {
            try
            {
                if (con.State == ConnectionState.Closed) con.Open();

                // Cập nhật query
                string query = "UPDATE DocGia SET TenDocGia = @TenDocGia, SoDienThoai = @SoDienThoai WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@TenDocGia", tenDocGia); 
                cmd.Parameters.AddWithValue("@SoDienThoai", soDienThoai);
                cmd.Parameters.AddWithValue("@Id", id); 
                cmd.ExecuteNonQuery();
                MessageBox.Show("Cập nhật độc giả thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                if (con.State == ConnectionState.Open) con.Close();
            }
        }

        // Dùng id (int)
        private void DeleteDocGiaFromDB(int id)
        {
            try
            {
                if (con.State == ConnectionState.Closed) con.Open();

                string query = "DELETE FROM DocGia WHERE Id = @Id";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Id", id); 
                cmd.ExecuteNonQuery();

                MessageBox.Show("Xóa độc giả thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Tải lại dữ liệu và đặt lại form sau khi xóa thành công
                LoadDataFromDB();
                SetFormState("default");
            }
            catch (SqlException ex)
            {
                // Lỗi vi phạm ràng buộc khóa ngoại
                if (ex.Number == 547)
                {
                    MessageBox.Show("Không thể xóa độc giả này vì họ đang có phiếu mượn sách.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Lỗi SQL: " + ex.Message, "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (con.State == ConnectionState.Open) con.Close();
            }
        }
    }
}