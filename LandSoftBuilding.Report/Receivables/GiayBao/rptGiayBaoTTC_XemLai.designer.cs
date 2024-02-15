namespace LandSoftBuilding.Receivables.GiayBao
{
    partial class rptGiayBaoTTC_XemLai
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(rptGiayBaoTTC));
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.rtTenLDV = new DevExpress.XtraReports.UI.XRRichText();
            this.rptDichVu = new DevExpress.XtraReports.UI.XRSubreport();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.rtHeader = new DevExpress.XtraReports.UI.XRRichText();
            this.imgLogo = new DevExpress.XtraReports.UI.XRPictureBox();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.cSoThanhtoan = new DevExpress.XtraReports.UI.XRLabel();
            this.rtFooter = new DevExpress.XtraReports.UI.XRRichText();
            this.xrTable5 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow7 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell18 = new DevExpress.XtraReports.UI.XRTableCell();
            this.cSumSoTien = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow23 = new DevExpress.XtraReports.UI.XRTableRow();
            this.csumNoCu_TieuDe = new DevExpress.XtraReports.UI.XRTableCell();
            this.csumNoCu = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow24 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell29 = new DevExpress.XtraReports.UI.XRTableCell();
            this.csumTongTien = new DevExpress.XtraReports.UI.XRTableCell();
            this.NgayIn = new DevExpress.XtraReports.Parameters.Parameter();
            this.ThangTB = new DevExpress.XtraReports.Parameters.Parameter();
            ((System.ComponentModel.ISupportInitialize)(this.rtTenLDV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtHeader)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtFooter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable5)).BeginInit();
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
            this.rtTenLDV.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold);
            this.rtTenLDV.LocationFloat = new DevExpress.Utils.PointFloat(0.9999831F, 0F);
            this.rtTenLDV.Name = "rtTenLDV";
            this.rtTenLDV.SerializableRtfString = resources.GetString("rtTenLDV.SerializableRtfString");
            this.rtTenLDV.SizeF = new System.Drawing.SizeF(725.9999F, 23F);
            this.rtTenLDV.StylePriority.UseFont = false;
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
            this.TopMargin.HeightF = 33.33333F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // BottomMargin
            // 
            this.BottomMargin.HeightF = 57.375F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // ReportHeader
            // 
            this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.rtHeader,
            this.imgLogo});
            this.ReportHeader.HeightF = 154.2638F;
            this.ReportHeader.Name = "ReportHeader";
            // 
            // rtHeader
            // 
            this.rtHeader.Font = new System.Drawing.Font("Arial", 9F);
            this.rtHeader.LocationFloat = new DevExpress.Utils.PointFloat(1.000142F, 0F);
            this.rtHeader.Name = "rtHeader";
            this.rtHeader.SerializableRtfString = resources.GetString("rtHeader.SerializableRtfString");
            this.rtHeader.SizeF = new System.Drawing.SizeF(725.9999F, 154.2638F);
            // 
            // imgLogo
            // 
            this.imgLogo.LocationFloat = new DevExpress.Utils.PointFloat(2.384186E-05F, 0F);
            this.imgLogo.Name = "imgLogo";
            this.imgLogo.SizeF = new System.Drawing.SizeF(160.4167F, 88.1389F);
            this.imgLogo.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
            // 
            // ReportFooter
            // 
            this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.cSoThanhtoan,
            this.rtFooter,
            this.xrTable5});
            this.ReportFooter.HeightF = 229.0417F;
            this.ReportFooter.Name = "ReportFooter";
            // 
            // cSoThanhtoan
            // 
            this.cSoThanhtoan.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold);
            this.cSoThanhtoan.LocationFloat = new DevExpress.Utils.PointFloat(1.00015F, 0F);
            this.cSoThanhtoan.Name = "cSoThanhtoan";
            this.cSoThanhtoan.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100F);
            this.cSoThanhtoan.SizeF = new System.Drawing.SizeF(724.9999F, 17.23963F);
            this.cSoThanhtoan.StylePriority.UseFont = false;
            this.cSoThanhtoan.StylePriority.UsePadding = false;
            this.cSoThanhtoan.StylePriority.UseTextAlignment = false;
            this.cSoThanhtoan.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // rtFooter
            // 
            this.rtFooter.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.rtFooter.LocationFloat = new DevExpress.Utils.PointFloat(1.00015F, 85.83339F);
            this.rtFooter.Name = "rtFooter";
            this.rtFooter.SerializableRtfString = resources.GetString("rtFooter.SerializableRtfString");
            this.rtFooter.SizeF = new System.Drawing.SizeF(725.9999F, 143.2083F);
            this.rtFooter.StylePriority.UseFont = false;
            // 
            // xrTable5
            // 
            this.xrTable5.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            this.xrTable5.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold);
            this.xrTable5.LocationFloat = new DevExpress.Utils.PointFloat(1.00015F, 27.23964F);
            this.xrTable5.Name = "xrTable5";
            this.xrTable5.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow7,
            this.xrTableRow23,
            this.xrTableRow24});
            this.xrTable5.SizeF = new System.Drawing.SizeF(725.9999F, 58.59375F);
            this.xrTable5.StylePriority.UseBorders = false;
            this.xrTable5.StylePriority.UseFont = false;
            this.xrTable5.StylePriority.UsePadding = false;
            this.xrTable5.StylePriority.UseTextAlignment = false;
            this.xrTable5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrTableRow7
            // 
            this.xrTableRow7.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell18,
            this.cSumSoTien});
            this.xrTableRow7.Name = "xrTableRow7";
            this.xrTableRow7.Weight = 1D;
            // 
            // xrTableCell18
            // 
            this.xrTableCell18.Name = "xrTableCell18";
            this.xrTableCell18.StylePriority.UseTextAlignment = false;
            this.xrTableCell18.Text = "Tổng phát sinh tháng/Total monthly accured expense";
            this.xrTableCell18.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell18.Weight = 2.3791939549588914D;
            // 
            // cSumSoTien
            // 
            this.cSumSoTien.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right)));
            this.cSumSoTien.Name = "cSumSoTien";
            this.cSumSoTien.StylePriority.UseBorders = false;
            this.cSumSoTien.StylePriority.UseTextAlignment = false;
            this.cSumSoTien.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.cSumSoTien.Weight = 0.62080604504110881D;
            // 
            // xrTableRow23
            // 
            this.xrTableRow23.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.csumNoCu_TieuDe,
            this.csumNoCu});
            this.xrTableRow23.Name = "xrTableRow23";
            this.xrTableRow23.Weight = 1D;
            // 
            // csumNoCu_TieuDe
            // 
            this.csumNoCu_TieuDe.Name = "csumNoCu_TieuDe";
            this.csumNoCu_TieuDe.StylePriority.UseTextAlignment = false;
            this.csumNoCu_TieuDe.Text = "Nợ trước/Previous debt";
            this.csumNoCu_TieuDe.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.csumNoCu_TieuDe.Weight = 2.3791939549588914D;
            // 
            // csumNoCu
            // 
            this.csumNoCu.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right)));
            this.csumNoCu.Name = "csumNoCu";
            this.csumNoCu.StylePriority.UseBorders = false;
            this.csumNoCu.StylePriority.UseTextAlignment = false;
            this.csumNoCu.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.csumNoCu.Weight = 0.62080604504110881D;
            // 
            // xrTableRow24
            // 
            this.xrTableRow24.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell29,
            this.csumTongTien});
            this.xrTableRow24.Name = "xrTableRow24";
            this.xrTableRow24.Weight = 1D;
            // 
            // xrTableCell29
            // 
            this.xrTableCell29.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell29.Name = "xrTableCell29";
            this.xrTableCell29.StylePriority.UseBorders = false;
            this.xrTableCell29.StylePriority.UseTextAlignment = false;
            this.xrTableCell29.Text = "Tổng thanh toán /Total payment";
            this.xrTableCell29.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell29.Weight = 2.3791939549588914D;
            // 
            // csumTongTien
            // 
            this.csumTongTien.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.csumTongTien.Name = "csumTongTien";
            this.csumTongTien.StylePriority.UseBorders = false;
            this.csumTongTien.StylePriority.UseTextAlignment = false;
            this.csumTongTien.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.csumTongTien.Weight = 0.62080604504110881D;
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
            // rptGiayBaoTTC
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.ReportHeader,
            this.ReportFooter});
            this.Font = new System.Drawing.Font("Arial", 9F);
            this.Margins = new System.Drawing.Printing.Margins(50, 50, 33, 57);
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
            ((System.ComponentModel.ISupportInitialize)(this.rtFooter)).EndInit();
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
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow7;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell18;
        private DevExpress.XtraReports.UI.XRTableCell cSumSoTien;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow23;
        private DevExpress.XtraReports.UI.XRTableCell csumNoCu_TieuDe;
        private DevExpress.XtraReports.UI.XRTableCell csumNoCu;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow24;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell29;
        private DevExpress.XtraReports.UI.XRTableCell csumTongTien;
        private DevExpress.XtraReports.UI.XRSubreport rptDichVu;
        private DevExpress.XtraReports.UI.XRPictureBox imgLogo;
        private DevExpress.XtraReports.UI.XRRichText rtHeader;
        private DevExpress.XtraReports.UI.XRRichText rtFooter;
        private DevExpress.XtraReports.UI.XRRichText rtTenLDV;
        private DevExpress.XtraReports.Parameters.Parameter NgayIn;
        private DevExpress.XtraReports.Parameters.Parameter ThangTB;
        private DevExpress.XtraReports.UI.XRLabel cSoThanhtoan;
    }
}
