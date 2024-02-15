using System;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Library;

namespace DichVu.BanGiaoMatBang.Local
{
    public partial class FrmScheduleUpdateStatus : DevExpress.XtraEditors.XtraForm
    {
        public int? Id { get; set; }
        public bool? IsStatus { get; set; }
        private MasterDataContext _db = new MasterDataContext();
        private ho_Schedule _schedule;
        private string _scheduleStatusName;
        public FrmScheduleUpdateStatus()
        {
            InitializeComponent();
        }

        private void FrmScheduleUpdateStatus_Load(object sender, EventArgs e)
        {
            try
            {
                if (Id == null || Id == 0) return;
                _schedule = _db.ho_Schedules.FirstOrDefault(_ => _.Id == Id);
                if (_schedule == null) return;
                glkScheduleStatus.Properties.DataSource = _db.ho_ScheduleStatus.Where(_ => _.IsStatus == IsStatus).ToList();
                glkScheduleStatus.EditValue =
                    IsStatus == true ? _schedule.ScheduleStatusId : _schedule.ScheduleConfirmId;
                _scheduleStatusName = IsStatus == true ? _schedule.ScheduleStatusName : _schedule.ScheduleConfirmName;
            }
            catch{}
        }

        private void itemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (_schedule == null) return;
            if (IsStatus == true)
            {
                _schedule.ScheduleStatusId = (int)glkScheduleStatus.EditValue;
                _schedule.ScheduleStatusName = _scheduleStatusName;
            }
            else
            {
                _schedule.ScheduleConfirmId = (int)glkScheduleStatus.EditValue;
                _schedule.ScheduleConfirmName = _scheduleStatusName;
            }
            _schedule.UserUpdateName = Common.User.HoTenNV;
            _schedule.UserUpdate = Common.User.MaNV;
            _schedule.DateUpdate = DateTime.UtcNow.AddHours(7);

            #region Cập nhật lại history
            var history = new ho_ScheduleHistory();
            history.BuildingId = _schedule.BuildingId;
            history.ScheduleId = _schedule.Id;
            //history.CustomerId = _schedule.CustomerId;
            //history.CustomerName = _schedule.CustomerName;
            history.ScheduleName = _schedule.Name;
            history.StatusId = _schedule.ScheduleStatusId;
            history.StatusName = _schedule.ScheduleStatusName;
            //history.ConfirmId = _schedule.ScheduleConfirmId ;
            //history.ConfirmName = _schedule.ScheduleConfirmName;
            history.DateCreate = DateTime.UtcNow.AddHours(7);
            history.UserCreate = Common.User.MaNV;
            history.UserCreateName = Common.User.HoTenNV;
            //_schedule.ho_ScheduleHistories.Add(history);
            
            history.PlanId = _schedule.PlanId;
            history.PlanName = _schedule.PlanName;
            history.Description = txtContent.Text;
            history.IsStatus = true;
            _db.ho_ScheduleHistories.InsertOnSubmit(history);
            #endregion

            _db.SubmitChanges();
            DialogBox.Success("Cập nhật trạng thái thành công.");
            DialogResult = DialogResult.OK;
            Close();
        }

        private void itemCancel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void glkScheduleStatus_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                var item = sender as GridLookUpEdit;
                if (item.EditValue == null)
                {
                    DialogBox.Alert("Vui lòng chọn trạng thái");
                    return;
                }

                _scheduleStatusName = item.Properties.View.GetFocusedRowCellValue("Name").ToString();
            }
            catch { }
        }
    }
}