using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace KyThuat.KeHoach
{
    public partial class frmTimeSelect : DevExpress.XtraEditors.XtraForm
    {
        public DateTime? NgayTao { get; set; }
        public frmTimeSelect()
        {
            InitializeComponent();
        }

        private void frmTimeSelect_Load(object sender, EventArgs e)
        {
            dateEdit1.EditValue = DateTime.Now;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            NgayTao = (DateTime?)dateEdit1.EditValue;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}