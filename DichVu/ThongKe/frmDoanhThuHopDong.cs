using System;
using System.Windows.Forms;
using Library;

namespace DichVu.ThongKe
{
    public partial class frmDoanhThuHopDong : DevExpress.XtraEditors.XtraForm
    {
        public frmDoanhThuHopDong()
        {
            InitializeComponent();
            TranslateLanguage.TranslateControl(this);
        }

        private void frmDoanhThuHopDong_Load(object sender, EventArgs e)
        {
            Library.ThongKeCtl.tkDoanhThuHopDongThue ctl = new Library.ThongKeCtl.tkDoanhThuHopDongThue();
            panelControl1.Controls.Add(ctl);
            ctl.Dock = DockStyle.Fill;
        }
    }
}