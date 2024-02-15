using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
using Library;
using System.Collections.Generic;
using System.Data.Linq.SqlClient;
using DevExpress.Data.PivotGrid;

namespace DichVu.NhanKhau.Reports
{
    public partial class rptNhanKhau : DevExpress.XtraReports.UI.XtraReport
    {
        byte MaTN;
        MasterDataContext db = new MasterDataContext();

        public rptNhanKhau(DateTime _TuNgay, DateTime _DenNgay, byte _MaTN)
        {
            InitializeComponent();
            this.MaTN = _MaTN;

            #region DataBindings
            cSTT.DataBindings.Add("Text", null, "STT");
            cMatBang.DataBindings.Add("Text", null, "MaSoMB");
            cHoTen.DataBindings.Add("Text", null, "HoTenNK");
            cQuanHe.DataBindings.Add("Text", null, "QuanHe");
            cNguyenQuan.DataBindings.Add("Text", null, "QuocTich");
            cGioiTinh.DataBindings.Add("Text", null, "GioiTinh");
            cDienThoai.DataBindings.Add("Text", null, "DienThoai");
            cEmail.DataBindings.Add("Text", null, "Email");
            cNoiLamViec.DataBindings.Add("Text", null, "NoiLamViec");
            cNgayKy.DataBindings.Add("Text", null, "NgayDK","{0:dd/MM/yyyy}");
            cNgayHetHan.DataBindings.Add("Text", null, "NgayHetHanDKTT","{0:dd/MM/yyyy}");
            cNgayDen.DataBindings.Add("Text", null, "NgayChuyenDen", "{0:dd/MM/yyyy}");
            cTrangThai.DataBindings.Add("Text", null, "TrangThai");
            #endregion

            var objTheXe = db.tnNhanKhaus
                        .Where(p => 
                                 p.mbMatBang.mbTangLau.mbKhoiNha.MaTN == _MaTN)
                        .OrderByDescending(p => p.NgayDK).AsEnumerable()
                        .Select((p, index) => new
                        {
                            STT = index + 1,
                            p.mbMatBang.MaSoMB,
                            p.HoTenNK,
                            QuanHe = p.tnQuanHe.Name,
                            p.QuocTich,
                            GioiTinh = p.GioiTinh == true ? "Nam" : "Nữ",
                            p.DienThoai,
                            p.Email,
                            p.NoiLamViec,
                            p.NgayDK,
                            p.NgayHetHanDKTT,
                            p.NgayChuyenDen,
                            TrangThai = db.tnNhanKhauTrangThais.SingleOrDefault(k => k.MaTT == p.MaTT).TenTrangThai,
                        }).ToList();
            this.DataSource = objTheXe;
        }

        double TinhSoThang(DateTime? NgayDK, DateTime _DenNgay)
        {
            double SoThang = (double)SqlMethods.DateDiffMonth(NgayDK, _DenNgay);
            SoThang = SoThang == 0 ? SoThang : SoThang - 1;
            SoThang = NgayDK.Value.Day > 15 ? SoThang + 0.5 : SoThang + 1;
            return SoThang;
        }
    }
}
