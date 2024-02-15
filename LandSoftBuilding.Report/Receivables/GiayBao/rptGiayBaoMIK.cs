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
    public partial class rptGiayBaoMIK : DevExpress.XtraReports.UI.XtraReport
    {
        byte MaTN;
        int MaKH, Thang, Nam;

        public rptGiayBaoMIK(byte _MaTN, int _Thang, int _Nam, int _MaKH, List<int> _MaLDVs, int _MaTKNH)
        {
            InitializeComponent();

            var db = new MasterDataContext();
            try
            {
                //Library.frmPrintControl.LoadLayout(this, 2, _MaTN);

                if (_MaKH == 0) return;

                this.MaTN = _MaTN;
                this.MaKH = _MaKH;
                this.Thang = _Thang;
                this.Nam = _Nam;

                var _NgayIn = db.GetSystemDate();
                //cNgayIn.Text = string.Format(cNgayIn.Text, _NgayIn);
                var _DenNgay = Common.GetLastDayOfMonth(_Thang, _Nam);
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
                             join kh in db.tnKhachHangs on mb.MaKH equals kh.MaKH
                             where mb.MaKH == _MaKH
                             select new
                             {
                                 TenKH = kh.IsCaNhan == true ? (kh.HoKH + " " + kh.TenKH): kh.CtyTen,
                                 mb.MaSoMB,
                                 //Sửa số tiền thu trước
                                 ThuTruoc = (from pt in db.ptPhieuThus
                                             where pt.MaKH == _MaKH & pt.MaPL == 2 & SqlMethods.DateDiffDay(pt.NgayThu, _DenNgay) >= 0
                                             select pt.SoTien).Sum().GetValueOrDefault()
                                              - (from pt in db.ktttKhauTruThuTruocs
                                                 where pt.MaKH == _MaKH & SqlMethods.DateDiffDay(pt.NgayCT, _DenNgay) >= 0
                                                 select pt.SoTien).Sum().GetValueOrDefault()
                             }).First();

                //Thong tin tai khoan
                var objTK = (from tk in db.nhTaiKhoans
                             join nh in db.nhNganHangs on tk.MaNH equals nh.ID
                             where tk.ID == _MaTKNH
                             select new { tk.ChuTK, tk.SoTK, nh.TenNH }).Single();

                //Đổ dữ liêu
                cSTT.DataBindings.Add("Text", null, "STT");
                cNoiDung.DataBindings.Add("Text", null, "NoiDung");
                cSoTien.DataBindings.Add("Text", null, "SoTien", "{0:#,0.##}");


                cNgayTB.Text = string.Format("Ngày {0:dd} tháng {0:MM} năm {0:yyyy}", NgayIn.Value);
                cTenKH.Text = objKH.TenKH;
                cMaSoMB.Text = string.Format("Căn hộ: {0}", objKH.MaSoMB);
                cSoTK.Text = objTK.SoTK;
                cTenNH.Text = objTK.TenNH;
                //Thay the du lieu
                var ctlRTF = new DevExpress.XtraRichEdit.RichEditControl();
                //ctlRTF.RtfText = rtHeader.Rtf;
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
                //rtHeader.Rtf = ctlRTF.RtfText;

                ctlRTF.RtfText = rtFooter.Rtf;
                ctlRTF.Document.ReplaceAll("[NgayIn]", _NgayIn.ToString("dd/MM/yyyy"), SearchOptions.None);
                ctlRTF.Document.ReplaceAll("[Thang]", _Thang.ToString(), SearchOptions.None);
                ctlRTF.Document.ReplaceAll("[Nam]", _Nam.ToString(), SearchOptions.None);
                ctlRTF.Document.ReplaceAll("[TenKH]", objKH.TenKH ?? "", SearchOptions.None);
                ctlRTF.Document.ReplaceAll("[MaSoMB]", objKH.MaSoMB ?? "", SearchOptions.None);
                ctlRTF.Document.ReplaceAll("[TenToaNha]", objTN.TenTN ?? "", SearchOptions.None);
                ctlRTF.Document.ReplaceAll("[TenCongTy]", objTN.CongTyQuanLy ?? "", SearchOptions.None);
                ctlRTF.Document.ReplaceAll("[TenTaiKhoan]", objTK.ChuTK ?? "", SearchOptions.None);
                ctlRTF.Document.ReplaceAll("[SoTaiKhoan]", objTK.SoTK ?? "", SearchOptions.None);
                ctlRTF.Document.ReplaceAll("[TenNganHang]", objTK.TenNH ?? "", SearchOptions.None);
                rtFooter.Rtf = ctlRTF.RtfText;

                //Du lieu hoa don
                DateTime date = new DateTime(_Nam, _Thang, 1);
                var ltData = (from hd in db.dvHoaDons
                              join ldv in db.dvLoaiDichVus on hd.MaLDV equals ldv.ID
                              where hd.MaKH == _MaKH & hd.NgayTT.Value.Month == _Thang & hd.NgayTT.Value.Year == _Nam & hd.ConNo.GetValueOrDefault() > 0
                                & (_MaLDVs.Contains(hd.MaLDV.Value) | _MaLDVs.Count == 0) & hd.IsDuyet == true
                              group hd by new { hd.MaLDV, ldv.TenHienThi, ldv.TenTA, Thang = hd.NgayTT.Value.Month, Nam = hd.NgayTT.Value.Year} into gr
                              select new DichVuItem
                              {
                                  MaLDV = gr.Key.MaLDV,
                                  Thang = gr.Key.Thang,
                                  Nam = gr.Key.Nam,
                                  NoiDung = string.Format("{0}: {1}/{2}",gr.Key.TenHienThi, gr.Key.MaLDV == 8 | gr.Key.MaLDV == 9 | gr.Key.MaLDV == 10 ? Thang -1 : Thang  ,gr.Key.Nam),
                                  SoTien = gr.Sum(p => p.ConNo)
                              }).AsEnumerable()
                              .Select((p, Index) => new
                              {
                                  STT = Index +1,
                                  p.MaLDV,
                                  p.NoiDung,
                                  p.SoTien,
                              }).ToList();

                if (ltData.Any(o => o.MaLDV == 9))
                {
                    xrSubreport1.ReportSource = new rptsubTienNuoc(this.MaTN, this.MaKH, this.Thang, this.Nam);
                }
                this.DataSource = ltData;
                
                var _Ngay = new DateTime(_Nam, _Thang, 1);
                var _NoCu = (from hd in db.dvHoaDons
                             where hd.MaKH == _MaKH & hd.ConNo.GetValueOrDefault() > 0 & SqlMethods.DateDiffDay(hd.NgayTT, _Ngay) > 0
                                 & (_MaLDVs.Contains(hd.MaLDV.Value) | _MaLDVs.Count == 0)
                             select hd.ConNo).Sum().GetValueOrDefault();
                var _PhatSinh = ltData.Sum(p => p.SoTien).GetValueOrDefault();
                var _TongTien = Convert.ToInt64(Math.Round(_PhatSinh + _NoCu, 0));

                cSumSoTien.Text = string.Format("{0:#,0}", _PhatSinh);
                //csumNoCu.Text = string.Format("{0:#,0}", _NoCu);
                //csumThuTruoc.Text = string.Format("{0:#,0}", objKH.ThuTruoc);
                //csumTongTien.Text = string.Format("{0:#,0}", _PhatSinh + _NoCu);
                //cSoTienBangChu.Text = new TienTeCls().DocTienBangChu(_TongTien);
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
                //if (objHD != null)
                //{
                //    cTienLai_DienGiai.Text = objHD.DienGiai;
                //    cTienLai.Text = string.Format("{0:#,0}", objHD.ConNo);
                //}
            }
            catch { }
            finally
            {
                db.Dispose();
            }
        }

        private void rptDichVu_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            try
            {
                var _MaLDV = (int?)GetCurrentColumnValue("MaLDV");
                //switch (_MaLDV)
                //{
                //    case 2:
                //        rptDichVu.ReportSource = new rptTienThue(this.MaTN, this.MaKH, this.Thang, this.Nam);
                //        break;
                //    case 3:
                //        rptDichVu.ReportSource = new rptTienSuaChua(this.MaTN, this.MaKH, this.Thang, this.Nam);
                //        break;
                //    case 4:
                //        rptDichVu.ReportSource = new rptTienDatCoc(this.MaTN, this.MaKH, this.Thang, this.Nam);
                //        break;
                //    case 5:
                //        rptDichVu.ReportSource = new rptTienDien3Pha(this.MaTN, this.MaKH, this.Thang, this.Nam);
                //        break;
                //    case 6:
                //        rptDichVu.ReportSource = new rptTheXe(this.MaTN, this.MaKH, this.Thang, this.Nam);
                //        break;
                //    case 8:
                //        rptDichVu.ReportSource = new rptTienDien(this.MaTN, this.MaKH, this.Thang, this.Nam);
                //        break;
                //    case 9:
                //        rptDichVu.ReportSource = new rptTienNuoc(this.MaTN, this.MaKH, this.Thang, this.Nam);
                //        break;
                //    case 10:
                //        rptDichVu.ReportSource = new rptTienGas(this.MaTN, this.MaKH, this.Thang, this.Nam);
                //        break;
                //    case 11:
                //        rptDichVu.ReportSource = new rptDieuHoaNgoaiGio(this.MaTN, this.MaKH, this.Thang, this.Nam);
                //        break;
                //    case 20:
                //        rptDichVu.ReportSource = new rptTienNuocNong(this.MaTN, this.MaKH, this.Thang, this.Nam);
                //        break;
                //    case 22:
                //        rptDichVu.ReportSource = new rptTienNuocSinhHoat(this.MaTN, this.MaKH, this.Thang, this.Nam);
                //        break;
                //    case 23: //Tien lai
                //        this.LoadTienLai();
                //        break;
                //    default:
                //        rptDichVu.ReportSource = new rptDichVuCoBan(this.MaTN, this.MaKH, this.Thang, this.Nam, _MaLDV.Value);
                //        break;
                //}
            }
            catch { }
        }

        int STT = 0;
        private void rtTenLDV_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            try
            {
                //var _MaLDV = (int?)GetCurrentColumnValue("MaLDV");
                //var _ThangDV = (_MaLDV == 5 || _MaLDV == 8 || _MaLDV == 9 || _MaLDV == 10 || _MaLDV == 11 || _MaLDV == 20 || _MaLDV == 22) ? (this.Thang - 1) : this.Thang;
                //var _Nam = this.Nam;
                //if (_ThangDV == 0)
                //{
                //    _ThangDV = 12;
                //    _Nam--;
                //}

                //STT++;
                //rtTenLDV.Html = string.Format("<div style='font-family:Times New Roman; font-size:9pt'><b>{0}. {1} <i>/ {2}</i> (Tháng {3:00}/{4})</b></div>",
                //    RomanNumerals.ToRoman(STT), GetCurrentColumnValue("TenLDV"),
                //    GetCurrentColumnValue("TenTA"),
                //    _ThangDV, _Nam);
            }
            catch { }
        }

        private void rptGiayBaoMIK_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //var _MaLDV = (int?)GetCurrentColumnValue("MaLDV");
        }

        class DichVuItem
        {
            public int? MaLDV { get; set; }
            public string TenLDV { get; set; }
            public string NoiDung { get; set; }
            public decimal? SoTien { get; set; }
            public string DienGiai { get; set; }
            public int? Thang { get; set; }
            public int? Nam { get; set; }
        }
    }
}
