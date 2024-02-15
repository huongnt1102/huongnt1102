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

namespace LichBaoTri.Lich
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