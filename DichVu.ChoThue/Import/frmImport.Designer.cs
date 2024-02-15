namespace DichVu.ChoThue.Import
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
            this.btnXoa = new DevExpress.XtraBars.BarButtonItem();
            this.btnLuu = new DevExpress.XtraBars.BarButtonItem();
            this.itemExportMau = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection();
            this.gc = new DevExpress.XtraGrid.GridControl();
            this.gv = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colKyHieu = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNgayKy = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colKhachHang = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lookKhachHang = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.colTrangThai = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lookTrangThai = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.colMatBang = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lookMatBang = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.colThanhTien = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colVAT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTong = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookKhachHang)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookTrangThai)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookMatBang)).BeginInit();
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
            this.btnXoa,
            this.itemExportMau});
            this.barManager1.MaxItemId = 4;
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnChonTapTin, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnXoa, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnLuu, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
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
            // btnXoa
            // 
            this.btnXoa.Caption = "Xóa dòng";
            this.btnXoa.Id = 2;
            this.btnXoa.ImageOptions.ImageIndex = 1;
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnXoa_ItemClick);
            // 
            // btnLuu
            // 
            this.btnLuu.Caption = "Lưu dữ liệu xuống database";
            this.btnLuu.Id = 1;
            this.btnLuu.ImageOptions.ImageIndex = 2;
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnLuu_ItemClick);
            // 
            // itemExportMau
            // 
            this.itemExportMau.Caption = "Export mẫu";
            this.itemExportMau.Id = 3;
            this.itemExportMau.ImageOptions.ImageIndex = 3;
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
            this.barDockControlTop.Size = new System.Drawing.Size(1007, 31);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 506);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.barDockControlBottom.Size = new System.Drawing.Size(1007, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 31);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 475);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1007, 31);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 475);
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.Images.SetKeyName(0, "icons8_open.png");
            this.imageCollection1.Images.SetKeyName(1, "icons8_delete1.png");
            this.imageCollection1.Images.SetKeyName(2, "icons8_save.png");
            this.imageCollection1.Images.SetKeyName(3, "Export1.png");
            // 
            // gc
            // 
            this.gc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gc.Location = new System.Drawing.Point(0, 31);
            this.gc.MainView = this.gv;
            this.gc.MenuManager = this.barManager1;
            this.gc.Name = "gc";
            this.gc.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.lookKhachHang,
            this.lookTrangThai,
            this.lookMatBang});
            this.gc.Size = new System.Drawing.Size(1007, 475);
            this.gc.TabIndex = 4;
            this.gc.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gv});
            // 
            // gv
            // 
            this.gv.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colKyHieu,
            this.colNgayKy,
            this.colKhachHang,
            this.colTrangThai,
            this.colMatBang,
            this.colThanhTien,
            this.colVAT,
            this.colTong});
            this.gv.GridControl = this.gc;
            this.gv.Name = "gv";
            this.gv.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.True;
            this.gv.OptionsSelection.MultiSelect = true;
            this.gv.OptionsView.ShowAutoFilterRow = true;
            // 
            // colKyHieu
            // 
            this.colKyHieu.Caption = "Số hợp đồng";
            this.colKyHieu.FieldName = "KyHieu";
            this.colKyHieu.Name = "colKyHieu";
            this.colKyHieu.Visible = true;
            this.colKyHieu.VisibleIndex = 0;
            // 
            // colNgayKy
            // 
            this.colNgayKy.Caption = "Ngày ký";
            this.colNgayKy.FieldName = "NgayKy";
            this.colNgayKy.Name = "colNgayKy";
            this.colNgayKy.Visible = true;
            this.colNgayKy.VisibleIndex = 1;
            // 
            // colKhachHang
            // 
            this.colKhachHang.Caption = "Khách hàng";
            this.colKhachHang.ColumnEdit = this.lookKhachHang;
            this.colKhachHang.FieldName = "MaKH";
            this.colKhachHang.Name = "colKhachHang";
            this.colKhachHang.Visible = true;
            this.colKhachHang.VisibleIndex = 3;
            // 
            // lookKhachHang
            // 
            this.lookKhachHang.AutoHeight = false;
            this.lookKhachHang.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookKhachHang.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("KhachHang", "Khách hàng")});
            this.lookKhachHang.DisplayMember = "KhachHang";
            this.lookKhachHang.Name = "lookKhachHang";
            this.lookKhachHang.NullText = "";
            this.lookKhachHang.ValueMember = "MaKH";
            // 
            // colTrangThai
            // 
            this.colTrangThai.Caption = "Trạng thái";
            this.colTrangThai.ColumnEdit = this.lookTrangThai;
            this.colTrangThai.FieldName = "MaTT";
            this.colTrangThai.Name = "colTrangThai";
            this.colTrangThai.Visible = true;
            this.colTrangThai.VisibleIndex = 4;
            // 
            // lookTrangThai
            // 
            this.lookTrangThai.AutoHeight = false;
            this.lookTrangThai.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookTrangThai.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenTT", "Trạng thái")});
            this.lookTrangThai.DisplayMember = "TenTT";
            this.lookTrangThai.Name = "lookTrangThai";
            this.lookTrangThai.NullText = "";
            this.lookTrangThai.ValueMember = "MaTT";
            // 
            // colMatBang
            // 
            this.colMatBang.Caption = "Mặt bằng";
            this.colMatBang.ColumnEdit = this.lookMatBang;
            this.colMatBang.FieldName = "MaMB";
            this.colMatBang.Name = "colMatBang";
            this.colMatBang.Visible = true;
            this.colMatBang.VisibleIndex = 2;
            // 
            // lookMatBang
            // 
            this.lookMatBang.AutoHeight = false;
            this.lookMatBang.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookMatBang.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("MaSoMB", "Mặt bằng")});
            this.lookMatBang.DisplayMember = "MaSoMB";
            this.lookMatBang.Name = "lookMatBang";
            this.lookMatBang.NullText = "";
            this.lookMatBang.ValueMember = "MaMB";
            // 
            // colThanhTien
            // 
            this.colThanhTien.Caption = "Thành tiền";
            this.colThanhTien.DisplayFormat.FormatString = "{0:#,0.##}";
            this.colThanhTien.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colThanhTien.FieldName = "ThanhTien";
            this.colThanhTien.Name = "colThanhTien";
            this.colThanhTien.Visible = true;
            this.colThanhTien.VisibleIndex = 5;
            // 
            // colVAT
            // 
            this.colVAT.Caption = "VAT";
            this.colVAT.DisplayFormat.FormatString = "{0:#,0.##}";
            this.colVAT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colVAT.FieldName = "ThueVAT";
            this.colVAT.Name = "colVAT";
            this.colVAT.Visible = true;
            this.colVAT.VisibleIndex = 6;
            // 
            // colTong
            // 
            this.colTong.Caption = "Tổng tiền";
            this.colTong.DisplayFormat.FormatString = "{0:#,0.##}";
            this.colTong.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colTong.FieldName = "TongThanhTien";
            this.colTong.Name = "colTong";
            this.colTong.Visible = true;
            this.colTong.VisibleIndex = 7;
            // 
            // frmImport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1007, 506);
            this.Controls.Add(this.gc);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "frmImport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Import hợp đồng";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmImport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookKhachHang)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookTrangThai)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookMatBang)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarButtonItem btnChonTapTin;
        private DevExpress.XtraBars.BarButtonItem btnLuu;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.Utils.ImageCollection imageCollection1;
        private DevExpress.XtraGrid.GridControl gc;
        private DevExpress.XtraGrid.Views.Grid.GridView gv;
        private DevExpress.XtraBars.BarButtonItem btnXoa;
        private DevExpress.XtraGrid.Columns.GridColumn colKyHieu;
        private DevExpress.XtraGrid.Columns.GridColumn colNgayKy;
        private DevExpress.XtraGrid.Columns.GridColumn colKhachHang;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lookKhachHang;
        private DevExpress.XtraGrid.Columns.GridColumn colTrangThai;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lookTrangThai;
        private DevExpress.XtraGrid.Columns.GridColumn colMatBang;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lookMatBang;
        private DevExpress.XtraGrid.Columns.GridColumn colThanhTien;
        private DevExpress.XtraGrid.Columns.GridColumn colVAT;
        private DevExpress.XtraGrid.Columns.GridColumn colTong;
        private DevExpress.XtraBars.BarButtonItem itemExportMau;
    }
}