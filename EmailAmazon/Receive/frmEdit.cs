using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Library;

namespace EmailAmazon.Receive
{
    public partial class frmEdit : DevExpress.XtraEditors.XtraForm
    {
        public int ID { get; set; }

        public frmEdit()
        {
            InitializeComponent();
            MailCommon.cmd = new API.APISoapClient();
            MailCommon.cmd.Open();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtTieuDe.Text.Trim() == "")
            {
                DialogBox.Error("Vui lòng nhập tên nhóm");
                txtTieuDe.Focus();
                return;
            }
            else
            {
                switch (MailCommon.cmd.EditNhomKhachHang(MailCommon.MaHD, MailCommon.MatKhau, this.ID, txtTieuDe.Text, txtDienGiai.Text, Common.User.HoTenNV))
                {
                    case API.Result.NhomKhachHangDaTonTai:
                        DialogBox.Error("Trùng tên nhóm khách hàng");
                        break;
                    case API.Result.ThanhCong:
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                        break;
                }
            }
        }

        private void frmEdit_Load(object sender, EventArgs e)
        {
            if (this.ID == 0)
                return;
            API.NhomKhachHang nhomKhachHang = MailCommon.cmd.DetailNhomKhachHang(MailCommon.MaHD, MailCommon.MatKhau, this.ID);
            txtTieuDe.Text = nhomKhachHang.TenNKH;
            txtDienGiai.Text = nhomKhachHang.DienGiai;
        }
    }
}