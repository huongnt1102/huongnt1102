using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Threading;
using Library;

namespace DichVu.ThongKe
{
    public partial class frmYeuCau : DevExpress.XtraEditors.XtraForm
    {
        Thread th1;
        Thread th2;
        Thread th3;
        Thread th4;
        public tnNhanVien objnhanvien;
        public frmYeuCau()
        {
            InitializeComponent();
            TranslateLanguage.TranslateControl(this);
        }

        private void frmYeuCau_Load(object sender, EventArgs e)
        {
            th1 = new Thread(LoadCtl1);
            th2 = new Thread(LoadCtl2);
            th3 = new Thread(LoadCtl3);
            th4 = new Thread(LoadCtl4);

            th1.IsBackground = true;
            th2.IsBackground = true;
            th3.IsBackground = true;
            th4.IsBackground = true;

            th1.Start();
            th2.Start();
            th3.Start();
            th4.Start();
        }

        private void frmYeuCau_SizeChanged(object sender, EventArgs e)
        {
            splitContainerControl1.SplitterPosition = splitContainerControl1.Width / 2;
            splitContainerControl2.SplitterPosition = splitContainerControl2.Height / 2;
            splitContainerControl3.SplitterPosition = splitContainerControl3.Height / 2;
        }


        private delegate void dlgAddItemN();
        private void LoadCtl1()
        {
            Library.ThongKeCtl.ctlThongKeYeuCau ctl = new Library.ThongKeCtl.ctlThongKeYeuCau(objnhanvien);
            ctl.Dock = DockStyle.Fill;

            if (pnlThongKeYeuCau.InvokeRequired)
            {
                Invoke(new dlgAddItemN(LoadCtl1));
            }
            else
            {
                pnlThongKeYeuCau.Controls.Add(ctl);
            }
        }

        private void LoadCtl2()
        {
            Library.ThongKeCtl.ctlThongKeYeuCauGuiDenPhongBan ctl1 = new Library.ThongKeCtl.ctlThongKeYeuCauGuiDenPhongBan(objnhanvien);
            ctl1.Dock = DockStyle.Fill;

            if (pnlThongKeYeuCauGuiDenPB.InvokeRequired)
            {
                Invoke(new dlgAddItemN(LoadCtl2));
            }
            else
            {
                pnlThongKeYeuCauGuiDenPB.Controls.Add(ctl1);
            }
        }

        private void LoadCtl3()
        {
            Library.ThongKeCtl.ctlTKYeuCauTheoMucDo ctl2 = new Library.ThongKeCtl.ctlTKYeuCauTheoMucDo(objnhanvien);
            ctl2.Dock = DockStyle.Fill;

            if (pnl3.InvokeRequired)
            {
                Invoke(new dlgAddItemN(LoadCtl3));
            }
            else
            {
                pnl3.Controls.Add(ctl2);
            }
        }

        private void LoadCtl4()
        {
            Library.ThongKeCtl.ctlThongKeSoLuongYeuCauTrongNam ctl3 = new Library.ThongKeCtl.ctlThongKeSoLuongYeuCauTrongNam(objnhanvien);
            ctl3.Dock = DockStyle.Fill;

            if (pnl4.InvokeRequired)
            {
                Invoke(new dlgAddItemN(LoadCtl4));
            }
            else
            {
                pnl4.Controls.Add(ctl3);
            }
        }
    }
}