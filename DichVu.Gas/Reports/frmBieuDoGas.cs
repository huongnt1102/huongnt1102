using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraCharts;
using Library;
using System.Linq;
using System.Data.Linq.SqlClient;
using DevExpress.XtraReports.UI;


namespace DichVu.Gas.Reports
{
    public partial class frmBieuDoGas : DevExpress.XtraEditors.XtraForm
    {
        public frmBieuDoGas()
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
                    var ltGas = (from n in db.dvGas
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
                    foreach (var l in ltGas)
                    {
                        series1.Points.Add(new SeriesPoint(l.Thang.ToString(), l.SoTieuThu));

                    }

                    // Add both series to the chart.
                    chartControl1.Series.AddRange(new Series[] { series1, series2 });


                    /// Access labels of the first series.
                    ((BarSeriesLabel)series1.Label).Visible = true;
                    ((BarSeriesLabel)series1.Label).ResolveOverlappingMode = ResolveOverlappingMode.Default;

                    ((PointSeriesLabel)series2.Label).Visible = false;
                    ((PointSeriesLabel)series2.Label).ResolveOverlappingMode = ResolveOverlappingMode.Default;

                    // Customize the view-type-specific properties of the series.
                    BarSeriesView myView = (BarSeriesView)series1.View;
                    myView.Transparency = 50;

                    //Diagramp 
                    XYDiagram diagram = (XYDiagram)chartControl1.Diagram;
                    diagram.AxisY.Title.Text = "Mức tiêu thụ";
                    diagram.AxisY.Title.Visible = true;
                    diagram.AxisX.Visible = true;
                    diagram.AxisY.Title.Font = new Font("Tahoma", 18);
                    ((BarSeriesLabel)series1.Label).PointOptions.Pattern = "{V}";


                }
            }
            catch { }
            finally
            {
                wait.Close();
            }
        }
        private void itemRefresh_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            BaoCao_Nap();
        }

        private void frmBieuDoGas_Load(object sender, EventArgs e)
        {
            lookToaNha.DataSource = Common.TowerList;
            itemToaNha.EditValue = Common.User.MaTN;
            itemTuNgay.EditValue = DateTime.Now;
            itemDenNgay.EditValue = DateTime.Now;
            BaoCao_Nap();
        }

        private void itemToaNha_EditValueChanged(object sender, EventArgs e)
        {
            using (var db = new Library.MasterDataContext())
            {
                var ltMatBang = db.mbMatBangs.OrderByDescending(p => p.MaMB).Where(p => p.MaTN == Convert.ToByte(itemToaNha.EditValue)).Select(p => new { p.MaMB, p.MaSoMB }).ToList();
                lookMatBang.DataSource = ltMatBang;
            }
            itemMatBang.EditValue = null;
        }

        private void lookMatBang_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                itemMatBang.EditValue = null;
            }
        }

        private void itemPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            chartControl1.ShowPrintPreview();
        }

    }
}