using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using Library;
using ftp = FTP;

namespace DichVu.MatBang.TichHopCRM
{
    public partial class frmThietBiKemTheo : XtraForm
    {
        public long? ID;
        public int? MaMB;
        public byte? MaTN;

        private MasterDataContext _db;
        private mbMatBang_ThietBiKemTheo _o;

        public frmThietBiKemTheo()
        {
            InitializeComponent();
        }

        private void frmThietBiKemTheo_Load(object sender, EventArgs e)
        {
            LoadThietBi();

            _o = new mbMatBang_ThietBiKemTheo();
            dtNgayBanGiao.DateTime = DateTime.Now;
            dtNgayHetHanBaoHanh.DateTime = DateTime.Now;

            if (ID != null & ID != 0)
            {
                _o = _db.mbMatBang_ThietBiKemTheos.FirstOrDefault(_ => _.ID == ID);
                if (_o != null)
                {
                    glkThietBi.EditValue = _o.MaThietBi;
                    txtSoBaoHanh.Text = _o.GiayBaoHanh;
                    txtGhiChu.Text = _o.GhiChu;
                    btnHinhAnh.Text = _o.HinhAnh;
                    btnFileDinhKem.Text = _o.FileDinhKem;
                    if (_o.NgayBanGiao != null) dtNgayBanGiao.DateTime = (DateTime) _o.NgayBanGiao;
                    if (_o.NgayHetHanBaoHanh != null) dtNgayHetHanBaoHanh.DateTime = (DateTime) _o.NgayHetHanBaoHanh;
                    spinThoiGianBaoHanh.Value = _o.ThoiGianBaoHanh.GetValueOrDefault();
                }
                else
                    _o = new mbMatBang_ThietBiKemTheo();
            }
        }

        private void LoadThietBi()
        {
            _db = new MasterDataContext();
            glkThietBi.Properties.DataSource = _db.mbMatBang_ThietBis.Where(_ => _.NgungSD == false).Select(_ =>
                new { _.ID, _.MaThietBi, _.TenThietBi, ThietBi = _.MaThietBi + " - " + _.TenThietBi }).ToList();
        }

        private void itemClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnHinhAnh_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var frm = new ftp.frmUploadFile();
            if (frm.SelectFile(true))
            {
                btnHinhAnh.Tag = frm.ClientPath;
                if (btnHinhAnh.Text.Trim() == "")
                    btnHinhAnh.Text = frm.FileName;
            }
            frm.Dispose();
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

        private void itemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (glkThietBi.EditValue == null)
            {
                DialogBox.Error("Vui lòng chọn [Thiết Bị].");
                return;
            }

            _o.MaTN = MaTN;
            _o.MaMB = MaMB;
            _o.MaThietBi = (int) glkThietBi.EditValue;
            _o.NgayBanGiao = dtNgayBanGiao.DateTime;
            _o.NgayHetHanBaoHanh = dtNgayHetHanBaoHanh.DateTime;
            _o.ThoiGianBaoHanh = (int?) spinThoiGianBaoHanh.Value;
            _o.GiayBaoHanh = txtSoBaoHanh.Text;
            _o.HinhAnh = btnHinhAnh.Text;
            _o.FileDinhKem = btnFileDinhKem.Text;
            _o.GhiChu = txtGhiChu.Text;

            if (ID != null & ID != 0)
            {
                _o.NguoiSua = Common.User.MaNV;
                _o.NgaySua = DateTime.Now;
            }
            else
            {
                _o.NguoiNhap = Common.User.MaNV;
                _o.NgayNhap = DateTime.Now;
                _db.mbMatBang_ThietBiKemTheos.InsertOnSubmit(_o);
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

        private void itemThemThietBi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var frm = new frmThietBiEdit())
            {
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                    LoadThietBi();
            }
        }
    }
}