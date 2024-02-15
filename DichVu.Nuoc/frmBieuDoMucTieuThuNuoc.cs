using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Library;
using DevExpress.XtraCharts;

namespace DichVu.Nuoc
{
    public partial class frmBieuDoMucTieuThuNuoc : DevExpress.XtraEditors.XtraForm
    {

        public frmBieuDoMucTieuThuNuoc()
        {
            InitializeComponent();
        }


        private void BaoCao_Nap()
        {
            var wait = DialogBox.WaitingForm();
            try
            {
                var mamb = Convert.ToInt32(itemMatBang.EditValue);
                var tungay = (DateTime?)itemTuNgay.EditValue;
                var denngay = (DateTime?)itemDenNgay.EditValue;


               chartControl1.Series.Clear();
               

                using (var db = new MasterDataContext())
                {
                    var ltSource = (from n in db.dvNuocs
                                    where n.MaMB == mamb && n.NgayTB >= tungay && n.NgayTB <= denngay
                                    group n by new { n.NgayTB.Value.Year, n.NgayTB.Value.Month } into gr
                                    select new
                                    {
                                        Thang = string.Format("Tháng {0}/{1}", gr.Key.Month, gr.Key.Year),
                                        SoTieuThu =gr.Sum(p => p.SoTieuThu)
                                    }).ToList();

                    Series series1 = new Series("", ViewType.Bar);
                    Series series2 = new Series("", ViewType.Line);
                    
                    // Add points to the series.
                    foreach (var l in ltSource)
                    {
                        series1.Points.Add(new SeriesPoint(l.Thang.ToString(), l.SoTieuThu));
                        //series2.Points.Add(new SeriesPoint(l.Thang.ToString(), l.SoTieuThu));
                    }

                    // Add both series to the chart.
                    chartControl1.Series.AddRange(new Series[] { series1, series2 });
                   

                    /// Access labels of the first series.
                    ((BarSeriesLabel)series1.Label).Visible = true;
                    ((BarSeriesLabel)series1.Label).ResolveOverlappingMode = ResolveOverlappingMode.Default;
                    ((BarSeriesLabel)series1.Label).PointOptions.Pattern = "{V} m3";

                    ((PointSeriesLabel)series2.Label).Visible = false;
                    ((PointSeriesLabel)series2.Label).ResolveOverlappingMode = ResolveOverlappingMode.Default;

                    // Customize the view-type-specific properties of the series.
                    BarSeriesView myView = (BarSeriesView)series1.View;
                    //myView.Transparency = 50;

                    //Diagramp 
                    XYDiagram diagram = (XYDiagram)chartControl1.Diagram;
                    diagram.AxisY.Title.Text = "Mức tiêu thụ";
                    diagram.AxisY.Title.Visible = true;
                    diagram.AxisX.Visible = true;
                    diagram.AxisY.Title.Font = new Font("Tahoma", 18);

                    // Add a title to the chart and hide the legend.
                    var strTitle = string.Format("BIỂU ĐỒ MỨC TIÊU THỤ NƯỚC TỪ {0} ĐẾN {1}", String.Format("{0:MM/yyyy}", tungay), String.Format("{0:MM/yyyy}", denngay)); //string.Format("MM/yyy", tungay), string.Format("MM/yyy", denngay));
                    if (chartControl1.Titles.Count == 0)
                    {

                        ChartTitle chartTitle1 = new ChartTitle();
                        chartTitle1.Text = strTitle;
                        chartTitle1.WordWrap = true;
                        chartControl1.Titles.Add(chartTitle1);
                        chartControl1.Legend.Visible = true;
                    }
                    else
                    {
                        chartControl1.Titles[0].Text = strTitle;
                        //chartControl1.Legends.Add("Legend1" + t.ToString());
                        //Chart1.Legends["Legend1" + t.ToString()].Enabled = false;
                        //// Chart1.Legends["Legend1" + t.ToString()].Docking = Docking.Bottom;
                        //Chart1.Legends["Legend1" + t.ToString()].Alignment = System.Drawing.StringAlignment.Center;
                        //Chart1.Legends["Legend1" + t.ToString()].DockedToChartArea = "ChartArea1" + t.ToString();


                        //chartControl1.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.RightOutside;
                        //chartControl1.Legend.AlignmentVertical = LegendAlignmentVertical.Top;
                        //chartControl1.Legend.Direction = LegendDirection.TopToBottom;
                    }

                    // Add the chart to the form.
                   // chartControl1.Series[0].PointOptions.ValueNumericOptions.Format = NumericFormat.Number;
                   // chartControl1.Series[0].PointOptions.ValueNumericOptions.Precision = 0;
                }
            }
            catch { }
            finally
            {
                wait.Close();
            }
        }


        private void frmBieuDoMucTieuThuNuoc_Load(object sender, EventArgs e)
        {
            repositoryItemLookUpToaNha.DataSource = Common.TowerList;
            itemTuNgay.EditValue = new DateTime(DateTime.Now.Year, 1, 1);
            itemDenNgay.EditValue = DateTime.Now;
            BaoCao_Nap();
        }


        private void barbtnNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            BaoCao_Nap();
        }

        private void chartControl1_Click(object sender, EventArgs e)
        {

        }

        private void barcboMatBang_EditValueChanged(object sender, EventArgs e)
        {
            
        }

        private void barEditItem1_EditValueChanged(object sender, EventArgs e)
        {
            var db = new MasterDataContext();
            lkMatBang.DataSource = db.mbMatBangs.Where(p => p.MaTN == Convert.ToByte(itemToaNha.EditValue)).Select(p => new { p.MaMB, p.MaSoMB });
        }

        private void itemToaNha_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var db = new MasterDataContext();
            lkMatBang.DataSource = db.mbMatBangs.Where(p => p.MaTN == Convert.ToByte(itemToaNha.EditValue)).Select(p => new { p.MaMB, p.MaSoMB });
        }
    }
}