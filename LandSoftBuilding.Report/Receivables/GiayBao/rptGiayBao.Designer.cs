namespace LandSoftBuilding.Receivables.GiayBao
{
    partial class rptGiayBao
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(rptGiayBao));
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.rtTenLDV = new DevExpress.XtraReports.UI.XRRichText();
            this.rptDichVu = new DevExpress.XtraReports.UI.XRSubreport();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.rtHeader = new DevExpress.XtraReports.UI.XRRichText();
            this.imgLogo = new DevExpress.XtraReports.UI.XRPictureBox();
            this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow6 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
            this.cNgayIn = new DevExpress.XtraReports.UI.XRTableCell();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.rtFooter = new DevExpress.XtraReports.UI.XRRichText();
            this.xrTable5 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow7 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell18 = new DevExpress.XtraReports.UI.XRTableCell();
            this.cSumSoTien = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow23 = new DevExpress.XtraReports.UI.XRTableRow();
            this.csumNoCu_TieuDe = new DevExpress.XtraReports.UI.XRTableCell();
            this.csumNoCu = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow4 = new DevExpress.XtraReports.UI.XRTableRow();
            this.csumThuTruoc_TieuDe = new DevExpress.XtraReports.UI.XRTableCell();
            this.csumThuTruoc = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow24 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell29 = new DevExpress.XtraReports.UI.XRTableCell();
            this.csumTongTien = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow8 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell16 = new DevExpress.XtraReports.UI.XRTableCell();
            this.cSoTienBangChu = new DevExpress.XtraReports.UI.XRTableCell();
            this.NgayIn = new DevExpress.XtraReports.Parameters.Parameter();
            this.ThangTB = new DevExpress.XtraReports.Parameters.Parameter();
            this.xrTableRow5 = new DevExpress.XtraReports.UI.XRTableRow();
            this.cTienLai_DienGiai = new DevExpress.XtraReports.UI.XRTableCell();
            this.cTienLai = new DevExpress.XtraReports.UI.XRTableCell();
            ((System.ComponentModel.ISupportInitialize)(this.rtTenLDV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtHeader)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
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
            this.rtTenLDV.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
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
            this.rptDichVu.SizeF = new System.Drawing.SizeF(727F, 23F);
            this.rptDichVu.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.rptDichVu_BeforePrint);
            // 
            // TopMargin
            // 
            this.TopMargin.HeightF = 50F;
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
            this.rtHeader,
            this.imgLogo,
            this.xrTable1});
            this.ReportHeader.HeightF = 252.1805F;
            this.ReportHeader.Name = "ReportHeader";
            // 
            // rtHeader
            // 
            this.rtHeader.Font = new System.Drawing.Font("Arial", 9F);
            this.rtHeader.LocationFloat = new DevExpress.Utils.PointFloat(0.9999831F, 88.1389F);
            this.rtHeader.Name = "rtHeader";
            this.rtHeader.SerializableRtfString = resources.GetString("rtHeader.SerializableRtfString");
            this.rtHeader.SizeF = new System.Drawing.SizeF(725.9999F, 164.0416F);
            // 
            // imgLogo
            // 
            this.imgLogo.LocationFloat = new DevExpress.Utils.PointFloat(2.384186E-05F, 0F);
            this.imgLogo.Name = "imgLogo";
            this.imgLogo.SizeF = new System.Drawing.SizeF(94.79166F, 70.13889F);
            this.imgLogo.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
            // 
            // xrTable1
            // 
            this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(173.9287F, 0F);
            this.xrTable1.Name = "xrTable1";
            this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1,
            this.xrTableRow2,
            this.xrTableRow6,
            this.xrTableRow3});
            this.xrTable1.SizeF = new System.Drawing.SizeF(380.6066F, 70.13889F);
            this.xrTable1.StylePriority.UseTextAlignment = false;
            this.xrTable1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell3});
            this.xrTableRow1.Name = "xrTableRow1";
            this.xrTableRow1.Weight = 1D;
            // 
            // xrTableCell3
            // 
            this.xrTableCell3.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCell3.Multiline = true;
            this.xrTableCell3.Name = "xrTableCell3";
            this.xrTableCell3.StylePriority.UseFont = false;
            this.xrTableCell3.Text = "CỘNG HÒA XÃ HỘI CHỦ NGHĨA VIỆT NAM\r\n";
            this.xrTableCell3.Weight = 0.79797110367346014D;
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell4});
            this.xrTableRow2.Name = "xrTableRow2";
            this.xrTableRow2.Weight = 1D;
            // 
            // xrTableCell4
            // 
            this.xrTableCell4.Font = new System.Drawing.Font("Arial", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            this.xrTableCell4.Name = "xrTableCell4";
            this.xrTableCell4.StylePriority.UseFont = false;
            this.xrTableCell4.Text = "Độc lập - Tự do - Hạnh phúc";
            this.xrTableCell4.Weight = 0.79797110367346014D;
            // 
            // xrTableRow6
            // 
            this.xrTableRow6.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell1});
            this.xrTableRow6.Name = "xrTableRow6";
            this.xrTableRow6.Weight = 1D;
            // 
            // xrTableCell1
            // 
            this.xrTableCell1.Name = "xrTableCell1";
            this.xrTableCell1.Text = "---oOo---";
            this.xrTableCell1.Weight = 0.79797110367346014D;
            // 
            // xrTableRow3
            // 
            this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.cNgayIn});
            this.xrTableRow3.Name = "xrTableRow3";
            this.xrTableRow3.Weight = 1D;
            // 
            // cNgayIn
            // 
            this.cNgayIn.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Italic);
            this.cNgayIn.Name = "cNgayIn";
            this.cNgayIn.StylePriority.UseFont = false;
            this.cNgayIn.StylePriority.UseTextAlignment = false;
            this.cNgayIn.Text = "HCM, ngày {0:dd} tháng {0:MM} năm {0:yyyy}";
            this.cNgayIn.Weight = 0.79797110367346014D;
            // 
            // ReportFooter
            // 
            this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.rtFooter,
            this.xrTable5});
            this.ReportFooter.HeightF = 347.7917F;
            this.ReportFooter.Name = "ReportFooter";
            // 
            // rtFooter
            // 
            this.rtFooter.Font = new System.Drawing.Font("Arial", 9F);
            this.rtFooter.LocationFloat = new DevExpress.Utils.PointFloat(1.000142F, 135.8334F);
            this.rtFooter.Name = "rtFooter";
            this.rtFooter.SerializableRtfString = resources.GetString("rtFooter.SerializableRtfString");
            this.rtFooter.SizeF = new System.Drawing.SizeF(725.9999F, 211.9583F);
            // 
            // xrTable5
            // 
            this.xrTable5.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.xrTable5.LocationFloat = new DevExpress.Utils.PointFloat(0F, 8.333344F);
            this.xrTable5.Name = "xrTable5";
            this.xrTable5.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow5,
            this.xrTableRow7,
            this.xrTableRow23,
            this.xrTableRow4,
            this.xrTableRow24,
            this.xrTableRow8});
            this.xrTable5.SizeF = new System.Drawing.SizeF(727F, 117.1875F);
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
            this.xrTableCell18.Text = "Tổng phát sinh trong tháng/ Grand total ";
            this.xrTableCell18.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell18.Weight = 2.5343877800229162D;
            // 
            // cSumSoTien
            // 
            this.cSumSoTien.Name = "cSumSoTien";
            this.cSumSoTien.StylePriority.UseBorders = false;
            this.cSumSoTien.StylePriority.UseTextAlignment = false;
            this.cSumSoTien.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.cSumSoTien.Weight = 0.46561221997708374D;
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
            this.csumNoCu_TieuDe.Text = "Tổng nợ cũ chuyển sang / Previous debt";
            this.csumNoCu_TieuDe.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.csumNoCu_TieuDe.Weight = 2.5343877800229162D;
            // 
            // csumNoCu
            // 
            this.csumNoCu.Name = "csumNoCu";
            this.csumNoCu.StylePriority.UseBorders = false;
            this.csumNoCu.StylePriority.UseTextAlignment = false;
            this.csumNoCu.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.csumNoCu.Weight = 0.46561221997708374D;
            // 
            // xrTableRow4
            // 
            this.xrTableRow4.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.csumThuTruoc_TieuDe,
            this.csumThuTruoc});
            this.xrTableRow4.Name = "xrTableRow4";
            this.xrTableRow4.Weight = 1D;
            // 
            // csumThuTruoc_TieuDe
            // 
            this.csumThuTruoc_TieuDe.Name = "csumThuTruoc_TieuDe";
            this.csumThuTruoc_TieuDe.Text = "Tổng tiền đã thu trước / Collected in advance";
            this.csumThuTruoc_TieuDe.Weight = 2.5343877800229162D;
            // 
            // csumThuTruoc
            // 
            this.csumThuTruoc.Name = "csumThuTruoc";
            this.csumThuTruoc.StylePriority.UseTextAlignment = false;
            this.csumThuTruoc.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.csumThuTruoc.Weight = 0.46561221997708374D;
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
            this.xrTableCell29.Name = "xrTableCell29";
            this.xrTableCell29.StylePriority.UseTextAlignment = false;
            this.xrTableCell29.Text = "Tổng tiền phải thanh toán / Grand total";
            this.xrTableCell29.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell29.Weight = 2.5343877800229162D;
            // 
            // csumTongTien
            // 
            this.csumTongTien.Name = "csumTongTien";
            this.csumTongTien.StylePriority.UseBorders = false;
            this.csumTongTien.StylePriority.UseTextAlignment = false;
            this.csumTongTien.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.csumTongTien.Weight = 0.46561221997708374D;
            // 
            // xrTableRow8
            // 
            this.xrTableRow8.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell16,
            this.cSoTienBangChu});
            this.xrTableRow8.Font = new System.Drawing.Font("Times New Roman", 13F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.xrTableRow8.Name = "xrTableRow8";
            this.xrTableRow8.StylePriority.UseFont = false;
            this.xrTableRow8.Weight = 1D;
            // 
            // xrTableCell16
            // 
            this.xrTableCell16.Font = new System.Drawing.Font("Arial", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.xrTableCell16.Name = "xrTableCell16";
            this.xrTableCell16.StylePriority.UseBorders = false;
            this.xrTableCell16.StylePriority.UseFont = false;
            this.xrTableCell16.Text = "Số tiền phải thanh toán bằng chữ: ";
            this.xrTableCell16.Weight = 0.88394063243839749D;
            // 
            // cSoTienBangChu
            // 
            this.cSoTienBangChu.Font = new System.Drawing.Font("Arial", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.cSoTienBangChu.Name = "cSoTienBangChu";
            this.cSoTienBangChu.StylePriority.UseBorders = false;
            this.cSoTienBangChu.StylePriority.UseFont = false;
            this.cSoTienBangChu.Weight = 2.1160593675616024D;
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
            // xrTableRow5
            // 
            this.xrTableRow5.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.cTienLai_DienGiai,
            this.cTienLai});
            this.xrTableRow5.Name = "xrTableRow5";
            this.xrTableRow5.Weight = 1D;
            // 
            // cTienLai_DienGiai
            // 
            this.cTienLai_DienGiai.Name = "cTienLai_DienGiai";
            this.cTienLai_DienGiai.Text = "Tiền lãi";
            this.cTienLai_DienGiai.Weight = 2.5343877800229162D;
            // 
            // cTienLai
            // 
            this.cTienLai.Name = "cTienLai";
            this.cTienLai.StylePriority.UseTextAlignment = false;
            this.cTienLai.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.cTienLai.Weight = 0.46561221997708374D;
            // 
            // rptGiayBao
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.ReportHeader,
            this.ReportFooter});
            this.Font = new System.Drawing.Font("Arial", 9F);
            this.Margins = new System.Drawing.Printing.Margins(50, 50, 50, 50);
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
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
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
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow8;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell16;
        private DevExpress.XtraReports.UI.XRTableCell cSoTienBangChu;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow23;
        private DevExpress.XtraReports.UI.XRTableCell csumNoCu_TieuDe;
        private DevExpress.XtraReports.UI.XRTableCell csumNoCu;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow24;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell29;
        private DevExpress.XtraReports.UI.XRTableCell csumTongTien;
        private DevExpress.XtraReports.UI.XRSubreport rptDichVu;
        private DevExpress.XtraReports.UI.XRTable xrTable1;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow1;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell3;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow2;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell4;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow3;
        private DevExpress.XtraReports.UI.XRTableCell cNgayIn;
        private DevExpress.XtraReports.UI.XRPictureBox imgLogo;
        private DevExpress.XtraReports.UI.XRRichText rtHeader;
        private DevExpress.XtraReports.UI.XRRichText rtFooter;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow6;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell1;
        private DevExpress.XtraReports.UI.XRRichText rtTenLDV;
        private DevExpress.XtraReports.Parameters.Parameter NgayIn;
        private DevExpress.XtraReports.Parameters.Parameter ThangTB;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow4;
        private DevExpress.XtraReports.UI.XRTableCell csumThuTruoc_TieuDe;
        private DevExpress.XtraReports.UI.XRTableCell csumThuTruoc;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow5;
        private DevExpress.XtraReports.UI.XRTableCell cTienLai_DienGiai;
        private DevExpress.XtraReports.UI.XRTableCell cTienLai;
    }
}
