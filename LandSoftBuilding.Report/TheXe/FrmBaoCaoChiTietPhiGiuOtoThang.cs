using System;
using System.Collections.Generic;
using System.Data.Linq.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using Library;

namespace LandSoftBuilding.Report.TheXe
{
    public partial class FrmBaoCaoChiTietPhiGiuOtoThang : XtraForm
    {
        public FrmBaoCaoChiTietPhiGiuOtoThang()
        {
            InitializeComponent();
        }

        private void FrmBaoCaoChiTietPhiGiuOtoThang_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);

            cbxToaNha.DataSource = Common.TowerList;
            itemToaNha.EditValue = Common.User.MaTN;

            var objKbc = new KyBaoCao();
            foreach (var item in objKbc.Source) cbxKbc.Items.Add(item);
            itemKyBaoCao.EditValue = objKbc.Source[3];
            SetDate(3);
            LoadData();
        }

        public void SetDate(int index)
        {
            var objKbc = new KyBaoCao {Index = index};
            objKbc.SetToDate();
            itemTuNgay.EditValue = objKbc.DateFrom;
            itemDenNgay.EditValue = objKbc.DateTo;
        }

        public async void LoadData()
        {
            var db = new MasterDataContext();
            try
            {
                if (itemTuNgay.EditValue == null || itemDenNgay.EditValue == null) return;
                var tuNgay = (DateTime) itemTuNgay.EditValue;
                var denNgay = (DateTime) itemDenNgay.EditValue;
                var strToaNha = (itemToaNha.EditValue ?? "").ToString().TrimEnd(',').TrimStart(',').Replace(" ", "");
                var ltToaNha = strToaNha.Split(',');
                if (strToaNha == "") return;

                var listHd = new List<ListHdBcChiTietGiuXeOto>();
                List<BcChiTietGiuXeOto> list1 = new List<BcChiTietGiuXeOto>(), list = new List<BcChiTietGiuXeOto>(), objList = new List<BcChiTietGiuXeOto>();

                await Task.Run(() =>
                {
                    listHd = (from hd in db.dvHoaDons
                        where ltToaNha.Contains(hd.MaTN.ToString()) & hd.IsDuyet.GetValueOrDefault() &
                              hd.MaLDV == 6 &
                              SqlMethods.DateDiffDay(tuNgay, hd.NgayTT) >= 0 &
                              SqlMethods.DateDiffDay(hd.NgayTT, denNgay) >= 0
                        select new ListHdBcChiTietGiuXeOto
                        {
                            PhaiThu = hd.PhaiThu.GetValueOrDefault(), LinkID = hd.LinkID, MaMB = hd.MaMB,
                            MaKH = hd.MaKH
                        }).ToList();
                });

                await Task.Run(() =>
                {
                    list1 = (from hd in listHd
                        join tx in db.dvgxTheXes on hd.LinkID equals tx.ID
                        join lx in db.dvgxLoaiXes on tx.MaLX equals lx.MaLX
                        join mb in db.mbMatBangs on hd.MaMB equals mb.MaMB
                        join kh in db.tnKhachHangs on hd.MaKH equals kh.MaKH
                        orderby mb.MaMB
                        select new BcChiTietGiuXeOto
                        {
                            MaCanHo = mb.MaSoMB,
                            MaPhu = kh.KyHieu,
                            TenKhachHang = kh.IsCaNhan == true ? kh.HoKH + " " + kh.TenKH : kh.CtyTen,
                            XeOTo = lx.MaNX == 3 ? hd.PhaiThu : 0
                        }).ToList();
                });

                await Task.Run(() =>
                {
                    list = (from hd in list1
                        group new {hd} by
                            new
                            {
                                hd.MaCanHo, hd.MaPhu,
                                hd.TenKhachHang
                            }
                        into g
                        select new BcChiTietGiuXeOto
                        {
                            MaCanHo = g.Key.MaCanHo, MaPhu = g.Key.MaPhu, TenKhachHang = g.Key.TenKhachHang,
                            XeOTo = g.Sum(_ => _.hd.XeOTo)
                        }).ToList();
                });

                await Task.Run(() =>
                {
                    objList = (from hd in list
                        where hd.XeOTo != 0
                        select new BcChiTietGiuXeOto
                        {
                            MaCanHo = hd.MaCanHo, MaPhu = hd.MaPhu, TenKhachHang = hd.TenKhachHang, XeOTo = hd.XeOTo
                        }).ToList();
                });
                gc.DataSource = objList;
            }
            catch{}
        }

        public class ListHdBcChiTietGiuXeOto
        {
            public decimal? PhaiThu { get; set; }
            public int? LinkID { get; set; }
            public int? MaMB { get; set; }
            public int? MaKH { get; set; }
        }

        public class BcChiTietGiuXeOto
        {
            public string MaCanHo { get; set; }
            public string MaPhu { get; set; }
            public string TenKhachHang { get; set; }

            public decimal? XeOTo { get; set; }
        }

        private void CbxKbc_EditValueChanged(object sender, EventArgs e)
        {
            SetDate(((ComboBoxEdit)sender).SelectedIndex);
        }

        private void ItemRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void ItemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Commoncls.ExportExcel(gc);
        }

        private void Gv_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
                var size = e.Graphics.MeasureString(e.Info.DisplayText, e.Appearance.Font);
                var width = Convert.ToInt32(size.Width) + 20;
                BeginInvoke(new MethodInvoker(delegate { Cal(width, gv); }));
            }
        }
        private bool Cal(int width, GridView view)
        {
            view.IndicatorWidth = view.IndicatorWidth < width ? width : view.IndicatorWidth;
            return true;
        }
    }
}