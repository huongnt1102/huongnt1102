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
    public partial class ctlTKHopDongTheoTrangThai : DevExpress.XtraEditors.XtraUserControl
    {
        tnNhanVien objnhanvien;
        public ctlTKHopDongTheoTrangThai(tnNhanVien obj)
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

        private class ThongKe
        {
            public string TrangThai { get; set; }
            public int SoLuong { get; set; }
            public ThongKe(string tt, int sl)
            {
                TrangThai = tt;
                SoLuong = sl;
            }

            public ThongKe()
            {

            }
        }

        private List<ThongKe> GetData(DateTime TuNgay, DateTime DenNgay)
        {
            List<ThongKe> lst = new List<ThongKe>();
            using (MasterDataContext db = new MasterDataContext())
            {
                var lstTrangThai = db.thueTrangThais.ToList();
                foreach (var item in lstTrangThai)
                {
                    if (objnhanvien.IsSuperAdmin.Value)
                    {
                        ThongKe objthongke = new ThongKe()
                        {
                            TrangThai = item.TenTT,
                            SoLuong = db.thueHopDongs.Where(p => p.thueTrangThai == item
                                & SqlMethods.DateDiffDay(TuNgay, p.NgayHD.Value) >= 0
                                & SqlMethods.DateDiffDay(p.NgayHD.Value, DenNgay) >= 0).Count()
                        };
                        lst.Add(objthongke);
                    }
                    else
                    {
                        ThongKe objthongke = new ThongKe()
                        {
                            TrangThai = item.TenTT,
                            SoLuong = db.thueHopDongs.Where(p => p.thueTrangThai == item
                                & SqlMethods.DateDiffDay(TuNgay, p.NgayHD.Value) >= 0
                                & SqlMethods.DateDiffDay(p.NgayHD.Value, DenNgay) >= 0
                                & p.mbMatBang.mbTangLau.mbKhoiNha.tnToaNha.MaTN == objnhanvien.MaTN).Count()
                        };
                        lst.Add(objthongke);
                    }
                }
                return lst;
            }
        }

        private void LoadData()
        {
            if (itemTuNgay.EditValue != null & itemDenNgay.EditValue != null)
            {
                var tuNgay = (DateTime)itemTuNgay.EditValue;
                var denNgay = (DateTime)itemDenNgay.EditValue;

                using (MasterDataContext db = new MasterDataContext())
                {
                    chartHDTTT.Series.Clear();
                    chartHDTTT.Titles.Clear();

                    Series srthu = new Series("Hợp đồng theo trạng thái", ViewType.Pie3D);
                    chartHDTTT.AppearanceName = "Chameleon";
                    srthu.DataSource = GetData(tuNgay, denNgay);
                    srthu.ArgumentDataMember = "TrangThai";
                    srthu.ArgumentScaleType = ScaleType.Qualitative;
                    srthu.ValueDataMembers.AddRange(new string[] { "SoLuong" });
                    srthu.ValueScaleType = ScaleType.Numerical;

                    chartHDTTT.Dock = DockStyle.Fill;
                    chartHDTTT.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.RightOutside;
                    chartHDTTT.Legend.AlignmentVertical = LegendAlignmentVertical.TopOutside;

                    Pie3DSeriesLabel label = (Pie3DSeriesLabel)srthu.Label;
                    label.PointOptions.PointView = PointView.Values;
                    label.PointOptions.ValueNumericOptions.Format = NumericFormat.Percent;
                    label.PointOptions.ValueNumericOptions.Precision = 1;
                    label.TextOrientation = TextOrientation.Horizontal;
                    

                    //Chart titles
                    ChartTitle title = new ChartTitle();
                    title.Text = string.Format("Thống kê hợp đồng từ {0} đến {1}", tuNgay.ToShortDateString(), denNgay.ToShortDateString());
                    title.Alignment = StringAlignment.Center;
                    title.Dock = ChartTitleDockStyle.Top;
                    title.Antialiasing = true;
                    title.Font = new Font("Tahoma", 14, FontStyle.Bold);
                    title.TextColor = Color.BlueViolet;
                    title.Indent = 10;

                    chartHDTTT.Titles.Add(title);
                    chartHDTTT.Series.AddRange(new Series[] { srthu });

                    //XYDiagram diagram = (XYDiagram)chartHDTTT.Diagram;

                    //diagram.AxisY.Title.Visible = true;
                    //diagram.AxisY.Title.Alignment = StringAlignment.Center;
                    //diagram.AxisY.Title.Text = "Số lượng hợp đồng";
                    //diagram.AxisY.Title.TextColor = Color.Blue;
                    //diagram.AxisY.Title.Antialiasing = true;
                    //diagram.AxisY.Title.Font = new Font("Tahoma", 14, FontStyle.Bold);

                    //diagram.AxisY.NumericOptions.Format = NumericFormat.General;
                    //diagram.AxisY.NumericOptions.Precision = 0;
                }
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
        
        private void btnNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void repositoryItemComboBoxKyBaoCao_EditValueChanged(object sender, EventArgs e)
        {
            SetDate((sender as ComboBoxEdit).SelectedIndex);
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
                chartHDTTT.ExportToImage(fs, ImageFormat.Jpeg);
                fs.Close();
            }
        }

        private void itempdf_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Pdf File (*.pdf)|*.pdf";
            saveFileDialog.Title = "Save an pdf File";
            saveFileDialog.ShowDialog();

            if (saveFileDialog.FileName != "")
            {
                System.IO.FileStream fs = (System.IO.FileStream)saveFileDialog.OpenFile();
                chartHDTTT.ExportToPdf(fs);
                fs.Close();
            }
        }

    }
}
