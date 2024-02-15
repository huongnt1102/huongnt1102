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


namespace DIPCRM.Need
{
    public partial class frmTrangThai : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db = new MasterDataContext();

        public frmTrangThai()
        {
            InitializeComponent();

            //

            this.Load += new EventHandler(frmTrangThai_Load);
            this.btnLuu.Click += new EventHandler(btnLuu_Click);
            this.btnHuy.Click += new EventHandler(btnHuy_Click);
            grView.KeyUp += new KeyEventHandler(grView_KeyUp);
        }

        void frmTrangThai_Load(object sender, EventArgs e)
        {
            grControl.DataSource = db.ncTrangThais.OrderBy(p=>p.STT);
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

        private void grView_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
               if (DialogBox.Question("Bạn có chắc không?") == DialogResult.Yes)
                    grView.DeleteSelectedRows();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (DialogBox.Question("Bạn có chắc không?") == DialogResult.Yes)
                    grView.DeleteSelectedRows();
            }
            catch
            {
                DialogBox.Error("Tên trạng thái đã được sử dụng. Không thể xóa!");
            }
        }
    }
}