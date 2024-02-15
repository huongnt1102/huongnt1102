using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Library;

namespace LandsoftBuildingGeneral.NguoiDung
{
    public partial class frmChangePassword : DevExpress.XtraEditors.XtraForm
    {
        public tnNhanVien _user = new tnNhanVien();
        public bool IsAdmin { get; set; }

        public frmChangePassword()
        {
            InitializeComponent();
            TranslateLanguage.TranslateControl(this);
        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnok_Click(object sender, EventArgs e)
        {
            if (txtpass.Text.Trim().Length==0 | txtpass2.Text.Trim().Length==0|txtcurrentpass.Text.Trim().Length==0)
            {
                Library.DialogBox.Alert("Các ô dữ liệu không được để trống");
                return;
            }
            if (String.Compare(txtpass.Text, txtpass2.Text, false) != 0 || txtpass.Text.Trim().Length == 0)
            {
                Library.DialogBox.Error("Mật khẩu không đúng");
                return;
            }
            if (txtcurrentpass.Text.Trim().Length == 0)
            {
                Library.DialogBox.Error("Mật khẩu không được để trống");
                return;
            }
            //CHANGE PASS
            tnNhanVien User = new tnNhanVien() { MaSoNV = _user.MaSoNV, MatKhau = txtpass.Text.Trim() };

            Library.HeThongCls.UserLogin usrlogin = new Library.HeThongCls.UserLogin();

            if (usrlogin.GetUserByMaNV(User.MaSoNV,txtcurrentpass.Text.Trim()) != null)
            {
                if (usrlogin.ChangePassword(User))
                {
                    Library.DialogBox.Alert("Đổi mật khẩuthành công");
                    this.Close();
                }
                else
                {
                    Library.DialogBox.Error("Đổi mật khẩu không thành công");
                }
            }
            else
                Library.DialogBox.Error("Mật khẩu hiện tại không đúng.");
        }

        private void frmChangePassword_Load(object sender, EventArgs e)
        {

        }
    }
}