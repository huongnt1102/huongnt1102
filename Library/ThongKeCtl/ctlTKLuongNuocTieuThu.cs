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
using System.Drawing.Imaging;
using Library;

namespace Library.ThongKeCtl
{
    public partial class ctlTKLuongNuocTieuThu : DevExpress.XtraEditors.XtraUserControl
    {
        tnNhanVien objnhanvien;
        public ctlTKLuongNuocTieuThu(tnNhanVien obj)
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

                ChartLuongNuoc.Series.Clear();
                ChartLuongNuoc.Titles.Clear();

                Series bar = new Series("Chỉ số nước", ViewType.Bar);
                bar.DataSource = LayDuLieu(tuNgay, denNgay);
                bar.ArgumentDataMember = "_ThoiGian";
                bar.ArgumentScaleType = ScaleType.Qualitative;
                bar.ValueDataMembers.AddRange(new string[] { "_SoTien" });
                bar.ValueScaleType = ScaleType.Numerical;
                ChartLuongNuoc.Dock = DockStyle.Fill;
                ChartLuongNuoc.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.RightOutside;
                ChartLuongNuoc.Legend.AlignmentVertical = LegendAlignmentVertical.TopOutside;

                //Chart titles
                ChartTitle title = new ChartTitle();
                title.Text = "Thống kê lượng nước tiêu thụ";
                title.Alignment = StringAlignment.Center;
                title.Dock = ChartTitleDockStyle.Top;
                title.Antialiasing = true;
                title.Font = new Font("Tahoma", 14, FontStyle.Bold);
                title.TextColor = Color.BlueViolet;
                title.Indent = 10;

                ChartLuongNuoc.Titles.Add(title);
                ChartLuongNuoc.Series.Add(bar);

                XYDiagram diagram = (XYDiagram)ChartLuongNuoc.Diagram;
                // Customize the X-axis labels' appearance.
                diagram.AxisX.Label.EnableAntialiasing = DevExpress.Utils.DefaultBoolean.False;
                diagram.AxisX.Label.Staggered = true;
            }
        }

        private List<LuongNuocTieuThu> LayDuLieu(DateTime TuNgay, DateTime DenNgay)
        {
            List<LuongNuocTieuThu> lstdt = new List<LuongNuocTieuThu>();
            using (MasterDataContext db = new MasterDataContext())
            {
                var a = db.dvdnNuocs.ToList();
                DateTime now = db.GetSystemDate();
                for (int i = 1; i <= 12; i++)
                {
                    LuongNuocTieuThu dt = new LuongNuocTieuThu();
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

        private class LuongNuocTieuThu
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

            public LuongNuocTieuThu()
            {
            }

            public LuongNuocTieuThu(string tg, decimal st)
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

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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
                ChartLuongNuoc.ExportToImage(fs, ImageFormat.Jpeg);
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
                ChartLuongNuoc.ExportToPdf(fs);
                fs.Close();
            }
        }
    }
}
