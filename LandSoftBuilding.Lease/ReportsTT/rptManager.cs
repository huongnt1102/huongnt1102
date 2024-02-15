using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
using Library;
using System.Collections.Generic;
using System.Data.Linq.SqlClient;
using System.Threading;
using DevExpress.Data.PivotGrid;

namespace LandSoftBuilding.Lease.ReportsTT
{
    public partial class rptManager : DevExpress.XtraReports.UI.XtraReport
    {
        byte MaTN;

        public rptManager(byte _MaTN)
        {
            
            InitializeComponent();
            this.MaTN = _MaTN;
            
        }
        
        public void LoadData(string _KyBC, DateTime _TuNgay, DateTime _DenNgay, System.IO.MemoryStream _DataSoure)
        {
            var db = new MasterDataContext();
            try
            {
                var objTN = (from tn in db.tnToaNhas
                             where tn.MaTN == this.MaTN
                             select new { tn.TenTN, tn.CongTyQuanLy, tn.Logo,tn.DiaChi })
                             .FirstOrDefault();
                picLogo.ImageUrl = objTN.Logo;
                lblDiaChi.Text = objTN.DiaChi;
                var str = cThoiGian.Text;
                str = str.Replace("[KyBC]", _KyBC);
                str = str.Replace("[TuNgay]", _TuNgay.ToString("dd/MM/yyyy"));
                str = str.Replace("[DenNgay]", _DenNgay.ToString("dd/MM/yyyy"));
                cThoiGian.Text = str;

                pvCongNo.DataSource = new PivotFileDataSource(_DataSoure);
                

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
