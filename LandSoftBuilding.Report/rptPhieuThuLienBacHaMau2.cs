namespace LandSoftBuilding.Report
{
    public partial class rptPhieuThuLienBacHaMau2 : DevExpress.XtraReports.UI.XtraReport
    {
        public rptPhieuThuLienBacHaMau2(int id, byte maTn)
        {
            InitializeComponent();
            xrSubreport1.ReportSource = new rptPhieuThuBacHaMau2( id,  maTn,1);
            xrSubreport2.ReportSource = new rptPhieuThuBacHaMau2( id,  maTn,2);
        }
    }
}
