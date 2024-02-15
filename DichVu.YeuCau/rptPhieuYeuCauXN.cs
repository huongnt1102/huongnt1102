using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data.Linq;
using System.Linq;
using Library;

namespace DichVu.YeuCau
{
    public partial class rptPhieuYeuCauXN : DevExpress.XtraReports.UI.XtraReport
    {
        MasterDataContext db;
        public rptPhieuYeuCauXN(int? MaYC)
        {
            InitializeComponent();
            db = new MasterDataContext();

            //var objYC = db.tnycYeuCaus.SingleOrDefault(p=>p.ID==MaYC);
            var objYC = (from p in db.tnycYeuCaus
                join nk in db.tnNhanKhaus on p.MaKH equals nk.MaKH into nhanKhau
                from nk in nhanKhau.DefaultIfEmpty()
                where p.ID == MaYC
                select new {p.MaKH, nk.HoTenNK,p.TieuDe,p.NgayYC,p.mbMatBang.MaSoMB,p.tnToaNha.TenTN,p.NoiDung}).FirstOrDefault();

            var objDMCV = db.btDauMucCongViecs.SingleOrDefault(p=>p.MaNguonCV == MaYC);
            lbNgayTB.Text = string.Format("{0:dd/MM/yyyy}", db.GetSystemDate());
            string Name = objYC.MaKH == null ? "" : objYC.HoTenNK;
            lbHoTenKH.Text = Name;
            lbTieuDe.Text = objYC.TieuDe;
            if (objDMCV != null)
            {
                lbChiPhiDuKien.Text = string.Format("Chi phí dự kiến: {0:#,0.##} VNĐ",objDMCV.ChiPhi);
            }
            lbNgayHen.Text = string.Format("Vì vậy chung tôi hy vọng quý (Ông/ Bà/Cty) sắp xếp lịch hẹn cho kĩ thuật viên của Dự án kiểm tra và khắc phụ sự cố nếu trên vào ngày : {0:dd/MM/yyyy} lúc : {1:hh:mm tt}",objDMCV.ThoiGianTheoLich,objDMCV.GioTHDuKien);
            lbNgayYeuCau.Text = string.Format("Chúng tôi gửi thông báo này theo yêu cầu của quý (Ông/Bà/Cty) về vấn đề phát sinh trong mặt bằng của quý (ông/bà/Cty) được gửi tới  từ ngày: {0:dd/MM/yyyy}",objYC.NgayYC);
            lbMatBang.Text = string.Format("Theo đó mặt bằng {0} thuộc Dự án {1} của quý (Ông/Bà/Cty) gặp vấn đề :",objYC.MaSoMB,objYC.TenTN);
            lbNoiDung.Text = string.Format("Cụ thể: {0}",objYC.NoiDung);

        }

    }
}
