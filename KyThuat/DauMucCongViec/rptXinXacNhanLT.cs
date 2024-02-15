using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data.Linq;
using System.Linq;
using Library;

namespace KyThuat.DauMucCongViec
{
    public partial class rptXinXacNhanLT : DevExpress.XtraReports.UI.XtraReport
    {
        MasterDataContext db;
        public rptXinXacNhanLT(long? MaDMCV)
        {
            InitializeComponent();
            db = new MasterDataContext();
            var objDMCV = db.btDauMucCongViecs.SingleOrDefault(p => p.ID == MaDMCV);
            lbNgayTB.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Now);
            string Name = objDMCV.MaMB == null ? "" : (objDMCV.mbMatBang.MaKH == null ? "" : objDMCV.mbMatBang.tnKhachHang.HoKH + " " + objDMCV.mbMatBang.tnKhachHang.TenKH);
            string MaSoMB = objDMCV.MaMB == null ? "" : objDMCV.mbMatBang.MaSoMB;
            string TenTN = objDMCV.MaTN == null ? "" : objDMCV.tnToaNha.TenTN;
            lbHoTenKH.Text = Name;
            lbMaSoCV.Text = string.Format("Mã số CV: {0}",MaDMCV);
            lbMatBang.Text = string.Format("Cụ thể: Là mặt bằng {0} thuộc Dự án {1} của quý (ông/bà/Cty)",MaSoMB,TenTN);
            lbNgayHen.Text = string.Format("Vì vậy chung tôi hy vọng quý (ông/ bà/Cty) sắp xếp lịch hẹn cho kĩ thuật viên của Dự án thực hiện nhiệm vụ vào ngày:  {0:dd/MM/yyyy}  lúc: {1:hh:mm tt}", objDMCV.ThoiGianTheoLich, objDMCV.GioTHDuKien);
            
        }

    }
}
