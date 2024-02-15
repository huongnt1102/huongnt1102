using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AuthorizationClass.Log
{
    public class ErrorLog
    {
        #region constructor
        public int ErrorLogID { get; set; }
        public string UserName { get; set; }
        public string Action { get; set; }
        public string Description { get; set; }
        public string Form { get; set; }
        public string Method { get; set; }
        public bool IsFixed { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public ErrorLog() { }
        public ErrorLog(int _errorID, string _username, string _action, string _description,
            string _form, string _method, bool _IsFixed, DateTime _createDate, DateTime _lastmodified)
        {
            this.ErrorLogID = _errorID;
            this.UserName = _username;
            this.Action = _action;
            this.Description = _description;
            this.Form = _form;
            this.Method = _method;
            this.IsFixed = _IsFixed;
            this.CreateDate = _createDate;
            this.LastModifiedDate = _lastmodified;
        }
        #endregion
    }
    public class ErrorLog_Access
    {
        /// <summary>
        /// Insert new record of new error log
        /// </summary>
        /// <param name="_username">UserName</param>
        /// <param name="_action">Action</param>
        /// <param name="_description">Description</param>
        /// <param name="_form">Form name</param>
        /// <param name="_method">Method name</param>
        public void InsertNewErrorLog(string _username, string _action, string _description,
            string _form, string _method)
        {
            Common.dbAccess db = new Common.dbAccess();
            try
            {
                db.CreateNewSqlCommand();
                db.AddParmetter("@UserName", _username);
                db.AddParmetter("@Action", _action);
                db.AddParmetter("@Description", _description);
                db.AddParmetter("@Form", _form);
                db.AddParmetter("@Method", _method);
                db.ExecuteNonQuery("spInsertErrorLog");  
            }
            catch
            {
            }
        }
        /// <summary>
        /// Update error log status
        /// </summary>
        /// <param name="ErrorLogID">Error Log ID</param>
        /// <param name="isFixed">Is fixed</param>
        /// <param name="LastModifiedDate">Modified Date</param>
        /// <returns>Bool true / false</returns>
        public bool UpdateErrorLog(int ErrorLogID, bool isFixed)
        {
            Common.dbAccess db = new Common.dbAccess();
            try
            {
                db.CreateNewSqlCommand();
                db.AddParmetter("@ErrorLogID", ErrorLogID);
                db.AddParmetter("@IsFixed", isFixed);
                db.AddParmetter("@LastModifiedDate", Common.CommonFunction.GetServerDateTime());
                db.ExecuteNonQuery("spUpdateErrorLog");  

                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// Delete an error log
        /// </summary>
        /// <param name="ErrorLogID">Error LogID</param>
        /// <returns>Bool true / false</returns>
        public bool DeleteAnErrorLog(int ErrorLogID)
        {
            Common.dbAccess db = new Common.dbAccess();
            try
            {
                db.CreateNewSqlCommand();
                db.AddParmetter("@ErrorLogID", ErrorLogID);
                db.ExecuteNonQuery("spDeleteAnErrorLog");  

                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// Reset / truncate errorlog
        /// </summary>
        /// <returns>bool true / false</returns>
        public bool ResetAllErrorLog()
        {
            Common.dbAccess db = new Common.dbAccess();
            try
            {
                db.CreateNewSqlCommand();
                db.ExecuteNonQuery("spResetAllErrorLog");  

                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// Get list of error log
        /// </summary>
        /// <returns>List</returns>
        public List<ErrorLog> GetErrorLogList()
        {
            List<ErrorLog> ListErrorLog = new List<ErrorLog>();
            Common.dbAccess db = new Common.dbAccess();
            db.CreateNewSqlCommand();

            System.Data.SqlClient.SqlDataReader reader = db.ExecuteSqlDataReader("spGetErrorLogList");  

            while (reader.Read())
            {
                ErrorLog errlog = new ErrorLog();
                errlog.ErrorLogID = (int)reader["ErrorLogID"];
                errlog.UserName = reader["UserName"].ToString();
                errlog.Action = reader["Action"].ToString();
                errlog.Description = reader["Description"].ToString();
                errlog.Form = reader["Form"].ToString();
                errlog.Method = reader["Method"].ToString();
                errlog.IsFixed = (bool)reader["IsFixed"];
                errlog.CreateDate = (DateTime)reader["CreateDate"];
                errlog.LastModifiedDate = (DateTime)reader["LastModifiedDate"];

                ListErrorLog.Add(errlog);
            }
            reader.Close();
            return ListErrorLog;
        }
        /// <summary>
        /// Get error log record by id
        /// </summary>
        /// <param name="ErrorLogID">ErrorLogID</param>
        /// <returns>ErrorLog record</returns>
        public ErrorLog GetAnErrorLogRecord(int ErrorLogID)
        {
            ErrorLog errlog = new ErrorLog();
            Common.dbAccess db = new Common.dbAccess();
            db.CreateNewSqlCommand();
            db.AddParmetter("@ErrorLogID", ErrorLogID);

            System.Data.SqlClient.SqlDataReader reader = db.ExecuteSqlDataReader("spGetErrorLogList");  

            if (reader.Read())
            {
                errlog.ErrorLogID = (int)reader["ErrorLogID"];
                errlog.UserName = reader["UserName"].ToString();
                errlog.Action = reader["Action"].ToString();
                errlog.Description = reader["Description"].ToString();
                errlog.Form = reader["Form"].ToString();
                errlog.Method = reader["Method"].ToString();
                errlog.IsFixed = (bool)reader["IsFixed"];
                errlog.CreateDate = (DateTime)reader["CreateDate"];
                errlog.LastModifiedDate = (DateTime)reader["LastModifiedDate"];
            }
            reader.Close();
            return errlog;
        }
    }
}
