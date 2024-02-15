using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using System.Xml.Linq;
using DevExpress.XtraReports.UI;

namespace DIP.SoftPhoneAPI
{
    public partial class HistoryEditorDB : DevExpress.XtraEditors.XtraUserControl
    {
        DateTime fromDate = DateTime.Now, toDate = DateTime.Now;
        string Types = "", Status = "", Staff = "", Trunks = "";
 
        public HistoryEditorDB()
        {
            InitializeComponent();
        }

        public event CallHistoryRowClickEventHandler CallHistoryRowClick;
        public event CallHistoryBinDataEventHandler CallHistoryBinData;

        void LoadHistory()
        {
            try
            {
                if (CallHistoryBinData != null)
                {
                    var arg = new CallHistoryBinDataEventArgs();
                    arg.fromDate = fromDate;
                    arg.toDate = toDate;

                    CallHistoryBinData(this, arg);

                    gcCallHistory.DataSource = arg.Result;
                }
                else
                {
                    gcCallHistory.DataSource = HistoryCls.GetHistory(fromDate, toDate, Types, Status, Staff, Trunks);
                }
            }
            catch { }
        }

        void click()
        {
            try
            {
                var status = gvCallHistory.GetFocusedRowCellValue("status") as string;
                itemXem.Enabled = status == "Thành công";
                
                var type = gvCallHistory.GetFocusedRowCellValue("type") as string;
                if (type == null) return;

                var phone = "";
                if (type == "Cuộc gọi đến")
                {
                    phone = gvCallHistory.GetFocusedRowCellValue("src").ToString();
                }
                else
                {
                    phone = gvCallHistory.GetFocusedRowCellValue("dst").ToString();
                }

                if (this.CallHistoryRowClick != null)
                {
                    var args = new CallHistoryRowClickEventArgs();
                    args.PhoneNumber = phone;
                    this.CallHistoryRowClick(this, args);
                    vgCustomer.DataSource = args.Result;
                }
            }
            catch
            {
                vgCustomer.DataSource = null;
            }
        }

        private void ctlManager_Load(object sender, EventArgs e)
        {
            try
            {
                this.LoadHistory();
            }
            catch { }
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var frm = new Reports.frmHistory())
            {
                frm.fromDate = this.fromDate;
                frm.toDate = this.toDate;
                frm.Types = this.Types;
                frm.Status = this.Status;
                frm.Staff = this.Staff;
                frm.Trunks = this.Trunks;
                frm.ShowDialog(this);
                if (frm.DialogResult == DialogResult.OK)
                {
                    this.fromDate = frm.fromDate;
                    this.toDate = frm.toDate;
                    this.Types = frm.Types;
                    this.Status = frm.Status;
                    this.Staff = frm.Staff;
                    this.Trunks = frm.Trunks;
                    LoadHistory();
                }
            }
        }

        private void txtXem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (gvCallHistory.FocusedRowHandle < 0) return;

                var filename = gvCallHistory.GetFocusedRowCellValue("filename").ToString();
                if (filename != "")
                {
                    HistoryCls.ListenAgain(filename, false);
                }
            }
            catch { }
        }

        private void txtTaiVe_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (gvCallHistory.FocusedRowHandle < 0) return;

                var filename = gvCallHistory.GetFocusedRowCellValue("filename").ToString();
                if (filename != "")
                {
                    HistoryCls.ListenAgain(filename, true);
                }
            }
            catch { }
        }
        
        private void itemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                using (var frm = new System.Windows.Forms.SaveFileDialog())
                {
                    frm.Filter = "Excel|*.xls";
                    frm.ShowDialog();
                    if (frm.FileName != "")
                    {
                        gcCallHistory.ExportToXls(frm.FileName);
                        if (MessageBox.Show("Đã xử lý xong, bạn có muốn xem lại không?", "Thông báo", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                            System.Diagnostics.Process.Start(frm.FileName);
                    }
                }
            }
            catch { }
        }

        private void gvCallHistory_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            this.click();
        }

        private void gvCallHistory_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.RowHandle < 0) return;

            if (e.Column.FieldName == "type")
            {
                var typeName = e.CellValue as string;
                switch (typeName)
                {
                    case "Cuộc gọi đến": e.Appearance.BackColor = Color.Blue; break;
                    case "Cuộc gọi đi": e.Appearance.BackColor = Color.Green; break;
                }
            }
            else if (e.Column.FieldName == "status")
            {
                var status = e.CellValue as string;
                switch (status)
                {
                    case "Thành công": e.Appearance.BackColor = Color.Yellow; break;
                    case "Cuộc gọi nhỡ": e.Appearance.BackColor = Color.Red; break;
                    default: e.Appearance.BackColor = Color.Olive; break;
                }
            }
        }

        private void itemPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var rpt = new Reports.rptHistory(fromDate, toDate, this.Types, this.Status, this.Staff, this.Trunks);
            rpt.ShowPreviewDialog();
        }
    }
}
