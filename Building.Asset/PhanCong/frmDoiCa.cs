using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Library;
using System.Linq;

namespace Building.Asset.PhanCong
{
    public partial class frmDoiCa : XtraForm
    {
        public DateTime Ngay { get; set; }
        public byte? MaTn { get; set; }
        public int? Id { get; set; }
        public int? IsSua { get; set; }

        private MasterDataContext _db;
        private tbl_PhanCong_DoiCa _o;

        public frmDoiCa()
        {
            InitializeComponent();
            TranslateLanguage.TranslateControl(this);
        }

        private void frmDoiCa_Load(object sender, EventArgs e)
        {
            dateNgay.DateTime = Ngay;
            LoadData();
        }

        private void LoadData()
        {
            _db = new MasterDataContext();

            glkNhanVien1.Properties.DataSource = (from p in _db.tbl_PhanCong_NhanVienChiTiets
                                                  where p.Ngay.Value.Date == dateNgay.DateTime.Date & p.MaTN == MaTn
                                                  group new { p } by new { p.MaNV, p.tnNhanVien.HoTenNV, p.tnNhanVien.MaSoNV } into g
                                                  select new
                                                  {
                                                      g.Key.MaNV,
                                                      g.Key.HoTenNV,
                                                      g.Key.MaSoNV
                                                  }).ToList();

            glkNhanVien2.Properties.DataSource = (from p in _db.tbl_PhanCong_NhanVienChiTiets
                                                  where p.Ngay.Value.Date == dateNgay.DateTime.Date & p.MaTN == MaTn
                                                  group new { p } by new { p.MaNV, p.tnNhanVien.HoTenNV, p.tnNhanVien.MaSoNV } into g
                                                  select new
                                                  {
                                                      g.Key.MaNV,
                                                      g.Key.HoTenNV,
                                                      g.Key.MaSoNV
                                                  }).ToList();
            if (IsSua == 0)
            {
                _o = new tbl_PhanCong_DoiCa();
            }
            else
            {
                _o = _db.tbl_PhanCong_DoiCas.FirstOrDefault(_ => _.ID == Id);
                if (_o != null)
                {
                    if (_o.Ngay != null) dateNgay.DateTime = (DateTime)_o.Ngay;
                    glkNhanVien1.EditValue = _o.MaNV1;
                    glkNhanVien2.EditValue = _o.MaNV2;
                    lkCaTruc1.EditValue = _o.IdPhanLoaiCaCu1;
                    lkCaTruc2.EditValue = _o.IdPhanLoaiCaCu2;
                }
            }
        }

        private void dateNgay_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void glkNhanVien1_EditValueChanged(object sender, EventArgs e)
        {
            var item = sender as GridLookUpEdit;
            if (item == null) return;

            var l = (from p in _db.tbl_PhanCong_NhanVienChiTiets
                     join lc in _db.tbl_PhanCong_PhanLoaiCas on p.IDPhanLoaiCa equals lc.ID
                     where p.MaNV == (int)item.EditValue & p.Ngay.Value.Date == dateNgay.DateTime.Date & p.MaTN == MaTn
                     group new { p } by new { p.tbl_PhanCong_PhanLoaiCa.ID, p.tbl_PhanCong_PhanLoaiCa.KyHieu, p.tbl_PhanCong_PhanLoaiCa.Ten } into g
                     select new
                     {
                         g.Key.ID,
                         g.Key.KyHieu,
                         g.Key.Ten
                     }).ToList();
            if (l.Count > 0)
            {
                lkCaTruc1.Properties.DataSource = l;
            }
        }

        private void glkNhanVien2_EditValueChanged(object sender, EventArgs e)
        {
            var item = sender as GridLookUpEdit;
            if (item == null) return;

            var l = (from p in _db.tbl_PhanCong_NhanVienChiTiets
                     join lc in _db.tbl_PhanCong_PhanLoaiCas on p.IDPhanLoaiCa equals lc.ID
                     where p.MaNV == (int)item.EditValue & p.Ngay.Value.Date == dateNgay.DateTime.Date & p.MaTN == MaTn
                     group new { p } by new { p.tbl_PhanCong_PhanLoaiCa.ID, p.tbl_PhanCong_PhanLoaiCa.KyHieu, p.tbl_PhanCong_PhanLoaiCa.Ten } into g
                     select new
                     {
                         g.Key.ID,
                         g.Key.KyHieu,
                         g.Key.Ten
                     }).ToList();
            if (l.Count > 0)
            {
                lkCaTruc2.Properties.DataSource = l;
            }
        }

        private void itemDoiCa_Click(object sender, EventArgs e)
        {
            #region Kiểm tra
            if (glkNhanVien1.EditValue == null)
            {
                DialogBox.Error("Vui lòng chọn nhân viên kỹ thuật cần chuyển");
                return;
            }

            if (glkNhanVien2.EditValue == null)
            {
                DialogBox.Error("Vui lòng chọn nhân viên kỹ thuật muốn chuyển đến");
                return;
            }

            if (lkCaTruc1.EditValue == null)
            {
                DialogBox.Error("Vui lòng chọn loại ca nhân viên 1");
                return;
            }

            if (lkCaTruc2.EditValue == null)
            {
                DialogBox.Error("Vui lòng chọn loại ca nhân viên 2");
                return;
            }
            #endregion

            bool value = false;

            _db = new MasterDataContext();
            var o = _db.tbl_PhanCong_NhanVienChiTiets.FirstOrDefault(_ =>
                _.MaNV == (int)glkNhanVien1.EditValue & _.Ngay.Value.Date == dateNgay.DateTime.Date &
                _.IDPhanLoaiCa == (int)lkCaTruc1.EditValue);

            var o2 = _db.tbl_PhanCong_NhanVienChiTiets.FirstOrDefault(_ =>
                _.MaNV == (int)glkNhanVien2.EditValue & _.Ngay.Value.Date == dateNgay.DateTime.Date &
                _.IDPhanLoaiCa == (int)lkCaTruc2.EditValue);
            if (o != null & o2 != null)
            {
                _o.IdDoiCa1 = o.ID;
                _o.IdDoiCa2 = o2.ID;
                _o.IdPhanLoaiCaMoi1 = (int)lkCaTruc2.EditValue;
                _o.IdPhanLoaiCaMoi2 = (int)lkCaTruc1.EditValue;
                _o.MaNV1 = (int)glkNhanVien1.EditValue;
                _o.MaNV2 = (int)glkNhanVien2.EditValue;
                _o.Ngay = dateNgay.DateTime;
                _o.IdPhanLoaiCaCu1 = o.IDPhanLoaiCa;
                _o.IdPhanLoaiCaCu2 = o2.IDPhanLoaiCa;
                _o.MaLoai = 20;//Đổi giữa 2 nhân viên
                _o.MaTN = MaTn;
                if (IsSua == 0)
                {
                    var oo = _db.tbl_PhanCong_DoiCas.FirstOrDefault(_ =>
                        _.Ngay.Value.Date == dateNgay.DateTime.Date
                        & _.MaNV1 == (int)glkNhanVien1.EditValue &
                        _.MaNV2 == (int)glkNhanVien2.EditValue & _.IdPhanLoaiCaMoi1 == (int)lkCaTruc2.EditValue & _.IdPhanLoaiCaMoi2 == (int)lkCaTruc1.EditValue);
                    if (oo == null)
                    {
                        _o.NgayLap = DateTime.Now;
                        _o.NguoiLap = Common.User.MaNV;
                        _o.IdTrangThai = 10;
                        _db.tbl_PhanCong_DoiCas.InsertOnSubmit(_o);

                        value = true;
                    }
                    else
                    {
                        DialogBox.Error("Nhân viên này đã có phiếu đổi ca");
                        return;
                    }
                }
                else
                {
                    _o.NgaySua = DateTime.Now;
                    _o.NguoiSua = Common.User.MaNV;
                }
            }
            _db.SubmitChanges();

            if (value)
            {
                DialogResult = DialogResult.OK;
                DialogBox.Alert("Đã lưu thành công");
            }
        }

        private void itemHuy_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}