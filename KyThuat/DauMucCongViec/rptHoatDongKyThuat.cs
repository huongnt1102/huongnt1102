using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
using Library;
using System.Data.Linq.SqlClient;

namespace KyThuat.DauMucCongViec
{
    public partial class rptHoatDongKyThuat : DevExpress.XtraReports.UI.XtraReport
    {
        public rptHoatDongKyThuat(DateTime? TuNgay, DateTime? DenNgay, byte MaTN, int MaNCV)
        {
            InitializeComponent();

            cTHoiGian.Text = string.Format("Period:   From. {0:MMM dd, yyyy} to {1:MMM dd, yyyy}", TuNgay, DenNgay);
            cSTT.DataBindings.Add(new XRBinding("Text", DataSource, "STT"));
            cMatBang.DataBindings.Add(new XRBinding("Text", DataSource, "MatBang"));
            cKhuVuc.DataBindings.Add(new XRBinding("Text", DataSource, "KhoiNha"));
            cHeThong.DataBindings.Add(new XRBinding("Text", DataSource, "HeThong"));
            cCV.DataBindings.Add(new XRBinding("Text", DataSource, "MoTa"));
            cNguoiPhuTrach.DataBindings.Add(new XRBinding("Text", DataSource, "NguoiPhuTrach"));
            cNgay.DataBindings.Add(new XRBinding("Text", DataSource, "ThoiHan"));
            cTinhTrang.DataBindings.Add(new XRBinding("Text", DataSource, "TrangThai"));
            
            using (var db = new MasterDataContext())
            {
                lbNgayBaoCao.Text = string.Format("{0:dd/MM/yyyy}", db.GetSystemDate());
                var obj = db.btDauMucCongViecs.Where(p => p.NguonCV == MaNCV && SqlMethods.DateDiffDay(TuNgay, p.ThoiGianTH) >= 0 && SqlMethods.DateDiffDay(p.ThoiGianTH, DenNgay) >= 0)//.AsEnumerable()
                    .Select(p => new
                    {
                        //    STT = index + 1,
                        MatBang = p.MaMB != null ? p.mbMatBang.MaSoMB : p.btDauMucCongViec_TaiSans.FirstOrDefault().tsTaiSan.mbMatBang.MaSoMB,
                        KhoiNha = p.MaKN != null ? p.mbKhoiNha.TenKN : p.btDauMucCongViec_TaiSans.FirstOrDefault().tsTaiSan.mbMatBang.mbTangLau.mbKhoiNha.TenKN,
                        HeThong = p.btDauMucCongViec_TaiSans == null ? "" : p.btDauMucCongViec_TaiSans.FirstOrDefault().tsTaiSan.tsHeThong.TenHT,
                        NguoiPhuTrach = p.NhanVienNhan == null ? "" : p.NhanVienNhan.ToString().Substring(1),
                        ThoiHan = p.ThoiGianHT == null ? SqlMethods.DateDiffDay(p.ThoiGianHetHan, p.ThoiGianTheoLich) : SqlMethods.DateDiffDay(p.ThoiGianHT, p.ThoiGianTH),
                        TrangThai = p.TrangThaiCV != null ? p.btCongViecBT_trangThai.TenTT : "",
                        p.MoTa
                    }).OrderBy(p=>p.KhoiNha).ToList();
                this.DataSource = obj;
            }
        }

        int STT = 0;
        private void cSTT_EvaluateBinding(object sender, BindingEventArgs e)
        {
            STT++;
            e.Value = STT;
        }

    }
}
