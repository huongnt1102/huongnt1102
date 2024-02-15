﻿using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
using Library;
using System.Data.Linq.SqlClient;

namespace LandSoftBuilding.Receivables.GiayBao
{
    public partial class rptTheXeTTC_XemLai : DevExpress.XtraReports.UI.XtraReport
    {
        public rptTheXeTTC_XemLai(byte _MaTN, int _MaKH, int _Thang, int _Nam)
        {
            InitializeComponent();

            Library.frmPrintControl.LoadLayout(this, 13, _MaTN);

            cSTT.DataBindings.Add("Text", null, "STT");
            cSTT.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.RecordNumber, "{0:#}");

            cTenLX.DataBindings.Add("Text", null, "TenLX");
            cBienSo.DataBindings.Add("Text", null, "BienSo");
            cDonGia.DataBindings.Add("Text", null, "DonGia", "{0:#,0.##}");
            cSoLuong.DataBindings.Add("Text", null, "SoLuong", "{0:#,0.##}");
            cNoCu.DataBindings.Add("Text", null, "NoCu", "{0:#,0.##}");
            cThanhTien.DataBindings.Add("Text", null, "ThanhTien", "{0:#,0.##}");
            cTienXe.DataBindings.Add("Text", null, "ThanhTien", "{0:#,0.##}");
            cTienXe.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,0.##}");
            var db = new MasterDataContext();
            try
            {
                     var NgayTB = new DateTime((int)_Nam, (int)_Thang, 1);
                    var ltGiuXete = (from tx in db.dvgxTheXes
                                     join lx in db.dvgxLoaiXes on tx.MaLX equals lx.MaLX into dslx
                                     from lx in dslx.DefaultIfEmpty()
                                  
                                     join dv in db.dvHoaDons on tx.ID equals dv.LinkID

                                    
                                     where tx.MaTN == _MaTN & tx.MaKH == _MaKH
                                          & (dv.PhaiThu.GetValueOrDefault()-
                    (db.SoQuy_ThuChis.Where(p => p.LinkID == dv.ID & p.TableName == "dvHoaDon" & SqlMethods.DateDiffDay(p.NgayPhieu, new DateTime(_Nam, _Thang, 1)) > 0).Sum(p => p.DaThu + p.KhauTru)).GetValueOrDefault()
                    ) > 0
                                     & dv.NgayTT.Value.Month == _Thang & dv.NgayTT.Value.Year == _Nam &dv.IsDuyet==true & dv.MaLDV==6
                                     group tx by new { tx.MaLX, lx.TenLX, tx.GiaThang } into gr
                                     select new GiuXeItem()
                                     {
                                         MaLX = gr.Key.MaLX,
                                         TenLX = gr.Key.TenLX,
                                         BienSo = "",
                                         SoLuong = gr.Count(),
                                         DonGia = gr.Key.GiaThang,
                                         ThanhTien = gr.Count() * gr.Key.GiaThang,
                                         NoCu = (from hd in db.dvHoaDons
                                                 join tx in db.dvgxTheXes on hd.LinkID equals tx.ID
                                                 where hd.MaKH==_MaKH && tx.MaLX==gr.Key.MaLX && hd.MaLDV==6 &&
                                                       SqlMethods.DateDiffDay(hd.NgayTT, NgayTB) > 0
                                                 select (hd.PhaiThu - (db.SoQuy_ThuChis.Where(p => p.LinkID == hd.ID & p.TableName == "dvHoaDon" & SqlMethods.DateDiffDay(p.NgayPhieu, NgayTB) > 0).Sum(p => p.DaThu + p.KhauTru).GetValueOrDefault()))).Sum().GetValueOrDefault()
                                         //(from hd in db.dvHoaDons
                                         //           join tx in db.dvgxTheXes on hd.LinkID equals tx.ID
                                         //         where hd.MaKH == _MaKH & tx.MaLX == gr.Key.MaLX
                                         //             & SqlMethods.DateDiffDay(hd.NgayTT, NgayTB) > 0
                                         //            & (hd.PhaiThu.GetValueOrDefault()

                                         //           - (from ct in db.ptChiTietPhieuThus
                                         //              join pt in db.ptPhieuThus on ct.MaPT equals pt.ID
                                         //              where ct.TableName == "dvHoaDon" & ct.LinkID == hd.ID
                                         //              & SqlMethods.DateDiffDay(pt.NgayThu, NgayTB) > 0
                                         //              select ct.SoTien).Sum().GetValueOrDefault()) != 0
                                         //                 & hd.MaLDV == 6
                                         //            select (hd.PhaiThu
                                         //            - (from ct in db.ptChiTietPhieuThus
                                         //               join pt in db.ptPhieuThus on ct.MaPT equals pt.ID
                                         //               where ct.TableName == "dvHoaDon" & ct.LinkID == hd.ID
                                         //                   //& SqlMethods.DateDiffDay(pt.NgayThu, _TuNgay) > 0 -- Sua lai dau ky toi ngay hien tai
                                         //               & SqlMethods.DateDiffDay(pt.NgayThu, NgayTB) > 0
                                         //               select ct.SoTien).Sum().GetValueOrDefault()

                            
                                         //            )

                                         //             ).Sum().GetValueOrDefault()
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
                    

                var _TienTT = (from hd in db.dvHoaDons
                               join ltt in db.dvgxLichThanhToans on new { hd.TableName, hd.LinkID } equals new { TableName = "dvgxLichThanhToan", LinkID = (int?)ltt.ID }
                               where hd.MaTN == _MaTN & hd.MaLDV == 6 & hd.MaKH == _MaKH & hd.NgayTT.Value.Month == _Thang & hd.NgayTT.Value.Year == _Nam & (hd.PhaiThu.GetValueOrDefault()
                             - (db.SoQuy_ThuChis.Where(p => p.LinkID == hd.ID & p.TableName == "dvHoaDon" & SqlMethods.DateDiffDay(p.NgayPhieu, new DateTime(_Nam, _Thang, 1)) > 0).Sum(p => p.DaThu + p.KhauTru)).GetValueOrDefault()
                             ) > (decimal)0
                               select new
                               {
                                   PhaiThu = hd.PhaiThu
                                   - db.SoQuy_ThuChis.Where(p => p.LinkID == hd.ID & SqlMethods.DateDiffDay(p.NgayPhieu, new DateTime(_Nam, _Thang, 1)) > 0).Sum(p => p.DaThu + p.KhauTru).GetValueOrDefault()
                               }).Sum(p=>p.PhaiThu).GetValueOrDefault();
                 cTienXe.Text = string.Format("{0:#,0.##}", _TienTT);
            }
            catch { }
            finally
            {
                db.Dispose();
            }
        }
       
    }

    
}
