using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Library;

namespace LandSoftBuilding.Report
{
    public partial class frmBaoCaoKHNoPhiDichVuToanCongTy : Building.PrintControls.PrintFilterForm
    {

        public int ReportID { get; set; } 
        public frmBaoCaoKHNoPhiDichVuToanCongTy()
        {
            InitializeComponent();
            cbbKyBaoCao.EditValueChanged += new EventHandler(cbbKyBaoCao_EditValueChanged);
            btnOK.Click += new EventHandler(btnOK_Click);
            btnCancel.Click += new EventHandler(btnCancel_Click);
        }
        void SetDate(int index)
        {
            var objKBC = new KyBaoCao()
            {
                Index = index
            };
            objKBC.SetToDate();

            dateTuNgay.EditValue = objKBC.DateFrom;
            dateDenNgay.EditValue = objKBC.DateTo;
        }
        void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void frmBaoCaoKHNoPhiDichVuToanCongTy_Load(object sender, EventArgs e)
        {
            chkTN.Properties.DataSource = Common.TowerList.Where(p=>p.MaTN==Common.User.MaTN);
            KyBaoCao objKBC = new KyBaoCao();
            foreach (string str in objKBC.Source)
                cbbKyBaoCao.Properties.Items.Add(str);
            cbbKyBaoCao.EditValue = objKBC.Source[3];
            SetDate(3);
        }
        private void cbbKyBaoCao_EditValueChanged(object sender, EventArgs e)
        {
            SetDate((sender as ComboBoxEdit).SelectedIndex);
        }
        private void chkTN_EditValueChanged(object sender, EventArgs e)
        {
       
        }public DateTime tuNgay, denNgay;
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (dateTuNgay.DateTime.Year == 1)
            {
                DialogBox.Alert("Vui lòng chọn [Kỳ báo cáo], xin cám ơn.");
                dateTuNgay.Focus();
                return;
            }

            if (dateDenNgay.DateTime.Year == 1)
            {
                DialogBox.Alert("Vui lòng chọn [Kỳ báo cáo], xin cám ơn.");
                dateDenNgay.Focus();
                return;
            }

            if (chkTN.EditValue == null)
            {
                DialogBox.Alert("Vui lòng chọn [Dự án], xin cám ơn.");
                chkTN.Focus();
                return;
            }
            var str= chkTN.EditValue.ToString() != "" ? "," + chkTN.EditValue + "," : "";
            str = str.Replace(" ", "");
            tuNgay = dateTuNgay.DateTime;
            denNgay = dateDenNgay.DateTime;
            //var rpt2 = new rptBaoCaoKhNoPhiDichVuToanCongTy(tuNgay, denNgay,
            //           str);
            //rpt2.ShowPreviewDialog();
            switch (this.ReportID)
            {
                case 52 :
                    this.PrintControl.Report = new rptBaoCaoKhNoPhiDichVuToanCongTy(tuNgay, denNgay, str);
                    break;
                case 53:
                        this.PrintControl.Report = new rptBaoCaoCacKhoanPhiDV(tuNgay, denNgay, str);
                    break;
            }
            
          
     

        }
    }
}