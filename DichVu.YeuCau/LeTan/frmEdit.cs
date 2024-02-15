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

namespace DichVu.YeuCau.LeTan
{
    public partial class frmEdit : DevExpress.XtraEditors.XtraForm
    {
        public long? ID { get; set; }
        public byte? MaTN { get; set; }

        MasterDataContext db;
        ltLeTan objLT;

        public frmEdit()
        {
            InitializeComponent();
        }

        private void frmEdit_Load(object sender, EventArgs e)
        {
            db = new MasterDataContext();
            lookKhachHang.Properties.DataSource = (from kh in db.tnKhachHangs
                                                   where kh.MaTN == this.MaTN
                                                   select new
                                                   {
                                                       kh.MaKH,
                                                       kh.KyHieu,
                                                       TenKH = kh.IsCaNhan.Value ? kh.HoKH + " " + kh.TenKH : kh.CtyTen,
                                                       SoCMND = kh.IsCaNhan.Value ? kh.CMND : kh.MaSoThue,
                                                       DienThoai = kh.IsCaNhan.Value ? kh.DienThoaiKH : kh.CtyTen
                                                   }).ToList();

            if (this.ID == null)
            {
                objLT = new ltLeTan();
                db.ltLeTans.InsertOnSubmit(objLT);

                dateNgayVao.EditValue = db.GetSystemDate();
                spinSoLuong.EditValue = 0;

                objLT.MaTN = this.MaTN;
                objLT.MaNVN = Common.User.MaNV;
                objLT.NgayNhap = db.GetSystemDate();
            }
            else
            {
                objLT = db.ltLeTans.Single(p => p.ID == this.ID);
                txtSoThe.EditValue = objLT.SoThe;
                spinSoLuong.EditValue = objLT.SoLuongNguoi;
                dateNgayVao.EditValue = objLT.GioVao;
                txtKhachDen.EditValue = objLT.KhachDen;
                txtSoCMND.EditValue = objLT.SoCMND;
                dateNgayCap.EditValue = objLT.NgayCap;
                txtNoiCap.EditValue = objLT.NoiCap;
                txtDonViCongTac.EditValue = objLT.DonViCongTac;
                txtGhiChu.EditValue = objLT.GhiChu;
                lookKhachHang.EditValue = objLT.MaKH;
                txtNguoiLienHe.EditValue = objLT.NguoiLienHe;
                txtDonViLienHe.EditValue = objLT.DonViLienHe;
                txtViTri.EditValue = objLT.ViTriKhachToi;

                objLT.NgaySua = db.GetSystemDate();
                objLT.MaNVS = Common.User.MaNV;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtSoThe.Text.Trim() == "")
            {
                DialogBox.Alert("Vui lòng nhập [Số thẻ]. Xin cám ơn");
                txtSoThe.Focus();
                return;
            }

            if (txtSoThe.Text.Trim() == "")
            {
                DialogBox.Alert("Vui lòng nhập [Số thẻ]. Xin cám ơn");
                txtSoThe.Focus();
                return;
            }

            if (dateNgayVao.EditValue == null)
            {
                DialogBox.Alert("Vui lòng nhập [Ngày vào]. Xin cám ơn");
                dateNgayVao.Focus();
                return;
            }

            if (txtKhachDen.Text.Trim() == "")
            {
                DialogBox.Alert("Vui lòng nhập [Khách đến]. Xin cám ơn");
                txtKhachDen.Focus();
                return;
            }

            try
            {
                objLT.SoThe = txtSoThe.Text;
                objLT.SoLuongNguoi = Convert.ToInt32(spinSoLuong.EditValue);
                objLT.GioVao = (DateTime?)dateNgayVao.EditValue;
                objLT.KhachDen = txtKhachDen.Text;
                objLT.SoCMND = txtSoCMND.Text;
                objLT.NgayCap = (DateTime?)dateNgayCap.EditValue;
                objLT.NoiCap = txtNoiCap.Text;
                objLT.DonViCongTac = txtDonViCongTac.Text;
            //    objLT.GhiChu = txtGhiChu.Text;
                objLT.MaKH = (int?)lookKhachHang.EditValue;
                objLT.NguoiLienHe = txtNguoiLienHe.Text;
                objLT.DonViLienHe = txtDonViLienHe.Text;
                objLT.ViTriKhachToi = txtViTri.Text;

                db.SubmitChanges();

                DialogBox.Success();
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lookKhachHang_SizeChanged(object sender, EventArgs e)
        {
            lookKhachHang.Properties.PopupFormSize = new Size(lookKhachHang.Size.Width, 0);
        }
    }
}