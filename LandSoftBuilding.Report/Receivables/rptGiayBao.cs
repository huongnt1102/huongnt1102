using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
using Library;
using System.Collections.Generic;
using System.Data.Linq.SqlClient;
using DevExpress.XtraRichEdit;
using DevExpress.XtraRichEdit.API.Native;
using DevExpress.XtraReports.Parameters;

namespace LandSoftBuilding.Receivables
{
    public partial class rptGiayBao : DevExpress.XtraReports.UI.XtraReport
    {
        List<GiayBaoItem> ListGiayBao = new List<GiayBaoItem>();

        MasterDataContext db = new MasterDataContext();
        byte MaTN;
        int MaKH, Thang, Nam, ThangDV, NamDV;

        public rptGiayBao(byte _MaTN, int _Thang, int _Nam, int _MaKH, List<int> _MaLDVs, int _MaTKNH)
        {
            InitializeComponent();

            try
            {
                this.MaTN = _MaTN;
                this.MaKH = _MaKH;
                this.Thang = _Thang;
                this.Nam = _Nam;
                var _DenNgay = Common.GetLastDayOfMonth(_Thang, _Nam);
                //Load layout
                Library.frmPrintControl.LoadLayout(this, 18, _MaTN);

                #region Setup Binding data
                cgrSTT.DataBindings.Add("Text", null, "STT");
                cgrTenLDV.DataBindings.Add("Text", null, "TenLDV");
                cgrChiSoCu.DataBindings.Add("Text", null, "ChiSoCu", "{0:#,0.##}");
                cgrChiSoMoi.DataBindings.Add("Text", null, "ChiSoMoi", "{0:#,0.##}");
                cgrSoLuong.DataBindings.Add("Text", null, "SoLuong", "{0:#,0.##}");
                cgrTenDVT.DataBindings.Add("Text", null, "TenDVT");
                cgrHeSo.DataBindings.Add("Text", null, "HeSo", "{0:#,0.##}");
                cgrDonGia.DataBindings.Add("Text", null, "DonGia", "{0:#,0}");
                cgrThanhTien.DataBindings.Add("Text", null, "ThanhTien", "{0:#,0}");

                cSTT.DataBindings.Add("Text", null, "STT");
                cSTT.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.RecordNumber, "{0:#}");
                cDienGiai.DataBindings.Add("Text", null, "TenLDV");
                cChiSoCu.DataBindings.Add("Text", null, "ChiSoCu", "{0:#,0.##}");
                cChiSoMoi.DataBindings.Add("Text", null, "ChiSoMoi", "{0:#,0.##}");
                cSoLuong.DataBindings.Add("Text", null, "SoLuong", "{0:#,0.##}");
                cTenDVT.DataBindings.Add("Text", null, "TenDVT");
                cHeSo.DataBindings.Add("Text", null, "HeSo", "{0:#,0.##}");
                cDonGia.DataBindings.Add("Text", null, "DonGia", "{0:#,0}");
                cThanhTien.DataBindings.Add("Text", null, "ThanhTien", "{0:#,0}");
                #endregion

                //Gan datasouce
                bindingSource1.DataSource = this.ListGiayBao;
                cThoiGian.Text = string.Format("Hà Nội ,Ngày {0} tháng {1} năm {2}", DateTime.Now.Day,
                   DateTime.Now.Month, DateTime.Now.Year);
                //Thong tin toa nha
                var objTN = (from tn in db.tnToaNhas
                             where tn.MaTN == _MaTN
                             select new { tn.TenTN, tn.CongTyQuanLy, tn.DiaChiCongTy, tn.Logo })
                             .FirstOrDefault();
                //Thong tin khach hang
                var objKH = (from mb in db.mbMatBangs
                             join tl in db.mbTangLaus on mb.MaTL equals tl.MaTL
                             join kn in db.mbKhoiNhas on tl.MaKN equals kn.MaKN
                             join kh in db.tnKhachHangs on mb.MaKH equals kh.MaKH
                             where mb.MaKH == _MaKH
                             select new
                             {
                                 mb.MaSoMB,
                                 tl.TenTL,
                                 kn.TenKN,
                                 kh.MaPhu,
                                 kh.KyHieu,
                                 TenKH = kh.IsCaNhan == true ? (kh.HoKH + " " + kh.TenKH) : kh.CtyTen,
                                 CtyDiaChi=kh.DCLL,
                                 ThuTruoc = (from pt in db.ptPhieuThus
                                             where pt.MaKH == _MaKH & pt.MaPL == 2 & SqlMethods.DateDiffDay(pt.NgayThu, _DenNgay) >= 0
                                             select pt.SoTien).Sum().GetValueOrDefault()
                                              - (from pt in db.ktttKhauTruThuTruocs
                                                 where pt.MaKH == _MaKH & SqlMethods.DateDiffDay(pt.NgayCT, _DenNgay) >= 0
                                                 select pt.SoTien).Sum().GetValueOrDefault()
                             }).First();
                //Thong tin tai khoan
                var objTK = (from tk in db.nhTaiKhoans
                             join nh in db.nhNganHangs on tk.MaNH equals nh.ID
                             where tk.ID == _MaTKNH
                             select new { tk.ChuTK, tk.SoTK, nh.TenNH }).Single();
                //Du lieu hoa don
                var ltLoaiDichVu = (from hd in db.dvHoaDons
                                    join ldv in db.dvLoaiDichVus on hd.MaLDV equals ldv.ID
                                    where hd.MaLDV != 23 & hd.IsDuyet == true & hd.MaKH == _MaKH & hd.NgayTT.Value.Month == _Thang & hd.NgayTT.Value.Year == _Nam 
                                      & (_MaLDVs.Contains(hd.MaLDV.Value) | _MaLDVs.Count == 0)
                                    group hd by new { ldv.STT, hd.MaLDV, ldv.TenHienThi, ldv.TenTA } into gr
                                    orderby gr.Key.STT
                                    select new
                                    {
                                        gr.Key.STT,
                                        gr.Key.MaLDV,
                                        TenLDV = gr.Key.TenHienThi,
                                        gr.Key.TenTA,
                                        SoTien = gr.Sum(p => p.PhaiThu)
                                    }).ToList();
                //
                var _NoCu = (from hd in db.dvHoaDons
                             where hd.IsDuyet == true & hd.MaKH == _MaKH & hd.ConNo.GetValueOrDefault() > 0 & SqlMethods.DateDiffDay(hd.NgayTT, new DateTime(_Nam, _Thang, 1)) > 0
                                 & (_MaLDVs.Contains(hd.MaLDV.Value) | _MaLDVs.Count == 0)
                             select hd.ConNo).Sum().GetValueOrDefault();
                var _TienLai = this.TinhTienLai();
                var _PhatSinh = ltLoaiDichVu.Sum(p => p.SoTien).GetValueOrDefault();
                var _TongTien = Convert.ToInt64(Math.Round(_PhatSinh + _NoCu + _TienLai, 0));
                var _TienBangChu = new TienTeCls().DocTienBangChu(_TongTien);

                #region Master report
                var objGB = new GiayBaoItem();
                objGB.NgayIn = db.GetSystemDate();
                objGB.Thang = this.Thang;
                objGB.Nam = this.Nam;
                objGB.ThangTA = Commoncls.GetMonthEnglish(this.Thang);
                objGB.ThangTB = string.Format("Tháng {0:00}-{1}/ {2}, {1}", objGB.Thang, objGB.Nam, objGB.ThangTA);
                objGB.TenTN = objTN.TenTN;
                objGB.TenCty = objTN.CongTyQuanLy;
                objGB.DiaChiCty = objTN.DiaChiCongTy;
                objGB.LoGoUrl = objTN.Logo;
                objGB.TangLau = objKH.TenTL;
                objGB.Toa = objKH.TenKN;
                objGB.TenNH = objTK.TenNH;
                objGB.ChuTKNH = objTK.ChuTK;
                objGB.SoTKNH = objTK.SoTK;

                objGB.MaSoMB = objKH.MaSoMB;
                objGB.MaSoKH = objKH.KyHieu;
                objGB.MaPhuKH = objKH.MaPhu;
                objGB.TenKH = objKH.TenKH;
                objGB.DiaChiKH = objKH.CtyDiaChi;

                objGB.PhatSinh = _PhatSinh;
                objGB.NoDauKy = _NoCu;
                objGB.TienLai = _TienLai;
                objGB.NoVaLai = objGB.NoDauKy + objGB.TienLai;
                objGB.TongTien = _TongTien;
                objGB.TongTienBangChu = _TienBangChu;
                objGB.ThuTruoc = objKH.ThuTruoc;
                objGB.DichVus = new List<DichVuItem>();
                #endregion

                #region Chi tiet dich vu
                int _SoLaMa = 0;
                foreach (var ldv in ltLoaiDichVu)
                {
                    _SoLaMa++;
                    this.ThangDV = (ldv.MaLDV == 5 || ldv.MaLDV == 8 || ldv.MaLDV == 9 || ldv.MaLDV == 10 || ldv.MaLDV == 11 || ldv.MaLDV == 20 || ldv.MaLDV == 22 || ldv.MaLDV == 23) ? (this.Thang - 1) : this.Thang;
                    this.NamDV = this.Nam;
                    if (this.ThangDV == 0)
                    {
                        this.ThangDV = 12;
                        this.NamDV--;
                    }

                    var objLDV = new DichVuItem();
                    objLDV.STT = RomanNumerals.ToRoman(_SoLaMa);
                    objLDV.MaLDV = ldv.MaLDV.GetValueOrDefault();
                    objLDV.TenLDV = string.Format("{0}/{1} (Tháng {2:00}/{3})", ldv.TenLDV, ldv.TenTA, this.ThangDV, this.NamDV);
                    objLDV.ThanhTien = ldv.SoTien;
                    objLDV.Details = new List<DichVuItem>();

                    switch (objLDV.MaLDV)
                    {
                        case 2: //Tien thue
                            this.Load_TienThue(objLDV);
                            break;
                        case 3: //Tien sua chua noi that
                            this.Load_TienSuaChua(objLDV);
                            break;
                        case 4: //Tien dat coc
                            this.Load_TienDatCocChoThue(objLDV);
                            break;
                        case 5: //Dien 3 pha
                            this.Load_Dien3Pha(objLDV);
                            break;
                        case 6: //Phi giu xe
                            this.Load_GiuXe(objLDV);
                            break;
                        case 8: //Tien dien
                            this.Load_Dien(objLDV);
                            break;
                        case 9: //Tien nuoc
                            this.Load_Nuoc(objLDV);
                            break;
                        case 10: //Tien gas
                            this.Load_GAS(objLDV);
                            break;
                        case 11: //Dieu hoa 
                            this.Load_DieuHoa(objLDV);
                            break;
                        case 20: //Nuoc nong
                            this.Load_NuocNong(objLDV);
                            break;
                        case 22: //Tien nuoc sinh hoat
                            this.Load_NuocSinhHoat(objLDV);
                            break;
                        default: //Dich vu co ban
                            this.Load_DichVuCoBan(objLDV);
                            break;
                    }

                    objGB.DichVus.Add(objLDV);
                }
                #endregion

                //
                this.ListGiayBao.Add(objGB);
            }
            catch 
            {
            
            }
            finally
            {
                db.Dispose();
            }
        }
        
        void Load_DichVuCoBan(DichVuItem objLDV)
        {
            var ltData = (from dv in db.dvDichVuKhacs
                          join dvt in db.DonViTinhs on dv.MaDVT equals dvt.ID
                          where dv.MaTN == this.MaTN & dv.MaLDV == objLDV.MaLDV & dv.MaKH == this.MaKH & dv.TienTTQD > 0
                          select new
                          {
                              dv.DienGiai,
                              dv.SoLuong,
                              dvt.TenDVT,
                              DonGia = dv.DonGia * dv.TyGia,
                              ThanhTien = dv.TienTTQD
                          }).ToList();

            if (ltData.Count == 1)
            {
                if (ltData[0].SoLuong > 0 & ltData[0].DonGia > 0)
                {
                    objLDV.SoLuong = ltData[0].SoLuong;
                    objLDV.TenDVT = ltData[0].TenDVT;
                    objLDV.DonGia = ltData[0].DonGia;
                }
            }
            else if (ltData.Count > 1)
            {
                foreach (var i in ltData)
                {
                    var objGB = new DichVuItem();
                    objGB.TenLDV = i.DienGiai;
                    if (i.SoLuong > 0 & i.DonGia > 0)
                    {
                        objGB.SoLuong = i.SoLuong;
                        objGB.TenDVT = i.TenDVT;
                        objGB.DonGia = i.DonGia;
                    }
                    objGB.ThanhTien = i.ThanhTien;
                    objLDV.Details.Add(objGB);
                }
            }
        }

        void Load_Nuoc(DichVuItem objLDV)
        {
            var ltNuoc = (from n in db.dvNuocs
                          where n.MaTN == this.MaTN & n.MaKH == this.MaKH & n.DenNgay.Value.Month == this.ThangDV & n.DenNgay.Value.Year == this.NamDV & n.ThanhTien > 0
                          select new
                          {
                              n.ID,
                              n.ChiSoCu,
                              n.ChiSoMoi,
                              SoTieuThu = n.ChiSoMoi - n.ChiSoCu,
                              n.HeSo,
                              SoUuDai = (from ud in db.dvNuocUuDais where ud.MaMB == n.MaMB select ud.SoNguoi).FirstOrDefault(),
                              n.ThanhTien,
                              n.TyLeVAT,
                              n.TienVAT,
                              n.TyLeBVMT,
                              n.TienBVMT,
                              n.DienGiai
                          }).ToList();
            
            foreach (var n in ltNuoc)
            {
                DichVuItem objGB = null;

                if (ltNuoc.Count == 1)
                {
                    objLDV.ChiSoCu = n.ChiSoCu;
                    objLDV.ChiSoMoi = n.ChiSoMoi;
                    objLDV.SoLuong = n.SoTieuThu;
                    objLDV.TenDVT = "m3";
                    objLDV.HeSo = n.HeSo;
                }
                else
                {
                    objGB = new DichVuItem();
                    objGB.TenDVT = " * " + n.DienGiai;
                    objGB.ChiSoCu = n.ChiSoCu;
                    objGB.ChiSoMoi = n.ChiSoMoi;
                    objGB.SoLuong = n.SoTieuThu;
                    objGB.TenDVT = "m3";
                    objGB.HeSo = n.HeSo;
                    objGB.ThanhTien = n.ThanhTien;
                    objLDV.Details.Add(objGB);
                }

                var ltData = (from ct in db.dvNuocChiTiets
                              join dm in db.dvNuocDinhMucs on ct.MaDM equals dm.ID
                              where ct.MaNuoc == n.ID
                              orderby dm.STT
                              select new
                              {
                                  dm.TenDM,
                                  ct.SoLuong,
                                  ct.DonGia,
                                  ct.ThanhTien
                              }).ToList();

                foreach (var i in ltData)
                {
                    if (ltData.Count == 1 & n.TienVAT <= 0 & n.TienBVMT <= 0)
                    {
                        if (objGB != null)
                            objGB.DonGia = i.DonGia;
                        else
                            objLDV.DonGia = i.DonGia;
                    }
                    else
                    {
                        objGB = new DichVuItem();
                        objGB.TenLDV = "  + " + i.TenDM;
                        objGB.SoLuong = i.SoLuong;
                        objGB.TenDVT = "m3";
                        objGB.DonGia = i.DonGia;
                        objGB.ThanhTien = i.ThanhTien;
                        objLDV.Details.Add(objGB);
                    }
                }

                if (n.TienVAT > 0)
                {
                    objGB = new DichVuItem();
                    objGB.TenLDV = string.Format("  + Thuế VAT {0:p0}", n.TyLeVAT);
                    objGB.ThanhTien = n.TienVAT;
                    objLDV.Details.Add(objGB);
                }

                if (n.TienBVMT > 0)
                {
                    objGB = new DichVuItem();
                    objGB.TenLDV = string.Format("  + Phí bảo vệ môi trường {0:p0}", n.TyLeBVMT);
                    objGB.ThanhTien = n.TienBVMT;
                    objLDV.Details.Add(objGB);
                }
            }
        }

        void Load_Dien(DichVuItem objLDV)
        {
            var ltDien = (from d in db.dvDiens
                          where d.MaTN == this.MaTN & d.MaKH == this.MaKH & d.DenNgay.Value.Month == this.ThangDV & d.DenNgay.Value.Year == this.NamDV & d.ThanhTien > 0
                           select new
                           {
                               d.ID,
                               d.ChiSoCu,
                               d.ChiSoMoi,
                               d.HeSo,
                               SoTieuThu = d.ChiSoMoi - d.ChiSoCu,
                               d.ThanhTien,
                               d.TyLeVAT,
                               d.TienVAT,
                               d.DienGiai
                           }).ToList();

            foreach (var d in ltDien)
            {
                DichVuItem objGB = null;

                if (ltDien.Count == 1)
                {
                    objLDV.ChiSoCu = d.ChiSoCu;
                    objLDV.ChiSoMoi = d.ChiSoMoi;
                    objLDV.SoLuong = d.SoTieuThu;
                    objLDV.TenDVT = "kw";
                    objLDV.HeSo = d.HeSo;
                }
                else
                {
                    objGB = new DichVuItem();
                    objGB.TenDVT = " * " + d.DienGiai;
                    objGB.ChiSoCu = d.ChiSoCu;
                    objGB.ChiSoMoi = d.ChiSoMoi;
                    objGB.SoLuong = d.SoTieuThu;
                    objGB.TenDVT = "kw";
                    objGB.HeSo = d.HeSo;
                    objGB.ThanhTien = d.ThanhTien;
                    objLDV.Details.Add(objGB);
                }

                var ltData = (from ct in db.dvDienChiTiets
                              join dm in db.dvDienDinhMucs on ct.MaDM equals dm.ID
                              where ct.MaDien == d.ID
                              orderby dm.STT
                              select new
                              {
                                  dm.TenDM,
                                  ct.SoLuong,
                                  ct.DonGia,
                                  ct.ThanhTien
                              }).ToList();

                foreach (var i in ltData)
                {
                    if (ltData.Count == 1 & d.TienVAT <= 0)
                    {
                        if (objGB != null)
                            objGB.DonGia = i.DonGia;
                        else
                            objLDV.DonGia = i.DonGia;
                    }
                    else
                    {
                        objGB = new DichVuItem();
                        objGB.TenLDV = "  + " + i.TenDM;
                        objGB.SoLuong = i.SoLuong;
                        objGB.TenDVT = "kw";
                        objGB.DonGia = i.DonGia;
                        objGB.ThanhTien = i.ThanhTien;
                        objLDV.Details.Add(objGB);
                    }
                }

                if (d.TienVAT > 0)
                {
                    objGB = new DichVuItem();
                    objGB.TenLDV = string.Format("  + Thuế VAT {0:p0}", d.TyLeVAT);
                    objGB.ThanhTien = d.TienVAT;
                    objLDV.Details.Add(objGB);
                }
            }
        }

        void Load_GAS(DichVuItem objLDV)
        {
            var ltGAS = (from g in db.dvGas
                         join dvt in db.dvgDonViTinhs on g.MaDVT equals dvt.ID
                         where g.MaTN == this.MaTN & g.MaKH == this.MaKH & g.DenNgay.Value.Month == this.ThangDV & g.DenNgay.Value.Year == this.NamDV & g.ThanhTien > 0
                         select new
                         {
                             g.ID,
                             g.ChiSoCu,
                             g.ChiSoMoi,
                             SoTieuThu = g.ChiSoMoi - g.ChiSoCu,
                             dvt.TenDVT,
                             HeSo = g.TyLe,
                             g.ThanhTien,
                             g.TyLeVAT,
                             g.TienVAT,
                             g.DienGiai
                         }).ToList();
            foreach (var g in ltGAS)
            {
                DichVuItem objGB = null;

                if (ltGAS.Count == 1)
                {
                    objLDV.ChiSoCu = g.ChiSoCu;
                    objLDV.ChiSoMoi = g.ChiSoMoi;
                    objLDV.SoLuong = g.SoTieuThu;
                    objLDV.TenDVT = g.TenDVT;
                    objLDV.HeSo = g.HeSo;
                }
                else
                {
                    objGB = new DichVuItem();
                    objGB.TenDVT = " * " + g.DienGiai;
                    objGB.ChiSoCu = g.ChiSoCu;
                    objGB.ChiSoMoi = g.ChiSoMoi;
                    objGB.SoLuong = g.SoTieuThu;
                    objGB.TenDVT = g.TenDVT;
                    objGB.HeSo = g.HeSo;
                    objGB.ThanhTien = g.ThanhTien;
                    objLDV.Details.Add(objGB);
                }

                var ltData = (from ct in db.dvGasChiTiets
                              join dm in db.dvGasDinhMucs on ct.MaDM equals dm.ID
                              where ct.MaGas == g.ID
                              orderby dm.STT
                              select new
                              {
                                  dm.TenDM,
                                  ct.SoLuong,
                                  ct.DonGia,
                                  ct.ThanhTien
                              }).ToList();

                foreach (var i in ltData)
                {
                    if (ltData.Count == 1 & g.TienVAT <= 0)
                    {
                        if (objGB != null)
                            objGB.DonGia = i.DonGia;
                        else
                            objLDV.DonGia = i.DonGia;
                    }
                    else
                    {
                        objGB = new DichVuItem();
                        objGB.TenLDV = "  + " + i.TenDM;
                        objGB.SoLuong = i.SoLuong;
                        objGB.TenDVT = g.TenDVT;
                        objGB.DonGia = i.DonGia;
                        objGB.ThanhTien = i.ThanhTien;
                        objLDV.Details.Add(objGB);
                    }
                }

                if (g.TienVAT > 0)
                {
                    objGB = new DichVuItem();
                    objGB.TenLDV = string.Format("  + Thuế VAT {0:p0}", g.TyLeVAT);
                    objGB.ThanhTien = g.TienVAT;
                    objLDV.Details.Add(objGB);
                }
            }
        }

        void Load_GiuXe(DichVuItem objLDV)
        {
            var ltGiuXe = (from tx in db.dvgxTheXes
                           join lx in db.dvgxLoaiXes on tx.MaLX equals lx.MaLX
                           where tx.MaTN == this.MaTN & tx.MaKH == this.MaKH & tx.NgungSuDung == false & tx.GiaThang > 0
                           group tx by new { tx.MaLX, lx.TenLX, tx.GiaThang } into gr
                           select new GiuXeItem()
                           {
                               MaLX = gr.Key.MaLX,
                               TenLX = gr.Key.TenLX,
                               BienSo = "",
                               SoLuong = gr.Count(),
                               DonGia = gr.Key.GiaThang,
                               ThanhTien = gr.Count() * gr.Key.GiaThang
                           }).ToList();
            foreach (var i in ltGiuXe)
            {
                if (i.SoLuong < 6)
                {
                    var ltBienSo = (from tx in db.dvgxTheXes
                                    where tx.MaTN == this.MaTN & tx.MaKH == this.MaKH & tx.NgungSuDung == false & tx.MaLX == i.MaLX & tx.GiaThang == i.DonGia
                                    select tx.BienSo).ToList();

                    foreach (var bs in ltBienSo)
                        if (!string.IsNullOrEmpty(bs))
                            i.BienSo += bs + "; ";

                    i.BienSo = i.BienSo.Trim(' ').Trim(';');
                }

                var objGB = new DichVuItem();
                objGB.TenLDV = i.TenLX + (!string.IsNullOrEmpty(i.BienSo) ? (" (" + i.BienSo + ")") : "");
                objGB.SoLuong = i.SoLuong;
                objGB.TenDVT = "chiếc";
                objGB.DonGia = i.DonGia;
                objGB.ThanhTien = i.ThanhTien;
                objLDV.Details.Add(objGB);
            }
        }

        decimal TinhTienLai()
        {
            var ltData = (from hd in db.dvHoaDons
                          where hd.MaKH == this.MaKH & hd.ConNo.GetValueOrDefault() > 0 & hd.MaLDV != 23 & hd.IsDuyet == true
                          select new { hd.MaLDV, hd.NgayTT, hd.ConNo }).ToList();

            decimal _TienLai = 0;
            var _NgayThu = new DateTime(this.Nam, this.Thang, 1);
            foreach (var i in ltData)
            {
                _TienLai += db.GetTienLai(this.MaTN, i.MaLDV, i.NgayTT, i.ConNo, _NgayThu).GetValueOrDefault();
            }

            return _TienLai;
        }

        void Load_TienThue(DichVuItem objLDV)
        {
            var ltData = (from hd in db.dvHoaDons
                          join ltt in db.ctLichThanhToans on new { hd.TableName, hd.LinkID } equals new { TableName = "ctLichThanhToan", LinkID = (int?)ltt.ID }
                          join ct in db.ctChiTiets on ltt.MaHD equals ct.MaHDCT
                          join cthd in db.ctHopDongs on ct.MaHDCT equals cthd.ID
                          join mb in db.mbMatBangs on ct.MaMB equals mb.MaMB
                          join tl in db.mbTangLaus on mb.MaTL equals tl.MaTL
                          where hd.MaTN == this.MaTN & hd.MaLDV == objLDV.MaLDV & hd.MaKH == this.MaKH & hd.NgayTT.Value.Month == this.Thang & hd.NgayTT.Value.Year == this.Nam
                          select new
                          {
                              ltt.DienGiai,
                              SoLuong = ct.DienTich,
                              HeSo = hd.KyTT,
                              DonGia = ct.DonGia * cthd.TyGia + ct.PhiDichVu * cthd.TyGia,
                              ThanhTien = ct.ThanhTien * cthd.TyGia * hd.KyTT
                          }).ToList();
            if (ltData.Count == 1)
            {
                objLDV.SoLuong = ltData[0].SoLuong;
                objLDV.TenDVT = "m2";
                objLDV.HeSo = ltData[0].HeSo;
                objLDV.DonGia = ltData[0].DonGia;
            }
            else if (ltData.Count>1)
            {
                foreach (var i in ltData)
                {
                    var objGB = new DichVuItem();
                    objGB.TenLDV = i.DienGiai;
                    objGB.SoLuong = i.SoLuong;
                    objGB.TenDVT = "m2";
                    objGB.HeSo = i.HeSo;
                    objGB.DonGia = i.DonGia;
                    objGB.ThanhTien = i.ThanhTien;
                    objLDV.Details.Add(objGB);
                }
            }
        }

        void Load_TienSuaChua(DichVuItem objLDV)
        {
            var ltData = (from hd in db.dvHoaDons
                          join ltt in db.ctLichThanhToans on new { hd.TableName, hd.LinkID } equals new { TableName = "ctLichThanhToan", LinkID = (int?)ltt.ID }
                          join ct in db.ctChiTiets on ltt.MaHD equals ct.MaHDCT
                          join cthd in db.ctHopDongs on ct.MaHDCT equals cthd.ID
                          join mb in db.mbMatBangs on ct.MaMB equals mb.MaMB
                          join tl in db.mbTangLaus on mb.MaTL equals tl.MaTL
                          where hd.MaTN == this.MaTN & hd.MaLDV == objLDV.MaLDV & hd.MaKH == this.MaKH & hd.NgayTT.Value.Month == this.Thang & hd.NgayTT.Value.Year == this.Nam
                          select new
                          {
                              ltt.DienGiai,
                              SoLuong = ct.DienTich,
                              HeSo = hd.KyTT,
                              DonGia = ct.PhiSuaChua * cthd.TyGia,
                              ThanhTien = ct.PhiSuaChua * ct.DienTich * cthd.TyGia * hd.KyTT
                          }).ToList();

            if (ltData.Count == 1)
            {
                objLDV.SoLuong = ltData[0].SoLuong;
                objLDV.TenDVT = "m2";
                objLDV.HeSo = ltData[0].HeSo;
                objLDV.DonGia = ltData[0].DonGia;
            }
            else if (ltData.Count > 1)
            {
                foreach (var i in ltData)
                {
                    var objGB = new DichVuItem();
                    objGB.TenLDV = i.DienGiai;
                    objGB.SoLuong = i.SoLuong;
                    objGB.TenDVT = "m2";
                    objGB.HeSo = i.HeSo;
                    objGB.DonGia = i.DonGia;
                    objGB.ThanhTien = i.ThanhTien;
                    objLDV.Details.Add(objGB);
                }
            }
        }

        void Load_TienDatCocChoThue(DichVuItem objLDV)
        {
            var ltData = (from hd in db.dvHoaDons
                          where hd.MaTN == this.MaTN & hd.MaLDV == objLDV.MaLDV & hd.MaKH == this.MaKH & hd.NgayTT.Value.Month == this.Thang & hd.NgayTT.Value.Year == this.Nam
                          select new
                          {
                              hd.DienGiai,
                              hd.PhaiThu
                          }).ToList();

            if (ltData.Count > 1)
            {
                foreach (var i in ltData)
                {
                    var objGB = new DichVuItem();
                    objGB.TenLDV = i.DienGiai;
                    objGB.ThanhTien = i.PhaiThu;
                    objLDV.Details.Add(objGB);
                }
            }
        }

        void Load_Dien3Pha(DichVuItem objLDV)
        {
            var ltDien = (from td in db.dvDien3Phas
                          where td.MaTN == this.MaTN & td.MaKH == this.MaKH & td.DenNgay.Value.Month == this.ThangDV & td.DenNgay.Value.Year == this.NamDV & td.ThanhTien > 0
                          select new
                          {
                              td.ID,
                              td.DienGiai,
                              td.SoTieuThu,
                              td.ThanhTien,
                              td.TienVAT,
                              td.TyLeVAT
                          }).ToList();

            foreach (var d in ltDien)
            {
                DichVuItem objGB = null;

                if (ltDien.Count == 1)
                {
                    objLDV.SoLuong = d.SoTieuThu;
                    objLDV.TenDVT = "kw";
                }
                else
                {
                    objGB = new DichVuItem();
                    objGB.TenDVT = " * " + d.DienGiai;
                    objGB.SoLuong = d.SoTieuThu;
                    objGB.TenDVT = "kw";
                    objGB.ThanhTien = d.ThanhTien;
                    objLDV.Details.Add(objGB);
                }

                var ltData = (from ct in db.dvDien3PhaChiTiets
                              join dm in db.dvDien3PhaDinhMucs on ct.MaDM equals dm.ID
                              where ct.MaDien == d.ID
                              orderby dm.STT
                              select new
                              {
                                  dm.TenDM,
                                  ct.ChiSoCu,
                                  ct.ChiSoMoi,
                                  ct.SoLuong,
                                  ct.DonGia,
                                  ct.ThanhTien
                              }).ToList();

                foreach (var i in ltData)
                {
                    objGB = new DichVuItem();
                    objGB.TenLDV =  "   + " + i.TenDM;
                    objGB.SoLuong = i.SoLuong;
                    objGB.TenDVT = "kw";
                    objGB.DonGia = i.DonGia;
                    objGB.ThanhTien = i.ThanhTien;
                    objLDV.Details.Add(objGB);
                }

                if (d.TienVAT > 0)
                {
                    objGB = new DichVuItem();
                    objGB.TenLDV = string.Format("  + Thuế VAT {0:p0}", d.TyLeVAT);
                    objGB.ThanhTien = d.TienVAT;
                    objLDV.Details.Add(objGB);
                }
            }
        }

        void Load_NuocNong(DichVuItem objLDV)
        {
            var ltNuoc = (from n in db.dvNuocNongs
                          where n.MaTN == this.MaTN & n.MaKH == this.MaKH & n.DenNgay.Value.Month == this.ThangDV & n.DenNgay.Value.Year == this.NamDV & n.ThanhTien > 0
                           select new
                           {
                               n.ID,
                               n.DienGiai,
                               ChiSoMoi = n.DauCap,
                               ChiSoCu = n.DauHoi,
                               SoTieuThu = n.DauCap - n.DauHoi,
                               n.HeSo,
                               n.ThanhTien,
                               n.TyLeBVMT,
                               n.TienBVMT,
                               n.TyLeVAT,
                               n.TienVAT
                           }).ToList();

            foreach (var n in ltNuoc)
            {
                DichVuItem objGB = null;

                if (ltNuoc.Count == 1)
                {
                    objLDV.ChiSoCu = n.ChiSoCu;
                    objLDV.ChiSoMoi = n.ChiSoMoi;
                    objLDV.SoLuong = n.SoTieuThu;
                    objLDV.TenDVT = "m3";
                    objLDV.HeSo = n.HeSo;
                }
                else
                {
                    objGB = new DichVuItem();
                    objGB.TenDVT = " * " + n.DienGiai;
                    objGB.ChiSoCu = n.ChiSoCu;
                    objGB.ChiSoMoi = n.ChiSoMoi;
                    objGB.SoLuong = n.SoTieuThu;
                    objGB.TenDVT = "m3";
                    objGB.HeSo = n.HeSo;
                    objGB.ThanhTien = n.ThanhTien;
                    objLDV.Details.Add(objGB);
                }

                var ltData = (from ct in db.dvNuocNongChiTiets
                              join dm in db.dvNuocNongDinhMucs on ct.MaDM equals dm.ID
                              where ct.MaNuoc == n.ID
                              orderby dm.STT
                              select new
                              {
                                  dm.TenDM,
                                  ct.SoLuong,
                                  ct.DonGia,
                                  ct.ThanhTien
                              }).ToList();
                foreach (var i in ltData)
                {
                    if (ltData.Count == 1 & n.TienVAT <= 0 & n.TienBVMT <= 0)
                    {
                        if (objGB != null)
                            objGB.DonGia = i.DonGia;
                        else
                            objLDV.DonGia = i.DonGia;
                    }
                    else
                    {
                        objGB = new DichVuItem();
                        objGB.TenLDV = "  + " + i.TenDM;
                        objGB.SoLuong = i.SoLuong;
                        objGB.TenDVT = "m3";
                        objGB.DonGia = i.DonGia;
                        objGB.ThanhTien = i.ThanhTien;
                        objLDV.Details.Add(objGB);
                    }
                }

                if (n.TienVAT > 0)
                {
                    objGB = new DichVuItem();
                    objGB.TenLDV = string.Format("  + Thuế VAT {0:p0}", n.TyLeVAT);
                    objGB.ThanhTien = n.TienVAT;
                    objLDV.Details.Add(objGB);
                }

                if (n.TienBVMT > 0)
                {
                    objGB = new DichVuItem();
                    objGB.TenLDV = string.Format("  + Phí bảo vệ môi trường {0:p0}", n.TyLeBVMT);
                    objGB.ThanhTien = n.TienBVMT;
                    objLDV.Details.Add(objGB);
                }
            }
        }

        void Load_NuocSinhHoat(DichVuItem objLDV)
        {
            var ltNuoc = (from n in db.dvNuocSinhHoats
                          where n.MaTN == this.MaTN & n.MaKH == this.MaKH & n.DenNgay.Value.Month == this.ThangDV & n.DenNgay.Value.Year == this.NamDV & n.ThanhTien > 0
                           select new
                           {
                               n.ID,
                               n.DienGiai,
                               ChiSoMoi = n.SoTieuThuNL,
                               ChiSoCu = n.SoTieuThuNN,
                               n.SoTieuThu,
                               n.ThanhTien,
                               n.TyLeBVMT,
                               n.TienBVMT,
                               n.TyLeVAT,
                               n.TienVAT
                           }).ToList();
            foreach (var n in ltNuoc)
            {
                DichVuItem objGB = null;

                if (ltNuoc.Count == 1)
                {
                    objLDV.ChiSoCu = n.ChiSoCu;
                    objLDV.ChiSoMoi = n.ChiSoMoi;
                    objLDV.SoLuong = n.SoTieuThu;
                    objLDV.TenDVT = "m3";
                }
                else
                {
                    objGB = new DichVuItem();
                    objGB.TenDVT = " * " + n.DienGiai;
                    objGB.ChiSoCu = n.ChiSoCu;
                    objGB.ChiSoMoi = n.ChiSoMoi;
                    objGB.SoLuong = n.SoTieuThu;
                    objGB.TenDVT = "m3";
                    objGB.ThanhTien = n.ThanhTien;
                    objLDV.Details.Add(objGB);
                }

                var ltData = (from ct in db.dvNuocSinhHoatChiTiets
                              join dm in db.dvNuocDinhMucs on ct.MaDM equals dm.ID
                              where ct.MaNuoc == n.ID
                              orderby dm.STT
                              select new
                              {
                                  dm.TenDM,
                                  ct.SoLuong,
                                  ct.DonGia,
                                  ct.ThanhTien
                              }).ToList();
                foreach (var i in ltData)
                {
                    if (ltData.Count == 1 & n.TienVAT <= 0 & n.TienBVMT <= 0)
                    {
                        if (objGB != null)
                            objGB.DonGia = i.DonGia;
                        else
                            objLDV.DonGia = i.DonGia;
                    }
                    else
                    {
                        objGB = new DichVuItem();
                        objGB.TenLDV = "  + " + i.TenDM;
                        objGB.SoLuong = i.SoLuong;
                        objGB.TenDVT = "m3";
                        objGB.DonGia = i.DonGia;
                        objGB.ThanhTien = i.ThanhTien;
                        objLDV.Details.Add(objGB);
                    }
                }

                if (n.TienVAT > 0)
                {
                    objGB = new DichVuItem();
                    objGB.TenLDV = string.Format("  + Thuế VAT {0:p0}", n.TyLeVAT);
                    objGB.ThanhTien = n.TienVAT;
                    objLDV.Details.Add(objGB);
                }

                if (n.TienBVMT > 0)
                {
                    objGB = new DichVuItem();
                    objGB.TenLDV = string.Format("  + Phí bảo vệ môi trường {0:p0}", n.TyLeBVMT);
                    objGB.ThanhTien = n.TienBVMT;
                    objLDV.Details.Add(objGB);
                }
            }
        }

        void Load_DieuHoa(DichVuItem objLDV)
        {
            var ltData = (from dh in db.dvDienDieuHoas
                          where dh.MaTN == this.MaTN & dh.MaKH == this.MaKH & dh.NgayCT.Value.Month == this.ThangDV & dh.NgayCT.Value.Year == this.NamDV & dh.ThanhTienQD > 0
                          select new
                          {
                              dh.DienGiai,
                              HeSo = dh.SoFCU,
                              SoLuong = dh.SoGio,
                              DonGia = dh.DonGia * dh.TyGia,
                              ThanhTien = dh.ThanhTienQD,
                          }).ToList();

            if (ltData.Count > 1)
            {
                foreach (var i in ltData)
                {
                    var objGB = new DichVuItem();
                    objGB.TenLDV = i.DienGiai;
                    objGB.SoLuong = i.SoLuong;
                    objGB.TenDVT = "giờ";
                    objGB.HeSo = i.HeSo;
                    objGB.DonGia = i.DonGia;
                    objGB.ThanhTien = i.ThanhTien;
                    objLDV.Details.Add(objGB);
                }
            }
        }

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            try
            {
                var objLDV = this.GetCurrentRow() as GiayBaoItem;
                DetailReport.DataSource = objLDV.DichVus;
            }
            catch { }
        }

        private void Detail1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            try
            {
                if (DetailReport.GetCurrentRow() is DichVuItem)
                {
                    var objLDV = DetailReport.GetCurrentRow() as DichVuItem;
                    if (objLDV.Details.Count > 0)
                    {
                        DetailReport1.DataSource = objLDV.Details;
                        DetailReport1.Visible = true;
                    }
                    else
                    {
                        DetailReport1.Visible = false;
                    }
                }
            }
            catch { }
        }
    }

    class GiayBaoItem
    {
        public DateTime? NgayIn { get; set; }
        public int Thang { get; set; }
        public int Nam { get; set; }
        public string ThangTA { get; set; }
        public string ThangTB { get; set; }
        public string TenCongTy { get; set; }
        public string TenToaNha { get; set; }
        public string TenTN { get; set; }
        public string TenCty { get; set; }
        public string DiaChiCty { get; set; }
        public string LoGoUrl { get; set; }
        public string TangLau { get; set; }
        public string Toa{ get; set; }
        public string MaSoMB { get; set; }
        public string TenTL { get; set; }
        public string TenKN { get; set; }
        public string MaSoKH { get; set; }
        public string MaPhuKH { get; set; }
        public string TenKH { get; set; }
        public string DiaChiKH { get; set; }

        public string SoTKNH { get; set; }
        public string ChuTKNH { get; set; }
        public string TenNH { get; set; }

        public decimal? PhatSinh { get; set; }
        public decimal? NoDauKy { get; set; }
        public decimal? TienLai { get; set; }
        public decimal? NoVaLai { get; set; }
        public decimal? ThuTruoc { get; set; }
        public decimal? TongTien { get; set; }
        public string TongTienBangChu { get; set; }

        public List<DichVuItem> DichVus { get; set; }
    }

    class DichVuItem
    {
        public string STT { get; set; }
        public int MaLDV { get; set; }
        public string TenLDV { get; set; }
        public decimal? ChiSoCu { get; set; }
        public decimal? ChiSoMoi { get; set; }
        public decimal? SoLuong { get; set; }
        public string TenDVT { get; set; }
        public decimal? HeSo { get; set; }
        public decimal? DonGia { get; set; }
        public decimal? ThanhTien { get; set; }
        public List<DichVuItem> Details { get; set; }
    }

    class GiuXeItem
    {
        public int? MaLX { get; set; }
        public string TenLX { get; set; }
        public string BienSo { get; set; }
        public int? SoLuong { get; set; }
        public decimal? DonGia { get; set; }
        public decimal? ThanhTien { get; set; }
    }
}
