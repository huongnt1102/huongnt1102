using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
using Library;
using System.Data.Linq.SqlClient;

namespace LandSoftBuilding.Receivables
{
    public partial class rptGiayBaoChiTiet : DevExpress.XtraReports.UI.XtraReport
    {
        public rptGiayBaoChiTiet(byte _MaTN, int _Thang, int _Nam, int _MaKH)
        {
            InitializeComponent();

            GroupHeader1.GroupFields.Add(new GroupField("MaLDV"));

            cGroupTenLDV.DataBindings.Add("Text", null, "TenLDV");
            cGroupSoTien.DataBindings.Add("Text", null, "SoTien");
            cGroupSoTien.Summary = new XRSummary(SummaryRunning.Group, SummaryFunc.Sum, "{0:#,0.##}");

            cSTT.DataBindings.Add("Text", null, "NgayTT");
            cSTT.Summary = new XRSummary(SummaryRunning.Group, SummaryFunc.RecordNumber, "{0:#}");
            cNgayTT.DataBindings.Add("Text", null, "NgayTT", "{0:dd/MM/yyyy}");
            cDienGiai.DataBindings.Add("Text", null, "DienGiai");
            cSoTien.DataBindings.Add("Text", null, "SoTien", "{0:#,0.##}");

            cSumSoTien.DataBindings.Add("Text", null, "SoTien", "{0:#,0.##}");
            cSumSoTien.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,0.##}");

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

                var ngay = Common.GetLastDayOfMonth(_Thang, _Nam);
                var ltData = (from hd in db.dvHoaDons
                              join ldv in db.dvLoaiDichVus on hd.MaLDV equals ldv.ID
                              where hd.MaKH == _MaKH & SqlMethods.DateDiffDay(hd.NgayTT, ngay) >= 0 & hd.ConNo.GetValueOrDefault() > 0
                              orderby hd.NgayTT descending
                              select new { hd.MaLDV, TenLDV = ldv.TenHienThi, hd.NgayTT, hd.DienGiai, SoTien = hd.ConNo })
                              .ToList();

                this.DataSource = ltData;

                var _PhatSinh = ltData.Sum(p => p.SoTien).GetValueOrDefault();

                var _Ngay = new DateTime(_Nam, _Thang, 1);
                var _NoCu = (from hd in db.dvHoaDons
                             where hd.MaKH == _MaKH & hd.ConNo.GetValueOrDefault() > 0 & SqlMethods.DateDiffDay(hd.NgayTT, _Ngay) > 0
                             select hd.ConNo).Sum().GetValueOrDefault();

                var _TongTien = Convert.ToInt64(Math.Round(_PhatSinh + _NoCu, 0));

                cSoTienBangChu.Text = new TienTeCls().DocTienBangChu(_TongTien);
            }
            catch { }
            finally
            {
                db.Dispose();
            }
        }

        int _IndexGroup = 0;
        private void cGroupSTT_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            _IndexGroup++;
            cGroupSTT.Text = RomanNumerals.ToRoman(_IndexGroup);
        }
    }
}
