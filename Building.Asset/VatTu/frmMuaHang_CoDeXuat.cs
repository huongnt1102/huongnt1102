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
    public partial class frmMuaHang_CoDeXuat: XtraForm
    {
        public byte MaTn { get; set; }
        public int? IsSua { get; set; } // 0: thêm, 1: sửa
        public long? Id { get; set; } // id phieu, dung cho sửa, thêm thì =0
        public int? MaNv { get; set; }

        private MasterDataContext _db;
        private tbl_VatTu_MuaHang _o;
        private List<tbl_VatTu_DeXuat_ChiTiet> _lCt;
        private List<tbl_VatTu_DeXuat> _l;
        private System.Collections.Generic.List<Library.Class.HuongDan.ShowAuto.ControlItem> controls;

        public frmMuaHang_CoDeXuat()
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
            txtMaPhieu.Text = GetMaMuaHang(DateTime.Now);

            glkNguoiNhan.Properties.DataSource = _db.tnNhanViens;
            glkNguoiNhan.EditValue = Common.User.MaNV;

            _l = _db.tbl_VatTu_DeXuats.Where(_ => _.MaTN == MaTn & _.TrangThaiID!=1).ToList();

            glkPhieuDeXuat.Properties.DataSource = _l.Where(_ => _.TrangThaiID != 4).ToList();

            _lCt = _db.tbl_VatTu_DeXuat_ChiTiets.ToList();
                
            glkNhaCungCap.Properties.DataSource = _db.tbl_NhaCungCapTaiSans;

            if (IsSua == 0)
            {
                _o = new tbl_VatTu_MuaHang();
                // thêm phiếu
                if (Id != 0)
                {
                    var obj = _db.tbl_VatTu_MuaHangs.FirstOrDefault(_ => _.ID == Id);
                    if (obj != null)
                    {
                        txtDienGiai.Text = obj.DienGiai;
                    }
                }
            }
            else
            {
                _o = _db.tbl_VatTu_MuaHangs.FirstOrDefault(_ => _.ID == (int) Id);
                if (_o != null)
                {
                    foreach (var i in _o.tbl_VatTu_MuaHang_ChiTiets)
                    {
                        var l1 = _lCt.FirstOrDefault(_ => _.ID == i.DeXuatChiTietID);
                        if (l1 != null)
                        {
                            l1.SoLuongMuaHang = l1.SoLuongMuaHang - i.SoLuong;
                        }
                    }

                    var l2 = _l.FirstOrDefault(_ => _.ID == _o.DeXuatID);
                    var kt = _lCt.Where(_ => l2 != null && _.SoLuongDuyet - _.SoLuongMuaHang > 0 & _.DeXuatID == l2.ID).ToList();
                    if (l2 != null) l2.TrangThaiID = kt.Count > 0 ? 3 : 4;

                    glkPhieuDeXuat.Properties.DataSource = _l.Where(_ => _.TrangThaiID != 4 || _.ID == _o.DeXuatID).ToList();
                    glkPhieuDeXuat.EditValue = _o.DeXuatID;

                    txtMaPhieu.Text = _o.MaPhieu;
                    if (_o.NgayPhieu != null) dateNgayPhieu.DateTime = (DateTime) _o.NgayPhieu;
                    glkNguoiNhan.EditValue = _o.NguoiMua;
                    txtDienGiai.Text = _o.DienGiai;
                    glkNhaCungCap.EditValue = _o.MaNCC;

                    glkPhieuDeXuat.Properties.ReadOnly = true;
                }
                else
                {
                    _o = new tbl_VatTu_MuaHang();
                }
            }

            gcTaiSan.DataSource = _o.tbl_VatTu_MuaHang_ChiTiets;

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

        private string GetMaMuaHang(DateTime ngayPhieu)
        {
            _db = new MasterDataContext();
            var o = _db.tbl_VatTu_MuaHangs.Where(_ => _.MaTN == MaTn & _.NgayPhieu.Value.Year == ngayPhieu.Year).Select(_ => _.MaPhieu).ToList();
            return Common.TaoMa(o, "PMH/");
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

                _db.tbl_VatTu_MuaHang_ChiTiets.DeleteAllOnSubmit(
                    _db.tbl_VatTu_MuaHang_ChiTiets.Where(_ => _.MuaHangID == null));

                #region kiểm tra

                if (glkNguoiNhan.EditValue == null)
                {
                    DialogBox.Error("Vui lòng chọn nhân viên");
                    return;
                }

                if (glkPhieuDeXuat.EditValue == null)
                {
                    DialogBox.Error("Vui lòng chọn phiếu đề xuất");
                    return;
                }

                if (glkNhaCungCap.EditValue == null)
                {
                    DialogBox.Error("Vui lòng chọn nhà cung cấp");
                    return;
                }

                #endregion

                _o.MaPhieu = txtMaPhieu.Text;
                _o.NgayPhieu = dateNgayPhieu.DateTime;
                _o.TrangThaiTraTienID = 1;
                _o.DienGiai = txtDienGiai.Text;
                _o.NguoiMua = (int) glkNguoiNhan.EditValue;
                _o.MaNCC = (int) glkNhaCungCap.EditValue;
                _o.ChungTuGoc = txtChungTuGoc.Text;
                _o.TongTienPhieu = _o.tbl_VatTu_MuaHang_ChiTiets.Sum(_ => _.ThanhTien);
                _o.TongTienDaTra = 0;
                _o.TongTienConLai = _o.TongTienPhieu;
                _o.DeXuatID = (long?) glkPhieuDeXuat.EditValue;
                _o.TrangThaiNhapKhoID = 1;

                if (IsSua == 0)
                {
                    _o.NguoiNhap = Common.User.MaNV;
                    _o.NgayNhap = DateTime.Now;
                    _o.MaTN = MaTn;
                    _db.tbl_VatTu_MuaHangs.InsertOnSubmit(_o);
                }
                else
                {
                    _o.NgaySua = DateTime.Now;
                    _o.NguoiSua = Common.User.MaNV;
                }

                _db.SubmitChanges();

                // cập nhật lại số lượng mua hàng trong phiếu đề xuất
                foreach (var i in _o.tbl_VatTu_MuaHang_ChiTiets)
                {
                    var ctDeXuat = _lCt.FirstOrDefault(_ => _.ID == i.DeXuatChiTietID);
                    if (ctDeXuat != null)
                    {
                        var soLuongMua = _db.tbl_VatTu_MuaHang_ChiTiets.Where(_ => _.DeXuatChiTietID == ctDeXuat.ID).Sum(_=>_.SoLuong).GetValueOrDefault();
                        ctDeXuat.SoLuongMuaHang = soLuongMua;
                    }
                }
                // cập nhật trạng thái phiếu đề xuất
                var objDeXuat = _l.FirstOrDefault(_ => _.ID == (long) glkPhieuDeXuat.EditValue);
                if (objDeXuat != null)
                {
                    var kt = objDeXuat.tbl_VatTu_DeXuat_ChiTiets.Where(_ =>(_.SoLuongDuyet - _.SoLuongMuaHang) > 0);
                    objDeXuat.TrangThaiID = kt.Count()>0 ? 3 : 4;
                }

                _db.SubmitChanges();

                DialogResult = DialogResult.OK;
                DialogBox.Alert("Đã lưu thành công");

            }
            catch (Exception)
            {
                //DialogResult = DialogResult.Cancel;
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
            }
            catch (Exception)
            {
                //throw;
            }
        }

        private void gvTaiSan_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            gvTaiSan.SetFocusedRowCellValue("ID",0);
            gvTaiSan.SetFocusedRowCellValue("ThanhTien", 0);
            gvTaiSan.SetFocusedRowCellValue("SoLuongNhapKho", 0);
        }

        private void gvTaiSan_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (!string.IsNullOrEmpty(gvTaiSan.GetSelectedRows().ToString()))
                {
                    gvTaiSan.DeleteSelectedRows();
                }
            }
        }

        private void repTaiSan_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                var item = sender as GridLookUpEdit;
                if (item == null)
                {
                    DialogBox.Alert("Vui lòng chọn Tài Sản");
                    return;
                }

                if (!IsDuplication("VatTuID", gvTaiSan.FocusedRowHandle, item.EditValue.ToString()))
                {
                    gvTaiSan.SetFocusedRowCellValue("TenDVT", item.Properties.View.GetFocusedRowCellValue("TenDVT"));
                    gvTaiSan.SetFocusedRowCellValue("VatTuID", item.EditValue.ToString());
                    gvTaiSan.SetFocusedRowCellValue("SoLuongDeXuat", item.Properties.View.GetFocusedRowCellValue("SoLuongConLai"));
                    gvTaiSan.SetFocusedRowCellValue("SoLuong", item.Properties.View.GetFocusedRowCellValue("SoLuongConLai"));
                    gvTaiSan.SetFocusedRowCellValue("DonGia", item.Properties.View.GetFocusedRowCellValue("GiaNhap"));
                    gvTaiSan.SetFocusedRowCellValue("DeXuatChiTietID", item.Properties.View.GetFocusedRowCellValue("DeXuatChiTietID"));
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

        private void glkPhieuDeXuat_EditValueChanged(object sender, EventArgs e)
        {
            var item = sender as GridLookUpEdit;
            if (item == null) return;

            repTaiSan.DataSource = (from _ in _lCt
                where _.DeXuatID == (long?) item.EditValue & ((_.SoLuongDuyet - _.SoLuongMuaHang) > 0)
                select new
                {
                    _.tbl_VatTu.ID,
                    _.tbl_VatTu.KyHieu,
                    _.tbl_VatTu.Ten,
                    _.tbl_VatTu.tbl_VatTu_DVT.TenDVT,
                    SoLuongConLai = _.SoLuongDuyet - _.SoLuongMuaHang,
                    _.tbl_VatTu.GiaNhap,
                    DeXuatChiTietID = _.ID
                }).ToList();
        }

        private void gvTaiSan_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "SoLuong" || e.Column.FieldName == "DonGia")
            {
                if (!string.IsNullOrEmpty(gvTaiSan.GetFocusedRowCellDisplayText("SoLuong")) &&
                    !string.IsNullOrEmpty(gvTaiSan.GetFocusedRowCellDisplayText("DonGia")))
                {
                    var sl = Convert.ToDecimal(gvTaiSan.GetFocusedRowCellDisplayText("SoLuong"));
                    var dg = Convert.ToDecimal(gvTaiSan.GetFocusedRowCellDisplayText("DonGia"));
                    gvTaiSan.SetFocusedRowCellValue("ThanhTien", sl * dg);

                    gvTaiSan.PostEditor();
                    gvTaiSan.UpdateCurrentRow();
                    gvTaiSan.UpdateSummary();
                }
            }
        }

        private void spinSoLuongMua_EditValueChanged(object sender, EventArgs e)
        {
            var item = sender as SpinEdit;
            if (item == null) return;
            var soLuongDeXuat = (decimal?)gvTaiSan.GetFocusedRowCellValue("SoLuongDeXuat");
            if ((decimal) item.EditValue > soLuongDeXuat)
                item.Value = soLuongDeXuat.Value;
        }
    }
}