namespace HopDongThueNgoai.DanhGia
{
    partial class FrmDanhGiaCongViec
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDanhGiaCongViec));
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.itemBuilding = new DevExpress.XtraBars.BarEditItem();
            this.lkBuilding = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.itemKbc = new DevExpress.XtraBars.BarEditItem();
            this.cbxKbc = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.itemDateFrom = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemDateEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.itemDateTo = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemDateEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.itemRefresh = new DevExpress.XtraBars.BarButtonItem();
            this.itemRating = new DevExpress.XtraBars.BarButtonItem();
            this.itemTaoPhanQuyen = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.gc = new DevExpress.XtraGrid.GridControl();
            this.gv = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemRatingControl1 = new DevExpress.XtraEditors.Repository.RepositoryItemRatingControl();
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.tabDanhGiaCongViec = new DevExpress.XtraTab.XtraTabPage();
            this.gcDanhGiaCongViec = new DevExpress.XtraGrid.GridControl();
            this.gvDanhGiaCongViec = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemRatingControl2 = new DevExpress.XtraEditors.Repository.RepositoryItemRatingControl();
            this.tabDanhSachNhanVien = new DevExpress.XtraTab.XtraTabPage();
            this.gcNhanVienThamGiaDanhGia = new DevExpress.XtraGrid.GridControl();
            this.gvNhanVienThamGiaDanhGia = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkBuilding)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxKbc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit2.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemRatingControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.tabDanhGiaCongViec.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcDanhGiaCongViec)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDanhGiaCongViec)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemRatingControl2)).BeginInit();
            this.tabDanhSachNhanVien.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcNhanVienThamGiaDanhGia)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvNhanVienThamGiaDanhGia)).BeginInit();
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
            this.itemBuilding,
            this.itemKbc,
            this.itemDateFrom,
            this.itemDateTo,
            this.itemRefresh,
            this.itemTaoPhanQuyen,
            this.itemRating});
            this.barManager1.MaxItemId = 10;
            this.barManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.lkBuilding,
            this.cbxKbc,
            this.repositoryItemDateEdit1,
            this.repositoryItemDateEdit2});
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.Width, this.itemBuilding, "", false, true, true, 111),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.Width, this.itemKbc, "", false, true, true, 124),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.Width, this.itemDateFrom, "", false, true, true, 100),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.Width, this.itemDateTo, "", false, true, true, 113),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemRefresh, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemRating, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemTaoPhanQuyen, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
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
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenTN", "Name1")});
            this.lkBuilding.DisplayMember = "TenTN";
            this.lkBuilding.Name = "lkBuilding";
            this.lkBuilding.NullText = "";
            this.lkBuilding.ShowHeader = false;
            this.lkBuilding.ValueMember = "MaTN";
            // 
            // itemKbc
            // 
            this.itemKbc.Caption = "Kỳ báo cáo";
            this.itemKbc.Edit = this.cbxKbc;
            this.itemKbc.Id = 2;
            this.itemKbc.Name = "itemKbc";
            // 
            // cbxKbc
            // 
            this.cbxKbc.AutoHeight = false;
            this.cbxKbc.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbxKbc.Name = "cbxKbc";
            // 
            // itemDateFrom
            // 
            this.itemDateFrom.Caption = "Từ ngày";
            this.itemDateFrom.Edit = this.repositoryItemDateEdit1;
            this.itemDateFrom.Id = 3;
            this.itemDateFrom.Name = "itemDateFrom";
            // 
            // repositoryItemDateEdit1
            // 
            this.repositoryItemDateEdit1.AutoHeight = false;
            this.repositoryItemDateEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemDateEdit1.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemDateEdit1.Name = "repositoryItemDateEdit1";
            // 
            // itemDateTo
            // 
            this.itemDateTo.Caption = "Đến ngày";
            this.itemDateTo.Edit = this.repositoryItemDateEdit2;
            this.itemDateTo.Id = 4;
            this.itemDateTo.Name = "itemDateTo";
            // 
            // repositoryItemDateEdit2
            // 
            this.repositoryItemDateEdit2.AutoHeight = false;
            this.repositoryItemDateEdit2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemDateEdit2.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemDateEdit2.Name = "repositoryItemDateEdit2";
            // 
            // itemRefresh
            // 
            this.itemRefresh.Caption = "Nạp";
            this.itemRefresh.Id = 5;
            this.itemRefresh.ImageOptions.ImageIndex = 0;
            this.itemRefresh.Name = "itemRefresh";
            this.itemRefresh.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.ItemRefresh_ItemClick);
            // 
            // itemRating
            // 
            this.itemRating.Caption = "Đánh giá";
            this.itemRating.Id = 7;
            this.itemRating.ImageOptions.ImageIndex = 1;
            this.itemRating.Name = "itemRating";
            this.itemRating.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemAdd_ItemClick);
            // 
            // itemTaoPhanQuyen
            // 
            this.itemTaoPhanQuyen.Caption = "Tạo phân quyền";
            this.itemTaoPhanQuyen.Id = 6;
            this.itemTaoPhanQuyen.ImageOptions.ImageIndex = 2;
            this.itemTaoPhanQuyen.Name = "itemTaoPhanQuyen";
            this.itemTaoPhanQuyen.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemTaoPhanQuyen_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(1028, 31);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 534);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(1028, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 31);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 503);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1028, 31);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 503);
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.Images.SetKeyName(0, "Refresh1.png");
            this.imageCollection1.Images.SetKeyName(1, "icons8_star_half_empty_filled_50px.png");
            this.imageCollection1.Images.SetKeyName(2, "Setting1.png");
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 31);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.gc);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.xtraTabControl1);
            this.splitContainer1.Size = new System.Drawing.Size(1028, 503);
            this.splitContainer1.SplitterDistance = 243;
            this.splitContainer1.TabIndex = 4;
            // 
            // gc
            // 
            this.gc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gc.Location = new System.Drawing.Point(0, 0);
            this.gc.MainView = this.gv;
            this.gc.MenuManager = this.barManager1;
            this.gc.Name = "gc";
            this.gc.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemRatingControl1});
            this.gc.Size = new System.Drawing.Size(1028, 243);
            this.gc.TabIndex = 0;
            this.gc.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gv});
            // 
            // gv
            // 
            this.gv.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3});
            this.gv.GridControl = this.gc;
            this.gv.Name = "gv";
            this.gv.OptionsBehavior.Editable = false;
            this.gv.OptionsBehavior.ReadOnly = true;
            this.gv.OptionsView.ColumnAutoWidth = false;
            this.gv.OptionsView.ShowAutoFilterRow = true;
            this.gv.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gv_RowClick);
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Hợp đồng";
            this.gridColumn1.FieldName = "HopDongNo";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 142;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Khách hàng";
            this.gridColumn2.FieldName = "KhachHang";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 297;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Đánh giá";
            this.gridColumn3.ColumnEdit = this.repositoryItemRatingControl1;
            this.gridColumn3.FieldName = "DanhGia";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            this.gridColumn3.Width = 120;
            // 
            // repositoryItemRatingControl1
            // 
            this.repositoryItemRatingControl1.AutoHeight = false;
            this.repositoryItemRatingControl1.Name = "repositoryItemRatingControl1";
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl1.Location = new System.Drawing.Point(0, 0);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.tabDanhGiaCongViec;
            this.xtraTabControl1.Size = new System.Drawing.Size(1028, 256);
            this.xtraTabControl1.TabIndex = 0;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tabDanhGiaCongViec,
            this.tabDanhSachNhanVien});
            this.xtraTabControl1.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(this.xtraTabControl1_SelectedPageChanged);
            // 
            // tabDanhGiaCongViec
            // 
            this.tabDanhGiaCongViec.Controls.Add(this.gcDanhGiaCongViec);
            this.tabDanhGiaCongViec.Name = "tabDanhGiaCongViec";
            this.tabDanhGiaCongViec.Size = new System.Drawing.Size(1022, 228);
            this.tabDanhGiaCongViec.Text = "1. Đánh giá công việc";
            // 
            // gcDanhGiaCongViec
            // 
            this.gcDanhGiaCongViec.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcDanhGiaCongViec.Location = new System.Drawing.Point(0, 0);
            this.gcDanhGiaCongViec.MainView = this.gvDanhGiaCongViec;
            this.gcDanhGiaCongViec.MenuManager = this.barManager1;
            this.gcDanhGiaCongViec.Name = "gcDanhGiaCongViec";
            this.gcDanhGiaCongViec.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemRatingControl2});
            this.gcDanhGiaCongViec.Size = new System.Drawing.Size(1022, 228);
            this.gcDanhGiaCongViec.TabIndex = 0;
            this.gcDanhGiaCongViec.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvDanhGiaCongViec});
            // 
            // gvDanhGiaCongViec
            // 
            this.gvDanhGiaCongViec.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn4,
            this.gridColumn5});
            this.gvDanhGiaCongViec.GridControl = this.gcDanhGiaCongViec;
            this.gvDanhGiaCongViec.Name = "gvDanhGiaCongViec";
            this.gvDanhGiaCongViec.OptionsBehavior.Editable = false;
            this.gvDanhGiaCongViec.OptionsView.ColumnAutoWidth = false;
            this.gvDanhGiaCongViec.OptionsView.ShowAutoFilterRow = true;
            this.gvDanhGiaCongViec.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Công việc";
            this.gridColumn4.FieldName = "CongViecName";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 0;
            this.gridColumn4.Width = 547;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Đánh giá";
            this.gridColumn5.ColumnEdit = this.repositoryItemRatingControl2;
            this.gridColumn5.FieldName = "DanhGia";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 1;
            this.gridColumn5.Width = 111;
            // 
            // repositoryItemRatingControl2
            // 
            this.repositoryItemRatingControl2.AutoHeight = false;
            this.repositoryItemRatingControl2.Name = "repositoryItemRatingControl2";
            // 
            // tabDanhSachNhanVien
            // 
            this.tabDanhSachNhanVien.Controls.Add(this.gcNhanVienThamGiaDanhGia);
            this.tabDanhSachNhanVien.Name = "tabDanhSachNhanVien";
            this.tabDanhSachNhanVien.Size = new System.Drawing.Size(1022, 228);
            this.tabDanhSachNhanVien.Text = "2. Danh sách nhân viên đánh giá";
            // 
            // gcNhanVienThamGiaDanhGia
            // 
            this.gcNhanVienThamGiaDanhGia.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcNhanVienThamGiaDanhGia.Location = new System.Drawing.Point(0, 0);
            this.gcNhanVienThamGiaDanhGia.MainView = this.gvNhanVienThamGiaDanhGia;
            this.gcNhanVienThamGiaDanhGia.MenuManager = this.barManager1;
            this.gcNhanVienThamGiaDanhGia.Name = "gcNhanVienThamGiaDanhGia";
            this.gcNhanVienThamGiaDanhGia.Size = new System.Drawing.Size(1022, 228);
            this.gcNhanVienThamGiaDanhGia.TabIndex = 0;
            this.gcNhanVienThamGiaDanhGia.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvNhanVienThamGiaDanhGia});
            // 
            // gvNhanVienThamGiaDanhGia
            // 
            this.gvNhanVienThamGiaDanhGia.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn6});
            this.gvNhanVienThamGiaDanhGia.GridControl = this.gcNhanVienThamGiaDanhGia;
            this.gvNhanVienThamGiaDanhGia.Name = "gvNhanVienThamGiaDanhGia";
            this.gvNhanVienThamGiaDanhGia.OptionsBehavior.Editable = false;
            this.gvNhanVienThamGiaDanhGia.OptionsView.ColumnAutoWidth = false;
            this.gvNhanVienThamGiaDanhGia.OptionsView.ShowAutoFilterRow = true;
            this.gvNhanVienThamGiaDanhGia.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "Nhân viên";
            this.gridColumn6.FieldName = "UserName";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 0;
            this.gridColumn6.Width = 712;
            // 
            // FrmDanhGiaCongViec
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1028, 534);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "FrmDanhGiaCongViec";
            this.Text = "Đánh giá công việc";
            this.Load += new System.EventHandler(this.FrmDanhGiaCongViec_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkBuilding)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxKbc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit2.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemRatingControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.tabDanhGiaCongViec.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcDanhGiaCongViec)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDanhGiaCongViec)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemRatingControl2)).EndInit();
            this.tabDanhSachNhanVien.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcNhanVienThamGiaDanhGia)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvNhanVienThamGiaDanhGia)).EndInit();
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
        private DevExpress.XtraBars.BarEditItem itemBuilding;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lkBuilding;
        private DevExpress.XtraBars.BarEditItem itemKbc;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox cbxKbc;
        private DevExpress.XtraBars.BarEditItem itemDateFrom;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit repositoryItemDateEdit1;
        private DevExpress.XtraBars.BarEditItem itemDateTo;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit repositoryItemDateEdit2;
        private DevExpress.XtraBars.BarButtonItem itemRefresh;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private DevExpress.XtraGrid.GridControl gc;
        private DevExpress.XtraGrid.Views.Grid.GridView gv;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage tabDanhGiaCongViec;
        private DevExpress.XtraGrid.GridControl gcDanhGiaCongViec;
        private DevExpress.XtraGrid.Views.Grid.GridView gvDanhGiaCongViec;
        private DevExpress.Utils.ImageCollection imageCollection1;
        private DevExpress.XtraBars.BarButtonItem itemTaoPhanQuyen;
        private DevExpress.XtraBars.BarButtonItem itemRating;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraEditors.Repository.RepositoryItemRatingControl repositoryItemRatingControl1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraEditors.Repository.RepositoryItemRatingControl repositoryItemRatingControl2;
        private DevExpress.XtraTab.XtraTabPage tabDanhSachNhanVien;
        private DevExpress.XtraGrid.GridControl gcNhanVienThamGiaDanhGia;
        private DevExpress.XtraGrid.Views.Grid.GridView gvNhanVienThamGiaDanhGia;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
    }
}