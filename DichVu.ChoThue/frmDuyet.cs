using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Library;
using System.Linq;

namespace DichVu.ChoThue
{
    public partial class frmDuyet : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public int? MaHD { get; set; }
        public thueLichSu objLS { get; set; }

        public frmDuyet()
        {
            InitializeComponent();

            db = new MasterDataContext();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            objLS = new thueLichSu();
            objLS.DienGiai = txtDienGiai.Text.Trim();
            objLS.MaHD = MaHD;

            DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
    }
}