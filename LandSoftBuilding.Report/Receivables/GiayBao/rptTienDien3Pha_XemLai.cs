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
    public partial class rptTienDien3Pha_XemLai : DevExpress.XtraReports.UI.XtraReport
    {
        public rptTienDien3Pha_XemLai(byte _MaTN, int _MaKH, int _Thang, int _Nam)
        {
            InitializeComponent();

            Library.frmPrintControl.LoadLayout(this, 7, _MaTN);

            #region Bingding
            GroupHeader1.GroupFields.Add(new GroupField("MaMB"));

            cMaSoMB.DataBindings.Add("Text", null, "MaSoMB");
            cTenTL.DataBindings.Add("Text", null, "TenTL");
            cTuNgay.DataBindings.Add("Text", null, "TuNgay", "{0:dd/MM/yyyy}");
            cDenNgay.DataBindings.Add("Text", null, "DenNgay", "{0:dd/MM/yyyy}");
            cSoTieuThu.DataBindings.Add("Text", null, "SoTieuThu", "{0:#,0.##}");
            cTienDien.DataBindings.Add("Text", null, "TienDien", "{0:#,0.##}");
            cTyLeVAT.DataBindings.Add("Text", null, "TyLeVAT", "Thuế VAT {0:p0}");
            cTienVAT.DataBindings.Add("Text", null, "TienVAT", "{0:#,0.##}");

            cSTT.DataBindings.Add("Text", null, "STT");
            cSTT.Summary = new XRSummary(SummaryRunning.Group, SummaryFunc.RecordNumber, "{0:#}");
            cTenDM.DataBindings.Add("Text", null, "TenDM");
            cChiSoCu.DataBindings.Add("Text", null, "ChiSoCu", "{0:#,0.##}");
            cChiSoMoi.DataBindings.Add("Text", null, "ChiSoMoi", "{0:#,0.##}");
            cSoLuong.DataBindings.Add("Text", null, "SoLuong", "{0:#,0.##}");
            cDonGia.DataBindings.Add("Text", null, "DonGia", "{0:#,0.##}");
            cThanhTien.DataBindings.Add("Text", null, "ThanhTien", "{0:#,0.##}");
            #endregion

            var db = new MasterDataContext();
            try
            {
                this.DataSource = (from hd in db.dvHoaDons
                                   join td in db.dvDien3Phas on new { hd.TableName, hd.LinkID } equals new { TableName = "dvDien3Pha", LinkID = (int?)td.ID }
                                   join ct in db.dvDien3PhaChiTiets on td.ID equals ct.MaDien
                                   join dm in db.dvDien3PhaDinhMucs on ct.MaDM equals dm.ID
                                   join mb in db.mbMatBangs on td.MaMB equals mb.MaMB
                                   join tl in db.mbTangLaus on mb.MaTL equals tl.MaTL
                                   where hd.MaTN == _MaTN & hd.MaKH == _MaKH & hd.NgayTT.Value.Month == _Thang & hd.NgayTT.Value.Year == _Nam
                                   orderby mb.MaSoMB, dm.STT
                                   select new
                                   {
                                       td.MaMB,
                                       mb.MaSoMB,
                                       tl.TenTL,
                                       td.TuNgay,
                                       td.DenNgay,
                                       td.SoTieuThu,
                                       TienDien = td.ThanhTien,
                                       td.TyLeVAT,
                                       td.TienVAT,
                                       dm.TenDM,
                                       ct.ChiSoCu,
                                       ct.ChiSoMoi,
                                       ct.SoLuong,
                                       ct.DonGia,
                                       ct.ThanhTien
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
