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
    public partial class rptChiTietQuyTienMat : DevExpress.XtraReports.UI.XtraReport
    {
        decimal? DuDK = 0;
        MasterDataContext db = new MasterDataContext();
        public rptChiTietQuyTienMat(DateTime _tungay, DateTime _dengnay, int? _MaTN)
        {
            InitializeComponent();
            cThoiGian.Text = string.Format("Từ ngày {0:dd/MM/yyyy} đến ngày {1:dd/MM/yyyy}", _tungay, _dengnay);
            DuDK = db.ptPhieuThus.Where(o => o.MaTKNH == null & o.MaTN == _MaTN & SqlMethods.DateDiffDay(o.NgayThu, _tungay) > 0).Sum(o => o.ptChiTietPhieuThus.Sum(p => p.SoTien)).GetValueOrDefault()
                        - db.pcPhieuChis.Where(o => o.MaTKNH == null & o.MaTN == _MaTN & SqlMethods.DateDiffDay(o.NgayChi, _tungay) > 0).Sum(o => o.pcChiTiets.Sum(p => p.SoTien)).GetValueOrDefault();
            ctonDK.Text = string.Format("{0:#,0.##}", DuDK);
            #region DataBinding
            cNgayHachToan.DataBindings.Add("Text", null, "NgayHachToan", "{0:dd/MM/yyyy}");
            cNgayHachToan.DataBindings.Add("Text", null, "NgayCT", "{0:dd/MM/yyyy}");
            cSoPT.DataBindings.Add("Text", null, "SoPT");
            cSoPC.DataBindings.Add("Text", null, "SoPC");
            cLyDo.DataBindings.Add("Text", null, "DienGiai");
            cTienChi.DataBindings.Add("Text", null, "TienChi", "{0:#,0.##}");
            cTienThu.DataBindings.Add("Text", null, "TienThu", "{0:#,0.##}");
            cTenKH.DataBindings.Add("Text", null, "TenKH");
            cTenNV.DataBindings.Add("Text", null, "TenNV");

            cSumTienThu.DataBindings.Add("Text", null, "TienThu");
            cSumTienChi.DataBindings.Add("Text", null, "TienChi");
            cSumTienThu.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,0.##}");
            cSumTienChi.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,0.##}");
            #endregion

            this.DataSource = (from p in db.ptPhieuThus
                               join nv in db.tnNhanViens on p.MaNV equals nv.MaNV
                               where SqlMethods.DateDiffDay(_tungay, p.NgayThu) >= 0 & SqlMethods.DateDiffDay(p.NgayThu, _dengnay) >= 0
                               & p.MaTN == _MaTN & p.MaTKNH == null
                               select new SoQuyItem
                               {
                                   NgayHachToan = p.NgayThu,
                                   NgayCT = p.NgayThu,
                                   SoPT = p.SoPT,
                                   SoPC = "",
                                   DienGiai = p.LyDo,
                                   TienThu = p.ptChiTietPhieuThus.Sum(o=>o.SoTien).GetValueOrDefault(),
                                   TienChi = null,
                                   TenKH = p.NguoiNop,
                                   TenNV = nv.HoTenNV,

                               }).Union
                               (from p in db.pcPhieuChis
                                join nv in db.tnNhanViens on p.MaNV equals nv.MaNV
                                where SqlMethods.DateDiffDay(_tungay, p.NgayChi) >= 0 & SqlMethods.DateDiffDay(p.NgayChi, _dengnay) >= 0
                                & p.MaTN == _MaTN & p.MaTKNH == null
                                select new SoQuyItem
                                {
                                    NgayHachToan = p.NgayChi,
                                    NgayCT = p.NgayChi,
                                    SoPT = "",
                                    SoPC = p.SoPC,
                                    DienGiai = p.LyDo,
                                    TienThu = null,
                                    TienChi = p.pcChiTiets.Sum(o => o.SoTien).GetValueOrDefault(),
                                    TenKH = p.NguoiNhan,
                                    TenNV = nv.HoTenNV,
                                }).ToList();
        }

        private void cTon_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell cell = (XRTableCell)sender;
            DuDK += ((decimal?)GetCurrentColumnValue("TienThu")).GetValueOrDefault();
            DuDK -= ((decimal?)GetCurrentColumnValue("TienChi")).GetValueOrDefault();
            cell.Text = cSumTon.Text = string.Format("{0:#,0.##}", DuDK);
        }

        class SoQuyItem
        {
            public DateTime? NgayHachToan { get; set; }
            public DateTime? NgayCT { get; set; }
            public string SoPT { get; set; }
            public string SoPC { get; set; }
            public string DienGiai { get; set; }
            public decimal? TienThu { get; set; }
            public decimal? TienChi { get; set; }
            public string TenKH { get; set; }
            public string TenNV { get; set; }
        }

    }
}
