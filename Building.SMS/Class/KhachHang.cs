namespace Building.SMS.Class
{
    public static class KhachHang
    {
        public static int SendSmsKhachHang(System.Collections.Generic.List<LKhachHang> lKhachHang, byte maTn, string contentsTemplate, int idTemplate, int tyleId)
        {
            int error = 0;

            var brandName = Building.SMS.Class.Common.GetBrandName(maTn);
            if (brandName.Id == 0) return 1; // không có brandName

            foreach (var khachHang in lKhachHang)
            {
                System.Collections.Generic.List<LKhachHang> khachHangItems = new System.Collections.Generic.List<LKhachHang>();
                khachHangItems.Add(khachHang);

                var table = new System.Data.DataTable();
                table = Library.SqlCommon.LINQToDataTable(khachHangItems);

                foreach (System.Data.DataRow i in table.Rows)
                {
                    var contents = Building.SMS.Class.Common.GetContents(contentsTemplate, tyleId, i, "GuiSMSKhachHang");
                    error = Building.SMS.Class.Common.SendSms(contents, brandName.BrandName, khachHang.SoDienThoai, brandName.Id, idTemplate, maTn, khachHang.MaKh, khachHang.MaMb, khachHang.TenKhachHang,khachHang.MaSoMb);
                }
            }

            return error;
        }

        public class LKhachHang
        {
            public int? MaKh { get; set; }
            public int? MaMb { get; set; }
            public string KyHieu { get; set; }
            public string MaPhu { get; set; }
            public string TenKhachHang { get; set; }
            public string SoDienThoai { get; set; }
            public string MaSoMb { get; set; }
        }
    }
}
