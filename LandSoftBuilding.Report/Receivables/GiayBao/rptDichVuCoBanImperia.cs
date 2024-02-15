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
    public partial class rptDichVuCoBanImperia : DevExpress.XtraReports.UI.XtraReport
    {
        public rptDichVuCoBanImperia(byte _MaTN, int _MaKH, int _Thang, int _Nam, int _MaLDV)
        {
            InitializeComponent();

         //   Library.frmPrintControl.LoadLayout(this, 5, _MaTN);
            cKy.DataBindings.Add("Text", null, "KyTT");
            cTenLDV.DataBindings.Add("Text", null, "TenHienThi");
            cHan.DataBindings.Add("Text", null, "HanTT");
            
            cThanhTien.DataBindings.Add("Text", null, "ThanhTien", "{0:#,0.##}");

            var db = new MasterDataContext();
            try
            {
                this.DataSource = (from hd in db.dvHoaDons
                                   //join dv in db.dvDichVuKhacs on new { hd.TableName, hd.LinkID } equals new { TableName = "dvDichVuKhac", LinkID = (int?)dv.ID }
                                   join dv in db.dvDichVuKhacs on new { hd.MaLDV, hd.LinkID } equals new { MaLDV = (int?)dv.MaLDV, LinkID = (int?)dv.ID }
                                   join ldv in db.dvLoaiDichVus on dv.MaLDV equals ldv.ID
                                   join dvt in db.DonViTinhs on dv.MaDVT equals dvt.ID
                                   where hd.MaTN == _MaTN & hd.MaLDV!=13 & hd.MaLDV == _MaLDV & hd.MaKH == _MaKH 
                                   & hd.NgayTT.Value.Month == _Thang & hd.NgayTT.Value.Year == _Nam 
                                   & hd.PhaiThu.GetValueOrDefault()
                                    - (db.ptChiTietPhieuThus.Where(p => p.LinkID == hd.ID & p.TableName == "dvHoaDon").Sum(p => p.SoTien)).GetValueOrDefault()
                                    - (db.ktttChiTiets.Where(p => p.LinkID == hd.ID & p.TableName == "dvHoaDon").Sum(p => p.SoTien)).GetValueOrDefault()
                                   > 0
                                   select new
                                   {
                                       
                                       dv.SoLuong,ldv.TenHienThi,
                                       dvt.TenDVT,
                                       DonGia = dv.DonGia * dv.TyGia,
                                       HanTT = string.Format("{0:dd/MM/yyyy}",new DateTime(_Nam, _Thang, DateTime.DaysInMonth(_Nam, _Thang))),
                                       ThanhTien = hd.PhaiThu - (db.ptChiTietPhieuThus.Where(p => p.LinkID == hd.ID & p.TableName == "dvHoaDon").Sum(p => p.SoTien)).GetValueOrDefault()
                                    - (db.ktttChiTiets.Where(p => p.LinkID == hd.ID & p.TableName == "dvHoaDon").Sum(p => p.SoTien)).GetValueOrDefault(),
                                       KyTT = string.Format("T{0}/{1}",_Thang-1 == 0 ? 12 : _Thang -1 , _Thang -1 == 0 ? _Nam-1 : _Nam),
                                   }).ToList();
            }
            catch { }
            finally
            {
                db.Dispose();
            }
        }

        public void LoadData(byte _MaTN, int _MaKH, int _Thang, int _Nam, int _MaLDV)
        {
            var db = new MasterDataContext();
            try
            {
                this.DataSource = (from hd in db.dvHoaDons
                                   //join dv in db.dvDichVuKhacs on new { hd.TableName, hd.LinkID } equals new { TableName = "dvDichVuKhac", LinkID = (int?)dv.ID }
                                   join dv in db.dvDichVuKhacs on new { hd.MaLDV, hd.LinkID } equals new { MaLDV = (int?)dv.MaLDV, LinkID = (int?)dv.ID }
                                   join ldv in db.dvLoaiDichVus on dv.MaLDV equals ldv.ID
                                   join dvt in db.DonViTinhs on dv.MaDVT equals dvt.ID
                                   where hd.MaTN == _MaTN & hd.MaLDV != 13 //& hd.MaLDV == _MaLDV 
                                   & hd.MaKH == _MaKH
                                   & hd.NgayTT.Value.Month == _Thang & hd.NgayTT.Value.Year == _Nam
                                   & hd.PhaiThu.GetValueOrDefault()
                                    - (db.ptChiTietPhieuThus.Where(p => p.LinkID == hd.ID & p.TableName == "dvHoaDon").Sum(p => p.SoTien)).GetValueOrDefault()
                                    - (db.ktttChiTiets.Where(p => p.LinkID == hd.ID & p.TableName == "dvHoaDon").Sum(p => p.SoTien)).GetValueOrDefault()
                                   > 0
                                   select new
                                   {

                                       dv.SoLuong,
                                       ldv.TenHienThi,
                                       dvt.TenDVT,
                                       DonGia = dv.DonGia * dv.TyGia,
                                       HanTT = string.Format("{0:dd/MM/yyyy}", new DateTime(_Nam, _Thang, DateTime.DaysInMonth(_Nam, _Thang))),
                                       ThanhTien = hd.PhaiThu - (db.ptChiTietPhieuThus.Where(p => p.LinkID == hd.ID & p.TableName == "dvHoaDon").Sum(p => p.SoTien)).GetValueOrDefault()
                                    - (db.ktttChiTiets.Where(p => p.LinkID == hd.ID & p.TableName == "dvHoaDon").Sum(p => p.SoTien)).GetValueOrDefault(),
                                       KyTT = string.Format("T{0}/{1}", _Thang - 1 == 0 ? 12 : _Thang - 1, _Thang - 1 == 0 ? _Nam - 1 : _Nam),
                                   }).ToList();
            }
            catch { }
            finally
            {
                db.Dispose();
            }
        }
    }
}
