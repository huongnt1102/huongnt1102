using System.Linq;

namespace HopDongThueNgoai.LichSuTienTrinh
{
    public partial class FrmLichSuThucHien : DevExpress.XtraEditors.XtraForm
    {
        public System.Collections.Generic.List<Library.PhanQuyen.ControlName> LControlName { get; set; }

        private Library.MasterDataContext _db;

        public FrmLichSuThucHien()
        {
            InitializeComponent();
        }

        private void FrmLichSuThucHien_Load(object sender, System.EventArgs e)
        {
            Library.TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Library.Common.User, barManager1);

            lkBuilding.DataSource = Library.Common.TowerList;
            itemBuilding.EditValue = Library.Common.User.MaTN;

            Library.KyBaoCao kbc = new Library.KyBaoCao();
            foreach (string item in kbc.Source) cbxKbc.Items.Add(item);
            itemKbc.EditValue = kbc.Source[3];

            SetDate(3);
            LoadData();
        }

        private void SetDate(int index)
        {
            var kbc = new Library.KyBaoCao() { Index = index };
            kbc.SetToDate();

            itemDateFrom.EditValue = kbc.DateFrom;
            itemDateTo.EditValue = kbc.DateTo;
        }

        private void LoadData()
        {
            _db = new Library.MasterDataContext();
            var tuNgay = (System.DateTime) itemDateFrom.EditValue;
            var denNgay = (System.DateTime) itemDateTo.EditValue;

            gc.DataSource = (from _ in _db.hdctnLichSuTienTrinhs
                join kh in _db.tnKhachHangs on _.KhachHangId equals kh.MaKH into khachHang
                from kh in khachHang.DefaultIfEmpty()
                where _.BuildingId == (byte?) itemBuilding.EditValue &
                      System.Data.Linq.SqlClient.SqlMethods.DateDiffDay(tuNgay, _.DateCreate) >= 0 &
                      System.Data.Linq.SqlClient.SqlMethods.DateDiffDay(_.DateCreate, denNgay) >= 0
                select new
                {
                    _.Id, _.HopDongId, _.KhachHangId, _.DateCreate, _.DienGiai, _.hdctnDanhSachHopDongThueNgoai.SoHopDong, _.hdctnDanhSachHopDongThueNgoai.NoiLamViecName, HoTenKh = kh.IsCaNhan == true ? kh.HoKH + " " + kh.TenKH : kh.CtyTen
                });
        }
        
        private void CreatePhanQuyen1()
        {
            if (LControlName == null) return;

            var lModuleControl = new System.Collections.Generic.List<Library.PhanQuyen.ModuleControl>
            {
                new Library.PhanQuyen.ModuleControl { ModuleName = itemRefresh.Caption, ModuleDescription = itemRefresh.Caption, ControlNames = itemRefresh.Name},
                new Library.PhanQuyen.ModuleControl { ModuleName = itemAdd.Caption, ModuleDescription = itemAdd.Caption, ControlNames = itemAdd.Name},
                new Library.PhanQuyen.ModuleControl { ModuleName = itemEdit.Caption, ModuleDescription = itemEdit.Caption, ControlNames = itemEdit.Name},
                new Library.PhanQuyen.ModuleControl { ModuleName = itemDelete.Caption, ModuleDescription = itemDelete.Caption, ControlNames = itemDelete.Name}
            };

            Library.PhanQuyen.CreatePhanQuyen(GetType().Namespace + "." + Name, Text, HopDongThueNgoai.Class.Const.MODULE_HOP_DONG_THUE_NGOAI_ID, HopDongThueNgoai.Class.Const.FORM_MAIN_ID, LControlName, lModuleControl);
        }

        private void CreatePhanQuyen()
        {
            if (LControlName == null) return;

            var lModuleControl = new System.Collections.Generic.List<Library.PhanQuyen.ModuleControl>();

            foreach (var item in barManager1.Items)
                if (item is DevExpress.XtraBars.BarButtonItem)
                {
                    var button = (DevExpress.XtraBars.BarButtonItem)item;
                    lModuleControl.Add(new Library.PhanQuyen.ModuleControl { ModuleName = button.Caption, ModuleDescription = button.Caption, ControlNames = button.Name });
                }

            Library.PhanQuyen.CreatePhanQuyen(GetType().Namespace + "." + Name, Text, HopDongThueNgoai.Class.Const.MODULE_HOP_DONG_THUE_NGOAI_ID, HopDongThueNgoai.Class.Const.FORM_MAIN_ID, LControlName, lModuleControl);
        }

        private void ItemRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void ItemEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var id = (int?) gv.GetFocusedRowCellValue("Id");
            if (id == null)
            {
                Library.DialogBox.Alert("Vui lòng chọn lịch sử");
                return;
            }

            using (var frm = new HopDongThueNgoai.LichSuTienTrinh.FrmLichSuThucHienEdit() {BuildingId = (byte?) itemBuilding.EditValue, TienTrinhId = id})
            {
                frm.ShowDialog();
                if (frm.DialogResult == System.Windows.Forms.DialogResult.OK) LoadData();
            }
        }

        private void ItemDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var indexs = gv.GetSelectedRows();
            if (indexs.Length == 0)
            {
                Library.DialogBox.Alert("Vui lòng chọn lịch sử thực hiện");
                return;
            }

            if (Library.DialogBox.QuestionDelete() == System.Windows.Forms.DialogResult.No) return;

            foreach (var i in indexs) _db.hdctnLichSuTienTrinhs.DeleteAllOnSubmit(_db.hdctnLichSuTienTrinhs.Where(_ => _.Id == (int)gv.GetRowCellValue(i, "Id")));

            _db.SubmitChanges();

            LoadData();
        }

        private void ItemAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var frm = new HopDongThueNgoai.LichSuTienTrinh.FrmLichSuThucHienEdit() {BuildingId = (byte?) itemBuilding.EditValue})
            {
                frm.ShowDialog();
                if (frm.DialogResult == System.Windows.Forms.DialogResult.OK) LoadData();
            }
        }

        private void cbxKbc_EditValueChanged(object sender, System.EventArgs e)
        {
            SetDate(((DevExpress.XtraEditors.ComboBoxEdit)sender).SelectedIndex);
        }

        private void itemTaoPhanQuyen_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            CreatePhanQuyen();
        }
    }
}