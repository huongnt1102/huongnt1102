using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DIP.SoftPhoneAPI
{
    public class Customer
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public string MaSoMB { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Note { get; set; }
        public int? StatusID { get; set; }
    }

    public class Transaction
    {
        public DateTime? Date { get; set; }
        public string Code { get; set; }
        public decimal? Money { get; set; }
        public string Type { get; set; }
        public string Note { get; set; }
        public string Staff { get; set; }
    }

    public class History
    {
        public string LinkName { get; set; }
        public DateTime? Date { get; set; }
        public string Content { get; set; }
        public string Formality { get; set; }
        public string Status { get; set; }
        public string Staff { get; set; }
        public string CallID { get; set; }
    }

    public class Contact
    {
        public string Vocative { get; set; }
        public string FullName { get; set; }
        public string Position { get; set; }
        public string Department { get; set; }
        public string PhoneNumber { get; set; }
        public string OtherPhone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Birthday { get; set; }
        public int Age { get; set; }
        public string Note { get; set; }
    }

    public class YeuCau
    {
        public string TenTT { get; set; }
        public string MaYC { get; set; }
        public DateTime? NgayYC { get; set; }
        public string TieuDe { get; set; }
        public string NoiDung { get; set; }
    }

    public class CongNo
    {
        public DateTime? NgayTT { get; set; }
        public string DienGiai { get; set; }
        public decimal? PhaiThu { get; set; }
        public decimal? DaThu { get; set; }
        public decimal? ConNo { get; set; }
        public string TenLDV { get; set; }
    }

    public class HistoryStatus
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
}
