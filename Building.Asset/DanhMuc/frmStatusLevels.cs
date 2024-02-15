using System;
using DevExpress.XtraEditors;
using Library;

namespace Building.Asset.DanhMuc
{
    public partial class frmStatusLevels : XtraForm
    {
        private MasterDataContext _db;
        public frmStatusLevels()
        {
            InitializeComponent();
            TranslateLanguage.TranslateControl(this,barManager1);
        }

        private void frmStatusLevels_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            _db = new MasterDataContext();
            gc.DataSource = _db.tbl_PhieuVanHanh_Status_Levels;
        }

        private void itemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                _db.SubmitChanges();

                DialogBox.Alert("Dữ liệu đã được lưu");

                LoadData();
            }
            catch
            {
                DialogBox.Alert("Lưu dữ liệu không thành công! Ràng buộc dữ liệu không cho phép thực hiện thao tác này hoặc đường truyền không ổn định!");
            }
        }

        private void itemReload_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void itemDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gv.DeleteSelectedRows();
        }

        private void itemAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gv.AddNewRow();
        }
    }
}