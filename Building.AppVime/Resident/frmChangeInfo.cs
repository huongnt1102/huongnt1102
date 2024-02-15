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
    public partial class frmChangeInfo : DevExpress.XtraEditors.XtraForm
    {
        public decimal? Id { get; set; }
        app_Resident objOrigin;

        public frmChangeInfo()
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
            string phoneNumber = CommonVime.FormatPhone(txtPhone.Text.Trim());
            long uid = Convert.ToInt64(CommonVime.GetUID(phoneNumber));

            using (var db = new MasterDataContext())
            {
                objOrigin = db.app_Residents.FirstOrDefault(p => p.Id == this.Id);
                if(objOrigin == null)
                {
                    DialogBox.Alert("[Cư dân] này không có trong hệ thống.\r\nVui lòng kiểm tra lại, xin cảm ơn.");
                    goto doo;
                }

                var objCheck = db.app_Residents.FirstOrDefault(p => p.Phone == phoneNumber);
                if (objCheck != null)
                {
                    //Đã có người dùng (Nhân viên)
                    //Nếu đã có Cư dân rồi thì thông báo đã sử dụng
                    //Không đổi điện thoại được
                    if (objCheck.IsResident.GetValueOrDefault())
                    {
                        DialogBox.Alert("[Số điện thoại] này đã được sử dụng.\r\nVui lòng kiểm tra lại. Xin cảm ơn.");
                        goto doo;
                    }else
                    {                        
                        //Nếu chưa có Cư dân kèm theo thì gôm nhân viên vào Cư dân hiện tại
                        //Cập nhật điện thoại mới
                        objOrigin.Phone = phoneNumber;
                        objOrigin.UID = uid;

                        //Xóa nhân viên đó đi
                        objOrigin.EmployeeFullName = objCheck.EmployeeFullName;
                        objOrigin.EmployeeIdRefer = objCheck.EmployeeIdRefer;
                        objOrigin.EmployeeIsLock = objCheck.EmployeeIsLock;
                        objOrigin.IsEmployee = objCheck.IsEmployee;
                        objOrigin.IsEmloyeeLogin = objCheck.IsEmloyeeLogin;

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
                }else
                {
                    //Chưa có số điện thoại trong hệ thống
                    //Đăng ký số mới
                    //Cập nhật số điện thoại
                    var model = new ResidentRegisterBindingModel();
                    model.displayName = objOrigin.FullName;
                    model.phoneNumber = phoneNumber;

                    CommonVime.GetConfig();
                    model.secretKey = CommonVime.SecretKey;
                    model.apiKey = CommonVime.ApiKey;

                    var retval = VimeService.Post(model, "/accounts/register");
                    string phone = CommonVime.FormatPhone(retval);
                    if (Convert.ToInt64(phone) > 0)
                    {
                        objOrigin.UID = uid;
                        objOrigin.Phone = phoneNumber;
                        objOrigin.IsResident = true;
                        objOrigin.IsLogin = false;
                        objOrigin.DescriptionProcess = "[Đổi số điện thoại] " + txtContents.Text;
                        objOrigin.EmployeeIdProcess = Common.User.MaNV;

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
                    }
                    else
                    {
                        DialogBox.Error("Đã xảy ra lỗi. Vui lòng thử lại sau.");
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
                if(objOrigin == null)
                {
                    DialogBox.Alert("[Nhân viên] này không có trong hệ thống.\r\nVui lòng kiểm tra lại, xin cảm ơn.");
                    this.Close();
                }else
                {
                    txtPhoneOld.Text = objOrigin.Phone;
                }
            }
        }
    }
}
