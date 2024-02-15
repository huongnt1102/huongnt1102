using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Library;

namespace DichVu.Dien
{
    public class CachTinhCls
    {
        public byte MaTN { get; set; }
        public int? MaMB { get; set; }
        public decimal? SoTieuThu { get; set; }

        public List<ChiTietDienItem> ltChiTiet;

        public void LoadDinhMuc()
        {
            var db = new MasterDataContext();
            try
            {
                ltChiTiet = (from dm in db.dvDienDinhMucs
                             where dm.MaMB == this.MaMB & dm.IsKhoa.GetValueOrDefault() == false
                             orderby dm.STT
                             select new ChiTietDienItem()
                             {
                                 MaDM = dm.ID,
                                 TenDM = dm.TenDM,
                                 DinhMuc = dm.DinhMuc,
                                 DonGia = dm.DonGia,
                                 DienGiai = dm.DienGiai
                             }).ToList();
                if (ltChiTiet.Count == 0)
                {
                    //var _MaLMB = (from pn in db.mbPhanNhoms where pn.MaLDV == 8 & pn.MaMB == this.MaMB select pn.MaLMB).FirstOrDefault();
                    var _MaLMB = db.mbMatBangs.Single(o => o.MaMB == this.MaMB).MaLMB;
                    ltChiTiet = (from dm in db.dvDienDinhMucs
                                 where dm.MaLMB == _MaLMB & dm.MaMB == null & dm.IsKhoa.GetValueOrDefault() == false
                                 orderby dm.STT
                                 select new ChiTietDienItem()
                                 {
                                     MaDM = dm.ID,
                                     TenDM = dm.TenDM,
                                     DinhMuc = dm.DinhMuc,
                                     DonGia = dm.DonGia,
                                     DienGiai = dm.DienGiai
                                 }).ToList();

                    if (ltChiTiet.Count == 0)
                    {
                        ltChiTiet = (from dm in db.dvDienDinhMucs
                                     where dm.MaTN == this.MaTN & dm.MaLMB == null & dm.MaMB == null & dm.IsKhoa.GetValueOrDefault() == false
                                     orderby dm.STT
                                     select new ChiTietDienItem()
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

                if (this.SoTieuThu > objCT.DinhMuc && i < ltChiTiet.Count - 1)
                {
                    objCT.SoLuong = objCT.DinhMuc;
                    this.SoTieuThu -= objCT.DinhMuc.Value;
                }
                else
                {
                    objCT.SoLuong = this.SoTieuThu;
                    this.SoTieuThu = 0;
                }

                objCT.ThanhTien = Math.Round((decimal)(objCT.DonGia * objCT.SoLuong),0,MidpointRounding.AwayFromZero);
                objCT.DienGiai = objCT.DienGiai;
            }
        }

        public decimal GetThanhTien()
        {
            return this.ltChiTiet.Sum(p => p.ThanhTien).GetValueOrDefault();
        }
    }
}
