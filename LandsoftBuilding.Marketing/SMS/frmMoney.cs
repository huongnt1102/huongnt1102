using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Library;

namespace LandSoftBuilding.Marketing.sms
{
    public partial class frmMoney : DevExpress.XtraEditors.XtraForm
    {
        public frmMoney()
        {
            InitializeComponent();

           // Translate.Language.TranslateControl(this);
        }

        private void frmMoney_Load(object sender, EventArgs e)
        {
            var wait = DialogBox.WaitingForm();
            try
            {
                //var smsClient = null; //new ServiceReference1.APISoapClient("APISoap");
                //label1.Text = string.Format("Số tiền trong tài khoản của bạn: {0:#,0} VNĐ.", smsClient.getBalance("username", "password"));
            }
            catch (Exception ex)
            {
                label1.Text = ex.Message;
            }
            finally
            {
                wait.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}