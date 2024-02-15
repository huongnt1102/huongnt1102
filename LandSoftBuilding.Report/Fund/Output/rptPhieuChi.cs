using System;
using System.Drawing;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
using Library;

namespace LandSoftBuilding.Fund.Output
{
    public partial class rptPhieuChi : DevExpress.XtraReports.UI.XtraReport
    {
        public rptPhieuChi(int ID, byte MaTN)
        {
            InitializeComponent();

            Library.frmPrintControl.LoadLayout(this, 4, MaTN);

            if (ID == 0) return;

            #region DataBindings
            cSoPhieu.DataBindings.Add("Text", null, "SoPC", "Số phiếu: {0}");
            cNgayPT.DataBindings.Add("Text", null, "NgayChi", "Ngày {0:dd} tháng {0:MM} năm {0:yyyy}");
            cNguoiNhan.DataBindings.Add("Text", null, "NguoiNhan");
            cDiaChi.DataBindings.Add("Text", null, "DiaChiNN");
            cLyDo.DataBindings.Add("Text", null, "LyDo");
            cSoTien.DataBindings.Add("Text", null, "SoTien", "{0:n0} VNĐ");
            cSoTienBC.DataBindings.Add("Text", null, "SoTien_BangChu");
            cNguoiLapPhieu.DataBindings.Add("Text", null, "HoTenNV");    
            #endregion

            var objTien = new TienTeCls();
            using (var db = new Library.MasterDataContext())
            {
                try
                {
                    var obj = (from p in db.pcPhieuChis
                               join nv in db.tnNhanViens on p.MaNVN equals nv.MaNV
                               where p.ID == ID
                               select new
                               {
                                   p.MaTN,
                                   p.SoPC,
                                   p.NgayChi,
                                   p.NguoiNhan,
                                   p.DiaChiNN,
                                   p.LyDo,
                                   p.SoTien,
                                   SoTien_BangChu = objTien.DocTienBangChu(p.SoTien.Value, "đồng chẵn"),
                                   p.ChungTuGoc,
                                   nv.HoTenNV,
                                   p.MaTKNH
                               }).ToList();

                    this.DataSource = obj;
                    
                    #region Thong tin toa nha
                    var objTN = db.tnToaNhas.FirstOrDefault(p => p.MaTN == obj.FirstOrDefault().MaTN);
                    cBanQuanLy.Text = "Ban Quản lý Tòa nhà: " + objTN.TenTN;
                    picLogo.ImageUrl = objTN.Logo;
                    cTieuDePhieu.Text = obj.FirstOrDefault().MaTKNH == null ? "PHIẾU CHI" : "PHIẾU CHI TIỀN CHUYỂN KHOẢN";
                    #endregion
                }
                catch { }
            }
        }
    }
}
