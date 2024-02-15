using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AuthorizationClass.Login
{
    public class clsUser
    {
        public string LoginID { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public DateTime CreateDate { get; set; }
        public bool LockedUser { get; set; }
        public DateTime LockedDate { get; set; }
        public string LockedReason { get; set; }
        public DateTime LastLogin { get; set; }
        public DateTime LastChangePassword { get; set; }
        public DateTime DeadlineOfUsing { get; set; }
        public clsUser() { }
    }
    public class UserAccess
    {
        public UserAccess() { }
        /// <summary>
        /// Get user by login id
        /// </summary>
        /// <param name="sLoginID">LoginID</param>
        /// <returns>User object</returns>
        public clsUser GetUserByLoginID(string sLoginID)
        {
            Common.dbAccess db = new Common.dbAccess();
            clsUser user = new clsUser();
            db.CreateNewSqlCommand();
            db.AddParmetter("@LoginID", sLoginID);
            System.Data.SqlClient.SqlDataReader reader = db.ExecuteSqlDataReader("spGetUserByID");  
            if (reader.Read())
            {
                user.LoginID = reader["LoginID"].ToString();
                user.FullName = reader["FullName"].ToString();
                user.Password = reader["Password"].ToString();
                user.Email = reader["Email"].ToString();
                user.CreateDate = (DateTime)reader["CreateDate"];
                user.LockedUser = (bool)reader["LockedUser"];
                user.LockedDate = (DateTime)reader["LockedDate"];
                user.LockedReason = reader["LockedReason"].ToString();
                user.LastLogin = (DateTime)reader["LastLogin"];
                user.LastChangePassword = (DateTime)reader["LastChangePassword"];
                user.DeadlineOfUsing = (DateTime)reader["DeadlineOfUsing"];
            }
            reader.Close();

            return user;
        }
        public List<clsUser> ListUser()
        {
            List<clsUser> UserCollection = new List<clsUser>();
            Common.dbAccess db = new Common.dbAccess();
            db.CreateNewSqlCommand();
            System.Data.SqlClient.SqlDataReader reader = db.ExecuteSqlDataReader("spGetAllUser");   

            while (reader.Read())
            {
                clsUser usr = new clsUser();
                usr.LoginID = reader["LoginID"].ToString();
                usr.Password = reader["Password"].ToString();
                usr.FullName = reader["FullName"].ToString();
                usr.Email = reader["Email"].ToString();
                usr.CreateDate = (DateTime)reader["CreateDate"];
                usr.LockedUser = (bool)reader["LockedUser"];
                usr.LockedReason = reader["LockedReason"].ToString();
                usr.LastLogin = (DateTime)reader["LastLogin"];
                //usr.LastChangePassword = reader["LastChangePassword"];
                //usr.DeadlineOfUsing = (DateTime)reader["DeadlineOfUsing"];

                UserCollection.Add(usr);

            }
            reader.Close();
            return UserCollection;
        }
        
    }
}
