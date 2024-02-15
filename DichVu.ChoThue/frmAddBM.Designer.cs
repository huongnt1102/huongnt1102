namespace DichVu.ChoThue
{
    partial class frmAddBM
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAddBM));
            this.gcBieuMau = new DevExpress.XtraGrid.GridControl();
            this.grvBieuMau = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colMaBm = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTenBM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDes = new DevExpress.XtraGrid.Columns.GridColumn();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.btnChon = new DevExpress.XtraBars.BarButtonItem();
            this.btnHuy = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.gcBieuMau)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvBieuMau)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            this.SuspendLayout();
            // 
            // gcBieuMau
            // 
            this.gcBieuMau.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcBieuMau.Location = new System.Drawing.Point(0, 31);
            this.gcBieuMau.MainView = this.grvBieuMau;
            this.gcBieuMau.Name = "gcBieuMau";
            this.gcBieuMau.Size = new System.Drawing.Size(655, 320);
            this.gcBieuMau.TabIndex = 0;
            this.gcBieuMau.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvBieuMau});
            // 
            // grvBieuMau
            // 
            this.grvBieuMau.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colMaBm,
            this.colTenBM,
            this.colDes});
            this.grvBieuMau.GridControl = this.gcBieuMau;
            this.grvBieuMau.Name = "grvBieuMau";
            this.grvBieuMau.OptionsBehavior.Editable = false;
            this.grvBieuMau.OptionsCustomization.AllowGroup = false;
            this.grvBieuMau.OptionsView.ShowGroupPanel = false;
            this.grvBieuMau.DoubleClick += new System.EventHandler(this.grvBieuMau_DoubleClick);
            // 
            // colMaBm
            // 
            this.colMaBm.Caption = "MaBM";
            this.colMaBm.FieldName = "MaBM";
            this.colMaBm.Name = "colMaBm";
            // 
            // colTenBM
            // 
            this.colTenBM.Caption = "Tên biểu mẫu";
            this.colTenBM.FieldName = "TenBM";
            this.colTenBM.Name = "colTenBM";
            this.colTenBM.OptionsColumn.AllowEdit = false;
            this.colTenBM.Visible = true;
            this.colTenBM.VisibleIndex = 0;
            this.colTenBM.Width = 352;
            // 
            // colDes
            // 
            this.colDes.Caption = "Diễn giải";
            this.colDes.FieldName = "Description";
            this.colDes.Name = "colDes";
            this.colDes.OptionsColumn.AllowEdit = false;
            this.colDes.Visible = true;
            this.colDes.VisibleIndex = 1;
            this.colDes.Width = 359;
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
            this.btnChon,
            this.btnHuy});
            this.barManager1.MaxItemId = 2;
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnChon, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnHuy, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.DisableCustomization = true;
            this.bar1.OptionsBar.DrawDragBorder = false;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Tools";
            // 
            // btnChon
            // 
            this.btnChon.Caption = "Chọn thêm";
            this.btnChon.Id = 0;
            this.btnChon.ImageOptions.ImageIndex = 0;
            this.btnChon.Name = "btnChon";
            this.btnChon.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnChon_ItemClick);
            // 
            // btnHuy
            // 
            this.btnHuy.Caption = "Hủy / thoát";
            this.btnHuy.Id = 1;
            this.btnHuy.ImageOptions.ImageIndex = 1;
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnHuy_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(655, 31);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 351);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(655, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 31);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 320);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(655, 31);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 320);
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.Images.SetKeyName(0, "icons8_add4.png");
            this.imageCollection1.Images.SetKeyName(1, "icons8_cancel1.png");
            // 
            // frmAddBM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(655, 351);
            this.Controls.Add(this.gcBieuMau);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "frmAddBM";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Biểu mẫu";
            this.Load += new System.EventHandler(this.frmAddBM_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gcBieuMau)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvBieuMau)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gcBieuMau;
        private DevExpress.XtraGrid.Views.Grid.GridView grvBieuMau;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarButtonItem btnChon;
        private DevExpress.XtraBars.BarButtonItem btnHuy;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.Utils.ImageCollection imageCollection1;
        private DevExpress.XtraGrid.Columns.GridColumn colMaBm;
        private DevExpress.XtraGrid.Columns.GridColumn colTenBM;
        private DevExpress.XtraGrid.Columns.GridColumn colDes;
    }
}