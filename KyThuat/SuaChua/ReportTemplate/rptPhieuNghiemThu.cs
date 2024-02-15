using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Library;
using System.Linq;

namespace KyThuat.SuaChua.ReportTemplate
{
    public partial class rptPhieuNghiemThu : DevExpress.XtraReports.UI.XtraReport
    {
        public rptPhieuNghiemThu(sckhSuaChua objsuachua)
        {
            InitializeComponent();
            if (objsuachua == null)
            {
                DialogBox.Error("Không nhận được tham số thích hợp");
                this.ClosePreview();
            }
            MasterDataContext db = new MasterDataContext();
            DateTime now = db.GetSystemDate();
            this.DataSource = db.btDauMucCongViec_ThietBis.Where(p => p.MaCVBT == objsuachua.MaDMCV)
                .Select(p => new
                {
                    TenThietBi = p.tsLoaiTaiSan.TenLTS,
                    p.DonGia,
                    SoLuong = p.SoLuong,
                    HienTrang = p.DienGiai,
                    ThanhTien = p.ThanhTien
                });
            var TongTienData = db.sckhThietBis.Where(p => p.MaSC == objsuachua.MaSC).Sum(p => p.ThanhTien);
            xrTenDoanhNghiep.Text = Library.Properties.Settings.Default.TenCongTy;
            xrDiaChiBenA.Text = Library.Properties.Settings.Default.DiaChiCongTy;
            xrDienThoaiBenA.Text = Library.Properties.Settings.Default.DienThoaiCongTy;

            xrTenKhachHang.Text = objsuachua.tnKhachHang.IsCaNhan.Value ? string.Format("{0} {1}", objsuachua.tnKhachHang.HoKH, objsuachua.tnKhachHang.TenKH) : objsuachua.tnKhachHang.CtyTen;
            xrDiaChiKhachHang.Text = objsuachua.tnKhachHang.DCLL ;
            xrDienThoai.Text = objsuachua.tnKhachHang.DienThoaiKH ?? "";
            xrMatBang.Text = objsuachua.mbMatBang.MaSoMB ?? "";
            xrNgayThang.Text = String.Format("Ngày {0} tháng {1} năm {2}", now.Day, now.Month, now.Year);
            xrNhanVien.Text = objsuachua.tnNhanVien.HoTenNV ?? "";
            xrChuVu.Text = objsuachua.tnNhanVien.tnChucVu == null ? "" : objsuachua.tnNhanVien.tnChucVu.TenCV;
            xrTongTien.Text = string.Format("{0:#,0.#} VNĐ", TongTienData + objsuachua.PhiSC);
            xrPhiSC.Text = string.Format("{0:#,0.#} VNĐ", objsuachua.PhiSC);

            xrTenThietBi.DataBindings.Add(new XRBinding("Text", DataSource, "TenThietBi"));
            xrSoLuong.DataBindings.Add(new XRBinding("Text", DataSource, "SoLuong"));
            xrMoTaHienTuong.DataBindings.Add(new XRBinding("Text", DataSource, "HienTrang"));
            xrThanhTien.DataBindings.Add(new XRBinding("Text", DataSource, "ThanhTien", "{0:#,0.#} VNĐ"));
            xrSoLuong.DataBindings.Add(new XRBinding("Text", DataSource,"SoLuong"));
        }
    }
}
