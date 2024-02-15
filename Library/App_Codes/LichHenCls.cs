using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;

namespace Library
{
    public class LichHenCls
    {
        public int MaLH;
        public string TieuDe;
        public string DienGiai;
        public string DiaDiem, Rings, NhanVienStr;
        public DateTime NgayBD;
        public DateTime NgayKT;
        public LichHen_ThoiDiemCls ThoiDiem = new LichHen_ThoiDiemCls();
        public LichHen_ChuDeCls ChuDe = new LichHen_ChuDeCls();
        public KhachHangCls KhachHang = new KhachHangCls();
        public NhanVienCls NhanVien = new NhanVienCls();
        public NhiemVuCls NhiemVu = new NhiemVuCls();
        public bool IsNhac, IsRepeat;
        public byte TimeID, TimeID2;
        public int TongSoNgay, MaNC, MaHD;
        public long? MaDMCV;
        public long? MaCVNV;

        public LichHenCls()
        {
        }

        public LichHenCls(int _MaLH)
        {
            SqlConnection sqlCon = new SqlConnection(Library.Properties.Settings.Default.Building_dbConnectionString);
            SqlCommand sqlCmd = new SqlCommand("LichHen_get", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaLH", _MaLH);
            sqlCon.Open();
            SqlDataReader dread = sqlCmd.ExecuteReader();
            if (dread.Read())
            {
                MaLH = int.Parse(dread["MaLH"].ToString());
                TieuDe = dread["TieuDe"] as string;
                DienGiai = dread["DienGiai"] as string;
                DiaDiem = dread["DiaDiem"] as string;
                Rings = dread["Rings"] as string;
                NgayBD = (DateTime)dread["NgayBD"];
                NgayKT = (DateTime)dread["NgayKT"];
                ThoiDiem.MaTD = int.Parse(dread["MaTD"].ToString());
                ChuDe.MaCD = int.Parse(dread["MaCD"].ToString());
                KhachHang.MaKH = dread["MaKH"].ToString() == "" ? 0 : int.Parse(dread["MaKH"].ToString());
                NhanVien.MaNV = int.Parse(dread["MaNV"].ToString());
                NhanVien.HoTen = dread["HoTen"] as string;
                NhiemVu.MaNVu = dread["MaNVu"].ToString() == "" ? 0 : int.Parse(dread["MaNVu"].ToString());
                MaNC = dread["MaNC"].ToString() == "" ? 0 : int.Parse(dread["MaNC"].ToString());
                MaHD = dread["MaHD"].ToString() == "" ? 0 : int.Parse(dread["MaHD"].ToString());
                NhiemVu.TieuDe = dread["TenNVu"].ToString();
                IsNhac = (bool)dread["IsNhac"];
                IsRepeat = (bool)dread["IsRepeat"];
                TimeID = dread["TimeID"].ToString() == "" ? (byte)0 : byte.Parse(dread["TimeID"].ToString());
                TimeID2 = dread["TimeID2"].ToString() == "" ? (byte)0 : byte.Parse(dread["TimeID2"].ToString());
                ThoiDiem.MaTD = Convert.ToInt32(dread["MaTD"].ToString());
                ChuDe.MaCD = Convert.ToInt32(dread["MaCD"].ToString());
                MaDMCV = long.Parse(dread["MaDMCV"].ToString());
                MaCVNV = long.Parse(dread["MaCVNV"].ToString());
            }
            sqlCon.Close();
        }

        public int Insert()
        {
            SqlConnection sqlCon = new SqlConnection(Library.Properties.Settings.Default.Building_dbConnectionString);
            SqlCommand sqlCmd = new SqlCommand("LichHen_add", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaLH", MaLH).Direction = ParameterDirection.Output;
            sqlCmd.Parameters.AddWithValue("@TieuDe", TieuDe);
            sqlCmd.Parameters.AddWithValue("@DienGiai", DienGiai);
            sqlCmd.Parameters.AddWithValue("@DiaDiem", DiaDiem);
            sqlCmd.Parameters.AddWithValue("@NgayBD", NgayBD);
            sqlCmd.Parameters.AddWithValue("@NgayKT", NgayKT);
            sqlCmd.Parameters.AddWithValue("@MaTD", ThoiDiem.MaTD);
            sqlCmd.Parameters.AddWithValue("@MaCD", ChuDe.MaCD);
            if (MaDMCV != null)
                sqlCmd.Parameters.AddWithValue("@MaDMCV", MaDMCV);
            else
                sqlCmd.Parameters.AddWithValue("@MaDMCV", DBNull.Value);
            if (MaCVNV != null)
                sqlCmd.Parameters.AddWithValue("@MaCVNV", MaCVNV);
            else
                sqlCmd.Parameters.AddWithValue("@MaCVNV", DBNull.Value);

            if (KhachHang.MaKH != 0)
                sqlCmd.Parameters.AddWithValue("@MaKH", KhachHang.MaKH);
            else
                sqlCmd.Parameters.AddWithValue("@MaKH", DBNull.Value);

            sqlCmd.Parameters.AddWithValue("@MaNV", NhanVien.MaNV);
            if (NhiemVu.MaNVu != 0)
                sqlCmd.Parameters.AddWithValue("@MaNVu", NhiemVu.MaNVu);
            else
                sqlCmd.Parameters.AddWithValue("@MaNVu", DBNull.Value);
            if (MaNC != 0)
                sqlCmd.Parameters.AddWithValue("@MaNC", MaNC);
            else
                sqlCmd.Parameters.AddWithValue("@MaNC", DBNull.Value);
            if (MaHD != 0)
                sqlCmd.Parameters.AddWithValue("@MaHD", MaHD);
            else
                sqlCmd.Parameters.AddWithValue("@MaHD", DBNull.Value);
            sqlCmd.Parameters.AddWithValue("@IsNhac", IsNhac);
            sqlCmd.Parameters.AddWithValue("@TimeID", TimeID);
            sqlCmd.Parameters.AddWithValue("@Rings", Rings);
            sqlCmd.Parameters.AddWithValue("@IsRepeat", IsRepeat);
            sqlCmd.Parameters.AddWithValue("@TimeID2", TimeID2);
            sqlCmd.Parameters.AddWithValue("@NhanVien", NhanVienStr);

            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();

            return int.Parse(sqlCmd.Parameters["@MaLH"].Value.ToString());
        }

        public void Update()
        {
            SqlConnection sqlCon = new SqlConnection(Library.Properties.Settings.Default.Building_dbConnectionString);
            SqlCommand sqlCmd = new SqlCommand("LichHen_update", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaLH", MaLH);
            sqlCmd.Parameters.AddWithValue("@TieuDe", TieuDe);
            sqlCmd.Parameters.AddWithValue("@DienGiai", DienGiai);
            sqlCmd.Parameters.AddWithValue("@DiaDiem", DiaDiem);
            sqlCmd.Parameters.AddWithValue("@NgayBD", NgayBD);
            sqlCmd.Parameters.AddWithValue("@NgayKT", NgayKT);
            sqlCmd.Parameters.AddWithValue("@MaTD", ThoiDiem.MaTD);
            sqlCmd.Parameters.AddWithValue("@MaCD", ChuDe.MaCD);
            if (KhachHang.MaKH != 0)
                sqlCmd.Parameters.AddWithValue("@MaKH", KhachHang.MaKH);
            else
                sqlCmd.Parameters.AddWithValue("@MaKH", DBNull.Value);

            sqlCmd.Parameters.AddWithValue("@MaNV", NhanVien.MaNV);
            if (NhiemVu.MaNVu != 0)
                sqlCmd.Parameters.AddWithValue("@MaNVu", NhiemVu.MaNVu);
            else
                sqlCmd.Parameters.AddWithValue("@MaNVu", DBNull.Value);
            if (MaNC != 0)
                sqlCmd.Parameters.AddWithValue("@MaNC", MaNC);
            else
                sqlCmd.Parameters.AddWithValue("@MaNC", DBNull.Value);
            if (MaHD != 0)
                sqlCmd.Parameters.AddWithValue("@MaHD", MaHD);
            else
                sqlCmd.Parameters.AddWithValue("@MaHD", DBNull.Value);
            sqlCmd.Parameters.AddWithValue("@IsNhac", IsNhac);
            sqlCmd.Parameters.AddWithValue("@TimeID", TimeID);
            sqlCmd.Parameters.AddWithValue("@Rings", Rings);
            sqlCmd.Parameters.AddWithValue("@IsRepeat", IsRepeat);
            sqlCmd.Parameters.AddWithValue("@TimeID2", TimeID2);
            sqlCmd.Parameters.AddWithValue("@NhanVien", NhanVienStr);

            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public DataTable Select()
        {
            SqlConnection sqlCon = new SqlConnection(Library.Properties.Settings.Default.Building_dbConnectionString);
            SqlDataAdapter sqlDA = new SqlDataAdapter("LichHen_getAll", sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable SelectBy()
        {
            SqlConnection sqlCon = new SqlConnection(Library.Properties.Settings.Default.Building_dbConnectionString);
            SqlDataAdapter sqlDA = new SqlDataAdapter("LichHen_getAllByMaNVu " + NhiemVu.MaNVu, sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable Select_ctl()
        {
            SqlConnection sqlCon = new SqlConnection(Library.Properties.Settings.Default.Building_dbConnectionString);
            SqlDataAdapter sqlDA = new SqlDataAdapter("LichHen_select_ctl " + NhanVien.MaNV, sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable SelectAll()
        {
            SqlConnection sqlCon = new SqlConnection(Library.Properties.Settings.Default.Building_dbConnectionString);
            SqlCommand sqlCmd = new SqlCommand("LichHen_getByDate", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@TuNgay", NgayBD);
            sqlCmd.Parameters.AddWithValue("@DenNgay", NgayKT);
            sqlCmd.Parameters.AddWithValue("@MaCD", ChuDe.TenCD);
            sqlCmd.Parameters.AddWithValue("@MaTD", ThoiDiem.TenTD);
            sqlCmd.Parameters.AddWithValue("@MaNV", NhanVien.MaNV);
            SqlDataAdapter sqlDA = new SqlDataAdapter(sqlCmd);

            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable SelectByStaff()
        {
            SqlConnection sqlCon = new SqlConnection(Library.Properties.Settings.Default.Building_dbConnectionString);
            SqlCommand sqlCmd = new SqlCommand("LichHen_getAllByMaNV", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@TuNgay", NgayBD);
            sqlCmd.Parameters.AddWithValue("@DenNgay", NgayKT);
            sqlCmd.Parameters.AddWithValue("@MaNV", NhanVien.MaNV);
            sqlCmd.Parameters.AddWithValue("@MaCD", ChuDe.TenCD);
            sqlCmd.Parameters.AddWithValue("@MaTD", ThoiDiem.TenTD);
            SqlDataAdapter sqlDA = new SqlDataAdapter(sqlCmd);

            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable SelectByGroup()
        {
            SqlConnection sqlCon = new SqlConnection(Library.Properties.Settings.Default.Building_dbConnectionString);
            SqlCommand sqlCmd = new SqlCommand("LichHen_getAllByGoup", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@TuNgay", NgayBD);
            sqlCmd.Parameters.AddWithValue("@DenNgay", NgayKT);
            sqlCmd.Parameters.AddWithValue("@GroupID", NhanVien.MaNKD);
            sqlCmd.Parameters.AddWithValue("@MaCD", ChuDe.TenCD);
            sqlCmd.Parameters.AddWithValue("@MaTD", ThoiDiem.TenTD);
            sqlCmd.Parameters.AddWithValue("@MaNV", NhanVien.MaNV);
            SqlDataAdapter sqlDA = new SqlDataAdapter(sqlCmd);

            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable SelectByDepartment()
        {
            SqlConnection sqlCon = new SqlConnection(Library.Properties.Settings.Default.Building_dbConnectionString);
            SqlCommand sqlCmd = new SqlCommand("LichHen_getAllByDepartment", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@TuNgay", NgayBD);
            sqlCmd.Parameters.AddWithValue("@DenNgay", NgayKT);
            sqlCmd.Parameters.AddWithValue("@DepID", NhanVien.MaPB);
            sqlCmd.Parameters.AddWithValue("@MaCD", ChuDe.TenCD);
            sqlCmd.Parameters.AddWithValue("@MaTD", ThoiDiem.TenTD);
            sqlCmd.Parameters.AddWithValue("@MaNV", NhanVien.MaNV);
            SqlDataAdapter sqlDA = new SqlDataAdapter(sqlCmd);

            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public void Delete()
        {
            SqlConnection sqlCon = new SqlConnection(Library.Properties.Settings.Default.Building_dbConnectionString);
            SqlCommand sqlCmd = new SqlCommand("LichHen_delete", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaLH", MaLH);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public void UpdateTime()
        {
            SqlConnection sqlCon = new SqlConnection(Library.Properties.Settings.Default.Building_dbConnectionString);
            SqlCommand sqlCmd = new SqlCommand("LichHen_updateTime", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaLH", MaLH);
            sqlCmd.Parameters.AddWithValue("@NgayBD", NgayBD);
            sqlCmd.Parameters.AddWithValue("@NgayKT", NgayKT);

            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public void UpdateSubject()
        {
            SqlConnection sqlCon = new SqlConnection(Library.Properties.Settings.Default.Building_dbConnectionString);
            SqlCommand sqlCmd = new SqlCommand("LichHen_updateSubject", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaLH", MaLH);
            sqlCmd.Parameters.AddWithValue("@TieuDe", TieuDe);
            sqlCmd.Parameters.AddWithValue("@MaCD", ChuDe.STT);
            sqlCmd.Parameters.AddWithValue("@MaTD", ThoiDiem.STT);

            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public void DeleteReminders()
        {
            SqlConnection sqlCon = new SqlConnection(Library.Properties.Settings.Default.Building_dbConnectionString);
            SqlCommand sqlCmd = new SqlCommand("Reminders_delete2", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaLH", MaLH);
            sqlCmd.Parameters.AddWithValue("@MaNV", NhanVien.MaNV);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public void UpdateReminders()
        {
            SqlConnection sqlCon = new SqlConnection(Library.Properties.Settings.Default.Building_dbConnectionString);
            SqlCommand sqlCmd = new SqlCommand("LichHen_updateReminders", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaNV", NhanVien.MaNV);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }
    }
}