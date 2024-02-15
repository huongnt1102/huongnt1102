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

namespace Building.AppVime.NguoiLienHe
{
    public partial class frmManager : DevExpress.XtraEditors.XtraForm
    {
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

        public class ItemUID
        {
            public long uid { get; set; }
            public MyUser MyUser { get; set; }
        }

        public class appResidentLoadData
        {
            public Guid? Id { get; set; }

            public byte StatusId { get; set; }

            public byte? TypeId { get; set; }

            public int? AmountNewsUnread { get; set; }

            public int? AmountNotifyUnread { get; set; }

            public int? SpaceMainId { get; set; }

            public System.DateTime? DateOfJoin { get; set; }

            public System.DateTime? DateOfProcess { get; set; }

            public bool? IsLogin { get; set; }

            public bool? IsLock { get; set; }

            public decimal ResidentId { get; set; }

            public decimal? UID { get; set; }

            public string DescriptionProcess { get; set; }

            public string Employeer { get; set; }

            public string FullName { get; set; }

            public string Phone { get; set; }

            public string EmployeerProcess { get; set; }

        }
        #endregion

        public frmManager()
        {
            InitializeComponent();
        }

        private string GetUID(string value)
        {
            string countryCode = "84";
            string phoneNumber = new Regex(@"\D").Replace(value, string.Empty);
            string uid = countryCode + (phoneNumber.StartsWith("0") ? phoneNumber.Substring(1) : phoneNumber);

            return uid;
        }

        private string FormatPhone(string value)
        {
            string phoneNumber = new Regex(@"\D").Replace(value, string.Empty);

            return phoneNumber;
        }

        private void itemSync_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var wait = DialogBox.WaitingForm();

            var maTN = (byte)itemToaNha.EditValue;
            List<tnKhachHang> listCus;

            using (var db = new MasterDataContext())
            {
                listCus = db.tnKhachHangs.Where(p => p.MaTN == maTN & p.DienThoaiKH != null).ToList();
            }

            int amount = 0, total = listCus.Count;

            foreach (var item in listCus)
            {
                using (var db = new MasterDataContext())
                {
                    string phoneNumber = FormatPhone(item.DienThoaiKH);

                    var model = new ResidentRegisterBindingModelNoId();
                    model.displayName = item.HoKH + " " + item.TenKH;
                    model.phoneNumber = phoneNumber;

                    model.secretKey = Properties.Settings.Default.secretKey;
                    model.apiKey = Properties.Settings.Default.apiKey;

                    var khachHang = db.tnKhachHangs.FirstOrDefault(_ => _.MaKH == (int?)item.MaKH);
                    if (khachHang == null)
                    {
                        continue;
                    }
                    var tn = db.app_TowerSettingPages.FirstOrDefault(_ => _.Id == khachHang.MaTN);
                    if (tn == null)
                    {
                        continue;
                    }

                    int maMatBang = 0;
                    string maSoMb = tn.DisplayName;
                    var matBang = db.mbMatBangs.FirstOrDefault(_ => _.MaKH == khachHang.MaKH);
                    if (matBang != null)
                    {
                        maMatBang = matBang.MaMB;
                        maSoMb = matBang.MaSoMB;
                    }
                    string pass = phoneNumber;
                    if (khachHang.MatKhau != null) pass = khachHang.MatKhau;

                    Class.user_edit_from_orther_db model_param = new Class.user_edit_from_orther_db() { UserName = phoneNumber, CreateDate = System.DateTime.UtcNow.AddHours(7), Lock = false, IsCustomer = true, IsEmp = false, building_matn = tn.Id, building_code = tn.DisplayName, apartment_mamb = maMatBang, apartment_code = maSoMb, pass = pass };
                    var param = new Dapper.DynamicParameters();
                    param.AddDynamicParams(model_param);
                    //param.Add("EmployeeId", employeeId);
                    var a = Library.Class.Connect.QueryConnect.QueryAsyncString<bool>("dbo.tbl_user_edit_from_orther_db", VimeService.isPersonal == false? Library.Class.Enum.ConnectString.CONNECT_MYHOME: Library.Class.Enum.ConnectString.CONNECT_STRING, param);

                    if (khachHang.MatKhau == null) khachHang.MatKhau = phoneNumber;
                    if (khachHang.TenDangNhap == null) khachHang.TenDangNhap = phoneNumber;

                    db.SubmitChanges();

                    try
                    {
                        var dateNow = db.GetSystemDate();

                        //Kiểm tra đã đăng ký chưa
                        var objRe = db.app_Residents.FirstOrDefault(p => p.Phone == item.DienThoaiKH);
                        if (objRe == null)
                        {
                            long uid = Convert.ToInt64(GetUID(phoneNumber));
                            //if (uid > 0)
                            //{
                                var obj = new app_Resident();
                                obj.DateOfCreate = dateNow;
                                obj.EmployeeId = Common.User.MaNV;
                                obj.FullName = item.HoKH + " " + item.TenKH;
                                obj.Id = uid;
                                obj.Phone = phoneNumber;
                                db.app_Residents.InsertOnSubmit(obj);

                                var objRT = new app_ResidentTower();
                                objRT.DateOfJoin = dateNow;
                                objRT.EmployeeId = Common.User.MaNV;
                                objRT.Id = Guid.NewGuid();
                                objRT.ResidentId = uid;
                                objRT.TowerId = maTN;
                                objRT.TypeId = 100;
                                db.app_ResidentTowers.InsertOnSubmit(objRT);

                                db.SubmitChanges();

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

                            var retval = VimeService.Post(model, "/accounts/registerNoId");
                                string phone = FormatPhone(retval);
                                if (Convert.ToInt64(phone) > 0)
                                {
                                    objRT.TypeId = 20;
                                    db.SubmitChanges();
                                }
                            //}
                        }
                        else//Đã đăng ký
                        {
                            if (!objRe.IsResident.GetValueOrDefault())
                            {
                                objRe.IsResident = true;
                                objRe.IsLock = false;
                                objRe.AmountNewsUnread = 0;
                                objRe.AmountNotifyUnread = 0;
                                objRe.FullName = item.HoKH + " " + item.TenKH;
                                objRe.DateOfCreate = dateNow;
                                objRe.EmployeeId = Common.User.MaNV;
                            }

                            var objRT = db.app_ResidentTowers.FirstOrDefault(p => p.ResidentId == objRe.Id & p.TowerId == maTN);
                            if (objRT == null)
                            {
                                var obj = new app_ResidentTower();
                                obj.DateOfJoin = dateNow;
                                obj.EmployeeId = Common.User.MaNV;
                                obj.Id = Guid.NewGuid();
                                obj.ResidentId = objRe.Id;
                                obj.TowerId = maTN;
                                obj.TypeId = 20;
                                db.app_ResidentTowers.InsertOnSubmit(obj);

                                db.SubmitChanges();
                            }
                        }

                    }
                    catch (Exception ex) { Library.DialogBox.Error(ex.Message); }
                }

                Thread.Sleep(500);
                amount++;

                wait.SetCaption(string.Format("Đã đồng bộ {0}/{1} cư dân", amount, total));
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

            lkToaNha.DataSource = Common.TowerList;
            itemToaNha.EditValue = CommonVime.TowerId == 0 ? Common.User.MaTN : CommonVime.TowerId;

            lookUpEditStatus.DataSource = new ResidentStatusBindingModel().GetData();

            KyBaoCao objKBC = new KyBaoCao();
            foreach (string str in objKBC.Source)
                cmbKyBaoCao.Items.Add(str);
            itemKyBaoCao.EditValue = objKBC.Source[4];
            SetDate(4);

            try
            {
                client = new FireSharp.FirebaseClient(ifc);
            }
            catch(System.Exception ex) { }

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
            try
            {
                gcResident.DataSource = null;

                var maTN = (byte)itemToaNha.EditValue;

                if (CommonVime.TowerId != maTN)
                    CommonVime.TowerId = maTN;

                DateTime dateNow = DateTime.Now;
                var tuNgay = itemTuNgay.EditValue != null ? (DateTime)itemTuNgay.EditValue : dateNow;
                tuNgay = new DateTime(tuNgay.Year, tuNgay.Month, tuNgay.Day, 0, 0, 0);

                var denNgay = itemDenNgay.EditValue != null ? (DateTime)itemDenNgay.EditValue : dateNow;
                denNgay = new DateTime(denNgay.Year, denNgay.Month, denNgay.Day, 23, 59, 0);
                var db = new MasterDataContext();

                var model = new
                {
                    TowerId = maTN,
                    DateFrom = tuNgay,
                    DateTo = denNgay,
                    IsResident = false,
                    IsContact = true
                };
                var param = new Dapper.DynamicParameters();
                param.AddDynamicParams(model);
                gcResident.DataSource = Library.Class.Connect.QueryConnect.Query<appResidentLoadData>("appResidentLoadData", param);
            }
            catch (System.Exception ex) { }
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
            
            
        }

        private void itemToaNha_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void itemProcess_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                var id = (Guid?)gvResident.GetFocusedRowCellValue("Id");
                var typeId = (byte?)gvResident.GetFocusedRowCellValue("TypeId");
                var residentId = (decimal)gvResident.GetFocusedRowCellValue("ResidentId");

                if (id == null)
                {
                    DialogBox.Error("Vui lòng chọn [hợp đồng], xin cảm ơn.");
                    return;
                }

                using (frmProcess frm = new frmProcess()
                {
                    TypeId = typeId,
                    Id = id,
                    ResidentId = residentId
                })
                {
                    frm.ShowDialog();
                    if (frm.DialogResult == DialogResult.OK)
                    {
                        this.LoadData();
                    }
                }
            }
            catch
            {
            }
        }

        private void itemRegister_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                int[] indexs = gvResident.GetSelectedRows();
                foreach (var i in indexs)
                {
                    var id = (Guid?)gvResident.GetRowCellValue(i, "Id");
                    var typeId = (byte?)gvResident.GetRowCellValue(i, "TypeId");
                    var spaceMainId = (int?)gvResident.GetRowCellValue(i, "SpaceMainId");
                    var residentId = (decimal)gvResident.GetFocusedRowCellValue("ResidentId");

                    if (id == null)
                    {
                        DialogBox.Error("Vui lòng chọn [hợp đồng], xin cảm ơn.");
                        return;
                    }

                    
                    var maTN = (byte)itemToaNha.EditValue;

                    if (CommonVime.TowerId != maTN)
                        CommonVime.TowerId = maTN;

                    var model = new ResidentRegisterBindingModelNoId();
                    model.displayName = gvResident.GetRowCellValue(i, "FullName").ToString();
                    model.phoneNumber = gvResident.GetRowCellValue(i, "Phone").ToString();

                    var db = new MasterDataContext();

                    var resident = db.app_Residents.FirstOrDefault(_ => _.Id == residentId);
                    if (resident == null) return;

                    if (spaceMainId == null)
                    {
                        //DialogBox.Error("Khách hàng không còn mặt bằng, không thể đổi.");
                        //return;
                        // capt nhật lại mặt bằng cho khách hàng
                        var khachHangItem = (from kh in db.tnKhachHangs
                                  join mb in db.mbMatBangs on kh.MaKH equals mb.MaKH
                                  join nlh in db.tnKhachHang_NguoiLienHes on kh.MaKH equals nlh.MaKH
                                  where nlh.ID == resident.ContactId
                                  select new
                                  {
                                      kh.MaKH,
                                      kh.MaTN,
                                      mb.MaMB,
                                      nlh.ID
                                  }).FirstOrDefault();
                        if (khachHangItem != null)
                        {
                            spaceMainId = khachHangItem.MaMB;
                        }
                    }


                    var listCus = (from kh in db.tnKhachHangs
                                   join nlh in db.tnKhachHang_NguoiLienHes on kh.MaKH equals nlh.MaKH
                                   join mb in db.mbMatBangs on kh.MaKH equals mb.MaKH
                                   where nlh.ID == resident.ContactId
                                   select new
                                   {
                                       kh.MaKH,
                                       kh.MaTN,
                                       nlh.ID
                                   }).FirstOrDefault();
                    if(listCus == null)
                    {
                        DialogBox.Error("Khách hàng không còn mặt bằng, không thể đổi.");
                        return;
                    }
                    var khachHang = db.tnKhachHang_NguoiLienHes.FirstOrDefault(_ => _.ID == listCus.ID);
                    if (khachHang == null)
                    {
                        continue;
                    }
                    var tn = db.app_TowerSettingPages.FirstOrDefault(_ => _.Id == listCus.MaTN);
                    if (tn == null)
                    {
                        continue;
                    }

                    int maMatBang = 0;
                    string maSoMb = tn.DisplayName;
                    var matBang = db.mbMatBangs.FirstOrDefault(_ => _.MaMB == spaceMainId);
                    if (matBang != null)
                    {
                        maMatBang = matBang.MaMB;
                        maSoMb = matBang.MaSoMB;
                    }

                    string pass = phoneNumber;

                    Class.user_edit_from_orther_db model_param = new Class.user_edit_from_orther_db() { UserName = phoneNumber, CreateDate = System.DateTime.UtcNow.AddHours(7), Lock = false, IsCustomer = true, IsEmp = false, building_matn = tn.Id, building_code = tn.DisplayName, apartment_mamb = maMatBang, apartment_code = maSoMb, pass = pass };
                    var param = new Dapper.DynamicParameters();
                    param.AddDynamicParams(model_param);
                    //param.Add("EmployeeId", employeeId);
                    var a = Library.Class.Connect.QueryConnect.QueryAsyncString<bool>("dbo.tbl_user_edit_from_orther_db", VimeService.isPersonal == false? Library.Class.Enum.ConnectString.CONNECT_MYHOME: Library.Class.Enum.ConnectString.CONNECT_STRING, param);

                    db.SubmitChanges();

                    if (typeId == 100)
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
                       // model.isPersonal = VimeService.isPersonal;

                        long uid = Convert.ToInt64(Building.AppVime.CommonVime.GetUID(model.phoneNumber));
                        

                        //using (var db = new MasterDataContext())
                        //{
                            var retval = VimeService.Post(model, "/accounts/registerNoId");
                            string phone = CommonVime.FormatPhone(retval);
                            if (Convert.ToInt64(phone) > 0)
                            {
                                var objRT = db.app_ResidentTowers.FirstOrDefault(p => p.Id == id);
                                objRT.TypeId = 20;

                                objRT.DateOfProcess = db.GetSystemDate();
                                objRT.DescriptionProcess = "Đăng ký lại";
                                objRT.EmployeeIdUpdate = Common.User.MaNV;

                                try
                                {
                                    db.SubmitChanges();

                                    gvResident.SetRowCellValue(i, "StatusId", (byte)20);
                                }
                                catch { }
                            }
                        //}
                    }
                }
            }
            catch { }

            this.LoadData();
        }

        private void itemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Library.Commoncls.ExportExcel(gcResident);
        }

        private void itemAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                var maTN = (byte)itemToaNha.EditValue;
                if (maTN > 0)
                {
                    var frm = new Building.AppVime.NguoiLienHe.frmEdit();
                    frm.IsTower = true;
                    frm.TowerId = maTN;
                    frm.ShowDialog();
                    if (frm.DialogResult == DialogResult.OK)
                    {
                        this.LoadData();
                    }
                }
                else
                {
                    DialogBox.Alert("Vui lòng chọn [Tòa nhà], xin cảm ơn.");
                }
            }
            catch
            {
                DialogBox.Alert("Vui lòng chọn [Tòa nhà], xin cảm ơn.");
            }
        }

        private void itemRegisterMulti_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var maTN = (byte)itemToaNha.EditValue;
            if (maTN > 0)
            {
                var frm = new Building.AppVime.NguoiLienHe.frmRegisterMulti();
                frm.IsTower = true;
                frm.TowerId = (byte)itemToaNha.EditValue;
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                {
                    this.LoadData();
                }
            }
            else
            {
                DialogBox.Alert("Vui lòng chọn [Tòa nhà], xin cảm ơn.");
            }
        }

        private void itemChangePhone_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gvResident.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn [Nhân viên]. Xin cảm ơn.");
                return;
            }

            var frm = new Building.AppVime.Resident.frmChangePhone();
            frm.Id = (decimal)gvResident.GetFocusedRowCellValue("ResidentId");
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                this.LoadData();
            }
        }

        string phoneNumber = "";

        private void gvResident_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            //try
            //{
            //    phoneNumber = gvResident.GetFocusedRowCellValue("Phone").ToString();

            //    LoadDetail();
            //}catch
            //{
            //    phoneNumber = "";
            //    gcCaNhan.DataSource = null;
            //}
        }

        void LoadDetail()
        {
            try
            {
                var maTN = (byte)itemToaNha.EditValue;

                var db = new MasterDataContext();
                gcCaNhan.DataSource = from c in db.tnKhachHangs
                                      join d in db.khNhomKhachHangs on c.MaNKH equals d.ID into tblNhomKH
                                      from d in tblNhomKH.DefaultIfEmpty()
                                      //join lkh in db.khLoaiKhachHangs on c.MaLoaiKH equals lkh.ID into loaiKH
                                      //from lkh in loaiKH.DefaultIfEmpty()
                                      where c.MaTN == maTN && c.DienThoaiKH == phoneNumber
                                      select new
                                      {
                                          c.KyHieu,
                                          c.MaPhu,
                                          c.MaKH,
                                          c.HoKH,
                                          c.TenKH,
                                          GioiTinh = c.GioiTinh.GetValueOrDefault() ? "Nam" : "Nữ",
                                          c.NgaySinh,
                                          c.CMND,
                                          c.DienThoaiKH,
                                          c.EmailKH,
                                          c.DCLL,
                                          c.DCTT,
                                          TenKV = c.MaKV != null ? c.tnKhuVuc.TenKV : "",
                                          c.QuocTich,
                                          c.MaSoThue,
                                          c.tnNhanVien.HoTenNV,
                                          TenNKH = d.TenNKH,
                                          //lkh.TenLoaiKH
                                      };
            }
            catch
            {
                phoneNumber = "";
                gcCaNhan.DataSource = null;
            }
        }

        private void gvResident_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (phoneNumber != gvResident.GetFocusedRowCellValue("Phone").ToString())
                {
                    phoneNumber = gvResident.GetFocusedRowCellValue("Phone").ToString();
                    LoadDetail();
                }
            }
            catch
            {
                phoneNumber = "";
                gcCaNhan.DataSource = null;
            }
        }

        /// <summary>
        /// Xóa
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (gvResident.FocusedRowHandle < 0)
                {
                    DialogBox.Alert("Vui lòng chọn [Người liên hệ]. Xin cảm ơn.");
                    return;
                }

                var id = (decimal?)gvResident.GetFocusedRowCellValue("ResidentId");
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
            catch (System.Exception ex)
            {
                XtraMessageBoxArgs args = new XtraMessageBoxArgs();
                //args.AutoCloseOptions.Delay = 5000;
                args.Caption = ex.GetType().FullName;
                args.Text = ex.Message;
                args.Buttons = new DialogResult[] { DialogResult.OK, DialogResult.Cancel };
                XtraMessageBox.Show(args).ToString();
            }
        }

        private void barButtonItem_ResetPassword_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                var db = new MasterDataContext();
                 var resident_id = (decimal)gvResident.GetFocusedRowCellValue("ResidentId");

                 var resident_item = db.app_Residents.FirstOrDefault(_ => _.Id == resident_id);
                if(resident_item != null)
                {
                    if (DialogBox.Question("Bạn có muốn đổi mật khẩu về mật khẩu mặc định không?") == DialogResult.No) return;

                    resident_item.Password = resident_item.Phone;
                    db.SubmitChanges();
                    DialogResult = DialogResult.OK;
                }
            }
            catch(System.Exception ex) { }
        }

        /// <summary>
        /// Kiểm tra số đã tồn tại trên firebase chưa
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem_KiemTraTonTaiTrenFirebase_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                var uid = (decimal)gvResident.GetFocusedRowCellValue("UID");
               // var data = client.Get("/users");
                var data = client.Get("/users/"+uid.ToString());
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
