namespace QuanLyMuonTraSach
{
    partial class MuonSach
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
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbDocGia = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbTenSach = new System.Windows.Forms.ComboBox();
            this.lblThongTinSach = new System.Windows.Forms.Label();
            this.btnThemSachVaoGio = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dgvGioSach = new System.Windows.Forms.DataGridView();
            this.ColMaSach = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColTenSach = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnXacNhanMuon = new System.Windows.Forms.Button();
            this.btnHuy = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGioSach)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(236, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(234, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tạo phiếu mượn sách";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbDocGia);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(12, 53);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(650, 68);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thông tin độc giả";
            // 
            // cbDocGia
            // 
            this.cbDocGia.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbDocGia.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbDocGia.FormattingEnabled = true;
            this.cbDocGia.Location = new System.Drawing.Point(113, 28);
            this.cbDocGia.Name = "cbDocGia";
            this.cbDocGia.Size = new System.Drawing.Size(515, 21);
            this.cbDocGia.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Chọn độc giả:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbTenSach);
            this.groupBox2.Controls.Add(this.lblThongTinSach);
            this.groupBox2.Controls.Add(this.btnThemSachVaoGio);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(12, 127);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(650, 81);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Thêm sách";
            // 
            // cbTenSach
            // 
            this.cbTenSach.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbTenSach.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbTenSach.FormattingEnabled = true;
            this.cbTenSach.Location = new System.Drawing.Point(113, 31);
            this.cbTenSach.Name = "cbTenSach";
            this.cbTenSach.Size = new System.Drawing.Size(221, 21);
            this.cbTenSach.TabIndex = 4;
            // 
            // lblThongTinSach
            // 
            this.lblThongTinSach.AutoSize = true;
            this.lblThongTinSach.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblThongTinSach.ForeColor = System.Drawing.Color.Blue;
            this.lblThongTinSach.Location = new System.Drawing.Point(110, 56);
            this.lblThongTinSach.Name = "lblThongTinSach";
            this.lblThongTinSach.Size = new System.Drawing.Size(124, 13);
            this.lblThongTinSach.TabIndex = 3;
            this.lblThongTinSach.Text = "(Thông tin sách kiểm tra)";
            // 
            // btnThemSachVaoGio
            // 
            this.btnThemSachVaoGio.Location = new System.Drawing.Point(340, 29);
            this.btnThemSachVaoGio.Name = "btnThemSachVaoGio";
            this.btnThemSachVaoGio.Size = new System.Drawing.Size(121, 23);
            this.btnThemSachVaoGio.TabIndex = 2;
            this.btnThemSachVaoGio.Text = "Thêm vào giỏ";
            this.btnThemSachVaoGio.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(36, 34);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Chọn sách:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dgvGioSach);
            this.groupBox3.Location = new System.Drawing.Point(12, 214);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(650, 201);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Giỏ sách mượn";
            // 
            // dgvGioSach
            // 
            this.dgvGioSach.AllowUserToAddRows = false;
            this.dgvGioSach.AllowUserToDeleteRows = false;
            this.dgvGioSach.AllowUserToResizeRows = false;
            this.dgvGioSach.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvGioSach.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColMaSach,
            this.ColTenSach});
            this.dgvGioSach.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvGioSach.Location = new System.Drawing.Point(3, 16);
            this.dgvGioSach.Name = "dgvGioSach";
            this.dgvGioSach.ReadOnly = true;
            this.dgvGioSach.Size = new System.Drawing.Size(644, 182);
            this.dgvGioSach.TabIndex = 0;
            // 
            // ColMaSach
            // 
            this.ColMaSach.DataPropertyName = "MaSach";
            this.ColMaSach.HeaderText = "Mã sách";
            this.ColMaSach.Name = "ColMaSach";
            this.ColMaSach.ReadOnly = true;
            this.ColMaSach.Width = 150;
            // 
            // ColTenSach
            // 
            this.ColTenSach.DataPropertyName = "TenSach";
            this.ColTenSach.HeaderText = "Tên sách";
            this.ColTenSach.Name = "ColTenSach";
            this.ColTenSach.ReadOnly = true;
            this.ColTenSach.Width = 450;
            // 
            // btnXacNhanMuon
            // 
            this.btnXacNhanMuon.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXacNhanMuon.Location = new System.Drawing.Point(459, 421);
            this.btnXacNhanMuon.Name = "btnXacNhanMuon";
            this.btnXacNhanMuon.Size = new System.Drawing.Size(192, 24);
            this.btnXacNhanMuon.TabIndex = 4;
            this.btnXacNhanMuon.Text = "Xác nhận Mượn";
            this.btnXacNhanMuon.UseVisualStyleBackColor = true;
            // 
            // btnHuy
            // 
            this.btnHuy.Location = new System.Drawing.Point(352, 421);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(96, 24);
            this.btnHuy.TabIndex = 5;
            this.btnHuy.Text = "Hủy";
            this.btnHuy.UseVisualStyleBackColor = true;
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // MuonSach
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(674, 455);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnXacNhanMuon);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Name = "MuonSach";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tạo Phiếu Mượn Sách";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvGioSach)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cbDocGia;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblThongTinSach;
        private System.Windows.Forms.Button btnThemSachVaoGio;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView dgvGioSach;
        private System.Windows.Forms.Button btnXacNhanMuon;
        private System.Windows.Forms.Button btnHuy;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColMaSach;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColTenSach;
        private System.Windows.Forms.ComboBox cbTenSach;
    }
}