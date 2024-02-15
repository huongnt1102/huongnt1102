namespace KyThuat.KhachHang.Import
{
    partial class frmImportDoanhNghiep
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmImportDoanhNghiep));
            this.imageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.btnChonTapTin = new DevExpress.XtraBars.BarButtonItem();
            this.btnLuu = new DevExpress.XtraBars.BarButtonItem();
            this.btnXoaDong = new DevExpress.XtraBars.BarButtonItem();
            this.itemClose = new DevExpress.XtraBars.BarButtonItem();
            this.itemExportMau = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.gcDoanhNghiep = new DevExpress.XtraGrid.GridControl();
            this.grvDoanhNghiep = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colTenVT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTenCty = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMST = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDienThoai = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDiaChi = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colKhuVuc = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lookKhuVuc = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.colFax = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colKyHieu = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNgayDKKD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemDateEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.colNoiDKKD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSoTaiKhoan = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNganHang = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMaPhu = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNhomKH = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.NguoiDaiDien = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ChucVu = new DevExpress.XtraGrid.Columns.GridColumn();
            this.NguoiLH = new DevExpress.XtraGrid.Columns.GridColumn();
            this.SDTNguoiLH = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDiaChiNhanThu = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colQuocTich = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colWebsite = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNganhNghe = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colError = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcDoanhNghiep)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvDoanhNghiep)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookKhuVuc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1.CalendarTimeProperties)).BeginInit();
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
            this.itemExportMau});
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
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnXoaDong, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemClose, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemExportMau, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
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
            // itemExportMau
            // 
            this.itemExportMau.Caption = "Export mẫu";
            this.itemExportMau.Id = 4;
            this.itemExportMau.ImageOptions.ImageIndex = 4;
            this.itemExportMau.Name = "itemExportMau";
            this.itemExportMau.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemExportMau_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.barDockControlTop.Size = new System.Drawing.Size(1202, 37);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 635);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.barDockControlBottom.Size = new System.Drawing.Size(1202, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 37);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 598);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1202, 37);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 598);
            // 
            // gcDoanhNghiep
            // 
            this.gcDoanhNghiep.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcDoanhNghiep.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gcDoanhNghiep.Location = new System.Drawing.Point(0, 37);
            this.gcDoanhNghiep.MainView = this.grvDoanhNghiep;
            this.gcDoanhNghiep.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gcDoanhNghiep.MenuManager = this.barManager1;
            this.gcDoanhNghiep.Name = "gcDoanhNghiep";
            this.gcDoanhNghiep.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.lookKhuVuc,
            this.repositoryItemDateEdit1});
            this.gcDoanhNghiep.Size = new System.Drawing.Size(1202, 598);
            this.gcDoanhNghiep.TabIndex = 4;
            this.gcDoanhNghiep.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvDoanhNghiep});
            // 
            // grvDoanhNghiep
            // 
            this.grvDoanhNghiep.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colTenVT,
            this.colTenCty,
            this.colMST,
            this.colDienThoai,
            this.colDiaChi,
            this.colKhuVuc,
            this.colFax,
            this.colKyHieu,
            this.colNgayDKKD,
            this.colNoiDKKD,
            this.colSoTaiKhoan,
            this.colNganHang,
            this.colMaPhu,
            this.colNhomKH,
            this.gridColumn1,
            this.NguoiDaiDien,
            this.ChucVu,
            this.NguoiLH,
            this.SDTNguoiLH,
            this.colDiaChiNhanThu,
            this.colQuocTich,
            this.colWebsite,
            this.colNganhNghe,
            this.gridColumn2,
            this.gridColumn3,
            this.colError});
            this.grvDoanhNghiep.DetailHeight = 431;
            this.grvDoanhNghiep.GridControl = this.gcDoanhNghiep;
            this.grvDoanhNghiep.Name = "grvDoanhNghiep";
            this.grvDoanhNghiep.OptionsCustomization.AllowGroup = false;
            this.grvDoanhNghiep.OptionsSelection.MultiSelect = true;
            this.grvDoanhNghiep.OptionsView.ColumnAutoWidth = false;
            this.grvDoanhNghiep.OptionsView.ShowAutoFilterRow = true;
            this.grvDoanhNghiep.OptionsView.ShowGroupPanel = false;
            this.grvDoanhNghiep.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.grvDoanhNghiep_RowStyle);
            // 
            // colTenVT
            // 
            this.colTenVT.Caption = "Tên viết tắt";
            this.colTenVT.FieldName = "TenVT";
            this.colTenVT.MinWidth = 23;
            this.colTenVT.Name = "colTenVT";
            this.colTenVT.Visible = true;
            this.colTenVT.VisibleIndex = 2;
            this.colTenVT.Width = 203;
            // 
            // colTenCty
            // 
            this.colTenCty.Caption = "Tên công ty";
            this.colTenCty.FieldName = "TenCty";
            this.colTenCty.MinWidth = 23;
            this.colTenCty.Name = "colTenCty";
            this.colTenCty.Visible = true;
            this.colTenCty.VisibleIndex = 3;
            this.colTenCty.Width = 170;
            // 
            // colMST
            // 
            this.colMST.Caption = "Mã số thuế";
            this.colMST.FieldName = "MaSoThue";
            this.colMST.MinWidth = 23;
            this.colMST.Name = "colMST";
            this.colMST.Visible = true;
            this.colMST.VisibleIndex = 7;
            this.colMST.Width = 111;
            // 
            // colDienThoai
            // 
            this.colDienThoai.Caption = "Điện thoại";
            this.colDienThoai.FieldName = "DienThoai";
            this.colDienThoai.MinWidth = 23;
            this.colDienThoai.Name = "colDienThoai";
            this.colDienThoai.Visible = true;
            this.colDienThoai.VisibleIndex = 5;
            this.colDienThoai.Width = 119;
            // 
            // colDiaChi
            // 
            this.colDiaChi.Caption = "Địa chỉ";
            this.colDiaChi.FieldName = "DiaChiCty";
            this.colDiaChi.MinWidth = 23;
            this.colDiaChi.Name = "colDiaChi";
            this.colDiaChi.Visible = true;
            this.colDiaChi.VisibleIndex = 4;
            this.colDiaChi.Width = 227;
            // 
            // colKhuVuc
            // 
            this.colKhuVuc.Caption = "Khu vực";
            this.colKhuVuc.ColumnEdit = this.lookKhuVuc;
            this.colKhuVuc.FieldName = "KhuVuc";
            this.colKhuVuc.MinWidth = 23;
            this.colKhuVuc.Name = "colKhuVuc";
            this.colKhuVuc.Visible = true;
            this.colKhuVuc.VisibleIndex = 10;
            this.colKhuVuc.Width = 119;
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
            // colFax
            // 
            this.colFax.Caption = "Fax";
            this.colFax.FieldName = "Fax";
            this.colFax.MinWidth = 23;
            this.colFax.Name = "colFax";
            this.colFax.Visible = true;
            this.colFax.VisibleIndex = 6;
            this.colFax.Width = 99;
            // 
            // colKyHieu
            // 
            this.colKyHieu.Caption = "Mã KH";
            this.colKyHieu.FieldName = "KyHieu";
            this.colKyHieu.MinWidth = 23;
            this.colKyHieu.Name = "colKyHieu";
            this.colKyHieu.Visible = true;
            this.colKyHieu.VisibleIndex = 0;
            this.colKyHieu.Width = 99;
            // 
            // colNgayDKKD
            // 
            this.colNgayDKKD.Caption = "Ngày ĐKKD";
            this.colNgayDKKD.ColumnEdit = this.repositoryItemDateEdit1;
            this.colNgayDKKD.FieldName = "NgayDKKD";
            this.colNgayDKKD.MinWidth = 23;
            this.colNgayDKKD.Name = "colNgayDKKD";
            this.colNgayDKKD.Visible = true;
            this.colNgayDKKD.VisibleIndex = 8;
            this.colNgayDKKD.Width = 91;
            // 
            // repositoryItemDateEdit1
            // 
            this.repositoryItemDateEdit1.AutoHeight = false;
            this.repositoryItemDateEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemDateEdit1.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.repositoryItemDateEdit1.Name = "repositoryItemDateEdit1";
            // 
            // colNoiDKKD
            // 
            this.colNoiDKKD.Caption = "Nơi ĐKKD";
            this.colNoiDKKD.FieldName = "NoiDKKD";
            this.colNoiDKKD.MinWidth = 23;
            this.colNoiDKKD.Name = "colNoiDKKD";
            this.colNoiDKKD.Visible = true;
            this.colNoiDKKD.VisibleIndex = 9;
            this.colNoiDKKD.Width = 141;
            // 
            // colSoTaiKhoan
            // 
            this.colSoTaiKhoan.Caption = "Số tài khoản";
            this.colSoTaiKhoan.FieldName = "SoTaiKhoan";
            this.colSoTaiKhoan.MinWidth = 23;
            this.colSoTaiKhoan.Name = "colSoTaiKhoan";
            this.colSoTaiKhoan.Visible = true;
            this.colSoTaiKhoan.VisibleIndex = 11;
            this.colSoTaiKhoan.Width = 124;
            // 
            // colNganHang
            // 
            this.colNganHang.Caption = "Ngân hàng";
            this.colNganHang.FieldName = "NganHang";
            this.colNganHang.MinWidth = 23;
            this.colNganHang.Name = "colNganHang";
            this.colNganHang.Visible = true;
            this.colNganHang.VisibleIndex = 12;
            this.colNganHang.Width = 199;
            // 
            // colMaPhu
            // 
            this.colMaPhu.Caption = "Mã phụ";
            this.colMaPhu.FieldName = "MaPhu";
            this.colMaPhu.MinWidth = 23;
            this.colMaPhu.Name = "colMaPhu";
            this.colMaPhu.Visible = true;
            this.colMaPhu.VisibleIndex = 1;
            this.colMaPhu.Width = 97;
            // 
            // colNhomKH
            // 
            this.colNhomKH.Caption = "Nhóm KH";
            this.colNhomKH.FieldName = "NhomKH";
            this.colNhomKH.MinWidth = 23;
            this.colNhomKH.Name = "colNhomKH";
            this.colNhomKH.Visible = true;
            this.colNhomKH.VisibleIndex = 13;
            this.colNhomKH.Width = 175;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Email";
            this.gridColumn1.FieldName = "Email";
            this.gridColumn1.MinWidth = 23;
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 14;
            this.gridColumn1.Width = 87;
            // 
            // NguoiDaiDien
            // 
            this.NguoiDaiDien.Caption = "Người đại diện";
            this.NguoiDaiDien.FieldName = "NguoiDaiDien";
            this.NguoiDaiDien.MinWidth = 23;
            this.NguoiDaiDien.Name = "NguoiDaiDien";
            this.NguoiDaiDien.Visible = true;
            this.NguoiDaiDien.VisibleIndex = 15;
            this.NguoiDaiDien.Width = 87;
            // 
            // ChucVu
            // 
            this.ChucVu.Caption = "Chức vụ";
            this.ChucVu.FieldName = "ChucVu";
            this.ChucVu.MinWidth = 23;
            this.ChucVu.Name = "ChucVu";
            this.ChucVu.Visible = true;
            this.ChucVu.VisibleIndex = 16;
            this.ChucVu.Width = 87;
            // 
            // NguoiLH
            // 
            this.NguoiLH.Caption = "Người liên hệ";
            this.NguoiLH.FieldName = "TenNLH";
            this.NguoiLH.MinWidth = 23;
            this.NguoiLH.Name = "NguoiLH";
            this.NguoiLH.Visible = true;
            this.NguoiLH.VisibleIndex = 17;
            this.NguoiLH.Width = 87;
            // 
            // SDTNguoiLH
            // 
            this.SDTNguoiLH.Caption = "SĐT NLH";
            this.SDTNguoiLH.FieldName = "SDTNLH";
            this.SDTNguoiLH.MinWidth = 23;
            this.SDTNguoiLH.Name = "SDTNguoiLH";
            this.SDTNguoiLH.Width = 87;
            // 
            // colDiaChiNhanThu
            // 
            this.colDiaChiNhanThu.Caption = "Địa chỉ nhận thư";
            this.colDiaChiNhanThu.FieldName = "DiaChiNhanThu";
            this.colDiaChiNhanThu.MinWidth = 23;
            this.colDiaChiNhanThu.Name = "colDiaChiNhanThu";
            this.colDiaChiNhanThu.Visible = true;
            this.colDiaChiNhanThu.VisibleIndex = 18;
            this.colDiaChiNhanThu.Width = 87;
            // 
            // colQuocTich
            // 
            this.colQuocTich.Caption = "Quốc tịch";
            this.colQuocTich.FieldName = "QuocTich";
            this.colQuocTich.MinWidth = 23;
            this.colQuocTich.Name = "colQuocTich";
            this.colQuocTich.Visible = true;
            this.colQuocTich.VisibleIndex = 19;
            this.colQuocTich.Width = 87;
            // 
            // colWebsite
            // 
            this.colWebsite.Caption = "Website";
            this.colWebsite.FieldName = "Website";
            this.colWebsite.MinWidth = 23;
            this.colWebsite.Name = "colWebsite";
            this.colWebsite.Visible = true;
            this.colWebsite.VisibleIndex = 20;
            this.colWebsite.Width = 87;
            // 
            // colNganhNghe
            // 
            this.colNganhNghe.Caption = "Ngành nghề";
            this.colNganhNghe.FieldName = "NganhNgheDoanhNghiep";
            this.colNganhNghe.MinWidth = 23;
            this.colNganhNghe.Name = "colNganhNghe";
            this.colNganhNghe.Visible = true;
            this.colNganhNghe.VisibleIndex = 21;
            this.colNganhNghe.Width = 87;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Email khách thuê";
            this.gridColumn2.FieldName = "EmailKhachThue";
            this.gridColumn2.MinWidth = 23;
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 22;
            this.gridColumn2.Width = 87;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Địa phận";
            this.gridColumn3.FieldName = "DiaPhan";
            this.gridColumn3.MinWidth = 23;
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 23;
            this.gridColumn3.Width = 87;
            // 
            // colError
            // 
            this.colError.Caption = "Lỗi";
            this.colError.FieldName = "Error";
            this.colError.MinWidth = 25;
            this.colError.Name = "colError";
            this.colError.Visible = true;
            this.colError.VisibleIndex = 24;
            this.colError.Width = 109;
            // 
            // frmImportDoanhNghiep
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1202, 635);
            this.Controls.Add(this.gcDoanhNghiep);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "frmImportDoanhNghiep";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Import khách hàng (Doanh nghiệp)";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmImport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcDoanhNghiep)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvDoanhNghiep)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookKhuVuc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1)).EndInit();
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
        private DevExpress.XtraGrid.GridControl gcDoanhNghiep;
        private DevExpress.XtraGrid.Views.Grid.GridView grvDoanhNghiep;
        private DevExpress.XtraGrid.Columns.GridColumn colTenVT;
        private DevExpress.XtraGrid.Columns.GridColumn colTenCty;
        private DevExpress.XtraGrid.Columns.GridColumn colMST;
        private DevExpress.XtraGrid.Columns.GridColumn colDienThoai;
        private DevExpress.XtraGrid.Columns.GridColumn colDiaChi;
        private DevExpress.XtraGrid.Columns.GridColumn colKhuVuc;
        private DevExpress.XtraGrid.Columns.GridColumn colFax;
        private DevExpress.XtraGrid.Columns.GridColumn colKyHieu;
        private DevExpress.XtraGrid.Columns.GridColumn colNgayDKKD;
        private DevExpress.XtraGrid.Columns.GridColumn colNoiDKKD;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lookKhuVuc;
        private DevExpress.XtraGrid.Columns.GridColumn colSoTaiKhoan;
        private DevExpress.XtraGrid.Columns.GridColumn colNganHang;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit repositoryItemDateEdit1;
        private DevExpress.XtraBars.BarButtonItem btnXoaDong;
        private DevExpress.XtraGrid.Columns.GridColumn colMaPhu;
        private DevExpress.XtraBars.BarButtonItem itemClose;
        private DevExpress.XtraGrid.Columns.GridColumn colNhomKH;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn NguoiDaiDien;
        private DevExpress.XtraGrid.Columns.GridColumn ChucVu;
        private DevExpress.XtraGrid.Columns.GridColumn NguoiLH;
        private DevExpress.XtraGrid.Columns.GridColumn SDTNguoiLH;
        private DevExpress.XtraBars.BarButtonItem itemExportMau;
        private DevExpress.XtraGrid.Columns.GridColumn colDiaChiNhanThu;
        private DevExpress.XtraGrid.Columns.GridColumn colQuocTich;
        private DevExpress.XtraGrid.Columns.GridColumn colWebsite;
        private DevExpress.XtraGrid.Columns.GridColumn colNganhNghe;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn colError;
    }
}