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
    public partial class rptDichVuCoBanTTC_XemLai : DevExpress.XtraReports.UI.XtraReport
    {
        public rptDichVuCoBanTTC_XemLai(byte _MaTN, int _MaKH, int _Thang, int _Nam, int _MaLDV)
        {
            InitializeComponent();

         //   Library.frmPrintControl.LoadLayout(this, 5, _MaTN);

            cSTT.DataBindings.Add("Text", null, "STT");
            cSTT.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.RecordNumber, "{0:#}");

            cSoLuong.DataBindings.Add("Text", null, "SoLuong", "{0:#,0.##}");
            //cTenDVT.DataBindings.Add("Text", null, "TenDVT");
            cDonGia.DataBindings.Add("Text", null, "DonGia", "{0:#,0.##}");
            cThanhTien.DataBindings.Add("Text", null, "ThanhTien", "{0:#,0.##}");

            csumThanhTien.DataBindings.Add("Text", null, "ThanhTien", "{0:#,0.##}");
            csumThanhTien.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,0.##}");

            var db = new MasterDataContext();
            try
            {
                this.DataSource = (from hd in db.dvHoaDons
                                   join dv in db.dvDichVuKhacs on new { hd.MaLDV, hd.LinkID } equals new { MaLDV = (int?)dv.MaLDV, LinkID = (int?)dv.ID }
                                   join dvt in db.DonViTinhs on dv.MaDVT equals dvt.ID
                                   where hd.MaTN == _MaTN & hd.MaLDV == _MaLDV & hd.MaKH == _MaKH & hd.NgayTT.Value.Month == _Thang & hd.NgayTT.Value.Year == _Nam & (hd.PhaiThu.GetValueOrDefault() - db.SoQuy_ThuChis.Where(sq => sq.LinkID == hd.LinkID && sq.TableName == "dvHoaDon" & SqlMethods.DateDiffDay(sq.NgayPhieu, new DateTime(_Nam, _Thang, 1)) > 0).Sum(s => s.DaThu + s.KhauTru).GetValueOrDefault()
                    ) > 0
                                   select new
                                   {
                                       dv.SoLuong,
                                       dvt.TenDVT,
                                       DonGia = dv.DonGia * dv.TyGia,
                                       ThanhTien = hd.PhaiThu,
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
