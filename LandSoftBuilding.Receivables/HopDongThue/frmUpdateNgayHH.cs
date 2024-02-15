using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Library;

namespace LandSoftBuilding.Receivables
{
    public partial class frmUpdateNgayHH : DevExpress.XtraEditors.XtraForm
    {
        public DateTime NgayHH;

        public frmUpdateNgayHH()
        {
            InitializeComponent();
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            if (dateNgayHH.EditValue == null)
            {
                DialogBox.Error("Vui lòng nhập đơn giá");
                return;
            }

            NgayHH = dateNgayHH.DateTime;

            DialogResult = System.Windows.Forms.DialogResult.OK;
        }
    }
}