using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandsoftBuilding.Receivables.Reports.Class
{
    public class BaoCaoTienCocModel
    {
        //public decimal? CongNoVuotCocSauKhiTru { get; set; }
        //public decimal? DatCocConPhaiThu { get; set; }
        //public decimal? DatCocPhaiThuTheoHD { get; set; }
        //public decimal? DieuChinhDatCoc { get; set; }
        //public decimal? DatCocDaHoanTra { get; set; }
        //public decimal? DatCocDaThu { get; set; }
        //public decimal? DatCocConGiuLai { get; set; }
        //public decimal? CongNoVuotCoc { get; set; }
        //public decimal? GiaTriTaiSanHienCo { get; set; }
        //public byte? MaTN { get; set; }
        public string TenTN { get; set; }
        public string TenKN { get; set; }
        //public string GhiChu { get; set; }

        public decimal? DatCocPhaiThu { get; set; }
        public decimal? DieuChinhTangGiam { get; set; }
        public decimal? DatCocDaThu { get; set; }
        public decimal? ConPhaiThu { get; set; }
        public decimal? DatCocThua { get; set; }
        public decimal? DatCocDaHoanTra { get; set; }
        public decimal? DatCocConGiuLai { get; set; }
        public decimal? CongNoVuotCoc { get; set; }
        public decimal? TongCongConNo { get; set; }

    }
    public class BaoCaoTienCoc_Form1Model : BaoCaoTienCocModel
    {
        public string KyHieu { get; set; }
        public string TenKH { get; set; }
        //public string TenNKH { get; set; }
        public string TenTL { get; set; }
        public string MaSoMB { get; set; }
        public string NgayBatDau { get; set; }
        public string NgayKetThuc { get; set; }
        //public string NganhNgheKD { get; set; }
        public decimal? DienTichThue { get; set; }
        public string TenNKH { get; set; }

        public string QuyDinh { get; set; }
        public string NganhNgheKD { get; set; }

    }
    public class BaoCaoTienCoc_Form2Model : BaoCaoTienCocModel
    {
        public decimal? SoKHVuotCoc { set; get; }
        public decimal? SoKHVuotCocSauKhiDinhGia { set; get; }

    }
}
