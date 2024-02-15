using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Library;
using System.Linq;

namespace Building.Asset.DanhMuc
{
    public partial class frmTaiSanEdit : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        tbl_ChiTietTaiSan CT_TS;
        tbl_ChiTietTaiSan_KhoHang CT_KH;
        public byte? MaTn;
        public long Id;
        public int IsSua; // 0: Thêm, 1: Sửa

        System.Collections.Generic.List<Library.Class.HuongDan.ShowAuto.ControlItem> controls;

        public frmTaiSanEdit()
        {
            InitializeComponent();
        }

        private void frmTaiSanEdit_Load(object sender, EventArgs e)
        {
            controls = Library.Class.HuongDan.ShowAuto.GetControlItems(this.Controls);
            Library.Class.HuongDan.ShowAuto.ClearText(this.Controls);

            db = new MasterDataContext();
            CT_TS = new tbl_ChiTietTaiSan();
            CT_KH = new tbl_ChiTietTaiSan_KhoHang();
            gridLKHeThongTS.Properties.DataSource = db.tbl_NhomTaiSans.Where(_=>_.MaTN==MaTn & _.IsSuDung==true);
            gridLKNCC.Properties.DataSource = db.tbl_NhaCungCapTaiSans;
            gridLKTinhTrangTS.Properties.DataSource = db.tbl_TinhTrangTaiSans;
            gridLKKhoiNha.Properties.DataSource = db.mbKhoiNhas;
            if (Id != null & IsSua == 1)
            {
                var objCT_TS = db.tbl_ChiTietTaiSans.FirstOrDefault(o => o.ID == Id);
                objCT_TS.MaTN = MaTn;
                gridLKHeThongTS.EditValue = objCT_TS.tbl_TenTaiSan.tbl_LoaiTaiSan.NhomTaiSanID;
                gridLKLoaiTaiSan.EditValue = objCT_TS.tbl_TenTaiSan.LoaiTaiSanID;
                gridLKTaiSan.EditValue = objCT_TS.TenTaiSanID;
                txtMaTaiSan.EditValue = objCT_TS.MaChiTietTaiSan;
                txtTenChiTietTS.EditValue = objCT_TS.TenChiTietTaiSan;
                gridLKTinhTrangTS.EditValue = objCT_TS.TinhTrangTaiSanID;             
                gridLKNCC.EditValue = objCT_TS.NhaCungCapID;
                txtThongSo.EditValue = objCT_TS.ThongSoKyThuat;
                txtMoTa.EditValue = objCT_TS.MoTa;
                gridLKKhoiNha.EditValue = objCT_TS.BlockID;
                txtNguyenGia.EditValue = objCT_TS.NguyenGia;
                dateNgayMua.EditValue = objCT_TS.NgayMua;
                dateNgaySuDung.EditValue = objCT_TS.NgayDuaVaoSuDung;
                txtvitri.EditValue = objCT_TS.ViTri;
                txTonKho.EditValue = objCT_TS.TonKho;
                txtSoLuong.EditValue = objCT_TS.SoLuong;
                if (objCT_TS.QuanTrong == 1)
                    checkeditQuanTrong.Checked = true;
                else
                    checkeditQuanTrong.Checked = false;

                if (objCT_TS.NgungSuDung == true)
                    checkNSD.Checked = true;
                else
                    checkNSD.Checked = false;
                dateNgayHetHanBH.EditValue = objCT_TS.NgayHetHanBaoHanh;
                txtThoigianBH.EditValue = objCT_TS.ThoiGianBaoHanh;

            }

            itemHuongDan.Click += ItemHuongDan_Click;
            itemClearText.Click += ItemClearText_Click;
            itemHuy.Click += ItemHuy_Click;
            itemLuu.Click += ItemLuu_Click;
        }

        private void ItemLuu_Click(object sender, EventArgs e)
        {
            try
            {
                #region Kiểm tra

                if (gridLKTinhTrangTS.EditValue == null)
                {
                    DialogBox.Error("Vui lòng chọn tình trạng tài sản");
                    gridLKTinhTrangTS.Focus();
                    return;
                }
                if (gridLKHeThongTS.EditValue == null)
                {
                    DialogBox.Error("Vui lòng chọn hệ thống");
                    gridLKHeThongTS.Focus();
                    return;
                }
                if (gridLKLoaiTaiSan.EditValue == null)
                {
                    DialogBox.Error("Vui lòng chọn loại tài sản");
                    gridLKLoaiTaiSan.Focus();
                    return;
                }
                if (gridLKTaiSan.EditValue == null)
                {
                    DialogBox.Error("Vui lòng chọn tài sản");
                    gridLKTaiSan.Focus();
                    return;
                }

                if (gridLKNCC.EditValue == null)
                {
                    DialogBox.Error("Vui lòng chọn nhà cung cấp");
                    gridLKNCC.Focus();
                    return;
                }

                if (txtMaTaiSan.EditValue == null)
                {
                    DialogBox.Error("Vui nhập mã tài sản");
                    txtMaTaiSan.Focus();
                    return;
                }
                #endregion

                if (IsSua == 0 & Id == 0)
                {
                    #region Thêm mới chi tiết tài sản
                    CT_TS.MaTN = MaTn;
                    CT_TS.MaChiTietTaiSan = txtMaTaiSan.EditValue as string;
                    CT_TS.TenChiTietTaiSan = txtTenChiTietTS.EditValue as string;
                    CT_TS.TinhTrangTaiSanID = (int?)gridLKTinhTrangTS.EditValue;
                    CT_TS.TenTaiSanID = (int?)gridLKTaiSan.EditValue;
                    CT_TS.NhaCungCapID = (int?)gridLKNCC.EditValue;
                    CT_TS.ThongSoKyThuat = txtThongSo.EditValue as string;
                    CT_TS.MoTa = txtMoTa.EditValue as string;
                    CT_TS.BlockID = (int?)gridLKKhoiNha.EditValue;
                    CT_TS.NguyenGia = (decimal?)txtNguyenGia.EditValue;
                    CT_TS.NgayMua = (DateTime?)dateNgayMua.EditValue;
                    CT_TS.NgayDuaVaoSuDung = (DateTime?)dateNgaySuDung.EditValue;
                    CT_TS.ViTri = txtvitri.EditValue as string;
                    CT_TS.TonKho = (double?)txTonKho.Value;
                    CT_TS.SoLuong = (double?)txtSoLuong.Value;
                    if (checkeditQuanTrong.Checked == true)
                        CT_TS.QuanTrong = 1;
                    else
                        CT_TS.QuanTrong = 0;

                    if (checkNSD.Checked == true)
                        CT_TS.NgungSuDung = true;
                    else
                        CT_TS.NgungSuDung = false;
                    CT_TS.NgayHetHanBaoHanh = (DateTime?)dateNgayHetHanBH.EditValue;
                    CT_TS.ThoiGianBaoHanh = (int?)txtThoigianBH.Value;
                    CT_TS.NgayNhap = DateTime.Now;
                    CT_TS.NguoiNhap = Common.User.MaNV;
                    db.tbl_ChiTietTaiSans.InsertOnSubmit(CT_TS);
                    #endregion
                }
                else
                {
                    #region Sửa công việc
                    var objCT_TS = db.tbl_ChiTietTaiSans.FirstOrDefault(o => o.ID == Id);
                    objCT_TS.MaTN = MaTn;
                    objCT_TS.MaChiTietTaiSan = txtMaTaiSan.EditValue as string;
                    objCT_TS.TenChiTietTaiSan = txtTenChiTietTS.EditValue as string;
                    objCT_TS.TinhTrangTaiSanID = (int?)gridLKTinhTrangTS.EditValue;
                    objCT_TS.tbl_TenTaiSan = db.tbl_TenTaiSans.FirstOrDefault(p => p.ID == (int?)gridLKTaiSan.EditValue);
                    objCT_TS.NhaCungCapID = (int?)gridLKNCC.EditValue;
                    objCT_TS.ThongSoKyThuat = txtThongSo.EditValue as string;
                    objCT_TS.MoTa = txtMoTa.EditValue as string;
                    objCT_TS.BlockID = (int?)gridLKKhoiNha.EditValue;
                    objCT_TS.NguyenGia = (decimal?)txtNguyenGia.EditValue;
                    objCT_TS.NgayMua = (DateTime?)dateNgayMua.EditValue;
                    objCT_TS.NgayDuaVaoSuDung = (DateTime?)dateNgaySuDung.EditValue;
                    objCT_TS.ViTri = txtvitri.EditValue as string;
                    objCT_TS.TonKho = (double?)txTonKho.Value;
                    objCT_TS.SoLuong = (double?)txtSoLuong.Value;
                    if (checkeditQuanTrong.Checked == true)
                        objCT_TS.QuanTrong = 1;
                    else
                        objCT_TS.QuanTrong = 0;

                    if (checkNSD.Checked == true)
                        objCT_TS.NgungSuDung = true;
                    else
                        objCT_TS.NgungSuDung = false;
                    objCT_TS.NgayHetHanBaoHanh = (DateTime?)dateNgayHetHanBH.EditValue;
                    objCT_TS.ThoiGianBaoHanh = (int?)txtThoigianBH.Value;
                    objCT_TS.NgaySua = Common.GetDateTimeSystem();
                    objCT_TS.NguoiSua = Common.User.MaNV;
                    #endregion
                }
                db.SubmitChanges();
                DialogResult = DialogResult.OK;
                DialogBox.Alert("Đã lưu thành công");
            }
            catch
            {
                DialogResult = DialogResult.Cancel;
                DialogBox.Error("Không lưu được, vui lòng kiểm tra lại");
            }
        }

        private void ItemHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ItemClearText_Click(object sender, EventArgs e)
        {
            Library.Class.HuongDan.ShowAuto.ClearText(this.Controls);
        }

        private void ItemHuongDan_Click(object sender, EventArgs e)
        {
            Library.Class.HuongDan.ShowAuto.ActiveDemo(true, controls);
        }

        private void gridLKHeThongTS_EditValueChanged(object sender, EventArgs e)
        {
            gridLKLoaiTaiSan.Properties.DataSource = db.tbl_LoaiTaiSans.Where(_ => _.NhomTaiSanID == (int?)gridLKHeThongTS.EditValue);
            gridLKLoaiTaiSan.EditValue = gridLKLoaiTaiSan.Properties.GetKeyValue(0);

        }

        private void gridLKLoaiTaiSan_EditValueChanged(object sender, EventArgs e)
        {
            gridLKTaiSan.Properties.DataSource = db.tbl_TenTaiSans.Where(o => o.LoaiTaiSanID == (int?)gridLKLoaiTaiSan.EditValue);
            gridLKTaiSan.EditValue = gridLKTaiSan.Properties.GetKeyValue(0);
        }

        private void txtMaTaiSan_Leave(object sender, EventArgs e)
        {
            var kt = db.tbl_ChiTietTaiSans.FirstOrDefault(o => o.MaChiTietTaiSan == txtMaTaiSan.EditValue & o.MaTN==this.MaTn & o.ID != Id);
            if(kt != null)
            {
                DialogBox.Error("Mã chi tiết tài sản đã tồn tại");
                txtMaTaiSan.Focus();
                return;
            }
        }
    }
}