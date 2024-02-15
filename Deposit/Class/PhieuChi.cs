using System.Linq;

namespace Deposit.Class
{
    public static class PhieuChi
    {
        private static Library.MasterDataContext db = new Library.MasterDataContext();

        public static System.Collections.Generic.List<PhieuChiItem> GetPhieuChiByHopDong(int? hopDongDatCocId)
        {
            return (from p in db.pcPhieuChi_TraLaiKhachHangs
                join pt in db.ptPhieuThus on p.MaPT equals pt.ID
                join nv in db.tnNhanViens on p.NguoiNhap equals nv.MaNV into nhanVien
                from nv in nhanVien.DefaultIfEmpty()
                where pt.HopDongDatCocId == hopDongDatCocId &
                      p.SoTienChi!=0
                select new PhieuChiItem
                {
                    NgayChi=p.NgayChi,
                    SoPhieuChi=p.SoPhieuChi,
                    KhachHang = pt.NguoiNop,
                    DiaChi = pt.DiaChiNN,
                    PhieuDatCoc = pt.SoPT,
                    SoTienChi=p.SoTienChi,
                    SoTienPhat=p.SoTienPhat,
                    DienGiai = p.GhiChu,
                    HoTenNV=nv.HoTenNV,
                    ID=p.ID,
                    MaPT=p.MaPT,
                    MaKH=pt.MaKH,
                    MaNV=pt.MaNV,
                    HopDongDatCocId=pt.HopDongDatCocId
                }).ToList();
        }

        public static System.Collections.Generic.List<PhieuChiItem> GetPhieuChiByPhieuThu(int? phieuThuId)
        {
            return (from p in db.pcPhieuChi_TraLaiKhachHangs
                join pt in db.ptPhieuThus on p.MaPT equals pt.ID
                join nv in db.tnNhanViens on p.NguoiNhap equals nv.MaNV into nhanVien
                from nv in nhanVien.DefaultIfEmpty()
                where p.MaPT == phieuThuId &
                      p.SoTienChi != 0
                select new PhieuChiItem
                {
                    NgayChi = p.NgayChi,
                    SoPhieuChi = p.SoPhieuChi,
                    KhachHang = pt.NguoiNop,
                    DiaChi = pt.DiaChiNN,
                    PhieuDatCoc = pt.SoPT,
                    SoTienChi = p.SoTienChi,
                    SoTienPhat = p.SoTienPhat,
                    DienGiai = p.GhiChu,
                    HoTenNV = nv.HoTenNV,
                    ID = p.ID,
                    MaPT = p.MaPT,
                    MaKH = pt.MaKH,
                    MaNV = pt.MaNV,
                    HopDongDatCocId = pt.HopDongDatCocId
                }).ToList();
        }

        public static System.Collections.Generic.List<PhieuChiItem> GetPhieuChi(byte? maTn, System.DateTime tuNgay, System.DateTime denNgay)
        {
            return (from p in db.pcPhieuChi_TraLaiKhachHangs
                join pt in db.ptPhieuThus on p.MaPT equals pt.ID
                join nv in db.tnNhanViens on p.NguoiNhap equals nv.MaNV into nhanVien
                from nv in nhanVien.DefaultIfEmpty()
                where pt.MaTN == maTn & 
                      pt.HopDongDatCocId!=null &
                      System.Data.Linq.SqlClient.SqlMethods.DateDiffDay(tuNgay, p.NgayChi) >= 0 &
                      System.Data.Linq.SqlClient.SqlMethods.DateDiffDay(p.NgayChi, denNgay) >= 0 &
                      p.SoTienChi != 0
                select new PhieuChiItem
                {
                    NgayChi = p.NgayChi,
                    SoPhieuChi = p.SoPhieuChi,
                    KhachHang = pt.NguoiNop,
                    DiaChi = pt.DiaChiNN,
                    PhieuDatCoc = pt.SoPT,
                    SoTienChi = p.SoTienChi,
                    SoTienPhat = p.SoTienPhat,
                    DienGiai = p.GhiChu,
                    HoTenNV = nv.HoTenNV,
                    ID = p.ID,
                    MaPT = p.MaPT,
                    MaKH = pt.MaKH,
                    MaNV = pt.MaNV,
                    HopDongDatCocId = pt.HopDongDatCocId
                }).ToList();
        }

        public class PhieuChiItem
        {
            public System.DateTime? NgayChi { get; set; }
            public string SoPhieuChi { get; set; }
            public string KhachHang { get; set; }
            public string DiaChi { get; set; }
            public string PhieuDatCoc { get; set; }
            public decimal? SoTienChi { get; set; }
            public decimal? SoTienPhat { get; set; }
            public string DienGiai { get; set; }
            public string HoTenNV { get; set; }
            public int? ID { get; set; }
            public int? MaPT { get; set; }
            public int? MaKH { get; set; }
            public int? MaNV { get; set; }
            public int? HopDongDatCocId { get; set; }
        }
    }
}
