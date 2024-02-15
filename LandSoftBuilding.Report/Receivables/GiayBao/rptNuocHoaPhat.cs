using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using DevExpress.XtraReports.UI;
using Library;

namespace LandSoftBuilding.Receivables.GiayBao
{
    public partial class rptNuocHoaPhat : DevExpress.XtraReports.UI.XtraReport
    {
        public rptNuocHoaPhat()
        {
            InitializeComponent();

        }

        public void LoadData(byte _MaTN, int _MaKH, int Thang, int _Nam)
        {
            var db = new MasterDataContext();
            try
            {
                if (_MaTN != 27)
                {
                    var ltGiuXete = (from tx in db.dvNuocs

                        join dv in db.dvHoaDons on tx.ID equals dv.LinkID
                        where tx.MaTN == _MaTN & tx.MaKH == _MaKH & dv.MaLDV == 9 //& dv.NgayTT.Value.Month == _Thang 
                            //  & dv.NgayTT.Value.Year <= _Nam
                            //  & dv.NgayTT.Value.Month <= Thang
                              & dv.IsDuyet == true
                              // & dv.NgayTT.Value.Month <= Thang 
                               & dv.NgayTT.Value.Year<= _Nam
                              &
                              (dv.PhaiThu.GetValueOrDefault() -
                               (db.ptChiTietPhieuThus.Where(p => p.LinkID == dv.ID & p.TableName == "dvHoaDon")
                                   .Sum(p => p.SoTien)).GetValueOrDefault())
                              > 0
                        group new {tx, dv} by new {Thang = dv.NgayTT.Value.Month, Nam = dv.NgayTT.Value.Year,}
                        into gr
                        select new PhiNuoc()
                        {
                            DienGiai = "",
                            ThanhTien = gr.Sum(p => p.dv.ConNo).GetValueOrDefault(),
                            Thang = gr.Key.Thang,
                            Nam = gr.Key.Nam,
                        }).ToList();
                    var NoNuoc2 = (from

                               dv in db.dvHoaDons
                                  where dv.MaTN == _MaTN & dv.MaKH == _MaKH & dv.MaLDV == 9 //& dv.NgayTT.Value.Month == _Thang 
                                      //& dv.NgayTT.Value.Year <= _Nam
                               & dv.NgayTT.Value.Year <= _Nam
                                  & dv.IsDuyet == true
                                  &
                                  (dv.PhaiThu.GetValueOrDefault() -
                                  (db.ptChiTietPhieuThus.Where(p => p.LinkID == dv.ID & p.TableName == "dvHoaDon").Sum(p => p.SoTien)).GetValueOrDefault())
                                  > 0

                                  select new
                                  {

                                      ConNo = Math.Round((decimal)dv.PhaiThu.GetValueOrDefault(), MidpointRounding.AwayFromZero)
                                      -
                                  (db.ptChiTietPhieuThus.Where(p => p.LinkID == dv.ID & p.TableName == "dvHoaDon").Sum(p => p.SoTien)).GetValueOrDefault()
                                      ,

                                  }).ToList();
                    var NuocNo = (from dv in db.dvHoaDons
                                  where dv.MaTN == _MaTN & dv.MaKH == _MaKH & dv.MaLDV == 9 //& dv.NgayTT.Value.Month == _Thang 
                                               //& dv.NgayTT.Value.Month <= Thang 
                                               & dv.NgayTT.Value.Year <= _Nam
                                        & dv.LinkID == null
                                      //& dv.NgayTT.Value.Month < Thang
                                        & dv.IsDuyet == true
                                        &
                                        (dv.PhaiThu.GetValueOrDefault() -
                                         (db.ptChiTietPhieuThus.Where(p => p.LinkID == dv.ID & p.TableName == "dvHoaDon")
                                             .Sum(p => p.SoTien)).GetValueOrDefault())
                                        > 0
                                  select new PhiNuoc1()
                                  {
                                      ThanhTien = (dv.PhaiThu.GetValueOrDefault() -
                                                   (db.ptChiTietPhieuThus.Where(p => p.LinkID == dv.ID & p.TableName == "dvHoaDon")
                                                       .Sum(p => p.SoTien)).GetValueOrDefault()),
                                      DienGiai = dv.DienGiai
                                  }).ToList();
                    foreach (var i in NuocNo)
                    {
                        if (cNoiDung.Text == "Phí nước: ")
                        {
                            cNoiDung.Text += i.DienGiai + " ";
                        }
                        else
                        {
                            cNoiDung.Text +=  i.DienGiai + ", ";
                        }
                    }
                    cNoiDung.Text = cNoiDung.Text.Length > 0 ? "Phí nước: " + cNoiDung.Text : "Phí nước: ";
                    string Quy = "";
                    string QuyNew = ",";
                    foreach (var i in ltGiuXete)
                    {

                        if (i.Thang >= 1 & i.Thang <= 3)
                        {

                            Quy = "Quý I" + "/" + i.Nam;
                            string tam = "";

                            if (Quy == QuyNew)
                            {
                                i.DienGiai += string.Format("{0}", i.Thang - 1==0 ? 12 : i.Thang-1) + "/" + (i.Thang - 1 == 0 ? string.Format("{0}", i.Nam - 1) : string.Format("{0}", i.Nam)) + ", ";
                            }
                            else
                            {
                                i.DienGiai += " tháng " + string.Format("{0}", i.Thang - 1 == 0 ? 12 : i.Thang-1) + "/" + (i.Thang - 1 == 0 ? string.Format("{0}", i.Nam - 1) : string.Format("{0}", i.Nam)) + " " + tam + ", ";
                            }
                            QuyNew = Quy;

                        }
                        if (i.Thang >= 4 & i.Thang <= 6)
                        {
                            Quy = "Quý II" + "/" + i.Nam;
                            ;
                            string tam = "";

                            if (Quy == QuyNew)
                            {
                                i.DienGiai += string.Format("{0}", i.Thang - 1 == 0 ? 12 : i.Thang - 1) + "/" + (i.Thang - 1 == 0 ? string.Format("{0}", i.Nam - 1) : string.Format("{0}", i.Nam)) + ", ";
                            }
                            else
                            {
                                i.DienGiai += " tháng " + string.Format("{0}", i.Thang - 1 == 0 ? 12 : i.Thang - 1) + "/" + (i.Thang - 1 == 0 ? string.Format("{0}", i.Nam - 1) : string.Format("{0}", i.Nam)) + " " + tam + ", ";
                            }
                            QuyNew = Quy;
                        }
                        if (i.Thang >= 7 & i.Thang <= 9)
                        {
                            Quy = "Quý III" + "/" + i.Nam;
                            ;
                            string tam = "";

                            if (Quy == QuyNew)
                            {
                                i.DienGiai += string.Format("{0}", i.Thang - 1 == 0 ? 12 : i.Thang - 1) + "/" + (i.Thang - 1 == 0 ? string.Format("{0}", i.Nam - 1) : string.Format("{0}", i.Nam)) + ", ";
                            }
                            else
                            {
                                i.DienGiai += " tháng " + string.Format("{0}", i.Thang - 1 == 0 ? 12 : i.Thang - 1) + "/" + (i.Thang - 1 == 0 ? string.Format("{0}", i.Nam - 1) : string.Format("{0}", i.Nam)) + " " + tam + ", ";
                            }
                            QuyNew = Quy;
                        }
                        if (i.Thang >= 10 & i.Thang <= 12)
                        {
                            Quy = "Quý IV" + "/" + i.Nam;
                            ;
                            string tam = "";

                            if (Quy == QuyNew)
                            {
                                i.DienGiai += string.Format("{0}", i.Thang - 1 == 0 ? 12 : i.Thang - 1) + "/" + (i.Thang - 1 == 0 ? string.Format("{0}", i.Nam - 1) : string.Format("{0}", i.Nam)) + ", ";
                            }
                            else
                            {
                                i.DienGiai += " tháng " + string.Format("{0}", i.Thang - 1 == 0 ? 12 : i.Thang - 1) + "/" + (i.Thang - 1 == 0 ? string.Format("{0}", i.Nam - 1) : string.Format("{0}", i.Nam)) + " " + tam + ", ";
                            }
                            QuyNew = Quy;
                        }







                        //i.BienSo = i.BienSo.Trim(' ').Trim(';');
                        cNoiDung.Text += string.Format("{0}", i.DienGiai);

                    }

                    cNoiDung.Text = cNoiDung.Text.Length > 0 ? cNoiDung.Text.Remove(cNoiDung.Text.Length-2) : "";

                    cDonGia.Text = string.Format("{0:#,0.##}", Math.Round((decimal)NoNuoc2.Sum(p => p.ConNo), MidpointRounding.AwayFromZero));


                }




            }
            catch
            {
                cNoiDung.Text = "Phí nước:"; cDonGia.Text = string.Format("{0:#,0.##}",0);
            }
            finally
            {
                db.Dispose();
            }
        }
    }
    class PhiNuoc
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
    class PhiNuoc1
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
