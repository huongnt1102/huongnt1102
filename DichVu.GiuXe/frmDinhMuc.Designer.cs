namespace DichVu.GiuXe
{
    partial class frmDinhMuc
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDinhMuc));
            this.gcDinhMuc = new DevExpress.XtraGrid.GridControl();
            this.gvDinhMuc = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colTenDM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDinhMuc = new DevExpress.XtraGrid.Columns.GridColumn();
            this.spDinhMuc = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.colDonGiaCH = new DevExpress.XtraGrid.Columns.GridColumn();
            this.spDonGiaCH = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.colDonGiaKD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.spDonGiaKD = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.colDienGiai = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lkLoaiTien = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.spSTT = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.itemToaNha = new DevExpress.XtraBars.BarEditItem();
            this.lkToaNha = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.itemLoaiXe = new DevExpress.XtraBars.BarEditItem();
            this.lkLoaiXe = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.itemLoaiMatBang = new DevExpress.XtraBars.BarEditItem();
            this.lkLoaiMatBang = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.itemMatBang = new DevExpress.XtraBars.BarEditItem();
            this.glkMatBang = new DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit();
            this.gvMatBang = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.itemNap = new DevExpress.XtraBars.BarButtonItem();
            this.itemThem = new DevExpress.XtraBars.BarButtonItem();
            this.itemSua = new DevExpress.XtraBars.BarButtonItem();
            this.itemXoa = new DevExpress.XtraBars.BarButtonItem();
            this.itemLuu = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
            this.glkKhachHang = new DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gcDinhMuc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDinhMuc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spDinhMuc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spDonGiaCH)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spDonGiaKD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkLoaiTien)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spSTT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkToaNha)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkLoaiXe)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkLoaiMatBang)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkMatBang)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMatBang)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkKhachHang)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // gcDinhMuc
            // 
            this.gcDinhMuc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcDinhMuc.Location = new System.Drawing.Point(0, 69);
            this.gcDinhMuc.MainView = this.gvDinhMuc;
            this.gcDinhMuc.Name = "gcDinhMuc";
            this.gcDinhMuc.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.lkLoaiTien,
            this.spDinhMuc,
            this.spSTT,
            this.spDonGiaCH,
            this.spDonGiaKD});
            this.gcDinhMuc.ShowOnlyPredefinedDetails = true;
            this.gcDinhMuc.Size = new System.Drawing.Size(934, 428);
            this.gcDinhMuc.TabIndex = 0;
            this.gcDinhMuc.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvDinhMuc});
            this.gcDinhMuc.KeyUp += new System.Windows.Forms.KeyEventHandler(this.gcDinhMucNuoc_KeyUp);
            // 
            // gvDinhMuc
            // 
            this.gvDinhMuc.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colTenDM,
            this.colDinhMuc,
            this.colDonGiaCH,
            this.colDonGiaKD,
            this.colDienGiai,
            this.gridColumn1,
            this.gridColumn3,
            this.gridColumn2,
            this.gridColumn9,
            this.gridColumn10,
            this.gridColumn5,
            this.gridColumn6,
            this.gridColumn11});
            this.gvDinhMuc.GridControl = this.gcDinhMuc;
            this.gvDinhMuc.GroupCount = 1;
            this.gvDinhMuc.Name = "gvDinhMuc";
            this.gvDinhMuc.OptionsBehavior.AutoExpandAllGroups = true;
            this.gvDinhMuc.OptionsBehavior.ReadOnly = true;
            this.gvDinhMuc.OptionsSelection.MultiSelect = true;
            this.gvDinhMuc.OptionsView.ColumnAutoWidth = false;
            this.gvDinhMuc.OptionsView.ShowAutoFilterRow = true;
            this.gvDinhMuc.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.gridColumn5, DevExpress.Data.ColumnSortOrder.Ascending),
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colDinhMuc, DevExpress.Data.ColumnSortOrder.Ascending),
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colDonGiaCH, DevExpress.Data.ColumnSortOrder.Ascending),
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colDonGiaKD, DevExpress.Data.ColumnSortOrder.Ascending),
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.gridColumn6, DevExpress.Data.ColumnSortOrder.Ascending),
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.gridColumn11, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.gvDinhMuc.InitNewRow += new DevExpress.XtraGrid.Views.Grid.InitNewRowEventHandler(this.gvDinhMucNuoc_InitNewRow);
            this.gvDinhMuc.ValidateRow += new DevExpress.XtraGrid.Views.Base.ValidateRowEventHandler(this.gvDinhMucNuoc_ValidateRow);
            // 
            // colTenDM
            // 
            this.colTenDM.Caption = "Tên Định Mức";
            this.colTenDM.FieldName = "TenDM";
            this.colTenDM.Name = "colTenDM";
            this.colTenDM.Visible = true;
            this.colTenDM.VisibleIndex = 0;
            this.colTenDM.Width = 150;
            // 
            // colDinhMuc
            // 
            this.colDinhMuc.Caption = "Định Mức";
            this.colDinhMuc.ColumnEdit = this.spDinhMuc;
            this.colDinhMuc.DisplayFormat.FormatString = "{0:#,0.####}";
            this.colDinhMuc.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colDinhMuc.FieldName = "SoLuong";
            this.colDinhMuc.Name = "colDinhMuc";
            this.colDinhMuc.Visible = true;
            this.colDinhMuc.VisibleIndex = 1;
            this.colDinhMuc.Width = 77;
            // 
            // spDinhMuc
            // 
            this.spDinhMuc.AutoHeight = false;
            this.spDinhMuc.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spDinhMuc.IsFloatValue = false;
            this.spDinhMuc.Mask.EditMask = "N00";
            this.spDinhMuc.Name = "spDinhMuc";
            // 
            // colDonGiaCH
            // 
            this.colDonGiaCH.Caption = "Giá tháng";
            this.colDonGiaCH.ColumnEdit = this.spDonGiaCH;
            this.colDonGiaCH.DisplayFormat.FormatString = "{0:#,0.####}";
            this.colDonGiaCH.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colDonGiaCH.FieldName = "GiaThang";
            this.colDonGiaCH.Name = "colDonGiaCH";
            this.colDonGiaCH.Visible = true;
            this.colDonGiaCH.VisibleIndex = 2;
            this.colDonGiaCH.Width = 84;
            // 
            // spDonGiaCH
            // 
            this.spDonGiaCH.AutoHeight = false;
            this.spDonGiaCH.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spDonGiaCH.Name = "spDonGiaCH";
            // 
            // colDonGiaKD
            // 
            this.colDonGiaKD.Caption = "Giá ngày";
            this.colDonGiaKD.ColumnEdit = this.spDonGiaKD;
            this.colDonGiaKD.DisplayFormat.FormatString = "{0:#,0.####}";
            this.colDonGiaKD.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colDonGiaKD.FieldName = "GiaNgay";
            this.colDonGiaKD.Name = "colDonGiaKD";
            this.colDonGiaKD.Visible = true;
            this.colDonGiaKD.VisibleIndex = 3;
            this.colDonGiaKD.Width = 89;
            // 
            // spDonGiaKD
            // 
            this.spDonGiaKD.AutoHeight = false;
            this.spDonGiaKD.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spDonGiaKD.Name = "spDonGiaKD";
            // 
            // colDienGiai
            // 
            this.colDienGiai.Caption = "Diễn Giải";
            this.colDienGiai.FieldName = "DienGiai";
            this.colDienGiai.Name = "colDienGiai";
            this.colDienGiai.Width = 350;
            // 
            // gridColumn1
            // 
            this.gridColumn1.FieldName = "MaTN";
            this.gridColumn1.Name = "gridColumn1";
            // 
            // gridColumn3
            // 
            this.gridColumn3.FieldName = "MaLX";
            this.gridColumn3.Name = "gridColumn3";
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "MaLMB";
            this.gridColumn2.FieldName = "MaLMB";
            this.gridColumn2.Name = "gridColumn2";
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "MaMB";
            this.gridColumn9.FieldName = "MaMB";
            this.gridColumn9.Name = "gridColumn9";
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "MaKH";
            this.gridColumn10.FieldName = "MaKH";
            this.gridColumn10.Name = "gridColumn10";
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Loại xe";
            this.gridColumn5.FieldName = "TenLX";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 5;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "Loại mặt bằng";
            this.gridColumn6.FieldName = "TenLMB";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 4;
            this.gridColumn6.Width = 165;
            // 
            // gridColumn11
            // 
            this.gridColumn11.Caption = "Mặt bằng";
            this.gridColumn11.FieldName = "MaSoMB";
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.Visible = true;
            this.gridColumn11.VisibleIndex = 5;
            this.gridColumn11.Width = 117;
            // 
            // lkLoaiTien
            // 
            this.lkLoaiTien.AutoHeight = false;
            this.lkLoaiTien.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkLoaiTien.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("KyHieuLT", "Name7")});
            this.lkLoaiTien.DisplayMember = "KyHieuLT";
            this.lkLoaiTien.Name = "lkLoaiTien";
            this.lkLoaiTien.NullText = "";
            this.lkLoaiTien.ShowHeader = false;
            this.lkLoaiTien.ValueMember = "ID";
            // 
            // spSTT
            // 
            this.spSTT.AutoHeight = false;
            this.spSTT.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spSTT.Name = "spSTT";
            // 
            // barManager1
            // 
            this.barManager1.AllowCustomization = false;
            this.barManager1.AllowMoveBarOnToolbar = false;
            this.barManager1.AllowQuickCustomization = false;
            this.barManager1.AllowShowToolbarsPopup = false;
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
            this.itemLuu,
            this.itemNap,
            this.itemThem,
            this.itemXoa,
            this.itemToaNha,
            this.itemLoaiXe,
            this.itemLoaiMatBang,
            this.itemMatBang,
            this.itemSua});
            this.barManager1.MaxItemId = 15;
            this.barManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.lkToaNha,
            this.lkLoaiXe,
            this.lkLoaiMatBang,
            this.glkMatBang,
            this.glkKhachHang});
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemToaNha, DevExpress.XtraBars.BarItemPaintStyle.Caption),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemLoaiXe, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemLoaiMatBang, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemMatBang, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemNap, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemThem, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemSua, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemXoa, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemLuu, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.DrawDragBorder = false;
            this.bar1.OptionsBar.MultiLine = true;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Tools";
            // 
            // itemToaNha
            // 
            this.itemToaNha.Caption = "Dự án";
            this.itemToaNha.Edit = this.lkToaNha;
            this.itemToaNha.EditWidth = 120;
            this.itemToaNha.Id = 9;
            this.itemToaNha.Name = "itemToaNha";
            this.itemToaNha.EditValueChanged += new System.EventHandler(this.itemToaNha_EditValueChanged);
            // 
            // lkToaNha
            // 
            this.lkToaNha.AutoHeight = false;
            this.lkToaNha.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkToaNha.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenTN", "Name1")});
            this.lkToaNha.DisplayMember = "TenTN";
            this.lkToaNha.Name = "lkToaNha";
            this.lkToaNha.NullText = "";
            this.lkToaNha.ShowHeader = false;
            this.lkToaNha.ValueMember = "MaTN";
            // 
            // itemLoaiXe
            // 
            this.itemLoaiXe.Caption = "Loại xe";
            this.itemLoaiXe.Edit = this.lkLoaiXe;
            this.itemLoaiXe.EditWidth = 100;
            this.itemLoaiXe.Id = 10;
            this.itemLoaiXe.Name = "itemLoaiXe";
            this.itemLoaiXe.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            // 
            // lkLoaiXe
            // 
            this.lkLoaiXe.AutoHeight = false;
            this.lkLoaiXe.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkLoaiXe.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenLX", "Name2")});
            this.lkLoaiXe.DisplayMember = "TenLX";
            this.lkLoaiXe.Name = "lkLoaiXe";
            this.lkLoaiXe.NullText = "";
            this.lkLoaiXe.ShowHeader = false;
            this.lkLoaiXe.ValueMember = "MaLX";
            // 
            // itemLoaiMatBang
            // 
            this.itemLoaiMatBang.Caption = "Loại mặt bằng";
            this.itemLoaiMatBang.Edit = this.lkLoaiMatBang;
            this.itemLoaiMatBang.EditWidth = 100;
            this.itemLoaiMatBang.Id = 11;
            this.itemLoaiMatBang.Name = "itemLoaiMatBang";
            this.itemLoaiMatBang.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            // 
            // lkLoaiMatBang
            // 
            this.lkLoaiMatBang.AutoHeight = false;
            this.lkLoaiMatBang.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
            this.lkLoaiMatBang.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenLMB", "Name1")});
            this.lkLoaiMatBang.DisplayMember = "TenLMB";
            this.lkLoaiMatBang.Name = "lkLoaiMatBang";
            this.lkLoaiMatBang.NullText = "";
            this.lkLoaiMatBang.ShowHeader = false;
            this.lkLoaiMatBang.ValueMember = "MaLMB";
            this.lkLoaiMatBang.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.lkLoaiMatBang_ButtonClick);
            // 
            // itemMatBang
            // 
            this.itemMatBang.Caption = "Mặt bằng";
            this.itemMatBang.Edit = this.glkMatBang;
            this.itemMatBang.EditWidth = 100;
            this.itemMatBang.Id = 12;
            this.itemMatBang.Name = "itemMatBang";
            this.itemMatBang.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            // 
            // glkMatBang
            // 
            this.glkMatBang.AutoHeight = false;
            this.glkMatBang.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
            this.glkMatBang.DisplayMember = "MaSoMB";
            this.glkMatBang.Name = "glkMatBang";
            this.glkMatBang.NullText = "";
            this.glkMatBang.PopupView = this.gvMatBang;
            this.glkMatBang.ValueMember = "MaMB";
            this.glkMatBang.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.glkMatBang_ButtonClick);
            this.glkMatBang.EditValueChanged += new System.EventHandler(this.glkMatBang_EditValueChanged);
            // 
            // gvMatBang
            // 
            this.gvMatBang.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn4});
            this.gvMatBang.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gvMatBang.Name = "gvMatBang";
            this.gvMatBang.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvMatBang.OptionsView.ShowAutoFilterRow = true;
            this.gvMatBang.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Mã mặt bằng";
            this.gridColumn4.FieldName = "MaSoMB";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            this.gridColumn4.OptionsColumn.ReadOnly = true;
            this.gridColumn4.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 0;
            // 
            // itemNap
            // 
            this.itemNap.Caption = "Nạp";
            this.itemNap.Id = 3;
            this.itemNap.ImageOptions.ImageIndex = 0;
            this.itemNap.Name = "itemNap";
            this.itemNap.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemNap_ItemClick);
            // 
            // itemThem
            // 
            this.itemThem.Caption = "Thêm";
            this.itemThem.Id = 4;
            this.itemThem.ImageOptions.ImageIndex = 1;
            this.itemThem.Name = "itemThem";
            this.itemThem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemThem_ItemClick);
            // 
            // itemSua
            // 
            this.itemSua.Caption = "Sửa";
            this.itemSua.Id = 14;
            this.itemSua.ImageOptions.ImageIndex = 4;
            this.itemSua.Name = "itemSua";
            this.itemSua.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemSua_ItemClick);
            // 
            // itemXoa
            // 
            this.itemXoa.Caption = "Xóa";
            this.itemXoa.Id = 5;
            this.itemXoa.ImageOptions.ImageIndex = 2;
            this.itemXoa.Name = "itemXoa";
            this.itemXoa.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemXoa_ItemClick);
            // 
            // itemLuu
            // 
            this.itemLuu.Caption = "Lưu";
            this.itemLuu.Id = 2;
            this.itemLuu.ImageOptions.ImageIndex = 3;
            this.itemLuu.Name = "itemLuu";
            this.itemLuu.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemLuu_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(934, 69);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 497);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(934, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 69);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 428);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(934, 69);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 428);
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.Images.SetKeyName(0, "icons8_refresh1.png");
            this.imageCollection1.Images.SetKeyName(1, "icons8_add4.png");
            this.imageCollection1.Images.SetKeyName(2, "icons8_delete1.png");
            this.imageCollection1.Images.SetKeyName(3, "icons8_save.png");
            this.imageCollection1.Images.SetKeyName(4, "Edit3.png");
            // 
            // glkKhachHang
            // 
            this.glkKhachHang.AutoHeight = false;
            this.glkKhachHang.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
            this.glkKhachHang.DisplayMember = "TenKH";
            this.glkKhachHang.Name = "glkKhachHang";
            this.glkKhachHang.NullText = "";
            this.glkKhachHang.PopupView = this.gridView2;
            this.glkKhachHang.ValueMember = "MaKH";
            this.glkKhachHang.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.glkKhachHang_ButtonClick);
            // 
            // gridView2
            // 
            this.gridView2.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn7,
            this.gridColumn8});
            this.gridView2.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView2.OptionsView.ShowAutoFilterRow = true;
            this.gridView2.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "Mã khách hàng";
            this.gridColumn7.FieldName = "KyHieu";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.OptionsColumn.AllowEdit = false;
            this.gridColumn7.OptionsColumn.ReadOnly = true;
            this.gridColumn7.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 0;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "Tên khách hàng";
            this.gridColumn8.FieldName = "TenKH";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.OptionsColumn.AllowEdit = false;
            this.gridColumn8.OptionsColumn.ReadOnly = true;
            this.gridColumn8.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 1;
            // 
            // frmDinhMuc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(934, 497);
            this.Controls.Add(this.gcDinhMuc);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDinhMuc";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bảng giá giữ xe";
            this.Load += new System.EventHandler(this.frmDinhMuc_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gcDinhMuc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDinhMuc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spDinhMuc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spDonGiaCH)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spDonGiaKD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkLoaiTien)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spSTT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkToaNha)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkLoaiXe)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkLoaiMatBang)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkMatBang)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMatBang)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkKhachHang)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gcDinhMuc;
        private DevExpress.XtraGrid.Views.Grid.GridView gvDinhMuc;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem itemLuu;
        private DevExpress.XtraBars.BarButtonItem itemNap;
        private DevExpress.Utils.ImageCollection imageCollection1;
        private DevExpress.XtraBars.BarButtonItem itemThem;
        private DevExpress.XtraBars.BarButtonItem itemXoa;
        private DevExpress.XtraGrid.Columns.GridColumn colTenDM;
        private DevExpress.XtraGrid.Columns.GridColumn colDinhMuc;
        private DevExpress.XtraGrid.Columns.GridColumn colDonGiaCH;
        private DevExpress.XtraGrid.Columns.GridColumn colDienGiai;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lkLoaiTien;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit spDinhMuc;
        private DevExpress.XtraBars.BarEditItem itemToaNha;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lkToaNha;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit spSTT;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit spDonGiaCH;
        private DevExpress.XtraGrid.Columns.GridColumn colDonGiaKD;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit spDonGiaKD;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraBars.BarEditItem itemLoaiMatBang;
        private DevExpress.XtraBars.BarEditItem itemLoaiXe;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lkLoaiXe;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lkLoaiMatBang;
        private DevExpress.XtraBars.BarEditItem itemMatBang;
        private DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit glkMatBang;
        private DevExpress.XtraGrid.Views.Grid.GridView gvMatBang;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit glkKhachHang;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private DevExpress.XtraBars.BarButtonItem itemSua;
    }
}