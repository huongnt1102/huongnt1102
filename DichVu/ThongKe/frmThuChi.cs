using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Library;

namespace DichVu.ThongKe
{
    public partial class frmThuChi : DevExpress.XtraEditors.XtraForm
    {
        public tnNhanVien objnhanvien;
        public frmThuChi()
        {
            InitializeComponent();
            TranslateLanguage.TranslateControl(this);
        }

        private void frmThuChi_Load(object sender, EventArgs e)
        {
            Library.ThongKeCtl.ctlTKThuChi ctl = new Library.ThongKeCtl.ctlTKThuChi(objnhanvien);
            pnlDoanhThu.Controls.Add(ctl);
            ctl.Dock = DockStyle.Fill;
        }
    }
}