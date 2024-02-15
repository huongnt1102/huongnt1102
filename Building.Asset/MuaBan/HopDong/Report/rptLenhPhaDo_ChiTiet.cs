
using DevExpress.XtraReports.UI;

using Library;

namespace TaiSan.BanHang.Report
{
    public partial class rptLenhPhaDo_ChiTiet : XtraReport
    {
        public rptLenhPhaDo_ChiTiet()
        {
            InitializeComponent();

            try
            {
                using (var db=new MasterDataContext())
                {
                    rptLenhPhaDo_ChiTiet_1 ct1 = new rptLenhPhaDo_ChiTiet_1();
                    subPhaDo.ReportSource = ct1;

                }
            }
            catch { }
        }
    }
}
