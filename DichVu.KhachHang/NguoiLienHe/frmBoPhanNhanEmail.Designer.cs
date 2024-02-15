namespace DichVu.KhachHang.NguoiLienHe
{
    partial class frmBoPhanNhanEmail
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBoPhanNhanEmail));
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.itemToaNha = new DevExpress.XtraBars.BarEditItem();
            this.lkToaNha = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.itemKbc = new DevExpress.XtraBars.BarEditItem();
            this.cbxKbc = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.itemTuNgay = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemDateEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.itemDenNgay = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemDateEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.itemRefresh = new DevExpress.XtraBars.BarButtonItem();
            this.itemThem = new DevExpress.XtraBars.BarButtonItem();
            this.itemSua = new DevExpress.XtraBars.BarButtonItem();
            this.itemExport = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
            this.itemAssetAdd = new DevExpress.XtraBars.BarButtonItem();
            this.itemAssetEdit = new DevExpress.XtraBars.BarButtonItem();
            this.itemAssetDelete = new DevExpress.XtraBars.BarButtonItem();
            this.itemAssetImport = new DevExpress.XtraBars.BarButtonItem();
            this.itemAssetExport = new DevExpress.XtraBars.BarButtonItem();
            this.itemViewImg = new DevExpress.XtraBars.BarButtonItem();
            this.itemThemKhachHang = new DevExpress.XtraBars.BarButtonItem();
            this.itemSuaKhachHang = new DevExpress.XtraBars.BarButtonItem();
            this.itemXoaKhachHang = new DevExpress.XtraBars.BarButtonItem();
            this.itemGuiEmailKhachHang = new DevExpress.XtraBars.BarButtonItem();
            this.itemXacNhan = new DevExpress.XtraBars.BarButtonItem();
            this.itemAddAll = new DevExpress.XtraBars.BarButtonItem();
            this.gv = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gc = new DevExpress.XtraGrid.GridControl();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkToaNha)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxKbc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit2.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gc)).BeginInit();
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
            this.itemToaNha,
            this.itemKbc,
            this.itemTuNgay,
            this.itemDenNgay,
            this.itemRefresh,
            this.itemThem,
            this.itemSua,
            this.itemAssetAdd,
            this.itemAssetEdit,
            this.itemAssetDelete,
            this.itemAssetImport,
            this.itemAssetExport,
            this.itemViewImg,
            this.itemExport,
            this.itemThemKhachHang,
            this.itemSuaKhachHang,
            this.itemXoaKhachHang,
            this.itemGuiEmailKhachHang,
            this.itemXacNhan,
            this.itemAddAll});
            this.barManager1.MaxItemId = 24;
            this.barManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.lkToaNha,
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
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.Width, this.itemToaNha, "", false, true, true, 211),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.Width, this.itemKbc, "", false, true, true, 95),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.Width, this.itemTuNgay, "", false, true, true, 90),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.Width, this.itemDenNgay, "", false, true, true, 112),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemRefresh, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemThem, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemSua, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemExport, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.DrawDragBorder = false;
            this.bar1.OptionsBar.MultiLine = true;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Tools";
            // 
            // itemToaNha
            // 
            this.itemToaNha.Caption = "Tòa nhà";
            this.itemToaNha.Edit = this.lkToaNha;
            this.itemToaNha.Id = 0;
            this.itemToaNha.Name = "itemToaNha";
            this.itemToaNha.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            // 
            // lkToaNha
            // 
            this.lkToaNha.AutoHeight = false;
            this.lkToaNha.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkToaNha.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenTN", "Name1")});
            this.lkToaNha.DisplayMember = "TenTN";
            this.lkToaNha.Name = "lkToaNha";
            this.lkToaNha.NullText = "";
            this.lkToaNha.ShowHeader = false;
            this.lkToaNha.ValueMember = "MaTN";
            // 
            // itemKbc
            // 
            this.itemKbc.Caption = "Kỳ báo cáo";
            this.itemKbc.Edit = this.cbxKbc;
            this.itemKbc.Id = 1;
            this.itemKbc.Name = "itemKbc";
            this.itemKbc.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            // 
            // cbxKbc
            // 
            this.cbxKbc.AutoHeight = false;
            this.cbxKbc.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbxKbc.Name = "cbxKbc";
            this.cbxKbc.EditValueChanged += new System.EventHandler(this.CbxKbc_EditValueChanged);
            // 
            // itemTuNgay
            // 
            this.itemTuNgay.Caption = "Từ ngày";
            this.itemTuNgay.Edit = this.repositoryItemDateEdit1;
            this.itemTuNgay.Id = 2;
            this.itemTuNgay.Name = "itemTuNgay";
            this.itemTuNgay.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
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
            // itemDenNgay
            // 
            this.itemDenNgay.Caption = "Đến ngày";
            this.itemDenNgay.Edit = this.repositoryItemDateEdit2;
            this.itemDenNgay.Id = 3;
            this.itemDenNgay.Name = "itemDenNgay";
            this.itemDenNgay.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
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
            this.itemRefresh.Id = 4;
            this.itemRefresh.ImageOptions.ImageIndex = 0;
            this.itemRefresh.Name = "itemRefresh";
            this.itemRefresh.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.ItemRefresh_ItemClick);
            // 
            // itemThem
            // 
            this.itemThem.Caption = "Thêm";
            this.itemThem.Id = 5;
            this.itemThem.ImageOptions.ImageIndex = 4;
            this.itemThem.Name = "itemThem";
            this.itemThem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemThem_ItemClick);
            // 
            // itemSua
            // 
            this.itemSua.Caption = "Sửa";
            this.itemSua.Id = 7;
            this.itemSua.ImageOptions.ImageIndex = 3;
            this.itemSua.Name = "itemSua";
            this.itemSua.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.ItemSua_ItemClick);
            // 
            // itemExport
            // 
            this.itemExport.Caption = "Export";
            this.itemExport.Id = 14;
            this.itemExport.ImageOptions.ImageIndex = 7;
            this.itemExport.Name = "itemExport";
            this.itemExport.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemExport_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(819, 69);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 616);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(819, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 69);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 547);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(819, 69);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 547);
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.Images.SetKeyName(0, "Refresh1.png");
            this.imageCollection1.Images.SetKeyName(1, "icons8_guest_filled_50px.png");
            this.imageCollection1.Images.SetKeyName(2, "Checklist1.png");
            this.imageCollection1.Images.SetKeyName(3, "Edit3.png");
            this.imageCollection1.Images.SetKeyName(4, "Add1.png");
            this.imageCollection1.Images.SetKeyName(5, "Delete1.png");
            this.imageCollection1.Images.SetKeyName(6, "Import1.png");
            this.imageCollection1.Images.SetKeyName(7, "Export1.png");
            this.imageCollection1.Images.SetKeyName(8, "View1.png");
            this.imageCollection1.Images.SetKeyName(9, "Setting1.png");
            this.imageCollection1.Images.SetKeyName(10, "Mail2.png");
            this.imageCollection1.Images.SetKeyName(11, "Duyet1.png");
            // 
            // itemAssetAdd
            // 
            this.itemAssetAdd.Caption = "Thêm";
            this.itemAssetAdd.Id = 8;
            this.itemAssetAdd.ImageOptions.ImageIndex = 4;
            this.itemAssetAdd.Name = "itemAssetAdd";
            // 
            // itemAssetEdit
            // 
            this.itemAssetEdit.Caption = "Sửa";
            this.itemAssetEdit.Id = 9;
            this.itemAssetEdit.ImageOptions.ImageIndex = 3;
            this.itemAssetEdit.Name = "itemAssetEdit";
            // 
            // itemAssetDelete
            // 
            this.itemAssetDelete.Caption = "Xóa";
            this.itemAssetDelete.Id = 10;
            this.itemAssetDelete.ImageOptions.ImageIndex = 5;
            this.itemAssetDelete.Name = "itemAssetDelete";
            // 
            // itemAssetImport
            // 
            this.itemAssetImport.Caption = "Import";
            this.itemAssetImport.Id = 11;
            this.itemAssetImport.ImageOptions.ImageIndex = 6;
            this.itemAssetImport.Name = "itemAssetImport";
            // 
            // itemAssetExport
            // 
            this.itemAssetExport.Caption = "Export";
            this.itemAssetExport.Id = 12;
            this.itemAssetExport.ImageOptions.ImageIndex = 7;
            this.itemAssetExport.Name = "itemAssetExport";
            // 
            // itemViewImg
            // 
            this.itemViewImg.Caption = "Xem ảnh";
            this.itemViewImg.Id = 13;
            this.itemViewImg.ImageOptions.ImageIndex = 8;
            this.itemViewImg.Name = "itemViewImg";
            // 
            // itemThemKhachHang
            // 
            this.itemThemKhachHang.Caption = "Thêm";
            this.itemThemKhachHang.Id = 18;
            this.itemThemKhachHang.ImageOptions.ImageIndex = 4;
            this.itemThemKhachHang.Name = "itemThemKhachHang";
            this.itemThemKhachHang.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemThemKhachHang_ItemClick);
            // 
            // itemSuaKhachHang
            // 
            this.itemSuaKhachHang.Caption = "Sửa";
            this.itemSuaKhachHang.Id = 19;
            this.itemSuaKhachHang.ImageOptions.ImageIndex = 3;
            this.itemSuaKhachHang.Name = "itemSuaKhachHang";
            this.itemSuaKhachHang.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemSuaKhachHang_ItemClick);
            // 
            // itemXoaKhachHang
            // 
            this.itemXoaKhachHang.Caption = "Xóa";
            this.itemXoaKhachHang.Id = 20;
            this.itemXoaKhachHang.ImageOptions.ImageIndex = 5;
            this.itemXoaKhachHang.Name = "itemXoaKhachHang";
            this.itemXoaKhachHang.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemXoaKhachHang_ItemClick);
            // 
            // itemGuiEmailKhachHang
            // 
            this.itemGuiEmailKhachHang.Caption = "Gửi";
            this.itemGuiEmailKhachHang.Id = 21;
            this.itemGuiEmailKhachHang.ImageOptions.ImageIndex = 10;
            this.itemGuiEmailKhachHang.Name = "itemGuiEmailKhachHang";
            this.itemGuiEmailKhachHang.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemGuiEmailKhachHang_ItemClick);
            // 
            // itemXacNhan
            // 
            this.itemXacNhan.Caption = "Xác nhận";
            this.itemXacNhan.Id = 22;
            this.itemXacNhan.ImageOptions.ImageIndex = 11;
            this.itemXacNhan.Name = "itemXacNhan";
            this.itemXacNhan.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemXacNhan_ItemClick);
            // 
            // itemAddAll
            // 
            this.itemAddAll.Caption = "Thêm nhiều";
            this.itemAddAll.Id = 23;
            this.itemAddAll.ImageOptions.ImageIndex = 4;
            this.itemAddAll.Name = "itemAddAll";
            this.itemAddAll.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemAddAll_ItemClick);
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
            this.gv.OptionsView.ShowFooter = true;
            this.gv.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.Gv_RowClick);
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Loại gửi";
            this.gridColumn1.FieldName = "LoaiGui";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 315;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Nhóm bộ phận";
            this.gridColumn2.FieldName = "TenNhomBoPhan";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 440;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Form thực thi";
            this.gridColumn3.FieldName = "FormThucThi";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            this.gridColumn3.Width = 302;
            // 
            // gc
            // 
            this.gc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gc.Location = new System.Drawing.Point(0, 69);
            this.gc.MainView = this.gv;
            this.gc.MenuManager = this.barManager1;
            this.gc.Name = "gc";
            this.gc.Size = new System.Drawing.Size(819, 547);
            this.gc.TabIndex = 0;
            this.gc.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gv});
            // 
            // frmBoPhanNhanEmail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(819, 616);
            this.Controls.Add(this.gc);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "frmBoPhanNhanEmail";
            this.Text = "Thiết lập nhóm nhận email";
            this.Load += new System.EventHandler(this.frmKeHoach_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkToaNha)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxKbc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit2.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gc)).EndInit();
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
        private DevExpress.XtraBars.BarEditItem itemToaNha;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lkToaNha;
        private DevExpress.XtraBars.BarEditItem itemKbc;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox cbxKbc;
        private DevExpress.XtraBars.BarEditItem itemTuNgay;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit repositoryItemDateEdit1;
        private DevExpress.XtraBars.BarEditItem itemDenNgay;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit repositoryItemDateEdit2;
        private DevExpress.XtraBars.BarButtonItem itemRefresh;
        private DevExpress.XtraBars.BarButtonItem itemThem;
        private DevExpress.Utils.ImageCollection imageCollection1;
        private DevExpress.XtraBars.BarButtonItem itemSua;
        private DevExpress.XtraBars.BarButtonItem itemAssetAdd;
        private DevExpress.XtraBars.BarButtonItem itemAssetEdit;
        private DevExpress.XtraBars.BarButtonItem itemAssetDelete;
        private DevExpress.XtraBars.BarButtonItem itemAssetImport;
        private DevExpress.XtraBars.BarButtonItem itemAssetExport;
        private DevExpress.XtraBars.BarButtonItem itemViewImg;
        private DevExpress.XtraBars.BarButtonItem itemExport;
        private DevExpress.XtraBars.BarButtonItem itemThemKhachHang;
        private DevExpress.XtraBars.BarButtonItem itemSuaKhachHang;
        private DevExpress.XtraBars.BarButtonItem itemXoaKhachHang;
        private DevExpress.XtraBars.BarButtonItem itemGuiEmailKhachHang;
        private DevExpress.XtraBars.BarButtonItem itemXacNhan;
        private DevExpress.XtraBars.BarButtonItem itemAddAll;
        private DevExpress.XtraGrid.GridControl gc;
        private DevExpress.XtraGrid.Views.Grid.GridView gv;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
    }
}