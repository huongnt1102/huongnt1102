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

namespace DichVu.ThangMay
{
    public partial class frmEdit : DevExpress.XtraEditors.XtraForm
    {
        public int? MaThe { get; set; }
        dvgxTheXe objTM;
        public tnNhanVien objnhanvien;
        public mbMatBang objmatbang;
        public byte? MaTN { get; set; }
        MasterDataContext db = new MasterDataContext();
        public frmEdit()
        {
            InitializeComponent();
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
                    lookMatBang.Properties.DataSource = db.mbMatBangs.Where(p => p.MaKH != null)
                        .Select(p => new
                        {
                            p.MaMB,
                            p.MaKH,
                            p.MaSoMB,
                            TenKH = (bool)p.tnKhachHang.IsCaNhan ? String.Format("{0} {1}", p.tnKhachHang.HoKH, p.tnKhachHang.TenKH) : p.tnKhachHang.CtyTen
                        });
                try
                {
                    lookMatBang.EditValue = objmatbang.MaMB;
                }
                catch
                {
                }
                objTM = db.dvgxTheXes.FirstOrDefault(o => o.ID == this.MaThe);
                if (this.objTM != null)
                {
                    txtSoThe.Text = objTM.SoThe;
                    dateNgungSD.EditValue = objTM.NgayDK;
                    spinPhiLamThe.EditValue = objTM.PhiLamThe;
                    ckbNgungSuDung.EditValue = objTM.NgungSuDung;
                    txtChuThe.Text = objTM.ChuThe;
                    txtDienGiai.Text = objTM.DienGiai;
                    lookMatBang.EditValue = objTM.MaMB;
                }
                else
                {
                    objTM = new dvgxTheXe();
                    objTM.IsThangMay = true;
                    objTM.NgayNhap = db.GetSystemDate();
                    objTM.MaNVN = Common.User.MaNV;
                    objTM.NgayDK = DateTime.Now;
                    db.dvgxTheXes.InsertOnSubmit(objTM);
                    txtSoThe.Text = db.CreateSoChungTu(34, this.MaTN);
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

                objTM.SoThe = txtSoThe.Text;
                objTM.PhiLamThe = spinPhiLamThe.Value;
                objTM.MaNVN = Common.User.MaNV;
                objTM.ChuThe = txtChuThe.Text;
                objTM.DienGiai = txtDienGiai.Text;
                objTM.mbMatBang = db.mbMatBangs.Single(p => p.MaMB == (int)lookMatBang.EditValue);
                objTM.MaKH = (int)lookMatBang.GetColumnValue("MaKH");
                objTM.MaTN = this.MaTN;
                objTM.NgayNgungSD = (DateTime?)dateNgungSD.EditValue;
                objTM.NgungSuDung = ckbNgungSuDung.Checked;

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

        private void ckbNgungSD_CheckedChanged(object sender, EventArgs e)
        {
            dateNgungSD.Enabled = ckbNgungSuDung.Checked;
            if (ckbNgungSuDung.Checked)
                dateNgungSD.EditValue = DateTime.Now;
            else
                dateNgungSD.EditValue = null;
        }
    }
}