using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using DIPCRM.DataEntity;
using DIPCRM.Library;

namespace DIP.SwitchBoard
{
    public partial class frmTrangThai : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db = new MasterDataContext();
        public frmTrangThai()
        {
            InitializeComponent();
            gcTrangThai.DataSource = db.cTrangThaiNKs;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var rows = gvTrangThai.GetSelectedRows();
            if (rows.Length <= 0)
            {
                XtraMessageBox.Show("Vui lòng chọn dòng cần xóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (XtraMessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No) return;
                {
                    var obj = (from p in db.cNhatKies where p.MaTT == int.Parse(gvTrangThai.GetFocusedRowCellValue("ID").ToString()) select p).ToList();

                    if(obj.Count > 0 )
                    {
                        if (XtraMessageBox.Show("Trạng thái này đã được sử dụng. Bạn có muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                            gvTrangThai.DeleteSelectedRows();
                    }
                }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            db.SubmitChanges();
            XtraMessageBox.Show("Dữ liệu đã được cập nhật.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
