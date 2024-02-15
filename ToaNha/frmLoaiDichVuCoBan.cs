using System;
using System.Windows.Forms;
using Library;
using System.Linq;
using DevExpress.XtraGrid.Views.Grid;

namespace ToaNha
{
    public partial class frmLoaiDichVuCoBan : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db = new MasterDataContext();

        public frmLoaiDichVuCoBan()
        {
            InitializeComponent();
        }

        void LoadData()
        {
            try
            {
                gcLoaiDichVu.DataSource = db.dvLoaiDichVus.Where(p => p.ParentID == 12);
            }
            catch
            {
                gcLoaiDichVu.DataSource = null;
            }
        }

        private void frmLoaiDichVuCoBan_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);

            gvLoaiDichVu.InvalidRowException += Library.Common.InvalidRowException;
            gcLoaiDichVu.KeyUp += Common.GridViewKeyUp;

            this.LoadData();
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void itemToaNha_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void itemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gvLoaiDichVu.AddNewRow();
        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DialogBox.QuestionDelete() == System.Windows.Forms.DialogResult.Yes)
                gvLoaiDichVu.DeleteSelectedRows();
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

        private void gvLoaiDichVu_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            gvLoaiDichVu.SetFocusedRowCellValue("ParentID", 12);
        }

        private void gvLoaiDichVu_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            var obj = e.Row as dvLoaiDichVu;
            if (obj == null) return;

            if (obj.TenLDV == null)
            {
                e.ErrorText = "Vui lòng nhập [tên loại dịch vụ]";
                e.Valid = false;
                return;
            }
            else
            {
                if (Common.Duplication(sender as GridView, e.RowHandle, "TenLDV", obj.TenLDV.ToString()))
                {
                    e.ErrorText = "Loại dịch vụ đã tồn tại";
                    e.Valid = false;
                    return;
                }
            }
        }
    }
}