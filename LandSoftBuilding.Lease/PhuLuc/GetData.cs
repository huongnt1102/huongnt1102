using Library;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;

namespace LandSoftBuilding.Lease
{
    public static class GetData
    {
        public static void InsertUpdate(object objGet, object objSet)
        {
           string TableName = objGet.GetType().Name;

            string PrimaryKey = GetPrimaryKey(TableName);
            foreach (PropertyInfo propGet in objGet.GetType().GetProperties())
            {
                PropertyInfo propSet = objSet.GetType().GetProperty(propGet.Name);
                try
                {
                    //if ((propGet.Name == PrimaryKey & propSet.GetValue(objSet, null) == null)) //propGet.Name != PrimaryKey |
                    //{
                    if (propGet.Name ==  propSet.Name)
                    {
                        propSet.SetValue(objSet, propGet.GetValue(objGet, null), null);

                    }
                    //}
                }
                catch (Exception ex)
                {      
                }
            }
        }

        private static string GetPrimaryKey(string tableName)
        {
            using (var db = new MasterDataContext())
            {
                string sql = "SELECT column_name "
                             + " FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE  "
                             + " WHERE OBJECTPROPERTY(OBJECT_ID(constraint_name), 'IsPrimaryKey') = 1"
                             + " AND table_name = '"+ tableName  +"'";

                SqlConnection conn2 = new SqlConnection(db.Connection.ConnectionString);
                SqlCommand cmd_server2 = new SqlCommand(sql);
                cmd_server2.CommandType = CommandType.Text;
                cmd_server2.Connection = conn2;
                conn2.Open();
                string ColumnName = (string)cmd_server2.ExecuteScalar();
                conn2.Close();
                return ColumnName;
            }
        }
    }
}