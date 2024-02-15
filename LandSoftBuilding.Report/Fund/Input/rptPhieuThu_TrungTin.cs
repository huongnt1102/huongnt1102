using System;
using System.Drawing;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
using Library;

namespace LandSoftBuilding.Fund.Input
{
    public partial class rptPhieuThu_TrungTin : DevExpress.XtraReports.UI.XtraReport
    {
        public rptPhieuThu_TrungTin(int ID, byte MaTN)
        {
            InitializeComponent();

          //  Library.frmPrintControl.LoadLayout(this, 57, MaTN);

            if (ID == 0) return;

            var db = new Library.MasterDataContext();
            try
            {
                #region Thong tin toa nha
                var objTN = db.tnToaNhas.FirstOrDefault(p => p.MaTN == MaTN);
                cTenTN.Text = objTN.CongTyQuanLy;
                cDiaChiTN.Text = objTN.DiaChiCongTy;
                cDienThoaiTN.Text = "Tel: " + objTN.DienThoai;
                picLogo.ImageUrl = objTN.Logo;
                #endregion

                var objTien = new TienTeCls();
                var objPT = (from p in db.ptPhieuThus
                             join nv in db.tnNhanViens on p.MaNVN equals nv.MaNV
                             where p.ID == ID
                             select new
                             {
                                 p.MaTN,
                                 p.SoPT,
                                 p.NgayThu,
                                 p.NguoiNop,
                                 p.DiaChiNN,
                                 p.LyDo,
                                 p.SoTien,
                                 SoTien_BangChu = objTien.DocTienBangChu(p.SoTien.Value, "đồng chẵn"),
                                 nv.HoTenNV
                             }).FirstOrDefault();

                cSoPhieu.Text = "Số phiếu: " + objPT.SoPT;
                cNgayPT.Text = string.Format("Ngày {0:dd} tháng {0:MM} năm {0:yyyy}", objPT.NgayThu);
                cNguoiNop.Text = objPT.NguoiNop;
                cDiaChi.Text = objPT.DiaChiNN;
                cSoTien.Text = objPT.SoTien.Value.ToString("c0");
                cSoTienBC.Text = objPT.SoTien_BangChu;
                cNguoiLap.Text = objPT.HoTenNV;

                //Dien giai
                var strDienGiai = "";
                var ltChiTiet = (from ct in db.ptChiTietPhieuThus
                                 where ct.MaPT == ID
                                 select new
                                 {
                                     ct.DienGiai,
                                     ct.SoTien
                                 }).ToList();
                foreach (var i in ltChiTiet)
                {
                    strDienGiai += i.DienGiai + string.Format(" ({0:#,0}đ)", i.SoTien) + "; ";
                }
                cLyDo.Text = strDienGiai.Trim().Trim(';');
            }
            catch { }
            finally
            {
                db.Dispose();
            }
        }
    }
}
