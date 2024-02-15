using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Library;

namespace Building.AppVime.ServiceExtension
{
    public partial class frmProcess : DevExpress.XtraEditors.XtraForm
    {
        /// <summary>
        /// 1: Tiếp nhận; 2: Đổi trạng thái; 3: Đổi nhân viên; 4: Giao việc
        /// </summary>
        public byte TypeId { get; set; }
        public long? Id;
        public byte StatusId { get; set; }
        public int TowerId { get; set; }
        public string TowerName { get; set; }
        public string ServiceName { get; set; }

        MasterDataContext db;

        public frmProcess()
        {
            InitializeComponent();
        }

        private void frmProcess_Load(object sender, EventArgs e)
        {
            db = new MasterDataContext();

            var list = new Building.AppVime.ResidentStatusBindingModel().GetData();
            lookUpStatus.Properties.DataSource = db.app_BookingServiceExtensionStatus;

            lookUpDepartment.Properties.DataSource = db.tnPhongBans.Where(p => p.MaTN == TowerId).Select(p => new {
                Id = p.MaPB,
                Name = p.TenPB
            });

            lblRequire.Visible = lblRequire2.Visible = false;

            switch (TypeId)
            {
                case 1:
                    this.Text = txtContents.Text = "Tiếp nhận";
                    lookUpStatus.Enabled = false;
                    StatusId = 2;
                    lookUpEmployee.Enabled = false;
                    lookUpDepartment.Enabled = false;
                    break;
                case 2:
                    this.Text = txtContents.Text = "Đổi trạng thái";
                    lookUpStatus.Enabled = true;
                    lookUpEmployee.Enabled = false;
                    lookUpDepartment.Enabled = false;
                    break;
                case 3:
                    this.Text = txtContents.Text = "Đổi nhân viên";
                    lookUpStatus.Enabled = false;
                    lookUpEmployee.Enabled = true;
                    lookUpDepartment.Enabled = true;
                    lblRequire.Visible = lblRequire2.Visible = true;
                    break;
                case 4:
                    this.Text = txtContents.Text = "Giao việc";
                    lookUpStatus.Enabled = false;
                    lookUpEmployee.Enabled = true;
                    lookUpDepartment.Enabled = true;
                    lblRequire.Visible = lblRequire2.Visible = true;
                    break;
                case 5:
                    this.Text = txtContents.Text = "Hoàn thành";
                    StatusId = 3;
                    lookUpStatus.Enabled = true;
                    lookUpEmployee.Enabled = false;
                    lookUpDepartment.Enabled = false;
                    break;
            }
            lookUpStatus.EditValue = StatusId;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            switch (TypeId)
            {
                case 1:
                case 2:
                case 5:
                    if(lookUpStatus.EditValue == null)
                    {
                        DialogBox.Warning("Vui lòng chọn [Trạng thái], xin cảm ơn.");
                        lookUpStatus.Focus();
                        return;
                    }
                    break;

                case 3:
                case 4:
                    if (lookUpEmployee.EditValue == null)
                    {
                        DialogBox.Warning("Vui lòng chọn [Nhân viên], xin cảm ơn.");
                        lookUpEmployee.Focus();
                        return;
                    }
                    break;

                default:
                    return;
            }

            using (db = new MasterDataContext())
            {
                var obj = db.app_BookingServiceExtensions.FirstOrDefault(p => p.Id == Id);
                if (obj != null)
                {
                    var dateNow = db.GetSystemDate();
                    byte statusId = Convert.ToByte(lookUpStatus.EditValue);

                    obj.DateOfProcess = dateNow;
                    obj.LastComment = txtContents.Text;

                    string message = "";
                    switch (TypeId)
                    {
                        case 1://Tiếp nhận yêu cầu
                            obj.EmployeeId = Common.User.MaNV;
                            obj.StatusId = statusId;
                            break;

                        case 2://2: Đổi trạng thái; 
                        case 5:
                            obj.StatusId = statusId;
                            break;

                        case 3://3: Đổi nhân viên;
                        case 4:// 4: Giao việc
                            message = string.Format("Giao việc cho nhân viên [{0}]",lookUpEmployee.Text);// $"Giao việc cho nhân viên [{lookUpEmployee.Text}]";
                            obj.EmployeeId = Convert.ToInt32(lookUpEmployee.EditValue);
                            break;
                    }

                    //Ghi log
                    var objHis = new app_BookingServiceExtensionHistory();
                    objHis.BookingId = Id;
                    objHis.DateCreate = dateNow;
                    objHis.Description = txtContents.Text;
                    objHis.EmployeeId = Common.User.MaNV;
                    objHis.IsCustomer = false;
                    objHis.StatusId = statusId;
                    db.app_BookingServiceExtensionHistories.InsertOnSubmit(objHis);

                    try
                    {
                        db.SubmitChanges();

                        //Gửi notify
                        CommonVime.GetConfig();
                        string secretKey = CommonVime.SecretKey;
                        string apiKey = CommonVime.ApiKey;

                        var model = new
                        {
                            BookingId = Id,
                            StatusId = (TypeId == 3 | TypeId == 4) ? 0 : StatusId,
                            Description = txtContents.Text,
                            TowerName = TowerName,
                            ServiceName = ServiceName,
                            EmployeeId = Common.User.MaNV,
                            EmployeeName = Common.User.HoTenNV,
                            ApiKey = apiKey,
                            SecretKey = secretKey
                        };

                        var retval = VimeService.Put(model, "/Vendors/ServiceExtension/UpdateSoftware");

                        DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        DialogBox.Error("Đã xảy ra lỗi. Vui lòng kiểm tra lại, xin cảm ơn.");
                    }
                }
            }
        }

        private void lookUpDepartment_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                lookUpEmployee.Properties.DataSource = (from p in db.tnNhanViens
                                                        join ep in db.app_EmployeeDepartments on p.MaNV equals ep.EmployeeId
                                                        where ep.TowerId == TowerId
                                                            & ep.DepartmentId == Convert.ToInt32(lookUpDepartment.EditValue)
                                                        select new
                                                        {
                                                            Id = p.MaNV,
                                                            Name = p.HoTenNV
                                                        });
            }
            catch {
                lookUpEmployee.Properties.DataSource = null;
            }
        }
    }
}