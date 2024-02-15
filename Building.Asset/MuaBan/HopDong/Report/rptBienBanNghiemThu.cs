using DevExpress.XtraReports.UI;
using Library;

namespace TaiSan.BanHang.Report
{
    public partial class rptBienBanNghiemThu : XtraReport
    {
        public rptBienBanNghiemThu()
        {
            InitializeComponent();

            #region Sub report

            rptBienBanNghiemThu_ChiTiet ct = new rptBienBanNghiemThu_ChiTiet();
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
