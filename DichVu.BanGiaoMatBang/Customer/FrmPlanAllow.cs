using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Library;

namespace DichVu.BanGiaoMatBang.Customer
{
    public partial class FrmPlanAllow : DevExpress.XtraEditors.XtraForm
    {
        public int? HandoverId { get; set; }

        private MasterDataContext _db = new MasterDataContext();
        private ho_Plan _plan;

        private List<ho_Schedule> _schedules;
        private ho_PlanHistory _history;

        public FrmPlanAllow()
        {
            InitializeComponent();
        }

        private void FrmPlanAllow_Load(object sender, EventArgs e)
        {
            _plan = _db.ho_Plans.FirstOrDefault(_ => _.Id == HandoverId);
            if (_plan == null) return;

            memo.Text = _plan.ContentAllow;

            #region Lưu lịch sử
            _history = new ho_PlanHistory();
            _history.BuildingId = _plan.BuildingId;
            _history.PlanId = _plan.Id;
            _history.PlanName = _plan.Name;
            _history.DateHandoverFrom = _plan.DateHandOverFrom;
            _history.DateHandoverTo = _plan.DateHandOverTo;
            _history.DateCreate = DateTime.UtcNow.AddHours(7);
            _history.UserCreate = Common.User.MaNV;
            _history.UserCreateName = Common.User.HoTenNV;
            _history.IsLocal = _plan.IsLocal;
            _db.ho_PlanHistories.InsertOnSubmit(_history);
            #endregion

            _schedules = _db.ho_Schedules.Where(_ => _.PlanId == HandoverId).ToList();
            gcSchedule.DataSource = _schedules;
        }

        private void ItemAllow_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _history.Content = "Xác nhận kế hoạch: " + memo.Text;

            _plan.ContentAllow = memo.Text;
            _plan.UserAllowName = Common.User.HoTenNV;
            _plan.UserAllowId = Common.User.MaNV;
            _plan.IsAllow = true;

            _schedules.ForEach(_ => _.IsPlanAllow = true);

            #region Tạo phiếu checklist tự động

            //_db.ho_ScheduleApartmentCheckLists.DeleteAllOnSubmit(
            //    _db.ho_ScheduleApartmentCheckLists.Where(_ => _.PlanId == HandoverId));

            //// tạo lịch và phiếu checklist thôi, chứ còn tài sản thì khi vào từng lịch mới tiến hành thêm tài sản vào, tài sản là cho từng tòa nhà, nên không thể gắn vào lịch, tài sản gắn vào từng tòa nhà, không liên quan gì tới phần duyệt lịch, duyệt kế hoạch
            //var listScheduleApartment = _db.ho_ScheduleApartments.Where(_ => _.PlanId == HandoverId);
            //foreach (var item in listScheduleApartment)
            //{
            //    if (item.BuildingChecklistId == null) continue;
            //    var buildingChecklist = _db.ho_BuildingChecklists.FirstOrDefault(_ => _.Id == item.BuildingChecklistId);
            //    if (buildingChecklist == null) continue;
            //    if (buildingChecklist.ListChecklistId == null) continue;

            //    var listChecklist = _db.ho_Checklists.Where(_ =>
            //        _.ListChecklistId == buildingChecklist.ListChecklistId & _.IsNotUse == false);
            //    foreach (var checklist in listChecklist)
            //    {
            //        var apartmentChecklist = new ho_ScheduleApartmentCheckList();
            //        apartmentChecklist.GroupName = checklist.GroupName;
            //        apartmentChecklist.BuildingId = item.BuildingId;
            //        apartmentChecklist.DateAction = item.DateHandoverFrom;
            //        apartmentChecklist.UserActionId = item.UserId;
            //        apartmentChecklist.UserActionName = item.UserName;
            //        apartmentChecklist.UserCreate = Common.User.MaNV;
            //        apartmentChecklist.UserCreateName = Common.User.HoTenNV;
            //        apartmentChecklist.DateCreate = DateTime.UtcNow.AddHours(7);
            //        apartmentChecklist.PlanId = HandoverId;
            //        apartmentChecklist.Name = checklist.Name;
            //        apartmentChecklist.PlanName = _plan.Name;
            //        apartmentChecklist.ScheduleName = item.ScheduleName;
            //        apartmentChecklist.BuildingChecklistId = buildingChecklist.Id;
            //        apartmentChecklist.BuildingChecklistName = buildingChecklist.ListChecklistName;
            //        apartmentChecklist.IsLocal = false;
            //        apartmentChecklist.Stt = checklist.Stt;
            //        apartmentChecklist.ScheduleId = item.ScheduleId;
            //        apartmentChecklist.Description = checklist.Description;
            //        apartmentChecklist.IsNoQuality = false;
            //        item.ho_ScheduleApartmentCheckLists.Add(apartmentChecklist);
            //    }

            //    item.StatusId = 6;
            //    item.StatusName = "Chờ bàn giao khách hàng";

            //    Library.mbMatBang matBang = GetMatBang(item.ApartmentId);
            //    if (matBang != null) matBang.MaTT = 53;
            //}

            var param = new Dapper.DynamicParameters();
            param.Add("@PlanId", HandoverId, System.Data.DbType.Int32, null, null);
            param.Add("@PlanName", _plan.Name, System.Data.DbType.String, null, null);
            param.Add("@UserCreateId", Common.User.MaNV, System.Data.DbType.Int32, null, null);
            param.Add("@UserCreateName", Common.User.HoTenNV, System.Data.DbType.String, null, null);
            param.Add("@DateCreate", DateTime.UtcNow.AddHours(7), System.Data.DbType.DateTime, null, null);
            param.Add("@IsLocal", false, System.Data.DbType.Boolean, null, null);
            param.Add("@StatusId", 6, System.Data.DbType.Int32, null, null);
            param.Add("@StatusName", "Chờ bàn giao khách hàng", System.Data.DbType.String, null, null);
            param.Add("@MaTT", 53, System.Data.DbType.Int32, null, null);
            var kq = Library.Class.Connect.QueryConnect.Query<bool>("dbo.ho_TaoChecklist", param).ToList();

            _db.SubmitChanges();

            DialogBox.Success("Thiết lập lịch bàn giao thành công.");
            DialogResult = DialogResult.OK;
            Close();

            #endregion
        }

        private Library.mbMatBang GetMatBang(int? matBangId)
        {
            return _db.mbMatBangs.FirstOrDefault(_ => _.MaMB == matBangId);
        }

        private void ItemNotAllow_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _history.Content = "Không xác nhận kế hoạch: " + memo.Text;

            _plan.ContentAllow = memo.Text;
            _plan.UserAllowName = Common.User.HoTenNV;
            _plan.UserAllowId = Common.User.MaNV;
            _plan.IsAllow = false;

            _db.SubmitChanges();

            DialogBox.Success("Thiết lập lịch bàn giao thành công.");
            DialogResult = DialogResult.OK;
            Close();
        }

        private void ItemCancel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void GvSchedule_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
                var size = e.Graphics.MeasureString(e.Info.DisplayText, e.Appearance.Font);
                var width = Convert.ToInt32(size.Width) + 20;
                BeginInvoke(new MethodInvoker(delegate { Cal(width, gvSchedule); }));
            }
        }

        private void GvScheduleApartment_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
                var size = e.Graphics.MeasureString(e.Info.DisplayText, e.Appearance.Font);
                var width = Convert.ToInt32(size.Width) + 20;
                BeginInvoke(new MethodInvoker(delegate { Cal(width, gvScheduleApartment); }));
            }
        }

        private bool Cal(int width, DevExpress.XtraGrid.Views.Grid.GridView view)
        {
            view.IndicatorWidth = view.IndicatorWidth < width ? width : view.IndicatorWidth;
            return true;
        }

        private void GvSchedule_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            var id = (int?) gvSchedule.GetFocusedRowCellValue("Id");
            if (id == null)
            {
                return;
            }

            gcScheduleApartment.DataSource = _db.ho_ScheduleApartments.Where(_ => _.ScheduleId == id & _.IsChoose == true);
        }
    }
}