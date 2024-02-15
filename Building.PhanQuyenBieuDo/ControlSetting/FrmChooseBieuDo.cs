using System.Linq;

namespace Building.PhanQuyenBieuDo.ControlSetting
{
    public partial class FrmChooseBieuDo : DevExpress.XtraEditors.XtraUserControl
    {
        public int? SoBieuDo { get; set; }
        public int? NhomCaiDatId { get; set; }
        public int? SttManHinh { get; set; }

        private readonly Library.MasterDataContext _db = new Library.MasterDataContext();
        private Library.pq_BieuDoMain_CaiDat _caiDat;
        private System.Collections.Generic.List<DevExpress.XtraEditors.RadioGroup> radioGroups = new System.Collections.Generic.List<DevExpress.XtraEditors.RadioGroup>();

        public FrmChooseBieuDo()
        {
            InitializeComponent();
        }

        private async void FrmChooseBieuDo_Load(object sender, System.EventArgs e)
        {
            CreateItemInRadioGroup();

            await System.Threading.Tasks.Task.Run(() => { _caiDat = GetCaiDat(); });
            if (_caiDat == null)
            {
                _caiDat = new Library.pq_BieuDoMain_CaiDat();
                _db.pq_BieuDoMain_CaiDats.InsertOnSubmit(_caiDat);
            }

            if (_caiDat.ControlId != null)
            {
                for (var i = 0; i < radioGroup1.Properties.Items.Count; i++)
                {
                    if ((int)radioGroup1.Properties.Items[i].Value == (int)_caiDat.ControlId)
                    {
                        radioGroup1.SelectedIndex = i;
                        return;
                    }
                }
            }
        }

        #region Get Biểu đồ main control

        public class BieuDoMainControl
        {
            public int? Id { get; set; }
            public int? GroupId { get; set; }
            public string GroupName { get; set; }
            public string DllName { get; set; }
            public string ControlName { get; set; }
            public string ControlCaption { get; set; }
        }

        private System.Collections.Generic.List<BieuDoMainControl> GetBieuDoMainControls()
        {
            using (Library.MasterDataContext db = new Library.MasterDataContext())
            {
                return db.pq_BieuDoMain_Controls.Select(_ => new BieuDoMainControl { Id = _.Id, GroupName = _.GroupName, DllName = _.DllName, ControlName = _.ControlName, ControlCaption = _.GroupName + ": " + _.ControlCaption, GroupId = _.GroupId }).ToList();
            }
        }

        #endregion

        private async void CreateItemInRadioGroup()
        {
            using (Library.MasterDataContext db = new Library.MasterDataContext())
            {
                System.Collections.Generic.List<BieuDoMainControl> lControl = new System.Collections.Generic.List<BieuDoMainControl>();
                await System.Threading.Tasks.Task.Run(() => { lControl = GetBieuDoMainControls(); });

                radioGroup1.Properties.BeginUpdate();
                radioGroup1.Properties.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
                var list = (from l in lControl orderby l.ControlCaption select l).ToList();

                var group = (from l in list group new { l } by new { l.GroupName, l.GroupId } into g select new { g.Key.GroupName, g.Key.GroupId }).ToList();
                foreach (var item in group)
                {
                    var itemControl = list.Where(_ => _.GroupName == item.GroupName);
                    var color = GetRandomColor();
                    var nhom = db.pq_BieuDoMain_Nhoms.FirstOrDefault(_ => _.Id == item.GroupId);
                    if (nhom != null) color = System.Drawing.ColorTranslator.ToHtml(System.Drawing.Color.FromArgb((int)nhom.Color));

                    foreach (var control in itemControl) radioGroup1.Properties.Items.Add(new DevExpress.XtraEditors.Controls.RadioGroupItem(control.Id, "<color=" + color + ">" + control.ControlCaption + "</color>"));
                }

                radioGroup1.Properties.EndUpdate();
            }

        }

        private string GetRandomColor()
        {
            System.Random r = new System.Random();
            var color = string.Format("#{0:X6}", r.Next(0x1000000));
            return color.ToString();
        }

        private System.Drawing.Color GetColor()
        {
            System.Random r = new System.Random();
            return System.Drawing.Color.FromArgb((byte)r.Next(0, 255), (byte)r.Next(0, 255), (byte)r.Next(0, 255));
        }

        private Library.pq_BieuDoMain_CaiDat GetCaiDat()
        {
            return _db.pq_BieuDoMain_CaiDats.FirstOrDefault(_ => _.NhomId == NhomCaiDatId & _.SttManHinh == SttManHinh);
        }

        private bool KiemTra()
        {
            if (NhomCaiDatId == null)
            {
                Library.DialogBox.Error("Vui lòng chọn nhóm biểu đồ");
                return true;
            }

            if (radioGroup1.SelectedIndex < 0)
            {
                Library.DialogBox.Error("Vui lòng chọn biểu đồ cụ thể trong màn hình");
                return true;
            }

            //var chon = false;
            //foreach(var item in radioGroups)
            //{
            //    if (item.SelectedIndex > 0) chon = true;
            //}

            //if (chon == false)
            //{
            //    Library.DialogBox.Error("Vui lòng chọn biểu đồ cụ thể trong màn hình");
            //    return true;
            //}

            return false;
        }

        private void ItemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (KiemTra()) return;

                //object controlSelected = null;
                //foreach (var item in radioGroups) if (item.SelectedIndex > 0) controlSelected = item.Properties.Items[item.SelectedIndex].Value;

                var controlSelected = radioGroup1.Properties.Items[radioGroup1.SelectedIndex].Value;
                if (_caiDat == null) return;
                _caiDat.ControlId = (int?) controlSelected;
                _caiDat.NhomId = NhomCaiDatId;
                _caiDat.SttManHinh = SttManHinh;

                _db.SubmitChanges();
                
                ColorButton();
            }
            catch (System.Exception ex)
            {
                Library.DialogBox.Error(ex.Message);
            }
        }

        private void ColorButton()
        {
            itemSave.Caption = "Đã lưu";
            this.itemSave.ItemAppearance.Normal.BackColor = GetColor(); // System.Drawing.Color.FromArgb(128, 255, 255);
            this.itemSave.ItemAppearance.Normal.Options.UseBackColor = true;
        }

        private void itemViewBieuDo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (KiemTra()) return;

            //object controlSelected = null;
            //foreach (var item in radioGroups) if (item.SelectedIndex > 0) controlSelected = item.Properties.Items[item.SelectedIndex].Value;
            var controlSelected = radioGroup1.Properties.Items[radioGroup1.SelectedIndex].Value;

            var control = GetControl((int?) controlSelected);
            if (control == null)
            {
                Library.DialogBox.Alert("Biểu đồ không có trong hệ thống");
                return;
            }

            using (var frm = new Building.PhanQuyenBieuDo.ControlSetting.FrmView(){ ControlName = control.ControlName, DllName = control.DllName})
            {
                frm.ShowDialog();
            }

        }

        private Library.pq_BieuDoMain_Control GetControl(int? controlId)
        {
            return _db.pq_BieuDoMain_Controls.FirstOrDefault(_ => _.Id == controlId);
        }

        private void LoadControl(string controlName, string dllName)
        {
            panelControl1.Controls.Clear();

            var ctl = Building.PhanQuyenBieuDo.Class.View.GetControlForm(controlName, dllName);
            if (ctl == null) return;

            ctl.Dock = System.Windows.Forms.DockStyle.Fill;
            panelControl1.Controls.Add(ctl);
        }

        private void radioGroup1_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (NhomCaiDatId == null) return;
            if (radioGroup1.SelectedIndex < 0) return;

            var controlSelected = radioGroup1.Properties.Items[radioGroup1.SelectedIndex].Value;

            var control = GetControl((int?)controlSelected);
            if (control == null) return;
            LoadControl(control.ControlName, control.DllName);
        }
    }
}
