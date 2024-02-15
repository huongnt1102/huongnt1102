using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace Library.HeThongCls
{
    public class UserLogin
    {
        public bool AddNewNV(tnNhanVien objnhanvien)
        {
            using (MasterDataContext db = new MasterDataContext())
            {
                db.tnNhanViens.InsertOnSubmit(objnhanvien);
                try
                {
                    db.SubmitChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
        public bool EditUser(tnNhanVien objnhanvien)
        {
            using (MasterDataContext db = new MasterDataContext())
            {
                var objnv = db.tnNhanViens.Single(p => p.MaNV == objnhanvien.MaNV);
                objnv.MaCV = objnhanvien.MaCV;
                objnv.SoNB = objnhanvien.SoNB;
                objnv.MaTN = objnhanvien.MaTN;
                objnv.MaPB = objnhanvien.MaPB;
                objnv.MaSoNV = objnhanvien.MaSoNV;
                if (objnhanvien.MatKhau.Length != 0) objnv.MatKhau = HashPassword(objnhanvien.MatKhau);
                objnv.DiaChi = objnhanvien.DiaChi;
                objnv.DienThoai = objnhanvien.DienThoai;
                objnv.Email = objnhanvien.Email;
                objnv.HoTenNV = objnhanvien.HoTenNV;

                try
                {
                    db.SubmitChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        public tnNhanVien GetUserByMaNV(string sMaNV, string sPassword)
        {
            tnNhanVien objnhanvien = new tnNhanVien();
            using (MasterDataContext db = new MasterDataContext())
            {
                var query = db.tnNhanViens.ToList();
                objnhanvien = db.tnNhanViens.SingleOrDefault(p => p.MaSoNV == sMaNV & p.MatKhau == HashPassword(sPassword) & p.IsLocked==false);
                return objnhanvien;
            }
        }

        public tnNhanVien GetUserByMaNVNonHash(string sMaNV, string sPassword)
        {
            tnNhanVien objnhanvien = new tnNhanVien();
            using (MasterDataContext db = new MasterDataContext())
            {
                objnhanvien = db.tnNhanViens.SingleOrDefault(p => p.MaSoNV == sMaNV & p.MatKhau == sPassword & p.IsLocked==false);
                return objnhanvien;
            }
        }

        public bool ChangePassword(tnNhanVien objnhanvien)
        {
            using (MasterDataContext db = new MasterDataContext())
            {
                var newuser = db.tnNhanViens.FirstOrDefault(p => p.MaSoNV == objnhanvien.MaSoNV);
                newuser.MatKhau = HashPassword(objnhanvien.MatKhau);

                try
                {
                    db.SubmitChanges();
                    return true;
                }
                catch
                {
                    return false; 
                }
            }
        }

        public string HashPassword(string OriginalString)
        {
            //Declarations
            Byte[] originalBytes;
            Byte[] encodedBytes;
            MD5 md5;

            //Instantiate MD5CryptoServiceProvider, get bytes for original password and compute hash (encoded password)
            md5 = new MD5CryptoServiceProvider();
            originalBytes = ASCIIEncoding.Default.GetBytes(OriginalString);
            encodedBytes = md5.ComputeHash(originalBytes);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < encodedBytes.Length; i++)
            {
                sb.Append(encodedBytes[i].ToString("X2"));
            }

            //Convert encoded bytes back to a 'readable' string
            return sb.ToString();
        }
    }
}
