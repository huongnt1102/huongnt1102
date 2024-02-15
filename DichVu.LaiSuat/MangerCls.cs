using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.SqlClient;

namespace DichVu.LaiSuat
{
    public static class MangerCls
    {
        public static decimal CalculatorInterest(DateTime date, decimal money, byte towerID)
        {
            decimal Result = 0;

            using (var db = new Library.MasterDataContext())
            {
                var laiSuat = db.dvLaiSuats
                    //.Where(p => SqlMethods.DateDiffDay(p.TuNgay, date) >= 0
                    //                                    & SqlMethods.DateDiffDay(p.DenNgay, date) <= 0
                    //                                    & p.IsByMonth.GetValueOrDefault()
                    //                                    & p.MaTN == towerID)
                                            .FirstOrDefault();
                decimal rate = 0;
                //if (laiSuat != null)
                    //rate = (laiSuat.LaiSuat ?? 0) / 100;

                Result = money * (decimal)Math.Pow((double)(1 + rate), 1);

                Result = Result - money;
            }

            return Math.Round(Result, 0, MidpointRounding.AwayFromZero);
        }

        public static decimal CalculatorInterestDay(DateTime date, decimal money, int day, byte towerID)
        {
            decimal Result = 0;
            if (day > 0)
            {
                using (var db = new Library.MasterDataContext())
                {
                    var laiSuat = db.dvLaiSuats
                        //.Where(p => SqlMethods.DateDiffDay(p.TuNgay, date) >= 0
                        //                                    & SqlMethods.DateDiffDay(p.DenNgay, date) <= 0
                        //                                    & !p.IsByMonth.GetValueOrDefault()
                        //                                    & p.MaTN == towerID)
                                                .FirstOrDefault();
                    decimal rate = 0;
                    //if (laiSuat != null)
                    //    rate = (laiSuat.LaiSuat ?? 0) / 100;

                    Result = money * (decimal)Math.Pow((double)(1 + rate), (double)day);

                    Result = Result - money;
                }
            }

            return Math.Round(Result, 0, MidpointRounding.AwayFromZero);
        }
    }
}
