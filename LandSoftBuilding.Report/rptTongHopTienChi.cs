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
    public partial class rptTongHopTienChi : DevExpress.XtraReports.UI.XtraReport
    {
        MasterDataContext db = new MasterDataContext();
        public rptTongHopTienChi(DateTime _tungay, DateTime _dengnay, int? _MaTN)
        {
            InitializeComponent();
            cThoiGian.Text = "(Từ ngày " + string.Format("{0:dd/MM/yyyy}", _tungay) + " đến ngày " + string.Format("{0:dd/MM/yyyy}", _dengnay) +")";
            cSTT.DataBindings.Add("Text", null, "STT");
            cNgayCT.DataBindings.Add("Text", null, "NgayChi","{0:dd/MM/yyy}");
            cSoPC.DataBindings.Add("Text", null, "SoPC");
            cSoTien.DataBindings.Add("Text", null, "SoTien", "{0:#,0.##}");
            cNguoiNhap.DataBindings.Add("Text", null, "NguoiNhan");
            cLyDo.DataBindings.Add("Text", null, "LyDo", "{0:#,0.##}");
            cSumTongTien.DataBindings.Add(new XRBinding("Text", null, "SoTien", "{0:#,0.##}"));
            cSumTongTien.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,0.##}");


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
                          p.NgaySua,
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
                        p.NguoiNhan,
                    }
                ).ToList();
        }

    }
}
