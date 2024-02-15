namespace AnNinh
{
    partial class frmKeHoachCuaToi
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmKeHoachCuaToi));
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.barManager1 = new DevExpress.XtraBars.BarManager();
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.itemKyBC = new DevExpress.XtraBars.BarEditItem();
            this.cmbKyBC = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.itemTuNgay = new DevExpress.XtraBars.BarEditItem();
            this.dateTuNgay = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.itemDenNgay = new DevExpress.XtraBars.BarEditItem();
            this.dateDenNgay = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.itemNap = new DevExpress.XtraBars.BarButtonItem();
            this.itemDaHoanThanh = new DevExpress.XtraBars.BarButtonItem();
            this.itemChuaHoanThanh = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection();
            this.itemSua = new DevExpress.XtraBars.BarButtonItem();
            this.btn2Excel = new DevExpress.XtraBars.BarButtonItem();
            this.btnGiayBaoRieng = new DevExpress.XtraBars.BarButtonItem();
            this.btnGiayBaoTongHop = new DevExpress.XtraBars.BarButtonItem();
            this.gcNV = new DevExpress.XtraGrid.GridControl();
            this.grvNV = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colMaNV = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDienGiai = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemMemoExEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIsDone = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbKyBC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTuNgay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTuNgay.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDenNgay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDenNgay.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcNV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvNV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoExEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 1;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.Text = "Tools";
            // 
            // barManager1
            // 
            this.barManager1.AllowCustomization = false;
            this.barManager1.AllowMoveBarOnToolbar = false;
            this.barManager1.AllowQuickCustomization = false;
            this.barManager1.AllowShowToolbarsPopup = false;
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar2});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Images = this.imageCollection1;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.itemDaHoanThanh,
            this.itemSua,
            this.itemChuaHoanThanh,
            this.itemNap,
            this.itemKyBC,
            this.itemTuNgay,
            this.itemDenNgay,
            this.btn2Excel,
            this.btnGiayBaoRieng,
            this.btnGiayBaoTongHop});
            this.barManager1.MaxItemId = 21;
            this.barManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.cmbKyBC,
            this.dateTuNgay,
            this.dateDenNgay});
            // 
            // bar2
            // 
            this.bar2.BarName = "Tools";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.itemKyBC),
            new DevExpress.XtraBars.LinkPersistInfo(this.itemTuNgay),
            new DevExpress.XtraBars.LinkPersistInfo(this.itemDenNgay),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemNap, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemDaHoanThanh, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemChuaHoanThanh, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar2.OptionsBar.AllowQuickCustomization = false;
            this.bar2.OptionsBar.DrawDragBorder = false;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Tools";
            // 
            // itemKyBC
            // 
            this.itemKyBC.Caption = "Kỳ báo cáo";
            this.itemKyBC.Edit = this.cmbKyBC;
            this.itemKyBC.EditWidth = 80;
            this.itemKyBC.Id = 4;
            this.itemKyBC.Name = "itemKyBC";
            // 
            // cmbKyBC
            // 
            this.cmbKyBC.AutoHeight = false;
            this.cmbKyBC.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbKyBC.Name = "cmbKyBC";
            this.cmbKyBC.NullText = "Kỳ báo cáo";
            this.cmbKyBC.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbKyBC.EditValueChanged += new System.EventHandler(this.cmbKyBC_EditValueChanged);
            // 
            // itemTuNgay
            // 
            this.itemTuNgay.Caption = "Từ ngày";
            this.itemTuNgay.Edit = this.dateTuNgay;
            this.itemTuNgay.EditWidth = 80;
            this.itemTuNgay.Id = 5;
            this.itemTuNgay.Name = "itemTuNgay";
            this.itemTuNgay.EditValueChanged += new System.EventHandler(this.itemTuNgay_EditValueChanged);
            // 
            // dateTuNgay
            // 
            this.dateTuNgay.AutoHeight = false;
            this.dateTuNgay.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateTuNgay.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateTuNgay.Name = "dateTuNgay";
            this.dateTuNgay.NullText = "Từ ngày";
            // 
            // itemDenNgay
            // 
            this.itemDenNgay.Caption = "Đến ngày";
            this.itemDenNgay.Edit = this.dateDenNgay;
            this.itemDenNgay.EditWidth = 80;
            this.itemDenNgay.Id = 7;
            this.itemDenNgay.Name = "itemDenNgay";
            this.itemDenNgay.EditValueChanged += new System.EventHandler(this.itemDenNgay_EditValueChanged);
            // 
            // dateDenNgay
            // 
            this.dateDenNgay.AutoHeight = false;
            this.dateDenNgay.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateDenNgay.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateDenNgay.Name = "dateDenNgay";
            this.dateDenNgay.NullText = "Đến ngày";
            // 
            // itemNap
            // 
            this.itemNap.Caption = "Nạp";
            this.itemNap.Id = 3;
            this.itemNap.ImageOptions.ImageIndex = 15;
            this.itemNap.Name = "itemNap";
            this.itemNap.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemNap_ItemClick);
            // 
            // itemDaHoanThanh
            // 
            this.itemDaHoanThanh.Caption = "Đã hoàn thành";
            this.itemDaHoanThanh.Id = 0;
            this.itemDaHoanThanh.ImageOptions.ImageIndex = 16;
            this.itemDaHoanThanh.Name = "itemDaHoanThanh";
            this.itemDaHoanThanh.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemDaHoanThanh_ItemClick);
            // 
            // itemChuaHoanThanh
            // 
            this.itemChuaHoanThanh.Caption = "Chưa hoàn thành";
            this.itemChuaHoanThanh.Id = 2;
            this.itemChuaHoanThanh.ImageOptions.ImageIndex = 17;
            this.itemChuaHoanThanh.Name = "itemChuaHoanThanh";
            this.itemChuaHoanThanh.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemChuaHoanThanh_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(909, 31);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 429);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(909, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 31);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 398);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(909, 31);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 398);
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.Images.SetKeyName(0, "001.gif");
            this.imageCollection1.Images.SetKeyName(1, "004.gif");
            this.imageCollection1.Images.SetKeyName(2, "095.gif");
            this.imageCollection1.Images.SetKeyName(3, "108.gif");
            this.imageCollection1.Images.SetKeyName(4, "039.gif");
            this.imageCollection1.Images.SetKeyName(5, "1334905686_check.png");
            this.imageCollection1.Images.SetKeyName(6, "1336097347_agt_print.png");
            this.imageCollection1.Images.SetKeyName(7, "phone.png");
            this.imageCollection1.Images.SetKeyName(8, "1339140059_email.png");
            this.imageCollection1.Images.SetKeyName(9, "excel16.png");
            this.imageCollection1.Images.SetKeyName(10, "export [].png");
            this.imageCollection1.Images.SetKeyName(11, "1337494333_document [].png");
            this.imageCollection1.Images.SetKeyName(12, "Purchase32 [].png");
            this.imageCollection1.Images.SetKeyName(13, "dialog-apply.png");
            this.imageCollection1.Images.SetKeyName(14, "icons8_connection_sync_filled_50px.png");
            this.imageCollection1.Images.SetKeyName(15, "icons8_available_updates_filled_50px_1.png");
            this.imageCollection1.Images.SetKeyName(16, "icons8_checkmark_filled_50px.png");
            this.imageCollection1.Images.SetKeyName(17, "icons8_multiply_filled_50px.png");
            // 
            // itemSua
            // 
            this.itemSua.Caption = "Sửa";
            this.itemSua.Id = 1;
            this.itemSua.ImageOptions.ImageIndex = 2;
            this.itemSua.Name = "itemSua";
            // 
            // btn2Excel
            // 
            this.btn2Excel.Caption = "Excel";
            this.btn2Excel.Id = 13;
            this.btn2Excel.ImageOptions.ImageIndex = 9;
            this.btn2Excel.Name = "btn2Excel";
            // 
            // btnGiayBaoRieng
            // 
            this.btnGiayBaoRieng.Caption = "Giấy báo riêng";
            this.btnGiayBaoRieng.Id = 18;
            this.btnGiayBaoRieng.ImageOptions.ImageIndex = 11;
            this.btnGiayBaoRieng.Name = "btnGiayBaoRieng";
            // 
            // btnGiayBaoTongHop
            // 
            this.btnGiayBaoTongHop.Caption = "Giấy báo tổng hợp";
            this.btnGiayBaoTongHop.Id = 19;
            this.btnGiayBaoTongHop.ImageOptions.ImageIndex = 11;
            this.btnGiayBaoTongHop.Name = "btnGiayBaoTongHop";
            // 
            // gcNV
            // 
            this.gcNV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcNV.Location = new System.Drawing.Point(0, 31);
            this.gcNV.MainView = this.grvNV;
            this.gcNV.MenuManager = this.barManager1;
            this.gcNV.Name = "gcNV";
            this.gcNV.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemMemoExEdit1,
            this.repositoryItemCheckEdit1});
            this.gcNV.Size = new System.Drawing.Size(909, 398);
            this.gcNV.TabIndex = 4;
            this.gcNV.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvNV});
            // 
            // grvNV
            // 
            this.grvNV.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colMaNV,
            this.colDienGiai,
            this.gridColumn1,
            this.colIsDone,
            this.gridColumn2});
            this.grvNV.GridControl = this.gcNV;
            this.grvNV.Name = "grvNV";
            // 
            // colMaNV
            // 
            this.colMaNV.Caption = "MaNV";
            this.colMaNV.FieldName = "MaNhiemVu";
            this.colMaNV.Name = "colMaNV";
            // 
            // colDienGiai
            // 
            this.colDienGiai.Caption = "Diễn giải";
            this.colDienGiai.ColumnEdit = this.repositoryItemMemoExEdit1;
            this.colDienGiai.FieldName = "DienGiai";
            this.colDienGiai.Name = "colDienGiai";
            this.colDienGiai.Visible = true;
            this.colDienGiai.VisibleIndex = 1;
            this.colDienGiai.Width = 180;
            // 
            // repositoryItemMemoExEdit1
            // 
            this.repositoryItemMemoExEdit1.AutoHeight = false;
            this.repositoryItemMemoExEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemMemoExEdit1.Name = "repositoryItemMemoExEdit1";
            this.repositoryItemMemoExEdit1.PopupFormMinSize = new System.Drawing.Size(400, 200);
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Diễn giải (thu gọn)";
            this.gridColumn1.FieldName = "DienGiaiRG";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 593;
            // 
            // colIsDone
            // 
            this.colIsDone.Caption = "Đã thực hiện";
            this.colIsDone.ColumnEdit = this.repositoryItemCheckEdit1;
            this.colIsDone.FieldName = "DaThucHien";
            this.colIsDone.Name = "colIsDone";
            this.colIsDone.Visible = true;
            this.colIsDone.VisibleIndex = 2;
            this.colIsDone.Width = 101;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Ghi chú";
            this.gridColumn2.FieldName = "DienGiai";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 3;
            this.gridColumn2.Width = 306;
            // 
            // frmKeHoachCuaToi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(909, 429);
            this.Controls.Add(this.gcNV);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "frmKeHoachCuaToi";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Nhiệm vụ của tôi";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmShowKeHoach_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbKyBC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTuNgay.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTuNgay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDenNgay.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDenNgay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcNV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvNV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoExEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarEditItem itemKyBC;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox cmbKyBC;
        private DevExpress.XtraBars.BarEditItem itemTuNgay;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit dateTuNgay;
        private DevExpress.XtraBars.BarEditItem itemDenNgay;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit dateDenNgay;
        private DevExpress.XtraBars.BarButtonItem itemNap;
        private DevExpress.XtraBars.BarButtonItem itemDaHoanThanh;
        private DevExpress.XtraBars.BarButtonItem itemChuaHoanThanh;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.Utils.ImageCollection imageCollection1;
        private DevExpress.XtraBars.BarButtonItem itemSua;
        private DevExpress.XtraBars.BarButtonItem btn2Excel;
        private DevExpress.XtraBars.BarButtonItem btnGiayBaoRieng;
        private DevExpress.XtraBars.BarButtonItem btnGiayBaoTongHop;
        private DevExpress.XtraGrid.GridControl gcNV;
        private DevExpress.XtraGrid.Views.Grid.GridView grvNV;
        private DevExpress.XtraGrid.Columns.GridColumn colMaNV;
        private DevExpress.XtraGrid.Columns.GridColumn colDienGiai;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit repositoryItemMemoExEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn colIsDone;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;

    }
}