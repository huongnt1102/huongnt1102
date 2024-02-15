using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using LandSoft.Library;

namespace LandSoft.DuAn.BieuMau
{
    public partial class frmEdit : DevExpress.XtraEditors.XtraForm
    {
        public int MaBM, MaDA;
        MasterDataContext db = new MasterDataContext();
        daBieuMau objBM;

        public frmEdit()
        {
            InitializeComponent();
        }

        private void frmEdit_Load(object sender, EventArgs e)
        {
            DialogBox.ShowWaitForm(this);

            lookLoaiBM.Properties.DataSource = db.daLoaiBieuMaus;
            if (this.MaBM != 0)
            {
                try
                {
                    objBM = db.daBieuMaus.SingleOrDefault(p => p.MaBM == this.MaBM);
                    txtTenBM.EditValue = objBM.TenBM;
                    lookLoaiBM.EditValue = objBM.MaLBM;
                    txtDienGiai.EditValue = objBM.DienGiai;
                    ckbKhoa.EditValue = objBM.Khoa;
                    lookUpThaoTac.EditValue = objBM.MaTTac;
                    txtTenLM.Text = objBM.TenLM;

                    chkBarcode.Checked = objBM.IsBarcode.GetValueOrDefault();
                    chkLogoCty.Checked = objBM.IsPowerSign.GetValueOrDefault();

                    spinBottom.EditValue = objBM.PaddingBottom ?? 0;
                    spinLeft.EditValue = objBM.PaddingLeft ?? 0;
                    spinTop.EditValue = objBM.PaddingTop ?? 0;
                    spinRight.EditValue = objBM.PaddingRight ?? 0;

                    chkLogoDIP.Checked = objBM.IsDIPLogo.GetValueOrDefault();
                    btnImageLogo.Text = chkLogoCty.Checked ? objBM.LogoUrl : "";
                }
                catch {
                    this.Close();
                }
            }
            else
            {
                objBM = new daBieuMau();
            }

           // LandSoft.Translate.Language.TranslateControl(this);
            DialogBox.HideWaitForm();
        }

        private void txtTenBM_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            using (frmDesign frm = new frmDesign())
            {
                frm.RtfText = objBM.NoiDung;
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                {
                    objBM.NoiDung = frm.RtfText;
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
                int count = db.daBieuMaus.Where(p => p.MaDA == this.MaDA & p.TenBM == txtTenBM.Text.Trim() & p.MaBM != this.MaBM).Count();
                if (count > 0)
                {
                    DialogBox.Error("Trùng [Tên biểu mẫu], vui lòng nhập lại");
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
            objBM.MaLBM = (byte)lookLoaiBM.EditValue;
            objBM.DienGiai = txtDienGiai.Text;
            objBM.Khoa = ckbKhoa.Checked;
            objBM.NgayCN = DateTime.Now;
            objBM.MaNV = Library.Common.StaffID;
            objBM.MaTTac = (int?)lookUpThaoTac.EditValue;
            objBM.TenLM = txtTenLM.Text.Trim();

            objBM.PaddingBottom = Convert.ToInt32(spinBottom.EditValue);
            objBM.PaddingLeft = Convert.ToInt32(spinLeft.EditValue);
            objBM.PaddingRight = Convert.ToInt32(spinRight.EditValue);
            objBM.PaddingTop = Convert.ToInt32(spinTop.EditValue);

            objBM.IsPowerSign = chkLogoCty.Checked;
            objBM.IsBarcode = chkBarcode.Checked;
            objBM.IsDIPLogo = chkLogoDIP.Checked;
            objBM.LogoUrl = objBM.IsPowerSign.GetValueOrDefault() ? btnImageLogo.Text : "";

            if (this.MaBM == 0)
            {
                objBM.MaDA = this.MaDA;
                db.daBieuMaus.InsertOnSubmit(objBM);
            }

            db.SubmitChanges();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lookLoaiBM_EditValueChanged(object sender, EventArgs e)
        {
            lookUpThaoTac.Properties.DataSource = db.nvFormActions.Where(p => p.FormID == Convert.ToInt32(lookLoaiBM.EditValue))
                .Select(p => new { p.ActionID, p.No, p.nvThaoTac.Name, p.nvThaoTac.ID });
        }

        private void panelControl1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnReview_Click(object sender, EventArgs e)
        {
            try
            {
                var frm = new DuAn.BieuMau.frmPreview();
                frm.RtfText = objBM.NoiDung;
                frm.ShowDialog(this);
            }
            catch { }
        }

        private void btnImageLogo_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            //var frm = new FTP.frmUploadFile();
            //if (frm.SelectFile(false))
            //{
            //    frm.Folder = "doc/" + DateTime.Now.ToString("yyyy/MM/dd");
            //    frm.ClientPath = frm.ClientPath;
            //    frm.ShowDialog();
            //    if (frm.DialogResult != DialogResult.OK) return;
            //    btnImageLogo.Text = frm.FileName;
            //}
            //frm.Dispose();
        }
    }
}