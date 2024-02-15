using System;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Library;

namespace Building.SMS.Category
{
    public partial class FrmBrandName : XtraForm
    {
        private MasterDataContext _db;
        public FrmBrandName()
        {
            InitializeComponent();
        }

        private void FrmBrandName_Load(object sender, EventArgs e)
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
                _db = new MasterDataContext();
                gc.DataSource = _db.SmsBrandNames.Where(_ => _.BuildingId == ((byte?)itemBuilding.EditValue ?? Common.User.MaTN));
            }
            catch{}
        }

        private void itemRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void itemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                _db.SubmitChanges();
                DialogBox.Success();
                LoadData();
            }
            catch
            {
                DialogBox.Error("Lưu dữ liệu lỗi");
            }
        }

        private void itemDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DialogBox.QuestionDelete() == DialogResult.No) return;
            try
            {
                gv.DeleteSelectedRows();
            }
            catch
            {
                DialogBox.Alert("Không xóa được, vui lòng liên hệ bộ phận kỹ thuật");
            }
        }

        private void gv_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            gv.AddNewRow();
            gv.SetFocusedRowCellValue("BuildingId", (byte?)itemBuilding.EditValue ?? Common.User.MaTN);
        }
    }
}