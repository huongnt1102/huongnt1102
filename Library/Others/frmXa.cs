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
    public partial class frmXa : DevExpress.XtraEditors.XtraForm
    {
        public int? MaXa { get; set; }
        public bool IsUpdate = false;
        public int? MaTinh { get; set; }
        public int? MaHuyen { get; set; }

        MasterDataContext db = new MasterDataContext();
        Xa objXa;

        public frmXa()
        {
            InitializeComponent();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Huong_frm_Load(object sender, EventArgs e)
        {
            lookUpTinh.Properties.DataSource = db.Tinhs.OrderBy(p => p.TenTinh).ToList();
            lookUpHuyen.Properties.DataSource = db.Huyens.Where(p => p.MaTinh == (int?)lookUpTinh.EditValue).OrderBy(p => p.TenHuyen).ToList();
            if (this.MaXa != null)
            {
                objXa = db.Xas.Single(p => p.MaXa == this.MaXa);
                txtTenXa.EditValue = objXa.TenXa;
                lookUpHuyen.EditValue = objXa.MaHuyen;
                lookUpTinh.EditValue = this.MaTinh;
            }
            else
            {
                objXa = new Xa();
                txtTenXa.EditValue = null;
                lookUpTinh.EditValue = this.MaTinh;
                txtTenXa.Focus();
            }
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            if (txtTenXa.Text == "")
            {
                DialogBox.Alert("Vui lòng nhập tên huyện. Xin cảm ơn");
                txtTenXa.Focus();
                return;
            }

            if (lookUpHuyen.Text == "")
            {
                DialogBox.Alert("Vui lòng nhập chọn huyện. Xin cảm ơn");
                lookUpHuyen.Focus();
                return;
            }

            objXa.TenXa = txtTenXa.Text;
            objXa.MaHuyen = (int)lookUpHuyen.EditValue;
            if (objXa.MaXa == 0)
                db.Xas.InsertOnSubmit(objXa);
            db.SubmitChanges();

            IsUpdate = true;
            this.MaXa = objXa.MaXa;
            DialogBox.Success();
            this.Close();
        }

        private void lookUpTinh_EditValueChanged(object sender, EventArgs e)
        {
            this.MaTinh = (int)lookUpTinh.EditValue;
            lookUpHuyen.Properties.DataSource = db.Huyens.Where(p => p.MaTinh == this.MaTinh).OrderBy(p => p.TenHuyen).ToList();
            lookUpHuyen.ItemIndex = 0;
        }
        
        private void lookUpTinh_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                var frm = new frmTinh();
                frm.ShowDialog();
                if (frm.IsUpdate)
                    lookUpTinh.Properties.DataSource = db.Tinhs.OrderBy(p => p.TenTinh).ToList();
            }
        }
        
        private void lookUpHuyen_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                var frm = new frmHuyen();
                frm.MaTinh = (int)lookUpTinh.EditValue;
                frm.ShowDialog();
                if (frm.IsUpdate)
                    lookUpHuyen.Properties.DataSource = db.Huyens.Where(p => p.MaTinh == (int?)lookUpTinh.EditValue).OrderBy(p => p.TenHuyen).ToList();
            }
        }
    }
}