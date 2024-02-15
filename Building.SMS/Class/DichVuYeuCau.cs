using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using DevExpress.XtraRichEdit.API.Native;
using Library;
using Newtonsoft.Json.Linq;
using ZaloDotNetSDK;

namespace Building.SMS.Class
{
    public static class DichVuYeuCau
    {
        // dịch vụ yêu cầu
        public static int SendSmsCustomer(string tieuDe, string noiDung, int maKh, byte maTn)
        {
            var db = new MasterDataContext();
            int error = 0;

            // kiểm tra brandname
            var brandName = Building.SMS.Class.Common.GetBrandName(maTn);
            if (brandName.Id == 0) return 1; // không có brandName

            // mẫu gửi sms
            //var buildingTemplateId = Building.SMS.Class.Common.GetBuildingTemplate(maTn, 2);
            //if (buildingTemplateId == 0) return 2; // không có mẫu template

            // trường trộn
            var template = Building.SMS.Class.Common.GetTemplate(maTn, 2);
            if (template.TemplateId == 0) return 2; // không có mẫu template

            List<NoiDungYeuCau> lNoiDung = new List<NoiDungYeuCau>();
            lNoiDung.Add(new NoiDungYeuCau { TieuDe = tieuDe, NoiDung = noiDung });

            var dt = new DataTable();
            dt = SqlCommon.LINQToDataTable(lNoiDung);
            DataRow rData = dt.Rows[0];
            var contents = Building.SMS.Class.Common.GetContents(template.Contents, 2, rData, "YcKhachHang");

            // khách hàng thì k cần kiểm tra nhận sms, ông nào gửi yêu cầu thì trả lại cho ông ấy thôi
            var kh = db.tnKhachHangs.First(_ => _.MaKH == maKh);
            if (kh != null & kh.DienThoaiKH != null & kh.DienThoaiKH != "")
            {

                // đổi số điện thoại sang 84
                string sdt1 = "";
                try
                {
                    sdt1 = long.Parse(kh.DienThoaiKH.ToString().Split('/')[0]).ToString();
                }
                catch
                {
                    return 4;
                }

                var sdt2 = string.Format("{0}{1}", "84", sdt1);
                // gửi sms
                var history = new smsHistory();

                history.ResultSend = "Thành công";
                Building.SMS.Class.Common.SmsSouthTelecom result = Building.SMS.Class.Common.SensMtSms(brandName.BrandName, sdt2, contents);

                if (result.status != "1")
                {
                    error = 5;
                    history.ResultSend = "Error: " + result.errorcode + " - " + result.description;
                }

                #region Lưu lịch sử gửi sms


                history.BrandID = brandName.Id;
                history.BrandName = brandName.BrandName;
                history.DateCreate = DateTime.UtcNow.AddHours(7);
                history.FormID = template.TemplateId;
                history.IsAds = false;
                history.LastSend = 1;
                history.ToMobile = sdt2;
                history.Contents = contents;
                history.MaKH = maKh;
                //history.MaMB = MaMb;
                history.HoTenKH = kh.TenKH;
                //history.MaSoMB = MaSoMb;

                history.MaTN = maTn;
                db.smsHistories.InsertOnSubmit(history);
                db.SubmitChanges();

                #endregion
            }
            else error = 4;

            return error;

        }

        public static int SendSmsStaf(string tieuDe, string noiDung, byte maTn, int maBp)
        {
            var db = new MasterDataContext();
            int error = 0;

            //if (_isSendSms == false) return 3; // chưa chọn phòng ban

            // kiểm tra brandname
            var brandName = Building.SMS.Class.Common.GetBrandName(maTn);
            if (brandName.Id == 0) return 1; // không có brandName

            // mẫu gửi sms
            //var buildingTemplate = db.SmsBuildings.FirstOrDefault(_ => _.BuildingId == maTn & _.TyleId == 1);
            //if (buildingTemplate == null) return 2; // không có mẫu template

            // trường trộn
            var template = Building.SMS.Class.Common.GetTemplate(maTn, 1);
            if (template.TemplateId == 0) return 2; // không có mẫu template

            List<NoiDungYeuCau> lNoiDung = new List<NoiDungYeuCau>();
            lNoiDung.Add(new NoiDungYeuCau { TieuDe = tieuDe, NoiDung = noiDung });

            var dt = new DataTable();
            dt = SqlCommon.LINQToDataTable(lNoiDung);
            DataRow rData = dt.Rows[0];
            var contents = Building.SMS.Class.Common.GetContents(template.Contents, 1, rData, "YcNhanVien");

            // kiểm tra nhân viên nhận sms
            var users = db.SmsNhanViens.Where(_ => _.BuildingId == maTn & _.MaPB == maBp & _.IsDichVuYeuCau == true);
            var rs = false;

            foreach (var i in users)
            {
                var user = db.tnNhanViens.FirstOrDefault(_ => _.MaNV == i.NhanVienId);
                if (user != null & user.DienThoai != null & user.DienThoai != "")
                {
                    // đổi số điện thoại sang 84
                    string sdt1 = "";
                    try
                    {
                        sdt1 = long.Parse(user.DienThoai.ToString().Split('/')[0]).ToString();
                    }
                    catch
                    {
                        continue;
                    }

                    var sdt2 = string.Format("{0}{1}", "84", sdt1);
                    // gửi sms
                    var history = new smsHistory();

                    history.ResultSend = "Thành công";

                    Building.SMS.Class.Common.SmsSouthTelecom result = Building.SMS.Class.Common.SensMtSms(brandName.BrandName, sdt2, contents);

                    if (result.status != "1")
                    {
                        history.ResultSend = "Error: " + result.errorcode + " - " + result.description;
                        rs = true;
                    }

                    #region Lưu lịch sử gửi sms

                    history.BrandID = brandName.Id;
                    history.BrandName = brandName.BrandName;
                    history.DateCreate = DateTime.UtcNow.AddHours(7);
                    history.FormID = template.TemplateId;
                    history.IsAds = false;
                    history.LastSend = 1;
                    history.MaTN = maTn;
                    history.ToMobile = sdt2;
                    history.Contents = contents;

                    db.smsHistories.InsertOnSubmit(history);
                    db.SubmitChanges();

                    #endregion
                }
                //else return 4; // không có số điện thoại
            }

            error = rs != true ? 0 : 4;

            return error;
        }

        public static int SendSmsStafZalo(string tieuDe, string noiDung, byte maTn, int maBp, string token, int? MaNV, string ghichu)
        {

            var db = new MasterDataContext();
            int error = 0;

            // trường trộn
            var template = Building.SMS.Class.Common.GetTemplate(maTn, 1);
            if (template.TemplateId == 0) return 2; // không có mẫu template

            List<NoiDungYeuCau> lNoiDung = new List<NoiDungYeuCau>();
            lNoiDung.Add(new NoiDungYeuCau { TieuDe = tieuDe, NoiDung = noiDung });

            var dt = new DataTable();
            dt = SqlCommon.LINQToDataTable(lNoiDung);
            DataRow rData = dt.Rows[0];
            var contents = Building.SMS.Class.Common.GetContents(template.Contents, 1, rData, "YcNhanVien");
            if (ghichu != "")
                contents = contents + " .Ghi chú: " + ghichu;
            if (MaNV == null)
            {
                // kiểm tra nhân viên nhận sms
                var users = db.SmsNhanViens.Where(_ => _.BuildingId == maTn & _.MaPB == maBp & _.IsDichVuYeuCau == true);
                foreach (var i in users)
                {
                    var user = db.tnNhanViens.FirstOrDefault(_ => _.MaNV == i.NhanVienId);
                    if (user != null & user.IdZalo != null & user.IdZalo != "")
                    {
                        var send = SendZalo(user.IdZalo, contents, token);
                        //Lưu lịch sử
                        tnNhanVien_history hs = new tnNhanVien_history();
                        hs.MaTN = maTn;
                        hs.ZaloID = user.IdZalo;
                        hs.Content = contents;
                        hs.DateCreate = DateTime.Now;
                        hs.StaffCreate = Library.Common.User.MaNV;
                        hs.Status = send.Item1;
                        hs.Message = send.Item2;
                        hs.MaNV = user.MaNV;
                        db.tnNhanVien_histories.InsertOnSubmit(hs);
                        db.SubmitChanges();
                    }

                }
            }
            else
            {
                // kiểm tra nhân viên nhận sms
                var user = db.tnNhanViens.FirstOrDefault(_ => _.MaNV == MaNV);
                if (user != null & user.IdZalo != null & user.IdZalo != "")
                {
                    var send = SendZalo(user.IdZalo, contents, token);
                    //Lưu lịch sử
                    tnNhanVien_history hs = new tnNhanVien_history();
                    hs.MaTN = maTn;
                    hs.ZaloID = user.IdZalo;
                    hs.Content = contents;
                    hs.DateCreate = DateTime.Now;
                    hs.StaffCreate = Library.Common.User.MaNV;
                    hs.Status = send.Item1;
                    hs.Message = send.Item2;
                    hs.MaNV = user.MaNV;
                    db.tnNhanVien_histories.InsertOnSubmit(hs);
                    db.SubmitChanges();
                }
            }
            return error;
        }

        public static Tuple<int, string> SendZalo(string smsid, string text, string token)
        {
            int err = 0;
            string mes = "";
            try
            {
                ZaloClient client = new ZaloClient(token);
                JObject result = client.sendTextMessageToUserId(smsid, text);
                JObject jObject = JObject.Parse(result.ToString());
                var lt = jObject.ToObject<clresult>();

                if (lt.error != "0")
                {
                    err = Convert.ToInt32(lt.error);
                    mes = lt.message;
                }
                else
                {
                    err = Convert.ToInt32(lt.error);
                    mes = lt.message;
                }
            }
            catch (Exception ex)
            {
                err = -1;
                mes = ex.Message;
            }
            return new Tuple<int, string>(err, mes);
        }

        private class NoiDungYeuCau
        {
            public string NoiDung { get; set; }
            public string TieuDe { get; set; }
        }
        public class clresult
        {
            public string error { get; set; }
            public string message { get; set; }
        }
    }
}
