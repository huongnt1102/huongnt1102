namespace LandSoftBuilding.Lease
{
    partial class frmImportPhoYen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmImportPhoYen));
            this.gcHD = new DevExpress.XtraGrid.GridControl();
            this.grvHD = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNgayDK = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSoThe = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colChuThe = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLoaiXe = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBienSo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDoiXe = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMauXe = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNgayHH = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colKyTT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDienGiai = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn15 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn14 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn31 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn27 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn26 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn16 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn17 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.barManager1 = new DevExpress.XtraBars.BarManager();
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.itemChoice = new DevExpress.XtraBars.BarButtonItem();
            this.itemSheet = new DevExpress.XtraBars.BarEditItem();
            this.cmbSheet = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.itemSave = new DevExpress.XtraBars.BarButtonItem();
            this.itemDelete = new DevExpress.XtraBars.BarButtonItem();
            this.itemClose = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection();
            this.dateNgayNhap = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.lookUpToaNha = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.cmbKieuLTT = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.itemExportMau = new DevExpress.XtraBars.BarButtonItem();
            ((System.ComponentModel.ISupportInitialize)(this.gcHD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvHD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSheet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateNgayNhap)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateNgayNhap.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpToaNha)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbKieuLTT)).BeginInit();
            this.SuspendLayout();
            // 
            // gcHD
            // 
            this.gcHD.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcHD.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gcHD.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gcHD.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gcHD.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gcHD.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.gcHD.EmbeddedNavigator.TextStringFormat = "{0}/{1}";
            this.gcHD.Location = new System.Drawing.Point(0, 31);
            this.gcHD.MainView = this.grvHD;
            this.gcHD.Name = "gcHD";
            this.gcHD.Size = new System.Drawing.Size(899, 413);
            this.gcHD.TabIndex = 10;
            this.gcHD.UseEmbeddedNavigator = true;
            this.gcHD.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvHD});
            // 
            // grvHD
            // 
            this.grvHD.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn3,
            this.colNgayDK,
            this.colSoThe,
            this.colChuThe,
            this.colLoaiXe,
            this.colBienSo,
            this.colDoiXe,
            this.colMauXe,
            this.colNgayHH,
            this.colKyTT,
            this.gridColumn4,
            this.gridColumn5,
            this.colDienGiai,
            this.gridColumn15,
            this.gridColumn14,
            this.gridColumn13,
            this.gridColumn12,
            this.gridColumn11,
            this.gridColumn10,
            this.gridColumn9,
            this.gridColumn8,
            this.gridColumn7,
            this.gridColumn6,
            this.gridColumn1,
            this.gridColumn31,
            this.gridColumn27,
            this.gridColumn26,
            this.gridColumn2,
            this.gridColumn16,
            this.gridColumn17});
            this.grvHD.GridControl = this.gcHD;
            this.grvHD.Name = "grvHD";
            this.grvHD.OptionsSelection.MultiSelect = true;
            this.grvHD.OptionsView.ColumnAutoWidth = false;
            this.grvHD.OptionsView.ShowAutoFilterRow = true;
            this.grvHD.OptionsView.ShowGroupedColumns = true;
            this.grvHD.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Số hợp đồng";
            this.gridColumn3.FieldName = "SoHD";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 0;
            this.gridColumn3.Width = 99;
            // 
            // colNgayDK
            // 
            this.colNgayDK.Caption = "Số phụ lục";
            this.colNgayDK.FieldName = "SoPL";
            this.colNgayDK.Name = "colNgayDK";
            this.colNgayDK.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.colNgayDK.Visible = true;
            this.colNgayDK.VisibleIndex = 1;
            this.colNgayDK.Width = 98;
            // 
            // colSoThe
            // 
            this.colSoThe.Caption = "Ngày ký";
            this.colSoThe.DisplayFormat.FormatString = "{0:dd/MM/yyyy}";
            this.colSoThe.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colSoThe.FieldName = "NgayKy";
            this.colSoThe.Name = "colSoThe";
            this.colSoThe.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.colSoThe.Visible = true;
            this.colSoThe.VisibleIndex = 2;
            // 
            // colChuThe
            // 
            this.colChuThe.Caption = "Ngày hiệu lực";
            this.colChuThe.DisplayFormat.FormatString = "{0:dd/MM/yyyy}";
            this.colChuThe.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colChuThe.FieldName = "NgayHL";
            this.colChuThe.Name = "colChuThe";
            this.colChuThe.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.colChuThe.Visible = true;
            this.colChuThe.VisibleIndex = 3;
            // 
            // colLoaiXe
            // 
            this.colLoaiXe.Caption = "Ngày hết hạn";
            this.colLoaiXe.DisplayFormat.FormatString = "{0:dd/MM/yyyy}";
            this.colLoaiXe.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colLoaiXe.FieldName = "NgayHH";
            this.colLoaiXe.Name = "colLoaiXe";
            this.colLoaiXe.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.colLoaiXe.Visible = true;
            this.colLoaiXe.VisibleIndex = 4;
            // 
            // colBienSo
            // 
            this.colBienSo.Caption = "Thời hạn";
            this.colBienSo.FieldName = "ThoiHan";
            this.colBienSo.Name = "colBienSo";
            this.colBienSo.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.colBienSo.Visible = true;
            this.colBienSo.VisibleIndex = 5;
            // 
            // colDoiXe
            // 
            this.colDoiXe.Caption = "Kỳ thanh toán";
            this.colDoiXe.FieldName = "KyThanhToan";
            this.colDoiXe.Name = "colDoiXe";
            this.colDoiXe.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.colDoiXe.Visible = true;
            this.colDoiXe.VisibleIndex = 8;
            this.colDoiXe.Width = 79;
            // 
            // colMauXe
            // 
            this.colMauXe.Caption = "Ngày bàn giao";
            this.colMauXe.DisplayFormat.FormatString = "{0:dd/MM/yyyy}";
            this.colMauXe.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colMauXe.FieldName = "NgayBG";
            this.colMauXe.Name = "colMauXe";
            this.colMauXe.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.colMauXe.Visible = true;
            this.colMauXe.VisibleIndex = 9;
            this.colMauXe.Width = 81;
            // 
            // colNgayHH
            // 
            this.colNgayHH.Caption = "Tỷ lệ ĐCGT";
            this.colNgayHH.DisplayFormat.FormatString = "{0:#,0.##}";
            this.colNgayHH.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colNgayHH.FieldName = "TyLeDCTG";
            this.colNgayHH.Name = "colNgayHH";
            this.colNgayHH.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.colNgayHH.Visible = true;
            this.colNgayHH.VisibleIndex = 10;
            this.colNgayHH.Width = 67;
            // 
            // colKyTT
            // 
            this.colKyTT.Caption = "Số năm ĐCGT";
            this.colKyTT.FieldName = "SoNamDCTG";
            this.colKyTT.Name = "colKyTT";
            this.colKyTT.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.colKyTT.Visible = true;
            this.colKyTT.VisibleIndex = 11;
            this.colKyTT.Width = 77;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Mức ĐCGT";
            this.gridColumn4.DisplayFormat.FormatString = "{0:#,0.##}";
            this.gridColumn4.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn4.FieldName = "MucDCTG";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 12;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Loại tiền";
            this.gridColumn5.FieldName = "TenLT";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 13;
            // 
            // colDienGiai
            // 
            this.colDienGiai.Caption = "Tỷ giá";
            this.colDienGiai.DisplayFormat.FormatString = "{0:n0}";
            this.colDienGiai.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colDienGiai.FieldName = "TyGia";
            this.colDienGiai.Name = "colDienGiai";
            this.colDienGiai.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.colDienGiai.Visible = true;
            this.colDienGiai.VisibleIndex = 14;
            // 
            // gridColumn15
            // 
            this.gridColumn15.Caption = "Mặt bằng";
            this.gridColumn15.FieldName = "MaSoMB";
            this.gridColumn15.Name = "gridColumn15";
            this.gridColumn15.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.gridColumn15.Visible = true;
            this.gridColumn15.VisibleIndex = 15;
            // 
            // gridColumn14
            // 
            this.gridColumn14.Caption = "Loại giá thuê";
            this.gridColumn14.FieldName = "LoaiGiaThue";
            this.gridColumn14.Name = "gridColumn14";
            this.gridColumn14.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // gridColumn13
            // 
            this.gridColumn13.Caption = "Diện tích";
            this.gridColumn13.DisplayFormat.FormatString = "{0:#,0.##} m2";
            this.gridColumn13.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn13.FieldName = "DienTich";
            this.gridColumn13.Name = "gridColumn13";
            this.gridColumn13.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.gridColumn13.Visible = true;
            this.gridColumn13.VisibleIndex = 16;
            // 
            // gridColumn12
            // 
            this.gridColumn12.Caption = "Đơn giá";
            this.gridColumn12.DisplayFormat.FormatString = "{0:n0}";
            this.gridColumn12.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn12.FieldName = "DonGia";
            this.gridColumn12.Name = "gridColumn12";
            this.gridColumn12.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.gridColumn12.Visible = true;
            this.gridColumn12.VisibleIndex = 17;
            this.gridColumn12.Width = 80;
            // 
            // gridColumn11
            // 
            this.gridColumn11.Caption = "Phí dịch vụ";
            this.gridColumn11.DisplayFormat.FormatString = "{0:n0}";
            this.gridColumn11.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn11.FieldName = "PhiDichVu";
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.gridColumn11.Visible = true;
            this.gridColumn11.VisibleIndex = 18;
            this.gridColumn11.Width = 80;
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "Thành tiền";
            this.gridColumn10.DisplayFormat.FormatString = "{0:n0}";
            this.gridColumn10.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn10.FieldName = "ThanhTien";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 19;
            this.gridColumn10.Width = 80;
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "Tỷ lệ VAT";
            this.gridColumn9.DisplayFormat.FormatString = "{0:p0}";
            this.gridColumn9.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn9.FieldName = "TyLeVAT";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 20;
            this.gridColumn9.Width = 65;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "Tiền VAT";
            this.gridColumn8.DisplayFormat.FormatString = "{0:n0}";
            this.gridColumn8.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn8.FieldName = "TienVAT";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 21;
            this.gridColumn8.Width = 80;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "Tỷ lệ CK";
            this.gridColumn7.DisplayFormat.FormatString = "{0:p0}";
            this.gridColumn7.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn7.FieldName = "TyLeCK";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 22;
            this.gridColumn7.Width = 65;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "Tiền CK";
            this.gridColumn6.DisplayFormat.FormatString = "{0:n0}";
            this.gridColumn6.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn6.FieldName = "TienCK";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 23;
            this.gridColumn6.Width = 80;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Phí sửa chữa";
            this.gridColumn1.DisplayFormat.FormatString = "{0:n0}";
            this.gridColumn1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn1.FieldName = "PhiSuaChua";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 24;
            this.gridColumn1.Width = 80;
            // 
            // gridColumn31
            // 
            this.gridColumn31.Caption = "Đợt đặt cọc";
            this.gridColumn31.FieldName = "DotDatCoc";
            this.gridColumn31.Name = "gridColumn31";
            this.gridColumn31.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.gridColumn31.Visible = true;
            this.gridColumn31.VisibleIndex = 25;
            this.gridColumn31.Width = 69;
            // 
            // gridColumn27
            // 
            this.gridColumn27.Caption = "Số tiền";
            this.gridColumn27.DisplayFormat.FormatString = "{0:n0}";
            this.gridColumn27.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn27.FieldName = "SoTien";
            this.gridColumn27.Name = "gridColumn27";
            this.gridColumn27.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.gridColumn27.Visible = true;
            this.gridColumn27.VisibleIndex = 26;
            this.gridColumn27.Width = 80;
            // 
            // gridColumn26
            // 
            this.gridColumn26.Caption = "Diễn giải";
            this.gridColumn26.FieldName = "DienGiai";
            this.gridColumn26.Name = "gridColumn26";
            this.gridColumn26.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.gridColumn26.Visible = true;
            this.gridColumn26.VisibleIndex = 27;
            this.gridColumn26.Width = 203;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Error";
            this.gridColumn2.FieldName = "Error";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 28;
            this.gridColumn2.Width = 200;
            // 
            // gridColumn16
            // 
            this.gridColumn16.Caption = "Thời gian thuê";
            this.gridColumn16.FieldName = "ThoiGianThue";
            this.gridColumn16.Name = "gridColumn16";
            this.gridColumn16.Visible = true;
            this.gridColumn16.VisibleIndex = 7;
            // 
            // gridColumn17
            // 
            this.gridColumn17.Caption = "Thời gian ưu đãi";
            this.gridColumn17.FieldName = "ThoiGianUD";
            this.gridColumn17.Name = "gridColumn17";
            this.gridColumn17.Visible = true;
            this.gridColumn17.VisibleIndex = 6;
            this.gridColumn17.Width = 101;
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
            this.itemChoice,
            this.itemSave,
            this.itemDelete,
            this.itemClose,
            this.itemSheet,
            this.itemExportMau});
            this.barManager1.MaxItemId = 10;
            this.barManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.cmbSheet,
            this.dateNgayNhap,
            this.lookUpToaNha,
            this.cmbKieuLTT});
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemChoice, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemSheet, DevExpress.XtraBars.BarItemPaintStyle.Standard),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemSave, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemDelete, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemClose, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemExportMau, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.DisableCustomization = true;
            this.bar1.OptionsBar.DrawDragBorder = false;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Tools";
            // 
            // itemChoice
            // 
            this.itemChoice.Caption = "Chọn file";
            this.itemChoice.Id = 0;
            this.itemChoice.ImageOptions.ImageIndex = 0;
            this.itemChoice.Name = "itemChoice";
            this.itemChoice.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemChoice_ItemClick);
            // 
            // itemSheet
            // 
            this.itemSheet.Edit = this.cmbSheet;
            this.itemSheet.EditWidth = 100;
            this.itemSheet.Id = 5;
            this.itemSheet.Name = "itemSheet";
            this.itemSheet.EditValueChanged += new System.EventHandler(this.itemSheet_EditValueChanged);
            // 
            // cmbSheet
            // 
            this.cmbSheet.AutoHeight = false;
            this.cmbSheet.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbSheet.Name = "cmbSheet";
            this.cmbSheet.NullText = "[Chọn sheet]";
            this.cmbSheet.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            // 
            // itemSave
            // 
            this.itemSave.Caption = "Lưu";
            this.itemSave.Id = 2;
            this.itemSave.ImageOptions.ImageIndex = 1;
            this.itemSave.Name = "itemSave";
            this.itemSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemSave_ItemClick);
            // 
            // itemDelete
            // 
            this.itemDelete.Caption = "Xóa dòng";
            this.itemDelete.Id = 3;
            this.itemDelete.ImageOptions.ImageIndex = 2;
            this.itemDelete.Name = "itemDelete";
            this.itemDelete.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemDelete_ItemClick);
            // 
            // itemClose
            // 
            this.itemClose.Caption = "Đóng";
            this.itemClose.Id = 4;
            this.itemClose.ImageOptions.ImageIndex = 3;
            this.itemClose.Name = "itemClose";
            this.itemClose.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemClose_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(899, 31);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 444);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(899, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 31);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 413);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(899, 31);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 413);
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.Images.SetKeyName(0, "Open1.png");
            this.imageCollection1.Images.SetKeyName(1, "Save1.png");
            this.imageCollection1.Images.SetKeyName(2, "Delete1.png");
            this.imageCollection1.Images.SetKeyName(3, "Cancel1.png");
            this.imageCollection1.Images.SetKeyName(4, "Export1.png");
            // 
            // dateNgayNhap
            // 
            this.dateNgayNhap.AutoHeight = false;
            this.dateNgayNhap.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateNgayNhap.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateNgayNhap.DisplayFormat.FormatString = "{0:dd/MM/yyyy}";
            this.dateNgayNhap.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateNgayNhap.EditFormat.FormatString = "{0:dd/MM/yyyy}";
            this.dateNgayNhap.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateNgayNhap.Mask.EditMask = "dd/MM/yyyy";
            this.dateNgayNhap.Name = "dateNgayNhap";
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
            this.lookUpToaNha.NullText = "[Chọn Dự án]";
            this.lookUpToaNha.ShowHeader = false;
            this.lookUpToaNha.ValueMember = "MaTN";
            // 
            // cmbKieuLTT
            // 
            this.cmbKieuLTT.AutoHeight = false;
            this.cmbKieuLTT.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbKieuLTT.Items.AddRange(new object[] {
            "Tạo lịch thanh toán theo từng thẻ xe",
            "Tạo lịch thanh toán tổng hợp"});
            this.cmbKieuLTT.Name = "cmbKieuLTT";
            this.cmbKieuLTT.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            // 
            // itemExportMau
            // 
            this.itemExportMau.Caption = "Export mẫu";
            this.itemExportMau.Id = 9;
            this.itemExportMau.ImageOptions.ImageIndex = 4;
            this.itemExportMau.Name = "itemExportMau";
            this.itemExportMau.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemExportMau_ItemClick);
            // 
            // frmImportPhoYen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(899, 444);
            this.Controls.Add(this.gcHD);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "frmImportPhoYen";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Import danh sách hợp đồng thuê";
            this.Load += new System.EventHandler(this.frmImport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gcHD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvHD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSheet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateNgayNhap.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateNgayNhap)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpToaNha)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbKieuLTT)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gcHD;
        private DevExpress.XtraGrid.Views.Grid.GridView grvHD;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarButtonItem itemChoice;
        private DevExpress.XtraBars.BarEditItem itemSheet;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox cmbSheet;
        private DevExpress.XtraBars.BarButtonItem itemDelete;
        private DevExpress.XtraBars.BarButtonItem itemSave;
        private DevExpress.XtraBars.BarButtonItem itemClose;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.Utils.ImageCollection imageCollection1;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit dateNgayNhap;
        private DevExpress.XtraGrid.Columns.GridColumn colSoThe;
        private DevExpress.XtraGrid.Columns.GridColumn colNgayDK;
        private DevExpress.XtraGrid.Columns.GridColumn colNgayHH;
        private DevExpress.XtraGrid.Columns.GridColumn colChuThe;
        private DevExpress.XtraGrid.Columns.GridColumn colLoaiXe;
        private DevExpress.XtraGrid.Columns.GridColumn colBienSo;
        private DevExpress.XtraGrid.Columns.GridColumn colDoiXe;
        private DevExpress.XtraGrid.Columns.GridColumn colMauXe;
        private DevExpress.XtraGrid.Columns.GridColumn colDienGiai;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lookUpToaNha;
        private DevExpress.XtraGrid.Columns.GridColumn colKyTT;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox cmbKieuLTT;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn15;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn14;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn13;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn12;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn31;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn27;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn26;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn16;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn17;
        private DevExpress.XtraBars.BarButtonItem itemExportMau;
    }
}