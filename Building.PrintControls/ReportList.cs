using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports.UserDesigner;
using System.Linq;
//using DIPCRM.Support;
//using DIPCRM.Support;
using Library;


namespace Building.PrintControls
{
    public partial class ReportList : DevExpress.XtraEditors.XtraUserControl
    {
        public ReportList()
        {
            InitializeComponent();

            itemPrintview.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemPrintview_ItemClick);
            itemEdit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemEdit_ItemClick);
            gvReport.DoubleClick += new EventHandler(gvReport_DoubleClick);
        }        

        [Category("Report"), Description("The event occurs when click printview button")]
        public event PrintviewEventHandler PrintviewClick;

        [Category("Report"), Description("The event occurs when click edit button")]
        public event EditButtonClickEventHandler EditClick;

        [Category("Report"), Description("The event occurs when saving tamplate")]
        public event SavingTemplateEventHandler SavingTemplate;

        [Category("Report"), Description("The event occurs when ToaNha EditValue Changed")]
        public event ToaNhaEditValueChangedEventHandler ToaNhaEditValueChanged;

        public byte MaTN { get; set; }

        public void SetItemEnabled(ReportItem item, bool enabled)
        {
            switch (item)
            {
                case ReportItem.PrintView: itemPrintview.Enabled = enabled; break;
                case ReportItem.EditItem: itemEdit.Enabled = enabled; break;
            }
        }
                
        void itemPrintview_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var id = (int?)gvReport.GetFocusedRowCellValue("ID");
            if (id == null)
            {
                DialogBox.Error("Vui lòng chọn mẫu báo cáo");
                return;
            }
            if (PrintviewClick != null)
            {
                PrintviewEventArgs objPrintview = new PrintviewEventArgs(id, gvReport.GetFocusedRowCellDisplayText("Name"));
                this.PrintviewClick(this, objPrintview);
            }
        }

        void gvReport_DoubleClick(object sender, EventArgs e)
        {
            var id = (int?)gvReport.GetFocusedRowCellValue("ID");
            if (id == null) return;
            if (PrintviewClick != null)
            {
                PrintviewEventArgs objPrintview = new PrintviewEventArgs(id, gvReport.GetFocusedRowCellDisplayText("Name"));
                this.PrintviewClick(this, objPrintview);
            }
        }

        void itemEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var id = (int?)gvReport.GetFocusedRowCellValue("ID");
            if (id == null)
            {
                DialogBox.Error("Vui lòng chọn mẫu báo cáo");
                return;
            }

            try
            {
                if (EditClick == null) return;

                EditButtonClickEventArgs objEdit = new EditButtonClickEventArgs(id);
                this.EditClick(this, objEdit);
                
                if (objEdit.Report == null) return;

                using (XRDesignFormEx designer = new XRDesignFormEx())
                {
                    if (this.SavingTemplate != null)
                    {
                        designer.DesignPanel.AddCommandHandler(new SaveCommandHandler(designer.DesignPanel, id, this.SavingTemplate));
                    }
                    designer.OpenReport(objEdit.Report);
                    designer.WindowState = FormWindowState.Maximized;
                    designer.ShowDialog(this);
                }
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
        }

        private void itemToaNha_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                this.MaTN = (byte)itemToaNha.EditValue;

                if (this.ToaNhaEditValueChanged == null) return;

                ToaNhaEditValueChangedEventArgs objTN = new ToaNhaEditValueChangedEventArgs();
                this.ToaNhaEditValueChanged(this, objTN);
                gcReport.DataSource = objTN.DataSource;
                gvReport.ExpandAllGroups();
            }
            catch { }
        }

        private void ReportList_Load(object sender, EventArgs e)
        {
            try
            {
                lkToaNha.DataSource = Common.TowerList;
                itemToaNha.EditValue = Common.User.MaTN;
            }
            catch { }
        }

        private void itemResetMau_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var id = (int?)gvReport.GetFocusedRowCellValue("ID");
            if (id == null)
            {
                DialogBox.Error("Vui lòng chọn mẫu báo cáo");
                return;
            }
            if (DialogBox.Question("Bạn muốn xóa hết những design trước đó?") == DialogResult.No) return;

            var db = new Library.MasterDataContext();
            var reportToaNha = db.rptReports_ToaNhas.FirstOrDefault(_ => _.MaTN == (byte)itemToaNha.EditValue & _.ReportID == id);
            if (reportToaNha != null) reportToaNha.Layout = null;
            db.SubmitChanges();
            Library.DialogBox.Success();
        }
    }
}
