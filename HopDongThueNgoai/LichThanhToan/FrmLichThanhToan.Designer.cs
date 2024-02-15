namespace HopDongThueNgoai.LichThanhToan
{
    partial class FrmLichThanhToan
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLichThanhToan));
            this.barManager1 = new DevExpress.XtraBars.BarManager();
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.itemBuilding = new DevExpress.XtraBars.BarEditItem();
            this.lkBuilding = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.itemKbc = new DevExpress.XtraBars.BarEditItem();
            this.cbxKbc = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.itemDateFrom = new DevExpress.XtraBars.BarEditItem();
            this.dateDateFrom = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.itemDateTo = new DevExpress.XtraBars.BarEditItem();
            this.dateDateTo = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.itemNap = new DevExpress.XtraBars.BarButtonItem();
            this.itemThanhToan = new DevExpress.XtraBars.BarButtonItem();
            this.itemDelete = new DevExpress.XtraBars.BarButtonItem();
            this.itemTaoPhanQuyen = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection();
            this.gc = new DevExpress.XtraGrid.GridControl();
            this.gv = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn15 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn14 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkBuilding)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxKbc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDateFrom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDateFrom.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDateTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDateTo.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv)).BeginInit();
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
            this.barManager1.Images = this.imageCollection1;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.itemBuilding,
            this.itemKbc,
            this.itemDateFrom,
            this.itemDateTo,
            this.itemNap,
            this.itemThanhToan,
            this.itemDelete,
            this.itemTaoPhanQuyen});
            this.barManager1.MaxItemId = 8;
            this.barManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.lkBuilding,
            this.cbxKbc,
            this.dateDateFrom,
            this.dateDateTo});
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.Width, this.itemBuilding, "", false, true, true, 110),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.Width, this.itemKbc, "", false, true, true, 128),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.Width, this.itemDateFrom, "", false, true, true, 109),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.Width, this.itemDateTo, "", false, true, true, 113),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemNap, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemThanhToan, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemDelete, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(this.itemTaoPhanQuyen)});
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.DrawBorder = false;
            this.bar1.OptionsBar.MultiLine = true;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Tools";
            // 
            // itemBuilding
            // 
            this.itemBuilding.Caption = "Tòa nhà";
            this.itemBuilding.Edit = this.lkBuilding;
            this.itemBuilding.Id = 0;
            this.itemBuilding.Name = "itemBuilding";
            // 
            // lkBuilding
            // 
            this.lkBuilding.AutoHeight = false;
            this.lkBuilding.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkBuilding.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenTN", "Name1")});
            this.lkBuilding.DisplayMember = "TenTN";
            this.lkBuilding.Name = "lkBuilding";
            this.lkBuilding.NullText = "";
            this.lkBuilding.ShowHeader = false;
            this.lkBuilding.ValueMember = "MaTN";
            // 
            // itemKbc
            // 
            this.itemKbc.Caption = "Kỳ báo cáo";
            this.itemKbc.Edit = this.cbxKbc;
            this.itemKbc.Id = 1;
            this.itemKbc.Name = "itemKbc";
            // 
            // cbxKbc
            // 
            this.cbxKbc.AutoHeight = false;
            this.cbxKbc.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbxKbc.Name = "cbxKbc";
            this.cbxKbc.EditValueChanged += new System.EventHandler(this.CbxKbc_EditValueChanged);
            // 
            // itemDateFrom
            // 
            this.itemDateFrom.Caption = "Từ ngày";
            this.itemDateFrom.Edit = this.dateDateFrom;
            this.itemDateFrom.Id = 2;
            this.itemDateFrom.Name = "itemDateFrom";
            // 
            // dateDateFrom
            // 
            this.dateDateFrom.AutoHeight = false;
            this.dateDateFrom.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateDateFrom.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateDateFrom.Name = "dateDateFrom";
            // 
            // itemDateTo
            // 
            this.itemDateTo.Caption = "Đến ngày";
            this.itemDateTo.Edit = this.dateDateTo;
            this.itemDateTo.Id = 3;
            this.itemDateTo.Name = "itemDateTo";
            // 
            // dateDateTo
            // 
            this.dateDateTo.AutoHeight = false;
            this.dateDateTo.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateDateTo.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateDateTo.Name = "dateDateTo";
            // 
            // itemNap
            // 
            this.itemNap.Caption = "Nạp";
            this.itemNap.Id = 4;
            this.itemNap.ImageOptions.ImageIndex = 0;
            this.itemNap.Name = "itemNap";
            this.itemNap.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.ItemNap_ItemClick);
            // 
            // itemThanhToan
            // 
            this.itemThanhToan.Caption = "Thanh toán";
            this.itemThanhToan.Id = 5;
            this.itemThanhToan.ImageOptions.ImageIndex = 1;
            this.itemThanhToan.Name = "itemThanhToan";
            this.itemThanhToan.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.ItemThanhToan_ItemClick);
            // 
            // itemDelete
            // 
            this.itemDelete.Caption = "Xóa";
            this.itemDelete.Id = 6;
            this.itemDelete.ImageOptions.ImageIndex = 2;
            this.itemDelete.Name = "itemDelete";
            this.itemDelete.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemDelete_ItemClick);
            // 
            // itemTaoPhanQuyen
            // 
            this.itemTaoPhanQuyen.Caption = "Tạo phân quyền";
            this.itemTaoPhanQuyen.Id = 7;
            this.itemTaoPhanQuyen.Name = "itemTaoPhanQuyen";
            this.itemTaoPhanQuyen.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemTaoPhanQuyen_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(757, 69);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 523);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(757, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 69);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 454);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(757, 69);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 454);
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.Images.SetKeyName(0, "Refresh1.png");
            this.imageCollection1.Images.SetKeyName(1, "Tien3.png");
            this.imageCollection1.Images.SetKeyName(2, "Delete1.png");
            // 
            // gc
            // 
            this.gc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gc.Location = new System.Drawing.Point(0, 69);
            this.gc.MainView = this.gv;
            this.gc.MenuManager = this.barManager1;
            this.gc.Name = "gc";
            this.gc.Size = new System.Drawing.Size(757, 454);
            this.gc.TabIndex = 4;
            this.gc.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gv});
            // 
            // gv
            // 
            this.gv.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn6,
            this.gridColumn12,
            this.gridColumn13,
            this.gridColumn15,
            this.gridColumn10,
            this.gridColumn11,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn7,
            this.gridColumn8,
            this.gridColumn9,
            this.gridColumn14});
            this.gv.GridControl = this.gc;
            this.gv.Name = "gv";
            this.gv.OptionsBehavior.Editable = false;
            this.gv.OptionsView.ColumnAutoWidth = false;
            this.gv.OptionsView.ShowAutoFilterRow = true;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Hợp đồng";
            this.gridColumn1.FieldName = "HopDongNo";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 140;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Đối tác";
            this.gridColumn2.FieldName = "HoTenKh";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 248;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "Diễn giải";
            this.gridColumn6.FieldName = "DienGiai";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 2;
            this.gridColumn6.Width = 362;
            // 
            // gridColumn12
            // 
            this.gridColumn12.Caption = "Số tiền";
            this.gridColumn12.DisplayFormat.FormatString = "{0:#,0.##; (#,0.##);-}";
            this.gridColumn12.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn12.FieldName = "SoTien";
            this.gridColumn12.Name = "gridColumn12";
            this.gridColumn12.Visible = true;
            this.gridColumn12.VisibleIndex = 3;
            this.gridColumn12.Width = 149;
            // 
            // gridColumn13
            // 
            this.gridColumn13.Caption = "Số tiền quy đổi";
            this.gridColumn13.DisplayFormat.FormatString = "{0:#,0.##; (#,0.##);-}";
            this.gridColumn13.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn13.FieldName = "SoTienQuyDoi";
            this.gridColumn13.Name = "gridColumn13";
            this.gridColumn13.Visible = true;
            this.gridColumn13.VisibleIndex = 4;
            this.gridColumn13.Width = 128;
            // 
            // gridColumn15
            // 
            this.gridColumn15.Caption = "Đã thanh toán";
            this.gridColumn15.DisplayFormat.FormatString = "{0:#,0.##; (#,0.##);-}";
            this.gridColumn15.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn15.FieldName = "DaTra";
            this.gridColumn15.Name = "gridColumn15";
            this.gridColumn15.Visible = true;
            this.gridColumn15.VisibleIndex = 5;
            this.gridColumn15.Width = 127;
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "Loại tiền";
            this.gridColumn10.FieldName = "TenLoaiTien";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 6;
            this.gridColumn10.Width = 126;
            // 
            // gridColumn11
            // 
            this.gridColumn11.Caption = "Tỷ giá";
            this.gridColumn11.DisplayFormat.FormatString = "{0:#,0.##; (#,0.##);-}";
            this.gridColumn11.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn11.FieldName = "TyGia";
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.Visible = true;
            this.gridColumn11.VisibleIndex = 7;
            this.gridColumn11.Width = 97;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Mặt bằng";
            this.gridColumn3.FieldName = "MaSoMB";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 8;
            this.gridColumn3.Width = 166;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Vị trí";
            this.gridColumn4.FieldName = "NoiLamViecName";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 9;
            this.gridColumn4.Width = 281;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Đợt thanh toán";
            this.gridColumn5.FieldName = "DotThanhToan";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 10;
            this.gridColumn5.Width = 109;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "Từ ngày";
            this.gridColumn7.FieldName = "TuNgay";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 11;
            this.gridColumn7.Width = 115;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "Đến ngày";
            this.gridColumn8.FieldName = "DenNgay";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 12;
            this.gridColumn8.Width = 146;
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "Kỳ thanh toán";
            this.gridColumn9.FieldName = "SoThang";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 13;
            this.gridColumn9.Width = 117;
            // 
            // gridColumn14
            // 
            this.gridColumn14.Caption = "Ngày hết hạn";
            this.gridColumn14.FieldName = "NgayHetHan";
            this.gridColumn14.Name = "gridColumn14";
            this.gridColumn14.Visible = true;
            this.gridColumn14.VisibleIndex = 14;
            this.gridColumn14.Width = 139;
            // 
            // FrmLichThanhToan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(757, 523);
            this.Controls.Add(this.gc);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "FrmLichThanhToan";
            this.Text = "Lịch thanh toán";
            this.Load += new System.EventHandler(this.FrmLichThanhToan_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkBuilding)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxKbc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDateFrom.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDateFrom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDateTo.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDateTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarEditItem itemBuilding;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lkBuilding;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarEditItem itemKbc;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox cbxKbc;
        private DevExpress.XtraBars.BarEditItem itemDateFrom;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit dateDateFrom;
        private DevExpress.XtraBars.BarEditItem itemDateTo;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit dateDateTo;
        private DevExpress.XtraBars.BarButtonItem itemNap;
        private DevExpress.XtraGrid.GridControl gc;
        private DevExpress.XtraGrid.Views.Grid.GridView gv;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn12;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn13;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn14;
        private DevExpress.Utils.ImageCollection imageCollection1;
        private DevExpress.XtraBars.BarButtonItem itemThanhToan;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn15;
        private DevExpress.XtraBars.BarButtonItem itemDelete;
        private DevExpress.XtraBars.BarButtonItem itemTaoPhanQuyen;
    }
}