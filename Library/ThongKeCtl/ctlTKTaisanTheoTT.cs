﻿using System;
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
    public partial class ctlTKTaisanTheoTT : DevExpress.XtraEditors.XtraUserControl
    {
        tnNhanVien objnhanvien;
        public ctlTKTaisanTheoTT(tnNhanVien obj)
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

                chartControl1.Titles.Clear();
                chartControl1.Series.Clear();
                Series sr = new Series("TaiSan_TT", ViewType.Pie3D);
                chartControl1.AppearanceName = "Chameleon";
                sr.DataSource = LayDSTaiSanDangDung(tuNgay,denNgay);
                sr.ArgumentScaleType = ScaleType.Qualitative;
                sr.ArgumentDataMember = "TrangThai";
                sr.ValueScaleType = ScaleType.Numerical;
                sr.ValueDataMembers.AddRange(new string[] { "SoLuong" });
                sr.LegendText = "Số lượng tài sản";
                sr.ShowInLegend = true;

                sr.LegendPointOptions.ValueNumericOptions.Format = NumericFormat.Percent;
                sr.LegendPointOptions.ValueNumericOptions.Precision = 1;

                // Access labels of the first series.
                sr.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
                ((Pie3DSeriesLabel)sr.Label).ResolveOverlappingMode = ResolveOverlappingMode.Default;

                // Access the series options.
                sr.LegendPointOptions.PointView = PointView.ArgumentAndValues;

                // Customize the view-type-specific properties of the series.
                Pie3DSeriesView pie3DView = (Pie3DSeriesView)sr.View;
                pie3DView.Depth = 5;
                pie3DView.SizeAsPercentage = 100;
                pie3DView.ExplodeMode = PieExplodeMode.All;

                // Adjust the point options of the series label.
                Pie3DSeriesLabel label = (Pie3DSeriesLabel)sr.Label;
                label.PointOptions.PointView = PointView.ArgumentAndValues;
                label.PointOptions.ValueNumericOptions.Format = NumericFormat.Percent;
                label.PointOptions.ValueNumericOptions.Precision = 1;
                label.Position = PieSeriesLabelPosition.Inside;

                //Chart titles
                ChartTitle title = new ChartTitle();
                title.Text = "Thống kê tài sản theo trạng thái";
                title.Alignment = StringAlignment.Center;
                title.Dock = ChartTitleDockStyle.Top;
                title.Antialiasing = true;
                title.Font = new Font("Tahoma", 14, FontStyle.Bold);
                title.TextColor = Color.BlueViolet;
                title.Indent = 10;

                chartControl1.Titles.Add(title);
                chartControl1.Series.Add(sr);
            }
        }

        private List<ThongKe> LayDSTaiSanDangDung(DateTime TuNgay, DateTime DenNgay)
        {
            List<ThongKe> lstThongKe = new List<ThongKe>();

            using (MasterDataContext db = new MasterDataContext())
            {
                var lstTT = db.tsTrangThais.ToList();
                foreach (var objtt in lstTT)
                {
                    if (objnhanvien.IsSuperAdmin.Value)
                    {
                        ThongKe objtk = new ThongKe()
                        {
                            TrangThai = objtt.TenTT,
                            SoLuong = db.tsTaiSanSuDungs
                            .Where(p => p.MaTT == objtt.MaTT
                                & SqlMethods.DateDiffDay(TuNgay, p.NgaySD.Value) >= 0
                                & SqlMethods.DateDiffDay(p.NgaySD.Value, DenNgay) >= 0)
                            .Count()
                        };
                        lstThongKe.Add(objtk);
                    }
                    else
                    {
                        ThongKe objtk = new ThongKe()
                        {
                            TrangThai = objtt.TenTT,
                            SoLuong = db.tsTaiSanSuDungs
                            .Where(p => p.MaTT == objtt.MaTT
                                & p.mbMatBang.mbTangLau.mbKhoiNha.tnToaNha.MaTN == objnhanvien.MaTN
                                & SqlMethods.DateDiffDay(TuNgay, p.NgaySD.Value) >= 0
                                & SqlMethods.DateDiffDay(p.NgaySD.Value, DenNgay) >= 0)
                            .Count()
                        };
                        lstThongKe.Add(objtk);
                    }
                }
                return lstThongKe;
            }
        }

        public class ThongKe
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

        private void repositoryItemComboBoxKyBaoCao_EditValueChanged(object sender, EventArgs e)
        {
            SetDate((sender as ComboBoxEdit).SelectedIndex);
        }

        private void itemTuNgay_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void itemDenNgay_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Pdf File (*.pdf)|*.pdf";
            saveFileDialog.Title = "Save an pdf File";
            saveFileDialog.ShowDialog();

            if (saveFileDialog.FileName != "")
            {
                System.IO.FileStream fs = (System.IO.FileStream)saveFileDialog.OpenFile();
                chartControl1.ExportToPdf(fs);
                fs.Close();
            }
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
                chartControl1.ExportToImage(fs, ImageFormat.Jpeg);
                fs.Close();
            }
        }

    }
}