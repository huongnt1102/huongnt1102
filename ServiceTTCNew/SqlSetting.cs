using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServiceTTC
{
    public class SqlSetting
    {
        public string Server { get; set; }
        public string UserID { get; set; }
        public string Database { get; set; }
        public string Conn { get; set; }
        public string SqlConn { get; set; }

        public SqlSetting()
        {
            this.Server = ServiceTTCNew.Properties.Settings.Default.SqlServer;
            this.UserID = ServiceTTCNew.Properties.Settings.Default.SqlUserID;
            this.Database = ServiceTTCNew.Properties.Settings.Default.SqlDatabase;
            this.Conn = ServiceTTCNew.Properties.Settings.Default.SqlConn;
        }

        public void Save()
        {
            ServiceTTCNew.Properties.Settings.Default.SqlServer = this.Server;
            ServiceTTCNew.Properties.Settings.Default.SqlUserID = this.UserID;
            ServiceTTCNew.Properties.Settings.Default.SqlDatabase = this.Database;
            ServiceTTCNew.Properties.Settings.Default.SqlConn = this.Conn;
            ServiceTTCNew.Properties.Settings.Default.Building_TTC_dbConnectionString = this.SqlConn;
            ServiceTTCNew.Properties.Settings.Default.Save();
        }
    }
}