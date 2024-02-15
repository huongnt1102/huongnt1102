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


namespace Building.SMSZalo.Mau
{
    public partial class frmEdit : DevExpress.XtraEditors.XtraForm
    {
        public int? Id;
        public byte? MaTN;
        MasterDataContext db;
        SMSTemplate objSMSTemp;
        public bool? IsLoad;
        public frmEdit()
        {
            InitializeComponent();

            //Translate.Language.TranslateControl(this);

            db = new MasterDataContext();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            if (txtName.Text.Trim() == "")
            {
                DialogBox.Error("Vui lòng nhập tên biểu mẫu. Xin cảm ơn.");
                txtName.Focus();
                return;
            }

            if (txtContent.Text.Trim() == "")
            {
                DialogBox.Error("Vui lòng nhập nội dung mẫu. Xin cảm ơn.");
                txtContent.Focus();
                return;
            }

            try
            {
                if (Id == null)
                {
                    web_ZaloTemplate objSMSTemp = new web_ZaloTemplate();
                    objSMSTemp.Name = txtName.Text;
                    objSMSTemp.Content = txtContent.Text;
                    objSMSTemp.MaTN = MaTN;
                    db.web_ZaloTemplates.InsertOnSubmit(objSMSTemp);
                }
                else
                {
                    var objSMSTemp = db.web_ZaloTemplates.FirstOrDefault(o => o.Id == Id);
                    objSMSTemp.Name = txtName.Text;
                    objSMSTemp.Content = txtContent.Text;
                    objSMSTemp.MaTN = MaTN;
                }
                db.SubmitChanges();
                DialogBox.Success("Đã cập nhật thành công");
                IsLoad = true;
                this.Close();
            }
            catch { DialogBox.Alert("Đã xảy ra lỗi, vui lòng thử lại lần nữa, xin cảm ơn."); }
        }

        private void frmTemplates_Load(object sender, EventArgs e)
        {
            if (Id != null)
            {
                var objSMSTemp = db.web_ZaloTemplates.FirstOrDefault(o => o.Id == Id);
                txtName.Text = objSMSTemp.Name;
                txtContent.Text = objSMSTemp.Content;
            }
        }

        private void btnFields_Click(object sender, EventArgs e)
        {
            frmFields frm = new frmFields();
            frm.txtContent = txtContent;
            frm.Show(this);
        }
    }
}