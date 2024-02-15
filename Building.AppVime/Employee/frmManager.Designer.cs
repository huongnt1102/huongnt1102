namespace Building.AppVime.Employee
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
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lookNhanVien = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.repositoryItemMemoExEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit();
            this.lookUpEditStatus = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.itemKyBaoCao = new DevExpress.XtraBars.BarEditItem();
            this.cmbKyBaoCao = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.itemTuNgay = new DevExpress.XtraBars.BarEditItem();
            this.dateTuNgay = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.itemDenNgay = new DevExpress.XtraBars.BarEditItem();
            this.dateDenNgay = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.itemRefresh = new DevExpress.XtraBars.BarButtonItem();
            this.itemAdd = new DevExpress.XtraBars.BarButtonItem();
            this.itemSync = new DevExpress.XtraBars.BarButtonItem();
            this.itemChangePhone = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem_ResetPassword = new DevExpress.XtraBars.BarButtonItem();
            this.itemLock = new DevExpress.XtraBars.BarButtonItem();
            this.itemUnlock = new DevExpress.XtraBars.BarButtonItem();
            this.itemExport = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
            this.itemEdit = new DevExpress.XtraBars.BarButtonItem();
            this.itemDelete = new DevExpress.XtraBars.BarButtonItem();
            this.itemImport = new DevExpress.XtraBars.BarButtonItem();
            this.itemProcess = new DevExpress.XtraBars.BarButtonItem();
            this.itemRegister = new DevExpress.XtraBars.BarButtonItem();
            this.itemRegisterMulti = new DevExpress.XtraBars.BarButtonItem();
            this.lkToaNha = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.linqInstantFeedbackSource1 = new DevExpress.Data.Linq.LinqInstantFeedbackSource();
            this.barButtonItem_KiemTraTonTaiFirebase = new DevExpress.XtraBars.BarButtonItem();
            ((System.ComponentModel.ISupportInitialize)(this.gcResident)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvResident)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookNhanVien)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoExEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbKyBaoCao)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTuNgay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTuNgay.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDenNgay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDenNgay.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkToaNha)).BeginInit();
            this.SuspendLayout();
            // 
            // gcResident
            // 
            this.gcResident.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcResident.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gcResident.Location = new System.Drawing.Point(0, 69);
            this.gcResident.MainView = this.gvResident;
            this.gcResident.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gcResident.Name = "gcResident";
            this.gcResident.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.lookNhanVien,
            this.repositoryItemMemoExEdit1,
            this.lookUpEditStatus});
            this.gcResident.Size = new System.Drawing.Size(1320, 470);
            this.gcResident.TabIndex = 1;
            this.gcResident.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvResident});
            // 
            // gvResident
            // 
            this.gvResident.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn11,
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn12,
            this.gridColumn13});
            this.gvResident.GridControl = this.gcResident;
            this.gvResident.GroupPanelText = "Kéo một vài cột lên đây để xem theo nhóm";
            this.gvResident.Name = "gvResident";
            this.gvResident.OptionsBehavior.Editable = false;
            this.gvResident.OptionsPrint.AutoWidth = false;
            this.gvResident.OptionsSelection.MultiSelect = true;
            this.gvResident.OptionsView.ColumnAutoWidth = false;
            this.gvResident.OptionsView.ShowAutoFilterRow = true;
            this.gvResident.OptionsView.ShowFooter = true;
            this.gvResident.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn11
            // 
            this.gridColumn11.Caption = "STT";
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "", "{0:#} dòng")});
            this.gridColumn11.Width = 47;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Họ và tên";
            this.gridColumn1.FieldName = "FullName";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "FullName", "{0:n0} dòng")});
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 151;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Số điện thoại";
            this.gridColumn2.FieldName = "Phone";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 93;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Ngày tham gia";
            this.gridColumn3.DisplayFormat.FormatString = "{0:HH:mm | dd/MM/yyyy}";
            this.gridColumn3.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gridColumn3.FieldName = "DateOfJoin";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            this.gridColumn3.Width = 121;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Nhân viên đăng ký";
            this.gridColumn4.FieldName = "Employeer";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 3;
            this.gridColumn4.Width = 140;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Khóa?";
            this.gridColumn5.FieldName = "IsLock";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 5;
            // 
            // gridColumn12
            // 
            this.gridColumn12.Caption = "Đăng nhập?";
            this.gridColumn12.FieldName = "IsLogin";
            this.gridColumn12.Name = "gridColumn12";
            this.gridColumn12.OptionsColumn.AllowEdit = false;
            this.gridColumn12.Visible = true;
            this.gridColumn12.VisibleIndex = 4;
            this.gridColumn12.Width = 84;
            // 
            // gridColumn13
            // 
            this.gridColumn13.Caption = "Khóa?";
            this.gridColumn13.FieldName = "IsLock";
            this.gridColumn13.Name = "gridColumn13";
            this.gridColumn13.OptionsColumn.AllowEdit = false;
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
            this.repositoryItemMemoExEdit1.PopupFormSize = new System.Drawing.Size(300, 0);
            this.repositoryItemMemoExEdit1.ReadOnly = true;
            this.repositoryItemMemoExEdit1.ShowDropDown = DevExpress.XtraEditors.Controls.ShowDropDown.DoubleClick;
            this.repositoryItemMemoExEdit1.ShowIcon = false;
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
            this.itemAdd,
            this.itemEdit,
            this.itemDelete,
            this.itemRefresh,
            this.itemKyBaoCao,
            this.itemTuNgay,
            this.itemDenNgay,
            this.itemImport,
            this.itemExport,
            this.itemSync,
            this.itemProcess,
            this.itemRegister,
            this.itemRegisterMulti,
            this.itemLock,
            this.itemUnlock,
            this.itemChangePhone,
            this.barButtonItem1,
            this.barButtonItem2,
            this.barButtonItem_ResetPassword,
            this.barButtonItem_KiemTraTonTaiFirebase});
            this.barManager1.MaxItemId = 45;
            this.barManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.cmbKyBaoCao,
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
            new DevExpress.XtraBars.LinkPersistInfo(this.itemKyBaoCao),
            new DevExpress.XtraBars.LinkPersistInfo(this.itemTuNgay),
            new DevExpress.XtraBars.LinkPersistInfo(this.itemDenNgay),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemRefresh, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemAdd, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemSync, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemChangePhone, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barButtonItem_ResetPassword, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemLock, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemUnlock, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemExport, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barButtonItem1, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barButtonItem2, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barButtonItem_KiemTraTonTaiFirebase, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar1.OptionsBar.DrawDragBorder = false;
            this.bar1.OptionsBar.MultiLine = true;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Tools";
            // 
            // itemKyBaoCao
            // 
            this.itemKyBaoCao.Caption = "barEditItem1";
            this.itemKyBaoCao.Edit = this.cmbKyBaoCao;
            this.itemKyBaoCao.EditWidth = 102;
            this.itemKyBaoCao.Id = 5;
            this.itemKyBaoCao.Name = "itemKyBaoCao";
            // 
            // cmbKyBaoCao
            // 
            this.cmbKyBaoCao.AutoHeight = false;
            this.cmbKyBaoCao.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbKyBaoCao.Name = "cmbKyBaoCao";
            this.cmbKyBaoCao.NullText = "Kỳ báo cáo";
            this.cmbKyBaoCao.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbKyBaoCao.EditValueChanged += new System.EventHandler(this.cmbKyBC_EditValueChanged);
            // 
            // itemTuNgay
            // 
            this.itemTuNgay.Caption = "barEditItem1";
            this.itemTuNgay.Edit = this.dateTuNgay;
            this.itemTuNgay.EditWidth = 92;
            this.itemTuNgay.Id = 6;
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
            this.itemDenNgay.Caption = "barEditItem1";
            this.itemDenNgay.Edit = this.dateDenNgay;
            this.itemDenNgay.EditWidth = 97;
            this.itemDenNgay.Id = 7;
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
            // itemRefresh
            // 
            this.itemRefresh.Caption = "Nạp";
            this.itemRefresh.Id = 3;
            this.itemRefresh.ImageOptions.Image = global::Building.AppVime.Properties.Resources.Refresh_icon__1_;
            this.itemRefresh.ImageOptions.ImageIndex = 0;
            this.itemRefresh.Name = "itemRefresh";
            this.itemRefresh.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemRefresh_ItemClick);
            // 
            // itemAdd
            // 
            this.itemAdd.Caption = "Thêm";
            this.itemAdd.Id = 0;
            this.itemAdd.ImageOptions.Image = global::Building.AppVime.Properties.Resources.Add_icon_blue;
            this.itemAdd.ImageOptions.ImageIndex = 1;
            this.itemAdd.Name = "itemAdd";
            this.itemAdd.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemAdd_ItemClick);
            // 
            // itemSync
            // 
            this.itemSync.Caption = "Đăng ký hàng loạt";
            this.itemSync.Id = 33;
            this.itemSync.ImageOptions.Image = global::Building.AppVime.Properties.Resources.sign_up_icon;
            this.itemSync.ImageOptions.ImageIndex = 13;
            this.itemSync.Name = "itemSync";
            this.itemSync.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemSync_ItemClick);
            // 
            // itemChangePhone
            // 
            this.itemChangePhone.Caption = "Đổi số điện thoại";
            this.itemChangePhone.Id = 39;
            this.itemChangePhone.ImageOptions.Image = global::Building.AppVime.Properties.Resources.phone_icon__1_;
            this.itemChangePhone.ImageOptions.ImageIndex = 11;
            this.itemChangePhone.Name = "itemChangePhone";
            this.itemChangePhone.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemChangePhone_ItemClick);
            // 
            // barButtonItem_ResetPassword
            // 
            this.barButtonItem_ResetPassword.Caption = "Reset mật khẩu";
            this.barButtonItem_ResetPassword.Id = 43;
            this.barButtonItem_ResetPassword.ImageOptions.ImageIndex = 18;
            this.barButtonItem_ResetPassword.Name = "barButtonItem_ResetPassword";
            this.barButtonItem_ResetPassword.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem_ResetPassword_ItemClick);
            // 
            // itemLock
            // 
            this.itemLock.Caption = "Khóa";
            this.itemLock.Id = 37;
            this.itemLock.ImageOptions.Image = global::Building.AppVime.Properties.Resources.padlock_lock_icon;
            this.itemLock.ImageOptions.ImageIndex = 14;
            this.itemLock.Name = "itemLock";
            this.itemLock.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemLock_ItemClick);
            // 
            // itemUnlock
            // 
            this.itemUnlock.Caption = "Mở khóa";
            this.itemUnlock.Id = 38;
            this.itemUnlock.ImageOptions.Image = global::Building.AppVime.Properties.Resources.padlock_unlock_icon;
            this.itemUnlock.ImageOptions.ImageIndex = 15;
            this.itemUnlock.Name = "itemUnlock";
            this.itemUnlock.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemUnlock_ItemClick);
            // 
            // itemExport
            // 
            this.itemExport.Caption = "Export";
            this.itemExport.Id = 21;
            this.itemExport.ImageOptions.Image = global::Building.AppVime.Properties.Resources.button_arrow_up_icon;
            this.itemExport.ImageOptions.ImageIndex = 5;
            this.itemExport.Name = "itemExport";
            this.itemExport.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemExport_ItemClick);
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "Update Firebase";
            this.barButtonItem1.Id = 41;
            this.barButtonItem1.ImageOptions.Image = global::Building.AppVime.Properties.Resources.synology_cloud_station_drive_icon;
            this.barButtonItem1.ImageOptions.ImageIndex = 16;
            this.barButtonItem1.Name = "barButtonItem1";
            this.barButtonItem1.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem1_ItemClick);
            // 
            // barButtonItem2
            // 
            this.barButtonItem2.Caption = "Xóa";
            this.barButtonItem2.Id = 42;
            this.barButtonItem2.ImageOptions.Image = global::Building.AppVime.Properties.Resources.delete_Close_icon1;
            this.barButtonItem2.ImageOptions.ImageIndex = 17;
            this.barButtonItem2.Name = "barButtonItem2";
            this.barButtonItem2.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem2_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.barDockControlTop.Size = new System.Drawing.Size(1320, 69);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 539);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.barDockControlBottom.Size = new System.Drawing.Size(1320, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 69);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 470);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1320, 69);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 470);
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
            this.imageCollection1.Images.SetKeyName(14, "lock blue.png");
            this.imageCollection1.Images.SetKeyName(15, "unlock blue.png");
            this.imageCollection1.Images.SetKeyName(16, "Upload1.png");
            this.imageCollection1.Images.SetKeyName(17, "Delete1.png");
            this.imageCollection1.Images.SetKeyName(18, "Refresh3.png");
            this.imageCollection1.Images.SetKeyName(19, "Connect1.png");
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
            // itemProcess
            // 
            this.itemProcess.Caption = "Xử lý";
            this.itemProcess.Id = 34;
            this.itemProcess.ImageOptions.ImageIndex = 12;
            this.itemProcess.Name = "itemProcess";
            this.itemProcess.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemProcess_ItemClick);
            // 
            // itemRegister
            // 
            this.itemRegister.Caption = "Đăng ký lại";
            this.itemRegister.Id = 35;
            this.itemRegister.ImageOptions.ImageIndex = 13;
            this.itemRegister.Name = "itemRegister";
            // 
            // itemRegisterMulti
            // 
            this.itemRegisterMulti.Caption = "Đăng ký theo dự án";
            this.itemRegisterMulti.Id = 36;
            this.itemRegisterMulti.ImageOptions.ImageIndex = 13;
            this.itemRegisterMulti.Name = "itemRegisterMulti";
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
            this.lkToaNha.ShowHeader = false;
            this.lkToaNha.ValueMember = "MaTN";
            // 
            // linqInstantFeedbackSource1
            // 
            this.linqInstantFeedbackSource1.KeyExpression = "ID";
            this.linqInstantFeedbackSource1.GetQueryable += new System.EventHandler<DevExpress.Data.Linq.GetQueryableEventArgs>(this.linqInstantFeedbackSource1_GetQueryable);
            this.linqInstantFeedbackSource1.DismissQueryable += new System.EventHandler<DevExpress.Data.Linq.GetQueryableEventArgs>(this.linqInstantFeedbackSource1_DismissQueryable);
            // 
            // barButtonItem_KiemTraTonTaiFirebase
            // 
            this.barButtonItem_KiemTraTonTaiFirebase.Caption = "Kiểm tra tồn tại firebase";
            this.barButtonItem_KiemTraTonTaiFirebase.Id = 44;
            this.barButtonItem_KiemTraTonTaiFirebase.ImageOptions.ImageIndex = 19;
            this.barButtonItem_KiemTraTonTaiFirebase.Name = "barButtonItem_KiemTraTonTaiFirebase";
            this.barButtonItem_KiemTraTonTaiFirebase.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem_KiemTraTonTaiFirebase_ItemClick);
            // 
            // frmManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1320, 539);
            this.Controls.Add(this.gcResident);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "frmManager";
            this.Text = "Nhân viên dùng App";
            this.Load += new System.EventHandler(this.frmManager_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gcResident)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvResident)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookNhanVien)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoExEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbKyBaoCao)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTuNgay.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTuNgay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDenNgay.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDenNgay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkToaNha)).EndInit();
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
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lkToaNha;
        private DevExpress.XtraBars.BarEditItem itemKyBaoCao;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox cmbKyBaoCao;
        private DevExpress.XtraBars.BarEditItem itemTuNgay;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit dateTuNgay;
        private DevExpress.XtraBars.BarEditItem itemDenNgay;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit dateDenNgay;
        private DevExpress.XtraBars.BarButtonItem itemRefresh;
        private DevExpress.XtraBars.BarButtonItem itemAdd;
        private DevExpress.XtraBars.BarButtonItem itemEdit;
        private DevExpress.XtraBars.BarButtonItem itemDelete;
        private DevExpress.XtraBars.BarButtonItem itemImport;
        private DevExpress.XtraBars.BarButtonItem itemExport;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.Utils.ImageCollection imageCollection1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraBars.BarButtonItem itemSync;
        private DevExpress.Data.Linq.LinqInstantFeedbackSource linqInstantFeedbackSource1;
        private DevExpress.XtraBars.BarButtonItem itemProcess;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lookUpEditStatus;
        private DevExpress.XtraBars.BarButtonItem itemRegister;
        private DevExpress.XtraBars.BarButtonItem itemRegisterMulti;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn12;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn13;
        private DevExpress.XtraBars.BarButtonItem itemLock;
        private DevExpress.XtraBars.BarButtonItem itemUnlock;
        private DevExpress.XtraBars.BarButtonItem itemChangePhone;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem2;
        private DevExpress.XtraBars.BarButtonItem barButtonItem_ResetPassword;
        private DevExpress.XtraBars.BarButtonItem barButtonItem_KiemTraTonTaiFirebase;
    }
}