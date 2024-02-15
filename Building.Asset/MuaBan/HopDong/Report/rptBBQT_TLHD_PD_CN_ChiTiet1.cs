
using DevExpress.XtraReports.UI;

using Library;

namespace TaiSan.BanHang.Report
{
    public partial class rptBBQT_TLHD_PD_CN_ChiTiet1 : XtraReport
    {
        public rptBBQT_TLHD_PD_CN_ChiTiet1()
        {
            InitializeComponent();

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
