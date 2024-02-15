namespace Building.AppExtension
{
    partial class frmTraLoi
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
            this.meNoiDung = new DevExpress.XtraEditors.MemoEdit();
            this.btnSave = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.meNoiDung.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // meNoiDung
            // 
            this.meNoiDung.Location = new System.Drawing.Point(26, 24);
            this.meNoiDung.Name = "meNoiDung";
            this.meNoiDung.Size = new System.Drawing.Size(534, 242);
            this.meNoiDung.TabIndex = 1;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(463, 287);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(97, 32);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "LƯU";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // frmTraLoi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(583, 331);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.meNoiDung);
            this.Name = "frmTraLoi";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TRẢ LỜI";
            this.Load += new System.EventHandler(this.frmTraLoi_Load);
            ((System.ComponentModel.ISupportInitialize)(this.meNoiDung.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.MemoEdit meNoiDung;
        private System.Windows.Forms.Button btnSave;
    }
}