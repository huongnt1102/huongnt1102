using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Library;

namespace DichVu.BanGiaoMatBang.Customer
{
    public partial class FrmScheduleApartmentAssetEdit : XtraForm
    {
        public int? ScheduleApartmentId { get; set; }
        public int? ScheduleApartmentAssetId { get; set; }
        public byte? BuildingId { get; set; }

        private MasterDataContext _db = new MasterDataContext();
        private string _unitName, _statusName, _assetGroupName,_assetCategoryName, _assetCategoryNo;
        private ho_ScheduleApartmentAsset _scheduleApartmentAsset;
        private ho_ScheduleApartment _scheduleApartment;
        private System.Collections.Generic.List<Library.Class.HuongDan.ShowAuto.ControlItem> controls;

        public FrmScheduleApartmentAssetEdit()
        {
            InitializeComponent();
        }

        private void FrmScheduleApartmentAssetEdit_Load(object sender, System.EventArgs e)
        {
            //controls = Library.Class.HuongDan.ShowAuto.GetControlItemsAutoSave(this.Controls);
            controls = Library.Class.HuongDan.ShowAuto.GetControlItemsAutoTag(this.Controls);
            Library.Class.HuongDan.ShowAuto.ClearText(this.Controls);
            itemClearText.ItemClick += ItemClearText_ItemClick;
            itemHuongDan.ItemClick += ItemHuongDan_ItemClick;

            if (ScheduleApartmentId == null) return;
            _scheduleApartment = _db.ho_ScheduleApartments.FirstOrDefault(_=>_.Id == ScheduleApartmentId);
            if (_scheduleApartment == null) return;

            txtDescription.Text = "";
            txtSystemNumber.Text = "";
            _unitName = "";
            _statusName = "";
            _assetGroupName = "";
            _assetCategoryName = "";
            _assetCategoryNo = "";
            glkAssetGroup.Properties.DataSource = _db.ho_AssetGroups;
            glkStatus.Properties.DataSource = _db.tbl_TinhTrangTaiSans;
            glkUnit.Properties.DataSource = _db.DonViTinhs;

            _scheduleApartmentAsset = new ho_ScheduleApartmentAsset();
            _scheduleApartmentAsset.BuildingId = BuildingId;
            _scheduleApartmentAsset.PlanId = _scheduleApartment.PlanId;
            _scheduleApartmentAsset.PlanName = _scheduleApartment.PlanName;
            _scheduleApartmentAsset.ScheduleId = _scheduleApartment.ScheduleId;
            _scheduleApartmentAsset.ScheduleName = _scheduleApartment.ScheduleName;
            _scheduleApartmentAsset.ScheduleApartmentId = ScheduleApartmentId;
            _scheduleApartmentAsset.ApartmentId = _scheduleApartment.ApartmentId;
            _scheduleApartmentAsset.ApartmentName = _scheduleApartment.ApartmentName;

            if (ScheduleApartmentAssetId != null)
            {
                _scheduleApartmentAsset = _db.ho_ScheduleApartmentAssets.FirstOrDefault(_ => _.Id == ScheduleApartmentAssetId);
                if (_scheduleApartmentAsset != null)
                {
                    glkAssetGroup.EditValue = _scheduleApartmentAsset.AssetGroupId;
                    _assetGroupName = _scheduleApartmentAsset.AssetGroupName;

                    glkAssetCategory.EditValue = _scheduleApartmentAsset.AssetCategoryId;
                    _assetCategoryName = _scheduleApartmentAsset.AssetCategoryName;
                    _assetCategoryNo = _scheduleApartmentAsset.AssetCategoryNo;

                    glkUnit.EditValue = _scheduleApartmentAsset.UnitId;
                    _unitName = _scheduleApartmentAsset.UnitName;

                    glkStatus.EditValue = _scheduleApartmentAsset.StatusId;
                    _statusName = _scheduleApartmentAsset.StatusName;

                    txtDescription.Text = _scheduleApartmentAsset.Description;
                    txtSystemNumber.Text = _scheduleApartmentAsset.SystemNumber;

                    if (_scheduleApartmentAsset.InventoryNumber != null) spinInventoryNumber.Value = (decimal) _scheduleApartmentAsset.InventoryNumber;
                }
            }
        }

        private void ItemHuongDan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Library.Class.HuongDan.ShowAuto.ActiveDemo(true, controls);
        }

        private void ItemClearText_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Library.Class.HuongDan.ShowAuto.ClearText(this.Controls);
        }

        private void GlkAssetGroup_EditValueChanged(object sender, System.EventArgs e)
        {
            var item = sender as GridLookUpEdit;
            if (item.EditValue == null) return;
            try
            {
                glkAssetCategory.Properties.DataSource = _db.ho_AssetCategories.Where(_ =>
                    _.BuildingId == BuildingId & _.AssetGroupId == (int) item.EditValue & _.IsNotUse == false);
                _assetGroupName = item.Properties.View.GetFocusedRowCellValue("Name").ToString();
            }
            catch{}
        }

        private void GlkAssetCategory_EditValueChanged(object sender, System.EventArgs e)
        {
            var item = sender as GridLookUpEdit;
            if (item.EditValue == null) return;

            try
            {
                txtDescription.Text = item.Properties.View.GetFocusedRowCellValue("Description").ToString();
                txtSystemNumber.Text = item.Properties.View.GetFocusedRowCellValue("SystemNumber").ToString();

                glkUnit.EditValue = (int) item.Properties.View.GetFocusedRowCellValue("UnitId");
                _unitName = item.Properties.View.GetFocusedRowCellValue("UnitName").ToString();
                _assetCategoryName = item.Properties.View.GetFocusedRowCellValue("Name").ToString();
                _assetCategoryNo = item.Properties.View.GetFocusedRowCellValue("No").ToString();
            }
            catch{}
        }

        private void GlkUnit_EditValueChanged(object sender, System.EventArgs e)
        {
            var item = sender as GridLookUpEdit;
            if (item.EditValue == null) return;
            try
            {
                _unitName = item.Properties.View.GetFocusedRowCellValue("TenDVT").ToString();
            }
            catch{}
        }

        private void GlkStatus_EditValueChanged(object sender, System.EventArgs e)
        {
            var item = sender as GridLookUpEdit;
            if (item.EditValue == null) return;

            try{_statusName = item.Properties.View.GetFocusedRowCellValue("TenTinhTrang").ToString();}catch{}
        }

        private void ItemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                #region Kiểm tra

                if (glkAssetGroup.EditValue == null)
                {
                    DialogBox.Error("Vui lòng chọn nhóm tài sản");
                    return;
                }

                if (glkAssetCategory.EditValue == null)
                {
                    DialogBox.Error("Vui lòng chọn tài sản");
                    return;
                }

                if (glkStatus.EditValue == null)
                {
                    DialogBox.Error("Vui lòng chọn tình trạng tài sản");
                    return;
                }

                if (glkUnit.EditValue == null)
                {
                    DialogBox.Error("Vui lòng chọn đơn vị tính");
                    return;
                }
                #endregion

                if (ScheduleApartmentAssetId == null)
                {
                    _db.ho_ScheduleApartmentAssets.InsertOnSubmit(_scheduleApartmentAsset);
                }

                _scheduleApartmentAsset.AssetCategoryId = (int?) glkAssetCategory.EditValue;
                _scheduleApartmentAsset.AssetCategoryName = _assetCategoryName;
                _scheduleApartmentAsset.AssetCategoryNo = _assetCategoryNo;
                _scheduleApartmentAsset.AssetGroupId = (int?) glkAssetGroup.EditValue;
                _scheduleApartmentAsset.AssetGroupName = _assetGroupName;
                _scheduleApartmentAsset.Description = txtDescription.Text;
                _scheduleApartmentAsset.InventoryNumber = (decimal?) spinInventoryNumber.Value;
                _scheduleApartmentAsset.StatusId = (int?) glkStatus.EditValue;
                _scheduleApartmentAsset.StatusName = _statusName;
                _scheduleApartmentAsset.SystemNumber = txtSystemNumber.Text;
                _scheduleApartmentAsset.UnitId = (int?) glkUnit.EditValue;
                _scheduleApartmentAsset.UnitName = _unitName;
                
                _db.SubmitChanges();

                DialogBox.Success();
                DialogResult = DialogResult.OK;
                Close();
            }
            catch
            {
                DialogBox.Error("Không lưu được tài sản, vui lòng thử lại");
            }
        }

        private void ItemCancel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}