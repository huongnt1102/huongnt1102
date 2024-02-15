using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
using Library;
using System.Collections.Generic;
using System.Data.Linq.SqlClient;
using DevExpress.Data.PivotGrid;

namespace DichVu.Reports
{
    public partial class rptTheXe : DevExpress.XtraReports.UI.XtraReport
    {
        byte MaTN;

        public rptTheXe(byte _MaTN)
        {
            InitializeComponent();

            this.MaTN = _MaTN;
            //Load layout           
            Library.frmPrintControl.LoadLayout(this, 29, this.MaTN);
        }

        public void LoadData(System.IO.MemoryStream _DataSoure)
        {
            var db = new MasterDataContext();
            try
            {
                var objTN = (from tn in db.tnToaNhas
                             where tn.MaTN == this.MaTN
                             select new { tn.TenTN, tn.CongTyQuanLy, tn.Logo })
                             .FirstOrDefault();
                picLogo.ImageUrl = objTN.Logo;

                pvData.DataSource = new PivotFileDataSource(_DataSoure);
            }
            catch
            {
            }
            finally
            {
                db.Dispose();
            }
        }
    }
}
