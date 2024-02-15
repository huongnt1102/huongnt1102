using DevExpress.XtraCharts;
using System.Linq;

namespace Building.BieuDoMain
{
    public partial class CtlKeToanThanhToanPhi : DevExpress.XtraEditors.XtraUserControl
    {
        private Library.MasterDataContext _db = new Library.MasterDataContext();
        System.Collections.Generic.List<string> lToaNha = new System.Collections.Generic.List<string>();
        //private Library.tnKhachHang khachHang { get; set; }
        private int? MaKh { get; set; }
        private int Thang { get; set; }
        private int Nam { get; set; }
        private System.DateTime? TuNgay { get; set; }
        private System.DateTime? DenNgay { get; set; }

        public CtlKeToanThanhToanPhi()
        {
            InitializeComponent();
        }

        private void CtlKeToanThanhToanPhi_Load(object sender, System.EventArgs e)
        {
            if (Library.Common.User == null) return;

            foreach (var item in Library.Common.TowerList) lToaNha.Add(item.MaTN.ToString());

            #region Thiết lập khách hàng, năm, tháng

            itemThang.EditValue = System.DateTime.Now.Month;
            itemNam.EditValue = System.DateTime.Now.Year;

            #endregion

            LoadData();
        }

        #region Get Công nợ

        private decimal GetDauKy()
        {
            var dauNam = _db.dvDauKies.Where(_ => _.MaKH == MaKh & _.Nam == Nam).Sum(_ => _.SoTien).GetValueOrDefault();
            var noDauKyHd = _db.dvHoaDons.Where(_ => _.MaKH == MaKh & System.Data.Linq.SqlClient.SqlMethods.DateDiffDay(_.NgayTT, TuNgay) > 0 & _.IsDuyet == true & _.NgayTT.Value.Year == Nam).Sum(_ => _.PhaiThu).GetValueOrDefault();
            var noDauKySq = _db.SoQuy_ThuChis.Where(_ => _.MaKH == MaKh & System.Data.Linq.SqlClient.SqlMethods.DateDiffDay(_.NgayPhieu, TuNgay) > 0 & _.IsPhieuThu == true & _.MaLoaiPhieu != 24 & _.LinkID != null & _.NgayPhieu.Value.Year == Nam & _.TableName == "dvHoaDon").Sum(_ => _.DaThu - _.ThuThua + _.KhauTru).GetValueOrDefault();
            return dauNam + noDauKyHd - noDauKySq;
        }

        private decimal GetPhatSinh()
        {
            return _db.dvHoaDons.Where(_ => _.MaKH == MaKh & System.Data.Linq.SqlClient.SqlMethods.DateDiffMonth(_.NgayTT, TuNgay) == 0 & _.IsDuyet == true).Sum(_ => _.PhaiThu).GetValueOrDefault();
        }

        private decimal GetDaThu()
        {
            return _db.SoQuy_ThuChis
                .GroupJoin(_db.dvHoaDons, _ => new {_.TableName, _.LinkID}, hd => new {TableName = "dvHoaDon", LinkID = (long?) hd.ID}, (_, hoaDon) => new {_, hoaDon}).SelectMany(t => t.hoaDon.DefaultIfEmpty(), (t, hd) => new {t, hd})
                .Where(t => t.t._.MaKH == MaKh &
                            System.Data.Linq.SqlClient.SqlMethods.DateDiffMonth(t.t._.NgayPhieu, TuNgay) == 0 &
                            t.t._.IsPhieuThu == true & t.t._.MaLoaiPhieu != 24 & t.t._.IsKhauTru == false &
                            t.t._.LinkID != null & t.hd.IsDuyet == true)
                .Sum(i => i.t._.DaThu - i.t._.ThuThua - i.t._.KhauTru).GetValueOrDefault();
        }

        private decimal GetKhauTru()
        {
            return _db.SoQuy_ThuChis
                .GroupJoin(_db.dvHoaDons, sq => new {sq.TableName, sq.LinkID}, hd => new {TableName = "dvHoaDon", LinkID = (long?) hd.ID}, (sq, hoaDon) => new {sq, hoaDon}).SelectMany(t => t.hoaDon.DefaultIfEmpty(), (t, hd) => new {t, hd})
                .Where(t => t.t.sq.MaKH == MaKh &
                            System.Data.Linq.SqlClient.SqlMethods.DateDiffMonth(t.t.sq.NgayPhieu, TuNgay) == 0 &
                            t.t.sq.IsPhieuThu == true &
                            t.t.sq.IsKhauTru == true &
                            t.t.sq.LinkID != null &
                            t.hd.IsDuyet == true)
                .Sum(_ => _.t.sq.KhauTru + _.t.sq.DaThu).GetValueOrDefault();
        }

        private decimal GetThuTruoc()
        {
            return _db.SoQuy_ThuChis
                .Where(_ => _.MaKH == MaKh &
                            System.Data.Linq.SqlClient.SqlMethods.DateDiffDay(_.NgayPhieu, DenNgay) >= 0 &
                            _.IsPhieuThu == true & 
                            _.MaLoaiPhieu != 24)
                .Sum(_ => _.ThuThua - _.KhauTru).GetValueOrDefault();
        }

        private decimal GetThuTruocTrongKy()
        {
            return _db.SoQuy_ThuChis
                .Where(_ => _.MaKH == MaKh &
                            System.Data.Linq.SqlClient.SqlMethods.DateDiffMonth(_.NgayPhieu, TuNgay) == 0 &
                            _.IsPhieuThu == true &
                            _.MaLoaiPhieu != 24)
                .Sum(_ => _.ThuThua).GetValueOrDefault();
        }

        #endregion

        #region Get danh sách Phiếu thu

        public class DanhSachPhieuThu
        {
            public long? ID { get; set; }
            public int? IdPhieu { get; set; }
            public string SoPT { get; set; }
            public string LyDo { get; set; }
            public string LoaiDichVu { get; set; }
            public decimal? SoTien { get; set; }
            public System.DateTime? NgayThu { get; set; }
        }

        private System.Collections.Generic.List<DanhSachPhieuThu> GetDanhSachPhieuThu()
        {
            return (from sq in _db.SoQuy_ThuChis
                    join hd in _db.dvHoaDons on sq.LinkID equals hd.ID
                    join l in _db.dvLoaiDichVus on hd.MaLDV equals l.ID
                    where hd.MaKH == MaKh &
                          System.Data.Linq.SqlClient.SqlMethods.DateDiffDay(TuNgay, sq.NgayPhieu) >= 0 &
                          System.Data.Linq.SqlClient.SqlMethods.DateDiffDay(sq.NgayPhieu, DenNgay) >= 0
                          orderby l.TenHienThi
                    select new DanhSachPhieuThu
                    {
                        ID = sq.ID, SoPT = sq.SoPhieu, NgayThu = sq.NgayPhieu, LyDo = sq.DienGiai,
                        SoTien = sq.DaThu, IdPhieu = sq.IDPhieu, LoaiDichVu = l.TenHienThi
                    })
                .ToList();
        }

        #endregion

        #region Get danh sách phí phải thu

        public class PhiPhaiThu
        {
            public string HangMuc { get; set; }
            public string DienGiai { get; set; }
            public decimal? ConNo { get; set; }
            public string ThangTT { get; set; }
        }

        private System.Collections.Generic.List<PhiPhaiThu> GetDanhSachPhiPhaiThu()
        {
            return (from hd in _db.dvHoaDons
                join l in _db.dvLoaiDichVus on hd.MaLDV equals l.ID
                join mb in _db.mbMatBangs on hd.MaMB equals mb.MaMB
                where hd.MaKH == MaKh & hd.ConNo.GetValueOrDefault() > 0 & hd.IsDuyet == true
                select new PhiPhaiThu()
                {
                    HangMuc = l.TenHienThi,
                    ConNo = hd.PhaiThu - _db.SoQuy_ThuChis.Where(_ => _.TableName == "dvHoaDon" & _.LinkID == hd.ID).Sum(_ => _.DaThu + _.KhauTru).GetValueOrDefault(),
                    DienGiai = hd.DienGiai,
                    ThangTT = string.Format("{0:yyyy-MM}", hd.NgayTT)
                }).ToList();
        }

        #endregion

        private async void LoadData()
        {
            try
            {
                string controlCaption = "Màn hình kế toán thanh toán phí"; //chartControl1.Titles[0].Lines[0].ToString()
                Library.PhanQuyenBieuDo.SaveControl(GetType().Namespace + "." + Name, controlCaption, GetType().Namespace + ".dll");

                _db = new Library.MasterDataContext();

                if (MaKh == null) return;

                Library.tnKhachHang khachHang = _db.tnKhachHangs.FirstOrDefault(_ => _.MaKH == MaKh);
                if (khachHang == null) return;

                layoutControlGroup2.Text = "Công nợ trong tháng: Khách hàng " + (khachHang.IsCaNhan == true ? (khachHang.HoKH + " " + khachHang.TenKH) : khachHang.CtyTen) + " - " + khachHang.KyHieu;
                layoutControlGroup1.Text = "Phí thu trong tháng: Khách hàng " + (khachHang.IsCaNhan == true ? (khachHang.HoKH + " " + khachHang.TenKH) : khachHang.CtyTen) + " - " + khachHang.KyHieu;
                layoutControlGroup4.Text = "Phí còn lại phải thu: Khách hàng " + (khachHang.IsCaNhan == true ? (khachHang.HoKH + " " + khachHang.TenKH) : khachHang.CtyTen) + " - " + khachHang.KyHieu;

                itemNap.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                itemThuTien.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

                #region Set giá trị

                Thang = System.Convert.ToInt32(itemThang.EditValue);
                Nam = System.Convert.ToInt32(itemNam.EditValue);
                TuNgay = new System.DateTime(Nam, Thang, 1);
                DenNgay = Library.Common.GetLastDayOfMonth(Thang, Nam);

                #endregion

                #region thông số công nợ
                // 
                decimal dauKy = 0, phatSinh = 0, daThu = 0, khauTru = 0, thuTruoc = 0, thuTruocTrongKy = 0;
                await System.Threading.Tasks.Task.Run(() => { dauKy = GetDauKy(); });
                await System.Threading.Tasks.Task.Run(() => { phatSinh = GetPhatSinh(); });
                await System.Threading.Tasks.Task.Run(() => { daThu = GetDaThu(); });
                await System.Threading.Tasks.Task.Run(() => { khauTru = GetKhauTru(); });
                await System.Threading.Tasks.Task.Run(() => { thuTruoc = GetThuTruoc(); });
                await System.Threading.Tasks.Task.Run(() => { thuTruocTrongKy = GetThuTruocTrongKy(); });

                spinDauKy.Value = dauKy;
                spinPhatSinh.Value = phatSinh;
                spinDaThu.Value = daThu;
                spinKhauTru.Value = khauTru;

                spinThuTruoc.Value = thuTruoc;
                spinConLai.Value = dauKy + phatSinh - (daThu + khauTru - thuTruocTrongKy) - thuTruoc;

                #endregion

                #region danh sách phiếu thu
                // 
                System.Collections.Generic.List<DanhSachPhieuThu> lPhieuThu = null;
                await System.Threading.Tasks.Task.Run(() => { lPhieuThu = GetDanhSachPhieuThu(); });
                gc.DataSource = lPhieuThu;

                #endregion

                #region Danh sách phí phải thu

                System.Collections.Generic.List<PhiPhaiThu> lPhiPhaiThu = null;
                await System.Threading.Tasks.Task.Run(() => { lPhiPhaiThu = GetDanhSachPhiPhaiThu(); });
                gcThuTien.DataSource = lPhiPhaiThu;

                #endregion

                #region Biểu đồ Phí phải thu trong tháng

                var dataPhiPhaiThu = (from l in lPhiPhaiThu group new {l} by new {l.HangMuc} into g orderby g.Key.HangMuc select new {g.Key.HangMuc, SoTien = g.Sum(_=>_.l.ConNo).GetValueOrDefault()}).ToList();

                // tạo chart trống
                chartControl1.DataSource = null;
                chartControl1.Series.Clear();
                chartControl1.RefreshData();

                // tạo 1 pie series
                DevExpress.XtraCharts.Series series1 = new DevExpress.XtraCharts.Series("Phí phải thu trong tháng", DevExpress.XtraCharts.ViewType.Pie);
                series1.Points.Clear();

                foreach (var item in dataPhiPhaiThu)
                {
                    series1.Points.Add(new DevExpress.XtraCharts.SeriesPoint(item.HangMuc, item.SoTien));
                }

                // Add series vào chart
                chartControl1.Series.Add(series1);

                // format series labels
                series1.Label.TextPattern = "{A}: {VP:P0}";

                // Format series legend items
                series1.LegendTextPattern = "{A}: {V:n0}";
                
                // Adjust the position of series labels
                ((DevExpress.XtraCharts.PieSeriesLabel)series1.Label).Position = DevExpress.XtraCharts.PieSeriesLabelPosition.TwoColumns;
                 

                // Detext overlapping of series labels.
                ((DevExpress.XtraCharts.PieSeriesLabel)series1.Label).ResolveOverlappingMode = DevExpress.XtraCharts.ResolveOverlappingMode.Default;
                ((DevExpress.XtraCharts.PieSeriesLabel)series1.Label).ResolveOverlappingMinIndent = 5;

                // Access the view-type-specific options of the series.
                DevExpress.XtraCharts.PieSeriesView myView = (DevExpress.XtraCharts.PieSeriesView)series1.View;

                // Specify a data filter to explode points.
                myView.ExplodedPointsFilters.Add(new DevExpress.XtraCharts.SeriesPointFilter(DevExpress.XtraCharts.SeriesPointKey.Value_1, DevExpress.XtraCharts.DataFilterCondition.GreaterThanOrEqual, 9));
                myView.ExplodedPointsFilters.Add(new DevExpress.XtraCharts.SeriesPointFilter(DevExpress.XtraCharts.SeriesPointKey.Argument, DevExpress.XtraCharts.DataFilterCondition.NotEqual, "Others"));
                myView.ExplodeMode = DevExpress.XtraCharts.PieExplodeMode.UseFilters;
                myView.ExplodedDistancePercentage = 5;
                myView.RuntimeExploding = true;

                chartControl1.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;
                chartControl1.Dock = System.Windows.Forms.DockStyle.Fill;
                
                #endregion

                #region Biểu đồ phí đã thu trong tháng

                var dataPhiDaThuTrongThang = (from l in lPhieuThu group new {l} by new {l.LoaiDichVu} into g select new {g.Key.LoaiDichVu, SoTien = g.Sum(_=>_.l.SoTien).GetValueOrDefault()}).ToList();

                chartControl2.DataSource = null;
                chartControl2.Series.Clear();
                chartControl2.RefreshData();

                var series2 = new DevExpress.XtraCharts.Series("Phí đã thu trong tháng", ViewType.Doughnut);
                foreach (var item in dataPhiDaThuTrongThang)
                    series2.Points.Add(new DevExpress.XtraCharts.SeriesPoint(item.LoaiDichVu, item.SoTien));

                //series2.PointOptions.ValueNumericOptions.Format = NumericFormat.Number;
                //series2.PointOptions.ValueNumericOptions.Precision = 2;
                series2.LegendTextPattern = "{A}: {V:n0}";
                //((PieSeriesLabel)series1.Label).PointOptions.Pattern = "{A}: {V}";
                ((DevExpress.XtraCharts.DoughnutSeriesLabel)series2.Label).Position = PieSeriesLabelPosition.TwoColumns;
                ((DevExpress.XtraCharts.DoughnutSeriesLabel)series2.Label).ResolveOverlappingMode = ResolveOverlappingMode.Default;
                //((PieSeriesLabel)series2.Label).PointOptions.ValueNumericOptions.Format = NumericFormat.Percent;
                //((PieSeriesLabel)series2.Label).PointOptions.ValueNumericOptions.Precision = 0;

                //((DoughnutSeriesView)series2.View).ExplodedPoints.Add(series2.Points[0]);
                //((DoughnutSeriesView)series2.View).ExplodedDistancePercentage = 30;

                DevExpress.XtraCharts.DoughnutSeriesLabel label2 = (DevExpress.XtraCharts.DoughnutSeriesLabel)series2.Label;
                label2.TextPattern = "{A}: {VP:P0}";

                //PieSeriesView myView2 = (PieSeriesView)series2.View;

                //myView2.ExplodedPointsFilters.Add(new SeriesPointFilter(SeriesPointKey.Value_1,
                //    DataFilterCondition.GreaterThanOrEqual, 9));
                //myView2.ExplodedPointsFilters.Add(new SeriesPointFilter(SeriesPointKey.Argument,
                //    DataFilterCondition.NotEqual, "Others"));
                //myView2.ExplodeMode = PieExplodeMode.UseFilters;

                //myView2.RuntimeExploding = true;

                chartControl2.Series.Add(series2);
                chartControl2.Dock = System.Windows.Forms.DockStyle.Fill;

                #endregion

                itemNap.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                itemThuTien.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            }
            catch (System.Exception ex)
            {
                Library.DialogBox.Error(ex.Message);
            }
        }

        private void ItemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //khachHang = GetMaKh(itemCustomerText.EditValue.ToString().Trim().ToLower());
            //if (khachHang != null)
            //{

            //    MaKh = khachHang.MaKH;
            //    LoadData();
            //}
            //else Library.DialogBox.Alert("Không tìm thấy khách hàng nào phù hợp.");

            LoadData();
        }

        private Library.tnKhachHang GetMaKh(string text)
        {
            return _db.tnKhachHangs.Where(_ => _.KyHieu.ToLower().Contains(text) | _.DienThoaiKH.ToLower().Contains(text) | _.TenKH.ToLower().Contains(text) | _.CtyTen.ToLower().Contains(text)).FirstOrDefault();
        }

        

        private void itemThuTien_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (MaKh == null) return;
            var khachHang = _db.tnKhachHangs.FirstOrDefault(_ => _.MaKH == MaKh);
            if (khachHang == null) return;

            using (var frm = new LandSoftBuilding.Receivables.frmPayment())
            {
                frm.MaKH = MaKh;
                frm.MaTN = (byte?)khachHang.MaTN;
                frm.ShowDialog();
                if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                {
                    LoadData();
                }
            }
        }

        private void itemKhachHang_EditValueChanged(object sender, System.EventArgs e)
        {
            try
            {
                //DevExpress.XtraEditors.GridLookUpEdit item = sender as DevExpress.XtraEditors.GridLookUpEdit;
                DevExpress.XtraBars.BarEditItem item = sender as DevExpress.XtraBars.BarEditItem;
                if (item == null) return;
                if (item.EditValue == null) return;

                MaKh = (int) item.EditValue;
            }
            catch{}
        }

        private void itemCustomerText_EditValueChanged(object sender, System.EventArgs e)
        {
            try
            {
                string text = itemCustomerText.EditValue.ToString().Trim().ToLower();
                glkKhachHang.DataSource = null;
                MaKh = null;

                var list = (from _ in _db.tnKhachHangs
                                           join mb in _db.mbMatBangs on _.MaKH equals mb.MaKHF1 // into matBang from mb in matBang.DefaultIfEmpty()
                                           join tn in _db.tnToaNhas on _.MaTN equals tn.MaTN
                                           where lToaNha.Contains(_.MaTN.ToString()) & (_.KyHieu.ToLower().Contains(text) | _.DienThoaiKH.ToLower().Contains(text) | _.TenKH.ToLower().Contains(text) | _.CtyTen.ToLower().Contains(text))
                                           select new
                                           {
                                               _.KyHieu,
                                               HoTenKH = _.IsCaNhan == true ? _.HoKH + " " + _.TenKH : _.CtyTen,
                                               MaSoMB = mb != null ? mb.MaSoMB : "",
                                               tn.TenTN,
                                               Name = _.IsCaNhan == true ? _.HoKH + " " + _.TenKH + (mb != null ? " - " + mb.MaSoMB : "") : _.CtyTen + (mb != null ? " - " + mb.MaSoMB : ""),
                                               _.MaKH
                                           }).ToList();
                if (list.Count() > 0)
                {
                    glkKhachHang.DataSource = list;
                    itemKhachHang.EditValue = glkKhachHang.GetKeyValue(0);
                }
            }
            catch { }
        }
    }
}
