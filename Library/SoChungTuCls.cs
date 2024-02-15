using System;
using System.Collections.Generic;
using System.Data.Linq.SqlClient;
using System.Linq;
using System.Text;

namespace Library
{
    public static class SoChungTuCls
    {
        public static string TaoSCT_CoHoi(byte? MaTN)
        {
            string soCH = "";

            using (var db = new MasterDataContext())
            {
                var objTN = db.tnToaNhas.Single(p => p.MaTN == MaTN);

                var format = string.Format("CH-{0}/{1:MMyy}", objTN.TenVT, db.GetSystemDate());

                int stt = 1;

                var data = db.ncNhuCaus.Where(p => SqlMethods.Like(p.SoNC, format + '%'));

                if (data.Any())
                {
                    stt = data.Select(p => int.Parse(p.SoNC.Substring(format.Length))).Max() + 1;
                }

                soCH = format + stt.ToString().PadLeft(2, '0');
            }

            return soCH;
        }
    }
}
