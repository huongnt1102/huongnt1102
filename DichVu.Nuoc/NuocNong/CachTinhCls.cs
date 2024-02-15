using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Library;

namespace DichVu.Nuoc.NuocNong
{
    public class CachTinhCls
    {
        public byte MaTN { get; set; }
        public int? MaMB { get; set; }
        public int SoTieuThu { get; set; }
        public List<ChiTietNuocItem> ltChiTiet;
        
        public void LoadDinhMuc()
        {
            var db = new MasterDataContext();
            try
            {
                ltChiTiet = (from dm in db.dvNuocNongDinhMucs
                             where dm.MaMB == this.MaMB
                             orderby dm.STT
                             select new ChiTietNuocItem()
                             {
                                 MaDM = dm.ID,
                                 TenDM = dm.TenDM,
                                 DinhMuc = dm.DinhMuc,
                                 DonGia = dm.DonGia,
                                 DienGiai = dm.DienGiai
                             }).ToList();
                if (ltChiTiet.Count == 0)
                {
                    var _MaLMB = (from pn in db.mbPhanNhoms where pn.MaLDV == 20 & pn.MaMB == this.MaMB select pn.MaLMB).FirstOrDefault();
                    ltChiTiet = (from dm in db.dvNuocNongDinhMucs
                                 where dm.MaLMB == _MaLMB & dm.MaMB == null
                                 orderby dm.STT
                                 select new ChiTietNuocItem()
                                 {
                                     MaDM = dm.ID,
                                     TenDM = dm.TenDM,
                                     DinhMuc = dm.DinhMuc,
                                     DonGia = dm.DonGia,
                                     DienGiai = dm.DienGiai
                                 }).ToList();

                    if (ltChiTiet.Count == 0)
                    {
                        ltChiTiet = (from dm in db.dvNuocNongDinhMucs
                                     where dm.MaTN == this.MaTN & dm.MaLMB == null & dm.MaMB == null
                                     orderby dm.STT
                                     select new ChiTietNuocItem()
                                     {
                                         MaDM = dm.ID,
                                         TenDM = dm.TenDM,
                                         DinhMuc = dm.DinhMuc,
                                         DonGia = dm.DonGia,
                                         DienGiai = dm.DienGiai
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

        public void XuLy()
        {
            for (var i = 0; i < ltChiTiet.Count; i++)
            {
                var objCT = ltChiTiet[i];

                if (SoTieuThu > objCT.DinhMuc && i < ltChiTiet.Count - 1)
                {
                    objCT.SoLuong = objCT.DinhMuc;
                    SoTieuThu -= objCT.DinhMuc.Value;
                }
                else
                {
                    objCT.SoLuong = SoTieuThu;
                    SoTieuThu = 0;
                }

                objCT.DonGia = objCT.DonGia;
                objCT.ThanhTien = objCT.DonGia * objCT.SoLuong;
                objCT.DienGiai = objCT.DienGiai;
            }
        }

        public decimal GetThanhTien()
        {
            return this.ltChiTiet.Sum(p => p.ThanhTien).GetValueOrDefault();
        }
    }
}
