using System.Reflection;
using System;
using System.Data.Linq.Mapping;
using System.Data.Linq;
namespace Library
{
    partial class tbl_TenTaiSan
    {
    }

    partial class template_Field
    {
    }

    partial class MasterDataContext
    {
        [Function(Name = "GetDate", IsComposable = true)]
        public DateTime GetSystemDate() 
        {
            try
            {
                return this.GetLocalDate().Value;
                //MethodInfo mi = MethodBase.GetCurrentMethod() as MethodInfo;
                //return (DateTime)this.ExecuteMethodCall(this, mi, new object[] { }).ReturnValue;
            }
            catch (Exception ex)
            {
                return DateTime.UtcNow.ToLocalTime();
            }
        }
    }

    public partial class BmBieuMau
    {
        public void Clear()
        {
            _BmLoaiBieuMau = default(EntityRef<BmLoaiBieuMau>);
        }
    }

    public partial class dvdnDien
    {
        public void Clear()
        {
            _mbMatBang = default(EntityRef<mbMatBang>);
            _tnKhachHang = default(EntityRef<tnKhachHang>);
            _mbMatBang_ChiaLo = default(EntityRef<mbMatBang_ChiaLo>);
        }
    }

    public partial class dvdnNuoc
    {
        public void Clear()
        {
            _mbMatBang = default(EntityRef<mbMatBang>);
            _tnKhachHang = default(EntityRef<tnKhachHang>);
            _mbMatBang_ChiaLo = default(EntityRef<mbMatBang_ChiaLo>);
        }
    }

    public partial class tsLoaiTaiSan
    {
        public void Clear()
        {
            _tsLoaiTaiSan_DVT = default(EntityRef<tsLoaiTaiSan_DVT>);
            _tsLoaiTaiSan_Type = default(EntityRef<tsLoaiTaiSan_Type>);
            _tsLoaiTanSan_Thue = default(EntityRef<tsLoaiTanSan_Thue>);
        }
    }

    public partial class KhoHang
    {
        public void Clear()
        {
            _mbMatBang = default(EntityRef<mbMatBang>);
        }
    }

    public partial class tnKhachHang
    {
        public string FullName
        {
            get { return this.IsCaNhan.Value ? this.HoKH + " " + this.TenKH : this.CtyTen; }
        }

        public string MobilePhone
        {
            get { return this.DienThoaiKH; }
        }

        public string Address
        {
            get { return this.DCLL; }
        }
    }

    public partial class rptPhatSinhPhiQuanLyResult
    {
        private decimal? _TongTien;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TongTien", DbType = "money")]
        public decimal? TongTien
        {
            get { return this.PGX + this.PhiQL + this.PVS; }
        }
    }

    public partial class rptChiTietGasNuocResult
    {
        private decimal? _SoTieuThuNuoc;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_SoTieuThuNuoc", DbType = "int")]
        public decimal? SoTieuThuNuoc
        {
            get { return this.ChiSoMoiNuoc - this.ChiSoCuNuoc; }
        }
    }

    public partial class rptNuocTieuThuResult
    {
        int? Format(int? val)
        {
            if (val > 0) return val;

            return 0;
        }

        private int? _TT1;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TT1", DbType = "int")]
        public int? TT1
        {
            get { return Format(this.CST1 - this.CSBG); }
        }

        private int? _TT2;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TT2", DbType = "int")]
        public int? TT2
        {
            get { return Format(this.CST2 - this.CST1); }
        }

        private int? _TT3;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TT3", DbType = "int")]
        public int? TT3
        {
            get { return Format(this.CST3 - this.CST2); }
        }

        private int? _TT4;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TT4", DbType = "int")]
        public int? TT4
        {
            get { return Format(this.CST4 - this.CST3); }
        }

        private int? _TT5;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TT5", DbType = "int")]
        public int? TT5
        {
            get { return Format(this.CST5 - this.CST4); }
        }

        private int? _TT6;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TT6", DbType = "int")]
        public int? TT6
        {
            get { return Format(this.CST6 - this.CST5); }
        }

        private int? _TT7;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TT7", DbType = "int")]
        public int? TT7
        {
            get { return Format(this.CST7 - this.CST6); }
        }

        private int? _TT8;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TT8", DbType = "int")]
        public int? TT8
        {
            get { return Format(this.CST8 - this.CST7); }
        }

        private int? _TT9;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TT9", DbType = "int")]
        public int? TT9
        {
            get { return Format(this.CST9 - this.CST8); }
        }

        private int? _TT10;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TT10", DbType = "int")]
        public int? TT10
        {
            get { return Format(this.CST10 - this.CST9); }
        }

        private int? _TT11;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TT11", DbType = "int")]
        public int? TT11
        {
            get { return Format(this.CST11 - this.CST10); }
        }

        private int? _TT12;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TT12", DbType = "int")]
        public int? TT12
        {
            get { return Format(this.CST12 - this.CST11); }
        }
    }

    public partial class rptDienTieuThuResult
    {
        int? Format(int? val)
        {
            if (val > 0) return val;

            return 0;
        }

        private int? _TT1;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TT1", DbType = "int")]
        public int? TT1
        {
            get { return Format(this.CST1 - this.CSBG); }
        }

        private int? _TT2;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TT2", DbType = "int")]
        public int? TT2
        {
            get { return Format(this.CST2 - this.CST1); }
        }

        private int? _TT3;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TT3", DbType = "int")]
        public int? TT3
        {
            get { return Format(this.CST3 - this.CST2); }
        }

        private int? _TT4;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TT4", DbType = "int")]
        public int? TT4
        {
            get { return Format(this.CST4 - this.CST3); }
        }

        private int? _TT5;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TT5", DbType = "int")]
        public int? TT5
        {
            get { return Format(this.CST5 - this.CST4); }
        }

        private int? _TT6;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TT6", DbType = "int")]
        public int? TT6
        {
            get { return Format(this.CST6 - this.CST5); }
        }

        private int? _TT7;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TT7", DbType = "int")]
        public int? TT7
        {
            get { return Format(this.CST7 - this.CST6); }
        }

        private int? _TT8;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TT8", DbType = "int")]
        public int? TT8
        {
            get { return Format(this.CST8 - this.CST7); }
        }

        private int? _TT9;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TT9", DbType = "int")]
        public int? TT9
        {
            get { return Format(this.CST9 - this.CST8); }
        }

        private int? _TT10;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TT10", DbType = "int")]
        public int? TT10
        {
            get { return Format(this.CST10 - this.CST9); }
        }

        private int? _TT11;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TT11", DbType = "int")]
        public int? TT11
        {
            get { return Format(this.CST11 - this.CST10); }
        }

        private int? _TT12;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TT12", DbType = "int")]
        public int? TT12
        {
            get { return Format(this.CST12 - this.CST11); }
        }
    }

    public partial class rptGasTieuThuResult
    {
        decimal? Format(decimal? val)
        {
            if (val > 0) return val;

            return 0;
        }

        private decimal? _TT1;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TT1", DbType = "money")]
        public decimal? TT1
        {
            get { return Format(this.CST1 - this.CSBG); }
        }

        private decimal? _TT2;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TT2", DbType = "money")]
        public decimal? TT2
        {
            get { return Format(this.CST2 - this.CST1); }
        }

        private decimal? _TT3;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TT3", DbType = "money")]
        public decimal? TT3
        {
            get { return Format(this.CST3 - this.CST2); }
        }

        private decimal? _TT4;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TT4", DbType = "money")]
        public decimal? TT4
        {
            get { return Format(this.CST4 - this.CST3); }
        }

        private decimal? _TT5;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TT5", DbType = "money")]
        public decimal? TT5
        {
            get { return Format(this.CST5 - this.CST4); }
        }

        private decimal? _TT6;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TT6", DbType = "money")]
        public decimal? TT6
        {
            get { return Format(this.CST6 - this.CST5); }
        }

        private decimal? _TT7;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TT7", DbType = "money")]
        public decimal? TT7
        {
            get { return Format(this.CST7 - this.CST6); }
        }

        private decimal? _TT8;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TT8", DbType = "money")]
        public decimal? TT8
        {
            get { return Format(this.CST8 - this.CST7); }
        }

        private decimal? _TT9;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TT9", DbType = "money")]
        public decimal? TT9
        {
            get { return Format(this.CST9 - this.CST8); }
        }

        private decimal? _TT10;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TT10", DbType = "money")]
        public decimal? TT10
        {
            get { return Format(this.CST10 - this.CST9); }
        }

        private decimal? _TT11;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TT11", DbType = "money")]
        public decimal? TT11
        {
            get { return Format(this.CST11 - this.CST10); }
        }

        private decimal? _TT12;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TT12", DbType = "money")]
        public decimal? TT12
        {
            get { return Format(this.CST12 - this.CST11); }
        }
    }

    public partial class rptTongHopPhaiThuResult
    {
        private decimal? _TongTien;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TongTien", DbType = "money")]
        public decimal? TongTien
        {
            get { return this.PQL + this.PQLNoCu + this.Nuoc + this.NuocNoCu + this.Gas + this.GasNoCu; }
        }
    }

    public partial class rptTongHopChuaThuResult
    {
        private decimal? _TongTien;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TongTien", DbType = "money")]
        public decimal? TongTien
        {
            get { return this.PQL + this.PQLNoCu + this.Nuoc + this.NuocNoCu + this.Gas + this.GasNoCu; }
        }
    }

    public partial class rptBangKeThuChiResult
    {
        private decimal? _TongDauKy;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TongDauKy", DbType = "money")]
        public decimal? TongDauKy
        {
            get { return this.PQLDauKy + this.DienDauKy + this.NuocDauKy + this.XeDauKy + this.HDDauKy; }
        }

        private decimal? _TongPhaiThu;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TongPhaiThu", DbType = "money")]
        public decimal? TongPhaiThu
        {
            get { return this.PQLPhaiThu + this.DienPhaiThu + this.NuocPhaiThu + this.XePhaiThu + this.HDPhaiThu; }
        }

        private decimal? _TongDaThu;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TongDaThu", DbType = "money")]
        public decimal? TongDaThu
        {
            get { return this.PQLDaThu + this.DienDaThu + this.NuocDaThu + this.XeDaThu + this.HDDaThu; }
        }

        private decimal? _TongCuoiKy;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TongCuoiKy", DbType = "money")]
        public decimal? TongCuoiKy
        {
            get { return this.PQLCuoiKy + this.DienCuoiKy + this.NuocCuoiKy + this.XeCuoiKy + this.HDCuoiKy; }
        }

        private string _Block;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Block", DbType = "NVarChar(50)")]
        public string Block
        {
            get { return GetCode(this.MaSoMB, 1); }
        }

        private string _Floor;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Floor", DbType = "NVarChar(50)")]
        public string Floor
        {
            get { return GetCode(this.MaSoMB, 2); }
        }

        private string _Lo;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Lo", DbType = "NVarChar(50)")]
        public string Lo
        {
            get { return GetCode(this.MaSoMB, 3); }
        }

        string GetCode(string code, byte position)
        {
            string[] array = code.Split('.');
            try
            {
                switch (position)
                {
                    case 1:
                        return array[1].Trim();
                    case 2:
                        return array[2].Trim();
                    case 3:
                        return array[3].Trim();
                }
            }
            catch
            {
            }
            return "";
        }
    }

    public partial class rptBangKeLuyKeNamResult
    {
        private decimal? _TongSoTien;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TongSoTien", DbType = "money")]
        public decimal? TongSoTien
        {
            get { return this.PQLSoTien + this.DienSoTien + this.NuocSoTien + this.XeSoTien; }
        }

        private decimal? _TongDaThu;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TongDaThu", DbType = "money")]
        public decimal? TongDaThu
        {
            get { return this.PQLDaThu + this.DienDaThu + this.NuocDaThu + this.XeDaThu; }
        }

        private string _Block;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Block", DbType = "NVarChar(50)")]
        public string Block
        {
            get { return GetCode(this.MaSoMB, 1); }
        }

        private string _Floor;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Floor", DbType = "NVarChar(50)")]
        public string Floor
        {
            get { return GetCode(this.MaSoMB, 2); }
        }

        private string _Lo;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Lo", DbType = "NVarChar(50)")]
        public string Lo
        {
            get { return GetCode(this.MaSoMB, 3); }
        }

        string GetCode(string code, byte position)
        {
            string[] array = code.Split('.');
            try
            {
                switch (position)
                {
                    case 1:
                        return array[1].Trim();
                    case 2:
                        return array[2].Trim();
                    case 3:
                        return array[3].Trim();
                }
            }
            catch
            {
            }
            return "";
        }
    }

    public partial class rptTongHopThuResult
    {
        private decimal? _TongTien;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TongTien", DbType = "money")]
        public decimal? TongTien
        {
            get { return this.PQL + this.PQLNoCu + this.Nuoc + this.NuocNoCu + this.Gas + this.GasNoCu; }
        }
    }

    public partial class rptPhieuThuTheoThangResult
    {
        private decimal? _TongCong;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TongCong", DbType = "money")]
        public decimal? TongCong
        {
            get { return this.PQL + this.Nuoc + this.Gas + this.PVS; }
        }
    }

    public partial class rptPhiQuanLyBangThuResult
    {
        private string _PQLQuy;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_PQLQuy", DbType = "NVarChar(2)")]
        public string PQLQuy
        {
            get { return this.DayPaid == 3 ? "x" : ""; }
        }

        private string _PQL6Thang;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_PQL6Thang", DbType = "NVarChar(2)")]
        public string PQL6Thang
        {
            get { return this.DayPaid == 6 ? "x" : ""; }
        }

        private string _PQLNam;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_PQLNam", DbType = "NVarChar(2)")]
        public string PQLNam
        {
            get { return this.DayPaid == 12 ? "x" : ""; }
        }
    }

    public partial class dvdnNuoc_getMonthResult
    {
        private int? _SoTieuThu;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_SoTieuThu", DbType = "int")]
        public int? SoTieuThu
        {
            get { return this.ChiSoMoi - this.ChiSoCu + this.HeSoDC.GetValueOrDefault(); }
        }

        private decimal? _ThueVAT;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ThueVAT", DbType = "money")]
        public decimal? ThueVAT
        {
            get { return this.TienNuoc * 10 / 100; }
        }

        private decimal? _ConLai;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ConLai", DbType = "money")]
        public decimal? ConLai
        {
            get { return this.SoTien - this.DaThu; }
        }

        private decimal? _TongNo;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TongNo", DbType = "money")]
        public decimal? TongNo
        {
            get { return this.NoTruoc + this.ConLai; }
        }

        private string _TrangThai;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TrangThai", DbType = "NVarChar(50)")]
        public string TrangThai
        {
            get { return (this.TongNo ?? 0) > 0 ? "Chưa thanh toán" : "Đã thanh toán"; }
        }
    }

    public partial class dvdnNuoc_getMonthDebtResult
    {
        private decimal? _SoTieuThu;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_SoTieuThu", DbType = "int")]
        public decimal? SoTieuThu
        {
            get { return this.ChiSoMoi - this.ChiSoCu + this.HeSoDC.GetValueOrDefault(); }
        }

        private decimal? _ThueVAT;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ThueVAT", DbType = "money")]
        public decimal? ThueVAT
        {
            get { return this.TienNuoc * 10 / 100; }
        }

        private decimal? _ConLai;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ConLai", DbType = "money")]
        public decimal? ConLai
        {
            get { return this.SoTien - this.DaThu; }
        }

        private decimal? _TongNo;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TongNo", DbType = "money")]
        public decimal? TongNo
        {
            get { return this.NoTruoc + this.ConLai; }
        }

        private string _TrangThai;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TrangThai", DbType = "NVarChar(50)")]
        public string TrangThai
        {
            get { return (this.TongNo ?? 0) > 0 ? "Chưa thanh toán" : "Đã thanh toán"; }
        }
    }

    public partial class dvGas_getMonthResult
    {
        private decimal? _SoTieuThu;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_SoTieuThu", DbType = "money")]
        public decimal? SoTieuThu
        {
            get { return this.ChiSoMoi - this.ChiSoCu + this.HeSoDC; }
        }

        private decimal? _ThueVAT;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ThueVAT", DbType = "money")]
        public decimal? ThueVAT
        {
            get { return this.TienGas * 10 / 100; }
        }

        private decimal? _TongNo;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TongNo", DbType = "money")]
        public decimal? TongNo
        {
            get { return this.NoTruoc + this.ConLai; }
        }

        private decimal? _ConLai;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ConLai", DbType = "money")]
        public decimal? ConLai
        {
            get { return this.SoTien - this.DaThu; }
        }

        private string _TrangThai;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TrangThai", DbType = "NVarChar(50)")]
        public string TrangThai
        {
            get { return (this.TongNo ?? 0) > 0 ? "Chưa thanh toán" : "Đã thanh toán"; }
        }
    }

    public partial class dvGas_getMonthDebtResult
    {
        private decimal? _SoTieuThu;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_SoTieuThu", DbType = "money")]
        public decimal? SoTieuThu
        {
            get { return this.ChiSoMoi - this.ChiSoCu; }
        }

        private decimal? _TongNo;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TongNo", DbType = "money")]
        public decimal? TongNo
        {
            get { return this.NoTruoc + this.ConLai; }
        }

        private decimal? _ConLai;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ConLai", DbType = "money")]
        public decimal? ConLai
        {
            get { return this.SoTien - this.DaThu; }
        }

        private string _TrangThai;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TrangThai", DbType = "NVarChar(50)")]
        public string TrangThai
        {
            get { return (this.TongNo ?? 0) > 0 ? "Chưa thanh toán" : "Đã thanh toán"; }
        }
    }

    public partial class dvdnDien_getMonthResult
    {
        private decimal? _SoTieuThu;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_SoTieuThu", DbType = "int")]
        public decimal? SoTieuThu
        {
            get { return this.ChiSoMoi - this.ChiSoCu ; }
        }

        private decimal? _SoTieuThuTD;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_SoTieuThuTD", DbType = "int")]
        public decimal? SoTieuThuTD
        {
            get { return this.TDChiSoCuoi ?? 0 - this.TDChiSoDau ?? 0; }
        }

        private decimal? _SoTieuThuCD;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_SoTieuThuCD", DbType = "int")]
        public decimal? SoTieuThuCD
        {
            get { return this.CDChiSoCuoi ?? 0 - this.CDChiSoDau ?? 0; }
        }

        private decimal? _ThueVAT;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ThueVAT", DbType = "money")]
        public decimal? ThueVAT
        {
            get { return this.TienDien * 10 / 100; }
        }

        private decimal? _TongNo;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TongNo", DbType = "money")]
        public decimal? TongNo
        {
            get { return this.NoTruoc + this.ConLai; }
        }

        private string _TrangThai;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TrangThai", DbType = "NVarChar(50)")]
        public string TrangThai
        {
            get { return (this.TongNo ?? 0) > 0 ? "Chưa thanh toán" : "Đã thanh toán"; }
        }
    }

    public partial class cnCongNo_getByMaMBAndMaLDVResult
    {
        private decimal? _TongNo;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TongNo", DbType = "money")]
        public decimal? TongNo
        {
            get { return this.NoTruoc + this.ConLai; }
        }
    }

    public partial class cnLichSu_getPhatSinhResult
    {
        private decimal? _TongNo;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TongNo", DbType = "money")]
        public decimal? TongNo
        {
            get { return this.NoTruoc + this.ConLai; }
        }
    }

    public partial class PhiQuanLy_selectBySuperAdminResult
    {
        private decimal? _TongNo;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TongNo", DbType = "money")]
        public decimal? TongNo
        {
            get { return this.NoTruoc + this.ConLai; }
        }

        private string _Status;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Status", DbType = "NVarChar(50)")]
        public string Status
        {
            get { return (this.TongNo ?? 0) > 0 ? "Chưa thanh toán" : "Đã thanh toán"; }
        }
    }

    public partial class PhiQuanLy_selectBySuperAdminV2Result
    {
        private decimal? _ConLai;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ConLai", DbType = "money")]
        public decimal? ConLai
        {
            get { return this.PhiQL - this.DaThu; }
        }

        private decimal? _TongNo;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TongNo", DbType = "money")]
        public decimal? TongNo
        {
            get { return this.NoTruoc + this.ConLai; }
        }

        private string _Status;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Status", DbType = "NVarChar(50)")]
        public string Status
        {
            get { return (this.TongNo ?? 0) > 0 ? "Chưa thanh toán" : "Đã thanh toán"; }
        }
    }

    public partial class PhiQuanLy_selectBySuperAdminDebtResult
    {
        private decimal? _TongNo;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TongNo", DbType = "money")]
        public decimal? TongNo
        {
            get { return this.NoTruoc + this.ConLai; }
        }

        private string _Status;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Status", DbType = "NVarChar(50)")]
        public string Status
        {
            get { return (this.TongNo ?? 0) > 0 ? "Chưa thanh toán" : "Đã thanh toán"; }
        }
    }

    public partial class PhiBaoTri_selectBySuperAdminV2Result
    {
        private decimal? _ConLai;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ConLai", DbType = "money")]
        public decimal? ConLai
        {
            get { return this.PhiQL - this.DaThu; }
        }

        private decimal? _TongNo;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TongNo", DbType = "money")]
        public decimal? TongNo
        {
            get { return this.NoTruoc + this.ConLai; }
        }

        private string _Status;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Status", DbType = "NVarChar(50)")]
        public string Status
        {
            get { return (this.TongNo ?? 0) > 0 ? "Chưa thanh toán" : "Đã thanh toán"; }
        }
    }

    public partial class PhiBaoTri_selectBySuperAdminDebtResult
    {
        private decimal? _TongNo;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TongNo", DbType = "money")]
        public decimal? TongNo
        {
            get { return this.NoTruoc + this.ConLai; }
        }

        private string _Status;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Status", DbType = "NVarChar(50)")]
        public string Status
        {
            get { return (this.TongNo ?? 0) > 0 ? "Chưa thanh toán" : "Đã thanh toán"; }
        }
    }

    public partial class PhiVeSinh_selectBySuperAdminResult
    {
        private decimal? _TongNo;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TongNo", DbType = "money")]
        public decimal? TongNo
        {
            get { return this.NoTruoc + this.ConLai; }
        }

        private string _Status;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Status", DbType = "NVarChar(50)")]
        public string Status
        {
            get { return (this.TongNo ?? 0) > 0 ? "Chưa thanh toán" : "Đã thanh toán"; }
        }
    }

    public partial class PhiVeSinh_selectBySuperAdminV2Result
    {
        private decimal? _ConLai;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ConLai", DbType = "money")]
        public decimal? ConLai
        {
            get { return this.PhiQL - this.DaThu; }
        }

        private decimal? _TongNo;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TongNo", DbType = "money")]
        public decimal? TongNo
        {
            get { return this.NoTruoc + this.ConLai; }
        }

        private string _Status;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Status", DbType = "NVarChar(50)")]
        public string Status
        {
            get { return (this.TongNo ?? 0) > 0 ? "Chưa thanh toán" : "Đã thanh toán"; }
        }
    }

    public partial class PhiVeSinh_selectBySuperAdminDebtResult
    {
        private decimal? _ConLai;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ConLai", DbType = "money")]
        public decimal? ConLai
        {
            get { return this.PhiQL - this.DaThu; }
        }

        private decimal? _TongNo;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TongNo", DbType = "money")]
        public decimal? TongNo
        {
            get { return this.NoTruoc + this.ConLai; }
        }

        private string _Status;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Status", DbType = "NVarChar(50)")]
        public string Status
        {
            get { return (this.TongNo ?? 0) > 0 ? "Chưa thanh toán" : "Đã thanh toán"; }
        }
    }

    public partial class dvgxTheXe_selectBySuperAdminResult
    {
        private decimal? _TongNo;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TongNo", DbType = "money")]
        public decimal? TongNo
        {
            get { return this.NoTruoc + this.ConLai; }
        }

        private string _Status;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Status", DbType = "NVarChar(50)")]
        public string Status
        {
            get { return (this.TongNo ?? 0) > 0 ? "Chưa thanh toán" : "Đã thanh toán"; }
        }
    }

    public partial class GiuXe_selectResult
    {
        private decimal? _ConLai;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ConLai", DbType = "money")]
        public decimal? ConLai
        {
            get { return this.PhiQL - this.DaThu; }
        }

        private decimal? _TongNo;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TongNo", DbType = "money")]
        public decimal? TongNo
        {
            get { return this.NoTruoc + this.ConLai; }
        }
    }

    public partial class HoBoi_selectResult
    {
        private decimal? _ConLai;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ConLai", DbType = "money")]
        public decimal? ConLai
        {
            get { return this.PhiQL - this.DaThu; }
        }

        private decimal? _TongNo;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TongNo", DbType = "money")]
        public decimal? TongNo
        {
            get { return this.NoTruoc + this.ConLai; }
        }
    }

    public partial class GiuXe_selectDebtResult
    {
        private decimal? _TongNo;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TongNo", DbType = "money")]
        public decimal? TongNo
        {
            get { return this.NoTruoc + this.ConLai; }
        }

        private string _Status;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Status", DbType = "NVarChar(50)")]
        public string Status
        {
            get { return (this.TongNo ?? 0) > 0 ? "Chưa thanh toán" : "Đã thanh toán"; }
        }
    }

    public partial class HoBoi_selectDebtResult
    {
        private decimal? _TongNo;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TongNo", DbType = "money")]
        public decimal? TongNo
        {
            get { return this.NoTruoc + this.ConLai; }
        }

        private string _Status;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Status", DbType = "NVarChar(50)")]
        public string Status
        {
            get { return (this.TongNo ?? 0) > 0 ? "Chưa thanh toán" : "Đã thanh toán"; }
        }
    }
}
