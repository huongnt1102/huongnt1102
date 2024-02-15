using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.HeThongCls
{
    public class PhanQuyenCls
    {
        public static System.Collections.Generic.List<PhanQuyen.ControlName> LControlName { get; set; }
        public static int? ParentId { get; set; }
        public static bool IsPhanQuyen { get; set; }
        public static bool KhongPhanQuyen { get; set; }

        public static void Authorize(System.Windows.Forms.Form frm, tnNhanVien objnhanvien, DevExpress.XtraBars.Ribbon.RibbonControl ribbon, DevExpress.XtraNavBar.NavBarControl control)
        {
            if (objnhanvien.IsSuperAdmin.Value) return;
            else
            {
                using (MasterDataContext db = new MasterDataContext())
                {
                    var objNhomNhanVien = db.pqNhomNhanViens.Where(p => p.MaNV == objnhanvien.MaNV).Select(p => p.pqNhom);

                    var ModuleHasPermisson = db.pqAccessRights.Where(p => objNhomNhanVien.Contains(p.pqNhom) & p.IsAccessRight == true).Select(p => p.pqModule);

                    var permissionOnModule = db.pqModule_FormControls.Where(p => p.pqModule == ModuleHasPermisson).Select(p => p.ControlName);

                    if (permissionOnModule.Count() <= 0 & frm.Name != "frmMain")
                        frm.Close();
                    else
                    {
                        var lstModuleHasPermisson = ModuleHasPermisson.ToList();
                        var ControlHasPermisson = db.pqModule_FormControls.Where(p => lstModuleHasPermisson.Contains(p.pqModule)).Select(p => p.ControlName).ToList();

                        var nlstRibbobPage = getAllRibbonPage(ribbon).Where(p => ControlHasPermisson.Contains(p.Name)).ToList();
                        var nlstRibbobPageGroup = getAllRibbonPageGroup(ribbon).Where(p => ControlHasPermisson.Contains(p.Name)).ToList();
                        var nlstBarButtonItem = getAllBarButtonItem(ribbon).Where(p => ControlHasPermisson.Contains(p.Name)).ToList();
                        var nlstNavGroup = getAllNavGroup(control).Where(p => ControlHasPermisson.Contains(p.Name)).ToList();
                        var nlstNavItem = getAllNavItem(control).Where(p => ControlHasPermisson.Contains(p.Name)).ToList();

                        #region Invisble All
                        getAllRibbonPage(ribbon).ForEach(item =>
                        {
                            item.Visible = false;
                        });

                        getAllRibbonPageGroup(ribbon).ForEach(item =>
                        {
                            item.Visible = false;
                        });

                        getAllBarButtonItem(ribbon).ForEach(item =>
                        {
                            item.Enabled = false;
                        });

                        getAllNavGroup(control).ForEach(item =>
                        {
                            item.Visible = false;
                        });

                        getAllNavItem(control).ForEach(item =>
                        {
                            item.Visible = false;
                        });
                        #endregion

                        foreach (DevExpress.XtraBars.BarButtonItem item in nlstBarButtonItem)
                        {
                            item.Enabled = true;
                        }
                        foreach (DevExpress.XtraNavBar.NavBarGroup item in nlstNavGroup)
                        {
                            item.Visible = true;
                        }
                        foreach (DevExpress.XtraNavBar.NavBarItem item in nlstNavItem)
                        {
                            item.Visible = true;
                        }
                        foreach (DevExpress.XtraBars.Ribbon.RibbonPage item in nlstRibbobPage)
                        {
                            item.Visible = true;
                        }
                        foreach (DevExpress.XtraBars.Ribbon.RibbonPageGroup item in nlstRibbobPageGroup)
                        {
                            item.Visible = true;
                        }
                    }
                }
            }
        }

        public static void PhanQuyenRibon(System.Windows.Forms.Form frm, tnNhanVien objnhanvien, DevExpress.XtraBars.Ribbon.RibbonControl ribbon)
        {
            #region Kiểm tra objNhanVien
            var db = new Library.MasterDataContext();
            if (objnhanvien == null)
            {
                Library.Properties.Settings.Default.NgonNgu = "VI";
                var nv = db.tnNhanViens.FirstOrDefault(_ => _.MaNV == 6);
                if (nv == null) return;
                Library.Properties.Settings.Default.Username = nv.MaSoNV;
                Library.Properties.Settings.Default.Password = nv.MatKhau;
                Library.Common.User = nv;
                Library.Common.TowerList = Library.ManagerTowerCls.GetAllTower(nv);

                return;
            }

            #endregion

            #region Invisble All

            HidenAllRibbon(ribbon);

            #endregion

            if (objnhanvien.IsSuperAdmin.Value)
            {
                #region Hiển thị

                var moduleMain = db.pqModules.Where(_ => _.IsInMain == true & _.IsHienThi == true).ToList();
                var moduleParent = moduleMain.Select(_ => _.ModuleID).ToList();
                var moduleChild = db.pqModules.Where(_ => (_.IsInMain == null) & moduleParent.Contains((int)_.ModuleParentID)).ToList();
                var lModuleShow = moduleMain.Union(moduleChild).ToList();
                var lModuleName = lModuleShow.Where(_ => _.Name != null).Select(_ => _.Name).ToList();

                var lPage = getAllRibbonPage(ribbon).Where(_ => lModuleName.Contains(_.Name)).ToList();
                var lPageGroup = getAllRibbonPageGroup(ribbon).Where(_ => lModuleName.Contains(_.Name)).ToList();
                System.Collections.Generic.List<DevExpress.XtraBars.BarButtonItem> lButton = getAllBarButtonItem(ribbon).Where(_ => lModuleName.Contains(_.Name)).ToList();

                foreach (DevExpress.XtraBars.BarButtonItem button in lButton) { button.Enabled = true; button.Visibility = DevExpress.XtraBars.BarItemVisibility.Always; }
                foreach (DevExpress.XtraBars.Ribbon.RibbonPage page in lPage) page.Visible = true;
                foreach (DevExpress.XtraBars.Ribbon.RibbonPageGroup pageGroup in lPageGroup) pageGroup.Visible = true;

                // hiden button admin
                System.Collections.Generic.List<string> buttonHiddenName = new List<string> { "itemPhanQuyenItem" }; //"itemPhanQuyenBieuDoMain", "itemPhanQuyenItem" 
                foreach (var buttonName in buttonHiddenName)
                {
                    DevExpress.XtraBars.BarButtonItem button = lButton.Find(_ => _.Name == buttonName);
                    if (button != null) HidenButtonAdmin(button);
                }

                #endregion
            }
            else
            {
                #region

                //var objNhomNhanVien = db.pqNhomNhanViens.Where(p => p.MaNV == objnhanvien.MaNV).Select(p => p.pqNhom).FirstOrDefault();
                var model_nhomnhanvien = new { manv = objnhanvien.MaNV };
                var param = new Dapper.DynamicParameters();
                param.AddDynamicParams(model_nhomnhanvien);
                var objNhomNhanVien = Library.Class.Connect.QueryConnect.Query<pqNhom>("pqnhomnhanvien_get_by_manv", param).FirstOrDefault();

                //objNhomNhanVien.Contains(p.pqNhom)
                var model_module = new { groupid = objNhomNhanVien.GroupID };
                param = new Dapper.DynamicParameters();
                param.AddDynamicParams(model_module);
                //var moduleHasPermisson = Library.Class.Connect.QueryConnect.Query<pqModule>("pqaccessright_get_by_groupid", param);

                //var moduleHasPermisson = db.pqAccessRights.Where(p => p.GroupID == objNhomNhanVien.GroupID & p.IsAccessRight == true).Select(p => p.pqModule);

                // cập nhật: nếu module form control show thì mới hiển thị
                var permissionOnModule = Library.Class.Connect.QueryConnect.Query<string>("pq_module_formcontrol_get_by_group", param);

                //var permissionOnModule = db.pqModule_FormControls
                //    .Where(p => p.pqModule == moduleHasPermisson & p.pqModule.IsHienThi == true)
                //    .Select(p => p.ControlName);

                if (permissionOnModule.Count() <= 0 & frm.Name != "frmMain")
                    frm.Close();
                else
                {
                    try
                    {
                        //var lstModuleHasPermisson = moduleHasPermisson.ToList();
                        //var controlHasPermisson = db.pqModule_FormControls
                        //    .Where(p => lstModuleHasPermisson.Contains(p.pqModule) & p.pqModule.IsHienThi == true)
                        //    .Select(p => p.ControlName).ToList();

                        var nlstRibbobPage = getAllRibbonPage(ribbon).Where(p => permissionOnModule.Contains(p.Name)).ToList();
                        var nlstRibbobPageGroup = getAllRibbonPageGroup(ribbon).Where(p => permissionOnModule.Contains(p.Name)).ToList();
                        var nlstBarButtonItem = getAllBarButtonItem(ribbon).Where(p => permissionOnModule.Contains(p.Name)).ToList();

                        foreach (DevExpress.XtraBars.BarButtonItem item in nlstBarButtonItem)
                        {
                            item.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                            item.Enabled = true;
                        }

                        foreach (DevExpress.XtraBars.Ribbon.RibbonPage item in nlstRibbobPage)
                        {
                            item.Visible = true;
                        }

                        foreach (DevExpress.XtraBars.Ribbon.RibbonPageGroup item in nlstRibbobPageGroup)
                        {
                            item.Visible = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        Library.DialogBox.Error("Phân quyền chưa đúng, lỗi: " + ex.Message);
                    }
                    
                }

                #endregion

            }
        }

        public static void HidenButtonAdmin(DevExpress.XtraBars.BarButtonItem button)
        {
            button.Enabled = false;
            button.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
        }

        public static void HidenAllRibbon(DevExpress.XtraBars.Ribbon.RibbonControl ribbon)
        {
            getAllRibbonPage(ribbon).ForEach(item => { item.Visible = false; });

            getAllRibbonPageGroup(ribbon).ForEach(item => { item.Visible = false; });

            getAllBarButtonItem(ribbon).ForEach(item =>
            {
                item.Enabled = false;
                item.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            });
        }

        public static void Authorize(System.Windows.Forms.Form frm, tnNhanVien objnhanvien, DevExpress.XtraBars.BarManager barManagerName)
        {
            if (IsPhanQuyen) CreatePhanQuyen(frm, barManagerName);
            if (objnhanvien == null) return;
            if (objnhanvien.IsSuperAdmin != null && objnhanvien.IsSuperAdmin.Value) return;
            else
            {

                using (MasterDataContext db = new MasterDataContext())
                {
                    //var objNhomNhanVien = db.pqNhomNhanViens.Where(p => p.MaNV == objnhanvien.MaNV).Select(p => p.GroupID).ToList();
                    //var moduleHasPermisson = db.pqAccessRights.Where(p => objNhomNhanVien.Contains(p.GroupID.Value) & p.IsAccessRight == true).Select(p => p.pqModule).ToList();

                    //var controlHasPermisson = db.pqModule_FormControls.Where(p => moduleHasPermisson.Contains(p.pqModule) & p.pqForm.FormName == frm.GetType().FullName).Select(p => p.ControlName).ToList();
                    var model_nhomnhanvien = new { manv = objnhanvien.MaNV, full_name = frm.GetType().FullName };
                    var param = new Dapper.DynamicParameters();
                    param.AddDynamicParams(model_nhomnhanvien);
                    var controlHasPermisson = Library.Class.Connect.QueryConnect.Query<string>("pq_authorize", param);

                    getAllBarButtonItem(barManagerName).ForEach(item =>
                    {
                        item.Enabled = false;
                        foreach (var per in controlHasPermisson)
                        {
                            if (per == item.Name) item.Enabled = true;
                        }
                    });

                    getAllBarSubItem(barManagerName).ForEach(item =>
                    {
                        item.Enabled = false;
                        foreach (var per in controlHasPermisson)
                        {
                            if (per == item.Name) item.Enabled = true;
                        }
                    });
                }
            }
        }

        public static void SetIsPhanQuyen(bool? flag)
        {
            if (flag != null) IsPhanQuyen = (bool)flag;
        }

        public static void SetParameterPhanQuyen(DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var module = Library.PhanQuyen.GetModuleByName(e.Item.Name);
            if (module == null) ParentId = 1;
            Library.HeThongCls.PhanQuyenCls.ParentId = module.ModuleID;
            Library.HeThongCls.PhanQuyenCls.LControlName = Library.PhanQuyen.GetListControlName(e);
            Library.PhanQuyen.KhoiTaoMain(ParentId, LControlName);
        }

        private static void CreatePhanQuyen(System.Windows.Forms.Form frm, DevExpress.XtraBars.BarManager barManager1)
        {
            if (LControlName == null) return;

            var lModuleControl = new System.Collections.Generic.List<Library.PhanQuyen.ModuleControl>();


            foreach (var item in barManager1.Items)
            {
                if (item is DevExpress.XtraBars.BarSubItem)
                {
                    var subItem = (DevExpress.XtraBars.BarSubItem)item;
                    bool isVisibly = false;
                    if (subItem.Visibility == DevExpress.XtraBars.BarItemVisibility.Always)
                    {
                        isVisibly = true;
                    }
                    lModuleControl.Add(new Library.PhanQuyen.ModuleControl
                    {
                        ModuleName = subItem.Caption,
                        ModuleDescription = subItem.Caption,
                        ControlNames = subItem.Name,
                        IsVisibly = isVisibly
                    });
                }
                if (item is DevExpress.XtraBars.BarButtonItem)
                {
                    var button = (DevExpress.XtraBars.BarButtonItem)item;
                    bool isVisibly = false;
                    if(button.Visibility == DevExpress.XtraBars.BarItemVisibility.Always 
                        & button.Caption != "" 
                        & button.Caption.Trim().ToLower() != "test"
                        & button.Caption.Trim().ToLower() != "add"
                        & button.Caption.Trim().ToLower() != "delete"
                        & button.Caption.Trim().ToLower() != "save"
                        & button.Caption.Trim().ToLower() != "refresh"
                        & button.Caption.Trim().ToLower() != "admin tool"
                    )
                    {
                        isVisibly = true;
                    }
                    lModuleControl.Add(new Library.PhanQuyen.ModuleControl
                    {
                        ModuleName = button.Caption,
                        ModuleDescription = button.Caption,
                        ControlNames = button.Name,
                        IsVisibly = isVisibly
                    });
                }
            }

            Library.PhanQuyen.CreatePhanQuyen(frm.GetType().Namespace + "." + frm.Name, frm.Text, ParentId, Library.PhanQuyen.ModuleId.FORM_MAIN_ID, LControlName, lModuleControl);
        }

        public static void EnumerateControls(System.Windows.Forms.Control ctrl)
        {
            if (ctrl.Controls.Count > 0)
            {
                foreach (System.Windows.Forms.Control control in ctrl.Controls)
                    EnumerateControls(control);
            }
        }


        public static List<DevExpress.XtraBars.BarButtonItem> getAllBarButtonItem(DevExpress.XtraBars.BarManager BarManagerName)
        {
            List<DevExpress.XtraBars.BarButtonItem> lstBarbuttonItem = new List<DevExpress.XtraBars.BarButtonItem>();
            foreach (var item in BarManagerName.Items)
            {
                if (item is DevExpress.XtraBars.BarButtonItem)
                {
                    lstBarbuttonItem.Add(item as DevExpress.XtraBars.BarButtonItem);
                }
            }

            return lstBarbuttonItem;
        }

        public static List<DevExpress.XtraBars.BarSubItem> getAllBarSubItem(DevExpress.XtraBars.BarManager BarManagerName)
        {
            List<DevExpress.XtraBars.BarSubItem> lstBarbuttonItem = new List<DevExpress.XtraBars.BarSubItem>();
            foreach (var item in BarManagerName.Items)
            {

                if (item is DevExpress.XtraBars.BarSubItem)
                {
                    lstBarbuttonItem.Add(item as DevExpress.XtraBars.BarSubItem);
                }
            }

            return lstBarbuttonItem;
        }

        public static List<DevExpress.XtraBars.BarButtonItem> getAllBarButtonItem(DevExpress.XtraBars.Ribbon.RibbonControl ribbon)
        {
            List<DevExpress.XtraBars.BarButtonItem> lstBarbuttonItem = new List<DevExpress.XtraBars.BarButtonItem>();
            foreach (var item in (ribbon as DevExpress.XtraBars.Ribbon.RibbonControl).Items)
            {
                if (item is DevExpress.XtraBars.BarButtonItem)
                {
                    lstBarbuttonItem.Add(item as DevExpress.XtraBars.BarButtonItem);
                }
            }

            return lstBarbuttonItem;
        }

        public static List<DevExpress.XtraBars.Ribbon.RibbonPage> getAllRibbonPage(DevExpress.XtraBars.Ribbon.RibbonControl ribbon)
        {
            List<DevExpress.XtraBars.Ribbon.RibbonPage> lstRibbonPage = new List<DevExpress.XtraBars.Ribbon.RibbonPage>();
            foreach (var page in ribbon.Pages)
            {
                if (page is DevExpress.XtraBars.Ribbon.RibbonPage)
                {
                    lstRibbonPage.Add(page as DevExpress.XtraBars.Ribbon.RibbonPage);
                }
            }

            return lstRibbonPage;
        }

        public static List<DevExpress.XtraBars.Ribbon.RibbonPageGroup> getAllRibbonPageGroup(DevExpress.XtraBars.Ribbon.RibbonControl ribbon)
        {
            List<DevExpress.XtraBars.Ribbon.RibbonPageGroup> lstRibbonPageGroup = new List<DevExpress.XtraBars.Ribbon.RibbonPageGroup>();
            foreach (DevExpress.XtraBars.Ribbon.RibbonPage page in ribbon.Pages)
            {
                foreach (var pageGroup in page.Groups)
                {
                    if (pageGroup is DevExpress.XtraBars.Ribbon.RibbonPageGroup)
                    {
                        lstRibbonPageGroup.Add(pageGroup as DevExpress.XtraBars.Ribbon.RibbonPageGroup);
                    }
                }
            }

            return lstRibbonPageGroup;
        }

        public static List<DevExpress.XtraNavBar.NavBarGroup> getAllNavGroup(DevExpress.XtraNavBar.NavBarControl control)
        {
            List<DevExpress.XtraNavBar.NavBarGroup> lstnavGroup = new List<DevExpress.XtraNavBar.NavBarGroup>();
            foreach (var item in control.Groups)
            {
                if (item is DevExpress.XtraNavBar.NavBarGroup)
                {
                    lstnavGroup.Add(item as DevExpress.XtraNavBar.NavBarGroup);
                }
            }
            return lstnavGroup;
        }

        public static List<DevExpress.XtraNavBar.NavBarItem> getAllNavItem(DevExpress.XtraNavBar.NavBarControl control)
        {
            List<DevExpress.XtraNavBar.NavBarItem> lstnavItem = new List<DevExpress.XtraNavBar.NavBarItem>();
            foreach (var item in control.Items)
            {
                if (item is DevExpress.XtraNavBar.NavBarItem)
                {
                    lstnavItem.Add(item as DevExpress.XtraNavBar.NavBarItem);
                }
            }
            return lstnavItem;
        }
    }
}
