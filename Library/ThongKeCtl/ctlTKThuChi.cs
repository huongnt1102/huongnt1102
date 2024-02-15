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
using DevExpress.XtraEditors.Controls;
using System.Drawing.Imaging;

namespace Library.ThongKeCtl
{
    public partial class ctlTKThuChi : DevExpress.XtraEditors.XtraUserControl
    {
        tnNhanVien objnhanvien;
        public ctlTKThuChi(tnNhanVien obj)
        {
            InitializeComponent();
            objnhanvien = obj;
            LoadKBC();
            LoadToaNha();
            LoadData();
        }

        private void LoadToaNha()
        {
            lookToaNha.NullText = "Chọn Dự án";
            using (var db = new MasterDataContext())
            {
                if (objnhanvien.IsSuperAdmin.Value)
                {
                    lookToaNha.DataSource = db.tnToaNhas
                                .Select(p => new
                                {
                                    p.MaTN,
                                    p.TenTN,
                                    p.TenVT
                                });
                }
                else
                {
                    lookToaNha.DataSource = db.tnToaNhas
                        .Where(p=>p.MaTN == objnhanvien.MaTN)
                                .Select(p => new
                                {
                                    p.MaTN,
                                    p.TenTN,
                                    p.TenVT
                                });
                }
                lookToaNha.ValueMember = "MaTN";
                lookToaNha.DisplayMember = "TenTN";
                lookToaNha.BestFitMode = BestFitMode.BestFitResizePopup;
                lookToaNha.SearchMode = SearchMode.AutoComplete;
                lookToaNha.DropDownRows = 10;
                LookUpColumnInfoCollection col = lookToaNha.Columns;
                col.Add(new LookUpColumnInfo("TenVT", "Tên viết tắt"));
                col.Add(new LookUpColumnInfo("TenTN", "Tên Dự án"));
            }
        }

        private void LoadData()
        {
            if (itemKyBC.EditValue!=null & itemtoanha.EditValue!=null)
            {
                using (MasterDataContext db = new MasterDataContext())
                {
                    var objtoanha = db.tnToaNhas.Single(p => p.MaTN == (byte)itemtoanha.EditValue);
                    chartControlDoanhThu.Series.Clear();
                    chartControlDoanhThu.Titles.Clear();

                    #region Thu
                    Series srthu = new Series("Thu trong năm", ViewType.Bar);

                    srthu.DataSource = DoanhSo_Thu((int)itemKyBC.EditValue, objtoanha);
                    srthu.ArgumentDataMember = "ThoiGian";
                    srthu.ArgumentScaleType = ScaleType.Qualitative;
                    srthu.ValueDataMembers.AddRange(new string[] { "SoTien" });
                    srthu.ValueScaleType = ScaleType.Numerical;
                    
                    chartControlDoanhThu.Dock = DockStyle.Fill;
                    chartControlDoanhThu.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.RightOutside;
                    chartControlDoanhThu.Legend.AlignmentVertical = LegendAlignmentVertical.TopOutside;

                    BarSeriesLabel label = (BarSeriesLabel)srthu.Label;
                    label.PointOptions.PointView = PointView.Values;
                    label.PointOptions.ValueNumericOptions.Format = NumericFormat.Currency;
                    label.PointOptions.ValueNumericOptions.Precision = 0;
                    label.TextOrientation = TextOrientation.BottomToTop;

                    //Chart titles
                    ChartTitle title = new ChartTitle();
                    title.Text = string.Format("Thống kê thu chi trong năm {0}",itemKyBC.EditValue);
                    title.Alignment = StringAlignment.Center;
                    title.Dock = ChartTitleDockStyle.Top;
                    title.Antialiasing = true;
                    title.Font = new Font("Tahoma", 14, FontStyle.Bold);
                    title.TextColor = Color.BlueViolet;
                    title.Indent = 10;

                    chartControlDoanhThu.Titles.Add(title);
                    chartControlDoanhThu.Series.AddRange(new Series[] { srthu });
                    #endregion

                    #region Chi
                    Series srchi = new Series("Chi trong năm", ViewType.Bar);

                    //foreach (var item in DoanhSo_Chi((int)itemKyBC.EditValue, objtoanha))
                    //{
                    //    srchi.Points.Add(new SeriesPoint(item.ThoiGian, item.SoTien));
                    //}
                    srchi.DataSource = DoanhSo_Chi((int)itemKyBC.EditValue, objtoanha);
                    srchi.ArgumentDataMember = "ThoiGian";
                    srchi.ArgumentScaleType = ScaleType.Qualitative;
                    srchi.ValueDataMembers.AddRange(new string[] { "SoTien" });
                    srchi.ValueScaleType = ScaleType.Numerical;
                    srchi.LegendPointOptions.ValueNumericOptions.Format = NumericFormat.Currency;
                    chartControlDoanhThu.Dock = DockStyle.Fill;
                    chartControlDoanhThu.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.RightOutside;
                    chartControlDoanhThu.Legend.AlignmentVertical = LegendAlignmentVertical.TopOutside;

                    BarSeriesLabel labelchi = (BarSeriesLabel)srchi.Label;
                    labelchi.PointOptions.PointView = PointView.Values;
                    labelchi.PointOptions.ValueNumericOptions.Format = NumericFormat.Currency;
                    labelchi.PointOptions.ValueNumericOptions.Precision = 0;
                    labelchi.TextOrientation = TextOrientation.BottomToTop;

                    chartControlDoanhThu.Series.AddRange(new Series[] { srchi });
                    #endregion

                    // Cast the chart's diagram to the XYDiagram type, to access its axes.
                    XYDiagram diagram = (XYDiagram)chartControlDoanhThu.Diagram;

                    diagram.AxisY.Title.Visible = true;
                    diagram.AxisY.Title.Alignment = StringAlignment.Center;
                    diagram.AxisY.Title.Text = "Số tiền";
                    diagram.AxisY.Title.TextColor = Color.Blue;
                    diagram.AxisY.Title.Antialiasing = true;
                    diagram.AxisY.Title.Font = new Font("Tahoma", 14, FontStyle.Bold);

                    diagram.AxisY.NumericOptions.Format = NumericFormat.Currency;
                    diagram.AxisY.NumericOptions.Precision = 0;

                } 
            }
        }

        private void LoadKBC()
        {
            for (int i = 2000; i < 2020; i++)
            {
                DateTime newdt = new DateTime(i, 1, 1);
                repositoryItemComboBoxKyBaoCao.Items.Add(i);
            }
            itemKyBC.EditValue = DateTime.Now.Year;
        }

        private List<DoanhThu> DoanhSo_Thu(int NamTinhToan, tnToaNha objtoanha)
        {
            List<DoanhThu> lstdt = new List<DoanhThu>();

            using (MasterDataContext db = new MasterDataContext())
            {
                var TongHopDong = db.thueCongNos
                    .Where(p => p.thueHopDong.mbMatBang.mbTangLau.mbKhoiNha.tnToaNha == objtoanha & p.ConNo <= 0
                        & p.ChuKyMax.Value.Year == NamTinhToan).ToList();
                var TongNuoc = db.dvdnNuocs
                    .Where(p => p.DaTT == true & p.mbMatBang.mbTangLau.mbKhoiNha.tnToaNha == objtoanha
                        & p.NgayNhap.Value.Year == NamTinhToan).ToList();
                var TongDien = db.dvdnDiens
                    .Where(p => p.DaTT == true & p.mbMatBang.mbTangLau.mbKhoiNha.tnToaNha == objtoanha
                        & p.NgayNhap.Value.Year == NamTinhToan).ToList();
                var TongDVK = 0;
                var TongTM = db.dvtmThanhToanThangMays
                    .Where(p => p.dvtmTheThangMay.mbMatBang.mbTangLau.mbKhoiNha.tnToaNha == objtoanha & p.DaTT == true
                        & p.ThangThanhToan.Value.Year == NamTinhToan).ToList();
                var tygia = db.tnTyGias.ToList();

                for (int i = 1; i <= 12; i++)
                {
                    DoanhThu dt = new DoanhThu();
                    dt.ThoiGian = string.Format("Tháng {0}", i);

                    TongHopDong.ForEach(p =>
                        {
                            if (p.ChuKyMax.Value.Month==i)
                            {
                                foreach (var tg in tygia)
                                {
                                    if (p.thueHopDong.MaTG == tg.MaTG)
                                    {
                                        dt.SoTien += p.DaThanhToan ?? 0 + tg.TyGia ?? 0;
                                    }
                                }
                            }
                        });

                    TongNuoc.ForEach(p =>
                        {
                            if (p.NgayNhap.Value.Month==i)
                            {
                                dt.SoTien += p.DaThanhToan ?? 0;
                            }
                        });
                    TongDien.ForEach(p =>
                        {
                            if (p.NgayNhap.Value.Month == i)
                            {
                                dt.SoTien += p.DaThanhToan ?? 0;
                            }
                        });
                    
                    TongTM.ForEach(p =>
                        {
                            if (p.ThangThanhToan.Value.Month == i)
                            {
                                dt.SoTien += p.dvtmTheThangMay.PhiLamThe ?? 0;
                            }
                        });
                    lstdt.Add(dt);
                }
            }

            return lstdt;
        }

        private List<DoanhThu> DoanhSo_Chi(int NamTinhToan, tnToaNha objtoanha)
        {
            List<DoanhThu> lstdtc = new List<DoanhThu>();

            using (MasterDataContext db = new MasterDataContext())
            {
                var ts = db.msTaiSans
                    .Where(p => p.msMuaHang.DaTT == true & p.msMuaHang.MaTN == objtoanha.MaTN
                        & p.msMuaHang.NgayMH.Value.Year == NamTinhToan).ToList();
                var hdtn = db.hdtnCongNos
                    .Where(p => p.hdtnHopDong.tnToaNha == objtoanha
                    & p.NgayThanhToan.Value.Year == NamTinhToan).ToList();

                for (int i = 1; i <= 12; i++)
                {
                    DoanhThu dt = new DoanhThu();
                    dt.ThoiGian = string.Format("Tháng {0}", i);
                    ts.ForEach(item =>
                    {
                        if (item.msMuaHang.NgayMH.Value.Month == i)
                        {
                            dt.SoTien += (item.SoLuong * item.DonGia) ?? 0;
                        }
                    });
                    hdtn.ForEach(item =>
                    {
                        if (item.NgayThanhToan.Value.Month == i)
                        {
                            dt.SoTien += (item.DaThanhToan ?? 0);
                        }
                    });

                    lstdtc.Add(dt);
                }
            }

            return lstdtc;
        }

        private class DoanhThu
        {
            public string ThoiGian { get; set; }
            public decimal SoTien { get; set; }
            public DoanhThu() { }
            public DoanhThu(string tg, decimal st)
            {
                SoTien = st;
                ThoiGian = tg;
            }
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
                chartControlDoanhThu.ExportToImage(fs, ImageFormat.Jpeg);
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
                chartControlDoanhThu.ExportToPdf(fs);
                fs.Close();
            }
        }

        private void itemtoanha_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void itemKyBC_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
