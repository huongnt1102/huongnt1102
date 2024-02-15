using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
using Library;
using System.Data.Linq.SqlClient;

namespace LandSoftBuilding.Receivables.GiayBao
{
    public partial class rptTheXe : DevExpress.XtraReports.UI.XtraReport
    {
        public rptTheXe(byte _MaTN, int _MaKH, int _Thang, int _Nam)
        {
            InitializeComponent();

            Library.frmPrintControl.LoadLayout(this, 13, _MaTN);

            cSTT.DataBindings.Add("Text", null, "STT");
            cSTT.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.RecordNumber, "{0:#}");

            cTenLX.DataBindings.Add("Text", null, "TenLX");
            cBienSo.DataBindings.Add("Text", null, "BienSo");
            cSoLuong.DataBindings.Add("Text", null, "SoLuong", "{0:#,0.##}");
            cDonGia.DataBindings.Add("Text", null, "DonGia", "{0:#,0.##}");
            cThanhTien.DataBindings.Add("Text", null, "ThanhTien", "{0:#,0.##}");
            cTienXe.DataBindings.Add("Text", null, "ThanhTien", "{0:#,0.##}");
            cTienXe.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,0.##}");
            var db = new MasterDataContext();
            try
            {


                
                if (_MaTN == 27)
                {
                    var ltGiuXete = (from tx in db.dvgxTheXes
                                   join lx in db.dvgxLoaiXes on tx.MaLX equals lx.MaLX
                                  // join gx in db.dvgxGiuXes on tx.MaGX equals gx.ID
                                     where tx.MaTN == _MaTN & tx.MaKH == _MaKH & tx.NgungSuDung == false
                                   group tx by new { tx.MaLX, lx.TenLX, tx.GiaThang } into gr
                                   select new GiuXeItem()
                                   {
                                       MaLX = gr.Key.MaLX,
                                       TenLX = gr.Key.TenLX,
                                       BienSo = "",
                                       SoLuong = gr.Count(),
                                       DonGia = gr.Key.GiaThang,
                                       ThanhTien = gr.Count() * gr.Key.GiaThang
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
                }
                else
                {
                    //Code Lâm sửa cho Kim Cương Xanh
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
                                     where tx.MaTN == _MaTN & tx.MaKH == _MaKH & tx.NgungSuDung == false & dv.NgayTT.Value.Month == _Thang & dv.NgayTT.Value.Year == _Nam & pt.DaThu == null
                                     group tx by new { tx.MaLX, lx.TenLX, tx.GiaThang } into gr
                                     select new GiuXeItem()
                                     {
                                         MaLX = gr.Key.MaLX,
                                         TenLX = gr.Key.TenLX,
                                         BienSo = "",
                                         SoLuong = gr.Count(),
                                         DonGia = gr.Key.GiaThang,
                                         ThanhTien = gr.Count() * gr.Key.GiaThang
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
                    //Code cũ 
                     //var ltGiuXe = (from tx in db.dvgxTheXes
                     //              join lx in db.dvgxLoaiXes on tx.MaLX equals lx.MaLX
                     //              join gx in db.dvgxGiuXes on tx.MaGX equals gx.ID
                     //              where gx.MaTN == _MaTN & gx.MaKH == _MaKH & gx.NgungSuDung == false
                     //              group tx by new { tx.MaLX, lx.TenLX, tx.GiaThang } into gr
                     //              select new GiuXeItem()
                     //              {
                     //                  MaLX = gr.Key.MaLX,
                     //                  TenLX = gr.Key.TenLX,
                     //                  BienSo = "",
                     //                  SoLuong = gr.Count(),
                     //                  DonGia = gr.Key.GiaThang,
                     //                  ThanhTien = gr.Count() * gr.Key.GiaThang
                     //              }).ToList();
                     //foreach (var i in ltGiuXe)
                     //{
                     //    var ltBienSo = (from tx in db.dvgxTheXes
                     //                    join gx in db.dvgxGiuXes on tx.MaGX equals gx.ID
                     //                    where gx.MaTN == _MaTN & gx.MaKH == _MaKH & gx.NgungSuDung == false & tx.MaLX == i.MaLX & tx.GiaThang == i.DonGia
                     //                    select tx.BienSo).ToList();

                     //    foreach (var bs in ltBienSo)
                     //        if (!string.IsNullOrEmpty(bs))
                     //            i.BienSo += bs + "; ";

                     //    i.BienSo = i.BienSo.Trim(' ').Trim(';');
                     //} this.DataSource = ltGiuXe;
                }
                //foreach (var i in ltGiuXe)
                //{
                //    var ltBienSo = (from tx in db.dvgxTheXes
                //                    join gx in db.dvgxGiuXes on tx.MaGX equals gx.ID
                //                    where gx.MaTN == _MaTN & gx.MaKH == _MaKH & gx.NgungSuDung == false & tx.MaLX == i.MaLX & tx.GiaThang == i.DonGia
                //                    select tx.BienSo).ToList();
                    
                //    foreach (var bs in ltBienSo)
                //        if (!string.IsNullOrEmpty(bs))
                //            i.BienSo += bs + "; ";

                //    i.BienSo = i.BienSo.Trim(' ').Trim(';');
                //}

                //this.DataSource = ltGiuXe;

                var _TienTT = (from hd in db.dvHoaDons
                               join ltt in db.dvgxLichThanhToans on new { hd.TableName, hd.LinkID } equals new { TableName = "dvgxLichThanhToan", LinkID = (int?)ltt.ID }
                               where hd.MaTN == _MaTN & hd.MaLDV == 6 & hd.MaKH == _MaKH & hd.NgayTT.Value.Month == _Thang & hd.NgayTT.Value.Year == _Nam & hd.ConNo.GetValueOrDefault() > 0
                               select hd.ConNo).Sum().GetValueOrDefault();
                 cTienXe.Text = string.Format("{0:#,0.##}", _TienTT);
            }
            catch { }
            finally
            {
                db.Dispose();
            }
        }

    }

    class GiuXeItem
    {
        public int? MaLX { get; set; }
        public string TenLX { get; set; }
        public string BienSo { get; set; }
        public int? SoLuong { get; set; }
        public decimal? DonGia { get; set; }
        public decimal? ThanhTien { get; set; }
        public decimal? NoCu { get; set; }
    }
}
