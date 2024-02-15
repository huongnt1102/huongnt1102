﻿using System;
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
    public partial class frmNhomCongViec : DevExpress.XtraEditors.XtraForm
    {
        public frmNhomCongViec()
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

                chartControl1.Series.Clear();

                using (var db = new MasterDataContext())
                {
                    var objYeuCau = (from p in db.tnycYeuCaus 
                                   where p.MaTN == maTN && p.GroupProcessId!=null
                                         && SqlMethods.DateDiffDay(tuNgay, p.NgayYC.Value) >= 0
                                         && SqlMethods.DateDiffDay(p.NgayYC.Value, denNgay) >= 0
                                   group p by p.GroupProcessId into NCV
                                   select new
                                   {
                                       IDNhomCV = NCV.Key,
                                       SoLuong = NCV.Count()
                                   }).ToList();
                    var objNhomCV = db.app_GroupProcesses.ToList();
                    var objData = (from ncv in objNhomCV
                                   join yc in objYeuCau on ncv.Id equals yc.IDNhomCV into ycau
                                   from yc in ycau.DefaultIfEmpty()
                                   select new { 
                                        TenNhomCV=ncv.Name,
                                        SoLuong=yc==null?0: yc.SoLuong
                                   });
                    gridControl1.DataSource = objData.ToList();
                    Series series1 = new Series("", ViewType.Bar);
                    Series series2 = new Series("", ViewType.Line);

                    // Add points to the series.
                    foreach (var l in objData)
                    {
                        series1.Points.Add(new SeriesPoint(l.TenNhomCV.ToString(), l.SoLuong));
                        
                    }

                    // Add both series to the chart.
                    chartControl1.Series.AddRange(new Series[] { series1, series2 });


                    /// Access labels of the first series.
                    ((BarSeriesLabel)series1.Label).Visible = true;
                    ((BarSeriesLabel)series1.Label).ResolveOverlappingMode = ResolveOverlappingMode.Default;

                    ((PointSeriesLabel)series2.Label).Visible = false;
                    ((PointSeriesLabel)series2.Label).ResolveOverlappingMode = ResolveOverlappingMode.Default;

                    // Customize the view-type-specific properties of the series.
                    BarSeriesView myView = (BarSeriesView)series1.View;
                    myView.Transparency = 50;

                    //Diagramp 
                    XYDiagram diagram = (XYDiagram)chartControl1.Diagram;
                    diagram.AxisY.Title.Text = "Số lượng";
                    diagram.AxisY.Title.Visible = true;
                    diagram.AxisX.Visible = true;
                    diagram.AxisY.Title.Font = new Font("Tahoma", 18);
                    ((BarSeriesLabel)series1.Label).PointOptions.Pattern = "{V}";


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