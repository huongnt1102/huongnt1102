using System.Linq;

namespace Deposit.Class
{
    public static class HopDong
    {
        private static Library.MasterDataContext _db = new Library.MasterDataContext();

        #region Update hợp đồng

        public static Library.Dep_HopDong UpdateHopDong(Library.Dep_HopDong hopDong, string loai, decimal? soTien)
        {
            switch (loai)
            {
                case Deposit.Class.Enum.TienDatCoc.TONG_TIEN: hopDong.TongTien = (hopDong.TongTien ?? 0) + soTien; break;
                case Deposit.Class.Enum.TienDatCoc.DA_THU: hopDong.ThuPhat = (hopDong.ThuPhat ?? 0) + soTien; break;
                case Deposit.Class.Enum.TienDatCoc.DA_TRA: hopDong.TienTra = (hopDong.TienTra ?? 0) + soTien; break;
            }
            return hopDong;
        }

        public static Library.Dep_HopDong UpdateHopDongAll(Library.Dep_HopDong hopDong, decimal? daThu, decimal? daTra, decimal? datCoc)
        {
            hopDong = Deposit.Class.HopDong.UpdateHopDong(hopDong, Deposit.Class.Enum.TienDatCoc.DA_THU, daThu);
            hopDong = Deposit.Class.HopDong.UpdateHopDong(hopDong, Deposit.Class.Enum.TienDatCoc.DA_TRA, daTra);
            hopDong = Deposit.Class.HopDong.UpdateHopDong(hopDong, Deposit.Class.Enum.TienDatCoc.TONG_TIEN, datCoc);

            return hopDong;
        }

        #endregion

        public static Library.Dep_HopDong GetHopDongById(int? hopDongId)
        {
            return _db.Dep_HopDongs.FirstOrDefault(_ => _.Id == hopDongId);
        }

        #region Hợp đồng item

        public static System.Collections.Generic.List<HopDongItem> SetValueHopDongItem(string soHopDong, string hoTenKhachHang, string matBang, string daiDienCongTy, string diaChiCongTy, string ghiChu)
        {
            return new System.Collections.Generic.List<HopDongItem>
            {
                new HopDongItem
                {
                    SoHopDong = soHopDong,
                    Ngay = System.DateTime.Now.Day.ToString(),
                    Thang = System.DateTime.Now.Month.ToString(),
                    Nam = System.DateTime.Now.Year.ToString(),
                    HoTenKhachHang = hoTenKhachHang,
                    MatBang = matBang,
                    DaiDienCongTy = daiDienCongTy,
                    DiaChiCongTy = diaChiCongTy,
                    GhiChu = ghiChu
                }
            }.ToList();
        }

        public class HopDongItem
        {
            public string SoHopDong { get; set; }
            public string Ngay { get; set; }
            public string Thang { get; set; }
            public string Nam { get; set; }
            public string HoTenKhachHang { get; set; }
            public string MatBang { get; set; }
            public string DaiDienCongTy { get; set; }
            public string DiaChiCongTy { get; set; }
            public string GhiChu { get; set; }
        }
        #endregion
    }
}
