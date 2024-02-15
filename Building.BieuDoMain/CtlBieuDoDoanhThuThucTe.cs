using System.Linq;

namespace Building.BieuDoMain
{
    public partial class CtlBieuDoDoanhThuThucTe : DevExpress.XtraEditors.XtraUserControl
    {
        public CtlBieuDoDoanhThuThucTe()
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
        public class ChartData
        {
            public int? MaTn { get; set; }
            public string TenTn { get; set; }
            public decimal? DoanhThu { get; set; }
        }

        public System.Collections.Generic.List<ChartData> GetSoLieus(string[] ltToaNha, System.DateTime? tuNgay, System.DateTime? denNgay)
        {
            using(Library.MasterDataContext db = new Library.MasterDataContext())
            {
                return (from ltt in db.ctLichThanhToans
                        join dv in db.dvHoaDons on new { TableName = "ctLichThanhToan", LinkID = (int?)ltt.ID } equals new { dv.TableName, dv.LinkID } into hoaDon
                        from dv in hoaDon.DefaultIfEmpty()
                        join ctt in db.ptChiTietPhieuThus on new { TableName = "dvHoaDon", LinkID = (long?)dv.ID } equals new { ctt.TableName, ctt.LinkID } into chiTietPhieuThu
                        from ctt in chiTietPhieuThu.DefaultIfEmpty()
                        join mb in db.mbMatBangs on ltt.MaMB equals mb.MaMB
                        join tn in db.tnToaNhas on mb.MaTN equals tn.MaTN
                        where ltToaNha.Contains(mb.MaTN.ToString()) &
                        System.Data.Linq.SqlClient.SqlMethods.DateDiffDay(tuNgay, ltt.TuNgay) >= 0 &
                        System.Data.Linq.SqlClient.SqlMethods.DateDiffDay(ltt.TuNgay, denNgay) >= 0
                        select new ChartData
                        {
                            MaTn = mb.MaTN,
                            TenTn = tn.TenTN,
                            DoanhThu = ctt.SoTien.GetValueOrDefault()
                        }).ToList();
            }
        }

        public System.Collections.Generic.List<ChartData> GetChartDatas(System.Collections.Generic.List<ChartData> chartDatas)
        {
            return (from l in chartDatas group new { l } by new { l.TenTn } into g select new ChartData { TenTn = g.Key.TenTn, DoanhThu = g.Sum(_ => _.l.DoanhThu).GetValueOrDefault() }).ToList();
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

                System.Collections.Generic.List<ChartData> chartDatas = new System.Collections.Generic.List<ChartData>();
                System.Collections.Generic.List<ChartData> datas = new System.Collections.Generic.List<ChartData>();
                
                await System.Threading.Tasks.Task.Run(() => { chartDatas = GetSoLieus(ltToaNha, tuNgay, denNgay); });
                await System.Threading.Tasks.Task.Run(() => { datas = GetChartDatas(chartDatas); });

                // chart
                chartControl1.DataSource = null;
                chartControl1.Series.Clear();
                chartControl1.RefreshData();

                // tạo series
                var series = new DevExpress.XtraCharts.Series("Tổng phải thu", DevExpress.XtraCharts.ViewType.Doughnut);
                series.Points.Clear();

                // data cho series

                foreach(var item in datas) series.Points.Add(new DevExpress.XtraCharts.SeriesPoint(item.TenTn, item.DoanhThu));

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
