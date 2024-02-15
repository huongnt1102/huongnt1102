using System;
using System.Linq;
using System.Net;
using System.IO;

namespace FTP
{
    public class FtpClient
    {
        public FtpClient()
        {
            using (var db = new Library.MasterDataContext())
            {
                var objConfig = db.tblConfigs.First();
                ftpAddress = objConfig.FtpUrl;
                ftpUser = objConfig.FtpUser;
                ftpPass = it.EncDec.Decrypt(objConfig.FtpPass);
                webUrl = objConfig.WebUrl;
            }
        }

        public string Url { get; set; }
        public string WebUrl {
            get { return webUrl; }
            set { this.Url = value.Replace(webUrl, "").Trim('/'); }
        }

        private string ftpAddress, ftpUser, ftpPass, webUrl;

        private FtpWebRequest getRequest(string method)
        {
            var request = FtpWebRequest.Create(ftpAddress.TrimEnd('/') + "/" + this.Url.Trim('/')) as FtpWebRequest;
            request.Credentials = new NetworkCredential(ftpUser, ftpPass);
            request.UsePassive = true;
            request.UseBinary = true;
            request.KeepAlive = false;
            request.Method = method;
            return request;
        }
        
        private FtpWebResponse getResponse(string method)
        {
            var request = getRequest(method);
            return request.GetResponse() as FtpWebResponse;
        }

        public long GetFileSize()
        {
            var response = getResponse(WebRequestMethods.Ftp.GetFileSize);
            return response.ContentLength;
        }

        public Stream DownloadFile()
        {
            var response = getResponse(WebRequestMethods.Ftp.DownloadFile);
            return response.GetResponseStream();
        }

        public Stream UploadFile(long length)
        {
            var request = getRequest(WebRequestMethods.Ftp.UploadFile);
            request.ContentLength = length;
            return request.GetRequestStream();
        }

        public void DeleteFile()
        {
            var response = getResponse(WebRequestMethods.Ftp.DeleteFile);
            response.Close();
        }

        public void MakeDirectory()
        {
            var folders = this.Url.Split('/');
            var temp = this.Url;
            this.Url = "";
            foreach (var name in folders)
            {
                try
                {
                    this.Url += "/" + name;
                    var response = getResponse(WebRequestMethods.Ftp.MakeDirectory);
                    response.Close();
                }
                catch {  }
            }
            this.Url = temp;
        }

        public void RemoveDirectory()
        {
            var response = getResponse(WebRequestMethods.Ftp.RemoveDirectory);
            response.Close();
        }
    }
}
