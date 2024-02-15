using System;
using System.Windows.Forms;
using Library;
using System.Linq;

namespace LandSoftBuilding.Lease.DanhMuc
{
    public partial class frmTyGia : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public tnNhanVien objnv;

        public frmTyGia()
        {
            InitializeComponent();
            db = new MasterDataContext();
            TranslateLanguage.TranslateControl(this, barManager1);
        }

        void LoadData()
        {
            lkLoaiTien.DataSource = db.LoaiTiens.Select(_ => new { MaLT = _.ID, TenLT = _.TenLT });
            gcTyGia.DataSource = db.TyGiaNganHangs;
            lookToaNha.DataSource = db.tnToaNhas.Select(p => new { p.MaTN, p.TenTN });
        }

        private void frmNhaCungCap_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void itemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gvTyGia.AddNewRow();
        }

        private void grvNhaCungCap_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            //grvNhaCungCap.SetFocusedRowCellValue("MaTN", UserInfo.MaTN);
        }

        void DeleteData()
        {
            if (DialogBox.QuestionDelete() == DialogResult.No) return;
            try
            {
                var deleteobj = db.tnTyGias.Single(p => p.MaTG == (int)gvTyGia.GetFocusedRowCellValue("MaTG"));
                db.tnTyGias.DeleteOnSubmit(deleteobj);
                db.SubmitChanges();
                gvTyGia.DeleteSelectedRows();
            }
            catch
            {
                DialogBox.Alert("Xóa không thành công vì bị ràng buộc dữ liệu");
            }
        }

        private void grvNhaCungCap_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                DeleteData();
        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DeleteData();
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
                DialogBox.Alert("Không lưu được, dữ liệu bị ràng buộc");
            }
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }
    }
}