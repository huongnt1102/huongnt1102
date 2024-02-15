using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace EmailAmazon
{
    public partial class Main : DevExpress.XtraEditors.XtraForm
    {
        public Main()
        {
            InitializeComponent();
        }

        private void XtraForm1_Load(object sender, EventArgs e)
        {
            //Common.cmd = new API.APISoapClient();
            //Common.cmd.Open();
            
            //MessageBox.Show(Common.cmd.GetTaiKhoan_SoDu(Common.MaHD, Common.MatKhau).ToString());
            //Common.cmd.SendMail(Common.MaHD, Common.MatKhau, "Test Mail", "dohunglamk54@gmail.com", "dohunglamk54@gmail.com", "Lâm DIP test mail", "Đỗ Hùng Lâm. DIP nha trang");
        }
    }
}