using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
using Library;
using System.Collections.Generic;
using System.Data.Linq.SqlClient;
using DevExpress.Data.PivotGrid;

namespace LandSoftBuilding.Report
{
    public partial class rptBaoCaoXe : DevExpress.XtraReports.UI.XtraReport
    {
        byte MaTN;
        MasterDataContext db = new MasterDataContext();

        public rptBaoCaoXe(DateTime _TuNgay, DateTime _DenNgay, byte _MaTN)
        {
            InitializeComponent();
            cThoiGian.Text = string.Format("Từ ngày {0:dd/MM/yyy} - Đến ngày {1:dd/MM/yyy}", _TuNgay, _DenNgay);
            //cTieuDeSoThang.Text = string.Format("Số tháng làm tròn tính đến ngày {0:dd/MM/yyy}", _DenNgay);
            this.MaTN = _MaTN;

            #region DataBindings
            cSTT.DataBindings.Add("Text", null, "STT");
            cMaKH.DataBindings.Add("Text", null, "KyHieu");
            cTenKH.DataBindings.Add("Text", null, "TenKH");
            cDKOto.DataBindings.Add("Text", null, "DKOto", "{0:#,0.##}");
            cHuyOto.DataBindings.Add("Text", null, "HuyOto", "{0:#,0.##}");
            cTongOto.DataBindings.Add("Text", null, "TongOto", "{0:#,0.##}");
            cDKXeMay.DataBindings.Add("Text", null, "DKXeMay", "{0:#,0.##}");
            cHuyXeMay.DataBindings.Add("Text", null, "HuyXeMay", "{0:#,0.##}");
            cTongXeMay.DataBindings.Add("Text", null, "TongXeMay", "{0:#,0.##}");
            cDKXeDapDien.DataBindings.Add("Text", null, "DKXeDapDien", "{0:#,0.##}");
            cHuyXeDapDien.DataBindings.Add("Text", null, "HuyXeDapDien", "{0:#,0.##}");
            cTongXeDapDien.DataBindings.Add("Text", null, "TongXeDapDien", "{0:#,0.##}");
            cDKXeDap.DataBindings.Add("Text", null, "DKXeDap", "{0:#,0.##}");
            cHuyXeDap.DataBindings.Add("Text", null, "HuyXeDap", "{0:#,0.##}");
            cTongXeDap.DataBindings.Add("Text", null, "TongXeDap", "{0:#,0.##}");


            cSumDKOto.DataBindings.Add("Text", null, "DKOto");
            cSumHuyOto.DataBindings.Add("Text", null, "HuyOto");
            cSumTongOto.DataBindings.Add("Text", null, "TongOto");
            cSumDKXeMay.DataBindings.Add("Text", null, "DKXeMay");
            cSumHuyXeMay.DataBindings.Add("Text", null, "HuyXeMay");
            cSumTongXeMay.DataBindings.Add("Text", null, "TongXeMay");
            cSumDKXeDapDien.DataBindings.Add("Text", null, "DKXeDapDien");
            cSumHuyXeDapDien.DataBindings.Add("Text", null, "HuyXeDapDien");
            cSumTongXeDapDien.DataBindings.Add("Text", null, "TongXeDapDien");
            cSumDKXeDap.DataBindings.Add("Text", null, "DKXeDap");
            cSumHuyXeDap.DataBindings.Add("Text", null, "HuyXeDap");
            cSumTongXeDap.DataBindings.Add("Text", null, "TongXeDap");

            cSumDKOto.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,0.##}");
            cSumHuyOto.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,0.##}");
            cSumTongOto.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,0.##}");
            cSumDKXeMay.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,0.##}");
            cSumHuyXeMay.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,0.##}");
            cSumTongXeMay.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,0.##}");
            cSumDKXeDap.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,0.##}");
            cSumHuyXeDap.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,0.##}");
            cSumTongXeDap.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,0.##}");
            cSumDKXeDapDien.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,0.##}");
            cSumHuyXeDapDien.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,0.##}");
            cSumTongXeDapDien.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,0.##}");

            //.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,0.##}");
            #endregion

            var objTheXe = (from tx in db.dvgxTheXes
                            join kh in db.tnKhachHangs on tx.MaKH equals kh.MaKH
                            join lx in db.dvgxLoaiXes on tx.MaLX equals lx.MaLX
                            where tx.MaTN == this.MaTN & ( SqlMethods.DateDiffDay(tx.NgayDK, _DenNgay) >= 0 | SqlMethods.DateDiffDay(tx.NgayNgungSD, _DenNgay) >= 0)
                            //& (SqlMethods.DateDiffDay(_TuNgay, tx.NgayDK) >= 0
                            //& SqlMethods.DateDiffDay(tx.NgayDK, _DenNgay) >= 0) | (SqlMethods.DateDiffDay(_TuNgay, tx.NgayNgungSD) >= 0
                            //& SqlMethods.DateDiffDay(tx.NgayNgungSD, _DenNgay) >= 0)
                            group tx by new {kh.MaKH,kh.HoKH,kh.TenKH,kh.CtyTen,kh.KyHieu,kh.IsCaNhan} into gr
                            select new
                            {
                               gr.Key.KyHieu,
                               TenKH = gr.Key.IsCaNhan == true ? (gr.Key.HoKH + " " + gr.Key.TenKH) : gr.Key.CtyTen,
                               DKOto = gr.Count(o => o.dvgxLoaiXe.MaNX == 3 & SqlMethods.DateDiffDay(o.NgayDK, _DenNgay) >= 0),
                               HuyOto = gr.Count(o => o.dvgxLoaiXe.MaNX == 3 & o.NgungSuDung.GetValueOrDefault() & SqlMethods.DateDiffDay(o.NgayNgungSD, _DenNgay) >= 0),
                               DKXeMay = gr.Count(o => o.dvgxLoaiXe.MaNX == 2 & SqlMethods.DateDiffDay(o.NgayDK, _DenNgay) >= 0),
                               HuyXeMay = gr.Count(o => o.dvgxLoaiXe.MaNX == 2 & o.NgungSuDung.GetValueOrDefault() & SqlMethods.DateDiffDay(o.NgayNgungSD, _DenNgay) >= 0),
                               DKXeDap = gr.Count(o => SqlMethods.Like(o.dvgxLoaiXe.TenLX.ToLower(), "%xe đạp thường%") & SqlMethods.DateDiffDay(o.NgayDK, _DenNgay) >= 0),
                               HuyXeDap = gr.Count(o => SqlMethods.Like(o.dvgxLoaiXe.TenLX.ToLower(), "%xe đạp thường%") & o.NgungSuDung.GetValueOrDefault() & SqlMethods.DateDiffDay(o.NgayNgungSD, _DenNgay) >= 0),
                               DKXeDapDien = gr.Count(o => SqlMethods.Like(o.dvgxLoaiXe.TenLX.ToLower(), "%xe đạp điện%") & SqlMethods.DateDiffDay(o.NgayDK, _DenNgay) >= 0),
                               HuyXeDapDien = gr.Count(o => SqlMethods.Like(o.dvgxLoaiXe.TenLX.ToLower(), "%xe đạp điện%") & o.NgungSuDung.GetValueOrDefault() & SqlMethods.DateDiffDay(o.NgayNgungSD, _DenNgay) >= 0),
                            }).ToList()
                              .AsEnumerable()
                              .Select((p, Index) => new
                              {
                                  STT = Index +1,
                                  p.KyHieu,
                                  p.TenKH,
                                  p.DKOto,
                                  p.HuyOto,
                                  TongOto = p.DKOto - p.HuyOto,
                                  p.DKXeMay,
                                  p.HuyXeMay,
                                  TongXeMay = p.DKXeMay - p.HuyXeMay,
                                  p.DKXeDap,
                                  p.HuyXeDap,
                                  TongXeDap = p.DKXeDap - p.HuyXeDap,
                                  p.DKXeDapDien,
                                  p.HuyXeDapDien,
                                  TongXeDapDien = p.DKXeDapDien - p.HuyXeDapDien,
                              }).ToList();
            this.DataSource = objTheXe;
        }

        double TinhSoThang(DateTime? NgayDK, DateTime _DenNgay)
        {
            double SoThang = (double)SqlMethods.DateDiffMonth(NgayDK, _DenNgay);
            SoThang = SoThang == 0 ? SoThang : SoThang - 1;
            SoThang = NgayDK.Value.Day > 15 ? SoThang + 0.5 : SoThang + 1;
            return SoThang;
        }
    }
}
