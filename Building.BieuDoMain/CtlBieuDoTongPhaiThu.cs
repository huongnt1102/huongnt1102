using System.Linq;

namespace Building.BieuDoMain
{
    public partial class CtlBieuDoTongPhaiThu : DevExpress.XtraEditors.XtraUserControl
    {
        /// <summary>
        /// phải thu = phát sinh + nợ đầu kỳ
        /// </summary>
        public CtlBieuDoTongPhaiThu()
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
            public byte? MaTn { get; set; }
            public long? IdHd { get; set; }
            public long? LinkId { get; set; }
            public decimal? SoTien { get; set; }
        }

        private System.Collections.Generic.List<SoLieu> GetNoDauKyHd1 (string[] ltToaNha, System.DateTime? tuNgay)
        {
            using(Library.MasterDataContext db = new Library.MasterDataContext())
            {
                return db.dvHoaDons.Where(_ => _.IsDuyet == true & (System.Data.Linq.SqlClient.SqlMethods.DateDiffDay(_.NgayTT, tuNgay) > 0 & ltToaNha.Contains(_.MaTN.ToString()))).Select(_ => new SoLieu { SoTien = _.PhaiThu, MaLdv = _.MaLDV, IdHd = _.ID}).ToList();
            }
        }

        private System.Collections.Generic.List<SoLieu> GetNoDauKyHdLdv(System.Collections.Generic.List<SoLieu> noDauKyHd1)
        {
            return (from hd in noDauKyHd1 group hd by new { hd.MaLdv } into g select new SoLieu { SoTien = g.Sum(_ => _.SoTien), MaLdv = g.Key.MaLdv }).ToList();
        }

        private System.Collections.Generic.List<SoLieu> GetNoDauKySq1(string[] ltToaNha, System.DateTime? tuNgay)
        {
            using(Library.MasterDataContext db = new Library.MasterDataContext())
            {
                return (from sq in db.SoQuy_ThuChis where System.Data.Linq.SqlClient.SqlMethods.DateDiffDay(sq.NgayPhieu, tuNgay) > 0 & ltToaNha.Contains(sq.MaTN.ToString()) & sq.IsPhieuThu == true & sq.MaLoaiPhieu != 24 & sq.LinkID != null & sq.TableName == "dvHoaDon" select new SoLieu {LinkId = sq.LinkID, SoTien = sq.DaThu.GetValueOrDefault() + sq.KhauTru.GetValueOrDefault() - sq.ThuThua.GetValueOrDefault() }).ToList();
            }
        }

        private System.Collections.Generic.List<SoLieu> GetNoDauKySqLdv(System.Collections.Generic.List<SoLieu> noDauKySq1, System.Collections.Generic.List<SoLieu> noDauKyHd1)
        {
            return (from sq in noDauKySq1 join hd in noDauKyHd1 on sq.LinkId equals hd.IdHd group new { sq, hd } by new { hd.MaLdv } into g select new SoLieu { SoTien = g.Sum(_ => _.sq.SoTien), MaLdv = g.Key.MaLdv }).ToList();
        }

        private System.Collections.Generic.List<SoLieu> GetPhatSinhs(string[] ltToaNha, System.DateTime? tuNgay, System.DateTime? denNgay)
        {
            using(Library.MasterDataContext db = new Library.MasterDataContext())
            {
                return (from hd in db.dvHoaDons where System.Data.Linq.SqlClient.SqlMethods.DateDiffDay(tuNgay, hd.NgayTT) >= 0 & System.Data.Linq.SqlClient.SqlMethods.DateDiffDay(hd.NgayTT, denNgay) >= 0 & hd.IsDuyet == true & ltToaNha.Contains(hd.MaTN.ToString()) group hd by new { hd.MaLDV } into g select new SoLieu { SoTien = g.Sum(_ => _.PhaiThu).GetValueOrDefault(), MaLdv = g.Key.MaLDV }).ToList();
            }
        }

        private System.Collections.Generic.List<LoaiDichVu> GetLoaiDichVus()
        {
            using(Library.MasterDataContext db = new Library.MasterDataContext())
            {
                return db.dvLoaiDichVus.OrderBy(_ => _.TenLDV).Select(_ => new LoaiDichVu { ID = _.ID, TenLDV = _.TenLDV }).ToList();
            }
        }

        private System.Collections.Generic.List<ChartData> GetChartDatas(System.Collections.Generic.List<SoLieu> noDauKyHd, System.Collections.Generic.List<SoLieu> noDauKySq, System.Collections.Generic.List<SoLieu> phatSinh, System.Collections.Generic.List<LoaiDichVu> dv)
        {
            try
            {
                return (from ldv in dv
                            join hd in noDauKyHd on ldv.ID equals hd.MaLdv into hoaDon
                            from hd in hoaDon.DefaultIfEmpty()
                            join sq in noDauKySq on ldv.ID equals sq.MaLdv into soQuy
                            from sq in soQuy.DefaultIfEmpty()
                            join ps in phatSinh on ldv.ID equals ps.MaLdv into phatSinhs
                            from ps in phatSinhs.DefaultIfEmpty()
                            select new ChartData
                            {
                                StatusName = ldv.TenLDV,
                                Count = (hd != null ? hd.SoTien.GetValueOrDefault() : 0) - (sq != null ? sq.SoTien.GetValueOrDefault() : 0) + (ps != null ? ps.SoTien.GetValueOrDefault() : 0)
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

                System.Collections.Generic.List<SoLieu> noDauKyHd1 = new System.Collections.Generic.List<SoLieu>();
                System.Collections.Generic.List<SoLieu> noDauKyHd = new System.Collections.Generic.List<SoLieu>();
                System.Collections.Generic.List<SoLieu> noDauKySq1 = new System.Collections.Generic.List<SoLieu>();
                System.Collections.Generic.List<SoLieu> noDauKySq = new System.Collections.Generic.List<SoLieu>();
                System.Collections.Generic.List<SoLieu> phatSinhs = new System.Collections.Generic.List<SoLieu>();
                System.Collections.Generic.List<LoaiDichVu> loaiDichVus = new System.Collections.Generic.List<LoaiDichVu>();

                System.Collections.Generic.List<ChartData> chartDatas = new System.Collections.Generic.List<ChartData>();
                System.Collections.Generic.List<ChartData> datas = new System.Collections.Generic.List<ChartData>();
                
                // nợ đầu kỳ hóa đơn
                await System.Threading.Tasks.Task.Run(() => { noDauKyHd1 = GetNoDauKyHd1(ltToaNha, tuNgay); });
                await System.Threading.Tasks.Task.Run(() => { noDauKyHd = GetNoDauKyHdLdv(noDauKyHd1); });

                // nợ đầu kỳ sổ quỹ
                await System.Threading.Tasks.Task.Run(() => { noDauKySq1 = GetNoDauKySq1(ltToaNha, tuNgay); });
                await System.Threading.Tasks.Task.Run(() => { noDauKySq = GetNoDauKySqLdv(noDauKySq1, noDauKyHd1); });

                // phát sinh
                await System.Threading.Tasks.Task.Run(() => { phatSinhs = GetPhatSinhs(ltToaNha, tuNgay, denNgay); });

                // loại dịch vụ
                await System.Threading.Tasks.Task.Run(() => { loaiDichVus = GetLoaiDichVus(); });

                // data
                await System.Threading.Tasks.Task.Run(() => { chartDatas = GetChartDatas(noDauKyHd, noDauKySq, phatSinhs, loaiDichVus); });
                await System.Threading.Tasks.Task.Run(() => { datas = GetDatas(chartDatas); });

                var data = (from l in datas where l.Count > 0 select new ChartData { StatusName = l.StatusName, Count = l.Count }).ToList();

                // chart
                chartControl1.DataSource = null;
                chartControl1.Series.Clear();
                chartControl1.RefreshData();

                chartControl1.Titles[0].Text = "Tổng phải thu: " + string.Format("{0:n0}", data.Sum(_ => _.Count)) ;

                // tạo series
                var series = new DevExpress.XtraCharts.Series("Tổng phải thu", DevExpress.XtraCharts.ViewType.Doughnut);
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
