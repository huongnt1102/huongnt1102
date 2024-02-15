using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraCharts;

using DevExpress.XtraEditors;
using Library;

namespace DIPCRM.Need.Reports
{
    public partial class ctlReport : DevExpress.XtraEditors.XtraUserControl
    {
        public System.Windows.Forms.Form frm { get; set; }
        ChartControl pie3DChart;
        int SDBID = 6;
        short DepID = 0, GroupID = 0;
        int? StaffID = 0;
        public ctlReport()
        {
            InitializeComponent();
            
        }

        void Departments()
        {
            //switch (SDBID)
            //{
            //    case 2:
            //        DepID = Common.MaPB;
            //        break;
            //}
        }

        void Groups()
        {
            //switch (SDBID)
            //{
            //    case 3:
            //        GroupID = Common.MaNKD;
            //        break;
            //}
        }

        void Staffs()
        {
            switch (SDBID)
            {
                case 4:
                    StaffID = Common.User.MaNV;
                    break;
            }
        }

        void Pie3D()
        {
            pie3DChart = new ChartControl();
            pie3DChart.AppearanceName = "Chameleon";
            panelControl1.Controls.Clear();
            var wait = DialogBox.WaitingForm();

            try
            {
                // Add a bar series to it.
                string Title = "";
                switch (itemCategory.EditValue.ToString())
                {
                    case "Theo Loại khách hàng":
                        Title = "DOANH SỐ CƠ HỘI THEO LOẠI KHÁCH HÀNG";
                        break;
                    case "Theo Tình trạng":
                        Title = "DOANH SỐ CƠ HỘI THEO TÌNH TRẠNG";
                        break;
                    case "Theo Nguồn":
                        Title = "DOANH SỐ CƠ HỘI THEO NGUỒN";
                        break;
                    case "Theo Tỉnh(TP)":
                        Title = "DOANH SỐ CƠ HỘI THEO TỈNH(TP)";
                        break;
                }
                Series series1 = new Series(Title, ViewType.Pie3D);

                using (MasterDataContext db = new MasterDataContext())
                {
                    var tuNgay = (DateTime?)itemTuNgay.EditValue;
                    var denNgay = (DateTime?)itemDenNgay.EditValue;
                    //switch (itemCategory.EditValue.ToString())
                    //{
                    //    case "Theo Loại khách hàng":
                    //        var tblResult = db.ncNhuCau_report(tuNgay, denNgay, DepID, GroupID, StaffID);

                    //        // Add points to the series.
                    //        foreach (var r in tblResult)
                    //            series1.Points.Add(new SeriesPoint("(" + r.Amount.Value.ToString() + ") " + r.TenNKH, double.Parse(r.Amount.Value.ToString())));
                    //        break;

                    //    case "Theo Tình trạng":
                    //        var tblResult2 = db.ncNhuCau_reportByStatus(tuNgay, denNgay, DepID, GroupID, StaffID);

                    //        // Add points to the series.
                    //        foreach (var r in tblResult2)
                    //            series1.Points.Add(new SeriesPoint("(" + r.Amount.Value.ToString() + ") " + r.TenTT, double.Parse(r.Amount.Value.ToString())));
                    //        break;

                    //    case "Theo Nguồn":
                    //        var tblResult3 = db.ncNhuCau_reportByResource(tuNgay, denNgay, DepID, GroupID, StaffID);

                    //        // Add points to the series.
                    //        foreach (var r in tblResult3)
                    //            series1.Points.Add(new SeriesPoint("(" + r.Amount.Value.ToString() + ") " + r.TenNguon, double.Parse(r.Amount.Value.ToString())));
                    //        break;
                    //    case "Theo Tỉnh(TP)":
                    //        var tblResult4 = db.ncNhuCau_reportByProvince(tuNgay, denNgay, DepID, GroupID, StaffID);

                    //        // Add points to the series.
                    //        foreach (var r in tblResult4)
                    //            series1.Points.Add(new SeriesPoint("(" + r.Amount.Value.ToString() + ") " + r.TenTinh, double.Parse(r.Amount.Value.ToString())));
                    //        break;
                    //}
                }

                // Add both series to the chart.
                pie3DChart.Series.AddRange(new Series[] { series1 });

                //        pie3DChart.AnnotationRepository.Add(new TextAnnotation("Annotation 1", "Some text..."));
                //        pie3DChart.AnnotationRepository[0].AnchorPoint =
                //new SeriesPointAnchorPoint(pie3DChart.Series[0].Points[1]);
                //        pie3DChart.AnnotationRepository[0].ConnectorStyle = AnnotationConnectorStyle.NotchedArrow;

                // Access labels of the first series.
                ((Pie3DSeriesLabel)series1.Label).Visible = true;
                ((Pie3DSeriesLabel)series1.Label).ResolveOverlappingMode =
                    ResolveOverlappingMode.Default;
                ((Pie3DSeriesLabel)series1.Label).Position = PieSeriesLabelPosition.Inside;

                // Access the series options.
                series1.PointOptions.PointView = PointView.ArgumentAndValues;
                ((SimpleDiagram3D)pie3DChart.Diagram).RuntimeRotation = true;

                // Customize the view-type-specific properties of the series.
                Pie3DSeriesView pie3DView = (Pie3DSeriesView)series1.View;
                pie3DView.Depth = 10;
                pie3DView.SizeAsPercentage = 100;
                pie3DView.ExplodeMode = PieExplodeMode.All;

                // Add a title to the chart and hide the legend.
                pie3DChart.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Left;
                pie3DChart.Legend.AlignmentVertical = LegendAlignmentVertical.Bottom;
                pie3DChart.Legend.Visible = true;
                pie3DChart.Legend.Direction = LegendDirection.LeftToRight;
                pie3DChart.Dock = DockStyle.Fill;

                // Add the chart to the form.
                panelControl1.Controls.Add(pie3DChart);
                SetNumericOptions(pie3DChart.Series[0], NumericFormat.Percent, 0);
            }
            catch { }
            finally
            { wait.Close(); }
        }

        protected void SetNumericOptions(Series series, NumericFormat format, int precision)
        {
            series.PointOptions.ValueNumericOptions.Format = format;
            series.PointOptions.ValueNumericOptions.Precision = precision;
        }

        private void ctlReport_Load(object sender, EventArgs e)
        {
            Library.HeThongCls.PhanQuyenCls.Authorize(frm, Common.User, barManager1);
            using (MasterDataContext db = new MasterDataContext())
            {
                //this.SDBID = db.AccessDatas.Single(p => p.FormID == 86 & p.PerID == Common.PerID).SDBID;
            }
            Departments();
            Groups();
            Staffs();

            KyBaoCao objKBC = new KyBaoCao();
            objKBC.Initialize(cmbKyBaoCao);

            SetDate(0);

            Pie3D();
        }

        private void SetDate(int index)
        {
            KyBaoCao objKBC = new KyBaoCao();
            objKBC.Index = index;
            objKBC.SetToDate();

            itemTuNgay.EditValueChanged -= new EventHandler(itemTuNgay_EditValueChanged);
            itemTuNgay.EditValue = objKBC.DateFrom;
            itemDenNgay.EditValue = objKBC.DateTo;
            itemTuNgay.EditValueChanged += new EventHandler(itemTuNgay_EditValueChanged);
        }

        private void itemTuNgay_EditValueChanged(object sender, EventArgs e)
        {
            Pie3D();
        }

        private void itemDenNgay_EditValueChanged(object sender, EventArgs e)
        {
            Pie3D();
        }

        private void itemCategory_EditValueChanged(object sender, EventArgs e)
        {
            Pie3D();
        }

        private void itemRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Pie3D();
        }

        private void cmbKyBaoCao_EditValueChanged(object sender, EventArgs e)
        {
            SetDate((sender as ComboBoxEdit).SelectedIndex);
        }

        private void itemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var f = new frmOption();
            f.ShowDialog();
            if (f.DialogResult == DialogResult.OK)
            {
                SaveFileDialog fsfd = new SaveFileDialog();
                switch (f.CateID)
                {
                    case 1://Word
                        fsfd.Filter = "File Word(.doc, .docx)|*.doc;*.docx";
                        break;
                    case 2://Excel
                        fsfd.Filter = "File Excel(.xls, .xlsx)|*.xls;*.xlsx";
                        break;
                    case 3://Pdf
                        fsfd.Filter = "File Pdf(.pdf)|*.pdf";
                        break;
                    case 4://Image
                        fsfd.Filter = "File Image(.jpg)|*.jpg";
                        break;
                }

                if (fsfd.ShowDialog() == DialogResult.OK)
                {
                    switch (f.CateID)
                    {
                        case 1://Word
                            pie3DChart.ExportToRtf(fsfd.FileName);
                            break;
                        case 2://Excel
                            pie3DChart.ExportToXlsx(fsfd.FileName);
                            break;
                        case 3://Pdf
                            DevExpress.XtraPrinting.PdfExportOptions opt = new DevExpress.XtraPrinting.PdfExportOptions();
                            opt.ImageQuality = DevExpress.XtraPrinting.PdfJpegImageQuality.Highest;
                            opt.DocumentOptions.Author = "DIPVietnam";
                            opt.DocumentOptions.Subject = "Report";
                            pie3DChart.ExportToPdf(fsfd.FileName, opt);
                            break;
                        case 4://Image
                            pie3DChart.ExportToImage(fsfd.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                            break;
                    }

                    if (DialogBox.Question("Bạn có muốn mở file vừa export không?") == DialogResult.Yes)
                        System.Diagnostics.Process.Start(fsfd.FileName);
                }
            }
        }
    }
}
