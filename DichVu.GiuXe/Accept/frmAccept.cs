using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Library;

namespace DichVu.GiuXe.Accept
{
    public partial class frmAccept : DevExpress.XtraEditors.XtraForm
    {
        public string reason { get; set; }
        public frmAccept()
        {
            InitializeComponent();
        }

        private void frmExtend_Load(object sender, EventArgs e)
        {
            txtReason.Text = "";
        }

        private void itemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            reason = txtReason.Text;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void itemCancel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}