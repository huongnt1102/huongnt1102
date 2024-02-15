using DevExpress.XtraEditors;
using Library;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Library.Controls.LichHen
{
    public partial class frmXuLy : XtraForm
    {
        MasterDataContext db = new MasterDataContext();
        int? MaLH;
        Library.LichHen objLH;

        public frmXuLy(int? maLH)
        {
            InitializeComponent();
            this.MaLH = maLH;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (lkTrangThai.EditValue == null)
            {
                DialogBox.Error("Vui lòng chọn trạng thái");
                return;
            }

			if (txtNoiDung.Text.Trim().Length == 0)
			{
				DialogBox.Error("Vui lòng nhập nội dung xử lý");
				return;
			}

			objLH.XuLy_MaTT = (int?)lkTrangThai.EditValue;
			objLH.XuLy_NoiDung = txtNoiDung.Text;
            objLH.XuLy_NgayXuLy = DateTime.UtcNow.AddHours(7);

			db.SubmitChanges();
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void frmXuLy_Load(object sender, EventArgs e)
        {
            lkTrangThai.Properties.DataSource = db.LichHen_TrangThais.OrderBy(p => p.STT);

            objLH = db.LichHens.Single(o => o.MaLH == this.MaLH);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
