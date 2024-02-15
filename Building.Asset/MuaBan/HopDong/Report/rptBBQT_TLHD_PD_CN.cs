using System;
using System.Drawing;
using System.Collections;
using DevExpress.XtraReports.UI;
using Library;

namespace TaiSan.BanHang.Report
{
    public partial class rptBBQT_TLHD_PD_CN : XtraReport
    {
        public rptBBQT_TLHD_PD_CN()
        {
            InitializeComponent();

            #region Sub report

            rptBBQT_TLHD_PD_CN_ChiTiet1 ct = new rptBBQT_TLHD_PD_CN_ChiTiet1();
            subXuatVatTu.ReportSource = ct;
            #endregion

            try
            {
                using (var db=new MasterDataContext())
                {
                    
                }
            }
            catch { }
        }
    }
}
