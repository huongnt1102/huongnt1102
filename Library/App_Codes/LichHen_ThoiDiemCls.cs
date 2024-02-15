using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;

namespace Library
{
    public class LichHen_ThoiDiemCls
    {
        public int MaTD;
        public byte STT;
        public string TenTD;

        public LichHen_ThoiDiemCls()
        {
        }

        public LichHen_ThoiDiemCls(int _MaTD)
        {
            SqlConnection sqlCon = new SqlConnection(Library.Properties.Settings.Default.Building_dbConnectionString);
            SqlCommand sqlCmd = new SqlCommand("LichHen_ThoiDiem_get", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaTD", _MaTD);
            sqlCon.Open();
            SqlDataReader dread = sqlCmd.ExecuteReader();
            if (dread.Read())
            {
                MaTD = int.Parse(dread["MaTD"].ToString());
                STT = byte.Parse(dread["STT"].ToString());
                TenTD = dread["TenTD"] as string;
            }
            sqlCon.Close();
        }
        public void Detail()
        {
            SqlConnection sqlCon = new SqlConnection(Library.Properties.Settings.Default.Building_dbConnectionString);
            SqlCommand sqlCmd = new SqlCommand("LichHen_ThoiDiem_get", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaTD", MaTD);
            sqlCon.Open();
            SqlDataReader dread = sqlCmd.ExecuteReader();
            if (dread.Read())
            {
                MaTD = int.Parse(dread["MaTD"].ToString());
                STT = byte.Parse(dread["STT"].ToString());
                TenTD = dread["TenTD"] as string;
            }
            sqlCon.Close();
        }
        public void Insert()
        {
            SqlConnection sqlCon = new SqlConnection(Library.Properties.Settings.Default.Building_dbConnectionString);
            SqlCommand sqlCmd = new SqlCommand("LichHen_ThoiDiem_add", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("MaTD", MaTD);
            sqlCmd.Parameters.AddWithValue("@STT", STT);
            sqlCmd.Parameters.AddWithValue("@TenTD", TenTD);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public void Update()
        {
            SqlConnection sqlCon = new SqlConnection(Library.Properties.Settings.Default.Building_dbConnectionString);
            SqlCommand sqlCmd = new SqlCommand("LichHen_ThoiDiem_update", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaTD", MaTD);
            sqlCmd.Parameters.AddWithValue("@STT", STT);
            sqlCmd.Parameters.AddWithValue("@TenTD", TenTD);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public DataTable Select()
        {
            SqlConnection sqlCon = new SqlConnection(Library.Properties.Settings.Default.Building_dbConnectionString);
            SqlDataAdapter sqlDA = new SqlDataAdapter("LichHen_ThoiDiem_getAll", sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable SelectAll()
        {
            SqlConnection sqlCon = new SqlConnection(Library.Properties.Settings.Default.Building_dbConnectionString);
            SqlDataAdapter sqlDA = new SqlDataAdapter("LichHen_ThoiDiem_getAll2", sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public void Delete()
        {
            SqlConnection sqlCon = new SqlConnection(Library.Properties.Settings.Default.Building_dbConnectionString);
            SqlCommand sqlCmd = new SqlCommand("LichHen_ThoiDiem_delete", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaTD", MaTD);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }
        public int GetID()
        {

            SqlConnection sqlCon = new SqlConnection(Library.Properties.Settings.Default.Building_dbConnectionString);
            SqlCommand sqlCmd = new SqlCommand("LichHen_ThoiDiem_getMaTD", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@TenTD", TenTD);
            sqlCmd.Parameters.Add("@MaTD", SqlDbType.Int).Direction = ParameterDirection.Output;
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
            return int.Parse(sqlCmd.Parameters["@MaTD"].Value.ToString());
        }
    }
}