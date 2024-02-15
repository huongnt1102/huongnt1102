using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Library;

namespace DichVu.GiuXe
{
    class CachTinhCls
    {
        public byte MaTN { get; set; }
        public int? MaMB { get; set; }
        public int? MaLX { get; set; }

        public List<ChiTietGiuXeItem> ltBangGia;

        public void LoadDinhMuc()
        {
            var db = new MasterDataContext();
            try
            {
                ltBangGia = (from dm in db.dvgxDinhMucs
                             where dm.MaMB == this.MaMB & dm.MaLX == this.MaLX
                             select new ChiTietGiuXeItem()
                             {
                                 MaDM = dm.ID,
                                 TenDM = dm.TenDM,
                                 SoLuong = dm.SoLuong,
                                 DonGiaThang = dm.GiaThang,
                                 DonGiaNgay = dm.GiaNgay
                             }).ToList();
                if (ltBangGia.Count == 0)
                {
                    var _MaLMB = (from pn in db.mbMatBangs where  pn.MaMB == this.MaMB select pn.MaLMB).FirstOrDefault();
                    ltBangGia = (from dm in db.dvgxDinhMucs
                                 where dm.MaLMB == _MaLMB & dm.MaMB == null & dm.MaLX == this.MaLX
                                 select new ChiTietGiuXeItem()
                                 {
                                     MaDM = dm.ID,
                                     TenDM = dm.TenDM,
                                     SoLuong = dm.SoLuong,
                                     DonGiaThang = dm.GiaThang,
                                     DonGiaNgay = dm.GiaNgay
                                 }).ToList();

                    if (ltBangGia.Count == 0)
                    {
                        ltBangGia = (from dm in db.dvgxDinhMucs
                                     where dm.MaTN == this.MaTN & dm.MaLMB == null & dm.MaMB == null & dm.MaLX == this.MaLX
                                     select new ChiTietGiuXeItem()
                                     {
                                         MaDM = dm.ID,
                                         TenDM = dm.TenDM,
                                         SoLuong = dm.SoLuong,
                                         DonGiaThang = dm.GiaThang,
                                         DonGiaNgay = dm.GiaNgay
                                     }).ToList();
                    }
                }
            }
            catch { }
            finally
            {
                db.Dispose();
            }
        }
    }
}
