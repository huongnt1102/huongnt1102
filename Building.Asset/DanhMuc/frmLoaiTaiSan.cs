using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Library;
using System.Linq;

namespace Building.Asset.DanhMuc
{
    public partial class frmLoaiTaiSan : XtraForm
    {
        private MasterDataContext _db;

        public frmLoaiTaiSan()
        {
            InitializeComponent();
        }

        private void frmLoaiTaiSan_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);

            _db = new MasterDataContext();

            repositoryItemLookUpEditToaNha.DataSource = Common.TowerList;
            barEditItemToaNha.EditValue = Common.User.MaTN;

            LoadData();
        }

        private void LoadData()
        {
            _db = new MasterDataContext();

            gridControl1.DataSource = _db.tbl_LoaiTaiSans.Where(_ => _.NhomTaiSanID == ((int?) barEditItemNhomTaiSan.EditValue ?? 0));
        }

        private void barEditItemToaNha_EditValueChanged(object sender, EventArgs e)
        {
            repositoryItemGridLookUpEditNhomTaiSan.DataSource = _db.tbl_NhomTaiSans.Where(_ => _.MaTN == ((byte?) barEditItemToaNha.EditValue ?? (byte) Common.User.MaTN)).ToList();
            barEditItemNhomTaiSan.EditValue = repositoryItemGridLookUpEditNhomTaiSan.GetKeyValue(0);
        }

        private void barButtonItemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void gridView1_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            gridView1.AddNewRow();
            gridView1.SetFocusedRowCellValue("NhomTaiSanID", (int?)barEditItemNhomTaiSan.EditValue ?? 0);
            gridView1.SetFocusedRowCellValue("NgayNhap", DateTime.Now);
            gridView1.SetFocusedRowCellValue("NguoiNhap", Common.User.MaNV);
        }

        private void barButtonItemLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (barEditItemNhomTaiSan.EditValue != null)
            {
                _db.SubmitChanges();
                DialogBox.Alert("Dữ liệu đã được lưu");

                LoadData();
            }
            else
            {
                DialogBox.Error("Tòa nhà này không có hệ thống");
            }
        }

        private void gridView1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (DialogBox.QuestionDelete() == DialogResult.No) return;
                try
                {
                    var obj = _db.tbl_LoaiTaiSans.FirstOrDefault(_ => _.ID == (int)gridView1.GetFocusedRowCellValue("ID"));
                    if (obj != null)
                    {
                        _db.tbl_LoaiTaiSans.DeleteOnSubmit(obj);
                    }

                    _db.SubmitChanges();
                    gridView1.DeleteSelectedRows();
                }
                catch
                {
                    DialogBox.Alert("Không xóa được, vui lòng liên hệ bộ phận kỹ thuật");
                }
            }
        }

        private void barButtonItemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DialogBox.QuestionDelete() == DialogResult.No) return;
            try
            {
                var obj = _db.tbl_LoaiTaiSans.FirstOrDefault(_ => _.ID == (int)gridView1.GetFocusedRowCellValue("ID"));
                if (obj != null)
                {
                    _db.tbl_LoaiTaiSans.DeleteOnSubmit(obj);
                }

                _db.SubmitChanges();
                gridView1.DeleteSelectedRows();
            }
            catch
            {
                DialogBox.Alert("Không xóa được, vui lòng liên hệ bộ phận kỹ thuật");
            }
        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            var id = (int?)gridView1.GetFocusedRowCellValue("ID");
            if (id == null | id == 0) return;
            if (e.Column.FieldName != "NguoiSua" & e.Column.FieldName != "NgaySua")
            {
                gridView1.SetFocusedRowCellValue("NguoiSua", Common.User.MaNV);
                gridView1.SetFocusedRowCellValue("NgaySua", DateTime.Now);
            }
        }

        private void gridView1_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            var tenVietTat = gridView1.GetFocusedRowCellValue("TenVietTat");
            if (tenVietTat == null)
            {
                e.ErrorText = "Vui lòng nhập ký hiệu Hệ thống";
                e.Valid = false;
                return;
            }

            if (IsDuplication("TenVietTat", e.RowHandle, tenVietTat.ToString()))
            {
                e.ErrorText = "Hệ thống này đã có";
                e.Valid = false;
                gridView1.FocusedRowHandle = e.RowHandle;
                return;
            }
        }

        private bool IsDuplication(string fielName, int index, string value)
        {
            for (var i = 0; i < gridView1.RowCount - 1; i++)
            {
                if (i == index) continue;
                if (gridView1.GetRowCellValue(i, fielName) != null)
                {
                    var oldValue = gridView1.GetRowCellValue(i, fielName).ToString();
                    if (oldValue == value) return true;
                }
            }
            return false;
        }

        private void gridView1_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            DialogBox.Error(e.ErrorText);
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        private void barButtonItemImport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                using (var frm = new Import.frmLoaiTaiSan_Import())
                {
                    frm.MaTn = (byte)barEditItemToaNha.EditValue;
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

        private void itemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Commoncls.ExportExcel(gridControl1);
        }
    }
}