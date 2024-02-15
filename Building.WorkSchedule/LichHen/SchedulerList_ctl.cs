using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Data.Linq.SqlClient;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraScheduler;
using Library;
using System.Linq;

namespace Building.WorkSchedule.LichHen
{
    public partial class SchedulerList_ctl : DevExpress.XtraEditors.XtraUserControl
    {
        public tnNhanVien objNV;
        public int maLH;
        public int maNVu;
        public int maNC;
        public int maKH;
        public int maHD;
        public string hoTenKH;
        bool KT = false, KT1 = false;
        bool IsAdd = true, IsEdit = true, IsDelete = true;
        int MaNC = 0;
        public long? MaDMCV { get; set; }
        public long? MaCVNV { get; set; }
        public SchedulerList_ctl()
        {
            InitializeComponent();
        }

        int ThangDauCuaQuy(int Thang)
        {
            if (Thang <= 3)
                return 1;
            else if (Thang <= 6)
                return 4;
            else if (Thang <= 9)
                return 7;
            else
                return 10;
        }

        void SetToDate()
        {
            KT = false;
            KT1 = false;
            dateDenNgay.Enabled = false;
            dateTuNgay.Enabled = false;
            DateTime dateHachToan = DateTime.Now.Date;
            switch (cmbKyBC.SelectedIndex)
            {
                case 0: //Ngay nay
                    dateDenNgay.DateTime = dateHachToan;
                    dateTuNgay.DateTime = dateHachToan;

                    break;
                case 1: //Tuan nay
                    dateDenNgay.DateTime = dateHachToan.AddDays(7 - (int)dateHachToan.DayOfWeek);
                    dateTuNgay.DateTime = dateHachToan.AddDays(1 - (int)dateHachToan.DayOfWeek);

                    break;
                case 2: //Dau tuan den hien tai
                    dateDenNgay.DateTime = dateHachToan;
                    dateTuNgay.DateTime = dateHachToan.AddDays(1 - (int)dateHachToan.DayOfWeek);

                    break;
                case 3: //Thang nay
                    dateDenNgay.DateTime = new DateTime(dateHachToan.Year, dateHachToan.Month, 1).AddMonths(1).AddDays(-1);
                    dateTuNgay.DateTime = new DateTime(dateHachToan.Year, dateHachToan.Month, 1);

                    break;
                case 4: //Dau thang den hien tai
                    dateDenNgay.DateTime = dateHachToan;
                    dateTuNgay.DateTime = new DateTime(dateHachToan.Year, dateHachToan.Month, 1);

                    break;
                case 5: //Quy nay
                    dateDenNgay.DateTime = new DateTime(dateHachToan.Year, ThangDauCuaQuy(dateHachToan.Month) + 2, 1).AddMonths(1).AddDays(-1);
                    dateTuNgay.DateTime = new DateTime(dateHachToan.Year, ThangDauCuaQuy(dateHachToan.Month), 1);

                    break;
                case 6: //Dau quy den hien tai
                    dateDenNgay.DateTime = dateHachToan;
                    dateTuNgay.DateTime = new DateTime(dateHachToan.Year, ThangDauCuaQuy(dateHachToan.Month), 1);

                    break;
                case 7: //Nam nay
                    dateDenNgay.DateTime = new DateTime(dateHachToan.Year, 12, 31);
                    dateTuNgay.DateTime = new DateTime(dateHachToan.Year, 1, 1);

                    break;
                case 8: //Dau nam den hien tai
                    dateDenNgay.DateTime = dateHachToan;
                    dateTuNgay.DateTime = new DateTime(dateHachToan.Year, 1, 1);

                    break;
                case 9: //Thang 1
                    dateDenNgay.DateTime = new DateTime(dateHachToan.Year, 2, 1).AddDays(-1);
                    dateTuNgay.DateTime = new DateTime(dateHachToan.Year, 1, 1);

                    break;
                case 10: //Thang 2
                    dateDenNgay.DateTime = new DateTime(dateHachToan.Year, 3, 1).AddDays(-1);
                    dateTuNgay.DateTime = new DateTime(dateHachToan.Year, 2, 1);

                    break;
                case 11: //Thang 3
                    dateDenNgay.DateTime = new DateTime(dateHachToan.Year, 4, 1).AddDays(-1);
                    dateTuNgay.DateTime = new DateTime(dateHachToan.Year, 3, 1);

                    break;
                case 12: //Thang 4
                    dateDenNgay.DateTime = new DateTime(dateHachToan.Year, 5, 1).AddDays(-1);
                    dateTuNgay.DateTime = new DateTime(dateHachToan.Year, 4, 1);

                    break;
                case 13: //Thang 5
                    dateDenNgay.DateTime = new DateTime(dateHachToan.Year, 6, 1).AddDays(-1);
                    dateTuNgay.DateTime = new DateTime(dateHachToan.Year, 5, 1);

                    break;
                case 14: //Thang 6
                    dateDenNgay.DateTime = new DateTime(dateHachToan.Year, 7, 1).AddDays(-1);
                    dateTuNgay.DateTime = new DateTime(dateHachToan.Year, 6, 1);

                    break;
                case 15: //Thang 7
                    dateDenNgay.DateTime = new DateTime(dateHachToan.Year, 8, 1).AddDays(-1);
                    dateTuNgay.DateTime = new DateTime(dateHachToan.Year, 7, 1);

                    break;
                case 16: //Thang 8
                    dateDenNgay.DateTime = new DateTime(dateHachToan.Year, 9, 1).AddDays(-1);
                    dateTuNgay.DateTime = new DateTime(dateHachToan.Year, 8, 1);

                    break;
                case 17: //Thang 9
                    dateDenNgay.DateTime = new DateTime(dateHachToan.Year, 10, 1).AddDays(-1);
                    dateTuNgay.DateTime = new DateTime(dateHachToan.Year, 9, 1);

                    break;
                case 18: //Thang 10
                    dateDenNgay.DateTime = new DateTime(dateHachToan.Year, 11, 1).AddDays(-1);
                    dateTuNgay.DateTime = new DateTime(dateHachToan.Year, 10, 1);

                    break;
                case 19: //Thang 11
                    dateDenNgay.DateTime = new DateTime(dateHachToan.Year, 12, 1).AddDays(-1);
                    dateTuNgay.DateTime = new DateTime(dateHachToan.Year, 11, 1);

                    break;
                case 20: //Thang 12
                    dateDenNgay.DateTime = new DateTime(dateHachToan.Year, 12, 31);
                    dateTuNgay.DateTime = new DateTime(dateHachToan.Year, 12, 1);

                    break;
                case 21: //Quy I
                    dateDenNgay.DateTime = new DateTime(dateHachToan.Year, 4, 1).AddDays(-1);
                    dateTuNgay.DateTime = new DateTime(dateHachToan.Year, 1, 1);

                    break;
                case 22: //Quy II
                    dateDenNgay.DateTime = new DateTime(dateHachToan.Year, 7, 1).AddDays(-1);
                    dateTuNgay.DateTime = new DateTime(dateHachToan.Year, 4, 1);

                    break;
                case 23: //Quy III
                    dateDenNgay.DateTime = new DateTime(dateHachToan.Year, 10, 1).AddDays(-1);
                    dateTuNgay.DateTime = new DateTime(dateHachToan.Year, 7, 1);

                    break;
                case 24: //Quy IV
                    dateDenNgay.DateTime = new DateTime(dateHachToan.Year, 12, 31);
                    dateTuNgay.DateTime = new DateTime(dateHachToan.Year, 10, 1);

                    break;
                case 25: //Tuan truoc
                    dateDenNgay.DateTime = dateHachToan.AddDays(-(int)dateHachToan.DayOfWeek);
                    dateTuNgay.DateTime = dateHachToan.AddDays(-(int)dateHachToan.DayOfWeek - 6);

                    break;
                case 26: //Thang truoc
                    dateDenNgay.DateTime = new DateTime(dateHachToan.Year, dateHachToan.Month, 1).AddDays(-1);
                    dateTuNgay.DateTime = new DateTime(dateHachToan.Year, dateHachToan.Month, 1).AddMonths(-1);

                    break;
                case 27: //Quy truoc
                    dateDenNgay.DateTime = new DateTime(dateHachToan.Year, ThangDauCuaQuy(dateHachToan.Month), 1).AddDays(-1);
                    dateTuNgay.DateTime = new DateTime(dateHachToan.Year, ThangDauCuaQuy(dateHachToan.Month), 1).AddMonths(-3);

                    break;
                case 28: //Nam truoc
                    dateDenNgay.DateTime = new DateTime(dateHachToan.Year - 1, 12, 31);
                    dateTuNgay.DateTime = new DateTime(dateHachToan.Year - 1, 1, 1);

                    break;
                case 29: //Tuan sau
                    dateDenNgay.DateTime = dateHachToan.AddDays(14 - (int)dateHachToan.DayOfWeek);
                    dateTuNgay.DateTime = dateHachToan.AddDays(8 - (int)dateHachToan.DayOfWeek);

                    break;
                case 30: //Bon tuan sau
                    dateDenNgay.DateTime = dateHachToan.AddDays(35 - (int)dateHachToan.DayOfWeek);
                    dateTuNgay.DateTime = dateHachToan.AddDays(8 - (int)dateHachToan.DayOfWeek);

                    break;
                case 31: //Thang sau
                    dateDenNgay.DateTime = new DateTime(dateHachToan.Year, dateHachToan.Month, 1).AddMonths(2).AddDays(-1);
                    dateTuNgay.DateTime = new DateTime(dateHachToan.Year, dateHachToan.Month, 1).AddMonths(1);

                    break;
                case 32: //Quy sau
                    switch (ThangDauCuaQuy(dateHachToan.Month))
                    {
                        case 10:
                            dateDenNgay.DateTime = new DateTime(dateHachToan.Year + 1, 4, 1).AddDays(-1);
                            dateTuNgay.DateTime = new DateTime(dateHachToan.Year + 1, 1, 1);
                            break;

                        case 1:
                            dateDenNgay.DateTime = new DateTime(dateHachToan.Year, 7, 1).AddDays(-1);
                            dateTuNgay.DateTime = new DateTime(dateHachToan.Year, 4, 1);
                            break;
                        case 4:

                            dateDenNgay.DateTime = new DateTime(dateHachToan.Year, 10, 1).AddDays(-1);
                            dateTuNgay.DateTime = new DateTime(dateHachToan.Year, 7, 1);
                            break;
                        case 7:

                            dateDenNgay.DateTime = new DateTime(dateHachToan.Year, 12, 31);
                            dateTuNgay.DateTime = new DateTime(dateHachToan.Year, 10, 1);
                            break;
                    }
                    break;

                case 33: //Nam sau
                    dateDenNgay.DateTime = new DateTime(dateHachToan.Year + 1, 12, 31);
                    dateTuNgay.DateTime = new DateTime(dateHachToan.Year + 1, 1, 1);

                    break;
                case 34: //Tu chon
                    dateDenNgay.Enabled = true;
                    dateTuNgay.Enabled = true;
                    KT = true;
                    KT1 = true;
                    dateDenNgay.DateTime = dateHachToan;
                    dateTuNgay.DateTime = dateHachToan;

                    break;
            }
        }

        void LoadData()
        {
            var wait = DialogBox.WaitingForm();
            try
            {
                //Library.LichHenCls o = new Library.LichHenCls();
                //o.NgayBD = dateTuNgay.DateTime;
                //o.NgayKT = dateDenNgay.DateTime;
                //o.ChuDe.TenCD = lookUpChuDe.Text == "<Tất cả>" ? "%%" : lookUpChuDe.EditValue.ToString();
                //o.ThoiDiem.TenTD = lookUpThoiDiem.Text == "<Tất cả>" ? "%%" : lookUpThoiDiem.EditValue.ToString();
                //o.NhanVien.MaNV = lookUpNhanVien.Text == "<Tất cả>" ? 0 : Convert.ToInt32(lookUpNhanVien.EditValue);
                //var tbl = o.SelectAll();
                //this.schedulerStorage1.Appointments.DataSource = tbl;
                //gcScheduler.DataSource = tbl;

                //schedulerStorage1.Appointments.Statuses.Clear();
                //DataTable tblTable1 = Library.Commoncls.Table("Select * from LichHen_ThoiDiem order by STT");
                //foreach (DataRow r1 in tblTable1.Rows)
                //    schedulerStorage1.Appointments.Statuses.Add(Color.FromArgb((int)r1["MaTD"]), r1["TenTD"].ToString());

                //schedulerStorage1.Appointments.Labels.Clear();
                //DataTable tblTable = Library.Commoncls.Table("select * from LichHen_ChuDe order by STT");
                //foreach (DataRow r in tblTable.Rows)
                //    schedulerStorage1.Appointments.Labels.Add(Color.FromArgb((int)r["MaCD"]), r["TenCD"].ToString());

                MasterDataContext db = new MasterDataContext();
                var ltLichHen = (from lh in db.LichHens
                                 join cd in db.LichHen_ChuDes on lh.MaCD equals cd.MaCD into chuDe
                                 from cd in chuDe.DefaultIfEmpty()
                                 join td in db.LichHen_ThoiDiems on lh.MaTD equals td.MaTD into thoiDiem
                                 from td in thoiDiem.DefaultIfEmpty()
                                 join kh in db.tnKhachHangs on lh.MaKH equals kh.MaKH into khachHang
                                 from kh in khachHang.DefaultIfEmpty()
                                 join nv in db.tnNhanViens on lh.MaNV equals nv.MaNV into nhanVien
                                 from nv in nhanVien.DefaultIfEmpty()
                                 where SqlMethods.DateDiffDay(dateTuNgay.DateTime, lh.NgayBD) >= 0 & SqlMethods.DateDiffDay(lh.NgayBD, dateDenNgay.DateTime) >= 0
                                 & nv.MaTN == Common.User.MaTN & ((int)lookUpThoiDiem.EditValue == 0 || lh.MaTD == (int)lookUpThoiDiem.EditValue)
                                 & (lookUpNhanVien.EditValue ==null || lh.MaNV == (int)lookUpNhanVien.EditValue)
                                    //& (arrCongTy.Contains(nv.MaCT.ToString()) == true | strCongTy == "")
                                    //& (lh.MaCD == maCD | maCD == 0) & (lh.MaTD == maTD | maTD == 0) & (lh.MaNV == maNVCHON | maNVCHON == 0)
                                    //& ((nv.MaPB == maPB | maPB == 0) & (nv.MaNKD == MaNKD | MaNKD == 0) & (nv.MaNV == maNV | maNV == 0)
                                   || (from xem in db.LichHen_tnNhanViens where xem.MaLH == lh.MaLH select xem.MaNV.ToString()).Contains(lookUpNhanVien.EditValue.ToString())
                                    // ==true
                                 orderby lh.NgayBD descending
                                 select new
                                 {
                                     lh.MaLH,
                                     lh.TieuDe,
                                     lh.DienGiai,
                                     nv.HoTenNV,
                                     lh.NgayBD,
                                     lh.NgayKT,
                                     LabelID = cd.STT,
                                     StatusID = td.STT,
                                     HoTenKH = kh.HoKH,
                                     lh.DiaDiem,
                                     NguoiLienQuan = String.Join(", ", db.LichHen_tnNhanViens.Where(o=>o.MaLH == lh.MaLH).Select(o=>o.tnNhanVien.HoTenNV).ToArray()),
                                 })
                                 .AsEnumerable()
                                 .Select((lh, index) => new
                                 {
                                     STT = index + 1,
                                     lh.MaLH,
                                     lh.TieuDe,
                                     lh.HoTenNV,
                                     lh.DienGiai,
                                     lh.NgayBD,
                                     lh.NgayKT,
                                     lh.LabelID,
                                     lh.StatusID,
                                     lh.HoTenKH,
                                     lh.DiaDiem,
                                     lh.NguoiLienQuan,
                                 })
                                 .ToList();

                  gcScheduler.DataSource = ltLichHen;
                schedulerStorage1.Appointments.DataSource = ltLichHen;

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
            }
            catch { }
            finally
            {
                wait.Close();
                System.GC.Collect();
            }
        }

        void LoadDictionary()
        {
            Library.LichHen_ChuDeCls objCD = new Library.LichHen_ChuDeCls();
            lookUpChuDe.Properties.DataSource = objCD.SelectAll();
            lookUpChuDe.ItemIndex = 0;
            objCD = null;

            Library.LichHen_ThoiDiemCls objTD = new Library.LichHen_ThoiDiemCls();
            lookUpThoiDiem.Properties.DataSource = objTD.SelectAll();
            lookUpThoiDiem.ItemIndex = 0;
            objTD = null;

           // Library.NhanVienCls objNV = new Library.NhanVienCls();
         //   lookUpNhanVien.Properties.DataSource = 
            using (var db = new MasterDataContext())
            {
                lookUpNhanVien.Properties.DataSource = db.tnNhanViens.Where(p=>p.MaTN == Common.User.MaTN).Select(p => new { p.MaNV, p.HoTenNV, p.NgaySinh });
            }
            lookUpNhanVien.ItemIndex = 0;
        }

        private void Sheduler_LichHen_ctl_Load(object sender, EventArgs e)
        {
            LoadDictionary();
            cmbKyBC.SelectedIndex = 3;            
            LoadData();
        }

        private void schedulerControl1_EditAppointmentFormShowing(object sender, AppointmentFormEventArgs e)
        {
            Appointment apt = e.Appointment;
            objNV = Common.User;
            bool openRecurrenceForm = apt.IsRecurring && schedulerStorage1.Appointments.IsNewAppointment(apt);

            AddNew_frm f = new AddNew_frm((SchedulerControl)sender, apt, openRecurrenceForm, objNV, MaDMCV, MaCVNV);
            f.IsEdit = true;// IsEdit;
            f.IsAdd = IsAdd;
            f.objNV = objNV;
            f.LookAndFeel.ParentLookAndFeel = this.LookAndFeel.ParentLookAndFeel;
            //e.DialogResult = 
                f.ShowDialog();
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

        private void cmbKyBC_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetToDate();
            try
            {
                schedulerControl1.Start = dateTuNgay.DateTime;
            }
            catch { }
        }

        private void schedulerControl1_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            //var menu = new DevExpress.Utils.Menu.DXMenuItem("View: Cơ hội", itemSearch2_ItemClick, imageCollection1.Images[1]);
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
                        MasterDataContext db = new MasterDataContext();
                        Appointment app = (Appointment)e.Object;
                        var o =
                            db.LichHens.SingleOrDefault(p => p.MaLH == int.Parse(app.CustomFields["MaLH"].ToString()));
                        if (o != null)
                        {
                            db.LichHens.DeleteOnSubmit(o);
                            db.SubmitChanges();
                            LoadData();
                        }
                       
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

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            schedulerControl1.Start = dateTuNgay.DateTime;
            LoadData();
        }

        private void itemSearch2_ItemClick(object sender, EventArgs e)
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

        private void itemSearch_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //var f = new Need.frmSearch();
            //f.MaNC = (int?)gvScheduler.GetFocusedRowCellValue("MaNC");
            //f.ShowDialog();
        }

        private void schedulerControl1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                Appointment selectedApt;
                if (this.schedulerControl1.SelectedAppointments.Count == 1)
                {
                    selectedApt = this.schedulerControl1.SelectedAppointments[0];
                    DataRowView row = (DataRowView)selectedApt.GetSourceObject(this.schedulerStorage1);

                    if (row["MaNC"].ToString() != "")
                        MaNC = Convert.ToInt32(row["MaNC"]);
                    else
                        MaNC = 0;
                }
                else
                    MaNC = 0;
            }
            catch { }
        }

        private void itemThem_Click(object sender, EventArgs e)
        {
            LichHen.AddNew_frm frm = new LichHen.AddNew_frm( maLH, maNVu, maKH, maNC,  maHD, hoTenKH);
            frm.objNV = objNV;
            frm.ShowDialog();
            if (frm.IsUpdate)
                LoadData();
        }
    }
}
