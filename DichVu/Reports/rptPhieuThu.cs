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
    public partial class rptPhieuThu : DevExpress.XtraReports.UI.XtraReport
    {
        byte MaTN;
        public rptPhieuThu(byte _MaTN)
        {
            InitializeComponent();

            this.MaTN = _MaTN;
            //Load layout           
            Library.frmPrintControl.LoadLayout(this, 22, this.MaTN);
        }

        public void LoadData(string _KyBC, DateTime _TuNgay, DateTime _DenNgay, System.IO.MemoryStream _DataSoure)
        {
            var db = new MasterDataContext();
            try
            {
                var objTN = (from tn in db.tnToaNhas
                             where tn.MaTN == this.MaTN
                             select new { tn.TenTN, tn.CongTyQuanLy, tn.Logo })
                             .FirstOrDefault();
                picLogo.ImageUrl = objTN.Logo;

                var str = cThoiGian.Text;
                str = str.Replace("[KyBC]", _KyBC);
                str = str.Replace("[TuNgay]", _TuNgay.ToString("dd/MM/yyyy"));
                str = str.Replace("[DenNgay]", _DenNgay.ToString("dd/MM/yyyy"));
                cThoiGian.Text = str;

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

        private void pvData_FieldValueDisplayText(object sender, DevExpress.XtraReports.UI.PivotGrid.PivotFieldDisplayTextEventArgs e)
        {
            if (e.ValueType == DevExpress.XtraPivotGrid.PivotGridValueType.Total)
                e.DisplayText = string.Format("{0} ({1})", e.Value, "Tổng");  
            else if (e.ValueType == DevExpress.XtraPivotGrid.PivotGridValueType.GrandTotal)
                e.DisplayText = "Tổng cộng";
        }

    }
}
