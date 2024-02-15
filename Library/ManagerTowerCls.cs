using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Library;

namespace Library
{
    public static class ManagerTowerCls
    {
        public static List<ToaNhaItem> GetAllTower(tnNhanVien objnv)
        {
            var ltToaNha = new List<ToaNhaItem>();
            using (var db = new MasterDataContext())
            {
                ltToaNha = (from nv in db.tnToaNhaNguoiDungs
                            join tn in db.tnToaNhas on nv.MaTN equals tn.MaTN
                            where nv.MaNV == objnv.MaNV
                            select new ToaNhaItem() { MaTN = tn.MaTN, TenTN = tn.TenTN })
                            .ToList();

                // Nếu chưa phân quyền tòa nhà thì được phép mặc định vào tòa nhà của anh đó
                //if (ltToaNha.Count() == 0)
                //{
                //    var objTN = db.tnToaNhas.Where(p => p.MaTN == objnv.MaTN).Select(tn => new ToaNhaItem() { MaTN = tn.MaTN, TenTN = tn.TenTN }).First();
                //    ltToaNha.Add(objTN);
                //}

                //Nếu là admin, lấy tất cả tòa nhà
                if ((bool)objnv.IsSuperAdmin)
                {
                    ltToaNha = db.tnToaNhas.Select(tn => new ToaNhaItem() { MaTN = tn.MaTN, TenTN = tn.TenTN }).ToList();
                }
                else
                {

                }
            }

            return ltToaNha;
        }

        /// <summary>
        /// Get tất cả nhân viên trong tòa mà người dùng đó có quyền
        /// Ví dụ có 10 tòa, anh A có quyền 2 tòa, vậy dựa theo fun GetAllTower thì lấy được 2 tòa. 
        /// Từ 2 tòa này, lấy ra tất cả nhân viên trong 2 tòa đó
        /// </summary>
        /// <param name="objnv"></param>
        /// <returns></returns>
        public static List<UserItem> GetAllUserReference(tnNhanVien objnv)
        {
            var towers = GetAllTower(objnv);
            var users = new List<UserItem>();
            using (var db = new MasterDataContext())
            {
                users = (from tn in towers
                         join nd in db.tnToaNhaNguoiDungs on tn.MaTN equals nd.MaTN
                            join nv in db.tnNhanViens on nd.MaNV equals nv.MaNV
                            group new { nv } by new { nv.MaNV, nv.HoTenNV } into g
                            orderby g.Key.HoTenNV
                            select new UserItem { MaNV = g.Key.MaNV, HoTenNV = g.Key.HoTenNV })
                            .ToList();
            }

            return users;
        }

        
    }

    public class ToaNhaItem
    {
        public byte MaTN { get; set; }
        public string TenTN { get; set; }
    }

    public class UserItem
    {
        public int? MaNV { get; set; }
        public string HoTenNV { get; set; }
    }
}
