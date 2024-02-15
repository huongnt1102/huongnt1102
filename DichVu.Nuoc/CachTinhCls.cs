using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Library;

namespace DichVu.Nuoc
{
    public class CachTinhCls
    {
        public byte MaTN { get; set; }
        public int? MaMB { get; set; }
        public int SoTieuThu { get; set; }
        public int SoUuDai { get; set; }
        public List<ChiTietNuocItem> ltChiTiet;

        private int MaCT
        {
            get
            {
                var db = new MasterDataContext();
                try
                {
                    var objCT = db.dvNuocCachTinhs.FirstOrDefault(p => p.MaMB == this.MaMB);
                    if (objCT == null)
                    {
                        var _MaLMB = (from pn in db.mbMatBangs where  pn.MaMB == this.MaMB select pn.MaLMB).FirstOrDefault();
                        objCT = db.dvNuocCachTinhs.FirstOrDefault(p => p.MaLMB == _MaLMB & p.MaMB == null);
                        if (objCT == null)
                        {
                            objCT = db.dvNuocCachTinhs.FirstOrDefault(p => p.MaTN == this.MaTN & p.MaLMB == null & p.MaMB == null);
                        }
                    }

                    if (objCT != null)
                    {
                        return objCT.MaCT.Value;
                    }
                    else
                    {
                        return 0;
                    }
                }
                catch {
                    return 0;
                }
                finally
                {
                    db.Dispose();
                }
            }
        }

        public void LoadDinhMuc()
        {
            var db = new MasterDataContext();
            try
            {
                ltChiTiet = (from dm in db.dvNuocDinhMucs
                             where dm.MaMB == this.MaMB & dm.IsKhoa.GetValueOrDefault() == false
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
                    var _MaLMB = (from pn in db.mbMatBangs where pn.MaMB == this.MaMB select pn.MaLMB).FirstOrDefault();
                    ltChiTiet = (from dm in db.dvNuocDinhMucs
                                 where dm.MaLMB == _MaLMB & dm.MaMB == null & dm.IsKhoa.GetValueOrDefault() == false
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
                        ltChiTiet = (from dm in db.dvNuocDinhMucs
                                     where dm.MaTN == this.MaTN & dm.MaLMB == null & dm.MaMB == null & dm.IsKhoa.GetValueOrDefault() == false
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
            switch (this.MaCT)
            {
                case 1:
                    this.LuyTien();
                    break;
                case 2:
                    this.UuDai();
                    break;
            }
        }

        void LuyTien()
        {
            for (var i = 0; i < ltChiTiet.Count; i++)
            {
                var objCT = ltChiTiet[i];

                var _DinhMuc = (i == 0 && SoUuDai > 0) ? SoUuDai : objCT.DinhMuc;

                if (SoTieuThu > _DinhMuc && i < ltChiTiet.Count - 1)
                {
                    objCT.SoLuong = _DinhMuc;
                    SoTieuThu -= _DinhMuc.Value;
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

        void UuDai()
        {
            //if (this.MaTN == 62 & SoTieuThu>0)
            //{
            //    for (var i = 0; i < ltChiTiet.Count; i++)
            //    {
            //        var objCT = ltChiTiet[i];
            //        switch (i)
            //        {
            //            case 0:

            //                if (SoUuDai <= 10) 
            //                {

            //                    if (SoTieuThu - SoUuDai < 0)
            //                        objCT.SoLuong = SoTieuThu;
            //                    // Nếu mức ưu đãi nước mà nhỏ hơn 10 thì kiểm tra nếu số tiêu thụ mà nhỏ hơn mức hưởng thì set mức 1 = số tiêu thụ luôn
            //                    else
            //                        objCT.SoLuong = SoUuDai;
            //                    //Ngược lại thì nếu số tiêu thụ lớn hơn mức dc hưởng ở mức 1 thì lấy số mức ưu đãi luôn
            //                }
            //                else
            //                {
            //                    //Mức ưu đãi tren 10 thi kiểm tra thuộc khoảng nào
                              
            //                    if (SoTieuThu < SoUuDai 
            //                        & SoTieuThu <= 10)
            //                        objCT.SoLuong = SoTieuThu; // 
            //                    if (SoTieuThu >= SoUuDai)//Nếu lượng tiêu thụ lớn hơn số ưu đãi thì lấy giá trị lớn nhất ở mức 1 là 10
            //                        objCT.SoLuong = 10;
            //                    if (SoTieuThu < SoUuDai & SoTieuThu > 10)
            //                        // Kiểm tra thêm trường hợp tuy số tiêu thu nhỏ hơn ưu đãi 13<19 tuy nhỏ hơn nhưng vẫn vượt mức 1
            //                        objCT.SoLuong = 10;
            //                }
            //                break;
            //            case 1:// Muc uu dai tren 10 thi muc 2 se co tien nguoc lai neu <10 thi muc 2 set tien = 0 muc 3 se tinh tiếp tiền
                            
            //                var _I8 = ltChiTiet[i - 1].SoLuong;
            //                if (_I8 >= SoTieuThu)
            //                {
            //                    objCT.SoLuong = 0;// trường hợp mức 1 có ưu đãi <=8
            //                }
            //                else// trường hợp mức 1 có ưu đãi >8
            //                {
            //                    if (SoUuDai <= 8)
            //                    {
            //                        objCT.SoLuong = 0;
            //                    }

            //                    if (SoUuDai > 8)
            //                    {
            //                        if (SoTieuThu - SoUuDai >= 0 & SoTieuThu >= 10 & SoTieuThu <= 16)
            //                            objCT.SoLuong = SoTieuThu - _I8;//?????
            //                        if (SoTieuThu - SoUuDai >= 0 & SoTieuThu >= 10 & SoTieuThu > 16)
            //                            objCT.SoLuong = 16 - _I8;
            //                        if (SoTieuThu - SoUuDai <= 0 & SoTieuThu >= 10 & SoTieuThu <= 16)
            //                            objCT.SoLuong = SoTieuThu - _I8;
            //                         if (SoTieuThu - SoUuDai <= 0 & SoTieuThu >= 10 & SoTieuThu > 16)
            //                             objCT.SoLuong = 16 - _I8;
            //                    }
            //                }     
            //                break;
            //            case 2:
                             
            //                if (SoUuDai <= 8)
            //                {
            //                    objCT.SoLuong = SoTieuThu - ltChiTiet[0].SoLuong;
            //                }

            //                if (SoUuDai > 8)
            //                {
            //                    if (SoTieuThu - SoUuDai>=0)
            //                        objCT.SoLuong = SoTieuThu - ltChiTiet[i - 1].SoLuong - ltChiTiet[i - 2].SoLuong;
            //                    if (SoTieuThu - SoUuDai <= 0 & ltChiTiet[i - 1].SoLuong < 6)
            //                        objCT.SoLuong =0;
            //                    if (SoTieuThu - SoUuDai <= 0 & ltChiTiet[i - 1].SoLuong>=6)
            //                        objCT.SoLuong = SoTieuThu-16;
            //                }
            //                break;
            //        }

            //        objCT.DonGia = objCT.DonGia;
            //        objCT.ThanhTien = objCT.DonGia * objCT.SoLuong;
            //        objCT.DienGiai = objCT.DienGiai;
            //    }
            //}
            //else
            //{
                for (var i = 0; i < ltChiTiet.Count; i++)
                {
                    var objCT = ltChiTiet[i];
                    switch (i)
                    {
                        case 0:
                            //=IF((G8-H8)<0,G8, H8)
                            if (SoTieuThu - SoUuDai < 0)
                                objCT.SoLuong = SoTieuThu;
                            else
                                objCT.SoLuong = SoUuDai;
                            break;
                        case 1:
                            //=IF((G8-I8)>=(I8/2),(I8/2),(G8-I8))
                            var _I8 = ltChiTiet[i - 1].SoLuong;
                            if (SoTieuThu - _I8 >= _I8 / 2)
                                objCT.SoLuong = _I8 / 2;
                            else
                                objCT.SoLuong = SoTieuThu - _I8;
                            break;
                        case 2:
                            //=ROUND(G8-I8-K8,0)
                            objCT.SoLuong = SoTieuThu - ltChiTiet[i - 1].SoLuong - ltChiTiet[i - 2].SoLuong;
                            break;
                    }

                    objCT.DonGia = objCT.DonGia;
                    objCT.ThanhTien = Math.Round((decimal)(objCT.DonGia * objCT.SoLuong),0,MidpointRounding.AwayFromZero);
                    objCT.DienGiai = objCT.DienGiai;
               // }
            }
            
        }
       
        public decimal GetThanhTien()
        {
            return this.ltChiTiet.Sum(p => p.ThanhTien).GetValueOrDefault();
        }
    }
}
