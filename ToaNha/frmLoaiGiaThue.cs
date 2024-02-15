using System;
using System.Windows.Forms;
using Library;
using System.Linq;

namespace ToaNha
{
    public partial class frmLoaiGiaThue : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;

        public frmLoaiGiaThue()
        {
            InitializeComponent();
        }

        void LoadData()
        {
            try
            {
                var _MaTN = (byte)itemToaNha.EditValue;
                gcLoaiGiaThue.DataSource = db.LoaiGiaThues.Where(p => p.MaTN == _MaTN);
            }
            catch 
            {
                gcLoaiGiaThue.DataSource = null;
            }
        }

        void Delete()
        {
            if (DialogBox.QuestionDelete() == System.Windows.Forms.DialogResult.Yes)
                gvLoaiGiaThue.DeleteSelectedRows();
        }

        private void frmLoaiGiaThue_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            gvLoaiGiaThue.InvalidRowException += Library.Common.InvalidRowException;

            db = new MasterDataContext();
            lkToaNha.DataSource = db.tnToaNhas.Select(p => new { p.MaTN, p.TenTN });
            lkLoaiTien.DataSource = db.LoaiTiens.Select(p => new { p.ID, p.KyHieuLT });

            itemToaNha.EditValue = Common.User.MaTN;
        }

        private void itemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gvLoaiGiaThue.AddNewRow();
        }

        private void gcLoaiGiaThue_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                Delete();
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

        private void gvLoaiGiaThue_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            var MaTN = gvLoaiGiaThue.GetRowCellValue(e.RowHandle, "MaTN");
            if (MaTN == null)
            {
                e.Valid = false;
                e.ErrorText = "Vui lòng chọn Dự án !";
                return;
            }

            var TenLG = (gvLoaiGiaThue.GetRowCellValue(e.RowHandle, "TenLG") ?? "").ToString();
            if (TenLG.Length == 0)
            {
                e.Valid = false;
                e.ErrorText = "Vui lòng nhập Tên loại giá !";
                return;
            }
            else if (Common.Duplication(gvLoaiGiaThue, e.RowHandle, "TenLG", TenLG))
            {
                e.Valid = false;
                e.ErrorText = "Tên Loại giá trùng, vui lòng nhập lại !";
                return;
            }

            var DonGia = (decimal?)gvLoaiGiaThue.GetFocusedRowCellValue("DonGia") ?? 0;
            if (DonGia <= 0)
            {
                e.Valid = false;
                e.ErrorText = "Vui lòng nhập đơn giá (số dương > 0) !";
                return;
            }

            var LoaiTien = gvLoaiGiaThue.GetRowCellValue(e.RowHandle, "MaLT");
            if (LoaiTien == null)
            {
                e.Valid = false;
                e.ErrorText = "Vui lòng nhập loại tiền !";
                return;
            }
        }

        private void itemToaNha_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void gvLoaiGiaThue_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            gvLoaiGiaThue.SetFocusedRowCellValue("MaTN", itemToaNha.EditValue);
        }
    }
}