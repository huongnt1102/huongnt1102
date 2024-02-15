using System;
using System.Windows.Forms;
using Library;
using System.Linq;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;

namespace Building.Asset.DanhMuc
{
    public partial class frmVatTu_Edit: XtraForm
    {
        public byte MaTn { get; set; }
        public int? IsSua { get; set; } // 0: thêm, 1: sửa
        public long? Id { get; set; } // id phieu, dung cho sửa, thêm thì =0

        private MasterDataContext _db;
        private tbl_VatTu _o;

        private System.Collections.Generic.List<Library.Class.HuongDan.ShowAuto.ControlItem> controls;

        public frmVatTu_Edit()
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

            txtMaVatTu.Text = "";
            txtTenVatTu.Text = "";
            txtViTri.Text = "";
            txtThongSoKyThuat.Text = "";
            txtMoTa.Text = "";
            chkNgungSuDung.Checked = false;
            spinNguyenGia.Value = 0;
            spinGiaNhap.Value = 0;
            spinGiaXuat.Value = 0;

            glkKhoiNha.Properties.DataSource = _db.mbKhoiNhas.Where(_ => _.MaTN == (byte?) MaTn);
            glkKhoiNha.EditValue = glkKhoiNha.Properties.GetKeyValue(0);

            glkDonViTinh.Properties.DataSource = _db.tbl_VatTu_DVTs.Where(_ => _.NgungSuDung == false);
            glkDonViTinh.EditValue = glkDonViTinh.Properties.GetKeyValue(0);

            if (IsSua == 0)
            {
                _o = new tbl_VatTu();
                // thêm phiếu
                if (Id != 0)
                {
                    var obj = _db.tbl_VatTus.FirstOrDefault(_ => _.ID == Id);
                    if (obj != null)
                    {
                        txtTenVatTu.Text = obj.Ten;
                        txtViTri.Text = obj.ViTri;
                        txtThongSoKyThuat.Text = obj.ThongSoKyThuat;
                        txtMoTa.Text = obj.MoTa;
                        if (obj.NguyenGia != null) spinNguyenGia.Value = (decimal) obj.NguyenGia;
                        if (obj.GiaNhap != null) spinGiaNhap.Value = (decimal) obj.GiaNhap;
                        if (obj.GiaXuat != null) spinGiaXuat.Value = (decimal) obj.GiaXuat;
                        glkKhoiNha.EditValue = obj.BlockID;
                        glkDonViTinh.EditValue = obj.DVTID;
                    }
                }
            }
            else
            {
                _o = _db.tbl_VatTus.FirstOrDefault(_ => _.ID == (int) Id);
                if (_o != null)
                {
                    txtTenVatTu.Text = _o.Ten;
                    txtViTri.Text = _o.ViTri;
                    txtThongSoKyThuat.Text = _o.ThongSoKyThuat;
                    txtMoTa.Text = _o.MoTa;
                    if (_o.NguyenGia != null) spinNguyenGia.Value = (decimal)_o.NguyenGia;
                    if (_o.GiaNhap != null) spinGiaNhap.Value = (decimal)_o.GiaNhap;
                    if (_o.GiaXuat != null) spinGiaXuat.Value = (decimal)_o.GiaXuat;
                    glkKhoiNha.EditValue = _o.BlockID;
                    glkDonViTinh.EditValue = _o.DVTID;
                    txtMaVatTu.Text = _o.KyHieu;
                }
                else
                {
                    _o = new tbl_VatTu();
                }
            }

            itemHuongDan.ItemClick += ItemHuongDan_ItemClick;
            itemLamLai.ItemClick += ItemClearText_ItemClick;
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

                if (txtMaVatTu.Text == "")
                {
                    DialogBox.Error("Vui lòng nhập Mã vật tư");
                    return;
                }

                if (txtTenVatTu.Text == null)
                {
                    DialogBox.Error("Vui lòng nhập tên vật tư");
                    return;
                }

                if (glkDonViTinh.EditValue == null)
                {
                    DialogBox.Error("Vui lòng chọn đơn vị tính");
                    return;
                }

                if (glkKhoiNha.EditValue == null)
                {
                    DialogBox.Error("Vui lòng chọn khối nhà");
                    return;
                }

                var kt = _db.tbl_VatTus.FirstOrDefault(_ =>
                    _.KyHieu.ToLower() == txtMaVatTu.Text.ToLower() & _.MaTN == MaTn & _.ID != _o.ID);
                if (kt != null)
                {
                    DialogBox.Error("Mã Vật tư này đã tồn tại.\nVui lòng nhập mã vật tư khác");
                    return;
                }

                #endregion

                _o.KyHieu = txtMaVatTu.Text;
                _o.Ten = txtTenVatTu.Text;
                _o.ThongSoKyThuat = txtThongSoKyThuat.Text;
                _o.MoTa = txtMoTa.Text;
                _o.ViTri = txtViTri.Text;
                _o.NguyenGia = spinNguyenGia.Value;
                _o.GiaXuat = spinGiaXuat.Value;
                _o.GiaNhap = spinGiaNhap.Value;
                _o.BlockID = (int) glkKhoiNha.EditValue;
                _o.DVTID = (int) glkDonViTinh.EditValue;
                _o.NgungSuDung = chkNgungSuDung.Checked;

                if (IsSua == 0)
                {
                    _o.MaTN = MaTn;
                    _o.NguoiNhap = Common.User.MaNV;
                    _o.NgayNhap = DateTime.Now;

                    _db.tbl_VatTus.InsertOnSubmit(_o);
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