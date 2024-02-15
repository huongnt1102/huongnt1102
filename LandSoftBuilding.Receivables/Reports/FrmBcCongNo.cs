using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.BandedGrid;
using Library;

namespace LandSoftBuilding.Receivables.Reports
{
    public partial class FrmBcCongNo : XtraForm
    {
        private DataTable _data;
        public FrmBcCongNo()
        {
            InitializeComponent();
        }

        private void FrmBcCongNoDichVu_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);

            lkToaNha.DataSource = Common.TowerList;
            itemToaNha.EditValue = Common.User.MaTN;

            var objKbc = new KyBaoCao();
            foreach (var item in objKbc.Source) cbxKbc.Items.Add(item);
            itemKyBaoCao.EditValue = objKbc.Source[3];
            SetDate(3);
            LoadData();
        }

        private void SetDate(int index)
        {
            var objKbc = new KyBaoCao {Index = index};
            objKbc.SetToDate();
            itemTuNgay.EditValue = objKbc.DateFrom;
            itemDenNgay.EditValue = objKbc.DateTo;
        }

        public class KhachHang
        {
            public int MaKH { get; set; }
            public string TenKH { get; set; }
            public string KyHieu { get; set; }
        }

        public class LoaiDichVu
        {
            public int? MaKH { get; set; }
            public int? MaLDV { get; set; }
            public decimal? SoTien { get; set; }
        }

        private void LoadData()
        {
            // đổi gridview thành bandgridview
            gv.Bands.Clear();
            gv.Columns.Clear();
            try
            {
                _data = new DataTable();
                if (itemTuNgay.EditValue != null && itemDenNgay.EditValue != null)
                {
                    var tuNgay = (DateTime) itemTuNgay.EditValue;
                    var denNgay = (DateTime) itemDenNgay.EditValue;
                    var toaNha = (byte) itemToaNha.EditValue;

                    var db = new MasterDataContext();

                    #region Khai báo list
                    List<KhachHang> objKh = new List<KhachHang>();
                    List<LoaiDichVu> objHdNdkLdv = new List<LoaiDichVu>();
                    List<LoaiDichVu> objHdNdk = new List<LoaiDichVu>();
                    List<LoaiDichVu> objSQ_NDK = new List<LoaiDichVu>();
                    List<LoaiDichVu> objPhatSinh_Ldv = new List<LoaiDichVu>();
                    List<LoaiDichVu> objPhatSinh = new List<LoaiDichVu>();
                    List<LoaiDichVu> objDaThu_Ldv = new List<LoaiDichVu>();
                    List<LoaiDichVu> objDaThu = new List<LoaiDichVu>();
                    List<LoaiDichVu> objKhauTru = new List<LoaiDichVu>();
                    List<LoaiDichVu> objKhauTru_Ldv = new List<LoaiDichVu>();
                    List<LoaiDichVu> objThuTruoc = new List<LoaiDichVu>();
                    List<LoaiDichVu> objThuTruoc_Ldv = new List<LoaiDichVu>();
                    List<LoaiDichVu> objThuTruocTrongKy = new List<LoaiDichVu>();
                    List<LoaiDichVu> objThuTruocTrongKy_Ldv = new List<LoaiDichVu>();
                    //List<LoaiDichVu> objListLdvDk = new List<LoaiDichVu>();
                    List<LoaiDichVu> objListLdvPs = new List<LoaiDichVu>();
                    List<LoaiDichVu> objListLdvDt = new List<LoaiDichVu>();
                    List<LoaiDichVu> objListLdvKt = new List<LoaiDichVu>();
                    List<LoaiDichVu> objListLdvTt = new List<LoaiDichVu>();
                    List<LoaiDichVu> objListLdvTttk = new List<LoaiDichVu>();
                    List<LoaiDichVu> objListLdvDk1 = new List<LoaiDichVu>();
                    List<LoaiDichVu> objListLdvDk2 = new List<LoaiDichVu>();
                    #endregion

                    #region Dữ liệu

                    // khách hàng
                    
                        objKh = (from kh in db.tnKhachHangs
                            where kh.MaTN == toaNha //&& kh.MaKH == 1944
                            orderby kh.KyHieu
                            select new KhachHang
                            {
                                MaKH = kh.MaKH,
                                KyHieu = kh.KyHieu,
                                //kh.MaPhu,
                                TenKH = kh.IsCaNhan == true ? (kh.HoKH + " " + kh.TenKH) : kh.CtyTen,
                                //DienThoai = kh.DienThoaiKH,
                                //kh.EmailKH,
                                //DiaChi = kh.CtyDiaChi,
                                //MaMB = db.dvHoaDons.First(p =>
                                //    p.MaKH == kh.MaKH & SqlMethods.DateDiffDay(p.NgayTT, denNgay) >= 0 & p.IsDuyet == true &
                                //    p.MaMB != null).MaMB,
                            }).ToList();
                   

                    
                    #region Nợ đầu kỳ
                    
                        // nợ đầu kỳ cho dịch vụ
                        objHdNdkLdv = (from hd in db.dvHoaDons
                            where SqlMethods.DateDiffDay(hd.NgayTT, tuNgay) > 0 & hd.IsDuyet == true && hd.MaTN == toaNha
                            group hd by new { hd.MaKH, hd.MaLDV }
                            into ndk
                            select new LoaiDichVu
                            {
                                MaKH = ndk.Key.MaKH,
                                SoTien = ndk.Sum(s => s.PhaiThu),
                                MaLDV = ndk.Key.MaLDV
                            }).ToList();
                    
                        objHdNdk = (from hd in db.dvHoaDons
                            where SqlMethods.DateDiffDay(hd.NgayTT, tuNgay) > 0 & hd.IsDuyet == true && hd.MaTN == toaNha
                            group hd by new { hd.MaKH }
                            into ndk
                            select new LoaiDichVu
                            {
                                MaKH = ndk.Key.MaKH,
                                SoTien = ndk.Sum(s => s.PhaiThu)
                            }).ToList();
                      objSQ_NDK = (from sq in db.SoQuy_ThuChis
                            where SqlMethods.DateDiffDay(sq.NgayPhieu, tuNgay) > 0 && sq.MaTN == toaNha &&
                                  sq.IsPhieuThu == true && sq.MaLoaiPhieu != 24 //&& sq.MaKH == 1944
                            group sq by sq.MaKH
                            into ndk
                            select new LoaiDichVu
                            {
                                MaKH = ndk.Key,
                                SoTien = ndk.Sum(s => s.DaThu + s.KhauTru - s.ThuThua),
                                MaLDV = 0
                            }).ToList();
                    

                    #endregion

                    #region Phát sinh
                    
                        objPhatSinh_Ldv = (from hd in db.dvHoaDons
                            where SqlMethods.DateDiffMonth(hd.NgayTT, tuNgay) == 0 & hd.IsDuyet == true && hd.MaTN == toaNha
                            // && hd.MaKH == 1846
                            group hd by new { hd.MaKH, hd.MaLDV }
                            into ps
                            select new LoaiDichVu
                            {
                                MaKH = ps.Key.MaKH,
                                SoTien = ps.Sum(s => s.PhaiThu).GetValueOrDefault(),
                                MaLDV = ps.Key.MaLDV
                            }).ToList();
                   
                        objPhatSinh = (from hd in db.dvHoaDons
                            where SqlMethods.DateDiffMonth(hd.NgayTT, tuNgay) == 0 & hd.IsDuyet == true && hd.MaTN == toaNha
                            // && hd.MaKH == 1846
                            group hd by new { hd.MaKH }
                            into ps
                            select new LoaiDichVu
                            {
                                MaKH = ps.Key.MaKH,
                                SoTien = ps.Sum(s => s.PhaiThu).GetValueOrDefault(),
                            }).ToList();
                    
                    
                    #endregion

                    #region Đã thu
                    
                        objDaThu_Ldv = (from ct in db.SoQuy_ThuChis
                            join hd in db.dvHoaDons on new { ct.TableName, ct.LinkID } equals new { TableName = "dvHoaDon", LinkID = (long?)hd.ID } into hoaDon
                            from hd in hoaDon.DefaultIfEmpty()
                            where SqlMethods.DateDiffMonth(ct.NgayPhieu, tuNgay) == 0
                                  && ct.IsPhieuThu == true && ct.MaLoaiPhieu != 24 &&
                                  ct.MaTN == toaNha & ct.IsKhauTru == false
                            // && ct.MaKH == 1846
                            group ct by new { ct.MaKH, MaLDV = hd == null ? 0 : hd.MaLDV }
                            into dt
                            select new LoaiDichVu
                            {
                                MaKH = dt.Key.MaKH,
                                SoTien = dt.Sum(s => s.DaThu).GetValueOrDefault(),
                                MaLDV = dt.Key.MaLDV
                            }).ToList();
                    
                        objDaThu = (from ct in db.SoQuy_ThuChis
                            where SqlMethods.DateDiffMonth(ct.NgayPhieu, tuNgay) == 0
                                  && ct.IsPhieuThu == true && ct.MaLoaiPhieu != 24 &&
                                  ct.MaTN == toaNha & ct.IsKhauTru == false
                            // && ct.MaKH == 1846
                            group ct by ct.MaKH
                            into dt
                            select new LoaiDichVu
                            {
                                MaKH = dt.Key,
                                SoTien = dt.Sum(s => s.DaThu).GetValueOrDefault()
                            }).ToList();
                    
                    
                    #endregion

                    #region Khấu trừ
                    
                        objKhauTru = (from ct in db.SoQuy_ThuChis
                            where SqlMethods.DateDiffMonth(ct.NgayPhieu, tuNgay) == 0
                                  //ct.NgayPhieu.Value.Month == _TuNgay.Month
                                  && ct.MaTN == toaNha && ct.IsPhieuThu == true & ct.IsKhauTru == true
                            // && ct.MaKH == 1846
                            group ct by ct.MaKH
                            into kt
                            select new LoaiDichVu
                            {
                                MaKH = kt.Key,
                                SoTien = kt.Sum(s => s.KhauTru + s.DaThu).GetValueOrDefault()
                            }).ToList();
                    
                        objKhauTru_Ldv = (from ct in db.SoQuy_ThuChis
                            join hd in db.dvHoaDons on new { ct.TableName, ct.LinkID } equals new { TableName = "dvHoaDon", LinkID = (long?)hd.ID } into hoaDon
                            from hd in hoaDon.DefaultIfEmpty()
                            where SqlMethods.DateDiffMonth(ct.NgayPhieu, tuNgay) == 0
                                  && ct.MaTN == toaNha && ct.IsPhieuThu == true & ct.IsKhauTru == true
                            group ct by new { ct.MaKH, MaLDV = hd == null ? 0 : hd.MaLDV }
                            into kt
                            select new LoaiDichVu
                            {
                                MaKH = kt.Key.MaKH,
                                SoTien = kt.Sum(s => s.KhauTru + s.DaThu).GetValueOrDefault(),
                                MaLDV = kt.Key.MaLDV
                            }).ToList();
                    
                    
                    #endregion

                    #region Thu Trước
                    
                        objThuTruoc = (from sq in db.SoQuy_ThuChis
                            where SqlMethods.DateDiffDay(sq.NgayPhieu, DateTime.Now) >= 0 && sq.MaTN == toaNha
                                                                                          && sq.IsPhieuThu == true &&
                                                                                          sq.MaLoaiPhieu !=
                                                                                          24 //&& sq.MaKH == 1846
                            // where SqlMethods.DateDiffDay(sq.NgayPhieu, _DenNgay) >= 0 && sq.MaTN == _MaTN 

                            group sq by sq.MaKH
                            into tt
                            select new LoaiDichVu
                            {
                                MaKH = tt.Key,
                                SoTien = tt.Sum(s => s.ThuThua - s.KhauTru)
                            }).ToList();
                    
                        objThuTruoc_Ldv = (from sq in db.SoQuy_ThuChis
                            join hd in db.dvHoaDons on new { sq.TableName, sq.LinkID } equals new { TableName = "dvHoaDon", LinkID = (long?)hd.ID } into hoaDon
                            from hd in hoaDon.DefaultIfEmpty()
                            where SqlMethods.DateDiffDay(sq.NgayPhieu, DateTime.Now) >= 0 && sq.MaTN == toaNha
                                                                                          && sq.IsPhieuThu == true &&
                                                                                          sq.MaLoaiPhieu !=
                                                                                          24 //&& sq.MaKH == 1846
                            // where SqlMethods.DateDiffDay(sq.NgayPhieu, _DenNgay) >= 0 && sq.MaTN == _MaTN 

                            group sq by new { sq.MaKH, MaLDV = hd == null ? 0 : hd.MaLDV }
                            into tt
                            select new LoaiDichVu
                            {
                                MaKH = tt.Key.MaKH,
                                SoTien = tt.Sum(s => s.ThuThua - s.KhauTru),
                                MaLDV = tt.Key.MaLDV
                            }).ToList();
                    
                        objThuTruocTrongKy = (from sq in db.SoQuy_ThuChis
                            where SqlMethods.DateDiffMonth(sq.NgayPhieu, tuNgay) == 0 && sq.MaTN == toaNha
                                                                                      && sq.IsPhieuThu == true &&
                                                                                      sq.MaLoaiPhieu !=
                                                                                      24 //&& sq.MaKH == 1846
                            group sq by sq.MaKH
                            into tt
                            select new LoaiDichVu
                            {
                                MaKH = tt.Key,
                                SoTien = tt.Sum(s => s.ThuThua)
                            }).ToList();
                    
                        objThuTruocTrongKy_Ldv = (from sq in db.SoQuy_ThuChis
                            join hd in db.dvHoaDons on new { sq.TableName, sq.LinkID } equals new { TableName = "dvHoaDon", LinkID = (long?)hd.ID } into hoaDon
                            from hd in hoaDon.DefaultIfEmpty()
                            where SqlMethods.DateDiffMonth(sq.NgayPhieu, tuNgay) == 0 && sq.MaTN == toaNha
                                                                                      && sq.IsPhieuThu == true &&
                                                                                      sq.MaLoaiPhieu != 24
                            group sq by new { sq.MaKH, MaLDV = hd == null ? 0 : hd.MaLDV }
                            into tt
                            select new LoaiDichVu
                            {
                                MaKH = tt.Key.MaKH,
                                SoTien = tt.Sum(s => s.ThuThua),
                                MaLDV = tt.Key.MaLDV
                            }).ToList();
                    

                    #endregion

                    #region List loại dịch vụ
                   
                        objListLdvDk1 = (from kh in objKh
                                         join ndk in objHdNdkLdv on kh.MaKH equals ndk.MaKH into nodk
                                         from ndk in nodk.DefaultIfEmpty()

                                         select new
                                         {
                                             MaKH = kh.MaKH,
                                             SoTien = (ndk == null ? 0 : ndk.SoTien.GetValueOrDefault()),
                                             MaLDV = ndk == null ? 0 : ndk.MaLDV
                                         }).Select(p => new LoaiDichVu
                                         {
                                             MaKH = p.MaKH,
                                             //SoTien = p.SoTien < 0 ? 0 : p.SoTien,
                                             SoTien = p.SoTien,
                                             MaLDV = p.MaLDV
                                         }).ToList();
                    
                        objListLdvDk2 = (from kh in objKh
                                         join sqdk in objSQ_NDK on kh.MaKH equals sqdk.MaKH into soquydk
                                         from sqdk in soquydk.DefaultIfEmpty()

                                         select new
                                         {
                                             kh.MaKH,
                                             SoTien = -
                                                      (sqdk == null ? 0 : sqdk.SoTien.GetValueOrDefault()),
                                             MaLdv = sqdk == null ? 0 : sqdk.MaLDV
                                         }).Select(p => new LoaiDichVu
                                         {
                                             MaKH = p.MaKH,
                                             //SoTien = p.SoTien < 0 ? 0 : p.SoTien,
                                             SoTien = p.SoTien,
                                             MaLDV = p.MaLdv
                                         }).ToList();
                    

                    var objListLdvDk = objListLdvDk1.Concat(objListLdvDk2);

                   
                        objListLdvPs = (from kh in objKh
                            join ps in objPhatSinh_Ldv on kh.MaKH equals ps.MaKH into psinh
                            from ps in psinh.DefaultIfEmpty()
                            select new
                            {
                                kh.MaKH,
                                SoTien = ps == null ? 0 : ps.SoTien.GetValueOrDefault(),
                                MaLdv = ps == null ? 0 : ps.MaLDV
                            }).Select(p => new LoaiDichVu
                        {
                            MaKH = p.MaKH,
                            SoTien = p.SoTien,
                            MaLDV = p.MaLdv
                        }).ToList();

                      objListLdvDt = (from kh in objKh
                            join ps in objDaThu_Ldv on kh.MaKH equals ps.MaKH into psinh
                            from ps in psinh.DefaultIfEmpty()
                            select new
                            {
                                kh.MaKH,
                                SoTien = ps == null ? 0 : ps.SoTien.GetValueOrDefault(),
                                MaLdv = ps == null ? 0 : ps.MaLDV
                            }).Select(p => new LoaiDichVu
                        {
                            MaKH = p.MaKH,
                            SoTien = p.SoTien,
                            MaLDV = p.MaLdv
                        }).ToList();
                    
                        objListLdvKt =
                            (from p in objKh
                                join k in objKhauTru_Ldv on p.MaKH equals k.MaKH into khauTru
                                from k in khauTru.DefaultIfEmpty()
                                select new {p.MaKH, SoTien = k == null ? 0 : k.SoTien.GetValueOrDefault(), MaLdv = k == null ? 0 : k.MaLDV})
                            .Select(_ => new LoaiDichVu {MaKH= _.MaKH,SoTien= _.SoTien,MaLDV= _.MaLdv}).ToList();
                    
                        objListLdvTt = (from p in objKh
                                join k in objThuTruoc_Ldv on p.MaKH equals k.MaKH into thuTruoc
                                from k in thuTruoc.DefaultIfEmpty()
                                select new
                                {
                                    p.MaKH, SoTien = k == null ? 0 : k.SoTien.GetValueOrDefault(),
                                    MaLdv = k == null ? 0 : k.MaLDV
                                })
                            .Select(_ => new LoaiDichVu {MaKH= _.MaKH,SoTien= _.SoTien,MaLDV= _.MaLdv}).ToList();
                    
                        objListLdvTttk =
                            (from p in objKh
                                join k in objThuTruocTrongKy_Ldv on p.MaKH equals k.MaKH into thuTruocTrongKy
                                from k in thuTruocTrongKy.DefaultIfEmpty()
                                select new
                                {
                                    p.MaKH, SoTien = k == null ? 0 : k.SoTien.GetValueOrDefault(),
                                    MaLdv = k == null ? 0 : k.MaLDV
                                }).Select(_ => new LoaiDichVu {MaKH =_.MaKH,SoTien= _.SoTien,MaLDV= _.MaLdv}).ToList();
                   

                    var loaiDichVus = objListLdvDk as LoaiDichVu[] ?? objListLdvDk.ToArray();
                    var objListLdvNc = loaiDichVus.Concat(objListLdvPs).Concat(objListLdvDt).Concat(objListLdvKt)
                        .Concat(objListLdvTt).ToList();

                    #endregion

                    #region list dịch vụ

                    var objList = (from kh in objKh
                                   join ndk in objHdNdk on kh.MaKH equals ndk.MaKH into nodk
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
                                   join tttk in objThuTruocTrongKy on kh.MaKH equals tttk.MaKH into tttrongky
                                   from tttk in tttrongky.DefaultIfEmpty()
                                   select new
                                   {
                                       kh.MaKH,
                                       kh.KyHieu,
                                       //kh.MaPhu,
                                       TenKH = kh.TenKH,
                                       //DienThoai = kh.DienThoai,
                                       //kh.EmailKH,
                                       //DiaChi = kh.DiaChi,
                                       //kh.MaMB,
                                       NoDauKy = (ndk == null ? 0 : ndk.SoTien.GetValueOrDefault()) -
                                                 (sqdk == null ? 0 : sqdk.SoTien.GetValueOrDefault()),
                                       PhatSinh = ps == null ? 0 : ps.SoTien,
                                       DaThu = dt == null ? 0 : dt.SoTien,
                                       KhauTru = kt == null ? 0 : kt.SoTien,
                                       ThuTruoc = tt == null ? 0 : tt.SoTien,
                                       ThuTruocTK = tttk == null ? 0 : tttk.SoTien,
                                   }).Select(p => new
                                   {
                                       ThuTruoc = p.ThuTruoc,
                                       NoDauKy = p.NoDauKy < 0 ? 0 : p.NoDauKy,
                                       PhatSinh = p.PhatSinh,
                                       KhauTru = p.KhauTru,
                                       DaThu = p.DaThu,
                                       ConNo = ((p.NoDauKy < 0 ? 0 : p.NoDauKy) + p.PhatSinh - (p.DaThu + p.KhauTru - p.ThuTruocTK)) <
                                               0
                                           ? 0
                                           : ((p.NoDauKy < 0 ? 0 : p.NoDauKy) + p.PhatSinh - (p.DaThu + p.KhauTru - p.ThuTruocTK)),
                                       MaKH = p.MaKH,
                                       KyHieu = p.KyHieu,
                                       //MaPhu = p.MaPhu,
                                       TenKH = p.TenKH,
                                       //DienThoai = p.DienThoai,
                                       //EmailKH = p.EmailKH,
                                       //DiaChi = p.DiaChi,
                                       //MaMB = p.MaMB,
                                       NoCuoi =
                                           (((p.NoDauKy < 0 ? 0 : p.NoDauKy) + p.PhatSinh - (p.DaThu + p.KhauTru - p.ThuTruocTK)) < 0
                                               ? 0
                                               : ((p.NoDauKy < 0 ? 0 : p.NoDauKy) + p.PhatSinh -
                                                  (p.DaThu + p.KhauTru - p.ThuTruocTK))) - p.ThuTruoc,
                                   }).ToList();
                    #endregion
                    
                    //var obj = (from p in objList
                    //    group new {p} by new {p.MaKH, p.TenKH}
                    //    into g
                    //    select new {NoDauKy = g.Sum(_ => _.p.NoDauKy),MaKH = g.Key.MaKH, TenKH = g.Key.TenKH, PhatSinh = g.Sum(_=>_.p.PhatSinh), DaThu = g.Sum(_=>_.p.DaThu)}).ToList();

                    #endregion

                    #region Đổ cột cố định

                    // data
                    _data.Columns.Add("MaKH");
                    _data.Columns.Add("TenKH");

                    // band
                    var band = new GridBand();
                    gv.Bands.Add(band);
                    gv.Columns.AddField("Mã khách hàng");
                    var colMaKh = new BandedGridColumn();
                    colMaKh.Name = "colMaKh";
                    colMaKh.Caption = @"Mã khách hàng";
                    colMaKh.FieldName = "MaKH";
                    colMaKh.Visible = true;
                    colMaKh.VisibleIndex = 1;
                    colMaKh.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                    colMaKh.OwnerBand = band;

                    gv.Columns.AddField("Tên khách hàng");
                    var colTenKh = new BandedGridColumn();
                    colTenKh.Name = "colTenKh";
                    colTenKh.Caption = @"Tên khách hàng";
                    colTenKh.FieldName = "TenKH";
                    colTenKh.Visible = true;
                    colTenKh.VisibleIndex = 2;
                    colTenKh.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                    colTenKh.OwnerBand = band;

                    band.Visible = true;
                    band.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;


                    #endregion

                    #region Đổ cột tự động

                    #region đầu kỳ
                    var bandDauKy = new GridBand();
                    bandDauKy.Name = "DauKy";
                    bandDauKy.Caption = "Đầu kỳ";
                    bandDauKy.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                    gv.Bands.Add(bandDauKy);

                    var objLdvDk = (from p in loaiDichVus
                                    join ldv in db.dvLoaiDichVus on p.MaLDV equals ldv.ID into loaiDichVu
                                    from ldv in loaiDichVu.DefaultIfEmpty()
                                    group p by new
                                    {
                                        p.MaLDV,
                                        TenLDV = p.MaLDV == 0 ? "Phí khác" : ldv.TenHienThi
                                    }
                                        into g
                                        select new { g.Key.MaLDV, SoTien = g.Sum(_ => _.SoTien), g.Key.TenLDV }).ToList();
                    var i = 2;
                    foreach (var item in objLdvDk)
                    {
                        if (item.SoTien != 0)
                        {
                            i++;
                            _data.Columns.Add("DauKy" + item.MaLDV.ToString());
                            var c = new BandedGridColumn();
                            c.Name = "DauKy" + item.MaLDV.ToString();
                            c.Caption = item.TenLDV;
                            c.FieldName = "DauKy" + item.MaLDV.ToString();
                            c.Visible = true;
                            c.VisibleIndex = i;
                            c.OwnerBand = bandDauKy;

                        }

                    }

                    // tổng đầu kỳ
                    //this.bandedGridColumn1.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
                    //this.bandedGridColumn1.AppearanceCell.Options.UseFont = true;
                    //this.bandedGridColumn1.AppearanceCell.Options.UseTextOptions = true;
                    //this.bandedGridColumn1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                    //this.bandedGridColumn1.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
                    //this.bandedGridColumn1.AppearanceHeader.Options.UseFont = true;
                    //this.bandedGridColumn1.Caption = "bandedGridColumn1";
                    //this.bandedGridColumn1.DisplayFormat.FormatString = "n0";
                    //this.bandedGridColumn1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    //this.bandedGridColumn1.FieldName = "abc";
                    //this.bandedGridColumn1.Name = "bandedGridColumn1";

                    var bandDauKyTong = new GridBand();
                    bandDauKyTong.Name = "DauKyTong";
                    bandDauKyTong.Caption = " ";
                    bandDauKyTong.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                    gv.Bands.Add(bandDauKyTong);
                    _data.Columns.Add("DauKyTong");
                    var cTotalDauKy = new BandedGridColumn();

                    cTotalDauKy.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
                    cTotalDauKy.AppearanceCell.Options.UseFont = true;
                    cTotalDauKy.AppearanceCell.Options.UseTextOptions = true;
                    cTotalDauKy.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                    cTotalDauKy.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
                    cTotalDauKy.AppearanceHeader.Options.UseFont = true;
                    cTotalDauKy.DisplayFormat.FormatString = "{0:#,0.##}";
                    cTotalDauKy.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;

                    cTotalDauKy.Name = "DauKyTong";
                    cTotalDauKy.Caption = "Đầu kỳ (Tổng)";
                    cTotalDauKy.FieldName = "DauKyTong";
                    cTotalDauKy.Visible = true;
                    cTotalDauKy.VisibleIndex = i + 1;
                    cTotalDauKy.OwnerBand = bandDauKyTong;

                    bandDauKy.Visible = true;
                    #endregion

                    #region Phát sinh
                    // Phát sinh
                    var bandPhatSinh = new GridBand();
                    bandPhatSinh.Name = "PhatSinh";
                    bandPhatSinh.Caption = "Phát sinh";
                    bandPhatSinh.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                    gv.Bands.Add(bandPhatSinh);

                    var objPs = (from p in objListLdvPs
                                 join ldv in db.dvLoaiDichVus on p.MaLDV equals ldv.ID into loaiDichVu
                                 from ldv in loaiDichVu.DefaultIfEmpty()
                                 group p by new { p.MaLDV, TenLDV = p.MaLDV == 0 ? "Phí khác" : ldv.TenHienThi }
                                     into g
                                     select new { g.Key.MaLDV, SoTien = g.Sum(_ => _.SoTien), g.Key.TenLDV }).ToList();
                    foreach (var item in objPs)
                    {
                        if (item.SoTien != 0)
                        {
                            i++;
                            _data.Columns.Add("PhatSinh" + item.MaLDV.ToString());
                            var c = new BandedGridColumn();
                            c.Name = "PhatSinh" + item.MaLDV.ToString();
                            c.Caption = item.TenLDV;
                            c.FieldName = "PhatSinh" + item.MaLDV.ToString();
                            c.Visible = true;
                            c.VisibleIndex = i;
                            c.OwnerBand = bandPhatSinh;
                        }
                    }

                    var bandPhatSinhTong = new GridBand();
                    bandPhatSinhTong.Name = "PhatSinhTong";
                    bandPhatSinhTong.Caption = " ";
                    bandPhatSinhTong.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                    gv.Bands.Add(bandPhatSinhTong);
                    _data.Columns.Add("PhatSinhTong");
                    var cPhatSinhTong = new BandedGridColumn();

                    cPhatSinhTong.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
                    cPhatSinhTong.AppearanceCell.Options.UseFont = true;
                    cPhatSinhTong.AppearanceCell.Options.UseTextOptions = true;
                    cPhatSinhTong.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                    cPhatSinhTong.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
                    cPhatSinhTong.AppearanceHeader.Options.UseFont = true;

                    cPhatSinhTong.Name = "PhatSinhTong";
                    cPhatSinhTong.DisplayFormat.FormatString = "n0";
                    cPhatSinhTong.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    cPhatSinhTong.Caption = "Phát sinh (Tổng)";
                    cPhatSinhTong.FieldName = "PhatSinhTong";
                    cPhatSinhTong.Visible = true;
                    cPhatSinhTong.VisibleIndex = i + 1;
                    cPhatSinhTong.OwnerBand = bandPhatSinhTong;
                    bandPhatSinh.Visible = true;
                    #endregion

                    #region Đã thu
                    // đã thu
                    var bandDaThu = new GridBand();
                    bandDaThu.Name = "DaThu";
                    bandDaThu.Caption = "Đã thu";
                    bandDaThu.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                    gv.Bands.Add(bandDaThu);

                    var objDt = (from p in objListLdvDt
                                 join ldv in db.dvLoaiDichVus on p.MaLDV equals ldv.ID into loaiDichVu
                                 from ldv in loaiDichVu.DefaultIfEmpty()
                                 group p by new { p.MaLDV, TenLDV = p.MaLDV == 0 ? "Thu trước dịch vụ" : ldv.TenHienThi }
                                     into g
                                     select new { g.Key.MaLDV, SoTien = g.Sum(_ => _.SoTien), g.Key.TenLDV }).ToList();
                    foreach (var item in objDt)
                    {
                        if (item.SoTien != 0)
                        {
                            i++;
                            _data.Columns.Add("DaThu" + item.MaLDV.ToString());
                            var c = new BandedGridColumn();
                            c.Name = "DaThu" + item.MaLDV.ToString();
                            c.Caption = item.TenLDV.ToString();
                            c.FieldName = "DaThu" + item.MaLDV.ToString();
                            c.Visible = true;

                            c.VisibleIndex = i;
                            c.OwnerBand = bandDaThu;
                        }
                    }

                    var bandDaThuTong = new GridBand();
                    bandDaThuTong.Name = "DaThuTong";
                    bandDaThuTong.Caption = " ";
                    bandDaThuTong.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                    gv.Bands.Add(bandDaThuTong);
                    _data.Columns.Add("DaThuTong");
                    var cDaThuTong = new BandedGridColumn();

                    cDaThuTong.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
                    cDaThuTong.AppearanceCell.Options.UseFont = true;
                    cDaThuTong.AppearanceCell.Options.UseTextOptions = true;
                    cDaThuTong.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                    cDaThuTong.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
                    cDaThuTong.AppearanceHeader.Options.UseFont = true;

                    cDaThuTong.Name = "DaThuTong";
                    cDaThuTong.DisplayFormat.FormatString = "n0";
                    cDaThuTong.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    cDaThuTong.Caption = "Đã thu (Tổng)";
                    cDaThuTong.FieldName = "DaThuTong";
                    cDaThuTong.Visible = true;
                    cDaThuTong.VisibleIndex = i + 1;
                    cDaThuTong.OwnerBand = bandDaThuTong;
                    bandDaThu.Visible = true;
                    #endregion

                    #region Khấu trừ
                    // khấu trừ
                    var bandKhauTru = new GridBand();
                    bandKhauTru.Name = "KhauTru";
                    bandKhauTru.Caption = "Khấu trừ";
                    bandKhauTru.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                    gv.Bands.Add(bandKhauTru);

                    var objKt = (from p in objListLdvKt
                                 join ldv in db.dvLoaiDichVus on p.MaLDV equals ldv.ID into loaiDichVu
                                 from ldv in loaiDichVu.DefaultIfEmpty()
                                 group p by new { p.MaLDV, TenLDV = p.MaLDV == 0 ? "Phí khác" : ldv.TenHienThi }
                                     into g
                                     select new { g.Key.MaLDV, SoTien = g.Sum(_ => _.SoTien), g.Key.TenLDV }).ToList();
                    foreach (var item in objKt)
                    {
                        if (item.SoTien != 0)
                        {
                            i++;
                            _data.Columns.Add("KhauTru" + item.MaLDV.ToString());
                            var c = new BandedGridColumn();
                            c.Name = "KhauTru" + item.MaLDV.ToString();
                            c.Caption = item.TenLDV.ToString();
                            c.FieldName = "KhauTru" + item.MaLDV.ToString();
                            c.Visible = true;
                            c.VisibleIndex = i;
                            c.OwnerBand = bandKhauTru;
                        }
                    }

                    var bandKhauTruTong = new GridBand();
                    bandKhauTruTong.Name = "KhauTruTong";
                    bandKhauTruTong.Caption = " ";
                    bandKhauTruTong.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                    gv.Bands.Add(bandKhauTruTong);
                    _data.Columns.Add("KhauTruTong");
                    var cKhauTruTong = new BandedGridColumn();

                    cKhauTruTong.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
                    cKhauTruTong.AppearanceCell.Options.UseFont = true;
                    cKhauTruTong.AppearanceCell.Options.UseTextOptions = true;
                    cKhauTruTong.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                    cKhauTruTong.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
                    cKhauTruTong.AppearanceHeader.Options.UseFont = true;

                    cKhauTruTong.Name = "KhauTruTong";
                    cKhauTruTong.DisplayFormat.FormatString = "n0";
                    cKhauTruTong.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    cKhauTruTong.Caption = "Khấu trừ (Tổng)";
                    cKhauTruTong.FieldName = "KhauTruTong";
                    cKhauTruTong.Visible = true;
                    cKhauTruTong.VisibleIndex = i + 1;
                    cKhauTruTong.OwnerBand = bandKhauTruTong;
                    bandKhauTru.Visible = true;
                    #endregion

                    #region Còn nợ
                    // còn nợ
                    var bandConNoTong = new GridBand();
                    bandConNoTong.Name = "ConNoTong";
                    bandConNoTong.Caption = "Còn nợ";
                    bandConNoTong.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                    gv.Bands.Add(bandConNoTong);
                    _data.Columns.Add("ConNoTong");
                    var cConNoTong = new BandedGridColumn();

                    cConNoTong.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
                    cConNoTong.AppearanceCell.Options.UseFont = true;
                    cConNoTong.AppearanceCell.Options.UseTextOptions = true;
                    cConNoTong.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                    cConNoTong.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
                    cConNoTong.AppearanceHeader.Options.UseFont = true;

                    cConNoTong.Name = "ConNoTong";
                    cConNoTong.DisplayFormat.FormatString = "n0";
                    cConNoTong.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    cConNoTong.Caption = "Còn nợ";
                    cConNoTong.FieldName = "ConNoTong";
                    cConNoTong.Visible = true;
                    cConNoTong.VisibleIndex = i + 1;
                    cConNoTong.OwnerBand = bandConNoTong;
                    bandConNoTong.Visible = true;
                    #endregion

                    #region Thu trước
                    // thu trước
                    var bandThuTruoc = new GridBand();
                    bandThuTruoc.Name = "ThuTruoc";
                    bandThuTruoc.Caption = "Thu trước";
                    bandThuTruoc.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                    gv.Bands.Add(bandThuTruoc);

                    var objTt = (from p in objListLdvTt
                                 join ldv in db.dvLoaiDichVus on p.MaLDV equals ldv.ID into loaiDichVu
                                 from ldv in loaiDichVu.DefaultIfEmpty()
                                 group p by new { p.MaLDV, TenLDV = p.MaLDV == 0 ? "Thu trước dịch vụ" : ldv.TenHienThi }
                                     into g
                                     select new { g.Key.MaLDV, SoTien = g.Sum(_ => _.SoTien), g.Key.TenLDV }).ToList();
                    foreach (var item in objTt)
                    {
                        if (item.SoTien != 0)
                        {
                            i++;
                            _data.Columns.Add("ThuTruoc" + item.MaLDV.ToString());
                            var c = new BandedGridColumn();
                            c.Name = "ThuTruoc" + item.MaLDV.ToString();
                            c.Caption = item.TenLDV.ToString();
                            c.FieldName = "ThuTruoc" + item.MaLDV.ToString();
                            c.Visible = true;
                            c.VisibleIndex = i;
                            c.OwnerBand = bandThuTruoc;
                        }
                    }

                    var bandThuTruocTong = new GridBand();
                    bandThuTruocTong.Name = "ThuTruocTong";
                    bandThuTruocTong.Caption = " ";
                    bandThuTruocTong.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                    gv.Bands.Add(bandThuTruocTong);
                    _data.Columns.Add("ThuTruocTong");
                    var cThuTruocTong = new BandedGridColumn();

                    cThuTruocTong.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
                    cThuTruocTong.AppearanceCell.Options.UseFont = true;
                    cThuTruocTong.AppearanceCell.Options.UseTextOptions = true;
                    cThuTruocTong.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                    cThuTruocTong.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
                    cThuTruocTong.AppearanceHeader.Options.UseFont = true;

                    cThuTruocTong.Name = "ThuTruocTong";
                    cThuTruocTong.DisplayFormat.FormatString = "n0";
                    cThuTruocTong.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    cThuTruocTong.Caption = "Thu trước (Tổng)";
                    cThuTruocTong.FieldName = "ThuTruocTong";
                    cThuTruocTong.Visible = true;
                    cThuTruocTong.VisibleIndex = i + 1;
                    cThuTruocTong.OwnerBand = bandThuTruocTong;
                    bandThuTruoc.Visible = true;
                    #endregion

                    #region Nợ cuối
                    // Nợ cuối
                    var bandNoCuoi = new GridBand();
                    bandNoCuoi.Name = "NoCuoi";
                    bandNoCuoi.Caption = "Nợ cuối";
                    bandNoCuoi.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                    gv.Bands.Add(bandNoCuoi);

                    var objNc = (from p in objListLdvNc
                                 join ldv in db.dvLoaiDichVus on p.MaLDV equals ldv.ID into lDichVu
                                 from ldv in lDichVu.DefaultIfEmpty()
                                 group p by new { p.MaLDV, TenLDV = p.MaLDV == 0 ? "Thu trước dịch vụ" : ldv.TenHienThi }
                                     into g
                                     select new { g.Key.MaLDV, SoTien = g.Sum(_ => _.SoTien), g.Key.TenLDV }).ToList();
                    foreach (var item in objNc)
                    {
                        if (item.SoTien != 0)
                        {
                            i++;
                            _data.Columns.Add("NoCuoi" + item.MaLDV.ToString());
                            var c = new BandedGridColumn();
                            c.Name = "NoCuoi" + item.MaLDV.ToString();
                            c.Caption = item.TenLDV.ToString();
                            c.FieldName = "NoCuoi" + item.MaLDV.ToString();
                            c.Visible = true;
                            c.VisibleIndex = i;
                            c.OwnerBand = bandNoCuoi;
                        }
                    }

                    var bandNoCuoiTong = new GridBand();
                    bandNoCuoiTong.Name = "NoCuoiTong";
                    bandNoCuoiTong.Caption = " ";
                    bandNoCuoiTong.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                    gv.Bands.Add(bandNoCuoiTong);
                    _data.Columns.Add("NoCuoiTong");
                    var cNoCuoiTong = new BandedGridColumn();

                    cNoCuoiTong.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
                    cNoCuoiTong.AppearanceCell.Options.UseFont = true;
                    cNoCuoiTong.AppearanceCell.Options.UseTextOptions = true;
                    cNoCuoiTong.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                    cNoCuoiTong.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
                    cNoCuoiTong.AppearanceHeader.Options.UseFont = true;

                    cNoCuoiTong.Name = "NoCuoiTong";
                    cNoCuoiTong.DisplayFormat.FormatString = "n0";
                    cNoCuoiTong.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    cNoCuoiTong.Caption = "Tổng nợ cuối";
                    cNoCuoiTong.FieldName = "NoCuoiTong";
                    cNoCuoiTong.Visible = true;
                    cNoCuoiTong.VisibleIndex = i + 1;
                    cNoCuoiTong.OwnerBand = bandNoCuoiTong;
                    bandNoCuoi.Visible = true;
                    #endregion

                    #endregion

                    #region Đổ dữ liệu

                    foreach (var item in objList)
                    {
                        var r = _data.NewRow();
                        r["MaKH"] = item.KyHieu;
                        r["TenKH"] = item.TenKH;


                        foreach (var it in objLdvDk)
                        {
                            if (it.SoTien != 0)
                            {
                                var soTien = loaiDichVus
                                    .Where(_ => _.MaLDV == it.MaLDV && _.MaKH == item.MaKH).Sum(_ => _.SoTien);
                                r["DauKy" + it.MaLDV.ToString()] = string.Format("{0:#,0.##}", soTien);
                            }

                        }

                        r["DauKyTong"] = string.Format("{0:#,0.##}", item.NoDauKy);
                        //r["DauKyTong"] = item.NoDauKy;

                        foreach (var it in objPs)
                        {
                            if (it.SoTien != 0)
                            {
                                //string.Format("{0:#,0.##}", soTien);
                                var soTien = objListLdvPs
                                    .Where(_ => _.MaLDV == it.MaLDV && _.MaKH == item.MaKH).Sum(_ => _.SoTien);
                                r["PhatSinh" + it.MaLDV] = string.Format("{0:#,0.##}", soTien);
                            }
                        }

                        r["PhatSinhTong"] = string.Format("{0:#,0.##}", item.PhatSinh);

                        foreach (var it in objDt)
                        {
                            if (it.SoTien != 0)
                            {
                                //string.Format("{0:#,0.##}", soTien);
                                var soTien = objListLdvDt
                                    .Where(_ => _.MaLDV == it.MaLDV && _.MaKH == item.MaKH).Sum(_ => _.SoTien);
                                r["DaThu" + it.MaLDV] = string.Format("{0:#,0.##}", soTien);
                            }
                        }

                        r["DaThuTong"] = string.Format("{0:#,0.##}", item.DaThu);

                        foreach (var it in objKt)
                        {
                            if (it.SoTien != 0)
                            {
                                //string.Format("{0:#,0.##}", soTien);
                                var soTien = objListLdvKt
                                    .Where(_ => _.MaLDV == it.MaLDV && _.MaKH == item.MaKH).Sum(_ => _.SoTien);
                                r["KhauTru" + it.MaLDV] = string.Format("{0:#,0.##}", soTien);
                            }
                        }

                        r["KhauTruTong"] = string.Format("{0:#,0.##}", item.KhauTru);
                        r["ConNoTong"] = string.Format("{0:#,0.##}", item.ConNo);

                        foreach (var it in objTt)
                        {
                            if (it.SoTien != 0)
                            {
                                //string.Format("{0:#,0.##}", soTien);
                                var soTien = objListLdvTt
                                    .Where(_ => _.MaLDV == it.MaLDV && _.MaKH == item.MaKH).Sum(_ => _.SoTien);
                                r["ThuTruoc" + it.MaLDV] = string.Format("{0:#,0.##}", soTien);
                            }
                        }

                        r["ThuTruocTong"] = string.Format("{0:#,0.##}", item.ThuTruoc);

                        // NoCuoi = (((p.NoDauKy < 0 ? 0 : p.NoDauKy) + p.PhatSinh - (p.DaThu + p.KhauTru - p.ThuTruocTK)) < 0 ? 0 : ((p.NoDauKy < 0 ? 0 : p.NoDauKy) + p.PhatSinh - (p.DaThu + p.KhauTru - p.ThuTruocTK))) - p.ThuTruoc,
                        foreach (var it in objNc)
                        {
                            if (it.SoTien != 0)
                            {
                                //string.Format("{0:#,0.##}", soTien);
                                var dk = loaiDichVus
                                    .Where(_ => _.MaLDV == it.MaLDV && _.MaKH == item.MaKH).Sum(_ => _.SoTien);
                                var ps = objListLdvPs
                                    .Where(_ => _.MaLDV == it.MaLDV && _.MaKH == item.MaKH).Sum(_ => _.SoTien);
                                var dt = objListLdvDt
                                    .Where(_ => _.MaLDV == it.MaLDV && _.MaKH == item.MaKH).Sum(_ => _.SoTien);
                                var kt = objListLdvKt
                                    .Where(_ => _.MaLDV == it.MaLDV && _.MaKH == item.MaKH).Sum(_ => _.SoTien);
                                var tt = objListLdvTt
                                    .Where(_ => _.MaLDV == it.MaLDV && _.MaKH == item.MaKH).Sum(_ => _.SoTien);
                                var ttk = objListLdvTttk.Where(_ => _.MaLDV == it.MaLDV && _.MaKH == item.MaKH)
                                    .Sum(_ => _.SoTien);
                                var soTien = (((dk < 0 ? 0 : dk) + ps - (dt + kt - ttk)) < 0
                                                 ? 0
                                                 : ((dk < 0 ? 0 : dk) + ps - (dt + kt - ttk))) - tt;
                                r["NoCuoi" + it.MaLDV] = string.Format("{0:#,0.##}", soTien);
                            }
                        }

                        r["NoCuoiTong"] = string.Format("{0:#,0.##}", item.NoCuoi);

                        _data.Rows.Add(r);
                    }

                    cTotalDauKy.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[]
                    {
                        new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum,
                            "DauKyTong", "{0:n0}")
                    });

                    cPhatSinhTong.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[]
                    {
                        new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum,
                            "PhatSinhTong", "{0:n0}")
                    });
                    cDaThuTong.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[]
                    {
                        new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum,
                            "DaThuTong", "{0:n0}")
                    });
                    cKhauTruTong.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[]
                    {
                        new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum,
                            "KhauTruTong", "{0:n0}")
                    });
                    cConNoTong.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[]
                    {
                        new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum,
                            "ConNoTong", "{0:n0}")
                    });
                    cThuTruocTong.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[]
                    {
                        new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum,
                            "ThuTruocTong", "{0:n0}")
                    });
                    cNoCuoiTong.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[]
                    {
                        new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "NoCuoiTong",
                            "{0:n0}")
                    });

                    #endregion
                }

                gc.DataSource = _data;
                gv.BestFitColumns();
            }
            catch
            {
            }
        }

        private void ItemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void ItemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Commoncls.ExportExcel(gc);
        }

        private void cbxKbc_EditValueChanged(object sender, EventArgs e)
        {
            SetDate(((ComboBoxEdit) sender).SelectedIndex);
        }

        private void itemSendMail_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                gv.SelectAll();
                //var strToaNha = (itemToaNha.EditValue ?? "").ToString().TrimEnd(',').TrimStart(',')
                //    .Replace(" ", "");
                //var ltToaNha = strToaNha.Split(',');
                //if (strToaNha == "") return;

                var db = new MasterDataContext();

                var objManagerEmployee = db.mail_SetupSendMailToEmployeeDebits
                    .Where(_ => _.BuildingId == (byte)itemToaNha.EditValue & _.IsSendMail == true & _.GroupId == 1)
                    .Select(_ => new { _.EmployeeId }).Distinct().ToList();

                foreach (var employee in objManagerEmployee)
                {
                    var objEmployee = db.tnNhanViens.FirstOrDefault(_ => _.MaNV == employee.EmployeeId);
                    if (objEmployee == null) continue;
                    if (objEmployee.Email == "") continue;

                    #region Send mail

                    var objMail = new LandSoftBuilding.Marketing.Mail.MailClient();
                    var objFrom = db.mailConfigs.OrderByDescending(_ => _.ID).FirstOrDefault();
                    int status = 1;
                    if (objFrom != null)
                    {
                        try
                        {
                            // file excel
                            var result = new System.IO.MemoryStream();
                            var options = new DevExpress.XtraPrinting.XlsExportOptionsEx();
                            options.ExportType = DevExpress.Export.ExportType.WYSIWYG;
                            gc.ExportToXlsx(result);
                            result.Seek(0, System.IO.SeekOrigin.Begin);

                            // send email
                            objMail.SmtpServer = objFrom.Server;
                            if (objFrom.Port != null) objMail.Port = objFrom.Port.Value;
                            if (objFrom.EnableSsl != null) objMail.EnableSsl = objFrom.EnableSsl.Value;
                            objMail.From = objFrom.Email;
                            objMail.Reply = objFrom.Reply;
                            objMail.Display = objFrom.Display;
                            objMail.Pass = it.EncDec.Decrypt(objFrom.Password);
                            objMail.To = objEmployee.Email;
                            objMail.Subject = "Báo cáo tổng hợp Công nợ dịch vụ";
                            objMail.Content = "";
                            var fileAttach = new System.Net.Mail.Attachment(result, "BaoCaoTongHopCongNoDichVu",
                                "application/vnd.ms-excel");
                            objMail.Attachs = new List<System.Net.Mail.Attachment>();
                            objMail.Attachs.Add(fileAttach);

                            objMail.Send();
                            status = 1;
                        }
                        catch
                        {
                            status = 2;
                        }
                    }

                    #endregion

                    #region Lịch sử gửi email

                    mailHistory objHistoryEmail = new mailHistory();
                    objHistoryEmail.MailID = objFrom.ID;
                    objHistoryEmail.ToMail = objEmployee.Email;
                    objHistoryEmail.CcMail = "";
                    objHistoryEmail.BccMail = "";
                    objHistoryEmail.Subject = "Báo cáo tổng hợp Công nợ dịch vụ";
                    objHistoryEmail.Contents = "";
                    objHistoryEmail.Status = status;
                    objHistoryEmail.DateCreate = DateTime.UtcNow.AddHours(7);
                    objHistoryEmail.StaffCreate = Common.User.MaNV;
                    objHistoryEmail.CusID = null;
                    db.mailHistories.InsertOnSubmit(objHistoryEmail);
                    db.SubmitChanges();

                    #endregion
                }

                DialogBox.Success("Đã gửi thư xong.");
                return;
            }
            catch { }
        }
    }
}