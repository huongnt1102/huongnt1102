using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace DichVu.Confirm
{
    public partial class frmSign : DevExpress.XtraEditors.XtraForm
    {
        public string Description = "";
        public frmSign()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Description = txtDienGiai.Text.Trim();
            DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
    }
}