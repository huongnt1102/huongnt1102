using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
using System.Data.Linq.SqlClient;
using Library;

namespace TaiSan.BanHang.Report
{
    public partial class rptLenhPhaDo_ChiTiet_1 : XtraReport
    {
        public rptLenhPhaDo_ChiTiet_1()
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

        private void subKhoiLuong_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

                subKhoiLuong.ReportSource = new rptLenhPhaDo_ChiTiet_2();

            
        }
    }
}
