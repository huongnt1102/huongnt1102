using System.Data;
using System.Linq;

namespace BuildingDesignTemplate.Class
{
    public static class KinhPhiDuKien
    {
        private static readonly Library.MasterDataContext Db = new Library.MasterDataContext();

        private const string GrsubChung = "CHUNG";
        private const string GrsubChiTiet = "CHITIET";

        private static decimal? TongConLai { get; set; }
        private static decimal? ChuaPhanBo { get; set; }

        public static string MergeKinhPhiDuKien(string contents, int? nam, string buildingId, int? groupId)
        {
            var toaNha = BuildingDesignTemplate.Class.Common.GetToaNhaById(buildingId);
            if (toaNha == null) return contents;


            var mp = GetMoneyPurpose(nam, toaNha.MaTN);
            if (mp == null) return contents;

            ChuaPhanBo = mp.MoneyExist;

            contents = MergeThongTinChiTiet(contents, mp.Id, groupId);
            contents = MergeThongTinChung(contents, nam, buildingId, groupId, toaNha.TenTN, mp.MoneyExist, mp.MoneyUsed, mp.MoneyPurpose, ChuaPhanBo + TongConLai);

            return contents;
        }

        public static string MergeThongTinChung(string contents, int? nam, string buildingId, int? groupId, string tenToaNha, decimal? moneyExist, decimal? moneyUsed, decimal? tongChiPhi, decimal? conLaiTong)
        {
            System.Collections.Generic.List<KinhPhiDuKienChung> lKinhPhiChung = CreateKinhPhiDuKienChung(tenToaNha, nam.ToString(), string.Format("{0:#,0.##}", moneyExist), string.Format("{0:#,0.##}", moneyUsed), string.Format("{0:#,0.##}", tongChiPhi), string.Format("{0:#,0.##}",conLaiTong));

            return BuildingDesignTemplate.Class.Common.GetContents(contents, groupId, Library.SqlCommon.LINQToDataTable(lKinhPhiChung).Rows[0], GrsubChung);
        }

        private static System.Collections.Generic.List<KinhPhiDuKienChung> CreateKinhPhiDuKienChung(string tenToaNha, string nam, string moneyExist, string moneyUsed, string tongChiPhi, string conLaiTong)
        {
            System.Collections.Generic.List<KinhPhiDuKienChung> lKinhPhiChung =
                new System.Collections.Generic.List<KinhPhiDuKienChung>
                {
                    new KinhPhiDuKienChung
                    {
                        TenToaNha = tenToaNha,
                        Thang = System.DateTime.Now.Month.ToString(),
                        Nam = System.DateTime.Now.Year.ToString(),
                        NamDuLieu = nam.ToString(),
                        ChuaPhanBo = moneyExist,
                        ChuaPhanBoConLai = moneyUsed,
                        TongChiPhi = tongChiPhi,
                        ConLaiTong = conLaiTong
                    }
                };
            return lKinhPhiChung;
        }

        public static string MergeThongTinChiTiet(string contents, int? mpId, int? groupId)
        {
            var list = (from p in Db.mp_MoneyPurposeItems
                    where p.MoneyPurposeId == mpId
                    select new { p.Id, p.MoneyExist, p.MoneyPurpose, p.MoneyPurposeId, p.MoneyUsed, p.Name, p.No, p.Year})
                .AsEnumerable().Select((p, index) => new KinhPhiChiTiet
                {
                    STT = index + 1,
                    ConLai = string.Format("{0:#,0.##}", p.MoneyExist),
                    DaSuDung = string.Format("{0:#,0.##}", p.MoneyUsed),
                    DuKien = string.Format("{0:#,0.##}", p.MoneyPurpose),
                    Name = p.Name, MoneyExist = p.MoneyExist, MoneyUsed = p.MoneyUsed
                }).ToList();

            if (list.Count == 0) return contents;

            contents = BuildingDesignTemplate.Class.Common.MergeTable(contents, "[KINHPHICHITIET]", Library.SqlCommon.LINQToDataTable(list));

            System.Collections.Generic.List<KinhPhiChiTiet> kinhPhiChiTiets = new System.Collections.Generic.List<KinhPhiChiTiet>
            {
                new KinhPhiChiTiet
                {
                    TongConLai = string.Format("{0:#,0.##}", list.Sum(_ => _.MoneyExist)),
                    TongDaSuDung = string.Format("{0:#,0.##}", list.Sum(_ => _.MoneyUsed))
                }
            };

            TongConLai = list.Sum(_ => _.MoneyExist);

            contents = BuildingDesignTemplate.Class.Common.GetContents(contents, groupId, Library.SqlCommon.LINQToDataTable(kinhPhiChiTiets).Rows[0], GrsubChiTiet);

            return contents;
        }

        private static Library.mp_MoneyPurpose GetMoneyPurpose(int? nam, byte? buildingId)
        {
            return Db.mp_MoneyPurposes.FirstOrDefault(_ => _.Year == nam & _.BuildingId == buildingId);
        }

        private class KinhPhiDuKienChung
        {
            public string TenToaNha { get; set; }
            public string Thang { get; set; }
            public string Nam { get; set; }
            public string NamDuLieu { get; set; }
            public string TongChiPhi { get; set; }
            public string ChuaPhanBo { get; set; }
            public string ChuaPhanBoConLai { get; set; }
            public string ConLaiTong { get; set; }
        }

        private class KinhPhiChiTiet
        {
            public string Name { get; set; }
            public string DuKien { get; set; }
            public string DaSuDung { get; set; }
            public string ConLai { get; set; }
            public string TongConLai { get; set; }
            public string TongDaSuDung { get; set; }
            public decimal? STT { get; set; }
            public decimal? MoneyExist { get; set; }
            public decimal? MoneyUsed { get; set; }
        }
    }
}
