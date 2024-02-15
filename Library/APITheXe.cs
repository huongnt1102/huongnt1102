using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Library;
using RestSharp;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Data.Linq.SqlClient;

namespace Library
{
    public static class APITheXe
    {
        static string GetLinkAPI(byte MaTN)
        {
            using (var db = new MasterDataContext())
            {
                return db.tnToaNhas.Single(o => o.MaTN == MaTN).Link_API;
            }
        }

        public static AccessToken GetToken(byte MaTN)
        {
            try
            {
                using (var db = new MasterDataContext())
                {
                    var config = db.tnToaNhas.Single(o => o.MaTN == MaTN);
                    if (config.Link_API == null)
                        return null;
                    Dictionary<string, string> Dic = new Dictionary<string, string>();
                    Dic.Add("Username", config.User_API);
                    Dic.Add("Password", config.Password_API);

                    var client = new RestClient(GetLinkAPI(MaTN) + "login");  
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("Content-Type", "application/json");  
                    var body = JsonConvert.SerializeObject(Dic);

                    request.AddParameter("application/json", body, ParameterType.RequestBody);
                    IRestResponse response = client.Execute(request);
                    JObject obj = JObject.Parse(response.Content);

                    JObject arrData = (JObject)obj["data"];

                    if (arrData == null)
                        return null;

                    var Data = arrData.ToObject<AccessToken>();
                    Data.Link_API = config.Link_API;
                    return Data;
                }
            }
            catch
            {
                return null;
            }
            return null;
        }

        public static bool GiaHanVeThang(byte MaTN,string blockCode)
        {
            using (var db = new MasterDataContext())
            {
                var objToKen = GetToken(MaTN);
                if (objToKen == null)
                    return false;
                var ltGiaHan = (from o in db.dvgxTheXes
                                where !o.NgungSuDung.GetValueOrDefault() & o.MaTN == MaTN & (o.MaTheChip ?? "").Trim() != ""
                                & o.TuNgay != null
                                & o.DenNgay != null
                                select new
                                {
                                    o.MaTheChip,
                                    o.TuNgay,
                                    o.DenNgay
                                }).AsEnumerable()
                               .Select(p => new
                               {
                                   rowID = GetRowIDVeThang(MaTN,blockCode, p.MaTheChip),
                                   dateStart = p.TuNgay.Value.ToString("yyyy-MM-dd 00:00:00"),
                                   dateEnd = p.DenNgay.Value.ToString("yyyy-MM-dd 00:00:00")
                               }).Where(o => o.rowID != null)
                               .ToList();

                var client = new RestClient(string.Format(GetLinkAPI(MaTN) + "renewal-ticket-month?blockCode={0}&IDAccountCreate={1}", blockCode, 1));
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/json");  
                request.AddHeader("Authorization", "Bearer " + objToKen.Token);

                var content = JsonConvert.SerializeObject(ltGiaHan);
                request.AddParameter("application/json", content, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);

                JObject Jobj = JObject.Parse(response.Content);
                Result result = Jobj.ToObject<Result>();

                if (result != null)
                {
                    if (result.errorCode == "0")
                        return true;
                    else
                    {
                        DialogBox.Error("Đồng bộ lỗi:" + result.message);
                        return false;
                    }
                }
            }

            return true;
        }
        private static string GetBlockCode(int? MaMB)
        {
            string sblockcode = "";
            using (var db = new MasterDataContext())
            {
                var obj = (from mb in db.mbMatBangs
                           join tl in db.mbTangLaus on mb.MaTL equals tl.MaTL
                           join kn in db.mbKhoiNhas on tl.MaKN equals kn.MaKN
                           where mb.MaMB == MaMB
                           select new { kn.BlockCode }).FirstOrDefault();
                if (obj != null)
                    sblockcode = obj.BlockCode;
            }
            return sblockcode;
        }
        public static bool GiaHanVeThang(byte MaTN, List<int> ltIDTheXe,string blockCode)
        {
            using (var db = new MasterDataContext())
            {
                var objToKen = GetToken(MaTN);
                if (objToKen == null)
                    return false;
                var ltGiaHan = (from o in db.dvgxTheXes
                                where !o.NgungSuDung.GetValueOrDefault() & o.MaTN == MaTN & (o.MaTheChip ?? "").Trim() != ""
                                & o.TuNgay != null
                                & o.DenNgay != null
                                & ltIDTheXe.Contains(o.ID)
                                select new
                                {
                                    o.MaTheChip,
                                    o.TuNgay,
                                    o.DenNgay,
                                    o.MaMB
                                }).AsEnumerable()
                               .Select(p => new
                               {
                                   rowID = GetRowIDVeThang( MaTN,GetBlockCode(p.MaMB), p.MaTheChip),
                                   dateStart = p.TuNgay.Value.ToString("yyyy-MM-dd 00:00:00"),
                                   dateEnd = p.DenNgay.Value.ToString("yyyy-MM-dd 00:00:00")
                               }).Where(o => o.rowID != null)
                               .ToList();

                var client = new RestClient(string.Format(GetLinkAPI(MaTN) + "renewal-ticket-month?blockCode={0}&IDAccountCreate={1}",blockCode, 1));
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/json");  
                request.AddHeader("Authorization", "Bearer " + objToKen.Token);

                var content = JsonConvert.SerializeObject(ltGiaHan);
                request.AddParameter("application/json", content, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);

                JObject Jobj = JObject.Parse(response.Content);
                Result result = Jobj.ToObject<Result>();

                if (result != null)
                {
                    if (result.errorCode == "0")
                        return true;
                    else
                    {
                        DialogBox.Error("Đồng bộ lỗi:" + result.message);
                        return false;
                    }
                }
            }

            return true;
        }

        public static ThongTinVeThang SearchVeThang(byte MaTN,string blockCode, string SoThe)
        {
            var objToKen = GetToken(MaTN);
            if (objToKen == null)
                return null;
            IEnumerable<ThongTinVeThang> ltData;
            var client = new RestClient(objToKen.Link_API + "search-ticket-month");  
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");  

            request.AddHeader("Authorization", "Bearer " + objToKen.Token);

            var obj = new TimKiemVeThang { blockCode = blockCode, SearchString = SoThe };
            var body = JsonConvert.SerializeObject(obj);
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            JObject Jobj = JObject.Parse(response.Content);

            JArray arrData = (JArray)Jobj["data"];

            if (arrData == null)
                return null;

            ltData = arrData.ToObject<IEnumerable<ThongTinVeThang>>();
            return ltData.FirstOrDefault();
        }
        public static ThongTinLoaiXe SearchLoaiXe(byte MaTN, string blockCode, string TenLoaiXe)
        {
            var objToKen = GetToken(MaTN);
            if (objToKen == null)
                return null;
            IEnumerable<ThongTinLoaiXe> ltData;
            var client = new RestClient(string.Format(objToKen.Link_API + "sreach-part?blockCode={0}&Name={1}",blockCode,TenLoaiXe));
            var request = new RestRequest(Method.GET);
            request.AddHeader("Content-Type", "application/json");  

            request.AddHeader("Authorization", "Bearer " + objToKen.Token);

            var obj = new TimKiemLoaiXe { blockCode = blockCode, SearchLoaiXe = TenLoaiXe };
            var body = JsonConvert.SerializeObject(obj);
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            JObject Jobj = JObject.Parse(response.Content);

            JObject arrData = (JObject)Jobj["data"];

            if (arrData == null)
                return null;
            var Data = arrData.ToObject<ThongTinLoaiXe>();
            return Data;
        }
        public static int? GetRowIDVeThang(byte MaTN,string blockCode, string SoThe)
        {
            AccessToken objToKen = GetToken(MaTN);
            if (objToKen == null)
                return null;
            IEnumerable<ThongTinVeThang> ltData;

            var client = new RestClient(GetLinkAPI(MaTN) + "search-ticket-month");  
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");  

            request.AddHeader("Authorization", "Bearer " + objToKen.Token);

            var obj = new TimKiemVeThang { blockCode = blockCode, SearchString = SoThe };
            var body = JsonConvert.SerializeObject(obj);
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            JObject Jobj = JObject.Parse(response.Content);

            JArray arrData = (JArray)Jobj["data"];

            if (arrData == null)
                return null;

            ltData = arrData.ToObject<IEnumerable<ThongTinVeThang>>();
            return ltData.FirstOrDefault().rowid;
        }

        public static IEnumerable<TheChuaTaoVeThang> DanhSachTheXeChuaTaoVeThang(byte MaTN,string blockCode)
        {
            AccessToken objToKen = GetToken(MaTN);
            if (objToKen == null)
                return null;
            var client = new RestClient(GetLinkAPI(MaTN) + "list-card-create-ticketmonth");  
            var request = new RestRequest(Method.GET);
            request.AddParameter("blockCode", blockCode);
            request.AddHeader("Authorization", "Bearer " + objToKen.Token);
            IRestResponse response = client.Execute(request);

            JObject obj = JObject.Parse(response.Content);

            JArray arrOrder = (JArray)obj["data"];
            var ltData = arrOrder.ToObject<IEnumerable<TheChuaTaoVeThang>>();

            return ltData;
        }

        // Đăng ký vé tháng
        public static IEnumerable<ThongTinVeThang> DangKyVeThang(TaoVeThangMoi obj, byte MaTN,string blockCode)
        {
            AccessToken objToKen = GetToken(MaTN);
            if (objToKen == null)
                return null;

            var client = new RestClient(GetLinkAPI(MaTN) + "create-ticket-month");  
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");  
            request.AddHeader("Authorization", "Bearer " + objToKen.Token);
            var content = JsonConvert.SerializeObject(obj);
            request.AddParameter("application/json", content, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            JObject Jobj = JObject.Parse(response.Content);
            Result result = Jobj.ToObject<Result>();

            if (result.errorCode == "0")
            {
                JArray arrData = (JArray)Jobj["data"];

                if (arrData == null)
                    return null;

                var Data = arrData.ToObject < IEnumerable<ThongTinVeThang>>();

                return Data;
            }
            else
            {
                DialogBox.Error("Đồng bộ lỗi:" + result.message);
                return null;
            }
        }

        public static ResultUpdateTicketMonth CapNhatVeThang(CapNhatTheXe obj, byte MaTN, string blockCode)
        {
            AccessToken objToKen = GetToken(MaTN);
            if (objToKen == null)
                return null;
            var client = new RestClient(GetLinkAPI(MaTN) + "edit-ticket-month");  
            var request = new RestRequest(Method.PUT);
            request.AddHeader("Content-Type", "application/json");  
            request.AddHeader("Authorization", "Bearer " + objToKen.Token);
            var content = JsonConvert.SerializeObject(obj);
            request.AddParameter("application/json", content, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            JObject Jobj = JObject.Parse(response.Content);
            Result result = Jobj.ToObject<Result>();

            if (result != null)
            {
                if (result.errorCode == "0")
                {
                    JObject arrData = (JObject)Jobj["data"];

                    if (arrData == null)
                        return null;

                    var Data = arrData.ToObject<ResultUpdateTicketMonth>();
                    return Data;
                }
                else
                {
                    DialogBox.Error(".Đồng bộ lỗi:" + result.message);
                    return null;
                }
            }

            return null;
        }

        public static bool NgungSuDung(string MaTheChip, byte MaTN, string blockCode)
        {
            AccessToken objToKen = GetToken(MaTN);
            if (objToKen == null)
                return false;

            var objTheCu = SearchVeThang(MaTN, blockCode, MaTheChip);

            NgungSuDungVeThang obj = new NgungSuDungVeThang
            {
                blockCode = blockCode,
                rowID = objTheCu.rowid
            };

            var client = new RestClient(GetLinkAPI(MaTN) + "block-ticket-month");  
            var request = new RestRequest(Method.PUT);
            request.AddHeader("Content-Type", "application/json");  
            request.AddHeader("Authorization", "Bearer " + objToKen.Token);
            var body = JsonConvert.SerializeObject(obj);
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            JObject Jobj = JObject.Parse(response.Content);
            Result result = Jobj.ToObject<Result>();

            if (result != null)
            {
                if (result.errorCode == "0")
                {
                    return true;
                }
                else
                {
                    DialogBox.Error(".Đồng bộ lỗi:" + result.message);
                    return false;
                }
            }

            return true;
        }

        public static bool KhoaThe(string MaTheChip, byte MaTN, string blockCode)
        {
            AccessToken objToKen = GetToken(MaTN);
            if (objToKen == null)
                return false;
            var objTheCu = SearchVeThang(MaTN, blockCode, MaTheChip);

            Dictionary<string, string> Dic = new Dictionary<string, string>();
            Dic.Add("blockCode", blockCode);
            Dic.Add("ID", MaTheChip);

            var client = new RestClient(GetLinkAPI(MaTN) + "insert-blacklist");  
            var request = new RestRequest(Method.PUT);
            request.AddHeader("Content-Type", "application/json");  
            request.AddHeader("Authorization", "Bearer " + objToKen.Token);
            var body = JsonConvert.SerializeObject(Dic);
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            JObject Jobj = JObject.Parse(response.Content);
            Result result = Jobj.ToObject<Result>();

            if (result != null)
            {
                if (result.errorCode == "0")
                {
                    return true;
                }
                else
                {
                    DialogBox.Error(".Đồng bộ lỗi:" + result.message);
                    return false;
                }
            }

            return true;
        }

        public static bool HuyKhoaThe(string MaTheChip, string rowID, byte MaTN, string blockCode)
        {
            AccessToken objToKen = GetToken(MaTN);

            if (objToKen == null)
                return false;

            Dictionary<string, string> Dic = new Dictionary<string, string>();
            Dic.Add("blockCode", blockCode);
            Dic.Add("ID", MaTheChip);
            Dic.Add("rowID", rowID);

            var client = new RestClient(GetLinkAPI(MaTN) + "delete-black-card");  
            var request = new RestRequest(Method.PUT);
            request.AddHeader("Content-Type", "application/json");  
            request.AddHeader("Authorization", "Bearer " + objToKen.Token);
            var body = JsonConvert.SerializeObject(Dic);
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            JObject Jobj = JObject.Parse(response.Content);
            Result result = Jobj.ToObject<Result>();

            if (result != null)
            {
                if (result.errorCode == "0")
                {
                    return true;
                }
                else
                {
                    DialogBox.Error(".Đồng bộ lỗi:" + result.message);
                    return false;
                }
            }

            return true;
        }

        public static IEnumerable<ThongTinVeThang> GetLichSuVeThang(byte MaTN, string ID, string blockCode)
        {
            AccessToken objToKen = GetToken(MaTN);
            if (objToKen == null)
                return null;

            var client = new RestClient(GetLinkAPI(MaTN) + "get-history-ticketmonth-create");  
            var request = new RestRequest(Method.GET);
            request.AddParameter("blockCode", blockCode);
            request.AddParameter("ID", ID);
            request.AddHeader("Authorization", "Bearer " + objToKen.Token);
            IRestResponse response = client.Execute(request);

            JObject obj = JObject.Parse(response.Content);

            JArray arrOrder = (JArray)obj["data"];
            var ltData = arrOrder.ToObject<IEnumerable<ThongTinVeThang>>();

            return ltData;
        }

        public static IEnumerable<BlackList> DanhSachKhoaThe(byte MaTN)
        {
            AccessToken objToKen = GetToken(MaTN);
            if (objToKen == null)
                return null;
            var client = new RestClient(GetLinkAPI(MaTN) + "get-blacklist");  
            var request = new RestRequest(Method.GET);
           // request.AddParameter("blockCode", objToKen.blockCode);
            request.AddHeader("Authorization", "Bearer " + objToKen.Token);
            IRestResponse response = client.Execute(request);

            JObject obj = JObject.Parse(response.Content);

            JArray arrOrder = (JArray)obj["data"];
            var ltData = arrOrder.ToObject<IEnumerable<BlackList>>();

            return ltData;
        }

        public static IEnumerable<ThongTinVeThang> KichHoatThe(string MaTheChip, byte MaTN, string blockCode)
        {
            AccessToken objToKen = GetToken(MaTN);
            if (objToKen == null)
                return null;
            var objTheCu = SearchVeThang(MaTN,blockCode, MaTheChip);
            NgungSuDungVeThang obj = new NgungSuDungVeThang
            {
                blockCode =blockCode,
                rowID = objTheCu.rowid
            };

            var client = new RestClient(GetLinkAPI(MaTN) + "unblock-ticket-month");  
            var request = new RestRequest(Method.PUT);
            request.AddHeader("Content-Type", "application/json");  
            request.AddHeader("Authorization", "Bearer " + objToKen.Token);
            var body = JsonConvert.SerializeObject(obj);
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            JObject Jobj = JObject.Parse(response.Content);
            Result result = Jobj.ToObject<Result>();

            if (result != null)
            {
                if (result.errorCode == "0")
                {
                    if (Jobj["data"] != null)
                    {
                        JArray arrData = (JArray)Jobj["data"];
                        if (arrData == null)
                            return null;
                        var Data = arrData.ToObject<IEnumerable<ThongTinVeThang>>();
                        return Data;
                    }
                    else
                    {
                        return null;
                    }
                   
                   
                }
                else
                {
                    DialogBox.Error(".Đồng bộ lỗi:" + result.message);
                    return null;
                }
            }

            return null;
        }

        public static bool DoiThe(DoiThe_cls obj, byte MaTN, string blockCode)
        {
            AccessToken objToKen = GetToken(MaTN);
            if (objToKen == null)
                return false;
            var client = new RestClient(GetLinkAPI(MaTN) + "update-ticket-month-id");  
            var request = new RestRequest(Method.PUT);
            request.AddHeader("Content-Type", "application/json");  
            request.AddHeader("Authorization", "Bearer " + GetToken(MaTN));
            var content = JsonConvert.SerializeObject(obj);
            request.AddParameter("application/json", content, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            JObject Jobj = JObject.Parse(response.Content);
            Result result = Jobj.ToObject<Result>();

            if (result != null)
            {
                if (result.errorCode == "0")
                    return true;
                else
                {
                    DialogBox.Error("Đồng bộ lỗi:" + result.message);
                    return false;
                }
            }

            return true;
           
        }

        public static bool XoaTheXe(string MaTheChip, byte MaTN, string blockCode, int? rowID)
        {
            AccessToken objToKen = GetToken(MaTN);

            if (rowID == null)
                return false;
            //var objTheCu = SearchVeThang(MaTN,blockCode, MaTheChip);
            //if (objTheCu == null)
            //    return false;
            XoaTheXe_cls obj = new XoaTheXe_cls
            {
                blockCode = blockCode,
                rowID = rowID
            };

            var client = new RestClient(GetLinkAPI(MaTN) + "delete-ticket-month");  
            var request = new RestRequest(Method.PUT);
            request.AddHeader("Content-Type", "application/json");  
            request.AddHeader("Authorization", "Bearer " + objToKen.Token);
            var body = JsonConvert.SerializeObject(obj);
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            JObject Jobj = JObject.Parse(response.Content);
            Result result = Jobj.ToObject<Result>();

            if (result != null)
            {
                if (result.errorCode == "0")
                {
                    return true;
                }
                else
                {
                    DialogBox.Error(".Đồng bộ lỗi:" + result.message);
                    return false;
                }
            }

            return true;
        }

        public static IEnumerable<ThongTinVeThang> Get_VeThangDangSuDung(byte MaTN)
        {
            AccessToken objToKen = GetToken(MaTN);
            if (objToKen == null)
                return null;
            var client = new RestClient(GetLinkAPI(MaTN) + "list-ticket-month");  
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", "Bearer " + objToKen.Token);
           // request.AddParameter("Code", objToKen.Code);

            IRestResponse response = client.Execute(request);

            JObject obj = JObject.Parse(response.Content);

            JArray arrOrder = (JArray)obj["data"];
            var ltData = arrOrder.ToObject<IEnumerable<ThongTinVeThang>>();

            return ltData;
        }
        public static ThongTinThongKe ThongKeTheXeSuDung(byte MaTN,DateTime TuNgay, DateTime DenNgay)
        {
            AccessToken objToKen = GetToken(MaTN);
            if (objToKen == null)
                return null;
            ThongKeTheXePOST obj = new ThongKeTheXePOST
            {
                fromDate = TuNgay.ToString("yyyy-MM-dd 00:00:00"),
                toDate = DenNgay.ToString("yyyy-MM-dd 00:00:00")
            };
            var client = new RestClient(GetLinkAPI(MaTN) + "list-card-report");  
            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", "Bearer " + objToKen.Token);
            var body = JsonConvert.SerializeObject(obj);
            request.AddParameter("application/json", body, ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);

            JObject Jobj = JObject.Parse(response.Content);
            Result result = Jobj.ToObject<Result>();
            if (result != null)
            {
                if (result.errorCode == "0")
                {
                    if (Jobj["data"] != null)
                    {
                        JObject arrData = (JObject)Jobj["data"];
                        if (arrData == null)
                            return null;
                        var Data = arrData.ToObject<ThongTinThongKe>();
                        return Data;
                    }
                    else
                    {
                        return null;
                    }


                }
                else
                {
                    DialogBox.Error(".Đồng bộ lỗi:" + result.message);
                    return null;
                }
            }
            return null;
        }
        public static IEnumerable<LichSuTheXe> GetLichSu(DateTime fromDate, DateTime toDate, string MaTheChip, byte MaTN, string blockCode)
        {
            AccessToken objToKen = GetToken(MaTN);
            ThongTinVeThang objTheCu;
            objTheCu = SearchVeThang(MaTN,blockCode, MaTheChip);

            var client = new RestClient(GetLinkAPI(MaTN) + "search-car-ticketmonth");  
            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", "Bearer " + objToKen.Token);
            request.AddParameter("blockCode", blockCode);
            request.AddParameter("fromDate", fromDate.ToString("yyyy-MM-dd 00:00:00"));
            request.AddParameter("toDate", toDate.ToString("yyyy-MM-dd 00:00:00"));

            if (objTheCu != null)
                request.AddParameter("Stt", objTheCu.stt);

            IRestResponse response = client.Execute(request);
            JObject obj = JObject.Parse(response.Content);
            JArray arrData = (JArray)obj["data"];

            if (arrData == null)
                return null;

            var ltData = arrData.ToObject<IEnumerable<LichSuTheXe>>();
            return ltData;
        }

        public static IEnumerable<LoaiXe> DanhSachLoaiXe(byte MaTN)
        {
            AccessToken objToKen = GetToken(MaTN);
            if (objToKen == null)
                return null;
            var client = new RestClient(GetLinkAPI(MaTN) + "list-part");  
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", "Bearer " + objToKen.Token);
           // request.AddParameter("blockCode", objToKen.blockCode);
            IRestResponse response = client.Execute(request);

            JObject obj = JObject.Parse(response.Content);

            JArray arrOrder = (JArray)obj["data"];
            var ltData = arrOrder.ToObject<IEnumerable<LoaiXe>>();

            return ltData;
        }

        public class LichSuTheXe
        {
            public string ID { get; set; }
            public DateTime? TimeStart { get; set; }
            public DateTime? TimeEnd { get; set; }
            public string Digit { get; set; }
            public decimal? CostIn { get; set; }
            public decimal? Cost { get; set; }
            public string Part { get; set; }
            public string Seri { get; set; }
            public string IDTicketMonth { get; set; }
            public string IDPart { get; set; }
            public string MatThe { get; set; }
            public string Computer { get; set; }
            public string Note { get; set; }
            public string Account { get; set; }
            public decimal? CostBefore { get; set; }
            public string DateUpdate { get; set; }
            public string ImagesURL1 { get; set; }
            public string ImagesURL2 { get; set; }
            public string ImagesURL3 { get; set; }
            public string ImagesURL4 { get; set; }

        }

        public class BlackList
        {
            public string RowID	 { get; set; }
            public string Stt { get; set; }
            public string ID { get; set; }
            public DateTime?  ProcessDate { get; set; }
            public string Digit { get; set; }
            public string TenKH { get; set; }
            public string CMND { get; set; }
            public string Company { get; set; }
            public string Email { get; set; }
            public string Address { get; set; }
            public string CarKind { get; set; }
            public string IDPart { get; set; }
            public DateTime? DateStart { get; set; }
            public DateTime? DateEnd { get; set; }
            public string Note { get; set; }
            public decimal? Amount { get; set; }
            public decimal? ChargesAmount { get; set; }
            public string Statu { get; set; }
            public string Account { get; set; }
            public string Image { get; set; }
            public string DayUnLimit { get; set; }
            public string Name { get; set; }
            public string BlockCode { get; set; }

        }

        public class DoiThe_cls
        {
          public string blockCode { get; set; }
          public int RowID { get; set;}
          public string NewID { get; set;}
          public int Stt { get; set; }
          public string AccountID { get; set; }
        }

        public class AccessToken
        {
            public string Token { get; set; }
            public string blockCode { get; set; }
            public string Link_API { get; set; }
        }

        public class ThongTinVeThang
        {
            public int? rowid { get; set;}
            public int? stt {get; set;}
            public string id {get; set;}
            public string Digit { get; set; } // Biển số nhập vào từ người dùng
            public string TenKH { get; set; } // Tên khách hàng nhập vào từ người dùng
            public string CMND { get; set; } // CMND nhập vào từ người dùng
            public string Company { get; set; } // Tên Công ty nhập vào từ người dùng
            public string Email { get; set; }
            public string Address { get; set; }
            public string CarKind { get; set; } // Hiêu xe nhập vào từ người dùng vd : Yamaha-Sririus , Honda-Wave
            public string Note { get; set; } // Ghi chú nhập từ người dùng
            public DateTime DateStart { get; set; } // Ngày đăng ký nhập từ người dùng
            public DateTime DateEnd { get; set; } // Ngày hết hạn nhập từ người dùng
            public int? Amount { get; set; } // Tiền thu thang nhập từ người dụng hoặc dựa theo tiền thu tháng mặc định của từng loại xe quy định sẵn
            public DateTime DayUnLimit { get; set; } // Ngày kích hoạt thẻ
            public string blockCode { get; set; }
        }
        public class ThongTinLoaiXe
        {
            public string id { get; set; }
        }

        public class Result
        {
            public string errorCode { get; set; }
            public string message { get; set; }
        }

        public class TheChuaTaoVeThang
        {
            public int? Identify { get; set; } // - Số thứ tự thẻ

            public string ID { get; set; } //- Mã thẻ chíp
            public string Using { get; set; } //-Tình Trạng sử dụng đang sử dụng hay khóa
            public string Name { get; set; } // Loại xe
            public string TicketMonth { get; set; } //Tình trạng tạo vé tháng của thẻ.

            public decimal? Amount { get; set; } // Tiền Thu vé tháng (Cấu hình sẵn theo từng loại xe)
        }

        public class LoaiXe
        {
            public int? ID { get; set; } // - ID loại xe
            public int? Limit { get; set; } // 0

            public string Name { get; set; } // Tên loại xe
            public string Sign { get; set; } // Ký hiệu loại xe

            public decimal? Amount { get; set; } // Tiền Thu Tháng

        }

        public class TaoVeThangMoi
        {
            public int? IdAccountCreate { get; set; } // ID người tạo (ID nhận được khi login Mặc giá trị bằng 1)
            public int? Stt { get; set; } // Stt (Chọn từ ds thẻ chưa tạo vé tháng theo stt của thẻ trong ds thẻ)
            public int? IDPart { get; set; } // ID loại xe (lấy từ danh sách loại xe) để quy định loại xe cho vé tháng

            public string ID { get; set; } // Mã thẻ (Chọn từ ds thẻ chưa tạo vé tháng)
            public string blockCode { get; set; } // Mã tòa nhà M1 hoặc M2
            public string Digit { get; set; } // Biển số nhập vào từ người dùng
            public string TenKH { get; set; } // Tên khách hàng nhập vào từ người dùng
            public string CMND { get; set; } // CMND nhập vào từ người dùng
            public string Company { get; set; } // Tên Công ty nhập vào từ người dùng
            public string Email { get; set; }
            public string Address { get; set; }
            public string CarKind { get; set; } // Hiêu xe nhập vào từ người dùng vd : Yamaha-Sririus , Honda-Wave
            public string Note { get; set; } // Ghi chú nhập từ người dùng
            public string fromDate { get; set; } // Ngày đăng ký nhập từ người dùng
            public string toDate { get; set; } // Ngày hết hạn nhập từ người dùng
            public int Amount { get; set; } // Tiền thu thang nhập từ người dụng hoặc dựa theo tiền thu tháng mặc định của từng loại xe quy định sẵn
            
        }

        public class CapNhatTheXe
        {
            public int? rowID {get;set;} // 0,rowID lấy từ thẻ muốn chỉnh sửa trong Danh sách vé tháng API get ds Vé tháng
            public int? IdAccountEdit {get;set;} // "1", Lấy từ API login hoặc mặc định là 1
            public int? Stt {get;set;} //"Stt": 0, Người dùng nhập thông tin cần cập nhật, giá trị bằng null hoặc Rỗng mặc định giữ thông tin củ.
            public int? IDPart { get; set; } // "5",  ID loại xe muốn sửa lấy từ API get Part

            public string blockCode { get; set; } // "M1", Mã tòa nhà 
            public string Digit {get;set;} // "string", Người dùng nhập thông tin cần cập nhật, giá trị bằng null hoặc Rỗng mặc định giữ thông tin củ.
            public string TenKH { get; set; } //: "string", Người dùng nhập thông tin cần cập nhật, giá trị bằng null hoặc Rỗng mặc định giữ thông tin củ.
            public string CMND { get; set; } //: "string", Người dùng nhập thông tin cần cập nhật, giá trị bằng null hoặc Rỗng mặc định giữ thông tin củ.
            public string Company { get; set; } //: "string", Người dùng nhập thông tin cần cập nhật, giá trị bằng null hoặc Rỗng mặc định giữ thông tin củ.
            public string Email { get; set; } // "string", Người dùng nhập thông tin cần cập nhật, giá trị bằng null hoặc Rỗng mặc định giữ thông tin củ.
            public string Address { get; set; } // "string", Người dùng nhập thông tin cần cập nhật, giá trị bằng null hoặc Rỗng mặc định giữ thông tin củ.
            public string CarKind { get; set; }// "string", Người dùng nhập thông tin cần cập nhật, giá trị bằng null hoặc Rỗng mặc định giữ thông tin củ.
            public string Note { get; set; } // "string", Người dùng nhập thông tin cần cập nhật, giá trị bằng null hoặc Rỗng mặc định giữ thông tin củ.

            public int Amount { get; set; } // 0 Người dùng nhập thông tin cần cập nhật, giá trị bằng null hoặc Rỗng mặc định giữ thông tin củ

            public string fromDate { get; set; } // Người dùng nhập thông tin cần cập nhật, giá trị bằng null hoặc Rỗng mặc định giữ thông tin củ.
            public string toDate { get; set; } // Người dùng nhập thông tin cần cập nhật, giá trị bằng null hoặc Rỗng mặc định giữ thông tin củ.
        }

        public class TimKiemVeThang
        {
            public string blockCode { get; set; } // Mã tòa nhà M1 hoặc M2
            public string SearchString {get;set;} //Nội Dung tìm thẻ vd : tìm theo Stt thẻ , tên , biển số ....
        }
        public class TimKiemLoaiXe
        {
            public string blockCode { get; set; } // Mã tòa nhà M1 hoặc M2
            public string SearchLoaiXe { get; set; } //Tên loại xe
        }

        public class NgungSuDungVeThang
        {
            public string blockCode { get; set; } // "M1", Mã tòa nhà M1 hoặc M2
            public int? rowID { get; set; } // Row ID thẻ cần ngưng sử dụng Lấy từ Api ds thẻ tháng hoặc tìm kiếm vé tháng
        }

        public class DanhSachTheNgungSuDung
        {
            public string blockCode { get; set; }
            public string SearchString { get; set; } // Nội Dung tìm thẻ vd : tìm theo Stt thẻ , tên , biển số ....
        }

        public class KichHoatLaiThe
        {
            public string blockCode { get; set; } // Mã tòa nhà M1 hoặc M2
            public int? rowID { get; set; } // Row ID thẻ cần kích hoạt lại Lấy từ Api ds thẻ tháng hoặc tìm kiếm vé tháng
        }

        public class XoaTheXe_cls
        {
            public string blockCode { get; set; } // Mã tòa nhà M1 hoặc M2

            public int? rowID { get; set; } // Row ID thẻ cần kích hoạt lại Lấy từ Api ds thẻ tháng hoặc tìm kiếm vé tháng
        }

        public class TimKiemXeRaVao
        {
            public string blockCode { get; set; } // Mã tòa nhà
            public string idIn { get; set; } // thông tin bổ sung có thể có hoặc không Lọc theo ID nhân viên vào
            public string idOut { get; set; } // thông tin bổ sung có thể có hoặc không Lọc theo ID ID Nhân viên ra
            public string Stt { get; set; } // thông tin bổ sung có thể có hoặc không Lọc theo STT 
            public string Bienso { get; set; } // thông tin bổ sung có thể có hoặc không Lọc theo Biển số

            public DateTime? fromDate { get; set; } // thời gian vào
            public DateTime? toDate { get; set; } // thời gian ra
        }

        public class XemDoanhThuVeThang
        {
            public string blockCode { get; set; }
            public DateTime? fromDate { get; set; }
            public DateTime? toDate { get; set; }
        }
        public class ThongTinThongKe
        {
            public int TotalTicketGuestUsingNow { get; set; }
            public int TotalTicketMonth { get; set; }
            public int TotalTicketMonthUsing { get; set; }
            public int TotalStopUsingTicketMonth { get; set; }
            public int TotalBlackTicketMonth { get; set; }
            public int TotalCreateNew { get; set; }
        }
        public class ThongKeTheXePOST
        {
            public string fromDate { get; set; } // Từ ngày
            public string toDate { get; set; } // Đến ngày
        }
        public class ResultCapNhatVeThang
        {
            public string id { set; get; }
            public int? rowid { set; get; }
            public DateTime? DayUnLimit { set; get; }
        }
        public class ResultUpdateTicketMonth
        {
            public int? RowID { set; get; }
            public int? Stt { set; get; }
            public string ID { set; get; }
            public DateTime? ProcessDate { set; get; }
            public string Digit { set; get; }
            public string TenKH { set; get; }
            public string CMND { set; get; }
            public string Company { set; get; }
            public string Email { set; get; }
            public string Address { set; get; }
            public string CarKind { set; get; }
            public int? IDPart { set; get; }
            public DateTime? DateStart { set; get; }
            public DateTime? DateEnd { set; get; }
            public string Note { set; get; }
            public decimal? Amount { set; get; }
            public decimal? ChargesAmount { set; get; }
            public int? Status { set; get; }
            public string Account { set; get; }
            public string Images { set; get; }
            public DateTime? DayUnLimit { set; get; }

        }
    }
    
}
