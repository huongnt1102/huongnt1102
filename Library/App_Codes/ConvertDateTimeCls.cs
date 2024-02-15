using System;
using System.Collections.Generic;
using System.Text;

namespace Library
{
    public class ConvertDateTimeCls
    {
        public static string StringDate(string _Date, string _DateNow)
        {
            //Dong bo tt
            //string ttA = _DateNow.Substring(_DateNow.LastIndexOf(" "));
            //string ttB = _DateNow.Substring(_Date.LastIndexOf(" "));
            
            string result = "";
            try
            {
                result = (DateTime.Parse(_Date) - DateTime.Parse(_DateNow)).ToString();
                string[] array = result.Split('.');
                if (array.Length <= 1)
                {
                    string[] gio = array[0].Split(':');
                    result = gio[0] + "h" + gio[1] + "'" + gio[2] + "s";
                    if (gio[0] == "00" && gio[1] == "00" && gio[2] == "00")
                        result = "finish";
                }
                else
                {
                    result = array[0] + " ngày ";
                    string[] gio = array[1].Split(':');
                    result += gio[0] + "h" + gio[1] + "'" + gio[2] + "s";
                }
                return result;
            }
            catch
            {
                return "finish";
            }
        }

        public static string StringDateTime(int val)
        {
            string temp = "";
            int hh = 0, mm = 0, ss = 0, dd = 0;
            dd = val / 86400;
            temp += dd > 0 ? dd + " ngày " : "";
            val %= 86400;
            hh = val / 3600;
            val %= 3600;
            temp += hh > 0 ? hh + "h" : "0h";
            mm = val / 60;
            val %= 60;
            temp += mm > 0 ? (mm < 10 ? "0" + mm + "'" : mm + "'") : "00\"";
            ss = val;
            temp += ss > 0 ? (ss < 10 ? "0" + ss + "\"" : ss + "\"") : "00\"";

            return temp;//1 ngày 22h30'40"
        }

        public static string StringTime(int val)
        {
            string temp = "";
            int hh = 0, mm = 0, ss = 0;
            hh = val / 3600;
            val %= 3600;
            temp += hh > 0 ? hh + " giờ " : "0 giờ ";
            mm = val / 60;
            val %= 60;
            temp += mm > 0 ? (mm < 10 ? "0" + mm + " phút " : mm + " phút ") : "00 phút ";
            ss = val;
            temp += ss > 0 ? (ss < 10 ? "0" + ss + " giây " : ss + " giây") : "00 giây";

            return temp;
        }
    }
}
