using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraReports.UI;

namespace DIP.SoftPhoneAPI.Reports
{
    public partial class frmHistory : DevExpress.XtraEditors.XtraForm
    {
        public DateTime fromDate = DateTime.Now, toDate = DateTime.Now;
        public string Types = "", Status = "", Staff = "", Trunks = "";

        public frmHistory()
        {
            InitializeComponent();
        }

        void SetDate(int index)
        {
            KyBaoCaoCls objKBC = new KyBaoCaoCls();
            objKBC.Index = index;
            objKBC.SetToDate();

            cmbKyBaoCao.SelectedIndex = index;
            dateTuNgay.EditValue = objKBC.DateFrom;
            dateDenNgay.EditValue = objKBC.DateTo;
        }

        private void frmHistory_Load(object sender, EventArgs e)
        {
            KyBaoCaoCls objKBC = new KyBaoCaoCls();
            objKBC.Initialize(cmbKyBaoCao);
            cmbKyBaoCao.SelectedIndex = 1;

            cmbStaff.Properties.DataSource = HistoryCls.Lines;
            cmbTrunk.Properties.DataSource = HistoryCls.Trunks;

            dateTuNgay.DateTime = fromDate;
            dateDenNgay.DateTime = toDate;
            cmbType.Text = Types;
            cmbStatus.Text = Status;
            cmbStaff.Text = Staff;
            cmbTrunk.Text = Trunks;
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            this.fromDate = dateTuNgay.DateTime;
            this.toDate = dateDenNgay.DateTime;
            this.Types = cmbType.Text;
            this.Status = cmbStatus.Text;
            this.Staff = cmbStaff.Text;
            this.Trunks = cmbTrunk.Text;

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbKyBaoCao_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SetDate(cmbKyBaoCao.SelectedIndex);
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                var rpt = new Reports.rptHistory(dateTuNgay.DateTime, dateDenNgay.DateTime, cmbType.Text, cmbStatus.Text, cmbStaff.Text, cmbTrunk.Text);
                rpt.ShowPreviewDialog();
            }
            catch { }
        }
    }
}