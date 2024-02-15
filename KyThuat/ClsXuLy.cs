using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KyThuat
{
    public class ClsXuLy
    {
        public static int CheckKeHoachBaoTri()
        {
            int result = 0;
            using (Library.MasterDataContext db = new Library.MasterDataContext())
            {
                DateTime now = db.GetSystemDate();
                var listKeHoach = db.khbtKeHoaches.ToList();
                foreach (var item in listKeHoach)
                {
                    try
                    {
                        string[] ArrayDayOfWeekInDB = item.DayOfWeeks.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        string[] ArrayMonthOfYearInDB = item.MonthOfYears.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);

                        if (ArrayDayOfWeekInDB.Contains(now.DayOfWeek.ToString())
                            & ArrayMonthOfYearInDB.Contains(now.Month.ToString())
                            & item.TuNgay <= now)
                        {
                            if (item.IsLoop != null)
                            {
                                if (item.IsLoop.Value)
                                {
                                    result++;
                                }

                            }
                        }
                    }
                    catch { }
                }
            }

            return result;
        }
    }
}
