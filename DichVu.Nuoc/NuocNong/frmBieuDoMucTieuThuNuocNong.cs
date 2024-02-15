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
using System.Data.Linq.SqlClient;

namespace DichVu.Nuoc.NuocNong
{
    public partial class frmBieuDoMucTieuThuNuocNong : DevExpress.XtraEditors.XtraForm
    {

        public frmBieuDoMucTieuThuNuocNong()
        {
            InitializeComponent();
        }


        private void BaoCao_Nap()
        {
            var wait = DialogBox.WaitingForm();
            try
            {
                var _MaTN = (byte)itemToaNha.EditValue;
                var _MaMB = (int?)itemMatBang.EditValue;
                var _TuNgay = (DateTime?)itemTuNgay.EditValue;
                var _DenNgay = (DateTime?)itemDenNgay.EditValue;


               chartControl1.Series.Clear();

                using (var db = new MasterDataContext())
                {
                    var ltSource = (from n in db.dvNuocNongs
                                    where n.MaTN == _MaTN & (n.MaMB == _MaMB | _MaMB == null) & SqlMethods.DateDiffMonth(_TuNgay, n.NgayTB) >= 0 & SqlMethods.DateDiffMonth(n.TuNgay, _DenNgay) >= 0
                                    group n by new { n.NgayTB.Value.Year, n.NgayTB.Value.Month } into gr
                                    select new
                                    {
                                        Thang = string.Format("Tháng {0}/{1}", gr.Key.Month, gr.Key.Year),
                                        SoTieuThu = gr.Sum(p => p.SoTieuThu)
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
                    var strTitle = string.Format("BIỂU ĐỒ MỨC TIÊU THỤ NƯỚC NÓNG TỪ {0:MM/yyyy} ĐẾN {1:MM/yyyy}", _TuNgay, _DenNgay); 
                    if (chartControl1.Titles.Count == 0)
                    {

                        ChartTitle chartTitle1 = new ChartTitle();
                        chartTitle1.Text = strTitle;
                        chartTitle1.WordWrap = true;
                        chartControl1.Titles.Add(chartTitle1);
                        chartControl1.Legend.Visible = false;
                    }
                    else
                    {
                        chartControl1.Titles[0].Text = strTitle;
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


        private void frmBieuDoMucTieuThuNuocNong_Load(object sender, EventArgs e)
        {
            lkToaNha.DataSource = Common.TowerList;
            itemToaNha.EditValue = Common.User.MaTN;
            itemTuNgay.EditValue = new DateTime(DateTime.Now.Year, 1, 1);
            itemDenNgay.EditValue = DateTime.Now;
            BaoCao_Nap();
        }

        private void barbtnNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            BaoCao_Nap();
        }

        private void itemPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            chartControl1.ShowPrintPreview();
        }

        private void itemToaNha_EditValueChanged(object sender, EventArgs e)
        {
            var db = new MasterDataContext();
            try
            {
                lkMatBang.DataSource = db.mbMatBangs.Where(p => p.MaTN == Convert.ToByte(itemToaNha.EditValue)).Select(p => new { p.MaMB, p.MaSoMB }).ToList();
                itemMatBang.EditValue = null;
            }
            catch { }
            finally
            {
                db.Dispose();
            }
        }

        private void lkMatBang_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                itemMatBang.EditValue = null;
            }
        }
    }
}