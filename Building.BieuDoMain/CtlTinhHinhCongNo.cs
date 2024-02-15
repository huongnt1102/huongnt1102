using System;
using System.Data.Linq.SqlClient;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraCharts;
using DevExpress.XtraEditors;
using Library;

namespace Building.BieuDoMain
{
    public partial class CtlTinhHinhCongNo : DevExpress.XtraEditors.XtraUserControl
    {
        public CtlTinhHinhCongNo()
        {
            InitializeComponent();
        }

        private void CtlTinhHinhCongNo_Load(object sender, EventArgs e)
        {
            if (Common.User == null) return;
            ckCbxToaNha.DataSource = Common.TowerList;
            itemTn.EditValue = Common.User.MaTN;

            var objKbc = new KyBaoCao();
            foreach (var kbc in objKbc.Source) cmbKbc.Items.Add(kbc);
            itemKyBaoCao.EditValue = objKbc.Source[3];
            SetDate(3);
            LoadData();
        }
        private void SetDate(int index)
        {
            var objKbc = new KyBaoCao {Index = index};
            objKbc.SetToDate();
            itemTuNgay.EditValue = objKbc.DateFrom;
            itemDenNgay.EditValue = objKbc.DateTo;
        }

        private void LoadData()
        {
            try
            {
                const string title = "Tình hình công nợ dịch vụ";
                //ChartTitle chartTitle1 = new ChartTitle();
                //chartControl1.Titles.Add(chartTitle1);
                //chartTitle1.Text = "Just a Very Lengthy Title for Such a Small Chart";

                //Library.PhanQuyenBieuDo.SaveControl(GetType().Namespace + "." + Name, title);
                Library.PhanQuyenBieuDo.SaveControl(GetType().Namespace + "." + Name, chartControl1.Titles[0].Lines[0].ToString(), GetType().Namespace + ".dll");

                var strToaNha = (itemTn.EditValue ?? "").ToString().TrimEnd(',').TrimStart(',').Replace(" ", "");
                var ltToaNha = strToaNha.Split(',');
                if (strToaNha == "") return;
                var tuNgay = (DateTime?)itemTuNgay.EditValue;
                var denNgay = (DateTime?)itemDenNgay.EditValue;
                using (var db = new MasterDataContext())
                {
                    var obj = (from hd in db.dvHoaDons
                        join ldv in db.dvLoaiDichVus on hd.MaLDV equals ldv.ID
                        join tn in db.tnToaNhas on hd.MaTN equals tn.MaTN
                        where ltToaNha.Contains(hd.MaTN.ToString()) & hd.IsDuyet == true & SqlMethods.DateDiffDay(hd.NgayTT, denNgay) > 0
                        group hd by new {hd.MaLDV, ldv.TenLDV}
                        into g
                        select new
                        {
                            g.Key.MaLDV, g.Key.TenLDV,
                            ConNo = g.Sum(_ =>
                                _.PhaiThu - (from ct in db.ptChiTietPhieuThus
                                    join pt in db.ptPhieuThus on ct.MaPT equals pt.ID
                                    where ct.TableName == "dvHoaDon" & ct.LinkID == pt.ID &
                                          SqlMethods.DateDiffDay(pt.NgayThu, denNgay) > 0
                                    select ct.SoTien).Sum().GetValueOrDefault())
                        }).ToList();

                    chartControl1.DataSource = null;
                    chartControl1.Series.Clear();
                    chartControl1.RefreshData();

                    Series series1 = new Series(title, ViewType.Pie);
                    series1.Points.Clear();

                    foreach (var item in obj)
                    {
                        series1.Points.Add(new SeriesPoint(item.TenLDV, new double[] { Convert.ToDouble(item.ConNo) }));
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

                    myView.ExplodedPointsFilters.Add(new SeriesPointFilter(SeriesPointKey.Argument,
                        DataFilterCondition.NotEqual, "MinValue"));
                    myView.ExplodeMode = PieExplodeMode.UseFilters;
                    myView.ExplodedDistancePercentage = 3;
                    myView.RuntimeExploding = true;

                    chartControl1.Series.Add(series1);
                    chartControl1.Dock = DockStyle.Fill;

                }
            }
            catch (Exception e)
            {
                //
            }
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void cmbKbc_EditValueChanged(object sender, EventArgs e)
        {
            SetDate(((ComboBoxEdit)sender).SelectedIndex);
        }
    }
}
