using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.SqlClient;

namespace Library.Utilities
{
    public static class BaoGiaCls
    {
        public static string TaoSoCT(int? maNC, byte? maTN)
        {
            using (var db = new MasterDataContext())
            {
                var objTN = db.tnToaNhas.Single(p => p.MaTN == maTN);

                var soCT = string.Format("{0}-{1:yy}/{1:MM}/BG-", objTN.TenVT, DateTime.UtcNow.AddHours(7));

                var objNC = db.ncNhuCaus.SingleOrDefault(p => p.MaNC == maNC);

                if (objNC != null)
                    soCT = objNC.SoNC + "-BG-";

                var index = 1;
                var lastIndex = db.BaoGias.Where(p => SqlMethods.Like(p.SoBG, soCT + "%")).Select(p => int.Parse(p.SoBG.Substring(soCT.Length))).ToList();

                if (lastIndex.Any())
                    index = lastIndex.Max() + 1;

                return soCT + index.ToString().PadLeft(2, '0');
            }
        }
    }
}
