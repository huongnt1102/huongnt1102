using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using System.Data.Linq.SqlClient;
using DevExpress.XtraCharts;
using System.Drawing.Imaging;

namespace Library.ThongKeCtl
{
    public partial class ctlTaiSanSuDung : DevExpress.XtraEditors.XtraUserControl
    {
        tnNhanVien objnhanvien;
        public ctlTaiSanSuDung(tnNhanVien obj)
        {
            InitializeComponent();
            objnhanvien = obj;
            LoadData();
        }


        private void LoadData()
        {
            chartControl1.Titles.Clear();
            chartControl1.Series.Clear();
            Series sr = new Series("TaiSan_TT", ViewType.Pie3D);
            chartControl1.AppearanceName = "Chameleon";
            sr.DataSource = LayDSTaiSanDangDung();
            sr.ArgumentScaleType = ScaleType.Qualitative;
            sr.ArgumentDataMember = "TrangThai";
            sr.ValueScaleType = ScaleType.Numerical;
            sr.ValueDataMembers.AddRange(new string[] { "SoLuong" });
            sr.LegendText = "Số lượng tài sản";
            sr.ShowInLegend = true;

            sr.LegendPointOptions.ValueNumericOptions.Format = NumericFormat.Percent;
            sr.LegendPointOptions.ValueNumericOptions.Precision = 1;

            // Access labels of the first series.
            sr.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            ((Pie3DSeriesLabel)sr.Label).ResolveOverlappingMode = ResolveOverlappingMode.Default;

            // Access the series options.
            sr.LegendPointOptions.PointView = PointView.ArgumentAndValues;

            // Customize the view-type-specific properties of the series.
            Pie3DSeriesView pie3DView = (Pie3DSeriesView)sr.View;
            pie3DView.Depth = 5;
            pie3DView.SizeAsPercentage = 100;
            pie3DView.ExplodeMode = PieExplodeMode.All;

            // Adjust the point options of the series label.
            Pie3DSeriesLabel label = (Pie3DSeriesLabel)sr.Label;
            label.PointOptions.PointView = PointView.ArgumentAndValues;
            label.PointOptions.ValueNumericOptions.Format = NumericFormat.Percent;
            label.PointOptions.ValueNumericOptions.Precision = 1;
            label.Position = PieSeriesLabelPosition.Inside;

            //Chart titles
            ChartTitle title = new ChartTitle();
            title.Text = "Thống kê tài sản";
            title.Alignment = StringAlignment.Center;
            title.Dock = ChartTitleDockStyle.Top;
            title.Antialiasing = true;
            title.Font = new Font("Tahoma", 14, FontStyle.Bold);
            title.TextColor = Color.BlueViolet;
            title.Indent = 10;

            chartControl1.Titles.Add(title);
            chartControl1.Series.Add(sr);
        }

        private List<ThongKe> LayDSTaiSanDangDung()
        {
            List<ThongKe> lstThongKe = new List<ThongKe>();

            using (MasterDataContext db = new MasterDataContext())
            {
                if (objnhanvien.IsSuperAdmin.Value)
                {
                    ThongKe objtkdsd = new ThongKe()
                    {
                        TrangThai = "Đang sử dụng",
                        SoLuong = db.tsTaiSanSuDungs
                        .Where(p => p.MaMB != null)
                        .Count()
                    };
                    ThongKe objtkcdsd = new ThongKe()
                    {
                        TrangThai = "Chưa sử dụng",
                        SoLuong = db.tsTaiSanSuDungs
                        .Where(p => p.MaMB == null)
                        .Count()
                    };
                    lstThongKe.Add(objtkdsd);
                    lstThongKe.Add(objtkcdsd);
                }
                else
                {
                    ThongKe objtkdsd = new ThongKe()
                    {
                        TrangThai = "Đang sử dụng",
                        SoLuong = db.tsTaiSanSuDungs
                        .Where(p => p.MaMB != null & p.mbMatBang.mbTangLau.mbKhoiNha.tnToaNha.MaTN == objnhanvien.MaTN)
                        .Count()
                    };
                    ThongKe objtkcdsd = new ThongKe()
                    {
                        TrangThai = "Chưa sử dụng",
                        SoLuong = db.tsTaiSanSuDungs
                        .Where(p => p.MaMB == null & p.mbMatBang.mbTangLau.mbKhoiNha.tnToaNha.MaTN == objnhanvien.MaTN)
                        .Count()
                    };
                    lstThongKe.Add(objtkdsd);
                    lstThongKe.Add(objtkcdsd);
                }
                return lstThongKe;
            }
        }

        public class ThongKe
        {
            public string TrangThai { get; set; }
            public int SoLuong { get; set; }
            public ThongKe(string tt, int sl)
            {
                TrangThai = tt;
                SoLuong = sl;
            }

            public ThongKe() { }
        }

        private void btnNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void itemImg_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Image File (*.jpeg)|*.jpeg";
            saveFileDialog.Title = "Save an Image File";
            saveFileDialog.ShowDialog();

            if (saveFileDialog.FileName != "")
            {
                System.IO.FileStream fs = (System.IO.FileStream)saveFileDialog.OpenFile();
                chartControl1.ExportToImage(fs, ImageFormat.Jpeg);
                fs.Close();
            }
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Pdf File (*.pdf)|*.pdf";
            saveFileDialog.Title = "Save an pdf File";
            saveFileDialog.ShowDialog();

            if (saveFileDialog.FileName != "")
            {
                System.IO.FileStream fs = (System.IO.FileStream)saveFileDialog.OpenFile();
                chartControl1.ExportToPdf(fs);
                fs.Close();
            }
        }
    }
}
