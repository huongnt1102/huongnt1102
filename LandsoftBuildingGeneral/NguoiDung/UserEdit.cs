using System;
using System.Windows.Forms;
using Library;
using System.Linq;

namespace LandsoftBuildingGeneral.NguoiDung
{
    public partial class UserEdit : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public UserEdit()
        {
            InitializeComponent();
            db = new MasterDataContext();
            TranslateLanguage.TranslateControl(this);
        }

        public tnNhanVien objNhanVien { get; set; }
        public byte? MaTN { get; set; }

        private void UserEdit_Load(object sender, EventArgs e)
        {
            BindDataToComboxBoxGroup();
            if (objNhanVien != null)
            {
                txtloginid.Text = objNhanVien.MaSoNV;
                groupControlUser.Text = objNhanVien.HoTenNV;
                lkgroup.EditValue = objNhanVien.MaPB ?? 0;
                txtfullname.Text = objNhanVien.HoTenNV;
                txtDiaChi.Text = objNhanVien.DiaChi;
                txtDienThoai.Text = objNhanVien.DienThoai;
                txtemail.Text = objNhanVien.Email;
                lookchucvu.EditValue = objNhanVien.MaCV;
                ckToanQuyen.Checked = objNhanVien.IsSuperAdmin.Value;
                ckIsLock.Checked = objNhanVien.IsLocked ?? false;
                dateNgaySinh.DateTime = objNhanVien.NgaySinh ?? DateTime.Now.AddYears(-18);

                // Kiểm tra điện thoại đã đăng ký dùng app hay chưa, nếu đã đăng ký thì không cho sửa nữa
                var kiem_tra_dung_app = db.app_Residents.FirstOrDefault(_ => _.Phone == objNhanVien.DienThoai);
                if (kiem_tra_dung_app != null)
                {
                    txtDienThoai.ReadOnly = true;
                }
            }
            else
            {
                groupControlUser.Text = "Thêm người sử dụng mới";
                ckToanQuyen.Checked = false;
                ckIsLock.Checked = false;
            }
        }

        void BindDataToComboxBoxGroup()
        {
            lkgroup.Properties.DataSource = db.tnPhongBans.Where(_=>_.MaTN == MaTN);
            lkgroup.Properties.DisplayMember = "TenPB";
            lkgroup.Properties.ValueMember = "MaPB";
            lookchucvu.Properties.DataSource = db.tnChucVus;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (txtemail.Text.Trim().Length == 0 || txtfullname.Text.Trim().Length == 0
                || txtloginid.Text.Trim().Length == 0)
            {
                Library.DialogBox.Error("Các thông tin không không được phép để trống");
                return;
            }

            //if (!AuthorizationClass.Common.CommonFunction.CheckEmailAddress(txtemail.Text.Trim()))
            //{
            //    Library.DialogBox.Error("Địa chỉ email không đúng định dạng!");
            //    return;
            //}
            if (objNhanVien == null)
            {
                var objKT = db.tnNhanViens.FirstOrDefault(p => p.MaSoNV.Equals(txtloginid.Text.Trim()));
                if (objKT != null)
                {
                    DialogBox.Error("Tài khoản đã được sử dụng.Vui lòng chọn tài khoản khác");
                    txtloginid.Focus();
                    return;
                }
            }
            else
            {
                var objKT = db.tnNhanViens.FirstOrDefault(p => p.MaSoNV.Equals(txtloginid.Text.Trim()) && p.MaNV!=objNhanVien.MaNV);
                if (objKT != null)
                {
                    DialogBox.Error("Tài khoản đã được sử dụng.Vui lòng chọn tài khoản khác");
                    txtloginid.Focus();
                    return;
                }
            }
            if (lkgroup.EditValue == null)
            {
                Library.DialogBox.Error("Chọn phòng ban!");
                return;
            }

            if (lookchucvu.EditValue == null)
            {
                Library.DialogBox.Error("Chọn chức vụ của nhân viên!");
                return;
            }

            if (String.Compare(txtpass.Text.Trim(), txtpass2.Text.Trim(), false) != 0)
            {
                Library.DialogBox.Error("Mật khẩu gõ lại không trùng khớp");
                return;
            }

            if (dateNgaySinh.EditValue == null)
            {
                Library.DialogBox.Error("Vui lòng nhập ngày tháng năm sinh nhân viên");
                return;
            }

            Library.HeThongCls.UserLogin usrlogin = new Library.HeThongCls.UserLogin();
            try
            {
                if (objNhanVien == null)
                {
                    objNhanVien = new tnNhanVien()
                    {
                        MaSoNV = txtloginid.Text.Trim(),
                        MatKhau = usrlogin.HashPassword(txtpass.Text.Trim()),
                        Email = txtemail.Text.Trim(),
                        DiaChi = txtDiaChi.Text.Trim(),
                        DienThoai = txtDienThoai.Text.Trim(),
                        MaPB = (int)lkgroup.EditValue,
                        HoTenNV = txtfullname.Text.Trim(),
                        MaTN = this.MaTN,
                        IsSuperAdmin = ckToanQuyen.Checked,
                        MaCV = (int)lookchucvu.EditValue,
                        IsLocked = ckIsLock.Checked,
                        NgaySinh = dateNgaySinh.DateTime
                    };
                    db.tnNhanViens.InsertOnSubmit(objNhanVien);
                }
                else
                {
                    objNhanVien = db.tnNhanViens.Single(p => p.MaNV == objNhanVien.MaNV);
                    if (txtpass.Text.Trim() != "" & txtpass2.Text.Trim() != "")
                    {
                        objNhanVien.MatKhau = usrlogin.HashPassword(txtpass.Text.Trim());
                    }
                    objNhanVien.MaSoNV = txtloginid.Text.Trim();
                    objNhanVien.Email = txtemail.Text.Trim();
                    objNhanVien.DiaChi = txtDiaChi.Text.Trim();
                    objNhanVien.DienThoai = txtDienThoai.Text.Trim();
                    objNhanVien.MaPB = (int)lkgroup.EditValue;
                    objNhanVien.HoTenNV = txtfullname.Text.Trim();
                    objNhanVien.MaTN = this.MaTN;
                    objNhanVien.IsSuperAdmin = ckToanQuyen.Checked;
                    objNhanVien.SLThongBao = 0;
                    objNhanVien.MaCV = (int)lookchucvu.EditValue;
                    objNhanVien.IsLocked = ckIsLock.Checked;
                    objNhanVien.NgaySinh = dateNgaySinh.DateTime;
                }

                db.SubmitChanges();

                var model = new { matn = objNhanVien.MaTN, manv = objNhanVien.MaNV };
                var param = new Dapper.DynamicParameters();
                param.AddDynamicParams(model);
                Library.Class.Connect.QueryConnect.Query<bool>("tntoanhanguoidung_edit", param);

                DialogBox.Alert("Dữ liệu đã được cập nhật.");
                this.DialogResult = DialogResult.OK;
                this.Close();

            }
            catch (Exception ex)
            {
                if (ex.Message == "Violation of UNIQUE KEY constraint 'IX_tnNhanVien'. Cannot insert duplicate key in object 'dbo.tnNhanVien'.\r\nThe statement has been terminated.")
                {
                    DialogBox.Alert("Tài khoản đã tồn tại. Vui lòng kiểm tra lại, xin cảm ơn.");
                    txtloginid.Focus();
                    objNhanVien = null;
                    return;
                }
            }
        }

        void CheckData()
        {
            if (txtemail.Text.Trim().Length == 0 || txtfullname.Text.Trim().Length == 0
                || txtloginid.Text.Trim().Length == 0 || txtpass.Text.Trim().Length == 0
                || txtpass2.Text.Trim().Length == 0)
            {
                Library.DialogBox.Error("Các thông tin không không được phép để trống");
                return;
            }

            if (String.Compare(txtpass.Text.Trim(), txtpass2.Text.Trim(), false) != 0)
            {
                Library.DialogBox.Error("Mật khẩu gõ lại không trùng khớp");
                return;
            }

            if (!AuthorizationClass.Common.CommonFunction.CheckEmailAddress(txtemail.Text.Trim()))
            {
                Library.DialogBox.Error("Địa chỉ email không đúng định dạng!");
                return;
            }

            if (lkgroup.EditValue == null)
            {
                Library.DialogBox.Error("Chọn phòng ban!");
                return;
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}