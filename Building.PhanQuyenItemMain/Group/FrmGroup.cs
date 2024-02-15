namespace Building.PhanQuyenItemMain.Group
{
    public partial class FrmGroup : DevExpress.XtraEditors.XtraForm
    {
        private readonly Library.MasterDataContext _db = new Library.MasterDataContext();

        public FrmGroup()
        {
            InitializeComponent();
        }

        private void FrmGroup_Load(object sender, System.EventArgs e)
        {
            gc.DataSource = _db.pq_PhanQuyenMain_Groups;
        }

        private void ItemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                gv.PostEditor();
                _db.SubmitChanges();
                DialogResult = System.Windows.Forms.DialogResult.OK;
                Library.DialogBox.Success();
                Close();
            }
            catch (System.Exception ex)
            {
                Library.DialogBox.Error(ex.Message);
            }
        }

        private void ItemCancel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Close();
        }
    }
}