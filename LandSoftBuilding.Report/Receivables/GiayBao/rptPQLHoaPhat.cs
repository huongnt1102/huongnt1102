using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using DevExpress.XtraReports.UI;
using Library;

namespace LandSoftBuilding.Receivables.GiayBao
{
    public partial class rptPQLHoaPhat : DevExpress.XtraReports.UI.XtraReport
    {
        public rptPQLHoaPhat()
        {
            InitializeComponent();

        }

        public void LoadData(byte _MaTN, int _MaKH, int _Nam)
        {
            var db = new MasterDataContext();
            try
            {
                if (_MaTN != 27)
                {
                    var TT = (from tx in db.dvDichVuKhacs

                        join dv in db.dvHoaDons on tx.ID equals dv.LinkID
                        where tx.MaTN == _MaTN & tx.MaKH == _MaKH & dv.MaLDV == 13 //& dv.NgayTT.Value.Month == _Thang 
                             // & dv.NgayTT.Value.Year == _Nam
                              select new {tx.SoLuong}
                        ).FirstOrDefault()
                    ;
                    if (TT != null)
                    {
                        cThanhTien.Text = string.Format("{0:#,0.##} m2",TT.SoLuong );
                    }
                    var ltGiuXete = (from tx in db.dvDichVuKhacs
                                    
                                     join dv in db.dvHoaDons on tx.ID equals dv.LinkID
                                     where tx.MaTN == _MaTN & tx.MaKH == _MaKH & dv.MaLDV == 13 //& dv.NgayTT.Value.Month == _Thang 
                                     & dv.NgayTT.Value.Year <= _Nam

                                     & dv.IsDuyet == true
                                     &
                                     (dv.PhaiThu.GetValueOrDefault() -
                                     (db.ptChiTietPhieuThus.Where(p => p.LinkID == dv.ID & p.TableName == "dvHoaDon").Sum(p => p.SoTien)).GetValueOrDefault())
                                     > 0
                                     group new { tx, dv } by new { Thang = dv.NgayTT.Value.Month, Nam = dv.NgayTT.Value.Year, } into gr
                                     select new PQL()
                                     {
                                         DienGiai= "",
                                         ThanhTien = gr.Sum(p => p.dv.ConNo).GetValueOrDefault(),
                                         Thang = gr.Key.Thang,
                                         Nam = gr.Key.Nam,
                                     }).ToList();
                    
                    var PQLNo = (from dv in db.dvHoaDons
                                 where dv.MaTN == _MaTN & dv.MaKH == _MaKH & dv.MaLDV == 13 //& dv.NgayTT.Value.Month == _Thang 
                                      // & dv.NgayTT.Value.Year == _Nam 
                                      & 
                                       dv.LinkID == null
                                     //& dv.NgayTT.Value.Month <= Thang
                                       & dv.IsDuyet == true
                                       &
                                       (dv.PhaiThu.GetValueOrDefault() -
                                        (db.ptChiTietPhieuThus.Where(p => p.LinkID == dv.ID & p.TableName == "dvHoaDon")
                                            .Sum(p => p.SoTien)).GetValueOrDefault())
                                       > 0
                                 select new PQL1()
                                 {
                                     ThanhTien = (dv.PhaiThu.GetValueOrDefault() -
                                                  (db.ptChiTietPhieuThus.Where(p => p.LinkID == dv.ID & p.TableName == "dvHoaDon")
                                                      .Sum(p => p.SoTien)).GetValueOrDefault()),
                                     DienGiai = dv.DienGiai
                                 }).ToList();
                    foreach (var i in PQLNo)
                    {
                        if (cNoiDung.Text == "Phí quản lý: ")
                        {
                            cNoiDung.Text += i.DienGiai + " ";
                        }
                        else
                        {
                            cNoiDung.Text +=  i.DienGiai + ", ";
                        }
                    }
                    cNoiDung.Text = cNoiDung.Text.Length > 0 ? "Phí quản lý: " + cNoiDung.Text : "Phí quản lý: ";
                    string Quy = "";
                    string QuyNew = ",";
                    foreach (var i in ltGiuXete)
                    {
                        
                        if (i.Thang >= 1 & i.Thang <= 3)
                        {
                           
                            Quy = "Quý I" + "/" + i.Nam; string tam = "";

                            if (Quy == QuyNew)
                            {
                                i.DienGiai += i.Thang + "/" + string.Format("{0}", i.Nam) + ", ";
                            }
                            else
                            {
                                i.DienGiai += i.Thang + "/" + string.Format("{0}", i.Nam) + ", ";
                            }
                            QuyNew = Quy;

                        }
                        if (i.Thang >= 4 & i.Thang <= 6)
                        {
                            Quy = "Quý II" + "/" + i.Nam; ; string tam = "";

                            if (Quy == QuyNew)
                            {
                                i.DienGiai += i.Thang + "/" + string.Format("{0}", i.Nam)  + ", ";
                            }
                            else
                            {
                                i.DienGiai += i.Thang + "/" + string.Format("{0}", i.Nam) +   ", ";
                            }
                            QuyNew = Quy;
                        }
                        if (i.Thang >= 7 & i.Thang <= 9)
                        {
                            Quy = "Quý III" + "/" + i.Nam; ; string tam = "";

                            if (Quy == QuyNew)
                            {
                                i.DienGiai += i.Thang + "/" + string.Format("{0}", i.Nam) + ", ";
                            }
                            else
                            {
                                i.DienGiai += i.Thang + "/" + string.Format("{0}", i.Nam) + ", ";
                            }
                            QuyNew = Quy;
                        }
                        if (i.Thang >= 10 & i.Thang <= 12)
                        {
                            Quy = "Quý IV" + "/" + i.Nam; ;
                            string tam = "";

                            if (Quy == QuyNew)
                            {
                                i.DienGiai += i.Thang + "/" + string.Format("{0}", i.Nam) + ", ";
                            }
                            else
                            {
                                i.DienGiai += i.Thang + "/" + string.Format("{0}", i.Nam) + ", ";
                            }
                            QuyNew = Quy;
                        }







                        //i.BienSo = i.BienSo.Trim(' ').Trim(';');
                        cNoiDung.Text += string.Format("{0}", i.DienGiai);

                    }
                    
                    cNoiDung.Text = cNoiDung.Text.Length > 0  ? cNoiDung.Text.Remove(cNoiDung.Text.Length - 2): "Phí quản lý: ";
                   
                    cDonGia.Text = string.Format("{0:#,0.##}", ltGiuXete.Sum(p => p.ThanhTien) + Math.Round((decimal)PQLNo.Sum(p => p.ThanhTien), MidpointRounding.AwayFromZero));


                }




            }
            catch { cNoiDung.Text = "Phí quản lý:"; cDonGia.Text = string.Format("{0:#,0.##}", 0); }
            finally
            {
                db.Dispose();
            }
        }
    }
    class PQL
    {
        public int? MaLX { get; set; }
        public string TenLX { get; set; }
        public string DienGiai { get; set; }
        public int? SoLuong { get; set; }
        public int? Thang { get; set; }
        public int? Nam { get; set; }
        public decimal? DonGia { get; set; }
        public decimal? ThanhTien { get; set; }
    }
    class PQL1
    {
        public int? MaLX { get; set; }
        public string TenLX { get; set; }
        public string DienGiai { get; set; }
        public int? SoLuong { get; set; }
        public int? Thang { get; set; }
        public int? Nam { get; set; }
        public decimal? DonGia { get; set; }
        public decimal? ThanhTien { get; set; }
    }
}
