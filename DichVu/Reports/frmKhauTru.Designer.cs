﻿namespace DichVu.Reports
{
    partial class frmKhauTru
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmKhauTru));
            DevExpress.XtraPivotGrid.PivotGridStyleFormatCondition pivotGridStyleFormatCondition1 = new DevExpress.XtraPivotGrid.PivotGridStyleFormatCondition();
            DevExpress.XtraPivotGrid.PivotGridCustomTotal pivotGridCustomTotal1 = new DevExpress.XtraPivotGrid.PivotGridCustomTotal();
            this.colTenLDV = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pivotGridField2 = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pivotGridField1 = new DevExpress.XtraPivotGrid.PivotGridField();
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
            this.pvData = new DevExpress.XtraPivotGrid.PivotGridControl();
            this.pivotGridField8 = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pivotGridField7 = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pivotGridField11 = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pivotGridField12 = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pivotGridField9 = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pivotGridField3 = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pivotGridField13 = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pivotGridField4 = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pivotGridField5 = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pivotGridField10 = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pivotGridField14 = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pivotGridField15 = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pivotGridField16 = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pivotGridField6 = new DevExpress.XtraPivotGrid.PivotGridField();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckbToaNha)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbKyBaoCao)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit2.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pvData)).BeginInit();
            this.SuspendLayout();
            // 
            // colTenLDV
            // 
            this.colTenLDV.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.colTenLDV.AreaIndex = 0;
            this.colTenLDV.Caption = "Loại dịch vụ";
            this.colTenLDV.FieldName = "TenLDV";
            this.colTenLDV.GrandTotalText = "Tổng cộng";
            this.colTenLDV.MinWidth = 23;
            this.colTenLDV.Name = "colTenLDV";
            this.colTenLDV.Width = 87;
            // 
            // pivotGridField2
            // 
            this.pivotGridField2.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.pivotGridField2.AreaIndex = 3;
            this.pivotGridField2.Caption = "Mã khách hàng";
            this.pivotGridField2.FieldName = "KyHieu";
            this.pivotGridField2.GrandTotalText = "Tổng cộng";
            this.pivotGridField2.MinWidth = 23;
            this.pivotGridField2.Name = "pivotGridField2";
            this.pivotGridField2.Width = 117;
            // 
            // pivotGridField1
            // 
            this.pivotGridField1.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.pivotGridField1.AreaIndex = 4;
            this.pivotGridField1.Caption = "Tên khách hàng";
            this.pivotGridField1.FieldName = "TenKH";
            this.pivotGridField1.GrandTotalText = "Tổng cộng";
            this.pivotGridField1.MinWidth = 23;
            this.pivotGridField1.Name = "pivotGridField1";
            this.pivotGridField1.Width = 203;
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
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemToaNha, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemKyBaoCao, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemTuNgay, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemDenNgay, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
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
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.barDockControlTop.Size = new System.Drawing.Size(1357, 37);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 527);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.barDockControlBottom.Size = new System.Drawing.Size(1357, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 37);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 490);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1357, 37);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 490);
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.Images.SetKeyName(0, "icons8_refresh1.png");
            this.imageCollection1.Images.SetKeyName(1, "icons8_print.png");
            // 
            // itemExport
            // 
            this.itemExport.Caption = "Export";
            this.itemExport.Id = 16;
            this.itemExport.ImageOptions.ImageIndex = 3;
            this.itemExport.Name = "itemExport";
            // 
            // pvData
            // 
            this.pvData.Appearance.ColumnHeaderArea.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.pvData.Appearance.ColumnHeaderArea.Options.UseFont = true;
            this.pvData.Appearance.FieldHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.pvData.Appearance.FieldHeader.Options.UseFont = true;
            this.pvData.Appearance.FieldHeader.Options.UseTextOptions = true;
            this.pvData.Appearance.FieldHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.pvData.Appearance.FieldValue.Options.UseTextOptions = true;
            this.pvData.Appearance.FieldValue.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.pvData.Appearance.FieldValueGrandTotal.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.pvData.Appearance.FieldValueGrandTotal.Options.UseFont = true;
            this.pvData.Appearance.HeaderArea.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.pvData.Appearance.HeaderArea.Options.UseFont = true;
            this.pvData.Appearance.TotalCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.pvData.Appearance.TotalCell.Options.UseFont = true;
            this.pvData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pvData.Fields.AddRange(new DevExpress.XtraPivotGrid.PivotGridField[] {
            this.colTenLDV,
            this.pivotGridField8,
            this.pivotGridField7,
            this.pivotGridField2,
            this.pivotGridField1,
            this.pivotGridField11,
            this.pivotGridField12,
            this.pivotGridField9,
            this.pivotGridField3,
            this.pivotGridField13,
            this.pivotGridField4,
            this.pivotGridField5,
            this.pivotGridField10,
            this.pivotGridField14,
            this.pivotGridField15,
            this.pivotGridField16,
            this.pivotGridField6});
            pivotGridStyleFormatCondition1.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            pivotGridStyleFormatCondition1.Appearance.Options.UseFont = true;
            pivotGridStyleFormatCondition1.Field = this.colTenLDV;
            pivotGridStyleFormatCondition1.FieldName = "colTenLDV";
            this.pvData.FormatConditions.AddRange(new DevExpress.XtraPivotGrid.PivotGridStyleFormatCondition[] {
            pivotGridStyleFormatCondition1});
            this.pvData.Location = new System.Drawing.Point(0, 37);
            this.pvData.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pvData.Name = "pvData";
            this.pvData.Size = new System.Drawing.Size(1357, 490);
            this.pvData.TabIndex = 19;
            this.pvData.FieldValueDisplayText += new DevExpress.XtraPivotGrid.PivotFieldDisplayTextEventHandler(this.pvData_FieldValueDisplayText);
            // 
            // pivotGridField8
            // 
            this.pivotGridField8.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.pivotGridField8.AreaIndex = 1;
            this.pivotGridField8.Caption = "Ngày CT";
            this.pivotGridField8.CellFormat.FormatString = "dd/MM/yyyy";
            this.pivotGridField8.CellFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.pivotGridField8.FieldName = "NgayCT";
            this.pivotGridField8.MinWidth = 23;
            this.pivotGridField8.Name = "pivotGridField8";
            this.pivotGridField8.SortOrder = DevExpress.XtraPivotGrid.PivotSortOrder.Descending;
            this.pivotGridField8.ValueFormat.FormatString = "dd/MM/yyyy";
            this.pivotGridField8.ValueFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.pivotGridField8.Width = 117;
            // 
            // pivotGridField7
            // 
            this.pivotGridField7.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.pivotGridField7.AreaIndex = 2;
            this.pivotGridField7.Caption = "Số phiếu";
            this.pivotGridField7.FieldName = "SoCT";
            this.pivotGridField7.MinWidth = 23;
            this.pivotGridField7.Name = "pivotGridField7";
            this.pivotGridField7.Width = 117;
            // 
            // pivotGridField11
            // 
            this.pivotGridField11.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.pivotGridField11.AreaIndex = 5;
            this.pivotGridField11.Caption = "Diễn giải";
            pivotGridCustomTotal1.SummaryType = DevExpress.Data.PivotGrid.PivotSummaryType.Max;
            this.pivotGridField11.CustomTotals.AddRange(new DevExpress.XtraPivotGrid.PivotGridCustomTotal[] {
            pivotGridCustomTotal1});
            this.pivotGridField11.FieldName = "DienGiai";
            this.pivotGridField11.MinWidth = 23;
            this.pivotGridField11.Name = "pivotGridField11";
            this.pivotGridField11.Width = 233;
            // 
            // pivotGridField12
            // 
            this.pivotGridField12.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.pivotGridField12.AreaIndex = 6;
            this.pivotGridField12.Caption = "Người khấu trừ";
            this.pivotGridField12.FieldName = "HoTenNV";
            this.pivotGridField12.MinWidth = 23;
            this.pivotGridField12.Name = "pivotGridField12";
            this.pivotGridField12.Width = 117;
            // 
            // pivotGridField9
            // 
            this.pivotGridField9.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.pivotGridField9.AreaIndex = 0;
            this.pivotGridField9.Caption = "Số tiền";
            this.pivotGridField9.CellFormat.FormatString = "{0:#,0.##;(#,0.##)}";
            this.pivotGridField9.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.pivotGridField9.FieldName = "SoTien";
            this.pivotGridField9.GrandTotalText = "Tổng cộng";
            this.pivotGridField9.MinWidth = 23;
            this.pivotGridField9.Name = "pivotGridField9";
            this.pivotGridField9.TotalsVisibility = DevExpress.XtraPivotGrid.PivotTotalsVisibility.None;
            this.pivotGridField9.Width = 114;
            // 
            // pivotGridField3
            // 
            this.pivotGridField3.AreaIndex = 4;
            this.pivotGridField3.Caption = "Mặt bằng";
            this.pivotGridField3.FieldName = "MaSoMB";
            this.pivotGridField3.GrandTotalText = "Tổng cộng";
            this.pivotGridField3.MinWidth = 23;
            this.pivotGridField3.Name = "pivotGridField3";
            this.pivotGridField3.Width = 117;
            // 
            // pivotGridField13
            // 
            this.pivotGridField13.AreaIndex = 1;
            this.pivotGridField13.Caption = "Loại mặt bằng";
            this.pivotGridField13.FieldName = "TenLMB";
            this.pivotGridField13.GrandTotalText = "Tổng cộng";
            this.pivotGridField13.MinWidth = 23;
            this.pivotGridField13.Name = "pivotGridField13";
            this.pivotGridField13.Width = 117;
            // 
            // pivotGridField4
            // 
            this.pivotGridField4.AreaIndex = 3;
            this.pivotGridField4.Caption = "Tầng";
            this.pivotGridField4.FieldName = "TenTL";
            this.pivotGridField4.GrandTotalText = "Tổng cộng";
            this.pivotGridField4.MinWidth = 23;
            this.pivotGridField4.Name = "pivotGridField4";
            this.pivotGridField4.Width = 117;
            // 
            // pivotGridField5
            // 
            this.pivotGridField5.AreaIndex = 2;
            this.pivotGridField5.Caption = "Khối nhà";
            this.pivotGridField5.FieldName = "TenKN";
            this.pivotGridField5.GrandTotalText = "Tổng cộng";
            this.pivotGridField5.MinWidth = 23;
            this.pivotGridField5.Name = "pivotGridField5";
            this.pivotGridField5.Width = 117;
            // 
            // pivotGridField10
            // 
            this.pivotGridField10.AreaIndex = 0;
            this.pivotGridField10.Caption = "Dự án";
            this.pivotGridField10.FieldName = "TenTN";
            this.pivotGridField10.GrandTotalText = "Tổng cộng";
            this.pivotGridField10.MinWidth = 23;
            this.pivotGridField10.Name = "pivotGridField10";
            this.pivotGridField10.Width = 117;
            // 
            // pivotGridField14
            // 
            this.pivotGridField14.AreaIndex = 5;
            this.pivotGridField14.Caption = "Năm";
            this.pivotGridField14.FieldName = "NgayCT";
            this.pivotGridField14.GroupInterval = DevExpress.XtraPivotGrid.PivotGroupInterval.DateYear;
            this.pivotGridField14.MinWidth = 23;
            this.pivotGridField14.Name = "pivotGridField14";
            this.pivotGridField14.UnboundFieldName = "pivotGridField14";
            this.pivotGridField14.Width = 117;
            // 
            // pivotGridField15
            // 
            this.pivotGridField15.AreaIndex = 6;
            this.pivotGridField15.Caption = "Tháng";
            this.pivotGridField15.CellFormat.FormatString = "Tháng {0:MM}";
            this.pivotGridField15.CellFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.pivotGridField15.FieldName = "NgayCT";
            this.pivotGridField15.GroupInterval = DevExpress.XtraPivotGrid.PivotGroupInterval.DateMonth;
            this.pivotGridField15.MinWidth = 23;
            this.pivotGridField15.Name = "pivotGridField15";
            this.pivotGridField15.UnboundFieldName = "pivotGridField15";
            this.pivotGridField15.ValueFormat.FormatString = "Tháng {0:MM}";
            this.pivotGridField15.ValueFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.pivotGridField15.Width = 117;
            // 
            // pivotGridField16
            // 
            this.pivotGridField16.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.pivotGridField16.AreaIndex = 7;
            this.pivotGridField16.Caption = "Hình thức";
            this.pivotGridField16.FieldName = "TenHT";
            this.pivotGridField16.MinWidth = 23;
            this.pivotGridField16.Name = "pivotGridField16";
            this.pivotGridField16.Visible = false;
            this.pivotGridField16.Width = 117;
            // 
            // pivotGridField6
            // 
            this.pivotGridField6.AreaIndex = 7;
            this.pivotGridField6.Caption = "Mã phụ";
            this.pivotGridField6.FieldName = "MaPhu";
            this.pivotGridField6.MinWidth = 23;
            this.pivotGridField6.Name = "pivotGridField6";
            this.pivotGridField6.Width = 117;
            // 
            // frmKhauTru
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1357, 527);
            this.Controls.Add(this.pvData);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmKhauTru";
            this.Text = "BC các khoản đã khấu trừ";
            this.Load += new System.EventHandler(this.frmKhauTru_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckbToaNha)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbKyBaoCao)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit2.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pvData)).EndInit();
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
        private DevExpress.XtraBars.BarEditItem itemToaNha;
        private DevExpress.XtraPivotGrid.PivotGridControl pvData;
        private DevExpress.XtraPivotGrid.PivotGridField pivotGridField2;
        private DevExpress.XtraPivotGrid.PivotGridField pivotGridField1;
        private DevExpress.XtraPivotGrid.PivotGridField pivotGridField3;
        private DevExpress.XtraPivotGrid.PivotGridField pivotGridField4;
        private DevExpress.XtraPivotGrid.PivotGridField pivotGridField5;
        private DevExpress.XtraPivotGrid.PivotGridField colTenLDV;
        private DevExpress.XtraPivotGrid.PivotGridField pivotGridField9;
        private DevExpress.XtraBars.BarButtonItem itemExport;
        private DevExpress.XtraPivotGrid.PivotGridField pivotGridField10;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckedComboBoxEdit ckbToaNha;
        private DevExpress.XtraPivotGrid.PivotGridField pivotGridField13;
        private DevExpress.XtraBars.BarButtonItem itemPrint;
        private DevExpress.XtraBars.BarEditItem itemKyBaoCao;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox cmbKyBaoCao;
        private DevExpress.XtraBars.BarEditItem itemTuNgay;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit repositoryItemDateEdit1;
        private DevExpress.XtraBars.BarEditItem itemDenNgay;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit repositoryItemDateEdit2;
        private DevExpress.XtraPivotGrid.PivotGridField pivotGridField7;
        private DevExpress.XtraPivotGrid.PivotGridField pivotGridField8;
        private DevExpress.XtraPivotGrid.PivotGridField pivotGridField11;
        private DevExpress.XtraPivotGrid.PivotGridField pivotGridField12;
        private DevExpress.XtraPivotGrid.PivotGridField pivotGridField14;
        private DevExpress.XtraPivotGrid.PivotGridField pivotGridField15;
        private DevExpress.XtraPivotGrid.PivotGridField pivotGridField16;
        private DevExpress.XtraPivotGrid.PivotGridField pivotGridField6;
    }
}