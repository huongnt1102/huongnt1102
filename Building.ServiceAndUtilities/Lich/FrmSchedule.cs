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

namespace Building.ServiceAndUtilities.Lich
{
    public partial class FrmSchedule : DevExpress.XtraEditors.XtraForm
    {
        public int Loai { get; set; }

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

                LoadSchedule();
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

            var colorStatus = db.ml_TrangThais;

            //var list = db.ml_KeHoaches.Where(_ => _.MaTN == maTn & SqlMethods.DateDiffDay(tuNgay, _.TuNgay) >= 0 & SqlMethods.DateDiffDay(_.DenNgay, denNgay) >= 0 & _.Loai == Loai).Select(_ => new
            //{
            //    _.Id,
            //    _.Ma,
            //    _.Ten,
            //    _.TuNgay,
            //    _.DenNgay,
            //    _.TrangThaiId
            //}).ToList();

            var handover = (from p in db.ml_KhachHangs
                        join kh in db.ml_KeHoaches on p.KeHoachId equals kh.Id
                        join k in db.tnKhachHangs on p.MaKH equals k.MaKH
                        where kh.MaTN == maTn & SqlMethods.DateDiffDay(tuNgay, p.TuNgay) >= 0 & SqlMethods.DateDiffDay(p.DenNgay, denNgay) >= 0 & kh.Loai == Loai
                        select new
                        {
                            p.Id,
                            TenKH = k.IsCaNhan == true ? k.TenKH : k.CtyTen,
                            p.TuNgay,
                            p.DenNgay,
                            TrangThaiId = p.IsDongY.GetValueOrDefault() == true ? 2: 1,
                            kh.Ma,
                            kh.Ten,
                            DaGui = p.IsDaGui.GetValueOrDefault() == true ? "Đã gửi" : "Chưa gửi",
                            DongY = p.IsDongY.GetValueOrDefault() == true ? "Đã đồng ý": "Chưa đồng ý",
                            p.NguoiLienHe, p.ChucVu, p.SDT, p.GhiChu
                        }).ToList();

            // get user

            List<string> list = new List<string>();
            var user = (from p in handover group new { p } by new { p.Ma } into g select new { UserName = g.Key.Ma }).ToList();
            foreach (var item in user) list.Add(item.UserName);
            String[] Users = list.ToArray();


            schedulerDataStorage1.Appointments.DataSource = handover;
            schedulerDataStorage1.Appointments.Statuses.Clear();

            foreach (var i in colorStatus)
                schedulerDataStorage1.Appointments.Statuses.Add(Color.FromArgb(int.Parse(i.Color.ToString())), i.Ten);

            schedulerDataStorage1.Appointments.Labels.Clear();
            foreach (var i in colorStatus)
                schedulerDataStorage1.Appointments.Labels.Add(Color.FromArgb(int.Parse(i.Color.ToString())), i.Ten);

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

                var obj = handover.Where(_ => _.Ma.ToLower() == resource.Caption.ToLower()).ToList();
                foreach (var io in obj)
                {
                    Appointment apt = schedulerDataStorage1.CreateAppointment(AppointmentType.Pattern);
                    apt.Subject = string.Format("Khách hàng: {0}", io.TenKH);
                    apt.ResourceId = resource.Id;
                    apt.Start = (DateTime)io.TuNgay;
                    apt.End = (DateTime)io.DenNgay;
                    apt.StatusId = (int)io.TrangThaiId;
                    apt.LabelId = (int)io.TrangThaiId;
                    apt.Description = string.Format("- Đã gửi: {0} \n- Đồng ý: {1} \n- Người liên hệ: {2} \n- Số điện thoại: {3} \n- Chức vụ", io.DaGui, io.DongY, io.NguoiLienHe, io.SDT, io.ChucVu);
                    //apt.Location = io.Ma;

                    //apt.RecurrenceInfo.Type = RecurrenceType.Monthly;
                    //apt.RecurrenceInfo.Start = apt.Start;
                    //apt.RecurrenceInfo.Periodicity = 5;
                    //apt.RecurrenceInfo.Range = RecurrenceRange.EndByDate;
                    //apt.RecurrenceInfo.End = apt.RecurrenceInfo.Start.AddMonths(1);

                    apt.RecurrenceInfo.Type = RecurrenceType.Daily;
                    apt.RecurrenceInfo.Start = (DateTime)io.TuNgay;
                    apt.RecurrenceInfo.Periodicity = 0;
                    apt.RecurrenceInfo.Range = RecurrenceRange.EndByDate;
                    apt.RecurrenceInfo.End = (DateTime)io.DenNgay;
                    schedulerDataStorage1.Appointments.Add(apt);
                }

            }


            #endregion

            //searchControl1.Client = schedulerControl1; // why error?
            ListViewHelper.AddListView(schedulerControl1, dateNavigator, searchControl1);
        }

        #endregion


        private void ItemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            schedulerControl1.Start = (DateTime) itemTuNgay.EditValue;
            LoadData();
        }



        private void cbxKbc_EditValueChanged(object sender, EventArgs e)
        {

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