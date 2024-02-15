using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace DIPBMS.SystemLog.Classes
{
    public class OSInfo
    {
        // Fields
        private const int VER_NT_DOMAIN_CONTROLLER = 2;
        private const int VER_NT_SERVER = 3;
        private const int VER_NT_WORKSTATION = 1;
        private const int VER_SUITE_BLADE = 0x400;
        private const int VER_SUITE_DATACENTER = 0x80;
        private const int VER_SUITE_ENTERPRISE = 2;
        private const int VER_SUITE_PERSONAL = 0x200;
        private const int VER_SUITE_SINGLEUSERTS = 0x100;
        private const int VER_SUITE_SMALLBUSINESS = 1;
        private const int VER_SUITE_TERMINAL = 0x10;

        // Methods
        public OSInfo()
        {
        }
        public static EnumOS GetOS()
        {
            OperatingSystem oSVersion = Environment.OSVersion;
            EnumOS unknown = EnumOS.Unknown;
            switch (oSVersion.Platform)
            {
                case PlatformID.Win32Windows:
                    switch (oSVersion.Version.Minor)
                    {
                        case 0:
                            return EnumOS.Windows_95;

                        case 10:
                            if (oSVersion.Version.Revision.ToString() == "2222A")
                            {
                                return EnumOS.Windows_98_Second_Edition;
                            }
                            return EnumOS.Windows_98;

                        case 90:
                            return EnumOS.Windows_Me;
                    }
                    return unknown;

                case PlatformID.Win32NT:
                    switch (oSVersion.Version.Major)
                    {
                        case 3:
                            return EnumOS.Windows_NT_3_51;

                        case 4:
                            return EnumOS.Windows_NT_40;

                        case 5:
                            if (oSVersion.Version.Minor == 0)
                            {
                                return EnumOS.Windows_2000;
                            }
                            if (oSVersion.Version.Minor == 1)
                            {
                                return EnumOS.Windows_XP;
                            }
                            if (oSVersion.Version.Minor != 2)
                            {
                                return unknown;
                            }
                            return EnumOS.Windows_Server_2003;

                        case 6:
                            if (oSVersion.Version.Minor == 0)
                            {
                                return EnumOS.Windows_Vista;
                            }
                            if (oSVersion.Version.Minor == 2)
                            {
                                return  EnumOS.Windows_8_1_Pro;
                            }
                            if (oSVersion.Version.Minor != 1)
                            {
                                return unknown;
                            }
                            return EnumOS.Windows_7;
                    }
                    return unknown;
            }
            return unknown;
        }
        public static string GetOSName()
        {
            OperatingSystem oSVersion = Environment.OSVersion;
            string str = "UNKNOWN";
            switch (oSVersion.Platform)
            {
                case PlatformID.Win32Windows:
                    switch (oSVersion.Version.Minor)
                    {
                        case 0:
                            return "Windows 95";

                        case 10:
                            if (oSVersion.Version.Revision.ToString() == "2222A")
                            {
                                return "Windows 98 Second Edition";
                            }
                            return "Windows 98";

                        case 90:
                            return "Windows Me";
                    }
                    return str;

                case PlatformID.Win32NT:
                    switch (oSVersion.Version.Major)
                    {
                        case 3:
                            return "Windows NT 3.51";

                        case 4:
                            return "Windows NT 4.0";

                        case 5:
                            if (oSVersion.Version.Minor == 0)
                            {
                                return "Windows 2000";
                            }
                            if (oSVersion.Version.Minor == 1)
                            {
                                return "Windows XP";
                            }
                            if (oSVersion.Version.Minor != 2)
                            {
                                return str;
                            }
                            return "Windows Server 2003";

                        case 6:
                            if (oSVersion.Version.Major == 0)
                            {
                                return "Windows Vista";
                            }
                            if (oSVersion.Version.Major == 6)
                            {
                                return "Windows 8.1 Pro";
                            }
                            if (oSVersion.Version.Major != 1)
                            {
                                return str;
                            }
                            return "Windows 7";
                    }
                    return str;
            }
            return str;
        }

        public static string GetOSProductType()
        {
            OSVERSIONINFOEX osVersionInfo = new OSVERSIONINFOEX();
            OperatingSystem oSVersion = Environment.OSVersion;
            osVersionInfo.dwOSVersionInfoSize = Marshal.SizeOf(typeof(OSVERSIONINFOEX));
            if (GetVersionEx(ref osVersionInfo))
            {
                if (oSVersion.Version.Major == 4)
                {
                    if (osVersionInfo.wProductType == 1)
                    {
                        return " Workstation";
                    }
                    if (osVersionInfo.wProductType == 3)
                    {
                        return " Server";
                    }
                    return "";
                }
                if (oSVersion.Version.Major == 5)
                {
                    if (osVersionInfo.wProductType == 1)
                    {
                        if ((osVersionInfo.wSuiteMask & 0x200) == 0x200)
                        {
                            return " Home Edition";
                        }
                        return " Professional";
                    }
                    if (osVersionInfo.wProductType == 3)
                    {
                        if (oSVersion.Version.Minor == 0)
                        {
                            if ((osVersionInfo.wSuiteMask & 0x80) == 0x80)
                            {
                                return " Datacenter Server";
                            }
                            if ((osVersionInfo.wSuiteMask & 2) == 2)
                            {
                                return " Advanced Server";
                            }
                            return " Server";
                        }
                        if ((osVersionInfo.wSuiteMask & 0x80) == 0x80)
                        {
                            return " Datacenter Edition";
                        }
                        if ((osVersionInfo.wSuiteMask & 2) == 2)
                        {
                            return " Enterprise Edition";
                        }
                        if ((osVersionInfo.wSuiteMask & 0x400) == 0x400)
                        {
                            return " Web Edition";
                        }
                        return " Standard Edition";
                    }
                }
            }
            return "";
        }
        public static string GetOSServicePack()
        {
            OSVERSIONINFOEX osVersionInfo = new OSVERSIONINFOEX();
            osVersionInfo.dwOSVersionInfoSize = Marshal.SizeOf(typeof(OSVERSIONINFOEX));
            if (!GetVersionEx(ref osVersionInfo))
            {
                return "";
            }
            return (" " + osVersionInfo.szCSDVersion);
        }
        [DllImport("kernel32.dll")]
        private static extern bool GetVersionEx(ref OSVERSIONINFOEX osVersionInfo);
        // Properties
        public static int OSBuildVersion
        {
            get
            {
                return System.Environment.OSVersion.Version.Build;
            }
        }
        public static int OSMajorVersion
        {
            get
            {
                return System.Environment.OSVersion.Version.Major;
            }
        }
        public static int OSMinorVersion
        {
            get
            {
                return System.Environment.OSVersion.Version.Minor;
            }
        }
        public static int OSRevisionVersion
        {
            get
            {
                return System.Environment.OSVersion.Version.Revision;
            }
        }
        public static string OSVersion
        {
            get
            {
                return System.Environment.OSVersion.Version.ToString();
            }
        }
        // Nested Types
        public enum EnumOS
        {
            Unknown,
            Windows_31,
            Windows_95,
            Windows_98_Second_Edition,
            Windows_98,
            Windows_Me,
            Windows_NT_3_51,
            Windows_NT_40,
            Windows_2000,
            Windows_XP,
            Windows_Media_Center,
            Windows_Server_2003,
            Windows_Server_2008,
            Windows_Longhorn,
            Windows_Vista,
            Windows_7,
            Windows_8,
            Windows_8_1_Pro,
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct OSVERSIONINFOEX
        {
            public int dwOSVersionInfoSize;
            public int dwMajorVersion;
            public int dwMinorVersion;
            public int dwBuildNumber;
            public int dwPlatformId;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x80)]
            public string szCSDVersion;
            public short wServicePackMajor;
            public short wServicePackMinor;
            public short wSuiteMask;
            public byte wProductType;
            public byte wReserved;
        }
    }

}
