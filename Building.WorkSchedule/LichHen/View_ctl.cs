using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraScheduler;
using Library;

namespace Building.WorkSchedule.LichHen
{
    public partial class View_ctl : DevExpress.XtraEditors.XtraUserControl
    {
        bool IsAdd = false, IsEdit = false, IsDelete = false;
        bool IsZoom = true;
        DevExpress.Utils.SuperToolTip superToolTip2 = new DevExpress.Utils.SuperToolTip();
        DevExpress.Utils.ToolTipItem toolTipItem2 = new DevExpress.Utils.ToolTipItem();
        int MaNC = 0;
        public tnNhanVien objNV;
        public long? MaDMCV { get; set; }
        public long? MaCVNV { get; set; }
        MasterDataContext db;

        public View_ctl(long? _MaDMCV, long? _MaCVNV)
        {
            InitializeComponent();
            db = new MasterDataContext();
            MaDMCV = _MaDMCV;
            MaCVNV = _MaCVNV;
        }

        public View_ctl()
        {
            InitializeComponent();
            db = new MasterDataContext();
        }

       public void LoadData()
        {
          //  DialogBox.ShowWaitDialog("Vui lòng đợi trong giây lát...", "Hệ thống đang xử lý");
            try
            {
                //Library.LichHenCls o = new Library.LichHenCls();
                //o.NgayBD = DateTime.Now;
                //o.NgayKT = DateTime.Now;
                //o.ChuDe.TenCD = "%%";
                //o.ThoiDiem.TenTD = "%%";
                
                if (MaDMCV != null)
                {
                    if (objNV.IsSuperAdmin.Value)
                    {
                        this.schedulerStorage1.Appointments.DataSource = db.LichHen_getByDateV2(DateTime.Now, DateTime.Now, "%%", "%%", objNV.MaNV, MaDMCV);
                    }
                    else
                    {
                        //o.NhanVien.MaNV = objNV.MaNV;
                        this.schedulerStorage1.Appointments.DataSource = db.LichHen_getAllByMaNVV2(DateTime.Now, DateTime.Now,objNV.MaNV, "%%", "%%",MaDMCV);
                    }
                }
               
                schedulerStorage1.Appointments.Statuses.Clear();
                DataTable tblTable1 = Library.Commoncls.Table("Select * from LichHen_ThoiDiem order by STT");
                foreach (DataRow r1 in tblTable1.Rows)
                    schedulerStorage1.Appointments.Statuses.Add(Color.FromArgb((int)r1["MaTD"]), r1["TenTD"].ToString());

                schedulerStorage1.Appointments.Labels.Clear();
                DataTable tblTable = Library.Commoncls.Table("select * from LichHen_ChuDe order by STT");
                foreach (DataRow r in tblTable.Rows)
                    schedulerStorage1.Appointments.Labels.Add(Color.FromArgb((int)r["MaCD"]), r["TenCD"].ToString());

                this.schedulerStorage1.Appointments.Mappings.Start = "NgayBD";
                this.schedulerStorage1.Appointments.Mappings.End = "NgayKT";
                this.schedulerStorage1.Appointments.Mappings.Subject = "TieuDe";
                this.schedulerStorage1.Appointments.Mappings.Description = "DienGiai";
                this.schedulerStorage1.Appointments.Mappings.Label = "LabelId";
                this.schedulerStorage1.Appointments.Mappings.Location = "HoTenKH";
                this.schedulerStorage1.Appointments.Mappings.Status = "StatusId";
                this.schedulerStorage1.Appointments.CustomFieldMappings.Add(new AppointmentCustomFieldMapping("StatusId", "StatusId"));
                this.schedulerStorage1.Appointments.CustomFieldMappings.Add(new AppointmentCustomFieldMapping("LabelId", "LabelId"));
                this.schedulerStorage1.Appointments.CustomFieldMappings.Add(new AppointmentCustomFieldMapping("MaLH", "MaLH"));
                this.schedulerStorage1.Appointments.CustomFieldMappings.Add(new AppointmentCustomFieldMapping("MaNC", "MaNC"));
                this.schedulerStorage1.Appointments.CustomFieldMappings.Add(new AppointmentCustomFieldMapping("MaKH", "MaKH"));
                this.schedulerStorage1.Appointments.CustomFieldMappings.Add(new AppointmentCustomFieldMapping("HoTenKH", "HoTenKH"));
                this.schedulerStorage1.Appointments.CustomFieldMappings.Add(new AppointmentCustomFieldMapping("TenNVu", "TenNVu"));
                this.schedulerStorage1.Appointments.CustomFieldMappings.Add(new AppointmentCustomFieldMapping("MaNVu", "MaNVu"));

                schedulerStorage1.EnableReminders = true;

                //o = null;
                System.GC.Collect();
            }
            catch { }
        }

        private void Sheduler_LichHen_ctl_Load(object sender, EventArgs e)
        { 
            schedulerControl1.Start = DateTime.Now;
            LoadData();
        }

        private void schedulerControl1_EditAppointmentFormShowing(object sender, AppointmentFormEventArgs e)
        {
            Appointment apt = e.Appointment;

            bool openRecurrenceForm = apt.IsRecurring && schedulerStorage1.Appointments.IsNewAppointment(apt);
            AddNew_frm f = new AddNew_frm((SchedulerControl)sender, apt, true, objNV, MaDMCV, MaCVNV);
            f.IsEdit = true;// IsEdit;
            f.IsAdd = true;// IsAdd;
            f.LookAndFeel.ParentLookAndFeel = this.LookAndFeel.ParentLookAndFeel;
            //f.ShowDialog();
            e.DialogResult = f.ShowDialog();
            e.Handled = true;

            if (f.IsUpdate)
                LoadData();
        }

        private void schedulerControl1_AppointmentDrop(object sender, AppointmentDragEventArgs e)
        {
            //if (XtraMessageBox.Show("Dữ liệu có thay đổi bạn có muốn lưu lại không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //{
            try
            {
                Appointment _New = (Appointment)e.EditedAppointment;
                Library.LichHenCls o = new Library.LichHenCls();
                o.NgayBD = _New.Start;
                o.NgayKT = _New.End;
                o.MaLH = int.Parse(_New.CustomFields["MaLH"].ToString());
                o.UpdateTime();
                // Load_Data();
            }
            catch { }
            //}
        }

        private void schedulerControl1_AppointmentResized(object sender, AppointmentResizeEventArgs e)
        {
            try
            {
                Appointment _New = (Appointment)e.EditedAppointment;
                Library.LichHenCls o = new Library.LichHenCls();
                o.NgayBD = _New.Start;
                o.NgayKT = _New.End;
                o.MaLH = int.Parse(_New.CustomFields["MaLH"].ToString());
                o.UpdateTime();
            }
            catch { }
        }

        private void schedulerStorage1_AppointmentsChanged(object sender, PersistentObjectsEventArgs e)
        {
            //if (XtraMessageBox.Show("Dữ liệu có thay đổi bạn có muốn lưu lại không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //{
            try
            {
                Appointment app = (Appointment)e.Objects[0];
                Library.LichHenCls o = new Library.LichHenCls();
                o.TieuDe = app.Subject;
                o.MaLH = int.Parse(app.CustomFields["MaLH"].ToString());
                o.ThoiDiem.STT = (byte)app.StatusId;
                o.ChuDe.STT = (byte)app.LabelId;
                o.UpdateSubject();
                //Load_Data();
            }
            catch { }
            //}
        }

        private void schedulerControl1_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            //var menu = new DevExpress.Utils.Menu.DXMenuItem("View: Cơ hội", itemSearch_ItemClick, imageCollection1.Images[3]);
            //menu.BeginGroup = true;
            //e.Menu.Items.Add(menu);

            switch (e.Menu.Id)
            {
                case SchedulerMenuItemId.DefaultMenu:
                    SchedulerMenuItem add = e.Menu.GetMenuItemById(SchedulerMenuItemId.NewAppointment);
                    if (add != null)
                    {
                        add.Caption = "Thêm lịch hẹn";
                        //add.Image
                    }
                    if(!IsAdd)
                        e.Menu.RemoveMenuItem(SchedulerMenuItemId.NewAppointment);

                    e.Menu.RemoveMenuItem(SchedulerMenuItemId.NewRecurringEvent);
                    e.Menu.RemoveMenuItem(SchedulerMenuItemId.GotoToday);
                    e.Menu.RemoveMenuItem(SchedulerMenuItemId.GotoDate);
                    e.Menu.RemoveMenuItem(SchedulerMenuItemId.GotoThisDay);
                    e.Menu.RemoveMenuItem(SchedulerMenuItemId.NewAllDayEvent);
                    e.Menu.RemoveMenuItem(SchedulerMenuItemId.NewRecurringAppointment);
                    e.Menu.RemoveMenuItem(SchedulerMenuItemId.TimeScaleEnable);
                    e.Menu.RemoveMenuItem(SchedulerMenuItemId.TimeScaleVisible);
                    //e.Menu.RemoveMenuItem(SchedulerMenuItemId.SwitchViewMenu);
                    SchedulerPopupMenu switchs = e.Menu.GetPopupMenuById(SchedulerMenuItemId.SwitchViewMenu);
                    if (switchs != null)
                    {
                        switchs.Caption = "Kiểu lịch";
                        switchs.Items[0].Caption = "Lịch ngày";
                        switchs.Items[1].Caption = "Lịch tuần làm việc";
                        switchs.Items[2].Caption = "Lịch tuần";
                        switchs.Items[3].Caption = "Lịch tháng";
                        switchs.Items[4].Caption = "Lịch dòng thời gian";
                    }
                    break;
                case SchedulerMenuItemId.AppointmentMenu:
                    // Find the "Label As" item of the appointment popup menu and corresponding submenu.        
                    SchedulerPopupMenu label = e.Menu.GetPopupMenuById(SchedulerMenuItemId.LabelSubMenu);
                    if (label != null)
                    {
                        // Rename the item of the appointment popup menu.             
                        label.Caption = "Loại lịch hẹn";
                        // Rename the first item of the submenu.            
                        //submenu.Items[0].Caption = "Label 1";      
                    }
                    
                    // Find the "Status As" item of the appointment popup menu and corresponding submenu.        
                    SchedulerPopupMenu status = e.Menu.GetPopupMenuById(SchedulerMenuItemId.StatusSubMenu);
                    if (status != null)
                        status.Caption = "Thời điểm liên hệ";

                    SchedulerMenuItem open = e.Menu.GetMenuItemById(SchedulerMenuItemId.OpenAppointment);
                    if (open != null)
                        open.Caption = "Xem thông tin";

                    SchedulerMenuItem delete = e.Menu.GetMenuItemById(SchedulerMenuItemId.DeleteAppointment);
                    if (delete != null)
                        delete.Caption = "Xóa lịch hẹn";
                    if(!IsDelete)
                        e.Menu.RemoveMenuItem(SchedulerMenuItemId.DeleteAppointment);

                    if (!IsEdit)
                    {
                        e.Menu.RemoveMenuItem(SchedulerMenuItemId.LabelSubMenu);
                        e.Menu.RemoveMenuItem(SchedulerMenuItemId.StatusSubMenu);
                        e.Menu.RemoveMenuItem(SchedulerMenuItemId.OpenAppointment);
                    }
                    break;
                case SchedulerMenuItemId.AppointmentDragMenu:
                    //SchedulerMenuItem cancel = e.Menu.GetMenuItemById(SchedulerMenuItemId.AppointmentDragCancel);
                    //if (cancel != null)
                    //    cancel.Caption = "Bỏ qua";

                    //SchedulerMenuItem copy = e.Menu.GetMenuItemById(SchedulerMenuItemId.AppointmentDragCopy);
                    //if (copy != null)
                    //    copy.Caption = "Sao chép";

                    //SchedulerMenuItem move = e.Menu.GetMenuItemById(SchedulerMenuItemId.AppointmentDragMove);
                    //if (move != null)
                    //    move.Caption = "Di chuyển";
                    e.Menu.RemoveMenuItem(SchedulerMenuItemId.AppointmentDragCancel);
                    e.Menu.RemoveMenuItem(SchedulerMenuItemId.AppointmentDragCopy);
                    e.Menu.RemoveMenuItem(SchedulerMenuItemId.AppointmentDragMove);
                    break;
            }
        }

        private void btnNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void schedulerStorage1_AppointmentDeleting(object sender, PersistentObjectCancelEventArgs e)
        {
            if (!IsDelete)
                e.Cancel = true;
            else
            {
                if (DialogBox.QuestionDelete() == DialogResult.Yes)
                {
                    try
                    {
                        Appointment app = (Appointment)e.Object;
                        Library.LichHenCls o = new Library.LichHenCls();
                        o.MaLH = int.Parse(app.CustomFields["MaLH"].ToString());
                        o.Delete();
                        LoadData();
                    }
                    catch {
                        DialogBox.Alert("Xóa không thành công vì lịch hẹn này đã được sử dụng. Vui lòng kiểm tra lại, xin cảm ơn.");
                    }
                }
                else
                    e.Cancel = true;
            }
        }

        private void schedulerStorage1_AppointmentChanging(object sender, PersistentObjectCancelEventArgs e)
        {
            if (!IsEdit)
                e.Cancel = true;
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void itemZoom_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //CRM_frm frm = (CRM_frm)this.ParentForm;
            //frm.ZoomScheduler();
            //if (IsZoom)
            //{
            //    itemZoom.ImageIndex = 2;
            //    IsZoom = false;
            //    toolTipItem2.LeftIndent = 6;
            //    toolTipItem2.Text = "Thu nhỏ lịch hẹn";
            //    superToolTip2.Items.Add(toolTipItem2);
            //}
            //else
            //{
            //    itemZoom.ImageIndex = 1;
            //    IsZoom = true;
            //    toolTipItem2.LeftIndent = 6;
            //    toolTipItem2.Text = "Phóng to lịch hẹn";
            //    superToolTip2.Items.Add(toolTipItem2);
            //}
            //this.itemZoom.SuperTip = superToolTip2;
        }

        private void schedulerControl1_SelectionChanged(object sender, EventArgs e)
        {
            //Appointment selectedApt;
            //if (this.schedulerControl1.SelectedAppointments.Count == 1)
            //{
            //    selectedApt = this.schedulerControl1.SelectedAppointments[0];
            //    DataRowView row = (DataRowView)selectedApt.GetSourceObject(this.schedulerStorage1);

            //    if (row["MaNC"].ToString() != "")
            //        MaNC = Convert.ToInt32(row["MaNC"]);
            //    else
            //        MaNC = 0;
            //}
            //else
            //    MaNC = 0;
        }

        private void itemSearch_ItemClick(object sender, EventArgs e)
        {
            //if (MaNC != 0)
            //{
            //    var f = new Need.frmSearch();
            //    f.MaNC = MaNC;
            //    f.ShowDialog();
            //}
            //else
            //    DialogBox.Alert("[Lịch hẹn] này không kèm theo cơ hội.\r\nVui lòng kiểm tra lại, xin cảm ơn.");
        }
    }
}