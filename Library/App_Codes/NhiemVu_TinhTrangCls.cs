using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;

namespace Library
{
    public class NhiemVu_TinhTrangCls
    {
        public int MaTT;
        public int Mau;
        public string TenTT;
        public byte STT;

        public NhiemVu_TinhTrangCls()
        {
        }

        public NhiemVu_TinhTrangCls(int _MaTT)
        {
            SqlConnection sqlCon = new SqlConnection(Library.Properties.Settings.Default.Building_dbConnectionString);
            SqlCommand sqlCmd = new SqlCommand("NhiemVu_TinhTrang_get", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaTT", _MaTT);
            sqlCon.Open();
            SqlDataReader dread = sqlCmd.ExecuteReader();
            if (dread.Read())
            {
                MaTT = int.Parse(dread["MaTT"].ToString());
                //Mau = int.Parse(dread["Mau"].ToString());
                TenTT = dread["TenTT"] as string;
                STT = byte.Parse(dread["STT"].ToString());
            }
            sqlCon.Close();
        }
        public void Detail()
        {
            SqlConnection sqlCon = new SqlConnection(Library.Properties.Settings.Default.Building_dbConnectionString);
            SqlCommand sqlCmd = new SqlCommand("NhiemVu_TinhTrang_get", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaTT", MaTT);
            sqlCon.Open();
            SqlDataReader dread = sqlCmd.ExecuteReader();
            if (dread.Read())
            {
                MaTT = int.Parse(dread["MaTT"].ToString());
                TenTT = dread["TenTT"] as string;
                STT = byte.Parse(dread["STT"].ToString());
            }
            sqlCon.Close();
        }

        public void Insert()
        {
            SqlConnection sqlCon = new SqlConnection(Library.Properties.Settings.Default.Building_dbConnectionString);
            SqlCommand sqlCmd = new SqlCommand("NhiemVu_TinhTrang_add", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@TenTT", TenTT);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public void Update()
        {
            SqlConnection sqlCon = new SqlConnection(Library.Properties.Settings.Default.Building_dbConnectionString);
            SqlCommand sqlCmd = new SqlCommand("NhiemVu_TinhTrang_update", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaTT", MaTT);
            sqlCmd.Parameters.AddWithValue("@TenTT", TenTT);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public DataTable Select()
        {
            SqlConnection sqlCon = new SqlConnection(Library.Properties.Settings.Default.Building_dbConnectionString);
            SqlDataAdapter sqlDA = new SqlDataAdapter("NhiemVu_TinhTrang_getAll", sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable SelectAll()
        {
            SqlConnection sqlCon = new SqlConnection(Library.Properties.Settings.Default.Building_dbConnectionString);
            SqlDataAdapter sqlDA = new SqlDataAdapter("NhiemVu_TinhTrang_getAll2", sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public void Delete()
        {
            SqlConnection sqlCon = new SqlConnection(Library.Properties.Settings.Default.Building_dbConnectionString);
            SqlCommand sqlCmd = new SqlCommand("NhiemVu_TinhTrang_delete", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaTT", MaTT);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public int GetID()
        {

            SqlConnection sqlCon = new SqlConnection(Library.Properties.Settings.Default.Building_dbConnectionString);
            SqlCommand sqlCmd = new SqlCommand("NhiemVu_TinhTrang_getMaTT", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@TenTT", TenTT);
            sqlCmd.Parameters.Add("@MaTT", SqlDbType.Int).Direction = ParameterDirection.Output;
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
            return int.Parse(sqlCmd.Parameters["@MaTT"].Value.ToString());
        }
    }
}