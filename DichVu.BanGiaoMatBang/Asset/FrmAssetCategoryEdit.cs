using System;
using System.Data.Linq.SqlClient;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Library;

namespace DichVu.BanGiaoMatBang.Asset
{
    public partial class FrmAssetCategoryEdit : XtraForm
    {
        public byte? BuildingId { get; set; }
        public int? AssetCategoryId { get; set; }

        private MasterDataContext _db = new MasterDataContext();
        private ho_AssetCategory _assetCategory;
        private string _assetGroupName;
        private string _unitName;
        private System.Collections.Generic.List<Library.Class.HuongDan.ShowAuto.ControlItem> controls;

        public FrmAssetCategoryEdit()
        {
            InitializeComponent();
        }

        private void FrmAssetCategoryEdit_Load(object sender, EventArgs e)
        {
            //controls = Library.Class.HuongDan.ShowAuto.GetControlItemsAutoSave(this.Controls);
            controls = Library.Class.HuongDan.ShowAuto.GetControlItemsAutoTag(this.Controls);
            Library.Class.HuongDan.ShowAuto.ClearText(this.Controls);

            txtNo.Text = CreateNo();
            txtName.Text = "";
            _assetGroupName = "";
            _unitName = "";
            chkIsNotUse.Checked = false;
            glkAssetGroup.Properties.DataSource = _db.ho_AssetGroups;
            glkUnit.Properties.DataSource = _db.DonViTinhs;
            _assetCategory = new ho_AssetCategory();
            _assetCategory.BuildingId = BuildingId;

            if (AssetCategoryId != null)
            {
                _assetCategory = _db.ho_AssetCategories.FirstOrDefault(_ => _.Id == AssetCategoryId);
                if (_assetCategory != null)
                {
                    txtNo.Text = _assetCategory.No;
                    txtName.Text = _assetCategory.Name;
                    glkAssetGroup.EditValue = _assetCategory.AssetGroupId;
                    glkUnit.EditValue = _assetCategory.UnitId;
                    _assetGroupName = _assetCategory.AssetGroupName;
                    _unitName = _assetCategory.UnitName;
                    if (_assetCategory.HistoricalCost != null) spinHistoricalCost.Value = (decimal) _assetCategory.HistoricalCost;
                    txtDescription.Text = _assetCategory.Description;
                    txtSystemNumber.Text = _assetCategory.SystemNumber;
                    if (_assetCategory.IsNotUse != null) chkIsNotUse.Checked = (bool) _assetCategory.IsNotUse;
                }
            }

            itemClearText.ItemClick += ItemClearText_ItemClick;
            itemHuongDan.ItemClick += ItemHuongDan_ItemClick;
        }

        private void ItemHuongDan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Library.Class.HuongDan.ShowAuto.ActiveDemo(true, controls);
        }

        private void ItemClearText_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var no = txtNo.Text;
            Library.Class.HuongDan.ShowAuto.ClearText(this.Controls);
            txtNo.Text = no;
        }

        public string CreateNo()
        {
            var no = "";
            try
            {
                var assetCategory = _db.ho_AssetCategories
                    .Where(_ => _.BuildingId == BuildingId &&
                                SqlMethods.Like(_.No.Substring(_.No.IndexOf('-') + 1, 4), "[0-9][0-9][0-9][0-9]"))
                    .OrderByDescending(_ => Convert.ToInt32(_.No.Substring(_.No.IndexOf('-') + 1, 4)))
                    .Select(_ => new { STT = int.Parse(_.No.Substring(_.No.IndexOf('-') + 1)) })
                    .FirstOrDefault();

                var stt = assetCategory == null ? "0001" : (assetCategory.STT + 1).ToString().PadLeft(4, '0');
                no = "TS-" + stt;
                return no;
            }
            catch
            {
                return no;
            }
        }

        private void ItemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (AssetCategoryId == null)
                {
                    _db.ho_AssetCategories.InsertOnSubmit(_assetCategory);
                    _assetCategory.UserCreate = Common.User.MaNV;
                    _assetCategory.UserCreateName = Common.User.HoTenNV;
                    _assetCategory.DateCreate = DateTime.UtcNow.AddHours(7);
                }
                else
                {
                    _assetCategory.UserUpdate = Common.User.MaNV;
                    _assetCategory.UserUpdateName = Common.User.HoTenNV;
                    _assetCategory.DateUpdate = DateTime.UtcNow.AddHours(7);
                }

                if (glkAssetGroup.EditValue == null)
                {
                    DialogBox.Error("Vui lòng chọn nhóm tài sản");
                    return;
                }

                if (glkUnit.EditValue == null)
                {
                    DialogBox.Error("Vui lòng chọn đơn vị tính");
                    return;
                }

                _assetCategory.AssetGroupId = (int?) glkAssetGroup.EditValue;
                _assetCategory.AssetGroupName = _assetGroupName;
                _assetCategory.Description = txtDescription.Text;
                _assetCategory.HistoricalCost = spinHistoricalCost.Value;
                _assetCategory.IsNotUse = chkIsNotUse.Checked;
                _assetCategory.Name = txtName.Text;
                _assetCategory.No = txtNo.Text;
                _assetCategory.SystemNumber = txtSystemNumber.Text;
                _assetCategory.UnitId = (int?) glkUnit.EditValue;
                _assetCategory.UnitName = _unitName;

                _db.SubmitChanges();
                DialogBox.Success();
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                DialogBox.Error("Không lưu được, vui lòng kiểm tra lại.");
            }
        }

        private void ItemCancel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void GlkAssetGroup_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                var item = sender as GridLookUpEdit;
                if (item.EditValue == null)
                {
                    return;
                }

                _assetGroupName = item.Properties.View.GetFocusedRowCellValue("Name").ToString();
            }
            catch { }
        }

        private void GlkUnit_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                var item = sender as GridLookUpEdit;
                if (item.EditValue == null)
                {
                    return;
                }

                _unitName = item.Properties.View.GetFocusedRowCellValue("TenDVT").ToString();
            }
            catch { }
        }
    }
}