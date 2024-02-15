namespace FTP
{
    partial class frmDownloadFile
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
            this.lblPross = new DevExpress.XtraEditors.LabelControl();
            this.progress = new DevExpress.XtraEditors.ProgressBarControl();
            this.lblStatus = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.progress.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // lblPross
            // 
            this.lblPross.Location = new System.Drawing.Point(12, 55);
            this.lblPross.Name = "lblPross";
            this.lblPross.Size = new System.Drawing.Size(180, 13);
            this.lblPross.TabIndex = 1;
            this.lblPross.Text = "Đang tải lên 0 trên tổng số 0 bytes...";
            // 
            // progress
            // 
            this.progress.EditValue = "60";
            this.progress.Location = new System.Drawing.Point(12, 31);
            this.progress.Name = "progress";
            this.progress.Properties.EndColor = System.Drawing.Color.LightSkyBlue;
            this.progress.Properties.ShowTitle = true;
            this.progress.Size = new System.Drawing.Size(363, 18);
            this.progress.TabIndex = 2;
            // 
            // lblStatus
            // 
            this.lblStatus.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblStatus.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.lblStatus.Appearance.Options.UseFont = true;
            this.lblStatus.Appearance.Options.UseForeColor = true;
            this.lblStatus.Location = new System.Drawing.Point(12, 12);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(74, 13);
            this.lblStatus.TabIndex = 3;
            this.lblStatus.Text = "Đang tải về...";
            // 
            // frmDownloadFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(389, 79);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.progress);
            this.Controls.Add(this.lblPross);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDownloadFile";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Trình tải file";
            this.Load += new System.EventHandler(this.UploadFile_frm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.progress.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl lblPross;
        private DevExpress.XtraEditors.ProgressBarControl progress;
        private DevExpress.XtraEditors.LabelControl lblStatus;
    }
}