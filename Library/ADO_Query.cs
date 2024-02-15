using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

public class ADO_Query
{
    SqlConnection con = null;
    
    #region Constructor
    public ADO_Query()
    {
        con = connect();
    }
    #endregion

    #region Connect
    public SqlConnection connect()
    {
        //var connection = System.Configuration.ConfigurationManager.ConnectionStrings["crmFANSIPAN_dbConnectionString"].ConnectionString;
        //var connection = DIPCRM.DataEntity.Properties.Settings.Default.crmNguyenDinh_dbConnectionString;
        var connection = Library.Properties.Settings.Default.Building_dbConnectionString;
        //ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings["DIPCRM.DataEntity.Properties.Settings.crmFANSIPAN_dbConnectionString"];
        //ConnectionStringSettings settings2 = ConfigurationManager.ConnectionStrings["crmFANSIPAN_dbConnectionString"];
        /*SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
        /*string connection = settings.ConnectionString;
        builder = new SqlConnectionStringBuilder(connection);#1#
        builder.DataSource = @"112.213.84.35";
        builder.InitialCatalog = "crmFANSIPAN_db";
        builder.UserID = "crmFansipan";
        builder.Password = "dip2@15fg";*/
        return new SqlConnection(connection);
    }
    #endregion

    #region Open connection
    public bool tryOpen()
    {
        try
        {
            con.Open();

            return true;
        }
        catch (SqlException sqlEx)
        {
            return false;

            throw sqlEx;
        }
    }
    #endregion

    #region Close connection
    public bool tryClose()
    {
        try
        {
            con.Close();
            return true;
        }
        catch (SqlException sqlEx)
        {
            return false;

            throw sqlEx;
        }
    }
    #endregion

    #region Get data to datatable
    public DataTable getDataTable(string sql, CommandType commandType, params object[] pars)
    {
        DataSet dst = new DataSet();
        try
        {
            if (tryOpen())
            {
                SqlCommand com = new SqlCommand(sql, con);
                com.CommandType = commandType;

                for (int i = 0; i < pars.Length; i += 2)
                {
                    SqlParameter par = new SqlParameter(pars[i].ToString(), pars[i + 1]);
                    com.Parameters.Add(par);
                }

                SqlDataAdapter dad = new SqlDataAdapter(com);
                dad.Fill(dst);
                tryClose();
            }
            else
            {
                return null;
            }
        }
        catch (Exception sqlEx)
        {
            tryClose();
        }
        return dst.Tables[0];
    }
    #endregion

    #region ExecuteNonQuery
    public bool executeNonQuery(string sql, CommandType commandType, params object[] pars)
    {
        bool ret = false;

        try
        {
            if (tryOpen())
            {
                SqlCommand com = new SqlCommand(sql, con);
                com.CommandType = commandType;
                #region
                for (int i = 0; i < pars.Length; i += 2)
                {
                    //if (pars[i + 1].ToString() == int.MinValue.ToString())
                    //{
                        //SqlParameter par = new SqlParameter(pars[i].ToString(), DBNull.Value);
                        //com.Parameters.Add(par);
                    //}
                    //else if (pars[i + 1].ToString() == string.Empty)
                    //{
                    //    SqlParameter par = new SqlParameter(pars[i].ToString(), DBNull.Value);
                    //    com.Parameters.Add(par);
                    //}
                    //else if (pars[i + 1].ToString() == DateTime.MinValue.ToString())
                    //{
                    //    SqlParameter par = new SqlParameter(pars[i].ToString(), DBNull.Value);
                    //    com.Parameters.Add(par);
                    //}
                    //else
                    //{
                        SqlParameter par = new SqlParameter(pars[i].ToString(), pars[i + 1]);
                        com.Parameters.Add(par);
                    //}
                }
                #endregion
                int n = com.ExecuteNonQuery();

                if (n > 0)
                {
                    ret = true;
                }

                tryClose();
            }
            else
            {
                ret = false;
            }
        }
        catch (Exception sqlEx)
        {
            tryClose();
        }

        return ret;
    }
    #endregion

    #region ExecuteNonQuery with Output params
    public bool executeNonQueryHaveOutput(string sql, CommandType commandType, params object[] pars)
    {
        bool ret = false;

        try
        {
            if (tryOpen())
            {
                SqlCommand com = new SqlCommand(sql, con);
                com.CommandType = commandType;

                for (int i = 0; i < pars.Length; i += 2)
                {
                    if (pars[i + 1].ToString() == int.MinValue.ToString())
                    {
                        SqlParameter par = new SqlParameter(pars[i].ToString(), DBNull.Value);
                        com.Parameters.Add(par);
                    }
                    else if (pars[i + 1].ToString() == string.Empty)
                    {
                        SqlParameter par = new SqlParameter(pars[i].ToString(), DBNull.Value);
                        com.Parameters.Add(par);
                    }
                    else if (pars[i + 1].ToString() == DateTime.MinValue.ToString())
                    {
                        SqlParameter par = new SqlParameter(pars[i].ToString(), DBNull.Value);
                        com.Parameters.Add(par);
                    }
                    else
                    {
                        SqlParameter par = new SqlParameter(pars[i].ToString(), pars[i + 1]);
                        com.Parameters.Add(par);
                    }
                }

                //Output Param (return value)
                SqlParameter retParam = com.Parameters.Add("@ReturnValue", SqlDbType.Int);
                retParam.Direction = ParameterDirection.Output;

                //Execute non query
                com.ExecuteNonQuery();

                //Get return value
                ret = Convert.ToBoolean(com.Parameters["@ReturnValue"].Value);

                tryClose();
            }
            else
            {
                ret = false;
            }
        }
        catch (Exception sqlEx)
        {
            tryClose();
        }

        return ret;
    }
    #endregion

}
