namespace DichVu.KhachHang
{
    partial class frmTaiLieu
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
            this.ctlTaiLieu1 = new Document.ctlTaiLieu();
            this.SuspendLayout();
            // 
            // ctlTaiLieu1
            // 
            this.ctlTaiLieu1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctlTaiLieu1.FormID = null;
            this.ctlTaiLieu1.LinkID = null;
            this.ctlTaiLieu1.Location = new System.Drawing.Point(0, 0);
            this.ctlTaiLieu1.MaNV = null;
            this.ctlTaiLieu1.Name = "ctlTaiLieu1";
            this.ctlTaiLieu1.Size = new System.Drawing.Size(727, 385);
            this.ctlTaiLieu1.TabIndex = 1;
            // 
            // frmTaiLieu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(727, 385);
            this.Controls.Add(this.ctlTaiLieu1);
            this.Name = "frmTaiLieu";
            this.Text = "frmTaiLieu";
            this.ResumeLayout(false);

        }

        #endregion

        private Document.ctlTaiLieu ctlTaiLieu1;
    }
}