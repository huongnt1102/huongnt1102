using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library
{
    public static class FunctionDateTime
    {
        public static int WeeksInMonth(int year, int month)
        {
            DayOfWeek wkstart = DayOfWeek.Monday;

            DateTime first = new DateTime(year, month, 1);
            int firstwkday = (int)first.DayOfWeek;
            int otherwkday = (int)wkstart;

            int offset = ((otherwkday + 7) - firstwkday) % 7;

            double weeks = (double)(DateTime.DaysInMonth(year, month) - offset) / 7d;

            return (int)Math.Ceiling(weeks);
        } 
    }
}
