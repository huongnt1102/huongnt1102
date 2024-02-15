using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
using Library;
using System.Data.Linq.SqlClient;
using DevExpress.XtraRichEdit.API.Native;

namespace LandSoftBuilding.Receivables.GiayBao
{
    public partial class rptPQLImperia : DevExpress.XtraReports.UI.XtraReport
    {
        public rptPQLImperia(byte _MaTN, int _MaKH, int _Thang, int _Nam, int _MaLDV)
        {
            InitializeComponent();

         //   Library.frmPrintControl.LoadLayout(this, 5, _MaTN);

            cSoLuong.DataBindings.Add("Text", null, "SoLuong", "{0:#,0.##}");
            cDonGia.DataBindings.Add("Text", null, "DonGia", "{0:#,0.##}");
            cTuNgay.DataBindings.Add("Text", null, "TuNgay","{0:dd/MM/yyyy}");
            cDenNgay.DataBindings.Add("Text", null, "DenNgay", "{0:dd/MM/yyyy}");
            cThanhTien.DataBindings.Add("Text", null, "ThanhTien", "{0:#,0.##}");

          
            
            var db = new MasterDataContext();
            try
            {
                var List = (from hd in db.dvHoaDons
                    //join dv in db.dvDichVuKhacs on new { hd.TableName, hd.LinkID } equals new { TableName = "dvDichVuKhac", LinkID = (int?)dv.ID }
                    join dv in db.dvDichVuKhacs on new {hd.MaLDV, hd.LinkID} equals
                    new {MaLDV = (int?) 13, LinkID = (int?) dv.ID}
                    join dvt in db.DonViTinhs on dv.MaDVT equals dvt.ID
                    where
                    hd.MaTN == _MaTN & hd.MaLDV == _MaLDV & hd.MaKH == _MaKH & hd.NgayTT.Value.Month == _Thang &
                    hd.NgayTT.Value.Year == _Nam & (hd.PhaiThu.GetValueOrDefault() 
                    - (db.ptChiTietPhieuThus.Where(p => p.LinkID == hd.ID & p.TableName == "dvHoaDon").Sum(p => p.SoTien)).GetValueOrDefault()
                    - (db.ktttChiTiets.Where(p => p.LinkID == hd.ID & p.TableName == "dvHoaDon").Sum(p => p.SoTien)).GetValueOrDefault()
                    ) > 0
                            
                    select new
                    {
                        dv.SoLuong,
                        dvt.TenDVT,
                        hd.TuNgay,
                        hd.DenNgay,
                        DonGia = string.Format("{0:#,0.##}/m2/tháng \n {0:#,0.##}/m2/month", dv.DonGia * dv.TyGia),
                        ThanhTien = dv.ThanhTien*dv.KyTT,
                        hd.PhaiThu,
                        dv.TienThueGTGT,
                        hd.KyTT //? () : string.Format("Tháng {0}/{1} \n Quarter {2}/{1}"),
                    }).ToList();
                if (List.Count() == 0) return;
                //xrRichText1
               
                this.DataSource = List;
                string ThangMoi = "";
                if (List.First().KyTT.GetValueOrDefault() >= 3)
                {
                    if (_Thang >= 1 & _Thang <= 3)
                    {
                        ThangMoi = "1";
                    }
                    if (_Thang >= 4 & _Thang <= 6)
                    {
                        ThangMoi = "2";
                    }
                    if (_Thang >= 7 & _Thang <= 9)
                    {
                        ThangMoi = "3";
                    }
                    if (_Thang >= 10 & _Thang <= 12)
                    {
                        ThangMoi = "4";
                    }
                    xrTableCell2.Text = string.Format("Quý {0}/{1} \n Quarter {0}/{1}", ThangMoi, _Nam);
                }
                else
                {
                    xrTableCell2.Text = string.Format("Tháng {0}/{1} \n Month {0}/{1}", _Thang, _Nam);
                }
                var NgayTB = new DateTime((int) _Nam, (int) _Thang, 1);
                var _NoCu = (from hd in db.dvHoaDons
                    //join ptct in db.ptChiTietPhieuThus on hd.ID equals ptct.LinkID
                    //join pt in db.ptPhieuThus on ptct.MaPT equals pt.ID
                    where hd.MaKH == _MaKH
                          & SqlMethods.DateDiffDay(hd.NgayTT, NgayTB) > 0
                          //  & SqlMethods.DateDiffDay(pt.NgayThu, NgayTB) > 0
                          & hd.IsThuThua.GetValueOrDefault() == false
                          & (hd.PhaiThu.GetValueOrDefault()

                             - (from ct in db.ptChiTietPhieuThus
                                 join pt in db.ptPhieuThus on ct.MaPT equals pt.ID
                                 where ct.TableName == "dvHoaDon" & ct.LinkID == hd.ID
                                       //& SqlMethods.DateDiffDay(pt.NgayThu, _TuNgay) > 0 -- Sua lai dau ky toi ngay hien tai
                                       & SqlMethods.DateDiffDay(pt.NgayThu, NgayTB) > 0
                                 select ct.SoTien).Sum().GetValueOrDefault()) != 0
                          // & (_MaLDVs.Contains(hd.MaLDV.Value) | _MaLDVs.Count == 0) & hd.IsDuyet == true
                          & hd.MaLDV == 13
                    select (hd.PhaiThu
                            - (from ct in db.ptChiTietPhieuThus
                                join pt in db.ptPhieuThus on ct.MaPT equals pt.ID
                                where ct.TableName == "dvHoaDon" & ct.LinkID == hd.ID
                                      //& SqlMethods.DateDiffDay(pt.NgayThu, _TuNgay) > 0 -- Sua lai dau ky toi ngay hien tai
                                      & SqlMethods.DateDiffDay(pt.NgayThu, NgayTB) > 0
                                select ct.SoTien).Sum().GetValueOrDefault()

                        //- (db.ptChiTietPhieuThus.Where(p => p.LinkID == hd.ID & p.TableName == "dvHoaDon").Sum(p => p.SoTien)).GetValueOrDefault()
                    )

                ).Sum().GetValueOrDefault();
                cVAT.Text = string.Format("{0:#,0.##}"
            ,
            List.First().TienThueGTGT);
                cPS.Text = string.Format("{0:#,0.##}"
            ,
            List.First().PhaiThu);
                cNo.Text = string.Format("{0:#,0.##}"
            ,
            _NoCu);
                csumThanhTien.Text = string.Format("{0:#,0.##}"
            ,
            List.First().PhaiThu.GetValueOrDefault() + _NoCu);

            }
            catch { }
            finally
            {
                db.Dispose();
            }
        }
    }
}
