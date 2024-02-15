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
    public partial class frmReceivables : DevExpress.XtraEditors.XtraForm
    {
        #region Khai báo list
        Library.Utilities.ThreadCls threads;
        object objLock = new object();
        System.Collections.Generic.List<KhachHang> objKH = new System.Collections.Generic.List<KhachHang>();
        System.Collections.Generic.List<TienCongNo> dauKy = new System.Collections.Generic.List<TienCongNo>();
        System.Collections.Generic.List<NoHoaDonDauKy> noDauKyHd1 = new System.Collections.Generic.List<NoHoaDonDauKy>();
        System.Collections.Generic.List<TienCongNo> objHD_NDK = new System.Collections.Generic.List<TienCongNo>();
        System.Collections.Generic.List<TienSoQuy> noDauKySq1 = new System.Collections.Generic.List<TienSoQuy>();
        System.Collections.Generic.List<TienCongNo> objSQ_NDK = new System.Collections.Generic.List<TienCongNo>();
        System.Collections.Generic.List<TienCongNo> objPhatSinh = new System.Collections.Generic.List<TienCongNo>();
        System.Collections.Generic.List<TienCongNo> objDaThu = new System.Collections.Generic.List<TienCongNo>();
        System.Collections.Generic.List<TienCongNo> objKhauTru = new System.Collections.Generic.List<TienCongNo>();
        System.Collections.Generic.List<TienCongNo> objThuTruoc = new System.Collections.Generic.List<TienCongNo>();
        System.Collections.Generic.List<TienCongNo> objThuTruocTrongKy = new System.Collections.Generic.List<TienCongNo>();
        System.Collections.Generic.List<Library.CongNoCls.DataCongNo> objList = new System.Collections.Generic.List<Library.CongNoCls.DataCongNo>();

        #endregion

        public frmReceivables()
        {
            InitializeComponent();

            threads = new Library.Utilities.ThreadCls(8, GetTask, GetData);
        }

        #region Class
        public class KhachHang { public System.Int32 MaKH { get; set; } public System.String KyHieu { get; set; } public System.String MaPhu { get; set; } public System.String TenKH { get; set; } public System.String DienThoai { get; set; } public System.String EmailKH { get; set; } public System.String DiaChi { get; set; } public System.Int32? MaMB { get; set; } public string MaSoMB { get; set; } }

        public class TienCongNo { public int? MaKH { get; set; } public System.Decimal SoTien { get; set; } }

        public class TienSoQuy { public int? MaKH { get; set; } public long? LinkID { get; set; } public System.Decimal SoTien { get; set; } }



        public class NoHoaDonDauKy { public System.Int64 ID { get; set; } public int? MaKH { get; set; } public System.Decimal? PhaiThu { get; set; } public int? MaLDV { get; set; } public int? MaMB { get; set; } }

        #endregion

        #region GetData

        private System.Collections.Generic.List<KhachHang> GetKhachHangs(byte? _MaTN, System.DateTime? _DenNgay)
        {
            using (Library.MasterDataContext db = new MasterDataContext())
            {
                //return (from kh in db.tnKhachHangs
                //        //join mb in db.mbMatBangs on kh.MaKH equals mb.MaKH into matBang from mb in matBang.DefaultIfEmpty()
                //        where kh.MaTN == _MaTN & (kh.IsNgungSuDung == null | kh.IsNgungSuDung == false) //&& kh.MaKH == 13259
                //        select new KhachHang
                //        {
                //            MaKH= kh.MaKH,
                //            KyHieu = kh.KyHieu,
                //            MaPhu= kh.MaPhu,
                //            TenKH = kh.IsCaNhan == true ? (kh.HoKH + " " + kh.TenKH) : kh.CtyTen,
                //            DienThoai = kh.DienThoaiKH,
                //            EmailKH= kh.EmailKH,
                //            DiaChi = kh.DCLL,
                //            MaMB = db.dvHoaDons.First(p =>
                //                p.MaKH == kh.MaKH & SqlMethods.DateDiffDay(p.NgayTT, _DenNgay) >= 0 & p.IsDuyet == true & p.IsHDThue == false &
                //                p.MaMB != null).MaMB,
                //            MaSoMB = db.mbMatBangs.FirstOrDefault(_=>_.MaKH == kh.MaKH) !=null? db.mbMatBangs.FirstOrDefault(_ => _.MaKH == kh.MaKH).MaSoMB:""
                //            //MaSoMB = mb!=null?mb.MaSoMB:"",
                //        }).ToList();
                //var param = new Dapper.DynamicParameters();
                //param = new Dapper.DynamicParameters();
                //param.Add("@TowerId", _MaTN, DbType.Byte, null, null);
                //param.Add("@DenNgay", _DenNgay, DbType.DateTime, null, null);
                //return Library.Class.Connect.QueryConnect.QueryData<KhachHang>("dbo.cn_Khach_Hang", new
                //{

                //}).ToList();
                var objCustomer = Library.Class.Connect.QueryConnect.QueryData<KhachHang>("dbo.cn_Khach_Hang", new
                {
                    TowerId = _MaTN,
                    DenNgay = _DenNgay
                });
                if (objCustomer.Count() > 0)
                {
                    return objCustomer.ToList();
                }
                else return new List<KhachHang>();
            }
        }

        private System.Collections.Generic.List<KhachHang> GetKhachHangs(byte? _MaTN, System.DateTime? _DenNgay, int? khachHangId)
        {
            using (Library.MasterDataContext db = new MasterDataContext())
            {
                return (from kh in db.tnKhachHangs
                            //join mb in db.mbMatBangs on kh.MaKH equals mb.MaKH into matBang from mb in matBang.DefaultIfEmpty()
                        where kh.MaTN == _MaTN & (kh.IsNgungSuDung == null | kh.IsNgungSuDung == false) && kh.MaKH == khachHangId
                        select new KhachHang
                        {
                            MaKH = kh.MaKH,
                            KyHieu = kh.KyHieu,
                            MaPhu = kh.MaPhu,
                            TenKH = kh.IsCaNhan == true ? (kh.HoKH + " " + kh.TenKH) : kh.CtyTen,
                            DienThoai = kh.DienThoaiKH,
                            EmailKH = kh.EmailKH,
                            DiaChi = kh.DCLL,
                            MaMB = db.dvHoaDons.First(p =>
                                p.MaKH == kh.MaKH & SqlMethods.DateDiffDay(p.NgayTT, _DenNgay) >= 0 & p.IsDuyet == true & p.IsHDThue == false &
                                p.MaMB != null).MaMB,
                            MaSoMB = db.mbMatBangs.FirstOrDefault(_ => _.MaKH == kh.MaKH) != null ? db.mbMatBangs.FirstOrDefault(_ => _.MaKH == kh.MaKH).MaSoMB : ""
                            //MaSoMB = mb!=null?mb.MaSoMB:"",
                        }).ToList();
            }
        }

        private System.Collections.Generic.List<TienCongNo> GetDauKys(byte? _MaTN, int? _Nam)
        {
            using (Library.MasterDataContext db = new MasterDataContext())
            {
                //return (from d in db.dvDauKies
                // where d.MaTN == _MaTN & d.Nam == _Nam //& d.MaKH == 2634
                // group new { d } by new { d.MaKH }
                //                 into g
                // select new TienCongNo { MaKH= g.Key.MaKH, SoTien = g.Sum(_ => _.d.SoTien).GetValueOrDefault() }).ToList();

                var param = new Dapper.DynamicParameters();
                param = new Dapper.DynamicParameters();
                param.Add("@TowerId", _MaTN, DbType.Byte, null, null);
                param.Add("@Nam", _Nam, DbType.Int32, null, null);
                return Library.Class.Connect.QueryConnect.Query<TienCongNo>("dbo.cn_Dau_Ky", param).ToList();
            }
        }

        private System.Collections.Generic.List<TienCongNo> GetDauKys(byte? _MaTN, int? _Nam, int? khachHangId)
        {
            using (Library.MasterDataContext db = new MasterDataContext())
            {
                return (from d in db.dvDauKies
                        where d.MaTN == _MaTN & d.Nam == _Nam & d.MaKH == khachHangId
                        group new { d } by new { d.MaKH }
                                 into g
                        select new TienCongNo { MaKH = g.Key.MaKH, SoTien = g.Sum(_ => _.d.SoTien).GetValueOrDefault() }).ToList();
            }
        }

        private System.Collections.Generic.List<NoHoaDonDauKy> GetNoDauKyHd1(byte? _MaTN, System.DateTime? _TuNgay, int? _Nam)
        {
            using (Library.MasterDataContext db = new Library.MasterDataContext())
            {
                return (from hd in db.dvHoaDons
                        where SqlMethods.DateDiffDay(hd.NgayTT, _TuNgay) > 0 & hd.IsDuyet == true & hd.MaTN == _MaTN & hd.NgayTT.Value.Year == _Nam & (hd.IsHDThue == null | hd.IsHDThue == false) //& hd.MaKH == 2634
                        select new NoHoaDonDauKy { ID = hd.ID, MaKH = hd.MaKH, PhaiThu = hd.PhaiThu, MaLDV = hd.MaLDV, MaMB = hd.MaMB }).ToList();
            }
        }

        private System.Collections.Generic.List<NoHoaDonDauKy> GetNoDauKyHd1(byte? _MaTN, System.DateTime? _TuNgay, int? _Nam, int? khachHangId)
        {
            using (Library.MasterDataContext db = new Library.MasterDataContext())
            {
                return (from hd in db.dvHoaDons
                        where SqlMethods.DateDiffDay(hd.NgayTT, _TuNgay) > 0 & hd.IsDuyet == true & hd.MaTN == _MaTN & hd.NgayTT.Value.Year == _Nam & (hd.IsHDThue == null | hd.IsHDThue == false) & hd.MaKH == khachHangId
                        select new NoHoaDonDauKy { ID = hd.ID, MaKH = hd.MaKH, PhaiThu = hd.PhaiThu, MaLDV = hd.MaLDV, MaMB = hd.MaMB }).ToList();
            }
        }

        private System.Collections.Generic.List<TienCongNo> GetNoDauKyHoaDon(System.Collections.Generic.List<NoHoaDonDauKy> noDauKyHd1)
        {
            return (from hd in noDauKyHd1
                    group hd by new { hd.MaKH }
                                     into ndk
                    select new TienCongNo
                    {
                        MaKH = ndk.Key.MaKH,
                        SoTien = ndk.Sum(s => s.PhaiThu).GetValueOrDefault()
                    }).ToList();
        }

        private System.Collections.Generic.List<TienSoQuy> GetNoDauKySq1(byte? _MaTN, System.DateTime? _TuNgay, int? _Nam)
        {
            using (Library.MasterDataContext db = new Library.MasterDataContext())
            {
                return (from sq in db.SoQuy_ThuChis
                        where SqlMethods.DateDiffDay(sq.NgayPhieu, _TuNgay) > 0 & sq.MaTN == _MaTN &
                sq.IsPhieuThu == true & sq.MaLoaiPhieu != 24 & sq.LinkID != null & sq.NgayPhieu.Value.Year == _Nam &
                sq.TableName == "dvHoaDon" //& sq.MaKH == 2634 
                        select new TienSoQuy
                        {
                            MaKH = sq.MaKH,
                            LinkID = sq.LinkID,
                            //SoTien = sq.DaThu.GetValueOrDefault() + sq.KhauTru.GetValueOrDefault() -
                            //         sq.ThuThua.GetValueOrDefault()
                            SoTien = sq.DaThu.GetValueOrDefault() - sq.ThuThua.GetValueOrDefault() + sq.KhauTru.GetValueOrDefault()
                        }).ToList();
            }
        }

        private System.Collections.Generic.List<TienSoQuy> GetNoDauKySq1(byte? _MaTN, System.DateTime? _TuNgay, int? _Nam, int? khachHangId)
        {
            using (Library.MasterDataContext db = new Library.MasterDataContext())
            {
                return (from sq in db.SoQuy_ThuChis
                        where SqlMethods.DateDiffDay(sq.NgayPhieu, _TuNgay) > 0 & sq.MaTN == _MaTN &
                sq.IsPhieuThu == true & sq.MaLoaiPhieu != 24 & sq.LinkID != null & sq.NgayPhieu.Value.Year == _Nam &
                sq.TableName == "dvHoaDon" & sq.MaKH == khachHangId
                        select new TienSoQuy
                        {
                            MaKH = sq.MaKH,
                            LinkID = sq.LinkID,
                            //SoTien = sq.DaThu.GetValueOrDefault() + sq.KhauTru.GetValueOrDefault() -
                            //         sq.ThuThua.GetValueOrDefault()
                            SoTien = sq.DaThu.GetValueOrDefault() - sq.ThuThua.GetValueOrDefault() + sq.KhauTru.GetValueOrDefault()
                        }).ToList();
            }
        }

        private System.Collections.Generic.List<TienCongNo> GetNoDauKySoQuy(System.Collections.Generic.List<TienSoQuy> noDauKySq1)
        {
            return (from sq in noDauKySq1
                    group new { sq } by new { sq.MaKH }
                                     into ndk
                    select new TienCongNo { MaKH = ndk.Key.MaKH, SoTien = ndk.Sum(_ => _.sq.SoTien) })
                    .ToList();
        }

        private System.Collections.Generic.List<TienCongNo> GetPhatSinhs(byte? _MaTN, System.DateTime? _TuNgay)
        {
            using (Library.MasterDataContext db = new Library.MasterDataContext())
            {
                return (from hd in db.dvHoaDons
                        where SqlMethods.DateDiffMonth(hd.NgayTT, _TuNgay) == 0 & hd.IsDuyet == true && hd.MaTN == _MaTN & (hd.IsHDThue == null | hd.IsHDThue == false)
                        //&& hd.MaKH == 13259
                        group hd by hd.MaKH
                                       into ps
                        select new TienCongNo
                        {
                            MaKH = ps.Key,
                            SoTien = ps.Sum(s => s.PhaiThu).GetValueOrDefault(),
                        }).ToList();
            }
        }

        private System.Collections.Generic.List<TienCongNo> GetPhatSinhs(byte? _MaTN, System.DateTime? _TuNgay, int? khachHangId)
        {
            using (Library.MasterDataContext db = new Library.MasterDataContext())
            {
                return (from hd in db.dvHoaDons
                        where SqlMethods.DateDiffMonth(hd.NgayTT, _TuNgay) == 0 & hd.IsDuyet == true && hd.MaTN == _MaTN & (hd.IsHDThue == null | hd.IsHDThue == false)
                        && hd.MaKH == khachHangId
                        group hd by hd.MaKH
                                       into ps
                        select new TienCongNo
                        {
                            MaKH = ps.Key,
                            SoTien = ps.Sum(s => s.PhaiThu).GetValueOrDefault(),
                        }).ToList();
            }
        }

        private System.Collections.Generic.List<TienCongNo> GetDaThus(byte? _MaTN, System.DateTime? _TuNgay)
        {
            using (Library.MasterDataContext db = new Library.MasterDataContext())
            {
                return (from ct in db.SoQuy_ThuChis
                        join hd in db.dvHoaDons on new { ct.TableName, ct.LinkID } equals new { TableName = "dvHoaDon", LinkID = (long?)hd.ID } into hoaDon
                        from hd in hoaDon.DefaultIfEmpty()
                        where SqlMethods.DateDiffMonth(ct.NgayPhieu, _TuNgay) == 0
                              && ct.IsPhieuThu == true && ct.MaLoaiPhieu != 24 && ct.MaTN == _MaTN & ct.IsKhauTru == false & ct.LinkID != null & hd.IsDuyet == true & (hd.IsHDThue == null | hd.IsHDThue == false)
                        //&& ct.MaKH == 13259
                        group ct by ct.MaKH
                                    into dt
                        select new TienCongNo
                        {
                            MaKH = dt.Key,
                            //DaThu = dt.Sum(s => s.DaThu).GetValueOrDefault()
                            SoTien = dt.Sum(s => s.DaThu).GetValueOrDefault() - dt.Sum(_ => _.ThuThua).GetValueOrDefault() - dt.Sum(_ => _.KhauTru).GetValueOrDefault()
                        }).ToList();
            }
        }

        private System.Collections.Generic.List<TienCongNo> GetDaThus(byte? _MaTN, System.DateTime? _TuNgay, int? khachHangId)
        {
            using (Library.MasterDataContext db = new Library.MasterDataContext())
            {
                return (from ct in db.SoQuy_ThuChis
                        join hd in db.dvHoaDons on new { ct.TableName, ct.LinkID } equals new { TableName = "dvHoaDon", LinkID = (long?)hd.ID } into hoaDon
                        from hd in hoaDon.DefaultIfEmpty()
                        where SqlMethods.DateDiffMonth(ct.NgayPhieu, _TuNgay) == 0
                              && ct.IsPhieuThu == true && ct.MaLoaiPhieu != 24 && ct.MaTN == _MaTN & ct.IsKhauTru == false & ct.LinkID != null & hd.IsDuyet == true & (hd.IsHDThue == null | hd.IsHDThue == false)
                        && ct.MaKH == khachHangId
                        group ct by ct.MaKH
                                    into dt
                        select new TienCongNo
                        {
                            MaKH = dt.Key,
                            //DaThu = dt.Sum(s => s.DaThu).GetValueOrDefault()
                            SoTien = dt.Sum(s => s.DaThu).GetValueOrDefault() - dt.Sum(_ => _.ThuThua).GetValueOrDefault() - dt.Sum(_ => _.KhauTru).GetValueOrDefault()
                        }).ToList();
            }
        }

        private System.Collections.Generic.List<TienCongNo> GetKhauTrus(byte? _MaTN, System.DateTime? _TuNgay)
        {
            using (Library.MasterDataContext db = new Library.MasterDataContext())
            {
                return (from ct in db.SoQuy_ThuChis
                        join hd in db.dvHoaDons on new { ct.TableName, ct.LinkID } equals new { TableName = "dvHoaDon", LinkID = (long?)hd.ID } into hoaDon
                        from hd in hoaDon.DefaultIfEmpty()
                        where SqlMethods.DateDiffMonth(ct.NgayPhieu, _TuNgay) == 0
                              //ct.NgayPhieu.Value.Month == _TuNgay.Month
                              && ct.MaTN == _MaTN && ct.IsPhieuThu == true & ct.IsKhauTru == true & ct.LinkID != null & hd.IsDuyet == true & (hd.IsHDThue == null | hd.IsHDThue == false)
                        //&& ct.MaKH == 13259
                        group ct by ct.MaKH
                                      into kt
                        select new TienCongNo
                        {
                            MaKH = kt.Key,
                            SoTien = kt.Sum(s => s.KhauTru + s.DaThu).GetValueOrDefault()
                        }).ToList();
            }
        }

        private System.Collections.Generic.List<TienCongNo> GetKhauTrus(byte? _MaTN, System.DateTime? _TuNgay, int? khachHangId)
        {
            using (Library.MasterDataContext db = new Library.MasterDataContext())
            {
                return (from ct in db.SoQuy_ThuChis
                        join hd in db.dvHoaDons on new { ct.TableName, ct.LinkID } equals new { TableName = "dvHoaDon", LinkID = (long?)hd.ID } into hoaDon
                        from hd in hoaDon.DefaultIfEmpty()
                        where SqlMethods.DateDiffMonth(ct.NgayPhieu, _TuNgay) == 0
                              //ct.NgayPhieu.Value.Month == _TuNgay.Month
                              && ct.MaTN == _MaTN && ct.IsPhieuThu == true & ct.IsKhauTru == true & ct.LinkID != null & hd.IsDuyet == true & (hd.IsHDThue == null | hd.IsHDThue == false)
                        && ct.MaKH == khachHangId
                        group ct by ct.MaKH
                                      into kt
                        select new TienCongNo
                        {
                            MaKH = kt.Key,
                            SoTien = kt.Sum(s => s.KhauTru + s.DaThu).GetValueOrDefault()
                        }).ToList();
            }
        }

        private System.Collections.Generic.List<TienCongNo> GetThuTruocs(byte? _MaTN, System.DateTime? _DenNgay)
        {
            // sq.IsPhieuThu == true && 
            using (Library.MasterDataContext db = new Library.MasterDataContext())
            {
                return (from sq in db.SoQuy_ThuChis
                        where SqlMethods.DateDiffDay(sq.NgayPhieu, _DenNgay) >= 0 && sq.MaTN == _MaTN
                       && sq.MaLoaiPhieu != 24 //&& sq.MaKH == 13259
                                               // where SqlMethods.DateDiffDay(sq.NgayPhieu, _DenNgay) >= 0 //&& sq.MaTN == _MaTN 

                        group sq by sq.MaKH
                                       into tt
                        select new TienCongNo
                        {
                            MaKH = tt.Key,
                            SoTien = tt.Sum(s => s.ThuThua - s.KhauTru).GetValueOrDefault()
                        }).ToList();
            }
        }

        private System.Collections.Generic.List<TienCongNo> GetThuTruocs(byte? _MaTN, System.DateTime? _DenNgay, int? khachHangId)
        {
            // sq.IsPhieuThu == true && 
            using (Library.MasterDataContext db = new Library.MasterDataContext())
            {
                return (from sq in db.SoQuy_ThuChis
                        where SqlMethods.DateDiffDay(sq.NgayPhieu, _DenNgay) >= 0 && sq.MaTN == _MaTN
                       && sq.MaLoaiPhieu != 24 && sq.MaKH == khachHangId
                        // where SqlMethods.DateDiffDay(sq.NgayPhieu, _DenNgay) >= 0 //&& sq.MaTN == _MaTN 

                        group sq by sq.MaKH
                                       into tt
                        select new TienCongNo
                        {
                            MaKH = tt.Key,
                            SoTien = tt.Sum(s => s.ThuThua - s.KhauTru).GetValueOrDefault()
                        }).ToList();
            }
        }

        private System.Collections.Generic.List<TienCongNo> GetThuTruocTrongKy(byte? _MaTN, System.DateTime? _TuNgay)
        {
            //sq.IsPhieuThu == true && 
            using (Library.MasterDataContext db = new Library.MasterDataContext())
            {
                return (from sq in db.SoQuy_ThuChis
                        where SqlMethods.DateDiffMonth(sq.NgayPhieu, _TuNgay) == 0 && sq.MaTN == _MaTN
                && sq.MaLoaiPhieu != 24 //&& sq.MaKH == 13259
                        group sq by sq.MaKH
                                              into tt
                        select new TienCongNo
                        {
                            MaKH = tt.Key,
                            SoTien = tt.Sum(s => s.ThuThua).GetValueOrDefault()
                        }).ToList();
            }
        }

        private System.Collections.Generic.List<TienCongNo> GetThuTruocTrongKy(byte? _MaTN, System.DateTime? _TuNgay, int? khachHangId)
        {
            //sq.IsPhieuThu == true && 
            using (Library.MasterDataContext db = new Library.MasterDataContext())
            {
                return (from sq in db.SoQuy_ThuChis
                        where SqlMethods.DateDiffMonth(sq.NgayPhieu, _TuNgay) == 0 && sq.MaTN == _MaTN
                && sq.MaLoaiPhieu != 24 && sq.MaKH == khachHangId
                        group sq by sq.MaKH
                                              into tt
                        select new TienCongNo
                        {
                            MaKH = tt.Key,
                            SoTien = tt.Sum(s => s.ThuThua).GetValueOrDefault()
                        }).ToList();
            }
        }

        private System.Collections.Generic.List<Library.CongNoCls.DataCongNo> GetDataCongNos(System.Collections.Generic.List<KhachHang> objKH, System.Collections.Generic.List<TienCongNo> dauKy, System.Collections.Generic.List<TienCongNo> objHD_NDK, System.Collections.Generic.List<TienCongNo> objSQ_NDK, System.Collections.Generic.List<TienCongNo> objPhatSinh, System.Collections.Generic.List<TienCongNo> objDaThu, System.Collections.Generic.List<TienCongNo> objKhauTru, System.Collections.Generic.List<TienCongNo> objThuTruoc, System.Collections.Generic.List<TienCongNo> objThuTruocTrongKy)
        {
            return (from kh in objKH
                    join dk in dauKy on kh.MaKH equals dk.MaKH into soDuDauKy
                    from dk in soDuDauKy.DefaultIfEmpty()
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
                        //join tttk in objThuTruocTrongKy on kh.MaKH equals tttk.MaKH into tttrongky
                        //from tttk in tttrongky.DefaultIfEmpty()
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
                        NoDauKy = (dk != null ? dk.SoTien : (decimal?)0) + (ndk == null ? 0 : ndk.SoTien) - (sqdk == null ? 0 : sqdk.SoTien),
                        PhatSinh = ps == null ? 0 : ps.SoTien,
                        DaThu = dt == null ? 0 : dt.SoTien,
                        KhauTru = kt == null ? 0 : kt.SoTien,
                        ThuTruoc = tt == null ? 0 : tt.SoTien,
                        //ThuTruocTK = tttk == null ? 0 : tttk.SoTien,
                        MaSoMB = kh.MaSoMB,
                    }).Select(p => new Library.CongNoCls.DataCongNo
                    {
                        ThuTruoc = p.ThuTruoc,
                        //NoDauKy = p.NoDauKy < 0 ? 0 : p.NoDauKy,
                        NoDauKy = p.NoDauKy,
                        PhatSinh = p.PhatSinh,
                        KhauTru = p.KhauTru,
                        DaThu = p.DaThu,
                        //ConNo = ((p.NoDauKy < 0 ? 0 : p.NoDauKy) + p.PhatSinh - (p.DaThu + p.KhauTru - p.ThuTruocTK)) < 0 ? 0 : ((p.NoDauKy < 0 ? 0 : p.NoDauKy) + p.PhatSinh - (p.DaThu + p.KhauTru - p.ThuTruocTK)),
                        //ConNo = ((p.NoDauKy < 0 ? 0 : p.NoDauKy) + p.PhatSinh - (p.DaThu + p.KhauTru)) < 0 ? 0 : ((p.NoDauKy < 0 ? 0 : p.NoDauKy) + p.PhatSinh - (p.DaThu + p.KhauTru)),
                        //ConNo = p.NoDauKy + p.PhatSinh - (p.DaThu + p.KhauTru),
                        ConNo = p.NoDauKy + p.PhatSinh - (p.DaThu + p.KhauTru),
                        MaKH = p.MaKH,
                        KyHieu = p.KyHieu,
                        MaPhu = p.MaPhu,
                        TenKH = p.TenKH,
                        DienThoai = p.DienThoai,
                        EmailKH = p.EmailKH,
                        DiaChi = p.DiaChi,
                        MaMB = p.MaMB,
                        //NoCuoi = (((p.NoDauKy < 0 ? 0 : p.NoDauKy) + p.PhatSinh - (p.DaThu + p.KhauTru - p.ThuTruocTK)) < 0 ? 0 : ((p.NoDauKy < 0 ? 0 : p.NoDauKy) + p.PhatSinh - (p.DaThu + p.KhauTru - p.ThuTruocTK))) - p.ThuTruoc,
                        //NoCuoi = (((p.NoDauKy < 0 ? 0 : p.NoDauKy) + p.PhatSinh - (p.DaThu + p.KhauTru)) < 0 ? 0 : ((p.NoDauKy < 0 ? 0 : p.NoDauKy) + p.PhatSinh - (p.DaThu + p.KhauTru))) - p.ThuTruoc,
                        NoCuoi = (p.NoDauKy) + p.PhatSinh - (p.DaThu + p.KhauTru) - p.ThuTruoc,
                        //NoCuoi = ((p.NoDauKy) + p.PhatSinh - (p.DaThu + p.KhauTru - p.ThuTruocTK)) - p.ThuTruoc,
                        //ThuTruocTK = p.ThuTruocTK,
                        MaSoMB = p.MaSoMB
                    }).ToList();
        }

        #endregion

        private  void GetData()
        {
            objList = GetDataCongNos(objKH, dauKy, objHD_NDK, objSQ_NDK, objPhatSinh, objDaThu, objKhauTru, objThuTruoc, objThuTruocTrongKy); 

            gcHoaDon.DataSource = objList;

            itemNap.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            itemThuTien.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            itemPrint.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            itemSendMail.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            itemKhauTuDong.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            itemExport.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            itemPrintAll.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
        }

        private void GetTask()
        {
            try
            {
                var db = new MasterDataContext();

                db.CommandTimeout = 100000;
                var _MaTN = (byte)itemToaNha.EditValue;
                var _Thang = Convert.ToInt32(itemThang.EditValue);
                var _Nam = Convert.ToInt32(itemNam.EditValue);
                var _TuNgay = new DateTime(_Nam, _Thang, 1);
                var _DenNgay = Common.GetLastDayOfMonth(_Thang, _Nam);
                var _NgayHienTai = DateTime.Now;

                var index = 0;
                lock (objLock)
                {
                    index = threads.GetTaskIndex();
                }

                var param = new Dapper.DynamicParameters();
                switch (index)
                {
                    case 1:
                        // khách hàng
                        objKH = GetKhachHangs(_MaTN, _DenNgay);
                        break;
                    case 2:
                        // nợ đầu năm
                        dauKy = GetDauKys(_MaTN, _Nam);
                        break;
                    case 3:
                        // ngày đầu năm
                        // hóa đơn
                        param = new Dapper.DynamicParameters();
                        param.Add("@TowerId", _MaTN, DbType.Byte, null, null);
                        param.Add("@Nam", _Nam, DbType.Int32, null, null);
                        param.Add("@TuNgay", _TuNgay, DbType.DateTime, null, null);
                        objHD_NDK = Library.Class.Connect.QueryConnect.Query<TienCongNo>("dbo.cn_No_Dau_Ky_Hoa_Don", param).ToList();

                        break;
                    case 4:
                        // sổ quỹ
                        param = new Dapper.DynamicParameters();
                        param.Add("@TowerId", _MaTN, DbType.Byte, null, null);
                        param.Add("@Nam", _Nam, DbType.Int32, null, null);
                        param.Add("@TuNgay", _TuNgay, DbType.DateTime, null, null);
                        objSQ_NDK = Library.Class.Connect.QueryConnect.Query<TienCongNo>("dbo.cn_No_Dau_Ky_So_Quy", param).ToList();

                        break;
                    case 5:
                        // phát sinh

                        param = new Dapper.DynamicParameters();
                        param.Add("@TowerId", _MaTN, DbType.Byte, null, null);
                        param.Add("@Nam", _TuNgay.Year, DbType.Int32, null, null);
                        param.Add("@Thang", _TuNgay.Month, DbType.Int32, null, null);
                        objPhatSinh = Library.Class.Connect.QueryConnect.Query<TienCongNo>("dbo.cn_Phat_Sinh", param).ToList();

                        break;
                    case 6:
                        // đã thu

                        param = new Dapper.DynamicParameters();
                        param.Add("@TowerId", _MaTN, DbType.Byte, null, null);
                        param.Add("@TuNgay", _TuNgay, DbType.DateTime, null, null);
                        objDaThu = Library.Class.Connect.QueryConnect.Query<TienCongNo>("dbo.cn_Da_Thu", param).ToList();

                        break;
                    case 7:
                        // khầu trừ

                        param = new Dapper.DynamicParameters();
                        param.Add("@TowerId", _MaTN, DbType.Byte, null, null);
                        param.Add("@TuNgay", _TuNgay, DbType.DateTime, null, null);

                        objKhauTru = Library.Class.Connect.QueryConnect.Query<TienCongNo>("dbo.cn_Khau_Tru", param).ToList();

                        break;
                    case 8:
                        // thu trước

                        param = new Dapper.DynamicParameters();
                        param.Add("@TowerId", _MaTN, DbType.Byte, null, null);
                        param.Add("@DenNgay", _DenNgay, DbType.DateTime, null, null);
                        objThuTruoc = Library.Class.Connect.QueryConnect.Query<TienCongNo>("dbo.cn_Thu_truoc", param).ToList();

                        break;
                    case 9:
                        // thu trước trong kỳ

                        param = new Dapper.DynamicParameters();
                        param.Add("@TowerId", _MaTN, DbType.Byte, null, null);
                        param.Add("@Nam", _TuNgay.Year, DbType.Int32, null, null);
                        param.Add("@Thang", _TuNgay.Month, DbType.Int32, null, null);
                        objThuTruocTrongKy = Library.Class.Connect.QueryConnect.Query<TienCongNo>("dbo.cn_Thu_Truoc_Trong_Ky", param).ToList();

                        break;
                    default:
                        threads.Stop();
                        break;
                }
            }
            catch(System.Exception ex) { Library.DialogBox.Error(ex.Message); }
           
        }

        private void Load1KhachHang(int? khachHangId)
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

            // khách hàng
            List<KhachHang> khachHangs = new List<KhachHang>();
            khachHangs = GetKhachHangs(_MaTN, _DenNgay, khachHangId); 

            // nợ đầu năm
            List<TienCongNo> dauKy = new List<TienCongNo>();
            dauKy = GetDauKys(_MaTN, _Nam, khachHangId); 

            // hóa đơn đầu năm
            List<NoHoaDonDauKy> noHoaDonDauKies = new List<NoHoaDonDauKy>();
            List<TienCongNo> noHoaDonDauKy = new List<TienCongNo>();
            noHoaDonDauKies = GetNoDauKyHd1(_MaTN, _TuNgay, _Nam, khachHangId); 
            noHoaDonDauKy = GetNoDauKyHoaDon(noHoaDonDauKies);

            // sổ quỹ
            List<TienSoQuy> noDauKySoQuy1 = new List<TienSoQuy>();
            List<TienCongNo> noSoQuyDauKy = new List<TienCongNo>();
            noDauKySoQuy1 = GetNoDauKySq1(_MaTN, _TuNgay, _Nam, khachHangId);
            noSoQuyDauKy = GetNoDauKySoQuy(noDauKySoQuy1); 

            // phát sinh
            List<TienCongNo> phatSinh = new List<TienCongNo>();
            phatSinh = GetPhatSinhs(_MaTN, _TuNgay, khachHangId);

            // đã thu
            List<TienCongNo> daThu = new List<TienCongNo>();
            daThu = GetDaThus(_MaTN, _TuNgay, khachHangId);

            // khầu trừ
            List<TienCongNo> khauTru = new List<TienCongNo>();
            khauTru = GetKhauTrus(_MaTN, _TuNgay, khachHangId); 

            // thu trước
            List<TienCongNo> thuTruoc = new List<TienCongNo>();
             thuTruoc = GetThuTruocs(_MaTN, _DenNgay, khachHangId); 

            // thu trước trong kỳ
            List<TienCongNo> thuTruocTrongKy = new List<TienCongNo>();
            thuTruocTrongKy = GetThuTruocTrongKy(_MaTN, _TuNgay, khachHangId); 

            List<Library.CongNoCls.DataCongNo> datas = GetDataCongNos(khachHangs, dauKy, noHoaDonDauKy, noSoQuyDauKy, phatSinh, daThu, khauTru, thuTruoc, thuTruocTrongKy);
            var tam = datas.FirstOrDefault();

            //gvHoaDon.SetFocusedRowCellValue("MaKH", tam.MaKH);
            //gvHoaDon.SetFocusedRowCellValue("KyHieu", tam.KyHieu);
            //gvHoaDon.SetFocusedRowCellValue("MaPhu", tam.MaPhu);
            //gvHoaDon.SetFocusedRowCellValue("TenKH", tam.TenKH);
            //gvHoaDon.SetFocusedRowCellValue("DienThoai", tam.DienThoai);
            //gvHoaDon.SetFocusedRowCellValue("EmailKH", tam.EmailKH);
            //gvHoaDon.SetFocusedRowCellValue("DiaChi", tam.DiaChi);
            ////gvHoaDon.SetFocusedRowCellValue("LoaiMB", tam.LoaiMB);
            //gvHoaDon.SetFocusedRowCellValue("MaMB", tam.MaMB);
            //gvHoaDon.SetFocusedRowCellValue("TenLMB", tam.ten);

            gvHoaDon.SetFocusedRowCellValue("NoDauKy", tam.NoDauKy);
            gvHoaDon.SetFocusedRowCellValue("PhatSinh", tam.PhatSinh);
            gvHoaDon.SetFocusedRowCellValue("DaThu", tam.DaThu);
            gvHoaDon.SetFocusedRowCellValue("ConNo", tam.ConNo);
            gvHoaDon.SetFocusedRowCellValue("KhauTru", tam.KhauTru);
            gvHoaDon.SetFocusedRowCellValue("ThuTruoc", tam.ThuTruoc);
            gvHoaDon.SetFocusedRowCellValue("NoCuoi", tam.NoCuoi);
        }

       void LoadData()
        {
            try
            {
                gcHoaDon.DataSource = null;
                var db = new MasterDataContext();

                db.CommandTimeout = 100000;
                var _MaTN = (byte)itemToaNha.EditValue;
                var _Thang = Convert.ToInt32(itemThang.EditValue);
                var _Nam = Convert.ToInt32(itemNam.EditValue);
                var _TuNgay = new DateTime(_Nam, _Thang, 1);
                var _DenNgay = Common.GetLastDayOfMonth(_Thang, _Nam);
                var _NgayHienTai = DateTime.Now;

                itemNap.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                itemThuTien.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                itemPrint.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                itemSendMail.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                itemKhauTuDong.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                itemExport.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                itemPrintAll.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

                objKH = null;
                dauKy = null;
                noDauKyHd1 = null;
                objHD_NDK = null;
                noDauKySq1 = null;
                objSQ_NDK = null;
                objPhatSinh = null;
                objDaThu = null;
                objKhauTru = null;
                objThuTruoc = null;
                objThuTruocTrongKy = null;
                objList = new System.Collections.Generic.List<Library.CongNoCls.DataCongNo>();

                threads.RunThread();
                Detail();
            }
            catch { }
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

                var thang = Convert.ToInt32(itemThang.EditValue);
                var nam = Convert.ToInt32(itemNam.EditValue);
                var ngay = Common.GetLastDayOfMonth(thang, nam);

                switch (xtraTabControl1.SelectedTabPageIndex)
                {
                    case 0:


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
                                                join lx in db.dvgxLoaiXes on hd.MaLX equals lx.MaLX into LoaiXe
                                                from lx in LoaiXe.DefaultIfEmpty()
                                                join cc in db.CompanyCodes on hd.CompanyCode equals cc.ID into Company from cc in Company.DefaultIfEmpty()
                                                where hd.MaKH == maKH & SqlMethods.DateDiffDay(hd.NgayTT, ngay) >= 0 & hd.IsDuyet == true & hd.ConNo != 0
                                                orderby hd.NgayTT descending
                                                select new
                                                {
                                                    ID = hd.ID,
                                                    LinkID = hd.LinkID,
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
                                                    hd.TienTruocThue,
                                                    lx.TenLX,
                                                    CompanyCode  = cc.KyHieu
                                                }).ToList();
                        break;
                    case 1:
                        ctlMailHistory1.MaKH = maKH;
                        ctlMailHistory1.MailHistory_Load();
                        break;
                    case 2:
                        var objData = from p in db.ptPhieuThus
                                      join mb in db.mbMatBangs on p.MaMB equals mb.MaMB into mbang
                                      from mb in mbang.DefaultIfEmpty()
                                      join pl in db.ptPhanLoais on p.MaPL equals (int?)pl.ID into tblPhanLoai
                                      from pl in tblPhanLoai.DefaultIfEmpty()
                                      join k in db.tnKhachHangs on p.MaKH equals (int?)k.MaKH into tblKhachHang
                                      from k in tblKhachHang.DefaultIfEmpty()
                                      join nv in db.tnNhanViens on p.MaNV equals (int?)nv.MaNV into nvchinh
                                      from nv in nvchinh.DefaultIfEmpty()
                                      join nvn in db.tnNhanViens on p.MaNVN equals (int?)nvn.MaNV into nvnhap
                                      from nvn in nvnhap.DefaultIfEmpty()
                                      join tk in db.nhTaiKhoans on p.MaTKNH equals (int?)tk.ID into tblTaiKhoan
                                      from tk in tblTaiKhoan.DefaultIfEmpty()
                                      join nh in db.nhNganHangs on tk.MaNH equals (int?)nh.ID into tblNganHang
                                      from nh in tblNganHang.DefaultIfEmpty()
                                      join nt in db.ptPhieuThu_NguonThanhToans on p.NguonThanhToan equals nt.ID
                                      where p.MaKH == maKH & p.NgayThu.Value.Year == nam & p.NgayThu.Value.Month == thang & p.IsKhauTru == false & p.MaPL != 24
                                      //&& (p.MaPL != (int?)2 || (p.MaPL == (int?)2 && p.ptChiTietPhieuThus.FirstOrDefault((ptChiTietPhieuThu g) => g.LinkID == null) != null)) 
                                      select new
                                      {
                                          ID = p.ID,
                                          SoPT = p.SoPT,
                                          NgayThu = p.NgayThu,
                                          KyHieu = k.KyHieu,
                                          TenKH = ((k.IsCaNhan == (bool?)true) ? (k.HoKH + " " + k.TenKH) : k.CtyTen),
                                          NguoiThu = nv.HoTenNV,
                                          NguoiNop = p.NguoiNop,
                                          DiaChiNN = p.DiaChiNN,
                                          LyDo = p.LyDo,
                                          TenPL = pl.TenPL,
                                          ChungTuGoc = p.ChungTuGoc,
                                          NguoiNhap = nvn.HoTenNV,
                                          NgayNhap = p.NgayNhap,
                                          //NguoiSua = nvs.HoTenNV,
                                          NgaySua = p.NgaySua,
                                          PhuongThuc = ((p.MaTKNH != null) ? "Chuyển khoản" : "Tiền mặt"),
                                          SoTK = tk.SoTK,
                                          TenNH = nh.TenNH,
                                          //TenNKH = nkh.TenNKH,
                                          //TenKN = tl.mbKhoiNha.TenKN,
                                          SoTienThu = p.ptChiTietPhieuThus.Sum(ct => ct.SoTien.GetValueOrDefault()),
                                          TienPhaiThu = p.ptChiTietPhieuThus.Sum(ct => ct.PhaiThu.GetValueOrDefault()),
                                          TienKhauTru = p.ptChiTietPhieuThus.Sum(ct => ct.KhauTru.GetValueOrDefault()),
                                          TienThuThua = p.ptChiTietPhieuThus.Sum(ct => ct.ThuThua.GetValueOrDefault()),
                                          NguonThu = nt.Name,
                                      } into p
                                      select new
                                      {
                                          ID = p.ID,
                                          SoPT = p.SoPT,
                                          NgayThu = p.NgayThu, //p.NgayThu.Value.Hour.ToString().PadLeft(2, '0') + ":" + p.NgayThu.Value.Minute.ToString().PadLeft(2, '0') + " | " + p.NgayThu.Value.Day.ToString().PadLeft(2, '0') + "/" + p.NgayThu.Value.Month.ToString().PadLeft(2, '0') + "/" + p.NgayThu.Value.Year.ToString(),
                                          GioThu = p.NgayThu.Value.Hour.ToString().PadLeft(2, '0') + ":" + p.NgayThu.Value.Minute.ToString().PadLeft(2, '0'),
                                          KyHieu = p.KyHieu,
                                          TenKH = p.TenKH,
                                          NguoiThu = p.NguoiThu,
                                          NguoiNop = p.NguoiNop,
                                          DiaChiNN = p.DiaChiNN,
                                          LyDo = p.LyDo,
                                          TenPL = p.TenPL,
                                          ChungTuGoc = p.ChungTuGoc,
                                          NguoiNhap = p.NguoiNhap,
                                          NgayNhap = p.NgayNhap,
                                          //NguoiSua = p.NguoiSua,
                                          NgaySua = p.NgaySua,
                                          PhuongThuc = p.PhuongThuc,
                                          SoTK = p.SoTK,
                                          //TenKN = p.TenKN,
                                          //TenNH = p.TenNH,
                                          //TenNKH = p.TenNKH,
                                          p.TienPhaiThu,
                                          p.TienKhauTru,
                                          p.TienThuThua,
                                          p.SoTienThu,
                                          p.NguonThu

                                      };
                        gcPhieuThu.DataSource = objData;
                        break;
                    case 3:
                        var khauTru = from p in db.ptPhieuThus
                                      join mb in db.mbMatBangs on p.MaMB equals mb.MaMB into mbang
                                      from mb in mbang.DefaultIfEmpty()
                                          //join tl in db.mbTangLaus on mb.MaTL equals tl.MaTL into tlau
                                          //from tl in tlau.DefaultIfEmpty()
                                      join pl in db.ptPhanLoais on p.MaPL equals (int?)pl.ID into tblPhanLoai
                                      from pl in tblPhanLoai.DefaultIfEmpty()
                                      join k in db.tnKhachHangs on p.MaKH equals (int?)k.MaKH into tblKhachHang
                                      from k in tblKhachHang.DefaultIfEmpty()
                                          //join nkh in db.khNhomKhachHangs on k.MaNKH equals (int?)nkh.ID into tblNhomKhachHang
                                          //from nkh in tblNhomKhachHang.DefaultIfEmpty()
                                      join nv in db.tnNhanViens on p.MaNV equals (int?)nv.MaNV
                                      join nvn in db.tnNhanViens on p.MaNVN equals (int?)nvn.MaNV
                                      //join nvs in db.tnNhanViens on p.MaNVS equals (int?)nvs.MaNV into tblNguoiSua
                                      //from nvs in tblNguoiSua.DefaultIfEmpty()
                                      //join tk in db.nhTaiKhoans on p.MaTKNH equals (int?)tk.ID into tblTaiKhoan
                                      //from tk in tblTaiKhoan.DefaultIfEmpty()
                                      //join nh in db.nhNganHangs on tk.MaNH equals (int?)nh.ID into tblNganHang
                                      //from nh in tblNganHang.DefaultIfEmpty()
                                      where p.MaKH == maKH & p.NgayThu.Value.Year == nam & p.NgayThu.Value.Month == thang & p.IsKhauTru == true
                                      select new
                                      {
                                          ID = p.ID,
                                          SoPT = p.SoPT,
                                          NgayThu = p.NgayThu,
                                          KyHieu = k.KyHieu,
                                          TenKH = ((k.IsCaNhan == (bool?)true) ? (k.HoKH + " " + k.TenKH) : k.CtyTen),
                                          NguoiThu = nv.HoTenNV,
                                          NguoiNop = p.NguoiNop,
                                          DiaChiNN = p.DiaChiNN,
                                          LyDo = p.LyDo,
                                          TenPL = pl.TenPL,
                                          ChungTuGoc = p.ChungTuGoc,
                                          NguoiNhap = nvn.HoTenNV,
                                          NgayNhap = p.NgayNhap,
                                          //NguoiSua = nvs.HoTenNV,
                                          NgaySua = p.NgaySua,
                                          PhuongThuc = ((p.MaTKNH != null) ? "Chuyển khoản" : "Tiền mặt"),
                                          //SoTK = tk.SoTK,
                                          //TenNH = nh.TenNH,
                                          //TenNKH = nkh.TenNKH,
                                          //TenKN = tl.mbKhoiNha.TenKN,
                                          SoTienThu = p.ptChiTietPhieuThus.Sum(ct => ct.SoTien.GetValueOrDefault()),
                                          TienPhaiThu = p.ptChiTietPhieuThus.Sum(ct => ct.PhaiThu.GetValueOrDefault()),
                                          TienKhauTru = p.ptChiTietPhieuThus.Sum(ct => ct.KhauTru.GetValueOrDefault() + ct.SoTien.GetValueOrDefault()),
                                          TienThuThua = p.ptChiTietPhieuThus.Sum(ct => ct.ThuThua.GetValueOrDefault()),
                                      } into p
                                      select new
                                      {
                                          ID = p.ID,
                                          SoPT = p.SoPT,
                                          NgayThu = p.NgayThu, //p.NgayThu.Value.Hour.ToString().PadLeft(2, '0') + ":" + p.NgayThu.Value.Minute.ToString().PadLeft(2, '0') + " | " + p.NgayThu.Value.Day.ToString().PadLeft(2, '0') + "/" + p.NgayThu.Value.Month.ToString().PadLeft(2, '0') + "/" + p.NgayThu.Value.Year.ToString(),
                                          GioThu = p.NgayThu.Value.Hour.ToString().PadLeft(2, '0') + ":" + p.NgayThu.Value.Minute.ToString().PadLeft(2, '0'),
                                          KyHieu = p.KyHieu,
                                          TenKH = p.TenKH,
                                          NguoiThu = p.NguoiThu,
                                          NguoiNop = p.NguoiNop,
                                          DiaChiNN = p.DiaChiNN,
                                          LyDo = p.LyDo,
                                          TenPL = p.TenPL,
                                          ChungTuGoc = p.ChungTuGoc,
                                          NguoiNhap = p.NguoiNhap,
                                          NgayNhap = p.NgayNhap,
                                          //NguoiSua = p.NguoiSua,
                                          NgaySua = p.NgaySua,
                                          PhuongThuc = p.PhuongThuc,
                                          //SoTK = p.SoTK,
                                          //TenKN = p.TenKN,
                                          //TenNH = p.TenNH,
                                          //TenNKH = p.TenNKH,
                                          p.TienPhaiThu,
                                          p.TienKhauTru,
                                          p.TienThuThua,
                                          p.SoTienThu
                                      };
                        gridControl1.DataSource = khauTru;
                        break;
                    case 4:
                        var model_ls_notify = new { id = maKH };
                        var param_ls_notify = new Dapper.DynamicParameters();
                        param_ls_notify.AddDynamicParams(model_ls_notify);
                        gridControl2.DataSource = Library.Class.Connect.QueryConnect.Query<GetLichSuNotify>("dvhoadon_get_list_su_notify", param_ls_notify);
                        break;
                }
            }
            catch { }
            finally
            {
                db.Dispose();
            }
        }

        public class GetLichSuNotify
        {
            public string Token { get; set; }
            public string TieuDe { get; set; }
            public string NoiDung { get; set; }
            public string Phone { get; set; }
            public DateTime? DateCreate { get; set; }
        }

        private void linqInstantFeedbackSource1_GetQueryable(object sender, DevExpress.Data.Linq.GetQueryableEventArgs e)
        {

        }

        private void linqInstantFeedbackSource1_DismissQueryable(object sender, DevExpress.Data.Linq.GetQueryableEventArgs e)
        {
            try
            {
                (e.Tag as MasterDataContext).Dispose();
            }
            catch { }
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
                DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thêm thu tiền", "Thêm", "Khách hàng: " + gvHoaDon.GetFocusedRowCellValue("MaKH").ToString() + " -Dự án: " + lkToaNha.GetDisplayText(itemToaNha.EditValue));
                frm.MaKH = (int?)gvHoaDon.GetFocusedRowCellValue("MaKH");
                frm.MaTN = (byte?)itemToaNha.EditValue;
                frm.ShowDialog();
                if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                {
                    //LoadData1Dong((int)gvHoaDon.GetFocusedRowCellValue("MaKH"));
                    //LoadData();
                    Load1KhachHang((int)gvHoaDon.GetFocusedRowCellValue("MaKH"));
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
                Library.MasterDataContext masterDataContext = new Library.MasterDataContext();
                List<Library.CongNoCls.DataCongNo> depts = new List<Library.CongNoCls.DataCongNo>();

                //foreach (var i in indexs)
                //{

                //}
                foreach (var k in indexs)
                {
                    ltMaKH.Add((int)gvHoaDon.GetRowCellValue(k, "MaKH"));
                    var mb = masterDataContext.mbMatBangs.FirstOrDefault(_ => _.MaKH == (int)gvHoaDon.GetRowCellValue(k, "MaKH"));
                    if (mb != null) ltMB.Add(mb.MaMB);
                    //ltMB.Add((int)gvHoaDon.GetRowCellValue(k, "MaMB"));
                    var dept = objList.FirstOrDefault(_ => _.MaKH == (int)gvHoaDon.GetRowCellValue(k, "MaKH"));
                    if (dept != null)
                    {
                        depts.Add(dept);
                    }
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
                    frm.Depts = depts;
                    frm.ShowDialog(this);
                }
            }
            catch (Exception ex)
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

            Library.MasterDataContext masterDataContext = new Library.MasterDataContext();
            List<Library.CongNoCls.DataCongNo> depts = new List<Library.CongNoCls.DataCongNo>();

            foreach (var index in rows)
            {
                try
                {
                    listMaKHs.Add(Convert.ToInt32(gvHoaDon.GetRowCellValue(index, "MaKH")));
                    var mb = masterDataContext.mbMatBangs.FirstOrDefault(_ => _.MaKH == (int)gvHoaDon.GetRowCellValue(index, "MaKH"));
                    int maMb = 0;
                    if (mb != null) maMb = mb.MaMB;
                    listKhMbs.Add(new InfoCusSendMail()
                    {
                        MaKH = (int)gvHoaDon.GetRowCellValue(index, "MaKH"),
                        MaMB = maMb
                    });
                    var dept = objList.FirstOrDefault(_ => _.MaKH == (int)gvHoaDon.GetRowCellValue(index, "MaKH"));
                    if (dept != null)
                    {
                        depts.Add(dept);
                    }
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
            frm.FormName = "LandSoftBuilding.Receivables.frmReceivables.itemSendMail";
            frm.Depts = depts;

            frm.Show();

            try
            {
                var _MaTN = (byte)itemToaNha.EditValue;
                var _Thang = Convert.ToInt32(itemThang.EditValue);
                var _Nam = Convert.ToInt32(itemNam.EditValue);
                CheckGuiMail(_MaTN, _Thang, _Nam, "ThongBaoCatDichVuERP");
            }
            catch (System.Exception ex) { } //DialogBox.Error(ex.Message);
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
                DIPBMS.SystemLog.Classes.SYS_LOG.Insert("In phiếu thu", "In phiếu", "Khách hàng: " + gvHoaDon.GetRowCellValue(index, "MaKH").ToString() + " -Dự án: " + lkToaNha.GetDisplayText(itemToaNha.EditValue));
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
                Library.MasterDataContext masterDataContext = new Library.MasterDataContext();

                List<Library.CongNoCls.DataCongNo> depts = new List<Library.CongNoCls.DataCongNo>();

                //foreach (var i in indexs)
                //{

                //}
                foreach (var k in indexs)
                {
                    ltMaKH.Add((int)gvHoaDon.GetRowCellValue(k, "MaKH"));
                    var mb = masterDataContext.mbMatBangs.FirstOrDefault(_ => _.MaKH == (int)gvHoaDon.GetRowCellValue(k, "MaKH"));
                    if (mb != null) ltMB.Add(mb.MaMB);
                    //ltMB.Add((int)gvHoaDon.GetRowCellValue(k, "MaMB"));
                    var dept = objList.FirstOrDefault(_ => _.MaKH == (int)gvHoaDon.GetRowCellValue(k, "MaKH"));
                    if (dept != null)
                    {
                        depts.Add(dept);
                    }
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
                    frm.Depts = depts;
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

        string GetDienGiai(List<KhauTruTheoCompany> ltDataSubFunc)
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

            List<KhauTruTheoCompany> ltData;

            #region lấy data hóa đơn
            ltData = (from hd in db.dvHoaDons
                      join l in db.dvLoaiDichVus on hd.MaLDV equals l.ID
                      join mb in db.mbMatBangs on hd.MaMB equals mb.MaMB
                      join dvtg in db.dvgxDonViThoiGians on hd.MaDVTG equals dvtg.ID into tblDonViThoiGian
                      from dvtg in tblDonViThoiGian.DefaultIfEmpty()

                      where hd.MaKH == (int?)eachMaKH & hd.ConNo.GetValueOrDefault() != 0 & hd.IsDuyet == true
                      orderby hd.NgayTT
                      select new KhauTruTheoCompany()
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
                          CompanyCode = hd.CompanyCode
                      }).Select(p => new KhauTruTheoCompany
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
                          KhachTra = p.ConNo - 0,
                          CompanyCode = p.CompanyCode
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
                    objSoTien = db.SoQuy_ThuChis.Where(p => p.IsPhieuThu == true && p.MaKH == eachMaKH & p.CompanyCode == hd.CompanyCode).Sum(s => s.ThuThua.GetValueOrDefault() - s.KhauTru.GetValueOrDefault());
                    eachThuTruocKH = objSoTien;

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
                    objCT.MaLDV = hd.MaLDV;
                    objCT.CompanyCode = hd.CompanyCode;
                    objPT.ptChiTietPhieuThus.Add(objCT);
                }
            }

            db.ptPhieuThus.InsertOnSubmit(objPT);
            db.SubmitChanges();
            foreach (var hd in objPT.ptChiTietPhieuThus)
            {
                //Lưu vào sổ quỹ
                Common.SoQuy_InsertIsKhauTruTuDong(db, hd.ptPhieuThu.NgayThu.Value.Month, hd.ptPhieuThu.NgayThu.Value.Year, _MaTN, hd.ptPhieuThu.MaKH, hd.ptPhieuThu.MaMB, hd.MaPT, hd.ID, hd.ptPhieuThu.NgayThu, hd.ptPhieuThu.SoPT, 0, 1, true, hd.PhaiThu.GetValueOrDefault(), hd.SoTien.GetValueOrDefault(), hd.ThuThua.GetValueOrDefault(), hd.KhauTru.GetValueOrDefault(), hd.LinkID, "dvHoaDon", hd.DienGiai, Common.User.MaNV, (bool)hd.ptPhieuThu.IsKhauTru, hd.ptPhieuThu.IsKhauTruTuDong, false);
            }
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thêm khấu trừ thu trước tự động - theo dòng chọn", "Lưu", "Số phiếu: " + SoChungTu + " - Dự án: " + db.tnToaNhas.Single(p => p.MaTN == _MaTN).TenTN);
            #endregion

            Library.Class.Connect.QueryConnect.QueryData<bool>("sq_Update"
                //,
                //new
                //{
                //    IDPhieu = objCT.MaPT,
                //    IDPhieuCT = objCT.ID
                //}
                    );
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
                            KhauTruTuDong(i, dtNgayThu);

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
                            count = count + KhauTruTuDong(i, dtNgayThu);
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

        private void itemSuaPhieuThu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var id = (int?)gvPhieuThu.GetFocusedRowCellValue("ID");
            if (id == null)
            {
                DialogBox.Alert("Vui lòng chọn mẫu tin");
                return;
            }

            using (var frm = new LandSoftBuilding.Fund.Input.frmEdit())
            {
                frm.MaPT = id;
                frm.MaTN = (byte)itemToaNha.EditValue;
                frm.ShowDialog();
                if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                    Detail();
            }
        }

        private void itemSmsZalo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var rows = gvHoaDon.GetSelectedRows();
            if (rows.Length <= 0)
            {
                DialogBox.Alert("Vui lòng chọn [Khách hàng]. Xin cám ơn!");
                return;
            }

            List<Building.SMSZalo.Gui.frmSend.smsZalo> listMaKHs = new List<Building.SMSZalo.Gui.frmSend.smsZalo>();

            foreach (var index in rows)
            {
                try
                {
                    var MaKH = (int)gvHoaDon.GetRowCellValue(index, "MaKH");
                    var MaMB = (int)gvHoaDon.GetRowCellValue(index, "MaKH");
                    var NoDauKy = (decimal?)gvHoaDon.GetRowCellValue(index, "NoDauKy");
                    var PhatSinh = (decimal?)gvHoaDon.GetRowCellValue(index, "PhatSinh");
                    var DaThu = (decimal?)gvHoaDon.GetRowCellValue(index, "DaThu");
                    var KhauTru = (decimal?)gvHoaDon.GetRowCellValue(index, "KhauTru");
                    var ConNo = (decimal?)gvHoaDon.GetRowCellValue(index, "ConNo");
                    var ThuTruoc = (decimal?)gvHoaDon.GetRowCellValue(index, "ThuTruoc");
                    var NoCuoi = (decimal?)gvHoaDon.GetRowCellValue(index, "NoCuoi");
                    var _Thang = Convert.ToInt32(itemThang.EditValue);
                    var _Nam = Convert.ToInt32(itemNam.EditValue);
                    listMaKHs.Add(new Building.SMSZalo.Gui.frmSend.smsZalo
                    {
                        MaKH = MaKH,
                        MaMB = MaMB,
                        Thang = _Thang,
                        Nam = _Nam,
                        NoDauKy = NoDauKy,
                        PhatSinh = PhatSinh,
                        DaThu = DaThu,
                        KhauTru = KhauTru,
                        ConNo = ConNo,
                        ThuTruoc = ThuTruoc,
                        NoCuoi = NoCuoi,
                    });
                }
                catch { }
            }
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Gửi sms zalo", "Gửi", "Tháng " + Convert.ToInt32(itemThang.EditValue).ToString() + " - Năm " + Convert.ToInt32(itemNam.EditValue).ToString() + " -Dự án: " + lkToaNha.GetDisplayText(itemToaNha.EditValue));
            var frm = new Building.SMSZalo.Gui.frmSend();
            frm.MaTN = (byte)itemToaNha.EditValue;
            frm.Month = Convert.ToInt32(itemThang.EditValue);
            frm.Year = Convert.ToInt32(itemNam.EditValue);
            frm.ListMaKHs = listMaKHs;
            frm.Show();
        }

        /// <summary>
        /// Gửi notify đến khách hàng
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var rows = gvHoaDon.GetSelectedRows();
            if (rows.Length <= 0)
            {
                DialogBox.Alert("Vui lòng chọn [Khách hàng]. Xin cám ơn!");
                return;
            }

            if (DialogBox.Question("Bạn chắc chắn muốn gửi thông báo đến chủ hộ?") == System.Windows.Forms.DialogResult.No) return;
            var db = new MasterDataContext();
            var send = 0;
            foreach (var index in rows)
            {
                try
                {


                    var MaKH = (int)gvHoaDon.GetRowCellValue(index, "MaKH");
                    //var MaMB = (int)gvHoaDon.GetRowCellValue(index, "MaKH");
                    //var NoDauKy = (decimal?)gvHoaDon.GetRowCellValue(index, "NoDauKy");
                    //var PhatSinh = (decimal?)gvHoaDon.GetRowCellValue(index, "PhatSinh");
                    //var DaThu = (decimal?)gvHoaDon.GetRowCellValue(index, "DaThu");
                    //var KhauTru = (decimal?)gvHoaDon.GetRowCellValue(index, "KhauTru");
                    //var ConNo = (decimal?)gvHoaDon.GetRowCellValue(index, "ConNo");
                    //var ThuTruoc = (decimal?)gvHoaDon.GetRowCellValue(index, "ThuTruoc");
                    //var NoCuoi = (decimal?)gvHoaDon.GetRowCellValue(index, "NoCuoi");
                    var _Thang = Convert.ToInt32(itemThang.EditValue);
                    var _Nam = Convert.ToInt32(itemNam.EditValue);

                    var model_lt = new { id = MaKH, thang = _Thang.ToString(), nam = _Nam.ToString() };
                    var param_lt = new Dapper.DynamicParameters();
                    param_lt.AddDynamicParams(model_lt);
                    var result_lt = Library.Class.Connect.QueryConnect.Query<NotifyCongNo>("dv_hoadon_gui_notify_congno", param_lt);

                    foreach (var item in result_lt)
                    {
                        var model_param = new { Building_Code = item.TowerName, Building_MaTN = item.TowerId };
                        var param = new Dapper.DynamicParameters();
                        param.AddDynamicParams(model_param);
                        //param.Add("EmployeeId", employeeId);
                        var a = Library.Class.Connect.QueryConnect.QueryAsyncString<int>("dbo.tbl_building_get_id", Building.AppVime.VimeService.isPersonal == false ? Library.Class.Enum.ConnectString.CONNECT_MYHOME : Library.Properties.Settings.Default.Building_dbConnectionString, param);

                        item.IdNew = a.FirstOrDefault();
                        item.isPersonal = Building.AppVime.VimeService.isPersonal;

                        var ret = Building.AppVime.VimeService.PostH(item, "/Notification/SendCongNo");
                        var result = ret.Replace("\"", "");
                        if (result.Equals("1"))
                        {

                        }
                        else
                        {
                            send = 1;
                        }
                    }
                }
                catch { }
            }
            if (send == 1)
            {
                DialogBox.Error("Gửi không thành công");
            }
            else
            {
                DialogBox.Alert("Gửi thành công");
            }
        }

        public class NotifyCongNo
        {
            /// <summary>
            /// idnew để xác định server sql
            /// </summary>
            public int IdNew { get; set; }
            /// <summary>
            /// ispersonal để xác định link kiểm tra server
            /// </summary>
            public bool? isPersonal { get; set; } //= false;
            /// <summary>
            /// token để xác định gửi notify đến token này
            /// </summary>
            public string Token { get; set; }
            /// <summary>
            /// Phone để xác định đây là ai trong idnew
            /// </summary>
            public string Phone { get; set; }
            /// <summary>
            /// Nội dung gửi notify
            /// </summary>
            public string NoiDung { get; set; }
            /// <summary>
            /// Tiêu đề gửi notify
            /// </summary>
            public string TieuDe { get; set; }
            /// <summary>
            /// ID của table quản lý khách hàng ra vào dự án
            /// </summary>
            public string ID { get; set; }

            public long ResidentId { get; set; }
            public byte TowerId { get; set; }
            public string TowerName { get; set; }
        }

        public void CheckGuiMail(byte? MaTN, int? Month, int? Year, string TemplateNumber)
        {
            var objData = Library.Class.Connect.QueryConnect.QueryData<CheckSendMail>("mailCheckSendMail",
               new
               {
                   MaTN = MaTN,
                   Month = Month,
                   Year = Year,
                   TemplateNumber = TemplateNumber
               }).ToList();

            foreach(var kh in objList)
            {
                var SendObj = objData.FirstOrDefault(_ => _.MaKH == kh.MaKH);
                if(SendObj != null)
                {
                    kh.IsGuiMailCatDichVu = true;
                }
                else
                {
                    kh.IsGuiMailCatDichVu = false;
                }
            }

            gvHoaDon.RefreshData();
        }

        public class CheckSendMail
        {
            public int? MaKH { get; set; }
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                var _MaTN = (byte)itemToaNha.EditValue;
                var _Thang = Convert.ToInt32(itemThang.EditValue);
                var _Nam = Convert.ToInt32(itemNam.EditValue);
                CheckGuiMail(_MaTN, _Thang, _Nam, "ThongBaoCatDichVuERP");
            }
            catch(System.Exception ex) { } //DialogBox.Error(ex.Message);
        }
    }
}
