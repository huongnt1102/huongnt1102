using System;
using Library;
using System.Collections.Generic;
using System.Linq;
using DevExpress.XtraReports.UI;

namespace KyThuat.SuaChua.ReportTemplate
{
    public partial class rptPhieuThanhToan : DevExpress.XtraReports.UI.XtraReport
    {
        public rptPhieuThanhToan(sckhSuaChua objsc)
        {
            InitializeComponent();
            var db = new Library.MasterDataContext();
            var ttcls = new TienTeCls();
            
            if (objsc.tnKhachHang.IsCaNhan.Value & objsc.tnKhachHang.IsCaNhan != null)
            {
                xrHoVaTen.Text = string.Format("{0} {1}", objsc.tnKhachHang.HoKH, objsc.tnKhachHang.TenKH);
            }
            else
            {
                xrHoVaTen.Text = objsc.tnKhachHang.CtyTen;
            }
            xrDiaChi.Text = String.Format("{0} - Tầng: {1}", objsc.mbMatBang.mbTangLau.mbKhoiNha.tnToaNha.TenTN, objsc.mbMatBang.mbTangLau.TenTL);
            xrLiDo.Text = "Thanh toán phí sửa chữa và phí thiết bị";
            xrSoTien.Text = string.Format("{0:#,0.#} VNĐ", objsc.PhiSC);
            xrSoTienChu.Text = ttcls.DocTienBangChu((decimal)objsc.PhiSC, "VNĐ");

            DateTime now = db.GetSystemDate();
            xrNgay.Text = String.Format("Ngày {0} tháng {1} năm {2}", now.Day, now.Month, now.Year);
            xrNgay2.Text = String.Format("Ngày {0} tháng {1} năm {2}", now.Day, now.Month, now.Year);

            this.DataSource = db.sckhThietBis.Where(p => p.MaSC == objsc.MaSC)
                .Select(p => new
                {
                    TenThietBi = p.tsLoaiTaiSan.TenLTS,
                    SoLuong = p.SoLuong,
                    HienTrang = p.sckhSuaChua.DienGiai,
                    ThanhTien = p.ThanhTien
                });
            var TongTienData = db.sckhThietBis.Where(p => p.MaSC == objsc.MaSC).Sum(p => p.ThanhTien);

            xrTenThietBi.DataBindings.Add(new XRBinding("Text", DataSource, "TenThietBi"));
            xrSoLuong.DataBindings.Add(new XRBinding("Text", DataSource, "SoLuong"));
            xrMoTaHienTuong.DataBindings.Add(new XRBinding("Text", DataSource, "HienTrang"));
            xrThanhTien.DataBindings.Add(new XRBinding("Text", DataSource, "ThanhTien", "{0:#,0.#} VNĐ"));
            xrTongTien.Text = string.Format("{0:#,0.#} VNĐ", TongTienData + objsc.PhiSC);
        }
        

    }
}
