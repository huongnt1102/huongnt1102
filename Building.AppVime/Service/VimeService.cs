using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace Building.AppVime
{
    public static class VimeService
    {
        public static string LinkAPI = Convert.ToString(Library.Class.Connect.QueryConnect.QueryData<string>("configGetLinkAPIApp").FirstOrDefault());

        public static bool isPersonal = true;


        public static string Post(object data, string api)
        {
            try
            {
                InitiateSSLTrust();

                var baseAddress = LinkAPI + api;

                var http = (HttpWebRequest)WebRequest.Create(new Uri(baseAddress));
                http.Accept = "application/json";
                http.ContentType = "application/json; charset=utf-8";
                http.Method = "POST";

                string serialisedData = JsonConvert.SerializeObject(data);
                string parsedContent = serialisedData;
                ASCIIEncoding encoding = new ASCIIEncoding();
                Byte[] bytes = encoding.GetBytes(parsedContent);

                Stream newStream = http.GetRequestStream();
                newStream.Write(bytes, 0, bytes.Length);
                newStream.Close();

                var response = http.GetResponse();

                var stream = response.GetResponseStream();
                var sr = new StreamReader(stream);
                string content = sr.ReadToEnd();

                return content;
            }
            catch (Exception ex)
            {
                return "0";
            }
        }

        public static string Put(object data, string api)
        {
            try
            {
                InitiateSSLTrust();

                var baseAddress = LinkAPI + api;

                var http = (HttpWebRequest)WebRequest.Create(new Uri(baseAddress));
                http.Accept = "application/json";
                http.ContentType = "application/json; charset=utf-8";
                http.Method = "PUT";

                string serialisedData = JsonConvert.SerializeObject(data);
                string parsedContent = serialisedData;

                using (var streamWriter = new StreamWriter(http.GetRequestStream()))
                {
                    string json = serialisedData;

                    streamWriter.Write(json);
                    streamWriter.Flush();
                }

                var response = http.GetResponse();

                var stream = response.GetResponseStream();
                var sr = new StreamReader(stream);
                string content = sr.ReadToEnd();

                return content;
            }
            catch (Exception ex)
            {
                return "0";
            }
        }

        public static string PostH(object data, string api)
        {
            try
            {
                InitiateSSLTrust();

                var baseAddress = LinkAPI + api;

                var http = (HttpWebRequest)WebRequest.Create(new Uri(baseAddress));
                http.Accept = "application/json";
                http.ContentType = "application/json; charset=utf-8";
                http.Method = "POST";

                string serialisedData = JsonConvert.SerializeObject(data);
                string parsedContent = serialisedData;

                using (var streamWriter = new StreamWriter(http.GetRequestStream()))
                {
                    string json = serialisedData;

                    streamWriter.Write(json);
                    streamWriter.Flush();
                }

                var response = http.GetResponse();

                var stream = response.GetResponseStream();
                var sr = new StreamReader(stream);
                string content = sr.ReadToEnd();

                return content;
            }
            catch (Exception ex)
            {
                return "0";
            }
        }

        public static string PostImage(object data, string api)
        {
            try
            {
                InitiateSSLTrust();

                var baseAddress = LinkAPI + api;

                var http = (HttpWebRequest)WebRequest.Create(new Uri(baseAddress));
                http.Accept = "application/json";
                http.ContentType = "application/json; charset=utf-8";
                http.Method = "POST";
                http.KeepAlive = false;

                string serialisedData = JsonConvert.SerializeObject(data);
                string parsedContent = serialisedData;
                ASCIIEncoding encoding = new ASCIIEncoding();
                Byte[] bytes = encoding.GetBytes(parsedContent);

                Stream newStream = http.GetRequestStream();
                newStream.Write(bytes, 0, bytes.Length);
                newStream.Close();

                var response = http.GetResponse();

                var stream = response.GetResponseStream();
                var sr = new StreamReader(stream);
                string content = sr.ReadToEnd();

                return content;
            }
            catch (Exception ex)
            {
                return "";
            }
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
    }
}
