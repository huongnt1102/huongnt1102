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

namespace DichVu.KhachHang.CSKH
{
    public partial class frmQuyMo : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db = new MasterDataContext();

        public frmQuyMo()
        {
            InitializeComponent();

            this.Load += new EventHandler(frmQuyMo_Load);
            this.btnLuu.Click += new EventHandler(btnLuu_Click);
            this.btnHuy.Click += new EventHandler(btnHuy_Click);
            grView.KeyUp += new KeyEventHandler(grView_KeyUp);
        }

        void frmQuyMo_Load(object sender, EventArgs e)
        {
            grControl.DataSource = db.QuyMoCongTies.OrderBy(p=>p.STT);
        }

        void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                db.SubmitChanges();

                DialogBox.Success();

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

        private void grView_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (DialogBox.Question("Bạn có chắc không?") == DialogResult.Yes)
                    grView.DeleteSelectedRows();
            }
        }
    }
}