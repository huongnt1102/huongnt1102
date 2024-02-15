using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Library;

namespace DichVu.GhiSo
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
                    var listKS = db.dvGhiSos.Where(p => p.mbMatBang.mbTangLau.mbKhoiNha.MaTN == TowerID
                        & p.Years == Year
                        & p.Months == Month).FirstOrDefault();

                    if (listKS != null)
                    {
                        DialogBox.Alert(string.Format("[Dự án] này đã ghi sổ tháng {0}/{1}.\r\nVui lòng kiểm tra lại, xin cảm ơn.", Month, Year));
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
                    var listKS = db.dvGhiSos.Where(p => p.mbMatBang.mbTangLau.mbKhoiNha.MaTN == TowerID
                        & p.Years == Year
                        & p.Months == Month).FirstOrDefault();

                    if (listKS != null)
                    {
                        DialogBox.Alert(string.Format("[Dự án] này đã ghi sổ tháng {0}/{1} nên không thể sửa.\r\nVui lòng kiểm tra lại, xin cảm ơn.", Month, Year));
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
                    var listKS = db.dvGhiSos.Where(p => p.mbMatBang.mbTangLau.mbKhoiNha.MaTN == TowerID
                        & p.Years == Year
                        & p.Months == Month
                        & p.IsLock.GetValueOrDefault()).Select(p => p.ID).Count();

                    if (listKS > 0)
                        return true;

                    return false;
                }
            }
            catch { return false; }
        }
    }
}
