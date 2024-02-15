namespace BuildingDesignTemplate
{
    partial class FrmManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmManager));
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.barButtonItemNap = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemThem = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemSua = new DevExpress.XtraBars.BarButtonItem();
            this.itemCopy = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemXoa = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemImport = new DevExpress.XtraBars.BarButtonItem();
            this.itemExport = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
            this.barEditItemToaNha = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemLookUpEditToaNha = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.itemChkToaNha = new DevExpress.XtraBars.BarEditItem();
            this.repChkToaNha = new DevExpress.XtraEditors.Repository.RepositoryItemCheckedComboBoxEdit();
            this.repositoryItemGridLookUpEditNhomTaiSan = new DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit();
            this.repositoryItemGridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gc = new DevExpress.XtraGrid.GridControl();
            this.gv = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEditToaNha)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repChkToaNha)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemGridLookUpEditNhomTaiSan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemGridLookUpEdit1View)).BeginInit();
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
            this.barEditItemToaNha,
            this.barButtonItemNap,
            this.barButtonItemThem,
            this.barButtonItemSua,
            this.barButtonItemXoa,
            this.barButtonItemImport,
            this.itemExport,
            this.itemCopy,
            this.itemChkToaNha});
            this.barManager1.MaxItemId = 11;
            this.barManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemLookUpEditToaNha,
            this.repositoryItemGridLookUpEditNhomTaiSan,
            this.repChkToaNha});
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barButtonItemNap, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barButtonItemThem, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barButtonItemSua, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemCopy, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barButtonItemXoa, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barButtonItemImport, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemExport, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Tools";
            // 
            // barButtonItemNap
            // 
            this.barButtonItemNap.Caption = "Nạp";
            this.barButtonItemNap.Id = 2;
            this.barButtonItemNap.ImageOptions.ImageIndex = 0;
            this.barButtonItemNap.Name = "barButtonItemNap";
            this.barButtonItemNap.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemNap_ItemClick);
            // 
            // barButtonItemThem
            // 
            this.barButtonItemThem.Caption = "Thêm";
            this.barButtonItemThem.Id = 3;
            this.barButtonItemThem.ImageOptions.ImageIndex = 1;
            this.barButtonItemThem.Name = "barButtonItemThem";
            this.barButtonItemThem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemThem_ItemClick);
            // 
            // barButtonItemSua
            // 
            this.barButtonItemSua.Caption = "Sửa";
            this.barButtonItemSua.Id = 4;
            this.barButtonItemSua.ImageOptions.ImageIndex = 2;
            this.barButtonItemSua.Name = "barButtonItemSua";
            this.barButtonItemSua.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemSua_ItemClick);
            // 
            // itemCopy
            // 
            this.itemCopy.Caption = "Copy";
            this.itemCopy.Id = 9;
            this.itemCopy.ImageOptions.ImageIndex = 6;
            this.itemCopy.Name = "itemCopy";
            this.itemCopy.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemCopy_ItemClick);
            // 
            // barButtonItemXoa
            // 
            this.barButtonItemXoa.Caption = "Xóa";
            this.barButtonItemXoa.Id = 5;
            this.barButtonItemXoa.ImageOptions.ImageIndex = 3;
            this.barButtonItemXoa.Name = "barButtonItemXoa";
            this.barButtonItemXoa.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemXoa_ItemClick);
            // 
            // barButtonItemImport
            // 
            this.barButtonItemImport.Caption = "Import";
            this.barButtonItemImport.Id = 6;
            this.barButtonItemImport.ImageOptions.ImageIndex = 4;
            this.barButtonItemImport.Name = "barButtonItemImport";
            this.barButtonItemImport.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemImport_ItemClick);
            // 
            // itemExport
            // 
            this.itemExport.Caption = "Export";
            this.itemExport.Id = 8;
            this.itemExport.ImageOptions.ImageIndex = 5;
            this.itemExport.Name = "itemExport";
            this.itemExport.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemExport_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(887, 31);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 435);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(887, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 31);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 404);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(887, 31);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 404);
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.Images.SetKeyName(0, "icons8_refresh1.png");
            this.imageCollection1.Images.SetKeyName(1, "icons8_add4.png");
            this.imageCollection1.Images.SetKeyName(2, "icons8_edit3.png");
            this.imageCollection1.Images.SetKeyName(3, "icons8_delete1.png");
            this.imageCollection1.Images.SetKeyName(4, "icons8_import.png");
            this.imageCollection1.Images.SetKeyName(5, "icons8_export.png");
            this.imageCollection1.Images.SetKeyName(6, "Copy1.png");
            // 
            // barEditItemToaNha
            // 
            this.barEditItemToaNha.Caption = "Tòa nhà";
            this.barEditItemToaNha.Edit = this.repositoryItemLookUpEditToaNha;
            this.barEditItemToaNha.EditWidth = 148;
            this.barEditItemToaNha.Id = 0;
            this.barEditItemToaNha.Name = "barEditItemToaNha";
            this.barEditItemToaNha.EditValueChanged += new System.EventHandler(this.barEditItemToaNha_EditValueChanged);
            // 
            // repositoryItemLookUpEditToaNha
            // 
            this.repositoryItemLookUpEditToaNha.AutoHeight = false;
            this.repositoryItemLookUpEditToaNha.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemLookUpEditToaNha.DisplayMember = "TenTN";
            this.repositoryItemLookUpEditToaNha.Name = "repositoryItemLookUpEditToaNha";
            this.repositoryItemLookUpEditToaNha.NullText = "[Tòa nhà]";
            this.repositoryItemLookUpEditToaNha.ValueMember = "MaTN";
            // 
            // itemChkToaNha
            // 
            this.itemChkToaNha.Caption = "Tòa Nhà";
            this.itemChkToaNha.Edit = this.repChkToaNha;
            this.itemChkToaNha.Id = 10;
            this.itemChkToaNha.Name = "itemChkToaNha";
            // 
            // repChkToaNha
            // 
            this.repChkToaNha.AutoHeight = false;
            this.repChkToaNha.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repChkToaNha.DisplayMember = "TenTN";
            this.repChkToaNha.Name = "repChkToaNha";
            this.repChkToaNha.ValueMember = "MaTN";
            // 
            // repositoryItemGridLookUpEditNhomTaiSan
            // 
            this.repositoryItemGridLookUpEditNhomTaiSan.AutoHeight = false;
            this.repositoryItemGridLookUpEditNhomTaiSan.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemGridLookUpEditNhomTaiSan.DisplayMember = "TenNhomTaiSan";
            this.repositoryItemGridLookUpEditNhomTaiSan.Name = "repositoryItemGridLookUpEditNhomTaiSan";
            this.repositoryItemGridLookUpEditNhomTaiSan.NullText = "[Hệ thống]";
            this.repositoryItemGridLookUpEditNhomTaiSan.PopupView = this.repositoryItemGridLookUpEdit1View;
            this.repositoryItemGridLookUpEditNhomTaiSan.ValueMember = "ID";
            // 
            // repositoryItemGridLookUpEdit1View
            // 
            this.repositoryItemGridLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn13});
            this.repositoryItemGridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.repositoryItemGridLookUpEdit1View.Name = "repositoryItemGridLookUpEdit1View";
            this.repositoryItemGridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.repositoryItemGridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn13
            // 
            this.gridColumn13.Caption = "Tên";
            this.gridColumn13.FieldName = "TenNhomTaiSan";
            this.gridColumn13.Name = "gridColumn13";
            this.gridColumn13.Visible = true;
            this.gridColumn13.VisibleIndex = 0;
            // 
            // gc
            // 
            this.gc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gc.Location = new System.Drawing.Point(0, 31);
            this.gc.MainView = this.gv;
            this.gc.MenuManager = this.barManager1;
            this.gc.Name = "gc";
            this.gc.ShowOnlyPredefinedDetails = true;
            this.gc.Size = new System.Drawing.Size(887, 404);
            this.gc.TabIndex = 4;
            this.gc.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gv});
            // 
            // gv
            // 
            this.gv.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn6,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn7,
            this.gridColumn8});
            this.gv.GridControl = this.gc;
            this.gv.Name = "gv";
            this.gv.OptionsBehavior.AutoExpandAllGroups = true;
            this.gv.OptionsView.ColumnAutoWidth = false;
            this.gv.OptionsView.ShowAutoFilterRow = true;
            this.gv.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Id";
            this.gridColumn1.FieldName = "Id";
            this.gridColumn1.Name = "gridColumn1";
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Tên biểu mẫu";
            this.gridColumn2.FieldName = "Name";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 0;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Loại biểu mẫu";
            this.gridColumn3.FieldName = "FormGroupName";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 1;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "Mô tả";
            this.gridColumn6.FieldName = "Description";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 2;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Người tạo";
            this.gridColumn4.FieldName = "UserName";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 3;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Ngày cập nhật";
            this.gridColumn5.FieldName = "DateUpdate";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 4;
            this.gridColumn5.Width = 115;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "Tòa nhà";
            this.gridColumn7.FieldName = "BuildingName";
            this.gridColumn7.Name = "gridColumn7";
            // 
            // gridColumn11
            // 
            this.gridColumn11.Caption = "Tên nhóm";
            this.gridColumn11.FieldName = "TenNhomCongViec";
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.Visible = true;
            this.gridColumn11.VisibleIndex = 0;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "In riêng mặt bằng";
            this.gridColumn8.FieldName = "IsUseApartment";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 5;
            this.gridColumn8.Width = 113;
            // 
            // FrmManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(887, 435);
            this.Controls.Add(this.gc);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "FrmManager";
            this.Text = "Danh sách biểu mẫu";
            this.Load += new System.EventHandler(this.frmVatTu_Manager_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEditToaNha)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repChkToaNha)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemGridLookUpEditNhomTaiSan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemGridLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv)).EndInit();
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
        private DevExpress.Utils.ImageCollection imageCollection1;
        private DevExpress.XtraBars.BarEditItem barEditItemToaNha;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEditToaNha;
        private DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit repositoryItemGridLookUpEditNhomTaiSan;
        private DevExpress.XtraGrid.Views.Grid.GridView repositoryItemGridLookUpEdit1View;
        private DevExpress.XtraBars.BarButtonItem barButtonItemNap;
        private DevExpress.XtraBars.BarButtonItem barButtonItemThem;
        private DevExpress.XtraBars.BarButtonItem barButtonItemSua;
        private DevExpress.XtraBars.BarButtonItem barButtonItemXoa;
        private DevExpress.XtraGrid.GridControl gc;
        private DevExpress.XtraGrid.Views.Grid.GridView gv;
        private DevExpress.XtraBars.BarButtonItem barButtonItemImport;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn13;
        private DevExpress.XtraBars.BarButtonItem itemExport;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraBars.BarButtonItem itemCopy;
        private DevExpress.XtraBars.BarEditItem itemChkToaNha;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckedComboBoxEdit repChkToaNha;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
    }
}