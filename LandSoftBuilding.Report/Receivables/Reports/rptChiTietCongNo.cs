using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
using System.Data.Linq.SqlClient;
using Library;

namespace LandSoftBuilding.Receivables.Reports
{
    public partial class rptChiTietCongNo : DevExpress.XtraReports.UI.XtraReport
    {
        public rptChiTietCongNo(byte _MaTN, int _MaKH, DateTime _TuNgay, DateTime _DenNgay)
        {
            InitializeComponent();

            var db = new MasterDataContext();
            try
            {
                //Thong tin khach hang, toa nha
                var objKH = (from kh in db.tnKhachHangs
                             join tn in db.tnToaNhas on kh.MaTN equals tn.MaTN
                             where kh.MaKH == _MaKH
                             select new { TenKH = kh.IsCaNhan == true ? (kh.HoKH + " " + kh.TenKH) : kh.CtyTen, tn.CongTyQuanLy })
                            .FirstOrDefault();
                //Dau ky
                var _DauKy = (from hd in db.dvHoaDons
                              where hd.MaTN == _MaTN & hd.MaKH == _MaKH & SqlMethods.DateDiffDay(hd.NgayTT, _TuNgay) > 0 & hd.IsDuyet == true
                              select hd.PhaiThu - (from ct in db.ptChiTietPhieuThus
                                                   join pt in db.ptPhieuThus on ct.MaPT equals pt.ID
                                                   where ct.TableName == "dvHoaDon" & ct.LinkID == hd.ID & SqlMethods.DateDiffDay(pt.NgayThu, _TuNgay) > 0
                                                   select ct.SoTien).Sum().GetValueOrDefault()
                                                - (from ct in db.ktttChiTiets
                                                   join pt in db.ktttKhauTruThuTruocs on ct.MaCT equals pt.ID
                                                   where ct.TableName == "dvHoaDon" & ct.LinkID == hd.ID & SqlMethods.DateDiffDay(pt.NgayCT, _TuNgay) > 0
                                                   select ct.SoTien).Sum().GetValueOrDefault()
                            ).Sum().GetValueOrDefault();
                //Phat sinh
                var ltPhatSinh = (from hd in db.dvHoaDons
                                  where hd.MaTN == _MaTN & hd.MaKH == _MaKH & SqlMethods.DateDiffDay(_TuNgay, hd.NgayTT) >= 0 & SqlMethods.DateDiffDay(hd.NgayTT, _DenNgay) >= 0
                                  select new { NgayCT = hd.NgayTT, SoCT = "", hd.DienGiai, PhatSinhNo = hd.PhaiThu, PhatSinhCo = (decimal?)0 })
                                      .Union(
                                      from pt in db.ptPhieuThus
                                      join ct in db.ptChiTietPhieuThus on pt.ID equals ct.MaPT
                                      where pt.MaTN == _MaTN & pt.MaKH == _MaKH & SqlMethods.DateDiffDay(_TuNgay, pt.NgayThu) >= 0 & SqlMethods.DateDiffDay(pt.NgayThu, _DenNgay) >= 0
                                      select new { NgayCT = pt.NgayThu, SoCT = pt.SoPT, ct.DienGiai, PhatSinhNo = (decimal?)0, PhatSinhCo = ct.SoTien }
                                      );
                //Cuoi ky                               
                var _CuoiKy = (from hd in db.dvHoaDons
                               where hd.MaTN == _MaTN & hd.MaKH == _MaKH & SqlMethods.DateDiffDay(hd.NgayTT, _DenNgay) >= 0 & hd.IsDuyet == true
                               select hd.PhaiThu
                                              - (from ct in db.ptChiTietPhieuThus
                                                 join pt in db.ptPhieuThus on ct.MaPT equals pt.ID
                                                 where ct.TableName == "dvHoaDon" & ct.LinkID == hd.ID & SqlMethods.DateDiffDay(pt.NgayThu, _DenNgay) >= 0
                                                 select ct.SoTien).Sum().GetValueOrDefault()
                                              - (from ct in db.ktttChiTiets
                                                 join pt in db.ktttKhauTruThuTruocs on ct.MaCT equals pt.ID
                                                 where ct.TableName == "dvHoaDon" & ct.LinkID == hd.ID & SqlMethods.DateDiffDay(pt.NgayCT, _DenNgay) >= 0
                                                 select ct.SoTien).Sum().GetValueOrDefault()
                               ).Sum().GetValueOrDefault();
                
                //Nap du lieu
                cTenCT.Text = objKH.CongTyQuanLy;
                cTenKH.Text = "Khách hàng: " + objKH.TenKH;
                cThoiGian.Text = string.Format("Từ ngày {0:dd/MM/yyyy}, đến ngày {1:dd/MM/yyyy}", _TuNgay, _DenNgay);
                //
                cDauKy.Text = _DauKy.ToString("n0");
                //
                cNgayCT.DataBindings.Add("Text", null, "NgayCT", "{0:dd/MM/yyyy}");
                cSoCT.DataBindings.Add("Text", null, "SoCT");
                cDienGiai.DataBindings.Add("Text", null, "DienGiai");
                cPSNO.DataBindings.Add("Text", null, "PhatSinhNo", "{0:n0}");
                cPSCO.DataBindings.Add("Text", null, "PhatSinhCo", "{0:n0}");
                csumPSNO.DataBindings.Add("Text", null, "PhatSinhNo", "{0:n0}");
                csumPSCO.DataBindings.Add("Text", null, "PhatSinhCo", "{0:n0}");
                csumPSNO.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:n0}");
                csumPSCO.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:n0}");
                this.DataSource = ltPhatSinh;
                //
                cCuoiKy.Text = _CuoiKy.ToString("n0");
            }
            catch { }
            //finally
            //{
            //    db.Dispose();
            //} 
        }

    }
}
