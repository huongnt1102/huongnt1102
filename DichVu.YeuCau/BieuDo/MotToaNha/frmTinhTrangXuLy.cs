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
    public partial class frmTinhTrangXuLy : DevExpress.XtraEditors.XtraForm
    {
        public frmTinhTrangXuLy()
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
                    var objData = (from p in db.tnycYeuCaus
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
                    gridControl1.DataSource = objData;
                    var series1 = chartControl1.Series[0];
                    series1.Points.Clear();
                    double tong =(double)objData.Sum(p => p.SoLuong);
                    foreach (var p in objData)
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
            chartControl1.ShowPrintPreview();
        }

    }
}