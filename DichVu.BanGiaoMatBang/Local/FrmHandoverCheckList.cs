using System;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Library;

namespace DichVu.BanGiaoMatBang.Local
{
    public partial class FrmHandoverCheckList : DevExpress.XtraEditors.XtraForm
    {
        public int? Id { get; set; }
        public byte? BuildingId { get; set; }

        private ho_ScheduleApartment _scheduleApartment;
        private MasterDataContext _db = new MasterDataContext();
        private string _userActionName, _handoverName;
        
        public FrmHandoverCheckList()
        {
            InitializeComponent();
        }
        private void FrmHandoverCheckList_Load(object sender, EventArgs e)
        {
            if (Id == null) return;
            glkUserAction.Properties.DataSource =
                _db.tnNhanViens.Select(_ => new {_.HoTenNV, _.MaNV, _.MaSoNV}).ToList();
            glkUserAction.EditValue = Common.User.MaNV;

            _scheduleApartment = _db.ho_ScheduleApartments.FirstOrDefault(_ => _.Id == Id);
            if (_scheduleApartment == null) return;
            dateAction.DateTime = DateTime.UtcNow.AddHours(7);

            gc.DataSource = _scheduleApartment.ho_ScheduleApartmentCheckLists;
        }

        private void ItemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                //foreach (var item in _handover.ho_HandoverCheckLists)
                //{
                //    var history = new ho_HandoverHistory();
                //    history.HandoverName = _handover.Name;
                //    history.ApartmentName = _handover.ApartmentName;
                //    history.CustomerName = _handover.CustomerName;
                //    if (item.DateUpdate != null) history.Content = "Cập nhật hạng mục bàn giao.";
                //    else history.Content = "Đưa ra các hạng mục bàn giao";
                //    history.ChecklistSytemHandover = item.SystemName;
                //    history.ChecklistContentHandover = item.Content;
                //    history.DateHandover = _handover.DateHandOver;
                //    history.ChecklistNoquality = item.IsNoQuality;
                //    history.ChecklistNotqualityAllowNote = item.UserAllowNote;
                //    history.UserCreate = Common.User.MaNV;
                //    history.UserCreateName = Common.User.HoTenNV;
                //    history.DateCreate = DateTime.UtcNow.AddHours(7);
                //    history.BuildingId = _handover.BuildingId;
                //    _handover.ho_HandoverHistories.Add(history);
                //}
                _db.SubmitChanges();
                DialogBox.Success("Lưu hạng mục checklist bàn giao thành công.");
                DialogResult = DialogResult.OK;
                Close();
                
            }
            catch
            {
                DialogBox.Error("Lưu hạng mục không thành công");
            }
        }

        private void ItemCancel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void GlkUserAction_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                var item = sender as GridLookUpEdit;
                if (item.EditValue == null)
                {
                    DialogBox.Error("Vui lòng chọn người thực hiện");
                    return;
                }
                _userActionName = item.Properties.View.GetFocusedRowCellValue("HoTenNV").ToString();
            }
            catch{}
        }

        private void Gv_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            gv.SetFocusedRowCellValue("Id", 0);
            gv.SetFocusedRowCellValue("UserCreate", Common.User.MaNV);
            gv.SetFocusedRowCellValue("DateCreate", DateTime.UtcNow.AddHours(7));
            gv.SetFocusedRowCellValue("UserActionName", _userActionName);
            gv.SetFocusedRowCellValue("UserCreateName", Common.User.HoTenNV);
            gv.SetFocusedRowCellValue("BuildingId", BuildingId);
            gv.SetFocusedRowCellValue("DateAction", (DateTime) dateAction.DateTime);
            gv.SetFocusedRowCellValue("UserActionId", (int) glkUserAction.EditValue);
            gv.SetFocusedRowCellValue("PlanId", _scheduleApartment.PlanId);
            gv.SetFocusedRowCellValue("PlanName", _scheduleApartment.PlanName);
            gv.SetFocusedRowCellValue("IsNoQuality", false);
            gv.SetFocusedRowCellValue("ScheduleName", _scheduleApartment.ScheduleName);
            gv.SetFocusedRowCellValue("ScheduleApartmentId", _scheduleApartment.Id);
            gv.SetFocusedRowCellValue("BuildingChecklistId", _scheduleApartment.BuildingChecklistId);
            gv.SetFocusedRowCellValue("BuildingChecklistName", _scheduleApartment.BuildingChecklistName);
            gv.SetFocusedRowCellValue("IsLocal", true);
            gv.SetFocusedRowCellValue("ScheduleId", _scheduleApartment.ScheduleId);
        }

        private void Gv_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                if (e.Column.FieldName == "GroupName" || e.Column.FieldName == "Name" || e.Column.FieldName == "Quality")
                {
                    gv.SetFocusedRowCellValue("UserActionName", _userActionName);
                    gv.SetFocusedRowCellValue("UserActionId", (int)glkUserAction.EditValue);
                    gv.SetFocusedRowCellValue("DateAction", (DateTime)dateAction.DateTime);
                }
                var id = gv.GetFocusedRowCellValue("Id");
                if (id == null | (int?)id == 0) return;
                if (e.Column.FieldName == "GroupName" || e.Column.FieldName == "Name" || e.Column.FieldName == "Quality" || e.Column.FieldName == "ValueChoose" || e.Column.FieldName == "ValueInput" || e.Column.FieldName == "Note" || e.Column.FieldName == "Image")
                {
                    gv.SetFocusedRowCellValue("UserUpdate", Common.User.MaNV);
                    gv.SetFocusedRowCellValue("DateUpdate", DateTime.UtcNow.AddHours(7));
                    gv.SetFocusedRowCellValue("UserUpdateName", Common.
User.HoTenNV);
                }
            }
            catch { }
        } 

        private void ItemDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(gv.GetFocusedDataSourceRowIndex().ToString()))
                {
                    gv.DeleteSelectedRows();
                }
            }
            catch (Exception)
            {
                //throw;
            }
        }
    }
}