using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandSoftBuilding.Fund.ChiThuTruoc.Class
{
    /// <summary>
    /// Các hàm xử lý với phiếu chi
    /// </summary>
    public class PhieuChi
    {
        /// <summary>
        /// Master data
        /// </summary>
        private static Library.MasterDataContext _db = new Library.MasterDataContext();

        /// <summary>
        /// Set thông tin phiếu chi
        /// </summary>
        /// <param name="pc"></param>
        /// <param name="toaNhaId"></param>
        /// <param name="soPhieuChi"></param>
        /// <param name="ngayChi"></param>
        /// <param name="tienChi"></param>
        /// <param name="nguoiChiId"></param>
        /// <param name="khachHangId"></param>
        /// <param name="khachHangName"></param>
        /// <param name="diaChiKhachHang"></param>
        /// <param name="nhanVienNhapId"></param>
        /// <param name="nhanVienNhanId"></param>
        /// <param name="taiKhoanNganHangId"></param>
        /// <param name="chungTuGoc"></param>
        /// <param name="lyDo"></param>
        /// <param name="outPutTyleId"></param>
        /// <param name="outPutTyleName"></param>
        /// <param name="hinhThucChiId"></param>
        /// <param name="hinhThucChiName"></param>
        /// <param name="tuMatBangId"></param>
        /// <param name="tuMatBangNo"></param>
        /// <param name="denMatBangId"></param>
        /// <param name="denMatBangNo"></param>
        /// <param name="phieuThuId"></param>
        /// <param name="phieuThuNo"></param>
        /// <param name="ngayNhap"></param>
        /// <param name="nhanVienSua"></param>
        /// <param name="ngaySua"></param>
        /// <returns></returns>
        public static Library.pcPhieuChi SetPhieuChi(Library.pcPhieuChi pc, byte? toaNhaId, string soPhieuChi, System.DateTime? ngayChi, decimal? tienChi, int? nguoiChiId, int? khachHangId, string khachHangName, string diaChiKhachHang, int? nhanVienNhapId, int? nhanVienNhanId, int? taiKhoanNganHangId, string chungTuGoc, string lyDo, int? outPutTyleId, string outPutTyleName, int? hinhThucChiId, string hinhThucChiName, int? tuMatBangId, string tuMatBangNo, int? denMatBangId, string denMatBangNo, int? phieuThuId, string phieuThuNo, System.DateTime? ngayNhap, int? nhanVienSua, System.DateTime? ngaySua)
        {
            pc.MaTN = toaNhaId;
            pc.SoPC = soPhieuChi;
            pc.NgayChi = ngayChi;
            pc.SoTien = tienChi;
            pc.MaNV = nguoiChiId;
            pc.MaNCC = khachHangId;
            pc.NguoiNhan = khachHangName;
            pc.DiaChiNN = diaChiKhachHang;
            pc.MaNVN = nhanVienNhapId;
            pc.MaNVNhan = nhanVienNhanId;
            pc.MaTKNH = taiKhoanNganHangId;
            pc.ChungTuGoc = chungTuGoc;
            pc.LyDo = lyDo;
            pc.OutputTyleId = outPutTyleId;
            pc.OutputTyleName = outPutTyleName;
            pc.HinhThucChiId = hinhThucChiId;
            pc.HinhThucChiName = hinhThucChiName;
            pc.TuMatBangId = tuMatBangId;
            pc.TuMatBangNo = tuMatBangNo;
            pc.DenMatBangId = denMatBangId;
            pc.DenMatBangNo = denMatBangNo;
            pc.PhieuThuId = phieuThuId;
            pc.PhieuThuNo = phieuThuNo;
            pc.NgayNhap = ngayNhap;
            pc.MaNVS = nhanVienSua;
            pc.NgaySua = ngaySua;

            return pc;
        }
    }

    public class PhieuThu
    {
        private static Library.MasterDataContext _db = new Library.MasterDataContext();

        public static Library.ptPhieuThu SetptPhieuThu(Library.ptPhieuThu pt, byte? toaNhaId, string soPhieuThu, System.DateTime? ngayThu, int? khachHangId, int? taiKhoanNganHangId, string nguoiNop, string diaChiNguoiNop, string lyDo, string chungTuGoc, decimal? soTien, int? phanLoaiId, int? nhanVienPhieuId, int? nhanVienNhapId, int? nhanVienSuaId, System.DateTime? ngayNhap, System.DateTime? ngaySua, bool? isKhauTru, bool isKhauTruTuDong, byte nguonThanhToanId, int? hinhThucThanhToanId, string hinhThucThanhToanName, int? tuMatBangId, string tuMatBangNo, int? denMatBangId, string denMatBangNo, int? phieuChiId, string phieuChiNo)
        {
            pt.MaTN = toaNhaId;
            pt.SoPT = soPhieuThu;
            pt.NgayThu = ngayThu;
            pt.MaKH = khachHangId;
            pt.MaTKNH = taiKhoanNganHangId;
            pt.NguoiNop = nguoiNop;
            pt.DiaChiNN = diaChiNguoiNop;
            pt.LyDo = lyDo;
            pt.ChungTuGoc = chungTuGoc;
            pt.SoTien = soTien;
            pt.MaPL = phanLoaiId;
            pt.MaNV = nhanVienPhieuId;
            pt.MaNVN = nhanVienNhapId;
            pt.MaNVS = nhanVienSuaId;
            pt.NgayNhap = ngayNhap;
            pt.NgaySua = ngaySua;
            pt.MaMB = denMatBangId;
            pt.IsKhauTru = isKhauTru;
            pt.IsKhauTruTuDong = isKhauTruTuDong;
            pt.NguonThanhToan = nguonThanhToanId;
            pt.HinhThucThanhToanId = hinhThucThanhToanId;
            pt.HinhThucThanhToanName = hinhThucThanhToanName;
            pt.TuMatBangId = tuMatBangId;
            pt.TuMatBangNo = tuMatBangNo;
            pt.DenMatBangNo = denMatBangNo;
            pt.PhieuChiId = phieuChiId;
            pt.PhieuChiNo = phieuChiNo;

            return pt;
        }

        public static Library.ptChiTietPhieuThu SetChiTietPhieuThu(Library.ptChiTietPhieuThu ct, int? phieuThuId, string dienGiai, decimal? soTien)
        {
            ct.MaPT = phieuThuId;
            ct.DienGiai = dienGiai;
            ct.SoTien = soTien;

            return ct;
        }

        public static string TaoSoPhieuThu(int phuongThucThanhToanId, object matBangId, int month, int year, byte buildingId, bool isKhauTru)
        {
            var maSo = "";
            if(matBangId!=null)
            {
                int temp = 0;
                if(int.TryParse(matBangId.ToString(), out temp))
                {
                    var matBang = _db.mbMatBangs.FirstOrDefault(_ => _.MaMB == Convert.ToInt32(matBangId));
                    return Library.Common.CreatePhieuThu(phuongThucThanhToanId, month, year, matBang.mbTangLau.MaKN.Value, buildingId, isKhauTru);
                }
            }
            return maSo;
        }
    }
}
