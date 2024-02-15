using System;
using System.Windows.Forms;
using System.Linq;
using Library;

namespace DichVu.NhanKhau.UuDai
{
    public partial class frmEdit : DevExpress.XtraEditors.XtraForm
    {
        public tnnkDangKyUuDai objDK;
        public tnNhanVien objnhanvien;
        public mbMatBang objmatbang;
        MasterDataContext db;

        public frmEdit()
        {
            InitializeComponent();
            db = new MasterDataContext();
            TranslateLanguage.TranslateControl(this);
        }

        private void frmEdit_Load(object sender, EventArgs e)
        {
            if (objnhanvien.IsSuperAdmin.Value)
            {
                seachNhanKhau.Properties.DataSource = db.tnNhanKhaus//.Where(p => p.MaKH != null)
                    .Select(p => new
                    {
                        p.MaMB,
                        p.MaKH,
                        p.mbMatBang.MaSoMB,
                        p.HoTenNK,
                        GioiTinh = (bool)p.GioiTinh ? "Nam" : "Nữ",
                        p.NgaySinh,
                        p.DienThoai,
                        p.DCTT,
                        p.Email,
                        p.CMND,
                        p.ID
                    });
            }
            else
            {
                seachNhanKhau.Properties.DataSource = db.tnNhanKhaus.Where(p => p.mbMatBang.mbTangLau.mbKhoiNha.tnToaNha.MaTN == objnhanvien.MaTN)
                     .Select(p => new
                     {
                         p.MaMB,
                         p.MaKH,
                         p.mbMatBang.MaSoMB,
                         p.HoTenNK,
                         GioiTinh = (bool)p.GioiTinh ? "Nam" : "Nữ",
                         p.NgaySinh,
                         p.DienThoai,
                         p.DCTT,
                         p.Email,
                         p.CMND,
                        p.ID
                     });
            }


            if (this.objDK != null)
            {
                objDK = db.tnnkDangKyUuDais.Single(p => p.ID == objDK.ID);
                seachNhanKhau.EditValue = objDK.MaNK;
                rdbGioiTinh.EditValue = objDK.tnNhanKhau.GioiTinh;
                dateNgaySinh.EditValue = objDK.tnNhanKhau.NgaySinh;
                txtCMND.Text = objDK.tnNhanKhau.CMND;
                txtDCTT.Text = objDK.tnNhanKhau.DCTT;
                txtDienThoai.Text = objDK.tnNhanKhau.DienThoai;
                seachNhanKhau.Enabled = false;
                txtMatBang.Text = objDK.tnNhanKhau.mbMatBang.MaSoMB;
                objDK.MaNVCN = objnhanvien.MaNV;
                objDK.NgayCN = db.GetSystemDate();
                dateTuNgay.EditValue = objDK.TuNgay;
                dateDenNgay.EditValue = objDK.DenNgay;
                memoDienGiai.Text = objDK.DienGiai;
            }
            else
            {
                objDK = new tnnkDangKyUuDai();
                db.tnnkDangKyUuDais.InsertOnSubmit(objDK);
                dateTuNgay.DateTime = db.GetSystemDate();
                dateDenNgay.DateTime = db.GetSystemDate();
                objDK.MaNV = objnhanvien.MaNV;
                objDK.NgayTao = db.GetSystemDate();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (seachNhanKhau.EditValue == null)
            {
                DialogBox.Error("Vui lòng chọn nhân khẩu");
                seachNhanKhau.Focus();
                return;
            }


            if (dateTuNgay.Text.Trim().Length == 0)
            {
                DialogBox.Error("Vui lòng nhập ngày bắt đầu");
                dateTuNgay.Focus();
                return;
            }

            if (dateDenNgay.Text.Trim().Length == 0)
            {
                DialogBox.Error("Vui lòng nhập ngày kết thúc");
                dateDenNgay.Focus();
                return;
            }
            if (dateTuNgay.DateTime > dateDenNgay.DateTime)
            {
                DialogBox.Error("Vui lòng kiểm tra từ ngày - đến ngày");
                dateTuNgay.Focus();
                return;
            }

            objDK.MaNK = (int)seachNhanKhau.EditValue;
            objDK.TuNgay = dateTuNgay.DateTime;
            objDK.DenNgay = dateDenNgay.DateTime;
            objDK.DienGiai = memoDienGiai.Text;
            try
            {
                db.SubmitChanges();
                DialogBox.Alert("Dữ liệu đã được lưu.");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch
            {
                DialogBox.Alert("Lưu dữ liệu không thành công! Ràng buộc dữ liệu không cho phép thực hiện thao tác này hoặc đường truyền không ổn định!");
                return;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void sechNhanKhau_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                var maNK = (int)seachNhanKhau.EditValue;
                var NK = db.tnNhanKhaus.Single(p => p.ID == maNK);
                dateNgaySinh.EditValue = NK.NgaySinh;
                txtCMND.Text = NK.CMND;
                txtDCTT.Text = NK.DCTT;
                txtDienThoai.Text = NK.DienThoai;
                txtMatBang.Text = NK.MaMB != null ? NK.mbMatBang.MaSoMB : "";
                rdbGioiTinh.EditValue = NK.GioiTinh;
            }
            catch { }
        }
    }
}