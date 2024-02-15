namespace LandSoftBuilding.Lease.Reports
{
    partial class frmCongNoDatCoc
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCongNoDatCoc));
            this.pvDatCoc = new DevExpress.XtraPivotGrid.PivotGridControl();
            this.pivotGridField1 = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pivotGridField2 = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pivotGridField5 = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pivotGridField4 = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pivotGridField6 = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pivotGridField7 = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pivotGridField8 = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pivotGridField15 = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pivotGridField3 = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pivotGridField9 = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pivotGridField10 = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pivotGridField11 = new DevExpress.XtraPivotGrid.PivotGridField();
            this.repositoryItemComboBox2 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.barManager1 = new DevExpress.XtraBars.BarManager();
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.itemToaNha = new DevExpress.XtraBars.BarEditItem();
            this.ckbToaNha = new DevExpress.XtraEditors.Repository.RepositoryItemCheckedComboBoxEdit();
            this.itemKyBaoCao = new DevExpress.XtraBars.BarEditItem();
            this.cmbKyBaoCao = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.itemTuNgay = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemDateEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.itemDenNgay = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemDateEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.btnNap = new DevExpress.XtraBars.BarButtonItem();
            this.itemPrint = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection();
            this.itemExport = new DevExpress.XtraBars.BarButtonItem();
            ((System.ComponentModel.ISupportInitialize)(this.pvDatCoc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckbToaNha)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbKyBaoCao)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit2.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            this.SuspendLayout();
            // 
            // pvDatCoc
            // 
            this.pvDatCoc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pvDatCoc.Fields.AddRange(new DevExpress.XtraPivotGrid.PivotGridField[] {
            this.pivotGridField1,
            this.pivotGridField2,
            this.pivotGridField5,
            this.pivotGridField4,
            this.pivotGridField6,
            this.pivotGridField7,
            this.pivotGridField8,
            this.pivotGridField15,
            this.pivotGridField3,
            this.pivotGridField9,
            this.pivotGridField10,
            this.pivotGridField11});
            this.pvDatCoc.Location = new System.Drawing.Point(0, 31);
            this.pvDatCoc.Name = "pvDatCoc";
            this.pvDatCoc.Size = new System.Drawing.Size(1093, 351);
            this.pvDatCoc.TabIndex = 4;
            // 
            // pivotGridField1
            // 
            this.pivotGridField1.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.pivotGridField1.AreaIndex = 0;
            this.pivotGridField1.Caption = "Mã Khách Hàng";
            this.pivotGridField1.FieldName = "KyHieu";
            this.pivotGridField1.Name = "pivotGridField1";
            // 
            // pivotGridField2
            // 
            this.pivotGridField2.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.pivotGridField2.AreaIndex = 1;
            this.pivotGridField2.Caption = "Tên Khách Hàng";
            this.pivotGridField2.FieldName = "TenKH";
            this.pivotGridField2.Name = "pivotGridField2";
            // 
            // pivotGridField5
            // 
            this.pivotGridField5.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.pivotGridField5.AreaIndex = 0;
            this.pivotGridField5.Caption = "Đầu kỳ";
            this.pivotGridField5.CellFormat.FormatString = "n0";
            this.pivotGridField5.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.pivotGridField5.FieldName = "DauKy";
            this.pivotGridField5.Name = "pivotGridField5";
            this.pivotGridField5.ValueFormat.FormatString = "n0";
            this.pivotGridField5.ValueFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.pivotGridField5.Width = 75;
            // 
            // pivotGridField4
            // 
            this.pivotGridField4.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.pivotGridField4.AreaIndex = 1;
            this.pivotGridField4.Caption = "Phát sinh";
            this.pivotGridField4.CellFormat.FormatString = "n0";
            this.pivotGridField4.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.pivotGridField4.FieldName = "PhatSinh";
            this.pivotGridField4.Name = "pivotGridField4";
            this.pivotGridField4.ValueFormat.FormatString = "n0";
            this.pivotGridField4.ValueFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.pivotGridField4.Width = 81;
            // 
            // pivotGridField6
            // 
            this.pivotGridField6.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.pivotGridField6.AreaIndex = 2;
            this.pivotGridField6.Caption = "Đã thu";
            this.pivotGridField6.CellFormat.FormatString = "n0";
            this.pivotGridField6.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.pivotGridField6.FieldName = "DaThu";
            this.pivotGridField6.Name = "pivotGridField6";
            this.pivotGridField6.ValueFormat.FormatString = "n0";
            this.pivotGridField6.ValueFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.pivotGridField6.Width = 84;
            // 
            // pivotGridField7
            // 
            this.pivotGridField7.AreaIndex = 1;
            this.pivotGridField7.Caption = "Khấu trừ";
            this.pivotGridField7.CellFormat.FormatString = "n0";
            this.pivotGridField7.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.pivotGridField7.FieldName = "KhauTru";
            this.pivotGridField7.Name = "pivotGridField7";
            this.pivotGridField7.ValueFormat.FormatString = "n0";
            this.pivotGridField7.ValueFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.pivotGridField7.Width = 81;
            // 
            // pivotGridField8
            // 
            this.pivotGridField8.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.pivotGridField8.AreaIndex = 3;
            this.pivotGridField8.Caption = "Cuối kỳ";
            this.pivotGridField8.CellFormat.FormatString = "n0";
            this.pivotGridField8.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.pivotGridField8.FieldName = "CuoiKy";
            this.pivotGridField8.Name = "pivotGridField8";
            this.pivotGridField8.ValueFormat.FormatString = "n0";
            this.pivotGridField8.ValueFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.pivotGridField8.Width = 83;
            // 
            // pivotGridField15
            // 
            this.pivotGridField15.AreaIndex = 0;
            this.pivotGridField15.Caption = "Dự án";
            this.pivotGridField15.FieldName = "TenTN";
            this.pivotGridField15.Name = "pivotGridField15";
            // 
            // pivotGridField3
            // 
            this.pivotGridField3.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.pivotGridField3.AreaIndex = 2;
            this.pivotGridField3.Caption = "Số hợp đồng";
            this.pivotGridField3.FieldName = "SoHD";
            this.pivotGridField3.Name = "pivotGridField3";
            // 
            // pivotGridField9
            // 
            this.pivotGridField9.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.pivotGridField9.AreaIndex = 3;
            this.pivotGridField9.Caption = "Ngày ký";
            this.pivotGridField9.CellFormat.FormatString = "dd/MM/yyyy";
            this.pivotGridField9.CellFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.pivotGridField9.FieldName = "NgayKy";
            this.pivotGridField9.Name = "pivotGridField9";
            this.pivotGridField9.ValueFormat.FormatString = "dd/MM/yyyy";
            this.pivotGridField9.ValueFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            // 
            // pivotGridField10
            // 
            this.pivotGridField10.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.pivotGridField10.AreaIndex = 4;
            this.pivotGridField10.Caption = "Ngày HL";
            this.pivotGridField10.CellFormat.FormatString = "dd/MM/yyyy";
            this.pivotGridField10.CellFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.pivotGridField10.FieldName = "NgayHL";
            this.pivotGridField10.Name = "pivotGridField10";
            this.pivotGridField10.ValueFormat.FormatString = "dd/MM/yyyy";
            this.pivotGridField10.ValueFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            // 
            // pivotGridField11
            // 
            this.pivotGridField11.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.pivotGridField11.AreaIndex = 5;
            this.pivotGridField11.Caption = "Ngày HH";
            this.pivotGridField11.CellFormat.FormatString = "dd/MM/yyyy";
            this.pivotGridField11.CellFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.pivotGridField11.FieldName = "NgayHH";
            this.pivotGridField11.Name = "pivotGridField11";
            this.pivotGridField11.ValueFormat.FormatString = "dd/MM/yyyy";
            this.pivotGridField11.ValueFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            // 
            // repositoryItemComboBox2
            // 
            this.repositoryItemComboBox2.AutoHeight = false;
            this.repositoryItemComboBox2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBox2.Name = "repositoryItemComboBox2";
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
            this.itemToaNha,
            this.itemExport,
            this.itemPrint,
            this.itemKyBaoCao,
            this.itemTuNgay,
            this.itemDenNgay});
            this.barManager1.MaxItemId = 23;
            this.barManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.ckbToaNha,
            this.cmbKyBaoCao,
            this.repositoryItemDateEdit1,
            this.repositoryItemDateEdit2});
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(((DevExpress.XtraBars.BarLinkUserDefines)((DevExpress.XtraBars.BarLinkUserDefines.PaintStyle | DevExpress.XtraBars.BarLinkUserDefines.Width))), this.itemToaNha, "", false, true, true, 205, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(((DevExpress.XtraBars.BarLinkUserDefines)((DevExpress.XtraBars.BarLinkUserDefines.PaintStyle | DevExpress.XtraBars.BarLinkUserDefines.Width))), this.itemKyBaoCao, "", false, true, true, 155, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(((DevExpress.XtraBars.BarLinkUserDefines)((DevExpress.XtraBars.BarLinkUserDefines.PaintStyle | DevExpress.XtraBars.BarLinkUserDefines.Width))), this.itemTuNgay, "", false, true, true, 137, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(((DevExpress.XtraBars.BarLinkUserDefines)((DevExpress.XtraBars.BarLinkUserDefines.PaintStyle | DevExpress.XtraBars.BarLinkUserDefines.Width))), this.itemDenNgay, "", false, true, true, 132, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnNap, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemPrint, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
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
            this.itemKyBaoCao.EditWidth = 100;
            this.itemKyBaoCao.Id = 20;
            this.itemKyBaoCao.Name = "itemKyBaoCao";
            // 
            // cmbKyBaoCao
            // 
            this.cmbKyBaoCao.AutoHeight = false;
            this.cmbKyBaoCao.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbKyBaoCao.Name = "cmbKyBaoCao";
            this.cmbKyBaoCao.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbKyBaoCao.EditValueChanged += new System.EventHandler(this.cmbKyBaoCao_EditValueChanged);
            // 
            // itemTuNgay
            // 
            this.itemTuNgay.Caption = "Từ ngày";
            this.itemTuNgay.Edit = this.repositoryItemDateEdit1;
            this.itemTuNgay.EditWidth = 80;
            this.itemTuNgay.Id = 21;
            this.itemTuNgay.Name = "itemTuNgay";
            // 
            // repositoryItemDateEdit1
            // 
            this.repositoryItemDateEdit1.AutoHeight = false;
            this.repositoryItemDateEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemDateEdit1.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.repositoryItemDateEdit1.Name = "repositoryItemDateEdit1";
            // 
            // itemDenNgay
            // 
            this.itemDenNgay.Caption = "Đến ngày";
            this.itemDenNgay.Edit = this.repositoryItemDateEdit2;
            this.itemDenNgay.EditWidth = 80;
            this.itemDenNgay.Id = 22;
            this.itemDenNgay.Name = "itemDenNgay";
            // 
            // repositoryItemDateEdit2
            // 
            this.repositoryItemDateEdit2.AutoHeight = false;
            this.repositoryItemDateEdit2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemDateEdit2.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.repositoryItemDateEdit2.Name = "repositoryItemDateEdit2";
            // 
            // btnNap
            // 
            this.btnNap.Caption = "Nạp";
            this.btnNap.Id = 0;
            this.btnNap.ImageOptions.ImageIndex = 0;
            this.btnNap.Name = "btnNap";
            this.btnNap.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemNap_ItemClick);
            // 
            // itemPrint
            // 
            this.itemPrint.Caption = "In";
            this.itemPrint.Id = 19;
            this.itemPrint.ImageOptions.ImageIndex = 1;
            this.itemPrint.Name = "itemPrint";
            this.itemPrint.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemPrint_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(1093, 31);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 382);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(1093, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 31);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 351);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1093, 31);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 351);
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.Images.SetKeyName(0, "Refresh1.png");
            this.imageCollection1.Images.SetKeyName(1, "Print1.png");
            // 
            // itemExport
            // 
            this.itemExport.Caption = "Export";
            this.itemExport.Id = 16;
            this.itemExport.ImageOptions.ImageIndex = 3;
            this.itemExport.Name = "itemExport";
            // 
            // frmCongNoDatCoc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1093, 382);
            this.Controls.Add(this.pvDatCoc);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "frmCongNoDatCoc";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sổ công nợ đặt cọc";
            this.Load += new System.EventHandler(this.frmCongNoTheXe_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pvDatCoc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckbToaNha)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbKyBaoCao)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit2.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraPivotGrid.PivotGridControl pvDatCoc;
        private DevExpress.XtraPivotGrid.PivotGridField pivotGridField1;
        private DevExpress.XtraPivotGrid.PivotGridField pivotGridField2;
        private DevExpress.XtraPivotGrid.PivotGridField pivotGridField5;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBox2;
        private DevExpress.XtraPivotGrid.PivotGridField pivotGridField4;
        private DevExpress.XtraPivotGrid.PivotGridField pivotGridField6;
        private DevExpress.XtraPivotGrid.PivotGridField pivotGridField7;
        private DevExpress.XtraPivotGrid.PivotGridField pivotGridField8;
        private DevExpress.XtraPivotGrid.PivotGridField pivotGridField15;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarEditItem itemToaNha;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckedComboBoxEdit ckbToaNha;
        private DevExpress.XtraBars.BarEditItem itemKyBaoCao;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox cmbKyBaoCao;
        private DevExpress.XtraBars.BarEditItem itemTuNgay;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit repositoryItemDateEdit1;
        private DevExpress.XtraBars.BarEditItem itemDenNgay;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit repositoryItemDateEdit2;
        private DevExpress.XtraBars.BarButtonItem btnNap;
        private DevExpress.XtraBars.BarButtonItem itemPrint;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.Utils.ImageCollection imageCollection1;
        private DevExpress.XtraBars.BarButtonItem itemExport;
        private DevExpress.XtraPivotGrid.PivotGridField pivotGridField3;
        private DevExpress.XtraPivotGrid.PivotGridField pivotGridField9;
        private DevExpress.XtraPivotGrid.PivotGridField pivotGridField10;
        private DevExpress.XtraPivotGrid.PivotGridField pivotGridField11;
    }
}