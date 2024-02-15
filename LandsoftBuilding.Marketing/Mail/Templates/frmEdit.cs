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

namespace LandSoftBuilding.Marketing.Mail.Templates
{
    public partial class frmEdit : DevExpress.XtraEditors.XtraForm
    {
        public int? TempID { get; set; }

        MasterDataContext db = new MasterDataContext();
        mailTemplate objTemp;

        public frmEdit()
        {
            InitializeComponent();
        }

        private void frmTemplates_Load(object sender, EventArgs e)
        {
            lookCategory.Properties.DataSource = db.mailCategories;
            if (TempID != null)
            {
                objTemp = db.mailTemplates.Single(p => p.TempID == TempID);
                txtTempName.Text = objTemp.TempName;
                htmlContent.InnerHtml = objTemp.Contents;
                lookCategory.EditValue = objTemp.CateID;
            }
            else
                lookCategory.ItemIndex = 0;
        }

        private void btnFields_Click(object sender, EventArgs e)
        {
            frmFields frm = new frmFields();
            frm.txtContent = htmlContent;
            frm.Show(this);
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            if (txtTempName.Text.Trim() == "")
            {
                DialogBox.Error("Vui lòng nhập tên biểu mẫu. Xin cảm ơn.");
                txtTempName.Focus();
                return;
            }

            if (htmlContent.InnerHtml == null || htmlContent.InnerHtml.Trim() == "")
            {
                DialogBox.Error("Vui lòng nhập nội dung mẫu. Xin cảm ơn.");
                htmlContent.Focus();
                return;
            }

            if (lookCategory.Text == "<Vui lòng chọn>")
            {
                DialogBox.Error("Vui lòng chọn loại mẫu. Xin cảm ơn.");
                lookCategory.Focus();
                return;
            }

            if (TempID == null)
            {
                objTemp = new mailTemplate();
                objTemp.DateCreate = db.GetSystemDate();
                objTemp.StaffID = Common.User.MaNV;
                db.mailTemplates.InsertOnSubmit(objTemp);
            }
            else
            {
                objTemp.StaffModify = Common.User.MaNV;
                objTemp.DateModify = db.GetSystemDate();
            }

            objTemp.TempName = txtTempName.Text;
            objTemp.Contents = htmlContent.InnerHtml;
            objTemp.CateID = (short?)lookCategory.EditValue;           

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