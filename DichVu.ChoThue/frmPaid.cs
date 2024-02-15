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
using DevExpress.XtraEditors.Popup;
using DevExpress.Utils.Win;

using System.Data.Linq.SqlClient;
using DevExpress.XtraReports.UI;

namespace DichVu.ChoThue
{
    public partial class frmPaid : DevExpress.XtraEditors.XtraForm
    {
        public tnNhanVien objnhanvien;
        public thueHopDong objhd;
        public DateTime? NgayTT { get; set; }
        public int MaMB;
        public int? MaKH, MaHD;
        string sSoPhieu;
        public DateTime? date;
        public decimal? soTien = 0;
        public int MaPhieu = 0;
        public string MaSoMB = "";
        PhieuThu objPT;
        ptChiTiet objPTCT;

        MasterDataContext db;
        DateTime now;

        public frmPaid()
        {
            InitializeComponent();
            db = new MasterDataContext();
            TranslateLanguage.TranslateControl(this);
            now = db.GetSystemDate();
        }

        private void btnChapNhan_Click(object sender, EventArgs e)
        {
            if (txtSoPhieu.Text.Trim() == "")
            {
                DialogBox.Alert("Vui lòng nhập [Số phiếu thu], xin cảm ơn.");
                txtSoPhieu.Focus();
                return;
            }

            if (dateNgayThu.Text.Trim() == "")
            {
                DialogBox.Alert("Vui lòng nhập [Ngày thu], xin cảm ơn.");
                dateNgayThu.Focus();
                return;
            }

            if (spinSoThang.Value == 0)
            {
                DialogBox.Alert("Vui lòng nhập [Số tháng], xin cảm ơn.");
                spinSoThang.Focus();
                return;
            }

            if (spinThucThu.Value == 0)
            {
                DialogBox.Alert("Vui lòng nhập [Thực thu], xin cảm ơn.");
                spinSoThang.Focus();
                return;
            }
            var objHD = db.thueHopDongs.FirstOrDefault(p=>p.MaHD == MaHD);
            decimal? PhaiThu = spinTyGia.Value == 1 ? spinPhaiThu.Value : spinPhaiThuUSD.Value;
            decimal? ThucThu = spinTyGia.Value == 1 ? spinThucThu.Value : spinThucThuUSD.Value;

            if (MaPhieu == 0)
            {
                objPT = new PhieuThu();
                objPT.NgayNhap = now;
                #region Detail
                objPTCT = new ptChiTiet();
                objPT.ptChiTiets.Add(objPTCT);
                objPTCT.ChietKhau = spinChietKhau.Value;
                objPTCT.DienGiai = txtDienGiai.Text;
                objPTCT.MaLDV = 5;
                objPTCT.MaMB = MaMB;
                if (dateNgayThu.DateTime.Year != 1)
                    objPTCT.NgayThu = dateNgayThu.DateTime;
                objPTCT.PhaiThu = PhaiThu;
                objPTCT.TongCong = ThucThu;
                objPTCT.MaHD = objhd.MaHD;
                #endregion

                objPT.MaNV = objnhanvien.MaNV;
            }
            else
            {
                objPT.MaNVCN = objnhanvien.MaNV;
                objPT.NgayCN = now;

                objPTCT = objPT.ptChiTiets.FirstOrDefault();
                objPTCT.ChietKhau = spinChietKhau.Value;
                objPTCT.DienGiai = txtDienGiai.Text;
                objPTCT.MaLDV = 5;
                objPTCT.MaMB = MaMB;
                if (dateNgayThu.DateTime.Year != 1)
                    objPTCT.NgayThu = dateNgayThu.DateTime;
                objPTCT.PhaiThu = PhaiThu;
                objPTCT.TongCong = ThucThu;
                objPTCT.MaHD = objhd.MaHD;
            }

            objPT.SoPhieu = txtSoPhieu.Text.Trim();
            objPT.DiaChi = txtDiaChi.Text.Trim();
            objPT.NguoiNop = txtNguoiNop.Text.Trim();
            objPT.DichVu = 5;
            objPT.DienGiai = txtDienGiai.Text.Trim();
            objPT.DotThanhToan = db.GetSystemDate();
            objPT.MaHopDong = MaMB.ToString();
            objPT.MaNV = objnhanvien.MaNV;
            objPT.IsXuatHoaDon = (bool)chkXuatHoaDon.Checked;
            if (dateNgayThu.DateTime.Year != 1)
                objPT.NgayThu = dateNgayThu.DateTime;
            objPT.SoTienThanhToan = ThucThu;
            objPT.SoThangThuPhiQuanLy = Convert.ToInt32(spinSoThang.Value.ToString());
            objPT.SoTienChietKhauPhiQL = spinChietKhau.Value;
            objPT.PhaiThu = PhaiThu;
            objPT.PhiQuanLy = spinPhiThangVND.Value;
            objPT.KeToanDaDuyet = false;
            objPT.MaMB = MaMB;
            objPT.CusID = MaKH;
            objPT.ChuyenKhoan = ckChuyenKhoan.Checked;
            objPT.MaHD = MaHD;

            try
            {
              //  objhd.
                if (MaPhieu == 0)
                    db.PhieuThus.InsertOnSubmit(objPT);

                db.SubmitChanges();
                if (DialogBox.Question("Bạn có muốn in phiếu thu không?") == DialogResult.Yes)
                {
                  //  ReportMisc.DichVu.Quy.rptPhieuThu rpt = new ReportMisc.DichVu.Quy.rptPhieuThu(objPT.MaPhieu, "no");
                    //var rpt = new ReportMisc.DichVu.HoaDon.rptPhieuThu(objPT.MaPhieu);
                    //rpt.ShowPreviewDialog();
                }
                if (chkXuatHoaDon.Checked)
                {
                    //ReportMisc.DichVu.Quy.rptHoaDonHDTH rpt = new ReportMisc.DichVu.Quy.rptHoaDonHDTH(objPT);
                    //rpt.ShowPreviewDialog();
                }
            }
            catch
            {
                DialogBox.Error("Không lưu được dữ liệu, vui lòng thử lại sau");
            }
        }

        public string CountCharater(string s)
        {
            string str = "";
            switch (s.Length)
            {
                case 1:
                    str = "0000" + s;
                    break;
                case 2:
                    str = "000" + s;
                    break;
                case 3:
                    str = "00" + s;
                    break;
                case 4:
                    str = "0" + s;
                    break;
                case 5:
                    str = s;
                    break;
            }
            return str;
        }

        public string CreateKeyword(DateTime date)
        {
            string SoPhieu = "";
            string cSoPhieu = "";
            try
            {
                var obj = db.PTDemSoLuongs.FirstOrDefault(p => p.ThangDem.Value.Month == date.Month & p.ThangDem.Value.Year == date.Year);
                if (obj == null)
                {
                    var objdem = new PTDemSoLuong();
                    objdem.ThangDem = date;
                    objdem.SoLuongPhieu = 1;
                    SoPhieu = string.Format("PT{0:yyyy}/{0:MM}/00001", date);
                    db.PTDemSoLuongs.InsertOnSubmit(objdem);
                }
                else
                {
                    cSoPhieu = (obj.SoLuongPhieu + 1).ToString();
                    obj.SoLuongPhieu++;
                    SoPhieu = string.Format("PT{0:yyyy}/{0:MM}/{1}", date, CountCharater(cSoPhieu));
                }
            }
            catch { }
            return SoPhieu;
        }

        private void frmThanhToan_Load(object sender, EventArgs e)
        {
            if (MaPhieu == 0)
            {
                now = db.GetSystemDate();
                txtSoPhieu.Text = CreateKeyword(now);
                var objmb = db.mbMatBangs.Single(p => p.MaMB == MaMB);
                txtMatBang.Text = objmb.MaSoMB;
                MaKH = objmb.MaKH;
                //Dien ten mac dinh
                try
                {
                    txtNguoiNop.Text = objmb.tnKhachHang.IsCaNhan.Value ? String.Format("{0} {1}", objmb.tnKhachHang.HoKH, objmb.tnKhachHang.TenKH) : objmb.tnKhachHang.CtyTen;
                    txtDiaChi.Text = objmb.tnKhachHang.DCLL;
                    txtDienGiai.Text = string.Format("Thanh toán tiền cho thuê hợp đồng {0}, mặt bằng {1}", objhd.SoHD, objhd.mbMatBang.MaSoMB);
                    MaHD = objhd.MaHD;
                    spinSoThang.EditValue = objhd.TyGia == 1 ? objhd.ChuKyThanhToan : objhd.ChuKyThanhToanUSD;
                }
                catch { }
                dateNgayThu.DateTime = now;
                ///kiểm tra load số tiền khi phụ lục thay đổi
                var objPL = objhd.thuePhuLucs.FirstOrDefault(p => SqlMethods.DateDiffDay(p.NgayGiao, NgayTT) >= 0 && SqlMethods.DateDiffDay(p.NgayHH, NgayTT) <= 0);
                if (objPL == null)
                {
                    spinPhiThangVND.EditValue = objhd.ThanhTien ?? 0;
                    spinPhiThangUSD.EditValue = objhd.ThanhTienUSD ?? 0;
                    spinSoTien.EditValue = objhd.ThanhTien ?? 0;
                    spinSoTienUSD.EditValue = objhd.ThanhTienUSD ?? 0;
                    spinPhaiThu.EditValue = objhd.ThanhTien ?? 0;
                    spinPhaiThuUSD.EditValue = objhd.ThanhTienUSD ?? 0;
                    spinThucThu.EditValue = objhd.ThanhTien ?? 0;
                }
                else
                {
                    spinPhiThangVND.EditValue = objPL.DonGia ?? 0;
                    spinPhiThangUSD.EditValue = objPL.DonGiaUSD ?? 0;
                    spinSoTienUSD.EditValue = objPL.GiaThueUSD;
                    spinSoTien.EditValue = objPL.GiaThue ?? 0;
                    spinPhaiThu.EditValue = objPL.GiaThue ?? 0;
                    spinPhaiThuUSD.EditValue = objPL.GiaThueUSD ?? 0;
                    spinThucThu.EditValue = objPL.GiaThue ?? 0;
                }
            }
            else
            {
                objPT = db.PhieuThus.Single(p => p.MaPhieu == MaPhieu);
                txtSoPhieu.Text = objPT.SoPhieu;
                txtDiaChi.Text = objPT.DiaChi;
                txtDienGiai.Text = objPT.DienGiai;
                txtMatBang.Text = MaSoMB;
                txtNguoiNop.Text = objPT.NguoiNop;
                MaKH = objPT.CusID;
                MaMB = objPT.MaMB.Value;
                if (objPT.NgayThu != null)
                    dateNgayThu.DateTime = objPT.NgayThu.Value;
                spinPhiThangVND.EditValue = objPT.PhiQuanLy ?? 0;
                spinSoThang.EditValue = objPT.SoThangThuPhiQuanLy ?? 0;
                //spinSoTien.EditValue = objPT.mbMatBang.PhiQuanLy * objPT.SoThangThuPhiQuanLy;
                spinChietKhau.EditValue = objPT.SoTienChietKhauPhiQL ?? 0;
                spinPhaiThu.EditValue = objPT.PhaiThu ?? 0;
              //  spinPhaiThuUSD.EditValue=objPT.
                spinThucThu.EditValue = objPT.SoTienThanhToan ?? 0;
                ckChuyenKhoan.Checked = objPT.ChuyenKhoan.GetValueOrDefault();
                MaHD = objPT.MaHD;
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void spinSoThang_EditValueChanged(object sender, EventArgs e)
        {
            spinSoTien.EditValue = spinPhaiThu.EditValue = spinThucThu.EditValue = spinPhiThangVND.Value * spinSoThang.Value;
            spinSoTienUSD.EditValue = spinPhaiThuUSD.EditValue = spinThucThuUSD.EditValue = spinPhiThangUSD.Value * spinSoThang.Value;
        }

        void SetTyGIa()
        {
            if (spinTyGia.Value == 1)
            {
                spinThucThu.Enabled = true;
                spinThucThuUSD.Enabled = false;
                spinThucThuUSD.EditValue = 0;

            }
            else
            {
                spinThucThu.Enabled = false;
                spinThucThuUSD.Enabled = true;
                spinThucThu.EditValue = spinThucThuUSD.Value * spinTyGia.Value;
            }
        }

        private void spinThucThu_EditValueChanged(object sender, EventArgs e)
        {
            SetTyGIa();
        }

        private void spinThucThuUSD_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                spinThucThu.EditValue = spinThucThuUSD.Value * (db.tnTyGias.FirstOrDefault(p => p.TenVT == "USD").TyGia);
            }
            catch { }
        }

        private void spinTyGia_EditValueChanged(object sender, EventArgs e)
        {
            SetTyGIa();
        }
    }
}