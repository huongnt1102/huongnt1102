using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel;
using Microsoft.VisualBasic;
using System.Security.Cryptography;
using System.Security.Principal;
using Building.SystemLog.Classes;

namespace DIPBMS.SystemLog.Classes
{
    public class Params
    {
        public const string XEM = "Xem";
    }
    public class DATA
    {
        // Fields
        private DateTime m_Created;
        private string m_Database;
        private string m_Path;
        private string m_Version;

        // Methods
        public DATA()
        {
            this.m_Database = "";
            this.m_Version = "";
            this.m_Created = DateTime.Now;
            this.m_Path = "";
        }

        public DATA(string Database, string Version, DateTime Created, string Path)
        {
            this.m_Database = Database;
            this.m_Version = Version;
            this.m_Created = Created;
            this.m_Path = Path;
        }

        public string Delete()
        {
            string[] myParams = new string[] { "@Database" };
            object[] myValues = new object[] { this.m_Database };
            SqlHelper helper = new SqlHelper();
            return helper.ExecuteNonQuery("DATA_Delete", myParams, myValues);
        }

        public string Delete(string Database)
        {
            string[] myParams = new string[] { "@Database" };
            object[] myValues = new object[] { Database };
            SqlHelper helper = new SqlHelper();
            return helper.ExecuteNonQuery("DATA_Delete", myParams, myValues);
        }

        public string Delete(SqlConnection myConnection, string Database)
        {
            string[] myParams = new string[] { "@Database" };
            object[] myValues = new object[] { Database };
            SqlHelper helper = new SqlHelper();
            return helper.ExecuteNonQuery(myConnection, "DATA_Delete", myParams, myValues);
        }

        public string Delete(SqlTransaction myTransaction, string Database)
        {
            string[] myParams = new string[] { "@Database" };
            object[] myValues = new object[] { Database };
            SqlHelper helper = new SqlHelper();
            return helper.ExecuteNonQuery(myTransaction, "DATA_Delete", myParams, myValues);
        }

        public bool Exist(string Database)
        {
            bool hasRows = false;
            string[] myParams = new string[] { "@Database" };
            object[] myValues = new object[] { Database };
            SqlHelper helper = new SqlHelper();
            SqlDataReader reader = helper.ExecuteReader("DATA_Get", myParams, myValues);
            if (reader != null)
            {
                hasRows = reader.HasRows;
                reader.Close();
                helper.Close();
                reader = null;
            }
            return hasRows;
        }

        public string Get(string Database)
        {
            string str = "";
            string[] myParams = new string[] { "@Database" };
            object[] myValues = new object[] { Database };
            SqlHelper helper = new SqlHelper();
            SqlDataReader reader = helper.ExecuteReader("DATA_Get", myParams, myValues);
            if (reader != null)
            {
                while (reader.Read())
                {
                    this.m_Database = Convert.ToString(reader["Database"]);
                    this.m_Version = Convert.ToString(reader["Version"]);
                    this.m_Created = Convert.ToDateTime(reader["Created"]);
                    this.m_Path = Convert.ToString(reader["Path"]);
                    str = "OK";
                }
                reader.Close();
                helper.Close();
                reader = null;
            }
            return str;
        }

        public string Get(SqlConnection myConnection, string Database)
        {
            string str = "";
            string[] myParams = new string[] { "@Database" };
            object[] myValues = new object[] { Database };
            SqlHelper helper = new SqlHelper();
            SqlDataReader reader = helper.ExecuteReader(myConnection, "DATA_Get", myParams, myValues);
            if (reader != null)
            {
                while (reader.Read())
                {
                    this.m_Database = Convert.ToString(reader["Database"]);
                    this.m_Version = Convert.ToString(reader["Version"]);
                    this.m_Created = Convert.ToDateTime(reader["Created"]);
                    this.m_Path = Convert.ToString(reader["Path"]);
                    str = "OK";
                }
                reader.Close();
                helper.Close();
                reader = null;
            }
            return str;
        }

        public string Get(SqlTransaction myTransaction, string Database)
        {
            string str = "";
            string[] myParams = new string[] { "@Database" };
            object[] myValues = new object[] { Database };
            SqlHelper helper = new SqlHelper();
            SqlDataReader reader = helper.ExecuteReader(myTransaction, "DATA_Get", myParams, myValues);
            if (reader != null)
            {
                while (reader.Read())
                {
                    this.m_Database = Convert.ToString(reader["Database"]);
                    this.m_Version = Convert.ToString(reader["Version"]);
                    this.m_Created = Convert.ToDateTime(reader["Created"]);
                    this.m_Path = Convert.ToString(reader["Path"]);
                    str = "OK";
                }
                reader.Close();
                helper.Close();
                reader = null;
            }
            return str;
        }

        public DataTable GetList()
        {
            SqlHelper helper = new SqlHelper();
            return helper.ExecuteDataTable("DATA_GetList");
        }

        public string Insert()
        {
            string[] myParams = new string[] { "@Database", "@Version", "@Created", "@Path" };
            object[] myValues = new object[] { this.m_Database, this.m_Version, this.m_Created, this.m_Path };
            SqlHelper helper = new SqlHelper();
            return helper.ExecuteNonQuery("DATA_Insert", myParams, myValues);
        }

        public string Insert(SqlTransaction myTransaction)
        {
            string[] myParams = new string[] { "@Database", "@Version", "@Created", "@Path" };
            object[] myValues = new object[] { this.m_Database, this.m_Version, this.m_Created, this.m_Path };
            SqlHelper helper = new SqlHelper();
            return helper.ExecuteNonQuery(myTransaction, "DATA_Insert", myParams, myValues);
        }

        public string Insert(string Database, string Version, DateTime Created, string Path)
        {
            string[] myParams = new string[] { "@Database", "@Version", "@Created", "@Path" };
            object[] myValues = new object[] { Database, Version, Created, Path };
            SqlHelper helper = new SqlHelper();
            return helper.ExecuteNonQuery("DATA_Insert", myParams, myValues);
        }

        public string Insert(SqlConnection myConnection, string Database, string Version, DateTime Created, string Path)
        {
            string[] myParams = new string[] { "@Database", "@Version", "@Created", "@Path" };
            object[] myValues = new object[] { Database, Version, Created, Path };
            SqlHelper helper = new SqlHelper();
            return helper.ExecuteNonQuery(myConnection, "DATA_Insert", myParams, myValues);
        }

        public string Insert(SqlTransaction myTransaction, string Database, string Version, DateTime Created, string Path)
        {
            string[] myParams = new string[] { "@Database", "@Version", "@Created", "@Path" };
            object[] myValues = new object[] { Database, Version, Created, Path };
            SqlHelper helper = new SqlHelper();
            return helper.ExecuteNonQuery(myTransaction, "DATA_Insert", myParams, myValues);
        }

        public string NewID()
        {
            return SqlHelper.GenCode("DATA", "DATAID", "");
        }

        public string Update()
        {
            string[] myParams = new string[] { "@Database", "@Version", "@Created", "@Path" };
            object[] myValues = new object[] { this.m_Database, this.m_Version, this.m_Created, this.m_Path };
            SqlHelper helper = new SqlHelper();
            return helper.ExecuteNonQuery("DATA_Update", myParams, myValues);
        }

        public string Update(SqlTransaction myTransaction)
        {
            string[] myParams = new string[] { "@Database", "@Version", "@Created", "@Path" };
            object[] myValues = new object[] { this.m_Database, this.m_Version, this.m_Created, this.m_Path };
            SqlHelper helper = new SqlHelper();
            return helper.ExecuteNonQuery(myTransaction, "DATA_Update", myParams, myValues);
        }

        public string Update(string Database, string Version, DateTime Created, string Path)
        {
            string[] myParams = new string[] { "@Database", "@Version", "@Created", "@Path" };
            object[] myValues = new object[] { Database, Version, Created, Path };
            SqlHelper helper = new SqlHelper();
            return helper.ExecuteNonQuery("DATA_Update", myParams, myValues);
        }

        public string Update(SqlConnection myConnection, string Database, string Version, DateTime Created, string Path)
        {
            string[] myParams = new string[] { "@Database", "@Version", "@Created", "@Path" };
            object[] myValues = new object[] { Database, Version, Created, Path };
            SqlHelper helper = new SqlHelper();
            return helper.ExecuteNonQuery(myConnection, "DATA_Update", myParams, myValues);
        }

        public string Update(SqlTransaction myTransaction, string Database, string Version, DateTime Created, string Path)
        {
            string[] myParams = new string[] { "@Database", "@Version", "@Created", "@Path" };
            object[] myValues = new object[] { Database, Version, Created, Path };
            SqlHelper helper = new SqlHelper();
            return helper.ExecuteNonQuery(myTransaction, "DATA_Update", myParams, myValues);
        }

        // Properties
        public DateTime Created
        {
            get
            {
                return this.m_Created;
            }
            set
            {
                this.m_Created = value;
            }
        }

        public string Database
        {
            get
            {
                return this.m_Database;
            }
            set
            {
                this.m_Database = value;
            }
        }

        public string Path
        {
            get
            {
                return this.m_Path;
            }
            set
            {
                this.m_Path = value;
            }
        }

        public string ProductName
        {
            get
            {
                return "Class DATA";
            }
        }

        public string ProductVersion
        {
            get
            {
                return "1.0.0.0";
            }
        }

        public string Version
        {
            get
            {
                return this.m_Version;
            }
            set
            {
                this.m_Version = value;
            }
        }
    }
    public class MyLogin
    {
        // Fields
        private static string _fullName = "";
        private static string _id = "";
        private static DateTime _loginDate = DateTime.Now;
        private static string _roleId = "";
        private static string _userAccount = "";
        private static string _Code = "";
        private static int _Level = 0;

        // Methods
        private static string byteArrayToString(byte[] inputArray)
        {
            StringBuilder builder = new StringBuilder("");
            for (int i = 0; i < inputArray.Length; i++)
            {
                builder.Append(inputArray[i].ToString("X2"));
            }
            return builder.ToString();
        }

        private static string ByteArrayToString(byte[] arryInput)
        {
            StringBuilder builder = new StringBuilder(arryInput.Length);
            for (int i = 0; i < arryInput.Length; i++)
            {
                builder.Append(arryInput[i].ToString());
            }
            return builder.ToString();
        }

        public static string CreatePassword(string strName, string strPw)
        {
            return MD5Encrypt(StringEncryption(strName + Strings.StrReverse(strPw) + Strings.StrReverse(strName)));
        }

        public static string CheckAccount(string accountId, string strPassword)
        {
            //SYS_USER sys_user = new SYS_USER();
            //if (sys_user.Get_UserName(accountId) == "OK")
            //{
            //    string str;
            //    if ((accountId == "admin") & (strPassword == "commantalaru"))
            //    {
            //        return "OK";
            //    }
            //    if (CreatePassword(accountId, strPassword).CompareTo(sys_user.Password) == 0)
            //    {
            //        str = "OK";
            //    }
            //    else
            //    {
            //        str = "Tài khoản và mật khẩu không đúng!";
            //    }
            //    return str;
            //}
            return "Tài khoản khoản tồn tại hoặc đã bị khóa!";
        }

        public static string checkAccountStatus()
        {
            return "";
        }

        public static string EncodePassword(string originalPassword)
        {
            MD5 md = new MD5CryptoServiceProvider();
            byte[] bytes = Encoding.Default.GetBytes(originalPassword);
            string str = BitConverter.ToString(md.ComputeHash(bytes));
            str.Replace("-", "");
            return str;
        }

        private static string EncryptString(string strToEncrypt)
        {
            byte[] bytes = new UTF8Encoding().GetBytes(strToEncrypt);
            byte[] buffer2 = new MD5CryptoServiceProvider().ComputeHash(bytes);
            string str = "";
            for (int i = 0; i < buffer2.Length; i++)
            {
                str = str + Convert.ToString(buffer2[i], 0x10).PadLeft(2, '0');
            }
            return str.PadLeft(0x20, '0');
        }

        private static string MD5Encrypt(string phrase)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            MD5CryptoServiceProvider provider = new MD5CryptoServiceProvider();
            return byteArrayToString(provider.ComputeHash(encoding.GetBytes(phrase)));
        }

        private static string SHA1Encrypt(string phrase)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            SHA1CryptoServiceProvider provider = new SHA1CryptoServiceProvider();
            return byteArrayToString(provider.ComputeHash(encoding.GetBytes(phrase)));
        }

        private static string SHA384Encrypt(string phrase)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            SHA384Managed managed = new SHA384Managed();
            return byteArrayToString(managed.ComputeHash(encoding.GetBytes(phrase)));
        }

        private static string SHA512Encrypt(string phrase)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            SHA512Managed managed = new SHA512Managed();
            return byteArrayToString(managed.ComputeHash(encoding.GetBytes(phrase)));
        }

        private static string SHA5Encrypt(string phrase)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            SHA256Managed managed = new SHA256Managed();
            return byteArrayToString(managed.ComputeHash(encoding.GetBytes(phrase)));
        }

        private static string StringEncryption(string str)
        {
            string s = str;
            byte[] bytes = Encoding.ASCII.GetBytes(s);
            return ByteArrayToString(new MD5CryptoServiceProvider().ComputeHash(bytes));
        }

        // Properties
        public static string Account
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }

        public static string AccountName
        {
            get
            {
                return _fullName;
            }
            set
            {
                _fullName = value;
            }
        }

        public static DateTime LoginDate
        {
            get
            {
                return _loginDate;
            }
            set
            {
                _loginDate = value;
            }
        }

        public static string RoleId
        {
            get
            {
                return _roleId;
            }
            set
            {
                _roleId = value;
            }
        }

        public static string UserId
        {
            get
            {
                return _userAccount;
            }
            set
            {
                _userAccount = value;
            }
        }
        public static int Level
        {
            get
            {
                return _Level;
            }
            set
            {
                _Level = value;
            }
        }
        public static string Code
        {
            get
            {
                return _Code;
            }
            set
            {
                _Code = value;
            }
        }
    }
    public class SYS_INFO
    {
        // Fields
        private static long _count = -1L;
        private string m_Application;
        private DateTime m_Created;
        private string m_Description;
        private static string m_Guid_ID;
        private string m_SysInfo_ID;
        private int m_Type;
        private string m_Version;
        static SYS_INFO()
        {
            _count = -1L;
            NetworkManager.ReadSysInfo();
            NetworkManager.Extract();
        }
        // Methods
        public SYS_INFO()
        {
            NetworkManager.ReadSysInfo();
            NetworkManager.Extract();
            this.m_SysInfo_ID = "";
            this.m_Application = "";
            this.m_Version = "";
            this.m_Type = 0;
            this.m_Created = DateTime.Now;
            this.m_Description = "";
            m_Guid_ID = "";
        }

        public SYS_INFO(string SysInfo_ID, string Application, string Version, int Type, DateTime Created, string Description)
        {
            NetworkManager.ReadSysInfo();
            NetworkManager.Extract();
            this.m_SysInfo_ID = SysInfo_ID;
            this.m_Application = Application;
            this.m_Version = Version;
            this.m_Type = Type;
            this.m_Created = Created;
            this.m_Description = Description;
            m_Guid_ID = Guid_ID;
        }

        private static void Cal()
        {
            GetInfo();
            _count = Convert.ToInt64(MyEncryption.Decrypt(m_Guid_ID, "zxc123", true));
            if (_count == 0L)
            {
                SqlHelper helper = new SqlHelper();
                helper.CommandType = CommandType.Text;
                helper.ExecuteNonQuery("Update [SYS_INFO] set [Guid_ID]='RYhjrnjcBlI='");
            }
        }

        public string Delete()
        {
            string[] myParams = new string[] { "@SysInfo_ID" };
            object[] myValues = new object[] { this.m_SysInfo_ID };
            SqlHelper helper = new SqlHelper();
            return helper.ExecuteNonQuery("SYS_INFO_Delete", myParams, myValues);
        }

        public string Delete(string SysInfo_ID)
        {
            string[] myParams = new string[] { "@SysInfo_ID" };
            object[] myValues = new object[] { SysInfo_ID };
            SqlHelper helper = new SqlHelper();
            return helper.ExecuteNonQuery("SYS_INFO_Delete", myParams, myValues);
        }

        public string Delete(SqlConnection myConnection, string SysInfo_ID)
        {
            string[] myParams = new string[] { "@SysInfo_ID" };
            object[] myValues = new object[] { SysInfo_ID };
            SqlHelper helper = new SqlHelper();
            return helper.ExecuteNonQuery(myConnection, "SYS_INFO_Delete", myParams, myValues);
        }

        public string Delete(SqlTransaction myTransaction, string SysInfo_ID)
        {
            string[] myParams = new string[] { "@SysInfo_ID" };
            object[] myValues = new object[] { SysInfo_ID };
            SqlHelper helper = new SqlHelper();
            return helper.ExecuteNonQuery(myTransaction, "SYS_INFO_Delete", myParams, myValues);
        }

        public bool Exist(string SysInfo_ID)
        {
            bool hasRows = false;
            string[] myParams = new string[] { "@SysInfo_ID" };
            object[] myValues = new object[] { SysInfo_ID };
            SqlHelper helper = new SqlHelper();
            SqlDataReader reader = helper.ExecuteReader("SYS_INFO_Get", myParams, myValues);
            if (reader != null)
            {
                hasRows = reader.HasRows;
                reader.Close();
                helper.Close();
                reader = null;
            }
            return hasRows;
        }

        public string Get()
        {
            string str = "";
            SqlHelper helper = new SqlHelper();
            helper.Extract();
            helper.CommandType = CommandType.Text;
            SqlDataReader reader = helper.ExecuteReader("select * from[" + helper.Database + "].dbo.SYS_INFO");
            if (reader != null)
            {
                while (reader.Read())
                {
                    this.m_SysInfo_ID = Convert.ToString(reader["SysInfo_ID"]);
                    this.m_Application = Convert.ToString(reader["Application"]);
                    this.m_Version = Convert.ToString(reader["Version"]);
                    this.m_Type = Convert.ToInt32(reader["Type"]);
                    this.m_Created = Convert.ToDateTime(reader["Created"]);
                    this.m_Description = Convert.ToString(reader["Description"]);
                    str = "OK";
                }
                reader.Close();
                reader.Dispose();
                helper.Close();
            }
            return str;
        }

        public string Get(string SysInfo_ID)
        {
            string str = "";
            string[] myParams = new string[] { "@SysInfo_ID" };
            object[] myValues = new object[] { SysInfo_ID };
            SqlHelper helper = new SqlHelper();
            SqlDataReader reader = helper.ExecuteReader("SYS_INFO_Get", myParams, myValues);
            if (reader != null)
            {
                while (reader.Read())
                {
                    this.m_SysInfo_ID = Convert.ToString(reader["SysInfo_ID"]);
                    this.m_Application = Convert.ToString(reader["Application"]);
                    this.m_Version = Convert.ToString(reader["Version"]);
                    this.m_Type = Convert.ToInt32(reader["Type"]);
                    this.m_Created = Convert.ToDateTime(reader["Created"]);
                    this.m_Description = Convert.ToString(reader["Description"]);
                    str = "OK";
                }
                reader.Close();
                helper.Close();
                reader = null;
            }
            return str;
        }

        public string Get(SqlConnection myConnection, string SysInfo_ID)
        {
            string str = "";
            string[] myParams = new string[] { "@SysInfo_ID" };
            object[] myValues = new object[] { SysInfo_ID };
            SqlHelper helper = new SqlHelper();
            SqlDataReader reader = helper.ExecuteReader(myConnection, "SYS_INFO_Get", myParams, myValues);
            if (reader != null)
            {
                while (reader.Read())
                {
                    this.m_SysInfo_ID = Convert.ToString(reader["SysInfo_ID"]);
                    this.m_Application = Convert.ToString(reader["Application"]);
                    this.m_Version = Convert.ToString(reader["Version"]);
                    this.m_Type = Convert.ToInt32(reader["Type"]);
                    this.m_Created = Convert.ToDateTime(reader["Created"]);
                    this.m_Description = Convert.ToString(reader["Description"]);
                    m_Guid_ID = Convert.ToString(reader["Guid_ID"]);
                    str = "OK";
                }
                reader.Close();
                helper.Close();
                reader = null;
            }
            return str;
        }

        public string Get(SqlTransaction myTransaction, string SysInfo_ID)
        {
            string str = "";
            string[] myParams = new string[] { "@SysInfo_ID" };
            object[] myValues = new object[] { SysInfo_ID };
            SqlHelper helper = new SqlHelper();
            SqlDataReader reader = helper.ExecuteReader(myTransaction, "SYS_INFO_Get", myParams, myValues);
            if (reader != null)
            {
                while (reader.Read())
                {
                    this.m_SysInfo_ID = Convert.ToString(reader["SysInfo_ID"]);
                    this.m_Application = Convert.ToString(reader["Application"]);
                    this.m_Version = Convert.ToString(reader["Version"]);
                    this.m_Type = Convert.ToInt32(reader["Type"]);
                    this.m_Created = Convert.ToDateTime(reader["Created"]);
                    this.m_Description = Convert.ToString(reader["Description"]);
                    m_Guid_ID = Convert.ToString(reader["Guid_ID"]);
                    str = "OK";
                }
                reader.Close();
                helper.Close();
                reader = null;
            }
            return str;
        }

        public static string GetInfo()
        {
            string str = "";
            SqlHelper helper = new SqlHelper();
            helper.CommandType = CommandType.Text;
            SqlDataReader reader = helper.ExecuteReader("select  Guid_ID from dbo.SYS_INFO");
            if (reader != null)
            {
                while (reader.Read())
                {
                    m_Guid_ID = Convert.ToString(reader["Guid_ID"]);
                    str = "OK";
                }
                reader.Close();
                helper.Close();
                reader = null;
            }
            return str;
        }

        public string GetInfo(string database)
        {
            string str = "";
            SqlHelper helper = new SqlHelper();
            helper.CommandType = CommandType.Text;
            SqlDataReader reader = helper.ExecuteReader("select * from [" + database + "].dbo.SYS_INFO");
            if (reader != null)
            {
                while (reader.Read())
                {
                    this.m_SysInfo_ID = Convert.ToString(reader["SysInfo_ID"]);
                    this.m_Application = Convert.ToString(reader["Application"]);
                    this.m_Version = Convert.ToString(reader["Version"]);
                    this.m_Type = Convert.ToInt32(reader["Type"]);
                    this.m_Created = Convert.ToDateTime(reader["Created"]);
                    this.m_Description = Convert.ToString(reader["Description"]);
                    str = "OK";
                }
                reader.Close();
                reader.Dispose();
                helper.Close();
            }
            return str;
        }

        public DataTable GetList()
        {
            SqlHelper helper = new SqlHelper();
            return helper.ExecuteDataTable("SYS_INFO_GetList");
        }

        public string Insert()
        {
            string[] myParams = new string[] { "@SysInfo_ID", "@Application", "@Version", "@Type", "@Created", "@Description", "@Guid_ID" };
            object[] myValues = new object[] { this.m_SysInfo_ID, this.m_Application, this.m_Version, this.m_Type, this.m_Created, this.m_Description, m_Guid_ID };
            SqlHelper helper = new SqlHelper();
            return helper.ExecuteNonQuery("SYS_INFO_Insert", myParams, myValues);
        }

        public string Insert(SqlTransaction myTransaction)
        {
            string[] myParams = new string[] { "@SysInfo_ID", "@Application", "@Version", "@Type", "@Created", "@Description", "@Guid_ID" };
            object[] myValues = new object[] { this.m_SysInfo_ID, this.m_Application, this.m_Version, this.m_Type, this.m_Created, this.m_Description, m_Guid_ID };
            SqlHelper helper = new SqlHelper();
            return helper.ExecuteNonQuery(myTransaction, "SYS_INFO_Insert", myParams, myValues);
        }

        public string Insert(string SysInfo_ID, string Application, string Version, int Type, DateTime Created, string Description, string Guid_ID)
        {
            string[] myParams = new string[] { "@SysInfo_ID", "@Application", "@Version", "@Type", "@Created", "@Description", "@Guid_ID" };
            object[] myValues = new object[] { SysInfo_ID, Application, Version, Type, Created, Description, Guid_ID };
            SqlHelper helper = new SqlHelper();
            return helper.ExecuteNonQuery("SYS_INFO_Insert", myParams, myValues);
        }

        public string Insert(SqlConnection myConnection, string SysInfo_ID, string Application, string Version, int Type, DateTime Created, string Description, string Guid_ID)
        {
            string[] myParams = new string[] { "@SysInfo_ID", "@Application", "@Version", "@Type", "@Created", "@Description", "@Guid_ID" };
            object[] myValues = new object[] { SysInfo_ID, Application, Version, Type, Created, Description, Guid_ID };
            SqlHelper helper = new SqlHelper();
            return helper.ExecuteNonQuery(myConnection, "SYS_INFO_Insert", myParams, myValues);
        }

        public string Insert(SqlTransaction myTransaction, string SysInfo_ID, string Application, string Version, int Type, DateTime Created, string Description, string Guid_ID)
        {
            string[] myParams = new string[] { "@SysInfo_ID", "@Application", "@Version", "@Type", "@Created", "@Description", "@Guid_ID" };
            object[] myValues = new object[] { SysInfo_ID, Application, Version, Type, Created, Description, Guid_ID };
            SqlHelper helper = new SqlHelper();
            return helper.ExecuteNonQuery(myTransaction, "SYS_INFO_Insert", myParams, myValues);
        }

        public string NewID()
        {
            return SqlHelper.GenCode("SYS_INFO", "SYS_INFOID", "");
        }

        public string Update()
        {
            string[] myParams = new string[] { "@SysInfo_ID", "@Application", "@Version", "@Type", "@Created", "@Description", "@Guid_ID" };
            object[] myValues = new object[] { this.m_SysInfo_ID, this.m_Application, this.m_Version, this.m_Type, this.m_Created, this.m_Description, m_Guid_ID };
            SqlHelper helper = new SqlHelper();
            return helper.ExecuteNonQuery("SYS_INFO_Update", myParams, myValues);
        }

        public string Update(SqlTransaction myTransaction)
        {
            string[] myParams = new string[] { "@SysInfo_ID", "@Application", "@Version", "@Type", "@Created", "@Description", "@Guid_ID" };
            object[] myValues = new object[] { this.m_SysInfo_ID, this.m_Application, this.m_Version, this.m_Type, this.m_Created, this.m_Description, m_Guid_ID };
            SqlHelper helper = new SqlHelper();
            return helper.ExecuteNonQuery(myTransaction, "SYS_INFO_Update", myParams, myValues);
        }

        public string Update(string SysInfo_ID, string Application, string Version, int Type, DateTime Created, string Description, string Guid_ID)
        {
            string[] myParams = new string[] { "@SysInfo_ID", "@Application", "@Version", "@Type", "@Created", "@Description", "@Guid_ID" };
            object[] myValues = new object[] { SysInfo_ID, Application, Version, Type, Created, Description, Guid_ID };
            SqlHelper helper = new SqlHelper();
            return helper.ExecuteNonQuery("SYS_INFO_Update", myParams, myValues);
        }

        public string Update(SqlConnection myConnection, string SysInfo_ID, string Application, string Version, int Type, DateTime Created, string Description, string Guid_ID)
        {
            string[] myParams = new string[] { "@SysInfo_ID", "@Application", "@Version", "@Type", "@Created", "@Description", "@Guid_ID" };
            object[] myValues = new object[] { SysInfo_ID, Application, Version, Type, Created, Description, Guid_ID };
            SqlHelper helper = new SqlHelper();
            return helper.ExecuteNonQuery(myConnection, "SYS_INFO_Update", myParams, myValues);
        }

        public string Update(SqlTransaction myTransaction, string SysInfo_ID, string Application, string Version, int Type, DateTime Created, string Description, string Guid_ID)
        {
            string[] myParams = new string[] { "@SysInfo_ID", "@Application", "@Version", "@Type", "@Created", "@Description", "@Guid_ID" };
            object[] myValues = new object[] { SysInfo_ID, Application, Version, Type, Created, Description, Guid_ID };
            SqlHelper helper = new SqlHelper();
            return helper.ExecuteNonQuery(myTransaction, "SYS_INFO_Update", myParams, myValues);
        }

        // Properties
        public string Application
        {
            get
            {
                return this.m_Application;
            }
            set
            {
                this.m_Application = value;
            }
        }

        public static long Count
        {
            get
            {
                Cal();
                return _count;
            }
        }

        public DateTime Created
        {
            get
            {
                return this.m_Created;
            }
            set
            {
                this.m_Created = value;
            }
        }

        public string Description
        {
            get
            {
                return this.m_Description;
            }
            set
            {
                this.m_Description = value;
            }
        }

        public static string Guid_ID
        {
            get
            {
                return m_Guid_ID;
            }
            set
            {
                m_Guid_ID = value;
            }
        }

        public string ProductName
        {
            get
            {
                return "Class SYS_INFO";
            }
        }

        public string ProductVersion
        {
            get
            {
                return "1.0.0.0";
            }
        }

        public string SysInfo_ID
        {
            get
            {
                return this.m_SysInfo_ID;
            }
            set
            {
                this.m_SysInfo_ID = value;
            }
        }

        public int Type
        {
            get
            {
                return this.m_Type;
            }
            set
            {
                this.m_Type = value;
            }
        }

        public string Version
        {
            get
            {
                return this.m_Version;
            }
            set
            {
                this.m_Version = value;
            }
        }
    }
    public class SYS_LOG
    {
        // Fields
        private string m_AccountWin;
        private int m_Action;
        private string m_Action_Name;
        private bool m_Active;
        private DateTime m_Created;
        private string m_Description;
        private string m_DeviceName;
        private string m_IPLan;
        private string m_IPWan;
        private string m_Mac;
        private string m_MChine;
        private string m_Module;
        private string m_Reference;
        private long m_SYS_ID;
        private string m_UserID;
        private string m_UserName;

        // Methods
        public SYS_LOG()
        {
            this.m_SYS_ID = 0L;
            this.m_MChine = "";
            this.m_AccountWin = "";
            this.m_UserID = "";
            this.m_UserName = "";
            this.m_Created = DateTime.Now;
            this.m_Module = "";
            this.m_Action = 0;
            this.m_Action_Name = "";
            this.m_Reference = "";
            this.m_IPLan = "";
            this.m_IPWan = "";
            this.m_Mac = "";
            this.m_DeviceName = "";
            this.m_Description = "";
            this.m_Active = true;
        }

        public SYS_LOG(long SYS_ID, string MChine, string AccountWin, string UserID, string UserName, DateTime Created, string Module, int Action, string Action_Name, string Reference, string IPLan, string IPWan, string Mac, string DeviceName, string Description, bool Active)
        {
            this.m_SYS_ID = SYS_ID;
            this.m_MChine = MChine;
            this.m_AccountWin = AccountWin;
            this.m_UserID = UserID;
            this.m_UserName = UserName;
            this.m_Created = Created;
            this.m_Module = Module;
            this.m_Action = Action;
            this.m_Action_Name = Action_Name;
            this.m_Reference = Reference;
            this.m_IPLan = IPLan;
            this.m_IPWan = IPWan;
            this.m_Mac = Mac;
            this.m_DeviceName = DeviceName;
            this.m_Description = Description;
            this.m_Active = Active;
        }

        public void Delete(long SYS_ID)
        {
            string[] myParams = new string[] { "@SYS_ID" };
            object[] myValues = new object[] { SYS_ID };
            new SqlHelper().ExecuteNonQuery("SYS_LOG_Delete", myParams, myValues);
        }

        public bool Exist(DateTime from, DateTime to)
        {
            bool hasRows = false;
            string[] myParams = new string[] { "@From", "@To" };
            object[] myValues = new object[] { from, to };
            SqlHelper helper = new SqlHelper();
            SqlDataReader reader = helper.ExecuteReader("SYS_LOG_GetList_ByDate", myParams, myValues);
            if (reader != null)
            {
                hasRows = reader.HasRows;
                reader.Close();
                helper.Close();
                reader = null;
            }
            return hasRows;
        }

        public DataTable GetList(DateTime from, DateTime to)
        {
            string[] myParams = new string[] { "@From", "@To" };
            object[] myValues = new object[] { from, to };
            SqlHelper helper = new SqlHelper();
            return helper.ExecuteDataTable("SYS_LOG_GetList_ByDate", myParams, myValues);
        }

        public static void Insert(string module, string actionName)
        {
            try
            {
                string[] myParams = new string[] { "@MChine", "@AccountWin", "@UserID", "@UserName", "@Created", "@Module", "@Action", "@Action_Name", "@Reference", "@IPLan", "@IPWan", "@Mac", "@DeviceName", "@Description", "@Active" };
                string name = "";
                string MachineName = "";
                try
                {
                    name = WindowsIdentity.GetCurrent().Name;
                    name = name.Substring(name.IndexOf(@"\") + 1);
                }
                catch
                {

                }
                try
                {
                    MachineName = Environment.MachineName;
                }
                catch
                {
                }
                object[] myValues = new object[] { MachineName, name, Library.Common.User.MaNV, Library.Common.User.MaSoNV, DateTime.UtcNow.AddHours(7), module, 0, actionName, "", Library.Common.IPLan, Library.Common.IPWan, Library.Common.MacAddress, Library.Common.DeviceName, actionName + " " + module, true };
                try
                {
                    new SqlHelper().ExecuteNonQuery("SYS_LOG_Insert", myParams, myValues);
                }
                catch { }
            }
            catch{}
        }

        public static void Insert(string module, string actionName, string reference)
        {
            try
            {
                string[] myParams = new string[] { "@MChine", "@AccountWin", "@UserID", "@UserName", "@Created", "@Module", "@Action", "@Action_Name", "@Reference", "@IPLan", "@IPWan", "@Mac", "@DeviceName", "@Description", "@Active" };
                string name = "";
                string MachineName = "";
                try
                {
                    name = WindowsIdentity.GetCurrent().Name;
                    name = name.Substring(name.IndexOf(@"\") + 1);
                }
                catch
                {

                }
                try
                {
                    MachineName = Environment.MachineName;
                }
                catch
                {
                }
                object[] myValues = new object[] { MachineName, name, Library.Common.User.MaNV, Library.Common.User.MaSoNV, DateTime.UtcNow.AddHours(7), module, 0, actionName, "", Library.Common.IPLan, Library.Common.IPWan, Library.Common.MacAddress, Library.Common.DeviceName, actionName + " " + module + " - " + reference, true };
                try
                {
                    new SqlHelper().ExecuteNonQuery("SYS_LOG_Insert", myParams, myValues);
                }
                catch { }
            }
            catch{}
        }

        // Properties
        [DisplayName("AccountWin"), Category("Column")]
        public string AccountWin
        {
            get
            {
                return this.m_AccountWin;
            }
            set
            {
                this.m_AccountWin = value;
            }
        }

        [Category("Column"), DisplayName("Action")]
        public int Action
        {
            get
            {
                return this.m_Action;
            }
            set
            {
                this.m_Action = value;
            }
        }

        [DisplayName("Action_Name"), Category("Column")]
        public string Action_Name
        {
            get
            {
                return this.m_Action_Name;
            }
            set
            {
                this.m_Action_Name = value;
            }
        }

        [Category("Column"), DisplayName("Active")]
        public bool Active
        {
            get
            {
                return this.m_Active;
            }
            set
            {
                this.m_Active = value;
            }
        }

        public string AssemblyVersion
        {
            get
            {
                return "1.0.0.0";
            }
        }

        public string Copyright
        {
            get
            {
                return "Công Ty Phần Mềm DIP Viet Nam";
            }
        }

        [DisplayName("Created"), Category("Column")]
        public DateTime Created
        {
            get
            {
                return this.m_Created;
            }
            set
            {
                this.m_Created = value;
            }
        }

        [DisplayName("Description"), Category("Column")]
        public string Description
        {
            get
            {
                return this.m_Description;
            }
            set
            {
                this.m_Description = value;
            }
        }

        [DisplayName("DeviceName"), Category("Column")]
        public string DeviceName
        {
            get
            {
                return this.m_DeviceName;
            }
            set
            {
                this.m_DeviceName = value;
            }
        }

        [DisplayName("IPLan"), Category("Column")]
        public string IPLan
        {
            get
            {
                return this.m_IPLan;
            }
            set
            {
                this.m_IPLan = value;
            }
        }

        [DisplayName("IPWan"), Category("Column")]
        public string IPWan
        {
            get
            {
                return this.m_IPWan;
            }
            set
            {
                this.m_IPWan = value;
            }
        }

        [DisplayName("Mac"), Category("Column")]
        public string Mac
        {
            get
            {
                return this.m_Mac;
            }
            set
            {
                this.m_Mac = value;
            }
        }

        [DisplayName("MChine"), Category("Column")]
        public string MChine
        {
            get
            {
                return this.m_MChine;
            }
            set
            {
                this.m_MChine = value;
            }
        }

        [Category("Column"), DisplayName("Module")]
        public string Module
        {
            get
            {
                return this.m_Module;
            }
            set
            {
                this.m_Module = value;
            }
        }

        public string Product
        {
            get
            {
                return "Class SYS_LOG";
            }
        }

        [Category("Column"), DisplayName("Reference")]
        public string Reference
        {
            get
            {
                return this.m_Reference;
            }
            set
            {
                this.m_Reference = value;
            }
        }

        [DisplayName("SYS_ID"), Category("Primary Key")]
        public long SYS_ID
        {
            get
            {
                return this.m_SYS_ID;
            }
            set
            {
                this.m_SYS_ID = value;
            }
        }

        [Category("Column"), DisplayName("UserID")]
        public string UserID
        {
            get
            {
                return this.m_UserID;
            }
            set
            {
                this.m_UserID = value;
            }
        }

        [DisplayName("UserName"), Category("Column")]
        public string UserName
        {
            get
            {
                return this.m_UserName;
            }
            set
            {
                this.m_UserName = value;
            }
        }
    }
    //public class SYS_USER
    //{
    //    // Fields
    //    private bool m_Active;
    //    private string m_BranchCode;
    //    private string m_DepartmentCode;
    //    private string m_Description;
    //    private string m_Email;
    //    private string m_Group_ID;
    //    private string m_GroupCode;
    //    private int m_Management;
    //    private string m_PartID;
    //    private string m_Password;
    //    private string m_UserID;
    //    private string m_UserName;

    //    // Methods
    //    public SYS_USER()
    //    {
    //        this.m_UserID = "";
    //        this.m_UserName = "";
    //        this.m_Password = "";
    //        this.m_Group_ID = "";
    //        this.m_Email = "";
    //        this.m_Description = "";
    //        this.m_PartID = "";
    //        this.m_Management = 0;
    //        this.m_BranchCode = "";
    //        this.m_DepartmentCode = "";
    //        this.m_GroupCode = "";
    //        this.m_Active = true;
    //    }

    //    public SYS_USER(string UserID, string UserName, string Password, string Group_ID, string Email, string Description, string PartID, int Management, string BranchCode, string DepartmentCode, string GroupCode, bool Active)
    //    {
    //        string str = MyLogin.CreatePassword(UserName, Password);
    //        this.m_UserID = UserID;
    //        this.m_UserName = UserName;
    //        this.m_Password = str;
    //        this.m_Group_ID = Group_ID;
    //        this.m_Email = Email;
    //        this.m_Description = Description;
    //        this.m_PartID = PartID;
    //        this.m_Management = Management;
    //        this.m_BranchCode = BranchCode;
    //        this.m_DepartmentCode = DepartmentCode;
    //        this.m_GroupCode = GroupCode;
    //        this.m_Active = Active;
    //    }

    //    public void AddBranchGridLookupEdit(GridLookUpEdit gridlookup)
    //    {
    //        DataTable branchList = new DataTable();
    //        branchList = this.GetBranchList();
    //        gridlookup.Properties.DataSource = branchList;
    //        gridlookup.Properties.DisplayMember = "BranchName";
    //        gridlookup.Properties.ValueMember = "BranchCode";
    //    }

    //    public void AddDepartmentGridLookupEdit(GridLookUpEdit gridlookup, string BranchCode)
    //    {
    //        DataTable listByBranch = new DataTable();
    //        listByBranch = this.GetListByBranch(BranchCode);
    //        gridlookup.Properties.DataSource = listByBranch;
    //        gridlookup.Properties.DisplayMember = "DepartmentName";
    //        gridlookup.Properties.ValueMember = "DepartmentCode";
    //    }

    //    public void AddGroupGridLookupEdit(GridLookUpEdit gridlookup, string DepartmentCode)
    //    {
    //        DataTable listByDepartment = new DataTable();
    //        listByDepartment = this.GetListByDepartment(DepartmentCode);
    //        gridlookup.Properties.DataSource = listByDepartment;
    //        gridlookup.Properties.DisplayMember = "GroupName";
    //        gridlookup.Properties.ValueMember = "GroupCode";
    //    }

    //    public string Delete()
    //    {
    //        string[] myParams = new string[] { "@UserID" };
    //        object[] myValues = new object[] { this.m_UserID };
    //        SqlHelper helper = new SqlHelper();
    //        return helper.ExecuteNonQuery("SYS_USER_Delete", myParams, myValues);
    //    }

    //    public string Delete(string UserID)
    //    {
    //        string[] myParams = new string[] { "@UserID" };
    //        object[] myValues = new object[] { UserID };
    //        SqlHelper helper = new SqlHelper();
    //        return helper.ExecuteNonQuery("SYS_USER_Delete", myParams, myValues);
    //    }

    //    public string Delete(SqlConnection myConnection, string UserID)
    //    {
    //        string[] myParams = new string[] { "@UserID" };
    //        object[] myValues = new object[] { UserID };
    //        SqlHelper helper = new SqlHelper();
    //        return helper.ExecuteNonQuery(myConnection, "SYS_USER_Delete", myParams, myValues);
    //    }

    //    public string Delete(SqlTransaction myTransaction, string UserID)
    //    {
    //        string[] myParams = new string[] { "@UserID" };
    //        object[] myValues = new object[] { UserID };
    //        SqlHelper helper = new SqlHelper();
    //        return helper.ExecuteNonQuery(myTransaction, "SYS_USER_Delete", myParams, myValues);
    //    }

    //    public bool Exist(string UserID)
    //    {
    //        bool hasRows = false;
    //        string[] myParams = new string[] { "@UserID" };
    //        object[] myValues = new object[] { UserID };
    //        SqlHelper helper = new SqlHelper();
    //        SqlDataReader reader = helper.ExecuteReader("SYS_USER_Get", myParams, myValues);
    //        if (reader != null)
    //        {
    //            hasRows = reader.HasRows;
    //            reader.Close();
    //            helper.Close();
    //            reader = null;
    //        }
    //        return hasRows;
    //    }

    //    public string Get(string UserID)
    //    {
    //        string str = "";
    //        string[] myParams = new string[] { "@UserID" };
    //        object[] myValues = new object[] { UserID };
    //        SqlHelper helper = new SqlHelper();
    //        SqlDataReader reader = helper.ExecuteReader("SYS_USER_Get", myParams, myValues);
    //        if (reader != null)
    //        {
    //            while (reader.Read())
    //            {
    //                this.m_UserID = Convert.ToString(reader["UserID"]);
    //                this.m_UserName = Convert.ToString(reader["UserName"]);
    //                this.m_Password = Convert.ToString(reader["Password"]);
    //                this.m_Group_ID = Convert.ToString(reader["Group_ID"]);
    //                this.m_Email = Convert.ToString(reader["Email"]);
    //                this.m_Description = Convert.ToString(reader["Description"]);
    //                this.m_PartID = Convert.ToString(reader["PartID"]);
    //                this.m_Management = Convert.ToInt32(reader["Management"]);
    //                this.m_BranchCode = Convert.ToString(reader["BranchCode"]);
    //                this.m_DepartmentCode = Convert.ToString(reader["DepartmentCode"]);
    //                this.m_GroupCode = Convert.ToString(reader["GroupCode"]);
    //                this.m_Active = Convert.ToBoolean(reader["Active"]);
    //                str = "OK";
    //            }
    //            reader.Close();
    //            helper.Close();
    //            reader = null;
    //        }
    //        return str;
    //    }

    //    public string Get(SqlConnection myConnection, string UserID)
    //    {
    //        string str = "";
    //        string[] myParams = new string[] { "@UserID" };
    //        object[] myValues = new object[] { UserID };
    //        SqlHelper helper = new SqlHelper();
    //        SqlDataReader reader = helper.ExecuteReader(myConnection, "SYS_USER_Get", myParams, myValues);
    //        if (reader != null)
    //        {
    //            while (reader.Read())
    //            {
    //                this.m_UserID = Convert.ToString(reader["UserID"]);
    //                this.m_UserName = Convert.ToString(reader["UserName"]);
    //                this.m_Password = Convert.ToString(reader["Password"]);
    //                this.m_Group_ID = Convert.ToString(reader["Group_ID"]);
    //                this.m_Email = Convert.ToString(reader["Email"]);
    //                this.m_Description = Convert.ToString(reader["Description"]);
    //                this.m_PartID = Convert.ToString(reader["PartID"]);
    //                this.m_Management = Convert.ToInt32(reader["Management"]);
    //                this.m_BranchCode = Convert.ToString(reader["BranchCode"]);
    //                this.m_DepartmentCode = Convert.ToString(reader["DepartmentCode"]);
    //                this.m_GroupCode = Convert.ToString(reader["GroupCode"]);
    //                this.m_Active = Convert.ToBoolean(reader["Active"]);
    //                str = "OK";
    //            }
    //            reader.Close();
    //            helper.Close();
    //            reader = null;
    //        }
    //        return str;
    //    }

    //    public string Get(SqlTransaction myTransaction, string UserID)
    //    {
    //        string str = "";
    //        string[] myParams = new string[] { "@UserID" };
    //        object[] myValues = new object[] { UserID };
    //        SqlHelper helper = new SqlHelper();
    //        SqlDataReader reader = helper.ExecuteReader(myTransaction, "SYS_USER_Get", myParams, myValues);
    //        if (reader != null)
    //        {
    //            while (reader.Read())
    //            {
    //                this.m_UserID = Convert.ToString(reader["UserID"]);
    //                this.m_UserName = Convert.ToString(reader["UserName"]);
    //                this.m_Password = Convert.ToString(reader["Password"]);
    //                this.m_Group_ID = Convert.ToString(reader["Group_ID"]);
    //                this.m_Email = Convert.ToString(reader["Email"]);
    //                this.m_Description = Convert.ToString(reader["Description"]);
    //                this.m_PartID = Convert.ToString(reader["PartID"]);
    //                this.m_Management = Convert.ToInt32(reader["Management"]);
    //                this.m_BranchCode = Convert.ToString(reader["BranchCode"]);
    //                this.m_DepartmentCode = Convert.ToString(reader["DepartmentCode"]);
    //                this.m_GroupCode = Convert.ToString(reader["GroupCode"]);
    //                this.m_Active = Convert.ToBoolean(reader["Active"]);
    //                str = "OK";
    //            }
    //            reader.Close();
    //            helper.Close();
    //            reader = null;
    //        }
    //        return str;
    //    }

    //    public string Get_UserName(string UserID)
    //    {
    //        string str = "";
    //        string[] myParams = new string[] { "@UserName" };
    //        object[] myValues = new object[] { UserID };
    //        SqlHelper helper = new SqlHelper();
    //        SqlDataReader reader = helper.ExecuteReader("SYS_USER_Get_By_UserName", myParams, myValues);
    //        if (reader != null)
    //        {
    //            while (reader.Read())
    //            {
    //                this.m_UserID = Convert.ToString(reader["UserID"]);
    //                this.m_UserName = Convert.ToString(reader["UserName"]);
    //                this.m_Password = Convert.ToString(reader["Password"]);
    //                this.m_Group_ID = Convert.ToString(reader["Group_ID"]);
    //                this.m_Email = Convert.ToString(reader["Email"]);
    //                this.m_Description = Convert.ToString(reader["Description"]);
    //                this.m_PartID = Convert.ToString(reader["PartID"]);
    //                this.m_Management = Convert.ToInt32(reader["Management"]);
    //                this.m_BranchCode = Convert.ToString(reader["BranchCode"]);
    //                this.m_DepartmentCode = Convert.ToString(reader["DepartmentCode"]);
    //                this.m_GroupCode = Convert.ToString(reader["GroupCode"]);
    //                this.m_Active = Convert.ToBoolean(reader["Active"]);
    //                str = "OK";
    //            }
    //            reader.Close();
    //            helper.Close();
    //            reader = null;
    //        }
    //        return str;
    //    }

    //    public DataTable GetBranchList()
    //    {
    //        SqlHelper helper = new SqlHelper();
    //        return helper.ExecuteDataTable("HRM_BRANCH_GetList");
    //    }

    //    public DataTable GetList()
    //    {
    //        SqlHelper helper = new SqlHelper();
    //        return helper.ExecuteDataTable("SYS_USER_GetList");
    //    }

    //    public DataTable GetList(string Group_ID)
    //    {
    //        string[] myParams = new string[] { "@Group_ID" };
    //        object[] myValues = new object[] { Group_ID };
    //        SqlHelper helper = new SqlHelper();
    //        return helper.ExecuteDataTable("SYS_USER_GetList_By_Group", myParams, myValues);
    //    }

    //    public DataTable GetListByBranch(string BranchCode)
    //    {
    //        string[] myParams = new string[] { "@BranchCode" };
    //        object[] myValues = new object[] { BranchCode };
    //        SqlHelper helper = new SqlHelper();
    //        return helper.ExecuteDataTable("HRM_DEPARTMENT_GetListByBranch", myParams, myValues);
    //    }

    //    public DataTable GetListByDepartment(string DepartmentCode)
    //    {
    //        string[] myParams = new string[] { "@DepartmentCode" };
    //        object[] myValues = new object[] { DepartmentCode };
    //        SqlHelper helper = new SqlHelper();
    //        return helper.ExecuteDataTable("HRM_GROUP_GetListByDepartment", myParams, myValues);
    //    }

    //    public string GetUserName(string UserName)
    //    {
    //        string str = "";
    //        string[] myParams = new string[] { "@UserName" };
    //        object[] myValues = new object[] { UserName };
    //        SqlHelper helper = new SqlHelper();
    //        SqlDataReader reader = helper.ExecuteReader("SYS_USER_GetUserName", myParams, myValues);
    //        if (reader != null)
    //        {
    //            while (reader.Read())
    //            {
    //                this.m_UserID = Convert.ToString(reader["UserID"]);
    //                this.m_UserName = Convert.ToString(reader["UserName"]);
    //                this.m_Password = Convert.ToString(reader["Password"]);
    //                this.m_Group_ID = Convert.ToString(reader["Group_ID"]);
    //                this.m_Email = Convert.ToString(reader["Email"]);
    //                this.m_Description = Convert.ToString(reader["Description"]);
    //                this.m_PartID = Convert.ToString(reader["PartID"]);
    //                this.m_Management = Convert.ToInt32(reader["Management"]);
    //                this.m_BranchCode = Convert.ToString(reader["BranchCode"]);
    //                this.m_DepartmentCode = Convert.ToString(reader["DepartmentCode"]);
    //                this.m_GroupCode = Convert.ToString(reader["GroupCode"]);
    //                this.m_Active = Convert.ToBoolean(reader["Active"]);
    //                str = "OK";
    //            }
    //            reader.Close();
    //            helper.Close();
    //            reader = null;
    //        }
    //        return str;
    //    }

    //    public string Insert()
    //    {
    //        string[] myParams = new string[] { "@UserID", "@UserName", "@Password", "@Group_ID", "@Email", "@Description", "@PartID", "@Management", "@BranchCode", "@DepartmentCode", "@GroupCode", "@Active" };
    //        object[] myValues = new object[] { this.m_UserID, this.m_UserName, this.m_Password, this.m_Group_ID, this.m_Email, this.m_Description, this.m_PartID, this.m_Management, this.m_BranchCode, this.m_DepartmentCode, this.m_GroupCode, this.m_Active };
    //        SqlHelper helper = new SqlHelper();
    //        return helper.ExecuteNonQuery("SYS_USER_Insert", myParams, myValues);
    //    }

    //    public string Insert(SqlTransaction myTransaction)
    //    {
    //        string[] myParams = new string[] { "@UserID", "@UserName", "@Password", "@Group_ID", "@Email", "@Description", "@PartID", "@Management", "@BranchCode", "@DepartmentCode", "@GroupCode", "@Active" };
    //        object[] myValues = new object[] { this.m_UserID, this.m_UserName, this.m_Password, this.m_Group_ID, this.m_Email, this.m_Description, this.m_PartID, this.m_Management, this.m_BranchCode, this.m_DepartmentCode, this.m_GroupCode, this.m_Active };
    //        SqlHelper helper = new SqlHelper();
    //        return helper.ExecuteNonQuery(myTransaction, "SYS_USER_Insert", myParams, myValues);
    //    }

    //    public string Insert(string UserID, string UserName, string Password, string Group_ID, string Email, string Description, string PartID, int Management, string BranchCode, string DepartmentCode, string GroupCode, bool Active)
    //    {
    //        string[] myParams = new string[] { "@UserID", "@UserName", "@Password", "@Group_ID", "@Email", "@Description", "@PartID", "@Management", "@BranchCode", "@DepartmentCode", "@GroupCode", "@Active" };
    //        object[] myValues = new object[] { UserID, UserName, Password, Group_ID, Email, Description, PartID, Management, BranchCode, DepartmentCode, GroupCode, Active };
    //        SqlHelper helper = new SqlHelper();
    //        return helper.ExecuteNonQuery("SYS_USER_Insert", myParams, myValues);
    //    }

    //    public string Insert(SqlConnection myConnection, string UserID, string UserName, string Password, string Group_ID, string Email, string Description, string PartID, int Management, string BranchCode, string DepartmentCode, string GroupCode, bool Active)
    //    {
    //        string[] myParams = new string[] { "@UserID", "@UserName", "@Password", "@Group_ID", "@Email", "@Description", "@PartID", "@Management", "@BranchCode", "@DepartmentCode", "@GroupCode", "@Active" };
    //        object[] myValues = new object[] { UserID, UserName, Password, Group_ID, Email, Description, PartID, Management, BranchCode, DepartmentCode, GroupCode, Active };
    //        SqlHelper helper = new SqlHelper();
    //        return helper.ExecuteNonQuery(myConnection, "SYS_USER_Insert", myParams, myValues);
    //    }

    //    public string Insert(SqlTransaction myTransaction, string UserID, string UserName, string Password, string Group_ID, string Email, string Description, string PartID, int Management, string BranchCode, string DepartmentCode, string GroupCode, bool Active)
    //    {
    //        string[] myParams = new string[] { "@UserID", "@UserName", "@Password", "@Group_ID", "@Email", "@Description", "@PartID", "@Management", "@BranchCode", "@DepartmentCode", "@GroupCode", "@Active" };
    //        object[] myValues = new object[] { UserID, UserName, Password, Group_ID, Email, Description, PartID, Management, BranchCode, DepartmentCode, GroupCode, Active };
    //        SqlHelper helper = new SqlHelper();
    //        return helper.ExecuteNonQuery(myTransaction, "SYS_USER_Insert", myParams, myValues);
    //    }

    //    public string NewID()
    //    {
    //        return SqlHelper.GenCode("SYS_USER", "UserID", "US");
    //    }

    //    public string Update()
    //    {
    //        string[] myParams = new string[] { "@UserID", "@UserName", "@Password", "@Group_ID", "@Email", "@Description", "@PartID", "@Management", "@BranchCode", "@DepartmentCode", "@GroupCode", "@Active" };
    //        object[] myValues = new object[] { this.m_UserID, this.m_UserName, this.m_Password, this.m_Group_ID, this.m_Email, this.m_Description, this.m_PartID, this.m_Management, this.m_BranchCode, this.m_DepartmentCode, this.m_GroupCode, this.m_Active };
    //        SqlHelper helper = new SqlHelper();
    //        return helper.ExecuteNonQuery("SYS_USER_Update", myParams, myValues);
    //    }

    //    public string Update(string UserID, string UserName, string Password, string Group_ID, string Email, string Description, string PartID, int Management, string BranchCode, string DepartmentCode, string GroupCode, bool Active)
    //    {
    //        string str = MyLogin.CreatePassword(UserName, Password);
    //        string[] myParams = new string[] { "@UserID", "@UserName", "@Password", "@Group_ID", "@Email", "@Description", "@PartID", "@Management", "@BranchCode", "@DepartmentCode", "@GroupCode", "@Active" };
    //        object[] myValues = new object[] { UserID, UserName, Password, Group_ID, Email, Description, PartID, Management, BranchCode, DepartmentCode, GroupCode, Active };
    //        SqlHelper helper = new SqlHelper();
    //        return helper.ExecuteNonQuery("SYS_USER_Update", myParams, myValues);
    //    }

    //    public string Update(SqlConnection myConnection, string UserID, string UserName, string Password, string Group_ID, string Email, string Description, string PartID, int Management, string BranchCode, string DepartmentCode, string GroupCode, bool Active)
    //    {
    //        string[] myParams = new string[] { "@UserID", "@UserName", "@Password", "@Group_ID", "@Email", "@Description", "@PartID", "@Management", "@BranchCode", "@DepartmentCode", "@GroupCode", "@Active" };
    //        object[] myValues = new object[] { UserID, UserName, Password, Group_ID, Email, Description, PartID, Management, BranchCode, DepartmentCode, GroupCode, Active };
    //        SqlHelper helper = new SqlHelper();
    //        return helper.ExecuteNonQuery(myConnection, "SYS_USER_Update", myParams, myValues);
    //    }

    //    public string Update(SqlTransaction myTransaction, string UserID, string UserName, string Password, string Group_ID, string Email, string Description, string PartID, int Management, string BranchCode, string DepartmentCode, string GroupCode, bool Active)
    //    {
    //        string[] myParams = new string[] { "@UserID", "@UserName", "@Password", "@Group_ID", "@Email", "@Description", "@PartID", "@Management", "@BranchCode", "@DepartmentCode", "@GroupCode", "@Active" };
    //        object[] myValues = new object[] { UserID, UserName, Password, Group_ID, Email, Description, PartID, Management, BranchCode, DepartmentCode, GroupCode, Active };
    //        SqlHelper helper = new SqlHelper();
    //        return helper.ExecuteNonQuery(myTransaction, "SYS_USER_Update", myParams, myValues);
    //    }

    //    public string UpdateNopass()
    //    {
    //        string[] strArray = new string[] { "@UserID", "@UserName", "@Password", "@Group_ID", "@Email", "@Description", "@PartID", "@Management", "@BranchCode", "@DepartmentCode", "@GroupCode", "@Active" };
    //        object[] objArray = new object[] { this.m_UserID, this.m_UserName, this.m_Password, this.m_Group_ID, this.m_Email, this.m_Description, this.m_PartID, this.m_Management, this.m_BranchCode, this.m_DepartmentCode, this.m_GroupCode, this.m_Active };
    //        string commandText = string.Concat(new object[] { 
    //        "UPDATE [SYS_USER] SET [UserName] = N'", this.m_UserName, "',[Group_ID] =N'", this.m_Group_ID, "',[Email]=N'", this.m_Email, "',[Description]=N'", this.m_Description, "' ,[PartID] = N'", this.m_PartID, "' ,[Management] = ", this.m_Management, " ,[BranchCode] = N'", this.m_BranchCode, "' ,[DepartmentCode] = N'", this.m_DepartmentCode, 
    //        "' ,[GroupCode] = N'", this.m_GroupCode, "',[Active] = ", this.m_Active ? 1 : 0, " Where [UserID] =N'", this.m_UserID, "'"
    //     });
    //        SqlHelper helper = new SqlHelper();
    //        helper.CommandType = CommandType.Text;
    //        return helper.ExecuteNonQuery(commandText);
    //    }

    //    // Properties
    //    public bool Active
    //    {
    //        get
    //        {
    //            return this.m_Active;
    //        }
    //        set
    //        {
    //            this.m_Active = value;
    //        }
    //    }

    //    public string BranchCode
    //    {
    //        get
    //        {
    //            return this.m_BranchCode;
    //        }
    //        set
    //        {
    //            this.m_BranchCode = value;
    //        }
    //    }

    //    public string DepartmentCode
    //    {
    //        get
    //        {
    //            return this.m_DepartmentCode;
    //        }
    //        set
    //        {
    //            this.m_DepartmentCode = value;
    //        }
    //    }

    //    public string Description
    //    {
    //        get
    //        {
    //            return this.m_Description;
    //        }
    //        set
    //        {
    //            this.m_Description = value;
    //        }
    //    }

    //    public string Email
    //    {
    //        get
    //        {
    //            return this.m_Email;
    //        }
    //        set
    //        {
    //            this.m_Email = value;
    //        }
    //    }

    //    public string Group_ID
    //    {
    //        get
    //        {
    //            return this.m_Group_ID;
    //        }
    //        set
    //        {
    //            this.m_Group_ID = value;
    //        }
    //    }

    //    public string GroupCode
    //    {
    //        get
    //        {
    //            return this.m_GroupCode;
    //        }
    //        set
    //        {
    //            this.m_GroupCode = value;
    //        }
    //    }

    //    public int Management
    //    {
    //        get
    //        {
    //            return this.m_Management;
    //        }
    //        set
    //        {
    //            this.m_Management = value;
    //        }
    //    }

    //    public string PartID
    //    {
    //        get
    //        {
    //            return this.m_PartID;
    //        }
    //        set
    //        {
    //            this.m_PartID = value;
    //        }
    //    }

    //    public string Password
    //    {
    //        get
    //        {
    //            return this.m_Password;
    //        }
    //        set
    //        {
    //            this.m_Password = value;
    //        }
    //    }

    //    public string ProductName
    //    {
    //        get
    //        {
    //            return "Class SYS_USER";
    //        }
    //    }

    //    public string ProductVersion
    //    {
    //        get
    //        {
    //            return "1.0.0.0";
    //        }
    //    }

    //    public string UserID
    //    {
    //        get
    //        {
    //            return this.m_UserID;
    //        }
    //        set
    //        {
    //            this.m_UserID = value;
    //        }
    //    }

    //    public string UserName
    //    {
    //        get
    //        {
    //            return this.m_UserName;
    //        }
    //        set
    //        {
    //            this.m_UserName = value;
    //        }
    //    }
    //}
}
