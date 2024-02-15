

using DevExpress.XtraCharts;
using Library;
using System.Linq;

namespace Building.BieuDoMain
{
    public partial class CtlDanhGiaSao : DevExpress.XtraEditors.XtraUserControl
    {
        public CtlDanhGiaSao()
        {
            InitializeComponent();
        }
        public class SaoClass{public int? SoSao { get; set; } public string TenSao { get; set; } }

        private void CtlDanhGiaSao_Load(object sender, System.EventArgs e)
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
                Library.PhanQuyenBieuDo.SaveControl(GetType().Namespace + "." + Name, chartControl1.Titles[0].Lines[0].ToString(), GetType().Namespace+".dll");

                var strToaNha = (itemToaNha.EditValue ?? "").ToString().TrimEnd(',').TrimStart(',').Replace(" ", "");
                var ltToaNha = strToaNha.Split(',');
                if (strToaNha == "") return;
                var tuNgay = (System.DateTime?) itemTuNgay.EditValue;
                var denNgay = (System.DateTime?) itemDenNgay.EditValue;

                using (var db = new MasterDataContext())
                {
                    var obj = (from p in db.tnycYeuCaus
                        where ltToaNha.Contains(p.MaTN.ToString()) &
                              System.Data.Linq.SqlClient.SqlMethods.DateDiffDay(tuNgay, p.NgayYC.Value) >= 0 &
                              System.Data.Linq.SqlClient.SqlMethods.DateDiffDay(p.NgayYC.Value, denNgay) >= 0 & p.MaTT == 5
                        group p by p.Rating
                        into g
                        select new {SoSao = g.Key, SoLuong = g.Count()}).ToList();
                    var objSao = new System.Collections.Generic.List<SaoClass>();
                    for (var i = 0; i <= 5; i++)
                    {
                        var item = new SaoClass {SoSao = i, TenSao = i + "*"};
                        objSao.Add(item);
                    }

                    var objDataSao = (from s in objSao
                        join yc in obj on s.SoSao equals yc.SoSao into yeuCau
                        from yc in yeuCau.DefaultIfEmpty()
                        select new {s.TenSao, SoLuong = yc == null ? 0 : yc.SoLuong}).ToList();

                    chartControl1.DataSource = null;
                    chartControl1.Series.Clear();
                    chartControl1.RefreshData();

                    var series1 = new DevExpress.XtraCharts.Series("Đánh giá của cư dân", ViewType.Pie);
                    
                    series1.Points.Clear();
                    double tong2 = (double)objDataSao.Sum(p => p.SoLuong);
                    foreach (var item in objDataSao)
                    {
                        series1.Points.Add(new SeriesPoint(item.TenSao,
                            new double[] { System.Convert.ToDouble(System.Convert.ToDouble(item.SoLuong) / tong2)}));
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
                    chartControl1.Dock = System.Windows.Forms.DockStyle.Fill;
                }
            }
            catch (System.Exception e)
            {
                //
            }
        }

        private void ItemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void cbxKbc_EditValueChanged(object sender, System.EventArgs e)
        {
            SetDate(((DevExpress.XtraEditors.ComboBoxEdit)sender).SelectedIndex);
        }
    }
}
