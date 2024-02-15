using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace DichVu.YeuCau
{
    public partial class frmNoiDung : DevExpress.XtraEditors.XtraForm
    {
        public frmNoiDung()
        {
            InitializeComponent();
        }
        public string NoiDung { get; set; }
        private void checkButton1_CheckedChanged(object sender, EventArgs e)
        {
            NoiDung = memoEdit1.Text;
            
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}