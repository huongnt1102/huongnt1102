using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Library;
using System.Data.Linq.SqlClient;

namespace DichVu.GiuXe
{
   public class TheXe_SetDonGia
    {
       public bool SetDonGia(byte MaTN)
       {
           try
           {
               var db = new MasterDataContext();

               // Lấy danh sách thẻ vẫn sử dụng đến thời điểm hiện tại
               var ltTheXe = db.dvgxTheXes.Where(o => ((SqlMethods.DateDiffMonth(Common.GetDateTimeSystem(), o.NgayTT) >= 0 
                   & o.NgungSuDung == false) // Đang sử dụng ở hiện và tương lai
               | (SqlMethods.DateDiffMonth(Common.GetDateTimeSystem(), o.NgayTT) > 0 & o.NgungSuDung == true)) // Hoặc ngưng sử dụng ở tương lai
               & o.MaTN == MaTN
               );

               // Danh sách mặt bằng sẽ update đơn giá
               var ltMB = ltTheXe.Select(o => new { o.MaMB, o.MaLX }).Distinct();

               foreach (var objMB in ltMB)
               {
                   int _sl = 0;

                   // Load định mức theo loại xe
                   var objCT = new CachTinhCls();
                   objCT.MaTN = MaTN;
                   objCT.MaMB = objMB.MaMB;
                   objCT.MaLX = objMB.MaLX;
                   objCT.LoadDinhMuc();

                   // Danh sách thẻ xe theo loại xe
                   var dsUpdate = ltTheXe.Where(o => o.MaMB == objMB.MaMB & o.MaLX == objMB.MaLX);

                   foreach (var tx in dsUpdate)
                   {
                       _sl++;
                       int SoLuong = _sl;

                       var objGia = (from bg in objCT.ltBangGia
                                     where bg.SoLuong <= SoLuong
                                     orderby bg.SoLuong descending
                                     select bg).First();

                       tx.MaDM = objGia.MaDM;
                       tx.GiaThang = objGia.DonGiaThang;
                       tx.TienTT = (tx.GiaThang * tx.KyTT.GetValueOrDefault())+ (tx.GiaThang*tx.ThueGTGT.GetValueOrDefault());
                       tx.TienTruocThue = objGia.DonGiaThang;
                       tx.TienThueGTGT = (tx.GiaThang * tx.ThueGTGT.GetValueOrDefault());
                   }
               }

               db.SubmitChanges();
               return true;
           }
           catch
           {
               return false;
           }
       }
    }
}
