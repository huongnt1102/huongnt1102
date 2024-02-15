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
    public partial class frmNhapKho_TuMuaHang: XtraForm
    {
        public byte MaTn { get; set; }
        public int? IsSua { get; set; } 
        public long? Id { get; set; } 
        public int? MaNv { get; set; }

        private MasterDataContext _db;
        private tbl_VatTu_NhapKho _o;
        private List<tbl_VatTu_MuaHang_ChiTiet> _lCtMh;
        private List<tbl_VatTu_MuaHang> _lMh;

        private System.Collections.Generic.List<Library.Class.HuongDan.ShowAuto.ControlItem> controls;

        public frmNhapKho_TuMuaHang()
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

            dateNgayNhap.DateTime = DateTime.Now;
            txtDienGiai.Text = "";
            txtMaPhieu.Text = GetMaNhapKho(DateTime.Now);

            glkNguoiNhap.Properties.DataSource = _db.tnNhanViens;
            glkNguoiNhap.EditValue = Common.User.MaNV;

            _lMh = _db.tbl_VatTu_MuaHangs.Where(_ => _.MaTN == MaTn).ToList();

            glkPhieuMuaHang.Properties.DataSource = _lMh.Where(_ => _.TrangThaiNhapKhoID != 3).ToList();

            _lCtMh = _db.tbl_VatTu_MuaHang_ChiTiets.ToList();

            glkKho.Properties.DataSource = _db.tbl_VatTu_Khos;
            glkLoaiNhap.Properties.DataSource = _db.tbl_VatTu_NhapKho_LoaiNhaps;
            glkLoaiNhap.EditValue = glkLoaiNhap.Properties.GetKeyValue(0);

            if (IsSua == 0)
            {
                _o = new tbl_VatTu_NhapKho();
                if (Id != 0)
                {
                    var obj = _db.tbl_VatTu_NhapKhos.FirstOrDefault(_ => _.ID == Id);
                    if (obj != null)
                    {
                        txtDienGiai.Text = obj.LyDoNhap;
                    }
                }
            }
            else
            {
                _o = _db.tbl_VatTu_NhapKhos.FirstOrDefault(_ => _.ID == (int) Id);
                if (_o != null)
                {
                    foreach (var i in _o.tbl_VatTu_NhapKho_ChiTiets)
                    {
                        var l1 = _lCtMh.FirstOrDefault(_ => _.ID == i.MuaHangChiTietID);
                        if (l1 != null)
                        {
                            l1.SoLuongNhapKho = l1.SoLuongNhapKho - i.SoLuong;
                        }
                    }

                    var l2 = _lMh.First(_ => _.ID == _o.MuaHangID);
                    var kt = _lCtMh.Where(_ => _.SoLuongNhapKho>0 & _.MuaHangID == l2.ID).ToList();
                    if (kt.Count != 0)
                    {
                        var kt1 = _lCtMh.Where(_ => _.SoLuong > _.SoLuongNhapKho).ToList();
                        l2.TrangThaiNhapKhoID = kt1.Count > 0 ? 2 : 3;
                    }
                    else
                    {
                        l2.TrangThaiNhapKhoID = 1;
                    }

                    glkPhieuMuaHang.Properties.DataSource = _lMh.Where(_ => _.TrangThaiNhapKhoID != 3 || _.ID == _o.MuaHangID).ToList();
                    glkPhieuMuaHang.EditValue = _o.MuaHangID;

                    txtMaPhieu.Text = _o.SoCT;
                    if (_o.NgayNhapKho != null) dateNgayNhap.DateTime = (DateTime) _o.NgayNhapKho;
                    glkNguoiNhap.EditValue = _o.NguoiNhapKho;
                    txtDienGiai.Text = _o.LyDoNhap;
                    glkKho.EditValue = _o.KhoID;

                    glkPhieuMuaHang.Properties.ReadOnly = true;
                }
                else
                {
                    _o = new tbl_VatTu_NhapKho();
                }
            }

            gcTaiSan.DataSource = _o.tbl_VatTu_NhapKho_ChiTiets;

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

        private string GetMaNhapKho( DateTime ngayPhieu)
        {
            _db = new MasterDataContext();
            var o = _db.tbl_VatTu_NhapKhos.Where(_ => _.MaTN == MaTn & _.NgayNhapKho.Value.Year == ngayPhieu.Year).Select(_ => _.SoCT).ToList();
            return Common.TaoMa(o, "PNK/");
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

                _db.tbl_VatTu_NhapKho_ChiTiets.DeleteAllOnSubmit(
                    _db.tbl_VatTu_NhapKho_ChiTiets.Where(_ => _.NhapKhoID == null));
                //_db.tbl_VatTu_SoKhos.DeleteAllOnSubmit(_db.tbl_VatTu_SoKhos.Where(_ => _.IDPhieu == _o.ID));

                #region kiểm tra

                if (glkNguoiNhap.EditValue == null)
                {
                    DialogBox.Error("Vui lòng chọn nhân viên");
                    return;
                }

                if (glkPhieuMuaHang.EditValue == null)
                {
                    DialogBox.Error("Vui lòng chọn phiếu đề xuất");
                    return;
                }

                if (glkKho.EditValue == null)
                {
                    DialogBox.Error("Vui lòng chọn nhà cung cấp");
                    return;
                }
                if (glkLoaiNhap.EditValue == null)
                {
                    DialogBox.Error("Vui lòng chọn loại nhập");
                    return;
                }

                #endregion

                _o.SoCT = txtMaPhieu.Text;
                _o.NgayNhapKho = dateNgayNhap.DateTime;
                _o.LyDoNhap = txtDienGiai.Text;
                _o.NguoiNhapKho = (int) glkNguoiNhap.EditValue;
                _o.KhoID = (int) glkKho.EditValue;
                _o.ChungTuGoc = txtChungTuGoc.Text;
                _o.MuaHangID = (long?) glkPhieuMuaHang.EditValue;
                _o.LoaiNhapID = (int?) glkLoaiNhap.EditValue;

                if (IsSua == 0)
                {
                    _o.NguoiNhap = Common.User.MaNV;
                    _o.NgayNhap = DateTime.Now;
                    _o.MaTN = MaTn;
                    _db.tbl_VatTu_NhapKhos.InsertOnSubmit(_o);
                }
                else
                {
                    _o.NgaySua = DateTime.Now;
                    _o.NguoiSua = Common.User.MaNV;
                }

                _db.SubmitChanges();

                // cập nhật lại số lượng nhập kho trong phiếu mua hàng
                foreach (var i in _o.tbl_VatTu_NhapKho_ChiTiets)
                {
                    var ctMuaHang = _lCtMh.FirstOrDefault(_ => _.ID == i.MuaHangChiTietID);
                    if (ctMuaHang != null)
                    {
                        var soLuongNhap = _db.tbl_VatTu_NhapKho_ChiTiets.Where(_ => _.MuaHangChiTietID == ctMuaHang.ID).Sum(_ => _.SoLuong).GetValueOrDefault();
                        ctMuaHang.SoLuongNhapKho = soLuongNhap;

                        if (i.DeXuatChiTietID != null)
                        {
                            var dexuat = _db.tbl_VatTu_DeXuat_ChiTiets.First(_ => _.ID == i.DeXuatChiTietID);
                            dexuat.SoLuongNhapKho = soLuongNhap;
                        }
                    }
                }
                // cập nhật trạng thái phiếu mua hàng
                var objMuaHang = _lMh.FirstOrDefault(_ => _.ID == (long)glkPhieuMuaHang.EditValue);
                if (objMuaHang != null)
                {
                    var kt2 = objMuaHang.tbl_VatTu_MuaHang_ChiTiets.Where(
                        _ => _.SoLuongNhapKho.GetValueOrDefault() != 0);
                    if (kt2.Count() != 0)
                    {
                        var kt = objMuaHang.tbl_VatTu_MuaHang_ChiTiets.Where(_ => _.SoLuong > _.SoLuongNhapKho);
                        objMuaHang.TrangThaiNhapKhoID = kt.Count() > 0 ? 2 : 3;
                    }
                    else
                    {
                        objMuaHang.TrangThaiNhapKhoID = 1;
                    }
                }

                // cập nhật sổ kho
                //var l1 = _db.tbl_VatTu_SoKhos.Where(_ => _.IDPhieu == _o.ID).ToList();
                DataTable dt = _o.tbl_VatTu_NhapKho_ChiTiets.ToList().ConvertToDataTable();

                if (dt != null)
                {
                    List<tbl_VatTu_SoKho> lCt = dt.AsEnumerable().Select(m => new tbl_VatTu_SoKho
                    {
                        NgayPhieu=_o.NgayNhapKho,
                        SoPhieu=_o.SoCT,
                        IDPhieu=_o.ID,
                        IDPhieuChiTiet=m.Field<long>("ID"),
                        SoLuong=m.Field<decimal>("SoLuong"),
                        DonGia=m.Field<decimal>("DonGia"),
                        ThanhTien=m.Field<decimal>("ThanhTien"),
                        MaLoaiNhapXuat=1,
                        MaTN=MaTn,
                        NgayNhap=_o.NgayNhap,
                        NguoiNhap=_o.NguoiNhap,
                        KhoID=_o.KhoID,
                        VatTuID=m.Field<long>("VatTuID"),
                    }).ToList();

                    var l = _db.tbl_VatTu_SoKhos;
                    var l1 = l.Where(_ => _.IDPhieu == _o.ID).ToList();

                    // vong for 1, kt lct khong co trong l1, for l1, neu kt thay khong co trong lct, delete l1

                    foreach (var i in l1)
                    {
                        var obj = lCt.FirstOrDefault(_ => _.IDPhieu == i.IDPhieu & _.IDPhieuChiTiet == i.IDPhieuChiTiet);
                        if (obj == null)
                        {
                            l.DeleteOnSubmit(i);
                        }
                    }

                    foreach (var i in lCt)
                    {
                        var obj = l1.FirstOrDefault(_ => _.IDPhieu == i.IDPhieu & _.IDPhieuChiTiet == i.IDPhieuChiTiet);
                        if (obj == null)
                        {
                            l.InsertOnSubmit(i);
                        }
                        else
                        {
                            // update
                            obj.NgayPhieu = i.NgayPhieu;
                            obj.SoPhieu = i.SoPhieu;
                            obj.SoLuong = i.SoLuong;
                            obj.DonGia = i.DonGia;
                            obj.ThanhTien = i.ThanhTien;
                            obj.KhoID = i.KhoID;
                            obj.VatTuID = i.VatTuID;
                            obj.NguoiSua = Common.User.MaNV;
                            obj.NgaySua = DateTime.Now;
                        }
                    }
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
                    gvTaiSan.SetFocusedRowCellValue("SoLuongMuaHang", item.Properties.View.GetFocusedRowCellValue("SoLuongConLai"));
                    gvTaiSan.SetFocusedRowCellValue("SoLuong", item.Properties.View.GetFocusedRowCellValue("SoLuongConLai"));
                    gvTaiSan.SetFocusedRowCellValue("DonGia", item.Properties.View.GetFocusedRowCellValue("DonGia"));
                    gvTaiSan.SetFocusedRowCellValue("ThanhTien", item.Properties.View.GetFocusedRowCellValue("ThanhTien"));
                    gvTaiSan.SetFocusedRowCellValue("DeXuatChiTietID", item.Properties.View.GetFocusedRowCellValue("DeXuatChiTietID"));
                    gvTaiSan.SetFocusedRowCellValue("MuaHangChiTietID", item.Properties.View.GetFocusedRowCellValue("MuaHangChiTietID"));
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

            repTaiSan.DataSource = (from _ in _lCtMh
                where _.MuaHangID == (long?) item.EditValue &
                      (_.SoLuong.GetValueOrDefault() > _.SoLuongNhapKho.GetValueOrDefault())
                select new
                {
                    _.tbl_VatTu.ID,_.tbl_VatTu.KyHieu,_.tbl_VatTu.tbl_VatTu_DVT.TenDVT,_.tbl_VatTu.Ten,
                    SoLuongConLai=_.SoLuong.GetValueOrDefault()-_.SoLuongNhapKho.GetValueOrDefault(),
                    _.DonGia,
                    _.ThanhTien,
                    _.DeXuatChiTietID,
                    MuaHangChiTietID = _.ID
                }).ToList();
            _o.DeXuatID = _lMh.First(_ => _.ID == (long) item.EditValue).DeXuatID;
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
            var soLuongMua = (decimal?)gvTaiSan.GetFocusedRowCellValue("SoLuongMuaHang");
            if ((decimal) item.EditValue > soLuongMua)
                item.Value = soLuongMua.Value;
        }
    }
}