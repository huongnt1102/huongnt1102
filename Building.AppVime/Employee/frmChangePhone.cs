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
    public partial class frmChangePhone : DevExpress.XtraEditors.XtraForm
    {
        public decimal? Id { get; set; }

        app_Resident objOrigin;

        public frmChangePhone()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            if (txtPhone.Text == "")
            {
                DialogBox.Warning("Vui lòng nhập [Điện thoại], xin cảm ơn.");
                txtPhone.Focus();
                return;
            }

            var wait = DialogBox.WaitingForm();

            using (var db = new MasterDataContext())
            {
                objOrigin = db.app_Residents.FirstOrDefault(p => p.Id == this.Id);
                if (objOrigin == null)
                {
                    DialogBox.Alert("[Nhân viên] này không có trong hệ thống.\r\nVui lòng kiểm tra lại, xin cảm ơn.");
                    goto doo;
                }

                // update nhan vien
                var nhanVien_item = db.tnNhanViens.Where(_ => _.MaNV == objOrigin.EmployeeIdRefer);
                foreach (var item in nhanVien_item)
                {
                    item.DienThoai = txtPhone.Text.Trim();
                }

                var obj = db.app_Residents.FirstOrDefault(p => p.Phone == txtPhone.Text.Trim());
                if (obj != null)
                {
                    #region
                    if (obj.EmployeeIdRefer == null)
                    {
                        string phoneNumber = CommonVime.FormatPhone(txtPhone.Text.Trim());

                        var model = new ResidentRegisterBindingModelNoId();
                        model.displayName = objOrigin.EmployeeFullName;
                        model.phoneNumber = phoneNumber;

                        CommonVime.GetConfig();
                        model.secretKey = CommonVime.SecretKey;
                        model.apiKey = CommonVime.ApiKey;

                        if(objOrigin.ToweId == null)
                        {
                            //update tòa cho dữ liệu chính xác
                            var nhanVienItem = db.tnNhanViens.FirstOrDefault(_ => _.MaNV == objOrigin.EmployeeIdRefer);
                            if(nhanVienItem != null)
                            {
                                objOrigin.ToweId = nhanVienItem.MaTN;
                                db.SubmitChanges();
                            }
                        }

                        var toaNhaItems = db.app_TowerSettingPages.FirstOrDefault(_ => _.Id == objOrigin.ToweId);
                        if (toaNhaItems == null)
                        {
                            DialogBox.Alert("[Cư dân] này không có tòa nhà.\r\nVui lòng kiểm tra lại, xin cảm ơn.");
                            goto doo;
                        }

                        Class.user_edit_from_orther_db model_param = new Class.user_edit_from_orther_db() { UserName = phoneNumber, CreateDate = System.DateTime.UtcNow.AddHours(7), Lock = false, IsCustomer = false, IsEmp = true, building_matn = objOrigin.ToweId, building_code = toaNhaItems.DisplayName, apartment_mamb = 0, apartment_code = toaNhaItems.DisplayName, pass = phoneNumber };
                        var param = new Dapper.DynamicParameters();
                        param.AddDynamicParams(model_param);
                        //param.Add("EmployeeId", employeeId);
                        var a = Library.Class.Connect.QueryConnect.QueryAsyncString<bool>("dbo.tbl_user_edit_from_orther_db", VimeService.isPersonal == false ? Library.Class.Enum.ConnectString.CONNECT_MYHOME : Library.Class.Enum.ConnectString.CONNECT_STRING, param);

                        Class.tbl_building_get_id model_param_1 = new Class.tbl_building_get_id() { Building_Code = toaNhaItems.DisplayName, Building_MaTN = objOrigin.ToweId };
                        var param_1 = new Dapper.DynamicParameters();
                        param_1.AddDynamicParams(model_param_1);
                        //param.Add("EmployeeId", employeeId);
                        var b = Library.Class.Connect.QueryConnect.QueryAsyncString<int>("dbo.tbl_building_get_id", VimeService.isPersonal == false ? Library.Class.Enum.ConnectString.CONNECT_MYHOME : db.Connection.ConnectionString, param_1);

                        CommonVime.GetConfig();
                        model.secretKey = CommonVime.SecretKey;
                        model.apiKey = CommonVime.ApiKey;
                        model.idNew = b.FirstOrDefault();

                        var retval = VimeService.Post(model, "/accounts/registerNoId");

                        obj.EmployeeFullName = objOrigin.EmployeeFullName;
                        obj.EmployeeIdRefer = objOrigin.EmployeeIdRefer;
                        obj.EmployeeImageUrl = objOrigin.EmployeeImageUrl;
                        obj.EmployeeIsLock = objOrigin.EmployeeIsLock;
                        obj.IsEmloyeeLogin = objOrigin.IsEmloyeeLogin;
                        obj.IsEmployee = true;
                        obj.DescriptionProcess = "[Đổi số điện thoại] " + txtContents.Text;
                        obj.EmployeeIdProcess = Common.User.MaNV;

                        objOrigin.EmployeeFullName = "";
                        objOrigin.EmployeeIdRefer = null;
                        objOrigin.EmployeeImageUrl = "";
                        objOrigin.EmployeeIsLock = false;
                        objOrigin.IsEmloyeeLogin = false;
                        objOrigin.IsEmployee = false;

                        


                        try
                        {
                            db.SubmitChanges();
                            DialogResult = DialogResult.OK;
                        }
                        catch (Exception ex)
                        {
                            DialogBox.Error("Đã xảy ra lỗi. Vui lòng thử lại sau. \r\nMã lỗi: " + ex.Message);
                        }
                    }else
                    {
                        DialogBox.Error("[Số điện thoại] này đã được [Nhân viên] khác sử dụng.\r\nVui lòng kiểm tra lại, xin cảm ơn.");
                    }
                    #endregion
                }
                else
                {
                    #region //Số điện thoại không có trong hệ thống
                    string phoneNumber = CommonVime.FormatPhone(txtPhone.Text.Trim());

                    var model = new ResidentRegisterBindingModelNoId();
                    model.displayName = objOrigin.EmployeeFullName;
                    model.phoneNumber = phoneNumber;

                    CommonVime.GetConfig();
                    model.secretKey = CommonVime.SecretKey;
                    model.apiKey = CommonVime.ApiKey;


                    


                    try
                    {
                        var dateNow = db.GetSystemDate();
                        
                        long uid = Convert.ToInt64(CommonVime.GetUID(phoneNumber));

                        if (objOrigin.ToweId == null)
                        {
                            //update tòa cho dữ liệu chính xác
                            var nhanVienItem = db.tnNhanViens.FirstOrDefault(_ => _.MaNV == objOrigin.EmployeeIdRefer);
                            if (nhanVienItem != null)
                            {
                                objOrigin.ToweId = nhanVienItem.MaTN;
                                db.SubmitChanges();
                            }
                        }

                        var toaNhaItems = db.app_TowerSettingPages.FirstOrDefault(_ => _.Id == objOrigin.ToweId);
                        if (toaNhaItems == null)
                        {
                            DialogBox.Alert("[Nhân viên] này không có tòa nhà.\r\nVui lòng kiểm tra lại, xin cảm ơn.");
                            goto doo;
                        }

                        Class.user_edit_from_orther_db model_param = new Class.user_edit_from_orther_db() { UserName = phoneNumber, CreateDate = System.DateTime.UtcNow.AddHours(7), Lock = false, IsCustomer = false, IsEmp = true, building_matn = objOrigin.ToweId, building_code = toaNhaItems.DisplayName, apartment_mamb = 0, apartment_code = toaNhaItems.DisplayName, pass = phoneNumber };
                        var param = new Dapper.DynamicParameters();
                        param.AddDynamicParams(model_param);
                        //param.Add("EmployeeId", employeeId);
                        var a = Library.Class.Connect.QueryConnect.QueryAsyncString<bool>("dbo.tbl_user_edit_from_orther_db", VimeService.isPersonal == false ? Library.Class.Enum.ConnectString.CONNECT_MYHOME : Library.Class.Enum.ConnectString.CONNECT_STRING, param);


                        Class.tbl_building_get_id model_param_1 = new Class.tbl_building_get_id() { Building_Code = toaNhaItems.DisplayName, Building_MaTN = objOrigin.ToweId };
                        var param_1 = new Dapper.DynamicParameters();
                        param_1.AddDynamicParams(model_param_1);
                        //param.Add("EmployeeId", employeeId);
                        var b = Library.Class.Connect.QueryConnect.QueryAsyncString<int>("dbo.tbl_building_get_id", VimeService.isPersonal == false ? Library.Class.Enum.ConnectString.CONNECT_MYHOME : db.Connection.ConnectionString, param_1);

                        CommonVime.GetConfig();
                        model.secretKey = CommonVime.SecretKey;
                        model.apiKey = CommonVime.ApiKey;
                        model.idNew = b.FirstOrDefault();

                        var retval = VimeService.Post(model, "/accounts/registerNoId");
                        string phone = CommonVime.FormatPhone(retval);
                        //if (Convert.ToInt64(phone) > 0)
                        //{
                            var objNew = new app_Resident();
                            objNew.DateOfCreate = dateNow;
                            objNew.EmployeeId = Common.User.MaNV;
                            objNew.FullName = objOrigin.EmployeeFullName;
                            objNew.UID = uid;
                            objNew.Phone = phoneNumber;
                            objNew.Password = phoneNumber;
                            objNew.EmployeeFullName = objOrigin.EmployeeFullName;
                            objNew.EmployeeIdRefer = objOrigin.EmployeeIdRefer;
                            objNew.EmployeeIsLock = objOrigin.EmployeeIsLock;
                            objNew.IsEmployee = true;
                            objNew.DescriptionProcess = "[Đổi số điện thoại] " + txtContents.Text;
                            objNew.EmployeeIdProcess = Common.User.MaNV;
                        objNew.ToweId = objOrigin.ToweId;
                            db.app_Residents.InsertOnSubmit(objNew);

                            objOrigin.EmployeeFullName = "";
                            objOrigin.EmployeeIdRefer = null;
                            objOrigin.EmployeeImageUrl = "";
                            objOrigin.EmployeeIsLock = false;
                            objOrigin.IsEmloyeeLogin = false;
                            objOrigin.IsEmployee = false;

                            db.SubmitChanges();
                            DialogResult = DialogResult.OK;
                        //}else
                        //{
                        //    DialogBox.Error("Đã xảy ra lỗi. Vui lòng thử lại sau.");
                        //}
                    }
                    catch (Exception ex)
                    {
                        DialogBox.Error("Đã xảy ra lỗi. Vui lòng thử lại sau. \r\nMã lỗi: " + ex.Message);
                    }

                    #endregion
                }
            }

            doo:
            if (!wait.IsDisposed)
            {
                wait.Close();
                wait.Dispose();
            }

            this.Close();
        }

        private void frmEdit_Load(object sender, EventArgs e)
        {
            using (var db = new MasterDataContext())
            {
                objOrigin = db.app_Residents.FirstOrDefault(p => p.Id == this.Id);
                if (objOrigin == null)
                {
                    DialogBox.Alert("[Nhân viên] này không có trong hệ thống.\r\nVui lòng kiểm tra lại, xin cảm ơn.");
                    this.Close();
                }
            }
        }
    }
}