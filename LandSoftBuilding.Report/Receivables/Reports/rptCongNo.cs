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

namespace LandSoftBuilding.Receivables.Reports
{
    public partial class rptCongNo : DevExpress.XtraReports.UI.XtraReport
    {
        byte MaTN;

        public rptCongNo(byte _MaTN)
        {
            InitializeComponent();

            this.MaTN = _MaTN;
            //Load layout           
            Library.frmPrintControl.LoadLayout(this, 20, this.MaTN);
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


        bool isLocked;
        private void SetCustomPageSize()
        {
            if (!isLocked)
            {
                isLocked = true;
                PrintingSystem.Document.AutoFitToPagesWidth = 1;
                float scaleFactor = PrintingSystem.Document.ScaleFactor;
                DevExpress.XtraPrinting.XtraPageSettingsBase pageSettings = PrintingSystem.PageSettings;
                Size customPaperSize = Size.Round(new SizeF(pageSettings.UsablePageSize.Width / scaleFactor + pageSettings.Margins.Left + pageSettings.Margins.Right, pageSettings.Bounds.Height));
                DevExpress.XtraPrinting.XtraPageSettingsBase.ApplyPageSettings(pageSettings, System.Drawing.Printing.PaperKind.Custom, customPaperSize, pageSettings.Margins, pageSettings.MinMargins, false);
                PrintingSystem.Document.AutoFitToPagesWidth = 0;
                PrintingSystem.Document.ScaleFactor = 1;
            }
        }      

        private void rptCongNo_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //pvCongNo.BestFit();
            isLocked = false;
        }

        private void rptCongNo_AfterPrint(object sender, EventArgs e)
        {
            //SetCustomPageSize();
        }

        private void pvCongNo_FieldValueDisplayText(object sender, DevExpress.XtraReports.UI.PivotGrid.PivotFieldDisplayTextEventArgs e)
        {
            if (e.ValueType == DevExpress.XtraPivotGrid.PivotGridValueType.Total)
                e.DisplayText = "Tổng " + e.Value;
            else if (e.ValueType == DevExpress.XtraPivotGrid.PivotGridValueType.GrandTotal)
                e.DisplayText = "Tổng cộng";
        }
    }
}
