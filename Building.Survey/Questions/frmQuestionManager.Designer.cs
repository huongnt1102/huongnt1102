namespace Building.Survey.Questions
{
    partial class frmQuestionManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmQuestionManager));
            DevExpress.XtraGrid.GridFormatRule gridFormatRule1 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRule2ColorScale formatConditionRule2ColorScale1 = new DevExpress.XtraEditors.FormatConditionRule2ColorScale();
            DevExpress.XtraGrid.GridFormatRule gridFormatRule2 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRule2ColorScale formatConditionRule2ColorScale2 = new DevExpress.XtraEditors.FormatConditionRule2ColorScale();
            DevExpress.XtraGrid.GridFormatRule gridFormatRule3 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRule2ColorScale formatConditionRule2ColorScale3 = new DevExpress.XtraEditors.FormatConditionRule2ColorScale();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.btnRefresh = new DevExpress.XtraBars.BarButtonItem();
            this.btnAdd = new DevExpress.XtraBars.BarButtonItem();
            this.itemEdit = new DevExpress.XtraBars.BarButtonItem();
            this.btnDelete = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
            this.btnSua = new DevExpress.XtraBars.BarButtonItem();
            this.cbbKyBC = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.dateTuNgay = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.dateDenNgay = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.lkToaNha = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.gcQuestion = new DevExpress.XtraGrid.GridControl();
            this.gvQuestion = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbbKyBC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTuNgay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTuNgay.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDenNgay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDenNgay.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkToaNha)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcQuestion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvQuestion)).BeginInit();
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
            this.btnRefresh,
            this.btnDelete,
            this.btnSua,
            this.itemEdit,
            this.btnAdd});
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
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnRefresh, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnAdd, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemEdit, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnDelete, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.DrawDragBorder = false;
            this.bar1.OptionsBar.MultiLine = true;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Tools";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Caption = "Nạp";
            this.btnRefresh.Id = 0;
            this.btnRefresh.ImageOptions.ImageIndex = 9;
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnRefresh_ItemClick);
            // 
            // btnAdd
            // 
            this.btnAdd.Caption = "Thêm";
            this.btnAdd.Id = 67;
            this.btnAdd.ImageOptions.ImageIndex = 1;
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnAdd_ItemClick);
            // 
            // itemEdit
            // 
            this.itemEdit.Caption = "Sửa";
            this.itemEdit.Id = 21;
            this.itemEdit.ImageOptions.ImageIndex = 2;
            this.itemEdit.Name = "itemEdit";
            this.itemEdit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemEdit_ItemClick);
            // 
            // btnDelete
            // 
            this.btnDelete.Caption = "Xóa";
            this.btnDelete.Id = 2;
            this.btnDelete.ImageOptions.ImageIndex = 3;
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnDelete_ItemClick);
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
            // cbbKyBC
            // 
            this.cbbKyBC.Name = "cbbKyBC";
            // 
            // dateTuNgay
            // 
            this.dateTuNgay.Name = "dateTuNgay";
            // 
            // dateDenNgay
            // 
            this.dateDenNgay.Name = "dateDenNgay";
            // 
            // lkToaNha
            // 
            this.lkToaNha.Name = "lkToaNha";
            // 
            // gcQuestion
            // 
            this.gcQuestion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcQuestion.Location = new System.Drawing.Point(0, 31);
            this.gcQuestion.MainView = this.gvQuestion;
            this.gcQuestion.Name = "gcQuestion";
            this.gcQuestion.Size = new System.Drawing.Size(994, 397);
            this.gcQuestion.TabIndex = 24;
            this.gcQuestion.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvQuestion});
            // 
            // gvQuestion
            // 
            this.gvQuestion.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3});
            gridFormatRule1.Name = "FormatTienTT";
            formatConditionRule2ColorScale1.PredefinedName = "White, Azure";
            gridFormatRule1.Rule = formatConditionRule2ColorScale1;
            gridFormatRule2.Name = "FormatDaThu";
            formatConditionRule2ColorScale2.PredefinedName = "White, Green";
            gridFormatRule2.Rule = formatConditionRule2ColorScale2;
            gridFormatRule3.Name = "FormatConNo";
            formatConditionRule2ColorScale3.PredefinedName = "White, Red";
            gridFormatRule3.Rule = formatConditionRule2ColorScale3;
            this.gvQuestion.FormatRules.Add(gridFormatRule1);
            this.gvQuestion.FormatRules.Add(gridFormatRule2);
            this.gvQuestion.FormatRules.Add(gridFormatRule3);
            this.gvQuestion.GridControl = this.gcQuestion;
            this.gvQuestion.GroupFormat = "[#image]{1} | {2}";
            this.gvQuestion.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "PhaiThu", null, "Phải thu: {0:#,0.##}"),
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "DaThu", null, "Đã thu: {0:#,0.##}"),
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "ConNo", null, "Còn nợ: {0:#,0.##}")});
            this.gvQuestion.Name = "gvQuestion";
            this.gvQuestion.OptionsBehavior.Editable = false;
            this.gvQuestion.OptionsMenu.ShowConditionalFormattingItem = true;
            this.gvQuestion.OptionsSelection.MultiSelect = true;
            this.gvQuestion.OptionsView.ColumnAutoWidth = false;
            this.gvQuestion.OptionsView.ShowAutoFilterRow = true;
            this.gvQuestion.OptionsView.ShowFooter = true;
            this.gvQuestion.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "ID";
            this.gridColumn1.FieldName = "question_id";
            this.gridColumn1.Name = "gridColumn1";
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Câu hỏi";
            this.gridColumn2.FieldName = "question_name";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 802;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Loại câu hỏi";
            this.gridColumn3.FieldName = "question_type_name";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 0;
            this.gridColumn3.Width = 277;
            // 
            // frmQuestionManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(994, 428);
            this.Controls.Add(this.gcQuestion);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "frmQuestionManager";
            this.Text = "Câu hỏi khảo sát";
            this.Load += new System.EventHandler(this.frmManager_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbbKyBC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTuNgay.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTuNgay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDenNgay.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDenNgay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkToaNha)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcQuestion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvQuestion)).EndInit();
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
        private DevExpress.XtraBars.BarButtonItem btnRefresh;
        private DevExpress.XtraBars.BarButtonItem btnDelete;
        private DevExpress.XtraBars.BarButtonItem btnSua;
        private DevExpress.Utils.ImageCollection imageCollection1;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox cbbKyBC;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit dateTuNgay;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit dateDenNgay;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lkToaNha;
        private DevExpress.XtraBars.BarButtonItem itemEdit;
        private DevExpress.XtraBars.BarButtonItem btnAdd;
        private DevExpress.XtraGrid.GridControl gcQuestion;
        private DevExpress.XtraGrid.Views.Grid.GridView gvQuestion;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
    }
}