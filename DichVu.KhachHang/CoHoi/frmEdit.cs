using System;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Library;

namespace DichVu.KhachHang.CoHoi
{
    public partial class FrmEdit : DevExpress.XtraEditors.XtraForm
    {
        private MasterDataContext _db = new MasterDataContext();
        public tnNhanVien objnv;
        public tnKhachHang objKH;
        public byte? maTN;
        public bool IsSua = false;
        public bool IsNCC = false;

        private string _loaiCoHoiName;
        private string _nguonDenName;
        private string _nhanVienPhuTrachName;

        public FrmEdit()
        {
            InitializeComponent();
            TranslateLanguage.TranslateControl(this);
        }

        private string GetNewMaKh()
        {
            string MaKH = "";
            _db.khKhachHang_getNewMaKH(ref MaKH);
            var tn = _db.tnToaNhas.First(o => o.MaTN == this.maTN);
            return tn.TenVT + _db.DinhDang(7, int.Parse(MaKH));
        }

        public bool IsCaNhan
        {
            get
            {
                return tabKhachHang.SelectedTabPageIndex == 0;
            }
            set
            {
                tabKhachHang.SelectedTabPageIndex = value ? 0 : 1;
            }
        }

        private void frmEdit_Load(object sender, EventArgs e)
        {
            // load danh mục
            LoadDanhMuc(DichVu.KhachHang.Class.KhachHang.DanhMuc.KHU_VUC);
            LoadDanhMuc(DichVu.KhachHang.Class.KhachHang.DanhMuc.LOAI_CO_HOI);
            LoadDanhMuc(DichVu.KhachHang.Class.KhachHang.DanhMuc.LOAI_KH);
            LoadDanhMuc(DichVu.KhachHang.Class.KhachHang.DanhMuc.NV_PHU_TRACH);
            LoadDanhMuc(DichVu.KhachHang.Class.KhachHang.DanhMuc.NGUON_DEN);
            LoadDanhMuc(DichVu.KhachHang.Class.KhachHang.DanhMuc.NHOM_KH);

            objKH = GetKhachHang();

            KhoiTaoGiaTri(objKH.MaKH != 0);
        }

        private void LoadDanhMuc (string danhMuc)
        {
            var db = new Library.MasterDataContext();
            switch (danhMuc)
            {
                case DichVu.KhachHang.Class.KhachHang.DanhMuc.NHOM_KH: lookUpNhomKH.Properties.DataSource = db.khNhomKhachHangs.Where(p => p.MaTN == maTN); break;
                case DichVu.KhachHang.Class.KhachHang.DanhMuc.LOAI_KH: lkLoaiKhachHang.Properties.DataSource = db.khLoaiKhachHangs.Where(p => p.MaTN == maTN).OrderBy(k => k.TenLoaiKH); break;
                case DichVu.KhachHang.Class.KhachHang.DanhMuc.KHU_VUC: lookKhuVuc.Properties.DataSource = db.tnKhuVucs; break;
                case DichVu.KhachHang.Class.KhachHang.DanhMuc.LOAI_CO_HOI: glkLoaiTiemNang.Properties.DataSource = db.ch_KhachHang_Loais; break;
                case DichVu.KhachHang.Class.KhachHang.DanhMuc.NGUON_DEN: glkNguonDen.Properties.DataSource = db.tnycNguonDens; break;
                case DichVu.KhachHang.Class.KhachHang.DanhMuc.NV_PHU_TRACH: glkNhanVienPhuTrach.Properties.DataSource = db.tnNhanViens; break;
            }

            db.Dispose();
        }

        private Library.tnKhachHang GetKhachHang()
        {
            return objKH != null ? _db.tnKhachHangs.First(_ => _.MaKH == objKH.MaKH) : new Library.tnKhachHang();
        }

        private void DuLieuChung()
        {
            chkTiemNang.Checked = true;
            chkCoHoi.Checked = true;

            _loaiCoHoiName = "";
            _nguonDenName = "";
            _nhanVienPhuTrachName = "";
        }

        private void KhoiTaoGiaTri(bool isEdit)
        {
            DuLieuChung();

            switch (isEdit)
            {
                case true:
                    txtMaKHCN.Text = objKH.KyHieu;
                    txtHoKH.EditValue = objKH.HoKH;
                    txtTenKH.EditValue = objKH.TenKH;
                    chkGioiTinh.Checked = objKH.GioiTinh.GetValueOrDefault();
                    chkGioiTinhNu.Checked = !objKH.GioiTinh.GetValueOrDefault();
                    dateNgaySinh.EditValue = objKH.NgaySinh;
                    txtCMND.EditValue = objKH.CMND;
                    txtNgayCap.EditValue = objKH.NgayCap;
                    txtNoiCap.EditValue = objKH.NoiCap;
                    txtDienThoai.EditValue = objKH.DienThoaiKH;
                    txtEmail.EditValue = objKH.EmailKH;
                    txtMaSoThueCN.Text = objKH.MaSoThue;
                    txtDCTT.EditValue = objKH.DCTT;
                    txtDCLL.EditValue = objKH.DCLL;
                    txtCtyTen.EditValue = objKH.CtyTen;
                    txtCtyTenVT.EditValue = objKH.CtyTenVT;
                    txtCtyDiaChi.EditValue = objKH.DCLL;
                    txtCtyDienThoai.EditValue = objKH.DienThoaiKH;
                    txtCtyFax.EditValue = objKH.CtyFax;
                    txtTenNHL.Text = objKH.NguoiLH;
                    txtSDTNLH.Text = objKH.SDTNguoiLH;
                    txtCtyDaiDien.EditValue = objKH.CtyNguoiDD;
                    txtCtyChucVu.EditValue = objKH.CtyChucVuDD;
                    txtCtySoDKKD.EditValue = objKH.CtySoDKKD;
                    txtCtyNgayDKKD.EditValue = objKH.CtyNgayDKKD;
                    txtCtyNoiDKKD.EditValue = objKH.CtyNoiDKKD;
                    txtCtySoTKNH.EditValue = objKH.CtySoTKNH;
                    txtCtyTenNH.EditValue = objKH.CtyTenNH;
                    lookKhuVuc.EditValue = objKH.MaKV;
                    txtMaSoThue.Text = objKH.CtyMaSoThue;
                    if (objKH.IsCaNhan != null) this.IsCaNhan = objKH.IsCaNhan.Value;
                    txtTKNganHang.Text = objKH.TaiKhoanNganHang;
                    txtQuocTich.Text = objKH.QuocTich;
                    txtMaPhu.Text = objKH.MaPhu;
                    lookUpNhomKH.EditValue = objKH.MaNKH;
                    lkLoaiKhachHang.EditValue = objKH.MaLoaiKH;
                    txtNguoiDaiDien.Text = objKH.NguoiDongSoHuu;
                    this.IsNCC = objKH.isNCC ?? false;

                    // cơ hội
                    if (objKH.IsCoHoi != null) chkCoHoi.Checked = (bool) objKH.IsCoHoi;
                    if (objKH.LoaiTiemNangId != null) glkLoaiTiemNang.EditValue = (int) objKH.LoaiTiemNangId;
                    if (objKH.NguonDenId != null) glkNguonDen.EditValue = (int) objKH.NguonDenId;
                    if (objKH.NhanVienPhuTrachId != null) glkNhanVienPhuTrach.EditValue = (int) objKH.NhanVienPhuTrachId;

                    _loaiCoHoiName = objKH.LoaiTiemNangName;
                    _nguonDenName = objKH.NguonDenName;
                    _nhanVienPhuTrachName = objKH.NhanVienPhuTrachName;

                    if (IsNCC)
                    {
                        tabKhachHang.SelectedTabPageIndex = 2;
                        txtEmailNCC.Text = objKH.EmailKH;
                        txtTenNCC.Text = objKH.CtyTen;
                        txtDiaChiNCC.Text = objKH.DCLL;
                        txtDienThoaiNCC.Text = objKH.DienThoaiKH;
                        txtTKNganHangNCC.Text = objKH.CtySoTKNH;
                        txtTenTKNganHangNCC.Text = objKH.CtyTenNH;
                        txtMaSoThueNCC.Text = objKH.CtyMaSoThue;
                        txtGhiChuNCC.Text = objKH.GhiChu;
                        txtFaxNCC.Text = objKH.CtyFax;
                    }

                    break;

                case false:
                    if (IsNCC)
                        tabKhachHang.SelectedTabPageIndex = 2;
                    objKH.MaTN = this.maTN;
                    _db.tnKhachHangs.InsertOnSubmit(objKH);

                    txtMaKHCN.Text = GetNewMaKh();
                    lookKhuVuc.ItemIndex = 0;
                    txtMaKHCN.Properties.ReadOnly = false;

                    chkGioiTinh.Checked = true;
                    
                    break;
            }
        }

        private bool KiemTraDuLieu()
        {
            var ltToaNha = Common.TowerList.Select(p => p.MaTN.ToString()).ToList();
            var ltMaKH = _db.tnKhachHangs.Where(p => p.MaKH != objKH.MaKH && ltToaNha.Contains(p.MaTN.ToString())).Select(p => p.KyHieu).ToList();
            var ltMaPhu = _db.tnKhachHangs.Where(p => p.MaKH != objKH.MaKH && ltToaNha.Contains(p.MaTN.ToString())).Select(p => p.MaPhu).ToList();
            if (tabKhachHang.SelectedTabPageIndex == 0 || tabKhachHang.SelectedTabPageIndex == 1)
            {
                if (this.IsCaNhan)
                {
                    if (txtTenKH.Text.Trim() == "")
                    {
                        DialogBox.Error("Vui lòng nhập [Tên khách hàng], xin cảm ơn.");
                        txtTenKH.Focus();
                        return true;
                    }
                }
                else
                {
                    if (txtCtyTen.Text.Trim() == "")
                    {
                        DialogBox.Error("Vui lòng nhập [Tên công ty], xin cảm ơn.");
                        txtCtyTen.Focus();
                        return true;
                    }
                }
            }
            else
            {
                if (tabKhachHang.SelectedTabPageIndex == 2)//nếu là khách hàng nhà cung cấp
                {
                    if (txtTenNCC.Text.Trim() == "")
                    {
                        DialogBox.Error("Vui lòng nhập tên [Nhà cung cấp], xin cảm ơn.");
                        txtTenNCC.Focus();
                        return true;
                    }
                }
            }

            if (lookKhuVuc.EditValue == null)
            {
                DialogBox.Error("Vui lòng chọn [Khu vực], xin cảm ơn.");
                return true;
            }

            if (txtMaKHCN.Text == "" || txtMaKHCN.ToString().Trim() == "")
            {
                DialogBox.Error("Vui lòng nhập [Mã khách hàng], xin cám ơn");
                return true;
            }

            if (ltMaKH.Contains(txtMaKHCN.Text.Trim()) & objKH == null)
            {
                DialogBox.Error("Mã khách hàng đã tồn tại vui lòng nhập lại, xin cám ơn");
                return true;
            }

            if (txtMaPhu.Text.Trim() != "" && ltMaPhu.Contains(txtMaPhu.Text.Trim()) & objKH == null)
            {
                DialogBox.Error("Mã phụ đã tồn tại vui lòng nhập lại, xin cám ơn");
                return true;
            }
            if (lookUpNhomKH.EditValue == null)
            {
                DialogBox.Error("Vui lòng chọn nhóm khách hàng");
                return true;
            }

            return false;
        }

        private void chkGioiTinh_CheckedChanged(object sender, EventArgs e)
        {
            if (chkGioiTinh.Checked)
                chkGioiTinhNu.Checked = false;
        }

        private void chkGioiTinhNu_CheckedChanged(object sender, EventArgs e)
        {
            if (chkGioiTinhNu.Checked)
                chkGioiTinh.Checked = false;
        }

        private void txtCtyDienThoai_EditValueChanged(object sender, EventArgs e)
        {
            var tam = (TextEdit)sender;
            txtSDTNLH.Text = tam.Text;
        }

        private void txtCtyDaiDien_EditValueChanged(object sender, EventArgs e)
        {
            if (txtCtyDaiDien.EditValue != null)
            {
                if (txtCtyDaiDien.EditValue.ToString() != "")
                {
                    txtTenKH.EditValue = txtCtyDaiDien.EditValue;
                }
            }
        }

        private void LuuLichSu()
        {
            if (!(IsSua == true & objKH != null)) return;
            using (var dbo = new MasterDataContext())
            {
                LichSuThayDoiKH objLS = new LichSuThayDoiKH();
                objLS.MaKH = objKH.MaKH;
                objLS.TenCu = IsNCC ? objKH.CtyTen : objKH.IsCaNhan.GetValueOrDefault() ? (objKH.HoKH + " " + objKH.TenKH) : objKH.CtyTen;
                objLS.TenMoi = IsNCC ? txtTenNCC.Text : objKH.IsCaNhan.GetValueOrDefault() ? (txtHoKH.Text + " " + txtTenKH.Text) : txtCtyTen.Text;
                objLS.NgayThayDoi = DateTime.Now;
                objLS.NguoiThayDoi = Common.User.HoTenNV;
                dbo.LichSuThayDoiKHs.InsertOnSubmit(objLS);
                dbo.SubmitChanges();
            }
        }

        private void ItemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (KiemTraDuLieu()) return;

            IsNCC = tabKhachHang.SelectedTabPageIndex == 2;
            LuuLichSu();

            objKH.IsCaNhan = this.IsCaNhan;
            objKH.HoKH = txtHoKH.Text.Trim();
            objKH.TenKH = txtTenKH.Text.Trim();
            objKH.GioiTinh = chkGioiTinh.Checked;
            objKH.NgaySinh = String.CompareOrdinal(dateNgaySinh.Text, "") != 0 ? dateNgaySinh.DateTime : (DateTime?)(null);
            objKH.CMND = txtCMND.Text.Trim();
            objKH.NgayCap = txtNgayCap.Text.Trim();
            objKH.NoiCap = txtNoiCap.Text.Trim();
            objKH.NguoiLH = txtTenNHL.Text;
            objKH.SDTNguoiLH = txtSDTNLH.Text;

            objKH.DienThoaiKH = txtDienThoai.Text.Trim();
            objKH.EmailKH = txtEmail.Text.Trim();
            objKH.DCTT = txtDCTT.Text.Trim();
            objKH.DCLL = txtDCLL.Text.Trim();
            objKH.CtyTen = IsNCC ? txtTenNCC.Text.Trim() : txtCtyTen.Text.Trim();//////NCC
            objKH.CtyTenVT = txtCtyTenVT.Text.Trim();
            //objKH.CtyDiaChi = IsNCC ? txtDiaChiNCC.Text.Trim() : txtCtyDiaChi.Text.Trim();
            //objKH.CtyDienThoai = IsNCC ? txtDienThoaiNCC.Text.Trim() : txtCtyDienThoai.Text.Trim();
            objKH.CtyFax = IsNCC ? txtFaxNCC.Text.Trim() : txtCtyFax.Text.Trim();

            if (IsCaNhan != true)
            {
                objKH.DienThoaiKH = IsNCC ? txtDienThoaiNCC.Text : txtSDTNLH.Text;
            }


            objKH.CtyNguoiDD = txtCtyDaiDien.Text.Trim();
            objKH.CtyChucVuDD = txtCtyChucVu.Text.Trim();
            objKH.CtySoDKKD = txtCtySoDKKD.Text.Trim();
            objKH.CtyNgayDKKD = txtCtyNgayDKKD.Text.Trim();
            objKH.CtyNoiDKKD = txtCtyNoiDKKD.Text.Trim();
            objKH.CtySoTKNH = IsNCC ? txtTKNganHangNCC.Text.Trim() : txtCtySoTKNH.Text.Trim();
            objKH.CtyTenNH = IsNCC ? txtTenTKNganHangNCC.Text.Trim() : txtCtyTenNH.Text.Trim();
            objKH.CtyMaSoThue = IsNCC ? txtMaSoThueNCC.Text.Trim() : txtMaSoThue.Text.Trim();
            objKH.tnKhuVuc = _db.tnKhuVucs.Single(p => p.MaKV == (int)lookKhuVuc.EditValue);
            objKH.tnNhanVien = _db.tnNhanViens.Single(p => p.MaNV == objnv.MaNV);
            objKH.KyHieu = txtMaKHCN.Text.Trim();
            objKH.MaPhu = txtMaPhu.Text.Trim();
            objKH.TaiKhoanNganHang = txtTKNganHang.Text.Trim();
            objKH.QuocTich = txtQuocTich.Text.Trim();
            objKH.MaSoThue = txtMaSoThueCN.Text.Trim();
            objKH.MaNKH = (int?)lookUpNhomKH.EditValue;
            objKH.MaLoaiKH = (int?)lkLoaiKhachHang.EditValue;
            objKH.NguoiDongSoHuu = txtNguoiDaiDien.Text;
            objKH.GhiChu = IsNCC ? txtGhiChuNCC.Text.Trim() : "";
            objKH.isNCC = this.IsNCC;

            LuuCoHoi();

            try
            {
                _db.SubmitChanges();
                this.DialogResult = DialogResult.OK;
                this.Close();
                DialogBox.Alert("Lưu khách hàng thành công!");
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
        }

        private void LuuCoHoi()
        {
            if (glkLoaiTiemNang.EditValue != null) objKH.LoaiTiemNangId = (int) glkLoaiTiemNang.EditValue;
            if (glkNguonDen.EditValue != null) objKH.NguonDenId = (int) glkNguonDen.EditValue;
            if (glkNhanVienPhuTrach.EditValue != null) objKH.NhanVienPhuTrachId = (int) glkNhanVienPhuTrach.EditValue;

            objKH.LoaiTiemNangName = _loaiCoHoiName;
            objKH.NguonDenName = _nguonDenName;
            objKH.NhanVienPhuTrachName = _nhanVienPhuTrachName;

            objKH.IsCoHoi = chkCoHoi.Checked;
            objKH.IsTiemNang = chkTiemNang.Checked;
        }

        private void ItemCancel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void ItemLoaiCoHoi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var frm = new DichVu.KhachHang.DanhMuc.FrmLoaiCoHoi())
            {
                frm.ShowDialog();
                LoadDanhMuc(DichVu.KhachHang.Class.KhachHang.DanhMuc.LOAI_CO_HOI);
            }
        }

        private void ItemNguonDen_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var frm = new DichVu.YeuCau.frmNguonDen())
            {
                frm.ShowDialog();
                LoadDanhMuc(DichVu.KhachHang.Class.KhachHang.DanhMuc.NGUON_DEN);
            }
        }

        private void GlkLoaiTiemNang_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                var item = sender as DevExpress.XtraEditors.GridLookUpEdit;
                if (item == null) return;
                if (item.EditValue == null) return;
                _loaiCoHoiName = item.Properties.View.GetFocusedRowCellValue("Name").ToString();
            } catch{}
        }

        private void GlkNguonDen_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                var item = sender as DevExpress.XtraEditors.GridLookUpEdit;
                if (item == null) return;
                if (item.EditValue == null) return;
                _nguonDenName = item.Properties.View.GetFocusedRowCellValue("TenNguonDen").ToString();
            }
            catch { }
        }

        private void GlkNhanVienPhuTrach_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                var item = sender as DevExpress.XtraEditors.GridLookUpEdit;
                if (item == null) return;
                if (item.EditValue == null) return;
                _nhanVienPhuTrachName = item.Properties.View.GetFocusedRowCellValue("HoTenNV").ToString();
            }
            catch { }
        }
    }
}