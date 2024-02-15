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
    public partial class rptTienNuocNong : DevExpress.XtraReports.UI.XtraReport
    {
        public rptTienNuocNong(byte _MaTN, int _MaKH, int _Thang, int _Nam)
        {
            InitializeComponent();

            Library.frmPrintControl.LoadLayout(this, 10, _MaTN);

            #region Bingding
            cSTT.DataBindings.Add("Text", null, "STT");
            cSTT.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.RecordNumber, "{0:#}");
            cTenDM.DataBindings.Add("Text", null, "TenDM");
            cSoLuong.DataBindings.Add("Text", null, "SoLuong", "{0:#,0.##}");
            cDonGia.DataBindings.Add("Text", null, "DonGia", "{0:#,0.##}");
            cThanhTien.DataBindings.Add("Text", null, "ThanhTien", "{0:#,0.##}");
            #endregion

            var db = new MasterDataContext();
            try
            {
                var objNuoc = (from hd in db.dvHoaDons
                               join tn in db.dvNuocNongs on new { hd.TableName, hd.LinkID } equals new { TableName = "dvNuocNong", LinkID = (int?)tn.ID }
                               where hd.MaTN == _MaTN & hd.MaKH == _MaKH & hd.NgayTT.Value.Month == _Thang & hd.NgayTT.Value.Year == _Nam
                               select new
                               {
                                   tn.ID,
                                   tn.DauCap,
                                   tn.DauHoi,
                                   tn.SoTieuThu,
                                   hd.ConNo
                               }).First();

                this.DataSource = (from ct in db.dvNuocNongChiTiets 
                                   join dm in db.dvNuocNongDinhMucs on ct.MaDM equals dm.ID
                                   where ct.MaNuoc == objNuoc.ID
                                   orderby dm.STT
                                   select new
                                   {
                                       dm.TenDM,
                                       ct.SoLuong,
                                       ct.DonGia,
                                       ct.ThanhTien
                                   }).ToList();

                cDauCap.Text = string.Format("{0:#,0.##}", objNuoc.DauCap);
                cDauHoi.Text = string.Format("{0:#,0.##}", objNuoc.DauHoi);
                cSoTieuThu.Text = string.Format("{0:#,0.##}", objNuoc.SoTieuThu);
                cTienNuoc.Text = string.Format("{0:#,0.##}", objNuoc.ConNo);
            }
            catch { }
            finally
            {
                db.Dispose();
            }
        }
    }
}
