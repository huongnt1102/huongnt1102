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

namespace LandsoftBuildingGeneral.DynBieuMau
{
    public partial class frmEdit : DevExpress.XtraEditors.XtraForm
    {
        public int MaBM;
        public byte MaTN;
        public tnNhanVien objnhanvien;
        MasterDataContext db = new MasterDataContext();
        BmBieuMau objBM;

        public frmEdit()
        {
            InitializeComponent();
            TranslateLanguage.TranslateControl(this);
        }

        private void frmEdit_Load(object sender, EventArgs e)
        {
            lookLoaiBM.Properties.DataSource = db.BmLoaiBieuMaus;
            if (this.MaBM != 0)
            {
                objBM = db.BmBieuMaus.Single(p => p.MaBM == this.MaBM);
                txtTenBM.EditValue = objBM.TenBM;
                lookLoaiBM.EditValue = objBM.MaLBM;
                txtDienGiai.EditValue = objBM.Description;
                ckbKhoa.EditValue = objBM.IsLock;
            }
            else
            {
                objBM = new BmBieuMau();
            }
        }

        private void txtTenBM_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            using (frmDesign frm = new frmDesign())
            {
                frm.RtfText = objBM.Template;
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                {
                    objBM.Template = frm.RtfText;
                }
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (txtTenBM.Text.Trim() == "")
            {
                DialogBox.Error("Vui lòng nhập [Tên biểu mẫu], xin cảm ơn.");
                txtTenBM.Focus();
                return;
            }
            else
            {
                int count = db.BmBieuMaus.Where(p =>p.TenBM == txtTenBM.Text.Trim() & p.MaBM != this.MaBM).Count();
                if (count > 0)
                {
                    DialogBox.Error("Trùng tên biểu mẫu, vui lòng nhập lại");
                    txtTenBM.Focus();
                    return;
                }
            }

            if (lookLoaiBM.EditValue == null)
            {
                DialogBox.Error("Vui lòng chọn [Loại biểu mẫu], xin cảm ơn.");
                lookLoaiBM.Focus();
                return;
            }

            objBM.TenBM = txtTenBM.Text.Trim();
            objBM.MaLBM = (int?)lookLoaiBM.EditValue;
            objBM.Description = txtDienGiai.Text;
            objBM.IsLock = ckbKhoa.Checked;
            objBM.MaTN = MaTN;

            if (this.MaBM == 0)
            {
                objBM.MaNV = Common.User.MaNV;
                db.BmBieuMaus.InsertOnSubmit(objBM);
            }
            else
            {
                objBM.MaNVCN = objBM.MaNV = Common.User.MaNV;
            }

            db.SubmitChanges();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}