using System;
using System.Linq;
using System.Windows.Forms;
using Library;
using DevExpress.XtraCharts;
using System.Data.Linq.SqlClient;
using System.Drawing;

namespace Building.Asset.BieuDo
{
    public partial class frmBieuDoTongPhanAnh : Form
    {
        public frmBieuDoTongPhanAnh()
        {
            InitializeComponent();
        }

        private void LoadData()
        {
            var wait = DialogBox.WaitingForm();
            try
            {
                var _TuNgay = (DateTime?)itemTuNgay .EditValue;
                var _DenNgay = (DateTime?)itemDenNgay.EditValue;

                barChart.Series.Clear();

                using (var db = new MasterDataContext())
                {
                    var ltSource = (from _ in db.tnycYeuCaus
                        where  SqlMethods.DateDiffDay(_TuNgay, _.NgayYC) >= 0 &
                              SqlMethods.DateDiffDay(_.NgayYC, _DenNgay) >= 0
                                    group _ by new { _.tnToaNha.MaTN,_.tnToaNha.TenTN }
                        into g
                        select new
                        {
                            TenTN = g.Key.TenTN??"-",
                            Count = g.Count()
                        }).ToList();

                    var series1 = new Series("", ViewType.Bar);
                    var series2 = new Series("", ViewType.Line);

                    foreach (var l in ltSource)
                    {
                        series1.Points.Add(new SeriesPoint(l.TenTN, l.Count));
                        
                    }

                    barChart.Series.AddRange(new Series[] { series1, series2 });
                    ((BarSeriesLabel)series1.Label).Visible = true;
                    ((BarSeriesLabel)series1.Label).ResolveOverlappingMode = ResolveOverlappingMode.Default;
                    ((BarSeriesLabel)series1.Label).PointOptions.Pattern = "{A}: {V}";

                    ((PointSeriesLabel)series2.Label).Visible = false;
                    ((PointSeriesLabel)series2.Label).ResolveOverlappingMode = ResolveOverlappingMode.Default;

                    var diagram = (XYDiagram)barChart.Diagram;
                    diagram.AxisY.Title.Text = @"Số lượng phản ánh";
                    diagram.AxisY.Title.Visible = true;
                    diagram.AxisX.Visible = true;
                    diagram.AxisY.Title.Font = new Font("Tahoma", 18);

                    var strTitle = string.Format("TỔNG PHẢN ÁNH CỦA CÁC TÒA NHÀ TỪ {0:MM/yyyy} ĐẾN {1:MM/yyyy}", _TuNgay, _DenNgay);
                    if (barChart.Titles.Count == 0)
                    {

                        ChartTitle chartTitle1 = new ChartTitle();
                        chartTitle1.Text = strTitle;
                        chartTitle1.WordWrap = true;
                        barChart.Titles.Add(chartTitle1);
                       barChart.Legend.Visible = true;
                    }
                    else
                    {
                        barChart.Titles[0].Text = strTitle;
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
            barChart.ShowPrintPreview();
        }
    }
}
