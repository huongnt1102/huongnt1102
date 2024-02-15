using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library
{
    public static class ReceiptMangerCls
    {
        public static string CountCharater(string s)
        {
            string str = "";
            switch (s.Length)
            {
                case 1:
                    str = "0000" + s;
                    break;
                case 2:
                    str = "000" + s;
                    break;
                case 3:
                    str = "00" + s;
                    break;
                case 4:
                    str = "0" + s;
                    break;
                case 5:
                    str = s;
                    break;
            }
            return str;
        }

        public static string CreateKeyword(DateTime date)
        {
            string SoPhieu = "";
            string cSoPhieu = "";
            try
            {
                using (var db = new MasterDataContext())
                {
                    var obj = db.PTDemSoLuongs.FirstOrDefault(p => p.ThangDem.Value.Month == date.Month & p.ThangDem.Value.Year == date.Year);
                    if (obj == null)
                    {
                        var objdem = new PTDemSoLuong();
                        objdem.ThangDem = date;
                        objdem.SoLuongPhieu = 0;
                        SoPhieu = string.Format("PT{0:yyyy}/{0:MM}/00001", date);
                        db.PTDemSoLuongs.InsertOnSubmit(objdem);
                    }
                    else
                    {
                        cSoPhieu = (obj.SoLuongPhieu + 1).ToString();
                        obj.SoLuongPhieu++;
                        SoPhieu = string.Format("PT{0:yyyy}/{0:MM}/{1}", date, CountCharater(cSoPhieu));
                    }
                }
            }
            catch { }
            return SoPhieu;
        }
    }
}
