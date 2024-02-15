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
    public partial class frmDeXuat_Duyet: XtraForm
    {
        public byte MaTn { get; set; }
        public int? IsSua { get; set; } // 0: thêm, 1: sửa
        public long? Id { get; set; } // id phieu, dung cho sửa, thêm thì =0
        public int? MaNv { get; set; }

        private MasterDataContext _db;
        private tbl_VatTu_DeXuat _o;
        private System.Collections.Generic.List<Library.Class.HuongDan.ShowAuto.ControlItem> controls;

        public frmDeXuat_Duyet()
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
            txtMaPhieu.Text = GetMaDeXuat();

            glkNguoiNhan.Properties.DataSource = _db.tnNhanViens;
            glkNguoiNhan.EditValue = Common.User.MaNV;

            repTaiSan.DataSource = (from _ in _db.tbl_VatTus
                where _.MaTN == MaTn & _.NgungSuDung == false
                select new
                {
                    _.ID, _.KyHieu, _.Ten, _.tbl_VatTu_DVT.TenDVT
                }).ToList();

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

        private string GetMaDeXuat()
        {
            var id = "";
            _db.tbl_VatTu_DeXuat_getNewID(ref id);
            return _db.DinhDang(37, int.Parse(id));
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
                _o.TrangThaiID = 2;
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
                    _o.NgayDuyet = DateTime.Now;
                    _o.NguoiDuyet = Common.User.MaNV;
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

                gvTaiSan.SetFocusedRowCellValue("TenDVT", item.Properties.View.GetFocusedRowCellValue("TenDVT")); 
                gvTaiSan.SetFocusedRowCellValue("VatTuID", item.EditValue.ToString());
                gvTaiSan.UpdateCurrentRow();
            }
            catch (Exception)
            {
                //throw;
            }
        }
    }
}