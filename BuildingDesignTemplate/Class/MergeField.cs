using System;
using System.Collections.Generic;
using System.Data.Linq.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.XtraRichEdit.API.Native;
using Library;

namespace BuildingDesignTemplate.Class
{
    public static class MergeField
    {
        private static MasterDataContext _db = new MasterDataContext();

        public class Field
        {
            public int Index { get; set; }
            public string Name { get; set; }
        }

        class GiuXeItem
        {
            public int? MaLX { get; set; }
            public string TenLX { get; set; }
            public string BienSo { get; set; }
            public int? SoLuong { get; set; }
            public decimal? DonGia { get; set; }
            public decimal? ThanhTien { get; set; }
            public decimal? NoCu { get; set; }
        }

        #region Phiếu thu groupid = 5
        public static string PhieuThu(int id, string rtfText)
        {
            #region Data

            if (id == 0) return rtfText;

            #region Get thu thừa
            //var thuThua = _db.ptPhieuThus.Where(_ => _.ThuThuaId == id).Sum(_ => _.SoTien).GetValueOrDefault();
            //var dienGiaiThuThua = "";
            //if (thuThua > 0)
            //{
            //    dienGiaiThuThua = "Số tiền thu thừa: " + thuThua;
            //}
            #endregion

            var objTien = new TienTeCls();
            var objPt = (from p in _db.ptPhieuThus
                         join kh in _db.tnKhachHangs on p.MaKH equals kh.MaKH
                         join nv in _db.tnNhanViens on p.MaNVN equals nv.MaNV into nhanVien
                         from nv in nhanVien.DefaultIfEmpty()
                         where p.ID == id
                         select new
                         {
                             p.MaTN,
                             p.SoPT,
                             p.NgayThu,
                             p.NguoiNop,
                             kh.KyHieu,
                             p.DiaChiNN,
                             LyDo = p.TienThuThua.GetValueOrDefault() <= 0 ? p.LyDo : string.Format("Đã đóng: {0:n0}; Thanh toán dịch vụ: {1:n0}; Thu thừa: {2:n0}.", p.TongTienDaThu.GetValueOrDefault(), p.SoTien.GetValueOrDefault(), p.TienThuThua.GetValueOrDefault()),
                             SoTien = p.TienThuThua.GetValueOrDefault() <= 0 ? p.SoTien.GetValueOrDefault() : p.TongTienDaThu.GetValueOrDefault(),
                             SoTienBangChu = objTien.DocTienBangChu(p.SoTien.GetValueOrDefault() + p.TienThuThua.GetValueOrDefault(), "đồng chẵn"),
                             HoTenNV = nv != null ? nv.HoTenNV : "",
                             p.MaTKNH,
                             p.MaMB
                         }).FirstOrDefault();
            if (objPt == null) return rtfText;
            var objMb = (from mb in _db.mbMatBangs
                         join tl in _db.mbTangLaus on mb.MaTL equals tl.MaTL
                         join kn in _db.mbKhoiNhas on tl.MaKN equals kn.MaKN
                         join tn in _db.tnToaNhas on kn.MaTN equals tn.MaTN
                         where mb.MaMB == objPt.MaMB
                         select new { tn.TenTN, kn.TenKN }).FirstOrDefault();
            if (objMb == null) return rtfText;
            #endregion

            return RtfPhieuThu(rtfText, objPt.MaTKNH, objMb.TenTN, objMb.TenKN, objPt.SoPT, objPt.NgayThu, objPt.NguoiNop, objPt.DiaChiNN, objPt.SoTien, objPt.SoTienBangChu, objPt.HoTenNV, objPt.LyDo);
        }

        private static string RtfPhieuThu(string rtfText, int? maTkNh, string tenTn, string tenKn, string soPt, DateTime? ngayThu, string nguoiNop, string diaChiNn, decimal soTien, string soTienBangChu, string hoTenNv, string lyDo)
        {
            var ctlRtf = new DevExpress.XtraRichEdit.RichEditControl { RtfText = rtfText };
            ctlRtf.Document.ReplaceAll("[TieuDePhieu]", maTkNh == null ? "PHIẾU THU" : "PHIẾU THU TIỀN CHUYỂN KHOẢN", SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[TenToaNha]", tenTn, SearchOptions.None); //"Ban Quản lý Tòa nhà: " + 
            ctlRtf.Document.ReplaceAll("[KhuVuc]", "Khu quản lý: " + tenKn, SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[SoPhieu]", soPt, SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[NgayChi]", string.Format("Ngày {0:dd} tháng {0:MM} năm {0:yyyy}", ngayThu), SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[NgayThu]", string.Format("Ngày {0:dd} tháng {0:MM} năm {0:yyyy}", ngayThu), SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[NguoiNop]", nguoiNop, SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[DiaChi]", diaChiNn, SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[SoTien]", soTien.ToString("c0"), SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[SoTienBC]", soTienBangChu, SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[NguoiLap]", hoTenNv, SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[DaNhanDu]", string.Format("Đã nhận đủ số tiền (Viết bằng chữ): {0} đồng chẵn.", soTienBangChu), SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[LyDo]", lyDo, SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[BuildingName]", tenTn, SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[NgayIn]", string.Format("Ngày {0:dd} tháng {0:MM} năm {0:yyyy}", ngayThu), SearchOptions.None);
            return ctlRtf.RtfText;
        }
        #endregion

        #region Phiếu chi groupid = 6

        public static string PhieuChi(int id, string rtfText)
        {
            if (id == 0) return rtfText;
            var objTien = new TienTeCls();

            #region Thông tin dữ liệu
            var obj = (from p in _db.pcPhieuChis
                       join nv in _db.tnNhanViens on p.MaNVN equals nv.MaNV into nhanVien
                       from nv in nhanVien.DefaultIfEmpty()
                       where p.ID == id
                       select new
                       {
                           p.MaTN,
                           p.SoPC,
                           p.NgayChi,
                           p.NguoiNhan,
                           p.DiaChiNN,
                           p.LyDo,
                           p.SoTien,
                           SoTien_BangChu = objTien.DocTienBangChu(p.SoTien.Value, "đồng chẵn"),
                           p.ChungTuGoc,
                           HoTenNV = nv != null ? nv.HoTenNV : "",
                           p.MaTKNH
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
            ctlRtf.Document.ReplaceAll("[TieuDePhieu]", maTknh == null ? "PHIẾU CHI" : "PHIẾU CHI TIỀN CHUYỂN KHOẢN", SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[TenTN]", tenTn, SearchOptions.None); //"Ban Quản lý Tòa nhà: " + 
            ctlRtf.Document.ReplaceAll("[SoPC]", soPc, SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[NguoiNhan]", nguoiNhan, SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[NgayChi]", string.Format("Ngày {0:dd} tháng {0:MM} năm {0:yyyy}", ngayChi), SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[DiaChiNN]", diaChiNn, SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[LyDo]", lyDo, SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[SoTien]", string.Format("{0:n0} VNĐ", soTien), SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[SoTien_BangChu]", soTienBangChu, SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[HoTenNV]", hoTenNv, SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[NgayIn]", string.Format("Ngày {0:dd} tháng {0:MM} năm {0:yyyy}", ngayChi), SearchOptions.None);
            return ctlRtf.RtfText;
        }

        #endregion

        #region Thông báo phí group id = 1

        public static string ThongBaoPhi(byte maTn, int thang, int nam, int maKh, int maMb, List<int> maLdvs,
            int maTkNh, string rtfText)
        {
            if (maKh == 0) return rtfText;


            #region Dữ liệu
            // tòa nhà
            var objTn = _db.tnToaNhas.Where(_ => _.MaTN == maTn).Select(_ => new { _.TenTN, _.CongTyQuanLy, _.Logo })
                .FirstOrDefault();
            if (objTn == null) return rtfText;

            //Thong tin khach hang
            var objKh = (from mb in _db.mbMatBangs
                         join kh in _db.tnKhachHangs on mb.MaKH equals kh.MaKH
                         where mb.MaKH == maKh & mb.MaMB == maMb
                         select new
                         {
                             TenKH = kh.IsCaNhan == true ? (kh.HoKH + " " + kh.TenKH) : kh.CtyTen,
                             mb.MaSoMB,
                             kh.ThuTruoc,
                             kh.MaPhu
                         }).FirstOrDefault();
            if (objKh == null) return rtfText;

            //Thong tin tai khoan
            var objTk = (from tk in _db.nhTaiKhoans
                         join nh in _db.nhNganHangs on tk.MaNH equals nh.ID
                         where tk.ID == maTkNh
                         select new { tk.ChuTK, tk.SoTK, nh.TenNH, ChiNhanhNH = tk.DienGiai }).FirstOrDefault();
            if (objTk == null) return rtfText;

            //var ctlRtf = new DevExpress.XtraRichEdit.RichEditControl { RtfText = rtfText };

            var ngay = new DateTime(nam, thang, 1);
            //Du lieu hoa don
            var ltLoaiDichVu = (from hd in _db.dvHoaDons
                                join ldv in _db.dvLoaiDichVus on hd.MaLDV equals ldv.ID
                                where hd.MaLDV != 23 & hd.IsDuyet == true & hd.MaKH == maKh & hd.NgayTT.Value.Month == thang
                                & hd.NgayTT.Value.Year == nam
                                & (maLdvs.Contains(hd.MaLDV.Value) | maLdvs.Count == 0)
                                & hd.PhaiThu > 0
                                & hd.ConNo > 0  //moi mo 5/11/2016
                                group hd by new { ldv.STT, hd.MaLDV, ldv.TenHienThi, ldv.TenTA } into gr
                                orderby gr.Key.STT
                                select new
                                {
                                    gr.Key.STT,
                                    gr.Key.MaLDV,
                                    TenLDV = gr.Key.TenHienThi,
                                    gr.Key.TenTA,
                                    SoTien = (decimal)gr.Key.MaLDV == 49 ? -gr.Sum(p => p.PhaiThu) : gr.Sum(p => p.ConNo),
                                }).ToList();

            var ltData = (from hd in _db.dvHoaDons
                          join ldv in _db.dvLoaiDichVus on hd.MaLDV equals ldv.ID
                          where hd.MaKH == maKh & hd.NgayTT.Value.Month == thang & hd.NgayTT.Value.Year == nam
                          & hd.ConNo.GetValueOrDefault() > 0
                          & (maLdvs.Contains(hd.MaLDV.Value) | maLdvs.Count == 0)
                          & (hd.PhaiThu.GetValueOrDefault()
                          - (_db.SoQuy_ThuChis.Where(p => p.LinkID == hd.ID & p.TableName == "dvHoaDon").Sum(p => p.DaThu + p.KhauTru)).GetValueOrDefault()) > 0
                          & hd.IsDuyet == true
                          group hd by new { hd.MaLDV, ldv.TenHienThi, ldv.TenTA } into gr
                          select new
                          {
                              gr.Key.MaLDV,
                              TenLDV = gr.Key.TenHienThi,
                              gr.Key.TenTA,
                              SoTien = gr.Sum(p => p.ConNo)
                          }).ToList();

            var noCu = (from hd in _db.dvHoaDons
                        where hd.MaKH == maKh
                              & SqlMethods.DateDiffDay(hd.NgayTT, ngay) > 0
                              & (maLdvs.Contains(hd.MaLDV.Value) | maLdvs.Count == 0) & hd.IsDuyet == true

                        select (hd.PhaiThu - _db.SoQuy_ThuChis.Where(p => p.LinkID == hd.ID & p.MaKH == maKh & p.TableName == "dvHoaDon").Sum(p => p.DaThu + p.KhauTru - p.ThuThua).GetValueOrDefault())).Sum().GetValueOrDefault();
            if (noCu < 0)
            {
                noCu = 0;
            }


            var phatSinh = ltLoaiDichVu.Sum(p => p.SoTien).GetValueOrDefault();
            var tongTien = Convert.ToInt64(Math.Round(phatSinh + noCu, 0));
            var ngayIn = Library.Common.GetDateTimeSystem();
            var thangTb = string.Format("{0:00} - {1}", thang, nam);

            var thanhToan = string.Format("{0}. Số thanh toán Payment:", RomanNumerals.ToRoman((int)(ltData.Count() + 1)));
            string bangChu = new TienTeCls().DocTienBangChu((long)tongTien);
            #endregion

            // merge rtfTong
            rtfText = RtfThongBaoPhi(rtfText, ngayIn, thang, nam, objKh.MaPhu ?? "", objKh.TenKH ?? "",
                objKh.MaSoMB ?? "", objTn.TenTN ?? "",
                objTk.ChuTK ?? "", objTk.SoTK ?? "", objTk.TenNH ?? "",
                objTk.ChiNhanhNH ?? "", thangTb, tongTien, noCu, thanhToan, tongTien, phatSinh, bangChu);


            var stt = 1;

            foreach (var item in ltData)
            {
                switch (item.MaLDV)
                {
                    case 2:
                        // tiền thuê
                        // gọi hàm cho tiền thuê
                        rtfText = RtfTienThue(rtfText, maTn, maKh, thang, nam);
                        rtfText = RtfSttLoaiDichVu(rtfText, item.MaLDV, thang, nam, stt, maTn, maKh, "[TBP.TT]",
                            item.TenLDV, item.TenTA);
                        break;
                    case 3:
                        rtfText = RtfTienSuaChua(rtfText, maTn, maKh, thang, nam);
                        rtfText = RtfSttLoaiDichVu(rtfText, item.MaLDV, thang, nam, stt, maTn, maKh, "[TBP.SC]",
                            item.TenLDV, item.TenTA);
                        break;
                    case 4:
                        rtfText = RtfTienDatCoc(rtfText, maTn, maKh, thang, nam);
                        rtfText = RtfSttLoaiDichVu(rtfText, item.MaLDV, thang, nam, stt, maTn, maKh, "[TBP.DC]",
                            item.TenLDV, item.TenTA);
                        break;
                    case 5:
                        rtfText = RtfTienDien3Pha(rtfText, maTn, maKh, thang, nam);
                        rtfText = RtfSttLoaiDichVu(rtfText, item.MaLDV, thang, nam, stt, maTn, maKh, "[TBP.D3P]",
                            item.TenLDV, item.TenTA);
                        break;
                    case 6:
                        rtfText = RtfPhiXe(rtfText, maTn, maKh, thang, nam);
                        rtfText = RtfSttLoaiDichVu(rtfText, item.MaLDV, thang, nam, stt, maTn, maKh, "[TBP.CAR]",
                            item.TenLDV, item.TenTA);
                        break;
                    case 8:
                        rtfText = RtfTienDien(rtfText, maTn, maKh, thang, nam);
                        rtfText = RtfSttLoaiDichVu(rtfText, item.MaLDV, thang, nam, stt, maTn, maKh, "[TBP.TD]",
                            item.TenLDV, item.TenTA);
                        break;
                    case 9:
                        rtfText = RtfTienNuoc(rtfText, maTn, maKh, thang, nam);
                        rtfText = RtfSttLoaiDichVu(rtfText, item.MaLDV, thang, nam, stt, maTn, maKh, "[TBP.TN]",
                            item.TenLDV, item.TenTA);
                        break;
                    case 10:
                        rtfText = RtfTienGas(rtfText, maTn, maKh, thang, nam);
                        rtfText = RtfSttLoaiDichVu(rtfText, item.MaLDV, thang, nam, stt, maTn, maKh, "[TBP.GAS]",
                            item.TenLDV, item.TenTA);
                        break;
                    case 11:
                        rtfText = RtfDieuHoaNgoaiGio(rtfText, maTn, maKh, thang, nam);
                        rtfText = RtfSttLoaiDichVu(rtfText, item.MaLDV, thang, nam, stt, maTn, maKh, "[TBP.DDH]",
                            item.TenLDV, item.TenTA);
                        break;
                    case 60:
                        rtfText = RtfNuocNong(rtfText, maTn, maKh, thang, nam);
                        rtfText = RtfSttLoaiDichVu(rtfText, item.MaLDV, thang, nam, stt, maTn, maKh, "[TBP.NN]",
                            item.TenLDV, item.TenTA);
                        break;
                    case 22:
                        rtfText = RtfNuocSinhHoat(rtfText, maTn, maKh, thang, nam);
                        rtfText = RtfSttLoaiDichVu(rtfText, item.MaLDV, thang, nam, stt, maTn, maKh, "[TBP.NSH]",
                            item.TenLDV, item.TenTA);
                        break;
                    case 49:
                        rtfText = RtfDoanhThuGiamTru(rtfText, maTn, maKh, thang, nam, (int)item.MaLDV);
                        rtfText = RtfSttLoaiDichVu(rtfText, item.MaLDV, thang, nam, stt, maTn, maKh, "[TBP.DTGT]",
                            item.TenLDV, item.TenTA);
                        break;
                    case 13:
                        rtfText = RtfPhiQuanLy(rtfText, maTn, maKh, thang, nam, (int)item.MaLDV);
                        rtfText = RtfSttLoaiDichVu(rtfText, item.MaLDV, thang, nam, stt, maTn, maKh, "[TBP.PQL]",
                            item.TenLDV, item.TenTA);
                        break;
                    default:
                        rtfText = RtfDichVuCoBan(rtfText, maTn, maKh, thang, nam, (int)item.MaLDV);
                        rtfText = RtfSttLoaiDichVu(rtfText, item.MaLDV, thang, nam, stt, maTn, maKh, "[TBP.DVCB]",
                            item.TenLDV, item.TenTA);
                        break;
                }

                stt++;
            }

            rtfText = RtfDeleteFieldFinish(rtfText);
            return rtfText;
        }

        private static string RtfThongBaoPhi(string rtfText, DateTime ngayIn, int thang, int nam, string maPhu, string tenKh, string maSoMb, string tenTn, string chuTk, string soTk, string tenNh, string chiNhanhNh, string thangTb, decimal tongThang, decimal noTruoc, string tongThanhToan, decimal TongTien, decimal phatSinh, string BangChu)
        {
            var ctlRtf = new DevExpress.XtraRichEdit.RichEditControl { RtfText = rtfText };
            ctlRtf.Document.ReplaceAll("[TBP.NgayIn]", ngayIn.ToString("dd/MM/yyyy"), SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[TBP.Thang]", thang.ToString(), SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[TBP.MaThucTe]", maPhu.ToString(), SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[TBP.Nam]", nam.ToString(), SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[TBP.Year]", nam.ToString(), SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[TBP.TenKH]", tenKh ?? "", SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[TBP.MaSoHopDong]", maSoMb ?? "", SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[TBP.MaSoMB]", maSoMb ?? "", SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[TBP.TenToaNha]", tenTn ?? "", SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[TBP.ThangTB]", thangTb, SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[TBP.Month]", Commoncls.GetMonth(thang) ?? "", SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[TBP.SoHopDong]", "", SearchOptions.None);

            ctlRtf.Document.ReplaceAll("[TBP.ChuTK]", chuTk, SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[TBP.SoTK]", soTk, SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[TBP.TenNH]", tenNh, SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[TBP.ChiNhanhNH]", chiNhanhNh, SearchOptions.None);

            ctlRtf.Document.ReplaceAll("[TBP.TongThang]", string.Format("{0:#,0}  VNĐ", tongThang > 0 ? tongThang : 0), SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[TBP.NoTruoc]", string.Format("{0:#,0}  VNĐ", noTruoc), SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[TBP.TongThanhToan]", tongThanhToan, SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[TBP.TongTienThanhToan]", string.Format("{0:#,0}  VNĐ", TongTien), SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[TBP.PhatSinh]", string.Format("{0:#,0}  VNĐ", TongTien), SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[TBP.TongTienBangChu]", BangChu, SearchOptions.None);

            return ctlRtf.RtfText;
        }

        private static string RtfSttLoaiDichVu(string rtfText, int? maLdv, int thang, int nam, int stt, byte maTn, int maKh, string fieldName, string tenLdv, string tenTa)
        {
            var ctlRtf = new DevExpress.XtraRichEdit.RichEditControl { RtfText = rtfText };

            var thangDv =
                (maLdv == 5 || maLdv == 8 || maLdv == 9 || maLdv == 10 || maLdv == 11 || maLdv == 20 || maLdv == 22)
                    ? thang - 1
                    : thang;
            var namDv = nam;
            if (thangDv == 0)
            {
                thangDv = 12;
                namDv--;
            }

            if (maLdv == 9)
            {
                var objNuoc = (from hd in _db.dvHoaDons
                               join tn in _db.dvNuocs on new { hd.TableName, hd.LinkID } equals new { TableName = "dvNuoc", LinkID = (int?)tn.ID }
                               where hd.MaTN == maTn & hd.MaKH == maKh & hd.NgayTT.Value.Month == thangDv & hd.NgayTT.Value.Year == namDv
                               select new
                               {
                                   tn.ID,
                                   tn.ChiSoCu,
                                   tn.ChiSoMoi,
                                   tn.TuNgay,
                                   tn.DenNgay,
                                   SoTieuThu = tn.SoTieuThu.GetValueOrDefault() + tn.SoTieuThuDHCu.GetValueOrDefault(),
                                   SoUuDai = (from ud in _db.dvNuocUuDais where ud.MaMB == tn.MaMB select ud.SoNguoi).FirstOrDefault(),
                                   ConNo = hd.PhaiThu - _db.ptChiTietPhieuThus.Where(p => p.LinkID == hd.ID).Sum(p => p.SoTien).GetValueOrDefault()
                                                      - _db.ktttChiTiets.Where(p => p.LinkID == hd.ID).Sum(p => p.SoTien).GetValueOrDefault()
                               }).FirstOrDefault();
                if (objNuoc != null)
                    ctlRtf.Document.ReplaceAll(fieldName, string.Format("{0}. {1} {2} (Từ/ From {3:dd/MM/yyyy} Đến/ to {4:dd/MM/yyyy})", stt, tenLdv, tenTa, objNuoc.TuNgay, objNuoc.DenNgay), SearchOptions.None);
                else
                    ctlRtf.Document.ReplaceAll(fieldName, string.Format("{0}. {1} {2} (Tháng {3:00}/{4})", stt, tenLdv, tenTa, thangDv, namDv), SearchOptions.None);
            }
            else
            {
                ctlRtf.Document.ReplaceAll(fieldName, string.Format("{0}. {1} {2} (Tháng {3:00}/{4})", stt, tenLdv, tenTa, thangDv, namDv), SearchOptions.None);
            }

            return ctlRtf.RtfText;
        }

        public static Field FindTable(Document document, string key)
        {
            var tableList = (from p in document.Tables
                             select new { cellName = document.GetText(p.Rows[0].Cells[0].Range).Replace(" ", "") }).AsEnumerable()
                .Select((t, index) => new Field { Index = index, Name = t.cellName }).ToList();
            var tableIndex = tableList.FirstOrDefault(_ => _.Name.Contains(key));
            return tableIndex;
        }

        private static string RtfTienThue(string rtfText, byte maTn, int maKh, int thang, int nam)
        {
            var obj = (from hd in _db.dvHoaDons
                       join ltt in _db.ctLichThanhToans on new { hd.TableName, hd.LinkID } equals new { TableName = "ctLichThanhToan", LinkID = (int?)ltt.ID }
                       join ct in _db.ctChiTiets on ltt.MaHD equals ct.MaHDCT
                       join cthd in _db.ctHopDongs on ct.MaHDCT equals cthd.ID
                       join mb in _db.mbMatBangs on ct.MaMB equals mb.MaMB
                       join tl in _db.mbTangLaus on mb.MaTL equals tl.MaTL
                       where hd.MaTN == maTn & hd.MaLDV == 2 & hd.MaKH == maKh & hd.NgayTT.Value.Month == thang & hd.NgayTT.Value.Year == nam & hd.ConNo.GetValueOrDefault() > 0
                       select new
                       {
                           ViTri = tl.TenTL,
                           ltt.TuNgay,
                           ltt.DenNgay,
                           SoThang = hd.KyTT,
                           GiaChoThue = ct.DonGia * cthd.TyGia,
                           PhiDichVu = ct.PhiDichVu * cthd.TyGia,
                           ct.DienTich,
                           ThanhTien = ct.ThanhTien * cthd.TyGia * hd.KyTT,
                           ltt.DienGiai
                       }).ToList();

            var ctlRtf = new DevExpress.XtraRichEdit.RichEditControl { RtfText = rtfText };
            var document = ctlRtf.Document;

            var field = FindTable(document, "[TBP.TT]");

            if (field == null) return ctlRtf.RtfText;
            var table = document.Tables[field.Index];
            var rField = table.Rows[2];
            var ltFieldName = (from cell in rField.Cells let cellName = document.GetText(cell.Range).Replace(Environment.NewLine, "").Replace(" ", "") select new Field { Index = cell.Index, Name = cellName }).ToList();

            var index = 2;
            var stt = 1;
            foreach (var r in obj)
            {
                var row = table.Rows.InsertBefore(index);
                // get all field in row
                foreach (var f in ltFieldName)
                {
                    var cell = row[f.Index];

                    if (f.Name.Contains("[TT.STT]")) document.InsertSingleLineText(cell.Range.Start, stt.ToString());
                    if (f.Name.Contains("[TT.ViTri]")) document.InsertSingleLineText(cell.Range.Start, r.ViTri);
                    if (f.Name.Contains("[TT.TuNgay]")) document.InsertSingleLineText(cell.Range.Start, string.Format("{0:dd/MM/yyyy}", r.TuNgay));
                    if (f.Name.Contains("[TT.DenNgay]")) document.InsertSingleLineText(cell.Range.Start, string.Format("{0:dd/MM/yyyy}", r.DenNgay));
                    if (f.Name.Contains("[TT.SoThang]")) document.InsertSingleLineText(cell.Range.Start, string.Format("{0:#,0.##}", r.SoThang));
                    if (f.Name.Contains("[TT.GiaChoThue]")) document.InsertSingleLineText(cell.Range.Start, string.Format("{0:#,0.##}", r.GiaChoThue));
                    if (f.Name.Contains("[TT.PhiDichVu]")) document.InsertSingleLineText(cell.Range.Start, string.Format("{0:#,0.##}", r.PhiDichVu));
                    if (f.Name.Contains("[TT.DienTich]")) document.InsertSingleLineText(cell.Range.Start, string.Format("{0:#,0.##} m2", r.DienTich));
                    if (f.Name.Contains("[TT.ThanhTien]")) document.InsertSingleLineText(cell.Range.Start, string.Format("{0:#,0.##} m2", r.ThanhTien));
                }

                index += 1;
                stt++;
            }

            table.Rows.RemoveAt(index);

            return ctlRtf.RtfText;
        }

        private static string RtfTienSuaChua(string rtfText, byte maTn, int maKh, int thang, int nam)
        {
            var obj = (from hd in _db.dvHoaDons
                       join ltt in _db.ctLichThanhToans on new { hd.TableName, hd.LinkID } equals new { TableName = "ctLichThanhToan", LinkID = (int?)ltt.ID }
                       join ct in _db.ctChiTiets on ltt.MaHD equals ct.MaHDCT
                       join cthd in _db.ctHopDongs on ct.MaHDCT equals cthd.ID
                       join mb in _db.mbMatBangs on ct.MaMB equals mb.MaMB
                       join tl in _db.mbTangLaus on mb.MaTL equals tl.MaTL
                       where hd.MaTN == maTn & hd.MaLDV == 3 & hd.MaKH == maKh & hd.NgayTT.Value.Month == thang & hd.NgayTT.Value.Year == nam & hd.ConNo.GetValueOrDefault() > 0
                       select new
                       {
                           ViTri = tl.TenTL,
                           ltt.TuNgay,
                           ltt.DenNgay,
                           SoThang = hd.KyTT,
                           PhiSuaChua = ct.PhiSuaChua * cthd.TyGia,
                           ct.DienTich,
                           ThanhTien = ct.PhiSuaChua * ct.DienTich * cthd.TyGia * hd.KyTT,
                           ltt.DienGiai
                       }).ToList();

            var ctlRtf = new DevExpress.XtraRichEdit.RichEditControl { RtfText = rtfText };
            var document = ctlRtf.Document;
            var field = FindTable(document, "[TBP.SC]");

            if (field != null)
            {
                var table = document.Tables[field.Index];
                var ltFieldName = new List<Field>();
                var rField = table.Rows[2];
                foreach (var cell in rField.Cells)
                {
                    var cellName = document.GetText(cell.Range).Replace(Environment.NewLine, "").Replace(" ", "");
                    ltFieldName.Add(new Field { Index = cell.Index, Name = cellName });
                }

                var index = 2;
                var stt = 1;
                foreach (var r in obj)
                {
                    var row = table.Rows.InsertBefore(index);
                    // get all field in row
                    foreach (var f in ltFieldName)
                    {
                        var cell = row[f.Index];
                        if (f.Name.Contains("[SC.STT]")) document.InsertSingleLineText(cell.Range.Start, string.Format("{0:#}", stt));
                        if (f.Name.Contains("[SC.ViTri]")) document.InsertSingleLineText(cell.Range.Start, r.ViTri);
                        if (f.Name.Contains("[SC.TuNgay]")) document.InsertSingleLineText(cell.Range.Start, string.Format("{0:dd/MM/yyyy}", r.TuNgay));
                        if (f.Name.Contains("[SC.DenNgay]")) document.InsertSingleLineText(cell.Range.Start, string.Format("{0:dd/MM/yyyy}", r.DenNgay));
                        if (f.Name.Contains("[SC.SoThang]")) document.InsertSingleLineText(cell.Range.Start, string.Format("{0:#,0.##}", r.SoThang));
                        if (f.Name.Contains("[SC.PhiSuaChua]")) document.InsertSingleLineText(cell.Range.Start, string.Format("{0:#,0.##}", r.SoThang));
                        if (f.Name.Contains("[SC.DienTich]")) document.InsertSingleLineText(cell.Range.Start, string.Format("{0:#,0.##} m2", r.DienTich));
                        if (f.Name.Contains("[SC.ThanhTien]")) document.InsertSingleLineText(cell.Range.Start, string.Format("{0:#,0.##} m2", r.ThanhTien));
                    }

                    index += 1;
                    stt++;
                }

                table.Rows.RemoveAt(index);
            }

            return ctlRtf.RtfText;
        }

        private static string RtfTienDatCoc(string rtfText, byte maTn, int maKh, int thang, int nam)
        {
            var obj = (from hd in _db.dvHoaDons
                       join ltt in _db.ctLichThanhToans on new { hd.TableName, hd.LinkID } equals new { TableName = "ctLichThanhToan", LinkID = (int?)ltt.ID }
                       join cthd in _db.ctHopDongs on ltt.MaHD equals cthd.ID
                       where hd.MaTN == maTn & hd.MaLDV == 4 & hd.MaKH == maKh & hd.NgayTT.Value.Month == thang & hd.NgayTT.Value.Year == nam & hd.ConNo.GetValueOrDefault() > 0
                       select new
                       {
                           SoHD = cthd.SoHDCT,
                           ltt.DotTT,
                           hd.NgayTT,
                           SoTien = hd.ConNo,
                           ltt.DienGiai
                       }).ToList();

            var ctlRtf = new DevExpress.XtraRichEdit.RichEditControl { RtfText = rtfText };
            var document = ctlRtf.Document;
            var field = FindTable(document, "[TBP.DC]");

            if (field == null) return ctlRtf.RtfText;
            var table = document.Tables[field.Index];
            var rField = table.Rows[2];
            var ltFieldName = (from cell in rField.Cells let cellName = document.GetText(cell.Range).Replace(Environment.NewLine, "").Replace(" ", "") select new Field { Index = cell.Index, Name = cellName }).ToList();

            var index = 2;
            var stt = 1;
            foreach (var r in obj)
            {
                var row = table.Rows.InsertBefore(index);
                // get all field in row
                foreach (var f in ltFieldName)
                {
                    var cell = row[f.Index];
                    if (f.Name.Contains("[DC.STT]")) document.InsertSingleLineText(cell.Range.Start, string.Format("{0:#}", stt));
                    if (f.Name.Contains("[DC.SoHopDong]")) document.InsertSingleLineText(cell.Range.Start, r.SoHD);
                    if (f.Name.Contains("[DC.Dot]")) document.InsertSingleLineText(cell.Range.Start, string.Format("{0:#,0.##}", r.DotTT));
                    if (f.Name.Contains("[DC.NgayThanhToan]")) document.InsertSingleLineText(cell.Range.Start, string.Format("{0:dd/MM/yyyy}", r.NgayTT));
                    if (f.Name.Contains("[DC.SoTien]")) document.InsertSingleLineText(cell.Range.Start, string.Format("{0:#,0.##}", r.SoTien));
                    if (f.Name.Contains("[DC.GhiChu]")) document.InsertSingleLineText(cell.Range.Start, r.DienGiai);
                }

                index += 1;
                stt++;
            }

            table.Rows.RemoveAt(index);

            return ctlRtf.RtfText;
        }

        private static string RtfTienDien3Pha(string rtfText, byte maTn, int maKh, int thang, int nam)
        {
            var obj = (from hd in _db.dvHoaDons
                       join td in _db.dvDien3Phas on new { hd.TableName, hd.LinkID } equals new { TableName = "dvDien3Pha", LinkID = (int?)td.ID }
                       join ct in _db.dvDien3PhaChiTiets on td.ID equals ct.MaDien
                       join dm in _db.dvDien3PhaDinhMucs on ct.MaDM equals dm.ID
                       join mb in _db.mbMatBangs on td.MaMB equals mb.MaMB
                       join tl in _db.mbTangLaus on mb.MaTL equals tl.MaTL
                       where hd.MaTN == maTn & hd.MaKH == maKh & hd.NgayTT.Value.Month == thang & hd.NgayTT.Value.Year == nam
                       orderby mb.MaSoMB, dm.STT
                       select new
                       {
                           td.MaMB,
                           mb.MaSoMB,
                           tl.TenTL,
                           td.TuNgay,
                           td.DenNgay,
                           td.SoTieuThu,
                           TienDien = td.ThanhTien,
                           td.TyLeVAT,
                           td.TienVAT,
                           dm.TenDM,
                           ct.ChiSoCu,
                           ct.ChiSoMoi,
                           ct.SoLuong,
                           ct.DonGia,
                           ct.ThanhTien
                       }).ToList();

            var ctlRtf = new DevExpress.XtraRichEdit.RichEditControl { RtfText = rtfText };
            ctlRtf.Document.ReplaceAll("[D3P.CanHoSo]", obj.First().MaSoMB, SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[D3P.ViTri]", obj.First().TenTL, SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[D3P.TuNgay]", string.Format("{0:dd/MM/yyyy}", obj.First().TuNgay), SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[D3P.DenNgay]", string.Format("{0:dd/MM/yyyy}", obj.First().DenNgay), SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[D3P.SoTieuThu]", string.Format("{0:#,0.##}", obj.First().SoTieuThu), SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[D3P.TongTien]", string.Format("{0:#,0.##}", obj.First().TienDien), SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[D3P.Thue]", string.Format("{0:#,0.##}", obj.First().TienVAT), SearchOptions.None);

            var document = ctlRtf.Document;
            var field = FindTable(document, "[TBP.D3P]");

            if (field == null) return ctlRtf.RtfText;
            var table = document.Tables[field.Index];
            var rField = table.Rows[2];
            var ltFieldName = (from cell in rField.Cells let cellName = document.GetText(cell.Range).Replace(Environment.NewLine, "").Replace(" ", "") select new Field { Index = cell.Index, Name = cellName }).ToList();

            var index = 2;
            var stt = 1;
            foreach (var r in obj)
            {
                var row = table.Rows.InsertBefore(index);
                // get all field in row
                foreach (var f in ltFieldName)
                {
                    var cell = row[f.Index];
                    if (f.Name.Contains("[D3P.STT]")) document.InsertSingleLineText(cell.Range.Start, string.Format("{0:#}", stt));
                    if (f.Name.Contains("[D3P.DinhMuc]")) document.InsertSingleLineText(cell.Range.Start, r.TenDM);
                    if (f.Name.Contains("[D3P.ChiSoCu]")) document.InsertSingleLineText(cell.Range.Start, string.Format("{0:#,0.##}", r.ChiSoCu));
                    if (f.Name.Contains("[D3P.ChiSoMoi]")) document.InsertSingleLineText(cell.Range.Start, string.Format("{0:#,0.##}", r.ChiSoMoi));
                    if (f.Name.Contains("[D3P.CongSuat]")) document.InsertSingleLineText(cell.Range.Start, string.Format("{0:#,0.##}", r.SoLuong));
                    if (f.Name.Contains("[D3P.DonGia]")) document.InsertSingleLineText(cell.Range.Start, string.Format("{0:#,0.##}", r.DonGia));
                    if (f.Name.Contains("[D3P.ThanhTien]")) document.InsertSingleLineText(cell.Range.Start, string.Format("{0:#,0.##}", r.ThanhTien));
                    if (f.Name.Contains("[D3P.GhiChu]")) document.InsertSingleLineText(cell.Range.Start, "");
                }

                index += 1;
                stt++;
            }

            table.Rows.RemoveAt(index);

            return ctlRtf.RtfText;
        }

        private static string RtfPhiXe(string rtfText, byte maTn, int maKh, int thang, int nam)
        {
            var ngayTb = new DateTime(nam, thang, 1);
            var ltGiuXete = (from tx in _db.dvgxTheXes
                             join lx in _db.dvgxLoaiXes on tx.MaLX equals lx.MaLX into dslx
                             from lx in dslx.DefaultIfEmpty()
                             join dv in _db.dvHoaDons on tx.ID equals dv.LinkID
                             where tx.MaTN == maTn & tx.MaKH == maKh
                                                   & dv.PhaiThu.GetValueOrDefault()
                                                   - _db.SoQuy_ThuChis
                                                       .Where(p => p.LinkID == dv.ID & p.MaKH == maKh & p.MaTN == maTn &
                                                                   p.TableName == "dvHoaDon").Sum(p => p.DaThu + p.KhauTru)
                                                   .GetValueOrDefault() > 0
                                                   & dv.NgayTT.Value.Month == thang & dv.NgayTT.Value.Year == nam &
                                                   dv.IsDuyet == true & dv.MaLDV == 6
                             group tx by new { tx.MaLX, lx.TenLX, tx.GiaThang }
                into gr
                             select new GiuXeItem()
                             {
                                 MaLX = gr.Key.MaLX,
                                 TenLX = gr.Key.TenLX,
                                 BienSo = "",
                                 SoLuong = gr.Count(),
                                 DonGia = gr.Key.GiaThang,
                                 ThanhTien = gr.Count() * gr.Key.GiaThang,
                                 NoCu = (from hd in _db.dvHoaDons
                                         join tx in _db.dvgxTheXes on hd.LinkID equals tx.ID
                                         where hd.MaKH == maKh & hd.IsDuyet.GetValueOrDefault() == true & tx.MaLX == gr.Key.MaLX
                                               & SqlMethods.DateDiffDay(hd.NgayTT, ngayTb) > 0
                                               & (hd.PhaiThu.GetValueOrDefault()

                                                  - (from ct in _db.SoQuy_ThuChis
                                                     where ct.TableName == "dvHoaDon" & ct.LinkID == hd.ID
                                                                     & SqlMethods.DateDiffDay(ct.NgayPhieu, ngayTb) > 0
                                                     select ct.KhauTru + ct.DaThu).Sum().GetValueOrDefault()) != 0
                                               & hd.MaLDV == 6
                                         select hd.PhaiThu
                                                - (from ct in _db.SoQuy_ThuChis
                                                   where ct.TableName == "dvHoaDon" & ct.LinkID == hd.ID
                                                                   & SqlMethods.DateDiffDay(ct.NgayPhieu, ngayTb) > 0
                                                   select ct.DaThu + ct.KhauTru).Sum().GetValueOrDefault()).Sum().GetValueOrDefault()
                             }).ToList();
            foreach (var gx in ltGiuXete)
            {
                var ltBienSo = (from tx in _db.dvgxTheXes
                                where tx.MaTN == maTn & tx.MaKH == maKh & tx.NgungSuDung == false & tx.MaLX == gx.MaLX & tx.GiaThang == gx.DonGia
                                select tx.BienSo).ToList();

                foreach (var bs in ltBienSo)
                    if (!string.IsNullOrEmpty(bs))
                        gx.BienSo += bs + "; ";

                gx.BienSo = gx.BienSo.Trim(' ').Trim(';');
            }

            var tienTt = (from hd in _db.dvHoaDons
                          join ltt in _db.dvgxLichThanhToans on new { hd.TableName, hd.LinkID } equals new { TableName = "dvgxLichThanhToan", LinkID = (int?)ltt.ID }
                          where hd.MaTN == maTn & hd.MaLDV == 6 & hd.MaKH == maKh & hd.NgayTT.Value.Month == thang & hd.NgayTT.Value.Year == nam & hd.ConNo.GetValueOrDefault() > 0
                          select new
                          {
                              PhaiThu = hd.PhaiThu
                                        - _db.SoQuy_ThuChis.Where(p => p.LinkID == hd.ID).Sum(p => p.DaThu + p.KhauTru).GetValueOrDefault()
                          }).Sum(p => p.PhaiThu).GetValueOrDefault();

            var ctlRtf = new DevExpress.XtraRichEdit.RichEditControl { RtfText = rtfText };
            ctlRtf.Document.ReplaceAll("[CAR.TongTien]", string.Format("{0:#,0.##}", tienTt), SearchOptions.None);

            var document = ctlRtf.Document;
            var field = FindTable(document, "[TBP.CAR]");

            if (field == null) return ctlRtf.RtfText;
            var table = document.Tables[field.Index];
            var rField = table.Rows[2];
            var ltFieldName = (from cell in rField.Cells let cellName = document.GetText(cell.Range).Replace(Environment.NewLine, "").Replace(" ", "") select new Field { Index = cell.Index, Name = cellName }).ToList();

            var index = 2;
            var stt = 1;
            foreach (var r in ltGiuXete)
            {
                var row = table.Rows.InsertBefore(index);
                // get all field in row
                foreach (var f in ltFieldName)
                {
                    var cell = row[f.Index];
                    if (f.Name.Contains("[CAR.STT]")) document.InsertSingleLineText(cell.Range.Start, string.Format("{0:#}", stt));
                    if (f.Name.Contains("[CAR.TheXe]")) document.InsertSingleLineText(cell.Range.Start, r.TenLX);
                    if (f.Name.Contains("[CAR.DonGia]")) document.InsertSingleLineText(cell.Range.Start, string.Format("{0:#,0.##}", r.DonGia));
                    if (f.Name.Contains("[CAR.BienSoXe]")) document.InsertSingleLineText(cell.Range.Start, r.BienSo);
                    if (f.Name.Contains("[CAR.SoLuongXe]")) document.InsertSingleLineText(cell.Range.Start, string.Format("{0:#,0.##}", r.SoLuong));
                    if (f.Name.Contains("[CAR.NoCu]")) document.InsertSingleLineText(cell.Range.Start, string.Format("{0:#,0.##}", r.NoCu));
                    if (f.Name.Contains("[CAR.GhiChu]")) document.InsertSingleLineText(cell.Range.Start, "");
                    if (f.Name.Contains("[CAR.ThanhTien]")) document.InsertSingleLineText(cell.Range.Start, string.Format("{0:#,0.##}", r.ThanhTien));
                }

                index += 1;
                stt++;
            }

            table.Rows.RemoveAt(index);

            return ctlRtf.RtfText;
        }

        private static string RtfTienDien(string rtfText, byte maTn, int maKh, int thang, int nam)
        {
            var obj = (from hd in _db.dvHoaDons
                       join td in _db.dvDiens on new { hd.TableName, hd.LinkID } equals new
                       { TableName = "dvDien", LinkID = (int?)td.ID }
                       join ct in _db.dvDienChiTiets on td.ID equals ct.MaDien
                       join dm in _db.dvDienDinhMucs on ct.MaDM equals dm.ID
                       join mb in _db.mbMatBangs on td.MaMB equals mb.MaMB
                       join tl in _db.mbTangLaus on mb.MaTL equals tl.MaTL
                       where hd.MaTN == maTn & hd.MaKH == maKh & hd.NgayTT.Value.Month == thang & hd.NgayTT.Value.Year == nam
                             & (hd.PhaiThu.GetValueOrDefault()
                                - (_db.ptChiTietPhieuThus.Where(p => p.LinkID == hd.ID & p.TableName == "dvHoaDon")
                                    .Sum(p => p.SoTien)).GetValueOrDefault()
                                - (_db.ktttChiTiets.Where(p => p.LinkID == hd.ID & p.TableName == "dvHoaDon")
                                    .Sum(p => p.SoTien)).GetValueOrDefault()
                             ) > 0
                       orderby mb.MaSoMB, dm.STT
                       select new
                       {
                           td.MaMB,
                           mb.MaSoMB,
                           tl.TenTL,
                           td.TuNgay,
                           td.DenNgay,
                           td.ChiSoCu,
                           td.ChiSoMoi,
                           td.SoTieuThu,
                           TienDien = td.ThanhTien,
                           td.TienVAT,
                           dm.TenDM,
                           ct.SoLuong,
                           DonGia = ct.DonGia, //
                           ThanhTien = ct.ThanhTien,
                           ConNo = hd.PhaiThu - _db.SoQuy_ThuChis.Where(p => p.LinkID == hd.ID).Sum(p => p.DaThu + p.KhauTru)
                                       .GetValueOrDefault()
                       }).AsEnumerable().Select((p, Index) => new
                       {
                           STT = Index + 1,
                           ChiSoCu = string.Format("{0:#,0.##}", p.ChiSoCu),
                           ChiSoMoi = string.Format("{0:#,0.##}", p.ChiSoCu),
                           TieuThu = string.Format("{0:#,0.##}", p.SoLuong),
                           DonGia = string.Format("{0:#,0.##}", p.DonGia),
                           ThanhTien = string.Format("{0:#,0.##}", p.ThanhTien),
                           p.TuNgay,
                           p.DenNgay,
                           TongCong = p.ThanhTien
                       }).ToList();


            if (obj.Count == 0) return rtfText;

            var ctlRtf = new DevExpress.XtraRichEdit.RichEditControl { RtfText = rtfText };
            ctlRtf.Document.ReplaceAll("[TD.TuNgayDenNgay]", string.Format("Từ/From    {0:dd/MM/yyyy}           Đến/to     {1:dd/MM/yyyy}", obj.First().TuNgay, obj.First().DenNgay), SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[TD.TongCong]", string.Format("{0:#,0.##}", obj.Sum(_ => _.TongCong).GetValueOrDefault()), SearchOptions.None);

            var document = ctlRtf.Document;
            var field = FindTable(document, "[TBP.TD]");

            if (field == null) return ctlRtf.RtfText;
            var table = document.Tables[field.Index];
            var rField = table.Rows[2];
            var ltFieldName = (from cell in rField.Cells let cellName = document.GetText(cell.Range).Replace(Environment.NewLine, "").Replace(" ", "") select new Field { Index = cell.Index, Name = cellName }).ToList();

            var index = 2;
            //var stt = 1;
            foreach (var r in obj)
            {
                var row = table.Rows.InsertBefore(index);
                // get all field in row {0:#,0.##}  {0:dd/MM/yyyy}
                foreach (var f in ltFieldName)
                {
                    var cell = row[f.Index];
                    var tx = r.GetType().GetProperty(f.Name.Replace("[TD.", "").Replace("]", "")) != null ? r.GetType().GetProperty(f.Name.Replace("[TD.", "").Replace("]", "")).GetValue(r, null) : null;
                    if (tx != null)
                    {
                        var text = tx.ToString();
                        document.InsertSingleLineText(cell.Range.Start, text);
                    }
                    //if (f.Name.Contains("[TD.STT]")) document.InsertSingleLineText(cell.Range.Start, string.Format("{0:#}", stt));
                    //if (f.Name.Contains("[TD.ChiSoCu]")) document.InsertSingleLineText(cell.Range.Start, string.Format("{0:#,0.##}", r.ChiSoCu));
                    //if (f.Name.Contains("[TD.ChiSoMoi]")) document.InsertSingleLineText(cell.Range.Start, string.Format("{0:#,0.##}", r.ChiSoMoi));
                    //if (f.Name.Contains("[TD.TieuThu]")) document.InsertSingleLineText(cell.Range.Start, string.Format("{0:#,0.##}", r.SoLuong));
                    //if (f.Name.Contains("[TD.DonGia]")) document.InsertSingleLineText(cell.Range.Start, string.Format("{0:#,0.##}", r.DonGia));
                    //if (f.Name.Contains("[TD.ThanhTien]")) document.InsertSingleLineText(cell.Range.Start, string.Format("{0:#,0.##}", r.ThanhTien));
                }

                index += 1;
                //stt++;
            }

            table.Rows.RemoveAt(index);

            return ctlRtf.RtfText;
        }

        private static string RtfTienNuoc(string rtfText, byte maTn, int maKh, int thang, int nam)
        {
            var objNuoc = (from hd in _db.dvHoaDons
                           join tn in _db.dvNuocs on new { hd.TableName, hd.LinkID } equals new { TableName = "dvNuoc", LinkID = (int?)tn.ID }
                           where hd.MaTN == maTn & hd.MaKH == maKh & hd.NgayTT.Value.Month == thang & hd.NgayTT.Value.Year == nam
                           select new
                           {
                               tn.ID,
                               tn.ChiSoCu,
                               tn.ChiSoMoi,
                               tn.TuNgay,
                               tn.DenNgay,
                               SoTieuThu = tn.SoTieuThu.GetValueOrDefault() + tn.SoTieuThuDHCu.GetValueOrDefault(),
                               SoUuDai = (from ud in _db.dvNuocUuDais where ud.MaMB == tn.MaMB select ud.SoNguoi).FirstOrDefault(),
                               ConNo = hd.PhaiThu - _db.SoQuy_ThuChis.Where(p => p.LinkID == hd.ID).Sum(p => p.DaThu + p.KhauTru).GetValueOrDefault()

                           }).FirstOrDefault();
            if (objNuoc == null) return rtfText;

            var obj = (from ct in _db.dvNuocChiTiets
                       join dm in _db.dvNuocDinhMucs on ct.MaDM equals dm.ID
                       where ct.MaNuoc == objNuoc.ID
                       orderby dm.STT
                       select new
                       {
                           dm.TenDM,
                           ct.SoLuong,
                           ct.DonGia,
                           ct.ThanhTien
                       }).ToList();

            if (obj.Count == 0) return rtfText;

            var ctlRtf = new DevExpress.XtraRichEdit.RichEditControl { RtfText = rtfText };
            ctlRtf.Document.ReplaceAll("[TN.ChiSoCu]", string.Format("{0:#,0.##}", objNuoc.ChiSoCu), SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[TN.ChiSoMoi]", string.Format("{0:#,0.##}", objNuoc.ChiSoMoi), SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[TN.TieuThu]", string.Format("{0:#,0.##}", objNuoc.SoTieuThu), SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[TN.TongTien]", string.Format("{0:#,0.##}", objNuoc.ConNo), SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[TN.DinhMuc]", "", SearchOptions.None);

            var document = ctlRtf.Document;
            var field = FindTable(document, "[TBP.1TN]");

            if (field == null) return ctlRtf.RtfText;
            var table = document.Tables[field.Index];
            var rField = table.Rows[2];
            var ltFieldName = (from cell in rField.Cells let cellName = document.GetText(cell.Range).Replace(Environment.NewLine, "").Replace(" ", "") select new Field { Index = cell.Index, Name = cellName }).ToList();

            var index = 2;
            var stt = 1;
            foreach (var r in obj)
            {
                var row = table.Rows.InsertBefore(index);
                // get all field in row {0:#,0.##}  {0:dd/MM/yyyy}
                foreach (var f in ltFieldName)
                {
                    var cell = row[f.Index];
                    if (f.Name.Contains("[TN.STT]")) document.InsertSingleLineText(cell.Range.Start, string.Format("{0:#}", stt));
                    if (f.Name.Contains("[TN.TieuThuChiTiet]")) document.InsertSingleLineText(cell.Range.Start, string.Format("{0:#,0.##}", r.SoLuong));
                    if (f.Name.Contains("[TN.DonGia]")) document.InsertSingleLineText(cell.Range.Start, string.Format("{0:#,0.##}", r.DonGia));
                    if (f.Name.Contains("[TN.ThanhTien]")) document.InsertSingleLineText(cell.Range.Start, string.Format("{0:#,0.##}", r.ThanhTien));
                    if (f.Name.Contains("[TN.GhiChu]")) document.InsertSingleLineText(cell.Range.Start, "");
                }

                index += 1;
                stt++;
            }

            table.Rows.RemoveAt(index);
            table.Rows.RemoveAt(0); // remove dòng key 1TN
            //var range = document.CreateRange(table.Range.Start.ToInt() - 2, 2);
            //    document.Replace(range, "");

            return ctlRtf.RtfText;
        }

        private static string RtfTienGas(string rtfText, byte maTn, int maKh, int thang, int nam)
        {
            var objGas = (from hd in _db.dvHoaDons
                          join ts in _db.dvGas on new { hd.TableName, hd.LinkID } equals new { TableName = "dvGas", LinkID = (int?)ts.ID }
                          where hd.MaTN == maTn & hd.MaKH == maKh & hd.NgayTT.Value.Month == thang & hd.NgayTT.Value.Year == nam
                          select new
                          {
                              ts.ID,
                              ts.ChiSoCu,
                              ts.ChiSoMoi,
                              ts.SoTieuThu,
                              ts.TyLe,
                              hd.ConNo
                          }).First();

            var obj = (from ct in _db.dvGasChiTiets
                       join dm in _db.dvGasDinhMucs on ct.MaDM equals dm.ID
                       where ct.MaGas == objGas.ID
                       orderby dm.STT
                       select new
                       {
                           SoLuongM3 = ct.SoLuong / objGas.TyLe,
                           ct.SoLuong,
                           ct.DonGia,
                           ct.ThanhTien
                       }).ToList();

            if (obj.Count == 0) return rtfText;

            var ctlRtf = new DevExpress.XtraRichEdit.RichEditControl { RtfText = rtfText };
            ctlRtf.Document.ReplaceAll("[GAS.ChiSoDau]", string.Format("{0:#,0.##}", objGas.ChiSoCu), SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[GAS.ChiSoCuoi]", string.Format("{0:#,0.##}", objGas.ChiSoMoi), SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[GAS.TongTieuThu]", string.Format("{0:#,0.##}", objGas.SoTieuThu), SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[GAS.TongTien]", string.Format("{0:#,0.##}", objGas.ConNo), SearchOptions.None);

            var document = ctlRtf.Document;
            var field = FindTable(document, "[TBP.GAS]");

            if (field != null)
            {
                var table = document.Tables[field.Index];
                var rField = table.Rows[2];
                var ltFieldName = (from cell in rField.Cells let cellName = document.GetText(cell.Range).Replace(Environment.NewLine, "").Replace(" ", "") select new Field { Index = cell.Index, Name = cellName }).ToList();

                var index = 2;
                var stt = 1;
                foreach (var r in obj)
                {
                    var row = table.Rows.InsertBefore(index);
                    // get all field in row {0:#,0.##}  {0:dd/MM/yyyy}
                    foreach (var f in ltFieldName)
                    {
                        var cell = row[f.Index];
                        if (f.Name.Contains("[GAS.STT]")) document.InsertSingleLineText(cell.Range.Start, string.Format("{0:#}", stt));
                        if (f.Name.Contains("[GAS.TieuThu]")) document.InsertSingleLineText(cell.Range.Start, string.Format("{0:#,0.##}", r.SoLuong));
                        if (f.Name.Contains("[GAS.QuyDoi]")) document.InsertSingleLineText(cell.Range.Start, string.Format("{0:#,0.##}", r.SoLuongM3));
                        if (f.Name.Contains("[GAS.DonGia]")) document.InsertSingleLineText(cell.Range.Start, string.Format("{0:#,0.##}", r.DonGia));
                        if (f.Name.Contains("[GAS.ThanhTien]")) document.InsertSingleLineText(cell.Range.Start, string.Format("{0:#,0.##}", r.ThanhTien));
                    }

                    index += 1;
                    stt++;
                }

                table.Rows.RemoveAt(index);

            }

            return ctlRtf.RtfText;
        }

        private static string RtfDieuHoaNgoaiGio(string rtfText, byte maTn, int maKh, int thang, int nam)
        {
            var obj = (from hd in _db.dvHoaDons
                       join dv in _db.dvDienDHs on new { hd.TableName, hd.LinkID } equals new
                       { TableName = "dvDienDH", LinkID = (int?)dv.ID }
                       join dm in _db.dvDienDH_DinhMucs on dv.MaMB equals dm.MaMB
                       join mb in _db.mbMatBangs on dv.MaMB equals mb.MaMB
                       where hd.MaTN == maTn & hd.MaKH == maKh & hd.NgayTT.Value.Month == thang & hd.NgayTT.Value.Year == nam &
                             hd.ConNo.GetValueOrDefault() > 0 & mb.MaLMB == 277 & hd.IsDuyet == true
                       select new
                       {
                           dv.NgayTT,
                           dv.HeSo,
                           dv.SoTieuThu,
                           ThanhTien = hd.ConNo,
                           dm.DonGia,
                           TienDien = _db.dvDienDH_ChiTiets.Where(p => p.MaDien == dv.ID).Sum(p => p.ThanhTien)
                               .GetValueOrDefault(),
                           TienVAT = _db.dvDienDH_ChiTiets.Where(p => p.MaDien == dv.ID).Sum(p => p.ThanhTien)
                                         .GetValueOrDefault() * 10 / 100,
                           hd.ID,
                           hd.TuNgay,
                           hd.DenNgay,
                       }).ToList();

            var ctlRtf = new DevExpress.XtraRichEdit.RichEditControl { RtfText = rtfText };
            var document = ctlRtf.Document;
            var field = FindTable(document, "[TBP.DDH]");

            if (field != null)
            {
                var table = document.Tables[field.Index];
                var rField = table.Rows[2];
                var ltFieldName = (from cell in rField.Cells let cellName = document.GetText(cell.Range).Replace(Environment.NewLine, "").Replace(" ", "") select new Field { Index = cell.Index, Name = cellName }).ToList();

                var index = 2;
                var stt = 1;
                foreach (var r in obj)
                {
                    var row = table.Rows.InsertBefore(index);
                    // get all field in row
                    foreach (var f in ltFieldName)
                    {
                        var cell = row[f.Index];
                        if (f.Name.Contains("[DDH.STT]")) document.InsertSingleLineText(cell.Range.Start, string.Format("{0:#}", stt));
                        if (f.Name.Contains("[DDH.ThoiGian]")) document.InsertSingleLineText(cell.Range.Start, string.Format("{0:dd/MM/yyyy}", r.NgayTT));
                        if (f.Name.Contains("[DDH.FCU]")) document.InsertSingleLineText(cell.Range.Start, string.Format("{0:#,0.##}", 1));
                        if (f.Name.Contains("[DDH.SoGio]")) document.InsertSingleLineText(cell.Range.Start, string.Format("{0:dd/MM/yyyy}", r.SoTieuThu));
                        if (f.Name.Contains("[DDH.DonGia]")) document.InsertSingleLineText(cell.Range.Start, string.Format("{0:#,0.##}", r.DonGia));
                        if (f.Name.Contains("[DDH.ThanhTien]")) document.InsertSingleLineText(cell.Range.Start, string.Format("{0:#,0.##}", r.ThanhTien));
                        if (f.Name.Contains("[DDH.GhiChu]")) document.InsertSingleLineText(cell.Range.Start, "");
                    }

                    index += 1;
                    stt++;
                }

                table.Rows.RemoveAt(index);
            }

            return ctlRtf.RtfText;
        }

        private static string RtfNuocNong(string rtfText, byte maTn, int maKh, int thang, int nam)
        {
            var objNuoc = (from hd in _db.dvHoaDons
                           join tn in _db.dvNuocNongs on new { hd.TableName, hd.LinkID } equals new { TableName = "dvNuocNong", LinkID = (int?)tn.ID }
                           where hd.MaTN == maTn & hd.MaKH == maKh & hd.NgayTT.Value.Month == thang & hd.NgayTT.Value.Year == nam
                           select new
                           {
                               tn.ID,
                               tn.DauCap,
                               tn.DauHoi,
                               tn.SoTieuThu,
                               hd.ConNo
                           }).FirstOrDefault();
            if (objNuoc == null) return rtfText;

            var obj = (from ct in _db.dvNuocNongChiTiets
                       join dm in _db.dvNuocNongDinhMucs on ct.MaDM equals dm.ID
                       where ct.MaNuoc == objNuoc.ID
                       orderby dm.STT
                       select new
                       {
                           dm.TenDM,
                           ct.SoLuong,
                           ct.DonGia,
                           ct.ThanhTien
                       }).ToList();

            if (obj.Count == 0) return rtfText;

            var ctlRtf = new DevExpress.XtraRichEdit.RichEditControl { RtfText = rtfText };
            ctlRtf.Document.ReplaceAll("[NN.DauCap]", string.Format("{0:#,0.##}", objNuoc.DauCap), SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[NN.DauHoi]", string.Format("{0:#,0.##}", objNuoc.DauHoi), SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[NN.TieuThu]", string.Format("{0:#,0.##}", objNuoc.SoTieuThu), SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[NN.SoUuDai]", "", SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[NN.TongTien]", string.Format("{0:#,0.##}", objNuoc.ConNo), SearchOptions.None);

            var document = ctlRtf.Document;
            var field = FindTable(document, "[TBP.NN]");

            if (field != null)
            {
                var table = document.Tables[field.Index];
                var rField = table.Rows[2];
                var ltFieldName = (from cell in rField.Cells let cellName = document.GetText(cell.Range).Replace(Environment.NewLine, "").Replace(" ", "") select new Field { Index = cell.Index, Name = cellName }).ToList();

                var index = 2;
                var stt = 1;
                foreach (var r in obj)
                {
                    var row = table.Rows.InsertBefore(index);
                    // get all field in row {0:#,0.##}  {0:dd/MM/yyyy}
                    foreach (var f in ltFieldName)
                    {
                        var cell = row[f.Index];
                        if (f.Name.Contains("[NN.STT]")) document.InsertSingleLineText(cell.Range.Start, string.Format("{0:#}", stt));
                        if (f.Name.Contains("[NN.DinhMuc]")) document.InsertSingleLineText(cell.Range.Start, r.TenDM);
                        if (f.Name.Contains("[NN.DonGia]")) document.InsertSingleLineText(cell.Range.Start, string.Format("{0:#,0.##}", r.DonGia));
                        if (f.Name.Contains("[NN.ThanhTien]")) document.InsertSingleLineText(cell.Range.Start, string.Format("{0:#,0.##}", r.ThanhTien));
                        if (f.Name.Contains("[NN.CongSuat]")) document.InsertSingleLineText(cell.Range.Start, string.Format("{0:#,0.##}", r.SoLuong));
                    }

                    index += 1;
                    stt++;
                }

                table.Rows.RemoveAt(index);
            }

            return ctlRtf.RtfText;
        }

        private static string RtfNuocSinhHoat(string rtfText, byte maTn, int maKh, int thang, int nam)
        {
            var objNuoc = (from hd in _db.dvHoaDons
                           join tn in _db.dvNuocSinhHoats on new { hd.TableName, hd.LinkID } equals new { TableName = "dvNuocSinhHoat", LinkID = (int?)tn.ID }
                           where hd.MaTN == maTn & hd.MaKH == maKh & hd.NgayTT.Value.Month == thang & hd.NgayTT.Value.Year == nam
                           select new
                           {
                               tn.ID,
                               tn.ChiSoCu,
                               tn.ChiSoMoi,
                               tn.SoTieuThuNL,
                               tn.DauCap_Cu,
                               tn.DauCap_Moi,
                               tn.DauHoi_Cu,
                               tn.DauHoi_Moi,
                               tn.SoTieuThuNN,
                               tn.SoTieuThu,
                               hd.PhaiThu,
                               ThueVAT = (hd.PhaiThu * 100 / 115) * 5 / 100,
                               PhiMT = (hd.PhaiThu * 100 / 115) * 10 / 100,
                               TienNuoc = hd.PhaiThu * 100 / 115,
                           }).First();

            var obj = (from ct in _db.dvNuocSinhHoatChiTiets
                       join dm in _db.dvNuocDinhMucs on ct.MaDM equals dm.ID
                       where ct.MaNuoc == objNuoc.ID
                       orderby dm.STT
                       select new
                       {
                           dm.TenDM,
                           ct.SoLuong,
                           ct.DonGia,
                           ct.ThanhTien,
                           ct.DienGiai,
                       }).ToList();

            if (obj.Count == 0) return rtfText;

            var ctlRtf = new DevExpress.XtraRichEdit.RichEditControl { RtfText = rtfText };
            ctlRtf.Document.ReplaceAll("[NSH.SoLuongTieuThuNuocLanh]", string.Format("{0:#,0.##}", objNuoc.SoTieuThuNL), SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[NSH.SoLuongTieuThuNuocNong]", string.Format("{0:#,0.##}", objNuoc.SoTieuThuNN), SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[NSH.SoTieuThu]", string.Format("{0:#,0.##}", objNuoc.SoTieuThu), SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[NSH.TongTienTruocThue]", string.Format("{0:#,0.##}", objNuoc.TienNuoc), SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[NSH.ThueVAT]", string.Format("{0:#,0.##}", objNuoc.ThueVAT), SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[NSH.PhiBVMT]", string.Format("{0:#,0.##}", objNuoc.PhiMT), SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[NSH.TongTien]", string.Format("{0:#,0.##}", objNuoc.PhaiThu), SearchOptions.None);

            var document = ctlRtf.Document;
            var field = FindTable(document, "[TBP.NSH]");

            if (field != null)
            {
                var table = document.Tables[field.Index];
                var rField = table.Rows[2];
                var ltFieldName = (from cell in rField.Cells let cellName = document.GetText(cell.Range).Replace(Environment.NewLine, "").Replace(" ", "") select new Field { Index = cell.Index, Name = cellName }).ToList();

                var index = 2;
                var stt = 1;
                foreach (var r in obj)
                {
                    var row = table.Rows.InsertBefore(index);
                    // get all field in row {0:#,0.##}  {0:dd/MM/yyyy}
                    foreach (var f in ltFieldName)
                    {
                        var cell = row[f.Index];
                        if (f.Name.Contains("[NSH.STT]")) document.InsertSingleLineText(cell.Range.Start, string.Format("{0:#}", stt));
                        if (f.Name.Contains("[NSH.NoiDung]")) document.InsertSingleLineText(cell.Range.Start, r.TenDM);
                        if (f.Name.Contains("[NSH.SoLuong]")) document.InsertSingleLineText(cell.Range.Start, string.Format("{0:#,0.##}", r.SoLuong));
                        if (f.Name.Contains("[NSH.DonGia]")) document.InsertSingleLineText(cell.Range.Start, string.Format("{0:#,0.##}", r.DonGia));
                        if (f.Name.Contains("[NSH.ThanhTien]")) document.InsertSingleLineText(cell.Range.Start, string.Format("{0:#,0.##}", r.ThanhTien));
                    }

                    index += 1;
                    stt++;
                }

                table.Rows.RemoveAt(index);
            }

            return ctlRtf.RtfText;
        }

        private static string RtfDoanhThuGiamTru(string rtfText, byte maTn, int maKh, int thang, int nam, int maLdv)
        {
            var obj = (from hd in _db.dvHoaDons
                       where hd.MaTN == maTn & hd.MaLDV == maLdv & hd.MaKH == maKh & hd.NgayTT.Value.Month == thang &
                             hd.NgayTT.Value.Year == nam & hd.ConNo.GetValueOrDefault() > 0
                       select new
                       {
                           hd.DienGiai,
                           ThanhTien = hd.ConNo,
                       }).ToList();

            var ctlRtf = new DevExpress.XtraRichEdit.RichEditControl { RtfText = rtfText };
            var document = ctlRtf.Document;
            var field = FindTable(document, "[TBP.DTGT]");

            if (field != null)
            {
                var table = document.Tables[field.Index];
                var rField = table.Rows[2];
                var ltFieldName = (from cell in rField.Cells let cellName = document.GetText(cell.Range).Replace(Environment.NewLine, "").Replace(" ", "") select new Field { Index = cell.Index, Name = cellName }).ToList();

                var index = 2;
                foreach (var r in obj)
                {
                    var row = table.Rows.InsertBefore(index);
                    // get all field in row
                    foreach (var f in ltFieldName)
                    {
                        var cell = row[f.Index];
                        if (f.Name.Contains("[DTGT.NoiDung]")) document.InsertSingleLineText(cell.Range.Start, r.DienGiai);
                        if (f.Name.Contains("[DTGT.ThanhTien]")) document.InsertSingleLineText(cell.Range.Start, string.Format("{0:#,0.##}", r.ThanhTien));
                    }

                    index += 1;
                }

                table.Rows.RemoveAt(index);
            }

            return ctlRtf.RtfText;
        }

        private static string RtfPhiQuanLy(string rtfText, byte maTn, int maKh, int thang, int nam, int maLdv)
        {
            var obj = (from hd in _db.dvHoaDons
                       join dv in _db.dvDichVuKhacs on new { hd.MaLDV, hd.LinkID } equals new { MaLDV = (int?)13, LinkID = (int?)dv.ID }
                       join dvt in _db.DonViTinhs on dv.MaDVT equals dvt.ID
                       where hd.MaTN == maTn & hd.MaLDV == maLdv & hd.MaKH == maKh & hd.NgayTT.Value.Month == thang & hd.NgayTT.Value.Year == nam
                             & hd.ConNo.GetValueOrDefault() > 0
                       select new
                       {
                           dv.SoLuong,
                           dvt.TenDVT,
                           DonGia = dv.DonGia * dv.TyGia,
                           ThanhTien = (hd.PhaiThu.GetValueOrDefault() - _db.SoQuy_ThuChis.Where(sq => sq.LinkID == hd.ID && sq.TableName == "dvHoaDon").Sum(s => s.DaThu + s.KhauTru).GetValueOrDefault()
                               ),
                       }).ToList();
            var ngayTb = new DateTime(nam, thang, 1);
            var noCu = (from hd in _db.dvHoaDons

                        where hd.MaKH == maKh
                              & SqlMethods.DateDiffDay(hd.NgayTT, ngayTb) > 0
                              & (hd.PhaiThu.GetValueOrDefault()

                                 - (from ct in _db.SoQuy_ThuChis
                                    where ct.TableName == "dvHoaDon" & ct.LinkID == hd.ID
                                                             & SqlMethods.DateDiffDay(ct.NgayPhieu, ngayTb) > 0
                                    select ct.DaThu + ct.KhauTru).Sum().GetValueOrDefault()) > 0
                              & (hd.MaLDV == 13 || hd.MaLDV == 69)
                        select (hd.PhaiThu
                                - (from ct in _db.SoQuy_ThuChis
                                   where ct.TableName == "dvHoaDon" & ct.LinkID == hd.ID
                                                            & SqlMethods.DateDiffDay(ct.NgayPhieu, ngayTb) > 0
                                   select ct.DaThu + ct.KhauTru).Sum().GetValueOrDefault()
                            )).Sum().GetValueOrDefault();
            if (obj.Count == 0) return rtfText;

            var ctlRtf = new DevExpress.XtraRichEdit.RichEditControl { RtfText = rtfText };

            ctlRtf.Document.ReplaceAll("[PQL.TongTien]", string.Format("{0:#,0.##}", noCu + obj.Sum(p => p.ThanhTien)), SearchOptions.None);

            var document = ctlRtf.Document;
            var field = FindTable(document, "[TBP.PQL]");

            if (field != null)
            {
                var table = document.Tables[field.Index];
                var rField = table.Rows[2];
                var ltFieldName = (from cell in rField.Cells let cellName = document.GetText(cell.Range).Replace(Environment.NewLine, "").Replace(" ", "") select new Field { Index = cell.Index, Name = cellName }).ToList();

                var index = 2;
                var stt = 1;
                foreach (var r in obj)
                {
                    var row = table.Rows.InsertBefore(index);
                    // get all field in row {0:#,0.##}  {0:dd/MM/yyyy}
                    foreach (var f in ltFieldName)
                    {
                        var cell = row[f.Index];
                        if (index == 1)
                        {
                            if (f.Name.Contains("[PQL.NoCu]")) document.InsertSingleLineText(cell.Range.Start, string.Format("{0:#,0.##}", noCu));
                        }
                        if (f.Name.Contains("[PQL.STT]")) document.InsertSingleLineText(cell.Range.Start, string.Format("{0:#}", stt));
                        if (f.Name.Contains("[PQL.DienTich]")) document.InsertSingleLineText(cell.Range.Start, string.Format("{0:#,0.##}", r.SoLuong));
                        if (f.Name.Contains("[PQL.DonGia]")) document.InsertSingleLineText(cell.Range.Start, string.Format("{0:#,0.##}", r.DonGia));
                        if (f.Name.Contains("[PQL.ThanhTien]")) document.InsertSingleLineText(cell.Range.Start, string.Format("{0:#,0.##}", r.ThanhTien));

                        if (f.Name.Contains("[PQL.Ngay]")) document.InsertSingleLineText(cell.Range.Start, "");
                    }

                    index += 1;
                    stt++;
                }

                table.Rows.RemoveAt(index);
            }

            return ctlRtf.RtfText;
        }

        private static string RtfDichVuCoBan(string rtfText, byte maTn, int maKh, int thang, int nam, int maLdv)
        {
            var obj = (from hd in _db.dvHoaDons
                       join dv in _db.dvDichVuKhacs on new { hd.MaLDV, hd.LinkID } equals new { MaLDV = (int?)dv.MaLDV, LinkID = (int?)dv.ID }
                       join dvt in _db.DonViTinhs on dv.MaDVT equals dvt.ID
                       where hd.MaTN == maTn & hd.MaLDV == maLdv & hd.MaKH == maKh & hd.NgayTT.Value.Month == thang & hd.NgayTT.Value.Year == nam & hd.ConNo.GetValueOrDefault() > 0
                       select new
                       {
                           dv.SoLuong,
                           dvt.TenDVT,
                           DonGia = dv.DonGia * dv.TyGia,
                           ThanhTien = hd.PhaiThu,
                       }).ToList();

            if (obj.Count == 0) return rtfText;

            var ctlRtf = new DevExpress.XtraRichEdit.RichEditControl { RtfText = rtfText };

            ctlRtf.Document.ReplaceAll("[DVCB.TongTien]", string.Format("{0:#,0.##}", obj.Sum(p => p.ThanhTien)), SearchOptions.None);

            var document = ctlRtf.Document;
            var field = FindTable(document, "[TBP.DVCB]");

            if (field != null)
            {
                var table = document.Tables[field.Index];
                var rField = table.Rows[2];
                var ltFieldName = (from cell in rField.Cells let cellName = document.GetText(cell.Range).Replace(Environment.NewLine, "").Replace(" ", "") select new Field { Index = cell.Index, Name = cellName }).ToList();

                var index = 2;
                var stt = 1;
                foreach (var r in obj)
                {
                    var row = table.Rows.InsertBefore(index);
                    // get all field in row {0:#,0.##}  {0:dd/MM/yyyy}
                    foreach (var f in ltFieldName)
                    {
                        var cell = row[f.Index];
                        if (f.Name.Contains("[DVCB.STT]")) document.InsertSingleLineText(cell.Range.Start, string.Format("{0:#}", stt));
                        if (f.Name.Contains("[DVCB.SoLuong]")) document.InsertSingleLineText(cell.Range.Start, string.Format("{0:#,0.##}", r.SoLuong));
                        if (f.Name.Contains("[DVCB.DonGia]")) document.InsertSingleLineText(cell.Range.Start, string.Format("{0:#,0.##}", r.DonGia));
                        if (f.Name.Contains("[DVCB.ThanhTien]")) document.InsertSingleLineText(cell.Range.Start, string.Format("{0:#,0.##}", r.ThanhTien));

                        if (f.Name.Contains("[DVCB.GhiChu]")) document.InsertSingleLineText(cell.Range.Start, "");
                    }

                    index += 1;
                    stt++;
                }

                table.Rows.RemoveAt(index);
            }

            return ctlRtf.RtfText;
        }

        private static string RtfDeleteFieldFinish(string rtfText)
        {
            var ctlRtf = new DevExpress.XtraRichEdit.RichEditControl { RtfText = rtfText };

            ctlRtf.RtfText = FindAndRemoveTable(ctlRtf.RtfText, "[TBP.TT]");
            ctlRtf.RtfText = FindAndRemoveTable(ctlRtf.RtfText, "[TBP.SC]");
            ctlRtf.RtfText = FindAndRemoveTable(ctlRtf.RtfText, "[TBP.DC]");
            ctlRtf.RtfText = FindAndRemoveTable(ctlRtf.RtfText, "[TBP.TD]");
            ctlRtf.RtfText = FindAndRemoveTable(ctlRtf.RtfText, "[TBP.D3P]");
            ctlRtf.RtfText = FindAndRemoveTable(ctlRtf.RtfText, "[TBP.TN]");
            ctlRtf.RtfText = FindAndRemoveTable(ctlRtf.RtfText, "[TBP.1TN]");
            ctlRtf.RtfText = FindAndRemoveTable(ctlRtf.RtfText, "[TBP.NN]");
            ctlRtf.RtfText = FindAndRemoveTable(ctlRtf.RtfText, "[TBP.NSH]");
            ctlRtf.RtfText = FindAndRemoveTable(ctlRtf.RtfText, "[TBP.DDH]");
            ctlRtf.RtfText = FindAndRemoveTable(ctlRtf.RtfText, "[TBP.GAS]");
            ctlRtf.RtfText = FindAndRemoveTable(ctlRtf.RtfText, "[TBP.CAR]");
            ctlRtf.RtfText = FindAndRemoveTable(ctlRtf.RtfText, "[TBP.DVCB]");
            ctlRtf.RtfText = FindAndRemoveTable(ctlRtf.RtfText, "[TBP.DTGT]");
            ctlRtf.RtfText = FindAndRemoveTable(ctlRtf.RtfText, "[TBP.PQL]");

            ctlRtf.Document.ReplaceAll("[TBP.PQL]", "", SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[TBP.TT]", "", SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[TBP.SC]", "", SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[TBP.DC]", "", SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[TBP.TD]", "", SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[TBP.D3P]", "", SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[TBP.TN]", "", SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[TBP.NN]", "", SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[TBP.NSH]", "", SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[TBP.DDH]", "", SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[TBP.GAS]", "", SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[TBP.CAR]", "", SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[TBP.DVCB]", "", SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[TBP.DTGT]", "", SearchOptions.None);

            return ctlRtf.RtfText;
        }

        public static int FindIndexTable(Document document, string key)
        {
            var tableList = (from p in document.Tables
                             select new { cellName = document.GetText(p.Rows[0].Cells[0].Range).Replace(" ", "") }).AsEnumerable()
                .Select((t, index) => new { stt = index, t.cellName }).ToList();
            var tableIndex = tableList.FirstOrDefault(_ => _.cellName.Contains(key));
            return tableIndex != null ? tableIndex.stt : 0;
        }

        private static string FindAndRemoveTable(string rtfText, string key)
        {
            var ctlRtf = new DevExpress.XtraRichEdit.RichEditControl { RtfText = rtfText };
            var document = ctlRtf.Document;
            var index = FindIndexTable(document, key);

            if (index <= 0) return ctlRtf.RtfText;
            var table = document.Tables[index];
            var pos = table.Range.Start.ToInt() - 2;
            document.Tables.Remove(table);
            if (pos <= 0) return ctlRtf.RtfText;
            var range = document.CreateRange(pos, 2);
            document.Replace(range, "");
            return ctlRtf.RtfText;
        }

        #endregion

        #region Request yêu cầu cho nhân viên và khách hàng groupid = 10 & 11

        public static string Request(string rtfText, string title, string content) // send email from edit, no need id
        {
            var ctlRtf = new DevExpress.XtraRichEdit.RichEditControl { RtfText = rtfText };
            ctlRtf.Document.ReplaceAll("[YCNV.NoiDung]", content, SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[YCNV.TieuDe]", content, SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[YCKH.NoiDung]", content, SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[YCKH.TieuDe]", content, SearchOptions.None);
            return ctlRtf.RtfText;
        }
        #endregion

        #region Mẫu thông báo xe groupid  = 8
        //private static string RtfPhiXe(string rtfText, byte maTn, int maKh, int thang, int nam)
        //{
        //    var ngayTb = new DateTime(nam, thang, 1);
        //    var ltGiuXete = (from tx in _db.dvgxTheXes
        //                     join lx in _db.dvgxLoaiXes on tx.MaLX equals lx.MaLX into dslx
        //                     from lx in dslx.DefaultIfEmpty()
        //                     join dv in _db.dvHoaDons on tx.ID equals dv.LinkID
        //                     where tx.MaTN == maTn & tx.MaKH == maKh
        //                                           & dv.PhaiThu.GetValueOrDefault()
        //                                           - _db.SoQuy_ThuChis
        //                                               .Where(p => p.LinkID == dv.ID & p.MaKH == maKh & p.MaTN == maTn &
        //                                                           p.TableName == "dvHoaDon").Sum(p => p.DaThu + p.KhauTru)
        //                                           .GetValueOrDefault() > 0
        //                                           & dv.NgayTT.Value.Month == thang & dv.NgayTT.Value.Year == nam &
        //                                           dv.IsDuyet == true & dv.MaLDV == 6
        //                     group tx by new { tx.MaLX, lx.TenLX, tx.GiaThang }
        //                         into gr
        //                         select new GiuXeItem()
        //                         {
        //                             MaLX = gr.Key.MaLX,
        //                             TenLX = gr.Key.TenLX,
        //                             BienSo = "",
        //                             SoLuong = gr.Count(),
        //                             DonGia = gr.Key.GiaThang,
        //                             ThanhTien = gr.Count() * gr.Key.GiaThang,
        //                             NoCu = (from hd in _db.dvHoaDons
        //                                     join tx in _db.dvgxTheXes on hd.LinkID equals tx.ID
        //                                     where hd.MaKH == maKh & hd.IsDuyet.GetValueOrDefault() == true & tx.MaLX == gr.Key.MaLX
        //                                           & SqlMethods.DateDiffDay(hd.NgayTT, ngayTb) > 0
        //                                           & (hd.PhaiThu.GetValueOrDefault()

        //                                              - (from ct in _db.SoQuy_ThuChis
        //                                                 where ct.TableName == "dvHoaDon" & ct.LinkID == hd.ID
        //                                                                                  & SqlMethods.DateDiffDay(ct.NgayPhieu, ngayTb) > 0
        //                                                 select ct.KhauTru + ct.DaThu).Sum().GetValueOrDefault()) != 0
        //                                           & hd.MaLDV == 6
        //                                     select hd.PhaiThu
        //                                            - (from ct in _db.SoQuy_ThuChis
        //                                               where ct.TableName == "dvHoaDon" & ct.LinkID == hd.ID
        //                                                                                & SqlMethods.DateDiffDay(ct.NgayPhieu, ngayTb) > 0
        //                                               select ct.DaThu + ct.KhauTru).Sum().GetValueOrDefault()).Sum().GetValueOrDefault()
        //                         }).ToList();
        //    foreach (var gx in ltGiuXete)
        //    {
        //        var ltBienSo = (from tx in _db.dvgxTheXes
        //                        where tx.MaTN == maTn & tx.MaKH == maKh & tx.NgungSuDung == false & tx.MaLX == gx.MaLX & tx.GiaThang == gx.DonGia
        //                        select tx.BienSo).ToList();

        //        foreach (var bs in ltBienSo)
        //            if (!string.IsNullOrEmpty(bs))
        //                gx.BienSo += bs + "; ";

        //        gx.BienSo = gx.BienSo.Trim(' ').Trim(';');
        //    }

        //    var tienTt = (from hd in _db.dvHoaDons
        //                  join ltt in _db.dvgxLichThanhToans on new { hd.TableName, hd.LinkID } equals new { TableName = "dvgxLichThanhToan", LinkID = (int?)ltt.ID }
        //                  where hd.MaTN == maTn & hd.MaLDV == 6 & hd.MaKH == maKh & hd.NgayTT.Value.Month == thang & hd.NgayTT.Value.Year == nam & hd.ConNo.GetValueOrDefault() > 0
        //                  select new
        //                  {
        //                      PhaiThu = hd.PhaiThu
        //                                - _db.SoQuy_ThuChis.Where(p => p.LinkID == hd.ID).Sum(p => p.DaThu + p.KhauTru).GetValueOrDefault()
        //                  }).Sum(p => p.PhaiThu).GetValueOrDefault();

        //    var ctlRtf = new DevExpress.XtraRichEdit.RichEditControl { RtfText = rtfText };
        //    ctlRtf.Document.ReplaceAll("[CAR.TongTien]", string.Format("{0:#,0.##}", tienTt), SearchOptions.None);

        //    var document = ctlRtf.Document;
        //    var field = FindTable(document, "[TBP.CAR]");

        //    if (field == null) return ctlRtf.RtfText;
        //    var table = document.Tables[field.Index];
        //    var rField = table.Rows[2];
        //    var ltFieldName = (from cell in rField.Cells let cellName = document.GetText(cell.Range).Replace(Environment.NewLine, "").Replace(" ", "") select new Field { Index = cell.Index, Name = cellName }).ToList();

        //    var index = 2;
        //    var stt = 1;
        //    foreach (var r in ltGiuXete)
        //    {
        //        var row = table.Rows.InsertBefore(index);
        //        // get all field in row
        //        foreach (var f in ltFieldName)
        //        {
        //            var cell = row[f.Index];
        //            if (f.Name.Contains("[CAR.STT]")) document.InsertSingleLineText(cell.Range.Start, string.Format("{0:#}", stt));
        //            if (f.Name.Contains("[CAR.TheXe]")) document.InsertSingleLineText(cell.Range.Start, r.TenLX);
        //            if (f.Name.Contains("[CAR.DonGia]")) document.InsertSingleLineText(cell.Range.Start, string.Format("{0:#,0.##}", r.DonGia));
        //            if (f.Name.Contains("[CAR.BienSoXe]")) document.InsertSingleLineText(cell.Range.Start, r.BienSo);
        //            if (f.Name.Contains("[CAR.SoLuongXe]")) document.InsertSingleLineText(cell.Range.Start, string.Format("{0:#,0.##}", r.SoLuong));
        //            if (f.Name.Contains("[CAR.NoCu]")) document.InsertSingleLineText(cell.Range.Start, string.Format("{0:#,0.##}", r.NoCu));
        //            if (f.Name.Contains("[CAR.GhiChu]")) document.InsertSingleLineText(cell.Range.Start, "");
        //            if (f.Name.Contains("[CAR.ThanhTien]")) document.InsertSingleLineText(cell.Range.Start, string.Format("{0:#,0.##}", r.ThanhTien));
        //        }

        //        index += 1;
        //        stt++;
        //    }

        //    table.Rows.RemoveAt(index);

        //    return ctlRtf.RtfText;
        //}
        #endregion

        #region Get tổng report
        public static string GetContentReport(int? formTemplateId, byte? buildingId, int? nam, int? thang)
        {
            var template = BuildingDesignTemplate.Class.Form.GetFormTemplateById(formTemplateId);
            if (template == null) return "";

            string contents = template.Content;

            switch (formTemplateId)
            {
                case BuildingDesignTemplate.Class.Common.ReportIndex.BIEU_MAU_CONG_NO_NCC: contents = BuildingDesignTemplate.Class.HopDongThueNgoai.GetContentCongNoNcc(contents, buildingId.ToString(), template.GroupId); break;
                case BuildingDesignTemplate.Class.Common.ReportIndex.THONG_KE_KINH_PHI_DU_KIEN: contents = BuildingDesignTemplate.Class.KinhPhiDuKien.MergeKinhPhiDuKien(contents, nam, buildingId.ToString(), template.GroupId); break;
                case BuildingDesignTemplate.Class.Common.ReportIndex._1KH_THU:
                    contents = BuildingDesignTemplate.Class.BaoCaoBWP._1_KH_Thu(contents, buildingId, (int)nam, (int)thang);
                    break;
                case BuildingDesignTemplate.Class.Common.ReportIndex.BC_THU:
                    contents = BuildingDesignTemplate.Class.BaoCaoBWP._2_KH_Thu(contents, buildingId, (int)nam, (int)thang);
                    break;
            }

            return contents;
        }

        #endregion

    }
}
