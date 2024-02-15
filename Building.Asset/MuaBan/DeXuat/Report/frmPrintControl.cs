namespace TaiSan.DeXuat.Report
{
    public partial class frmPrintControl : DevExpress.XtraEditors.XtraForm
    {
        public frmPrintControl(string maPhieu)
        {
            InitializeComponent();

            var rptxk = new rptPhieuDeXuat(maPhieu);
            BSprintControl.PrintingSystem = rptxk.PrintingSystem;
            rptxk.CreateDocument();
        }
    }
}