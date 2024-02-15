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

namespace DichVu.PhiBaoTri
{
    public partial class frmPaid : DevExpress.XtraEditors.XtraForm
    {
        public tnNhanVien objnhanvien;
        public int MaMB;
        public int? MaKH;
        string sSoPhieu;
        public DateTime? date;
        public decimal soTien = 0;
        public int MaPhieu = 0;
        public string MaSoMB = "";
        public DateTime? NgayNhap;
        public byte MaTN = 0;
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
                objPTCT.MaLDV = 16;
                objPTCT.MaMB = MaMB;
                if (dateDotTT.DateTime.Year != 1)
                    objPTCT.NgayThu = dateDotTT.DateTime;
                objPTCT.PhaiThu = spinPhaiThu.Value;
                objPTCT.TongCong = spinThucThu.Value;
                #endregion

                objPT.MaNV = objnhanvien.MaNV;
            }
            else
            {
                objPT.MaNVCN = objnhanvien.MaNV;
                objPT.NgayCN = now;

                objPTCT = objPT.ptChiTiets.FirstOrDefault();
                objPTCT.ChietKhau = 0;
                objPTCT.DienGiai = txtDienGiai.Text;
                objPTCT.MaLDV = 16;
                objPTCT.MaMB = MaMB;
                if (dateDotTT.DateTime.Year != 1)
                    objPTCT.NgayThu = dateDotTT.DateTime;
                objPTCT.PhaiThu = spinPhaiThu.Value;
                objPTCT.TongCong = spinThucThu.Value;
            }

            objPT.SoPhieu = txtSoPhieu.Text.Trim();
            objPT.DiaChi = txtDiaChi.Text.Trim();
            objPT.NguoiNop = txtNguoiNop.Text.Trim();
            objPT.DichVu = 16;
            objPT.DienGiai = txtDienGiai.Text.Trim();
            if (dateDotTT.DateTime.Year != 1)
                objPT.DotThanhToan = dateDotTT.DateTime;
            objPT.MaHopDong = MaMB.ToString();
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
                if (MaPhieu == 0)
                    db.PhieuThus.InsertOnSubmit(objPT);

                db.SubmitChanges();
                if (DialogBox.Question("Bạn có muốn in phiếu thu không?") == DialogResult.Yes)
                {
                    if (ckChuyenKhoan.Checked)
                    {
                        //var rpt = new ReportMisc.DichVu.Quy.Sacomreal.rptPhieuThuTienChuyenKhoan(objPT.MaPhieu, MaTN, objnhanvien.HoTenNV);
                        //rpt.ShowPreviewDialog();
                    }
                    else
                    {
                        //var rpt = new ReportMisc.DichVu.Quy.rptPhieuThuV2(objPT.MaPhieu);
                        //rpt.ShowPreviewDialog();
                    }
                }
                if (DialogBox.Question("Bạn có muốn in hóa đơn không?") == DialogResult.Yes)
                {
                    //ReportMisc.DichVu.Quy.rptHoaDonHDTH rpt = new ReportMisc.DichVu.Quy.rptHoaDonHDTH(objPT);
                    //rpt.ShowPreviewDialog();
                }
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
            this.Text = "Thanh toán phí bảo trì";
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
                    txtDienGiai.Text = string.Format("Thanh toán dịch vụ Nước cho mặt bằng {0}", objmb.MaSoMB);
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