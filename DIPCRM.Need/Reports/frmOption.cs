using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DIPCRM.Need.Reports
{
    public partial class frmOption : DevExpress.XtraEditors.XtraForm
    {
        public int CateID = 1;
        public frmOption()
        {
            InitializeComponent();
        }

        private void btnWord_Click(object sender, EventArgs e)
        {
            CateID = 1;
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            CateID = 2;
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void btnPdf_Click(object sender, EventArgs e)
        {
            CateID = 3;
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void btnImage_Click(object sender, EventArgs e)
        {
            CateID = 4;
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }
    }
}
