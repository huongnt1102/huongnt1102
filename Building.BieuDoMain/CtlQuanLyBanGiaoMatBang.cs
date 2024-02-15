using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Building.BieuDoMain
{
    public partial class CtlQuanLyBanGiaoMatBang : XtraUserControl
    {
        private delegate void DlgAddItemN();
        public CtlQuanLyBanGiaoMatBang()
        {
            InitializeComponent();
        }

        private void CtlQuanLy_Load(object sender, EventArgs e)
        {
            var bw = new BackgroundWorker();
            bw.DoWork += bw_DoWork;
            bw.RunWorkerAsync();
        }

        async void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            await Task.Run(() => LoadControl1());
            await Task.Run(() => { LoadControl2(); });
            await Task.Run(() => { LoadControl3(); });
            await Task.Run(() => { LoadControl4(); });
            await Task.Run(() => { LoadControl5(); });
            await Task.Run(() => { LoadControl6(); });
        }

        private async void LoadControl1()
        {
            var ctl1 = new CtlSoCanBanGiaoNoiBo {Dock = DockStyle.Fill};
            if (panel1.InvokeRequired)
                await Task.Run(() => { BeginInvoke(new DlgAddItemN(LoadControl1)); });
            else panel1.Controls.Add(ctl1);
        }

        private async void LoadControl2()
        {
            var ctl2 = new CtlTinhHinhCongNo { Dock = DockStyle.Fill };
            if (panel3.InvokeRequired)
                await Task.Run(() => { BeginInvoke(new DlgAddItemN(LoadControl2)); });
            else panel3.Controls.Add(ctl2);
        }

        private async void LoadControl3()
        {
            var ctl3 = new CtlSoCanBanGiaoKhachHang {Dock = DockStyle.Fill};
            if (panel2.InvokeRequired)
                await Task.Run(() => { BeginInvoke(new DlgAddItemN(LoadControl3)); });
            else panel2.Controls.Add(ctl3);
        }

        private async void LoadControl4()
        {
            var ctl4 = new CtlSoCanBanGiaoKhachHangTong { Dock = DockStyle.Fill };
            if (panel4.InvokeRequired)
                await Task.Run(() => { BeginInvoke(new DlgAddItemN(LoadControl4)); });
            else panel4.Controls.Add(ctl4);
        }

        private async void LoadControl5()
        {
            //var ctl5 = new CtlThongKeSuDungApp {Dock = DockStyle.Fill};
            var ctl5 = new CtlSoCanBanGiaoNoiBoTong { Dock = DockStyle.Fill };
            if (panel5.InvokeRequired)
                await Task.Run(() => { BeginInvoke(new DlgAddItemN(LoadControl5)); });
            else panel5.Controls.Add(ctl5);
        }

        private async void LoadControl6()
        {
            //var ctl6 = new CtlDanhGiaSao {Dock = DockStyle.Fill};
            var ctl6 = new CtlSoLuongYeuCau { Dock = DockStyle.Fill };
            if (panel6.InvokeRequired)
                await Task.Run(() => { BeginInvoke(new DlgAddItemN(LoadControl6)); });
            else panel6.Controls.Add(ctl6);
        }

        private void CtlQuanLy_SizeChanged(object sender, EventArgs e)
        {
            //splitContainer1.SplitterPosition = splitContainerControl2.Width / 2;
        }
    }
}
