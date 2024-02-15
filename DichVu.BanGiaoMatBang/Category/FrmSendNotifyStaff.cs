using System.Linq;

namespace DichVu.BanGiaoMatBang.Category
{
    public partial class FrmSendNotifyStaff : DevExpress.XtraEditors.XtraForm
    {
        public byte? BuidingId { get; set; }

        private Library.MasterDataContext _db = new Library.MasterDataContext();

        public FrmSendNotifyStaff()
        {
            InitializeComponent();
        }

        private void FrmSendNotifyStaff_Load(object sender, System.EventArgs e)
        {
            Library.TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Library.Common.User, barManager1);

            lkBuilding.DataSource = Library.Common.TowerList;
            itemBuilding.EditValue = Library.Common.User.MaTN;

            LoadData();
        }

        private void LoadData()
        {
            try
            {
                _db = new Library.MasterDataContext();
                glkStaff.DataSource = _db.tnNhanViens;
                var buildingId = (byte)itemBuilding.EditValue;
                gc.DataSource = _db.ho_SendNotifyStaffs.Where(_ => _.BuildingId == buildingId & _.LoaiPhanQuyenId == 1);
            }
            catch{}
        }

        private void ItemRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void ItemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                gv.PostEditor();
                _db.SubmitChanges();
                Library.DialogBox.Success();
            }
            catch
            {
                Library. DialogBox.Error("Không lưu được dữ liệu");
                return;
            }
        }

        private void ItemDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gv.DeleteSelectedRows();
        }

        private void gv_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            gv.AddNewRow();
            gv.SetFocusedRowCellValue("BuildingId", (byte)itemBuilding.EditValue);
            gv.SetFocusedRowCellValue("LoaiPhanQuyenId", 1);
            gv.SetFocusedRowCellValue("IsNotify", true);
        }
    }
}