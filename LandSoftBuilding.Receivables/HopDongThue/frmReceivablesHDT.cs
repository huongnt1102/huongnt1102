using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Data.Linq.SqlClient;
using Library;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;
using LinqToExcel.Extensions;
using DevExpress.XtraReports.UI;
//using Building.Template;

namespace LandSoftBuilding.Receivables
{
    public partial class frmReceivablesHDT : DevExpress.XtraEditors.XtraForm
    {
        public bool IsHDThue = false;
        public frmReceivablesHDT()
        {
            InitializeComponent();
        }

        void LoadData()
        {

            DevExpress.Data.Helpers.ServerModeCore.DefaultForceCaseInsensitiveForAnySource = true;
            gcHoaDon.DataSource = null;

            //gcHoaDon.DataSource = linqInstantFeedbackSource1;

            var db = new MasterDataContext();
            db.CommandTimeout = 100000;
            var _MaTN = (byte)itemToaNha.EditValue;
            var _Thang = Convert.ToInt32(itemThang.EditValue);
            var _Nam = Convert.ToInt32(itemNam.EditValue);
            var _TuNgay = new DateTime(_Nam, _Thang, 1);
            var _DenNgay = Common.GetLastDayOfMonth(_Thang, _Nam);
            var _NgayHienTai = DateTime.Now;


            var No = (from hd in db.dvHoaDons

                      join ltt in db.ctLichThanhToans on new { hd.TableName, hd.LinkID } equals new { TableName = "ctLichThanhToan", LinkID = (int?)ltt.ID }
                      where SqlMethods.DateDiffDay(hd.NgayTT, _TuNgay) > 0
                      & hd.IsDuyet == true
                      & hd.IsHDThue.GetValueOrDefault() == IsHDThue
                      select new
                      {
                          hd.MaKH,
                          MaHD = ltt != null ? ltt.MaHD :  hd.idctHopDong,
                          PhaiThu = hd.PhaiThu
                                       - (from kt in db.ptPhieuThus
                                          join ctkt in db.ptChiTietPhieuThus
                                              on kt.ID equals ctkt.MaPT
                                          where SqlMethods.DateDiffDay(kt.NgayThu, _TuNgay) > 0 & ctkt.LinkID == hd.ID
                                          select ctkt.SoTien
                                       ).Sum().GetValueOrDefault()

                                       - (from kt in db.ktttChiTietHDTs
                                          join ctkt in db.ktttKhauTruThuTruocHDTs
                                              on kt.MaCT equals ctkt.ID
                                          where SqlMethods.DateDiffDay(ctkt.NgayCT, _TuNgay) > 0 & kt.LinkID == hd.ID
                                          select kt.SoTien
                                       ).Sum().GetValueOrDefault()
                      }).Where(p => p.PhaiThu > 0);

            var SysDate = db.GetSystemDate();

            var _DenNgayLai = SqlMethods.DateDiffDay(SysDate, _DenNgay) > 0 ? SysDate : _DenNgay;

            //var ltTienLai = (from p in db.dvTienLaiNopCham_TheoThangs
            //                 join ltt in db.ctLichThanhToans on p.MaLTT equals ltt.ID
            //                 where SqlMethods.DateDiffMonth(p.TuNgay, _TuNgay) >= 0
            //                 select new
            //                 {
            //                     ltt.MaHD,
            //                     TuNgay = p.TuNgay,
            //                     DenNgay = p.DenNgay == null ? _DenNgayLai : p.DenNgay,
            //                     p.SoTienTinhLai,
            //                     p.LaiSuat,
            //                     p.TienLai,
            //                 });

            var ltTienLaiDaThu = (from ct in db.ptChiTietPhieuThus
                                  join pt in db.ptPhieuThus on ct.MaPT equals pt.ID
                                  join ltt in db.ctLichThanhToans on ct.LinkID equals ltt.ID
                                  where ct.TableName == "ctLichThanhToan"
                                  select new
                                  {
                                      MaHD = ltt.MaHD,
                                      NgayThu = pt.NgayThu,
                                      SoTien = ct.SoTien
                                  }).Union
                                  (from ct in db.ktttChiTietHDTs
                                   join pt in db.ktttKhauTruThuTruocHDTs on ct.MaCT equals pt.ID
                                   join ltt in db.ctLichThanhToans on ct.LinkID equals ltt.ID
                                   where ct.TableName == "ctLichThanhToan"
                                   select new
                                   {
                                       MaHD = ltt.MaHD,
                                       NgayThu = pt.NgayCT,
                                       SoTien = ct.SoTien
                                   });

            var _Ngay = new DateTime(_Nam, _Thang, 1);

            gcHoaDon.DataSource = (from hdong in db.ctHopDongs
                                   join kh in db.tnKhachHangs on hdong.MaKH equals kh.MaKH
                                   where kh.MaTN == _MaTN
                                   select new
                                   {
                                       // Thêm mã công nợ Conic
                                       MaCN = string.Format("{0}.{1}.{2}", _Thang.ToString().PadLeft(2, '0'), _Nam.ToString().Substring(2, 2), kh.KyHieu),

                                       kh.MaKH,

                                       MaHD = hdong.ID,

                                       hdong.SoHDCT,

                                       hdong.NgayKy,

                                       kh.KyHieu,

                                       kh.MaPhu,

                                       TenKH = kh.IsCaNhan == true ? (kh.HoKH + " " + kh.TenKH) : kh.CtyTen,

                                       DienThoai = kh.DienThoaiKH,

                                       EmailKH =  kh.EmailKH,

                                       DiaChi = kh.DCLL,

                                       //TrangThai = db.LichSuXuatHoaDons.FirstOrDefault(p => p.MaKH == kh.MaKH & p.ThangCongNo.Value.Month == _Thang & p.ThangCongNo.Value.Year == _Nam) == null ? "Chưa xuất hóa đơn" : "Đã xuất hóa đơn",

                                       //MaMB = db.dvHoaDons.First(p => p.MaKH == kh.MaKH & p.IsHDThue.GetValueOrDefault() == IsHDThue & SqlMethods.DateDiffDay(p.NgayTT, _DenNgay) >= 0 & p.IsDuyet == true & p.MaMB != null).MaMB,
                                       MaMB = db.ctChiTiets.Where(o => o.MaHDCT == hdong.ID).FirstOrDefault().MaMB,

                                       TenLMB = db.mbMatBangs.SingleOrDefault(p => p.MaMB == db.dvHoaDons.First(k => k.MaKH == kh.MaKH & k.IsHDThue.GetValueOrDefault() == IsHDThue & SqlMethods.DateDiffDay(k.NgayTT, _DenNgay) >= 0 & k.IsDuyet == true & k.MaMB != null).MaMB).mbLoaiMatBang.TenLMB,

                                       NoDauKy = (decimal)No.Where(p => p.MaKH == kh.MaKH & p.MaHD == hdong.ID).Sum(p => p.PhaiThu).GetValueOrDefault(),

                                       PhatSinh = (from hd in db.dvHoaDons
                                                   join ltt in db.ctLichThanhToans on new { hd.TableName, hd.LinkID } equals new { TableName = "ctLichThanhToan", LinkID = (int?)ltt.ID }
                                                   into lichthanhtoan
                                                   from ltt in lichthanhtoan.DefaultIfEmpty()
                                                   where hd.MaKH == kh.MaKH
                                                   & ((ltt != null && ltt.MaHD == hdong.ID) | (hd.idctHopDong == hdong.ID))
                                                   & hd.IsHDThue.GetValueOrDefault() == IsHDThue
                                                   & SqlMethods.DateDiffMonth(hd.NgayTT, _TuNgay) == 0
                                                   & hd.IsDuyet == true
                                                   select hd.PhaiThu
                                                   ).Sum().GetValueOrDefault(),

                                       DaThu = (from ct in db.ptChiTietPhieuThus
                                                join pt in db.ptPhieuThus on ct.MaPT equals pt.ID
                                                join hd in db.dvHoaDons on new { ct.TableName, ct.LinkID } equals new { TableName = "dvHoaDon", LinkID = (long?)hd.ID }

                                                join ltt in db.ctLichThanhToans on new { hd.TableName, hd.LinkID } equals new { TableName = "ctLichThanhToan", LinkID = (int?)ltt.ID }
                                                into lichthanhtoan
                                                from ltt in lichthanhtoan.DefaultIfEmpty()

                                                where hd.MaKH == kh.MaKH & SqlMethods.DateDiffMonth(pt.NgayThu, _TuNgay) == 0
                                                & hd.IsDuyet == true
                                               & ((ltt != null && ltt.MaHD == hdong.ID) | (hd.idctHopDong == hdong.ID))
                                                & SqlMethods.DateDiffDay(hd.NgayTT, _DenNgay) >= 0
                                                & hd.IsHDThue.GetValueOrDefault() == IsHDThue
                                                select ct.SoTien).Sum().GetValueOrDefault(),

                                       KhauTruHDT = (from ct in db.ktttChiTietHDTs
                                                     join pt in db.ktttKhauTruThuTruocHDTs on ct.MaCT equals pt.ID
                                                     join hd in db.dvHoaDons on new { ct.TableName, ct.LinkID } equals new { TableName = "dvHoaDon", LinkID = (long?)hd.ID }
                                                     join ltt in db.ctLichThanhToans on new { hd.TableName, hd.LinkID } equals new { TableName = "ctLichThanhToan", LinkID = (int?)ltt.ID }
                                                     into lichthanhtoan
                                                     from ltt in lichthanhtoan.DefaultIfEmpty()
                                                     where hd.MaKH == kh.MaKH & hd.IsHDThue.GetValueOrDefault() == IsHDThue & SqlMethods.DateDiffDay(hd.NgayTT, _DenNgay) >= 0 & SqlMethods.DateDiffMonth(pt.NgayCT, _TuNgay) == 0 & hd.IsDuyet == true
                                                     & ((ltt != null && ltt.MaHD == hdong.ID) | hd.idctHopDong == hdong.ID)
                                                     select ct.SoTien).Sum().GetValueOrDefault(),

                                       ConNo = (from hd in db.dvHoaDons
                                                join ltt in db.ctLichThanhToans on new { hd.TableName, hd.LinkID } equals new { TableName = "ctLichThanhToan", LinkID = (int?)ltt.ID } into lichthanhtoan
                                                from ltt in lichthanhtoan.DefaultIfEmpty()

                                                where hd.MaKH == kh.MaKH & SqlMethods.DateDiffDay(hd.NgayTT, _DenNgay) >= 0 & hd.IsDuyet == true & hd.IsHDThue.GetValueOrDefault() == IsHDThue
                                               & ((ltt != null && ltt.MaHD == hdong.ID) | (hd.idctHopDong == hdong.ID))
                                                select hd.PhaiThu
                                                       -
                                                       (from ct in db.ptChiTietPhieuThus
                                                        join pt in db.ptPhieuThus on ct.MaPT equals pt.ID
                                                        where ct.LinkID == hd.ID & SqlMethods.DateDiffDay(pt.NgayThu, _DenNgay) >= 0
                                                        select ct.SoTien).Sum().GetValueOrDefault()
                                                        -
                                                        (from ct in db.ktttChiTietHDTs
                                                         join pt in db.ktttKhauTruThuTruocHDTs on ct.MaCT equals pt.ID
                                                         where ct.LinkID == hd.ID & SqlMethods.DateDiffDay(pt.NgayCT, _DenNgay) >= 0
                                                         select ct.SoTien).Sum().GetValueOrDefault()
                                                ).Sum().GetValueOrDefault(),

                                       ThuTruocHDT = (from pt in db.ptPhieuThus
                                                      where pt.MaKH == kh.MaKH & pt.MaPL == 22 & SqlMethods.DateDiffDay(pt.NgayThu, _DenNgay) >= 0
                                                      & pt.idctHopDong == hdong.ID
                                                      select pt.SoTien - db.ptPhieuThus.Where(o => o.idPhieuThuGoc == pt.ID).Sum(o => o.SoTien).GetValueOrDefault()).Sum().GetValueOrDefault()
                                                      -
                                                      (from pt in db.ktttKhauTruThuTruocHDTs
                                                       where pt.MaKH == kh.MaKH & SqlMethods.DateDiffDay(pt.NgayCT, _DenNgay) >= 0
                                                       & pt.idctHopDong == hdong.ID
                                                       & pt.MaPL == 22
                                                       select pt.SoTien).Sum().GetValueOrDefault()
                                                      -
                                                      (from pt in db.pcPhieuChis
                                                       where pt.MaNCC == kh.MaKH & SqlMethods.DateDiffDay(pt.NgayChi, _DenNgay) >= 0 & pt.LoaiChi == 8
                                                       & pt.idctHopDong == hdong.ID
                                                       select pt.SoTien).Sum().GetValueOrDefault(),

                                       //TienLai_PhatSinh = ltTienLai.Where(o => o.MaHD == hdong.ID & SqlMethods.DateDiffMonth(o.TuNgay, _TuNgay) == 0 & o.TienLai.GetValueOrDefault() > 1000)
                                                          //.Sum(o => ((SqlMethods.DateDiffDay(o.TuNgay.Value, o.DenNgay) + 1) * o.LaiSuat * o.SoTienTinhLai)).GetValueOrDefault(),

                                       //TienLai_NoDK = ltTienLai.Where(o => o.MaHD == hdong.ID & SqlMethods.DateDiffMonth(o.DenNgay, _TuNgay) > 0)
                                                      //.Sum(o => ((SqlMethods.DateDiffDay(o.TuNgay.Value, o.DenNgay) + 1) * o.LaiSuat * o.SoTienTinhLai)).GetValueOrDefault(),

                                       //TienLai_DaThuDK = ltTienLaiDaThu.Where(o => o.MaHD == hdong.ID & SqlMethods.DateDiffDay(o.NgayThu, _TuNgay) > 0).Sum(o => o.SoTien).GetValueOrDefault(),

                                       //TienLai_DaThu = ltTienLaiDaThu.Where(o => o.MaHD == hdong.ID & SqlMethods.DateDiffMonth(o.NgayThu, _TuNgay) == 0).Sum(o => o.SoTien).GetValueOrDefault(),

                                       //TienLai_DieuChinhTK = (decimal?) hdong.ctLichThanhToans.Where(o => SqlMethods.DateDiffMonth(o.Ngay_hieuchinh,_TuNgay) == 0).Sum(o => o.SoTien_hieuchinh.GetValueOrDefault()),

                                       //TienLai_DieuChinhDK =(decimal?) hdong.ctLichThanhToans.Where(o => SqlMethods.DateDiffMonth(o.Ngay_hieuchinh, _TuNgay) > 0).Sum(o => o.SoTien_hieuchinh.GetValueOrDefault()),

                                   }).AsEnumerable()
                                   .Select(o => new
                                   {
                                       o.MaCN,

                                       o.MaKH,

                                       o.MaHD,

                                       o.SoHDCT,

                                       o.NgayKy,

                                       o.KyHieu,

                                       o.MaPhu,

                                       o.TenKH,

                                       o.DienThoai,

                                       o.EmailKH,

                                       o.DiaChi,

                                       //o.TrangThai,

                                       o.MaMB,

                                       o.TenLMB,

                                       o.NoDauKy,

                                       o.PhatSinh,

                                       o.DaThu,

                                       o.KhauTruHDT,

                                       o.ConNo,

                                       o.ThuTruocHDT,

                                       //TienLai_DieuChinh = o.TienLai_DieuChinhTK.GetValueOrDefault(),

                                       //TienLai_PhatSinh = Math.Round(o.TienLai_PhatSinh, 0),

                                       //TienLai_NoDK = Math.Round(o.TienLai_NoDK, 0) - Math.Round(o.TienLai_DaThuDK, 0) + o.TienLai_DieuChinhDK.GetValueOrDefault(),

                                       //TienLai_DaThu = Math.Round(o.TienLai_DaThu, 0),

                                       //TienLai_ConNo = Math.Round(o.TienLai_NoDK, 0)
                                       //                +
                                       //                o.TienLai_DieuChinhDK.GetValueOrDefault()
                                       //                -
                                       //                Math.Round(o.TienLai_DaThuDK, 0)
                                       //                +
                                       //                Math.Round(o.TienLai_PhatSinh, 0)
                                       //                +
                                       //                o.TienLai_DieuChinhTK.GetValueOrDefault()
                                       //                -
                                       //                Math.Round(o.TienLai_DaThu, 0),

                                       CSH = db.tnKhachHangs.Where(p => p.MaKH == db.mbMatBangs.Where(m => m.MaMB == o.MaMB).FirstOrDefault().MaKH)
                                                            .Select(kh => kh.IsCaNhan == true ? (kh.HoKH + " " + kh.TenKH) : kh.CtyTen).FirstOrDefault(),
                                   });

           
        }

        void LoadData1Dong(int MaKH)
        {

            DevExpress.Data.Helpers.ServerModeCore.DefaultForceCaseInsensitiveForAnySource = true;

            var db = new MasterDataContext();
            db.CommandTimeout = 100000;
            var _MaTN = (byte)itemToaNha.EditValue;
            var _Thang = Convert.ToInt32(itemThang.EditValue);
            var _Nam = Convert.ToInt32(itemNam.EditValue);
            var _TuNgay = new DateTime(_Nam, _Thang, 1);
            var _DenNgay = Common.GetLastDayOfMonth(_Thang, _Nam);
            var _NgayHienTai = DateTime.Now;
            var No = (from hd in db.dvHoaDons
                      where SqlMethods.DateDiffDay(hd.NgayTT, _TuNgay) > 0 & hd.IsDuyet == true
                      & hd.MaKH == MaKH
                      & hd.IsHDThue.GetValueOrDefault() == IsHDThue
                      select new
                      {
                          hd.MaKH,
                          PhaiThu = hd.PhaiThu
                                       - (from kt in db.ptPhieuThus
                                          join ctkt in db.ptChiTietPhieuThus
                                              on kt.ID equals ctkt.MaPT
                                          where SqlMethods.DateDiffDay(kt.NgayThu, _TuNgay) > 0 & ctkt.LinkID == hd.ID
                                          select ctkt.SoTien
                                       ).Sum().GetValueOrDefault()
                                       - (from kt in db.ktttChiTiets
                                          join ctkt in db.ktttKhauTruThuTruocs
                                              on kt.MaCT equals ctkt.ID
                                          where SqlMethods.DateDiffDay(ctkt.NgayCT, _TuNgay) > 0 & kt.LinkID == hd.ID
                                          select kt.SoTien
                                       ).Sum().GetValueOrDefault()
                                       //- (from kt in db.ktttChiTietGYMs
                                       //   join ctkt in db.ktttKhauTruThuTruocGYMs
                                       //       on kt.MaCT equals ctkt.ID
                                       //   where SqlMethods.DateDiffDay(ctkt.NgayCT, _TuNgay) > 0 & kt.LinkID == hd.ID
                                       //   select kt.SoTien
                                       //).Sum().GetValueOrDefault()
                                       // - (from kt in db.ktttChiTietBBQs
                                       //    join ctkt in db.ktttKhauTruThuTruocBBQs
                                       //        on kt.MaCT equals ctkt.ID
                                       //    where SqlMethods.DateDiffDay(ctkt.NgayCT, _TuNgay) > 0 & kt.LinkID == hd.ID
                                       //    select kt.SoTien
                                       //).Sum().GetValueOrDefault()
                                       // - (from kt in db.ktttChiTietTheBois
                                       //    join ctkt in db.ktttKhauTruThuTruocTheBois
                                       //        on kt.MaCT equals ctkt.ID
                                       //    where SqlMethods.DateDiffDay(ctkt.NgayCT, _TuNgay) > 0 & kt.LinkID == hd.ID
                                       //    select kt.SoTien
                                       //).Sum().GetValueOrDefault()
                                       // - (from kt in db.ktttChiTietThiCongs
                                       //    join ctkt in db.ktttKhauTruThuTruocThiCongs
                                       //        on kt.MaCT equals ctkt.ID
                                       //    where SqlMethods.DateDiffDay(ctkt.NgayCT, _TuNgay) > 0 & kt.LinkID == hd.ID
                                       //    select kt.SoTien
                                       //).Sum().GetValueOrDefault()
                      }).Where(p => p.PhaiThu > 0);

            var tam = (from kh in db.tnKhachHangs
                       where kh.MaTN == _MaTN & kh.MaKH == MaKH
                       select new
                       {
                           // Thêm mã công nợ Conic
                           MaCN = string.Format("{0}.{1}.{2}", _Thang.ToString().PadLeft(2, '0'), _Nam.ToString().Substring(2, 2), kh.KyHieu),
                           kh.MaKH,
                           kh.KyHieu,
                           kh.MaPhu,
                           TenKH = kh.IsCaNhan == true ? (kh.HoKH + " " + kh.TenKH) : kh.CtyTen,
                           DienThoai = kh.DienThoaiKH,
                           EmailKH = kh.EmailKH ,
                           DiaChi = kh.DCLL,
                           //TrangThai = db.LichSuXuatHoaDons.FirstOrDefault(p => p.MaKH == kh.MaKH & p.ThangCongNo.Value.Month == _Thang & p.ThangCongNo.Value.Year == _Nam) == null ? "Chưa xuất hóa đơn" : "Đã xuất hóa đơn",

                           MaMB = db.dvHoaDons.First(p => p.MaKH == kh.MaKH & p.IsHDThue == IsHDThue & SqlMethods.DateDiffDay(p.NgayTT, _DenNgay) >= 0 & p.IsDuyet == true & p.MaMB != null).MaMB,
                           TenLMB = db.mbMatBangs.SingleOrDefault(p => p.MaMB == db.dvHoaDons.First(k => k.MaKH == kh.MaKH & k.IsHDThue == IsHDThue & SqlMethods.DateDiffDay(k.NgayTT, _DenNgay) >= 0 & k.IsDuyet == true & k.MaMB != null).MaMB).mbLoaiMatBang.TenLMB,
                           NoDauKy = (decimal)No.Where(p => p.MaKH == kh.MaKH).Sum(p => p.PhaiThu).GetValueOrDefault(),

                           PhatSinh = (from hd in db.dvHoaDons
                                       where hd.MaKH == kh.MaKH & hd.IsHDThue.GetValueOrDefault() == IsHDThue & SqlMethods.DateDiffMonth(hd.NgayTT, _TuNgay) == 0 & hd.IsDuyet == true
                                       select hd.PhaiThu).Sum().GetValueOrDefault(),


                           DaThu = (from ct in db.ptChiTietPhieuThus
                                    join pt in db.ptPhieuThus on ct.MaPT equals pt.ID
                                    join hd in db.dvHoaDons on new { ct.TableName, ct.LinkID } equals new { TableName = "dvHoaDon", LinkID = (long?)hd.ID }
                                    where hd.MaKH == kh.MaKH & hd.IsHDThue.GetValueOrDefault() == IsHDThue
                                    & SqlMethods.DateDiffMonth(pt.NgayThu, _TuNgay) == 0
                                    & SqlMethods.DateDiffDay(hd.NgayTT, _DenNgay) >= 0
                                    & hd.IsDuyet == true
                                    select ct.SoTien).Sum().GetValueOrDefault(),

                           KhauTru = (from ct in db.ktttChiTiets
                                      join pt in db.ktttKhauTruThuTruocs on ct.MaCT equals pt.ID
                                      join hd in db.dvHoaDons on new { ct.TableName, ct.LinkID } equals new { TableName = "dvHoaDon", LinkID = (long?)hd.ID }
                                      where hd.MaKH == kh.MaKH & hd.IsHDThue.GetValueOrDefault() == IsHDThue & SqlMethods.DateDiffMonth(pt.NgayCT, _TuNgay) == 0 & hd.IsDuyet == true
                                      select ct.SoTien).Sum().GetValueOrDefault(),

                           //KhauTruBBQ = (from ct in db.ktttChiTietBBQs
                           //              join pt in db.ktttKhauTruThuTruocBBQs on ct.MaCT equals pt.ID
                           //              join hd in db.dvHoaDons on new { ct.TableName, ct.LinkID } equals new { TableName = "dvHoaDon", LinkID = (long?)hd.ID }
                           //              where hd.MaKH == kh.MaKH & hd.IsHDThue.GetValueOrDefault() == IsHDThue & SqlMethods.DateDiffMonth(pt.NgayCT, _TuNgay) == 0 & hd.IsDuyet == true
                           //              select ct.SoTien).Sum().GetValueOrDefault(),

                           //KhauTruTheBoi = (from ct in db.ktttChiTietTheBois
                           //                 join pt in db.ktttKhauTruThuTruocTheBois on ct.MaCT equals pt.ID
                           //                 join hd in db.dvHoaDons on new { ct.TableName, ct.LinkID } equals new { TableName = "dvHoaDon", LinkID = (long?)hd.ID }
                           //                 where hd.MaKH == kh.MaKH & hd.IsHDThue.GetValueOrDefault() == IsHDThue & SqlMethods.DateDiffMonth(pt.NgayCT, _TuNgay) == 0 & hd.IsDuyet == true
                           //                 select ct.SoTien).Sum().GetValueOrDefault(),

                           //KhauTruGYM = (from ct in db.ktttChiTietGYMs
                           //              join pt in db.ktttKhauTruThuTruocGYMs on ct.MaCT equals pt.ID
                           //              join hd in db.dvHoaDons on new { ct.TableName, ct.LinkID } equals new { TableName = "dvHoaDon", LinkID = (long?)hd.ID }
                           //              where hd.MaKH == kh.MaKH & hd.IsHDThue.GetValueOrDefault() == IsHDThue & SqlMethods.DateDiffMonth(pt.NgayCT, _TuNgay) == 0 & hd.IsDuyet == true
                           //              select ct.SoTien).Sum().GetValueOrDefault(),

                           //KhauTruThiCong = (from ct in db.ktttChiTietThiCongs
                           //                  join pt in db.ktttKhauTruThuTruocThiCongs on ct.MaCT equals pt.ID
                           //                  join hd in db.dvHoaDons on new { ct.TableName, ct.LinkID } equals new { TableName = "dvHoaDon", LinkID = (long?)hd.ID }
                           //                  where hd.MaKH == kh.MaKH & hd.IsHDThue.GetValueOrDefault() == IsHDThue & SqlMethods.DateDiffMonth(pt.NgayCT, _TuNgay) == 0 & hd.IsDuyet == true
                           //                  select ct.SoTien).Sum().GetValueOrDefault(),

                           ThuTruoc = (from pt in db.ptPhieuThus
                                       where pt.MaKH == kh.MaKH & pt.MaPL == 2 & SqlMethods.DateDiffDay(pt.NgayThu, _DenNgay) >= 0
                                       select pt.SoTien).Sum().GetValueOrDefault()
                                      -
                                      (from pt in db.ktttKhauTruThuTruocs
                                       where pt.MaKH == kh.MaKH & SqlMethods.DateDiffDay(pt.NgayCT, _DenNgay) >= 0
                                       select pt.SoTien).Sum().GetValueOrDefault()
                                      -
                                      (from pt in db.pcPhieuChis
                                       where pt.MaNCC == kh.MaKH & SqlMethods.DateDiffDay(pt.NgayChi, _DenNgay) >= 0 & pt.LoaiChi == 6
                                       select pt.SoTien).Sum().GetValueOrDefault(),

                           //ThuTruocBBQ = (from pt in db.ptPhieuThus
                           //               where pt.MaKH == kh.MaKH & pt.MaPL == 19 & SqlMethods.DateDiffDay(pt.NgayThu, _DenNgay) >= 0
                           //               select pt.SoTien).Sum().GetValueOrDefault()
                           //               //-
                           //               //(from pt in db.ktttKhauTruThuTruocBBQs
                           //               // where pt.MaKH == kh.MaKH & SqlMethods.DateDiffDay(pt.NgayCT, _DenNgay) >= 0
                           //               // select pt.SoTien).Sum().GetValueOrDefault()
                           //               -
                           //               (from pt in db.pcPhieuChis
                           //                where pt.MaNCC == kh.MaKH & SqlMethods.DateDiffDay(pt.NgayChi, _DenNgay) >= 0 & pt.LoaiChi == 3
                           //                select pt.SoTien).Sum().GetValueOrDefault(),

                           ConNo = (from hd in db.dvHoaDons
                                    where hd.MaKH == kh.MaKH & SqlMethods.DateDiffDay(hd.NgayTT, _DenNgay) >= 0 & hd.IsDuyet == true & hd.IsHDThue.GetValueOrDefault() == IsHDThue
                                    select hd.PhaiThu
                                           -
                                           (from ct in db.ptChiTietPhieuThus
                                            join pt in db.ptPhieuThus on ct.MaPT equals pt.ID
                                            where ct.LinkID == hd.ID & SqlMethods.DateDiffDay(pt.NgayThu, _DenNgay) >= 0
                                            select ct.SoTien).Sum().GetValueOrDefault()
                                           -
                                           (from ct in db.ktttChiTietHDTs
                                            join pt in db.ktttKhauTruThuTruocHDTs on ct.MaCT equals pt.ID
                                            where ct.LinkID == hd.ID & SqlMethods.DateDiffDay(pt.NgayCT, _DenNgay) >= 0
                                            select ct.SoTien).Sum().GetValueOrDefault()
                                      ).Sum().GetValueOrDefault(),

                           //ThuTruocTheBoi = (from pt in db.ptPhieuThus
                           //                  where pt.MaKH == kh.MaKH & pt.MaPL == 17 & SqlMethods.DateDiffDay(pt.NgayThu, _DenNgay) >= 0
                           //                  select pt.SoTien).Sum().GetValueOrDefault()
                                            //-
                                            //(from pt in db.ktttKhauTruThuTruocTheBois
                                            // where pt.MaKH == kh.MaKH & SqlMethods.DateDiffDay(pt.NgayCT, _DenNgay) >= 0
                                            // select pt.SoTien).Sum().GetValueOrDefault()
                                            //-
                                            //(from pt in db.pcPhieuChis
                                            // where pt.MaNCC == kh.MaKH & SqlMethods.DateDiffDay(pt.NgayChi, _DenNgay) >= 0 & pt.LoaiChi == 2
                                            // select pt.SoTien).Sum().GetValueOrDefault(),
                       })
                       .Select(p => new CongNoListHDT()
                       {
                           ThuTruocHDT = p.ThuTruoc,
                           NoDauKy = p.NoDauKy,
                           PhatSinh = p.PhatSinh,
                           DaThu = p.DaThu,
                           ConNo = p.ConNo,
                           MaKH = p.MaKH,
                           KyHieu = p.KyHieu,
                           MaPhu = p.MaPhu,
                           TenKH = p.TenKH,
                           DienThoai = p.DienThoai,
                           EmailKH = p.EmailKH,
                           DiaChi = p.DiaChi,
                           MaMB = p.MaMB,
                           KhauTru = p.KhauTru,
                           //TrangThai = p.TrangThai,
                           TenLMB = p.TenLMB,
                       }).FirstOrDefault();

            gvHoaDon.SetFocusedRowCellValue("MaKH", tam.MaKH);
            gvHoaDon.SetFocusedRowCellValue("KyHieu", tam.KyHieu);
            gvHoaDon.SetFocusedRowCellValue("MaPhu", tam.MaPhu);
            gvHoaDon.SetFocusedRowCellValue("TenKH", tam.TenKH);
            gvHoaDon.SetFocusedRowCellValue("DienThoai", tam.DienThoai);
            gvHoaDon.SetFocusedRowCellValue("EmailKH", tam.EmailKH);
            gvHoaDon.SetFocusedRowCellValue("DiaChi", tam.DiaChi);
            gvHoaDon.SetFocusedRowCellValue("TrangThai", tam.TrangThai);
            gvHoaDon.SetFocusedRowCellValue("TenLMB", tam.TenLMB);

            gvHoaDon.SetFocusedRowCellValue("MaMB", tam.MaMB);

            gvHoaDon.SetFocusedRowCellValue("NoDauKy", tam.NoDauKy);
            gvHoaDon.SetFocusedRowCellValue("PhatSinh", tam.PhatSinh);
            gvHoaDon.SetFocusedRowCellValue("DaThu", tam.DaThu);
            gvHoaDon.SetFocusedRowCellValue("ConNo", tam.ConNo);
            gvHoaDon.SetFocusedRowCellValue("KhauTru", tam.KhauTru);
            gvHoaDon.SetFocusedRowCellValue("ThuTruocHDT", tam.ThuTruocHDT);
        }

        void RefreshData()
        {
            LoadData();
        }

        void Detail()
        {
            var db = new MasterDataContext();
            try
            {
                var maKH = (int?)gvHoaDon.GetFocusedRowCellValue("MaKH");
                var maHD = (int?)gvHoaDon.GetFocusedRowCellValue("MaHD");
                if (maKH == null)
                {
                    gcChiTiet.DataSource = null;
                    return;
                }

                switch (xtraTabControl1.SelectedTabPageIndex)
                {
                    case 0:
                        var thang = Convert.ToInt32(itemThang.EditValue);
                        var nam = Convert.ToInt32(itemNam.EditValue);
                        var ngay = Common.GetLastDayOfMonth(thang, nam);
                        var Moi = new DateTime(nam, thang, 1);

                        gcChiTiet.DataSource = (from hd in db.dvHoaDons

                                                join l in db.dvLoaiDichVus on hd.MaLDV equals l.ID

                                                join mb in db.mbMatBangs on hd.MaMB equals mb.MaMB into tblMatBang
                                                from mb in tblMatBang.DefaultIfEmpty()

                                                join lmb in db.mbLoaiMatBangs on mb.MaLMB equals lmb.MaLMB into loaimb
                                                from lmb in loaimb.DefaultIfEmpty()

                                                join tl in db.mbTangLaus on mb.MaTL equals tl.MaTL into tblTangLau
                                                from tl in tblTangLau.DefaultIfEmpty()

                                                join kn in db.mbKhoiNhas on tl.MaKN equals kn.MaKN into tblKhoiNha
                                                from kn in tblKhoiNha.DefaultIfEmpty()

                                                join ltt in db.ctLichThanhToans on new { hd.TableName, hd.LinkID } equals new { TableName = "ctLichThanhToan", LinkID = (int?)ltt.ID }
                                                into lichthanhtoan
                                                from ltt in lichthanhtoan.DefaultIfEmpty()
                                                where hd.MaKH == maKH
                                                & hd.IsDuyet == true
                                                & hd.IsHDThue.GetValueOrDefault() == IsHDThue
                                                & ((ltt != null && ltt.MaHD == maHD) | (hd.idctHopDong == maHD))
                                                orderby hd.NgayTT descending
                                                select new
                                                {
                                                    hd.IsDuyet,
                                                    hd.NgayTT,
                                                    TenLDV = l.TenHienThi,
                                                    hd.DienGiai,
                                                    hd.PhiDV,
                                                    hd.KyTT,
                                                    hd.TienTT,
                                                    hd.TyLeCK,
                                                    hd.TienCK,
                                                    hd.PhaiThu,

                                                    DaThu = (from ct in db.ptChiTietPhieuThus
                                                             join pt in db.ptPhieuThus on ct.MaPT equals pt.ID
                                                             where hd.ID == ct.LinkID
                                                             select ct.SoTien).Sum().GetValueOrDefault(),

                                                    ConNo = hd.PhaiThu
                                                            -
                                                            (from ct in db.ptChiTietPhieuThus
                                                             join pt in db.ptPhieuThus on ct.MaPT equals pt.ID
                                                             where ct.LinkID == hd.ID
                                                             select ct.SoTien).Sum().GetValueOrDefault()
                                                            -
                                                            (from ct in db.ktttChiTiets
                                                             join pt in db.ktttKhauTruThuTruocs on ct.MaCT equals pt.ID
                                                             where ct.LinkID == hd.ID
                                                             select ct.SoTien).Sum().GetValueOrDefault(),
                                                           // -
                                                           // (from ct in db.ktttChiTietBBQs
                                                           //  join pt in db.ktttKhauTruThuTruocBBQs on ct.MaCT equals pt.ID
                                                           //  where ct.LinkID == hd.ID
                                                           //  select ct.SoTien).Sum().GetValueOrDefault()
                                                           // -
                                                           // (from ct in db.ktttChiTietTheBois
                                                           //  join pt in db.ktttKhauTruThuTruocTheBois on ct.MaCT equals pt.ID
                                                           //  where ct.LinkID == hd.ID
                                                           //  select ct.SoTien).Sum().GetValueOrDefault()
                                                           //-
                                                           //(from ct in db.ktttChiTietGYMs
                                                           // join pt in db.ktttKhauTruThuTruocGYMs on ct.MaCT equals pt.ID
                                                           // where ct.LinkID == hd.ID
                                                           // select ct.SoTien).Sum().GetValueOrDefault()
                                                           //-
                                                           //(from ct in db.ktttChiTietThiCongs
                                                           // join pt in db.ktttKhauTruThuTruocThiCongs on ct.MaCT equals pt.ID
                                                           // where ct.LinkID == hd.ID
                                                           // select ct.SoTien).Sum().GetValueOrDefault(),

                                                    mb.MaSoMB,
                                                    mb.MaMB,
                                                    tl.TenTL,
                                                    kn.TenKN,
                                                    lmb.TenLMB,
                                                }).Where(p => p.ConNo > 0).ToList();
                        break;
                    case 1:
                        ctlMailHistory1.MaKH = maKH;
                        ctlMailHistory1.MailHistory_Load();
                        break;
                    case 2:
                        var thang1 = Convert.ToInt32(itemThang.EditValue);
                        var nam1 = Convert.ToInt32(itemNam.EditValue);
                        //gcXuatHDDT.DataSource = (from ls in db.LichSuXuatHoaDons
                        //                         join nv in db.tnNhanViens on ls.MaNV equals nv.MaNV
                        //                         where ls.MaKH == maKH & ls.ThangCongNo.Value.Month == thang1

                        //                         & ls.ThangCongNo.Value.Year == nam1
                        //                         select new
                        //                         {
                        //                             ls.NgayIn,
                        //                             ls.ThangCongNo,
                        //                             nv.HoTenNV
                        //                         }
                        //                         ).ToList();

                        break;
                }
            }
            catch { }
            finally
            {
                db.Dispose();
            }
        }

        void LoadBieuMau()
        {
            //try
            //{
            //    if (itemToaNha.EditValue == null)
            //        return;

            //    var db = new MasterDataContext();
            //    var ltReport = (from rp in db.HDTTemplates
            //                    where rp.MaTN == (byte)itemToaNha.EditValue
            //                    & rp.IsCongNo.GetValueOrDefault()
            //                    select new { rp.ID, rp.TieuDe }).ToList();

            //    DevExpress.XtraBars.BarButtonItem itemPrint;

            //    foreach (var i in ltReport)
            //    {
            //        itemPrint = new DevExpress.XtraBars.BarButtonItem(barManager1, i.TieuDe);
            //        itemPrint.Tag = i.ID;
            //        itemPrint.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemPrintAmazon_ItemClick);
            //        barManager1.Items.Add(itemPrint);
            //        itemPrintBM.ItemLinks.Add(itemPrint);
            //    }
            //}
            //catch { }
        }

        void InBieuMau(bool IsPrint)
        {

        }

        private void itemPrintAmazon_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //bool IsPrint = true;
            //var result = DialogBox.Question("Bạn có muốn xem trước không?");

            //if (result == System.Windows.Forms.DialogResult.Yes)
            //    IsPrint = false;

            //using (var db = new MasterDataContext())
            //{
            //    var indexs = gvHoaDon.GetSelectedRows();

            //    if (indexs.Count() == 0)
            //        return;

            //    foreach (var i in indexs)
            //    {
            //        var _maKH = (int?)gvHoaDon.GetRowCellValue(i,"MaKH");
            //        var _maHD = (int?)gvHoaDon.GetRowCellValue(i,"MaHD");

            //        if (_maKH == null | _maHD == null)
            //        {
            //            return;
            //        }

            //        if (e.Item.Tag == null)
            //        {
            //            return;
            //        }
            //        if (itemThang.EditValue == null)
            //        {
            //            DialogBox.Error("Vui lòng chọn tháng!");
            //            return;
            //        }
            //        if (itemNam.EditValue == null)
            //        {
            //            DialogBox.Error("Vui lòng chọn năm!");
            //            return;
            //        }

            //        var idTemp = (int)e.Item.Tag;

            //        var temp = db.HDTTemplates.Single(o => o.ID == idTemp);

            //        var ctlRTF = new DevExpress.XtraRichEdit.RichEditControl();
            //        int _Thang = int.Parse(itemThang.EditValue.ToString());
            //        int _Nam = int.Parse(itemNam.EditValue.ToString());
            //        byte _maTN = byte.Parse(itemToaNha.EditValue.ToString());

            //        ctlRTF.Document.RtfText = temp.NoiDung;

            //        MergeField merge = new MergeField();

            //        merge.MaKH = _maKH.Value;

            //        merge.MaHD = _maHD.Value;

            //        merge.Thang = _Thang;

            //        merge.Nam = _Nam;

            //        merge.MaTN = _maTN;

            //        ctlRTF.Document.RtfText = merge.KhachHang(ctlRTF.Document.RtfText, true);

            //        ctlRTF.Document.RtfText = merge.ThongBaoPhi(ctlRTF.Document.RtfText, true);

            //        ctlRTF.Document.RtfText = merge.ThongTinKhac(ctlRTF.Document.RtfText, true);

            //        ctlRTF.Document.RtfText = merge.HopDongThue(ctlRTF.Document.RtfText, true);

            //        ctlRTF.Document.RtfText = merge.Function(ctlRTF.Document.RtfText, true);

            //        if (IsPrint)
            //        {
            //            ctlRTF.Print();
            //        }
            //        else
            //        {
            //            using (var frm = new Library.frmDesign())
            //            {
            //                frm.RtfText = ctlRTF.Document.RtfText;
            //                frm.ShowDialog();
            //                break;
            //            }
            //        }
            //    }
            //}
        }

        private void frmManager_Load(object sender, EventArgs e)
        {
            try
            {
                TranslateLanguage.TranslateControl(this, barManager1);
                Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);
                lkToaNha.DataSource = Common.TowerList;

                gvHoaDon.CustomColumnDisplayText += Common.GridViewCustomColumnDisplayText;
                itemToaNha.EditValue = Common.User.MaTN;
                itemThang.EditValue = DateTime.Now.Month;
                itemNam.EditValue = DateTime.Now.Year;
                this.LoadData();
                LoadBieuMau();
                LoadMauInHDT();
            }
            catch { }
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.LoadData();
        }

        private void gvHoaDon_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            this.Detail();
        }

        private void gvHoaDon_FocusedRowLoaded(object sender, DevExpress.XtraGrid.Views.Base.RowEventArgs e)
        {
            this.Detail();
        }

        private void itemThuTien_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var indexs = gvHoaDon.GetSelectedRows();
            if (indexs.Length == 0)
            {
                DialogBox.Alert("Vui lòng chọn khách hàng");
                return;
            }

            if (IsHDThue)
            {
                using (var frm = new frmPaymentHDT())
                {
                    DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thêm thu tiền", "Thêm", "Khách hàng: " + gvHoaDon.GetFocusedRowCellValue("TenKH").ToString() + " -Dự án: " + lkToaNha.GetDisplayText(itemToaNha.EditValue));
                    frm.MaKH = (int?)gvHoaDon.GetFocusedRowCellValue("MaKH");
                    frm.MaTN = (byte?)itemToaNha.EditValue;
                    frm.ShowDialog();
                    if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                    {
                        LoadData1Dong((int)gvHoaDon.GetFocusedRowCellValue("MaKH"));
                    }
                }
            }
            else
            {
                using (var frm = new frmPayment())
                {
                    DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thêm thu tiền", "Thêm", "Khách hàng: " + gvHoaDon.GetFocusedRowCellValue("TenKH").ToString() + " -Dự án: " + lkToaNha.GetDisplayText(itemToaNha.EditValue));
                    frm.MaKH = (int?)gvHoaDon.GetFocusedRowCellValue("MaKH");
                    frm.MaTN = (byte?)itemToaNha.EditValue;
                    frm.ShowDialog();
                    if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                    {
                        LoadData1Dong((int)gvHoaDon.GetFocusedRowCellValue("MaKH"));
                    }
                }
            }
        }

        private void itemPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                var indexs = gvHoaDon.GetSelectedRows();
                if (indexs.Length == 0)
                {
                    DialogBox.Alert("Vui lòng chọn khách hàng");
                    return;
                }

                var ltMaKH = new List<int>();
                var ltMB = new List<int>();
                foreach (var i in indexs)
                {
                    ltMaKH.Add((int)gvHoaDon.GetRowCellValue(i, "MaKH"));
                }
                foreach (var k in indexs)
                {
                    ltMB.Add((int)gvHoaDon.GetRowCellValue(k, "MaMB"));
                }
                DIPBMS.SystemLog.Classes.SYS_LOG.Insert("In giấy thông báo", "In", "Tháng " + Convert.ToInt32(itemThang.EditValue).ToString() + " - Năm " + Convert.ToInt32(itemNam.EditValue).ToString() + " -Dự án: " + lkToaNha.GetDisplayText(itemToaNha.EditValue));
                using (var frm = new GiayBao.frmGiayBao())
                {
                    frm.MaTN = (byte)itemToaNha.EditValue;
                    frm.Thang = Convert.ToInt32(itemThang.EditValue);
                    frm.Nam = Convert.ToInt32(itemNam.EditValue);
                    frm.MaKHs = new List<int>();
                    frm.MaKHs = ltMaKH;
                    frm.MaMBs = new List<int>();
                    frm.MaMBs = ltMB;
                    frm.ShowDialog(this);
                }
            }
            catch (Exception)
            {
                DialogBox.Alert("Kiểm tra lại các hóa đơn có hay không ");
            }

        }

        private void itemSendMail_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var rows = gvHoaDon.GetSelectedRows();
            if (rows.Length <= 0)
            {
                DialogBox.Alert("Vui lòng chọn [Khách hàng]. Xin cám ơn!");
                return;
            }

            List<int> listMaKHs = new List<int>();
            foreach (var index in rows)
            {
                try
                {
                    listMaKHs.Add(Convert.ToInt32(gvHoaDon.GetRowCellValue(index, "MaKH")));
                }
                catch { }
            }

            if (listMaKHs.Count() == 0) return;
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Gửi mail thông báo phí", "Gửi", "Tháng " + Convert.ToInt32(itemThang.EditValue).ToString() + " - Năm " + Convert.ToInt32(itemNam.EditValue).ToString() + " -Dự án: " + lkToaNha.GetDisplayText(itemToaNha.EditValue));
            var frm = new Email.frmSend();
            frm.MaTN = (byte)itemToaNha.EditValue;
            frm.Month = Convert.ToInt32(itemThang.EditValue);
            frm.Year = Convert.ToInt32(itemNam.EditValue);
            frm.ListMaKHs = listMaKHs;


            frm.Show();
        }

        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            this.Detail();
        }

        private void itemPreview_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var rows = gvHoaDon.GetSelectedRows();
            if (rows.Length <= 0)
            {
                return;
            }

            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Xem phiếu thu tiền", "Xem", "Khách hàng: " + gvHoaDon.GetFocusedRowCellValue("TenKH").ToString() + " -Dự án: " + lkToaNha.GetDisplayText(itemToaNha.EditValue));
            var MaTN = (byte)itemToaNha.EditValue;
            var Thang = Convert.ToInt32(itemThang.EditValue);
            var Nam = Convert.ToInt32(itemNam.EditValue);
            var MaKH = Convert.ToInt32(gvHoaDon.GetFocusedRowCellValue("MaKH"));

            var rpt = new GiayBao.rptPhieuThu(MaTN, Thang, Nam, MaKH);
            rpt.ShowPreviewDialog();

        }

        private void itemPrintFund_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var rows = gvHoaDon.GetSelectedRows();
            if (rows.Length <= 0)
            {
                DialogBox.Alert("Vui lòng chọn [Khách hàng]. Xin cám ơn!");
                return;
            }

            var MaTN = (byte)itemToaNha.EditValue;
            var Thang = Convert.ToInt32(itemThang.EditValue);
            var Nam = Convert.ToInt32(itemNam.EditValue);

            foreach (var index in rows)
            {
                DIPBMS.SystemLog.Classes.SYS_LOG.Insert("In phiếu thu", "In phiếu", "Khách hàng: " + gvHoaDon.GetRowCellValue(index, "TenKH").ToString() + " -Dự án: " + lkToaNha.GetDisplayText(itemToaNha.EditValue));
                var MaKH = Convert.ToInt32(gvHoaDon.GetRowCellValue(index, "MaKH"));
                var rpt = new GiayBao.rptPhieuThu(MaTN, Thang, Nam, MaKH);
                rpt.Print();
            }
        }

        private void itemPrintSelectAll_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //try
            //{
            var db = new MasterDataContext();
            var _MaTN = (byte)itemToaNha.EditValue;
            var _Thang = Convert.ToInt32(itemThang.EditValue);
            var _Nam = Convert.ToInt32(itemNam.EditValue);
            var _TuNgay = new DateTime(_Nam, _Thang, 1);
            var _DenNgay = Common.GetLastDayOfMonth(_Thang, _Nam);
            var _NgayHienTai = DateTime.Now;
            var indexs = (from kh in db.tnKhachHangs
                          where kh.MaTN == _MaTN
                          select new
                          {
                              kh.MaKH,
                              kh.KyHieu,
                              kh.MaPhu,
                              TenKH = kh.IsCaNhan == true ? (kh.HoKH + " " + kh.TenKH) : kh.CtyTen,
                              DienThoai = kh.DienThoaiKH,
                              kh.EmailKH,
                              DiaChi = kh.DCLL,
                              MaMB = db.dvHoaDons.First(p => p.MaKH == kh.MaKH & SqlMethods.DateDiffDay(p.NgayTT, _DenNgay) >= 0 & p.IsDuyet == true & p.MaMB != null).MaMB
                          }).ToList();
            if (indexs.Count == 0)
            {
                DialogBox.Alert("Vui lòng chọn khách hàng");
                return;
            }

            var ltMaKH = new List<int>();
            var ltMB = new List<int>();
            foreach (var i in indexs)
            {
                ltMaKH.Add((int)i.MaKH);
            }
            foreach (var k in indexs)
            {
                if (k.MaMB != null)
                    ltMB.Add((int)k.MaMB);
            }

            using (var frm = new GiayBao.frmGiayBao())
            {
                frm.MaTN = (byte)itemToaNha.EditValue;
                frm.Thang = Convert.ToInt32(itemThang.EditValue);
                frm.Nam = Convert.ToInt32(itemNam.EditValue);
                frm.MaKHs = new List<int>();
                frm.MaKHs = ltMaKH;
                frm.MaMBs = new List<int>();
                frm.MaMBs = ltMB;
                frm.ShowDialog(this);
            }
        }

        private void itemPrintSelect_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                var indexs = gvHoaDon.GetSelectedRows();
                if (indexs.Length == 0)
                {
                    DialogBox.Alert("Vui lòng chọn khách hàng");
                    return;
                }

                var ltMaKH = new List<int>();
                var ltMB = new List<int>();
                foreach (var i in indexs)
                {
                    ltMaKH.Add((int)gvHoaDon.GetRowCellValue(i, "MaKH"));
                }
                foreach (var k in indexs)
                {
                    ltMB.Add((int)gvHoaDon.GetRowCellValue(k, "MaMB"));
                }

                using (var frm = new GiayBao.frmGiayBao())
                {
                    frm.MaTN = (byte)itemToaNha.EditValue;
                    frm.Thang = Convert.ToInt32(itemThang.EditValue);
                    frm.Nam = Convert.ToInt32(itemNam.EditValue);
                    frm.MaKHs = new List<int>();
                    frm.MaKHs = ltMaKH;
                    frm.MaMBs = new List<int>();
                    frm.MaMBs = ltMB;
                    frm.ShowDialog(this);
                }
            }
            catch (Exception)
            {
                DialogBox.Alert("Kiểm tra lại các hóa đơn có hay không ");
            }
        }

        private void itemSendAmazon_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //var rows = gvHoaDon.GetSelectedRows();
            //if (rows.Length <= 0)
            //{
            //    DialogBox.Alert("Vui lòng chọn [Khách hàng]. Xin cám ơn!");
            //    return;
            //}
            //int TongGui = 0;
            //List<Email.frmSendAmazonHDT.EmailHDTCls> listMaKHs = new List<Email.frmSendAmazonHDT.EmailHDTCls>();
            //foreach (var index in rows)
            //{
            //    try
            //    {
            //        string[] ltMail = gvHoaDon.GetRowCellDisplayText(index, "EmailKH").ToString().Replace(" ", "").Split(';');

            //        foreach (var email in ltMail)
            //        {
            //            if (email.Length > 0)
            //            {
            //                listMaKHs.Add(new Email.frmSendAmazonHDT.EmailHDTCls
            //                {
            //                    MaKH = Convert.ToInt32(gvHoaDon.GetRowCellValue(index, "MaKH")),
            //                    Email = email,
            //                    MaHD = Convert.ToInt32(gvHoaDon.GetRowCellValue(index, "MaHD"))
            //                });
            //                TongGui++;
            //            }
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        DialogBox.Error(ex.Message);
            //    }
            //}

            //if (TongGui == 0)
            //{
            //    DialogBox.Error("Không có email nào để gửi");
            //    return;
            //}

            //DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Gửi mail thông báo phí", "Gửi", "Tháng " + Convert.ToInt32(itemThang.EditValue).ToString() + " - Năm " + Convert.ToInt32(itemNam.EditValue).ToString() + " -Dự án: " + lkToaNha.GetDisplayText(itemToaNha.EditValue));
            //var frm = new Email.frmSendAmazonHDT();
            //frm.MaTN = (byte)itemToaNha.EditValue;
            //frm.Month = Convert.ToInt32(itemThang.EditValue);
            //frm.Year = Convert.ToInt32(itemNam.EditValue);
            //frm.ListMaKHs = listMaKHs;
            //frm.Show();
        }

        private void itemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Commoncls.ExportExcel(gcHoaDon);
        }

        class EmailCls
        {
            public int MaKH { get; set; }
            public string Email { get; set; }
        }

        private void itemXuatHoaDonDienTu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var indexs = gvHoaDon.GetSelectedRows();
            if (indexs.Length == 0)
            {
                DialogBox.Alert("Vui lòng chọn khách hàng");
                return;
            }

            var ltMaKH = new List<int>();
            var ltMB = new List<int>();
            var _MaTN = (byte)itemToaNha.EditValue;
            var _Thang = Convert.ToInt32(itemThang.EditValue);
            var _Nam = Convert.ToInt32(itemNam.EditValue);
            foreach (var i in indexs)
            {
                ltMaKH.Add((int)gvHoaDon.GetRowCellValue(i, "MaKH"));
            }

            //foreach (var _MaKH in ltMaKH)
            //{
            //    ClsDongBoHDT.HoaDonDienTu(_MaKH, _MaTN, _Thang, _Nam, true);
            //}

        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var indexs = gvHoaDon.GetSelectedRows();
            if (indexs.Length == 0)
            {
                DialogBox.Alert("Vui lòng chọn khách hàng");
                return;
            }

            var ltMaKH = new List<int>();
            var ltMB = new List<int>();
            var _MaTN = (byte)itemToaNha.EditValue;
            var _Thang = Convert.ToInt32(itemThang.EditValue);
            var _Nam = Convert.ToInt32(itemNam.EditValue);
            foreach (var i in indexs)
            {
                ltMaKH.Add((int)gvHoaDon.GetRowCellValue(i, "MaKH"));
            }

            //foreach (var _MaKH in ltMaKH)
            //{
            //    ClsDongBoHDT.HoaDonDienTuTest(_MaKH, _MaTN, _Thang, _Nam, true);
            //}
        }

        private void itemSMS_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //var rows = gvHoaDon.GetSelectedRows();
            //if (rows.Length <= 0)
            //{
            //    DialogBox.Alert("Vui lòng chọn [Khách hàng]. Xin cám ơn!");
            //    return;
            //}

            //List<SMS.frmSendSMS.SMSCls> listMaKHs = new List<SMS.frmSendSMS.SMSCls>();
            //foreach (var index in rows)
            //{
            //    try
            //    {
            //        SMS.frmSendSMS.SMSCls objSMS = new SMS.frmSendSMS.SMSCls();
            //        objSMS.SDT = gvHoaDon.GetRowCellDisplayText(index, "DienThoai").ToString();
            //        objSMS.MaKH = Convert.ToInt32(gvHoaDon.GetRowCellValue(index, "MaKH"));

            //        if (objSMS.SDT.Length > 0)
            //            listMaKHs.Add(objSMS);
            //    }
            //    catch { }
            //}

            //if (listMaKHs.Count() == 0) return;

            //var frm = new SMS.frmSendSMS();
            //frm.MaTN = (byte)itemToaNha.EditValue;
            //frm.Month = Convert.ToInt32(itemThang.EditValue);
            //frm.Year = Convert.ToInt32(itemNam.EditValue);
            //frm.ListMaKHs = listMaKHs;


            //frm.Show();
        }

        private void itemSendSMS_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //var rows = gvHoaDon.GetSelectedRows();
            //if (rows.Length <= 0)
            //{
            //    DialogBox.Alert("Vui lòng chọn [Khách hàng]. Xin cám ơn!");
            //    return;
            //}

            //List<SMS.frmSendSMS.SMSCls> listMaKHs = new List<SMS.frmSendSMS.SMSCls>();

            //foreach (var index in rows)
            //{
            //    try
            //    {
            //        SMS.frmSendSMS.SMSCls objSMS = new SMS.frmSendSMS.SMSCls();
            //        objSMS.SDT = gvHoaDon.GetRowCellDisplayText(index, "DienThoai").ToString().Split(';')[0];
            //        objSMS.MaKH = Convert.ToInt32(gvHoaDon.GetRowCellValue(index, "MaKH"));

            //        if (objSMS.SDT.Length > 0)
            //            listMaKHs.Add(objSMS);
            //    }
            //    catch { }
            //}

            //if (listMaKHs.Count() == 0) return;

            //var frm = new SMS.frmSendSMS();
            //frm.MaTN = (byte)itemToaNha.EditValue;
            //frm.Month = Convert.ToInt32(itemThang.EditValue);
            //frm.Year = Convert.ToInt32(itemNam.EditValue);
            //frm.ListMaKHs = listMaKHs;
            //frm.Show();
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            XuatHoaDonDT(false);
        }

        void XuatHoaDonDT(bool IsView)
        {
            var indexs = gvHoaDon.GetSelectedRows();
            if (indexs.Length == 0)
            {
                DialogBox.Alert("Vui lòng chọn khách hàng");
                return;
            }

            var ltMaKH = new List<XuatHDCls>();

            foreach (var i in indexs)
            {
                ltMaKH.Add(new XuatHDCls()
                {
                    maKH = (int)gvHoaDon.GetRowCellValue(i, "MaKH"),
                    maHD = (int)gvHoaDon.GetRowCellValue(i, "MaHD"),
                });
            }

            var _MaTN = (byte)itemToaNha.EditValue;
            var _Thang = Convert.ToInt32(itemThang.EditValue);
            var _Nam = Convert.ToInt32(itemNam.EditValue);

            //foreach (var item in ltMaKH)
            //{
            //    ClsDongBoHDT.HoaDonDienTuNhieuThueXuat(item.maKH, item.maHD, _MaTN, _Thang, _Nam, IsView);
            //}
        }

        class XuatHDCls
        {
            public int maKH { get; set; }
            public int maHD { get; set; }
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            XuatHoaDonDT(true);
        }

        public class CongNoListHDT
        {
            public int MaKH { get; set; }
            public int? MaHD { get; set; }
            public string KyHieu { get; set; }
            public string MaPhu { get; set; }
            public string TenKH { get; set; }
            public string DienThoai { get; set; }
            public string EmailKH { get; set; }
            public string LoaiMB { get; set; }
            public string DiaChi { get; set; }
            public int? MaMB { get; set; }
            public decimal? NoDauKy { get; set; }
            public decimal? PhatSinh { get; set; }
            public decimal? DaThu { get; set; }
            public decimal? KhauTru { get; set; }
            public decimal? ConNo { get; set; }
            public decimal? ThuTruocHDT { get; set; }
            public decimal? KhauTruHDT { get; set; }
            public string MaSoMB { get; set; }
            public string TrangThai { get; set; }
            public string TenLMB { get; set; }
            public decimal TienLai { get; set; }
        }

        private void itemXuatHoaDonDienTu_TEST_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var indexs = gvHoaDon.GetSelectedRows();
            if (indexs.Length == 0)
            {
                DialogBox.Alert("Vui lòng chọn khách hàng");
                return;
            }

            var ltMaKH = new List<int>();
            var ltMB = new List<int>();
            var _MaTN = (byte)itemToaNha.EditValue;
            var _Thang = Convert.ToInt32(itemThang.EditValue);
            var _Nam = Convert.ToInt32(itemNam.EditValue);
            foreach (var i in indexs)
            {
                ltMaKH.Add((int)gvHoaDon.GetRowCellValue(i, "MaKH"));
            }

            //foreach (var _MaKH in ltMaKH)
            //{
            //    ClsDongBoTestHDT.HoaDonDienTuNhieuThueXuat(_MaKH, _MaTN, _Thang, _Nam);
            //}
        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var indexs = gvHoaDon.GetSelectedRows();
            if (indexs.Length == 0)
            {
                DialogBox.Alert("Vui lòng chọn khách hàng");
                return;
            }

            var ltMaKH = new List<int>();
            var ltMB = new List<int>();
            var _MaTN = (byte)itemToaNha.EditValue;
            var _Thang = Convert.ToInt32(itemThang.EditValue);
            var _Nam = Convert.ToInt32(itemNam.EditValue);

            foreach (var i in indexs)
            {
                ltMaKH.Add((int)gvHoaDon.GetRowCellValue(i, "MaKH"));
            }

            //foreach (var _MaKH in ltMaKH)
            //{
            //    ClsDongBoTestHDT.HoaDonDienTuTestNhieuThueXuat(_MaKH, _MaTN, _Thang, _Nam);
            //}
        }

        private void itemToaNha_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadBieuMau();
            LoadMauInHDT();
        }

        void LoadMauInHDT()
        {
            try
            {
                var db = new MasterDataContext();
                var ltReport = (from rp in db.HDTTemplates
                                where rp.MaTN == Common.User.MaTN
                                & rp.IsCongNo.GetValueOrDefault()
                                select new { rp.ID, rp.TieuDe }).ToList();
                barSub_MauIn.ItemLinks.Clear();
                DevExpress.XtraBars.BarButtonItem itemPrint;
                foreach (var i in ltReport)
                {
                    itemPrint = new DevExpress.XtraBars.BarButtonItem(barManager1, i.TieuDe);
                    itemPrint.Tag = i.ID;
                    itemPrint.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemPrint_ItemClick2);
                    barManager1.Items.Add(itemPrint);
                    barSub_MauIn.ItemLinks.Add(itemPrint);
                }
            }
            catch { }
        }

        void itemPrint_ItemClick2(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            MasterDataContext db = new MasterDataContext();
            var id = 0;
            var maTN = (byte)itemToaNha.EditValue;
            var Thang = Convert.ToInt32(itemThang.EditValue);
            var Nam = Convert.ToInt32(itemNam.EditValue);
            var MaKH = (int)gvHoaDon.GetFocusedRowCellValue("MaKH");
            if (MaKH == null)
            {
                DialogBox.Error("Không tồn tại khách hàng");
                return;
            }
            var objLTT = (from hd in db.dvHoaDons
                          join ltt in db.ctLichThanhToans on hd.LinkID equals ltt.ID
                          where hd.MaTN == maTN
                          & hd.NgayTT.Value.Month == Thang
                          & hd.NgayTT.Value.Year == Nam
                          & hd.MaKH == MaKH
                              & hd.IsDuyet.GetValueOrDefault()
                                 & hd.ConNo.GetValueOrDefault() > 0
                          select new
                          {
                              hd.ID,
                              ltt.MaHD
                          }).ToList();

            if (objLTT.Count() == 0)
            {
                DialogBox.Error("Không tồn tại lịch thanh toán hóa đơn tháng: " + Thang);
                return;
            }
            else
            {
                foreach (var item in objLTT)
                {
                    id = (int)item.MaHD;
                    using (var ctlRTF = new DevExpress.XtraRichEdit.RichEditControl())
                    {
                        var bm = db.HDTTemplates.Single(o => o.ID == (int)e.Item.Tag);
                        ctlRTF.Document.RtfText = bm.NoiDung;

                        LandSoftBuilding.Lease.Mau.MergeField merge = new LandSoftBuilding.Lease.Mau.MergeField();
                        merge.MaTN = maTN;
                        merge.Thang = Thang;
                        merge.Nam = Nam;
   
                        merge.MaKH = MaKH;
                        ctlRTF.RtfText = merge.HopDongThue(ctlRTF.RtfText, id, true);

                        using (var frm = new LandSoftBuilding.Lease.Mau.frmDesign())
                        {
                            frm.RtfText = ctlRTF.RtfText;
                            frm.ShowDialog();
                        }
                    }
                }
            }
        }
    }
}