using System;
using System.Drawing;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
using Library;

namespace LandSoftBuilding.Fund.Input
{
    public partial class rptPhieuThu : DevExpress.XtraReports.UI.XtraReport
    {
        public int _ID { get; set; }
        public int _MaTN { get; set; }
        public rptPhieuThu(int ID, byte MaTN,int SoLien)
        {
            _ID = ID;
            _MaTN = MaTN;
            InitializeComponent();

            Library.frmPrintControl.LoadLayout(this, 3, MaTN);

            if (ID == 0) return;

            var db = new Library.MasterDataContext();
            try
            {
                #region Thong tin toa nha
               
                var objTN = db.tnToaNhas.FirstOrDefault(p => p.MaTN == MaTN);
                //cTenTN.Text = objTN.CongTyQuanLy;
                //cDiaChiTN.Text = objTN.DiaChiCongTy;
                //cDienThoaiTN.Text = "Tel: " + objTN.DienThoai;
                picLogo.ImageUrl = objTN.Logo;
                #endregion

                var objTien = new TienTeCls();

                #region Get thu thừa
                
                //var thuThua = db.ptPhieuThus.Where(_ => _.ThuThuaId == ID).Sum(_=>_.SoTien).GetValueOrDefault();
                //var dienGiaiThuThua = "";
                //if(thuThua>0)
                //{
                //    dienGiaiThuThua = "Số tiền thu thừa: " + thuThua;
                //}
                
                #endregion

                var objPT = (from p in db.ptPhieuThus
                             join kh in db.tnKhachHangs on p.MaKH equals kh.MaKH
                             join nv in db.tnNhanViens on p.MaNVN equals nv.MaNV
                             where p.ID == ID
                             select new
                             {
                                 p.MaTN,
                                 p.SoPT,
                                 p.NgayThu,
                                 p.NguoiNop,kh.KyHieu,
                                 p.DiaChiNN,
                                 LyDo = p.TienThuThua.GetValueOrDefault() <= 0 ? p.LyDo : "Đã đóng: " + p.TongTienDaThu.GetValueOrDefault().ToString("c0") + "; Thanh toán dịch vụ: " + p.SoTien.GetValueOrDefault().ToString("c0") + "; " + "Thu thừa: " + p.TienThuThua.GetValueOrDefault().ToString("c0"),
                                 SoTien = p.TienThuThua.GetValueOrDefault() <= 0 ? p.SoTien.GetValueOrDefault() :p.TongTienDaThu.GetValueOrDefault(),
                                 kh.MaPhu,
                                 SoTien_BangChu = objTien.DocTienBangChu(p.SoTien.GetValueOrDefault()+p.TienThuThua.GetValueOrDefault(), "đồng chẵn"),
                                 nv.HoTenNV,
                                 p.MaTKNH,
                                 p.MaMB
                             }).FirstOrDefault();
                var objMB = (from mb in db.mbMatBangs
                             join tl in db.mbTangLaus on mb.MaTL equals tl.MaTL
                             join kn in db.mbKhoiNhas on tl.MaKN equals kn.MaKN
                             join tn in db.tnToaNhas on kn.MaTN equals tn.MaTN
                             where mb.MaMB == objPT.MaMB
                             select new { tn.TenTN,kn.TenKN }).FirstOrDefault();
                cTieuDePhieu.Text = objPT.MaTKNH == null ? "PHIẾU THU" : "PHIẾU THU TIỀN CHUYỂN KHOẢN";
                cBanQuanLy.Text = "Ban Quản lý Tòa nhà: " + objMB.TenTN;
                cKhuVuc.Text = "Khu quản lý: " + objMB.TenKN;
                cSoPhieu.Text = "Số phiếu: " + objPT.SoPT;
                cNgayPT.Text = string.Format("Ngày {0:dd} tháng {0:MM} năm {0:yyyy}", objPT.NgayThu);
                cNguoiNop.Text = objPT.NguoiNop;
                cDiaChi.Text = MaTN == 6 ? objPT.KyHieu + "- (" + objPT.MaPhu + ")- " + objTN.TenVT : objPT.DiaChiNN;
                cSoTien.Text = objPT.SoTien.ToString("c0");
                cSoTienBC.Text = objPT.SoTien_BangChu;
                cNguoiLap.Text = objPT.HoTenNV;
                cDaNhanDu.Text = string.Format("Đã nhận đủ số tiền (Viết bằng chữ): {0} đồng chẵn.", objPT.SoTien_BangChu);
                //Dien giai
                var strDienGiai = "";
                var ltChiTiet = (from ct in db.ptChiTietPhieuThus
                                 where ct.MaPT == ID
                                 select new
                                 {
                                     ct.DienGiai,
                                     SoTien=ct.SoTien.GetValueOrDefault() +ct.ThuThua.GetValueOrDefault()
                                 }).ToList();
               // string sDienGiai=GetDienGiaiThangLong();
                cLyDo.Text = objPT.LyDo; //sDienGiai == "" ? objPT.LyDo : sDienGiai;
                   
            }
            catch { }
            finally
            {
                db.Dispose();
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
                              join tx in db.dvgxTheXes on new { hd.LinkID, hd.MaLDV } equals new { LinkID=(int?)tx.ID, MaLDV = (int?)6 } into the
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
                                                                                             orderby gr.Max(p => p.NgayTT)
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
                                if (i.MaLDV == 8 | i.MaLDV == 22 | i.MaLDV == 9 | i.MaLDV == 11)
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
                                if (i.MaLDV == 8 | i.MaLDV == 22 | i.MaLDV == 9 | i.MaLDV == 11)
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
                    //strDienGiai += string.Format("{0} {1} ({2}), ", i.TenLDV, i.BienSo, strTime);
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
