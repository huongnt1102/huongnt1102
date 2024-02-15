namespace DichVu.ThongKe
{
    partial class frmHopDong
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
            this.pnlHDTTTT = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.pnlHDTTTT)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlHDTTTT
            // 
            this.pnlHDTTTT.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlHDTTTT.Location = new System.Drawing.Point(0, 0);
            this.pnlHDTTTT.Name = "pnlHDTTTT";
            this.pnlHDTTTT.Size = new System.Drawing.Size(615, 398);
            this.pnlHDTTTT.TabIndex = 0;
            // 
            // frmHopDong
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(615, 398);
            this.Controls.Add(this.pnlHDTTTT);
            this.Name = "frmHopDong";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Thống kê hợp đồng";
            this.Load += new System.EventHandler(this.frmHopDong_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pnlHDTTTT)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl pnlHDTTTT;
    }
}