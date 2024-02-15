namespace TaiSan.Import
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
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.btnChonTapTin = new DevExpress.XtraBars.BarButtonItem();
            this.btnLuu = new DevExpress.XtraBars.BarButtonItem();
            this.btnXoaDong = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
            this.gcTaiSan = new DevExpress.XtraGrid.GridControl();
            this.grvTaiSan = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colKyHieu = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLoai = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lookLoai = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lookLTSCha = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.colDVT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lookDVT = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.colThue = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lookThue = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.colTenLTS = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDacTinh = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTiLeKhauHao = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lookTN = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcTaiSan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvTaiSan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookLoai)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookLTSCha)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookDVT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookThue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookTN)).BeginInit();
            this.SuspendLayout();
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
            this.btnChonTapTin,
            this.btnLuu,
            this.btnXoaDong});
            this.barManager1.MaxItemId = 3;
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnChonTapTin, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnLuu, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnXoaDong, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.DrawDragBorder = false;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Tools";
            // 
            // btnChonTapTin
            // 
            this.btnChonTapTin.Caption = "Chọn tập tin";
            this.btnChonTapTin.Id = 0;
            this.btnChonTapTin.ImageIndex = 0;
            this.btnChonTapTin.Name = "btnChonTapTin";
            this.btnChonTapTin.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnChonTapTin_ItemClick);
            // 
            // btnLuu
            // 
            this.btnLuu.Caption = "Lưu dữ liệu xuống database";
            this.btnLuu.Id = 1;
            this.btnLuu.ImageIndex = 1;
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnLuu_ItemClick);
            // 
            // btnXoaDong
            // 
            this.btnXoaDong.Caption = "Xóa dòng";
            this.btnXoaDong.Id = 2;
            this.btnXoaDong.ImageIndex = 2;
            this.btnXoaDong.Name = "btnXoaDong";
            this.btnXoaDong.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnXoaDong_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.barDockControlTop.Size = new System.Drawing.Size(1054, 31);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 494);
            this.barDockControlBottom.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.barDockControlBottom.Size = new System.Drawing.Size(1054, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 31);
            this.barDockControlLeft.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 463);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1054, 31);
            this.barDockControlRight.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 463);
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.Images.SetKeyName(0, "directory.png");
            this.imageCollection1.Images.SetKeyName(1, "save.png");
            this.imageCollection1.Images.SetKeyName(2, "Delete64 [].png");
            // 
            // gcTaiSan
            // 
            this.gcTaiSan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcTaiSan.Location = new System.Drawing.Point(0, 31);
            this.gcTaiSan.MainView = this.grvTaiSan;
            this.gcTaiSan.Name = "gcTaiSan";
            this.gcTaiSan.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.lookThue,
            this.lookDVT,
            this.lookLoai,
            this.lookLTSCha,
            this.lookTN});
            this.gcTaiSan.Size = new System.Drawing.Size(1054, 463);
            this.gcTaiSan.TabIndex = 9;
            this.gcTaiSan.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvTaiSan});
            // 
            // grvTaiSan
            // 
            this.grvTaiSan.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colKyHieu,
            this.colLoai,
            this.gridColumn1,
            this.colDVT,
            this.colThue,
            this.colTenLTS,
            this.colDacTinh,
            this.colTiLeKhauHao,
            this.gridColumn2});
            this.grvTaiSan.GridControl = this.gcTaiSan;
            this.grvTaiSan.GroupPanelText = "Kéo cột lên đây để xem theo nhóm";
            this.grvTaiSan.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "DienTich", this.colDacTinh, "{0:#,0.##} m2"),
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "ThanhTien", this.colTiLeKhauHao, "{0:#,0.##}"),
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "DonGia", this.colTenLTS, "{0:#,0.##}")});
            this.grvTaiSan.Name = "grvTaiSan";
            this.grvTaiSan.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.True;
            this.grvTaiSan.OptionsMenu.EnableFooterMenu = false;
            this.grvTaiSan.OptionsSelection.MultiSelect = true;
            this.grvTaiSan.OptionsView.ColumnAutoWidth = false;
            this.grvTaiSan.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;
            this.grvTaiSan.OptionsView.ShowAutoFilterRow = true;
            this.grvTaiSan.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.grvMatBang_RowStyle);
            // 
            // colKyHieu
            // 
            this.colKyHieu.Caption = "Mã LTS";
            this.colKyHieu.FieldName = "KyHieu";
            this.colKyHieu.Name = "colKyHieu";
            this.colKyHieu.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "MaSoMB", "Tổng: {0:#,0.##}")});
            this.colKyHieu.Visible = true;
            this.colKyHieu.VisibleIndex = 1;
            this.colKyHieu.Width = 77;
            // 
            // colLoai
            // 
            this.colLoai.Caption = "Loại";
            this.colLoai.ColumnEdit = this.lookLoai;
            this.colLoai.FieldName = "Loai";
            this.colLoai.Name = "colLoai";
            this.colLoai.Visible = true;
            this.colLoai.VisibleIndex = 4;
            this.colLoai.Width = 55;
            // 
            // lookLoai
            // 
            this.lookLoai.AutoHeight = false;
            this.lookLoai.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookLoai.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TypeNam", "Loại")});
            this.lookLoai.DisplayMember = "TypeNam";
            this.lookLoai.Name = "lookLoai";
            this.lookLoai.NullText = "";
            this.lookLoai.ValueMember = "TypeID";
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Loại tài sản nguồn";
            this.gridColumn1.ColumnEdit = this.lookLTSCha;
            this.gridColumn1.FieldName = "LoaiTSCha";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 3;
            this.gridColumn1.Width = 99;
            // 
            // lookLTSCha
            // 
            this.lookLTSCha.AutoHeight = false;
            this.lookLTSCha.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookLTSCha.DisplayMember = "TenLTS";
            this.lookLTSCha.Name = "lookLTSCha";
            this.lookLTSCha.NullText = "";
            this.lookLTSCha.ValueMember = "MaLTS";
            // 
            // colDVT
            // 
            this.colDVT.Caption = "Đơn vị tính";
            this.colDVT.ColumnEdit = this.lookDVT;
            this.colDVT.FieldName = "DVT";
            this.colDVT.Name = "colDVT";
            this.colDVT.Visible = true;
            this.colDVT.VisibleIndex = 5;
            this.colDVT.Width = 74;
            // 
            // lookDVT
            // 
            this.lookDVT.AutoHeight = false;
            this.lookDVT.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookDVT.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenDVT", "Tên")});
            this.lookDVT.DisplayMember = "TenDVT";
            this.lookDVT.Name = "lookDVT";
            this.lookDVT.NullText = "";
            this.lookDVT.ValueMember = "MaDVT";
            // 
            // colThue
            // 
            this.colThue.Caption = "Thuế";
            this.colThue.ColumnEdit = this.lookThue;
            this.colThue.FieldName = "Thue";
            this.colThue.Name = "colThue";
            this.colThue.Visible = true;
            this.colThue.VisibleIndex = 6;
            this.colThue.Width = 60;
            // 
            // lookThue
            // 
            this.lookThue.AutoHeight = false;
            this.lookThue.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookThue.DisplayMember = "TiLe";
            this.lookThue.Name = "lookThue";
            this.lookThue.NullText = "";
            this.lookThue.ValueMember = "ThueID";
            // 
            // colTenLTS
            // 
            this.colTenLTS.Caption = "Tên TLS";
            this.colTenLTS.FieldName = "TenLTS";
            this.colTenLTS.Name = "colTenLTS";
            this.colTenLTS.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "DonGia", "{0:#,0.##}")});
            this.colTenLTS.Visible = true;
            this.colTenLTS.VisibleIndex = 2;
            this.colTenLTS.Width = 187;
            // 
            // colDacTinh
            // 
            this.colDacTinh.Caption = "Đặc tính";
            this.colDacTinh.FieldName = "DacTinh";
            this.colDacTinh.Name = "colDacTinh";
            this.colDacTinh.Visible = true;
            this.colDacTinh.VisibleIndex = 8;
            this.colDacTinh.Width = 377;
            // 
            // colTiLeKhauHao
            // 
            this.colTiLeKhauHao.Caption = "Tỉ lệ khấu hao / tháng";
            this.colTiLeKhauHao.DisplayFormat.FormatString = "{0:#,0.##}";
            this.colTiLeKhauHao.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colTiLeKhauHao.FieldName = "TiLeKhauHao";
            this.colTiLeKhauHao.Name = "colTiLeKhauHao";
            this.colTiLeKhauHao.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "ThanhTien", "{0:#,0.##}")});
            this.colTiLeKhauHao.Visible = true;
            this.colTiLeKhauHao.VisibleIndex = 7;
            this.colTiLeKhauHao.Width = 112;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Dự án";
            this.gridColumn2.ColumnEdit = this.lookTN;
            this.gridColumn2.FieldName = "ToaNha";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 0;
            this.gridColumn2.Width = 123;
            // 
            // lookTN
            // 
            this.lookTN.AutoHeight = false;
            this.lookTN.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookTN.DisplayMember = "TenTN";
            this.lookTN.Name = "lookTN";
            this.lookTN.NullText = "";
            this.lookTN.ValueMember = "MaTN";
            // 
            // frmImport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1054, 494);
            this.Controls.Add(this.gcTaiSan);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "frmImport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Import loại tài sản";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmImport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcTaiSan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvTaiSan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookLoai)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookLTSCha)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookDVT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookThue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookTN)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarButtonItem btnChonTapTin;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraGrid.GridControl gcTaiSan;
        private DevExpress.XtraGrid.Views.Grid.GridView grvTaiSan;
        private DevExpress.XtraGrid.Columns.GridColumn colKyHieu;
        private DevExpress.XtraGrid.Columns.GridColumn colLoai;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lookLoai;
        private DevExpress.XtraGrid.Columns.GridColumn colDVT;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lookDVT;
        private DevExpress.XtraGrid.Columns.GridColumn colThue;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lookThue;
        private DevExpress.XtraGrid.Columns.GridColumn colTenLTS;
        private DevExpress.XtraGrid.Columns.GridColumn colDacTinh;
        private DevExpress.XtraGrid.Columns.GridColumn colTiLeKhauHao;
        private DevExpress.XtraBars.BarButtonItem btnLuu;
        private DevExpress.Utils.ImageCollection imageCollection1;
        private DevExpress.XtraBars.BarButtonItem btnXoaDong;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lookLTSCha;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lookTN;
    }
}