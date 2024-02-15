using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Linq.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraScheduler;
using DevExpress.XtraScheduler.UI;
using System.Linq;
using Library;

namespace Building.WorkSchedule.LichHen
{
    public partial class AddNew_frm : DevExpress.XtraEditors.XtraForm
    {
        public tnNhanVien objNV;
        public Library.LichHen o;
        public int? MaLH { get; set; }
        public bool IsUpdate = false, IsEdit = true, IsAdd = true;
        public int MaNC = 0, MaHD = 0, MaKH = 0, MaNVu = 0;
        public string NhiemVu = "", KhachHang = "", CoHoi = "", HopDong = "";
        public long? MaDMCV { get; set; }
        public long? MaCVNV { get; set; }

        SchedulerControl control;
        Appointment apt;
        bool openRecurrenceForm = false;
        int suspendUpdateCount;
        MyAppointmentFormController controller;
        protected AppointmentStorage Appointments { get { return control.Storage.Appointments; } }

        public AddNew_frm(int? maLH, int? maNVu, int? maKH, int? maNC, int? maHD, string hoTenKH)
        {
            InitializeComponent();

            this.control = new SchedulerControl();
            this.control.Storage = new SchedulerStorage();
            this.control.Storage.Appointments.CustomFieldMappings.Add(new AppointmentCustomFieldMapping("MaLH", "MaLH"));
            this.control.Storage.Appointments.CustomFieldMappings.Add(new AppointmentCustomFieldMapping("MaKH", "MaKH"));
            this.control.Storage.Appointments.CustomFieldMappings.Add(new AppointmentCustomFieldMapping("MaNC", "MaNC"));
            this.control.Storage.Appointments.CustomFieldMappings.Add(new AppointmentCustomFieldMapping("MaHD", "MaHD"));
            this.control.Storage.Appointments.CustomFieldMappings.Add(new AppointmentCustomFieldMapping("MaNVu", "MaNVu"));
            this.control.Storage.Appointments.CustomFieldMappings.Add(new AppointmentCustomFieldMapping("HoTenKH", "HoTenKH"));
            this.apt = this.control.Storage.CreateAppointment(AppointmentType.Normal);
            if (maLH != null)
            {
                this.apt.CustomFields["MaLH"] = maLH;
                MaNC = maNC ?? 0;
                MaHD = maHD ?? 0;
                MaKH = maKH ?? 0;
                MaNVu = maNVu ?? 0;
                KhachHang = hoTenKH;
            }
            else
            {
                this.apt.CustomFields["MaKH"] = maKH;
                this.apt.CustomFields["MaNC"] = maNC;
                this.apt.CustomFields["MaHD"] = maHD;
                this.apt.CustomFields["MaNVu"] = maNVu;
                MaNC = maNC ?? 0;
                MaHD = maHD ?? 0;
                MaKH = maKH ?? 0;
                MaNVu = maNVu ?? 0;
                KhachHang = hoTenKH;
                this.apt.CustomFields["HoTenKH"] = hoTenKH;
                this.apt.Start = DateTime.Now;
                this.apt.End = DateTime.Now.AddDays(1);
            }
            this.control.Storage.Appointments.Add(this.apt);
            this.controller = new MyAppointmentFormController(control, apt);

            this.control.Storage.Appointments.Statuses.Clear();
            DataTable tblTable1 = Library.Commoncls.Table("Select * from LichHen_ThoiDiem order by STT");
            foreach (DataRow r1 in tblTable1.Rows)
                this.control.Storage.Appointments.Statuses.Add(Color.FromArgb((int)r1["MaTD"]), r1["TenTD"].ToString());
            this.control.Storage.Appointments.Labels.Clear();
            DataTable tblTable = Library.Commoncls.Table("select * from LichHen_ChuDe order by STT");
            foreach (DataRow r in tblTable.Rows)
                this.control.Storage.Appointments.Labels.Add(Color.FromArgb((int)r["MaCD"]), r["TenCD"].ToString());
        }

        public AddNew_frm(SchedulerControl control, Appointment apt, bool openRecurrenceForm, tnNhanVien _objnv, long? _MaDMCV, long? _MaCVNV)
        {
            InitializeComponent();

			this.openRecurrenceForm = openRecurrenceForm;
			this.controller = new MyAppointmentFormController(control, apt);
			this.apt = apt;
			this.control = control;
            this.MaCVNV = _MaCVNV;
            this.MaDMCV = _MaDMCV;
            this.objNV = _objnv;
		}

        void LoadDictionary()
        {
            lookUpRemine.Properties.DataSource = Library.Commoncls.Table("select * from Times");
            lookUpRemine.ItemIndex = 0;
            lookUpRepeat.Properties.DataSource = Library.Commoncls.Table("select * from Times");
            lookUpRepeat.ItemIndex = 0;
        }

        void LoadData()
        {
            //var db = new Library.MasterDataContext();
            try
            {
                if (this.MaLH != null && this.MaLH != 0)
                {
                    var objLH = db.LichHens.Single(p => p.MaLH == this.MaLH);
                    o = db.LichHens.Single(p => p.MaLH == MaLH);
                    lblNguoiCapNhat.Caption = string.Format("Người cập nhật: {0} | {1:dd/MM/yyyy}", objNV.HoTenNV, DateTime.Now);
                    lblNguoiTao.Caption = string.Format("Người tạo: {0} | {1:dd/MM/yyyy}", objLH.tnNhanVien.HoTenNV, objLH.NgayBD);
                    txtTieuDe.EditValue = objLH.TieuDe;
                    txtDienGiai.EditValue = objLH.DienGiai;
                    txtDiaDiem.EditValue = objLH.DiaDiem;
                    dateNgayBD.EditValue = objLH.NgayBD;
                    dateNgayKT.EditValue = objLH.NgayKT;

                    lookUpStatus.Storage = control.Storage;
                    lookUpLabel.Storage = control.Storage;
                    if (objLH.MaCD != null)
                    {
                        lookUpStatus.Status = Appointments.Statuses[objLH.LichHen_ThoiDiem.STT.GetValueOrDefault()];
                    }

                    if (objLH.MaTD != null)
                    {
                        lookUpLabel.Label = Appointments.Labels[objLH.LichHen_ChuDe.STT.GetValueOrDefault()];
                    }


                    btnRing.Tag = objLH.Rings;
                    if (objLH.NhiemVu != null)
                    {
                        btnNhiemVu.Tag = objLH.MaNVu;
                        btnNhiemVu.Text = objLH.NhiemVu;
                    }
                    else
                    {
                        btnNhiemVu.Text = "";
                    }
                    if (objLH.MaKH != null && objLH.MaKH != 0)
                    {
                        var objKH = db.tnKhachHangs.FirstOrDefault(p => p.MaKH == objLH.MaKH);
                        btnKhachHang.Tag = objLH.MaKH;
                        btnKhachHang.EditValue = objKH.HoKH + " " + objKH.TenKH;
                    }
                    else
                    {
                        btnKhachHang.Text = "";
                    }

                    chkRemine.EditValue = objLH.IsNhac;
                    chkIsRepeat.EditValue = objLH.IsRepeat;
                    if (objLH.IsNhac.GetValueOrDefault())
                    {
                        lookUpRemine.Enabled = true;
                        lookUpRemine.EditValue = objLH.TimeID;
                    }
                    else
                        lookUpRemine.Enabled = false;
                    if (objLH.IsRepeat.GetValueOrDefault())
                    {
                        lookUpRepeat.Enabled = true;
                        lookUpRepeat.EditValue = objLH.TimeID;
                    }
                    else
                        lookUpRepeat.Enabled = false;
                    MaNC = objLH.MaNC ?? 0;
                    MaHD = objLH.MaHD ?? 0;
                    MaKH = objLH.MaKH ?? 0;
                    MaNVu = objLH.MaNVu ?? 0;

                    if (MaNC != 0)
                    {
                        lblCateName.Text = "Cơ hội";
                        btnCategory.Text = objLH.ncNhuCau;
                    }
                    else
                    {
                        if (MaHD != 0)
                        {
                            lblCateName.Text = "Hợp đồng";
                            //btnCategory.Text = objLH.cContract.ContractName;
                        }
                        else
                        {
                            btnCategory.Visible = false;
                            lblCateName.Visible = false;
                        }
                    }

                    string nv = "";
                    foreach (var i in objLH.LichHen_tnNhanViens)
                    {
                        nv += i.MaNV + "; ";
                    }
                    nv = nv.TrimEnd(' ').TrimEnd(';');
                    cmbNhanVien.SetEditValue(nv);
                }
            }
            catch (Exception ex)
            {
                //DialogBox.Error(ex.Message);
            }
            finally
            {
                //db.Dispose();
            }
        }

        int GetLabel(int _MaCD)
        {
            Library.LichHen_ChuDeCls o = new Library.LichHen_ChuDeCls(_MaCD);
            return o.STT;
        }

        int GetStatus(int _MaTD)
        {
            Library.LichHen_ThoiDiemCls o = new Library.LichHen_ThoiDiemCls(_MaTD);
            return o.STT;
        }

        string GetNhiemVu(int _MaNVu)
        {
            Library.NhiemVuCls o = new Library.NhiemVuCls(_MaNVu);
            o.MaNVu = _MaNVu;
            return o.GetNhiemVu();
        }

        string GetCustumer(int _MaKH)
        {
            Library.KhachHangCls o = new Library.KhachHangCls();
            o.MaKH = _MaKH;
            return "";// o.GetCustomer();
        }

        private void AddNew_frm_Load(object sender, EventArgs e)
        {
            LoadDictionary();
            lookUpLabel.Storage = control.Storage;
            lookUpStatus.Storage = control.Storage;

            using (var db = new Library.MasterDataContext())
            {
                cmbNhanVien.Properties.DataSource = db.tnNhanViens.Where(p => p.MaNV != Common.User.MaNV & p.MaTN == Common.User.MaTN).Select(p => new { p.MaNV, HoTen = p.HoTenNV }).ToList();
            }

            if (this.apt.CustomFields["MaLH"] != null)
            {
                MaLH = int.Parse(this.apt.CustomFields["MaLH"].ToString());
                if (!IsEdit)
                    this.Close();
                LoadData();
            }
            else
            {
                if (!IsAdd)
                    this.Close();
                dateNgayBD.DateTime = apt.Start;
                dateNgayKT.DateTime = apt.End.AddDays(-1).AddHours(3);
                lblThoiHan.Visible = false;
                lookUpLabel.SelectedIndex = 0;
                lookUpStatus.SelectedIndex = 0;
                lookUpRemine.Enabled = false;
                lblNguoiCapNhat.Caption = string.Format("Người cập nhật: {0} | {1:dd/MM/yyyy}", objNV.HoTenNV, DateTime.Now);
                lblNguoiTao.Caption = string.Format("Người tạo: {0} | {1:dd/MM/yyyy}", objNV.HoTenNV, DateTime.Now);
                lookUpRemine.Enabled = false;
                lookUpRepeat.Enabled = false;
                //if (this.apt.CustomFields["MaKH"] != null)
                //{
                //    btnKhachHang.Tag = this.apt.CustomFields["MaKH"];
                //    btnKhachHang.Text = this.apt.CustomFields["HoTenKH"].ToString();
                //}
                if (MaKH != 0)
                    btnKhachHang.Text = KhachHang;

                if (MaNVu != 0)
                    btnNhiemVu.Text = NhiemVu;

                if (MaNC != 0)
                {
                    lblCateName.Text = "Cơ hội";
                    btnCategory.Text = CoHoi;
                }
                else
                {
                    if (MaHD != 0)
                    {
                        lblCateName.Text = "Hợp đồng";
                        btnCategory.Text = HopDong;
                    }
                    else
                    {
                        btnCategory.Visible = false;
                        lblCateName.Visible = false;
                        btnNhiemVu.Width = 666;
                    }
                }
            }
        }

        public class MyAppointmentFormController : AppointmentFormController
        {
            public string CustomName { get { return (string)EditedAppointmentCopy.CustomFields["CustomName"]; } set { EditedAppointmentCopy.CustomFields["CustomName"] = value; } }
            public string CustomStatus { get { return (string)EditedAppointmentCopy.CustomFields["CustomStatus"]; } set { EditedAppointmentCopy.CustomFields["CustomStatus"] = value; } }

            string SourceCustomName { get { return (string)SourceAppointment.CustomFields["CustomName"]; } set { SourceAppointment.CustomFields["CustomName"] = value; } }
            string SourceCustomStatus { get { return (string)SourceAppointment.CustomFields["CustomStatus"]; } set { SourceAppointment.CustomFields["CustomStatus"] = value; } }

            public MyAppointmentFormController(SchedulerControl control, Appointment apt)
                : base(control, apt)
            {
            }

            public override bool IsAppointmentChanged()
            {
                if (base.IsAppointmentChanged())
                    return true;
                return SourceCustomName != CustomName ||
                    SourceCustomStatus != CustomStatus;
            }

            protected override void ApplyCustomFieldsValues()
            {
                SourceCustomName = CustomName;
                SourceCustomStatus = CustomStatus;
            }
        }

        private void btnKhachHang_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 0)
            {
                var frm = new DichVu.KhachHang.frmFind();
                frm.ShowDialog();
                if (frm.MaKH != 0)
                {
                    btnKhachHang.Tag = frm.MaKH;
                    btnKhachHang.Text = frm.HoTen;
                    MaKH = frm.MaKH;
                }
            }
            else
            {
                btnKhachHang.Tag = 0;
                btnKhachHang.Text = "";
                MaKH = 0;
            }
        }

        private void chkRemine_CheckedChanged(object sender, EventArgs e)
        {
            CheckEdit _New = (CheckEdit)sender;
            if (_New.Checked)
                lookUpRemine.Enabled = true;
            else
                lookUpRemine.Enabled = false;
        }

        private void txtTieuDe_EditValueChanged(object sender, EventArgs e)
        {
            TextEdit _New = (TextEdit)sender;
            this.Text = _New.Text + " - Lịch hẹn";
        }
        MasterDataContext db = new MasterDataContext();
        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtTieuDe.Text.Trim() == "")
                {
                    DialogBox.Alert("Vui lòng nhập chủ đề. Xin cảm ơn.");
                    txtTieuDe.Focus();
                    return;
                }
                if (SqlMethods.DateDiffSecond(dateNgayBD.DateTime, dateNgayKT.DateTime) < 0)
                {
                    DialogBox.Error("Khoảng thời gian không hợp lệ. Vui lòng kiểm tra lại");
                    dateNgayKT.Focus();
                    return;
                }
                string[] nv = cmbNhanVien.EditValue != null ? cmbNhanVien.EditValue.ToString().Split(';') : null;
                if (this.MaLH == null || this.MaLH == 0)
                {
                    o = new Library.LichHen();
                    o.MaNV = Common.User.MaNV;
                    db.LichHens.InsertOnSubmit(o);
                }
                else
                {
                    if (nv != null)
                    {
                        foreach (var i in o.LichHen_tnNhanViens)
                        {
                            if (nv.Where(p => p == i.MaNV.ToString()).Count() <= 0)
                            {
                                db.LichHen_tnNhanViens.DeleteOnSubmit(i);
                            }
                        }
                    }
                }
                //Library.LichHenCls o = new Library.LichHenCls();
                //o.tnKhachHang = db.tnKhachHangs.Single(p => p.MaKH == MaKH);
                o.MaKH = MaKH;
                o.TieuDe = txtTieuDe.Text;
                o.DienGiai = txtDienGiai.Text;
                o.DiaDiem = txtDiaDiem.Text;
                o.NgayBD = dateNgayBD.DateTime;
                o.NgayKT = dateNgayKT.DateTime;
                o.MaNVu = MaNVu;
                o.NhiemVu = btnNhiemVu.Text;
                o.IsNhac = chkRemine.Checked;
                o.TimeID = byte.Parse(lookUpRemine.EditValue.ToString());
                o.TimeID2 = (byte?)lookUpRepeat.EditValue;
                o.Rings = (btnRing.Tag as string) ?? "";

                if (chkRemine.Checked == true)
                {
                    var obj = db.Times.Single(p => p.TimeID == (byte?)lookUpRemine.EditValue);
                    o.NgayNhac = dateNgayBD.DateTime.AddMinutes(-obj.Minutes.GetValueOrDefault());
                    o.IsNhac = true;
                }
                else
                {
                    var obj = db.Times.Single(p => p.TimeID == (byte?)lookUpRemine.EditValue);
                    o.NgayNhac = null;
                    o.IsNhac = false;
                }
                //try
                //{
                //    o.NhiemVu.MaNVu = MaNVu;
                //}
                //catch { o.NhiemVu.MaNVu = 0; }
                //o.ChuDe.TenCD = lookUpLabel.EditValue.ToString();
                //o.ChuDe.MaCD = o.ChuDe.GetID();
                //o.ThoiDiem.TenTD = lookUpStatus.EditValue.ToString();
                //o.ThoiDiem.MaTD = o.ThoiDiem.GetID();
                //if (btnRing.Tag != null)
                //    o.Rings = btnRing.Tag.ToString();
                //else
                //    o.Rings = "";
                o.IsRepeat = chkIsRepeat.Checked;

                o.MaNC = MaNC;
                o.MaHD = MaHD;
                //Luu nhan vien co mat trong table Lichhen_tnnhanvien
                if (nv[0] != "")
                {
                    foreach (var i in nv)
                    {
                        if (o.LichHen_tnNhanViens.Where(p => p.MaNV.ToString() == i & p.MaNV != o.MaNV).Count() <= 0)
                        {
                            var objNV = new LichHen_tnNhanVien();
                            objNV.MaNV = int.Parse(i.ToString());
                            objNV.DaNhac = false;
                            objNV.IsMain = true;
                            objNV.IsNhac = true;
                            objNV.NgayNhac = o.NgayNhac;
                            o.LichHen_tnNhanViens.Add(objNV);
                        }
                    }
                }



                //shechdolure
                //lookUpLabel.Label = Appointments.Labels[controller.LabelId];
                //lookUpStatus.Status = Appointments.Statuses[controller.StatusId];  
                controller.Start = dateNgayBD.DateTime;
                controller.End = dateNgayKT.DateTime;

                controller.Description = txtDienGiai.Text;
                controller.Subject = txtTieuDe.Text;

                //Cap nhat NhanVu_NhanVien
                //Library.NhiemVu_NhanVienCls obj;
                //if (_ListNhanVien.Count > 0)
                //{
                //    foreach (int s in _ListNhanVien)
                //    {
                //        obj = new Library.NhiemVu_NhanVienCls();
                //        obj.MaNVu = MaNVu;
                //        obj.MaNV = s;
                //        obj.Insert();
                //    }
                //}
                if (lookUpLabel.EditValue != null)
                {
                    var maCD = lookUpLabel.Label.Color.ToArgb();
                    o.LichHen_ChuDe = db.LichHen_ChuDes.SingleOrDefault(p => p.MaCD == maCD);
                    controller.SetLabel(lookUpLabel.Label);
                }
                else
                {
                    o.MaCD = null;
                }
                if (lookUpStatus.EditValue != null)
                {
                    var maTD = lookUpStatus.Status.Color.ToArgb();
                    o.LichHen_ThoiDiem = db.LichHen_ThoiDiems.SingleOrDefault(p => p.MaTD == maTD);
                    controller.SetStatus(lookUpStatus.Status);
                }
                else
                {
                    o.MaTD = null;
                }

                //o.MaNVu = (int?)btnNhiemVu.Tag;
                db.SubmitChanges();


                // Tạo nhắc lịch hẹn cho nhân viên
                if (!db.LichHen_tnNhanViens.Any(p => p.MaNV == o.MaNV & p.MaLH == o.MaLH))
                {
                    var lhnv = new LichHen_tnNhanVien();
                    lhnv.MaNV = (int)o.MaNV;
                    lhnv.DaNhac = false;
                    lhnv.IsMain = true;
                    lhnv.IsNhac = true;
                    lhnv.NgayNhac = o.NgayNhac;
                    o.LichHen_tnNhanViens.Add(lhnv);
                    db.SubmitChanges();
                }

                controller.ApplyChanges();
                IsUpdate = true;
                DialogBox.Alert("Dữ liệu đã được cập nhật");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (System.Exception ex)
            {
                Library.DialogBox.Error("Đã bị lỗi: " + ex);}
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRing_Click(object sender, EventArgs e)
        {
            NhiemVu.Rings_frm frm = new NhiemVu.Rings_frm();
            if (btnRing.Tag != null)
                frm.Rings = btnRing.Tag.ToString();
            frm.ShowDialog();
            if (frm.IsUpdate)
                btnRing.Tag = frm.Rings;
        }

        private void chkIsRepeat_CheckedChanged(object sender, EventArgs e)
        {
            CheckEdit _New = (CheckEdit)sender;
            if (_New.Checked)
                lookUpRepeat.Enabled = true;
            else
                lookUpRepeat.Enabled = false;
        }

        private void btnNhiemVu_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 0)
            {
                NhiemVu.Select_frm frm = new Building.WorkSchedule.NhiemVu.Select_frm();
                frm.ShowDialog();
                if (frm.MaNVu != 0)
                {
                    MaNVu = frm.MaNVu;
                    btnNhiemVu.Text = frm.TieuDe;
                }
            }
            else
            {
                MaNVu = 0;
                btnNhiemVu.Text = "";
            }
        }

        private void txtDienGiai_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}