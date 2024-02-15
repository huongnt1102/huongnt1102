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

namespace Building.AppVime.Resident
{
    public partial class frmChangePhone : DevExpress.XtraEditors.XtraForm
    {
        public decimal? Id { get; set; }
        public int TowerId { get; set; }

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

            if (txtFullName.Text == "")
            {
                DialogBox.Warning("Vui lòng nhập [Họ và tên], xin cảm ơn.");
                txtFullName.Focus();
                return;
            }

            var wait = DialogBox.WaitingForm();
            string phoneNumber = CommonVime.FormatPhone(txtPhone.Text.Trim());
            long uid = Convert.ToInt64(CommonVime.GetUID(phoneNumber));

            using (var db = new MasterDataContext())
            {
                objOrigin = db.app_Residents.FirstOrDefault(p => p.Id == this.Id);
                if (objOrigin == null)
                {
                    DialogBox.Alert("[Cư dân] này không có trong hệ thống.\r\nVui lòng kiểm tra lại, xin cảm ơn.");
                    goto doo;
                }

                if (!txtPhone.Text.Trim().Equals(txtPhoneOld.Text.ToString().Trim()))
                {
                    if (!txtFullName.Text.Trim().Equals(txtFullName.Tag.ToString().Trim()))
                    {
                        //Chỉ đổi tên
                        objOrigin.FullName = txtFullName.Text.Trim();
                    }

                    //Đổi số điện thoại
                    var objCheck = db.app_Residents.FirstOrDefault(p => p.Phone == phoneNumber);
                    if (objCheck != null)
                    {
                        //Đã có người dùng (Khách hàng)
                        //Nếu đã có Cư dân rồi thì thông báo đã sử dụng
                        //Không đổi điện thoại được
                        if (objCheck.IsResident.GetValueOrDefault())
                        {
                            DialogBox.Alert("[Số điện thoại] này đã được sử dụng.\r\nVui lòng kiểm tra lại. Xin cảm ơn.");
                            goto doo;
                        }
                        else
                        {
                            //Đã có người dùng (Nhân viên)
                            //Nếu chưa có Cư dân kèm theo thì gôm nhân viên vào Cư dân hiện tại
                            //Cập nhật điện thoại mới
                            objOrigin.Phone = phoneNumber;
                            objOrigin.UID = uid;
                            objOrigin.FullName = txtFullName.Text.Trim();

                            objOrigin.DescriptionProcess = "Đổi [Số điện thoại] " + txtContents.Text;
                            objOrigin.EmployeeIdProcess = Common.User.MaNV;
                            objOrigin.SpaceMainId = null;
                            objOrigin.ToweId = null;
                            objOrigin.IsResident = true;

                            var listCus = (from kh in db.tnKhachHangs
                                           join mb in db.mbMatBangs on kh.MaKH equals mb.MaKH
                                           where mb.MaMB == objOrigin.SpaceMainId
                                           select new
                                           {
                                               kh.MaKH,
                                               kh.MaTN,
                                               mb.MaMB,
                                               mb.MaSoMB
                                           }).ToList();
                            foreach (var itemObj in listCus)
                            {
                                var itemKhachHang = db.tnKhachHangs.FirstOrDefault(_ => _.MaKH == itemObj.MaKH);
                                if (itemKhachHang != null)
                                {
                                    itemKhachHang.DienThoaiKH = phoneNumber;
                                    objOrigin.ToweId = itemObj.MaTN;
                                    objOrigin.SpaceMainId = itemObj.MaMB;
                                    objOrigin.SpaceMainCode = itemObj.MaSoMB;
                                }
                            }

                            var model = new ResidentRegisterBindingModelNoId();
                            model.displayName = objOrigin.FullName;
                            model.phoneNumber = phoneNumber;

                            CommonVime.GetConfig();
                            model.secretKey = CommonVime.SecretKey;
                            model.apiKey = CommonVime.ApiKey;

                            var toaNhaItems = db.app_TowerSettingPages.FirstOrDefault(_ => _.Id == objOrigin.ToweId);
                            if (toaNhaItems == null)
                            {
                                DialogBox.Alert("[Cư dân] này không có tòa nhà.\r\nVui lòng kiểm tra lại, xin cảm ơn.");
                                goto doo;
                            }

                            Class.user_edit_from_orther_db model_param = new Class.user_edit_from_orther_db() { UserName = phoneNumber, CreateDate = System.DateTime.UtcNow.AddHours(7), Lock = false, IsCustomer = true, IsEmp = false, building_matn = objOrigin.ToweId, building_code = toaNhaItems.DisplayName, apartment_mamb = objOrigin.SpaceMainId, apartment_code = objOrigin.SpaceMainCode, pass = objOrigin.Password };
                            var param = new Dapper.DynamicParameters();
                            param.AddDynamicParams(model_param);
                            //param.Add("EmployeeId", employeeId);
                            var a = Library.Class.Connect.QueryConnect.QueryAsyncString<bool>("dbo.tbl_user_edit_from_orther_db", VimeService.isPersonal == false ? Library.Class.Enum.ConnectString.CONNECT_MYHOME : Library.Class.Enum.ConnectString.CONNECT_STRING, param);

                            db.SubmitChanges();

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

                            //objOrigin.SpaceMainCode = "";

                            //Update nhân viên cũ vào Khách hàng
                            objOrigin.EmployeeFullName = objCheck.EmployeeFullName;
                            objOrigin.EmployeeIdRefer = objCheck.EmployeeIdRefer;
                            objOrigin.EmployeeIsLock = objCheck.EmployeeIsLock;
                            objOrigin.IsEmployee = objCheck.IsEmployee;
                            objOrigin.IsEmloyeeLogin = objCheck.IsEmloyeeLogin;

                            //Xóa nhân viên cũ
                            db.app_Residents.DeleteOnSubmit(objCheck);
                            try
                            {
                                db.SubmitChanges();
                                DialogResult = DialogResult.OK;
                            }
                            catch (Exception ex)
                            {
                                DialogBox.Error("Đã xảy ra lỗi. Vui lòng thử lại sau. \r\nMã lỗi: " + ex.Message);
                            }
                        }
                    }
                    else
                    {
                        //Chưa có số điện thoại trong hệ thống
                        //Đăng ký số mới
                        //Cập nhật số điện thoại
                        
                        //string phone = CommonVime.FormatPhone(retval);
                        //if (Convert.ToInt64(phone) > 0)
                        //{
                        //    objOrigin.UID = uid;
                        objOrigin.Phone = phoneNumber;
                        objOrigin.IsResident = true;
                        objOrigin.IsLogin = false;
                        objOrigin.DescriptionProcess = "[Đổi số điện thoại] " + txtContents.Text;
                        objOrigin.EmployeeIdProcess = Common.User.MaNV;
                        objOrigin.UID = Convert.ToInt64(CommonVime.GetUID(phoneNumber));


                        //var listCus = db.tnKhachHangs.Where(p => p.DienThoaiKH == txtPhoneOld.Text & p.MaTN == TowerId).ToList();
                        //var listCus = db.tnKhachHangs.Where(p => p.DienThoaiKH == txtPhoneOld.Text & p.MaTN == TowerId).ToList();
                        //if (listCus.Count > 0)
                        //{
                        //    listCus.ForEach(p => p.DienThoaiKH = phoneNumber);
                        //}

                        var listCus = (from kh in db.tnKhachHangs
                                       join mb in db.mbMatBangs on kh.MaKH equals mb.MaKH
                                       where mb.MaMB == objOrigin.SpaceMainId
                                       select new
                                       {
                                           kh.MaKH,
                                           kh.MaTN,
                                           mb.MaMB,
                                           mb.MaSoMB
                                       }).ToList();
                        foreach(var itemObj in listCus)
                        {
                            var itemKhachHang = db.tnKhachHangs.FirstOrDefault(_ => _.MaKH == itemObj.MaKH);
                            if(itemKhachHang != null)
                            {
                                itemKhachHang.DienThoaiKH = phoneNumber;
                                objOrigin.ToweId = itemObj.MaTN;
                                objOrigin.SpaceMainId = itemObj.MaMB;
                                objOrigin.SpaceMainCode = itemObj.MaSoMB;
                            }
                        }



                        var model = new ResidentRegisterBindingModelNoId();
                        model.displayName = objOrigin.FullName;
                        model.phoneNumber = phoneNumber;

                        CommonVime.GetConfig();
                        model.secretKey = CommonVime.SecretKey;
                        model.apiKey = CommonVime.ApiKey;

                        var toaNhaItems = db.app_TowerSettingPages.FirstOrDefault(_ => _.Id == objOrigin.ToweId);
                        if (toaNhaItems == null)
                        {
                            DialogBox.Alert("[Cư dân] này không có tòa nhà.\r\nVui lòng kiểm tra lại, xin cảm ơn.");
                            goto doo;
                        }

                        Class.user_edit_from_orther_db model_param = new Class.user_edit_from_orther_db() { UserName = phoneNumber, CreateDate = System.DateTime.UtcNow.AddHours(7), Lock = false, IsCustomer = true, IsEmp = false, building_matn = objOrigin.ToweId, building_code = toaNhaItems.DisplayName, apartment_mamb = objOrigin.SpaceMainId, apartment_code = objOrigin.SpaceMainCode, pass = objOrigin.Password };
                        var param = new Dapper.DynamicParameters();
                        param.AddDynamicParams(model_param);
                        //param.Add("EmployeeId", employeeId);
                        var a = Library.Class.Connect.QueryConnect.QueryAsyncString<bool>("dbo.tbl_user_edit_from_orther_db", VimeService.isPersonal == false ? Library.Class.Enum.ConnectString.CONNECT_MYHOME : Library.Class.Enum.ConnectString.CONNECT_STRING, param);

                        db.SubmitChanges();

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

                        //Nếu cư dân gốc có nhân viên kèm theo thì
                        //Tạo ra 1 dòng mới với số điện thoại cũ
                        //để chứa nhân viên
                        if (objOrigin.IsEmployee.GetValueOrDefault())
                        {
                            var objNew = new app_Resident();
                            objNew.DateOfCreate = db.GetSystemDate();
                            objNew.EmployeeId = Common.User.MaNV;
                            objNew.FullName = objOrigin.EmployeeFullName;
                            objNew.UID = Convert.ToInt64(CommonVime.GetUID(txtPhoneOld.Text));
                            objNew.Phone = txtPhoneOld.Text;
                            objNew.Password = txtPhoneOld.Text;
                            objNew.EmployeeFullName = objOrigin.EmployeeFullName;
                            objNew.EmployeeIdRefer = objOrigin.EmployeeIdRefer;
                            objNew.EmployeeIsLock = objOrigin.EmployeeIsLock;
                            objNew.IsEmployee = true;
                            objNew.IsEmloyeeLogin = objOrigin.IsEmloyeeLogin;
                            objNew.DescriptionProcess = "[Đổi số điện thoại] " + txtContents.Text;
                            objNew.EmployeeIdProcess = Common.User.MaNV;
                            db.app_Residents.InsertOnSubmit(objNew);

                            objOrigin.EmployeeFullName = "";
                            objOrigin.EmployeeIdRefer = null;
                            objOrigin.EmployeeIsLock = false;
                            objOrigin.IsEmployee = false;
                            objOrigin.IsEmloyeeLogin = false;
                        }

                        try
                        {
                            db.SubmitChanges();
                            DialogResult = DialogResult.OK;
                        }
                        catch (Exception ex)
                        {
                            DialogBox.Error("Đã xảy ra lỗi. Vui lòng thử lại sau. \r\nMã lỗi: " + ex.Message);
                        }
                        //}
                        //else
                        //{
                        //    DialogBox.Error("Đã xảy ra lỗi. Vui lòng thử lại sau.");
                        //}
                    }
                }else
                {
                    if (!txtFullName.Text.Trim().Equals(txtFullName.Tag.ToString().Trim()))
                    {
                        //Chỉ đổi tên
                        objOrigin.FullName = txtFullName.Text.Trim();

                        var objRT = db.app_ResidentTowers.FirstOrDefault(p => p.ResidentId == Id & p.TowerId == TowerId);
                        if(objRT != null)
                        {
                            objRT.DateOfProcess = db.GetSystemDate();
                            objRT.DescriptionProcess = "Đổi [Họ và tên] " + txtContents.Text;
                            objRT.EmployeeIdUpdate = Common.User.MaNV;
                        }
                        try
                        {
                            db.SubmitChanges();
                            DialogResult = DialogResult.OK;
                        }
                        catch (Exception ex)
                        {
                            DialogBox.Error("Đã xảy ra lỗi. Vui lòng thử lại sau. \r\nMã lỗi: " + ex.Message);
                        }
                    }
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
                else
                {
                    txtPhoneOld.Text = objOrigin.Phone;
                    txtPhoneOld.Tag = objOrigin.Phone;
                    txtPhone.Text = objOrigin.Phone;
                    txtFullName.Text = objOrigin.FullName;
                    txtFullName.Tag = objOrigin.FullName;
                }
            }
        }
    }
}
