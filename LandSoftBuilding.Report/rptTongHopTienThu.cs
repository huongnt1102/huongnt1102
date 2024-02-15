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
    public partial class rptTongHopTienThu : DevExpress.XtraReports.UI.XtraReport
    {
        MasterDataContext db = new MasterDataContext();
        public rptTongHopTienThu(DateTime _tungay, DateTime _dengnay, int? _MaTN, string LoaiThu)
        {
            InitializeComponent();
            cThoiGian.Text = "(Từ ngày " + string.Format("{0:dd/MM/yyyy}", _tungay) + " đến ngày " + string.Format("{0:dd/MM/yyyy}", _dengnay) +")";
            cSTT.DataBindings.Add("Text", null, "STT");
            cNgayCT.DataBindings.Add("Text", null, "NgayThu","{0:dd/MM/yyy}");
            cSoPC.DataBindings.Add("Text", null, "SoPT");
            cSoTien.DataBindings.Add("Text", null, "SoTien", "{0:#,0.##}");
            cNguoiNhap.DataBindings.Add("Text", null, "NguoiNop");
            cLyDo.DataBindings.Add("Text", null, "LyDo", "{0:#,0.##}");
            cSumTongTien.DataBindings.Add(new XRBinding("Text", null, "SoTien", "{0:#,0.##}"));
            cSumTongTien.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,0.##}");

            var ltPhieuThu = (from pt in db.ptPhieuThus
                     join lpt in db.ptPhanLoais on pt.MaPL equals lpt.ID
                     join kh in db.tnKhachHangs on pt.MaKH equals kh.MaKH
                     join nkh in db.khNhomKhachHangs on kh.MaNKH equals nkh.ID into nhom
                     from nkh in nhom.DefaultIfEmpty()
                     join tn in db.tnToaNhas on kh.MaTN equals tn.MaTN
                     join nv in db.tnNhanViens on pt.MaNV equals nv.MaNV
                     where pt.MaTN == _MaTN & SqlMethods.DateDiffDay(_tungay, pt.NgayThu) >= 0 & SqlMethods.DateDiffDay(pt.NgayThu, _dengnay) >= 0 & (LoaiThu == "1, 2" || (LoaiThu == "1" & pt.MaTKNH == null) || (LoaiThu == "2" & pt.MaTKNH != null)) 
                     orderby pt.NgayThu ascending 
                     select new
                     {
                         pt.SoPT,
                         pt.NgayThu,
                         pt.NgayNhap,
                         pt.SoTien,
                         NguoiNhap = nv.HoTenNV,
                         pt.LyDo,
                         pt.NguoiNop,
                     }).ToList();
            this.DataSource = ltPhieuThu.Select
                (
                    (p, index) =>
                    new
                    {
                        STT = index + 1,
                        p.NguoiNhap,
                        p.SoPT,
                        p.LyDo,
                        p.SoTien,
                        p.NgayThu,
                        p.NguoiNop,
                    }
                ).ToList();
        }

    }
}
