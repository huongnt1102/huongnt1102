using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Library;
using System.Data.Linq;
using System.Linq;

namespace LandSoftBuilding.Report
{
    public partial class frmOption : Building.PrintControls.PrintFilterForm
    {
        public byte MaTN { get; set; }
        public int ReportID { get; set; }

        public frmOption()
        {
            InitializeComponent();
        }

        private void frmOption_Load(object sender, EventArgs e)
        {
            lookUpToaNha.Properties.DataSource = Common.TowerList;
            lookUpToaNha.EditValue = this.MaTN;

            spinYear.EditValue = DateTime.Now.Year;
            SetMonth();
        }

        int GetMonth()
        {
            switch (cmbMonth.EditValue.ToString())
            {
                case "Tháng 1":
                    return 1;
                case "Tháng 2":
                    return 2;
                case "Tháng 3":
                    return 3;
                case "Tháng 4":
                    return 4;
                case "Tháng 5":
                    return 5;
                case "Tháng 6":
                    return 6;
                case "Tháng 7":
                    return 7;
                case "Tháng 8":
                    return 8;
                case "Tháng 9":
                    return 9;
                case "Tháng 10":
                    return 10;
                case "Tháng 11":
                    return 11;
                case "Tháng 12":
                    return 12;
                default:
                    return 0;
            }
        }

        void SetMonth()
        {
            int month = DateTime.Now.Month;

            cmbMonth.EditValue = string.Format("Tháng {0}", month);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            var _Month = GetMonth();
            var _Year = Convert.ToInt32(spinYear.EditValue);
            var _MaTN = Convert.ToByte(lookUpToaNha.EditValue);

            switch (this.ReportID)
            {
                case 32: //Tong hop phai thu
                    this.PrintControl.Report = new rptBangTongHopPhaiThu(_Month, _Year, _MaTN);
                    break;
                case 34: //Tong hop chua thu
                    this.PrintControl.Report = new rptTongHopChuaThu(_Month, _Year, _MaTN);
                    break;
                case 36: //Tong hop da thu
                    this.PrintControl.Report = new rptTongHopThuTheoThang(_Month, _Year, _MaTN);
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