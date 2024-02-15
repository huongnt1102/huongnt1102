namespace DichVu.NhanKhau.Import
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
            this.barManager1 = new DevExpress.XtraBars.BarManager();
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.btnChonTapTin = new DevExpress.XtraBars.BarButtonItem();
            this.btnLuu = new DevExpress.XtraBars.BarButtonItem();
            this.btnXoaDong = new DevExpress.XtraBars.BarButtonItem();
            this.itemClose = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection();
            this.gcNhanKhau = new DevExpress.XtraGrid.GridControl();
            this.grvNhanKhau = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colMatBang = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lookMatBang = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.colHoTen = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDCTT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colGioiTinh = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNgaySinh = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCMND = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNgayCap = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNoiCap = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDienThoai = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEmail = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDaDKTT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colHetHanDKTT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTrangThai = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lookTrangThai = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.colQuocTich = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNgayDK = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDienGiai = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colKhachHang = new DevExpress.XtraGrid.Columns.GridColumn();
            this.QuanHe = new DevExpress.XtraGrid.Columns.GridColumn();
            this.CardNumber = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.itemExport = new DevExpress.XtraBars.BarButtonItem();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcNhanKhau)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvNhanKhau)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookMatBang)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookTrangThai)).BeginInit();
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
            this.btnChonTapTin,
            this.btnLuu,
            this.btnXoaDong,
            this.itemClose,
            this.itemExport});
            this.barManager1.MaxItemId = 5;
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
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.barDockControlTop.Size = new System.Drawing.Size(1159, 31);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 641);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.barDockControlBottom.Size = new System.Drawing.Size(1159, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 31);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 610);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1159, 31);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 610);
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
            // gcNhanKhau
            // 
            this.gcNhanKhau.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcNhanKhau.Location = new System.Drawing.Point(0, 31);
            this.gcNhanKhau.MainView = this.grvNhanKhau;
            this.gcNhanKhau.Name = "gcNhanKhau";
            this.gcNhanKhau.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.lookTrangThai,
            this.lookMatBang});
            this.gcNhanKhau.Size = new System.Drawing.Size(1159, 610);
            this.gcNhanKhau.TabIndex = 9;
            this.gcNhanKhau.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvNhanKhau});
            // 
            // grvNhanKhau
            // 
            this.grvNhanKhau.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colMatBang,
            this.colHoTen,
            this.colDCTT,
            this.colGioiTinh,
            this.colNgaySinh,
            this.colCMND,
            this.colNgayCap,
            this.colNoiCap,
            this.colDienThoai,
            this.colEmail,
            this.colDaDKTT,
            this.colHetHanDKTT,
            this.colTrangThai,
            this.colQuocTich,
            this.colNgayDK,
            this.colDienGiai,
            this.colKhachHang,
            this.QuanHe,
            this.CardNumber,
            this.gridColumn1});
            this.grvNhanKhau.GridControl = this.gcNhanKhau;
            this.grvNhanKhau.GroupPanelText = "Kéo cột lên đây để xem theo nhóm";
            this.grvNhanKhau.Name = "grvNhanKhau";
            this.grvNhanKhau.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.True;
            this.grvNhanKhau.OptionsMenu.EnableFooterMenu = false;
            this.grvNhanKhau.OptionsSelection.MultiSelect = true;
            this.grvNhanKhau.OptionsView.ColumnAutoWidth = false;
            this.grvNhanKhau.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;
            this.grvNhanKhau.OptionsView.ShowAutoFilterRow = true;
            this.grvNhanKhau.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.grvMatBang_RowStyle);
            this.grvNhanKhau.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.grvNhanKhau_CellValueChanged);
            // 
            // colMatBang
            // 
            this.colMatBang.Caption = "Mã mặt bằng";
            this.colMatBang.ColumnEdit = this.lookMatBang;
            this.colMatBang.FieldName = "MaMB";
            this.colMatBang.Name = "colMatBang";
            this.colMatBang.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "MaSoMB", "Tổng: {0:#,0.##}")});
            this.colMatBang.Visible = true;
            this.colMatBang.VisibleIndex = 0;
            this.colMatBang.Width = 84;
            // 
            // lookMatBang
            // 
            this.lookMatBang.AutoHeight = false;
            this.lookMatBang.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookMatBang.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("MaSoMB", "Mã MB")});
            this.lookMatBang.DisplayMember = "MaSoMB";
            this.lookMatBang.Name = "lookMatBang";
            this.lookMatBang.NullText = "";
            this.lookMatBang.ValueMember = "MaMB";
            // 
            // colHoTen
            // 
            this.colHoTen.Caption = "Họ tên";
            this.colHoTen.FieldName = "HoTenNK";
            this.colHoTen.Name = "colHoTen";
            this.colHoTen.Visible = true;
            this.colHoTen.VisibleIndex = 1;
            // 
            // colDCTT
            // 
            this.colDCTT.Caption = "Địa chỉ";
            this.colDCTT.FieldName = "DCTT";
            this.colDCTT.Name = "colDCTT";
            this.colDCTT.Visible = true;
            this.colDCTT.VisibleIndex = 2;
            // 
            // colGioiTinh
            // 
            this.colGioiTinh.Caption = "Giới tính";
            this.colGioiTinh.FieldName = "GioiTinh";
            this.colGioiTinh.Name = "colGioiTinh";
            this.colGioiTinh.Visible = true;
            this.colGioiTinh.VisibleIndex = 3;
            // 
            // colNgaySinh
            // 
            this.colNgaySinh.Caption = "Ngày sinh";
            this.colNgaySinh.FieldName = "NgaySinh";
            this.colNgaySinh.Name = "colNgaySinh";
            this.colNgaySinh.Visible = true;
            this.colNgaySinh.VisibleIndex = 4;
            // 
            // colCMND
            // 
            this.colCMND.Caption = "CMND";
            this.colCMND.FieldName = "CMND";
            this.colCMND.Name = "colCMND";
            this.colCMND.Visible = true;
            this.colCMND.VisibleIndex = 5;
            // 
            // colNgayCap
            // 
            this.colNgayCap.Caption = "Ngày cấp";
            this.colNgayCap.FieldName = "NgayCap";
            this.colNgayCap.Name = "colNgayCap";
            this.colNgayCap.Visible = true;
            this.colNgayCap.VisibleIndex = 6;
            // 
            // colNoiCap
            // 
            this.colNoiCap.Caption = "Nơi cấp";
            this.colNoiCap.FieldName = "NoiCap";
            this.colNoiCap.Name = "colNoiCap";
            this.colNoiCap.Visible = true;
            this.colNoiCap.VisibleIndex = 7;
            // 
            // colDienThoai
            // 
            this.colDienThoai.Caption = "Điện thoại";
            this.colDienThoai.FieldName = "DienThoai";
            this.colDienThoai.Name = "colDienThoai";
            this.colDienThoai.Visible = true;
            this.colDienThoai.VisibleIndex = 8;
            // 
            // colEmail
            // 
            this.colEmail.Caption = "Email";
            this.colEmail.FieldName = "Email";
            this.colEmail.Name = "colEmail";
            this.colEmail.Visible = true;
            this.colEmail.VisibleIndex = 9;
            // 
            // colDaDKTT
            // 
            this.colDaDKTT.Caption = "Đã DKTT";
            this.colDaDKTT.FieldName = "DaDKTT";
            this.colDaDKTT.Name = "colDaDKTT";
            this.colDaDKTT.Visible = true;
            this.colDaDKTT.VisibleIndex = 10;
            // 
            // colHetHanDKTT
            // 
            this.colHetHanDKTT.Caption = "Hết hạn DKTT";
            this.colHetHanDKTT.FieldName = "NgayHetHanDKTT";
            this.colHetHanDKTT.Name = "colHetHanDKTT";
            this.colHetHanDKTT.Visible = true;
            this.colHetHanDKTT.VisibleIndex = 11;
            // 
            // colTrangThai
            // 
            this.colTrangThai.Caption = "Trạng thái";
            this.colTrangThai.ColumnEdit = this.lookTrangThai;
            this.colTrangThai.FieldName = "TrangThai";
            this.colTrangThai.Name = "colTrangThai";
            this.colTrangThai.Visible = true;
            this.colTrangThai.VisibleIndex = 12;
            // 
            // lookTrangThai
            // 
            this.lookTrangThai.AutoHeight = false;
            this.lookTrangThai.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookTrangThai.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenTrangThai", "Trạng thái")});
            this.lookTrangThai.DisplayMember = "TenTrangThai";
            this.lookTrangThai.Name = "lookTrangThai";
            this.lookTrangThai.NullText = "";
            this.lookTrangThai.ValueMember = "MaTT";
            // 
            // colQuocTich
            // 
            this.colQuocTich.Caption = "Quốc tịch";
            this.colQuocTich.FieldName = "QuocTich";
            this.colQuocTich.Name = "colQuocTich";
            this.colQuocTich.Visible = true;
            this.colQuocTich.VisibleIndex = 13;
            // 
            // colNgayDK
            // 
            this.colNgayDK.Caption = "Ngày DKNK";
            this.colNgayDK.FieldName = "NgayDK";
            this.colNgayDK.Name = "colNgayDK";
            this.colNgayDK.Visible = true;
            this.colNgayDK.VisibleIndex = 14;
            // 
            // colDienGiai
            // 
            this.colDienGiai.Caption = "Diễn giải";
            this.colDienGiai.FieldName = "DienGiai";
            this.colDienGiai.Name = "colDienGiai";
            this.colDienGiai.Visible = true;
            this.colDienGiai.VisibleIndex = 15;
            // 
            // colKhachHang
            // 
            this.colKhachHang.Caption = "MaKH";
            this.colKhachHang.FieldName = "MaKH";
            this.colKhachHang.Name = "colKhachHang";
            this.colKhachHang.Visible = true;
            this.colKhachHang.VisibleIndex = 16;
            // 
            // QuanHe
            // 
            this.QuanHe.Caption = "Quan hệ";
            this.QuanHe.FieldName = "QuanHe";
            this.QuanHe.Name = "QuanHe";
            this.QuanHe.Visible = true;
            this.QuanHe.VisibleIndex = 17;
            // 
            // CardNumber
            // 
            this.CardNumber.Caption = "CardNumber";
            this.CardNumber.FieldName = "CardNumber";
            this.CardNumber.Name = "CardNumber";
            this.CardNumber.Visible = true;
            this.CardNumber.VisibleIndex = 18;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Error";
            this.gridColumn1.FieldName = "Error";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 19;
            // 
            // itemExport
            // 
            this.itemExport.Caption = "Export mẫu";
            this.itemExport.Id = 4;
            this.itemExport.ImageOptions.ImageIndex = 4;
            this.itemExport.Name = "itemExport";
            this.itemExport.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemExport_ItemClick);
            // 
            // frmImport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1159, 641);
            this.Controls.Add(this.gcNhanKhau);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "frmImport";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Import nhân khẩu";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmImport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcNhanKhau)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvNhanKhau)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookMatBang)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookTrangThai)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarButtonItem btnChonTapTin;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraGrid.GridControl gcNhanKhau;
        private DevExpress.XtraGrid.Views.Grid.GridView grvNhanKhau;
        private DevExpress.XtraGrid.Columns.GridColumn colMatBang;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lookTrangThai;
        private DevExpress.XtraBars.BarButtonItem btnLuu;
        private DevExpress.Utils.ImageCollection imageCollection1;
        private DevExpress.XtraBars.BarButtonItem btnXoaDong;
        private DevExpress.XtraGrid.Columns.GridColumn colHoTen;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lookMatBang;
        private DevExpress.XtraGrid.Columns.GridColumn colDCTT;
        private DevExpress.XtraGrid.Columns.GridColumn colGioiTinh;
        private DevExpress.XtraGrid.Columns.GridColumn colNgaySinh;
        private DevExpress.XtraGrid.Columns.GridColumn colCMND;
        private DevExpress.XtraGrid.Columns.GridColumn colNgayCap;
        private DevExpress.XtraGrid.Columns.GridColumn colNoiCap;
        private DevExpress.XtraGrid.Columns.GridColumn colDienThoai;
        private DevExpress.XtraGrid.Columns.GridColumn colEmail;
        private DevExpress.XtraGrid.Columns.GridColumn colDaDKTT;
        private DevExpress.XtraGrid.Columns.GridColumn colHetHanDKTT;
        private DevExpress.XtraGrid.Columns.GridColumn colTrangThai;
        private DevExpress.XtraGrid.Columns.GridColumn colQuocTich;
        private DevExpress.XtraGrid.Columns.GridColumn colNgayDK;
        private DevExpress.XtraGrid.Columns.GridColumn colDienGiai;
        private DevExpress.XtraGrid.Columns.GridColumn colKhachHang;
        private DevExpress.XtraBars.BarButtonItem itemClose;
        private DevExpress.XtraGrid.Columns.GridColumn QuanHe;
        private DevExpress.XtraGrid.Columns.GridColumn CardNumber;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraBars.BarButtonItem itemExport;
    }
}