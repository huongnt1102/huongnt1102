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
    public partial class rptTienNuoc : DevExpress.XtraReports.UI.XtraReport
    {
        public rptTienNuoc(byte _MaTN, int _MaKH, int _Thang, int _Nam)
        {
            InitializeComponent();

            Library.frmPrintControl.LoadLayout(this, 9, _MaTN);

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
                               join tn in db.dvNuocs on new { hd.TableName, hd.LinkID } equals new { TableName = "dvNuoc", LinkID = (int?)tn.ID }
                               where hd.MaTN == _MaTN & hd.MaKH == _MaKH & hd.NgayTT.Value.Month == _Thang & hd.NgayTT.Value.Year == _Nam
                               select new
                               {
                                   tn.ID,
                                   tn.ChiSoCu,
                                   tn.ChiSoMoi,
                                   SoTieuThu=tn.SoTieuThu.GetValueOrDefault()+tn.SoTieuThuDHCu.GetValueOrDefault(),
                                   SoUuDai = (from ud in db.dvNuocUuDais where ud.MaMB == tn.MaMB select ud.SoNguoi).FirstOrDefault(),
                                   hd.ConNo
                               }).First();

                this.DataSource = (from ct in db.dvNuocChiTiets 
                                   join dm in db.dvNuocDinhMucs on ct.MaDM equals dm.ID
                                   where ct.MaNuoc == objNuoc.ID
                                   orderby dm.STT
                                   select new
                                   {
                                       dm.TenDM,
                                       ct.SoLuong,
                                       ct.DonGia,
                                       ct.ThanhTien
                                   }).ToList();

                cChiSoDau.Text = string.Format("{0:#,0.##}", objNuoc.ChiSoCu);
                cChiSoCuoi.Text = string.Format("{0:#,0.##}", objNuoc.ChiSoMoi);
                cSoTieuThu.Text = string.Format("{0:#,0.##}", objNuoc.SoTieuThu);
                cSoUuDai.Text = string.Format("{0:#,0.##}", objNuoc.SoUuDai);
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
