using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Library;
using System.Text.RegularExpressions;

namespace Building.AppVime
{
    public static class CommonVime
    {
        public static string VersionCode = "1.0.1.5";

        public static string ConnectionString { get; set; }

        public static string ApiKey { get; set; }
        public static string SecretKey { get; set; }

        public static bool SaveConfig()
        {
            bool isNotExist = false;

            var db = new MasterDataContext();

            //create PROCEDURE [app.Config] @ApiKey nvarchar(50) output, @SecretKey nvarchar(300) output WITH encryption AS set @ApiKey=N'a'; set @SecretKey=N's'

            doo:
            string sqlString = string.Format("{0} PROCEDURE [app.Config] @ApiKey nvarchar(50) output, " +
                "@SecretKey nvarchar(300) output WITH encryption AS set @ApiKey=N'{1}'; set @SecretKey=N'{2}';", isNotExist ? "CREATE" : "ALTER", ApiKey, SecretKey);
            
            bool result = false;
            SqlConnection sqlConn = new SqlConnection(db.Connection.ConnectionString);
            SqlCommand sqlCmd = new SqlCommand(sqlString, sqlConn);
            sqlCmd.CommandType = CommandType.Text;

            try
            {
                sqlConn.Open();
                sqlCmd.ExecuteNonQuery();

                result = true;
            }
            catch(Exception ex) {
                if (ex.Message.Contains("app.Config"))
                {
                    isNotExist = true;
                    goto doo;
                }
            }
            finally
            {
                sqlConn.Close();
            }

            return result;
        }

        public static void GetConfig()
        {
            var db = new MasterDataContext();
            SqlConnection sqlConn = new SqlConnection(db.Connection.ConnectionString);
            SqlCommand sqlCmd = new SqlCommand("[app.Config]", sqlConn);

            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.Add("ApiKey", SqlDbType.NVarChar, 150).Direction = ParameterDirection.Output;
            sqlCmd.Parameters.Add("SecretKey", SqlDbType.NVarChar, 300).Direction = ParameterDirection.Output;

            try
            {
                sqlConn.Open();
                sqlCmd.ExecuteNonQuery();

                ApiKey = sqlCmd.Parameters["ApiKey"].Value.ToString();
                SecretKey = sqlCmd.Parameters["SecretKey"].Value.ToString();
            }
            catch { }
            finally
            {
                sqlConn.Close();
            }
        }

        public static string GetSecretKey()
        {
            string[] arrayKey = {"0", "1", "2", "3", "4", "5", "6", "7", "8", "9",
                "a", "b", "c", "d", "e", "f", "g", "h", "i", "j",
                "A", "B", "C", "D", "E", "F", "G", "H", "I", "J",
                "k", "l", "m", "n","o", "p", "q", "r", "s", "t",
                "K", "L", "M", "N","O", "P", "Q", "R", "S", "T",
                "a", "A", "B", "b", "c", "C", "d", "D", "e", "E",
                "0", "1", "2", "3", "4", "5", "6", "7", "8", "9",
                "A", "B", "C", "D", "E", "F", "G", "H", "I", "J",
                "k", "l", "m", "n","o", "p", "q", "r", "s", "t",
                "u", "v", "w", "x", "z","y", "U", "V", "W", "X", "Z","Y",
                "a", "b", "c", "d", "e", "f", "g", "h", "i", "j"};
            string result = "";
            int index;
            System.Random ojRandom = new System.Random();
            for (byte i = 0; i < 90; i++)
            {
                index = ojRandom.Next(0, 111);
                result += arrayKey[index];
            }

            return result;
        }

        public static string GetApiKey()
        {
            return Guid.NewGuid().ToString().ToLower();
        }

        public static string GetMimeType(string fileName)
        {
            string mimeType = "application/unknown";
            string ext = System.IO.Path.GetExtension(fileName).ToLower();
            Microsoft.Win32.RegistryKey regKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(ext);
            if (regKey != null && regKey.GetValue("Content Type") != null)
                mimeType = regKey.GetValue("Content Type").ToString();
            return mimeType;
        }

        public static byte TowerId { get; set; }

        public static string GetUID(string value)
        {
            string countryCode = "84";
            string phoneNumber = new Regex(@"\D").Replace(value, string.Empty);
            string uid = countryCode + (phoneNumber.StartsWith("0") ? phoneNumber.Substring(1) : phoneNumber);

            return uid;
        }

        public static string FormatPhone(string value)
        {
            string phoneNumber = new Regex(@"\D").Replace(value, string.Empty);

            return phoneNumber;
        }

        public static System.Drawing.Bitmap LoadImage(string imgUrl)
        {
            try
            {
                return new System.Drawing.Bitmap(new System.IO.MemoryStream(new System.Net.WebClient().DownloadData(imgUrl)));
            }
            catch
            {
                return null;
            }
        }
    }
}
