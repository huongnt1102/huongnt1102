namespace DichVu.SendMail
{
    partial class frmEmailAccount
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEmailAccount));
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtServer = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtPort = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtUID = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.txtPWD = new DevExpress.XtraEditors.TextEdit();
            this.btnHuy = new DevExpress.XtraEditors.SimpleButton();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection();
            this.btnChapNhan = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.txtdiachiEmail = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.txtServer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPort.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPWD.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtdiachiEmail.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(12, 15);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(36, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Server:";
            // 
            // txtServer
            // 
            this.txtServer.Location = new System.Drawing.Point(94, 12);
            this.txtServer.Name = "txtServer";
            this.txtServer.Size = new System.Drawing.Size(200, 20);
            this.txtServer.TabIndex = 0;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(313, 15);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(24, 13);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "Port:";
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(343, 12);
            this.txtPort.Name = "txtPort";
            this.txtPort.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtPort.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtPort.Size = new System.Drawing.Size(57, 20);
            this.txtPort.TabIndex = 1;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(12, 67);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(76, 13);
            this.labelControl3.TabIndex = 0;
            this.labelControl3.Text = "Tên đăng nhập:";
            // 
            // txtUID
            // 
            this.txtUID.Location = new System.Drawing.Point(94, 64);
            this.txtUID.Name = "txtUID";
            this.txtUID.Size = new System.Drawing.Size(306, 20);
            this.txtUID.TabIndex = 3;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(12, 93);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(48, 13);
            this.labelControl4.TabIndex = 0;
            this.labelControl4.Text = "Mật khẩu:";
            // 
            // txtPWD
            // 
            this.txtPWD.Location = new System.Drawing.Point(94, 90);
            this.txtPWD.Name = "txtPWD";
            this.txtPWD.Properties.UseSystemPasswordChar = true;
            this.txtPWD.Size = new System.Drawing.Size(306, 20);
            this.txtPWD.TabIndex = 4;
            // 
            // btnHuy
            // 
            this.btnHuy.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnHuy.ImageOptions.ImageIndex = 1;
            this.btnHuy.ImageOptions.ImageList = this.imageCollection1;
            this.btnHuy.Location = new System.Drawing.Point(300, 116);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(100, 26);
            this.btnHuy.TabIndex = 6;
            this.btnHuy.Text = "Hủy";
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.Images.SetKeyName(0, "icons8_save.png");  
            this.imageCollection1.Images.SetKeyName(1, "icons8_cancel1.png");  
            // 
            // btnChapNhan
            // 
            this.btnChapNhan.ImageOptions.ImageIndex = 0;
            this.btnChapNhan.ImageOptions.ImageList = this.imageCollection1;
            this.btnChapNhan.Location = new System.Drawing.Point(195, 116);
            this.btnChapNhan.Name = "btnChapNhan";
            this.btnChapNhan.Size = new System.Drawing.Size(99, 26);
            this.btnChapNhan.TabIndex = 5;
            this.btnChapNhan.Text = "Lưu và thoát";
            this.btnChapNhan.Click += new System.EventHandler(this.btnChapNhan_Click);
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(12, 41);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(63, 13);
            this.labelControl5.TabIndex = 0;
            this.labelControl5.Text = "Địa chỉ Email:";
            // 
            // txtdiachiEmail
            // 
            this.txtdiachiEmail.Location = new System.Drawing.Point(94, 38);
            this.txtdiachiEmail.Name = "txtdiachiEmail";
            this.txtdiachiEmail.Size = new System.Drawing.Size(306, 20);
            this.txtdiachiEmail.TabIndex = 2;
            // 
            // frmEmailAccount
            // 
            this.AcceptButton = this.btnChapNhan;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnHuy;
            this.ClientSize = new System.Drawing.Size(412, 152);
            this.Controls.Add(this.btnChapNhan);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.txtPort);
            this.Controls.Add(this.txtPWD);
            this.Controls.Add(this.txtdiachiEmail);
            this.Controls.Add(this.txtUID);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.txtServer);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmEmailAccount";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Tài khoản gửi mail";
            this.Load += new System.EventHandler(this.frmEmailAccount_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtServer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPort.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPWD.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtdiachiEmail.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtServer;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txtPort;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit txtUID;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.TextEdit txtPWD;
        private DevExpress.XtraEditors.SimpleButton btnHuy;
        private DevExpress.XtraEditors.SimpleButton btnChapNhan;
        private DevExpress.Utils.ImageCollection imageCollection1;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.TextEdit txtdiachiEmail;
    }
}