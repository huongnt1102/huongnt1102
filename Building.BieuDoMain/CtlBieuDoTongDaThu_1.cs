using DevExpress.XtraCharts;
using System.Linq;

namespace Building.BieuDoMain
{
    public partial class CtlBieuDoTongDaThu_1 : DevExpress.XtraEditors.XtraUserControl
    {
        /// <summary>
        /// phải thu = phát sinh + nợ đầu kỳ
        /// </summary>
        public CtlBieuDoTongDaThu_1()
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
            itemKyBaoCao.EditValue = objKbc.Source[3];

            SetDate(3);
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

        public class chart_tong_da_thu_1
        {
            public decimal? SoTien { get; set; }

            public string TenLDV { get; set; }
            public int? MaLDV { get; set; }
        }

        private System.Collections.Generic.List<chart_tong_da_thu_1> GetHangMucThus()
        {
            var tuNgay = (System.DateTime?)itemTuNgay.EditValue;
            var denNgay = (System.DateTime?)itemDenNgay.EditValue;
            var strToaNha = (itemToaNha.EditValue ?? "").ToString().TrimEnd(',').TrimStart(',').Replace(" ", "");

            var model = new { tungay = tuNgay, denngay = denNgay, arr_tn = strToaNha };
            var param = new Dapper.DynamicParameters();
            param.AddDynamicParams(model);
            var result = Library.Class.Connect.QueryConnect.Query<chart_tong_da_thu_1>("chart_tong_da_thu_1", param);
            if (result.Count() > 0) return result.ToList();
            else return null;
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


                // data
                System.Collections.Generic.List<chart_tong_da_thu_1> datas = null;
                await System.Threading.Tasks.Task.Run(() => { datas = GetHangMucThus(); }).ConfigureAwait(true);

                // chart
                chartControl1.DataSource = null;
                chartControl1.Series.Clear();
                chartControl1.RefreshData();

                chartControl1.Titles[0].Text = "Tổng đã thu: " + string.Format("{0:n0}", datas.Sum(_ => _.SoTien)) ;

                // tạo series
                var series = new DevExpress.XtraCharts.Series("Tổng đã thu", DevExpress.XtraCharts.ViewType.Doughnut);
                series.Points.Clear();

                // data cho series
                foreach (var item in datas) series.Points.Add(new DevExpress.XtraCharts.SeriesPoint(item.TenLDV, item.SoTien));

                // add series vào chart
                chartControl1.Series.Add(series);

                // định dạng legend
                series.LegendTextPattern = "{A}: {V:n0} - {VP:P0}";

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

                //itemNap.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
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
