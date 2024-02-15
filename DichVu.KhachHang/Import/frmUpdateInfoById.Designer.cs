namespace KyThuat.KhachHang.Import
{
    partial class frmUpdateInfoById
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUpdateInfoById));
            this.imageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.btnChonTapTin = new DevExpress.XtraBars.BarButtonItem();
            this.itemSheet = new DevExpress.XtraBars.BarEditItem();
            this.cmbSheet = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.btnLuu = new DevExpress.XtraBars.BarButtonItem();
            this.btnXoaDong = new DevExpress.XtraBars.BarButtonItem();
            this.itemClose = new DevExpress.XtraBars.BarButtonItem();
            this.itemExportMau = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.gcCaNhan = new DevExpress.XtraGrid.GridControl();
            this.grvCaNhan = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colTen = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSoCMND = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDienThoai = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDiaChi = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colKhuVuc = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDiaChiThuongTru = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colKyHieu = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNgayCap = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNoiCap = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTaiKhoanNganHang = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colGioiTinh = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMaPhu = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMaSoThue = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNhomKH = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNguoiDongSoHuu = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lookKhuVuc = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSheet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcCaNhan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvCaNhan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookKhuVuc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
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
            this.itemExportMau,
            this.itemSheet});
            this.barManager1.MaxItemId = 6;
            this.barManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.cmbSheet});
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnChonTapTin, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.Width, this.itemSheet, "", false, true, true, 142),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnLuu, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnXoaDong, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
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
            // itemSheet
            // 
            this.itemSheet.Caption = "Chọn sheet";
            this.itemSheet.Edit = this.cmbSheet;
            this.itemSheet.Id = 5;
            this.itemSheet.Name = "itemSheet";
            this.itemSheet.EditValueChanged += new System.EventHandler(this.itemSheet_EditValueChanged);
            // 
            // cmbSheet
            // 
            this.cmbSheet.AutoHeight = false;
            this.cmbSheet.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbSheet.Name = "cmbSheet";
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
            this.gcCaNhan.Location = new System.Drawing.Point(2, 2);
            this.gcCaNhan.MainView = this.grvCaNhan;
            this.gcCaNhan.MenuManager = this.barManager1;
            this.gcCaNhan.Name = "gcCaNhan";
            this.gcCaNhan.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.lookKhuVuc});
            this.gcCaNhan.Size = new System.Drawing.Size(1026, 480);
            this.gcCaNhan.TabIndex = 4;
            this.gcCaNhan.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvCaNhan});
            // 
            // grvCaNhan
            // 
            this.grvCaNhan.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colTen,
            this.colSoCMND,
            this.colDienThoai,
            this.colDiaChi,
            this.colKhuVuc,
            this.colDiaChiThuongTru,
            this.colKyHieu,
            this.colNgayCap,
            this.colNoiCap,
            this.colTaiKhoanNganHang,
            this.colGioiTinh,
            this.colMaPhu,
            this.colMaSoThue,
            this.colNhomKH,
            this.gridColumn1,
            this.colNguoiDongSoHuu,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn6,
            this.gridColumn2,
            this.gridColumn7});
            this.grvCaNhan.GridControl = this.gcCaNhan;
            this.grvCaNhan.Name = "grvCaNhan";
            this.grvCaNhan.OptionsCustomization.AllowGroup = false;
            this.grvCaNhan.OptionsSelection.MultiSelect = true;
            this.grvCaNhan.OptionsView.ColumnAutoWidth = false;
            this.grvCaNhan.OptionsView.ShowAutoFilterRow = true;
            this.grvCaNhan.OptionsView.ShowGroupPanel = false;
            // 
            // colTen
            // 
            this.colTen.Caption = "Tên";
            this.colTen.FieldName = "TenKH";
            this.colTen.Name = "colTen";
            this.colTen.Visible = true;
            this.colTen.VisibleIndex = 2;
            this.colTen.Width = 82;
            // 
            // colSoCMND
            // 
            this.colSoCMND.Caption = "Số CMND";
            this.colSoCMND.FieldName = "CMND";
            this.colSoCMND.Name = "colSoCMND";
            this.colSoCMND.Visible = true;
            this.colSoCMND.VisibleIndex = 5;
            this.colSoCMND.Width = 82;
            // 
            // colDienThoai
            // 
            this.colDienThoai.Caption = "Điện thoại";
            this.colDienThoai.FieldName = "DienThoai";
            this.colDienThoai.Name = "colDienThoai";
            this.colDienThoai.Visible = true;
            this.colDienThoai.VisibleIndex = 7;
            this.colDienThoai.Width = 102;
            // 
            // colDiaChi
            // 
            this.colDiaChi.Caption = "Địa chỉ liên lạc";
            this.colDiaChi.FieldName = "DiaChiLL";
            this.colDiaChi.Name = "colDiaChi";
            this.colDiaChi.Visible = true;
            this.colDiaChi.VisibleIndex = 8;
            this.colDiaChi.Width = 195;
            // 
            // colKhuVuc
            // 
            this.colKhuVuc.Caption = "Khu vực";
            this.colKhuVuc.FieldName = "KhuVuc";
            this.colKhuVuc.Name = "colKhuVuc";
            this.colKhuVuc.Visible = true;
            this.colKhuVuc.VisibleIndex = 6;
            this.colKhuVuc.Width = 102;
            // 
            // colDiaChiThuongTru
            // 
            this.colDiaChiThuongTru.Caption = "Địa chỉ thường trú";
            this.colDiaChiThuongTru.FieldName = "DiaChiThuongTru";
            this.colDiaChiThuongTru.Name = "colDiaChiThuongTru";
            this.colDiaChiThuongTru.Visible = true;
            this.colDiaChiThuongTru.VisibleIndex = 10;
            this.colDiaChiThuongTru.Width = 187;
            // 
            // colKyHieu
            // 
            this.colKyHieu.Caption = "Mã KH";
            this.colKyHieu.FieldName = "KyHieu";
            this.colKyHieu.Name = "colKyHieu";
            this.colKyHieu.Visible = true;
            this.colKyHieu.VisibleIndex = 0;
            this.colKyHieu.Width = 87;
            // 
            // colNgayCap
            // 
            this.colNgayCap.Caption = "Ngày cấp";
            this.colNgayCap.FieldName = "NgayCap";
            this.colNgayCap.Name = "colNgayCap";
            this.colNgayCap.Visible = true;
            this.colNgayCap.VisibleIndex = 11;
            // 
            // colNoiCap
            // 
            this.colNoiCap.Caption = "Nơi cấp";
            this.colNoiCap.FieldName = "NoiCap";
            this.colNoiCap.Name = "colNoiCap";
            this.colNoiCap.Visible = true;
            this.colNoiCap.VisibleIndex = 12;
            // 
            // colTaiKhoanNganHang
            // 
            this.colTaiKhoanNganHang.Caption = "Tài khoản ngân hàng";
            this.colTaiKhoanNganHang.FieldName = "TaiKhoanNganHang";
            this.colTaiKhoanNganHang.Name = "colTaiKhoanNganHang";
            this.colTaiKhoanNganHang.Visible = true;
            this.colTaiKhoanNganHang.VisibleIndex = 13;
            this.colTaiKhoanNganHang.Width = 121;
            // 
            // colGioiTinh
            // 
            this.colGioiTinh.Caption = "Giới tính";
            this.colGioiTinh.FieldName = "GioiTinh";
            this.colGioiTinh.Name = "colGioiTinh";
            this.colGioiTinh.Visible = true;
            this.colGioiTinh.VisibleIndex = 3;
            // 
            // colMaPhu
            // 
            this.colMaPhu.Caption = "Mã phụ";
            this.colMaPhu.FieldName = "MaPhu";
            this.colMaPhu.Name = "colMaPhu";
            this.colMaPhu.Visible = true;
            this.colMaPhu.VisibleIndex = 1;
            this.colMaPhu.Width = 91;
            // 
            // colMaSoThue
            // 
            this.colMaSoThue.Caption = "Mã số thuế";
            this.colMaSoThue.FieldName = "MaSoThue";
            this.colMaSoThue.Name = "colMaSoThue";
            this.colMaSoThue.Visible = true;
            this.colMaSoThue.VisibleIndex = 4;
            this.colMaSoThue.Width = 93;
            // 
            // colNhomKH
            // 
            this.colNhomKH.Caption = "Nhóm KH";
            this.colNhomKH.FieldName = "NhomKH";
            this.colNhomKH.Name = "colNhomKH";
            this.colNhomKH.Visible = true;
            this.colNhomKH.VisibleIndex = 14;
            this.colNhomKH.Width = 150;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Email";
            this.gridColumn1.FieldName = "Email";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 9;
            // 
            // colNguoiDongSoHuu
            // 
            this.colNguoiDongSoHuu.Caption = "Đồng sở hữu";
            this.colNguoiDongSoHuu.FieldName = "DongSH";
            this.colNguoiDongSoHuu.Name = "colNguoiDongSoHuu";
            this.colNguoiDongSoHuu.Visible = true;
            this.colNguoiDongSoHuu.VisibleIndex = 15;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Địa chỉ nhận thư";
            this.gridColumn3.FieldName = "DiaChiNhanThu";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 16;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Quốc tịch";
            this.gridColumn4.FieldName = "QuocTich";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 17;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Email khách thuê";
            this.gridColumn5.FieldName = "EmailKhachThue";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 18;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "Địa phận";
            this.gridColumn6.FieldName = "DiaPhan";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 19;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Lỗi";
            this.gridColumn2.FieldName = "Error";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 20;
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
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.gcCaNhan);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 31);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1030, 484);
            this.panelControl1.TabIndex = 9;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "ID";
            this.gridColumn7.FieldName = "MaKH";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 21;
            // 
            // frmUpdateInfoById
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1030, 515);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "frmUpdateInfoById";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Import khách hàng (Cá Nhân)";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmImport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSheet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcCaNhan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvCaNhan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookKhuVuc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
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
        private DevExpress.XtraGrid.Columns.GridColumn colTen;
        private DevExpress.XtraGrid.Columns.GridColumn colSoCMND;
        private DevExpress.XtraGrid.Columns.GridColumn colDienThoai;
        private DevExpress.XtraGrid.Columns.GridColumn colDiaChi;
        private DevExpress.XtraGrid.Columns.GridColumn colKhuVuc;
        private DevExpress.XtraGrid.Columns.GridColumn colDiaChiThuongTru;
        private DevExpress.XtraGrid.Columns.GridColumn colKyHieu;
        private DevExpress.XtraGrid.Columns.GridColumn colNgayCap;
        private DevExpress.XtraGrid.Columns.GridColumn colNoiCap;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lookKhuVuc;
        private DevExpress.XtraGrid.Columns.GridColumn colTaiKhoanNganHang;
        private DevExpress.XtraGrid.Columns.GridColumn colGioiTinh;
        private DevExpress.XtraBars.BarButtonItem btnXoaDong;
        private DevExpress.XtraGrid.Columns.GridColumn colMaPhu;
        private DevExpress.XtraBars.BarButtonItem itemClose;
        private DevExpress.XtraGrid.Columns.GridColumn colMaSoThue;
        private DevExpress.XtraGrid.Columns.GridColumn colNhomKH;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn colNguoiDongSoHuu;
        private DevExpress.XtraBars.BarButtonItem itemExportMau;
        private DevExpress.XtraBars.BarEditItem itemSheet;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox cmbSheet;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
    }
}