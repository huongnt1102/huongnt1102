using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Library;

namespace DichVu.Dien.DieuHoa
{
    public class CachTinhCls
    {
        public byte MaTN { get; set; }
        public int? MaMB { get; set; }
        public decimal? SoTieuThu { get; set; }

        public List<ChiTietDienDieuHoaItem> ltChiTiet;

        public void LoadDinhMuc()
        {
            var db = new MasterDataContext();
            try
            {
                ltChiTiet = (from dm in db.dvDienDH_DinhMucs
                             where dm.MaMB == this.MaMB
                             orderby dm.STT
                             select new ChiTietDienDieuHoaItem()
                             {
                                 MaDM = dm.ID,
                                 TenDM = dm.TenDM,
                                 DinhMuc = dm.DinhMuc,
                                 DonGia = dm.DonGia,
                                 DienGiai = dm.DienGiai
                             }).ToList();
                if (ltChiTiet.Count == 0)
                {
                    var _MaLMB = (from pn in db.mbPhanNhoms where pn.MaLDV == 8 & pn.MaMB == this.MaMB select pn.MaLMB).FirstOrDefault();
                    ltChiTiet = (from dm in db.dvDienDH_DinhMucs
                                 where dm.MaLMB == _MaLMB & dm.MaMB == null
                                 orderby dm.STT
                                 select new ChiTietDienDieuHoaItem()
                                 {
                                     MaDM = dm.ID,
                                     TenDM = dm.TenDM,
                                     DinhMuc = dm.DinhMuc,
                                     DonGia = dm.DonGia,
                                     DienGiai = dm.DienGiai
                                 }).ToList();

                    if (ltChiTiet.Count == 0)
                    {
                        ltChiTiet = (from dm in db.dvDienDH_DinhMucs
                                     where dm.MaTN == this.MaTN & dm.MaLMB == null & dm.MaMB == null
                                     orderby dm.STT
                                     select new ChiTietDienDieuHoaItem()
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
            if (ltChiTiet == null) return;
            for (var i = 0; i < ltChiTiet.Count; i++)
            {
                var objCT = ltChiTiet[i];

                if (this.SoTieuThu > objCT.DinhMuc && i < ltChiTiet.Count - 1)
                {
                    objCT.SoLuong = objCT.DinhMuc;
                    this.SoTieuThu -= (decimal?)objCT.DinhMuc.Value;
                }
                else
                {
                    objCT.SoLuong = this.SoTieuThu;
                    this.SoTieuThu = 0;
                }

                objCT.ThanhTien = objCT.DonGia * objCT.SoLuong;
                objCT.DienGiai = objCT.DienGiai;
            }
        }

        public decimal GetThanhTien()
        {
            return this.ltChiTiet.Sum(p => p.ThanhTien).GetValueOrDefault();
        }
    }

    public class ChiTietDienDieuHoaItem
    {
        public int? ID { get; set; }
        public int? MaGas { get; set; }
        public int? MaDM { get; set; }
        public string TenDM { get; set; }
        public decimal? SoLuong { get; set; }
        public decimal? DonGia { get; set; }
        public decimal? ThanhTien { get; set; }
        public int? DinhMuc { get; set; }
        public string DienGiai { get; set; }
    }
}
