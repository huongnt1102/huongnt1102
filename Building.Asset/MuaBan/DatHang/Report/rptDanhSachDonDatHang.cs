using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Library;
using System.Linq;
using System.Data.Linq.SqlClient;

namespace TaiSan.DatHang.Report
{
    public partial class rptDanhSachDonDatHang : DevExpress.XtraReports.UI.XtraReport
    {
        public rptDanhSachDonDatHang(DateTime TuNgay, DateTime DenNgay)
        {
            InitializeComponent();
            lblThoiGianBaoCao.Text = string.Format("Từ ngày {0} đến ngày {1}", TuNgay.ToShortDateString(), DenNgay.ToShortDateString());
            using (MasterDataContext db = new MasterDataContext())
            {
                DataSource = db.ddhDatHangs
                            .Where(p => SqlMethods.DateDiffDay(TuNgay, p.NgayDH.Value) >= 0 &
                                    SqlMethods.DateDiffDay(p.NgayDH.Value, DenNgay) >= 0)
                            .OrderByDescending(p => p.NgayDH).AsEnumerable()
                            .Select((p, index) => new
                            {
                                STT = index + 1,
                                p.MaDH,
                                p.NgayDH,
                                p.NgayGH,
                                p.MaSoDH,
                                TenTT = p.MaTT == null ? "" : p.ddhTrangThai.TenTT,
                                MauNen = p.MaTT == null ? 0 : p.ddhTrangThai.MauNen,
                                TenNCC = p.MaNCC == null ? "" : p.tnKhachHang.CtyTen,
                                p.DienGiai,
                                p.tnNhanVien.HoTenNV,
                                TongTien = p.ddhTaiSans.Where(o => o.MaDH == p.MaDH).Sum(s => s.SoLuong * s.DonGia)
                            }).ToList();

                lblSTT.DataBindings.Add(new XRBinding("Text", DataSource, "STT"));
                lblThoiGian.DataBindings.Add(new XRBinding("Text", DataSource, "NgayDH", "{0:dd/MM/yyyy}"));
                lblNgayGiao.DataBindings.Add(new XRBinding("Text", DataSource, "NgayGH", "{0:dd/MM/yyyy}"));
                lblNhaCungCap.DataBindings.Add(new XRBinding("Text", DataSource, "TenNCC"));
                lblMaDon.DataBindings.Add(new XRBinding("Text", DataSource, "MaSoDH"));
                lblDienGia.DataBindings.Add(new XRBinding("Text", DataSource, "DienGiai"));
                lblTongTien.DataBindings.Add(new XRBinding("Text", DataSource, "TongTien", "{0:#,0.#} VNÐ"));
            }
        }

    }
}
