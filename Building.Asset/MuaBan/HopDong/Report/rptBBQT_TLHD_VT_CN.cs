using DevExpress.XtraReports.UI;


namespace TaiSan.BanHang.Report
{
    public partial class rptBBQT_TLHD_VT_CN : XtraReport
    {
        public rptBBQT_TLHD_VT_CN()
        {
            InitializeComponent();

            #region Sub report

            rptBBQT_TLHD_VT_CN_ChiTiet2 ct = new rptBBQT_TLHD_VT_CN_ChiTiet2();
            subXuatVatTu.ReportSource = ct;

            rptBBQT_TLHD_PD_CN_ChiTiet2 ct2 = new rptBBQT_TLHD_PD_CN_ChiTiet2();
            subPhaDo.ReportSource = ct2;
            #endregion

            try
            {
                
            }
            catch { }
        }
    }
}
