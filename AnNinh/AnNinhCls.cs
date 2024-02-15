using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnNinh
{
    public class AnNinhCls
    {
        public static void CheckAnNinhJobs(Library.tnNhanVien objnhanvien)
        {
            using (Library.MasterDataContext db = new Library.MasterDataContext())
            {
                DateTime now = db.GetSystemDate();
                var GroupOfUser = db.pqNhomNhanViens.Where(p => p.MaNV == objnhanvien.MaNV).Select(p => p.GroupID);

                if (GroupOfUser.Count() > 0)
                {
                    var KeHoachOfUser = db.AnNinhMapKeHoaches.Where(p => GroupOfUser.Contains(p.GroupID)).Select(p=>p.MaKeHoach).ToList();
                    var lstKeHoachOfUser = db.AnNinhKeHoaches.Where(p => KeHoachOfUser.Contains(p.MaKeHoach)).ToList();
                    List<Library.AnNinhKeHoach> ListOK = new List<Library.AnNinhKeHoach>();
                    foreach (var item in lstKeHoachOfUser)
                    {
                        string[] ArrayDayOfWeekInDB = item.DayOfWeeks.Split(new char[] {',',' '}, StringSplitOptions.RemoveEmptyEntries);
                        string[] ArrayMonthOfYearInDB = item.MonthOfYears.Split(new char[] { ',',' ' }, StringSplitOptions.RemoveEmptyEntries);

                        if (ArrayDayOfWeekInDB.Contains(now.DayOfWeek.ToString())
                            & ArrayMonthOfYearInDB.Contains(now.Month.ToString())
                            & item.NgayBatDau <= now)
                        {
                            if (item.IsLoop != null)
                            {
                                if (item.IsLoop.Value | (!item.IsLoop.Value & item.NgayKetThuc >= now))
                                {
                                    ListOK.Add(item);
                                }
                            }
                            else
                            {
                                if (item.NgayKetThuc >= now)
                                    ListOK.Add(item);
                            }
                        }
                    }

                    if (ListOK.Count > 0)
                    {
                        frmShowKeHoach frm = new frmShowKeHoach() { objnhanvien = objnhanvien, lstKeHoach = ListOK };
                        frm.Show();
                    }
                }
            }
        }

        private static DateTime TimeOnly(DateTime source)
        {
            return new DateTime(0, 0, 0, source.Hour, source.Minute, source.Second);
        }
    }
}
