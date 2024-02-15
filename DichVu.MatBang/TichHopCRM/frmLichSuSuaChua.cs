using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using Library;

namespace DichVu.MatBang.TichHopCRM
{
    public partial class frmLichSuSuaChua : XtraForm
    {
        public long? ID;
        public byte MaTN;
        public int? MaMB;

        private MasterDataContext _db;
        private mbMatBang_LichSuSuaChua _o;

        public frmLichSuSuaChua()
        {
            InitializeComponent();
        }

        private void frmLichSuSuaChua_Load(object sender, EventArgs e)
        {
            _db = new MasterDataContext();
            _o = new mbMatBang_LichSuSuaChua();

            glkThietBi.Properties.DataSource = _db.mbMatBang_ThietBis.Where(_ => _.NgungSD == false).Select(_ =>
                new {_.ID, _.MaThietBi, _.TenThietBi, ThietBi = _.MaThietBi + " - " + _.TenThietBi}).ToList();
            glkNguoiThucHien.Properties.DataSource = _db.tnNhanViens;
            glkNguoiPhoiHop.Properties.DataSource = _db.tnNhanViens;
            dtNgaySuaChua.DateTime = DateTime.Now;
            dtNgayHoanThanhDuKien.DateTime = DateTime.Now;
            dtNgayHoanThanhThucTe.DateTime = DateTime.Now;

            if (ID != null & ID != 0)
            {
                _o = _db.mbMatBang_LichSuSuaChuas.FirstOrDefault(_ => _.ID == ID);
                if (_o != null)
                {
                    glkThietBi.EditValue = _o.MaThietBi;
                    glkNguoiThucHien.EditValue = _o.NguoiThucHien;
                    glkNguoiPhoiHop.EditValue = _o.NguoiPhoiHop;
                    spinLanSuaChua.Value = _o.LanSuaChua.GetValueOrDefault();
                    if (_o.NgaySuaChua != null) dtNgaySuaChua.DateTime = (DateTime) _o.NgaySuaChua;
                    if (_o.NgayHoanThanhDuKien != null)
                        dtNgayHoanThanhDuKien.DateTime = (DateTime) _o.NgayHoanThanhDuKien;
                    if (_o.NgayHoanThanhChinhThuc != null)
                        dtNgayHoanThanhThucTe.DateTime = (DateTime) _o.NgayHoanThanhChinhThuc;
                    txtGhiChu.Text = _o.GhiChu;
                    txtLyDoSuaChua.Text = _o.LyDoSuaChua;
                    txtTinhTrangSauSuaChua.Text = _o.TinhTrangThietBiSauSuaChua;
                    txtTrinhTrangThietBi.Text = _o.TrangThaiThucHien;
                }
                else
                    _o = new mbMatBang_LichSuSuaChua();
            }
        }

        private void itemClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void itemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (glkThietBi.EditValue == null)
            {
                DialogBox.Error("Vui lòng chọn [Thiết bị].");
                return;
            }

            _o.MaTN = MaTN;
            _o.MaMB = MaMB;
            _o.MaThietBi = (int) glkThietBi.EditValue;
            _o.GhiChu = txtGhiChu.Text;
            _o.LanSuaChua = (int)spinLanSuaChua.Value;
            _o.LyDoSuaChua = txtLyDoSuaChua.Text;
            _o.NgayHoanThanhChinhThuc = dtNgayHoanThanhThucTe.DateTime;
            _o.NgayHoanThanhDuKien = dtNgayHoanThanhDuKien.DateTime;
            _o.NgaySuaChua = dtNgaySuaChua.DateTime;

            if (glkNguoiThucHien.EditValue != null)
                _o.NguoiThucHien =
                    ((tnNhanVien) glkNguoiThucHien.Properties.GetRowByKeyValue(glkNguoiThucHien.EditValue)).HoTenNV;
            if (glkNguoiPhoiHop.EditValue != null)
                _o.NguoiPhoiHop = ((tnNhanVien) glkNguoiPhoiHop.Properties.GetRowByKeyValue(glkNguoiPhoiHop.EditValue)).HoTenNV;

            _o.TinhTrangThietBiSauSuaChua = txtTinhTrangSauSuaChua.Text;
            _o.TrangThaiThucHien = txtTrinhTrangThietBi.Text;

            if (ID != null & ID != 0)
            {
                _o.NguoiSua = Common.User.MaNV;
                _o.NgaySua = DateTime.Now;
            }
            else
            {
                _o.NguoiNhap = Common.User.MaNV;
                _o.NgayNhap = DateTime.Now;
                _db.mbMatBang_LichSuSuaChuas.InsertOnSubmit(_o);
            }

            try
            {
                _db.SubmitChanges();
                DialogResult = DialogResult.OK;
                DialogBox.Alert("Đã lưu thành công");
            }
            catch
            {
                DialogResult = DialogResult.Cancel;
                DialogBox.Error("Không lưu được, vui lòng kiểm tra lại");
            }
        }
    }
}