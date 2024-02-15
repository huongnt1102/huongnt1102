using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace DIPCRM.Need
{
    public partial class frmDetail : DevExpress.XtraEditors.XtraForm
    {
        public frmDetail(int? maNC)
        {
            InitializeComponent();

            

            ctlManager1.MaNC = maNC;
        }
    }
}