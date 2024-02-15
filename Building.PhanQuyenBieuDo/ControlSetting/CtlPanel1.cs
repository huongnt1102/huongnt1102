using System.Linq;

namespace Building.PhanQuyenBieuDo.ControlSetting
{
    public partial class CtlPanel1 : DevExpress.XtraEditors.XtraUserControl
    {
        public int? SoBieuDo { get; set; }
        public int? NhomCaiDatId { get; set; }
        public bool? IsView { get; set; }

        private delegate void DlgAddItemN();

        private DevExpress.XtraEditors.PanelControl panelControl;
        private Library.MasterDataContext _db = new Library.MasterDataContext();

        public CtlPanel1()
        {
            InitializeComponent();
        }

        private void CrlPanel1_Load(object sender, System.EventArgs e)
        {
            var bw = new System.ComponentModel.BackgroundWorker();
            bw.DoWork += bw_DoWork;
            bw.RunWorkerAsync();
        }

        private async void bw_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            if (IsView == false)
                await System.Threading.Tasks.Task.Run(() => LoadControl());
            else
            {
                panelControl1.Controls.Clear();
                panelControl = panelControl1;
                
                await System.Threading.Tasks.Task.Run(() => LoadControl1());
            }
        }

        private async void LoadControl()
        {
            var ctl = new Building.PhanQuyenBieuDo.ControlSetting.FrmChooseBieuDo { Dock = System.Windows.Forms.DockStyle.Fill, NhomCaiDatId = NhomCaiDatId, SttManHinh = 1 };
            if (panelControl1.InvokeRequired)
                await System.Threading.Tasks.Task.Run(() => { BeginInvoke(new DlgAddItemN(LoadControl)); });
            else panelControl1.Controls.Add(ctl);
        }

        private async void LoadControl1()
        {
            // lấy về dc control, nhưng làm sao new cái control này hả trời
            // get control
            var caiDat = _db.pq_BieuDoMain_CaiDats.FirstOrDefault(_ => _.NhomId == NhomCaiDatId & _.SttManHinh == 1);
            if (caiDat == null) return;

            var control = _db.pq_BieuDoMain_Controls.FirstOrDefault(_ => _.Id == caiDat.ControlId);
            if (control == null) return;

            var controlForm = (DevExpress.XtraEditors.XtraUserControl)Building.PhanQuyenBieuDo.Class.View.GetControlForm(control.ControlName, control.DllName); //control.ControlName, control.DllName
            controlForm.Dock = System.Windows.Forms.DockStyle.Fill;
            if (panelControl.InvokeRequired)
                await System.Threading.Tasks.Task.Run(() => { BeginInvoke(new DlgAddItemN(LoadControl1)); });
            else panelControl.Controls.Add(controlForm);
        }
    }
}
