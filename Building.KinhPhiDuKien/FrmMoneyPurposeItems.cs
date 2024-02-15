using System.Linq;

namespace Building.KinhPhiDuKien
{
    public partial class FrmMoneyPurposeItems : DevExpress.XtraEditors.XtraForm
    {
        private Library.MasterDataContext _db;
        private Library.mp_MoneyPurpose _mp;
        private int _year;
        private byte? _buildingId;

        public FrmMoneyPurposeItems()
        {
            InitializeComponent();
        }

        private void FrmMoneyPurposeItems_Load(object sender, System.EventArgs e)
        {
            Library.TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Library.Common.User, barManager1);

            lkBuilding.DataSource = Library.Common.TowerList;
            itemBuilding.EditValue = Library.Common.User.MaTN;
            itemYear.EditValue = System.DateTime.Now.Year;
            
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                _db = new Library.MasterDataContext();
                _buildingId = (byte?) itemBuilding.EditValue;
                _year = int.Parse(itemYear.EditValue.ToString());

                itemMoneyPurpose.EditValue = 0;
                _mp = GetMoneyPurpose(_buildingId, _year);
                if (_mp.MoneyExist != null) itemMoneyPurpose.EditValue = _mp.MoneyExist;

                gc.DataSource = _db.mp_MoneyPurposeItems.Where(_=>_.BuildingId == _buildingId & _.Year == _year & _.MoneyPurposeId == _mp.Id);
            }
            catch (System.Exception ex)
            {
                Library.DialogBox.Error(ex.ToString());
            }
        }

        private Library.mp_MoneyPurpose GetMoneyPurpose(byte? buildingId, int? year)
        {
            return _db.mp_MoneyPurposes.FirstOrDefault(_ => _.BuildingId == buildingId & _.Year == year) ?? new Library.mp_MoneyPurpose();
        }

        private void ItemRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void ItemAddMoneyPurpose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                using (var frm = new Building.KinhPhiDuKien.FrmMoneyPurposeEdit {MpId = _mp.Id, Year = _year, BuildingId = _buildingId})
                {
                    frm.ShowDialog();
                    if (frm.DialogResult == System.Windows.Forms.DialogResult.OK) LoadData();
                }
            }
            catch (System.Exception ex)
            {
                Library.DialogBox.Error("Thêm kinh phí đầu năm lỗi: " + ex);
            }
        }

        private void ItemAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (_mp.MoneyExist == 0)
                {
                    Library.DialogBox.Error("Kinh phí cả năm cần > 0");
                    return;
                }

                using (var frm = new Building.KinhPhiDuKien.FrmMoneyPurposeItemsEdit {MpId = _mp.Id, MpiId = 0})
                {
                    frm.ShowDialog();
                    if (frm.DialogResult == System.Windows.Forms.DialogResult.OK) LoadData();
                }
            }
            catch (System.Exception ex)
            {
                Library.DialogBox.Error("Thêm hạng mục kinh phí bị lỗi: " + ex);
            }
        }

        private void ItemEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (gv.GetFocusedRowCellValue("Id") == null)
                {
                    Library.DialogBox.Error("Vui lòng chọn hạng mục cần sửa, xin cảm ơn.");
                    return;
                }

                using (var frm = new Building.KinhPhiDuKien.FrmMoneyPurposeItemsEdit { MpId = _mp.Id, MpiId = (int)gv.GetFocusedRowCellValue("Id") })
                {
                    frm.ShowDialog();
                    if (frm.DialogResult == System.Windows.Forms.DialogResult.OK) LoadData();
                }
            }
            catch (System.Exception ex)
            {  
                Library.DialogBox.Error("Thêm hạng mục kinh phí bị lỗi: " + ex);
            }
        }

        private void ItemDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                var indexs = gv.GetSelectedRows();
                if (indexs.Length <= 0)
                {
                    Library.DialogBox.Alert("Vui lòng chọn những hạng mục cần xóa");
                    return;
                }

                if (Library.DialogBox.QuestionDelete() == System.Windows.Forms.DialogResult.No) return;

                _db = new Library.MasterDataContext();
                foreach (var item in indexs)
                {
                    var moneyPurposeItem = _db.mp_MoneyPurposeItems.FirstOrDefault(_=>_.Id == (int?)gv.GetRowCellValue(item,"Id"));
                    if (moneyPurposeItem != null)
                    {
                        _mp.MoneyUsed = _mp.MoneyUsed - moneyPurposeItem.MoneyPurpose;
                        _mp.MoneyExist = _mp.MoneyPurpose - _mp.MoneyUsed;

                        _db.mp_MoneyPurposeItems.DeleteOnSubmit(moneyPurposeItem);
                    }
                }

                _db.SubmitChanges();
                LoadData();
            }
            catch (System.Exception ex)
            {
                Library.DialogBox.Error("Xóa bị lỗi: " + ex);
            }
        }

        private void itemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Library.Commoncls.ExportExcel(gc);
        }

        private void itemImport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                using (var frm = new Building.KinhPhiDuKien.Import.FrmMoneyPurposeItemsImport())
                {
                    frm.MpId = _mp.Id;
                    frm.ShowDialog();
                    if (frm.IsSave)
                        LoadData();
                }
            }
            catch
            {
                //
            }
        }
    }
}