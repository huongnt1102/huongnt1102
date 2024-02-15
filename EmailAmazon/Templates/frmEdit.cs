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
using EmailAmazon.API;

namespace EmailAmazon.Templates
{
    public partial class frmEdit : DevExpress.XtraEditors.XtraForm
    {
        public int TempID { get; set; }

        MasterDataContext db = new MasterDataContext();
        mailTemplate objTemp;

        public frmEdit()
        {
            InitializeComponent();
            MailCommon.cmd = new APISoapClient();
            MailCommon.cmd.Open();
        }

        private void frmTemplates_Load(object sender, EventArgs e)
        {
            if (this.TempID == 0)
                return;
            MauMail mauMail = MailCommon.cmd.DetailMauMail(MailCommon.MaHD, MailCommon.MatKhau, this.TempID);
            this.txtTempName.Text = mauMail.TieuDe;
            this.htmlContent.InnerHtml = mauMail.NoiDung;
        }

        private void btnFields_Click(object sender, EventArgs e)
        {
            frmFields frm = new frmFields();
            frm.txtContent = htmlContent;
            frm.Show(this);
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            if (this.txtTempName.Text.Trim() == "")
            {
                DialogBox.Error("Vui lòng nhập tên biểu mẫu. Xin cảm ơn.");
                this.txtTempName.Focus();
            }
            else if (this.htmlContent.InnerHtml == null || this.htmlContent.InnerHtml.Trim() == "")
            {
                DialogBox.Error("Vui lòng nhập nội dung mẫu. Xin cảm ơn.");
                this.htmlContent.Focus();
            }
            else
            {
                switch (MailCommon.cmd.EditMauMail(MailCommon.MaHD, MailCommon.MatKhau, this.TempID, this.txtTempName.Text, this.htmlContent.InnerHtml, Common.User.HoTenNV))
                {
                    case Result.MauMailDaTonTai:
                        DialogBox.Error("Trùng tiêu đề mẫu mail");
                        break;
                    case Result.ThanhCong:
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                        break;
                }
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}