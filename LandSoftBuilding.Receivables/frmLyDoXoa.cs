using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace LandSoftBuilding.Receivables
{
    public partial class frmLyDoXoa : DevExpress.XtraEditors.XtraForm
    {
        public string LyDo { get; set; }
        public frmLyDoXoa()
        {
            InitializeComponent();
        }

        private void memoExEdit1_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void checkButton1_CheckedChanged(object sender, EventArgs e)
        {
            LyDo = memoEdit1.Text;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
    }
}