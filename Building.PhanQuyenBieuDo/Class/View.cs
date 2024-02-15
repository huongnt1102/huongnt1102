using System.Linq;

namespace Building.PhanQuyenBieuDo.Class
{
    public static class View
    {
        public static DevExpress.XtraEditors.XtraUserControl GetControlForm(string controlName, string dllName)
        {
            try
            {
                if (controlName == null) return null;
                if (dllName == null) return null;
                string absPathContainingHrefs = System.AppDomain.CurrentDomain.BaseDirectory; // Get the "base" path
                string fullPath = System.IO.Path.Combine(absPathContainingHrefs, dllName); // dllName: string @"LandSoftBuildingMain.dll"
                fullPath = System.IO.Path.GetFullPath(fullPath);
                System.Reflection.Assembly dllFile = System.Reflection.Assembly.LoadFile(fullPath);

                System.Type theType = dllFile.GetType(controlName);
                if (theType == null) return null;

                var objectReturn = (DevExpress.XtraEditors.XtraUserControl) System.Activator.CreateInstance(theType);
                return objectReturn;
            }
            catch(System.Exception ex)
            {
                return null;
            }
        }

        public static System.Collections.Generic.List<ControlNames> GetAllUserControlOnDllByDllName(string dllName)
        {
            if (dllName == null) return null;
            string absPathContainingHrefs = System.AppDomain.CurrentDomain.BaseDirectory;
            string fullPath = System.IO.Path.Combine(absPathContainingHrefs, dllName);
            fullPath = System.IO.Path.GetFullPath(fullPath);
            var dll = System.Reflection.Assembly.LoadFile(fullPath);

            System.Collections.Generic.List<ControlNames> nameControl = new System.Collections.Generic.List<ControlNames>();

            System.Type[] typelist = GetTypesInNamespace(dll);
            foreach (var item in typelist)
            {
                if (item.BaseType.FullName == "DevExpress.XtraEditors.XtraUserControl")
                    nameControl.Add(new ControlNames { ControlName = item.Namespace + "." + item.Name });
            }
            

            return nameControl;
        }

        private static System.Type[] GetTypesInNamespace(System.Reflection.Assembly assembly)
        {
            return assembly.GetTypes().ToArray();
        }

        private static System.Type[] GetTypesInNamespace(System.Reflection.Assembly assembly, string nameSpace)
        {
            return
                assembly.GetTypes()
                    .Where(t => System.String.Equals(t.Namespace, nameSpace, System.StringComparison.Ordinal))
                    .ToArray();
        }

        public class ControlNames
        {
            public string ControlName { get; set; }
        }
    }
}
