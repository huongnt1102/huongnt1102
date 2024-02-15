using System.Linq;

namespace Building.PhanQuyenBieuDo.Group
{
    public partial class FrmNhomBieuDo : DevExpress.XtraEditors.XtraForm
    {

        private Library.MasterDataContext _db;
        public FrmNhomBieuDo()
        {
            InitializeComponent();
        }

        private void FrmNhomBieuDo_Load(object sender, System.EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            _db = new Library.MasterDataContext();
            glkNhomBieuDo.DataSource = _db.pq_BieuDoMain_Nhoms;
            gc.DataSource = _db.pq_BieuDoMain_Controls.OrderBy(_=>_.GroupId);
        }

        private void itemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _db.SubmitChanges();

            Library.DialogBox.Alert("Dữ liệu đã được lưu");

            LoadData();
        }

        private void itemCancel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void glkNhomBieuDo_EditValueChanged(object sender, System.EventArgs e)
        {
            try
            {
                var item = sender as DevExpress.XtraEditors.GridLookUpEdit;
                if (item == null) return;
                if (item.EditValue == null) return;

                gv.SetFocusedRowCellValue("GroupName",item.Properties.View.GetFocusedRowCellValue("Name"));
            }
            catch { }
        }
    }
}