namespace Building.PhanQuyenBieuDo.Group
{
    public partial class FrmNhomCaiDat : DevExpress.XtraEditors.XtraForm
    {
        private readonly Library.MasterDataContext _db = new Library.MasterDataContext();

        public FrmNhomCaiDat()
        {
            InitializeComponent();
        }

        private void FrmNhomCaiDat_Load(object sender, System.EventArgs e)
        {
            gc.DataSource = _db.pq_BieuDoMain_Nhoms;
        }

        private void ItemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                _db.SubmitChanges();
                DialogResult = System.Windows.Forms.DialogResult.OK;
                Close();
            }
            catch (System.Exception ex)
            {
                Library.DialogBox.Error("Lưu nhóm cài đặt bị lỗi: " + ex.Message);
            }
        }

        private void ItemCancel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Close();
        }
    }
}