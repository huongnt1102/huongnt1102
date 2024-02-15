using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DichVu.PhieuDatCocGiuCho
{
    public partial class frmPhieuDatCocGiuCho : DevExpress.XtraEditors.XtraForm
    {
        public frmPhieuDatCocGiuCho()
        {
            InitializeComponent();
        }

        private void btnAdd_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmAdd _frmAdd = new frmAdd();
            _frmAdd.ShowDialog();
        }
    }
}
