

namespace Building.PhanQuyenBieuDo.ControlSetting
{
    public partial class FrmViewUserControl : DevExpress.XtraEditors.XtraUserControl
    {
        private const string DllLoad = "Building.BieuDoMain.dll";

        private delegate void DlgAddItemN();

        public FrmViewUserControl()
        {
            InitializeComponent();
        }

        private void FrmViewUserControl_Load(object sender, System.EventArgs e)
        {
            var listName = Building.PhanQuyenBieuDo.Class.View.GetAllUserControlOnDllByDllName(DllLoad);
            gc.DataSource = listName;

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
            var controlName = gv.GetFocusedRowCellValue("ControlName");
            if (controlName == null) return;

            panelControl1.Controls.Clear();

            var ctl = Building.PhanQuyenBieuDo.Class.View.GetControlForm(controlName.ToString(), DllLoad);
            if (ctl == null) return;
            ctl.Dock = System.Windows.Forms.DockStyle.Fill;
            if (panelControl1.InvokeRequired)
                await System.Threading.Tasks.Task.Run(() => { BeginInvoke(new DlgAddItemN(LoadControl)); });
            else panelControl1.Controls.Add(ctl);
        }

        private void Gv_Click(object sender, System.EventArgs e)
        {
            LoadControl();
        }
    }
}
