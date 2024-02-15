using Library;
using Newtonsoft.Json.Linq;
using System;
using ZaloDotNetSDK;

namespace Building.SMSZalo
{
    public class ZaloSend
    {
        public static Tuple<int, int,string> smsZALO(byte? MaTN,string token, string text,int MaKH, string smsid, string pdf)
        {
            var db = new MasterDataContext();
            int tc = 0;
            int tb = 0; 
            var access_token = token;
            try
            {
                ZaloClient client = new ZaloClient(access_token);
                JObject result = client.sendTextMessageToUserId(smsid, text);
                JObject jObject = JObject.Parse(result.ToString());
                var lt = jObject.ToObject<clresult>();

                //Upload file
                bool? isFile = false;
                if(pdf != "")
                {
                    ZaloFile file = new ZaloFile(pdf);
                    file.setMediaTypeHeader("application/pdf");
                    JObject resultfile = client.uploadFileForOfficialAccountAPI(file);
                    JToken jData = resultfile["data"];
                    var tokenFile = jData["token"];
                    JObject resultSend = client.sendFileToUserId(smsid, tokenFile.ToString());
                    var ltfile = resultSend.ToObject<clresult>();
                    if(lt.error == "0")
                    {
                        isFile = true;
                    }
                }

                if (lt.error != "0")
                {
                    tb = tb + 1;
                    web_ZaloHistory hs = new web_ZaloHistory();
                    hs.MaTN = MaTN;
                    hs.ZaloID = smsid;
                    hs.Content = text;
                    hs.DateCreate = DateTime.Now;
                    hs.StaffCreate = Library.Common.User.MaNV;
                    hs.Status = 0;
                    hs.Message = lt.message;
                    hs.MaKH = MaKH;
                    hs.isFile = isFile;
                    db.web_ZaloHistories.InsertOnSubmit(hs);
                    db.SubmitChanges();
                }
                else
                {
                    tc = tc + 1;
                    web_ZaloHistory hs = new web_ZaloHistory();
                    hs.MaTN = MaTN;
                    hs.ZaloID = smsid;
                    hs.Content = text;
                    hs.DateCreate = DateTime.Now;
                    hs.StaffCreate = Library.Common.User.MaNV;
                    hs.Status = 1;
                    hs.Message = lt.message;
                    hs.MaKH = MaKH;
                    hs.isFile = isFile;
                    db.web_ZaloHistories.InsertOnSubmit(hs);
                    db.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                tb = tb + 1;
            }
            return new Tuple<int, int, string>(tc, tb, "Đã gửi sms thành công!");
        }
        public static string ReplaceFirstOccurrence(string Source, string Find, string Replace)
        {
            int Place = Source.IndexOf(Find);
            if (Place == -1)
            {
                return Source;
            }
            string result = Source.Remove(Place, Find.Length).Insert(Place, Replace);
            return result;
        }
        public class clresult
        {
            public string error { get; set; }
            public string message { get; set; }
        }

        public class clresultFile
        {
            public string token { get; set; }
        }
        public class clresultUpfile
        {
            public string token { get; set; }
        }
    }
}