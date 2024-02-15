using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using DevExpress.XtraRichEdit.API.Native;
using Library;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace Building.SMS.Class
{
    public static class Common
    {
        private static MasterDataContext db = new MasterDataContext();
        public static SmsSouthTelecom SensMtSms(string brandName, string phoneNumber, string message)
        {
            //byte[] bytes = Encoding.UTF8.GetBytes(message);
            //string base64 = Convert.ToBase64String(bytes);
            // AUTHORIZATION_KEYS:c2Fjb21yZWFsX206MjhlYmIyMw==

            Dictionary<string, string> obj = new Dictionary<string, string>
            {
                {"from", brandName},
                {"to", phoneNumber},
                {"text", message}
            };
            var client = new RestClient("http://api-01.worldsms.vn/webapi/sendSMS");
            //var credentials = this.EncodeBasicAuthenticationCredentials("UserA", "123");
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", JsonConvert.SerializeObject(obj), ParameterType.RequestBody);
            request.AddHeader("Authorization", "Basic " + "c2Fjb21yZWFsX206MjhlYmIyMw==");
            IRestResponse reponse = client.Execute(request);

            JObject jObj = JObject.Parse(reponse.Content);
            SmsSouthTelecom result = jObj.ToObject<SmsSouthTelecom>();
            return result;
        }

        public static BrandNameCheck GetBrandName(byte maTn)
        {
            var brandName = db.SmsBrandNames.FirstOrDefault(_ => _.BuildingId == maTn);
            return brandName!=null?new BrandNameCheck{ Id = brandName.Id, BrandName = brandName.BrandName} : new BrandNameCheck{Id = 0, BrandName = ""};
        }

        public static BuildingTemplate GetBuildingTemplate(byte maTn, int tyleId)
        {
            var buildingTemplate = db.SmsBuildings.FirstOrDefault(_ => _.BuildingId == maTn & _.TyleId == tyleId);
            return buildingTemplate != null ? new BuildingTemplate {Id = buildingTemplate.Id, TemplateId = buildingTemplate.TemplateId} : new BuildingTemplate {Id = 0, TemplateId = 0};
        }

        public static Template GetTemplate(byte maTn, int tyleId)
        {
            var buildingTemplateId = GetBuildingTemplate(maTn, tyleId);
            var t = db.SmsTemplateDvs.FirstOrDefault(_ => _.Id == buildingTemplateId.TemplateId);
            return t != null ? new Template {TemplateId = buildingTemplateId.TemplateId, Contents = t.Contents} : new Template {TemplateId = 0, Contents = ""};
        }

        public static string GetContentsTemplate(int id)
        {
            var t = db.SmsTemplateDvs.FirstOrDefault(_ => _.Id == id);
            return t != null ? t.Contents : "";
        }

        public static string GetContents(string contents, int tyleId, DataRow rData, string groupSub)
        {
            var db = new MasterDataContext();
            var fields = db.SmsFields.Where(_ => _.GroupId == tyleId & _.GroupSub.ToLower() == groupSub.ToLower());

            var ctlRtf = new DevExpress.XtraRichEdit.RichEditControl();
            ctlRtf.Text = contents;

            #region Trộn trường

            //DataRow rData = dt.Rows[0];
            foreach (var i in fields)
            {
                ctlRtf.Document.ReplaceAll(i.Field, rData[i.Symbol].ToString(), SearchOptions.None);
            }

            #endregion
            return ctlRtf.Text;
        }

        public static int SendSms(string content, string brandName, string sdt, int? brandNameId, int? templateId, byte maTn, int? MaKh, int? MaMb, string HoTenKh, string MaSoMb)
        {
            // gửi sms
            int error = 0;
            var db = new MasterDataContext();


            var history = new smsHistory();

            history.ResultSend = "Thành công";
            Building.SMS.Class.Common.SmsSouthTelecom result = Building.SMS.Class.Common.SensMtSms(brandName, sdt, content);

            if (result.status != "1")
            {
                error = 5;
                history.ResultSend = "Error: " + result.errorcode + " - " + result.description;
            }

            #region Lưu lịch sử gửi sms


            history.BrandID = brandNameId;
            history.BrandName = brandName;
            history.DateCreate = DateTime.UtcNow.AddHours(7);
            history.FormID = templateId;
            history.IsAds = false;
            history.LastSend = 1;
            history.ToMobile = sdt;
            history.Contents = content;
            history.MaKH = MaKh;
            history.MaMB = MaMb;
            history.HoTenKH = HoTenKh;
            history.MaSoMB = MaSoMb;

            history.MaTN = maTn;
            db.smsHistories.InsertOnSubmit(history);
            db.SubmitChanges();

            #endregion

            return error;
        }

        public class BrandNameCheck
        {
            public int? Id { get; set; }
            public string BrandName { get; set; }
        }

        public class BuildingTemplate
        {
            public int? Id { get; set; }
            public int? TemplateId { get; set; }
        }

        public class Template
        {
            public int? TemplateId {get;set;}
            public string Contents {get;set;}
        }

        public class SmsSouthTelecom
        {
            public string from { get; set; }
            public string to { get; set; }
            public string text { get; set; }
            public int? unicode { get; set; }
            public int? dlr { get; set; }
            public string smsid { get; set; }
            public string campaignid { get; set; }
            public string status { get; set; }
            public string mnp { get; set; }
            public string carrier { get; set; }
            public string errorcode { get; set; }
            public string description { get; set; }
        }
    }
}
