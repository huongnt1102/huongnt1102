using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
using Library;
using System.Data.Linq.SqlClient;

namespace LandSoftBuilding.Receivables.GiayBao
{
    public partial class rptPQLTTC : DevExpress.XtraReports.UI.XtraReport
    {
        public rptPQLTTC(byte _MaTN, int _MaKH, int _Thang, int _Nam, int _MaLDV)
        {
            InitializeComponent();

         //   Library.frmPrintControl.LoadLayout(this, 5, _MaTN);

            cSTT.DataBindings.Add("Text", null, "STT");
            cSTT.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.RecordNumber, "{0:#}");

            cSoLuong.DataBindings.Add("Text", null, "SoLuong", "{0:#,0.##}");
           
            cDonGia.DataBindings.Add("Text", null, "DonGia", "{0:#,0.##}");
            cThanhTien.DataBindings.Add("Text", null, "ThanhTien", "{0:#,0.##}");

         
            var db = new MasterDataContext();
            try
            {
               var tam = (from hd in db.dvHoaDons
                                   join dv in db.dvDichVuKhacs on new { hd.MaLDV, hd.LinkID } equals new { MaLDV = (int?)13, LinkID = (int?)dv.ID }
                                   join dvt in db.DonViTinhs on dv.MaDVT equals dvt.ID
                                   where hd.MaTN == _MaTN & hd.MaLDV == _MaLDV & hd.MaKH == _MaKH & hd.NgayTT.Value.Month == _Thang & hd.NgayTT.Value.Year == _Nam
                                    & hd.ConNo.GetValueOrDefault() > 0
                          select new
                                   {
                                       dv.SoLuong,
                                       dvt.TenDVT,
                                       DonGia = dv.DonGia * dv.TyGia,
                                       ThanhTien = (hd.PhaiThu.GetValueOrDefault() - db.SoQuy_ThuChis.Where(sq => sq.LinkID == hd.ID && sq.TableName == "dvHoaDon").Sum(s => s.DaThu + s.KhauTru).GetValueOrDefault()
                    ),
                                   }).ToList();
                this.DataSource = tam;
                var NgayTB = new DateTime((int)_Nam, (int)_Thang, 1);
                var _NoCu = (from hd in db.dvHoaDons
                          
                             where hd.MaKH == _MaKH
                              & SqlMethods.DateDiffDay(hd.NgayTT, NgayTB) > 0
                             & (hd.PhaiThu.GetValueOrDefault()

                            - (from ct in db.SoQuy_ThuChis
                               where ct.TableName == "dvHoaDon" & ct.LinkID == hd.ID
                               & SqlMethods.DateDiffDay(ct.NgayPhieu, NgayTB) > 0
                               select ct.DaThu+ct.KhauTru).Sum().GetValueOrDefault()) > 0
                                  & (hd.MaLDV == 13 || hd.MaLDV == 69)
                             select (hd.PhaiThu
                             - (from ct in db.SoQuy_ThuChis
                                where ct.TableName == "dvHoaDon" & ct.LinkID == hd.ID
                                & SqlMethods.DateDiffDay(ct.NgayPhieu, NgayTB) > 0
                                select ct.DaThu+ct.KhauTru).Sum().GetValueOrDefault()
                             )).Sum().GetValueOrDefault();
                cNoCu.Text = string.Format("{0:#,0.##}", _NoCu);
                csumThanhTien.Text = string.Format("{0:#,0.##}", _NoCu + tam.Sum(p=>p.ThanhTien));
            }
            catch { }
            finally
            {
                db.Dispose();
            }
        }
    }
}
