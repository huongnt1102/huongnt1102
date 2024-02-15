using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Library;

namespace Building.WorkSchedule.NhiemVu
{
    public partial class frmManager : DevExpress.XtraEditors.XtraForm
    {
        public tnNhanVien objNV;
        public frmManager()
        {
            InitializeComponent();
        }

        private void frmManager_Load(object sender, EventArgs e)
        {
            var ctl = new List_ctl() { frm = this };
            ctl.objNV = objNV;
            ctl.Dock = DockStyle.Fill;
            this.Controls.Clear();
            this.Controls.Add(ctl);
        }
    }
}