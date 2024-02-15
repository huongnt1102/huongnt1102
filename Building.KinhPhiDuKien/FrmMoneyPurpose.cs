using System.Linq;

namespace Building.KinhPhiDuKien
{
    public partial class FrmMoneyPurpose : DevExpress.XtraEditors.XtraForm
    {
        private Library.MasterDataContext _db;

        public FrmMoneyPurpose()
        {
            InitializeComponent();
        }

        private void FrmMoneyPurpose_Load(object sender, System.EventArgs e)
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
                var buildingId = (byte?) itemBuilding.EditValue;
                gc.DataSource = _db.mp_MoneyPurposes.Where(_ => _.BuildingId == buildingId);
            }
            catch (System.Exception ex)
            {
                Library.DialogBox.Error(ex.ToString());
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
                gv.PostEditor();
                _db.SubmitChanges();
                Library.DialogBox.Success();
                LoadData();
            }
            catch (System.Exception ex)
            {
                Library.DialogBox.Error("Lưu dữ liệu lỗi: " + ex);
            }
        }

        private void ItemDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (Library.DialogBox.QuestionDelete() == System.Windows.Forms.DialogResult.No) return;
            try
            {
                _db.mp_MoneyPurposes.DeleteAllOnSubmit(_db.mp_MoneyPurposes.Where(_=>_.Id == (int)gv.GetFocusedRowCellValue("Id")));
                _db.SubmitChanges();
                gv.DeleteSelectedRows();
            }
            catch (System.Exception ex)
            {
                Library.DialogBox.Error("Không xóa được, vui lòng liên hệ kỹ thuật, lỗi: "+ex);
            }
        }

        private void Gv_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            var id = (int?) gv.GetFocusedRowCellValue("Id");
            if (id == null | id == 0) return;
            try
            {
                if (e.Column.FieldName == "MoneyPurpose")
                {
                    gv.SetFocusedRowCellValue("UserUpdateId", Library.Common.User.MaNV);
                    gv.SetFocusedRowCellValue("UserUpdateName", Library.Common.User.HoTenNV);
                    gv.SetFocusedRowCellValue("DateUpdate", System.DateTime.UtcNow.AddHours(7));
                }
            }
            catch
            {

            }
        }

        private void Gv_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            gv.AddNewRow();
            gv.SetFocusedRowCellValue("Name", " ");
            gv.SetFocusedRowCellValue("UserCreateId", Library.Common.User.MaNV);
            gv.SetFocusedRowCellValue("UserCreateName", Library.Common.User.HoTenNV);
            gv.SetFocusedRowCellValue("DateCreate", System.DateTime.UtcNow.AddHours(7));
            gv.SetFocusedRowCellValue("BuildingId", (byte) itemBuilding.EditValue);
            gv.SetFocusedRowCellValue("MoneyUsed", 0);
            gv.SetFocusedRowCellValue("MoneyExist", 0);
        }

        private void gv_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
                var size = e.Graphics.MeasureString(e.Info.DisplayText, e.Appearance.Font);
                var width = System.Convert.ToInt32(size.Width) + 20;
                BeginInvoke(new System.Windows.Forms.MethodInvoker(delegate { Cal(width, gv); }));
            }
        }

        private bool Cal(int width, DevExpress.XtraGrid.Views.Grid.GridView view)
        {
            view.IndicatorWidth = view.IndicatorWidth < width ? width : view.IndicatorWidth;
            return true;
        }

        private void spinMoney_EditValueChanged(object sender, System.EventArgs e)
        {
            var item = sender as DevExpress.XtraEditors.SpinEdit;
            if (item == null) return;

            var moneyUsed = (decimal?) gv.GetFocusedRowCellValue("MoneyUsed");
            gv.SetFocusedRowCellValue("MoneyExist", (decimal)item.EditValue - moneyUsed);
        }
    }
}