namespace DichVu.ChoThue.CongNo
{
    partial class frmCongNoManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCongNoManager));
            this.barManager1 = new DevExpress.XtraBars.BarManager();
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.itemToaNha = new DevExpress.XtraBars.BarEditItem();
            this.lookUpToaNha = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.itemTuNgay = new DevExpress.XtraBars.BarEditItem();
            this.dateTuNGay = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.itemDenNgay = new DevExpress.XtraBars.BarEditItem();
            this.DenNgay = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.btnRefresh = new DevExpress.XtraBars.BarButtonItem();
            this.itemEdit = new DevExpress.XtraBars.BarButtonItem();
            this.btnThanhToan = new DevExpress.XtraBars.BarButtonItem();
            this.itemSendMail = new DevExpress.XtraBars.BarButtonItem();
            this.itemSendSMS = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection();
            this.barStaticItem1 = new DevExpress.XtraBars.BarStaticItem();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.TuNgay = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.gcCongNo = new DevExpress.XtraGrid.GridControl();
            this.grvCongNo = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDaThanhToan = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colConNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPhaiThanhToan = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.itemKyBC = new DevExpress.XtraBars.BarEditItem();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpToaNha)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTuNGay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTuNGay.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DenNgay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DenNgay.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TuNgay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TuNgay.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcCongNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvCongNo)).BeginInit();
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
            this.itemDenNgay,
            this.btnRefresh,
            this.barStaticItem1,
            this.barButtonItem1,
            this.btnThanhToan,
            this.itemSendMail,
            this.itemSendSMS,
            this.itemToaNha,
            this.itemTuNgay,
            this.itemEdit});
            this.barManager1.MaxItemId = 13;
            this.barManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.DenNgay,
            this.TuNgay,
            this.lookUpToaNha,
            this.dateTuNGay});
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.itemToaNha),
            new DevExpress.XtraBars.LinkPersistInfo(this.itemTuNgay),
            new DevExpress.XtraBars.LinkPersistInfo(this.itemDenNgay),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnRefresh, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemEdit, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnThanhToan, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemSendMail, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemSendSMS, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar1.OptionsBar.AllowCollapse = true;
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.DisableCustomization = true;
            this.bar1.OptionsBar.DrawDragBorder = false;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Tools";
            // 
            // itemToaNha
            // 
            this.itemToaNha.Edit = this.lookUpToaNha;
            this.itemToaNha.EditWidth = 156;
            this.itemToaNha.Id = 9;
            this.itemToaNha.Name = "itemToaNha";
            this.itemToaNha.EditValueChanged += new System.EventHandler(this.itemToaNha_EditValueChanged);
            // 
            // lookUpToaNha
            // 
            this.lookUpToaNha.AutoHeight = false;
            this.lookUpToaNha.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpToaNha.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenTN", "Name10")});
            this.lookUpToaNha.DisplayMember = "TenTN";
            this.lookUpToaNha.Name = "lookUpToaNha";
            this.lookUpToaNha.NullText = "[Chọn Dự án]";
            this.lookUpToaNha.ShowHeader = false;
            this.lookUpToaNha.ValueMember = "MaTN";
            // 
            // itemTuNgay
            // 
            this.itemTuNgay.Caption = "Từ ngày";
            this.itemTuNgay.Edit = this.dateTuNGay;
            this.itemTuNgay.EditWidth = 90;
            this.itemTuNgay.Id = 11;
            this.itemTuNgay.Name = "itemTuNgay";
            // 
            // dateTuNGay
            // 
            this.dateTuNGay.AutoHeight = false;
            this.dateTuNGay.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateTuNGay.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateTuNGay.DisplayFormat.FormatString = "{0:dd/MM/yyyy}";
            this.dateTuNGay.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateTuNGay.EditFormat.FormatString = "{0:dd/MM/yyyy}";
            this.dateTuNGay.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateTuNGay.Name = "dateTuNGay";
            this.dateTuNGay.NullText = "Từ ngày";
            // 
            // itemDenNgay
            // 
            this.itemDenNgay.Caption = "Đến ngày";
            this.itemDenNgay.Edit = this.DenNgay;
            this.itemDenNgay.EditWidth = 90;
            this.itemDenNgay.Id = 0;
            this.itemDenNgay.Name = "itemDenNgay";
            this.itemDenNgay.EditValueChanged += new System.EventHandler(this.itemDenNgay_EditValueChanged);
            // 
            // DenNgay
            // 
            this.DenNgay.AutoHeight = false;
            this.DenNgay.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.DenNgay.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.DenNgay.Name = "DenNgay";
            this.DenNgay.NullText = "Đến ngày";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Caption = "Nạp";
            this.btnRefresh.Id = 1;
            this.btnRefresh.ImageOptions.ImageIndex = 0;
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnRefresh_ItemClick);
            // 
            // itemEdit
            // 
            this.itemEdit.Caption = "Sửa";
            this.itemEdit.Id = 12;
            this.itemEdit.ImageOptions.ImageIndex = 1;
            this.itemEdit.Name = "itemEdit";
            this.itemEdit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemEdit_ItemClick);
            // 
            // btnThanhToan
            // 
            this.btnThanhToan.Caption = "Thanh toán";
            this.btnThanhToan.Id = 5;
            this.btnThanhToan.ImageOptions.ImageIndex = 2;
            this.btnThanhToan.Name = "btnThanhToan";
            this.btnThanhToan.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnThanhToan_ItemClick);
            // 
            // itemSendMail
            // 
            this.itemSendMail.Caption = "Email";
            this.itemSendMail.Id = 6;
            this.itemSendMail.ImageOptions.ImageIndex = 3;
            this.itemSendMail.Name = "itemSendMail";
            // 
            // itemSendSMS
            // 
            this.itemSendSMS.Caption = "SMS";
            this.itemSendSMS.Id = 7;
            this.itemSendSMS.ImageOptions.ImageIndex = 4;
            this.itemSendSMS.Name = "itemSendSMS";
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(1020, 31);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 483);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(1020, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 31);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 452);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1020, 31);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 452);
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.Images.SetKeyName(0, "icons8_refresh1.png");
            this.imageCollection1.Images.SetKeyName(1, "icons8_edit3.png");
            this.imageCollection1.Images.SetKeyName(2, "icons8_thanhtoan.png");
            this.imageCollection1.Images.SetKeyName(3, "icons8_mail1.png");
            this.imageCollection1.Images.SetKeyName(4, "icons8_phone.png");
            // 
            // barStaticItem1
            // 
            this.barStaticItem1.Caption = "Hợp đồng";
            this.barStaticItem1.Id = 2;
            this.barStaticItem1.Name = "barStaticItem1";
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "In";
            this.barButtonItem1.Id = 4;
            this.barButtonItem1.Name = "barButtonItem1";
            // 
            // TuNgay
            // 
            this.TuNgay.AutoHeight = false;
            this.TuNgay.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.TuNgay.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.TuNgay.Name = "TuNgay";
            this.TuNgay.NullText = "Từ ngày";
            // 
            // gcCongNo
            // 
            this.gcCongNo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcCongNo.Location = new System.Drawing.Point(0, 31);
            this.gcCongNo.MainView = this.grvCongNo;
            this.gcCongNo.MenuManager = this.barManager1;
            this.gcCongNo.Name = "gcCongNo";
            this.gcCongNo.Size = new System.Drawing.Size(1020, 452);
            this.gcCongNo.TabIndex = 4;
            this.gcCongNo.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvCongNo});
            // 
            // grvCongNo
            // 
            this.grvCongNo.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.colDaThanhToan,
            this.colConNo,
            this.colPhaiThanhToan,
            this.gridColumn7,
            this.gridColumn4,
            this.gridColumn8,
            this.gridColumn9,
            this.gridColumn10,
            this.gridColumn11,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn5,
            this.gridColumn6});
            this.grvCongNo.GridControl = this.gcCongNo;
            this.grvCongNo.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "DaThanhToan", this.colDaThanhToan, "Tổng: {0:#,0.##}"),
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "ConNo", this.colConNo, "Tổng: {0:#,0.##}"),
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "PhaiThanhToan", this.colPhaiThanhToan, "Tổng: {0:#,0.##}")});
            this.grvCongNo.Name = "grvCongNo";
            this.grvCongNo.OptionsBehavior.Editable = false;
            this.grvCongNo.OptionsView.ColumnAutoWidth = false;
            this.grvCongNo.OptionsView.ShowAutoFilterRow = true;
            this.grvCongNo.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.grvCongNo.OptionsView.ShowFooter = true;
            this.grvCongNo.OptionsView.ShowGroupedColumns = true;
            this.grvCongNo.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.gridColumn2, DevExpress.Data.ColumnSortOrder.Ascending)});
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Hợp đồng";
            this.gridColumn1.FieldName = "SoHopDong";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 103;
            // 
            // colDaThanhToan
            // 
            this.colDaThanhToan.Caption = "Đã thanh toán";
            this.colDaThanhToan.DisplayFormat.FormatString = "{0:#,0.#}";
            this.colDaThanhToan.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colDaThanhToan.FieldName = "DaThanhToan";
            this.colDaThanhToan.Name = "colDaThanhToan";
            this.colDaThanhToan.OptionsColumn.AllowEdit = false;
            this.colDaThanhToan.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "DaThanhToan", "{0:#,0.#}")});
            this.colDaThanhToan.Visible = true;
            this.colDaThanhToan.VisibleIndex = 5;
            this.colDaThanhToan.Width = 85;
            // 
            // colConNo
            // 
            this.colConNo.Caption = "Nợ nay";
            this.colConNo.DisplayFormat.FormatString = "{0:#,0.#}";
            this.colConNo.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colConNo.FieldName = "ConNo";
            this.colConNo.Name = "colConNo";
            this.colConNo.OptionsColumn.AllowEdit = false;
            this.colConNo.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "ConNo", "{0:#,0.#}")});
            this.colConNo.Visible = true;
            this.colConNo.VisibleIndex = 6;
            this.colConNo.Width = 92;
            // 
            // colPhaiThanhToan
            // 
            this.colPhaiThanhToan.Caption = "Phải thanh toán";
            this.colPhaiThanhToan.DisplayFormat.FormatString = "{0:#,0.#}";
            this.colPhaiThanhToan.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colPhaiThanhToan.FieldName = "PhaiThanhToan";
            this.colPhaiThanhToan.Name = "colPhaiThanhToan";
            this.colPhaiThanhToan.OptionsColumn.AllowEdit = false;
            this.colPhaiThanhToan.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "PhaiThanhToan", "{0:#,0.#}")});
            this.colPhaiThanhToan.Visible = true;
            this.colPhaiThanhToan.VisibleIndex = 4;
            this.colPhaiThanhToan.Width = 92;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "Loại tiền";
            this.gridColumn7.FieldName = "LoaiTien";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.OptionsColumn.AllowEdit = false;
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 9;
            this.gridColumn7.Width = 52;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Ngày thanh toán";
            this.gridColumn4.DisplayFormat.FormatString = "{0:dd/MM/yyyy}";
            this.gridColumn4.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gridColumn4.FieldName = "ChuKyMin";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 3;
            this.gridColumn4.Width = 94;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "Họ và tên";
            this.gridColumn8.FieldName = "HoVaTenKH";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.OptionsColumn.AllowEdit = false;
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 10;
            this.gridColumn8.Width = 107;
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "Địa  chỉ";
            this.gridColumn9.FieldName = "DiaChiKH";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.OptionsColumn.AllowEdit = false;
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 11;
            this.gridColumn9.Width = 127;
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "Số điện thoại";
            this.gridColumn10.FieldName = "SoDienThoaiKH";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.OptionsColumn.AllowEdit = false;
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 12;
            this.gridColumn10.Width = 91;
            // 
            // gridColumn11
            // 
            this.gridColumn11.Caption = "Email";
            this.gridColumn11.FieldName = "EmailKH";
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.OptionsColumn.AllowEdit = false;
            this.gridColumn11.Visible = true;
            this.gridColumn11.VisibleIndex = 13;
            this.gridColumn11.Width = 125;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Khách hàng";
            this.gridColumn2.FieldName = "HoVaTenKH";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 2;
            this.gridColumn2.Width = 138;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Nợ trước";
            this.gridColumn3.DisplayFormat.FormatString = "{0:#,0.#}";
            this.gridColumn3.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn3.FieldName = "NoTruoc";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "NoTruoc", "{0:#,0.#}")});
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 7;
            this.gridColumn3.Width = 95;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Tổng nợ";
            this.gridColumn5.DisplayFormat.FormatString = "{0:#,0.#}";
            this.gridColumn5.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn5.FieldName = "TongNo";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            this.gridColumn5.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "TongNo", "{0:#,0.#}")});
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 8;
            this.gridColumn5.Width = 95;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "Mặt bằng";
            this.gridColumn6.FieldName = "MaSoMB";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.OptionsColumn.AllowEdit = false;
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 1;
            this.gridColumn6.Width = 104;
            // 
            // itemKyBC
            // 
            this.itemKyBC.Caption = "Kỳ báo cáo";
            this.itemKyBC.Edit = null;
            this.itemKyBC.EditWidth = 111;
            this.itemKyBC.Id = 4;
            this.itemKyBC.Name = "itemKyBC";
            // 
            // frmCongNoManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1020, 483);
            this.Controls.Add(this.gcCongNo);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "frmCongNoManager";
            this.Text = "Công nợ Hợp đồng cho thuê";
            this.Load += new System.EventHandler(this.frmCongNoManager_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpToaNha)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTuNGay.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTuNGay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DenNgay.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DenNgay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TuNgay.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TuNgay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcCongNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvCongNo)).EndInit();
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
        private DevExpress.XtraBars.BarEditItem itemDenNgay;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit DenNgay;
        private DevExpress.XtraBars.BarButtonItem btnRefresh;
        private DevExpress.XtraGrid.GridControl gcCongNo;
        private DevExpress.XtraGrid.Views.Grid.GridView grvCongNo;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn colDaThanhToan;
        private DevExpress.XtraGrid.Columns.GridColumn colConNo;
        private DevExpress.XtraGrid.Columns.GridColumn colPhaiThanhToan;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private DevExpress.XtraBars.BarStaticItem barStaticItem1;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit TuNgay;
        private DevExpress.Utils.ImageCollection imageCollection1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarButtonItem btnThanhToan;
        private DevExpress.XtraBars.BarButtonItem itemSendMail;
        private DevExpress.XtraBars.BarButtonItem itemSendSMS;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraBars.BarEditItem itemToaNha;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lookUpToaNha;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraBars.BarEditItem itemKyBC;
        private DevExpress.XtraBars.BarEditItem itemTuNgay;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit dateTuNGay;
        private DevExpress.XtraBars.BarButtonItem itemEdit;
    }
}