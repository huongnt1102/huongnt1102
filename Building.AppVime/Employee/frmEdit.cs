using Dapper;
using DevExpress.XtraEditors;
using Library;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace Building.AppVime.Employee
{
    public partial class frmEdit : DevExpress.XtraEditors.XtraForm
    {
        public byte TowerId { get; set; }
        public bool IsTower { get; set; } 

        public frmEdit()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            if (lookUpNhanVien.EditValue == null)
            {
                DialogBox.Warning("Vui lòng chọn [Nhân viên], xin cảm ơn.");
                lookUpNhanVien.Focus();
                return;
            }

            if (txtFullName.Text == "")
            {
                DialogBox.Warning("Vui lòng nhập [Họ tên], xin cảm ơn.");
                txtFullName.Focus();
                return;
            }

            if (txtPhone.Text == "")
            {
                DialogBox.Warning("Vui lòng nhập [Điện thoại], xin cảm ơn.");
                txtPhone.Focus();
                return;
            }

            var wait = DialogBox.WaitingForm();

            using (var db = new MasterDataContext())
            {
                string phoneNumber = CommonVime.FormatPhone(txtPhone.Text.Trim());

                var model = new ResidentRegisterBindingModelNoId();
                model.displayName = txtFullName.Text;
                model.phoneNumber = phoneNumber;

                CommonVime.GetConfig();
                model.secretKey = CommonVime.SecretKey;
                model.apiKey = CommonVime.ApiKey;

                try
                {
                    var dateNow = db.GetSystemDate();

                    //Kiểm tra đã đăng ký chưa
                    var objRe = db.app_Residents.FirstOrDefault(p => p.Phone == phoneNumber);
                    var nhanVien = db.tnNhanViens.FirstOrDefault(_ => _.MaNV == (int?)lookUpNhanVien.EditValue);
                    if(nhanVien == null)
                    {
                        DialogBox.Warning("Hiện tại không tìm thấy nhân viên nào có số điện thoại: " + phoneNumber);
                        if (!wait.IsDisposed)
                        {
                            wait.Close();
                            wait.Dispose();
                        }
                        return;
                    }
                    var tntoanha = db.tnToaNhas.FirstOrDefault(_ => _.MaTN == nhanVien.MaTN);
                    string tenTN = "";
                    if (tntoanha != null) tenTN = tntoanha.TenTN;
                    var tn = db.app_TowerSettingPages.FirstOrDefault(_ => _.Id == nhanVien.MaTN);
                    if(tn == null)
                    {
                        DialogBox.Warning("Tòa " + tenTN + " chưa đăng ký cấu hình page.");
                        if (!wait.IsDisposed)
                        {
                            wait.Close();
                            wait.Dispose();
                        }
                        return;
                    }

                    Class.user_edit_from_orther_db model_param = new Class.user_edit_from_orther_db() { UserName = phoneNumber, CreateDate = System.DateTime.UtcNow.AddHours(7), Lock = false, IsCustomer = false, IsEmp = true, building_matn = tn.Id, building_code = tn.DisplayName, apartment_mamb = 0, apartment_code = tn.DisplayName, pass = phoneNumber};
                    var param = new DynamicParameters();
                    param.AddDynamicParams(model_param);
                    //param.Add("EmployeeId", employeeId);
                    var a = Library.Class.Connect.QueryConnect.QueryAsyncString<bool>("dbo.tbl_user_edit_from_orther_db", VimeService.isPersonal == false? Library.Class.Enum.ConnectString.CONNECT_MYHOME: Library.Class.Enum.ConnectString.CONNECT_STRING, param);

                    if(a == null)
                    {
                        DialogBox.Error("Page " + tn.DisplayName + " chưa được đăng ký");
                        return;
                    }

                    if (objRe == null)
                    {
                        Class.tbl_building_get_id model_param_1 = new Class.tbl_building_get_id() { Building_Code = tn.DisplayName, Building_MaTN = tn.Id };
                        var param_1 = new Dapper.DynamicParameters();
                        param_1.AddDynamicParams(model_param_1);
                        //param.Add("EmployeeId", employeeId);
                        var b = Library.Class.Connect.QueryConnect.QueryAsyncString<int>("dbo.tbl_building_get_id", VimeService.isPersonal == false? Library.Class.Enum.ConnectString.CONNECT_MYHOME: Library.Class.Enum.ConnectString.CONNECT_STRING, param_1);

                        CommonVime.GetConfig();
                        model.secretKey = CommonVime.SecretKey;
                        model.apiKey = CommonVime.ApiKey;
                        model.idNew = b.FirstOrDefault();
                        //model.isPersonal = VimeService.isPersonal;

                        long uid = Convert.ToInt64(CommonVime.GetUID(phoneNumber));
                        var retval = VimeService.PostH(model, "/accounts/registerNoId");
                        string phone = CommonVime.FormatPhone(retval);
                        //if (Convert.ToInt64(phone) > 0)
                        //{
                            var obj = new app_Resident();
                            obj.DateOfCreate = dateNow;
                            obj.EmployeeId = Common.User.MaNV;
                            obj.FullName = txtFullName.Text;
                            obj.UID = uid;
                            obj.Phone = phoneNumber;
                            obj.Password = phoneNumber;
                            obj.EmployeeFullName = txtFullName.Text;
                            obj.EmployeeIdRefer = Convert.ToInt32(lookUpNhanVien.EditValue);
                            obj.EmployeeIsLock = false;
                            obj.IsEmployee = true;
                        obj.ToweId = tntoanha.MaTN;
                            db.app_Residents.InsertOnSubmit(obj);
                            db.SubmitChanges();
                            DialogResult = DialogResult.OK;
                        //}
                    }
                    else//Đã đăng ký
                    {
                        if (!objRe.IsEmployee.GetValueOrDefault())
                        {
                            objRe.EmployeeFullName = txtFullName.Text;
                            objRe.EmployeeIdRefer = Convert.ToInt32(lookUpNhanVien.EditValue);
                            objRe.EmployeeIsLock = false;
                            objRe.IsEmployee = true;
                            //objRe.ToweId = tntoanha.MaTN;
                            objRe.EmployeeId = Common.User.MaNV;

                            db.SubmitChanges();
                            DialogResult = DialogResult.OK;
                        }
                    }
                }
                catch (Exception ex) {
                    string mes = Translate.TranslateGoogle.TranslateText(ex.Message, "en-us", "vi-vn");
                    XtraMessageBoxArgs args = new XtraMessageBoxArgs();
                    //args.AutoCloseOptions.Delay = 1000;
                    args.Caption = ex.GetType().FullName;
                    args.Text = ex.Message + " (" + mes + ")";
                    args.Buttons = new DialogResult[] { DialogResult.OK, DialogResult.Cancel };
                    XtraMessageBox.Show(args).ToString();
                }
            }

            if (!wait.IsDisposed)
            {
                wait.Close();
                wait.Dispose();
            }

            this.Close();
        }
        //@UserName NVARCHAR(50), @Password NVARCHAR(50), @CreateDate DATETIME, @Lock bit, @IsCustomer bit, @IsEmp bit, @building_matn int, @building_code nvarchar(500) , @apartment_mamb int, @apartment_code nvarchar(500)
        

        private void frmEdit_Load(object sender, EventArgs e)
        {
            using (var db = new MasterDataContext())
            {
                var list = db.app_Residents.Where(p => p.IsEmployee.GetValueOrDefault()).Select(p => p.EmployeeIdRefer);

                lookUpNhanVien.Properties.DataSource = db.tnNhanViens.Where(p => p.DienThoai != "" & !list.Contains(p.MaNV)).Select(p => new { p.MaNV, p.HoTenNV, p.DienThoai });
            }
        }

        private void lookUpNhanVien_EditValueChanged(object sender, EventArgs e)
        {
            using (var db = new MasterDataContext())
            {
                var obj = db.tnNhanViens.FirstOrDefault(p => p.MaNV == Convert.ToInt32(lookUpNhanVien.EditValue));
                if (obj != null)
                {
                    txtFullName.Text = obj.HoTenNV;
                    txtPhone.Text = obj.DienThoai;
                }
            }
        }
    }
}
