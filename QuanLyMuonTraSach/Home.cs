using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace QuanLyMuonTraSach
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();

            this.Load += new System.EventHandler(this.Home_Load);

            this.Activated += new System.EventHandler(this.Home_Activated);
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            this.btnThuPhat.Click += new System.EventHandler(this.btnThuPhat_Click);
        }

        // --- SỰ KIỆN TẢI DỮ LIỆU ---

        private void Home_Load(object sender, EventArgs e)
        {
            LoadAllDashboardData();
        }

        private void Home_Activated(object sender, EventArgs e)
        {
            LoadAllDashboardData();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadAllDashboardData();
        }

        // --- SỰ KIỆN CLICK MENU (ĐIỀU HƯỚNG) ---

        private void MuonSachNav_Click(object sender, EventArgs e)
        {
            MuonSach f = new MuonSach();
            f.ShowDialog(); // Dùng ShowDialog để bắt người dùng phải tắt form này
        }

        private void TraSachNav_Click(object sender, EventArgs e)
        {
            TraSach f = new TraSach();
            f.Show();
        }

        private void SachNav_Click(object sender, EventArgs e)
        {
            Sach f = new Sach();
            f.Show();
        }

        private void DocGiaNav_Click(object sender, EventArgs e)
        {
            DocGia f = new DocGia();
            f.Show();
        }

        // --- CÁC HÀM XỬ LÝ DASHBOARD ---

        /// <summary>
        /// Hàm tổng, gọi cả 3 hàm con để tải dữ liệu
        /// </summary>
        private void LoadAllDashboardData()
        {
            LoadSachDangMuon();
            LoadSachTreHan();
            LoadTinhHinhPhat();
        }

        /// <summary>
        /// Tải Tab 1 - Grid 1: Sách đang mượn
        /// </summary>
        private void LoadSachDangMuon()
        {
            try
            {
                using (SqlConnection c = new SqlConnection(Connection.ConString))
                {
                    c.Open();
                    DataTable dt = new DataTable();
                    string query = @"
                        SELECT S.TenSach, D.HoTen, PM.NgayHenTra
                        FROM Sach S
                        JOIN ChiTietPhieuMuon CT ON S.MaSach = CT.MaSach
                        JOIN PhieuMuon PM ON CT.MaPhieuMuon = PM.MaPhieuMuon
                        JOIN DocGia D ON PM.MaDocGia = D.MaDocGia
                        WHERE CT.NgayTra IS NULL"; // CHƯA TRẢ

                    SqlDataAdapter adapter = new SqlDataAdapter(query, c);
                    adapter.Fill(dt);
                    dgvDangMuon.AutoGenerateColumns = false;
                    dgvDangMuon.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải Sách Đang Mượn: " + ex.Message);
            }
        }

        /// <summary>
        /// Tải Tab 1 - Grid 2: Sách trễ hạn
        /// </summary>
        private void LoadSachTreHan()
        {
            try
            {
                using (SqlConnection c = new SqlConnection(Connection.ConString))
                {
                    c.Open();
                    DataTable dt = new DataTable();
                    string query = @"
                        SELECT S.TenSach, D.HoTen, PM.NgayHenTra,
                               DATEDIFF(day, PM.NgayHenTra, GETDATE()) AS SoNgayTre
                        FROM Sach S
                        JOIN ChiTietPhieuMuon CT ON S.MaSach = CT.MaSach
                        JOIN PhieuMuon PM ON CT.MaPhieuMuon = PM.MaPhieuMuon
                        JOIN DocGia D ON PM.MaDocGia = D.MaDocGia
                        WHERE CT.NgayTra IS NULL AND PM.NgayHenTra < GETDATE()"; // CHƯA TRẢ VÀ QUÁ HẠN

                    SqlDataAdapter adapter = new SqlDataAdapter(query, c);
                    adapter.Fill(dt);
                    dgvTreHan.AutoGenerateColumns = false;
                    dgvTreHan.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải Sách Trễ Hạn: " + ex.Message);
            }
        }

        /// <summary>
        /// Tải Tab 2: Tình hình tiền phạt
        /// </summary>
        private void LoadTinhHinhPhat()
        {
            try
            {
                using (SqlConnection c = new SqlConnection(Connection.ConString))
                {
                    c.Open();

                    // 1. Cập nhật các Label (Dùng ExecuteScalar)
                    string queryDaThu = "SELECT SUM(SoTienPhat) FROM PhieuPhat WHERE TrangThai = @TrangThaiDaThu";
                    SqlCommand cmdDaThu = new SqlCommand(queryDaThu, c);
                    cmdDaThu.Parameters.AddWithValue("@TrangThaiDaThu", TrangThaiPhieuPhat.Paid);
                    object resultDaThu = cmdDaThu.ExecuteScalar();
                    lblDaThu.Text = (resultDaThu == DBNull.Value) ? "0 đ" : $"{Convert.ToDecimal(resultDaThu):N0} đ";

                    string queryChuaThu = "SELECT SUM(SoTienPhat) FROM PhieuPhat WHERE TrangThai = @TrangThaiChuaThu";
                    SqlCommand cmdChuaThu = new SqlCommand(queryChuaThu, c);
                    cmdChuaThu.Parameters.AddWithValue("@TrangThaiChuaThu", TrangThaiPhieuPhat.NotPaid);
                    object resultChuaThu = cmdChuaThu.ExecuteScalar();
                    lblChuaThu.Text = (resultChuaThu == DBNull.Value) ? "0 đ" : $"{Convert.ToDecimal(resultChuaThu):N0} đ";

                    // 2. Tải Grid các khoản CHƯA THU
                    DataTable dt = new DataTable();
                    string queryGrid = @"
                        SELECT PP.MaPhieuPhat, D.HoTen, S.TenSach, PP.SoNgayTre, PP.SoTienPhat
                        FROM PhieuPhat PP
                        JOIN ChiTietPhieuMuon CT ON PP.MaChiTietPhieuMuon = CT.MaChiTietPhieuMuon
                        JOIN Sach S ON CT.MaSach = S.MaSach
                        JOIN PhieuMuon PM ON CT.MaPhieuMuon = PM.MaPhieuMuon
                        JOIN DocGia D ON PM.MaDocGia = D.MaDocGia
                        WHERE PP.TrangThai = @TrangThaiChuaThu";

                    SqlDataAdapter adapter = new SqlDataAdapter(queryGrid, c);
                    adapter.SelectCommand.Parameters.AddWithValue("@TrangThaiChuaThu", TrangThaiPhieuPhat.NotPaid);
                    adapter.Fill(dt);
                    dgvPhatChuaThu.AutoGenerateColumns = false;
                    dgvPhatChuaThu.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải Tình Hình Phạt: " + ex.Message);
            }
        }

        /// <summary>
        /// Xử lý nút "Xác nhận Thu"
        /// </summary>
        private void btnThuPhat_Click(object sender, EventArgs e)
        {
            if (dgvPhatChuaThu.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một khoản phạt để thu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Lấy MaPhieuPhat từ cột ẩn
            int maPhieuPhat = Convert.ToInt32(dgvPhatChuaThu.SelectedRows[0].Cells["MaPhieuPhat"].Value);

            try
            {
                using (SqlConnection c = new SqlConnection(Connection.ConString))
                {
                    c.Open();
                    string query = "UPDATE PhieuPhat SET TrangThai = @TrangThaiMoi WHERE MaPhieuPhat = @MaPhieuPhat";
                    SqlCommand cmd = new SqlCommand(query, c);
                    cmd.Parameters.AddWithValue("@TrangThaiMoi", TrangThaiPhieuPhat.Paid);
                    cmd.Parameters.AddWithValue("@MaPhieuPhat", maPhieuPhat);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Thu phạt thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        // Tải lại toàn bộ dashboard
                        LoadAllDashboardData();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật phiếu phạt: " + ex.Message);
            }
        }
    }
}