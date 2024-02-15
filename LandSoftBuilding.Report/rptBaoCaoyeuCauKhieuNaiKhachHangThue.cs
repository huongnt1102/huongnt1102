using System;
//using DIP.BUILDING.TRUNGTIN.DATAENTITY;
//using Shark.Net.Http;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq.SqlClient;
using Library;
namespace LandSoftBuilding.Report
{
    public partial class rptBaoCaoyeuCauKhieuNaiKhachHangThue : DevExpress.XtraReports.UI.XtraReport
    {
        // tnycYeuCau,mbmatbang,tnkhachhang,tnycTrangThai,,tnPhongBan,tnnhanvien
        public rptBaoCaoyeuCauKhieuNaiKhachHangThue(DateTime tuNgay, DateTime denNgay, byte MaTN, List<int> _MaLDVs, List<int> _MaYCs)
        {
            InitializeComponent();
            #region Binding
            cellMaYeuCau.DataBindings.Add("Text", null, "MaYC");
            cellNgayYeuCau.DataBindings.Add("Text", null, "NgayYC", "{0:dd/MM/yyyy}");
            cellMatBang.DataBindings.Add("Text", null, "MaMB");
            cellTenCongTy.DataBindings.Add("Text", null, "TenKH");
            cellNoiDung.DataBindings.Add("Text", null,"NoiDung");
            cellTrangThai.DataBindings.Add("Text", null, "TenTT");
            cellDoUuTien.DataBindings.Add("Text", null, "TenDoUuTien");
            cellNguoiGuiYeuCau.DataBindings.Add("Text", null, "NguoiGui");
            cellBoPhanTiepNhan.DataBindings.Add("Text", null,"TenPB");
            cellNguoiXuLy.DataBindings.Add("Text", null, "HoTenNV");
            #endregion
            var db = new MasterDataContext();
        try
            {
                tittleDate.Text = string.Format("Từ ngày {0:dd/MM/yyyy} đến ngày {1:dd/MM/yyyy}", tuNgay, denNgay);
                var objTN = db.tnToaNhas.Single(p => p.MaTN == MaTN);
                //if (MaTN != 58)
                //{
                    xrPictureBox1.ImageUrl = objTN.Logo;
                //}
               // if (MaTT == null & MaUu == null)
               // {
                    this.DataSource = (from yc in db.tnycYeuCaus
                                       join mb in db.mbMatBangs on yc.MaMB equals mb.MaMB
                                       join kh in db.tnKhachHangs on mb.MaKH equals kh.MaKH into khach
                                       from kh in khach.DefaultIfEmpty()
                                       join tt in db.tnycTrangThais on yc.MaTT equals tt.MaTT
                                       join pb in db.tnPhongBans on yc.MaBP equals pb.MaPB
                                       join nv in db.tnNhanViens on yc.MaNV equals nv.MaNV
                                       join dut in db.tnycDoUuTiens on yc.MaDoUuTien equals dut.MaDoUuTien
                                       where
                                      SqlMethods.DateDiffDay(tuNgay, yc.NgayYC) >= 0 & SqlMethods.DateDiffDay(yc.NgayYC, denNgay) >= 0
                                           // & (tt.MaTT == MaTT | MaTT == 0) & (dut.MaDoUuTien == MaUu | MaUu == 0)
                                           & _MaLDVs.Contains(tt.MaTT) & _MaYCs.Contains(dut.MaDoUuTien)
                                       & yc.MaTN == MaTN
                                       select new
                                       {
                                           yc.MaYC,
                                           yc.NgayYC,
                                           MaMB = mb.MaSoMB,
                                           TenKH = kh.IsCaNhan == true ? (kh.HoKH + " " + kh.TenKH) : kh.CtyTen,
                                           yc.NoiDung,
                                           tt.TenTT,
                                           dut.TenDoUuTien,
                                           yc.NguoiGui,
                                           pb.TenPB,
                                           nv.HoTenNV

                                       }).ToList();
                    
                //}
                //if (MaTT != null & MaUu == null)
                //{
                //    this.DataSource = (from yc in db.tnycYeuCaus
                //                       join mb in db.mbMatBangs on yc.MaMB equals mb.MaMB
                //                       join kh in db.tnKhachHangs on mb.MaKH equals kh.MaKH into khach
                //                       from kh in khach.DefaultIfEmpty()
                //                       join tt in db.tnycTrangThais on yc.MaTT equals tt.MaTT
                //                       join pb in db.tnPhongBans on yc.MaBP equals pb.MaPB
                //                       join nv in db.tnNhanViens on yc.MaNV equals nv.MaNV
                //                       join dut in db.tnycDoUuTiens on yc.MaDoUuTien equals dut.MaDoUuTien
                //                       where
                //                      SqlMethods.DateDiffDay(tuNgay, yc.NgayYC) >= 0 & SqlMethods.DateDiffDay(yc.NgayYC, denNgay) >= 0
                //                           & tt.MaTT == MaTT
                //                       & yc.MaTN == MaTN
                //                       select new
                //                       {
                //                           yc.MaYC,
                //                           yc.NgayYC,
                //                           MaMB = mb.MaSoMB,
                //                           TenKH = kh.IsCaNhan == true ? (kh.HoKH + " " + kh.TenKH) : kh.CtyTen,
                //                           yc.NoiDung,
                //                           tt.TenTT,
                //                           dut.TenDoUuTien,
                //                           yc.NguoiGui,
                //                           pb.TenPB,
                //                           nv.HoTenNV

                //                       }).ToList();

                //}
                //if (MaTT == null & MaUu != null)
                //{
                //    this.DataSource = (from yc in db.tnycYeuCaus
                //                       join mb in db.mbMatBangs on yc.MaMB equals mb.MaMB
                //                       join kh in db.tnKhachHangs on mb.MaKH equals kh.MaKH into khach
                //                       from kh in khach.DefaultIfEmpty()
                //                       join tt in db.tnycTrangThais on yc.MaTT equals tt.MaTT
                //                       join pb in db.tnPhongBans on yc.MaBP equals pb.MaPB
                //                       join nv in db.tnNhanViens on yc.MaNV equals nv.MaNV
                //                       join dut in db.tnycDoUuTiens on yc.MaDoUuTien equals dut.MaDoUuTien
                //                       where
                //                      SqlMethods.DateDiffDay(tuNgay, yc.NgayYC) >= 0 & SqlMethods.DateDiffDay(yc.NgayYC, denNgay) >= 0
                //                           & dut.MaDoUuTien == MaUu 
                //                       & yc.MaTN == MaTN
                //                       select new
                //                       {
                //                           yc.MaYC,
                //                           yc.NgayYC,
                //                           MaMB = mb.MaSoMB,
                //                           TenKH = kh.IsCaNhan == true ? (kh.HoKH + " " + kh.TenKH) : kh.CtyTen,
                //                           yc.NoiDung,
                //                           tt.TenTT,
                //                           dut.TenDoUuTien,
                //                           yc.NguoiGui,
                //                           pb.TenPB,
                //                           nv.HoTenNV

                //                       }).ToList();

                //}
                //if (MaTT != null & MaUu != null)
                //{
                //    this.DataSource = (from yc in db.tnycYeuCaus
                //                       join mb in db.mbMatBangs on yc.MaMB equals mb.MaMB
                //                       join kh in db.tnKhachHangs on mb.MaKH equals kh.MaKH into khach
                //                       from kh in khach.DefaultIfEmpty()
                //                       join tt in db.tnycTrangThais on yc.MaTT equals tt.MaTT
                //                       join pb in db.tnPhongBans on yc.MaBP equals pb.MaPB
                //                       join nv in db.tnNhanViens on yc.MaNV equals nv.MaNV
                //                       join dut in db.tnycDoUuTiens on yc.MaDoUuTien equals dut.MaDoUuTien
                //                       where
                //                      SqlMethods.DateDiffDay(tuNgay, yc.NgayYC) >= 0 & SqlMethods.DateDiffDay(yc.NgayYC, denNgay) >= 0
                //                        & tt.MaTT == MaTT& dut.MaDoUuTien == MaUu
                //                       & yc.MaTN == MaTN
                //                       select new
                //                       {
                //                           yc.MaYC,
                //                           yc.NgayYC,
                //                           MaMB = mb.MaSoMB,
                //                           TenKH = kh.IsCaNhan == true ? (kh.HoKH + " " + kh.TenKH) : kh.CtyTen,
                //                           yc.NoiDung,
                //                           tt.TenTT,
                //                           dut.TenDoUuTien,
                //                           yc.NguoiGui,
                //                           pb.TenPB,
                //                           nv.HoTenNV

                //                       }).ToList();

                //}
            }
            catch 
            {

            }
            finally
            {
                db.Dispose();
            }

         }

    }
}
