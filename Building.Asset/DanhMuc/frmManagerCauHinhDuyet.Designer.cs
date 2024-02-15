namespace Building.Asset.DanhMuc
{
    partial class frmManagerCauHinhDuyet
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmManagerCauHinhDuyet));
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.gcFormDuyet = new DevExpress.XtraGrid.GridControl();
            this.gvFormDuyet = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.itemToaNha = new DevExpress.XtraBars.BarEditItem();
            this.lkToaNha = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.itemNap = new DevExpress.XtraBars.BarButtonItem();
            this.itemThietLap = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
            this.itemppThem = new DevExpress.XtraBars.BarButtonItem();
            this.itemppXoa = new DevExpress.XtraBars.BarButtonItem();
            this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
            this.gcHeThong = new DevExpress.XtraGrid.GridControl();
            this.gvHeThong = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.popupMenu1 = new DevExpress.XtraBars.PopupMenu(this.components);
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.gcChucVu = new DevExpress.XtraGrid.GridControl();
            this.gvChucVu = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcFormDuyet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvFormDuyet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkToaNha)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            this.xtraTabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcHeThong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvHeThong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.xtraTabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcChucVu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvChucVu)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Horizontal = false;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 31);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.gcFormDuyet);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.xtraTabControl1);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(1072, 465);
            this.splitContainerControl1.SplitterPosition = 262;
            this.splitContainerControl1.TabIndex = 0;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // gcFormDuyet
            // 
            this.gcFormDuyet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcFormDuyet.Location = new System.Drawing.Point(0, 0);
            this.gcFormDuyet.MainView = this.gvFormDuyet;
            this.gcFormDuyet.MenuManager = this.barManager1;
            this.gcFormDuyet.Name = "gcFormDuyet";
            this.gcFormDuyet.ShowOnlyPredefinedDetails = true;
            this.gcFormDuyet.Size = new System.Drawing.Size(1072, 262);
            this.gcFormDuyet.TabIndex = 0;
            this.gcFormDuyet.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvFormDuyet});
            // 
            // gvFormDuyet
            // 
            this.gvFormDuyet.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3});
            this.gvFormDuyet.GridControl = this.gcFormDuyet;
            this.gvFormDuyet.IndicatorWidth = 40;
            this.gvFormDuyet.Name = "gvFormDuyet";
            this.gvFormDuyet.OptionsView.ColumnAutoWidth = false;
            this.gvFormDuyet.OptionsView.ShowAutoFilterRow = true;
            this.gvFormDuyet.OptionsView.ShowGroupPanel = false;
            this.gvFormDuyet.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.gvFormDuyet_CustomDrawRowIndicator);
            this.gvFormDuyet.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gvFormDuyet_FocusedRowChanged);
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "ID";
            this.gridColumn1.FieldName = "ID";
            this.gridColumn1.Name = "gridColumn1";
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Chức năng";
            this.gridColumn2.FieldName = "FormName";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 0;
            this.gridColumn2.Width = 512;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Ghi chú";
            this.gridColumn3.FieldName = "GhiChu";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 1;
            this.gridColumn3.Width = 512;
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
            this.itemToaNha,
            this.itemNap,
            this.itemThietLap,
            this.itemppThem,
            this.itemppXoa});
            this.barManager1.MaxItemId = 7;
            this.barManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.lkToaNha});
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.itemToaNha),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemNap, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemThietLap, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.DrawDragBorder = false;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Tools";
            // 
            // itemToaNha
            // 
            this.itemToaNha.Caption = "Tòa nhà";
            this.itemToaNha.Edit = this.lkToaNha;
            this.itemToaNha.EditWidth = 160;
            this.itemToaNha.Id = 0;
            this.itemToaNha.Name = "itemToaNha";
            this.itemToaNha.EditValueChanged += new System.EventHandler(this.itemToaNha_EditValueChanged);
            // 
            // lkToaNha
            // 
            this.lkToaNha.AutoHeight = false;
            this.lkToaNha.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkToaNha.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenTN", "Name1")});
            this.lkToaNha.DisplayMember = "TenTN";
            this.lkToaNha.Name = "lkToaNha";
            this.lkToaNha.NullText = "[Chọn tòa nhà]";
            this.lkToaNha.ShowFooter = false;
            this.lkToaNha.ShowHeader = false;
            this.lkToaNha.ValueMember = "MaTN";
            // 
            // itemNap
            // 
            this.itemNap.Caption = "Nạp";
            this.itemNap.Id = 1;
            this.itemNap.ImageOptions.ImageIndex = 0;
            this.itemNap.Name = "itemNap";
            // 
            // itemThietLap
            // 
            this.itemThietLap.Caption = "Thiết lập";
            this.itemThietLap.Id = 3;
            this.itemThietLap.ImageOptions.ImageIndex = 1;
            this.itemThietLap.Name = "itemThietLap";
            this.itemThietLap.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemThietLap_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(1072, 31);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 496);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(1072, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 31);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 465);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1072, 31);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 465);
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.Images.SetKeyName(0, "icons8_refresh1.png");
            this.imageCollection1.Images.SetKeyName(1, "icons8_setting3.png");
            this.imageCollection1.Images.SetKeyName(2, "icons8_add4.png");
            this.imageCollection1.Images.SetKeyName(3, "icons8_delete1.png");
            // 
            // itemppThem
            // 
            this.itemppThem.Caption = "Thêm";
            this.itemppThem.Id = 5;
            this.itemppThem.ImageOptions.ImageIndex = 2;
            this.itemppThem.Name = "itemppThem";
            this.itemppThem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemppThem_ItemClick);
            // 
            // itemppXoa
            // 
            this.itemppXoa.Caption = "Xóa";
            this.itemppXoa.Id = 6;
            this.itemppXoa.ImageOptions.ImageIndex = 3;
            this.itemppXoa.Name = "itemppXoa";
            this.itemppXoa.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemppXoa_ItemClick);
            // 
            // xtraTabPage2
            // 
            this.xtraTabPage2.Controls.Add(this.gcHeThong);
            this.xtraTabPage2.Name = "xtraTabPage2";
            this.barManager1.SetPopupContextMenu(this.xtraTabPage2, this.popupMenu1);
            this.xtraTabPage2.Size = new System.Drawing.Size(1066, 170);
            this.xtraTabPage2.Text = "2. Hệ thống duyệt";
            // 
            // gcHeThong
            // 
            this.gcHeThong.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcHeThong.Location = new System.Drawing.Point(0, 0);
            this.gcHeThong.MainView = this.gvHeThong;
            this.gcHeThong.MenuManager = this.barManager1;
            this.gcHeThong.Name = "gcHeThong";
            this.gcHeThong.Size = new System.Drawing.Size(1066, 170);
            this.gcHeThong.TabIndex = 0;
            this.gcHeThong.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvHeThong});
            // 
            // gvHeThong
            // 
            this.gvHeThong.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn8,
            this.gridColumn9});
            this.gvHeThong.GridControl = this.gcHeThong;
            this.gvHeThong.IndicatorWidth = 40;
            this.gvHeThong.Name = "gvHeThong";
            this.gvHeThong.OptionsView.ColumnAutoWidth = false;
            this.gvHeThong.OptionsView.ShowAutoFilterRow = true;
            this.gvHeThong.OptionsView.ShowGroupPanel = false;
            this.gvHeThong.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.gvHeThong_CustomDrawRowIndicator);
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "MaHT";
            this.gridColumn8.FieldName = "MaHT";
            this.gridColumn8.Name = "gridColumn8";
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "Hệ thống";
            this.gridColumn9.FieldName = "TenHT";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 0;
            this.gridColumn9.Width = 554;
            // 
            // popupMenu1
            // 
            this.popupMenu1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.itemppThem),
            new DevExpress.XtraBars.LinkPersistInfo(this.itemppXoa)});
            this.popupMenu1.Manager = this.barManager1;
            this.popupMenu1.Name = "popupMenu1";
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl1.Location = new System.Drawing.Point(0, 0);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.xtraTabPage1;
            this.xtraTabControl1.Size = new System.Drawing.Size(1072, 198);
            this.xtraTabControl1.TabIndex = 0;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1,
            this.xtraTabPage2});
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.gcChucVu);
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(1066, 170);
            this.xtraTabPage1.Text = "1. Chức vụ duyệt";
            // 
            // gcChucVu
            // 
            this.gcChucVu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcChucVu.Location = new System.Drawing.Point(0, 0);
            this.gcChucVu.MainView = this.gvChucVu;
            this.gcChucVu.MenuManager = this.barManager1;
            this.gcChucVu.Name = "gcChucVu";
            this.gcChucVu.Size = new System.Drawing.Size(1066, 170);
            this.gcChucVu.TabIndex = 0;
            this.gcChucVu.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvChucVu});
            // 
            // gvChucVu
            // 
            this.gvChucVu.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn6,
            this.gridColumn7});
            this.gvChucVu.GridControl = this.gcChucVu;
            this.gvChucVu.IndicatorWidth = 40;
            this.gvChucVu.Name = "gvChucVu";
            this.gvChucVu.OptionsView.ColumnAutoWidth = false;
            this.gvChucVu.OptionsView.ShowAutoFilterRow = true;
            this.gvChucVu.OptionsView.ShowGroupPanel = false;
            this.gvChucVu.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.gvChucVu_CustomDrawRowIndicator);
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Chức vụ";
            this.gridColumn4.FieldName = "TenCV";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 0;
            this.gridColumn4.Width = 141;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Nhân viên";
            this.gridColumn5.FieldName = "HoTen";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 1;
            this.gridColumn5.Width = 180;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "Thứ tự duyệt";
            this.gridColumn6.FieldName = "STT";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 2;
            this.gridColumn6.Width = 131;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "Duyệt cuối";
            this.gridColumn7.FieldName = "IsDuyet";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 3;
            this.gridColumn7.Width = 115;
            // 
            // frmManagerCauHinhDuyet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1072, 496);
            this.Controls.Add(this.splitContainerControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "frmManagerCauHinhDuyet";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Quản lý cấu hình duyệt";
            this.Load += new System.EventHandler(this.frmManagerCauHinhDuyet_Load);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcFormDuyet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvFormDuyet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkToaNha)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            this.xtraTabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcHeThong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvHeThong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.xtraTabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcChucVu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvChucVu)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraGrid.GridControl gcFormDuyet;
        private DevExpress.XtraGrid.Views.Grid.GridView gvFormDuyet;
        private DevExpress.XtraBars.BarEditItem itemToaNha;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lkToaNha;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage2;
        private DevExpress.XtraBars.BarButtonItem itemNap;
        private DevExpress.XtraBars.BarButtonItem itemThietLap;
        private DevExpress.Utils.ImageCollection imageCollection1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.GridControl gcChucVu;
        private DevExpress.XtraGrid.Views.Grid.GridView gvChucVu;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.GridControl gcHeThong;
        private DevExpress.XtraGrid.Views.Grid.GridView gvHeThong;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraBars.PopupMenu popupMenu1;
        private DevExpress.XtraBars.BarButtonItem itemppThem;
        private DevExpress.XtraBars.BarButtonItem itemppXoa;
    }
}