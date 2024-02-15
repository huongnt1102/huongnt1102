namespace FTP
{
    partial class frmConfig
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmConfig));
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
            this.txtFtpUrl = new DevExpress.XtraEditors.TextEdit();
            this.txtFtpUser = new DevExpress.XtraEditors.TextEdit();
            this.txtFtpPass = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.labelApiLdap = new DevExpress.XtraEditors.LabelControl();
            this.txtApiLdap = new DevExpress.XtraEditors.TextEdit();
            this.txtWebUrl = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.btnAccept = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFtpUrl.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFtpUser.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFtpPass.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtApiLdap.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWebUrl.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.ImageOptions.ImageIndex = 1;
            this.btnCancel.ImageOptions.ImageList = this.imageCollection1;
            this.btnCancel.Location = new System.Drawing.Point(380, 172);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "Hủy";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.Images.SetKeyName(0, "Save1.png");
            this.imageCollection1.Images.SetKeyName(1, "Cancel1.png");
            // 
            // txtFtpUrl
            // 
            this.txtFtpUrl.Location = new System.Drawing.Point(101, 41);
            this.txtFtpUrl.Name = "txtFtpUrl";
            this.txtFtpUrl.Size = new System.Drawing.Size(326, 20);
            this.txtFtpUrl.TabIndex = 1;
            // 
            // txtFtpUser
            // 
            this.txtFtpUser.Location = new System.Drawing.Point(101, 67);
            this.txtFtpUser.Name = "txtFtpUser";
            this.txtFtpUser.Size = new System.Drawing.Size(326, 20);
            this.txtFtpUser.TabIndex = 1;
            // 
            // txtFtpPass
            // 
            this.txtFtpPass.Location = new System.Drawing.Point(101, 93);
            this.txtFtpPass.Name = "txtFtpPass";
            this.txtFtpPass.Properties.PasswordChar = '*';
            this.txtFtpPass.Size = new System.Drawing.Size(326, 20);
            this.txtFtpPass.TabIndex = 1;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(18, 44);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(57, 13);
            this.labelControl1.TabIndex = 2;
            this.labelControl1.Text = "Địa chỉ FTP:";
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.labelApiLdap);
            this.panelControl1.Controls.Add(this.txtApiLdap);
            this.panelControl1.Controls.Add(this.txtWebUrl);
            this.panelControl1.Controls.Add(this.labelControl4);
            this.panelControl1.Controls.Add(this.txtFtpUrl);
            this.panelControl1.Controls.Add(this.labelControl3);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.txtFtpUser);
            this.panelControl1.Controls.Add(this.txtFtpPass);
            this.panelControl1.Location = new System.Drawing.Point(12, 12);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(443, 154);
            this.panelControl1.TabIndex = 3;
            // 
            // labelApiLdap
            // 
            this.labelApiLdap.Location = new System.Drawing.Point(18, 123);
            this.labelApiLdap.Name = "labelApiLdap";
            this.labelApiLdap.Size = new System.Drawing.Size(45, 13);
            this.labelApiLdap.TabIndex = 6;
            this.labelApiLdap.Text = "Api Ldap:";
            // 
            // txtApiLdap
            // 
            this.txtApiLdap.Location = new System.Drawing.Point(101, 120);
            this.txtApiLdap.Name = "txtApiLdap";
            this.txtApiLdap.Size = new System.Drawing.Size(326, 20);
            this.txtApiLdap.TabIndex = 5;
            // 
            // txtWebUrl
            // 
            this.txtWebUrl.Location = new System.Drawing.Point(101, 15);
            this.txtWebUrl.Name = "txtWebUrl";
            this.txtWebUrl.Size = new System.Drawing.Size(326, 20);
            this.txtWebUrl.TabIndex = 3;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(18, 18);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(76, 13);
            this.labelControl4.TabIndex = 4;
            this.labelControl4.Text = "Địa chỉ website:";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(18, 96);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(48, 13);
            this.labelControl3.TabIndex = 2;
            this.labelControl3.Text = "Mật khẩu:";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(18, 70);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(76, 13);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "Tên đăng nhập:";
            // 
            // btnAccept
            // 
            this.btnAccept.ImageOptions.ImageIndex = 0;
            this.btnAccept.ImageOptions.ImageList = this.imageCollection1;
            this.btnAccept.Location = new System.Drawing.Point(275, 172);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(99, 23);
            this.btnAccept.TabIndex = 0;
            this.btnAccept.Text = "Lưu && Đóng";
            this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
            // 
            // frmConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(465, 201);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.btnAccept);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "frmConfig";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cấu hình FTP";
            this.Load += new System.EventHandler(this.frmConfig_Load);
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFtpUrl.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFtpUser.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFtpPass.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtApiLdap.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWebUrl.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.TextEdit txtFtpUrl;
        private DevExpress.XtraEditors.TextEdit txtFtpUser;
        private DevExpress.XtraEditors.TextEdit txtFtpPass;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.SimpleButton btnAccept;
        private DevExpress.XtraEditors.TextEdit txtWebUrl;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.Utils.ImageCollection imageCollection1;
        private DevExpress.XtraEditors.LabelControl labelApiLdap;
        private DevExpress.XtraEditors.TextEdit txtApiLdap;
    }
}