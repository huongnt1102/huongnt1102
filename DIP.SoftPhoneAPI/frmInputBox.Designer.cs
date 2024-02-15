namespace DIP.SoftPhoneAPI
{
    partial class frmInputBox
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
            this.txtText = new DevExpress.XtraEditors.TextEdit();
            this.btnHuy = new DevExpress.XtraEditors.SimpleButton();
            this.btnDongY = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.txtText.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // txtText
            // 
            this.txtText.EditValue = "";
            this.txtText.Location = new System.Drawing.Point(12, 12);
            this.txtText.Name = "txtText";
            this.txtText.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI Light", 16F);
            this.txtText.Properties.Appearance.Options.UseFont = true;
            this.txtText.Properties.LookAndFeel.SkinName = "McSkin";
            this.txtText.Properties.LookAndFeel.UseDefaultLookAndFeel = false;
            this.txtText.Size = new System.Drawing.Size(260, 36);
            this.txtText.TabIndex = 22;
            // 
            // btnHuy
            // 
            this.btnHuy.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnHuy.Appearance.Options.UseFont = true;
            this.btnHuy.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnHuy.Location = new System.Drawing.Point(207, 54);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(65, 23);
            this.btnHuy.TabIndex = 23;
            this.btnHuy.Text = "Hủy";
            // 
            // btnDongY
            // 
            this.btnDongY.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnDongY.Appearance.Options.UseFont = true;
            this.btnDongY.Location = new System.Drawing.Point(116, 54);
            this.btnDongY.Name = "btnDongY";
            this.btnDongY.Size = new System.Drawing.Size(85, 23);
            this.btnDongY.TabIndex = 23;
            this.btnDongY.Text = "Đồng ý";
            this.btnDongY.Click += new System.EventHandler(this.btnDongY_Click);
            // 
            // frmInputBox
            // 
            this.AcceptButton = this.btnDongY;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnHuy;
            this.ClientSize = new System.Drawing.Size(284, 87);
            this.Controls.Add(this.btnDongY);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.txtText);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmInputBox";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hộp nhập liệu";
            this.Load += new System.EventHandler(this.frmInputBox_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtText.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit txtText;
        private DevExpress.XtraEditors.SimpleButton btnHuy;
        private DevExpress.XtraEditors.SimpleButton btnDongY;
    }
}