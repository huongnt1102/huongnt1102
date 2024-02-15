namespace Library.Other
{
    partial class ctlXa
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ctlXa));
            this.imageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.itemTinh = new DevExpress.XtraBars.BarEditItem();
            this.lkTinh = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.itemRefresh = new DevExpress.XtraBars.BarButtonItem();
            this.itemAdd = new DevExpress.XtraBars.BarButtonItem();
            this.itemSave = new DevExpress.XtraBars.BarButtonItem();
            this.itemDelete = new DevExpress.XtraBars.BarButtonItem();
            this.itemImport = new DevExpress.XtraBars.BarButtonItem();
            this.itemExport = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.itemEdit = new DevExpress.XtraBars.BarButtonItem();
            this.gcXa = new DevExpress.XtraGrid.GridControl();
            this.gvXa = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colTenHuong = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.glkHuyen = new DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit();
            this.repositoryItemGridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lookUpTinh = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.lookUpHuyen2 = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkTinh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcXa)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvXa)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkHuyen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemGridLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpTinh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpHuyen2)).BeginInit();
            this.SuspendLayout();
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.Images.SetKeyName(0, "add_outline.png");
            this.imageCollection1.Images.SetKeyName(1, "cross.png");
            this.imageCollection1.Images.SetKeyName(2, "edit.png");
            this.imageCollection1.Images.SetKeyName(3, "refresh.png");
            this.imageCollection1.Images.SetKeyName(4, "save_16px.png");
            this.imageCollection1.Images.SetKeyName(5, "export_excel.png");
            this.imageCollection1.Images.SetKeyName(6, "import_excel.png");
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
            this.itemRefresh,
            this.itemSave,
            this.itemEdit,
            this.itemDelete,
            this.itemTinh,
            this.itemImport,
            this.itemExport,
            this.itemAdd});
            this.barManager1.MaxItemId = 8;
            this.barManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.lkTinh});
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemTinh, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemRefresh, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemAdd, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemSave, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemDelete, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemImport, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemExport, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.DisableCustomization = true;
            this.bar1.OptionsBar.DrawDragBorder = false;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Tools";
            // 
            // itemTinh
            // 
            this.itemTinh.Caption = "Tỉnh";
            this.itemTinh.Edit = this.lkTinh;
            this.itemTinh.EditValue = 2;
            this.itemTinh.Id = 4;
            this.itemTinh.ItemAppearance.Normal.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.itemTinh.ItemAppearance.Normal.Options.UseFont = true;
            this.itemTinh.Name = "itemTinh";
            this.itemTinh.Width = 150;
            this.itemTinh.EditValueChanged += new System.EventHandler(this.itemTinh_EditValueChanged);
            // 
            // lkTinh
            // 
            this.lkTinh.AutoHeight = false;
            this.lkTinh.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkTinh.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenTinh", "Name3")});
            this.lkTinh.DisplayMember = "TenTinh";
            this.lkTinh.Name = "lkTinh";
            this.lkTinh.NullText = "";
            this.lkTinh.ShowHeader = false;
            this.lkTinh.ShowLines = false;
            this.lkTinh.ValueMember = "MaTinh";
            // 
            // itemRefresh
            // 
            this.itemRefresh.Caption = "Nạp";
            this.itemRefresh.Id = 0;
            this.itemRefresh.ImageIndex = 3;
            this.itemRefresh.Name = "itemRefresh";
            this.itemRefresh.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemRefresh_ItemClick);
            // 
            // itemAdd
            // 
            this.itemAdd.Caption = "Thêm";
            this.itemAdd.Id = 7;
            this.itemAdd.ImageIndex = 0;
            this.itemAdd.Name = "itemAdd";
            this.itemAdd.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemAdd_ItemClick_1);
            // 
            // itemSave
            // 
            this.itemSave.Caption = "Lưu";
            this.itemSave.Id = 1;
            this.itemSave.ImageIndex = 4;
            this.itemSave.Name = "itemSave";
            this.itemSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemAdd_ItemClick);
            // 
            // itemDelete
            // 
            this.itemDelete.Caption = "Xóa";
            this.itemDelete.Id = 3;
            this.itemDelete.ImageIndex = 1;
            this.itemDelete.Name = "itemDelete";
            this.itemDelete.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemDelete_ItemClick);
            // 
            // itemImport
            // 
            this.itemImport.Caption = "Import";
            this.itemImport.Id = 5;
            this.itemImport.ImageIndex = 6;
            this.itemImport.Name = "itemImport";
            this.itemImport.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemImport_ItemClick);
            // 
            // itemExport
            // 
            this.itemExport.Caption = "Export";
            this.itemExport.Id = 6;
            this.itemExport.ImageIndex = 5;
            this.itemExport.Name = "itemExport";
            this.itemExport.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemExport_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(734, 31);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 346);
            this.barDockControlBottom.Size = new System.Drawing.Size(734, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 31);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 315);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(734, 31);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 315);
            // 
            // itemEdit
            // 
            this.itemEdit.Caption = "Sửa";
            this.itemEdit.Id = 2;
            this.itemEdit.ImageIndex = 2;
            this.itemEdit.Name = "itemEdit";
            this.itemEdit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemEdit_ItemClick);
            // 
            // gcXa
            // 
            this.gcXa.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcXa.Location = new System.Drawing.Point(0, 31);
            this.gcXa.MainView = this.gvXa;
            this.gcXa.Name = "gcXa";
            this.gcXa.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.lookUpTinh,
            this.lookUpHuyen2,
            this.glkHuyen});
            this.gcXa.Size = new System.Drawing.Size(734, 315);
            this.gcXa.TabIndex = 15;
            this.gcXa.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvXa});
            // 
            // gvXa
            // 
            this.gvXa.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colTenHuong,
            this.gridColumn1,
            this.gridColumn2});
            this.gvXa.GridControl = this.gcXa;
            this.gvXa.GroupPanelText = "Kéo một cột lên đây để xem theo nhóm";
            this.gvXa.IndicatorWidth = 35;
            this.gvXa.Name = "gvXa";
            this.gvXa.OptionsView.ShowAutoFilterRow = true;
            this.gvXa.OptionsView.ShowGroupPanel = false;
            this.gvXa.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.gvXa_CustomDrawRowIndicator);
            // 
            // colTenHuong
            // 
            this.colTenHuong.Caption = "Tên xã";
            this.colTenHuong.FieldName = "TenXa";
            this.colTenHuong.Name = "colTenHuong";
            this.colTenHuong.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.colTenHuong.Visible = true;
            this.colTenHuong.VisibleIndex = 0;
            this.colTenHuong.Width = 350;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Quận (Huyện)";
            this.gridColumn1.ColumnEdit = this.glkHuyen;
            this.gridColumn1.FieldName = "MaHuyen";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 2;
            this.gridColumn1.Width = 313;
            // 
            // glkHuyen
            // 
            this.glkHuyen.AutoHeight = false;
            this.glkHuyen.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.glkHuyen.DisplayMember = "TenHuyen";
            this.glkHuyen.Name = "glkHuyen";
            this.glkHuyen.NullText = "";
            this.glkHuyen.ValueMember = "MaHuyen";
            this.glkHuyen.View = this.repositoryItemGridLookUpEdit1View;
            // 
            // repositoryItemGridLookUpEdit1View
            // 
            this.repositoryItemGridLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn4});
            this.repositoryItemGridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.repositoryItemGridLookUpEdit1View.Name = "repositoryItemGridLookUpEdit1View";
            this.repositoryItemGridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.repositoryItemGridLookUpEdit1View.OptionsView.ShowAutoFilterRow = true;
            this.repositoryItemGridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Tên Huyện";
            this.gridColumn4.FieldName = "TenHuyen";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 0;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Tên Hiển Thị";
            this.gridColumn2.FieldName = "TenHienThi";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 479;
            // 
            // lookUpTinh
            // 
            this.lookUpTinh.AutoHeight = false;
            this.lookUpTinh.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpTinh.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TênTinh", "Name1")});
            this.lookUpTinh.DisplayMember = "TenTinh";
            this.lookUpTinh.Name = "lookUpTinh";
            this.lookUpTinh.NullText = "";
            this.lookUpTinh.ShowHeader = false;
            this.lookUpTinh.ValueMember = "MaTinh";
            // 
            // lookUpHuyen2
            // 
            this.lookUpHuyen2.AutoHeight = false;
            this.lookUpHuyen2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpHuyen2.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenHuyen", "TenHuyen")});
            this.lookUpHuyen2.DisplayMember = "TenHuyen";
            this.lookUpHuyen2.Name = "lookUpHuyen2";
            this.lookUpHuyen2.NullText = "";
            this.lookUpHuyen2.ShowHeader = false;
            this.lookUpHuyen2.ShowLines = false;
            this.lookUpHuyen2.ValueMember = "MaHuyen";
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Tên Huyện";
            this.gridColumn3.FieldName = "TenHuyen";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 0;
            // 
            // ctlXa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(734, 346);
            this.Controls.Add(this.gcXa);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "ctlXa";
            this.Text = "Xã/Phường";
            this.Load += new System.EventHandler(this.ctlXa_Load);
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkTinh)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcXa)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvXa)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkHuyen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemGridLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpTinh)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpHuyen2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.Utils.ImageCollection imageCollection1;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarButtonItem itemRefresh;
        private DevExpress.XtraBars.BarButtonItem itemSave;
        private DevExpress.XtraBars.BarButtonItem itemEdit;
        private DevExpress.XtraBars.BarButtonItem itemDelete;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarEditItem itemTinh;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lkTinh;
        private DevExpress.XtraGrid.GridControl gcXa;
        private DevExpress.XtraGrid.Views.Grid.GridView gvXa;
        private DevExpress.XtraGrid.Columns.GridColumn colTenHuong;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lookUpHuyen2;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lookUpTinh;
        private DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit glkHuyen;
        private DevExpress.XtraGrid.Views.Grid.GridView repositoryItemGridLookUpEdit1View;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraBars.BarButtonItem itemImport;
        private DevExpress.XtraBars.BarButtonItem itemExport;
        private DevExpress.XtraBars.BarButtonItem itemAdd;
    }
}
