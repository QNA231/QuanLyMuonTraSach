using System;
using Microsoft.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLyMuonTraSach
{
    public partial class Home : Form
    {
        SqlConnection con = new SqlConnection(Connection.ConString);
        public Home()
        {
            InitializeComponent();
        }

        private void SachNav_Click(object sender, EventArgs e)
        {
            Sach formSach = new Sach();
            formSach.Show();
        }

        private void DocGiaNav_Click(object sender, EventArgs e)
        {
            DocGia formDocGia = new DocGia();
            formDocGia.Show();
        }
    }
}
