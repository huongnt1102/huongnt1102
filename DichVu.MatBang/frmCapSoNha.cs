using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Library;
using System.Linq;

namespace MatBang
{
    public partial class frmCapSoNha : DevExpress.XtraEditors.XtraForm
    {
        public mbMatBang objmatbang;
        MasterDataContext db;
        public frmCapSoNha()
        {
            InitializeComponent();
            db = new MasterDataContext();
            TranslateLanguage.TranslateControl(this);
        }

        private void frmCapSoNha_Load(object sender, EventArgs e)
        {
            if (objmatbang != null)
            {
                objmatbang = db.mbMatBangs.Single(p => p.MaMB == objmatbang.MaMB);
                txtMaMB.Text = objmatbang.MaSoMB;
                txtSoNha.Text = objmatbang.SoNha;
            }
            else
            {
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            objmatbang.SoNha = txtSoNha.Text.Trim();
            db.SubmitChanges();
            this.Close();
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }
    }
}