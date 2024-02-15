using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.Other
{
    public static class API
    {
        private static string Url = "https://thongtindoanhnghiep.co/api/";

        public static List<object> Get_DSNghanhNghe()
        {
            var api = "industry";

            var client = new RestClient(Url + api);

            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            IRestResponse response = client.Execute(request);
            JObject Jobj = JObject.Parse(response.Content);

            var result = Jobj["LtsItem"].ToObject<List<object>>();
            return result;
        }

        public static object GetThongTinTheoMST(string _mst)
        {
            string api = "company/";

            var client = new RestClient(Url + api + _mst);

            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            IRestResponse response = client.Execute(request);
            JObject Jobj = JObject.Parse(response.Content);

            var result = Jobj["LtsItem"].ToObject<object>();
            return result;
        }

        public static List<object> GetThongTinDoanhNghiep(string Key )
        {
            RestClient client = new RestClient(Url + "company?k=" + Key);

            List<object> data = new List<object>();

            var request = new RestRequest(Method.GET);

            request.AddHeader("cache-control", "no-cache");

            IRestResponse response = client.Execute(request);
            JObject Jobj = JObject.Parse(response.Content);
            try
            {
                data = Jobj["LtsItems"].ToObject<List<object>>();
            }
            catch
            {
            }

            return data;
        }

        public static tnKhachHang GetChiTietDoanhNghiep(string mst)
        {
            var kh = new tnKhachHang();

            RestClient client = new RestClient(Url + "company/" + mst);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");

            IRestResponse response = client.Execute(request);

            JObject Jobj = JObject.Parse(response.Content);
            using (var db = new MasterDataContext())
            {
                if (db.tnKhachHangs.Any(o => o.MaSoThue == getValue(Jobj, "MaSoThue")))
                {
                    DialogBox.Error("Khách hàng này đã tồn tại trong hệ thống");
                    return kh;
                }

               
                kh.MaTN = Common.User.MaTN;
                kh.IsCaNhan = false;
                kh.IsKhachHang = true;
                kh.MaLoaiKH = 17;
                kh.MaSoThue = getValue(Jobj, "MaSoThue");
                kh.KyHieu = kh.MaSoThue;
                kh.CtyTen = getValue(Jobj, "Title");
                kh.DiaChi = getValue(Jobj, "DiaChiCongTy");
                kh.CtySoDKKD = getValue(Jobj, "GiayPhepKinhDoanh");
                kh.CtyNgayDKKD = getValue(Jobj, "GiayPhepKinhDoanh_NgayCap");
                kh.MaSoThue_NgayCap = DateTime.Parse(getValue(Jobj, "NgayCap"));
                kh.IsCSKH = true;
                kh.NgayNhap = DateTime.Now;
                kh.MaNVN = Common.User.MaNV;
            }

            return kh;
        }

        private static string getValue(JObject obj, string field)
        {
            try
            {
                return obj[field].ToString();
            }
            catch
            {
                return "";
            }
        }

        public static List<Locationcls> GetTinh()
        {

            var api = "city";
            var client = new RestClient(Url + api);
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            JObject Jobj = JObject.Parse(response.Content);

            var result = Jobj["LtsItem"].ToObject<List<Locationcls>>();
            return result;
        }

        public static List<Locationcls> GetHuyen(int id)
        {
            string api = string.Format("https://thongtindoanhnghiep.co/api/city/{0}/district", id);

            var client = new RestClient(api);
            var request = new RestRequest(Method.GET);
            IRestResponse res = client.Execute(request);

            return JsonConvert.DeserializeObject<List<Locationcls>>(res.Content);
        }

        public static List<Locationcls> GetXa(int id)
        {
            string api = string.Format("https://thongtindoanhnghiep.co/api/district/{0}/ward", id);

            var client = new RestClient(api);
            var request = new RestRequest(Method.GET);
            IRestResponse res = client.Execute(request);

            return JsonConvert.DeserializeObject<List<Locationcls>>(res.Content);
        }

        public class Locationcls
        {
            public int ID { get; set; }
            public string Title { get; set; }
        }
    }
}
