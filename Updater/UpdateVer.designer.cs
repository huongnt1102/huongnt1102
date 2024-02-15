namespace Updater
{
    partial class UpdateVer
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
            this.progress = new DevExpress.XtraEditors.ProgressBarControl();
            this.lblStatus = new DevExpress.XtraEditors.LabelControl();
            this.memoEdit1 = new DevExpress.XtraEditors.MemoEdit();
            ((System.ComponentModel.ISupportInitialize)(this.progress.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoEdit1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // progress
            // 
            this.progress.EditValue = "0";
            this.progress.Location = new System.Drawing.Point(12, 31);
            this.progress.Name = "progress";
            this.progress.Size = new System.Drawing.Size(425, 18);
            this.progress.TabIndex = 0;
            // 
            // lblStatus
            // 
            this.lblStatus.Location = new System.Drawing.Point(12, 12);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(292, 13);
            this.lblStatus.TabIndex = 1;
            this.lblStatus.Text = "Cập nhật phiên bản mới nhất của phần mềm LandSoftBuilding";
            // 
            // memoEdit1
            // 
            this.memoEdit1.Location = new System.Drawing.Point(13, 56);
            this.memoEdit1.Name = "memoEdit1";
            this.memoEdit1.Properties.ReadOnly = true;
            this.memoEdit1.Size = new System.Drawing.Size(424, 108);
            this.memoEdit1.TabIndex = 2;
            // 
            // UpdateVer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(450, 181);
            this.Controls.Add(this.memoEdit1);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.progress);
            this.FormBorderEffect = DevExpress.XtraEditors.FormBorderEffect.None;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UpdateVer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LandSoft Building - Cập nhật phiên bản mới";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.UpdateVer_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.UpdateVer_FormClosed);
            this.Load += new System.EventHandler(this.UpdateVer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.progress.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoEdit1.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.ProgressBarControl progress;
        private DevExpress.XtraEditors.LabelControl lblStatus;
        private DevExpress.XtraEditors.MemoEdit memoEdit1;
    }
}