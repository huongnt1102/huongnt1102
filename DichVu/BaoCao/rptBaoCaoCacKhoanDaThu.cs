using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
using Library;

namespace DichVu.BaoCao
{
    public partial class rptBaoCaoCacKhoanDaThu : DevExpress.XtraReports.UI.XtraReport
    {
        public rptBaoCaoCacKhoanDaThu(byte MaTN, int month, int year)
        {
            InitializeComponent();

            var db = new Library.MasterDataContext();
            var d2 = new DateTime(year, month, 1).AddMonths(1).AddDays(-1);//31/10
            var d1 = d2.AddYears(-1).AddDays(1);

            #region "   CongTyQuanLy"
            xrNgay.Text = string.Format("Hà Nội, Ngày  {0:dd}  tháng  {0:MM}  năm  {0:yyyy}", DateTime.Now);

            var objTN = db.tnToaNhas.FirstOrDefault(p => p.MaTN == MaTN);
            cCongTy.Text = objTN.CongTyQuanLy;
            lbDiaChi.Text = string.Format("Dự án {0}", objTN.TenTN.ToUpper());//objTN.DiaChiCongTy;
            //lbMST.Text = lbMST1.Text = objTN.MaSoThue;
            #endregion


            #region "   Binding"
            lbTieuDe.Text = String.Format(lbTieuDe.Text, month, year);
            if (month == 12)
            {
                xrTableCell2014.Text = year + "";
                xrTableCell2015.Visible = false;
            }
            else
            {
                xrTableCell2014.WidthF = xrT1.WidthF * (13 - d1.Month);
                xrTableCell2014.Text = (year - 1) + "";
                xrTableCell2015.Text = year + "";
            }

            cellSTT.DataBindings.Add(new XRBinding("Text", null, "STT2"));
            cellPhiChungCu.DataBindings.Add(new XRBinding("Text", null, "TenLDV"));

            #region "   T1->T12"
            T1.Text = "T" + (d1.Month + 0 > 12 ? d1.Month + 0 - 12 : d1.Month + 0);
            T2.Text = "T" + (d1.Month + 1 > 12 ? d1.Month + 1 - 12 : d1.Month + 1);
            T3.Text = "T" + (d1.Month + 2 > 12 ? d1.Month + 2 - 12 : d1.Month + 2);
            T4.Text = "T" + (d1.Month + 3 > 12 ? d1.Month + 3 - 12 : d1.Month + 3);
            T5.Text = "T" + (d1.Month + 4 > 12 ? d1.Month + 4 - 12 : d1.Month + 4);
            T6.Text = "T" + (d1.Month + 5 > 12 ? d1.Month + 5 - 12 : d1.Month + 5);

            T7.Text = "T" + (d1.Month + 6 > 12 ? d1.Month + 6 - 12 : d1.Month + 6);
            T8.Text = "T" + (d1.Month + 7 > 12 ? d1.Month + 7 - 12 : d1.Month + 7);
            T9.Text = "T" + (d1.Month + 8 > 12 ? d1.Month + 8 - 12 : d1.Month + 8);
            T10.Text = "T" + (d1.Month + 9 > 12 ? d1.Month + 9 - 12 : d1.Month + 9);
            T11.Text = "T" + (d1.Month + 10 > 12 ? d1.Month + 10 - 12 : d1.Month + 10);
            T12.Text = "T" + (d1.Month + 11 > 12 ? d1.Month + 11 - 12 : d1.Month + 11);
            #endregion


            cellTongCong.DataBindings.Add(new XRBinding("Text", null, "DaThu", "{0:#,0.##}"));
            #region "   cellT1 -> cellT12"
            cellT1.DataBindings.Add(new XRBinding("Text", null, "T1", "{0:#,0.##}"));
            cellT2.DataBindings.Add(new XRBinding("Text", null, "T2", "{0:#,0.##}"));
            cellT3.DataBindings.Add(new XRBinding("Text", null, "T3", "{0:#,0.##}"));
            cellT4.DataBindings.Add(new XRBinding("Text", null, "T4", "{0:#,0.##}"));
            cellT5.DataBindings.Add(new XRBinding("Text", null, "T5", "{0:#,0.##}"));
            cellT6.DataBindings.Add(new XRBinding("Text", null, "T6", "{0:#,0.##}"));
            cellT7.DataBindings.Add(new XRBinding("Text", null, "T7", "{0:#,0.##}"));
            cellT8.DataBindings.Add(new XRBinding("Text", null, "T8", "{0:#,0.##}"));
            cellT9.DataBindings.Add(new XRBinding("Text", null, "T9", "{0:#,0.##}"));
            cellT10.DataBindings.Add(new XRBinding("Text", null, "T10", "{0:#,0.##}"));
            cellT11.DataBindings.Add(new XRBinding("Text", null, "T11", "{0:#,0.##}"));
            cellT12.DataBindings.Add(new XRBinding("Text", null, "T12", "{0:#,0.##}"));
            #endregion

            #region "   Summary"
            xrT1.DataBindings.Add(new XRBinding("Text", null, "T1", "{0:#,0.##}"));
            xrT2.DataBindings.Add(new XRBinding("Text", null, "T2", "{0:#,0.##}"));
            xrT3.DataBindings.Add(new XRBinding("Text", null, "T3", "{0:#,0.##}"));
            xrT4.DataBindings.Add(new XRBinding("Text", null, "T4", "{0:#,0.##}"));
            xrT5.DataBindings.Add(new XRBinding("Text", null, "T5", "{0:#,0.##}"));
            xrT6.DataBindings.Add(new XRBinding("Text", null, "T6", "{0:#,0.##}"));
            xrT7.DataBindings.Add(new XRBinding("Text", null, "T7", "{0:#,0.##}"));
            xrT8.DataBindings.Add(new XRBinding("Text", null, "T8", "{0:#,0.##}"));
            xrT9.DataBindings.Add(new XRBinding("Text", null, "T9", "{0:#,0.##}"));
            xrT10.DataBindings.Add(new XRBinding("Text", null, "T10", "{0:#,0.##}"));
            xrT11.DataBindings.Add(new XRBinding("Text", null, "T11", "{0:#,0.##}"));
            xrT12.DataBindings.Add(new XRBinding("Text", null, "T12", "{0:#,0.##}"));

            xrT1.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,0.##}");  
            xrT2.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,0.##}");  
            xrT3.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,0.##}");  
            xrT4.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,0.##}");  
            xrT5.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,0.##}");  
            xrT6.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,0.##}");  
            xrT7.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,0.##}");  
            xrT8.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,0.##}");  
            xrT9.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,0.##}");  
            xrT10.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,0.##}");  
            xrT11.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,0.##}");  
            xrT12.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,0.##}");  

            cellTongCongSum.DataBindings.Add(new XRBinding("Text", null, "DaThu", "{0:#,0.##}"));
            cellTongCongSum.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,0.##}");  
            #endregion

            #endregion

            
            #region "   var data"
            var data = (from h in db.dvHoaDons
                        join l in db.dvLoaiDichVus on h.MaLDV equals l.ID
                        group h by new { h.MaTN, l.TenLDV, l.ID } into g
                        where g.Key.MaTN == MaTN
                            & g.First().NgayTT.Value >= d1
                            & g.First().NgayTT.Value <= d2
                            & g.Sum(h => h.DaThu) > 0

                        select new
                        {
                            g.Key.ID,
                            g.Key.TenLDV,

                            DaThu = g.Sum(h => h.DaThu),

                            T1 = g.Where(h => h.NgayTT.Value.Month == (d1.Month + 0 > 12 ? d1.Month + 0 - 12 : d1.Month + 0)).Sum(h => h.DaThu),
                            T2 = g.Where(h => h.NgayTT.Value.Month == (d1.Month + 1 > 12 ? d1.Month + 1 - 12 : d1.Month + 1)).Sum(h => h.DaThu),
                            T3 = g.Where(h => h.NgayTT.Value.Month == (d1.Month + 2 > 12 ? d1.Month + 2 - 12 : d1.Month + 2)).Sum(h => h.DaThu),
                            T4 = g.Where(h => h.NgayTT.Value.Month == (d1.Month + 3 > 12 ? d1.Month + 3 - 12 : d1.Month + 3)).Sum(h => h.DaThu),
                            T5 = g.Where(h => h.NgayTT.Value.Month == (d1.Month + 4 > 12 ? d1.Month + 4 - 12 : d1.Month + 4)).Sum(h => h.DaThu),
                            T6 = g.Where(h => h.NgayTT.Value.Month == (d1.Month + 5 > 12 ? d1.Month + 5 - 12 : d1.Month + 5)).Sum(h => h.DaThu),
                            T7 = g.Where(h => h.NgayTT.Value.Month == (d1.Month + 6 > 12 ? d1.Month + 6 - 12 : d1.Month + 6)).Sum(h => h.DaThu),
                            T8 = g.Where(h => h.NgayTT.Value.Month == (d1.Month + 7 > 12 ? d1.Month + 7 - 12 : d1.Month + 7)).Sum(h => h.DaThu),
                            T9 = g.Where(h => h.NgayTT.Value.Month == (d1.Month + 8 > 12 ? d1.Month + 8 - 12 : d1.Month + 8)).Sum(h => h.DaThu),
                            T10 = g.Where(h => h.NgayTT.Value.Month == (d1.Month + 9 > 12 ? d1.Month + 9 - 12 : d1.Month + 9)).Sum(h => h.DaThu),
                            T11 = g.Where(h => h.NgayTT.Value.Month == (d1.Month + 10 > 12 ? d1.Month + 10 - 12 : d1.Month + 10)).Sum(h => h.DaThu),
                            T12 = g.Where(h => h.NgayTT.Value.Month == (d1.Month + 11 > 12 ? d1.Month + 11 - 12 : d1.Month + 11)).Sum(h => h.DaThu),

                        }).ToList();
            #endregion

            #region "    this.DataSource = data"
            this.DataSource = data.AsEnumerable().Select((p, index) => new
            {
                p.TenLDV,
                p.DaThu,
                p.T1,
                p.T2,
                p.T3,
                p.T4,
                p.T5,
                p.T6,
                p.T7,
                p.T8,
                p.T9,
                p.T10,
                p.T11,
                p.T12,
                STT2 = index + 1
            });
            #endregion





        }
    }
}
