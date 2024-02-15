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
    public partial class rptTienThue_XemLai : DevExpress.XtraReports.UI.XtraReport
    {
        public rptTienThue_XemLai(byte _MaTN, int _MaKH, int _Thang, int _Nam)
        {
            InitializeComponent();

            Library.frmPrintControl.LoadLayout(this, 16, _MaTN);

            cSTT.DataBindings.Add("Text", null, "STT");
            cSTT.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.RecordNumber, "{0:#}");

            cViTri.DataBindings.Add("Text", null, "ViTri");
            cTuNgay.DataBindings.Add("Text", null, "TuNgay", "{0:dd/MM/yyyy}");
            cDenNgay.DataBindings.Add("Text", null, "DenNgay", "{0:dd/MM/yyyy}");
            cSoThang.DataBindings.Add("Text", null, "SoThang", "{0:#,0.##}");
            cGiaChoThue.DataBindings.Add("Text", null, "GiaChoThue", "{0:#,0.##}");
            cPhiDichVu.DataBindings.Add("Text", null, "PhiDichVu", "{0:#,0.##}");
            cDienTich.DataBindings.Add("Text", null, "DienTich", "{0:#,0.##} m2");
            cThanhTien.DataBindings.Add("Text", null, "ThanhTien", "{0:#,0.##}");
            cDienGiai.DataBindings.Add("Text", null, "DienGiai");

            var db = new MasterDataContext();
            try
            {
                this.DataSource = (from hd in db.dvHoaDons
                                   join ltt in db.ctLichThanhToans on new { hd.TableName, hd.LinkID } equals new { TableName = "ctLichThanhToan", LinkID = (int?)ltt.ID }
                                   join ct in db.ctChiTiets on ltt.MaHD equals ct.MaHDCT
                                   join cthd in db.ctHopDongs on ct.MaHDCT equals cthd.ID
                                   join mb in db.mbMatBangs on ct.MaMB equals mb.MaMB
                                   join tl in db.mbTangLaus on mb.MaTL equals tl.MaTL
                                   where hd.MaTN == _MaTN & hd.MaLDV == 2 & hd.MaKH == _MaKH & hd.NgayTT.Value.Month == _Thang & hd.NgayTT.Value.Year == _Nam &(hd.PhaiThu.GetValueOrDefault()
                             - (db.SoQuy_ThuChis.Where(p => p.LinkID == hd.ID & p.TableName == "dvHoaDon" & SqlMethods.DateDiffDay(p.NgayPhieu, new DateTime(_Nam, _Thang, 1)) > 0).Sum(p => p.DaThu + p.KhauTru)).GetValueOrDefault()
                             )>(decimal)0
                                   select new
                                   {
                                       ViTri = tl.TenTL,
                                       ltt.TuNgay,
                                       ltt.DenNgay,
                                       SoThang = hd.KyTT,
                                       GiaChoThue = ct.DonGia * cthd.TyGia,
                                       PhiDichVu = ct.PhiDichVu * cthd.TyGia,
                                       ct.DienTich,
                                       ThanhTien = ct.ThanhTien * cthd.TyGia * hd.KyTT,
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
