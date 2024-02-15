using System;
using System.Linq;
using System.Windows.Forms;
using Library;
using DevExpress.XtraCharts;
using System.Data.Linq.SqlClient;

namespace Building.Asset.BieuDo
{
    public partial class frmBieuDo_TinTucToaNha : Form
    {
        public frmBieuDo_TinTucToaNha()
        {
            InitializeComponent();
        }

        private void LoadData()
        {
            var wait = DialogBox.WaitingForm();
            try
            {
                var _MaTN = (byte)itemToaNha.EditValue;
                var _TuNgay = (DateTime?)itemTuNgay .EditValue;
                var _DenNgay = (DateTime?)itemDenNgay.EditValue;

                var strToaNha = (itemTN.EditValue ?? "").ToString().TrimEnd(',').TrimStart(',').Replace(" ", "");
                var ltToaNha = strToaNha.Split(',');

                lineChart.Series.Clear();

                using (var db = new MasterDataContext())
                {
                    var ltSource = (from news in db.app_News
                        join tower in db.tnToaNhas on news.TowerId equals tower.MaTN
                        where ltToaNha.Contains(news.TowerId.ToString()) &
                              SqlMethods.DateDiffDay(_TuNgay, news.DateCreate) >= 0 &
                              SqlMethods.DateDiffDay(news.DateCreate, _DenNgay) >= 0
                        //group news by new
                        //{
                        //    tower.MaTN, tower.TenTN,
                        //    Thang = news.DateCreate.Value.Month + "/" + news.DateCreate.Value.Year
                        //}
                        //into g
                        select new
                        {
                            TenTN = tower.TenTN ?? "Tòa nhà khác",
                            Thang = news.DateCreate,
                            MaTN = tower.MaTN
                        }).ToList();

                    var listSeri = (from p in ltSource
                        group p by new {p.TenTN,p.MaTN}
                        into g
                        orderby g.Key.TenTN
                        select new
                        {
                            g.Key.TenTN, g.Key.MaTN
                        }).ToList();

                    gridControl1.DataSource = (from p in ltSource
                            group p by new
                            {
                                p.MaTN, p.TenTN, Thang = p.Thang.Value.Month + "/" + p.Thang.Value.Year,
                                p.Thang.Value.Year, p.Thang.Value.Month
                            }
                            into g
                            orderby g.Key.TenTN, g.Key.Year, g.Key.Month
                            select new
                            {
                                g.Key.TenTN, g.Key.Thang, Count = g.Count()
                            }
                        ).ToList();

                    foreach (var l in listSeri)
                    {
                        Series series = new Series(l.TenTN, ViewType.Line);

                        var listPoint = (from p in ltSource
                            where p.MaTN == l.MaTN
                            group p by new
                            {
                                Thang = p.Thang.Value.Month + "/" + p.Thang.Value.Year, p.Thang.Value.Year,
                                p.Thang.Value.Month
                            }
                            into g
                            orderby g.Key.Year, g.Key.Month
                            select new
                            {
                                g.Key.Thang, CountT = g.Count()
                            }).ToList();
                        foreach (var i in listPoint)
                        {
                            series.Points.Add(new SeriesPoint("T"+i.Thang, i.CountT));
                        }

                        lineChart.Series.Add(series);
                        //series.ArgumentScaleType = ScaleType.Numerical;
                        //((LineSeriesView) series.View).LineStyle.DashStyle = DashStyle.Dash;
                        //((LineSeriesView) series.View).LineMarkerOptions.Kind = MarkerKind.Triangle;
                    }

                    //((XYDiagram)lineChart.Diagram).EnableAxisXZooming = true;

                    //lineChart.Series.Add(series1);

                    //((PieSeriesLabel)series1.Label).PointOptions.Pattern = "{A}: {V}";
                    //((PieSeriesLabel)series1.Label).Position = PieSeriesLabelPosition.TwoColumns;
                    //((PieSeriesLabel)series1.Label).ResolveOverlappingMode = ResolveOverlappingMode.Default;
                    //((PieSeriesLabel) series1.Label).PointOptions.ValueNumericOptions.Format = NumericFormat.Percent;
                    //((PieSeriesLabel) series1.Label).PointOptions.ValueNumericOptions.Precision = 0;

                    //LineSeriesView myView = (LineSeriesView)series1.View;

                    //myView.Titles.Add(new SeriesTitle());
                    //myView.Titles[0].Text = series1.Name;

                    //myView.ExplodedPointsFilters.Add(new SeriesPointFilter(SeriesPointKey.Value_1,
                    //    DataFilterCondition.GreaterThanOrEqual, 9));
                    //myView.ExplodedPointsFilters.Add(new SeriesPointFilter(SeriesPointKey.Argument,
                    //    DataFilterCondition.NotEqual, "Others"));
                    //myView.ExplodeMode = PieExplodeMode.UseFilters;

                    //myView.RuntimeExploding = true;

                    lineChart.Dock = DockStyle.Fill;


                    var strTitle = string.Format("TIN TỨC CỦA CÁC BAN TỪ {0:MM/yyyy} ĐẾN {1:MM/yyyy}", _TuNgay, _DenNgay); 
                    if (lineChart.Titles.Count == 0)
                    {

                        ChartTitle chartTitle1 = new ChartTitle();
                        chartTitle1.Text = strTitle;
                        chartTitle1.WordWrap = true;
                        lineChart.Titles.Add(chartTitle1);
                        lineChart.Legend.Visible = true;
                        
                    }
                    else
                    {
                        lineChart.Titles[0].Text = strTitle;
                    }
                    //this.Controls.Add(lineChart);
                }
            }
            catch { }
            finally
            {
                wait.Close();
            }
        }

        private void frmBieuDoTieuThuDien_Load(object sender, EventArgs e)
        {
            lookToaNha.DataSource = Common.TowerList;
            itemToaNha.EditValue = Common.User.MaTN;
            var db = new MasterDataContext();

            //cmbToaNha.DataSource = Common.TowerList;
            cmbToaNha.DataSource = db.tnToaNhas;

            itemTuNgay.EditValue = new DateTime(DateTime.Now.Year, 1, 1);
            itemDenNgay.EditValue = DateTime.Now;
            LoadData();
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void itemToaNha_EditValueChanged(object sender, EventArgs e)
        {
            var db = new MasterDataContext();
            lookToaNha.DataSource = db.tnToaNhas;
        }

        private void lookMatBang_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                //itemMatBang.EditValue = null;
            }
        }

        private void itemPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            lineChart.ShowPrintPreview();
        }
    }
}
