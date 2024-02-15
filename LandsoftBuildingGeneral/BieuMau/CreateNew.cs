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

namespace LandsoftBuildingGeneral.BieuMau
{
    public partial class frmCreateNew : DevExpress.XtraEditors.XtraForm
    {
        public frmCreateNew()
        {
            InitializeComponent();
            TranslateLanguage.TranslateControl(this);
        }
        public int MaBM { get; set; }

        private void btnsave_Click(object sender, EventArgs e)
        {
            frmEditor frmedit = new frmEditor();
            frmedit.IsCreateNew = true;
            frmedit.TenBieuMau = btnten.Text.Trim();
            frmedit.GhiChu = btnGhiChu.Text.Trim();

            frmedit.ShowDialog();
            this.Close();
        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmCreateNew_Load(object sender, EventArgs e)
        {
            btnten.Focus();
        }
    }
}