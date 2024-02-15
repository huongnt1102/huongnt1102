namespace DichVu.LichSuDieuChinhCN
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
            this.itemYear = new DevExpress.XtraBars.BarEditItem();
            this.spinYear = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.itemMonth = new DevExpress.XtraBars.BarEditItem();
            this.cmbMonth = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.itemToaNha = new DevExpress.XtraBars.BarEditItem();
            this.lookUpToaNha = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.itemRefresh = new DevExpress.XtraBars.BarButtonItem();
            this.itemExport = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection();
            this.itemKyBC = new DevExpress.XtraBars.BarEditItem();
            this.cmbKyBC = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.itemTuNgay = new DevExpress.XtraBars.BarEditItem();
            this.dateTuNgay = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.itemDenNgay = new DevExpress.XtraBars.BarEditItem();
            this.dateDenNgay = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.itemInGBDien = new DevExpress.XtraBars.BarButtonItem();
            this.itemInGBNuoc = new DevExpress.XtraBars.BarButtonItem();
            this.barCheckItem1 = new DevExpress.XtraBars.BarCheckItem();
            this.itemInGBGas = new DevExpress.XtraBars.BarButtonItem();
            this.itemInGBTongHop = new DevExpress.XtraBars.BarButtonItem();
            this.itemViewGBDien = new DevExpress.XtraBars.BarButtonItem();
            this.itemViewGBNuoc = new DevExpress.XtraBars.BarButtonItem();
            this.itemViewGBGas = new DevExpress.XtraBars.BarButtonItem();
            this.itemViewTongHop = new DevExpress.XtraBars.BarButtonItem();
            this.itemSynPQL = new DevExpress.XtraBars.BarButtonItem();
            this.itemSynPVS = new DevExpress.XtraBars.BarButtonItem();
            this.itemSynDien = new DevExpress.XtraBars.BarButtonItem();
            this.itemSynNuoc = new DevExpress.XtraBars.BarButtonItem();
            this.itemSynGas = new DevExpress.XtraBars.BarButtonItem();
            this.cmbYear = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.toolTipController1 = new DevExpress.Utils.ToolTipController();
            this.gcTongHop = new DevExpress.XtraGrid.GridControl();
            this.gvTongHop = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemMemoExEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinYear)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbMonth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpToaNha)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbKyBC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTuNgay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTuNgay.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDenNgay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDenNgay.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbYear)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcTongHop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvTongHop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoExEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // barManager1
            // 
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
            this.itemKyBC,
            this.itemTuNgay,
            this.itemDenNgay,
            this.itemRefresh,
            this.itemMonth,
            this.itemYear,
            this.itemInGBDien,
            this.itemInGBNuoc,
            this.barCheckItem1,
            this.itemInGBGas,
            this.itemInGBTongHop,
            this.itemViewGBDien,
            this.itemViewGBNuoc,
            this.itemViewGBGas,
            this.itemViewTongHop,
            this.itemToaNha,
            this.itemSynPQL,
            this.itemSynPVS,
            this.itemSynDien,
            this.itemSynNuoc,
            this.itemSynGas,
            this.itemExport});
            this.barManager1.MaxItemId = 30;
            this.barManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.cmbKyBC,
            this.dateTuNgay,
            this.dateDenNgay,
            this.cmbYear,
            this.cmbMonth,
            this.spinYear,
            this.lookUpToaNha});
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.itemYear),
            new DevExpress.XtraBars.LinkPersistInfo(this.itemMonth),
            new DevExpress.XtraBars.LinkPersistInfo(this.itemToaNha),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemRefresh, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemExport, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.DisableCustomization = true;
            this.bar1.OptionsBar.DrawDragBorder = false;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Tools";
            // 
            // itemYear
            // 
            this.itemYear.Edit = this.spinYear;
            this.itemYear.EditValue = 2013;
            this.itemYear.EditWidth = 60;
            this.itemYear.Id = 10;
            this.itemYear.Name = "itemYear";
            this.itemYear.EditValueChanged += new System.EventHandler(this.itemYear_EditValueChanged);
            // 
            // spinYear
            // 
            this.spinYear.AutoHeight = false;
            this.spinYear.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinYear.Mask.EditMask = "n0";
            this.spinYear.MaxValue = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.spinYear.MinValue = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.spinYear.Name = "spinYear";
            // 
            // itemMonth
            // 
            this.itemMonth.Edit = this.cmbMonth;
            this.itemMonth.EditWidth = 81;
            this.itemMonth.Id = 9;
            this.itemMonth.Name = "itemMonth";
            this.itemMonth.EditValueChanged += new System.EventHandler(this.itemMonth_EditValueChanged);
            // 
            // cmbMonth
            // 
            this.cmbMonth.AutoHeight = false;
            this.cmbMonth.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbMonth.Items.AddRange(new object[] {
            "[Tháng]",
            "Tháng 1",
            "Tháng 2",
            "Tháng 3",
            "Tháng 4",
            "Tháng 5",
            "Tháng 6",
            "Tháng 7",
            "Tháng 8",
            "Tháng 9",
            "Tháng 10",
            "Tháng 11",
            "Tháng 12"});
            this.cmbMonth.Name = "cmbMonth";
            this.cmbMonth.NullText = "[Tháng]";
            this.cmbMonth.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            // 
            // itemToaNha
            // 
            this.itemToaNha.Edit = this.lookUpToaNha;
            this.itemToaNha.EditWidth = 144;
            this.itemToaNha.Id = 22;
            this.itemToaNha.Name = "itemToaNha";
            this.itemToaNha.EditValueChanged += new System.EventHandler(this.itemToaNha_EditValueChanged);
            // 
            // lookUpToaNha
            // 
            this.lookUpToaNha.AutoHeight = false;
            this.lookUpToaNha.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpToaNha.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenTN", "Name1")});
            this.lookUpToaNha.DisplayMember = "TenTN";
            this.lookUpToaNha.Name = "lookUpToaNha";
            this.lookUpToaNha.NullText = "[Chọn tòa nhà]";
            this.lookUpToaNha.ShowHeader = false;
            this.lookUpToaNha.ValueMember = "MaTN";
            // 
            // itemRefresh
            // 
            this.itemRefresh.Caption = "Nạp";
            this.itemRefresh.Id = 3;
            this.itemRefresh.ImageOptions.ImageIndex = 0;
            this.itemRefresh.Name = "itemRefresh";
            this.itemRefresh.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemRefresh_ItemClick);
            // 
            // itemExport
            // 
            this.itemExport.Caption = "Export Excel";
            this.itemExport.Id = 29;
            this.itemExport.ImageOptions.ImageIndex = 1;
            this.itemExport.Name = "itemExport";
            this.itemExport.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemExport_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(979, 31);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 496);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(979, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 31);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 465);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(979, 31);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 465);
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.Images.SetKeyName(0, "icons8_refresh1.png");  
            this.imageCollection1.Images.SetKeyName(1, "icons8_export.png");  
            // 
            // itemKyBC
            // 
            this.itemKyBC.Edit = this.cmbKyBC;
            this.itemKyBC.EditWidth = 119;
            this.itemKyBC.Id = 0;
            this.itemKyBC.Name = "itemKyBC";
            // 
            // cmbKyBC
            // 
            this.cmbKyBC.AutoHeight = false;
            this.cmbKyBC.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbKyBC.Name = "cmbKyBC";
            this.cmbKyBC.NullText = "[Kỳ báo cáo]";
            this.cmbKyBC.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            // 
            // itemTuNgay
            // 
            this.itemTuNgay.Edit = this.dateTuNgay;
            this.itemTuNgay.EditWidth = 95;
            this.itemTuNgay.Id = 1;
            this.itemTuNgay.Name = "itemTuNgay";
            // 
            // dateTuNgay
            // 
            this.dateTuNgay.AutoHeight = false;
            this.dateTuNgay.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateTuNgay.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateTuNgay.DisplayFormat.FormatString = "{0:dd/MM/yyyy}";
            this.dateTuNgay.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateTuNgay.EditFormat.FormatString = "{0:dd/MM/yyyy}";
            this.dateTuNgay.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateTuNgay.Mask.EditMask = "dd/MM/yyyy";
            this.dateTuNgay.Name = "dateTuNgay";
            this.dateTuNgay.NullText = "[Từ ngày]";
            // 
            // itemDenNgay
            // 
            this.itemDenNgay.Edit = this.dateDenNgay;
            this.itemDenNgay.EditWidth = 94;
            this.itemDenNgay.Id = 2;
            this.itemDenNgay.Name = "itemDenNgay";
            // 
            // dateDenNgay
            // 
            this.dateDenNgay.AutoHeight = false;
            this.dateDenNgay.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateDenNgay.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateDenNgay.DisplayFormat.FormatString = "{0:dd/MM/yyyy}";
            this.dateDenNgay.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateDenNgay.EditFormat.FormatString = "{0:dd/MM/yyyy}";
            this.dateDenNgay.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateDenNgay.Mask.EditMask = "dd/MM/yyyy";
            this.dateDenNgay.Name = "dateDenNgay";
            this.dateDenNgay.NullText = "[Đến ngày]";
            // 
            // itemInGBDien
            // 
            this.itemInGBDien.Caption = "Giấy báo tiền điện";
            this.itemInGBDien.Id = 12;
            this.itemInGBDien.Name = "itemInGBDien";
            // 
            // itemInGBNuoc
            // 
            this.itemInGBNuoc.Caption = "Giấy báo tiền nước";
            this.itemInGBNuoc.Id = 13;
            this.itemInGBNuoc.Name = "itemInGBNuoc";
            // 
            // barCheckItem1
            // 
            this.barCheckItem1.Caption = "barCheckItem1";
            this.barCheckItem1.Id = 14;
            this.barCheckItem1.Name = "barCheckItem1";
            // 
            // itemInGBGas
            // 
            this.itemInGBGas.Caption = "Giấy báo tiền gas";
            this.itemInGBGas.Id = 15;
            this.itemInGBGas.Name = "itemInGBGas";
            // 
            // itemInGBTongHop
            // 
            this.itemInGBTongHop.Caption = "Giấy báo tổng hợp";
            this.itemInGBTongHop.Id = 16;
            this.itemInGBTongHop.Name = "itemInGBTongHop";
            // 
            // itemViewGBDien
            // 
            this.itemViewGBDien.Caption = "Giấy báo tiền điện";
            this.itemViewGBDien.Id = 18;
            this.itemViewGBDien.Name = "itemViewGBDien";
            // 
            // itemViewGBNuoc
            // 
            this.itemViewGBNuoc.Caption = "Giấy báo tiền nước";
            this.itemViewGBNuoc.Id = 19;
            this.itemViewGBNuoc.Name = "itemViewGBNuoc";
            // 
            // itemViewGBGas
            // 
            this.itemViewGBGas.Caption = "Giấy báo tiền gas";
            this.itemViewGBGas.Id = 20;
            this.itemViewGBGas.Name = "itemViewGBGas";
            // 
            // itemViewTongHop
            // 
            this.itemViewTongHop.Caption = "Giấy báo tổng hợp";
            this.itemViewTongHop.Id = 21;
            this.itemViewTongHop.Name = "itemViewTongHop";
            // 
            // itemSynPQL
            // 
            this.itemSynPQL.Caption = "Phí quản lý";
            this.itemSynPQL.Id = 24;
            this.itemSynPQL.Name = "itemSynPQL";
            // 
            // itemSynPVS
            // 
            this.itemSynPVS.Caption = "Phí vệ sinh";
            this.itemSynPVS.Id = 25;
            this.itemSynPVS.Name = "itemSynPVS";
            // 
            // itemSynDien
            // 
            this.itemSynDien.Caption = "Điện";
            this.itemSynDien.Id = 26;
            this.itemSynDien.Name = "itemSynDien";
            // 
            // itemSynNuoc
            // 
            this.itemSynNuoc.Caption = "Nước";
            this.itemSynNuoc.Id = 27;
            this.itemSynNuoc.Name = "itemSynNuoc";
            // 
            // itemSynGas
            // 
            this.itemSynGas.Caption = "Gas";
            this.itemSynGas.Id = 28;
            this.itemSynGas.Name = "itemSynGas";
            // 
            // cmbYear
            // 
            this.cmbYear.AutoHeight = false;
            this.cmbYear.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbYear.Name = "cmbYear";
            this.cmbYear.NullText = "[Năm]";
            this.cmbYear.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            // 
            // toolTipController1
            // 
            this.toolTipController1.AutoPopDelay = 50000;
            this.toolTipController1.InitialDelay = 10;
            this.toolTipController1.Rounded = true;
            this.toolTipController1.RoundRadius = 4;
            // 
            // gcTongHop
            // 
            this.gcTongHop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcTongHop.Location = new System.Drawing.Point(0, 31);
            this.gcTongHop.MainView = this.gvTongHop;
            this.gcTongHop.MenuManager = this.barManager1;
            this.gcTongHop.Name = "gcTongHop";
            this.gcTongHop.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemMemoExEdit1});
            this.gcTongHop.Size = new System.Drawing.Size(979, 465);
            this.gcTongHop.TabIndex = 9;
            this.gcTongHop.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvTongHop});
            // 
            // gvTongHop
            // 
            this.gvTongHop.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn7,
            this.gridColumn10,
            this.gridColumn8,
            this.gridColumn9});
            this.gvTongHop.GridControl = this.gcTongHop;
            this.gvTongHop.Name = "gvTongHop";
            this.gvTongHop.OptionsSelection.MultiSelect = true;
            this.gvTongHop.OptionsView.ColumnAutoWidth = false;
            this.gvTongHop.OptionsView.ShowAutoFilterRow = true;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Mặt bằng";
            this.gridColumn1.FieldName = "MaSoMB";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 101;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Khách hàng";
            this.gridColumn2.FieldName = "KhachHang";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 150;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Ngày DC";
            this.gridColumn3.DisplayFormat.FormatString = "{0:dd/MM/yyyy}";
            this.gridColumn3.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gridColumn3.FieldName = "NgayDC";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 5;
            this.gridColumn3.Width = 102;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Nhân viên DC";
            this.gridColumn4.FieldName = "HoTenNV";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 6;
            this.gridColumn4.Width = 117;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "Diễn giải";
            this.gridColumn7.ColumnEdit = this.repositoryItemMemoExEdit1;
            this.gridColumn7.FieldName = "DienGiai";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 7;
            this.gridColumn7.Width = 206;
            // 
            // repositoryItemMemoExEdit1
            // 
            this.repositoryItemMemoExEdit1.AutoHeight = false;
            this.repositoryItemMemoExEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemMemoExEdit1.Name = "repositoryItemMemoExEdit1";
            this.repositoryItemMemoExEdit1.PopupFormMinSize = new System.Drawing.Size(400, 120);
            this.repositoryItemMemoExEdit1.ReadOnly = true;
            this.repositoryItemMemoExEdit1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.repositoryItemMemoExEdit1.ShowIcon = false;
            // 
            // gridColumn10
            // 
            this.gridColumn10.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn10.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn10.Caption = "Loại dịch vụ";
            this.gridColumn10.FieldName = "LoaiDV";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.OptionsColumn.AllowEdit = false;
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 2;
            this.gridColumn10.Width = 97;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "Tiền trước DC";
            this.gridColumn8.DisplayFormat.FormatString = "{0:#,0.##}";
            this.gridColumn8.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn8.FieldName = "TienTruocDC";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 3;
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "Tiền sau DC";
            this.gridColumn9.DisplayFormat.FormatString = "{0:#,0.##}";
            this.gridColumn9.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn9.FieldName = "TienSauDC";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 4;
            // 
            // frmManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(979, 496);
            this.Controls.Add(this.gcTongHop);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.KeyPreview = true;
            this.Name = "frmManager";
            this.Text = "Lịch sử điều chỉnh công nợ";
            this.Load += new System.EventHandler(this.frmManager_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinYear)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbMonth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpToaNha)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbKyBC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTuNgay.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTuNgay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDenNgay.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDenNgay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbYear)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcTongHop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvTongHop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoExEdit1)).EndInit();
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
        private DevExpress.XtraBars.BarEditItem itemKyBC;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox cmbKyBC;
        private DevExpress.XtraBars.BarEditItem itemTuNgay;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit dateTuNgay;
        private DevExpress.XtraBars.BarEditItem itemDenNgay;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit dateDenNgay;
        private DevExpress.XtraBars.BarButtonItem itemRefresh;
        private DevExpress.Utils.ImageCollection imageCollection1;
        private DevExpress.Utils.ToolTipController toolTipController1;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox cmbYear;
        private DevExpress.XtraBars.BarEditItem itemMonth;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox cmbMonth;
        private DevExpress.XtraBars.BarEditItem itemYear;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit spinYear;
        private DevExpress.XtraBars.BarButtonItem itemInGBDien;
        private DevExpress.XtraBars.BarButtonItem itemInGBNuoc;
        private DevExpress.XtraBars.BarButtonItem itemInGBGas;
        private DevExpress.XtraBars.BarCheckItem barCheckItem1;
        private DevExpress.XtraBars.BarButtonItem itemInGBTongHop;
        private DevExpress.XtraBars.BarButtonItem itemViewGBDien;
        private DevExpress.XtraBars.BarButtonItem itemViewGBNuoc;
        private DevExpress.XtraBars.BarButtonItem itemViewGBGas;
        private DevExpress.XtraBars.BarButtonItem itemViewTongHop;
        private DevExpress.XtraBars.BarEditItem itemToaNha;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lookUpToaNha;
        private DevExpress.XtraBars.BarButtonItem itemSynPQL;
        private DevExpress.XtraBars.BarButtonItem itemSynPVS;
        private DevExpress.XtraBars.BarButtonItem itemSynDien;
        private DevExpress.XtraBars.BarButtonItem itemSynNuoc;
        private DevExpress.XtraBars.BarButtonItem itemSynGas;
        private DevExpress.XtraGrid.GridControl gcTongHop;
        private DevExpress.XtraGrid.Views.Grid.GridView gvTongHop;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit repositoryItemMemoExEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraBars.BarButtonItem itemExport;
    }
}