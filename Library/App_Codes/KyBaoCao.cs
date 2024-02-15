using System;
using System.Collections.Generic;
using System.Text;

namespace Library
{
    public class KyBaoCao
    {
        public string[] Source = new string[] 
        { 
            "Hôm nay", "Tuần này", "Đầu tuần tới hiện tại", "Tháng này", "Đầu tháng đến hiện tại",
            "Quý này", "Đầu quý đến hiện tại", "Năm này", "Đầu năm đến hiện tại", "Tháng 1", 
            "Tháng 2", "Tháng 3", "Tháng 4", "Tháng 5", "Tháng 6", "Tháng 7", "Tháng 8", "Tháng 9",
            "Tháng 10", "Tháng 11", "Tháng 12", "Quý I", "Quý II", "Quý III", "Quý IV", "Tuần trước", 
            "Tháng trước", "Quý trước", "Năm trước", "Tuần sau", "Bốn tuần sau", "Tháng sau", "Quý sau",
            "Năm sau", "Tự chọn"
        };

        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public int Index { get; set; }

        public int FirstMonth(int month)
        {
            if (month <= 3)
                return 1;
            else if (month <= 6)
                return 4;
            else if (month <= 9)
                return 7;
            else
                return 10;
        }

        public void SetToDate()
        {
            this.SetToDate(DateTime.Now);
        }

        public void SetToDate(DateTime dateNow)
        {
            switch (this.Index)
            {
                case 0: //Ngay nay
                    DateFrom = dateNow;
                    DateTo = dateNow;
                    break;
                case 1: //Tuan nay
                    DateFrom = dateNow.AddDays(1 - (int)dateNow.DayOfWeek);
                    DateTo = dateNow.AddDays(7 - (int)dateNow.DayOfWeek);
                    break;
                case 2: //Dau tuan den hien tai
                    DateFrom = dateNow.AddDays(1 - (int)dateNow.DayOfWeek);
                    DateTo = dateNow;
                    break;
                case 3: //Thang nay
                    DateFrom = new DateTime(dateNow.Year, dateNow.Month, 1);
                    DateTo = new DateTime(dateNow.Year, dateNow.Month, 1).AddMonths(1).AddDays(-1);
                    break;
                case 4: //Dau thang den hien tai
                    DateFrom = new DateTime(dateNow.Year, dateNow.Month, 1);
                    DateTo = dateNow;
                    break;
                case 5: //Quy nay
                    DateFrom = new DateTime(dateNow.Year, FirstMonth(dateNow.Month), 1);
                    DateTo = new DateTime(dateNow.Year,
                        FirstMonth(dateNow.Month) + 2, 1).AddMonths(1).AddDays(-1);
                    break;
                case 6: //Dau quy den hien tai
                    DateFrom = new DateTime(dateNow.Year, FirstMonth(dateNow.Month), 1);
                    DateTo = dateNow;
                    break;
                case 7: //Nam nay
                    DateFrom = new DateTime(dateNow.Year, 1, 1);
                    DateTo = new DateTime(dateNow.Year, 12, 31);
                    break;
                case 8: //Dau nam den hien tai
                    DateFrom = new DateTime(dateNow.Year, 1, 1);
                    DateTo = dateNow;
                    break;
                case 9: //Thang 1
                    DateFrom = new DateTime(dateNow.Year, 1, 1);
                    DateTo = new DateTime(dateNow.Year, 2, 1).AddDays(-1);
                    break;
                case 10: //Thang 2
                    DateFrom = new DateTime(dateNow.Year, 2, 1);
                    DateTo = new DateTime(dateNow.Year, 3, 1).AddDays(-1);
                    break;
                case 11: //Thang 3
                    DateFrom = new DateTime(dateNow.Year, 3, 1);
                    DateTo = new DateTime(dateNow.Year, 4, 1).AddDays(-1);
                    break;
                case 12: //Thang 4
                    DateFrom = new DateTime(dateNow.Year, 4, 1);
                    DateTo = new DateTime(dateNow.Year, 5, 1).AddDays(-1);
                    break;
                case 13: //Thang 5
                    DateFrom = new DateTime(dateNow.Year, 5, 1);
                    DateTo = new DateTime(dateNow.Year, 6, 1).AddDays(-1);
                    break;
                case 14: //Thang 6
                    DateFrom = new DateTime(dateNow.Year, 6, 1);
                    DateTo = new DateTime(dateNow.Year, 7, 1).AddDays(-1);
                    break;
                case 15: //Thang 7
                    DateFrom = new DateTime(dateNow.Year, 7, 1);
                    DateTo = new DateTime(dateNow.Year, 8, 1).AddDays(-1);
                    break;
                case 16: //Thang 8
                    DateFrom = new DateTime(dateNow.Year, 8, 1);
                    DateTo = new DateTime(dateNow.Year, 9, 1).AddDays(-1);
                    break;
                case 17: //Thang 9
                    DateFrom = new DateTime(dateNow.Year, 9, 1);
                    DateTo = new DateTime(dateNow.Year, 10, 1).AddDays(-1);
                    break;
                case 18: //Thang 10
                    DateFrom = new DateTime(dateNow.Year, 10, 1);
                    DateTo = new DateTime(dateNow.Year, 11, 1).AddDays(-1);
                    break;
                case 19: //Thang 11
                    DateFrom = new DateTime(dateNow.Year, 11, 1);
                    DateTo = new DateTime(dateNow.Year, 12, 1).AddDays(-1);
                    break;
                case 20: //Thang 12
                    DateFrom = new DateTime(dateNow.Year, 12, 1);
                    DateTo = new DateTime(dateNow.Year, 12, DateTime.DaysInMonth(dateNow.Year,12));
                    break;
                case 21: //Quy I
                    DateFrom = new DateTime(dateNow.Year, 1, 1);
                    DateTo = new DateTime(dateNow.Year, 4, 1).AddDays(-1);
                    break;
                case 22: //Quy II
                    DateFrom = new DateTime(dateNow.Year, 4, 1);
                    DateTo = new DateTime(dateNow.Year, 7, 1).AddDays(-1);
                    break;
                case 23: //Quy III
                    DateFrom = new DateTime(dateNow.Year, 7, 1);
                    DateTo = new DateTime(dateNow.Year, 10, 1).AddDays(-1);
                    break;
                case 24: //Quy IV
                    DateFrom = new DateTime(dateNow.Year, 10, 1);
                    DateTo = new DateTime(dateNow.Year, 12, 31);
                    break;
                case 25: //Tuan truoc
                    DateFrom = dateNow.AddDays(-(int)dateNow.DayOfWeek - 6);
                    DateTo = dateNow.AddDays(-(int)dateNow.DayOfWeek);
                    break;
                case 26: //Thang truoc
                    DateFrom = new DateTime(dateNow.Year, dateNow.Month, 1).AddMonths(-1);
                    DateTo = new DateTime(dateNow.Year, dateNow.Month, 1).AddDays(-1);
                    break;
                case 27: //Quy truoc
                    DateFrom = new DateTime(dateNow.Year, FirstMonth(dateNow.Month), 1).AddMonths(-3);
                    DateTo = new DateTime(dateNow.Year, FirstMonth(dateNow.Month), 1).AddDays(-1);
                    break;
                case 28: //Nam truoc
                    DateFrom = new DateTime(dateNow.Year - 1, 1, 1);
                    DateTo = new DateTime(dateNow.Year - 1, 12, DateTime.DaysInMonth(dateNow.Year, 12));
                    break;
                case 29: //Tuan sau
                    DateFrom = dateNow.AddDays(8 - (int)dateNow.DayOfWeek);
                    DateTo = dateNow.AddDays(14 - (int)dateNow.DayOfWeek);
                    break;
                case 30: //Bon tuan sau
                    DateFrom = dateNow;
                    DateTo = dateNow.AddMonths(4);
                    break;
                case 31: //Thang sau
                    DateFrom = new DateTime(dateNow.Year, dateNow.Month, 1).AddMonths(1);
                    DateTo = new DateTime(dateNow.Year, dateNow.Month, 1).AddMonths(2).AddDays(-1);
                    break;
                case 32: //Quy sau
                    DateTime QuySau = new DateTime(dateNow.Year, FirstMonth(dateNow.Month) + 3, 1);
                    DateFrom = QuySau;
                    DateTo = QuySau.AddMonths(3).AddDays(-1);
                    break;
                case 33: //Nam sau
                    DateFrom = new DateTime(dateNow.Year + 1, 1, 1);
                    DateTo = new DateTime(dateNow.Year + 1, 12, 31);
                    break;
                case 34: //Tu chon
                    DateFrom = new DateTime(2010, 1, 1);
                    DateTo = dateNow;
                    break;
                case 35: //đầu năm trước đến hết năm này
                    DateFrom = new DateTime(dateNow.Year - 1, 1, 1);
                    DateTo = new DateTime(dateNow.Year, 12, 31);
                    break;
                default:
                    break;
            }
        }


        public void Initialize(DevExpress.XtraEditors.Repository.RepositoryItemComboBox cmd)
        {
            foreach (string str in Source)
            {
                cmd.Items.Add(str);
            }
        }

        public void Initialize(DevExpress.XtraEditors.ComboBoxEdit cmd)
        {
            foreach (string str in Source)
            {
                cmd.Properties.Items.Add(str);
            }
        }
    }
}
