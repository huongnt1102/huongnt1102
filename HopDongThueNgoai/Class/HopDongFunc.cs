using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HopDongThueNgoai.Class
{
    public static class HopDongFunc
    {
        public static System.Collections.Generic.List<HopDongThueNgoai.Class.PhanLoai> GetListPhanLoai()
        {
            return new System.Collections.Generic.List<HopDongThueNgoai.Class.PhanLoai>
            {
                new HopDongThueNgoai.Class.PhanLoai {Id = HopDongThueNgoai.Class.TenHienThi.HOP_DONG, Name = "Hợp đồng"},
                new HopDongThueNgoai.Class.PhanLoai {Id = HopDongThueNgoai.Class.TenHienThi.PHU_LUC, Name = "Phụ lục"}
            };
        }
    }
}
