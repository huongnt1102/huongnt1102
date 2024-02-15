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

namespace FTP
{
    public partial class frmConfig : DevExpress.XtraEditors.XtraForm
    {
        public frmConfig()
        {
            InitializeComponent();
        }

        private MasterDataContext db;
        private tblConfig objConfig;
          
        private void frmConfig_Load(object sender, EventArgs e)
        {
            try
            {
                db = new MasterDataContext();
                objConfig = db.tblConfigs.First();
                txtFtpUrl.EditValue = objConfig.FtpUrl;
                txtFtpUser.EditValue = objConfig.FtpUser;
                txtFtpPass.EditValue = it.EncDec.Decrypt(objConfig.FtpPass);
                txtWebUrl.EditValue = objConfig.WebUrl;
                txtApiLdap.EditValue = objConfig.ApiLdap;
            }
            catch { }
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            try
            {
                objConfig.FtpUrl = txtFtpUrl.Text.Trim();
                objConfig.FtpUser = txtFtpUser.Text.Trim();
                objConfig.FtpPass = it.EncDec.Encrypt(txtFtpPass.Text.Trim());
                objConfig.WebUrl = txtWebUrl.Text.Trim();
                objConfig.ApiLdap = txtApiLdap.Text.Trim();
                db.SubmitChanges();

                DialogBox.Alert("Dữ liệu đã được cập nhật.");
                this.Close();
            }
            catch { }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}