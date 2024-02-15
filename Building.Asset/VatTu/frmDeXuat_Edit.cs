using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Library;
using System.Linq;
using System.Text.RegularExpressions;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;

namespace Building.Asset.VatTu
{
    public partial class frmDeXuat_Edit: XtraForm
    {
        public byte MaTn { get; set; }
        public int? IsSua { get; set; } // 0: thêm, 1: sửa
        public long? Id { get; set; } // id phieu, dung cho sửa, thêm thì =0
        public int? MaNv { get; set; }

        private MasterDataContext _db;
        private tbl_VatTu_DeXuat _o;

        private System.Collections.Generic.List<Library.Class.HuongDan.ShowAuto.ControlItem> controls;

        public frmDeXuat_Edit()
        {
            InitializeComponent();
            TranslateLanguage.TranslateControl(this);
        }
        private void frmEdit_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            LoadData();
        }

        private void LoadData()
        {

            controls = Library.Class.HuongDan.ShowAuto.GetControlItems(this.Controls);
            Library.Class.HuongDan.ShowAuto.ClearText(this.Controls);

            _db = new MasterDataContext();

            dateNgayPhieu.DateTime = DateTime.Now;
            txtDienGiai.Text = "";
            txtMaPhieu.Text = GetMaDeXuat(DateTime.Now);

            glkNguoiNhan.Properties.DataSource = _db.tnNhanViens;
            glkNguoiNhan.EditValue = Common.User.MaNV;

            repTaiSan.DataSource = (from _ in _db.tbl_VatTus
                where _.MaTN == MaTn & _.NgungSuDung == false
                select new
                {
                    _.ID, _.KyHieu, _.Ten, _.tbl_VatTu_DVT.TenDVT
                }).ToList();

            repPhieuDeXuatSuaChua.DataSource = _db.tbl_DeXuatSuaChuas.Where(_ => _.MaTN == MaTn)
                .Select(_ => new {_.ID, _.SoPhieu, _.Ngay}).ToList();

            if (IsSua == 0)
            {
                _o = new tbl_VatTu_DeXuat();
                // thêm phiếu
                if (Id != 0)
                {
                    var obj = _db.tbl_VatTu_DeXuats.FirstOrDefault(_ => _.ID == Id);
                    if (obj != null)
                    {
                        txtDienGiai.Text = obj.DienGiai;
                    }
                }
            }
            else
            {
                _o = _db.tbl_VatTu_DeXuats.FirstOrDefault(_ => _.ID == (int) Id);
                if (_o != null)
                {
                    txtMaPhieu.Text = _o.MaPhieu;
                    if (_o.NgayPhieu != null) dateNgayPhieu.DateTime = (DateTime) _o.NgayPhieu;
                    glkNguoiNhan.EditValue = _o.NguoiNhan;
                    txtDienGiai.Text = _o.DienGiai;
                }
                else
                {
                    _o = new tbl_VatTu_DeXuat();
                }
            }

            gcTaiSan.DataSource = _o.tbl_VatTu_DeXuat_ChiTiets;
            gcPhieuSuaChua.DataSource = _o.tbl_VatTu_DeXuat_PhieuSuaChuas;

            itemHuongDan.ItemClick += ItemHuongDan_ItemClick;
            itemClearText.ItemClick += ItemClearText_ItemClick;
        }

        private void ItemClearText_ItemClick(object sender, ItemClickEventArgs e)
        {
            Library.Class.HuongDan.ShowAuto.ClearText(this.Controls);
        }

        private void ItemHuongDan_ItemClick(object sender, ItemClickEventArgs e)
        {
            Library.Class.HuongDan.ShowAuto.ActiveDemo(true, controls);
        }

        private string GetMaDeXuat(DateTime ngayPhieu)
        {
            _db = new MasterDataContext();
            var o = _db.tbl_VatTu_DeXuats.Where(_ => _.MaTN == MaTn & _.NgayPhieu.Value.Year == ngayPhieu.Year).Select(_ => _.MaPhieu).ToList();
            return Common.TaoMa(o, "PDX/");
        }
        

        private void itemHuy_ItemClick(object sender, ItemClickEventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
 
        private void itemLuu_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                gvTaiSan.UpdateCurrentRow();
                gvTaiSan.PostEditor();

                #region kiểm tra

                if (glkNguoiNhan.EditValue == null)
                {
                    DialogBox.Error("Vui lòng chọn nhân viên");
                    return;
                }

                #endregion

                _o.MaPhieu = txtMaPhieu.Text;
                _o.NgayPhieu = dateNgayPhieu.DateTime;
                _o.TrangThaiID = 1;
                _o.DienGiai = txtDienGiai.Text;
                _o.NguoiNhan = (int) glkNguoiNhan.EditValue;

                if (IsSua == 0)
                {
                    _o.NguoiNhap = Common.User.MaNV;
                    _o.NgayNhap = DateTime.Now;
                    _o.MaTN = MaTn;
                    _db.tbl_VatTu_DeXuats.InsertOnSubmit(_o);
                }
                else
                {
                    _o.NgaySua = DateTime.Now;
                    _o.NguoiSua = Common.User.MaNV;
                }

                _db.SubmitChanges();

                var listIdPsc = "";
                foreach (var i in _o.tbl_VatTu_DeXuat_PhieuSuaChuas)
                {
                    listIdPsc += i.PhieuDeXuatSuaChuaID + ",";
                }

                listIdPsc = listIdPsc.TrimEnd(',');
                _o.PhieuDeXuatSuaChuaIDs = listIdPsc;
                _db.SubmitChanges();

                DialogResult = DialogResult.OK;
                DialogBox.Alert("Đã lưu thành công");

            }
            catch (Exception)
            {
                DialogResult = DialogResult.Cancel;
                DialogBox.Error("Không lưu được, vui lòng kiểm tra lại");
            }
        }

        private void bbiXoa_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(gvTaiSan.GetFocusedDataSourceRowIndex().ToString()))
                {
                    gvTaiSan.DeleteSelectedRows();
                }
                if (!string.IsNullOrEmpty(gvPhieuSuaChua.GetFocusedDataSourceRowIndex().ToString()))
                {
                    gvPhieuSuaChua.DeleteSelectedRows();
                }
            }
            catch (Exception)
            {
                //throw;
            }
        }

        private void gvTaiSan_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            gvTaiSan.SetFocusedRowCellValue("ID",0);
            gvTaiSan.SetFocusedRowCellValue("SoLuongDuyet", 0);
            gvTaiSan.SetFocusedRowCellValue("SoLuongNhapKho", 0);
            gvTaiSan.SetFocusedRowCellValue("SoLuongDeXuat", 0);
            gvTaiSan.SetFocusedRowCellValue("SoLuongMuaHang", 0);
        }

        private void gvTaiSan_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (!string.IsNullOrEmpty(gvTaiSan.GetSelectedRows().ToString()))
                {
                    gvTaiSan.DeleteSelectedRows();
                }
                if (!string.IsNullOrEmpty(gvPhieuSuaChua.GetSelectedRows().ToString()))
                {
                    gvPhieuSuaChua.DeleteSelectedRows();
                }
            }
        }

        private void repTaiSan_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                var item = sender as GridLookUpEdit;
                if (item.EditValue == null)
                {
                    DialogBox.Alert("Vui lòng chọn Tài Sản");
                    return;
                }

                if (!IsDuplication("VatTuID", gvTaiSan.FocusedRowHandle, item.EditValue.ToString()))
                {
                    gvTaiSan.SetFocusedRowCellValue("TenDVT", item.Properties.View.GetFocusedRowCellValue("TenDVT"));
                    gvTaiSan.SetFocusedRowCellValue("VatTuID", item.EditValue.ToString());
                    gvTaiSan.UpdateCurrentRow();
                    return;
                }
                DialogBox.Error("Trùng vật tư, vui lòng chọn vật tư khác.");
                gvTaiSan.DeleteSelectedRows();
                gvTaiSan.UpdateCurrentRow();
                gvTaiSan.FocusedRowHandle = -1;
            }
            catch (Exception)
            {
                //throw;
            }
        }

        private bool IsDuplication(string fielName, int index, string value)
        {
            for (var i = 0; i < gvTaiSan.RowCount - 1; i++)
            {
                if (i == index) continue;
                if (gvTaiSan.GetRowCellValue(i, fielName) != null)
                {
                    var oldValue = gvTaiSan.GetRowCellValue(i, fielName).ToString();
                    if (oldValue == value) return true;
                }
            }
            return false;
        }

        private void repositoryItemSpinEdit1_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            //var item = sender as SpinEdit;
            //if (item == null) return;
            //gvTaiSan.SetFocusedRowCellValue("SoLuongDuyet", item.EditValue);
        }

        private void gvPhieuVanHanh_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            gvPhieuSuaChua.SetFocusedRowCellValue("ID", 0);
        }

        private void repPhieuSuaChua_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                var item = sender as GridLookUpEdit;
                if (item.EditValue == null) return;
                gvPhieuSuaChua.SetFocusedRowCellValue("PhieuDeXuatSuaChuaID", item.EditValue.ToString());
                gvPhieuSuaChua.SetFocusedRowCellValue("MaPhieuDeXuatSuaChua",
                    item.Properties.View.GetFocusedRowCellValue("SoPhieu"));
                gvPhieuSuaChua.UpdateCurrentRow();
            }
            catch
            {
                //
            }
        }
    }
}