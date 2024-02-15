using System;
using System.Collections.Generic;
using System.Data.Linq.SqlClient;
using System.Linq;
using System.Text;

namespace Library.Utilities
{
    class LichHenCls
    {
        public static string TaoSoCT(int? maNC, byte? maTN)
        {
            using (var db = new MasterDataContext())
            {
                var objTN = db.tnToaNhas.Single(p => p.MaTN == maTN);

                var soCT = string.Format("{0}-{1:yy}/{1:MM}/LH-", objTN.TenVT, db.GetSystemDate());

                var objNC = db.ncNhuCaus.SingleOrDefault(p => p.MaNC == maNC);

                if (objNC != null)
                    soCT = objNC.SoNC + "-LH-";

                var index = 1;
                var lastIndex = db.BaoGias.Where(p => SqlMethods.Like(p.SoBG, soCT + "%")).Select(p => int.Parse(p.SoBG.Substring(soCT.Length))).ToList();

                if (lastIndex.Any())
                    index = lastIndex.Max() + 1;

                return soCT + index.ToString().PadLeft(2, '0');
            }
        }
    }
}
