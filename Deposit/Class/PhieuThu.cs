using System.Linq;

namespace Deposit.Class
{
    public static class PhieuThu
    {
        private static Library.MasterDataContext db = new Library.MasterDataContext();

        public static Library.ptPhieuThuDaXoa CreatePhieuThuDaXoa(string lyDo, int? maKhachHang, int? maNhanVien, int? maNhanVienNhap, int? maPhanLoai, int? maTaiKhoanNganHang, byte? maToaNha, string nguoiNop, System.DateTime? ngayNhap, System.DateTime? ngayThu, string soPhieuThu, decimal? soTien, string chungTuGoc, string diaChi)
        {
            return new Library.ptPhieuThuDaXoa{ LyDo = lyDo, MaKH = maKhachHang, MaNV = maNhanVien, MaNVN = maNhanVienNhap, MaPL = maPhanLoai, MaTKNH = maTaiKhoanNganHang, MaTN = maToaNha, NguoiNop = nguoiNop, NgayNhap = ngayNhap, NgayThu = ngayThu, SoPT = soPhieuThu, SoTien = soTien, ChungTuGoc = chungTuGoc, DiaChiNN = diaChi};
        }

        public static Library.ptChiTietPhieuThuDaXoa CreateChiTietPhieuThuDaXoa(long? linkId, string soPt, decimal? soTien, string tableName, string dienGiai)
        {
            return new Library.ptChiTietPhieuThuDaXoa{ LinkID = linkId, MaPT = soPt, SoTien = soTien, TableName = tableName, DienGiai = dienGiai};
        }

        public static System.Collections.Generic.List<PhieuThuItem> GetPhieuThuDatCocByHopDong(int? hopDongDatCocId)
        {
            var list = (from pt in db.ptPhieuThus
                join mb in db.mbMatBangs on pt.MaMB equals mb.MaMB into matBang
                from mb in matBang.DefaultIfEmpty()
                join tl in db.mbTangLaus on mb.MaTL equals tl.MaTL into tanglau
                from tl in tanglau.DefaultIfEmpty()
                join pl in db.ptPhanLoais on pt.MaPL equals pl.ID into phanLoai
                from pl in phanLoai.DefaultIfEmpty()
                join kh in db.tnKhachHangs on pt.MaKH equals kh.MaKH into khachHang
                from kh in khachHang.DefaultIfEmpty()
                join nkh in db.khNhomKhachHangs on kh.MaNKH equals nkh.ID into nhomKhachHang
                from nkh in nhomKhachHang.DefaultIfEmpty()
                join nv in db.tnNhanViens on pt.MaNV equals nv.MaNV into nguoiThu
                from nv in nguoiThu.DefaultIfEmpty()
                join nvn in db.tnNhanViens on pt.MaNVN equals nvn.MaNV into nhanVienNhap
                from nvn in nhanVienNhap.DefaultIfEmpty()
                join nvs in db.tnNhanViens on pt.MaNVS equals nvs.MaNV into nhanVienSua
                from nvs in nhanVienSua.DefaultIfEmpty()
                join tk in db.nhTaiKhoans on pt.MaTKNH equals tk.ID into taiKhoan
                from tk in taiKhoan.DefaultIfEmpty()
                join nh in db.nhNganHangs on tk.MaNH equals nh.ID into nganHang
                from nh in nganHang.DefaultIfEmpty()
                join nt in db.ptPhieuThu_NguonThanhToans on pt.NguonThanhToan equals nt.ID
                where pt.HopDongDatCocId == hopDongDatCocId &
                      //pt.MaTN == maTn &
                      //SqlMethods.DateDiffDay(tuNgay, pt.NgayThu) >= 0 &
                      //SqlMethods.DateDiffDay(pt.NgayThu, denNgay) >= 0 & 
                      pt.IsKhauTru == false & pt.MaPL == 24 & pt.IsDepositFather == true
                select new PhieuThuItem
                {
                    ID = pt.ID,
                    NgayThu = pt.NgayThu,
                    GioThu = pt.NgayThu,
                    SoPhieu = pt.SoPT,
                    DienGiai = pt.LyDo,
                    SoTienThu = pt.SoTien,
                    PhanLoai = pl.TenPL,
                    MaKhachHang = kh.KyHieu,
                    KhachHang = kh.IsCaNhan == true ? kh.HoKH + " " + kh.TenKH : kh.CtyTen,
                    NguoiThu = nv.HoTenNV,
                    NguoiNop = pt.NguoiNop,
                    DiaChi = pt.DiaChiNN,
                    ChungTuGoc = pt.ChungTuGoc,
                    HinhThucThanhToan = pt.MaTKNH != null ? "Chuyển khoản" : "Tiền mặt",
                    SoTaiKhoan = tk.SoTK,
                    NganHang = nh.TenNH,
                    NhomKhachHang = nkh.TenNKH,
                    NguonThanhToan = nt.Name,
                    NguoiNhap = nvn.HoTenNV,
                    NgayNhap = pt.NgayNhap,
                    NguoiSua = nvs.HoTenNV,
                    NgaySua = pt.NgaySua,
                    DepositTyleName = pt.DepositTyleName,
                    DepositTyleId = pt.DepositTyleId,
                    TotalReceipts = pt.TotalReceipts,
                    TotalPay = pt.TotalPay,
                    ConLai = pt.SoTien.GetValueOrDefault() -
                             (pt.TotalReceipts.GetValueOrDefault() + pt.TotalPay.GetValueOrDefault()),
                    HopDongDatCocId = pt.HopDongDatCocId
                }).ToList();
            return list;
        }

        public static System.Collections.Generic.List<PhieuThuItem> GetPhieuThuDatCoc(byte? maTn, System.DateTime tuNgay, System.DateTime denNgay)
        {
            var list = (from pt in db.ptPhieuThus
                             join mb in db.mbMatBangs on pt.MaMB equals mb.MaMB into matBang
                             from mb in matBang.DefaultIfEmpty()
                             join tl in db.mbTangLaus on mb.MaTL equals tl.MaTL into tanglau
                             from tl in tanglau.DefaultIfEmpty()
                             join pl in db.ptPhanLoais on pt.MaPL equals pl.ID into phanLoai
                             from pl in phanLoai.DefaultIfEmpty()
                             join kh in db.tnKhachHangs on pt.MaKH equals kh.MaKH into khachHang
                             from kh in khachHang.DefaultIfEmpty()
                             join nkh in db.khNhomKhachHangs on kh.MaNKH equals nkh.ID into nhomKhachHang
                             from nkh in nhomKhachHang.DefaultIfEmpty()
                             join nv in db.tnNhanViens on pt.MaNV equals nv.MaNV into nguoiThu
                             from nv in nguoiThu.DefaultIfEmpty()
                             join nvn in db.tnNhanViens on pt.MaNVN equals nvn.MaNV into nhanVienNhap
                             from nvn in nhanVienNhap.DefaultIfEmpty()
                             join nvs in db.tnNhanViens on pt.MaNVS equals nvs.MaNV into nhanVienSua
                             from nvs in nhanVienSua.DefaultIfEmpty()
                             join tk in db.nhTaiKhoans on pt.MaTKNH equals tk.ID into taiKhoan
                             from tk in taiKhoan.DefaultIfEmpty()
                             join nh in db.nhNganHangs on tk.MaNH equals nh.ID into nganHang
                             from nh in nganHang.DefaultIfEmpty()
                             join nt in db.ptPhieuThu_NguonThanhToans on pt.NguonThanhToan equals nt.ID
                             where
                                 pt.MaTN == maTn &
                                 pt.HopDongDatCocId != null &
                                 System.Data.Linq.SqlClient.SqlMethods.DateDiffDay(tuNgay, pt.NgayThu) >= 0 &
                                 System.Data.Linq.SqlClient.SqlMethods.DateDiffDay(pt.NgayThu, denNgay) >= 0 & 
                                 pt.IsKhauTru == false & pt.MaPL == 24 & pt.IsDepositFather == true
                             select new PhieuThuItem
                             {
                                 ID=pt.ID,
                                 NgayThu = pt.NgayThu,
                                 GioThu = pt.NgayThu,
                                 SoPhieu = pt.SoPT,
                                 DienGiai = pt.LyDo,
                                 SoTienThu = pt.SoTien,
                                 PhanLoai = pl.TenPL,
                                 MaKhachHang = kh.KyHieu,
                                 KhachHang = kh.IsCaNhan == true ? kh.HoKH + " " + kh.TenKH : kh.CtyTen,
                                 NguoiThu = nv.HoTenNV,
                                 NguoiNop = pt.NguoiNop,
                                 DiaChi = pt.DiaChiNN,
                                 ChungTuGoc = pt.ChungTuGoc,
                                 HinhThucThanhToan = pt.MaTKNH != null ? "Chuyển khoản" : "Tiền mặt",
                                 SoTaiKhoan = tk.SoTK,
                                 NganHang = nh.TenNH,
                                 NhomKhachHang = nkh.TenNKH,
                                 NguonThanhToan = nt.Name,
                                 NguoiNhap = nvn.HoTenNV,
                                 NgayNhap = pt.NgayNhap,
                                 NguoiSua = nvs.HoTenNV,
                                 NgaySua = pt.NgaySua,
                                 DepositTyleName = pt.DepositTyleName,
                                 DepositTyleId = pt.DepositTyleId,
                                 TotalReceipts = pt.TotalReceipts,
                                 TotalPay = pt.TotalPay,
                                 ConLai = pt.SoTien.GetValueOrDefault() -
                                          (pt.TotalReceipts.GetValueOrDefault() + pt.TotalPay.GetValueOrDefault()),
                                 HopDongDatCocId = pt.HopDongDatCocId
                             }).ToList();
            return list;
        }

        public static System.Collections.Generic.List<PhieuThuItem> GetPhieuThuTienPhatByHopDong(int? hopDongDatCocId)
        {
            var list = (from pt in db.ptPhieuThus
                             join mb in db.mbMatBangs on pt.MaMB equals mb.MaMB into matBang
                             from mb in matBang.DefaultIfEmpty()
                             join tl in db.mbTangLaus on mb.MaTL equals tl.MaTL into tanglau
                             from tl in tanglau.DefaultIfEmpty()
                             join pl in db.ptPhanLoais on pt.MaPL equals pl.ID into phanLoai
                             from pl in phanLoai.DefaultIfEmpty()
                             join kh in db.tnKhachHangs on pt.MaKH equals kh.MaKH into khachHang
                             from kh in khachHang.DefaultIfEmpty()
                             join nkh in db.khNhomKhachHangs on kh.MaNKH equals nkh.ID into nhomKhachHang
                             from nkh in nhomKhachHang.DefaultIfEmpty()
                             join nv in db.tnNhanViens on pt.MaNV equals nv.MaNV into nguoiThu
                             from nv in nguoiThu.DefaultIfEmpty()
                             join nvn in db.tnNhanViens on pt.MaNVN equals nvn.MaNV into nhanVienNhap
                             from nvn in nhanVienNhap.DefaultIfEmpty()
                             join nvs in db.tnNhanViens on pt.MaNVS equals nvs.MaNV into nhanVienSua
                             from nvs in nhanVienSua.DefaultIfEmpty()
                             join tk in db.nhTaiKhoans on pt.MaTKNH equals tk.ID into taiKhoan
                             from tk in taiKhoan.DefaultIfEmpty()
                             join nh in db.nhNganHangs on tk.MaNH equals nh.ID into nganHang
                             from nh in nganHang.DefaultIfEmpty()
                             join nt in db.ptPhieuThu_NguonThanhToans on pt.NguonThanhToan equals nt.ID
                             where pt.HopDongDatCocId == hopDongDatCocId &
                                 //pt.MaTN == maTn &
                                 //SqlMethods.DateDiffDay(tuNgay, pt.NgayThu) >= 0 &
                                 //SqlMethods.DateDiffDay(pt.NgayThu, denNgay) >= 0 & 
                                 pt.IsKhauTru == false & pt.MaPL == 24 & pt.IsDepositFather == false
                             select new PhieuThuItem
                             {
                                 ID=pt.ID,
                                 NgayThu = pt.NgayThu,
                                 GioThu = pt.NgayThu,
                                 SoPhieu = pt.SoPT,
                                 DienGiai = pt.LyDo,
                                 PhaiThu = pt.ptChiTietPhieuThus.Sum(_ => _.PhaiThu.GetValueOrDefault()),
                                 SoTienThu = pt.ptChiTietPhieuThus.Sum(_ => _.SoTien.GetValueOrDefault()),
                                 TienKhauTru = pt.ptChiTietPhieuThus.Sum(_ => _.KhauTru.GetValueOrDefault()),
                                 TienThuThua = pt.ptChiTietPhieuThus.Sum(_ => _.ThuThua.GetValueOrDefault()),
                                 PhanLoai = pl.TenPL,
                                 MaKhachHang = kh.KyHieu,
                                 KhachHang = kh.IsCaNhan == true ? kh.HoKH + " " + kh.TenKH : kh.CtyTen,
                                 NguoiThu = nv.HoTenNV,
                                 NguoiNop = pt.NguoiNop,
                                 DiaChi = pt.DiaChiNN,
                                 ChungTuGoc = pt.ChungTuGoc,
                                 HinhThucThanhToan = pt.MaTKNH != null ? "Chuyển khoản" : "Tiền mặt",
                                 SoTaiKhoan = tk.SoTK,
                                 NganHang = nh.TenNH,
                                 NhomKhachHang = nkh.TenNKH,
                                 NguonThanhToan = nt.Name,
                                 NguoiNhap = nvn.HoTenNV,
                                 NgayNhap = pt.NgayNhap,
                                 NguoiSua = nvs.HoTenNV,
                                 NgaySua = pt.NgaySua,
                                 DepositTyleName=pt.DepositTyleName,
                                 DepositTyleId=pt.DepositTyleId,
                                 IsDepositFather=pt.IsDepositFather,
                                 DepositFatherId=pt.DepositFatherId
                             }).ToList();
            return list;
        }

        public static System.Collections.Generic.List<PhieuThuItem> GetPhieuThuTienPhatByDepositFather(int? depositId)
        {
            var list = (from pt in db.ptPhieuThus
                        join mb in db.mbMatBangs on pt.MaMB equals mb.MaMB into matBang
                        from mb in matBang.DefaultIfEmpty()
                        join tl in db.mbTangLaus on mb.MaTL equals tl.MaTL into tanglau
                        from tl in tanglau.DefaultIfEmpty()
                        join pl in db.ptPhanLoais on pt.MaPL equals pl.ID into phanLoai
                        from pl in phanLoai.DefaultIfEmpty()
                        join kh in db.tnKhachHangs on pt.MaKH equals kh.MaKH into khachHang
                        from kh in khachHang.DefaultIfEmpty()
                        join nkh in db.khNhomKhachHangs on kh.MaNKH equals nkh.ID into nhomKhachHang
                        from nkh in nhomKhachHang.DefaultIfEmpty()
                        join nv in db.tnNhanViens on pt.MaNV equals nv.MaNV into nguoiThu
                        from nv in nguoiThu.DefaultIfEmpty()
                        join nvn in db.tnNhanViens on pt.MaNVN equals nvn.MaNV into nhanVienNhap
                        from nvn in nhanVienNhap.DefaultIfEmpty()
                        join nvs in db.tnNhanViens on pt.MaNVS equals nvs.MaNV into nhanVienSua
                        from nvs in nhanVienSua.DefaultIfEmpty()
                        join tk in db.nhTaiKhoans on pt.MaTKNH equals tk.ID into taiKhoan
                        from tk in taiKhoan.DefaultIfEmpty()
                        join nh in db.nhNganHangs on tk.MaNH equals nh.ID into nganHang
                        from nh in nganHang.DefaultIfEmpty()
                        join nt in db.ptPhieuThu_NguonThanhToans on pt.NguonThanhToan equals nt.ID
                        where pt.DepositFatherId == depositId &
                            //pt.MaTN == maTn &
                            //SqlMethods.DateDiffDay(tuNgay, pt.NgayThu) >= 0 &
                            //SqlMethods.DateDiffDay(pt.NgayThu, denNgay) >= 0 & 
                            pt.IsKhauTru == false & pt.MaPL == 24 & pt.IsDepositFather == false
                        select new PhieuThuItem
                        {
                            ID = pt.ID,
                            NgayThu = pt.NgayThu,
                            GioThu = pt.NgayThu,
                            SoPhieu = pt.SoPT,
                            DienGiai = pt.LyDo,
                            PhaiThu = pt.ptChiTietPhieuThus.Sum(_ => _.PhaiThu.GetValueOrDefault()),
                            SoTienThu = pt.ptChiTietPhieuThus.Sum(_ => _.SoTien.GetValueOrDefault()),
                            TienKhauTru = pt.ptChiTietPhieuThus.Sum(_ => _.KhauTru.GetValueOrDefault()),
                            TienThuThua = pt.ptChiTietPhieuThus.Sum(_ => _.ThuThua.GetValueOrDefault()),
                            PhanLoai = pl.TenPL,
                            MaKhachHang = kh.KyHieu,
                            KhachHang = kh.IsCaNhan == true ? kh.HoKH + " " + kh.TenKH : kh.CtyTen,
                            NguoiThu = nv.HoTenNV,
                            NguoiNop = pt.NguoiNop,
                            DiaChi = pt.DiaChiNN,
                            ChungTuGoc = pt.ChungTuGoc,
                            HinhThucThanhToan = pt.MaTKNH != null ? "Chuyển khoản" : "Tiền mặt",
                            SoTaiKhoan = tk.SoTK,
                            NganHang = nh.TenNH,
                            NhomKhachHang = nkh.TenNKH,
                            NguonThanhToan = nt.Name,
                            NguoiNhap = nvn.HoTenNV,
                            NgayNhap = pt.NgayNhap,
                            NguoiSua = nvs.HoTenNV,
                            NgaySua = pt.NgaySua,
                            DepositTyleName = pt.DepositTyleName,
                            DepositTyleId = pt.DepositTyleId,
                            IsDepositFather = pt.IsDepositFather,
                            DepositFatherId = pt.DepositFatherId
                        }).ToList();
            return list;
        }

        public static System.Collections.Generic.List<PhieuThuItem> GetPhieuThuTienPhat(byte? maTn, System.DateTime tuNgay, System.DateTime denNgay)
        {
            var list = (from pt in db.ptPhieuThus
                             join mb in db.mbMatBangs on pt.MaMB equals mb.MaMB into matBang
                             from mb in matBang.DefaultIfEmpty()
                             join tl in db.mbTangLaus on mb.MaTL equals tl.MaTL into tanglau
                             from tl in tanglau.DefaultIfEmpty()
                             join pl in db.ptPhanLoais on pt.MaPL equals pl.ID into phanLoai
                             from pl in phanLoai.DefaultIfEmpty()
                             join kh in db.tnKhachHangs on pt.MaKH equals kh.MaKH into khachHang
                             from kh in khachHang.DefaultIfEmpty()
                             join nkh in db.khNhomKhachHangs on kh.MaNKH equals nkh.ID into nhomKhachHang
                             from nkh in nhomKhachHang.DefaultIfEmpty()
                             join nv in db.tnNhanViens on pt.MaNV equals nv.MaNV into nguoiThu
                             from nv in nguoiThu.DefaultIfEmpty()
                             join nvn in db.tnNhanViens on pt.MaNVN equals nvn.MaNV into nhanVienNhap
                             from nvn in nhanVienNhap.DefaultIfEmpty()
                             join nvs in db.tnNhanViens on pt.MaNVS equals nvs.MaNV into nhanVienSua
                             from nvs in nhanVienSua.DefaultIfEmpty()
                             join tk in db.nhTaiKhoans on pt.MaTKNH equals tk.ID into taiKhoan
                             from tk in taiKhoan.DefaultIfEmpty()
                             join nh in db.nhNganHangs on tk.MaNH equals nh.ID into nganHang
                             from nh in nganHang.DefaultIfEmpty()
                             join nt in db.ptPhieuThu_NguonThanhToans on pt.NguonThanhToan equals nt.ID
                             where
                                 pt.MaTN == maTn & 
                                 System.Data.Linq.SqlClient.SqlMethods.DateDiffDay(tuNgay, pt.NgayThu) >= 0 &
                                 System.Data.Linq.SqlClient.SqlMethods.DateDiffDay(pt.NgayThu, denNgay) >= 0 & 
                                 pt.MaPL == 24 & pt.IsDepositFather == false
                             select new PhieuThuItem
                             {
                                 ID= pt.ID,
                                 NgayThu = pt.NgayThu,
                                 GioThu = pt.NgayThu,
                                 SoPhieu = pt.SoPT,
                                 DienGiai = pt.LyDo,
                                 PhaiThu = pt.ptChiTietPhieuThus.Sum(_ => _.PhaiThu.GetValueOrDefault()),
                                 SoTienThu = pt.ptChiTietPhieuThus.Sum(_ => _.SoTien.GetValueOrDefault()),
                                 TienKhauTru = pt.ptChiTietPhieuThus.Sum(_ => _.KhauTru.GetValueOrDefault()),
                                 TienThuThua = pt.ptChiTietPhieuThus.Sum(_ => _.ThuThua.GetValueOrDefault()),
                                 PhanLoai = pl.TenPL,
                                 MaKhachHang = kh.KyHieu,
                                 KhachHang = kh.IsCaNhan == true ? kh.HoKH + " " + kh.TenKH : kh.CtyTen,
                                 NguoiThu = nv.HoTenNV,
                                 NguoiNop = pt.NguoiNop,
                                 DiaChi = pt.DiaChiNN,
                                 ChungTuGoc = pt.ChungTuGoc,
                                 HinhThucThanhToan = pt.MaTKNH != null ? "Chuyển khoản" : "Tiền mặt",
                                 SoTaiKhoan = tk.SoTK,
                                 NganHang = nh.TenNH,
                                 NhomKhachHang = nkh.TenNKH,
                                 NguonThanhToan = nt.Name,
                                 NguoiNhap = nvn.HoTenNV,
                                 NgayNhap = pt.NgayNhap,
                                 NguoiSua = nvs.HoTenNV,
                                 NgaySua = pt.NgaySua,
                                 DepositTyleName = pt.DepositTyleName,
                                 DepositTyleId = pt.DepositTyleId,
                                 IsDepositFather = pt.IsDepositFather,
                                 DepositFatherId = pt.DepositFatherId
                             }).ToList();
            return list;
        }

        public class PhieuThuItem
        {
            public int? ID { get; set; }
            public System.DateTime? NgayThu { get; set; }
            public System.DateTime? GioThu { get; set; }
            public string SoPhieu { get; set; }
            public string DienGiai { get; set; }
            public decimal? SoTienThu { get; set; }
            public string PhanLoai { get; set; }
            public string MaKhachHang { get; set; }
            public string KhachHang { get; set; }
            public string NguoiThu { get; set; }
            public string NguoiNop { get; set; }
            public string DiaChi {get;set;}
            public string ChungTuGoc { get; set; }
            public string HinhThucThanhToan { get; set; }
            public string SoTaiKhoan { get; set; }
            public string NganHang { get; set; }
            public string NhomKhachHang { get; set; }
            public System.DateTime? NgayNhap { get; set; }
            public string NguoiNhap { get; set; }
            public string NguonThanhToan { get; set; }
            public string NguoiSua { get; set; }
            public System.DateTime? NgaySua { get; set; }
            public string DepositTyleName { get; set; }
            public int? DepositTyleId { get; set; }
            public decimal? ToTalReceipts { get; set; }
            public decimal? TotalReceipts { get; set; }
            public decimal? TotalPay { get; set; }
            public decimal? ConLai { get; set; }
            public decimal? PhaiThu { get; set; }
            public decimal? TienKhauTru { get; set; }
            public decimal? TienThuThua { get; set; }
            public int? HopDongDatCocId { get; set; }
            public bool? IsDepositFather { get; set; }
            public int? DepositFatherId { get; set; }
        }
    }
}
