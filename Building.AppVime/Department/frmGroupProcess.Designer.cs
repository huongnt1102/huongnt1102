namespace Building.AppVime.Tower.Department
{
    partial class frmGroupProcess
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmGroupProcess));
            this.gcDepartment = new DevExpress.XtraGrid.GridControl();
            this.gvDepartment = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lookUpEditTower = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.itemNap = new DevExpress.XtraBars.BarButtonItem();
            this.itemSave = new DevExpress.XtraBars.BarButtonItem();
            this.itemClose = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
            this.itemLuu = new DevExpress.XtraBars.BarButtonItem();
            this.itemStaffAdd = new DevExpress.XtraBars.BarButtonItem();
            this.itemStaffSave = new DevExpress.XtraBars.BarButtonItem();
            this.itemStaffDelete = new DevExpress.XtraBars.BarButtonItem();
            this.itemDelGroup = new DevExpress.XtraBars.BarButtonItem();
            this.itemSaveGroup = new DevExpress.XtraBars.BarButtonItem();
            this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
            this.gcGroup = new DevExpress.XtraGrid.GridControl();
            this.gvGroup = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDepartmentId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemLookUpEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.ppGroup = new DevExpress.XtraBars.PopupMenu(this.components);
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.tabMain = new DevExpress.XtraTab.XtraTabControl();
            ((System.ComponentModel.ISupportInitialize)(this.gcDepartment)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDepartment)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditTower)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            this.xtraTabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ppGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabMain)).BeginInit();
            this.tabMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // gcDepartment
            // 
            this.gcDepartment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcDepartment.Location = new System.Drawing.Point(0, 0);
            this.gcDepartment.MainView = this.gvDepartment;
            this.gcDepartment.Name = "gcDepartment";
            this.gcDepartment.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.lookUpEditTower});
            this.gcDepartment.Size = new System.Drawing.Size(0, 0);
            this.gcDepartment.TabIndex = 1;
            this.gcDepartment.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvDepartment});
            // 
            // gvDepartment
            // 
            this.gvDepartment.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn7,
            this.gridColumn1});
            this.gvDepartment.DetailHeight = 284;
            this.gvDepartment.GridControl = this.gcDepartment;
            this.gvDepartment.Name = "gvDepartment";
            this.gvDepartment.OptionsDetail.EnableMasterViewMode = false;
            this.gvDepartment.OptionsView.ColumnAutoWidth = false;
            this.gvDepartment.OptionsView.ShowAutoFilterRow = true;
            this.gvDepartment.OptionsView.ShowGroupPanel = false;
            this.gvDepartment.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gvDepartment_FocusedRowChanged);
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "Id";
            this.gridColumn7.FieldName = "Id";
            this.gridColumn7.MinWidth = 17;
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.Width = 64;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Tên phòng";
            this.gridColumn1.FieldName = "Name";
            this.gridColumn1.MinWidth = 17;
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 254;
            // 
            // lookUpEditTower
            // 
            this.lookUpEditTower.AutoHeight = false;
            this.lookUpEditTower.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEditTower.DisplayMember = "TenTN";
            this.lookUpEditTower.Name = "lookUpEditTower";
            this.lookUpEditTower.NullText = "";
            this.lookUpEditTower.ValueMember = "MaTN";
            // 
            // barManager1
            // 
            this.barManager1.AllowCustomization = false;
            this.barManager1.AllowMoveBarOnToolbar = false;
            this.barManager1.AllowQuickCustomization = false;
            this.barManager1.AllowShowToolbarsPopup = false;
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
            this.itemLuu,
            this.itemNap,
            this.itemStaffAdd,
            this.itemStaffSave,
            this.itemStaffDelete,
            this.itemClose,
            this.itemDelGroup,
            this.itemSaveGroup,
            this.itemSave});
            this.barManager1.MaxItemId = 16;
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemNap, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemSave, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemClose, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.DrawDragBorder = false;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Tools";
            // 
            // itemNap
            // 
            this.itemNap.Caption = "Nạp";
            this.itemNap.Id = 3;
            this.itemNap.ImageOptions.Image = global::Building.AppVime.Properties.Resources.Refresh_icon__1_;
            this.itemNap.ImageOptions.ImageIndex = 3;
            this.itemNap.Name = "itemNap";
            this.itemNap.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemNap_ItemClick);
            // 
            // itemSave
            // 
            this.itemSave.Caption = "Lưu";
            this.itemSave.Id = 15;
            this.itemSave.ImageOptions.Image = global::Building.AppVime.Properties.Resources.Save_as_icon;
            this.itemSave.ImageOptions.ImageIndex = 7;
            this.itemSave.Name = "itemSave";
            this.itemSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemSave_ItemClick);
            // 
            // itemClose
            // 
            this.itemClose.Caption = "Đóng";
            this.itemClose.Id = 12;
            this.itemClose.ImageOptions.Image = global::Building.AppVime.Properties.Resources.delete_Close_icon1;
            this.itemClose.ImageOptions.ImageIndex = 6;
            this.itemClose.Name = "itemClose";
            this.itemClose.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemClose_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(822, 31);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 475);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(822, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 31);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 444);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(822, 31);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 444);
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.Images.SetKeyName(0, "add.png");
            this.imageCollection1.Images.SetKeyName(1, "delete.png");
            this.imageCollection1.Images.SetKeyName(2, "edit-blue.png");
            this.imageCollection1.Images.SetKeyName(3, "refresh.png");
            this.imageCollection1.Images.SetKeyName(4, "setting.png");
            this.imageCollection1.Images.SetKeyName(5, "mind_map-16.png");
            this.imageCollection1.Images.SetKeyName(6, "close.png");
            this.imageCollection1.Images.SetKeyName(7, "save.gif");
            // 
            // itemLuu
            // 
            this.itemLuu.Caption = "Lưu";
            this.itemLuu.Id = 2;
            this.itemLuu.ImageOptions.ImageIndex = 2;
            this.itemLuu.Name = "itemLuu";
            // 
            // itemStaffAdd
            // 
            this.itemStaffAdd.Caption = "Thêm nhiều";
            this.itemStaffAdd.Id = 7;
            this.itemStaffAdd.ImageOptions.ImageIndex = 0;
            this.itemStaffAdd.Name = "itemStaffAdd";
            // 
            // itemStaffSave
            // 
            this.itemStaffSave.Caption = "Lưu";
            this.itemStaffSave.Id = 8;
            this.itemStaffSave.ImageOptions.ImageIndex = 5;
            this.itemStaffSave.Name = "itemStaffSave";
            // 
            // itemStaffDelete
            // 
            this.itemStaffDelete.Caption = "Xóa";
            this.itemStaffDelete.Id = 9;
            this.itemStaffDelete.ImageOptions.ImageIndex = 1;
            this.itemStaffDelete.Name = "itemStaffDelete";
            // 
            // itemDelGroup
            // 
            this.itemDelGroup.Caption = "Xóa nhóm";
            this.itemDelGroup.Id = 13;
            this.itemDelGroup.Name = "itemDelGroup";
            this.itemDelGroup.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemDelGroup_ItemClick);
            // 
            // itemSaveGroup
            // 
            this.itemSaveGroup.Caption = "Lưu";
            this.itemSaveGroup.Id = 14;
            this.itemSaveGroup.Name = "itemSaveGroup";
            this.itemSaveGroup.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemSaveGroup_ItemClick);
            // 
            // xtraTabPage2
            // 
            this.xtraTabPage2.Controls.Add(this.gcGroup);
            this.xtraTabPage2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.xtraTabPage2.Name = "xtraTabPage2";
            this.xtraTabPage2.Padding = new System.Windows.Forms.Padding(1);
            this.barManager1.SetPopupContextMenu(this.xtraTabPage2, this.ppGroup);
            this.xtraTabPage2.Size = new System.Drawing.Size(816, 411);
            this.xtraTabPage2.Text = "Nhóm công việc";
            // 
            // gcGroup
            // 
            this.gcGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcGroup.Location = new System.Drawing.Point(1, 1);
            this.gcGroup.MainView = this.gvGroup;
            this.gcGroup.Name = "gcGroup";
            this.gcGroup.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemLookUpEdit2});
            this.gcGroup.Size = new System.Drawing.Size(814, 409);
            this.gcGroup.TabIndex = 2;
            this.gcGroup.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvGroup});
            // 
            // gvGroup
            // 
            this.gvGroup.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn5,
            this.gridColumn10,
            this.colDepartmentId});
            this.gvGroup.DetailHeight = 284;
            this.gvGroup.GridControl = this.gcGroup;
            this.gvGroup.Name = "gvGroup";
            this.gvGroup.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.True;
            this.gvGroup.OptionsDetail.EnableMasterViewMode = false;
            this.gvGroup.OptionsView.ColumnAutoWidth = false;
            this.gvGroup.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            this.gvGroup.OptionsView.ShowAutoFilterRow = true;
            this.gvGroup.OptionsView.ShowGroupPanel = false;
            this.gvGroup.InitNewRow += new DevExpress.XtraGrid.Views.Grid.InitNewRowEventHandler(this.gvGroup_InitNewRow);
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Tên nhóm";
            this.gridColumn5.FieldName = "Name";
            this.gridColumn5.MinWidth = 17;
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 0;
            this.gridColumn5.Width = 325;
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "Id";
            this.gridColumn10.FieldName = "Id";
            this.gridColumn10.MinWidth = 17;
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.Width = 64;
            // 
            // colDepartmentId
            // 
            this.colDepartmentId.Caption = "DepartmentId";
            this.colDepartmentId.FieldName = "DepartmentId";
            this.colDepartmentId.MinWidth = 17;
            this.colDepartmentId.Name = "colDepartmentId";
            this.colDepartmentId.Width = 64;
            // 
            // repositoryItemLookUpEdit2
            // 
            this.repositoryItemLookUpEdit2.AutoHeight = false;
            this.repositoryItemLookUpEdit2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemLookUpEdit2.DisplayMember = "TenTN";
            this.repositoryItemLookUpEdit2.Name = "repositoryItemLookUpEdit2";
            this.repositoryItemLookUpEdit2.NullText = "";
            this.repositoryItemLookUpEdit2.ValueMember = "MaTN";
            // 
            // ppGroup
            // 
            this.ppGroup.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.itemDelGroup),
            new DevExpress.XtraBars.LinkPersistInfo(this.itemSaveGroup, true)});
            this.ppGroup.Manager = this.barManager1;
            this.ppGroup.MenuCaption = "Tùy chọn";
            this.ppGroup.Name = "ppGroup";
            this.ppGroup.ShowCaption = true;
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.Panel2;
            this.splitContainerControl1.Horizontal = false;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 31);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.gcDepartment);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.tabMain);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(822, 444);
            this.splitContainerControl1.SplitterPosition = 443;
            this.splitContainerControl1.TabIndex = 7;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // tabMain
            // 
            this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMain.Location = new System.Drawing.Point(0, 0);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedTabPage = this.xtraTabPage2;
            this.tabMain.Size = new System.Drawing.Size(822, 439);
            this.tabMain.TabIndex = 4;
            this.tabMain.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage2});
            // 
            // frmGroupProcess
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(822, 475);
            this.Controls.Add(this.splitContainerControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.Name = "frmGroupProcess";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cài đặt Nhóm công việc";
            this.Load += new System.EventHandler(this.frmManager_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gcDepartment)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDepartment)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditTower)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            this.xtraTabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ppGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tabMain)).EndInit();
            this.tabMain.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gcDepartment;
        private DevExpress.XtraGrid.Views.Grid.GridView gvDepartment;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lookUpEditTower;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarButtonItem itemNap;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.Utils.ImageCollection imageCollection1;
        private DevExpress.XtraBars.BarButtonItem itemLuu;
        private DevExpress.XtraBars.BarButtonItem itemStaffAdd;
        private DevExpress.XtraBars.BarButtonItem itemStaffSave;
        private DevExpress.XtraBars.BarButtonItem itemStaffDelete;
        private DevExpress.XtraBars.BarButtonItem itemClose;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraTab.XtraTabControl tabMain;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage2;
        private DevExpress.XtraGrid.GridControl gcGroup;
        private DevExpress.XtraGrid.Views.Grid.GridView gvGroup;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEdit2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraBars.BarButtonItem itemDelGroup;
        private DevExpress.XtraBars.PopupMenu ppGroup;
        private DevExpress.XtraBars.BarButtonItem itemSaveGroup;
        private DevExpress.XtraGrid.Columns.GridColumn colDepartmentId;
        private DevExpress.XtraBars.BarButtonItem itemSave;
    }
}