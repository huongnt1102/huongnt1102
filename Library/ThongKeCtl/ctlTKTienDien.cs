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
using System.Data.Linq.SqlClient;
using System.Drawing.Imaging;

namespace Library.ThongKeCtl
{
    public partial class ctlTKTienDien : DevExpress.XtraEditors.XtraUserControl
    {
        tnNhanVien objnhanvien;
        public ctlTKTienDien(tnNhanVien obj)
        {
            InitializeComponent();
            objnhanvien = obj;
            LoadData();
            LoadKBC();
        }

        void LoadKBC()
        {
            KyBaoCao objKBC = new KyBaoCao();
            foreach (string str in objKBC.Source)
            {
                repositoryItemComboBoxKyBaoCao.Items.Add(str);
            }
            itemKyBC.EditValue = objKBC.Source[7];
            SetDate(7);
        }

        void SetDate(int index)
        {
            var objKBC = new KyBaoCao()
            {
                Index = index
            };
            objKBC.SetToDate();

            itemTuNgay.EditValueChanged -= new EventHandler(itemTuNgay_EditValueChanged);
            itemTuNgay.EditValue = objKBC.DateFrom;
            itemDenNgay.EditValue = objKBC.DateTo;
            itemTuNgay.EditValueChanged += new EventHandler(itemTuNgay_EditValueChanged);
        }

        private void LoadData()
        {
            if (itemTuNgay.EditValue != null & itemDenNgay.EditValue != null)
            {
                var tuNgay = (DateTime)itemTuNgay.EditValue;
                var denNgay = (DateTime)itemDenNgay.EditValue;

                chartTienDien.Series.Clear();
                chartTienDien.Titles.Clear();
                Series sr = new Series("Thống kê thanh toán tiền điện", ViewType.Pie3D);

                chartTienDien.AppearanceName = "Chameleon";
                sr.DataSource = GetTienDien(tuNgay,denNgay);
                sr.ArgumentScaleType = ScaleType.Qualitative;
                sr.ArgumentDataMember = "DaTT";
                sr.ValueScaleType = ScaleType.Numerical;
                sr.ValueDataMembers.AddRange(new string[] { "ThanhTien" });
                sr.LegendText = "VNĐ";
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
                label.PointOptions.PointView = PointView.Values;
                label.PointOptions.ValueNumericOptions.Format = NumericFormat.Percent;
                label.PointOptions.ValueNumericOptions.Precision = 1;
                label.Position = PieSeriesLabelPosition.Inside;

                //Chart titles
                ChartTitle title = new ChartTitle();
                title.Text = "Thống kê tình trạng thanh toán tiền điện";
                title.Alignment = StringAlignment.Center;
                title.Dock = ChartTitleDockStyle.Top;
                title.Antialiasing = true;
                title.Font = new Font("Tahoma", 14, FontStyle.Bold);
                title.TextColor = Color.BlueViolet;
                title.Indent = 10;

                chartTienDien.Titles.Add(title);
                chartTienDien.Series.Add(sr);
            }
        }

        private List<TienDien> GetTienDien(DateTime TuNgay, DateTime DenNgay)
        {
            List<TienDien> lsttd = new List<TienDien>();
            using (MasterDataContext db = new MasterDataContext())
            {
                if (objnhanvien.IsSuperAdmin.Value)
                {
                    TienDien tddtt = new TienDien()
                    {
                        DaTT = "Đã thanh toán",
                        ThanhTien = db.dvdnDiens
                        .Where(p => p.DaTT.Value & SqlMethods.DateDiffDay(TuNgay, p.NgayNhap.Value) >= 0
                            & SqlMethods.DateDiffDay(p.NgayNhap.Value, DenNgay) >= 0)
                        .Sum(p => p.DaThanhToan) ?? 0
                    };
                    TienDien tdctt = new TienDien()
                    {
                        DaTT = "Chưa thanh toán",
                        ThanhTien = db.dvdnDiens
                        .Where(p => !p.DaTT.Value & SqlMethods.DateDiffDay(TuNgay, p.NgayNhap.Value) >= 0
                            & SqlMethods.DateDiffDay(p.NgayNhap.Value, DenNgay) >= 0)
                        .Sum(p => p.TienDien) ?? 0
                    };
                    lsttd.Add(tddtt);
                    lsttd.Add(tdctt);
                }
                else
                {
                    TienDien tddtt = new TienDien()
                    {
                        DaTT = "Đã thanh toán",
                        ThanhTien = db.dvdnDiens
                        .Where(p => p.DaTT.Value & SqlMethods.DateDiffDay(TuNgay, p.NgayNhap.Value) >= 0
                            & SqlMethods.DateDiffDay(p.NgayNhap.Value, DenNgay) >= 0
                            & p.mbMatBang.mbTangLau.mbKhoiNha.tnToaNha.MaTN == objnhanvien.MaTN)
                        .Sum(p => p.DaThanhToan) ?? 0
                    };
                    TienDien tdctt = new TienDien()
                    {
                        DaTT = "Chưa thanh toán",
                        ThanhTien = db.dvdnDiens
                        .Where(p => !p.DaTT.Value & SqlMethods.DateDiffDay(TuNgay, p.NgayNhap.Value) >= 0
                            & SqlMethods.DateDiffDay(p.NgayNhap.Value, DenNgay) >= 0
                            & p.mbMatBang.mbTangLau.mbKhoiNha.tnToaNha.MaTN == objnhanvien.MaTN)
                        .Sum(p => p.TienDien) ?? 0
                    };
                    lsttd.Add(tddtt);
                    lsttd.Add(tdctt);
                }
                return lsttd;
            }
        }

        private class TienDien
        {
            public string DaTT { get; set; }
            public decimal ThanhTien { get; set; }

            public TienDien() { }
            public TienDien(string datt, decimal tt)
            {
                DaTT = datt;
                ThanhTien = tt;
            }
        }

        private void itemTuNgay_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void itemDenNgay_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void repositoryItemComboBoxKyBaoCao_EditValueChanged(object sender, EventArgs e)
        {
            SetDate((sender as ComboBoxEdit).SelectedIndex);
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
                chartTienDien.ExportToImage(fs, ImageFormat.Jpeg);
                fs.Close();
            }
        }

        private void itemPdf_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Pdf File (*.pdf)|*.pdf";
            saveFileDialog.Title = "Save an pdf File";
            saveFileDialog.ShowDialog();

            if (saveFileDialog.FileName != "")
            {
                System.IO.FileStream fs = (System.IO.FileStream)saveFileDialog.OpenFile();
                chartTienDien.ExportToPdf(fs);
                fs.Close();
            }
        }
    }
}
