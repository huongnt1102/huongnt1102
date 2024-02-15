namespace Library.ThongKeCtl
{
    partial class ctlTKThuChi
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ctlTKThuChi));
            DevExpress.XtraCharts.SideBySideBarSeriesLabel sideBySideBarSeriesLabel1 = new DevExpress.XtraCharts.SideBySideBarSeriesLabel();
            this.barManager1 = new DevExpress.XtraBars.BarManager();
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.itemtoanha = new DevExpress.XtraBars.BarEditItem();
            this.lookToaNha = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.itemKyBC = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemComboBoxKyBaoCao = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.btnNap = new DevExpress.XtraBars.BarButtonItem();
            this.itemExport = new DevExpress.XtraBars.BarSubItem();
            this.itemImg = new DevExpress.XtraBars.BarButtonItem();
            this.itemPdf = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.repositoryItemDateEditNgay = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.repositoryItemDateEditDenNgay = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.chartControlDoanhThu = new DevExpress.XtraCharts.ChartControl();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookToaNha)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxKyBaoCao)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEditNgay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEditNgay.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEditDenNgay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEditDenNgay.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartControlDoanhThu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
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
            this.itemKyBC,
            this.btnNap,
            this.itemtoanha,
            this.itemExport,
            this.itemImg,
            this.itemPdf});
            this.barManager1.MaxItemId = 9;
            this.barManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemDateEditNgay,
            this.repositoryItemDateEditDenNgay,
            this.repositoryItemComboBoxKyBaoCao,
            this.lookToaNha});
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.itemtoanha),
            new DevExpress.XtraBars.LinkPersistInfo(this.itemKyBC),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnNap, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemExport, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.DrawDragBorder = false;
            this.bar1.OptionsBar.MultiLine = true;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Tools";
            // 
            // itemtoanha
            // 
            this.itemtoanha.Caption = "Dự án";
            this.itemtoanha.Edit = this.lookToaNha;
            this.itemtoanha.EditWidth = 150;
            this.itemtoanha.Id = 5;
            this.itemtoanha.Name = "itemtoanha";
            this.itemtoanha.EditValueChanged += new System.EventHandler(this.itemtoanha_EditValueChanged);
            // 
            // lookToaNha
            // 
            this.lookToaNha.AutoHeight = false;
            this.lookToaNha.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookToaNha.Name = "lookToaNha";
            this.lookToaNha.NullText = "Chọn Dự án";
            // 
            // itemKyBC
            // 
            this.itemKyBC.Caption = "Kỳ bc";
            this.itemKyBC.Edit = this.repositoryItemComboBoxKyBaoCao;
            this.itemKyBC.EditWidth = 80;
            this.itemKyBC.Id = 3;
            this.itemKyBC.Name = "itemKyBC";
            this.itemKyBC.EditValueChanged += new System.EventHandler(this.itemKyBC_EditValueChanged);
            // 
            // repositoryItemComboBoxKyBaoCao
            // 
            this.repositoryItemComboBoxKyBaoCao.AutoHeight = false;
            this.repositoryItemComboBoxKyBaoCao.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBoxKyBaoCao.Name = "repositoryItemComboBoxKyBaoCao";
            this.repositoryItemComboBoxKyBaoCao.NullText = "Kỳ báo cáo";
            // 
            // btnNap
            // 
            this.btnNap.Caption = "Nạp báo cáo";
            this.btnNap.Id = 4;
            this.btnNap.ImageOptions.ImageIndex = 0;
            this.btnNap.Name = "btnNap";
            this.btnNap.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnNap_ItemClick);
            // 
            // itemExport
            // 
            this.itemExport.Caption = "Export";
            this.itemExport.Id = 6;
            this.itemExport.ImageOptions.ImageIndex = 1;
            this.itemExport.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemImg, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemPdf, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.itemExport.Name = "itemExport";
            // 
            // itemImg
            // 
            this.itemImg.Caption = "Image";
            this.itemImg.Id = 7;
            this.itemImg.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("itemImg.ImageOptions.Image")));
            this.itemImg.Name = "itemImg";
            this.itemImg.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemImg_ItemClick);
            // 
            // itemPdf
            // 
            this.itemPdf.Caption = "Pdf";
            this.itemPdf.Id = 8;
            this.itemPdf.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("itemPdf.ImageOptions.Image")));
            this.itemPdf.Name = "itemPdf";
            this.itemPdf.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemPdf_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(589, 31);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 414);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(589, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 31);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 383);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(589, 31);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 383);
            // 
            // repositoryItemDateEditNgay
            // 
            this.repositoryItemDateEditNgay.AutoHeight = false;
            this.repositoryItemDateEditNgay.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemDateEditNgay.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.repositoryItemDateEditNgay.Name = "repositoryItemDateEditNgay";
            this.repositoryItemDateEditNgay.NullText = "Từ ngày";
            // 
            // repositoryItemDateEditDenNgay
            // 
            this.repositoryItemDateEditDenNgay.AutoHeight = false;
            this.repositoryItemDateEditDenNgay.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemDateEditDenNgay.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.repositoryItemDateEditDenNgay.Name = "repositoryItemDateEditDenNgay";
            this.repositoryItemDateEditDenNgay.NullText = "Đến ngày";
            // 
            // chartControlDoanhThu
            // 
            this.chartControlDoanhThu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartControlDoanhThu.Legend.Name = "Default Legend";
            this.chartControlDoanhThu.Location = new System.Drawing.Point(0, 31);
            this.chartControlDoanhThu.Name = "chartControlDoanhThu";
            this.chartControlDoanhThu.SeriesSerializable = new DevExpress.XtraCharts.Series[0];
            sideBySideBarSeriesLabel1.LineVisibility = DevExpress.Utils.DefaultBoolean.True;
            this.chartControlDoanhThu.SeriesTemplate.Label = sideBySideBarSeriesLabel1;
            this.chartControlDoanhThu.Size = new System.Drawing.Size(589, 383);
            this.chartControlDoanhThu.TabIndex = 4;
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.Images.SetKeyName(0, "Refresh5.png");  
            this.imageCollection1.Images.SetKeyName(1, "Export1.png");  
            // 
            // ctlTKThuChi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chartControlDoanhThu);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "ctlTKThuChi";
            this.Size = new System.Drawing.Size(589, 414);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookToaNha)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxKyBaoCao)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEditNgay.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEditNgay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEditDenNgay.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEditDenNgay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartControlDoanhThu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarEditItem itemKyBC;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBoxKyBaoCao;
        private DevExpress.XtraBars.BarButtonItem btnNap;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit repositoryItemDateEditNgay;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit repositoryItemDateEditDenNgay;
        private DevExpress.XtraCharts.ChartControl chartControlDoanhThu;
        private DevExpress.XtraBars.BarEditItem itemtoanha;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lookToaNha;
        private DevExpress.XtraBars.BarSubItem itemExport;
        private DevExpress.XtraBars.BarButtonItem itemImg;
        private DevExpress.XtraBars.BarButtonItem itemPdf;
        private DevExpress.Utils.ImageCollection imageCollection1;
    }
}
