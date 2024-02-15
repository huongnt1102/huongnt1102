using System;
using DevExpress.XtraEditors;
using Library;
using System.Windows.Forms;
using System.Linq;

namespace Building.Asset.DanhMuc
{
    public partial class frmTinhTrangTaiSan : XtraForm
    {
        private MasterDataContext _db;

        public frmTinhTrangTaiSan()
        {
            InitializeComponent();
        }

        private void frmNhomTaiSan_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);

            repositoryItemLookUpEditToaNha.DataSource = Common.TowerList;
            //barEditItemToaNha.EditValue = Common.User.MaTN;

            LoadData();
        }

        private void LoadData()
        {
            _db = new MasterDataContext();
            gridControl.DataSource = _db.tbl_TinhTrangTaiSans;
        }

        private void gridView_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            gridView.AddNewRow();
            //gridView.SetFocusedRowCellValue("MaTN", ((byte?)barEditItemToaNha.EditValue ?? Common.User.MaTN));
            gridView.SetFocusedRowCellValue("NgayNhap", DateTime.Now);
            gridView.SetFocusedRowCellValue("NguoiNhap", Common.User.MaNV);
        }

        private void barButtonItemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void barButtonItemLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _db.SubmitChanges();

            DialogBox.Alert("Dữ liệu đã được lưu");

            LoadData();
        }

        private void barButtonItemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DialogBox.QuestionDelete() == DialogResult.No) return;
            try
            {
                var obj = _db.tbl_NhomTaiSans.FirstOrDefault(_ => _.ID == (int) gridView.GetFocusedRowCellValue("ID"));
                if (obj != null)
                {
                    _db.tbl_NhomTaiSans.DeleteOnSubmit(obj);
                }

                _db.SubmitChanges();
                gridView.DeleteSelectedRows();
            }
            catch
            {
                DialogBox.Alert("Không xóa được, vui lòng liên hệ bộ phận kỹ thuật");
            }
        }

        private void gridView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            var id = (int?) gridView.GetFocusedRowCellValue("ID");
            if (id == null | id == 0) return;
            if (e.Column.FieldName != "NguoiSua" & e.Column.FieldName != "NgaySua")
            {
                gridView.SetFocusedRowCellValue("NguoiSua", Common.User.MaNV);
                gridView.SetFocusedRowCellValue("NgaySua", DateTime.Now);
            }
        }

        private void gridView_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            var tenVietTat = gridView.GetFocusedRowCellValue("TenTinhTrang");
            if (tenVietTat == null)
            {
                e.ErrorText = "Vui lòng nhập tên tình trạng!";
                e.Valid = false;
                return;
            }

            if (IsDuplication("TenTinhTrang", e.RowHandle, tenVietTat.ToString()))
            {
                e.ErrorText = "Hệ thống đã tồn tại tình trạng";
                e.Valid = false;
                gridView.FocusedRowHandle = e.RowHandle;
                return;
            }
        }

        private bool IsDuplication(string fielName, int index, string value)
        {
            for (var i = 0; i < gridView.RowCount - 1; i++)
            {
                if (i == index) continue;
                if (gridView.GetRowCellValue(i, fielName) != null)
                {
                    var oldValue = gridView.GetRowCellValue(i, fielName).ToString();
                    if (oldValue == value) return true;
                }
            }
            return false;
        }

        private void gridView_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            DialogBox.Error(e.ErrorText);
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        private void gridView_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (DialogBox.QuestionDelete() == DialogResult.No) return;
                try
                {
                    var obj = _db.tbl_NhomTaiSans.FirstOrDefault(_ => _.ID == (int)gridView.GetFocusedRowCellValue("ID"));
                    if (obj != null)
                    {
                        _db.tbl_NhomTaiSans.DeleteOnSubmit(obj);
                    }

                    _db.SubmitChanges();
                    gridView.DeleteSelectedRows();
                }
                catch
                {
                    DialogBox.Alert("Không xóa được, vui lòng liên hệ bộ phận kỹ thuật");
                }
            }
        }

        private void barButtonItemImport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                using (var frm = new Import.frmNhomTaiSan_Import())
                {
                    frm.ShowDialog();
                    if (frm.IsSave)
                        LoadData();
                }
            }
            catch
            {
                //
            }
        }

        private void barEditItemToaNha_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }
    }
}