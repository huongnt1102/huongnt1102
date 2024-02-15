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


namespace DIPCRM.PriceAlert.DanhMuc
{
    public partial class frmLoaiBaoGia : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db = new MasterDataContext();

        public frmLoaiBaoGia()
        {
            InitializeComponent();

            

            this.Load += new EventHandler(frmLoaiBaoGia_Load);
            this.btnLuu.Click += new EventHandler(btnLuu_Click);
            this.btnHuy.Click += new EventHandler(btnHuy_Click);
        }

        void frmLoaiBaoGia_Load(object sender, EventArgs e)
        {
            gcCapDo.DataSource = db.bgLoaiBaoGias;
        }

        void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                db.SubmitChanges();

                

                this.DialogResult = System.Windows.Forms.DialogResult.OK;
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

        private void grvCapDo_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (DialogBox.Question("Bạn có chắc không?") == DialogResult.Yes)
                    grvCapDo.DeleteSelectedRows();
            }
        }
    }
}