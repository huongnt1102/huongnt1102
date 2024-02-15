using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Linq.SqlClient;
using System.Linq;
using DevExpress.XtraReports.UI;
using Library;

namespace LandSoftBuilding.Report
{
    public partial class SubrptBaoCaoCacKhoanPhiDV : DevExpress.XtraReports.UI.XtraReport
    {
        public SubrptBaoCaoCacKhoanPhiDV()
        {
            InitializeComponent();
            cTenKH.DataBindings.Add("Text", null, "TenTN");
            cTienChuyenKhoan.DataBindings.Add("Text", null, "TienCK", "{0:#,0.##}");
            cTienMat.DataBindings.Add("Text", null, "TienMat", "{0:#,0.##}");
            cSum.DataBindings.Add("Text", null, "TongTien", "{0:#,0.##}");

            cSumSum.DataBindings.Add("Text", null, "TongTien", "{0:#,0.##}");
            cSumSum.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,0.##}");
            cSumCK.DataBindings.Add("Text", null, "TienCK", "{0:#,0.##}");
            cSumCK.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,0.##}");
            cSumTienMat.DataBindings.Add("Text", null, "TienMat", "{0:#,0.##}");
            cSumTienMat.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,0.##}");
        }
       
        private MasterDataContext db = new MasterDataContext();
        List<byte?> GetToaNha()
        {
            var ltToaNha = new List<byte?>();
            //var arrMaTN = (itemToaNha.EditValue ?? "").ToString().Split(',');
            var arrMaTN = strTN.Split(',');
            foreach (var s in arrMaTN)
                if (s != "")
                    ltToaNha.Add(byte.Parse(s.Trim()));
            return ltToaNha;
        }

        private string strTN = "";
        public void LoadData(string MaTN, DateTime _TuNgay,DateTime _DenNgay)
        {
            strTN = MaTN;
            var ltToaNha = this.GetToaNha();
            this.DataSource = (from ct in db.ptChiTietPhieuThus
                         join pt in db.ptPhieuThus on ct.MaPT equals pt.ID
                               join tn in db.tnToaNhas on pt.MaTN equals tn.MaTN
                         join hd in db.dvHoaDons on new { ct.TableName, ct.LinkID } equals new { TableName = "dvHoaDon", LinkID = (long?)hd.ID }
                         where ltToaNha.Contains(pt.MaTN) & SqlMethods.DateDiffDay(_TuNgay, pt.NgayThu) >= 0 & SqlMethods.DateDiffDay(pt.NgayThu, _DenNgay) >= 0
                                & (hd.MaLDV == 35 | hd.MaLDV == (int)MaLDVs.Nuoc | hd.MaLDV == (int)MaLDVs.PGX | hd.MaLDV == (int)MaLDVs.Dien)

                               group new { ct,pt,tn } by new { pt.MaTN,tn.TenTN } into gr
                         select new
                         {

                             gr.Key.MaTN,
                             gr.Key.TenTN,

                            
                             TienCK = (decimal?)gr.Sum(p => p.pt.MaTKNH!=null ? p.ct.SoTien:0).GetValueOrDefault(),
                             TienMat = (decimal?)gr.Sum(p => p.pt.MaTKNH == null ? p.ct.SoTien : 0).GetValueOrDefault(),
                         }).Select(p => new { p.MaTN, p.TenTN, p.TienCK, p.TienMat, TongTien = p.TienCK+ p.TienMat }).ToList();
        }
    }
}
