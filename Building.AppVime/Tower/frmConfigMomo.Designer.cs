namespace Building.AppVime.Tower
{
    partial class frmConfigMomo
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
            this.chkIsUseWallet = new DevExpress.XtraEditors.CheckEdit();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.txtPublicKey = new DevExpress.XtraEditors.MemoEdit();
            this.llinkConvert = new System.Windows.Forms.LinkLabel();
            this.txtLink = new DevExpress.XtraEditors.TextEdit();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsUseWallet.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPublicKey.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLink.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // chkIsUseWallet
            // 
            this.chkIsUseWallet.Location = new System.Drawing.Point(91, 12);
            this.chkIsUseWallet.Name = "chkIsUseWallet";
            this.chkIsUseWallet.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            this.chkIsUseWallet.Properties.Caption = "sử dụng ví điện tử?";
            this.chkIsUseWallet.Size = new System.Drawing.Size(150, 19);
            this.chkIsUseWallet.TabIndex = 16;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 17;
            this.label1.Text = "Tùy chọn:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "Public Key:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(90, 202);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(237, 13);
            this.label3.TabIndex = 19;
            this.label3.Text = "Cách chuyển đổi PUB KEY Momo sang Public Key";
            // 
            // btnOK
            // 
            this.btnOK.ImageOptions.Image = global::Building.AppVime.Properties.Resources.Save_as_icon;
            this.btnOK.Location = new System.Drawing.Point(534, 344);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(107, 22);
            this.btnOK.TabIndex = 20;
            this.btnOK.Text = "Lưu";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.ImageOptions.Image = global::Building.AppVime.Properties.Resources.delete_Close_icon1;
            this.btnCancel.Location = new System.Drawing.Point(534, 372);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(107, 22);
            this.btnCancel.TabIndex = 21;
            this.btnCancel.Text = "Bỏ qua";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // txtPublicKey
            // 
            this.txtPublicKey.Location = new System.Drawing.Point(93, 37);
            this.txtPublicKey.Name = "txtPublicKey";
            this.txtPublicKey.Size = new System.Drawing.Size(548, 160);
            this.txtPublicKey.TabIndex = 22;
            // 
            // llinkConvert
            // 
            this.llinkConvert.AutoSize = true;
            this.llinkConvert.Location = new System.Drawing.Point(345, 202);
            this.llinkConvert.Name = "llinkConvert";
            this.llinkConvert.Size = new System.Drawing.Size(202, 13);
            this.llinkConvert.TabIndex = 23;
            this.llinkConvert.TabStop = true;
            this.llinkConvert.Text = "Click vào đây hoặc truy cập link bên dưới";
            this.llinkConvert.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llinkConvert_LinkClicked);
            // 
            // txtLink
            // 
            this.txtLink.EditValue = "https://superdry.apphb.com/tools/online-rsa-key-converter";
            this.txtLink.Location = new System.Drawing.Point(93, 222);
            this.txtLink.Name = "txtLink";
            this.txtLink.Size = new System.Drawing.Size(548, 20);
            this.txtLink.TabIndex = 24;
            this.txtLink.MouseDown += new System.Windows.Forms.MouseEventHandler(this.txtLink_MouseDown);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label4.Location = new System.Drawing.Point(90, 248);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(195, 13);
            this.label4.TabIndex = 25;
            this.label4.Text = "Bước 1: Kéo xuống tới PEM to XML";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(110, 294);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(131, 13);
            this.label5.TabIndex = 26;
            this.label5.Text = "nhập PUB KEY vào đây";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(110, 272);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(136, 13);
            this.label6.TabIndex = 27;
            this.label6.Text = "-----BEGIN PUBLIC KEY-----";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(110, 317);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(126, 13);
            this.label7.TabIndex = 28;
            this.label7.Text = "-----END PUBLIC KEY-----";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(252, 272);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(118, 13);
            this.label8.TabIndex = 29;
            this.label8.Text = "//Giữ nguyên đoạn này";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(252, 317);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(118, 13);
            this.label9.TabIndex = 30;
            this.label9.Text = "//Giữ nguyên đoạn này";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.label10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label10.Location = new System.Drawing.Point(90, 346);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(127, 13);
            this.label10.TabIndex = 31;
            this.label10.Text = "Bước 2: Nhấn Convert";
            this.label10.Click += new System.EventHandler(this.label10_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.label11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label11.Location = new System.Drawing.Point(90, 374);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(357, 13);
            this.label11.TabIndex = 32;
            this.label11.Text = "Bước 3: Copy hết giá trị từ <RSAKeyValue>...</RSAKeyValue>";
            // 
            // frmConfigMomo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(653, 406);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtLink);
            this.Controls.Add(this.llinkConvert);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chkIsUseWallet);
            this.Controls.Add(this.txtPublicKey);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmConfigMomo";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cấu hình Ví điện tử";
            this.Load += new System.EventHandler(this.frmConfigMomo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chkIsUseWallet.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPublicKey.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLink.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraEditors.CheckEdit chkIsUseWallet;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.MemoEdit txtPublicKey;
        private System.Windows.Forms.LinkLabel llinkConvert;
        private DevExpress.XtraEditors.TextEdit txtLink;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
    }
}