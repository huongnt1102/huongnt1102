using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using Library;

namespace DichVu.KhachHang.CSKH
{
    public partial class frmLoaiHinhKinhDoanh : Form
    {
        MasterDataContext db = new MasterDataContext();
        public frmLoaiHinhKinhDoanh()
        {
            InitializeComponent();
        }

        private void frmLoaiHinhKinhDoanh_Load(object sender, EventArgs e)
        {
            gcLoaiHinhKD.DataSource = db.LoaiHinhKinhDoanhs.OrderBy(p => p.STT);
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                db.SubmitChanges();

                DialogBox.Success();

                this.Close();
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gvLoaiHinhKD_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (DialogBox.Question("Bạn có chắc không?") == DialogResult.Yes)
                    gvLoaiHinhKD.DeleteSelectedRows();
            }
        }
    }
}
