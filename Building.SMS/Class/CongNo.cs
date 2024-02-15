using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using DevExpress.XtraRichEdit.API.Native;
using Library;

namespace Building.SMS.Class
{
    public static class CongNo
    {
        public static int SendSmsThongBaoThanhToan(List<ThongBaoThanhToan> lKhachHang, byte maTn, int thang, int nam)
        {
            int error = 0;

            var brandName = Building.SMS.Class.Common.GetBrandName(maTn);
            if (brandName.Id == 0) return 1; // không có brandName

            var template = Building.SMS.Class.Common.GetTemplate(maTn, 3);
            if (template.TemplateId == 0) return 2; // không có mẫu template

            // khách hàng thì không cần kiểm tra nhận sms, gửi hết trong cái list khách hàng được chọn, yêu cầu list khách hàng này phải có số điện thoại, đã được chuyển sang 84
            foreach (var item in lKhachHang)
            {
                // gửi sms

                List<ThongBaoThanhToan> lNoiDung = new List<ThongBaoThanhToan>();
                lNoiDung.Add(item);

                var dt = new DataTable();
                dt = SqlCommon.LINQToDataTable(lNoiDung);

                //error = SendSms(template.Contents, 3, dt, "ThongBaoToiKyThanhToan", brandName.BrandName, item.Sdt, brandName.Id, template.TemplateId, maTn);
                //DataRow rData = dt.Rows[0];
                foreach (DataRow i in dt.Rows)
                {
                    var contents = Building.SMS.Class.Common.GetContents(template.Contents, 3, i, "ThongBaoToiKyThanhToan");
                    error = Building.SMS.Class.Common.SendSms(contents, brandName.BrandName, item.Sdt, brandName.Id, template.TemplateId, maTn, item.MaKh, item.MaMb, item.TenKhachHang, item.MaMatBang);
                }
            }

            return error;
        }

        public static int GuiSmsDaThanhToan(DaThanhToan lKhachHang, byte maTn)
        {
            var result = 0;

            var brandName = Building.SMS.Class.Common.GetBrandName(maTn);
            if (brandName.Id == 0) return 1; // không có brandName

            var template = Building.SMS.Class.Common.GetTemplate(maTn, 4);
            if (template.TemplateId == 0) return 2; // không có mẫu template

            List<DaThanhToan> lDaThanhToan = new List<DaThanhToan>();
            lDaThanhToan.Add(new DaThanhToan{ DienGiai = lKhachHang.DienGiai, NamThanhToan = lKhachHang.NamThanhToan, MaMatBang = lKhachHang.MaMatBang, NgayThanhToan = lKhachHang.NgayThanhToan, NguoiThanhToan = lKhachHang.NguoiThanhToan, PhuongThucThanhToan = lKhachHang.PhuongThucThanhToan, Sdt = lKhachHang.Sdt, SoTien = lKhachHang.SoTien, TenKhachHang = lKhachHang.TenKhachHang, ThangThanhToan = lKhachHang.ThangThanhToan});

            var dt = new DataTable();
            dt = SqlCommon.LINQToDataTable(lDaThanhToan);
            DataRow rData = dt.Rows[0];
            var contents = Building.SMS.Class.Common.GetContents(template.Contents, 4, rData, "GuiSmsDaThanhToan");
            result = Building.SMS.Class.Common.SendSms(contents, brandName.BrandName, lKhachHang.Sdt, brandName.Id, template.TemplateId, maTn, lKhachHang.MaKh, lKhachHang.MaMb, lKhachHang.TenKhachHang, lKhachHang.MaMatBang);

            return result;
        }

        public static int GuiSmsNhacNo(List<NhacNo> lKhachHang, byte maTn)
        {
            int error = 0;

            var brandName = Building.SMS.Class.Common.GetBrandName(maTn);
            if (brandName.Id == 0) return 1; // không có brandName

            var template = Building.SMS.Class.Common.GetTemplate(maTn, 5);
            if (template.TemplateId == 0) return 2; // không có mẫu template

            //var contents = GetContents(template.Contents); // giả sử như ở đây mình đã giải quyết được nội dung để gửi rồi

            // khách hàng thì không cần kiểm tra nhận sms, gửi hết trong cái list khách hàng được chọn, yêu cầu list khách hàng này phải có số điện thoại, đã được chuyển sang 84
            foreach (var item in lKhachHang)
            {
                List<NhacNo> lNoiDung = new List<NhacNo>();
                lNoiDung.Add(item);

                var dt = new DataTable();
                dt = SqlCommon.LINQToDataTable(lNoiDung);

                foreach (DataRow i in dt.Rows)
                {
                    var contents = Building.SMS.Class.Common.GetContents(template.Contents, 5, i, "GuiSmsNhacNo");
                    error = Building.SMS.Class.Common.SendSms(contents, brandName.BrandName, item.Sdt, brandName.Id, template.TemplateId, maTn, item.MaKh, item.MaMb, item.TenKhachHang, item.MaMatBang);
                }
                
            }

            return error;
        }

        
        public class ThongBaoThanhToan
        {
            public int? MaKh { get; set; }
            public int? MaMb { get; set; }
            public string Sdt { get; set; }

            public int ThangThongBao { get; set; }
            public int NamThongBao { get; set; }

            public string MaKhachHang { get; set; }
            public string TenKhachHang { get; set; }
            public string MaMatBang { get; set; }

            public string TongTien { get; set; }
            public string DaThu { get; set; }
            public string KhauTru { get; set; }
            public string PhatSinh { get; set; }
            public string NoCu { get; set; }
            public string ConNo { get; set; }
        }

        public class DaThanhToan
        {
            public int? MaKh { get; set; }
            public int? MaMb { get; set; }

            public string Sdt { get; set; }
            public string TenKhachHang { get; set; }
            public string MaMatBang { get; set; }
            public string NguoiThanhToan { get; set; }
            public string NgayThanhToan { get; set; }
            public string ThangThanhToan { get; set; }
            public string NamThanhToan { get; set; }
            public string PhuongThucThanhToan { get; set; }
            public string DienGiai { get; set; }
            public string SoTien { get; set; }
        }

        public class NhacNo
        {
            public int? MaKh { get; set; }
            public int? MaMb { get; set; }

            public string Sdt { get; set; }
            public string TenKhachHang { get; set; }
            public string MaMatBang { get; set; }
            public string TienNo { get; set; }
        }
    }
}
