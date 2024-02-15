using Library;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SAP.Funct
{
    public class RequestApi
    {
        public static Class.LinkAPIAddress GetLinkAPI()
        {
            var objLinkApi = Library.Class.Connect.QueryConnect.QueryData<Class.LinkAPIAddress>("sapin_GetLinkAPI");
            if (objLinkApi.Count() > 0) return objLinkApi.First();
            else return new Class.LinkAPIAddress() ;
        }

        public static IRestResponse post_json(string link_api, string dieu_huong, string json_data, string token, RestSharp.Method method)
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            InitiateSSLTrust();

            string api = link_api + dieu_huong;
            var client = new RestClient(api);
            //DialogBox.Alert(link_api + dieu_huong);
            //DialogBox.Alert(json_data);
            client.Timeout = -1;
            var request = new RestRequest(method);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Basic " + token);
            request.AddParameter("application/json", json_data, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            return response;
        }

        public static void InitiateSSLTrust()
        {
            try
            {
                ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(delegate { return true; });
            }
            catch (System.Exception ex)
            { }
        }

        public static void save_log_request_api(int loai, string id, string link_api, string dieu_huong, string param, string token, string result)
        {
            //DialogBox.Alert("1");
            var model_log = new { loai = loai, id = id, linkapi = link_api, dieuhuong = dieu_huong, param = param, token = token, result = result };
            var param_log = new Dapper.DynamicParameters();
            param_log.AddDynamicParams(model_log);
            Library.Class.Connect.QueryConnect.Query<bool>("sapin_save_log_request_api", param_log);
            //DialogBox.Alert("2");
        }

        public static string post_item<T>(T item, int loai, string Id)
        {
            // Get linkk api
            Class.LinkAPIAddress link = Funct.RequestApi.GetLinkAPI();
            string value = string.Format("{0}:{1}",link.UserName, link.Password);
            //var byteArray = Encoding.ASCII.GetBytes($"{link.UserName}:{link.Password}"); //$
            var byteArray = Encoding.ASCII.GetBytes(value);
            string encodeString = Convert.ToBase64String(byteArray);

            IRestResponse objResponse = Funct.RequestApi.post_json
                                            (
                                                link.LinkAPI,
                                                link.Directional,
                                                JsonConvert.SerializeObject
                                                (
                                                     item
                                                ),
                                                encodeString,
                                                Method.POST
                                            );


            // Xử lý dữ liệu

            //if (objResponse.StatusCode != HttpStatusCode.OK)
            //{
            //    //DialogBox.Error(Convert.ToString(objResponse.ErrorMessage));
            //    return "";
            //}
            try
            {
                //DialogBox.Alert(objResponse.Content);
                Funct.RequestApi.save_log_request_api
                        (
                            loai,
                            Id,
                            link.LinkAPI,
                            link.Directional,
                            JsonConvert.SerializeObject
                            (
                                 item
                            ),
                            encodeString,
                            objResponse.Content
                         );
                return objResponse.Content;
            }
            catch(System.Exception ex) { return ex.Message; }
            // Lưu kịch sử dữ liệu
            
        }
    }
}
