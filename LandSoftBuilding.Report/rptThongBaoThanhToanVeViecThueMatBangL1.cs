using System;
using System.CodeDom;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using DevExpress.XtraReports.UI;
using Library;
using DevExpress.XtraRichEdit;
using DevExpress.XtraRichEdit.API.Native;
using DevExpress.XtraReports.Parameters;
namespace LandSoftBuilding.Report
{
    public partial class rptThongBaoThanhToanVeViecThueMatBangL1 : DevExpress.XtraReports.UI.XtraReport
    {
        public rptThongBaoThanhToanVeViecThueMatBangL1(int? MaKh, byte? MaTN, int Thang, int Nam, int _MaTKNH)
        {
            MasterDataContext db = new MasterDataContext();
            InitializeComponent();
            try
            {



                //try 
                 Library.frmPrintControl.LoadLayout(this, 74, (byte)MaTN); 
                //catch { }
                //finally
                //{
                //    db.Dispose();
                //}

                cDienGiai.DataBindings.Add("Text", null, "DienGiai");
                //cThoiHanThanhToan.DataBindings.Add("Text", null, "ThoiHan", "{0:#,0.##} ngày");
                cSoTien.DataBindings.Add("Text", null, "ThanhTienChuaThue", "{0:#,0.##}");
                cSTT.DataBindings.Add("Text", null, "STT");
                cKyTT.DataBindings.Add("Text", null, "SoThang", "{0:#,0.##}");
                cDG.DataBindings.Add("Text", null, "DG", "{0:#,0.##}");
                cSum.DataBindings.Add("Text", null, "SoTienQD", "{0:#,0.##}");
                //cSum.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,0.##}");
                cVAT.DataBindings.Add("Text", null, "TienVAT", "{0:#,0.##}");
                cVAT.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,0.##}");
                var objKH = db.tnKhachHangs.SingleOrDefault(p => p.MaKH == MaKh);
                cK.Text = string.Format("Kính gửi : {0}",
                    (bool) objKH.IsCaNhan ? (objKH.HoKH + " " + objKH.TenKH) : objKH.CtyTen);

                var objTyGia = db.LoaiTiens.Single(p => p.ID == 2).TyGia;
                cTienQuyDoiHienTai.Text =
                    string.Format(
                        "Tỷ giá bán ra của đồng đô la mỹ của Ngân hàng TMCP Ngoại Thương ngày {0:dd/MM/yyyy} là {1:#,0.##} VNĐ/USD",
                        DateTime.Now, objTyGia);
                var objHD = (from hd in db.dvHoaDons
                    join ltt in db.ctLichThanhToans on hd.LinkID equals ltt.ID
                    join hopdong in db.ctHopDongs on ltt.MaHD equals hopdong.ID
                    join kh in db.tnKhachHangs on hopdong.MaKH equals kh.MaKH
                    where hd.MaTN == MaTN & kh.MaKH == MaKh
                          & hd.NgayTT.Value.Month == Thang & hd.NgayTT.Value.Year == Nam
                        & hd.PhaiThu - ((from ct in db.ptChiTietPhieuThus
                                         join pt in db.ptPhieuThus on ct.MaPT equals pt.ID
                                         where
                                             ct.TableName == "dvHoaDon" & ct.LinkID == hd.ID //&
                                         //SqlMethods.DateDiffDay(pt.NgayThu, denNgay) >= 0
                                         select ct.SoTien).Sum().GetValueOrDefault()) > 0
                    select new
                    {
                        hopdong.SoHDCT,
                        hopdong.NgayKy,
                        hopdong.HanTT,
                        ltt.TuNgay,
                        ltt.DenNgay,
                        DK = db.ctChiTiets.FirstOrDefault(p => p.MaMB == ltt.MaMB & p.MaHDCT == ltt.MaHD).TienVAT > 0 ? true : false,
                      
                        TenKH = (bool) objKH.IsCaNhan ? (objKH.HoKH + " " + objKH.TenKH) : objKH.CtyTen
                    }).FirstOrDefault();
                cNgayIn.Text = string.Format("Hà Nội,ngày {0} tháng {1} năm {2}", DateTime.Now.Day, DateTime.Now.Month,
                    DateTime.Now.Year);
                if (objHD != null)
                {
                    var ctlRTF = new DevExpress.XtraRichEdit.RichEditControl();
                    ctlRTF.RtfText = rtHeader.Rtf;
                    ctlRTF.Document.ReplaceAll("[SoHD]", objHD.SoHDCT, SearchOptions.None);
                    ctlRTF.Document.ReplaceAll("[NgayHD]", string.Format("{0:dd/MM/yyyy}", objHD.NgayKy),
                        SearchOptions.None);
                    ctlRTF.Document.ReplaceAll("[TenKH]", objHD.TenKH.ToString(), SearchOptions.None);
                    rtHeader.Rtf = ctlRTF.RtfText;
                }

                var Obj =
                    (from hd in db.dvHoaDons
                        join ltt in db.ctLichThanhToans on hd.LinkID equals ltt.ID
                        join hopdong in db.ctHopDongs on ltt.MaHD equals hopdong.ID
                        join hdct in db.ctChiTiets on hopdong.ID equals hdct.MaHDCT
                        where
                            hd.MaKH == MaKh & hd.MaLDV == 2
                            &
                            hd.MaTN == MaTN
                            & hd.NgayTT.Value.Month == Thang & hd.NgayTT.Value.Year == Nam
                            & hd.ConNo > 0

                        select new
                        {
                            ltt.DienGiai,
                            hdct.DienTich,
                            SoTienQD = hd.PhaiThu,
                            hdct.TyLeVAT,
                            hopdong.ThoiHan,
                            //ThoiHan = 0,
                            ltt.SoThang
                        }).ToList().Distinct();
                if (Obj.Count() >= 2)
                {
                    //var tam = Obj.Take(2);
                   this.DataSource= Obj.AsEnumerable().Select((p, index) => new
                    {
                        p.DienGiai,
                        STT = index + 1,

                        SoTienQD = p.SoTienQD,
                        p.ThoiHan,
                        p.SoThang,
                        DG = objHD.DK == true ? ((p.SoTienQD / (decimal)1.1) / (decimal)p.SoThang) / p.DienTich : (p.SoTienQD / (decimal)p.SoThang) / p.DienTich,
                        //( p.SoTienQD * (1 - p.TyLeVAT) )/ (p.SoThang == null ? 1 : p.SoThang),


                        TienVAT = objHD.DK == true ?
                                p.SoTienQD - (p.SoTienQD / (decimal)1.1) : 0,
                        ThanhTienChuaThue = objHD.DK == true ? p.SoTienQD / (decimal)1.1 : p.SoTienQD,
                    }).ToList().Take(2); 
                }
                else
                {
                    this.DataSource = Obj.AsEnumerable().Select((p, index) => new
                        {
                            p.DienGiai,
                            STT = index + 1,

                            p.SoTienQD,
                            p.ThoiHan,
                            p.SoThang,
                            DG = objHD.DK == true ? ((p.SoTienQD / (decimal)1.1) / (decimal)p.SoThang) / p.DienTich : (p.SoTienQD  / (decimal)p.SoThang) / p.DienTich,
                            //( p.SoTienQD * (1 - p.TyLeVAT) )/ (p.SoThang == null ? 1 : p.SoThang),
                            TienVAT = objHD.DK == true ?
                              p.SoTienQD - (p.SoTienQD / (decimal)1.1) : 0,
                            ThanhTienChuaThue = objHD.DK == true ? p.SoTienQD / (decimal)1.1 : p.SoTienQD,
                        }).ToList(); //this.DataSource = Obj;
                }
                    
                var objTK = (from tk in db.nhTaiKhoans
                    join nh in db.nhNganHangs on tk.MaNH equals nh.ID
                    where tk.ID == _MaTKNH
                    select new {tk.ChuTK, tk.SoTK, nh.TenNH}).Single();


                if (objTK != null)
                {
                    var ctlRTF = new DevExpress.XtraRichEdit.RichEditControl();
                    ctlRTF.RtfText = xrRichText2.Rtf;
                    //ctlRTF.Document.ReplaceAll("[NgayIn]", _NgayIn.ToString("dd/MM/yyyy"), SearchOptions.None);
                    //ctlRTF.Document.ReplaceAll("[Thang]", _Thang.ToString(), SearchOptions.None);
                    //ctlRTF.Document.ReplaceAll("[Nam]", _Nam.ToString(), SearchOptions.None);
                    //ctlRTF.Document.ReplaceAll("[TenKH]", objKH.TenKH ?? "", SearchOptions.None);
                    //ctlRTF.Document.ReplaceAll("[MaSoMB]", objKH.MaSoMB ?? "", SearchOptions.None);
                    //ctlRTF.Document.ReplaceAll("[TenToaNha]", objTN.TenTN ?? "", SearchOptions.None);
                    //ctlRTF.Document.ReplaceAll("[TenCongTy]", objTN.CongTyQuanLy ?? "", SearchOptions.None);
                    ctlRTF.Document.ReplaceAll("[ChuTK]", objTK.ChuTK ?? "", SearchOptions.None);
                    ctlRTF.Document.ReplaceAll("[SoTK]", objTK.SoTK ?? "", SearchOptions.None);
                    ctlRTF.Document.ReplaceAll("[TenNganHang]", objTK.TenNH ?? "", SearchOptions.None);
                    if (objHD != null)
                    {
                        var a3 = (decimal) objHD.HanTT;
                        ctlRTF.Document.ReplaceAll("[SoNgay]", a3.ToString("#,0.##"), SearchOptions.None);
                        var
                            a1 = (DateTime) objHD.TuNgay;
                        var a2 = (DateTime) objHD.DenNgay;
                        ctlRTF.Document.ReplaceAll("[TuNgayThue]", a1.ToString("dd/MM/yyyy"), SearchOptions.None);
                        ctlRTF.Document.ReplaceAll("[DenNgayThue]", a2.ToString("dd/MM/yyyy"), SearchOptions.None);
                        xrRichText2.Rtf = ctlRTF.RtfText;
                    }

                }
                
                var objTien = new TienTeCls();
                cTienBangChu.Text = "Bằng chữ : " +
                                    objTien.DocTienBangChu(Obj.Sum(p => p.SoTienQD).GetValueOrDefault(), "đồng");
            }
            catch{}
        }
      

    }
}
