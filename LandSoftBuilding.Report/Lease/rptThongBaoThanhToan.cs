using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data.Linq;
using System.Data.Linq.SqlClient;
using DevExpress.XtraRichEdit;
using DevExpress.XtraRichEdit.API.Native;
using DevExpress.XtraReports.Parameters;
using Library;
using System.Linq;

namespace LandSoftBuilding.Lease
{
    public partial class rptThongBaoThanhToan : DevExpress.XtraReports.UI.XtraReport
    {
        public rptThongBaoThanhToan(int? MaHD, byte _MaTN)
        {
            Library.frmPrintControl.LoadLayout(this, 76, _MaTN);
            var db = new MasterDataContext();
            InitializeComponent();
            try
            {

                cNgayIn.Text = string.Format("Hà Nội,ngày {0:dd} tháng {0:MM} năm {0:yyyy}", DateTime.Now);

                #region DataBinding
                cSTT.DataBindings.Add("Text", null, "STT");
                cDienGiai.DataBindings.Add("Text", null, "DienGiai");
                cKyTT.DataBindings.Add("Text", null, "SoThang", "{0:#,0.##}");
                cDonGia.DataBindings.Add("Text", null, "GiaThue", "{0:#,0.##}");
                cSoTien.DataBindings.Add("Text", null, "SoTienQD", "{0:#,0.##}");
                cTongTien.DataBindings.Add("Text", null, "SoTienQD", "{0:#,0.##}");
                cTongTien.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,0.##}");
                #endregion

                var objHD = (from p in db.ctHopDongs
                             join kh in db.tnKhachHangs on p.MaKH equals kh.MaKH
                             where p.ID == MaHD
                             select new
                             {
                                 TenKH = kh.IsCaNhan == true ? (kh.HoKH + " " + kh.TenKH) : kh.CtyTen,
                                 p.NgayKy,
                                 p.SoHDCT,
                                 TienVAT = p.ctChiTiets.Sum(o => o.TienVAT).GetValueOrDefault(),
                                 p.NgayHL,
                                 p.TyGia,
                                 p.HanTT,
                             }).First();

                var ltChiTiet = (from p in db.ctLichThanhToans
                                 where p.MaHD == MaHD
                                 select new
                                 {
                                     p.DienGiai,
                                     p.SoThang,
                                     p.SoTien,
                                     p.ctHopDong.GiaThue,
                                     p.SoTienQD,
                                 }).AsEnumerable()
                                 .Select((p, index) => new
                                 {
                                     STT = index + 1,
                                     p.DienGiai,
                                     p.SoThang,
                                     p.SoTien,
                                     p.GiaThue,
                                     p.SoTienQD,
                                 }).ToList();
                this.DataSource = ltChiTiet;

                //Thay the du lieu
                var ctlRTF = new DevExpress.XtraRichEdit.RichEditControl();
                ctlRTF.RtfText = rtHeader.Rtf;
                ctlRTF.Document.ReplaceAll("[NgayHD]", objHD.NgayKy.Value.Day.ToString(), SearchOptions.None);
                ctlRTF.Document.ReplaceAll("[ThangHD]", objHD.NgayKy.Value.Month.ToString(), SearchOptions.None);
                ctlRTF.Document.ReplaceAll("[NamHD]", objHD.NgayKy.Value.Year.ToString(), SearchOptions.None);
                ctlRTF.Document.ReplaceAll("[TenCT]", objHD.TenKH, SearchOptions.None);
                ctlRTF.Document.ReplaceAll("[SoHD]", objHD.SoHDCT, SearchOptions.None);
                rtHeader.Rtf = ctlRTF.RtfText;

                ctlRTF.RtfText = rtFooter.Rtf;
                ctlRTF.Document.ReplaceAll("[TuNgay]", string.Format("{0:dd/MM/yyyy}", objHD.NgayKy), SearchOptions.None);
                ctlRTF.Document.ReplaceAll("[HanTT]", string.Format("{0:n0}", objHD.HanTT), SearchOptions.None);
                rtFooter.Rtf = ctlRTF.RtfText;

                cSTT2.Text = (ltChiTiet.Count() + 1).ToString();
                cSTT3.Text = (ltChiTiet.Count() + 2).ToString();
                cSTT4.Text = (ltChiTiet.Count() + 3).ToString();
                cThueGTGT.Text = string.Format("{0:#,0.##}", objHD.TienVAT);
                cTyGiaUSD.Text = string.Format("Tỷ giá bán ra của đồng đô la mỹ của Ngân hàng TMCP Ngoại Thương  ngày {0:dd/MM/yyyy} là {1:#,0.##} VNĐ/USD", objHD.NgayKy, objHD.TyGia);
                cTienBC.Text = "Bằng chữ: " + new TienTeCls().DocTienBangChu(ltChiTiet.Sum(o => o.SoTien).GetValueOrDefault(), "đồng");
            }
            catch { }
        }

    }
}
