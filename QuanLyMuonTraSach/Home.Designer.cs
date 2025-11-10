namespace QuanLyMuonTraSach
{
    partial class Home
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
            this.menuStrip1.SuspendLayout();
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
            // 
            // TraSachNav
            // 
            this.TraSachNav.Name = "TraSachNav";
            this.TraSachNav.Size = new System.Drawing.Size(62, 20);
            this.TraSachNav.Text = "Trả sách";
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
            // Home
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Home";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem MuonSachNav;
        private System.Windows.Forms.ToolStripMenuItem TraSachNav;
        private System.Windows.Forms.ToolStripMenuItem SachNav;
        private System.Windows.Forms.ToolStripMenuItem DocGiaNav;
    }
}

