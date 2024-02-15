using System;
using System.Data.Linq.SqlClient;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraCharts;
using DevExpress.XtraEditors;
using Library;

namespace Building.BieuDoMain
{
    public partial class CtlSoLuongYeuCauHoanThanh : DevExpress.XtraEditors.XtraUserControl
    {
        public CtlSoLuongYeuCauHoanThanh()
        {
            InitializeComponent();
        }

        private void CtlSoLuongYeuCauHoanThanh_Load(object sender, EventArgs e)
        {
            if (Common.User == null) return;
            glkToaNha.DataSource = Common.TowerList;
            itemToaNha.EditValue = Library.Common.User.MaTN;

            var objKbc = new KyBaoCao();

            foreach (var v in objKbc.Source)
            {
                cbxKbc.Items.Add(v);
            }

            itemKbc.EditValue = objKbc.Source[3];
            SetDate(3);

            LoadData();
        }
        private void SetDate(int index)
        {
            var objKbc = new KyBaoCao()
            {
                Index = index
            };
            objKbc.SetToDate();
            itemTuNgay.EditValue = objKbc.DateFrom;
            itemDenNgay.EditValue = objKbc.DateTo;
        }

        private void LoadData()
        {
            try
            {
                Library.PhanQuyenBieuDo.SaveControl(GetType().Namespace + "." + Name, chartControl1.Titles[0].Lines[0].ToString(), GetType().Namespace + ".dll");

                var maTn = (byte) itemToaNha.EditValue;
                var tuNgay = (DateTime?) itemTuNgay.EditValue;
                var denNgay = (DateTime?) itemDenNgay.EditValue;
                using (var db = new MasterDataContext())
                {
                    var obj = (from p in db.tnycYeuCaus
                        join tt in db.tnycTrangThais on p.MaTT equals tt.MaTT
                        where p.MaTN == maTn
                              & SqlMethods.DateDiffDay(tuNgay, p.NgayYC.Value) >= 0
                              & SqlMethods.DateDiffDay(p.NgayYC.Value, denNgay) >= 0
                        group new {tt, p} by new{tt.TenTT,tt.MaTT}
                        into g
                        select new
                        {
                            TenTrangThai = g.Key.TenTT, SoLuong = g.Count(),MaTrangThai=g.Key.MaTT
                        }).ToList();

                    chartControl1.DataSource = null;
                    chartControl1.Series.Clear();
                    chartControl1.RefreshData();

                    Series series1 = new Series("A Pie Series", ViewType.Pie);
                    series1.Points.Clear();

                    var tong = (double)obj.Sum(_ => _.SoLuong) ;
                    var hoanThanh = (double)obj.Where(_ => _.MaTrangThai == 3||_.MaTrangThai==5).Sum(_ => _.SoLuong) ;

                    series1.Points.Add(new SeriesPoint("Tổng", new double[] {tong}));
                    series1.Points.Add(new SeriesPoint("Hoàn thành", new double[] {hoanThanh / tong}));

                    //var point1 = new SeriesPoint();
                    //point1.Argument = "Tổng";
                    //point1.Tag = "Tổng";
                    
                    //point1.Values = new double[]{tong};
                    //series1.Points.Add(point1);

                    //var point2 = new SeriesPoint();
                    //point2.Argument = "Hoàn thành";
                    //point2.Tag = "Hoàn thành";
                    //point2.Values = new double[] {hoanThanh / tong};
                    //series1.Points.Add(point2);

                    //foreach (var item in obj)
                    //{
                    //    var point = new SeriesPoint();
                    //    point.Argument = item.TenTrangThai;
                    //    point.Tag = item.TenTrangThai;
                    //    point.Values = new double[] { Convert.ToDouble(item.SoLuong) / tong };
                    //    series1.Points.Add(point);
                    //}
                    
                    //series1.LegendPointOptions.PointView = PointView.Argument;
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
            catch{}
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void cbxKbc_EditValueChanged(object sender, EventArgs e)
        {
            SetDate(((ComboBoxEdit)sender).SelectedIndex);
        }
    }
}
