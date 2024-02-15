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

namespace Building.AppVime
{
    public partial class frmRegisterMulti : DevExpress.XtraEditors.XtraForm
    {
        public byte TowerId { get; set; }
        public bool IsTower { get; set; }

        public List<ResidentChoiceModel> ListResident;

        public frmRegisterMulti()
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

            if (IsTower)
            {
                using (var db = new MasterDataContext())
                {
                    ListResident = db.tnKhachHangs.Where(p => p.MaTN == TowerId & (p.DienThoaiKH ?? "") != "").Select(p => new ResidentChoiceModel()
                    {
                        Id = p.MaKH,
                        FullName = p.HoKH +" "+ p.TenKH,
                        Phone = p.DienThoaiKH
                    }).ToList();
                }
            }

            if (ListResident == null)
                ListResident = new List<AppVime.ResidentChoiceModel>();

            int amount = 0, total = ListResident.Count;

            CommonVime.GetConfig();
            string secretKey = CommonVime.SecretKey;
            string apiKey = CommonVime.ApiKey;

            foreach (var item in ListResident)
            {
                using (var db = new MasterDataContext())
                {
                    string phoneNumber = CommonVime.FormatPhone(item.Phone);
                    if (phoneNumber.Length > 11) continue;

                    long uid = Convert.ToInt64(Building.AppVime.CommonVime.GetUID(phoneNumber));

                    var model = new ResidentRegisterBindingModelNoId();
                    model.displayName = item.FullName;
                    model.phoneNumber = phoneNumber;

                    model.secretKey = secretKey;
                    model.apiKey = apiKey;

                    try
                    {
                        var dateNow = db.GetSystemDate();                        

                        //Kiểm tra đã đăng ký chưa
                        var objRe = db.app_Residents.FirstOrDefault(p => p.Phone == phoneNumber);

                        var khachHang = db.tnKhachHangs.FirstOrDefault(_ => _.MaKH == item.Id);
                        if (khachHang == null)
                        {
                            continue;
                        }
                        var tn = db.tnToaNhas.FirstOrDefault(_ => _.MaTN == khachHang.MaTN);
                        if (tn == null)
                        {
                            continue;
                        }

                        int maMatBang = 0;
                        string maSoMb = tn.TenTN;
                        var matBang = db.mbMatBangs.FirstOrDefault(_ => _.MaKH == khachHang.MaKH);
                        if (matBang != null)
                        {
                            maMatBang = matBang.MaMB;
                            maSoMb = matBang.MaSoMB;
                        }
                        string pass = phoneNumber;
                        if (khachHang.MatKhau != null) pass = khachHang.MatKhau; 

                        Class.user_edit_from_orther_db model_param = new Class.user_edit_from_orther_db() { UserName = phoneNumber, CreateDate = System.DateTime.UtcNow.AddHours(7), Lock = false, IsCustomer = true, IsEmp = false, building_matn = tn.MaTN, building_code = tn.TenTN, apartment_mamb = maMatBang, apartment_code = maSoMb, pass = pass };
                        var param = new Dapper.DynamicParameters();
                        param.AddDynamicParams(model_param);
                        //param.Add("EmployeeId", employeeId);
                        var a = Library.Class.Connect.QueryConnect.QueryAsyncString<bool>("dbo.tbl_user_edit_from_orther_db", VimeService.isPersonal == false? Library.Class.Enum.ConnectString.CONNECT_MYHOME: Library.Class.Enum.ConnectString.CONNECT_STRING, param);

                        if (khachHang.MatKhau == null) khachHang.MatKhau = phoneNumber;
                        if (khachHang.TenDangNhap == null) khachHang.TenDangNhap = phoneNumber;

                        db.SubmitChanges();

                        if (objRe == null)
                        {
                            Class.tbl_building_get_id model_param_1 = new Class.tbl_building_get_id() { Building_Code = tn.TenTN, Building_MaTN = tn.MaTN };
                            var param_1 = new Dapper.DynamicParameters();
                            param_1.AddDynamicParams(model_param_1);
                            //param.Add("EmployeeId", employeeId);
                            var b = Library.Class.Connect.QueryConnect.QueryAsyncString<int>("dbo.tbl_building_get_id", VimeService.isPersonal == false? Library.Class.Enum.ConnectString.CONNECT_MYHOME: Library.Class.Enum.ConnectString.CONNECT_STRING, param_1);

                            CommonVime.GetConfig();
                            model.secretKey = CommonVime.SecretKey;
                            model.apiKey = CommonVime.ApiKey;
                            model.idNew = b.FirstOrDefault();
                            //model.isPersonal = VimeService.isPersonal;

                            //Chưa đăng ký
                            var retval = VimeService.Post(model, "/accounts/registerNoId");
                            string phone = CommonVime.FormatPhone(retval);
                            //if (Convert.ToInt64(phone) > 0)
                            //{
                                var obj = new app_Resident();
                                obj.DateOfCreate = dateNow;
                                obj.EmployeeId = Common.User.MaNV;
                                obj.FullName = item.FullName;
                                obj.UID = uid;
                                obj.Phone = phoneNumber;
                                if (objRe.Password == null) objRe.Password = pass;
                                obj.AmountNewsUnread = 0;
                                obj.AmountNotifyUnread = 0;
                                obj.IsResident = true;
                                obj.DescriptionProcess = txtContents.Text;
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
                        }
                        else
                        {
                            //Đã đăng ký
                            //Nếu chưa có Cư dân kèm theo thì cập nhật Cư dân vào
                            if (!objRe.IsResident.GetValueOrDefault())
                            {
                                objRe.IsResident = true;
                                if (objRe.Password == null) objRe.Password = pass;
                                objRe.IsLock = false;
                                objRe.AmountNewsUnread = 0;
                                objRe.AmountNotifyUnread = 0;
                                objRe.FullName = item.FullName;
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
                                //objRT.DescriptionProcess = txtContents.Text;
                                db.app_ResidentTowers.InsertOnSubmit(obj);
                            }

                            db.SubmitChanges();

                            DialogResult = DialogResult.OK;
                        }
                    }
                    catch (Exception ex) { }
                }

                Thread.Sleep(500);
                amount++;

                wait.SetCaption(string.Format("Đã đăng ký {0}/{1} cư dân", amount, total));
            }

            if (!wait.IsDisposed)
            {
                wait.Close();
                wait.Dispose();
            }
        }
    }
}
