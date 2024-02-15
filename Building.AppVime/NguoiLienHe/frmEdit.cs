using Dapper;
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

namespace Building.AppVime.NguoiLienHe
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
            var wait = DialogBox.WaitingForm();

            if (glCustomer.EditValue == null)
            {
                DialogBox.Error("Vui lòng chọn [Người liên hệ], xin cảm ơn.");
                glCustomer.Focus();
            }

            if (txtPhone.Text.Trim() == "")
            {
                DialogBox.Error("Vui lòng nhập [Số điện thoại], xin cảm ơn.");
                txtPhone.Focus();
            }

            using (var db = new MasterDataContext())
            {
                string phoneNumber = CommonVime.FormatPhone(txtPhone.Text.Trim());
                long uid = Convert.ToInt64(Building.AppVime.CommonVime.GetUID(phoneNumber));

                var model = new ResidentRegisterBindingModelNoId();
                model.displayName = glCustomer.Text;
                model.phoneNumber = phoneNumber;

                CommonVime.GetConfig();
                model.secretKey = CommonVime.SecretKey;
                model.apiKey = CommonVime.ApiKey;

                try
                {
                    var dateNow = db.GetSystemDate();

                    //Kiểm tra đã đăng ký chưa
                    var objRe = db.app_Residents.FirstOrDefault(p => p.Phone == phoneNumber);

                    var khachHang = (from nlh in db.tnKhachHang_NguoiLienHes
                                     join kh in db.tnKhachHangs on nlh.MaKH equals kh.MaKH
                                     join t in db.tnToaNhas on kh.MaTN equals t.MaTN
                                     where nlh.ID == (int?)glCustomer.EditValue
                                     select new
                                     {
                                         kh.MaTN,
                                         t.TenTN,
                                         kh.MaKH,
                                         nlh.ID
                                     }).FirstOrDefault();

                    if (khachHang == null)
                    {
                        DialogBox.Warning("Hiện tại không tìm thấy khách hàng nào có số điện thoại: " + phoneNumber);
                        if (!wait.IsDisposed)
                        {
                            wait.Close();
                            wait.Dispose();
                        }
                        return;
                    }

                    string tenTN = khachHang.TenTN;

                    var tn = db.app_TowerSettingPages.FirstOrDefault(_ => _.Id == khachHang.MaTN);
                    if (tn == null)
                    {
                        DialogBox.Warning("Tòa " + tenTN + " chưa đăng ký cấu hình page.");
                        if (!wait.IsDisposed)
                        {
                            wait.Close();
                            wait.Dispose();
                        }
                        return;
                    }

                    int maMatBang = 0;
                    string maSoMb = tn.DisplayName;
                    var matBang = db.mbMatBangs.FirstOrDefault(_ => _.MaKH == khachHang.MaKH);
                    if (matBang == null)
                    {
                        DialogBox.Warning("Khách hàng chưa có mặt bằng");
                        if (!wait.IsDisposed)
                        {
                            wait.Close();
                            wait.Dispose();
                        }
                        return;
                    }
                    if (matBang != null)
                    {
                        maMatBang = matBang.MaMB;
                        maSoMb = matBang.MaSoMB;
                    }

                    string pass = phoneNumber;


                    Class.user_edit_from_orther_db model_param = new Class.user_edit_from_orther_db() { UserName = phoneNumber, CreateDate = System.DateTime.UtcNow.AddHours(7), Lock = false, IsCustomer = true, IsEmp = false, building_matn = tn.Id, building_code = tn.DisplayName, apartment_mamb = maMatBang, apartment_code = maSoMb, pass = pass };
                    var param = new DynamicParameters();
                    param.AddDynamicParams(model_param);
                    //param.Add("EmployeeId", employeeId);
                    var a = Library.Class.Connect.QueryConnect.QueryAsyncString<bool>("dbo.tbl_user_edit_from_orther_db", VimeService.isPersonal == false ? Library.Class.Enum.ConnectString.CONNECT_MYHOME : Library.Class.Enum.ConnectString.CONNECT_STRING, param);

                    db.SubmitChanges();

                    if (objRe == null)
                    {
                        Class.tbl_building_get_id model_param_1 = new Class.tbl_building_get_id() { Building_Code = tn.DisplayName, Building_MaTN = tn.Id };
                        var param_1 = new Dapper.DynamicParameters();
                        param_1.AddDynamicParams(model_param_1);
                        //param.Add("EmployeeId", employeeId);
                        var b = Library.Class.Connect.QueryConnect.QueryAsyncString<int>("dbo.tbl_building_get_id", VimeService.isPersonal == false ? Library.Class.Enum.ConnectString.CONNECT_MYHOME : Library.Class.Enum.ConnectString.CONNECT_STRING, param_1);

                        CommonVime.GetConfig();
                        model.secretKey = CommonVime.SecretKey;
                        model.apiKey = CommonVime.ApiKey;
                        model.idNew = b.FirstOrDefault();
                        // model.isPersonal = VimeService.isPersonal;

                        //Chưa đăng ký
                        var retval = VimeService.Post(model, "/accounts/registerNoId");
                        string phone = CommonVime.FormatPhone(retval);
                        //if (Convert.ToInt64(phone) > 0)
                        //{
                        var obj = new app_Resident();
                        obj.DateOfCreate = dateNow;
                        obj.EmployeeId = Common.User.MaNV;
                        obj.FullName = model.displayName;
                        obj.UID = uid;
                        obj.Phone = phoneNumber;
                        obj.Password = phoneNumber;
                        obj.IsResident = false;
                        obj.IsContact = true;
                        obj.ContactId = khachHang.ID;
                        if (matBang != null)
                        {
                            obj.SpaceMainId = matBang.MaMB;
                            obj.SpaceMainCode = matBang.MaSoMB;
                            obj.ToweId = matBang.MaTN;
                        }
                        else
                        {
                            obj.ToweId = TowerId;
                        }
                        db.app_Residents.InsertOnSubmit(obj);

                        db.SubmitChanges();

                        var objRT = new app_ResidentTower();
                        objRT.DateOfJoin = dateNow;
                        objRT.EmployeeId = Common.User.MaNV;
                        objRT.Id = Guid.NewGuid();
                        objRT.ResidentId = obj.Id;
                        objRT.TowerId = TowerId;
                        objRT.TypeId = 20;

                        objRT.DescriptionProcess = txtContents.Text;
                        db.app_ResidentTowers.InsertOnSubmit(objRT);

                        db.SubmitChanges();
                        DialogResult = DialogResult.OK;
                        //}
                        //else
                        //{
                        //    DialogBox.Error("[Số điện thoại] này đăng ký không thành công trên firebase.");
                        //}
                    }
                    else//Đã đăng ký
                    {
                        //Chưa có Cư dân kèm theo
                        if (!objRe.IsResident.GetValueOrDefault())
                        {
                            objRe.IsResident = true;
                            objRe.IsLock = false;
                            objRe.AmountNewsUnread = 0;
                            objRe.AmountNotifyUnread = 0;
                            objRe.FullName = model.displayName;
                            objRe.DateOfCreate = dateNow;
                            objRe.EmployeeId = Common.User.MaNV;
                        }

                        var objRT = db.app_ResidentTowers.FirstOrDefault(p => p.ResidentId == objRe.Id & p.TowerId == TowerId);
                        if (objRT == null)
                        {
                            var obj = new app_ResidentTower();
                            obj.DateOfJoin = dateNow;
                            obj.EmployeeId = Common.User.MaNV;
                            obj.Id = Guid.NewGuid();
                            obj.ResidentId = objRe.Id;
                            obj.TowerId = TowerId;
                            obj.TypeId = 20;
                            db.app_ResidentTowers.InsertOnSubmit(obj);
                        }

                        db.SubmitChanges();

                        DialogResult = DialogResult.OK;
                        //else
                        //{
                        //    DialogBox.Alert("[Số điện thoại] này đã có [Cư dân] sử dụng.\r\nVui lòng kiểm tra lại, xin cảm ơn.");
                        //}
                    }
                }
                catch (Exception ex) { DialogBox.Error(ex.Message); }
            }

            if (!wait.IsDisposed)
            {
                wait.Close();
                wait.Dispose();
            }

            this.Close();
        }

        private void frmEdit_Load(object sender, EventArgs e)
        {
            IsTower = false;

            using(var db = new MasterDataContext())
            {
                var listPhones = (from r in db.app_Residents
                                  join re in db.app_ResidentTowers on r.Id equals re.ResidentId
                                  where re.TowerId == TowerId 
                                    & r.IsResident == false
                                    & r.IsContact == true
                                  select r.Phone).ToList();
                glCustomer.Properties.DataSource = (from nlh in db.tnKhachHang_NguoiLienHes
                                                    join kh in db.tnKhachHangs on nlh.MaKH equals kh.MaKH
                                                    where kh.MaTN == TowerId
                                                        & !listPhones.Contains(nlh.DienThoai)
                                                    select new
                                                    {
                                                        Id = nlh.ID,
                                                        FullName = nlh.HoVaTen,
                                                        Phone = nlh.DienThoai,
                                                        Email = nlh.Email,
                                                        Company = kh.IsCaNhan.GetValueOrDefault() ? kh.TenKH : kh.CtyTen,
                                                        Type = kh.IsCaNhan.GetValueOrDefault() ? "Cá nhân" : "Doanh nghiệp"
                                                    });
            }
        }

        private void glCustomer_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                using (var db = new MasterDataContext())
                {
                    var obj = db.tnKhachHang_NguoiLienHes.FirstOrDefault(_ => _.ID == (int)glCustomer.EditValue);
                    txtPhone.Text = obj.DienThoai;
                }
            } catch (Exception ex) { }
        }
    }
}
