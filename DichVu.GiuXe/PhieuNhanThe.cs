using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace DichVu.GiuXe
{
    public partial class PhieuNhanThe : DevExpress.XtraReports.UI.XtraReport
    {
        public PhieuNhanThe(int? MaCapThe)
        {
            InitializeComponent();

            xrSub1.ReportSource = xrSub2.ReportSource = new SubPhieuNhanThe(MaCapThe);
        }

    }
}
