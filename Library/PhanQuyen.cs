using System.Linq;

namespace Library
{
    public static class PhanQuyen
    {
        private static readonly Library.MasterDataContext Db = new Library.MasterDataContext();

        public class ModuleId
        {
            public const int FORM_MAIN_ID = 92;
            public const int HOP_DONG_THUE_NGOAI_ID = 1509; // nhánh trong dịch vụ
            public const int PHAN_QUYEN_ID = 1904; // nhánh trong dự án
            public const string FORM_MAIN_NAME = "LandSoftBuildingMain.frmMain";
        }
        
        public static Library.pqForm CreateForm(string formName, string dienGiai)
        {
            var pqForm = Db.pqForms.FirstOrDefault(_ => _.FormName.ToLower() == formName.ToLower());
            if (pqForm == null)
            {
                pqForm = new Library.pqForm();
                Db.pqForms.InsertOnSubmit(pqForm);
            }

            pqForm.FormName = formName;
            pqForm.DienGiai = dienGiai;
            Db.SubmitChanges();

            return pqForm;
        }

        public static Library.pqModule CreateModule(string moduleName, string dienGiai, int? moduleParentId)
        {
            var module = Db.pqModules.FirstOrDefault(_ => _.ModuleName.ToLower() == moduleName.ToLower() & _.ModuleParentID == moduleParentId);
            if (module == null)
            {
                module = new Library.pqModule();
                Db.pqModules.InsertOnSubmit(module);
            }

            module.ModuleName = moduleName;
            module.DienGiai = dienGiai;
            module.ModuleParentID = moduleParentId;

            Db.SubmitChanges();

            return module;
        }

        public static Library.pqModule GetModuleByName(string name)
        {
            var module = Db.pqModules.FirstOrDefault(_ => _.Name.ToLower() == name.ToLower());
            if (module == null)
            {
                module = new Library.pqModule();
                module.Name = name;
                module.IsInMain = true;
                module.IsHienThi = false;
                module.ModuleName = name;
                module.DienGiai = name;
                module.ModuleParentID = 1;
                Db.pqModules.InsertOnSubmit(module);
                Db.SubmitChanges();
            }

            return module;
        }

        public static Library.pqModule GetModuleByName(string name, string moduleName)
        {
            var module = Db.pqModules.FirstOrDefault(_ => _.Name.ToLower() == name.ToLower());
            if (module == null)
            {
                module = new Library.pqModule();
                module.Name = name;
                module.IsInMain = true;
                module.IsHienThi = false;
                module.ModuleParentID = 1;
                Db.pqModules.InsertOnSubmit(module);
                Db.SubmitChanges();
            }

            module.ModuleName = moduleName;
            module.DienGiai = moduleName;

            return module;
        }

        public static Library.pqModule GetModuleById(int? id)
        {
            return Db.pqModules.FirstOrDefault(_ => _.ModuleID == id);
        }

        public static Library.pqModule_FormControl CreateControl(int? formId, int? moduleId, string controlName)
        {
            var control = Db.pqModule_FormControls.FirstOrDefault(_ => _.FormID == formId & _.ModuleID == moduleId & _.ControlName.ToLower() == controlName.ToLower());
            if (control != null) //return null;
            {
                control.ControlName = controlName;
                control.FormID = formId;
                control.ModuleID = moduleId;
                Db.SubmitChanges();
            }
            else
            {
                control = new Library.pqModule_FormControl { ControlName = controlName, FormID = formId, ModuleID = moduleId };
                Db.pqModule_FormControls.InsertOnSubmit(control);
                Db.SubmitChanges();
            }

            return control;
        }

        public static void CreateModuleControl(int? formId, int? moduleParentId, string moduleName, string moduleDecription, string controlName)
        {
            var module = Library.PhanQuyen.CreateModule(moduleName, moduleDecription, moduleParentId);
            Library.PhanQuyen.CreateControl(formId, module.ModuleID, controlName);
        }

        public static void CreateControlMain(int? formId, int? moduleParentId, System.Collections.Generic.List<Library.PhanQuyen.ControlName> lControlName)
        {
            foreach (var item in lControlName)
                Library.PhanQuyen.CreateControl(formId, moduleParentId, item.Name);
        }

        public static Library.pqForm GetFormMainId()
        {
            return Db.pqForms.FirstOrDefault(_=>_.FormName.ToLower() == Library.PhanQuyen.ModuleId.FORM_MAIN_NAME.ToLower());
        }

       public static void CreatePhanQuyen(string formName, string formDescription, int? mouduleParentId, int? formMainId, System.Collections.Generic.List<Library.PhanQuyen.ControlName> lControlName, System.Collections.Generic.List<Library.PhanQuyen.ModuleControl> lModuleControl)
        {
            // module tổng
            var form = Library.PhanQuyen.CreateForm(formName, formDescription);
            //var module = Library.PhanQuyen.CreateModule(formDescription, formDescription, mouduleParentId);
            //var module = Library.PhanQuyen.GetModuleById(mouduleParentId);

            // khởi tạo main
            var formMain = KhoiTaoMain(mouduleParentId, lControlName);
            if (formMain == null) return;

            // khởi tạo module control
            foreach (var item in lModuleControl)
            {
                if(item.IsVisibly == false)
                {
                    // delete

                    var module = Db.pqModules.FirstOrDefault(_ => _.ModuleName.ToLower() == item.ModuleName.ToLower() & _.ModuleParentID == mouduleParentId);
                    if (module != null)
                    {
                        var control = Db.pqModule_FormControls.Where(_ => _.FormID == form.ID & _.ModuleID == module.ModuleID & _.ControlName.ToLower() == item.ControlNames.ToLower());
                        if (control.Count() > 0) //return null;
                        {
                            Db.pqModule_FormControls.DeleteAllOnSubmit(control);
                            Db.SubmitChanges();
                        }

                        Db.pqModules.DeleteOnSubmit(module);
                        Db.SubmitChanges();
                    }

                }
                if(item.IsVisibly == true)
                {
                    // Thêm hoặc update
                    Library.PhanQuyen.CreateModuleControl(form.ID, mouduleParentId, item.ModuleName, item.ModuleDescription, item.ControlNames);
                }
            }
                
        }

       public static Library.pqForm KhoiTaoMain(int? mouduleParentId, System.Collections.Generic.List<Library.PhanQuyen.ControlName> lControlName)
       {
           var formMain = GetFormMainId();
           if (formMain == null) return null;
           Library.PhanQuyen.CreateControlMain(formMain.ID, mouduleParentId, lControlName);

           return formMain;
       }

       public static System.Collections.Generic.List<Library.PhanQuyen.ControlName> GetListControlName(DevExpress.XtraBars.ItemClickEventArgs e)
       {
           try
           {
               // button
               if (e.Item.Links[0].Links is DevExpress.XtraBars.Ribbon.RibbonPageGroupItemLinkCollection) return GetListByRibbonPageGroupItem(e);
               else if (e.Item.Links[0].Links is DevExpress.XtraBars.BarItemLinkCollection) return GetListByBarItemLink(e);
               else Library.DialogBox.Alert(e.Item.Links[0].Links.GetType().FullName);

           }
           catch (System.Exception ex)
           {
               Library.DialogBox.Error(ex.Message);
           }

           return null;
       }

       private static System.Collections.Generic.List<Library.PhanQuyen.ControlName> GetListByRibbonPageGroupItem(DevExpress.XtraBars.ItemClickEventArgs e)
       {
            //var item = new System.Collections.Generic.List<PhanQuyen.ControlName>();
            //var item1 = new Library.PhanQuyen.ControlName { Name = ((DevExpress.XtraBars.Ribbon.RibbonPageGroupItemLinkCollection)e.Item.Links[0].Links).PageGroup.Page.Name };
            //var item2 = new Library.PhanQuyen.ControlName { Name = ((DevExpress.XtraBars.Ribbon.RibbonPageGroupItemLinkCollection)e.Item.Links[0].Links).PageGroup.Name };
            //var item3 = new Library.PhanQuyen.ControlName { Name = e.Item.Name };
            //item.Add(item1);
            //item.Add(item2);
            //item.Add(item3);

            //return item;

            return new System.Collections.Generic.List<PhanQuyen.ControlName>
           {
               new Library.PhanQuyen.ControlName {Name = ((DevExpress.XtraBars.Ribbon.RibbonPageGroupItemLinkCollection) e.Item.Links[0].Links).PageGroup.Page.Name},
               new Library.PhanQuyen.ControlName {Name = ((DevExpress.XtraBars.Ribbon.RibbonPageGroupItemLinkCollection) e.Item.Links[0].Links).PageGroup.Name},
               new Library.PhanQuyen.ControlName {Name = e.Item.Name}
           };
        }

       private static System.Collections.Generic.List<Library.PhanQuyen.ControlName> GetListByBarItemLink(DevExpress.XtraBars.ItemClickEventArgs e)
       {
           DevExpress.XtraBars.BarItemLink barItemLink = e.Item.Links[0].Links[0];
           DevExpress.XtraBars.BarSubItem barSubItem =
               (DevExpress.XtraBars.BarSubItem)e.Item.Links[0].Links[0].LinkedObject;
           if (barSubItem.Links[0].LinkedObject is DevExpress.XtraBars.Ribbon.RibbonPageGroupItemLinkCollection)
           {
               return GetListByRibbonPageGroupItemLink(barSubItem, e.Item.Name);//barItemLink.Item.Name
           }
           else
           {
               if (barSubItem.Links[0].LinkedObject is DevExpress.XtraBars.BarSubItem)
               {
                   var item = barSubItem.ItemLinks[0];

                   var subInSub = (DevExpress.XtraBars.BarSubItem)barSubItem.Links[0].LinkedObject;

                   DevExpress.XtraBars.Ribbon.RibbonPageGroupItemLinkCollection groupItemLinkCollection = (DevExpress.XtraBars.Ribbon.RibbonPageGroupItemLinkCollection)subInSub.Links[0].LinkedObject;

                    //var item1 = new System.Collections.Generic.List<Library.PhanQuyen.ControlName>();
                    //var item2 = new Library.PhanQuyen.ControlName { Name = groupItemLinkCollection.PageGroup.Page.Name }; // page
                    //var item3 = new Library.PhanQuyen.ControlName { Name = groupItemLinkCollection.PageGroup.Name }; // group
                    //var item4 = new Library.PhanQuyen.ControlName { Name = subInSub.Name }; // sub tin tức
                    //var item5 = new Library.PhanQuyen.ControlName { Name = barSubItem.Name };
                    //var item6 = new Library.PhanQuyen.ControlName { Name = item.Item.Name };

                    //item1.Add(item2);
                    //item1.Add(item3);
                    //item1.Add(item4);
                    //item1.Add(item5);
                    //item1.Add(item6);
                    //return item1;

                    return new System.Collections.Generic.List<Library.PhanQuyen.ControlName>
                   {
                       new Library.PhanQuyen.ControlName {Name = groupItemLinkCollection.PageGroup.Page.Name}, // page
                       new Library.PhanQuyen.ControlName {Name = groupItemLinkCollection.PageGroup.Name}, // group
                       new Library.PhanQuyen.ControlName {Name = subInSub.Name}, // sub tin tức
                       new Library.PhanQuyen.ControlName {Name = barSubItem.Name},
                       new Library.PhanQuyen.ControlName { Name = item.Item.Name}
                   };
                }
           }

           return null;
       }

       private static System.Collections.Generic.List<Library.PhanQuyen.ControlName> GetListByRibbonPageGroupItemLink(DevExpress.XtraBars.BarSubItem barSubItem, string barItemLink)
       {
           DevExpress.XtraBars.Ribbon.RibbonPageGroupItemLinkCollection groupItemLinkCollection = (DevExpress.XtraBars.Ribbon.RibbonPageGroupItemLinkCollection)barSubItem.Links[0].LinkedObject;

           return new System.Collections.Generic.List<Library.PhanQuyen.ControlName>
           {
               new Library.PhanQuyen.ControlName {Name = groupItemLinkCollection.PageGroup.Page.Name},
               new Library.PhanQuyen.ControlName {Name = groupItemLinkCollection.PageGroup.Name},
               new Library.PhanQuyen.ControlName {Name = barSubItem.Name},
               new Library.PhanQuyen.ControlName {Name = barItemLink}
           };
       }

        public class ControlName
        {
            public string Name { get; set; }
        }

        public class ModuleControl
        {
            public string ModuleName { get; set; }
            public string ModuleDescription { get; set; }
            public string ControlNames { get; set; }
            public bool? IsVisibly { get; set; }
        }
    }
}
