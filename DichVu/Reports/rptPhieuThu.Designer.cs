using System;
using DevExpress.XtraReports.UI.PivotGrid;

namespace DichVu.Reports
{
    partial class rptPhieuThu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(rptPhieuThu));
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.pvData = new DevExpress.XtraReports.UI.XRPivotGrid();
            this.colMaKH = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.colTenKH = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.colKyBC = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.colLDV = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.colSoTien = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.cThoiGian = new DevExpress.XtraReports.UI.XRTableCell();
            this.picLogo = new DevExpress.XtraReports.UI.XRPictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.pvData});
            this.Detail.HeightF = 101.0417F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // pvData
            // 
            this.pvData.Appearance.FieldHeader.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.pvData.Appearance.FieldHeader.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.pvData.Appearance.FieldHeader.TextVerticalAlignment = DevExpress.Utils.VertAlignment.Center;
            this.pvData.Appearance.FieldValue.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.pvData.Appearance.FieldValue.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.pvData.Appearance.FieldValue.TextVerticalAlignment = DevExpress.Utils.VertAlignment.Center;
            this.pvData.Appearance.FieldValueGrandTotal.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.pvData.Appearance.FieldValueGrandTotal.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.pvData.Appearance.FieldValueGrandTotal.TextVerticalAlignment = DevExpress.Utils.VertAlignment.Center;
            this.pvData.Fields.AddRange(new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField[] {
            this.colMaKH,
            this.colTenKH,
            this.colKyBC,
            this.colLDV,
            this.colSoTien});
            this.pvData.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.pvData.Name = "pvData";
            this.pvData.OptionsPrint.FilterSeparatorBarPadding = 3;
            this.pvData.OptionsView.ShowColumnHeaders = false;
            this.pvData.OptionsView.ShowDataHeaders = false;
            this.pvData.OptionsView.ShowFilterHeaders = false;
            this.pvData.SizeF = new System.Drawing.SizeF(1069F, 101.0417F);
            this.pvData.FieldValueDisplayText += new System.EventHandler<DevExpress.XtraReports.UI.PivotGrid.PivotFieldDisplayTextEventArgs>(this.pvData_FieldValueDisplayText);
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
            this.BottomMargin.HeightF = 50F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // ReportHeader
            // 
            this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable1,
            this.picLogo});
            this.ReportHeader.HeightF = 157.2917F;
            this.ReportHeader.Name = "ReportHeader";
            // 
            // xrTable1
            // 
            this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 75.00001F);
            this.xrTable1.Name = "xrTable1";
            this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1,
            this.xrTableRow2});
            this.xrTable1.SizeF = new System.Drawing.SizeF(1069F, 69.79167F);
            this.xrTable1.StylePriority.UseTextAlignment = false;
            this.xrTable1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
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
            this.xrTableCell1.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Bold);
            this.xrTableCell1.Name = "xrTableCell1";
            this.xrTableCell1.StylePriority.UseFont = false;
            this.xrTableCell1.Text = "BÁO CÁO CHI TIẾT CÁC KHOẢN ĐÃ THU DỊCH VỤ";
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
            // picLogo
            // 
            this.picLogo.ImageSource = new DevExpress.XtraPrinting.Drawing.ImageSource("img", resources.GetString("picLogo.ImageSource"));
            this.picLogo.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.picLogo.Name = "picLogo";
            this.picLogo.SizeF = new System.Drawing.SizeF(121.7502F, 75.00001F);
            this.picLogo.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
            // 
            // rptPhieuThu
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.ReportHeader});
            this.Landscape = true;
            this.Margins = new System.Drawing.Printing.Margins(50, 50, 50, 50);
            this.PageHeight = 827;
            this.PageWidth = 1169;
            this.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.Version = "18.2";
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.XRPivotGrid pvData;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField colMaKH;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField colTenKH;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField colKyBC;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField colLDV;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField colSoTien;
        private DevExpress.XtraReports.UI.ReportHeaderBand ReportHeader;
        private DevExpress.XtraReports.UI.XRPictureBox picLogo;
        private DevExpress.XtraReports.UI.XRTable xrTable1;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow1;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell1;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow2;
        private DevExpress.XtraReports.UI.XRTableCell cThoiGian;
    }
}
