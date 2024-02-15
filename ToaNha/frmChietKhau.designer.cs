namespace ToaNha
{
    partial class frmChietKhau
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmChietKhau));
            this.gcChietKhau = new DevExpress.XtraGrid.GridControl();
            this.gvChietKhau = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lkLoaiDichVu = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.spKyTT = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.spTyLeCK = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lkLoaiTien = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.spinDonGia = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.txtNVN = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.barManager1 = new DevExpress.XtraBars.BarManager();
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.itemToaNha = new DevExpress.XtraBars.BarEditItem();
            this.lkToaNha = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.itemThem = new DevExpress.XtraBars.BarButtonItem();
            this.itemXoa = new DevExpress.XtraBars.BarButtonItem();
            this.itemLuu = new DevExpress.XtraBars.BarButtonItem();
            this.itemNap = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection();
            this.lk_TenToaNha = new DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit();
            this.repositoryItemGridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.gcChietKhau)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvChietKhau)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkLoaiDichVu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spKyTT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spTyLeCK)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkLoaiTien)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinDonGia)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNVN)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkToaNha)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lk_TenToaNha)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemGridLookUpEdit1View)).BeginInit();
            this.SuspendLayout();
            // 
            // gcChietKhau
            // 
            this.gcChietKhau.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcChietKhau.Location = new System.Drawing.Point(0, 31);
            this.gcChietKhau.MainView = this.gvChietKhau;
            this.gcChietKhau.Name = "gcChietKhau";
            this.gcChietKhau.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.lkLoaiTien,
            this.spinDonGia,
            this.lkLoaiDichVu,
            this.spKyTT,
            this.spTyLeCK,
            this.txtNVN});
            this.gcChietKhau.Size = new System.Drawing.Size(882, 363);
            this.gcChietKhau.TabIndex = 0;
            this.gcChietKhau.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvChietKhau});
            this.gcChietKhau.KeyUp += new System.Windows.Forms.KeyEventHandler(this.gcChietKhau_KeyUp);
            // 
            // gvChietKhau
            // 
            this.gvChietKhau.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn6,
            this.gridColumn5,
            this.gridColumn7,
            this.gridColumn8,
            this.gridColumn9,
            this.gridColumn1});
            this.gvChietKhau.GridControl = this.gcChietKhau;
            this.gvChietKhau.Name = "gvChietKhau";
            this.gvChietKhau.OptionsDetail.EnableMasterViewMode = false;
            this.gvChietKhau.OptionsSelection.MultiSelect = true;
            this.gvChietKhau.OptionsView.ColumnAutoWidth = false;
            this.gvChietKhau.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            this.gvChietKhau.OptionsView.ShowGroupPanel = false;
            this.gvChietKhau.InitNewRow += new DevExpress.XtraGrid.Views.Grid.InitNewRowEventHandler(this.gvChietKhau_InitNewRow);
            this.gvChietKhau.ValidateRow += new DevExpress.XtraGrid.Views.Base.ValidateRowEventHandler(this.gvChietKhau_ValidateRow);
            this.gvChietKhau.RowUpdated += new DevExpress.XtraGrid.Views.Base.RowObjectEventHandler(this.gvChietKhau_RowUpdated);
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Loại Dịch Vụ";
            this.gridColumn2.ColumnEdit = this.lkLoaiDichVu;
            this.gridColumn2.FieldName = "MaLDV";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 0;
            this.gridColumn2.Width = 167;
            // 
            // lkLoaiDichVu
            // 
            this.lkLoaiDichVu.AutoHeight = false;
            this.lkLoaiDichVu.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkLoaiDichVu.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenLDV", "Tên")});
            this.lkLoaiDichVu.DisplayMember = "TenLDV";
            this.lkLoaiDichVu.Name = "lkLoaiDichVu";
            this.lkLoaiDichVu.NullText = "";
            this.lkLoaiDichVu.ShowHeader = false;
            this.lkLoaiDichVu.ValueMember = "ID";
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Kỳ Thanh Toán";
            this.gridColumn3.ColumnEdit = this.spKyTT;
            this.gridColumn3.FieldName = "KyTT";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 1;
            this.gridColumn3.Width = 92;
            // 
            // spKyTT
            // 
            this.spKyTT.AutoHeight = false;
            this.spKyTT.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spKyTT.DisplayFormat.FormatString = "{0} tháng";
            this.spKyTT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spKyTT.Name = "spKyTT";
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Tỷ Lệ Chiết Khấu";
            this.gridColumn4.ColumnEdit = this.spTyLeCK;
            this.gridColumn4.FieldName = "TyLeCK";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 2;
            this.gridColumn4.Width = 92;
            // 
            // spTyLeCK
            // 
            this.spTyLeCK.AutoHeight = false;
            this.spTyLeCK.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spTyLeCK.DisplayFormat.FormatString = "p2";
            this.spTyLeCK.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spTyLeCK.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.spTyLeCK.Mask.EditMask = "p2";
            this.spTyLeCK.Name = "spTyLeCK";
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "Diễn Giải";
            this.gridColumn6.FieldName = "DienGiai";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 3;
            this.gridColumn6.Width = 344;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Nhân Viên Nhập";
            this.gridColumn5.FieldName = "MaNVN";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Width = 120;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "Ngày Nhập";
            this.gridColumn7.DisplayFormat.FormatString = "{0:dd/MM/yyyy}";
            this.gridColumn7.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gridColumn7.FieldName = "NgayNhap";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.Width = 100;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "Nhân Viên Sửa";
            this.gridColumn8.FieldName = "MaNVS";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.Width = 120;
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "Ngày Sửa";
            this.gridColumn9.DisplayFormat.FormatString = "{0:dd/MM/yyyy}";
            this.gridColumn9.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gridColumn9.FieldName = "NgaySua";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.Width = 100;
            // 
            // gridColumn1
            // 
            this.gridColumn1.FieldName = "MaTN";
            this.gridColumn1.Name = "gridColumn1";
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
            // spinDonGia
            // 
            this.spinDonGia.AutoHeight = false;
            this.spinDonGia.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinDonGia.Name = "spinDonGia";
            // 
            // txtNVN
            // 
            this.txtNVN.AutoHeight = false;
            this.txtNVN.Name = "txtNVN";
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
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemThem, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemXoa, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemLuu, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemNap, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
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
            this.barDockControlTop.Size = new System.Drawing.Size(882, 31);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 394);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(882, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 31);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 363);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(882, 31);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 363);
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.Images.SetKeyName(0, "Add1.png");
            this.imageCollection1.Images.SetKeyName(1, "Delete1.png");
            this.imageCollection1.Images.SetKeyName(2, "Save1.png");
            this.imageCollection1.Images.SetKeyName(3, "Refresh1.png");
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
            // frmChietKhau
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(882, 394);
            this.Controls.Add(this.gcChietKhau);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmChietKhau";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chiết Khấu";
            this.Load += new System.EventHandler(this.frmChietKhau_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gcChietKhau)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvChietKhau)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkLoaiDichVu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spKyTT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spTyLeCK)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkLoaiTien)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinDonGia)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNVN)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkToaNha)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lk_TenToaNha)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemGridLookUpEdit1View)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gcChietKhau;
        private DevExpress.XtraGrid.Views.Grid.GridView gvChietKhau;
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
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lkLoaiTien;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit spinDonGia;
        private DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit lk_TenToaNha;
        private DevExpress.XtraGrid.Views.Grid.GridView repositoryItemGridLookUpEdit1View;
        private DevExpress.XtraBars.BarEditItem itemToaNha;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lkToaNha;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lkLoaiDichVu;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit spKyTT;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit spTyLeCK;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit txtNVN;
    }
}