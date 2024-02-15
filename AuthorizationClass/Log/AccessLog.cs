using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AuthorizationClass.Log
{
    public class AccessLog
    {
        #region Constructor
        private string _username;

        public string UserName
        {
            get { return _username; }
            set { _username = value; }
        }
        private string _action;

        public string Action
        {
            get { return _action; }
            set { _action = value; }
        }
        private string _description;

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }
        private int _LogId;

        public int LogID
        {
            get { return _LogId; }
            set { _LogId = value; }
        }
        private DateTime _create_date;

        public DateTime CreateDate
        {
            get { return _create_date; }
            set { _create_date = value; }
        }


        public AccessLog(string _uid, string _act, string _des)
        {
            this._action = _act;
            this._description = _des;
            this._username = _uid;
        }

        public AccessLog()
        { }

        #endregion
    }
    public class AccessLog_Access
    {
        /// <summary>
        /// Insert new record into log table
        /// </summary>
        /// <param name="_uid">Username</param>
        /// <param name="_act">Action</param>
        /// <param name="_des">Description</param>
        /// <returns></returns>
        public bool InsertNewAccessLogRecord(string _uid, string _act, string _des)
        {
            Common.dbAccess db = new Common.dbAccess();
            try
            {
                db.CreateNewSqlCommand();
                db.AddParmetter("@UserName", _uid);
                db.AddParmetter("@Action", _act);
                db.AddParmetter("@Description", _des);
                db.ExecuteNonQueryWithTransaction("spInsertLog");  

                db.CommitTransaction();
                return true;
            }
            catch
            {
                db.RollbackTransaction();
                return false;
            }
        }

        /// <summary>
        /// Get list of access log
        /// </summary>
        /// <returns>List</returns>
        public List<AccessLog> GetListAccessLog()
        {
            List<AccessLog> logList = new List<AccessLog>();
            Common.dbAccess db = new Common.dbAccess();
            db.CreateNewSqlCommand();

            System.Data.SqlClient.SqlDataReader reader = db.ExecuteSqlDataReader("spGetAllAccessLog");  

            while (reader.Read())
            {
                AccessLog log = new AccessLog();
                log.LogID = (int)reader["LogID"];
                log.UserName = reader["UserName"].ToString();
                log.Action = reader["Action"].ToString();
                log.Description = reader["Description"].ToString();
                log.CreateDate = (DateTime)reader["Create_date"];

                logList.Add(log);
            }
            reader.Close();
            return logList;
        }

        /// <summary>
        /// Delete An access log record
        /// </summary>
        /// <param name="LogID">Access Log ID</param>
        /// <returns></returns>
        public bool DeleteAnAccessLogRecord(int LogID)
        {
            Common.dbAccess db = new Common.dbAccess();
            try
            {
                db.CreateNewSqlCommand();
                db.AddParmetter("@LogID", LogID);
                db.ExecuteNonQuery("spDeleteAnAccessLog");  

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Reset/Truncate all access log
        /// </summary>
        /// <returns>bool</returns>
        public bool ResetAllAccessLog()
        {
            Common.dbAccess db = new Common.dbAccess();
            try
            {
                db.CreateNewSqlCommand();
                db.ExecuteNonQuery("spResetAllAccessLog");  

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
