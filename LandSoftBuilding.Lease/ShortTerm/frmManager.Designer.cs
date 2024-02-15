namespace LandSoftBuilding.Lease.ShortTerm
{
    partial class frmManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmManager));
            this.barManager1 = new DevExpress.XtraBars.BarManager();
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.itemToaNha = new DevExpress.XtraBars.BarEditItem();
            this.lkToaNha = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.itemKyBC = new DevExpress.XtraBars.BarEditItem();
            this.cbbKyBC = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.itemTuNgay = new DevExpress.XtraBars.BarEditItem();
            this.dateTuNgay = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.itemDenNgay = new DevExpress.XtraBars.BarEditItem();
            this.dateDenNgay = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.btnNap = new DevExpress.XtraBars.BarButtonItem();
            this.btnThem = new DevExpress.XtraBars.BarButtonItem();
            this.btnSua = new DevExpress.XtraBars.BarButtonItem();
            this.btnXoa = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection();
            this.gcNganHan = new DevExpress.XtraGrid.GridControl();
            this.gvNganHan = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colKhachHang = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMatBang = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn15 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn16 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn17 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn18 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn19 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn20 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn14 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn21 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn22 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn23 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.linqInstantFeedbackSource1 = new DevExpress.Data.Linq.LinqInstantFeedbackSource();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkToaNha)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbbKyBC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTuNgay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTuNgay.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDenNgay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDenNgay.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcNganHan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvNganHan)).BeginInit();
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
            this.btnNap,
            this.btnThem,
            this.btnXoa,
            this.btnSua,
            this.itemKyBC,
            this.itemTuNgay,
            this.itemDenNgay,
            this.itemToaNha});
            this.barManager1.MaxItemId = 15;
            this.barManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.cbbKyBC,
            this.dateTuNgay,
            this.dateDenNgay,
            this.lkToaNha});
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemToaNha, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(this.itemKyBC),
            new DevExpress.XtraBars.LinkPersistInfo(this.itemTuNgay),
            new DevExpress.XtraBars.LinkPersistInfo(this.itemDenNgay),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnNap, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnThem, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnSua, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnXoa, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.DrawDragBorder = false;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Tools";
            // 
            // itemToaNha
            // 
            this.itemToaNha.Caption = "Dự án";
            this.itemToaNha.Edit = this.lkToaNha;
            this.itemToaNha.EditWidth = 91;
            this.itemToaNha.Id = 14;
            this.itemToaNha.Name = "itemToaNha";
            // 
            // lkToaNha
            // 
            this.lkToaNha.AutoHeight = false;
            this.lkToaNha.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkToaNha.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenTN", "Dự án")});
            this.lkToaNha.DisplayMember = "TenTN";
            this.lkToaNha.Name = "lkToaNha";
            this.lkToaNha.NullText = "";
            this.lkToaNha.ValueMember = "MaTN";
            // 
            // itemKyBC
            // 
            this.itemKyBC.Caption = "Kỳ báo cáo";
            this.itemKyBC.Edit = this.cbbKyBC;
            this.itemKyBC.EditWidth = 100;
            this.itemKyBC.Id = 6;
            this.itemKyBC.Name = "itemKyBC";
            // 
            // cbbKyBC
            // 
            this.cbbKyBC.AutoHeight = false;
            this.cbbKyBC.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbbKyBC.Name = "cbbKyBC";
            this.cbbKyBC.NullText = "Kỳ báo cáo";
            this.cbbKyBC.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cbbKyBC.EditValueChanged += new System.EventHandler(this.cbbKyBC_EditValueChanged);
            // 
            // itemTuNgay
            // 
            this.itemTuNgay.Caption = "Từ Ngày";
            this.itemTuNgay.Edit = this.dateTuNgay;
            this.itemTuNgay.EditWidth = 100;
            this.itemTuNgay.Id = 7;
            this.itemTuNgay.Name = "itemTuNgay";
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
            this.itemDenNgay.EditWidth = 100;
            this.itemDenNgay.Id = 8;
            this.itemDenNgay.Name = "itemDenNgay";
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
            // btnNap
            // 
            this.btnNap.Caption = "Nạp";
            this.btnNap.Id = 0;
            this.btnNap.ImageOptions.ImageIndex = 0;
            this.btnNap.Name = "btnNap";
            this.btnNap.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnNap_ItemClick);
            // 
            // btnThem
            // 
            this.btnThem.Caption = "Thêm";
            this.btnThem.Id = 1;
            this.btnThem.ImageOptions.ImageIndex = 1;
            this.btnThem.Name = "btnThem";
            this.btnThem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnThem_ItemClick);
            // 
            // btnSua
            // 
            this.btnSua.Caption = "Sửa";
            this.btnSua.Id = 3;
            this.btnSua.ImageOptions.ImageIndex = 2;
            this.btnSua.Name = "btnSua";
            this.btnSua.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSua_ItemClick);
            // 
            // btnXoa
            // 
            this.btnXoa.Caption = "Xóa";
            this.btnXoa.Id = 2;
            this.btnXoa.ImageOptions.ImageIndex = 3;
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnXoa_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(983, 31);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 431);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(983, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 31);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 400);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(983, 31);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 400);
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.Images.SetKeyName(0, "Refresh1.png");
            this.imageCollection1.Images.SetKeyName(1, "Add1.png");
            this.imageCollection1.Images.SetKeyName(2, "Edit3.png");
            this.imageCollection1.Images.SetKeyName(3, "Delete1.png");
            // 
            // gcNganHan
            // 
            this.gcNganHan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcNganHan.Location = new System.Drawing.Point(0, 31);
            this.gcNganHan.MainView = this.gvNganHan;
            this.gcNganHan.MenuManager = this.barManager1;
            this.gcNganHan.Name = "gcNganHan";
            this.gcNganHan.Size = new System.Drawing.Size(983, 400);
            this.gcNganHan.TabIndex = 9;
            this.gcNganHan.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvNganHan});
            // 
            // gvNganHan
            // 
            this.gvNganHan.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn2,
            this.colKhachHang,
            this.gridColumn3,
            this.colMatBang,
            this.gridColumn6,
            this.gridColumn12,
            this.gridColumn15,
            this.gridColumn16,
            this.gridColumn17,
            this.gridColumn7,
            this.gridColumn13,
            this.gridColumn18,
            this.gridColumn19,
            this.gridColumn20,
            this.gridColumn14,
            this.gridColumn11,
            this.gridColumn5,
            this.gridColumn21,
            this.gridColumn22,
            this.gridColumn23,
            this.gridColumn8,
            this.gridColumn1,
            this.gridColumn4,
            this.gridColumn9,
            this.gridColumn10});
            this.gvNganHan.GridControl = this.gcNganHan;
            this.gvNganHan.GroupPanelText = "Kéo một vài cột lên đây để xem theo nhóm";
            this.gvNganHan.Name = "gvNganHan";
            this.gvNganHan.OptionsBehavior.Editable = false;
            this.gvNganHan.OptionsView.ColumnAutoWidth = false;
            this.gvNganHan.OptionsView.ShowAutoFilterRow = true;
            this.gvNganHan.OptionsView.ShowFooter = true;
            this.gvNganHan.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.gridColumn2, DevExpress.Data.ColumnSortOrder.Descending)});
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Ngày CT";
            this.gridColumn2.FieldName = "NgayCT";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "NgayCT", "{0}")});
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 0;
            this.gridColumn2.Width = 77;
            // 
            // colKhachHang
            // 
            this.colKhachHang.Caption = "Mã khách hàng";
            this.colKhachHang.FieldName = "KyHieu";
            this.colKhachHang.Name = "colKhachHang";
            this.colKhachHang.OptionsColumn.AllowEdit = false;
            this.colKhachHang.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.colKhachHang.Visible = true;
            this.colKhachHang.VisibleIndex = 1;
            this.colKhachHang.Width = 89;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Tên khách hàng";
            this.gridColumn3.FieldName = "TenKH";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            this.gridColumn3.Width = 203;
            // 
            // colMatBang
            // 
            this.colMatBang.Caption = "Mặt bằng";
            this.colMatBang.FieldName = "MaSoMB";
            this.colMatBang.Name = "colMatBang";
            this.colMatBang.OptionsColumn.AllowEdit = false;
            this.colMatBang.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.colMatBang.Visible = true;
            this.colMatBang.VisibleIndex = 3;
            this.colMatBang.Width = 89;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "Từ ngày";
            this.gridColumn6.DisplayFormat.FormatString = "HH:mm | dd/MM/yyyy";
            this.gridColumn6.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gridColumn6.FieldName = "TuNgay";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 4;
            this.gridColumn6.Width = 120;
            // 
            // gridColumn12
            // 
            this.gridColumn12.Caption = "Đến ngày";
            this.gridColumn12.DisplayFormat.FormatString = "HH:mm | dd/MM/yyyy";
            this.gridColumn12.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gridColumn12.FieldName = "DenNgay";
            this.gridColumn12.Name = "gridColumn12";
            this.gridColumn12.Visible = true;
            this.gridColumn12.VisibleIndex = 5;
            this.gridColumn12.Width = 114;
            // 
            // gridColumn15
            // 
            this.gridColumn15.Caption = "Số ngày";
            this.gridColumn15.DisplayFormat.FormatString = "{0:#,0.##}";
            this.gridColumn15.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn15.FieldName = "SoNgay";
            this.gridColumn15.Name = "gridColumn15";
            this.gridColumn15.Visible = true;
            this.gridColumn15.VisibleIndex = 6;
            this.gridColumn15.Width = 54;
            // 
            // gridColumn16
            // 
            this.gridColumn16.Caption = "Giá ngày";
            this.gridColumn16.DisplayFormat.FormatString = "{0:#,0.##}";
            this.gridColumn16.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn16.FieldName = "GiaNgay";
            this.gridColumn16.Name = "gridColumn16";
            this.gridColumn16.Visible = true;
            this.gridColumn16.VisibleIndex = 7;
            // 
            // gridColumn17
            // 
            this.gridColumn17.Caption = "Tiền ngày";
            this.gridColumn17.DisplayFormat.FormatString = "{0:#,0.##}";
            this.gridColumn17.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn17.FieldName = "TienNgay";
            this.gridColumn17.Name = "gridColumn17";
            this.gridColumn17.Visible = true;
            this.gridColumn17.VisibleIndex = 8;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "Số giờ";
            this.gridColumn7.DisplayFormat.FormatString = "{0:#,0.##}";
            this.gridColumn7.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn7.FieldName = "SoGio";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "SoGio", "{0:n0}")});
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 9;
            this.gridColumn7.Width = 52;
            // 
            // gridColumn13
            // 
            this.gridColumn13.Caption = "Giá giờ";
            this.gridColumn13.DisplayFormat.FormatString = "{0:#,0.##}";
            this.gridColumn13.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn13.FieldName = "GiaGioi";
            this.gridColumn13.Name = "gridColumn13";
            this.gridColumn13.OptionsColumn.AllowEdit = false;
            this.gridColumn13.Visible = true;
            this.gridColumn13.VisibleIndex = 10;
            this.gridColumn13.Width = 80;
            // 
            // gridColumn18
            // 
            this.gridColumn18.Caption = "Tiền giờ";
            this.gridColumn18.DisplayFormat.FormatString = "{0:#,0.##}";
            this.gridColumn18.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn18.FieldName = "TienGio";
            this.gridColumn18.Name = "gridColumn18";
            this.gridColumn18.Visible = true;
            this.gridColumn18.VisibleIndex = 11;
            // 
            // gridColumn19
            // 
            this.gridColumn19.Caption = "Tiền thuê";
            this.gridColumn19.DisplayFormat.FormatString = "{0:#,0.##}";
            this.gridColumn19.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn19.FieldName = "TienThue";
            this.gridColumn19.Name = "gridColumn19";
            this.gridColumn19.Visible = true;
            this.gridColumn19.VisibleIndex = 12;
            // 
            // gridColumn20
            // 
            this.gridColumn20.Caption = "Tiền dịch vụ";
            this.gridColumn20.DisplayFormat.FormatString = "{0:#,0.##}";
            this.gridColumn20.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn20.FieldName = "TienDV";
            this.gridColumn20.Name = "gridColumn20";
            this.gridColumn20.Visible = true;
            this.gridColumn20.VisibleIndex = 13;
            // 
            // gridColumn14
            // 
            this.gridColumn14.Caption = "Thành Tiền";
            this.gridColumn14.DisplayFormat.FormatString = "{0:#,0.##}";
            this.gridColumn14.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn14.FieldName = "ThanhTien";
            this.gridColumn14.Name = "gridColumn14";
            this.gridColumn14.OptionsColumn.AllowEdit = false;
            this.gridColumn14.Visible = true;
            this.gridColumn14.VisibleIndex = 14;
            this.gridColumn14.Width = 86;
            // 
            // gridColumn11
            // 
            this.gridColumn11.Caption = "Loại Tiền";
            this.gridColumn11.FieldName = "KyHieuLT";
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.OptionsColumn.AllowEdit = false;
            this.gridColumn11.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.gridColumn11.Visible = true;
            this.gridColumn11.VisibleIndex = 15;
            this.gridColumn11.Width = 56;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Thành tiền QĐ";
            this.gridColumn5.DisplayFormat.FormatString = "{0:#,0.##}";
            this.gridColumn5.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn5.FieldName = "ThanhTienQD";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "ThanhTienQD", "{0:n0}")});
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 16;
            this.gridColumn5.Width = 86;
            // 
            // gridColumn21
            // 
            this.gridColumn21.Caption = "Kết chuyển";
            this.gridColumn21.FieldName = "IsKetChuyen";
            this.gridColumn21.Name = "gridColumn21";
            this.gridColumn21.Visible = true;
            this.gridColumn21.VisibleIndex = 17;
            // 
            // gridColumn22
            // 
            this.gridColumn22.Caption = "Đã thu";
            this.gridColumn22.DisplayFormat.FormatString = "{0:#,0.##}";
            this.gridColumn22.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn22.FieldName = "DaThu";
            this.gridColumn22.Name = "gridColumn22";
            this.gridColumn22.Visible = true;
            this.gridColumn22.VisibleIndex = 18;
            // 
            // gridColumn23
            // 
            this.gridColumn23.Caption = "Còn lại";
            this.gridColumn23.DisplayFormat.FormatString = "{0:#,0.##}";
            this.gridColumn23.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn23.FieldName = "ConLai";
            this.gridColumn23.Name = "gridColumn23";
            this.gridColumn23.Visible = true;
            this.gridColumn23.VisibleIndex = 19;
            this.gridColumn23.Width = 87;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "Diễn giải";
            this.gridColumn8.FieldName = "DienGiai";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 20;
            this.gridColumn8.Width = 254;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Người nhập";
            this.gridColumn1.FieldName = "NguoiNhap";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 21;
            this.gridColumn1.Width = 82;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Ngày nhập";
            this.gridColumn4.FieldName = "NgayNhap";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 22;
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "Người sửa";
            this.gridColumn9.FieldName = "NguoiSua";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 23;
            this.gridColumn9.Width = 96;
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "Ngày sửa";
            this.gridColumn10.DisplayFormat.FormatString = "{0:dd/MM/yyyy}";
            this.gridColumn10.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gridColumn10.FieldName = "NgaySua";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 24;
            this.gridColumn10.Width = 85;
            // 
            // linqInstantFeedbackSource1
            // 
            this.linqInstantFeedbackSource1.KeyExpression = "ID";
            this.linqInstantFeedbackSource1.GetQueryable += new System.EventHandler<DevExpress.Data.Linq.GetQueryableEventArgs>(this.linqInstantFeedbackSource1_GetQueryable);
            // 
            // frmManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(983, 431);
            this.Controls.Add(this.gcNganHan);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "frmManager";
            this.Text = "Cho thuê ngắn hạn";
            this.Load += new System.EventHandler(this.frmManager_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkToaNha)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbbKyBC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTuNgay.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTuNgay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDenNgay.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDenNgay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcNganHan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvNganHan)).EndInit();
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
        private DevExpress.XtraBars.BarButtonItem btnNap;
        private DevExpress.XtraBars.BarButtonItem btnThem;
        private DevExpress.XtraBars.BarButtonItem btnXoa;
        private DevExpress.XtraBars.BarButtonItem btnSua;
        private DevExpress.Utils.ImageCollection imageCollection1;
        private DevExpress.XtraBars.BarEditItem itemKyBC;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox cbbKyBC;
        private DevExpress.XtraBars.BarEditItem itemTuNgay;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit dateTuNgay;
        private DevExpress.XtraBars.BarEditItem itemDenNgay;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit dateDenNgay;
        private DevExpress.XtraGrid.GridControl gcNganHan;
        private DevExpress.XtraGrid.Views.Grid.GridView gvNganHan;
        private DevExpress.XtraGrid.Columns.GridColumn colMatBang;
        private DevExpress.XtraGrid.Columns.GridColumn colKhachHang;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraBars.BarEditItem itemToaNha;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lkToaNha;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn13;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn14;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.Data.Linq.LinqInstantFeedbackSource linqInstantFeedbackSource1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn12;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn15;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn16;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn17;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn18;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn19;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn20;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn21;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn22;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn23;
    }
}