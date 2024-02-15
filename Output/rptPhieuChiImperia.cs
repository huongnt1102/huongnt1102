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

namespace LandSoftBuilding.Fund.Output
{
    public partial class rptPhieuChiImperia : DevExpress.XtraReports.UI.XtraReport
    {
        public int _ID { get; set; }
        public rptPhieuChiImperia(int MaPT, int MaTN)
        {
            InitializeComponent();
            _ID = MaPT;
            var objTien = new TienTeCls();
            using (var db = new Library.MasterDataContext())
            {
                try
                {
                    var obj = (from p in db.pcPhieuChis
                               where p.ID == MaPT
                               select new
                               {
                                   p.MaTN,
                                   MaKH=p.MaNCC,
                                   SoPT=p.SoPC,
                                   NgayThu=p.NgayChi,
                                   NguoiNop=p.NguoiNhan,
                                   p.DiaChiNN,
                                   p.LyDo,
                                   p.SoTien,
                                   p.LoaiChi,
                                   SoTien_BangChu = objTien.DocTienBangChu(p.SoTien.Value, "đồng chẵn")
                               }).ToList();
                    if (obj.Count() == 0) return;
                    
                      
                        this.DataSource = (from p in db.pcPhieuChis
                                           join ct in db.pcChiTiets on p.ID equals ct.MaPC
                                           where p.ID == MaPT
                                           select new
                                           {
                                               p.MaTN,
                                               MaKH =p.MaNCC,
                                               SoPT=p.SoPC,
                                               NgayThu=p.NgayChi,
                                               NguoiNop=p.NguoiNhan,
                                               p.DiaChiNN,
                                               TenLDV = ct.DienGiai,
                                               ct.SoTien,
                                               //TenTA = "",
                                               //KyTT = string.Format("T{0}/{1}", p.NgayChi.Value.Month, p.NgayThu.Value.Year),
                                               //p.MaPL,
                                               SoTien_BangChu = objTien.DocTienBangChu(p.SoTien.Value, "đồng chẵn")
                                           }).ToList();
                       // xrLabel6.Text = string.Format("Số:   {0}",obj.First().SoPT);
                        cNoiDung.DataBindings.Add("Text", null, "TenLDV");
                        //cTA.DataBindings.Add("Text", null, "TenTA");
                       // cKyTT.DataBindings.Add("Text", null, "KyTT");
                        cSoTien.DataBindings.Add("Text", null, "SoTien", "{0:#,0.##}");
                        cTotal.DataBindings.Add(new XRBinding("Text", null, "SoTien", "{0:#,0.##}"));
                        cTotal.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,0.##}");
                        var objKH = (from mb in db.mbMatBangs
                                     join kh in db.tnKhachHangs on mb.MaKHF1 equals kh.MaKH into khach
                                     from kh in khach.DefaultIfEmpty()
                                     where kh.MaKH == obj.First().MaKH

                                     select new
                                     {
                                         TenKH = kh.IsCaNhan == true ? (kh.HoKH + " " + kh.TenKH) : kh.CtyTen,
                                         mb.MaSoMB,
                                         //Sửa số tiền thu trước

                                     }).First();
                        var ctlRTF = new DevExpress.XtraRichEdit.RichEditControl();
                        ctlRTF.RtfText = xrRichText1.Rtf;
                        ctlRTF.Document.ReplaceAll("[TenKH]", objKH.TenKH ?? "", SearchOptions.None);
                        ctlRTF.Document.ReplaceAll("[MaSoMB]", objKH.MaSoMB ?? "", SearchOptions.None);
                        xrRichText1.Rtf = ctlRTF.RtfText;
                        cNgayIn.Text = string.Format(cNgayIn.Text, obj.First().NgayThu.Value.Day, obj.First().NgayThu.Value.Month, obj.First().NgayThu.Value.Year);
                        cThoiGianIn.Text = string.Format("{0: hh:mm:ss tt}", obj.First().NgayThu.Value);

                        var Ngay = (DateTime)obj.First().NgayThu;
                        var tam = Ngay.DayOfWeek.ToString();
                        xrLabel2.Text = string.Format("{0}, {1} {2}, {3}", tam, Commoncls.GetMonth(obj.First().NgayThu.Value.Month), obj.First().NgayThu.Value.Month, obj.First().NgayThu.Value.Year);
                        cBangChu.Text = "Viết bằng chữ :" + objTien.DocTienBangChu(obj.First().SoTien.Value, "đồng chẵn");
                     
                        var objTN = db.tnToaNhas.FirstOrDefault(p => p.MaTN == MaTN);
                        //cTenTN.Text = cTenTN2.Text = objTN.CongTyQuanLy;
                        //cDiaChiTN.Text = cDiaChiTN2.Text = objTN.DiaChiCongTy;
                        //cDienThoaiTN.Text = cDienThoaiTN2.Text = "Tel: " + objTN.DienThoai;
                        xrPictureBox1.ImageUrl = objTN.Logo;
                   
                       
                    
                }
                catch { }
            }
        }
    
    }
}
