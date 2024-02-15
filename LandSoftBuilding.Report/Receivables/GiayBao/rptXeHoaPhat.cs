using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
using Library;
using System.Data.Linq.SqlClient;
using LandSoftBuilding.Report;

namespace LandSoftBuilding.Receivables.GiayBao
{
    public partial class rptXeHoaPhat : DevExpress.XtraReports.UI.XtraReport
    {
        public byte _MaTN1
            {
                get;
                set;
            } 
        public int _MaKH1{
                get;
                set;
            }
        public int _Nam1 { get; set; }
        public int _Thang1 { get; set; }
        public rptXeHoaPhat(byte _MaTN, int _MaKH, int _Thang, int _Nam)
        {
            InitializeComponent();
            _MaTN1 = _MaTN;
            _MaKH1 = _MaKH;
            _Nam1 = _Nam;
            _Thang1 = _Thang;
            //Library.frmPrintControl.LoadLayout(this, 13, _MaTN);

            //cSTT.DataBindings.Add("Text", null, "STT");
            //cSTT.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.RecordNumber, "{0:#}");

            //cTenLX.DataBindings.Add("Text", null, "TenLX");
           // cNoiDung.DataBindings.Add("Text", null, "BienSo");
           //// cSoLuong.DataBindings.Add("Text", null, "SoLuong", "{0:#,0.##}");
           // cDonGia.DataBindings.Add("Text", null, "DonGia", "{0:#,0.##}");
           // cThanhTien.DataBindings.Add("Text", null, "ThanhTien", "{0:#,0.##}");
           // cTienXe.DataBindings.Add("Text", null, "ThanhTien","{0:#,0.##}");
           // cTienXe.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,0.##}");
            var rpt = new rptPQLHoaPhat();
            xrSubreport1.ReportSource = rpt;
            var rpt1 = new rptNuocHoaPhat();
            xrSubreport2.ReportSource = rpt1;
            var db = new MasterDataContext();
            try
            {
                if (_MaTN != 27)
                {
                    var ltGiuXeCongNo = (from hd in db.dvHoaDons
                                         where hd.IsDuyet == true
                                               &
                                               (hd.PhaiThu.GetValueOrDefault() -
                                                (db.ptChiTietPhieuThus.Where(p => p.LinkID == hd.ID & p.TableName == "dvHoaDon")
                                                    .Sum(p => p.SoTien)).GetValueOrDefault())
                                               > 0 & hd.LinkID == null & hd.MaLDV == 6
                                               & hd.MaKH == _MaKH
                                         // & hd.NgayTT.Value.Year == _Nam
                                         select new GiuXeItem3()
                                         {
                                             BienSo = hd.DienGiai,
                                             Thang = hd.NgayTT.Value.Month,
                                             Nam = hd.NgayTT.Value.Year,
                                             ThanhTien = (hd.PhaiThu.GetValueOrDefault() -
                                                 (db.ptChiTietPhieuThus.Where(p => p.LinkID == hd.ID & p.TableName == "dvHoaDon")
                                                 .Sum(p => p.SoTien)).GetValueOrDefault())
                                         }).ToList();
                    var ltGiuXeCongNo2 = (from hd in db.dvHoaDons
                                         where hd.IsDuyet == true
                                               &
                                               (hd.PhaiThu.GetValueOrDefault() -
                                                (db.ptChiTietPhieuThus.Where(p => p.LinkID == hd.ID & p.TableName == "dvHoaDon")
                                                    .Sum(p => p.SoTien)).GetValueOrDefault())
                                               > 0 //& hd.LinkID == null 
                                               & hd.MaLDV == 6
                                                 & hd.NgayTT.Value.Year <= _Nam 
                                               & hd.MaKH == _MaKH
                                         // & hd.NgayTT.Value.Year == _Nam
                                          select new //GiuXeItem3()
                                          {
                                              BienSo = hd.DienGiai,
                                              Thang = hd.NgayTT.Value.Month,
                                              Nam = hd.NgayTT.Value.Year,
                                              ThanhTien = (hd.PhaiThu.GetValueOrDefault() -
                                                  (db.ptChiTietPhieuThus.Where(p => p.LinkID == hd.ID & p.TableName == "dvHoaDon")
                                                  .Sum(p => p.SoTien)).GetValueOrDefault())
                                          }).ToList();
                    var ltGiuXete =(from tx in db.dvgxTheXes
                                   join lx in db.dvgxLoaiXes on tx.MaLX equals lx.MaLX into dslx
                                   from lx in dslx.DefaultIfEmpty()                               
                                   join dv in db.dvHoaDons on tx.ID equals dv.LinkID
                                    join nhomxe in db.dvgxNhomXes on lx.MaNX equals nhomxe.ID
                                   where 
                                   tx.MaTN == _MaTN & tx.MaKH == _MaKH  //& dv.NgayTT.Value.Month == _Thang 
                                   & dv.NgayTT.Value.Year <= _Nam 
                                    & dv.MaLDV==6
                                   & dv.IsDuyet == true
                                   & 
                                   (dv.PhaiThu.GetValueOrDefault() - 
                                   (db.ptChiTietPhieuThus.Where(p => p.LinkID == dv.ID & p.TableName == "dvHoaDon").Sum(p => p.SoTien)).GetValueOrDefault()) 
                                   > 0
                                    group new { tx, dv } by new { tx.MaLX, nhomxe.TenNX,lx.TenLX, tx.GiaThang, Thang = dv.NgayTT.Value.Month, Nam = dv.NgayTT.Value.Year } into gr
                                   select new GiuXeItem2()
                                   {
                                       MaLX = gr.Key.MaLX,
                                       TenLX = gr.Key.TenNX,
                                     TenNX=gr.Key.TenNX,
                                      BienSo="",
                                       SoLuong = gr.Count(),
                                       ThanhTien = 
                                       gr.Sum(p => p.dv.ConNo) .GetValueOrDefault(),
                                       Thang = gr.Key.Thang,
                                       Nam = gr.Key.Nam,
                                   }).ToList();

                    string Quy = "";
                    string QuyNew = ",";
                    string ThangCu = "";
                    string ThangMoi = ",";
                    string XeCu = "";
                    string XeMoi = ",";
                    foreach (var i in ltGiuXeCongNo)
                    {
                        if (cNoiDung.Text == "Phí giữ xe :")
                        {
                            cNoiDung.Text += " " + i.BienSo + " ";
                        }
                        else
                        {
                            cNoiDung.Text += i.BienSo + ", ";
                        }
                        //cNoiDung.Text += ", ";
                        //cNoiDung.Text += i.BienSo += ", ";
                    }
                    cNoiDung.Text = cNoiDung.Text.Length > 0 ? "Phí giữ xe: " + cNoiDung.Text : "Phí giữ xe: ";
                    foreach (var i in ltGiuXete)
                    {
                        #region Xu ly
                        var ltBienSo = (from tx in db.dvgxTheXes
                                        join lx in db.dvgxLoaiXes on tx.MaLX equals lx.MaLX
                                        join nhomxe in db.dvgxNhomXes on lx.MaNX equals nhomxe.ID
                                        where tx.MaTN == _MaTN & tx.MaKH == _MaKH & tx.MaLX == i.MaLX
                                        select new { TenLX = nhomxe.TenNX, LX = lx.TenLX, SoLuong = i.SoLuong }).ToList().Take(1);

                        if (i.Thang >= 1 & i.Thang <= 3)
                        {
                            Quy = "Quý I" + "/" + i.Nam; ThangCu = i.Thang.ToString();
                            string tam = "";
                            XeMoi = i.TenNX;
                            foreach (var bs in ltBienSo)
                                if (!string.IsNullOrEmpty(bs.TenLX))
                                {
                                    if (Quy == QuyNew)
                                    {
                                        if (i.TenNX == "o to")
                                        {
                                            
                                                tam += bs.SoLuong + " " + bs.LX;
                                            
                                        }
                                        else
                                        {
                                            if (XeCu == XeMoi)
                                            {
                                                
                                            }
                                            else
                                            {
                                                tam += ltGiuXete.Where(p => p.Thang >= 1 & p.Thang <= 3 & p.TenLX == i.TenLX).Sum(p => p.SoLuong) + " " + bs.TenLX;
                                            }
                                            
                                        }

                                    }
                                    else
                                    {
                                        if (i.TenNX == "o to")
                                        {
                                            tam += bs.SoLuong + " " + bs.LX;
                                        }
                                        else
                                        {
                                            tam += ltGiuXete.Where(p => p.Thang >= 1 & p.Thang <= 3 & p.TenLX == i.TenLX).Sum(p => p.SoLuong) + " " + bs.TenLX;
                                        }
                                    }


                                }
                            if (Quy == QuyNew)
                            {
                                if (i.TenNX == "o to")
                                {
                                    //if (ThangCu != ThangMoi)
                                    i.BienSo += string.Format("T{0}/{1}", i.Thang, i.Nam) + " " + tam + ", ";
                                }
                                else
                                {
                                    if (XeCu == XeMoi)
                                    {
                                        i.BienSo +=tam == "" ? "": tam + ", ";

                                    }
                                    else
                                    {
                                        i.BienSo += tam == "" ? "" : Quy + " " + tam + ", ";
                                    }
                                    
                                }

                            }
                            else
                            {
                                if (i.TenNX == "o to")
                                {
                                    i.BienSo += " " + string.Format("T{0}/{1}", i.Thang, i.Nam) + " " + tam + ", ";
                                }
                                else
                                {
                                   
                                        i.BienSo += Quy + " " + tam + ", ";
                                        
                                  
                                    
                                }

                            }
                            QuyNew = Quy;
                            ThangMoi = ThangCu;
                           
                            XeCu = XeMoi;
                        }
                        if (i.Thang >= 4 & i.Thang <= 6)
                        {
                            Quy = "Quý II" + "/" + i.Nam;
                            ThangCu = i.Thang.ToString(); XeMoi = i.TenNX;
                            string tam = "";
                            foreach (var bs in ltBienSo)
                                if (!string.IsNullOrEmpty(bs.TenLX))
                                {
                                    if (Quy == QuyNew)
                                    {
                                        if (i.TenNX == "o to")
                                        {
                                            tam += bs.SoLuong + " " + bs.LX;
                                        }
                                        else
                                        {
                                            if (XeCu == XeMoi)
                                            {

                                            }
                                            else
                                            {
                                                tam += ltGiuXete.Where(p => p.Thang >= 4 & p.Thang <= 6 & p.TenLX == i.TenLX).Sum(p => p.SoLuong) + " " + bs.TenLX;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (i.TenNX == "o to")
                                        {
                                            tam += bs.SoLuong + " " + bs.LX;
                                        }
                                        else
                                        {
                                            tam += ltGiuXete.Where(p => p.Thang >= 4 & p.Thang <= 6 & p.TenLX == i.TenLX).Sum(p => p.SoLuong) + " " + bs.TenLX;
                                        }
                                    }


                                }
                            if (Quy == QuyNew)
                            {
                                if (i.TenNX == "o to")
                                {
                                    i.BienSo += string.Format("T{0}/{1}", i.Thang, i.Nam) + " " + tam + ", ";
                                }
                                else
                                {
                                    if (XeCu == XeMoi)
                                    {
                                        i.BienSo += tam == "" ? "" : tam + ", ";

                                    }
                                    else
                                    {
                                        i.BienSo += tam == "" ? "" : Quy + " " + tam + ", ";
                                    }
                                    
                                }

                            }
                            else
                            {
                                if (i.TenNX == "o to")
                                {
                                    i.BienSo += " " + string.Format("T{0}/{1}", i.Thang, i.Nam) + " " + tam + ", ";
                                }
                                else
                                {
                                    i.BienSo += Quy + " " + tam + ", ";
                                }

                            }
                            QuyNew = Quy;
                            ThangMoi = ThangCu; XeCu = XeMoi;
                        }
                        
                        if (i.Thang >= 7 & i.Thang <= 9)
                        {
                            Quy = "Quý III" + "/" + i.Nam; ;
                            ThangCu = i.Thang.ToString();
                            string tam = ""; XeMoi = i.TenNX;
                            foreach (var bs in ltBienSo)
                                if (!string.IsNullOrEmpty(bs.TenLX))
                                {
                                    if (Quy == QuyNew)
                                    {
                                        if (i.TenNX == "o to")
                                        {
                                            tam += bs.SoLuong + " " + bs.LX;
                                        }
                                        else
                                        {
                                            if (XeCu == XeMoi)
                                            {

                                            }
                                            else
                                            {
                                                tam += ltGiuXete.Where(p => p.Thang >= 7 & p.Thang <= 9 & p.TenLX == i.TenLX).Sum(p => p.SoLuong) + " " + bs.TenLX;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (i.TenNX == "o to")
                                        {
                                            tam += bs.SoLuong + " " + bs.LX;
                                        }
                                        else
                                        {
                                            tam += ltGiuXete.Where(p => p.Thang >= 7 & p.Thang <= 9 & p.TenLX == i.TenLX).Sum(p => p.SoLuong) + " " + bs.TenLX;
                                        }
                                    }


                                }
                            if (Quy == QuyNew)
                            {
                                if (i.TenNX == "o to")
                                {
                                    i.BienSo += string.Format("T{0}/{1}", i.Thang, i.Nam) + " " + tam + ", ";
                                }
                                else
                                {
                                    if (XeCu == XeMoi)
                                    {
                                        i.BienSo += tam == "" ? "" : tam + ", ";

                                    }
                                    else
                                    {
                                        i.BienSo += tam == "" ? "" : Quy + " " + tam + ", ";
                                    }
                                }

                            }
                            else
                            {
                                if (i.TenNX == "o to")
                                {
                                    i.BienSo += " " + string.Format("T{0}/{1}", i.Thang, i.Nam) + " " + tam + ", ";
                                }
                                else
                                {
                                    i.BienSo += Quy + " " + tam + ", ";
                                }

                            }
                            QuyNew = Quy;
                            ThangMoi = ThangCu; XeCu = XeMoi;
                        }
                        if (i.Thang >= 10 & i.Thang <= 12)
                        {
                            Quy = "Quý IV" + "/" + i.Nam; ;
                            ThangCu = i.Thang.ToString();
                            string tam = ""; XeMoi = i.TenNX;
                            foreach (var bs in ltBienSo)
                                if (!string.IsNullOrEmpty(bs.TenLX))
                                {
                                    if (Quy == QuyNew)
                                    {
                                        if (i.TenNX == "o to")
                                        {
                                            tam += bs.SoLuong + " " + bs.LX;
                                        }
                                        else
                                        {
                                            if (XeCu == XeMoi)
                                            {

                                            }
                                            else
                                            {
                                                tam += ltGiuXete.Where(p => p.Thang >= 10 & p.Thang <= 12 & p.TenLX == i.TenLX).Sum(p => p.SoLuong) + " " + bs.TenLX;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (i.TenNX == "o to")
                                        {
                                            tam += bs.SoLuong + " " + bs.LX;
                                        }
                                        else
                                        {
                                            tam += ltGiuXete.Where(p => p.Thang >= 10 & p.Thang <= 12 & p.TenLX == i.TenLX).Sum(p => p.SoLuong) + " " + bs.TenLX;
                                        }
                                    }


                                }
                            if (Quy == QuyNew)
                            {
                                if (i.TenNX == "o to")
                                {
                                    i.BienSo += string.Format("T{0}/{1}", i.Thang, i.Nam) + " " + tam + ", ";
                                }
                                else
                                {
                                    if (XeCu == XeMoi)
                                    {
                                        i.BienSo += tam == "" ? "" : tam + ", ";

                                    }
                                    else
                                    {
                                        i.BienSo += tam == "" ? "" : Quy + " " + tam + ", ";
                                    }
                                }

                            }
                            else
                            {
                                if (i.TenNX == "o to")
                                {
                                    i.BienSo += " " + string.Format("T{0}/{1}", i.Thang, i.Nam) + " " + tam + ", ";
                                }
                                else
                                {
                                    i.BienSo += Quy + " " + tam + ", ";
                                }

                            }
                            QuyNew = Quy;
                            ThangMoi = ThangCu;
                            XeCu = XeMoi;
                        }
                        //i.BienSo = i.BienSo.Trim(' ').Trim(';');
                        cNoiDung.Text += string.Format("{0}", i.BienSo);
                        #endregion
                    }
                    cNoiDung.Text = cNoiDung.Text.Length > 0 ? cNoiDung.Text.Remove(cNoiDung.Text.Length - 2) : "";
                   
                    var _TienTT = (from hd in db.dvHoaDons
                                   //join ltt in db.dvgxLichThanhToans on new { hd.TableName, hd.LinkID } equals new { TableName = "dvgxLichThanhToan", LinkID = (int?)ltt.ID }
                                   where hd.MaTN == _MaTN & (hd.MaLDV == 6 | hd.MaLDV == 13 ) & hd.MaKH == _MaKH 
                                   & hd.NgayTT.Value.Year <= _Nam
                                  // & hd.NgayTT.Value.Month <= _Thang
                                   //& hd.ConNo.GetValueOrDefault() > 0
                                         & hd.IsDuyet==true
                                          &
                                     (hd.PhaiThu.GetValueOrDefault() -
                                     (db.ptChiTietPhieuThus.Where(p => p.LinkID == hd.ID & p.TableName == "dvHoaDon").Sum(p => p.SoTien)).GetValueOrDefault())
                                     > 0
                                   select new
                                           {
                                               ConNo = (decimal)(hd.PhaiThu.GetValueOrDefault() -
                                                   (db.ptChiTietPhieuThus.Where(p => p.LinkID == hd.ID & p.TableName == "dvHoaDon").Sum(p => p.SoTien)).GetValueOrDefault())
                                           }
                                   
                                   ).Sum(p=>p.ConNo);
                    var NoNuoc = (from 

                                      dv in db.dvHoaDons
                                  where dv.MaTN == _MaTN & dv.MaKH == _MaKH & dv.MaLDV == 9 //& dv.NgayTT.Value.Month == _Thang 
                                     & dv.NgayTT.Value.Year <= _Nam
                                     //& dv.NgayTT.Value.Month <= _Thang
                                     & dv.IsDuyet == true
                                     &
                                     (dv.PhaiThu.GetValueOrDefault() -
                                     (db.ptChiTietPhieuThus.Where(p => p.LinkID == dv.ID & p.TableName == "dvHoaDon").Sum(p => p.SoTien)).GetValueOrDefault())
                                     > 0
                                     
                                     select new 
                                     {

                                         ConNo = (dv.PhaiThu.GetValueOrDefault() //-
                                     // (db.ptChiTietPhieuThus.Where(p => p.LinkID == dv.ID & p.TableName == "dvHoaDon").Sum(p => p.SoTien)).GetValueOrDefault()
                                      ),
                                         
                                     }).ToList();
                    cDonGia.Text = string.Format("{0:#,0}", ltGiuXeCongNo2.Sum(p => p.ThanhTien) );
                    cSum.Text = string.Format("{0:#,0}", _TienTT + NoNuoc.Sum(p => p.ConNo));
                
            
                }
               

         
                 
            }
            catch { cNoiDung.Text = "Phí giữ xe :"; cDonGia.Text = string.Format("{0:#,0.##}", 0); }
            finally
            {
                db.Dispose();
            }
        }


        public void LoadData(byte _MaTN, int _MaKH, int _Thang, int _Nam)
        {
       
            var rpt = new rptPQLHoaPhat();
            xrSubreport1.ReportSource = rpt;
            var rpt1 = new rptNuocHoaPhat();
            xrSubreport2.ReportSource = rpt1;
            var db = new MasterDataContext();
            try
            {
                if (_MaTN != 27)
                {
                    var ltGiuXete = (from tx in db.dvgxTheXes
                                     join lx in db.dvgxLoaiXes on tx.MaLX equals lx.MaLX into dslx
                                     from lx in dslx.DefaultIfEmpty()
                                     join dv in db.dvHoaDons on tx.ID equals dv.LinkID
                                     join nhomxe in db.dvgxNhomXes on lx.MaNX equals nhomxe.ID
                                     where tx.MaTN == _MaTN & tx.MaKH == _MaKH & tx.NgungSuDung == false //& dv.NgayTT.Value.Month == _Thang 
                                     & dv.NgayTT.Value.Year == _Nam

                                     & dv.IsDuyet == true
                                     &
                                     (dv.PhaiThu.GetValueOrDefault() -
                                     (db.ptChiTietPhieuThus.Where(p => p.LinkID == dv.ID & p.TableName == "dvHoaDon").Sum(p => p.SoTien)).GetValueOrDefault())
                                     > 0
                                     group new { tx, dv } by new { tx.MaLX, nhomxe.TenNX, lx.TenLX, tx.GiaThang, Thang = dv.NgayTT.Value.Month, Nam = dv.NgayTT.Value.Year } into gr
                                     select new GiuXeItem2()
                                     {
                                         MaLX = gr.Key.MaLX,
                                         TenLX = gr.Key.TenLX,
                                         TenNX = gr.Key.TenNX,
                                         BienSo = "",
                                         SoLuong = gr.Count(),
                                         ThanhTien = gr.Sum(p => p.dv.ConNo).GetValueOrDefault(),
                                         Thang = gr.Key.Thang,
                                         Nam = gr.Key.Nam,
                                     }).ToList();
                    string Quy = "";
                    string QuyNew = ",";
                    string ThangCu = "";
                    string ThangMoi = ",";
                    foreach (var i in ltGiuXete)
                    {
                        var ltBienSo = (from tx in db.dvgxTheXes
                                        join lx in db.dvgxLoaiXes on tx.MaLX equals lx.MaLX
                                        join nhomxe in db.dvgxNhomXes on lx.MaNX equals nhomxe.ID
                                        where tx.MaTN == _MaTN & tx.MaKH == _MaKH & tx.NgungSuDung == false & tx.MaLX == i.MaLX
                                        select new { TenLX = nhomxe.TenNX, LX = lx.TenLX, SoLuong = i.SoLuong }).ToList().Take(1);

                        if (i.Thang >= 1 & i.Thang <= 3)
                        {
                            Quy = "Quý I" + "/" + i.Nam; string tam = "";
                            foreach (var bs in ltBienSo)
                                if (!string.IsNullOrEmpty(bs.TenLX))
                                {
                                    if (i.TenNX == "o to")
                                    {
                                        tam += bs.SoLuong + " " + bs.LX;
                                    }
                                    else
                                    {
                                        tam += bs.SoLuong + " " + bs.TenLX;
                                    }

                                }
                            if (Quy == QuyNew)
                            {
                                if (i.TenNX == "o to")
                                {
                                    i.BienSo += string.Format("T{0}/{1}", i.Thang, i.Nam) + " " + tam + ", ";
                                }
                                else
                                {
                                    i.BienSo += tam + ", ";
                                }

                            }
                            else
                            {
                                i.BienSo += Quy + " " + tam + ", ";
                            }
                            QuyNew = Quy;

                        }
                        if (i.Thang >= 4 & i.Thang <= 6)
                        {
                            Quy = "Quý II" + "/" + i.Nam; ; string tam = "";
                            foreach (var bs in ltBienSo)
                                if (!string.IsNullOrEmpty(bs.TenLX))
                                {
                                    if (i.TenNX == "o to")
                                    {
                                        tam += bs.SoLuong + " " + bs.LX;
                                    }
                                    else
                                    {
                                        tam += bs.SoLuong + " " + bs.TenLX;
                                    }

                                }
                            if (Quy == QuyNew)
                            {
                                if (i.TenNX == "o to")
                                {
                                    i.BienSo += string.Format("T{0}/{1}", i.Thang, i.Nam) + " " + tam + ", ";
                                }
                                else
                                {
                                    i.BienSo += tam + ", ";
                                }

                            }
                            else
                            {
                                i.BienSo += Quy + " " + tam + ", ";
                            }
                            QuyNew = Quy;
                        }
                        if (i.Thang >= 7 & i.Thang <= 9)
                        {
                            Quy = "Quý III" + "/" + i.Nam; ; string tam = "";
                            ThangCu = i.Thang.ToString();
                            foreach (var bs in ltBienSo)
                                if (!string.IsNullOrEmpty(bs.TenLX))
                                {
                                    if (ThangCu == ThangMoi)
                                    {

                                    }
                                    else
                                    {
                                        if (i.TenNX == "o to")
                                        {
                                            tam += bs.SoLuong + " " + bs.LX;
                                        }
                                        else
                                        {
                                            tam += bs.SoLuong + " " + bs.TenLX;
                                        }
                                    }

                                }
                            if (Quy == QuyNew)
                            {
                                if (i.TenNX == "o to")
                                {
                                    i.BienSo += string.Format("T{0}/{1}", i.Thang, i.Nam) + " " + tam + ", ";
                                }
                                else
                                {
                                    i.BienSo += tam + ", ";
                                }

                            }
                            else
                            {
                                i.BienSo += Quy + " " + tam + ", ";
                            }
                            QuyNew = Quy;
                        }
                        if (i.Thang >= 10 & i.Thang <= 12)
                        {
                            Quy = "Quý IV" + "/" + i.Nam; ;
                            ThangCu = i.Thang.ToString();
                            string tam = "";
                            foreach (var bs in ltBienSo)
                                if (!string.IsNullOrEmpty(bs.TenLX))
                                {
                                    if (ThangCu == ThangMoi)
                                    {

                                    }
                                    else
                                    {
                                        if (i.TenNX == "o to")
                                        {
                                            tam += bs.SoLuong + " " + bs.LX;
                                        }
                                        else
                                        {
                                            tam += bs.SoLuong + " " + bs.TenLX;
                                        }
                                    }
                                    

                                }
                            if (Quy == QuyNew)
                            {
                                if (i.TenNX == "o to")
                                {
                                    i.BienSo += string.Format("T{0}/{1}", i.Thang, i.Nam) + " " + tam + ", ";
                                }
                                else
                                {
                                    i.BienSo += tam + ", ";
                                }

                            }
                            else
                            {
                                i.BienSo += Quy + " " + tam + ", ";
                            }
                            QuyNew = Quy;
                            ThangMoi = ThangCu;
                        }
                        //i.BienSo = i.BienSo.Trim(' ').Trim(';');
                        cNoiDung.Text += string.Format("{0}", i.BienSo);

                    }

                    cNoiDung.Text = "Phí giữ xe - " + cNoiDung.Text.Remove(cNoiDung.Text.Length - 2);
                    var _TienTT = (from hd in db.dvHoaDons
                                   //join ltt in db.dvgxLichThanhToans on new { hd.TableName, hd.LinkID } equals new { TableName = "dvgxLichThanhToan", LinkID = (int?)ltt.ID }
                                   where hd.MaTN == _MaTN & (hd.MaLDV == 6 | hd.MaLDV == 13) & hd.MaKH == _MaKH & hd.NgayTT.Value.Year == _Nam & hd.ConNo.GetValueOrDefault() > 0
                                   select hd.ConNo).Sum().GetValueOrDefault();
                    var NoNuoc = (from tx in db.dvNuocs

                                  join dv in db.dvHoaDons on tx.ID equals dv.LinkID
                                  where tx.MaTN == _MaTN & tx.MaKH == _MaKH & dv.MaLDV == 9 //& dv.NgayTT.Value.Month == _Thang 
                                  & dv.NgayTT.Value.Year == _Nam
                                  & dv.NgayTT.Value.Month <= _Thang
                                  & dv.IsDuyet == true
                                  &
                                  (dv.PhaiThu.GetValueOrDefault() -
                                  (db.ptChiTietPhieuThus.Where(p => p.LinkID == dv.ID & p.TableName == "dvHoaDon").Sum(p => p.SoTien)).GetValueOrDefault())
                                  > 0

                                  select new
                                  {

                                      ConNo = dv.ConNo.GetValueOrDefault(),

                                  }).ToList();
                    cDonGia.Text = string.Format("{0:#,0.##}", ltGiuXete.Sum(p => p.ThanhTien));
                    cSum.Text = string.Format("{0:#,0.##}", _TienTT + NoNuoc.Sum(p => p.ConNo));


                }




            }
            catch { cNoiDung.Text = "Phí giữ xe"; cDonGia.Text = string.Format("{0:#,0.##}", 0); }
            finally
            {
                db.Dispose();
            }
        }
        private void xrSubreport1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            
            var rpt = xrSubreport1.ReportSource as rptPQLHoaPhat;
            rpt.LoadData(_MaTN1, _MaKH1,_Nam1);
        }

        private void xrSubreport2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var rpt = xrSubreport2.ReportSource as rptNuocHoaPhat;
            rpt.LoadData(_MaTN1,_MaKH1,_Thang1, _Nam1);
        }

    }

    class GiuXeItem2
    {
        public int? MaLX { get; set; }
        public string TenLX { get; set; }
        public string BienSo { get; set; }
        public int? SoLuong { get; set; }
        public int? Thang { get; set; }
        public int? Nam { get; set; }
        public decimal? DonGia { get; set; }
        public decimal? ThanhTien { get; set; }
        public string TenNX { get; set; }
    }
    class GiuXeItem3
    {
        public int? MaLX { get; set; }
        public string TenLX { get; set; }
        public string BienSo { get; set; }
        public int? SoLuong { get; set; }
        public int? Thang { get; set; }
        public int? Nam { get; set; }
        public decimal? DonGia { get; set; }
        public decimal? ThanhTien { get; set; }
        public string TenNX { get; set; }
    }
}
