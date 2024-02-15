namespace Building.AppVime.Tower
{
    partial class frmZone
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmZone));
            this.gcZone = new DevExpress.XtraGrid.GridControl();
            this.gvZone = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lookUpEditTower = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.itemService = new DevExpress.XtraBars.BarEditItem();
            this.lookUpService = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.itemNap = new DevExpress.XtraBars.BarButtonItem();
            this.itemThem = new DevExpress.XtraBars.BarButtonItem();
            this.itemEdit = new DevExpress.XtraBars.BarButtonItem();
            this.itemXoa = new DevExpress.XtraBars.BarButtonItem();
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
            this.itemTime_Add = new DevExpress.XtraBars.BarButtonItem();
            this.itemTime_Edit = new DevExpress.XtraBars.BarButtonItem();
            this.itemTime_Del = new DevExpress.XtraBars.BarButtonItem();
            this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
            this.gcZoneTime = new DevExpress.XtraGrid.GridControl();
            this.gvZoneTime = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemLookUpEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.ppZoneTime = new DevExpress.XtraBars.PopupMenu(this.components);
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.tabMain = new DevExpress.XtraTab.XtraTabControl();
            ((System.ComponentModel.ISupportInitialize)(this.gcZone)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvZone)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditTower)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpService)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            this.xtraTabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcZoneTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvZoneTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ppZoneTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabMain)).BeginInit();
            this.tabMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // gcZone
            // 
            this.gcZone.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcZone.Location = new System.Drawing.Point(0, 0);
            this.gcZone.MainView = this.gvZone;
            this.gcZone.Name = "gcZone";
            this.gcZone.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.lookUpEditTower});
            this.gcZone.Size = new System.Drawing.Size(917, 223);
            this.gcZone.TabIndex = 1;
            this.gcZone.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvZone});
            // 
            // gvZone
            // 
            this.gvZone.ColumnPanelRowHeight = 28;
            this.gvZone.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn7,
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn10});
            this.gvZone.DetailHeight = 284;
            this.gvZone.GridControl = this.gcZone;
            this.gvZone.Name = "gvZone";
            this.gvZone.OptionsDetail.EnableMasterViewMode = false;
            this.gvZone.OptionsView.ColumnAutoWidth = false;
            this.gvZone.OptionsView.ShowAutoFilterRow = true;
            this.gvZone.OptionsView.ShowGroupPanel = false;
            this.gvZone.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gvZone_FocusedRowChanged);
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
            this.gridColumn1.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn1.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridColumn1.Caption = "Tên khu";
            this.gridColumn1.FieldName = "Name";
            this.gridColumn1.MinWidth = 17;
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 178;
            // 
            // gridColumn2
            // 
            this.gridColumn2.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn2.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridColumn2.Caption = "Số lượt đặt chỗ tối đa";
            this.gridColumn2.FieldName = "AmountTimesBooking";
            this.gridColumn2.MinWidth = 17;
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 2;
            this.gridColumn2.Width = 140;
            // 
            // gridColumn3
            // 
            this.gridColumn3.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn3.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridColumn3.Caption = "Tên khu (Tiếng Anh)";
            this.gridColumn3.FieldName = "NameEN";
            this.gridColumn3.MinWidth = 17;
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 1;
            this.gridColumn3.Width = 197;
            // 
            // gridColumn10
            // 
            this.gridColumn10.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn10.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridColumn10.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn10.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridColumn10.Caption = "Đăng ký đến hết thời gian của suất";
            this.gridColumn10.FieldName = "IsBookToEndTime";
            this.gridColumn10.MinWidth = 17;
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.OptionsColumn.AllowEdit = false;
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 3;
            this.gridColumn10.Width = 112;
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
            this.itemThem,
            this.itemXoa,
            this.itemEdit,
            this.itemStaffAdd,
            this.itemStaffSave,
            this.itemStaffDelete,
            this.itemClose,
            this.itemTime_Add,
            this.itemTime_Edit,
            this.itemTime_Del,
            this.itemService});
            this.barManager1.MaxItemId = 17;
            this.barManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.lookUpService});
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.itemService, true),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemNap, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemThem, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemEdit, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemXoa, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemClose, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.DrawDragBorder = false;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Tools";
            // 
            // itemService
            // 
            this.itemService.Edit = this.lookUpService;
            this.itemService.EditWidth = 197;
            this.itemService.Id = 16;
            this.itemService.Name = "itemService";
            this.itemService.EditValueChanged += new System.EventHandler(this.itemService_EditValueChanged);
            // 
            // lookUpService
            // 
            this.lookUpService.AutoHeight = false;
            this.lookUpService.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpService.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "Name")});
            this.lookUpService.DisplayMember = "Name";
            this.lookUpService.Name = "lookUpService";
            this.lookUpService.NullText = "";
            this.lookUpService.ShowHeader = false;
            this.lookUpService.ValueMember = "ServiceId";
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
            // itemThem
            // 
            this.itemThem.Caption = "Thêm";
            this.itemThem.Id = 4;
            this.itemThem.ImageOptions.Image = global::Building.AppVime.Properties.Resources.Add_icon_blue;
            this.itemThem.ImageOptions.ImageIndex = 0;
            this.itemThem.Name = "itemThem";
            this.itemThem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemThem_ItemClick);
            // 
            // itemEdit
            // 
            this.itemEdit.Caption = "Sửa";
            this.itemEdit.Id = 6;
            this.itemEdit.ImageOptions.Image = global::Building.AppVime.Properties.Resources.shape_square_edit_icon;
            this.itemEdit.ImageOptions.ImageIndex = 2;
            this.itemEdit.Name = "itemEdit";
            this.itemEdit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemEdit_ItemClick);
            // 
            // itemXoa
            // 
            this.itemXoa.Caption = "Xóa";
            this.itemXoa.Id = 5;
            this.itemXoa.ImageOptions.Image = global::Building.AppVime.Properties.Resources.bin_red;
            this.itemXoa.ImageOptions.ImageIndex = 1;
            this.itemXoa.Name = "itemXoa";
            this.itemXoa.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemXoa_ItemClick);
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
            this.barDockControlTop.Size = new System.Drawing.Size(917, 31);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 502);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(917, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 31);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 471);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(917, 31);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 471);
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
            // itemTime_Add
            // 
            this.itemTime_Add.Caption = "Thêm";
            this.itemTime_Add.Id = 13;
            this.itemTime_Add.Name = "itemTime_Add";
            this.itemTime_Add.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemTime_Add_ItemClick);
            // 
            // itemTime_Edit
            // 
            this.itemTime_Edit.Caption = "Sửa";
            this.itemTime_Edit.Id = 14;
            this.itemTime_Edit.Name = "itemTime_Edit";
            this.itemTime_Edit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemTime_Edit_ItemClick);
            // 
            // itemTime_Del
            // 
            this.itemTime_Del.Caption = "Xóa";
            this.itemTime_Del.Id = 15;
            this.itemTime_Del.Name = "itemTime_Del";
            this.itemTime_Del.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemTime_Del_ItemClick);
            // 
            // xtraTabPage2
            // 
            this.xtraTabPage2.Controls.Add(this.gcZoneTime);
            this.xtraTabPage2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.xtraTabPage2.Name = "xtraTabPage2";
            this.xtraTabPage2.Padding = new System.Windows.Forms.Padding(1);
            this.barManager1.SetPopupContextMenu(this.xtraTabPage2, this.ppZoneTime);
            this.xtraTabPage2.Size = new System.Drawing.Size(911, 215);
            this.xtraTabPage2.Text = "Chi tiết";
            // 
            // gcZoneTime
            // 
            this.gcZoneTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcZoneTime.Location = new System.Drawing.Point(1, 1);
            this.gcZoneTime.MainView = this.gvZoneTime;
            this.gcZoneTime.Name = "gcZoneTime";
            this.gcZoneTime.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemLookUpEdit1});
            this.gcZoneTime.Size = new System.Drawing.Size(909, 213);
            this.gcZoneTime.TabIndex = 2;
            this.gcZoneTime.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvZoneTime});
            // 
            // gvZoneTime
            // 
            this.gvZoneTime.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn4,
            this.gridColumn6,
            this.gridColumn5,
            this.gridColumn8,
            this.gridColumn9});
            this.gvZoneTime.DetailHeight = 284;
            this.gvZoneTime.GridControl = this.gcZoneTime;
            this.gvZoneTime.Name = "gvZoneTime";
            this.gvZoneTime.OptionsDetail.EnableMasterViewMode = false;
            this.gvZoneTime.OptionsView.ColumnAutoWidth = false;
            this.gvZoneTime.OptionsView.ShowAutoFilterRow = true;
            this.gvZoneTime.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Id";
            this.gridColumn4.FieldName = "Id";
            this.gridColumn4.MinWidth = 17;
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Width = 64;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "STT";
            this.gridColumn6.FieldName = "NumberIndex";
            this.gridColumn6.MinWidth = 17;
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.OptionsColumn.AllowEdit = false;
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 0;
            this.gridColumn6.Width = 51;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Tên suất";
            this.gridColumn5.FieldName = "Name";
            this.gridColumn5.MinWidth = 17;
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            this.gridColumn5.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 1;
            this.gridColumn5.Width = 178;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "Từ";
            this.gridColumn8.FieldName = "TimeFrom";
            this.gridColumn8.MinWidth = 17;
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.OptionsColumn.AllowEdit = false;
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 2;
            this.gridColumn8.Width = 81;
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "Đến";
            this.gridColumn9.FieldName = "TimeTo";
            this.gridColumn9.MinWidth = 17;
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.OptionsColumn.AllowEdit = false;
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 3;
            this.gridColumn9.Width = 76;
            // 
            // repositoryItemLookUpEdit1
            // 
            this.repositoryItemLookUpEdit1.AutoHeight = false;
            this.repositoryItemLookUpEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemLookUpEdit1.DisplayMember = "TenTN";
            this.repositoryItemLookUpEdit1.Name = "repositoryItemLookUpEdit1";
            this.repositoryItemLookUpEdit1.NullText = "";
            this.repositoryItemLookUpEdit1.ValueMember = "MaTN";
            // 
            // ppZoneTime
            // 
            this.ppZoneTime.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.itemTime_Add),
            new DevExpress.XtraBars.LinkPersistInfo(this.itemTime_Edit),
            new DevExpress.XtraBars.LinkPersistInfo(this.itemTime_Del)});
            this.ppZoneTime.Manager = this.barManager1;
            this.ppZoneTime.MenuCaption = "Tùy chọn";
            this.ppZoneTime.Name = "ppZoneTime";
            this.ppZoneTime.ShowCaption = true;
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.Panel2;
            this.splitContainerControl1.Horizontal = false;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 31);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.gcZone);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.tabMain);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(917, 471);
            this.splitContainerControl1.SplitterPosition = 243;
            this.splitContainerControl1.TabIndex = 6;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // tabMain
            // 
            this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMain.Location = new System.Drawing.Point(0, 0);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedTabPage = this.xtraTabPage2;
            this.tabMain.Size = new System.Drawing.Size(917, 243);
            this.tabMain.TabIndex = 4;
            this.tabMain.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage2});
            // 
            // frmZone
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(917, 502);
            this.Controls.Add(this.splitContainerControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.Name = "frmZone";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cài đặt Khu vực";
            this.Load += new System.EventHandler(this.frmManager_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gcZone)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvZone)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditTower)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpService)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            this.xtraTabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcZoneTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvZoneTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ppZoneTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tabMain)).EndInit();
            this.tabMain.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gcZone;
        private DevExpress.XtraGrid.Views.Grid.GridView gvZone;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lookUpEditTower;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarButtonItem itemNap;
        private DevExpress.XtraBars.BarButtonItem itemThem;
        private DevExpress.XtraBars.BarButtonItem itemEdit;
        private DevExpress.XtraBars.BarButtonItem itemXoa;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.Utils.ImageCollection imageCollection1;
        private DevExpress.XtraBars.BarButtonItem itemLuu;
        private DevExpress.XtraBars.BarButtonItem itemStaffAdd;
        private DevExpress.XtraBars.BarButtonItem itemStaffSave;
        private DevExpress.XtraBars.BarButtonItem itemStaffDelete;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraBars.BarButtonItem itemClose;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraGrid.GridControl gcZoneTime;
        private DevExpress.XtraGrid.Views.Grid.GridView gvZoneTime;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraBars.BarButtonItem itemTime_Add;
        private DevExpress.XtraBars.BarButtonItem itemTime_Edit;
        private DevExpress.XtraBars.BarButtonItem itemTime_Del;
        private DevExpress.XtraBars.PopupMenu ppZoneTime;
        private DevExpress.XtraTab.XtraTabControl tabMain;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage2;
        private DevExpress.XtraBars.BarEditItem itemService;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lookUpService;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
    }
}