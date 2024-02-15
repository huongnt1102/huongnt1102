using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Data.Linq.SqlClient;
using System.Linq;
using DevExpress.XtraReports.UI;
using Library;

namespace LandSoftBuilding.Report
{
    public partial class rptTongHopThuTheoThang : DevExpress.XtraReports.UI.XtraReport
    {
        public rptTongHopThuTheoThang(int month, int year, int maTN)
        {
            InitializeComponent();
            cellMaMB.DataBindings.Add(new XRBinding("Text", null, "MaSoMB"));
            cellTenKH.DataBindings.Add(new XRBinding("Text", null, "TenKH"));
            cellGhiChu.DataBindings.Add(new XRBinding("Text", null, "GhiChu"));

            cellPQLT08.DataBindings.Add(new XRBinding("Text", null, "PQL", "{0:#,0.##}"));
            cellNoCuPQL.DataBindings.Add(new XRBinding("Text", null, "PQLNoCu", "{0:#,0.##}"));
            cellPGXT08.DataBindings.Add(new XRBinding("Text", null, "PGX", "{0:#,0.##}"));
            cellNoCuPGX.DataBindings.Add(new XRBinding("Text", null, "PGXNoCu", "{0:#,0.##}"));
            cellPVST08.DataBindings.Add(new XRBinding("Text", null, "PVS", "{0:#,0.##}"));
            cellNoCuPVS.DataBindings.Add(new XRBinding("Text", null, "PVSNoCu", "{0:#,0.##}"));

            cellNuocT7.DataBindings.Add(new XRBinding("Text", null, "Nuoc", "{0:#,0.##}"));
            cellNoCuNuoc.DataBindings.Add(new XRBinding("Text", null, "NuocNoCu", "{0:#,0.##}"));
            cellGasT7.DataBindings.Add(new XRBinding("Text", null, "Gas", "{0:#,0.##}"));
            cellNoCuGas.DataBindings.Add(new XRBinding("Text", null, "GasNoCu", "{0:#,0.##}"));
            cellTongTien.DataBindings.Add(new XRBinding("Text", null, "TongTien", "{0:#,0.##}"));

            cellSumPQLT08.DataBindings.Add(new XRBinding("Text", null, "PQL", "{0:#,0.##}"));
            cellSumPQLT08.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,0.##}");

            cellSumPGXT08.DataBindings.Add(new XRBinding("Text", null, "PGX", "{0:#,0.##}"));
            cellSumPGXT08.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,0.##}");

            cellSumPVST08.DataBindings.Add(new XRBinding("Text", null, "PVS", "{0:#,0.##}"));
            cellSumPVST08.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,0.##}");

            cellSumNoCuPQL.DataBindings.Add(new XRBinding("Text", null, "PQLNoCu", "{0:#,0.##}"));
            cellSumNoCuPQL.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,0.##}");

            cellSumNoCuPGX.DataBindings.Add(new XRBinding("Text", null, "PGXNoCu", "{0:#,0.##}"));
            cellSumNoCuPGX.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,0.##}");

            cellSumNoCuPVS.DataBindings.Add(new XRBinding("Text", null, "PVSNoCu", "{0:#,0.##}"));
            cellSumNoCuPVS.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,0.##}");

            cellSumNuocT7.DataBindings.Add(new XRBinding("Text", null, "Nuoc", "{0:#,0.##}"));
            cellSumNuocT7.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,0.##}");

            cellSumNoCuNuoc.DataBindings.Add(new XRBinding("Text", null, "NuocNoCu", "{0:#,0.##}"));
            cellSumNoCuNuoc.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,0.##}");

            cellSumGasT7.DataBindings.Add(new XRBinding("Text", null, "Gas", "{0:#,0.##}"));
            cellSumGasT7.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,0.##}");

            cellSumNoCuGas.DataBindings.Add(new XRBinding("Text", null, "GasNoCu", "{0:#,0.##}"));
            cellSumNoCuGas.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,0.##}");

            cellSumTongTien.DataBindings.Add(new XRBinding("Text", null, "TongTien", "{0:#,0.##}"));
            cellSumTongTien.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,0.##}");
            
            var db = new MasterDataContext();
            try
            {
                var objTN = db.tnToaNhas.Single(p => p.MaTN == maTN);
                lblTitle.Text = string.Format("BẢNG TỔNG HỢP THU THÁNG {0}/{1} TẠI {2}", month, year, objTN.TenTN.ToUpper());
                //
                lbPQL1.Text = string.Format(lbPQL1.Text, month, year);
                lbPQL.Text = string.Format(lbPQL.Text, month);
                lbPGX.Text = string.Format(lbPGX.Text, month);
                lbPVS.Text = string.Format(lbPVS.Text, month);
                lblNuoc.Text = string.Format(lblNuoc.Text, (month - 1) == 0 ? 12 : month - 1);
                lblGas.Text = string.Format(lblGas.Text, (month - 1) == 0 ? 12 : month - 1);

                //Load data
                var _Ngay = new DateTime(year, month, 1);
                this.DataSource = (from ct in db.ptChiTietPhieuThus
                                   join pt in db.ptPhieuThus on ct.MaPT equals pt.ID
                                   join hd in db.dvHoaDons on new { ct.TableName, ct.LinkID } equals new { TableName = "dvHoaDon", LinkID = (long?)hd.ID }
                                   join mb in db.mbMatBangs on hd.MaMB equals mb.MaMB
                                   join kh in db.tnKhachHangs on hd.MaKH equals kh.MaKH
                                   where hd.MaTN == maTN & hd.IsDuyet == true & SqlMethods.DateDiffMonth(pt.NgayThu, _Ngay) == 0
                                   & (hd.MaLDV == (int)MaLDVs.PQL | hd.MaLDV == (int)MaLDVs.Nuoc | hd.MaLDV == (int)MaLDVs.Gas | hd.MaLDV == (int)MaLDVs.PGX | hd.MaLDV == (int)MaLDVs.PVS)
                                   group new { ct, hd } by new { hd.MaMB, mb.MaSoMB, TenKH = kh.IsCaNhan == true ? (kh.HoKH + " " + kh.TenKH) : kh.CtyTen } into gr
                                   orderby gr.Key.MaSoMB
                                   select new
                                   {
                                       gr.Key.MaSoMB,
                                       gr.Key.TenKH,
                                       PQL = gr.Sum(p => p.hd.MaLDV == (int)MaLDVs.PQL & SqlMethods.DateDiffMonth(p.hd.NgayTT, _Ngay) == 0 ? p.ct.SoTien : 0).GetValueOrDefault(),
                                       Nuoc = gr.Sum(p => p.hd.MaLDV == (int)MaLDVs.Nuoc & SqlMethods.DateDiffMonth(p.hd.NgayTT, _Ngay) == 0 ? p.ct.SoTien : 0).GetValueOrDefault(),
                                       Gas = gr.Sum(p => p.hd.MaLDV == (int)MaLDVs.Gas & SqlMethods.DateDiffMonth(p.hd.NgayTT, _Ngay) == 0 ? p.ct.SoTien : 0).GetValueOrDefault(),
                                       PGX = gr.Sum(p => p.hd.MaLDV == (int)MaLDVs.PGX & SqlMethods.DateDiffMonth(p.hd.NgayTT, _Ngay) == 0 ? p.ct.SoTien : 0).GetValueOrDefault(),
                                       PVS = gr.Sum(p => p.hd.MaLDV == (int)MaLDVs.PVS & SqlMethods.DateDiffMonth(p.hd.NgayTT, _Ngay) == 0 ? p.ct.SoTien : 0).GetValueOrDefault(),
                                       PQLNoCu = gr.Sum(p => p.hd.MaLDV == (int)MaLDVs.PQL & SqlMethods.DateDiffMonth(p.hd.NgayTT, _Ngay) > 0 ? p.ct.SoTien : 0).GetValueOrDefault(),
                                       NuocNoCu = gr.Sum(p => p.hd.MaLDV == (int)MaLDVs.Nuoc & SqlMethods.DateDiffMonth(p.hd.NgayTT, _Ngay) > 0 ? p.ct.SoTien : 0).GetValueOrDefault(),
                                       GasNoCu = gr.Sum(p => p.hd.MaLDV == (int)MaLDVs.Gas & SqlMethods.DateDiffMonth(p.hd.NgayTT, _Ngay) > 0 ? p.ct.SoTien : 0).GetValueOrDefault(),
                                       PGXNoCu = gr.Sum(p => p.hd.MaLDV == (int)MaLDVs.PGX & SqlMethods.DateDiffMonth(p.hd.NgayTT, _Ngay) > 0 ? p.ct.SoTien : 0).GetValueOrDefault(),
                                       PVSNoCu = gr.Sum(p => p.hd.MaLDV == (int)MaLDVs.PVS & SqlMethods.DateDiffMonth(p.hd.NgayTT, _Ngay) > 0 ? p.ct.SoTien : 0).GetValueOrDefault(),
                                       TongTien = gr.Sum(p => p.ct.SoTien).GetValueOrDefault()
                                   }).ToList();
            }
            catch { }
            finally
            {
                db.Dispose();
            }
        }

    }
}
