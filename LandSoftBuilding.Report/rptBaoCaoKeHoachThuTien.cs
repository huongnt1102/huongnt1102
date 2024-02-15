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
    public partial class rptBaoCaoKeHoachThuTien : DevExpress.XtraReports.UI.XtraReport
    {
        public rptBaoCaoKeHoachThuTien(DateTime tuNgay, DateTime denNgay, int maTN)
        {
            InitializeComponent();
            cSTT.DataBindings.Add(new XRBinding("Text", null, "MaSoMB"));
            cTenCTy.DataBindings.Add(new XRBinding("Text", null, "KhachHang"));
            cGhiChu.DataBindings.Add(new XRBinding("Text", null, "GhiChu"));

            cNoCu.DataBindings.Add(new XRBinding("Text", null, "PQL", "{0:#,0.##}"));
            cTienQuangCao.DataBindings.Add(new XRBinding("Text", null, "PQLNoCu", "{0:#,0.##}"));
            cTienDien.DataBindings.Add(new XRBinding("Text", null, "PGX", "{0:#,0.##}"));
            //cTienDienThoai.DataBindings.Add(new XRBinding("Text", null, "PGXNoCu", "{0:#,0.##}"));
            //cTienMatBang.DataBindings.Add(new XRBinding("Text", null, "PVS", "{0:#,0.##}"));
            //cellNoCuPVS.DataBindings.Add(new XRBinding("Text", null, "PVSNoCu", "{0:#,0.##}"));

            cTienATM.DataBindings.Add(new XRBinding("Text", null, "Nuoc", "{0:#,0.##}"));
            cTienKhac.DataBindings.Add(new XRBinding("Text", null, "NuocNoCu", "{0:#,0.##}"));
            //cTienMat.DataBindings.Add(new XRBinding("Text", null, "Gas", "{0:#,0.##}"));
            cDaThu.DataBindings.Add(new XRBinding("Text", null, "GasNoCu", "{0:#,0.##}"));
            cNgayTT.DataBindings.Add(new XRBinding("Text", null, "TongTien", "{0:#,0.##}"));

            cellSumNoCu.DataBindings.Add(new XRBinding("Text", null, "PQL", "{0:#,0.##}"));
            cellSumNoCu.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,0.##}");

            cellSumTienDien.DataBindings.Add(new XRBinding("Text", null, "PGX", "{0:#,0.##}"));
            cellSumTienDien.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,0.##}");

            cellSumTienQuangCao.DataBindings.Add(new XRBinding("Text", null, "PVS", "{0:#,0.##}"));
            cellSumTienQuangCao.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,0.##}");

            cellSumTienMB.DataBindings.Add(new XRBinding("Text", null, "PQLNoCu", "{0:#,0.##}"));
            cellSumTienMB.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,0.##}");

            cellSumTienQuangCao.DataBindings.Add(new XRBinding("Text", null, "PGXNoCu", "{0:#,0.##}"));
            cellSumTienQuangCao.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,0.##}");

            //cellSumNoCuPVS.DataBindings.Add(new XRBinding("Text", null, "PVSNoCu", "{0:#,0.##}"));
            //cellSumNoCuPVS.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,0.##}");

            cellSumTienATM.DataBindings.Add(new XRBinding("Text", null, "Nuoc", "{0:#,0.##}"));
            cellSumTienATM.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,0.##}");

            cellSumThuKhac.DataBindings.Add(new XRBinding("Text", null, "NuocNoCu", "{0:#,0.##}"));
            cellSumThuKhac.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,0.##}");

            cellSumTongCong.DataBindings.Add(new XRBinding("Text", null, "Gas", "{0:#,0.##}"));
            cellSumTongCong.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,0.##}");

            cellSumDaThu.DataBindings.Add(new XRBinding("Text", null, "GasNoCu", "{0:#,0.##}"));
            cellSumDaThu.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,0.##}");

            cell.DataBindings.Add(new XRBinding("Text", null, "TongTien", "{0:#,0.##}"));
            cell.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,0.##}");

            lbPQL1.Text = string.Format(lbPQL1.Text, denNgay.Month, denNgay.Year);
            lbPQL.Text = string.Format(lbPQL.Text, denNgay.Month);
            lbPGX.Text = string.Format(lbPGX.Text, denNgay.Month);
            lbPVS.Text = string.Format(lbPVS.Text, denNgay.Month);
            //lblGas.Text = string.Format("GAS T{0}", denNgay.Month - 1);
            //lblNuoc.Text = string.Format("NƯỚC T{0}", denNgay.Month - 1);
            using (var db = new MasterDataContext())
            {
                try
                {
                    var objTN = db.tnToaNhas.Single(p => p.MaTN == maTN);
                    lblTitle.Text = string.Format("BẢNG TỔNG HỢP THU THÁNG {0}/{1} TẠI {2}", denNgay.Month, denNgay.Year, objTN.TenTN.ToUpper());
                }
                catch { }
                var ThuPQL = (from pt in db.dvHoaDons
                              join mb in db.mbMatBangs on pt.MaMB equals mb.MaMB
                              join tl in db.mbTangLaus on mb.MaTL equals tl.MaTL
                              join kn in db.mbKhoiNhas on tl.MaKN equals kn.MaKN
                              join tn in db.tnToaNhas on kn.MaTN equals tn.MaTN
                              join kh in db.tnKhachHangs on pt.MaKH equals kh.MaKH
                              join phieuct in db.ptChiTietPhieuThus on pt.ID equals phieuct.LinkID
                              join phieu in db.ptPhieuThus on phieuct.MaPT equals phieu.ID
                              where

                                 SqlMethods.DateDiffDay(tuNgay, phieu.NgayThu) >= 0 & SqlMethods.DateDiffDay(  phieu.NgayThu,denNgay ) >= 0
                                  & pt.LinkID != null &
                                 pt.MaTN == maTN & pt.MaLDV == 13 & pt.MaMB != null & pt.IsDuyet == true
                              group new { mb, tn, kh, pt } by new { pt.MaKH, pt.MaMB, mb.MaSoMB, kh.CtyTen, kh.TenKH, kh.HoKH, kh.MaPhu, pt.PhiDV, pt.NgayTT, pt.ConNo, pt.DaThu } into gr
                              select new
                              {
                                  KhachHang = gr.Key.HoKH + " " + gr.Key.TenKH,
                                  DiaChi = gr.Key.MaPhu,

                                  gr.Key.MaPhu,
                                  gr.Key.MaSoMB,
                                  PQL = gr.Key.DaThu,
                                  PQLNoCu = (decimal?)db.dvHoaDons.Where(p => p.MaKH == gr.Key.MaKH & p.MaMB == gr.Key.MaMB & SqlMethods.DateDiffDay(denNgay, gr.Key.NgayTT) < 0 & p.ConNo > 0 & p.MaLDV == 13 & p.LinkID != null).Select(p => p.ConNo).Sum().GetValueOrDefault(),
                                  Nuoc = (decimal?)0,
                                  NuocNoCu = (decimal?)0,
                                  Gas = (decimal?)0,
                                  GasNoCu = (decimal?)0,
                                  PGX = (decimal?)0,
                                  PGXNoCu = (decimal?)0,
                                  PVS = (decimal?)0,
                                  PVSNoCu = (decimal?)0,
                              }
                    ).ToList();
                var ThuPVS = (from pt in db.dvHoaDons
                              join mb in db.mbMatBangs on pt.MaMB equals mb.MaMB
                              join tl in db.mbTangLaus on mb.MaTL equals tl.MaTL
                              join kn in db.mbKhoiNhas on tl.MaKN equals kn.MaKN
                              join tn in db.tnToaNhas on kn.MaTN equals tn.MaTN
                              join kh in db.tnKhachHangs on pt.MaKH equals kh.MaKH
                              join phieuct in db.ptChiTietPhieuThus on pt.ID equals phieuct.LinkID
                              join phieu in db.ptPhieuThus on phieuct.MaPT equals phieu.ID
                              where

                                 SqlMethods.DateDiffDay(tuNgay, phieu.NgayThu) >= 0 & SqlMethods.DateDiffDay(  phieu.NgayThu,denNgay ) >= 0
                                  & pt.LinkID != null &
                                 pt.MaTN == maTN & pt.MaLDV == 14 & pt.MaMB != null & pt.IsDuyet == true
                              group new { mb, tn, kh, pt } by new { pt.MaKH, pt.MaMB, mb.MaSoMB, kh.CtyTen, kh.TenKH, kh.HoKH, kh.MaPhu, pt.ConNo, pt.DaThu, pt.PhiDV, pt.NgayTT } into gr
                              select new
                              {
                                  KhachHang = gr.Key.HoKH + " " + gr.Key.TenKH,
                                  DiaChi = gr.Key.MaPhu,

                                  gr.Key.MaPhu,
                                  gr.Key.MaSoMB,
                                  PQL = (decimal?)0,
                                  PQLNoCu = (decimal?)0,
                                  Nuoc = (decimal?)0,
                                  NuocNoCu = (decimal?)0,
                                  Gas = (decimal?)0,
                                  GasNoCu = (decimal?)0,
                                  PGX = (decimal?)0,
                                  PGXNoCu = (decimal?)0,
                                  PVS = (decimal?)gr.Key.DaThu,
                                  PVSNoCu = (decimal?)db.dvHoaDons.Where(p => p.MaKH == gr.Key.MaKH & p.IsDuyet == true & p.MaMB == gr.Key.MaMB & SqlMethods.DateDiffDay(denNgay, gr.Key.NgayTT) < 0 & p.ConNo > 0 & p.MaLDV == 14 & p.LinkID != null).Select(p => p.ConNo).Sum().GetValueOrDefault(),
                              }
                   ).ToList();
                var ThuNuoc = (from pt in db.dvHoaDons
                               join mb in db.mbMatBangs on pt.MaMB equals mb.MaMB
                               join tl in db.mbTangLaus on mb.MaTL equals tl.MaTL
                               join kn in db.mbKhoiNhas on tl.MaKN equals kn.MaKN
                               join tn in db.tnToaNhas on kn.MaTN equals tn.MaTN
                               join kh in db.tnKhachHangs on pt.MaKH equals kh.MaKH
                               join phieuct in db.ptChiTietPhieuThus on pt.ID equals phieuct.LinkID
                               join phieu in db.ptPhieuThus on phieuct.MaPT equals phieu.ID
                               where

                                  SqlMethods.DateDiffDay(tuNgay, phieu.NgayThu) >= 0 & SqlMethods.DateDiffDay(  phieu.NgayThu,denNgay ) >= 0
                                   & pt.LinkID != null &
                                  pt.MaTN == maTN & pt.MaLDV == 9 & pt.MaMB != null & pt.IsDuyet == true
                               group new { mb, tn, kh, pt } by new { pt.MaKH, pt.MaMB, mb.MaSoMB, kh.CtyTen, kh.TenKH, kh.HoKH, kh.MaPhu, pt.PhiDV, pt.ConNo, pt.DaThu, pt.NgayTT } into gr
                               select new
                               {
                                   KhachHang = gr.Key.HoKH + " " + gr.Key.TenKH,
                                   DiaChi = gr.Key.MaPhu,

                                   gr.Key.MaPhu,
                                   gr.Key.MaSoMB,
                                   PQL = (decimal?)0,
                                   PQLNoCu = (decimal?)0,
                                   Nuoc = (decimal?)gr.Key.DaThu,
                                   NuocNoCu = (decimal?)db.dvHoaDons.Where(p => p.MaKH == gr.Key.MaKH
                                       & p.MaMB == gr.Key.MaMB & SqlMethods.DateDiffDay(denNgay, gr.Key.NgayTT) < 0 & p.IsDuyet == true & p.ConNo > 0 & p.MaLDV == 9 & p.LinkID != null).Select(p => p.ConNo).Sum().GetValueOrDefault(),

                                   Gas = (decimal?)0,
                                   GasNoCu = (decimal?)0,
                                   PGX = (decimal?)0,
                                   PGXNoCu = (decimal?)0,
                                   PVS = (decimal?)0,
                                   PVSNoCu = (decimal?)0
                               }
                   ).ToList();
                var ThuGas = (from pt in db.dvHoaDons
                              join mb in db.mbMatBangs on pt.MaMB equals mb.MaMB
                              join tl in db.mbTangLaus on mb.MaTL equals tl.MaTL
                              join kn in db.mbKhoiNhas on tl.MaKN equals kn.MaKN
                              join tn in db.tnToaNhas on kn.MaTN equals tn.MaTN
                              join kh in db.tnKhachHangs on pt.MaKH equals kh.MaKH
                              join phieuct in db.ptChiTietPhieuThus on pt.ID equals phieuct.LinkID
                              join phieu in db.ptPhieuThus on phieuct.MaPT equals phieu.ID
                              where

                                 SqlMethods.DateDiffDay(tuNgay, phieu.NgayThu) >= 0 & SqlMethods.DateDiffDay(  phieu.NgayThu,denNgay ) >= 0
                                  & pt.LinkID != null &
                                 pt.MaTN == maTN & pt.MaLDV == 10 & pt.MaMB != null & pt.IsDuyet == true
                              group new { mb, tn, kh, pt } by new { pt.MaKH, pt.MaMB, mb.MaSoMB, kh.CtyTen, kh.TenKH, kh.HoKH, kh.MaPhu, pt.ConNo, pt.DaThu, pt.PhiDV, pt.NgayTT } into gr
                              select new
                              {
                                  KhachHang = gr.Key.HoKH + " " + gr.Key.TenKH,
                                  DiaChi = gr.Key.MaPhu,

                                  gr.Key.MaPhu,
                                  gr.Key.MaSoMB,
                                  PQL = (decimal?)0,
                                  PQLNoCu = (decimal?)0,
                                  Nuoc = (decimal?)0,
                                  NuocNoCu = (decimal?)0,
                                  Gas = (decimal?)gr.Key.DaThu,
                                  GasNoCu = (decimal?)db.dvHoaDons.Where(p => p.MaKH == gr.Key.MaKH
                                      & p.MaMB == gr.Key.MaMB & SqlMethods.DateDiffDay(denNgay, gr.Key.NgayTT) < 0 & p.ConNo > 0 & p.IsDuyet == true & p.MaLDV == 10 & p.LinkID != null).Select(p => p.ConNo).Sum().GetValueOrDefault(),

                                  PGX = (decimal?)0,
                                  PGXNoCu = (decimal?)0,
                                  PVS = (decimal?)0,
                                  PVSNoCu = (decimal?)0
                              }
                   ).ToList();
                var ThuPGX = (from pt in db.dvHoaDons
                              join mb in db.mbMatBangs on pt.MaMB equals mb.MaMB
                              join tl in db.mbTangLaus on mb.MaTL equals tl.MaTL
                              join kn in db.mbKhoiNhas on tl.MaKN equals kn.MaKN
                              join tn in db.tnToaNhas on kn.MaTN equals tn.MaTN
                              join kh in db.tnKhachHangs on pt.MaKH equals kh.MaKH
                              join phieuct in db.ptChiTietPhieuThus on pt.ID equals phieuct.LinkID
                             join phieu in db.ptPhieuThus on phieuct.MaPT equals phieu.ID
                              where

                                 SqlMethods.DateDiffDay(tuNgay, phieu.NgayThu) >= 0 & SqlMethods.DateDiffDay(  phieu.NgayThu,denNgay ) >= 0
                                  & pt.LinkID != null &
                                 pt.MaTN == maTN & pt.MaLDV == 6 & pt.MaMB != null & pt.IsDuyet==true
                              group new { mb, tn, kh, pt } by new { pt.MaKH, pt.MaMB, mb.MaSoMB, kh.CtyTen, kh.TenKH, kh.HoKH, kh.MaPhu, pt.ConNo, pt.DaThu, pt.NgayTT } into gr
                              select new
                              {
                                  KhachHang = gr.Key.HoKH + " " + gr.Key.TenKH,
                                  DiaChi = gr.Key.MaPhu,

                                  gr.Key.MaPhu,
                                  gr.Key.MaSoMB,
                                  PQL = (decimal?)0,
                                  PQLNoCu = (decimal?)0,
                                  Nuoc = (decimal?)0,
                                  NuocNoCu = (decimal?)0,
                                  Gas = (decimal?)0,
                                  GasNoCu = (decimal?)0,
                                  PGX = (decimal?)gr.Sum(p => p.pt.DaThu),
                                  PGXNoCu = (decimal?)db.dvHoaDons.Where(p => p.MaKH == gr.Key.MaKH 
                                      &p.IsDuyet==true
                                      & p.MaMB == gr.Key.MaMB
                                      & SqlMethods.DateDiffDay(denNgay, gr.Key.NgayTT) < 0 & p.ConNo > 0 & p.MaLDV == 6 & p.LinkID != null).Select(p => p.ConNo).Sum().GetValueOrDefault(),

                                  PVS = (decimal?)0,
                                  PVSNoCu = (decimal?)0
                              }
                   ).ToList();
                var query = ThuPQL.Concat(ThuNuoc).Concat(ThuGas).Concat(ThuPGX).Concat(ThuPVS);
                var queryAll = (from a in query
                                where a.PQL != 0 | a.Nuoc != 0 | a.Gas != 0 | a.PGX != 0
                          group a by new { a.MaPhu, a.MaSoMB, a.KhachHang, a.DiaChi }
                              into gr
                              select new
                              {
                                  gr.Key.KhachHang,
                                  gr.Key.DiaChi,

                                  gr.Key.MaPhu,
                                  gr.Key.MaSoMB,
                                  PQL = gr.Sum(p => p.PQL),

                                  PQLNoCu = gr.Sum(p => p.PQLNoCu),
                                  Nuoc = gr.Sum(p => p.Nuoc),
                                  NuocNoCu = gr.Sum(p => p.NuocNoCu),
                                  Gas = gr.Sum(p => p.Gas),
                                  GasNoCu = gr.Sum(p => p.GasNoCu),
                                  PGX = gr.Sum(p => p.PGX),
                                  PGXNoCu = gr.Sum(p => p.PGXNoCu),
                                  PVS = gr.Sum(p => p.PVS),
                                  PVSNoCu = gr.Sum(p => p.PVSNoCu),
                              }).Select(p => new
                              {
                                  p.KhachHang,
                                  p.DiaChi,

                                  p.MaPhu,
                                  p.MaSoMB,
                                  p.PQL,

                                  p.PQLNoCu,
                                  p.Nuoc,
                                  p.NuocNoCu,
                                  p.Gas,
                                  p.GasNoCu,
                                  p.PGX,
                                  p.PGXNoCu,
                                  p.PVS,
                                  p.PVSNoCu,
                                  TongTien = p.PQL +

                                  p.PQLNoCu +
                                  p.Nuoc +
                                  p.NuocNoCu +
                                  p.Gas +
                                  p.GasNoCu +
                                  p.PGX +
                                  p.PGXNoCu +
                                  p.PVS +
                                  p.PVSNoCu
                              }).ToList();
                this.DataSource = queryAll;
            }
        }

    }
}
