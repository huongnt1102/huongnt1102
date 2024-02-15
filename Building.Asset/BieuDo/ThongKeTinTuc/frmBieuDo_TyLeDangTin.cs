﻿using System;
using System.Linq;
using System.Windows.Forms;
using Library;
using DevExpress.XtraCharts;
using System.Data.Linq.SqlClient;
using System.Drawing;

namespace Building.Asset.BieuDo
{
    public partial class frmBieuDo_TyLeDangTin : Form
    {
        public frmBieuDo_TyLeDangTin()
        {
            InitializeComponent();
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

                    var ltSource = (from news in db.app_News
                        join tower in db.tnToaNhas on news.TowerId equals tower.MaTN
                        where //news.TowerId == maTn & //ltToaNha.Contains(news.TowerId.ToString()) &
                              SqlMethods.DateDiffDay(tuNgay, news.DateCreate) >= 0 &
                              SqlMethods.DateDiffDay(news.DateCreate, denNgay) >= 0
                        group news by new
                        {
                            tower.MaTN,
                            tower.TenTN,
                            //Thang = news.DateCreate.Value.Month + "/" + news.DateCreate.Value.Year
                        }
                        into g
                        select new
                        {
                            g.Key.TenTN, Count = g.Count()
                        }).ToList();

                    gridControl1.DataSource = ltSource;


                    Series series1 = new Series("", ViewType.Pie);

                    foreach (var i in ltSource)
                    {
                        series1.Points.Add(new SeriesPoint(i.TenTN, i.Count));
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

                    myView.RuntimeExploding = true;

                    pieChart.Dock = DockStyle.Fill;
                    //this.Controls.Add(pieChart);

                    var strTitle = string.Format("TỶ LỆ ĐĂNG TIN TỪ {0:MM/yyyy} ĐẾN {1:MM/yyyy}", tuNgay, denNgay);
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
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Library.Common.User, printBarManager1);
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
