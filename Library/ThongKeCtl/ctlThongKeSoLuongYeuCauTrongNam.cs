using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraCharts;
using System.Linq;
using System.Data.Linq.SqlClient;
using System.Drawing.Imaging;

namespace Library.ThongKeCtl
{
    public partial class ctlThongKeSoLuongYeuCauTrongNam : DevExpress.XtraEditors.XtraUserControl
    {
        tnNhanVien objnhanvien;
        public ctlThongKeSoLuongYeuCauTrongNam(tnNhanVien obj)
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

                Series bar = new Series("Số lượng yêu cầu", ViewType.Bar);
                bar.DataSource = LayDuLieu(tuNgay, denNgay);
                bar.ArgumentDataMember = "_ThoiGian";
                bar.ArgumentScaleType = ScaleType.Qualitative;
                bar.ValueDataMembers.AddRange(new string[] { "_SoLuong" });
                bar.ValueScaleType = ScaleType.Numerical;
                chartSoLuongYeuCau.Dock = DockStyle.Fill;
                chartSoLuongYeuCau.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.RightOutside;
                chartSoLuongYeuCau.Legend.AlignmentVertical = LegendAlignmentVertical.TopOutside;

                //Chart titles
                ChartTitle title = new ChartTitle();
                title.Text = "Thống kê số lượng yêu cầu";
                title.Alignment = StringAlignment.Center;
                title.Dock = ChartTitleDockStyle.Top;
                title.Antialiasing = true;
                title.Font = new Font("Tahoma", 14, FontStyle.Bold);
                title.TextColor = Color.BlueViolet;
                title.Indent = 10;

                chartSoLuongYeuCau.Titles.Add(title);
                chartSoLuongYeuCau.Series.Add(bar);

                XYDiagram diagram = (XYDiagram)chartSoLuongYeuCau.Diagram;
                // Customize the X-axis labels' appearance.
                diagram.AxisX.Label.EnableAntialiasing = DevExpress.Utils.DefaultBoolean.False;
                diagram.AxisX.Label.Staggered = true;
            }
        }

        private List<SoLuongyeuCau> LayDuLieu(DateTime TuNgay, DateTime DenNgay)
        {
            List<SoLuongyeuCau> lstdt = new List<SoLuongyeuCau>();
            using (MasterDataContext db = new MasterDataContext())
            {
                var a = db.tnycYeuCaus.ToList();
                DateTime now = db.GetSystemDate();
                for (int i = 1; i <= 12; i++)
                {
                    SoLuongyeuCau dt = new SoLuongyeuCau();
                    foreach (var item in a)
                    {
                        dt._ThoiGian = string.Format("Tháng {0}", i);
                        if (objnhanvien.IsSuperAdmin.Value)
                        {
                            if (item.NgayYC.Value.Month == i & item.NgayYC.Value.Year >= TuNgay.Year & item.NgayYC.Value.Year <= DenNgay.Year)
                            {
                                dt._SoLuong = dt._SoLuong + 1;
                            }
                        }
                        else
                        {
                            if (item.NgayYC.Value.Month == i & item.NgayYC.Value.Year >= TuNgay.Year & item.NgayYC.Value.Year <= DenNgay.Year & item.tnNhanVien.MaTN == objnhanvien.MaTN)
                            {
                                dt._SoLuong = dt._SoLuong + 1;
                            }
                        }
                    }
                    lstdt.Add(dt);
                }
            }
            return lstdt;
        }

        private class SoLuongyeuCau
        {
            private string ThoiGian;

            public string _ThoiGian
            {
                get { return ThoiGian; }
                set { ThoiGian = value; }
            }

            private int SoLuong;

            public int _SoLuong
            {
                get { return SoLuong; }
                set { SoLuong = value; }
            }

            public SoLuongyeuCau()
            {
            }

            public SoLuongyeuCau(string tg, int st)
            {
                ThoiGian = tg;
                SoLuong = st;
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

        private void itemEx2Image_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Image File (*.jpeg)|*.jpeg";
            saveFileDialog.Title = "Save an Image File";
            saveFileDialog.ShowDialog();

            if (saveFileDialog.FileName != "")
            {
                System.IO.FileStream fs = (System.IO.FileStream)saveFileDialog.OpenFile();
                chartSoLuongYeuCau.ExportToImage(fs, ImageFormat.Jpeg);
                fs.Close();
            }
        }

        private void itemEx2pdf_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Pdf File (*.pdf)|*.pdf";
            saveFileDialog.Title = "Save an pdf File";
            saveFileDialog.ShowDialog();

            if (saveFileDialog.FileName != "")
            {
                System.IO.FileStream fs = (System.IO.FileStream)saveFileDialog.OpenFile();
                chartSoLuongYeuCau.ExportToPdf(fs);
                fs.Close();
            }
        }
    }
}
