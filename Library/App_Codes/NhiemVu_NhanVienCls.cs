using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;

namespace Library
{
    public class NhiemVu_NhanVienCls
    {
        public int MaNVu;
        public int MaNV, MaNVGiao;

        public NhiemVu_NhanVienCls()
        {
        }

        public NhiemVu_NhanVienCls(int _MaNVu, int _MaNV)
        {
            SqlConnection sqlCon = new SqlConnection(Library.Properties.Settings.Default.Building_dbConnectionString);
            SqlCommand sqlCmd = new SqlCommand("NhiemVu_NhanVien_get", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaNVu", _MaNVu);
            sqlCmd.Parameters.AddWithValue("@MaNV", _MaNV);
            sqlCon.Open();
            SqlDataReader dread = sqlCmd.ExecuteReader();
            if (dread.Read())
            {
                MaNVu = int.Parse(dread["MaNVu"].ToString());
                MaNV = int.Parse(dread["MaNV"].ToString());
            }
            sqlCon.Close();
        }

        public void Insert()
        {
            SqlConnection sqlCon = new SqlConnection(Library.Properties.Settings.Default.Building_dbConnectionString);
            SqlCommand sqlCmd = new SqlCommand("NhiemVu_NhanVien_add", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;

            sqlCmd.Parameters.AddWithValue("@MaNVu", MaNVu);
            sqlCmd.Parameters.AddWithValue("@MaNV", MaNV);
            sqlCmd.Parameters.AddWithValue("@MaNVGiao", MaNVGiao);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public void Update()
        {
            SqlConnection sqlCon = new SqlConnection(Library.Properties.Settings.Default.Building_dbConnectionString);
            SqlCommand sqlCmd = new SqlCommand("NhiemVu_NhanVien_update", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaNVu", MaNVu);
            sqlCmd.Parameters.AddWithValue("@MaNV", MaNV);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public DataTable Select()
        {
            SqlConnection sqlCon = new SqlConnection(Library.Properties.Settings.Default.Building_dbConnectionString);
            SqlDataAdapter sqlDA = new SqlDataAdapter("NhiemVu_NhanVien_getAll", sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable SelectBy()
        {
            SqlConnection sqlCon = new SqlConnection(Library.Properties.Settings.Default.Building_dbConnectionString);
            SqlDataAdapter sqlDA = new SqlDataAdapter("NhiemVu_NhanVien_getByMaNVu " + MaNVu + ", " + MaNV, sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable SelectBy2()
        {
            SqlConnection sqlCon = new SqlConnection(Library.Properties.Settings.Default.Building_dbConnectionString);
            SqlDataAdapter sqlDA = new SqlDataAdapter("NhiemVu_NhanVien_getByMaNVu2 " + MaNVu + ", " + MaNV, sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public void Delete()
        {
            SqlConnection sqlCon = new SqlConnection(Library.Properties.Settings.Default.Building_dbConnectionString);
            SqlCommand sqlCmd = new SqlCommand("NhiemVu_NhanVien_delete", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaNVu", MaNVu);
            sqlCmd.Parameters.AddWithValue("@MaNV", MaNV);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }
    }
}