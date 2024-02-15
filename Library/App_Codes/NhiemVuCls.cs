using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;

namespace Library
{
    public class NhiemVuCls
    {
        public int MaNVu;
        public string TieuDe;
        public string DienGiai, Rings;
        public DateTime NgayBD;
        public DateTime NgayHH;
        public DateTime NgayHT, NgayTao, NgayCapNhat;
        public decimal PhanTramHT;
        public NhiemVu_TinhTrangCls TinhTrang = new NhiemVu_TinhTrangCls();
        public NhiemVu_MucDoCls MucDo = new NhiemVu_MucDoCls();
        public NhiemVu_LoaiCls LoaiNV = new NhiemVu_LoaiCls();
        public NhiemVu_NhanVienCls NVNhanVien = new NhiemVu_NhanVienCls();
        //public int MaKH;
        public KhachHangCls KhachHang = new KhachHangCls();
        public bool IsNhac;
        public DateTime NgayNhac;
        public NhanVienCls NhanVien = new NhanVienCls();
        public int NguoiCapNhat;
        public int TongSoNgay;
        public Library.NhiemVu_TienDo TienDo = new Library.NhiemVu_TienDo();

        public NhiemVuCls()
        {
        }

        public NhiemVuCls(int _MaNVu)
        {
            SqlConnection sqlCon = new SqlConnection(Library.Properties.Settings.Default.Building_dbConnectionString);
            SqlCommand sqlCmd = new SqlCommand("NhiemVu_get", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaNVu", _MaNVu);
            sqlCon.Open();
            SqlDataReader dread = sqlCmd.ExecuteReader();
            if (dread.Read())
            {
                MaNVu = int.Parse(dread["MaNVu"].ToString());
                TieuDe = dread["TieuDe"] as string;
                DienGiai = dread["DienGiai"] as string;
                if (dread["NgayBD"].ToString() != "")
                    NgayBD = (DateTime)dread["NgayBD"];
                if (dread["NgayHH"].ToString() != "")
                    NgayHH = (DateTime)dread["NgayHH"];
                if (dread["NgayHT"].ToString() != "")
                    NgayHT = (DateTime)dread["NgayHT"];
                if (dread["NgayTao"].ToString() != "")
                    NgayTao = (DateTime)dread["NgayTao"];
                if (dread["NgayCapNhat"].ToString() != "")
                    NgayCapNhat = (DateTime)dread["NgayCapNhat"];
                PhanTramHT = decimal.Parse(dread["PhanTramHT"].ToString());
                TinhTrang.MaTT = int.Parse(dread["MaTT"].ToString());
                MucDo.MaMD = int.Parse(dread["MaMD"].ToString());
                LoaiNV.MaLNV = byte.Parse(dread["MaLNV"].ToString());
                TienDo.MaTD = dread["MaTD"].ToString() == "" ? 0 : (short)dread["MaTD"];
                KhachHang.MaKH = dread["MaKH"].ToString() == "" ? 0 : int.Parse(dread["MaKH"].ToString());
                KhachHang.HoKH = dread["HoTenKH"] as string;//Vay muon
                NhanVien.MaNV = dread["MaNV"].ToString() == "" ? 0 : int.Parse(dread["MaNV"].ToString());
                NhanVien.HoTen = dread["HoTenNV"] as string;
                NguoiCapNhat = dread["NguoiCapNhat"].ToString() == "" ? 0 : int.Parse(dread["NguoiCapNhat"].ToString());
                IsNhac = (bool)dread["IsNhac"];
                if (dread["NgayNhac"].ToString() != "")
                    NgayNhac = (DateTime)dread["NgayNhac"];
            }
            sqlCon.Close();
        }

        public int Insert()
        {
            SqlConnection sqlCon = new SqlConnection(Library.Properties.Settings.Default.Building_dbConnectionString);
            SqlCommand sqlCmd = new SqlCommand("NhiemVu_add", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.Add("@MaNVu", SqlDbType.Int).Direction = ParameterDirection.Output;
            sqlCmd.Parameters.AddWithValue("@TieuDe", TieuDe);
            sqlCmd.Parameters.AddWithValue("@DienGiai", DienGiai);
            sqlCmd.Parameters.AddWithValue("@NgayBD", NgayBD);
            sqlCmd.Parameters.AddWithValue("@NgayHH", NgayHH);
            if (NgayHT.Year != 1)
                sqlCmd.Parameters.AddWithValue("@NgayHT", NgayHT);
            else
                sqlCmd.Parameters.AddWithValue("@NgayHT", DBNull.Value);
            sqlCmd.Parameters.AddWithValue("@PhanTramHT", PhanTramHT);
            sqlCmd.Parameters.AddWithValue("@MaTT", TinhTrang.MaTT);
            sqlCmd.Parameters.AddWithValue("@MaMD", MucDo.MaMD);
            sqlCmd.Parameters.AddWithValue("@MaLNV", LoaiNV.MaLNV);
            sqlCmd.Parameters.AddWithValue("@MaTD", DBNull.Value);
            if (KhachHang.MaKH != 0)
                sqlCmd.Parameters.AddWithValue("@MaKH", KhachHang.MaKH);
            else
                sqlCmd.Parameters.AddWithValue("@MaKH", DBNull.Value);
            sqlCmd.Parameters.AddWithValue("@MaNV", NhanVien.MaNV);
            sqlCmd.Parameters.AddWithValue("@IsNhac", IsNhac);
            if (NgayNhac.Year != 1)
                sqlCmd.Parameters.AddWithValue("@NgayNhac", NgayNhac);
            else
                sqlCmd.Parameters.AddWithValue("@NgayNhac", DBNull.Value);
            sqlCmd.Parameters.AddWithValue("@Rings", Rings);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();

            return int.Parse(sqlCmd.Parameters["@MaNVu"].Value.ToString());
        }

        public void Update()
        {
            SqlConnection sqlCon = new SqlConnection(Library.Properties.Settings.Default.Building_dbConnectionString);
            SqlCommand sqlCmd = new SqlCommand("NhiemVu_update1", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaNVu", MaNVu); sqlCmd.Parameters.AddWithValue("@TieuDe", TieuDe);
            sqlCmd.Parameters.AddWithValue("@DienGiai", DienGiai);
            sqlCmd.Parameters.AddWithValue("@NgayBD", NgayBD);
            sqlCmd.Parameters.AddWithValue("@NgayHH", NgayHH);
            if (NgayHT.Year != 1)
                sqlCmd.Parameters.AddWithValue("@NgayHT", NgayHT);
            else
                sqlCmd.Parameters.AddWithValue("@NgayHT", DBNull.Value);

            sqlCmd.Parameters.AddWithValue("@PhanTramHT", PhanTramHT);
            sqlCmd.Parameters.AddWithValue("@MaTT", TinhTrang.MaTT);
            sqlCmd.Parameters.AddWithValue("@MaMD", MucDo.MaMD);
            sqlCmd.Parameters.AddWithValue("@MaLNV", LoaiNV.MaLNV);
            sqlCmd.Parameters.AddWithValue("@MaTD", DBNull.Value);
            if (KhachHang.MaKH != 0)
                sqlCmd.Parameters.AddWithValue("@MaKH", KhachHang.MaKH);
            else
                sqlCmd.Parameters.AddWithValue("@MaKH", DBNull.Value);
            sqlCmd.Parameters.AddWithValue("@MaNV", NhanVien.MaNV);
            sqlCmd.Parameters.AddWithValue("@IsNhac", IsNhac);
            if (NgayNhac.Year != 1)
                sqlCmd.Parameters.AddWithValue("@NgayNhac", NgayNhac);
            else
                sqlCmd.Parameters.AddWithValue("@NgayNhac", DBNull.Value);
            sqlCmd.Parameters.AddWithValue("@Rings", Rings);
            if (NguoiCapNhat != 0)
                sqlCmd.Parameters.AddWithValue("@NguoiCapNhat", NguoiCapNhat);
            else
                sqlCmd.Parameters.AddWithValue("@NguoiCapNhat", DBNull.Value);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public void UpdateProcess()
        {
            SqlConnection sqlCon = new SqlConnection(Library.Properties.Settings.Default.Building_dbConnectionString);
            SqlCommand sqlCmd = new SqlCommand("NhiemVu_LichSu_add", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaNVu", MaNVu); 
            sqlCmd.Parameters.AddWithValue("@DienGiai", DienGiai);
            sqlCmd.Parameters.AddWithValue("@PhanTram", PhanTramHT);
            sqlCmd.Parameters.AddWithValue("@MaTT", TinhTrang.MaTT);
            sqlCmd.Parameters.AddWithValue("@MaTD", DBNull.Value);
            sqlCmd.Parameters.AddWithValue("@MaNV", NhanVien.MaNV);

            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public void UpdateTime()
        {
            SqlConnection sqlCon = new SqlConnection(Library.Properties.Settings.Default.Building_dbConnectionString);
            SqlCommand sqlCmd = new SqlCommand("NhiemVu_updateTime", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaNVu", MaNVu); 
            sqlCmd.Parameters.AddWithValue("@NgayBD", NgayBD);
            sqlCmd.Parameters.AddWithValue("@NgayHH", NgayHH);

            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public void UpdateFinish()
        {
            SqlConnection sqlCon = new SqlConnection(Library.Properties.Settings.Default.Building_dbConnectionString);
            SqlCommand sqlCmd = new SqlCommand("NhiemVu_updateFinish", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaNVu", MaNVu);
            sqlCmd.Parameters.AddWithValue("@MaNV", NhanVien.MaNV);

            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public void UpdateSubject()
        {
            SqlConnection sqlCon = new SqlConnection(Library.Properties.Settings.Default.Building_dbConnectionString);
            SqlCommand sqlCmd = new SqlCommand("NhiemVu_updateSubject", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaNVu", MaNVu);
            sqlCmd.Parameters.AddWithValue("@TieuDe", TieuDe);

            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public DataTable Select()
        {
            SqlConnection sqlCon = new SqlConnection(Library.Properties.Settings.Default.Building_dbConnectionString);
            SqlDataAdapter sqlDA = new SqlDataAdapter("NhiemVu_getAll", sqlCon);

            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable SelectHistory()
        {
            SqlConnection sqlCon = new SqlConnection(Library.Properties.Settings.Default.Building_dbConnectionString);
            SqlDataAdapter sqlDA = new SqlDataAdapter("NhiemVu_LichSu_getByMaNVu " + MaNVu, sqlCon);

            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable Reminders(bool cateID)
        {
            SqlConnection sqlCon = new SqlConnection(Library.Properties.Settings.Default.Building_dbConnectionString);
            SqlDataAdapter sqlDA = new SqlDataAdapter(string.Format("Reminders_getBy {0}, {1}", NhanVien.MaNV, cateID), sqlCon);
            try
            {
                DataSet dSet = new DataSet();
                sqlCon.Open();
                sqlDA.Fill(dSet);
                sqlCon.Close();
                return dSet.Tables[0];
            }
            catch {
                return null;
            }
        }

        public DataTable SelectDay(DateTime TuNgay)
        {
            SqlConnection sqlCon = new SqlConnection(Library.Properties.Settings.Default.Building_dbConnectionString);
            SqlCommand sqlCmd = new SqlCommand("NhiemVu_getByDate ", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@TuNgay", TuNgay);
            SqlDataAdapter sqlDA = new SqlDataAdapter(sqlCmd);

            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable SelectAll()
        {
            SqlConnection sqlCon = new SqlConnection(Library.Properties.Settings.Default.Building_dbConnectionString);
            SqlCommand sqlCmd = new SqlCommand("NhiemVu_getByDate", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@TuNgay", NgayBD);
            sqlCmd.Parameters.AddWithValue("@DenNgay", NgayHH);
            sqlCmd.Parameters.AddWithValue("@MaMD", MucDo.TenMD);
            sqlCmd.Parameters.AddWithValue("@MaTT", TinhTrang.TenTT);
            sqlCmd.Parameters.AddWithValue("@MaLNV", LoaiNV.TenLNV);
            sqlCmd.Parameters.AddWithValue("@MaNV", NhanVien.MaNV);
            sqlCmd.Parameters.AddWithValue("@Option", IsNhac);
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
            SqlCommand sqlCmd = new SqlCommand("NhiemVu_getAllByMaNV", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@TuNgay", NgayBD);
            sqlCmd.Parameters.AddWithValue("@DenNgay", NgayHH);
            sqlCmd.Parameters.AddWithValue("@MaNV", NhanVien.MaNV);
            sqlCmd.Parameters.AddWithValue("@MaMD", MucDo.TenMD);
            sqlCmd.Parameters.AddWithValue("@MaTT", TinhTrang.TenTT);
            sqlCmd.Parameters.AddWithValue("@MaLNV", LoaiNV.TenLNV);
            sqlCmd.Parameters.AddWithValue("@Option", IsNhac);
            SqlDataAdapter sqlDA = new SqlDataAdapter(sqlCmd);

            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable SelectDuocGiao()
        {
            SqlConnection sqlCon = new SqlConnection(Library.Properties.Settings.Default.Building_dbConnectionString);
            SqlCommand sqlCmd = new SqlCommand("NhiemVu_getAllDuocGiao", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@TuNgay", NgayBD);
            sqlCmd.Parameters.AddWithValue("@DenNgay", NgayHH);
            sqlCmd.Parameters.AddWithValue("@MaNV", NhanVien.MaNV);
            sqlCmd.Parameters.AddWithValue("@MaMD", MucDo.TenMD);
            sqlCmd.Parameters.AddWithValue("@MaTT", TinhTrang.TenTT);
            sqlCmd.Parameters.AddWithValue("@MaLNV", LoaiNV.TenLNV);
            sqlCmd.Parameters.AddWithValue("@Option", IsNhac);
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
            SqlCommand sqlCmd = new SqlCommand("NhiemVu_getAllByGoup", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@TuNgay", NgayBD);
            sqlCmd.Parameters.AddWithValue("@DenNgay", NgayHH);
            sqlCmd.Parameters.AddWithValue("@GroupID", NhanVien.MaNKD);
            sqlCmd.Parameters.AddWithValue("@MaMD", MucDo.TenMD);
            sqlCmd.Parameters.AddWithValue("@MaTT", TinhTrang.TenTT);
            sqlCmd.Parameters.AddWithValue("@MaLNV", LoaiNV.TenLNV);
            sqlCmd.Parameters.AddWithValue("@MaNV", NhanVien.MaNV);
            sqlCmd.Parameters.AddWithValue("@Option", IsNhac);
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
            SqlCommand sqlCmd = new SqlCommand("NhiemVu_getAllByDepartment", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@TuNgay", NgayBD);
            sqlCmd.Parameters.AddWithValue("@DenNgay", NgayHH);
            sqlCmd.Parameters.AddWithValue("@DepID", NhanVien.MaPB);
            sqlCmd.Parameters.AddWithValue("@MaMD", MucDo.TenMD);
            sqlCmd.Parameters.AddWithValue("@MaTT", TinhTrang.TenTT);
            sqlCmd.Parameters.AddWithValue("@MaLNV", LoaiNV.TenLNV);
            sqlCmd.Parameters.AddWithValue("@MaNV", NhanVien.MaNV);
            sqlCmd.Parameters.AddWithValue("@Option", IsNhac);
            SqlDataAdapter sqlDA = new SqlDataAdapter(sqlCmd);

            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable SelectNVGiao()
        {
            SqlConnection sqlCon = new SqlConnection(Library.Properties.Settings.Default.Building_dbConnectionString);
            SqlDataAdapter sqlDa = new SqlDataAdapter("NhiemVu_TheoNV " + NVNhanVien.MaNV, sqlCon);
            DataSet dset = new DataSet();
            sqlCon.Open();
            sqlDa.Fill(dset);
            sqlCon.Close();
            return dset.Tables[0];
        }
        public DataTable Select_List()
        {
            SqlConnection sqlCon = new SqlConnection(Library.Properties.Settings.Default.Building_dbConnectionString);
            SqlDataAdapter sqlDa = new SqlDataAdapter("NhiemVu_select_List " + NhanVien.MaNV, sqlCon);
            DataSet dset = new DataSet();
            sqlCon.Open();
            sqlDa.Fill(dset);
            sqlCon.Close();
            return dset.Tables[0];
        }
        public DataTable Selectdetail()
        {
            SqlConnection sqlCon = new SqlConnection(Library.Properties.Settings.Default.Building_dbConnectionString);
            SqlDataAdapter sqlDa = new SqlDataAdapter("NhiemVu_selectdetail '" + MaNVu + "'", sqlCon);
            DataSet dset = new DataSet();
            sqlCon.Open();
            sqlDa.Fill(dset);
            sqlCon.Close();
            return dset.Tables[0];
        }

        public void Delete()
        {
            SqlConnection sqlCon = new SqlConnection(Library.Properties.Settings.Default.Building_dbConnectionString);
            SqlCommand sqlCmd = new SqlCommand("NhiemVu_delete", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaNVu", MaNVu);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public void DeleteReminders()
        {
            SqlConnection sqlCon = new SqlConnection(Library.Properties.Settings.Default.Building_dbConnectionString);
            SqlCommand sqlCmd = new SqlCommand("Reminders_delete", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaNVu", MaNVu);            
            sqlCmd.Parameters.AddWithValue("@MaNV", NhanVien.MaNV);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public DataTable SelectMaLH()
        {
            SqlConnection sqlCon = new SqlConnection(Library.Properties.Settings.Default.Building_dbConnectionString);
            SqlDataAdapter sqlDA = new SqlDataAdapter("LichHen_selectdetail_ctl " + MaNVu, sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public string GetStaff()
        {

            SqlConnection sqlCon = new SqlConnection(Library.Properties.Settings.Default.Building_dbConnectionString);
            SqlCommand sqlCmd = new SqlCommand("NhiemVu_NhanVien_getName", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaNVu", MaNVu);
            sqlCmd.Parameters.Add("@Re", SqlDbType.NVarChar, 1000).Direction = ParameterDirection.Output;
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
            return sqlCmd.Parameters["@Re"].Value.ToString();
        }
        public void NhiemVu_NhanVien_Delete()
        {
            SqlConnection sqlCon = new SqlConnection(Library.Properties.Settings.Default.Building_dbConnectionString);
            SqlCommand sqlCmd = new SqlCommand("NhiemVu_NhanVien_delete1", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaNVu", MaNVu);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }
        public string GetNhiemVu()
        {

            SqlConnection sqlCon = new SqlConnection(Library.Properties.Settings.Default.Building_dbConnectionString);
            SqlCommand sqlCmd = new SqlCommand("NhiemVu_getTieuDe", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaNVu",MaNVu);
            sqlCmd.Parameters.Add("@Re", SqlDbType.NVarChar, 150).Direction = ParameterDirection.Output;
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
            return sqlCmd.Parameters["@Re"].Value.ToString();
        }
    }
}