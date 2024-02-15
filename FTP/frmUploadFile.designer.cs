namespace FTP
{
    partial class frmUploadFile
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
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
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
            this.progress.EditValue = "0";
            this.progress.Location = new System.Drawing.Point(12, 31);
            this.progress.Name = "progress";
            this.progress.Properties.ShowTitle = true;
            this.progress.Size = new System.Drawing.Size(363, 18);
            this.progress.TabIndex = 2;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Appearance.Options.UseForeColor = true;
            this.labelControl1.Location = new System.Drawing.Point(12, 12);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(71, 13);
            this.labelControl1.TabIndex = 3;
            this.labelControl1.Text = "Đang tải lên:";
            // 
            // frmUploadFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(389, 79);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.progress);
            this.Controls.Add(this.lblPross);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmUploadFile";
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
        private DevExpress.XtraEditors.LabelControl labelControl1;
    }
}