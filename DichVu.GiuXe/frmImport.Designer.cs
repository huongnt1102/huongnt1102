namespace DichVu.GiuXe
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmImport));
            this.gcTheXe = new DevExpress.XtraGrid.GridControl();
            this.grvTheXe = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colMatBang = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNgayDK = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSoThe = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colChuThe = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLoaiXe = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBienSo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDoiXe = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMauXe = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNgayHH = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colKyTT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDienGiai = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.barManager1 = new DevExpress.XtraBars.BarManager();
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.itemChoice = new DevExpress.XtraBars.BarButtonItem();
            this.itemSheet = new DevExpress.XtraBars.BarEditItem();
            this.cmbSheet = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.itemKieuLTT = new DevExpress.XtraBars.BarEditItem();
            this.cmbKieuLTT = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.itemSave = new DevExpress.XtraBars.BarButtonItem();
            this.itemDelete = new DevExpress.XtraBars.BarButtonItem();
            this.itemClose = new DevExpress.XtraBars.BarButtonItem();
            this.itemExportMau = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection();
            this.itemNgayNhap = new DevExpress.XtraBars.BarEditItem();
            this.dateNgayNhap = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.lookUpToaNha = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            ((System.ComponentModel.ISupportInitialize)(this.gcTheXe)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvTheXe)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSheet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbKieuLTT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateNgayNhap)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateNgayNhap.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpToaNha)).BeginInit();
            this.SuspendLayout();
            // 
            // gcTheXe
            // 
            this.gcTheXe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcTheXe.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gcTheXe.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gcTheXe.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gcTheXe.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gcTheXe.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.gcTheXe.EmbeddedNavigator.TextStringFormat = "{0}/{1}";
            this.gcTheXe.Location = new System.Drawing.Point(0, 31);
            this.gcTheXe.MainView = this.grvTheXe;
            this.gcTheXe.Name = "gcTheXe";
            this.gcTheXe.Size = new System.Drawing.Size(1014, 413);
            this.gcTheXe.TabIndex = 10;
            this.gcTheXe.UseEmbeddedNavigator = true;
            this.gcTheXe.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvTheXe});
            // 
            // grvTheXe
            // 
            this.grvTheXe.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colMatBang,
            this.gridColumn3,
            this.colNgayDK,
            this.colSoThe,
            this.colChuThe,
            this.colLoaiXe,
            this.colBienSo,
            this.colDoiXe,
            this.colMauXe,
            this.colNgayHH,
            this.colKyTT,
            this.gridColumn1,
            this.gridColumn4,
            this.gridColumn5,
            this.colDienGiai,
            this.gridColumn2});
            this.grvTheXe.GridControl = this.gcTheXe;
            this.grvTheXe.Name = "grvTheXe";
            this.grvTheXe.OptionsSelection.MultiSelect = true;
            this.grvTheXe.OptionsView.ColumnAutoWidth = false;
            this.grvTheXe.OptionsView.ShowAutoFilterRow = true;
            this.grvTheXe.OptionsView.ShowGroupedColumns = true;
            this.grvTheXe.OptionsView.ShowGroupPanel = false;
            // 
            // colMatBang
            // 
            this.colMatBang.Caption = "Mặt bằng (*)";
            this.colMatBang.FieldName = "MaSoMB";
            this.colMatBang.Name = "colMatBang";
            this.colMatBang.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.colMatBang.Visible = true;
            this.colMatBang.VisibleIndex = 0;
            this.colMatBang.Width = 93;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Số đăng ký";
            this.gridColumn3.FieldName = "SoDK";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 1;
            // 
            // colNgayDK
            // 
            this.colNgayDK.Caption = "Ngày đăng ký";
            this.colNgayDK.DisplayFormat.FormatString = "{0:dd/MM/yyyy}";
            this.colNgayDK.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colNgayDK.FieldName = "NgayDK";
            this.colNgayDK.Name = "colNgayDK";
            this.colNgayDK.Visible = true;
            this.colNgayDK.VisibleIndex = 2;
            this.colNgayDK.Width = 78;
            // 
            // colSoThe
            // 
            this.colSoThe.Caption = "Mã thẻ";
            this.colSoThe.FieldName = "SoThe";
            this.colSoThe.Name = "colSoThe";
            this.colSoThe.Visible = true;
            this.colSoThe.VisibleIndex = 3;
            // 
            // colChuThe
            // 
            this.colChuThe.Caption = "Chủ thẻ";
            this.colChuThe.FieldName = "ChuThe";
            this.colChuThe.Name = "colChuThe";
            this.colChuThe.Visible = true;
            this.colChuThe.VisibleIndex = 4;
            this.colChuThe.Width = 132;
            // 
            // colLoaiXe
            // 
            this.colLoaiXe.Caption = "Loại xe (*)";
            this.colLoaiXe.FieldName = "LoaiXe";
            this.colLoaiXe.Name = "colLoaiXe";
            this.colLoaiXe.Visible = true;
            this.colLoaiXe.VisibleIndex = 5;
            // 
            // colBienSo
            // 
            this.colBienSo.Caption = "Biển số";
            this.colBienSo.FieldName = "BienSo";
            this.colBienSo.Name = "colBienSo";
            this.colBienSo.Visible = true;
            this.colBienSo.VisibleIndex = 6;
            // 
            // colDoiXe
            // 
            this.colDoiXe.Caption = "Đời xe";
            this.colDoiXe.FieldName = "DoiXe";
            this.colDoiXe.Name = "colDoiXe";
            this.colDoiXe.Visible = true;
            this.colDoiXe.VisibleIndex = 7;
            // 
            // colMauXe
            // 
            this.colMauXe.Caption = "Mẫu xe";
            this.colMauXe.FieldName = "MauXe";
            this.colMauXe.Name = "colMauXe";
            this.colMauXe.Visible = true;
            this.colMauXe.VisibleIndex = 8;
            // 
            // colNgayHH
            // 
            this.colNgayHH.Caption = "Ngày thanh toán";
            this.colNgayHH.DisplayFormat.FormatString = "{0:dd/MM/yyyy}";
            this.colNgayHH.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colNgayHH.FieldName = "NgayTT";
            this.colNgayHH.Name = "colNgayHH";
            this.colNgayHH.Visible = true;
            this.colNgayHH.VisibleIndex = 9;
            this.colNgayHH.Width = 100;
            // 
            // colKyTT
            // 
            this.colKyTT.Caption = "Kỳ thanh toán";
            this.colKyTT.FieldName = "KyTT";
            this.colKyTT.Name = "colKyTT";
            this.colKyTT.Visible = true;
            this.colKyTT.VisibleIndex = 10;
            this.colKyTT.Width = 100;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Tỷ lệ VAT";
            this.gridColumn1.FieldName = "TyLeVAT";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 11;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Từ ngày";
            this.gridColumn4.FieldName = "TuNgay";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 12;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Đến ngày";
            this.gridColumn5.FieldName = "DenNgay";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 13;
            // 
            // colDienGiai
            // 
            this.colDienGiai.Caption = "Diễn giải";
            this.colDienGiai.FieldName = "DienGiai";
            this.colDienGiai.Name = "colDienGiai";
            this.colDienGiai.Visible = true;
            this.colDienGiai.VisibleIndex = 14;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Error";
            this.gridColumn2.FieldName = "Error";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 15;
            this.gridColumn2.Width = 200;
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
            this.itemNgayNhap,
            this.itemKieuLTT,
            this.itemExportMau});
            this.barManager1.MaxItemId = 10;
            this.barManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.cmbSheet,
            this.dateNgayNhap,
            this.lookUpToaNha,
            this.cmbKieuLTT});
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemChoice, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemSheet, DevExpress.XtraBars.BarItemPaintStyle.Standard),
            new DevExpress.XtraBars.LinkPersistInfo(this.itemKieuLTT),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemSave, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemDelete, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemClose, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemExportMau, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
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
            // itemKieuLTT
            // 
            this.itemKieuLTT.Edit = this.cmbKieuLTT;
            this.itemKieuLTT.EditValue = "Tạo lịch thanh toán theo từng thẻ xe";
            this.itemKieuLTT.EditWidth = 200;
            this.itemKieuLTT.Id = 8;
            this.itemKieuLTT.Name = "itemKieuLTT";
            // 
            // cmbKieuLTT
            // 
            this.cmbKieuLTT.AutoHeight = false;
            this.cmbKieuLTT.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbKieuLTT.Items.AddRange(new object[] {
            "Tạo lịch thanh toán theo từng thẻ xe",
            "Tạo lịch thanh toán tổng hợp"});
            this.cmbKieuLTT.Name = "cmbKieuLTT";
            this.cmbKieuLTT.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            // 
            // itemSave
            // 
            this.itemSave.Caption = "Lưu";
            this.itemSave.Id = 2;
            this.itemSave.ImageOptions.ImageIndex = 1;
            this.itemSave.Name = "itemSave";
            this.itemSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemSave_ItemClick);
            // 
            // itemDelete
            // 
            this.itemDelete.Caption = "Xóa dòng";
            this.itemDelete.Id = 3;
            this.itemDelete.ImageOptions.ImageIndex = 2;
            this.itemDelete.Name = "itemDelete";
            this.itemDelete.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemDelete_ItemClick);
            // 
            // itemClose
            // 
            this.itemClose.Caption = "Đóng";
            this.itemClose.Id = 4;
            this.itemClose.ImageOptions.ImageIndex = 3;
            this.itemClose.Name = "itemClose";
            this.itemClose.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemClose_ItemClick);
            // 
            // itemExportMau
            // 
            this.itemExportMau.Caption = "Export mẫu";
            this.itemExportMau.Id = 9;
            this.itemExportMau.ImageOptions.ImageIndex = 4;
            this.itemExportMau.Name = "itemExportMau";
            this.itemExportMau.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemExportMau_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(1014, 31);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 444);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(1014, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 31);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 413);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1014, 31);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 413);
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.Images.SetKeyName(0, "icons8_open.png");
            this.imageCollection1.Images.SetKeyName(1, "icons8_save.png");
            this.imageCollection1.Images.SetKeyName(2, "icons8_delete1.png");
            this.imageCollection1.Images.SetKeyName(3, "icons8_cancel1.png");
            this.imageCollection1.Images.SetKeyName(4, "Export1.png");
            // 
            // itemNgayNhap
            // 
            this.itemNgayNhap.Edit = this.dateNgayNhap;
            this.itemNgayNhap.EditWidth = 94;
            this.itemNgayNhap.Id = 6;
            this.itemNgayNhap.Name = "itemNgayNhap";
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
            // lookUpToaNha
            // 
            this.lookUpToaNha.AutoHeight = false;
            this.lookUpToaNha.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpToaNha.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenTN", "Name1")});
            this.lookUpToaNha.DisplayMember = "TenTN";
            this.lookUpToaNha.Name = "lookUpToaNha";
            this.lookUpToaNha.NullText = "[Chọn Dự án]";
            this.lookUpToaNha.ShowHeader = false;
            this.lookUpToaNha.ValueMember = "MaTN";
            // 
            // frmImport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1014, 444);
            this.Controls.Add(this.gcTheXe);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "frmImport";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Import danh sách thẻ xe";
            this.Load += new System.EventHandler(this.frmImport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gcTheXe)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvTheXe)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSheet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbKieuLTT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateNgayNhap.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateNgayNhap)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpToaNha)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gcTheXe;
        private DevExpress.XtraGrid.Views.Grid.GridView grvTheXe;
        private DevExpress.XtraGrid.Columns.GridColumn colMatBang;
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
        private DevExpress.XtraBars.BarEditItem itemNgayNhap;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit dateNgayNhap;
        private DevExpress.XtraGrid.Columns.GridColumn colSoThe;
        private DevExpress.XtraGrid.Columns.GridColumn colNgayDK;
        private DevExpress.XtraGrid.Columns.GridColumn colNgayHH;
        private DevExpress.XtraGrid.Columns.GridColumn colChuThe;
        private DevExpress.XtraGrid.Columns.GridColumn colLoaiXe;
        private DevExpress.XtraGrid.Columns.GridColumn colBienSo;
        private DevExpress.XtraGrid.Columns.GridColumn colDoiXe;
        private DevExpress.XtraGrid.Columns.GridColumn colMauXe;
        private DevExpress.XtraGrid.Columns.GridColumn colDienGiai;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lookUpToaNha;
        private DevExpress.XtraGrid.Columns.GridColumn colKyTT;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraBars.BarEditItem itemKieuLTT;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox cmbKieuLTT;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraBars.BarButtonItem itemExportMau;
    }
}