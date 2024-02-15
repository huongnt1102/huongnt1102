using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using Library;
using System.Data.Linq.SqlClient;
using DevExpress.XtraScheduler;
using DevExpress.XtraScheduler.Reporting;

namespace KyThuat.NhiemVu
{
    public partial class frmLichLamViecNV : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public tnNhanVien objnhanvien;
        btDauMucCongViec_NhanVien obj;
        bool KT = false, KT1 = false;

        public frmLichLamViecNV()
        {
            InitializeComponent();
            db = new MasterDataContext();
            cmbKyBC.EditValueChanged += new EventHandler(cmbKyBC_EditValueChanged);
            schedulerControl1.EditAppointmentFormShowing += new DevExpress.XtraScheduler.AppointmentFormEventHandler(schedulerControl1_EditAppointmentFormShowing);
           // schedulerControl1_EditAppointmentFormShowing
        }

        void Click()
        {
            try
            {
                var scheduler = schedulerControl1.SelectedAppointments[0];                
                var ID = Convert.ToInt32( scheduler.CustomFields["ID"]);
                obj = db.btDauMucCongViec_NhanViens.SingleOrDefault(p => p.ID == ID);
                if (obj != null)
                {
                    txtViTri.Text = obj.ViTri;
                    txtNoiDung.Text = obj.NoiDungCV;
                    gcNhanVien.DataSource = db.btDauMucCongViec_NhanViens.Where(p => p.MaCVBT == obj.MaCVBT)
                        .Select(p => new { 
                            p.tnNhanVien.HoTenNV,
                            p.NgayGiaoViec,
                            p.ThoiGianTH,
                            p.ThoiGianHetHan,
                            p.TienDo,
                            p.TieuDe
                        });
                    gcThietBi.DataSource = db.btDauMucCongViec_ThietBis.Where(p => p.MaCVBT == obj.MaCVBT);
                }
            }
            catch
            {
                txtViTri.Text = null;
                txtNoiDung.Text = null;
                gcNhanVien.DataSource = null;
                gcThietBi.DataSource = null;
            }
        }

        void schedulerControl1_EditAppointmentFormShowing(object sender, DevExpress.XtraScheduler.AppointmentFormEventArgs e)
        {
            Appointment apt = e.Appointment;

            bool openRecurrenceForm = apt.IsRecurring && schedulerStorage1.Appointments.IsNewAppointment(apt);

            AddNew_frm f = new AddNew_frm((SchedulerControl)sender, apt, openRecurrenceForm, objnhanvien);
            f.IsEdit = true;// IsEdit;
            f.IsAdd = true;
            f.objNV = objnhanvien;
            f.LookAndFeel.ParentLookAndFeel = this.LookAndFeel.ParentLookAndFeel;
            e.DialogResult = f.ShowDialog();
            e.Handled = true;

            if (f.IsUpdate)
            {
                LoadData();
                Click();
            }
        }

        void LoadData()
        {
            db = new MasterDataContext();
            colMaTB.ColumnEdit = new RepositoryItemPopupContainerEditLoaiTaiSan(objnhanvien);
            if (!(bool)objnhanvien.IsSuperAdmin)
                itemNhanVien.EditValue = objnhanvien.MaNV;
            itemNhanVien.Enabled = (bool)objnhanvien.IsSuperAdmin.GetValueOrDefault();

            var tuNgay = (DateTime?)itemTuNgay.EditValue;
            var denNgay = (DateTime?)itemDenNgay.EditValue;
            int MaNV = itemNhanVien.EditValue != null ? Convert.ToInt32(itemNhanVien.EditValue) : 0;

            var obj = db.btDauMucCongViec_NhanViens.Where(p => SqlMethods.DateDiffDay(tuNgay, p.ThoiGianTH.Value) >= 0
                & SqlMethods.DateDiffDay(p.ThoiGianTH.Value, denNgay) >= 0 & (MaNV == 0 | p.MaNVTH == MaNV))
                .Select(p => new
                {
                    p.ID,
                    p.MaNVGiaoViec,
                    p.MaNVTH,
                    p.ThoiGianTH,
                    p.ThoiGianHetHan,
                    p.btDauMucCongViec_TrangThaiCVNV.STT,
                    p.btDauMucCongViec_TrangThaiCVNV.TenTT,
                    p.ViTri,
                    p.NoiDungCV,
                    p.ThoiGianHT,
                    p.TienDo,
                    TieuDe = string.Format("({0:#,0.#} %) - {1}", p.TienDo.GetValueOrDefault(), p.TieuDe)
                })
                .ToList();
            this.schedulerStorage1.Appointments.DataSource = obj;
            schedulerStorage1.Appointments.Labels.Clear();
            var objTT=db.btDauMucCongViec_TrangThaiCVNVs.ToList();
            foreach (var r in objTT)
            {
                switch (r.STT)
                {
                    case 0:
                        schedulerStorage1.Appointments.Labels.Add(Color.Chocolate, r.TenTT.ToString());
                        break;
                    case 1:
                        schedulerStorage1.Appointments.Labels.Add(Color.Cyan, r.TenTT.ToString());
                        break;
                    case 2:
                        schedulerStorage1.Appointments.Labels.Add(Color.LightPink, r.TenTT.ToString());
                        break;
                    case 3:
                        schedulerStorage1.Appointments.Labels.Add(Color.SpringGreen, r.TenTT.ToString());
                        break;
                }
            }

            this.schedulerStorage1.Appointments.Mappings.AppointmentId = "ID";
            this.schedulerStorage1.Appointments.Mappings.Start = "ThoiGianTH";
            this.schedulerStorage1.Appointments.Mappings.End = "ThoiGianHetHan";
            this.schedulerStorage1.Appointments.Mappings.Subject = "TieuDe";
            this.schedulerStorage1.Appointments.Mappings.Description = "NoiDungCV";
            this.schedulerStorage1.Appointments.Mappings.Label = "STT";
            this.schedulerStorage1.Appointments.Mappings.Location = "ViTri";
            this.schedulerStorage1.Appointments.Mappings.Status = "TienDo";
            this.schedulerStorage1.Appointments.CustomFieldMappings.Add(new AppointmentCustomFieldMapping("LabelId", "STT"));
            this.schedulerStorage1.Appointments.CustomFieldMappings.Add(new AppointmentCustomFieldMapping("ID", "ID"));
        }

        void cmbKyBC_EditValueChanged(object sender, EventArgs e)
        {
            SetDate((sender as ComboBoxEdit).SelectedIndex);
        }

        void SetDate(int index)
        {
            KyBaoCao objKBC = new KyBaoCao();
            objKBC.Index = index;
            objKBC.SetToDate();

            itemTuNgay.EditValueChanged -= new EventHandler(itemTuNgay_EditValueChanged);
            itemTuNgay.EditValue = objKBC.DateFrom;
            itemDenNgay.EditValue = objKBC.DateTo;
            itemTuNgay.EditValueChanged += new EventHandler(itemTuNgay_EditValueChanged);
        }

        private void frmLichLamViecNV_Load(object sender, EventArgs e)
        {
        //    Library.HeThongCls.PhanQuyenCls.Authorize(this, objnhanvien, barManager1);
            lookNhanVien.DataSource = db.tnNhanViens
                .Select(p => new
                {
                    p.MaNV,
                    p.HoTenNV,
                    PhongBan = p.MaPB == null ? "" : p.tnPhongBan.TenPB,
                    ChucVu = p.MaCV == null ? "" : p.tnChucVu.TenCV
                });
            KyBaoCao objKBC = new KyBaoCao();
            foreach (string str in objKBC.Source)
                cmbKyBC.Items.Add(str);
            itemKyBC.EditValue = objKBC.Source[7];
            SetDate(7);
            
        }

        private void itemTuNgay_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void itemDenNgay_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void itemNhanVien_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void schedulerControl1_Click(object sender, EventArgs e)
        {
            Click();
        }

        private void grvThietBi_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "SoLuong" | e.Column.FieldName == "DonGia")
            {
                if (grvThietBi.GetFocusedRowCellValue("SoLuong") != null &&
                    grvThietBi.GetFocusedRowCellValue("DonGia") != null)
                {
                    decimal ThanhTien = (int)grvThietBi.GetFocusedRowCellValue("SoLuong") *
                        (decimal)grvThietBi.GetFocusedRowCellValue("DonGia");
                    grvThietBi.SetFocusedRowCellValue("ThanhTien", ThanhTien);
                }
            }
        }

        private void btnThemTB_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            grvThietBi.AddNewRow();
        }

        private void btnXoaTB_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            grvThietBi.DeleteSelectedRows();
        }

        private void btnLuuTB_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                var scheduler = schedulerControl1.SelectedAppointments[0];
                var ID = Convert.ToInt32(scheduler.CustomFields["ID"]);
                obj = db.btDauMucCongViec_NhanViens.SingleOrDefault(p => p.ID == ID);
                if (obj != null)
                {
                    var objLS = new btDauMucCongViec_NhanVienLSTH();
                    objLS.MaNV = objnhanvien.MaNV;
                    objLS.NgayTH = DateTime.Now;
                    objLS.TienDo = obj.TienDo;
                    objLS.TrangThai = obj.TrangThai;
                    objLS.DienGiai = "Cập nhật vật tư sử dụng.";
                    db.btDauMucCongViec_NhanVienLSTHs.InsertOnSubmit(objLS);
                    db.SubmitChanges();
                    DialogBox.Alert("Dữ liệu đã cập nhật thành công!");
                }
            }
            catch
            {
                DialogBox.Error("Dữ liệu không thể cập nhật!");
            }
        }

        private void schedulerControl1_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            //var menu = new DevExpress.Utils.Menu.DXMenuItem("View: Cơ hội", itemSearch_ItemClick, imageCollection1.Images[3]);
            //menu.BeginGroup = true;
            //e.Menu.Items.Add(menu);

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

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            schedulerControl1.ShowPrintOptionsForm();
        }
    }
}