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

namespace DichVu.DichVuCongCong
{
    public partial class frmThanhToan : DevExpress.XtraEditors.XtraForm
    {
        public dvCongCongThanhToan objdvcc;
        MasterDataContext db;

        public frmThanhToan()
        {
            InitializeComponent();
            db = new MasterDataContext();
            TranslateLanguage.TranslateControl(this);

        }

        private void frmThanhToan_Load(object sender, EventArgs e)
        {
            if (objdvcc != null)
            {
                try
                {
                    txtKhachHang.Text = objdvcc.mbMatBang.tnKhachHang.IsCaNhan.Value ? objdvcc.mbMatBang.tnKhachHang.HoKH + " " + objdvcc.mbMatBang.tnKhachHang.TenKH : objdvcc.mbMatBang.tnKhachHang.CtyTen;
                }
                catch
                {
                    txtKhachHang.Text = "Chưa có khách hàng";
                }
                txtMatBang.Text = objdvcc.mbMatBang.MaSoMB;
                txtPhiDichVu.Text = objdvcc.PhiDichVu.Value.ToString("C");  
                txtThangThanhToan.Text = objdvcc.ThangThanhToan.Value.ToString("y");  
            }
        }

        private void btnChapNhan_Click(object sender, EventArgs e)
        {

        }
    }
}