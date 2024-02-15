namespace ToaNha
{
    partial class frmLoaiGiaThue
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLoaiGiaThue));
            this.gcLoaiGiaThue = new DevExpress.XtraGrid.GridControl();
            this.gvLoaiGiaThue = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.coltenloaigia = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldongia = new DevExpress.XtraGrid.Columns.GridColumn();
            this.spinDonGia = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.colloaitien = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lkLoaiTien = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.barManager1 = new DevExpress.XtraBars.BarManager();
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.itemToaNha = new DevExpress.XtraBars.BarEditItem();
            this.lkToaNha = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.itemNap = new DevExpress.XtraBars.BarButtonItem();
            this.itemThem = new DevExpress.XtraBars.BarButtonItem();
            this.itemXoa = new DevExpress.XtraBars.BarButtonItem();
            this.itemLuu = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection();
            this.lk_TenToaNha = new DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit();
            this.repositoryItemGridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.gcLoaiGiaThue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvLoaiGiaThue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinDonGia)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkLoaiTien)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkToaNha)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lk_TenToaNha)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemGridLookUpEdit1View)).BeginInit();
            this.SuspendLayout();
            // 
            // gcLoaiGiaThue
            // 
            this.gcLoaiGiaThue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcLoaiGiaThue.Location = new System.Drawing.Point(0, 31);
            this.gcLoaiGiaThue.MainView = this.gvLoaiGiaThue;
            this.gcLoaiGiaThue.Name = "gcLoaiGiaThue";
            this.gcLoaiGiaThue.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.lkLoaiTien,
            this.spinDonGia});
            this.gcLoaiGiaThue.Size = new System.Drawing.Size(691, 376);
            this.gcLoaiGiaThue.TabIndex = 0;
            this.gcLoaiGiaThue.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvLoaiGiaThue});
            this.gcLoaiGiaThue.KeyUp += new System.Windows.Forms.KeyEventHandler(this.gcLoaiGiaThue_KeyUp);
            // 
            // gvLoaiGiaThue
            // 
            this.gvLoaiGiaThue.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.coltenloaigia,
            this.coldongia,
            this.colloaitien,
            this.gridColumn6,
            this.gridColumn1});
            this.gvLoaiGiaThue.GridControl = this.gcLoaiGiaThue;
            this.gvLoaiGiaThue.Name = "gvLoaiGiaThue";
            this.gvLoaiGiaThue.OptionsDetail.EnableMasterViewMode = false;
            this.gvLoaiGiaThue.OptionsSelection.MultiSelect = true;
            this.gvLoaiGiaThue.OptionsView.ColumnAutoWidth = false;
            this.gvLoaiGiaThue.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            this.gvLoaiGiaThue.OptionsView.ShowGroupPanel = false;
            this.gvLoaiGiaThue.InitNewRow += new DevExpress.XtraGrid.Views.Grid.InitNewRowEventHandler(this.gvLoaiGiaThue_InitNewRow);
            this.gvLoaiGiaThue.ValidateRow += new DevExpress.XtraGrid.Views.Base.ValidateRowEventHandler(this.gvLoaiGiaThue_ValidateRow);
            // 
            // coltenloaigia
            // 
            this.coltenloaigia.Caption = "Tên Loại Giá";
            this.coltenloaigia.FieldName = "TenLG";
            this.coltenloaigia.Name = "coltenloaigia";
            this.coltenloaigia.Visible = true;
            this.coltenloaigia.VisibleIndex = 0;
            this.coltenloaigia.Width = 118;
            // 
            // coldongia
            // 
            this.coldongia.Caption = "Đơn Giá";
            this.coldongia.ColumnEdit = this.spinDonGia;
            this.coldongia.DisplayFormat.FormatString = "{0:#,0.####}";
            this.coldongia.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.coldongia.FieldName = "DonGia";
            this.coldongia.Name = "coldongia";
            this.coldongia.Visible = true;
            this.coldongia.VisibleIndex = 1;
            this.coldongia.Width = 87;
            // 
            // spinDonGia
            // 
            this.spinDonGia.AutoHeight = false;
            this.spinDonGia.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinDonGia.Name = "spinDonGia";
            // 
            // colloaitien
            // 
            this.colloaitien.Caption = "Loại Tiền";
            this.colloaitien.ColumnEdit = this.lkLoaiTien;
            this.colloaitien.FieldName = "MaLT";
            this.colloaitien.Name = "colloaitien";
            this.colloaitien.Visible = true;
            this.colloaitien.VisibleIndex = 2;
            this.colloaitien.Width = 61;
            // 
            // lkLoaiTien
            // 
            this.lkLoaiTien.AutoHeight = false;
            this.lkLoaiTien.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkLoaiTien.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("KyHieuLT", "Name7")});
            this.lkLoaiTien.DisplayMember = "KyHieuLT";
            this.lkLoaiTien.Name = "lkLoaiTien";
            this.lkLoaiTien.NullText = "";
            this.lkLoaiTien.ShowHeader = false;
            this.lkLoaiTien.ValueMember = "ID";
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "Diễn Giải";
            this.gridColumn6.FieldName = "DienGiai";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 3;
            this.gridColumn6.Width = 331;
            // 
            // gridColumn1
            // 
            this.gridColumn1.FieldName = "MaTN";
            this.gridColumn1.Name = "gridColumn1";
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
            this.itemXoa,
            this.itemToaNha});
            this.barManager1.MaxItemId = 10;
            this.barManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.lk_TenToaNha,
            this.lkToaNha});
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemToaNha, DevExpress.XtraBars.BarItemPaintStyle.Caption),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemNap, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemThem, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemXoa, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemLuu, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.DrawDragBorder = false;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Tools";
            // 
            // itemToaNha
            // 
            this.itemToaNha.Caption = "Dự án:";
            this.itemToaNha.Edit = this.lkToaNha;
            this.itemToaNha.EditWidth = 150;
            this.itemToaNha.Id = 9;
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
            this.lkToaNha.NullText = "";
            this.lkToaNha.ShowHeader = false;
            this.lkToaNha.ValueMember = "MaTN";
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
            // lk_TenToaNha
            // 
            this.lk_TenToaNha.AutoHeight = false;
            this.lk_TenToaNha.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lk_TenToaNha.Name = "lk_TenToaNha";
            this.lk_TenToaNha.NullText = "";
            this.lk_TenToaNha.PopupView = this.repositoryItemGridLookUpEdit1View;
            // 
            // repositoryItemGridLookUpEdit1View
            // 
            this.repositoryItemGridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.repositoryItemGridLookUpEdit1View.Name = "repositoryItemGridLookUpEdit1View";
            this.repositoryItemGridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.repositoryItemGridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // frmLoaiGiaThue
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(691, 407);
            this.Controls.Add(this.gcLoaiGiaThue);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmLoaiGiaThue";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Loại Giá Thuê";
            this.Load += new System.EventHandler(this.frmLoaiGiaThue_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gcLoaiGiaThue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvLoaiGiaThue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinDonGia)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkLoaiTien)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkToaNha)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lk_TenToaNha)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemGridLookUpEdit1View)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gcLoaiGiaThue;
        private DevExpress.XtraGrid.Views.Grid.GridView gvLoaiGiaThue;
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
        private DevExpress.XtraGrid.Columns.GridColumn coltenloaigia;
        private DevExpress.XtraGrid.Columns.GridColumn coldongia;
        private DevExpress.XtraGrid.Columns.GridColumn colloaitien;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lkLoaiTien;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit spinDonGia;
        private DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit lk_TenToaNha;
        private DevExpress.XtraGrid.Views.Grid.GridView repositoryItemGridLookUpEdit1View;
        private DevExpress.XtraBars.BarEditItem itemToaNha;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lkToaNha;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
    }
}