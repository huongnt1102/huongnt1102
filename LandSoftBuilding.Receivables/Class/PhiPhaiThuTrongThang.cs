using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandSoftBuilding.Receivables.Class
{
    /// <summary>
    /// Phí phải thu trong tháng
    /// </summary>
    public class PhiPhaiThuTrongThang
    {
        public class KhachHang { public int? MaKH { get; set; } public int? MaMB { get; set; } public string ChuHo { get; set; } }

        public class PhiQuanLy { public int? MaKH { get; set; } public decimal? SoTien { get; set; } public decimal? PhiQuanLyDienTich { get; set; } public decimal? PhiQuanLyDonGia { get; set; } public int? MaMB { get; set; } }

        public class PhiDien { public int? MaKH { get; set; } public decimal? PhiDienCsc { get; set; } public decimal? PhiDienCsm { get; set; } public decimal? SoTieuThu { get; set; } public decimal? DonGia { get; set; } public decimal? ThanhTien { get; set; } public int? MaMB { get; set; } }

        public class PhiNuoc { public int? MaKH { get; set; } public int? PhiNuocCsc { get; set; } public int? PhiNuocCsm { get; set; } public int? PhiNuocTieuThu { get; set; } public int? PhiNuocNhanKhau { get; set; } public int? PhiNuocHuongDm { get; set; } public decimal? PhiNuocTongNuoc { get; set; } public int? MaMB { get; set; } public int? PhiNuocSoLuongDm1 { get; set; } public decimal? PhiNuocDonGiaDm1 { get; set; } public decimal? PhiNuocThanhTienDm1 { get; set; } public int? PhiNuocSoLuongDm2 { get; set; } public decimal? PhiNuocDonGiaDm2 { get; set; } public decimal? PhiNuocThanhTienDm2 { get; set; } public int? PhiNuocSoLuongDm3 { get; set; } public decimal? PhiNuocDonGiaDm3 { get; set; } public decimal? PhiNuocThanhTienDm3 { get; set; } public int? PhiNuocSoLuongDm4 { get; set; } public decimal? PhiNuocDonGiaDm4 { get; set; } public decimal? PhiNuocThanhTienDm4 { get; set; } }

        public class PhiXe { public int? MaKH { get; set; } public int? PhiXeThangOToTheoDinhMucSoLuong { get; set; } public decimal? PhiXeThangOToTheoDinhMucDonGia { get; set; } public decimal? PhiXeThangOToTheoDinhMucThanhTien { get; set; } public int? MaMB { get; set; } public int? PhiXeThangOToNgoaiDinhMucSoLuong { get; set; } public decimal? PhiXeThangOToNgoaiDinhMucDonGia { get; set; } public decimal? PhiXeThangOToNgoaiDinhMucThanhTien { get; set; } public int? PhiXeThangXeMayTheoDinhMucSoLuong { get; set; } public decimal? PhiXeThangXeMayTheoDinhMucDonGia { get; set; } public decimal? PhiXeThangXeMayTheoDinhMucThanhTien { get; set; } public int? PhiXeThangXeMayNgoaiDinhMucSoLuong { get; set; } public decimal? PhiXeThangXeMayNgoaiDinhMucDonGia { get; set; } public decimal? PhiXeThangXeMayNgoaiDinhMucThanhTien { get; set; } public int? PhiXeThangXeDapSoLuong { get; set; } public decimal? PhiXeThangXeDapDonGia { get; set; } public decimal? PhiXeThangXeDapThanhTien { get; set; } public decimal? PhiXeThangTongTien { get; set; } }

        public class PhiVangLai { public int? MaKH { get; set; } public int? MaMB { get; set; } public decimal? ThuKhacPhiCoSoHaTang { get; set; } public decimal? PhiXeVangLai { get; set; } public decimal? ThuKhacPhiVeSinh { get; set; } public decimal? ThanhTien { get; set; } public decimal? TongTien { get; set; } }

        public class PhiKhac { public int? MaKH { get; set; } public int? MaMB { get; set; } public decimal? ThuKhac { get; set; } }

        public class List { public string MaCan { get; set; } public string ChuHo { get; set; } public decimal? PhiQuanLyDienTich { get; set; } public decimal? PhiQuanLyDonGia { get; set; } public decimal? PhiQuanLyPql { get; set; } public decimal? PhiDienCsc { get; set; } public decimal? PhiDienCsm { get; set; } public decimal? PhiDienTieuThu { get; set; } public decimal? PhiDienDonGia { get; set; } public decimal? PhiDienThanhTien { get; set; } public int? PhiNuocCsc { get; set; } public int? PhiNuocCsm { get; set; } public int? PhiNuocTieuThu { get; set; } public int? PhiNuocNhanKhau { get; set; } public int? PhiNuocHuongDm { get; set; } public decimal? PhiNuocTongNuoc { get; set; } public int? PhiNuocSoLuongDm1 { get; set; } public decimal? PhiNuocDonGiaDm1 { get; set; } public decimal? PhiNuocThanhTienDm1 { get; set; } public int? PhiNuocSoLuongDm2 { get; set; } public decimal? PhiNuocDonGiaDm2 { get; set; } public decimal? PhiNuocThanhTienDm2 { get; set; } public int? PhiNuocSoLuongDm3 { get; set; } public decimal? PhiNuocDonGiaDm3 { get; set; } public decimal? PhiNuocThanhTienDm3 { get; set; } public int? PhiNuocSoLuongDm4 { get; set; } public decimal? PhiNuocDonGiaDm4 { get; set; } public decimal? PhiNuocThanhTienDm4 { get; set; } public int? PhiXeThangOToTheoDinhMucSoLuong { get; set; } public decimal? PhiXeThangOToTheoDinhMucDonGia { get; set; } public decimal? PhiXeThangOToTheoDinhMucThanhTien { get; set; } public int? PhiXeThangOToNgoaiDinhMucSoLuong { get; set; } public decimal? PhiXeThangOToNgoaiDinhMucDonGia { get; set; } public decimal? PhiXeThangOToNgoaiDinhMucThanhTien { get; set; } public int? PhiXeThangXeMayTheoDinhMucSoLuong { get; set; } public decimal? PhiXeThangXeMayTheoDinhMucDonGia { get; set; } public decimal? PhiXeThangXeMayTheoDinhMucThanhTien { get; set; } public int? PhiXeThangXeMayNgoaiDinhMucSoLuong { get; set; } public decimal? PhiXeThangXeMayNgoaiDinhMucDonGia { get; set; } public decimal? PhiXeThangXeMayNgoaiDinhMucThanhTien { get; set; } public int? PhiXeThangXeDapSoLuong { get; set; } public decimal? PhiXeThangXeDapDonGia { get; set; } public decimal? PhiXeThangXeDapThanhTien { get; set; } public decimal? PhiXeThangTongTien { get; set; } public decimal? PhiXeVangLai { get; set; } public decimal? ThuKhacPhiVeSinh { get; set; } public decimal? ThuKhacPhiCoSoHaTang { get; set; } public decimal? ThuKhacKhac { get; set; } public decimal? ThuKhacTong { get; set; } public decimal? TongCong { get; set; } }
    }

    public class MatBangItem
    {
        public int? MaMB { get; set; }
        public string KhoiNha { get; set; }
        public string LoaiMatBangName { get; set; }
        public string MaSoMB { get; set; }
        public decimal? DienTich { set; get; }
    }
}
