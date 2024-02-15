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
    public partial class rptTienNuocHoaPhat : DevExpress.XtraReports.UI.XtraReport
    {
        public rptTienNuocHoaPhat(byte _MaTN, int _MaKH, int _Thang, int _Nam)
        {
            InitializeComponent();

            //Library.frmPrintControl.LoadLayout(this, 9, _MaTN);
            var Ngay = new DateTime(_Nam, _Thang, 1);
            #region Bingding
            //cSTT.DataBindings.Add("Text", null, "STT");
            //cSTT.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.RecordNumber, "{0:#}");
            cTenDM.DataBindings.Add("Text", null, "TenDM");
            cSoLuong.DataBindings.Add("Text", null, "SoLuong", "{0:#,0.##}");
            cDonGia.DataBindings.Add("Text", null, "DonGia", "{0:#,0.##}");
            cThanhTien.DataBindings.Add("Text", null, "ThanhTien", "{0:#,0.##}");
           
            
            #endregion

            var db = new MasterDataContext();
            try
            {
                var NoNuoc = (from

                                  dv in db.dvHoaDons
                              where dv.MaTN == _MaTN & dv.MaKH == _MaKH & dv.MaLDV == 9 //& dv.NgayTT.Value.Month == _Thang 
                                  //& dv.NgayTT.Value.Year <= _Nam
                                  // & dv.NgayTT.Value.Month != _Thang 
                              & dv.NgayTT.Value.Year <= _Nam
                              & SqlMethods.DateDiffDay(dv.NgayTT, Ngay) > 0
                              & dv.IsDuyet == true
                              &
                              (dv.PhaiThu.GetValueOrDefault() -
                              (db.ptChiTietPhieuThus.Where(p => p.LinkID == dv.ID & p.TableName == "dvHoaDon").Sum(p => p.SoTien)).GetValueOrDefault())
                              > 0

                              select new
                              {

                                  ConNo = Math.Round((decimal)dv.PhaiThu.GetValueOrDefault(), MidpointRounding.AwayFromZero)
                                  -
                              (db.ptChiTietPhieuThus.Where(p => p.LinkID == dv.ID & p.TableName == "dvHoaDon").Sum(p => p.SoTien)).GetValueOrDefault()
                                  ,

                              }).ToList();
                var objNuoc = (from hd in db.dvHoaDons
                               join tn in db.dvNuocs on new { hd.TableName, hd.LinkID } equals new { TableName = "dvNuoc", LinkID = (int?)tn.ID }
                               where hd.MaTN == _MaTN & hd.MaKH == _MaKH & hd.NgayTT.Value.Month == _Thang & hd.NgayTT.Value.Year == _Nam
                               select new
                               {
                                   tn.ID,
                                   tn.ChiSoCu,
                                   tn.ChiSoMoi,
                                   TienBVMT = Math.Round((decimal)tn.TienBVMT.GetValueOrDefault(),MidpointRounding.AwayFromZero),
                                   TienVAT=Math.Round((decimal)tn.TienVAT.GetValueOrDefault(),MidpointRounding.AwayFromZero),
                                   tn.SoTieuThu,
                                   tn.ThanhTien,
                                   SoUuDai = (from ud in db.dvNuocUuDais where ud.MaMB == tn.MaMB select ud.SoNguoi).FirstOrDefault(),
                                   tn.TienTT,
                                    ConNo=Math.Round((decimal)hd.ConNo,MidpointRounding.AwayFromZero)
                               }).FirstOrDefault();
                if (objNuoc != null)
                {
                    cBVMT.Text = string.Format("{0:#,0.##}", objNuoc.TienBVMT);

                    cVAT.Text = string.Format("{0:#,0.##}", objNuoc.TienVAT);
                    cTongTien
               .Text = string.Format("{0:#,0.##}", objNuoc.TienTT);
                    cChiSoDau.Text = string.Format("{0:#,0.##}", objNuoc.ChiSoCu);
                    cChiSoCuoi.Text = string.Format("{0:#,0.##}", objNuoc.ChiSoMoi);
                    cSoTieuThu.Text = string.Format("{0:#,0.##}", objNuoc.SoTieuThu);
                    cTienNuoc.Text = string.Format("{0:#,0.##}", objNuoc.ThanhTien.GetValueOrDefault());
                    this.DataSource = (from ct in db.dvNuocChiTiets
                                       join dm in db.dvNuocDinhMucs on ct.MaDM equals dm.ID
                                       where ct.MaNuoc == objNuoc.ID
                                       orderby dm.STT
                                       select new
                                       {
                                           dm.TenDM,
                                           ct.SoLuong,
                                           ct.DonGia,
                                           ThanhTien = Math.Round((decimal)ct.ThanhTien, MidpointRounding.AwayFromZero)
                                       }).ToList();
                }
                else
                {
                    var objNuocNon = (from 
                                   tn in db.dvNuocs 
                                   where tn.MaTN == _MaTN & tn.MaKH == _MaKH & tn.NgayTT.Value.Month == _Thang & tn.NgayTT.Value.Year == _Nam
                                   & tn.SoTieuThu==0
                                   select new
                                   {
                                       tn.ID,
                                       tn.ChiSoCu,
                                       tn.ChiSoMoi,
                                       TienBVMT = Math.Round((decimal)tn.TienBVMT.GetValueOrDefault(), MidpointRounding.AwayFromZero),
                                       TienVAT = Math.Round((decimal)tn.TienVAT.GetValueOrDefault(), MidpointRounding.AwayFromZero),
                                       tn.SoTieuThu,
                                       tn.ThanhTien,
                                       SoUuDai = (from ud in db.dvNuocUuDais where ud.MaMB == tn.MaMB select ud.SoNguoi).FirstOrDefault(),
                                       tn.TienTT,
                                       //ConNo = Math.Round((decimal)hd.ConNo, MidpointRounding.AwayFromZero)
                                   }).FirstOrDefault();
                    cBVMT.Text = string.Format("{0:#,0.##}", objNuocNon.TienBVMT);

                    cVAT.Text = string.Format("{0:#,0.##}", objNuocNon.TienVAT);
                    cTongTien
               .Text = string.Format("{0:#,0.##}", objNuocNon.TienTT);
                    cChiSoDau.Text = string.Format("{0:#,0.##}", objNuocNon.ChiSoCu);
                    cChiSoCuoi.Text = string.Format("{0:#,0.##}", objNuocNon.ChiSoMoi);
                    cSoTieuThu.Text = string.Format("{0:#,0.##}", objNuocNon.SoTieuThu);
                    cTienNuoc.Text = string.Format("{0:#,0.##}", objNuocNon.ThanhTien.GetValueOrDefault());
                }
              
               
             
                
                
               
                cTongNo.Text = string.Format("{0:#,0.##}", NoNuoc.Sum(p=>p.ConNo));
                var tam = objNuoc == null ? 0 : objNuoc.ConNo;
                cThanhToan.Text = string.Format("{0:#,0.##}", NoNuoc.Sum(p => p.ConNo) + tam);
            }
            catch { }
            finally
            {
                db.Dispose();
            }
        }
    }
}
