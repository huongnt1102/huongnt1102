using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace DIP.SoftPhoneAPI
{
    public partial class frmInputBox : DevExpress.XtraEditors.XtraForm
    {
        public frmInputBox()
        {
            InitializeComponent();
        }

        public string TextValue { get; set; }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtText.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập liệu");
                txtText.Focus();
            }

            this.TextValue = txtText.Text.Trim();

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void frmInputBox_Load(object sender, EventArgs e)
        {
            txtText.Focus();
        }
    }
}