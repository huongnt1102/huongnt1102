using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Library;
using System.Threading;
using System.Text.RegularExpressions;
using System.Data.Linq.SqlClient;
using DevExpress.XtraEditors;

namespace Building.AppVime.Employee
{
    public partial class frmManager : DevExpress.XtraEditors.XtraForm
    {
        //public System.Collections.Generic.List<Library.Class.PhanQuyen.ControlName> LControlName { get; set; }
        FireSharp.Interfaces.IFirebaseClient client;
        FireSharp.Interfaces.IFirebaseConfig ifc = new FireSharp.Config.FirebaseConfig()
        {
            AuthSecret = "f0LT95jxtqbfFo8UT94mJNCSXaH7SLVotqwIbUSh",
            BasePath = "https://app-building-bitexco-default-rtdb.firebaseio.com/"
        };

        #region Class Model
        public class MyUser
        {
            public int? code { set; get; }
            public bool? codeValid { set; get; }
            public string AccountInfoResident { set; get; }
            public string AccountInfoEmployment { set; get; }
        }
        #endregion
        public frmManager()
        {
            InitializeComponent();
        }

        private void itemSync_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var wait = DialogBox.WaitingForm();

            List<tnNhanVien> listUser;

            using (var db = new MasterDataContext())
            {
                listUser = db.tnNhanViens.Where(p => (p.DienThoai ?? "") != "").ToList();
            }

            int amount = 0, total = listUser.Count;

            CommonVime.GetConfig();
            string secretKey = CommonVime.SecretKey;
            string apiKey = CommonVime.ApiKey;

            foreach (var item in listUser)
            {
                using (var db = new MasterDataContext())
                {
                    string phoneNumber = CommonVime.FormatPhone(item.DienThoai);
                    long uid = Convert.ToInt64(CommonVime.GetUID(phoneNumber));

                    var model = new ResidentRegisterBindingModelNoId();
                    model.displayName = item.HoTenNV;
                    model.phoneNumber = phoneNumber;

                    model.secretKey = secretKey;
                    model.apiKey = apiKey;

                    var nhanVien = db.tnNhanViens.FirstOrDefault(_ => _.MaNV == (int?)item.MaNV);
                    if (nhanVien == null)
                    {
                        DialogBox.Warning("Không tìm ra nhân viên.");
                        return;
                    }
                    var tn = (from p in db.tnToaNhas join nd in db.tnToaNhaNguoiDungs on p.MaTN equals nd.MaTN join ap in db.app_TowerSettingPages on p.MaTN equals ap.Id where nd.MaNV == nhanVien.MaNV select new { p.MaTN, ap.DisplayName }).FirstOrDefault();
                    if (tn == null)
                    {
                        DialogBox.Warning("Không tìm ra tòa nhà.");
                        return;
                    }

                    Class.user_edit_from_orther_db model_param = new Class.user_edit_from_orther_db() { UserName = phoneNumber, CreateDate = System.DateTime.UtcNow.AddHours(7), Lock = false, IsCustomer = false, IsEmp = true, building_matn = tn.MaTN, building_code = tn.DisplayName, apartment_mamb = 0, apartment_code = tn.DisplayName, pass = phoneNumber };
                    var param = new Dapper.DynamicParameters();
                    param.AddDynamicParams(model_param);
                    //param.Add("EmployeeId", employeeId);
                    var a = Library.Class.Connect.QueryConnect.QueryAsyncString<bool>("dbo.tbl_user_edit_from_orther_db", VimeService.isPersonal == false? Library.Class.Enum.ConnectString.CONNECT_MYHOME: Library.Class.Enum.ConnectString.CONNECT_STRING, param);

                    try
                    {
                        var dateNow = db.GetSystemDate();

                        //Kiểm tra đã đăng ký chưa
                        var objRe = db.app_Residents.FirstOrDefault(p => p.Phone == item.DienThoai);
                        if (objRe == null)
                        {
                            Class.tbl_building_get_id model_param_1 = new Class.tbl_building_get_id() { Building_Code = tn.DisplayName, Building_MaTN = tn.MaTN };
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
                                obj.FullName = item.HoTenNV;
                                obj.UID = uid;
                                obj.Phone = phoneNumber;
                                obj.Password = phoneNumber;
                                obj.EmployeeFullName = item.HoTenNV;
                                obj.EmployeeIdRefer = item.MaNV;
                                obj.EmployeeIsLock = false;
                                obj.IsEmployee = true;
                                obj.IsResident = false;
                                db.app_Residents.InsertOnSubmit(obj);

                                db.SubmitChanges();
                            //}
                        }
                        else//Đã đăng ký
                        {
                            if (!objRe.IsEmployee.GetValueOrDefault())
                            {
                                objRe.EmployeeFullName = item.HoTenNV;
                                objRe.EmployeeIdRefer = item.MaNV;
                                objRe.EmployeeIsLock = false;
                                objRe.IsEmployee = true;
                                objRe.EmployeeId = Common.User.MaNV;

                                db.SubmitChanges();
                            }
                        }

                    }
                    catch (Exception ex) { }
                }

                Thread.Sleep(500);
                amount++;

                wait.SetCaption(string.Format("Đã đồng bộ {0}/{1} nhân viên", amount, total));
            }

            if (!wait.IsDisposed)
            {
                wait.Close();
                wait.Dispose();
            }
        }

        private void frmManager_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);

            lookUpEditStatus.DataSource = new ResidentStatusBindingModel().GetData();

            KyBaoCao objKBC = new KyBaoCao();
            foreach (string str in objKBC.Source)
                cmbKyBaoCao.Items.Add(str);
            itemKyBaoCao.EditValue = objKBC.Source[7];
            SetDate(4);

            try
            {
                client = new FireSharp.FirebaseClient(ifc);
            }
            catch (System.Exception ex) { }

            LoadData();
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

        private void cmbKyBC_EditValueChanged(object sender, EventArgs e)
        {
            SetDate((sender as ComboBoxEdit).SelectedIndex);
        }

        private void itemTuNgay_EditValueChanged(object sender, EventArgs e)
        {
            //if (!first) LoadData();
        }

        private void itemDenNgay_EditValueChanged(object sender, EventArgs e)
        {
            //if (!first) LoadData();
        }

        private void itemRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        void LoadData()
        {
            gcResident.DataSource = null;
            gcResident.DataSource = linqInstantFeedbackSource1;
        }

        void RefreshData()
        {
            linqInstantFeedbackSource1.Refresh();
        }

        private void linqInstantFeedbackSource1_DismissQueryable(object sender, DevExpress.Data.Linq.GetQueryableEventArgs e)
        {
            try
            {
                (e.Tag as MasterDataContext).Dispose();
            }
            catch { }
        }

        private void linqInstantFeedbackSource1_GetQueryable(object sender, DevExpress.Data.Linq.GetQueryableEventArgs e)
        {
            DateTime dateNow = DateTime.Now;
            var tuNgay = itemTuNgay.EditValue != null ? (DateTime)itemTuNgay.EditValue : dateNow;
            tuNgay = new DateTime(tuNgay.Year, tuNgay.Month, tuNgay.Day, 0, 0, 0);

            var denNgay = itemDenNgay.EditValue != null ? (DateTime)itemDenNgay.EditValue : dateNow;
            denNgay = new DateTime(denNgay.Year, denNgay.Month, denNgay.Day, 23, 59, 0);
            var db = new MasterDataContext();

            e.QueryableSource = from r in db.app_Residents
                                join nvn in db.tnNhanViens on r.EmployeeId equals nvn.MaNV
                                where r.DateOfCreate >= tuNgay
                                    & r.DateOfCreate <= denNgay
                                    & r.IsEmployee.GetValueOrDefault()
                                orderby r.DateOfCreate descending
                                select new
                                {
                                    r.Id,
                                    AmountNewsUnread = r.AmountNewsUnread > 0 ? r.AmountNewsUnread : 0,
                                    AmountNotifyUnread = r.AmountNotifyUnread > 0 ? r.AmountNotifyUnread : 0,
                                    DateOfJoin = r.DateOfCreate,
                                    r.DateOfProcess,
                                    r.DescriptionProcess,
                                    Employeer = nvn.HoTenNV,
                                    FullName = r.EmployeeFullName,
                                    r.Phone,
                                    EmployeerProcess = "",
                                    IsLogin = r.IsEmloyeeLogin.GetValueOrDefault(),
                                    IsLock = r.EmployeeIsLock.GetValueOrDefault(),
                                    r.UID
                                };
            e.Tag = db;
        }

        private void itemToaNha_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void itemProcess_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void itemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Library.Commoncls.ExportExcel(gcResident);
        }

        private void itemAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var frm = new frmEdit();
            frm.IsTower = true;
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                this.LoadData();
            }
        }

        private void itemLock_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LockUnlock(true);
        }

        private void itemUnlock_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LockUnlock(false);
        }

        void LockUnlock(bool val)
        {
            try
            {
                if (gvResident.FocusedRowHandle < 0)
                {
                    DialogBox.Alert("Vui lòng chọn [Nhân viên]. Xin cảm ơn.");
                    return;
                }

                var id = (decimal?)gvResident.GetFocusedRowCellValue("Id");
                if (DialogBox.Question(string.Format("Bạn có chắc chắn muốn {0} không?", val ? "Khóa" : "Mở khóa")) == DialogResult.No) return;

                using (var db = new MasterDataContext())
                {
                    var obj = db.app_Residents.FirstOrDefault(p => p.Id == id);
                    if (obj != null)
                    {
                        obj.EmployeeIsLock = val;
                        obj.EmployeeIdProcess = Common.User.MaNV;
                        obj.DescriptionProcess = val ? "Khóa nhân viên" : "Mở khóa nhân viên";
                        db.SubmitChanges();

                        this.LoadData();
                    }
                }
            }
            catch
            {
            }
        }

        private void itemChangePhone_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gvResident.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn [Nhân viên]. Xin cảm ơn.");
                return;
            }

            var frm = new frmChangePhone();
            frm.Id = (decimal?)gvResident.GetFocusedRowCellValue("Id");
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                this.LoadData();
            }
        }

        /// <summary>
        /// UPDATE FIREBASE
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gvResident.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn [Nhân viên]. Xin cảm ơn.");
                return;
            }

            var wait = DialogBox.WaitingForm();

            var id = (decimal?)gvResident.GetFocusedRowCellValue("Id");

            using (var db = new MasterDataContext())
            {
                var objRe = db.app_Residents.FirstOrDefault(item => item.Id == id);
                if (objRe == null) return;
                string phoneNumber = CommonVime.FormatPhone(objRe.Phone);
                long uid = Convert.ToInt64(CommonVime.GetUID(phoneNumber));

                CommonVime.GetConfig();
                string secretKey = CommonVime.SecretKey;
                string apiKey = CommonVime.ApiKey;

                var model = new ResidentRegisterBindingModelNoId();
                model.displayName = objRe.FullName;
                model.phoneNumber = phoneNumber;

                model.secretKey = secretKey;
                model.apiKey = apiKey;

                var nhanVien = db.tnNhanViens.FirstOrDefault(_ => _.MaNV == (int?)objRe.EmployeeIdRefer);
                if (nhanVien == null)
                {
                    wait.Close();
                    DialogBox.Warning("Không tìm ra nhân viên.");
                    return;
                }
                var tn = (from p in db.tnToaNhas join nd in db.tnToaNhaNguoiDungs on p.MaTN equals nd.MaTN join ap in db.app_TowerSettingPages on p.MaTN equals ap.Id where nd.MaNV == nhanVien.MaNV select new {  p.MaTN, ap.DisplayName }).FirstOrDefault();
                if (tn == null)
                {
                    wait.Close();
                    DialogBox.Warning("Không tìm ra tòa nhà.");
                    return;
                }

                Class.user_edit_from_orther_db model_param = new Class.user_edit_from_orther_db() { UserName = phoneNumber, CreateDate = System.DateTime.UtcNow.AddHours(7), Lock = false, IsCustomer = false, IsEmp = true, building_matn = tn.MaTN, building_code = tn.DisplayName, apartment_mamb = 0, apartment_code = tn.DisplayName, pass = phoneNumber };
                var param = new Dapper.DynamicParameters();
                param.AddDynamicParams(model_param);
                //param.Add("EmployeeId", employeeId);
                var a = Library.Class.Connect.QueryConnect.QueryAsyncString<bool>("dbo.tbl_user_edit_from_orther_db", Library.Class.Enum.ConnectString.CONNECT_STRING, param);

                try
                {
                    var dateNow = db.GetSystemDate();

                    Class.tbl_building_get_id model_param_1 = new Class.tbl_building_get_id() { Building_Code = tn.DisplayName, Building_MaTN = tn.MaTN };
                    var param_1 = new Dapper.DynamicParameters();
                    param_1.AddDynamicParams(model_param_1);
                    //param.Add("EmployeeId", employeeId);
                    var b = Library.Class.Connect.QueryConnect.QueryAsyncString<int>("dbo.tbl_building_get_id", Library.Class.Enum.ConnectString.CONNECT_STRING, param_1);

                    CommonVime.GetConfig();
                    model.secretKey = CommonVime.SecretKey;
                    model.apiKey = CommonVime.ApiKey;
                    model.idNew = b.FirstOrDefault();

                    //Chưa đăng ký
                    var retval = VimeService.Post(model, "/accounts/registerNoId");
                    string phone = CommonVime.FormatPhone(retval);

                    if (!objRe.IsEmployee.GetValueOrDefault())
                    {
                        objRe.EmployeeIsLock = false;
                        objRe.IsEmployee = true;
                        objRe.EmployeeId = Common.User.MaNV;

                        db.SubmitChanges();
                    }
                    wait.Close();
                    Library.DialogBox.Success();
                }
                catch (Exception ex) {
                    wait.Close();
                    XtraMessageBoxArgs args = new XtraMessageBoxArgs();
                    //args.AutoCloseOptions.Delay = 5000;
                    args.Caption = ex.GetType().FullName;
                    args.Text = ex.Message;
                    args.Buttons = new DialogResult[] { DialogResult.OK, DialogResult.Cancel };
                    XtraMessageBox.Show(args).ToString();
                }
            }
        }

        /// <summary>
        /// XÓA
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gvResident.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn [Nhân viên]. Xin cảm ơn.");
                return;
            }

            var id = (decimal?)gvResident.GetFocusedRowCellValue("Id");
            if (DialogBox.QuestionDelete() == DialogResult.No) return;

            using (var db = new MasterDataContext())
            {
                var obj = db.app_Residents.FirstOrDefault(p => p.Id == id);
                if (obj != null)
                {
                    db.app_Residents.DeleteOnSubmit(obj);
                    db.SubmitChanges();

                    this.LoadData();
                }
            }
        }

        /// <summary>
        /// Reset mật khẩu của tài khoản app
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem_ResetPassword_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                var db = new MasterDataContext();
                var resident_id = (decimal?)gvResident.GetFocusedRowCellValue("Id");

                var resident_item = db.app_Residents.FirstOrDefault(_ => _.Id == resident_id);
                if (resident_item != null)
                {
                    if (DialogBox.Question("Bạn có muốn đổi mật khẩu về mật khẩu mặc định không?") == DialogResult.No) return;

                    resident_item.Password = resident_item.Phone;
                    db.SubmitChanges();
                    DialogResult = DialogResult.OK;
                }
            }
            catch (System.Exception ex) { }
        }

        private void barButtonItem_KiemTraTonTaiFirebase_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                var uid = (decimal)gvResident.GetFocusedRowCellValue("UID");
                // var data = client.Get("/users");
                var data = client.Get("/users/" + uid.ToString());
                // var data = client.Get("/users/2342457567");
                //var mList = Newtonsoft.Json.JsonConvert.DeserializeObject<IDictionary<long, MyUser>>(data.Body);
                var mList = Newtonsoft.Json.JsonConvert.DeserializeObject<MyUser>(data.Body);

                //var item = mList.FirstOrDefault(_ => _.Key == uid);
                //if(item.Key != null)
                //{

                //}

                if (mList != null)
                {
                    DialogBox.Alert("Đã có trên firebase");
                }
                else
                {
                    DialogBox.Alert("Chưa có trên firebase");
                }

                //mList.Keys.ToList();
                //var listNumber = mList.Values.ToList();
            }
            catch (System.Exception ex) { }
        }
    }
}
