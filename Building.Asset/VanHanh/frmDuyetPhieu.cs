using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Library;
namespace Building.Asset.VanHanh
{
    public partial class frmDuyetPhieu : DevExpress.XtraEditors.XtraForm
    {
        public DateTime NgayDuyet=DateTime.UtcNow.AddHours(7);
        public string GhiChuDuyet="";
        public bool IsSave = false;
        public frmDuyetPhieu()
        {
            InitializeComponent();
        }

        private void itemThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmDuyetPhieu_Load(object sender, EventArgs e)
        {
            dtNgayDuyet.EditValue = DateTime.UtcNow.AddHours(7);
            txtGhiChu.Text = @"Duyệt";
        }

        private void itemLuu_Click(object sender, EventArgs e)
        {
            if (txtGhiChu.Text.Trim().Length == 0)
            {
                DialogBox.Alert("Vui lòng nhập nội dung duyệt");
                txtGhiChu.Focus();
                return;
            }
            NgayDuyet = dtNgayDuyet.DateTime;
            GhiChuDuyet = txtGhiChu.Text;
            IsSave = true;
            this.Close();
        }
    }
}