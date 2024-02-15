using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AuthorizationClass.Login
{
    public class clsGroup
    {
        public int GroupID { get; set; }
        public string GroupName { get; set; }
        public string GroupNote { get; set; }
        public bool IsAdminGroup { get; set; }

        public clsGroup() { }

        public clsGroup(string sGroupName, string sGroupNote, bool sIsAdminGroup)
        {
            this.GroupName = sGroupName;
            this.GroupNote = sGroupNote;
            this.IsAdminGroup = sIsAdminGroup;
        }
    }
    public class Group_access
    {
        /// <summary>
        /// Get all clsGroup into list
        /// </summary>
        /// <returns>List clsGroup</returns>
        public List<clsGroup> GetGroupCollection()
        {
            List<clsGroup> lstGroup = new List<clsGroup>();
            Common.dbAccess db = new Common.dbAccess();
            db.CreateNewSqlCommand();
            System.Data.SqlClient.SqlDataReader reader = db.ExecuteSqlDataReader("spGetAllGroup");  

            while (reader.Read())
            {
                clsGroup gp = new clsGroup();
                gp.GroupID = (int)reader["GroupID"];
                gp.GroupName = reader["GroupName"].ToString();
                gp.GroupNote = reader["Note"].ToString();
                gp.IsAdminGroup = (bool)reader["IsAdmin"];

                lstGroup.Add(gp);

            }
            reader.Close();
            return lstGroup;
        }

        /// <summary>
        /// Get clsGroup list by username
        /// </summary>
        /// <param name="sUserName">UserName</param>
        /// <returns>List clsGroup</returns>
        public List<clsGroup> GetGroupCollectionByUserName(string sUserName)
        {
            List<clsGroup> lstGroup = new List<clsGroup>();
            Common.dbAccess db = new Common.dbAccess();
            db.CreateNewSqlCommand();
            db.AddParmetter("@LoginID", sUserName);
            System.Data.SqlClient.SqlDataReader reader = db.ExecuteSqlDataReader("spGetAllGroupByUserName");  

            while (reader.Read())
            {
                clsGroup gp = new clsGroup();
                gp.GroupID = (int)reader["GroupID"];
                gp.GroupName = reader["GroupName"].ToString();
                gp.GroupNote = reader["Note"].ToString();
                gp.IsAdminGroup = (bool)reader["IsAdmin"];

                lstGroup.Add(gp);

            }
            reader.Close();
            return lstGroup;
        }

        /// <summary>
        /// Thêm một nhóm mới
        /// </summary>
        /// <param name="group"></param>
        /// <returns>Bool</returns>
        public bool AddNewGroup(clsGroup group)
        {
            try
            {
                Common.dbAccess db = new Common.dbAccess();
                db.AddParmetter("@GroupName", group.GroupName);
                db.AddParmetter("@Note", group.GroupNote);
                db.AddParmetter("@IsAdmin", false);

                db.ExecuteNonQuery("spAddNewGroup");  
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Xóa một nhóm người dùng
        /// </summary>
        /// <param name="GroupID"></param>
        /// <returns></returns>
        public bool DeleteAGroup(int GroupID)
        {
            try
            {
                Common.dbAccess db = new Common.dbAccess();
                db.AddParmetter("@GroupID", GroupID);

                db.ExecuteNonQuery("spDeleteGroup");  
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateGroup(int GroupID, string GroupName, string Note, bool IsAdmin = false)
        {
            try
            {
                Common.dbAccess db = new Common.dbAccess();
                db.CreateNewSqlCommand();
                db.AddParmetter("@GroupID", GroupID);
                db.AddParmetter("@GroupName", GroupName);
                db.AddParmetter("@Note", Note);
                db.AddParmetter("@IsAdmin", IsAdmin);
                db.ExecuteNonQuery("spUpdateGroup");   
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
