namespace DichVu.ThueNgoai
{
    partial class frmCongNoManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCongNoManager));
            this.imageCollection1 = new DevExpress.Utils.ImageCollection();
            this.barManager1 = new DevExpress.XtraBars.BarManager();
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.itemKyBC = new DevExpress.XtraBars.BarEditItem();
            this.cmbKyBC = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.itemTuNgay = new DevExpress.XtraBars.BarEditItem();
            this.dateTuNgay = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.itemDenNgay = new DevExpress.XtraBars.BarEditItem();
            this.DenNgay = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.itemToaNha = new DevExpress.XtraBars.BarEditItem();
            this.lookToaNha = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.btnRefresh = new DevExpress.XtraBars.BarButtonItem();
            this.btnThanhToan = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barStaticItem1 = new DevExpress.XtraBars.BarStaticItem();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.TuNgay = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.gcCongNo = new DevExpress.XtraGrid.GridControl();
            this.grvCongNo = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDaThanhToan = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colConNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPhaiThanhToan = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbKyBC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTuNgay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTuNgay.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DenNgay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DenNgay.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookToaNha)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TuNgay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TuNgay.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcCongNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvCongNo)).BeginInit();
            this.SuspendLayout();
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.Images.SetKeyName(0, "icons8_refresh1.png");  
            this.imageCollection1.Images.SetKeyName(1, "icons8_thanhtoan.png");  
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
            this.itemDenNgay,
            this.btnRefresh,
            this.barStaticItem1,
            this.barButtonItem1,
            this.btnThanhToan,
            this.itemToaNha,
            this.itemTuNgay,
            this.itemKyBC});
            this.barManager1.MaxItemId = 12;
            this.barManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.DenNgay,
            this.TuNgay,
            this.lookToaNha,
            this.dateTuNgay,
            this.cmbKyBC});
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.itemKyBC),
            new DevExpress.XtraBars.LinkPersistInfo(this.itemTuNgay),
            new DevExpress.XtraBars.LinkPersistInfo(this.itemDenNgay),
            new DevExpress.XtraBars.LinkPersistInfo(this.itemToaNha),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnRefresh, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnThanhToan, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar1.OptionsBar.AllowCollapse = true;
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.DisableCustomization = true;
            this.bar1.OptionsBar.DrawDragBorder = false;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Tools";
            // 
            // itemKyBC
            // 
            this.itemKyBC.Caption = "barEditItem1";
            this.itemKyBC.Edit = this.cmbKyBC;
            this.itemKyBC.EditWidth = 114;
            this.itemKyBC.Id = 11;
            this.itemKyBC.Name = "itemKyBC";
            // 
            // cmbKyBC
            // 
            this.cmbKyBC.AutoHeight = false;
            this.cmbKyBC.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbKyBC.Name = "cmbKyBC";
            this.cmbKyBC.NullText = "Kỳ báo cáo";
            this.cmbKyBC.EditValueChanged += new System.EventHandler(this.cmbKyBC_EditValueChanged);
            // 
            // itemTuNgay
            // 
            this.itemTuNgay.Caption = "barEditItem1";
            this.itemTuNgay.Edit = this.dateTuNgay;
            this.itemTuNgay.EditWidth = 82;
            this.itemTuNgay.Id = 10;
            this.itemTuNgay.Name = "itemTuNgay";
            this.itemTuNgay.EditValueChanged += new System.EventHandler(this.itemTuNgay_EditValueChanged);
            // 
            // dateTuNgay
            // 
            this.dateTuNgay.AutoHeight = false;
            this.dateTuNgay.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateTuNgay.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateTuNgay.DisplayFormat.FormatString = "{0:dd/MM/yyyy}";
            this.dateTuNgay.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateTuNgay.EditFormat.FormatString = "{0:dd/MM/yyyy}";
            this.dateTuNgay.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateTuNgay.Name = "dateTuNgay";
            this.dateTuNgay.NullText = "Từ ngày";
            // 
            // itemDenNgay
            // 
            this.itemDenNgay.Caption = "Đến ngày";
            this.itemDenNgay.Edit = this.DenNgay;
            this.itemDenNgay.EditWidth = 80;
            this.itemDenNgay.Id = 0;
            this.itemDenNgay.Name = "itemDenNgay";
            this.itemDenNgay.EditValueChanged += new System.EventHandler(this.itemDenNgay_EditValueChanged);
            // 
            // DenNgay
            // 
            this.DenNgay.AutoHeight = false;
            this.DenNgay.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.DenNgay.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.DenNgay.Name = "DenNgay";
            this.DenNgay.NullText = "Đến ngày";
            // 
            // itemToaNha
            // 
            this.itemToaNha.Caption = "Toaf nhaf";
            this.itemToaNha.Edit = this.lookToaNha;
            this.itemToaNha.EditWidth = 91;
            this.itemToaNha.Id = 9;
            this.itemToaNha.Name = "itemToaNha";
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
            this.lookToaNha.NullText = "[Dự án ...]";
            this.lookToaNha.ValueMember = "MaTN";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Caption = "Nạp lại";
            this.btnRefresh.Id = 1;
            this.btnRefresh.ImageOptions.ImageIndex = 0;
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnRefresh_ItemClick);
            // 
            // btnThanhToan
            // 
            this.btnThanhToan.Caption = "Thanh toán";
            this.btnThanhToan.Id = 5;
            this.btnThanhToan.ImageOptions.ImageIndex = 1;
            this.btnThanhToan.Name = "btnThanhToan";
            this.btnThanhToan.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnThanhToan_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(947, 31);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 434);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(947, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 31);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 403);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(947, 31);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 403);
            // 
            // barStaticItem1
            // 
            this.barStaticItem1.Caption = "Hợp đồng";
            this.barStaticItem1.Id = 2;
            this.barStaticItem1.Name = "barStaticItem1";
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "In";
            this.barButtonItem1.Id = 4;
            this.barButtonItem1.Name = "barButtonItem1";
            // 
            // TuNgay
            // 
            this.TuNgay.AutoHeight = false;
            this.TuNgay.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.TuNgay.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.TuNgay.Name = "TuNgay";
            this.TuNgay.NullText = "Từ ngày";
            // 
            // gcCongNo
            // 
            this.gcCongNo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcCongNo.Location = new System.Drawing.Point(0, 31);
            this.gcCongNo.MainView = this.grvCongNo;
            this.gcCongNo.MenuManager = this.barManager1;
            this.gcCongNo.Name = "gcCongNo";
            this.gcCongNo.Size = new System.Drawing.Size(947, 403);
            this.gcCongNo.TabIndex = 8;
            this.gcCongNo.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvCongNo});
            // 
            // grvCongNo
            // 
            this.grvCongNo.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.colDaThanhToan,
            this.colConNo,
            this.colPhaiThanhToan,
            this.gridColumn4,
            this.gridColumn2});
            this.grvCongNo.GridControl = this.gcCongNo;
            this.grvCongNo.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "DaThanhToan", this.colDaThanhToan, "Tổng: {0:#,0.##}"),
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "ConNo", this.colConNo, "Tổng: {0:#,0.##}"),
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "PhaiThanhToan", this.colPhaiThanhToan, "Tổng: {0:#,0.##}")});
            this.grvCongNo.Name = "grvCongNo";
            this.grvCongNo.OptionsBehavior.Editable = false;
            this.grvCongNo.OptionsView.ColumnAutoWidth = false;
            this.grvCongNo.OptionsView.ShowAutoFilterRow = true;
            this.grvCongNo.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.grvCongNo.OptionsView.ShowFooter = true;
            this.grvCongNo.OptionsView.ShowGroupedColumns = true;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Hợp đồng";
            this.gridColumn1.FieldName = "SoHopDong";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 159;
            // 
            // colDaThanhToan
            // 
            this.colDaThanhToan.Caption = "Đã thanh toán";
            this.colDaThanhToan.DisplayFormat.FormatString = "{0:#,0.##} VNĐ";
            this.colDaThanhToan.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colDaThanhToan.FieldName = "DaThanhToan";
            this.colDaThanhToan.Name = "colDaThanhToan";
            this.colDaThanhToan.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "DaThanhToan", "{0:#,0.##}")});
            this.colDaThanhToan.Visible = true;
            this.colDaThanhToan.VisibleIndex = 4;
            this.colDaThanhToan.Width = 98;
            // 
            // colConNo
            // 
            this.colConNo.Caption = "Còn nợ";
            this.colConNo.DisplayFormat.FormatString = "{0:#,0.##} VNĐ";
            this.colConNo.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colConNo.FieldName = "ConNo";
            this.colConNo.Name = "colConNo";
            this.colConNo.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "ConNo", "{0:#,0.##}")});
            this.colConNo.Visible = true;
            this.colConNo.VisibleIndex = 5;
            this.colConNo.Width = 97;
            // 
            // colPhaiThanhToan
            // 
            this.colPhaiThanhToan.Caption = "Phải thanh toán";
            this.colPhaiThanhToan.DisplayFormat.FormatString = "{0:#,0.##} VNĐ";
            this.colPhaiThanhToan.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colPhaiThanhToan.FieldName = "PhaiThanhToan";
            this.colPhaiThanhToan.Name = "colPhaiThanhToan";
            this.colPhaiThanhToan.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "PhaiThanhToan", "{0:#,0.##}")});
            this.colPhaiThanhToan.Visible = true;
            this.colPhaiThanhToan.VisibleIndex = 3;
            this.colPhaiThanhToan.Width = 108;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Tháng thanh toán";
            this.gridColumn4.DisplayFormat.FormatString = "d";
            this.gridColumn4.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gridColumn4.FieldName = "NgayThanhToan";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 2;
            this.gridColumn4.Width = 99;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Nhà cung cấp";
            this.gridColumn2.FieldName = "NhaCungCap";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 165;
            // 
            // frmCongNoManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(947, 434);
            this.Controls.Add(this.gcCongNo);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "frmCongNoManager";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Công nợ hợp đồng thuê ngoài";
            this.Load += new System.EventHandler(this.frmCongNoManager_Load);
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbKyBC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTuNgay.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTuNgay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DenNgay.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DenNgay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookToaNha)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TuNgay.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TuNgay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcCongNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvCongNo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.Utils.ImageCollection imageCollection1;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarEditItem itemDenNgay;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit DenNgay;
        private DevExpress.XtraBars.BarButtonItem btnRefresh;
        private DevExpress.XtraBars.BarButtonItem btnThanhToan;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarStaticItem barStaticItem1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit TuNgay;
        private DevExpress.XtraGrid.GridControl gcCongNo;
        private DevExpress.XtraGrid.Views.Grid.GridView grvCongNo;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn colDaThanhToan;
        private DevExpress.XtraGrid.Columns.GridColumn colConNo;
        private DevExpress.XtraGrid.Columns.GridColumn colPhaiThanhToan;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraBars.BarEditItem itemToaNha;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lookToaNha;
        private DevExpress.XtraBars.BarEditItem itemTuNgay;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit dateTuNgay;
        private DevExpress.XtraBars.BarEditItem itemKyBC;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox cmbKyBC;
    }
}