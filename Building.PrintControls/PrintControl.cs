using System;
using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraReports.UI;

namespace Building.PrintControls
{
    public partial class PrintControl : XtraUserControl
    {
        public PrintControl()
        {
            InitializeComponent();

            itemRefresh.ItemClick += itemRefresh_ItemClick;
            itemEdit.ItemClick += itemEdit_ItemClick;
            Load += PrintControl_Load;
        }

        private PrintFilterForm _filterForm;
        private int? _reportId = null;
        private XtraReport _rpt = new XtraReport();

        [Category("Report"), Description("The event occurs when ReportID changed")]
        public event ReportIDChangedEventHandler ReportIDChanged;
        public int MaBC { get; set; }
        public byte MaTN { get; set; }
       
        [Category("Report"), Description("ReportID")]
        public int? ReportID
        {
            get
            {
                return _reportId;
            }
            set
            {
                _reportId = value;
                if (ReportIDChanged != null)
                {
                    ReportIDChangedEventArgs objChange = new ReportIDChangedEventArgs(value);
                    this.ReportIDChanged(this, objChange);
                }
            }
        }
        public int IDPhieuthu { get; set; }
        [Category("Report"), Description("FilterForm")]
        public PrintFilterForm FilterForm
        {
            get { return _filterForm; }
            set
            {
                if (value == null) return;
                _filterForm = value;
                _filterForm.FormClosing += _FilterForm_FormClosing;
            }
        }

        [Category("Report"), Description("Report")]
        public XtraReport Report
        {
            get
            {
                return _rpt;
            }
            set
            {
                try
                {
                    _rpt = value;
                    printControl1.PrintingSystem = _rpt.PrintingSystem;
                    _rpt.CreateDocument();
                    Focus();
                }
                catch
                {
                    // ignored
                }
            }
        }

        private void ShowFilterForm()
        {
            try
            {
                FilterForm.PrintControl = this;
                FilterForm.Show(this);
            }
            catch
            {
                // ignored
            }
        }

        private void PrintControl_Load(object sender, EventArgs e)
        {
            ShowFilterForm();
        }

        private void _FilterForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            _filterForm.Hide();
        }

        private void itemRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ShowFilterForm();
        }

        private void itemEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                Report.ShowDesignerDialog();
                Report.CreateDocument();
            }
            catch
            {
                // ignored
            }
        }

        private void printPreviewBarItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //using (var dbo = new MasterDataContext())
            //{
            //    switch (this.MaBC)
            //    {
            //        case 3:
            //            for (int i = 1; i <= 2; i++)
            //            {
            //                //rpt = new LandSoftBuilding.Fund.Input.rptPhieuThu257(IDPhieuthu, MaTN, i);
            //                //rpt.Print();
            //            }

            //            break;

            //    }


            //}
        }
    }
}
