using System;
using DevExpress.XtraReports.UI;
using Library;

namespace TaiSan.DeXuat.Report
{
    public partial class rptPhieuDeXuat : XtraReport
    {
        public rptPhieuDeXuat(string maPhieu)
        {
            InitializeComponent();

            using (var db = new MasterDataContext())
            {
                try
                {
                    
                }
                catch (Exception)
                {
                    //throw ex;
                }
            }
        }

    }
}
