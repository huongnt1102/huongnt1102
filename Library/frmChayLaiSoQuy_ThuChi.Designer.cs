namespace Library
{
    partial class frmChayLaiSoQuy_ThuChi
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
            this.btnRun = new DevExpress.XtraEditors.SimpleButton();
            this.lblSoLuong = new DevExpress.XtraEditors.LabelControl();
            this.SuspendLayout();
            // 
            // btnRun
            // 
            this.btnRun.Location = new System.Drawing.Point(137, 107);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(75, 23);
            this.btnRun.TabIndex = 0;
            this.btnRun.Text = "Bắt đầu";
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // lblSoLuong
            // 
            this.lblSoLuong.Location = new System.Drawing.Point(149, 74);
            this.lblSoLuong.Name = "lblSoLuong";
            this.lblSoLuong.Size = new System.Drawing.Size(41, 13);
            this.lblSoLuong.TabIndex = 1;
            this.lblSoLuong.Text = "SoLuong";
            // 
            // frmChayLaiSoQuy_ThuChi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 200);
            this.Controls.Add(this.lblSoLuong);
            this.Controls.Add(this.btnRun);
            this.Name = "frmChayLaiSoQuy_ThuChi";
            this.Text = "frmChayLaiSoQuy_ThuChi";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnRun;
        private DevExpress.XtraEditors.LabelControl lblSoLuong;
    }
}