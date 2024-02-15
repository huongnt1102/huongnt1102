using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Library;
using System.Linq;

namespace DichVu.PhiQuanLy
{
    public partial class frmAdjust : DevExpress.XtraEditors.XtraForm
    {
        public decimal? Money;
        public string GhiChu = "";
        public frmAdjust()
        {
            InitializeComponent();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnChapNhan_Click(object sender, EventArgs e)
        {
            if (DialogBox.Question("Bạn có chắn chắn muốn tiếp tục không?") == System.Windows.Forms.DialogResult.Yes)
            {
                DialogResult = System.Windows.Forms.DialogResult.OK;
                Money = spinSoTien.Value;
                GhiChu = txtGhiChu.Text.Trim();
            }

            this.Close();
        }
    }
}