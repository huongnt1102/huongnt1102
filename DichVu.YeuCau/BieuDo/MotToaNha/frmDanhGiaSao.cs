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
    public partial class frmDanhGiaSao : DevExpress.XtraEditors.XtraForm
    {
        public frmDanhGiaSao()
        {
            InitializeComponent();
        }
        private class SaoClass
        {
            public decimal SoSao {set;get;}
            public string TenSao {set;get;}
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
                    var objYeuCau = (from p in db.tnycYeuCaus
                                   where p.MaTN == maTN
                                         && SqlMethods.DateDiffDay(tuNgay, p.NgayYC.Value) >= 0
                                         && SqlMethods.DateDiffDay(p.NgayYC.Value, denNgay) >= 0
                                         && p.MaTT==5
                                   group  p  by p.Rating into TT
                                   select new
                                   {
                                       SoSao = TT.Key,
                                       SoLuong = TT.Count()
                                   }).ToList();
                    var objSao= new List<SaoClass>();
                    for(int i=0;i<=5;i++)
                    {
                        var item =new SaoClass();
                        item.SoSao=i;
                        item.TenSao=i.ToString()+"*";
                        objSao.Add(item);
                    }
                    var objData=(from s in objSao
                                 join yc in objYeuCau on s.SoSao equals yc.SoSao into ycau
                                 from yc in ycau.DefaultIfEmpty()
                                 select new {
                                    s.TenSao,
                                    SoLuong=yc==null?0:yc.SoLuong
                                 });
                    gridControl1.DataSource = objData;
                    var series1 = chartControl1.Series[0];
                    series1.Points.Clear();
                    double tong =(double)objData.Sum(p => p.SoLuong);
                    foreach (var p in objData)
                    {
                        var point = new SeriesPoint();
                        point.Argument = p.TenSao.ToString();
                        point.Tag = p.TenSao;
                        point.Values = new double[] { Convert.ToDouble(Convert.ToDouble(p.SoLuong) / tong) };
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

        private void chartControl1_MouseDown(object sender, MouseEventArgs e)
        {
            //ChartHitInfo info = this.chartControl1.CalcHitInfo(e.Location);
            //if (info.InChart && info.SeriesPoint!=null)
            //{
            //    double[] val = info.SeriesPoint.Values;
            //    DialogBox.Alert(info.SeriesPoint.Argument);
            //    DialogBox.Alert(val.ToString());
            //}
            //if (info.InLegend && info.Series != null)
            //{
            //    chartControl1.ClearSelection();
            //    chartControl1.SetObjectSelection(info.SeriesPoint.Argument);
            //    //Hiển thị tên series
            //   // DialogBox.Alert(info.SeriesPoint.Argument);
            //    double[] val = info.SeriesPoint.Values;
            //    DialogBox.Alert(val.ToString());
            //}
        }

    }
}