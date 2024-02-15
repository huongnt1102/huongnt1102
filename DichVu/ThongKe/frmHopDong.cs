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
    public partial class frmHopDong : DevExpress.XtraEditors.XtraForm
    {
        public Library.tnNhanVien objnhanvien;
        public frmHopDong()
        {
            InitializeComponent();
            TranslateLanguage.TranslateControl(this);
        }

        private void frmHopDong_Load(object sender, EventArgs e)
        {
            Library.ThongKeCtl.ctlTKHopDongTheoTrangThai ctl = new Library.ThongKeCtl.ctlTKHopDongTheoTrangThai(objnhanvien);
            pnlHDTTTT.Controls.Add(ctl);
            ctl.Dock = DockStyle.Fill;
        }
    }
}