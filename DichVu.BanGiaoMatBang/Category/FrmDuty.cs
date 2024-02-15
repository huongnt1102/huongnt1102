using System;
using System.Linq;
using DevExpress.XtraEditors;
using Library;

namespace DichVu.BanGiaoMatBang.Category
{
    public partial class FrmDuty : XtraForm
    {
        private MasterDataContext _db = new MasterDataContext();
        public FrmDuty()
        {
            InitializeComponent();
        }

        private void FrmDuty_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);

            lkBuilding.DataSource = Common.TowerList;
            itemBuilding.EditValue = Common.User.MaTN;

            LoadData();
        }

        private void LoadData()
        {
            try
            {
                var buildingId = (byte) itemBuilding.EditValue;
                gc.DataSource = _db.ho_Duties.Where(_ => _.BuildingId == buildingId);
            }
            catch
            {
                //
            }
        }

        private void ItemRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void ItemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                _db.SubmitChanges();
                DialogBox.Success();
            }
            catch
            {
                DialogBox.Error("Không lưu được dữ liệu");
                return;
            }
        }

        private void gv_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            gv.AddNewRow();
            gv.SetFocusedRowCellValue("BuildingId", (byte)itemBuilding.EditValue);
        }
    }
}