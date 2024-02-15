namespace DichVu.HoBoi.Import
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
            this.gcHoBoi = new DevExpress.XtraGrid.GridControl();
            this.grvHoBoi = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colSoThe = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNgayDangKy = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemDateEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.colNgayHetHan = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLoaiThe = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lookLoaiThe = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.colMucPhi = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMatBang = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lookMatBang = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.colNhanKhau = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lookNhanKhau = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.colChuThe = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIsSuDung = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colDienGiai = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMaNK = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIsTinhDuThang = new DevExpress.XtraGrid.Columns.GridColumn();
            this.itemExportMau = new DevExpress.XtraBars.BarButtonItem();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcHoBoi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvHoBoi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookLoaiThe)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookMatBang)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookNhanKhau)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
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
            // gcHoBoi
            // 
            this.gcHoBoi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcHoBoi.Location = new System.Drawing.Point(0, 31);
            this.gcHoBoi.MainView = this.grvHoBoi;
            this.gcHoBoi.Name = "gcHoBoi";
            this.gcHoBoi.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.lookLoaiThe,
            this.lookMatBang,
            this.repositoryItemDateEdit1,
            this.lookNhanKhau,
            this.repositoryItemCheckEdit1});
            this.gcHoBoi.Size = new System.Drawing.Size(1159, 610);
            this.gcHoBoi.TabIndex = 9;
            this.gcHoBoi.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvHoBoi});
            // 
            // grvHoBoi
            // 
            this.grvHoBoi.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colSoThe,
            this.colNgayDangKy,
            this.colNgayHetHan,
            this.colLoaiThe,
            this.colMucPhi,
            this.colMatBang,
            this.colNhanKhau,
            this.colChuThe,
            this.colIsSuDung,
            this.colDienGiai,
            this.colMaNK,
            this.colIsTinhDuThang});
            this.grvHoBoi.GridControl = this.gcHoBoi;
            this.grvHoBoi.GroupPanelText = "Kéo cột lên đây để xem theo nhóm";
            this.grvHoBoi.Name = "grvHoBoi";
            this.grvHoBoi.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.True;
            this.grvHoBoi.OptionsMenu.EnableFooterMenu = false;
            this.grvHoBoi.OptionsSelection.MultiSelect = true;
            this.grvHoBoi.OptionsView.ColumnAutoWidth = false;
            this.grvHoBoi.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;
            this.grvHoBoi.OptionsView.ShowAutoFilterRow = true;
            this.grvHoBoi.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.grvMatBang_RowStyle);
            this.grvHoBoi.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.grvNhanKhau_CellValueChanged);
            // 
            // colSoThe
            // 
            this.colSoThe.Caption = "Số thẻ";
            this.colSoThe.FieldName = "SoThe";
            this.colSoThe.Name = "colSoThe";
            this.colSoThe.Visible = true;
            this.colSoThe.VisibleIndex = 0;
            this.colSoThe.Width = 98;
            // 
            // colNgayDangKy
            // 
            this.colNgayDangKy.Caption = "Ngày đăng ký";
            this.colNgayDangKy.ColumnEdit = this.repositoryItemDateEdit1;
            this.colNgayDangKy.FieldName = "NgayDangKy";
            this.colNgayDangKy.Name = "colNgayDangKy";
            this.colNgayDangKy.Visible = true;
            this.colNgayDangKy.VisibleIndex = 1;
            this.colNgayDangKy.Width = 95;
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
            // colNgayHetHan
            // 
            this.colNgayHetHan.Caption = "Ngày hết hạn";
            this.colNgayHetHan.FieldName = "NgayHetHan";
            this.colNgayHetHan.Name = "colNgayHetHan";
            this.colNgayHetHan.Visible = true;
            this.colNgayHetHan.VisibleIndex = 2;
            // 
            // colLoaiThe
            // 
            this.colLoaiThe.Caption = "Loại thẻ";
            this.colLoaiThe.ColumnEdit = this.lookLoaiThe;
            this.colLoaiThe.FieldName = "MaLT";
            this.colLoaiThe.Name = "colLoaiThe";
            this.colLoaiThe.Visible = true;
            this.colLoaiThe.VisibleIndex = 3;
            this.colLoaiThe.Width = 131;
            // 
            // lookLoaiThe
            // 
            this.lookLoaiThe.AutoHeight = false;
            this.lookLoaiThe.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookLoaiThe.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenLT", "Trạng thái")});
            this.lookLoaiThe.DisplayMember = "TenLT";
            this.lookLoaiThe.Name = "lookLoaiThe";
            this.lookLoaiThe.NullText = "";
            this.lookLoaiThe.ValueMember = "ID";
            // 
            // colMucPhi
            // 
            this.colMucPhi.Caption = "Mức phí";
            this.colMucPhi.FieldName = "MucPhi";
            this.colMucPhi.Name = "colMucPhi";
            this.colMucPhi.Visible = true;
            this.colMucPhi.VisibleIndex = 4;
            this.colMucPhi.Width = 129;
            // 
            // colMatBang
            // 
            this.colMatBang.Caption = "Mặt bằng";
            this.colMatBang.ColumnEdit = this.lookMatBang;
            this.colMatBang.FieldName = "MaMB";
            this.colMatBang.Name = "colMatBang";
            this.colMatBang.Visible = true;
            this.colMatBang.VisibleIndex = 5;
            this.colMatBang.Width = 96;
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
            // colNhanKhau
            // 
            this.colNhanKhau.Caption = "Nhân khẩu";
            this.colNhanKhau.ColumnEdit = this.lookNhanKhau;
            this.colNhanKhau.FieldName = "MaNK";
            this.colNhanKhau.Name = "colNhanKhau";
            this.colNhanKhau.Visible = true;
            this.colNhanKhau.VisibleIndex = 6;
            this.colNhanKhau.Width = 150;
            // 
            // lookNhanKhau
            // 
            this.lookNhanKhau.AutoHeight = false;
            this.lookNhanKhau.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookNhanKhau.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("HoTenNK", "Họ tên")});
            this.lookNhanKhau.DisplayMember = "HoTenNK";
            this.lookNhanKhau.Name = "lookNhanKhau";
            this.lookNhanKhau.NullText = "";
            this.lookNhanKhau.ValueMember = "MaNK";
            // 
            // colChuThe
            // 
            this.colChuThe.Caption = "Chủ thẻ";
            this.colChuThe.FieldName = "ChuThe";
            this.colChuThe.Name = "colChuThe";
            this.colChuThe.Visible = true;
            this.colChuThe.VisibleIndex = 7;
            this.colChuThe.Width = 154;
            // 
            // colIsSuDung
            // 
            this.colIsSuDung.Caption = "Đang sử dụng";
            this.colIsSuDung.ColumnEdit = this.repositoryItemCheckEdit1;
            this.colIsSuDung.FieldName = "IsSuDung";
            this.colIsSuDung.Name = "colIsSuDung";
            this.colIsSuDung.Visible = true;
            this.colIsSuDung.VisibleIndex = 8;
            this.colIsSuDung.Width = 66;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            // 
            // colDienGiai
            // 
            this.colDienGiai.Caption = "Diễn giải";
            this.colDienGiai.FieldName = "DienGiai";
            this.colDienGiai.Name = "colDienGiai";
            this.colDienGiai.Visible = true;
            this.colDienGiai.VisibleIndex = 10;
            this.colDienGiai.Width = 115;
            // 
            // colMaNK
            // 
            this.colMaNK.Caption = "MaNK";
            this.colMaNK.FieldName = "MaNK";
            this.colMaNK.Name = "colMaNK";
            // 
            // colIsTinhDuThang
            // 
            this.colIsTinhDuThang.Caption = "IsTinhDuThang";
            this.colIsTinhDuThang.FieldName = "IsTinhDuThang";
            this.colIsTinhDuThang.Name = "colIsTinhDuThang";
            this.colIsTinhDuThang.Visible = true;
            this.colIsTinhDuThang.VisibleIndex = 9;
            this.colIsTinhDuThang.Width = 51;
            // 
            // itemExportMau
            // 
            this.itemExportMau.Caption = "Export mẫu";
            this.itemExportMau.Id = 4;
            this.itemExportMau.ImageOptions.ImageIndex = 4;
            this.itemExportMau.Name = "itemExportMau";
            this.itemExportMau.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemExportMau_ItemClick);
            // 
            // frmImport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1159, 641);
            this.Controls.Add(this.gcHoBoi);
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
            ((System.ComponentModel.ISupportInitialize)(this.gcHoBoi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvHoBoi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookLoaiThe)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookMatBang)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookNhanKhau)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
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
        private DevExpress.XtraGrid.GridControl gcHoBoi;
        private DevExpress.XtraGrid.Views.Grid.GridView grvHoBoi;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lookLoaiThe;
        private DevExpress.XtraBars.BarButtonItem btnLuu;
        private DevExpress.Utils.ImageCollection imageCollection1;
        private DevExpress.XtraBars.BarButtonItem btnXoaDong;
        private DevExpress.XtraGrid.Columns.GridColumn colSoThe;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lookMatBang;
        private DevExpress.XtraGrid.Columns.GridColumn colLoaiThe;
        private DevExpress.XtraGrid.Columns.GridColumn colMucPhi;
        private DevExpress.XtraGrid.Columns.GridColumn colNgayDangKy;
        private DevExpress.XtraGrid.Columns.GridColumn colMatBang;
        private DevExpress.XtraGrid.Columns.GridColumn colNhanKhau;
        private DevExpress.XtraGrid.Columns.GridColumn colChuThe;
        private DevExpress.XtraGrid.Columns.GridColumn colIsSuDung;
        private DevExpress.XtraGrid.Columns.GridColumn colNgayHetHan;
        private DevExpress.XtraGrid.Columns.GridColumn colDienGiai;
        private DevExpress.XtraGrid.Columns.GridColumn colMaNK;
        private DevExpress.XtraBars.BarButtonItem itemClose;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit repositoryItemDateEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lookNhanKhau;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn colIsTinhDuThang;
        private DevExpress.XtraBars.BarButtonItem itemExportMau;
    }
}