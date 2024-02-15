namespace DIPCRM.Need.Reports
{
    partial class frmOption
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
            this.btnImage = new DevExpress.XtraEditors.SimpleButton();
            this.btnPdf = new DevExpress.XtraEditors.SimpleButton();
            this.btnExcel = new DevExpress.XtraEditors.SimpleButton();
            this.btnWord = new DevExpress.XtraEditors.SimpleButton();
            this.SuspendLayout();
            // 
            // btnImage
            // 
            this.btnImage.ImageLocation = DevExpress.XtraEditors.ImageLocation.TopCenter;
            this.btnImage.Location = new System.Drawing.Point(316, 30);
            this.btnImage.Name = "btnImage";
            this.btnImage.Size = new System.Drawing.Size(73, 67);
            this.btnImage.TabIndex = 0;
            this.btnImage.Text = "File Image";
            this.btnImage.Click += new System.EventHandler(this.btnImage_Click);
            // 
            // btnPdf
            // 
            this.btnPdf.ImageLocation = DevExpress.XtraEditors.ImageLocation.TopCenter;
            this.btnPdf.Location = new System.Drawing.Point(225, 30);
            this.btnPdf.Name = "btnPdf";
            this.btnPdf.Size = new System.Drawing.Size(73, 67);
            this.btnPdf.TabIndex = 0;
            this.btnPdf.Text = "File Pdf";
            this.btnPdf.Click += new System.EventHandler(this.btnPdf_Click);
            // 
            // btnExcel
            // 
            this.btnExcel.ImageLocation = DevExpress.XtraEditors.ImageLocation.TopCenter;
            this.btnExcel.Location = new System.Drawing.Point(130, 30);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(73, 67);
            this.btnExcel.TabIndex = 0;
            this.btnExcel.Text = "File Excel";
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // btnWord
            // 
            this.btnWord.ImageLocation = DevExpress.XtraEditors.ImageLocation.TopCenter;
            this.btnWord.Location = new System.Drawing.Point(36, 30);
            this.btnWord.Name = "btnWord";
            this.btnWord.Size = new System.Drawing.Size(73, 67);
            this.btnWord.TabIndex = 0;
            this.btnWord.Text = "File Word";
            this.btnWord.Click += new System.EventHandler(this.btnWord_Click);
            // 
            // frmOption
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(425, 131);
            this.Controls.Add(this.btnImage);
            this.Controls.Add(this.btnPdf);
            this.Controls.Add(this.btnExcel);
            this.Controls.Add(this.btnWord);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmOption";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tùy chọn";
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnWord;
        private DevExpress.XtraEditors.SimpleButton btnExcel;
        private DevExpress.XtraEditors.SimpleButton btnPdf;
        private DevExpress.XtraEditors.SimpleButton btnImage;
    }
}