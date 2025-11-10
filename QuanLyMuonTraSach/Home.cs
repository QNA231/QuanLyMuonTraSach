using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLyMuonTraSach
{
    public partial class Home : Form
    {
        SqlConnection con = new SqlConnection("Data Source=LAPTOP-1600EKM7\\SQLEXPRESS;Initial Catalog=QuanLyMuonTraSach;Persist Security Info=True;User ID=sa;Trust Server Certificate=True");
        public Home()
        {
            InitializeComponent();
        }

        private void SachNav_Click(object sender, EventArgs e)
        {
            Sach formSach = new Sach();
            formSach.Show();
        }
    }
}
