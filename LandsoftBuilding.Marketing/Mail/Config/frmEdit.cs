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

namespace LandSoftBuilding.Marketing.Mail.Config
{
    public partial class frmEdit : DevExpress.XtraEditors.XtraForm
    {
        public int? MailID { get; set; }
        public byte? MaTN { get; set; }

        MasterDataContext db = new MasterDataContext();
        mailConfig objConfig;

        public frmEdit()
        {
            InitializeComponent();
        }

        private void frmConfig_Load(object sender, EventArgs e)
        {
            if (MailID != null)
            {
                objConfig = db.mailConfigs.Single(p => p.ID == MailID);

                txtDisplay.EditValue = objConfig.Display;
                txtEmail.EditValue = objConfig.Email;
                txtReply.EditValue = objConfig.Reply;
                ckbSynonym.EditValue = objConfig.isSynonym;

                txtOutServer.EditValue = objConfig.Server;
                spOutPort.EditValue = objConfig.Port;
                spSendMax.EditValue = objConfig.SendMax;
                ckbOutSsl.EditValue = objConfig.EnableSsl;

                txtInServer.EditValue = objConfig.InServer;
                spInPort.EditValue = objConfig.InPort;
                ckbInSsl.EditValue = objConfig.InSsl;
                txtPassword.EditValue = it.EncDec.Decrypt(objConfig.Password);
            }
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            if (txtEmail.Text == "")
            {
                DialogBox.Error("Vui lòng nhập email");
                txtEmail.Focus();
                return;
            }

            if (txtReply.Text.Trim() == "")
            {
                DialogBox.Error("Vui lòng nhập Email nhận");
                txtReply.Focus();
                return;
            }

            if (txtPassword.Text == "")
            {
                DialogBox.Error("Vui lòng nhập mật khẩu");
                txtPassword.Focus();
                return;
            }

            if (txtOutServer.Text == "")
            {
                DialogBox.Error("Vui lòng nhập máy chủ gửi mail");
                txtOutServer.Focus();
                return;
            }

            if (ckbInSsl.Enabled)
            {
                if (txtInServer.Text.Trim() == "")
                {
                    DialogBox.Error("Vui lòng nhập máy chủ nhận mail");
                    txtInServer.Focus();
                    return;
                }
            }

            if (MailID == null)
            {
                objConfig = new mailConfig();
                objConfig.MaTN = this.MaTN;
                db.mailConfigs.InsertOnSubmit(objConfig);
            }

            objConfig.Display = txtDisplay.Text;
            objConfig.Email = txtEmail.Text;
            objConfig.Reply = txtReply.Text;
            objConfig.Password = it.EncDec.Encrypt(txtPassword.Text);
            objConfig.isSynonym = ckbSynonym.Enabled;
            objConfig.Server = txtOutServer.Text;
            objConfig.Port = Convert.ToInt32(spOutPort.Value);
            objConfig.EnableSsl = ckbOutSsl.Checked;
            objConfig.SendMax = int.Parse(spSendMax.EditValue.ToString());
            objConfig.InServer = txtInServer.Text;
            objConfig.InPort = Convert.ToInt32(spInPort.EditValue);
            objConfig.InSsl = ckbInSsl.Checked;
            objConfig.StaffID = Common.User.MaNV;
            objConfig.DateModify = DateTime.Now;
            
            db.SubmitChanges();

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtEmail_EditValueChanged(object sender, EventArgs e)
        {
            txtReply.Text = txtEmail.Text;
        }

        private void txtOutServer_EditValueChanged(object sender, EventArgs e)
        {
            txtInServer.Text = txtOutServer.Text;
        }
    }
}