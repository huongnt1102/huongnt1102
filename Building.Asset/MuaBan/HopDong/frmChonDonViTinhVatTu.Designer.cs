namespace TaiSan.XuatKho
{
    partial class frmChonDonViTinhVatTu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmChonDonViTinhVatTu));
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.itemSave = new DevExpress.XtraBars.BarButtonItem();
            this.itemCancel = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
            this.barButtonItem4 = new DevExpress.XtraBars.BarButtonItem();
            this.btnThanhLy = new DevExpress.XtraBars.BarButtonItem();
            this.btnChuyenKho = new DevExpress.XtraBars.BarButtonItem();
            this.itemThanhToan = new DevExpress.XtraBars.BarButtonItem();
            this.cbxKBC = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.lkToaNha = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.groupThuTien = new DevExpress.XtraEditors.GroupControl();
            this.gc = new DevExpress.XtraGrid.GridControl();
            this.gv = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.itemThemDonViQuyDoi = new DevExpress.XtraBars.BarButtonItem();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxKBC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkToaNha)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupThuTien)).BeginInit();
            this.groupThuTien.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv)).BeginInit();
            this.SuspendLayout();
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
            this.barButtonItem4,
            this.btnThanhLy,
            this.btnChuyenKho,
            this.itemThanhToan,
            this.itemSave,
            this.itemCancel,
            this.itemThemDonViQuyDoi});
            this.barManager1.MaxItemId = 23;
            this.barManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.cbxKBC,
            this.lkToaNha});
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemSave, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemCancel, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemThemDonViQuyDoi, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.DrawDragBorder = false;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Tools";
            // 
            // itemSave
            // 
            this.itemSave.Caption = "Chọn";
            this.itemSave.Glyph = global::TaiSan.Properties.Resources.OK;
            this.itemSave.Id = 20;
            this.itemSave.Name = "itemSave";
            this.itemSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemSave_ItemClick);
            // 
            // itemCancel
            // 
            this.itemCancel.Caption = "Hủy";
            this.itemCancel.Glyph = global::TaiSan.Properties.Resources.cancel;
            this.itemCancel.Id = 21;
            this.itemCancel.Name = "itemCancel";
            this.itemCancel.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemCancel_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(319, 31);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 300);
            this.barDockControlBottom.Size = new System.Drawing.Size(319, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 31);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 269);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(319, 31);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 269);
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.Images.SetKeyName(0, "add_outline.png");
            this.imageCollection1.Images.SetKeyName(1, "edit-delete.png");
            this.imageCollection1.Images.SetKeyName(2, "edit.png");
            this.imageCollection1.Images.SetKeyName(3, "108.gif");
            this.imageCollection1.Images.SetKeyName(4, "print.png");
            this.imageCollection1.Images.SetKeyName(5, "1337494333_document.png");
            this.imageCollection1.Images.SetKeyName(6, "check.png");
            // 
            // barButtonItem4
            // 
            this.barButtonItem4.Caption = "Chuyển kho nội bộ";
            this.barButtonItem4.Id = 14;
            this.barButtonItem4.ImageIndex = 6;
            this.barButtonItem4.Name = "barButtonItem4";
            // 
            // btnThanhLy
            // 
            this.btnThanhLy.Caption = "Thanh lý";
            this.btnThanhLy.Id = 15;
            this.btnThanhLy.ImageIndex = 6;
            this.btnThanhLy.Name = "btnThanhLy";
            // 
            // btnChuyenKho
            // 
            this.btnChuyenKho.Caption = "Chuyển kho nội bộ";
            this.btnChuyenKho.Id = 16;
            this.btnChuyenKho.ImageIndex = 6;
            this.btnChuyenKho.Name = "btnChuyenKho";
            // 
            // itemThanhToan
            // 
            this.itemThanhToan.Caption = "Thanh toán";
            this.itemThanhToan.Glyph = global::TaiSan.Properties.Resources.ThanToan;
            this.itemThanhToan.Id = 19;
            this.itemThanhToan.Name = "itemThanhToan";
            // 
            // cbxKBC
            // 
            this.cbxKBC.AutoHeight = false;
            this.cbxKBC.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbxKBC.Name = "cbxKBC";
            this.cbxKBC.NullText = "Kỳ báo cáo";
            this.cbxKBC.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            // 
            // lkToaNha
            // 
            this.lkToaNha.AutoHeight = false;
            this.lkToaNha.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkToaNha.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenTN", "Tên Tòa Nhà")});
            this.lkToaNha.DisplayMember = "TenTN";
            this.lkToaNha.DropDownRows = 15;
            this.lkToaNha.Name = "lkToaNha";
            this.lkToaNha.NullText = "[Chọn Tòa Nhà]";
            this.lkToaNha.ValueMember = "MaTN";
            // 
            // groupThuTien
            // 
            this.groupThuTien.Controls.Add(this.gc);
            this.groupThuTien.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupThuTien.Location = new System.Drawing.Point(0, 31);
            this.groupThuTien.Name = "groupThuTien";
            this.groupThuTien.Size = new System.Drawing.Size(319, 269);
            this.groupThuTien.TabIndex = 0;
            // 
            // gc
            // 
            this.gc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gc.Location = new System.Drawing.Point(2, 21);
            this.gc.MainView = this.gv;
            this.gc.Name = "gc";
            this.gc.Size = new System.Drawing.Size(315, 246);
            this.gc.TabIndex = 1;
            this.gc.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gv});
            // 
            // gv
            // 
            this.gv.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1});
            this.gv.GridControl = this.gc;
            this.gv.Name = "gv";
            this.gv.OptionsDetail.EnableMasterViewMode = false;
            this.gv.OptionsView.ShowGroupPanel = false;
            this.gv.DoubleClick += new System.EventHandler(this.gv_DoubleClick);
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Đơn vị tính";
            this.gridColumn1.FieldName = "TenDVT";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.OptionsColumn.ReadOnly = true;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 362;
            // 
            // itemThemDonViQuyDoi
            // 
            this.itemThemDonViQuyDoi.Caption = "Thêm đơn vị quy đổi";
            this.itemThemDonViQuyDoi.Id = 22;
            this.itemThemDonViQuyDoi.ImageIndex = 0;
            this.itemThemDonViQuyDoi.Name = "itemThemDonViQuyDoi";
            this.itemThemDonViQuyDoi.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemThemDonViQuyDoi_ItemClick);
            // 
            // frmChonDonViTinhVatTu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(319, 300);
            this.Controls.Add(this.groupThuTien);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "frmChonDonViTinhVatTu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chọn đơn vị tính";
            this.Load += new System.EventHandler(this.frmQuanLyHopDong_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxKBC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkToaNha)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupThuTien)).EndInit();
            this.groupThuTien.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox cbxKBC;
        private DevExpress.Utils.ImageCollection imageCollection1;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lkToaNha;
        private DevExpress.XtraBars.BarButtonItem btnChuyenKho;
        private DevExpress.XtraBars.BarButtonItem btnThanhLy;
        private DevExpress.XtraBars.BarButtonItem barButtonItem4;
        private DevExpress.XtraBars.BarButtonItem itemThanhToan;
        private DevExpress.XtraBars.BarButtonItem itemSave;
        private DevExpress.XtraBars.BarButtonItem itemCancel;
        private DevExpress.XtraEditors.GroupControl groupThuTien;
        private DevExpress.XtraGrid.GridControl gc;
        private DevExpress.XtraGrid.Views.Grid.GridView gv;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraBars.BarButtonItem itemThemDonViQuyDoi;
    }
}