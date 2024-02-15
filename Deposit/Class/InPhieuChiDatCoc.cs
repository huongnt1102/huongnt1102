using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deposit.Class
{
    public static class InPhieuChiDatCoc
    {
        private static Library.MasterDataContext _db = new Library.MasterDataContext();

        #region Phiếu chi groupid = 16

        public static string PhieuChi(int id, string rtfText)
        {
            if (id == 0) return rtfText;
            var objTien = new Library.TienTeCls();

            #region Thông tin dữ liệu
            //from p in db.pcPhieuChi_TraLaiKhachHangs
            //    join pt in db.ptPhieuThus on p.MaPT equals pt.ID
            //    join nv in db.tnNhanViens on p.NguoiNhap equals nv.MaNV into nhanVien
            //    from nv in nhanVien.DefaultIfEmpty()
            //    where pt.MaTN == maTn & 
            //          pt.HopDongDatCocId!=null &
            //          System.Data.Linq.SqlClient.SqlMethods.DateDiffDay(tuNgay, p.NgayChi) >= 0 &
            //          System.Data.Linq.SqlClient.SqlMethods.DateDiffDay(p.NgayChi, denNgay) >= 0 &
            //          p.SoTienChi != 0
            //    select new PhieuChiItem
            //    {
            //        NgayChi = p.NgayChi,
            //        SoPhieuChi = p.SoPhieuChi,
            //        KhachHang = pt.NguoiNop,
            //        DiaChi = pt.DiaChiNN,
            //        PhieuDatCoc = pt.SoPT,
            //        SoTienChi = p.SoTienChi,
            //        SoTienPhat = p.SoTienPhat,
            //        DienGiai = p.GhiChu,
            //        HoTenNV = nv.HoTenNV,
            //        ID = p.ID,
            //        MaPT = p.MaPT,
            //        MaKH = pt.MaKH,
            //        MaNV = pt.MaNV,
            //        HopDongDatCocId = pt.HopDongDatCocId
            //    }).ToList()
            var obj = (from p in _db.pcPhieuChi_TraLaiKhachHangs
                join pt in _db.ptPhieuThus on p.MaPT equals pt.ID
                       join nv in _db.tnNhanViens on p.NguoiNhap equals nv.MaNV into nhanVien
                       from nv in nhanVien.DefaultIfEmpty()
                       where p.ID == id
                       select new
                       {
                           pt.MaTN,
                           SoPC = p.SoPhieuChi,
                           p.NgayChi,
                           NguoiNhan = pt.NguoiNop,
                           pt.DiaChiNN,
                           LyDo = p.GhiChu,
                           SoTien = p.SoTienChi>0?p.SoTienChi:p.SoTienPhat,
                           SoTien_BangChu = objTien.DocTienBangChu(p.SoTienChi > 0 ? p.SoTienChi.Value : p.SoTienPhat.Value, "đồng chẵn"),
                           pt.ChungTuGoc,
                           HoTenNV = nv != null ? nv.HoTenNV : "",
                           pt.MaTKNH
                       }).FirstOrDefault();
            if (obj == null) return rtfText;

            #endregion

            #region Thong tin toa nha
            var objTn = _db.tnToaNhas.FirstOrDefault(p => p.MaTN == obj.MaTN);
            if (objTn == null) return rtfText;
            //picLogo.ImageUrl = objTN.Logo;
            #endregion

            return RtfPhieuChi(rtfText, obj.MaTKNH, objTn.TenTN, obj.SoPC, obj.NguoiNhan, obj.NgayChi, obj.DiaChiNN,
                obj.LyDo, obj.SoTien_BangChu, obj.HoTenNV, obj.SoTien);
        }

        private static string RtfPhieuChi(string rtfText, int? maTknh, string tenTn, string soPc, string nguoiNhan, DateTime? ngayChi, string diaChiNn, string lyDo, string soTienBangChu, string hoTenNv, decimal? soTien)
        {
            var ctlRtf = new DevExpress.XtraRichEdit.RichEditControl { RtfText = rtfText };
            ctlRtf.Document.ReplaceAll("[TieuDePhieu]", maTknh == null ? "PHIẾU CHI" : "PHIẾU CHI TIỀN CHUYỂN KHOẢN", DevExpress.XtraRichEdit.API.Native.SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[TenTN]", "Ban Quản lý Tòa nhà: " + tenTn, DevExpress.XtraRichEdit.API.Native.SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[SoPC]", soPc, DevExpress.XtraRichEdit.API.Native.SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[NguoiNhan]", nguoiNhan, DevExpress.XtraRichEdit.API.Native.SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[NgayChi]", string.Format("Ngày {0:dd} tháng {0:MM} năm {0:yyyy}", ngayChi), DevExpress.XtraRichEdit.API.Native.SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[DiaChiNN]", diaChiNn, DevExpress.XtraRichEdit.API.Native.SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[LyDo]", lyDo, DevExpress.XtraRichEdit.API.Native.SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[SoTien]", string.Format("{0:n0} VNĐ", soTien), DevExpress.XtraRichEdit.API.Native.SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[SoTien_BangChu]", soTienBangChu, DevExpress.XtraRichEdit.API.Native.SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[HoTenNV]", hoTenNv, DevExpress.XtraRichEdit.API.Native.SearchOptions.None);

            return ctlRtf.RtfText;
        }

        #endregion
    }
}
