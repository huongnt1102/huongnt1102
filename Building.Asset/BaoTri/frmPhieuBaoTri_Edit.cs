using System;
using System.Windows.Forms;
using Library;
using System.Linq;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;

namespace Building.Asset.BaoTri
{
    public partial class frmPhieuBaoTri_Edit : XtraForm
    {
        public byte? MaTn { get; set; }
        public int? IsSua { get; set; } // 0: thêm, 1: sửa
        public int? Id { get; set; } // id phieu, dung cho sửa, thêm thì =0

        private MasterDataContext _db;
        private tbl_PhieuVanHanh _o;

        System.Collections.Generic.List<Library.Class.HuongDan.ShowAuto.ControlItem> controls;

        public frmPhieuBaoTri_Edit()
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

            glkToaNha.Properties.DataSource = _db.tnToaNhas;
            glkToaNha.EditValue = MaTn ?? Common.User.MaTN;

            glkKeHoachVanHanh.Properties.DataSource = _db.tbl_KeHoachVanHanhs.Where(_ => _.MaTN == MaTn);
            glkNhomTaiSan.Properties.DataSource = _db.tbl_NhomTaiSans.Where(_=>_.MaTN==MaTn);
            glkNguoiThucHien.Properties.DataSource = _db.tnNhanViens.Where(_=>_.MaTN==MaTn);
            glkTinhTrangTaiSan.DataSource = _db.tbl_PhieuVanHanh_ChiTiet_TinhTrangTaiSans.ToList();
            dateNgayPhieu.DateTime = DateTime.Now;
            glkNguoiThucHien.EditValue = Common.User.MaNV;
           
            if (IsSua == null || IsSua == 0)
            {
                _o = new tbl_PhieuVanHanh();
            }
            else
            {
                _o = _db.tbl_PhieuVanHanhs.FirstOrDefault(_ => _.ID == Id);
                if (_o != null)
                {
                    if (_o.NgayPhieu != null) dateNgayPhieu.DateTime = (DateTime) _o.NgayPhieu;
                    txtSoPhieu.Text = _o.SoPhieu;
                    glkKeHoachVanHanh.EditValue = _o.KeHoachVanHanhID;
                    glkNguoiThucHien.EditValue = _o.NguoiThucHien;
                    glkNhomTaiSan.EditValue = _o.HeThongTaiSanID;
                    spinSoNgayCoTheTre.EditValue = _o.SoNgayCoTheTre;
                }
            }
            gcChiTiet.DataSource = _o.tbl_PhieuVanHanh_ChiTiets;
            gvChiTiet.BestFitColumns();
            gcChiTietTaiSan.DataSource = _o.tbl_PhieuVanHanh_ChiTiet_TaiSans;

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
                gvChiTiet.PostEditor();

                #region kiểm tra

                if (glkKeHoachVanHanh.EditValue == null)
                {
                    DialogBox.Error("Vui lòng chọn kế hoạch vận hành");
                    return;
                }

                if (glkNhomTaiSan.EditValue == null)
                {
                    DialogBox.Error("Vui lòng chọn hệ thống");
                    return;
                }
               
                //if (glkNguoiThucHien.EditValue == null)
                //{
                //    DialogBox.Error("Vui lòng chọn người thực hiện");
                //    return;
                //}

                #endregion

                _o.NgayPhieu = dateNgayPhieu.DateTime;

                _o.KeHoachVanHanhID = (int)glkKeHoachVanHanh.EditValue;
               
                _o.SoPhieu = txtSoPhieu.Text;
                _o.NguoiThucHien = (int?)glkNguoiThucHien.EditValue;
                _o.IsPhieuBaoTri = true;
                _o.SoNgayCoTheTre = spinSoNgayCoTheTre.Value;
                _o.NgayHetHanCuoiCung = _o.DenNgay.GetValueOrDefault().AddDays((double)spinSoNgayCoTheTre.Value);

                if (IsSua == 0 & Id == 0)
                {
                    _o.NhomTaiSanID = (int?)glkNhomTaiSan.EditValue;
                    _o.MaTN = MaTn;
                    _db.tbl_PhieuVanHanhs.InsertOnSubmit(_o);
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
                if (!string.IsNullOrEmpty(gvChiTiet.GetFocusedDataSourceRowIndex().ToString()))
                {
                    gvChiTiet.DeleteSelectedRows();
                }
            }
            catch (Exception)
            {
                //throw;
            }
        }

        private void gvChiTiet_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            gvChiTiet.SetFocusedRowCellValue("ID",0);
        }

        private void gvChiTiet_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (!string.IsNullOrEmpty(gvChiTiet.GetSelectedRows().ToString()))
                {
                    gvChiTiet.DeleteSelectedRows();
                }
            }
        }
        private void glkNhomTaiSan_EditValueChanged(object sender, EventArgs e)
        {
            glkTaiSanChiTiet.DataSource = (from ct in _db.tbl_ChiTietTaiSans
                                           join tts in _db.tbl_TenTaiSans on ct.TenTaiSanID equals tts.ID
                                           join lts in _db.tbl_LoaiTaiSans on tts.LoaiTaiSanID equals lts.ID
                                           where lts.NhomTaiSanID == (int?)glkNhomTaiSan.EditValue
                                           select new
                                           {
                                               ct.ID,
                                               ct.MaChiTietTaiSan,
                                               ct.TenChiTietTaiSan,
                                               tts.TenTaiSan,
                                           }).ToList();
        }

        private void glkKeHoachVanHanh_EditValueChanged(object sender, EventArgs e)
        {
            var rowKeHoachVanHanh = glkKeHoachVanHanh.Properties.GetRowByKeyValue((int)((GridLookUpEdit) sender).EditValue);
            var type = rowKeHoachVanHanh.GetType();
            var nhomTaiSanId = type.GetProperty("NhomTaiSanID").GetValue(rowKeHoachVanHanh, null);
            if (nhomTaiSanId != null) glkNhomTaiSan.EditValue = (int?)nhomTaiSanId;
        }
    }
}