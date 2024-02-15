namespace EmailAmazon
{
    partial class frmImport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmImport));
            this.imageCollection1 = new DevExpress.Utils.ImageCollection();
            this.barManager1 = new DevExpress.XtraBars.BarManager();
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.btnChonTapTin = new DevExpress.XtraBars.BarButtonItem();
            this.btnLuu = new DevExpress.XtraBars.BarButtonItem();
            this.btnXoaDong = new DevExpress.XtraBars.BarButtonItem();
            this.itemClose = new DevExpress.XtraBars.BarButtonItem();
            this.itemNhomKhachHang = new DevExpress.XtraBars.BarEditItem();
            this.lkNhomKhachHang = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.gcCaNhan = new DevExpress.XtraGrid.GridControl();
            this.grvCaNhan = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colHo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTen = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSoCMND = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colKyHieu = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colGioiTinh = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMaPhu = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMaSoThue = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lookKhuVuc = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.itemExport = new DevExpress.XtraBars.BarButtonItem();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkNhomKhachHang)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcCaNhan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvCaNhan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookKhuVuc)).BeginInit();
            this.SuspendLayout();
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.Images.SetKeyName(0, "Open1.png");
            this.imageCollection1.Images.SetKeyName(1, "Save1.png");
            this.imageCollection1.Images.SetKeyName(2, "Delete1.png");
            this.imageCollection1.Images.SetKeyName(3, "Cancel1.png");
            this.imageCollection1.Images.SetKeyName(4, "Export1.png");
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
            this.btnChonTapTin,
            this.btnLuu,
            this.btnXoaDong,
            this.itemClose,
            this.itemNhomKhachHang,
            this.itemExport});
            this.barManager1.MaxItemId = 6;
            this.barManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.lkNhomKhachHang});
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnChonTapTin, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnLuu, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnXoaDong, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemClose, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemNhomKhachHang, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemExport, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.DrawDragBorder = false;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Tools";
            // 
            // btnChonTapTin
            // 
            this.btnChonTapTin.Caption = "Chọn tập tin";
            this.btnChonTapTin.Id = 0;
            this.btnChonTapTin.ImageOptions.ImageIndex = 0;
            this.btnChonTapTin.Name = "btnChonTapTin";
            this.btnChonTapTin.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnChonTapTin_ItemClick);
            // 
            // btnLuu
            // 
            this.btnLuu.Caption = "Lưu dữ liệu";
            this.btnLuu.Id = 1;
            this.btnLuu.ImageOptions.ImageIndex = 1;
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnLuu_ItemClick);
            // 
            // btnXoaDong
            // 
            this.btnXoaDong.Caption = "Xóa dòng";
            this.btnXoaDong.Id = 2;
            this.btnXoaDong.ImageOptions.ImageIndex = 2;
            this.btnXoaDong.Name = "btnXoaDong";
            this.btnXoaDong.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnXoaDong_ItemClick);
            // 
            // itemClose
            // 
            this.itemClose.Caption = "Đóng";
            this.itemClose.Id = 3;
            this.itemClose.ImageOptions.ImageIndex = 3;
            this.itemClose.Name = "itemClose";
            this.itemClose.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemClose_ItemClick);
            // 
            // itemNhomKhachHang
            // 
            this.itemNhomKhachHang.Caption = "Danh sách nhận";
            this.itemNhomKhachHang.Edit = this.lkNhomKhachHang;
            this.itemNhomKhachHang.EditWidth = 159;
            this.itemNhomKhachHang.Id = 4;
            this.itemNhomKhachHang.Name = "itemNhomKhachHang";
            // 
            // lkNhomKhachHang
            // 
            this.lkNhomKhachHang.AutoHeight = false;
            this.lkNhomKhachHang.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkNhomKhachHang.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenNKH", "Danh sách nhận")});
            this.lkNhomKhachHang.DisplayMember = "TenNKH";
            this.lkNhomKhachHang.Name = "lkNhomKhachHang";
            this.lkNhomKhachHang.NullText = "";
            this.lkNhomKhachHang.ValueMember = "ID";
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.barDockControlTop.Size = new System.Drawing.Size(1030, 31);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 515);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.barDockControlBottom.Size = new System.Drawing.Size(1030, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 31);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 484);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1030, 31);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 484);
            // 
            // gcCaNhan
            // 
            this.gcCaNhan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcCaNhan.Location = new System.Drawing.Point(0, 31);
            this.gcCaNhan.MainView = this.grvCaNhan;
            this.gcCaNhan.MenuManager = this.barManager1;
            this.gcCaNhan.Name = "gcCaNhan";
            this.gcCaNhan.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.lookKhuVuc});
            this.gcCaNhan.Size = new System.Drawing.Size(1030, 484);
            this.gcCaNhan.TabIndex = 4;
            this.gcCaNhan.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvCaNhan});
            // 
            // grvCaNhan
            // 
            this.grvCaNhan.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colHo,
            this.colTen,
            this.colSoCMND,
            this.colKyHieu,
            this.colGioiTinh,
            this.colMaPhu,
            this.colMaSoThue});
            this.grvCaNhan.GridControl = this.gcCaNhan;
            this.grvCaNhan.Name = "grvCaNhan";
            this.grvCaNhan.OptionsCustomization.AllowGroup = false;
            this.grvCaNhan.OptionsSelection.MultiSelect = true;
            this.grvCaNhan.OptionsView.ColumnAutoWidth = false;
            this.grvCaNhan.OptionsView.ShowAutoFilterRow = true;
            this.grvCaNhan.OptionsView.ShowGroupPanel = false;
            this.grvCaNhan.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.grvCaNhan_RowStyle);
            // 
            // colHo
            // 
            this.colHo.Caption = "Email";
            this.colHo.FieldName = "Email";
            this.colHo.Name = "colHo";
            this.colHo.Visible = true;
            this.colHo.VisibleIndex = 2;
            this.colHo.Width = 115;
            // 
            // colTen
            // 
            this.colTen.Caption = "Ngày sinh";
            this.colTen.FieldName = "NgaySinh";
            this.colTen.Name = "colTen";
            this.colTen.Visible = true;
            this.colTen.VisibleIndex = 3;
            this.colTen.Width = 126;
            // 
            // colSoCMND
            // 
            this.colSoCMND.Caption = "Kết quá kiểm tra";
            this.colSoCMND.FieldName = "KQKT";
            this.colSoCMND.Name = "colSoCMND";
            this.colSoCMND.Visible = true;
            this.colSoCMND.VisibleIndex = 6;
            this.colSoCMND.Width = 129;
            // 
            // colKyHieu
            // 
            this.colKyHieu.Caption = "Xưng hô";
            this.colKyHieu.FieldName = "XungHo";
            this.colKyHieu.Name = "colKyHieu";
            this.colKyHieu.Visible = true;
            this.colKyHieu.VisibleIndex = 0;
            this.colKyHieu.Width = 87;
            // 
            // colGioiTinh
            // 
            this.colGioiTinh.Caption = "Điện thoại";
            this.colGioiTinh.FieldName = "DienThoai";
            this.colGioiTinh.Name = "colGioiTinh";
            this.colGioiTinh.Visible = true;
            this.colGioiTinh.VisibleIndex = 4;
            // 
            // colMaPhu
            // 
            this.colMaPhu.Caption = "Họ tên";
            this.colMaPhu.FieldName = "TenKH";
            this.colMaPhu.Name = "colMaPhu";
            this.colMaPhu.Visible = true;
            this.colMaPhu.VisibleIndex = 1;
            this.colMaPhu.Width = 91;
            // 
            // colMaSoThue
            // 
            this.colMaSoThue.Caption = "Địa chỉ";
            this.colMaSoThue.FieldName = "DiaChi";
            this.colMaSoThue.Name = "colMaSoThue";
            this.colMaSoThue.Visible = true;
            this.colMaSoThue.VisibleIndex = 5;
            this.colMaSoThue.Width = 93;
            // 
            // lookKhuVuc
            // 
            this.lookKhuVuc.AutoHeight = false;
            this.lookKhuVuc.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookKhuVuc.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenKV", "Khu vực")});
            this.lookKhuVuc.DisplayMember = "TenKV";
            this.lookKhuVuc.Name = "lookKhuVuc";
            this.lookKhuVuc.NullText = "";
            this.lookKhuVuc.ValueMember = "MaKV";
            // 
            // itemExport
            // 
            this.itemExport.Caption = "Export mẫu";
            this.itemExport.Id = 5;
            this.itemExport.ImageOptions.ImageIndex = 4;
            this.itemExport.Name = "itemExport";
            this.itemExport.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemExport_ItemClick);
            // 
            // frmImport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1030, 515);
            this.Controls.Add(this.gcCaNhan);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "frmImport";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Import email";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmImport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkNhomKhachHang)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcCaNhan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvCaNhan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookKhuVuc)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.Utils.ImageCollection imageCollection1;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarButtonItem btnChonTapTin;
        private DevExpress.XtraBars.BarButtonItem btnLuu;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraGrid.GridControl gcCaNhan;
        private DevExpress.XtraGrid.Views.Grid.GridView grvCaNhan;
        private DevExpress.XtraGrid.Columns.GridColumn colHo;
        private DevExpress.XtraGrid.Columns.GridColumn colTen;
        private DevExpress.XtraGrid.Columns.GridColumn colSoCMND;
        private DevExpress.XtraGrid.Columns.GridColumn colKyHieu;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lookKhuVuc;
        private DevExpress.XtraGrid.Columns.GridColumn colGioiTinh;
        private DevExpress.XtraBars.BarButtonItem btnXoaDong;
        private DevExpress.XtraGrid.Columns.GridColumn colMaPhu;
        private DevExpress.XtraBars.BarButtonItem itemClose;
        private DevExpress.XtraGrid.Columns.GridColumn colMaSoThue;
        private DevExpress.XtraBars.BarEditItem itemNhomKhachHang;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lkNhomKhachHang;
        private DevExpress.XtraBars.BarButtonItem itemExport;
    }
}