using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Library;

namespace DichVu.KhoaSo
{
    public static class ManagerCls
    {
        public static int ProductID { get; set; }
        public static int Month { get; set; }
        public static int Year { get; set; }
        public static byte TowerID { get; set; }
        public static string TowerName { get; set; }
        public static bool IsFirst { get; set; }
        public static bool IsLock { get; set; }
        public static long KeyID { get; set; }

        public static bool Check()
        {
            try
            {
                using (var db = new MasterDataContext())
                {
                    var listKS = db.dvKhoaSos.Where(p => p.mbMatBang.mbTangLau.mbKhoiNha.MaTN == TowerID
                        & p.Nam == Year
                        & p.Thang == Month
                        & p.IsFirst.GetValueOrDefault() == IsFirst).FirstOrDefault();

                    if (listKS != null)
                    {
                        DialogBox.Alert(string.Format("Dự án [{0}] đã khóa sổ tháng {1}/{2}.\r\nVui lòng kiểm tra lại, xin cảm ơn.", TowerName, Month, Year));
                        return true;
                    }
                }
            }
            catch { }

            return false;
        }

        public static bool CheckEditData()
        {
            try
            {
                using (var db = new MasterDataContext())
                {
                    var listKS = db.dvKhoaSos.Where(p => p.mbMatBang.mbTangLau.mbKhoiNha.MaTN == TowerID
                        & p.Nam == Year
                        & p.Thang == Month
                        & p.IsFirst.GetValueOrDefault() == IsFirst).FirstOrDefault();

                    if (listKS != null)
                    {
                        DialogBox.Alert(string.Format("Dự án [{0}] đã khóa sổ tháng {1}/{2} nên không thể sửa.\r\nVui lòng kiểm tra lại, xin cảm ơn.", TowerName, Month, Year));
                        return true;
                    }
                }
            }
            catch { }

            return false;
        }

        public static bool CheckIsLock()
        {
            try
            {
                using (var db = new MasterDataContext())
                {
                    var listKS = db.dvKhoaSos.Where(p => p.mbMatBang.mbTangLau.mbKhoiNha.MaTN == TowerID
                        & p.Nam == Year
                        & p.Thang == Month
                        & p.IsFirst.GetValueOrDefault() == IsFirst
                        & p.MaMB ==ProductID).FirstOrDefault();

                    if (listKS != null)
                    {
                        IsLock = listKS.IsLock.GetValueOrDefault();
                        return IsLock;
                    }

                    return false;
                }
            }
            catch { return false; }
        }
    }
}
