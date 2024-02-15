using System.Linq;

namespace Building.BieuDoMain
{
    public partial class CtlBieuDoTongDaThu : DevExpress.XtraEditors.XtraUserControl
    {
        public CtlBieuDoTongDaThu()
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
            public string StatusName { get; set; }
            public decimal? Count { get; set; }
        }

        public class LoaiDichVu
        {
            public int? ID { get; set; }
            public string TenLDV { get; set; }
        }

        public class SoLieu
        {
            public int? MaLdv { get; set; }
            public decimal? SoTien { get; set; }
        }

        private System.Collections.Generic.List<SoLieu> GetDaThus(string[] ltToaNha, System.DateTime? tuNgay, System.DateTime? denNgay)
        {
            using(Library.MasterDataContext db = new Library.MasterDataContext())
            {
                return (from ct in db.SoQuy_ThuChis
                        join hd in db.dvHoaDons on new { ct.TableName, ct.LinkID } equals new { TableName = "dvHoaDon", LinkID = (long?)hd.ID } into hoaDon
                        from hd in hoaDon.DefaultIfEmpty()
                        where System.Data.Linq.SqlClient.SqlMethods.DateDiffDay(tuNgay, ct.NgayPhieu) >= 0 &
                        System.Data.Linq.SqlClient.SqlMethods.DateDiffDay(ct.NgayPhieu, denNgay) >= 0 &
                        ct.IsPhieuThu == true &
                        ct.MaLoaiPhieu != 24 &
                        ltToaNha.Contains(ct.MaTN.ToString()) &
                        ct.LinkID != null
                        select new SoLieu
                        {
                            MaLdv = hd == null ? 0 : hd.MaLDV,
                            SoTien = ct.DaThu.GetValueOrDefault() + ct.KhauTru.GetValueOrDefault() - ct.ThuThua.GetValueOrDefault()
                        }).ToList();
            }
        }

        private System.Collections.Generic.List<SoLieu> GetDaThu(System.Collections.Generic.List<SoLieu> daThus)
        {
            return (from ct in daThus group ct by ct.MaLdv into g select new SoLieu { MaLdv = g.Key, SoTien = g.Sum(_ => _.SoTien) }).ToList();
        }

        private System.Collections.Generic.List<LoaiDichVu> GetLoaiDichVus()
        {
            using(Library.MasterDataContext db = new Library.MasterDataContext())
            {
                return db.dvLoaiDichVus.OrderBy(_ => _.TenLDV).Select(_ => new LoaiDichVu { ID = _.ID, TenLDV = _.TenLDV }).ToList();
            }
        }

        private System.Collections.Generic.List<ChartData> GetChartDatas(System.Collections.Generic.List<SoLieu> soLieus ,System.Collections.Generic.List<LoaiDichVu> dv)
        {
            try
            {
                return (from ldv in dv
                            join hd in soLieus on ldv.ID equals hd.MaLdv into hoaDon
                            from hd in hoaDon.DefaultIfEmpty()
                            select new ChartData
                            {
                                StatusName = ldv.TenLDV,
                                Count = (hd != null ? hd.SoTien.GetValueOrDefault() : 0) 
                            }).ToList();
            }
            catch (System.Exception ex) { return null; }
        }

        private System.Collections.Generic.List<ChartData> GetDatas(System.Collections.Generic.List<ChartData> chartDatas)
        {
            return (from l in chartDatas group new { l } by new { l.StatusName } into g select new ChartData { StatusName = g.Key.StatusName, Count = g.Sum(_ => _.l.Count) }).ToList();
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

                System.Collections.Generic.List<SoLieu> daThus = new System.Collections.Generic.List<SoLieu>();
                System.Collections.Generic.List<SoLieu> daThu = new System.Collections.Generic.List<SoLieu>();
                System.Collections.Generic.List<LoaiDichVu> loaiDichVus = new System.Collections.Generic.List<LoaiDichVu>();

                System.Collections.Generic.List<ChartData> chartDatas = new System.Collections.Generic.List<ChartData>();
                System.Collections.Generic.List<ChartData> datas = new System.Collections.Generic.List<ChartData>();
                
                // dữ liệu đã thu
                await System.Threading.Tasks.Task.Run(() => { daThus = GetDaThus(ltToaNha, tuNgay, denNgay); });
                await System.Threading.Tasks.Task.Run(() => { daThu = GetDaThu(daThus); });

                // loại dịch vụ
                await System.Threading.Tasks.Task.Run(() => { loaiDichVus = GetLoaiDichVus(); });

                // data
                await System.Threading.Tasks.Task.Run(() => { chartDatas = GetChartDatas(daThu, loaiDichVus); });
                await System.Threading.Tasks.Task.Run(() => { datas = GetDatas(chartDatas); });

                var data = (from l in datas where l.Count > 0 select new ChartData { StatusName = l.StatusName, Count = l.Count }).ToList();

                // chart
                chartControl1.DataSource = null;
                chartControl1.Series.Clear();
                chartControl1.RefreshData();

                chartControl1.Titles[0].Text = "Tổng đã thu: " + string.Format("{0:n0}", data.Sum(_ => _.Count)) ;

                // tạo series
                var series = new DevExpress.XtraCharts.Series("Tổng đã thu", DevExpress.XtraCharts.ViewType.Doughnut);
                series.Points.Clear();

                // data cho series
                foreach (var item in data) series.Points.Add(new DevExpress.XtraCharts.SeriesPoint(item.StatusName, item.Count));

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
