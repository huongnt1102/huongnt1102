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

namespace DichVu.MatBang
{
    public partial class frmChiaMatBang : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public mbMatBang objmatbang;
        public tnNhanVien objnhanvien;

        public frmChiaMatBang()
        {
            InitializeComponent();
            db = new MasterDataContext();
        }

        private void frmChiaMatBang_Load(object sender, EventArgs e)
        {
            lookLoaiMB.Properties.DataSource = db.mbLoaiMatBangs;

            if (objmatbang != null)
            {
                //txtGiaChoThue.Text = objmatbang.ThanhTien.ToString();
                if (objmatbang.MaLMB != null)
                {
                    txtLoaiMatBang.Text = db.mbLoaiMatBangs.SingleOrDefault(p1 => p1.MaLMB == objmatbang.MaLMB).TenLMB;//objmatbang.mbLoaiMatBang.TenLMB;
                    lookLoaiMB.EditValue = objmatbang.MaLMB;
                }
                txtKyHieu.Text = objmatbang.MaSoMB;
                //txtDonGia.Text = objmatbang.DonGia.ToString();

                if (objmatbang.MaTL != null)
                {
                    txtToaNha.Text = txtToaNha2.Text = objmatbang.mbTangLau.mbKhoiNha.tnToaNha.TenTN;
                    txtTangLau.Text = txtTangLau2.Text = objmatbang.mbTangLau.TenTL;
                    txtKhoiNha.Text = txtKhoiNha2.Text = objmatbang.mbTangLau.mbKhoiNha.TenKN;
                }
                txtDienTich.Value = objmatbang.DienTich ?? 0;
            }
        }

        private void spinDienTich_EditValueChanged(object sender, EventArgs e)
        {
            if (spinDienTich.Value > objmatbang.DienTich)
            {
                DialogBox.Error("Diện tích mặt bằng đích không được lớn hơn mặt bằng nguồn");
                return;
            }
            txtDienTich.Value = (objmatbang.DienTich ?? 0) - spinDienTich.Value;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (txtKyHieu2.Text.Trim() == "")
            {
                DialogBox.Error("Ký hiệu / Mã mặt bằng không được để trống");
                return;
            }

            if (db.mbMatBangs.Count(p => p.MaSoMB.Trim() == txtKyHieu2.Text.Trim()) > 0)
            {
                DialogBox.Error("Ký hiệu / Mã mặt bằng này đã tồn tại");
                return;
            }
            if (spinDienTich.Value == 0)
            {
                DialogBox.Error("Vui lòng điền diện tích cho mặt bằng được chia ra");
                return;
            }


            var objmatbangSplit = new mbMatBang();

            //objmatbangSplit.MaSoMB = txtKyHieu2.Text.Trim();
            //objmatbangSplit.tnTyGia = db.tnTyGias.Single(p => p.MaTG == objmatbang.MaTG);
            //objmatbangSplit.mbTrangThai = db.mbTrangThais.Single(p => p.MaTT == objmatbang.MaTT);
            //objmatbangSplit.DienTich = spinDienTich.Value;
            //objmatbangSplit.DonGia = spinDonGia.Value;
            //objmatbangSplit.ThanhTien = spinGiaChoThue.Value;
            //objmatbangSplit.mbTangLau = db.mbTangLaus.Single(p => p.MaTL == objmatbang.MaTL);
            //if (lookLoaiMB.EditValue != null)
            //    objmatbangSplit.mbLoaiMatBang = db.mbLoaiMatBangs.Single(p => p.MaLMB == (int)lookLoaiMB.EditValue);
            //objmatbangSplit.tnKhachHang = null;
            //objmatbangSplit.tnKhachHang = null;
            //objmatbangSplit.MaNV = objnhanvien.MaNV;
            //objmatbangSplit.DienGiai = "Mặt bằng này được chia ra từ mặt bằng " + objmatbang.MaSoMB;

            //objmatbangSplit.TinhPhiTheoDienTich = objmatbang.TinhPhiTheoDienTich ?? false;
            //objmatbangSplit.IsCanHoCaNhan = objmatbang.IsCanHoCaNhan ?? true;
            //objmatbangSplit.DaGiaoChiaKhoa = objmatbang.DaGiaoChiaKhoa ?? false;
            //if (objmatbangSplit.TinhPhiTheoDienTich ?? false)
            //{
            //    objmatbangSplit.SoTienPerMet = objmatbangSplit.SoTienPerMet;
            //    objmatbangSplit.DienTichThuPhi = spinDienTich.Value;
            //    objmatbangSplit.PhiQuanLy = objmatbangSplit.SoTienPerMet * spinDienTich.Value;
            //}


            //db.mbMatBangs.InsertOnSubmit(objmatbangSplit);

            //objmatbang = db.mbMatBangs.Single(p => p.MaMB == objmatbang.MaMB);
            //objmatbang.DienTich = txtDienTich.Value;
            //if (objmatbang.TinhPhiTheoDienTich ?? false)
            //{
            //    objmatbang.SoTienPerMet = objmatbang.SoTienPerMet;
            //    objmatbang.DienTichThuPhi = objmatbang.DienTichThuPhi + objmatbang.DienTichThuPhi;
            //    objmatbang.PhiQuanLy = objmatbang.SoTienPerMet * objmatbang.DienTichThuPhi;
            //}
            try
            {
                db.SubmitChanges();
                DialogBox.Alert("Chia mặt bằng thành công, mặt bằng mới phát sinh có mã " + objmatbangSplit.MaSoMB);
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

    }
}