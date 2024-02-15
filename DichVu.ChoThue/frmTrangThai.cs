using System;
using System.Windows.Forms;
using Library;
using System.Linq;

namespace DichVu.ChoThue
{
    public partial class frmTrangThai : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db = new MasterDataContext();
        public tnNhanVien objnhanvien;
        public frmTrangThai()
        {
            InitializeComponent();
            TranslateLanguage.TranslateControl(this);
        }

        void LoadData()
        {
            gcTrangThai.DataSource = db.thueTrangThais;
        }

        private void frmTrangThai_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void itemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            grvTrangThai.AddNewRow();
        }

        private void grvTrangThai_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            grvTrangThai.SetFocusedRowCellValue("MaTN", objnhanvien.MaTN);
        }

        private void grvTrangThai_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (DialogBox.QuestionDelete() == DialogResult.No) return;
                try
                {
                    var deleteobj = db.thueTrangThais.Single(p => p.MaTT == (int)grvTrangThai.GetFocusedRowCellValue("MaTT"));
                    db.thueTrangThais.DeleteOnSubmit(deleteobj);
                    db.SubmitChanges();
                    grvTrangThai.DeleteSelectedRows();
                }
                catch
                {
                    DialogBox.Alert("Xóa không thành công vì bị ràng buộc dữ liệu");
                }
            }
        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DialogBox.QuestionDelete() == DialogResult.No) return;
            try
            {
                var deleteobj = db.thueTrangThais.Single(p => p.MaTT == (int)grvTrangThai.GetFocusedRowCellValue("MaTT"));
                db.thueTrangThais.DeleteOnSubmit(deleteobj);
                db.SubmitChanges();
                grvTrangThai.DeleteSelectedRows();
            }
            catch
            {
                DialogBox.Alert("Xóa không thành công vì bị ràng buộc dữ liệu");
            }
        }

        private void itemLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                db.SubmitChanges();

                DialogBox.Alert("Dữ liệu đã được lưu");

                LoadData();
            }
            catch
            {
                DialogBox.Alert("Vui lòng xem lại dữ liệu có bị ràng buộc hay không");
                this.Close();
            }
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }
    }
}