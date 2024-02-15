namespace Building.AppVime.Resident
{
    partial class frmManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmManager));
            this.gcResident = new DevExpress.XtraGrid.GridControl();
            this.gvResident = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFullname = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lookUpEditStatus = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lookNhanVien = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.repositoryItemMemoExEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.itemToaNha = new DevExpress.XtraBars.BarEditItem();
            this.lkToaNha = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.itemBlock = new DevExpress.XtraBars.BarEditItem();
            this.lookUpBlock = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.itemRefresh = new DevExpress.XtraBars.BarButtonItem();
            this.itemChoice = new DevExpress.XtraBars.BarButtonItem();
            this.itemClose = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
            this.itemEdit = new DevExpress.XtraBars.BarButtonItem();
            this.itemDelete = new DevExpress.XtraBars.BarButtonItem();
            this.itemImport = new DevExpress.XtraBars.BarButtonItem();
            this.barEditItem1 = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemCheckEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.cmbKyBaoCao = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.dateTuNgay = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.dateDenNgay = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.linqInstantFeedbackSource1 = new DevExpress.Data.Linq.LinqInstantFeedbackSource();
            this.chkAll = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.gcResident)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvResident)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookNhanVien)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoExEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkToaNha)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpBlock)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbKyBaoCao)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTuNgay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTuNgay.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDenNgay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDenNgay.CalendarTimeProperties)).BeginInit();
            this.SuspendLayout();
            // 
            // gcResident
            // 
            this.gcResident.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gcResident.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gcResident.Location = new System.Drawing.Point(0, 54);
            this.gcResident.MainView = this.gvResident;
            this.gcResident.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gcResident.Name = "gcResident";
            this.gcResident.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.lookNhanVien,
            this.repositoryItemMemoExEdit1,
            this.lookUpEditStatus,
            this.repositoryItemCheckEdit1});
            this.gcResident.Size = new System.Drawing.Size(959, 463);
            this.gcResident.TabIndex = 1;
            this.gcResident.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvResident});
            // 
            // gvResident
            // 
            this.gvResident.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn11,
            this.colFullname,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn6,
            this.gridColumn7,
            this.gridColumn8,
            this.gridColumn9,
            this.gridColumn10,
            this.gridColumn12,
            this.gridColumn1,
            this.gridColumn13});
            this.gvResident.DetailHeight = 284;
            this.gvResident.GridControl = this.gcResident;
            this.gvResident.GroupPanelText = "Kéo một vài cột lên đây để xem theo nhóm";
            this.gvResident.Name = "gvResident";
            this.gvResident.OptionsPrint.AutoWidth = false;
            this.gvResident.OptionsSelection.MultiSelect = true;
            this.gvResident.OptionsView.ColumnAutoWidth = false;
            this.gvResident.OptionsView.ShowAutoFilterRow = true;
            this.gvResident.OptionsView.ShowFooter = true;
            this.gvResident.OptionsView.ShowGroupPanel = false;
            this.gvResident.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.gvResident_RowCellClick);
            // 
            // gridColumn11
            // 
            this.gridColumn11.Caption = "STT";
            this.gridColumn11.MinWidth = 17;
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "", "{0:#} dòng")});
            this.gridColumn11.Width = 40;
            // 
            // colFullname
            // 
            this.colFullname.Caption = "Họ và tên";
            this.colFullname.FieldName = "FullName";
            this.colFullname.MinWidth = 17;
            this.colFullname.Name = "colFullname";
            this.colFullname.OptionsColumn.AllowEdit = false;
            this.colFullname.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.colFullname.Visible = true;
            this.colFullname.VisibleIndex = 1;
            this.colFullname.Width = 129;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Số điện thoại";
            this.gridColumn2.FieldName = "Phone";
            this.gridColumn2.MinWidth = 17;
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 4;
            this.gridColumn2.Width = 80;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Ngày tham gia";
            this.gridColumn3.DisplayFormat.FormatString = "{0:HH:mm | dd/MM/yyyy}";
            this.gridColumn3.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gridColumn3.FieldName = "DateOfJoin";
            this.gridColumn3.MinWidth = 17;
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 5;
            this.gridColumn3.Width = 104;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Nhân viên đăng ký";
            this.gridColumn4.FieldName = "Employeer";
            this.gridColumn4.MinWidth = 17;
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 6;
            this.gridColumn4.Width = 120;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Trạng thái";
            this.gridColumn5.ColumnEdit = this.lookUpEditStatus;
            this.gridColumn5.FieldName = "StatusId";
            this.gridColumn5.MinWidth = 17;
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 7;
            this.gridColumn5.Width = 105;
            // 
            // lookUpEditStatus
            // 
            this.lookUpEditStatus.AutoHeight = false;
            this.lookUpEditStatus.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEditStatus.DisplayMember = "Name";
            this.lookUpEditStatus.Name = "lookUpEditStatus";
            this.lookUpEditStatus.NullText = "";
            this.lookUpEditStatus.ShowHeader = false;
            this.lookUpEditStatus.ValueMember = "Id";
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "SL thông báo";
            this.gridColumn6.FieldName = "AmountNotifyUnread";
            this.gridColumn6.MinWidth = 17;
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.OptionsColumn.AllowEdit = false;
            this.gridColumn6.Width = 73;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "SL tin tức";
            this.gridColumn7.FieldName = "AmountNewsUnread";
            this.gridColumn7.MinWidth = 17;
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.OptionsColumn.AllowEdit = false;
            this.gridColumn7.Width = 64;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "Ngày xử lý";
            this.gridColumn8.DisplayFormat.FormatString = "{0:HH:mm | dd/MM/yyyy}";
            this.gridColumn8.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gridColumn8.FieldName = "DateOfProcess";
            this.gridColumn8.MinWidth = 17;
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.OptionsColumn.AllowEdit = false;
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 8;
            this.gridColumn8.Width = 112;
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "Nhân viên xử lý";
            this.gridColumn9.FieldName = "EmployeerProcess";
            this.gridColumn9.MinWidth = 17;
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.OptionsColumn.AllowEdit = false;
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 9;
            this.gridColumn9.Width = 111;
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "Nội dung xử lý";
            this.gridColumn10.FieldName = "DescriptionProcess";
            this.gridColumn10.MinWidth = 17;
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.OptionsColumn.AllowEdit = false;
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 10;
            this.gridColumn10.Width = 155;
            // 
            // gridColumn12
            // 
            this.gridColumn12.Caption = " ";
            this.gridColumn12.FieldName = "IsCheck";
            this.gridColumn12.MinWidth = 17;
            this.gridColumn12.Name = "gridColumn12";
            this.gridColumn12.Visible = true;
            this.gridColumn12.VisibleIndex = 0;
            this.gridColumn12.Width = 31;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Mã căn hộ";
            this.gridColumn1.FieldName = "Code";
            this.gridColumn1.MinWidth = 17;
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 2;
            this.gridColumn1.Width = 80;
            // 
            // gridColumn13
            // 
            this.gridColumn13.Caption = "Tầng";
            this.gridColumn13.FieldName = "Floor";
            this.gridColumn13.MinWidth = 17;
            this.gridColumn13.Name = "gridColumn13";
            this.gridColumn13.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.gridColumn13.Visible = true;
            this.gridColumn13.VisibleIndex = 3;
            this.gridColumn13.Width = 64;
            // 
            // lookNhanVien
            // 
            this.lookNhanVien.AutoHeight = false;
            this.lookNhanVien.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookNhanVien.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("HoTenNV", "#")});
            this.lookNhanVien.DisplayMember = "HoTenNV";
            this.lookNhanVien.Name = "lookNhanVien";
            this.lookNhanVien.NullText = "";
            this.lookNhanVien.ShowHeader = false;
            this.lookNhanVien.ValueMember = "MaNV";
            // 
            // repositoryItemMemoExEdit1
            // 
            this.repositoryItemMemoExEdit1.AutoHeight = false;
            this.repositoryItemMemoExEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemMemoExEdit1.Name = "repositoryItemMemoExEdit1";
            this.repositoryItemMemoExEdit1.PopupFormSize = new System.Drawing.Size(257, 0);
            this.repositoryItemMemoExEdit1.ReadOnly = true;
            this.repositoryItemMemoExEdit1.ShowDropDown = DevExpress.XtraEditors.Controls.ShowDropDown.DoubleClick;
            this.repositoryItemMemoExEdit1.ShowIcon = false;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
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
            this.itemEdit,
            this.itemDelete,
            this.itemRefresh,
            this.itemToaNha,
            this.itemImport,
            this.itemChoice,
            this.itemClose,
            this.itemBlock,
            this.barEditItem1});
            this.barManager1.MaxItemId = 40;
            this.barManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.cmbKyBaoCao,
            this.dateTuNgay,
            this.dateDenNgay,
            this.lkToaNha,
            this.lookUpBlock,
            this.repositoryItemCheckEdit2});
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemToaNha, DevExpress.XtraBars.BarItemPaintStyle.Caption),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemBlock, DevExpress.XtraBars.BarItemPaintStyle.Caption),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemRefresh, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemChoice, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemClose, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar1.OptionsBar.DrawDragBorder = false;
            this.bar1.OptionsBar.MultiLine = true;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Tools";
            // 
            // itemToaNha
            // 
            this.itemToaNha.Caption = "Dự án";
            this.itemToaNha.Edit = this.lkToaNha;
            this.itemToaNha.EditWidth = 213;
            this.itemToaNha.Id = 11;
            this.itemToaNha.Name = "itemToaNha";
            this.itemToaNha.EditValueChanged += new System.EventHandler(this.itemToaNha_EditValueChanged);
            // 
            // lkToaNha
            // 
            this.lkToaNha.AutoHeight = false;
            this.lkToaNha.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkToaNha.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenTN", "Name1")});
            this.lkToaNha.DisplayMember = "TenTN";
            this.lkToaNha.DropDownRows = 15;
            this.lkToaNha.Name = "lkToaNha";
            this.lkToaNha.NullText = "";
            this.lkToaNha.PopupFormMinSize = new System.Drawing.Size(300, 0);
            this.lkToaNha.ReadOnly = true;
            this.lkToaNha.ShowHeader = false;
            this.lkToaNha.ValueMember = "MaTN";
            // 
            // itemBlock
            // 
            this.itemBlock.Caption = "Khu/ Block";
            this.itemBlock.Edit = this.lookUpBlock;
            this.itemBlock.EditWidth = 117;
            this.itemBlock.Id = 38;
            this.itemBlock.Name = "itemBlock";
            this.itemBlock.EditValueChanged += new System.EventHandler(this.itemBlock_EditValueChanged);
            // 
            // lookUpBlock
            // 
            this.lookUpBlock.AutoHeight = false;
            this.lookUpBlock.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpBlock.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "Name1")});
            this.lookUpBlock.DisplayMember = "Name";
            this.lookUpBlock.Name = "lookUpBlock";
            this.lookUpBlock.NullText = "[Chọn Block]";
            this.lookUpBlock.ShowHeader = false;
            this.lookUpBlock.ValueMember = "Id";
            // 
            // itemRefresh
            // 
            this.itemRefresh.Caption = "Nạp";
            this.itemRefresh.Id = 3;
            this.itemRefresh.ImageOptions.Image = global::Building.AppVime.Properties.Resources.Refresh_icon__1_;
            this.itemRefresh.ImageOptions.ImageIndex = 11;
            this.itemRefresh.Name = "itemRefresh";
            this.itemRefresh.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemRefresh_ItemClick);
            // 
            // itemChoice
            // 
            this.itemChoice.Caption = "Chọn và Đóng";
            this.itemChoice.Id = 36;
            this.itemChoice.ImageOptions.Image = global::Building.AppVime.Properties.Resources.Save_as_icon;
            this.itemChoice.ImageOptions.ImageIndex = 8;
            this.itemChoice.Name = "itemChoice";
            this.itemChoice.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemChoice_ItemClick);
            // 
            // itemClose
            // 
            this.itemClose.Caption = "Đóng";
            this.itemClose.Id = 37;
            this.itemClose.ImageOptions.Image = global::Building.AppVime.Properties.Resources.delete_Close_icon1;
            this.itemClose.ImageOptions.ImageIndex = 7;
            this.itemClose.Name = "itemClose";
            this.itemClose.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemClose_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.barDockControlTop.Size = new System.Drawing.Size(959, 31);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 517);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.barDockControlBottom.Size = new System.Drawing.Size(959, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 31);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 486);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(959, 31);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 486);
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.Images.SetKeyName(0, "108.gif");
            this.imageCollection1.Images.SetKeyName(1, "001.gif");
            this.imageCollection1.Images.SetKeyName(2, "005.gif");
            this.imageCollection1.Images.SetKeyName(3, "004.gif");
            this.imageCollection1.Images.SetKeyName(4, "print.png");
            this.imageCollection1.Images.SetKeyName(5, "014.gif");
            this.imageCollection1.Images.SetKeyName(6, "document28.png");
            this.imageCollection1.Images.SetKeyName(7, "unchecked.png");
            this.imageCollection1.Images.SetKeyName(8, "success.png");
            this.imageCollection1.Images.SetKeyName(9, "document286.png");
            this.imageCollection1.Images.SetKeyName(10, "file.png");
            this.imageCollection1.Images.SetKeyName(11, "refresh.png");
            this.imageCollection1.Images.SetKeyName(12, "process.png");
            this.imageCollection1.Images.SetKeyName(13, "upload-16.png");
            // 
            // itemEdit
            // 
            this.itemEdit.Caption = "Sửa";
            this.itemEdit.Id = 1;
            this.itemEdit.ImageOptions.ImageIndex = 2;
            this.itemEdit.Name = "itemEdit";
            // 
            // itemDelete
            // 
            this.itemDelete.Caption = "Xóa";
            this.itemDelete.Id = 2;
            this.itemDelete.ImageOptions.ImageIndex = 3;
            this.itemDelete.Name = "itemDelete";
            // 
            // itemImport
            // 
            this.itemImport.Caption = "Import";
            this.itemImport.Id = 20;
            this.itemImport.ImageOptions.ImageIndex = 5;
            this.itemImport.Name = "itemImport";
            // 
            // barEditItem1
            // 
            this.barEditItem1.Caption = "Tất cả";
            this.barEditItem1.Edit = this.repositoryItemCheckEdit2;
            this.barEditItem1.Id = 39;
            this.barEditItem1.Name = "barEditItem1";
            // 
            // repositoryItemCheckEdit2
            // 
            this.repositoryItemCheckEdit2.AutoHeight = false;
            this.repositoryItemCheckEdit2.Name = "repositoryItemCheckEdit2";
            // 
            // cmbKyBaoCao
            // 
            this.cmbKyBaoCao.AutoHeight = false;
            this.cmbKyBaoCao.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbKyBaoCao.Name = "cmbKyBaoCao";
            this.cmbKyBaoCao.NullText = "Kỳ báo cáo";
            this.cmbKyBaoCao.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
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
            // linqInstantFeedbackSource1
            // 
            this.linqInstantFeedbackSource1.KeyExpression = "ID";
            // 
            // chkAll
            // 
            this.chkAll.AutoSize = true;
            this.chkAll.Location = new System.Drawing.Point(21, 33);
            this.chkAll.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chkAll.Name = "chkAll";
            this.chkAll.Size = new System.Drawing.Size(150, 17);
            this.chkAll.TabIndex = 6;
            this.chkAll.Text = "Tất cả (theo điều kiện lọc)";
            this.chkAll.UseVisualStyleBackColor = true;
            this.chkAll.CheckedChanged += new System.EventHandler(this.chkAll_CheckedChanged);
            // 
            // frmManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(959, 517);
            this.Controls.Add(this.chkAll);
            this.Controls.Add(this.gcResident);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.Name = "frmManager";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chọn Cư dân";
            this.Load += new System.EventHandler(this.frmManager_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gcResident)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvResident)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookNhanVien)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoExEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkToaNha)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpBlock)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbKyBaoCao)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTuNgay.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTuNgay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDenNgay.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDenNgay)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gcResident;
        private DevExpress.XtraGrid.Views.Grid.GridView gvResident;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lookNhanVien;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit repositoryItemMemoExEdit1;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarEditItem itemToaNha;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lkToaNha;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox cmbKyBaoCao;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit dateTuNgay;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit dateDenNgay;
        private DevExpress.XtraBars.BarButtonItem itemRefresh;
        private DevExpress.XtraBars.BarButtonItem itemEdit;
        private DevExpress.XtraBars.BarButtonItem itemDelete;
        private DevExpress.XtraBars.BarButtonItem itemImport;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.Utils.ImageCollection imageCollection1;
        private DevExpress.XtraGrid.Columns.GridColumn colFullname;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.Data.Linq.LinqInstantFeedbackSource linqInstantFeedbackSource1;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lookUpEditStatus;
        private DevExpress.XtraBars.BarButtonItem itemChoice;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn12;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraBars.BarButtonItem itemClose;
        private DevExpress.XtraBars.BarEditItem itemBlock;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lookUpBlock;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn13;
        private System.Windows.Forms.CheckBox chkAll;
        private DevExpress.XtraBars.BarEditItem barEditItem1;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit2;
    }
}