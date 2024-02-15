using Library;

namespace KyThuat.SuaChua.ReportTemplate
{
    public partial class frmPrintControl : DevExpress.XtraEditors.XtraForm
    {
        //1 : Phieu thanh toan
        //2 : Phieu nghiem thu
        public frmPrintControl(sckhSuaChua objsc, int Loai)
        {
            InitializeComponent();
            switch (Loai)
            {
                case 1:
                    var rpt = new rptPhieuThanhToan(objsc);
                    BSprintControl.PrintingSystem = rpt.PrintingSystem;
                    rpt.CreateDocument();
                    break;
                case 2:
                    var rptNghiemThu = new rptPhieuNghiemThu(objsc);
                    BSprintControl.PrintingSystem = rptNghiemThu.PrintingSystem;
                    rptNghiemThu.CreateDocument();
                    break;
                default:
                    break;
            }
            
        }
    }
}