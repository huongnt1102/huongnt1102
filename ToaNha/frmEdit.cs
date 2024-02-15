using System;
using Library;
using System.Linq;

namespace ToaNha
{
    public partial class frmEdit : DevExpress.XtraEditors.XtraForm
    {
        private MasterDataContext db;
        public tnNhanVien objNV;
        tnToaNha objTN;
        public byte? MaTN { get; set; }

        System.Collections.Generic.List<Library.Class.HuongDan.ShowAuto.ControlItem> controls;

        public frmEdit()
        {
            InitializeComponent();

            db = new MasterDataContext();
        }

        void LoadImage()
        {
            try
            {
                picLogo.Image = new System.Drawing.Bitmap(new System.IO.MemoryStream(new System.Net.WebClient().DownloadData(objTN.Logo)));
            }
            catch { }
        }

        private void picLogo_DoubleClick(object sender, EventArgs e)
        {
            using (var frm = new FTP.frmUploadFile())
            {
                if (frm.SelectFile(true))
                {
                    frm.Folder = "toanha/" + DateTime.Now.ToString("yyyy/MM/dd");
                    if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        objTN.Logo = frm.WebUrl;
                        picLogo.Tag = frm.WebUrl;
                        LoadImage();
                    }
                }
            }
        }

        private void KhoiTaoGiaTri()
        {
            GetGiaTri(ToaNha.Class.DanhMucDuAnEnum.CAP_CONG_TRINH);
            GetGiaTri(ToaNha.Class.DanhMucDuAnEnum.DON_VI_QUAN_LY);
            GetGiaTri(ToaNha.Class.DanhMucDuAnEnum.DON_VI_SU_DUNG);
            GetGiaTri(ToaNha.Class.DanhMucDuAnEnum.HANG_TOA_NHA);
            GetGiaTri(ToaNha.Class.DanhMucDuAnEnum.HINH_THUC_XAY_DUNG);
            GetGiaTri(ToaNha.Class.DanhMucDuAnEnum.KHU_DAT);
            GetGiaTri(ToaNha.Class.DanhMucDuAnEnum.NHOM_NHA);

            dateNgayDvSd.DateTime = System.DateTime.UtcNow.AddHours(7);
            dateNgayKhoiCong.DateTime = System.DateTime.UtcNow.AddHours(7);

            spinGiaTriMua.Value = 0;
            spinGiaTriXayDung.Value = 0;
            spinGiaTriPhanThietBi.Value = 0;
            spinDienTichXayDung.Value = 0;
            spinSoTangHam.Value = 0;
            spinSoTangNoi.Value = 0;
            spinTongDienTichSan.Value = 0;
            spinNgayCuoiCungThanhToanHoaDon.Value = 20;
            spinPhanTramLaiSuat.Value = 10;
        }

        private void GanGiaTri()
        {
            objTN = db.tnToaNhas.Single(p => p.MaTN == MaTN);

            txtDiaChi.Text = objTN.DiaChi;
            txtDienThoai.Text = objTN.DienThoai;
            txtEmail.Text = objTN.Email;
            txtFax.Text = objTN.Fax;
            txtNganHang.Text = objTN.NganHang;
            txtNguoiDaiDien.Text = objTN.NguoiDaiDien;
            txtCongTYQL.Text = objTN.ChuTaiKhoan;
            txtSoTaiKhoan.Text = objTN.SoTaiKhoan;
            txtTenTN.Text = objTN.TenTN;
            txtTenVietTat.Text = objTN.TenVT;
            txtTenNgan.Text = objTN.TenNgan;
            picLogo.Tag = objTN.Logo;
            LoadImage();
            txtMaSoThue.Text = objTN.MaSoThue;
            txtCongTYQL.Text = objTN.CongTyQuanLy;
            txtDiaCHiCTY.Text = objTN.DiaChiCongTy;
            //txtBrandName.EditValue = objTN.BrandName;
            //txtTaiKhoan.EditValue = objTN.UserName;
            //txtMatKhau.EditValue = objTN.MatKhau;
            txtSoBbbg.Text = objTN.SoBbbg;
            txtMoTa.Text = objTN.MoTa;
            txtLeTan.Text = objTN.LeTan;
            txtSdtLeTan.Text = objTN.SdtLeTan;
            txtNhanVienQuanLy.Text = objTN.NhanVienQuanLy;
            txtSdtNhanVienQuanLy.Text = objTN.SdtNhanVienQuanLy;
            txtSdtDoiTruongBaoVe.Text = objTN.DoiTruongBaoVe;
            txtSdtDoiTruongBaoVe.Text = objTN.SdtDoiTruongBaoVe;
            txtDauMoiBql.Text = objTN.DauMoiBql;
            txtSdtDauMoiBql.Text = objTN.SdtDauMoiBql;

            if (objTN.CapCongTrinhId != null) glkCapCongTrinh.EditValue = objTN.CapCongTrinhId;
            if (objTN.DonViQuanLyId != null) glkDonViQuanLy.EditValue = objTN.DonViQuanLyId;
            if (objTN.DonViSuDungId != null) glkDonViSuDung.EditValue = objTN.DonViSuDungId;
            if (objTN.HangToaNhaId != null) glkHangToaNha.EditValue = objTN.HangToaNhaId;
            if (objTN.HinhThucXayDungId != null) glkHinhThuc.EditValue = objTN.HinhThucXayDungId;
            if (objTN.KhuDatId != null) glkKhuDat.EditValue = objTN.KhuDatId;
            if (objTN.NhomNhaId != null) glkNhomNha.EditValue = objTN.NhomNhaId;

            if (objTN.NgayDvSd != null) dateNgayDvSd.DateTime = (DateTime)objTN.NgayDvSd;
            if (objTN.NgayKhoiCong != null) dateNgayKhoiCong.DateTime = (DateTime)objTN.NgayKhoiCong;

            if (objTN.GiaTriMua != null) spinGiaTriMua.Value = (decimal) objTN.GiaTriMua;
            if (objTN.GiaTriXayDung != null) spinGiaTriXayDung.Value = (decimal) objTN.GiaTriXayDung;
            if (objTN.GiaTriPhanThietBi != null) spinGiaTriPhanThietBi.Value = (decimal) objTN.GiaTriPhanThietBi;
            if (objTN.DienTichXayDung != null) spinDienTichXayDung.Value = (decimal) objTN.DienTichXayDung;
            if (objTN.SoTangHam != null) spinSoTangHam.Value = (decimal) objTN.SoTangHam;
            if (objTN.SoTangNoi != null) spinSoTangNoi.Value = (decimal) objTN.SoTangNoi;
            if (objTN.TongDienTichSan != null) spinTongDienTichSan.Value = (decimal) objTN.TongDienTichSan;

            spinNgayCuoiCungThanhToanHoaDon.Value = (decimal)objTN.NgayCuoiCungThanhToanTrongThang.GetValueOrDefault();
            spinPhanTramLaiSuat.Value = (decimal)objTN.PhanTramLaiSuat.GetValueOrDefault();

            txtCompanyCode.EditValue = objTN.CompanyCode;
        }

        private void GetGiaTri(string name)
        {
            var data = new MasterDataContext();
            switch (name)
            {
                case ToaNha.Class.DanhMucDuAnEnum.CAP_CONG_TRINH: glkCapCongTrinh.Properties.DataSource = data.tnCapCongTrinhs; break;
                case ToaNha.Class.DanhMucDuAnEnum.DON_VI_QUAN_LY: glkDonViQuanLy.Properties.DataSource = data.tnDonViQuanLies; break;
                case ToaNha.Class.DanhMucDuAnEnum.DON_VI_SU_DUNG: glkDonViSuDung.Properties.DataSource = data.tnDonViSuDungs; break;
                case ToaNha.Class.DanhMucDuAnEnum.HANG_TOA_NHA: glkHangToaNha.Properties.DataSource = data.tnHangToaNhas; break;
                case ToaNha.Class.DanhMucDuAnEnum.HINH_THUC_XAY_DUNG: glkHinhThuc.Properties.DataSource = data.tnHinhThucXayDungs; break;
                case ToaNha.Class.DanhMucDuAnEnum.KHU_DAT: glkKhuDat.Properties.DataSource = data.tnKhuDats; break;
                case ToaNha.Class.DanhMucDuAnEnum.NHOM_NHA: glkNhomNha.Properties.DataSource = data.tnNhomNhas; break;
            }
            data.Dispose();
        }

        private void frmEdit_Load(object sender, EventArgs e)
        {
            controls = Library.Class.HuongDan.ShowAuto.GetControlItems(this.Controls);
            Library.Class.HuongDan.ShowAuto.ClearText(this.Controls);

            KhoiTaoGiaTri();

            if (MaTN != null)
            {
                try
                {
                    GanGiaTri();
                }
                catch { }
            }
            else
                picLogo.Tag = "";
        }

        private void ItemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (txtTenVietTat.Text.Trim() == "")
            {
                DialogBox.Alert("Vui lòng nhập [Tên viết tắt], xin cảm ơn.");
                txtTenVietTat.Focus();
                return;
            }

            if (txtTenTN.Text.Trim() == "")
            {
                DialogBox.Alert("Vui lòng nhập [Tên Dự án], xin cảm ơn.");
                txtTenTN.Focus();
                return;
            }

            if (txtTenNgan.Text.Trim() == "")
            {
                DialogBox.Alert("Vui lòng nhập [Tên ngắn], xin cảm ơn.");
                txtTenNgan.Focus();
                return;
            }

            if (MaTN == null)
            {
                objTN = new tnToaNha();
                objTN.MaNV = Common.User.MaNV;
                objTN.NgayTao = db.GetSystemDate();
                byte max = db.tnToaNhas.Max(p => p.MaTN);
                objTN.MaTN = (byte)(max + 1);
                db.tnToaNhas.InsertOnSubmit(objTN);
            }
            else
            {
                objTN.NgayCN = db.GetSystemDate();
                objTN.MaNVCN = Common.User.MaNV;
            }
            objTN.MaSoThue = txtMaSoThue.Text;
            objTN.ChuTaiKhoan = txtCongTYQL.Text.Trim();
            objTN.DiaChi = txtDiaChi.Text.Trim();
            objTN.DienThoai = txtDienThoai.Text.Trim();
            objTN.Email = txtEmail.Text.Trim();
            objTN.Fax = txtFax.Text.Trim();
            objTN.Logo = picLogo.Tag == null ? "" : picLogo.Tag.ToString();
            objTN.NganHang = txtNganHang.Text.Trim();
            objTN.NguoiDaiDien = txtNguoiDaiDien.Text.Trim();
            objTN.SoTaiKhoan = txtSoTaiKhoan.Text.Trim();
            objTN.TenTN = txtTenTN.Text.Trim();
            objTN.TenVT = txtTenVietTat.Text.Trim();
            objTN.TenNgan = txtTenNgan.Text.Trim();
            objTN.CongTyQuanLy = txtCongTYQL.Text.Trim();
            objTN.DiaChiCongTy = txtDiaCHiCTY.Text.Trim();
            //objTN.BrandName = txtBrandName.Text.Trim();
            //objTN.UserName = txtTaiKhoan.Text.Trim();
            //objTN.MatKhau = txtMatKhau.Text.Trim();
            objTN.SoBbbg = txtSoBbbg.Text;
            objTN.MoTa = txtMoTa.Text;
            objTN.LeTan = txtLeTan.Text;
            objTN.SdtLeTan = txtSdtLeTan.Text;
            objTN.NhanVienQuanLy = txtNhanVienQuanLy.Text;
            objTN.SdtNhanVienQuanLy = txtSdtNhanVienQuanLy.Text;
            objTN.DoiTruongBaoVe = txtDoiTruongBaoVe.Text;
            objTN.SdtDoiTruongBaoVe = txtDoiTruongBaoVe.Text;
            objTN.DauMoiBql = txtDauMoiBql.Text;
            objTN.SdtDauMoiBql = txtSdtDauMoiBql.Text;

            objTN.GiaTriMua = spinGiaTriMua.Value;
            objTN.GiaTriXayDung = spinGiaTriXayDung.Value;
            objTN.GiaTriPhanThietBi = spinGiaTriPhanThietBi.Value;
            objTN.DienTichXayDung = spinDienTichXayDung.Value;
            objTN.SoTangHam = spinSoTangHam.Value;
            objTN.SoTangNoi = spinSoTangNoi.Value;
            objTN.TongDienTichSan = spinTongDienTichSan.Value;

            if (glkKhuDat.EditValue != null) objTN.KhuDatId = (int)glkKhuDat.EditValue;
            if (glkHinhThuc.EditValue != null) objTN.HinhThucXayDungId = (int)glkHinhThuc.EditValue;
            if (glkCapCongTrinh.EditValue != null) objTN.CapCongTrinhId = (int)glkCapCongTrinh.EditValue;
            if (glkHangToaNha.EditValue != null) objTN.HangToaNhaId = (int)glkHangToaNha.EditValue;
            if (glkNhomNha.EditValue != null) objTN.NhomNhaId = (int)glkNhomNha.EditValue;
            if (glkDonViSuDung.EditValue != null) objTN.DonViSuDungId = (int)glkDonViSuDung.EditValue;
            if (glkDonViQuanLy.EditValue != null) objTN.DonViQuanLyId = (int)glkDonViQuanLy.EditValue;

            objTN.NgayKhoiCong = dateNgayKhoiCong.DateTime;
            objTN.NgayDvSd = dateNgayDvSd.DateTime;

            objTN.NgayCuoiCungThanhToanTrongThang = (int?)spinNgayCuoiCungThanhToanHoaDon.Value;
            objTN.PhanTramLaiSuat = (decimal?)spinPhanTramLaiSuat.Value;

            objTN.CompanyCode = txtCompanyCode.Text;

            try
            {
                db.SubmitChanges();

                DialogResult = System.Windows.Forms.DialogResult.OK;
                DialogBox.Alert("Dữ liệu đã được cập nhật");
                this.Close();
            }
            catch { DialogBox.Alert("Đã xảy ra lỗi. Vui lòng kiểm tra lại, xin cảm ơn."); }
        }

        private void ItemCancel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void ItemKhuDat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var frm = new ToaNha.DanhMuc.FrmDanhMucDuAn() { Loai = ToaNha.Class.DanhMucDuAnEnum.KHU_DAT })
            {
                frm.ShowDialog();
                GetGiaTri(ToaNha.Class.DanhMucDuAnEnum.KHU_DAT);
            }
        }

        private void ItemHinhThucXayDung_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var frm = new ToaNha.DanhMuc.FrmDanhMucDuAn() { Loai = ToaNha.Class.DanhMucDuAnEnum.HINH_THUC_XAY_DUNG})
            {
                frm.ShowDialog();
                GetGiaTri(ToaNha.Class.DanhMucDuAnEnum.HINH_THUC_XAY_DUNG);
            }
        }

        private void ItemHangToaNha_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var frm = new ToaNha.DanhMuc.FrmDanhMucDuAn() { Loai = ToaNha.Class.DanhMucDuAnEnum.HANG_TOA_NHA})
            {
                frm.ShowDialog();
                GetGiaTri(ToaNha.Class.DanhMucDuAnEnum.HANG_TOA_NHA);
            }
        }

        private void ItemNhomNha_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var frm = new ToaNha.DanhMuc.FrmDanhMucDuAn() { Loai = ToaNha.Class.DanhMucDuAnEnum.NHOM_NHA })
            {
                frm.ShowDialog();
                GetGiaTri(ToaNha.Class.DanhMucDuAnEnum.NHOM_NHA);
            }
        }

        private void ItemDonViSuDung_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var frm = new ToaNha.DanhMuc.FrmDanhMucDuAn() { Loai = ToaNha.Class.DanhMucDuAnEnum.DON_VI_SU_DUNG})
            {
                frm.ShowDialog();
                GetGiaTri(ToaNha.Class.DanhMucDuAnEnum.DON_VI_SU_DUNG);
            }
        }

        private void ItemDonViQuanLy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var frm = new ToaNha.DanhMuc.FrmDanhMucDuAn() { Loai = ToaNha.Class.DanhMucDuAnEnum.DON_VI_QUAN_LY })
            {
                frm.ShowDialog();
                GetGiaTri(ToaNha.Class.DanhMucDuAnEnum.DON_VI_QUAN_LY);
            }
        }

        private void itemCapCongTrinh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var frm = new ToaNha.DanhMuc.FrmDanhMucDuAn {Loai = ToaNha.Class.DanhMucDuAnEnum.CAP_CONG_TRINH})
            {
                frm.ShowDialog();
                GetGiaTri(ToaNha.Class.DanhMucDuAnEnum.CAP_CONG_TRINH);
            }
        }

        private void itemHuongDan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Library.Class.HuongDan.ShowAuto.ActiveDemo(true, controls);
        }

        private void itemClearText_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Library.Class.HuongDan.ShowAuto.ClearText(this.Controls);
            //txtMaKHCN.Text = getNewMaKH();
        }
    }
}