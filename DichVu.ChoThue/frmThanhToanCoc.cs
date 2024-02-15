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
using DichVu.HoaDon;
using System.Data.Linq.SqlClient;
using DevExpress.XtraReports.UI;

namespace DichVu.ChoThue
{
    public partial class frmThanhToanCoc : DevExpress.XtraEditors.XtraForm
    {
        public tnNhanVien objnhanvien;
        public int? MaDotTT { get; set; }
        public int MaPhieu = 0;
        thueLTTCoc objLTT;
       // thueHopDong objhd;
       // public DateTime? NgayTT { get; set; }
       //  public int MaMB;
       // public int? MaKH, MaHD;
       //  string sSoPhieu;
       // public DateTime? date;
       //  public decimal? soTien = 0;
       // public string MaSoMB = "";
        PhieuThu objPT;
        ptChiTiet objPTCT;

        MasterDataContext db;
        DateTime now;

        public frmThanhToanCoc()
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
            if (spinPhaiThu.Value <= 0)
            {
                DialogBox.Alert("Vui lòng nhập số tiền thực thu");
                return;
            }
            decimal? PhaiThu = spinTyGia.Value == 1 ? spinPhaiThu.Value : spinPhaiThuUSD.Value;
            decimal? ThucThu = spinTyGia.Value == 1 ? spinThucThu.Value : spinThucThuUSD.Value;

            if (MaPhieu == 0)
            {
                objPT = new PhieuThu();
                db.PhieuThus.InsertOnSubmit(objPT);
                objPT.NgayNhap = now;
                objPT.MaNV = objnhanvien.MaNV;
                objPT.DotThanhToan = now;
            }
            else
            {
                objPT.MaNVCN = objnhanvien.MaNV;
                objPT.NgayCN = now;
            }

            objPT.SoPhieu = txtSoPhieu.Text.Trim();
            objPT.DiaChi = txtDiaChi.Text.Trim();
            objPT.NguoiNop = txtNguoiNop.Text.Trim();
            objPT.DichVu = 12;
            objPT.DienGiai = txtDienGiai.Text.Trim();
            objPT.MaHD = objLTT.MaHD;
            objPT.MaNV = objnhanvien.MaNV;
            if (dateNgayThu.DateTime.Year != 1)
                objPT.NgayThu = dateNgayThu.DateTime;
          //  objPT.SoTienThanhToan = ThucThu;
            objPT.PhaiThu = PhaiThu;
            objPT.KeToanDaDuyet = false;
            objPT.MaMB = objLTT.thueHopDong.MaMB;
            objPT.CusID = objLTT.thueHopDong.MaKH;
            objPT.ChuyenKhoan = ckChuyenKhoan.Checked;
            objPT.MaHD = objLTT.MaHD;
            objPT.IsXuatHoaDon = (bool)chkXuatHoaDon.Checked;
            objPT.SoHDVAT = txtSoHoaDonVAT.Text;
            objPT.IsTienCoc = true;
            objPT.SoTienCoc = ThucThu;
            objPT.MaDotTT = objLTT.ID;
            objLTT = db.thueLTTCocs.FirstOrDefault(p => p.ID == objLTT.ID);
            objLTT.DaThu = (objLTT.DaThu ?? 0) + (ThucThu ?? 0);
            objLTT.NgayThu = now;
            try
            {
                db.SubmitChanges();
                if (DialogBox.Question("Bạn có muốn in phiếu thu không?") == DialogResult.Yes)
                {
                    //var rpt = new ReportMisc.DichVu.HoaDon.rptPhieuThuCoc(objPT.MaPhieu);
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
            this.Close();
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
                objLTT = db.thueLTTCocs.FirstOrDefault(p => p.ID == MaDotTT);
                var objmb = db.mbMatBangs.Single(p => p.MaMB == objLTT.thueHopDong.MaMB);
             //   objhd = db.thueHopDongs.FirstOrDefault(p => p.MaHD == objLTT.MaHD);
                try
                {
                    txtNguoiNop.Text = objmb.tnKhachHang.IsCaNhan.Value ? String.Format("{0} {1}", objmb.tnKhachHang.HoKH, objmb.tnKhachHang.TenKH) : objmb.tnKhachHang.CtyTen;
                    txtDiaChi.Text = objmb.tnKhachHang.DCLL;
                    txtDienGiai.Text = string.Format("Thanh toán tiền cọc hợp đồng {0}, mặt bằng {1}", objLTT.thueHopDong.SoHD, objLTT.thueHopDong.mbMatBang.MaSoMB);
                    spinPhaiThu.EditValue = objLTT.SoTien ?? 0 - objLTT.DaThu ?? 0;
                    dateNgayThu.DateTime = now;
                    //MaHD = objhd.MaHD;
                }
                catch { }
            }
            else
            {
                objPT = db.PhieuThus.Single(p => p.MaPhieu == MaPhieu);
                objLTT = db.thueLTTCocs.FirstOrDefault(p => p.ID == objPT.MaDotTT);
                txtSoPhieu.Text = objPT.SoPhieu;
                txtDiaChi.Text = objPT.DiaChi;
                txtDienGiai.Text = objPT.DienGiai;
                txtMatBang.Text = objPT.mbMatBang.MaSoMB;
                txtNguoiNop.Text = objPT.NguoiNop;
                if (objPT.NgayThu != null)
                    dateNgayThu.DateTime = objPT.NgayThu.Value;
                spinPhaiThu.EditValue = objPT.PhaiThu ?? 0;
                spinTyGia.EditValue = objPT.TyGiaUSD ?? 0;
                spinThucThu.EditValue = objPT.SoTienThanhToan ?? 0;
                ckChuyenKhoan.Checked = objPT.ChuyenKhoan.GetValueOrDefault();
                chkXuatHoaDon.Checked = objPT.IsXuatHoaDon.GetValueOrDefault();
                txtSoHoaDonVAT.Text = objPT.SoHDVAT;

            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
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