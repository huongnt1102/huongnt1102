
namespace Building.PhanQuyenBieuDo.ControlSetting
{
    public partial class FrmView : DevExpress.XtraEditors.XtraForm
    {
        public string ControlName { get; set; }
        public string DllName { get; set; }
        private delegate void DlgAddItemN();

        public FrmView()
        {
            InitializeComponent();
        }

        private void FrmView_Load(object sender, System.EventArgs e)
        {
            var bw = new System.ComponentModel.BackgroundWorker();
            bw.DoWork += bw_DoWork;
            bw.RunWorkerAsync();
        }

        private async void bw_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            await System.Threading.Tasks.Task.Run(() => LoadControl());
        }

        private async void LoadControl()
        {
            panelControl1.Controls.Clear();

            var ctl = Building.PhanQuyenBieuDo.Class.View.GetControlForm(ControlName, DllName);
            if (ctl == null) return;
            ctl.Dock = System.Windows.Forms.DockStyle.Fill;
            if (panelControl1.InvokeRequired)
                await System.Threading.Tasks.Task.Run(() => { BeginInvoke(new DlgAddItemN(LoadControl)); });
            else panelControl1.Controls.Add(ctl);
        }
    }
}