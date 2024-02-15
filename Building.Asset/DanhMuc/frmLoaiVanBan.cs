using System;
using DevExpress.XtraEditors;
using Library;
using System.Windows.Forms;
using System.Linq;

namespace Building.Asset.DanhMuc
{
    public partial class frmLoaiVanBan : XtraForm
    {
        private MasterDataContext _db;

        public frmLoaiVanBan()
        {
            InitializeComponent();
        }

        private void frmLoaiVanBan_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);

            LoadData();
        }

        private void LoadData()
        {
            _db = new MasterDataContext();
            gc.DataSource = _db.tbl_HoSo_LoaiVanBans;
        }

        private void gv_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            gv.AddNewRow();
            gv.SetFocusedRowCellValue("NguoiNhap", Common.User.MaNV);
            gv.SetFocusedRowCellValue("NgayNhap", DateTime.Now);
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void itemLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _db.SubmitChanges();

            DialogBox.Alert("Dữ liệu đã được lưu");

            LoadData();
        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DialogBox.QuestionDelete() == DialogResult.No) return;
            try
            {
                var obj = _db.tbl_HoSo_LoaiVanBans.FirstOrDefault(_ => _.ID == (int) gv.GetFocusedRowCellValue("ID"));
                if (obj != null)
                {
                    _db.tbl_HoSo_LoaiVanBans.DeleteOnSubmit(obj);
                }

                _db.SubmitChanges();
                gv.DeleteSelectedRows();
            }
            catch
            {
                DialogBox.Alert("Không xóa được, vui lòng liên hệ bộ phận kỹ thuật");
            }
        }

        private void gv_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            DialogBox.Error(e.ErrorText);
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        private void gv_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (DialogBox.QuestionDelete() == DialogResult.No) return;
                try
                {
                    var obj = _db.tbl_HoSo_LoaiVanBans.FirstOrDefault(_ => _.ID == (int)gv.GetFocusedRowCellValue("ID"));
                    if (obj != null)
                    {
                        _db.tbl_HoSo_LoaiVanBans.DeleteOnSubmit(obj);
                    }

                    _db.SubmitChanges();
                    gv.DeleteSelectedRows();
                }
                catch
                {
                    DialogBox.Alert("Không xóa được, vui lòng liên hệ bộ phận kỹ thuật");
                }
            }
        }

        private void gv_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            var id = (int?)gv.GetFocusedRowCellValue("ID");
            if (id == null | id == 0)
                return;
            if (e.Column.FieldName != "NguoiSua" & e.Column.FieldName != "NgaySua")
            {
                gv.SetFocusedRowCellValue("NguoiSua", Common.User.MaNV);
                gv.SetFocusedRowCellValue("NgaySua", DateTime.Now);
            }
        }

        private void gv_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            var tenVietTat = gv.GetFocusedRowCellValue("KyHieu");
            if (tenVietTat == null)
            {
                e.ErrorText = "Vui lòng nhập ký hiệu";
                e.Valid = false;
                return;
            }

            if (IsDuplication("KyHieu", e.RowHandle, tenVietTat.ToString().ToLower()))
            {
                e.ErrorText = "Ký hiệu này đã có";
                e.Valid = false;
                gv.FocusedRowHandle = e.RowHandle;
                return;
            }
        }
        private bool IsDuplication(string fielName, int index, string value)
        {
            for (var i = 0; i < gv.RowCount - 1; i++)
            {
                if (i == index) continue;
                if (gv.GetRowCellValue(i, fielName) != null)
                {
                    var oldValue = gv.GetRowCellValue(i, fielName).ToString();
                    if (oldValue.ToLower() == value) return true;
                }
            }
            return false;
        }
    }
}