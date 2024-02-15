using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace TaiSan.DatHang.Report
{
    public partial class frmPrintControl : DevExpress.XtraEditors.XtraForm
    {
        public frmPrintControl(string MaPhieu)
        {
            InitializeComponent();

            rptDonDatHang rptxk = new rptDonDatHang(MaPhieu);
            BSprintControl.PrintingSystem = rptxk.PrintingSystem;
            rptxk.CreateDocument();
        }
    }
}