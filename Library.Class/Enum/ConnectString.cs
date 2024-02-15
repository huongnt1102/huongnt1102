using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Library.Class.Enum
{
    public class ConnectString
    {
        /// <summary>
        /// Chuỗi connect string của Roots
        /// </summary>
        public static string CONNECT_MYHOME = "Data Source=45.119.81.69; Initial Catalog= BUILDING_APP_DB;User ID= Building_App_Login;Password=NhaTrang-1@#45";

        public static string CONNECT_STRING = GetConnectString();
        // Tạo chuỗi connect string mặc định thêm vào interre = true

        public static string GetConnectString()
        {
            SqlConnectionStringBuilder connectionStringBuilder = new SqlConnectionStringBuilder();
            connectionStringBuilder.ConnectionString = Library.Properties.Settings.Default.Building_dbConnectionString;
            connectionStringBuilder.PersistSecurityInfo = true;
            return connectionStringBuilder.ToString();
        }
    }
}
