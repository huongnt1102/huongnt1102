using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Library;
using EmailAmazon.API;

namespace EmailAmazon.Brand
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
            switch (MailCommon.cmd.EditThuongHieu(MailCommon.MaHD, MailCommon.MatKhau, this.ID, this.txtTenTH.Text, this.txtEmail.Text, this.txtDienGiai.Text, Common.User.HoTenNV))
            {
                case Result.ThuongHieuDaTonTai:
                    DialogBox.Error("Trùng tên thương hiệu");
                    break;
                case Result.EmailKhongHhopLe:
                    DialogBox.Error("Email không hợp lệ");
                    break;
                case Result.ThanhCong:
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                    break;
            }
        }

        private void frmEdit_Load(object sender, EventArgs e)
        {
            if (this.ID <= 0)
                return;
            ThuongHieu thuongHieu = MailCommon.cmd.DetailThuongHieu(MailCommon.MaHD, MailCommon.MatKhau, this.ID);
            this.txtTenTH.EditValue = (object)thuongHieu.TenTH;
            this.txtEmail.EditValue = (object)thuongHieu.Email;
            this.txtDienGiai.EditValue = (object)thuongHieu.DienGiai;
        }
    }
}