using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using DevExpress.XtraRichEdit;
using DevExpress.XtraRichEdit.API.Native;
using DevExpress.XtraReports.Parameters;
using System.Linq;
using Library;

namespace LandSoftBuilding.Lease
{
    public partial class rptHopDongThueGianHang : DevExpress.XtraReports.UI.XtraReport
    {
        public rptHopDongThueGianHang(int? MaHD)
        {
            InitializeComponent();

            var db = new MasterDataContext();
            try
            {
                var objHD = (from hd in db.ctHopDongs
                             join kh in db.tnKhachHangs on hd.MaKH equals kh.MaKH
                             where hd.ID == MaHD
                             select new
                             {
                                 hd.SoHDCT,
                                 hd.NgayKy,
                                 TenKH = kh.IsCaNhan.Value ? kh.HoKH + " " + kh.TenKH : kh.CtyTen,
                                 kh.CMND,
                                 kh.NgaySinh,
                                 kh.NgayCap,
                                 kh.NoiCap,
                                 kh.DCLL,
                                 kh.DienThoaiKH,
                                 MaSoMB = hd.ctChiTiets.FirstOrDefault().mbMatBang.MaSoMB,
                                 GiaThue = hd.ctChiTiets.Sum(p => p.DonGia).GetValueOrDefault() * hd.ThoiHan,
                                 TyLeVAT = hd.ctChiTiets.FirstOrDefault().TyLeVAT ?? 0,
                                 TienVAT = hd.ctChiTiets.Sum(p => p.TienVAT).GetValueOrDefault() * hd.ThoiHan,
                                 ThanhTien = hd.ctChiTiets.Sum(p => p.ThanhTien).GetValueOrDefault() * hd.ThoiHan
                             }).First();

                var objTien = new TienTeCls();

                var ctlRTF = new DevExpress.XtraRichEdit.RichEditControl();
                ctlRTF.RtfText = rtHeader.Rtf;
                ctlRTF.Document.ReplaceAll("[SoHD]", objHD.SoHDCT, SearchOptions.None);
                ctlRTF.Document.ReplaceAll("[NgayKy]", string.Format("ngày {0:dd} tháng {0:MM} năm {0:yyyy}", objHD.NgayKy), SearchOptions.None);
                ctlRTF.Document.ReplaceAll("[HoTenKH]", objHD.TenKH, SearchOptions.None);
                ctlRTF.Document.ReplaceAll("[NgaySinh]", string.Format("ngày {0:dd} tháng {0:MM} năm {0:yyyy}", objHD.NgaySinh), SearchOptions.None);
                ctlRTF.Document.ReplaceAll("[SoCMND]", objHD.CMND ?? "", SearchOptions.None);
                ctlRTF.Document.ReplaceAll("[NgayCap]", string.Format("{0:dd/MM/yyyy}", objHD.NgayCap), SearchOptions.None);
                ctlRTF.Document.ReplaceAll("[NoiCap]", objHD.NoiCap ?? "", SearchOptions.None);
                ctlRTF.Document.ReplaceAll("[DienThoai]", objHD.DienThoaiKH ?? "", SearchOptions.None);
                ctlRTF.Document.ReplaceAll("[DiaChi]", objHD.DCLL ?? "", SearchOptions.None);
                ctlRTF.Document.ReplaceAll("[MaSoMB]", objHD.MaSoMB ?? "", SearchOptions.None);
                ctlRTF.Document.ReplaceAll("[GiaThue]", string.Format("{0:n0}", objHD.GiaThue), SearchOptions.None);
                ctlRTF.Document.ReplaceAll("[GiaThueBC]", objTien.DocTienBangChu(objHD.GiaThue ?? 0, "đồng"), SearchOptions.None);
                ctlRTF.Document.ReplaceAll("[TyLeVAT]", string.Format("{0:p0}", objHD.TyLeVAT), SearchOptions.None);
                ctlRTF.Document.ReplaceAll("[TienVAT]", string.Format("{0:n0}", objHD.TienVAT), SearchOptions.None);
                ctlRTF.Document.ReplaceAll("[TienVATBC]", objTien.DocTienBangChu(objHD.TienVAT ?? 0, "đồng"), SearchOptions.None);
                ctlRTF.Document.ReplaceAll("[ThanhTien]", string.Format("{0:n0}", objHD.ThanhTien), SearchOptions.None);
                ctlRTF.Document.ReplaceAll("[ThanhTienBC]", objTien.DocTienBangChu(objHD.ThanhTien ?? 0, "đồng"), SearchOptions.None);
                rtHeader.Rtf = ctlRTF.RtfText;
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
            finally
            {
                db.Dispose();
            }
        }
    }
}
