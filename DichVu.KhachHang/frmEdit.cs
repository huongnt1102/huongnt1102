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
using DevExpress.DataProcessing;

namespace DichVu.KhachHang
{
    public partial class frmEdit : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db = new MasterDataContext();
        public tnNhanVien objnv;
        public tnKhachHang objKH;
        public byte maTN;
        public bool IsSua = false;
        public bool IsNCC = false;

        System.Collections.Generic.List<Library.Class.HuongDan.ShowAuto.ControlItem> controls;

        public frmEdit()
        {
            InitializeComponent();
            TranslateLanguage.TranslateControl(this);
        }

        string getNewMaKH()
        {
            if (maTN != 1)
            {
               return  Common.GetCustomerCode((byte)1);
            }
            else
            {
                string MaKH = "";
                db.khKhachHang_getNewMaKH(ref MaKH);
                var tn = db.tnToaNhas.FirstOrDefault(o => o.MaTN == this.maTN);
                return tn.TenVT + db.DinhDang(7, int.Parse(MaKH));
            }
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

        private void LoadNgheNghiep()
        {
            var check = (from nn in db.NgheNghieps
                         join kh in db.NgheNghiepKHs on nn.MaNN equals kh.MaNN
                         where kh.MaKH == objKH.MaKH
                         select new
                         {
                             nn.MaNN
                         }).ToList();
            string[] array1 = (from A in check select A.MaNN.ToString()).ToArray();
            char separator1 = check1.Properties.SeparatorChar;
            string result1 = string.Empty;
            foreach (var element in array1)
            {
                result1 += element + separator1;
            }
            check1.SetEditValue(result1);
        }
        private void frmEdit_Load(object sender, EventArgs e)
        {
            controls = Library.Class.HuongDan.ShowAuto.GetControlItems(this.Controls);
            Library.Class.HuongDan.ShowAuto.ClearText(this.Controls);

            lookUpNhomKH.Properties.DataSource = db.khNhomKhachHangs.Where(p => p.MaTN == maTN);
            lkLoaiKhachHang.Properties.DataSource = db.khLoaiKhachHangs.Where(p => p.MaTN == maTN).OrderBy(k=>k.TenLoaiKH);
            //lookTinh.Properties.DataSource = db.tnKhuVucs;
            check1.Properties.DataSource = db.NgheNghieps.ToList();
            txt_ten_dang_nhap.Text = "";
            txt_mat_khau.Text = "";
            txt_nhap_lai_mat_khau.Text = "";

            glkQuocGia.Properties.DataSource = (from p in db.QuocTiches
                                                select new
                                                {
                                                    Id = p.ID,
                                                    Name = p.TenVT,
                                                    Ten = p.TenQuocTich
                                                });

            if (objKH != null)
            {
                objKH = db.tnKhachHangs.Single(p => p.MaKH == objKH.MaKH);
                txtMaKHCN.Text = objKH.KyHieu;
                txtHoKH.EditValue = objKH.HoKH;
                txtTenKH.EditValue = objKH.TenKH;
                chkGioiTinh.Checked = objKH.GioiTinh.GetValueOrDefault();
                chkGioiTinhNu.Checked = !objKH.GioiTinh.GetValueOrDefault();
                //rdbGioiTinh.EditValue = objKH.GioiTinh;
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
                //lookTinh.EditValue = objKH.MaKV;
                txtMaSoThue.Text = objKH.CtyMaSoThue;
                this.IsCaNhan = objKH.IsCaNhan.Value;
                //txtMaKHCN.Properties.ReadOnly = true;
                txtTKNganHang.Text = objKH.TaiKhoanNganHang;
                txtQuocTich.Text = objKH.QuocTich;

                if(objKH.MaQT != null)
                {
                    glkQuocGia.EditValue = objKH.MaQT;
                }

                if(objKH.MaTinh != null)
                {
                    glkTinh.EditValue = objKH.MaTinh;
                }

                if(objKH.MaHuyen != null)
                {
                    glkHuyen.EditValue = objKH.MaHuyen;
                }

                txtMaPhu.Text = objKH.MaPhu;
                lookUpNhomKH.EditValue = objKH.MaNKH;
                lkLoaiKhachHang.EditValue = objKH.MaLoaiKH;
                txtNguoiDaiDien.Text = objKH.NguoiDongSoHuu;
                txtzalosms.Text = objKH.smsZalo;
                txtZaloName.Text = objKH.nameZalo;
                txtWebsite.Text = objKH.Website;
                this.IsNCC = objKH.isNCC != null ? (bool)objKH.isNCC : false;
                memoEdit1.Text = objKH.DiaChiNhanThu;
                txt_ten_dang_nhap.Text = objKH.TenDangNhap != null ? objKH.TenDangNhap : "";
                txtEmailKhachThue.Text = objKH.EmailKhachThue;
                txtDiaPhan.Text = objKH.DiaPhan;
                if (IsNCC)
                {
                    tabKhachHang.SelectedTabPageIndex = 2;
                    txtEmailNCC.Text = objKH.EmailKH;
                    txtTenNCC.Text = objKH.CtyTen;
                    txtTKNganHangNCC.Text = objKH.CtySoTKNH;
                    txtTenTKNganHangNCC.Text = objKH.CtyTenNH;
                    txtMaSoThueNCC.Text = objKH.CtyMaSoThue;
                    txtGhiChuNCC.Text = objKH.GhiChu;
                    txtFaxNCC.Text = objKH.CtyFax;
                }
                LoadNgheNghiep();

                // Kiểm tra điện thoại đã đăng ký dùng app hay chưa, nếu đã đăng ký thì không cho sửa nữa
                var kiem_tra_dung_app = db.app_Residents.FirstOrDefault(_ => _.Phone == objKH.DienThoaiKH);
                if (kiem_tra_dung_app != null)
                {
                    txtDienThoai.ReadOnly = true;
                }

                txtReference.Text = objKH.Reference;
            }
            else
            {
                if (IsNCC)
                    tabKhachHang.SelectedTabPageIndex = 2;
                objKH = new tnKhachHang();
                objKH.MaTN = this.maTN;
                db.tnKhachHangs.InsertOnSubmit(objKH);

                txtMaKHCN.Text = getNewMaKH();

                txtMaKHCN.Properties.ReadOnly = false;

                txtMaKHCN.TextChanged += txtMaKHCN_TextChanged;
                chkGioiTinh.Checked = true;
                
            }

            
        }

        void txtMaKHCN_TextChanged(object sender, EventArgs e)
        {
            //if (txtMaKHCN.Text.Trim() == "")
            //{
            //    lblThongBao.Text = "";
            //    return;
            //}

            //var check = db.tnKhachHangs.Where(p => p.KyHieu.Trim() == txtMaKHCN.Text.Trim()).ToList();
            //if (check.Count > 0)
            //{
            //    lblThongBao.Text = "Mã khách hàng này đã tồn tại";
            //}
            //else
            //{
            //    lblThongBao.Text = "";
            //}
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {

           
            try
            {
                #region Kiểm tra
                // Kiểm tra cmnd
                //if (tabKhachHang.SelectedTabPageIndex == 0 || tabKhachHang.SelectedTabPageIndex == 1)///kiểm tra nếu là KH thường
                //{
                //    if (this.IsCaNhan)
                //    {
                //        if (txtCMND.Text.Trim() == "")
                //        {
                //            DialogBox.Error("Vui lòng nhập [CMND/Passport], xin cảm ơn.");
                //            txtCMND.Focus();
                //            return;
                //        }
                //    }
                //}




                var ltToaNha = Common.TowerList.Select(p => p.MaTN.ToString()).ToList();
                var ltMaKH = db.tnKhachHangs.Where(p => p.MaKH != objKH.MaKH && ltToaNha.Contains(p.MaTN.ToString())).Select(p => p.KyHieu).ToList();
                var ltMaPhu = db.tnKhachHangs.Where(p => p.MaKH != objKH.MaKH && ltToaNha.Contains(p.MaTN.ToString())).Select(p => p.MaPhu).ToList();

                // kiểm tra trùng tên đăng nhập
                if (txt_ten_dang_nhap.Text != "")
                {
                    var tenDangNhapKhachHang = db.tnKhachHangs.FirstOrDefault(_ => _.MaKH != objKH.MaKH & _.TenDangNhap == txt_ten_dang_nhap.Text.Trim() && ltToaNha.Contains(_.MaTN.ToString()));
                    if (tenDangNhapKhachHang != null)
                    {
                        Library.DialogBox.Error("Số điện thoại đã có, vui lòng đăng ký số khác");
                        txt_ten_dang_nhap.Focus();
                        return;
                    }
                }

                bool IsLuuVaoCacDuAn = true;

                // Kiểm tra trùng CMND
                if (tabKhachHang.SelectedTabPageIndex == 0 || tabKhachHang.SelectedTabPageIndex == 1)///kiểm tra nếu là KH thường
                {
                    //if (this.IsCaNhan)
                    //{
                    if (txtCMND.Text != "" || txtMaSoThue.Text != "")
                    {
                        var checkCMND = db.tnKhachHangs.Where(_ => _.MaKH != objKH.MaKH
                                                                & ((_.CMND == txtCMND.Text.Trim() & txtCMND.Text != "")
                                                                || (_.CtyMaSoThue == txtMaSoThue.Text.Trim() & txtMaSoThue.Text != ""))
                                                                & _.MaKH != objKH.MaKH
                                                                && ltToaNha.Contains(_.MaTN.ToString()))
                                                                .ToList();
                        if (checkCMND.Count() > 0)
                        {
                            Library.DialogBox.Error("[CMND/Passport/Mã số thuế] này đang tồn tại ở nhiều dự án khác, bạn cần chọn Đồng bộ hoặc Lưu vào dự án này");
                            txtCMND.Focus();

                            DichVu.KhachHang.PopupKhachHang.frmCheckMaKhachHang frm = new PopupKhachHang.frmCheckMaKhachHang();
                            frm.KyHieu = this.IsCaNhan ? txtCMND.Text.Trim() : txtMaSoThue.Text.Trim();
                            frm.MaTN = maTN;

                            frm.ShowDialog();
                            if (frm.IsLuuVaoDuAnMoi == true)
                            {
                                IsLuuVaoCacDuAn = false;
                                this.Close();
                                goto Thoat;
                            }
                            else
                            {
                                IsLuuVaoCacDuAn = true;
                            }

                            var khachHangChon = db.tnKhachHangs.FirstOrDefault(_ => _.MaKH == frm.MaKH);
                            if (khachHangChon != null)
                            {
                                #region Thay thế các thông tin bằng khách hàng mới
                                txtCMND.Text = khachHangChon.CMND;
                                txtCtyChucVu.Text = khachHangChon.CtyTen;
                                txt_ten_dang_nhap.Text = khachHangChon.TenDangNhap;
                                txtTenKH.Text = khachHangChon.TenKH;
                                txtCtyTen.Text = khachHangChon.CtyTen;
                                // lookTinh.EditValue = khachHangChon.MaKV;
                                txtDienThoai.Text = khachHangChon.DienThoaiKH;
                                txtMaPhu.Text = khachHangChon.MaPhu;
                                chkGioiTinh.Checked = khachHangChon.GioiTinh.GetValueOrDefault();
                                txtEmail.Text = khachHangChon.EmailKH;
                                txtDCTT.Text = khachHangChon.DCTT;
                                txtDCLL.Text = khachHangChon.DCLL;
                                txtCtyTenVT.Text = khachHangChon.CtyTenVT;
                                txtMaKHCN.Text = khachHangChon.KyHieu;
                                txtMaPhu.Text = khachHangChon.MaPhu;
                                lookUpNhomKH.EditValue = khachHangChon.MaNKH;
                                lkLoaiKhachHang.EditValue = khachHangChon.MaLoaiKH;
                                txtMaSoThue.Text = khachHangChon.CtyMaSoThue;
                                #endregion
                                return;
                            }



                            //return;
                        }
                    }
                    //}
                }
                

                if (tabKhachHang.SelectedTabPageIndex == 0 || tabKhachHang.SelectedTabPageIndex == 1)///kiểm tra nếu là KH thường
                {
                    if (this.IsCaNhan)
                    {
                        if (txtTenKH.Text.Trim() == "")
                        {
                            DialogBox.Error("Vui lòng nhập [Tên khách hàng], xin cảm ơn.");
                            txtTenKH.Focus();
                            return;
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
                            return;
                        }
                    }
                }

                if (glkTinh.EditValue == null)
                {
                    DialogBox.Error("Vui lòng chọn [Khu vực], xin cảm ơn.");
                    return;
                }

                if (txtMaKHCN.Text == "" || txtMaKHCN.ToString().Trim() == "")
                {
                    DialogBox.Error("Vui lòng nhập [Mã khách hàng], xin cám ơn");
                    return;
                }

                if (txtDienThoai.Text == "" || txtDienThoai.ToString().Trim() == "")
                {
                    DialogBox.Error("Vui lòng nhập [Điện thoại], xin cám ơn");
                    return;
                }

                var idKh = objKH == null ? 0 : objKH.MaKH;

                if (ltMaKH.Contains(txtMaKHCN.Text.Trim()) & idKh == 0)
                {
                    //DialogBox.Error("Mã khách hàng đã tồn tại vui lòng nhập lại, xin cám ơn");
                    //return;
                    txtMaKHCN.Text = getNewMaKH();



                }

                if (txtMaPhu.Text.Trim() != "" && ltMaPhu.Contains(txtMaPhu.Text.Trim()) & objKH == null)
                {
                    DialogBox.Error("Mã phụ đã tồn tại vui lòng nhập lại, xin cám ơn");
                    return;
                }
                if (lookUpNhomKH.EditValue == null)
                {
                    DialogBox.Error("Vui lòng chọn nhóm khách hàng");
                    return;
                }
                if (String.CompareOrdinal(txt_mat_khau.Text.Trim(), txt_nhap_lai_mat_khau.Text.Trim()) != 0)
                {
                    Library.DialogBox.Error("Mật khẩu gõ lại không trùng khớp");
                    return;
                }

                if (glkQuocGia.EditValue == null)
                {
                    Library.DialogBox.Error("Vui lòng chọn quốc tịch");
                    return;
                }

                if (glkHuyen.EditValue == null)
                {
                    Library.DialogBox.Error("Vui lòng chọn huyện");
                    return;
                }

                #endregion
                IsNCC = tabKhachHang.SelectedTabPageIndex == 2;
                if (IsSua == true & objKH != null)
                {
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
                objKH.IsCaNhan = this.IsCaNhan;
                objKH.HoKH = txtHoKH.Text.Trim();
                objKH.TenKH = txtTenKH.Text.Trim();
                objKH.GioiTinh = chkGioiTinh.Checked;
                objKH.NgaySinh = String.Compare(dateNgaySinh.Text, "", false) != 0 ? dateNgaySinh.DateTime : (DateTime?)(null);
                objKH.CMND = txtCMND.Text.Trim();
                objKH.NgayCap = txtNgayCap.Text.Trim();
                objKH.NoiCap = txtNoiCap.Text.Trim();
                objKH.NguoiLH = txtTenNHL.Text;
                objKH.SDTNguoiLH = txtSDTNLH.Text;
                objKH.DiaChiNhanThu = memoEdit1.Text.Trim();
                objKH.DienThoaiKH = txtDienThoai.Text.Trim();
                objKH.EmailKH = txtEmail.Text.Trim();
                objKH.DCTT = txtDCTT.Text.Trim();
                objKH.DCLL = txtDCLL.Text.Trim();
                objKH.Website = txtWebsite.Text.Trim();
                objKH.CtyTen = IsNCC ? txtTenNCC.Text.Trim() : txtCtyTen.Text.Trim();//////NCC
                objKH.CtyTenVT = txtCtyTenVT.Text.Trim();
                //objKH.CtyDiaChi = IsNCC ? txtDiaChiNCC.Text.Trim() : txtCtyDiaChi.Text.Trim();
                //objKH.CtyDienThoai = IsNCC ? txtDienThoaiNCC.Text.Trim() : txtCtyDienThoai.Text.Trim();
                objKH.CtyFax = IsNCC ? txtFaxNCC.Text.Trim() : txtCtyFax.Text.Trim();
                objKH.nameZalo = txtZaloName.Text;
                if (txtzalosms.Text != "")
                {
                    objKH.smsZalo = txtzalosms.Text;
                    objKH.issmsZalo = true;
                }
                else
                {
                    objKH.smsZalo = txtzalosms.Text;
                    objKH.issmsZalo = false;
                }



                objKH.CtyNguoiDD = txtCtyDaiDien.Text.Trim();
                objKH.CtyChucVuDD = txtCtyChucVu.Text.Trim();
                objKH.CtySoDKKD = txtCtySoDKKD.Text.Trim();
                objKH.CtyNgayDKKD = txtCtyNgayDKKD.Text.Trim();
                objKH.CtyNoiDKKD = txtCtyNoiDKKD.Text.Trim();
                objKH.CtySoTKNH = IsNCC ? txtTKNganHangNCC.Text.Trim() : txtCtySoTKNH.Text.Trim();
                objKH.CtyTenNH = IsNCC ? txtTenTKNganHangNCC.Text.Trim() : txtCtyTenNH.Text.Trim();
                objKH.CtyMaSoThue = IsNCC ? txtMaSoThueNCC.Text.Trim() : txtMaSoThue.Text.Trim();
                //objKH.tnKhuVuc = db.tnKhuVucs.Single(p => p.MaKV == (int)lookTinh.EditValue);
                objKH.tnNhanVien = db.tnNhanViens.Single(p => p.MaNV == objnv.MaNV);
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
                objKH.EmailKhachThue = txtEmailKhachThue.Text;
                objKH.DiaPhan = txtDiaPhan.Text;
                objKH.Reference = txtReference.Text;

                objKH.MaQT = Convert.ToInt32(glkQuocGia.EditValue);
                objKH.MaHuyen = Convert.ToInt32(glkHuyen.EditValue);
                objKH.MaTinh = Convert.ToInt32(glkTinh.EditValue);

                db.SubmitChanges();
                char separator1 = check1.Properties.SeparatorChar;
                string result1 = string.Empty;
                for (int i = 0; i < check1.Properties.Items.GetCheckedValues().Count(); i++)
                {
                    var check = db.NgheNghiepKHs.SingleOrDefault(p => p.MaKH == objKH.MaKH & p.MaNN == (int?)check1.Properties.Items.GetCheckedValues()[i]);
                    if (check == null)
                    {
                        NgheNghiepKH nn = new NgheNghiepKH();
                        nn.MaKH = objKH.MaKH;
                        nn.MaNN = (int?)check1.Properties.Items.GetCheckedValues()[i];
                        db.NgheNghiepKHs.InsertOnSubmit(nn);
                        db.SubmitChanges();
                    }
                    var ngheNghiep = db.NgheNghieps.FirstOrDefault(_ => _.MaNN == (int?)check1.Properties.Items.GetCheckedValues()[i]);
                    if(ngheNghiep != null)
                    {
                        result1 += ngheNghiep.TenNN + separator1;
                    }

                    
                }
                objKH.NganhNgheDoanhNghiep = result1;

                db.SubmitChanges();
                if (objKH != null)
                {
                    for (int i = 0; i < check1.Properties.Items.Count; i++)
                    {
                        if (check1.Properties.Items[i].CheckState == System.Windows.Forms.CheckState.Unchecked)
                        {
                            var xoa = db.NgheNghiepKHs.SingleOrDefault(p => p.MaNN == (int?)check1.Properties.Items[i].Value && p.MaKH == objKH.MaKH);
                            if (xoa != null)
                            {
                                db.NgheNghiepKHs.DeleteOnSubmit(xoa);
                                db.SubmitChanges();
                            }
                        }
                    }
                }

            

            Thoat:

                if (IsLuuVaoCacDuAn == true)
                {
                    Library.Class.Connect.QueryConnect.QueryData<bool>("tnKhachHang_Copy", new
                    {
                        MaTN = maTN,
                        MaKH = objKH.MaKH
                    });
                }

                

                this.DialogResult = DialogResult.OK;
                this.Close();
                DialogBox.Alert("Lưu khách hàng thành công!");
            }
            catch(Exception ex)
            {
                DialogBox.Error(ex.Message);
            }


        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
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

        private void labelControl2_Click(object sender, EventArgs e)
        {

        }

        private void txtTenKH_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void txtCtyNgayDKKD_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void labelControl18_Click(object sender, EventArgs e)
        {

        }

        private void txtCtySoDKKD_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void labelControl19_Click(object sender, EventArgs e)
        {

        }

        private void labelControl20_Click(object sender, EventArgs e)
        {

        }

        private void txtCtyNoiDKKD_EditValueChanged(object sender, EventArgs e)
        {

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
        
        private void itemHuongDan_Click(object sender, EventArgs e)
        {
            Library.Class.HuongDan.ShowAuto.ActiveDemo(true, controls);
        }

        private void frmEdit_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Library.Class.HuongDan.ShowAuto.IsActive = false;
            }
        }

        private void itemClearText_Click(object sender, EventArgs e)
        {
            Library.Class.HuongDan.ShowAuto.ClearText(this.Controls);
            txtMaKHCN.Text = getNewMaKH();
        }

        private void check1_EditValueChanged(object sender, EventArgs e)
        {
          
           
        }

        private void glkQuocGia_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                var item = sender as GridLookUpEdit;
                if (item.EditValue == null)
                {
                    DialogBox.Alert("Vui lòng chọn Quốc gia");
                    return;
                }

                glkTinh.Properties.DataSource = (from t in db.Tinhs
                                                 where t.MaQG == Convert.ToInt32(item.EditValue)
                                                 select new
                                                 {
                                                     MaTinh = t.MaTinh,
                                                     TenTinh = t.TenTinh,
                                                     Code = t.Code
                                                 }).ToList();
            }
            catch (Exception)
            {
                //throw;
            }
        }

        private void lookTinh_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                var item = sender as LookUpEdit;
                if(item.EditValue == null)
                {
                    DialogBox.Alert("Vui lòng chọn tỉnh/ thành phố");
                    return;
                }

                glkHuyen.Properties.DataSource = (from h in db.Huyens
                                                  where h.MaTinh == Convert.ToInt32(item.EditValue)
                                                  select new
                                                  {
                                                      Id = h.MaHuyen,
                                                      Name = h.TenHuyen,
                                                      Code = h.Code
                                                  }).ToList();
            }
            catch { }
        }

        private void glkTinh_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                var item = sender as GridLookUpEdit;
                if (item.EditValue == null)
                {
                    DialogBox.Alert("Vui lòng chọn tỉnh/ thành phố");
                    return;
                }

                glkHuyen.Properties.DataSource = (from h in db.Huyens
                                                  where h.MaTinh == Convert.ToInt32(item.EditValue)
                                                  select new
                                                  {
                                                      Id = h.MaHuyen,
                                                      Name = h.TenHuyen,
                                                      Code = h.Code
                                                  }).ToList();
            }
            catch (Exception)
            {
                //throw;
            }
        }
    }
}