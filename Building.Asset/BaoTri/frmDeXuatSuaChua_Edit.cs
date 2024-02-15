using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Library;
using System.Linq;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;

namespace Building.Asset.BaoTri
{
    public partial class frmDeXuatSuaChua_Edit : XtraForm
    {
        public byte? MaTn { get; set; }
        public int? IsSua { get; set; } // 0: thêm, 1: sửa
        public int? Id { get; set; } // id phieu, dung cho sửa, thêm thì =0
        public int? IdPhieuVanHanh { get; set; }

        System.Collections.Generic.List<Library.Class.HuongDan.ShowAuto.ControlItem> controls;

        private MasterDataContext _db;
        private tbl_DeXuatSuaChua _o;

        public frmDeXuatSuaChua_Edit()
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

            glkPhieuVanHanh.Properties.DataSource = _db.tbl_PhieuVanHanhs.Where(_ => _.MaTN == MaTn);
            if (IdPhieuVanHanh != null)
            {
                glkPhieuVanHanh.EditValue = IdPhieuVanHanh;
            }

            dateNgay.DateTime = DateTime.Now;
            glkNguoiYeuCau.Properties.DataSource = _db.tnNhanViens;
            glkNguoiYeuCau.EditValue = Common.User.MaNV;

            if (IsSua == null || IsSua == 0)
            {
                _o = new tbl_DeXuatSuaChua();
            }
            else
            {
                _o = _db.tbl_DeXuatSuaChuas.FirstOrDefault(_ => _.ID == Id);
                if (_o == null)
                {
                    _o = new tbl_DeXuatSuaChua();
                }
                else
                {
                    txtSoPhieu.Text = _o.SoPhieu;
                    glkPhieuVanHanh.EditValue = _o.PhieuVanHanhID;
                    txtLyDo.Text = _o.LyDo;
                    glkNguoiYeuCau.EditValue = _o.NguoiYeuCau;
                    if (_o.Ngay != null) dateNgay.DateTime = (DateTime) _o.Ngay;
                }
            }

            gcTaiSan.DataSource = _o.tbl_DeXuatSuaChua_ChiTiets;
            gvTaiSan.BestFitColumns();
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

                _o.SoPhieu = txtSoPhieu.Text;
                _o.Ngay = dateNgay.DateTime;
                _o.NguoiYeuCau = (int) glkNguoiYeuCau.EditValue;
                _o.PhieuVanHanhID = (int) glkPhieuVanHanh.EditValue;
                _o.LyDo = txtLyDo.Text;

                if (IsSua == 0)
                {
                    _o.MaTN = MaTn;
                    _o.NgayLap = DateTime.Now;
                    _o.NguoiLap = Common.User.MaNV;
                    _o.BuocDaDuyet = 0;
                    _o.TongBuocDuyet = 0;
                    _o.TrangThaiID = 0;
                    _db.tbl_DeXuatSuaChuas.InsertOnSubmit(_o);
                }  
                else
                {
                    _o.NguoiSua = Common.User.MaNV;
                    _o.NgaySua = DateTime.Now;
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
                if (IsSua == null || IsSua == 0)
                {
                    if (!string.IsNullOrEmpty(gvTaiSan.GetFocusedDataSourceRowIndex().ToString()))
                    {
                        gvTaiSan.DeleteSelectedRows();
                    }
                }
            }
            catch (Exception ex)
            {
                //throw;
            }
        }

        private void gvTaiSan_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            gvTaiSan.SetFocusedRowCellValue("ID",0);
        }

        private void gvTaiSan_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                gvTaiSan.DeleteSelectedRows();
            }
        }
        bool isDuplication(string fieldName, int index, object value, DevExpress.XtraGrid.Views.Grid.GridView gridView)
        {
            var newValue = value.ToString();
            for (int i = 0; i < gridView.RowCount - 1; i++)
            {
                if (i == index) continue;
                var oldValue = gridView.GetRowCellValue(i, fieldName).ToString();
                if (oldValue == newValue) return true;
            }
            return false;
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
                // bắt trùng
                if (isDuplication("IdTaiSanChiTiet", gvTaiSan.FocusedRowHandle, item.EditValue.ToString(), gvTaiSan))
                {
                    if (gvTaiSan.FocusedRowHandle < 0)
                    {
                        gvTaiSan.SetFocusedRowCellValue("IdTaiSanChiTiet", "");
                        gvTaiSan.DeleteSelectedRows();
                    }
                    else
                    {
                        gvTaiSan.DeleteRow(gvTaiSan.FocusedRowHandle);
                    }
                    DialogBox.Error("Tài sản này đã được chọn");
                    return;
                }
                gvTaiSan.SetFocusedRowCellValue("IdTaiSanChiTiet", item.EditValue.ToString());
                gvTaiSan.SetFocusedRowCellValue("TenTaiSanChiTiet", item.Properties.View.GetFocusedRowCellValue("TenChiTietTaiSan"));
                gvTaiSan.SetFocusedRowCellValue("LoaiHeThong", 4);
                gvTaiSan.SetFocusedRowCellValue("TenTaiSanID", item.Properties.View.GetFocusedRowCellValue("TenTaiSanID"));
                gvTaiSan.SetFocusedRowCellValue("LoaiTaiSanID", item.Properties.View.GetFocusedRowCellValue("LoaiTaiSanID"));
                gvTaiSan.SetFocusedRowCellValue("HeThongTaiSanID", item.Properties.View.GetFocusedRowCellValue("HeThongTaiSanID"));
                gvTaiSan.UpdateCurrentRow();
            }
            catch (Exception)
            {

                //throw;
            }
        }

        private void glkPhieuVanHanh_EditValueChanged(object sender, EventArgs e)
        {
            var item = sender as GridLookUpEdit;
            if (item == null) return;
            var pvh = _db.tbl_PhieuVanHanhs.FirstOrDefault(_ => _.ID.ToString() == item.EditValue.ToString());
            if (pvh == null) return;

            repTaiSan.DataSource = _db.tbl_PhieuVanHanh_ChiTiet_TaiSans
                .Where(p => p.PhieuVanHanhID == (int) item.EditValue)
                .Select(p => new
                {
                    p.tbl_ChiTietTaiSan.MaChiTietTaiSan,
                    p.tbl_ChiTietTaiSan.TenChiTietTaiSan,
                    p.tbl_ChiTietTaiSan.ID,
                    TenTaiSanID = p.tbl_ChiTietTaiSan.tbl_TenTaiSan.ID,
                    LoaiTaiSanID = p.tbl_ChiTietTaiSan.tbl_TenTaiSan.tbl_LoaiTaiSan.ID,
                    HeThongTaiSanID = p.tbl_ChiTietTaiSan.tbl_TenTaiSan.tbl_LoaiTaiSan.tbl_NhomTaiSan.ID,
                    HeThong = p.tbl_ChiTietTaiSan.tbl_TenTaiSan.tbl_LoaiTaiSan.tbl_NhomTaiSan.TenNhomTaiSan,
                    TenTaiSan=p.tbl_ChiTietTaiSan.tbl_TenTaiSan.TenTaiSan,
                    LoaiTaiSan=p.tbl_ChiTietTaiSan.tbl_TenTaiSan.tbl_LoaiTaiSan.TenLoaiTaiSan
                }).ToList();

            switch (pvh.LoaiHeThong)
            {
                case 1:
                    // hệ thống
                    repTaiSan.DataSource = _db.tbl_ChiTietTaiSans
                        .Where(_ => _.tbl_TenTaiSan.tbl_LoaiTaiSan.NhomTaiSanID == pvh.NhomTaiSanID).Select(_ => new
                        {
                            _.MaChiTietTaiSan, _.TenChiTietTaiSan, _.ID, TenTaiSanID = _.tbl_TenTaiSan.ID,
                            LoaiTaiSanID = _.tbl_TenTaiSan.tbl_LoaiTaiSan.ID,
                            HeThongTaiSanID = _.tbl_TenTaiSan.tbl_LoaiTaiSan.tbl_NhomTaiSan.ID,
                            HeThong = _.tbl_TenTaiSan.tbl_LoaiTaiSan.tbl_NhomTaiSan.TenNhomTaiSan,
                            _.tbl_TenTaiSan.TenTaiSan,
                            LoaiTaiSan = _.tbl_TenTaiSan.tbl_LoaiTaiSan.TenLoaiTaiSan
                        }).ToList();
                    break;
                case 2:
                    // loại tài sản
                    repTaiSan.DataSource = _db.tbl_ChiTietTaiSans
                        .Where(_ => _.tbl_TenTaiSan.tbl_LoaiTaiSan.ID == pvh.LoaiTaiSanID).Select(_ => new
                        {
                            _.MaChiTietTaiSan,
                            _.TenChiTietTaiSan,
                            _.ID,
                            TenTaiSanID = _.tbl_TenTaiSan.ID,
                            LoaiTaiSanID = _.tbl_TenTaiSan.tbl_LoaiTaiSan.ID,
                            HeThongTaiSanID = _.tbl_TenTaiSan.tbl_LoaiTaiSan.tbl_NhomTaiSan.ID,
                            HeThong = _.tbl_TenTaiSan.tbl_LoaiTaiSan.tbl_NhomTaiSan.TenNhomTaiSan,
                            _.tbl_TenTaiSan.TenTaiSan,
                            LoaiTaiSan = _.tbl_TenTaiSan.tbl_LoaiTaiSan.TenLoaiTaiSan
                        }).ToList();
                    break;
                case 3:
                    // tên tài sản
                    repTaiSan.DataSource = _db.tbl_ChiTietTaiSans
                        .Where(_ => _.tbl_TenTaiSan.ID == pvh.TenTaiSanID).Select(_ => new
                        {
                            _.MaChiTietTaiSan,
                            _.TenChiTietTaiSan,
                            _.ID,
                            TenTaiSanID = _.tbl_TenTaiSan.ID,
                            LoaiTaiSanID = _.tbl_TenTaiSan.tbl_LoaiTaiSan.ID,
                            HeThongTaiSanID = _.tbl_TenTaiSan.tbl_LoaiTaiSan.tbl_NhomTaiSan.ID,
                            HeThong = _.tbl_TenTaiSan.tbl_LoaiTaiSan.tbl_NhomTaiSan.TenNhomTaiSan,
                            _.tbl_TenTaiSan.TenTaiSan,
                            LoaiTaiSan = _.tbl_TenTaiSan.tbl_LoaiTaiSan.TenLoaiTaiSan
                        }).ToList();
                    break;
            }
            
        }

        private void itemHuongDan_ItemClick(object sender, ItemClickEventArgs e)
        {
            Library.Class.HuongDan.ShowAuto.ActiveDemo(true, controls);
        }

        private void itemClearText_ItemClick(object sender, ItemClickEventArgs e)
        {
            Library.Class.HuongDan.ShowAuto.ClearText(this.Controls);
        }
    }
}