using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyMuonTraSach
{
    public partial class TraSach : Form
    {
        SqlConnection con = new SqlConnection(Connection.ConString);
        private const decimal TIEN_PHAT_MOI_NGAY = 10000;
        private int? selectedMaPhieuMuon = null;
        private int? selectedMaChiTiet = null;
        private string selectedMaSach = null;
        private DateTime selectedNgayHenTra;

        public TraSach()
        {
            InitializeComponent();

            this.Load += new System.EventHandler(this.frmTraSach_Load);
            this.cbDocGia.SelectedIndexChanged += new System.EventHandler(this.cbDocGia_SelectedIndexChanged);
            this.dgvPhieuMuon.SelectionChanged += new System.EventHandler(this.dgvPhieuMuon_SelectionChanged);
            this.dgvChiTiet.SelectionChanged += new System.EventHandler(this.dgvChiTiet_SelectionChanged);
            this.dgvChiTiet.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvChiTiet_DataBindingComplete);
            this.btnXacNhanTra.Click += new System.EventHandler(this.btnXacNhanTra_Click);
        }

        // --- CÁC HÀM XỬ LÝ SỰ KIỆN ---

        private void frmTraSach_Load(object sender, EventArgs e)
        {
            LoadDocGiaComboBox();
            SetInitialState();
        }

        /// <summary>
        /// Sự kiện khi chọn Độc Giả: Tải các Phiếu Mượn của họ
        /// </summary>
        private void cbDocGia_SelectedIndexChanged(object sender, EventArgs e)
        {
            object selectedItem = cbDocGia.SelectedItem;

            if (selectedItem is DataRowView selectedRow)
            {
                string maDocGia = selectedRow["MaDocGia"].ToString();

                // Bây giờ maDocGia đã chắc chắn đúng, tải PhieuMuon
                LoadPhieuMuonGrid(maDocGia);

                // Xóa grid chi tiết (vì chưa chọn phiếu mượn nào)
                dgvChiTiet.DataSource = null;
                btnXacNhanTra.Enabled = false;
            }
            else
            {
                // Xóa sạch cả 2 grid
                dgvPhieuMuon.DataSource = null;
                dgvChiTiet.DataSource = null;
                btnXacNhanTra.Enabled = false;
            }
        }

        /// <summary>
        /// Sự kiện khi chọn Phiếu Mượn: Tải các Sách trong phiếu đó
        /// </summary>
        private void dgvPhieuMuon_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvPhieuMuon.SelectedRows.Count > 0)
            {
                var row = dgvPhieuMuon.SelectedRows[0];
                selectedMaPhieuMuon = (int)row.Cells["MaPhieuMuon"].Value;
                selectedNgayHenTra = (DateTime)row.Cells["NgayHenTra"].Value;

                LoadChiTietGrid(selectedMaPhieuMuon.Value);
                btnXacNhanTra.Enabled = false; // Reset nút trả
            }
        }

        /// <summary>
        /// Sự kiện khi chọn Sách: Kích hoạt nút "Trả" nếu sách CHƯA TRẢ
        /// </summary>
        private void dgvChiTiet_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvChiTiet.SelectedRows.Count > 0)
            {
                var row = dgvChiTiet.SelectedRows[0];

                // Kiểm tra xem sách đã trả chưa (cột NgayTra)
                if (row.Cells["NgayTra"].Value != DBNull.Value)
                {
                    // Sách này đã trả rồi -> Vô hiệu hóa nút
                    btnXacNhanTra.Enabled = false;
                    selectedMaChiTiet = null;
                    selectedMaSach = null; 
                }
                else
                {
                    // Sách này CHƯA TRẢ -> Lấy ID và cho phép trả
                    selectedMaChiTiet = (int)row.Cells["MaChiTietPhieuMuon"].Value;
                    selectedMaSach = row.Cells["MaSach"].Value?.ToString();
                    btnXacNhanTra.Enabled = true;
                }
            }
        }

        /// <summary>
        /// Sự kiện tô màu cho các dòng sách đã trả
        /// </summary>
        private void dgvChiTiet_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewRow row in dgvChiTiet.Rows)
            {
                if (row.Cells["NgayTra"].Value != DBNull.Value)
                {
                    row.DefaultCellStyle.BackColor = Color.LightGray;
                    row.DefaultCellStyle.ForeColor = Color.DarkGray;
                }
            }
        }

        private void btnXacNhanTra_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra lần cuối
            if (!selectedMaChiTiet.HasValue || string.IsNullOrEmpty(selectedMaSach) || !selectedMaPhieuMuon.HasValue)
            {
                MessageBox.Show("Vui lòng chọn một cuốn sách CHƯA TRẢ để thực hiện.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DateTime ngayTra = DateTime.Now.Date;

            // 2. Bắt đầu Transaction
            SqlTransaction transaction = null;
            try
            {
                if (con.State == ConnectionState.Closed) con.Open();
                transaction = con.BeginTransaction();

                // 3. Xử lý Phạt (Nếu có)
                if (ngayTra > selectedNgayHenTra)
                {
                    int soNgayTre = (int)(ngayTra - selectedNgayHenTra).TotalDays;
                    decimal soTienPhat = soNgayTre * TIEN_PHAT_MOI_NGAY;

                    string queryPhat = "INSERT INTO PhieuPhat (MaChiTietPhieuMuon, SoNgayTre, SoTienPhat, TrangThai) " +
                                       "VALUES (@MaChiTiet, @SoNgayTre, @SoTienPhat, @TrangThai)";

                    SqlCommand cmdPhat = new SqlCommand(queryPhat, con, transaction);
                    cmdPhat.Parameters.AddWithValue("@MaChiTiet", selectedMaChiTiet.Value);
                    cmdPhat.Parameters.AddWithValue("@SoNgayTre", soNgayTre);
                    cmdPhat.Parameters.AddWithValue("@SoTienPhat", soTienPhat);
                    cmdPhat.Parameters.AddWithValue("@TrangThai", TrangThaiPhieuPhat.NotPaid);
                    cmdPhat.ExecuteNonQuery();

                    // Thông báo phạt
                    MessageBox.Show($"Trả sách trễ {soNgayTre} ngày!\nTiền phạt: {soTienPhat:N0} đ.\nVui lòng qua quầy thanh toán.",
                                    "Thông báo Phạt", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                // 4. Cập nhật ChiTietPhieuMuon (Set NgayTra)
                string queryChiTiet = "UPDATE ChiTietPhieuMuon SET NgayTra = @NgayTra WHERE MaChiTietPhieuMuon = @MaChiTiet";
                SqlCommand cmdChiTiet = new SqlCommand(queryChiTiet, con, transaction);
                cmdChiTiet.Parameters.AddWithValue("@NgayTra", ngayTra);
                cmdChiTiet.Parameters.AddWithValue("@MaChiTiet", selectedMaChiTiet.Value);
                cmdChiTiet.ExecuteNonQuery();

                // 5. Cập nhật Sach (Set TrangThai = "Có sẵn")
                string querySach = "UPDATE Sach SET TrangThai = @TrangThai WHERE MaSach = @MaSach";
                SqlCommand cmdSach = new SqlCommand(querySach, con, transaction);
                cmdSach.Parameters.AddWithValue("@TrangThai", TrangThaiSach.Available);
                cmdSach.Parameters.AddWithValue("@MaSach", selectedMaSach);
                cmdSach.ExecuteNonQuery();

                // 6. Kiểm tra xem phiếu này đã trả hết sách chưa
                string queryKiemTra = "SELECT COUNT(*) FROM ChiTietPhieuMuon WHERE MaPhieuMuon = @MaPhieuMuon AND NgayTra IS NULL";
                SqlCommand cmdKiemTra = new SqlCommand(queryKiemTra, con, transaction);
                cmdKiemTra.Parameters.AddWithValue("@MaPhieuMuon", selectedMaPhieuMuon.Value);

                int sachConLai = (int)cmdKiemTra.ExecuteScalar();

                if (sachConLai == 0)
                {
                    // Nếu đã trả hết -> Cập nhật TrangThaiPhieu
                    string queryPhieuMuon = "UPDATE PhieuMuon SET TrangThai = @TrangThai WHERE MaPhieuMuon = @MaPhieuMuon";
                    SqlCommand cmdPhieuMuon = new SqlCommand(queryPhieuMuon, con, transaction);
                    cmdPhieuMuon.Parameters.AddWithValue("@TrangThai", TrangThaiPhieuMuon.Returned);
                    cmdPhieuMuon.Parameters.AddWithValue("@MaPhieuMuon", selectedMaPhieuMuon.Value);
                    cmdPhieuMuon.ExecuteNonQuery();
                }

                // 7. Hoàn tất
                transaction.Commit();
                MessageBox.Show("Trả sách thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // 8. Tải lại dữ liệu
                LoadChiTietGrid(selectedMaPhieuMuon.Value); // Tải lại chi tiết (sẽ thấy sách bị xám đi)
                if (sachConLai == 0)
                {
                    // Nếu phiếu đã xong, tải lại danh sách phiếu (phiếu đó sẽ biến mất)
                    LoadPhieuMuonGrid(cbDocGia.SelectedValue.ToString());
                }
                btnXacNhanTra.Enabled = false;

            }
            catch (Exception ex)
            {
                // 8. Nếu Lỗi -> Rollback
                transaction?.Rollback();
                MessageBox.Show("Lỗi nghiêm trọng khi trả sách: " + ex.Message, "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (con.State == ConnectionState.Open) con.Close();
            }
        }

        // --- CÁC HÀM HỖ TRỢ (HELPER FUNCTIONS) ---

        private void SetInitialState()
        {
            dgvPhieuMuon.DataSource = null;
            dgvChiTiet.DataSource = null;
            btnXacNhanTra.Enabled = false;
        }

        private void LoadDocGiaComboBox()
        {
            try
            {
                using (SqlConnection c = new SqlConnection(Connection.ConString))
                {
                    c.Open();
                    DataTable dtDocGia = new DataTable();
                    string query = "SELECT MaDocGia, TenDocGia FROM DocGia";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, c);
                    adapter.Fill(dtDocGia);

                    cbDocGia.DataSource = dtDocGia;
                    cbDocGia.DisplayMember = "TenDocGia";
                    cbDocGia.ValueMember = "MaDocGia"; 
                    cbDocGia.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách độc giả: " + ex.Message);
            }
        }

        /// <summary>
        /// Tải các phiếu CHƯA TRẢ (Đang mượn) của độc giả
        /// </summary>
        private void LoadPhieuMuonGrid(string maDocGia)
        {
            try
            {
                using (SqlConnection c = new SqlConnection(Connection.ConString))
                {
                    c.Open();
                    DataTable dt = new DataTable();
                    string query = "SELECT MaPhieuMuon, NgayMuon, NgayHenTra FROM PhieuMuon " +
                                   "WHERE MaDocGia = @MaDocGia AND TrangThai = @TrangThai";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, c);
                    adapter.SelectCommand.Parameters.AddWithValue("@MaDocGia", maDocGia); 
                    adapter.SelectCommand.Parameters.AddWithValue("@TrangThai", TrangThaiPhieuMuon.Borrowing);
                    adapter.Fill(dt);
                    dgvPhieuMuon.AutoGenerateColumns = false;
                    dgvPhieuMuon.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách phiếu mượn: " + ex.Message);
            }
        }

        /// <summary>
        /// Tải chi tiết các sách (đã trả và chưa trả) của 1 phiếu mượn
        /// </summary>
        private void LoadChiTietGrid(int maPhieuMuon)
        {
            try
            {
                using (SqlConnection c = new SqlConnection(Connection.ConString))
                {
                    c.Open();
                    DataTable dt = new DataTable();
                    // JOIN với Sách để lấy TenSach
                    string query = "SELECT CT.MaChiTietPhieuMuon, CT.MaSach, S.TenSach, CT.NgayTra " +
                                   "FROM ChiTietPhieuMuon CT " +
                                   "JOIN Sach S ON CT.MaSach = S.MaSach " +
                                   "WHERE CT.MaPhieuMuon = @MaPhieuMuon";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, c);
                    adapter.SelectCommand.Parameters.AddWithValue("@MaPhieuMuon", maPhieuMuon);
                    adapter.Fill(dt);
                    dgvChiTiet.AutoGenerateColumns = false;
                    dgvChiTiet.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải chi tiết phiếu mượn: " + ex.Message);
            }
        }
    }
}