using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
using Library;

namespace KyThuat.BaoTri.report
{
    public partial class rptMauChiTiet : DevExpress.XtraReports.UI.XtraReport
    {
        public rptMauChiTiet(int MaBT)
        {
            InitializeComponent();

            using (MasterDataContext db = new MasterDataContext())
            {
                var objbaotri = db.btBaoTris.Single(p => p.MaBT == MaBT);

                lblTaiSan.Text = String.Format("{0} - {1}", objbaotri.tsTaiSanSuDung.KyHieu, objbaotri.tsTaiSanSuDung.tsLoaiTaiSan.TenLTS);
                lblTinhTrang.Text = objbaotri.tsTrangThai.TenTT;
                lblThoiGian.Text = objbaotri.NgayBT.Value.ToShortDateString();
                lblDoiTac.Text = objbaotri.tnNhaCungCap.TenNCC;
                lblHinhThuc.Text = objbaotri.btHinhThuc.TenHT;
                lblPhi.Text = objbaotri.PhiBT.Value.ToString("C");

                DataSource = db.tsTaiSanChiTiets.Where(p => p.MaTS == objbaotri.MaTS)
                    .Select(p => new
                    {
                        p.ChiTietTaiSan.TenChiTiet,
                        p.ChiTietTaiSan.DonGia,
                        p.tsTrangThai.TenTT,
                        p.NgayNhap,
                        p.DienGiai
                    });

                lblTenChiTiet.DataBindings.Add(new XRBinding("Text", DataSource, "TenChiTiet"));
                //lblTrangThai.DataBindings.Add(new XRBinding("Text", DataSource, "TenTT"));
                //lblNgay.DataBindings.Add(new XRBinding("Text", DataSource, "NgayNhap", "{0:dd/MM/yyyy}"));
                //lbldiengiaichitiet.DataBindings.Add(new XRBinding("Text", DataSource, "DienGiai"));
            }
        }

    }
}
