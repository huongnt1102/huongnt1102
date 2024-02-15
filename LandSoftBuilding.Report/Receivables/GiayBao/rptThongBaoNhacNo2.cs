using System;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
using Library;
using System.Collections.Generic;
using System.Data.Linq.SqlClient;
using DevExpress.XtraRichEdit;
using DevExpress.XtraRichEdit.API.Native;
using DevExpress.XtraReports.Parameters;

namespace LandSoftBuilding.Receivables.GiayBao
{
    public partial class rptThongBaoNhacNo2 : DevExpress.XtraReports.UI.XtraReport
    {
        public rptThongBaoNhacNo2(byte _MaTN, int _Thang, int _Nam, int _MaKH,int _MaMB, List<int> _MaLDVs, int _MaTKNH)
        {
            InitializeComponent();
            Library.frmPrintControl.LoadLayout(this, 90, _MaTN);
            MasterDataContext db = new MasterDataContext();
            var ctlRTF = new DevExpress.XtraRichEdit.RichEditControl();
            var objTN = (from tn in db.tnToaNhas
                         where tn.MaTN == _MaTN
                         select new { tn.TenTN, tn.CongTyQuanLy, tn.Logo })
                            .FirstOrDefault();
           
            //Thong tin khach hang
            var objKH = (from mb in db.mbMatBangs
                         join kh in db.tnKhachHangs on mb.MaKH equals kh.MaKH
                         where mb.MaKH == _MaKH && mb.MaMB==_MaMB
                         select new
                         {
                             TenKH = kh.IsCaNhan == true ? (kh.HoKH + " " + kh.TenKH) : kh.CtyTen,
                             mb.MaSoMB,
                             kh.ThuTruoc,
                             kh.MaPhu
                         }).FirstOrDefault();
            if (objKH == null) return;
            var objTK = (from tk in db.nhTaiKhoans
                         join nh in db.nhNganHangs on tk.MaNH equals nh.ID
                         where tk.ID == _MaTKNH
                         select new { tk.ChuTK, tk.SoTK, nh.TenNH,ChiNhanhNH=tk.DienGiai }).Single();
            var _Ngay = new DateTime(_Nam, _Thang, 1);
            var ltLoaiDichVu = (from hd in db.dvHoaDons
                                join ldv in db.dvLoaiDichVus on hd.MaLDV equals ldv.ID
                                where  hd.IsDuyet == true & hd.MaKH == _MaKH & hd.NgayTT.Value.Month == _Thang
                                & hd.NgayTT.Value.Year == _Nam
                                & (_MaLDVs.Contains(hd.MaLDV.Value) | _MaLDVs.Count == 0)
                                & hd.PhaiThu > 0
                                & (hd.PhaiThu.GetValueOrDefault()
                                - (db.SoQuy_ThuChis.Where(p => p.LinkID == hd.ID & p.TableName == "dvHoaDon").Sum(p => p.DaThu+p.KhauTru)).GetValueOrDefault()
                                )


                                > 0  //moi mo 5/11/2016
                                group hd by new { ldv.STT, hd.MaLDV, ldv.TenHienThi, ldv.TenTA } into gr
                                orderby gr.Key.STT
                                select new
                                {
                                    gr.Key.STT,
                                    gr.Key.MaLDV,
                                    TenLDV = gr.Key.TenHienThi,
                                    gr.Key.TenTA,
                                    SoTien = gr.Key.MaLDV == 49 ? -gr.Sum(p => p.PhaiThu) : gr.Sum(p => p.PhaiThu),
                                }).ToList();
            var _NoCu = (from hd in db.dvHoaDons
                         where hd.MaKH == _MaKH
                         & hd.IsThuThua.GetValueOrDefault() == false
                         & (hd.PhaiThu.GetValueOrDefault()
                         - (db.SoQuy_ThuChis.Where(p => p.LinkID == hd.ID & p.TableName == "dvHoaDon").Sum(p => p.KhauTru+p.DaThu)).GetValueOrDefault()
                         ) != 0
                         & SqlMethods.DateDiffDay(hd.NgayTT, _Ngay) > 0
                             & (_MaLDVs.Contains(hd.MaLDV.Value) | _MaLDVs.Count == 0) & hd.IsDuyet == true

                         select (hd.PhaiThu - (db.ptChiTietPhieuThus.Where(p => p.LinkID == hd.ID & p.TableName == "dvHoaDon").Sum(p => p.SoTien)).GetValueOrDefault())).Sum().GetValueOrDefault();
            if (_NoCu < 0)
            {
                _NoCu = 0;
            }


            var _PhatSinh = ltLoaiDichVu.Sum(p => p.SoTien).GetValueOrDefault();
            var _TongTien = Convert.ToInt64(Math.Round(_PhatSinh + _NoCu, 0));
            xrLabel2.Text = string.Format(xrLabel2.Text, _Thang, _Nam);
            xrLabel3.Text = string.Format(xrLabel3.Text, _Thang, _Nam);
            ctlRTF.RtfText = rtHeader.Rtf;
           
            ctlRTF.Document.ReplaceAll("[Thang]", _Thang.ToString(), SearchOptions.None);
            ctlRTF.Document.ReplaceAll("[MaPhu]", objKH.MaPhu, SearchOptions.None);
            ctlRTF.Document.ReplaceAll("[SoTien]", string.Format("{0:#,0.##}", _TongTien), SearchOptions.None);
            ctlRTF.Document.ReplaceAll("[Nam]", _Nam.ToString(), SearchOptions.None);
            ctlRTF.Document.ReplaceAll("[TenKH]", objKH.TenKH ?? "", SearchOptions.None);
            ctlRTF.Document.ReplaceAll("[MaSoMB]", objKH.MaSoMB ?? "", SearchOptions.None);
            ctlRTF.Document.ReplaceAll("[TenToaNha]", objTN.TenTN ?? "", SearchOptions.None);
            ctlRTF.Document.ReplaceAll("[TenCongTy]", objTN.CongTyQuanLy ?? "", SearchOptions.None);
            ctlRTF.Document.ReplaceAll("[ChuTK]", objTK.ChuTK ?? "", SearchOptions.None);
            ctlRTF.Document.ReplaceAll("[SoTK]", objTK.SoTK ?? "", SearchOptions.None);
            ctlRTF.Document.ReplaceAll("[TenNH]", objTK.TenNH ?? "", SearchOptions.None);
            ctlRTF.Document.ReplaceAll("[ChiNhanhNH]", objTK.ChiNhanhNH ?? "", SearchOptions.None);
            ctlRTF.Document.ReplaceAll("[ThangTA]", Commoncls.GetMonth(_Thang) ?? "", SearchOptions.None);
            rtHeader.Rtf = ctlRTF.RtfText;
        }

    }
}
