using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace LandSoftBuildingMain.IntrolCtl
{
    public partial class CtlKyThuat : XtraUserControl
    {
        private delegate void DlgAddItemN();
        public CtlKyThuat()
        {
            InitializeComponent();
        }

        private void CtlKyThuat_Load(object sender, EventArgs e)
        {
            var bw = new BackgroundWorker();
            bw.DoWork += Bw_DoWork;
            bw.RunWorkerAsync();
        }

        private void Bw_DoWork(object sender, DoWorkEventArgs e)
        {
            LoadControlTinhHinhXuLyYeuCau();
            LoadControlSoLuongYeuCau();
            LoadControlThongKeSuDungApp();
            LoadControlTyLeDangTin();
        }
        private void LoadControlTinhHinhXuLyYeuCau()
        {
            //var ctl1 = new CtlTinhHinhXuLyYeuCau { Dock = DockStyle.Fill };
            //if (panelControl1.InvokeRequired)
            //    await Task.Run(() => { BeginInvoke(new DlgAddItemN(LoadControlTinhHinhXuLyYeuCau)); });
            //else panelControl1.Controls.Add(ctl1);
        }

        private void LoadControlSoLuongYeuCau()
        {
            //var ctl1 = new CtlSoLuongYeuCau { Dock = DockStyle.Fill };
            //if (panelControl2.InvokeRequired)
            //    await Task.Run(() => { BeginInvoke(new DlgAddItemN(LoadControlSoLuongYeuCau)); });
            //else panelControl2.Controls.Add(ctl1);
        }

        private void LoadControlThongKeSuDungApp()
        {
            //var ctl = new CtlThongKeSuDungApp {Dock = DockStyle.Fill};
            //if (panelControl3.InvokeRequired)
            //    await Task.Run(() => { BeginInvoke(new DlgAddItemN(LoadControlThongKeSuDungApp)); });
            //else panelControl3.Controls.Add(ctl);
        }

        private void LoadControlTyLeDangTin()
        {
            //var ctl = new CtlTyLeDangTin {Dock = DockStyle.Fill};
            //if (panelControl4.InvokeRequired)
            //    await Task.Run(() => { BeginInvoke(new DlgAddItemN(LoadControlTyLeDangTin)); });
            //else panelControl4.Controls.Add(ctl);
        }
    }
}
