using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Library;

namespace DichVu.DienLanh
{
    public class CachTinhCls
    {
        public byte MaTN { get; set; }
        public int? MaMB { get; set; }
        public int? MaLD { get; set; }
        public decimal? SoTieuThu { get; set; }

        public List<ChiTietDienItem> ltChiTiet;

        public void LoadDinhMuc()
        {
            var db = new MasterDataContext();
            try
            {
                ltChiTiet = (from dm in db.dvDienLanhDinhMucs
                             where dm.MaMB == this.MaMB && dm.MaLoaiDien == MaLD
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
                    ltChiTiet = (from dm in db.dvDienLanhDinhMucs
                                 where dm.MaLMB == _MaLMB & dm.MaMB == null && dm.MaLoaiDien == MaLD
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
                        ltChiTiet = (from dm in db.dvDienLanhDinhMucs
                                     where dm.MaTN == this.MaTN & dm.MaLMB == null & dm.MaMB == null && dm.MaLoaiDien == MaLD
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

        public void XuLy2()
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
        public void XuLy()
        {
            var db = new MasterDataContext();
            if (ltChiTiet == null) return;
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
                var objLD = db.dvDienLoaiDiens.FirstOrDefault(p=> p.ID== MaLD);
                //objCT.ThanhTien = Math.Round((decimal)(objCT.DonGia * (objCT.SoLuong/objLD.GiaTriTruoc/objLD.GiaTriSau)), 0, MidpointRounding.AwayFromZero);
                objCT.ThanhTien = Math.Round((decimal)(objCT.DonGia * objCT.SoLuong), 0, MidpointRounding.AwayFromZero);
                objCT.DienGiai = objCT.DienGiai;
            }

        }
        public decimal GetThanhTien()
        {
            return this.ltChiTiet.Sum(p => p.ThanhTien).GetValueOrDefault();
        }
    }
}
