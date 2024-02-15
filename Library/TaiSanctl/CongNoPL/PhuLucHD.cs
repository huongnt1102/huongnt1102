using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.CongNoPL
{

    public class PhuLucHD
    {
        MasterDataContext db;
        public decimal TongTienThanhToan(decimal DonGia, decimal DienTich, decimal ThueXuat)
        {
            return DonGia * DienTich * ThueXuat;
        }

        public bool DenKyThanhToan(int ChuKyThanhToanTinhTheoThang)
        {
            db = new MasterDataContext();
            int ThangHienTai = db.GetSystemDate().Month;
            if (ThangHienTai % ChuKyThanhToanTinhTheoThang == 0)
                return true;
            else
                return false;
        }

        public List<int> ThangBatDau(int ChuKyThanhToan, int ThangThanhToanDauTien, int SoLanThanhToan)
        {
            db = new MasterDataContext();
            List<int> result = new List<int>();
            result.Add(ThangThanhToanDauTien);
            for (int i = 1; i < SoLanThanhToan; i++)
            {
                result.Add(ThangThanhToanDauTien + (ChuKyThanhToan * i));
            }

            return result;
        }

        public List<ChuKyCls> DanhSachChuKyThanhToan(thuePhuLuc _objhopdong)
        {
            db = new MasterDataContext();
            DateTime BatDauThanhToan;
            DateTime max = DateTime.Now;
            DateTime min = DateTime.Now;

            var hdtcls = new Library.CongNoPL.PhuLucHD();
            BatDauThanhToan = (DateTime)db.thuePhuLucs.Single(p => p.ID == _objhopdong.ID).NgayGiao;
            //int chuky = _objhopdong.ChuKyThanhToan ?? 1;
            //int SoLanThanhToan = (int)((_objhopdong.ThoiHan ?? 12) / (_objhopdong.MaTG == 1 ? _objhopdong.ChuKyThanhToan : _objhopdong.ChuKyThanhToanUSD));


            int chuky = (int)(_objhopdong.TyGiaPL.Value == 1 ? _objhopdong.ChuKyThanhToan : _objhopdong.ChuKyThanhToanUSD);
            int SoLanThanhToan = (int)(_objhopdong.ThoiHan % chuky == 0 ? _objhopdong.ThoiHan / chuky : _objhopdong.ThoiHan / chuky + 1);

            int sothangdu = (int)((_objhopdong.ThoiHan ?? 12) % chuky);
            //if (sothangdu > 0)
            //{
            //    SoLanThanhToan++;
            //}
            List<int> tbd = hdtcls.ThangBatDau(chuky, BatDauThanhToan.Month, SoLanThanhToan);

            List<DateTime> lstdt = new List<DateTime>();
            foreach (int item in tbd)
            {
                DateTime dt = BatDauThanhToan;
                dt = dt.AddMonths((item % 12) - dt.Month);
                dt = dt.AddYears((item / 12));
                lstdt.Add(dt);
            }

            List<ChuKyCls> ckcls = new List<ChuKyCls>();
            for (int i = 0; i < lstdt.Count; i++)
            {
                if (BatDauThanhToan <= lstdt[i])
                {
                    max = (i + 1) < lstdt.Count ? lstdt[i].AddMonths(chuky).AddDays(-1) : sothangdu > 0 ? lstdt[i].AddMonths(sothangdu).AddDays(-1) : lstdt[i].AddMonths(chuky).AddDays(-1);
                    min = lstdt[i];
                    ChuKyCls objChuKy = new ChuKyCls();
                    objChuKy.Min = min;
                    objChuKy.Max = max;
                    ckcls.Add(objChuKy);
                }
            }
            return ckcls;
        }

        public ChuKyCls LayChuKyTheoThoiDiemHienTai(thuePhuLuc _objhopdong)
        {
            db = new MasterDataContext();
            List<ChuKyCls> lstchuky = new List<ChuKyCls>();
            DateTime BatDauThanhToan;
            BatDauThanhToan = (DateTime)db.thuePhuLucs.Single(p => p.ID == _objhopdong.ID).NgayGiao;
            if (BatDauThanhToan < db.GetSystemDate())
            {
                BatDauThanhToan = db.GetSystemDate();
            }
            lstchuky = DanhSachChuKyThanhToan(_objhopdong);
            ChuKyCls objChuKy = new ChuKyCls();
            foreach (ChuKyCls item in lstchuky)
            {
                if (BatDauThanhToan >= item.Min && BatDauThanhToan <= item.Max)
                {
                    objChuKy = item;
                    break;
                }
            }

            return objChuKy;
        }
    }
}

