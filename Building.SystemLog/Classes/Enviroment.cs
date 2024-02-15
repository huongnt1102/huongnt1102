using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.CompilerServices;
using System.Net;
using System.IO;
using System.Management;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace DIPBMS.SystemLog.Classes
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
    public class NetworkInfo
    {
        // Fields
        private NetConnectionStatus _mStatus;

        private string _AdapterType;

        private string _ConnectionID;

        private string _DefaultGateway;

        private string _DeviceName;

        private string _Ip;

        private string _IpWan;

        private string _MacAddress;

        private string _Mask;

        // Methods
        public string GetHelp()
        {
            if (this._mStatus == NetConnectionStatus.Connected)
            {
                return "Connect succeed.";
            }
            if (this._mStatus == NetConnectionStatus.Disconnected)
            {
                return "Your connection was disable, please check Network Setting in Console.";
            }
            if (this._mStatus == NetConnectionStatus.MediaDisconnected)
            {
                return "Cable had bad contact with Network Card! Please check it.";
            }
            if (this._mStatus == NetConnectionStatus.InvalidAddress)
            {
                return "IP address is Invalid, please check DHCP/Router or IP setting.";
            }
            return string.Format("NetConnectionStatus is {0}", this._mStatus);
        }

        // Properties
        public string AdapterType
        {

            get
            {
                return this._AdapterType;
            }

            set
            {
                this._AdapterType = value;
            }
        }

        public string ConnectionID
        {

            get
            {
                return this._ConnectionID;
            }

            set
            {
                this._ConnectionID = value;
            }
        }

        public string DefaultGateway
        {

            get
            {
                return this._DefaultGateway;
            }

            set
            {
                this._DefaultGateway = value;
            }
        }

        public string DeviceName
        {

            get
            {
                return this._DeviceName;
            }

            set
            {
                this._DeviceName = value;
            }
        }

        public string Ip
        {

            get
            {
                return this._Ip;
            }

            set
            {
                this._Ip = value;
            }
        }

        public string IpWan
        {

            get
            {
                return this._IpWan;
            }

            set
            {
                this._IpWan = value;
            }
        }

        public string MacAddress
        {

            get
            {
                return this._MacAddress;
            }

            set
            {
                this._MacAddress = value;
            }
        }

        public string Mask
        {

            get
            {
                return this._Mask;
            }

            set
            {
                this._Mask = value;
            }
        }

        public NetConnectionStatus Status
        {
            get
            {
                return this._mStatus;
            }
            set
            {
                this._mStatus = value;
            }
        }
    }
    public static class NetworkManager
    {
        // Fields
        private static Dictionary<string, NetworkInfo> _mInformations;
        private static NetworkInfo _networkInfo;

        // Methods
        public static void Extract()
        {
            NetworkInfo info = null;
            if (_mInformations.Count > 0)
            {
                foreach (NetworkInfo info2 in _mInformations.Values)
                {
                    if (info2.Status == NetConnectionStatus.Connected)
                    {
                        info = info2;
                        break;
                    }
                    info = info2;
                }
            }
            if (info == null)
            {
                NetworkInfo info3 = new NetworkInfo();
                info3.DeviceName = "";
                info3.MacAddress = "";
                info3.AdapterType = "";
                info3.Ip = "0.0.0.0";
                info3.IpWan = "0.0.0.0";
                info3.Mask = "0.0.0.0";
                info3.DefaultGateway = "0.0.0.0";
                info3.ConnectionID = "";
                info = info3;
            }
            info.IpWan = GetIpWan();
            _networkInfo = info;
        }

        private static string GetIpWan()
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
            _mInformations = new Dictionary<string, NetworkInfo>();
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_NetworkAdapter WHERE NetConnectionID IS NOT NULL");
            foreach (ManagementObject obj2 in searcher.Get())
            {
                NetworkInfo info2 = new NetworkInfo();
                info2.DeviceName = ParseProperty(obj2["Description"]);
                info2.AdapterType = ParseProperty(obj2["AdapterType"]);
                info2.MacAddress = ParseProperty(obj2["MACAddress"]);
                info2.ConnectionID = ParseProperty(obj2["NetConnectionID"]);
                info2.Status = (NetConnectionStatus)Convert.ToInt32(obj2["NetConnectionStatus"]);
                NetworkInfo info = info2;
                SetIp(info);
                _mInformations.Add(info.ConnectionID, info);
            }
        }

        private static void SetIp(NetworkInfo info)
        {
            ManagementClass class2 = new ManagementClass("Win32_NetworkAdapterConfiguration");
            foreach (ManagementObject obj2 in class2.GetInstances())
            {
                try
                {
                    if (info.Status != NetConnectionStatus.Connected)
                    {
                        info.Ip = "0.0.0.0";
                        info.Mask = "0.0.0.0";
                        info.DefaultGateway = "0.0.0.0";
                    }
                    if (!((bool)obj2["ipEnabled"]))
                    {
                        continue;
                    }
                    if (obj2["MACAddress"].ToString().Equals(info.MacAddress))
                    {
                        string[] strArray = (string[])obj2["IPAddress"];
                        info.Ip = strArray[0];
                        string[] strArray2 = (string[])obj2["IPSubnet"];
                        info.Mask = strArray2[0];
                        string[] strArray3 = (string[])obj2["DefaultIPGateway"];
                        info.DefaultGateway = strArray3[0];
                        break;
                    }
                }
                catch (Exception exception)
                {
                    Debug.WriteLine("[SetIP]:" + exception.Message);
                }
            }
        }

        // Properties
        public static NetworkInfo NetworkInfo
        {
            get
            {
                return _networkInfo;
            }
        }
    }
    public class OsEnviroment
    {
        // Methods
        private static int GetOsArchitecture()
        {
            string environmentVariable = Environment.GetEnvironmentVariable("PROCESSOR_ARCHITECTURE");
            return ((string.IsNullOrEmpty(environmentVariable) || (string.Compare(environmentVariable, 0, "x86", 0, 3, true) == 0)) ? 0x20 : 0x40);
        }

        public static string GetOsInfo()
        {
            OperatingSystem oSVersion = Environment.OSVersion;
            Version version = oSVersion.Version;
            string str = "";
            if (oSVersion.Platform == PlatformID.Win32Windows)
            {
                switch (version.Minor)
                {
                    case 0:
                        return "95";

                    case 10:
                        if (version.Revision.ToString() == "2222A")
                        {
                            return "98SE";
                        }
                        return "98";

                    case 90:
                        return "Me";
                }
                return str;
            }
            if (oSVersion.Platform == PlatformID.Win32NT)
            {
                switch (version.Major)
                {
                    case 3:
                        return "NT 3.51";

                    case 4:
                        return "NT 4.0";

                    case 5:
                        if (version.Minor != 0)
                        {
                            return "XP";
                        }
                        return "2000";

                    case 6:
                        if (version.Minor != 0)
                        {
                            return "7";
                        }
                        return "Vista";
                }
            }
            return str;
        }
    }
    public class WinInet
    {
        // Methods
        [DllImport("wininet.dll", CharSet = CharSet.Auto)]
        private static extern bool InternetGetConnectedState(ref ConnectionState lpdwFlags, int dwReserved);
        public static bool IsConnectedToInternet()
        {
            bool flag;
            ConnectionState lpdwFlags = 0;
            try
            {
                flag = InternetGetConnectedState(ref lpdwFlags, 0);
            }
            catch (Exception)
            {
                return false;
            }
            return flag;
        }

        // Nested Types
        [Flags]
        private enum ConnectionState
        {
            InternetConnectionConfigured = 0x40,
            InternetConnectionLan = 2,
            InternetConnectionModem = 1,
            InternetConnectionOffline = 0x20,
            InternetConnectionProxy = 4,
            InternetRasInstalled = 0x10
        }
    }
}
