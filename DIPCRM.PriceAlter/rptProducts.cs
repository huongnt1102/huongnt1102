using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
using Library;

namespace DIPCRM.PriceAlert
{
    public partial class rptProducts : DevExpress.XtraReports.UI.XtraReport
    {
        public rptProducts(int id)
        {
            InitializeComponent();

            cSTT.DataBindings.Add("Text", null, "STT");
            cMaSP.DataBindings.Add("Text", null, "MaSP");
            cTenSP.DataBindings.Add("Text", null, "TenSP");
            cDienGiai.DataBindings.Add("Text", null, "DienGiai");            
            cDVT.DataBindings.Add("Text", null, "TenDVT");
            cSoLuong.DataBindings.Add("Text", null, "SoLuong", "{0:#,0.##}");
            cDonGia.DataBindings.Add("Text", null, "DonGia", "{0:#,0.##}");
            cThanhTien.DataBindings.Add("Text", null, "ThanhTien", "{0:#,0.##}");
            cThueVAT.DataBindings.Add("Text", null, "ThueGTGT", "{0:#,0.##} %");

            cSumThanhTien.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,0.##}");
            cSumThanhTien.DataBindings.Add("Text", null, "ThanhTien", "{0:#,0.##}");
            cSumTienVAT.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,0.##}");
            cSumTienVAT.DataBindings.Add("Text", null, "TienGTGT", "{0:#,0.##}");
            cSumSoTien.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,0.##}");
            cSumSoTien.DataBindings.Add("Text", null, "SoTien", "{0:#,0.##}");

            using (var db = new MasterDataContext())
            {
                //this.DataSource = (from ct in db.bgSanPhams
                //                   join sp in db.SanPhams on ct.MaSP equals sp.ID
                //                   join x in db.XuatXus on sp.MaXX equals x.MaXX into xl
                //                   from xx in xl.DefaultIfEmpty()
                //                   join d in db.DonViTinhs on sp.MaDVT equals d.MaDVT into dv
                //                   from dvt in dv.DefaultIfEmpty()
                //                   where ct.MaBG == id
                //                   select new { sp.MaSP, sp.TenSP, sp.DienGiai, xx.TenXX, dvt.TenDVT, ct.SoLuong, ct.DonGia, ct.ThueGTGT, ct.ThanhTien })
                //                   .AsEnumerable()
                //                   .Select((p, index) => new
                //                   {
                //                       STT = index + 1,
                //                       p.MaSP,
                //                       p.TenSP,
                //                       p.DienGiai,
                //                       p.TenXX,
                //                       p.TenDVT,
                //                       p.SoLuong,
                //                       p.DonGia,
                //                       p.ThanhTien,
                //                       ThueGTGT = p.ThueGTGT * 100,
                //                       TienGTGT = p.ThanhTien * p.ThueGTGT,
                //                       SoTien = p.ThanhTien * (1 + p.ThueGTGT)
                //                   })
                //                   .ToList();
            }
        }
        
        private void cDienGiai_HtmlItemCreated(object sender, HtmlEventArgs e)
        {
            DevExpress.XtraPrinting.HtmlExport.Controls.DXHtmlTableRow parentRow = (DevExpress.XtraPrinting.HtmlExport.Controls.DXHtmlTableRow)e.ContentCell.Parent;
            parentRow.Controls.Clear();

            DevExpress.XtraPrinting.HtmlExport.Controls.DXHtmlTableCell cell = new DevExpress.XtraPrinting.HtmlExport.Controls.DXHtmlTableCell();
            cell.InnerHtml = GetCurrentColumnValue("DienGiai").ToString();

            parentRow.Cells.Add(cell);
        }
    }
}
