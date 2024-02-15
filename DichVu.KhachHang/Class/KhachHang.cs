
using Remotion.Mixins.Definitions;
using System.Linq;
using System.Windows.Forms;

namespace DichVu.KhachHang.Class
{
    public static class KhachHang
    {
        public class Loai
        {
            public const string CA_NHAN = "CaNhan";
            public const string CTY = "CongTy";
            public const string NCC = "Ncc";
        }

        public class DanhMuc
        {
            public const string NHOM_KH = "NhomKh";
            public const string LOAI_KH = "LoaiKh";
            public const string KHU_VUC = "KhuVuc";

            public const string LOAI_CO_HOI = "LoaiCoHoi";
            public const string NGUON_DEN = "NguonDen";
            public const string NV_PHU_TRACH = "NvPhuTrach";
        }
    }
}
