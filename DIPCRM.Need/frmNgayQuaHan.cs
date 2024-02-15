using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Library;
using DevExpress.XtraEditors;


namespace DIPCRM.NhuCau
{
    public partial class frmNgayQuaHan : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db = new MasterDataContext();

        //private ncNhuCau_NgayQuaHan objNgayQuahan;

        public frmNgayQuaHan()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmAddProduct_Load(object sender, EventArgs e)
        {
            //objNgayQuahan = db.ncNhuCau_NgayQuaHans.FirstOrDefault();

            //spinNgayQuaHan.EditValue = objNgayQuahan != null ? objNgayQuahan.Number : 0;
            //btnSave.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //objNgayQuahan.Number = (int?)spinNgayQuaHan.Value;
            db.SubmitChanges();
            this.Close();
        }
    }
}
