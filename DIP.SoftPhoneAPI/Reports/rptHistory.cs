using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace DIP.SoftPhoneAPI.Reports
{
    public partial class rptHistory : DevExpress.XtraReports.UI.XtraReport
    {
        public rptHistory(DateTime fromDate, DateTime toDate, string type, string status, string staff, string trunks)
        {
            InitializeComponent();

            #region Databing
            cLoai.DataBindings.Add("Text", null, "type");
            cTrangThai.DataBindings.Add("Text", null, "status");
            cNgayGoi.DataBindings.Add("Text", null, "calldate");
            cSoNguon.DataBindings.Add("Text", null, "src");
            cSoDich.DataBindings.Add("Text", null, "dst");
            cDamThoai.DataBindings.Add("Text", null, "billsec");
            cTongThoiGian.DataBindings.Add("Text", null, "duration");
            cNhanVien.DataBindings.Add("Text", null, "staff");
            cDauSo.DataBindings.Add("Text", null, "trunk");

            cSumNgayGoi.DataBindings.Add("Text", null, "calldate");
            cSumNgayGoi.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Count, "{0} cuộc gọi");          
            cSumDamThoai.DataBindings.Add("Text", null, "billsec");
            cSumDamThoai.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:n0}");
            cSumThoiGian.DataBindings.Add("Text", null, "duration");
            cSumThoiGian.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:n0}");
            #endregion

            try
            {
                cThoiGian.Text = string.Format("(Từ ngày {0: dd/MM/yyyy} - Đến ngày {1:dd/MM/yyyy})", fromDate, toDate);

                this.DataSource = HistoryCls.GetHistory(fromDate, toDate, type, status, staff, trunks);
            }
            catch { }
        }
    }
}
