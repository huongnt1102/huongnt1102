using System;
using System.Drawing;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
using Library;

namespace LandSoftBuilding.Fund.Input
{
    public partial class rptDetail : DevExpress.XtraReports.UI.XtraReport
    {
        public int _ID { get; set; }
        public int _MaTN { get; set; }
        public rptDetail(int ID, byte MaTN)
        {
            InitializeComponent();
            _ID = ID;
            _MaTN = MaTN;
            //Library.frmPrintControl.LoadLayout(this, 19, MaTN);

            if (ID == 0) return;

            #region DataBindings
            cSoPT.DataBindings.Add("Text", null, "SoPT", "Số phiếu: {0}");
            cNgayPT.DataBindings.Add("Text", null, "NgayThu", "Ngày {0:dd} tháng {0:MM} năm {0:yyyy}");
            cNguoiNop.DataBindings.Add("Text", null, "NguoiNop");
            cDiaChi.DataBindings.Add("Text", null, "DiaChiNN");
            //cLyDo.DataBindings.Add("Text", null, "LyDo");
            cSoTien.DataBindings.Add("Text", null, "SoTien", "{0:n0} VNĐ");
            cSoTienBC.DataBindings.Add("Text", null, "SoTien_BangChu");

            cSoPT2.DataBindings.Add("Text", null, "SoPT", "Số phiếu: {0}");
            cNgayPT2.DataBindings.Add("Text", null, "NgayThu", "Ngày {0:dd} tháng {0:MM} năm {0:yyyy}");
            cNguoiNop2.DataBindings.Add("Text", null, "NguoiNop");
            cDiaChi2.DataBindings.Add("Text", null, "DiaChiNN");
          //  cLyDo2.DataBindings.Add("Text", null, "LyDo");
            cSoTien2.DataBindings.Add("Text", null, "SoTien", "{0:n0} VNĐ");
            cSoTienBC2.DataBindings.Add("Text", null, "SoTien_BangChu");
           
            
            #endregion

            DevExpress.XtraPrinting.BarCode.QRCodeGenerator qrCodeGenerator1 = new DevExpress.XtraPrinting.BarCode.QRCodeGenerator();
            var objTien = new TienTeCls();
            using (var db = new Library.MasterDataContext())
            {
                try
                {
                    var obj = (from p in db.ptPhieuThus
                               where p.ID == ID
                               select new
                               {
                                   p.MaTN,
                                   p.SoPT,
                                   p.NgayThu,
                                   p.NguoiNop,
                                   p.DiaChiNN,
                                   p.LyDo,
                                   p.SoTien,p.MaPL,
                                   SoTien_BangChu = objTien.DocTienBangChu(p.SoTien.Value, "đồng chẵn")
                               }).ToList();

                    this.DataSource = obj;

                    #region Thong tin toa nha
                    var objTN = db.tnToaNhas.FirstOrDefault(p => p.MaTN == MaTN);
                    cTenTN.Text = cTenTN2.Text = objTN.CongTyQuanLy;
                    cDiaChiTN.Text = cDiaChiTN2.Text = objTN.DiaChiCongTy;
                    cDienThoaiTN.Text = cDienThoaiTN2.Text = "Tel: " +  objTN.DienThoai;
                    picLogo.ImageUrl = picLogo2.ImageUrl = objTN.Logo;
                    c1.Text = c2.Text = Common.User.HoTenNV.ToString().ToUpper();
              if (_MaTN == 27)
                {
                    if (obj.First().MaPL == 2)
                    {
                        cLyDo.DataBindings.Add("Text", null, "LyDo");
                    }
                    else
                    {
                        cLyDo.Text = GetDienGiaiThangLong();
                    }
                    
                }
            
            else
                cLyDo.DataBindings.Add("Text", null, "LyDo");
            if (_MaTN == 27)
                if (obj.First().MaPL == 2)
                {
                    cLyDo2.DataBindings.Add("Text", null, "LyDo");
                }
                else
                {
                    cLyDo2.Text = GetDienGiaiThangLong();
                }
            else
                cLyDo2.DataBindings.Add("Text", null, "LyDo");
                    #endregion
                }
                catch { }
            }
        }
        string GetDienGiaiThangLong()
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
                              where ptct.MaPT == _ID
                              group hd by new { hd.MaLDV, ldv.TenLDV, tx.BienSo } into gr
                              select new
                              {

                                  gr.Key.BienSo,
                                  gr.Key.MaLDV,
                                  gr.Key.TenLDV,
                                  // gr.Key.NgayTT,
                              }).ToList();

                foreach (var i in ltData)
                {
                    var Test = (from hd in db.dvHoaDons
                                  join ptct in db.ptChiTietPhieuThus on hd.ID equals ptct.LinkID
                                  join ldv in db.dvLoaiDichVus on hd.MaLDV equals ldv.ID
                                  
                                  where ptct.MaPT == _ID & ldv.ID==i.MaLDV
                                  
                                  select new
                                  {

                                     hd.LinkID,
                                     hd.TableName,
                                     hd.DienGiai,hd.ID,
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
                                           where l.MaLDV == i.MaLDV & l.BienSo == i.BienSo
                                           group l by new { l.MaLDV, l.BienSo } into gr
                                           select new { gr.Key.MaLDV, BienSo = gr.Key.BienSo }).Distinct().ToList();
                            var ltDV = i.BienSo != null ? (from hd in db.dvHoaDons
                                                           join ptct in db.ptChiTietPhieuThus on hd.ID equals ptct.LinkID
                                                           join ldv in db.dvLoaiDichVus on hd.MaLDV equals ldv.ID
                                                           //join tx in db.dvgxTheXes on hd.LinkID equals tx.ID into the
                                                           //from tx in the.DefaultIfEmpty()
                                                           where ldv.ID == i.MaLDV & ptct.MaPT == _ID & ptct.DienGiai.Contains(i.BienSo.ToString())
                                                           group hd by new { hd.NgayTT.Value.Month, hd.NgayTT.Value.Year } into gr
                                                           orderby gr.Max(p => p.NgayTT) 
                                                           select gr.Max(p => p.NgayTT)).ToList() : (from hd in db.dvHoaDons
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
                                strDienGiai += string.Format("{0} {1} ({2}) , ", i.TenLDV, tam.BienSo, strTime);
                            }
                            if (ltLDVXe.Count == 0)
                            {
                                strDienGiai += string.Format("{0} ({1}), ", i.TenLDV, strTime);
                            }
                        }
                    }
                    
                }
            }
            catch (Exception ec){ DialogBox.Alert(ec.Message);}

            return strDienGiai.Trim().TrimEnd(',');
        }
    }
}
