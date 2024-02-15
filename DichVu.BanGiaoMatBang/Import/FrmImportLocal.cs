using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Library;
using System.Linq;

namespace DichVu.BanGiaoMatBang.Import
{
    public partial class FrmImportLocal : XtraForm
    {
        public bool IsSave { get; set; }
        public byte? BuildingId { get; set; }

        public FrmImportLocal()
        {
            InitializeComponent();
        }

        private void itemSheet_EditValueChanged(object sender, EventArgs e)
        {
            if (itemSheet.EditValue == null)
                gc.DataSource = null;
            else
                try
                {
                    var excel = new LinqToExcel.ExcelQueryFactory(Tag.ToString());

                    System.Collections.Generic.List<ImportLocal> list = Library.Import.ExcelAuto.ConvertDataTable<ImportLocal>(Library.Import.ExcelAuto.GetDataExcel(excel, gv, itemSheet));

                    gc.DataSource = list;

                    //gc.DataSource = excel.Worksheet(itemSheet.EditValue.ToString()).Select(_ => new ImportLocal
                    //{
                    //    MaKeHoach = _["Mã kế hoạch"].ToString().Trim(),
                    //    TenKeHoach=_["Tên kế hoạch"].ToString().Trim(),
                    //    KeHoachTuNgay=_["Kế hoạch từ ngày"].Cast<DateTime>(),
                    //    KeHoachDenNgay=_["Kế hoạch đến ngày"].Cast<DateTime>(),
                    //    MaLich=_["Mã lịch"].ToString().Trim(),
                    //    TenLich=_["Tên lịch"].ToString().Trim(),
                    //    TenNhomLich=_["Tên nhóm lịch"].ToString().Trim(),
                    //    LichTuNgay=_["Lịch từ ngày"].Cast<DateTime>(),
                    //    LichDenNgay=_["Lịch đến ngày"].Cast<DateTime>(),
                    //    MaBanGiao=_["Mã bàn giao"].ToString().Trim(),
                    //    MaMatBang=_["Mã mặt bằng"].ToString().Trim(),
                    //    NgayBanGiao=_["Ngày bàn giao"].Cast<DateTime>(),
                    //    CaBanGiao=_["Ca bàn giao"].ToString().Trim(),
                    //    NhanVienBanGiao=_["Nhân viên bàn giao"].ToString().Trim(),
                    //    ThoiGianBaoTruoc=_["Thời gian báo trước"].Cast<decimal>(),
                    //    TenMauBanGiao=_["Tên mẫu bàn giao"].ToString().Trim()
                    //}).ToList();

                    excel = null;
                }
                catch (Exception ex)
                {
                    DialogBox.Error(ex.Message);
                }
        }

        private void itemClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void itemDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DialogBox.QuestionDelete() == DialogResult.No) return;
            gv.DeleteSelectedRows();
        }

        private void itemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gc.DataSource == null)
            {
                DialogBox.Error("Vui lòng chọn sheet");
                return;
            }

            var wait = DialogBox.WaitingForm();
            var db = new MasterDataContext();
            try
            {
                var handOverLocal = (List<ImportLocal>) gc.DataSource;
                var ltError = new List<ImportLocal>();
                foreach (var n in handOverLocal)
                {
                    try
                    {
                        db = new MasterDataContext();

                        #region Kiểm tra
                        
                        // Kiểm tra nhóm lịch
                        var groupSchedule = db.ho_ScheduleGroups.FirstOrDefault(_=>_.Name.Trim().ToLower() == n.TenNhomLich.ToLower());
                        if (groupSchedule == null)
                        {
                            n.Error = "Nhóm lịch: " + n.TenNhomLich + " không tồn tại";
                            ltError.Add(n);
                            continue;
                        }

                        // kiểm tra mặt bằng
                        var apartment = db.mbMatBangs.FirstOrDefault(_ => _.MaSoMB.Trim().ToLower() == n.MaMatBang.ToLower() & _.MaTN == BuildingId);
                        if (apartment == null)
                        {
                            n.Error = "Mặt bằng: " + n.MaMatBang + " không tồn tại";
                            ltError.Add(n);
                            continue;
                        }

                        // kiểm tra ca bàn giao
                        var duty = db.ho_Duties.FirstOrDefault(_ => _.Name.Trim().ToLower() == n.CaBanGiao.ToLower() & _.BuildingId == BuildingId);
                        if (duty == null)
                        {
                            n.Error = "Ca bàn giao: " + n.CaBanGiao + " không tồn tại";
                            ltError.Add(n);
                            continue;
                        }

                        // kiểm tra nhân viên bàn giao
                        var user = db.tnNhanViens.FirstOrDefault(_ => _.MaSoNV.Trim().ToLower() == n.NhanVienBanGiao.ToLower());
                        if (user == null)
                        {
                            n.Error = "Nhân viên bàn giao: " + n.NhanVienBanGiao + " không tồn tại";
                            ltError.Add(n);
                            continue;
                        }

                        // kiểm tra tên mẫu bàn giao
                        var buildingChecklist = db.ho_BuildingChecklists.FirstOrDefault(_=>_.ListChecklistName.Trim().ToLower() == n.TenMauBanGiao.ToLower() & _.BuildingId == BuildingId & _.IsNotUse ==false);
                        if (buildingChecklist == null)
                        {
                            n.Error = "Mẫu bàn giao : " + n.TenMauBanGiao + " không tồn tại";
                            ltError.Add(n);
                            continue;
                        }
                        #endregion

                        #region Lưu kế hoạch

                        ho_Plan plan;
                        plan = db.ho_Plans.FirstOrDefault(_ => _.No.Trim().ToLower() == n.MaKeHoach.ToLower() & _.BuildingId == BuildingId);
                        if (plan != null)
                        {
                            plan.UserUpdate = Common.User.MaNV;
                            plan.DateUpdate = DateTime.UtcNow.AddHours(7);
                            plan.UserUpdateName = Common.User.HoTenNV;
                        }
                        else
                        {
                            plan = new ho_Plan();
                            plan.UserCreate = Common.User.MaNV;
                            plan.DateCreate = DateTime.UtcNow.AddHours(7);
                            plan.UserCreateName = Common.User.HoTenNV;
                            db.ho_Plans.InsertOnSubmit(plan);
                        }

                        plan.BuildingId = BuildingId;
                        plan.DateHandOverFrom = n.KeHoachTuNgay;
                        plan.Name = n.TenKeHoach;

                        plan.UserAllowId = Common.User.MaNV;
                        plan.UserAllowName = Common.User.HoTenNV;
                        plan.DateHandOverTo = n.KeHoachDenNgay;
                        plan.IsLocal = true;
                        plan.IsAllow = true;
                        plan.No = n.MaKeHoach;

                        #endregion

                        #region Lịch

                        ho_Schedule schedule;
                        schedule = db.ho_Schedules.FirstOrDefault(_ => _.No.Trim().ToLower() == n.MaLich.ToLower() & _.BuildingId == BuildingId);
                        if (schedule != null)
                        {
                            schedule.DateUpdate = DateTime.UtcNow.AddHours(7);
                            schedule.UserUpdate = Common.User.MaNV;
                            schedule.UserUpdateName = Common.User.HoTenNV;
                        }
                        else
                        {
                            schedule = new ho_Schedule();
                            schedule.DateCreate = DateTime.UtcNow.AddHours(7);
                            schedule.UserCreate = Common.User.MaNV;
                            schedule.UserCreateName = Common.User.HoTenNV;
                            plan.ho_Schedules.Add(schedule);
                        }

                        schedule.Name = n.TenLich;
                        schedule.ScheduleGroupId = groupSchedule.Id;
                        schedule.BuildingId = BuildingId;
                        schedule.ScheduleGroupName = n.TenNhomLich;
                        schedule.PlanName = n.TenKeHoach;
                        schedule.DateHandoverFrom = n.LichTuNgay;
                        schedule.DateHandoverTo = n.LichDenNgay;
                        schedule.IsLocal = true;
                        schedule.IsPlanAllow = true;
                        schedule.No = n.MaLich;

                        db.SubmitChanges();
                        #endregion

                        #region Tạo apartment schedule và apartment checklist

                        ho_ScheduleApartment scheduleApartment;
                        scheduleApartment = db.ho_ScheduleApartments.FirstOrDefault(_=>_.No.Trim().ToLower() == n.MaBanGiao.ToLower() & _.BuildingId == BuildingId);
                        if (scheduleApartment != null)
                        {
                            scheduleApartment.UserUpdateId = Common.User.MaNV;
                            scheduleApartment.UserUpdateName = Common.User.HoTenNV;
                            scheduleApartment.DateUpdate = DateTime.UtcNow.AddHours(7);
                        }
                        else
                        {
                            scheduleApartment = new ho_ScheduleApartment();
                            scheduleApartment.DateCreate = DateTime.UtcNow.AddHours(7);
                            scheduleApartment.UserCreateId = Common.User.MaNV;
                            scheduleApartment.UserCreateName = Common.User.HoTenNV;
                            schedule.ho_ScheduleApartments.Add(scheduleApartment);
                        }

                        scheduleApartment.ApartmentId = apartment.MaMB;
                        scheduleApartment.ApartmentName = apartment.MaSoMB +" - "+apartment.mbTangLau.TenTL+" - "+apartment.mbTangLau.mbKhoiNha.TenKN;
                        scheduleApartment.PlanId = schedule.PlanId;
                        scheduleApartment.PlanName = n.TenKeHoach;
                        scheduleApartment.BuildingChecklistId = buildingChecklist.Id;
                        scheduleApartment.BuildingChecklistName = buildingChecklist.ListChecklistName;
                        scheduleApartment.BuildingId = BuildingId;
                        var ngayBanGiao = (DateTime) n.NgayBanGiao;
                        scheduleApartment.DateHandoverFrom = new DateTime(ngayBanGiao.Year, ngayBanGiao.Month, ngayBanGiao.Day, duty.HourFrom.Value.Hour, duty.HourFrom.Value.Minute, duty.HourFrom.Value.Second);
                        scheduleApartment.DateHandoverTo = new DateTime(ngayBanGiao.Year, ngayBanGiao.Month, ngayBanGiao.Day, duty.HourTo.Value.Hour, duty.HourTo.Value.Minute, duty.HourTo.Value.Second);
                        scheduleApartment.UserId = user.MaNV;
                        scheduleApartment.UserName = user.HoTenNV;
                        scheduleApartment.DateNumberNotification = n.ThoiGianBaoTruoc;
                        scheduleApartment.StatusId = 4;
                        scheduleApartment.StatusName = "Chờ bàn giao khách hàng";
                        scheduleApartment.IsChoose = true;
                        scheduleApartment.DutyId = duty.Id;
                        scheduleApartment.DutyName = duty.Name;
                        scheduleApartment.IsLocal = true;
                        scheduleApartment.No = n.MaBanGiao;

                        // tạo checklist
                        var listChecklist = db.ho_Checklists.Where(_ => _.ListChecklistId == buildingChecklist.ListChecklistId & _.IsNotUse == false);
                        foreach (var checklist in listChecklist)
                        {
                            var apartmentChecklist = new ho_ScheduleApartmentCheckList();
                            apartmentChecklist.GroupName = checklist.GroupName;
                            apartmentChecklist.BuildingId = BuildingId;
                            apartmentChecklist.DateAction = scheduleApartment.DateHandoverFrom;
                            apartmentChecklist.UserActionId = user.MaNV;
                            apartmentChecklist.UserActionName = user.HoTenNV;
                            apartmentChecklist.UserCreate = Common.User.MaNV;
                            apartmentChecklist.UserCreateName = Common.User.HoTenNV;
                            apartmentChecklist.DateCreate = DateTime.UtcNow.AddHours(7);
                            apartmentChecklist.PlanId = plan.Id;
                            apartmentChecklist.Name = checklist.Name;
                            apartmentChecklist.PlanName = plan.Name;
                            apartmentChecklist.ScheduleName = schedule.Name;
                            apartmentChecklist.BuildingChecklistId = buildingChecklist.Id;
                            apartmentChecklist.BuildingChecklistName = buildingChecklist.ListChecklistName;
                            apartmentChecklist.IsLocal = true;
                            apartmentChecklist.Stt = checklist.Stt;
                            apartmentChecklist.ScheduleId = schedule.Id;
                            apartmentChecklist.Description = checklist.Description;
                            apartmentChecklist.IsNoQuality = false;
                            apartmentChecklist.UserAllow = user.MaNV;
                            apartmentChecklist.UserAllowName = user.HoTenNV;
                            apartmentChecklist.DateAllow = DateTime.UtcNow.AddHours(7);
                            scheduleApartment.ho_ScheduleApartmentCheckLists.Add(apartmentChecklist);
                        }
                        #endregion

                        db.SubmitChanges();
                        #region Lịch sử

                        // lịch sử kế hoạch
                        var history = new ho_PlanHistory();
                        history.PlanId = plan.Id;
                        history.PlanName = plan.Name;
                        history.Content = "Chờ bàn giao khách hàng";
                        history.DateHandoverFrom = scheduleApartment.DateHandoverFrom;
                        history.DateHandoverTo = scheduleApartment.DateHandoverTo;
                        history.BuildingId = BuildingId;
                        history.DateCreate = DateTime.UtcNow.AddHours(7);
                        history.UserCreate = Common.User.MaNV;
                        history.UserCreateName = Common.User.HoTenNV;
                        history.IsLocal = true;
                        history.ScheduleId = schedule.Id;
                        history.ScheduleName = schedule.Name;
                        history.ScheduleApartmentId = scheduleApartment.Id;
                        history.ApartmentId = scheduleApartment.ApartmentId;
                        history.ApartmentName = scheduleApartment.ApartmentName;
                        
                        db.ho_PlanHistories.InsertOnSubmit(history);
                        #endregion

                        db.SubmitChanges();
                    }
                    catch (Exception ex)
                    {
                        n.Error = ex.Message;
                        ltError.Add(n);
                    }
                }

                IsSave = true;
                DialogBox.Success();

                if (ltError.Count > 0)
                {
                    gc.DataSource = ltError;
                }
                else
                {
                    gc.DataSource = null;
                }
            }
            catch
            {
                wait.Close();
                DialogBox.Alert("Vui lòng xem lại dữ liệu có bị ràng buộc hay không");
                Close();
            }
            finally
            {
                wait.Dispose();
                db.Dispose();
            }
        }

        private void itemChonFile_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var file = new OpenFileDialog();
            try
            {
                file.Filter = @"(Excel file)|*.xls;*.xlsx";
                file.ShowDialog();
                if (file.FileName == "") return;

                var excel = new LinqToExcel.ExcelQueryFactory(file.FileName);
                var sheets = excel.GetWorksheetNames();
                cmbSheet.Items.Clear();
                foreach (var s in sheets)
                    cmbSheet.Items.Add(s.Trim('$'));

                itemSheet.EditValue = null;
                Tag = file.FileName;
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
            finally
            {
                file.Dispose();
            }
        }

        private void itemExportMau_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Library.Commoncls.ExportExcel(gc);
        }

    }

    public class ImportLocal
    {
        public string MaKeHoach { get; set; }
        public string TenKeHoach { get; set; }
        public string MaLich { get; set; }
        public string TenLich { get; set; }
        public string TenNhomLich { get; set; }
        public string MaBanGiao { get; set; }
        public string MaMatBang { get; set; }
        public string CaBanGiao { get; set; }
        public string NhanVienBanGiao { get; set; }
        public string TenMauBanGiao { get; set; }

        public DateTime? KeHoachTuNgay { get; set; }
        public DateTime? KeHoachDenNgay { get; set; }
        public DateTime? LichTuNgay { get; set; }
        public DateTime? LichDenNgay { get; set; }
        public DateTime? NgayBanGiao { get; set; }

        public decimal? ThoiGianBaoTruoc { get; set; }

        public string Error { get; set; }
    }
}