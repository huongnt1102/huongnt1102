using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Library;

namespace LandSoftBuilding.Lease
{
    public static class SchedulePaymentCls
    {
        public static void ctLichThanhToan(ctLichThanhToan obj, bool IsLamTron)
        {
            using(var db = new MasterDataContext())
            {
                obj.GiaThue = Math.Round(  obj.DienTich.GetValueOrDefault() * (obj.PhiDichVu.GetValueOrDefault() + obj.DonGia.GetValueOrDefault()),0);
                obj.SoTien = Math.Round(obj.DienTich.GetValueOrDefault() * (obj.PhiDichVu.GetValueOrDefault() + obj.DonGia.GetValueOrDefault()) * obj.SoThang.GetValueOrDefault(), 0);
                obj.SoTienQD = Math.Round(obj.DienTich.GetValueOrDefault() * (obj.PhiDichVu.GetValueOrDefault() + obj.DonGia.GetValueOrDefault()) * obj.SoThang.GetValueOrDefault() * obj.TyGia.GetValueOrDefault(), 0);

                if (IsLamTron)
                    obj.SoTienQD = obj.SoTienQD.GetValueOrDefault().LamTron();
            }
        }
        public static void ctLichThanhToanCoVAT(ctLichThanhToan obj, bool IsLamTron)
        {
            using (var db = new MasterDataContext())
            {
                obj.GiaThue = Math.Round((obj.DienTich.GetValueOrDefault() * (obj.PhiDichVu.GetValueOrDefault() + obj.DonGia.GetValueOrDefault() + obj.PhiDieuHoaChieuSang.GetValueOrDefault())) + (obj.DienTich.GetValueOrDefault() * (obj.PhiDichVu.GetValueOrDefault() + obj.DonGia.GetValueOrDefault() + obj.PhiDieuHoaChieuSang.GetValueOrDefault())) * (obj.TyLeVAT != null ? obj.TyLeVAT.Value : 0), 0);
                obj.SoTien = Math.Round((obj.DienTich.GetValueOrDefault() * (obj.PhiDichVu.GetValueOrDefault() + obj.DonGia.GetValueOrDefault() + obj.PhiDieuHoaChieuSang.GetValueOrDefault()) * obj.SoThang.GetValueOrDefault()) + ((obj.DienTich.GetValueOrDefault() * (obj.PhiDichVu.GetValueOrDefault() + obj.DonGia.GetValueOrDefault() + obj.PhiDieuHoaChieuSang.GetValueOrDefault()) * obj.SoThang.GetValueOrDefault()) * (obj.TyLeVAT != null ? obj.TyLeVAT.Value : 0)), 0);
                obj.SoTienQD = Math.Round((obj.DienTich.GetValueOrDefault() * (obj.PhiDichVu.GetValueOrDefault() + obj.DonGia.GetValueOrDefault() + obj.PhiDieuHoaChieuSang.GetValueOrDefault()) * obj.SoThang.GetValueOrDefault() * obj.TyGia.GetValueOrDefault()) + ((obj.DienTich.GetValueOrDefault() * (obj.PhiDichVu.GetValueOrDefault() + obj.DonGia.GetValueOrDefault() + obj.PhiDieuHoaChieuSang.GetValueOrDefault()) * obj.SoThang.GetValueOrDefault() * obj.TyGia.GetValueOrDefault()) * (obj.TyLeVAT != null ? obj.TyLeVAT.Value : 0)), 0);

                if (IsLamTron)
                    obj.SoTienQD = obj.SoTienQD.GetValueOrDefault().LamTron();
            }
        }
        //public static void ctLichThanhToan_LamTronThanhTien(ctLichThanhToan obj, bool IsLamTron, bool IsLamTronGia)
        //{
        //    using (var db = new MasterDataContext())
        //    {
        //        obj.GiaThue = obj.DienTich.GetValueOrDefault() * obj.DonGia.GetValueOrDefault();

        //        if (IsLamTron && IsLamTronGia)
        //        {
        //            obj.GiaThue = obj.GiaThue.GetValueOrDefault().LamTron();
        //        }

        //        obj.TyLeCK = obj.TyLeCK.GetValueOrDefault();

        //        obj.TienCK = obj.GiaThue.GetValueOrDefault() * obj.TyLeCK.GetValueOrDefault();

        //        obj.GiaThueSauCK = obj.GiaThue - obj.TienCK;

        //        obj.TyLeVAT = obj.TyLeVAT.GetValueOrDefault();

        //        obj.TienVAT = obj.GiaThueSauCK * obj.TyLeVAT.GetValueOrDefault();

        //        obj.GiaThueSauVAT = obj.GiaThueSauCK + obj.TienVAT;

        //        obj.ThanhTien = obj.GiaThueSauVAT * obj.SoThang.GetValueOrDefault();

        //        obj.SoTien = obj.ThanhTien;

        //        if (obj.MaLoaiTien == null)
        //            obj.MaLoaiTien = 1;

        //        var _loaiTien = db.LoaiTiens.FirstOrDefault(o => o.ID == obj.MaLoaiTien);

        //        obj.TyGia = _loaiTien.TyGia;

        //        obj.SoTienQD = obj.SoTien * obj.TyGia;

        //        if (IsLamTron && !IsLamTronGia)
        //        {
        //            obj.SoTienQD = obj.SoTienQD.GetValueOrDefault().LamTron();
        //        }

        //    }
        //}

        public static void UpdateTyGia(ctLichThanhToan obj, int _maLoaiTien, decimal _tyGia)
        {
            using (var db = new MasterDataContext())
            {
                obj.MaLoaiTien = _maLoaiTien;
                obj.TyGia = _tyGia;
                obj.SoTienQD = obj.SoTien * obj.TyGia;
            }
        }

        public static decimal LamTron(this decimal _soTien)
        {
            var SoTienLe = _soTien % 1000;

            var SoTienChan = _soTien - SoTienLe;
            SoTienLe = SoTienLe <= 500 ? 0 : 1000;
            return SoTienLe + SoTienChan;
        }

    }
}
