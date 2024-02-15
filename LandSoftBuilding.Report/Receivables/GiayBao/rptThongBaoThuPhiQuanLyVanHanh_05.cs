using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
using Library;
using System.Data.Linq.SqlClient;

namespace LandSoftBuilding.Receivables.GiayBao
{
    public partial class RptThongBaoThuPhiQuanLyVanHanh05 : XtraReport
    {
        public RptThongBaoThuPhiQuanLyVanHanh05(byte maTn, int maKh, int thang, int nam, int _MaTKNH)
        {
            InitializeComponent();

            Library.frmPrintControl.LoadLayout(this, 9, maTn);

            #region Bingding
            //cSTT.DataBindings.Add("Text", null, "STT");
            //cSTT.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.RecordNumber, "{0:#}");
            //cTenDM.DataBindings.Add("Text", null, "SoLuong");
            ////cSoLuong.DataBindings.Add("Text", null, "SoLuong", "{0:#,0.##}");
            //cDonGia.DataBindings.Add("Text", null, "DonGia", "{0:#,0.##}");
            //cThanhTien.DataBindings.Add("Text", null, "ThanhTien", "{0:#,0.##}");
            cMaHopDong.DataBindings.Add("Text", null, "MaSoMB");
            cDienTichNen.DataBindings.Add("Text", null, "DienTichNen");
            cDonGia.DataBindings.Add("Text", null, "DonGia", "{0:#,0.##}");
            cThanhTien.DataBindings.Add("Text", null, "ConNo", "{0:#,0.##}");
            cNoCu.DataBindings.Add("Text", null, "NoDauKy", "{0:#,0.##}");
            cTongTien.DataBindings.Add("Text", null, "TongTien", "{0:#,0.##}");

            cSumTongTien.DataBindings.Add("Text", null, "TongTien", "{0:#,0.##}");
            cSumTongTien.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,0.##}");
            #endregion

            var db = new MasterDataContext();
            var ctlRTF = new DevExpress.XtraRichEdit.RichEditControl();
            var objTn = (db.tnToaNhas.Where(tn => tn.MaTN == maTn).Select(tn => new {tn.TenTN, tn.CongTyQuanLy, tn.Logo})).FirstOrDefault();

            xrLabel5.Text = string.Format(xrLabel5.Text, thang, nam);
            xrLabel6.Text = string.Format(xrLabel6.Text, thang, nam);

            var ngay = Common.GetLastDayOfMonth(thang, nam);

            var month = new DateTime(nam, thang, ngay.Day);
            var lastMonth =month.AddMonths(-1);


            var objKh = (from mb in db.mbMatBangs
                join kh in db.tnKhachHangs on mb.MaKH equals kh.MaKH
                where mb.MaKH == maKh
                select new
                {
                    TenKH = kh.IsCaNhan == true ? (kh.HoKH + " " + kh.TenKH) : kh.CtyTen,
                    mb.MaSoMB,
                    kh.ThuTruoc,
                    kh.MaPhu
                }).FirstOrDefault();
            
            try
            {
                if (objKh != null && objTn != null)
                {
                    cBanQuanLy.Text = "BAN QUẢN LÝ KHU DÂN CƯ " + objTn.TenTN;
                    cKhachHang.Text = objKh.TenKH;
                    cMaNen.Text = objKh.MaSoMB;
                    cThongBao.Text =
                        "Ban quản lý Khu dân cư " + objTn.TenTN + " xin thông báo phí quản lý vận hành tháng " + thang + "/" + nam + " của Quý cư dân như sau:";
                    var objTK = (from tk in db.nhTaiKhoans
                        join nh in db.nhNganHangs on tk.MaNH equals nh.ID
                        where tk.ID == _MaTKNH
                        select new { tk.ChuTK, tk.SoTK, nh.TenNH,tk.DienGiai }).Single();
                    cSoTK.Text = "Số tài khoản: "+objTK.SoTK+" tại ngân hàng: "+objTK.TenNH+" - "+objTK.DienGiai;
                    cNoiDungChuyenKhoan.Text =
                        "Nội dung chuyển khoản:   Mã căn_"+objKh.MaSoMB+"_thanh toán phí tháng "+thang+"/"+nam;

                    var list = (from hd in db.dvHoaDons
                                join l in db.dvLoaiDichVus on hd.MaLDV equals l.ID
                                join mb in db.mbMatBangs on hd.MaMB equals mb.MaMB into tblMatBang
                                from mb in tblMatBang.DefaultIfEmpty()
                                join lmb in db.mbLoaiMatBangs on mb.MaLMB equals lmb.MaLMB into loaimb
                                from lmb in loaimb.DefaultIfEmpty()
                                join tl in db.mbTangLaus on mb.MaTL equals tl.MaTL into tblTangLau
                                from tl in tblTangLau.DefaultIfEmpty()
                                join kn in db.mbKhoiNhas on tl.MaKN equals kn.MaKN into tblKhoiNha
                                from kn in tblKhoiNha.DefaultIfEmpty()
                                join dvk in db.dvDichVuKhacs on mb.MaMB equals dvk.MaMB
                                where hd.MaKH == maKh
                                & hd.NgayTT.Value.Month == thang
                                & hd.IsDuyet == true
                                      & l.ID == 13 & dvk.MaLDV == 13
                                orderby hd.NgayTT descending
                                select new
                                {
                                    MaDichVu = l.ID,
                                    mb.MaSoMB,
                                    DienTichNen = mb.MaSoMB,
                                    DonGia = dvk.DonGia,
                                    ConNo = hd.PhaiThu,// dvk.SoLuong * dvk.DonGia,
                                    NoDauKy = (from hdd in db.dvHoaDons
                                               where hdd.MaKH == maKh & SqlMethods.DateDiffDay(hdd.NgayTT, lastMonth) >= 0 &
                                                     hdd.IsDuyet == true & (hdd.MaLDV == 13 || hdd.MaLDV == 69)
                                               select hdd.PhaiThu - (from ct in db.SoQuy_ThuChis
                                                                     where ct.TableName == "dvHoaDon" & ct.LinkID == hdd.ID

                                                                                                      & SqlMethods.DateDiffDay(
                                                                                                          ct.NgayPhieu, lastMonth) >= 0
                                                                                                      & (hdd.MaLDV == 13 || hdd.MaLDV == 69)
                                                                     select new { SoTien = ct.DaThu - ct.KhauTru }).Sum(s => s.SoTien).GetValueOrDefault()
                                        ).Sum().GetValueOrDefault(),
                                    TongTien =hd.PhaiThu// (dvk.SoLuong * dvk.DonGia)
                                    + ((from hdd in db.dvHoaDons
                                        where hdd.MaKH == maKh & SqlMethods.DateDiffDay(hdd.NgayTT, lastMonth) >= 0 &
                                              hdd.IsDuyet == true & (hdd.MaLDV == 13 || hdd.MaLDV == 69)
                                        select hdd.PhaiThu - (from ct in db.SoQuy_ThuChis
                                                              where ct.TableName == "dvHoaDon" & ct.LinkID == hdd.ID

                                                                                               & SqlMethods.DateDiffDay(
                                                                                                   ct.NgayPhieu, lastMonth) >= 0
                                                                                               & (hdd.MaLDV == 13 || hdd.MaLDV == 69)
                                                              select new { SoTien = ct.DaThu + ct.KhauTru }).Sum(s=>s.SoTien).GetValueOrDefault()
                                        ).Sum().GetValueOrDefault())
                                }).AsEnumerable().Select((p, index) => new
                                {
                                    STT = index + 1,
                                    p.MaSoMB,
                                    p.DienTichNen,
                                    p.DonGia,
                                    p.ConNo,
                                    NoDauKy = p.ConNo == 0 ? 0 : p.NoDauKy,
                                    p.TongTien
                                }).ToList();

                    if (list.Count() <= 0)
                    {
                        // tháng đó không có phát sinh, thành tiền =0
                        var list2 = (from hd in db.dvHoaDons
                                     join l in db.dvLoaiDichVus on hd.MaLDV equals l.ID
                                     join mb in db.mbMatBangs on hd.MaMB equals mb.MaMB into tblMatBang
                                     from mb in tblMatBang.DefaultIfEmpty()
                                     join lmb in db.mbLoaiMatBangs on mb.MaLMB equals lmb.MaLMB into loaimb
                                     from lmb in loaimb.DefaultIfEmpty()
                                     join tl in db.mbTangLaus on mb.MaTL equals tl.MaTL into tblTangLau
                                     from tl in tblTangLau.DefaultIfEmpty()
                                     join kn in db.mbKhoiNhas on tl.MaKN equals kn.MaKN into tblKhoiNha
                                     from kn in tblKhoiNha.DefaultIfEmpty()
                                     join dvk in db.dvDichVuKhacs on mb.MaMB equals dvk.MaMB
                                     where hd.MaKH == maKh
                                         & SqlMethods.DateDiffDay(hd.NgayTT, ngay) >= 0
                                     & hd.IsDuyet == true
                                           & l.ID == 13 & dvk.MaLDV == 13

                                     orderby hd.NgayTT descending
                                     select new
                                     {
                                         MaDichVu = l.ID,
                                         mb.MaSoMB,
                                         DienTichNen = mb.MaSoMB,
                                         DonGia = dvk.DonGia,
                                         ConNo = 0,
                                         NoDauKy = (from hdd in db.dvHoaDons
                                                    where hdd.MaKH == maKh & SqlMethods.DateDiffDay(hdd.NgayTT, lastMonth) >= 0 &
                                                          hdd.IsDuyet == true & (hdd.MaLDV == 13 || hdd.MaLDV == 69)
                                                    select hdd.PhaiThu - (from ct in db.SoQuy_ThuChis
                                                                          where ct.TableName == "dvHoaDon" & ct.LinkID == hdd.ID

                                                                                                           & SqlMethods.DateDiffDay(
                                                                                                               ct.NgayPhieu, lastMonth) >= 0
                                                                                                           & (hdd.MaLDV == 13 || hdd.MaLDV == 69)
                                                                          select new { SoTien = ct.DaThu - ct.KhauTru }).Sum(s => s.SoTien).GetValueOrDefault()
                                             ).Sum().GetValueOrDefault(),
                                         TongTien =
                                          ((from hdd in db.dvHoaDons
                                            where hdd.MaKH == maKh & SqlMethods.DateDiffDay(hdd.NgayTT, lastMonth) >= 0 &
                                                  hdd.IsDuyet == true & (hdd.MaLDV == 13 || hdd.MaLDV == 69)
                                            select hdd.PhaiThu - (from ct in db.SoQuy_ThuChis
                                                                  where ct.TableName == "dvHoaDon" & ct.LinkID == hdd.ID

                                                                                                   & SqlMethods.DateDiffDay(
                                                                                                       ct.NgayPhieu, lastMonth) >= 0
                                                                                                   & (hdd.MaLDV == 13 || hdd.MaLDV == 69)
                                                                  select new { SoTien = ct.DaThu - ct.KhauTru }).Sum(s => s.SoTien).GetValueOrDefault()
                                             ).Sum().GetValueOrDefault())
                                     }).ToList();
                        DataSource = (from p in list2
                                      group new { p } by new {p.MaSoMB, p.DienTichNen, p.DonGia, p.ConNo, p.NoDauKy, p.TongTien } into g
                                      select new
                                      {
                                          g.Key.MaSoMB,
                                          g.Key.DienTichNen,
                                          g.Key.DonGia,
                                          g.Key.ConNo,
                                          g.Key.NoDauKy,
                                          g.Key.TongTien
                                      }).AsEnumerable().Select((q, index) => new
                                      {
                                          STT = index + 1,
                                          q.MaSoMB,
                                          q.DienTichNen,
                                          q.DonGia,
                                          q.ConNo,
                                          NoDauKy = q.ConNo == 0 ? 0 : q.NoDauKy,
                                          TongTien = q.ConNo == 0 ? 0 : q.TongTien
                                      }).ToList();
                    }
                    else
                    {
                        DataSource = list;
                    }
                }
            
            }
            catch { }
            finally
            {
                db.Dispose();
            }
        }
    }
}
