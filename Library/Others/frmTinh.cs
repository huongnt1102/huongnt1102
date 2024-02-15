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

namespace Library.Other
{
    public partial class frmTinh : DevExpress.XtraEditors.XtraForm
    {
        public int? MaTinh { get; set; }
        public bool IsUpdate = false;

        MasterDataContext db = new MasterDataContext();
        Tinh objTinh;

        public frmTinh()
        {
            InitializeComponent();

        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Huong_frm_Load(object sender, EventArgs e)
        {
            if (this.MaTinh != null)
            {
                objTinh = db.Tinhs.Single(p => p.MaTinh == this.MaTinh);
                txtTenTinh.EditValue = objTinh.TenTinh;
            }
            else
            {
                objTinh = new Tinh();
                txtTenTinh.EditValue = null;
                txtTenTinh.Focus();
            }
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            if (txtTenTinh.Text == "")
            {
                DialogBox.Alert("Vui lòng nhập tên tỉnh. Xin cảm ơn");
                txtTenTinh.Focus();
                return;
            }

            objTinh.TenTinh = txtTenTinh.Text;
            if (objTinh.MaTinh == 0)
            {
                db.Tinhs.InsertOnSubmit(objTinh);
            }
            db.SubmitChanges();

            IsUpdate = true;
            this.MaTinh = objTinh.MaTinh;
            DialogBox.Success();
            this.Close();
        }
    }
}