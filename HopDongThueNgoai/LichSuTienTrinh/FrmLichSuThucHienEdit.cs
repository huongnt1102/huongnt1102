using System;
using System.Linq;

namespace HopDongThueNgoai.LichSuTienTrinh
{
    public partial class FrmLichSuThucHienEdit : DevExpress.XtraEditors.XtraForm
    {
        public int? HopDongId { get; set; }
        public int? TienTrinhId {get;set;}
        public byte? BuildingId { get; set; }

        private Library.MasterDataContext _db = new Library.MasterDataContext();
        private Library.hdctnLichSuTienTrinh _tienTrinh;
        private Library.hdctnDanhSachHopDongThueNgoai _hopDong;

        public FrmLichSuThucHienEdit()
        {
            InitializeComponent();
        }

        private void FrmLichSuThucHienEdit_Load(object sender, System.EventArgs e)
        {
            _tienTrinh = CreateTienTrinh();

            LoadDanhMuc();

            GanGiaTri(_tienTrinh.Id != 0);
        }

        private Library.hdctnDanhSachHopDongThueNgoai GetHopDongById(int? hopDongId)
        {
            return _db.hdctnDanhSachHopDongThueNgoais.FirstOrDefault(_ => _.RowID == hopDongId);
        }

        private Library.hdctnLichSuTienTrinh CreateTienTrinh()
        {
            return TienTrinhId != null ? _db.hdctnLichSuTienTrinhs.First(_=>_.Id == TienTrinhId) : new Library.hdctnLichSuTienTrinh();
        }

        private void LoadDanhMuc()
        {
            glkHopDong.Properties.DataSource = (from _ in _db.hdctnDanhSachHopDongThueNgoais
                join dt in _db.tnKhachHangs on _.NhaCungCap equals dt.MaKH.ToString() into khachHang
                from dt in khachHang.DefaultIfEmpty()
                where _.MaToaNha == BuildingId.ToString()
                select new
                {
                    _.IsPhuLuc, _.SoHopDong, HoTenKh = dt.IsCaNhan == true ? dt.HoKH + " " + dt.TenKH : dt.CtyTen,
                    _.NoiLamViecName, _.NgayHieuLuc,_.RowID
                }).ToList();

            glkKhachHang.Properties.DataSource = _db.tnKhachHangs.Where(_ => _.MaTN == BuildingId).Select(_ => new {_.MaKH, _.IsCaNhan, HoTenKh = _.IsCaNhan == true ? _.HoKH + " " + _.TenKH : _.CtyTen,_.KyHieu}).ToList();
        }

        private void GanGiaTri(bool? isEdit)
        {
            switch (isEdit)
            {
                case true:
                    glkHopDong.EditValue = _tienTrinh.HopDongId;
                    glkKhachHang.EditValue = _tienTrinh.KhachHangId;
                    if (_tienTrinh.DateCreate != null) dateNgay.DateTime = (DateTime) _tienTrinh.DateCreate;
                    txtNoiDung.Text = _tienTrinh.DienGiai;
                    break;

                case false:
                    glkHopDong.EditValue = HopDongId;
                    dateNgay.DateTime = System.DateTime.UtcNow.AddHours(7);

                    _tienTrinh.BuildingId = BuildingId;
                    _db.hdctnLichSuTienTrinhs.InsertOnSubmit(_tienTrinh);
                    break;
            }
        }

        private void ItemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (KiemTra()) return;

                _tienTrinh.HopDongId = (int?) glkHopDong.EditValue;
                _tienTrinh.KhachHangId = (int?) glkKhachHang.EditValue;
                _tienTrinh.DateCreate = dateNgay.DateTime;
                _tienTrinh.DienGiai = txtNoiDung.Text;

                _db.SubmitChanges();
                DialogResult = System.Windows.Forms.DialogResult.OK;
                Close();
            }
            catch (System.Exception ex)
            {
                Library.DialogBox.Error("Lỗi lưu dữ liệu: " + ex.Message);
            }
        }

        private bool KiemTra()
        {
            if (glkHopDong.EditValue == null)
            {
                Library.DialogBox.Alert("Vui lòng chọn hợp đồng");
                return true;
            }

            if (glkKhachHang.EditValue == null)
            {
                Library.DialogBox.Alert("Vui lòng chọn đối tác");
                return true;
            }

            return false;
        }

        private void ItemCancel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Close();
        }

        private void GlkHopDong_EditValueChanged(object sender, System.EventArgs e)
        {
            try
            {
                var item = sender as DevExpress.XtraEditors.GridLookUpEdit;
                if (item.EditValue == null) return;

                _hopDong = GetHopDongById((int?) item.EditValue);
                if (_hopDong != null) glkKhachHang.EditValue = int.Parse(_hopDong.NhaCungCap);
            }
            catch{}
        }
    }
}