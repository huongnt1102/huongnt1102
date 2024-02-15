using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraCharts;
using Library;
using System.Linq;
using System.Data.Linq.SqlClient;
using DevExpress.XtraReports.UI;


namespace DichVu.YeuCau.BieuDo.MotToaNha
{
    public partial class frmViewAll : DevExpress.XtraEditors.XtraForm
    {
        public frmViewAll()
        {
            InitializeComponent();
        }
        private void BaoCao_Nap()
        {
            var wait = DialogBox.WaitingForm();
            try
            {
                var maTN = (byte)itemToaNha.EditValue;
                var tuNgay = (DateTime?)itemTuNgay.EditValue;
                var denNgay = (DateTime?)itemDenNgay.EditValue;


                using (var db = new MasterDataContext())
                {
                    #region "Chart Tình trạng xử lý"
                    var objDataTTXL = (from p in db.tnycYeuCaus
                                   join tt in db.tnycTrangThais on p.MaTT equals tt.MaTT
                                   where p.MaTN == maTN
                                         && SqlMethods.DateDiffDay(tuNgay, p.NgayYC.Value) >= 0
                                         && SqlMethods.DateDiffDay(p.NgayYC.Value, denNgay) >= 0
                                   group new { tt, p } by tt.TenTT into TT
                                   select new
                                   {
                                       TenTrangThai = TT.Key,
                                       SoLuong = TT.Count()
                                   }).ToList();
                   // gridControl1.DataSource = objData;
                    var series1 = chartTinhTrangXuLy.Series[0];
                    series1.Points.Clear();
                    double tong = (double)objDataTTXL.Sum(p => p.SoLuong);
                    foreach (var p in objDataTTXL)
                    {
                        var point = new SeriesPoint();
                        point.Argument = p.TenTrangThai;
                        point.Tag = p.TenTrangThai;
                        point.Values = new double[] { Convert.ToDouble(p.SoLuong) / tong};
                        series1.Points.Add(point);
                    }
                    series1.LegendPointOptions.PointView = PointView.Argument;
                    series1.PointOptions.ValueNumericOptions.Format = NumericFormat.Percent;
                    series1.PointOptions.ValueNumericOptions.Precision = 2;
                    #endregion
                    #region "Chart Đánh giá sao"
                    var objYeuCau = (from p in db.tnycYeuCaus
                                     where p.MaTN == maTN
                                           && SqlMethods.DateDiffDay(tuNgay, p.NgayYC.Value) >= 0
                                           && SqlMethods.DateDiffDay(p.NgayYC.Value, denNgay) >= 0
                                           && p.MaTT==5
                                     group p by p.Rating into TT
                                     select new
                                     {
                                         SoSao = TT.Key,
                                         SoLuong = TT.Count()
                                     }).ToList();
                    var objSao = new List<SaoClass>();
                    for (int i = 0; i <= 5; i++)
                    {
                        var item = new SaoClass();
                        item.SoSao = i;
                        item.TenSao = i.ToString() + "*";
                        objSao.Add(item);
                    }
                    var objDataSao = (from s in objSao
                                      join yc in objYeuCau on s.SoSao equals yc.SoSao into ycau
                                   from yc in ycau.DefaultIfEmpty()
                                   select new
                                   {
                                       s.TenSao,
                                       SoLuong = yc == null ? 0 : yc.SoLuong
                                   });
                    var series2 = chartDanhGiaSao.Series[0];
                    series2.Points.Clear();
                    double tong2 = (double)objDataSao.Sum(p => p.SoLuong);
                    foreach (var p in objDataSao)
                    {
                        var point = new SeriesPoint();
                        point.Argument = p.TenSao.ToString();
                        point.Tag = p.TenSao;
                        point.Values = new double[] { Convert.ToDouble(Convert.ToDouble(p.SoLuong) / tong2) };
                        series2.Points.Add(point);
                    }
                    series2.LegendPointOptions.PointView = PointView.Argument;
                    series2.PointOptions.ValueNumericOptions.Format = NumericFormat.Percent;
                    series2.PointOptions.ValueNumericOptions.Precision = 2;
                    #endregion
                    #region Chart độ ưu tiên
                    var objYeuCau3 = (from p in db.tnycYeuCaus
                                     where p.MaTN == maTN
                                      && SqlMethods.DateDiffDay(tuNgay, p.NgayYC.Value) >= 0
                                      && SqlMethods.DateDiffDay(p.NgayYC.Value, denNgay) >= 0
                                     group p by p.MaDoUuTien into TT
                                     select new
                                     {
                                         MaDoUuTien = TT.Key,
                                         SoLuong = TT.Count()
                                     }).ToList();
                    var objDoUuTien = db.tnycDoUuTiens.ToList();
                    var objData3 = (from ut in objDoUuTien
                                   join yc in objYeuCau3 on ut.MaDoUuTien equals yc.MaDoUuTien into ycau
                                   from yc in ycau.DefaultIfEmpty()
                                   select new
                                   {
                                       ut.TenDoUuTien,
                                       SoLuong = yc == null ? 0 : yc.SoLuong
                                   });
                    var series3 = chartDoUuTien.Series[0];
                    series3.Points.Clear();
                    double tong3 = (double)objData3.Sum(p => p.SoLuong);
                    foreach (var p in objData3)
                    {
                        var point = new SeriesPoint();
                        point.Argument = p.TenDoUuTien;
                        point.Tag = p.TenDoUuTien;
                        point.Values = new double[] { Convert.ToDouble(Convert.ToDouble(p.SoLuong) / tong3) };
                        series3.Points.Add(point);
                    }
                    series3.LegendPointOptions.PointView = PointView.Argument;
                    series3.PointOptions.ValueNumericOptions.Format = NumericFormat.Percent;
                    series3.PointOptions.ValueNumericOptions.Precision = 2;
                    #endregion
                    #region Chart nguồn đến
                    var objYeuCau4 = (from p in db.tnycYeuCaus
                                     where p.MaTN == maTN
                                      && SqlMethods.DateDiffDay(tuNgay, p.NgayYC.Value) >= 0
                                      && SqlMethods.DateDiffDay(p.NgayYC.Value, denNgay) >= 0
                                     group p by p.MaNguonDen into TT
                                     select new
                                     {
                                         MaNguonDen = TT.Key,
                                         SoLuong = TT.Count()
                                     }).ToList();
                    var objNguonDen = db.tnycNguonDens.ToList();
                    var objData4 = (from ut in objNguonDen
                                   join yc in objYeuCau4 on ut.ID equals yc.MaNguonDen into ycau
                                   from yc in ycau.DefaultIfEmpty()
                                   select new
                                   {
                                       ut.TenNguonDen,
                                       SoLuong = yc == null ? 0 : yc.SoLuong
                                   });
                    var series4 = chartNguonPhanAnh.Series[0];
                    series4.Points.Clear();
                    double tong4 = (double)objData4.Sum(p => p.SoLuong);
                    foreach (var p in objData4)
                    {
                        var point = new SeriesPoint();
                        point.Argument = p.TenNguonDen;
                        point.Tag = p.TenNguonDen;
                        point.Values = new double[] { Convert.ToDouble(Convert.ToDouble(p.SoLuong) / tong4) };
                        series4.Points.Add(point);
                    }
                    series4.LegendPointOptions.PointView = PointView.Argument;
                    series4.PointOptions.ValueNumericOptions.Format = NumericFormat.Percent;
                    series4.PointOptions.ValueNumericOptions.Precision = 2;
                    #endregion
                    #region Nhóm công việc
                    var objYeuCau5 = (from p in db.tnycYeuCaus
                                     where p.MaTN == maTN && p.GroupProcessId != null
                                           && SqlMethods.DateDiffDay(tuNgay, p.NgayYC.Value) >= 0
                                           && SqlMethods.DateDiffDay(p.NgayYC.Value, denNgay) >= 0
                                     group p by p.GroupProcessId into NCV
                                     select new
                                     {
                                         IDNhomCV = NCV.Key,
                                         SoLuong = NCV.Count()
                                     }).ToList();
                    var objNhomCV = db.app_GroupProcesses.ToList();
                    var objData5 = (from ncv in objNhomCV
                                   join yc in objYeuCau5 on ncv.Id equals yc.IDNhomCV into ycau
                                   from yc in ycau.DefaultIfEmpty()
                                   select new
                                   {
                                       TenNhomCV = ncv.Name,
                                       SoLuong = yc == null ? 0 : yc.SoLuong
                                   });
                    Series series5 = new Series("", ViewType.Bar);
                   // Series series2 = new Series("", ViewType.Line);

                    // Add points to the series.
                    foreach (var l in objData5)
                    {
                        series5.Points.Add(new SeriesPoint(l.TenNhomCV.ToString(), l.SoLuong));

                    }

                    // Add both series to the chart.
                    chartNhomCongViec.Series.AddRange(new Series[] { series5 });


                    /// Access labels of the first series.
                    ((BarSeriesLabel)series5.Label).Visible = true;
                    ((BarSeriesLabel)series5.Label).ResolveOverlappingMode = ResolveOverlappingMode.Default;

                    //((PointSeriesLabel)series2.Label).Visible = false;
                    //((PointSeriesLabel)series2.Label).ResolveOverlappingMode = ResolveOverlappingMode.Default;

                    // Customize the view-type-specific properties of the series.
                    BarSeriesView myView = (BarSeriesView)series5.View;
                    myView.Transparency = 50;

                    //Diagramp 
                    XYDiagram diagram = (XYDiagram)chartNhomCongViec.Diagram;
                    diagram.AxisY.Title.Text = "Số lượng";
                    diagram.AxisY.Title.Visible = true;
                    diagram.AxisX.Visible = true;
                    diagram.AxisY.Title.Font = new Font("Tahoma", 18);
                    ((BarSeriesLabel)series5.Label).PointOptions.Pattern = "{V}";
                    #endregion
                    #region Phản ánh theo tòa nhà
                    chartYeuCauToaNha.Series.Clear();
                    var objYeuCau6 = (from p in db.tnycYeuCaus
                                     where SqlMethods.DateDiffDay(tuNgay, p.NgayYC.Value) >= 0
                                         && SqlMethods.DateDiffDay(p.NgayYC.Value, denNgay) >= 0
                                     group p by p.MaTN into NCV
                                     select new
                                     {
                                         MaTN = NCV.Key,
                                         SoLuong = NCV.Count()
                                     }).ToList();
                    var objToaNha = (from tn in db.tnToaNhas
                                     select new
                                     {
                                         tn.MaTN,
                                         tn.TenTN
                                     }).ToList();
                    var objData6 = (from tn in objToaNha
                                   join yc in objYeuCau6 on tn.MaTN equals yc.MaTN into ycau
                                   from yc in ycau.DefaultIfEmpty()
                                   select new
                                   {
                                       tn.TenTN,
                                       SoLuong = yc == null ? 0 : yc.SoLuong
                                   });
                    Series series6 = new Series("", ViewType.Bar);
                    //Series series2 = new Series("", ViewType.Line);

                    // Add points to the series.
                    foreach (var l in objData6)
                    {
                        series6.Points.Add(new SeriesPoint(l.TenTN.ToString(), l.SoLuong));

                    }

                    // Add both series to the chart.
                    chartYeuCauToaNha.Series.AddRange(new Series[] { series6 });


                    /// Access labels of the first series.
                    ((BarSeriesLabel)series6.Label).Visible = true;
                    ((BarSeriesLabel)series6.Label).ResolveOverlappingMode = ResolveOverlappingMode.Default;

                    //((PointSeriesLabel)series2.Label).Visible = false;
                    //((PointSeriesLabel)series2.Label).ResolveOverlappingMode = ResolveOverlappingMode.Default;

                    // Customize the view-type-specific properties of the series.
                    BarSeriesView myView6 = (BarSeriesView)series6.View;
                    myView6.Transparency = 50;

                    //Diagram 
                    XYDiagram diagram6 = (XYDiagram)chartYeuCauToaNha.Diagram;
                    diagram6.AxisY.Title.Text = "Số lượng";
                    diagram6.AxisY.Title.Visible = true;
                    diagram6.AxisX.Visible = true;
                    diagram6.AxisY.Title.Font = new Font("Tahoma", 18);
                    ((BarSeriesLabel)series6.Label).PointOptions.Pattern = "{V}";
                    #endregion
                }
            }
            catch { }
            finally
            {
                wait.Close();
            }
        }
        private void itemRefresh_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            BaoCao_Nap();
        }

        private void frmBieuDoGas_Load(object sender, EventArgs e)
        {
            lookToaNha.DataSource = Common.TowerList;
            itemToaNha.EditValue = Common.User.MaTN;
            itemTuNgay.EditValue = DateTime.Now;
            itemDenNgay.EditValue = DateTime.Now;
            BaoCao_Nap();
        }

        private void itemPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            chartTinhTrangXuLy.ShowPrintPreview();
        }
        private class SaoClass
        {
            public decimal SoSao { set; get; }
            public string TenSao { set; get; }
        }

        private void chartDanhGiaSao_MouseDown(object sender, MouseEventArgs e)
        {
            ChartHitInfo info = this.chartDanhGiaSao.CalcHitInfo(e.Location);
            if (info.InChart && info.SeriesPoint != null)
            {
                //double[] val = info.SeriesPoint.Values;
                //DialogBox.Alert(info.SeriesPoint.Argument);
                //DialogBox.Alert(val[0].ToString());
                frmManager frm = new frmManager();
                frm.MaTinhTrang = 5;
                frm.MaTN =(byte?)itemToaNha.EditValue;
                frm.TuNgay = (DateTime?)itemTuNgay.EditValue;
                frm.DenNgay = (DateTime?)itemDenNgay.EditValue;
                frm.SoSao =Convert.ToInt32(info.SeriesPoint.Argument.Replace("*", ""));
                frm.ShowDialog();
            }
        }

        private void chartDoUuTien_MouseDown(object sender, MouseEventArgs e)
        {
            ChartHitInfo info = this.chartDoUuTien.CalcHitInfo(e.Location);
            if (info.InChart && info.SeriesPoint != null)
            {
                using (var db = new MasterDataContext())
                {
                    var objDUT = db.tnycDoUuTiens.FirstOrDefault(p => p.TenDoUuTien.Equals(info.SeriesPoint.Argument));
                    if (objDUT != null)
                    {
                        frmManager frm = new frmManager();
                        frm.MaTN = (byte?)itemToaNha.EditValue;
                        frm.TuNgay = (DateTime?)itemTuNgay.EditValue;
                        frm.DenNgay = (DateTime?)itemDenNgay.EditValue;
                        frm.DoUuTien = objDUT.MaDoUuTien;
                        frm.ShowDialog();
                    }
                }
            }
        }

        private void chartNguonPhanAnh_MouseDown(object sender, MouseEventArgs e)
        {
            ChartHitInfo info = this.chartNguonPhanAnh.CalcHitInfo(e.Location);
            if (info.InChart && info.SeriesPoint != null)
            {
                using (var db = new MasterDataContext())
                {
                    var objDUT = db.tnycNguonDens.FirstOrDefault(p => p.TenNguonDen.Equals(info.SeriesPoint.Argument));
                    if (objDUT != null)
                    {
                        frmManager frm = new frmManager();
                        frm.MaTN = (byte?)itemToaNha.EditValue;
                        frm.TuNgay = (DateTime?)itemTuNgay.EditValue;
                        frm.DenNgay = (DateTime?)itemDenNgay.EditValue;
                        frm.NguonDen = objDUT.ID;
                        frm.ShowDialog();
                    }
                }
            }
        }

        private void chartTinhTrangXuLy_MouseDown(object sender, MouseEventArgs e)
        {
            ChartHitInfo info = this.chartTinhTrangXuLy.CalcHitInfo(e.Location);
            if (info.InChart && info.SeriesPoint != null)
            {
                using (var db = new MasterDataContext())
                {
                    var objDUT = db.tnycTrangThais.FirstOrDefault(p => p.TenTT.Equals(info.SeriesPoint.Argument));
                    if (objDUT != null)
                    {
                        frmManager frm = new frmManager();
                        frm.MaTN = (byte?)itemToaNha.EditValue;
                        frm.TuNgay = (DateTime?)itemTuNgay.EditValue;
                        frm.DenNgay = (DateTime?)itemDenNgay.EditValue;
                        frm.MaTinhTrang = objDUT.MaTT;
                        frm.ShowDialog();
                    }
                }
            }
        }

        private void chartNhomCongViec_MouseDown(object sender, MouseEventArgs e)
        {
            ChartHitInfo info = this.chartNhomCongViec.CalcHitInfo(e.Location);
            if (info.InChart && info.SeriesPoint != null)
            {
                using (var db = new MasterDataContext())
                {
                    var objDUT = db.app_GroupProcesses.FirstOrDefault(p => p.Name.Equals(info.SeriesPoint.Argument));
                    if (objDUT != null)
                    {
                        frmManager frm = new frmManager();
                        frm.MaTN = (byte?)itemToaNha.EditValue;
                        frm.TuNgay = (DateTime?)itemTuNgay.EditValue;
                        frm.DenNgay = (DateTime?)itemDenNgay.EditValue;
                        frm.MaNhomCongViec = objDUT.Id;
                        frm.ShowDialog();
                    }
                }
            }
        }

        private void chartYeuCauToaNha_MouseDown(object sender, MouseEventArgs e)
        {
            ChartHitInfo info = this.chartYeuCauToaNha.CalcHitInfo(e.Location);
            if (info.InChart && info.SeriesPoint != null)
            {
                using (var db = new MasterDataContext())
                {
                    var objDUT = db.tnToaNhas.FirstOrDefault(p => p.TenTN.Equals(info.SeriesPoint.Argument));
                    if (objDUT != null)
                    {
                        frmManager frm = new frmManager();
                        frm.MaTN = (byte?)objDUT.MaTN;
                        frm.TuNgay = (DateTime?)itemTuNgay.EditValue;
                        frm.DenNgay = (DateTime?)itemDenNgay.EditValue;
                        frm.ShowDialog();
                    }
                }
            }
        }

    }
}