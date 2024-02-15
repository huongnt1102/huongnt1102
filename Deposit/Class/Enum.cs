
namespace Deposit.Class
{
    public static class Enum
    {
        public static class FormName
        {
            public const string PHIEU_THU_DAT_COC = "PHIẾU THU TIỀN CỌC";
            public const string PHIEU_THU_TIEN_PHAT = "PHIẾU THU TIỀN PHẠT";
            public const string PHIEU_CHI_HOAN_TIEN = "PHIẾU CHI HOÀN TIỀN";
            public const string HOP_DONG_DAT_COC = "HỢP ĐỒNG ĐẶT CỌC";
        }

        public static class ThaoTac
        {
            public const string ADD = "ADD";
            public const string EDIT = "EDIT";
            public const string SAVE = "SAVE";
            public const string VIEW = "VIEW";
        }

        public static class KiemTra
        {
            public const string RANGBUOC = "RangBuoc";
            public const string MASO = "MaSo";
        }

        public static class TienDatCoc
        {
            public const string TONG_TIEN = "TongTien";
            public const string DA_THU = "DaThu";
            public const string DA_TRA = "DaTra";
            public const string CON_LAI = "ConLai";
        }

        public static class Category
        {
            public const string LOAI_HOP_DONG = "LoaiHopDong";
            public const string TRANG_THAI = "TrangThai";
            public const string NHA_THAU = "NhaThau";
        }

        public static class ReportGroupId
        {
            public const int PHIEU_THU_ID = 5;
            public const int PHIEU_CHI_DAT_COC_ID = 16;
        }
    }
}
