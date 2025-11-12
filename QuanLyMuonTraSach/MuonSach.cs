using Microsoft.Data.SqlClient;
using QuanLyMuonTraSach.Helpers;
using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

namespace QuanLyMuonTraSach
{
    public partial class MuonSach : Form
    {
        SqlConnection con = new SqlConnection(Connection.ConString);
        private BindingList<SachGioHang> gioHang;

        // Quy định của thư viện (Hard-code)
        private const int SO_NGAY_MUON_TOI_DA = 7;

        public MuonSach()
        {
            InitializeComponent();

            // Đăng ký các sự kiện
            this.Load += new System.EventHandler(this.MuonSach_Load);
            this.btnThemSachVaoGio.Click += new System.EventHandler(this.btnThemSachVaoGio_Click);
            this.btnXacNhanMuon.Click += new System.EventHandler(this.btnXacNhanMuon_Click);
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
        }

        // --- CÁC HÀM XỬ LÝ SỰ KIỆN ---

        private void MuonSach_Load(object sender, EventArgs e)
        {
            LoadDocGiaComboBox();
            LoadSachComboBox(); // Tải danh sách SÁCH CÓ SẴN

            // Khởi tạo giỏ hàng
            gioHang = new BindingList<SachGioHang>();
            dgvGioSach.DataSource = gioHang;

            ResetForm();
        }

        private void btnThemSachVaoGio_Click(object sender, EventArgs e)
        {
            // 1. Validate ComboBox Sách
            if (cbTenSach.SelectedValue == null)
            {
                lblThongTinSach.Text = "Vui lòng chọn một cuốn sách.";
                lblThongTinSach.ForeColor = System.Drawing.Color.Red;
                return;
            }

            int maSach = (int)cbTenSach.SelectedValue;
            string tenSach = cbTenSach.Text; // Lấy TenSach đang hiển thị

            // 2. Kiểm tra sách đã có trong giỏ hàng chưa
            foreach (var sach in gioHang)
            {
                if (sach.MaSach == maSach)
                {
                    lblThongTinSach.Text = "Sách này đã có trong giỏ mượn.";
                    lblThongTinSach.ForeColor = System.Drawing.Color.Red;
                    return;
                }
            }

            // 3. Vì ComboBox CHỈ load sách có sẵn, ta không cần KiemTraSachHopLe nữa
            // Chỉ cần thêm vào giỏ
            gioHang.Add(new SachGioHang { MaSach = maSach, TenSach = tenSach });

            // Cập nhật UI
            lblThongTinSach.Text = $"Đã thêm: {tenSach}";
            lblThongTinSach.ForeColor = System.Drawing.Color.Blue;
            cbTenSach.SelectedIndex = -1; // Reset combobox
            cbTenSach.Focus();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            ResetForm();
        }

        private void btnXacNhanMuon_Click(object sender, EventArgs e)
        {
            // --- 1. Validate (Giữ nguyên) ---
            if (cbDocGia.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn độc giả.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (gioHang.Count == 0)
            {
                MessageBox.Show("Giỏ mượn đang trống. Vui lòng thêm sách.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int maDocGia = (int)cbDocGia.SelectedValue;
            DateTime ngayMuon = DateTime.Now.Date;
            DateTime ngayHenTra = ngayMuon.AddDays(SO_NGAY_MUON_TOI_DA);

            // --- 2. Bắt đầu GIAO DỊCH (TRANSACTION) ---
            SqlTransaction transaction = null;
            try
            {
                if (con.State == ConnectionState.Closed) con.Open();
                transaction = con.BeginTransaction();

                // TẠO 1 PHIẾU MƯỢN (HEADER)
                string queryPhieuMuon = "INSERT INTO PhieuMuon (NgayMuon, NgayHenTra, MaDocGia, TrangThaiPhieu) " +
                                        "VALUES (@NgayMuon, @NgayHenTra, @MaDocGia, @TrangThaiPhieu); " +
                                        "SELECT SCOPE_IDENTITY();";

                SqlCommand cmdPhieuMuon = new SqlCommand(queryPhieuMuon, con, transaction);
                cmdPhieuMuon.Parameters.AddWithValue("@NgayMuon", ngayMuon);
                cmdPhieuMuon.Parameters.AddWithValue("@NgayHenTra", ngayHenTra);
                cmdPhieuMuon.Parameters.AddWithValue("@MaDocGia", maDocGia);

                cmdPhieuMuon.Parameters.AddWithValue("@TrangThaiPhieu", TrangThaiPhieuMuon.Borrowing);

                int maPhieuMuonMoi = Convert.ToInt32(cmdPhieuMuon.ExecuteScalar());

                // LẶP QUA GIỎ HÀNG ĐỂ TẠO CHI TIẾT VÀ CẬP NHẬT SÁCH
                string queryChiTiet = "INSERT INTO ChiTietPhieuMuon (MaPhieuMuon, MaSach, NgayTra) VALUES (@MaPhieuMuon, @MaSach, NULL)";

                string queryUpdateSach = "UPDATE Sach SET TrangThai = @TrangThai WHERE MaSach = @MaSach";

                SqlCommand cmdChiTiet = new SqlCommand(queryChiTiet, con, transaction);
                SqlCommand cmdUpdateSach = new SqlCommand(queryUpdateSach, con, transaction);

                foreach (var sach in gioHang)
                {
                    // Thêm Chi Tiết Phiếu Mượn
                    cmdChiTiet.Parameters.Clear();
                    cmdChiTiet.Parameters.AddWithValue("@MaPhieuMuon", maPhieuMuonMoi);
                    cmdChiTiet.Parameters.AddWithValue("@MaSach", sach.MaSach);
                    cmdChiTiet.ExecuteNonQuery();

                    // Cập nhật Trạng Thái Sách
                    cmdUpdateSach.Parameters.Clear();

                    cmdUpdateSach.Parameters.AddWithValue("@TrangThai", TrangThaiSach.Borrow);
                    cmdUpdateSach.Parameters.AddWithValue("@MaSach", sach.MaSach);
                    cmdUpdateSach.ExecuteNonQuery();
                }

                // HOÀN TẤT GIAO DỊCH (Giữ nguyên)
                transaction.Commit();
                MessageBox.Show($"Tạo phiếu mượn với mã phiếu {maPhieuMuonMoi} thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadSachComboBox();
                ResetForm();
            }
            catch (Exception ex)
            {
                // ROLLBACK (Giữ nguyên)
                try
                {
                    transaction?.Rollback();
                }
                catch (Exception rbEx)
                {
                    MessageBox.Show("Lỗi nghiêm trọng khi Rollback: " + rbEx.Message);
                }
                MessageBox.Show("Tạo phiếu mượn thất bại. Lỗi: " + ex.Message, "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (con.State == ConnectionState.Open) con.Close();
            }
        }

        // --- CÁC HÀM HỖ TRỢ (HELPER FUNCTIONS) ---

        /// <summary>
        /// Tải danh sách độc giả lên ComboBox
        /// </summary>
        private void LoadDocGiaComboBox()
        {
            try
            {
                using (SqlConnection c = new SqlConnection(Connection.ConString))
                {
                    c.Open();
                    DataTable dtDocGia = new DataTable();
                    string query = "SELECT MaDocGia, HoTen FROM DocGia";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, c);
                    adapter.Fill(dtDocGia);

                    cbDocGia.DataSource = dtDocGia;
                    cbDocGia.DisplayMember = "HoTen";
                    cbDocGia.ValueMember = "MaDocGia";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách độc giả: " + ex.Message);
            }
        }

        private void LoadSachComboBox()
        {
            try
            {
                using (SqlConnection c = new SqlConnection(Connection.ConString))
                {
                    c.Open();
                    DataTable dtSach = new DataTable();

                    // THAY ĐỔI Ở ĐÂY: Dùng @TrangThai
                    string query = "SELECT MaSach, TenSach FROM Sach WHERE TrangThai = @TrangThai";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, c);

                    // THAY ĐỔI Ở ĐÂY: Thêm Parameter dùng class static
                    adapter.SelectCommand.Parameters.AddWithValue("@TrangThai", TrangThaiSach.Available);

                    adapter.Fill(dtSach);

                    cbTenSach.DataSource = dtSach;
                    cbTenSach.DisplayMember = "TenSach";
                    cbTenSach.ValueMember = "MaSach";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách sách: " + ex.Message);
            }
        }

        private void ResetForm()
        {
            cbDocGia.SelectedIndex = -1;
            cbTenSach.SelectedIndex = -1;
            lblThongTinSach.Text = "";
            gioHang.Clear(); // Xóa sạch giỏ hàng
            cbDocGia.Focus();
        }
    }
}