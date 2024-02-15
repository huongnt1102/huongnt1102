using System;
using System.Windows.Forms;
using Library;
using System.Linq;
using DevExpress.XtraEditors;
using System.Diagnostics;
using System.Drawing;

namespace Building.AppVime.Tower
{
    public partial class frmManager : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public tnNhanVien objNV;

        public frmManager()
        {
            InitializeComponent();
            db = new MasterDataContext();
            TranslateLanguage.TranslateControl(this, barManager1);
        }

        void LoadData()
        {
            db = new MasterDataContext();

            var listIds = Common.TowerList.Select(p => p.MaTN);

            gcToaNha.DataSource = (from tsp in db.app_TowerSettingPages
                                   join tn in db.tnToaNhas on tsp.Id equals tn.MaTN
                                   where listIds.Contains(tn.MaTN)
                                   select new
                                   {
                                       tsp.Address,
                                       tsp.Banner,
                                       tsp.DisplayName,
                                       tsp.Hotline,
                                       tsp.Id,
                                       tsp.IsAutoConfirm,
                                       tsp.Logo,
                                       tn.TenTN,
                                       tn.Email,
                                       tn.NguoiDaiDien,
                                       tn.WebSite,
                                       IsUseEWallet = tsp.IsUseEWallet.GetValueOrDefault()
                                   });
        }

        void Edit()
        {
            if (grvToaNha.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn [Tòa nhà], xin cảm ơn.");
                return;
            }
            var f = new frmEdit();
            f.TowerId = (byte?)grvToaNha.GetFocusedRowCellValue("Id");
            f.ShowDialog();
            if (f.DialogResult == System.Windows.Forms.DialogResult.OK)
                LoadData();
        }

        private void frmBuilding_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);

            itemSettingRule_Del.Enabled = itemSettingRule_Edit.Enabled = itemSetupGeneral.Enabled;

            //itemTimeNow.Caption = string.Format("{0:dd/MM/yyyy HH:mm}", db.GetSystemDate());

            LoadData();
        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvToaNha.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn [Dự án], xin cảm ơn.");
                return;
            }

            if (DialogBox.QuestionDelete() == DialogResult.No) return;

            try
            {
                var obj = db.app_TowerSettingPages.FirstOrDefault(p => p.Id == (byte)grvToaNha.GetFocusedRowCellValue("Id"));
                db.app_TowerSettingPages.DeleteOnSubmit(obj);

                var objDetail = db.app_TowerSettingPageDetails.FirstOrDefault(p => p.Id == (byte)grvToaNha.GetFocusedRowCellValue("Id"));
                db.app_TowerSettingPageDetails.DeleteOnSubmit(objDetail);

                db.SubmitChanges();
                grvToaNha.DeleteSelectedRows();
                LoadData();
            }
            catch
            {
                DialogBox.Alert("Không xóa được Dự án vì bị ràng buộc dữ liệu");
            }
        }

        private void itemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var f = new frmEdit();
            f.ShowDialog();
            if (f.DialogResult == System.Windows.Forms.DialogResult.OK)
                LoadData();
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void grvToaNha_DoubleClick(object sender, EventArgs e)
        {
            if (itemEdit.Enabled)
                Edit();
        }

        private void itemEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Edit();
        }

        void Clicks()
        {
            if (grvToaNha.FocusedRowHandle < 0)
            {
                picLogo.Image = null;
                picBanner.Image = null;
                gcDepartment.DataSource = null;
                gcRule.DataSource = null;
                htmlIntro.DocumentText = "";
                gcService.DataSource = null;
                gcEmployee.DataSource = null;
                return;
            }

            db = new MasterDataContext();

            switch (tabMain.SelectedTabPageIndex)
            {
                case 0:
                    var wait = DialogBox.WaitingForm();
                    wait.SetCaption("Đang tải hình. Vui lòng chờ...");
                    try
                    {
                        picLogo.Image = LoadImage(grvToaNha.GetFocusedRowCellValue("Logo").ToString());
                        picBanner.Image = LoadImage(grvToaNha.GetFocusedRowCellValue("Banner").ToString());
                    }
                    catch
                    {
                        picLogo.Image = null;
                        picBanner.Image = null;
                        wait.Close();
                        wait.Dispose();
                    }

                    if (!wait.IsDisposed)
                    {
                        wait.Close();
                        wait.Dispose();
                    }

                    break;

                case 1:
                    try
                    {
                        var objDetail = db.app_TowerSettingPageDetails.FirstOrDefault(p => p.Id == (byte)grvToaNha.GetFocusedRowCellValue("Id"));
                        var str = "";
                        if (objDetail != null)
                        {
                            str = "<html><head><meta content=\"width=device-width, initial-scale=1\" name=\"viewport\"></head><body>" + objDetail.ContentGeneral + "</body></html>";
                        }
                        htmlIntro.DocumentText = str;
                    }
                    catch
                    {
                        htmlIntro.DocumentText = "Lỗi chi tiết";
                    }
                    break;
                case 2:
                    LoadDepartment();
                    break;

                case 3:
                    LoadRule();
                    break;

                case 4:
                    LoadService();
                    break;

                case 5:
                    LoadEmployee();
                    break;
            }
        }

        void LoadEmployee()
        {
            gcEmployee.DataSource = from r in db.app_Residents
                                    join nv in db.tnNhanViens on r.EmployeeIdRefer equals nv.MaNV
                                    join nvn in db.tnNhanViens on r.EmployeeId equals nvn.MaNV
                                    join dp in db.app_EmployeeDepartments on r.EmployeeIdRefer equals dp.EmployeeId
                                    join pb in db.tnPhongBans on dp.DepartmentId equals pb.MaPB
                                    where dp.TowerId == (byte)grvToaNha.GetFocusedRowCellValue("Id")
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
                                        Department = pb.TenPB,
                                        Role = nv.IsSuperAdmin.GetValueOrDefault() ? "Supper Admin" : (dp.IsAdminTower.GetValueOrDefault() ? "Admin Tower" : (dp.IsAdmin.GetValueOrDefault() ? "Leader" : "Staff"))
                                    };
        }

        void LoadRule()
        {
            gcRule.DataSource = (from tr in db.app_TowerSettingRules
                                 join r in db.app_TowerRules on tr.RuleId equals r.Id
                                 where tr.TowerId == (byte)grvToaNha.GetFocusedRowCellValue("Id")
                                 select new
                                 {
                                     tr.Id,
                                     IsFile = (tr.FileUrl ?? "").Trim().Equals("") ? false : true,
                                     RuleName = r.Name,
                                     //r.NameEN,
                                     tr.FileUrl
                                 });
        }

        void LoadService()
        {
            try
            {
                using (var db = new MasterDataContext())
                {

                    var list = (from p in db.app_TowerSettingServices
                                join ss in db.app_SettingServices on p.ServiceId equals ss.Id into dichVu
                                from ss in dichVu.DefaultIfEmpty()
                                join dv in db.dvLoaiDichVus on ss.Id equals dv.ID into loaiDichVu
                                from dv in loaiDichVu.DefaultIfEmpty()
                                join gr in db.dvLoaiDichVus on dv.ParentID equals gr.ID into tempParent
                                from gr in tempParent.DefaultIfEmpty()
                                join u in db.DonViTinhs on p.UnitId equals u.ID into tempUnit
                                from u in tempUnit.DefaultIfEmpty()
                                join nv in db.tnNhanViens on p.EmployeeIdModify equals nv.MaNV into nhanVien
                                from nv in nhanVien.DefaultIfEmpty()
                                join type in db.app_TypeOfServices on p.TypeId equals type.Id into loai
                                from type in loai.DefaultIfEmpty()
                                join pb in db.tnPhongBans on p.DepartmentId equals pb.MaPB into phongBan
                                from pb in phongBan.DefaultIfEmpty()
                                where p.TowerId == Convert.ToInt32(grvToaNha.GetFocusedRowCellValue("Id"))
                                select new
                                {
                                    p.Id,
                                    Name = dv.TenHienThi,
                                    Group = gr != null ? gr.TenHienThi : gr.TenHienThi,
                                    p.Amount,
                                    p.ContentReminder,
                                    p.DateModify,
                                    p.Deposit,
                                    p.NumberIndex,
                                    p.PreregistrationTime,
                                    p.Price,
                                    Type = type.Name,
                                    p.TypeId,
                                    Unit = u.TenDVT,
                                    EmployeeName = nv.HoTenNV,
                                    p.ServiceId,
                                    Department = pb.TenPB,
                                    ParentId = dv != null ? dv.ParentID ?? dv.ID: 0,
                                    IsParent = dv!= null?  dv.ParentID ?? 0 : 0
                                }).ToList();

                    gcService.DataSource = list.OrderBy(p => p.Type).ThenBy(p => p.ParentId).ThenBy(p => p.NumberIndex);
                    gvService.ExpandAllGroups();
                }
            }
            catch(System.Exception ex) { }
            
        }

        private void grvToaNha_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            Clicks();
        }

        public System.Drawing.Bitmap LoadImage(string imgUrl)
        {
            try
            {
                return new System.Drawing.Bitmap(new System.IO.MemoryStream(new System.Net.WebClient().DownloadData(imgUrl)));
            }
            catch
            {
                return null;
            }
        }

        private void tabMain_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            Clicks();
        }

        private void itemSetupFunction_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvToaNha.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn [Tòa nhà], xin cảm ơn.");
                return;
            }
            var f = new frmSetupFunction();
            f.TowerId = (byte)grvToaNha.GetFocusedRowCellValue("Id");
            f.ShowDialog();
        }

        private void itemSetupGroup_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvToaNha.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn [Tòa nhà], xin cảm ơn.");
                return;
            }
            var f = new Department.frmManager();
            f.TowerId = (byte)grvToaNha.GetFocusedRowCellValue("Id");
            f.ShowDialog();

            Clicks();
        }

        void LoadDepartment()
        {
            gcDepartment.DataSource = (from d in db.app_EmployeeDepartments
                                       join pb in db.tnPhongBans on d.DepartmentId equals pb.MaPB
                                       join nv in db.tnNhanViens on d.EmployeeId equals nv.MaNV
                                       where d.TowerId == (byte)grvToaNha.GetFocusedRowCellValue("Id")
                                       select new
                                       {
                                           d.Id,
                                           IsAdmin = d.IsAdmin.GetValueOrDefault(),
                                           EmployeeName = nv.HoTenNV,
                                           Name = pb.TenPB,
                                           Role = nv.IsSuperAdmin.GetValueOrDefault() ? "Supper Admin" : (d.IsAdminTower.GetValueOrDefault() ? "Admin Tower" : (d.IsAdmin.GetValueOrDefault() ? "Leader" : "Staff"))
                                       });
        }

        private void itemSetupGeneral_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvToaNha.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn [Tòa nhà], xin cảm ơn.");
                return;
            }
            var f = new frmEditRule();
            f.TowerId = (byte)grvToaNha.GetFocusedRowCellValue("Id");
            f.ShowDialog();
            if (f.DialogResult == DialogResult.OK)
                Clicks();
        }

        private void gvRule_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Column.FieldName.Equals("IsFile"))
            {
                if ((bool)gvRule.GetFocusedRowCellValue("IsFile"))
                    Process.Start(gvRule.GetFocusedRowCellValue("FileUrl").ToString());
            }
        }

        private void itemSettingRule_Edit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gvRule.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn [Cài đặt], xin cảm ơn.");
                return;
            }
            var f = new frmEditRule();
            f.Id = (int)gvRule.GetFocusedRowCellValue("Id");
            f.TowerId = (byte)grvToaNha.GetFocusedRowCellValue("Id");
            f.ShowDialog();
            if (f.DialogResult == DialogResult.OK)
                Clicks();
        }

        private void itemSettingRule_Del_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gvRule.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn [Cài đặt], xin cảm ơn.");
                return;
            }

            if (DialogBox.Question("Bạn có chắc chắn muốn xóa không?") == DialogResult.No) return;

            try
            {
                db = new MasterDataContext();
                var objRule = db.app_TowerSettingRules.FirstOrDefault(p => p.Id == (int)gvRule.GetFocusedRowCellValue("Id"));
                db.app_TowerSettingRules.DeleteOnSubmit(objRule);
                db.SubmitChanges();
                gvRule.DeleteRow(gvRule.FocusedRowHandle);
            }
            catch(Exception ex) { }
        }

        private void itemSettingService_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvToaNha.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn [Tòa nhà], xin cảm ơn.");
                return;
            }

            var f = new frmSettingService();
            f.TowerId = (byte)grvToaNha.GetFocusedRowCellValue("Id");
            f.ShowDialog();
            if (f.DialogResult == DialogResult.OK)
                Clicks();
        }

        private void itemServiceAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvToaNha.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn [Tòa nhà], xin cảm ơn.");
                return;
            }

            var f = new frmSettingService();
            f.TowerId = Convert.ToInt32(grvToaNha.GetFocusedRowCellValue("Id"));
            f.ShowDialog();
            if (f.DialogResult == DialogResult.OK)
                Clicks();
        }

        private void itemServiceEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gvService.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn [Dịch vụ], xin cảm ơn.");
                return;
            }

            var f = new frmSettingService();
            f.Id = (int)gvService.GetFocusedRowCellValue("Id");
            f.TowerId = Convert.ToInt32(grvToaNha.GetFocusedRowCellValue("Id"));
            f.ServiceId = Convert.ToInt32(gvService.GetFocusedRowCellValue("ServiceId"));
            f.ShowDialog();
            if (f.DialogResult == DialogResult.OK)
                Clicks();
        }

        private void itemServiceDel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gvService.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn [Dịch vụ], xin cảm ơn.");
                return;
            }

            if (DialogBox.Question("Bạn có chắc chắn muốn xóa không?") == DialogResult.No) return;

            try
            {
                db = new MasterDataContext();
                var obj = db.app_TowerSettingServices.FirstOrDefault(p => p.Id == (int)gvService.GetFocusedRowCellValue("Id"));
                db.app_TowerSettingServices.DeleteOnSubmit(obj);
                db.SubmitChanges();
                Clicks();
            }
            catch { }
        }

        private void itemSettingZone_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gvService.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn [Dịch vụ], xin cảm ơn.");
                return;
            }

            try
            {
                var f = new frmZone();
                f.TowerId = Convert.ToInt32(grvToaNha.GetFocusedRowCellValue("Id"));
                f.ServiceId = Convert.ToInt32(gvService.GetFocusedRowCellValue("ServiceId"));
                f.Datasource = gcService.DataSource;
                f.ShowDialog();

                LoadService();
            }
            catch
            {

            }
        }

        private void gvService_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.RowHandle < 0) return;

            if (Convert.ToInt32(gvService.GetRowCellValue(e.RowHandle, "IsParent")) == 0)
            {
                if (e.Column.FieldName == "Name")
                {
                    e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                }
            }else
            {
                if (Convert.ToInt32(gvService.GetRowCellValue(e.RowHandle, "TypeId")) == 0)
                {
                    if (e.Column.FieldName == "Name")
                    {
                        e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                    }
                }
            }
        }

        private void itemSetupGroupRule_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var f = new Rules.frmManager();
            f.ShowDialog();
        }

        private void itemMomo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvToaNha.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn [Tòa nhà], xin cảm ơn.");
                return;
            }

            var f = new frmConfigMomo();
            f.TowerId = Convert.ToInt32(grvToaNha.GetFocusedRowCellValue("Id"));
            f.ShowDialog();
            if (f.DialogResult == DialogResult.OK)
                LoadData();
        }

        private void itemCaiDatNgayDichVu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var f = new frmConfigDate();
            f.TowerId = (byte?)grvToaNha.GetFocusedRowCellValue("Id");
            f.ShowDialog();
            LoadData();
        }
    }
}