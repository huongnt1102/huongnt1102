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
using LandSoftBuilding.Marketing.SMS;


namespace LandSoftBuilding.Marketing.SMS
{
    public partial class frmTemplates : DevExpress.XtraEditors.XtraForm
    {
        public int KeyID = 0;
        public bool IsUpdate = false;
        DateTime? DateCreate;
        int? StaffID = 0;

        MasterDataContext db;
        SMSTemplate objSMSTemp;
        public frmTemplates()
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
            //if (txtTempName.Text.Trim() == "")
            //{
            //    DialogBox.Infomation("Vui lòng nhập tên biểu mẫu. Xin cảm ơn.");
            //    txtTempName.Focus();
            //    return;
            //}

            //if (txtContents.Text.Trim() == "")
            //{
            //    DialogBox.Infomation("Vui lòng nhập nội dung mẫu. Xin cảm ơn.");
            //    txtContents.Focus();
            //    return;
            //}

            //if (lookUpCategory.Text == "<Vui lòng chọn>")
            //{
            //    DialogBox.Infomation("Vui lòng chọn loại mẫu. Xin cảm ơn.");
            //    lookUpCategory.Focus();
            //    return;
            //}

           // if (KeyID <= 0)
           // {
                //objSMSTemp = new SMSTemplate();
                objSMSTemp.DateCreate = DateTime.Now;
                objSMSTemp.StaffID = Common.User.MaNV;
           // }
           // else
           // {
                objSMSTemp.StaffIDModify = Common.User.MaNV;
                objSMSTemp.DateModify = DateTime.Now;
                objSMSTemp.DateCreate = DateCreate;
                objSMSTemp.StaffID = StaffID;
           // }

           // objSMSTemp.TempName = txtTempName.Text;
            objSMSTemp.Contents = txtContents.Text;
            //objSMSTemp.CateID = Convert.ToInt32(lookUpCategory.EditValue);   

          

            try
            {
                db.SubmitChanges();
                //DialogBox.Infomation();
                IsUpdate = true;
                this.Close();
            }
            catch { DialogBox.Alert("Đã xảy ra lỗi, vui lòng thử lại lần nữa, xin cảm ơn."); }
        }

        private void frmTemplates_Load(object sender, EventArgs e)
        {
           // lookUpCategory.Properties.DataSource = db.SMSCategories;
           
                objSMSTemp = db.SMSTemplates.FirstOrDefault();
                if (objSMSTemp != null)
                {
                    //  txtTempName.Text = objSMSTemp.TempName;
                    txtContents.Text = objSMSTemp.Contents;
                    // lookUpCategory.EditValue = objSMSTemp.CateID;
                    StaffID = objSMSTemp.StaffID;
                    DateCreate = objSMSTemp.DateCreate;
                }
            
          
        }

        private void btnFields_Click(object sender, EventArgs e)
        {
            frmFields frm = new frmFields();
            frm.txtContent = txtContents;
            frm.Show(this);
        }
    }
}