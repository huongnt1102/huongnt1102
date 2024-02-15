using System;
using System.Drawing;
using System.Collections;

using DevExpress.XtraReports.UI;

using Library;

namespace TaiSan.BanHang.Report
{
    public partial class rptBienBanNghiemThu_ChiTiet : XtraReport
    {
        public rptBienBanNghiemThu_ChiTiet()
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
