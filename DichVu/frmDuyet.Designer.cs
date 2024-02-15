namespace DichVu
{
    partial class frmDuyet
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
            this.btnSave = new System.Windows.Forms.Button();
            this.meNoiDung = new DevExpress.XtraEditors.MemoEdit();
            this.checkDuyet = new DevExpress.XtraEditors.CheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.meNoiDung.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkDuyet.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(460, 204);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(97, 32);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "LƯU";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // meNoiDung
            // 
            this.meNoiDung.Location = new System.Drawing.Point(23, 24);
            this.meNoiDung.Name = "meNoiDung";
            this.meNoiDung.Size = new System.Drawing.Size(534, 168);
            this.meNoiDung.TabIndex = 3;
            // 
            // checkDuyet
            // 
            this.checkDuyet.Location = new System.Drawing.Point(379, 211);
            this.checkDuyet.Name = "checkDuyet";
            this.checkDuyet.Properties.Caption = "Duyệt";
            this.checkDuyet.Size = new System.Drawing.Size(75, 19);
            this.checkDuyet.TabIndex = 5;
            // 
            // frmDuyet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 248);
            this.Controls.Add(this.checkDuyet);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.meNoiDung);
            this.Name = "frmDuyet";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DUYỆT ĐĂNG KÝ";
            this.Load += new System.EventHandler(this.frmDuyet_Load);
            ((System.ComponentModel.ISupportInitialize)(this.meNoiDung.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkDuyet.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSave;
        private DevExpress.XtraEditors.MemoEdit meNoiDung;
        private DevExpress.XtraEditors.CheckEdit checkDuyet;
    }
}