namespace LandSoftBuilding.Receivables.GiayBao
{
    partial class rptGiayBaoImperiaXe
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(rptGiayBaoImperiaXe));
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.rtTenLDV = new DevExpress.XtraReports.UI.XRRichText();
            this.rptDichVu = new DevExpress.XtraReports.UI.XRSubreport();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.rtHeader = new DevExpress.XtraReports.UI.XRRichText();
            this.imgLogo = new DevExpress.XtraReports.UI.XRPictureBox();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.xrRichText1 = new DevExpress.XtraReports.UI.XRRichText();
            this.NgayIn = new DevExpress.XtraReports.Parameters.Parameter();
            this.ThangTB = new DevExpress.XtraReports.Parameters.Parameter();
            this.xrRichText2 = new DevExpress.XtraReports.UI.XRRichText();
            ((System.ComponentModel.ISupportInitialize)(this.rtTenLDV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtHeader)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrRichText1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrRichText2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.rtTenLDV,
            this.rptDichVu});
            this.Detail.HeightF = 50.2917F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // rtTenLDV
            // 
            this.rtTenLDV.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.rtTenLDV.LocationFloat = new DevExpress.Utils.PointFloat(0.9999831F, 0F);
            this.rtTenLDV.Name = "rtTenLDV";
            this.rtTenLDV.SerializableRtfString = resources.GetString("rtTenLDV.SerializableRtfString");
            this.rtTenLDV.SizeF = new System.Drawing.SizeF(725.9999F, 23F);
            this.rtTenLDV.StylePriority.UseFont = false;
            this.rtTenLDV.Visible = false;
            this.rtTenLDV.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.rtTenLDV_BeforePrint);
            // 
            // rptDichVu
            // 
            this.rptDichVu.LocationFloat = new DevExpress.Utils.PointFloat(2.384186E-05F, 20F);
            this.rptDichVu.Name = "rptDichVu";
            this.rptDichVu.SizeF = new System.Drawing.SizeF(726F, 23F);
            this.rptDichVu.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.rptDichVu_BeforePrint);
            // 
            // TopMargin
            // 
            this.TopMargin.HeightF = 15.29166F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // BottomMargin
            // 
            this.BottomMargin.HeightF = 10.58337F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // ReportHeader
            // 
            this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.rtHeader,
            this.imgLogo});
            this.ReportHeader.HeightF = 188.3472F;
            this.ReportHeader.Name = "ReportHeader";
            // 
            // rtHeader
            // 
            this.rtHeader.Font = new System.Drawing.Font("Arial", 9F);
            this.rtHeader.LocationFloat = new DevExpress.Utils.PointFloat(1.00015F, 70.1389F);
            this.rtHeader.Name = "rtHeader";
            this.rtHeader.SerializableRtfString = resources.GetString("rtHeader.SerializableRtfString");
            this.rtHeader.SizeF = new System.Drawing.SizeF(725.9999F, 118.2083F);
            // 
            // imgLogo
            // 
            this.imgLogo.Image = ((System.Drawing.Image)(resources.GetObject("imgLogo.Image")));
            this.imgLogo.LocationFloat = new DevExpress.Utils.PointFloat(320.8334F, 0F);
            this.imgLogo.Name = "imgLogo";
            this.imgLogo.SizeF = new System.Drawing.SizeF(94.79166F, 70.13889F);
            this.imgLogo.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
            // 
            // ReportFooter
            // 
            this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrRichText2,
            this.xrRichText1});
            this.ReportFooter.HeightF = 534.4583F;
            this.ReportFooter.Name = "ReportFooter";
            // 
            // xrRichText1
            // 
            this.xrRichText1.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.xrRichText1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 142.7084F);
            this.xrRichText1.Name = "xrRichText1";
            this.xrRichText1.Padding = new DevExpress.XtraPrinting.PaddingInfo(20, 0, 0, 0, 100F);
            this.xrRichText1.SerializableRtfString = resources.GetString("xrRichText1.SerializableRtfString");
            this.xrRichText1.SizeF = new System.Drawing.SizeF(754.9999F, 391.7499F);
            this.xrRichText1.StylePriority.UseFont = false;
            this.xrRichText1.StylePriority.UsePadding = false;
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
            // xrRichText2
            // 
            this.xrRichText2.Font = new System.Drawing.Font("Arial", 9F);
            this.xrRichText2.LocationFloat = new DevExpress.Utils.PointFloat(1.00015F, 0F);
            this.xrRichText2.Name = "xrRichText2";
            this.xrRichText2.SerializableRtfString = resources.GetString("xrRichText2.SerializableRtfString");
            this.xrRichText2.SizeF = new System.Drawing.SizeF(753.9998F, 142.7084F);
            // 
            // rptGiayBaoImperiaXe
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.ReportHeader,
            this.ReportFooter});
            this.Font = new System.Drawing.Font("Arial", 9F);
            this.Margins = new System.Drawing.Printing.Margins(35, 7, 15, 11);
            this.PageHeight = 1169;
            this.PageWidth = 827;
            this.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[] {
            this.NgayIn,
            this.ThangTB});
            this.RequestParameters = false;
            this.Version = "12.1";
            ((System.ComponentModel.ISupportInitialize)(this.rtTenLDV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtHeader)).EndInit();
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
        private DevExpress.XtraReports.UI.XRSubreport rptDichVu;
        private DevExpress.XtraReports.UI.XRPictureBox imgLogo;
        private DevExpress.XtraReports.UI.XRRichText rtHeader;
        private DevExpress.XtraReports.UI.XRRichText rtTenLDV;
        private DevExpress.XtraReports.Parameters.Parameter NgayIn;
        private DevExpress.XtraReports.Parameters.Parameter ThangTB;
        private DevExpress.XtraReports.UI.XRRichText xrRichText1;
        private DevExpress.XtraReports.UI.XRRichText xrRichText2;
    }
}
