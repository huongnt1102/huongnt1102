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
    public partial class rptsubTienNuoc : DevExpress.XtraReports.UI.XtraReport
    {
        public rptsubTienNuoc(byte _MaTN, int _MaKH, int _Thang, int _Nam)
        {
            InitializeComponent();

            Library.frmPrintControl.LoadLayout(this, 9, _MaTN);

            #region Bingding
            cTenDM.DataBindings.Add("Text", null, "TenDM");
            cSoLuong.DataBindings.Add("Text", null, "SoLuong", "{0:#,0.##}");
            cDonGia.DataBindings.Add("Text", null, "DonGia", "{0:#,0.##}");
            cThanhTien.DataBindings.Add("Text", null, "ThanhTien", "{0:#,0.##}");
            #endregion
            DateTime date = new DateTime(_Nam, _Thang, 1);
            var db = new MasterDataContext();
            try
            {
                var objNuoc = (from hd in db.dvHoaDons
                               join tn in db.dvNuocs on new { hd.TableName, hd.LinkID } equals new { TableName = "dvNuoc", LinkID = (int?)tn.ID }
                               where hd.MaTN == _MaTN & hd.MaKH == _MaKH & hd.NgayTT.Value.Month == _Thang & hd.NgayTT.Value.Year == _Nam
                               & hd.IsDuyet == true
                               select new
                               {
                                   tn.ID,
                                   tn.ChiSoCu,
                                   tn.ChiSoMoi,
                                   tn.SoTieuThu,
                                   SoUuDai = (from ud in db.dvNuocUuDais where ud.MaMB == tn.MaMB select ud.SoNguoi).FirstOrDefault(),
                                   hd.ConNo,
                                   tn.TienBVMT,
                                   tn.TienVAT,
                                   tn.ThanhTien,
                               }).First();
                var TienNuocDK = (from hd in db.dvHoaDons
                               join tn in db.dvNuocs on new { hd.TableName, hd.LinkID } equals new { TableName = "dvNuoc", LinkID = (int?)tn.ID }
                               where hd.MaTN == _MaTN & hd.MaKH == _MaKH & SqlMethods.DateDiffDay(hd.NgayTT, date) > 0
                               & hd.IsDuyet == true
                               select new
                               {
                                   SoTien = hd.PhaiThu - db.ptChiTietPhieuThus.Where(o => o.TableName == "dvHoaDon" & o.LinkID == hd.ID).Sum(o => o.SoTien).GetValueOrDefault()
                               }).Sum(o=>o.SoTien).GetValueOrDefault();

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
                //cSoUuDai.Text = string.Format("{0:#,0.##}", objNuoc.SoUuDai);
                cTienTruocThue.Text = string.Format("{0:#,0.##}", objNuoc.ThanhTien);
                cTienBVMT.Text = string.Format("{0:#,0.##}", objNuoc.TienBVMT);
                cTienThue.Text = string.Format("{0:#,0.##}", objNuoc.TienVAT);
                cPhaiThu.Text = string.Format("{0:#,0.##}", objNuoc.ConNo);
                cNoDK.Text = string.Format("{0:#,0.##}", TienNuocDK);
                cTongTienNuoc.Text = string.Format("{0:#,0.##}",objNuoc.ConNo + TienNuocDK);
            }
            catch { }
            finally
            {
                db.Dispose();
            }
        }
    }
}
