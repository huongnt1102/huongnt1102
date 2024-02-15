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
    public partial class frmHuyen : DevExpress.XtraEditors.XtraForm
    {
        public int? MaHuyen { get; set; }
        public bool IsUpdate = false;
        public int? MaTinh { get; set; }

        MasterDataContext db = new MasterDataContext();
        Huyen objHuyen;

        public frmHuyen()
        {
            InitializeComponent();

            //Translate.Language.TranslateControl(this);
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Huong_frm_Load(object sender, EventArgs e)
        {
            lookUpTinh.Properties.DataSource = db.Tinhs.OrderBy(p => p.TenTinh).ToList();
            lookUpTinh.ItemIndex = 0;
            if (this.MaHuyen != null)
            {
                objHuyen = db.Huyens.Single(p => p.MaHuyen == this.MaHuyen);
                txtTenHuyen.EditValue = objHuyen.TenHuyen;
                lookUpTinh.EditValue = objHuyen.MaTinh;
            }
            else
            {
                objHuyen = new Huyen();
                txtTenHuyen.EditValue = null;
                lookUpTinh.EditValue = MaTinh;
                txtTenHuyen.Focus();
            }
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            if (txtTenHuyen.Text == "")
            {
                DialogBox.Alert("Vui lòng nhập tên huyện. Xin cảm ơn");
                txtTenHuyen.Focus();
                return;
            }

            objHuyen.TenHuyen = txtTenHuyen.Text;
            objHuyen.MaTinh = (int?)lookUpTinh.EditValue;
            if (objHuyen.MaHuyen == 0)
                db.Huyens.InsertOnSubmit(objHuyen);
            db.SubmitChanges();

            IsUpdate = true;
            this.MaHuyen = objHuyen.MaHuyen;
            DialogBox.Success();
            this.Close();
        }
    }
}