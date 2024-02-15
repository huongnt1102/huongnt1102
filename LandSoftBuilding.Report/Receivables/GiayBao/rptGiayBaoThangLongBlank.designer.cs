namespace LandSoftBuilding.Receivables.GiayBao
{
    partial class rptGiayBaoThangLongBlank
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

        #region Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(rptGiayBaoThangLongBlank));
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.rtTenLDV = new DevExpress.XtraReports.UI.XRRichText();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.xrRichText1 = new DevExpress.XtraReports.UI.XRRichText();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.xrRichText2 = new DevExpress.XtraReports.UI.XRRichText();
            this.NgayIn = new DevExpress.XtraReports.Parameters.Parameter();
            this.ThangTB = new DevExpress.XtraReports.Parameters.Parameter();
            ((System.ComponentModel.ISupportInitialize)(this.rtTenLDV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrRichText1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrRichText2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.rtTenLDV});
            this.Detail.HeightF = 50.2917F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // rtTenLDV
            // 
            this.rtTenLDV.Font = new System.Drawing.Font("Arial", 9F);
            this.rtTenLDV.LocationFloat = new DevExpress.Utils.PointFloat(0F, 12.5F);
            this.rtTenLDV.Name = "rtTenLDV";
            this.rtTenLDV.SerializableRtfString = resources.GetString("rtTenLDV.SerializableRtfString");
            this.rtTenLDV.SizeF = new System.Drawing.SizeF(725.9999F, 23F);
            this.rtTenLDV.StylePriority.UseFont = false;
            // 
            // TopMargin
            // 
            this.TopMargin.HeightF = 33.33333F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // BottomMargin
            // 
            this.BottomMargin.HeightF = 50F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // ReportHeader
            // 
            this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrRichText1});
            this.ReportHeader.HeightF = 252.1805F;
            this.ReportHeader.Name = "ReportHeader";
            // 
            // xrRichText1
            // 
            this.xrRichText1.Font = new System.Drawing.Font("Arial", 9F);
            this.xrRichText1.ForeColor = System.Drawing.Color.Transparent;
            this.xrRichText1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 10.00001F);
            this.xrRichText1.Name = "xrRichText1";
            this.xrRichText1.SerializableRtfString = resources.GetString("xrRichText1.SerializableRtfString");
            this.xrRichText1.SizeF = new System.Drawing.SizeF(725.9999F, 242.1805F);
            this.xrRichText1.StylePriority.UseFont = false;
            this.xrRichText1.StylePriority.UseForeColor = false;
            // 
            // ReportFooter
            // 
            this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrRichText2});
            this.ReportFooter.HeightF = 421.75F;
            this.ReportFooter.Name = "ReportFooter";
            // 
            // xrRichText2
            // 
            this.xrRichText2.Font = new System.Drawing.Font("Arial", 9F);
            this.xrRichText2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 10.00001F);
            this.xrRichText2.Name = "xrRichText2";
            this.xrRichText2.SerializableRtfString = resources.GetString("xrRichText2.SerializableRtfString");
            this.xrRichText2.SizeF = new System.Drawing.SizeF(725.9999F, 401.75F);
            this.xrRichText2.StylePriority.UseFont = false;
            // 
            // NgayIn
            // 
            this.NgayIn.Description = "Ngày in giấy báo";
            this.NgayIn.Name = "NgayIn";
            this.NgayIn.Type = typeof(System.DateTime);
            this.NgayIn.Value = new System.DateTime(2016, 2, 28, 23, 59, 4, 97);
            this.NgayIn.Visible = false;
            // 
            // ThangTB
            // 
            this.ThangTB.Description = "Tháng thông báo phí";
            this.ThangTB.Name = "ThangTB";
            this.ThangTB.Value = "";
            this.ThangTB.Visible = false;
            // 
            // rptGiayBaoThangLongBlank
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.ReportHeader,
            this.ReportFooter});
            this.Font = new System.Drawing.Font("Arial", 9F);
            this.Margins = new System.Drawing.Printing.Margins(50, 50, 33, 50);
            this.PageHeight = 1169;
            this.PageWidth = 827;
            this.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[] {
            this.NgayIn,
            this.ThangTB});
            this.RequestParameters = false;
            this.Version = "12.1";
            ((System.ComponentModel.ISupportInitialize)(this.rtTenLDV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrRichText1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrRichText2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.ReportHeaderBand ReportHeader;
        private DevExpress.XtraReports.UI.ReportFooterBand ReportFooter;
        private DevExpress.XtraReports.Parameters.Parameter NgayIn;
        private DevExpress.XtraReports.Parameters.Parameter ThangTB;
        private DevExpress.XtraReports.UI.XRRichText rtTenLDV;
        private DevExpress.XtraReports.UI.XRRichText xrRichText1;
        private DevExpress.XtraReports.UI.XRRichText xrRichText2;
    }
}
