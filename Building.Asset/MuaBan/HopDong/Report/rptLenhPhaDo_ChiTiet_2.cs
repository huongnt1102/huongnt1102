
using DevExpress.XtraReports.UI;


using Library;

namespace TaiSan.BanHang.Report
{
    public partial class rptLenhPhaDo_ChiTiet_2 : XtraReport
    {
        public rptLenhPhaDo_ChiTiet_2()
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
