using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Library;
using System.Linq;

namespace Provider
{
    public partial class frmEdit : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public tnNhanVien objnhanvien;
        NhaCungCap objNCC;
        public int MaNCC = 0;
        public frmEdit()
        {
            InitializeComponent();
            db = new MasterDataContext();

            TranslateLanguage.TranslateControl(this);
        }

        private void frmEdit_Load(object sender, EventArgs e)
        {
            if (MaNCC != 0)
            {
                objNCC = db.NhaCungCaps.Single(p => p.MaNCC == MaNCC);
                txtDiaChi.Text = objNCC.DiaChi;
                txtDiDongNLH.Text = objNCC.DiDongNLH;
                txtDienThoai.Text = objNCC.DienThoai;
                txtDienThoaiNLH.Text = objNCC.DienThoaiNLH;
                txtEmail.Text = objNCC.Email;
                txtEmailNLH.Text = objNCC.EmailNLH;
                txtFax.Text = objNCC.Fax;
                txtGhiChu.Text = objNCC.GhiChu;
                txtNguoiLienHe.Text = objNCC.NguoiLienHe;
                txtTenNCC.Text = objNCC.TenNCC;

                this.Text = "Cập nhật thông tin Nhà cung cấp";
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtTenNCC.Text.Trim() == "")
            {
                DialogBox.Alert("Vui lòng nhập [Tên], xin cảm ơn.");
                txtTenNCC.Focus();
                return;
            }

            if (MaNCC == 0)
            {
                objNCC = new NhaCungCap();
                objNCC.MaNV = objnhanvien.MaNV;
                objNCC.NgayTao = db.GetSystemDate();
                db.NhaCungCaps.InsertOnSubmit(objNCC);
            }
            else
            {
                objNCC.MaNVCN = objnhanvien.MaNV;
                objNCC.NgayCN = db.GetSystemDate();
            }

            objNCC.DiaChi = txtDiaChi.Text.Trim();
            objNCC.DiDongNLH = txtDiDongNLH.Text.Trim();
            objNCC.DienThoai = txtDienThoai.Text.Trim();
            objNCC.DienThoaiNLH = txtDienThoaiNLH.Text.Trim();
            objNCC.Email = txtEmail.Text.Trim();
            objNCC.EmailNLH = txtEmailNLH.Text.Trim();
            objNCC.Fax = txtFax.Text.Trim();
            objNCC.GhiChu = txtGhiChu.Text.Trim();
            objNCC.NguoiLienHe = txtNguoiLienHe.Text.Trim();
            objNCC.TenNCC = txtTenNCC.Text.Trim();

            try
            {
                db.SubmitChanges();

                DialogResult = System.Windows.Forms.DialogResult.OK;
                DialogBox.Alert("Dữ liệu đã được cập nhật.");
                this.Close();
            }
            catch { DialogBox.Alert("Đã có lỗi xảy ra, vui lòng kiểm tra lại."); }
        }
    }
}