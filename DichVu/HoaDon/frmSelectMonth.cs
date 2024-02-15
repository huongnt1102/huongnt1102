using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Collections.Generic;

namespace DichVu.HoaDon
{
    public partial class frmSelectMonth : DevExpress.XtraEditors.XtraForm
    {
        public int Month, year;
        List<int> listThang;
        List<int> listNam;
        public frmSelectMonth()
        {
            InitializeComponent();
        }

        private void btnCHon_Click(object sender, EventArgs e)
        {
            Month = Convert.ToInt32(cbmThang.EditValue);
            year = Convert.ToInt32(cbmNam.EditValue);
            this.Close();
        }

        private void frmSelectMonth_Load(object sender, EventArgs e)
        {
            for (int i = 1; i <= 12; i++)
                listThang.Add(i);
            for (int i = 2000; i < 2050; i++)
                listNam.Add(i);
            cbmThang.Properties.Items.AddRange(listThang);
            cbmNam.Properties.Items.AddRange(listNam);
            cbmThang.EditValue = DateTime.Now.Month;
            cbmNam.EditValue = DateTime.Now.Year;
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}