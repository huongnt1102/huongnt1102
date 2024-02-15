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

namespace Document
{
    public partial class frmLoaiTaiLieu : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db = new MasterDataContext();

        public frmLoaiTaiLieu()
        {
            InitializeComponent();

            TranslateLanguage.TranslateControl(this);

            this.Load += new EventHandler(frmLoaiTaiLieu_Load);
            this.btnLuu.Click += new EventHandler(btnLuu_Click);
            this.btnHuy.Click += new EventHandler(btnHuy_Click);
            grvLoaiTaiLieu.KeyUp += new KeyEventHandler(grvLoaiTaiLieu_KeyUp);
        }

        void frmLoaiTaiLieu_Load(object sender, EventArgs e)
        {
            gcLoaiTaiLieu.DataSource = db.docLoaiTaiLieus;
        }

        void grvLoaiTaiLieu_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                grvLoaiTaiLieu.DeleteSelectedRows();
        }

        void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                db.SubmitChanges();
                this.Close();
            }
            catch(Exception ex){
                DialogBox.Error(ex.Message);
            }
        }

        void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}