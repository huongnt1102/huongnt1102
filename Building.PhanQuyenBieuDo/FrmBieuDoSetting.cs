using System.Linq;

namespace Building.PhanQuyenBieuDo
{
    public partial class FrmBieuDoSetting : DevExpress.XtraEditors.XtraForm
    {
        public System.Collections.Generic.List<Library.PhanQuyen.ControlName> LControlName { get; set; }
        public int? ParentId { get; set; }

        private delegate void DlgAddItemN();

        private Library.MasterDataContext _db;
        private int? NhomCaiDatId { get; set; }

        private const string DaChonHienThi = "Đã chọn mặc định";
        private const string ChuaChonHienThi = "Chưa chọn mặc định";

        public FrmBieuDoSetting()
        {
            InitializeComponent();
        }

        private void FrmBieuDoSetting_Load(object sender, System.EventArgs e)
        {
            Library.TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Library.Common.User, barManager1);

            itemSoBieuDo.EditValue = 0;
            chkHienThi.ValueChecked = false;
            LoadData();

            var bw = new System.ComponentModel.BackgroundWorker();
            bw.DoWork += bw_DoWork;
            bw.RunWorkerAsync();

            LoadControl();
        }

        private async void bw_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            await System.Threading.Tasks.Task.Run(() => LoadControl());
        }

        private void LoadData()
        {
            _db = new Library.MasterDataContext();
              
            glkNhomCaiDat.DataSource = _db.pq_BieuDoMain_Nhoms;
        }

        private async void LoadControl()
        {
            panelControl1.Controls.Clear();

            var soBieuDo = System.Convert.ToInt32(itemSoBieuDo.EditValue);
            // biểu đồ từ 7 trở lên là biểu đồ thiết kế
            switch (soBieuDo)
            {
                case 1: LoadControlSetting(new Building.PhanQuyenBieuDo.ControlSetting.CtlPanel1 { Dock = System.Windows.Forms.DockStyle.Fill, SoBieuDo = soBieuDo, NhomCaiDatId = NhomCaiDatId, IsView = false }); break;
                case 2: LoadControlSetting(new Building.PhanQuyenBieuDo.ControlSetting.CtlPanel2 { Dock = System.Windows.Forms.DockStyle.Fill, SoBieuDo = soBieuDo, NhomCaiDatId = NhomCaiDatId, IsView = false }); break;
                case 3: LoadControlSetting(new Building.PhanQuyenBieuDo.ControlSetting.CtlPanel3 { Dock = System.Windows.Forms.DockStyle.Fill, SoBieuDo = soBieuDo, NhomCaiDatId = NhomCaiDatId, IsView = false }); break;
                case 4: LoadControlSetting(new Building.PhanQuyenBieuDo.ControlSetting.CtlPanel4 { Dock = System.Windows.Forms.DockStyle.Fill, SoBieuDo = soBieuDo, NhomCaiDatId = NhomCaiDatId, IsView = false }); break;
                case 5: LoadControlSetting(new Building.PhanQuyenBieuDo.ControlSetting.CtlPanel5 { Dock = System.Windows.Forms.DockStyle.Fill, SoBieuDo = soBieuDo, NhomCaiDatId = NhomCaiDatId, IsView = false }); break;
                case 6: LoadControlSetting(new Building.PhanQuyenBieuDo.ControlSetting.CtlPanel6 { Dock = System.Windows.Forms.DockStyle.Fill, SoBieuDo = soBieuDo, NhomCaiDatId = NhomCaiDatId, IsView = false }); break;
                case 7: LoadControlSetting(new Building.PhanQuyenBieuDo.ControlSetting.CtlPanel7 { Dock = System.Windows.Forms.DockStyle.Fill, SoBieuDo = soBieuDo, NhomCaiDatId = NhomCaiDatId, IsView = false }); break;
            }
        }

        private async void LoadControlSetting(DevExpress.XtraEditors.XtraUserControl ctl)
        {
            if (panelControl1.InvokeRequired)
                await System.Threading.Tasks.Task.Run(() => { BeginInvoke(new DlgAddItemN(LoadControl)); });
            else panelControl1.Controls.Add(ctl);
        }


        private void GlkNhomCaiDat_EditValueChanged(object sender, System.EventArgs e)
        {
            try
            {
                var item = sender as DevExpress.XtraEditors.GridLookUpEdit;
                if (item.EditValue == null) return;

                itemSoBieuDo.EditValue = int.Parse(item.Properties.View.GetFocusedRowCellValue("SoBieuDo").ToString());
                if (item.Properties.View.GetFocusedRowCellValue("IsHienThi") != null)
                {
                    itemHienThi.Checked = bool.Parse(item.Properties.View.GetFocusedRowCellValue("IsHienThi").ToString());
                    itemHienThi.Caption = itemHienThi.Checked ? DaChonHienThi : ChuaChonHienThi;
                }
                NhomCaiDatId = (int?) item.EditValue;
            }
            catch{}
        }

        private void ItemRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadControl();
        }

        private void ItemThemNhomCaiDat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var frm = new Building.PhanQuyenBieuDo.Group.FrmNhomCaiDat())
            {
                frm.ShowDialog();
                if (frm.DialogResult == System.Windows.Forms.DialogResult.OK) LoadData();
            }
        }

        private void itemHienThi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _db = new Library.MasterDataContext();

            itemHienThi.Caption = itemHienThi.Checked ? DaChonHienThi : ChuaChonHienThi;

            var nhom = GetNhom();
            if (nhom == null)
            {
                Library.DialogBox.Alert("Vui lòng chọn nhóm biểu đồ");
                itemHienThi.Caption = ChuaChonHienThi;
                itemHienThi.Checked = false;
                return;
            }

            var lNhom = _db.pq_BieuDoMain_Nhoms.ToList();
            lNhom.ForEach(_ => { _.IsHienThi = false; });

            nhom.IsHienThi = itemHienThi.Checked;
            _db.SubmitChanges();
        }

        private Library.pq_BieuDoMain_Nhom GetNhom()
        {
            return _db.pq_BieuDoMain_Nhoms.FirstOrDefault(_ => _.Id == NhomCaiDatId);
        }

        private void ItemViewBieuDo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadView();
        }

        private async void LoadView()
        {
            panelControl1.Controls.Clear();
            var ctl = new Building.PhanQuyenBieuDo.ControlSetting.FrmViewUserControl {Dock = System.Windows.Forms.DockStyle.Fill};
            if (panelControl1.InvokeRequired)
                await System.Threading.Tasks.Task.Run(() => { BeginInvoke(new DlgAddItemN(LoadView)); });
            else panelControl1.Controls.Add(ctl);
        }

        private void ItemPhanQuyenNhanVien_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using(var frm = new Building.PhanQuyenBieuDo.Group.FrmNhanVien())
            {
                frm.ShowDialog();
            }
        }

        private void itemNhomBieuDo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var frm = new Building.PhanQuyenBieuDo.Group.FrmNhomBieuDo()) frm.ShowDialog();
        }
    }
}