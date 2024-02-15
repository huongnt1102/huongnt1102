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

namespace LandSoftBuilding.Fund.Input
{
    public partial class rptPhieuThuImperiaOutside : DevExpress.XtraReports.UI.XtraReport
    {
        public int _ID { get; set; }
        public rptPhieuThuImperiaOutside(int MaPT, int MaTN)
        {
            InitializeComponent();
            _ID = MaPT;
            var objTien = new TienTeCls();
            using (var db = new Library.MasterDataContext())
            {
                try
                {
                    var obj = (from p in db.ptPhieuThus
                               where p.ID == MaPT
                               select new
                               {
                                   p.MaTN,p.MaKH,
                                   p.SoPT,
                                   p.NgayThu,
                                   p.NguoiNop,
                                   p.DiaChiNN,
                                   p.LyDo,
                                   p.SoTien,
                                   p.MaPL,
                                   SoTien_BangChu = objTien.DocTienBangChu(p.SoTien.Value, "đồng chẵn")
                               }).ToList();
                    if (obj.Count() == 0) return;
                    
                        var ltChiTiet = (from p in db.ptPhieuThus
                                         join ptct in db.ptChiTietPhieuThus on p.ID equals ptct.MaPT
                                         join hd in db.dvHoaDons on ptct.LinkID equals hd.ID
                                         join ldv in db.dvLoaiDichVus on hd.MaLDV equals ldv.ID
                                         where p.ID == MaPT
                                         group new { ptct } by new { ldv.ID, ldv.TenHienThi, ldv.TenTA } into gr
                                         select new
                                         {
                                             TenLDV = gr.Key.TenHienThi,
                                             TenTA = "/ " + gr.Key.TenTA,
                                             KyTT = GetDienGiaiThangLong(gr.Key.ID),
                                             SoTien = gr.Sum(p => p.ptct.SoTien).GetValueOrDefault()
                                         }).ToList();
                    if (ltChiTiet.Count() == 0)
                    {
                        this.DataSource = (from p in db.ptPhieuThus
                                           join ct in db.ptChiTietPhieuThus on p.ID equals ct.MaPT
                                           where p.ID == MaPT
                                           select new
                                           {
                                               p.MaTN,
                                               p.MaKH,
                                               p.SoPT,
                                               p.NgayThu,
                                               p.NguoiNop,
                                               p.DiaChiNN,
                                               TenLDV = ct.DienGiai,
                                               ct.SoTien,
                                               TenTA = "",
                                               KyTT = string.Format("T{0}/{1}", p.NgayThu.Value.Month, p.NgayThu.Value.Year),
                                               p.MaPL,
                                               SoTien_BangChu = objTien.DocTienBangChu(p.SoTien.Value, "đồng chẵn")
                                           }).ToList();
                        cNoiDung.DataBindings.Add("Text", null, "TenLDV");
                        cTA.DataBindings.Add("Text", null, "TenTA");
                        cKyTT.DataBindings.Add("Text", null, "KyTT");
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
                    else
                    {
                        this.DataSource = ltChiTiet;

                        #region Thong tin toa nha
                        var objTN = db.tnToaNhas.FirstOrDefault(p => p.MaTN == MaTN);
                        //cTenTN.Text = cTenTN2.Text = objTN.CongTyQuanLy;
                        //cDiaChiTN.Text = cDiaChiTN2.Text = objTN.DiaChiCongTy;
                        //cDienThoaiTN.Text = cDienThoaiTN2.Text = "Tel: " + objTN.DienThoai;
                        xrPictureBox1.ImageUrl =  objTN.Logo;
                        //c1.Text = c2.Text = Common.User.HoTenNV.ToString().ToUpper();

                        cNoiDung.DataBindings.Add("Text", null, "TenLDV");
                        cTA.DataBindings.Add("Text", null, "TenTA");
                        cKyTT.DataBindings.Add("Text", null, "KyTT");
                        cSoTien.DataBindings.Add("Text", null, "SoTien", "{0:#,0.##}");
                        cTotal.DataBindings.Add(new XRBinding("Text", null, "SoTien", "{0:#,0.##}"));
                        cTotal.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,0.##}");

                        var objKH = (from kh in db.tnKhachHangs
                                     join mb in db.mbMatBangs on kh.MaKH equals mb.MaKHF1
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

                        #endregion
                    
                    }
                       
                    
                }
                catch { }
            }
        }
        string GetDienGiaiThangLong(int MaLDV)
        {
            var db = new Library.MasterDataContext();
            string strDienGiai = "";
            try
            {

                var ltData = (from hd in db.dvHoaDons
                              join ptct in db.ptChiTietPhieuThus on hd.ID equals ptct.LinkID
                              join ldv in db.dvLoaiDichVus on hd.MaLDV equals ldv.ID
                              join tx in db.dvgxTheXes on hd.LinkID equals tx.ID into the
                              from tx in the.DefaultIfEmpty()
                              where ptct.MaPT == _ID & hd.MaLDV == MaLDV
                              group hd by new { hd.MaLDV, ldv.TenLDV } into gr
                              select new
                              {

                                 
                                  gr.Key.MaLDV,
                                  gr.Key.TenLDV,
                                  // gr.Key.NgayTT,
                              }).ToList();

                foreach (var i in ltData)
                {
                    var Test = (from hd in db.dvHoaDons
                                join ptct in db.ptChiTietPhieuThus on hd.ID equals ptct.LinkID
                                join ldv in db.dvLoaiDichVus on hd.MaLDV equals ldv.ID

                                where ptct.MaPT == _ID & ldv.ID == i.MaLDV

                                select new
                                {

                                    hd.LinkID,
                                    hd.TableName,
                                    hd.DienGiai,
                                    hd.ID,
                                }).FirstOrDefault();
                    if (Test != null)
                    {
                        if (Test.LinkID == null & Test.TableName != null)
                        {
                            strDienGiai += Test.DienGiai;
                        }
                        else
                        {
                            var ltLDVXe = (from l in ltData
                                           //join xe in db.dvgxTheXes on l.LinkID equals xe.ID into t
                                           //from xe in t.DefaultIfEmpty()
                                           where l.MaLDV == i.MaLDV 
                                           group l by new { l.MaLDV} into gr
                                           select new { gr.Key.MaLDV }).Distinct().ToList();
                            var ltDV =  (from hd in db.dvHoaDons
                                                                                                     join ptct in db.ptChiTietPhieuThus on hd.ID equals ptct.LinkID
                                                                                                     join ldv in db.dvLoaiDichVus on hd.MaLDV equals ldv.ID
                                                                                                     //join tx in db.dvgxTheXes on hd.LinkID equals tx.ID into the
                                                                                                     //from tx in the.DefaultIfEmpty()
                                                                                                     where ldv.ID == i.MaLDV & ptct.MaPT == _ID //& ptct.DienGiai.Contains(i.BienSo.ToString())
                                                                                                     group hd by new { hd.NgayTT.Value.Month, hd.NgayTT.Value.Year } into gr
                                                                                                     orderby gr.Max(p => p.NgayTT) descending
                                                                                                     select gr.Max(p => p.NgayTT)).ToList();
                            var j = 0;
                            var _Start = j;
                            var strTime = "";

                            while (j < ltDV.Count)
                            {
                                if ((j + 1) == ltDV.Count || System.Data.Linq.SqlClient.SqlMethods.DateDiffMonth(ltDV[j], ltDV[j + 1]) != 1)
                                {


                                    if (_Start != j)
                                    {
                                        if (i.MaLDV == 8 | i.MaLDV == 22 | i.MaLDV == 9)
                                        {
                                            if (ltDV[_Start].Value.Year != ltDV[j].Value.Year)
                                                strTime += string.Format("T{0:MM/yyyy}-T{1:MM/yyyy},", ltDV[_Start].Value.AddMonths(-1));
                                            else
                                                strTime += string.Format("T{0:MM}-T{1:MM/yyyy},", ltDV[_Start].Value.AddMonths(-1), ltDV[j].Value.AddMonths(-1));
                                        }
                                        else
                                        {
                                            if (ltDV[_Start].Value.Year != ltDV[j].Value.Year)
                                                strTime += string.Format("T{0:MM}-T{1:MM/yyyy},", ltDV[_Start], ltDV[j]);////Loi
                                            else
                                                strTime += string.Format("T{0:MM}-T{1:MM/yyyy},", ltDV[_Start], ltDV[j]);
                                        }

                                    }
                                    else
                                    {
                                        if (i.MaLDV == 8 | i.MaLDV == 22)
                                            strTime += string.Format("T{0:MM/yyyy},", ltDV[_Start].Value.AddMonths(-1));
                                        else
                                        {
                                            strTime += string.Format("T{0:MM/yyyy},", ltDV[_Start]);
                                        }
                                    }

                                    _Start = j + 1;
                                }

                                j++;
                            }

                            strTime = strTime.TrimEnd(',');

                            foreach (var tam in ltLDVXe)
                            {
                                strDienGiai += string.Format("{0}, ", strTime);
                            }
                            if (ltLDVXe.Count == 0)
                            {
                                strDienGiai += string.Format("{0}, ", strTime);
                            }
                        }
                    }

                }
            }
            catch (Exception ec) { DialogBox.Alert(ec.Message); }

            return strDienGiai.Trim().TrimEnd(',');
        }
    }
}
