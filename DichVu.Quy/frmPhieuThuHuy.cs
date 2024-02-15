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

namespace DichVu.Quy
{
    public partial class frmPhieuThuHuy : DevExpress.XtraEditors.XtraForm
    {
        public int MaPhieu { get; set; }
        public bool TrangThai { get; set; }
        public bool confirm { get; set; }
        public frmPhieuThuHuy()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            confirm = false;
            this.Close();           
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                using (var db = new MasterDataContext())
                {
                    var objpt = db.PhieuThus.Single(p => p.MaPhieu == MaPhieu);
                    objpt.IsCancel = cmbTrangThai.SelectedIndex == 0 ? false : true;
                    objpt.LyDoHuy = txtLyDoHuy.Text.Trim();
                    db.SubmitChanges();
                }
                confirm = true;
                this.Close();
            }
            catch { }

        }

        private void frmPhieuThuHuy_Load(object sender, EventArgs e)
        {
            if (TrangThai)
                cmbTrangThai.SelectedIndex = 1;
            else
                cmbTrangThai.SelectedIndex = 0;
        }

    }
}