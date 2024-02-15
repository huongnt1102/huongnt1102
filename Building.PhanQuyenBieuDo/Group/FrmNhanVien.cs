using System.Linq;

namespace Building.PhanQuyenBieuDo.Group
{
    public partial class FrmNhanVien : DevExpress.XtraEditors.XtraForm
    {
        private readonly Library.MasterDataContext _db = new Library.MasterDataContext();

        public FrmNhanVien()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Mục đích: Nếu nhân viên đang log có trong bảng này, lấy nhóm đầu tiên mà nhân viên này thuộc
        /// Nếu nhân viên không thuộc trong bảng này, lấy group chung, nếu group chung k có, khỏi lấy luôn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmNhanVien_Load(object sender, System.EventArgs e)
        {
            gcNhom.DataSource = _db.pq_BieuDoMain_Nhoms;
            glkNhomNhanVien.DataSource = _db.pqNhoms;

            LoadDetail();
        }

        private void LoadDetail()
        {
            var nhomId = gvNhom.GetFocusedRowCellValue("Id");
            if (nhomId == null) return;

            Library.pq_BieuDoMain_Nhom nhom = GetNhomById((int)nhomId);
            if (nhom == null) return;

            gcNhomNhanVien.DataSource = nhom.pq_BieuDoMain_NhomNhanViens;
        }

        private Library.pq_BieuDoMain_Nhom GetNhomById(int? id)
        {
            return _db.pq_BieuDoMain_Nhoms.FirstOrDefault(_ => _.Id == id);
        }

        private void ItemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                _db.SubmitChanges();

                Library.DialogBox.Success();
            }
            catch (System.Exception ex)
            {
                Library.DialogBox.Error(ex.Message);
            }
        }

        private void ItemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (Library.DialogBox.QuestionDelete() == System.Windows.Forms.DialogResult.No) return;

            gvNhomNhanVien.DeleteSelectedRows();
        }

        private void gvNhom_Click(object sender, System.EventArgs e)
        {
            LoadDetail();
        }
    }
}