using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Linq.SqlClient;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using Library;
using DevExpress.XtraScheduler;
using ListViewComponent;

namespace DichVu.BanGiaoMatBang.Customer
{
    public partial class FrmSchedule : DevExpress.XtraEditors.XtraForm
    {
        public FrmSchedule()
        {
            InitializeComponent();
        }

        private void FrmSchedule_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);
            glkToaNha.DataSource = Common.TowerList;
            itemToaNha.EditValue = Common.User.MaTN;

            var objKbc = new KyBaoCao();
            foreach (var item in objKbc.Source) cbxKbc.Items.Add(item);
            itemKyBaoCao.EditValue = objKbc.Source[3];
            SetDate(3);
            LoadData();
        }

        private void SetDate(int index)
        {
            var objKbc = new KyBaoCao
            {
                Index = index
            };
            objKbc.SetToDate();
            itemTuNgay.EditValue = objKbc.DateFrom;
            itemDenNgay.EditValue = objKbc.DateTo;
        }

        private void LoadData()
        {
            try
            {
                var buildingId = (byte) itemToaNha.EditValue;
                var tuNgay = (DateTime?) itemTuNgay.EditValue;
                var denNgay = (DateTime?) itemDenNgay.EditValue;
                var db = new MasterDataContext();
                gc.DataSource = db.ho_Schedules.Where(_ => _.BuildingId == buildingId & SqlMethods.DateDiffDay(tuNgay, _.DateHandoverFrom) >= 0 & SqlMethods.DateDiffDay(_.DateHandoverFrom, denNgay) >= 0 & _.IsLocal == false).ToList();

                LoadSchedule();
                LoadDetail();
            }
            catch { }
        }
  
        private void LoadSchedule()
        {
            schedulerControl1.Start = System.DateTime.Now;
            //searchControl1.Client = schedulerControl1;
            schedulerControl1.GroupType = SchedulerGroupType.Resource;

            #region Load lịch
            var buildingId = (byte)itemToaNha.EditValue;
            var tuNgay = (DateTime?)itemTuNgay.EditValue;
            var denNgay = (DateTime?)itemDenNgay.EditValue;
            var db = new MasterDataContext();

            var colorStatus = db.ho_Status;

            var handover = db.ho_ScheduleApartments
                    .Where(_ => _.BuildingId == buildingId & SqlMethods.DateDiffDay(tuNgay, _.DateHandoverFrom) >= 0 &
                                SqlMethods.DateDiffDay(_.DateHandoverFrom, denNgay) >= 0 & _.IsChoose == true &
                                _.StatusId > 5 & _.ScheduleApartmentLocalId != null).Select(_ => new
                                {
                                    _.ApartmentName,
                                    _.CustomerName,
                                    _.BuildingChecklistName,
                                    _.DateHandoverFrom,
                                    _.DateHandoverTo,
                                    //DateHandoverTo = DateTime.ParseExact(_.DateHandoverTo.ToString(), "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture),
                                    _.Id,
                                    _.PlanName,
                                    _.IsChoose,
                                    _.ScheduleName,
                                    _.StatusId,
                                    _.StatusName,
                                    _.UserName,
                                    _.DateNumberNotification
                                }).ToList();

            // get user

            List<string> list = new List<string>();
            var user = (from p in handover group new { p } by new { p.UserName } into g select new { UserName = g.Key.UserName }).ToList();
            foreach (var item in user) list.Add(item.UserName);
            String[] Users = list.ToArray();


            schedulerDataStorage1.Appointments.DataSource = handover;
            schedulerDataStorage1.Appointments.Statuses.Clear();

            foreach (var i in colorStatus)
                schedulerDataStorage1.Appointments.Statuses.Add(Color.FromArgb(int.Parse(i.Color.ToString())), i.Name);

            schedulerDataStorage1.Appointments.Labels.Clear();
            foreach (var i in colorStatus)
                schedulerDataStorage1.Appointments.Labels.Add(Color.FromArgb(int.Parse(i.Color.ToString())), i.Name);

            // add resource user
            schedulerDataStorage1.Resources.Clear();
            schedulerDataStorage1.Appointments.Clear();
            int cnt = Math.Min(50, Users.Length);
            for (int i = 1; i <= cnt; i++)
            {
                Resource resource = schedulerDataStorage1.CreateResource(i);
                resource.Caption = Users[i - 1];
                schedulerDataStorage1.Resources.Add(resource);
            }

            // data
            int count = schedulerDataStorage1.Resources.Count;
            for (int i = 0; i < count; i++)
            {
                Resource resource = schedulerDataStorage1.Resources[i];

                var obj = handover.Where(_ => _.UserName.ToLower() == resource.Caption.ToLower()).ToList();
                foreach (var io in obj)
                {
                    //var a = DateTime.Parse(((DateTime)io.DateHandoverTo).ToString("dd/MM/yyyy HH:mm:ss"));
                    Appointment apt = schedulerDataStorage1.CreateAppointment(AppointmentType.Pattern);
                    apt.Subject = string.Format("Mặt bằng: {0}", io.ApartmentName);
                    apt.ResourceId = resource.Id;
                    apt.Start = (DateTime)io.DateHandoverFrom;
                    apt.End = (io.DateHandoverFrom>io.DateHandoverTo)?(DateTime)io.DateHandoverFrom: (DateTime) io.DateHandoverTo;
                    //apt.End = DateTime.ParseExact(io.DateHandoverTo.ToString(), "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);//(DateTime)io.DateHandoverTo;
                    apt.StatusId = (int)io.StatusId;
                    apt.LabelId = (int)io.StatusId;
                    apt.Description =string.Format("- Tên lịch: {0} \n- Kế hoạch: {1} \n- Trạng thái: {2}", io.ScheduleName, io.PlanName, io.StatusName);
                    apt.Location = string.Format("Khách hàng: {0}", io.CustomerName);

                    apt.RecurrenceInfo.Type = RecurrenceType.Daily;
                    apt.RecurrenceInfo.Start = (DateTime)io.DateHandoverFrom;
                    apt.RecurrenceInfo.Periodicity = 0;
                    apt.RecurrenceInfo.Range = RecurrenceRange.EndByDate;
                    apt.RecurrenceInfo.End = (DateTime)io.DateHandoverTo;
                    schedulerDataStorage1.Appointments.Add(apt);
                }

            }

            #endregion

            //searchControl1.Client = schedulerControl1; // why error?
            ListViewHelper.AddListView(schedulerControl1, dateNavigator, searchControl1);
        }

        private void LoadDetail()
        {
            try
            {
                var db = new MasterDataContext();

                var id = (int?) gv.GetFocusedRowCellValue("Id");
                if (id == null)
                {
                    return;
                }

                switch (xtraTabControl1.SelectedTabPage.Name)
                {
                    case "tabScheduleApartment":
                        //gcScheduleApartment.DataSource = db.ho_ScheduleApartments.Where(_ => _.ScheduleId == id);
                        gcScheduleApartment.DataSource = (from _ in db.ho_ScheduleApartments
                            join cl in db.ho_Status on _.StatusId equals cl.Id into color
                            from cl in color.DefaultIfEmpty()
                            where _.ScheduleId == id
                            select new
                            {
                                _.ApartmentName,
                                _.CustomerName,
                                _.BuildingChecklistName,
                                _.DateHandoverFrom,
                                _.DateHandoverTo,
                                _.Id,
                                _.PlanName,
                                _.IsChoose,
                                _.ScheduleName,
                                _.StatusName,
                                _.UserName,
                                _.DateNumberNotification,
                                cl.Color,
                                _.DutyName,_.ApartmentId,_.IsBooked,_.CustomerId
                            }).ToList();
                        break;
                    case "tabStatus":
                        gcStatus.DataSource = db.ho_ScheduleHistories.Where(_ => _.ScheduleId == id & _.IsStatus == true);
                        break;
                    case "tabConfirm":
                        gcConfirm.DataSource = db.ho_ScheduleHistories.Where(_ => _.ScheduleId == id & _.IsStatus == false);
                        break;
                    case "tabHistory":
                        gcHistory.DataSource = db.ho_PlanHistories.Where(_ => _.ScheduleId == id);
                        break;
                    case "tabNotifycation":
                        gcNotifycation.DataSource = db.ho_Notifycations.Where(_ => _.ScheduleId == id);
                        break;
                }
            }
            catch (Exception)
            {
                //
            }
        }

        private void ItemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void ItemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var frm = new FrmScheduleEdit { BuildingId = (byte)itemToaNha.EditValue })
            {
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK) LoadData();
            }
        }

        private void Gv_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
                var size = e.Graphics.MeasureString(e.Info.DisplayText, e.Appearance.Font);
                var width = Convert.ToInt32(size.Width) + 20;
                BeginInvoke(new MethodInvoker(delegate { Cal(width, gv); }));
            }
        }
        private bool Cal(int width, GridView view)
        {
            view.IndicatorWidth = view.IndicatorWidth < width ? width : view.IndicatorWidth;
            return true;
        }

        private void ItemSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (gv.GetFocusedRowCellValue("Id") == null)
                {
                    DialogBox.Error("Vui lòng chọn phiếu cần sửa, xin cảm ơn.");
                    return;
                }

                if (gv.GetFocusedRowCellValue("IsPlanAllow") != null)
                {
                    if ((bool?)gv.GetFocusedRowCellValue("IsPlanAllow") == true)
                    {
                        DialogBox.Error("Kế hoạch của lịch này đã xác nhận, không được sửa nữa");
                        return;
                    }
                }

                using (var frm = new FrmScheduleEdit { BuildingId = (byte)itemToaNha.EditValue, Id = (int?)gv.GetFocusedRowCellValue("Id") })
                {
                    frm.ShowDialog();
                    if (frm.DialogResult == DialogResult.OK) LoadData();
                }
            }
            catch (Exception)
            {
                //throw;
            }
        }

        private void ItemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                using (var db = new MasterDataContext())
                {
                    int[] indexs = gv.GetSelectedRows();
                    if (indexs.Length <= 0)
                    {
                        DialogBox.Alert("Vui lòng chọn những phiếu cần xóa");
                        return;
                    }
                    if (DialogBox.QuestionDelete() == DialogResult.No) return;

                    foreach (var r in indexs)
                    {
                        var schedule = db.ho_Schedules.FirstOrDefault(_ =>
                            _.Id == int.Parse(gv.GetRowCellValue(r, "Id").ToString()));
                        if (schedule != null)
                        {
                            if (schedule.IsPlanAllow.GetValueOrDefault() != true)
                            {
                                #region Lưu lịch sử

                                var history = new ho_PlanHistory();
                                history.PlanId = schedule.PlanId;
                                history.PlanName = schedule.PlanName;
                                history.Content = "Xóa lịch bàn giao: " + schedule.Name;
                                history.DateHandoverFrom = schedule.DateHandoverFrom;
                                history.DateHandoverTo = schedule.DateHandoverTo;
                                history.DateCreate = DateTime.UtcNow.AddHours(7);
                                history.UserCreate = Common.User.MaNV;
                                history.UserCreateName = Common.User.HoTenNV;
                                history.BuildingId = schedule.BuildingId;
                                history.IsLocal = schedule.IsLocal;
                                history.ScheduleId = schedule.Id;
                                history.ScheduleName = schedule.Name;
                                db.ho_PlanHistories.InsertOnSubmit(history);
                                #endregion

                                db.ho_ScheduleApartmentCheckLists.DeleteAllOnSubmit(
                                    db.ho_ScheduleApartmentCheckLists.Where(_ => _.ScheduleId == schedule.Id));
                                db.ho_ScheduleApartmentAssets.DeleteAllOnSubmit(
                                    db.ho_ScheduleApartmentAssets.Where(_ => _.ScheduleId == schedule.Id));
                                db.ho_ScheduleApartments.DeleteAllOnSubmit(
                                    db.ho_ScheduleApartments.Where(_ => _.ScheduleId == schedule.Id));
                                db.ho_ScheduleHistories.DeleteAllOnSubmit(db.ho_ScheduleHistories
                                    .Where(_ => _.ScheduleId == schedule.Id).ToList());
                                db.ho_Schedules.DeleteOnSubmit(schedule);
                            }
                        }

                    }
                    db.SubmitChanges();
                    LoadData();
                }
            }
            catch (Exception)
            {
                DialogBox.Error("Có nơi khác đang dùng Lịch bàn giao này nên không xóa được");
                return;
            }
        }

        private void xtraTabControl1_Click(object sender, EventArgs e)
        {
            LoadDetail();
        }

        private void itemScheduleStatus_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (gv.GetFocusedRowCellValue("Id") == null)
                {
                    DialogBox.Error("Vui lòng chọn phiếu cần cập nhật, xin cảm ơn.");
                    return;
                }
                if (gv.GetFocusedRowCellValue("IsPlanAllow") == null)
                {
                    DialogBox.Alert("Lịch đã duyệt mới được sử dụng tính năng này.");
                    return;
                }
                if ((bool?)gv.GetFocusedRowCellValue("IsPlanAllow") == false)
                {
                    DialogBox.Alert("Lịch đã duyệt mới được sử dụng tính năng này.");
                    return;
                }

                using (var frm = new Local.FrmScheduleUpdateStatus { Id = (int?)gv.GetFocusedRowCellValue("Id"), IsStatus = true })
                {
                    frm.ShowDialog();
                    if (frm.DialogResult == DialogResult.OK) LoadData();
                }
            }
            catch (Exception)
            {
                //throw;
            }
        }

        private void itemScheduleConfirm_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (gv.GetFocusedRowCellValue("Id") == null)
                {
                    DialogBox.Error("Vui lòng chọn phiếu cần cập nhật, xin cảm ơn.");
                    return;
                }
                if (gv.GetFocusedRowCellValue("IsPlanAllow") == null)
                {
                    DialogBox.Alert("Lịch đã duyệt mới được sử dụng tính năng này.");
                    return;
                }
                if ((bool?)gv.GetFocusedRowCellValue("IsPlanAllow") == false)
                {
                    DialogBox.Alert("Lịch đã duyệt mới được sử dụng tính năng này.");
                    return;
                }

                using (var frm = new FrmScheduleUpdateConfirm { Id = (int?)gv.GetFocusedRowCellValue("Id"), IsStatus = false })
                {
                    frm.ShowDialog();
                    if (frm.DialogResult == DialogResult.OK) LoadData();
                }
            }
            catch (Exception)
            {
                //throw;
            }
        }

        private void gv_RowClick(object sender, RowClickEventArgs e)
        {
            LoadDetail();
        }

        private void ItemUpdateCustomer_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (gv.GetFocusedRowCellValue("Id") == null)
                {
                    DialogBox.Error("Vui lòng chọn phiếu cần sửa, xin cảm ơn.");
                    return;
                }

                using (var frm = new FrmScheduleUpdateInfo { BuildingId = (byte)itemToaNha.EditValue, Id = (int?)gv.GetFocusedRowCellValue("Id") })
                {
                    frm.ShowDialog();
                    if (frm.DialogResult == DialogResult.OK) LoadData();
                }
            }
            catch (Exception)
            {
                //throw;
            }
        }

        private void ItemCreateEmpty_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (gv.GetFocusedRowCellValue("Id") == null)
                {
                    DialogBox.Error("Vui lòng chọn lịch, xin cảm ơn.");
                    return;
                }

                using (var frm = new FrmScheduleApartmentEmpty { ScheduleId = (int?)gv.GetFocusedRowCellValue("Id") })
                {
                    frm.ShowDialog();
                    if (frm.DialogResult == DialogResult.OK) LoadData();
                }
            }
            catch (Exception)
            {
                //throw;
            }
        }

        private void ItemAddSchedule_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (gv.GetFocusedRowCellValue("Id") == null)
                {
                    DialogBox.Error("Vui lòng chọn lịch, xin cảm ơn.");
                    return;
                }

                if (gvScheduleApartment.GetFocusedRowCellValue("ApartmentId") !=null)
                {
                    DialogBox.Error("Vui lòng chọn chổ trống, xin cảm ơn.");
                    return;
                }
                if (gvScheduleApartment.GetFocusedRowCellValue("IsBooked") !=null)
                {
                    var isBooked = (bool?) gvScheduleApartment.GetFocusedRowCellValue("IsBooked");
                    if (isBooked == true)
                    {
                        DialogBox.Error("Vị trí đã có người khác đặt trước");
                        return;
                    }
                }

                using (var frm = new FrmScheduleChange { ScheduleId = (int?)gv.GetFocusedRowCellValue("Id"), ScheduleApartment = (int?)gvScheduleApartment.GetFocusedRowCellValue("Id") })
                {
                    frm.ShowDialog();
                    if (frm.DialogResult == DialogResult.OK) LoadData();
                }
            }
            catch (Exception)
            {
                //throw;
            }
        }

        private void ItemDeleteSchedule_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (gv.GetFocusedRowCellValue("Id") == null)
                {
                    DialogBox.Error("Vui lòng chọn lịch, xin cảm ơn.");
                    return;
                }

                if (gvScheduleApartment.GetFocusedRowCellValue("Id") == null)
                {
                    DialogBox.Error("Vui lòng mặt bằng cần hủy đặt lịch.");
                    return;
                }

                if (gvScheduleApartment.GetFocusedRowCellValue("StatusId") != null)
                {
                    if ((int?) gvScheduleApartment.GetFocusedRowCellValue("StatusId") == 8)
                    {
                        DialogBox.Error("Mặt bằng đã bàn giao thành công, không được hủy.");
                        return;
                    }
                }

                // khi hủy đặt lịch, có nên xóa phiếu checklist không? câu trả lời là không cần
                var db = new MasterDataContext();
                var scheduleApartment = db.ho_ScheduleApartments.FirstOrDefault(_ => _.Id == (int?) gvScheduleApartment.GetFocusedRowCellValue("Id"));
                if (scheduleApartment != null)
                {
                    if (scheduleApartment.ScheduleApartmentLocalId != null)
                    {
                        var scheduleApartmentLocal = db.ho_ScheduleApartments.FirstOrDefault(_ =>_.Id == (int?) scheduleApartment.ScheduleApartmentLocalId);
                        if (scheduleApartmentLocal != null)
                        {
                            scheduleApartmentLocal.StatusId = 4;
                            scheduleApartmentLocal.StatusName = "Chờ bàn giao khách hàng";
                            scheduleApartmentLocal.UserUpdateId = Common.User.MaNV;
                            scheduleApartmentLocal.UserUpdateName = Common.User.HoTenNV;
                            scheduleApartmentLocal.DateUpdate = DateTime.UtcNow.AddHours(7);
                        }
                    }

                    #region Lưu lịch sử

                    var history = new ho_PlanHistory();
                    history.PlanId = scheduleApartment.PlanId;
                    history.PlanName = scheduleApartment.PlanName;
                    history.Content = "Hủy đặt lịch mặt bằng " + scheduleApartment.ApartmentName;
                    history.DateHandoverFrom = scheduleApartment.DateHandoverFrom;
                    history.DateHandoverTo = scheduleApartment.DateHandoverTo;
                    history.DateCreate = DateTime.UtcNow.AddHours(7);
                    history.UserCreate = Common.User.MaNV;
                    history.UserCreateName = Common.User.HoTenNV;
                    history.BuildingId = scheduleApartment.BuildingId;
                    history.IsLocal = scheduleApartment.IsLocal;
                    history.ScheduleId = scheduleApartment.ScheduleId;
                    history.ScheduleName = scheduleApartment.ScheduleName;
                    history.ScheduleApartmentId = scheduleApartment.Id;
                    history.ApartmentId = scheduleApartment.ApartmentId;
                    history.ApartmentName = scheduleApartment.ApartmentName;
                    db.ho_PlanHistories.InsertOnSubmit(history);
                    #endregion

                    scheduleApartment.ApartmentId = null;
                    scheduleApartment.ApartmentName = null;
                    scheduleApartment.CustomerId = null;
                    scheduleApartment.CustomerName = null;
                    scheduleApartment.ScheduleApartmentLocalId = null;
                    scheduleApartment.UserUpdateId = Common.User.MaNV;
                    scheduleApartment.UserUpdateName = Common.User.HoTenNV;
                    scheduleApartment.DateUpdate = DateTime.UtcNow.AddHours(7);
                    scheduleApartment.StatusId = 5;
                    scheduleApartment.StatusName = "Chưa bàn giao";
                }

                db.SubmitChanges();
                DialogBox.Success("Lưu dữ liệu thành công.");

                LoadDetail();
            }
            catch (Exception)
            {
                //throw;
            }
        }

        private void gvScheduleApartment_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            try
            {
                if ((e.Column.FieldName == "StatusName") & gvScheduleApartment.GetRowCellValue(e.RowHandle, "Color")!=null)
                {
                    e.Appearance.BackColor = System.Drawing.Color.FromArgb((int)gvScheduleApartment.GetRowCellValue(e.RowHandle, "Color"));
                }
            }
            catch { }
        }

        private void schedulerControl1_PopupMenuShowing(object sender, DevExpress.XtraScheduler.PopupMenuShowingEventArgs e)
        {
            switch (e.Menu.Id)
            {
                case SchedulerMenuItemId.DefaultMenu:
                    e.Menu.RemoveMenuItem(SchedulerMenuItemId.NewAppointment);
                    e.Menu.RemoveMenuItem(SchedulerMenuItemId.NewRecurringEvent);
                    e.Menu.RemoveMenuItem(SchedulerMenuItemId.GotoToday);
                    e.Menu.RemoveMenuItem(SchedulerMenuItemId.GotoDate);
                    e.Menu.RemoveMenuItem(SchedulerMenuItemId.GotoThisDay);
                    e.Menu.RemoveMenuItem(SchedulerMenuItemId.NewAllDayEvent);
                    e.Menu.RemoveMenuItem(SchedulerMenuItemId.NewRecurringAppointment);
                    e.Menu.RemoveMenuItem(SchedulerMenuItemId.TimeScaleEnable);
                    e.Menu.RemoveMenuItem(SchedulerMenuItemId.TimeScaleVisible);

                    SchedulerPopupMenu itemSwitchViewTo = e.Menu.GetPopupMenuById(SchedulerMenuItemId.SwitchViewMenu);
                    if (itemSwitchViewTo != null)
                    {
                        itemSwitchViewTo.Caption = "Kiểu lịch";
                        itemSwitchViewTo.Items[0].Caption = "Lịch ngày";
                        itemSwitchViewTo.Items[1].Caption = "Lịch tuần làm việc";
                        itemSwitchViewTo.Items[2].Caption = "Lịch tuần";
                        itemSwitchViewTo.Items[3].Caption = "Lịch tháng";
                        itemSwitchViewTo.Items[4].Caption = "Lịch dòng thời gian";
                    }

                    break;

                case SchedulerMenuItemId.AppointmentMenu:
                    e.Menu.RemoveMenuItem(SchedulerMenuItemId.LabelSubMenu);
                    e.Menu.RemoveMenuItem(SchedulerMenuItemId.StatusSubMenu);
                    e.Menu.RemoveMenuItem(SchedulerMenuItemId.OpenAppointment);
                    e.Menu.RemoveMenuItem(SchedulerMenuItemId.DeleteAppointment);
                    break;

                case SchedulerMenuItemId.AppointmentDragMenu:
                    e.Menu.RemoveMenuItem(SchedulerMenuItemId.AppointmentDragMove);
                    e.Menu.RemoveMenuItem(SchedulerMenuItemId.AppointmentDragCancel);
                    e.Menu.RemoveMenuItem(SchedulerMenuItemId.AppointmentDragCopy);
                    break;
            }
        }

        private void schedulerControl1_EditAppointmentFormShowing(object sender, AppointmentFormEventArgs e)
        {
            e.Handled = true;
        }

        private bool IsDelete = false; 
        private void searchControl1_ButtonPressed(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Clear)
            {
                IsDelete = true;
                ListViewHelper.SwitchToNormalView();
                schedulerControl1.SetSearchControl(searchControl1);
            }
        }

        private void searchControl1_EditValueChanged(object sender, EventArgs e)
        {
            if (IsDelete == false)
                ListViewHelper.SwitchToListView();
            IsDelete = false;
        }

        private void itemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Commoncls.ExportExcel(gc);
        }

        private void itemGuiThongBaoKhachHang_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gv.GetFocusedRowCellValue("Id") == null)
            {
                DialogBox.Error("Vui lòng chọn lịch cần gửi, xin cảm ơn");
                return;
            }
            var xac_nhan = gv.GetFocusedRowCellValue("IsPlanAllow");
            if (gv.GetFocusedRowCellValue("IsPlanAllow") == null)
            {
                DialogBox.Alert("Lịch đã duyệt mới được sử dụng tính năng này.");
                return;
            }
            if((bool?)gv.GetFocusedRowCellValue("IsPlanAllow") == false)
            {
                DialogBox.Alert("Lịch đã duyệt mới được sử dụng tính năng này.");
                return;
            }


            using (var frm = new DichVu.BanGiaoMatBang.Category.FrmNotifycationEdit {BuildingId = (byte) itemToaNha.EditValue, IsLocal = false, ScheduleId = (int)gv.GetFocusedRowCellValue("Id"), CustomerId = GetCustomerId(), ApartmentId = GetApartmentId(), ApartmentName = GetApartmetnName(), ScheduleApartmentId = GetScheduleApartmentId()})
            {
                frm.ShowDialog();
                if (frm.DialogResult == System.Windows.Forms.DialogResult.OK) LoadData();
            }
        }

        #region Get dữ liệu thông báo
        private int GetCustomerId()
        {
            return gvScheduleApartment.GetFocusedRowCellValue("CustomerId")!=null ? (int)gvScheduleApartment.GetFocusedRowCellValue("CustomerId"):0;
        }

        private int GetApartmentId()
        {
            return gvScheduleApartment.GetFocusedRowCellValue("ApartmentId") != null ? (int)gvScheduleApartment.GetFocusedRowCellValue("ApartmentId") : 0;
        }

        private int GetScheduleApartmentId()
        {
            return gvScheduleApartment.GetFocusedRowCellValue("Id") !=null ? (int)gvScheduleApartment.GetFocusedRowCellValue("Id"):0;
        }

        private string GetApartmetnName()
        {
            return gvScheduleApartment.GetFocusedRowCellValue("ApartmentName")!=null?gvScheduleApartment.GetFocusedRowCellValue("ApartmentName").ToString():"";
        }

        #endregion

        private void updateGioChuan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var db = new Library.MasterDataContext();
            var handover = db.ho_ScheduleApartments.Where(_ => _.DateHandoverTo.Value.Minute == 30 & _.DateHandoverTo.Value.Hour == 0);
            foreach (var item in handover)
            {
                item.DateHandoverTo = new DateTime(item.DateHandoverTo.Value.Year, item.DateHandoverTo.Value.Month,
                    item.DateHandoverTo.Value.Day, 12, 30, 0);
            }

            db.SubmitChanges();
            db.Dispose();
        }
    }
}