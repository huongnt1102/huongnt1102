using System.Linq;

namespace DichVu.KhachHang.DanhMuc
{
    public partial class FrmLoaiCoHoi : DevExpress.XtraEditors.XtraForm
    {
        private Library.MasterDataContext _db = new Library.MasterDataContext();

        public FrmLoaiCoHoi()
        {
            InitializeComponent();
        }

        private void FrmLoaiCoHoi_Load(object sender, System.EventArgs e)
        {
            gc.DataSource = _db.ch_KhachHang_Loais;
        }

        private void ItemLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                gv.PostEditor();

                _db.SubmitChanges();
                Library.DialogBox.Success();
            }
            catch (System.Exception ex)
            {
                Library.DialogBox.Error("Lưu dữ liệu bị lỗi: " + ex);
            }
        }

        private void ItemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (Library.DialogBox.QuestionDelete() == System.Windows.Forms.DialogResult.No) return;
                gv.DeleteSelectedRows();
            }
            catch (System.Exception ex)
            {
                Library.DialogBox.Error("Xóa bị lỗi: " + ex);
            }
        }

        private void ItemHuy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }
    }
}