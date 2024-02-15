using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Threading;
using Library;
using DevExpress.XtraCharts;
using System.Linq;

namespace DichVu.MatBang.ThongKe
{
    public partial class frmThongKe : DevExpress.XtraEditors.XtraForm
    {
        Thread th1;
        Thread th2;
        public tnNhanVien objnhanvien;
        public int MaTN { get; set; }
        public frmThongKe()
        {
            InitializeComponent();
            TranslateLanguage.TranslateControl(this);
        }

        private void frmThongKe_SizeChanged(object sender, EventArgs e)
        {
            splitContainerControl1.SplitterPosition = splitContainerControl1.Width / 2;
            splitContainerControl2.SplitterPosition = splitContainerControl2.Height / 2;
            splitContainerControl3.SplitterPosition = splitContainerControl3.Height / 2;
        }

        private void frmThongKe_Load(object sender, EventArgs e)
        {
            //th1 = new Thread(LoadCtl1);
            //th2 = new Thread(LoadCtl2);

            //th1.IsBackground = true;
            //th2.IsBackground = true;
            //th1.Start();
            //th2.Start();
            lookupToaNha.DataSource = Common.TowerList;
            ThongKeMatBangTheoLoai();
            ThongKeMatBangTheoTrangThai();
        }

        private delegate void dlgAddItemN();
        private void LoadCtl1()
        {
            Library.ThongKeCtl.tkTrangThaiTS ctl = new Library.ThongKeCtl.tkTrangThaiTS(objnhanvien);
            ctl.Dock = DockStyle.Fill;

            if (pnlMatBangTheoTT.InvokeRequired)
            {
                Invoke(new dlgAddItemN(LoadCtl1));
            }
            else
            {
                pnlMatBangTheoTT.Controls.Add(ctl);
            }
        }
        private void LoadCtl2()
        {
            Library.ThongKeCtl.ctlTKMatBangTheoLoaiMB ctl2 = new Library.ThongKeCtl.ctlTKMatBangTheoLoaiMB(objnhanvien);
            ctl2.Dock = DockStyle.Fill;

            if (pnlMBTheoLoai.InvokeRequired)
            {
                Invoke(new dlgAddItemN(LoadCtl2));
            }
            else
            {
                pnlMBTheoLoai.Controls.Add(ctl2);
            }
        }

        private void frmThongKe_FormClosed(object sender, FormClosedEventArgs e)
        {
            //th1.Abort();
            //th2.Abort();


        }

        private void ThongKeMatBangTheoLoai()
        {
            var wait = DialogBox.WaitingForm();
            try
            {

                //chartTheoLoai.Series.Clear();
                using (var db = new MasterDataContext())
                {
                    var ltSource = (from mb in db.mbMatBangs
                                    join lmb in db.mbLoaiMatBangs on mb.MaLMB equals lmb.MaLMB
                                    where mb.MaTN == MaTN & mb.MaLMB != null
                                    group mb by new { lmb.TenLMB } into gr
                                    select new
                                    {
                                        TenLoaiMB = gr.Key.TenLMB,
                                        SoLuongMB = gr.Count()
                                    }).ToList();

                    Series series1 = new Series("", ViewType.Bar);
                    Series series2 = new Series("", ViewType.Line);
                    // Add points to the series.
                    foreach (var l in ltSource)
                    {
                        series1.Points.Add(new SeriesPoint(l.TenLoaiMB.ToString(), l.SoLuongMB));
                        //series2.Points.Add(new SeriesPoint(l.TenLoaiMB.ToString(), l.SoLuongMB));
                    }

                    // Add both series to the chart.
                    chartTheoLoai.Series.AddRange(new Series[] { series1, series2 });


                    /// Access labels of the first series.
                    ((BarSeriesLabel)series1.Label).Visible = true;
                    ((BarSeriesLabel)series1.Label).ResolveOverlappingMode = ResolveOverlappingMode.Default;
                    ((BarSeriesLabel)series1.Label).PointOptions.Pattern = "{A}: {V}";


                    ((PointSeriesLabel)series2.Label).Visible = false;
                    ((PointSeriesLabel)series2.Label).ResolveOverlappingMode = ResolveOverlappingMode.Default;

                    // Customize the view-type-specific properties of the series.
                    BarSeriesView myView = (BarSeriesView)series1.View;
                    myView.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))));
                    myView.FillStyle.FillMode = DevExpress.XtraCharts.FillMode.Solid;

                    //myView.Transparency = 50;

                    // Add a title to the chart and hide the legend.
                    var strTitle = "Thống kê mặt bằng theo loại";
                    if (chartTheoLoai.Titles.Count == 0)
                    {

                        ChartTitle chartTitle1 = new ChartTitle();
                        chartTitle1.Text = strTitle;
                        chartTitle1.WordWrap = true;
                        chartTitle1.Font = new System.Drawing.Font("Tahoma", 16F);
                        chartTitle1.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))));
                        chartTheoLoai.Titles.Add(chartTitle1);

                    }
                    else
                    {
                        chartTheoLoai.Titles[0].Text = strTitle;
                        chartTheoLoai.Titles[0].Font = new System.Drawing.Font("Tahoma", 16F);
                        chartTheoLoai.Titles[0].TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))));
                    }

                    chartTheoLoai.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
                    // Add the chart to the form.
                    // chartControl1.Series[0].PointOptions.ValueNumericOptions.Format = NumericFormat.Number;
                    // chartControl1.Series[0].PointOptions.ValueNumericOptions.Precision = 0;
                }
            }
            catch { }
            finally
            {
                wait.Close();
            }
        }


        private void ThongKeMatBangTheoTrangThai()
        {
            var wait = DialogBox.WaitingForm();
            try
            {

                ///chartTheoTrangThai.Series.Clear();
                using (var db = new MasterDataContext())
                {
                    var ltSource = (from mb in db.mbMatBangs
                                    join tt in db.mbTrangThais on mb.MaTT equals tt.MaTT
                                    where mb.MaTN == MaTN & mb.MaLMB != null
                                    group mb by new { tt.TenTT } into gr
                                    select new
                                    {
                                        TenTT = gr.Key.TenTT,
                                        SoLuongMB = gr.Count()
                                    }).ToList();

                    Series series1 = new Series("", ViewType.Pie3D);
                    Series series2 = new Series("", ViewType.Line);

                    // Add points to the series.
                    foreach (var l in ltSource)
                    {
                        series1.Points.Add(new SeriesPoint(l.TenTT.ToString(), l.SoLuongMB));
                        //series2.Points.Add(new SeriesPoint(l.Thang.ToString(), l.SoTieuThu));
                    }

                    // Add both series to the chart.
                    chartTheoTrangThai.Series.AddRange(new Series[] { series1, series2 });

                    /// Access labels of the first series.
                    ((Pie3DSeriesLabel)series1.Label).Visible = true;
                    ((Pie3DSeriesLabel)series1.Label).ResolveOverlappingMode = ResolveOverlappingMode.Default;
                    ((Pie3DSeriesLabel)series1.Label).PointOptions.Pattern = "{A}: {V}";
                    ((Pie3DSeriesLabel)series1.Label).Position = PieSeriesLabelPosition.TwoColumns;
                    ((Pie3DSeriesLabel)series1.Label).PointOptions.ArgumentNumericOptions.Precision = 0;
                    ((Pie3DSeriesLabel)series1.Label).PointOptions.ValueNumericOptions.Format = DevExpress.XtraCharts.NumericFormat.Percent;
                    ((Pie3DSeriesLabel)series1.Label).PointOptions.ValueNumericOptions.Precision = 1;
                    ((Pie3DSeriesLabel)series1.Label).PointOptions.PointView = PointView.ArgumentAndValues;

                    ((PointSeriesLabel)series2.Label).Visible = false;
                    ((PointSeriesLabel)series2.Label).ResolveOverlappingMode = ResolveOverlappingMode.Default;

                    // Customize the view-type-specific properties of the series.
                    Pie3DSeriesView myView = (Pie3DSeriesView)series1.View;
                    //myView.Transparency = 50;

                    // Add a title to the chart and hide the legend.
                    var strTitle = "Thống kê mặt bằng theo trạng thái";
                    if (chartTheoTrangThai.Titles.Count == 0)
                    {

                        ChartTitle chartTitle1 = new ChartTitle();
                        chartTitle1.Text = strTitle;
                        chartTitle1.WordWrap = true;
                        chartTitle1.Font = new System.Drawing.Font("Tahoma", 16F);
                        chartTitle1.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))));
                        chartTheoTrangThai.Titles.Add(chartTitle1);

                    }
                    else
                    {
                        chartTheoTrangThai.Titles[0].Text = strTitle;
                        chartTheoTrangThai.Titles[0].Font = new System.Drawing.Font("Tahoma", 16F);
                        chartTheoTrangThai.Titles[0].TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))));
                    }

                    // Add the chart to the form.
                    // chartControl1.Series[0].PointOptions.ValueNumericOptions.Format = NumericFormat.Number;
                    // chartControl1.Series[0].PointOptions.ValueNumericOptions.Precision = 0;
                }
            }
            catch { }
            finally
            {
                wait.Close();
            }
        }


        private void lookupToaNha_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ThongKeMatBangTheoLoai();
            ThongKeMatBangTheoTrangThai();
        }
    }
}