using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;

namespace Library
{
    public class NhiemVu_MucDoCls
    {
        public int MaMD;
        public byte STT;
        public string TenMD;

        public NhiemVu_MucDoCls()
        {
        }

        public NhiemVu_MucDoCls(int _MaMD)
        {
            SqlConnection sqlCon = new SqlConnection(Library.Properties.Settings.Default.Building_dbConnectionString);
            SqlCommand sqlCmd = new SqlCommand("NhiemVu_MucDo_get", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaMD", _MaMD);
            sqlCon.Open();
            SqlDataReader dread = sqlCmd.ExecuteReader();
            if (dread.Read())
            {
                MaMD = int.Parse(dread["MaMD"].ToString());
                STT = byte.Parse(dread["STT"].ToString());
                TenMD = dread["TenMD"] as string;
            }
            sqlCon.Close();
        }
        public void Detail()
        {
            SqlConnection sqlCon = new SqlConnection(Library.Properties.Settings.Default.Building_dbConnectionString);
            SqlCommand sqlCmd = new SqlCommand("NhiemVu_MucDo_get", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaMD", MaMD);
            sqlCon.Open();
            SqlDataReader dread = sqlCmd.ExecuteReader();
            if (dread.Read())
            {
                MaMD = int.Parse(dread["MaMD"].ToString());
                STT = byte.Parse(dread["STT"].ToString());
                TenMD = dread["TenMD"] as string;
            }
            sqlCon.Close();
        }
        public void Insert()
        {
            SqlConnection sqlCon = new SqlConnection(Library.Properties.Settings.Default.Building_dbConnectionString);
            SqlCommand sqlCmd = new SqlCommand("NhiemVu_MucDo_add", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@TenMD", TenMD);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public void Update()
        {
            SqlConnection sqlCon = new SqlConnection(Library.Properties.Settings.Default.Building_dbConnectionString);
            SqlCommand sqlCmd = new SqlCommand("NhiemVu_MucDo_update", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaMD", MaMD);
            sqlCmd.Parameters.AddWithValue("@TenMD", TenMD);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public DataTable Select()
        {
            SqlConnection sqlCon = new SqlConnection(Library.Properties.Settings.Default.Building_dbConnectionString);
            SqlDataAdapter sqlDA = new SqlDataAdapter("NhiemVu_MucDo_getAll", sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable SelectAll()
        {
            SqlConnection sqlCon = new SqlConnection(Library.Properties.Settings.Default.Building_dbConnectionString);
            SqlDataAdapter sqlDA = new SqlDataAdapter("NhiemVu_MucDo_getAll2", sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public void Delete()
        {
            SqlConnection sqlCon = new SqlConnection(Library.Properties.Settings.Default.Building_dbConnectionString);
            SqlCommand sqlCmd = new SqlCommand("NhiemVu_MucDo_delete", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaMD", MaMD);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }
        public int GetID()
        {

            SqlConnection sqlCon = new SqlConnection(Library.Properties.Settings.Default.Building_dbConnectionString);
            SqlCommand sqlCmd = new SqlCommand("NhiemVu_MucDo_getMaMD", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@TenMD", TenMD);
            sqlCmd.Parameters.Add("@MaMD", SqlDbType.Int).Direction = ParameterDirection.Output;
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
            return int.Parse(sqlCmd.Parameters["@MaMD"].Value.ToString());
        }
    }
}