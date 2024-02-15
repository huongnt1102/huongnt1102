using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Library;
using System.Linq;
using System.Collections.Generic;
using System.Data.Linq.SqlClient;


namespace LandSoftBuilding.Receivables.GiayBao
{
    public partial class rptHoaDonGiayBaoMau8 : DevExpress.XtraReports.UI.XtraReport
    {
        public rptHoaDonGiayBaoMau8(byte _MaTN, int _Thang, int _Nam, int _MaKH)
        {
            InitializeComponent();

            var db = new MasterDataContext();
            try
            {
                //Load layout
                Library.frmPrintControl.LoadLayout(this, 54, _MaTN);
                //Ngay in
                lblNgayRaGB.Text = db.GetSystemDate().ToString("dd/MM/yyyy");

                #region Thong tin khach hang
                var objKH = (from mb in db.mbMatBangs
                             join kh in db.tnKhachHangs on mb.MaKH equals kh.MaKH
                             where mb.MaKH == _MaKH
                             select new
                             {
                                 mb.MaMB,
                                 mb.MaSoMB,
                                 kh.MaPhu,
                                 TenKH = kh.IsCaNhan == true ? (kh.HoKH + " " + kh.TenKH) : kh.CtyTen
                             }).First();
                lblFullName.Text = objKH.TenKH;
                lblMaCanHo.Text = objKH.MaSoMB;
                lblMaKH.Text = objKH.MaPhu;
                #endregion

                #region Dich vu Nuoc
                var objNuoc = (from n in db.dvNuocs
                               where n.MaKH == _MaKH & n.MaMB == objKH.MaMB & n.NgayTT.Value.Year == _Nam & n.NgayTT.Value.Month == _Thang
                               select new
                               {
                                   n.ID,
                                   n.TuNgay,
                                   n.DenNgay,
                                   n.ChiSoCu,
                                   n.ChiSoMoi,
                                   n.SoTieuThu,
                                   n.SoNguoiUD,
                                   n.TienTT
                               }).FirstOrDefault();
                if (objNuoc != null)
                {
                    cellNhanKhau.Text = string.Format("{0:n0}", objNuoc.SoNguoiUD);
                    cellNuocDienGiai.Text = string.Format("Nước - Water\r\n\r\nTừ (From) {0:dd/MM/yyyy} đến (to) {1:dd/MM/yyyy}", objNuoc.TuNgay, objNuoc.DenNgay);
                    cellNuocCSC.Text = string.Format("{0:n0}", objNuoc.ChiSoCu);
                    cellNuocCSD.Text = string.Format("{0:n0}", objNuoc.ChiSoMoi);
                    cellNuocTieuThu.Text = string.Format("{0:n0}", objNuoc.SoTieuThu);
                    cellNuocThanhTien.Text = string.Format("{0:n0}", objNuoc.TienTT);

                    var ltNuocChiTiet = from ct in db.dvNuocChiTiets
                                        join dm in db.dvNuocDinhMucs on ct.MaDM equals dm.ID
                                        where ct.MaNuoc == objNuoc.ID
                                        select new
                                        {
                                            dm.STT,
                                            ct.SoLuong,
                                            ct.DonGia,
                                            ct.ThanhTien
                                        };
                    foreach (var i in ltNuocChiTiet)
                    {
                        switch (i.STT.Value)
                        {
                            case 1:
                                cellMuc1.Text = string.Format("{0:n0}", i.SoLuong);
                                cellDGMuc1.Text = string.Format("{0:n0}", i.DonGia);
                                cellTienMuc1.Text = string.Format("{0:n0}", i.ThanhTien);
                                break;
                            case 2:
                                cellMuc2.Text = string.Format("{0:n0}", i.SoLuong);
                                cellDGMuc2.Text = string.Format("{0:n0}", i.DonGia);
                                cellTienMuc2.Text = string.Format("{0:n0}", i.ThanhTien);
                                break;
                            case 3:
                                cellMuc3.Text = string.Format("{0:n0}", i.SoLuong);
                                cellDGMuc3.Text = string.Format("{0:n0}", i.DonGia);
                                cellTienMuc3.Text = string.Format("{0:n0}", i.ThanhTien);
                                break;
                        }
                    }
                }
                #endregion

                #region Dich vu GAS
                var objGas = (from g in db.dvGas
                              join ct in db.dvGasChiTiets on g.ID equals ct.MaGas
                              where g.MaKH == _MaKH & g.MaMB == objKH.MaMB & g.NgayTT.Value.Year == _Nam & g.NgayTT.Value.Month == _Thang
                              select new
                              {
                                  g.ID,
                                  g.TuNgay,
                                  g.DenNgay,
                                  g.ChiSoCu,
                                  g.ChiSoMoi,
                                  g.SoTieuThu,
                                  ct.DonGia,
                                  g.TienTT
                              }).FirstOrDefault();
                if (objGas != null)
                {
                    cellGasDienGiai.Text = string.Format("Ga - Gas\r\n\r\nTừ (From) {0:dd/MM/yyyy} đến (to) {1:dd/MM/yyyy}", objGas.TuNgay, objGas.DenNgay);
                    cellGasCSC.Text = string.Format("{0:n0}", objGas.ChiSoCu);
                    cellGasCSD.Text = string.Format("{0:n0}", objGas.ChiSoMoi);
                    cellGasTieuThu.Text = string.Format("{0:n0}", objGas.SoTieuThu);
                    cellGasDG.Text = string.Format("{0:n0}", objGas.DonGia);
                    cellGasThanhTien.Text = string.Format("{0:n0}", objGas.TienTT);
                }
                #endregion

                #region Phi quan ly
                var objPQL = (from p in db.dvDichVuKhacs
                              where p.MaKH == _MaKH & p.MaMB == objKH.MaMB & p.MaLDV == (int)MaLDVs.PQL
                              select new
                              {
                                  p.SoLuong,
                                  p.DonGia,
                                  p.TienTT
                              }).FirstOrDefault();
                cellPQLDienGiai.Text = string.Format("Tháng {0}/{1}", _Thang, _Nam);
                if (objPQL != null)
                {
                    cellDienTich.Text = string.Format("{0:#,0.##}", objPQL.SoLuong);
                    cellPQLDG.Text = string.Format("{0:n0}", objPQL.DonGia);
                    cellPQLThanhTien.Text = string.Format("{0:n0}", objPQL.TienTT);
                }
                #endregion

                #region Phi ve sinh
                var objPVS = (from p in db.dvDichVuKhacs
                              where p.MaKH == _MaKH & p.MaMB == objKH.MaMB & p.MaLDV == (int)MaLDVs.PVS
                              select new
                              {
                                  p.SoLuong,
                                  p.DonGia,
                                  p.TienTT
                              }).FirstOrDefault();
                cellPVSDienGiai.Text = string.Format("Tháng {0}/{1}", _Thang, _Nam);
                if (objPQL != null)
                {
                    cellPVSDG.Text = string.Format("{0:n0}", objPQL.DonGia);
                    cellPVSThanhTien.Text = string.Format("{0:n0}", objPQL.TienTT);
                }
                #endregion

                #region Phi giu xe
                var ltGiuXe = from tx in db.dvgxTheXes
                              join lx in db.dvgxLoaiXes on tx.MaLX equals lx.MaLX
                              where tx.MaKH == _MaKH & tx.MaMB == objKH.MaMB
                              group tx by lx.MaLX into gr
                              select new
                              {
                                  gr.Key,
                                  SoLuong = gr.Count(),
                                  DonGia = gr.Max(p => p.GiaThang),
                                  ThanhTien = gr.Sum(p => p.TienTT)
                              };
                cellMucOtoText.Text = cellMucXeMayText.Text = cellMucXeDapText.Text = string.Format("Tháng {0}/{1}", _Thang, _Nam);
                foreach (var i in ltGiuXe)
                {
                    switch (i.Key)
                    {
                        case 1:
                            cellPGXTTOto.Text = string.Format("{0:n0}", i.SoLuong);
                            cellPGXDGOto.Text = string.Format("{0:n0}", i.DonGia);
                            cellThanhTienOto.Text = string.Format("{0:n0}", i.ThanhTien);
                            break;
                        case 2:
                            cellPGXTTXeMay.Text = string.Format("{0:n0}", i.SoLuong);
                            cellPGXDGXeMay.Text = string.Format("{0:n0}", i.DonGia);
                            cellThanhTienXeMay.Text = string.Format("{0:n0}", i.ThanhTien);
                            break;
                        case 3:
                            cellPGXTTXeDap.Text = string.Format("{0:n0}", i.SoLuong);
                            cellPGXDGXeDap.Text = string.Format("{0:n0}", i.DonGia);
                            cellThanhTienXeDap.Text = string.Format("{0:n0}", i.ThanhTien);
                            break;
                    }
                }
                #endregion

                #region No cu, Tong no
                var _Ngay = new DateTime(_Nam, _Thang, 1);
                //No ky truoc
                var _NoCu = (from hd in db.dvHoaDons
                             where hd.IsDuyet == true & hd.MaKH == _MaKH & hd.ConNo.GetValueOrDefault() > 0 & SqlMethods.DateDiffDay(hd.NgayTT, _Ngay) > 0
                             select hd.ConNo).Sum().GetValueOrDefault();
                cellTotalOfDebt.Text = string.Format("{0:n0}", _NoCu);
                //Tong no
                var _TongNo = (from hd in db.dvHoaDons
                               where hd.IsDuyet == true & hd.MaKH == _MaKH & hd.ConNo.GetValueOrDefault() > 0 & SqlMethods.DateDiffDay(hd.NgayTT, _Ngay) >= 0
                               select hd.ConNo).Sum().GetValueOrDefault();
                cellTongCong.Text = string.Format("{0:n0}", _TongNo);
                //Tien bang chu
                var _TienBangChu = new TienTeCls().DocTienBangChu(Convert.ToInt64(Math.Round(_TongNo, 0)));
                cellTongCongBCEN.Text = _TienBangChu;
                #endregion
            }
            catch { }
            finally
            {
                db.Dispose();
            }
        }
    }
}
