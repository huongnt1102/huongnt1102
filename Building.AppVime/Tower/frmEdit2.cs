using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Building.AppVime.Tower
{
    public partial class frmEdit2 : DevExpress.XtraEditors.XtraForm
    {
        public frmEdit2()
        {
            InitializeComponent();
        }

        private void frmEdit_Load(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
