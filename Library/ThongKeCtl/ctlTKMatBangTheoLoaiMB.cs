using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using DevExpress.XtraCharts;

namespace Library.ThongKeCtl
{
    public partial class ctlTKMatBangTheoLoaiMB : DevExpress.XtraEditors.XtraUserControl
    {
        tnNhanVien objnhanvien;
        public ctlTKMatBangTheoLoaiMB(tnNhanVien obj)
        {
            InitializeComponent();
            objnhanvien = obj;
            LoadData();
        }

        private void LoadData()
        {
            chartMatBangTheoLoai.Series.Clear();
            Series sr = new Series("Thống kê mặt bằng", ViewType.Bar);

            chartMatBangTheoLoai.AppearanceName = "Chameleon";
            sr.DataSource = GetDanhSach();
            sr.ArgumentScaleType = ScaleType.Qualitative;
            sr.ArgumentDataMember = "LoaiMB";
            
            sr.ValueScaleType = ScaleType.Numerical;
            sr.ValueDataMembers.AddRange(new string[] { "SoLuong" });
            sr.LegendText = "Số lượng mặt bằng";
            sr.ShowInLegend = true;
            sr.LegendPointOptions.ValueNumericOptions.Format = NumericFormat.Percent;
            sr.LegendPointOptions.ValueNumericOptions.Precision = 0;

            // Access labels of the first series.
            sr.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            ((BarSeriesLabel)sr.Label).ResolveOverlappingMode = ResolveOverlappingMode.Default;

            // Access the series options.
            sr.LegendPointOptions.PointView = PointView.ArgumentAndValues;

            // Adjust the point options of the series label.
            BarSeriesLabel label = (BarSeriesLabel)sr.Label;
            label.PointOptions.PointView = PointView.ArgumentAndValues;
            label.PointOptions.ValueNumericOptions.Format = NumericFormat.Number;
            label.PointOptions.ValueNumericOptions.Precision = 0;

            //Chart titles
            ChartTitle title = new ChartTitle();
            title.Text = "Thống kê mặt bằng theo loại";
            title.Alignment = StringAlignment.Center;
            title.Dock = ChartTitleDockStyle.Top;
            title.Antialiasing = true;
            title.Font = new Font("Tahoma", 14, FontStyle.Bold);
            title.TextColor = Color.BlueViolet;
            title.Indent = 10;

            chartMatBangTheoLoai.Titles.Add(title);
            chartMatBangTheoLoai.Series.Add(sr);

            XYDiagram diagram = (XYDiagram)chartMatBangTheoLoai.Diagram;
            // Customize the X-axis labels' appearance.
            diagram.AxisX.Label.EnableAntialiasing = DevExpress.Utils.DefaultBoolean.False;
            diagram.AxisX.Label.Staggered = true;
        }

        private List<MatBangTheoLoai> GetDanhSach()
        {
            List<MatBangTheoLoai> lstMBTL = new List<MatBangTheoLoai>();
            using (MasterDataContext db = new MasterDataContext())
            {
                var lstLoaiMB = db.mbLoaiMatBangs.ToList();
                foreach (var item in lstLoaiMB)
                {
                    if (objnhanvien.IsSuperAdmin.Value)
                    {
                        MatBangTheoLoai objmb = new MatBangTheoLoai()
                        {
                            LoaiMB = item.TenLMB,
                            SoLuong = db.mbMatBangs.Where(p => p.MaLMB == item.MaLMB).Count()
                        };
                        lstMBTL.Add(objmb);
                    }
                    else
                    {
                        MatBangTheoLoai objmb = new MatBangTheoLoai()
                        {
                            LoaiMB = item.TenLMB,
                            SoLuong = db.mbMatBangs.Where(p => p.MaLMB == item.MaLMB & p.mbTangLau.mbKhoiNha.tnToaNha.MaTN==objnhanvien.MaTN)
                            .Count()
                        };
                        lstMBTL.Add(objmb);
                    }
                }
                return lstMBTL;
            }
        }

        public class MatBangTheoLoai
        {
            public string LoaiMB { get; set; }
            public int SoLuong { get; set; }
            public MatBangTheoLoai() { }
            public MatBangTheoLoai(string loai, int sl)
            {
                LoaiMB = loai;
                SoLuong = sl;
            }
        }

    }
}
