using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraReports.UI;

namespace TaiSan.BaoCaoPhanTich
{
    public partial class rptTimeRequre : DevExpress.XtraEditors.XtraForm
    {
        public int? MaTS { get; set; }
        public rptTimeRequre()
        {
            InitializeComponent();
            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            BaoCaoPhanTich.rptTheTaiSan rpt = new rptTheTaiSan(MaTS, (DateTime?)dateNgayLT.EditValue);
            rpt.ShowPreviewDialog();
            this.Close();
        }

        private void rptTimeRequre_Load(object sender, EventArgs e)
        {
            dateNgayLT.EditValue = DateTime.Now;
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {

            BaoCaoPhanTich.rptTheTaiSan rpt = new rptTheTaiSan(MaTS, (DateTime?)dateNgayLT.EditValue);
            rpt.PrintDialog();
            this.Close();
        }
    }
}