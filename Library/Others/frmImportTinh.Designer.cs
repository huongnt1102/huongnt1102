namespace Library
{
    partial class frmImportTinh
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmImportTinh));
            this.gcTinh = new DevExpress.XtraGrid.GridControl();
            this.gvTinh = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colTenTinh = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTenHienThi = new DevExpress.XtraGrid.Columns.GridColumn();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.itemBrowse = new DevExpress.XtraBars.BarButtonItem();
            this.itemSave = new DevExpress.XtraBars.BarButtonItem();
            this.itemDelete = new DevExpress.XtraBars.BarButtonItem();
            this.itemClose = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControl1 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            ((System.ComponentModel.ISupportInitialize)(this.gcTinh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvTinh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // gcTinh
            // 
            this.gcTinh.Location = new System.Drawing.Point(0, 31);
            this.gcTinh.MainView = this.gvTinh;
            this.gcTinh.Name = "gcTinh";
            this.gcTinh.Size = new System.Drawing.Size(590, 326);
            this.gcTinh.TabIndex = 6;
            this.gcTinh.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvTinh});
            // 
            // gvTinh
            // 
            this.gvTinh.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colTenTinh,
            this.colTenHienThi});
            this.gvTinh.GridControl = this.gcTinh;
            this.gvTinh.Name = "gvTinh";
            this.gvTinh.OptionsSelection.MultiSelect = true;
            this.gvTinh.OptionsView.ShowAutoFilterRow = true;
            this.gvTinh.OptionsView.ShowGroupPanel = false;
            // 
            // colTenTinh
            // 
            this.colTenTinh.Caption = "Tên Tỉnh";
            this.colTenTinh.FieldName = "TenTinh";
            this.colTenTinh.Name = "colTenTinh";
            this.colTenTinh.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.colTenTinh.Visible = true;
            this.colTenTinh.VisibleIndex = 0;
            // 
            // colTenHienThi
            // 
            this.colTenHienThi.Caption = "Tên Hiển Thị";
            this.colTenHienThi.FieldName = "TenHienThi";
            this.colTenHienThi.Name = "colTenHienThi";
            this.colTenHienThi.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.colTenHienThi.Visible = true;
            this.colTenHienThi.VisibleIndex = 1;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 31);
            this.barDockControlTop.Size = new System.Drawing.Size(591, 29);
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.Images.SetKeyName(0, "open.png");
            this.imageCollection1.Images.SetKeyName(1, "Close Square.png");
            this.imageCollection1.Images.SetKeyName(2, "save_16x16.gif");
            this.imageCollection1.Images.SetKeyName(3, "edit-delete.png");
            this.imageCollection1.Images.SetKeyName(4, "delete_2_16px.png");
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1});
            this.barManager1.DockControls.Add(this.barDockControl1);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Images = this.imageCollection1;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.itemBrowse,
            this.itemSave,
            this.itemDelete,
            this.itemClose});
            this.barManager1.MaxItemId = 4;
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemBrowse, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemSave, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemDelete, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemClose, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar1.Text = "Tools";
            // 
            // itemBrowse
            // 
            this.itemBrowse.Caption = "Chọn tập tin";
            this.itemBrowse.Id = 0;
            this.itemBrowse.ImageIndex = 0;
            this.itemBrowse.Name = "itemBrowse";
            this.itemBrowse.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemBrowse_ItemClick);
            // 
            // itemSave
            // 
            this.itemSave.Caption = "Lưu dữ liệu";
            this.itemSave.Id = 1;
            this.itemSave.ImageIndex = 2;
            this.itemSave.Name = "itemSave";
            this.itemSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemSave_ItemClick);
            // 
            // itemDelete
            // 
            this.itemDelete.Caption = "Xóa";
            this.itemDelete.Id = 2;
            this.itemDelete.ImageIndex = 4;
            this.itemDelete.Name = "itemDelete";
            this.itemDelete.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemDelete_ItemClick);
            // 
            // itemClose
            // 
            this.itemClose.Caption = "Đóng";
            this.itemClose.Id = 3;
            this.itemClose.ImageIndex = 1;
            this.itemClose.Name = "itemClose";
            this.itemClose.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemClose_ItemClick);
            // 
            // barDockControl1
            // 
            this.barDockControl1.CausesValidation = false;
            this.barDockControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControl1.Location = new System.Drawing.Point(0, 0);
            this.barDockControl1.Size = new System.Drawing.Size(591, 31);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 359);
            this.barDockControlBottom.Size = new System.Drawing.Size(591, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 31);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 328);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(591, 31);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 328);
            // 
            // frmImportTinh
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(591, 359);
            this.Controls.Add(this.gcTinh);
            this.Controls.Add(this.barDockControlTop);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControl1);
            this.Name = "frmImportTinh";
            this.Text = "Import Tỉnh";
            this.Load += new System.EventHandler(this.frmImportTinh_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gcTinh)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvTinh)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gcTinh;
        private DevExpress.XtraGrid.Views.Grid.GridView gvTinh;
        private DevExpress.XtraGrid.Columns.GridColumn colTenTinh;
        private DevExpress.XtraGrid.Columns.GridColumn colTenHienThi;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.Utils.ImageCollection imageCollection1;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarButtonItem itemBrowse;
        private DevExpress.XtraBars.BarButtonItem itemSave;
        private DevExpress.XtraBars.BarButtonItem itemDelete;
        private DevExpress.XtraBars.BarButtonItem itemClose;
        private DevExpress.XtraBars.BarDockControl barDockControl1;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
    }
}