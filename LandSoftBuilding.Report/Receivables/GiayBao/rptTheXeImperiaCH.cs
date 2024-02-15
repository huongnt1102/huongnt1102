using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
using Library;
using System.Data.Linq.SqlClient;

namespace LandSoftBuilding.Receivables.GiayBao
{
    public partial class rptTheXeImperiaCH : DevExpress.XtraReports.UI.XtraReport
    {
        private int Thang { get; set; }
        private int Nam { get; set; }
        public rptTheXeImperiaCH(byte _MaTN, int _MaKH, int _Thang, int _Nam)
        {
            InitializeComponent();

            Library.frmPrintControl.LoadLayout(this, 13, _MaTN);

            Thang= _Thang;
            Nam = _Nam;
            var db = new MasterDataContext();
            try
            {
              
                var TuNgay = new DateTime(_Nam, _Thang, 1);
                var DenNgay = new DateTime(_Nam, _Thang, DateTime.DaysInMonth(_Nam, _Thang));
                cTuNgay.Text = string.Format("{0:dd/MM/yyyy}", TuNgay);
                cDenNgay.Text = string.Format("{0:dd/MM/yyyy}", DenNgay);
                //Code Lâm sửa cho Kim Cương Xanh
                var TienBac = (from tx in db.dvgxTheXes
                               join lx in db.dvgxLoaiXes on tx.MaLX equals lx.MaLX into dslx
                               from lx in dslx.DefaultIfEmpty()
                               join nx in db.dvgxNhomXes on lx.MaNX equals nx.ID
                               // join gx in db.dvgxGiuXes on tx.MaGX equals gx.ID
                               join dv in db.dvHoaDons on tx.ID equals dv.LinkID
                              
                               where tx.MaTN == _MaTN & tx.MaKH == _MaKH & dv.NgayTT.Value.Month == _Thang & dv.NgayTT.Value.Year == _Nam
                                & (dv.PhaiThu.GetValueOrDefault()
                    - (db.ptChiTietPhieuThus.Where(p => p.LinkID == dv.ID & p.TableName == "dvHoaDon").Sum(p => p.SoTien)).GetValueOrDefault()
                    - (db.ktttChiTiets.Where(p => p.LinkID == dv.ID & p.TableName == "dvHoaDon").Sum(p => p.SoTien)).GetValueOrDefault()
                    ) > 0
                               select new
                               {
                                   tx.ID,tx.GiaThang,
                                   MaLX = nx.ID,
                                   DonGia = tx.GiaThang,
                                   tx.TienTruocThue,
                                   tx.TienTT,
                                   tx.TienThueGTGT
                               }).ToList();
                //var ltGiuXete = (from tx in db.dvgxTheXes
                //                 join lx in db.dvgxLoaiXes on tx.MaLX equals lx.MaLX into dslx
                //                 from lx in dslx.DefaultIfEmpty()
                //                 join nx in db.dvgxNhomXes on lx.MaNX equals nx.ID
                //                 // join gx in db.dvgxGiuXes on tx.MaGX equals gx.ID
                //                 join dv in db.dvHoaDons on tx.ID equals dv.LinkID
                //                 join pt in
                //                     (
                //                          from ct in db.ptChiTietPhieuThus
                //                          join pthu in db.ptPhieuThus on ct.MaPT equals pthu.ID
                //                          where ct.TableName == "dvHoadon" & pthu.MaTN == _MaTN
                //                          select new { ct.LinkID, DaThu = true }
                //                     ) on dv.ID equals pt.LinkID into dspt
                //                 from pt in dspt.DefaultIfEmpty()
                //                 where tx.MaTN == _MaTN & tx.MaKH == _MaKH & dv.NgayTT.Value.Month == _Thang & dv.NgayTT.Value.Year == _Nam
                //                 group tx by new { nx.ID, nx.TenNX, tx.GiaThang } into gr
                //                 select new GiuXeItem()
                //                 {
                //                     MaLX = gr.Key.ID,
                //                     TenLX = gr.Key.TenNX,
                //                     BienSo = "",
                //                     SoLuong = gr.Count(),
                //                     DonGia = gr.Key.GiaThang,
                //                     ThanhTien = gr.Count() * gr.Key.GiaThang
                //                 }).ToList();

                //this.DataSource = ltGiuXete;

                var NgayTB = new DateTime((int)_Nam, (int)_Thang, 1);
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
                                   & hd.MaLDV == 6
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
                cSLXM.Text = string.Format("{0:#,0.##}  Xe máy (bike)", TienBac.Where(p => p.MaLX == 2).Count());
                cSLOto.Text = string.Format("{0:#,0.##}  Ô tô (car)", TienBac.Where(p => p.MaLX == 3).Count());
                cDGXM.Text = string.Format("{0}", TienBac.Where(p => p.MaLX == 2).Count() == 0 ? "" : string.Format("{0:#,0.##} VNĐ", TienBac.Where(p => p.MaLX == 2).FirstOrDefault().DonGia));
                cDGOto.Text = string.Format("{0}", TienBac.Where(p => p.MaLX == 3).Count() == 0 ? "" : string.Format("{0:#,0.##} VNĐ", TienBac.Where(p => p.MaLX == 3).FirstOrDefault().DonGia));
                cTTXM.Text = string.Format("{0}", TienBac.Where(p => p.MaLX == 2).Count() == 0 ? "" : string.Format("{0:#,0.##} VNĐ", TienBac.Where(p => p.MaLX == 2).Sum(p => p.GiaThang)));
                cTTOto.Text = string.Format("{0}", TienBac.Where(p => p.MaLX == 3).Count() == 0 ? "" : string.Format("{0:#,0.##} VNĐ", TienBac.Where(p => p.MaLX == 3).Sum(p => p.GiaThang)));
                cPS.Text = string.Format("{0:#,0.##} VNĐ", TienBac.Sum(p => p.TienTruocThue).GetValueOrDefault());
                //cTienVAT.Text = string.Format("{0:#,0.##}", TienBac.Sum(p => p.TienThueGTGT).GetValueOrDefault());
                //cTongChuaNo.Text = string.Format("{0:#,0.##}", TienBac.Sum(p => p.TienTT).GetValueOrDefault());
                cNo.Text = string.Format("{0:#,0.##} VNĐ", _NoCu);
                cTienXe.Text = string.Format("{0:#,0.##} VNĐ", TienBac.Sum(p => p.TienTT).GetValueOrDefault() + _NoCu);
              
            }
            catch { }
            finally
            {
                db.Dispose();
            }
        }
   
    }


}
