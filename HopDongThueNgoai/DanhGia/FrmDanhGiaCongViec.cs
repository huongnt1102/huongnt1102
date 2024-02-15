using System.Linq;

namespace HopDongThueNgoai.DanhGia
{
    public partial class FrmDanhGiaCongViec : DevExpress.XtraEditors.XtraForm
    {
        private Library.MasterDataContext db;

        public FrmDanhGiaCongViec()
        {
            InitializeComponent();
        }

        private void FrmDanhGiaCongViec_Load(object sender, System.EventArgs e)
        {
            Library.TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Library.Common.User, barManager1);
            lkBuilding.DataSource = Library.Common.TowerList;
            itemBuilding.EditValue = Library.Common.User.MaTN;

            var objKbc = new Library.KyBaoCao();
            foreach (var item in objKbc.Source) cbxKbc.Items.Add(item);
            itemKbc.EditValue = objKbc.Source[1];

            SetDate(1);

            LoadData();
        }

        private void SetDate(int index)
        {
            var objKbc = new Library.KyBaoCao
            {
                Index = index
            };
            objKbc.SetToDate();
            itemDateFrom.EditValue = objKbc.DateFrom;
            itemDateTo.EditValue = objKbc.DateTo;
        }

        private void LoadData()
        {
            db = new Library.MasterDataContext();

            var listDanhGia = (from hd in db.hdctnDanhSachHopDongThueNgoais
                join kh in db.tnKhachHangs on hd.NhaCungCap equals kh.MaKH.ToString()
                join p in db.hdctnCongViec_DanhGias on hd.RowID equals p.HopDongId into dg
                from p in dg.DefaultIfEmpty()
                where hd.MaToaNha == itemBuilding.EditValue.ToString()
                select new
                {
                    HopDongNo = hd.SoHopDong,
                    DanhGia = p.DanhGia, Count = 1, KhachHang = kh.IsCaNhan == true? kh.HoKH + " "+kh.TenKH : kh.CtyTen, HopDongId = hd.RowID
                }).ToList();

            gc.DataSource = (from p in listDanhGia
                group new {p} by new {p.HopDongNo, p.KhachHang, p.HopDongId}
                into g
                select new
                {
                    g.Key.KhachHang, g.Key.HopDongNo, g.Key.HopDongId,
                    DanhGia = g.Sum(_=>_.p.Count) >0? g.Sum(_ => _.p.DanhGia).GetValueOrDefault() / g.Sum(_ => _.p.Count):0
                }).ToList();
        }

        private void LoadDetail()
        {
            try
            {
                db = new Library.MasterDataContext();
                var id = (int?) gv.GetFocusedRowCellValue("HopDongId");
                if (id == null) return;

                switch (xtraTabControl1.SelectedTabPage.Name)
                {
                    case "tabDanhGiaCongViec":
                        gcDanhGiaCongViec.DataSource = (from p in db.hdctnCongViec_DanhGias
                            where p.HopDongId == id
                            group new {p} by new {p.CongViecName}
                            into g
                            select new
                            {
                                g.Key.CongViecName, DanhGia = g.Count() > 0 ? g.Sum(_ => _.p.DanhGia) / g.Count() : 0
                            }).ToList();
                        break;
                    case "tabDanhSachNhanVien":
                        gcNhanVienThamGiaDanhGia.DataSource = (from p in db.hdctnCongViec_DanhGias
                            where p.HopDongId == id
                            group new {p} by new {p.UserName}
                            into g
                            select new {g.Key.UserName}).ToList();
                        break;
                }
            }
            catch{}
        }

        private void ItemRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void itemTaoPhanQuyen_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
        }

        private void itemAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var id = (int?) gv.GetFocusedRowCellValue("HopDongId");
            if (id == null)
            {
                Library.DialogBox.Alert("Vui lòng chọn hợp đồng");
                return;
            }

            using (var frm = new HopDongThueNgoai.DanhGia.FrmDanhGiaCongViecEdit(){ HopDongId = id})
            {
                frm.ShowDialog();
                if (frm.DialogResult == System.Windows.Forms.DialogResult.OK) LoadData();
            }
        }

        private void gv_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            LoadDetail();
        }

        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            LoadDetail();
        }
    }
}