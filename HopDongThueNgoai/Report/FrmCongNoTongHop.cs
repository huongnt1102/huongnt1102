using System.Linq;

namespace HopDongThueNgoai.Report
{
    public partial class FrmCongNoTongHop : DevExpress.XtraEditors.XtraForm
    {
        private Library.MasterDataContext _db;
        private System.Collections.Generic.List<string> _lError = new System.Collections.Generic.List<string>();

        public FrmCongNoTongHop()
        {
            InitializeComponent();
        }

        private void FrmCongNoTongHop_Load(object sender, System.EventArgs e)
        {
            Library.TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Library.Common.User, barManager1);

            lkBuilding.DataSource = Library.Common.TowerList;
            itemBuilding.EditValue = Library.Common.User.MaTN;

            LoadData();
        }

        private async void LoadData()
        {
            try
            {
                _db = new Library.MasterDataContext();

                System.Collections.Generic.List<HopDongThueNgoai.Class.CongNo.CongNoNhaCungCap> congNo = new System.Collections.Generic.List<Class.CongNo.CongNoNhaCungCap>(),
                    tongCongNo = new System.Collections.Generic.List<Class.CongNo.CongNoNhaCungCap>();
                System.Collections.Generic.List<HopDongThueNgoai.Class.CongNo.CongNoHopDong> danhSachHopDong = new System.Collections.Generic.List<Class.CongNo.CongNoHopDong>();

                await System.Threading.Tasks.Task.Run(() =>
                {
                    congNo = (from _ in _db.hdctnCongNoNhaCungCaps
                              where _.BuildingId == (byte?)itemBuilding.EditValue
                              select new HopDongThueNgoai.Class.CongNo.CongNoNhaCungCap
                              {
                                  KhachHangId = _.KhachHangId,
                                  HopDongId = _.HopDongId,
                                  DaTra = _.IsPhieuChi == true ? _.SoTien : -_.SoTien
                              }).ToList();
                });

                await System.Threading.Tasks.Task.Run(() =>
                {
                    tongCongNo = (from _ in congNo
                                  group new { _ } by new { _.KhachHangId, _.HopDongId }
                                      into g
                                      select new HopDongThueNgoai.Class.CongNo.CongNoNhaCungCap
                                      {
                                          HopDongId = g.Key.HopDongId,
                                          KhachHangId = g.Key.KhachHangId,
                                          DaTra = g.Sum(c => c._.DaTra).GetValueOrDefault()
                                      }).ToList();
                });

                await System.Threading.Tasks.Task.Run(() =>
                {
                    danhSachHopDong = (from _ in _db.hdctnDanhSachHopDongThueNgoais
                                       join kh in _db.tnKhachHangs on _.NhaCungCap equals kh.MaKH.ToString()
                                       join tt in _db.hdctnTrangThais on _.TrangThai equals tt.MaTrangThai
                                       where _.MaToaNha == itemBuilding.EditValue.ToString()
                                       select new HopDongThueNgoai.Class.CongNo.CongNoHopDong
                                       {
                                           HopDongId = _.RowID,
                                           KhachHangId = kh.MaKH,
                                           HopDongNo = _.SoHopDong,
                                           TongTienHopDong = _.TienSauThue,
                                           KhachHangName = kh.IsCaNhan == true ? kh.HoKH + " " + kh.TenKH : kh.CtyTen,
                                           TrangThaiName = tt.TenTrangThai
                                       }).ToList();
                });

                gc.DataSource = (from hd in danhSachHopDong
                    select new
                    {
                        hd.HopDongId,
                        hd.KhachHangId,
                        HoTenKh = hd.KhachHangName,
                        SoHopDong = hd.HopDongNo,
                        PhaiTra = hd.TongTienHopDong,
                        DaTra = tongCongNo.Where(_ => _.HopDongId == hd.HopDongId & _.KhachHangId == hd.KhachHangId).Sum(_ => _.DaTra),
                        ConLai = hd.TongTienHopDong - tongCongNo.Where(_ => _.HopDongId == hd.HopDongId & _.KhachHangId == hd.KhachHangId).Sum(_ => _.DaTra),
                        TrangThai = hd.TrangThaiName
                    }).ToList();
            }
            catch (System.Exception ex)
            {
                _lError.Add("LoadData: " + ex.Message);
            }
        }

        private void LoadDetail()
        {
            _db = new Library.MasterDataContext();
            try
            {
                var hopDongId = gv.GetFocusedRowCellValue("HopDongId");
                if (hopDongId == null) return;

                switch (xtraTabControl1.SelectedTabPage.Name)
                {
                    case "tabLichSuThanhToan":
                        gcLichSuThanhToan.DataSource = _db.hdctnCongNoNhaCungCaps
                            .Where(_ => _.LichThanhToanId != null & _.HopDongId == (int) hopDongId).ToList();
                        break;
                    case "tabLichSuThanhLy":
                        gcLichSuThanhLy.DataSource = _db.hdctnCongNoNhaCungCaps
                            .Where(_ => _.ThanhLyId != null & _.HopDongId == (int) hopDongId).ToList();
                        break;
                }
            }
            catch(System.Exception ex)
            {
                _lError.Add("LoadDetails: " + ex.Message);
            }
        }

        private void ItemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Library.Commoncls.ExportExcel(gc);
        }

        private void ItemRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void ItemTaoPhanQuyen_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void gv_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            LoadDetail();
        }

        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            LoadDetail();
        }
    }
}