using System;
using System.Data.Linq.SqlClient;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraCharts;
using DevExpress.XtraEditors;
using Library;

namespace Building.BieuDoMain
{
    public partial class CtlTinhHinhCongNoPhatSinh : DevExpress.XtraEditors.XtraUserControl
    {
        public CtlTinhHinhCongNoPhatSinh()
        {
            InitializeComponent();
        }

        private void CtlTinhHinhCongNo_Load(object sender, EventArgs e)
        {
            if (Common.User == null) return;
            ckCbxToaNha.DataSource = Common.TowerList;
            itemTn.EditValue = Common.User.MaTN;

            var objKbc = new KyBaoCao();
            foreach (var kbc in objKbc.Source) cmbKbc.Items.Add(kbc);
            itemKyBaoCao.EditValue = objKbc.Source[3];
            SetDate(3);
            LoadData();
        }
        private void SetDate(int index)
        {
            var objKbc = new KyBaoCao {Index = index};
            objKbc.SetToDate();
            itemTuNgay.EditValue = objKbc.DateFrom;
            itemDenNgay.EditValue = objKbc.DateTo;
        }

        private void LoadData()
        {
            try
            {
                Library.PhanQuyenBieuDo.SaveControl(GetType().Namespace + "." + Name, chartControl1.Titles[0].Lines[0].ToString(), GetType().Namespace + ".dll");

                var strToaNha = (itemTn.EditValue ?? "").ToString().TrimEnd(',').TrimStart(',').Replace(" ", "");
                var ltToaNha = strToaNha.Split(',');
                if (strToaNha == "") return;
                var tuNgay = (DateTime?)itemTuNgay.EditValue;
                var denNgay = (DateTime?)itemDenNgay.EditValue;
                using (var db = new MasterDataContext())
                {
                    var objKH = (from kh in db.tnKhachHangs
                                 where ltToaNha.Contains(kh.MaTN.ToString()) //&& kh.MaKH == 1944
                                 select new
                                 {
                                     kh.MaKH
                                 }).ToList(); 


                    var objHD_NDK = (from hd in db.dvHoaDons
                                     where SqlMethods.DateDiffDay(hd.NgayTT,tuNgay) > 0 & hd.IsDuyet == true && ltToaNha.Contains(hd.MaTN.ToString())
                                     // && hd.MaKH == 1944
                                     group hd by hd.MaKH into ndk
                                     select new
                                     {
                                         MaKH = ndk.Key,
                                         PhaiThu = ndk.Sum(s => s.PhaiThu)
                                     }).ToList();
                    var objSQ_NDK = (from sq in db.SoQuy_ThuChis
                                     where SqlMethods.DateDiffDay(sq.NgayPhieu, tuNgay) > 0 && ltToaNha.Contains(sq.MaTN.ToString()) && sq.IsPhieuThu == true && sq.MaLoaiPhieu != 24 //&& sq.MaKH == 1944
                                     group sq by sq.MaKH into ndk
                                     select new
                                     {
                                         MaKH = ndk.Key,
                                         NoDauKy = ndk.Sum(s => s.DaThu + s.KhauTru - s.ThuThua)
                                     }).ToList();

                    var objPhatSinh = (from hd in db.dvHoaDons
                                       where SqlMethods.DateDiffMonth(hd.NgayTT, tuNgay) == 0 & hd.IsDuyet == true && ltToaNha.Contains(hd.MaTN.ToString())
                                       // && hd.MaKH == 1846
                                       group hd by hd.MaKH
                                           into ps
                                       select new
                                       {
                                           MaKH = ps.Key,
                                           PhaiThu = ps.Sum(s => s.PhaiThu).GetValueOrDefault(),
                                       }).ToList();
                    var objDaThu = (from ct in db.SoQuy_ThuChis
                                    join hd in db.dvHoaDons on new { ct.TableName, ct.LinkID } equals new { TableName = "dvHoaDon", LinkID = (long?)hd.ID } into hoaDon
                                    from hd in hoaDon.DefaultIfEmpty()
                                    where System.Data.Linq.SqlClient.SqlMethods.DateDiffDay(tuNgay, ct.NgayPhieu) >= 0 &
                                    System.Data.Linq.SqlClient.SqlMethods.DateDiffDay(ct.NgayPhieu, denNgay) >= 0 &
                                    ct.IsPhieuThu == true &
                                    ct.MaLoaiPhieu != 24 &
                                    ltToaNha.Contains(ct.MaTN.ToString()) &
                                    ct.LinkID != null
                                    //where SqlMethods.DateDiffMonth(ct.NgayPhieu, tuNgay) == 0
                                    //      && ct.IsPhieuThu == true && ct.MaLoaiPhieu != 24 && ltToaNha.Contains(ct.MaTN.ToString())
                                    // && ct.MaKH == 1846
                                    group ct by ct.MaKH
                                        into dt
                                    select new
                                    {
                                        MaKH = dt.Key,
                                        DaThu = dt.Sum(s => s.DaThu).GetValueOrDefault()
                                    }).ToList();
                    var objKhauTru = (from ct in db.SoQuy_ThuChis
                                      where SqlMethods.DateDiffMonth(ct.NgayPhieu, tuNgay) == 0
                                      && ltToaNha.Contains(ct.MaTN.ToString()) && ct.IsPhieuThu == true
                                      // && ct.MaKH == 1846
                                      group ct by ct.MaKH
                                          into kt
                                      select new
                                      {
                                          MaKH = kt.Key,
                                          KhauTru = kt.Sum(s => s.KhauTru).GetValueOrDefault()
                                      }).ToList();

                    var objThuTruoc = (from sq in db.SoQuy_ThuChis
                                       where SqlMethods.DateDiffDay(sq.NgayPhieu, denNgay) >= 0 && ltToaNha.Contains(sq.MaTN.ToString())
                                      && sq.IsPhieuThu == true && sq.MaLoaiPhieu != 24 //&& sq.MaKH == 1846
                                                                                       // where SqlMethods.DateDiffDay(sq.NgayPhieu, _DenNgay) >= 0 && sq.MaTN == _MaTN 

                                       group sq by sq.MaKH
                                           into tt
                                       select new
                                       {
                                           MaKH = tt.Key,
                                           ThuTruoc = tt.Sum(s => s.ThuThua - s.KhauTru)
                                       }).ToList();
                    var objThuTruocTrongKy = (from sq in db.SoQuy_ThuChis
                                              where SqlMethods.DateDiffMonth(sq.NgayPhieu, tuNgay) == 0 && ltToaNha.Contains(sq.MaTN.ToString())
                                      && sq.IsPhieuThu == true && sq.MaLoaiPhieu != 24 //&& sq.MaKH == 1846
                                              group sq by sq.MaKH
                                                  into tt
                                              select new
                                              {
                                                  MaKH = tt.Key,
                                                  ThuTruoc = tt.Sum(s => s.ThuThua)
                                              }).ToList();

                    var objList = (from kh in objKH
                                   join ndk in objHD_NDK on kh.MaKH equals ndk.MaKH into nodk
                                   from ndk in nodk.DefaultIfEmpty()
                                   join sqdk in objSQ_NDK on kh.MaKH equals sqdk.MaKH into soquydk
                                   from sqdk in soquydk.DefaultIfEmpty()
                                   join ps in objPhatSinh on kh.MaKH equals ps.MaKH into psinh
                                   from ps in psinh.DefaultIfEmpty()
                                   join dt in objDaThu on kh.MaKH equals dt.MaKH into dthu
                                   from dt in dthu.DefaultIfEmpty()
                                   join kt in objKhauTru on kh.MaKH equals kt.MaKH into ktru
                                   from kt in ktru.DefaultIfEmpty()
                                   join tt in objThuTruoc on kh.MaKH equals tt.MaKH into ttruoc
                                   from tt in ttruoc.DefaultIfEmpty()
                                   join tttk in objThuTruocTrongKy on kh.MaKH equals tttk.MaKH into tttrongky
                                   from tttk in tttrongky.DefaultIfEmpty()
                                   select new
                                   {
                                       kh.MaKH,
                                       NoDauKy = (ndk == null ? 0 : ndk.PhaiThu.GetValueOrDefault()) - (sqdk == null ? 0 : sqdk.NoDauKy.GetValueOrDefault()),
                                       PhatSinh = ps == null ? 0 : ps.PhaiThu,
                                       DaThu = dt == null ? 0 : dt.DaThu,
                                       KhauTru = kt == null ? 0 : kt.KhauTru,
                                       ThuTruoc = tt == null ? 0 : tt.ThuTruoc,
                                       ThuTruocTK = tttk == null ? 0 : tttk.ThuTruoc,
                                   }).Select(p => new
                                   {
                                       ThuTruoc = p.ThuTruoc,
                                       NoDauKy = p.NoDauKy < 0 ? 0 : p.NoDauKy,
                                       PhatSinh = p.PhatSinh,
                                       KhauTru = p.KhauTru,
                                       DaThu = p.DaThu,
                                       ConNo = ((p.NoDauKy < 0 ? 0 : p.NoDauKy) + p.PhatSinh - (p.DaThu + p.KhauTru - p.ThuTruocTK)) < 0 ? 0 : ((p.NoDauKy < 0 ? 0 : p.NoDauKy) + p.PhatSinh - (p.DaThu + p.KhauTru - p.ThuTruocTK)),
                                       MaKH = p.MaKH,
                                       NoCuoi = (((p.NoDauKy < 0 ? 0 : p.NoDauKy) + p.PhatSinh - (p.DaThu + p.KhauTru - p.ThuTruocTK)) < 0 ? 0 : ((p.NoDauKy < 0 ? 0 : p.NoDauKy) + p.PhatSinh - (p.DaThu + p.KhauTru - p.ThuTruocTK))) - p.ThuTruoc
                                   });

                    chartControl1.DataSource = null;
                    chartControl1.Series.Clear();
                    chartControl1.RefreshData();

                    Series series1 = new Series("Tình hình công nợ", ViewType.Pie);
                    series1.Points.Clear();
                    series1.Points.Add(new SeriesPoint("Nợ đầu kỳ",new double[]{Convert.ToDouble(objList.Sum(_=>_.NoDauKy))}));
                    series1.Points.Add(new SeriesPoint("Phát sinh", new double[] { Convert.ToDouble(objList.Sum(_ => _.PhatSinh)) }));
                    series1.Points.Add(new SeriesPoint("Đã thu", new double[] { Convert.ToDouble(objList.Sum(_ => _.DaThu)) }));
                    series1.Points.Add(new SeriesPoint("Khấu trừ", new double[] { Convert.ToDouble(objList.Sum(_ => _.KhauTru)) }));
                    series1.Points.Add(new SeriesPoint("Thu trước", new double[] { Convert.ToDouble(objList.Sum(_ => _.ThuTruoc)) }));

                    series1.PointOptions.ValueNumericOptions.Format = NumericFormat.Number;
                    series1.PointOptions.ValueNumericOptions.Precision = 2;
                    series1.LegendTextPattern = "{A}: {V:n0}";
                    //((PieSeriesLabel)series1.Label).PointOptions.Pattern = "{A}: {V}";
                    ((PieSeriesLabel)series1.Label).Position = PieSeriesLabelPosition.TwoColumns;
                    ((PieSeriesLabel)series1.Label).ResolveOverlappingMode = ResolveOverlappingMode.Default;
                    ((PieSeriesLabel)series1.Label).PointOptions.ValueNumericOptions.Format = NumericFormat.Percent;
                    ((PieSeriesLabel)series1.Label).PointOptions.ValueNumericOptions.Precision = 0;

                    PieSeriesLabel label = (PieSeriesLabel)series1.Label;
                    label.TextPattern = "{A}: {VP:P0}";

                    PieSeriesView myView = (PieSeriesView)series1.View;

                    myView.ExplodedPointsFilters.Add(new SeriesPointFilter(SeriesPointKey.Value_1,
                        DataFilterCondition.GreaterThanOrEqual, 9));
                    myView.ExplodedPointsFilters.Add(new SeriesPointFilter(SeriesPointKey.Argument,
                        DataFilterCondition.NotEqual, "Others"));
                    myView.ExplodeMode = PieExplodeMode.UseFilters;

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

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void cmbKbc_EditValueChanged(object sender, EventArgs e)
        {
            SetDate(((ComboBoxEdit)sender).SelectedIndex);
        }
    }
}
