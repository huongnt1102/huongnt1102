using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Library;
using System.Linq;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;

namespace Building.Asset.PhanCong
{
    public partial class frmGiaoCa_Edit: XtraForm
    {
        public byte MaTn { get; set; }
        public int? IsSua { get; set; } // 0: thêm, 1: sửa
        public int? Id { get; set; } // id phieu, dung cho sửa, thêm thì =0
        public int? MaNv { get; set; }

        private MasterDataContext _db;
        private tbl_PhanCong_BanGiaoCa _o;
        private bool _isKhoaNoiDung=false;
        private List<tbl_PhanCong_BanGiaoCa_ThietBi> _lCt;

        private System.Collections.Generic.List<Library.Class.HuongDan.ShowAuto.ControlItem> controls;

        public frmGiaoCa_Edit()
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

            txtMaSo.Text = "";

            glkCa.Properties.DataSource = _db.tbl_PhanCong_PhanLoaiCas;
            dateNgayNhan.DateTime = DateTime.Now;

            repTaiSan.DataSource = _db.tbl_PhanCong_ThietBis
                .Select(_ => new {_.ID, _.KyHieu, _.Name, _.SoLuong, _.tbl_TinhTrangTaiSan.TenTinhTrang,_.IDTinhTrangTaiSan}).ToList();
            repTinhTrangTaiSan.DataSource = _db.tbl_TinhTrangTaiSans;

            if (IsSua == 0)
            {
                _o = new tbl_PhanCong_BanGiaoCa();
                // thêm phiếu
                if (Id != 0)
                {
                    var obj = _db.tbl_PhanCong_BanGiaoCas.FirstOrDefault(_ => _.ID == Id);
                    if (obj != null)
                    {
                        glkCa.EditValue = obj.IDLoaiCa;
                        glkNhanVien.EditValue = obj.NguoiNhan;
                        _isKhoaNoiDung = true;
                    }
                }
            }
            else
            {
                _o = _db.tbl_PhanCong_BanGiaoCas.FirstOrDefault(_ => _.ID == (int) Id);
                if (_o != null)
                {
                    glkCa.EditValue = _o.IDLoaiCa;
                    glkNhanVien.EditValue = _o.NguoiNhan;
                    txtMaSo.Text = _o.MaSoPhieu;
                    if (_o.NgayNhan != null) dateNgayNhan.DateTime = (DateTime) _o.NgayNhan;
                    _lCt = _o.tbl_PhanCong_BanGiaoCa_ThietBis.ToList();
                }
                else
                {
                    _o = new tbl_PhanCong_BanGiaoCa();
                }
            }

            gcTaiSan.DataSource = _o.tbl_PhanCong_BanGiaoCa_ThietBis;

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

                if (glkCa.EditValue == null)
                {
                    DialogBox.Error("Vui lòng chọn Ca");
                    return;
                }

                if (glkNhanVien.EditValue == null)
                {
                    DialogBox.Error("Vui lòng chọn nhân viên");
                    return;
                }

                #endregion

                var db = new MasterDataContext();
                if (IsSua == 1)
                {
                    foreach (var s in _lCt)
                    {
                        var dlO = db.tbl_PhanCong_BanGiaoCa_ThietBis.FirstOrDefault(_ => _.ID == s.ID);
                        if (dlO != null)
                        {
                            var tb = db.tbl_PhanCong_ThietBis.FirstOrDefault(_ =>
                                _.ID == dlO.IDThietBi & _.IDTinhTrangTaiSan == dlO.IDTinhTrangTaiSanNhan);
                            if (tb != null)
                            {
                                tb.SoLuong = tb.SoLuong - dlO.SoLuongNhan;
                            }
                            db.SubmitChanges();
                        }
                    }

                    db.Dispose();
                }

                // cập nhật lại bảng công cụ thiết bị
                db = new MasterDataContext();
                for (var i = 0; i < gvTaiSan.RowCount - 1; i++)
                {
                    if (!string.IsNullOrEmpty(gvTaiSan.GetRowCellDisplayText(i, "IDThietBi")))
                    {
                        var objThietBi = db.tbl_PhanCong_ThietBis.FirstOrDefault(_ =>
                            _.ID == int.Parse(gvTaiSan.GetRowCellValue(i, "IDThietBi").ToString()) &
                            _.IDTinhTrangTaiSan ==
                            int.Parse(gvTaiSan.GetRowCellValue(i, "IDTinhTrangTaiSanNhan").ToString()));
                        if (objThietBi != null)
                        {
                            objThietBi.SoLuong = objThietBi.SoLuong +
                                                 decimal.Parse(gvTaiSan.GetRowCellValue(i, "SoLuongNhan").ToString());
                            db.SubmitChanges();
                        }
                    }
                }

                var nhanVienChiTiet = _db.tbl_PhanCong_NhanVienChiTiets.FirstOrDefault(_ =>
                    _.IDPhanLoaiCa == (int) glkCa.EditValue & _.MaNV == (int) glkNhanVien.EditValue &
                    _.Ngay.Value.Date == dateNgayNhan.DateTime.Date);
                if (nhanVienChiTiet != null)
                    _o.IDPhanCongNhanVienChiTiet = nhanVienChiTiet.ID;

                _o.IDLoaiCa = (int) glkCa.EditValue;
                _o.IDLoaiNhan = 2;
                _o.MaSoPhieu = txtMaSo.Text;
                _o.NgayNhan = dateNgayNhan.DateTime;
                _o.NguoiNhan = (int) glkNhanVien.EditValue;

                if (IsSua == 0)
                {
                    _o.NguoiTao = Common.User.MaNV;
                    _o.NgayTao = DateTime.Now;
                    _o.MaTN = MaTn;
                    _db.tbl_PhanCong_BanGiaoCas.InsertOnSubmit(_o);
                }
                else
                {
                    _o.NgaySua = DateTime.Now;
                    _o.NguoiSua = Common.User.MaNV;
                }

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
            }
            catch (Exception)
            {
                //throw;
            }
        }

        private void gvTaiSan_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            gvTaiSan.SetFocusedRowCellValue("ID",0);
            gvTaiSan.SetFocusedRowCellValue("NgayTao", DateTime.Now);
            gvTaiSan.SetFocusedRowCellValue("NguoiTao", Common.User.MaNV);
            gvTaiSan.SetFocusedRowCellValue("MaTN", MaTn);
            gvTaiSan.SetFocusedRowCellValue("SoLuongNhan", 0);
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
                if (item.EditValue == null)
                {
                    DialogBox.Alert("Vui lòng chọn Tài Sản");
                    return;
                }

                gvTaiSan.SetFocusedRowCellValue("IDTinhTrangTaiSanNhan", item.Properties.View.GetFocusedRowCellValue("IDTinhTrangTaiSan"));
                gvTaiSan.SetFocusedRowCellValue("IDThietBi", item.EditValue);
                gvTaiSan.UpdateCurrentRow();
            }
            catch (Exception)
            {
                //throw;
            }
        }

        private void gvTaiSan_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            var id = (int?)gvTaiSan.GetFocusedRowCellValue("ID");
            if (id == null | id == 0) return;
            if (e.Column.FieldName != "NguoiSua" & e.Column.FieldName != "NgaySua")
            {
                gvTaiSan.SetFocusedRowCellValue("NguoiSua", Common.User.MaNV);
                gvTaiSan.SetFocusedRowCellValue("NgaySua", DateTime.Now);
            }
        }

        private void glkCa_EditValueChanged(object sender, EventArgs e)
        {
            var item = sender as GridLookUpEdit;
            if (item == null) return;
            glkNhanVien.Properties.DataSource = (from _ in _db.tbl_PhanCong_NhanVienChiTiets
                where _.MaTN == (byte) MaTn & _.IDPhanLoaiCa == (int)item.EditValue &
                      _.Ngay.Value.Date == dateNgayNhan.DateTime.Date select new {_.MaNV,_.tnNhanVien.HoTenNV,_.tnNhanVien.MaSoNV}).ToList();
        }

        private void dateNgayNhan_EditValueChanged(object sender, EventArgs e)
        {
            if(glkCa.EditValue!=null)
            glkNhanVien.Properties.DataSource = (from _ in _db.tbl_PhanCong_NhanVienChiTiets
                where _.MaTN == (byte)MaTn & _.IDPhanLoaiCa == (int)glkCa.EditValue &
                      _.Ngay.Value.Date == dateNgayNhan.DateTime.Date
                select new { _.MaNV, _.tnNhanVien.HoTenNV, _.tnNhanVien.MaSoNV }).ToList();
        }

        private void itemThemCongCu_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (var frm = new DanhMuc.frmThietBi_Edit { MaTn = (byte)MaTn, IsSua = 0, Id = 0 })
            {
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                {
                    _db = new MasterDataContext();
                    repTaiSan.DataSource = _db.tbl_PhanCong_ThietBis
                        .Select(_ => new { _.ID, _.KyHieu, _.Name, _.SoLuong, _.tbl_TinhTrangTaiSan.TenTinhTrang, _.IDTinhTrangTaiSan }).ToList();
                }
            }
        }
    }
}