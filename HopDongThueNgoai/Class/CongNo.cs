
namespace HopDongThueNgoai.Class
{
    public static class CongNo
    {
        public class CongNoNhaCungCap
        {
            public int? KhachHangId { get; set; }
            public int? HopDongId { get; set; }
            public decimal? DaTra { get; set; }
        }

        public class CongNoHopDong
        {
            public int? HopDongId { get; set; }
            public int? KhachHangId { get; set; }
            
            public decimal? TongTienHopDong { get; set; }

            public string HopDongNo { get; set; }
            public string KhachHangName { get; set; }
            public string TrangThaiName { get; set; }
        }
    }
}
