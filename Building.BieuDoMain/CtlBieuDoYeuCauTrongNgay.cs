using System.Linq;

namespace Building.BieuDoMain
{
    public partial class CtlBieuDoYeuCauTrongNgay : DevExpress.XtraEditors.XtraUserControl
    {
        public CtlBieuDoYeuCauTrongNgay()
        {
            InitializeComponent();
        }

        private void CtlCtlBieuDoYeuCauTrongNgay_Load(object sender, System.EventArgs e)
        {
            if (Library.Common.User == null) return;

            ckCbxToaNha.DataSource = Library.Common.TowerList;
            itemToaNha.EditValue = Library.Common.User.MaTN;

            var objKbc = new Library.KyBaoCao();
            foreach (var item in objKbc.Source) cbxKbc.Items.Add(item);
            itemKyBaoCao.EditValue = objKbc.Source[0];

            SetDate(0);
            LoadData();
        }

        private void SetDate(int index)
        {
            var objKbc = new Library.KyBaoCao { Index = index };
            objKbc.SetToDate();
            itemTuNgay.EditValue = objKbc.DateFrom;
            itemDenNgay.EditValue = objKbc.DateTo;
        }

        #region Số liệu biểu đồ

        public class ChartData
        {
            public string StatusName { get; set; }
            public int Count { get; set; }
        }

        private System.Collections.Generic.List<ChartData> GetChartDatas(string[] ltToaNha, System.DateTime? tuNgay, System.DateTime? denNgay)
        {
            using(Library.MasterDataContext db = new Library.MasterDataContext())
            {
                return (from yc in db.tnycYeuCaus
                        join tt in db.tnycTrangThais on yc.MaTT equals tt.MaTT
                        join mb in db.mbMatBangs on yc.MaMB equals mb.MaMB
                        where ltToaNha.Contains(yc.MaTN.ToString()) & yc.NgayYC != null & System.Data.Linq.SqlClient.SqlMethods.DateDiffDay(tuNgay, yc.NgayYC.Value) >= 0 & System.Data.Linq.SqlClient.SqlMethods.DateDiffDay(yc.NgayYC.Value, denNgay) >= 0
                        select new ChartData
                        {
                            StatusName = tt.TenTT,
                            Count = 1
                        }).ToList();
            }
        }

        #endregion

        private async void LoadData()
        {
            try
            {
                Library.PhanQuyenBieuDo.SaveControl(GetType().Namespace + "." + Name, chartControl1.Titles[0].Lines[0].ToString(), GetType().Namespace+".dll");

                var strToaNha = (itemToaNha.EditValue ?? "").ToString().TrimEnd(',').TrimStart(',').Replace(" ", "");
                var ltToaNha = strToaNha.Split(',');
                if (strToaNha == "") return;
                var tuNgay = (System.DateTime?) itemTuNgay.EditValue;
                var denNgay = (System.DateTime?) itemDenNgay.EditValue;

                itemNap.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

                System.Collections.Generic.List<ChartData> chartDatas = new System.Collections.Generic.List<ChartData>();
                await System.Threading.Tasks.Task.Run(() => { chartDatas = GetChartDatas(ltToaNha, tuNgay, denNgay); });

                var data = (from l in chartDatas group new { l } by new { l.StatusName } into g select new { g.Key.StatusName, Count = g.Sum(_ => _.l.Count) }).ToList();

                chartControl1.DataSource = null;
                chartControl1.Series.Clear();
                chartControl1.RefreshData();

                // tạo series
                var series = new DevExpress.XtraCharts.Series("Yêu cầu trong ngày", DevExpress.XtraCharts.ViewType.Doughnut);
                series.Points.Clear();

                // data cho series
                foreach (var item in data) series.Points.Add(new DevExpress.XtraCharts.SeriesPoint(item.StatusName, item.Count));

                // add series vào chart
                chartControl1.Series.Add(series);

                // định dạng legend
                series.LegendTextPattern = "{A}: {V:n0}";

                // định dạng text
                series.Label.TextPattern = "{A}: {VP:P0}";

                // adjust vị trí của series label
                ((DevExpress.XtraCharts.DoughnutSeriesLabel)series.Label).Position = DevExpress.XtraCharts.PieSeriesLabelPosition.TwoColumns;

                // detext overlapping của series label
                ((DevExpress.XtraCharts.DoughnutSeriesLabel)series.Label).ResolveOverlappingMode = DevExpress.XtraCharts.ResolveOverlappingMode.Default;
                ((DevExpress.XtraCharts.DoughnutSeriesLabel)series.Label).ResolveOverlappingMinIndent = 5;

                // không cần myview
                chartControl1.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;
                chartControl1.Dock = System.Windows.Forms.DockStyle.Fill;

                itemNap.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
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
