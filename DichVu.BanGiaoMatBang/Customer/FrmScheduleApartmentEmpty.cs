using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Library;

namespace DichVu.BanGiaoMatBang.Customer
{
    public partial class FrmScheduleApartmentEmpty : DevExpress.XtraEditors.XtraForm
    {
        public int? ScheduleId { get; set; }

        private MasterDataContext _db = new MasterDataContext();
        private ho_Schedule _schedule;
        private ho_PlanHistory _history;

        public FrmScheduleApartmentEmpty()
        {
            InitializeComponent();
        }

        private void FrmScheduleApartmentEmpty_Load(object sender, EventArgs e)
        {
            _schedule = _db.ho_Schedules.FirstOrDefault(_ => _.Id == ScheduleId);
            if (_schedule == null) return;
            glkDuty.DataSource = _db.ho_Duties.Where(_=>_.BuildingId == _schedule.BuildingId);
            glkBuildingChecklist.DataSource = _db.ho_BuildingChecklists.Where(_=>_.BuildingId == _schedule.BuildingId).Select(_=>new{_.Id,Name=_.ListChecklistName}).ToList();
            gc.DataSource = _db.ho_ScheduleApartments.Where(_ => _.ApartmentId == null & _.ScheduleId == ScheduleId);

            #region Lưu lịch sử

            _history = new ho_PlanHistory();
            _history.PlanId = _schedule.PlanId;
            _history.PlanName = _schedule.PlanName;
            _history.Content = "Đặt chổ trống cho lịch " + _schedule.Name;
            _history.DateHandoverFrom = _schedule.DateHandoverFrom;
            _history.DateHandoverTo = _schedule.DateHandoverTo;
            _history.DateCreate = DateTime.UtcNow.AddHours(7);
            _history.UserCreate = Common.User.MaNV;
            _history.UserCreateName = Common.User.HoTenNV;
            _history.BuildingId = _schedule.BuildingId;
            _history.IsLocal = _schedule.IsLocal;
            _history.ScheduleId = _schedule.Id;
            _history.ScheduleName = _schedule.Name;

            _db.ho_PlanHistories.InsertOnSubmit(_history);

            #endregion
        }

        private void ItemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                gv.PostEditor();
                _db.SubmitChanges();
                DialogBox.Success("Lưu dữ liệu thành công.");
                DialogResult = DialogResult.OK;
                Close();
            }
            catch{}
        }

        private void ItemCancel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void Gv_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            gv.AddNewRow();
            gv.SetFocusedRowCellValue("ScheduleId", _schedule.Id);
            gv.SetFocusedRowCellValue("ScheduleName", _schedule.Name);
            gv.SetFocusedRowCellValue("PlanId", _schedule.PlanId);
            gv.SetFocusedRowCellValue("PlanName", _schedule.PlanName);
            gv.SetFocusedRowCellValue("BuildingId", _schedule.BuildingId);
            gv.SetFocusedRowCellValue("UserCreateId", Common.User.MaNV);
            gv.SetFocusedRowCellValue("UserCreateName", Common.User.HoTenNV);
            gv.SetFocusedRowCellValue("DateCreate", DateTime.UtcNow.AddHours(7));
            gv.SetFocusedRowCellValue("StatusId", 5);
            gv.SetFocusedRowCellValue("StatusName", "Chờ xác nhận");
            gv.SetFocusedRowCellValue("IsChoose", true);
            gv.SetFocusedRowCellValue("IsLocal", false);
        }

        private void GlkDuty_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                var item = sender as GridLookUpEdit;
                if (item.EditValue == null) return;
                gv.SetFocusedRowCellValue("DutyId", item.EditValue);
                gv.SetFocusedRowCellValue("DutyName", item.Properties.View.GetFocusedRowCellValue("Name"));
                var timeFrom = (DateTime)item.Properties.View.GetFocusedRowCellValue("HourFrom");
                var timeTo = (DateTime)item.Properties.View.GetFocusedRowCellValue("HourTo");
                var day = (DateTime)gv.GetFocusedRowCellValue("DateHandoverFrom");

                gv.SetFocusedRowCellValue("DateHandoverFrom", new DateTime(day.Year, day.Month, day.Day, timeFrom.Hour, timeFrom.Minute, 0));
                gv.SetFocusedRowCellValue("DateHandoverTo", new DateTime(day.Year, day.Month, day.Day, timeTo.Hour, timeTo.Minute, 0));

                gv.UpdateCurrentRow();
            }
            catch { }
        }

        private void GlkBuildingChecklist_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                var item = sender as GridLookUpEdit;
                if (item.EditValue == null)
                {
                    return;
                }

                gv.SetFocusedRowCellValue("BuildingChecklistId", item.EditValue);
                gv.SetFocusedRowCellValue("BuildingChecklistName",
                    item.Properties.View.GetFocusedRowCellValue("Name"));
                gv.UpdateCurrentRow();

            }
            catch { }
        }
    }
}