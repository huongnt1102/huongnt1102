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

namespace DichVu.SendMail
{
    public partial class frmEmailAccount : DevExpress.XtraEditors.XtraForm
    {
        public SendMailAccount objacc;
        MasterDataContext db;

        public frmEmailAccount()
        {
            InitializeComponent();
            db = new MasterDataContext();
            TranslateLanguage.TranslateControl(this);
        }

        private void frmEmailAccount_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            if (objacc != null)
            {
                objacc = db.SendMailAccounts.Single(p => p.ID == objacc.ID);
                txtPort.Text = objacc.Port.ToString();
                txtPWD.Text = objacc.Password;
                txtServer.Text = objacc.Server;
                txtUID.Text = objacc.Username;
                txtdiachiEmail.Text = objacc.DiaChi;
            }
            else
            {
                objacc = new SendMailAccount();
                db.SendMailAccounts.InsertOnSubmit(objacc);
            }
        }

        private void btnChapNhan_Click(object sender, EventArgs e)
        {
            objacc.Password = Commoncls.EncodeString(txtPWD.Text.Trim());
            objacc.Username = txtUID.Text.Trim();
            objacc.Server = txtServer.Text.Trim();
            objacc.Port = Convert.ToInt32(txtPort.EditValue);
            objacc.DiaChi = txtdiachiEmail.Text.Trim();
            try
            {
                db.SubmitChanges();
                DialogBox.Alert("Lưu thành công");  
                DialogResult = System.Windows.Forms.DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}