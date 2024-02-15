using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;

namespace Library
{
    public class NhiemVu_LoaiCls
    {
        public int MaLNV;
        public byte STT;
        public string TenLNV;

        public NhiemVu_LoaiCls()
        {
        }

        public NhiemVu_LoaiCls(byte _MaLNV)
        {
            SqlConnection sqlCon = new SqlConnection(Library.Properties.Settings.Default.Building_dbConnectionString);
            SqlCommand sqlCmd = new SqlCommand("NhiemVu_Loai_get", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaLNV", _MaLNV);
            sqlCon.Open();
            SqlDataReader dread = sqlCmd.ExecuteReader();
            if (dread.Read())
            {
                MaLNV = int.Parse(dread["MaLNV"].ToString());
                STT = byte.Parse(dread["STT"].ToString());
                TenLNV = dread["TenLNV"] as string;
            }
            sqlCon.Close();
        }
        public void Detail()
        {
            SqlConnection sqlCon = new SqlConnection(Library.Properties.Settings.Default.Building_dbConnectionString);
            SqlCommand sqlCmd = new SqlCommand("NhiemVu_Loai_get", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaLNV", MaLNV);
            sqlCon.Open();
            SqlDataReader dread = sqlCmd.ExecuteReader();
            if (dread.Read())
            {
                MaLNV = byte.Parse(dread["MaLNV"].ToString());
                STT = byte.Parse(dread["STT"].ToString());
                TenLNV = dread["TenLNV"] as string;
            }
            sqlCon.Close();
        }
        public void Insert()
        {
            SqlConnection sqlCon = new SqlConnection(Library.Properties.Settings.Default.Building_dbConnectionString);
            SqlCommand sqlCmd = new SqlCommand("NhiemVu_Loai_add", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@STT", STT);
            sqlCmd.Parameters.AddWithValue("@TenLNV", TenLNV);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public void Update()
        {
            SqlConnection sqlCon = new SqlConnection(Library.Properties.Settings.Default.Building_dbConnectionString);
            SqlCommand sqlCmd = new SqlCommand("NhiemVu_Loai_update", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaLNV", MaLNV);
            sqlCmd.Parameters.AddWithValue("@STT", STT);
            sqlCmd.Parameters.AddWithValue("@TenLNV", TenLNV);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public DataTable Select()
        {
            SqlConnection sqlCon = new SqlConnection(Library.Properties.Settings.Default.Building_dbConnectionString);
            SqlDataAdapter sqlDA = new SqlDataAdapter("NhiemVu_Loai_getAll", sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable SelectAll()
        {
            SqlConnection sqlCon = new SqlConnection(Library.Properties.Settings.Default.Building_dbConnectionString);
            SqlDataAdapter sqlDA = new SqlDataAdapter("NhiemVu_Loai_getAll2", sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public void Delete()
        {
            SqlConnection sqlCon = new SqlConnection(Library.Properties.Settings.Default.Building_dbConnectionString);
            SqlCommand sqlCmd = new SqlCommand("NhiemVu_Loai_delete", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaLNV", MaLNV);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }
    }
}