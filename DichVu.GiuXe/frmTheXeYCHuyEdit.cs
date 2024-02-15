using Library;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DichVu.GiuXe
{
    public partial class frmTheXeYCHuyEdit : Form
    {
        public frmTheXeYCHuyEdit()
        {
            InitializeComponent();

        }
        public int? MaKH { get; set; }
        public int? ID { get; set; }
        public byte? MaTN { get; set; }

        public bool? IsTheCuDan { get; set; }


        public dvgxTheXeCuDan objTheCuDan { get; set; }
        dvgxTheXe objTheXe;

        MasterDataContext db;

        private void frmTheXeYCHuyEdit_Load(object sender, EventArgs e)
        {
           // TranslateLanguage.TranslateControl(this);

            db = new MasterDataContext();
            if (this.ID != null)
            {
                objTheXe = db.dvgxTheXes.Single(p => p.ID == this.ID);
                txtMatBang.EditValue = objTheXe.MaMB;
                txtSoThe.Text = objTheXe.SoThe;
                txtNĐK.EditValue = objTheXe.NgayDK;
                txtLoaiXe.EditValue = objTheXe.MaLX;
                txtTenChuThe.EditValue = objTheXe.ChuThe;
                txtBienSo.EditValue = objTheXe.BienSo;
                txtMauXe.EditValue = objTheXe.MauXe;
                txtDoiXe.EditValue = objTheXe.DoiXe;
                txtDienGiai.EditValue = objTheXe.DienGiai;
                txtPGX.EditValue = objTheXe.PhiLamThe;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                objTheXe.NgayDuyetNgung = DateTime.Now;
                objTheXe.NVDuyetNgung = Common.User.MaNV;
                objTheXe.IsYCDuyet = true;
                objTheXe.NgungSuDung = true;
                db.SubmitChanges();
                DialogBox.Alert("Đã ngưng thẻ xe thành công");
            }
            catch
            {

            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
