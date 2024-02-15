namespace DichVu.Dien
{
    partial class frmImport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmImport));
            this.gcDien = new DevExpress.XtraGrid.GridControl();
            this.grvNuoc = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repSpinChiSoMoi = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.gridColumn14 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn20 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn15 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn16 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn17 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn18 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn19 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lookNhanVien = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.lookKhachHang = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.spinTienDien = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.slookMatBang = new DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit();
            this.repositoryItemSearchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.itemChoice = new DevExpress.XtraBars.BarButtonItem();
            this.itemSheet = new DevExpress.XtraBars.BarEditItem();
            this.cmbSheet = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.itemDelete = new DevExpress.XtraBars.BarButtonItem();
            this.itemSave = new DevExpress.XtraBars.BarButtonItem();
            this.itemClose = new DevExpress.XtraBars.BarButtonItem();
            this.itemExport = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
            this.dateNgayNhap = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.gridColumn21 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gcDien)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvNuoc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repSpinChiSoMoi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookNhanVien)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookKhachHang)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinTienDien)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.slookMatBang)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSearchLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSheet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateNgayNhap)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateNgayNhap.CalendarTimeProperties)).BeginInit();
            this.SuspendLayout();
            // 
            // gcDien
            // 
            this.gcDien.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcDien.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gcDien.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gcDien.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gcDien.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gcDien.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.gcDien.EmbeddedNavigator.TextStringFormat = "{0}/{1}";
            this.gcDien.Location = new System.Drawing.Point(0, 31);
            this.gcDien.MainView = this.grvNuoc;
            this.gcDien.Name = "gcDien";
            this.gcDien.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.lookNhanVien,
            this.lookKhachHang,
            this.spinTienDien,
            this.slookMatBang,
            this.repSpinChiSoMoi});
            this.gcDien.Size = new System.Drawing.Size(895, 520);
            this.gcDien.TabIndex = 10;
            this.gcDien.UseEmbeddedNavigator = true;
            this.gcDien.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvNuoc});
            // 
            // grvNuoc
            // 
            this.grvNuoc.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn13,
            this.gridColumn6,
            this.gridColumn7,
            this.gridColumn8,
            this.gridColumn9,
            this.gridColumn14,
            this.gridColumn12,
            this.gridColumn21,
            this.gridColumn10,
            this.gridColumn20,
            this.gridColumn11,
            this.gridColumn2,
            this.gridColumn5,
            this.gridColumn15,
            this.gridColumn16,
            this.gridColumn17,
            this.gridColumn18,
            this.gridColumn19});
            this.grvNuoc.GridControl = this.gcDien;
            this.grvNuoc.Name = "grvNuoc";
            this.grvNuoc.OptionsMenu.ShowConditionalFormattingItem = true;
            this.grvNuoc.OptionsSelection.MultiSelect = true;
            this.grvNuoc.OptionsView.ColumnAutoWidth = false;
            this.grvNuoc.OptionsView.ShowAutoFilterRow = true;
            this.grvNuoc.OptionsView.ShowGroupedColumns = true;
            this.grvNuoc.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Mã mặt bằng (*)";
            this.gridColumn1.FieldName = "MaSoMB";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 2;
            this.gridColumn1.Width = 100;
            // 
            // gridColumn13
            // 
            this.gridColumn13.Caption = "Đồng hồ (*)";
            this.gridColumn13.FieldName = "SoDH";
            this.gridColumn13.Name = "gridColumn13";
            this.gridColumn13.Visible = true;
            this.gridColumn13.VisibleIndex = 4;
            this.gridColumn13.Width = 89;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "Từ ngày (*)";
            this.gridColumn6.FieldName = "TuNgay";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 5;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "Đến ngày (*)";
            this.gridColumn7.FieldName = "DenNgay";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 7;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "Chỉ số cũ";
            this.gridColumn8.FieldName = "ChiSoCu";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 9;
            this.gridColumn8.Width = 65;
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "Chỉ số mới";
            this.gridColumn9.ColumnEdit = this.repSpinChiSoMoi;
            this.gridColumn9.FieldName = "ChiSoMoi";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 10;
            this.gridColumn9.Width = 65;
            // 
            // repSpinChiSoMoi
            // 
            this.repSpinChiSoMoi.AutoHeight = false;
            this.repSpinChiSoMoi.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repSpinChiSoMoi.Name = "repSpinChiSoMoi";
            // 
            // gridColumn14
            // 
            this.gridColumn14.Caption = "Hệ số";
            this.gridColumn14.FieldName = "HeSo";
            this.gridColumn14.Name = "gridColumn14";
            this.gridColumn14.Visible = true;
            this.gridColumn14.VisibleIndex = 11;
            this.gridColumn14.Width = 40;
            // 
            // gridColumn12
            // 
            this.gridColumn12.Caption = "Số tiêu thụ";
            this.gridColumn12.FieldName = "SoTieuThu";
            this.gridColumn12.Name = "gridColumn12";
            this.gridColumn12.Visible = true;
            this.gridColumn12.VisibleIndex = 12;
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "Tỷ lệ VAT";
            this.gridColumn10.DisplayFormat.FormatString = "p0";
            this.gridColumn10.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn10.FieldName = "TyLeVAT";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 13;
            this.gridColumn10.Width = 61;
            // 
            // gridColumn20
            // 
            this.gridColumn20.Caption = "Tiền thuế VAT";
            this.gridColumn20.FieldName = "TienThue";
            this.gridColumn20.Name = "gridColumn20";
            this.gridColumn20.Visible = true;
            this.gridColumn20.VisibleIndex = 14;
            // 
            // gridColumn11
            // 
            this.gridColumn11.Caption = "Số tiền";
            this.gridColumn11.DisplayFormat.FormatString = "{0:#,0.##}";
            this.gridColumn11.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn11.FieldName = "SoTien";
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.Visible = true;
            this.gridColumn11.VisibleIndex = 15;
            this.gridColumn11.Width = 81;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Diễn giải";
            this.gridColumn2.FieldName = "DienGiai";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 16;
            this.gridColumn2.Width = 199;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Error";
            this.gridColumn5.FieldName = "Error";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 18;
            this.gridColumn5.Width = 331;
            // 
            // gridColumn15
            // 
            this.gridColumn15.Caption = "Ngày TT (*)";
            this.gridColumn15.FieldName = "NgayTT";
            this.gridColumn15.Name = "gridColumn15";
            this.gridColumn15.Visible = true;
            this.gridColumn15.VisibleIndex = 6;
            // 
            // gridColumn16
            // 
            this.gridColumn16.Caption = "Tháng";
            this.gridColumn16.FieldName = "Thang";
            this.gridColumn16.Name = "gridColumn16";
            this.gridColumn16.Visible = true;
            this.gridColumn16.VisibleIndex = 0;
            // 
            // gridColumn17
            // 
            this.gridColumn17.Caption = "Năm";
            this.gridColumn17.FieldName = "Nam";
            this.gridColumn17.Name = "gridColumn17";
            this.gridColumn17.Visible = true;
            this.gridColumn17.VisibleIndex = 1;
            // 
            // gridColumn18
            // 
            this.gridColumn18.Caption = "Số tiêu thụ đồng hồ cũ";
            this.gridColumn18.FieldName = "TieuThuDHCu";
            this.gridColumn18.Name = "gridColumn18";
            this.gridColumn18.Visible = true;
            this.gridColumn18.VisibleIndex = 17;
            // 
            // gridColumn19
            // 
            this.gridColumn19.Caption = "Mã khách hàng";
            this.gridColumn19.FieldName = "MaKhachHang";
            this.gridColumn19.Name = "gridColumn19";
            this.gridColumn19.Visible = true;
            this.gridColumn19.VisibleIndex = 3;
            this.gridColumn19.Width = 117;
            // 
            // lookNhanVien
            // 
            this.lookNhanVien.AutoHeight = false;
            this.lookNhanVien.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookNhanVien.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("MaSoNV", "Name3", 30, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("HoTenNV", "Name4", 70, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default)});
            this.lookNhanVien.DisplayMember = "HoTenNV";
            this.lookNhanVien.Name = "lookNhanVien";
            this.lookNhanVien.NullText = "";
            this.lookNhanVien.ShowHeader = false;
            this.lookNhanVien.ShowLines = false;
            this.lookNhanVien.ValueMember = "MaNV";
            // 
            // lookKhachHang
            // 
            this.lookKhachHang.AutoHeight = false;
            this.lookKhachHang.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookKhachHang.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenKH", "Name7")});
            this.lookKhachHang.DisplayMember = "TenKH";
            this.lookKhachHang.Name = "lookKhachHang";
            this.lookKhachHang.NullText = "";
            this.lookKhachHang.ShowHeader = false;
            this.lookKhachHang.ValueMember = "MaKH";
            // 
            // spinTienDien
            // 
            this.spinTienDien.AutoHeight = false;
            this.spinTienDien.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinTienDien.Mask.EditMask = "c0";
            this.spinTienDien.Name = "spinTienDien";
            // 
            // slookMatBang
            // 
            this.slookMatBang.AutoHeight = false;
            this.slookMatBang.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.slookMatBang.DisplayMember = "MaSoMB";
            this.slookMatBang.Name = "slookMatBang";
            this.slookMatBang.NullText = "";
            this.slookMatBang.PopupView = this.repositoryItemSearchLookUpEdit1View;
            this.slookMatBang.ValueMember = "MaMB";
            // 
            // repositoryItemSearchLookUpEdit1View
            // 
            this.repositoryItemSearchLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn3,
            this.gridColumn4});
            this.repositoryItemSearchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.repositoryItemSearchLookUpEdit1View.Name = "repositoryItemSearchLookUpEdit1View";
            this.repositoryItemSearchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.repositoryItemSearchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Mã MB";
            this.gridColumn3.FieldName = "MaSoMB";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 0;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Diễn giải";
            this.gridColumn4.FieldName = "DienGiai";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 1;
            // 
            // barManager1
            // 
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
            this.itemChoice,
            this.itemSave,
            this.itemDelete,
            this.itemClose,
            this.itemSheet,
            this.itemExport});
            this.barManager1.MaxItemId = 8;
            this.barManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.cmbSheet,
            this.dateNgayNhap});
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemChoice, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(this.itemSheet),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemDelete, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemSave, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemClose, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemExport, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.DisableCustomization = true;
            this.bar1.OptionsBar.DrawDragBorder = false;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Tools";
            // 
            // itemChoice
            // 
            this.itemChoice.Caption = "Chọn file";
            this.itemChoice.Id = 0;
            this.itemChoice.ImageOptions.ImageIndex = 0;
            this.itemChoice.Name = "itemChoice";
            this.itemChoice.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemChoice_ItemClick);
            // 
            // itemSheet
            // 
            this.itemSheet.Edit = this.cmbSheet;
            this.itemSheet.EditWidth = 100;
            this.itemSheet.Id = 5;
            this.itemSheet.Name = "itemSheet";
            this.itemSheet.EditValueChanged += new System.EventHandler(this.itemSheet_EditValueChanged);
            // 
            // cmbSheet
            // 
            this.cmbSheet.AutoHeight = false;
            this.cmbSheet.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbSheet.Name = "cmbSheet";
            this.cmbSheet.NullText = "[Chọn sheet]";
            this.cmbSheet.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            // 
            // itemDelete
            // 
            this.itemDelete.Caption = "Xóa dòng";
            this.itemDelete.Id = 3;
            this.itemDelete.ImageOptions.ImageIndex = 1;
            this.itemDelete.Name = "itemDelete";
            this.itemDelete.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemDelete_ItemClick);
            // 
            // itemSave
            // 
            this.itemSave.Caption = "Lưu";
            this.itemSave.Id = 2;
            this.itemSave.ImageOptions.ImageIndex = 2;
            this.itemSave.Name = "itemSave";
            this.itemSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemSave_ItemClick);
            // 
            // itemClose
            // 
            this.itemClose.Caption = "Đóng";
            this.itemClose.Id = 4;
            this.itemClose.ImageOptions.ImageIndex = 3;
            this.itemClose.Name = "itemClose";
            this.itemClose.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemClose_ItemClick);
            // 
            // itemExport
            // 
            this.itemExport.Caption = "Export mẫu";
            this.itemExport.Id = 7;
            this.itemExport.ImageOptions.ImageIndex = 4;
            this.itemExport.Name = "itemExport";
            this.itemExport.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemExport_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(895, 31);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 551);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(895, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 31);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 520);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(895, 31);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 520);
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.Images.SetKeyName(0, "icons8_open.png");
            this.imageCollection1.Images.SetKeyName(1, "icons8_delete1.png");
            this.imageCollection1.Images.SetKeyName(2, "icons8_save.png");
            this.imageCollection1.Images.SetKeyName(3, "icons8_cancel1.png");
            this.imageCollection1.Images.SetKeyName(4, "Export1.png");
            // 
            // dateNgayNhap
            // 
            this.dateNgayNhap.AutoHeight = false;
            this.dateNgayNhap.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateNgayNhap.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateNgayNhap.DisplayFormat.FormatString = "{0:dd/MM/yyyy}";
            this.dateNgayNhap.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateNgayNhap.EditFormat.FormatString = "{0:dd/MM/yyyy}";
            this.dateNgayNhap.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateNgayNhap.Mask.EditMask = "dd/MM/yyyy";
            this.dateNgayNhap.Name = "dateNgayNhap";
            // 
            // gridColumn21
            // 
            this.gridColumn21.Caption = "Tiền trước thuế";
            this.gridColumn21.FieldName = "TienTruocThue";
            this.gridColumn21.Name = "gridColumn21";
            this.gridColumn21.Visible = true;
            this.gridColumn21.VisibleIndex = 8;
            // 
            // frmImport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(895, 551);
            this.Controls.Add(this.gcDien);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "frmImport";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Import chỉ số Điện";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmImport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gcDien)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvNuoc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repSpinChiSoMoi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookNhanVien)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookKhachHang)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinTienDien)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.slookMatBang)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSearchLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSheet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateNgayNhap.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateNgayNhap)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gcDien;
        private DevExpress.XtraGrid.Views.Grid.GridView grvNuoc;
        private DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit slookMatBang;
        private DevExpress.XtraGrid.Views.Grid.GridView repositoryItemSearchLookUpEdit1View;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lookKhachHang;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit spinTienDien;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lookNhanVien;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarButtonItem itemChoice;
        private DevExpress.XtraBars.BarEditItem itemSheet;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox cmbSheet;
        private DevExpress.XtraBars.BarButtonItem itemDelete;
        private DevExpress.XtraBars.BarButtonItem itemSave;
        private DevExpress.XtraBars.BarButtonItem itemClose;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.Utils.ImageCollection imageCollection1;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit dateNgayNhap;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn12;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn13;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn14;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn15;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn16;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn17;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit repSpinChiSoMoi;
        private DevExpress.XtraBars.BarButtonItem itemExport;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn18;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn19;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn20;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn21;
    }
}