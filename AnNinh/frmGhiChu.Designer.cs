namespace AnNinh
{
    partial class frmGhiChu
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
            this.txtnoiDung = new DevExpress.XtraEditors.MemoEdit();
            this.btnok = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.txtnoiDung.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // txtnoiDung
            // 
            this.txtnoiDung.Location = new System.Drawing.Point(12, 31);
            this.txtnoiDung.Name = "txtnoiDung";
            this.txtnoiDung.Size = new System.Drawing.Size(500, 96);
            this.txtnoiDung.TabIndex = 0;
            // 
            // btnok
            // 
            this.btnok.Location = new System.Drawing.Point(202, 133);
            this.btnok.Name = "btnok";
            this.btnok.Size = new System.Drawing.Size(118, 31);
            this.btnok.TabIndex = 1;
            this.btnok.Text = "Chấp nhận";
            this.btnok.Click += new System.EventHandler(this.btnok_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(12, 12);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(176, 13);
            this.labelControl1.TabIndex = 2;
            this.labelControl1.Text = "Ghi chú, chú thích (Có thể để trống):";
            // 
            // frmGhiChu
            // 
            this.AcceptButton = this.btnok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(524, 176);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.btnok);
            this.Controls.Add(this.txtnoiDung);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "frmGhiChu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Ghi chú về nhiệm vụ";
            this.Load += new System.EventHandler(this.frmGhiChu_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtnoiDung.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.MemoEdit txtnoiDung;
        private DevExpress.XtraEditors.SimpleButton btnok;
        private DevExpress.XtraEditors.LabelControl labelControl1;
    }
}