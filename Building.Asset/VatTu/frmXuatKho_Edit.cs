using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
using System.Windows.Forms;
using Library;
using System.Linq;
using System.Text.RegularExpressions;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using System.Data.Linq.SqlClient;

namespace Building.Asset.VatTu
{
    public partial class frmXuatKho_Edit: XtraForm
    {
        public byte MaTn { get; set; }
        public int? IsSua { get; set; } 
        public long? Id { get; set; } 
        public int? MaNv { get; set; }

        private MasterDataContext _db=new MasterDataContext();
        private tbl_VatTu_XuatKho _o;
        private Table<tbl_VatTu_SoKho> l;

        private System.Collections.Generic.List<Library.Class.HuongDan.ShowAuto.ControlItem> controls;

        public frmXuatKho_Edit()
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

            l = _db.tbl_VatTu_SoKhos;

            dateNgayXuat.DateTime = DateTime.Now;
            txtDienGiai.Text = "";
            txtMaPhieu.Text = GetSttMa(DateTime.Now);

            glkNguoiXuat.Properties.DataSource = _db.tnNhanViens;
            glkNguoiXuat.EditValue = Common.User.MaNV;
                
            glkKho.Properties.DataSource = _db.tbl_VatTu_Khos.Where(_=>_.MaTN==MaTn);
            glkLoaiXuat.Properties.DataSource = _db.tbl_VatTu_XuatKho_LoaiXuats;
            glkLoaiXuat.EditValue = glkLoaiXuat.Properties.GetKeyValue(0);

            if (IsSua == 0)
            {
                _o = new tbl_VatTu_XuatKho();
                if (Id != 0)
                {
                    var obj = _db.tbl_VatTu_XuatKhos.FirstOrDefault(_ => _.ID == Id);
                    if (obj != null)
                    {
                        txtDienGiai.Text = obj.DienGiai;
                    }
                }
            }
            else
            {
                _o = _db.tbl_VatTu_XuatKhos.FirstOrDefault(_ => _.ID == (int) Id);
                if (_o != null)
                {
                    txtMaPhieu.Text = _o.MaPhieu;
                    if (_o.NgayPhieu != null) dateNgayXuat.DateTime = (DateTime) _o.NgayPhieu;
                    glkNguoiXuat.EditValue = _o.NguoiNhap;
                    txtDienGiai.Text = _o.DienGiai;
                    glkKho.EditValue = _o.KhoID;
                }
                else
                {
                    _o = new tbl_VatTu_XuatKho();
                }
            }

            gcTaiSan.DataSource = _o.tbl_VatTu_XuatKho_ChiTiets;

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

        private string GetSttMa(DateTime ngayPhieu)
        {
            _db = new MasterDataContext();
            var o = _db.tbl_VatTu_XuatKhos.Where(_ => _.MaTN == MaTn & _.NgayPhieu.Value.Year == ngayPhieu.Year).Select(_ => _.MaPhieu).ToList();
            return Common.TaoMa(o, "PXK/");
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

                if (glkNguoiXuat.EditValue == null)
                {
                    DialogBox.Error("Vui lòng chọn nhân viên");
                    return;
                }

                if (glkKho.EditValue == null)
                {
                    DialogBox.Error("Vui lòng chọn nhà cung cấp");
                    return;
                }
                if (glkLoaiXuat.EditValue == null)
                {
                    DialogBox.Error("Vui lòng chọn loại nhập");
                    return;
                }

                #endregion

                _o.MaPhieu = txtMaPhieu.Text;
                _o.NgayPhieu = dateNgayXuat.DateTime;
                _o.DienGiai = txtDienGiai.Text;
                _o.NguoiNhan = (int) glkNguoiXuat.EditValue;
                _o.KhoID = (int) glkKho.EditValue;
                _o.ChungTuGoc = txtChungTuGoc.Text;
                //_o.MuaHangID = (long?) glkPhieuMuaHang.EditValue;
                _o.LoaiXuatID = (int?) glkLoaiXuat.EditValue;

                if (IsSua == 0)
                {
                    _o.NguoiNhap = Common.User.MaNV;
                    _o.NgayNhap = DateTime.Now;
                    _o.MaTN = MaTn;
                    _db.tbl_VatTu_XuatKhos.InsertOnSubmit(_o);
                }
                else
                {
                    _o.NgaySua = DateTime.Now;
                    _o.NguoiSua = Common.User.MaNV;
                }

                _db.SubmitChanges();

                // cập nhật sổ kho
                //var l1 = _db.tbl_VatTu_SoKhos.Where(_ => _.IDPhieu == _o.ID).ToList();
                DataTable dt = _o.tbl_VatTu_XuatKho_ChiTiets.ToList().ConvertToDataTable();
                l = _db.tbl_VatTu_SoKhos;

                if (dt != null)
                {
                    List<tbl_VatTu_SoKho> lCt = dt.AsEnumerable().Select(m => new tbl_VatTu_SoKho
                    {
                        NgayPhieu = _o.NgayPhieu,
                        SoPhieu = _o.MaPhieu,
                        IDPhieu = _o.ID,
                        IDPhieuChiTiet = m.Field<long>("ID"),
                        SoLuong = -m.Field<decimal>("SoLuong"),
                        DonGia = -m.Field<decimal>("DonGia"),
                        ThanhTien = -m.Field<decimal>("ThanhTien"),
                        MaLoaiNhapXuat = 2,
                        MaTN = _o.MaTN,
                        NgayNhap = _o.NgayNhap,
                        NguoiNhap = _o.NguoiNhap,
                        KhoID = _o.KhoID,
                        VatTuID = m.Field<long>("VatTuID"),
                    }).ToList();

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
                    gvTaiSan.SetFocusedRowCellValue("VatTuID", item.EditValue.ToString());
                    gvTaiSan.SetFocusedRowCellValue("SoLuongTonKho", item.Properties.View.GetFocusedRowCellValue("SoLuongTonKho"));
                    gvTaiSan.SetFocusedRowCellValue("SoLuong", 0);
                    gvTaiSan.SetFocusedRowCellValue("DonGia", item.Properties.View.GetFocusedRowCellValue("DonGia"));
                    gvTaiSan.SetFocusedRowCellValue("ThanhTien", 0);
                    gvTaiSan.SetFocusedRowCellValue("TenDVT", item.Properties.View.GetFocusedRowCellValue("TenDVT"));
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

        private void glkKho_EditValueChanged(object sender, EventArgs e)
        {
            var item = sender as GridLookUpEdit;
            if (item == null) return;
            repTaiSan.DataSource = (from p in l
                where p.MaTN == MaTn & p.KhoID == (int) item.EditValue
                group new {p} by new {p.VatTuID, p.tbl_VatTu.tbl_VatTu_DVT.TenDVT, p.tbl_VatTu.GiaXuat,p.tbl_VatTu.KyHieu,p.tbl_VatTu.Ten}
                into g
                select new
                {
                    g.Key.TenDVT,
                    ID = g.Key.VatTuID,
                    DonGia = g.Key.GiaXuat,
                    g.Key.Ten,
                    g.Key.KyHieu,
                    SoLuongTonKho = g.Sum(_ => _.p.SoLuong).GetValueOrDefault()
                }).ToList();
        }

        private void spinSoLuongTonKho_EditValueChanged(object sender, EventArgs e)
        {
            var item = sender as SpinEdit;
            if (item == null) return;
            var soLuongTonKho = (decimal?)gvTaiSan.GetFocusedRowCellValue("SoLuongTonKho");
            if ((decimal)item.EditValue > soLuongTonKho)
                item.Value = soLuongTonKho.Value;
        }
    }
}