using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandsoftBuilding.Receivables.Reports.Class
{
    public class ChiTietThueMatBangModel
    {
        public int? STT { get; set; }
        public string TenTN { get; set; }
        public string TenKN { get; set; }
        public string TenTL { get; set; }
        public string KyHieu { get; set; }
        public string TenKH { get; set; }
        public string MaSoMB { get; set; }
        public string TenNKH { get; set; }
        public DateTime? NgayBatDau { set; get; }
        public DateTime? NgayKetThuc { set; get; }
        public string HanThanhToan { set; get; }
        public string NganhNgheKD { set; get; }
        public decimal? DienTichThue { set; get; }
        public decimal? DonGiaThue { set; get; }
        public decimal? DonGiaPhiDV { set; get; }
        public decimal? DonGiaPhiDHCS { set; get; }
        public decimal? SoTien { get; set; }
        public string Nhom { get; set; }
        public string Loai { get; set; }
    }
}
