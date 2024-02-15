using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using Library;

namespace LandSoftBuilding.Report
{
    public partial class frmPickDateTower : Building.PrintControls.PrintFilterForm
    {
        public byte MaTN { get; set; }
        public int ReportID { get; set; }

        public frmPickDateTower()
        {
            InitializeComponent();
            this.Load += new EventHandler(frmPickDateTower_Load);
        }

        void frmPickDateTower_Load(object sender, EventArgs e)
        {
            lookUpToaNha.Properties.DataSource = Common.TowerList;
            lookUpToaNha.EditValue = this.MaTN;
           
            SetDate(3);
        }

        private void SetDate(int index)
        {
            var objKBC = new KyBaoCao()
            {
                Index = index
            };
            objKBC.SetToDate();

            dateTuNgay.EditValue = objKBC.DateFrom;
        }

        private void cbbKyBaoCao_EditValueChanged(object sender, EventArgs e)
        {
            SetDate((sender as ComboBoxEdit).SelectedIndex);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (dateTuNgay.DateTime.Year == 1)
            {
                DialogBox.Alert("Vui lòng chọn [Kỳ báo cáo], xin cám ơn.");
                dateTuNgay.Focus();
                return;
            }

            if (lookUpToaNha.EditValue == null)
            {
                DialogBox.Alert("Vui lòng chọn [Dự án], xin cám ơn.");
                lookUpToaNha.Focus();
                return;
            }

            switch (this.ReportID)
            {
                case 31:
                    this.PrintControl.Report = new rptDoanhThuTrongNgay(dateTuNgay.DateTime, Convert.ToInt32(lookUpToaNha.EditValue));
                    break;
            }

            this.Hide();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
