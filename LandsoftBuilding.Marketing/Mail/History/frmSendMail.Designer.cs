namespace LandSoftBuilding.Marketing.Mail.History
{
    partial class frmSendMail
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
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.lkTaiKhoan = new DevExpress.XtraEditors.LookUpEdit();
            this.ckbLoaiDichVu = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.btnTemplate = new DevExpress.XtraEditors.SimpleButton();
            this.btnFields = new DevExpress.XtraEditors.SimpleButton();
            this.lkEmailSend = new DevExpress.XtraEditors.LookUpEdit();
            this.txtTieuDe = new DevExpress.XtraEditors.TextEdit();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.txtNoiDung = new MSDN.Html.Editor.HtmlEditorControl();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lkTaiKhoan.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckbLoaiDichVu.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkEmailSend.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTieuDe.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.txtNoiDung);
            this.layoutControl1.Controls.Add(this.lkTaiKhoan);
            this.layoutControl1.Controls.Add(this.ckbLoaiDichVu);
            this.layoutControl1.Controls.Add(this.btnTemplate);
            this.layoutControl1.Controls.Add(this.btnFields);
            this.layoutControl1.Controls.Add(this.lkEmailSend);
            this.layoutControl1.Controls.Add(this.txtTieuDe);
            this.layoutControl1.Controls.Add(this.btnOK);
            this.layoutControl1.Controls.Add(this.btnCancel);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(655, 368);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // lkTaiKhoan
            // 
            this.lkTaiKhoan.Location = new System.Drawing.Point(136, 60);
            this.lkTaiKhoan.Name = "lkTaiKhoan";
            this.lkTaiKhoan.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkTaiKhoan.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("SoTK", 30, "Số TK"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ChuTK", 30, "Chủ TK"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenNH", 40, "Tên ngân hàng")});
            this.lkTaiKhoan.Properties.DisplayMember = "SoTK";
            this.lkTaiKhoan.Properties.NullText = "";
            this.lkTaiKhoan.Properties.ShowLines = false;
            this.lkTaiKhoan.Properties.ValueMember = "ID";
            this.lkTaiKhoan.Size = new System.Drawing.Size(507, 20);
            this.lkTaiKhoan.StyleController = this.layoutControl1;
            this.lkTaiKhoan.TabIndex = 7;
            // 
            // ckbLoaiDichVu
            // 
            this.ckbLoaiDichVu.Location = new System.Drawing.Point(136, 36);
            this.ckbLoaiDichVu.Name = "ckbLoaiDichVu";
            this.ckbLoaiDichVu.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ckbLoaiDichVu.Properties.DisplayMember = "TenLDV";
            this.ckbLoaiDichVu.Properties.SelectAllItemCaption = "Chọn tất cả";
            this.ckbLoaiDichVu.Properties.ValueMember = "ID";
            this.ckbLoaiDichVu.Size = new System.Drawing.Size(507, 20);
            this.ckbLoaiDichVu.StyleController = this.layoutControl1;
            this.ckbLoaiDichVu.TabIndex = 6;
            // 
            // btnTemplate
            // 
            this.btnTemplate.Location = new System.Drawing.Point(319, 334);
            this.btnTemplate.Name = "btnTemplate";
            this.btnTemplate.Size = new System.Drawing.Size(78, 22);
            this.btnTemplate.StyleController = this.layoutControl1;
            this.btnTemplate.TabIndex = 9;
            this.btnTemplate.Text = "Mẫu";
            this.btnTemplate.Click += new System.EventHandler(this.btnTemplate_Click);
            // 
            // btnFields
            // 
            this.btnFields.Location = new System.Drawing.Point(401, 334);
            this.btnFields.Name = "btnFields";
            this.btnFields.Size = new System.Drawing.Size(78, 22);
            this.btnFields.StyleController = this.layoutControl1;
            this.btnFields.TabIndex = 8;
            this.btnFields.Text = "Trường trộn";
            this.btnFields.Click += new System.EventHandler(this.btnFields_Click);
            // 
            // lkEmailSend
            // 
            this.lkEmailSend.Location = new System.Drawing.Point(136, 12);
            this.lkEmailSend.Name = "lkEmailSend";
            this.lkEmailSend.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkEmailSend.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Email", "Name1")});
            this.lkEmailSend.Properties.DisplayMember = "Email";
            this.lkEmailSend.Properties.NullText = "";
            this.lkEmailSend.Properties.ShowHeader = false;
            this.lkEmailSend.Properties.ShowLines = false;
            this.lkEmailSend.Properties.ValueMember = "ID";
            this.lkEmailSend.Size = new System.Drawing.Size(507, 20);
            this.lkEmailSend.StyleController = this.layoutControl1;
            this.lkEmailSend.TabIndex = 7;
            // 
            // txtTieuDe
            // 
            this.txtTieuDe.Location = new System.Drawing.Point(136, 84);
            this.txtTieuDe.Name = "txtTieuDe";
            this.txtTieuDe.Size = new System.Drawing.Size(507, 20);
            this.txtTieuDe.StyleController = this.layoutControl1;
            this.txtTieuDe.TabIndex = 6;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(483, 334);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(78, 22);
            this.btnOK.StyleController = this.layoutControl1;
            this.btnOK.TabIndex = 5;
            this.btnOK.Text = "Thực hiện";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(565, 334);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(78, 22);
            this.btnCancel.StyleController = this.layoutControl1;
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Hủy";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.emptySpaceItem2,
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem5,
            this.layoutControlItem6,
            this.layoutControlItem4,
            this.layoutControlItem8,
            this.layoutControlItem9,
            this.layoutControlItem3,
            this.layoutControlItem7});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(655, 368);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.btnCancel;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(553, 322);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(82, 26);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.btnOK;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(471, 322);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(82, 26);
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.btnFields;
            this.layoutControlItem5.CustomizationFormText = "layoutControlItem5";
            this.layoutControlItem5.Location = new System.Drawing.Point(389, 322);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(82, 26);
            this.layoutControlItem5.Text = "layoutControlItem5";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextToControlDistance = 0;
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.btnTemplate;
            this.layoutControlItem6.CustomizationFormText = "layoutControlItem6";
            this.layoutControlItem6.Location = new System.Drawing.Point(307, 322);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(82, 26);
            this.layoutControlItem6.Text = "layoutControlItem6";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextToControlDistance = 0;
            this.layoutControlItem6.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.lkEmailSend;
            this.layoutControlItem4.CustomizationFormText = "Email gửi (*):";
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(635, 24);
            this.layoutControlItem4.Text = "Email gửi (*):";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(121, 13);
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.ckbLoaiDichVu;
            this.layoutControlItem8.CustomizationFormText = "Loại dịch vụ:";
            this.layoutControlItem8.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(635, 24);
            this.layoutControlItem8.Text = "Loại dịch vụ (*):";
            this.layoutControlItem8.TextSize = new System.Drawing.Size(121, 13);
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.Control = this.lkTaiKhoan;
            this.layoutControlItem9.CustomizationFormText = "Tài khoản ngân hàng:";
            this.layoutControlItem9.Location = new System.Drawing.Point(0, 48);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Size = new System.Drawing.Size(635, 24);
            this.layoutControlItem9.Text = "Tài khoản ngân hàng (*):";
            this.layoutControlItem9.TextSize = new System.Drawing.Size(121, 13);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.txtTieuDe;
            this.layoutControlItem3.CustomizationFormText = "Tiêu đề (*):";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 72);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(635, 24);
            this.layoutControlItem3.Text = "Tiêu đề (*):";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(121, 13);
            // 
            // txtNoiDung
            // 
            this.txtNoiDung.InnerText = null;
            this.txtNoiDung.Location = new System.Drawing.Point(136, 108);
            this.txtNoiDung.Name = "txtNoiDung";
            this.txtNoiDung.Size = new System.Drawing.Size(507, 222);
            this.txtNoiDung.TabIndex = 5;
            this.txtNoiDung.ToolbarDock = System.Windows.Forms.DockStyle.Top;
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.txtNoiDung;
            this.layoutControlItem7.CustomizationFormText = "Nội dung (*):";
            this.layoutControlItem7.Location = new System.Drawing.Point(0, 96);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(635, 226);
            this.layoutControlItem7.Text = "Nội dung (*):";
            this.layoutControlItem7.TextSize = new System.Drawing.Size(121, 13);
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.CustomizationFormText = "emptySpaceItem2";
            this.emptySpaceItem2.Location = new System.Drawing.Point(0, 322);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(307, 26);
            this.emptySpaceItem2.Text = "emptySpaceItem2";
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // frmSendMail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(655, 368);
            this.Controls.Add(this.layoutControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmSendMail";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gửi mail";
            this.Load += new System.EventHandler(this.frmSendMail_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lkTaiKhoan.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckbLoaiDichVu.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkEmailSend.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTieuDe.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.SimpleButton btnTemplate;
        private DevExpress.XtraEditors.SimpleButton btnFields;
        private DevExpress.XtraEditors.LookUpEdit lkEmailSend;
        private DevExpress.XtraEditors.TextEdit txtTieuDe;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraEditors.LookUpEdit lkTaiKhoan;
        private DevExpress.XtraEditors.CheckedComboBoxEdit ckbLoaiDichVu;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
        private MSDN.Html.Editor.HtmlEditorControl txtNoiDung;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
    }
}