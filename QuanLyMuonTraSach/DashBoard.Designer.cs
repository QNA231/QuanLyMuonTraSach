namespace QuanLyMuonTraSach
{
    partial class DashBoard
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.MuonSachNav = new System.Windows.Forms.ToolStripMenuItem();
            this.TraSachNav = new System.Windows.Forms.ToolStripMenuItem();
            this.SachNav = new System.Windows.Forms.ToolStripMenuItem();
            this.DocGiaNav = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgvTreHan = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgvDangMuon = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnThuPhat = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.dgvPhatChuaThu = new System.Windows.Forms.DataGridView();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblChuaThu = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblDaThu = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SoNgayTre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SoTienPhat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MaPhieuPhat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.menuStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTreHan)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDangMuon)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhatChuaThu)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MuonSachNav,
            this.TraSachNav,
            this.SachNav,
            this.DocGiaNav});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // MuonSachNav
            // 
            this.MuonSachNav.Name = "MuonSachNav";
            this.MuonSachNav.Size = new System.Drawing.Size(78, 20);
            this.MuonSachNav.Text = "Mượn sách";
            this.MuonSachNav.Click += new System.EventHandler(this.MuonSachNav_Click);
            // 
            // TraSachNav
            // 
            this.TraSachNav.Name = "TraSachNav";
            this.TraSachNav.Size = new System.Drawing.Size(62, 20);
            this.TraSachNav.Text = "Trả sách";
            this.TraSachNav.Click += new System.EventHandler(this.TraSachNav_Click);
            // 
            // SachNav
            // 
            this.SachNav.Name = "SachNav";
            this.SachNav.Size = new System.Drawing.Size(44, 20);
            this.SachNav.Text = "Sách";
            this.SachNav.Click += new System.EventHandler(this.SachNav_Click);
            // 
            // DocGiaNav
            // 
            this.DocGiaNav.Name = "DocGiaNav";
            this.DocGiaNav.Size = new System.Drawing.Size(59, 20);
            this.DocGiaNav.Text = "Độc giả";
            this.DocGiaNav.Click += new System.EventHandler(this.DocGiaNav_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 40);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(776, 398);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(768, 372);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Tình hình Mượn/Trễ";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.dgvTreHan);
            this.groupBox2.Location = new System.Drawing.Point(6, 191);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(756, 175);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Sách Trễ Hạn (Chưa Trả)";
            // 
            // dgvTreHan
            // 
            this.dgvTreHan.AllowUserToAddRows = false;
            this.dgvTreHan.AllowUserToDeleteRows = false;
            this.dgvTreHan.AllowUserToResizeRows = false;
            this.dgvTreHan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTreHan.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6,
            this.SoNgayTre});
            this.dgvTreHan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTreHan.Location = new System.Drawing.Point(3, 16);
            this.dgvTreHan.Name = "dgvTreHan";
            this.dgvTreHan.ReadOnly = true;
            this.dgvTreHan.Size = new System.Drawing.Size(750, 156);
            this.dgvTreHan.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.dgvDangMuon);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(756, 179);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Sách Đang Mượn";
            // 
            // dgvDangMuon
            // 
            this.dgvDangMuon.AllowUserToAddRows = false;
            this.dgvDangMuon.AllowUserToDeleteRows = false;
            this.dgvDangMuon.AllowUserToResizeRows = false;
            this.dgvDangMuon.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDangMuon.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3});
            this.dgvDangMuon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDangMuon.Location = new System.Drawing.Point(3, 16);
            this.dgvDangMuon.Name = "dgvDangMuon";
            this.dgvDangMuon.ReadOnly = true;
            this.dgvDangMuon.Size = new System.Drawing.Size(750, 160);
            this.dgvDangMuon.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btnThuPhat);
            this.tabPage2.Controls.Add(this.groupBox4);
            this.tabPage2.Controls.Add(this.groupBox3);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(768, 372);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Tình hình Tiền Phạt";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnThuPhat
            // 
            this.btnThuPhat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnThuPhat.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThuPhat.Location = new System.Drawing.Point(593, 335);
            this.btnThuPhat.Name = "btnThuPhat";
            this.btnThuPhat.Size = new System.Drawing.Size(169, 31);
            this.btnThuPhat.TabIndex = 2;
            this.btnThuPhat.Text = "Xác nhận Thu (Đã chọn)";
            this.btnThuPhat.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.dgvPhatChuaThu);
            this.groupBox4.Location = new System.Drawing.Point(6, 89);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(756, 240);
            this.groupBox4.TabIndex = 1;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Các khoản phạt CHƯA THU";
            // 
            // dgvPhatChuaThu
            // 
            this.dgvPhatChuaThu.AllowUserToAddRows = false;
            this.dgvPhatChuaThu.AllowUserToDeleteRows = false;
            this.dgvPhatChuaThu.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPhatChuaThu.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn7,
            this.dataGridViewTextBoxColumn8,
            this.dataGridViewTextBoxColumn9,
            this.SoTienPhat,
            this.MaPhieuPhat});
            this.dgvPhatChuaThu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPhatChuaThu.Location = new System.Drawing.Point(3, 16);
            this.dgvPhatChuaThu.MultiSelect = false;
            this.dgvPhatChuaThu.Name = "dgvPhatChuaThu";
            this.dgvPhatChuaThu.ReadOnly = true;
            this.dgvPhatChuaThu.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPhatChuaThu.Size = new System.Drawing.Size(750, 221);
            this.dgvPhatChuaThu.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.lblChuaThu);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.lblDaThu);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Location = new System.Drawing.Point(6, 6);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(756, 77);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Doanh thu";
            // 
            // lblChuaThu
            // 
            this.lblChuaThu.AutoSize = true;
            this.lblChuaThu.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChuaThu.ForeColor = System.Drawing.Color.Red;
            this.lblChuaThu.Location = new System.Drawing.Point(404, 45);
            this.lblChuaThu.Name = "lblChuaThu";
            this.lblChuaThu.Size = new System.Drawing.Size(34, 20);
            this.lblChuaThu.TabIndex = 3;
            this.lblChuaThu.Text = "0 đ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(404, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(137, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "Tổng phạt còn nợ:";
            // 
            // lblDaThu
            // 
            this.lblDaThu.AutoSize = true;
            this.lblDaThu.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDaThu.ForeColor = System.Drawing.Color.Green;
            this.lblDaThu.Location = new System.Drawing.Point(100, 45);
            this.lblDaThu.Name = "lblDaThu";
            this.lblDaThu.Size = new System.Drawing.Size(34, 20);
            this.lblDaThu.TabIndex = 1;
            this.lblDaThu.Text = "0 đ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(100, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(174, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tổng doanh thu đã thu:";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.Location = new System.Drawing.Point(713, 27);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 2;
            this.btnRefresh.Text = "Làm mới";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "TenSach";
            this.dataGridViewTextBoxColumn1.HeaderText = "Tên Sách";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 350;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "TenDocGia";
            this.dataGridViewTextBoxColumn2.HeaderText = "Độc Giả Mượn";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 200;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "NgayHenTra";
            this.dataGridViewTextBoxColumn3.HeaderText = "Ngày Hẹn Trả";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 150;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "TenSach";
            this.dataGridViewTextBoxColumn4.HeaderText = "Tên Sách";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Width = 300;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "TenDocGia";
            this.dataGridViewTextBoxColumn5.HeaderText = "Độc Giả Mượn";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.Width = 200;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.DataPropertyName = "NgayHenTra";
            this.dataGridViewTextBoxColumn6.HeaderText = "Ngày Hẹn Trả";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            // 
            // SoNgayTre
            // 
            this.SoNgayTre.DataPropertyName = "SoNgayTre";
            this.SoNgayTre.HeaderText = "Số Ngày Trễ";
            this.SoNgayTre.Name = "SoNgayTre";
            this.SoNgayTre.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.DataPropertyName = "TenDocGia";
            this.dataGridViewTextBoxColumn7.HeaderText = "Tên Độc Giả";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            this.dataGridViewTextBoxColumn7.Width = 200;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.DataPropertyName = "TenSach";
            this.dataGridViewTextBoxColumn8.HeaderText = "Sách Trả Trễ";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.ReadOnly = true;
            this.dataGridViewTextBoxColumn8.Width = 250;
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.DataPropertyName = "SoNgayTre";
            this.dataGridViewTextBoxColumn9.HeaderText = "Số Ngày Trễ";
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            this.dataGridViewTextBoxColumn9.ReadOnly = true;
            // 
            // SoTienPhat
            // 
            this.SoTienPhat.DataPropertyName = "SoTienPhat";
            this.SoTienPhat.HeaderText = "Tiền Phạt";
            this.SoTienPhat.Name = "SoTienPhat";
            this.SoTienPhat.ReadOnly = true;
            // 
            // MaPhieuPhat
            // 
            this.MaPhieuPhat.DataPropertyName = "MaPhieuPhat";
            this.MaPhieuPhat.HeaderText = "MaPhieuPhat";
            this.MaPhieuPhat.Name = "MaPhieuPhat";
            this.MaPhieuPhat.ReadOnly = true;
            this.MaPhieuPhat.Visible = false;
            // 
            // DashBoard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "DashBoard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Trang Chủ - Quản Lý Thư Viện";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTreHan)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDangMuon)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhatChuaThu)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem MuonSachNav;
        private System.Windows.Forms.ToolStripMenuItem TraSachNav;
        private System.Windows.Forms.ToolStripMenuItem SachNav;
        private System.Windows.Forms.ToolStripMenuItem DocGiaNav;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgvTreHan;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgvDangMuon;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.DataGridView dgvPhatChuaThu;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label lblChuaThu;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblDaThu;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnThuPhat;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn SoNgayTre;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private System.Windows.Forms.DataGridViewTextBoxColumn SoTienPhat;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaPhieuPhat;
    }
}

