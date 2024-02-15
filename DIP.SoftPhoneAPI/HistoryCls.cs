using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;
using System.Diagnostics;

namespace DIP.SoftPhoneAPI
{
    public class HistoryCls
    {
        public static List<CallLine> Lines;
        public static string LinkReport;
        public static string LinkAudio;
        public static string UserName;
        public static string Password;
        public static List<string> Trunks;

        public static string GetType(string did)
        {
            return did == "" ? "Cuộc gọi đi" : "Cuộc gọi đến";
        }

        public static string GetTrunk(string src, string dst)
        {
            return src != "" ? src : dst;
        }

        public static string GetStatus(string type, string disposition)
        {
            if (type == "Cuộc gọi đến")
            {
                if (disposition == "ANSWERED")
                    return "Thành công";
                else
                    return "Cuộc gọi nhỡ";
            }
            else
            {
                switch (disposition)
                {
                    case "ANSWERED":
                        return "Thành công";
                    case "BUSY":
                        return "Máy bận";
                    case "FAILED":
                        return "Không liên lạc được";
                    case "NO ANSWER":
                        return "Không trả lời";
                    default:
                        return "Thất bại";
                }
            }
        }

        public static string GetLine(string src, string dst)
        {
            try
            {
                return Lines.First(p => p.ID == src || p.ID == dst).ID;
            }
            catch
            {
                return null;
            }
        }

        public static string GetStaff(string src, string dst)
        {
            try
            {
                return Lines.First(p => p.ID == src || p.ID == dst).Name;
            }
            catch
            {
                return "";
            }
        }

        public static List<CallRecord> GetHistory(DateTime fromdate, DateTime todate)
        {
            try
            {
                if (Trunks == null || Trunks.Count == 0) return null;

                string _Trunks = "";
                foreach (var t in Trunks)
                    _Trunks += "<" + t + ">";
                
                if (_Trunks == "") return null;

                System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                System.Net.WebClient client = new System.Net.WebClient();

                var url = string.Format("{0}?user={1}&pass={2}&fromdate={3:yyyy-MM-dd}&todate={4:yyyy-MM-dd}&trunks={5}", LinkReport, UserName, Password, fromdate, todate, _Trunks);
                var strXML = client.DownloadString(url);

                var doc = XDocument.Parse(strXML);

                return (from c in doc.Descendants("item")
                        select new CallRecord()
                        {
                            uniqueid = c.Attribute("uniqueid").Value,
                            calldate = c.Attribute("calldate").Value,
                            src = c.Attribute("src").Value,
                            dst = c.Attribute("dst").Value,
                            filename = GetAudioUrl(c.Attribute("filename").Value, c.Attribute("calldate").Value),
                            duration = int.Parse(c.Attribute("duration").Value),
                            billsec = int.Parse(c.Attribute("billsec").Value),
                            type = GetType(c.Attribute("did").Value),
                            status = GetStatus(GetType(c.Attribute("did").Value), c.Attribute("disposition").Value),
                            line = GetLine(c.Attribute("src").Value, c.Attribute("dst").Value),
                            staff = GetStaff(c.Attribute("src").Value, c.Attribute("dst").Value),
                            trunk = GetTrunk(c.Attribute("outbound_cnum").Value, c.Attribute("did").Value)
                        }).ToList();
            }
            catch
            {
                return null;
            }
        }

        public static List<CallRecord> GetHistory(DateTime fromDate, DateTime toDate, string type, string status, string staff, string trunks)
        {
            try
            {
                var ltResult = HistoryCls.GetHistory(fromDate, toDate);
                return (from h in ltResult
                        where (type.Contains(h.type) == true | type == "") & (status.Contains(h.status) == true | status == "")
                         & ((h.staff != "" & staff.Contains(h.staff) == true) | staff == "") & (trunks.Contains(h.trunk) == true | trunks == "")
                        select h).ToList();
            }
            catch
            {
                return null;
            }
        }

        public static string GetAudioUrl(string fileName, string date)
        {
            if (fileName != "")
            {
                var audio = string.Format("/{0}/{1}/{2}/{3}", date.Substring(0, 4), date.Substring(5, 2), date.Substring(8, 2), fileName);
                var url = "";
                if (LinkAudio.IndexOf('?') > 0)
                {
                    url = string.Format("{0}&user={1}&pass={2}&audio={3}", LinkAudio, UserName, Password, audio);
                }
                else
                {
                    url = string.Format("{0}?user={1}&pass={2}&audio={3}", LinkAudio, UserName, Password, audio);
                }

                return url;
            }

            return "";
        }

        public static DateTime GetDate(string date)
        {
            var year = int.Parse(date.Substring(0, 4));
            var month = int.Parse(date.Substring(5, 2));
            var day = int.Parse(date.Substring(8, 2));
            var hour = int.Parse(date.Substring(11, 2));
            var minute = int.Parse(date.Substring(14, 2));
            var second = int.Parse(date.Substring(17, 2));
            return new DateTime(year, month, day, hour, minute, second);
        }

        public static void ListenAgain(string phoneNumber, DateTime date)
        {
            try
            {
                var ltHistory = GetHistory(date, date);
                var objRecord = (from h in ltHistory
                                 where (h.src == phoneNumber | h.dst == phoneNumber) & (date - GetDate(h.calldate)).TotalSeconds >= 0
                                 orderby GetDate(h.calldate) descending
                                 select h).First();
                if (objRecord.filename != null && objRecord.filename != "")
                {
                    var url = GetAudioUrl(objRecord.filename, objRecord.calldate);
                    ListenAgain(url, false);
                }
            }
            catch { }
        }

        public static void ListenAgain(string url, bool isDownload)
        {
            try
            {
                if (isDownload)
                {
                    SaveFileDialog frm = new SaveFileDialog();
                    frm.FileName = url.Substring(url.LastIndexOf('/') + 1);
                    frm.Filter = "Audio|*" + url.Substring(url.LastIndexOf('.'));
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        WebClient webClient = new WebClient();
                        webClient.DownloadFile(url, frm.FileName);

                        MessageBox.Show("File đã được lưu");
                    }
                }
                else
                {
                    using (var frm = new MediaEditor())
                    {
                        frm.Url = url;
                        frm.ShowDialog();
                    }
                }
            }
            catch { }
        }
    }

    public class CallRecord
    {
        public string uniqueid { get; set; }
        public string calldate { get; set; }
        public string src { get; set; }
        public string dst { get; set; }
        public string filename { get; set; }
        public int duration { get; set; }
        public int billsec { get; set; }
        public string type { get; set; }
        public string status { get; set; }
        public string line { get; set; }
        public string staff { get; set; }
        public string trunk { get; set; }
    }

    public class CallLine
    {
        public string ID { get; set; }
        public string Name { get; set; }
    }
}
