using System.Linq;

namespace Deposit.DoiTac
{
    public partial class FrmDoiTac : DevExpress.XtraEditors.XtraForm
    {
        public byte? BuildingId { get; set; }
        public int? HopDongDatCocId { get; set; }
        public string HopDongDatCocNo { get; set; }
        public int? DoiTacId { get; set; }

        private Library.MasterDataContext _db = new Library.MasterDataContext();
        private Library.Dep_DoiTac _doiTac;
        private string _tenKh, _tenMb;

        public FrmDoiTac()
        {
            InitializeComponent();
        }

        private void FrmDoiTac_Load(object sender, System.EventArgs e)
        {
            _doiTac = GetDoiTac();
            LoadDanhMuc();
            TaoGiaTri(DoiTacId != null);
        }

        private Library.Dep_DoiTac GetDoiTac()
        {
            return DoiTacId != null ? _db.Dep_DoiTacs.First(_ => _.Id == DoiTacId) : new Library.Dep_DoiTac();
        }

        private void LoadDanhMuc()
        {
            glkKhachHang.Properties.DataSource = _db.tnKhachHangs.Where(_=>_.MaTN == BuildingId).Select(_ => new { _.MaPhu, _.MaKH, _.KyHieu, TenKh = _.IsCaNhan == true ? _.HoKH + " " + _.TenKH : _.CtyTen});
            glkMatBang.Properties.DataSource = _db.mbMatBangs.Where(_ => _.MaTN == BuildingId).Select(_=> new { _.MaMB, _.MaSoMB, TenMb = _.MaSoMB + " - " + _.mbTangLau.TenTL + " - " + _.mbTangLau.mbKhoiNha.TenKN, _.mbTangLau.TenTL, _.mbTangLau.mbKhoiNha.TenKN});
        }

        private void TaoGiaTri(bool isId)
        {
            switch (isId)
            {
                case true:
                    if (_doiTac.KhachHangId != null) glkKhachHang.EditValue = _doiTac.KhachHangId;
                    if (_doiTac.MatBangId != null) glkMatBang.EditValue = _doiTac.MatBangId;
                    txtGhiChu.Text = _doiTac.GhiChu;
                    _tenKh = _doiTac.KhachHangName;
                    _tenMb = _doiTac.MatBangName;
                    break;
            }
        }

        private bool KiemTraDuLieu()
        {
            if (glkKhachHang.EditValue == null)
            {
                Library.DialogBox.Error("Vui lòng chọn khách hàng");
                return true;
            }

            if (glkMatBang.EditValue == null)
            {
                Library.DialogBox.Error("Vui lòng chọn mặt bằng");
                return true;
            }

            return false;
        }

        private Library.Dep_DoiTac TaoDoiTacMoi(bool isId)
        {
            switch (isId)
            {
                case false:
                    _doiTac.HopDongId = HopDongDatCocId;
                    _doiTac.HopDongNo = HopDongDatCocNo;
                    _doiTac.BuildingId = BuildingId;
                    _db.Dep_DoiTacs.InsertOnSubmit(_doiTac);
                    break;
            }

            return _doiTac;
        }

        private Library.Dep_DoiTac CapNhatDoiTac(Library.Dep_DoiTac doiTac)
        {
            doiTac.KhachHangId = (int) glkKhachHang.EditValue;
            doiTac.KhachHangName = _tenKh;
            doiTac.MatBangId = (int) glkMatBang.EditValue;
            doiTac.MatBangName = _tenMb;
            doiTac.GhiChu = txtGhiChu.Text;
            return doiTac;
        }

        private void ItemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (KiemTraDuLieu()) return;

                _doiTac = TaoDoiTacMoi(DoiTacId!=null);
                _doiTac = CapNhatDoiTac(_doiTac);

                _db.SubmitChanges();
                Library.DialogBox.Success();
                DialogResult = System.Windows.Forms.DialogResult.OK;
                Close();
            }
            catch (System.Exception ex)
            {
                Library.DialogBox.Error("Lưu dữ liệu bị lỗi: " + ex);
            }
        }

        private void GlkKhachHang_EditValueChanged(object sender, System.EventArgs e)
        {
            try
            {
                var item = sender as DevExpress.XtraEditors.GridLookUpEdit;
                if (item.EditValue == null) return;
                _tenKh = item.Properties.View.GetFocusedRowCellValue("TenKh").ToString();
            }
            catch{}
        }

        private void GlkMatBang_EditValueChanged(object sender, System.EventArgs e)
        {
            try
            {
                var item = sender as DevExpress.XtraEditors.GridLookUpEdit;
                if (item.EditValue == null) return;
                _tenMb = item.Properties.View.GetFocusedRowCellValue("TenMb").ToString();
            }
            catch { }
        }

        private void ItemCancel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Close();
        }
    }
}