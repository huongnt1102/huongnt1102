﻿using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
using Library;
using System.Data.Linq.SqlClient;
using DevExpress.XtraRichEdit.API.Native;

namespace LandSoftBuilding.Receivables.GiayBao
{
    public partial class rptTienNuocImperia : DevExpress.XtraReports.UI.XtraReport
    {
        public rptTienNuocImperia(byte _MaTN, int _MaKH, int _Thang, int _Nam, string lama)
        {
            InitializeComponent();

       

            #region Bingding
            //cSTT.DataBindings.Add("Text", null, "STT");
            //cSTT.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.RecordNumber, "{0:#}");
            //cTuNgay.DataBindings.Add("Text", null, "TenDM");
            //cDenNgay.DataBindings.Add("Text", null, "SoLuong", "{0:#,0.##}");
            //cCSC.DataBindings.Add("Text", null, "DonGia", "{0:#,0.##}");
            //cThanhTien.DataBindings.Add("Text", null, "ThanhTien", "{0:#,0.##}");
            #endregion

            xrTableCell7.Text = string.Format(xrTableCell7.Text, lama);
            var db = new MasterDataContext();
            try
            {
                var objNuoc = (from hd in db.dvHoaDons
                               join tn in db.dvNuocs on   hd.LinkID  equals tn.ID 
                               where hd.MaTN == _MaTN & hd.MaKH == _MaKH & hd.NgayTT.Value.Month == _Thang & hd.NgayTT.Value.Year == _Nam & hd.MaLDV==9
                                 & (hd.PhaiThu.GetValueOrDefault()
                    - (db.ptChiTietPhieuThus.Where(p => p.LinkID == hd.ID & p.TableName == "dvHoaDon").Sum(p => p.SoTien)).GetValueOrDefault()
                    - (db.ktttChiTiets.Where(p => p.LinkID == hd.ID & p.TableName == "dvHoaDon").Sum(p => p.SoTien)).GetValueOrDefault()
                    ) > 0
                               select new
                               {
                                   tn.ID,
                                   tn.ChiSoCu,tn.TienVAT,tn.TienBVMT,
                                   tn.ChiSoMoi,tn.TuNgay,tn.DenNgay,tn.ThanhTien,
                                   SoTieuThu=tn.SoTieuThu.GetValueOrDefault()+tn.SoTieuThuDHCu.GetValueOrDefault(),
                                   SoUuDai = (from ud in db.dvNuocUuDais where ud.MaMB == tn.MaMB select ud.SoNguoi).FirstOrDefault(),
                                   hd.PhaiThu,DG=tn.dvNuocChiTiets.FirstOrDefault().DonGia
                               }).FirstOrDefault();
                if (objNuoc == null) return;


                cDM2.Text = string.Format("{0:#,0.##}", objNuoc.SoTieuThu);

                cTT2.Text = string.Format("{0:#,0.##}", objNuoc.ThanhTien);
                   
        
                var NgayTB = new DateTime((int)_Nam, (int)_Thang, 1);
                var _NoCu = (from hd in db.dvHoaDons
                             //join ptct in db.ptChiTietPhieuThus on hd.ID equals ptct.LinkID
                             //join pt in db.ptPhieuThus on ptct.MaPT equals pt.ID
                             where hd.MaKH == _MaKH
                                   & SqlMethods.DateDiffDay(hd.NgayTT, NgayTB) > 0
                                 //  & SqlMethods.DateDiffDay(pt.NgayThu, NgayTB) > 0
                                   & hd.IsThuThua.GetValueOrDefault() == false
                                   & (hd.PhaiThu.GetValueOrDefault()

                                      - (from ct in db.ptChiTietPhieuThus
                                         join pt in db.ptPhieuThus on ct.MaPT equals pt.ID
                                         where ct.TableName == "dvHoaDon" & ct.LinkID == hd.ID
                                             //& SqlMethods.DateDiffDay(pt.NgayThu, _TuNgay) > 0 -- Sua lai dau ky toi ngay hien tai
                                               & SqlMethods.DateDiffDay(pt.NgayThu, NgayTB) > 0
                                         select ct.SoTien).Sum().GetValueOrDefault()) != 0
                                 // & (_MaLDVs.Contains(hd.MaLDV.Value) | _MaLDVs.Count == 0) & hd.IsDuyet == true
                                   & hd.MaLDV == 9
                             select (hd.PhaiThu
                                     - (from ct in db.ptChiTietPhieuThus
                                        join pt in db.ptPhieuThus on ct.MaPT equals pt.ID
                                        where ct.TableName == "dvHoaDon" & ct.LinkID == hd.ID
                                            //& SqlMethods.DateDiffDay(pt.NgayThu, _TuNgay) > 0 -- Sua lai dau ky toi ngay hien tai
                                              & SqlMethods.DateDiffDay(pt.NgayThu, NgayTB) > 0
                                        select ct.SoTien).Sum().GetValueOrDefault()

                                 //- (db.ptChiTietPhieuThus.Where(p => p.LinkID == hd.ID & p.TableName == "dvHoaDon").Sum(p => p.SoTien)).GetValueOrDefault()
                             )

                ).Sum().GetValueOrDefault();
                cVAT.Text = string.Format("{0:#,0.##}", objNuoc.TienVAT);
                cBVMT.Text = string.Format("{0:#,0.##}", objNuoc.TienBVMT);
                cCSC.Text = string.Format("{0:#,0.##}", objNuoc.ChiSoCu);
                cCSM.Text = string.Format("{0:#,0.##}", objNuoc.ChiSoMoi);
                //cSTT.Text = string.Format("{0:#,0.##}", objNuoc.SoTieuThu);
                cPS.Text = string.Format("{0:#,0.##}", objNuoc.PhaiThu);
                cNoCu.Text = string.Format("{0:#,0.##}", _NoCu);
                cTuNgay.Text = string.Format("{0:dd/MM/yyyy}", objNuoc.TuNgay);
                cDenNgay.Text = string.Format("{0:dd/MM/yyyy}", objNuoc.DenNgay);
                cTienNuoc.Text = string.Format("{0:#,0.##}", objNuoc.PhaiThu.GetValueOrDefault() + _NoCu);
                var ctlRTF = new DevExpress.XtraRichEdit.RichEditControl();
                ctlRTF.RtfText = rtHeader.Rtf;
                ctlRTF.Document.ReplaceAll("[DG]", string.Format("{0:#,0.##}", objNuoc.DG) ?? "", SearchOptions.None);
                rtHeader.Rtf = ctlRTF.RtfText;
                //xrTableCell22 // Nợ những tháng nào
            }
            catch { }
            finally
            {
                db.Dispose();
            }
        }
    }
}
