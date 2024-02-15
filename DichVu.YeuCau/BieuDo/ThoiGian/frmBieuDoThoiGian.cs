using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Library;
using System.Linq;
using System.Data.Linq.SqlClient;
using DevExpress.XtraCharts;
namespace DichVu.YeuCau.BieuDo.ThoiGian
{
    public partial class frmBieuDoThoiGian : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db = new MasterDataContext();
        bool first = false;
        public frmBieuDoThoiGian()
        {
            InitializeComponent();
        }
        int LayThuCuaTuan(DateTime dt)
        {
            switch (dt.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    return 2;
                case DayOfWeek.Tuesday:
                    return 3;
                case DayOfWeek.Wednesday:
                    return 4;
                case DayOfWeek.Thursday:
                    return 5;
                case DayOfWeek.Friday:
                    return 6;
                case DayOfWeek.Saturday:
                    return 7;
                case DayOfWeek.Sunday:
                    return 8;
                default:
                    return 0;
            }
            return 0;
        }
        bool ThuocNgayLe(byte MaTN, int Nam, DateTime dtKT)
        {
            var objKT = db.tbl_ThietLapNgayNghis.FirstOrDefault(p => p.MaTN == MaTN && p.Nam == Nam);
            if (objKT != null)
            {
                foreach (var item in objKT.tbl_CauHinhNgayNghis)
                {
                    if (SqlMethods.DateDiffDay(item.NgayBD, dtKT) >= 0 && SqlMethods.DateDiffDay(dtKT, item.NgayKT) >= 0)
                        return true;
                }
            }
            return false;
        }
        private void frmBieuDoThoiGian_Load(object sender, EventArgs e)
        {
            lkToaNha.DataSource = Common.TowerList;
            itemToaNha.EditValue = Common.User.MaTN;
            KyBaoCao objKBC = new KyBaoCao();
            foreach (string str in objKBC.Source)
            {
                cmbKyBaoCao.Items.Add(str);
            }
            itemKyBaoCao.EditValue = objKBC.Source[3];
            SetDate(3);
        }
        bool GioQuyDinh(DateTime dtkt)
        {
            if ((dtkt.Hour >= 7 && dtkt.Hour <= 11) || (dtkt.Hour >= 13 && dtkt.Hour <= 17))
            {
                return true;
            }
            return false;
        }
        public class BaoCaoThoiGian
        {
            public int GroupProcessId { set; get; }
            public int MaYC { set; get; }
            public int SoNgay { set; get; }
        }
        void LoadData()
        {
            chartControl1.Series.Clear();
            byte MaTN = (byte)itemToaNha.EditValue;
            int Nam = Convert.ToDateTime(itemTuNgay.EditValue).Year;
            var tuNgay = (DateTime)itemTuNgay.EditValue;
            var denNgay = (DateTime)itemDenNgay.EditValue;
            var objTG = db.tbl_ThietLapNgayNghis.FirstOrDefault(p => p.MaTN == MaTN && p.Nam == Nam);
            int NgayGioiHan = 8;
            if (objTG != null)
            {
                if (objTG.IsThuBay.GetValueOrDefault() && objTG.IsChuNhat.GetValueOrDefault())
                {
                    NgayGioiHan = 7;
                }
            }
            var objData = (from p in db.tnycYeuCaus
                           where SqlMethods.DateDiffDay(tuNgay, p.NgayYC.Value) >= 0
                                 && SqlMethods.DateDiffDay(p.NgayYC.Value, denNgay) >= 0
                                 && p.MaTN == MaTN && (p.MaTT == 5 || p.MaTT==3) && p.GroupProcessId != null
                           select new
                           {
                               p.GroupProcessId,
                               MaYC = p.ID,
                               NgayTiepNhan = db.tnycLichSuCapNhats.FirstOrDefault(n => n.MaYC == p.ID && n.MaTT == 2).NgayCN,
                               NgayHoanThanh = db.tnycLichSuCapNhats.FirstOrDefault(n => n.MaYC == p.ID && n.MaTT == 3).NgayCN,
                           }).ToList();
            List<BaoCaoThoiGian> lstBC = new List<BaoCaoThoiGian>();
            foreach (var item in objData.Where(p=>p.NgayHoanThanh!=null && p.NgayTiepNhan!=null))
            {
                int SoGio = 0;
                for (DateTime date = item.NgayTiepNhan.Value; date < item.NgayHoanThanh.Value; date = date.AddHours(1.0))
                {
                    if (!ThuocNgayLe(MaTN, Nam, date) || (LayThuCuaTuan(date) < NgayGioiHan))
                    {
                        if (GioQuyDinh(date))
                        {
                            SoGio += 1;
                        }
                    }
                }
                var itemBC = new BaoCaoThoiGian();
                itemBC.GroupProcessId = item.GroupProcessId.Value;
                itemBC.MaYC = item.MaYC;
                itemBC.SoNgay = SoGio;
                lstBC.Add(itemBC);
            }
            var objTrungBinh = (from bc in lstBC
                                group bc by bc.GroupProcessId into key
                                select new
                                {
                                    GroupProcessId = key.Key,
                                    SoNgayTB =key.Sum(p=>p.SoNgay)
                                });
            var objNhomCV = (from cv in db.app_GroupProcesses
                             select new
                             {
                                 cv.Id,
                                 cv.Name,
                                 NgayThucHien = cv.TimeFinish
                             }).ToList();

            var objAll = (from n in objNhomCV
                          join tb in objTrungBinh on n.Id equals tb.GroupProcessId into _tbinh
                          from tb in _tbinh.DefaultIfEmpty()
                          select new
                          {
                              n.Id,
                              TenNhomCV=n.Name,
                              ThoiGianChuan = n.NgayThucHien == null? 0: n.NgayThucHien,
                              ThoiGianThucHien = tb == null ? 0 : tb.SoNgayTB/8
                          }).ToList();
            gridControl1.DataSource = objAll;
            Series series1 = new Series("", ViewType.Bar);
            Series series2 = new Series("", ViewType.Line);

            try
            {
                // Add points to the series.
                foreach (var l in objAll)
                {
                    series1.Points.Add(new SeriesPoint(l.TenNhomCV.ToString(), l.ThoiGianChuan));
                    series2.Points.Add(new SeriesPoint(l.TenNhomCV.ToString(), l.ThoiGianThucHien));
                }
            }
            catch (Exception ex)
            {
                Library.DialogBox.Alert(ex.Message);
            }
            

            // Add both series to the chart.
            chartControl1.Series.AddRange(new Series[] { series1, series2 });


            /// Access labels of the first series.
            ((BarSeriesLabel)series1.Label).Visible = true;
            ((BarSeriesLabel)series1.Label).ResolveOverlappingMode = ResolveOverlappingMode.Default;

            ((PointSeriesLabel)series2.Label).Visible = true;
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
        private void cmbKyBaoCao_EditValueChanged(object sender, EventArgs e)
        {
            SetDate((sender as ComboBoxEdit).SelectedIndex);
        }

        //private void itemTuNgay_EditValueChanged(object sender, EventArgs e)
        //{
        //    if (!first) LoadData();
        //}

        //private void itemDenNgay_EditValueChanged(object sender, EventArgs e)
        //{
        //    if (!first) LoadData();
        //}
        void SetDate(int index)
        {
            var objKBC = new KyBaoCao()
            {
                Index = index
            };
            objKBC.SetToDate();

            itemTuNgay.EditValue = objKBC.DateFrom;
            itemDenNgay.EditValue = objKBC.DateTo;
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void itemIn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            chartControl1.ShowPrintPreview();
        }
    }
}