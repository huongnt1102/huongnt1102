namespace LandSoftBuilding.Receivables.DauKy
{
    partial class FrmDauKy
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDauKy));
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.itemBuilding = new DevExpress.XtraBars.BarEditItem();
            this.lkBuilding = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.itemYear = new DevExpress.XtraBars.BarEditItem();
            this.spinYear = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.itemRefresh = new DevExpress.XtraBars.BarButtonItem();
            this.itemAdd = new DevExpress.XtraBars.BarButtonItem();
            this.itemEdit = new DevExpress.XtraBars.BarButtonItem();
            this.itemDelete = new DevExpress.XtraBars.BarButtonItem();
            this.itemImport = new DevExpress.XtraBars.BarButtonItem();
            this.itemExport = new DevExpress.XtraBars.BarButtonItem();
            this.itemCapNhatSoDu = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
            this.gc = new DevExpress.XtraGrid.GridControl();
            this.gv = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.barSubItem1 = new DevExpress.XtraBars.BarSubItem();
            this.itemChayLaiMotKhachHang = new DevExpress.XtraBars.BarButtonItem();
            this.itemSuaTien = new DevExpress.XtraBars.BarButtonItem();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkBuilding)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinYear)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv)).BeginInit();
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
            this.itemBuilding,
            this.itemYear,
            this.itemRefresh,
            this.itemAdd,
            this.itemEdit,
            this.itemDelete,
            this.itemImport,
            this.itemExport,
            this.itemCapNhatSoDu,
            this.barSubItem1,
            this.itemChayLaiMotKhachHang,
            this.itemSuaTien});
            this.barManager1.MaxItemId = 12;
            this.barManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.lkBuilding,
            this.spinYear});
            this.barManager1.ShowFullMenus = true;
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.itemBuilding),
            new DevExpress.XtraBars.LinkPersistInfo(this.itemYear),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemRefresh, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemAdd, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemEdit, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemDelete, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemImport, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemExport, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemCapNhatSoDu, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barSubItem1, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.DrawDragBorder = false;
            this.bar1.OptionsBar.MultiLine = true;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Tools";
            // 
            // itemBuilding
            // 
            this.itemBuilding.Caption = "Tòa nhà";
            this.itemBuilding.Edit = this.lkBuilding;
            this.itemBuilding.Id = 0;
            this.itemBuilding.Name = "itemBuilding";
            this.itemBuilding.Width = 153;
            // 
            // lkBuilding
            // 
            this.lkBuilding.AutoHeight = false;
            this.lkBuilding.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkBuilding.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenTN", "Name1")});
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
            this.itemYear.Id = 1;
            this.itemYear.Name = "itemYear";
            this.itemYear.Width = 62;
            // 
            // spinYear
            // 
            this.spinYear.AutoHeight = false;
            this.spinYear.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinYear.Name = "spinYear";
            // 
            // itemRefresh
            // 
            this.itemRefresh.Caption = "Nạp";
            this.itemRefresh.Id = 2;
            this.itemRefresh.ImageIndex = 0;
            this.itemRefresh.Name = "itemRefresh";
            this.itemRefresh.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemRefresh_ItemClick);
            // 
            // itemAdd
            // 
            this.itemAdd.Caption = "Thêm";
            this.itemAdd.Id = 3;
            this.itemAdd.ImageIndex = 1;
            this.itemAdd.Name = "itemAdd";
            this.itemAdd.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemAdd_ItemClick);
            // 
            // itemEdit
            // 
            this.itemEdit.Caption = "Sửa";
            this.itemEdit.Id = 4;
            this.itemEdit.ImageIndex = 2;
            this.itemEdit.Name = "itemEdit";
            this.itemEdit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemEdit_ItemClick);
            // 
            // itemDelete
            // 
            this.itemDelete.Caption = "Xóa";
            this.itemDelete.Id = 5;
            this.itemDelete.ImageIndex = 3;
            this.itemDelete.Name = "itemDelete";
            this.itemDelete.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemDelete_ItemClick);
            // 
            // itemImport
            // 
            this.itemImport.Caption = "Import";
            this.itemImport.Id = 6;
            this.itemImport.ImageIndex = 4;
            this.itemImport.Name = "itemImport";
            this.itemImport.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemImport_ItemClick);
            // 
            // itemExport
            // 
            this.itemExport.Caption = "Export";
            this.itemExport.Id = 7;
            this.itemExport.ImageIndex = 5;
            this.itemExport.Name = "itemExport";
            this.itemExport.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemExport_ItemClick);
            // 
            // itemCapNhatSoDu
            // 
            this.itemCapNhatSoDu.Caption = "Chạy lại số dư đầu kỳ";
            this.itemCapNhatSoDu.Id = 8;
            this.itemCapNhatSoDu.ImageIndex = 6;
            this.itemCapNhatSoDu.Name = "itemCapNhatSoDu";
            this.itemCapNhatSoDu.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemCapNhatSoDu_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(794, 67);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 500);
            this.barDockControlBottom.Size = new System.Drawing.Size(794, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 67);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 433);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(794, 67);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 433);
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.Images.SetKeyName(0, "Refresh1.png");
            this.imageCollection1.Images.SetKeyName(1, "Add1.png");
            this.imageCollection1.Images.SetKeyName(2, "Edit3.png");
            this.imageCollection1.Images.SetKeyName(3, "Delete1.png");
            this.imageCollection1.Images.SetKeyName(4, "Import1.png");
            this.imageCollection1.Images.SetKeyName(5, "Export1.png");
            this.imageCollection1.Images.SetKeyName(6, "Data2.png");
            // 
            // gc
            // 
            this.gc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gc.Location = new System.Drawing.Point(0, 67);
            this.gc.MainView = this.gv;
            this.gc.MenuManager = this.barManager1;
            this.gc.Name = "gc";
            this.gc.Size = new System.Drawing.Size(794, 433);
            this.gc.TabIndex = 4;
            this.gc.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gv});
            // 
            // gv
            // 
            this.gv.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn2,
            this.gridColumn1,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5});
            this.gv.GridControl = this.gc;
            this.gv.GroupCount = 1;
            this.gv.Name = "gv";
            this.gv.OptionsBehavior.AutoExpandAllGroups = true;
            this.gv.OptionsSelection.MultiSelect = true;
            this.gv.OptionsView.ColumnAutoWidth = false;
            this.gv.OptionsView.ShowAutoFilterRow = true;
            this.gv.OptionsView.ShowFooter = true;
            this.gv.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.gridColumn1, DevExpress.Data.ColumnSortOrder.Ascending)});
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Mặt bằng";
            this.gridColumn2.FieldName = "MaSoMB";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 0;
            this.gridColumn2.Width = 173;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Khách hàng";
            this.gridColumn1.FieldName = "TenKH";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 168;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Dịch vụ";
            this.gridColumn3.FieldName = "TenLDV";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 1;
            this.gridColumn3.Width = 171;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Số tiền";
            this.gridColumn4.DisplayFormat.FormatString = "{0:#,0.##; (0.##);-}";
            this.gridColumn4.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn4.FieldName = "SoTien";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "SoTien", "{0:#,0.##; (0.##);-}")});
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 2;
            this.gridColumn4.Width = 148;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Diễn giải";
            this.gridColumn5.FieldName = "DienGiai";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 3;
            this.gridColumn5.Width = 303;
            // 
            // barSubItem1
            // 
            this.barSubItem1.Caption = "Tool";
            this.barSubItem1.Id = 9;
            this.barSubItem1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.itemChayLaiMotKhachHang),
            new DevExpress.XtraBars.LinkPersistInfo(this.itemSuaTien)});
            this.barSubItem1.Name = "barSubItem1";
            // 
            // itemChayLaiMotKhachHang
            // 
            this.itemChayLaiMotKhachHang.Caption = "Chạy lại 1 khách hàng";
            this.itemChayLaiMotKhachHang.Id = 10;
            this.itemChayLaiMotKhachHang.Name = "itemChayLaiMotKhachHang";
            this.itemChayLaiMotKhachHang.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemChayLaiMotKhachHang_ItemClick);
            // 
            // itemSuaTien
            // 
            this.itemSuaTien.Caption = "Sửa tiền";
            this.itemSuaTien.Id = 11;
            this.itemSuaTien.Name = "itemSuaTien";
            this.itemSuaTien.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemSuaTien_ItemClick);
            // 
            // FrmDauKy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(794, 500);
            this.Controls.Add(this.gc);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "FrmDauKy";
            this.Text = "Số dư đầu kỳ";
            this.Load += new System.EventHandler(this.FrmDauKy_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkBuilding)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinYear)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarEditItem itemBuilding;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lkBuilding;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarEditItem itemYear;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit spinYear;
        private DevExpress.XtraBars.BarButtonItem itemRefresh;
        private DevExpress.XtraBars.BarButtonItem itemAdd;
        private DevExpress.XtraBars.BarButtonItem itemEdit;
        private DevExpress.XtraBars.BarButtonItem itemDelete;
        private DevExpress.XtraBars.BarButtonItem itemImport;
        private DevExpress.XtraBars.BarButtonItem itemExport;
        private DevExpress.XtraGrid.GridControl gc;
        private DevExpress.XtraGrid.Views.Grid.GridView gv;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraBars.BarButtonItem itemCapNhatSoDu;
        private DevExpress.Utils.ImageCollection imageCollection1;
        private DevExpress.XtraBars.BarSubItem barSubItem1;
        private DevExpress.XtraBars.BarButtonItem itemChayLaiMotKhachHang;
        private DevExpress.XtraBars.BarButtonItem itemSuaTien;
    }
}