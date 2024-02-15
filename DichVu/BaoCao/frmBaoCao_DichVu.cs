using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraReports.UI;
using Library;

namespace DichVu.BaoCao
{
    public partial class frmBaoCao_DichVu : Form
    {
        public frmBaoCao_DichVu()
        {
            InitializeComponent();

            var list = Library.ManagerTowerCls.GetAllTower(Common.User);
            lkToaNha.Properties.DataSource = list;
            if (list.Count > 0)
                lkToaNha.EditValue = list[1].MaTN;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                rptBaoCaoCacKhoanDaThu rpt = new rptBaoCaoCacKhoanDaThu((byte)lkToaNha.EditValue, dateTimePicker1.Value.Month, dateTimePicker1.Value.Year);
                rpt.Print();
            }
            catch (Exception ex)
            {
                string s = ex.Message;
            }
        }
    }
}
