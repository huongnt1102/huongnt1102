using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Library;
using System.Data.Linq.SqlClient;

namespace LandSoftBuilding.Receivables.HopDongThue
{
    public static class PaymentCls
    {
        public static List<PaymentItemHDT> GetListPaymentByIDLiquidate(int id)
        {
            var ltData = new List<PaymentItemHDT>();
            try
            {
                //using (var db = new MasterDataContext())
                //{
                //    var objTL = db.ctThanhLies.Single(o => o.ID == id);

                //    #region Tính tiền còn nợ hóa đơn
                //    var ltMB = objTL.ctHopDong.ctLichThanhToans.Select(o => o.MaMB).Distinct();

                //    var ltHoaDon = (from hd in db.dvHoaDons

                //                    join ltt in db.ctLichThanhToans on new { hd.TableName, hd.LinkID } equals new { TableName = "ctLichThanhToan", LinkID = (int?)ltt.ID } into lichthanhtoan
                //                    from ltt in lichthanhtoan.DefaultIfEmpty()

                //                    join ddh in db.dvDienDieuHoas on new { hd.TableName, hd.LinkID } equals new { TableName = "dvDienDieuHoa", LinkID = (int?)ddh.ID } into dieuhoa
                //                    from ddh in dieuhoa.DefaultIfEmpty()

                //                    where hd.MaKH == objTL.ctHopDong.MaKH
                //                    & SqlMethods.DateDiffDay(hd.NgayTT, objTL.NgayTL) >= 0
                //                    & hd.IsDuyet == true
                //                    & ltMB.Contains(hd.MaMB)
                //                    select new PaymentItemHDT
                //                    {
                //                        MaLTT = ltt.ID,
                //                        //MaHD = hd.idctHopDong ?? objTL.ctHopDong.ID,
                //                        //SoHD = hd.ctHopDong != null ? hd.ctHopDong.SoHDCT : objTL.ctHopDong.SoHDCT,
                //                        ID = (int?)hd.ID,
                //                        DienGiai = hd.DienGiai,
                //                        TableName = "dvHoaDon",
                //                        ThucThu = 0,
                //                        ThangTT = string.Format("{0:yyyy-MM}", hd.NgayTT),
                //                        NgayTT = hd.NgayTT,
                //                        ConNo = hd.PhaiThu
                //                                -
                //                                (from ct in db.ptChiTietPhieuThus
                //                                 join pt in db.ptPhieuThus on ct.MaPT equals pt.ID
                //                                 where ct.LinkID == hd.ID
                //                                 select ct.SoTien).Sum().GetValueOrDefault()
                //                                 -
                //                                 (from ct in db.ktttChiTiets
                //                                  join pt in db.ktttKhauTruThuTruocs on ct.MaCT equals pt.ID
                //                                  where ct.LinkID == hd.ID
                //                                  select ct.SoTien).Sum().GetValueOrDefault()
                //                                  -
                //                                 (from ct in db.ktttChiTietBBQs
                //                                  join pt in db.ktttKhauTruThuTruocBBQs on ct.MaCT equals pt.ID
                //                                  where ct.LinkID == hd.ID
                //                                  select ct.SoTien).Sum().GetValueOrDefault()
                //                                   -
                //                                   (from ct in db.ktttChiTietTheBois
                //                                    join pt in db.ktttKhauTruThuTruocTheBois on ct.MaCT equals pt.ID
                //                                    where ct.LinkID == hd.ID
                //                                    select ct.SoTien).Sum().GetValueOrDefault()
                //                                    -
                //                                    (from ct in db.ktttChiTietGYMs
                //                                     join pt in db.ktttKhauTruThuTruocGYMs on ct.MaCT equals pt.ID
                //                                     where ct.LinkID == hd.ID
                //                                     select ct.SoTien).Sum().GetValueOrDefault()
                //                                     -
                //                                     (from ct in db.ktttChiTietThiCongs
                //                                      join pt in db.ktttKhauTruThuTruocThiCongs on ct.MaCT equals pt.ID
                //                                      where ct.LinkID == hd.ID
                //                                      select ct.SoTien).Sum().GetValueOrDefault()
                //                                      -
                //                                      (from ct in db.ktttChiTietHDTs
                //                                       join pt in db.ktttKhauTruThuTruocHDTs on ct.MaCT equals pt.ID
                //                                       where ct.LinkID == hd.ID
                //                                       select ct.SoTien).Sum().GetValueOrDefault()
                //                    });
                //    #endregion

                //    #region Tính tiền lãi nộp chậm
                //    // Lấy những lịch thanh toán cuối
                //    var ltDotCuoi = db.dvTienLaiNopChams.Where(o => o.DenNgay == null & SqlMethods.DateDiffDay(o.TuNgay, objTL.NgayTL) >= 0)
                //                                       .Select(o => new
                //                                       {
                //                                           o.MaLTT,
                //                                           SoNgay = SqlMethods.DateDiffDay(o.TuNgay, objTL.NgayTL) + 1,
                //                                           o.SoTienTinhLai,
                //                                           o.LaiSuat,
                //                                       });

                //    var ltLichThanhToan = (from p in db.ctLichThanhToans
                //                           join hd in db.ctHopDongs on p.MaHD equals hd.ID
                //                           join kh in db.tnKhachHangs on hd.MaKH equals kh.MaKH
                //                           where p.ctHopDong.MaTN == objTL.MaTN
                //                           & p.MaHD == objTL.MaHD
                //                           select new
                //                           {
                //                               p.ID,
                //                               SoHD = hd.SoHDCT,
                //                               TenKH = kh.IsCaNhan.GetValueOrDefault() ? kh.HoKH + " " + kh.TenKH : kh.CtyTen,
                //                               TongTien = p.SoTien.GetValueOrDefault() + p.dv_SoTien.GetValueOrDefault(),
                //                               p.TuNgay,
                //                               p.DenNgay,
                //                               p.DotTT,
                //                               p.NgayHHTT,
                //                               p.SoNgayTT,
                //                               p.SoTien_hieuchinh,
                //                               p.LyDo_hieuchinh,
                //                               p.Ngay_hieuchinh,
                //                               NhanVien = p.tnNhanVien.HoTenNV,
                //                               SoNgayTam = p.dvTienLaiNopChams.Where(o => o.DenNgay != null).Sum(o => o.SoNgay).GetValueOrDefault(),
                //                               TienLaiTam = p.dvTienLaiNopChams.Where(o => o.DenNgay != null).Sum(o => o.TienLai).GetValueOrDefault(),
                //                               p.MaHD,
                //                               DaThu = db.ptChiTietPhieuThus.Where(o => o.TableName == "ctLichThanhToan" & o.LinkID == p.ID).Sum(o => o.SoTien).GetValueOrDefault()
                //                                       +
                //                                       db.ktttChiTietHDTs.Where(o => o.TableName == "ctLichThanhToan" & o.LinkID == p.ID).Sum(o => o.SoTien).GetValueOrDefault(),
                //                           }).ToList();

                //    var ltTienLaiNopCham = from p in ltLichThanhToan
                //                           join last in ltDotCuoi on p.ID equals last.MaLTT into dotcuoi
                //                           from last in dotcuoi.DefaultIfEmpty()
                //                           join hd in db.ctHopDongs on p.MaHD equals hd.ID
                //                           select new PaymentItemHDT
                //                           {
                //                               SoHD = p.SoHD,
                //                               MaHD = p.MaHD,
                //                               MaLTT = p.ID,
                //                               NgayTT = p.TuNgay,
                //                               ThangTT = string.Format("{0:yyyy-MM}", p.TuNgay),
                //                               TableName = "ctLichThanhToan",
                //                               ID = p.ID,
                //                               DienGiai = string.Format("Tiền lãi của đợt thanh toán số {0} từ ngày {1:dd/MM/yyyy} đến này {2:dd/MM/yyyy}", p.DotTT, p.TuNgay, p.DenNgay),
                //                               ThucThu = 0,
                //                               ConNo = p.TienLaiTam + (last == null ? 0 : (last.SoNgay * last.SoTienTinhLai * last.LaiSuat).GetValueOrDefault()) - p.SoTien_hieuchinh.GetValueOrDefault() - p.DaThu,
                //                           };
                //    #endregion

                //    var TienPhat = objTL.TienPhat.GetValueOrDefault()
                //                  -
                //                  db.ptChiTietPhieuThus.Where(o => o.TableName == "ctThanhLy_PhiPhat" & o.LinkID == objTL.ID).Sum(o => o.SoTien).GetValueOrDefault()
                //                  -
                //                  db.ktttChiTietHDTs.Where(o => o.TableName == "ctThanhLy_PhiPhat" & o.LinkID == objTL.ID).Sum(o => o.SoTien).GetValueOrDefault();

                //    var PhiSuaChua = objTL.PhiSuaChua.GetValueOrDefault()
                //                  -
                //                  db.ptChiTietPhieuThus.Where(o => o.TableName == "ctThanhLy_PhiSuaChua" & o.LinkID == objTL.ID).Sum(o => o.SoTien).GetValueOrDefault()
                //                  -
                //                  db.ktttChiTietHDTs.Where(o => o.TableName == "ctThanhLy_PhiSuaChua" & o.LinkID == objTL.ID).Sum(o => o.SoTien).GetValueOrDefault();

                //    ltData = ltHoaDon.Where(o => o.ConNo > 0).ToList().Concat(ltTienLaiNopCham.Where(o => o.ConNo > 0).ToList()).ToList();

                //    if (TienPhat > 0)
                //    {
                //        var ct = new PaymentItemHDT();
                //        ct.NgayTT = objTL.NgayTL;
                //        ct.ThangTT = string.Format("{0:yyyy-MM}", objTL.NgayTL);
                //        ct.ID = objTL.ID;
                //        ct.MaHD = objTL.MaHD;
                //        ct.SoHD = objTL.ctHopDong.SoHDCT;
                //        ct.TableName = "ctThanhLy_PhiPhat";
                //        ct.DienGiai = "Tiền phạt hợp đồng thuê";
                //        ct.ThucThu = 0;
                //        ct.ConNo = TienPhat;
                //        ltData.Add(ct);
                //    }

                //    if (PhiSuaChua > 0)
                //    {
                //        var ct = new PaymentItemHDT();
                //        ct.NgayTT = objTL.NgayTL;
                //        ct.ThangTT = string.Format("{0:yyyy-MM}", objTL.NgayTL);
                //        ct.ID = objTL.ID;
                //        ct.MaHD = objTL.MaHD;
                //        ct.SoHD = objTL.ctHopDong.SoHDCT;
                //        ct.TableName = "ctThanhLy_PhiSuaChua";
                //        ct.DienGiai = "Phí sửa chữa hợp đồng thuê";
                //        ct.ThucThu = 0;
                //        ct.ConNo = PhiSuaChua;
                //        ltData.Add(ct);
                //    }
                //}
            }
            catch { }

            return ltData;
        }

        public static List<PaymentItemHDT> GetListPaymentByIDCustomer(int? _maKH)
        {
            var ltData = new List<PaymentItemHDT>();

            try
            {
                var db = new MasterDataContext();

                ltData = (from hd in db.dvHoaDons

                          join l in db.dvLoaiDichVus on hd.MaLDV equals l.ID

                          join ltt in db.ctLichThanhToans on new { hd.TableName, hd.LinkID } equals new { TableName = "ctLichThanhToan", LinkID = (int?)ltt.ID } into lichthanhtoan
                          from ltt in lichthanhtoan.DefaultIfEmpty()

                          //join ddh in db.dvDienDieuHoas on new { hd.TableName, hd.LinkID } equals new { TableName = "dvDienDieuHoa", LinkID = (int?)ddh.ID } into dieuhoa
                          //from ddh in dieuhoa.DefaultIfEmpty()

                          where hd.MaKH == _maKH

                          & hd.IsHDThue.GetValueOrDefault()

                          & (ltt != null  | hd.ctHopDong != null)

                          & (
                              (ltt != null && ltt.ctHopDong != null && !ltt.ctHopDong.ctThanhLies.Any())
                               //||
                               //(ddh != null && ddh.ctHopDong != null && !ddh.ctHopDong.ctThanhLies.Any())
                               ||
                               (!hd.ctHopDong.ctThanhLies.Any())
                           )
                          select new HopDongThue.PaymentCls.PaymentItemHDT()
                          {
                              IsChon = false,
                              MaLTT = ltt.ID,
                              MaHD = hd.idctHopDong ?? ltt.MaHD,

                              SoHD = hd.idctHopDong != null ? hd.ctHopDong.SoHDCT : ltt.ctHopDong.SoHDCT,
                              ID = hd.ID,
                              MaLDV = hd.MaLDV,
                              SoTTDV = l.STT,
                              TenLDV = l.TenHienThi,
                              DienGiai = hd.DienGiai,
                              PhiDV = hd.PhiDV,
                              NgayTT = hd.NgayTT,
                              ThangTT = string.Format("{0:yyyy-MM}", hd.NgayTT),
                              KyTT = hd.KyTT,
                              TienTT = hd.TienTT,
                              TyLeCK = hd.TyLeCK,
                              TienCK = hd.TienCK,
                              TableName = "dvHoaDon",
                              LinkID = hd.LinkID,
                              PhaiThu = hd.PhaiThu,

                              DaThu = (from ct in db.ptChiTietPhieuThus
                                       join pt in db.ptPhieuThus on ct.MaPT equals pt.ID
                                       join hdct in db.dvHoaDons on ct.LinkID equals hdct.ID into tam
                                       from hdct in tam.DefaultIfEmpty()
                                       where ct.LinkID == hd.ID
                                       & ct.TableName == "dvHoaDon"
                                       select ct.SoTien).Sum().GetValueOrDefault()
                                       +
                                       (from ct in db.ktttChiTietHDTs
                                        join pt in db.ktttKhauTruThuTruocHDTs on ct.MaCT equals pt.ID
                                        join hdct in db.dvHoaDons on ct.LinkID equals hdct.ID into tam
                                        from hdct in tam.DefaultIfEmpty()
                                        where ct.LinkID == hd.ID
                                        & ct.TableName == "dvHoaDon"
                                        select ct.SoTien).Sum().GetValueOrDefault(),

                              ConNo = hd.PhaiThu.GetValueOrDefault()
                                      -
                                      ((from ct in db.ptChiTietPhieuThus
                                        join pt in db.ptPhieuThus on ct.MaPT equals pt.ID
                                        where ct.TableName == "dvHoaDon" & ct.LinkID == hd.ID
                                        select ct.SoTien).Sum().GetValueOrDefault())
                                      -
                                      (from ctkt in db.ktttChiTietHDTs
                                       join kt in db.ktttKhauTruThuTruocHDTs on ctkt.MaCT equals kt.ID
                                       where ctkt.LinkID == hd.ID
                                       select ctkt.SoTien).Sum().GetValueOrDefault(),

                              ThucThu = 0,
                              TuNgay = hd.TuNgay,
                              DenNgay = hd.DenNgay
                          }).Where(p => p.ConNo != 0).ToList();

            }
            catch(Exception ex)
            {
                DialogBox.Error(ex.Message);
            }

            return ltData;
        }

        public class PaymentItemHDT
        {
            public bool IsChon { get; set; }
            public int? MaLTT { get; set; }
            public int? MaHD { get; set; }
            public string SoHD { get; set; }
            public long? ID { get; set; }
            public int? MaLDV { get; set; }
            public string TenLDV { get; set; }
            public string DienGiai { get; set; }
            public decimal? PhiDV { get; set; }
            public DateTime? NgayTT { get; set; }
            public DateTime? TuNgay { get; set; }
            public DateTime? DenNgay { get; set; }
            public string ThangTT { get; set; }
            public decimal? KyTT { get; set; }
            public decimal? TienTT { get; set; }
            public decimal? TyLeCK { get; set; }
            public decimal? TienCK { get; set; }
            public decimal? PhaiThu { get; set; }
            public decimal? DaThu { get; set; }
            public decimal? ConNo { get; set; }
            public decimal? ThucThu { get; set; }
            public string DonVi { get; set; }
            public string NhomXe { get; set; }
            public int? SoTTDV { get; set; }
            public string TableName { get; set; }
            public int? LinkID { get; set; }
            public string SoThe { get; set; }
        }

    }
}
