using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraScheduler;
using DevExpress.XtraScheduler.UI;
using System.Linq;
using Library;

namespace KyThuat.DauMucCongViec
{
    public partial class AddNew_frm : DevExpress.XtraEditors.XtraForm
    {
        public tnNhanVien objNV;
        btDauMucCongViec objCV;
        MasterDataContext db;
        public int MaLH = 0;
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

        public AddNew_frm(SchedulerControl control, Appointment apt, bool openRecurrenceForm, tnNhanVien _objnv)
        {
            InitializeComponent();
            db = new MasterDataContext();
			this.openRecurrenceForm = openRecurrenceForm;
			this.controller = new MyAppointmentFormController(control, apt);
			this.apt = apt;
			this.control = control;
            this.objNV = _objnv;
		}

        void LoadData()
        {
            try
            {
                objCV = db.btDauMucCongViecs.SingleOrDefault(p => p.ID == Convert.ToInt32(apt.CustomFields["ID"]));
                txtTieuDe.Text = apt.Subject.Substring(apt.Subject.IndexOf("-") + 2);
                txtDienGiai.Text = apt.Description.Substring(apt.Description.IndexOf("-") + 2);
                txtDiaDiem.Text = apt.Location;
                lookUpLabel.Storage = control.Storage;
                lookUpLabel.Label = Appointments.Labels[controller.LabelId];
                dateNgayBD.DateTime = apt.Start;
                dateNgayKT.DateTime = apt.End;
                if (objCV != null)
                {
                    spinTienDo.EditValue = (decimal?)objCV.TienDoTH;
                   // lblThoiHan.Text = string.Format("Thời gian còn lại :  {0} Giờ", (apt.End - DateTime.Now).Hours);
                    //lblNguoiCapNhat.Caption = string.Format("Người thực hiện: {0} | {1:dd/MM/yyyy}", objCV..HoTenNV, objCV.ThoiGianTH);
                    //lblNguoiTao.Caption = string.Format("Người giao việc: {0} | {1:dd/MM/yyyy}", objCV.tnNhanVien1.HoTenNV, objCV.NgayGiaoViec);
                    chkHoanThanh.Checked = objCV.HoanThanh.GetValueOrDefault();
                }
                else
                {
                    btnLuu.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
        }

        private void AddNew_frm_Load(object sender, EventArgs e)
        {
            LoadData();
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

        private void txtTieuDe_EditValueChanged(object sender, EventArgs e)
        {
            TextEdit _New = (TextEdit)sender;
            this.Text = _New.Text + " - Lịch hẹn";
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
               // objCV.TieuDe = txtTieuDe.Text.Trim();
                objCV.MoTa = txtDienGiai.Text.Trim();
                objCV.ThoiGianTH = (DateTime?)dateNgayBD.EditValue;
                objCV.ThoiGianHetHan = (DateTime?)dateNgayKT.EditValue;
                objCV.TrangThaiCV = db.btCongViecBT_trangThais.Where(p => p.TenTT == lookUpLabel.Text).SingleOrDefault().ID;
               // objCV.ViTri = txtDiaDiem.Text.Trim();
                objCV.TienDoTH = (decimal?)spinTienDo.EditValue;
                objCV.HoanThanh = (bool)chkHoanThanh.Checked;
                var objls = new btDauMucCongViec_LichSu();
                objls.MaNVCN = objNV.MaNV;
                objls.NgayCN = DateTime.Now;
                objls.TienDo = (decimal?)spinTienDo.EditValue;
                objls.DienGiai = txtDienGiai.Text.Trim();
                objCV.btDauMucCongViec_LichSus.Add(objls);
                db.SubmitChanges();
                IsUpdate = true;
                DialogBox.Alert("Dữ liệu đã được cập nhật");
                this.DialogResult = DialogResult.OK;
            }
            catch
            {
                DialogBox.Alert("Dữ liệu không thể lưu.");
            }
            this.Close();
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}