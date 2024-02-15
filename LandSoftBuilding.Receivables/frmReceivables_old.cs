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
using System.Transactions;
using System.Diagnostics;
using DevExpress.XtraReports.UI;

namespace LandSoftBuilding.Receivables
{
    public partial class frmReceivables_old : DevExpress.XtraEditors.XtraForm
    {
        public frmReceivables_old()
        {
            InitializeComponent();
        }

        void LoadData()
        {
            // DevExpress.Data.Helpers.ServerModeCore.DefaultForceCaseInsensitiveForAnySource = true;
            gcHoaDon.DataSource = null;
            var db = new MasterDataContext();
            //db.GetSystemDate();
            db.CommandTimeout = 100000;
            var _MaTN = (byte)itemToaNha.EditValue;
            var _Thang = Convert.ToInt32(itemThang.EditValue);
            var _Nam = Convert.ToInt32(itemNam.EditValue);
            var _TuNgay = new DateTime(_Nam, _Thang, 1);
            var _DenNgay = Common.GetLastDayOfMonth(_Thang, _Nam);
            var _NgayHienTai = DateTime.Now;
            //var watch = Stopwatch.StartNew();
            //using (var txn = new TransactionScope(
            //        TransactionScopeOption.Required,
            //        new TransactionOptions
            //        {
            //            IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted
            //        }
            //    ))
            //{
            #region Code mới
            if (_MaTN == 8)
            {
                var objKH = (from kh in db.tnKhachHangs
                             where kh.MaTN == _MaTN //&& kh.MaKH == 8871
                             select new
                             {
                                 kh.MaKH,
                                 kh.KyHieu,
                                 kh.MaPhu,
                                 TenKH = kh.IsCaNhan == true ? (kh.HoKH + " " + kh.TenKH) : kh.CtyTen,
                                 DienThoai = kh.DienThoaiKH,
                                 kh.EmailKH,
                                 DiaChi = kh.DCLL,
                                 MaMB = (from hd in db.dvHoaDons
                                         orderby hd.ID descending
                                         where hd.MaKH==kh.MaKH
                                         & SqlMethods.DateDiffDay(hd.NgayTT, _DenNgay) >= 0
                                         & hd.IsDuyet == true
                                         & hd.MaMB != null
                                          select new { hd.MaMB}).FirstOrDefault().MaMB,
                             }).ToList();
                var objHD_NDK = (from hd in db.dvHoaDons
                                 where SqlMethods.DateDiffDay(hd.NgayTT, _TuNgay) > 0 & hd.IsDuyet == true && hd.MaTN == _MaTN
                                 // & hd.MaKH == 5723
                                 group hd by hd.MaKH into ndk
                                 select new
                                 {
                                     MaKH = ndk.Key,
                                     PhaiThu = ndk.Sum(s => s.PhaiThu)
                                 }).ToList();
                var objSQ_NDK = (from sq in db.SoQuy_ThuChis
                                 where SqlMethods.DateDiffDay(sq.NgayPhieu, _TuNgay) > 0 && sq.TableName == "dvHoaDon" && sq.LinkID != null && sq.MaTN == _MaTN //& sq.MaKH == 5723
                                 group sq by sq.MaKH into ndk
                                 select new
                                 {
                                     MaKH = ndk.Key,
                                     NoDauKy = ndk.Sum(s => s.DaThu + s.KhauTru - s.ThuThua)
                                 }).ToList();
                var objPhatSinh = (from hd in db.dvHoaDons
                                   where SqlMethods.DateDiffMonth(hd.NgayTT, _TuNgay) == 0 & hd.IsDuyet == true && hd.MaTN == _MaTN
                                   //&& hd.MaKH == 5723
                                   group hd by hd.MaKH into ps
                                   select new
                                   {
                                       MaKH = ps.Key,
                                       PhaiThu = ps.Sum(s => s.PhaiThu),
                                   }).ToList();
                var objDaThu1 = (from ct in db.SoQuy_ThuChis
                                 join hd in db.dvHoaDons on ct.LinkID equals hd.ID
                                 where SqlMethods.DateDiffMonth(hd.NgayTT, _TuNgay) == 0
                                 && ct.IsPhieuThu == true && ct.MaLoaiPhieu != 24 && ct.MaTN == _MaTN //&& ct.MaKH == 5723
                                 group ct by ct.MaKH into dt
                                 select new
                                 {
                                     MaKH = dt.Key,
                                     DaThu = dt.Sum(s => s.DaThu)
                                 }).ToList();
                var objDaThu2 = (from ct in db.SoQuy_ThuChis
                                 join hd in db.dvHoaDons on ct.LinkID equals hd.ID
                                 where SqlMethods.DateDiffMonth(ct.NgayPhieu, _TuNgay) == 0
                                 && SqlMethods.DateDiffDay(_TuNgay, hd.NgayTT) < 0
                                 && ct.IsPhieuThu == true && ct.MaLoaiPhieu != 24 && ct.MaTN == _MaTN //&& ct.MaKH == 5723
                                 group ct by ct.MaKH into dt
                                 select new
                                 {
                                     MaKH = dt.Key,
                                     DaThu = dt.Sum(s => s.DaThu)
                                 }).ToList();
                var objDaThu3 = (from ct in db.SoQuy_ThuChis
                                 where SqlMethods.DateDiffMonth(ct.NgayPhieu, _TuNgay) == 0
                                 && ct.IsPhieuThu == true && ct.MaLoaiPhieu != 24 && ct.MaTN == _MaTN && ct.LinkID == null //&& ct.MaKH == 5723
                                 group ct by ct.MaKH into dt
                                 select new
                                 {
                                     MaKH = dt.Key,
                                     DaThu = dt.Sum(s => s.DaThu)
                                 }).ToList();
                var objDaThuTam = objDaThu1.Concat(objDaThu2).Concat(objDaThu3);
                var objDaThu = (from dt in objDaThuTam
                                group dt by dt.MaKH into _DT
                                select new
                                {
                                    MaKH = _DT.Key,
                                    DaThu = _DT.Sum(s => s.DaThu)
                                }).ToList();
                var objKhauTru = (from ct in db.SoQuy_ThuChis
                                  where SqlMethods.DateDiffMonth(ct.NgayPhieu, _TuNgay) == 0 && ct.MaTN == _MaTN //&& ct.MaKH == 5723

                                  group ct by ct.MaKH into kt
                                  select new
                                  {
                                      MaKH = kt.Key,
                                      KhauTru = kt.Sum(s => s.KhauTru)
                                  }).ToList();
                var objThuTruoc = (from sq in db.SoQuy_ThuChis
                                   where SqlMethods.DateDiffDay(sq.NgayPhieu, _DenNgay) >= 0 && sq.MaTN == _MaTN //&& sq.MaKH == 5723

                                   group sq by sq.MaKH into tt
                                   select new
                                   {
                                       MaKH = tt.Key,
                                       ThuTruoc = tt.Sum(s => s.ThuThua - s.KhauTru)
                                   }).ToList();
                var objList = (from kh in objKH
                               join ndk in objHD_NDK on kh.MaKH equals ndk.MaKH into nodk
                               from ndk in nodk.DefaultIfEmpty()
                               join sqdk in objSQ_NDK on kh.MaKH equals sqdk.MaKH into soquydk
                               from sqdk in soquydk.DefaultIfEmpty()
                               join ps in objPhatSinh on kh.MaKH equals ps.MaKH into psinh
                               from ps in psinh.DefaultIfEmpty()
                               join dt in objDaThu on kh.MaKH equals dt.MaKH into dthu
                               from dt in dthu.DefaultIfEmpty()
                               join kt in objKhauTru on kh.MaKH equals kt.MaKH into ktru
                               from kt in ktru.DefaultIfEmpty()
                               join tt in objThuTruoc on kh.MaKH equals tt.MaKH into ttruoc
                               from tt in ttruoc.DefaultIfEmpty()
                               select new
                               {
                                   kh.MaKH,
                                   kh.KyHieu,
                                   kh.MaPhu,
                                   TenKH = kh.TenKH,
                                   DienThoai = kh.DienThoai,
                                   kh.EmailKH,
                                   DiaChi = kh.DiaChi,
                                   kh.MaMB,
                                   NoDauKy = (ndk == null ? 0 : ndk.PhaiThu.GetValueOrDefault()) - (sqdk == null ? 0 : sqdk.NoDauKy.GetValueOrDefault()),
                                   PhatSinh = ps == null ? 0 : ps.PhaiThu.GetValueOrDefault(),
                                   DaThu = dt == null ? 0 : dt.DaThu.GetValueOrDefault(),
                                   KhauTru = kt == null ? 0 : kt.KhauTru.GetValueOrDefault(),
                                   ThuTruoc = tt == null ? 0 : tt.ThuTruoc.GetValueOrDefault()
                               }).Select(p => new
                                           {
                                               ThuTruoc = p.ThuTruoc,
                                               NoDauKy = p.NoDauKy < 0 ? 0 : p.NoDauKy,
                                               PhatSinh = p.PhatSinh,
                                               KhauTru = p.KhauTru,
                                               DaThu = p.DaThu,
                                               ConNo = ((p.NoDauKy < 0 ? 0 : p.NoDauKy) +
                                                       p.PhatSinh -
                                                       p.KhauTru -
                                                       p.DaThu) < 0 ? 0 : ((p.NoDauKy < 0 ? 0 : p.NoDauKy) +
                                                       p.PhatSinh -
                                                       p.KhauTru -
                                                       p.DaThu),
                                               MaKH = p.MaKH,
                                               KyHieu = p.KyHieu,
                                               MaPhu = p.MaPhu,
                                               TenKH = p.TenKH,
                                               DienThoai = p.DienThoai,
                                               EmailKH = p.EmailKH,
                                               DiaChi = p.DiaChi,
                                               MaMB = p.MaMB,
                                               NoCuoi = (((p.NoDauKy < 0 ? 0 : p.NoDauKy) +
                                                       p.PhatSinh -
                                                       p.KhauTru -
                                                       p.DaThu) < 0 ? 0 : ((p.NoDauKy < 0 ? 0 : p.NoDauKy) +
                                                       p.PhatSinh -
                                                       p.KhauTru -
                                                       p.DaThu)) - p.ThuTruoc
                                           });

                gcHoaDon.DataSource = objList;
            }
            else
            {
                var objKH = (from kh in db.tnKhachHangs
                             where kh.MaTN == _MaTN //&& kh.MaKH == 11695
                             select new
                             {
                                 kh.MaKH,
                                 kh.KyHieu,
                                 kh.MaPhu,
                                 TenKH = kh.IsCaNhan == true ? (kh.HoKH + " " + kh.TenKH) : kh.CtyTen,
                                 DienThoai = kh.DienThoaiKH,
                                 kh.EmailKH,
                                 DiaChi = kh.DCLL,
                                 MaMB = (from hd in db.dvHoaDons
                                         orderby hd.ID descending
                                         where hd.MaKH == kh.MaKH
                                         & SqlMethods.DateDiffDay(hd.NgayTT, _DenNgay) >= 0
                                         & hd.IsDuyet == true
                                         & hd.MaMB != null
                                         select new { hd.MaMB }).FirstOrDefault().MaMB,
                             }).ToList();
                var objHD_NDK = (from hd in db.dvHoaDons
                                 where SqlMethods.DateDiffDay(hd.NgayTT, _TuNgay) > 0 & hd.IsDuyet == true && hd.MaTN == _MaTN
                                 //& hd.MaKH == 11695
                                 group hd by hd.MaKH into ndk
                                 select new
                                 {
                                     MaKH = ndk.Key,
                                     PhaiThu = ndk.Sum(s => s.PhaiThu)
                                 }).ToList();
                var objSQ_NDK = (from sq in db.SoQuy_ThuChis
                                 join hd in db.dvHoaDons on sq.LinkID equals hd.ID
                                 where SqlMethods.DateDiffDay(sq.NgayPhieu, _TuNgay) > 0 && sq.TableName == "dvHoaDon" && sq.LinkID != null && sq.MaTN == _MaTN & hd.IsDuyet == true //& sq.MaKH == 2229
                                 group sq by sq.MaKH into ndk
                                 select new
                                 {
                                     MaKH = ndk.Key,
                                     NoDauKy = ndk.Sum(s => s.DaThu + s.KhauTru - s.ThuThua)
                                 }).ToList();
                var objPhatSinh = (from hd in db.dvHoaDons
                                   where SqlMethods.DateDiffMonth(hd.NgayTT, _TuNgay) == 0 & hd.IsDuyet == true && hd.MaTN == _MaTN
                                   // && hd.MaKH == 11695
                                   group hd by hd.MaKH into ps
                                   select new
                                   {
                                       MaKH = ps.Key,
                                       PhaiThu = ps.Sum(s => s.PhaiThu),
                                   }).ToList();
                var objDaThu = (from ct in db.SoQuy_ThuChis
                                where SqlMethods.DateDiffMonth(ct.NgayPhieu, _TuNgay) == 0
                                && ct.IsPhieuThu == true && ct.MaLoaiPhieu != 24 && ct.MaTN == _MaTN && ct.IsPhieuThu==true && ct.LinkID!=null //&& ct.MaKH == 11695
                                group ct by ct.MaKH into dt
                                select new
                                {
                                    MaKH = dt.Key,
                                    DaThu = dt.Sum(s => s.DaThu)
                                }).ToList();
                var objKhauTru = (from ct in db.SoQuy_ThuChis
                                  where SqlMethods.DateDiffMonth(ct.NgayPhieu, _TuNgay) == 0 && ct.MaTN == _MaTN //&& ct.MaKH == 11695

                                  group ct by ct.MaKH into kt
                                  select new
                                  {
                                      MaKH = kt.Key,
                                      KhauTru = kt.Sum(s => s.KhauTru)
                                  }).ToList();
                var objThuTruoc = (from sq in db.SoQuy_ThuChis
                                   where SqlMethods.DateDiffDay(sq.NgayPhieu, _DenNgay) >= 0 && sq.MaTN == _MaTN //&& sq.MaKH == 11695

                                   group sq by sq.MaKH into tt
                                   select new
                                   {
                                       MaKH = tt.Key,
                                       ThuTruoc = tt.Sum(s => s.ThuThua - s.KhauTru)
                                   }).ToList();
                var objList = (from kh in objKH
                               join ndk in objHD_NDK on kh.MaKH equals ndk.MaKH into nodk
                               from ndk in nodk.DefaultIfEmpty()
                               join sqdk in objSQ_NDK on kh.MaKH equals sqdk.MaKH into soquydk
                               from sqdk in soquydk.DefaultIfEmpty()
                               join ps in objPhatSinh on kh.MaKH equals ps.MaKH into psinh
                               from ps in psinh.DefaultIfEmpty()
                               join dt in objDaThu on kh.MaKH equals dt.MaKH into dthu
                               from dt in dthu.DefaultIfEmpty()
                               join kt in objKhauTru on kh.MaKH equals kt.MaKH into ktru
                               from kt in ktru.DefaultIfEmpty()
                               join tt in objThuTruoc on kh.MaKH equals tt.MaKH into ttruoc
                               from tt in ttruoc.DefaultIfEmpty()
                               select new
                               {
                                   kh.MaKH,
                                   kh.KyHieu,
                                   kh.MaPhu,
                                   TenKH = kh.TenKH,
                                   DienThoai = kh.DienThoai,
                                   kh.EmailKH,
                                   DiaChi = kh.DiaChi,
                                   kh.MaMB,
                                   NoDauKy = (ndk == null ? 0 : ndk.PhaiThu.GetValueOrDefault()) - (sqdk == null ? 0 : sqdk.NoDauKy.GetValueOrDefault()),
                                   PhatSinh = ps == null ? 0 : ps.PhaiThu.GetValueOrDefault(),
                                   DaThu = dt == null ? 0 : dt.DaThu.GetValueOrDefault(),
                                   KhauTru = kt == null ? 0 : kt.KhauTru.GetValueOrDefault(),
                                   ThuTruoc = tt == null ? 0 : tt.ThuTruoc.GetValueOrDefault()
                               }).Select(p => new
                               {
                                   ThuTruoc = p.ThuTruoc,
                                   NoDauKy = p.NoDauKy < 0 ? 0 : p.NoDauKy,
                                   PhatSinh = p.PhatSinh,
                                   KhauTru = p.KhauTru,
                                   DaThu = p.DaThu,
                                   ConNo = ((p.NoDauKy < 0 ? 0 : p.NoDauKy) +
                                           p.PhatSinh -
                                           p.KhauTru -
                                           p.DaThu) < 0 ? 0 : ((p.NoDauKy < 0 ? 0 : p.NoDauKy) +
                                           p.PhatSinh -
                                           p.KhauTru -
                                           p.DaThu),
                                   MaKH = p.MaKH,
                                   KyHieu = p.KyHieu,
                                   MaPhu = p.MaPhu,
                                   TenKH = p.TenKH,
                                   DienThoai = p.DienThoai,
                                   EmailKH = p.EmailKH,
                                   DiaChi = p.DiaChi,
                                   MaMB = p.MaMB,
                                   NoCuoi = (((p.NoDauKy < 0 ? 0 : p.NoDauKy) +
                                           p.PhatSinh -
                                           p.KhauTru -
                                           p.DaThu) < 0 ? 0 : ((p.NoDauKy < 0 ? 0 : p.NoDauKy) +
                                           p.PhatSinh -
                                           p.KhauTru -
                                           p.DaThu)) - p.ThuTruoc
                               });

                gcHoaDon.DataSource = objList;
            }
            #endregion

            //watch.Stop();
            //long milliseconds = watch.ElapsedMilliseconds;
            //DialogBox.Alert(milliseconds.ToString());
            //}

        }
        void LoadData1Dong(int MAKH)
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
            var objThuChi = db.SoQuy_ThuChis.Where(p => p.MaTN == _MaTN && p.MaKH == MAKH);
            var tam = (from kh in db.tnKhachHangs
                       where kh.MaTN == _MaTN && kh.MaKH == MAKH
                       select new
                       {
                           kh.MaKH,
                           kh.KyHieu,
                           kh.MaPhu,
                           TenKH = kh.IsCaNhan == true ? (kh.HoKH + " " + kh.TenKH) : kh.CtyTen,
                           DienThoai = kh.DienThoaiKH,
                           kh.EmailKH,
                           DiaChi = kh.DCLL,
                           MaMB = db.dvHoaDons.First(p => p.MaKH == kh.MaKH & SqlMethods.DateDiffDay(p.NgayTT, _DenNgay) >= 0 & p.IsDuyet == true & p.MaMB != null).MaMB,
                           TenLMB = db.mbMatBangs.SingleOrDefault(p => p.MaMB == db.dvHoaDons.First(k => k.MaKH == kh.MaKH & SqlMethods.DateDiffDay(k.NgayTT, _DenNgay) >= 0 & k.IsDuyet == true & k.MaMB != null).MaMB).mbLoaiMatBang.TenLMB,
                           NoDauKy = (from hd in db.dvHoaDons
                                      where hd.MaKH == kh.MaKH & SqlMethods.DateDiffDay(hd.NgayTT, _TuNgay) > 0 & hd.IsDuyet == true
                                      select hd.PhaiThu - objThuChi.Where(sq => sq.LinkID == hd.ID && sq.TableName == "dvHoaDon" && SqlMethods.DateDiffDay(sq.NgayPhieu, _TuNgay) > 0).Sum(s => s.DaThu + s.KhauTru).GetValueOrDefault()).Sum().GetValueOrDefault(),
                           PhatSinh = (from hd in db.dvHoaDons where hd.MaKH == kh.MaKH & SqlMethods.DateDiffMonth(hd.NgayTT, _TuNgay) == 0 & hd.IsDuyet == true select hd.PhaiThu).Sum().GetValueOrDefault(),


                           DaThu = (from ct in objThuChi
                                    join hd in db.dvHoaDons on new { ct.TableName, ct.LinkID } equals new { TableName = "dvHoaDon", LinkID = (long?)hd.ID }
                                    where hd.MaKH == kh.MaKH & SqlMethods.DateDiffMonth(ct.NgayPhieu, _TuNgay) == 0
                                    select ct.DaThu).Sum().GetValueOrDefault(),
                           KhauTru = (from ct in objThuChi
                                      join hd in db.dvHoaDons on new { ct.TableName, ct.LinkID } equals new { TableName = "dvHoaDon", LinkID = (long?)hd.ID }
                                      where hd.MaKH == kh.MaKH & SqlMethods.DateDiffMonth(ct.NgayPhieu, _TuNgay) == 0
                                      select ct.KhauTru).Sum().GetValueOrDefault(),
                           ConNo = (from hd in db.dvHoaDons
                                    where hd.MaKH == kh.MaKH & SqlMethods.DateDiffDay(hd.NgayTT, _DenNgay) >= 0 & hd.IsDuyet == true
                                    select hd.PhaiThu - objThuChi.Where(sq => sq.LinkID == hd.ID && sq.TableName == "dvHoaDon" && SqlMethods.DateDiffDay(sq.NgayPhieu, _DenNgay) >= 0).Sum(s => s.DaThu + s.KhauTru).GetValueOrDefault()).Sum().GetValueOrDefault(),

                           ThuTruoc = objThuChi.Where(sq => sq.MaKH == kh.MaKH && SqlMethods.DateDiffDay(sq.NgayPhieu, _DenNgay) >= 0).Sum(s => s.ThuThua - s.KhauTru).GetValueOrDefault(),
                       }).Select(p => new CongNoList()
                       {
                           ThuTruoc = p.ThuTruoc,
                           NoDauKy = p.NoDauKy,
                           PhatSinh = p.PhatSinh,
                           KhauTru = p.KhauTru,
                           DaThu = p.DaThu,
                           ConNo = p.NoDauKy +
                                   p.PhatSinh -
                                   p.KhauTru -
                                   p.DaThu,
                           MaKH = p.MaKH,
                           KyHieu = p.KyHieu,
                           MaPhu = p.MaPhu,
                           TenKH = p.TenKH,
                           DienThoai = p.DienThoai,
                           EmailKH = p.EmailKH,
                           DiaChi = p.DiaChi,
                           MaMB = p.MaMB,
                           TenLMB = p.TenLMB,
                           NoCuoi = p.ConNo - p.ThuTruoc
                       }).FirstOrDefault();

            gvHoaDon.SetFocusedRowCellValue("MaKH", tam.MaKH);
            gvHoaDon.SetFocusedRowCellValue("KyHieu", tam.KyHieu);
            gvHoaDon.SetFocusedRowCellValue("MaPhu", tam.MaPhu);
            gvHoaDon.SetFocusedRowCellValue("TenKH", tam.TenKH);
            gvHoaDon.SetFocusedRowCellValue("DienThoai", tam.DienThoai);
            gvHoaDon.SetFocusedRowCellValue("EmailKH", tam.EmailKH);
            gvHoaDon.SetFocusedRowCellValue("DiaChi", tam.DiaChi);
            //gvHoaDon.SetFocusedRowCellValue("LoaiMB", tam.LoaiMB);
            gvHoaDon.SetFocusedRowCellValue("MaMB", tam.MaMB);
            gvHoaDon.SetFocusedRowCellValue("TenLMB", tam.TenLMB);

            gvHoaDon.SetFocusedRowCellValue("NoDauKy", tam.NoDauKy);
            gvHoaDon.SetFocusedRowCellValue("PhatSinh", tam.PhatSinh);
            gvHoaDon.SetFocusedRowCellValue("DaThu", tam.DaThu);
            gvHoaDon.SetFocusedRowCellValue("ConNo", tam.ConNo);
            gvHoaDon.SetFocusedRowCellValue("KhauTru", tam.KhauTru);
            gvHoaDon.SetFocusedRowCellValue("ThuTruoc", tam.ThuTruoc);
            gvHoaDon.SetFocusedRowCellValue("NoCuoi", tam.NoCuoi);
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
                                                where hd.MaKH == maKH & SqlMethods.DateDiffDay(hd.NgayTT, ngay) >= 0 & hd.IsDuyet == true
                                                & ((hd.PhaiThu - (from ct in db.SoQuy_ThuChis
                                                                  where hd.ID == ct.LinkID
                                                                  select ct.DaThu + ct.KhauTru).Sum().GetValueOrDefault()) > 0 || (hd.MaLDV == 49 && hd.ConNo < 0))
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
                                                    DaThu = (from ct in db.SoQuy_ThuChis
                                                             where hd.ID == ct.LinkID
                                                             select ct.DaThu + ct.KhauTru).Sum().GetValueOrDefault(),
                                                    ConNo = hd.PhaiThu - (from ct in db.SoQuy_ThuChis
                                                                          where hd.ID == ct.LinkID
                                                                          select ct.DaThu + ct.KhauTru).Sum().GetValueOrDefault(),
                                                    mb.MaSoMB,
                                                    mb.MaMB,
                                                    tl.TenTL,
                                                    kn.TenKN,
                                                    lmb.TenLMB,
                                                }).ToList();
                        break;
                    case 1:
                        ctlMailHistory1.MaKH = maKH;
                        ctlMailHistory1.MailHistory_Load();
                        break;
                }
            }
            catch { }
            finally
            {
                db.Dispose();
            }
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
            var listKhMbs = new List<InfoCusSendMail>();
            foreach (var index in rows)
            {
                try
                {
                    listMaKHs.Add(Convert.ToInt32(gvHoaDon.GetRowCellValue(index, "MaKH")));
                    listKhMbs.Add(new InfoCusSendMail()
                    {
                        MaKH = (int) gvHoaDon.GetRowCellValue(index, "MaKH"),
                        MaMB = (int) gvHoaDon.GetRowCellValue(index, "MaMB")
                    });
                }
                catch { }
            }
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Gửi mail thông báo phí", "Gửi", "Tháng " + Convert.ToInt32(itemThang.EditValue).ToString() + " - Năm " + Convert.ToInt32(itemNam.EditValue).ToString() + " -Dự án: " + lkToaNha.GetDisplayText(itemToaNha.EditValue));
            var frm = new Email.frmSend();
            frm.MaTN = (byte)itemToaNha.EditValue;
            frm.Month = Convert.ToInt32(itemThang.EditValue);
            frm.Year = Convert.ToInt32(itemNam.EditValue);
            frm.ListMaKHs = listMaKHs;
            frm.ListKhMbs = listKhMbs;

            frm.Show();
        }

        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            this.Detail();
        }

        private void itemPreview_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
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
            //}
            //catch (Exception)
            //{
            //    DialogBox.Alert("Kiểm tra lại các hóa đơn có hay không ");
            //}
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

        public class CongNoList
        {
            public bool IsGui { get; set; }
            public int MaKH { get; set; }
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
            public decimal? ThuTruoc { get; set; }
            public decimal? NoCuoi { get; set; }
            public string TenLMB { get; set; }
            public string MaSoMB { get; set; }

        }

        string GetDienGiai(List<PaymentItem> ltDataSubFunc)
        {
            string strDienGiai = "";
            try
            {
                var ltLDV = (from l in ltDataSubFunc
                             where l.IsChon == true
                             orderby l.SoTTDV
                             select new { l.MaLDV, l.TenLDV, l.PhaiThu }).Distinct().ToList();
                foreach (var i in ltLDV)
                {
                    var ltDV = (from l in ltDataSubFunc
                                where l.IsChon == true & l.MaLDV == i.MaLDV
                                group l by new { l.NgayTT.Value.Month, l.NgayTT.Value.Year } into gr
                                orderby gr.Max(p => p.NgayTT)
                                select gr.Max(p => p.NgayTT)).ToList();
                    var j = 0;
                    var _Start = j;
                    var strTime = "";
                    while (j < ltDV.Count)
                    {
                        if ((j + 1) == ltDV.Count || System.Data.Linq.SqlClient.SqlMethods.DateDiffMonth(ltDV[j], ltDV[j + 1]) != 1)
                        {
                            //TienXe += i.PhaiThu.GetValueOrDefault();
                            if (_Start != j)
                            {
                                if (ltDV[_Start].Value.Year != ltDV[j].Value.Year)
                                    strTime += string.Format("T{0:MM/yyyy}-T{1:MM/yyyy},", ltDV[_Start], ltDV[j]);
                                else
                                    strTime += string.Format("T{0:MM}-T{1:MM/yyyy},", ltDV[_Start], ltDV[j]);
                            }
                            else
                            {
                                strTime += string.Format("T{0:MM/yyyy},", ltDV[_Start]);
                            }

                            _Start = j + 1;
                        }

                        j++;
                    }

                    strTime = strTime.TrimEnd(',');
                    strDienGiai += string.Format("{0} ({1}), ", i.TenLDV, strTime);
                }
            }
            catch { }

            return strDienGiai.Trim().TrimEnd(',');
        }

        int KhauTruTuDong(int i, DateTime dtNgayThu)
        {
            var db = new MasterDataContext();
            var _MaTN = (byte)itemToaNha.EditValue;
            int? MaMB = (int?)gvHoaDon.GetRowCellValue(i, "MaMB");
            int eachMaKH = (int)gvHoaDon.GetRowCellValue(i, "MaKH");
            var objSoTien = db.SoQuy_ThuChis.Where(p => p.IsPhieuThu == true && p.MaKH == eachMaKH).Sum(s => s.ThuThua.GetValueOrDefault() - s.KhauTru.GetValueOrDefault());
            decimal eachThuTruocKH = objSoTien;

            List<PaymentItem> ltData;

            #region lấy data hóa đơn
            ltData = (from hd in db.dvHoaDons
                      join l in db.dvLoaiDichVus on hd.MaLDV equals l.ID
                      join mb in db.mbMatBangs on hd.MaMB equals mb.MaMB
                      join dvtg in db.dvgxDonViThoiGians on hd.MaDVTG equals dvtg.ID into tblDonViThoiGian
                      from dvtg in tblDonViThoiGian.DefaultIfEmpty()

                      where hd.MaKH == (int?)eachMaKH & hd.ConNo.GetValueOrDefault() != 0 & hd.IsDuyet == true
                      select new PaymentItem()
                      {
                          IsChon = false,
                          ID = hd.ID,
                          MaLDV = hd.MaLDV,
                          SoTTDV = l.STT,
                          MaSoMB = mb.MaSoMB,
                          TenLDV = l.TenHienThi,
                          DienGiai = hd.DienGiai,
                          PhiDV = hd.PhiDV,
                          NgayTT = hd.NgayTT,
                          ThangTT = string.Format("{0:yyyy-MM}", hd.NgayTT),
                          KyTT = hd.KyTT,
                          DonVi = dvtg.TenDVTG,
                          TienTT = hd.TienTT,
                          TyLeCK = hd.TyLeCK,
                          TienCK = hd.TienCK,
                          PhaiThu = hd.PhaiThu,
                          DaThu = ((from ct in db.SoQuy_ThuChis
                                    where
                                        ct.TableName == "dvHoaDon" & ct.LinkID == hd.ID
                                    select ct.DaThu + ct.KhauTru).Sum().GetValueOrDefault()),

                          ConNo
                           = hd.PhaiThu - db.SoQuy_ThuChis.Where(sq => sq.TableName == "dvHoaDon" && sq.LinkID == hd.ID).Sum(s => s.DaThu + s.KhauTru).GetValueOrDefault(),
                          ThucThu = 0,

                      }).Select(p => new PaymentItem
                      {
                          IsChon = p.IsChon,
                          ID = p.ID,
                          MaLDV = p.MaLDV,
                          SoTTDV = p.SoTTDV,
                          MaSoMB = p.MaSoMB,
                          TenLDV = p.TenLDV,
                          DienGiai = p.DienGiai,
                          PhiDV = p.PhiDV,
                          NgayTT = p.NgayTT,
                          ThangTT = p.ThangTT,
                          KyTT = p.KyTT,
                          DonVi = p.DonVi,
                          TienTT = p.TienTT,
                          TyLeCK = p.TyLeCK,
                          TienCK = p.TienCK,
                          PhaiThu = p.PhaiThu,
                          DaThu = p.DaThu,
                          ConNo = p.ConNo,
                          KhauTru = 0,
                          ThuThua = 0,
                          KhachTra = p.ConNo - 0
                      }).ToList();
            string DiaChiNguoiNop = (from mb in db.mbMatBangs
                                     join tn in db.tnToaNhas on mb.MaTN equals tn.MaTN
                                     where mb.MaKH == eachMaKH
                                     select mb.MaSoMB + " - " + tn.TenTN).FirstOrDefault();
            string TenNguoiNop = (from kh in db.tnKhachHangs
                                  where kh.MaKH == eachMaKH
                                  select kh.TenKH).FirstOrDefault();

            #endregion

            #region thuat toan chon dv can khau tru

            //eachTongTienHD = ltData.Sum(p => p.ConNo).GetValueOrDefault(); , nếu tự động thu tất cả dv thì lấy chỗ này

            var objNhomDVKhauTruTuDongUuTien = db.dvDichVuKhauTrus.Where(p => p.MaTN == _MaTN && p.IsUuTien == true).Select(p => p.MaLDV).ToList();
            //var objNhomDVKhauTruTuDong = db.dvDichVuKhauTrus.Where(p => p.MaTN == _MaTN).Select(p => p.MaLDV).ToList();
            // decimal eachTongTienTuDongThu = ltData.Where(p => objNhomDVKhauTruTuDong.Contains(p.MaLDV)).Sum(p => p.ConNo).GetValueOrDefault();
            // decimal eachTongTienTuDongThuUuTien = ltData.Where(p => objNhomDVKhauTruTuDongUuTien.Contains(p.MaLDV)).Sum(p => p.ConNo).GetValueOrDefault();

            //if (eachThuTruocKH >= eachTongTienTuDongThu)
            //{
            //    foreach (var hd in ltData)
            //    {
            //        if (objNhomDVKhauTruTuDong.Contains(hd.MaLDV))
            //            hd.IsChon = hd.ConNo > 0;
            //    }
            //}
            //else if (eachThuTruocKH >= eachTongTienTuDongThuUuTien)
            //{
            foreach (var hd in ltData)
            {
                if (objNhomDVKhauTruTuDongUuTien.Contains(hd.MaLDV))
                {
                    if (hd.ConNo > 0)
                    {
                        if (eachThuTruocKH > hd.ConNo)
                        {
                            hd.IsChon = true;
                            hd.KhauTru = hd.ConNo;
                            eachThuTruocKH = eachThuTruocKH - hd.ConNo.Value;
                        }
                        else
                        {
                            if (eachThuTruocKH > 0)
                            {
                                hd.IsChon = true;
                                hd.KhauTru = eachThuTruocKH;
                                eachThuTruocKH = 0;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    else
                    {
                        hd.IsChon = false;
                    }
                }
            }
            //}

            #endregion

            #region thu khau tru tu dong


            var objPT = new ptPhieuThu();
            objPT.MaTN = _MaTN;
            objPT.NgayThu = dtNgayThu;
            string SoChungTu = Common.CreatePhieuThu(0, objPT.NgayThu.Value.Month, objPT.NgayThu.Value.Year, 0, _MaTN, true);
            objPT.SoPT = SoChungTu;

            objPT.SoTien = ltData.Where(p => p.IsChon == true).Sum(p => p.KhauTru).GetValueOrDefault();
            if (objPT.SoTien == 0)
                return 0;
            objPT.MaKH = eachMaKH;
            objPT.MaPL = 1;
            objPT.NguoiNop = TenNguoiNop;
            objPT.DiaChiNN = DiaChiNguoiNop;
            objPT.LyDo = this.GetDienGiai(ltData);
            objPT.ChungTuGoc = string.Empty;
            objPT.MaNV = Common.User.MaNV;
            objPT.MaNVN = Common.User.MaNV;
            objPT.NgayNhap = Common.GetDateTimeSystem();
            objPT.MaTKNH = null;
            objPT.MaMB = MaMB;
            objPT.IsKhauTru = true;
            objPT.IsKhauTruTuDong = true;
            foreach (var hd in ltData)
            {
                if (hd.IsChon == true)
                {
                    var objCT = new ptChiTietPhieuThu();
                    objCT.TableName = "dvHoaDon";
                    objCT.LinkID = hd.ID;
                    objCT.SoTien = 0;
                    objCT.PhaiThu = hd.PhaiThu;
                    objCT.KhauTru = hd.KhauTru;
                    objCT.ThuThua = 0;
                    objCT.DienGiai = hd.DienGiai;
                    objPT.ptChiTietPhieuThus.Add(objCT);
                }
            }

            db.ptPhieuThus.InsertOnSubmit(objPT);
            db.SubmitChanges();
            foreach (var hd in objPT.ptChiTietPhieuThus)
            {
                //Lưu vào sổ quỹ
                Common.SoQuy_InsertIsKhauTruTuDong(db, hd.ptPhieuThu.NgayThu.Value.Month, hd.ptPhieuThu.NgayThu.Value.Year, _MaTN, hd.ptPhieuThu.MaKH, hd.ptPhieuThu.MaMB, hd.MaPT, hd.ID, hd.ptPhieuThu.NgayThu, hd.ptPhieuThu.SoPT, 0, 1, true, hd.PhaiThu.GetValueOrDefault(), hd.SoTien.GetValueOrDefault(), hd.ThuThua.GetValueOrDefault(), hd.KhauTru.GetValueOrDefault(), hd.LinkID, "dvHoaDon", hd.DienGiai, Common.User.MaNV, hd.ptPhieuThu.IsKhauTru.GetValueOrDefault(), hd.ptPhieuThu.IsKhauTruTuDong, false);
            }
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thêm khấu trừ thu trước tự động - theo dòng chọn", "Lưu", "Số phiếu: " + SoChungTu + " - Dự án: " + db.tnToaNhas.Single(p => p.MaTN == _MaTN).TenTN);
            #endregion
            return 1;

        }

        private void itemKhautruTudongCanhan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                DateTime dtNgayThu = Common.GetDateTimeSystem();
                var frm = new frmSelectMonth();
                frm.ShowDialog();
                if (frm.IsSave)
                {
                    dtNgayThu = new DateTime(frm.Nam, frm.Thang, 1);
                }
                else
                {
                    return;
                }
                var indexs = gvHoaDon.GetSelectedRows();
                if (indexs.Length == 0)
                {
                    DialogBox.Alert("Vui lòng chọn khách hàng");
                    return;
                }

                //var ltMaKH = new List<int>();
                using (var db = new MasterDataContext())
                {
                    //var _MaTN = (byte)itemToaNha.EditValue;
                    foreach (var i in indexs)
                    {
                        int eachMaKH = (int)gvHoaDon.GetRowCellValue(i, "MaKH");
                        var objSoTien = db.SoQuy_ThuChis.Where(p => p.IsPhieuThu == true && p.MaKH == eachMaKH).Sum(s => s.ThuThua.GetValueOrDefault() - s.KhauTru.GetValueOrDefault());
                        decimal eachThuTruocKH = objSoTien;

                        if (eachThuTruocKH > 0)
                            KhauTruTuDong(i,dtNgayThu);

                    }
                }
                DialogBox.Success("Đã thực hiện khấu trừ tự động");
            }
            catch (Exception)
            {
                DialogBox.Alert("Kiểm tra lại các số tiền thu trước có hay không ");
            }
        }

        private void itemKhautruTudongTatca_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DateTime dtNgayThu = Common.GetDateTimeSystem();
            var frm = new frmSelectMonth();
            frm.ShowDialog();
            if (frm.IsSave)
            {
                dtNgayThu = new DateTime(frm.Nam, frm.Thang, 1);
            }
            else
            {
                return;
            }
            var wait = DialogBox.WaitingForm();
            try
            {
                int count = 0;
                using (var db = new MasterDataContext())
                {
                    for (int i = 0; i < gvHoaDon.RowCount; i++)
                    {
                        int eachMaKH = (int)gvHoaDon.GetRowCellValue(i, "MaKH");
                        decimal eachThuTruocKH = 0;
                        try
                        {
                            var objSoTien = db.SoQuy_ThuChis.Where(p => p.IsPhieuThu == true && p.MaKH == eachMaKH).Sum(s => s.ThuThua.GetValueOrDefault() - s.KhauTru.GetValueOrDefault());
                            eachThuTruocKH = objSoTien;
                        }
                        catch
                        {
                        }
                        if (eachThuTruocKH > 0)
                            count = count + KhauTruTuDong(i,dtNgayThu);
                    }
                }
                if (count > 0)
                {
                    DialogBox.Success("Đã thực hiện khấu trừ tự động được " + count.ToString() + " khách hàng");
                }
            }
            catch (Exception)
            {

            }
            finally
            {
                wait.Close();
            }
        }

        private void itemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Commoncls.ExportExcel(gcHoaDon);
        }
    }
}
