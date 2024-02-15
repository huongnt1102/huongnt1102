using System.Linq;

namespace Building.BieuDoMain
{
    public partial class CtlBieuDoTyLeLapDay : DevExpress.XtraEditors.XtraUserControl
    {
        public CtlBieuDoTyLeLapDay()
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

        public class ChartData { public decimal? TongDienTich { get; set; } public decimal? CanHoThue { get; set; } public decimal? VanPhongThue { get; set; } public decimal? KhacThue { get; set; } public decimal? TongDienTichThue { get; set; } public decimal? CanHoTrong { get; set; } public decimal? VanPhongTrong { get; set; } public decimal? KhacTrong { get; set; } public decimal? TongDienTichTrong { get; set; } public decimal? TyLeLapDay { get; set; } }

        public System.Collections.Generic.List<ChartData> GetSoLieus(string[] ltToaNha)
        {
            using(Library.MasterDataContext db = new Library.MasterDataContext())
            {
                return (from mb in db.mbMatBangs
                        join lmb in db.mbLoaiMatBangs on mb.MaLMB equals lmb.MaLMB
                        join tt in db.mbTrangThais on mb.MaTT equals tt.MaTT
                        where ltToaNha.Contains(mb.MaTN.ToString())
                        select new ChartData
                        {
                            TongDienTich = db.mbMatBangs.Where(_ => ltToaNha.Contains(mb.MaTN.ToString())).Sum(_ => _.DienTich).GetValueOrDefault(),

                            // đang sử dụng
                            CanHoThue = tt.ChoThue == false & lmb.MaNMB == 1 ? mb.DienTich : 0,
                            VanPhongThue = tt.ChoThue == false & lmb.MaNMB == 2 ? mb.DienTich : 0,
                            KhacThue = tt.ChoThue == false & lmb.MaNMB == 3 ? mb.DienTich : 0,
                            TongDienTichThue = tt.ChoThue == false ? mb.DienTich : 0,

                            // chưa sử dụng
                            CanHoTrong = tt.ChoThue == true & lmb.MaNMB == 1 ? mb.DienTich : 0,
                            VanPhongTrong = tt.ChoThue == true & lmb.MaNMB == 2 ? mb.DienTich : 0,
                            KhacTrong = tt.ChoThue == true & lmb.MaNMB == 3 ? mb.DienTich : 0,
                            TongDienTichTrong = tt.ChoThue == true ? mb.DienTich : 0
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
                //var tuNgay = (System.DateTime?) itemTuNgay.EditValue;
                //var denNgay = (System.DateTime?) itemDenNgay.EditValue;

                //itemNap.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

                System.Collections.Generic.List<ChartData> chartDatas = new System.Collections.Generic.List<ChartData>();
                
                await System.Threading.Tasks.Task.Run(() => { chartDatas = GetSoLieus(ltToaNha); });

                // chart
                chartControl1.DataSource = null;
                chartControl1.Series.Clear();
                chartControl1.RefreshData();

                // tạo series
                var series = new DevExpress.XtraCharts.Series("Tỷ lệ lấp đầy", DevExpress.XtraCharts.ViewType.Doughnut);
                series.Points.Clear();

                // data cho series
                series.Points.Add(new DevExpress.XtraCharts.SeriesPoint("Căn hộ cho thuê", chartDatas.Sum(_ => _.CanHoThue).GetValueOrDefault()));
                series.Points.Add(new DevExpress.XtraCharts.SeriesPoint("Chưa cho thuê", chartDatas.Select(_=>_.TongDienTich).FirstOrDefault() - chartDatas.Sum(_=>_.TongDienTichThue).GetValueOrDefault()));
                series.Points.Add(new DevExpress.XtraCharts.SeriesPoint("Văn phòng cho thuê", chartDatas.Sum(_ => _.VanPhongThue).GetValueOrDefault()));
                series.Points.Add(new DevExpress.XtraCharts.SeriesPoint("Thuê khác", chartDatas.Sum(_ => _.KhacThue).GetValueOrDefault()));

                chartControl1.Titles[0].Text = "Tỷ lệ lấp đầy: " + string.Format("{0:n0}", chartDatas.Sum(_ => _.TongDienTichThue).GetValueOrDefault() / chartDatas.Select(_ => _.TongDienTich).FirstOrDefault() * 100) + " %";

                // add series vào chart
                chartControl1.Series.Add(series);

                // định dạng legend
                series.LegendTextPattern = "{A}: {V:n0} m2";

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
