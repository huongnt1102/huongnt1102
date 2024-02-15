using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Library;
using System.Linq;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;

namespace Building.Asset.DanhMuc
{
    public partial class frmThietBi_Edit: XtraForm
    {
        public byte MaTn { get; set; }
        public int? IsSua { get; set; } // 0: thêm, 1: sửa
        public int? Id { get; set; } // id phieu, dung cho sửa, thêm thì =0

        private MasterDataContext _db;
        private tbl_PhanCong_ThietBi _o;
        private bool _khoa=true;

        System.Collections.Generic.List<Library.Class.HuongDan.ShowAuto.ControlItem> controls;

        public frmThietBi_Edit()
        {
            InitializeComponent();
            TranslateLanguage.TranslateControl(this);
        }
        private void frmEdit_Load(object sender, EventArgs e)
        {
            controls = Library.Class.HuongDan.ShowAuto.GetControlItems(this.Controls);
            Library.Class.HuongDan.ShowAuto.ClearText(this.Controls);

            TranslateLanguage.TranslateControl(this, barManager1);
            LoadData();

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

        private void LoadData()
        {
            _db = new MasterDataContext();

            txtMaThietBi.Text = "";
            txtTenThietBi.Text = "";
            spinSoLuong.Value = 0;

            glkTinhTrang.Properties.DataSource = _db.tbl_TinhTrangTaiSans;

            if (IsSua == 0)
            {
                _o = new tbl_PhanCong_ThietBi();
                // thêm phiếu
                if (Id != 0)
                {
                    var obj = _db.tbl_PhanCong_ThietBis.FirstOrDefault(_ => _.ID == Id);
                    if (obj != null)
                    {
                        txtTenThietBi.Text = obj.Name;
                        if (obj.SoLuong != null) spinSoLuong.Value = (decimal) obj.SoLuong;
                        glkTinhTrang.EditValue = obj.IDTinhTrangTaiSan;
                    }
                }
            }
            else
            {
                _o = _db.tbl_PhanCong_ThietBis.FirstOrDefault(_ => _.ID == (int) Id);
                if (_o != null)
                {
                    txtTenThietBi.Text = _o.Name;
                    if (_o.SoLuong != null) spinSoLuong.Value = (decimal)_o.SoLuong;
                    glkTinhTrang.EditValue = _o.IDTinhTrangTaiSan;
                    txtMaThietBi.Text = _o.KyHieu;
                    _khoa = false;
                }
                else
                {
                    _o = new tbl_PhanCong_ThietBi();
                }
            }
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

                if (txtMaThietBi.Text == "")
                {
                    DialogBox.Error("Vui lòng nhập Mã công cụ thiết bị");
                    return;
                }

                if (txtTenThietBi.Text == null)
                {
                    DialogBox.Error("Vui lòng nhập tên công cụ thiết bị");
                    return;
                }

                if (glkTinhTrang.EditValue == null)
                {
                    DialogBox.Error("Vui lòng chọn tình trạng");
                    return;
                }

                if (_khoa)
                {
                    var kt = _db.tbl_PhanCong_ThietBis.FirstOrDefault(_ =>
                        _.KyHieu.ToLower() == txtMaThietBi.Text.ToLower() &
                        _.IDTinhTrangTaiSan == (int) glkTinhTrang.EditValue);
                    if (kt != null)
                    {
                        DialogBox.Error("Công cụ này đã tồn tại tình trạng bạn đã chọn\nVui lòng chọn tình trạng khác\nHoặc chọn sửa phiếu công cụ");
                        return;
                    }
                }

                #endregion

                _o.KyHieu = txtMaThietBi.Text;
                _o.Name = txtTenThietBi.Text;
                _o.SoLuong = spinSoLuong.Value;
                _o.IDTinhTrangTaiSan = (int) glkTinhTrang.EditValue;
                _o.MaTN = MaTn;
                if (IsSua == 0)
                {
                    _o.NguoiTao = Common.User.MaNV;
                    _o.NgayTao = DateTime.Now;

                    _db.tbl_PhanCong_ThietBis.InsertOnSubmit(_o);
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
    }
}