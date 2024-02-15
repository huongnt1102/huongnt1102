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
    public partial class rptTienDatCoc_XemLai : DevExpress.XtraReports.UI.XtraReport
    {
        public rptTienDatCoc_XemLai(byte _MaTN, int _MaKH, int _Thang, int _Nam)
        {
            InitializeComponent();

            Library.frmPrintControl.LoadLayout(this, 17, _MaTN);

            cSTT.DataBindings.Add("Text", null, "STT");
            cSTT.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.RecordNumber, "{0:#}");

            cSoHD.DataBindings.Add("Text", null, "SoHD");
            cDotTT.DataBindings.Add("Text", null, "DotTT", "{0:#,0.##}");
            cNgayTT.DataBindings.Add("Text", null, "NgayTT", "{0:dd/MM/yyyy}");
            cSoTien.DataBindings.Add("Text", null, "SoTien", "{0:#,0.##}");
            cDienGiai.DataBindings.Add("Text", null, "DienGiai");

            var db = new MasterDataContext();
            try
            {
                this.DataSource = (from hd in db.dvHoaDons
                                   join ltt in db.ctLichThanhToans on new { hd.TableName, hd.LinkID } equals new { TableName = "ctLichThanhToan", LinkID = (int?)ltt.ID }
                                   join cthd in db.ctHopDongs on ltt.MaHD equals cthd.ID
                                   where hd.MaTN == _MaTN & hd.MaLDV == 4 & hd.MaKH == _MaKH & hd.NgayTT.Value.Month == _Thang & hd.NgayTT.Value.Year == _Nam & (hd.PhaiThu.GetValueOrDefault()
                             - (db.SoQuy_ThuChis.Where(p => p.LinkID == hd.ID & p.TableName == "dvHoaDon" & SqlMethods.DateDiffDay(p.NgayPhieu, new DateTime(_Nam, _Thang, 1)) > 0).Sum(p => p.DaThu + p.KhauTru)).GetValueOrDefault()
                             ) > (decimal)0
                                   select new
                                   {
                                       SoHD = cthd.SoHDCT,
                                       ltt.DotTT,
                                       hd.NgayTT,
                                       SoTien = hd.ConNo,
                                       ltt.DienGiai
                                   }).ToList();
            }
            catch { }
            finally
            {
                db.Dispose();
            }
        }
    }
}
