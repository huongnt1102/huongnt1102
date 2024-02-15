using System;
using System.Collections.Generic;
using System.Data.Linq.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.Utils.Menu;
using DevExpress.XtraGrid.Views.Grid;
using Library;
using DevExpress.XtraScheduler;
using ListViewComponent;

namespace DichVu.BanGiaoMatBang.Local
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
                var maTn = (byte) itemToaNha.EditValue;
                var tuNgay = (DateTime?) itemTuNgay.EditValue;
                var denNgay = (DateTime?) itemDenNgay.EditValue;
                var db = new MasterDataContext();
                var lSchedule = db.ho_Schedules.Where(_ =>
                    _.BuildingId == maTn & SqlMethods.DateDiffDay(tuNgay, _.DateHandoverFrom) >= 0 &
                    SqlMethods.DateDiffDay(_.DateHandoverFrom, denNgay) >= 0 & _.IsLocal == true).ToList();
                gc.DataSource = lSchedule;



                LoadSchedule();

                LoadDetail();
            }
            catch
            {
                ListViewHelper.AddListView(schedulerControl1, dateNavigator, searchControl1);
            }
        }

        #region Lịch control
        
        private void LoadSchedule()
        {
            schedulerControl1.Start = System.DateTime.Now;
            //searchControl1.Client = schedulerControl1;
            schedulerControl1.GroupType = SchedulerGroupType.Resource;

            #region Load lịch
            var db = new MasterDataContext();
            var maTn = (byte)itemToaNha.EditValue;
            var tuNgay = (DateTime?)itemTuNgay.EditValue;
            var denNgay = (DateTime?)itemDenNgay.EditValue;

            var colorStatus = db.ho_Status;

            var handover = db.ho_ScheduleApartments
                    .Where(_ => _.BuildingId == maTn & SqlMethods.DateDiffDay(tuNgay, _.DateHandoverTo) >= 0 &
                                SqlMethods.DateDiffDay(_.DateHandoverTo, denNgay) >= 0 & _.IsChoose == true & _.ho_Schedule.IsLocal == true).Select(_ => new
                                {
                                    _.ApartmentName,
                                    _.CustomerName,
                                    _.BuildingChecklistName,
                                    DateHandoverFrom = (DateTime)_.DateHandoverFrom,
                                    DateHandoverTo = (DateTime)(_.DateHandoverFrom > _.DateHandoverTo ? _.DateHandoverFrom.Value.AddHours(2) : _.DateHandoverTo),
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
            var user = (from p in handover group new { p } by new { p.UserName } into g select new { UserName= g.Key.UserName }).ToList();
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
            int cnt = Math.Min(7, Users.Length);
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
                    Appointment apt = schedulerDataStorage1.CreateAppointment(AppointmentType.Pattern);
                    apt.Subject = string.Format("Mặt bằng: {0}", io.ApartmentName);
                    apt.ResourceId = resource.Id;
                    apt.Start = (DateTime)io.DateHandoverFrom;
                    apt.End = (DateTime)io.DateHandoverTo;
                    apt.StatusId = (int)io.StatusId;
                    apt.LabelId = (int)io.StatusId;
                    apt.Description = string.Format("- Tên lịch: {0} \n- Kế hoạch: {1} \n- Trạng thái: {2}", io.ScheduleName, io.PlanName, io.StatusName);
                    apt.Location = io.UserName;

                    //apt.RecurrenceInfo.Type = RecurrenceType.Monthly;
                    //apt.RecurrenceInfo.Start = apt.Start;
                    //apt.RecurrenceInfo.Periodicity = 5;
                    //apt.RecurrenceInfo.Range = RecurrenceRange.EndByDate;
                    //apt.RecurrenceInfo.End = apt.RecurrenceInfo.Start.AddMonths(1);

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

        #endregion

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
                            where _.ScheduleId==id
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
                                _.DutyName
                            }).ToList();
                        break;
                    case "tabHistory":
                        gcHistory.DataSource = db.ho_PlanHistories.Where(_ => _.ScheduleId == id);
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
            schedulerControl1.Start = (DateTime) itemTuNgay.EditValue;
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
                    if ((bool?) gv.GetFocusedRowCellValue("IsPlanAllow") == true)
                    {
                        DialogBox.Error("Kế hoạch của lịch này đã xác nhận, không được sửa nữa");
                        return;
                    }
                }

                using (var frm = new FrmScheduleEdit
                    {BuildingId = (byte) itemToaNha.EditValue, Id = (int?) gv.GetFocusedRowCellValue("Id")})
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

                using (var frm = new FrmScheduleUpdateStatus { Id = (int?)gv.GetFocusedRowCellValue("Id"), IsStatus=true })
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

                using (var frm = new Customer.FrmScheduleUpdateConfirm { Id = (int?)gv.GetFocusedRowCellValue("Id"), IsStatus = false })
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

        private void cbxKbc_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void gvScheduleApartment_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            try
            {
                if (e.Column.FieldName == "StatusName")
                {
                    if (gvScheduleApartment.GetRowCellValue(e.RowHandle, "Color") == null) return;
                    e.Appearance.BackColor = System.Drawing.Color.FromArgb((int)gvScheduleApartment.GetRowCellValue(e.RowHandle, "Color"));
                }
            }
            catch { }
        }

        private void SchedulerControl1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.Control && e.KeyCode == System.Windows.Forms.Keys.E)
            {
                searchControl1.Focus();
                e.IsInputKey = false;
            }
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

        // IsDelete can auto call Button pressed and Event edit value changed
        private bool IsDelete = false; 
        private void searchControl1_EditValueChanged(object sender, EventArgs e)
        {
            if (IsDelete == false)
                ListViewHelper.SwitchToListView();
            IsDelete = false;
        }

        private void searchControl1_ButtonPressed(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Clear)
            {
                IsDelete = true;
                ListViewHelper.SwitchToNormalView();
                schedulerControl1.SetSearchControl(searchControl1);
            }
        }
    }
}