using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Globalization;

namespace Library
{
    public static class ExtensionMethod
    {
        
        public static DateTime StartDateOfTheMonth(this DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, 1);
        }
        public static DateTime EndDateOfCurrentMonth(this DateTime dt)
        {
            return DateTime.Now.StartDateOfTheMonth().AddDays(DateTime.DaysInMonth(dt.Year, dt.Month) - 1);
        }
        public static DateTime AddBusinessDays(this DateTime date, int days)
        {
            double sign = Convert.ToDouble(Math.Sign(days));
            int unsignedDays = Math.Sign(days) * days;
            for (int i = 0; i < unsignedDays; i++)
            {
                do
                {
                    date = date.AddDays(sign);
                }
                while (date.DayOfWeek == DayOfWeek.Saturday ||
                    date.DayOfWeek == DayOfWeek.Sunday);
            }
            return date;
        }
        public static int GetWeekNumber(this DateTime date)
        {
            //Constants
            const int JAN = 1;
            const int DEC = 12;
            const int LASTDAYOFDEC = 31;
            const int FIRSTDAYOFJAN = 1;
            const int THURSDAY = 4;
            bool thursdayFlag = false;

            //Get the day number since the beginning of the year
            int dayOfYear = date.DayOfYear;

            //Get the first and last weekday of the year
            int startWeekDay = (int)(new DateTime(date.Year, JAN, FIRSTDAYOFJAN)).DayOfWeek;
            int endWeekDay = (int)(new DateTime(date.Year, DEC, LASTDAYOFDEC)).DayOfWeek;

            //Compensate for using monday as the first day of the week
            if (startWeekDay == 0)
            {
                startWeekDay = 7;
            }
            if (endWeekDay == 0)
            {
                endWeekDay = 7;
            }

            //Calculate the number of days in the first week
            int daysInFirstWeek = 8 - (startWeekDay);

            //Year starting and ending on a thursday will have 53 weeks
            if (startWeekDay == THURSDAY || endWeekDay == THURSDAY)
            {
                thursdayFlag = true;
            }

            //We begin by calculating the number of FULL weeks between
            //the year start and our date. The number is rounded up so
            //the smallest possible value is 0.
            int fullWeeks = (int)Math.Ceiling((dayOfYear - (daysInFirstWeek)) / 7.0);
            int result = fullWeeks;

            //If the first week of the year has at least four days, the
            //actual week number for our date can be incremented by one.
            if (daysInFirstWeek >= THURSDAY)
            {
                result = result + 1;
            }

            //If the week number is larger than 52 (and the year doesn't
            //start or end on a thursday), the correct week number is 1.
            if (result > 52 && !thursdayFlag)
            {
                result = 1;
            }

            //If the week number is still 0, it means that we are trying
            //to evaluate the week number for a week that belongs to the
            //previous year (since it has 3 days or less in this year).
            //We therefore execute this function recursively, using the
            //last day of the previous year.
            if (result == 0)
            {
                result = GetWeekNumber(new DateTime(date.Year - 1, DEC, LASTDAYOFDEC));
            }

            return result;
        }
        public static DateTime GetFirstDateOfWeek(this DateTime date)
        {
            if (date == DateTime.MinValue)
            {
                return date;
            }

            int week = date.GetWeekNumber();

            while (week == date.GetWeekNumber())
            {
                date = date.AddDays(-1);
            }

            return date.AddDays(1);
        }
        public static DateTime GetLastDateOfWeek(this DateTime date)
        {
            if (date == DateTime.MaxValue)
            {
                return date;
            }

            int week = date.GetWeekNumber();

            while (week == date.GetWeekNumber())
            {
                date = date.AddDays(1);
            }

            return date.AddDays(-1);
        }
        public static int GetWeekInYear(this DateTime date)
        {
            CultureInfo cul = CultureInfo.CurrentCulture;

            var firstDayWeek = cul.Calendar.GetWeekOfYear(
                date,
                CalendarWeekRule.FirstDay,
                DayOfWeek.Monday);

            int weekNum = cul.Calendar.GetWeekOfYear(
                date,
                CalendarWeekRule.FirstDay,
                DayOfWeek.Monday);
            return weekNum;
           // int year = weekNum == 52 && d.Month == 1 ? d.Year - 1 : d.Year;
        }
        public static int GetWeekInMonth(this DateTime date)
        {
            DateTime tempdate = date.AddDays(-date.Day + 1);

            CultureInfo ciCurr = CultureInfo.CurrentCulture;
            int weekNumStart = ciCurr.Calendar.GetWeekOfYear(tempdate, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            int weekNum = ciCurr.Calendar.GetWeekOfYear(date, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

            return weekNum - weekNumStart + 1;

        }
    }
}
