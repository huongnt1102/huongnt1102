namespace DichVu.SendMail
{
    partial class frmSendMailEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSendMailEdit));
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtTieuDe = new DevExpress.XtraEditors.TextEdit();
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.tpNguoiNhan = new DevExpress.XtraTab.XtraTabPage();
            this.gcNguoiNhan = new DevExpress.XtraGrid.GridControl();
            this.grvNguoiNhan = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colMaKhachHang = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lookNguoiNhan = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.colTrangThai = new DevExpress.XtraGrid.Columns.GridColumn();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
            this.tpNoiDung = new DevExpress.XtraTab.XtraTabPage();
            this.txtNoiDung = new MSDN.Html.Editor.HtmlEditorControl();
            this.btnChapNhan = new DevExpress.XtraEditors.SimpleButton();
            this.btnHuy = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.dateNgayGui = new DevExpress.XtraEditors.DateEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.lookMailFrom = new DevExpress.XtraEditors.LookUpEdit();
            this.ckSelectAll = new DevExpress.XtraEditors.CheckEdit();
            this.itemHuongDan = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.txtTieuDe.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.tpNguoiNhan.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcNguoiNhan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvNguoiNhan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookNguoiNhan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            this.tpNoiDung.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateNgayGui.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateNgayGui.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookMailFrom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckSelectAll.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(14, 15);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(44, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Tiêu đề:";
            // 
            // txtTieuDe
            // 
            this.txtTieuDe.Location = new System.Drawing.Point(95, 12);
            this.txtTieuDe.Name = "txtTieuDe";
            this.txtTieuDe.Size = new System.Drawing.Size(559, 20);
            this.txtTieuDe.TabIndex = 0;
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Location = new System.Drawing.Point(12, 90);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.tpNguoiNhan;
            this.xtraTabControl1.Size = new System.Drawing.Size(647, 290);
            this.xtraTabControl1.TabIndex = 2;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tpNguoiNhan,
            this.tpNoiDung});
            // 
            // tpNguoiNhan
            // 
            this.tpNguoiNhan.Controls.Add(this.gcNguoiNhan);
            this.tpNguoiNhan.Name = "tpNguoiNhan";
            this.tpNguoiNhan.Padding = new System.Windows.Forms.Padding(1);
            this.tpNguoiNhan.Size = new System.Drawing.Size(641, 262);
            this.tpNguoiNhan.Text = "Danh sách người nhận";
            // 
            // gcNguoiNhan
            // 
            this.gcNguoiNhan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcNguoiNhan.Location = new System.Drawing.Point(1, 1);
            this.gcNguoiNhan.MainView = this.grvNguoiNhan;
            this.gcNguoiNhan.Name = "gcNguoiNhan";
            this.gcNguoiNhan.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.lookNguoiNhan});
            this.gcNguoiNhan.Size = new System.Drawing.Size(639, 260);
            this.gcNguoiNhan.TabIndex = 3;
            this.gcNguoiNhan.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvNguoiNhan});
            // 
            // grvNguoiNhan
            // 
            this.grvNguoiNhan.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colMaKhachHang,
            this.colTrangThai});
            this.grvNguoiNhan.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.grvNguoiNhan.GridControl = this.gcNguoiNhan;
            this.grvNguoiNhan.Images = this.imageCollection1;
            this.grvNguoiNhan.Name = "grvNguoiNhan";
            this.grvNguoiNhan.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.True;
            this.grvNguoiNhan.OptionsCustomization.AllowGroup = false;
            this.grvNguoiNhan.OptionsSelection.MultiSelect = true;
            this.grvNguoiNhan.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            this.grvNguoiNhan.OptionsView.ShowGroupPanel = false;
            this.grvNguoiNhan.InitNewRow += new DevExpress.XtraGrid.Views.Grid.InitNewRowEventHandler(this.grvNguoiNhan_InitNewRow);
            // 
            // colMaKhachHang
            // 
            this.colMaKhachHang.Caption = "Người nhận";
            this.colMaKhachHang.ColumnEdit = this.lookNguoiNhan;
            this.colMaKhachHang.FieldName = "MaKH";
            this.colMaKhachHang.Name = "colMaKhachHang";
            this.colMaKhachHang.Visible = true;
            this.colMaKhachHang.VisibleIndex = 0;
            // 
            // lookNguoiNhan
            // 
            this.lookNguoiNhan.AutoHeight = false;
            this.lookNguoiNhan.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookNguoiNhan.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("KhachHang", "Khách hàng", 100, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("EmailKH", "Email", 80, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default)});
            this.lookNguoiNhan.DisplayMember = "KhachHang";
            this.lookNguoiNhan.Name = "lookNguoiNhan";
            this.lookNguoiNhan.NullText = "";
            this.lookNguoiNhan.ValueMember = "MaKH";
            // 
            // colTrangThai
            // 
            this.colTrangThai.Caption = "TrangThai";
            this.colTrangThai.FieldName = "TrangThai";
            this.colTrangThai.Name = "colTrangThai";
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.Images.SetKeyName(0, "icons8_save.png");
            this.imageCollection1.Images.SetKeyName(1, "icons8_cancel1.png");
            this.imageCollection1.Images.SetKeyName(2, "Open2.png");
            // 
            // tpNoiDung
            // 
            this.tpNoiDung.Controls.Add(this.txtNoiDung);
            this.tpNoiDung.Name = "tpNoiDung";
            this.tpNoiDung.Padding = new System.Windows.Forms.Padding(1);
            this.tpNoiDung.Size = new System.Drawing.Size(641, 262);
            this.tpNoiDung.Text = "Nội dung mail";
            // 
            // txtNoiDung
            // 
            this.txtNoiDung.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtNoiDung.InnerText = null;
            this.txtNoiDung.Location = new System.Drawing.Point(1, 1);
            this.txtNoiDung.Name = "txtNoiDung";
            this.txtNoiDung.Size = new System.Drawing.Size(639, 260);
            this.txtNoiDung.TabIndex = 0;
            // 
            // btnChapNhan
            // 
            this.btnChapNhan.ImageOptions.ImageIndex = 0;
            this.btnChapNhan.ImageOptions.ImageList = this.imageCollection1;
            this.btnChapNhan.Location = new System.Drawing.Point(448, 386);
            this.btnChapNhan.Name = "btnChapNhan";
            this.btnChapNhan.Size = new System.Drawing.Size(100, 26);
            this.btnChapNhan.TabIndex = 2;
            this.btnChapNhan.Text = "Lưu và thoát";
            this.btnChapNhan.Click += new System.EventHandler(this.btnChapNhan_Click);
            // 
            // btnHuy
            // 
            this.btnHuy.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnHuy.ImageOptions.ImageIndex = 1;
            this.btnHuy.ImageOptions.ImageList = this.imageCollection1;
            this.btnHuy.Location = new System.Drawing.Point(554, 386);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(100, 26);
            this.btnHuy.TabIndex = 3;
            this.btnHuy.Text = "Hủy";
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(16, 41);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(52, 13);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "Ngày gửi:";
            // 
            // dateNgayGui
            // 
            this.dateNgayGui.EditValue = null;
            this.dateNgayGui.Location = new System.Drawing.Point(95, 38);
            this.dateNgayGui.Name = "dateNgayGui";
            this.dateNgayGui.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateNgayGui.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateNgayGui.Size = new System.Drawing.Size(559, 20);
            this.dateNgayGui.TabIndex = 1;
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Location = new System.Drawing.Point(16, 67);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(58, 13);
            this.labelControl3.TabIndex = 0;
            this.labelControl3.Text = "Tài khoản:";
            // 
            // lookMailFrom
            // 
            this.lookMailFrom.Location = new System.Drawing.Point(95, 64);
            this.lookMailFrom.Name = "lookMailFrom";
            this.lookMailFrom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookMailFrom.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("DiaChi", "Email", 50, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default)});
            this.lookMailFrom.Properties.DisplayFormat.FormatString = "d";
            this.lookMailFrom.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.lookMailFrom.Properties.DisplayMember = "DiaChi";
            this.lookMailFrom.Properties.EditFormat.FormatString = "d";
            this.lookMailFrom.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.lookMailFrom.Properties.NullText = "";
            this.lookMailFrom.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.lookMailFrom.Properties.ValueMember = "ID";
            this.lookMailFrom.Size = new System.Drawing.Size(559, 20);
            this.lookMailFrom.TabIndex = 2;
            // 
            // ckSelectAll
            // 
            this.ckSelectAll.Location = new System.Drawing.Point(13, 386);
            this.ckSelectAll.Name = "ckSelectAll";
            this.ckSelectAll.Properties.Caption = "Gửi cho tất cả khách hàng";
            this.ckSelectAll.Size = new System.Drawing.Size(154, 19);
            this.ckSelectAll.TabIndex = 4;
            this.ckSelectAll.CheckedChanged += new System.EventHandler(this.ckSelectAll_CheckedChanged);
            // 
            // itemHuongDan
            // 
            this.itemHuongDan.ImageOptions.ImageIndex = 2;
            this.itemHuongDan.ImageOptions.ImageList = this.imageCollection1;
            this.itemHuongDan.Location = new System.Drawing.Point(273, 389);
            this.itemHuongDan.Name = "itemHuongDan";
            this.itemHuongDan.Size = new System.Drawing.Size(89, 23);
            this.itemHuongDan.TabIndex = 5;
            this.itemHuongDan.Text = "Hướng dẫn";
            // 
            // frmSendMailEdit
            // 
            this.AcceptButton = this.btnChapNhan;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnHuy;
            this.ClientSize = new System.Drawing.Size(668, 422);
            this.Controls.Add(this.itemHuongDan);
            this.Controls.Add(this.ckSelectAll);
            this.Controls.Add(this.dateNgayGui);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnChapNhan);
            this.Controls.Add(this.xtraTabControl1);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.txtTieuDe);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.lookMailFrom);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSendMailEdit";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Email";
            this.Load += new System.EventHandler(this.frmSendMailEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtTieuDe.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.tpNguoiNhan.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcNguoiNhan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvNguoiNhan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookNguoiNhan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            this.tpNoiDung.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dateNgayGui.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateNgayGui.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookMailFrom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckSelectAll.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtTieuDe;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage tpNoiDung;
        private DevExpress.XtraTab.XtraTabPage tpNguoiNhan;
        private DevExpress.XtraGrid.GridControl gcNguoiNhan;
        private DevExpress.XtraGrid.Views.Grid.GridView grvNguoiNhan;
        private DevExpress.XtraEditors.SimpleButton btnChapNhan;
        private DevExpress.XtraEditors.SimpleButton btnHuy;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.DateEdit dateNgayGui;
        private DevExpress.Utils.ImageCollection imageCollection1;
        private MSDN.Html.Editor.HtmlEditorControl txtNoiDung;
        private DevExpress.XtraGrid.Columns.GridColumn colMaKhachHang;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lookNguoiNhan;
        private DevExpress.XtraGrid.Columns.GridColumn colTrangThai;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LookUpEdit lookMailFrom;
        private DevExpress.XtraEditors.CheckEdit ckSelectAll;
        private DevExpress.XtraEditors.SimpleButton itemHuongDan;
    }
}