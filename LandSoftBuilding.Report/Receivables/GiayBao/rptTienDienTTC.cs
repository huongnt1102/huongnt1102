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
    public partial class rptTienDienTTC : DevExpress.XtraReports.UI.XtraReport
    {
        public rptTienDienTTC(byte _MaTN, int _MaKH, int _Thang, int _Nam)
        {
            InitializeComponent();

            Library.frmPrintControl.LoadLayout(this, 6, _MaTN);

            #region Bingding
            GroupHeader1.GroupFields.Add(new GroupField("MaMB"));

           
            
            cCSC.DataBindings.Add("Text", null, "ChiSoCu", "{0:#,0.##}");
            cCSM.DataBindings.Add("Text", null, "ChiSoMoi", "{0:#,0.##}");
           // cSoTieuThu.DataBindings.Add("Text", null, "SoTieuThu", "{0:#,0.##}");
            cTienDien.DataBindings.Add("Text", null, "TienDien", "{0:#,0.##}");
           // cTyLeVAT.DataBindings.Add("Text", null, "TyLeVAT", "Thuế VAT {0:p0}");
           // cTienVAT.DataBindings.Add("Text", null, "TienDien", "{0:#,0.##}");
           
            cSTT.DataBindings.Add("Text", null, "STT");
            cSTT.Summary = new XRSummary(SummaryRunning.Group, SummaryFunc.RecordNumber, "{0:#}");
           
            cSoLuong.DataBindings.Add("Text", null, "SoLuong", "{0:#,0.##}");
            cDonGia.DataBindings.Add("Text", null, "DonGia", "{0:#,0.##}");
            cThanhTien.DataBindings.Add("Text", null, "ThanhTien", "{0:#,0.##}");
            #endregion

            var db = new MasterDataContext();
            try
            {
               var tam = (from hd in db.dvHoaDons
                                   join td in db.dvDiens on new { hd.TableName, hd.LinkID } equals new { TableName = "dvDien", LinkID = (int?)td.ID }
                                   join ct in db.dvDienChiTiets on td.ID equals ct.MaDien
                                   join dm in db.dvDienDinhMucs on ct.MaDM equals dm.ID
                                   join mb in db.mbMatBangs on td.MaMB equals mb.MaMB
                                   join tl in db.mbTangLaus on mb.MaTL equals tl.MaTL
                                   where hd.MaTN == _MaTN & hd.MaKH == _MaKH & hd.NgayTT.Value.Month == _Thang & hd.NgayTT.Value.Year == _Nam
                                    & (hd.PhaiThu.GetValueOrDefault()
                    - (db.ptChiTietPhieuThus.Where(p => p.LinkID == hd.ID & p.TableName == "dvHoaDon").Sum(p => p.SoTien)).GetValueOrDefault()
                    - (db.ktttChiTiets.Where(p => p.LinkID == hd.ID & p.TableName == "dvHoaDon").Sum(p => p.SoTien)).GetValueOrDefault()
                    ) > 0
                                   orderby mb.MaSoMB, dm.STT
                                   select new
                                   {
                                       td.MaMB,
                                       mb.MaSoMB,
                                       tl.TenTL,
                                       td.TuNgay,
                                       td.DenNgay,
                                       td.ChiSoCu,
                                       td.ChiSoMoi,
                                       td.SoTieuThu,
                                       TienDien =  td.ThanhTien,
                                       td.TienVAT,
                                       dm.TenDM,
                                       ct.SoLuong,
                                       DonGia =  ct.DonGia,//
                                       ThanhTien =  ct.ThanhTien,
                                       ConNo = hd.PhaiThu - db.SoQuy_ThuChis.Where(p => p.LinkID == hd.ID).Sum(p => p.DaThu+p.KhauTru).GetValueOrDefault()
                                   }).ToList();
                if (tam.Count() == 0) return;
                this.DataSource = tam;
                cTuNgayDenNgay.Text = string.Format("Từ/From    {0:dd/MM/yyyy}           Đến/to     {1:dd/MM/yyyy}", tam.First().TuNgay, tam.First().DenNgay);
            }
            catch { }
            finally
            {
                db.Dispose();
            }
        }
    }
}
