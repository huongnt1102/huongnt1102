using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Building.AppVime.Tower
{
    public partial class frmConfigMomo : DevExpress.XtraEditors.XtraForm
    {
        public int TowerId { get; set; }

        public frmConfigMomo()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            using (var db = new Library.MasterDataContext())
            {
                try
                {
                    var objTower = db.app_TowerSettingPages.FirstOrDefault(p => p.Id == TowerId);
                    if (objTower != null)
                    {
                        objTower.PublicKey = txtPublicKey.Text;
                        objTower.IsUseEWallet = chkIsUseWallet.Checked;
                        db.SubmitChanges();

                        DialogResult = DialogResult.OK;
                    }
                }
                catch
                {
                    Library.DialogBox.Error("Đã xả ra lỗi. Vui lòng thử lại sau, xin cảm ơn.");
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmConfigMomo_Load(object sender, EventArgs e)
        {
            using(var db = new Library.MasterDataContext())
            {
                var objTower = db.app_TowerSettingPages.FirstOrDefault(p => p.Id == TowerId);
                if(objTower != null)
                {
                    txtPublicKey.Text = objTower.PublicKey;
                    chkIsUseWallet.Checked = objTower.IsUseEWallet.GetValueOrDefault();
                }
            }
        }

        private void llinkConvert_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(txtLink.Text);
        }

        private void txtLink_MouseDown(object sender, MouseEventArgs e)
        {
            txtLink.SelectAll();
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }
    }
}
