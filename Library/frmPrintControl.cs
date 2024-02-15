using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using Library;

namespace Library
{
    public partial class frmPrintControl : DevExpress.XtraEditors.XtraForm
    {
        public frmPrintControl()
        {
            InitializeComponent();

            printControl1.ReportIDChanged += new Building.PrintControls.ReportIDChangedEventHandler(printControl1_ReportIDChanged);
        }

        public static void LoadLayout(XtraReport rpt, int reportID, byte maTN)
        {
            try
            {
                using (var db = new MasterDataContext())
                {
                    var kt = db.rptReports_ToaNhas.Where(p => p.MaTN == maTN & p.ReportID == reportID).ToList();
                    if (kt.Count() < 0 | kt[0].Layout == null) return;

                    using (var stream = new System.IO.MemoryStream(kt[0].Layout.ToArray()))
                    {
                        rpt.LoadLayout(stream);
                    }
                }
            }
            catch { }
        }

        public int? ReportID
        {
            get { return printControl1.ReportID; }
            set { printControl1.ReportID = value; }
        }

        public XtraReport Report
        {
            get { return printControl1.Report; }
            set { printControl1.Report = value; }
        }

        public Building.PrintControls.PrintFilterForm FilterForm
        {
            get { return printControl1.FilterForm; }
            set { printControl1.FilterForm = value; }
        }

        void printControl1_ReportIDChanged(object sender, Building.PrintControls.ReportIDChangedEventArgs e)
        {
            
        }
    }
}