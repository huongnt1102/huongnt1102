using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace LandSoftBuildingMain
{
    public partial class frmGoogleMaps : DevExpress.XtraEditors.XtraForm
    {
        public frmGoogleMaps()
        {
            InitializeComponent();
        }

        private void frmGoogleMaps_Load(object sender, EventArgs e)
        {
            webBrowser1.Navigate("https://www.google.com/maps/preview#!q=380+chu+văn+an");  
        }
    }
}