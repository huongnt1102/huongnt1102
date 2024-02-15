using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.App_Codes
{
    public class KhachHangBAL
    {
        static MasterDataContext db = new MasterDataContext();

        // Xem danh muc Khach hang
        public static object GetKhachhangList(byte? matn)
        {
            //return db.tnKhachHangs.ToList();
            return (from kh in db.tnKhachHangs
                        where kh.MaTN == matn
                        orderby kh.KyHieu descending
                        select new
                        {
                            kh.MaKH,
                            kh.KyHieu,
                            TenKH = kh.IsCaNhan.GetValueOrDefault() ? kh.HoKH.ToString() + " " + kh.TenKH.ToString() : kh.CtyTen
                        }).ToList();
        }

      
    }
}
