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

namespace DichVu.PhiVeSinh
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
        PhieuThu objPT;
        ptChiTiet objPTCT;

        MasterDataContext db;
        DateTime now;
        private System.Collections.Generic.List<Library.Class.HuongDan.ShowAuto.ControlItem> controls;

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
                DialogBox.Alert("Vui lòng nhập [Số phiếu thu], xin cảm ơn.");  
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

            if (MaPhieu == 0)
            {
                objPT = new PhieuThu();
                objPT.NgayNhap = now;
                #region Detail
                objPTCT = new ptChiTiet();
                objPT.ptChiTiets.Add(objPTCT);
                objPTCT.ChietKhau = 0;
                objPTCT.DienGiai = txtDienGiai.Text;
                objPTCT.MaLDV = 13;
                objPTCT.MaMB = MaMB;
                objPTCT.MaKH = MaKH;
                if (dateDotTT.DateTime.Year != 1)
                    objPTCT.NgayThu = dateDotTT.DateTime;
                objPTCT.PhaiThu = spinThucThu.Value;
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
                objPTCT.MaLDV = 13;
                objPTCT.MaMB = MaMB;
                if (dateDotTT.DateTime.Year != 1)
                    objPTCT.NgayThu = dateDotTT.DateTime;
                objPTCT.PhaiThu = spinThucThu.Value;
                objPTCT.TongCong = spinThucThu.Value;
            }

            objPT.SoPhieu = txtSoPhieu.Text.Trim();
            objPT.DiaChi = txtDiaChi.Text.Trim();
            objPT.NguoiNop = txtNguoiNop.Text.Trim();
            objPT.DichVu = 13;
            objPT.DienGiai = txtDienGiai.Text.Trim();
            if (dateDotTT.DateTime.Year != 1)
                objPT.DotThanhToan = dateDotTT.DateTime;
            objPT.MaHopDong = MaMB.ToString();
            objPT.MaNV = objnhanvien.MaNV;
            if (dateNgayThu.DateTime.Year != 1)
                objPT.NgayThu = dateNgayThu.DateTime;
            objPT.SoTienThanhToan = spinThucThu.Value;
            objPT.SoThangThuPhiQuanLy = Convert.ToInt32(spinSoThang.Value.ToString());
            objPT.SoTienChietKhauPhiQL = 0;
            objPT.PhaiThu = spinThucThu.Value;
            objPT.PhiQuanLy = spinPhiQuanLy.Value;
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
                    //ReportMisc.DichVu.Quy.rptPhieuThu rpt = new ReportMisc.DichVu.Quy.rptPhieuThu(objPT.MaPhieu, "no");  
                    //rpt.ShowPreviewDialog();
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
            //controls = Library.Class.HuongDan.ShowAuto.GetControlItemsAutoSave(this.Controls);
            controls = Library.Class.HuongDan.ShowAuto.GetControlItemsAutoTag(this.Controls);
            Library.Class.HuongDan.ShowAuto.ClearText(this.Controls);

            try
            {
                if (MaPhieu == 0)
                {
                    now = db.GetSystemDate();
                    txtSoPhieu.Text = CreateKeyword(now);
                    var objmb = db.mbMatBangs.Single(p => p.MaMB == MaMB);
                    txtMatBang.Text = objmb.MaSoMB;
                    //Dien ten mac dinh
                    try
                    {
                        txtNguoiNop.Text = objmb.tnKhachHang.IsCaNhan.Value ? string.Format("{0} {1}", objmb.tnKhachHang.HoKH, objmb.tnKhachHang.TenKH) : objmb.tnKhachHang.CtyTen;
                        txtDiaChi.Text = objmb.tnKhachHang.DCLL;
                        txtDienGiai.Text = string.Format("Thanh toán phí vệ sinh cho mặt bằng {0}", objmb.MaSoMB);
                    }
                    catch { }
                    dateNgayThu.DateTime = now;
                    dateDotTT.EditValue = NgayNhap;
                    //spinPhiQuanLy.EditValue = objmb.PhiVeSinh ?? 0;
                    //spinSoTien.EditValue = objmb.PhiVeSinh ?? 0;
                    //spinThucThu.EditValue = objmb.PhiVeSinh ?? 0;
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
                    spinPhiQuanLy.EditValue = 0;
                    spinSoThang.EditValue = objPT.SoThangThuPhiQuanLy ?? 0;
                    spinSoTien.EditValue = objPT.SoThangThuPhiQuanLy;
                    spinThucThu.EditValue = objPT.SoTienThanhToan ?? 0;
                    ckChuyenKhoan.Checked = objPT.ChuyenKhoan.GetValueOrDefault();
                }
            }
            catch { }

            

            itemHuongDan.Click += ItemHuongDan_Click;
        }

        private void ItemHuongDan_Click(object sender, EventArgs e)
        {
            Library.Class.HuongDan.ShowAuto.ActiveDemo(true, controls);
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void spinSoThang_EditValueChanged(object sender, EventArgs e)
        {
            spinSoTien.EditValue = spinPhiQuanLy.Value * spinSoThang.Value;            
        }

        private void spinSoTien_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}