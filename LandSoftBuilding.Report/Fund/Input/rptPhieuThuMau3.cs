using System;
using System.Drawing;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
using Library;

namespace LandSoftBuilding.Fund.Input
{
    public partial class rptPhieuThuMau3 : DevExpress.XtraReports.UI.XtraReport
    {
        public rptPhieuThuMau3(int ID, byte MaTN)
        {
            InitializeComponent();

            Library.frmPrintControl.LoadLayout(this, 42, MaTN);

            if (ID == 0) return;

            #region DataBindings
            cSTT.DataBindings.Add("Text", null, "STT");
            cSTT.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.RecordNumber, "{0:n0}");
            cDienGiai.DataBindings.Add("Text", null, "TenLDV");
            cSoTien.DataBindings.Add("Text", null, "SoTien", "{0:n0}");
            #endregion

            var db = new Library.MasterDataContext();
            try
            {
                #region Thong tin toa nha
                var objTN = db.tnToaNhas.FirstOrDefault(p => p.MaTN == MaTN);
                cTenTN.Text = objTN.CongTyQuanLy;
                cDiaChiTN.Text = objTN.DiaChiCongTy;
                cDienThoaiTN.Text = "Tel: " + objTN.DienThoai;
                picLogo.ImageUrl = objTN.Logo;
                #endregion

                var objTien = new TienTeCls();
                var objPT = (from p in db.ptPhieuThus
                             join nv in db.tnNhanViens on p.MaNVN equals nv.MaNV
                             where p.ID == ID
                             select new
                             {
                                 p.MaTN,
                                 p.SoPT,
                                 p.NgayThu,
                                 p.NguoiNop,
                                 p.DiaChiNN,
                                 p.LyDo,
                                 p.SoTien,
                                 SoTien_BangChu = objTien.DocTienBangChu(p.SoTien.Value, "đồng chẵn"),
                                 nv.HoTenNV
                             }).FirstOrDefault();

                cSoPhieu.Text = "Số phiếu: " + objPT.SoPT;
                cNgayPT.Text = string.Format("Ngày {0:dd} tháng {0:MM} năm {0:yyyy}", objPT.NgayThu);
                cNguoiNop.Text = objPT.NguoiNop;
                cDiaChi.Text = objPT.DiaChiNN;
                cLyDo.Text = objPT.LyDo;
                csumSoTien.Text = objPT.SoTien.Value.ToString("n0");
                cSoTienBC.Text = objPT.SoTien_BangChu;
                cNguoiLap.Text = objPT.HoTenNV;

                this.DataSource = (from ct in db.ptChiTietPhieuThus
                                  join hd in db.dvHoaDons on new { ct.TableName, ct.LinkID } equals new { TableName = "dvHoaDon", LinkID = (long?)hd.ID }
                                  join ldv in db.dvLoaiDichVus on hd.MaLDV equals ldv.ID
                                  where ct.MaPT == ID
                                  group ct by new { ldv.ID, TenLDV = ldv.TenHienThi } into gr
                                  select new
                                  {
                                      gr.Key.TenLDV,
                                      SoTien = gr.Sum(p=>p.SoTien).GetValueOrDefault()
                                  }).ToList() ;

            }
            catch { }

        }
    }
}
