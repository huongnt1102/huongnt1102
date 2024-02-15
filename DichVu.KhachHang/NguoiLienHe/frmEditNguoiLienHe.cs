using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DichVu.KhachHang.NguoiLienHe
{
    public partial class frmEditNguoiLienHe : DevExpress.XtraEditors.XtraForm
    {
        #region Param public
        public int? MaKH { get; set; }
        public int? Id { get; set; }
        #endregion 

        public frmEditNguoiLienHe()
        {
            InitializeComponent();
        }

        private void frmEditNguoiLienHe_Load(object sender, EventArgs e)
        {
            var db = new Library.MasterDataContext();

            glkBoPhan.Properties.DataSource = db.tnKhachHang_NguoiLienHe_BoPhans;
            glkNhomLienHe.Properties.DataSource = db.tnKhachHang_NguoiLienHe_NhomLienHes;

            if(Id != 0)
            {
                var nguoiLienHe = db.tnKhachHang_NguoiLienHes.FirstOrDefault(_ => _.ID == Id);
                if(nguoiLienHe != null)
                {
                    txtHoTen.Text = nguoiLienHe.HoVaTen;
                    txtDiaChi.Text = nguoiLienHe.DiaChi;
                    txtEmail.Text = nguoiLienHe.Email;
                    txtDienThoai.Text = nguoiLienHe.DienThoai;
                    txtTaiKhoan.Text = nguoiLienHe.TaiKhoan;
                    txtGhiChu.Text = nguoiLienHe.GhiChu;

                    glkBoPhan.EditValue = nguoiLienHe.MaBoPhan;
                    glkNhomLienHe.EditValue = nguoiLienHe.MaNhomLH;
                }
            }
        }

        private void itemLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var model = new { id = Id, hoTen = txtHoTen.Text, diaChi = txtDiaChi.Text, email = txtEmail.Text, dienThoai = txtDienThoai.Text, taiKhoan = txtTaiKhoan.Text, ghiChu = txtGhiChu.Text, matKhau = txtMatKhau.Text, boPhan = glkBoPhan.EditValue, nguoiLienHe = glkNhomLienHe.EditValue, makh = MaKH };
            var param = new Dapper.DynamicParameters();
            param.AddDynamicParams(model);
            Library.Class.Connect.QueryConnect.Query<bool>("tnKhachHang_NguoiLienHe_Edit", param);

            Library.DialogBox.Success("Lưu dữ liệu thành công.");
            DialogResult = DialogResult.OK;
            Close();
        }

        private void itemHuy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}