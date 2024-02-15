using System;
using DevExpress.XtraRichEdit.API.Native;
using Library;

namespace Library.BieuMauCls
{
    public class ReplaceHopDongCls : DevExpress.XtraRichEdit.RichEditControl
    {   
        public void ThayNoiDungHD(thueHopDong objhopdong)
        {
            TienTeCls ttcls = new TienTeCls();
            decimal TongTien; 
            const decimal ThueVAT = 1.1M;
            decimal DonGia = (decimal)objhopdong.ThanhTien;
            //decimal DienTich = (decimal)objhopdong.DienTich;
            TongTien = DonGia * objhopdong.ChuKyThanhToan.Value * ThueVAT + objhopdong.PhiBaoDuong ?? 0;
            DateTime Now;
            using (MasterDataContext db = new MasterDataContext())
            {
                Now = db.GetSystemDate();
            }

            Document.ReplaceAll("[TenKhachHang]", (bool)objhopdong.tnKhachHang.IsCaNhan ? String.Format("{0} {1}", objhopdong.tnKhachHang.HoKH, objhopdong.tnKhachHang.TenKH) : objhopdong.tnKhachHang.CtyTen, DevExpress.XtraRichEdit.API.Native.SearchOptions.None);
            Document.ReplaceAll("[DiaChi]", objhopdong.tnKhachHang.DCLL, DevExpress.XtraRichEdit.API.Native.SearchOptions.None);
            Document.ReplaceAll("[DienThoai]", objhopdong.tnKhachHang.DienThoaiKH, DevExpress.XtraRichEdit.API.Native.SearchOptions.None);
            Document.ReplaceAll("[Fax]", objhopdong.tnKhachHang.CtyFax, DevExpress.XtraRichEdit.API.Native.SearchOptions.None);
            Document.ReplaceAll("[MaSoThue]", objhopdong.tnKhachHang.CtyMaSoThue ?? "", SearchOptions.None);
            Document.ReplaceAll("[TaiKhoanNganHang]", String.Format("{0} {1}", objhopdong.tnKhachHang.CtySoTKNH, objhopdong.tnKhachHang.CtyTenNH), SearchOptions.None);
            Document.ReplaceAll("[DaiDien]", (bool)objhopdong.tnKhachHang.IsCaNhan ? String.Format("{0} {1}", objhopdong.tnKhachHang.HoKH, objhopdong.tnKhachHang.TenKH) : objhopdong.tnKhachHang.CtyNguoiDD, SearchOptions.None);
            Document.ReplaceAll("[ChucVu]", objhopdong.tnKhachHang.CtyChucVuDD,SearchOptions.None);
            Document.ReplaceAll("[DienTichChothue]", objhopdong.mbMatBang.DienTich.ToString(), SearchOptions.None);
            Document.ReplaceAll("[ViTriChoThue]", String.Format("Khu {0} - Tầng {1} - Mặt bằng {2}", objhopdong.mbMatBang.mbTangLau.mbKhoiNha.TenKN, objhopdong.mbMatBang.mbTangLau.TenTL, objhopdong.mbMatBang.MaSoMB), SearchOptions.None);
            Document.ReplaceAll("[ThoiGianBanGiao]",((DateTime)objhopdong.NgayBG).ToShortDateString(),SearchOptions.None);
            Document.ReplaceAll("[DonGia]", DonGia.ToString("C"), SearchOptions.None);
            Document.ReplaceAll("[DonGiaDaVAT]", (DonGia*1.1M).ToString("C"), SearchOptions.None);
            Document.ReplaceAll("[TienThanhToan]", TongTien.ToString("C"), SearchOptions.None);
            Document.ReplaceAll("[TienBangChu]", ttcls.DocTienBangChu(TongTien,"VND"),SearchOptions.None);
            Document.ReplaceAll("[ThoiHanThue]", objhopdong.ThoiHan.ToString(), SearchOptions.None);
            Document.ReplaceAll("[SoHD]", objhopdong.SoHD, SearchOptions.None);
            Document.ReplaceAll("[NamHD]", objhopdong.NgayHD.Value.Year.ToString(), SearchOptions.None);
            Document.ReplaceAll("[NgayThangNamHienTai]", string.Format("ngày {0} tháng {1} năm {2}",Now.Day,Now.Month,Now.Year), SearchOptions.None);
            Document.ReplaceAll("[ChuKy]", objhopdong.ChuKyThanhToan.ToString(), SearchOptions.None);
            Document.ReplaceAll("[ChuKyChu]", ttcls.DocTienBangChu(objhopdong.ChuKyThanhToan.Value, ""), SearchOptions.None);
            Document.ReplaceAll("[PhiBaoDuong]", (objhopdong.PhiBaoDuong ?? 0).ToString("C"), SearchOptions.None);
            Document.ReplaceAll("[CMND/Passport]", objhopdong.tnKhachHang.CMND, SearchOptions.None);
            Document.ReplaceAll("[MaSoMB]", objhopdong.mbMatBang.MaSoMB, SearchOptions.None);
            try
            {
                Document.ReplaceAll("[NgayCap]", Convert.ToDateTime(objhopdong.tnKhachHang.NgayCap).ToShortDateString(), SearchOptions.None);
            }
            catch { Document.ReplaceAll("[NgayCap]", "......./........./...............", SearchOptions.None); }
            Document.ReplaceAll("[NoiCap]", objhopdong.tnKhachHang.NoiCap, SearchOptions.None);
            Document.ReplaceAll("[TuNgay]", objhopdong.NgayBG.Value.ToShortDateString(), SearchOptions.None);
            Document.ReplaceAll("[DenNgay]", objhopdong.NgayBG.Value.AddMonths(objhopdong.ThoiHan ?? 0).ToShortDateString(), SearchOptions.None);
            Document.ReplaceAll("[DatCoc]", (objhopdong.TienCoc ?? 0).ToString("C"), SearchOptions.None);
        }

        public void ThayNoiDungBanGiao(thueHopDong objhopdong)
        {
            TienTeCls ttcls = new TienTeCls();
        }
    }
}
