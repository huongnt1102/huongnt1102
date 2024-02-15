using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Library;

namespace DichVu.ChoThue.PhuLuc
{
    public partial class frmEdit : DevExpress.XtraEditors.XtraForm
    {
        public thueHopDong objHD;
        public thuePhuLuc objPL;
        readonly MasterDataContext db;
        bool DaDuyetPL = false;
        public tnNhanVien objnhanvien;
        public int? ID { get; set; }
        public int? MaHD { get; set; }
        public frmEdit()
        {
            InitializeComponent();
            db = new MasterDataContext();
            TranslateLanguage.TranslateControl(this);
        }

        private void frmEdit_Load(object sender, EventArgs e)
        {
            lookLoaiTien.Properties.DataSource = db.tnTyGias;
            if (objPL != null)
            {
                objPL = db.thuePhuLucs.Single(p => p.ID == objPL.ID);
                objHD = objPL.thueHopDong;

                spinTyGiaPL.EditValue = objPL.TyGiaPL;
                txtSoPL.Text = objPL.SoPL;
                dateNgayPLNew.EditValue = objPL.NgayPL;
                dateNgayGiaoNew.EditValue = objPL.NgayGiao;
                spinThoiHanNew.EditValue = objPL.ThoiHan;
                spinDienTichNew.EditValue = objPL.DienTich;
                spinDonGiaNew.EditValue = objPL.DonGia;
                spinDonGiaUSD.EditValue = objPL.DonGiaUSD;
                txtDienGiai.Text = objPL.DienGiai;
                spinChuKyThanhToanNew.EditValue = objPL.ChuKyThanhToan;
                spinKyThanhToanUSD.EditValue = objPL.ChuKyThanhToanUSD;
                lookLoaiTien.EditValue = objPL.MaTG;
                dateNgayHH.EditValue = objPL.NgayHH;
                spinTienCoc.EditValue = objPL.SoTienCoc;
                DaDuyetPL = objPL.IsConfirm.GetValueOrDefault();
                spinTyGia.EditValue = objPL.TyGia;
                spinTienCocUSD.EditValue = objPL.SoTienCocUSD;
            }
            else
            {
                string MaPLNew = string.Empty;
                db.thuePhuLuc_getNewMaPL(ref MaPLNew);
                dateNgayPLNew.EditValue = db.GetSystemDate();
                txtSoPL.Text = db.DinhDang(27, int.Parse(MaPLNew));
                objPL = new thuePhuLuc();
                db.thuePhuLucs.InsertOnSubmit(objPL);

                objHD = db.thueHopDongs.Single(p => p.MaHD == MaHD);

                spinThoiHanNew.EditValue = objHD.ThoiHan;
                dateNgayGiaoNew.EditValue = objHD.NgayHH.Value.AddDays(1);
                spinDienTichNew.EditValue = objHD.DienTich;
                spinDonGiaUSD.EditValue = objHD.DonGiaUSD;
                spinDonGiaNew.EditValue = objHD.DonGia;
                txtDienGiai.Text = objHD.DienGiai;
                spinChuKyThanhToanNew.EditValue = objHD.ChuKyThanhToan;
                spinKyThanhToanUSD.EditValue = objHD.ChuKyThanhToanUSD;
            }

            txtSoHD.Text = objHD.SoHD;
            dateNgayBG.EditValue = objHD.NgayBG;
            spinThoiHan.EditValue = objHD.ThoiHan;
            txtTyGia.Text = objHD.MaTG != null ? objHD.tnTyGia.TenVT : "";
            if (txtTyGia.Text == "USD")
            {
                spinDonGia.EditValue = objHD.DonGiaUSD;
                spinThanhTien.EditValue = objHD.ThanhTienUSD;
            }
            else
            {
                spinDonGia.EditValue = objHD.DonGia;
                spinThanhTien.EditValue = objHD.ThanhTien; 
            }
            txtTrangThai.EditValue = objHD.MaTT != null ? objHD.thueTrangThai.TenTT : ""; ;
            txtMatBang.EditValue = objHD.mbMatBang.MaSoMB;
            spinDienTich.EditValue = objHD.DienTich;
            spinThoiHan.EditValue = objHD.ThoiHan;
            spinChuKyThanhToan.EditValue = objHD.ChuKyThanhToan.GetValueOrDefault() > 0 ? objHD.ChuKyThanhToan : objHD.ChuKyThanhToanUSD;
            txtKhachHang.Text = objHD.tnKhachHang != null ? (objHD.tnKhachHang.IsCaNhan.GetValueOrDefault() ? objHD.tnKhachHang.HoKH + " " + objHD.tnKhachHang.TenKH : objHD.tnKhachHang.CtyTen) : "";
            txtDiaChiKH.EditValue = objHD.tnKhachHang != null ? (objHD.tnKhachHang.DCLL) : "";
            txtDienGiai.Text = objHD.DienGiai;
            spinChuKyThanhToan.EditValue = objHD.ChuKyThanhToan;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!objnhanvien.IsSuperAdmin.Value)
            {
                if (DaDuyetPL)
                {
                    DialogBox.Error("Không thể thay đổi hợp đồng đã duyệt hoặc đã bàn giao mặt bằng.");
                    return;
                }
            }
            if (txtSoPL.Text.Length == 0)
            {
                DialogBox.Error("Vui lòng nhập [Số phụ lục], xin cảm ơn.");
                txtSoPL.Focus();
                return;
            }

            if (spinDienTichNew.Value > spinDienTich.Value)
            {
                DialogBox.Error("Diện tích thuê không được lớn hơn diện tích mặt bằng.");
                return;
            }

            if (ID == null)
            {
                objPL.MaNV = objnhanvien.MaNV;
                objPL.NgayTao = db.GetSystemDate();
                objPL.IsConfirm = false;
                objPL.NgayBGOld = objHD.NgayBG;
                objPL.DienTichOld = objHD.DienTich;
                objPL.DonGiaOld = objHD.DonGia;
                objPL.GiaThueOld = objHD.ThanhTien;
                objPL.ThoiHanOld = objHD.ThoiHan;
                objPL.ChuKyThanhToanOld = objHD.ChuKyThanhToan;
                objPL.MaHD = objHD.MaHD;
                objPL.ThoiHan = objPL.ThoiHanF1 = (int?)spinThoiHanNew.Value;
                objHD.ThoiHan += (int?)spinThoiHanNew.Value;
            }
            else
            {
                objPL.MaNVCN = objnhanvien.MaNV;
                objPL.NgayCN = db.GetSystemDate();
                objHD = db.thueHopDongs.SingleOrDefault(p => p.MaHD == objPL.MaHD);
                objHD.ThoiHan = objHD.ThoiHan.GetValueOrDefault() - objPL.ThoiHan.GetValueOrDefault() + Convert.ToInt32(spinThoiHanNew.EditValue ?? 0);
                objPL.ThoiHan = Convert.ToInt32(spinThoiHanNew.EditValue ?? 0);
            }
          //  if (objPL == null)
            objPL.TyGiaPL = (decimal?)spinTyGiaPL.EditValue;
            objPL.SoPL = txtSoPL.Text;
            objPL.NgayPL = dateNgayPLNew.DateTime;
            objPL.NgayGiao = dateNgayGiaoNew.DateTime;
            objPL.DienTich = spinDienTichNew.Value;
            objPL.DonGia = spinDonGiaNew.Value;
            objPL.DonGiaUSD = spinDonGiaUSD.Value;
            objPL.GiaThue = spinThanhTienNew.Value;
            objPL.GiaThueUSD = spinThanhTienUSD.Value;
        //    objPL.ThoiHan = (int?)spinThoiHanNew.Value;
            objPL.ChuKyThanhToan = (int?)spinChuKyThanhToanNew.Value;
            objPL.ChuKyThanhToanUSD = (int?)spinKyThanhToanUSD.Value;
            objPL.DienGiai = txtDienGiai.Text;
            objPL.SoTienCoc = (decimal?)spinTienCoc.EditValue;
            objPL.ThanhTienKTT = (decimal?)spinThanhTienKTT.EditValue;
            objPL.ThanhTienKTTUSD = (decimal?)spinThanhTienKTTUSD.EditValue;
            objPL.MaTG = (int?)lookLoaiTien.EditValue;
            objPL.NgayHH = (DateTime?)dateNgayHH.EditValue;
            objPL.TyGia = (decimal?)spinTyGia.EditValue;
            objPL.SoTienCocUSD = (decimal?)spinTienCocUSD.EditValue;
            var wait = DialogBox.WaitingForm();
            try
            {
                db.SubmitChanges();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch
            {
            }
            finally
            {
                wait.Close();
                wait.Dispose();
            }
        }

        private void spinThanhTienNew_EditValueChanged(object sender, EventArgs e)
        {
          //  if (spinThanhTien.EditValue != null && spinChuKyThanhToan.EditValue != null)
            spinThanhTienKTT.EditValue = (decimal?)spinThanhTienNew.EditValue * (decimal?)spinChuKyThanhToanNew.EditValue;
        }

        void LoadTienTyGia()
        {
            spinTyGia.Value = 1;
            spinTienCocUSD.Value= spinTienCoc.Value = 0;
            if (lookLoaiTien.EditValue == null)
                return;
            if (lookLoaiTien.Text == "VND")
            {
                spinTyGia.Enabled = false;
                spinTienCocUSD.Enabled = false;
                spinTienCoc.Enabled = true;
            }
            else if (lookLoaiTien.Text == "USD")
            {
                spinTyGia.Enabled = true;
                spinTienCocUSD.Enabled = true;
                spinTienCoc.Enabled = false;
            }
        }

        private void lookLoaiTien_EditValueChanged(object sender, EventArgs e)
        {
            LoadTienTyGia();
        }

        private void spinTienCocUSD_EditValueChanged(object sender, EventArgs e)
        {
            spinTienCoc.Value = spinTienCocUSD.Value * spinTyGia.Value;
        }

        private void spinThanhTienUSD_EditValueChanged(object sender, EventArgs e)
        {
            if ((decimal?)spinThanhTienUSD.EditValue != 0 && spinKyThanhToanUSD.EditValue != null)
            {
                spinThanhTienKTTUSD.EditValue = (decimal?)spinThanhTienUSD.EditValue * (decimal?)spinKyThanhToanUSD.EditValue;
                spinThanhTienNew.EditValue = spinThanhTienUSD.Value * spinTyGiaPL.Value;
            }
        }

        private void spinDienTichNew_EditValueChanged(object sender, EventArgs e)
        {
            // if (spinDonGiaNew.EditValue != null)
            spinThanhTienNew.EditValue = spinDonGiaNew.Value * spinDienTichNew.Value;
            // if (spinDonGiaUSD.EditValue != null)
            if ((decimal?)spinDonGiaUSD.EditValue != 0)
                spinDonGiaNew.EditValue = spinDonGiaUSD.Value * spinTyGiaPL.Value;
            spinThanhTienUSD.EditValue = spinDonGiaUSD.Value * spinDienTichNew.Value;
        }

        private void spinThoiHanNew_EditValueChanged(object sender, EventArgs e)
        {
            if (spinThoiHanNew.EditValue != null && dateNgayGiaoNew.EditValue != null)
                dateNgayHH.EditValue = dateNgayGiaoNew.DateTime.AddDays(1).AddMonths((int)spinThoiHanNew.Value);
        }

    }
}