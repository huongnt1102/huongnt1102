using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Library
{
    public partial class rptInBieuMau : DevExpress.XtraReports.UI.XtraReport
    {
        public rptInBieuMau(string rtfText)
        {
            InitializeComponent();

            //Library.HeThongCls.UserLogin usrlogin = new Library.HeThongCls.UserLogin();
            
            rtHeader.Html = rtfText;

            //xrBarCode1.Text = usrlogin.HashPassword(barcode).Substring(0, 15);
        }

    }
}
