using System.Linq;

namespace Building.PhanQuyenBieuDo.ControlSetting
{
    public partial class CtlPanel4 : DevExpress.XtraEditors.XtraUserControl
    {
        public int? SoBieuDo { get; set; }
        public int? NhomCaiDatId { get; set; }
        public bool? IsView { get; set; }

        private delegate void DlgAddItemN();

        public CtlPanel4()
        {
            InitializeComponent();
        }

        private void CtlPanel4_Load(object sender, System.EventArgs e)
        {
            var bw = new System.ComponentModel.BackgroundWorker();
            bw.DoWork += bw_DoWork;
            bw.RunWorkerAsync();
        }

        private async void bw_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            switch (IsView)
            {
                case false:
                    await System.Threading.Tasks.Task.Run(() => LoadControlSetting1());
                    await System.Threading.Tasks.Task.Run(() => LoadControlSetting2());
                    await System.Threading.Tasks.Task.Run(() => LoadControlSetting3());
                    await System.Threading.Tasks.Task.Run(() => LoadControlSetting4());

                    break;
                default:
                    await System.Threading.Tasks.Task.Run(() => LoadControlView1());
                    await System.Threading.Tasks.Task.Run(() => LoadControlView2());
                    await System.Threading.Tasks.Task.Run(() => LoadControlView3());
                    await System.Threading.Tasks.Task.Run(() => LoadControlView4());
                    break;
            }
        }

        private Library.pq_BieuDoMain_CaiDat GetCaiDat(int? sttManHinh)
        {
            var db = new Library.MasterDataContext();
            return db.pq_BieuDoMain_CaiDats.FirstOrDefault(_ => _.NhomId == NhomCaiDatId & _.SttManHinh == sttManHinh);
        }

        private Library.pq_BieuDoMain_Control GetControl(int? controlId)
        {
            var db = new Library.MasterDataContext();
            return db.pq_BieuDoMain_Controls.FirstOrDefault(_ => _.Id == controlId);
        }

        #region LoadControlSetting
        private async void LoadControlSetting1()
        {
            var ctl = new Building.PhanQuyenBieuDo.ControlSetting.FrmChooseBieuDo { Dock = System.Windows.Forms.DockStyle.Fill, NhomCaiDatId = NhomCaiDatId, SttManHinh = 1 };
            if (panelControl1.InvokeRequired)
                await System.Threading.Tasks.Task.Run(() => { BeginInvoke(new DlgAddItemN(LoadControlSetting1)); });
            else panelControl1.Controls.Add(ctl);
        }

        private async void LoadControlSetting2()
        {
            var ctl = new Building.PhanQuyenBieuDo.ControlSetting.FrmChooseBieuDo { Dock = System.Windows.Forms.DockStyle.Fill, NhomCaiDatId = NhomCaiDatId, SttManHinh = 2 };
            if (panelControl2.InvokeRequired)
                await System.Threading.Tasks.Task.Run(() => { BeginInvoke(new DlgAddItemN(LoadControlSetting2)); });
            else panelControl2.Controls.Add(ctl);
        }

        private async void LoadControlSetting3()
        {
            var ctl = new Building.PhanQuyenBieuDo.ControlSetting.FrmChooseBieuDo { Dock = System.Windows.Forms.DockStyle.Fill, NhomCaiDatId = NhomCaiDatId, SttManHinh = 3 };
            if (panelControl3.InvokeRequired)
                await System.Threading.Tasks.Task.Run(() => { BeginInvoke(new DlgAddItemN(LoadControlSetting3)); });
            else panelControl3.Controls.Add(ctl);
        }

        private async void LoadControlSetting4()
        {
            var ctl = new Building.PhanQuyenBieuDo.ControlSetting.FrmChooseBieuDo { Dock = System.Windows.Forms.DockStyle.Fill, NhomCaiDatId = NhomCaiDatId, SttManHinh = 4 };
            if (panelControl4.InvokeRequired)
                await System.Threading.Tasks.Task.Run(() => { BeginInvoke(new DlgAddItemN(LoadControlSetting4)); });
            else panelControl4.Controls.Add(ctl);
        }

        #endregion

        #region loadControlView

        private async void LoadControlView1()
        {
            var caiDat = GetCaiDat(1);
            if (caiDat == null) return;
            var control = GetControl(caiDat.ControlId);
            if (control == null) return;

            var controlForm = (DevExpress.XtraEditors.XtraUserControl)Building.PhanQuyenBieuDo.Class.View.GetControlForm(control.ControlName, control.DllName); //control.ControlName, 
            if (controlForm == null) return;
            controlForm.Dock = System.Windows.Forms.DockStyle.Fill;
            if (panelControl1.InvokeRequired)
                await System.Threading.Tasks.Task.Run(() => { BeginInvoke(new DlgAddItemN(LoadControlView1)); });
            else panelControl1.Controls.Add(controlForm);
        }

        private async void LoadControlView2()
        {
            var caiDat = GetCaiDat(2);
            if (caiDat == null) return;
            var control = GetControl(caiDat.ControlId);
            if (control == null) return;

            var controlForm = (DevExpress.XtraEditors.XtraUserControl)Building.PhanQuyenBieuDo.Class.View.GetControlForm(control.ControlName, control.DllName); //control.ControlName, 
            if (controlForm == null) return;
            controlForm.Dock = System.Windows.Forms.DockStyle.Fill;
            if (panelControl2.InvokeRequired)
                await System.Threading.Tasks.Task.Run(() => { BeginInvoke(new DlgAddItemN(LoadControlView2)); });
            else panelControl2.Controls.Add(controlForm);
        }

        private async void LoadControlView3()
        {
            var caiDat = GetCaiDat(3);
            if (caiDat == null) return;
            var control = GetControl(caiDat.ControlId);
            if (control == null) return;

            var controlForm = (DevExpress.XtraEditors.XtraUserControl)Building.PhanQuyenBieuDo.Class.View.GetControlForm(control.ControlName, control.DllName); //control.ControlName, 
            if (controlForm == null) return;
            controlForm.Dock = System.Windows.Forms.DockStyle.Fill;
            if (panelControl3.InvokeRequired)
                await System.Threading.Tasks.Task.Run(() => { BeginInvoke(new DlgAddItemN(LoadControlView3)); });
            else panelControl3.Controls.Add(controlForm);
        }

        private async void LoadControlView4()
        {
            var caiDat = GetCaiDat(4);
            if (caiDat == null) return;
            var control = GetControl(caiDat.ControlId);
            if (control == null) return;

            var controlForm = (DevExpress.XtraEditors.XtraUserControl)Building.PhanQuyenBieuDo.Class.View.GetControlForm(control.ControlName, control.DllName); //control.ControlName, 
            if (controlForm == null) return;
            controlForm.Dock = System.Windows.Forms.DockStyle.Fill;
            if (panelControl4.InvokeRequired)
                await System.Threading.Tasks.Task.Run(() => { BeginInvoke(new DlgAddItemN(LoadControlView4)); });
            else panelControl4.Controls.Add(controlForm);
        }

        #endregion
        
    }
}
