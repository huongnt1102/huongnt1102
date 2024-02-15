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

namespace DichVu.HoBoi
{
    public partial class frmPaid : DevExpress.XtraEditors.XtraForm
    {
        public tnNhanVien objnhanvien;
        public dvdnDien objDien;
        public int MaMB;
        public int? MaKH;
        string sSoPhieu;
        public decimal soTien = 0;
        public int MaPhieu = 0;
        public string MaSoMB = "";
        public DateTime? NgayNhap;
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

            if (MaPhieu == 0)
            {
                objPT = new PhieuThu();
                objPT.NgayNhap = now;
                #region Detail
                objPTCT = new ptChiTiet();
                objPT.ptChiTiets.Add(objPTCT);
                objPTCT.ChietKhau = 0;
                objPTCT.DienGiai = txtDienGiai.Text;
                objPTCT.MaLDV = 15;
                objPTCT.MaMB = MaMB;
                objPTCT.MaKH = MaKH;
                if (dateDotTT.DateTime.Year != 1)
                    objPTCT.NgayThu = dateDotTT.DateTime;
                objPTCT.PhaiThu = spinPhaiThu.Value;
                objPTCT.TongCong = spinThucThu.Value;
                #endregion

                db.PhieuThus.InsertOnSubmit(objPT);
                objPT.MaNV = objnhanvien.MaNV;
                
            }
            else
            {
                objPT.MaNVCN = objnhanvien.MaNV;
                objPT.NgayCN = now;

                objPTCT = objPT.ptChiTiets.FirstOrDefault();
                objPTCT.ChietKhau = 0;
                objPTCT.DienGiai = txtDienGiai.Text;
                objPTCT.MaLDV = 15;
                objPTCT.MaMB = MaMB;
                if (dateDotTT.DateTime.Year != 1)
                    objPTCT.NgayThu = dateDotTT.DateTime;
                objPTCT.PhaiThu = spinPhaiThu.Value;
                objPTCT.TongCong = spinThucThu.Value;
            }

            int count = 0;
        doo:

            objPT.SoPhieu = txtSoPhieu.Text.Trim();
            objPT.DiaChi = txtDiaChi.Text.Trim();
            objPT.NguoiNop = txtNguoiNop.Text.Trim();
            objPT.DichVu = 15;
            objPT.DienGiai = txtDienGiai.Text.Trim();
            if (dateDotTT.DateTime.Year != 1)
                objPT.DotThanhToan = dateDotTT.DateTime;
            objPT.MaHopDong = MaMB.ToString();
            objPT.MaNV = objnhanvien.MaNV;
            if (dateNgayThu.DateTime.Year != 1)
                objPT.NgayThu = dateNgayThu.DateTime;
            objPT.SoTienThanhToan = spinThucThu.Value;
            objPT.SoThangThuPhiQuanLy = 0;
            objPT.SoTienChietKhauPhiQL = 0;
            objPT.PhaiThu = spinPhaiThu.Value;
            objPT.PhiQuanLy = 0;
            objPT.KeToanDaDuyet = false;
            objPT.MaMB = MaMB;
            objPT.CusID = MaKH;
            objPT.ChuyenKhoan = ckChuyenKhoan.Checked;            

            try
            {

                try
                {
                    db.SubmitChanges();
                }
                catch (Exception ex)
                {
                            DialogBox.Error("Không lưu được dữ liệu, vui lòng thử lại sau");
                }

                db.SubmitChanges();
                //if (DialogBox.Question("Bạn có muốn in phiếu thu không?") == DialogResult.Yes)
                //{
                //    ReportMisc.DichVu.Quy.rptPhieuThu rpt = new ReportMisc.DichVu.Quy.rptPhieuThu(objPT);
                //    rpt.ShowPreviewDialog();
                //}
                //if (DialogBox.Question("Bạn có muốn in hóa đơn không?") == DialogResult.Yes)
                //{
                //    ReportMisc.DichVu.Quy.rptHoaDonHDTH rpt = new ReportMisc.DichVu.Quy.rptHoaDonHDTH(objPT);
                //    rpt.ShowPreviewDialog();
                //}
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
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
                var khachhang = db.tnKhachHangs.Single(p => p.MaKH == MaKH);
                try
                {
                    txtNguoiNop.Text = khachhang.IsCaNhan.Value ? String.Format("{0} {1}", khachhang.HoKH, khachhang.TenKH) : khachhang.CtyTen;
                    txtDiaChi.Text = khachhang.DCLL;
                    txtDienGiai.Text = string.Format("Thanh toán dịch vụ Điện cho mặt bằng {0}", objmb.MaSoMB);
                }
                catch { }
                dateNgayThu.DateTime = now;
                dateDotTT.DateTime = NgayNhap.Value;
                spinPhaiThu.EditValue = soTien;
                spinThucThu.EditValue = soTien;
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
                dateDotTT.EditValue = objPT.DotThanhToan;
                spinPhaiThu.EditValue = objPT.PhaiThu ?? 0;
                spinThucThu.EditValue = objPT.SoTienThanhToan ?? 0;
                ckChuyenKhoan.Checked = objPT.ChuyenKhoan.GetValueOrDefault();
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void spinSoThang_EditValueChanged(object sender, EventArgs e)
        {
        }

        private void spinSoTien_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}