using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Data.Linq.SqlClient;
using System.Drawing.Imaging;
using DevExpress.XtraCharts;
using System.Linq;

namespace Library.ThongKeCtl
{
    public partial class ctlThongKeTaiSanNhapVaoTrongNam : DevExpress.XtraEditors.XtraUserControl
    {
        tnNhanVien objnhanvien;
        public ctlThongKeTaiSanNhapVaoTrongNam(tnNhanVien obj)
        {
            InitializeComponent();
            objnhanvien = obj;
            LoadKBC();
            LoadData();
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

                chartSoLuongYeuCau.Series.Clear();
                chartSoLuongYeuCau.Titles.Clear();

                Series bar = new Series("Số tiền mua", ViewType.Bar);
                bar.DataSource = LayDuLieu(tuNgay, denNgay);
                bar.ArgumentDataMember = "_ThoiGian";
                bar.ArgumentScaleType = ScaleType.Qualitative;
                bar.ValueDataMembers.AddRange(new string[] { "_SoTien" });
                bar.ValueScaleType = ScaleType.Numerical;
                chartSoLuongYeuCau.Dock = DockStyle.Fill;
                chartSoLuongYeuCau.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.RightOutside;
                chartSoLuongYeuCau.Legend.AlignmentVertical = LegendAlignmentVertical.TopOutside;

                BarSeriesLabel label = (BarSeriesLabel)bar.Label;
                label.PointOptions.PointView = PointView.Values;
                label.PointOptions.ValueNumericOptions.Format = NumericFormat.Currency;
                label.PointOptions.ValueNumericOptions.Precision = 0;
                label.TextOrientation = TextOrientation.Horizontal;

                //Chart titles
                ChartTitle title = new ChartTitle();
                title.Text = "Thống kê số tiền mua tài sản";
                title.Alignment = StringAlignment.Center;
                title.Dock = ChartTitleDockStyle.Top;
                title.Antialiasing = true;
                title.Font = new Font("Tahoma", 14, FontStyle.Bold);
                title.TextColor = Color.BlueViolet;
                title.Indent = 10;

                chartSoLuongYeuCau.Titles.Add(title);
                chartSoLuongYeuCau.Series.Add(bar);

                XYDiagram diagram = (XYDiagram)chartSoLuongYeuCau.Diagram;
                diagram.AxisX.Label.EnableAntialiasing = DevExpress.Utils.DefaultBoolean.False;
                diagram.AxisX.Label.Staggered = true;
            }
        }

        private List<SoTienTaiSan> LayDuLieu(DateTime TuNgay, DateTime DenNgay)
        {
            List<SoTienTaiSan> lstdt = new List<SoTienTaiSan>();
            using (MasterDataContext db = new MasterDataContext())
            {
                var a = db.nkTaiSans.ToList();
                DateTime now = db.GetSystemDate();
                for (int i = 1; i <= 12; i++)
                {
                    SoTienTaiSan dt = new SoTienTaiSan();
                    foreach (var item in a)
                    {
                        dt._ThoiGian = string.Format("Tháng {0}", i);
                        if (objnhanvien.IsSuperAdmin.Value)
                        {
                            if (item.nkNhapKho.NgayNK.Value.Month == i & item.nkNhapKho.NgayNK.Value.Year >= TuNgay.Year & item.nkNhapKho.NgayNK.Value.Year <= DenNgay.Year)
                            {
                                dt._SoTien = dt._SoTien + ((item.SoLuong * item.DonGia) ?? 0);
                            }
                        }
                        else
                        {
                            if (item.nkNhapKho.NgayNK.Value.Month == i & item.nkNhapKho.NgayNK.Value.Year >= TuNgay.Year & item.nkNhapKho.NgayNK.Value.Year <= DenNgay.Year & item.nkNhapKho.tnNhanVien.MaTN == objnhanvien.MaTN)
                            {
                                dt._SoTien = dt._SoTien + ((item.SoLuong * item.DonGia) ?? 0);
                            }
                        }
                    }
                    lstdt.Add(dt);
                }
            }
            return lstdt;
        }

        private class SoTienTaiSan
        {
            private string ThoiGian;

            public string _ThoiGian
            {
                get { return ThoiGian; }
                set { ThoiGian = value; }
            }

            private decimal SoTien;

            public decimal _SoTien
            {
                get { return SoTien; }
                set { SoTien = value; }
            }

            public SoTienTaiSan()
            {
            }

            public SoTienTaiSan(string tg, decimal st)
            {
                ThoiGian = tg;
                SoTien = st;
            }
        }

        private void itemDenNgay_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void itemTuNgay_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void repositoryItemComboBoxKyBaoCao_EditValueChanged(object sender, EventArgs e)
        {
            SetDate((sender as ComboBoxEdit).SelectedIndex);
        }
    }
}
