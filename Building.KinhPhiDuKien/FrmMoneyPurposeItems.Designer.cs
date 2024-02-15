namespace Building.KinhPhiDuKien
{
    partial class FrmMoneyPurposeItems
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMoneyPurposeItems));
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.itemBuilding = new DevExpress.XtraBars.BarEditItem();
            this.lkBuilding = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.itemYear = new DevExpress.XtraBars.BarEditItem();
            this.spinYear = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.itemMoneyPurpose = new DevExpress.XtraBars.BarEditItem();
            this.txtMoneyPurpose = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.itemRefresh = new DevExpress.XtraBars.BarButtonItem();
            this.itemAddMoneyPurpose = new DevExpress.XtraBars.BarButtonItem();
            this.itemAdd = new DevExpress.XtraBars.BarButtonItem();
            this.itemEdit = new DevExpress.XtraBars.BarButtonItem();
            this.itemDelete = new DevExpress.XtraBars.BarButtonItem();
            this.itemExport = new DevExpress.XtraBars.BarButtonItem();
            this.itemImport = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
            this.gc = new DevExpress.XtraGrid.GridControl();
            this.gv = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn16 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn14 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn15 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkBuilding)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinYear)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMoneyPurpose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
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
            this.barManager1.Images = this.imageCollection1;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.itemRefresh,
            this.itemBuilding,
            this.itemYear,
            this.itemMoneyPurpose,
            this.itemAddMoneyPurpose,
            this.itemAdd,
            this.itemEdit,
            this.itemDelete,
            this.itemExport,
            this.itemImport});
            this.barManager1.MaxItemId = 12;
            this.barManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.lkBuilding,
            this.spinYear,
            this.txtMoneyPurpose});
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.Width, this.itemBuilding, "", false, true, true, 138),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.Width, this.itemYear, "", false, true, true, 78),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.Width, this.itemMoneyPurpose, "", false, true, true, 126),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemRefresh, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemAddMoneyPurpose, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemAdd, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemEdit, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemDelete, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemExport, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemImport, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.DrawBorder = false;
            this.bar1.OptionsBar.MultiLine = true;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Tools";
            // 
            // itemBuilding
            // 
            this.itemBuilding.Caption = "Tòa nhà";
            this.itemBuilding.Edit = this.lkBuilding;
            this.itemBuilding.Id = 1;
            this.itemBuilding.Name = "itemBuilding";
            // 
            // lkBuilding
            // 
            this.lkBuilding.AutoHeight = false;
            this.lkBuilding.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkBuilding.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenTN", "Name2")});
            this.lkBuilding.DisplayMember = "TenTN";
            this.lkBuilding.Name = "lkBuilding";
            this.lkBuilding.NullText = "";
            this.lkBuilding.ShowHeader = false;
            this.lkBuilding.ValueMember = "MaTN";
            // 
            // itemYear
            // 
            this.itemYear.Caption = "Năm";
            this.itemYear.Edit = this.spinYear;
            this.itemYear.Id = 2;
            this.itemYear.Name = "itemYear";
            // 
            // spinYear
            // 
            this.spinYear.AutoHeight = false;
            this.spinYear.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spinYear.DisplayFormat.FormatString = "n0";
            this.spinYear.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinYear.EditFormat.FormatString = "n0";
            this.spinYear.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinYear.Mask.EditMask = "n0";
            this.spinYear.Name = "spinYear";
            // 
            // itemMoneyPurpose
            // 
            this.itemMoneyPurpose.Caption = "Số tiền còn lại";
            this.itemMoneyPurpose.Edit = this.txtMoneyPurpose;
            this.itemMoneyPurpose.Id = 3;
            this.itemMoneyPurpose.Name = "itemMoneyPurpose";
            // 
            // txtMoneyPurpose
            // 
            this.txtMoneyPurpose.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.txtMoneyPurpose.Appearance.Options.UseFont = true;
            this.txtMoneyPurpose.Appearance.Options.UseTextOptions = true;
            this.txtMoneyPurpose.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.txtMoneyPurpose.AutoHeight = false;
            this.txtMoneyPurpose.DisplayFormat.FormatString = "{0:#,0.##}";
            this.txtMoneyPurpose.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtMoneyPurpose.Name = "txtMoneyPurpose";
            this.txtMoneyPurpose.ReadOnly = true;
            // 
            // itemRefresh
            // 
            this.itemRefresh.Caption = "Nạp";
            this.itemRefresh.Id = 0;
            this.itemRefresh.ImageOptions.ImageIndex = 0;
            this.itemRefresh.Name = "itemRefresh";
            this.itemRefresh.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.ItemRefresh_ItemClick);
            // 
            // itemAddMoneyPurpose
            // 
            this.itemAddMoneyPurpose.Caption = "Thêm tổng tiền";
            this.itemAddMoneyPurpose.Id = 4;
            this.itemAddMoneyPurpose.ImageOptions.ImageIndex = 1;
            this.itemAddMoneyPurpose.Name = "itemAddMoneyPurpose";
            this.itemAddMoneyPurpose.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.ItemAddMoneyPurpose_ItemClick);
            // 
            // itemAdd
            // 
            this.itemAdd.Caption = "Thêm Items";
            this.itemAdd.Id = 5;
            this.itemAdd.ImageOptions.ImageIndex = 1;
            this.itemAdd.Name = "itemAdd";
            this.itemAdd.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.ItemAdd_ItemClick);
            // 
            // itemEdit
            // 
            this.itemEdit.Caption = "Sửa";
            this.itemEdit.Id = 6;
            this.itemEdit.ImageOptions.ImageIndex = 2;
            this.itemEdit.Name = "itemEdit";
            this.itemEdit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.ItemEdit_ItemClick);
            // 
            // itemDelete
            // 
            this.itemDelete.Caption = "Xóa";
            this.itemDelete.Id = 7;
            this.itemDelete.ImageOptions.ImageIndex = 3;
            this.itemDelete.Name = "itemDelete";
            this.itemDelete.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.ItemDelete_ItemClick);
            // 
            // itemExport
            // 
            this.itemExport.Caption = "Export";
            this.itemExport.Id = 10;
            this.itemExport.ImageOptions.ImageIndex = 4;
            this.itemExport.Name = "itemExport";
            this.itemExport.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemExport_ItemClick);
            // 
            // itemImport
            // 
            this.itemImport.Caption = "Import";
            this.itemImport.Id = 11;
            this.itemImport.ImageOptions.ImageIndex = 5;
            this.itemImport.Name = "itemImport";
            this.itemImport.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemImport_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(707, 69);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 537);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(707, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 69);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 468);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(707, 69);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 468);
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.Images.SetKeyName(0, "Refresh1.png");
            this.imageCollection1.Images.SetKeyName(1, "Add1.png");
            this.imageCollection1.Images.SetKeyName(2, "Edit3.png");
            this.imageCollection1.Images.SetKeyName(3, "Delete1.png");
            this.imageCollection1.Images.SetKeyName(4, "Export1.png");
            this.imageCollection1.Images.SetKeyName(5, "Import1.png");
            // 
            // gc
            // 
            this.gc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gc.Location = new System.Drawing.Point(0, 69);
            this.gc.MainView = this.gv;
            this.gc.MenuManager = this.barManager1;
            this.gc.Name = "gc";
            this.gc.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1});
            this.gc.Size = new System.Drawing.Size(707, 468);
            this.gc.TabIndex = 4;
            this.gc.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gv});
            // 
            // gv
            // 
            this.gv.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn7,
            this.gridColumn1,
            this.gridColumn16,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn6,
            this.gridColumn8,
            this.gridColumn9,
            this.gridColumn10,
            this.gridColumn11,
            this.gridColumn12,
            this.gridColumn13,
            this.gridColumn14,
            this.gridColumn15});
            this.gv.GridControl = this.gc;
            this.gv.Name = "gv";
            this.gv.OptionsBehavior.Editable = false;
            this.gv.OptionsView.ColumnAutoWidth = false;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "Sử dụng";
            this.gridColumn7.ColumnEdit = this.repositoryItemCheckEdit1;
            this.gridColumn7.FieldName = "IsUse";
            this.gridColumn7.Name = "gridColumn7";
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Id";
            this.gridColumn1.FieldName = "Id";
            this.gridColumn1.Name = "gridColumn1";
            // 
            // gridColumn16
            // 
            this.gridColumn16.Caption = "Mã hạng mục";
            this.gridColumn16.FieldName = "No";
            this.gridColumn16.Name = "gridColumn16";
            this.gridColumn16.Visible = true;
            this.gridColumn16.VisibleIndex = 0;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Tên hạng mục";
            this.gridColumn2.FieldName = "Name";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 274;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Kinh phí dự kiến";
            this.gridColumn3.DisplayFormat.FormatString = "{0:#,0.##; (0.##);-}";
            this.gridColumn3.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn3.FieldName = "MoneyPurpose";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            this.gridColumn3.Width = 140;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "MoneyPurposeId";
            this.gridColumn4.FieldName = "MoneyPurposeId";
            this.gridColumn4.Name = "gridColumn4";
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Year";
            this.gridColumn5.FieldName = "Year";
            this.gridColumn5.Name = "gridColumn5";
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "BuildingId";
            this.gridColumn6.FieldName = "BuildingId";
            this.gridColumn6.Name = "gridColumn6";
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "Đã sử dụng";
            this.gridColumn8.DisplayFormat.FormatString = "{0:#,0.##; (0.##);-}";
            this.gridColumn8.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn8.FieldName = "MoneyUsed";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 3;
            this.gridColumn8.Width = 141;
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "UserCreateId";
            this.gridColumn9.FieldName = "UserCreateId";
            this.gridColumn9.Name = "gridColumn9";
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "UserCreateName";
            this.gridColumn10.FieldName = "UserCreateName";
            this.gridColumn10.Name = "gridColumn10";
            // 
            // gridColumn11
            // 
            this.gridColumn11.Caption = "DateCreate";
            this.gridColumn11.FieldName = "DateCreate";
            this.gridColumn11.Name = "gridColumn11";
            // 
            // gridColumn12
            // 
            this.gridColumn12.Caption = "UserUpdateId";
            this.gridColumn12.FieldName = "UserUpdateId";
            this.gridColumn12.Name = "gridColumn12";
            // 
            // gridColumn13
            // 
            this.gridColumn13.Caption = "UserUpdateName";
            this.gridColumn13.FieldName = "UserUpdateName";
            this.gridColumn13.Name = "gridColumn13";
            // 
            // gridColumn14
            // 
            this.gridColumn14.Caption = "DateUpdate";
            this.gridColumn14.FieldName = "DateUpdate";
            this.gridColumn14.Name = "gridColumn14";
            // 
            // gridColumn15
            // 
            this.gridColumn15.Caption = "Còn lại";
            this.gridColumn15.DisplayFormat.FormatString = "{0:#,0.##; (0.##);-}";
            this.gridColumn15.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn15.FieldName = "MoneyExist";
            this.gridColumn15.Name = "gridColumn15";
            this.gridColumn15.Visible = true;
            this.gridColumn15.VisibleIndex = 4;
            this.gridColumn15.Width = 148;
            // 
            // FrmMoneyPurposeItems
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(707, 537);
            this.Controls.Add(this.gc);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "FrmMoneyPurposeItems";
            this.Text = "Hạng mục kinh phí";
            this.Load += new System.EventHandler(this.FrmMoneyPurposeItems_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkBuilding)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinYear)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMoneyPurpose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarEditItem itemBuilding;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lkBuilding;
        private DevExpress.XtraBars.BarEditItem itemYear;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit spinYear;
        private DevExpress.XtraBars.BarEditItem itemMoneyPurpose;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit txtMoneyPurpose;
        private DevExpress.XtraBars.BarButtonItem itemRefresh;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem itemAddMoneyPurpose;
        private DevExpress.XtraBars.BarButtonItem itemAdd;
        private DevExpress.XtraBars.BarButtonItem itemEdit;
        private DevExpress.XtraBars.BarButtonItem itemDelete;
        private DevExpress.XtraGrid.GridControl gc;
        private DevExpress.XtraGrid.Views.Grid.GridView gv;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn12;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn13;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn14;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn15;
        private DevExpress.Utils.ImageCollection imageCollection1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn16;
        private DevExpress.XtraBars.BarButtonItem itemExport;
        private DevExpress.XtraBars.BarButtonItem itemImport;
    }
}