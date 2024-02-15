using System;
using System.Linq;
using System.Windows.Forms;
using Library;
using DevExpress.XtraCharts;
using System.Data.Linq.SqlClient;

namespace Building.Asset.BieuDo
{
    public partial class frmBieuDo_CountNhomCongViec : Form
    {
        public frmBieuDo_CountNhomCongViec()
        {
            InitializeComponent();
        }

        private void LoadData()
        {
            var wait = DialogBox.WaitingForm();
            try
            {
                var _MaTN = (byte)itemToaNha.EditValue;
                var _TuNgay = (DateTime?)itemTuNgay .EditValue;
                var _DenNgay = (DateTime?)itemDenNgay.EditValue;


                pieChart.Series.Clear();

                using (var db = new MasterDataContext())
                {
                    var ltSource = (from _ in db.tnycYeuCaus
                        where _.MaTN == _MaTN & SqlMethods.DateDiffDay(_TuNgay, _.NgayYC) >= 0 &
                              SqlMethods.DateDiffDay(_.NgayYC, _DenNgay) >= 0
                        group _ by new {_.tnPhongBan.MaPB, _.tnPhongBan.TenPB}
                        into g
                        select new
                        {
                            NhomCongViec = g.Key.TenPB??"-", Count = g.Count()
                        }).ToList();

                    Series series1 = new Series("", ViewType.Pie);

                    foreach (var l in ltSource)
                    {
                        series1.Points.Add(new SeriesPoint(l.NhomCongViec.ToString(), l.Count));
                        
                    }

                    pieChart.Series.Add(series1);

                    ((PieSeriesLabel)series1.Label).PointOptions.Pattern = "{A}: {V}";
                    ((PieSeriesLabel)series1.Label).Position = PieSeriesLabelPosition.TwoColumns;
                    ((PieSeriesLabel)series1.Label).ResolveOverlappingMode = ResolveOverlappingMode.Default;
                    ((PieSeriesLabel) series1.Label).PointOptions.ValueNumericOptions.Format = NumericFormat.Percent;
                    ((PieSeriesLabel) series1.Label).PointOptions.ValueNumericOptions.Precision = 0;

                    PieSeriesView myView = (PieSeriesView)series1.View;

                    myView.Titles.Add(new SeriesTitle());
                    myView.Titles[0].Text = series1.Name;

                    myView.ExplodedPointsFilters.Add(new SeriesPointFilter(SeriesPointKey.Value_1,
                        DataFilterCondition.GreaterThanOrEqual, 9));
                    myView.ExplodedPointsFilters.Add(new SeriesPointFilter(SeriesPointKey.Argument,
                        DataFilterCondition.NotEqual, "Others"));
                    myView.ExplodeMode = PieExplodeMode.UseFilters;

                    myView.RuntimeExploding = true;

                    pieChart.Dock = DockStyle.Fill;
                    this.Controls.Add(pieChart);

                    var strTitle = string.Format("THỐNG KÊ PHẢN ÁNH THEO NHÓM CÔNG VIỆC TỪ {0:MM/yyyy} ĐẾN {1:MM/yyyy}", _TuNgay, _DenNgay); 
                    if (pieChart.Titles.Count == 0)
                    {

                        ChartTitle chartTitle1 = new ChartTitle();
                        chartTitle1.Text = strTitle;
                        chartTitle1.WordWrap = true;
                        pieChart.Titles.Add(chartTitle1);
                        pieChart.Legend.Visible = true;
                        
                    }
                    else
                    {
                        pieChart.Titles[0].Text = strTitle;
                    }
                }
            }
            catch { }
            finally
            {
                wait.Close();
            }
        }

        private void frmBieuDoTieuThuDien_Load(object sender, EventArgs e)
        {
            lookToaNha.DataSource = Common.TowerList;
            itemToaNha.EditValue = Common.User.MaTN;
            itemTuNgay.EditValue = new DateTime(DateTime.Now.Year, 1, 1);
            itemDenNgay.EditValue = DateTime.Now;
            LoadData();
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void itemToaNha_EditValueChanged(object sender, EventArgs e)
        {
            var db = new MasterDataContext();
            lookToaNha.DataSource = db.tnToaNhas;
        }

        private void lookMatBang_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                //itemMatBang.EditValue = null;
            }
        }

        private void itemPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            pieChart.ShowPrintPreview();
        }
    }
}
