using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace TaiSan.BaoCaoPhanTich
{
    public partial class rptThongKeTS : DevExpress.XtraReports.UI.XtraReport
    {
        public rptThongKeTS(object obj, DateTime? TuNgay, DateTime? DenNgay, string TenTN, string DVSD, string LoaiTS)
        {
            InitializeComponent();

            //lblSTT.DataBindings.Add(new XRBinding("Text", DataSource, "STT"));
            lblToaNha.Text = "Dự án " + TenTN.ToUpper();
            lblTuNgay.Text = string.Format("{0:dd/MM/yyyy}", TuNgay);
            lblDenNgay.Text = string.Format("{0:dd/MM/yyyy}", DenNgay);
            cDonVi.Text = DVSD ?? null;
            cLoaiTaiSan.Text = LoaiTS ?? null;
            cDVSD.DataBindings.Add(new XRBinding("Text", DataSource, "TenDV"));
            cGTConLai.DataBindings.Add(new XRBinding("Text", DataSource, "GiaTriConLai","{0:#,0.##}"));
            cGTKhauHaoHT.DataBindings.Add(new XRBinding("Text", DataSource, "GTTKHHienThoi", "{0:#,0.##}"));
            cGTTinhKH.DataBindings.Add(new XRBinding("Text", DataSource, "GiaTriTinhKH", "{0:#,0.##}"));
            cHaoMonLuyKe.DataBindings.Add(new XRBinding("Text", DataSource, "HaoMonLuyKe", "{0:#,0.##}"));
            cHaoMonTrongKy.DataBindings.Add(new XRBinding("Text", DataSource, "HaoMonTrongKy", "{0:#,0.##}"));
            cLoaiTSCD.DataBindings.Add(new XRBinding("Text", DataSource, "TenLTS"));
            cMaTS.DataBindings.Add(new XRBinding("Text", DataSource, "MaTS"));
            cMucKHThang.DataBindings.Add(new XRBinding("Text", DataSource, "GiaTriKHThang", "{0:#,0.##}"));
            cNgaybdSD.DataBindings.Add(new XRBinding("Text", DataSource, "NgayBDSD"));
            cNgayGhiGiam.DataBindings.Add(new XRBinding("Text", DataSource, ""));
            cNgayGT.DataBindings.Add(new XRBinding("Text", DataSource, "NgayGT"));
            cNguyenGia.DataBindings.Add(new XRBinding("Text", DataSource, "NguyenGia", "{0:#,0.##}"));
            cSoCTGhiGiam.DataBindings.Add(new XRBinding("Text", DataSource, ""));
            cSoGT.DataBindings.Add(new XRBinding("Text", DataSource, "SoGT"));
            cTenTS.DataBindings.Add(new XRBinding("Text", DataSource, "TenTS"));
            cTKKhauHao.DataBindings.Add(new XRBinding("Text", DataSource, "TKKhauHao"));
            cTKNguyenGia.DataBindings.Add(new XRBinding("Text", DataSource, "TKNguyenGia"));
            cTLKHThang.DataBindings.Add(new XRBinding("Text", DataSource, "TyLeKHThang", "{0:#,0.##}"));
            cTHoiGianSD.DataBindings.Add(new XRBinding("Text", DataSource, "ThoiGianSD", "{0:#,0}"));
            cThoiGianSDCL.DataBindings.Add(new XRBinding("Text", DataSource, "ThoiGianSDCL", "{0:#,0}"));
            this.DataSource = obj;
            
        }

    }
}
