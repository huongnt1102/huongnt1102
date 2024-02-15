namespace DichVu.GiuXe.Reports
{
    partial class rptCongNoTheXe
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
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.pvCongNo = new DevExpress.XtraReports.UI.XRPivotGrid();
            this.colMaKH = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.colTenKH = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.colKyBC = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.colLDV = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.colSoTien = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.tblHeader = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.cThoiGian = new DevExpress.XtraReports.UI.XRTableCell();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.tblFooter = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow61 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell91 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell94 = new DevExpress.XtraReports.UI.XRTableCell();
            ((System.ComponentModel.ISupportInitialize)(this.tblHeader)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblFooter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.pvCongNo});
            this.Detail.HeightF = 78.12503F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // pvCongNo
            // 
            this.pvCongNo.Appearance.FieldHeader.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.pvCongNo.Appearance.FieldHeader.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.pvCongNo.Appearance.FieldValue.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.pvCongNo.Appearance.FieldValue.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.pvCongNo.Appearance.FieldValueGrandTotal.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.pvCongNo.Appearance.FieldValueGrandTotal.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.pvCongNo.Fields.AddRange(new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField[] {
            this.colMaKH,
            this.colTenKH,
            this.colKyBC,
            this.colLDV,
            this.colSoTien});
            this.pvCongNo.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.pvCongNo.Name = "pvCongNo";
            this.pvCongNo.OptionsChartDataSource.UpdateDelay = 300;
            this.pvCongNo.OptionsView.ShowColumnHeaders = false;
            this.pvCongNo.OptionsView.ShowDataHeaders = false;
            this.pvCongNo.OptionsView.ShowFilterHeaders = false;
            this.pvCongNo.SizeF = new System.Drawing.SizeF(1069F, 78.12503F);
            // 
            // colMaKH
            // 
            this.colMaKH.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.colMaKH.AreaIndex = 0;
            this.colMaKH.Caption = "Mã khách hàng";
            this.colMaKH.FieldName = "MaKH";
            this.colMaKH.GrandTotalText = "Tổng cộng";
            this.colMaKH.Name = "colMaKH";
            // 
            // colTenKH
            // 
            this.colTenKH.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.colTenKH.AreaIndex = 1;
            this.colTenKH.Caption = "Tên khách hàng";
            this.colTenKH.FieldName = "TenKH";
            this.colTenKH.GrandTotalText = "Tổng cộng";
            this.colTenKH.Name = "colTenKH";
            this.colTenKH.Width = 200;
            // 
            // colKyBC
            // 
            this.colKyBC.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea;
            this.colKyBC.AreaIndex = 0;
            this.colKyBC.Caption = "Công nợ";
            this.colKyBC.FieldName = "KyBC";
            this.colKyBC.GrandTotalText = "Tổng cộng";
            this.colKyBC.Name = "colKyBC";
            // 
            // colLDV
            // 
            this.colLDV.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea;
            this.colLDV.AreaIndex = 1;
            this.colLDV.Caption = "Loại dịch vụ";
            this.colLDV.FieldName = "TenLDV";
            this.colLDV.GrandTotalText = "Tổng cộng";
            this.colLDV.Name = "colLDV";
            this.colLDV.Width = 80;
            // 
            // colSoTien
            // 
            this.colSoTien.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.colSoTien.AreaIndex = 0;
            this.colSoTien.Caption = "Số tiền";
            this.colSoTien.CellFormat.FormatString = "{0:#,0;(#,0)}";
            this.colSoTien.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colSoTien.FieldName = "SoTien";
            this.colSoTien.GrandTotalText = "Tổng cộng";
            this.colSoTien.Name = "colSoTien";
            this.colSoTien.Width = 70;
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
            this.BottomMargin.HeightF = 52F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // ReportHeader
            // 
            this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.tblHeader});
            this.ReportHeader.HeightF = 80.20837F;
            this.ReportHeader.Name = "ReportHeader";
            // 
            // tblHeader
            // 
            this.tblHeader.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.tblHeader.Name = "tblHeader";
            this.tblHeader.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1,
            this.xrTableRow2});
            this.tblHeader.SizeF = new System.Drawing.SizeF(1069F, 69.79167F);
            this.tblHeader.StylePriority.UseTextAlignment = false;
            this.tblHeader.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell1});
            this.xrTableRow1.Name = "xrTableRow1";
            this.xrTableRow1.Weight = 1.2716418741879825D;
            // 
            // xrTableCell1
            // 
            this.xrTableCell1.Font = new System.Drawing.Font("Times New Roman", 20F, System.Drawing.FontStyle.Bold);
            this.xrTableCell1.Name = "xrTableCell1";
            this.xrTableCell1.StylePriority.UseFont = false;
            this.xrTableCell1.Text = "BÁO CÁO CÔNG NỢ THẺ XE";
            this.xrTableCell1.Weight = 3D;
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.cThoiGian});
            this.xrTableRow2.Name = "xrTableRow2";
            this.xrTableRow2.Weight = 0.72835812581201753D;
            // 
            // cThoiGian
            // 
            this.cThoiGian.Font = new System.Drawing.Font("Times New Roman", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.cThoiGian.Name = "cThoiGian";
            this.cThoiGian.StylePriority.UseFont = false;
            this.cThoiGian.Text = "[KyBC] (Từ ngày [TuNgay] - Đến Ngày [DenNgay])";
            this.cThoiGian.Weight = 3D;
            // 
            // ReportFooter
            // 
            this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.tblFooter});
            this.ReportFooter.HeightF = 28.50001F;
            this.ReportFooter.Name = "ReportFooter";
            // 
            // tblFooter
            // 
            this.tblFooter.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.tblFooter.LocationFloat = new DevExpress.Utils.PointFloat(0F, 10.00001F);
            this.tblFooter.Name = "tblFooter";
            this.tblFooter.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 0, 0, 0, 100F);
            this.tblFooter.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow61});
            this.tblFooter.SizeF = new System.Drawing.SizeF(1069F, 18.5F);
            this.tblFooter.StylePriority.UseFont = false;
            this.tblFooter.StylePriority.UsePadding = false;
            this.tblFooter.StylePriority.UseTextAlignment = false;
            this.tblFooter.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrTableRow61
            // 
            this.xrTableRow61.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell91,
            this.xrTableCell94});
            this.xrTableRow61.Name = "xrTableRow61";
            this.xrTableRow61.Weight = 1D;
            // 
            // xrTableCell91
            // 
            this.xrTableCell91.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Bold);
            this.xrTableCell91.Name = "xrTableCell91";
            this.xrTableCell91.StylePriority.UseFont = false;
            this.xrTableCell91.StylePriority.UseTextAlignment = false;
            this.xrTableCell91.Text = "GIÁM ĐỐC Dự án";
            this.xrTableCell91.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell91.Weight = 1.573616791729394D;
            // 
            // xrTableCell94
            // 
            this.xrTableCell94.Name = "xrTableCell94";
            this.xrTableCell94.StylePriority.UseTextAlignment = false;
            this.xrTableCell94.Text = "KẾ TOÁN";
            this.xrTableCell94.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell94.Weight = 1.4878536783389063D;
            // 
            // rptCongNoTheXe
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.ReportHeader,
            this.ReportFooter});
            this.Landscape = true;
            this.Margins = new System.Drawing.Printing.Margins(50, 50, 50, 52);
            this.PageHeight = 827;
            this.PageWidth = 1169;
            this.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.Version = "12.1";
            ((System.ComponentModel.ISupportInitialize)(this.tblHeader)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblFooter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.XRPivotGrid pvCongNo;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField colMaKH;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField colTenKH;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField colKyBC;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField colLDV;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField colSoTien;
        private DevExpress.XtraReports.UI.ReportHeaderBand ReportHeader;
        private DevExpress.XtraReports.UI.ReportFooterBand ReportFooter;
        private DevExpress.XtraReports.UI.XRTable tblHeader;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow1;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell1;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow2;
        private DevExpress.XtraReports.UI.XRTableCell cThoiGian;
        private DevExpress.XtraReports.UI.XRTable tblFooter;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow61;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell91;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell94;
    }
}
