using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Library;
using System.Linq;

namespace TaiSan.GhiTang
{
    public partial class frmTaiSanMB : DevExpress.XtraEditors.XtraForm
    {
        public int? MaTS { get; set; }
        MasterDataContext db;
        public frmTaiSanMB()
        {
            InitializeComponent();
            db = new MasterDataContext();
        }

        private void frmTaiSanMB_Load(object sender, EventArgs e)
        {
            slookMatBang.Properties.DataSource = db.mbMatBangs
                .Select(p => new { 
                    p.MaMB,
                    p.MaSoMB,
                    p.mbTangLau.TenTL,
                    p.mbTangLau.mbKhoiNha.TenKN,
                    p.mbTangLau.mbKhoiNha.tnToaNha.TenTN,
                    KhachHang=(p.MaKH==null?"":p.tnKhachHang.HoKH +" "+ p.tnKhachHang.TenKH)
                });
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (slookMatBang.EditValue == null)
            {
                DialogBox.Alert("Vui lòng chọn mặt bằng để lưu dữ liệu!");
                slookMatBang.Focus();
                return;
            }
            tsTaiSanMatBang obj = new tsTaiSanMatBang();
            obj.MaTS = MaTS;
            obj.MaMB = (int?)slookMatBang.EditValue;
            db.tsTaiSanMatBangs.InsertOnSubmit(obj);
            try
            {
                db.SubmitChanges();
                DialogBox.Alert("Lưu thành công!");
            }
            catch 
            {
                DialogBox.Alert("Có lỗi xảy ra, vui lòng kiểm tra kết nối!");
            }
            this.Close();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}