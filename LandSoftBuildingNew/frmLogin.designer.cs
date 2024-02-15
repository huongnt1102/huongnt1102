namespace LandSoftBuildingMain
{
    partial class frmLogin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLogin));
            this.btnconnect = new DevExpress.XtraEditors.SimpleButton();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
            this.btnThoat = new DevExpress.XtraEditors.SimpleButton();
            this.btnAccept = new DevExpress.XtraEditors.SimpleButton();
            this.ckLuu = new DevExpress.XtraEditors.CheckEdit();
            this.lblMatKhau = new DevExpress.XtraEditors.LabelControl();
            this.lblMaSo = new DevExpress.XtraEditors.LabelControl();
            this.txtpassword = new DevExpress.XtraEditors.TextEdit();
            this.txtloginid = new DevExpress.XtraEditors.TextEdit();
            this.applicationMenu1 = new DevExpress.XtraBars.Ribbon.ApplicationMenu(this.components);
            this.ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.cbbLanguage = new DevExpress.XtraEditors.ComboBoxEdit();
            this.lblNgonNgu = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckLuu.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtpassword.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtloginid.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.applicationMenu1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbbLanguage.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnconnect
            // 
            this.btnconnect.ImageOptions.ImageIndex = 2;
            this.btnconnect.ImageOptions.ImageList = this.imageCollection1;
            this.btnconnect.Location = new System.Drawing.Point(262, 160);
            this.btnconnect.Name = "btnconnect";
            this.btnconnect.Size = new System.Drawing.Size(96, 27);
            this.btnconnect.TabIndex = 6;
            this.btnconnect.Text = "Kết nối";
            this.btnconnect.Click += new System.EventHandler(this.btnconnect_Click);
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.Images.SetKeyName(0, "Login2.png");
            this.imageCollection1.Images.SetKeyName(1, "Cancel1.png");
            this.imageCollection1.Images.SetKeyName(2, "Connect1.png");
            // 
            // btnThoat
            // 
            this.btnThoat.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnThoat.ImageOptions.ImageIndex = 1;
            this.btnThoat.ImageOptions.ImageList = this.imageCollection1;
            this.btnThoat.Location = new System.Drawing.Point(157, 160);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(99, 27);
            this.btnThoat.TabIndex = 5;
            this.btnThoat.Text = "Thoát";
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // btnAccept
            // 
            this.btnAccept.ImageOptions.ImageIndex = 0;
            this.btnAccept.ImageOptions.ImageList = this.imageCollection1;
            this.btnAccept.Location = new System.Drawing.Point(59, 160);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(92, 27);
            this.btnAccept.TabIndex = 4;
            this.btnAccept.Text = "Đăng nhập";
            this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
            // 
            // ckLuu
            // 
            this.ckLuu.Location = new System.Drawing.Point(125, 131);
            this.ckLuu.Name = "ckLuu";
            this.ckLuu.Properties.Caption = "Ghi nhớ thông tin đăng nhập";
            this.ckLuu.Size = new System.Drawing.Size(244, 19);
            this.ckLuu.TabIndex = 3;
            this.ckLuu.CheckedChanged += new System.EventHandler(this.ckLuu_CheckedChanged);
            // 
            // lblMatKhau
            // 
            this.lblMatKhau.Location = new System.Drawing.Point(45, 81);
            this.lblMatKhau.Name = "lblMatKhau";
            this.lblMatKhau.Size = new System.Drawing.Size(48, 13);
            this.lblMatKhau.TabIndex = 1;
            this.lblMatKhau.Text = "Mật khẩu:";
            // 
            // lblMaSo
            // 
            this.lblMaSo.Location = new System.Drawing.Point(45, 55);
            this.lblMaSo.Name = "lblMaSo";
            this.lblMaSo.Size = new System.Drawing.Size(76, 13);
            this.lblMaSo.TabIndex = 1;
            this.lblMaSo.Text = "Tên đăng nhập:";
            // 
            // txtpassword
            // 
            this.txtpassword.Location = new System.Drawing.Point(127, 78);
            this.txtpassword.Name = "txtpassword";
            this.txtpassword.Properties.PasswordChar = '*';
            this.txtpassword.Size = new System.Drawing.Size(242, 20);
            this.txtpassword.TabIndex = 1;
            // 
            // txtloginid
            // 
            this.txtloginid.EditValue = "";
            this.txtloginid.Location = new System.Drawing.Point(127, 52);
            this.txtloginid.Name = "txtloginid";
            this.txtloginid.Size = new System.Drawing.Size(242, 20);
            this.txtloginid.TabIndex = 0;
            // 
            // applicationMenu1
            // 
            this.applicationMenu1.Name = "applicationMenu1";
            this.applicationMenu1.Ribbon = this.ribbonControl1;
            // 
            // ribbonControl1
            // 
            this.ribbonControl1.AutoSizeItems = true;
            this.ribbonControl1.ExpandCollapseItem.Id = 0;
            this.ribbonControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbonControl1.ExpandCollapseItem});
            this.ribbonControl1.Location = new System.Drawing.Point(0, 0);
            this.ribbonControl1.MaxItemId = 1;
            this.ribbonControl1.Name = "ribbonControl1";
            this.ribbonControl1.SetPopupContextMenu(this.ribbonControl1, this.applicationMenu1);
            this.ribbonControl1.RibbonCaptionAlignment = DevExpress.XtraBars.Ribbon.RibbonCaptionAlignment.Center;
            this.ribbonControl1.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonControlStyle.Office2019;
            this.ribbonControl1.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.False;
            this.ribbonControl1.ShowCategoryInCaption = false;
            this.ribbonControl1.ShowDisplayOptionsMenuButton = DevExpress.Utils.DefaultBoolean.False;
            this.ribbonControl1.ShowExpandCollapseButton = DevExpress.Utils.DefaultBoolean.False;
            this.ribbonControl1.ShowItemCaptionsInCaptionBar = true;
            this.ribbonControl1.ShowPageHeadersMode = DevExpress.XtraBars.Ribbon.ShowPageHeadersMode.Hide;
            this.ribbonControl1.ShowToolbarCustomizeItem = false;
            this.ribbonControl1.Size = new System.Drawing.Size(422, 27);
            this.ribbonControl1.Toolbar.ShowCustomizeItem = false;
            this.ribbonControl1.ToolbarLocation = DevExpress.XtraBars.Ribbon.RibbonQuickAccessToolbarLocation.Hidden;
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.labelControl3.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.labelControl3.Appearance.Options.UseBackColor = true;
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Appearance.Options.UseForeColor = true;
            this.labelControl3.Location = new System.Drawing.Point(82, 202);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(250, 13);
            this.labelControl3.TabIndex = 5;
            this.labelControl3.Text = "Phần mềm quản lý Dự án - LandSoft Building";
            // 
            // cbbLanguage
            // 
            this.cbbLanguage.Location = new System.Drawing.Point(127, 105);
            this.cbbLanguage.MenuManager = this.ribbonControl1;
            this.cbbLanguage.Name = "cbbLanguage";
            this.cbbLanguage.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbbLanguage.Properties.Items.AddRange(new object[] {
            "Tiếng Việt",
            "English"});
            this.cbbLanguage.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cbbLanguage.Properties.EditValueChanged += new System.EventHandler(this.cbbLanguage_Properties_EditValueChanged);
            this.cbbLanguage.Size = new System.Drawing.Size(242, 20);
            this.cbbLanguage.TabIndex = 2;
            this.cbbLanguage.SelectedIndexChanged += new System.EventHandler(this.cbbLanguage_SelectedIndexChanged);
            // 
            // lblNgonNgu
            // 
            this.lblNgonNgu.Location = new System.Drawing.Point(45, 108);
            this.lblNgonNgu.Name = "lblNgonNgu";
            this.lblNgonNgu.Size = new System.Drawing.Size(51, 13);
            this.lblNgonNgu.TabIndex = 1;
            this.lblNgonNgu.Text = "Ngôn ngữ:";
            // 
            // frmLogin
            // 
            this.AcceptButton = this.btnAccept;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnThoat;
            this.ClientSize = new System.Drawing.Size(422, 237);
            this.Controls.Add(this.cbbLanguage);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.btnconnect);
            this.Controls.Add(this.btnThoat);
            this.Controls.Add(this.lblMaSo);
            this.Controls.Add(this.btnAccept);
            this.Controls.Add(this.txtloginid);
            this.Controls.Add(this.ckLuu);
            this.Controls.Add(this.txtpassword);
            this.Controls.Add(this.lblNgonNgu);
            this.Controls.Add(this.lblMatKhau);
            this.Controls.Add(this.ribbonControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.ImeMode = System.Windows.Forms.ImeMode.On;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmLogin";
            this.ribbonControl1.SetPopupContextMenu(this, this.applicationMenu1);
            this.Ribbon = this.ribbonControl1;
            this.RibbonVisibility = DevExpress.XtraBars.Ribbon.RibbonVisibility.Visible;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Đăng nhập - Landsoft Building";
            this.Load += new System.EventHandler(this.frmLogin_Load);
            this.Shown += new System.EventHandler(this.frmLogin_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckLuu.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtpassword.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtloginid.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.applicationMenu1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbbLanguage.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.CheckEdit ckLuu;
        private DevExpress.XtraEditors.LabelControl lblMatKhau;
        private DevExpress.XtraEditors.LabelControl lblMaSo;
        private DevExpress.XtraEditors.TextEdit txtpassword;
        private DevExpress.XtraEditors.TextEdit txtloginid;
        private DevExpress.XtraEditors.SimpleButton btnconnect;
        private DevExpress.XtraEditors.SimpleButton btnAccept;
        private DevExpress.XtraEditors.SimpleButton btnThoat;
        private DevExpress.XtraBars.Ribbon.ApplicationMenu applicationMenu1;
        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl1;
        private DevExpress.Utils.ImageCollection imageCollection1;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.ComboBoxEdit cbbLanguage;
        private DevExpress.XtraEditors.LabelControl lblNgonNgu;
    }
}