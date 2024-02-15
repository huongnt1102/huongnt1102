using Library;
using System;
using System.Collections.Generic;
using System.Data.Linq.SqlClient;
using System.Linq;
using System.Text;

namespace Library.Utilities
{
    public static class NhuCauCls
    {
        public static string TaoSoCT(byte? maTN)
        {
            using (var db = new MasterDataContext())
            {
                var objTN = db.tnToaNhas.Single(p => p.MaTN == maTN);

                var soCT = string.Format("CH-{0}{1:yy}/{1:MM}", objTN.TenVT, db.GetSystemDate());

                var index = 1;
                var lastIndex = db.ncNhuCaus.Where(p => SqlMethods.Like(p.SoNC, soCT + "%")).Select(p => int.Parse(p.SoNC.Substring(soCT.Length))).ToList();

                if (lastIndex.Any())
                    index = lastIndex.Max() + 1;

                return soCT + index.ToString().PadLeft(3, '0');
            }
        }

        public static void SetTenCoHoi(int? MaNC)
        {
            using (var db = new MasterDataContext())
            {
                var objNC = db.ncNhuCaus.Single(p => p.MaNC == MaNC);

                var arrTenCH = objNC.ncSanPhams.Where(p => p.NhuCauThue != null)
                                    .Select(p => p.NhuCauThue.TenNhuCau
                                                  +
                                                   (p.SoLuong.GetValueOrDefault() > 0 ? String.Format("({0:n0} ghế)", p.SoLuong) : "")
                                    ).Distinct().ToArray();

                objNC.TenCH = String.Join(", ", arrTenCH);
                db.SubmitChanges();
            }
        }

        public static bool checkHopLe(int? maKH)
        {
            using (var db = new MasterDataContext())
            {
                return db.ncNhuCaus.Any(o => o.MaKH == maKH & o.MaTT != 4);
            }

        }

        public static void TinhTiemNang(int? maNC)
        {
            if (maNC.GetValueOrDefault() == 0)
                return;

            using (var db = new MasterDataContext())
            {
                var objNC = db.ncNhuCaus.Single(p => p.MaNC == maNC);

                if (objNC.LichHens.Any())
                    objNC.StepID = 1;

                if (objNC.BaoGias.Any())
                    objNC.StepID = 2;

                //if (objNC.BaoGias.Any(o => o.ctHopDongs.Any()) || db.ctHopDongs.Any(o=>o.MaNC== maNC))
                //    objNC.StepID = 3;

                //objNC.TiemNang = objNC.StepID.GetValueOrDefault() == 0 ? 0 : db.ncCauHinhTiemNangs.Single( o=> o.Step == objNC.StepID).Percent.GetValueOrDefault();

                db.SubmitChanges();
            }
        }
    }
}
