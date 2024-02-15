using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using DevExpress.XtraEditors;
using Library;

namespace DIPCRM.NhuCau
{
    public partial class frmAddProduct : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db = new MasterDataContext();
        ncSanPham objNCSP;
        public int MaNC = 0, ID = 0;
        public frmAddProduct()
        {
            InitializeComponent();

            //
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmAddProduct_Load(object sender, EventArgs e)
        {
            if (ID != 0)
            {
                objNCSP = db.ncSanPhams.Single(o => o.ID == ID);
                txtDienGiai.Text = objNCSP.DienGiai;
                //txtTenSP.Text = objNCSP.SanPham.TenSP;
                spinChietKhau.EditValue = objNCSP.TyLeCK;
                spinDonGia.EditValue = objNCSP.DonGia;
                spinSoLuong.EditValue = objNCSP.SoLuong;
                spinThanhTien.EditValue = objNCSP.ThanhTien;
                dateNgayDH.EditValue = objNCSP.NgayDatHang;
                //btnProduct.Text = objNCSP.SanPham.TenSP;
            }
            else
                dateNgayDH.EditValue = DateTime.Now;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ID == 0)
                objNCSP = new ncSanPham();

            objNCSP.TyLeCK = Convert.ToDecimal(spinChietKhau.EditValue);
            objNCSP.DienGiai = txtDienGiai.Text;
            objNCSP.DonGia = Convert.ToDecimal(spinDonGia.EditValue);
            objNCSP.MaNC = MaNC;
            objNCSP.NgayDatHang = (DateTime?)dateNgayDH.EditValue;
            objNCSP.SoLuong = Convert.ToInt32(spinSoLuong.EditValue);
            objNCSP.ThanhTien = Convert.ToDecimal(spinThanhTien.EditValue);

            if (ID == 0) db.ncSanPhams.InsertOnSubmit(objNCSP);

            try
            {
                db.SubmitChanges();
                DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
            catch { }
        }

        private void btnProduct_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            //var f = new Products.frmSelect();
            //f.ShowDialog();
            //if (f.ProductID != 0)
            //{
            //    btnProduct.Tag = f.ProductID;
            //    btnProduct.Text = f.ProductName;

            //    SanPham objSP = db.SanPhams.Single(o => o.ID == f.ProductID);

            //    txtTenSP.Text = objSP.TenSP;
            //    spinDonGia.EditValue = objSP.GiaBan;
            //}
        }

        private void spinSoLuong_EditValueChanged(object sender, EventArgs e)
        {
            SpinEdit _New = (SpinEdit)sender;
            spinThanhTien.EditValue = Convert.ToDecimal(_New.EditValue) * Convert.ToDecimal(spinDonGia.EditValue);
        }

        private void spinDonGia_EditValueChanged(object sender, EventArgs e)
        {
            SpinEdit _New = (SpinEdit)sender;
            spinThanhTien.EditValue = Convert.ToDecimal(_New.EditValue) * Convert.ToDecimal(spinSoLuong.EditValue);
        }
    }
}
