namespace Building.Survey.Answers
{
    partial class frmAnswerManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAnswerManager));
            DevExpress.XtraGrid.GridFormatRule gridFormatRule1 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRule2ColorScale formatConditionRule2ColorScale1 = new DevExpress.XtraEditors.FormatConditionRule2ColorScale();
            DevExpress.XtraGrid.GridFormatRule gridFormatRule2 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRule2ColorScale formatConditionRule2ColorScale2 = new DevExpress.XtraEditors.FormatConditionRule2ColorScale();
            DevExpress.XtraGrid.GridFormatRule gridFormatRule3 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRule2ColorScale formatConditionRule2ColorScale3 = new DevExpress.XtraEditors.FormatConditionRule2ColorScale();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
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
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.itemSua = new DevExpress.XtraBars.BarButtonItem();
            this.btnXoa = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
            this.btnSua = new DevExpress.XtraBars.BarButtonItem();
            this.gcSurvey = new DevExpress.XtraGrid.GridControl();
            this.gvSurvey = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkToaNha)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbbKyBC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTuNgay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTuNgay.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDenNgay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDenNgay.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcSurvey)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvSurvey)).BeginInit();
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
            this.btnXoa,
            this.btnSua,
            this.itemKyBC,
            this.itemTuNgay,
            this.itemDenNgay,
            this.itemToaNha,
            this.itemSua,
            this.barButtonItem1});
            this.barManager1.MaxItemId = 68;
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
            new DevExpress.XtraBars.LinkPersistInfo(((DevExpress.XtraBars.BarLinkUserDefines)((DevExpress.XtraBars.BarLinkUserDefines.PaintStyle | DevExpress.XtraBars.BarLinkUserDefines.Width))), this.itemToaNha, "", false, true, true, 168, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(this.itemKyBC),
            new DevExpress.XtraBars.LinkPersistInfo(this.itemTuNgay),
            new DevExpress.XtraBars.LinkPersistInfo(this.itemDenNgay),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnNap, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barButtonItem1, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemSua, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnXoa, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.DrawDragBorder = false;
            this.bar1.OptionsBar.MultiLine = true;
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
            this.btnNap.ImageOptions.ImageIndex = 9;
            this.btnNap.Name = "btnNap";
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "Thêm";
            this.barButtonItem1.Id = 67;
            this.barButtonItem1.ImageOptions.ImageIndex = 1;
            this.barButtonItem1.Name = "barButtonItem1";
            // 
            // itemSua
            // 
            this.itemSua.Caption = "Sửa";
            this.itemSua.Id = 21;
            this.itemSua.ImageOptions.ImageIndex = 2;
            this.itemSua.Name = "itemSua";
            // 
            // btnXoa
            // 
            this.btnXoa.Caption = "Xóa";
            this.btnXoa.Id = 2;
            this.btnXoa.ImageOptions.ImageIndex = 3;
            this.btnXoa.Name = "btnXoa";
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(994, 31);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 428);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(994, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 31);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 397);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(994, 31);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 397);
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.Images.SetKeyName(0, "Refresh1.png");
            this.imageCollection1.Images.SetKeyName(1, "Add1.png");
            this.imageCollection1.Images.SetKeyName(2, "Edit3.png");
            this.imageCollection1.Images.SetKeyName(3, "Delete1.png");
            this.imageCollection1.Images.SetKeyName(4, "DuyetYes.png");
            this.imageCollection1.Images.SetKeyName(5, "TienMat1.png");
            this.imageCollection1.Images.SetKeyName(6, "Import1.png");
            this.imageCollection1.Images.SetKeyName(7, "Export1.png");
            this.imageCollection1.Images.SetKeyName(8, "Tien1.png");
            this.imageCollection1.Images.SetKeyName(9, "Refresh1.png");
            this.imageCollection1.Images.SetKeyName(10, "Duyet1.png");
            this.imageCollection1.Images.SetKeyName(11, "Checklist6.png");
            this.imageCollection1.Images.SetKeyName(12, "icons8_discount_filled_50px.png");
            this.imageCollection1.Images.SetKeyName(13, "icons8_money_bag_filled_50px.png");
            this.imageCollection1.Images.SetKeyName(14, "icons8_treatment_plan_filled_50px.png");
            this.imageCollection1.Images.SetKeyName(15, "icons8_calendar_plus_filled_50px.png");
            // 
            // btnSua
            // 
            this.btnSua.Caption = "Sửa";
            this.btnSua.Id = 3;
            this.btnSua.ImageOptions.ImageIndex = 2;
            this.btnSua.Name = "btnSua";
            // 
            // gcSurvey
            // 
            this.gcSurvey.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcSurvey.Location = new System.Drawing.Point(0, 31);
            this.gcSurvey.MainView = this.gvSurvey;
            this.gcSurvey.Name = "gcSurvey";
            this.gcSurvey.Size = new System.Drawing.Size(994, 397);
            this.gcSurvey.TabIndex = 14;
            this.gcSurvey.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvSurvey});
            // 
            // gvSurvey
            // 
            this.gvSurvey.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2});
            gridFormatRule1.Name = "FormatTienTT";
            formatConditionRule2ColorScale1.PredefinedName = "White, Azure";
            gridFormatRule1.Rule = formatConditionRule2ColorScale1;
            gridFormatRule2.Name = "FormatDaThu";
            formatConditionRule2ColorScale2.PredefinedName = "White, Green";
            gridFormatRule2.Rule = formatConditionRule2ColorScale2;
            gridFormatRule3.Name = "FormatConNo";
            formatConditionRule2ColorScale3.PredefinedName = "White, Red";
            gridFormatRule3.Rule = formatConditionRule2ColorScale3;
            this.gvSurvey.FormatRules.Add(gridFormatRule1);
            this.gvSurvey.FormatRules.Add(gridFormatRule2);
            this.gvSurvey.FormatRules.Add(gridFormatRule3);
            this.gvSurvey.GridControl = this.gcSurvey;
            this.gvSurvey.GroupFormat = "[#image]{1} | {2}";
            this.gvSurvey.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "PhaiThu", null, "Phải thu: {0:#,0.##}"),
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "DaThu", null, "Đã thu: {0:#,0.##}"),
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "ConNo", null, "Còn nợ: {0:#,0.##}")});
            this.gvSurvey.Name = "gvSurvey";
            this.gvSurvey.OptionsBehavior.Editable = false;
            this.gvSurvey.OptionsMenu.ShowConditionalFormattingItem = true;
            this.gvSurvey.OptionsSelection.MultiSelect = true;
            this.gvSurvey.OptionsView.ColumnAutoWidth = false;
            this.gvSurvey.OptionsView.ShowAutoFilterRow = true;
            this.gvSurvey.OptionsView.ShowFooter = true;
            this.gvSurvey.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "gridColumn2";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            // 
            // frmAnswerManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(994, 428);
            this.Controls.Add(this.gcSurvey);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "frmAnswerManager";
            this.Text = "Trả lời khảo sát";
            this.Load += new System.EventHandler(this.frmManager_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkToaNha)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbbKyBC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTuNgay.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTuNgay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDenNgay.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDenNgay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcSurvey)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvSurvey)).EndInit();
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
        private DevExpress.XtraGrid.GridControl gcSurvey;
        private DevExpress.XtraGrid.Views.Grid.GridView gvSurvey;
        private DevExpress.XtraBars.BarButtonItem itemSua;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
    }
}