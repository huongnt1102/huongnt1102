using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
using Library;
using System.Collections.Generic;
using System.Data.Linq.SqlClient;
using DevExpress.XtraRichEdit;
using DevExpress.XtraRichEdit.API.Native;
using DevExpress.XtraReports.Parameters;

namespace LandSoftBuilding.Receivables.GiayBao
{
    public partial class rptGiayBaoPhiGiuXeKhoiDe : DevExpress.XtraReports.UI.XtraReport
    {
        byte MaTN;
        int MaKH, Thang, Nam;

        public rptGiayBaoPhiGiuXeKhoiDe(byte _MaTN, int _Thang, int _Nam, int _MaKH, List<int> _MaLDVs, int _MaTKNH)
        {
            InitializeComponent();

            var db = new MasterDataContext();
            try
            {
                Library.frmPrintControl.LoadLayout(this, 62, _MaTN);

                if (_MaKH == 0) return;

                this.MaTN = _MaTN;
                this.MaKH = _MaKH;
                this.Thang = _Thang;
                this.Nam = _Nam;

                var _NgayIn = db.GetSystemDate();
                cNgayIn.Text = string.Format(cNgayIn.Text, _NgayIn);
                cThoiGian.Text = string.Format(cThoiGian.Text, _NgayIn);
                //Add value in paramater
                this.Parameters["NgayIn"].Value = db.GetSystemDate();
                this.Parameters["ThangTB"].Value = string.Format("{0:00} - {1}", _Thang, _Nam);

                //Thong tin toa nha
                var objTN = (from tn in db.tnToaNhas
                             where tn.MaTN == _MaTN
                             select new { tn.TenTN, tn.CongTyQuanLy, tn.Logo })
                             .FirstOrDefault();
                //Logo
                imgLogo.ImageUrl = objTN.Logo;

                //Thong tin khach hang
                var objKH = (from mb in db.mbMatBangs
                             join tl in db.mbTangLaus on mb.MaTL equals tl.MaTL
                             join kn in db.mbKhoiNhas on tl.MaKN equals kn.MaKN
                             join kh in db.tnKhachHangs on mb.MaKH equals kh.MaKH
                             where mb.MaKH == _MaKH
                             select new
                             {
                                 TenKH = kh.IsCaNhan == true ? (kh.HoKH + " " + kh.TenKH): kh.CtyTen,
                                 mb.MaSoMB,
                                 kh.ThuTruoc,
                                 mb.mbTangLau.TenTL,
                                 kn.TenKN,
                             }).First();

                //Thong tin tai khoan
                var objTK = (from tk in db.nhTaiKhoans
                             join nh in db.nhNganHangs on tk.MaNH equals nh.ID
                             where tk.ID == _MaTKNH
                             select new { tk.ChuTK, tk.SoTK, nh.TenNH }).Single();

                //Thay the du lieu
                var ctlRTF = new DevExpress.XtraRichEdit.RichEditControl();
                ctlRTF.RtfText = rtHeader.Rtf;
                ctlRTF.Document.ReplaceAll("[NgayIn]", _NgayIn.ToString("dd/MM/yyyy"), SearchOptions.None);
                ctlRTF.Document.ReplaceAll("[HanThanhToan]", "", SearchOptions.None);
                ctlRTF.Document.ReplaceAll("[Thang]", _Thang.ToString(), SearchOptions.None);
                ctlRTF.Document.ReplaceAll("[Nam]", _Nam.ToString(), SearchOptions.None);
                ctlRTF.Document.ReplaceAll("[TenKH]", objKH.TenKH ?? "", SearchOptions.None);
                ctlRTF.Document.ReplaceAll("[MaSoMB]", objKH.MaSoMB ?? "", SearchOptions.None);
                ctlRTF.Document.ReplaceAll("[TenToaNha]", objTN.TenTN ?? "", SearchOptions.None);
                ctlRTF.Document.ReplaceAll("[TenCongTy]", objTN.CongTyQuanLy ?? "", SearchOptions.None);
                ctlRTF.Document.ReplaceAll("[TenTaiKhoan]", objTK.ChuTK ?? "", SearchOptions.None);
                ctlRTF.Document.ReplaceAll("[SoTaiKhoan]", objTK.SoTK ?? "", SearchOptions.None);
                ctlRTF.Document.ReplaceAll("[TenNganHang]", objTK.TenNH ?? "", SearchOptions.None);
                ctlRTF.Document.ReplaceAll("[TangLau]", objKH.TenTL ?? "", SearchOptions.None);
                ctlRTF.Document.ReplaceAll("[KhoiNha]", objKH.TenKN ?? "", SearchOptions.None);
                rtHeader.Rtf = ctlRTF.RtfText;


                // Đổ dữ liệu điện
                cDienGiai.DataBindings.Add("Text", null, "DienGiai");
                //cChiSoCu.DataBindings.Add("Text", null, "ChiSoCu", "{0:#,0.##}");
                //cChiSoMoi.DataBindings.Add("Text", null, "ChiSoMoi", "{0:#,0.##}");
                cSoLuong.DataBindings.Add("Text", null, "SoLuong", "{0:#,0.##}");
                //cHeSo.DataBindings.Add("Text", null, "HeSo", "{0:#,0.##}");
                cTien.DataBindings.Add("Text", null, "ThanhTien", "{0:#,0.##}");
                cTienVAT.DataBindings.Add("Text", null, "TienVAT", "{0:N0}");
                cTong.DataBindings.Add("Text", null, "TongTien", "{0:N0}");
                cDonGia.DataBindings.Add("Text", null, "DonGia", "{0:N0}");
                cDienGiai.DataBindings.Add("Text", null, "TenLX", "{0:N0}");

                cSumSL.DataBindings.Add("Text", null, "SoLuong", "{0:N0}");
                cSumSL.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:N0}");
                cSumTien.DataBindings.Add("Text", null, "ThanhTien", "{0:N0}");
                cSumTien.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:N0}");
                cSumVAT.DataBindings.Add("Text", null, "TienVAT", "{0:N0}");
                cSumVAT.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:N0}");
                cTongTien.DataBindings.Add("Text", null, "TongTien", "{0:N0}");
                cTongTien.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:N0}");
                //Du lieu hoa don
                var ltGiuXete = (from tx in db.dvgxTheXes
                                 join lx in db.dvgxLoaiXes on tx.MaLX equals lx.MaLX into dslx
                                 from lx in dslx.DefaultIfEmpty()
                                 // join gx in db.dvgxGiuXes on tx.MaGX equals gx.ID
                                 join dv in db.dvHoaDons on tx.ID equals dv.LinkID
                                 join pt in
                                     (
                                          from ct in db.ptChiTietPhieuThus
                                          join pthu in db.ptPhieuThus on ct.MaPT equals pthu.ID
                                          where ct.TableName == "dvHoadon" & pthu.MaTN == _MaTN
                                          select new { ct.LinkID, DaThu = true }
                                     ) on dv.ID equals pt.LinkID into dspt
                                 from pt in dspt.DefaultIfEmpty()
                                 join mb in db.mbMatBangs on dv.MaMB equals mb.MaMB
                                 join tl in db.mbTangLaus on mb.MaTL equals tl.MaTL
                                 where tx.MaTN == _MaTN & tx.MaKH == _MaKH & tx.NgungSuDung == false & dv.NgayTT.Value.Month == _Thang & dv.NgayTT.Value.Year == _Nam & pt.DaThu == null
                                 group tx by new { tx.MaLX, lx.TenLX, tx.GiaThang } into gr
                                 select new GiuXeItemKhoiDe()
                                 {
                                     MaLX = gr.Key.MaLX,
                                     TenLX =  "Tiền trông giữ " + gr.Key.TenLX,
                                     BienSo = "",
                                     SoLuong = gr.Count(),
                                     DonGia = gr.Key.GiaThang *90/100,
                                     TienVAT = gr.Count() * gr.Key.GiaThang *10/100,
                                     ThanhTien = gr.Count() * gr.Key.GiaThang * 90 / 100,
                                     TongTien = gr.Count() * gr.Key.GiaThang,
                                 }).ToList();
                foreach (var i in ltGiuXete)
                {
                    var ltBienSo = (from tx in db.dvgxTheXes
                                    //join gx in db.dvgxGiuXes on tx.MaGX equals gx.ID
                                    where tx.MaTN == _MaTN & tx.MaKH == _MaKH & tx.NgungSuDung == false & tx.MaLX == i.MaLX & tx.GiaThang == i.DonGia
                                    select tx.BienSo).ToList();

                    foreach (var bs in ltBienSo)
                        if (!string.IsNullOrEmpty(bs))
                            i.BienSo += bs + "; ";

                    i.BienSo = i.BienSo.Trim(' ').Trim(';');
                } this.DataSource = ltGiuXete;
                
                var _Ngay = new DateTime(_Nam, _Thang, 1);
                decimal _PhatSinh = (decimal)ltGiuXete.Sum(p => p.TongTien);
                var _TongTien = Convert.ToInt64(Math.Round(_PhatSinh, 0));

                cSoTienBangChu.Text = new TienTeCls().DocTienBangChu(_TongTien);
            }
            catch { }
            finally
            {
                db.Dispose();
            }
        }

        void LoadTienLai()
        {
            var db = new MasterDataContext();
            try
            {
                var objHD = (from hd in db.dvHoaDons
                             where hd.MaTN == this.MaTN & hd.MaKH == this.MaKH & hd.MaLDV == 23 & hd.NgayTT.Value.Month == this.Thang & hd.NgayTT.Value.Year == this.Nam
                             select new { hd.DienGiai, hd.ConNo }).FirstOrDefault();
                if (objHD != null)
                {
                    //cTienLai_DienGiai.Text = objHD.DienGiai;
                    //cTienLai.Text = string.Format("{0:#,0}", objHD.ConNo);
                }
            }
            catch { }
            finally
            {
                db.Dispose();
            }
        }
        int STT = 0;
        private void rtTenLDV_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            try
            {
                var _MaLDV = (int?)GetCurrentColumnValue("MaLDV");
                var _ThangDV = (_MaLDV == 5 || _MaLDV == 8 || _MaLDV == 9 || _MaLDV == 10 || _MaLDV == 11 || _MaLDV == 20 || _MaLDV == 22) ? (this.Thang - 1) : this.Thang;
                var _Nam = this.Nam;
                if (_ThangDV == 0)
                {
                    _ThangDV = 12;
                    _Nam--;
                }

                //STT++;
                //rtTenLDV.Html = string.Format("<div style='font-family:Times New Roman; font-size:9pt'><b>{0}. {1} <i>/ {2}</i> (Tháng {3:00}/{4})</b></div>",
                //    RomanNumerals.ToRoman(STT), GetCurrentColumnValue("TenLDV"),
                //    GetCurrentColumnValue("TenTA"),
                //    _ThangDV, _Nam);
            }
            catch { }
        }

        class GiuXeItemKhoiDe
        {
            public int? MaLX { get; set; }
            public string TenLX { get; set; }
            public string BienSo { get; set; }
            public int? SoLuong { get; set; }
            public decimal? DonGia { get; set; }
            public decimal? TongTien { get; set; }
            public decimal? ThanhTien { get; set; }
            public decimal? TienVAT { get; set; }
        }
    }
}
