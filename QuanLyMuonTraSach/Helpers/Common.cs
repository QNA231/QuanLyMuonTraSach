namespace QuanLyMuonTraSach
{
    public static class Connection
    {
        public static string ConString { get; set; } = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=QuanLyMuonTraSach;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";
    }

    public static class TrangThaiSach
    {
        public static string Available { get; set; } = "Có sẵn";
        public static string Borrow { get; set; } = "Đã mượn";
    }

    public static class TrangThaiPhieuPhat
    {
        public static string NotPaid { get; set; } = "Chưa đóng";
        public static string Paid { get; set; } = "Đã đóng";
    }

    public static class TrangThaiPhieuMuon
    {
        public static string Borrowing { get; set; } = "Đang mượn";
        public static string Returned { get; set; } = "Đã trả";
    }
}
