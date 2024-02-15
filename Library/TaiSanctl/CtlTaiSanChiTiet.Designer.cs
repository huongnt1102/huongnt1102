namespace Library.TaiSanctl
{
    partial class CtlTaiSanChiTiet
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CtlTaiSanChiTiet));
            this.barManager1 = new DevExpress.XtraBars.BarManager();
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.btnThem = new DevExpress.XtraBars.BarButtonItem();
            this.btnXoa = new DevExpress.XtraBars.BarButtonItem();
            this.btnSave = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection();
            this.gcChiTietTS = new DevExpress.XtraGrid.GridControl();
            this.grvChiTietTS = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colKyHieu = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMaTS = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryNCC = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryHSX = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.dateNgaySX = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.spinHSD = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.spinDonGia = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryXuatXu = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryTrangThai = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.colTenChiTiet = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcChiTietTS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvChiTietTS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryNCC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryHSX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateNgaySX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateNgaySX.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinHSD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinDonGia)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryXuatXu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryTrangThai)).BeginInit();
            this.SuspendLayout();
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.HideBarsWhenMerging = false;
            this.barManager1.Images = this.imageCollection1;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.btnThem,
            this.btnXoa,
            this.btnSave});
            this.barManager1.MaxItemId = 3;
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnThem, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnXoa, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnSave, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.DrawDragBorder = false;
            this.bar1.OptionsBar.MultiLine = true;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Tools";
            // 
            // btnThem
            // 
            this.btnThem.Caption = "Thêm";
            this.btnThem.Id = 0;
            this.btnThem.ImageOptions.ImageIndex = 0;
            this.btnThem.Name = "btnThem";
            this.btnThem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnThem_ItemClick);
            // 
            // btnXoa
            // 
            this.btnXoa.Caption = "Xóa";
            this.btnXoa.Id = 1;
            this.btnXoa.ImageOptions.ImageIndex = 1;
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnXoa_ItemClick);
            // 
            // btnSave
            // 
            this.btnSave.Caption = "Lưu";
            this.btnSave.Id = 2;
            this.btnSave.ImageOptions.ImageIndex = 2;
            this.btnSave.Name = "btnSave";
            this.btnSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSave_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(717, 31);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 414);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(717, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 31);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 383);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(717, 31);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 383);
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.Images.SetKeyName(0, "Add1.png");  
            this.imageCollection1.Images.SetKeyName(1, "Delete1.png");  
            this.imageCollection1.Images.SetKeyName(2, "Save1.png");  
            // 
            // gcChiTietTS
            // 
            this.gcChiTietTS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcChiTietTS.Location = new System.Drawing.Point(0, 31);
            this.gcChiTietTS.MainView = this.grvChiTietTS;
            this.gcChiTietTS.MenuManager = this.barManager1;
            this.gcChiTietTS.Name = "gcChiTietTS";
            this.gcChiTietTS.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryNCC,
            this.repositoryHSX,
            this.repositoryTrangThai,
            this.repositoryXuatXu,
            this.dateNgaySX,
            this.spinHSD,
            this.spinDonGia});
            this.gcChiTietTS.ShowOnlyPredefinedDetails = true;
            this.gcChiTietTS.Size = new System.Drawing.Size(717, 383);
            this.gcChiTietTS.TabIndex = 4;
            this.gcChiTietTS.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvChiTietTS});
            // 
            // grvChiTietTS
            // 
            this.grvChiTietTS.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colKyHieu,
            this.colMaTS,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn6,
            this.gridColumn7,
            this.gridColumn8,
            this.gridColumn9,
            this.gridColumn10,
            this.gridColumn1,
            this.colTenChiTiet});
            this.grvChiTietTS.GridControl = this.gcChiTietTS;
            this.grvChiTietTS.Name = "grvChiTietTS";
            this.grvChiTietTS.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.True;
            this.grvChiTietTS.OptionsView.ColumnAutoWidth = false;
            this.grvChiTietTS.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;
            this.grvChiTietTS.InitNewRow += new DevExpress.XtraGrid.Views.Grid.InitNewRowEventHandler(this.grvChiTietTS_InitNewRow);
            // 
            // colKyHieu
            // 
            this.colKyHieu.Caption = "Ký hiệu";
            this.colKyHieu.FieldName = "KyHieu";
            this.colKyHieu.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.colKyHieu.Name = "colKyHieu";
            this.colKyHieu.Visible = true;
            this.colKyHieu.VisibleIndex = 0;
            this.colKyHieu.Width = 137;
            // 
            // colMaTS
            // 
            this.colMaTS.Caption = "MaTS";
            this.colMaTS.FieldName = "MaTS";
            this.colMaTS.Name = "colMaTS";
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Nhà cung cấp";
            this.gridColumn3.ColumnEdit = this.repositoryNCC;
            this.gridColumn3.FieldName = "MaNCC";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            this.gridColumn3.Width = 108;
            // 
            // repositoryNCC
            // 
            this.repositoryNCC.AutoHeight = false;
            this.repositoryNCC.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryNCC.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenVT", "Tên VT", 50, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenNCC", "Tên nhà cung cấp", 80, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default)});
            this.repositoryNCC.DisplayMember = "TenVT";
            this.repositoryNCC.Name = "repositoryNCC";
            this.repositoryNCC.NullText = "";
            this.repositoryNCC.ValueMember = "MaNCC";
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Hãng SX";
            this.gridColumn4.ColumnEdit = this.repositoryHSX;
            this.gridColumn4.FieldName = "MaHSX";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 3;
            this.gridColumn4.Width = 98;
            // 
            // repositoryHSX
            // 
            this.repositoryHSX.AutoHeight = false;
            this.repositoryHSX.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryHSX.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenHSX", "Tên HSX", 50, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default)});
            this.repositoryHSX.DisplayMember = "TenHSX";
            this.repositoryHSX.Name = "repositoryHSX";
            this.repositoryHSX.NullText = "";
            this.repositoryHSX.ValueMember = "MaHSX";
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Ngày SX";
            this.gridColumn5.ColumnEdit = this.dateNgaySX;
            this.gridColumn5.FieldName = "NgaySX";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 4;
            this.gridColumn5.Width = 108;
            // 
            // dateNgaySX
            // 
            this.dateNgaySX.AutoHeight = false;
            this.dateNgaySX.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateNgaySX.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateNgaySX.Name = "dateNgaySX";
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "Hạn sử dụng";
            this.gridColumn6.ColumnEdit = this.spinHSD;
            this.gridColumn6.FieldName = "HSD";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 6;
            this.gridColumn6.Width = 108;
            // 
            // spinHSD
            // 
            this.spinHSD.AutoHeight = false;
            this.spinHSD.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinHSD.DisplayFormat.FormatString = "{0:#,0.#} tháng";
            this.spinHSD.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinHSD.EditFormat.FormatString = "{0:#,0.#} tháng";
            this.spinHSD.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinHSD.Name = "spinHSD";
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "Ngày bắt đầu SD";
            this.gridColumn7.ColumnEdit = this.dateNgaySX;
            this.gridColumn7.FieldName = "NgayBDSD";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 7;
            this.gridColumn7.Width = 142;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "Đơn giá";
            this.gridColumn8.ColumnEdit = this.spinDonGia;
            this.gridColumn8.FieldName = "DonGia";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 8;
            this.gridColumn8.Width = 108;
            // 
            // spinDonGia
            // 
            this.spinDonGia.AutoHeight = false;
            this.spinDonGia.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinDonGia.DisplayFormat.FormatString = "{0:#,0.#} VNĐ";
            this.spinDonGia.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinDonGia.EditFormat.FormatString = "{0:#,0.#} VNĐ";
            this.spinDonGia.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinDonGia.Name = "spinDonGia";
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "Diễn giải";
            this.gridColumn9.FieldName = "DienGiai";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 10;
            this.gridColumn9.Width = 180;
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "Xuất xứ";
            this.gridColumn10.ColumnEdit = this.repositoryXuatXu;
            this.gridColumn10.FieldName = "MaXX";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 5;
            this.gridColumn10.Width = 112;
            // 
            // repositoryXuatXu
            // 
            this.repositoryXuatXu.AutoHeight = false;
            this.repositoryXuatXu.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryXuatXu.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenXX", "Xuất xứ", 50, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default)});
            this.repositoryXuatXu.DisplayMember = "TenXX";
            this.repositoryXuatXu.Name = "repositoryXuatXu";
            this.repositoryXuatXu.NullText = "";
            this.repositoryXuatXu.ValueMember = "MaXX";
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Trạng thái";
            this.gridColumn1.ColumnEdit = this.repositoryTrangThai;
            this.gridColumn1.FieldName = "MaTT";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 9;
            this.gridColumn1.Width = 136;
            // 
            // repositoryTrangThai
            // 
            this.repositoryTrangThai.AutoHeight = false;
            this.repositoryTrangThai.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryTrangThai.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenTT", "Trạng thái", 50, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default)});
            this.repositoryTrangThai.DisplayMember = "TenTT";
            this.repositoryTrangThai.Name = "repositoryTrangThai";
            this.repositoryTrangThai.NullText = "";
            this.repositoryTrangThai.ValueMember = "MaTT";
            // 
            // colTenChiTiet
            // 
            this.colTenChiTiet.Caption = "Tên chi tiết";
            this.colTenChiTiet.FieldName = "TenChiTiet";
            this.colTenChiTiet.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.colTenChiTiet.Name = "colTenChiTiet";
            this.colTenChiTiet.Visible = true;
            this.colTenChiTiet.VisibleIndex = 1;
            this.colTenChiTiet.Width = 154;
            // 
            // CtlTaiSanChiTiet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gcChiTietTS);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "CtlTaiSanChiTiet";
            this.Size = new System.Drawing.Size(717, 414);
            this.Load += new System.EventHandler(this.CtlTaiSanChiTiet_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcChiTietTS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvChiTietTS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryNCC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryHSX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateNgaySX.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateNgaySX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinHSD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinDonGia)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryXuatXu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryTrangThai)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraGrid.GridControl gcChiTietTS;
        private DevExpress.XtraGrid.Views.Grid.GridView grvChiTietTS;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryNCC;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryHSX;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryTrangThai;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryXuatXu;
        private DevExpress.XtraBars.BarButtonItem btnThem;
        private DevExpress.XtraBars.BarButtonItem btnXoa;
        private DevExpress.XtraBars.BarButtonItem btnSave;
        private DevExpress.XtraGrid.Columns.GridColumn colKyHieu;
        private DevExpress.XtraGrid.Columns.GridColumn colMaTS;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit dateNgaySX;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit spinHSD;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn colTenChiTiet;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit spinDonGia;
        private DevExpress.Utils.ImageCollection imageCollection1;
    }
}
