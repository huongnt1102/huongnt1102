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
    public partial class rptTongHopTienMatNopVeCongTy : DevExpress.XtraReports.UI.XtraReport
    {
        MasterDataContext db = new MasterDataContext();
        public rptTongHopTienMatNopVeCongTy(DateTime _tungay,DateTime _dengnay)
        {
            InitializeComponent();
            cSTT.DataBindings.Add("Text", null, "STT");
            cDien.DataBindings.Add("Text", null, "Dien","{0:#,0.##}");
            cNuoc.DataBindings.Add("Text", null, "Nuoc", "{0:#,0.##}");
            cND.DataBindings.Add("Text", null, "NgayTT");
            //cXe.DataBindings.Add("Text", null, "Xe", "{0:#,0.##}");
            cThanhTien.DataBindings.Add("Text", null, "ThanhTien", "{0:#,0.##}");
            cXeOto.DataBindings.Add("Text", null, "XeOTo", "{0:#,0.##}");
            cXeMay.DataBindings.Add("Text", null, "XeMay", "{0:#,0.##}");
            cXe.DataBindings.Add("Text", null, "XeTrongNgay", "{0:#,0.##}");
            cSumDien.DataBindings.Add(new XRBinding("Text", null, "Dien", "{0:#,0.##}"));
            cSumDien.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,0.##}");
            csumNuoc.DataBindings.Add(new XRBinding("Text", null, "Nuoc", "{0:#,0.##}"));
            csumNuoc.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,0.##}");
            cSumXeMay.DataBindings.Add(new XRBinding("Text", null, "XeMay", "{0:#,0.##}"));
            cSumXeMay.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,0.##}");
            cSumTongTien.DataBindings.Add(new XRBinding("Text", null, "ThanhTien", "{0:#,0.##}"));
            cSumTongTien.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,0.##}");
            cSumOto.DataBindings.Add(new XRBinding("Text", null, "XeOTo", "{0:#,0.##}"));
            cSumOto.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,0.##}");
            cSumXeNgay.DataBindings.Add(new XRBinding("Text", null, "XeTrongNgay", "{0:#,0.##}"));
            cSumXeNgay.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,0.##}");

            cNgayIn.Text = string.Format("Hà nội , ngày {0} tháng {1} năm {2}", DateTime.Now.Day, DateTime.Now.Month , DateTime.Now.Year);
            //var DaThu = (from ct in db.ptChiTietPhieuThus
            //             join pt in db.ptPhieuThus on ct.MaPT equals pt.ID
                        
            //             join hd in db.dvHoaDons on new { ct.TableName, ct.LinkID } equals new { TableName = "dvHoaDon", LinkID = (long?)hd.ID }
            //              join xe in db.dvgxTheXes on hd.LinkID equals  xe.ID into TienXe
            //              from xe in TienXe.DefaultIfEmpty()
            //             where pt.MaTN == 27 
            //                    //& (hd.MaLDV == 22 | hd.MaLDV == (int)MaLDVs.Dien | hd.MaLDV == (int)MaLDVs.PGX)
            //                    & SqlMethods.DateDiffDay(_tungay, pt.NgayThu) >= 0 & SqlMethods.DateDiffDay(pt.NgayThu,_dengnay ) >= 0 
            //             group new { hd, ct, xe ,pt} by new { pt.NgayThu.Value.Month, pt.NgayThu.Value.Year } into gr
            //             select new
            //             {

            //                 NgayTTT = gr.Key.Month,
            //                 NgayTTY = gr.Key.Year,
            //                 Dien = (decimal?)gr.Sum(p => p.hd.MaLDV == 8 | p.hd.MaLDV == 5 | p.hd.MaLDV == 11 | p.hd.MaLDV == 17 ? p.ct.SoTien : 0).GetValueOrDefault(),
            //                 Nuoc = (decimal?)gr.Sum(p =>p.hd.MaLDV == 22  ? p.ct.SoTien : 0).GetValueOrDefault(),
            //                 XeTrongNgay = (decimal?)gr.Sum(p => p.hd.MaLDV == 6 & !p.hd.DienGiai.Contains("xe máy") & p.hd.LinkID == null & !p.hd.DienGiai.Contains("ô tô") ? p.ct.SoTien : 0).GetValueOrDefault(),
            //                 XeOTo = (decimal?)gr.Sum(p => p.hd.MaLDV == 6 & (p.xe.MaLX == 25 | p.xe.MaLX == 65) ? p.ct.SoTien : 0).GetValueOrDefault(),
            //                 OToNgoai = (decimal?)gr.Sum(p => p.hd.MaLDV == 6 & p.hd.DienGiai.Contains("ô tô") & p.hd.LinkID == null ? p.ct.SoTien : 0).GetValueOrDefault(),
            //                 XeMay = (decimal?)gr.Sum(p => p.hd.MaLDV == 6 & (p.xe.MaLX == 23 | p.xe.MaLX == 22 | p.xe.MaLX == 21 | p.xe.MaLX == 24) ? p.ct.SoTien : 0).GetValueOrDefault(),
            //                 XeNgoai = (decimal?)gr.Sum(p => p.hd.MaLDV == 6 & p.hd.DienGiai.Contains("xe máy") & p.hd.LinkID == null ? p.ct.SoTien : 0).GetValueOrDefault(),
            //             }).OrderBy(k=>k.NgayTTY).OrderBy(p => p.NgayTTT).ToList();


            var DaThu2 = (from ct in db.ptChiTietPhieuThus
                                     join pt in db.ptPhieuThus on ct.MaPT equals pt.ID
                                     join hd in db.dvHoaDons on new { ct.TableName, ct.LinkID } equals new { TableName = "dvHoaDon", LinkID = (long?)hd.ID }
                                     join kh in db.tnKhachHangs on hd.MaKH equals kh.MaKH
                                     join l in db.dvLoaiDichVus on hd.MaLDV equals l.ID
                                     join mb in db.mbMatBangs on hd.MaMB equals mb.MaMB into tblMatBang
                                     from mb in tblMatBang.DefaultIfEmpty()
                                     join tl in db.mbTangLaus on mb.MaTL equals tl.MaTL into tblTangLau
                                     from tl in tblTangLau.DefaultIfEmpty()
                                     join kn in db.mbKhoiNhas on tl.MaKN equals kn.MaKN into tblKhoiNha
                                     from kn in tblKhoiNha.DefaultIfEmpty()
                                     join tn in db.tnToaNhas on hd.MaTN equals tn.MaTN
                                     join lmb in db.mbLoaiMatBangs on mb.MaLMB equals lmb.MaLMB into tblLoaiMatBang
                                     from lmb in tblLoaiMatBang.DefaultIfEmpty()
                                     join xe in db.dvgxTheXes on hd.LinkID equals xe.ID into TienXe
                                     from xe in TienXe.DefaultIfEmpty()
                                     join nv in db.tnNhanViens on pt.MaNV equals nv.MaNV
                                     where hd.MaTN == Common.User.MaTN & SqlMethods.DateDiffDay(_tungay, pt.NgayThu) >= 0 & SqlMethods.DateDiffDay(pt.NgayThu, _dengnay) >= 0
                                     group new { hd, ct, pt, xe } by new { pt.NgayThu.Value.Month, pt.NgayThu.Value.Year } into gr
                                     select new
                                     {
                                         NgayTTT = gr.Key.Month,
                                         NgayTTY = gr.Key.Year,
                                         Dien = (decimal?)gr.Sum(p => p.hd.MaLDV == 8 | p.hd.MaLDV == 5 | p.hd.MaLDV == 11 | p.hd.MaLDV == 17 ? p.ct.SoTien : 0).GetValueOrDefault(),
                                         Nuoc = (decimal?)gr.Sum(p => p.hd.MaLDV == 22 | p.hd.MaLDV == 18 ? p.ct.SoTien : 0).GetValueOrDefault(),
                                         XeTrongNgay = (decimal?)gr.Sum(p => p.hd.MaLDV == 6 & p.xe.MaLX ==null ? p.ct.SoTien : 0).GetValueOrDefault(),
                                         XeOTo = (decimal?)gr.Sum(p => p.hd.MaLDV == 6 & (p.xe.MaLX == 25 | p.xe.MaLX == 65 | p.xe.MaLX == 48) ? p.ct.SoTien : 0).GetValueOrDefault(),
                                         //OToNgoai = (decimal?)gr.Sum(p => p.hd.MaLDV == 6 & p.hd.DienGiai.Contains("ô tô") & p.hd.LinkID == null ? p.ct.SoTien : 0).GetValueOrDefault(),
                                         XeMay = (decimal?)gr.Sum(p => p.hd.MaLDV == 6 & (p.xe.MaLX == 23 | p.xe.MaLX == 22 | p.xe.MaLX == 21 | p.xe.MaLX == 24) ? p.ct.SoTien : 0).GetValueOrDefault(),
                                         //XeNgoai = (decimal?)gr.Sum(p => p.hd.MaLDV == 6 & p.hd.DienGiai.Contains("xe máy") & p.hd.LinkID == null ? p.ct.SoTien : 0).GetValueOrDefault(),
                                     }).ToList();

            this.DataSource = DaThu2.Select
                (
                    (p, index) =>
                    new
                    {
                        STT = index + 1,
                        NgayTT = p.NgayTTT + "/" + p.NgayTTY,
                        Dien = p.Dien,
                        Nuoc = p.Nuoc,
                        XeTrongNgay = p.XeTrongNgay == 0 ? null : p.XeTrongNgay,
                        ThanhTien = p.Dien + p.Nuoc + p.XeOTo + p.XeMay  + p.XeTrongNgay,
                        XeOTo = p.XeOTo,
                        XeMay = p.XeMay,
                    }
                ).ToList();
        }

    }
}
