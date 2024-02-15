using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace AuthorizationClass.Common
{
    public class dbConnection
    {
        public static string _connstr = string.Empty;

        private static SqlConnection sqlconn = new SqlConnection();

        public static SqlConnection SqlConnection
        {
            get { return sqlconn; }
            set { sqlconn = value; }
        }

        public static void OpenConnection()
        {
            sqlconn.ConnectionString = _connstr;
            sqlconn.Open();
        }

        public static void CloseConnection()
        {
            sqlconn.Close();
        }
    }
    public class dbAccess
    {
        SqlTransaction _sqltran;
        SqlCommand _cmd;

        #region Open - Close Connection section
        /// <summary>
        /// Open Connection
        /// </summary>
        public void OpenConnection()
        {
            dbConnection.OpenConnection();
        }
        /// <summary>
        /// Close Connection
        /// </summary>
        public void CloseConnection()
        {
            dbConnection.CloseConnection();
        }
        #endregion

        #region Transaction Section
        /// <summary>
        /// Begin Transaction
        /// </summary>
        public void BeginTransaction()
        {
            dbConnection.OpenConnection();
            _sqltran = dbConnection.SqlConnection.BeginTransaction();
        }
        /// <summary>
        /// Commit Transaction
        /// </summary>
        public void CommitTransaction()
        {
            _sqltran.Commit();
            dbConnection.CloseConnection();
        }
        /// <summary>
        /// Rollback Transaction
        /// </summary>
        public void RollbackTransaction()
        {
            _sqltran.Rollback();
            dbConnection.CloseConnection();
        }
        #endregion

        #region "Create and Add parameter for SqlCommand"
        /// <summary>
        /// Create new sql command
        /// </summary>
        public void CreateNewSqlCommand()
        {
            _cmd = new SqlCommand();
            _cmd.CommandType = CommandType.StoredProcedure;
            _cmd.Connection = dbConnection.SqlConnection;
        }
        /// <summary>
        /// Addd parametter for calling store procedures
        /// </summary>
        /// <param name="paraName">Name of parametter</param>
        /// <param name="value">Value of parametter</param>
        public void AddParmetter(string paraName, object value)
        {
            SqlParameter param = new SqlParameter();
            param.ParameterName = paraName;
            param.Value = value;
            _cmd.Parameters.Add(param);
        }

        public void AddParametterWithOutputValue(string paraName)
        {
            SqlParameter param = new SqlParameter();
            param.ParameterName = paraName;
            param.Value = 1;
            param.Direction = ParameterDirection.Output;
            _cmd.Parameters.Add(param);
        }
        public object GetValueFromOutputParametter(string paraName)
        {
            return _cmd.Parameters[paraName].Value;
        }
        #endregion

        #region Execute sql command
        /// <summary>
        /// ExecuteNonQuery
        /// </summary>
        /// <param name="StoreProcedureName">Name of StoreProcedure</param>
        /// <returns>int type value</returns>
        public int ExecuteNonQuery(string StoreProcedureName)
        {
            try
            {
                _cmd.CommandText = StoreProcedureName;
                this.OpenConnection();
                return _cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.CloseConnection();
            }
        }
        /// <summary>
        /// ExecuteNonQuery With Transaction
        /// </summary>
        /// <param name="StoreProcedureName">Name of StoreProcedure</param>
        /// <returns>True/False</returns>
        public bool ExecuteNonQueryWithTransaction(string StoreProcedureName)
        {
            try
            {
                _cmd.CommandText = StoreProcedureName;
                _cmd.Transaction = _sqltran;
                _cmd.ExecuteNonQuery();

                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// ExecuteScalar
        /// </summary>
        /// <param name="StoreProcedureName">StoreProcude Name</param>
        /// <returns>Object type value</returns>
        public object ExecuteScalar(string StoreProcedureName)
        {
            try
            {
                _cmd.CommandText = StoreProcedureName;
                this.OpenConnection();
                return _cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.CloseConnection();
            }
        }
        /// <summary>
        /// Execute DataReader
        /// </summary>
        /// <param name="StoreProcdureName">StoreProcedure name</param>
        /// <returns>SqlDataReader</returns>
        public SqlDataReader ExecuteSqlDataReader(string StoreProcdureName)
        {
            _cmd.CommandText = StoreProcdureName;
            this.OpenConnection();
            return _cmd.ExecuteReader(CommandBehavior.CloseConnection);
        }
        /// <summary>
        /// ExecuteSqlDataReaderWithoutCloseConnection
        /// </summary>
        /// <param name="StoreProcdureName">StoreProcedure name</param>
        /// <returns>SqlDataReader</returns>
        public SqlDataReader ExecuteSqlDataReaderWithoutCloseConnection(string StoreProcdureName)
        {
            _cmd.CommandText = StoreProcdureName;
            this.OpenConnection();
            return _cmd.ExecuteReader();
        }
        /// <summary>
        /// ExecuteDataTable
        /// </summary>
        /// <param name="StoreProcdureName">StoreProcedure name</param>
        /// <returns>DataTable</returns>
        public DataTable ExecuteDataTable(string StoreProcdureName)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter();
                _cmd.CommandText = StoreProcdureName;
                da.SelectCommand = _cmd;
                da.Fill(dt);       
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dt;
        }
        #endregion
    }
}
