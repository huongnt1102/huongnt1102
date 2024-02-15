using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Library;

namespace DichVu.YeuCau.Extend
{
    public partial class frmExtend : DevExpress.XtraEditors.XtraForm
    {
        public tnycYeuCau objRequest { get; set; }
        public string reason { get; set; }
        public string formName { get; set; }
        public frmExtend()
        {
            InitializeComponent();
        }

        private void frmExtend_Load(object sender, EventArgs e)
        {
            dateExtend.EditValue = objRequest.NewDeadline;
            txtReason.Text = "";
        }

        private void itemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (dateExtend.Text.Trim().Length == 0)
            {
                DialogBox.Error("Vui lòng nhập ngày hết hạn");
                this.layoutControlItem1.AppearanceItemCaption.ForeColor = System.Drawing.Color.Red;
                this.layoutControlItem1.AppearanceItemCaption.Options.UseForeColor = true;
                dateExtend.Focus();
                return;
            }

            objRequest.NewDeadline = dateExtend.DateTime;
            reason = txtReason.Text;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void itemCancel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}