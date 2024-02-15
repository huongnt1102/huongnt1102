namespace DichVu.Dien
{
    partial class frmDinhMuc
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDinhMuc));
            this.gcDinhMuc = new DevExpress.XtraGrid.GridControl();
            this.gvDinhMuc = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTenDM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSTT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.spSTT = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.colDinhMuc = new DevExpress.XtraGrid.Columns.GridColumn();
            this.spDinhMuc = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.colDonGiaCH = new DevExpress.XtraGrid.Columns.GridColumn();
            this.spDonGiaCH = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.colDienGiai = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.itemToaNha = new DevExpress.XtraBars.BarEditItem();
            this.lkToaNha = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.itemLoaiMatBang = new DevExpress.XtraBars.BarEditItem();
            this.lkLoaiMatBang = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.itemMatBang = new DevExpress.XtraBars.BarEditItem();
            this.glkMatBang = new DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.itemNap = new DevExpress.XtraBars.BarButtonItem();
            this.itemThem = new DevExpress.XtraBars.BarButtonItem();
            this.itemXoa = new DevExpress.XtraBars.BarButtonItem();
            this.itemLuu = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.gcDinhMuc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDinhMuc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spSTT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spDinhMuc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spDonGiaCH)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkToaNha)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkLoaiMatBang)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkMatBang)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            this.SuspendLayout();
            // 
            // gcDinhMuc
            // 
            this.gcDinhMuc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcDinhMuc.Location = new System.Drawing.Point(0, 31);
            this.gcDinhMuc.MainView = this.gvDinhMuc;
            this.gcDinhMuc.Name = "gcDinhMuc";
            this.gcDinhMuc.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.spDinhMuc,
            this.spSTT,
            this.spDonGiaCH,
            this.repositoryItemCheckEdit1});
            this.gcDinhMuc.Size = new System.Drawing.Size(1068, 472);
            this.gcDinhMuc.TabIndex = 0;
            this.gcDinhMuc.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvDinhMuc});
            // 
            // gvDinhMuc
            // 
            this.gvDinhMuc.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn4,
            this.colTenDM,
            this.colSTT,
            this.colDinhMuc,
            this.colDonGiaCH,
            this.colDienGiai,
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn7});
            this.gvDinhMuc.GridControl = this.gcDinhMuc;
            this.gvDinhMuc.GroupCount = 1;
            this.gvDinhMuc.Name = "gvDinhMuc";
            this.gvDinhMuc.OptionsBehavior.AutoExpandAllGroups = true;
            this.gvDinhMuc.OptionsDetail.EnableMasterViewMode = false;
            this.gvDinhMuc.OptionsDetail.SmartDetailHeight = true;
            this.gvDinhMuc.OptionsSelection.MultiSelect = true;
            this.gvDinhMuc.OptionsView.ColumnAutoWidth = false;
            this.gvDinhMuc.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            this.gvDinhMuc.OptionsView.ShowAutoFilterRow = true;
            this.gvDinhMuc.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.gridColumn4, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.gvDinhMuc.InitNewRow += new DevExpress.XtraGrid.Views.Grid.InitNewRowEventHandler(this.gvDinhMuc_InitNewRow);
            this.gvDinhMuc.ValidateRow += new DevExpress.XtraGrid.Views.Base.ValidateRowEventHandler(this.gvDinhMuc_ValidateRow);
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Nhóm";
            this.gridColumn4.FieldName = "Nhom";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 0;
            // 
            // colTenDM
            // 
            this.colTenDM.Caption = "Tên Định Mức";
            this.colTenDM.FieldName = "TenDM";
            this.colTenDM.Name = "colTenDM";
            this.colTenDM.Visible = true;
            this.colTenDM.VisibleIndex = 1;
            this.colTenDM.Width = 150;
            // 
            // colSTT
            // 
            this.colSTT.Caption = "STT";
            this.colSTT.ColumnEdit = this.spSTT;
            this.colSTT.DisplayFormat.FormatString = "{0:00}";
            this.colSTT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colSTT.FieldName = "STT";
            this.colSTT.Name = "colSTT";
            this.colSTT.Visible = true;
            this.colSTT.VisibleIndex = 0;
            this.colSTT.Width = 45;
            // 
            // spSTT
            // 
            this.spSTT.AutoHeight = false;
            this.spSTT.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spSTT.Name = "spSTT";
            // 
            // colDinhMuc
            // 
            this.colDinhMuc.Caption = "Định Mức";
            this.colDinhMuc.ColumnEdit = this.spDinhMuc;
            this.colDinhMuc.DisplayFormat.FormatString = "{0:00}";
            this.colDinhMuc.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colDinhMuc.FieldName = "DinhMuc";
            this.colDinhMuc.Name = "colDinhMuc";
            this.colDinhMuc.Visible = true;
            this.colDinhMuc.VisibleIndex = 2;
            this.colDinhMuc.Width = 77;
            // 
            // spDinhMuc
            // 
            this.spDinhMuc.AutoHeight = false;
            this.spDinhMuc.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spDinhMuc.Name = "spDinhMuc";
            // 
            // colDonGiaCH
            // 
            this.colDonGiaCH.Caption = "Đơn giá";
            this.colDonGiaCH.ColumnEdit = this.spDonGiaCH;
            this.colDonGiaCH.DisplayFormat.FormatString = "{0:#,0.####}";
            this.colDonGiaCH.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colDonGiaCH.FieldName = "DonGia";
            this.colDonGiaCH.Name = "colDonGiaCH";
            this.colDonGiaCH.Visible = true;
            this.colDonGiaCH.VisibleIndex = 3;
            this.colDonGiaCH.Width = 84;
            // 
            // spDonGiaCH
            // 
            this.spDonGiaCH.AutoHeight = false;
            this.spDonGiaCH.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spDonGiaCH.Name = "spDonGiaCH";
            // 
            // colDienGiai
            // 
            this.colDienGiai.Caption = "Diễn Giải";
            this.colDienGiai.FieldName = "DienGiai";
            this.colDienGiai.Name = "colDienGiai";
            this.colDienGiai.Visible = true;
            this.colDienGiai.VisibleIndex = 4;
            this.colDienGiai.Width = 350;
            // 
            // gridColumn1
            // 
            this.gridColumn1.FieldName = "MaTN";
            this.gridColumn1.Name = "gridColumn1";
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Loai mặt bằng";
            this.gridColumn2.FieldName = "MaLMB";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Width = 84;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Mặt bằng";
            this.gridColumn3.FieldName = "MaMB";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Width = 123;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "Khóa";
            this.gridColumn7.ColumnEdit = this.repositoryItemCheckEdit1;
            this.gridColumn7.FieldName = "IsKhoa";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 5;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
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
            this.itemToaNha,
            this.itemLoaiMatBang,
            this.itemMatBang});
            this.barManager1.MaxItemId = 24;
            this.barManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.lkToaNha,
            this.lkLoaiMatBang,
            this.glkMatBang});
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemToaNha, DevExpress.XtraBars.BarItemPaintStyle.Caption),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemLoaiMatBang, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemMatBang, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
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
            this.itemToaNha.EditWidth = 120;
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
            // itemLoaiMatBang
            // 
            this.itemLoaiMatBang.Caption = "Loại";
            this.itemLoaiMatBang.Edit = this.lkLoaiMatBang;
            this.itemLoaiMatBang.EditWidth = 120;
            this.itemLoaiMatBang.Id = 15;
            this.itemLoaiMatBang.Name = "itemLoaiMatBang";
            // 
            // lkLoaiMatBang
            // 
            this.lkLoaiMatBang.AutoHeight = false;
            this.lkLoaiMatBang.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
            this.lkLoaiMatBang.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenLMB", "Name1")});
            this.lkLoaiMatBang.DisplayMember = "TenLMB";
            this.lkLoaiMatBang.Name = "lkLoaiMatBang";
            this.lkLoaiMatBang.NullText = "";
            this.lkLoaiMatBang.ShowFooter = false;
            this.lkLoaiMatBang.ShowHeader = false;
            this.lkLoaiMatBang.ShowLines = false;
            this.lkLoaiMatBang.ValueMember = "MaLMB";
            this.lkLoaiMatBang.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.lkLoaiMatBang_ButtonClick);
            // 
            // itemMatBang
            // 
            this.itemMatBang.Caption = "Mặt bằng";
            this.itemMatBang.Edit = this.glkMatBang;
            this.itemMatBang.EditWidth = 120;
            this.itemMatBang.Id = 19;
            this.itemMatBang.Name = "itemMatBang";
            // 
            // glkMatBang
            // 
            this.glkMatBang.AutoHeight = false;
            this.glkMatBang.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
            this.glkMatBang.DisplayMember = "MaSoMB";
            this.glkMatBang.Name = "glkMatBang";
            this.glkMatBang.NullText = "";
            this.glkMatBang.PopupView = this.gridView1;
            this.glkMatBang.ValueMember = "MaMB";
            this.glkMatBang.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.glkMatBang_ButtonClick);
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn5,
            this.gridColumn6});
            this.gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView1.OptionsView.ShowAutoFilterRow = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Mặt bằng";
            this.gridColumn5.FieldName = "MaSoMB";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            this.gridColumn5.OptionsColumn.ReadOnly = true;
            this.gridColumn5.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 0;
            this.gridColumn5.Width = 288;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "Khách hàng";
            this.gridColumn6.FieldName = "TenKH";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.OptionsColumn.AllowEdit = false;
            this.gridColumn6.OptionsColumn.ReadOnly = true;
            this.gridColumn6.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 1;
            this.gridColumn6.Width = 339;
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
            this.barDockControlTop.Size = new System.Drawing.Size(1068, 31);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 503);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(1068, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 31);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 472);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1068, 31);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 472);
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.Images.SetKeyName(0, "icons8_refresh1.png");
            this.imageCollection1.Images.SetKeyName(1, "icons8_add4.png");
            this.imageCollection1.Images.SetKeyName(2, "icons8_delete1.png");
            this.imageCollection1.Images.SetKeyName(3, "icons8_save.png");
            // 
            // frmDinhMuc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1068, 503);
            this.Controls.Add(this.gcDinhMuc);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDinhMuc";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ĐỊNH MỨC ĐIỆN";
            this.Load += new System.EventHandler(this.frmDinhMuc_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gcDinhMuc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDinhMuc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spSTT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spDinhMuc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spDonGiaCH)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkToaNha)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkLoaiMatBang)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkMatBang)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gcDinhMuc;
        private DevExpress.XtraGrid.Views.Grid.GridView gvDinhMuc;
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
        private DevExpress.XtraGrid.Columns.GridColumn colTenDM;
        private DevExpress.XtraGrid.Columns.GridColumn colDinhMuc;
        private DevExpress.XtraGrid.Columns.GridColumn colDonGiaCH;
        private DevExpress.XtraGrid.Columns.GridColumn colDienGiai;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit spDinhMuc;
        private DevExpress.XtraBars.BarEditItem itemToaNha;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lkToaNha;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn colSTT;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit spSTT;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit spDonGiaCH;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraBars.BarEditItem itemLoaiMatBang;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lkLoaiMatBang;
        private DevExpress.XtraBars.BarEditItem itemMatBang;
        private DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit glkMatBang;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
    }
}