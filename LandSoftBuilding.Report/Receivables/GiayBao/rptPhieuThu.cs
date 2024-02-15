using System;
using System.Drawing;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
using Library;
using System.Data.Linq.SqlClient;
using LandSoftBuilding.Report;

namespace LandSoftBuilding.Receivables.GiayBao
{
    public partial class rptPhieuThu : DevExpress.XtraReports.UI.XtraReport
    {
        public int kh
        {
            get;
            set;
        }
        public int thang
        {
            get;
            set;
        }
        public int nam
        {
            get;
            set;
        }
        public rptPhieuThu(byte _MaTN, int _Thang, int _Nam, int _MaKH)
        {
           
            InitializeComponent();
            kh = _MaKH;
            thang = _Thang;
            nam = _Nam;
            Library.frmPrintControl.LoadLayout(this, 30, _MaTN);

            var objTien = new TienTeCls();
            using (var db = new Library.MasterDataContext())
            {
                try
                {
                    var objKH = (from mb in db.mbMatBangs
                                 join Kh in db.tnKhachHangs on mb.MaKH equals Kh.MaKH
                                 where mb.MaKH == _MaKH
                                 select new
                                 {
                                     TenKH = Kh.IsCaNhan == true ? (Kh.HoKH + " " + Kh.TenKH) : Kh.CtyTen,
                                     mb.MaSoMB
                                 }).First();

                    var ltData = (from hd in db.dvHoaDons
                                  join ldv in db.dvLoaiDichVus on hd.MaLDV equals ldv.ID
                                  where hd.MaKH == _MaKH & hd.NgayTT.Value.Month == _Thang & hd.NgayTT.Value.Year == _Nam & hd.ConNo.GetValueOrDefault() > 0
                                  group hd by new { hd.MaLDV, hd.DienGiai, ldv.TenHienThi } into gr
                                  select new
                                  {
                                      gr.Key.MaLDV,
                                      gr.Key.TenHienThi,
                                      gr.Key.DienGiai,
                                      SoTien = gr.Sum(p => p.ConNo)
                                  }).ToList();

                    var _Ngay = new DateTime(_Nam, _Thang, 1);
                    var _NoCu = (from hd in db.dvHoaDons
                                 where hd.MaKH == _MaKH & hd.ConNo.GetValueOrDefault() > 0 & SqlMethods.DateDiffDay(hd.NgayTT, _Ngay) > 0
                                 select hd.ConNo).Sum().GetValueOrDefault();
                    var _PhatSinh = ltData.Sum(p => p.SoTien).GetValueOrDefault();
                    var _TongTien = Convert.ToInt64(Math.Round(_PhatSinh + _NoCu, 0));

                    var dienGiai = "";
                    foreach (var i in ltData)
                        dienGiai += i.DienGiai + ", ";

                    if (objKH.TenKH != "")
                        cNguoiNop.Text = objKH.TenKH;
                    if (objKH.MaSoMB != "")
                        cDiaChi.Text = objKH.MaSoMB;
                    if (dienGiai != "")
                        cLyDo.Text = _MaTN == 27 ? GetDienGiaiThangLong() : dienGiai.Trim().Trim(',');
                    cSoTien.Text = string.Format("{0:#,0.##}", _TongTien);
                    cSoTienBC.Text = cSoTienBC2.Text = objTien.DocTienBangChu(_TongTien, "đồng chẵn");

                    #region Thong tin toa nha
                    var objTN = db.tnToaNhas.FirstOrDefault(p => p.MaTN == _MaTN);
                    cTenTN.Text = objTN.CongTyQuanLy;
                    cDiaChiTN.Text = objTN.DiaChiCongTy;
                    cDienThoaiTN.Text = "Tel: " +  objTN.DienThoai;
                   // picLogo.ImageUrl = objTN.Logo;
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
                              join ldv in db.dvLoaiDichVus on hd.MaLDV equals ldv.ID
                              join tx in db.dvgxTheXes on hd.LinkID equals tx.ID into the
                              from tx in the.DefaultIfEmpty()
                              where hd.MaKH == kh & hd.NgayTT.Value.Month == thang & hd.NgayTT.Value.Year == nam & hd.ConNo.GetValueOrDefault() > 0
                              group hd by new { hd.MaLDV, ldv.TenLDV,tx.BienSo } into gr
                              select new
                              {
                               
                                  gr.Key.BienSo,
                                  gr.Key.MaLDV,
                                  gr.Key.TenLDV,
                                
                                  
                                 // gr.Key.NgayTT,
                              }).ToList();

                foreach (var i in ltData)
                {
                    var ltLDVXe = (from l in ltData
                                   //join xe in db.dvgxTheXes on l.LinkID equals xe.ID into t
                                   //from xe in t.DefaultIfEmpty()
                                   where l.MaLDV == i.MaLDV & i.MaLDV == 6 & l.BienSo == i.BienSo
                                   group l by new { l.MaLDV, l.BienSo } into gr
                                   select new { gr.Key.MaLDV, BienSo = gr.Key.BienSo }).Distinct().ToList();
                    var ltDV = (from hd in db.dvHoaDons
                                join ldv in db.dvLoaiDichVus on hd.MaLDV equals ldv.ID
                                join tx in db.dvgxTheXes on hd.LinkID equals tx.ID into the
                                from tx in the.DefaultIfEmpty()
                                where hd.MaKH == kh & hd.NgayTT.Value.Month == thang & hd.NgayTT.Value.Year == nam & hd.ConNo.GetValueOrDefault() > 0
                                group hd by new { hd.NgayTT.Value.Month, hd.NgayTT.Value.Year} into gr
                                orderby gr.Max(p => p.NgayTT)
                                select gr.Max(p => p.NgayTT)).ToList();
                    var j = 0;
                    var _Start = j;
                    var strTime = "";

                    while (j < ltDV.Count)
                    {
                        if ((j + 1) == ltDV.Count || System.Data.Linq.SqlClient.SqlMethods.DateDiffMonth(ltDV[j], ltDV[j + 1]) != 1)
                        {

                            decimal TienXeC = 0;
                            decimal TienXeD = 0;
                            if (_Start != j)
                            {
                                if (i.MaLDV == 8 | i.MaLDV == 22)
                                {
                                    if (ltDV[_Start].Value.Year != ltDV[j].Value.Year)
                                        strTime += string.Format("T{0:MM/yyyy}-T{1:MM/yyyy},", ltDV[_Start].Value.AddMonths(-1));
                                    else
                                        strTime += string.Format("T{0:MM}-T{1:MM/yyyy},", ltDV[_Start].Value.AddMonths(-1), ltDV[j].Value.AddMonths(-1));
                                }
                                else
                                {
                                    if (ltDV[_Start].Value.Year != ltDV[j].Value.Year)
                                        strTime += string.Format("T{0:MM/yyyy}-T{1:MM/yyyy},", ltDV[_Start]);
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
                    //strDienGiai += string.Format("{0} ({1}), ", i.TenLDV, strTime);
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
            catch { }

            return strDienGiai.Trim().TrimEnd(',');
        }
    }

}
