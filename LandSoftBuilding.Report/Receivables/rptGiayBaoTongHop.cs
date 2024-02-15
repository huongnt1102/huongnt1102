using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
using System.Data.Linq.SqlClient;
using Library;

namespace LandSoftBuilding.Receivables
{
    public partial class rptGiayBaoTongHop : DevExpress.XtraReports.UI.XtraReport
    {
        public rptGiayBaoTongHop(byte _MaTN, int _Thang, int _Nam, int _MaKH)
        {
            InitializeComponent();

            cSTT.DataBindings.Add("Text", null, "TenLDV");
            cSTT.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.RecordNumber, "{0:n0}");
            cTenLDV.DataBindings.Add("Text", null, "TenLDV");
            cPhatSinh.DataBindings.Add("Text", null, "PhatSinh", "{0:#,0.##}");
            cNoCu.DataBindings.Add("Text", null, "NoCu", "{0:#,0.##}");
            cThanhTien.DataBindings.Add("Text", null, "ThanhTien", "{0:#,0.##}");

            cSumThanhTien.DataBindings.Add("Text", null, "ThanhTien", "{0:#,0.##}");
            cSumThanhTien.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,0.##}");
            cSumPhatSinh.DataBindings.Add("Text", null, "PhatSinh", "{0:#,0.##}");
            cSumPhatSinh.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,0.##}");
            cSumNoCu.DataBindings.Add("Text", null, "NoCu", "{0:#,0.##}");
            cSumNoCu.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,0.##}");

            var db = new MasterDataContext();

            try
            {
                cNgayIn.Text = string.Format("Hà Nội, Ngày {0:dd} tháng {1:MM} năm {2:yyyy}", DateTime.Now, DateTime.Now, DateTime.Now);
                cThang.Text = string.Format("(Tháng {0:00} năm {1})", _Thang, _Nam);
                cNgayNhanThongBao.Text = string.Format("Đã nhận thông báo ngày ---/{0:MM/yyyy}", DateTime.Now);

                var objTN = (from tn in db.tnToaNhas
                             where tn.MaTN == _MaTN
                             select new { tn.TenTN, tn.CongTyQuanLy, tn.ChuTaiKhoan, tn.SoTaiKhoan, tn.NganHang })
                             .FirstOrDefault();

                cTenCongTy.Text = objTN.CongTyQuanLy;
                cTenTNHeader.Text = ("BQL Dự án " + objTN.TenTN).ToUpper();
                cTenTNFooter.Text = ("T/M BQL Dự án " + objTN.TenTN).ToUpper();
                cTenTKNH.Text = objTN.ChuTaiKhoan;
                cSoTKNH.Text = objTN.SoTaiKhoan;
                cTenNH.Text = objTN.NganHang;
                cThongBaoHeader.Text = string.Format("Ban quản lý Dự án {0} kính đề nghị quý khách hàng thanh toán các khoản tiền (đã bao gồm thuế VAT) đến hết tháng {1:00} năm {2}, chi tiết như sau:",
                    objTN.TenTN, _Thang, _Nam);
                cThongBaoFooter.Text = string.Format("Ban quản lý Dự án {0} kính đề nghị quý khách hàng thanh toán các khoản tiền trên trước ngày 01/{1:00}/{2} bằng tiền mặt hoặc chuyển khoản theo tài khoản sau:",
                    objTN.TenTN, _Thang, _Nam);

                var objKH = db.tnKhachHangs.Single(p => p.MaKH == _MaKH);
                cTenKH.Text = objKH.IsCaNhan.GetValueOrDefault() ? (objKH.HoKH + " " + objKH.TenKH) : objKH.CtyTen;
                cKyHieuKH.Text = objKH.KyHieu;
                cDiaChiKH.Text = objKH.DCLL;

                var _Ngay = new DateTime(_Nam, _Thang, 1);
                var ltData = (from l in db.dvLoaiDichVus
                              join ps in
                                  (from hd in db.dvHoaDons
                                   where hd.MaKH == _MaKH & hd.ConNo.GetValueOrDefault() > 0 & hd.NgayTT.Value.Month == _Thang & hd.NgayTT.Value.Year == _Nam
                                   group hd by hd.MaLDV into gr
                                   select new { ID = gr.Key, PhatSinh = gr.Sum(p => p.ConNo) }
                                   )
                               on l.ID equals ps.ID into tblPhatSinh
                              from ps in tblPhatSinh.DefaultIfEmpty()
                              join nc in
                                  (
                                  from hd in db.dvHoaDons
                                  where hd.MaKH == _MaKH & hd.ConNo.GetValueOrDefault() > 0 & SqlMethods.DateDiffDay(hd.NgayTT, _Ngay) > 0
                                  group hd by hd.MaLDV into gr
                                  select new { ID = gr.Key, NoCu = gr.Sum(p => p.ConNo) }
                                  )
                              on l.ID equals nc.ID into tblNoCu
                              from nc in tblNoCu.DefaultIfEmpty()
                              where ps.PhatSinh.GetValueOrDefault() + nc.NoCu.GetValueOrDefault() > 0
                              select new { TenLDV = l.TenHienThi, ps.PhatSinh, nc.NoCu, ThanhTien = ps.PhatSinh.GetValueOrDefault() + nc.NoCu.GetValueOrDefault() })
                              .ToList();

                this.DataSource = ltData;

                var _TongTien = Convert.ToInt64(Math.Round(ltData.Sum(p => p.ThanhTien), 0));
                cSoTienBangChu.Text = new TienTeCls().DocTienBangChu(_TongTien);
            }
            catch { }
            finally
            {
                db.Dispose();
            }
        }
    }
}
