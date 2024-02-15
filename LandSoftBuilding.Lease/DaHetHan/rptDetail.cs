using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Data;
using DevExpress.XtraReports.UI;
using System.Linq;
using System.Data.Linq.SqlClient;
using System.IO;
using System.Threading;
using DevExpress.Data;
using DevExpress.Data.PivotGrid;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;
using Library;


namespace LandSoftBuilding.Lease.DaHetHan
{
    public partial class rptDetail : XtraReport
    {
        byte MaTN;
        public rptDetail(byte iMaTN)
        {
            InitializeComponent();
            MaTN = iMaTN;
            //this.Visible = false;
        }

        public void loadData(int ngay, object _DataSource)
        {
            //lblThoiGian.Text = string.Format("(Từ ngày {0: dd/MM/yyyy} - Đến ngày {1:dd/MM/yyyy})", tuNgay, denNgay);
            var db = new MasterDataContext();
            try
            {
                var objTN = (from tn in db.tnToaNhas
                             where tn.MaTN == this.MaTN
                             select new { tn.TenTN, tn.CongTyQuanLy, tn.Logo, tn.DiaChi })
                             .FirstOrDefault();
                picLogo.ImageUrl = objTN.Logo;
                lblDiaChi.Text = objTN.DiaChi;
                cTenCT.Text = objTN.TenTN;
                var str = lblThoiGian.Text;
                str = str.Replace("[ngay]", ngay.ToString());
                lblThoiGian.Text = str;
                
                xrTableCell30.DataBindings.Add("Text", null, "NgayHL", "{0:dd/MM/yyyy}");
                xrTableCell31.DataBindings.Add("Text", null, "NgayHH", "{0:dd/MM/yyyy}");
                xrTableCell35.DataBindings.Add("Text", null, "GiaThue", "{0:N0}");
                xrTableCell36.DataBindings.Add("Text", null, "TienCoc", "{0:N0}");
                xrTableCell39.DataBindings.Add("Text", null, "GiaThueQD", "{0:N0}");
                xrTableCell40.DataBindings.Add("Text", null, "TienCocQD", "{0:N0}");
                xrTableCell32.DataBindings.Add("Text", null, "ThoiHan", "{0:N0}");

                xrTableCell47.DataBindings.Add("Text", null, "NgayBG", "{0:dd/MM/yyyy}");
                xrTableCell49.DataBindings.Add("Text", null, "NgaySua", "{0:dd/MM/yyyy}");
                xrTableCell51.DataBindings.Add("Text", null, "NgayNhap", "{0:dd/MM/yyyy}");

                xrTableCell38.DataBindings.Add("Text", null, "TyGia", "{0:N0}");
                xrTableCell41.DataBindings.Add("Text", null, "TyGiaHD", "{0:N0}");
                xrTableCell42.DataBindings.Add("Text", null, "TyGiaTT", "{0:N0}");

                // SUM
                xrTableCell53.DataBindings.Add("Text", null, "GiaThue", "{0:N0}");
                xrTableCell53.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:N0}");

                xrTableCell54.DataBindings.Add("Text", null, "TienCoc", "{0:N0}");
                xrTableCell54.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:N0}");

                xrTableCell56.DataBindings.Add("Text", null, "GiaThueQD", "{0:N0}");
                xrTableCell56.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:N0}");

                xrTableCell57.DataBindings.Add("Text", null, "TienCocQD", "{0:N0}");
                xrTableCell57.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:N0}");
                // Load Data
                this.DataSource = _DataSource;

                // export excel
                Library.Commoncls.ExportExcel(this);
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
