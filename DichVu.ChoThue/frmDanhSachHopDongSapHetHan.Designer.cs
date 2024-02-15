namespace DichVu.ChoThue
{
    partial class frmDanhSachHopDongSapHetHan
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
            DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lookTT;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDanhSachHopDongSapHetHan));
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.itemKyBC = new DevExpress.XtraBars.BarEditItem();
            this.cmbKyBC = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.itemTuNgay = new DevExpress.XtraBars.BarEditItem();
            this.dateTuNgay = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.itemDenNgay = new DevExpress.XtraBars.BarEditItem();
            this.dateDenNgay = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.itemNap = new DevExpress.XtraBars.BarButtonItem();
            this.barSubItem1 = new DevExpress.XtraBars.BarSubItem();
            this.btnInCongNo = new DevExpress.XtraBars.BarButtonItem();
            this.btnThahToan = new DevExpress.XtraBars.BarButtonItem();
            this.itemExport = new DevExpress.XtraBars.BarSubItem();
            this.btn2Excel = new DevExpress.XtraBars.BarButtonItem();
            this.btn2hdt = new DevExpress.XtraBars.BarButtonItem();
            this.itemClose = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
            this.btnInHopDong = new DevExpress.XtraBars.BarButtonItem();
            this.itemChuyenQuyenSuDung = new DevExpress.XtraBars.BarButtonItem();
            this.btnDaDuyet = new DevExpress.XtraBars.BarButtonItem();
            this.btnThemTaiLieu = new DevExpress.XtraBars.BarButtonItem();
            this.btnXoaTaiLieu = new DevExpress.XtraBars.BarButtonItem();
            this.btnTaiTaiLieu = new DevExpress.XtraBars.BarButtonItem();
            this.gcHopDong = new DevExpress.XtraGrid.GridControl();
            this.grvHopDong = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colSoHopDong = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colGiaThue = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPhiQuanLy = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTienCoc = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn22 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn14 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn15 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn34 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMaTG = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTimeRemain = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            lookTT = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            ((System.ComponentModel.ISupportInitialize)(lookTT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbKyBC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTuNgay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTuNgay.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDenNgay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDenNgay.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcHopDong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvHopDong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // lookTT
            // 
            lookTT.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            lookTT.Name = "lookTT";
            lookTT.NullText = "Chọn trạng thái của hợp đồng";
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
            this.itemNap,
            this.itemKyBC,
            this.itemTuNgay,
            this.itemDenNgay,
            this.btnInHopDong,
            this.barSubItem1,
            this.btnInCongNo,
            this.btnThahToan,
            this.itemChuyenQuyenSuDung,
            this.btn2Excel,
            this.itemExport,
            this.btn2hdt,
            this.btnDaDuyet,
            this.btnThemTaiLieu,
            this.btnXoaTaiLieu,
            this.btnTaiTaiLieu,
            this.itemClose});
            this.barManager1.MaxItemId = 32;
            this.barManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.cmbKyBC,
            this.dateTuNgay,
            this.dateDenNgay,
            lookTT});
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.itemKyBC),
            new DevExpress.XtraBars.LinkPersistInfo(this.itemTuNgay),
            new DevExpress.XtraBars.LinkPersistInfo(this.itemDenNgay),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemNap, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barSubItem1, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnThahToan, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemExport, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemClose, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.DisableClose = true;
            this.bar1.OptionsBar.DisableCustomization = true;
            this.bar1.OptionsBar.DrawDragBorder = false;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Tools";
            // 
            // itemKyBC
            // 
            this.itemKyBC.Caption = "Kỳ báo cáo";
            this.itemKyBC.Edit = this.cmbKyBC;
            this.itemKyBC.EditWidth = 111;
            this.itemKyBC.Id = 4;
            this.itemKyBC.Name = "itemKyBC";
            // 
            // cmbKyBC
            // 
            this.cmbKyBC.AutoHeight = false;
            this.cmbKyBC.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbKyBC.Name = "cmbKyBC";
            this.cmbKyBC.NullText = "Kỳ báo cáo";
            this.cmbKyBC.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbKyBC.EditValueChanged += new System.EventHandler(this.cmbKyBC_EditValueChanged);
            // 
            // itemTuNgay
            // 
            this.itemTuNgay.Caption = "Từ ngày";
            this.itemTuNgay.Edit = this.dateTuNgay;
            this.itemTuNgay.EditWidth = 80;
            this.itemTuNgay.Id = 5;
            this.itemTuNgay.Name = "itemTuNgay";
            this.itemTuNgay.EditValueChanged += new System.EventHandler(this.itemTuNgay_EditValueChanged);
            // 
            // dateTuNgay
            // 
            this.dateTuNgay.AutoHeight = false;
            this.dateTuNgay.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateTuNgay.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateTuNgay.Name = "dateTuNgay";
            this.dateTuNgay.NullText = "Từ ngày";
            // 
            // itemDenNgay
            // 
            this.itemDenNgay.Caption = "Đến ngày";
            this.itemDenNgay.Edit = this.dateDenNgay;
            this.itemDenNgay.EditWidth = 80;
            this.itemDenNgay.Id = 7;
            this.itemDenNgay.Name = "itemDenNgay";
            this.itemDenNgay.EditValueChanged += new System.EventHandler(this.itemDenNgay_EditValueChanged);
            // 
            // dateDenNgay
            // 
            this.dateDenNgay.AutoHeight = false;
            this.dateDenNgay.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateDenNgay.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateDenNgay.Name = "dateDenNgay";
            this.dateDenNgay.NullText = "Đến ngày";
            // 
            // itemNap
            // 
            this.itemNap.Caption = "Nạp";
            this.itemNap.Id = 3;
            this.itemNap.ImageOptions.ImageIndex = 0;
            this.itemNap.Name = "itemNap";
            this.itemNap.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemNap_ItemClick);
            // 
            // barSubItem1
            // 
            this.barSubItem1.Caption = "In ấn";
            this.barSubItem1.Id = 11;
            this.barSubItem1.ImageOptions.ImageIndex = 1;
            this.barSubItem1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnInCongNo, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.barSubItem1.Name = "barSubItem1";
            // 
            // btnInCongNo
            // 
            this.btnInCongNo.Caption = "In công nợ";
            this.btnInCongNo.Id = 16;
            this.btnInCongNo.ImageOptions.ImageIndex = 1;
            this.btnInCongNo.Name = "btnInCongNo";
            this.btnInCongNo.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnInCongNo_ItemClick);
            // 
            // btnThahToan
            // 
            this.btnThahToan.Caption = "Thanh toán";
            this.btnThahToan.Id = 17;
            this.btnThahToan.ImageOptions.ImageIndex = 2;
            this.btnThahToan.Name = "btnThahToan";
            this.btnThahToan.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnThahToan_ItemClick);
            // 
            // itemExport
            // 
            this.itemExport.Caption = "Export";
            this.itemExport.Id = 21;
            this.itemExport.ImageOptions.ImageIndex = 3;
            this.itemExport.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.btn2Excel),
            new DevExpress.XtraBars.LinkPersistInfo(this.btn2hdt)});
            this.itemExport.Name = "itemExport";
            // 
            // btn2Excel
            // 
            this.btn2Excel.Caption = "Export danh sách công nợ";
            this.btn2Excel.Id = 20;
            this.btn2Excel.ImageOptions.ImageIndex = 9;
            this.btn2Excel.Name = "btn2Excel";
            this.btn2Excel.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btn2Excel_ItemClick);
            // 
            // btn2hdt
            // 
            this.btn2hdt.Caption = "Export danh sách hợp đồng thuê";
            this.btn2hdt.Id = 22;
            this.btn2hdt.ImageOptions.ImageIndex = 9;
            this.btn2hdt.Name = "btn2hdt";
            this.btn2hdt.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btn2hdt_ItemClick);
            // 
            // itemClose
            // 
            this.itemClose.Caption = "Đóng";
            this.itemClose.Id = 31;
            this.itemClose.ImageOptions.ImageIndex = 4;
            this.itemClose.Name = "itemClose";
            this.itemClose.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemClose_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(954, 31);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 480);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(954, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 31);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 449);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(954, 31);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 449);
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.Images.SetKeyName(0, "icons8_refresh1.png");
            this.imageCollection1.Images.SetKeyName(1, "icons8_print.png");
            this.imageCollection1.Images.SetKeyName(2, "icons8_average.png");
            this.imageCollection1.Images.SetKeyName(3, "icons8_export.png");
            this.imageCollection1.Images.SetKeyName(4, "icons8_cancel1.png");
            // 
            // btnInHopDong
            // 
            this.btnInHopDong.Caption = "In hợp đồng";
            this.btnInHopDong.Id = 8;
            this.btnInHopDong.ImageOptions.ImageIndex = 4;
            this.btnInHopDong.Name = "btnInHopDong";
            // 
            // itemChuyenQuyenSuDung
            // 
            this.itemChuyenQuyenSuDung.Caption = "Bàn giao mặt bằng";
            this.itemChuyenQuyenSuDung.Id = 19;
            this.itemChuyenQuyenSuDung.ImageOptions.ImageIndex = 7;
            this.itemChuyenQuyenSuDung.Name = "itemChuyenQuyenSuDung";
            // 
            // btnDaDuyet
            // 
            this.btnDaDuyet.Caption = "Duyệt hợp đồng";
            this.btnDaDuyet.Id = 25;
            this.btnDaDuyet.ImageOptions.ImageIndex = 12;
            this.btnDaDuyet.Name = "btnDaDuyet";
            // 
            // btnThemTaiLieu
            // 
            this.btnThemTaiLieu.Caption = "Thêm tài liệu";
            this.btnThemTaiLieu.Id = 28;
            this.btnThemTaiLieu.ImageOptions.ImageIndex = 0;
            this.btnThemTaiLieu.Name = "btnThemTaiLieu";
            // 
            // btnXoaTaiLieu
            // 
            this.btnXoaTaiLieu.Caption = "Xóa tài liệu";
            this.btnXoaTaiLieu.Id = 29;
            this.btnXoaTaiLieu.ImageOptions.ImageIndex = 1;
            this.btnXoaTaiLieu.Name = "btnXoaTaiLieu";
            // 
            // btnTaiTaiLieu
            // 
            this.btnTaiTaiLieu.Caption = "Tải tài liệu về";
            this.btnTaiTaiLieu.Id = 30;
            this.btnTaiTaiLieu.ImageOptions.ImageIndex = 14;
            this.btnTaiTaiLieu.Name = "btnTaiTaiLieu";
            // 
            // gcHopDong
            // 
            this.gcHopDong.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcHopDong.Location = new System.Drawing.Point(0, 31);
            this.gcHopDong.MainView = this.grvHopDong;
            this.gcHopDong.MenuManager = this.barManager1;
            this.gcHopDong.Name = "gcHopDong";
            this.gcHopDong.Size = new System.Drawing.Size(954, 449);
            this.gcHopDong.TabIndex = 4;
            this.gcHopDong.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvHopDong,
            this.gridView2});
            // 
            // grvHopDong
            // 
            this.grvHopDong.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colSoHopDong,
            this.gridColumn1,
            this.gridColumn13,
            this.gridColumn6,
            this.gridColumn11,
            this.colGiaThue,
            this.colPhiQuanLy,
            this.colTienCoc,
            this.gridColumn22,
            this.gridColumn14,
            this.gridColumn15,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn34,
            this.colMaTG,
            this.colTimeRemain});
            this.grvHopDong.GridControl = this.gcHopDong;
            this.grvHopDong.Name = "grvHopDong";
            this.grvHopDong.OptionsBehavior.Editable = false;
            this.grvHopDong.OptionsView.ColumnAutoWidth = false;
            this.grvHopDong.OptionsView.ShowAutoFilterRow = true;
            this.grvHopDong.OptionsView.ShowFooter = true;
            this.grvHopDong.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.grvHopDong_FocusedRowChanged);
            // 
            // colSoHopDong
            // 
            this.colSoHopDong.Caption = "Số HĐ";
            this.colSoHopDong.FieldName = "SoHD";
            this.colSoHopDong.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.colSoHopDong.Name = "colSoHopDong";
            this.colSoHopDong.Visible = true;
            this.colSoHopDong.VisibleIndex = 0;
            this.colSoHopDong.Width = 91;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Ngày ký";
            this.gridColumn1.FieldName = "NgayHD";
            this.gridColumn1.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 1;
            this.gridColumn1.Width = 109;
            // 
            // gridColumn13
            // 
            this.gridColumn13.Caption = "Ngày bàn giao";
            this.gridColumn13.FieldName = "NgayBG";
            this.gridColumn13.Name = "gridColumn13";
            this.gridColumn13.Visible = true;
            this.gridColumn13.VisibleIndex = 2;
            this.gridColumn13.Width = 100;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "Thời hạn";
            this.gridColumn6.DisplayFormat.FormatString = "{0} tháng";
            this.gridColumn6.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn6.FieldName = "ThoiHan";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 3;
            this.gridColumn6.Width = 70;
            // 
            // gridColumn11
            // 
            this.gridColumn11.Caption = "Trạng thái";
            this.gridColumn11.FieldName = "TenTT";
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.Visible = true;
            this.gridColumn11.VisibleIndex = 5;
            this.gridColumn11.Width = 105;
            // 
            // colGiaThue
            // 
            this.colGiaThue.Caption = "Giá thuê";
            this.colGiaThue.DisplayFormat.FormatString = "{0:#,0.##}";
            this.colGiaThue.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colGiaThue.FieldName = "ThanhTien";
            this.colGiaThue.Name = "colGiaThue";
            this.colGiaThue.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Custom, "ThanhTien", "{0:#,0.##} VNĐ", "GiaThue")});
            this.colGiaThue.Visible = true;
            this.colGiaThue.VisibleIndex = 6;
            this.colGiaThue.Width = 88;
            // 
            // colPhiQuanLy
            // 
            this.colPhiQuanLy.Caption = "Phí quản lý";
            this.colPhiQuanLy.DisplayFormat.FormatString = "{0:#,0.##}";
            this.colPhiQuanLy.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colPhiQuanLy.FieldName = "PhiQL";
            this.colPhiQuanLy.Name = "colPhiQuanLy";
            this.colPhiQuanLy.Visible = true;
            this.colPhiQuanLy.VisibleIndex = 7;
            this.colPhiQuanLy.Width = 82;
            // 
            // colTienCoc
            // 
            this.colTienCoc.Caption = "Tiền cọc";
            this.colTienCoc.DisplayFormat.FormatString = "{0:#,0.##}";
            this.colTienCoc.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colTienCoc.FieldName = "TienCoc";
            this.colTienCoc.Name = "colTienCoc";
            this.colTienCoc.Visible = true;
            this.colTienCoc.VisibleIndex = 8;
            this.colTienCoc.Width = 84;
            // 
            // gridColumn22
            // 
            this.gridColumn22.Caption = "Loại tiền";
            this.gridColumn22.FieldName = "TenTG";
            this.gridColumn22.Name = "gridColumn22";
            this.gridColumn22.Visible = true;
            this.gridColumn22.VisibleIndex = 9;
            this.gridColumn22.Width = 50;
            // 
            // gridColumn14
            // 
            this.gridColumn14.Caption = "Điện thoại";
            this.gridColumn14.FieldName = "DienThoaiKH";
            this.gridColumn14.Name = "gridColumn14";
            this.gridColumn14.Visible = true;
            this.gridColumn14.VisibleIndex = 10;
            // 
            // gridColumn15
            // 
            this.gridColumn15.Caption = "Địa chỉ";
            this.gridColumn15.FieldName = "DiaChiKH";
            this.gridColumn15.Name = "gridColumn15";
            this.gridColumn15.Visible = true;
            this.gridColumn15.VisibleIndex = 11;
            this.gridColumn15.Width = 126;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Diễn giải";
            this.gridColumn3.FieldName = "DienGiai";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 12;
            this.gridColumn3.Width = 106;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Nhân viên";
            this.gridColumn4.FieldName = "HoTenNV";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 13;
            this.gridColumn4.Width = 99;
            // 
            // gridColumn34
            // 
            this.gridColumn34.Caption = "Chu kỳ";
            this.gridColumn34.DisplayFormat.FormatString = "{0:#,0.##} tháng/lần";
            this.gridColumn34.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn34.FieldName = "ChuKyThanhToan";
            this.gridColumn34.Name = "gridColumn34";
            this.gridColumn34.Visible = true;
            this.gridColumn34.VisibleIndex = 14;
            this.gridColumn34.Width = 74;
            // 
            // colMaTG
            // 
            this.colMaTG.Caption = "colMaTG";
            this.colMaTG.FieldName = "MaTG";
            this.colMaTG.Name = "colMaTG";
            // 
            // colTimeRemain
            // 
            this.colTimeRemain.Caption = "Thời gian còn lại";
            this.colTimeRemain.DisplayFormat.FormatString = "{0:#,0.##} ngày";
            this.colTimeRemain.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colTimeRemain.FieldName = "TimeRemain";
            this.colTimeRemain.Name = "colTimeRemain";
            this.colTimeRemain.Visible = true;
            this.colTimeRemain.VisibleIndex = 4;
            this.colTimeRemain.Width = 93;
            // 
            // gridView2
            // 
            this.gridView2.GridControl = this.gcHopDong;
            this.gridView2.Name = "gridView2";
            // 
            // frmDanhSachHopDongSapHetHan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(954, 480);
            this.Controls.Add(this.gcHopDong);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "frmDanhSachHopDongSapHetHan";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hợp đồng sắp hết hạn";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmDanhSachHopDongSapHetHan_Load);
            ((System.ComponentModel.ISupportInitialize)(lookTT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbKyBC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTuNgay.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTuNgay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDenNgay.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDenNgay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcHopDong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvHopDong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarEditItem itemKyBC;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox cmbKyBC;
        private DevExpress.XtraBars.BarEditItem itemTuNgay;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit dateTuNgay;
        private DevExpress.XtraBars.BarEditItem itemDenNgay;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit dateDenNgay;
        private DevExpress.XtraBars.BarButtonItem itemNap;
        private DevExpress.XtraBars.BarSubItem barSubItem1;
        private DevExpress.XtraBars.BarButtonItem btnInHopDong;
        private DevExpress.XtraBars.BarButtonItem btnInCongNo;
        private DevExpress.XtraBars.BarButtonItem btnDaDuyet;
        private DevExpress.XtraBars.BarButtonItem itemChuyenQuyenSuDung;
        private DevExpress.XtraBars.BarButtonItem btnThahToan;
        private DevExpress.XtraBars.BarSubItem itemExport;
        private DevExpress.XtraBars.BarButtonItem btn2Excel;
        private DevExpress.XtraBars.BarButtonItem btn2hdt;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.Utils.ImageCollection imageCollection1;
        private DevExpress.XtraBars.BarButtonItem btnThemTaiLieu;
        private DevExpress.XtraBars.BarButtonItem btnXoaTaiLieu;
        private DevExpress.XtraBars.BarButtonItem btnTaiTaiLieu;
        private DevExpress.XtraGrid.GridControl gcHopDong;
        private DevExpress.XtraGrid.Views.Grid.GridView grvHopDong;
        private DevExpress.XtraGrid.Columns.GridColumn colSoHopDong;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn13;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private DevExpress.XtraGrid.Columns.GridColumn colGiaThue;
        private DevExpress.XtraGrid.Columns.GridColumn colPhiQuanLy;
        private DevExpress.XtraGrid.Columns.GridColumn colTienCoc;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn22;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn14;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn15;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn34;
        private DevExpress.XtraGrid.Columns.GridColumn colMaTG;
        private DevExpress.XtraGrid.Columns.GridColumn colTimeRemain;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraBars.BarButtonItem itemClose;
    }
}