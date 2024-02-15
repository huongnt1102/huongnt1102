using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Library;
namespace Building.Asset.BaoTri
{
    public partial class frmDuyetProfile : DevExpress.XtraEditors.XtraForm
    {
        public string GhiChu;
        public bool IsSave;
        public bool IsDuyet;
        public frmDuyetProfile()
        {
            InitializeComponent();
        }

        private void frmDuyetProfile_Load(object sender, EventArgs e)
        {

        }

        private void itemThoat_Click(object sender, EventArgs e)
        {
            IsSave = false;
            this.Close();
        }

        private void itemKhongDuyet_Click(object sender, EventArgs e)
        {
            if (txtGhiChu.Text.Trim().Length == 0)
            {
                DialogBox.Alert("Vui lòng nhập ghi chú");
                return;
            }
            IsSave = true;
            GhiChu = txtGhiChu.Text;
            IsDuyet = false;
            this.Close();
        }

        private void itemDuyet_Click(object sender, EventArgs e)
        {
            if (txtGhiChu.Text.Trim().Length == 0)
            {
                DialogBox.Alert("Vui lòng nhập ghi chú");
                return;
            }
            IsSave = true;
            GhiChu = txtGhiChu.Text;
            IsDuyet = true;
            this.Close();
        }
    }
}