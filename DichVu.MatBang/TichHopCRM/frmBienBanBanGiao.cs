using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Library;
using System.Linq;
using ftp = FTP;

namespace DichVu.MatBang.TichHopCRM
{
    public partial class frmBienBanBanGiao : XtraForm
    {
        public long? ID;
        public int? MaMB;
        public byte? MaTn;

        private MasterDataContext _db;
        private mbMatBang_BienBan_BanGiao _o;

        public frmBienBanBanGiao()
        {
            InitializeComponent();
        }

        private void frmBienBanBanGiao_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this);

            _db = new MasterDataContext();
            _o = new mbMatBang_BienBan_BanGiao();

            dtNgayThucHien.DateTime = DateTime.Now;

            glkMatBang.Properties.DataSource = _db.mbMatBangs.Where(_ => _.MaTN == (byte) MaTn).Select(_ =>
                new
                {
                    _.MaSoMB, _.mbTangLau.TenTL, _.mbTangLau.mbKhoiNha.TenKN,
                    MatBang = _.MaSoMB + " - Lầu: " + _.mbTangLau.TenTL + " - Khối nhà: " + _.mbTangLau.mbKhoiNha.TenKN,
                    _.MaMB
                }).ToList();
            glkMatBang.EditValue = MaMB;

            glkBoPhanBanGiao.Properties.DataSource = glkBoPhanTiepNhan.Properties.DataSource = _db.tnPhongBans;
            glkBoPhanBanGiao.EditValue = glkBoPhanTiepNhan.EditValue = Common.User.MaPB;

            glkNhanVienBanGiao.Properties.DataSource = _db.tnNhanViens.Select(_ => new {_.MaSoNV, _.MaNV, _.HoTenNV});
            glkNhanVienTiepNhan.Properties.DataSource = _db.tnNhanViens.Select(_ => new {_.MaSoNV, _.MaNV, _.HoTenNV});
            glkNhanVienBanGiao.EditValue = glkNhanVienTiepNhan.EditValue = Common.User.MaNV;

            if (ID != null & ID != 0)
            {
                _o = _db.mbMatBang_BienBan_BanGiaos.FirstOrDefault(_ => _.ID == ID);
                if (_o != null)
                {
                    txtSoBienBan.Text = _o.SoBienBan;
                    txtTenBienBan.Text = _o.TenBienBan;
                    if (_o.NgayThucHien != null) dtNgayThucHien.DateTime = (DateTime) _o.NgayThucHien;
                    glkBoPhanBanGiao.EditValue = _o.BoPhanBanGiao;
                    glkNhanVienBanGiao.EditValue = _o.NguoiBanGiao;
                    glkBoPhanTiepNhan.EditValue = _o.BoPhanTiepNhan;
                    glkNhanVienTiepNhan.EditValue = _o.NguoiTiepNhan;
                    txtGhiChu.Text = _o.GhiChu;
                    btnFileDinhKem.Text = _o.FileDinhKem;
                    btnAnhDinhKem.Text = _o.HinhAnhDinhKem;
                }
                else
                    _o = new mbMatBang_BienBan_BanGiao();
            }
        }

        private void itemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _o.SoBienBan = txtSoBienBan.Text;
            _o.TenBienBan = txtTenBienBan.Text;
            _o.GhiChu = txtGhiChu.Text;
            _o.NgayThucHien = dtNgayThucHien.DateTime;
            _o.MaMB = (int)glkMatBang.EditValue;
            _o.MaTN = MaTn;
            _o.NguoiBanGiao = (int) glkNhanVienBanGiao.EditValue;
            _o.BoPhanBanGiao = (int) glkBoPhanBanGiao.EditValue;
            _o.NguoiTiepNhan = (int) glkNhanVienTiepNhan.EditValue;
            _o.BoPhanTiepNhan = (int) glkBoPhanTiepNhan.EditValue;
            _o.FileDinhKem = btnFileDinhKem.Text;
            _o.HinhAnhDinhKem = btnAnhDinhKem.Text;

            if (ID != null & ID != 0)
            {
                _o.NguoiSua = Common.User.MaNV;
                _o.NgaySua = DateTime.Now;
            }
            else
            {
                _o.NguoiNhap = Common.User.MaNV;
                _o.NgayNhap = DateTime.Now;
                _db.mbMatBang_BienBan_BanGiaos.InsertOnSubmit(_o);
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

        private void btnFileDinhKem_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var frm = new ftp.frmUploadFile();
            if (frm.SelectFile(false))
            {
                btnFileDinhKem.Tag = frm.ClientPath;
                if (btnFileDinhKem.Text.Trim() == "")
                    btnFileDinhKem.Text = frm.FileName;
            }
            frm.Dispose();
        }

        private void btnAnhDinhKem_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var frm = new ftp.frmUploadFile();
            if (frm.SelectFile(true))
            {
                btnAnhDinhKem.Tag = frm.ClientPath;
                if (btnAnhDinhKem.Text.Trim() == "")
                    btnAnhDinhKem.Text = frm.FileName;
            }
            frm.Dispose();
        }

        private void itemClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}