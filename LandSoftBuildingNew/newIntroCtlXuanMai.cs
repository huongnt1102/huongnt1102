using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Threading;
using Library;

namespace LandSoftBuildingMain
{
    public partial class newIntroCtlXuanMai : DevExpress.XtraEditors.XtraUserControl
    {
        Thread th1;
        Thread th2;
        Thread th3;
        Thread th4;
        public tnNhanVien objnhanvien;
        private delegate void dlgAddItemN();

        public newIntroCtlXuanMai()
        {
            InitializeComponent();
        }

        private void newIntroCtl_SizeChanged(object sender, EventArgs e)
        {
            splitContainerControl2.SplitterPosition = splitContainerControl2.Width / 2;
        }

        private void newIntroCtl_Load(object sender, EventArgs e)
        {
            BackgroundWorker bw = new BackgroundWorker();
            bw.DoWork += bw_DoWork;
            bw.RunWorkerAsync();
        }

        void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            th2 = new Thread(LoadCtl2) { IsBackground = true };
            th2.Start();

            th3 = new Thread(LoadCtl3) { IsBackground = true };
            th3.Start();

            th4 = new Thread(LoadCtl4) { IsBackground = true };
            th4.Start();
        }
        
        private void LoadCtl2()
        {
            Library.NoticeCtl.NhacViecCtl ctl1 = new Library.NoticeCtl.NhacViecCtl() { Dock = DockStyle.Fill, objnhanvien = objnhanvien };

            if (pnlNhacViec.InvokeRequired)
            {
                BeginInvoke(new dlgAddItemN(LoadCtl2));
            }
            else
            {
                pnlNhacViec.Controls.Add(ctl1);
            }
        }

        private void LoadCtl3()
        {
            Library.NoticeCtl.YeuCauCtl ctl1 = new Library.NoticeCtl.YeuCauCtl() { Dock = DockStyle.Fill, objnhanvien = objnhanvien };

            if (pnlYeuCau.InvokeRequired)
            {
                BeginInvoke(new dlgAddItemN(LoadCtl3));
            }
            else
            {
                pnlYeuCau.Controls.Add(ctl1);
            }
        }
        private void LoadCtl4()
        {
            LandSoftBuilding.Receivables.ctlCongNoTheoKy ctl1 = new LandSoftBuilding.Receivables.ctlCongNoTheoKy() { Dock = DockStyle.Fill, objnhanvien = objnhanvien };

            if (pnlCongNo.InvokeRequired)
            {
                BeginInvoke(new dlgAddItemN(LoadCtl4));
            }
            else
            {
                pnlCongNo.Controls.Add(ctl1);
            }
        }
    }
}
