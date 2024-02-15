using System.Linq;

namespace DichVu.KhachHang.CoHoi
{
    public partial class FrmLichSu : DevExpress.XtraEditors.XtraForm
    {
        public int? KhachHangId { get; set; }
        public int? NhatKyId { get; set; }
        public byte? BuildingId { get; set; }

        private Library.MasterDataContext _db = new Library.MasterDataContext();
        private Library.ch_KhachHang_NhatKy _nhatKy;

        public FrmLichSu()
        {
            InitializeComponent();
        }

        private void FrmLichSu_Load(object sender, System.EventArgs e)
        {
            GetKhachHang();
            if (KhachHangId != null) glkKhachHang.EditValue = KhachHangId;

            _nhatKy = GetNhatKy();

            UpdateDuLieu(_nhatKy.Id != 0);
        }

        private void GetKhachHang()
        {
            var db = new Library.MasterDataContext();
            glkKhachHang.Properties.DataSource = db.tnKhachHangs.Where(_ => _.MaTN == BuildingId).Select(_=>new{_.MaKH, _.KyHieu, _.MaPhu,HoTenKh = _.IsCaNhan == true?(_.HoKH+" "+_.TenKH):_.CtyTen});
            db.Dispose();
        }

        private Library.ch_KhachHang_NhatKy GetNhatKy()
        {
            return NhatKyId!=null? _db.ch_KhachHang_NhatKies.First(_=>_.Id == NhatKyId):new Library.ch_KhachHang_NhatKy();
        }

        private void UpdateDuLieu(bool isEdit)
        {
            switch (isEdit)
            {
                case true: txtNoiDung.EditValue = _nhatKy.NoiDung; break;
                case false: _db.ch_KhachHang_NhatKies.InsertOnSubmit(_nhatKy); break;
            }
        }

        private bool KiemTra()
        {
            if (glkKhachHang.EditValue == null)
            {
                Library.DialogBox.Error("Vui lòng chọn khách hàng.");
                return true;
            }

            return false;
        }

        private void ItemLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (KiemTra()) return;

                _nhatKy.KhachHangId = (int?) glkKhachHang.EditValue;
                _nhatKy.NoiDung = txtNoiDung.Text;
                _nhatKy.BuildingId = BuildingId;
                _nhatKy.UserCreateId = Library.Common.User.MaNV;
                _nhatKy.UserCreateName = Library.Common.User.HoTenNV;
                _nhatKy.DateCreate = System.DateTime.UtcNow.AddHours(7);

                _db.SubmitChanges();
                Library.DialogBox.Success();
                DialogResult = System.Windows.Forms.DialogResult.OK;
                KhachHangId = (int?) glkKhachHang.EditValue;
                Close();
            }
            catch (System.Exception ex)
            {
                Library.DialogBox.Error("Lỗi lưu dữ liệu: " + ex);
            }
        }

        private void ItemThemKhachHang_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DichVu.KhachHang.CoHoi.FrmEdit frm = new DichVu.KhachHang.CoHoi.FrmEdit() { maTN = BuildingId, objnv = Library.Common.User, IsNCC = false };
            frm.ShowDialog();
            if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                GetKhachHang();
            }
        }

        private void ItemHuy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.No;
            Close();
        }
    }
}