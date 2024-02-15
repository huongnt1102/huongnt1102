using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace AuthorizationClass.Login
{
    public class UserGroup
    {
        public string LoginID { get; set; }
        public int GroupID { get; set; }
    }
    public class Login_Register
    {
        public enum UserStatus 
        {
            Administrator = 100,
            OK = 0, 
            Locked = -1, 
            ExpiredDate = -2, 
            NotExists = -3
        }

        /// <summary>
        /// Add new user and add this user to group
        /// </summary>
        /// <param name="objuser">Object User</param>
        /// <param name="lstUsrGroup">Object UserGroup</param>
        /// <returns>Bool</returns>
        public bool AddNewUser(clsUser objuser,List<UserGroup> lstUsrGroup)
        {
            Common.dbAccess db = new Common.dbAccess();
            //check
            db.CreateNewSqlCommand();
            db.AddParmetter("@LoginID", objuser.LoginID);
            System.Data.SqlClient.SqlDataReader reader = db.ExecuteSqlDataReader("spCheckUserAlrealyExists");  
            if (reader.Read())
                return false;

            db.BeginTransaction();
            try
            {
                //1. Insert new user
                db.CreateNewSqlCommand();
                db.AddParmetter("@LoginID", objuser.LoginID);
                db.AddParmetter("@Password",objuser.Password);
                db.AddParmetter("@FullName", objuser.FullName);
                db.AddParmetter("@Email", objuser.Email);
                db.AddParmetter("@LockedUser", objuser.LockedUser);
                db.AddParmetter("@LockeDate", objuser.LockedDate);
                db.AddParmetter("@LockedReason", objuser.LockedReason);
                db.AddParmetter("@DeadlineOfUsing", objuser.DeadlineOfUsing);

                db.ExecuteNonQueryWithTransaction("spAddNewUser");  

                //2. Insert records into GroupUser table
                for (int i = 0; i < lstUsrGroup.Count; i++)
                {
                    db.CreateNewSqlCommand();
                    db.AddParmetter("@GroupID", lstUsrGroup[i].GroupID);
                    db.AddParmetter("@LoginID", lstUsrGroup[i].LoginID);
                    db.ExecuteNonQueryWithTransaction("spGroupUser_Insert");  
                }

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
        /// Check users is admin or not
        /// </summary>
        /// <param name="sUsername">UserName</param>
        /// <returns>Bool</returns>
        public bool IsAdminUser(string sUsername)
        {
            Group_access gp = new Group_access();
            List<clsGroup> lstgroup = gp.GetGroupCollectionByUserName(sUsername);
            foreach (clsGroup item in lstgroup)
            {
                if (item.IsAdminGroup)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Check login
        /// </summary>
        /// <param name="sUserName"></param>
        /// <param name="sPassword"></param>
        /// <param name="userstatus"></param>
        /// <returns></returns>
        public clsUser CheckUser(string sUserName, string sPassword,ref UserStatus userstatus)
        {
            clsUser user = new clsUser();
            UserAccess uac = new UserAccess();
            user = uac.GetUserByLoginID(sUserName);
            DateTime dtnow = Common.CommonFunction.GetServerDateTime();

            if (user != null)
            {
                if (user.Password == Common.CommonFunction.HashPassword(sPassword))
                {
                    if (IsAdminUser(sUserName))
                        userstatus = UserStatus.Administrator;
                    else if (!user.LockedUser)
                    {
                        if (user.DeadlineOfUsing >= dtnow)
                            userstatus = UserStatus.OK;
                        else
                            userstatus = UserStatus.ExpiredDate;
                    }
                    else
                        userstatus = UserStatus.Locked;
                }
                else
                    userstatus = UserStatus.NotExists;
            }
            else
                userstatus = UserStatus.NotExists;

            return user;
        }

        /// <summary>
        /// Update password
        /// </summary>
        /// <param name="usr">Object user</param>
        /// <returns>Bool</returns>
        public bool UpdatePassword(clsUser usr)
        {
            try
            {
                Common.dbAccess db = new Common.dbAccess();
                db.CreateNewSqlCommand();

                db.AddParmetter("@LoginID", usr.LoginID);
                db.AddParmetter("@Password", Common.CommonFunction.HashPassword(usr.Password));
                db.AddParmetter("@LastChangedPassword", Common.CommonFunction.GetServerDateTime());

                db.ExecuteNonQuery("spUpdatePassword");  

                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// Kiểm tra mật khẩu hiện tại có đúng hay không
        /// </summary>
        /// <param name="sUserName"></param>
        /// <param name="sPassword"></param>
        /// <returns></returns>
        public bool CheckPassword(string sUserName,string sPassword)
        {
            clsUser user = new UserAccess().GetUserByLoginID(sUserName);
            if (user == null)
                return false;
            if (Common.CommonFunction.HashPassword(sPassword) == user.Password)
                return true;
            else 
                return false;
        }
    }
}
