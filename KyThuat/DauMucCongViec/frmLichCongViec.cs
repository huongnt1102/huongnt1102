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

namespace KyThuat.DauMucCongViec
{
    public partial class frmLichCongViec : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public tnNhanVien objnhanvien;
        btDauMucCongViec obj;
        bool KT = false, KT1 = false;

        public frmLichCongViec()
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
                var ID = (long)scheduler.CustomFields["ID"];
                obj = db.btDauMucCongViecs.SingleOrDefault(p => p.ID == ID);
                if (obj != null)
                {
                    txtViTri.Text = string.Format("Dự án: {0} \r\n Khối nhà:{1} \r\n Tầng lầu:{2} \r\n Mặt bằng: {3}", obj.MaTN == null ? "" : obj.tnToaNha.TenTN, obj.MaKN == null ? "" : obj.mbKhoiNha.TenKN, obj.MaTL == null ? "" : obj.mbTangLau.TenTL, obj.MaMB == null ? "" : obj.mbMatBang.MaSoMB);
                    txtNoiDung.Text = obj.MoTa;
                    gcNhanVien.DataSource = db.btDauMucCongViec_NhanViens.Where(p => p.MaCVBT == obj.ID)
                        .Select(p => new { 
                            p.tnNhanVien.HoTenNV,
                            p.NgayGiaoViec,
                            p.ThoiGianTH,
                            p.ThoiGianHetHan,
                            p.TienDo,
                            p.TieuDe
                        });
                    gcThietBi.DataSource = db.btDauMucCongViec_ThietBis.Where(p => p.MaCVBT == obj.ID);
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

            var tuNgay = (DateTime?)itemTuNgay.EditValue;
            var denNgay = (DateTime?)itemDenNgay.EditValue;
            var maTN = itemToaNha.EditValue == null ? 0 : Convert.ToInt32(itemToaNha.EditValue);
            int NguonCV = (itemNguonCV.EditValue == "Yêu cầu khách hàng" ? 0 : (itemNguonCV.EditValue == "Hệ thống vận hành" ? 1 : (itemNguonCV.EditValue == "Lịch bảo trì tài sản cố định" ? 2 : -1)));
            int MaTT = itemTrangThai.EditValue == null ? 0 : Convert.ToInt32(itemTrangThai.EditValue);
            var obj = db.btDauMucCongViecs.Where(p => SqlMethods.DateDiffDay(tuNgay, p.ThoiGianTheoLich.Value) >= 0 & SqlMethods.DateDiffDay(p.ThoiGianTheoLich.Value, denNgay) >= 0 &
                (p.MaTN == maTN || maTN == 0) & (p.NguonCV == NguonCV || NguonCV == -1) & (p.TrangThaiCV == MaTT || MaTT == 0))
                .Select(p => new
                {
                    p.ID,
                    p.ThoiGianTheoLich,
                    p.ThoiGianHetHan,
                    p.TrangThaiCV,
                    p.TienDoTH,
                    STT = p.TrangThaiCV == null ? 0 : p.btCongViecBT_trangThai.STT,
                    TenTT = p.TrangThaiCV == null ? "" : p.btCongViecBT_trangThai.TenTT,
                    TieuDe = string.Format("{0:#,0.#} % - {1}", p.TienDoTH.GetValueOrDefault(), p.MoTa),
                    ViTri = string.Format("Dự án:{0} - Khối nhà:{1} - Tầng lầu:{2} - Mặt bằng: {3}",
                    p.MaTN == null? "":p.tnToaNha.TenTN, p.MaKN == null?"":p.mbKhoiNha.TenKN, p.MaTL==null? "": p.mbTangLau.TenTL, p.MaMB == null?"":p.mbMatBang.MaSoMB),

                }).ToList() ;
            this.schedulerStorage1.Appointments.DataSource = obj;
            schedulerStorage1.Appointments.Labels.Clear();
            var objTT=db.btCongViecBT_trangThais.ToList();
            gcNote.DataSource = objTT;
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
                    case 4:
                        schedulerStorage1.Appointments.Labels.Add(Color.Red, r.TenTT.ToString());
                        break;
                    case 5:
                        schedulerStorage1.Appointments.Labels.Add(Color.BlueViolet, r.TenTT.ToString());
                        break;
                    case 6:
                        schedulerStorage1.Appointments.Labels.Add(Color.Green, r.TenTT.ToString());
                        break;
                        
                }
            }

            this.schedulerStorage1.Appointments.Mappings.AppointmentId = "ID";
            this.schedulerStorage1.Appointments.Mappings.Start = "ThoiGianTheoLich";
            this.schedulerStorage1.Appointments.Mappings.End = "ThoiGianHetHan";
            this.schedulerStorage1.Appointments.Mappings.Subject = "TieuDe";
            this.schedulerStorage1.Appointments.Mappings.Description = "TieuDe";
            this.schedulerStorage1.Appointments.Mappings.Label = "STT";
            this.schedulerStorage1.Appointments.Mappings.Location = "ViTri";
            this.schedulerStorage1.Appointments.Mappings.Status = "TienDoTH";
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
            lookTrangThai.DataSource = db.btCongViecBT_trangThais;
            lookToaNha.DataSource = db.tnToaNhas.Select(p => new { p.MaTN, p.TenTN });
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
                obj = db.btDauMucCongViecs.SingleOrDefault(p => p.ID == ID);
                if (obj != null)
                {
                    var objLS = new btDauMucCongViec_LichSu();
                    objLS.MaNVCN = objnhanvien.MaNV;
                    objLS.NgayCN = DateTime.Now;
                    objLS.TienDo = obj.TienDoTH;
                    objLS.TrangThaiCV = obj.TrangThaiCV;
                    objLS.DienGiai = "Cập nhật vật tư sử dụng.";
                    db.btDauMucCongViec_LichSus.InsertOnSubmit(objLS);
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
                    SchedulerPopupMenu label = e.Menu.GetPopupMenuById(SchedulerMenuItemId.LabelSubMenu);
                    if (label != null)
                    {           
                        label.Caption = "Loại lịch hẹn";
                        // Rename the first item of the submenu.            
                        //submenu.Items[0].Caption = "Label 1";      
                    }

                    break;
                case SchedulerMenuItemId.AppointmentDragMenu:
                    e.Menu.RemoveMenuItem(SchedulerMenuItemId.AppointmentDragCancel);
                    e.Menu.RemoveMenuItem(SchedulerMenuItemId.AppointmentDragCopy);
                    e.Menu.RemoveMenuItem(SchedulerMenuItemId.AppointmentDragMove);
                    break;
            }
        }

        private void gvNote_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            try
            {
                if (e.RowHandle < 0) return;

                if (e.Column == colTrangThai)
                {
                    switch ((int?)gvNote.GetRowCellValue(e.RowHandle, "STT"))
                    {
                        case 0:
                            e.Appearance.BackColor = Color.Chocolate;
                            break;
                        case 1:
                            e.Appearance.BackColor = Color.Cyan;
                            break;
                        case 2:
                            e.Appearance.BackColor = Color.LightPink;
                            break;
                        case 3:
                            e.Appearance.BackColor = Color.SpringGreen;
                            break;
                        case 4:
                            e.Appearance.BackColor = Color.Red;
                            break;
                        case 5:
                            e.Appearance.BackColor = Color.BlueViolet;
                            break;
                        case 6:
                            e.Appearance.BackColor = Color.Green;
                            break;
                    }
                }
            }
            catch { }
        }

        private void schedulerControl1_AppointmentResized(object sender, AppointmentResizeEventArgs e)
        {
            try
            {
                Appointment _New = (Appointment)e.EditedAppointment;
                obj.ThoiGianTheoLich = _New.Start;
                obj.ThoiGianHetHan = _New.End;
                db.SubmitChanges();
            }
            catch { }
        }

        private void itemPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           // schedulerControl1.OptionsPrint.PrintStyle = DevExpress.XtraScheduler.Printing.SchedulerPrintStyleKind.Monthly;
            schedulerControl1.ShowPrintOptionsForm();
        }

    }
}