using System;
using System.Data.Linq.SqlClient;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraCharts;
using DevExpress.XtraEditors;
using Library;

namespace Building.BieuDoMain
{
    public partial class CtlSoCanBanGiaoKhachHang : DevExpress.XtraEditors.XtraUserControl
    {
        public CtlSoCanBanGiaoKhachHang()
        {
            InitializeComponent();
        }

        private void CtlSoCanBanGiaoKhachHang_Load(object sender, EventArgs e)
        {
            if (Common.User == null) return;
            chkBuilding.DataSource = Common.TowerList;
            itemBuilding.EditValue = Common.User.MaTN;

            var objKbc = new KyBaoCao();
            foreach (var item in objKbc.Source) cmbKbc.Items.Add(item);
            itemKyBaoCao.EditValue= objKbc.Source[3];

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

                var strToaNha = (itemBuilding.EditValue ?? "").ToString().TrimEnd(',').TrimStart(',').Replace(" ", "");
                var ltToaNha = strToaNha.Split(',');
                if (strToaNha == "") return;
                var tuNgay = (DateTime?) itemTuNgay.EditValue;
                var denNgay = (DateTime?) itemDenNgay.EditValue;
                using (var db = new MasterDataContext())
                {
                    var status = (from p in db.ho_ScheduleApartments where ltToaNha.Contains(p.BuildingId.ToString()) & SqlMethods.DateDiffDay(tuNgay, p.DateHandoverFrom) >= 0 & SqlMethods.DateDiffDay(p.DateHandoverFrom, denNgay) >= 0 & p.IsLocal == false group new { p } by new { p.StatusId, p.StatusName } into g select new { g.Key.StatusId, g.Key.StatusName, Count = g.Count() }).ToList();

                    chartControl1.DataSource = null;
                    chartControl1.Series.Clear();
                    chartControl1.RefreshData();

                    Series series1 = new Series("Trạng thái", ViewType.Pie);
                    series1.Points.Clear();

                    var tong = (double) status.Sum(_ => _.Count);

                    //series1.Points.Add(new SeriesPoint("Tổng", new double[] {tong}));

                    foreach (var item in status)
                    {
                        var point = new SeriesPoint();
                        point.Argument = item.StatusName;
                        point.Tag = item.StatusName;
                        //point.Values = new double[] { Convert.ToDouble(item.Count) / tong };
                        point.Values = new double[] { Convert.ToDouble(item.Count)};
                        series1.Points.Add(point);
                    }

                    //series1.LegendPointOptions.PointView = PointView.Argument;
                    series1.PointOptions.ValueNumericOptions.Format = NumericFormat.Number;
                    series1.PointOptions.ValueNumericOptions.Precision = 2;
                    series1.LegendTextPattern = "{A}: {V:n0}";
                    //((PieSeriesLabel)series1.Label).PointOptions.Pattern = "{A}: {V}";
                    ((PieSeriesLabel) series1.Label).Position = PieSeriesLabelPosition.TwoColumns;
                    ((PieSeriesLabel) series1.Label).ResolveOverlappingMode = ResolveOverlappingMode.Default;
                    ((PieSeriesLabel) series1.Label).PointOptions.ValueNumericOptions.Format = NumericFormat.Percent;
                    ((PieSeriesLabel) series1.Label).PointOptions.ValueNumericOptions.Precision = 0;

                    PieSeriesLabel label = (PieSeriesLabel) series1.Label;
                    label.TextPattern = "{A}: {VP:P0}";

                    PieSeriesView myView = (PieSeriesView) series1.View;

                    myView.ExplodedPointsFilters.Add(new SeriesPointFilter(SeriesPointKey.Argument,
                        DataFilterCondition.NotEqual, "MinValue"));
                    myView.ExplodeMode = PieExplodeMode.UseFilters;
                    myView.ExplodedDistancePercentage = 3;
                    myView.RuntimeExploding = true;

                    chartControl1.Series.Add(series1);
                    chartControl1.Dock = DockStyle.Fill;


                }
            }
            catch (Exception e)
            {
                //
            }
        }

        private void ItemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void cmbKbc_EditValueChanged(object sender, EventArgs e)
        {
            SetDate(((ComboBoxEdit)sender).SelectedIndex);
        }
    }
}
