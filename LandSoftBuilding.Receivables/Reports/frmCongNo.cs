using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Library;
using System.Linq;
using System.Data.Linq.SqlClient;
using System.IO;
using DevExpress.XtraReports.UI;
using DevExpress.Data.PivotGrid;
using DevExpress.XtraGrid.Views.Grid;
using Remotion.FunctionalProgramming;

namespace LandSoftBuilding.Receivables.Reports
{
    public partial class frmCongNo : DevExpress.XtraEditors.XtraForm
    {
        public frmCongNo()
        {
            InitializeComponent();
        }

        void SetDate(int index)
        {
            var objKBC = new KyBaoCao()
            {
                Index = index
            };
            objKBC.SetToDate();

            itemTuNgay.EditValue = objKBC.DateFrom;
            itemDenNgay.EditValue = objKBC.DateTo;
        }

        List<byte?> GetToaNha()
        {
            var ltToaNha = new List<byte?>();
            var arrMaTN = (itemToaNha.EditValue ?? "").ToString().Split(',');
            foreach (var s in arrMaTN)
                if (s != "")
                    ltToaNha.Add(byte.Parse(s));
            return ltToaNha;
        }
        public class TKCongNoCls
        {
            public string KyBC { set; get; }
            public byte? MaTN { set; get; }
            public int? MaKH { set; get; }
            public int? MaMB { set; get; }
            public int? MaLDV { set; get; }
            public decimal? SoTien { set; get; }
        }
        void LoadData()
        {
            var wait = DialogBox.WaitingForm();
            var db = new MasterDataContext();
            try
            {
                db.CommandTimeout = 100000;
                var ltToaNha = this.GetToaNha();
                var _TuNgay = (DateTime)itemTuNgay.EditValue;
                var _DenNgay = (DateTime)itemDenNgay.EditValue;

                #region Báo cáo trên bản demo
                //var ltDauKy = (from hd in db.dvHoaDons
                //               where ltToaNha.Contains(hd.MaTN) & SqlMethods.DateDiffDay(hd.NgayTT, _TuNgay) > 0 & hd.IsDuyet == true
                //               group hd by new { hd.MaTN, hd.MaKH, hd.MaMB, hd.MaLDV, hd.ID } into gr
                //               select new 
                //               {
                //                   KyBC = "1. Đầu kỳ",
                //                   gr.Key.MaTN,
                //                   gr.Key.MaKH,
                //                   gr.Key.MaMB,
                //                   gr.Key.MaLDV,
                //                   SoTien = gr.Sum(p => p.PhaiThu)
                //               }).Select(p => new TKCongNoCls
                //              {
                //                  KyBC=p.KyBC,
                //                  MaTN=p.MaTN,
                //                  MaKH=p.MaKH,
                //                  MaMB = p.MaMB,
                //                  MaLDV=p.MaLDV,
                //                  SoTien = p.SoTien
                //              }).ToList();
                //var objtemp = (from sq in db.SoQuy_ThuChis
                //               join hd in db.dvHoaDons on sq.LinkID equals hd.ID into _squy
                //               from hd in _squy.DefaultIfEmpty()
                //               where ltToaNha.Contains(sq.MaTN) & SqlMethods.DateDiffDay(sq.NgayPhieu, _TuNgay) > 0 & sq.MaLoaiPhieu != 24
                //               select new TKCongNoCls
                //               {
                //                   KyBC = "1. Đầu kỳ",
                //                   MaTN=sq.MaTN,
                //                   MaKH =sq.MaKH,
                //                   MaMB=sq.MaMB,
                //                   MaLDV = hd == null ? (int?)0 : hd.MaLDV,
                //                   SoTien = sq.DaThu + sq.KhauTru - sq.ThuThua
                //               }).ToList();
                //ltDauKy = ltDauKy.Concat(from ct in objtemp
                //                         group ct by new { ct.KyBC, ct.MaTN, ct.MaKH, ct.MaMB, ct.MaLDV } into gr
                //                         select new TKCongNoCls
                //                         {
                //                             KyBC = gr.Key.KyBC,
                //                             MaTN = gr.Key.MaTN,
                //                             MaKH = gr.Key.MaKH,
                //                             MaMB = gr.Key.MaMB,
                //                             MaLDV = gr.Key.MaLDV,
                //                             SoTien = -gr.Sum(p => p.SoTien)
                //                         }).ToList();
                //var KHDK = (from dk in ltDauKy
                //            group dk by new { dk.MaTN, dk.MaKH } into gr
                //            select new
                //            {
                //                MaTN = gr.Key.MaTN,
                //                MaKH = gr.Key.MaKH,
                //                TongTien = gr.Sum(p => p.SoTien)
                //            });
                //foreach (var item in KHDK)
                //{
                //    if (item.TongTien < 0)
                //    {
                //       ltDauKy.Where(p => p.MaKH == item.MaKH && p.MaTN == item.MaTN).Select(n=>{n.SoTien=0; return n;}).ToList();
                //    }
                //}

                ////Phát sinh trong kỳ
                //var ltPhatSinh = (from hd in db.dvHoaDons
                //                  where ltToaNha.Contains(hd.MaTN) & SqlMethods.DateDiffDay(_TuNgay, hd.NgayTT) >= 0 //& hd.MaKH==2584
                //                  & SqlMethods.DateDiffDay(hd.NgayTT, _DenNgay) >= 0
                //                  & hd.IsDuyet == true
                //                  group hd by new { hd.MaTN, hd.MaKH, hd.MaMB, hd.MaLDV } into gr
                //                  select new TKCongNoCls
                //                  {
                //                      KyBC = "2. Phát sinh",
                //                      MaTN = gr.Key.MaTN,
                //                      MaKH = gr.Key.MaKH,
                //                      MaMB = gr.Key.MaMB,
                //                      MaLDV = gr.Key.MaLDV,
                //                      SoTien = gr.Sum(p => p.PhaiThu)
                //                  }).ToList();

                //////Đã thu trong kỳ
                //var ltDaThu = (from ct in db.SoQuy_ThuChis
                //               join hd in db.dvHoaDons on new { ct.TableName, ct.LinkID } equals new { TableName = "dvHoaDon", LinkID = (long?)hd.ID }
                //               where ltToaNha.Contains(ct.MaTN) && ct.MaLoaiPhieu != 24
                //               & SqlMethods.DateDiffDay(_TuNgay, ct.NgayPhieu) >= 0
                //               & SqlMethods.DateDiffDay(ct.NgayPhieu, _DenNgay) >= 0
                //               & hd.IsDuyet == true
                //               group ct by new { ct.MaTN, ct.MaKH, ct.MaMB, hd.MaLDV } into gr
                //               select new TKCongNoCls
                //               {
                //                   KyBC = "3. Đã thu",
                //                   MaTN = gr.Key.MaTN,
                //                   MaKH = gr.Key.MaKH,
                //                   MaMB = gr.Key.MaMB,
                //                   MaLDV = gr.Key.MaLDV,
                //                   SoTien = -gr.Sum(p => p.DaThu)
                //               }).ToList();
                //////Đã thu trong kỳ
                //ltDaThu = ltDaThu.Concat(from ct in db.SoQuy_ThuChis
                //                         where ltToaNha.Contains(ct.MaTN)
                //                         & SqlMethods.DateDiffDay(_TuNgay, ct.NgayPhieu) >= 0
                //                         & SqlMethods.DateDiffDay(ct.NgayPhieu, _DenNgay) >= 0
                //                         & ct.LinkID == null && ct.MaLoaiPhieu != 24
                //                         group ct by new { ct.MaTN, ct.MaKH, ct.MaMB } into gr
                //                         select new TKCongNoCls
                //                         {
                //                             KyBC = "3. Đã thu",
                //                             MaTN = gr.Key.MaTN,
                //                             MaKH = gr.Key.MaKH,
                //                             MaMB = gr.Key.MaMB,
                //                             MaLDV = (int?)0,
                //                             SoTien = -gr.Sum(p => p.DaThu)
                //                         }).ToList();

                //////Đã khau tru trong kỳ
                //var ltKhauTru = (from ct in db.SoQuy_ThuChis
                //                 join hd in db.dvHoaDons on new { ct.TableName, ct.LinkID } equals new { TableName = "dvHoaDon", LinkID = (long?)hd.ID }
                //                 where ltToaNha.Contains(ct.MaTN)
                //                 & SqlMethods.DateDiffDay(_TuNgay, ct.NgayPhieu) >= 0 & SqlMethods.DateDiffDay(ct.NgayPhieu, _DenNgay) >= 0 & hd.IsDuyet == true
                //                 //& hd.MaKH == 2584
                //                 group ct by new { ct.MaTN, ct.MaKH, hd.MaMB, hd.MaLDV } into gr
                //                 select new TKCongNoCls
                //                 {
                //                     KyBC = "4. Đã khấu trừ",
                //                     MaTN = gr.Key.MaTN,
                //                     MaKH = gr.Key.MaKH,
                //                     MaMB = gr.Key.MaMB,
                //                     MaLDV = gr.Key.MaLDV,
                //                     SoTien = -gr.Sum(p => p.KhauTru)
                //                 }).ToList();

                //var ltThuTruoc = (from ct in db.SoQuy_ThuChis
                //                  join hd in db.dvHoaDons on new { ct.TableName, ct.LinkID } equals new { TableName = "dvHoaDon", LinkID = (long?)hd.ID } into hoaDon
                //                  from hd in hoaDon.DefaultIfEmpty()
                //                  where ltToaNha.Contains(ct.MaTN) & SqlMethods.DateDiffDay(ct.NgayPhieu, _DenNgay) >= 0
                //                  group new
                //                      {
                //                          ct,
                //                          hd
                //                      }
                //                      by new { ct.MaTN, ct.MaKH, MaMB = ct.MaMB, MaLDV = hd != null ? hd.MaLDV : 0 }
                //                      into g
                //                      select new TKCongNoCls
                //                      {
                //                          KyBC = "5.Thu trước",
                //                          MaTN = g.Key.MaTN,
                //                          MaKH = g.Key.MaKH,
                //                          MaMB = g.Key.MaMB,
                //                          MaLDV = g.Key.MaLDV,

                //                          SoTien = g.Sum(c => c.ct.ThuThua - c.ct.KhauTru)
                //                      }).ToList();

                //var ltThuTruocTK = (from ct in db.SoQuy_ThuChis
                //                    join hd in db.dvHoaDons on new { ct.TableName, ct.LinkID } equals new { TableName = "dvHoaDon", LinkID = (long?)hd.ID } into hoaDon
                //                    from hd in hoaDon.DefaultIfEmpty()
                //                    where ltToaNha.Contains(ct.MaTN) & SqlMethods.DateDiffDay(_TuNgay, ct.NgayPhieu) >= 0 & SqlMethods.DateDiffDay(ct.NgayPhieu, _DenNgay) >= 0
                //                    group new
                //                        {
                //                            ct,
                //                            hd
                //                        }
                //                        by new { ct.MaTN, ct.MaKH, MaMB = ct.MaMB, MaLDV = hd != null ? hd.MaLDV : 0 }
                //                        into g
                //                        select new TKCongNoCls
                //                        {
                //                            KyBC = "5.Thu trước",
                //                            MaTN = g.Key.MaTN,
                //                            MaKH = g.Key.MaKH,
                //                            MaMB = g.Key.MaMB,
                //                            MaLDV = g.Key.MaLDV,
                //                            SoTien = g.Sum(c => c.ct.ThuThua)
                //                        }).ToList();
                //var ltData = ltDauKy.Concat(ltPhatSinh).Concat(ltDaThu).Concat(ltKhauTru).Concat(ltThuTruocTK);
                //var ltTongCong = (from ct in ltData
                //                  where ltToaNha.Contains(ct.MaTN) //& ct.MaKH == 2584
                //                  group ct by new { ct.MaTN, ct.MaKH, ct.MaMB, ct.MaLDV } into gr
                //                  select new TKCongNoCls
                //                  {
                //                      KyBC = "6. Tổng cộng",
                //                      MaTN = gr.Key.MaTN,
                //                      MaKH = gr.Key.MaKH,
                //                      MaMB = gr.Key.MaMB,
                //                      MaLDV = gr.Key.MaLDV,
                //                      SoTien = gr.Sum(p => p.SoTien)
                //                  }).ToList();
                //var TongCong = (from dk in ltTongCong
                //            group dk by new { dk.MaTN, dk.MaKH } into gr
                //            select new
                //            {
                //                MaTN = gr.Key.MaTN,
                //                MaKH = gr.Key.MaKH,
                //                TongTien = gr.Sum(p => p.SoTien)
                //            });
                //foreach (var item in TongCong)
                //{
                //    if (item.TongTien < 0)
                //    {
                //        ltTongCong.Where(p => p.MaKH == item.MaKH && p.MaTN == item.MaTN).Select(n => { n.SoTien = 0; return n; }).ToList();
                //    }
                //}
                //var ltData1 = ltDauKy.Concat(ltPhatSinh).Concat(ltDaThu).Concat(ltKhauTru).Concat(ltThuTruoc).Concat(ltTongCong).ToList();
                //var objKH = (from kh in db.tnKhachHangs
                //             where ltToaNha.Contains(kh.MaTN)
                //             select new
                //             {
                //                 kh.MaKH,
                //                 kh.KyHieu,
                //                 kh.MaPhu,
                //                 TenKH = kh.IsCaNhan == true ? (kh.HoKH + " " + kh.TenKH) : kh.CtyTen,
                //             }).ToList();
                //var objLoaiDV = (from ldv in db.dvLoaiDichVus
                //                 select new
                //                 {
                //                     ldv.ID,
                //                     TenLDV = ldv.TenHienThi
                //                 }).ToList();
                //var objMB = (from mb in db.mbMatBangs
                //             join tl in db.mbTangLaus on mb.MaTL equals tl.MaTL into tblTangLau
                //             from tl in tblTangLau.DefaultIfEmpty()
                //             join kn in db.mbKhoiNhas on tl.MaKN equals kn.MaKN into tblKhoiNha
                //             from kn in tblKhoiNha.DefaultIfEmpty()
                //             join tn in db.tnToaNhas on mb.MaTN equals tn.MaTN
                //             join lmb in db.mbLoaiMatBangs on mb.MaLMB equals lmb.MaLMB into tblLoaiMatBang
                //             from lmb in tblLoaiMatBang.DefaultIfEmpty()
                //             where ltToaNha.Contains(mb.MaTN)
                //             select new
                //             {
                //                 mb.MaMB,
                //                 lmb.TenLMB,
                //                 tn.TenTN,
                //                 kn.TenKN,
                //                 tl.TenTL,
                //                 mb.MaSoMB,
                //             }).ToList();
                ////Nap vào pivot
                //var listTong = (from kh in objKH
                //                join hd in ltData1 on kh.MaKH equals hd.MaKH
                //                join l in objLoaiDV on hd.MaLDV equals l.ID into _dv
                //                from l in _dv.DefaultIfEmpty()
                //                join mb in objMB on hd.MaMB equals mb.MaMB
                //                select new
                //                {
                //                    hd.KyBC,
                //                    mb.TenLMB,
                //                    mb.TenTN,
                //                    mb.TenKN,
                //                    mb.TenTL,
                //                    mb.MaSoMB,
                //                    kh.KyHieu,
                //                    kh.MaPhu,
                //                    TenKH = kh.TenKH,
                //                    TenLDV = hd.MaLDV == 0 ? "Phí khác" : l.TenLDV,
                //                    SoTien = hd.SoTien
                //                }).ToList();
                //pvHoaDon.DataSource = listTong;
                #endregion

                #region code mới
                var ltDauKy = (from hd in db.dvHoaDons
                               where ltToaNha.Contains(hd.MaTN) & SqlMethods.DateDiffDay(hd.NgayTT, _TuNgay) > 0 & hd.IsDuyet == true  //& hd.MaKH == 3099
                               group hd by new { hd.MaTN, hd.MaKH, hd.MaMB, hd.MaLDV, hd.ID } into gr
                               select new
                               {
                                   KyBC = "1. Đầu kỳ",
                                   gr.Key.MaTN,
                                   gr.Key.MaKH,
                                   gr.Key.MaMB,
                                   gr.Key.MaLDV,
                                   SoTien = gr.Sum(p => p.PhaiThu)
                               }).Select(p => new TKCongNoCls
                               {
                                   KyBC = p.KyBC,
                                   MaTN = p.MaTN,
                                   MaKH = p.MaKH,
                                   MaMB = p.MaMB,
                                   MaLDV = p.MaLDV,
                                   SoTien = p.SoTien
                               }).ToList();
                var objtemp = (from sq in db.SoQuy_ThuChis
                               join hd in db.dvHoaDons on sq.LinkID equals hd.ID into _squy
                               from hd in _squy.DefaultIfEmpty()
                               where ltToaNha.Contains(sq.MaTN) & SqlMethods.DateDiffDay(sq.NgayPhieu, _TuNgay) > 0 & sq.MaLoaiPhieu != 24 //& sq.MaKH == 3099
                               select new TKCongNoCls
                               {
                                   KyBC = "1. Đầu kỳ",
                                   MaTN = sq.MaTN,
                                   MaKH = sq.MaKH,
                                   MaMB = sq.MaMB,
                                   MaLDV = hd == null ? (int?)0 : hd.MaLDV,
                                   SoTien = sq.DaThu + sq.KhauTru - sq.ThuThua
                               }).ToList();
                ltDauKy = ltDauKy.Concat(from ct in objtemp
                                         group ct by new { ct.KyBC, ct.MaTN, ct.MaKH, ct.MaMB, ct.MaLDV } into gr
                                         select new TKCongNoCls
                                         {
                                             KyBC = gr.Key.KyBC,
                                             MaTN = gr.Key.MaTN,
                                             MaKH = gr.Key.MaKH,
                                             MaMB = gr.Key.MaMB,
                                             MaLDV = gr.Key.MaLDV,
                                             SoTien = -gr.Sum(p => p.SoTien)
                                         }).ToList();
                var KHDK = (from dk in ltDauKy
                            group dk by new { dk.MaTN, dk.MaKH } into gr
                            select new
                            {
                                MaTN = gr.Key.MaTN,
                                MaKH = gr.Key.MaKH,
                                TongTien = gr.Sum(p => p.SoTien)
                            });
                foreach (var item in KHDK)
                {
                    if (item.TongTien < 0)
                    {
                        ltDauKy.Where(p => p.MaKH == item.MaKH && p.MaTN == item.MaTN).Select(n => { n.SoTien = 0; return n; }).ToList();
                    }
                }

                //Phát sinh trong kỳ
                var ltPhatSinh = (from hd in db.dvHoaDons
                                  where ltToaNha.Contains(hd.MaTN) & SqlMethods.DateDiffDay(_TuNgay, hd.NgayTT) >= 0 //& hd.MaKH==3099
                                  & SqlMethods.DateDiffDay(hd.NgayTT, _DenNgay) >= 0
                                  & hd.IsDuyet == true
                                  group hd by new { hd.MaTN, hd.MaKH, hd.MaMB, hd.MaLDV } into gr
                                  select new TKCongNoCls
                                  {
                                      KyBC = "2. Phát sinh",
                                      MaTN = gr.Key.MaTN,
                                      MaKH = gr.Key.MaKH,
                                      MaMB = gr.Key.MaMB,
                                      MaLDV = gr.Key.MaLDV,
                                      SoTien = gr.Sum(p => p.PhaiThu)
                                  }).ToList();

                ////Đã thu trong kỳ
                var ltDaThu = (from ct in db.SoQuy_ThuChis
                               join hd in db.dvHoaDons on new { ct.TableName, ct.LinkID } equals new { TableName = "dvHoaDon", LinkID = (long?)hd.ID } into hoaDon
                               from hd in hoaDon.DefaultIfEmpty()
                               where ltToaNha.Contains(ct.MaTN) && ct.MaLoaiPhieu != 24
                               & SqlMethods.DateDiffDay(_TuNgay, ct.NgayPhieu) >= 0
                               & SqlMethods.DateDiffDay(ct.NgayPhieu, _DenNgay) >= 0
                               & ((hd != null && hd.IsDuyet == true) || hd == null) //& ct.MaKH == 3099 
                               & ct.IsKhauTru == false
                               group ct by new { ct.MaTN, ct.MaKH, ct.MaMB, MaLDV = hd == null ? 0 : hd.MaLDV } into gr
                               select new TKCongNoCls
                               {
                                   KyBC = "3. Đã thu",
                                   MaTN = gr.Key.MaTN,
                                   MaKH = gr.Key.MaKH,
                                   MaMB = gr.Key.MaMB,
                                   MaLDV = gr.Key.MaLDV,
                                   SoTien = -gr.Sum(p => p.DaThu)
                               }).ToList();
                ////Đã thu trong kỳ
                //ltDaThu = ltDaThu.Concat(from ct in db.SoQuy_ThuChis
                //                         join hd in db.dvHoaDons on new { ct.TableName, ct.LinkID } equals new { TableName = "dvHoaDon", LinkID = (long?)hd.ID } into hoaDon
                //                         from hd in hoaDon.DefaultIfEmpty()
                //                         where ltToaNha.Contains(ct.MaTN)
                //                         & SqlMethods.DateDiffDay(_TuNgay, ct.NgayPhieu) >= 0
                //                         & SqlMethods.DateDiffDay(ct.NgayPhieu, _DenNgay) >= 0
                //                         & ct.LinkID == null && ct.MaLoaiPhieu != 24 & ct.MaKH == 3099
                //                         group ct by new { ct.MaTN, ct.MaKH, ct.MaMB } into gr
                //                         select new TKCongNoCls
                //                         {
                //                             KyBC = "3. Đã thu",
                //                             MaTN = gr.Key.MaTN,
                //                             MaKH = gr.Key.MaKH,
                //                             MaMB = gr.Key.MaMB,
                //                             MaLDV = (int?)0,
                //                             SoTien = -gr.Sum(p => p.DaThu)
                //                         }).ToList();

                ////Đã khau tru trong kỳ
                var ltKhauTru = (from ct in db.SoQuy_ThuChis
                                 join hd in db.dvHoaDons on new { ct.TableName, ct.LinkID } equals new { TableName = "dvHoaDon", LinkID = (long?)hd.ID } into hoaDon
                                 from hd in hoaDon.DefaultIfEmpty()
                                 where ltToaNha.Contains(ct.MaTN)
                                 & SqlMethods.DateDiffDay(_TuNgay, ct.NgayPhieu) >= 0 & SqlMethods.DateDiffDay(ct.NgayPhieu, _DenNgay) >= 0 & ((hd != null && hd.IsDuyet == true) || hd == null) & ct.IsKhauTru == true //& ct.MaKH == 3099
                                 //& hd.MaKH == 2584
                                 group ct by new { ct.MaTN, ct.MaKH, ct.MaMB, MaLDV = hd == null ? 0 : hd.MaLDV } into gr
                                 select new TKCongNoCls
                                 {
                                     KyBC = "4. Đã khấu trừ",
                                     MaTN = gr.Key.MaTN,
                                     MaKH = gr.Key.MaKH,
                                     MaMB = gr.Key.MaMB,
                                     MaLDV = gr.Key.MaLDV,
                                     SoTien = -gr.Sum(p => p.KhauTru + p.DaThu)
                                 }).ToList();

                // tách thu trước và thu trước trong kỳ + hiển thị
                var ltThuTruoc = (from ct in db.SoQuy_ThuChis
                                  join hd in db.dvHoaDons on new { ct.TableName, ct.LinkID } equals new { TableName = "dvHoaDon", LinkID = (long?)hd.ID } into hoaDon
                                  from hd in hoaDon.DefaultIfEmpty()
                                  where ltToaNha.Contains(ct.MaTN) & SqlMethods.DateDiffDay(ct.NgayPhieu, _DenNgay) >= 0
                                        && ct.IsPhieuThu == true && ct.MaLoaiPhieu != 24  //& ct.MaKH == 3099
                                  group new
                                  {
                                      ct
                                  }
                                      by new { ct.MaTN, ct.MaKH, MaMB = ct.MaMB, MaLDV = hd != null ? hd.MaLDV : 0 }
                                      into g
                                      select new TKCongNoCls
                                      {

                                          KyBC = "5.Thu trước",
                                          MaTN = g.Key.MaTN,
                                          MaKH = g.Key.MaKH,
                                          MaMB = g.Key.MaMB,
                                          MaLDV = g.Key.MaLDV,

                                          SoTien = -g.Sum(c => c.ct.ThuThua - c.ct.KhauTru)
                                      }).ToList();

                var ltThuTruocTK = (from ct in db.SoQuy_ThuChis
                                    join hd in db.dvHoaDons on new { ct.TableName, ct.LinkID } equals new { TableName = "dvHoaDon", LinkID = (long?)hd.ID } into hoaDon
                                    from hd in hoaDon.DefaultIfEmpty()
                                    where 
                                        SqlMethods.DateDiffMonth(ct.NgayPhieu, _TuNgay) == 0 & ltToaNha.Contains(ct.MaTN)
                                                                                             && ct.IsPhieuThu == true && ct.MaLoaiPhieu != 24 
                                    group new
                                    {
                                        ct
                                    }
                                        by new { ct.MaTN, ct.MaKH, MaMB = ct.MaMB, MaLDV = hd != null ? hd.MaLDV : 0 }
                                        into g
                                        select new TKCongNoCls
                                        {
                                            KyBC = "6.Thu trước trong kỳ",
                                            MaTN = g.Key.MaTN,
                                            MaKH = g.Key.MaKH,
                                            MaMB = g.Key.MaMB,
                                            MaLDV = g.Key.MaLDV,
                                            SoTien = g.Sum(c => c.ct.ThuThua)
                                        }).ToList();
                //var ltData = ltDauKy.Concat(ltPhatSinh).Concat(ltDaThu).Concat(ltKhauTru).Concat(ltThuTruocTK);
                var ltData = ltDauKy.Concat(ltPhatSinh).Concat(ltDaThu).Concat(ltKhauTru).Concat(ltThuTruoc).Concat(ltThuTruocTK).ToList();
                var ltTongCong = (from ct in ltData
                                  where ltToaNha.Contains(ct.MaTN) //& ct.MaKH == 2584
                                  group ct by new { ct.MaTN, ct.MaKH, ct.MaMB, ct.MaLDV } into gr
                                  select new TKCongNoCls
                                  {
                                      KyBC = "7. Tổng cộng",
                                      MaTN = gr.Key.MaTN,
                                      MaKH = gr.Key.MaKH,
                                      MaMB = gr.Key.MaMB,
                                      MaLDV = gr.Key.MaLDV,
                                      SoTien = gr.Sum(p => p.SoTien)
                                  }).ToList();

                var ltData1 = ltDauKy.Concat(ltPhatSinh).Concat(ltDaThu).Concat(ltKhauTru).Concat(ltThuTruoc).Concat(ltThuTruocTK).Concat(ltTongCong).ToList();
                //var ltData1 = ltDauKy.Concat(ltPhatSinh).Concat(ltDaThu).Concat(ltKhauTru).Concat(ltThuTruocTK).Concat(ltTongCong).ToList();
                var objKH = (from kh in db.tnKhachHangs
                             where ltToaNha.Contains(kh.MaTN)
                             select new
                             {
                                 kh.MaKH,
                                 kh.KyHieu,
                                 kh.MaPhu,
                                 TenKH = kh.IsCaNhan == true ? (kh.HoKH + " " + kh.TenKH) : kh.CtyTen,
                             }).ToList();
                var objLoaiDV = (from ldv in db.dvLoaiDichVus
                                 select new
                                 {
                                     ldv.ID,
                                     TenLDV = ldv.TenHienThi
                                 }).ToList();
                var objMB = (from mb in db.mbMatBangs
                             join tl in db.mbTangLaus on mb.MaTL equals tl.MaTL into tblTangLau
                             from tl in tblTangLau.DefaultIfEmpty()
                             join kn in db.mbKhoiNhas on tl.MaKN equals kn.MaKN into tblKhoiNha
                             from kn in tblKhoiNha.DefaultIfEmpty()
                             join tn in db.tnToaNhas on mb.MaTN equals tn.MaTN
                             join lmb in db.mbLoaiMatBangs on mb.MaLMB equals lmb.MaLMB into tblLoaiMatBang
                             from lmb in tblLoaiMatBang.DefaultIfEmpty()
                             where ltToaNha.Contains(mb.MaTN)
                             select new
                             {
                                 mb.MaMB,
                                 lmb.TenLMB,
                                 tn.TenTN,
                                 kn.TenKN,
                                 tl.TenTL,
                                 mb.MaSoMB,
                             }).ToList();
                //Nap vào pivot
                var listTong = (from kh in objKH
                                join hd in ltData1 on kh.MaKH equals hd.MaKH
                                join l in objLoaiDV on hd.MaLDV equals l.ID into _dv
                                from l in _dv.DefaultIfEmpty()
                                join mb in objMB on hd.MaMB equals mb.MaMB
                                select new
                                {
                                    hd.KyBC,
                                    mb.TenLMB,
                                    mb.TenTN,
                                    mb.TenKN,
                                    mb.TenTL,
                                    mb.MaSoMB,
                                    kh.KyHieu,
                                    kh.MaPhu,
                                    TenKH = kh.TenKH,
                                    TenLDV = hd.MaLDV == 0 ? "Thu trước" : l.TenLDV,
                                    SoTien = hd.SoTien
                                }).ToList();
                pvHoaDon.DataSource = listTong;
                #endregion

                #region Báo cáo trên bản TTC
                //var ltDauKy = (from hd in db.dvHoaDons
                //               where ltToaNha.Contains(hd.MaTN) & SqlMethods.DateDiffDay(hd.NgayTT, _TuNgay) > 0 & hd.IsDuyet == true  //& hd.MaKH == 3099
                //               group hd by new { hd.MaTN, hd.MaKH, hd.MaMB, hd.MaLDV, hd.ID } into gr
                //               select new
                //               {
                //                   KyBC = "1. Đầu kỳ",
                //                   gr.Key.MaTN,
                //                   gr.Key.MaKH,
                //                   gr.Key.MaMB,
                //                   gr.Key.MaLDV,
                //                   SoTien = gr.Sum(p => p.PhaiThu)
                //               }).Select(p => new TKCongNoCls
                //               {
                //                   KyBC = p.KyBC,
                //                   MaTN = p.MaTN,
                //                   MaKH = p.MaKH,
                //                   MaMB = p.MaMB,
                //                   MaLDV = p.MaLDV,
                //                   SoTien = p.SoTien
                //               }).ToList();
                //var objtemp = (from sq in db.SoQuy_ThuChis
                //               join hd in db.dvHoaDons on sq.LinkID equals hd.ID into _squy
                //               from hd in _squy.DefaultIfEmpty()
                //               where ltToaNha.Contains(sq.MaTN) & SqlMethods.DateDiffDay(sq.NgayPhieu, _TuNgay) > 0 & sq.MaLoaiPhieu != 24 //& sq.MaKH == 3099
                //               select new TKCongNoCls
                //               {
                //                   KyBC = "1. Đầu kỳ",
                //                   MaTN = sq.MaTN,
                //                   MaKH = sq.MaKH,
                //                   MaMB = sq.MaMB,
                //                   MaLDV = hd == null ? (int?)0 : hd.MaLDV,
                //                   SoTien = sq.DaThu + sq.KhauTru - sq.ThuThua
                //               }).ToList();
                //ltDauKy = ltDauKy.Concat(from ct in objtemp
                //                         group ct by new { ct.KyBC, ct.MaTN, ct.MaKH, ct.MaMB, ct.MaLDV } into gr
                //                         select new TKCongNoCls
                //                         {
                //                             KyBC = gr.Key.KyBC,
                //                             MaTN = gr.Key.MaTN,
                //                             MaKH = gr.Key.MaKH,
                //                             MaMB = gr.Key.MaMB,
                //                             MaLDV = gr.Key.MaLDV,
                //                             SoTien = -gr.Sum(p => p.SoTien)
                //                         }).ToList();
                //var KHDK = (from dk in ltDauKy
                //            group dk by new { dk.MaTN, dk.MaKH } into gr
                //            select new
                //            {
                //                MaTN = gr.Key.MaTN,
                //                MaKH = gr.Key.MaKH,
                //                TongTien = gr.Sum(p => p.SoTien)
                //            });
                //foreach (var item in KHDK)
                //{
                //    if (item.TongTien < 0)
                //    {
                //        ltDauKy.Where(p => p.MaKH == item.MaKH && p.MaTN == item.MaTN).Select(n => { n.SoTien = 0; return n; }).ToList();
                //    }
                //}

                ////Phát sinh trong kỳ
                //var ltPhatSinh = (from hd in db.dvHoaDons
                //                  where ltToaNha.Contains(hd.MaTN) & SqlMethods.DateDiffDay(_TuNgay, hd.NgayTT) >= 0 //& hd.MaKH==3099
                //                  & SqlMethods.DateDiffDay(hd.NgayTT, _DenNgay) >= 0
                //                  & hd.IsDuyet == true
                //                  group hd by new { hd.MaTN, hd.MaKH, hd.MaMB, hd.MaLDV } into gr
                //                  select new TKCongNoCls
                //                  {
                //                      KyBC = "2. Phát sinh",
                //                      MaTN = gr.Key.MaTN,
                //                      MaKH = gr.Key.MaKH,
                //                      MaMB = gr.Key.MaMB,
                //                      MaLDV = gr.Key.MaLDV,
                //                      SoTien = gr.Sum(p => p.PhaiThu)
                //                  }).ToList();

                //////Đã thu trong kỳ
                //var ltDaThu = (from ct in db.SoQuy_ThuChis
                //               join hd in db.dvHoaDons on new { ct.TableName, ct.LinkID } equals new { TableName = "dvHoaDon", LinkID = (long?)hd.ID } into hoaDon
                //               from hd in hoaDon.DefaultIfEmpty()
                //               where ltToaNha.Contains(ct.MaTN) && ct.MaLoaiPhieu != 24
                //               & SqlMethods.DateDiffDay(_TuNgay, ct.NgayPhieu) >= 0
                //               & SqlMethods.DateDiffDay(ct.NgayPhieu, _DenNgay) >= 0
                //               & hd.IsDuyet == true //& ct.MaKH == 3099
                //               group ct by new { ct.MaTN, ct.MaKH, ct.MaMB, MaLDV = hd == null ? 0 : hd.MaLDV } into gr
                //               select new TKCongNoCls
                //               {
                //                   KyBC = "3. Đã thu",
                //                   MaTN = gr.Key.MaTN,
                //                   MaKH = gr.Key.MaKH,
                //                   MaMB = gr.Key.MaMB,
                //                   MaLDV = gr.Key.MaLDV,
                //                   SoTien = -gr.Sum(p => p.DaThu)
                //               }).ToList();
                //////Đã thu trong kỳ
                ////ltDaThu = ltDaThu.Concat(from ct in db.SoQuy_ThuChis
                ////                         join hd in db.dvHoaDons on new { ct.TableName, ct.LinkID } equals new { TableName = "dvHoaDon", LinkID = (long?)hd.ID } into hoaDon
                ////                         from hd in hoaDon.DefaultIfEmpty()
                ////                         where ltToaNha.Contains(ct.MaTN)
                ////                         & SqlMethods.DateDiffDay(_TuNgay, ct.NgayPhieu) >= 0
                ////                         & SqlMethods.DateDiffDay(ct.NgayPhieu, _DenNgay) >= 0
                ////                         & ct.LinkID == null && ct.MaLoaiPhieu != 24 & ct.MaKH == 3099
                ////                         group ct by new { ct.MaTN, ct.MaKH, ct.MaMB } into gr
                ////                         select new TKCongNoCls
                ////                         {
                ////                             KyBC = "3. Đã thu",
                ////                             MaTN = gr.Key.MaTN,
                ////                             MaKH = gr.Key.MaKH,
                ////                             MaMB = gr.Key.MaMB,
                ////                             MaLDV = (int?)0,
                ////                             SoTien = -gr.Sum(p => p.DaThu)
                ////                         }).ToList();

                //////Đã khau tru trong kỳ
                //var ltKhauTru = (from ct in db.SoQuy_ThuChis
                //                 join hd in db.dvHoaDons on new { ct.TableName, ct.LinkID } equals new { TableName = "dvHoaDon", LinkID = (long?)hd.ID } into hoaDon
                //                 from hd in hoaDon.DefaultIfEmpty()
                //                 where ltToaNha.Contains(ct.MaTN)
                //                 & SqlMethods.DateDiffDay(_TuNgay, ct.NgayPhieu) >= 0 & SqlMethods.DateDiffDay(ct.NgayPhieu, _DenNgay) >= 0 & hd.IsDuyet == true //& ct.MaKH == 3099
                //                 //& hd.MaKH == 2584
                //                 group ct by new { ct.MaTN, ct.MaKH, ct.MaMB, MaLDV = hd == null ? 0 : hd.MaLDV } into gr
                //                 select new TKCongNoCls
                //                 {
                //                     KyBC = "4. Đã khấu trừ",
                //                     MaTN = gr.Key.MaTN,
                //                     MaKH = gr.Key.MaKH,
                //                     MaMB = gr.Key.MaMB,
                //                     MaLDV = gr.Key.MaLDV,
                //                     SoTien = -gr.Sum(p => p.KhauTru)
                //                 }).ToList();

                //// tách thu trước và thu trước trong kỳ + hiển thị
                //var ltThuTruoc = (from ct in db.SoQuy_ThuChis
                //                  join hd in db.dvHoaDons on new { ct.TableName, ct.LinkID } equals new { TableName = "dvHoaDon", LinkID = (long?)hd.ID } into hoaDon
                //                  from hd in hoaDon.DefaultIfEmpty()
                //                  where ltToaNha.Contains(ct.MaTN) & SqlMethods.DateDiffDay(ct.NgayPhieu, _DenNgay) >= 0
                //                        && ct.IsPhieuThu == true && ct.MaLoaiPhieu != 24  //& ct.MaKH == 3099
                //                  group new
                //                  {
                //                      ct
                //                  }
                //                      by new { ct.MaTN, ct.MaKH, MaMB = ct.MaMB, MaLDV = hd != null ? hd.MaLDV : 0 }
                //                      into g
                //                      select new TKCongNoCls
                //                      {

                //                          KyBC = "5.Thu trước",
                //                          MaTN = g.Key.MaTN,
                //                          MaKH = g.Key.MaKH,
                //                          MaMB = g.Key.MaMB,
                //                          MaLDV = g.Key.MaLDV,

                //                          SoTien = -g.Sum(c => c.ct.ThuThua - c.ct.KhauTru)
                //                      }).ToList();

                //var ltThuTruocTK = (from ct in db.SoQuy_ThuChis
                //                    join hd in db.dvHoaDons on new { ct.TableName, ct.LinkID } equals new { TableName = "dvHoaDon", LinkID = (long?)hd.ID } into hoaDon
                //                    from hd in hoaDon.DefaultIfEmpty()
                //                    where //ltToaNha.Contains(ct.MaTN) & SqlMethods.DateDiffDay(_TuNgay, ct.NgayPhieu) >= 0 & SqlMethods.DateDiffDay(ct.NgayPhieu, _DenNgay) >= 0
                //                        SqlMethods.DateDiffMonth(ct.NgayPhieu, _TuNgay) == 0 & ltToaNha.Contains(ct.MaTN)
                //                                                                             && ct.IsPhieuThu == true && ct.MaLoaiPhieu != 24 //& ct.MaKH == 3099
                //                    group new
                //                    {
                //                        ct
                //                    }
                //                        by new { ct.MaTN, ct.MaKH, MaMB = ct.MaMB, MaLDV = hd != null ? hd.MaLDV : 0 }
                //                        into g
                //                        select new TKCongNoCls
                //                        {
                //                            KyBC = "6.Thu trước trong kỳ",
                //                            MaTN = g.Key.MaTN,
                //                            MaKH = g.Key.MaKH,
                //                            MaMB = g.Key.MaMB,
                //                            MaLDV = g.Key.MaLDV,
                //                            SoTien = g.Sum(c => c.ct.ThuThua)
                //                        }).ToList();
                ////var ltData = ltDauKy.Concat(ltPhatSinh).Concat(ltDaThu).Concat(ltKhauTru).Concat(ltThuTruocTK);
                //var ltData = ltDauKy.Concat(ltPhatSinh).Concat(ltDaThu).Concat(ltKhauTru).Concat(ltThuTruoc).Concat(ltThuTruocTK).ToList();
                //var ltTongCong = (from ct in ltData
                //                  where ltToaNha.Contains(ct.MaTN) //& ct.MaKH == 2584
                //                  group ct by new { ct.MaTN, ct.MaKH, ct.MaMB, ct.MaLDV } into gr
                //                  select new TKCongNoCls
                //                  {
                //                      KyBC = "7. Tổng cộng",
                //                      MaTN = gr.Key.MaTN,
                //                      MaKH = gr.Key.MaKH,
                //                      MaMB = gr.Key.MaMB,
                //                      MaLDV = gr.Key.MaLDV,
                //                      SoTien = gr.Sum(p => p.SoTien)
                //                  }).ToList();
                ////var TongCong = (from dk in ltTongCong
                ////                group dk by new { dk.MaTN, dk.MaKH } into gr
                ////                select new
                ////                {
                ////                    MaTN = gr.Key.MaTN,
                ////                    MaKH = gr.Key.MaKH,
                ////                    TongTien = gr.Sum(p => p.SoTien)
                ////                });
                ////foreach (var item in TongCong)
                ////{
                ////    if (item.TongTien < 0)
                ////    {
                ////        ltTongCong.Where(p => p.MaKH == item.MaKH && p.MaTN == item.MaTN).Select(n => { n.SoTien = 0; return n; }).ToList();
                ////    }
                ////}
                //var ltData1 = ltDauKy.Concat(ltPhatSinh).Concat(ltDaThu).Concat(ltKhauTru).Concat(ltThuTruoc).Concat(ltThuTruocTK).Concat(ltTongCong).ToList();
                ////var ltData1 = ltDauKy.Concat(ltPhatSinh).Concat(ltDaThu).Concat(ltKhauTru).Concat(ltThuTruocTK).Concat(ltTongCong).ToList();
                //var objKH = (from kh in db.tnKhachHangs
                //             where ltToaNha.Contains(kh.MaTN)
                //             select new
                //             {
                //                 kh.MaKH,
                //                 kh.KyHieu,
                //                 kh.MaPhu,
                //                 TenKH = kh.IsCaNhan == true ? (kh.HoKH + " " + kh.TenKH) : kh.CtyTen,
                //             }).ToList();
                //var objLoaiDV = (from ldv in db.dvLoaiDichVus
                //                 select new
                //                 {
                //                     ldv.ID,
                //                     TenLDV = ldv.TenHienThi
                //                 }).ToList();
                //var objMB = (from mb in db.mbMatBangs
                //             join tl in db.mbTangLaus on mb.MaTL equals tl.MaTL into tblTangLau
                //             from tl in tblTangLau.DefaultIfEmpty()
                //             join kn in db.mbKhoiNhas on tl.MaKN equals kn.MaKN into tblKhoiNha
                //             from kn in tblKhoiNha.DefaultIfEmpty()
                //             join tn in db.tnToaNhas on mb.MaTN equals tn.MaTN
                //             join lmb in db.mbLoaiMatBangs on mb.MaLMB equals lmb.MaLMB into tblLoaiMatBang
                //             from lmb in tblLoaiMatBang.DefaultIfEmpty()
                //             where ltToaNha.Contains(mb.MaTN)
                //             select new
                //             {
                //                 mb.MaMB,
                //                 lmb.TenLMB,
                //                 tn.TenTN,
                //                 kn.TenKN,
                //                 tl.TenTL,
                //                 mb.MaSoMB,
                //             }).ToList();
                ////Nap vào pivot
                //var listTong = (from kh in objKH
                //                join hd in ltData1 on kh.MaKH equals hd.MaKH
                //                join l in objLoaiDV on hd.MaLDV equals l.ID into _dv
                //                from l in _dv.DefaultIfEmpty()
                //                join mb in objMB on hd.MaMB equals mb.MaMB
                //                select new
                //                {
                //                    hd.KyBC,
                //                    mb.TenLMB,
                //                    mb.TenTN,
                //                    mb.TenKN,
                //                    mb.TenTL,
                //                    mb.MaSoMB,
                //                    kh.KyHieu,
                //                    kh.MaPhu,
                //                    TenKH = kh.TenKH,
                //                    TenLDV = hd.MaLDV == 0 ? "Phí khác" : l.TenLDV,
                //                    SoTien = hd.SoTien
                //                }).ToList();
                //pvHoaDon.DataSource = listTong;
                #endregion

                
            }
            catch
            {
            }
            finally
            {
                db.Dispose();
                wait.Close();
            }
        }


        void Print()
        {
            var rpt = new rptCongNo(Common.User.MaTN.Value);
            var stream = new System.IO.MemoryStream();
            var _KyBC = (itemKyBaoCao.EditValue ?? "").ToString();
            var _TuNgay = (DateTime)itemTuNgay.EditValue;
            var _DenNgay = (DateTime)itemDenNgay.EditValue;

            pvHoaDon.OptionsView.ShowColumnHeaders = false;
            pvHoaDon.OptionsView.ShowDataHeaders = false;
            pvHoaDon.OptionsView.ShowFilterHeaders = false;
            pvHoaDon.SavePivotGridToStream(stream);
            pvHoaDon.OptionsView.ShowColumnHeaders = true;
            pvHoaDon.OptionsView.ShowDataHeaders = true;
            pvHoaDon.OptionsView.ShowFilterHeaders = true;

            rpt.LoadData(_KyBC, _TuNgay, _DenNgay, stream);
            rpt.ShowPreviewDialog();
        }

        private void frmCongNo_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);

            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);

            ckbToaNha.DataSource = Common.TowerList;
            itemToaNha.EditValue = Common.User.MaTN.ToString();

            KyBaoCao objKBC = new KyBaoCao();
            objKBC.Initialize(cmbKyBaoCao);
            var index = DateTime.Now.Month + 8;
            itemKyBaoCao.EditValue = objKBC.Source[index];
            SetDate(index);

            LoadData();
        }

        private void cmbKyBaoCao_EditValueChanged(object sender, EventArgs e)
        {
            SetDate((sender as ComboBoxEdit).SelectedIndex);
        }

        private void btnNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void itemPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Print();
        }

        private void pvHoaDon_FieldValueDisplayText(object sender, DevExpress.XtraPivotGrid.PivotFieldDisplayTextEventArgs e)
        {
            if (e.ValueType == DevExpress.XtraPivotGrid.PivotGridValueType.Total)
                e.DisplayText = string.Format("{0} ({1})", e.Value, "Tổng");
            else if (e.ValueType == DevExpress.XtraPivotGrid.PivotGridValueType.GrandTotal)
                e.DisplayText = "Tổng cộng";
        }
    }
}