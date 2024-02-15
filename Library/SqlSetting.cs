using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library
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
            this.Server = Properties.Settings.Default.SqlServer;
            this.UserID = Properties.Settings.Default.SqlUserID;
            this.Database = Properties.Settings.Default.SqlDatabase;
            this.Conn = Properties.Settings.Default.SqlConn;
        }

        public void Save()
        {
            Properties.Settings.Default.SqlServer = this.Server;
            Properties.Settings.Default.SqlUserID = this.UserID;
            Properties.Settings.Default.SqlDatabase = this.Database;
            Properties.Settings.Default.SqlConn = this.Conn;
            Properties.Settings.Default.Building_dbConnectionString = this.SqlConn;
            Properties.Settings.Default.Save();
        }
    }
}