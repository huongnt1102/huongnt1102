using System;
using System.Windows.Forms;
using Library;
using System.Linq;

namespace ToaNha
{
    public partial class frmChietKhau : DevExpress.XtraEditors.XtraForm
    {
        // Member vars
        private MasterDataContext db;

        public frmChietKhau()
        {
            InitializeComponent();
        }

        // Load ChietKhaus to grid
        void LoadData()
        {
            try
            {
                var _MaTN = (byte)itemToaNha.EditValue;
                gcChietKhau.DataSource = db.dvChietKhaus.Where(p => p.MaTN == _MaTN);       
            }
            catch 
            {
                gcChietKhau.DataSource = null;
            }
        }

        void Delete()
        {
            if (DialogBox.QuestionDelete() == System.Windows.Forms.DialogResult.Yes)
                gvChietKhau.DeleteSelectedRows();
        }

        // Events to load ChietKhau Form
        private void frmChietKhau_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            gvChietKhau.InvalidRowException += Library.Common.InvalidRowException;

            db = new MasterDataContext();
            lkToaNha.DataSource = db.tnToaNhas.Select(p => new { p.MaTN, p.TenTN });
            lkLoaiDichVu.DataSource = db.dvLoaiDichVus.Select(p => new { p.ID, TenLDV = p.TenHienThi });

            itemToaNha.EditValue = Common.User.MaTN;
        }

        private void itemToaNha_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void itemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gvChietKhau.AddNewRow();
        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Delete();
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

        private void gcChietKhau_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                Delete();
        }

        private void gvChietKhau_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            var MaTN = gvChietKhau.GetRowCellValue(e.RowHandle, "MaTN");
            if (MaTN == null)
            {
                e.Valid = false;
                e.ErrorText = "Vui lòng chọn Dự án !";
                return;
            }

            var loaiDV = gvChietKhau.GetRowCellValue(e.RowHandle, "MaLDV");
            if (loaiDV == null)
            {
                e.Valid = false;
                e.ErrorText = "Vui lòng chọn loại dịch vụ !";
                return;
            }

            var kyTT = (int?)gvChietKhau.GetFocusedRowCellValue("KyTT") ?? 0;
            if (kyTT <= 0)
            {
                e.Valid = false;
                e.ErrorText = "Vui lòng nhập kỳ thanh toán (số dương > 0) !";
                return;
            }

            var tyleCK = (decimal?)gvChietKhau.GetFocusedRowCellValue("TyLeCK") ?? 0;
            if (tyleCK <= 0)
            {
                e.Valid = false;
                e.ErrorText = "Vui lòng nhập tỷ lệ chiết khấu (số dương > 0) !";
                return;
            }
        }

        private void gvChietKhau_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            gvChietKhau.SetFocusedRowCellValue("MaTN", itemToaNha.EditValue);
            gvChietKhau.SetFocusedRowCellValue("MaNVN", Library.Common.User.MaNV);
            gvChietKhau.SetFocusedRowCellValue("NgayNhap", DateTime.Now); 
        }

        private void gvChietKhau_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            gvChietKhau.SetRowCellValue(e.RowHandle, "MaNVS", Library.Common.User.MaNV);
            gvChietKhau.SetRowCellValue(e.RowHandle, "NgaySua", db.GetSystemDate());
        }
    }
}