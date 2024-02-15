namespace TaiSan.KhoHang
{
    partial class frmKhoManage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmKhoManage));
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.btnNap = new DevExpress.XtraBars.BarButtonItem();
            this.btnNhapKho = new DevExpress.XtraBars.BarButtonItem();
            this.btnXuatKho = new DevExpress.XtraBars.BarButtonItem();
            this.itemChuyenNoiBo = new DevExpress.XtraBars.BarButtonItem();
            this.btnXoa = new DevExpress.XtraBars.BarButtonItem();
            this.btnThangThai = new DevExpress.XtraBars.BarButtonItem();
            this.btnDuaTSSD = new DevExpress.XtraBars.BarButtonItem();
            this.btnImport = new DevExpress.XtraBars.BarButtonItem();
            this.btnExport = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
            this.gcKho = new DevExpress.XtraGrid.GridControl();
            this.grvKho = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcKho)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvKho)).BeginInit();
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
            this.btnNhapKho,
            this.btnXuatKho,
            this.btnThangThai,
            this.btnNap,
            this.btnDuaTSSD,
            this.btnImport,
            this.btnXoa,
            this.btnExport,
            this.itemChuyenNoiBo});
            this.barManager1.MaxItemId = 11;
            // 
            // bar1
            // 
            this.bar1.BarName = "Custom 4";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnNap, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnNhapKho, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnXuatKho, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemChuyenNoiBo, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnXoa, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnThangThai, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnDuaTSSD, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnImport, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnExport, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.DrawDragBorder = false;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Custom 4";
            // 
            // btnNap
            // 
            this.btnNap.Caption = "Nạp";
            this.btnNap.Id = 3;
            this.btnNap.ImageIndex = 8;
            this.btnNap.Name = "btnNap";
            this.btnNap.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnNap_ItemClick);
            // 
            // btnNhapKho
            // 
            this.btnNhapKho.Caption = "Nhập kho";
            this.btnNhapKho.Id = 0;
            this.btnNhapKho.ImageIndex = 5;
            this.btnNhapKho.Name = "btnNhapKho";
            this.btnNhapKho.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnNhapKho_ItemClick);
            // 
            // btnXuatKho
            // 
            this.btnXuatKho.Caption = "Xuất kho";
            this.btnXuatKho.Id = 1;
            this.btnXuatKho.ImageIndex = 4;
            this.btnXuatKho.Name = "btnXuatKho";
            this.btnXuatKho.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnXuatKho_ItemClick);
            // 
            // itemChuyenNoiBo
            // 
            this.itemChuyenNoiBo.Caption = "Chuyển kho nội bộ";
            this.itemChuyenNoiBo.Id = 10;
            this.itemChuyenNoiBo.ImageIndex = 11;
            this.itemChuyenNoiBo.Name = "itemChuyenNoiBo";
            this.itemChuyenNoiBo.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemChuyenNoiBo_ItemClick);
            // 
            // btnXoa
            // 
            this.btnXoa.Caption = "Xóa";
            this.btnXoa.Id = 8;
            this.btnXoa.ImageIndex = 7;
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnXoa_ItemClick);
            // 
            // btnThangThai
            // 
            this.btnThangThai.Caption = "Trạng thái";
            this.btnThangThai.Id = 2;
            this.btnThangThai.ImageIndex = 1;
            this.btnThangThai.Name = "btnThangThai";
            this.btnThangThai.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnThangThai_ItemClick);
            // 
            // btnDuaTSSD
            // 
            this.btnDuaTSSD.Caption = "Đưa vào sử dụng";
            this.btnDuaTSSD.Id = 6;
            this.btnDuaTSSD.ImageIndex = 3;
            this.btnDuaTSSD.Name = "btnDuaTSSD";
            this.btnDuaTSSD.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnChuyenDenTaiSanSuDung_ItemClick);
            // 
            // btnImport
            // 
            this.btnImport.Caption = "Import tài sản";
            this.btnImport.Id = 7;
            this.btnImport.ImageIndex = 10;
            this.btnImport.Name = "btnImport";
            this.btnImport.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnImport_ItemClick);
            // 
            // btnExport
            // 
            this.btnExport.Caption = "Export tài sản";
            this.btnExport.Id = 9;
            this.btnExport.ImageIndex = 9;
            this.btnExport.Name = "btnExport";
            this.btnExport.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnExport_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(998, 31);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 589);
            this.barDockControlBottom.Size = new System.Drawing.Size(998, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 31);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 558);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(998, 31);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 558);
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.Images.SetKeyName(0, "1337494333_document.png");
            this.imageCollection1.Images.SetKeyName(1, "1337494441_Status.png");
            this.imageCollection1.Images.SetKeyName(2, "1334904798_gtk-refresh.png");
            this.imageCollection1.Images.SetKeyName(3, "ShoppingCart16.png");
            this.imageCollection1.Images.SetKeyName(4, "stattus32.png");
            this.imageCollection1.Images.SetKeyName(5, "supplier16.png");
            this.imageCollection1.Images.SetKeyName(6, "excel16.png");
            this.imageCollection1.Images.SetKeyName(7, "Delete64 [].png");
            this.imageCollection1.Images.SetKeyName(8, "refresh-orange.png");
            this.imageCollection1.Images.SetKeyName(9, "table-export.png");
            this.imageCollection1.Images.SetKeyName(10, "table-import.png");
            this.imageCollection1.Images.SetKeyName(11, "peer-to-peer-icon.png");
            // 
            // gcKho
            // 
            this.gcKho.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcKho.Location = new System.Drawing.Point(0, 31);
            this.gcKho.MainView = this.grvKho;
            this.gcKho.MenuManager = this.barManager1;
            this.gcKho.Name = "gcKho";
            this.gcKho.Size = new System.Drawing.Size(998, 558);
            this.gcKho.TabIndex = 4;
            this.gcKho.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvKho});
            // 
            // grvKho
            // 
            this.grvKho.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn6,
            this.gridColumn7,
            this.gridColumn8,
            this.gridColumn9,
            this.gridColumn10,
            this.gridColumn11});
            this.grvKho.GridControl = this.gcKho;
            this.grvKho.Name = "grvKho";
            this.grvKho.OptionsView.ColumnAutoWidth = false;
            this.grvKho.OptionsView.ShowAutoFilterRow = true;
            this.grvKho.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.gridColumn8, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.grvKho.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.grvKho_RowStyle);
            this.grvKho.DoubleClick += new System.EventHandler(this.grvKho_DoubleClick);
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Tên tài sản";
            this.gridColumn1.FieldName = "TenLTS";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 2;
            this.gridColumn1.Width = 190;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Trạng thái";
            this.gridColumn2.FieldName = "TenTT";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 6;
            this.gridColumn2.Width = 157;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Số lượng";
            this.gridColumn3.FieldName = "SoLuong";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 7;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Đơn giá";
            this.gridColumn4.DisplayFormat.FormatString = "{0:#,0.##}";
            this.gridColumn4.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn4.FieldName = "DonGia";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 8;
            this.gridColumn4.Width = 121;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Ngày nhập";
            this.gridColumn5.FieldName = "NgayNhap";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 0;
            this.gridColumn5.Width = 92;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "Mã nhập kho";
            this.gridColumn6.FieldName = "ID";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.OptionsColumn.AllowEdit = false;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "Màu nền";
            this.gridColumn7.FieldName = "MauNen";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.OptionsColumn.AllowEdit = false;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "HSD (tháng)";
            this.gridColumn8.DisplayFormat.FormatString = "{0:#,0.#} Tháng";
            this.gridColumn8.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn8.FieldName = "HanSuDung";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 4;
            this.gridColumn8.Width = 84;
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "Kho hàng";
            this.gridColumn9.FieldName = "TenKho";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 5;
            this.gridColumn9.Width = 213;
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "Nhà cung cấp";
            this.gridColumn10.FieldName = "TenNCC";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 3;
            this.gridColumn10.Width = 93;
            // 
            // gridColumn11
            // 
            this.gridColumn11.Caption = "Nhóm tài sản";
            this.gridColumn11.FieldName = "NhomTS";
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.Visible = true;
            this.gridColumn11.VisibleIndex = 1;
            this.gridColumn11.Width = 118;
            // 
            // frmKhoManage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(998, 589);
            this.Controls.Add(this.gcKho);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "frmKhoManage";
            this.ShowInTaskbar = false;
            this.Text = "Quản lý kho";
            this.Load += new System.EventHandler(this.frmKhoManage_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcKho)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvKho)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraGrid.GridControl gcKho;
        private DevExpress.XtraGrid.Views.Grid.GridView grvKho;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraBars.BarButtonItem btnNhapKho;
        private DevExpress.XtraBars.BarButtonItem btnXuatKho;
        private DevExpress.XtraBars.BarButtonItem btnThangThai;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.Utils.ImageCollection imageCollection1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarButtonItem btnNap;
        private DevExpress.XtraBars.BarButtonItem btnDuaTSSD;
        private DevExpress.XtraBars.BarButtonItem btnImport;
        private DevExpress.XtraBars.BarButtonItem btnXoa;
        private DevExpress.XtraBars.BarButtonItem btnExport;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraBars.BarButtonItem itemChuyenNoiBo;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
    }
}