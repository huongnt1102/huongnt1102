namespace TaiSan
{
    partial class frmLoaiTaiSan
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLoaiTaiSan));
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.itemNap = new DevExpress.XtraBars.BarButtonItem();
            this.itemThem = new DevExpress.XtraBars.BarButtonItem();
            this.itemXoa = new DevExpress.XtraBars.BarButtonItem();
            this.itemLuu = new DevExpress.XtraBars.BarButtonItem();
            this.itemChiTietTS = new DevExpress.XtraBars.BarButtonItem();
            this.btnloai = new DevExpress.XtraBars.BarButtonItem();
            this.btndvt = new DevExpress.XtraBars.BarButtonItem();
            this.btnTiLeThue = new DevExpress.XtraBars.BarButtonItem();
            this.btnImportTaiSan = new DevExpress.XtraBars.BarButtonItem();
            this.itemExport = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
            this.itemThemCon = new DevExpress.XtraBars.BarButtonItem();
            this.gcTaiSan = new DevExpress.XtraGrid.GridControl();
            this.grvTaiSan = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colKyHieu = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTenTaiSan = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTiLeKhauHao = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLoai = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lookLoai = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.colDVT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lookDVT = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.colTiLeThue = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lookThue = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.colDacTinh = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemMemoExEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit();
            this.colTN = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lookTN = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lookLTSCha = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.repositoryItemMemoExEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcTaiSan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvTaiSan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookLoai)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookDVT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookThue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoExEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookTN)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookLTSCha)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoExEdit2)).BeginInit();
            this.SuspendLayout();
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
            this.itemThemCon,
            this.itemChiTietTS,
            this.btnloai,
            this.btndvt,
            this.btnTiLeThue,
            this.btnImportTaiSan,
            this.itemExport});
            this.barManager1.MaxItemId = 13;
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemNap, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemThem, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemXoa, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemLuu, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemChiTietTS, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnloai, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btndvt, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnTiLeThue, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnImportTaiSan, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemExport, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.DrawDragBorder = false;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Tools";
            // 
            // itemNap
            // 
            this.itemNap.Caption = "Nạp";
            this.itemNap.Id = 3;
            this.itemNap.ImageIndex = 3;
            this.itemNap.Name = "itemNap";
            this.itemNap.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemNap_ItemClick);
            // 
            // itemThem
            // 
            this.itemThem.Caption = "Thêm";
            this.itemThem.Id = 4;
            this.itemThem.ImageIndex = 0;
            this.itemThem.Name = "itemThem";
            this.itemThem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemThem_ItemClick);
            // 
            // itemXoa
            // 
            this.itemXoa.Caption = "Xóa";
            this.itemXoa.Id = 5;
            this.itemXoa.ImageIndex = 1;
            this.itemXoa.Name = "itemXoa";
            this.itemXoa.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemXoa_ItemClick);
            // 
            // itemLuu
            // 
            this.itemLuu.Caption = "Lưu";
            this.itemLuu.Id = 2;
            this.itemLuu.ImageIndex = 2;
            this.itemLuu.Name = "itemLuu";
            this.itemLuu.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemLuu_ItemClick);
            // 
            // itemChiTietTS
            // 
            this.itemChiTietTS.Caption = "Chi tiết";
            this.itemChiTietTS.Id = 7;
            this.itemChiTietTS.ImageIndex = 4;
            this.itemChiTietTS.Name = "itemChiTietTS";
            this.itemChiTietTS.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemChiTietTS_ItemClick);
            // 
            // btnloai
            // 
            this.btnloai.Caption = "Loại tài sản";
            this.btnloai.Id = 8;
            this.btnloai.ImageIndex = 5;
            this.btnloai.Name = "btnloai";
            this.btnloai.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnloai_ItemClick);
            // 
            // btndvt
            // 
            this.btndvt.Caption = "Đơn vị tính";
            this.btndvt.Id = 9;
            this.btndvt.ImageIndex = 5;
            this.btndvt.Name = "btndvt";
            this.btndvt.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btndvt_ItemClick);
            // 
            // btnTiLeThue
            // 
            this.btnTiLeThue.Caption = "Tỉ lệ thuế";
            this.btnTiLeThue.Id = 10;
            this.btnTiLeThue.ImageIndex = 5;
            this.btnTiLeThue.Name = "btnTiLeThue";
            this.btnTiLeThue.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnTiLeThue_ItemClick);
            // 
            // btnImportTaiSan
            // 
            this.btnImportTaiSan.Caption = "Import";
            this.btnImportTaiSan.Id = 11;
            this.btnImportTaiSan.ImageIndex = 9;
            this.btnImportTaiSan.Name = "btnImportTaiSan";
            this.btnImportTaiSan.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnImportTaiSan_ItemClick);
            // 
            // itemExport
            // 
            this.itemExport.Caption = "Export";
            this.itemExport.Id = 12;
            this.itemExport.ImageIndex = 8;
            this.itemExport.Name = "itemExport";
            this.itemExport.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemExport_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(1092, 31);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 526);
            this.barDockControlBottom.Size = new System.Drawing.Size(1092, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 31);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 495);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1092, 31);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 495);
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.Images.SetKeyName(0, "add_outline.png");
            this.imageCollection1.Images.SetKeyName(1, "edit-delete.png");
            this.imageCollection1.Images.SetKeyName(2, "save_16x16.gif");
            this.imageCollection1.Images.SetKeyName(3, "108.gif");
            this.imageCollection1.Images.SetKeyName(4, "1337746822_control-center2 [].png");
            this.imageCollection1.Images.SetKeyName(5, "edit.png");
            this.imageCollection1.Images.SetKeyName(6, "excel16.png");
            this.imageCollection1.Images.SetKeyName(7, "edit.png");
            this.imageCollection1.Images.SetKeyName(8, "table-export.png");
            this.imageCollection1.Images.SetKeyName(9, "table-import.png");
            // 
            // itemThemCon
            // 
            this.itemThemCon.Caption = "Thêm cấp";
            this.itemThemCon.Id = 6;
            this.itemThemCon.ImageIndex = 0;
            this.itemThemCon.Name = "itemThemCon";
            // 
            // gcTaiSan
            // 
            this.gcTaiSan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcTaiSan.Location = new System.Drawing.Point(0, 31);
            this.gcTaiSan.MainView = this.grvTaiSan;
            this.gcTaiSan.MenuManager = this.barManager1;
            this.gcTaiSan.Name = "gcTaiSan";
            this.gcTaiSan.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.lookLoai,
            this.lookDVT,
            this.lookThue,
            this.repositoryItemMemoExEdit1,
            this.repositoryItemMemoExEdit2,
            this.lookTN,
            this.lookLTSCha});
            this.gcTaiSan.ShowOnlyPredefinedDetails = true;
            this.gcTaiSan.Size = new System.Drawing.Size(1092, 495);
            this.gcTaiSan.TabIndex = 10;
            this.gcTaiSan.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvTaiSan});
            // 
            // grvTaiSan
            // 
            this.grvTaiSan.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colKyHieu,
            this.colTenTaiSan,
            this.colTiLeKhauHao,
            this.colLoai,
            this.colDVT,
            this.colTiLeThue,
            this.colDacTinh,
            this.colTN,
            this.gridColumn1,
            this.gridColumn2});
            this.grvTaiSan.GridControl = this.gcTaiSan;
            this.grvTaiSan.Name = "grvTaiSan";
            this.grvTaiSan.OptionsPrint.AutoWidth = false;
            this.grvTaiSan.OptionsView.ColumnAutoWidth = false;
            this.grvTaiSan.OptionsView.ShowAutoFilterRow = true;
            this.grvTaiSan.CellValueChanging += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.grvTaiSan_CellValueChanging);
            // 
            // colKyHieu
            // 
            this.colKyHieu.Caption = "Ký hiệu";
            this.colKyHieu.FieldName = "KyHieu";
            this.colKyHieu.Name = "colKyHieu";
            this.colKyHieu.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.colKyHieu.Visible = true;
            this.colKyHieu.VisibleIndex = 2;
            this.colKyHieu.Width = 98;
            // 
            // colTenTaiSan
            // 
            this.colTenTaiSan.Caption = "Tên tài sản";
            this.colTenTaiSan.FieldName = "TenLTS";
            this.colTenTaiSan.Name = "colTenTaiSan";
            this.colTenTaiSan.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.colTenTaiSan.Visible = true;
            this.colTenTaiSan.VisibleIndex = 3;
            this.colTenTaiSan.Width = 208;
            // 
            // colTiLeKhauHao
            // 
            this.colTiLeKhauHao.Caption = "Tỉ lệ khấu hao / tháng";
            this.colTiLeKhauHao.FieldName = "TiLeKhauHao";
            this.colTiLeKhauHao.Name = "colTiLeKhauHao";
            this.colTiLeKhauHao.Width = 121;
            // 
            // colLoai
            // 
            this.colLoai.Caption = "Nhóm tài sản";
            this.colLoai.ColumnEdit = this.lookLoai;
            this.colLoai.FieldName = "TypeID";
            this.colLoai.Name = "colLoai";
            this.colLoai.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.colLoai.Width = 121;
            // 
            // lookLoai
            // 
            this.lookLoai.AutoHeight = false;
            this.lookLoai.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookLoai.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TypeNam", 50, "Loại")});
            this.lookLoai.DisplayMember = "TypeNam";
            this.lookLoai.Name = "lookLoai";
            this.lookLoai.NullText = "";
            this.lookLoai.ValueMember = "TypeID";
            // 
            // colDVT
            // 
            this.colDVT.Caption = "Đơn vị tính";
            this.colDVT.ColumnEdit = this.lookDVT;
            this.colDVT.FieldName = "MaDVT";
            this.colDVT.Name = "colDVT";
            this.colDVT.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.colDVT.Visible = true;
            this.colDVT.VisibleIndex = 4;
            // 
            // lookDVT
            // 
            this.lookDVT.AutoHeight = false;
            this.lookDVT.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookDVT.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenDVT", 50, "Đơn vị tính")});
            this.lookDVT.DisplayMember = "TenDVT";
            this.lookDVT.Name = "lookDVT";
            this.lookDVT.NullText = "";
            this.lookDVT.ValueMember = "MaDVT";
            // 
            // colTiLeThue
            // 
            this.colTiLeThue.Caption = "Tỉ lệ thuế";
            this.colTiLeThue.ColumnEdit = this.lookThue;
            this.colTiLeThue.FieldName = "ThueID";
            this.colTiLeThue.Name = "colTiLeThue";
            this.colTiLeThue.Width = 60;
            // 
            // lookThue
            // 
            this.lookThue.AutoHeight = false;
            this.lookThue.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookThue.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TiLe", "Tỉ lệ thuế")});
            this.lookThue.DisplayMember = "TiLe";
            this.lookThue.Name = "lookThue";
            this.lookThue.NullText = "";
            this.lookThue.ValueMember = "ThueID";
            // 
            // colDacTinh
            // 
            this.colDacTinh.Caption = "Đặc tính kỹ thuật";
            this.colDacTinh.ColumnEdit = this.repositoryItemMemoExEdit1;
            this.colDacTinh.FieldName = "DacTinh";
            this.colDacTinh.Name = "colDacTinh";
            this.colDacTinh.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.colDacTinh.Visible = true;
            this.colDacTinh.VisibleIndex = 5;
            this.colDacTinh.Width = 557;
            // 
            // repositoryItemMemoExEdit1
            // 
            this.repositoryItemMemoExEdit1.AutoHeight = false;
            this.repositoryItemMemoExEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemMemoExEdit1.Name = "repositoryItemMemoExEdit1";
            this.repositoryItemMemoExEdit1.PopupFormMinSize = new System.Drawing.Size(400, 150);
            this.repositoryItemMemoExEdit1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.repositoryItemMemoExEdit1.ShowIcon = false;
            // 
            // colTN
            // 
            this.colTN.Caption = "MaTN";
            this.colTN.FieldName = "MaTN";
            this.colTN.Name = "colTN";
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Dự án";
            this.gridColumn1.ColumnEdit = this.lookTN;
            this.gridColumn1.FieldName = "MaTN";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 118;
            // 
            // lookTN
            // 
            this.lookTN.AutoHeight = false;
            this.lookTN.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookTN.DisplayMember = "TenTN";
            this.lookTN.Name = "lookTN";
            this.lookTN.NullText = "";
            this.lookTN.ValueMember = "MaTN";
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Loại tài sản cấp trên";
            this.gridColumn2.ColumnEdit = this.lookLTSCha;
            this.gridColumn2.FieldName = "MaLTSCHA";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 163;
            // 
            // lookLTSCha
            // 
            this.lookLTSCha.AutoHeight = false;
            this.lookLTSCha.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookLTSCha.DisplayMember = "TenLTS";
            this.lookLTSCha.Name = "lookLTSCha";
            this.lookLTSCha.NullText = "";
            this.lookLTSCha.ValueMember = "MaLTS";
            // 
            // repositoryItemMemoExEdit2
            // 
            this.repositoryItemMemoExEdit2.AutoHeight = false;
            this.repositoryItemMemoExEdit2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemMemoExEdit2.Name = "repositoryItemMemoExEdit2";
            // 
            // frmLoaiTaiSan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1092, 526);
            this.Controls.Add(this.gcTaiSan);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "frmLoaiTaiSan";
            this.Text = "Loại tài sản";
            this.Load += new System.EventHandler(this.frmLoaiMatBang_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcTaiSan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvTaiSan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookLoai)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookDVT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookThue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoExEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookTN)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookLTSCha)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoExEdit2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

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
        private DevExpress.XtraBars.BarButtonItem itemThemCon;
        private DevExpress.XtraBars.BarButtonItem itemChiTietTS;
        private DevExpress.XtraBars.BarButtonItem btnloai;
        private DevExpress.XtraBars.BarButtonItem btndvt;
        private DevExpress.XtraBars.BarButtonItem btnTiLeThue;
        private DevExpress.XtraBars.BarButtonItem btnImportTaiSan;
        private DevExpress.XtraGrid.GridControl gcTaiSan;
        private DevExpress.XtraGrid.Views.Grid.GridView grvTaiSan;
        private DevExpress.XtraGrid.Columns.GridColumn colKyHieu;
        private DevExpress.XtraGrid.Columns.GridColumn colTenTaiSan;
        private DevExpress.XtraGrid.Columns.GridColumn colTiLeKhauHao;
        private DevExpress.XtraGrid.Columns.GridColumn colLoai;
        private DevExpress.XtraGrid.Columns.GridColumn colDVT;
        private DevExpress.XtraGrid.Columns.GridColumn colTiLeThue;
        private DevExpress.XtraGrid.Columns.GridColumn colDacTinh;
        private DevExpress.XtraGrid.Columns.GridColumn colTN;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lookLoai;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lookDVT;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lookThue;
        private DevExpress.XtraBars.BarButtonItem itemExport;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit repositoryItemMemoExEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lookTN;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit repositoryItemMemoExEdit2;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lookLTSCha;
    }
}