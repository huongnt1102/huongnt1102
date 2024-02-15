using System;
using System.Windows.Forms;
using System.Linq;
using Library;
using System.Drawing;

namespace DichVu.NhanKhau
{
    public partial class frmNhanKhaiThangLong : DevExpress.XtraEditors.XtraForm
    {
        public int? ID { get; set; }
        public int? MaKH { get; set; }
        public int? MaMB { get; set; }
        public byte? MaTN { get; set; }
        MasterDataContext db = new MasterDataContext();
        tnNhanKhau objNK;

        public frmNhanKhaiThangLong()
        {
            InitializeComponent();
        }

        private void frmEdit_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this);
            txtQuocTich.Properties.DataSource = db.QuocTiches;
            lookNhanVien.Properties.DataSource = db.tnNhanViens.Where(p => p.MaTN == Common.User.MaTN)
                   .Select(p => new { p.MaNV, p.MaSoNV, p.HoTenNV });
            var ltMatBang = (from mb in db.mbMatBangs
                             join tl in db.mbTangLaus on mb.MaTL equals tl.MaTL
                             join kn in db.mbKhoiNhas on tl.MaKN equals kn.MaKN
                             join kh in db.tnKhachHangs on mb.MaKH equals kh.MaKH
                             where (mb.MaKH == this.MaKH | this.MaKH == null) & mb.MaTN == MaTN
                             select new
                             {
                                 mb.MaMB,
                                 mb.MaSoMB,
                                 tl.TenTL,
                                 kn.TenKN,
                                 kh.MaKH,
                                 kh.KyHieu,
                                 TenKH = kh.IsCaNhan.GetValueOrDefault() ? kh.HoKH.ToString() + " " + kh.TenKH.ToString() : kh.CtyTen
                             }).ToList();
            lookMatBang.Properties.DataSource = ltMatBang;
            lookTrangThai.Properties.DataSource = db.tnNhanKhauTrangThais;
            lookUpQuanHe.Properties.DataSource = db.tnQuanHes;

            if (this.ID != null)
            {
                objNK = db.tnNhanKhaus.Single(p => p.ID == this.ID);
                txtHoTenNK.Text = objNK.HoTenNK;
                rdbGioiTinh.EditValue = objNK.GioiTinh;
                txtNgaySinh.EditValue = objNK.NgaySinh;
                txtCMND.Text = objNK.CMND;
                txtNgayCap.EditValue = objNK.NgayCap;
                txtNoiCap.Text = objNK.NoiCap;
                txtDCTT.Text = objNK.DCTT;
                txtDienThoai.Text = objNK.DienThoai;
                txtEmail.Text = objNK.Email;
                txtDienGiai.Text = objNK.DienGiai;
                lookMatBang.EditValue = objNK.MaMB;
                lookNhanVien.EditValue = objNK.MaNV;
                dateNgayDK.EditValue = objNK.NgayDK;
                dateNgayDen.EditValue = objNK.NgayChuyenDen;
                dateNgayDi.EditValue = objNK.NgayChuyenDi;
                txtNgheNgiep.Text = objNK.NgheNghiep;
                txtNoiCap.Text = objNK.NoiCap;
                txtNoiLamViec.Text = objNK.NoiLamViec;
                lookMatBang.Enabled = false;
                lookMatBang.EditValue = objNK.MaMB;
                ckbDaDKTT.EditValue = objNK.DaDKTT;
                lookTrangThai.EditValue = objNK.MaTT;
                txtQuocTich.EditValue = objNK.MaQT;
                dateHHDKTT.Text = objNK.NgayHetHanDKTT ?? "";
                ckDinhMuc.Checked = objNK.DangKyDinhMuc ?? false;
                lookChuHo.EditValue = objNK.ParentID;
                lookUpQuanHe.EditValue = objNK.QuanHeID;
                txtDienThoai2.Text = objNK.DienThoai2;
                txtEmail2.Text = objNK.Email2;
                txtDanToc.Text = objNK.DanToc;
                txtTonGiao.Text = objNK.TonGiao;
                txtThiThuVISA.Text = objNK.SoThiThucVISA;
                txtHanThiTHuc.EditValue = objNK.HanThiThucVISAThangLong;
            }
            else
            {
                objNK = new tnNhanKhau();
                db.tnNhanKhaus.InsertOnSubmit(objNK);
                dateNgayDK.Text = "";
                lookNhanVien.EditValue = Common.User.MaNV;                
                lookUpQuanHe.ItemIndex = 0;
                lookTrangThai.ItemIndex = 0;

                if (this.MaKH != null & ltMatBang.Count > 0)
                    lookMatBang.EditValue = ltMatBang.FirstOrDefault().MaMB;

                if (this.MaMB != null)
                    lookMatBang.EditValue = this.MaMB;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtHoTenNK.Text.Trim().Length == 0)
            {
                DialogBox.Error("Vui lòng nhập họ và tên");
                txtHoTenNK.Focus();
                return;
            }

            if (lookMatBang.Text.Trim().Length == 0)
            {
                DialogBox.Error("Vui lòng chọn mặt bằng");
                lookMatBang.Focus();
                return;
            }

            if (dateNgayDK.Text.Trim().Length == 0)
            {
                DialogBox.Error("Vui lòng nhập ngày đăng ký");
                dateNgayDK.Focus();
                return;
            }

            if (lookTrangThai.Text.Trim().Length == 0)
            {
                DialogBox.Error("Vui lòng trạng thái");
                lookMatBang.Focus();
                return;
            }
            objNK.HoTenNK = txtHoTenNK.Text.Trim();
            objNK.GioiTinh = (bool)rdbGioiTinh.EditValue;
            objNK.NgaySinh = txtNgaySinh.Text;
            objNK.CMND = txtCMND.Text.Trim();
            objNK.NgayCap = txtNgayCap.Text;
            objNK.NoiCap = txtNoiCap.Text.Trim();
            objNK.DCTT = txtDCTT.Text.Trim();
            objNK.DienThoai = txtDienThoai.Text.Trim();
            objNK.Email = txtEmail.Text.Trim();
            objNK.DienGiai = txtDienGiai.Text.Trim();
            objNK.DaDKTT = ckbDaDKTT.Checked;            
            objNK.NgayDK = dateNgayDK.Text;
            objNK.NgayChuyenDi = dateNgayDi.Text;
            objNK.NgayChuyenDen = dateNgayDen.Text;
            objNK.HanThiThucVISAThangLong = txtHanThiTHuc.Text;
            objNK.SoThiThucVISA = txtThiThuVISA.Text.Trim();
            objNK.TonGiao = txtTonGiao.Text.Trim();
            objNK.DanToc = txtDanToc.Text.Trim();
            objNK.DienThoai2 = txtDienThoai2.Text.Trim();
            objNK.Email2 = txtEmail2.Text.Trim();
            objNK.MaNV = (int?)lookNhanVien.EditValue;
            objNK.MaMB = (int?)lookMatBang.EditValue;
            objNK.MaKH = this.MaKH;
            objNK.MaTT = (int?)lookTrangThai.EditValue;
            objNK.MaQT = (int?)txtQuocTich.EditValue;
           
                objNK.NgayHetHanDKTT = "";
            objNK.DangKyDinhMuc = ckDinhMuc.Checked;
            objNK.QuanHeID = (int?)lookUpQuanHe.EditValue;
            objNK.ParentID = (int?)lookChuHo.EditValue;
            try
            {
                db.SubmitChanges();
                DialogBox.Alert("Dữ liệu đã được lưu.");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch
            {
                DialogBox.Error("Lưu dữ liệu không thành công! Ràng buộc dữ liệu không cho phép thực hiện thao tác này hoặc đường truyền không ổn định!");
                return;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void lookChuHo_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                lookChuHo.EditValue = null;
                lookChuHo.ClosePopup();
            }
        }

        private void lookMatBang_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                var r = lookMatBang.Properties.GetRowByKeyValue(lookMatBang.EditValue);
                if (r != null)
                {
                    var type = r.GetType();
                    var _MaMB = (int)lookMatBang.EditValue;

                    this.MaKH = (int?)type.GetProperty("MaKH").GetValue(r, null);

                    lookChuHo.Properties.DataSource = db.tnNhanKhaus.Where(p => p.MaMB == _MaMB).Select(p => new { p.ID, p.HoTenNK });
                }
            }
            catch { }
        }

        private void lookUpQuanHe_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            switch (e.Button.Index)
            {
                case 1:
                    var frm = new frmQuanHe();
                    frm.ShowDialog();

                    lookUpQuanHe.Properties.DataSource = db.tnQuanHes;
                    break;
                case 2:
                    lookUpQuanHe.EditValue = null;
                    break;
            }
        }

        private void lookMatBang_SizeChanged(object sender, EventArgs e)
        {
            lookMatBang.Properties.PopupFormSize = new Size(lookMatBang.Size.Width, 0);
        }

        private void txtQuocTich_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

        }
    }
}