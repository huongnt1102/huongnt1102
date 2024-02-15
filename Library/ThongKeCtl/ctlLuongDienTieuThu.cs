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
using System.IO;
using Library;

namespace Library.ThongKeCtl
{
    public partial class ctlLuongDienTieuThu : DevExpress.XtraEditors.XtraUserControl
    {
        tnNhanVien objnhanvien;
        public ctlLuongDienTieuThu(tnNhanVien obj)
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

                chartLuongDienTieuThu.Series.Clear();
                chartLuongDienTieuThu.Titles.Clear();

                Series bar = new Series("Chỉ số điện", ViewType.Bar);
                bar.DataSource = LayDuLieu(tuNgay, denNgay);
                bar.ArgumentDataMember = "_ThoiGian";
                bar.ArgumentScaleType = ScaleType.Qualitative;
                bar.ValueDataMembers.AddRange(new string[] { "_SoTien" });
                bar.ValueScaleType = ScaleType.Numerical;
                chartLuongDienTieuThu.Dock = DockStyle.Fill;
                chartLuongDienTieuThu.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.RightOutside;
                chartLuongDienTieuThu.Legend.AlignmentVertical = LegendAlignmentVertical.TopOutside;

                //Chart titles
                ChartTitle title = new ChartTitle();
                title.Text = "Thống kê lượng điện tiêu thụ";
                title.Alignment = StringAlignment.Center;
                title.Dock = ChartTitleDockStyle.Top;
                title.Antialiasing = true;
                title.Font = new Font("Tahoma", 14, FontStyle.Bold);
                title.TextColor = Color.BlueViolet;
                title.Indent = 10;

                chartLuongDienTieuThu.Titles.Add(title);
                chartLuongDienTieuThu.Series.Add(bar);

                XYDiagram diagram = (XYDiagram)chartLuongDienTieuThu.Diagram;
                // Customize the X-axis labels' appearance.
                diagram.AxisX.Label.EnableAntialiasing = DevExpress.Utils.DefaultBoolean.False;
                diagram.AxisX.Label.Staggered = true;
            }
        }

        private List<LuongDienTieuThu> LayDuLieu(DateTime TuNgay, DateTime DenNgay)
        {
            List<LuongDienTieuThu> lstdt = new List<LuongDienTieuThu>();
            using (MasterDataContext db = new MasterDataContext())
            {
                var a = db.dvdnDiens.ToList();
                DateTime now = db.GetSystemDate();
                for (int i = 1; i <= 12; i++)
                {
                    LuongDienTieuThu dt = new LuongDienTieuThu();
                    foreach (var item in a)
                    {
                        dt._ThoiGian = string.Format("Tháng {0}", i);
                        if (objnhanvien.IsSuperAdmin.Value)
                        {
                            if (item.NgayNhap.Value.Month == i & item.NgayNhap.Value.Year >= TuNgay.Year & item.NgayNhap.Value.Year <= DenNgay.Year)
                            {
                                dt._SoTien += item.SoTieuThu ?? 0;
                            }
                        }
                        else
                        {
                            if (item.NgayNhap.Value.Month == i & item.NgayNhap.Value.Year >= TuNgay.Year & item.NgayNhap.Value.Year <= DenNgay.Year & item.mbMatBang.mbTangLau.mbKhoiNha.tnToaNha.MaTN == objnhanvien.MaTN)
                            {
                                dt._SoTien += item.SoTieuThu ?? 0;
                            }
                        }
                    }
                    lstdt.Add(dt);
                }
            }
            return lstdt;
        }

        private class LuongDienTieuThu
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

            public LuongDienTieuThu()
            {
            }

            public LuongDienTieuThu(string tg, decimal st)
            {
                ThoiGian = tg;
                SoTien = st;
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

        private void btnTKNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
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
                chartLuongDienTieuThu.ExportToImage(fs, ImageFormat.Jpeg);
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
                chartLuongDienTieuThu.ExportToPdf(fs);
                fs.Close();
            }
        }
    }
}
