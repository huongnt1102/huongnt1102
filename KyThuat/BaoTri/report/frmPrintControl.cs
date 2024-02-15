using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace KyThuat.BaoTri.report
{
    public partial class frmPrintControl : DevExpress.XtraEditors.XtraForm
    {
        public frmPrintControl(int MaBaoTri, int loaiphieu)
        {
            InitializeComponent();
            switch (loaiphieu)
            {
                case 1:
                    rptGiayBaoTri rpt = new rptGiayBaoTri(MaBaoTri);
                    BSprintControl.PrintingSystem = rpt.PrintingSystem;
                    rpt.CreateDocument();
                    break;

                case 2:
                    rptMauChiTiet rpt1 = new rptMauChiTiet(MaBaoTri);
                    BSprintControl.PrintingSystem = rpt1.PrintingSystem;
                    rpt1.CreateDocument();
                    break;
                default:
                    break;
            }
            
           
        }
    }
}