using System;
using System.Linq;
using System.Windows.Forms;
using Library;
using DevExpress.XtraCharts;
using System.Data.Linq.SqlClient;

namespace Building.Asset.BieuDo
{
    public partial class frmBieuDo_TinTucTungToaNha : Form
    {
        public frmBieuDo_TinTucTungToaNha()
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

                lineChart.Series.Clear();

                using (var db = new MasterDataContext())
                {
                    var tenTn = db.tnToaNhas.First(_ => _.MaTN == maTn).TenTN;

                    var ltSource = (from news in db.app_News
                        join tower in db.tnToaNhas on news.TowerId equals tower.MaTN
                        where news.TowerId == maTn & //ltToaNha.Contains(news.TowerId.ToString()) &
                              SqlMethods.DateDiffDay(tuNgay, news.DateCreate) >= 0 &
                              SqlMethods.DateDiffDay(news.DateCreate, denNgay) >= 0
                        group news by new
                        {
                            tower.MaTN,
                            tower.TenTN,
                            Thang = news.DateCreate.Value.Month + "/" + news.DateCreate.Value.Year,
                            news.DateCreate.Value.Year,news.DateCreate.Value.Month
                        }
                        into g
                        orderby g.Key.Year,g.Key.Month
                        select new
                        {
                            g.Key.Thang, Count = g.Count()
                        }).ToList();

                    gridControl1.DataSource = ltSource;


                    Series series = new Series("", ViewType.Line);

                    foreach (var i in ltSource)
                    {
                        series.Points.Add(new SeriesPoint("T" + i.Thang, i.Count));
                    }

                    lineChart.Series.Add(series);

                    lineChart.Dock = DockStyle.Fill;


                    var strTitle = string.Format("TIN TỨC CỦA TÒA NHÀ {0} TỪ {1:MM/yyyy} ĐẾN {2:MM/yyyy}",tenTn, tuNgay,
                        denNgay);
                    if (lineChart.Titles.Count == 0)
                    {

                        ChartTitle chartTitle1 = new ChartTitle();
                        chartTitle1.Text = strTitle;
                        chartTitle1.WordWrap = true;
                        lineChart.Titles.Add(chartTitle1);
                        lineChart.Legend.Visible = true;

                    }
                    else
                    {
                        lineChart.Titles[0].Text = strTitle;
                    }

                    //this.Controls.Add(lineChart);
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
            lineChart.ShowPrintPreview();
        }
    }
}
