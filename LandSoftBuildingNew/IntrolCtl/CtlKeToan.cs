using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace LandSoftBuildingMain.IntrolCtl
{
    public partial class CtlKeToan : DevExpress.XtraEditors.XtraUserControl
    {
        private delegate void DlgAddItemN();
        public CtlKeToan()
        {
            InitializeComponent();
        }

        private void CtlKeToan_Load(object sender, EventArgs e)
        {
            var bw = new BackgroundWorker();
            bw.DoWork += Bw_DoWork;
            bw.RunWorkerAsync();
        }

        private void Bw_DoWork(object sender, DoWorkEventArgs e)
        {
            LoadControlCongNoTheoKy();
            LoadControlTinhHinhCongNoPhatSinh();
        }
        private void LoadControlCongNoTheoKy()
        {
            //var ctl2 = new CtlTinhHinhCongNo { Dock = DockStyle.Fill };
            //if (panelControl1.InvokeRequired)
            //    await Task.Run(() => { BeginInvoke(new DlgAddItemN(LoadControlCongNoTheoKy)); });
            //else panelControl1.Controls.Add(ctl2);
        }

        private void LoadControlTinhHinhCongNoPhatSinh()
        {
            //var ctl = new CtlTinhHinhCongNoPhatSinh {Dock = DockStyle.Fill};
            //if (panelControl2.InvokeRequired)
            //    await Task.Run(() => { BeginInvoke(new DlgAddItemN(LoadControlTinhHinhCongNoPhatSinh)); });
            //else panelControl2.Controls.Add(ctl);
        }
    }
}
