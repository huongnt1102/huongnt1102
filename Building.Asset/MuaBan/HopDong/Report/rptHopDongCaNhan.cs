using DevExpress.XtraReports.UI;
using Library;

namespace TaiSan.BanHang.Report
{
    public partial class rptHopDongCaNhan : XtraReport
    {
        public rptHopDongCaNhan()
        {
            InitializeComponent();

            #region Sub report
            rptHopDongCaNhan_MuaBanVatTu_Sub banVatTuSub = new rptHopDongCaNhan_MuaBanVatTu_Sub();
            subMuaBanVatTu.ReportSource = banVatTuSub;
            rptHopDongCaNhan_PhaDoVanChuyen_Sub phaDoVanChuyenSub = new rptHopDongCaNhan_PhaDoVanChuyen_Sub();
            subPhaDoVanChuyen.ReportSource = phaDoVanChuyenSub;
            #endregion

            try
            {
                using (var db = new MasterDataContext())
                {
                    
                }
            }
            catch { }
        }
    }
}
