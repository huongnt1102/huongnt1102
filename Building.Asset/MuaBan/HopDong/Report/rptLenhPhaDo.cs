
using DevExpress.XtraReports.UI;


namespace TaiSan.BanHang.Report
{
    public partial class rptLenhPhaDo : XtraReport
    {
        public rptLenhPhaDo()
        {
            InitializeComponent();

            #region Sub report

            rptLenhPhaDo_ChiTiet ct = new rptLenhPhaDo_ChiTiet();
            subXuatVatTu.ReportSource = ct;
            #endregion
        }
    }
}
