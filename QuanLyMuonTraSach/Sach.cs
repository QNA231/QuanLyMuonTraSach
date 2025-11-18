using Microsoft.Data.SqlClient;
using QuanLyMuonTraSach.Helpers;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;

namespace QuanLyMuonTraSach
{
    public partial class Sach : Form
    {
        SqlConnection con = new SqlConnection(Connection.ConString);
        private string formMode = "default";
        private int? selectedId = null;
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
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);

            this.btnChonAnh.Click += new System.EventHandler(this.btnChonAnh_Click);
            this.btnXoaAnh.Click += new System.EventHandler(this.btnXoaAnh_Click);

            this.txtTimKiem.Enter += new System.EventHandler(this.txtTimKiem_Enter);
            this.txtTimKiem.Leave += new System.EventHandler(this.txtTimKiem_Leave);
            this.txtTimKiem.TextChanged += new System.EventHandler(this.txtTimKiem_TextChanged);
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
            if (gridSach.SelectedRows.Count > 0 && selectedId.HasValue)
            {
                string trangThai = gridSach.SelectedRows[0].Cells["TrangThai"].Value.ToString();

                if (trangThai == TrangThaiSach.Borrow)
                {
                    MessageBox.Show("Không thể sửa sách đang được mượn. Vui lòng chờ độc giả trả sách.", "Lỗi Ràng Buộc", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                SetFormState("edit");
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một cuốn sách để sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (gridSach.SelectedRows.Count > 0 && selectedId.HasValue)
            {
                string trangThai = gridSach.SelectedRows[0].Cells["TrangThai"].Value.ToString();

                if (trangThai == TrangThaiSach.Borrow)
                {
                    MessageBox.Show("Không thể xóa sách đang được mượn. Vui lòng chờ độc giả trả sách.", "Lỗi Ràng Buộc", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var result = MessageBox.Show("Bạn có chắc chắn muốn xóa cuốn sách này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    DeleteSachFromDB(selectedId.Value);
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
            if (formMode == "add" && string.IsNullOrWhiteSpace(txtMaSach.Text))
            {
                MessageBox.Show("Mã sách không được để trống.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMaSach.Focus();
                return;
            }
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

            // Chuyển ảnh sang byte[]
            byte[] hinhAnhBytes = ImageToByteArray(picHinhAnh.Image);

            // --- 2. Xử lý Lưu (Thêm hoặc Sửa) vào CSDL ---
            try
            {
                if (formMode == "add")
                {
                    InsertSachToDB(txtMaSach.Text, txtTenSach.Text, txtTacGia.Text, hinhAnhBytes);
                }
                else if (formMode == "edit")
                {
                    if (selectedId.HasValue)
                    {
                        UpdateSachInDB(selectedId.Value, txtTenSach.Text, txtTacGia.Text, hinhAnhBytes);
                    }
                }

                // --- 3. Tải lại dữ liệu và đưa form về trạng thái ban đầu ---
                LoadDataFromDB();
                SetFormState("default");
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627 || ex.Number == 2601)
                {
                    MessageBox.Show($"Mã sách '{txtMaSach.Text}' đã tồn tại. Vui lòng nhập mã khác.", "Lỗi Trùng Lặp", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtMaSach.Focus();
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
            SearchHelper.ApplyFilter(dtSach, searchText, "MaSach", "TenSach", "TacGia", "TrangThai");
        }

        private void GridSach_SelectionChanged(object sender, EventArgs e)
        {
            if (formMode == "default" && gridSach.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = gridSach.SelectedRows[0];

                object idValue = selectedRow.Cells["Id"].Value;
                if (idValue != null && idValue != DBNull.Value)
                {
                    selectedId = Convert.ToInt32(idValue);
                }
                else
                {
                    selectedId = null;
                }

                txtMaSach.Text = selectedRow.Cells["MaSach"].Value?.ToString();
                txtTenSach.Text = selectedRow.Cells["TenSach"].Value?.ToString();
                txtTacGia.Text = selectedRow.Cells["TacGia"].Value?.ToString();

                object imageData = selectedRow.Cells["Anh"].Value;
                if (imageData != DBNull.Value && imageData != null)
                {
                    picHinhAnh.Image = ByteArrayToImage((byte[])imageData);
                }
                else
                {
                    picHinhAnh.Image = null;
                }


                btnSua.Enabled = true;
                btnXoa.Enabled = true;
            }
        }

        // --- CÁC HÀM XỬ LÝ ẢNH ---

        private void btnChonAnh_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    picHinhAnh.Image = Image.FromFile(openFileDialog1.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Không thể tải ảnh: " + ex.Message, "Lỗi Ảnh", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnXoaAnh_Click(object sender, EventArgs e)
        {
            picHinhAnh.Image = null;
        }


        // --- CÁC HÀM XỬ LÝ LOGIC VÀ DATABASE ---

        private void SetFormState(string state)
        {
            formMode = state;
            switch (state)
            {
                case "default":
                    txtMaSach.Enabled = false;
                    txtTenSach.Enabled = false;
                    txtTacGia.Enabled = false;
                    btnChonAnh.Enabled = false;
                    btnXoaAnh.Enabled = false;

                    btnThem.Enabled = true;
                    btnSua.Enabled = false;
                    btnXoa.Enabled = false;
                    btnLuu.Enabled = false;
                    gridSach.Enabled = true;

                    txtMaSach.Clear();
                    txtTenSach.Clear();
                    txtTacGia.Clear();
                    picHinhAnh.Image = null;
                    gridSach.ClearSelection();
                    selectedId = null;
                    break;

                case "add":
                    txtMaSach.Enabled = true;
                    txtTenSach.Enabled = true;
                    txtTacGia.Enabled = true;
                    btnChonAnh.Enabled = true;
                    btnXoaAnh.Enabled = true;

                    btnThem.Enabled = false;
                    btnSua.Enabled = false;
                    btnXoa.Enabled = false;
                    btnLuu.Enabled = true;
                    gridSach.Enabled = false;

                    txtMaSach.Clear();
                    txtTenSach.Clear();
                    txtTacGia.Clear();
                    picHinhAnh.Image = null;
                    txtMaSach.Focus();
                    selectedId = null;
                    break;

                case "edit":
                    txtMaSach.Enabled = false;
                    txtTenSach.Enabled = true;
                    txtTacGia.Enabled = true;
                    btnChonAnh.Enabled = true;
                    btnXoaAnh.Enabled = true;

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
                string query = "SELECT Id, MaSach, TenSach, TacGia, TrangThai, Anh FROM Sach";
                SqlDataAdapter adapter = new SqlDataAdapter(query, con);
                adapter.Fill(dtSach);

                gridSach.DataSource = dtSach;

                if (gridSach.Columns["Id"] != null)
                {
                    gridSach.Columns["Id"].Visible = false;
                }
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

        private void InsertSachToDB(string maSach, string tenSach, string tacGia, byte[] imageBytes)
        {
            try
            {
                if (con.State == ConnectionState.Closed) con.Open();

                string query = "INSERT INTO Sach (MaSach, TenSach, TacGia, TrangThai, Anh) VALUES (@MaSach, @TenSach, @TacGia, @TrangThai, @Anh)";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@MaSach", maSach);
                cmd.Parameters.AddWithValue("@TenSach", tenSach);
                cmd.Parameters.AddWithValue("@TacGia", tacGia);
                cmd.Parameters.AddWithValue("@TrangThai", TrangThaiSach.Available);

                SqlParameter imageParam = new SqlParameter("@Anh", SqlDbType.VarBinary, -1);
                if (imageBytes != null)
                    imageParam.Value = imageBytes;
                else
                    imageParam.Value = DBNull.Value;
                cmd.Parameters.Add(imageParam);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Thêm sách mới thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                if (con.State == ConnectionState.Open) con.Close();
            }
        }

        private void UpdateSachInDB(int id, string tenSach, string tacGia, byte[] imageBytes)
        {
            try
            {
                if (con.State == ConnectionState.Closed) con.Open();

                string query = "UPDATE Sach SET TenSach = @TenSach, TacGia = @TacGia, Anh = @Anh WHERE Id = @Id";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@TenSach", tenSach);
                cmd.Parameters.AddWithValue("@TacGia", tacGia);
                cmd.Parameters.AddWithValue("@Id", id);

                SqlParameter imageParam = new SqlParameter("@Anh", SqlDbType.VarBinary, -1);
                if (imageBytes != null)
                    imageParam.Value = imageBytes;
                else
                    imageParam.Value = DBNull.Value;
                cmd.Parameters.Add(imageParam);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Cập nhật sách thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                if (con.State == ConnectionState.Open) con.Close();
            }
        }

        private void DeleteSachFromDB(int id)
        {
            try
            {
                if (con.State == ConnectionState.Closed) con.Open();
                string query = "DELETE FROM Sach WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
            }
            finally
            {
                if (con.State == ConnectionState.Open) con.Close();
            }
        }

        // --- HÀM HỖ TRỢ CHUYỂN ĐỔI ẢNH ---

        private byte[] ImageToByteArray(Image imageIn)
        {
            if (imageIn == null)
                return null;

            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, ImageFormat.Png);
                return ms.ToArray();
            }
        }

        private Image ByteArrayToImage(byte[] byteArrayIn)
        {
            if (byteArrayIn == null || byteArrayIn.Length == 0)
                return null;

            using (var ms = new MemoryStream(byteArrayIn))
            {
                try
                {
                    return Image.FromStream(ms);
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
    }
}