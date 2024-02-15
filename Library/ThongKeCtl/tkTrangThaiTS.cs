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
using Library;

namespace Library.ThongKeCtl
{
    public partial class tkTrangThaiTS : DevExpress.XtraEditors.XtraUserControl
    {
        tnNhanVien objnhanvien;
        public tkTrangThaiTS(tnNhanVien obj)
        {
            InitializeComponent();
            objnhanvien = obj;
            LoadData();
        }
        
        private void LoadData()
        {
            ChartTrangThaiTS.Series.Clear();
            Series sr = new Series("Thống kê mặt bằng", ViewType.Pie3D);
            
            ChartTrangThaiTS.AppearanceName = "Chameleon";
            sr.DataSource = GetData();
            sr.ArgumentScaleType = ScaleType.Qualitative;
            sr.ArgumentDataMember = "TrangThai";
            sr.ValueScaleType = ScaleType.Numerical;
            sr.ValueDataMembers.AddRange(new string[] { "SoLuong" });
            sr.LegendText = "Số lượng mặt bằng";
            sr.ShowInLegend = true;
            sr.LegendPointOptions.ValueNumericOptions.Format = NumericFormat.Percent;
            sr.LegendPointOptions.ValueNumericOptions.Precision = 1;

            // Access labels of the first series.
            sr.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            ((Pie3DSeriesLabel)sr.Label).ResolveOverlappingMode = ResolveOverlappingMode.Default;
            ((Pie3DSeriesLabel)sr.Label).Position = PieSeriesLabelPosition.Inside;

            // Access the series options.
            sr.LegendPointOptions.PointView = PointView.ArgumentAndValues;

            // Customize the view-type-specific properties of the series.
            Pie3DSeriesView pie3DView = (Pie3DSeriesView)sr.View;
            pie3DView.Depth = 5;
            pie3DView.SizeAsPercentage = 100;
            pie3DView.ExplodeMode = PieExplodeMode.All;

            // Adjust the point options of the series label.
            Pie3DSeriesLabel label = (Pie3DSeriesLabel)sr.Label;
            label.PointOptions.PointView = PointView.Values;
            label.PointOptions.ValueNumericOptions.Format = NumericFormat.Percent;
            label.PointOptions.ValueNumericOptions.Precision = 1;
            label.Position = PieSeriesLabelPosition.Outside;

            //Chart titles
            ChartTitle title = new ChartTitle();
            title.Text = "Thống kê mặt bằng theo trạng thái";
            title.Alignment = StringAlignment.Center;
            title.Dock = ChartTitleDockStyle.Top;
            title.Antialiasing = true;
            title.Font = new Font("Tahoma", 14, FontStyle.Bold);
            title.TextColor = Color.BlueViolet;
            title.Indent = 10;

            ChartTrangThaiTS.Titles.Add(title);
            ChartTrangThaiTS.Series.Add(sr);
        }
        private List<TrangThaiTaiSan> GetData()
        {
            List<TrangThaiTaiSan> lstTTTS = new List<TrangThaiTaiSan>();
            using (MasterDataContext db = new MasterDataContext())
            {
                var lstTT = db.mbTrangThais.ToList();
                foreach (var item in lstTT)
                {
                    if (objnhanvien.IsSuperAdmin.Value)
                    {
                        TrangThaiTaiSan objttts = new TrangThaiTaiSan()
                        {
                            TrangThai = item.TenTT,
                            SoLuong = db.mbMatBangs
                            .Where(p => p.MaTT == item.MaTT)
                            .Count()
                        };
                        lstTTTS.Add(objttts);
                    }
                    else
                    {
                        TrangThaiTaiSan objttts = new TrangThaiTaiSan()
                        {
                            TrangThai = item.TenTT,
                            SoLuong = db.mbMatBangs
                            .Where(p => p.MaTT == item.MaTT & p.mbTangLau.mbKhoiNha.tnToaNha.MaTN == objnhanvien.MaTN)
                            .Count()
                        };
                        lstTTTS.Add(objttts);
                    }
                }
                return lstTTTS;
            }
        }

        public class TrangThaiTaiSan
        {
            public string TrangThai { get; set; }
            public int SoLuong { get; set; }
            public TrangThaiTaiSan(string tt, int sl)
            {
                TrangThai = tt;
                SoLuong = sl;
            }

            public TrangThaiTaiSan()
            {

            }
        }
            
    }
}
