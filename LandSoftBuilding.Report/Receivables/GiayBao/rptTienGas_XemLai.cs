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
    public partial class rptTienGas_XemLai : DevExpress.XtraReports.UI.XtraReport
    {
        public rptTienGas_XemLai(byte _MaTN, int _MaKH, int _Thang, int _Nam)
        {
            InitializeComponent();

            Library.frmPrintControl.LoadLayout(this, 12, _MaTN);

            #region Bingding
            cSTT.DataBindings.Add("Text", null, "STT");
            cSTT.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.RecordNumber, "{0:#}");
            cSoLuongM3.DataBindings.Add("Text", null, "SoLuongM3", "{0:#,0.##}");
            cSoLuong.DataBindings.Add("Text", null, "SoLuong", "{0:#,0.##}");
            cDonGia.DataBindings.Add("Text", null, "DonGia", "{0:#,0.##}");
            cThanhTien.DataBindings.Add("Text", null, "ThanhTien", "{0:#,0.##}");
            #endregion

            var db = new MasterDataContext();
            try
            {
                var objGas = (from hd in db.dvHoaDons
                              join ts in db.dvGas on new { hd.TableName, hd.LinkID } equals new { TableName = "dvGas", LinkID = (int?)ts.ID }
                              where hd.MaTN == _MaTN & hd.MaKH == _MaKH & hd.NgayTT.Value.Month == _Thang & hd.NgayTT.Value.Year == _Nam
                              select new
                              {
                                  ts.ID,
                                  ts.ChiSoCu,
                                  ts.ChiSoMoi,
                                  ts.SoTieuThu,
                                  ts.TyLe,
                                  ConNo=hd.PhaiThu - db.SoQuy_ThuChis.Where(p => p.LinkID == hd.ID & SqlMethods.DateDiffDay(p.NgayPhieu, new DateTime(_Nam, _Thang, 1)) > 0).Sum(p => p.DaThu + p.KhauTru).GetValueOrDefault()
                              }).FirstOrDefault();

                this.DataSource = (from ct in db.dvGasChiTiets
                                   join dm in db.dvGasDinhMucs on ct.MaDM equals dm.ID
                                   where ct.MaGas == objGas.ID
                                   orderby dm.STT
                                   select new
                                   {
                                       SoLuongM3 = ct.SoLuong/objGas.TyLe,
                                       ct.SoLuong,
                                       ct.DonGia,
                                       ct.ThanhTien
                                   }).ToList();

                cChiSoDau.Text = string.Format("{0:#,0.##}", objGas.ChiSoCu);
                cChiSoCuoi.Text = string.Format("{0:#,0.##}", objGas.ChiSoMoi);
                cSoTieuThu.Text = string.Format("{0:#,0.##}", objGas.SoTieuThu);
                cTyLe.Text = string.Format("Quy đổi (1m3={0:#,0.##}kg)", objGas.TyLe);
                cTienGas.Text = string.Format("{0:#,0.##}", objGas.ConNo);
            }
            catch { }
            finally
            {
                db.Dispose();
            }
        }
    }
}
