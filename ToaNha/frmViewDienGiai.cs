using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using Library;

namespace ToaNha
{
    public partial class frmViewDienGiai : DevExpress.XtraEditors.XtraForm
    {
        public int MaLDV;

        public string TableName = "";

        public string DienGiai = "";

        MasterDataContext db = new MasterDataContext();

     
        public frmViewDienGiai()
        {
            InitializeComponent();
        }

        private void txtCauHinhDienGiai_EditValueChanged(object sender, EventArgs e)
        {
          
        }

        private void frmViewDienGiai_Load(object sender, EventArgs e)
        {
            treeField.DataSource = db.dvDienGiais;
            txtCauHinhDienGiai.Text = DienGiai;
        }

        private void txtCauHinhDienGiai_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            try
            {
                txtDienGiai.Text = db.func_dvHoaDonSetDienGiai(TableName, MaLDV, DateTime.Now.Month, DateTime.Now.Year, null, true, txtCauHinhDienGiai.Text);
            }
            catch { }
        }
    }
}