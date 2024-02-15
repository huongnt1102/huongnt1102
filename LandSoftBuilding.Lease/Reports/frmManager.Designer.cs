namespace LandSoftBuilding.Lease.Reports
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
            this.ckbToaNha = new DevExpress.XtraEditors.Repository.RepositoryItemCheckedComboBoxEdit();
            this.itemKyBaoCao = new DevExpress.XtraBars.BarEditItem();
            this.cmbKyBaoCao = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.itemTuNgay = new DevExpress.XtraBars.BarEditItem();
            this.dateTuNgay = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.itemDenNgay = new DevExpress.XtraBars.BarEditItem();
            this.dateDenNgay = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.btnNap = new DevExpress.XtraBars.BarButtonItem();
            this.itemPrint = new DevExpress.XtraBars.BarButtonItem();
            this.itemExport = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection();
            this.pivotGridField9 = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pivotGridField8 = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pivotGridField7 = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pivotGridField1 = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pivotGridField2 = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pivotGridField3 = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pivotGridField4 = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pivotGridField5 = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pivotGridField10 = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pivotGridField6 = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pivotGridField12 = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pivotGridField11 = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pvChoThue = new DevExpress.XtraPivotGrid.PivotGridControl();
            this.pivotGridField13 = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pivotGridField14 = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pivotGridField15 = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pivotGridField16 = new DevExpress.XtraPivotGrid.PivotGridField();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckbToaNha)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbKyBaoCao)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTuNgay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTuNgay.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDenNgay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDenNgay.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pvChoThue)).BeginInit();
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
            this.itemDenNgay,
            this.itemToaNha,
            this.itemExport,
            this.itemKyBaoCao,
            this.itemTuNgay,
            this.itemPrint});
            this.barManager1.MaxItemId = 20;
            this.barManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.dateDenNgay,
            this.ckbToaNha,
            this.cmbKyBaoCao,
            this.dateTuNgay});
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemToaNha, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(this.itemKyBaoCao),
            new DevExpress.XtraBars.LinkPersistInfo(this.itemTuNgay),
            new DevExpress.XtraBars.LinkPersistInfo(this.itemDenNgay),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnNap, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemPrint, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemExport, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.DrawDragBorder = false;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Tools";
            // 
            // itemToaNha
            // 
            this.itemToaNha.Caption = "Dự án";
            this.itemToaNha.Edit = this.ckbToaNha;
            this.itemToaNha.EditWidth = 91;
            this.itemToaNha.Id = 14;
            this.itemToaNha.Name = "itemToaNha";
            // 
            // ckbToaNha
            // 
            this.ckbToaNha.AutoHeight = false;
            this.ckbToaNha.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ckbToaNha.DisplayMember = "TenTN";
            this.ckbToaNha.Name = "ckbToaNha";
            this.ckbToaNha.SelectAllItemCaption = "Tất cả";
            this.ckbToaNha.ValueMember = "MaTN";
            // 
            // itemKyBaoCao
            // 
            this.itemKyBaoCao.Caption = "Kỳ báo cáo";
            this.itemKyBaoCao.Edit = this.cmbKyBaoCao;
            this.itemKyBaoCao.EditWidth = 120;
            this.itemKyBaoCao.Id = 17;
            this.itemKyBaoCao.Name = "itemKyBaoCao";
            // 
            // cmbKyBaoCao
            // 
            this.cmbKyBaoCao.AutoHeight = false;
            this.cmbKyBaoCao.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbKyBaoCao.Name = "cmbKyBaoCao";
            this.cmbKyBaoCao.NullText = "Kỳ báo cáo";
            this.cmbKyBaoCao.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbKyBaoCao.EditValueChanged += new System.EventHandler(this.cmbKyBaoCao_EditValueChanged);
            // 
            // itemTuNgay
            // 
            this.itemTuNgay.Caption = "Từ ngày";
            this.itemTuNgay.Edit = this.dateTuNgay;
            this.itemTuNgay.EditWidth = 80;
            this.itemTuNgay.Id = 18;
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
            this.itemDenNgay.EditWidth = 80;
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
            // itemPrint
            // 
            this.itemPrint.Caption = "In";
            this.itemPrint.Id = 19;
            this.itemPrint.ImageOptions.ImageIndex = 1;
            this.itemPrint.Name = "itemPrint";
            this.itemPrint.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemPrint_ItemClick);
            // 
            // itemExport
            // 
            this.itemExport.Caption = "Export";
            this.itemExport.Id = 16;
            this.itemExport.ImageOptions.ImageIndex = 2;
            this.itemExport.Name = "itemExport";
            this.itemExport.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemExport_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(1177, 31);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 415);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(1177, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 31);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 384);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1177, 31);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 384);
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.Images.SetKeyName(0, "Refresh1.png");
            this.imageCollection1.Images.SetKeyName(1, "Print1.png");
            this.imageCollection1.Images.SetKeyName(2, "Export1.png");
            // 
            // pivotGridField9
            // 
            this.pivotGridField9.AreaIndex = 5;
            this.pivotGridField9.Caption = "Còn nợ";
            this.pivotGridField9.CellFormat.FormatString = "{0:#,0.##}";
            this.pivotGridField9.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.pivotGridField9.FieldName = "ConNo";
            this.pivotGridField9.Name = "pivotGridField9";
            this.pivotGridField9.Width = 79;
            // 
            // pivotGridField8
            // 
            this.pivotGridField8.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.pivotGridField8.AreaIndex = 0;
            this.pivotGridField8.Caption = "Đã thu";
            this.pivotGridField8.CellFormat.FormatString = "{0:#,0.##}";
            this.pivotGridField8.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.pivotGridField8.FieldName = "DaThu";
            this.pivotGridField8.Name = "pivotGridField8";
            this.pivotGridField8.Width = 76;
            // 
            // pivotGridField7
            // 
            this.pivotGridField7.AreaIndex = 6;
            this.pivotGridField7.Caption = "Phải thu";
            this.pivotGridField7.CellFormat.FormatString = "{0:#,0.##}";
            this.pivotGridField7.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.pivotGridField7.FieldName = "PhaiThu";
            this.pivotGridField7.Name = "pivotGridField7";
            this.pivotGridField7.Width = 82;
            // 
            // pivotGridField1
            // 
            this.pivotGridField1.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.pivotGridField1.AreaIndex = 1;
            this.pivotGridField1.Caption = "Tên khách hàng";
            this.pivotGridField1.FieldName = "TenKH";
            this.pivotGridField1.Name = "pivotGridField1";
            this.pivotGridField1.Width = 174;
            // 
            // pivotGridField2
            // 
            this.pivotGridField2.AreaIndex = 2;
            this.pivotGridField2.Caption = "Mã khách hàng";
            this.pivotGridField2.FieldName = "KyHieu";
            this.pivotGridField2.Name = "pivotGridField2";
            // 
            // pivotGridField3
            // 
            this.pivotGridField3.AreaIndex = 1;
            this.pivotGridField3.Caption = "Mặt bằng";
            this.pivotGridField3.FieldName = "MaSoMB";
            this.pivotGridField3.Name = "pivotGridField3";
            // 
            // pivotGridField4
            // 
            this.pivotGridField4.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.pivotGridField4.AreaIndex = 2;
            this.pivotGridField4.Caption = "Vị trí";
            this.pivotGridField4.FieldName = "TenTL";
            this.pivotGridField4.Name = "pivotGridField4";
            // 
            // pivotGridField5
            // 
            this.pivotGridField5.AreaIndex = 0;
            this.pivotGridField5.Caption = "Khối nhà";
            this.pivotGridField5.FieldName = "TenKN";
            this.pivotGridField5.Name = "pivotGridField5";
            // 
            // pivotGridField10
            // 
            this.pivotGridField10.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.pivotGridField10.AreaIndex = 0;
            this.pivotGridField10.Caption = "Dự án";
            this.pivotGridField10.FieldName = "TenTN";
            this.pivotGridField10.Name = "pivotGridField10";
            // 
            // pivotGridField6
            // 
            this.pivotGridField6.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea;
            this.pivotGridField6.AreaIndex = 0;
            this.pivotGridField6.Caption = "Loại mặt bằng";
            this.pivotGridField6.FieldName = "TenLMB";
            this.pivotGridField6.Name = "pivotGridField6";
            // 
            // pivotGridField12
            // 
            this.pivotGridField12.AreaIndex = 3;
            this.pivotGridField12.Caption = "Năm";
            this.pivotGridField12.FieldName = "Nam";
            this.pivotGridField12.Name = "pivotGridField12";
            // 
            // pivotGridField11
            // 
            this.pivotGridField11.AreaIndex = 4;
            this.pivotGridField11.Caption = "Tháng";
            this.pivotGridField11.FieldName = "Thang";
            this.pivotGridField11.Name = "pivotGridField11";
            // 
            // pvChoThue
            // 
            this.pvChoThue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pvChoThue.Fields.AddRange(new DevExpress.XtraPivotGrid.PivotGridField[] {
            this.pivotGridField11,
            this.pivotGridField12,
            this.pivotGridField6,
            this.pivotGridField10,
            this.pivotGridField5,
            this.pivotGridField4,
            this.pivotGridField3,
            this.pivotGridField2,
            this.pivotGridField1,
            this.pivotGridField7,
            this.pivotGridField8,
            this.pivotGridField9,
            this.pivotGridField13,
            this.pivotGridField14,
            this.pivotGridField15,
            this.pivotGridField16});
            this.pvChoThue.Location = new System.Drawing.Point(0, 31);
            this.pvChoThue.Name = "pvChoThue";
            this.pvChoThue.Size = new System.Drawing.Size(1177, 384);
            this.pvChoThue.TabIndex = 19;
            // 
            // pivotGridField13
            // 
            this.pivotGridField13.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.pivotGridField13.AreaIndex = 3;
            this.pivotGridField13.Caption = "Diện tích";
            this.pivotGridField13.CellFormat.FormatString = "{0:#,0.##} m2";
            this.pivotGridField13.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.pivotGridField13.FieldName = "DienTich";
            this.pivotGridField13.Name = "pivotGridField13";
            this.pivotGridField13.ValueFormat.FormatString = "{0:#,0.##} m2";
            this.pivotGridField13.ValueFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            // 
            // pivotGridField14
            // 
            this.pivotGridField14.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.pivotGridField14.AreaIndex = 4;
            this.pivotGridField14.Caption = "Số HĐ";
            this.pivotGridField14.FieldName = "SoHDCT";
            this.pivotGridField14.Name = "pivotGridField14";
            // 
            // pivotGridField15
            // 
            this.pivotGridField15.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.pivotGridField15.AreaIndex = 5;
            this.pivotGridField15.Caption = "Ngày ký";
            this.pivotGridField15.FieldName = "NgayKy";
            this.pivotGridField15.Name = "pivotGridField15";
            this.pivotGridField15.ValueFormat.FormatString = "{0:dd/MM/yyyy}";
            this.pivotGridField15.ValueFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            // 
            // pivotGridField16
            // 
            this.pivotGridField16.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.pivotGridField16.AreaIndex = 6;
            this.pivotGridField16.Caption = "Ngày HH";
            this.pivotGridField16.FieldName = "NgayHH";
            this.pivotGridField16.Name = "pivotGridField16";
            this.pivotGridField16.ValueFormat.FormatString = "{0:dd/MM/yyyy}";
            this.pivotGridField16.ValueFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            // 
            // frmManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1177, 415);
            this.Controls.Add(this.pvChoThue);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "frmManager";
            this.Text = "Thông kê cho thuê";
            this.Load += new System.EventHandler(this.frmManager_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckbToaNha)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbKyBaoCao)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTuNgay.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTuNgay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDenNgay.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDenNgay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pvChoThue)).EndInit();
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
        private DevExpress.Utils.ImageCollection imageCollection1;
        private DevExpress.XtraBars.BarEditItem itemDenNgay;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit dateDenNgay;
        private DevExpress.XtraBars.BarEditItem itemToaNha;
        private DevExpress.XtraBars.BarButtonItem itemExport;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckedComboBoxEdit ckbToaNha;
        private DevExpress.XtraPivotGrid.PivotGridControl pvChoThue;
        private DevExpress.XtraPivotGrid.PivotGridField pivotGridField11;
        private DevExpress.XtraPivotGrid.PivotGridField pivotGridField12;
        private DevExpress.XtraPivotGrid.PivotGridField pivotGridField6;
        private DevExpress.XtraPivotGrid.PivotGridField pivotGridField10;
        private DevExpress.XtraPivotGrid.PivotGridField pivotGridField5;
        private DevExpress.XtraPivotGrid.PivotGridField pivotGridField4;
        private DevExpress.XtraPivotGrid.PivotGridField pivotGridField3;
        private DevExpress.XtraPivotGrid.PivotGridField pivotGridField2;
        private DevExpress.XtraPivotGrid.PivotGridField pivotGridField1;
        private DevExpress.XtraPivotGrid.PivotGridField pivotGridField7;
        private DevExpress.XtraPivotGrid.PivotGridField pivotGridField8;
        private DevExpress.XtraPivotGrid.PivotGridField pivotGridField9;
        private DevExpress.XtraBars.BarEditItem itemKyBaoCao;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox cmbKyBaoCao;
        private DevExpress.XtraBars.BarEditItem itemTuNgay;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit dateTuNgay;
        private DevExpress.XtraPivotGrid.PivotGridField pivotGridField13;
        private DevExpress.XtraPivotGrid.PivotGridField pivotGridField14;
        private DevExpress.XtraPivotGrid.PivotGridField pivotGridField15;
        private DevExpress.XtraPivotGrid.PivotGridField pivotGridField16;
        private DevExpress.XtraBars.BarButtonItem itemPrint;
    }
}