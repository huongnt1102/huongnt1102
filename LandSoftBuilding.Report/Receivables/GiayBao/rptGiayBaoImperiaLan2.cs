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
    public partial class rptGiayBaoImperiaLan2 : DevExpress.XtraReports.UI.XtraReport
    {
        byte MaTN;
        int MaKH, _Thang, _Nam;

        public rptGiayBaoImperiaLan2(byte _MaTN, int _Thang, int _Nam, int _MaKH, List<int> _MaLDVs, int _MaTKNH)
        {
            InitializeComponent();
         
            var db = new MasterDataContext();
            try
            {
                Library.frmPrintControl.LoadLayout(this, 87, _MaTN);
              
                if (_MaKH == 0) return;
                cNoiDung.DataBindings.Add("Text", null, "TenLDV");
                cSoTien.DataBindings.Add("Text", null, "TongTien", "{0:#,0.##}");
                cKyTT.DataBindings.Add("Text", null, "KyTT");
                cHan.DataBindings.Add("Text", null, "HanTT");
                this.MaTN = _MaTN;
                this.MaKH = _MaKH;
                this._Thang = _Thang;
                this._Nam = _Nam;

                var _NgayIn = db.GetSystemDate();
                //cNgayIn.Text = string.Format(cNgayIn.Text, _NgayIn);

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
                                 kh.ThuTruoc,kh.MaPhu
                             }).First();

                //Thong tin tai khoan
                var objTK = (from tk in db.nhTaiKhoans
                             join nh in db.nhNganHangs on tk.MaNH equals nh.ID
                             where tk.ID == _MaTKNH
                             select new { tk.ChuTK, tk.SoTK, nh.TenNH }).Single();

                //Thay the du lieu
                var ctlRTF = new DevExpress.XtraRichEdit.RichEditControl();
                ctlRTF.RtfText = rtHeader.Rtf;
                ctlRTF.Document.ReplaceAll("[NgayIn]", DateTime.Now.Day.ToString(), SearchOptions.None);
                ctlRTF.Document.ReplaceAll("[ThangIn]", db.GetLocalDate().Value.Month.ToString(), SearchOptions.None);
                ctlRTF.Document.ReplaceAll("[ThangInTA]", Commoncls.GetMonth(db.GetLocalDate().Value.Month), SearchOptions.None);
                ctlRTF.Document.ReplaceAll("[NamIn]", db.GetLocalDate().Value.Year.ToString(), SearchOptions.None);
                ctlRTF.Document.ReplaceAll("[HanThanhToan]", "", SearchOptions.None);
                ctlRTF.Document.ReplaceAll("[Thang]", db.GetLocalDate().Value.Month.ToString(), SearchOptions.None);
                ctlRTF.Document.ReplaceAll("[MaPhu]", objKH.MaPhu.ToString(), SearchOptions.None);
                ctlRTF.Document.ReplaceAll("[Nam]", db.GetLocalDate().Value.Year.ToString(), SearchOptions.None);
                ctlRTF.Document.ReplaceAll("[TenKH]", objKH.TenKH ?? "", SearchOptions.None);
                ctlRTF.Document.ReplaceAll("[MaSoMB]", objKH.MaSoMB ?? "", SearchOptions.None);
                ctlRTF.Document.ReplaceAll("[TenTN]", objTN.TenTN ?? "", SearchOptions.None);
                ctlRTF.Document.ReplaceAll("[TenCongTy]", objTN.CongTyQuanLy ?? "", SearchOptions.None);
                ctlRTF.Document.ReplaceAll("[TenTaiKhoan]", objTK.ChuTK ?? "", SearchOptions.None);
                ctlRTF.Document.ReplaceAll("[SoTaiKhoan]", objTK.SoTK ?? "", SearchOptions.None);
                ctlRTF.Document.ReplaceAll("[TenNganHang]", objTK.TenNH ?? "", SearchOptions.None);
                ctlRTF.Document.ReplaceAll("[ThangTA]", Commoncls.GetMonth(db.GetLocalDate().Value.Month) ?? "", SearchOptions.None);
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
                rtFooter.Rtf = ctlRTF.RtfText;
                var _Ngay = new DateTime(_Nam, _Thang, 1);
                //Du lieu hoa don
                //var ltLoaiDichVu = (from hd in db.dvHoaDons
                //                    join ldv in db.dvLoaiDichVus on hd.MaLDV equals ldv.ID
                //                    where hd.MaLDV != 23 & hd.IsDuyet == true & hd.MaKH == _MaKH & hd.NgayTT.Value.Month == _Thang
                //                    & hd.NgayTT.Value.Year == _Nam
                //                    & (_MaLDVs.Contains(hd.MaLDV.Value) | _MaLDVs.Count == 0)
                //                    & hd.PhaiThu > 0
                //                    & (hd.PhaiThu.GetValueOrDefault() 
                //                    - (db.ptChiTietPhieuThus.Where(p => p.LinkID == hd.ID & p.TableName == "dvHoaDon").Sum(p => p.SoTien)).GetValueOrDefault()
                                    
                //                    - (db.ktttChiTiets.Where(p => p.LinkID == hd.ID & p.TableName == "dvHoaDon").Sum(p => p.SoTien)).GetValueOrDefault() 
                //                    ) 
                                      
                              
                //                    > 0  //moi mo 5/11/2016
                //                    group hd by new { ldv.STT, hd.MaLDV, ldv.TenHienThi, ldv.TenTA } into gr
                //                    orderby gr.Key.STT
                //                    select new
                //                    {
                //                        gr.Key.STT,
                //                        gr.Key.MaLDV,
                //                        TenLDV = gr.Key.TenHienThi,
                //                        gr.Key.TenTA,
                //                        SoTien = gr.Key.MaLDV == 49 ? -gr.Sum(p => p.PhaiThu) : gr.Sum(p => p.PhaiThu),
                //                    }).ToList();

                //var ltData = (from hd in db.dvHoaDons
                //              join ldv in db.dvLoaiDichVus on hd.MaLDV equals ldv.ID
                //              where hd.MaKH == _MaKH & hd.NgayTT.Value.Month == _Thang & hd.NgayTT.Value.Year == _Nam
                //              & hd.ConNo.GetValueOrDefault() > 0
                //              & (_MaLDVs.Contains(hd.MaLDV.Value) | _MaLDVs.Count == 0)
                //              & (hd.PhaiThu.GetValueOrDefault() 
                //              - (db.ptChiTietPhieuThus.Where(p => p.LinkID == hd.ID & p.TableName == "dvHoaDon").Sum(p => p.SoTien)).GetValueOrDefault()

                //              - (db.ktttChiTiets.Where(p => p.LinkID == hd.ID & p.TableName == "dvHoaDon").Sum(p => p.SoTien)).GetValueOrDefault() 
                //              ) 
                                
                              
                //              > 0
                //              & hd.IsDuyet == true
                //              group hd by new { hd.MaLDV, ldv.TenHienThi, ldv.TenTA } into gr
                //              select new
                //              {
                //                  gr.Key.MaLDV,
                //                  TenLDV = gr.Key.TenHienThi,
                //                  gr.Key.TenTA,
                //                  SoTien = gr.Sum(p => p.ConNo)
                //              }).ToList();
             
                var tam = (from hd in db.dvHoaDons
                           join ldv in db.dvLoaiDichVus on hd.MaLDV equals ldv.ID
                                 
                                   where hd.MaTN == MaTN & hd.IsDuyet == true
                                   & SqlMethods.DateDiffMonth(hd.NgayTT, _Ngay) >= 0
                                   & hd.MaKH == MaKH

                           group hd by new { hd.MaLDV, TenLDV =ldv.TenLDV + "( " + ldv.TenTA + " )", hd.KyTT, ldv.NgayHanTT } into gr
                               
                                   select new
                                   {
                                       gr.Key.TenLDV,
                                   
                                       
                                       TongTien = gr.Sum(p =>p.PhaiThu -
                                               (from ct in db.ptChiTietPhieuThus
                                                join pt in db.ptPhieuThus on ct.MaPT equals pt.ID
                                                where ct.TableName == "dvHoaDon" & ct.LinkID == p.ID & SqlMethods.DateDiffMonth(pt.NgayThu, _Ngay) > 0
                                                select ct.SoTien).Sum().GetValueOrDefault()



                                                - ((from ct in db.ktttChiTiets
                                                    join pt in db.ktttKhauTruThuTruocs on ct.MaCT equals pt.ID
                                                    where ct.TableName == "dvHoaDon" & ct.LinkID == p.ID & SqlMethods.DateDiffMonth(pt.NgayCT, _Ngay) > 0
                                                    select ct.SoTien).Sum().GetValueOrDefault()))
                                                                        ,



                                       KyTT = KyTT((int)gr.Key.KyTT),
                                       HanTT =  string.Format("{2}/{0}/{1}", _Thang, _Nam, gr.Key.NgayHanTT)
                                   }).ToList();
                this.DataSource = tam;
                csumTongTien.Text = string.Format("{0:#,0.##}", tam.Sum(p => p.TongTien).GetValueOrDefault());



            }
            catch { }
            finally
            {
                db.Dispose();
            }
        }

        string KyTT(int KyTT)
        {
            string giatri = "";
             string ThangMoi = "";
             if (KyTT >= 3)
                {
                    if (_Thang >= 1 & _Thang <= 3)
                    {
                        ThangMoi = "1";
                    }
                    if (_Thang >= 4 & _Thang <= 6)
                    {
                        ThangMoi = "2";
                    }
                    if (_Thang >= 7 & _Thang <= 9)
                    {
                        ThangMoi = "3";
                    }
                    if (_Thang >= 10 & _Thang <= 12)
                    {
                        ThangMoi = "4";
                    }
                    giatri = string.Format("Q{0}/{1}", ThangMoi, _Nam);
                }
                else
                {
                    giatri = string.Format("T{0}/{1}", _Thang, _Nam);
                }
            return giatri;
        }
 

        int STT = 0;
 
    }
}
