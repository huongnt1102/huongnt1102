﻿using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
using Library;
using System.Data.Linq.SqlClient;

namespace LandSoftBuilding.Receivables.GiayBao
{
    public partial class rptTienNuocSinhHoat : DevExpress.XtraReports.UI.XtraReport
    {
        public rptTienNuocSinhHoat(byte _MaTN, int _MaKH, int _Thang, int _Nam)
        {
            InitializeComponent();

            Library.frmPrintControl.LoadLayout(this, 11, _MaTN);

            #region Bingding
            cSTT.DataBindings.Add("Text", null, "STT");
            cSTT.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.RecordNumber, "{0:#}");
            cTenDM.DataBindings.Add("Text", null, "TenDM");
            cSoLuong.DataBindings.Add("Text", null, "SoLuong", "{0:#,0.##}");
            cDonGia.DataBindings.Add("Text", null, "DonGia", "{0:#,0.##}");
            cThanhTien.DataBindings.Add("Text", null, "ThanhTien", "{0:#,0.##}");
            cGhiChu.DataBindings.Add("Text", null, "DienGiai");
            #endregion

            var db = new MasterDataContext();
            try
            {
                var objNuoc = (from hd in db.dvHoaDons
                               join tn in db.dvNuocSinhHoats on new { hd.TableName, hd.LinkID } equals new { TableName = "dvNuocSinhHoat", LinkID = (int?)tn.ID }
                               where hd.MaTN == _MaTN & hd.MaKH == _MaKH & hd.NgayTT.Value.Month == _Thang & hd.NgayTT.Value.Year == _Nam
                               select new
                               {
                                   tn.ID,
                                   tn.ChiSoCu,
                                   tn.ChiSoMoi,
                                   tn.SoTieuThuNL,
                                   tn.DauCap_Cu,
                                   tn.DauCap_Moi,
                                   tn.DauHoi_Cu,
                                   tn.DauHoi_Moi,
                                   tn.SoTieuThuNN,
                                   tn.SoTieuThu,
                                   hd.ConNo
                               }).First();

                this.DataSource = (from ct in db.dvNuocSinhHoatChiTiets 
                                   join dm in db.dvNuocDinhMucs on ct.MaDM equals dm.ID
                                   where ct.MaNuoc == objNuoc.ID
                                   orderby dm.STT
                                   select new
                                   {
                                       dm.TenDM,
                                       ct.SoLuong,
                                       DonGia = _MaTN == 27 ? ct.DonGia * (decimal)(1.15) : ct.DonGia,
                                       ThanhTien = _MaTN == 27 ? ct.ThanhTien * (decimal)(1.15) : ct.ThanhTien,
                                       ct.DienGiai
                                   }).ToList();

                cChiSoDau.Text = string.Format("Chỉ số đầu: {0:#,0.##}", objNuoc.ChiSoCu);
                cChiSoCuoi.Text = string.Format("Chỉ số cuối: {0:#,0.##}", objNuoc.ChiSoMoi);
                cSoTieuThuNL.Text = string.Format("{0:#,0.##}", objNuoc.SoTieuThuNL);
                cDauCap_Cu.Text = string.Format("Chỉ số đầu cấp cũ: {0:#,0.##}", objNuoc.DauCap_Cu);
                cDauHoi_Cu.Text = string.Format("Chỉ số đầu hồi cũ: {0:#,0.##}", objNuoc.DauHoi_Cu);
                cDauCap_Moi.Text = string.Format("Chỉ số đầu cấp mới: {0:#,0.##}", objNuoc.DauCap_Moi);
                cDauHoi_Moi.Text = string.Format("Chỉ số đầu hồi mới: {0:#,0.##}", objNuoc.DauHoi_Moi);
                cSoTieuThuNN.Text = string.Format("{0:#,0.##}", objNuoc.SoTieuThuNN);
                cSoTieuThu.Text = string.Format("{0:#,0.##}", objNuoc.SoTieuThu);     
                cTienNuoc.Text = string.Format("{0:#,0.##}", objNuoc.ConNo);
            }
            catch { }
            finally
            {
                db.Dispose();
            }
        }
    }
}