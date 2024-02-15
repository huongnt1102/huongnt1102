namespace ToaNha
{
    partial class frmLaiSuat
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLaiSuat));
            this.gcLaiSuat = new DevExpress.XtraGrid.GridControl();
            this.gvLaiSuat = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lkToaNha = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lkLoaiDichVu = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lkCachTinh = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lkLoaiLaiSuat = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.spinLaiSuat = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.barManager1 = new DevExpress.XtraBars.BarManager();
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.itemToaNha = new DevExpress.XtraBars.BarEditItem();
            this.lookUpToaNha = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.itemNap = new DevExpress.XtraBars.BarButtonItem();
            this.itemThem = new DevExpress.XtraBars.BarButtonItem();
            this.itemXoa = new DevExpress.XtraBars.BarButtonItem();
            this.itemLuu = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection();
            ((System.ComponentModel.ISupportInitialize)(this.gcLaiSuat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvLaiSuat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkToaNha)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkLoaiDichVu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkCachTinh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkLoaiLaiSuat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinLaiSuat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpToaNha)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            this.SuspendLayout();
            // 
            // gcLaiSuat
            // 
            this.gcLaiSuat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcLaiSuat.Location = new System.Drawing.Point(0, 31);
            this.gcLaiSuat.MainView = this.gvLaiSuat;
            this.gcLaiSuat.Name = "gcLaiSuat";
            this.gcLaiSuat.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.lkToaNha,
            this.lkLoaiDichVu,
            this.lkLoaiLaiSuat,
            this.spinLaiSuat,
            this.lkCachTinh});
            this.gcLaiSuat.Size = new System.Drawing.Size(1023, 388);
            this.gcLaiSuat.TabIndex = 0;
            this.gcLaiSuat.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvLaiSuat});
            this.gcLaiSuat.KeyUp += new System.Windows.Forms.KeyEventHandler(this.gcLaiSuat_KeyUp);
            // 
            // gvLaiSuat
            // 
            this.gvLaiSuat.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn2,
            this.gridColumn1,
            this.gridColumn11,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn12,
            this.gridColumn6,
            this.gridColumn7,
            this.gridColumn8,
            this.gridColumn9,
            this.gridColumn10});
            this.gvLaiSuat.GridControl = this.gcLaiSuat;
            this.gvLaiSuat.Name = "gvLaiSuat";
            this.gvLaiSuat.OptionsDetail.EnableMasterViewMode = false;
            this.gvLaiSuat.OptionsSelection.MultiSelect = true;
            this.gvLaiSuat.OptionsView.ColumnAutoWidth = false;
            this.gvLaiSuat.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            this.gvLaiSuat.OptionsView.ShowGroupPanel = false;
            this.gvLaiSuat.InitNewRow += new DevExpress.XtraGrid.Views.Grid.InitNewRowEventHandler(this.gvLaiSuat_InitNewRow);
            this.gvLaiSuat.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gvLaiSuat_CellValueChanged);
            this.gvLaiSuat.ValidateRow += new DevExpress.XtraGrid.Views.Base.ValidateRowEventHandler(this.gvLaiSuat_ValidateRow);
            this.gvLaiSuat.RowUpdated += new DevExpress.XtraGrid.Views.Base.RowObjectEventHandler(this.gvLaiSuat_RowUpdated);
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Dự án";
            this.gridColumn2.ColumnEdit = this.lkToaNha;
            this.gridColumn2.FieldName = "MaTN";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Width = 97;
            // 
            // lkToaNha
            // 
            this.lkToaNha.AutoHeight = false;
            this.lkToaNha.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkToaNha.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenVT", "Tên viết tắt"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenTN", "Tên đầy đủ", 40, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default)});
            this.lkToaNha.DisplayMember = "TenTN";
            this.lkToaNha.Name = "lkToaNha";
            this.lkToaNha.NullText = "";
            this.lkToaNha.ValueMember = "MaTN";
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Dịch vụ";
            this.gridColumn1.ColumnEdit = this.lkLoaiDichVu;
            this.gridColumn1.FieldName = "MaLDV";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 226;
            // 
            // lkLoaiDichVu
            // 
            this.lkLoaiDichVu.AutoHeight = false;
            this.lkLoaiDichVu.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkLoaiDichVu.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenLDV", "TenLDV", 200, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.Ascending, DevExpress.Utils.DefaultBoolean.Default)});
            this.lkLoaiDichVu.DisplayMember = "TenLDV";
            this.lkLoaiDichVu.DropDownRows = 20;
            this.lkLoaiDichVu.Name = "lkLoaiDichVu";
            this.lkLoaiDichVu.NullText = "";
            this.lkLoaiDichVu.ShowFooter = false;
            this.lkLoaiDichVu.ShowHeader = false;
            this.lkLoaiDichVu.ValueMember = "ID";
            // 
            // gridColumn11
            // 
            this.gridColumn11.Caption = "Cách tính";
            this.gridColumn11.ColumnEdit = this.lkCachTinh;
            this.gridColumn11.FieldName = "MaCT";
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.Visible = true;
            this.gridColumn11.VisibleIndex = 1;
            this.gridColumn11.Width = 161;
            // 
            // lkCachTinh
            // 
            this.lkCachTinh.AutoHeight = false;
            this.lkCachTinh.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkCachTinh.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenCT", "Tên", 30, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("DienGiai", "Diễn giải", 70, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default)});
            this.lkCachTinh.DisplayMember = "TenCT";
            this.lkCachTinh.Name = "lkCachTinh";
            this.lkCachTinh.NullText = "";
            this.lkCachTinh.PopupWidth = 300;
            this.lkCachTinh.ShowLines = false;
            this.lkCachTinh.ValueMember = "MaCT";
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Loại lãi suất";
            this.gridColumn3.ColumnEdit = this.lkLoaiLaiSuat;
            this.gridColumn3.FieldName = "MaLLS";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            this.gridColumn3.Width = 78;
            // 
            // lkLoaiLaiSuat
            // 
            this.lkLoaiLaiSuat.AutoHeight = false;
            this.lkLoaiLaiSuat.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkLoaiLaiSuat.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenLLS", "TenLLS")});
            this.lkLoaiLaiSuat.DisplayMember = "TenLLS";
            this.lkLoaiLaiSuat.Name = "lkLoaiLaiSuat";
            this.lkLoaiLaiSuat.NullText = "";
            this.lkLoaiLaiSuat.ShowHeader = false;
            this.lkLoaiLaiSuat.ValueMember = "ID";
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Lãi tháng (%)";
            this.gridColumn4.ColumnEdit = this.spinLaiSuat;
            this.gridColumn4.FieldName = "LaiThang";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 3;
            this.gridColumn4.Width = 86;
            // 
            // spinLaiSuat
            // 
            this.spinLaiSuat.AutoHeight = false;
            this.spinLaiSuat.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinLaiSuat.DisplayFormat.FormatString = "p2";
            this.spinLaiSuat.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinLaiSuat.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.spinLaiSuat.Mask.EditMask = "p2";
            this.spinLaiSuat.Name = "spinLaiSuat";
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Lãi ngày (%)";
            this.gridColumn5.ColumnEdit = this.spinLaiSuat;
            this.gridColumn5.FieldName = "LaiNgay";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 4;
            this.gridColumn5.Width = 85;
            // 
            // gridColumn12
            // 
            this.gridColumn12.Caption = "Thời hạn (Ngày)";
            this.gridColumn12.FieldName = "SoNgayHH";
            this.gridColumn12.Name = "gridColumn12";
            this.gridColumn12.Visible = true;
            this.gridColumn12.VisibleIndex = 5;
            this.gridColumn12.Width = 95;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "Diễn giải";
            this.gridColumn6.FieldName = "DienGiai";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 6;
            this.gridColumn6.Width = 253;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "NVN";
            this.gridColumn7.FieldName = "MaNVN";
            this.gridColumn7.Name = "gridColumn7";
            // 
            // gridColumn8
            // 
            this.gridColumn8.FieldName = "NgayNhap";
            this.gridColumn8.Name = "gridColumn8";
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "người sửa";
            this.gridColumn9.FieldName = "MaNVS";
            this.gridColumn9.Name = "gridColumn9";
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "Ngày sửa";
            this.gridColumn10.FieldName = "NgaySua";
            this.gridColumn10.Name = "gridColumn10";
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
            this.barManager1.MaxItemId = 7;
            this.barManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.lookUpToaNha});
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.itemToaNha, true),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemNap, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemThem, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemXoa, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemLuu, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.DrawDragBorder = false;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Tools";
            // 
            // itemToaNha
            // 
            this.itemToaNha.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Left;
            this.itemToaNha.Caption = "Dự án";
            this.itemToaNha.Edit = this.lookUpToaNha;
            this.itemToaNha.EditWidth = 131;
            this.itemToaNha.Id = 6;
            this.itemToaNha.Name = "itemToaNha";
            this.itemToaNha.EditValueChanged += new System.EventHandler(this.itemToaNha_EditValueChanged);
            // 
            // lookUpToaNha
            // 
            this.lookUpToaNha.AutoHeight = false;
            this.lookUpToaNha.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpToaNha.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenTN", "Dự án", 200, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default)});
            this.lookUpToaNha.DisplayMember = "TenTN";
            this.lookUpToaNha.DropDownRows = 15;
            this.lookUpToaNha.Name = "lookUpToaNha";
            this.lookUpToaNha.NullText = "Dự án";
            this.lookUpToaNha.ShowFooter = false;
            this.lookUpToaNha.ShowHeader = false;
            this.lookUpToaNha.ValueMember = "MaTN";
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
            this.itemThem.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.itemThem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemThem_ItemClick);
            // 
            // itemXoa
            // 
            this.itemXoa.Caption = "Xóa";
            this.itemXoa.Id = 5;
            this.itemXoa.ImageOptions.ImageIndex = 2;
            this.itemXoa.Name = "itemXoa";
            this.itemXoa.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
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
            this.barDockControlTop.Size = new System.Drawing.Size(1023, 31);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 419);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(1023, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 31);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 388);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1023, 31);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 388);
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.Images.SetKeyName(0, "Refresh1.png");
            this.imageCollection1.Images.SetKeyName(1, "Add1.png");
            this.imageCollection1.Images.SetKeyName(2, "Delete1.png");
            this.imageCollection1.Images.SetKeyName(3, "Save1.png");
            // 
            // frmLaiSuat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1023, 419);
            this.Controls.Add(this.gcLaiSuat);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "frmLaiSuat";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cài đặt lãi suất nộp chậm";
            this.Load += new System.EventHandler(this.frmLoaiMatBang_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gcLaiSuat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvLaiSuat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkToaNha)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkLoaiDichVu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkCachTinh)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkLoaiLaiSuat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinLaiSuat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpToaNha)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gcLaiSuat;
        private DevExpress.XtraGrid.Views.Grid.GridView gvLaiSuat;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
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
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lkToaNha;
        private DevExpress.XtraBars.BarEditItem itemToaNha;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lookUpToaNha;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lkLoaiDichVu;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lkLoaiLaiSuat;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit spinLaiSuat;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lkCachTinh;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn12;
    }
}