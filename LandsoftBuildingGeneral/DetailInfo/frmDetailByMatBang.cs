using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Library;
using System.Linq;

namespace LandsoftBuildingGeneral.DetailInfo
{
    public partial class frmDetailByMatBang : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public int MaMB { get; set; }
        DateTime now;

        public frmDetailByMatBang()
        {
            InitializeComponent();
            db = new MasterDataContext();
            TranslateLanguage.TranslateControl(this);
            now = db.GetSystemDate();
        }

        private void frmDetailByMatBang_Load(object sender, EventArgs e)
        {
            var wait = DialogBox.WaitingForm();

            var obj = db.mbMatBangs.Where(p => p.MaMB == MaMB)
                .Select(p => new
                {
                    p.MaMB,
                    p.MaSoMB,
                   // p.mbLoaiMatBang.TenLMB,
                    p.DienTich,
                    p.mbTangLau.TenTL,
                    p.mbTrangThai.TenTT,
                    p.DienGiai,
                    p.SoNha
                }).ToList();
            vgcThongTinMatBang.DataSource = obj;
            txtDienGiai.Text = obj.FirstOrDefault().DienGiai;

            LoadTheXe(MaMB);
            LoadTheThangMay(MaMB);
            LoadNhanKhau(MaMB);
            LoadLichSuGiaoDich(MaMB);
            LoadChiaLo(MaMB);
            LoadCongNo(MaMB);

            CNLoadDien(MaMB);
            CNLoadDVK(MaMB);
            CNLoadNuoc(MaMB);
            CNLoadPhiQL(MaMB);
            CNLoadThangMay(MaMB);
            CNLoadTheXe(MaMB);
            CNLoadThueHopDong(MaMB);

            wait.Close();
            wait.Dispose();
        }

        #region Load Detail Data

        private void LoadLichSuGiaoDich(int MaMB)
        {
            gcLichSuGiaoDich.DataSource = db.lsGiaoDiches
                .Where(p => p.MaMB == MaMB)
                .Select(p => new
                {
                    SoHopDongLS = p.thueHopDong.SoHD,
                    KhachHangLS = p.tnKhachHang.TenKH,
                    NhanVienLS = p.tnNhanVien.HoTenNV,
                    MatBangLS = p.mbMatBang.MaSoMB,
                    TrangThaiLS = p.thueTrangThai.TenTT,
                    NgayLapLS = p.NgayLap,
                    DienGiaiLS = p.DienGiai
                }).ToList();
        }

        private void LoadTheXe(int MaMB)
        {
        }

        private void LoadTheThangMay(int MaMB)
        {
            gcTheThangMay.DataSource = db.dvtmTheThangMays.Where(p => p.MaMB==MaMB)
                .Select(tm => new
                {
                    SoTheTM = tm.SoThe,
                    ChuTheTM = tm.ChuThe,
                    NgayDangKyTM = tm.NgayDK,
                    DaThanhToanTM = (bool)tm.DaTT ? "Ðã thanh toán" : "Chưa thanh toán",
                    MatBangTM = tm.mbMatBang.MaSoMB,
                    NhanVienLamTheTM = tm.tnNhanVien.HoTenNV,
                    DienGiaiTM = tm.DienGiai,
                    PhilamTheTM = tm.PhiLamThe
                }).ToList();
        }

        private void LoadNhanKhau(int MaMB)
        {
            gcNhanKhau.DataSource = db.tnNhanKhaus
                .Where(p => p.MaMB==MaMB)
                .Select(p => new
                {
                    HoTenNK = p.HoTenNK,
                    GioiTinhNK = (bool)p.GioiTinh ? "Nam" : "Nữ?",
                    NgaySinhNK = p.NgaySinh,
                    CMNKNK = p.CMND,
                    CMNDNgayCapNK = p.NgayCap,
                    CMNDNoiCapNK = p.NoiCap,
                    DiaChiTT = p.DCTT,
                    DienThoaiNK = p.DienThoai,
                    EmailNK = p.Email,
                    DienGiaiNK = p.DienGiai,
                    DaDangKyTT = (bool)p.DaDKTT ? "Ðã đăng ký" : "Chưa đăng ký",
                    NhanVienNK = p.tnNhanVien.HoTenNV
                }).ToList();
        }

        private void LoadChiaLo(int MaMB)
        {
            gcChiaLo.DataSource = db.mbMatBang_ChiaLos.Where(p => p.MaMB == MaMB)
                .Select(p => new
                {
                    p.GiaThue,
                    p.mbTrangThai.TenTT,
                    p.TenLo,
                    p.DienGiai,
                    p.DienTich,
                    KhachHang = p.MaKH.HasValue ? (p.tnKhachHang.IsCaNhan.HasValue ? (p.tnKhachHang.IsCaNhan.Value ? p.tnKhachHang.HoKH + " " + p.tnKhachHang.TenKH : p.tnKhachHang.CtyTen) : "") : "",
                });
        }

        private void LoadCongNo(int maMB)
        {
            gcDichVu.DataSource = db.PaidMulti(maMB);
        }

        #endregion

        #region Cong No chi tiet

        private void CNLoadThueHopDong(int MaMB)
        {
            gcThueHopDong.DataSource = db.thueCongNos
                    .Where(p => p.ConNo > 0 & p.ChuKyMin <= db.GetSystemDate() & p.thueHopDong.mbMatBang.MaMB == MaMB)
                    .OrderBy(p => p.ChuKyMin)
                    .Select(p => new
                    {
                        p.MaCN,
                        p.thueHopDong.SoHD,
                        KhachHang = p.thueHopDong.tnKhachHang.IsCaNhan.Value ? string.Format("{0} {1}", p.thueHopDong.tnKhachHang.HoKH, p.thueHopDong.tnKhachHang.TenKH) : p.thueHopDong.tnKhachHang.CtyTen,
                        DiaChi = p.thueHopDong.tnKhachHang.DCLL,
                        p.ConNo,
                        ThangThanhToan = string.Format("{0}/{1}", p.ChuKyMax.Value.Month, p.ChuKyMax.Value.Year),
                        p.dvTrangThaiNhacNo.TenTT,
                        p.dvTrangThaiNhacNo.MauNen
                    });
        }

        private void CNLoadDVK(int MaMB)
        {
        }

        private void CNLoadThangMay(int MaMB)
        {
            gcThangMay.DataSource = db.dvtmThanhToanThangMays
                    .Where(p => p.DaTT == false & p.ThangThanhToan <= db.GetSystemDate() & p.dvtmTheThangMay.mbMatBang.MaMB == MaMB)
                    .OrderBy(p => p.ThangThanhToan)
                    .Select(p => new
                    {
                        p.ThanhToanID,
                        p.dvtmTheThangMay.SoThe,
                        p.dvtmTheThangMay.ChuThe,
                        KhachHang = p.dvtmTheThangMay.tnKhachHang.IsCaNhan.Value ? string.Format("{0} {1}", p.dvtmTheThangMay.tnKhachHang.HoKH, p.dvtmTheThangMay.tnKhachHang.TenKH) : p.dvtmTheThangMay.tnKhachHang.CtyTen,
                        DiaChi =  p.dvtmTheThangMay.tnKhachHang.DCLL,
                        p.dvtmTheThangMay.PhiLamThe,
                        ThangThanhToan = string.Format("{0}/{1}", p.ThangThanhToan.Value.Month, p.ThangThanhToan.Value.Year),
                        p.dvTrangThaiNhacNo.TenTT,
                        p.dvTrangThaiNhacNo.MauNen
                    });
        }

        private void CNLoadTheXe(int MaMB)
        {
        }

        private void CNLoadNuoc(int MaMB)
        {
            gcNuoc.DataSource = db.dvdnNuocs
                    .Where(p => p.DaTT == false & p.NgayNhap <= db.GetSystemDate() & p.mbMatBang.MaMB == MaMB)
                    .OrderBy(p => p.NgayNhap)
                    .Select(p => new
                    {
                        p.ID,
                        p.mbMatBang.MaSoMB,
                        KhachHang = p.tnKhachHang.IsCaNhan.Value ? string.Format("{0} {1}", p.tnKhachHang.HoKH, p.tnKhachHang.TenKH) : p.tnKhachHang.CtyTen,
                        DiaChi = p.tnKhachHang.DCLL,
                        p.SoTieuThu,
                        p.TienNuoc,
                        p.NgayNhap,
                        ThangThanhToan = string.Format("{0}/{1}", p.NgayNhap.Value.Month - 1, p.NgayNhap.Value.Year),
                        p.dvTrangThaiNhacNo.TenTT,
                        p.dvTrangThaiNhacNo.MauNen
                    });
        }

        private void CNLoadDien(int MaMB)
        {
            gcDien.DataSource = db.dvdnDiens
                    .Where(p => p.DaTT == false & p.NgayNhap <= db.GetSystemDate() & p.mbMatBang.MaMB == MaMB)
                    .OrderBy(p => p.NgayNhap)
                    .Select(p => new
                    {
                        p.ID,
                        p.mbMatBang.MaSoMB,
                        KhachHang = p.tnKhachHang.IsCaNhan.Value ? string.Format("{0} {1}", p.tnKhachHang.HoKH, p.tnKhachHang.TenKH) : p.tnKhachHang.CtyTen,
                        DiaChi = p.tnKhachHang.DCLL,
                        p.SoTieuThu,
                        p.TienDien,
                        p.NgayNhap,
                        ThangThanhToan = string.Format("{0}/{1}", p.NgayNhap.Value.Month - 1, p.NgayNhap.Value.Year),
                        p.dvTrangThaiNhacNo.TenTT,
                        p.dvTrangThaiNhacNo.MauNen
                    });
        }

        private void CNLoadPhiQL(int MaMB)
        {
            var souce = db.mbMatBangs.Where(p => p.MaKH != null & p.MaMB == MaMB)
                    .Select(p => new
                    {
                        TenKH = (bool)p.tnKhachHang.IsCaNhan ? String.Format("{0} {1}", p.tnKhachHang.HoKH, p.tnKhachHang.TenKH) : p.tnKhachHang.CtyTen,
                        p.MaMB,
                        p.MaSoMB,
                        PhiQL = getPhiQL(p.MaMB),
                        TrangThai = DaThanhToanPQL(p.MaMB),
                        p.DienGiai,
                        //p.mbLoaiMatBang.TenLMB,
                        p.MaKH
                    }).ToList();
            gcMatBang.DataSource = souce.Where(p => p.PhiQL > 0 & !p.TrangThai).ToList();
        }

        #endregion

        private bool DaThanhToanPQL(int MaMB)
        {
            bool result = false;
            var PhiQLDaThu = db.PhiQuanLies.Where(p => p.mbMatBang.MaMB == MaMB & p.ThangThanhToan.Value.Month == now.Month & p.ThangThanhToan.Value.Year == now.Year);
            if (PhiQLDaThu.Count() <= 0)
                result = false;
            else
                result = true;

            return result;
        }

        private decimal getPhiQL(int MaMB)
        {
            var pqlhdt = db.thueHopDongs.Where(p => p.MaMB == MaMB).Select(p => p.PhiQL).FirstOrDefault() ?? 0;

            return  pqlhdt;
        }
    }
}