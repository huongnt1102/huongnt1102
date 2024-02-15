using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using Microsoft.Win32;
using System.Management;

namespace Building.AppVime
{
    public partial class frmConfig : DevExpress.XtraEditors.XtraForm
    {

        public frmConfig()
        {
            InitializeComponent();
        }

        private void frmApp_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }

        private void frmApp_Load(object sender, EventArgs e)
        {
            CommonVime.GetConfig();
            txtApiKey.Text = CommonVime.ApiKey;
            txtSecretKey.Text = CommonVime.SecretKey;

            //PhanQuyen();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtApiKey.Text.Trim() == "")
            {
                MessageBox.Show("Vui lòng nhập [Api key], xin cảm ơn.", "Thông báo", MessageBoxButtons.OK);
                txtApiKey.Focus();
                return;
            }

            if (txtSecretKey.Text.Trim() == "")
            {
                MessageBox.Show("Vui lòng nhập [Secret key], xin cảm ơn.", "Thông báo", MessageBoxButtons.OK);
                txtSecretKey.Focus();
                return;
            }
                        
            CommonVime.ApiKey = txtApiKey.Text.Trim();
            CommonVime.SecretKey = txtSecretKey.Text.Trim();

            var result = CommonVime.SaveConfig();

            if (result)
            {
                MessageBox.Show("Dữ liệu đã được cập nhật.", "Thông báo", MessageBoxButtons.OK);
                this.Close();
            }
            else
                MessageBox.Show("Đã xảy ra sự cố. Vui lòng kiểm tra lại, xin cảm ơn.", "Thông báo", MessageBoxButtons.OK);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCreateKey_Click(object sender, EventArgs e)
        {
            txtApiKey.Text = CommonVime.GetApiKey();
            txtSecretKey.Text = CommonVime.GetSecretKey();
        }
    }
}