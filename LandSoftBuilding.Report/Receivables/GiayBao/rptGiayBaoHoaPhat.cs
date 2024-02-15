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
    public partial class rptGiayBaoHoaPhat : DevExpress.XtraReports.UI.XtraReport
    {
        byte MaTN;
        int MaKH, Thang, Nam;

        public rptGiayBaoHoaPhat(byte _MaTN, int _Thang, int _Nam, int _MaKH, List<int> _MaLDVs, int _MaTKNH)
        {
            InitializeComponent();

            var db = new MasterDataContext();
            try
            {
                //Library.frmPrintControl.LoadLayout(this, 58, _MaTN);

                if (_MaKH == 0) return;

                this.MaTN = _MaTN;
                this.MaKH = _MaKH;
                this.Thang = _Thang;
                this.Nam = _Nam;

                var _NgayIn = db.GetSystemDate();
               // cNgayIn.Text = string.Format(cNgayIn.Text, _NgayIn);

                //Add value in paramater
                this.Parameters["NgayIn"].Value = db.GetSystemDate();
                this.Parameters["ThangTB"].Value = string.Format("{0:00} - {1}", _Thang, _Nam);
                cNgayTB.Text = string.Format("Ngày {0} tháng {1} năm {2}", DateTime.Now.Day, DateTime.Now.Month,
                    DateTime.Now.Year);
                //Thong tin toa nha
                var objTN = (from tn in db.tnToaNhas
                             where tn.MaTN == _MaTN
                             select new { tn.TenTN, tn.CongTyQuanLy, tn.Logo })
                             .FirstOrDefault();
                //Logo
                //imgLogo.ImageUrl = objTN.Logo;

                //Thong tin khach hang
                var objKH = (from mb in db.mbMatBangs
                             join kh in db.tnKhachHangs on mb.MaKH equals kh.MaKH
                             where mb.MaKH == _MaKH
                             select new
                             {
                                 TenKH = kh.IsCaNhan == true ? (kh.HoKH + " " + kh.TenKH): kh.CtyTen,
                                 mb.MaSoMB,
                                 kh.ThuTruoc
                             }).First();

                //Thong tin tai khoan
                var objTK = (from tk in db.nhTaiKhoans
                             join nh in db.nhNganHangs on tk.MaNH equals nh.ID
                             where tk.ID == _MaTKNH
                             select new { tk.ChuTK, tk.SoTK, nh.TenNH }).Single();
                var rpt = new rptXeHoaPhat(MaTN,MaKH,Thang,Nam);
                xrSubreport1.ReportSource = rpt;
                //Thay the du lieu
                var ctlRTF = new DevExpress.XtraRichEdit.RichEditControl();
                //ctlRTF.RtfText = rtHeader.Rtf;
                //ctlRTF.Document.ReplaceAll("[NgayIn]", _NgayIn.ToString("dd/MM/yyyy"), SearchOptions.None);
                //ctlRTF.Document.ReplaceAll("[HanThanhToan]", "", SearchOptions.None);
                //ctlRTF.Document.ReplaceAll("[Thang]", _Thang.ToString(), SearchOptions.None);
                //ctlRTF.Document.ReplaceAll("[Nam]", _Nam.ToString(), SearchOptions.None);
                //ctlRTF.Document.ReplaceAll("[TenKH]", objKH.TenKH ?? "", SearchOptions.None);
                //ctlRTF.Document.ReplaceAll("[MaSoMB]", objKH.MaSoMB ?? "", SearchOptions.None);
                //ctlRTF.Document.ReplaceAll("[TenToaNha]", objTN.TenTN ?? "", SearchOptions.None);
                //ctlRTF.Document.ReplaceAll("[TenCongTy]", objTN.CongTyQuanLy ?? "", SearchOptions.None);
                //ctlRTF.Document.ReplaceAll("[TenTaiKhoan]", objTK.ChuTK ?? "", SearchOptions.None);
                //ctlRTF.Document.ReplaceAll("[SoTK]", objTK.SoTK ?? "", SearchOptions.None);
                //ctlRTF.Document.ReplaceAll("[TenNganHang]", objTK.TenNH ?? "", SearchOptions.None);
                //rtHeader.Rtf = ctlRTF.RtfText;
                cSoTK.Text = string.Format("{0}", objTK.SoTK);
                cNganHang.Text = string.Format("Tại: {0}", objTK.TenNH);
                cTenKH.Text = string.Format("{0}", objKH.TenKH.ToString().Trim());
                cCanHo.Text = string.Format("Căn hộ: {0}", objKH.MaSoMB);
                //ctlRTF.RtfText = rtFooter.Rtf;
                var NgayDong = new DateTime(Nam, Thang, 7);
                //cHanThanhToan.Text = string.Format("Thời hạn thanh toán: {0:dd/MM/yyyy} - {1:dd/MM/yyyy}", Ngay, Ngay.AddDays(9));
                //ctlRTF.Document.ReplaceAll("[NgayFrom]", NgayDong.ToString("dd/MM/yyyy"), SearchOptions.None);
               // ctlRTF.Document.ReplaceAll("[NgayTo]", NgayDong.AddDays(3).ToString("dd/MM/yyyy"), SearchOptions.None);
                //ctlRTF.Document.ReplaceAll("[Nam]", _Nam.ToString(), SearchOptions.None);
                //ctlRTF.Document.ReplaceAll("[TenKH]", objKH.TenKH ?? "", SearchOptions.None);
                //ctlRTF.Document.ReplaceAll("[MaSoMB]", objKH.MaSoMB ?? "", SearchOptions.None);
                //ctlRTF.Document.ReplaceAll("[TenToaNha]", objTN.TenTN ?? "", SearchOptions.None);
                //ctlRTF.Document.ReplaceAll("[TenCongTy]", objTN.CongTyQuanLy ?? "", SearchOptions.None);
                //ctlRTF.Document.ReplaceAll("[TenTaiKhoan]", objTK.ChuTK ?? "", SearchOptions.None);
                //ctlRTF.Document.ReplaceAll("[SoTaiKhoan]", objTK.SoTK ?? "", SearchOptions.None);
                //ctlRTF.Document.ReplaceAll("[TenNganHang]", objTK.TenNH ?? "", SearchOptions.None);
                //rtFooter.Rtf = ctlRTF.RtfText;
                var _Ngay = new DateTime(_Nam, _Thang, 1);
                //Du lieu hoa don
                var ltLoaiDichVu = (from hd in db.dvHoaDons
                                    join ldv in db.dvLoaiDichVus on hd.MaLDV equals ldv.ID
                                    where hd.MaLDV != 23  & hd.IsDuyet == true & hd.MaKH == _MaKH //& hd.NgayTT.Value.Month == _Thang
                                    & hd.NgayTT.Value.Year <= _Nam
                                    & (_MaLDVs.Contains(hd.MaLDV.Value) | _MaLDVs.Count == 0)
                                    & hd.PhaiThu > 0
                                    & (hd.PhaiThu.GetValueOrDefault() - (db.ptChiTietPhieuThus.Where(p => p.LinkID == hd.ID & p.TableName == "dvHoaDon").Sum(p => p.SoTien)).GetValueOrDefault()) > 0 
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
                var ltLoaiDichVuPS = (from hd in db.dvHoaDons
                                    join ldv in db.dvLoaiDichVus on hd.MaLDV equals ldv.ID
                                    where hd.MaLDV != 23 & hd.IsDuyet == true & hd.MaKH == _MaKH //& hd.NgayTT.Value.Month == _Thang
                                    & hd.NgayTT.Value.Year == _Nam
                                    & (_MaLDVs.Contains(hd.MaLDV.Value) | _MaLDVs.Count == 0)
                                    & hd.PhaiThu > 0
                                    & (hd.PhaiThu.GetValueOrDefault() - (db.ptChiTietPhieuThus.Where(p => p.LinkID == hd.ID & p.TableName == "dvHoaDon").Sum(p => p.SoTien)).GetValueOrDefault()) > 0
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
                              where hd.MaKH == _MaKH 
                              //& hd.NgayTT.Value.Month == _Thang 
                              & hd.NgayTT.Value.Year <= _Nam
                              & hd.ConNo.GetValueOrDefault() > 0 
                              & hd.MaLDV!=13
                              & hd.MaLDV != 6
                                & (_MaLDVs.Contains(hd.MaLDV.Value) | _MaLDVs.Count == 0)
                              group hd by new { hd.MaLDV, ldv.TenHienThi, ldv.TenTA } into gr
                              select new
                              {
                                  gr.Key.MaLDV,
                                  TenLDV = gr.Key.TenHienThi,
                                  gr.Key.TenTA,
                                  SoTien = gr.Sum(p => p.ConNo)
                              }).ToList();

                this.DataSource = ltData;

                // Số tiền thu thừa từ tháng trước
                var date = new DateTime(_Nam, _Thang, 1);
                var ltThuThua1 = (from hd in db.dvHoaDons
                                    join ldv in db.dvLoaiDichVus on hd.MaLDV equals ldv.ID
                                    where hd.MaLDV != 23 & hd.IsDuyet == true & hd.MaKH == _MaKH & SqlMethods.DateDiffMonth(hd.NgayTT,date) > 0
                                    & hd.NgayTT.Value.Year == _Nam
                                    & (_MaLDVs.Contains(hd.MaLDV.Value) | _MaLDVs.Count == 0)
                                    & hd.PhaiThu.GetValueOrDefault() > 0 
                                    & (hd.PhaiThu.GetValueOrDefault() - (db.ptChiTietPhieuThus.Where(p => p.LinkID == hd.ID & p.TableName == "dvHoaDon").Sum(p => p.SoTien)).GetValueOrDefault()) < 0
                                    select new
                                    {
                                        SoTien = -(hd.PhaiThu.GetValueOrDefault() - (db.ptChiTietPhieuThus.Where(p => p.LinkID == hd.ID & p.TableName == "dvHoaDon").Sum(p => p.SoTien)).GetValueOrDefault()),
                                    }).ToList();

                // Hóa đơn thu trước
                var ltThuThua2 = (from hd in db.dvHoaDons
                                  join ldv in db.dvLoaiDichVus on hd.MaLDV equals ldv.ID
                                  where hd.MaLDV != 23 & hd.IsDuyet == true & hd.MaKH == _MaKH & SqlMethods.DateDiffMonth(hd.NgayTT, date) == 0
                                  //& (hd.PhaiThu.GetValueOrDefault() - (db.ptChiTietPhieuThus.Where(p => p.LinkID == hd.ID & p.TableName == "dvHoaDon" & p.IsThuThua == true).Sum(p => p.SoTien)).GetValueOrDefault()) > 0
                                  & hd.IsThuThua.GetValueOrDefault() == true
                                  select new
                                  {
                                      SoTien = db.ptChiTietPhieuThus.Where(p => p.LinkID == hd.ID & p.TableName == "dvHoaDon" & p.IsThuThua == true).Sum(p => p.SoTien).GetValueOrDefault(),
                                  }).ToList();
                
                // Hóa đơn giảm trừ
                var ltThuThua3 = (from hd in db.dvHoaDons
                                  join ldv in db.dvLoaiDichVus on hd.MaLDV equals ldv.ID
                                  where hd.MaLDV != 23 & hd.IsDuyet == true & hd.MaKH == _MaKH & SqlMethods.DateDiffMonth(hd.NgayTT, date) >= 0
                                  & hd.NgayTT.Value.Year == _Nam
                                  & (_MaLDVs.Contains(hd.MaLDV.Value) | _MaLDVs.Count == 0)
                                  & hd.PhaiThu.GetValueOrDefault() < 0
                                  & (hd.PhaiThu.GetValueOrDefault() - (db.ptChiTietPhieuThus.Where(p => p.LinkID == hd.ID & p.TableName == "dvHoaDon").Sum(p => p.SoTien)).GetValueOrDefault()) != 0
                                  select new
                                  {
                                      SoTien = -(hd.PhaiThu.GetValueOrDefault() - (db.ptChiTietPhieuThus.Where(p => p.LinkID == hd.ID & p.TableName == "dvHoaDon").Sum(p => p.SoTien)).GetValueOrDefault()),
                                  }).ToList();

               // Tính tiền chuyển khoản thừa lần 2
                //decimal _TienThua = 0;
                //var NgayBD = (from hd in db.dvHoaDons
                //                  join ldv in db.dvLoaiDichVus on hd.MaLDV equals ldv.ID
                //                  where hd.MaLDV != 23 & hd.IsDuyet == true & hd.MaKH == _MaKH & SqlMethods.DateDiffMonth(hd.NgayTT, date) > 0
                //                  & hd.NgayTT.Value.Year == _Nam
                //                  & (_MaLDVs.Contains(hd.MaLDV.Value) | _MaLDVs.Count == 0)
                //                  & hd.IsThuThua.GetValueOrDefault() == true
                //                  select hd.NgayTT
                //                  ).Min();
                //if (NgayBD != null)
                //{
                //    _TienThua = db.dvHoaDons.Where(p => p.IsDuyet == true & p.IsThuThua == true & p.MaKH == _MaKH & p.NgayTT.Value.Month == NgayBD.Value.Month & p.NgayTT.Value.Year == NgayBD.Value.Year).Sum(o => o.PhaiThu).GetValueOrDefault();
                //    NgayBD = NgayBD.Value.AddMonths(1);
                //    var _thang = NgayBD.Value.Month;
                //    var _nam = NgayBD.Value.Year;
                //    while (_thang != Thang || _nam != Nam)
                //    {
                //        // - Tiền phí quản lý tháng sau
                //        _TienThua -= db.dvHoaDons.Where(p => p.IsDuyet == true & p.IsThuThua.GetValueOrDefault() == false & p.MaKH == _MaKH & p.MaLDV == 13 & p.NgayTT.Value.Month == _thang & p.NgayTT.Value.Year == _nam).Sum(o => o.PhaiThu).GetValueOrDefault();

                //        // Nếu tiền thừa < 0 cho tiền thừa về 0
                //        _TienThua = _TienThua > 0 ? _TienThua : 0;

                //        // + Tiền thừa phát sinh tháng sau
                //        _TienThua += db.dvHoaDons.Where(p => p.IsDuyet == true & p.IsThuThua == true & p.MaKH == _MaKH & p.NgayTT.Value.Month == _thang & p.NgayTT.Value.Year == _nam).Sum(o => o.PhaiThu).GetValueOrDefault();

                //        if (_thang == 12)
                //        {
                //            _thang = 1;
                //            _nam++;
                //        }
                //        else
                //            _thang++;
                //    }
                //}
                
                var _NoCu = (from hd in db.dvHoaDons
                             where hd.MaKH == _MaKH
                             & hd.IsThuThua.GetValueOrDefault() == false
                             & (hd.PhaiThu.GetValueOrDefault() - (db.ptChiTietPhieuThus.Where(p => p.LinkID == hd.ID & p.TableName == "dvHoaDon").Sum(p => p.SoTien)).GetValueOrDefault()) != 0
                             & SqlMethods.DateDiffDay(hd.NgayTT, _Ngay) > 0
                                 & (_MaLDVs.Contains(hd.MaLDV.Value) | _MaLDVs.Count == 0) & hd.IsDuyet == true

                             select (hd.PhaiThu - (db.ptChiTietPhieuThus.Where(p => p.LinkID == hd.ID & p.TableName == "dvHoaDon").Sum(p => p.SoTien)).GetValueOrDefault())).Sum().GetValueOrDefault();
                if (_NoCu < 0)
                {
                    _NoCu = 0;
                }

                //var _ThuThua = ltThuThua1.Sum(p => p.SoTien) + ltThuThua2.Sum(p => p.SoTien) + ltThuThua3.Sum(p => p.SoTien);
                var _PhatSinh = ltLoaiDichVu.Sum(p => p.SoTien).GetValueOrDefault();
                var _TongTien = Convert.ToInt64(Math.Round(_PhatSinh, 0));
                //cSumSoTien.Text = string.Format("{0:#,0}", _PhatSinh);
                //csumNoCu.Text = string.Format("{0:#,0}", _NoCu);
               // cSumThuThua.Text = string.Format("{0:#,0}", _ThuThua);
                //csumTongTien.Text = string.Format("{0:#,0}", _TongTien > 0 ? _TongTien : 0);
                //cSoTienBangChu.Text = new TienTeCls().DocTienBangChu(_TongTien > 0 ? _TongTien : 0);
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
                        rptDichVu.ReportSource = new rptTienThue(this.MaTN, this.MaKH, this.Thang, this.Nam);
                        break;
                    case 3:
                        rptDichVu.ReportSource = new rptTienSuaChua(this.MaTN, this.MaKH, this.Thang, this.Nam);
                        break;
                    case 4:
                        rptDichVu.ReportSource = new rptTienDatCoc(this.MaTN, this.MaKH, this.Thang, this.Nam);
                        break;
                    case 5:
                        rptDichVu.ReportSource = new rptTienDien3Pha(this.MaTN, this.MaKH, this.Thang, this.Nam);
                        break;
                    case 6:
                        //rptDichVu.ReportSource = new rptXeHoaPhat(this.MaTN, this.MaKH, this.Thang, this.Nam);
                        break;
                    case 8:
                        rptDichVu.ReportSource = new rptTienDienThangLong(this.MaTN, this.MaKH, this.Thang, this.Nam);
                        break;
                    case 9:
                        //rptDichVu.ReportSource = new rptTienNuoc(this.MaTN, this.MaKH, this.Thang, this.Nam);
                        rptDichVu.ReportSource = new rptTienNuocHoaPhat(this.MaTN, this.MaKH, this.Thang, this.Nam);
                        break;
                    case 10:
                        rptDichVu.ReportSource = new rptTienGas(this.MaTN, this.MaKH, this.Thang, this.Nam);
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
                        default:
                        rptDichVu.ReportSource = new rptDichVuCoBanImperia(this.MaTN, this.MaKH, this.Thang, this.Nam, _MaLDV.Value);
                        //rptDichVu.ReportSource = new rptXeHoaPhat(this.MaTN, this.MaKH, this.Thang, this.Nam);
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
                //if (_MaLDV != 13 & _MaLDV != 6)
                //{
                //    STT++;
                //rtTenLDV.Html = string.Format("<div style='font-family:Times New Roman; font-size:9pt'><b>{0}. {1} <i>/ {2}</i> (Tháng {3:00}/{4})</b></div>",
                //    RomanNumerals.ToRoman(STT), GetCurrentColumnValue("TenLDV"),
                //    GetCurrentColumnValue("TenTA"),
                //    _ThangDV, _Nam);
                //}
            }
            catch { }
        }

        private void xrSubreport1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var rpt = xrSubreport1.ReportSource as rptXeHoaPhat;
            rpt.LoadData(MaTN,MaKH,Thang,Nam);
        }
    }
}
