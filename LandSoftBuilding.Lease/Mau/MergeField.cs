using Library;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Data.Linq.SqlClient;
using DevExpress.XtraRichEdit;
using DevExpress.XtraRichEdit.API.Native;
using DevExpress.XtraReports.Parameters;
using System.Windows.Forms;
using System.Diagnostics;

namespace LandSoftBuilding.Lease.Mau
{
    public class MergeField : IDisposable
    {
        private bool disposed;
        public int MaKH { get; set; }
        public int Thang { get; set; }
        public int Nam { get; set; }
        public int MaTN { get; set; }
        public bool IsError = false;
        RichEditControl ctlRTF;
        MasterDataContext db;
        tnToaNha objTN;
        List<int?> ltDV = new List<int?>() { 6, 8, 9, 10, 11, 13, 14, 15 };

        public MergeField()
        {
            ctlRTF = new DevExpress.XtraRichEdit.RichEditControl();
            db = new MasterDataContext();
            objTN = new tnToaNha();
        }

        /// <summary>
        /// Destructor
        /// </summary>
        ~MergeField()
        {
            this.Dispose(false);
        }

        /// <summary>
        /// The dispose method that implements IDisposable.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// The virtual dispose method that allows
        /// classes inherithed from this one to dispose their resources.
        /// </summary>
        /// <param name="disposing"></param>
        /// 

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    // Dispose managed resources here.
                }

                // Dispose unmanaged resources here.
            }

            disposed = true;
        }

        public void LoadData()
        {
            objTN = db.tnToaNhas.Single(p => p.MaTN == this.MaTN);
        }

        public string DateTimeToString(DateTime? date, string _format)
        {
            if (date == null)
                return "";

            return string.Format(_format, date);
        }

        public string DateTimeToString(DateTime? date)
        {
            if (date == null)
                return "";

            return string.Format("{0:dd/MM/yyyy}", date);
        }

        public string DecimalToString(decimal? num)
        {
            try
            {
                return string.Format("{0:#,0.##}", num.GetValueOrDefault());
            }
            catch
            {
                return "";
            }
        }

        public string DecimalToString(decimal? num, string _format)
        {
            try
            {
                return string.Format(_format, num);
            }
            catch
            {
                return "";
            }
        }

        public string HopDongThue(string str, int ID, bool Isprint)
        {
            try
            {
                ctlRTF.Document.RtfText = str;

                var objThongTin = (from hd in db.ctHopDongs
                                   join kh in db.tnKhachHangs on hd.MaKH equals kh.MaKH
                                   where hd.ID == ID
                                   select new
                                   {
                                       //1. Thông tin hợp đồng
                                       hd.SoHDCT,
									   KyTT = hd.KyTT.GetValueOrDefault().ToString().PadLeft(2, '0'),
									   //KyTT_EN = new TienTeCls().FindNumber((decimal)hd.KyTT.GetValueOrDefault()),
									   NgayHL = DateTimeToString(hd.NgayHL),
                                       NgayHH = DateTimeToString(hd.NgayHH),
                                       NgayKy = DateTimeToString(hd.NgayKy),
                                       NgayKy_Ngay = DateTimeToString(hd.NgayKy, "{0:dd}"),
                                       NgayKy_Thang = DateTimeToString(hd.NgayKy, "{0:MM}"),
                                       NgayKy_Nam = DateTimeToString(hd.NgayKy, "{0:yyyy}"),
                                       ThoiHan_Thang = DecimalToString(hd.ThoiHan),
                                       hd.ThoiHan,
                                       NgayBanGiao = DateTimeToString(hd.NgayBG),
                                       TenKH = kh.IsCaNhan.GetValueOrDefault() ? kh.HoKH + " " + kh.TenKH : kh.CtyTen,
                                       hd.TienCoc,
                                       //hd.SoThangCoc,
                                       TyLeDCGT = DecimalToString(hd.TyLeDCGT, "{0:p0}"),
                                       //hd.SoNamDCGT,
                                       //2. Thông tin khách hàng cá nhân
                                       TenKH_CaNhan = kh.IsCaNhan.GetValueOrDefault() ? kh.HoKH ?? "" + " " + kh.TenKH ?? "" : "",
                                       NgaySinh = kh.IsCaNhan.GetValueOrDefault() ? DateTimeToString(kh.NgaySinh) : "",
                                       CMND = kh.IsCaNhan.GetValueOrDefault() ? kh.CMND ?? "" : "",
                                       NgayCap = kh.IsCaNhan.GetValueOrDefault() ? kh.NgayCap ?? "" : "",
                                       NoiCap = kh.IsCaNhan.GetValueOrDefault() ? kh.NoiCap ?? "" : "",
                                       DCTT = kh.IsCaNhan.GetValueOrDefault() ? kh.DCTT ?? "" : "",
                                       DienThoai = kh.DienThoaiKH,
                                       EmailKH = kh.IsCaNhan.GetValueOrDefault() ? kh.EmailKH ?? "" : "",
                                       TaiKhoanNganHang = kh.IsCaNhan.GetValueOrDefault() ? kh.TaiKhoanNganHang ?? "" : "",
                                       //3. Thông tin cty
                                       TenCty = !kh.IsCaNhan.GetValueOrDefault() ? kh.CtyTen ?? "" : "",
                                       CtySoDKKD = !kh.IsCaNhan.GetValueOrDefault() ? kh.CtySoDKKD ?? "" : "",
                                       CtyNgayDKKD = !kh.IsCaNhan.GetValueOrDefault() ? kh.CtyNgayDKKD ?? "" : "",
                                       CtyNoiDKKD = !kh.IsCaNhan.GetValueOrDefault() ? kh.CtyNoiDKKD ?? "" : "",
                                       //CtyDiaChi = !kh.IsCaNhan.GetValueOrDefault() ? kh.CtyDiaChi ?? "" : "",
                                       CtyDienThoai = kh.DienThoaiKH,
                                       SoTKNH = !kh.IsCaNhan.GetValueOrDefault() ? kh.CtySoTKNH ?? "" : "",
                                       CtyTenNH = !kh.IsCaNhan.GetValueOrDefault() ? kh.CtyTenNH ?? "" : "",

                                       //4. Người đại diện
                                       TenNDD = !kh.IsCaNhan.GetValueOrDefault() ? kh.CtyNguoiDD ?? "" : "",
                                       CtyChucVuDD = !kh.IsCaNhan.GetValueOrDefault() ? kh.CtyChucVuDD ?? "" : "",
                                       MaSoThue = kh.MaSoThue != null ? kh.MaSoThue : ""

                                   }).First();
                #region Thông tin chung
                var _ngayHienTai = db.GetSystemDate();
                ctlRTF.Document.ReplaceAll("[NgayIn]", DateTimeToString(_ngayHienTai), SearchOptions.None);
                ctlRTF.Document.ReplaceAll("[NgayIn_Ngay]", DateTimeToString(_ngayHienTai, "{0:dd}"), SearchOptions.None);
                ctlRTF.Document.ReplaceAll("[NgayIn_Thang]", DateTimeToString(_ngayHienTai, "{0:MM}"), SearchOptions.None);
                ctlRTF.Document.ReplaceAll("[NgayIn_Nam]", DateTimeToString(_ngayHienTai, "{0:yyyy}"), SearchOptions.None);
                #endregion

                #region 1. Thông tin hợp đồng
                ctlRTF.Document.ReplaceAll("[TenKH]", objThongTin.TenKH.ToUpper(), SearchOptions.None);
                ctlRTF.Document.ReplaceAll("[SoHD]", objThongTin.SoHDCT.ToUpper(), SearchOptions.None);
                ctlRTF.Document.ReplaceAll("[NgayHL]", objThongTin.NgayHL, SearchOptions.None);
                ctlRTF.Document.ReplaceAll("[NgayBanGiao]", objThongTin.NgayBanGiao, SearchOptions.None);
                ctlRTF.Document.ReplaceAll("[NgayHH]", objThongTin.NgayHH, SearchOptions.None);
                ctlRTF.Document.ReplaceAll("[NgayKy]", objThongTin.NgayKy, SearchOptions.None);
                ctlRTF.Document.ReplaceAll("[NgayKy_Ngay]", objThongTin.NgayKy_Ngay, SearchOptions.None);
                ctlRTF.Document.ReplaceAll("[NgayKy_Thang]", objThongTin.NgayKy_Thang, SearchOptions.None);
                ctlRTF.Document.ReplaceAll("[NgayKy_Nam]", objThongTin.NgayKy_Nam, SearchOptions.None);
                ctlRTF.Document.ReplaceAll("[ThoiHan_Thang]", objThongTin.ThoiHan_Thang, SearchOptions.None);

				ctlRTF.Document.ReplaceAll("[KyTT]", objThongTin.KyTT, SearchOptions.None);
				//ctlRTF.Document.ReplaceAll("[KyTT_EN]", objThongTin.KyTT_EN, SearchOptions.None);
				var _ThoiHan_Nam = objThongTin.ThoiHan / 12M;
                ctlRTF.Document.ReplaceAll("[ThoiHan_Nam]", DecimalToString(_ThoiHan_Nam), SearchOptions.None);
                ctlRTF.Document.ReplaceAll("[TienDatCoc]", DecimalToString(objThongTin.TienCoc), SearchOptions.None);
                ctlRTF.Document.ReplaceAll("[TienDatCoc_BC]", new TienTeCls().DocTienBangChu((long)objThongTin.TienCoc, "").TrimEnd(), SearchOptions.None);      
                ctlRTF.Document.ReplaceAll("[DCGT_TyLe]", objThongTin.TyLeDCGT, SearchOptions.None);
				#endregion
				#region 2. Thông tin khách hàng cá nhân
				ctlRTF.Document.ReplaceAll("[TenKH_CaNhan]", objThongTin.TenKH_CaNhan, SearchOptions.None);
                ctlRTF.Document.ReplaceAll("[KH_NgaySinh]", objThongTin.NgaySinh, SearchOptions.None);
                ctlRTF.Document.ReplaceAll("[KH_SoCMND]", objThongTin.CMND, SearchOptions.None);
                ctlRTF.Document.ReplaceAll("[KH_NgayCap]", objThongTin.NgayCap, SearchOptions.None);
                ctlRTF.Document.ReplaceAll("[KH_NoiCap]", objThongTin.NoiCap, SearchOptions.None);
                ctlRTF.Document.ReplaceAll("[KH_DCTT]", objThongTin.DCTT, SearchOptions.None);
                ctlRTF.Document.ReplaceAll("[KH_DienThoai]", objThongTin.DienThoai, SearchOptions.None);
                ctlRTF.Document.ReplaceAll("[KH_Email]", objThongTin.EmailKH, SearchOptions.None);
                ctlRTF.Document.ReplaceAll("[KH_TaiKhoanNganHang]", objThongTin.TaiKhoanNganHang, SearchOptions.None);
                #endregion

                #region 3. Thông tin khách hàng doanh nghiệp
                ctlRTF.Document.ReplaceAll("[Cty_TenCongTy]", objThongTin.TenCty, SearchOptions.None);
                ctlRTF.Document.ReplaceAll("[Cty_SoDKKD]", objThongTin.CtySoDKKD, SearchOptions.None);
                ctlRTF.Document.ReplaceAll("[Cty_NgayDKKD]", objThongTin.CtyNgayDKKD, SearchOptions.None);
                ctlRTF.Document.ReplaceAll("[Cty_NoiDKKD]", objThongTin.CtyNoiDKKD, SearchOptions.None);
                //ctlRTF.Document.ReplaceAll("[Cty_DiaChi]", objThongTin.CtyDiaChi, SearchOptions.None);
                ctlRTF.Document.ReplaceAll("[Cty_DienThoai]", objThongTin.CtyDienThoai, SearchOptions.None);
                ctlRTF.Document.ReplaceAll("[Cty_SoTKNH]", objThongTin.CtySoDKKD, SearchOptions.None);
                ctlRTF.Document.ReplaceAll("[Cty_TenNH]", objThongTin.CtyTenNH, SearchOptions.None);
                ctlRTF.Document.ReplaceAll("[MaSoThue]", objThongTin.MaSoThue, SearchOptions.None);
                #endregion

                #region 4. Thông tin người đại diện
                ctlRTF.Document.ReplaceAll("[Cty_NguoiDaiDien]", objThongTin.TenNDD, SearchOptions.None);
                ctlRTF.Document.ReplaceAll("[ChucVu_NguoiDaiDien]", objThongTin.CtyChucVuDD, SearchOptions.None);
                #endregion

                #region Thông tin chi tiết hợp đồng
                var ltChiTiet = (from p in db.ctChiTiets
                                 join mb in db.mbMatBangs on p.MaMB equals mb.MaMB
                                 join lmb in db.mbLoaiMatBangs on mb.MaLMB equals lmb.MaLMB
                                 join tl in db.mbTangLaus on mb.MaTL equals tl.MaTL
                                 join kn in db.mbKhoiNhas on tl.MaKN equals kn.MaKN
                                 join tn in db.tnToaNhas on mb.MaTN equals tn.MaTN
                                 where p.MaHDCT == ID
                                 select new
                                 {
                                     mb.MaSoMB,
                                     kn.TenKN,
                                     tl.TenTL,
                                     lmb.TenLMB,
                                     p.DienTich,
                                     p.DonGia,
                                     p.PhiDichVu,
                                     tn.TenTN,
                                     mb.DienGiai
                                     , p.TongGiaThue
                                     ,p.TienVAT
                                     , p.ThanhTien
                                     , p.TyGiaNgoaiTe
                                 });
                string ChiTiet_MaSoMB = string.Join("+", ltChiTiet.Select(o => o.MaSoMB).ToArray());
                string ChiTiet_TangLau = string.Join("+", ltChiTiet.Select(o => o.TenTL).Distinct().ToArray());
                string ChiTiet_KhoiNha = string.Join("+", ltChiTiet.Select(o => o.TenKN).Distinct().ToArray());
                string ChiTiet_LMB = string.Join("+", ltChiTiet.Select(o => o.TenLMB).Distinct().ToArray());

                //ctlRTF.Document.ReplaceAll("[ChiTietMBThue]", ChiTietHDT, SearchOptions.None);
                ctlRTF.Document.ReplaceAll("[ChiTietMBThue_MaSoMB]", ChiTiet_MaSoMB, SearchOptions.None);
                ctlRTF.Document.ReplaceAll("[ChiTietMBThue_TangLau]", ChiTiet_TangLau, SearchOptions.None);
                ctlRTF.Document.ReplaceAll("[ChiTietMBThue_KhoiNha]", ChiTiet_KhoiNha, SearchOptions.None);
                ctlRTF.Document.ReplaceAll("[ChiTietMBThue_LoaiMB]", ChiTiet_LMB, SearchOptions.None);
                ctlRTF.Document.ReplaceAll("[ChiTietMBThue_DienGiai]", ChiTiet_LMB, SearchOptions.None);

                var _TongDienTich = ltChiTiet.Sum(o => o.DienTich).GetValueOrDefault();

                ctlRTF.Document.ReplaceAll("[TongDienTichThue]", string.Format("{0:#,0.##}", _TongDienTich), SearchOptions.None);

                var _DonGiaThue = Math.Round(ltChiTiet.Sum(o => o.DienTich * (o.DonGia*o.TyGiaNgoaiTe)).GetValueOrDefault());
                var _DonGiaThue_1m2 = Math.Round(ltChiTiet.Average(o => o.DonGia).GetValueOrDefault());

                ctlRTF.Document.ReplaceAll("[DonGiaThue]", string.Format("{0:n0}", _DonGiaThue), SearchOptions.None);
                ctlRTF.Document.ReplaceAll("[DonGiaThue_BC]", new TienTeCls().DocTienBangChu((long)_DonGiaThue, "đồng").TrimEnd(), SearchOptions.None);
				ctlRTF.Document.ReplaceAll("[DonGiaThue_BC_EN]", new TienTeCls().DocTienBangChuEN((long)_DonGiaThue, "Vietnamese dongs").TrimEnd(), SearchOptions.None);

				ctlRTF.Document.ReplaceAll("[DonGiaThue_1m2]", string.Format("{0:n0}", _DonGiaThue_1m2), SearchOptions.None);
                ctlRTF.Document.ReplaceAll("[DonGiaThue_1m2_BC]", new TienTeCls().DocTienBangChu((long)_DonGiaThue_1m2, "").TrimEnd(), SearchOptions.None);

                var _DonGiaPDV = Math.Round(ltChiTiet.Sum(o => o.DienTich * (o.PhiDichVu*o.TyGiaNgoaiTe)).GetValueOrDefault());
                var _DonGiaPDV_1m2 = Math.Round(ltChiTiet.Average(o => o.PhiDichVu).GetValueOrDefault());

                ctlRTF.Document.ReplaceAll("[DonGiaPDV]", string.Format("{0:n0}", _DonGiaPDV), SearchOptions.None);
                ctlRTF.Document.ReplaceAll("[DonGiaPDV_BC]", new TienTeCls().DocTienBangChu((long)_DonGiaPDV, "").TrimEnd(), SearchOptions.None);

                ctlRTF.Document.ReplaceAll("[DonGiaPDV_1m2]", string.Format("{0:n0}", _DonGiaPDV_1m2), SearchOptions.None);
                ctlRTF.Document.ReplaceAll("[DonGiaPDV_1m2_BC]", new TienTeCls().DocTienBangChu((long)_DonGiaPDV_1m2, "").TrimEnd(), SearchOptions.None);

                var tongGiaThue = ltChiTiet.Sum(_ => _.TongGiaThue).GetValueOrDefault();
                var tongTienVAT = ltChiTiet.Sum(_ => _.TienVAT).GetValueOrDefault();
                var tongThanhTien = ltChiTiet.Sum(_ => _.ThanhTien).GetValueOrDefault();

                ctlRTF.Document.ReplaceAll("[TongGiaThue]", string.Format("{0:n0}", tongGiaThue), SearchOptions.None);
                ctlRTF.Document.ReplaceAll("[TongTienVAT]", string.Format("{0:n0}", tongTienVAT), SearchOptions.None);
                ctlRTF.Document.ReplaceAll("[TongThanhTien]", string.Format("{0:n0}", tongThanhTien), SearchOptions.None);
                #endregion

                var format = "Thanh toán đợt {0}: Từ {1:dd/MM/yyyy} Đến  {2:dd/MM/yyyy}. Bên A gửi đề nghị thanh toán cho bên B trước ngày {3:dd/MM/yyyy}"
							  + Environment.NewLine + "Số tiền: {4:#,0.##} VNĐ  x {5:#,0.##} tháng = {6:#,0.##} VNĐ"
							  + Environment.NewLine + "(Bằng chữ: {7})";

				var formatEN = "{0} payment: From {1:dd/MM/yyyy} To  {2:dd/MM/yyyy}. Party A must send payment request before  {3:dd/MM/yyyy}"
							  + Environment.NewLine + "Amount : {4:#,0.##} VNĐ  x {5:#,0.##} months = {6:#,0.##} VND"
							  + Environment.NewLine + "(In words: {7})";

				#region Chi tiết lịch thanh toán
				var ltLichThanhToan = from p in db.ctLichThanhToans
									  orderby p.DotTT
									  where p.MaHD == ID
									  select string.Format(format,
										  //0
										  p.DotTT, 
										  //1
										  p.TuNgay, 
										  //2
										  p.DenNgay,
										  //3
										  p.TuNgay.Value.AddDays(p.ctHopDong.HanTT.GetValueOrDefault()),
										  //4
										  Math.Round(p.SoTienQD.GetValueOrDefault() / p.SoThang.GetValueOrDefault(),0, MidpointRounding.AwayFromZero),
										  //5
										  p.SoThang.GetValueOrDefault(),
										  //6
										  p.SoTienQD,
										  //7
										  new TienTeCls().DocTienBangChu((long)p.SoTienQD, "đồng")
										  );

                //var ltLichThanhToanEN = from p in db.ctLichThanhToans
                //                          orderby p.DotTT
                //                          where p.MaHD == ID
                //                          select string.Format(formatEN,
                //                              //0
                //                              //new TienTeCls().DocSoThuTuEN(p.DotTT.GetValueOrDefault()),
                //                              //1
                //                              p.TuNgay,
                //                              //2
                //                              p.DenNgay,
                //                              //3
                //                              p.TuNgay.Value.AddDays(p.ctHopDong.HanTT.GetValueOrDefault()),
                //                              //4
                //                              Math.Round(p.SoTienQD.GetValueOrDefault() / p.SoThang.GetValueOrDefault(), 0, MidpointRounding.AwayFromZero),
                //                              //5
                //                              p.SoThang.GetValueOrDefault(),
                //                              //6
                //                              p.SoTienQD,
                //                              //7
                //                              new TienTeCls().DocTienBangChuEN((long)p.SoTienQD, "Vietnamese dongs")
                //                              );

                var objHD_ltt = (from hd in db.dvHoaDons
                                 join ltt in db.ctLichThanhToans on hd.LinkID equals ltt.ID
                                 where hd.MaTN == MaTN
                                 & hd.NgayTT.Value.Month == Thang
                                 & hd.NgayTT.Value.Year == Nam
                                 & hd.MaKH == MaKH
                                 & hd.IsDuyet.GetValueOrDefault()
                                 & hd.ConNo.GetValueOrDefault() > 0
                                 select new
                                 {
                                     ltt.MaHD,
                                     hd.TuNgay,
                                     hd.DenNgay,
                                     hd.TienTT,
                                     hd.ThueGTGT,
                                     hd.PhaiThu,
                                 }).FirstOrDefault();
                if (objHD_ltt != null)
                {

                    var NgayTruoc10 = objHD_ltt.TuNgay.Value.AddDays(-10);
                    var NgayTruoc = objHD_ltt.TuNgay.Value.AddDays(-1);

                    ctlRTF.Document.ReplaceAll("[TuNgay10]", string.Format("{0:dd/MM/yyyy}", NgayTruoc10), SearchOptions.None);
                    ctlRTF.Document.ReplaceAll("[DenNgay10]", string.Format("{0:dd/MM/yyyy}", NgayTruoc), SearchOptions.None);

                    ctlRTF.Document.ReplaceAll("[TuNgay]", string.Format("{0:dd/MM/yyyy}", objHD_ltt.TuNgay), SearchOptions.None);
                    ctlRTF.Document.ReplaceAll("[DenNgay]", string.Format("{0:dd/MM/yyyy}", objHD_ltt.DenNgay), SearchOptions.None);
                    ctlRTF.Document.ReplaceAll("[PhiDV]", string.Format("{0:n0}", objHD_ltt.TienTT), SearchOptions.None);
                    ctlRTF.Document.ReplaceAll("[Thue]", string.Format("{0:n0}", objHD_ltt.ThueGTGT), SearchOptions.None);
                    ctlRTF.Document.ReplaceAll("[PhaiThu]", string.Format("{0:n0}", objHD_ltt.PhaiThu), SearchOptions.None);
                    ctlRTF.Document.ReplaceAll("[TienBangChu]", new TienTeCls().DocTienBangChu((long)objHD_ltt.PhaiThu, "").TrimEnd(), SearchOptions.None);
                }

                //var strLTT = string.Join(Environment.NewLine, ltLichThanhToan.ToArray());
                //var strLTT_EN = string.Join(Environment.NewLine, ltLichThanhToanEN.ToArray());
                //ctlRTF.Document.ReplaceAll("[ChiTietLTT]", strLTT, SearchOptions.None);
                //ctlRTF.Document.ReplaceAll("[ChiTietLTT_EN]", strLTT_EN, SearchOptions.None);
                #endregion

                #region Thông tin dự án

                var objTN = db.tnToaNhas.FirstOrDefault(p => p.MaTN == MaTN);
                if (objTN != null)
                {
                    ctlRTF.Document.ReplaceAll("[TenTN]", objTN.TenTN, SearchOptions.None);
                    ctlRTF.Document.ReplaceAll("[DiaChiTN]", objTN.DiaChi, SearchOptions.None);
                    ctlRTF.Document.ReplaceAll("[TaiKhoan]", objTN.ChuTaiKhoan, SearchOptions.None);
                    ctlRTF.Document.ReplaceAll("[SoTaiKhoan]", objTN.SoTaiKhoan, SearchOptions.None);
                    ctlRTF.Document.ReplaceAll("[NganHang]", objTN.NganHang, SearchOptions.None);
                }

                #endregion

				GC.Collect();
            }
            catch (Exception ex)
            {
                // Get stack trace for the exception with source file information
                var st = new StackTrace(ex, true);
                // Get the top stack frame
                var frame = st.GetFrame(0);
                // Get the line number from the stack frame
                var line = frame.GetFileLineNumber();
                MessageBox.Show("Line number error: " + line.ToString());
            }

            return ctlRTF.Document.RtfText;
        }

    }
}
