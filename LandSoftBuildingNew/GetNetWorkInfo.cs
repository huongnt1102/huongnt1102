using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Management;
using System.Diagnostics;

namespace LandSoftBuildingMain
{
    public enum NetConnectionStatus
    {
        Disconnected,
        Connecting,
        Connected,
        Disconnecting,
        HardwareNotPresent,
        HardwareDisabled,
        HardwareMalfunction,
        MediaDisconnected,
        Authenticating,
        AuthenticationSucceeded,
        AuthenticationFailed,
        InvalidAddress,
        CredentialsRequired
    }
    public static class GetNetWorkInfo
    {
        public static string DeviceName { get; set; }
        public static string AdapterType { get; set; }
        public static string MacAddress { get; set; }
        public static string ConnectionID { get; set; }
        public static int Status { get; set; }
        public static string IPAddress { get; set; }
        public static string IPSubnet { get; set; }
        public static string DefaultIPGateway { get; set; }
        public static string GetIpWan()
        {
            try
            {
                WebRequest request = WebRequest.Create("http://checkip.dyndns.org/"); 
                request.Credentials = CredentialCache.DefaultCredentials;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Console.WriteLine(response.StatusDescription);
                Stream responseStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(responseStream);
                string str = reader.ReadToEnd();
                int index = str.IndexOf(":"); 
                str = str.Remove(0, index + 1);
                int startIndex = str.LastIndexOf("</body>");  
                str = str.Remove(startIndex, 14);
                reader.Close();
                responseStream.Close();
                response.Close();
                return str.Trim();
            }
            catch (Exception)
            {
                return "0.0.0.0";
            }
        }
        private static string ParseProperty(object data)
        {
            if (data != null)
            {
                return data.ToString();
            }
            return "";
        }
        public static void ReadSysInfo()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_NetworkAdapter WHERE NetConnectionID IS NOT NULL");  
            foreach (ManagementObject obj2 in searcher.Get())
            {
               DeviceName = ParseProperty(obj2["Description"]);
               AdapterType = ParseProperty(obj2["AdapterType"]);
               MacAddress = ParseProperty(obj2["MACAddress"]);
               ConnectionID = ParseProperty(obj2["NetConnectionID"]);
               //Status = (NetConnectionStatus)Convert.ToInt32(obj2["NetConnectionStatus"]);
            }
        }
        public static void GetIp()
        {
            ManagementClass class2 = new ManagementClass("Win32_NetworkAdapterConfiguration");  
            foreach (ManagementObject obj2 in class2.GetInstances())
            {
                try
                {
                    if (!((bool)obj2["ipEnabled"]))
                    {
                        continue;
                    }
                    if (obj2["MACAddress"].ToString()!="")
                    {
                        string[] strArray = (string[])obj2["IPAddress"];
                        IPAddress = strArray[0];
                        string[] strArray2 = (string[])obj2["IPSubnet"];
                        IPSubnet = strArray2[0];
                        string[] strArray3 = (string[])obj2["DefaultIPGateway"];
                        DefaultIPGateway = strArray3[0];
                        break;
                    }
                }
                catch (Exception exception)
                {
                    IPAddress = "0.0.0.0";
                    IPSubnet = "0.0.0.0";
                    DefaultIPGateway = "0.0.0.0";
                }
            }
        }

    }
}
