﻿namespace ToaNha
{
    partial class frmLoaiDichVuCoBan
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLoaiDichVuCoBan));
            this.barManager1 = new DevExpress.XtraBars.BarManager();
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.itemNap = new DevExpress.XtraBars.BarButtonItem();
            this.itemThem = new DevExpress.XtraBars.BarButtonItem();
            this.itemXoa = new DevExpress.XtraBars.BarButtonItem();
            this.itemLuu = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection();
            this.gcLoaiDichVu = new DevExpress.XtraGrid.GridControl();
            this.gvLoaiDichVu = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn22 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn32 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcLoaiDichVu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvLoaiDichVu)).BeginInit();
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
            this.itemLuu,
            this.itemNap,
            this.itemThem,
            this.itemXoa});
            this.barManager1.MaxItemId = 10;
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemNap, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemThem, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemXoa, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemLuu, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.DrawDragBorder = false;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Tools";
            // 
            // itemNap
            // 
            this.itemNap.Caption = "Nạp";
            this.itemNap.Id = 3;
            this.itemNap.ImageOptions.ImageIndex = 0;
            this.itemNap.Name = "itemNap";
            this.itemNap.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemNap_ItemClick);
            // 
            // itemThem
            // 
            this.itemThem.Caption = "Thêm";
            this.itemThem.Id = 4;
            this.itemThem.ImageOptions.ImageIndex = 1;
            this.itemThem.Name = "itemThem";
            this.itemThem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemThem_ItemClick);
            // 
            // itemXoa
            // 
            this.itemXoa.Caption = "Xóa";
            this.itemXoa.Id = 5;
            this.itemXoa.ImageOptions.ImageIndex = 2;
            this.itemXoa.Name = "itemXoa";
            this.itemXoa.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemXoa_ItemClick);
            // 
            // itemLuu
            // 
            this.itemLuu.Caption = "Lưu";
            this.itemLuu.Id = 2;
            this.itemLuu.ImageOptions.ImageIndex = 3;
            this.itemLuu.Name = "itemLuu";
            this.itemLuu.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemLuu_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(691, 31);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 407);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(691, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 31);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 376);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(691, 31);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 376);
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.Images.SetKeyName(0, "Refresh1.png");
            this.imageCollection1.Images.SetKeyName(1, "Add1.png");
            this.imageCollection1.Images.SetKeyName(2, "Delete1.png");
            this.imageCollection1.Images.SetKeyName(3, "Save1.png");
            // 
            // gcLoaiDichVu
            // 
            this.gcLoaiDichVu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcLoaiDichVu.Location = new System.Drawing.Point(0, 31);
            this.gcLoaiDichVu.MainView = this.gvLoaiDichVu;
            this.gcLoaiDichVu.Name = "gcLoaiDichVu";
            this.gcLoaiDichVu.ShowOnlyPredefinedDetails = true;
            this.gcLoaiDichVu.Size = new System.Drawing.Size(691, 376);
            this.gcLoaiDichVu.TabIndex = 23;
            this.gcLoaiDichVu.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvLoaiDichVu});
            // 
            // gvLoaiDichVu
            // 
            this.gvLoaiDichVu.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn22,
            this.gridColumn32,
            this.gridColumn1});
            this.gvLoaiDichVu.GridControl = this.gcLoaiDichVu;
            this.gvLoaiDichVu.Name = "gvLoaiDichVu";
            this.gvLoaiDichVu.OptionsView.ColumnAutoWidth = false;
            this.gvLoaiDichVu.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            this.gvLoaiDichVu.OptionsView.ShowGroupPanel = false;
            this.gvLoaiDichVu.InitNewRow += new DevExpress.XtraGrid.Views.Grid.InitNewRowEventHandler(this.gvLoaiDichVu_InitNewRow);
            this.gvLoaiDichVu.ValidateRow += new DevExpress.XtraGrid.Views.Base.ValidateRowEventHandler(this.gvLoaiDichVu_ValidateRow);
            // 
            // gridColumn22
            // 
            this.gridColumn22.Caption = "Tên loại dịch vụ";
            this.gridColumn22.FieldName = "TenLDV";
            this.gridColumn22.Name = "gridColumn22";
            this.gridColumn22.Visible = true;
            this.gridColumn22.VisibleIndex = 0;
            this.gridColumn22.Width = 158;
            // 
            // gridColumn32
            // 
            this.gridColumn32.Caption = "Tên hiển thị";
            this.gridColumn32.FieldName = "TenHienThi";
            this.gridColumn32.Name = "gridColumn32";
            this.gridColumn32.Visible = true;
            this.gridColumn32.VisibleIndex = 1;
            this.gridColumn32.Width = 300;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "gridColumn1";
            this.gridColumn1.FieldName = "ParentID";
            this.gridColumn1.Name = "gridColumn1";
            // 
            // frmLoaiDichVuCoBan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(691, 407);
            this.Controls.Add(this.gcLoaiDichVu);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmLoaiDichVuCoBan";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Loại dịch vụ cơ bản";
            this.Load += new System.EventHandler(this.frmLoaiDichVuCoBan_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcLoaiDichVu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvLoaiDichVu)).EndInit();
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
        private DevExpress.XtraBars.BarButtonItem itemLuu;
        private DevExpress.XtraBars.BarButtonItem itemNap;
        private DevExpress.Utils.ImageCollection imageCollection1;
        private DevExpress.XtraBars.BarButtonItem itemThem;
        private DevExpress.XtraBars.BarButtonItem itemXoa;
        private DevExpress.XtraGrid.GridControl gcLoaiDichVu;
        private DevExpress.XtraGrid.Views.Grid.GridView gvLoaiDichVu;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn22;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn32;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
    }
}