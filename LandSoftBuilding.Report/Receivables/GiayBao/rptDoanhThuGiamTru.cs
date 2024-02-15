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
    public partial class rptDoanhThuGiamTru : DevExpress.XtraReports.UI.XtraReport
    {
        public rptDoanhThuGiamTru(byte _MaTN, int _MaKH, int _Thang, int _Nam, int _MaLDV)
        {
            InitializeComponent();

            //Library.frmPrintControl.LoadLayout(this, 5, _MaTN);


            cDienGiai.DataBindings.Add("Text", null, "DienGiai", "{0:#,0.##}");
            cThanhTien.DataBindings.Add("Text", null, "ThanhTien", "{0:#,0.##}");

            var db = new MasterDataContext();
            try
            {
                this.DataSource = (from hd in db.dvHoaDons
                                   where hd.MaTN == _MaTN & hd.MaLDV == _MaLDV & hd.MaKH == _MaKH & hd.NgayTT.Value.Month == _Thang & hd.NgayTT.Value.Year == _Nam & hd.ConNo.GetValueOrDefault() > 0
                                   select new
                                   {
                                       hd.DienGiai,
                                       ThanhTien = hd.ConNo,
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
