using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using Library;

namespace DichVu.TheTichHop
{
    public partial class frmEdit : DevExpress.XtraEditors.XtraForm
    {         
        public dvTheTichHop objTM;
        public tnNhanVien objnhanvien;
        public mbMatBang objmatbang;
        MasterDataContext db;
        public byte? MaTN { get; set; }

        public frmEdit()
        {
            InitializeComponent();
            db = new MasterDataContext();
            TranslateLanguage.TranslateControl(this);
        }

        string getNewMaNK()
        {
            string MaNK = "";
            db.tmThangMay_getNewMaTM(ref MaNK);
            return db.DinhDang(5, int.Parse(MaNK));
        }

        private void frmEdit_Load(object sender, EventArgs e)
        {
            if (objnhanvien.IsSuperAdmin.Value)
            {
                lookNhanVien.Properties.DataSource = db.tnNhanViens
                    .Select(p => new { p.MaNV, p.MaSoNV, p.HoTenNV });
                lookMatBang.Properties.DataSource = db.mbMatBangs.Where(p => p.MaKH != null)
                    .Select(p => new
                    {
                        p.MaMB,
                        p.MaKH,
                        p.MaSoMB,
                        TenKH = (bool)p.tnKhachHang.IsCaNhan ? String.Format("{0} {1}", p.tnKhachHang.HoKH, p.tnKhachHang.TenKH) : p.tnKhachHang.CtyTen
                    });
            }
            else
            {
                lookNhanVien.Properties.DataSource = db.tnNhanViens.Where(p => p.MaTN == objnhanvien.MaTN)
                    .Select(p => new { p.MaNV, p.MaSoNV, p.HoTenNV });
                lookMatBang.Properties.DataSource = db.mbMatBangs.Where(p => p.MaKH != null & p.mbTangLau.mbKhoiNha.tnToaNha.MaTN == objnhanvien.MaTN)
                    .Select(p => new
                    {
                        p.MaMB,
                        p.MaKH,
                        p.MaSoMB,
                        TenKH = (bool)p.tnKhachHang.IsCaNhan ? String.Format("{0} {1}", p.tnKhachHang.HoKH, p.tnKhachHang.TenKH) : p.tnKhachHang.CtyTen
                    });
            }
            lookTrangThai.Properties.DataSource = db.dvtmTrangThais;
            try
            {
                lookMatBang.EditValue = objmatbang.MaMB;
            }
            catch
            {
            }

            if (this.objTM != null)
            {
                txtSoThe.Text = objTM.SoThe;
                dateNgayDK.EditValue = objTM.NgayDK;
                spinPhiLamThe.EditValue = objTM.PhiLamThe;
                ckbDaTT.EditValue = objTM.DaTT;
                lookNhanVien.EditValue = objTM.MaNV;
                txtChuThe.Text = objTM.ChuThe;
                txtDienGiai.Text = objTM.DienGiai;
                lookMatBang.EditValue = objTM.MaMB;
                lookMatBang.Enabled = false;
                lookTrangThai.EditValue = objTM.MaTrangThai;
                objTM = db.dvTheTichHops.Single(p => p.ID == objTM.ID);
            }
            else
            {
                objTM = new dvTheTichHop();
                db.dvTheTichHops.InsertOnSubmit(objTM);
                dateNgayDK.DateTime = DateTime.Now;
                lookNhanVien.EditValue = objnhanvien.MaNV;
                txtSoThe.Text = db.CreateSoChungTu(35, this.MaTN);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtSoThe.Text.Trim().Length == 0)
            {
                DialogBox.Error("Vui lòng nhập số thẻ");
                txtSoThe.Focus();
                return;
            }
            if (txtChuThe.Text.Trim().Length == 0)
            {
                DialogBox.Error("Vui lòng nhập chủ thẻ");
                txtChuThe.Focus();
                return;
            }
            if (lookMatBang.Text.Trim().Length == 0)
            {
                DialogBox.Error("Vui lòng chọn mặt bằng");
                lookMatBang.Focus();
                return;
            }
            if (lookTrangThai.Text.Trim().Length == 0)
            {
                DialogBox.Error("Vui lòng chọn trạng thái");
                lookTrangThai.Focus();
                return;
            }

            objTM.SoThe = txtSoThe.Text;
            objTM.NgayDK = dateNgayDK.DateTime;
            objTM.PhiLamThe = spinPhiLamThe.Value;
            objTM.DaTT = ckbDaTT.Checked;
            objTM.tnNhanVien = db.tnNhanViens.Single(p => p.MaNV == (int)lookNhanVien.EditValue);
            objTM.ChuThe = txtChuThe.Text;
            objTM.DienGiai = txtDienGiai.Text;
            objTM.mbMatBang = db.mbMatBangs.Single(p => p.MaMB == (int)lookMatBang.EditValue);
            objTM.tnKhachHang = db.tnKhachHangs.Single(p => p.MaKH == (int)lookMatBang.GetColumnValue("MaKH"));
            objTM.dvtmTrangThai = db.dvtmTrangThais.Single(p => p.MaTrangThai == (int)lookTrangThai.EditValue);

            luu:
            try
            {
                db.SubmitChanges();

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch
            {
                txtSoThe.Text = getNewMaNK();
                goto luu;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}