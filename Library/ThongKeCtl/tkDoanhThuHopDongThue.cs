using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using DevExpress.XtraCharts;
using DevExpress.XtraEditors.Controls;

namespace Library.ThongKeCtl
{
    public partial class tkDoanhThuHopDongThue : DevExpress.XtraEditors.XtraUserControl
    {
        public tkDoanhThuHopDongThue()
        {
            InitializeComponent();
            LoadKBC();
            LoadToaNha();
            LoadData();
        }

        private void LoadKBC()
        {
            for (int i = 2000; i < 2030; i++)
            {
                DateTime newdt = new DateTime(i, 1, 1);
                repositoryItemComboBoxKyBaoCao.Items.Add(i);
            }
            itemKyBC.EditValue = DateTime.Now.Year;
        }

        private void LoadToaNha()
        {
            lookToaNha.NullText = "Chọn Dự án";
            using (var db = new MasterDataContext())
            {
                lookToaNha.DataSource = db.tnToaNhas
                            .Select(p => new
                            {
                                p.MaTN,
                                p.TenTN,
                                p.TenVT
                            });
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
            if (itemKyBC.EditValue != null & itemtoanha.EditValue != null)
            {
                using (MasterDataContext db = new MasterDataContext())
                {
                    var objtoanha = db.tnToaNhas.Single(p => p.MaTN == (byte)itemtoanha.EditValue);
                    chart.Series.Clear();
                    chart.Titles.Clear();

                    Series bar = new Series("Doanh thu trong năm", ViewType.Bar);
                    bar.DataSource = LayDuLieu((int)itemKyBC.EditValue, objtoanha);
                    bar.ArgumentDataMember = "_ThoiGian";
                    bar.ArgumentScaleType = ScaleType.Qualitative;
                    bar.ValueDataMembers.AddRange(new string[] { "_SoTien" });
                    bar.ValueScaleType = ScaleType.Numerical;
                    chart.Dock = DockStyle.Fill;
                    chart.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Right;
                    chart.Legend.AlignmentVertical = LegendAlignmentVertical.Top;

                    BarSeriesLabel label = (BarSeriesLabel)bar.Label;
                    label.PointOptions.PointView = PointView.Values;
                    label.PointOptions.ValueNumericOptions.Format = NumericFormat.Currency;
                    label.PointOptions.ValueNumericOptions.Precision = 0;
                    label.TextOrientation = TextOrientation.BottomToTop;

                    chart.Series.Add(bar);
                }
            }
            
        }

        private List<DoanhThuTrongNam> LayDuLieu(int NamTinhToan, tnToaNha objtoanha)
        {
            List<DoanhThuTrongNam> lstdt = new List<DoanhThuTrongNam>();
            using (MasterDataContext db = new MasterDataContext())
            {
                var TongHopDong = db.thueCongNos
                    .Where(p => p.thueHopDong.mbMatBang.mbTangLau.mbKhoiNha.tnToaNha == objtoanha & p.ConNo <= 0
                        & p.ChuKyMax.Value.Year == NamTinhToan).ToList();
                var tygia = db.tnTyGias.ToList();
                DateTime now = db.GetSystemDate();
                for (int i = 1; i <= 12; i++)
                {
                    DoanhThuTrongNam dt = new DoanhThuTrongNam();
                    dt._ThoiGian = string.Format("Tháng {0}", i);
                    TongHopDong.ForEach(p =>
                    {
                        if (p.ChuKyMax.Value.Month == i)
                        {
                            foreach (var tg in tygia)
                            {
                                if (p.thueHopDong.MaTG == tg.MaTG)
                                {
                                    dt._SoTien += p.DaThanhToan.Value * tg.TyGia.Value;
                                }
                            }
                        }
                    });
                    lstdt.Add(dt);
                }
            }
            return lstdt;
        }

        public class DoanhThuTrongNam
        {
            private string ThoiGian;

            public string _ThoiGian
            {
                get { return ThoiGian; }
                set { ThoiGian = value; }
            }

            private decimal SoTien;

            public decimal _SoTien
            {
                get { return SoTien; }
                set { SoTien = value; }
            }

            public DoanhThuTrongNam()
            {
            }

            public DoanhThuTrongNam(string tg, decimal st)
            {
                ThoiGian = tg;
                SoTien = st;
            }
        }

        private void btnNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
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
