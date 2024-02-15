using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace DichVu.GiuXe
{
    public partial class PhieuDangKy : DevExpress.XtraReports.UI.XtraReport
    {
        public PhieuDangKy(int? MaCapThe)
        {
            InitializeComponent();

            xrSub1.ReportSource = xrSub2.ReportSource = new SubPhieuDangKy(MaCapThe);
        }

    }
}
