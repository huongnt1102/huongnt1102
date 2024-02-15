using DevExpress.XtraCharts;
using System.Linq;
using System.Windows.Forms;

namespace Building.BieuDoMain
{
    public partial class CtlLineDoanhThu_DaThu : DevExpress.XtraEditors.XtraUserControl
    {
        public CtlLineDoanhThu_DaThu()
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
            itemKyBaoCao.EditValue = objKbc.Source[7];

            SetDate(7);
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
        public class chart_doanh_thu_phai_thu
        {
            public int? MaLDV { get; set; }

            public decimal? SoTien { get; set; }

            public string TenLDV { get; set; }

        }

        public class chart_doanh_thu_phai_thu_data
        {
            public decimal? SoTien { get; set; }

            public System.DateTime? NgayTT { get; set; }

            public string TenLDV { get; set; }
            public int? MaLDV { get; set; }
        }

        private System.Collections.Generic.List<chart_doanh_thu_phai_thu_data> GetHangMucThus()
        {
            var tuNgay = (System.DateTime?)itemTuNgay.EditValue;
            var denNgay = (System.DateTime?)itemDenNgay.EditValue;
            var strToaNha = (itemToaNha.EditValue ?? "").ToString().TrimEnd(',').TrimStart(',').Replace(" ", "");

            var model = new { tungay = tuNgay, denngay = denNgay, arr_tn = strToaNha };
            var param = new Dapper.DynamicParameters();
            param.AddDynamicParams(model);
            var result = Library.Class.Connect.QueryConnect.Query<chart_doanh_thu_phai_thu_data>("chart_doanh_thu_phai_thu_data", param);
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

                //itemNap.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

                System.Collections.Generic.List<chart_doanh_thu_phai_thu_data> datas = null;
                await System.Threading.Tasks.Task.Run(() => { datas = GetHangMucThus(); }).ConfigureAwait(true);

                // chart
                chartControl1.DataSource = null;
                chartControl1.Series.Clear();
                chartControl1.RefreshData();

                var model = new { tungay = tuNgay, denngay = denNgay, arr_tn = strToaNha };
                var param_ldv = new Dapper.DynamicParameters();
                param_ldv.AddDynamicParams(model);
                var result_ldv = Library.Class.Connect.QueryConnect.Query<chart_doanh_thu_phai_thu>("chart_doanh_thu_phai_thu", param_ldv);

                if (result_ldv.Count() == 0) return;

                foreach (var item in result_ldv)
                {
                    var series = new DevExpress.XtraCharts.Series(item.TenLDV, DevExpress.XtraCharts.ViewType.Line);
                    series.Points.Clear();
                    var data = datas.Where(_ => _.MaLDV == item.MaLDV);
                    foreach (var i in data) series.Points.Add(new DevExpress.XtraCharts.SeriesPoint(i.NgayTT, i.SoTien));
                    chartControl1.Series.Add(series);
                    series.LegendTextPattern = "{A}";
                    series.Label.TextPattern = "{A}: {VP:P0}";

                    ((DevExpress.XtraCharts.LineSeriesView)series.View).MarkerVisibility = DevExpress.Utils.DefaultBoolean.True;
                    ((DevExpress.XtraCharts.LineSeriesView)series.View).LineMarkerOptions.Kind = MarkerKind.Triangle;
                    ((LineSeriesView)series.View).LineStyle.DashStyle = DashStyle.Dash;
                }

                ((XYDiagram)chartControl1.Diagram).EnableAxisXZooming = true;

                chartControl1.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;
                //Checkbox cho legend
                chartControl1.Legend.MarkerMode = LegendMarkerMode.CheckBoxAndMarker;

                chartControl1.Dock = DockStyle.Fill;
                this.Controls.Add(chartControl1);
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
