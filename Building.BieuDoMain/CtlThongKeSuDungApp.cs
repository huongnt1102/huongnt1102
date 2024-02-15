using System;
using System.Collections.Generic;
using System.Data.Linq.SqlClient;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraCharts;
using DevExpress.XtraEditors;
using Library;

namespace Building.BieuDoMain
{
    public partial class CtlThongKeSuDungApp : DevExpress.XtraEditors.XtraUserControl
    {
        public CtlThongKeSuDungApp()
        {
            InitializeComponent();
        }
        public class TyLeTong
        {
            public int DangKy { get; set; }
            public int SuDung { get; set; }
        }

        private void CtlThongKeSuDungApp_Load(object sender, EventArgs e)
        {
            if (Common.User == null) return;
            ckCbxToaNha.DataSource = Common.TowerList;
            itemToaNha.EditValue = Common.User.MaTN;
            var objKbc = new KyBaoCao();
            foreach (var item in objKbc.Source) cbxKbc.Items.Add(item);
            itemKyBaoCao.EditValue = objKbc.Source[3];
            SetDate(3);
            LoadData();
        }

        private void SetDate(int index)
        {
            var objKbc = new KyBaoCao { Index = index };
            objKbc.SetToDate();
            itemTuNgay.EditValue = objKbc.DateFrom;
            itemDenNgay.EditValue = objKbc.DateTo;
        }

        private void LoadData()
        {
            try
            {
                Library.PhanQuyenBieuDo.SaveControl(GetType().Namespace + "." + Name, chartControl1.Titles[0].Lines[0].ToString(), GetType().Namespace + ".dll");

                var strToaNha = (itemToaNha.EditValue ?? "").ToString().TrimEnd(',').TrimStart(',').Replace(" ", "");
                var ltToaNha = strToaNha.Split(',');
                if (strToaNha == "") return;
                var tuNgay = (DateTime?)itemTuNgay.EditValue;
                var denNgay = (DateTime?)itemDenNgay.EditValue;
                using (var db = new MasterDataContext())
                {
                    var data = (from news in db.app_Residents
                        join rt in db.app_ResidentTowers on news.Id equals rt.ResidentId
                        join tower in db.tnToaNhas on rt.TowerId equals tower.MaTN
                        where ltToaNha.Contains(tower.MaTN.ToString()) &
                            SqlMethods.DateDiffDay(tuNgay, news.DateOfCreate) >= 0 &
                            SqlMethods.DateDiffDay(news.DateOfCreate, denNgay) >= 0 & news.IsResident == true

                        select new
                        {
                            news.DateOfCreate,
                            news.Id,
                            Login = news.IsLogin != null ? 1 : 0
                        }).ToList();
                    List<TyLeTong> ltSource = new List<TyLeTong>();
                    ltSource.Add(new TyLeTong
                        { DangKy = data.Select(_ => _.Id).Count(), SuDung = data.Select(_ => _.Login).Sum() });
                    chartControl1.DataSource = null;
                    chartControl1.Series.Clear();
                    chartControl1.RefreshData();

                    Series series1 = new Series("Thống kê sử dụng App", ViewType.Pie);
                    series1.Points.Clear();

                    foreach (var i in ltSource)
                    {
                        series1.Points.Add(new SeriesPoint("Đăng ký", i.DangKy));
                        series1.Points.Add(new SeriesPoint("Sử dụng", i.SuDung));
                    }

                    series1.PointOptions.ValueNumericOptions.Format = NumericFormat.Number;
                    series1.PointOptions.ValueNumericOptions.Precision = 2;
                    series1.LegendTextPattern = "{A}: {V:n0}";
                    //((PieSeriesLabel)series1.Label).PointOptions.Pattern = "{A}: {V}";
                    ((PieSeriesLabel)series1.Label).Position = PieSeriesLabelPosition.TwoColumns;
                    ((PieSeriesLabel)series1.Label).ResolveOverlappingMode = ResolveOverlappingMode.Default;
                    ((PieSeriesLabel)series1.Label).PointOptions.ValueNumericOptions.Format = NumericFormat.Percent;
                    ((PieSeriesLabel)series1.Label).PointOptions.ValueNumericOptions.Precision = 0;

                    PieSeriesLabel label = (PieSeriesLabel)series1.Label;
                    label.TextPattern = "{A}: {VP:P0}";

                    PieSeriesView myView = (PieSeriesView)series1.View;

                    myView.ExplodedPointsFilters.Add(new SeriesPointFilter(SeriesPointKey.Value_1,
                        DataFilterCondition.GreaterThanOrEqual, 9));
                    myView.ExplodedPointsFilters.Add(new SeriesPointFilter(SeriesPointKey.Argument,
                        DataFilterCondition.NotEqual, "Others"));
                    myView.ExplodeMode = PieExplodeMode.UseFilters;

                    myView.RuntimeExploding = true;

                    chartControl1.Series.Add(series1);
                    chartControl1.Dock = DockStyle.Fill;
                }
            }
            catch { }
        }

        private void ItemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void cbxKbc_EditValueChanged(object sender, EventArgs e)
        {
            SetDate(((ComboBoxEdit)sender).SelectedIndex);
        }
    }
}
