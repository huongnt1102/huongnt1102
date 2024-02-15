using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Library;

namespace DichVu
{
    public partial class frmChooseDate : DevExpress.XtraEditors.XtraForm
    {
        public DateTime? PickedDate { get; set; }
        public frmChooseDate()
        {
            InitializeComponent();
        }

        private void frmChooseDate_Load(object sender, EventArgs e)
        {
            dateEdit1.DateTime = DateTime.Now;
            PickedDate = null;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (dateEdit1.DateTime.Year == 1)
            {
                DialogBox.Alert("Vui lòng chọn [Ngày nhập], xin cảm ơn.");  
                dateEdit1.Focus();
                return;
            }
            this.PickedDate = dateEdit1.DateTime;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}