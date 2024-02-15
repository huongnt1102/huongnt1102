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
    public partial class newIntroCtl : DevExpress.XtraEditors.XtraUserControl
    {
        Thread th1;
        Thread th2;
        Thread th3;
        public tnNhanVien objnhanvien;
        private delegate void dlgAddItemN();

        public newIntroCtl()
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
        }
        
        private void LoadCtl2()
        {
            Library.NoticeCtl.NhacViecCtl ctl1 = new Library.NoticeCtl.NhacViecCtl() { Dock = DockStyle.Fill, objnhanvien = objnhanvien };

            if (panelControl2.InvokeRequired)
            {
                BeginInvoke(new dlgAddItemN(LoadCtl2));
            }
            else
            {
                panelControl2.Controls.Add(ctl1);
            }
        }

        private void LoadCtl3()
        {
            Library.NoticeCtl.YeuCauCtl ctl1 = new Library.NoticeCtl.YeuCauCtl() { Dock = DockStyle.Fill, objnhanvien = objnhanvien };

            if (panelControl3.InvokeRequired)
            {
                BeginInvoke(new dlgAddItemN(LoadCtl3));
            }
            else
            {
                panelControl3.Controls.Add(ctl1);
            }
        }
    }
}
