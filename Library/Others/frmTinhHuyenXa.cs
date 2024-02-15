using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;

namespace Library.Other
{
    public partial class frmTinhHuyenXa : DevExpress.XtraEditors.XtraForm
    {
        public int? MaXa { get; set; }
        public int? MaHuyen { get; set; }
        public int? MaTinh { get; set; }
        public string Result { get; set; }

        void TinhLoad()
        {
            using (var db = new MasterDataContext())
            {
                lookTinh.Properties.DataSource = db.Tinhs.OrderBy(p => p.TenTinh).ToList();
            }
        }

        void HuyenLoad()
        {
            var maTinh = (int?)lookTinh.EditValue;
            if (maTinh != null)
            {
                using (var db = new MasterDataContext())
                {
                    lookHuyen.Properties.DataSource = db.Huyens.Where(p => p.MaTinh == maTinh).OrderBy(p => p.TenHuyen).ToList();
                }
            }
            lookHuyen.EditValue = null;
        }

        void XaLoad()
        {
            var maHuyen = (int?)lookHuyen.EditValue;
            if (maHuyen != null)
            {
                using (var db = new MasterDataContext())
                {
                    lookXa.Properties.DataSource = db.Xas.Where(p => p.MaHuyen == maHuyen).OrderBy(p => p.TenXa).ToList();
                }
            }
            lookXa.EditValue = null;
        }

        public frmTinhHuyenXa()
        {
            InitializeComponent();

            //Translate.Language.TranslateControl(this);

            this.Load += new EventHandler(frmTinhHuyenXa_Load);
            lookTinh.EditValueChanged += new EventHandler(lookTinh_EditValueChanged);
            lookTinh.KeyUp += new KeyEventHandler(lookTinh_KeyUp);
            lookHuyen.EditValueChanged += new EventHandler(lookHuyen_EditValueChanged);
            lookHuyen.KeyUp += new KeyEventHandler(lookHuyen_KeyUp);
            lookXa.KeyUp += new KeyEventHandler(lookXa_KeyUp);
            btnDongY.Click += new EventHandler(btnDongY_Click);
            btnHuy.Click += new EventHandler(btnHuy_Click);
        }

        void lookXa_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete) lookXa.EditValue = null;
        }

        void lookHuyen_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete) lookHuyen.EditValue = null;
        }

        void lookTinh_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete) lookTinh.EditValue = null;
        }

        private void frmTinhHuyenXa_Load(object sender, EventArgs e)
        {
            TinhLoad();
            lookTinh.EditValue = this.MaTinh;
            lookHuyen.EditValue = this.MaHuyen;
            lookXa.EditValue = this.MaXa;
        }

        private void lookTinh_EditValueChanged(object sender, EventArgs e)
        {
            HuyenLoad();
        }

        private void lookHuyen_EditValueChanged(object sender, EventArgs e)
        {
            XaLoad();
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            if (lookTinh.EditValue == null && lookHuyen.EditValue == null && lookXa.EditValue == null)
            {
                DialogBox.Error("Vui lòng chọn vị trí");
                return;
            }

            this.MaXa = (int?)lookXa.EditValue;
            this.MaHuyen = (int?)lookHuyen.EditValue;
            this.MaTinh = (int?)lookTinh.EditValue;

            this.Result = (lookXa.Text != "" ? lookXa.Text + ", " : "") + (lookHuyen.Text != "" ? lookHuyen.Text + ", " : "") + (lookTinh.Text != "" ? lookTinh.Text + ", " : "");
            this.Result = this.Result.TrimEnd(' ').TrimEnd(',');

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}