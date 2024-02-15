using System.Linq;

namespace Building.BieuDoMain
{
    public partial class CtlBieuDoTinhHinhThueCanHo : DevExpress.XtraEditors.XtraUserControl
    {
        public CtlBieuDoTinhHinhThueCanHo()
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
            public decimal? TongGiaTri { get; set; }
            public decimal? DaThu { get; set; }
            public decimal? ConNo { get; set; }
        }

        public System.Collections.Generic.List<ChartData> GetSoLieus(string[] ltToaNha, System.DateTime? tuNgay, System.DateTime? denNgay)
        {
            using(Library.MasterDataContext db = new Library.MasterDataContext())
            {
                return (from ct in db.ctChiTiets
                        join hd in db.ctHopDongs on ct.MaHDCT equals hd.ID
                        join kh in db.tnKhachHangs on hd.MaKH equals kh.MaKH
                        join mb in db.mbMatBangs on ct.MaMB equals mb.MaMB
                        join tl in db.mbTangLaus on mb.MaTL equals tl.MaTL
                        join kn in db.mbKhoiNhas on tl.MaKN equals kn.MaKN
                        join tn in db.tnToaNhas on kn.MaTN equals tn.MaTN
                        join lmb in db.mbLoaiMatBangs on mb.MaLMB equals lmb.MaLMB
                        where ltToaNha.Contains(tn.MaTN.ToString()) &
                        System.Data.Linq.SqlClient.SqlMethods.DateDiffDay(tuNgay, hd.NgayHL) >= 0 &
                        System.Data.Linq.SqlClient.SqlMethods.DateDiffDay(hd.NgayHL, denNgay) >= 0
                        select new ChartData
                        {
                            TongGiaTri = ((hd.ThoiHan * ct.TongGiaThue) + (hd.ThoiHan * ct.TienVAT)) * hd.TyGia,
                            DaThu = (from ltt in db.ctLichThanhToans
                                     join dv in db.dvHoaDons on new { TableName = "ctLichThanhToan", LinkID = (int?)ltt.ID } equals new { dv.TableName, dv.LinkID } into tblLTT from dv in tblLTT.DefaultIfEmpty()
                                     join ctt in db.ptChiTietPhieuThus on new { TableName = "dvHoaDon", LinkID = (long?)dv.ID } equals new { ctt.TableName, ctt.LinkID } into cttPhieuThu from ctt in cttPhieuThu.DefaultIfEmpty()
                                     where dv.MaMB == ct.MaMB
                                     select new { ctt.SoTien }).Sum(p => p.SoTien).GetValueOrDefault(),
                            ConNo = (((hd.ThoiHan * ct.TongGiaThue) + (hd.ThoiHan * ct.TienVAT)) * hd.TyGia) -
                            ((from ltt in db.ctLichThanhToans
                              join dv in db.dvHoaDons on new { TableName = "ctLichThanhToan", LinkID = (int?)ltt.ID } equals new { dv.TableName, dv.LinkID } into tblLTT from dv in tblLTT.DefaultIfEmpty()
                              join ctt in db.ptChiTietPhieuThus on new { TableName = "dvHoaDon", LinkID = (long?)dv.ID } equals new { ctt.TableName, ctt.LinkID } into cttPhieuThu from ctt in cttPhieuThu.DefaultIfEmpty()
                              where dv.MaMB == ct.MaMB
                              select new { ctt.SoTien }).Sum(p => p.SoTien).GetValueOrDefault())
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

                //itemNap.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

                System.Collections.Generic.List<ChartData> chartDatas = new System.Collections.Generic.List<ChartData>();
                
                await System.Threading.Tasks.Task.Run(() => { chartDatas = GetSoLieus(ltToaNha, tuNgay, denNgay); });

                // chart
                chartControl1.DataSource = null;
                chartControl1.Series.Clear();
                chartControl1.RefreshData();

                // tạo series
                var series = new DevExpress.XtraCharts.Series("Tổng phải thu", DevExpress.XtraCharts.ViewType.Doughnut);
                series.Points.Clear();

                // data cho series
                series.Points.Add(new DevExpress.XtraCharts.SeriesPoint("Tổng giá trị HĐ", chartDatas.Sum(_ => _.TongGiaTri).GetValueOrDefault()));
                series.Points.Add(new DevExpress.XtraCharts.SeriesPoint("Đã thu", chartDatas.Sum(_ => _.DaThu).GetValueOrDefault()));
                series.Points.Add(new DevExpress.XtraCharts.SeriesPoint("Còn nợ", chartDatas.Sum(_ => _.ConNo).GetValueOrDefault()));

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
