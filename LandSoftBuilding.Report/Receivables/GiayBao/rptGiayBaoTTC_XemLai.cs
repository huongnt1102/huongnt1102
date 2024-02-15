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
    public partial class rptGiayBaoTTC_XemLai : DevExpress.XtraReports.UI.XtraReport
    {
        byte MaTN;
        int MaKH, Thang, Nam;

        public rptGiayBaoTTC_XemLai(byte _MaTN, int _Thang, int _Nam, int _MaKH, int _MaMB, List<int> _MaLDVs, int _MaTKNH)
        {
            InitializeComponent();

            var db = new MasterDataContext();
            try
            {
                Library.frmPrintControl.LoadLayout(this, 89, _MaTN);

                if (_MaKH == 0) return;

                this.MaTN = _MaTN;
                this.MaKH = _MaKH;
                this.Thang = _Thang;
                this.Nam = _Nam;

                var _NgayIn = Common.GetDateTimeSystem();
                //cNgayIn.Text = string.Format(cNgayIn.Text, _NgayIn);

                //Add value in paramater
                this.Parameters["NgayIn"].Value = Common.GetDateTimeSystem();
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
                             where mb.MaKH == _MaKH & mb.MaMB==_MaMB
                             select new
                             {
                                 TenKH = kh.IsCaNhan == true ? (kh.HoKH + " " + kh.TenKH): kh.CtyTen,
                                 mb.MaSoMB,
                                 kh.ThuTruoc,kh.MaPhu
                             }).First();

                //Thong tin tai khoan
                var objTK = (from tk in db.nhTaiKhoans
                             join nh in db.nhNganHangs on tk.MaNH equals nh.ID
                             where tk.ID == _MaTKNH
                             select new { tk.ChuTK, tk.SoTK, nh.TenNH,ChiNhanhNH=tk.DienGiai }).Single();

                //Thay the du lieu
                var ctlRTF = new DevExpress.XtraRichEdit.RichEditControl();
                ctlRTF.RtfText = rtHeader.Rtf;
                ctlRTF.Document.ReplaceAll("[NgayIn]", _NgayIn.ToString("dd/MM/yyyy"), SearchOptions.None);
                ctlRTF.Document.ReplaceAll("[HanThanhToan]", "", SearchOptions.None);
                ctlRTF.Document.ReplaceAll("[Thang]", _Thang.ToString(), SearchOptions.None);
                ctlRTF.Document.ReplaceAll("[MaPhu]", objKH.MaPhu.ToString(), SearchOptions.None);
                ctlRTF.Document.ReplaceAll("[Nam]", _Nam.ToString(), SearchOptions.None);
                ctlRTF.Document.ReplaceAll("[TenKH]", objKH.TenKH ?? "", SearchOptions.None);
                ctlRTF.Document.ReplaceAll("[MaSoMB]", objKH.MaSoMB ?? "", SearchOptions.None);
                ctlRTF.Document.ReplaceAll("[TenToaNha]", objTN.TenTN ?? "", SearchOptions.None);
                ctlRTF.Document.ReplaceAll("[TenCongTy]", objTN.CongTyQuanLy ?? "", SearchOptions.None);
                ctlRTF.Document.ReplaceAll("[TenTaiKhoan]", objTK.ChuTK ?? "", SearchOptions.None);
                ctlRTF.Document.ReplaceAll("[SoTaiKhoan]", objTK.SoTK ?? "", SearchOptions.None);
                ctlRTF.Document.ReplaceAll("[TenNganHang]", objTK.TenNH ?? "", SearchOptions.None);
                ctlRTF.Document.ReplaceAll("[ChiNhanhNH]", objTK.ChiNhanhNH ?? "", SearchOptions.None);
                ctlRTF.Document.ReplaceAll("[ThangTA]", Commoncls.GetMonth(_Thang) ?? "", SearchOptions.None);
                ctlRTF.Document.ReplaceAll("[HopDongSo]", "", SearchOptions.None);
              
                rtHeader.Rtf = ctlRTF.RtfText;

                ctlRTF.RtfText = rtFooter.Rtf;
                ctlRTF.Document.ReplaceAll("[NgayIn]", _NgayIn.ToString("dd/MM/yyyy"), SearchOptions.None);
                ctlRTF.Document.ReplaceAll("[ThangTA]", Commoncls.GetMonth(_Thang) ?? "", SearchOptions.None);
                ctlRTF.Document.ReplaceAll("[Thang]", _Thang.ToString(), SearchOptions.None);
                ctlRTF.Document.ReplaceAll("[Nam]", _Nam.ToString(), SearchOptions.None);
                ctlRTF.Document.ReplaceAll("[TenKH]", objKH.TenKH ?? "", SearchOptions.None);
                ctlRTF.Document.ReplaceAll("[MaSoMB]", objKH.MaSoMB ?? "", SearchOptions.None);
                ctlRTF.Document.ReplaceAll("[TenToaNha]", objTN.TenTN ?? "", SearchOptions.None);
                ctlRTF.Document.ReplaceAll("[TenCongTy]", objTN.CongTyQuanLy ?? "", SearchOptions.None);
                ctlRTF.Document.ReplaceAll("[ChuTK]", objTK.ChuTK ?? "", SearchOptions.None);
                ctlRTF.Document.ReplaceAll("[SoTK]", objTK.SoTK ?? "", SearchOptions.None);
                ctlRTF.Document.ReplaceAll("[TenNH]", objTK.TenNH ?? "", SearchOptions.None);
                ctlRTF.Document.ReplaceAll("[ChiNhanhNH]", objTK.ChiNhanhNH ?? "", SearchOptions.None);
                rtFooter.Rtf = ctlRTF.RtfText;
                var _Ngay = new DateTime(_Nam, _Thang, 1);
                //Du lieu hoa don
                var ltLoaiDichVu = (from hd in db.dvHoaDons
                                    join ldv in db.dvLoaiDichVus on hd.MaLDV equals ldv.ID
                                    where hd.MaLDV != 23 & hd.IsDuyet == true & hd.MaKH == _MaKH & hd.NgayTT.Value.Month == _Thang
                                    & hd.NgayTT.Value.Year == _Nam
                                    & (_MaLDVs.Contains(hd.MaLDV.Value) | _MaLDVs.Count == 0)
                                    & hd.PhaiThu > 0
                                    & (hd.PhaiThu.GetValueOrDefault()
                                    - (db.SoQuy_ThuChis.Where(p => p.LinkID == hd.ID & p.TableName == "dvHoaDon" & SqlMethods.DateDiffDay(p.NgayPhieu, new DateTime(_Nam, _Thang, 1)) > 0).Sum(p => p.DaThu + p.KhauTru)).GetValueOrDefault()
                                    )> 0  //moi mo 5/11/2016
                                    group hd by new { ldv.STT, hd.MaLDV, ldv.TenHienThi, ldv.TenTA } into gr
                                    orderby gr.Key.STT
                                    select new
                                    {
                                        gr.Key.STT,
                                        gr.Key.MaLDV,
                                        TenLDV = gr.Key.TenHienThi,
                                        gr.Key.TenTA,
                                        SoTien = gr.Key.MaLDV == 49 ? -gr.Sum(p => p.PhaiThu) : gr.Sum(p => p.PhaiThu),
                                    }).ToList();

                var ltData = (from hd in db.dvHoaDons
                              join ldv in db.dvLoaiDichVus on hd.MaLDV equals ldv.ID
                              where hd.MaKH == _MaKH & hd.NgayTT.Value.Month == _Thang & hd.NgayTT.Value.Year == _Nam
                             // & hd.ConNo.GetValueOrDefault() > 0
                              & (_MaLDVs.Contains(hd.MaLDV.Value) | _MaLDVs.Count == 0)
                              & (hd.PhaiThu.GetValueOrDefault() 
                              - (db.SoQuy_ThuChis.Where(p => p.LinkID == hd.ID & p.TableName == "dvHoaDon" & SqlMethods.DateDiffDay(p.NgayPhieu, new DateTime(_Nam,_Thang,1))>0).Sum(p => p.DaThu+ p.KhauTru)).GetValueOrDefault())> 0
                              & hd.IsDuyet == true
                              group hd by new { hd.MaLDV, ldv.TenHienThi, ldv.TenTA } into gr
                              select new
                              {
                                  gr.Key.MaLDV,
                                  TenLDV = gr.Key.TenHienThi,
                                  gr.Key.TenTA,
                                  SoTien = gr.Sum(p => p.PhaiThu.GetValueOrDefault() - (db.SoQuy_ThuChis.Where(sq => sq.LinkID == p.ID & sq.TableName == "dvHoaDon" & SqlMethods.DateDiffDay(sq.NgayPhieu, new DateTime(_Nam, _Thang, 1)) > 0).Sum(s => s.DaThu + s.KhauTru)).GetValueOrDefault())
                              }).ToList();

                this.DataSource = ltData;

                // Số tiền thu thừa từ tháng trước
                var date = new DateTime(_Nam, _Thang, 1);
               

               
                
                var _NoCu = (from hd in db.dvHoaDons
                             where hd.MaKH == _MaKH
                             & hd.IsThuThua.GetValueOrDefault() == false
                             & (hd.PhaiThu.GetValueOrDefault()
                             - (db.SoQuy_ThuChis.Where(p => p.LinkID == hd.ID & p.TableName == "dvHoaDon" & SqlMethods.DateDiffDay(p.NgayPhieu, new DateTime(_Nam, _Thang, 1)) > 0).Sum(p => p.DaThu + p.KhauTru-p.ThuThua)).GetValueOrDefault()
                             ) != 0
                             & SqlMethods.DateDiffDay(hd.NgayTT, new DateTime(_Nam, _Thang, 1)) > 0
                                 & (_MaLDVs.Contains(hd.MaLDV.Value) | _MaLDVs.Count == 0) & hd.IsDuyet == true

                             select (hd.PhaiThu - (db.SoQuy_ThuChis.Where(sq => sq.LinkID == hd.ID & sq.TableName == "dvHoaDon" & SqlMethods.DateDiffDay(sq.NgayPhieu, new DateTime(_Nam, _Thang, 1)) > 0).Sum(s => s.DaThu + s.KhauTru-s.ThuThua)).GetValueOrDefault())).Sum().GetValueOrDefault();
                if (_NoCu < 0)
                {
                    _NoCu = 0;
                }

              
                var _PhatSinh = ltLoaiDichVu.Sum(p => p.SoTien).GetValueOrDefault();
                var _TongTien = Convert.ToInt64(Math.Round(_PhatSinh  + _NoCu , 0));
                cSumSoTien.Text = string.Format("{0:#,0}  VNĐ", _PhatSinh);
                csumNoCu.Text = string.Format("{0:#,0}  VNĐ", _NoCu);
               
                csumTongTien.Text = string.Format("{0:#,0}  VNĐ", _TongTien > 0 ? _TongTien : 0);
                cSoThanhtoan.Text = string.Format("{0}. Số thanh toán Payment:", RomanNumerals.ToRoman((int)(ltData.Count() + 1)));
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

        private void rptDichVu_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            try
            {
                var _MaLDV = (int?)GetCurrentColumnValue("MaLDV");
                switch (_MaLDV)
                {
                    case 2:
                        rptDichVu.ReportSource = new rptTienThue_XemLai(this.MaTN, this.MaKH, this.Thang, this.Nam);
                        break;
                    case 3:
                        rptDichVu.ReportSource = new rptTienSuaChua_XemLai(this.MaTN, this.MaKH, this.Thang, this.Nam);
                        break;
                    case 4:
                        rptDichVu.ReportSource = new rptTienDatCoc_XemLai(this.MaTN, this.MaKH, this.Thang, this.Nam);
                        break;
                    case 5:
                        rptDichVu.ReportSource = new rptTienDien3Pha_XemLai(this.MaTN, this.MaKH, this.Thang, this.Nam);
                        break;
                    case 6:
                        rptDichVu.ReportSource = new rptTheXeTTC_XemLai(this.MaTN, this.MaKH, this.Thang, this.Nam);
                        break;
                    case 8:
                        rptDichVu.ReportSource = new rptTienDienTTC_XemLai(this.MaTN, this.MaKH, this.Thang, this.Nam);
                        break;
                    case 9:
                        rptDichVu.ReportSource = new rptTienNuocTTC_XemLai(this.MaTN, this.MaKH, this.Thang, this.Nam);
                        break;
                    case 10:
                        rptDichVu.ReportSource = new rptTienGas_XemLai(this.MaTN, this.MaKH, this.Thang, this.Nam);
                        break;
                    case 11:
                        rptDichVu.ReportSource = new rptDieuHoaNgoaiGio(this.MaTN, this.MaKH, this.Thang, this.Nam);
                        break;
                    case 20:
                        rptDichVu.ReportSource = new rptTienNuocNong(this.MaTN, this.MaKH, this.Thang, this.Nam);
                        break;
                    case 22:
                        rptDichVu.ReportSource = new rptTienNuocSinhHoatThangLong(this.MaTN, this.MaKH, this.Thang, this.Nam);
                        break;
                    case 23: //Tien lai
                        this.LoadTienLai();
                        break;
                    case 49: //Tien lai
                        rptDichVu.ReportSource = new rptDoanhThuGiamTru(this.MaTN, this.MaKH, this.Thang, this.Nam, _MaLDV.Value);
                        break;
                    case 13: //Tien lai
                        rptDichVu.ReportSource = new rptPQLTTC_XemLai(this.MaTN, this.MaKH, this.Thang, this.Nam, _MaLDV.Value);
                        break;
                    default:
                        rptDichVu.ReportSource = new rptDichVuCoBanTTC(this.MaTN, this.MaKH, this.Thang, this.Nam, _MaLDV.Value);
                        break;
                }
            }
            catch { }
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

                STT++;
                if (_MaLDV == 9)
                {
                   // cTuNgayDenNgay.Text = string.Format("Từ/From    {0:dd/MM/yyyy}           Đến/to     {1:dd/MM/yyyy}", objNuoc.TuNgay, objNuoc.DenNgay);
                    MasterDataContext db = new MasterDataContext();
                    var objNuoc = (from hd in db.dvHoaDons
                                   join tn in db.dvNuocs on new { hd.TableName, hd.LinkID } equals new { TableName = "dvNuoc", LinkID = (int?)tn.ID }
                                   where hd.MaTN == MaTN & hd.MaKH == MaKH & hd.NgayTT.Value.Month == Thang & hd.NgayTT.Value.Year == Nam
                                   select new
                                   {
                                       tn.ID,
                                       tn.ChiSoCu,
                                       tn.ChiSoMoi,
                                       tn.TuNgay,
                                       tn.DenNgay,
                                       SoTieuThu = tn.SoTieuThu.GetValueOrDefault() + tn.SoTieuThuDHCu.GetValueOrDefault(),
                                       SoUuDai = (from ud in db.dvNuocUuDais where ud.MaMB == tn.MaMB select ud.SoNguoi).FirstOrDefault(),
                                       ConNo = hd.PhaiThu - (db.SoQuy_ThuChis.Where(p=>p.LinkID==hd.ID & SqlMethods.DateDiffDay(p.NgayPhieu, new DateTime(_Nam, Thang, 1)) > 0).Sum(s=>s.DaThu + s.KhauTru).GetValueOrDefault())
                                   }).First();
                    rtTenLDV.Html = string.Format("<div style='font-family:Times New Roman; font-size:9pt'><b>{0}. {1} <i>/ {2}</i> ({3})</b></div>",
                       RomanNumerals.ToRoman(STT), GetCurrentColumnValue("TenLDV"),
                       GetCurrentColumnValue("TenTA"),
                       string.Format("Từ/From    {0:dd/MM/yyyy}           Đến/to     {1:dd/MM/yyyy}", objNuoc.TuNgay, objNuoc.DenNgay));
                }
                else
                {
                    rtTenLDV.Html = string.Format("<div style='font-family:Times New Roman; font-size:9pt'><b>{0}. {1} <i>/ {2}</i> (Tháng {3:00}/{4})</b></div>",
                                       RomanNumerals.ToRoman(STT), GetCurrentColumnValue("TenLDV"),
                                       GetCurrentColumnValue("TenTA"),
                                       _ThangDV, _Nam);
                }
               
            }
            catch { }
        }
    }
}
