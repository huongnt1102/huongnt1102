namespace LandSoftBuilding.Receivables.GiayBao
{
    partial class rptGiayBaoImperia
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(rptGiayBaoImperia));
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.rptDichVu = new DevExpress.XtraReports.UI.XRSubreport();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.rtHeader = new DevExpress.XtraReports.UI.XRRichText();
            this.imgLogo = new DevExpress.XtraReports.UI.XRPictureBox();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.xrRichText1 = new DevExpress.XtraReports.UI.XRRichText();
            this.xrTable5 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow24 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell29 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.csumTongTien = new DevExpress.XtraReports.UI.XRTableCell();
            this.NgayIn = new DevExpress.XtraReports.Parameters.Parameter();
            this.ThangTB = new DevExpress.XtraReports.Parameters.Parameter();
            ((System.ComponentModel.ISupportInitialize)(this.rtHeader)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrRichText1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.rptDichVu});
            this.Detail.HeightF = 34.6667F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // rptDichVu
            // 
            this.rptDichVu.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.rptDichVu.Name = "rptDichVu";
            this.rptDichVu.SizeF = new System.Drawing.SizeF(726F, 23F);
            this.rptDichVu.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.rptDichVu_BeforePrint);
            // 
            // TopMargin
            // 
            this.TopMargin.HeightF = 33F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // BottomMargin
            // 
            this.BottomMargin.HeightF = 0F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // ReportHeader
            // 
            this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.rtHeader,
            this.imgLogo});
            this.ReportHeader.HeightF = 200.8472F;
            this.ReportHeader.Name = "ReportHeader";
            // 
            // rtHeader
            // 
            this.rtHeader.Font = new System.Drawing.Font("Arial", 9F);
            this.rtHeader.LocationFloat = new DevExpress.Utils.PointFloat(1.000142F, 70.1389F);
            this.rtHeader.Name = "rtHeader";
            this.rtHeader.SerializableRtfString = resources.GetString("rtHeader.SerializableRtfString");
            this.rtHeader.SizeF = new System.Drawing.SizeF(725.9999F, 130.7083F);
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
            this.xrRichText1,
            this.xrTable5});
            this.ReportFooter.HeightF = 50.86457F;
            this.ReportFooter.Name = "ReportFooter";
            // 
            // xrRichText1
            // 
            this.xrRichText1.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.xrRichText1.LocationFloat = new DevExpress.Utils.PointFloat(0.0001192093F, 27.86458F);
            this.xrRichText1.Name = "xrRichText1";
            this.xrRichText1.SerializableRtfString = resources.GetString("xrRichText1.SerializableRtfString");
            this.xrRichText1.SizeF = new System.Drawing.SizeF(725.9999F, 22.99999F);
            this.xrRichText1.StylePriority.UseFont = false;
            // 
            // xrTable5
            // 
            this.xrTable5.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold);
            this.xrTable5.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable5.Name = "xrTable5";
            this.xrTable5.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow24});
            this.xrTable5.SizeF = new System.Drawing.SizeF(727.2748F, 27.86458F);
            this.xrTable5.StylePriority.UseBorders = false;
            this.xrTable5.StylePriority.UseFont = false;
            this.xrTable5.StylePriority.UsePadding = false;
            this.xrTable5.StylePriority.UseTextAlignment = false;
            this.xrTable5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrTableRow24
            // 
            this.xrTableRow24.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell29,
            this.xrTableCell1,
            this.csumTongTien});
            this.xrTableRow24.Name = "xrTableRow24";
            this.xrTableRow24.Weight = 1D;
            // 
            // xrTableCell29
            // 
            this.xrTableCell29.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCell29.Multiline = true;
            this.xrTableCell29.Name = "xrTableCell29";
            this.xrTableCell29.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100F);
            this.xrTableCell29.StylePriority.UseBorders = false;
            this.xrTableCell29.StylePriority.UsePadding = false;
            this.xrTableCell29.StylePriority.UseTextAlignment = false;
            this.xrTableCell29.Text = "Tổng cộng thanh toán/ Total payment ";
            this.xrTableCell29.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell29.Weight = 0.9101271834086675D;
            // 
            // xrTableCell1
            // 
            this.xrTableCell1.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCell1.Name = "xrTableCell1";
            this.xrTableCell1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100F);
            this.xrTableCell1.StylePriority.UseBorders = false;
            this.xrTableCell1.StylePriority.UsePadding = false;
            this.xrTableCell1.StylePriority.UseTextAlignment = false;
            this.xrTableCell1.Text = " I + II + III + IV";
            this.xrTableCell1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell1.Weight = 1.5608786742181586D;
            // 
            // csumTongTien
            // 
            this.csumTongTien.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.csumTongTien.Name = "csumTongTien";
            this.csumTongTien.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 7, 0, 0, 100F);
            this.csumTongTien.StylePriority.UseBorders = false;
            this.csumTongTien.StylePriority.UsePadding = false;
            this.csumTongTien.StylePriority.UseTextAlignment = false;
            this.csumTongTien.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.csumTongTien.Weight = 0.53279826171706168D;
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
            // rptGiayBaoImperia
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.ReportHeader,
            this.ReportFooter});
            this.Font = new System.Drawing.Font("Arial", 9F);
            this.Margins = new System.Drawing.Printing.Margins(50, 45, 33, 0);
            this.PageHeight = 1169;
            this.PageWidth = 827;
            this.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[] {
            this.NgayIn,
            this.ThangTB});
            this.RequestParameters = false;
            this.Version = "12.1";
            ((System.ComponentModel.ISupportInitialize)(this.rtHeader)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrRichText1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.ReportHeaderBand ReportHeader;
        private DevExpress.XtraReports.UI.ReportFooterBand ReportFooter;
        private DevExpress.XtraReports.UI.XRTable xrTable5;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow24;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell29;
        private DevExpress.XtraReports.UI.XRTableCell csumTongTien;
        private DevExpress.XtraReports.UI.XRSubreport rptDichVu;
        private DevExpress.XtraReports.UI.XRPictureBox imgLogo;
        private DevExpress.XtraReports.UI.XRRichText rtHeader;
        private DevExpress.XtraReports.Parameters.Parameter NgayIn;
        private DevExpress.XtraReports.Parameters.Parameter ThangTB;
        private DevExpress.XtraReports.UI.XRRichText xrRichText1;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell1;
    }
}
