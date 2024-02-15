using DevExpress.XtraReports.UI;
using Library;

namespace TaiSan.BanHang.Report
{
    public partial class rptHopDongCaNhan_PhaDoVanChuyen_Sub : XtraReport
    {
        public rptHopDongCaNhan_PhaDoVanChuyen_Sub()
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
