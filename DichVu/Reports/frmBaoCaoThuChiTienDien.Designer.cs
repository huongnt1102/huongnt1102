namespace DichVu
{
    partial class frmBaoCaoThuChiTienDien
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBaoCaoThuChiTienDien));
            DevExpress.XtraPivotGrid.PivotGridCustomTotal pivotGridCustomTotal1 = new DevExpress.XtraPivotGrid.PivotGridCustomTotal();
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
            this.itemExport = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection();
            this.btnThem = new DevExpress.XtraBars.BarButtonItem();
            this.btnXoa = new DevExpress.XtraBars.BarButtonItem();
            this.btnSua = new DevExpress.XtraBars.BarButtonItem();
            this.itemThuTien = new DevExpress.XtraBars.BarButtonItem();
            this.pvHoaDon = new DevExpress.XtraPivotGrid.PivotGridControl();
            this.pivotGridField2 = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pivotGridField5 = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pivotGridField4 = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pivotGridField3 = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pivotGridField1 = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pivotGridField2_DauKy = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pivotGridField6_CuoiKy = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pivotGridField10 = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pivotGridField7_ThanhTien = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pivotGridField8_DaThu = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pivotGridField9_ConNo = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pivotGridField12 = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pivotGridField11 = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pivotGridField6 = new DevExpress.XtraPivotGrid.PivotGridField();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkToaNha)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbbKyBC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTuNgay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTuNgay.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDenNgay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDenNgay.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pvHoaDon)).BeginInit();
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
            this.itemToaNha,
            this.itemThuTien,
            this.itemExport});
            this.barManager1.MaxItemId = 17;
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
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemExport, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
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
            this.itemToaNha.EditValueChanged += new System.EventHandler(this.itemToaNha_EditValueChanged);
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
            this.itemTuNgay.EditValueChanged += new System.EventHandler(this.itemToaNha_EditValueChanged);
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
            this.itemDenNgay.EditValueChanged += new System.EventHandler(this.itemToaNha_EditValueChanged);
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
            // itemExport
            // 
            this.itemExport.Caption = "Export";
            this.itemExport.Id = 16;
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
            this.barDockControlTop.Size = new System.Drawing.Size(1022, 31);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 431);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(1022, 0);
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
            this.barDockControlRight.Location = new System.Drawing.Point(1022, 31);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 400);
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.Images.SetKeyName(0, "icons8_refresh1.png");  
            this.imageCollection1.Images.SetKeyName(1, "icons8_export.png");  
            // 
            // btnThem
            // 
            this.btnThem.Caption = "Lập hóa đơn";
            this.btnThem.Id = 1;
            this.btnThem.ImageOptions.ImageIndex = 1;
            this.btnThem.Name = "btnThem";
            // 
            // btnXoa
            // 
            this.btnXoa.Caption = "Xóa";
            this.btnXoa.Id = 2;
            this.btnXoa.ImageOptions.ImageIndex = 2;
            this.btnXoa.Name = "btnXoa";
            // 
            // btnSua
            // 
            this.btnSua.Caption = "Sửa";
            this.btnSua.Id = 3;
            this.btnSua.ImageOptions.ImageIndex = 2;
            this.btnSua.Name = "btnSua";
            // 
            // itemThuTien
            // 
            this.itemThuTien.Caption = "Thu tiền";
            this.itemThuTien.Id = 15;
            this.itemThuTien.ImageOptions.ImageIndex = 3;
            this.itemThuTien.Name = "itemThuTien";
            // 
            // pvHoaDon
            // 
            this.pvHoaDon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pvHoaDon.Fields.AddRange(new DevExpress.XtraPivotGrid.PivotGridField[] {
            this.pivotGridField2,
            this.pivotGridField5,
            this.pivotGridField4,
            this.pivotGridField3,
            this.pivotGridField1,
            this.pivotGridField2_DauKy,
            this.pivotGridField6_CuoiKy,
            this.pivotGridField10,
            this.pivotGridField7_ThanhTien,
            this.pivotGridField8_DaThu,
            this.pivotGridField9_ConNo,
            this.pivotGridField12,
            this.pivotGridField11,
            this.pivotGridField6});
            this.pvHoaDon.Location = new System.Drawing.Point(0, 31);
            this.pvHoaDon.Name = "pvHoaDon";
            this.pvHoaDon.Size = new System.Drawing.Size(1022, 400);
            this.pvHoaDon.TabIndex = 19;
            // 
            // pivotGridField2
            // 
            this.pivotGridField2.AreaIndex = 0;
            this.pivotGridField2.Caption = "Loại mặt bằng";
            this.pivotGridField2.FieldName = "TenLMB";
            this.pivotGridField2.Name = "pivotGridField2";
            // 
            // pivotGridField5
            // 
            this.pivotGridField5.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.pivotGridField5.AreaIndex = 0;
            this.pivotGridField5.Caption = "Khối nhà";
            this.pivotGridField5.FieldName = "TenKN";
            this.pivotGridField5.Name = "pivotGridField5";
            // 
            // pivotGridField4
            // 
            this.pivotGridField4.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.pivotGridField4.AreaIndex = 1;
            this.pivotGridField4.Caption = "Tầng";
            this.pivotGridField4.FieldName = "TenTL";
            this.pivotGridField4.Name = "pivotGridField4";
            // 
            // pivotGridField3
            // 
            this.pivotGridField3.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.pivotGridField3.AreaIndex = 2;
            this.pivotGridField3.Caption = "Mặt bằng";
            this.pivotGridField3.FieldName = "MaSoMB";
            this.pivotGridField3.Name = "pivotGridField3";
            // 
            // pivotGridField1
            // 
            this.pivotGridField1.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.pivotGridField1.AreaIndex = 3;
            this.pivotGridField1.Caption = "Tên khách hàng";
            this.pivotGridField1.FieldName = "TenKH";
            this.pivotGridField1.Name = "pivotGridField1";
            this.pivotGridField1.Width = 200;
            // 
            // pivotGridField2_DauKy
            // 
            this.pivotGridField2_DauKy.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.pivotGridField2_DauKy.AreaIndex = 0;
            this.pivotGridField2_DauKy.Caption = "Đầu kì";
            this.pivotGridField2_DauKy.CellFormat.FormatString = "{0:#,0.##}";
            this.pivotGridField2_DauKy.CellFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.pivotGridField2_DauKy.FieldName = "ChiSoCu";
            this.pivotGridField2_DauKy.GrandTotalCellFormat.FormatString = "{0:#,0.##}";
            this.pivotGridField2_DauKy.GrandTotalCellFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.pivotGridField2_DauKy.Name = "pivotGridField2_DauKy";
            this.pivotGridField2_DauKy.TotalCellFormat.FormatString = "{0:#,0.##}";
            this.pivotGridField2_DauKy.TotalCellFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.pivotGridField2_DauKy.TotalValueFormat.FormatString = "{0:#,0.##}";
            this.pivotGridField2_DauKy.TotalValueFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.pivotGridField2_DauKy.ValueFormat.FormatString = "{0:#,0.##}";
            this.pivotGridField2_DauKy.ValueFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.pivotGridField2_DauKy.Width = 67;
            // 
            // pivotGridField6_CuoiKy
            // 
            this.pivotGridField6_CuoiKy.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.pivotGridField6_CuoiKy.AreaIndex = 1;
            this.pivotGridField6_CuoiKy.Caption = "Cuối kì";
            this.pivotGridField6_CuoiKy.CellFormat.FormatString = "{0:#,0.##}";
            this.pivotGridField6_CuoiKy.CellFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.pivotGridField6_CuoiKy.FieldName = "ChiSoMoi";
            this.pivotGridField6_CuoiKy.GrandTotalCellFormat.FormatString = "{0:#,0.##}";
            this.pivotGridField6_CuoiKy.GrandTotalCellFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.pivotGridField6_CuoiKy.Name = "pivotGridField6_CuoiKy";
            this.pivotGridField6_CuoiKy.TotalCellFormat.FormatString = "{0:#,0.##}";
            this.pivotGridField6_CuoiKy.TotalCellFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.pivotGridField6_CuoiKy.TotalValueFormat.FormatString = "{0:#,0.##}";
            this.pivotGridField6_CuoiKy.TotalValueFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.pivotGridField6_CuoiKy.ValueFormat.FormatString = "{0:#,0.##}";
            this.pivotGridField6_CuoiKy.ValueFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.pivotGridField6_CuoiKy.Width = 62;
            // 
            // pivotGridField10
            // 
            this.pivotGridField10.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.pivotGridField10.AreaIndex = 2;
            this.pivotGridField10.Caption = "Tiêu thụ";
            this.pivotGridField10.CellFormat.FormatString = "{0:#,0.##}";
            this.pivotGridField10.CellFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.pivotGridField10.FieldName = "SoTieuThu";
            this.pivotGridField10.GrandTotalCellFormat.FormatString = "{0:#,0.##}";
            this.pivotGridField10.GrandTotalCellFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.pivotGridField10.Name = "pivotGridField10";
            this.pivotGridField10.TotalCellFormat.FormatString = "{0:#,0.##}";
            this.pivotGridField10.TotalCellFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.pivotGridField10.TotalValueFormat.FormatString = "{0:#,0.##}";
            this.pivotGridField10.TotalValueFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.pivotGridField10.ValueFormat.FormatString = "{0:#,0.##}";
            this.pivotGridField10.ValueFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.pivotGridField10.Width = 69;
            // 
            // pivotGridField7_ThanhTien
            // 
            this.pivotGridField7_ThanhTien.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.pivotGridField7_ThanhTien.AreaIndex = 3;
            this.pivotGridField7_ThanhTien.Caption = "Thành tiền";
            this.pivotGridField7_ThanhTien.CellFormat.FormatString = "{0:#,0.##}";
            this.pivotGridField7_ThanhTien.CellFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.pivotGridField7_ThanhTien.CustomTotals.AddRange(new DevExpress.XtraPivotGrid.PivotGridCustomTotal[] {
            pivotGridCustomTotal1});
            this.pivotGridField7_ThanhTien.FieldName = "PhaiThu";
            this.pivotGridField7_ThanhTien.GrandTotalCellFormat.FormatString = "{0:#,0.##}";
            this.pivotGridField7_ThanhTien.GrandTotalCellFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.pivotGridField7_ThanhTien.Name = "pivotGridField7_ThanhTien";
            this.pivotGridField7_ThanhTien.TotalCellFormat.FormatString = "{0:#,0.##}";
            this.pivotGridField7_ThanhTien.TotalCellFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.pivotGridField7_ThanhTien.TotalValueFormat.FormatString = "{0:#,0.##}";
            this.pivotGridField7_ThanhTien.TotalValueFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.pivotGridField7_ThanhTien.ValueFormat.FormatString = "{0:#,0.##}";
            this.pivotGridField7_ThanhTien.ValueFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.pivotGridField7_ThanhTien.Width = 91;
            // 
            // pivotGridField8_DaThu
            // 
            this.pivotGridField8_DaThu.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.pivotGridField8_DaThu.AreaIndex = 4;
            this.pivotGridField8_DaThu.Caption = "Đã thu";
            this.pivotGridField8_DaThu.CellFormat.FormatString = "{0:#,0.##}";
            this.pivotGridField8_DaThu.CellFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.pivotGridField8_DaThu.FieldName = "DaThu";
            this.pivotGridField8_DaThu.GrandTotalCellFormat.FormatString = "{0:#,0.##}";
            this.pivotGridField8_DaThu.GrandTotalCellFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.pivotGridField8_DaThu.Name = "pivotGridField8_DaThu";
            this.pivotGridField8_DaThu.TotalCellFormat.FormatString = "{0:#,0.##}";
            this.pivotGridField8_DaThu.TotalCellFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.pivotGridField8_DaThu.TotalValueFormat.FormatString = "{0:#,0.##}";
            this.pivotGridField8_DaThu.TotalValueFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.pivotGridField8_DaThu.ValueFormat.FormatString = "{0:#,0.##}";
            this.pivotGridField8_DaThu.ValueFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.pivotGridField8_DaThu.Width = 97;
            // 
            // pivotGridField9_ConNo
            // 
            this.pivotGridField9_ConNo.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.pivotGridField9_ConNo.AreaIndex = 5;
            this.pivotGridField9_ConNo.Caption = "Còn nợ";
            this.pivotGridField9_ConNo.CellFormat.FormatString = "{0:#,0.##}";
            this.pivotGridField9_ConNo.CellFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.pivotGridField9_ConNo.FieldName = "ConNo";
            this.pivotGridField9_ConNo.GrandTotalCellFormat.FormatString = "{0:#,0.##}";
            this.pivotGridField9_ConNo.GrandTotalCellFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.pivotGridField9_ConNo.Name = "pivotGridField9_ConNo";
            this.pivotGridField9_ConNo.TotalCellFormat.FormatString = "{0:#,0.##}";
            this.pivotGridField9_ConNo.TotalCellFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.pivotGridField9_ConNo.TotalValueFormat.FormatString = "{0:#,0.##}";
            this.pivotGridField9_ConNo.TotalValueFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.pivotGridField9_ConNo.ValueFormat.FormatString = "{0:#,0.##}";
            this.pivotGridField9_ConNo.ValueFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.pivotGridField9_ConNo.Width = 94;
            // 
            // pivotGridField12
            // 
            this.pivotGridField12.Appearance.Value.Options.UseTextOptions = true;
            this.pivotGridField12.Appearance.Value.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.pivotGridField12.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea;
            this.pivotGridField12.AreaIndex = 0;
            this.pivotGridField12.Caption = "Năm";
            this.pivotGridField12.FieldName = "NgayTB";
            this.pivotGridField12.GroupInterval = DevExpress.XtraPivotGrid.PivotGroupInterval.DateYear;
            this.pivotGridField12.Name = "pivotGridField12";
            this.pivotGridField12.UnboundFieldName = "pivotGridField12";
            // 
            // pivotGridField11
            // 
            this.pivotGridField11.Appearance.Value.Options.UseTextOptions = true;
            this.pivotGridField11.Appearance.Value.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.pivotGridField11.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea;
            this.pivotGridField11.AreaIndex = 1;
            this.pivotGridField11.Caption = "Tháng";
            this.pivotGridField11.CellFormat.FormatString = "Tháng {0:MM}";
            this.pivotGridField11.CellFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.pivotGridField11.FieldName = "NgayTB";
            this.pivotGridField11.GroupInterval = DevExpress.XtraPivotGrid.PivotGroupInterval.DateMonth;
            this.pivotGridField11.Name = "pivotGridField11";
            this.pivotGridField11.UnboundFieldName = "pivotGridField11";
            this.pivotGridField11.ValueFormat.FormatString = "Tháng {0:MM}";
            this.pivotGridField11.ValueFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            // 
            // pivotGridField6
            // 
            this.pivotGridField6.AreaIndex = 1;
            this.pivotGridField6.Caption = "Mã phụ";
            this.pivotGridField6.FieldName = "MaPhu";
            this.pivotGridField6.Name = "pivotGridField6";
            // 
            // frmBaoCaoThuChiTienDien
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1022, 431);
            this.Controls.Add(this.pvHoaDon);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "frmBaoCaoThuChiTienDien";
            this.Text = "Báo cáo tiền điện";
            this.Load += new System.EventHandler(this.frmBaoCaoThuChiTienDien_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkToaNha)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbbKyBC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTuNgay.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTuNgay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDenNgay.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDenNgay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pvHoaDon)).EndInit();
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
        private DevExpress.XtraBars.BarEditItem itemToaNha;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lkToaNha;
        private DevExpress.XtraBars.BarButtonItem itemThuTien;
        private DevExpress.XtraPivotGrid.PivotGridControl pvHoaDon;
        private DevExpress.XtraPivotGrid.PivotGridField pivotGridField1;
        private DevExpress.XtraPivotGrid.PivotGridField pivotGridField3;
        private DevExpress.XtraPivotGrid.PivotGridField pivotGridField4;
        private DevExpress.XtraPivotGrid.PivotGridField pivotGridField5;
        private DevExpress.XtraPivotGrid.PivotGridField pivotGridField7_ThanhTien;
        private DevExpress.XtraPivotGrid.PivotGridField pivotGridField8_DaThu;
        private DevExpress.XtraPivotGrid.PivotGridField pivotGridField9_ConNo;
        private DevExpress.XtraBars.BarButtonItem itemExport;
        private DevExpress.XtraPivotGrid.PivotGridField pivotGridField2_DauKy;
        private DevExpress.XtraPivotGrid.PivotGridField pivotGridField6_CuoiKy;
        private DevExpress.XtraPivotGrid.PivotGridField pivotGridField10;
        private DevExpress.XtraPivotGrid.PivotGridField pivotGridField12;
        private DevExpress.XtraPivotGrid.PivotGridField pivotGridField11;
        private DevExpress.XtraPivotGrid.PivotGridField pivotGridField2;
        private DevExpress.XtraPivotGrid.PivotGridField pivotGridField6;
    }
}