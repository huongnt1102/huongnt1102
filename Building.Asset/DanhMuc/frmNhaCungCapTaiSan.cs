using System;
using System.Windows.Forms;
using Library;
using System.Linq;
using DevExpress.XtraGrid;

namespace Building.Asset.DanhMuc
{
    public partial class frmNhaCungCapTaiSan : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public tnNhanVien objnhanvien;

        public frmNhaCungCapTaiSan()
        {
            InitializeComponent();
            db = new MasterDataContext();
            TranslateLanguage.TranslateControl(this,barManager1);
        }

        void LoadData()
        {
            db = new MasterDataContext();
            lkNhanVien.DataSource = db.tnNhanViens.Select(o => new { o.MaNV, o.HoTenNV }).ToList();
            gc.DataSource = db.tbl_NhaCungCapTaiSans;
        }

        private void frmLoaiSanPham_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);
            repToaNha.DataSource = Common.TowerList;
            itemToaNha.EditValue = Common.User.MaTN;
            LoadData();
        }

        private void itemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gv.AddNewRow();
            gv.SetFocusedRowCellValue("NguoiNhap", Common.User.MaNV);
            gv.SetFocusedRowCellValue("NgayNhap", DateTime.Now);
        }

        private void grvHangSanXuat_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            gv.AddNewRow();
            gv.SetFocusedRowCellValue("NguoiNhap", Common.User.MaNV);
            gv.SetFocusedRowCellValue("NgayNhap", DateTime.Now);
        }

        private void grvHangSanXuat_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (DialogBox.QuestionDelete() == DialogResult.No) return;
                try
                {
                    var delobj = db.tbl_NhaCungCapTaiSans.FirstOrDefault(p => p.ID == (int)gv.GetFocusedRowCellValue("ID"));
                    if (delobj != null)
                        db.tbl_NhaCungCapTaiSans.DeleteOnSubmit(delobj);
                    db.SubmitChanges();
                    gv.DeleteSelectedRows();
                }
                catch
                {
                    DialogBox.Alert("Xóa không thành công vì bị ràng buộc dữ liệu");
                }
            }
        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DialogBox.QuestionDelete() == DialogResult.No) return;
            try
            {
                var delobj = db.tbl_NhaCungCapTaiSans.FirstOrDefault(p => p.ID == (int)gv.GetFocusedRowCellValue("ID"));
                if (delobj != null)
                    db.tbl_NhaCungCapTaiSans.DeleteOnSubmit(delobj);
                db.SubmitChanges();
                gv.DeleteSelectedRows();
            }
            catch
            {
                DialogBox.Alert("Xóa không thành công vì bị ràng buộc dữ liệu");
            }
        }

        private void itemLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gv.PostEditor();
            textEdit1.Focus();
            db.SubmitChanges();
            DialogBox.Alert("Dữ liệu đã được lưu");
            LoadData();
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void grvHangSanXuat_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            var ID = (int?)gv.GetFocusedRowCellValue("ID");
            if (ID == null | ID == 0)
                return;
            if (e.Column.FieldName != "NguoiSua" & e.Column.FieldName != "NgaySua")
            {
                gv.SetFocusedRowCellValue("NguoiSua", Common.User.MaNV);
                gv.SetFocusedRowCellValue("NgaySua", DateTime.Now);
            }
        }

        bool IsDuplication(string fieldName, int index, string value)
        {
            var newValue = value;
            for (int i = 0; i < gv.RowCount - 1; i++)
            {
                if (i == index) continue;
                if (gv.GetRowCellValue(i, fieldName) != null)
                {
                    var oldValue = gv.GetRowCellValue(i, fieldName).ToString();
                    if (oldValue == newValue) return true;
                }
                else return false;
            }
            return false;
        }


        private void grvHangSanXuat_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            var ncc = gv.GetFocusedRowCellValue("TenNhaCungCap");
            if (ncc == null)
            {
                e.ErrorText = "Vui lòng tên nhà cung cấp";
                e.Valid = false;
                return;
            }

            if (IsDuplication("TenNhaCungCap", e.RowHandle, ncc.ToString()))
            {
                e.ErrorText = "Nhà cung cấp này đã có";
                e.Valid = false;
                return;
            }
        }

        private void grvHangSanXuat_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            DialogBox.Error(e.ErrorText);
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        private void itemToaNha_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}