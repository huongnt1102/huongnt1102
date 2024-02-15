using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Library;
using System.Linq;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;

namespace Building.Asset.VatTu
{
    public partial class frmMuaHang_ThanhToan: XtraForm
    {
        public byte MaTn { get; set; }
        public int? IsSua { get; set; } // 0: thêm, 1: sửa
        public long? Id { get; set; }
        public long? IdMuaHang { get; set; }
        public int? MaNv { get; set; }

        private MasterDataContext _db;
        private tbl_VatTu_MuaHang_ThanhToan _o;
        private tbl_VatTu_MuaHang _mh;
        private System.Collections.Generic.List<Library.Class.HuongDan.ShowAuto.ControlItem> controls;

        public frmMuaHang_ThanhToan()
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

            dateNgayThanhToan.DateTime = DateTime.Now;
            txtLyDo.Text = "";
            spinSoTien.Value = 0;

            glkNguoiThanhToan.Properties.DataSource = _db.tnNhanViens;
            glkNguoiThanhToan.EditValue = Common.User.MaNV;

            glkPhieuMuaHang.Properties.DataSource =
                _db.tbl_VatTu_MuaHangs.Where(_ => _.MaTN == MaTn & _.TrangThaiTraTienID != 3);

            if (IdMuaHang != null)
            {
                glkPhieuMuaHang.EditValue = IdMuaHang;
            }

            if (IsSua == 0)
            {
                _o = new tbl_VatTu_MuaHang_ThanhToan();
                // thêm phiếu
                if (Id != 0)
                {
                    var obj = _db.tbl_VatTu_MuaHang_ThanhToans.FirstOrDefault(_ => _.ID == Id);
                    if (obj != null)
                    {
                        txtLyDo.Text = obj.GhiChu;
                    }
                }
            }
            else
            {
                _o = _db.tbl_VatTu_MuaHang_ThanhToans.FirstOrDefault(_ => _.ID == (int) Id);
                if (_o != null)
                {
                    glkPhieuMuaHang.EditValue = _o.MuaHangID;

                    if (_o.NgayThanhToan != null) dateNgayThanhToan.DateTime = (DateTime) _o.NgayThanhToan;
                    glkNguoiThanhToan.EditValue = _o.NguoiThanhToan;
                    txtLyDo.Text = _o.GhiChu;
                    spinSoTien.Value = _o.SoTien.GetValueOrDefault();

                    var daTra = _mh.TongTienDaTra.GetValueOrDefault() - _o.SoTien.GetValueOrDefault();
                    spinDaTra.Value = daTra;
                    spinConLai.Value = _mh.TongTienPhieu.GetValueOrDefault() - daTra;

                    glkPhieuMuaHang.Properties.ReadOnly = true;
                }
                else
                {
                    _o = new tbl_VatTu_MuaHang_ThanhToan();
                }
            }

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
                #region kiểm tra

                if (glkNguoiThanhToan.EditValue == null)
                {
                    DialogBox.Error("Vui lòng chọn nhân viên");
                    return;
                }

                #endregion

                _o.NgayThanhToan = dateNgayThanhToan.DateTime;
                _o.GhiChu = txtLyDo.Text;
                _o.NguoiThanhToan = (int) glkNguoiThanhToan.EditValue;
                _o.MuaHangID = _mh.ID;
                _o.SoTien = spinSoTien.Value;

                if (IsSua == 0)
                {
                    _o.MaTN = MaTn;
                    _db.tbl_VatTu_MuaHang_ThanhToans.InsertOnSubmit(_o);
                }

                _mh.TongTienDaTra = spinDaTra.Value + spinSoTien.Value;
                _mh.TongTienConLai = spinTongTien.Value - _mh.TongTienDaTra;
                _mh.TrangThaiTraTienID = 2;
                if (_mh.TongTienConLai == 0) _mh.TrangThaiTraTienID = 3;

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

        private void glkPhieuMuaHang_EditValueChanged(object sender, EventArgs e)
        {
            var item = sender as GridLookUpEdit;
            if (item == null) return;
            _mh = _db.tbl_VatTu_MuaHangs.FirstOrDefault(_ => _.ID == (long) item.EditValue);
            if (_mh != null)
            {
                txtNhaCungCap.Text = _mh.tbl_NhaCungCapTaiSan.TenNhaCungCap;
                spinTongTien.Value = _mh.TongTienPhieu.GetValueOrDefault();
                spinDaTra.Value = _mh.TongTienDaTra.GetValueOrDefault();
                spinConLai.Value = _mh.TongTienConLai.GetValueOrDefault();
                //spinSoTien.Value = o.TongTienConLai.GetValueOrDefault();
            }
        }

        private void spinSoTien_EditValueChanged(object sender, EventArgs e)
        {
            var item = sender as SpinEdit;
            if (item == null) return;
            var conLai = (decimal?)spinConLai.Value;
            if ((decimal)item.EditValue > conLai)
                item.Value = conLai.Value;
        }
    }
}