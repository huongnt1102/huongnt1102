using System;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraCharts;
using DevExpress.XtraEditors;
using Library;

namespace Building.BieuDoMain
{
    public partial class CtlSoCanBanGiaoNoiBoTong : DevExpress.XtraEditors.XtraUserControl
    {
        public CtlSoCanBanGiaoNoiBoTong()
        {
            InitializeComponent();
        }

        private void CtlSoCanBanGiaoNoiBo_Load(object sender, EventArgs e)
        {
            if (Common.User == null) return;
            chkBuilding.DataSource = Common.TowerList;
            itemBuilding.EditValue = Common.User.MaTN;

            var objKbc = new KyBaoCao();
            foreach (var item in objKbc.Source) cmbKbc.Items.Add(item);
            itemKyBaoCao.EditValue= objKbc.Source[3];

            SetDate(3);
            LoadData();
        }

        private void SetDate(int index)
        {
            var objKbc = new KyBaoCao { Index = index };
            objKbc.SetToDate();
            itemTuNgay.EditValue = objKbc.DateFrom;
            itemDenNgay.EditValue = objKbc.DateTo;
        }

        private void LoadData()
        {
            try
            {
                Library.PhanQuyenBieuDo.SaveControl(GetType().Namespace + "." + Name, chartControl1.Titles[0].Lines[0].ToString(), GetType().Namespace + ".dll");

                var strToaNha = (itemBuilding.EditValue ?? "").ToString().TrimEnd(',').TrimStart(',').Replace(" ", "");
                var ltToaNha = strToaNha.Split(',');
                if (strToaNha == "") return;
                var tuNgay = (DateTime?) itemTuNgay.EditValue;
                var denNgay = (DateTime?) itemDenNgay.EditValue;
                using (var db = new MasterDataContext())
                {
                    var status = (from p in db.ho_ScheduleApartments
                        where ltToaNha.Contains(p.BuildingId.ToString()) &
                              //SqlMethods.DateDiffDay(tuNgay, p.DateHandoverFrom) >= 0 &
                              //SqlMethods.DateDiffDay(p.DateHandoverFrom, denNgay) >= 0 &
                              p.IsLocal == true & p.ApartmentId!=null
                              //& (p.StatusId == 1 | p.StatusId == 2 | p.StatusId == 3 | p.StatusId == 4 | p.StatusId == 9)
                        group new {p} by new {p.ApartmentId}
                        into g
                        select new {g.Key.ApartmentId,Count = g.Count()}).ToList();
                    var statusTc = (from p in db.ho_ScheduleApartments
                        where ltToaNha.Contains(p.BuildingId.ToString()) &
                              //SqlMethods.DateDiffDay(tuNgay, p.DateHandoverFrom) >= 0 &
                              //SqlMethods.DateDiffDay(p.DateHandoverFrom, denNgay) >= 0 &
                              p.IsLocal == false//&
                        //(p.StatusId == 1 | p.StatusId == 2 | p.StatusId == 3 | p.StatusId == 4 | p.StatusId == 9)
                        group new { p } by new { p.ApartmentId }
                        into g
                        select new { g.Key.ApartmentId, Count = -g.Count() }).ToList();

                    var s1 = status.Concat(statusTc).ToList();
                    var s = (from p in s1
                        group new {p} by new {p.ApartmentId}
                        into g
                        select new {g.Key.ApartmentId, Count = g.Sum(_ => _.p.Count)}).ToList();

                    var getS = (from ss in s
                                join p in db.ho_ScheduleApartments on ss.ApartmentId equals p.ApartmentId
                                where ss.Count > 0 & p.IsLocal == true
                                group new { p,ss } by new { p.StatusId, p.StatusName } into g
                                select new { g.Key.StatusId, g.Key.StatusName, Count = g.Sum(_ => _.ss.Count) }).ToList();

                    //var getS = (from p in db.ho_ScheduleApartments
                    //            where ltToaNha.Contains(p.BuildingId.ToString())
                    //                   & SqlMethods.DateDiffDay(tuNgay, p.DateHandoverFrom) >= 0 &
                    //                   SqlMethods.DateDiffDay(p.DateHandoverFrom, denNgay) >= 0
                    //                  & p.IsLocal == true
                    //                  & (p.StatusId == 1 | p.StatusId == 2 | p.StatusId == 3 | p.StatusId == 4 | p.StatusId == 9)
                    //            group new { p } by new { p.StatusId, p.StatusName }
                    //                into g
                    //                select new { g.Key.StatusId, g.Key.StatusName, Count = g.Count() }).ToList();

                    //var tongStatus = (from p in db.ho_ScheduleApartments
                    //    where ltToaNha.Contains(p.BuildingId.ToString()) & p.IsLocal == true
                    //    group new {p} by new {p.ApartmentId}
                    //    into g
                    //    select new {Count = g.Count()}).ToList();

                    var tongMatBang = (from p in db.mbMatBangs
                        where ltToaNha.Contains(p.MaTN.ToString())
                        group new {p} by new {p.MaMB}
                        into g
                        select new {Count = g.Count()}).ToList();

                    chartControl1.DataSource = null;
                    chartControl1.Series.Clear();
                    chartControl1.RefreshData();

                    Series series1 = new Series("Trạng thái", ViewType.Pie);
                    series1.Points.Clear();

                    var tong = (double) status.Sum(_ => _.Count);
                    var tongS = (double) tongMatBang.Sum(_ => _.Count);
                    var chuaDuaVaoKeHoach = tongS - tong;
                    if (chuaDuaVaoKeHoach > 0)
                    {
                        series1.Points.Add(new SeriesPoint("Chưa đưa vào kế hoạch bàn giao", chuaDuaVaoKeHoach));
                    }

                    //series1.Points.Add(new SeriesPoint("Tổng", new double[] {tong}));

                    foreach (var item in getS)
                    {
                        var statusName = item.StatusName;
                        if (item.StatusName == "Chờ bàn giao khách hàng")
                            statusName = "Bàn giao nội bộ thành công";
                        var point = new SeriesPoint();
                        point.Argument = statusName;
                        point.Tag = statusName;
                        //point.Values = new double[] { Convert.ToDouble(item.Count) / tong };
                        point.Values = new double[] { Convert.ToDouble(item.Count) };
                        series1.Points.Add(point);
                    }

                    //series1.LegendPointOptions.PointView = PointView.Argument;
                    series1.PointOptions.ValueNumericOptions.Format = NumericFormat.Number;
                    series1.PointOptions.ValueNumericOptions.Precision = 2;
                    series1.LegendTextPattern = "{A}: {V:n0}/{TV} ({VP:P0})";
                    //((PieSeriesLabel)series1.Label).PointOptions.Pattern = "{A}: {V}";
                    ((PieSeriesLabel) series1.Label).Position = PieSeriesLabelPosition.TwoColumns;
                    ((PieSeriesLabel) series1.Label).ResolveOverlappingMode = ResolveOverlappingMode.Default;
                    ((PieSeriesLabel) series1.Label).PointOptions.ValueNumericOptions.Format = NumericFormat.Percent;
                    ((PieSeriesLabel) series1.Label).PointOptions.ValueNumericOptions.Precision = 0;

                    PieSeriesLabel label = (PieSeriesLabel) series1.Label;
                    label.TextPattern = "{A}: {VP:P0}";

                    PieSeriesView myView = (PieSeriesView) series1.View;

                    myView.ExplodedPointsFilters.Add(new SeriesPointFilter(SeriesPointKey.Argument,
                        DataFilterCondition.NotEqual, "MinValue"));
                    myView.ExplodeMode = PieExplodeMode.UseFilters;
                    myView.ExplodedDistancePercentage = 3;
                    myView.RuntimeExploding = true;

                    chartControl1.Series.Add(series1);
                    chartControl1.Dock = DockStyle.Fill;


                }
            }
            catch (Exception e)
            {
                //
            }
        }

        private void ItemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void cmbKbc_EditValueChanged(object sender, EventArgs e)
        {
            SetDate(((ComboBoxEdit)sender).SelectedIndex);
        }

        private void itemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            chartControl1.ShowPrintPreview();
        }
    }
}
