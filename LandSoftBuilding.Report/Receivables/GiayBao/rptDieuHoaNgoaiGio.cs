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
    public partial class rptDieuHoaNgoaiGio : DevExpress.XtraReports.UI.XtraReport
    {
        public rptDieuHoaNgoaiGio(byte _MaTN, int _MaKH, int _Thang, int _Nam)
        {
            InitializeComponent();

            Library.frmPrintControl.LoadLayout(this, 8, _MaTN);

            cSTT.DataBindings.Add("Text", null, "STT");
            cSTT.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.RecordNumber, "{0:#}");

            //cNgayCT.DataBindings.Add("Text", null, "NgayCT", "{0:dd/MM/yyyy}");
            //cSoFCU.DataBindings.Add("Text", null, "SoFCU", "{0:#,0.##}");
            //cSoGio.DataBindings.Add("Text", null, "SoGio", "{0:#,0.##}");
            //cDonGia.DataBindings.Add("Text", null, "DonGia", "{0:#,0.##}");
            //cThanhTien.DataBindings.Add("Text", null, "ThanhTien", "{0:#,0.##}");
            //cDienGiai.DataBindings.Add("Text", null, "DienGiai");
            
            cNgayCT.DataBindings.Add("Text", null, "NgayTT", "{0:dd/MM/yyyy}");
            cSoFCU.DataBindings.Add("Text", null, "HeSo", "{0:#,0.##}");
            cSoGio.DataBindings.Add("Text", null, "SoTieuThu", "{0:#,0.##}");
            cDonGia.DataBindings.Add("Text", null, "DonGia", "{0:#,0.##}");
            cThanhTien.DataBindings.Add("Text", null, "ThanhTien", "{0:#,0.##}");
            cDienGiai.DataBindings.Add("Text", null, "DienGiai");

            var db = new MasterDataContext();
            try
            {
                //this.DataSource = (from hd in db.dvHoaDons
                //                   join dv in db.dvDienDieuHoas on new { hd.TableName, hd.LinkID } equals new { TableName = "dvDienDieuHoa", LinkID = (int?)dv.ID }
                //                   where hd.MaTN == _MaTN & hd.MaKH == _MaKH & hd.NgayTT.Value.Month == _Thang & hd.NgayTT.Value.Year == _Nam & hd.ConNo.GetValueOrDefault() > 0
                //                   select new
                //                   {
                //                       dv.NgayCT,
                //                       dv.SoFCU,
                //                       dv.SoGio,
                //                       DonGia = dv.DonGia * dv.TyGia,
                //                       ThanhTien = hd.ConNo,
                //                       dv.DienGiai
                //                   }).ToList();
                this.DataSource = (from hd in db.dvHoaDons
                              join dv in db.dvDienDHs on new { hd.TableName, hd.LinkID } equals new { TableName = "dvDienDH", LinkID = (int?)dv.ID }
                              join dm in db.dvDienDH_DinhMucs on dv.MaMB equals dm.MaMB
                              join mb in db.mbMatBangs on dv.MaMB equals mb.MaMB
                              where hd.MaTN == _MaTN & hd.MaKH == _MaKH & hd.NgayTT.Value.Month == _Thang & hd.NgayTT.Value.Year == _Nam & hd.ConNo.GetValueOrDefault() > 0 & mb.MaLMB == 277 & hd.IsDuyet == true
                              select new
                              {
                                  dv.NgayTT,
                                  dv.HeSo,
                                  dv.SoTieuThu,
                                  ThanhTien = hd.ConNo,
                                  //DienGiai = "Từ ngày " + dv.TuNgay.Value.Day.ToString().PadLeft(2, '0') + "/" + dv.TuNgay.Value.Month.ToString().PadLeft(2, '0') + "/" + dv.TuNgay.Value.Year + " đến ngày " + dv.DenNgay.Value.Day.ToString().PadLeft(2, '0') + "/" + dv.DenNgay.Value.Month.ToString().PadLeft(2, '0') + "/" + dv.DenNgay.Value.Year,
                                  dm.DonGia,
                                  TienDien = db.dvDienDH_ChiTiets.Where(p => p.MaDien == dv.ID).Sum(p => p.ThanhTien).GetValueOrDefault(),
                                  TienVAT = db.dvDienDH_ChiTiets.Where(p => p.MaDien == dv.ID).Sum(p => p.ThanhTien).GetValueOrDefault() * 10 / 100,
                                  //hd.NgayTT,
                                  hd.ID,
                                  hd.TuNgay,
                                  hd.DenNgay,
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
