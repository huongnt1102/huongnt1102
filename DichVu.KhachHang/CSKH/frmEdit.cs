using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using Library;
using System.Xml;
//using System.ServiceModel;
//using Library.Invoice;

namespace DichVu.KhachHang.CSKH
{
    public partial class frmEdit : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db = new MasterDataContext();

        public tnKhachHang objKH;

        public int? MaKH;

        public byte maTN;

        public bool IsView = false;

        public bool IsRoot = false;

        public bool IsResearch = false;

        public bool IsCSKH = false;

        public bool IsChinhThuc = false;

        public frmEdit()
        {
            InitializeComponent();
        }

        string getNewMaKH()
        {
            string MaKH = "";
            db.khKhachHang_getNewMaKH(ref MaKH);
            var tn = db.tnToaNhas.FirstOrDefault(o => o.MaTN == this.maTN);
            return tn.TenVT + db.DinhDang(7, int.Parse(MaKH));
        }

        public bool IsCaNhan { get; set; }

        private void frmEdit_Load(object sender, EventArgs e)
        {
            btnLuu.Visible = !IsView;
            lookDanhXungNLH.Properties.DataSource = db.QuyDanhs.ToList();
            lookUpNhomKH.Properties.DataSource = db.khNhomKhachHangs;
            lkLoaiKhachHang.Properties.DataSource = db.khLoaiKhachHangs.OrderBy(k => k.TenLoaiKH);
            cbNgheNghiepEdit.Properties.DataSource = db.NgheNghieps.OrderBy(p => p.STT).ToList();
            lookKhuVuc.Properties.DataSource = db.tnKhuVucs;
            lkTinh.Properties.DataSource = db.Tinhs.OrderBy(p => p.TenTinh).ToList();
            lkLoaiHinhKinhDoanh.Properties.DataSource = db.LoaiHinhKinhDoanhs.OrderBy(p => p.TenLoaiHinhKD).ToList();
            ctlQuyMoEdit1.LoadData();
            ctlNguonKHEdit1.LoadData();

            if (this.MaKH > 0)
            {
                objKH = db.tnKhachHangs.Single(p => p.MaKH == this.MaKH);
                objKH.MaNVS = Common.User.MaNV;
                objKH.NgaySua = db.GetSystemDate();

                ckbUpdateDiaChi.Checked = !objKH.IsRoot.GetValueOrDefault();
                
            }
            else
            {
                if (!this.IsResearch)
                {
                    objKH = new tnKhachHang();
                }
                objKH.IsCaNhan = !this.IsResearch;
                objKH.MaTN = this.maTN;
                objKH.MaNVN = Common.User.MaNV;
                objKH.NgayNhap = db.GetSystemDate();
                objKH.GioiTinh = true;
                
                objKH.IsKhachHang = true;
                objKH.IsRoot = this.IsRoot;
                db.tnKhachHangs.InsertOnSubmit(objKH);
                ckbUpdateDiaChi.Checked = true;
            }
            if (IsCSKH)
                txtMaKHCN.Properties.ReadOnly = true;

            txtMaKHCN.Text = objKH.KyHieu;
            txtHoKH.EditValue = objKH.HoKH;
            txtTenKH.EditValue = objKH.TenKH;
            rbGioiTinh.EditValue = objKH.GioiTinh.GetValueOrDefault();
            dateNgaySinh.EditValue = objKH.NgaySinh;
            txtCMND.EditValue = objKH.CMND;
            txtNgayCap.EditValue = objKH.NgayCap;
            txtNoiCap.EditValue = objKH.NoiCap;
            txtDiDong.Text = objKH.DiDong;
            txtDienThoai.EditValue = objKH.DienThoai;
            txtEmail.EditValue = objKH.Email;
            txtDCTT.EditValue = objKH.DCTT;
            txtDiaChi.EditValue = objKH.DiaChi;

            txtCtyTen.EditValue = objKH.CtyTen;
            txtCtyTenVT.EditValue = objKH.CtyTenVT;
            txtFax.EditValue = objKH.Fax;
            ctlNguonKHEdit1.EditValue = objKH.MaNguon;
            txtCtyDaiDien.EditValue = objKH.CtyNguoiDD;
            txtCtyChucVu.EditValue = objKH.CtyChucVuDD;
            txtCtySoDKKD.EditValue = objKH.CtySoDKKD;
            txtCtyNgayDKKD.EditValue = objKH.CtyNgayDKKD;
            txtCtyNoiDKKD.EditValue = objKH.CtyNoiDKKD;
            //txtSoTKNH.EditValue = objKH.SoTKNH;
            //txtTenNH.EditValue = objKH.TenNH;
            lookKhuVuc.EditValue = objKH.MaKV;
            txtMaSoThue.Text = objKH.MaSoThue;
            this.IsCaNhan = objKH.IsCaNhan.GetValueOrDefault();
            txtQuocTich.Text = objKH.QuocTich;
            txtMaPhu.Text = objKH.MaPhu;
            lookUpNhomKH.EditValue = objKH.MaNKH;
            lkLoaiKhachHang.EditValue = objKH.MaLoaiKH;
            rbLoaiKH.EditValue = objKH.IsCaNhan.GetValueOrDefault();
            ckbNCC.Checked = objKH.isNCC.GetValueOrDefault();
            ckbKH.Checked = objKH.IsKhachHang.GetValueOrDefault();
            txtGhiChu.Text = objKH.GhiChu;

            // Thông tin chuyển từ CSKH qua
            txtSoNha.Text = objKH.SoNha;
            lkTinh.EditValue = objKH.MaTinh;
            lkHuyen.EditValue = objKH.MaHuyen;
            lkXa.EditValue = objKH.MaXa;
            cbNgheNghiepEdit.SetEditValue(objKH.idNgheNghiep);
            lkLoaiHinhKinhDoanh.EditValue = objKH.MaLHKD;
            dateNgayCapMST.EditValue = objKH.MaSoThue_NgayCap;
            txtNoiCapMST.EditValue = objKH.MaSoThue_NoiCap;
            ctlQuyMoEdit1.EditValue = objKH.MaQM;
            spinVonDieuLe.EditValue = objKH.VonDieuLe.GetValueOrDefault();
            txtWebsite.EditValue = objKH.Website;

            lkNguoiLienHe.Properties.DataSource = objKH.NguoiLienHes;
            lkNguoiLienHe.EditValueChanged -= new EventHandler(lkNguoiLienHe_EditValueChanged);
            lkNguoiLienHe.EditValue = objKH.MaNLH;
            NguoiLienHeLoad();
            lkNguoiLienHe.EditValueChanged += lkNguoiLienHe_EditValueChanged;
        }

        void lkNguoiLienHe_EditValueChanged(object sender, EventArgs e)
        {
            NguoiLienHeLoad();
        }

        void NguoiLienHeLoad()
        {
            Library.NguoiLienHe objLH;

            var maNLH = (int?)lkNguoiLienHe.EditValue;

            objLH = db.NguoiLienHes.SingleOrDefault(p => p.ID == maNLH.GetValueOrDefault());

            if (objLH != null)
            {
                txtMaNLH.EditValue = objLH.MaHieu;
                //Tinh, huyen, xa
                var xht = (from t in db.Tinhs
                           join h in db.Huyens on objLH.MaHuyen equals h.MaHuyen into huyen
                           from h in huyen.DefaultIfEmpty()
                           join x in db.Xas on objLH.MaXa equals x.MaXa into xa
                           from x in xa.DefaultIfEmpty()
                           where t.MaTinh == objLH.MaTinh
                           select new { x.TenXa, h.TenHuyen, t.TenTinh }).FirstOrDefault();

            }
            else
            {
                objLH = new Library.NguoiLienHe();
                txtMaNLH.EditValue = db.DinhDang(14, (db.NguoiLienHes.Max(p => (int?)p.ID) ?? 0) + 1);
            }

            lookDanhXungNLH.EditValue = objLH.MaQD;
            txtHoTenNLH.EditValue = objLH.HoTen;
            txtDiDongNLH.EditValue = objLH.DiDong;
            txtDiDongNLH2.EditValue = objLH.DiDongKhac;
            txtDienThoaiNLH.EditValue = objLH.DienThoai;
            txtEmailNLH.EditValue = objLH.Email;
            txtDiaChiNLH.EditValue = objLH.DiaChi;
            dateNgaySinhNLH.EditValue = objLH.NgaySinh;
            txtTenCVNLH.EditValue = objLH.TenCV;
            txtTenPBNLH.EditValue = objLH.TenPB;
            txtDienThoaiCQNLH.EditValue = objLH.DienThoaiCQ;
            txtFaxCQNLH.EditValue = objLH.FaxCQ;
            txtGhiChuNLH.EditValue = objLH.GhiChu;
            txtCMND_NLH.EditValue = objLH.SoCMND;
            dateNgayCap_NLH.EditValue = objLH.NgayCap;
            txtNoiCap_NLH.EditValue = objLH.NoiCap;
        }

        void txtMaKHCN_TextChanged(object sender, EventArgs e)
        {
        }

        private bool CheckRangBuoc()
        {
            bool IsTrue = true;

            if ((bool)rbLoaiKH.EditValue == true)
            {
                if (txtHoKH.Text.Replace(" ", "").Length == 0)
                {
                    DialogBox.Error("Vui lòng nhập họ khách hàng");
                    return false;
                }

                if (txtTenKH.Text.Replace(" ", "").Length == 0)
                {
                    DialogBox.Error("Vui lòng nhập tên khách hàng");
                    return false;
                }

            }

            return IsTrue;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            #region Kiểm tra
            var ltToaNha = Common.TowerList.Select(p => p.MaTN.ToString()).ToList();

            if ((bool?)rbLoaiKH.EditValue == true)
            {
                if (txtTenKH.Text.Trim() == "")
                {
                    DialogBox.Error("Vui lòng nhập [Tên khách hàng], xin cảm ơn.");
                    txtTenKH.Focus();
                    return;
                }

                if (txtHoKH.Text.Trim() == "")
                {
                    DialogBox.Error("Vui lòng nhập [Họ Khách Hàng], xin cảm ơn.");
                    txtHoKH.Focus();
                    return;
                }

                if (txtDiDong.Text.Trim() == "")
                {
                    DialogBox.Error("Vui lòng nhập số [Di Động], xin cảm ơn");
                    txtDiDong.Focus();
                    return;
                }

                if (objKH.IsRoot.GetValueOrDefault() || (IsChinhThuc) || (IsCSKH))
                {

                    if (txtDienThoai.Text.Trim() == "")
                    {
                        DialogBox.Error("Vui lòng nhập số [Điện Thoại], xin cảm ơn");
                        txtDienThoai.Focus();
                        return;
                    }

                    if (txtCMND.Text.Trim() == "")
                    {
                        DialogBox.Error("Vui lòng nhập [CMND/Passport], xin cám ơn");
                        txtCMND.Focus();
                        return;
                    }

                    if (db.tnKhachHangs.Any(o => o.CMND == txtCMND.Text.Trim() & o.MaKH != objKH.MaKH))
                    {
                        DialogBox.Error("[CMND/Passport] đã tồn tại vui lòng nhập lại, xin cám ơn");
                        return;
                    }

                    if (txtNgayCap.Text.Trim() == "")
                    {
                        DialogBox.Error("Vui lòng nhập [Ngày Cấp], xin cảm ơn");
                        txtNgayCap.Focus();
                        return;
                    }

                    if (txtNoiCap.Text.Trim() == "")
                    {
                        DialogBox.Error("Vui lòng nhập [Nơi Cấp], xin cảm ơn");
                        txtNoiCap.Focus();
                        return;
                    }

                    if (txtDCTT.Text.Trim() == "")
                    {
                        DialogBox.Error("Vui lòng nhập [Địa Chỉ Thường Trú], xin cảm ơn");
                        txtDCTT.Focus();
                        return;
                    }

                    if (dateNgaySinh.Text.Trim() == "")
                    {
                        DialogBox.Error("Vui lòng nhập [Ngày Sinh], xin cảm ơn");
                        dateNgaySinh.Focus();
                        return;
                    }

                }
            }
            else
            {
                if (txtCtyTen.Text.Trim() == "")
                {
                    DialogBox.Error("Vui lòng nhập [Tên công ty], xin cảm ơn.");
                    txtCtyTen.Focus();
                    return;
                }

                if (txtCtyTenVT.Text.Trim() == "")
                {
                    DialogBox.Error("Vui lòng nhập [Tên Viết Tắt], xin cảm ơn");
                    txtCtyTenVT.Focus();
                    return;
                }

                if (txtCtyDaiDien.Text.Trim() == "")
                {
                    DialogBox.Error("Vui lòng nhập [Nguời Đại Diện], xin cảm ơn");
                    txtCtyDaiDien.Focus();
                    return;
                }

                if (txtDiaChi.Text.Trim() == "")
                {
                    DialogBox.Error("Vui lòng nhập [Địa Chỉ], xin cảm ơn");
                    txtDiaChi.Focus();
                    return;
                }

                if (txtDienThoai.Text.Trim() == "")
                {
                    DialogBox.Error("Vui lòng nhập [Số Điện Thoại], xin cảm ơn");
                    txtDienThoai.Focus();
                    return;
                }

                if (objKH.IsRoot.GetValueOrDefault() || (IsChinhThuc) || (IsCSKH))
                {

                    if (txtFax.Text.Trim() == "")
                    {
                        DialogBox.Error("Vui lòng nhập số [Fax], xin cảm ơn");
                        txtFax.Focus();
                        return;
                    }



                    if (txtCtyChucVu.Text.Trim() == "")
                    {
                        DialogBox.Error("Vui lòng nhập [Chức Vụ], xin cảm ơn");
                        txtCtyChucVu.Focus();
                        return;
                    }

                    if (txtMaSoThue.Text.Trim() == "")
                    {
                        DialogBox.Error("Vui lòng nhập [Mã Số Thuế], xin cảm ơn");
                        txtMaSoThue.Focus();
                        return;
                    }

                    if (db.tnKhachHangs.Any(o => o.MaSoThue == txtMaSoThue.Text.Trim() & o.MaKH != objKH.MaKH))
                    {
                        DialogBox.Error("[Mã Số Thuế] đã tồn tại vui lòng nhập lại, xin cám ơn");
                        return;
                    }

                    if (txtCtySoDKKD.Text.Trim() == "")
                    {
                        DialogBox.Error("Vui lòng nhập [Số ĐKKD], xin cảm ơn");
                        txtCtySoDKKD.Focus();
                        return;
                    }

                    if (txtCtyNgayDKKD.Text.Trim() == "")
                    {
                        DialogBox.Error("Vui lòng nhập [Ngày ĐKKD], xin cảm ơn");
                        txtCtyNgayDKKD.Focus();
                        return;
                    }

                    if (txtCtyNoiDKKD.Text.Trim() == "")
                    {
                        DialogBox.Error("Vui lòng nhập [Nơi ĐKKD], xin cảm ơn");
                        txtCtyNoiDKKD.Focus();
                        return;
                    }
                    if (ctlQuyMoEdit1.EditValue == null)
                    {
                        DialogBox.Error("Vui lòng chọn [Quy Mô], xin cảm ơn");
                        ctlQuyMoEdit1.Focus();
                        return;
                    }
                    if (spinVonDieuLe.Value == 0)
                    {
                        DialogBox.Error("Vui lòng nhập [Vốn Điều Lệ], xin cảm ơn");
                        spinVonDieuLe.Focus();
                        return;
                    }
                    if (dateNgayCapMST.EditValue == null)
                    {
                        DialogBox.Error("Vui lòng nhập [Ngày Cấp MST], xin cảm ơn");
                        dateNgayCapMST.Focus();
                        return;
                    }

                    if (txtNoiCapMST.Text.Trim() == "")
                    {
                        DialogBox.Error("Vui lòng nhập [Nơi Cấp MST], xin cảm ơn");
                        txtNoiCapMST.Focus();
                        return;
                    }

                    if (txtHoTenNLH.Text.Trim() == "")
                    {
                        DialogBox.Error("Vui lòng nhập [Họ tên Người Liên Hệ], xin cám ơn");
                        txtDiaChiNLH.Focus();
                        return;
                    }

                    if (txtDiaChiNLH.Text.Trim() == "")
                    {
                        DialogBox.Error("Vui lòng nhập [Địa Chỉ Người Liên Hệ], xin cám ơn");
                        txtDiaChiNLH.Focus();
                        return;
                    }

                    if (txtDiDongNLH.Text.Trim() == "")
                    {
                        DialogBox.Error("Vui lòng nhập [Di Động Người Liên Hệ], xin cám ơn");
                        txtDiDongNLH.Focus();
                        return;
                    }
                    if (txtEmailNLH.Text.Trim() == "")
                    {
                        DialogBox.Error("Vui lòng nhập [Email Người Liên Hệ], xin cám ơn");
                        txtEmailNLH.Focus();
                        return;
                    }

                    if (txtTenCVNLH.Text.Trim() == "")
                    {
                        DialogBox.Error("Vui lòng nhập [Chức Danh Người Liên Hệ], xin cám ơn");
                        txtTenCVNLH.Focus();
                        return;
                    }
                    if (dateNgaySinhNLH.Text.Trim() == "")
                    {
                        DialogBox.Error("Vui lòng nhập [Ngày Sinh Người Liên Hệ], xin cám ơn");
                        dateNgaySinhNLH.Focus();
                        return;
                    }
                    if (txtCMND_NLH.Text.Trim() == "")
                    {
                        DialogBox.Error("Vui lòng nhập [CMND/Passport Người Liên Hệ], xin cám ơn");
                        txtCMND_NLH.Focus();
                        return;
                    }
                    if (dateNgayCap_NLH.Text.Trim() == "")
                    {
                        DialogBox.Error("Vui lòng nhập [Ngày cấp Người Liên Hệ], xin cám ơn");
                        dateNgayCap_NLH.Focus();
                        return;
                    }
                }
            }

            //if (tpDoanhNghiep.Visible == true)
            //{
            //    if (ctlQuyMoEdit1.EditValue == null)
            //    {
            //        DialogBox.Error("Vui lòng nhập quy mô");
            //        return;
            //    }

            //    if (spinVonDieuLe.Value == 0)
            //    {
            //        DialogBox.Error("Vui lòng nhập vốn điều lệ");
            //        return;
            //    }

            //}
            

            if (lookKhuVuc.EditValue == null)
            {
                DialogBox.Error("Vui lòng chọn [Khu vực], xin cảm ơn.");
                return;
            }

            if (txtMaKHCN.Text == "" || txtMaKHCN.ToString().Trim() == "")
            {
                DialogBox.Error("Vui lòng nhập [Mã khách hàng], xin cám ơn");
                return;
            }

            if (db.tnKhachHangs.Any(o => o.KyHieu == txtMaKHCN.Text.Trim() & o.MaKH != objKH.MaKH))
            {
                DialogBox.Error("[Mã khách hàng] đã tồn tại vui lòng nhập lại, xin cám ơn");
                return;
            }

            if (lookUpNhomKH.EditValue == null)
            {
                DialogBox.Error("Vui lòng chọn [Nhóm khách hàng]");
                return;
            }

            if (lkLoaiKhachHang.EditValue == null)
            {
                DialogBox.Error("Vui lòng chọn [Loại khách hàng]");
                return;
            }

            if (objKH.MaKH == 0)
            {
                if (lkXa.EditValue == null | txtSoNha.Text.Trim().Length == 0)
                {
                    DialogBox.Error("Vui lòng nhập đầy đủ [Số nhà] - [Tỉnh/Thành Phố]");
                    return;
                }

                if (lkTinh.EditValue == null | lkTinh.Text.Trim().Length == 0)
                {
                    DialogBox.Error("Vui lòng chọn [Tỉnh]");
                    return;
                }

                if (lkHuyen.EditValue == null | lkHuyen.Text.Trim().Length == 0)
                {
                    DialogBox.Error("Vui lòng chọn [Huyện]");
                    return;
                }

                if (lkXa.EditValue == null | lkXa.Text.Trim().Length == 0)
                {
                    DialogBox.Error("Vui lòng chọn [Xã]");
                    return;
                }
            }

            if (ctlNguonKHEdit1.EditValue == null)
            {
                DialogBox.Error("Vui lòng chọn nguồn đến");
                return;
            }

            if (cbNgheNghiepEdit.EditValue == null)
            {
                DialogBox.Error("Vui lòng chọn nghề nghiệp");
                return;
            }

            if (txtEmail.Text.Trim() == "")
            {
                DialogBox.Error("Vui lòng nhập [Email], xin cám ơn");
                txtEmail.Focus();
                return;
            }

            #endregion

            objKH.IsCaNhan = (bool?)rbLoaiKH.EditValue;
            objKH.HoKH = txtHoKH.Text.Trim();
            objKH.TenKH = txtTenKH.Text.Trim();
            objKH.GioiTinh = (bool)rbGioiTinh.EditValue;
            objKH.NgaySinh = String.Compare(dateNgaySinh.Text, "", false) != 0 ? dateNgaySinh.DateTime : (DateTime?)(null);
            objKH.CMND = txtCMND.Text.Trim();
            objKH.NgayCap = txtNgayCap.Text.Trim();
            objKH.NoiCap = txtNoiCap.Text.Trim();
            objKH.DienThoai = txtDienThoai.Text.Trim();
            objKH.Email = txtEmail.Text.Trim();
            objKH.DCTT = txtDCTT.Text.Trim();
            objKH.CtyTen = txtCtyTen.Text.Trim();
            objKH.CtyTenVT = txtCtyTenVT.Text.Trim();
            objKH.DiaChi = txtDiaChi.Text.Trim();
            objKH.Fax = txtFax.Text.Trim();
            objKH.CtyNguoiDD = txtCtyDaiDien.Text.Trim();
            objKH.CtyChucVuDD = txtCtyChucVu.Text.Trim();
            objKH.CtySoDKKD = txtCtySoDKKD.Text.Trim();
            objKH.CtyNgayDKKD = txtCtyNgayDKKD.Text.Trim();
            objKH.CtyNoiDKKD = txtCtyNoiDKKD.Text.Trim();
            //objKH.SoTKNH = txtSoTKNH.Text.Trim();
            //objKH.TenNH = txtTenNH.Text.Trim();
            objKH.MaSoThue = txtMaSoThue.Text.Trim();
            objKH.tnKhuVuc = db.tnKhuVucs.Single(p => p.MaKV == (int)lookKhuVuc.EditValue);
            objKH.MaNguon = (short?)ctlNguonKHEdit1.EditValue;
            objKH.tnNhanVien = db.tnNhanViens.Single(p => p.MaNV == Common.User.MaNV);

            objKH.KyHieu = txtMaKHCN.Text.Trim();
            objKH.MaPhu = txtMaPhu.Text.Trim();
            objKH.QuocTich = txtQuocTich.Text.Trim();
            objKH.MaNKH = (int?)lookUpNhomKH.EditValue;
            objKH.MaLoaiKH = (int?)lkLoaiKhachHang.EditValue;
            objKH.DiDong = txtDiDong.Text;
            objKH.isNCC = ckbNCC.Checked;
            objKH.IsKhachHang = ckbKH.Checked;
            objKH.GhiChu = txtGhiChu.Text;

            // Chuyển từ CSKH
            objKH.SoNha = txtSoNha.Text;
            objKH.MaTinh = (int?)lkTinh.EditValue;
            objKH.MaHuyen = (int?)lkHuyen.EditValue;
            objKH.MaXa = (int?)lkXa.EditValue;
            objKH.idNgheNghiep = Convert.ToString(cbNgheNghiepEdit.EditValue);
            objKH.MaLHKD = (short?)lkLoaiHinhKinhDoanh.EditValue;
            objKH.MaSoThue_NgayCap = (DateTime?)dateNgayCapMST.EditValue;
            objKH.MaSoThue_NoiCap = txtNoiCapMST.Text;
            objKH.MaQM =  (short?)ctlQuyMoEdit1.EditValue;
            objKH.VonDieuLe = spinVonDieuLe.Value;
            objKH.Website = txtWebsite.Text;

            #region Người liên hệ
            Library.NguoiLienHe objLH = null;
            if (txtHoTenNLH.Text.Trim() != "")
            {
                var maNLH = (int?)lkNguoiLienHe.EditValue;

                if (maNLH == null)
                {
                    objLH = new Library.NguoiLienHe();
                    objLH.MaKH = objKH.MaKH;
                    objKH.NguoiLienHes.Add(objLH);
                }
                else
                {
                    objLH = db.NguoiLienHes.Single(p => p.ID == maNLH);
                }

                objLH.MaHieu = txtMaNLH.Text;
                objLH.MaQD = (short?)lookDanhXungNLH.EditValue;
                objLH.HoTen = txtHoTenNLH.Text;
                objLH.DiaChi = txtDiaChiNLH.Text;
                objLH.DiDong = txtDiDongNLH.Text;
                objLH.DiDongKhac = txtDiDongNLH2.Text;
                objLH.DienThoai = txtDienThoaiNLH.Text;
                objLH.Email = txtEmailNLH.Text;
                objLH.NgaySinh = (DateTime?)dateNgaySinhNLH.EditValue;
                objLH.TenCV = txtTenCVNLH.Text;
                objLH.TenPB = txtTenPBNLH.Text;
                objLH.DienThoaiCQ = txtDienThoaiCQNLH.Text;
                objLH.FaxCQ = txtFaxCQNLH.Text;
                objLH.GhiChu = txtGhiChuNLH.Text;
                objLH.SoCMND = txtCMND_NLH.Text;
                objLH.NgayCap = (DateTime?)dateNgayCap_NLH.EditValue;
                objLH.NoiCap = txtNoiCap_NLH.Text;
                objLH.MaNVN = Common.User.MaNV;
                objLH.NgayNhap = DateTime.Now;
            }
            #endregion
            db.SubmitChanges();

            if (objLH != null)
            {
                using (var dba = new MasterDataContext())
                {
                    var obj = dba.tnKhachHangs.Single(p => p.MaKH == objKH.MaKH);
                    obj.MaNLH = objLH.ID;
                    dba.SubmitChanges();
                }
            }

            try
            {
                db.SubmitChanges();
                //InvoiceCls.DongBoKH(objKH.MaKH);
                this.DialogResult = DialogResult.OK;
                this.Close();
                DialogBox.Alert("Lưu khách hàng thành công!");
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
        }

        void SetDiaChi()
        {
            if (!ckbUpdateDiaChi.Checked)
                return;

            List<string> ltDiaChi = new List<string>();

            if (txtSoNha.Text.Trim() != "")
                ltDiaChi.Add(txtSoNha.Text);

            if (lkXa.EditValue != null)
            {
                var xa = db.Xas.Single(o => o.MaXa == (int?)lkXa.EditValue);
                if (xa != null)
                    ltDiaChi.Add(xa.TenHienThi);
            }

            if (lkHuyen.EditValue != null)
            {
                var huyen = db.Huyens.Single(o => o.MaHuyen == (int?)lkHuyen.EditValue);
                if (huyen != null)
                    ltDiaChi.Add(huyen.TenHienThi);
            }

            if (lkTinh.EditValue != null)
            {
                var tinh = db.Tinhs.SingleOrDefault(o => o.MaTinh == (int?)lkTinh.EditValue);
                if (tinh != null)
                    ltDiaChi.Add(tinh.TenHienThi);
            }

            txtDiaChi.Text = string.Join(", ", ltDiaChi.ToArray());
        }


        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void rbLoaiKH_SelectedIndexChanged(object sender, EventArgs e)
        {
            var isCaNhan = ((bool?)rbLoaiKH.EditValue).GetValueOrDefault();

            tpCaNhan.PageVisible = isCaNhan;
            tpDoanhNghiep.PageVisible = !isCaNhan;

            var show = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            var hide = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

            layoutHoKH.Visibility = isCaNhan ? show : hide;
            layoutTenKH.Visibility = isCaNhan ? show : hide;
            layoutCMND.Visibility = isCaNhan ? show : hide;
            layoutNgayCap.Visibility = isCaNhan ? show : hide;
            layoutNoiCap.Visibility = isCaNhan ? show : hide;

            layoutCtyTenVT.Visibility = !isCaNhan ? show : hide;
            layoutTenCty.Visibility = !isCaNhan ? show : hide;
        }

        void SetCheck()
        {
            ckbKH.CheckedChanged -= ckbKH_CheckedChanged;
            ckbNCC.CheckedChanged -= ckbNCC_CheckedChanged;

            if (!ckbKH.Checked & !ckbNCC.Checked)
                ckbKH.Checked = true;

            ckbKH.CheckedChanged += ckbKH_CheckedChanged;
            ckbNCC.CheckedChanged += ckbNCC_CheckedChanged;
        }

        private void ckbKH_CheckedChanged(object sender, EventArgs e)
        {
            SetCheck();
        }

        private void ckbNCC_CheckedChanged(object sender, EventArgs e)
        {
            SetCheck();
        }

        private void lookUpNhomKH_EditValueChanged(object sender, EventArgs e)
        {
            switch ((int?)lookUpNhomKH.EditValue)
            {
                case 65:
                    txtMaSoThue.Properties.Mask.EditMask = "[0-9A-Za-z]{0,25}";
                    break;
                default:
                    txtMaSoThue.Properties.Mask.EditMask = "[0-9-]{0,20}";
                    break;
            }
        }

        private void lookHuyen_EditValueChanged(object sender, EventArgs e)
        {
            XaLoad();
            SetDiaChi();
        }

        private void lookTinh_EditValueChanged(object sender, EventArgs e)
        {
            HuyenLoad();
            SetDiaChi();
        }

        private void HuyenLoad()
        {
            int? maTinh = (int?)lkTinh.EditValue;
            if (maTinh.HasValue)
            {
                using (MasterDataContext db = new MasterDataContext())
                {
                    lkHuyen.Properties.DataSource = (from p in db.Huyens
                                                       where p.MaTinh == maTinh
                                                       orderby p.TenHuyen
                                                       select p).ToList<Huyen>();
                }
            }
            lkHuyen.EditValue = null;
        }

        private void XaLoad()
        {
            int? maHuyen = (int?)lkHuyen.EditValue;
            if (maHuyen.HasValue)
            {
                using (MasterDataContext db = new MasterDataContext())
                {
                    lkXa.Properties.DataSource = (from p in db.Xas
                                                    where p.MaHuyen == maHuyen
                                                    orderby p.TenXa
                                                    select p).ToList();
                }
            }
            lkXa.EditValue = null;
        }

        private void lkXa_EditValueChanged(object sender, EventArgs e)
        {
            SetDiaChi();
        }

        private void txtSoNha_EditValueChanged(object sender, EventArgs e)
        {
            SetDiaChi();
        }

    }
}