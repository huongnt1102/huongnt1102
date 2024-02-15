namespace Library.ThongKeCtl
{
    partial class ctlThongKeSoLuongYeuCauTrongNam
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ctlThongKeSoLuongYeuCauTrongNam));
            DevExpress.XtraCharts.SideBySideBarSeriesLabel sideBySideBarSeriesLabel1 = new DevExpress.XtraCharts.SideBySideBarSeriesLabel();
            this.barManager1 = new DevExpress.XtraBars.BarManager();
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.itemKyBC = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemComboBoxKyBaoCao = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.itemTuNgay = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemDateEditNgay = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.itemDenNgay = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemDateEditDenNgay = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.btnTKNap = new DevExpress.XtraBars.BarButtonItem();
            this.itemExport = new DevExpress.XtraBars.BarSubItem();
            this.itemEx2Image = new DevExpress.XtraBars.BarButtonItem();
            this.itemEx2pdf = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.chartSoLuongYeuCau = new DevExpress.XtraCharts.ChartControl();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxKyBaoCao)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEditNgay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEditNgay.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEditDenNgay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEditDenNgay.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartSoLuongYeuCau)).BeginInit();
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
            this.itemTuNgay,
            this.itemDenNgay,
            this.itemKyBC,
            this.btnTKNap,
            this.itemExport,
            this.itemEx2Image,
            this.itemEx2pdf});
            this.barManager1.MaxItemId = 8;
            this.barManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemDateEditNgay,
            this.repositoryItemDateEditDenNgay,
            this.repositoryItemComboBoxKyBaoCao});
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
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnTKNap, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemExport, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.DrawDragBorder = false;
            this.bar1.OptionsBar.MultiLine = true;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Tools";
            // 
            // itemKyBC
            // 
            this.itemKyBC.Caption = "Kỳ bc";
            this.itemKyBC.Edit = this.repositoryItemComboBoxKyBaoCao;
            this.itemKyBC.EditWidth = 80;
            this.itemKyBC.Id = 3;
            this.itemKyBC.Name = "itemKyBC";
            // 
            // repositoryItemComboBoxKyBaoCao
            // 
            this.repositoryItemComboBoxKyBaoCao.AutoHeight = false;
            this.repositoryItemComboBoxKyBaoCao.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBoxKyBaoCao.Name = "repositoryItemComboBoxKyBaoCao";
            this.repositoryItemComboBoxKyBaoCao.NullText = "Kỳ báo cáo";
            this.repositoryItemComboBoxKyBaoCao.EditValueChanged += new System.EventHandler(this.repositoryItemComboBoxKyBaoCao_EditValueChanged);
            // 
            // itemTuNgay
            // 
            this.itemTuNgay.Edit = this.repositoryItemDateEditNgay;
            this.itemTuNgay.EditWidth = 80;
            this.itemTuNgay.Id = 0;
            this.itemTuNgay.Name = "itemTuNgay";
            this.itemTuNgay.EditValueChanged += new System.EventHandler(this.itemTuNgay_EditValueChanged);
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
            // itemDenNgay
            // 
            this.itemDenNgay.Edit = this.repositoryItemDateEditDenNgay;
            this.itemDenNgay.EditWidth = 80;
            this.itemDenNgay.Id = 1;
            this.itemDenNgay.Name = "itemDenNgay";
            this.itemDenNgay.EditValueChanged += new System.EventHandler(this.itemDenNgay_EditValueChanged);
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
            // btnTKNap
            // 
            this.btnTKNap.Caption = "Nạp lại";
            this.btnTKNap.Id = 4;
            this.btnTKNap.ImageOptions.ImageIndex = 0;
            this.btnTKNap.Name = "btnTKNap";
            // 
            // itemExport
            // 
            this.itemExport.Caption = "Export";
            this.itemExport.Id = 5;
            this.itemExport.ImageOptions.ImageIndex = 1;
            this.itemExport.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.itemEx2Image),
            new DevExpress.XtraBars.LinkPersistInfo(this.itemEx2pdf)});
            this.itemExport.Name = "itemExport";
            // 
            // itemEx2Image
            // 
            this.itemEx2Image.Caption = "Image";
            this.itemEx2Image.Id = 6;
            this.itemEx2Image.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("itemEx2Image.ImageOptions.Image")));
            this.itemEx2Image.Name = "itemEx2Image";
            this.itemEx2Image.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemEx2Image_ItemClick);
            // 
            // itemEx2pdf
            // 
            this.itemEx2pdf.Caption = "Pdf";
            this.itemEx2pdf.Id = 7;
            this.itemEx2pdf.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("itemEx2pdf.ImageOptions.Image")));
            this.itemEx2pdf.Name = "itemEx2pdf";
            this.itemEx2pdf.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemEx2pdf_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(786, 31);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 473);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(786, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 31);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 442);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(786, 31);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 442);
            // 
            // chartSoLuongYeuCau
            // 
            this.chartSoLuongYeuCau.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartSoLuongYeuCau.Legend.Name = "Default Legend";
            this.chartSoLuongYeuCau.Location = new System.Drawing.Point(0, 31);
            this.chartSoLuongYeuCau.Name = "chartSoLuongYeuCau";
            this.chartSoLuongYeuCau.SeriesSerializable = new DevExpress.XtraCharts.Series[0];
            sideBySideBarSeriesLabel1.LineVisibility = DevExpress.Utils.DefaultBoolean.True;
            this.chartSoLuongYeuCau.SeriesTemplate.Label = sideBySideBarSeriesLabel1;
            this.chartSoLuongYeuCau.Size = new System.Drawing.Size(786, 442);
            this.chartSoLuongYeuCau.TabIndex = 4;
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.Images.SetKeyName(0, "Refresh5.png");  
            this.imageCollection1.Images.SetKeyName(1, "Export1.png");  
            // 
            // ctlThongKeSoLuongYeuCauTrongNam
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chartSoLuongYeuCau);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "ctlThongKeSoLuongYeuCauTrongNam";
            this.Size = new System.Drawing.Size(786, 473);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxKyBaoCao)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEditNgay.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEditNgay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEditDenNgay.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEditDenNgay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartSoLuongYeuCau)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarEditItem itemKyBC;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBoxKyBaoCao;
        private DevExpress.XtraBars.BarEditItem itemTuNgay;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit repositoryItemDateEditNgay;
        private DevExpress.XtraBars.BarEditItem itemDenNgay;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit repositoryItemDateEditDenNgay;
        private DevExpress.XtraBars.BarButtonItem btnTKNap;
        private DevExpress.XtraBars.BarSubItem itemExport;
        private DevExpress.XtraBars.BarButtonItem itemEx2Image;
        private DevExpress.XtraBars.BarButtonItem itemEx2pdf;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraCharts.ChartControl chartSoLuongYeuCau;
        private DevExpress.Utils.ImageCollection imageCollection1;
    }
}
