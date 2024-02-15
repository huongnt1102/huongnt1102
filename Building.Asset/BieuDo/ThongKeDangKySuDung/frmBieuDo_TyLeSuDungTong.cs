using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Library;
using DevExpress.XtraCharts;
using System.Data.Linq.SqlClient;
using System.Drawing;

namespace Building.Asset.BieuDo
{
    public partial class frmBieuDo_TyLeSuDungTong : Form
    {
        public frmBieuDo_TyLeSuDungTong()
        {
            InitializeComponent();
        }

        public class TyLeTong
        {
            public int DangKy { get; set; }
            public int SuDung { get; set; }
        }

        private void LoadData()
        {
            var wait = DialogBox.WaitingForm();
            try
            {
                //var maTn = (byte) itemToaNha.EditValue;
                var tuNgay = (DateTime?) itemTuNgay.EditValue;
                var denNgay = (DateTime?) itemDenNgay.EditValue;

                //var strToaNha = (itemTN.EditValue ?? "").ToString().TrimEnd(',').TrimStart(',').Replace(" ", "");
                //var ltToaNha = strToaNha.Split(',');

                pieChart.Series.Clear();

                using (var db = new MasterDataContext())
                {
                    //var tenTn = db.tnToaNhas.First(_ => _.MaTN == maTn).TenTN;

                    var data = (from news in db.app_Residents
                        join rt in db.app_ResidentTowers on news.Id equals rt.ResidentId
                        join tower in db.tnToaNhas on rt.TowerId equals tower.MaTN
                        where //news.TowerId == maTn & //ltToaNha.Contains(news.TowerId.ToString()) &
                              SqlMethods.DateDiffDay(tuNgay, news.DateOfCreate) >= 0 &
                              SqlMethods.DateDiffDay(news.DateOfCreate, denNgay) >= 0 & news.IsResident==true
                             
                        select new
                        {
                            news.DateOfCreate,news.Id,Login=news.IsLogin!=null?1:0
                        }).ToList();
                    List<TyLeTong> ltSource = new List<TyLeTong>();
                    ltSource.Add(new TyLeTong
                        {DangKy = data.Select(_ => _.Id).Count(), SuDung = data.Select(_ => _.Login).Sum()});

                    gridControl1.DataSource = ltSource;


                    Series series1 = new Series("", ViewType.Pie);

                    foreach (var i in ltSource)
                    {
                        series1.Points.Add(new SeriesPoint("Đăng ký", i.DangKy));
                        series1.Points.Add(new SeriesPoint("Sử dụng", i.SuDung));
                    }

                    pieChart.Series.Add(series1);

                    ((PieSeriesLabel)series1.Label).PointOptions.Pattern = "{A}: {V}";
                    ((PieSeriesLabel)series1.Label).Position = PieSeriesLabelPosition.TwoColumns;
                    ((PieSeriesLabel)series1.Label).ResolveOverlappingMode = ResolveOverlappingMode.Default;
                    ((PieSeriesLabel)series1.Label).PointOptions.ValueNumericOptions.Format = NumericFormat.Percent;
                    ((PieSeriesLabel)series1.Label).PointOptions.ValueNumericOptions.Precision = 0;

                    PieSeriesView myView = (PieSeriesView)series1.View;

                    myView.Titles.Add(new SeriesTitle());
                    myView.Titles[0].Text = series1.Name;

                    myView.ExplodedPointsFilters.Add(new SeriesPointFilter(SeriesPointKey.Value_1,
                        DataFilterCondition.GreaterThanOrEqual, 9));
                    myView.ExplodedPointsFilters.Add(new SeriesPointFilter(SeriesPointKey.Argument,
                        DataFilterCondition.NotEqual, "Others"));
                    myView.ExplodeMode = PieExplodeMode.UseFilters;
                    myView.ExplodedDistancePercentage = 4;
                    myView.RuntimeExploding = true;

                    pieChart.Dock = DockStyle.Fill;
                    //this.Controls.Add(pieChart);

                    var strTitle = string.Format("THỐNG KÊ TỔNG \n TỪ {0:MM/yyyy} ĐẾN {1:MM/yyyy}", tuNgay, denNgay);
                    if (pieChart.Titles.Count == 0)
                    {

                        ChartTitle chartTitle1 = new ChartTitle();
                        chartTitle1.Text = strTitle;
                        chartTitle1.WordWrap = true;
                        pieChart.Titles.Add(chartTitle1);
                        pieChart.Legend.Visible = true;

                    }
                    else
                    {
                        pieChart.Titles[0].Text = strTitle;
                    }
                }
            }
            catch
            {
            }
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
            //cmbToaNha.DataSource = db.tnToaNhas;

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
            pieChart.ShowPrintPreview();
        }
    }
}
