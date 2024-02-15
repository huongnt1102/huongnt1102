using System;
using DevExpress.XtraEditors;
using Library;
using System.Windows.Forms;
using System.Linq;

namespace Building.Asset.DanhMuc
{
    public partial class frmNhomTaiSan_CaiDat : XtraForm
    {
        private MasterDataContext _db;

        public frmNhomTaiSan_CaiDat()
        {
            InitializeComponent();
        }

        private void frmNhomTaiSan_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);

            repositoryItemLookUpEditToaNha.DataSource = Common.TowerList;
            barEditItemToaNha.EditValue = Common.User.MaTN;

            LoadData();
        }

        private void LoadData()
        {
            _db = new MasterDataContext();
            //gridControl.DataSource = _db.tbl_NhomTaiSans.Where(_ => _.MaTN == ((byte?)barEditItemToaNha.EditValue ?? Common.User.MaTN));
            gridControl.DataSource = (from p in _db.tbl_DanhMuc_NhomTaiSans
                //join c in _db.tbl_NhomTaiSans on p.ID equals c.IDDanhMucNhomTaiSan into nhomTaiSan
                //from c in nhomTaiSan.DefaultIfEmpty()
                select new CaiDatNhomTaiSan
                {
                    TenVietTat = p.KyHieu,
                    TenNhomTaiSan = p.TenNhomTaiSan ?? "",
                    DienGiai = p.DienGiai ?? "",
                    IsSuDung = _db.tbl_NhomTaiSans.FirstOrDefault(_ =>
                                   _.IDDanhMucNhomTaiSan == p.ID &
                                   _.MaTN == (byte?) barEditItemToaNha.EditValue) != null
                        ? _db.tbl_NhomTaiSans.First(_ =>
                            _.IDDanhMucNhomTaiSan == p.ID & _.MaTN == (byte?) barEditItemToaNha.EditValue).IsSuDung
                        : false,
                    //ID=c.ID!=null?c.ID:0,
                    //c.MaTN,
                    //c.NgayNhap,
                    //c.NguoiNhap,
                    //c.NgaySua,
                    //c.NguoiSua,
                    //IDDanhMucNhomTaiSan=p.ID,
                    ID = p.ID
                }).ToList();
        }

        public class CaiDatNhomTaiSan
        {
            public string TenVietTat { get; set; }
            public string TenNhomTaiSan { get; set; }
            public string DienGiai { get; set; }

            public int? ID { get; set; }

            public bool? IsSuDung { get; set; }
        }
        private void gridView_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            //gridView.AddNewRow();
            //gridView.SetFocusedRowCellValue("MaTN", ((byte?)barEditItemToaNha.EditValue ?? Common.User.MaTN));
            //gridView.SetFocusedRowCellValue("NgayNhap", DateTime.Now);
            //gridView.SetFocusedRowCellValue("NguoiNhap", Common.User.MaNV);
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
            //var tenVietTat = gridView.GetFocusedRowCellValue("TenVietTat");
            //if (tenVietTat == null)
            //{
            //    e.ErrorText = "Vui lòng nhập ký hiệu Hệ thống";
            //    e.Valid = false;
            //    return;
            //}

            //if (IsDuplication("TenVietTat", e.RowHandle, tenVietTat.ToString()))
            //{
            //    e.ErrorText = "Hệ thống này đã có";
            //    e.Valid = false;
            //    gridView.FocusedRowHandle = e.RowHandle;
            //    return;
            //}
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

        private void repositoryItemCheckEditNgungSuDung_CheckedChanged(object sender, EventArgs e)
        {
            var item = sender as CheckEdit;
            if (item == null) return;
            var Id = (int) gridView.GetFocusedRowCellValue("ID");
            var nts = _db.tbl_NhomTaiSans.FirstOrDefault(_ =>
                _.IDDanhMucNhomTaiSan == (int) gridView.GetFocusedRowCellValue("ID") &
                _.MaTN == ((byte?) barEditItemToaNha.EditValue ?? Common.User.MaTN));
            if (nts != null)
            {
                nts.IsSuDung =(bool) item.EditValue;
                nts.NgaySua = DateTime.Now;
                nts.NguoiSua = Common.User.MaNV;
            }
            else
            {
                var ntsNew = new tbl_NhomTaiSan();
                ntsNew.MaTN = (byte?) barEditItemToaNha.EditValue ?? Common.User.MaTN;
                ntsNew.TenVietTat = gridView.GetFocusedRowCellValue("TenVietTat").ToString();
                ntsNew.TenNhomTaiSan = gridView.GetFocusedRowCellValue("TenNhomTaiSan").ToString();
                ntsNew.DienGiai = gridView.GetFocusedRowCellValue("DienGiai").ToString();
                ntsNew.NgayNhap = DateTime.Now;
                ntsNew.NguoiNhap = Common.User.MaNV;
                ntsNew.IDDanhMucNhomTaiSan = (int) gridView.GetFocusedRowCellValue("ID");
                ntsNew.IsSuDung = (bool) item.EditValue;
                _db.tbl_NhomTaiSans.InsertOnSubmit(ntsNew);

            }
            _db.SubmitChanges();
        }

        private void barEditItemToaNha_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}