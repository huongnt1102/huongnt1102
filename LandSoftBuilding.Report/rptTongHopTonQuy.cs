using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Data.Linq.SqlClient;
using System.Linq;
using DevExpress.XtraReports.UI;
using Library;

namespace LandSoftBuilding.Report
{
    public partial class rptTongHopTonQuy : DevExpress.XtraReports.UI.XtraReport
    {
        MasterDataContext db = new MasterDataContext();
        public rptTongHopTonQuy(DateTime _tungay, DateTime _dengnay, int? _MaTN)
        {
            InitializeComponent();
            cThoiGian.Text = "(Từ ngày " + string.Format("{0:dd/MM/yyyy}", _tungay) + " đến ngày " + string.Format("{0:dd/MM/yyyy}", _dengnay) + ")";
            decimal _ChiDK = db.pcPhieuChis.Where(p => p.MaTN == _MaTN & SqlMethods.DateDiffDay(p.NgayChi, _tungay) > 0).Sum(p => p.SoTien).GetValueOrDefault();
            decimal _ThuDK = db.ptPhieuThus.Where(p => p.MaTN == _MaTN & SqlMethods.DateDiffDay(p.NgayThu, _tungay) > 0 & p.MaTKNH == null).Sum(p => p.SoTien).GetValueOrDefault();
            decimal _TonDK = _ThuDK - _ChiDK;
            decimal _Thu = db.ptPhieuThus.Where(p => p.MaTN == _MaTN & SqlMethods.DateDiffDay(_tungay, p.NgayThu) >= 0 & SqlMethods.DateDiffDay(p.NgayThu, _dengnay) >= 0 & p.MaTKNH == null).Sum(p => p.SoTien).GetValueOrDefault();
            decimal _Chi = db.pcPhieuChis.Where(p => p.MaTN == _MaTN & SqlMethods.DateDiffDay(_tungay, p.NgayChi) >= 0 & SqlMethods.DateDiffDay(p.NgayChi, _dengnay) >= 0 ).Sum(p => p.SoTien).GetValueOrDefault();
            decimal _TonCuoi = _TonDK + _Thu - _Chi;

            cTonDau.Text = string.Format("{0:N0}", _TonDK);
            cThu.Text = string.Format("{0:N0}", _Thu);
            cChi.Text = string.Format("{0:N0}", _Chi);
            cTonCuoi.Text = string.Format("{0:N0}", _TonCuoi); 

            var ltPhieuChi = (from p in db.pcPhieuChis
                      join nv in db.tnNhanViens on p.MaNV equals nv.MaNV
                      join nvn in db.tnNhanViens on p.MaNVN equals nvn.MaNV
                      join nvs in db.tnNhanViens on p.MaNVS equals nvs.MaNV into tblNguoiSua
                      from nvs in tblNguoiSua.DefaultIfEmpty()
                      where p.MaTN == _MaTN & SqlMethods.DateDiffDay(_tungay, p.NgayChi) >= 0 & SqlMethods.DateDiffDay(p.NgayChi, _dengnay) >= 0
                      select new
                      {
                          p.ID,
                          p.SoPC,
                          p.NgayChi,
                          p.SoTien,
                          NguoiChi = nv.HoTenNV,
                          p.NguoiNhan,
                          p.DiaChiNN,
                          p.LyDo,
                          p.ChungTuGoc,
                          NguoiNhap = nvn.HoTenNV,
                          p.NgayNhap,
                          NguoiSua = nvs.HoTenNV,
                          p.NgaySua
                      }).ToList();

            this.DataSource = ltPhieuChi.Select
                (
                    (p, index) =>
                    new
                    {
                        STT = index + 1,
                        p.NguoiNhap,
                        p.SoPC,
                        p.LyDo,
                        p.SoTien,
                        p.NgayChi,
                    }
                ).ToList();
        }

    }
}
