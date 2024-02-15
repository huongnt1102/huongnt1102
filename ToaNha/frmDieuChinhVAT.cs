using System;
using System.Windows.Forms;
using Library;
using System.Linq;

namespace ToaNha
{
    public partial class frmDieuChinhVAT : DevExpress.XtraEditors.XtraForm
    {
        private MasterDataContext db;
        public frmDieuChinhVAT()
        {
            InitializeComponent();
        }
        
        void LoadData()
        {
            try
            {
                var _MaTN = (byte?)itemToaNha.EditValue;
                gc.DataSource = db.tblConfigVATs.Where(p => p.MaTN == _MaTN);
            }
            catch
            {
                gc.DataSource = null;
            }
        }

        void Delete()
        {
            if (DialogBox.QuestionDelete() == System.Windows.Forms.DialogResult.Yes)
                gv.DeleteSelectedRows();
        }

        private void frmDieuChinhVAT_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            gv.InvalidRowException += Library.Common.InvalidRowException;
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
            gv.AddNewRow();
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
            catch (Exception ex)
            {
                DialogBox.Alert("Error: " + ex);
            }
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void gv_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            var MaTN = gv.GetRowCellValue(e.RowHandle, "MaTN");
            if (MaTN == null)
            {
                e.Valid = false;
                e.ErrorText = "Vui lòng chọn Dự án !";
                return;
            }

            var loaiDV = gv.GetRowCellValue(e.RowHandle, "MaLoaiDV");
            if (loaiDV == null)
            {
                e.Valid = false;
                e.ErrorText = "Vui lòng chọn loại dịch vụ !";
                return;
            }

            var tyleCK = (decimal?)gv.GetFocusedRowCellValue("TyLeVAT") ?? 0;
            if (tyleCK <= 0)
            {
                e.Valid = false;
                e.ErrorText = "Vui lòng nhập VAT!";
                return;
            }
        }

        private void gv_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            gv.SetFocusedRowCellValue("MaTN", itemToaNha.EditValue);
            gv.SetFocusedRowCellValue("NguoiNhap", Library.Common.User.MaNV);
            gv.SetFocusedRowCellValue("NgayNhap", DateTime.Now);
        }

        private void gv_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            gv.SetRowCellValue(e.RowHandle, "NguoiSua", Library.Common.User.MaNV);
            gv.SetRowCellValue(e.RowHandle, "NgaySua", db.GetSystemDate());
         
        }
    }
}
