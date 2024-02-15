namespace ToaNha
{
    partial class frmLoaiTien
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLoaiTien));
            this.gcTyGia = new DevExpress.XtraGrid.GridControl();
            this.gvTyGia = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colkyhieu = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coltenloaitien = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coltygia = new DevExpress.XtraGrid.Columns.GridColumn();
            this.spinTyGia = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.lookTN = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.lookToaNha = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.dateTuNgay = new DevExpress.XtraEditors.Repository.RepositoryItemTimeEdit();
            this.dateDenNGay = new DevExpress.XtraEditors.Repository.RepositoryItemTimeEdit();
            this.repositoryItemDateEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.repositoryItemDateEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.itemThem = new DevExpress.XtraBars.BarButtonItem();
            this.itemXoa = new DevExpress.XtraBars.BarButtonItem();
            this.itemLuu = new DevExpress.XtraBars.BarButtonItem();
            this.itemNap = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            ((System.ComponentModel.ISupportInitialize)(this.gcTyGia)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvTyGia)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinTyGia)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookTN)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookToaNha)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTuNgay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDenNGay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit2.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            this.SuspendLayout();
            // 
            // gcTyGia
            // 
            this.gcTyGia.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcTyGia.Location = new System.Drawing.Point(0, 31);
            this.gcTyGia.MainView = this.gvTyGia;
            this.gcTyGia.Name = "gcTyGia";
            this.gcTyGia.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.lookTN,
            this.lookToaNha,
            this.spinTyGia,
            this.dateTuNgay,
            this.dateDenNGay,
            this.repositoryItemDateEdit1,
            this.repositoryItemDateEdit2});
            this.gcTyGia.Size = new System.Drawing.Size(454, 237);
            this.gcTyGia.TabIndex = 0;
            this.gcTyGia.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvTyGia});
            // 
            // gvTyGia
            // 
            this.gvTyGia.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colkyhieu,
            this.coltenloaitien,
            this.coltygia});
            this.gvTyGia.GridControl = this.gcTyGia;
            this.gvTyGia.Name = "gvTyGia";
            this.gvTyGia.OptionsDetail.EnableMasterViewMode = false;
            this.gvTyGia.OptionsSelection.MultiSelect = true;
            this.gvTyGia.OptionsView.ColumnAutoWidth = false;
            this.gvTyGia.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            this.gvTyGia.OptionsView.ShowGroupPanel = false;
            this.gvTyGia.ValidateRow += new DevExpress.XtraGrid.Views.Base.ValidateRowEventHandler(this.gvTyGia_ValidateRow);
            this.gvTyGia.KeyUp += new System.Windows.Forms.KeyEventHandler(this.grvNhaCungCap_KeyUp);
            // 
            // colkyhieu
            // 
            this.colkyhieu.AppearanceCell.Options.UseTextOptions = true;
            this.colkyhieu.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colkyhieu.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colkyhieu.Caption = "Ký Hiệu";
            this.colkyhieu.FieldName = "KyHieuLT";
            this.colkyhieu.Name = "colkyhieu";
            this.colkyhieu.Visible = true;
            this.colkyhieu.VisibleIndex = 0;
            // 
            // coltenloaitien
            // 
            this.coltenloaitien.AppearanceHeader.Options.UseTextOptions = true;
            this.coltenloaitien.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.coltenloaitien.Caption = "Tên Loại Tiền";
            this.coltenloaitien.FieldName = "TenLT";
            this.coltenloaitien.Name = "coltenloaitien";
            this.coltenloaitien.Visible = true;
            this.coltenloaitien.VisibleIndex = 1;
            this.coltenloaitien.Width = 187;
            // 
            // coltygia
            // 
            this.coltygia.Caption = "Tỷ giá";
            this.coltygia.ColumnEdit = this.spinTyGia;
            this.coltygia.FieldName = "TyGia";
            this.coltygia.Name = "coltygia";
            this.coltygia.Visible = true;
            this.coltygia.VisibleIndex = 2;
            this.coltygia.Width = 88;
            // 
            // spinTyGia
            // 
            this.spinTyGia.AutoHeight = false;
            this.spinTyGia.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinTyGia.DisplayFormat.FormatString = "{0:#,0.##}";
            this.spinTyGia.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinTyGia.EditFormat.FormatString = "{0:#,0.##}";
            this.spinTyGia.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinTyGia.Name = "spinTyGia";
            // 
            // lookTN
            // 
            this.lookTN.AutoHeight = false;
            this.lookTN.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookTN.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenVT", "Tên viết tắt", 30, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenTN", "Tên đầy đủ", 50, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default)});
            this.lookTN.DisplayMember = "TenVT";
            this.lookTN.Name = "lookTN";
            this.lookTN.NullText = "";
            this.lookTN.ValueMember = "MaTN";
            // 
            // lookToaNha
            // 
            this.lookToaNha.AutoHeight = false;
            this.lookToaNha.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookToaNha.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenTN", "Dự án")});
            this.lookToaNha.DisplayMember = "TenTN";
            this.lookToaNha.Name = "lookToaNha";
            this.lookToaNha.NullText = "";
            this.lookToaNha.ValueMember = "MaTN";
            // 
            // dateTuNgay
            // 
            this.dateTuNgay.AutoHeight = false;
            this.dateTuNgay.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateTuNgay.DisplayFormat.FormatString = "d";
            this.dateTuNgay.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateTuNgay.EditFormat.FormatString = "d";
            this.dateTuNgay.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateTuNgay.Name = "dateTuNgay";
            // 
            // dateDenNGay
            // 
            this.dateDenNGay.AutoHeight = false;
            this.dateDenNGay.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateDenNGay.DisplayFormat.FormatString = "d";
            this.dateDenNGay.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateDenNGay.EditFormat.FormatString = "d";
            this.dateDenNGay.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateDenNGay.Name = "dateDenNGay";
            // 
            // repositoryItemDateEdit1
            // 
            this.repositoryItemDateEdit1.AutoHeight = false;
            this.repositoryItemDateEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemDateEdit1.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.repositoryItemDateEdit1.DisplayFormat.FormatString = "{0:dd/MM/yyyy}";
            this.repositoryItemDateEdit1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.repositoryItemDateEdit1.EditFormat.FormatString = "{0:dd/MM/yyyy}";
            this.repositoryItemDateEdit1.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.repositoryItemDateEdit1.Name = "repositoryItemDateEdit1";
            // 
            // repositoryItemDateEdit2
            // 
            this.repositoryItemDateEdit2.AutoHeight = false;
            this.repositoryItemDateEdit2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemDateEdit2.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.repositoryItemDateEdit2.DisplayFormat.FormatString = "{0:dd/MM/yyyy}";
            this.repositoryItemDateEdit2.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.repositoryItemDateEdit2.EditFormat.FormatString = "{0:dd/MM/yyyy}";
            this.repositoryItemDateEdit2.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.repositoryItemDateEdit2.Name = "repositoryItemDateEdit2";
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
            this.barButtonItem1});
            this.barManager1.MaxItemId = 7;
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemThem, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemXoa, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemLuu, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemNap, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barButtonItem1, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.DrawDragBorder = false;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Tools";
            // 
            // itemThem
            // 
            this.itemThem.Caption = "Thêm";
            this.itemThem.Id = 4;
            this.itemThem.ImageOptions.ImageIndex = 0;
            this.itemThem.Name = "itemThem";
            this.itemThem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemThem_ItemClick);
            // 
            // itemXoa
            // 
            this.itemXoa.Caption = "Xóa";
            this.itemXoa.Id = 5;
            this.itemXoa.ImageOptions.ImageIndex = 1;
            this.itemXoa.Name = "itemXoa";
            this.itemXoa.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemXoa_ItemClick);
            // 
            // itemLuu
            // 
            this.itemLuu.Caption = "Lưu";
            this.itemLuu.Id = 2;
            this.itemLuu.ImageOptions.ImageIndex = 2;
            this.itemLuu.Name = "itemLuu";
            this.itemLuu.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemLuu_ItemClick);
            // 
            // itemNap
            // 
            this.itemNap.Caption = "Nạp";
            this.itemNap.Id = 3;
            this.itemNap.ImageOptions.ImageIndex = 3;
            this.itemNap.Name = "itemNap";
            this.itemNap.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemNap_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(454, 31);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 268);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(454, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 31);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 237);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(454, 31);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 237);
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.Images.SetKeyName(0, "Add1.png");
            this.imageCollection1.Images.SetKeyName(1, "Delete1.png");
            this.imageCollection1.Images.SetKeyName(2, "Save1.png");
            this.imageCollection1.Images.SetKeyName(3, "Refresh1.png");
            this.imageCollection1.Images.SetKeyName(4, "Import1.png");
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "Import";
            this.barButtonItem1.Id = 6;
            this.barButtonItem1.ImageOptions.ImageIndex = 4;
            this.barButtonItem1.Name = "barButtonItem1";
            this.barButtonItem1.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem1_ItemClick);
            // 
            // frmLoaiTien
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(454, 268);
            this.Controls.Add(this.gcTyGia);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmLoaiTien";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Loại tiền";
            this.Load += new System.EventHandler(this.frmLoaiTien_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gcTyGia)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvTyGia)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinTyGia)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookTN)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookToaNha)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTuNgay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDenNGay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit2.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gcTyGia;
        private DevExpress.XtraGrid.Views.Grid.GridView gvTyGia;
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
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lookTN;
        private DevExpress.XtraGrid.Columns.GridColumn coltenloaitien;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lookToaNha;
        private DevExpress.XtraGrid.Columns.GridColumn coltygia;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit spinTyGia;
        private DevExpress.XtraEditors.Repository.RepositoryItemTimeEdit dateTuNgay;
        private DevExpress.XtraEditors.Repository.RepositoryItemTimeEdit dateDenNGay;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit repositoryItemDateEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit repositoryItemDateEdit2;
        private DevExpress.XtraGrid.Columns.GridColumn colkyhieu;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
    }
}