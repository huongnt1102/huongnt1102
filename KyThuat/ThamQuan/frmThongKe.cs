using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Library;

namespace KyThuat.ThamQuan
{
    public partial class frmThongKe : DevExpress.XtraEditors.XtraForm
    {
        public tnNhanVien objnhanvien;
        public frmThongKe()
        {
            InitializeComponent();
            TranslateLanguage.TranslateControl(this);
        }

        private void frmThongKe_Load(object sender, EventArgs e)
        {
            Library.ThongKeCtl.ctlTKTaisanTheoTT ctl = new Library.ThongKeCtl.ctlTKTaisanTheoTT(objnhanvien);
            pnlThongKe.Controls.Add(ctl);
            ctl.Dock = DockStyle.Fill;
        }
    }
}