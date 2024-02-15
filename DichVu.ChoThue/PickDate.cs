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

namespace DichVu.ChoThue
{
    public partial class PickDate : DevExpress.XtraEditors.XtraForm
    {
        public List<int> SelectedMaHD { get; set; }

        public PickDate()
        {
            InitializeComponent();
            TranslateLanguage.TranslateControl(this);
        }

        private void PickDate_Load(object sender, EventArgs e)
        {
            KyBaoCao objKBC = new KyBaoCao();
            foreach (string str in objKBC.Source)
            {
                cbbKyBaoCao.Properties.Items.Add(str);
            }
            cbbKyBaoCao.EditValue = objKBC.Source[7];
            SetDate(7);
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

        private void cbbKyBaoCao_EditValueChanged(object sender, EventArgs e)
        {
            SetDate((sender as ComboBoxEdit).SelectedIndex);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (dateTuNgay.EditValue != null && dateDenNgay.EditValue != null)
            {
                var tuNgay = (DateTime)dateTuNgay.EditValue;
                var denNgay = (DateTime)dateDenNgay.EditValue;
                
                //using (ReportMisc.DichVu.ChoThue.ReportTemplate.frmPrintControl frm = new ReportMisc.DichVu.ChoThue.ReportTemplate.frmPrintControl(null, 3, "", SelectedMaHD, tuNgay, denNgay))
                //{
                //    frm.ShowDialog();
                //    this.Close();
                //}
            }
            else
            {
                DialogBox.Alert("Vui lòng chọn kỳ báo cáo");
                return;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}