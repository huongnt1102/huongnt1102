using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
using Library;

namespace TaiSan.DeXuat.Report
{
    public partial class rptPhieuDeXuat : DevExpress.XtraReports.UI.XtraReport
    {
        public rptPhieuDeXuat(string MaPhieu)
        {
            InitializeComponent();

            using (MasterDataContext db = new MasterDataContext())
            {
                DateTime now = db.GetSystemDate();
                var phieu = db.dxmsDeXuats.Single(p => p.MaDX == int.Parse(MaPhieu));
                lblSoPhieu.Text = phieu.MaSoDX;
                lblDienGiai.Text = phieu.DienGiai;
                lblNgayThangNam.Text = string.Format("Ngày {0} tháng {1} năm {2}", now.Day, now.Month, now.Year);
                lblNDX.Text = lblNguoiDeXuat.Text = phieu.tnNhanVien.HoTenNV;
                lblNguoiNhan.Text = phieu.tnNhanVien1.HoTenNV;
                
                var ct = db.dxmsTaiSans.Where(p => p.MaDX == phieu.MaDX)
                    .Select(p => new
                    {
                        p.tsLoaiTaiSan.TenLTS,
                        p.SoLuong,
                        p.DienGiai,
                        p.tsLoaiTaiSan.tsLoaiTaiSan_Type.TypeNam,
                    }).ToList();
                this.DataSource = ct;

                lbltenhang.DataBindings.Add(new XRBinding("Text", DataSource, "TenLTS"));
                lblsoluong.DataBindings.Add(new XRBinding("Text", DataSource, "SoLuong"));
                lblghichu.DataBindings.Add(new XRBinding("Text", DataSource, "DienGiai"));
                lbNhomTaiSan.DataBindings.Add(new XRBinding("Text", DataSource, "TypeNam"));
            }
        }

    }
}
