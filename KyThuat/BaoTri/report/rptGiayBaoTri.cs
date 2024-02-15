using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
using Library;

namespace KyThuat.BaoTri.report
{
    public partial class rptGiayBaoTri : DevExpress.XtraReports.UI.XtraReport
    {
        public rptGiayBaoTri(int MaBaoTri)
        {
            InitializeComponent();


            using (MasterDataContext db = new MasterDataContext())
            {
                var objbaotri = db.btBaoTris.Single(p => p.MaBT == MaBaoTri);

                lblSoPhieu.Text = objbaotri.MaSoBT;
                lblTaiSan.Text = String.Format("{0} - {1}", objbaotri.tsTaiSanSuDung.KyHieu, objbaotri.tsTaiSanSuDung.tsLoaiTaiSan.TenLTS);
                lblTinhTrang.Text = objbaotri.tsTrangThai.TenTT;
                lblThoiGian.Text = objbaotri.NgayBT.Value.ToShortDateString();
                lblDoiTac.Text = objbaotri.tnNhaCungCap.TenNCC;
                lblHinhThuc.Text = objbaotri.btHinhThuc.TenHT;
                lblPhi.Text = objbaotri.PhiBT.Value.ToString("C");

                DetailReport.DataSource = db.btThietBis.Where(p => p.MaBT == MaBaoTri)
                    .Select(p => new
                    {
                        p.tsLoaiTaiSan.TenLTS,
                        p.SoLuong,
                        p.DienGiai
                    });

                lblthietbi.DataBindings.Add(new XRBinding("Text", DetailReport.DataSource, "TenLTS"));
                lblSoLuong.DataBindings.Add(new XRBinding("Text", DetailReport.DataSource, "SoLuong"));
                lblDienGiai.DataBindings.Add(new XRBinding("Text", DetailReport.DataSource, "DienGiai"));

                DetailReport1.DataSource = db.btNhanViens.Where(p => p.MaBT == MaBaoTri)
                    .Select(p => new
                    {
                        p.tnNhanVien.MaSoNV,
                        p.tnNhanVien.HoTenNV,
                        p.DienGiai
                    });

                lblMaNV.DataBindings.Add(new XRBinding("Text", DetailReport1.DataSource, "MaSoNV"));
                lblTenNV.DataBindings.Add(new XRBinding("Text", DetailReport1.DataSource, "HoTenNV"));
                lblNSDienGiai.DataBindings.Add(new XRBinding("Text", DetailReport1.DataSource, "DienGiai"));

                DetailReport2.DataSource = db.tsTaiSanChiTiets.Where(p => p.MaTS == objbaotri.MaTS)
                    .Select(p => new
                    {
                        p.ChiTietTaiSan.TenChiTiet,
                        p.ChiTietTaiSan.DonGia,
                        p.tsTrangThai.TenTT,
                        p.NgayNhap,
                        p.DienGiai
                    });

                lblTenChiTiet.DataBindings.Add(new XRBinding("Text", DetailReport2.DataSource, "TenChiTiet"));
                lblDonGia.DataBindings.Add(new XRBinding("Text", DetailReport2.DataSource, "DonGia", "{0:#,0.#} VNĐ"));
                lblTrangThai.DataBindings.Add(new XRBinding("Text", DetailReport2.DataSource, "TenTT"));
                lblNgay.DataBindings.Add(new XRBinding("Text", DetailReport2.DataSource, "NgayNhap", "{0:dd/MM/yyyy}"));
                lbldiengiaichitiet.DataBindings.Add(new XRBinding("Text", DetailReport2.DataSource, "DienGiai"));
            }
        }

    }
}
