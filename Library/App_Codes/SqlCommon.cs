using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Collections.Generic;
using System.Reflection;

namespace Library
{
    public class SqlCommon
    {
        //Test connect sql
        public static bool sqlTestConnect(string Conn)
        {
            SqlConnection sqlConn = new SqlConnection(Conn);
            try
            {
                sqlConn.Open();
                sqlConn.Close();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                sqlConn.Dispose();
            }
        }
        //Xu ly sql cmd ExecuteNonQuery
        public static void sqlExecuteNonQuery(SqlCommand sqlCmd)
        {
            sqlCmd.Connection = new SqlConnection(Properties.Settings.Default.Building_dbConnectionString);
            try
            {
                sqlCmd.Connection.Open();
                sqlCmd.ExecuteNonQuery();
                sqlCmd.Connection.Close();
            }
            catch
            {
                sqlCmd.Connection.Close();
            }
            finally
            {
                sqlCmd.Connection.Dispose();
            }
        }
        //Xu ly sql cmd string
        public static void sqlExecuteNonQueryPro(SqlCommand sqlCmd)
        {
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlExecuteNonQuery(sqlCmd);
        }
        //Xu ly sql cmd string
        public static void sqlExecuteNonQueryText(string sqlString)
        {
            using (SqlCommand sqlCmd = new SqlCommand(sqlString))
            {
                sqlCmd.CommandType = CommandType.Text;
                sqlExecuteNonQuery(sqlCmd);
            }
        }
        //Truy van SQL
        public static DataTable getData(string sqlString)
        {
            SqlConnection sqlConn = new SqlConnection(Properties.Settings.Default.Building_dbConnectionString);
            SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlString, sqlConn);
            try
            {
                using (DataTable tbl = new DataTable())
                {
                    sqlConn.Open();
                    sqlAdapter.Fill(tbl);
                    sqlConn.Close();
                    return tbl;
                }
            }
            catch
            {
                sqlConn.Close();
                return null;
            }
            finally
            {
                sqlConn.Dispose();
                sqlAdapter.Dispose();
            }
        }

        public static DataTable LINQToDataTable<T>(IEnumerable<T> varlist)
        {
             DataTable dtReturn = new DataTable();

             // column names 
             PropertyInfo[] oProps = null;

             if (varlist == null) return dtReturn;

             foreach (T rec in varlist)
             {
                  if (oProps == null)
                  {
                       oProps = ((Type)rec.GetType()).GetProperties();
                       foreach (PropertyInfo pi in oProps)
                       {
                            Type colType = pi.PropertyType;

                            if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                             {
                                 colType = colType.GetGenericArguments()[0];
                             }

                            dtReturn.Columns.Add(new DataColumn(pi.Name, colType));
                       }
                  }

                  DataRow dr = dtReturn.NewRow();

                  foreach (PropertyInfo pi in oProps)
                  {
                      dr[pi.Name] = pi.GetValue(rec, null) ?? DBNull.Value;
                  }

                  dtReturn.Rows.Add(dr);
             }
             return dtReturn;
        }
    }
}
