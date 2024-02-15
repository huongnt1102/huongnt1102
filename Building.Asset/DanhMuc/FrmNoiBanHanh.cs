using System.Linq;

namespace Building.Asset.DanhMuc
{
    public partial class FrmNoiBanHanh : DevExpress.XtraEditors.XtraForm
    {
        private Library.MasterDataContext _db;

        public FrmNoiBanHanh()
        {
            InitializeComponent();
        }

        private void FrmNoiBanHanh_Load(object sender, System.EventArgs e)
        {
            Library.TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Library.Common.User, barManager1);

            LoadData();
        }

        private void LoadData()
        {
            _db = new Library.MasterDataContext();
            gc.DataSource = _db.tbl_HoSo_NoiBanHanhs;
        }

        private void ItemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void ItemLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                _db.SubmitChanges();

                Library.DialogBox.Success();
            }
            catch (System.Exception ex)
            {
                Library.DialogBox.Error("Lưu dữ liệu bị lỗi: " + ex.Message);
            }
        }

        private void ItemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (Library.DialogBox.QuestionDelete() == System.Windows.Forms.DialogResult.No) return;
            try
            {
                var obj = _db.tbl_HoSo_NoiBanHanhs.FirstOrDefault(_ => _.Id == (int)gv.GetFocusedRowCellValue("Id"));
                if (obj != null)
                {
                    _db.tbl_HoSo_NoiBanHanhs.DeleteOnSubmit(obj);
                }

                _db.SubmitChanges();
                gv.DeleteSelectedRows();
            }
            catch (System.Exception ex)
            {
                Library.DialogBox.Alert("Xóa bị lỗi: "+ex.Message);
            }
        }
    }
}