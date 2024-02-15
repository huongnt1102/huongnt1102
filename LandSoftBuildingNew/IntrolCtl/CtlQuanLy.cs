using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace LandSoftBuildingMain.IntrolCtl
{
    public partial class CtlQuanLy : XtraUserControl
    {
        private delegate void DlgAddItemN();
        public CtlQuanLy()
        {
            InitializeComponent();
        }

        private void CtlQuanLy_Load(object sender, EventArgs e)
        {
            var bw = new BackgroundWorker();
            bw.DoWork += bw_DoWork;
            bw.RunWorkerAsync();
        }

        void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            LoadControlSoLuongYeuCauHoanThanh();
            LoadControlCongNoTheoKy();
            LoadControlSoLuongLuongYeuCau();
            LoadControlTyLeDangTin();
            LoadControlThongKeSuDungApp();
            LoadControlDanhGiaSao();
        }

        private void LoadControlSoLuongYeuCauHoanThanh()
        {
            //var ctl1 = new CtlSoLuongYeuCauHoanThanh {Dock = DockStyle.Fill};
            //if (panelControlSoLuongYeuCauHoanThanh.InvokeRequired)
            //    await Task.Run(() => { BeginInvoke(new DlgAddItemN(LoadControlSoLuongYeuCauHoanThanh)); });
            //else panelControlSoLuongYeuCauHoanThanh.Controls.Add(ctl1);
        }

        private void LoadControlCongNoTheoKy()
        {
            //var ctl2 = new CtlTinhHinhCongNo {Dock = DockStyle.Fill};
            //if (panelControlCongNoTheoKy.InvokeRequired)
            //    await Task.Run(() => { BeginInvoke(new DlgAddItemN(LoadControlCongNoTheoKy)); });
            //else panelControlCongNoTheoKy.Controls.Add(ctl2);
        }

        private  void LoadControlSoLuongLuongYeuCau()
        {
            //var ctl3 = new CtlSoLuongYeuCau {Dock = DockStyle.Fill};
            //if (panelControlSoLuongYeuCau.InvokeRequired)
            //    await Task.Run(() => { BeginInvoke(new DlgAddItemN(LoadControlSoLuongLuongYeuCau)); });
            //else panelControlSoLuongYeuCau.Controls.Add(ctl3);
        }

        private void LoadControlTyLeDangTin()
        {
            //var ctl4 = new CtlTyLeDangTin {Dock = DockStyle.Fill};
            //if (panelControlTyLeDangTin.InvokeRequired)
            //    await Task.Run(() => { BeginInvoke(new DlgAddItemN(LoadControlTyLeDangTin)); });
            //else panelControlTyLeDangTin.Controls.Add(ctl4);
        }

        private void LoadControlThongKeSuDungApp()
        {
            //var ctl5 = new CtlThongKeSuDungApp {Dock = DockStyle.Fill};
            //if (panelControlThongKeSuDungApp.InvokeRequired)
            //    await Task.Run(() => { BeginInvoke(new DlgAddItemN(LoadControlThongKeSuDungApp)); });
            //else panelControlThongKeSuDungApp.Controls.Add(ctl5);
        }

        private void LoadControlDanhGiaSao()
        {
            //var ctl6 = new CtlDanhGiaSao {Dock = DockStyle.Fill};
            //if (panelControlDanhGiaSao.InvokeRequired)
            //    await Task.Run(() => { BeginInvoke(new DlgAddItemN(LoadControlDanhGiaSao)); });
            //else panelControlDanhGiaSao.Controls.Add(ctl6);
        }

        private void CtlQuanLy_SizeChanged(object sender, EventArgs e)
        {
            //splitContainer1.SplitterPosition = splitContainerControl2.Width / 2;
        }
    }
}
