using System;
using System.Linq;
using System.Windows.Forms;
using Library;
using DevExpress.XtraCharts;
using System.Data.Linq.SqlClient;
using System.Drawing;

namespace Building.Asset.BieuDo
{
    public partial class frmBieuDo_ThongKeSuDungTungToaNha : Form
    {
        public frmBieuDo_ThongKeSuDungTungToaNha()
        {
            InitializeComponent();
        }

        private void LoadData()
        {
            var wait = DialogBox.WaitingForm();
            try
            {
                var maTn = (byte) itemToaNha.EditValue;
                var tuNgay = (DateTime?) itemTuNgay.EditValue;
                var denNgay = (DateTime?) itemDenNgay.EditValue;

                //var strToaNha = (itemTN.EditValue ?? "").ToString().TrimEnd(',').TrimStart(',').Replace(" ", "");
                //var ltToaNha = strToaNha.Split(',');

                barChart.Series.Clear();

                using (var db = new MasterDataContext())
                {
                    var tenTn = db.tnToaNhas.First(_ => _.MaTN == maTn).TenTN;

                    var ltSource = (from news in db.app_Residents
                        join rt in db.app_ResidentTowers on news.Id equals rt.ResidentId
                        join tower in db.tnToaNhas on rt.TowerId equals tower.MaTN
                        where tower.MaTN == maTn & //ltToaNha.Contains(news.TowerId.ToString()) &
                              SqlMethods.DateDiffDay(tuNgay, news.DateOfCreate) >= 0 &
                              SqlMethods.DateDiffDay(news.DateOfCreate, denNgay) >= 0 &
                              news.IsResident == true
                        group new {news} by new
                        {
                            Thang = news.DateOfCreate.Value.Month + "/" + news.DateOfCreate.Value.Year,
                            news.DateOfCreate.Value.Month, news.DateOfCreate.Value.Year
                        }
                        into g
                        orderby g.Key.Year, g.Key.Month
                        select new
                        {
                            g.Key.Thang, Login = g.Sum(_ => _.news.IsLogin != null ? 1 : 0), Count = g.Count(),
                            g.Key.Month
                        }).ToList();

                    var series1 = new Series("Đăng ký", ViewType.Bar);
                    var series2 = new Series("Sử dụng", ViewType.Bar);
                    foreach (var i in ltSource)
                    {
                        series1.Points.Add(new SeriesPoint("T" + i.Thang, i.Count));
                        series2.Points.Add(new SeriesPoint("T" + i.Thang, i.Login));
                    }

                    gridControl1.DataSource = ltSource;

                    barChart.Series.AddRange(new Series[] { series1, series2 });
                    ((BarSeriesLabel)series1.Label).Visible = true;
                    ((BarSeriesLabel)series1.Label).ResolveOverlappingMode = ResolveOverlappingMode.Default;
                    ((BarSeriesLabel)series1.Label).PointOptions.Pattern = "{V}";//"{A}: {V}"

                    ((BarSeriesLabel)series2.Label).Visible = true;
                    ((BarSeriesLabel)series2.Label).ResolveOverlappingMode = ResolveOverlappingMode.Default;
                    ((BarSeriesLabel)series2.Label).PointOptions.Pattern = "{V}";

                    var diagram = (XYDiagram)barChart.Diagram;
                    diagram.AxisY.Title.Text = @"";
                    diagram.AxisY.Title.Visible = true;
                    diagram.AxisX.Visible = true;
                    diagram.AxisY.Title.Font = new Font("Tahoma", 18);

                    var strTitle = string.Format("THỐNG KÊ ĐĂNG KÝ/ SỬ DỤNG APP CỦA {0} \n TỪ {1:MM/yyyy} ĐẾN {2:MM/yyyy}",tenTn, tuNgay, denNgay);
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
                        //ChartTitle chartTitle1 = new ChartTitle();
                        //chartTitle1.Text = strTitle;
                        //chartTitle1.WordWrap = true;
                        //barChart.Titles.Add(chartTitle1);
                        barChart.Titles[0].Text = strTitle;
                        
                    }

                    //this.Controls.Add(barChart);
                }
            }
            catch
            {
            }
            finally
            {
                wait.Close();
            }
        }

        private void frmBieuDoTieuThuDien_Load(object sender, EventArgs e)
        {
            lookToaNha.DataSource = Common.TowerList;
            itemToaNha.EditValue = Common.User.MaTN;
            var db = new MasterDataContext();

            //cmbToaNha.DataSource = Common.TowerList;
            //cmbToaNha.DataSource = db.tnToaNhas;

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
