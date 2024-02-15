using System;
using System.Windows.Forms;
using Library;
using System.Linq;

namespace ToaNha
{
    public partial class frmLoaiTien : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;

        public frmLoaiTien()
        {
            InitializeComponent();
        }

        void LoadData()
        {
            db = new MasterDataContext();
            gcTyGia.DataSource = db.LoaiTiens;
        }

        void Delete()
        {
            if (DialogBox.QuestionDelete() == System.Windows.Forms.DialogResult.Yes)
                gvTyGia.DeleteSelectedRows();
        }

        private void frmLoaiTien_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            gvTyGia.InvalidRowException += Library.Common.InvalidRowException;

            LoadData();
        }

        private void itemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gvTyGia.AddNewRow();
        }

        private void grvNhaCungCap_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                Delete();
            }
        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Delete();
        }

        private void itemLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Đồng tiền hạch toán", "Lưu");
                db.SubmitChanges();
                DialogBox.Alert("Dữ liệu đã được lưu");
                
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
        
        private void gvTyGia_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            var kyHieuLT = (gvTyGia.GetRowCellValue(e.RowHandle, "KyHieuLT") ?? "").ToString();
            if (kyHieuLT.Length == 0)
            {
                e.Valid = false;
                e.ErrorText = "Vui lòng nhập ký hiệu !";
                return;
            }
            else if (Common.Duplication(gvTyGia, e.RowHandle, "KyHieuLT", kyHieuLT))
            {
                e.Valid = false;
                e.ErrorText = "Ký Hiệu trùng, vui lòng nhập lại !";
                return;
            }

            var tenLT = (gvTyGia.GetRowCellValue(e.RowHandle, "TenLT") ?? "").ToString();
            if (tenLT.Length == 0)
            {
                e.Valid = false;
                e.ErrorText = "Vui lòng nhập tên loại tiền !";
                return;
            }
            else if (Common.Duplication(gvTyGia, e.RowHandle, "TenLT", tenLT))
            {
                e.Valid = false;
                e.ErrorText = "Tên Loại Tiền trùng, vui lòng nhập lại !";
                return;
            }

            var tyGia = (decimal?)gvTyGia.GetFocusedRowCellValue("TyGia") ?? 0;
            if (tyGia <= 0)
            {
                e.Valid = false;
                e.ErrorText = "Vui lòng nhập tỷ giá (số dương > 0) !";
            }
        }

        /// <summary>
        /// Import
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Đồng tiền hạch toán", "Import");
            using (var f = new Import.frmLoaiTien())
            {
                f.ShowDialog();
                if (f.isSave)
                    LoadData();
            }
        }
    }
}