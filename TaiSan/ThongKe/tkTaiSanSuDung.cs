using System;
using System.Windows.Forms;
using Library;
using System.Threading;

namespace TaiSan.ThongKe
{
    public partial class tkTaiSanSuDung : DevExpress.XtraEditors.XtraForm
    {
        Thread th1;
        Thread th2;
        Thread th3;
        Thread th4;
        public tnNhanVien objnhanvien;
        public tkTaiSanSuDung()
        {
            InitializeComponent();
            TranslateLanguage.TranslateControl(this);
        }

        private void tkTaiSanSuDung_Load(object sender, EventArgs e)
        {
            th1 = new Thread(LoadCtl1);
            th2 = new Thread(LoadCtl2);
            th3 = new Thread(LoadCtl3);
            th4 = new Thread(LoadCtl4);
            th3.IsBackground = true;
            th1.IsBackground = true;
            th2.IsBackground = true;
            th4.IsBackground = true;
            th1.Start();
            th2.Start();
            th3.Start();
            th4.Start();
        }
        private delegate void dlgAddItemN();
        private void LoadCtl1()
        {
            Library.ThongKeCtl.ctlTKTaisanTheoTT ctl = new Library.ThongKeCtl.ctlTKTaisanTheoTT(objnhanvien);
            ctl.Dock = DockStyle.Fill;

            if (pnlTaiSanTheoTrangThai.InvokeRequired)
            {
                Invoke(new dlgAddItemN(LoadCtl1));
            }
            else
            {
                pnlTaiSanTheoTrangThai.Controls.Add(ctl);
            }
        }
        private void LoadCtl2()
        {
            var ctl = new Library.ThongKeCtl.ctlThongKeTaiSanTheoToaNha(objnhanvien);
            ctl.Dock = DockStyle.Fill;

            if (pnl2.InvokeRequired)
            {
                Invoke(new dlgAddItemN(LoadCtl2));
            }
            else
            {
                pnl2.Controls.Add(ctl);
            }
        }

        private void LoadCtl3()
        {
            Library.ThongKeCtl.ctlTaiSanSuDung ctl3 = new Library.ThongKeCtl.ctlTaiSanSuDung(objnhanvien);
            ctl3.Dock = DockStyle.Fill;

            if (pnl3.InvokeRequired)
            {
                Invoke(new dlgAddItemN(LoadCtl3));
            }
            else
            {
                pnl3.Controls.Add(ctl3);
            }
        }

        private void LoadCtl4()
        {
            Library.ThongKeCtl.ctlThongKeTaiSanNhapVaoTrongNam ctl4 = new Library.ThongKeCtl.ctlThongKeTaiSanNhapVaoTrongNam(objnhanvien);
            ctl4.Dock = DockStyle.Fill;

            if (pnl4.InvokeRequired)
            {
                Invoke(new dlgAddItemN(LoadCtl4));
            }
            else
            {
                pnl4.Controls.Add(ctl4);
            }
        }
        private void tkTaiSanSuDung_SizeChanged(object sender, EventArgs e)
        {
            splitContainerControl1.SplitterPosition = splitContainerControl1.Width / 2;
            splitContainerControl2.SplitterPosition = splitContainerControl2.Height / 2;
            splitContainerControl3.SplitterPosition = splitContainerControl3.Height / 2;
        }

        private void tkTaiSanSuDung_FormClosed(object sender, FormClosedEventArgs e)
        {
            th1.Abort();
            th2.Abort();
            th3.Abort();
            th4.Abort();
        }
        
    }
}