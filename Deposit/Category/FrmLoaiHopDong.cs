namespace Deposit.Category
{
    public partial class FrmLoaiHopDong : DevExpress.XtraEditors.XtraForm
    {
        private Library.MasterDataContext _db = new Library.MasterDataContext();

        public FrmLoaiHopDong()
        {
            InitializeComponent();
        }

        private void FrmLoaiHopDong_Load(object sender, System.EventArgs e)
        {
            gc.DataSource = _db.Dep_LoaiHopDongs;
        }

        private void ItemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                gv.PostEditor();
                _db.SubmitChanges();
                Library.DialogBox.Success();
            }
            catch(System.Exception ex)
            {
                Library.DialogBox.Error("Lưu dữ liệu bị lỗi: "+ex);
            }
        }

        private void ItemDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (Library.DialogBox.QuestionDelete() == System.Windows.Forms.DialogResult.No) return;
                gv.DeleteSelectedRows();
            }
            catch
            {
            }
        }

        private void ItemCancel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Close();
        }
    }
}