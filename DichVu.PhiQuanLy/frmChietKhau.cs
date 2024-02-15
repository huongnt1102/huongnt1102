using System;
using System.Windows.Forms;
using System.Linq;
using Library;

namespace DichVu.PhiQuanLy
{
    public partial class frmDinhMuc : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public tnNhanVien objnhanvien;
        public frmDinhMuc()
        {
            InitializeComponent();

            db = new MasterDataContext();
            TranslateLanguage.TranslateControl(this);
        }

        private void frmDinhMuc_Load(object sender, EventArgs e)
        {
            gcDinhMuc.DataSource = db.PhiQuanLy_ChietKhaus.OrderBy(p => p.SoThangThanhToan);
        }

        private void grvDinhMuc_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                db.SubmitChanges();

                DialogBox.Alert("Dữ liệu đã được lưu");

                this.Close();
            }
            catch
            {
                DialogBox.Alert("Vui lòng xem lại dữ liệu có bị ràng buộc hay không");
                this.Close();
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void grvDinhMuc_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            try
            {
                var deleteobj = db.PhiQuanLy_ChietKhaus.Single(p => p.ID == (int)grvDinhMuc.GetFocusedRowCellValue("ID"));
                db.PhiQuanLy_ChietKhaus.DeleteOnSubmit(deleteobj);
                db.SubmitChanges();
                grvDinhMuc.DeleteSelectedRows();
            }
            catch
            {
                DialogBox.Alert("Xóa không thành công vì bị ràng buộc dữ liệu");
            }
        }
    }
}