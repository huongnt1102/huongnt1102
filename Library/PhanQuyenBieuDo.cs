using System.Linq;

namespace Library
{
    public static class PhanQuyenBieuDo
    {
        private static readonly Library.MasterDataContext Db = new Library.MasterDataContext();

        private static Library.pq_BieuDoMain_Control GetControlByName(string controlName)
        {
            return Db.pq_BieuDoMain_Controls.FirstOrDefault(_ => _.ControlName.ToLower() == controlName.ToLower());
        }

        public static void SaveControl(string controlName, string controlCaption, string dllName)
        {
            var control = GetControlByName(controlName);
            if (control == null)
            {
                control = new Library.pq_BieuDoMain_Control {ControlName = controlName};
                Db.pq_BieuDoMain_Controls.InsertOnSubmit(control);
            }

            control.ControlCaption = controlCaption;
            control.DllName = dllName;

            Db.SubmitChanges();
        }
    }
}
