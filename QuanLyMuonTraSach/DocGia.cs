using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLyMuonTraSach
{
    public partial class DocGia : Form
    {
        SqlConnection con = new SqlConnection(Connection.ConString);
        private string formMode = "default";
        private int? selectedMaDocGia = null;
        private DataTable dtDocGia;

        public DocGia()
        {
            InitializeComponent();

            this.Load += new System.EventHandler(this.DocGia_Load);
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);

            this.gridDocGia.SelectionChanged += new System.EventHandler(this.GridDocGia_SelectionChanged);
        }

        // --- CÁC HÀM XỬ LÝ SỰ KIỆN ---

        private void DocGia_Load(object sender, EventArgs e)
        {
            LoadDataFromDB();
            SetFormState("default");
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            SetFormState("add");
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (gridDocGia.SelectedRows.Count > 0 && selectedMaDocGia.HasValue)
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
            if (gridDocGia.SelectedRows.Count > 0 && selectedMaDocGia.HasValue)
            {
                var result = MessageBox.Show("Bạn có chắc chắn muốn xóa độc giả này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    // Gọi hàm xóa 
                    DeleteDocGiaFromDB(selectedMaDocGia.Value);
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
            if (string.IsNullOrWhiteSpace(txtHoTen.Text))
            {
                MessageBox.Show("Họ tên không được để trống.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtHoTen.Focus();
                return;
            }

            // --- 2. Xử lý Lưu (Thêm hoặc Sửa) vào CSDL ---
            try
            {
                if (formMode == "add")
                {
                    // Gọi hàm INSERT 
                    InsertDocGiaToDB(txtHoTen.Text, txtSoDienThoai.Text);
                }
                else if (formMode == "edit")
                {
                    // Gọi hàm UPDATE 
                    if (selectedMaDocGia.HasValue)
                    {
                        UpdateDocGiaInDB(selectedMaDocGia.Value, txtHoTen.Text, txtSoDienThoai.Text);
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

        private void GridDocGia_SelectionChanged(object sender, EventArgs e)
        {
            if (formMode == "default" && gridDocGia.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = gridDocGia.SelectedRows[0];

                // Lấy giá trị MaDocGia
                object maDocGiaValue = selectedRow.Cells["MaDocGia"].Value;
                if (maDocGiaValue != null && maDocGiaValue != DBNull.Value)
                {
                    selectedMaDocGia = Convert.ToInt32(maDocGiaValue);
                }
                else
                {
                    selectedMaDocGia = null;
                }

                // Đẩy dữ liệu lên textbox
                txtHoTen.Text = selectedRow.Cells["HoTen"].Value?.ToString();
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
                    txtHoTen.Enabled = false;
                    txtSoDienThoai.Enabled = false;

                    btnThem.Enabled = true;
                    btnSua.Enabled = false;
                    btnXoa.Enabled = false;
                    btnLuu.Enabled = false;
                    gridDocGia.Enabled = true;

                    txtHoTen.Clear();
                    txtSoDienThoai.Clear();
                    gridDocGia.ClearSelection();
                    selectedMaDocGia = null; // Reset ID đang chọn
                    break;

                case "add":
                    txtHoTen.Enabled = true;
                    txtSoDienThoai.Enabled = true;

                    btnThem.Enabled = false;
                    btnSua.Enabled = false;
                    btnXoa.Enabled = false;
                    btnLuu.Enabled = true;
                    gridDocGia.Enabled = false;

                    txtHoTen.Clear();
                    txtSoDienThoai.Clear();
                    txtHoTen.Focus();
                    selectedMaDocGia = null; // Đảm bảo không giữ ID cũ
                    break;

                case "edit":
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
                string query = "SELECT MaDocGia, HoTen, SoDienThoai FROM DocGia";
                SqlDataAdapter adapter = new SqlDataAdapter(query, con);
                adapter.Fill(dtDocGia);
                gridDocGia.DataSource = dtDocGia;
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

        private void InsertDocGiaToDB(string hoTen, string soDienThoai)
        {
            try
            {
                if (con.State == ConnectionState.Closed) con.Open();

                // Sửa query (không có TrangThai)
                string query = "INSERT INTO DocGia (HoTen, SoDienThoai) VALUES (@HoTen, @SoDienThoai)";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@HoTen", hoTen);
                cmd.Parameters.AddWithValue("@SoDienThoai", soDienThoai);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Thêm độc giả mới thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                if (con.State == ConnectionState.Open) con.Close();
            }
        }

        private void UpdateDocGiaInDB(int maDocGia, string hoTen, string soDienThoai)
        {
            try
            {
                if (con.State == ConnectionState.Closed) con.Open();

                string query = "UPDATE DocGia SET HoTen = @HoTen, SoDienThoai = @SoDienThoai WHERE MaDocGia = @MaDocGia";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@HoTen", hoTen);
                cmd.Parameters.AddWithValue("@SoDienThoai", soDienThoai);
                cmd.Parameters.AddWithValue("@MaDocGia", maDocGia);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Cập nhật độc giả thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                if (con.State == ConnectionState.Open) con.Close();
            }
        }

        private void DeleteDocGiaFromDB(int maDocGia)
        {
            try
            {
                if (con.State == ConnectionState.Closed) con.Open();

                string query = "DELETE FROM DocGia WHERE MaDocGia = @MaDocGia";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@MaDocGia", maDocGia);
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