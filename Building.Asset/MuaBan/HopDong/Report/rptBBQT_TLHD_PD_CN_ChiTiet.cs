using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
using System.Data.Linq.SqlClient;
using Library;

namespace TaiSan.BanHang.Report
{
    public partial class rptBBQT_TLHD_PD_CN_ChiTiet : XtraReport
    {
        public rptBBQT_TLHD_PD_CN_ChiTiet(int? maHd)
        {
            InitializeComponent();

            #region BindingData

            cSTT.DataBindings.Add("Text", null, "STT");
            cNoiDung.DataBindings.Add("Text", null, "Name");
            cDonViTinh.DataBindings.Add("Text", null, "TenDVT");
            cKhoiLuongTamTinh.DataBindings.Add("Text", null, "KhoiLuongThucTinh", "{0:#,0.##}");
            cKhoiLuong.DataBindings.Add("Text", null, "KhoiLuongThucTinh", "{0:#,0.##}");
            
            //Sum
            //cSSoLuong.DataBindings.Add("Text", null, "SoLuong", "{0:N0}");
            //cSSoLuong.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:n0}");


            #endregion

            try
            {
                using (var db=new MasterDataContext())
                {
                    var list = (from p in db.xkHopDongBan_PhaDoVanChuyens
                                join nd in db.xkHopDongBan_SanPhamPhaDos on p.MaCongViec equals nd.ID into noiDung from nd in noiDung.DefaultIfEmpty()
                                join dvt in db.tsLoaiTaiSan_DVTs on p.MaDVT equals dvt.MaDVT into donViTinh from dvt in donViTinh.DefaultIfEmpty()
                                where p.MaHD == maHd
                                select new
                                {
                                    nd.Name,
                                    dvt.TenDVT,
                                    p.KhoiLuongThucTinh
                                }).AsEnumerable().Select((p, Index) => new
                                {
                                    STT = Index + 1,
                                    p.Name,
                                    p.TenDVT,
                                    p.KhoiLuongThucTinh
                                }).ToList();
                    
                    this.DataSource = list;
                }
            }
            catch { }
        }
    }
}
