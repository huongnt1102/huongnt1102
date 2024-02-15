using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraEditors;
using System.Drawing;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Data.Linq.SqlClient;
using System.Data;
using System.ComponentModel;
namespace Library
{
    public static class Common
    {
        public static tnNhanVien User { get; set; }

        public static List<ToaNhaItem> TowerList;
        public static void InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            DialogBox.Error(e.ErrorText);
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }


        public static void GridViewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (DialogBox.QuestionDelete() == System.Windows.Forms.DialogResult.Yes)
                {
                    var gc = (sender as DevExpress.XtraGrid.GridControl);
                    var gv = (DevExpress.XtraGrid.Views.Grid.GridView)gc.MainView;
                    gv.DeleteSelectedRows();
                }
            }
        }

        public static void GridViewCustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.Caption == "STT" && e.ListSourceRowIndex >= 0)
            {
                e.DisplayText = (e.ListSourceRowIndex + 1).ToString();
            }
        }

        public static bool Duplication(DevExpress.XtraGrid.Views.Grid.GridView grv, int rowIndex, string fileName, string value)
        {
            for (int i = 0; i < grv.RowCount; i++)
            {
                if (i != rowIndex && grv.GetRowCellDisplayText(i, fileName) == value)
                {
                    return true;
                }
            }
            return false;
        }

        public static DateTime GetFirstDayOfMonth(DateTime dtInput)
        {
            DateTime dtResult = dtInput;
            dtResult = dtResult.AddDays((-dtResult.Day) + 1);
            return dtResult;
        }

        public static DateTime GetLastDayOfMonth(DateTime dtInput)
        {
            DateTime dtResult = dtInput;
            dtResult = dtResult.AddMonths(1);
            dtResult = dtResult.AddDays(-(dtResult.Day));
            return dtResult;
        }

        public static DateTime GetLastDayOfMonth(int iMonth, int iYear)
        {
            DateTime dtResult = new DateTime(iYear, iMonth, 1);
            dtResult = dtResult.AddMonths(1);
            dtResult = dtResult.AddDays(-(dtResult.Day));
            return dtResult;
        }

        public static void SoQuy_Insert(MasterDataContext db, int? Thang, int? Nam, byte? MaTN, int? MaKH, int? MaMB, int? IdPhieu, int? IdPhieuChiTiet, DateTime? NgayPhieu, string SoPhieu, int? HinhThucThuChi, int? MaLoaiPhieu, bool? IsPhieuThu, decimal? PhaiThu, decimal? DaThu, decimal? ThuThua, decimal? KhauTru, long? LinkID, string TableName, string DienGiai, int? MaNV,bool IsKhauTru, bool IsDvApp)
        {
            SoQuy_ThuChi item = new SoQuy_ThuChi();
            item.Thang = Thang;
            item.Nam = Nam;
            item.MaTN = MaTN;
            item.MaKH = MaKH;
            item.MaMB = MaMB;
            item.IDPhieu = IdPhieu;
            item.IDPhieuChiTiet = IdPhieuChiTiet;
            item.NgayPhieu = NgayPhieu;
            item.SoPhieu = SoPhieu;
            item.HinhThucThuChi = HinhThucThuChi;
            item.MaLoaiPhieu = MaLoaiPhieu;//1: Thu dịch vụ -- 2: Khấu trừ thu trước -- 3: Thu trước
            item.IsPhieuThu = IsPhieuThu;
            item.PhaiThu = PhaiThu;
            item.DaThu = DaThu;
            item.ThuThua = ThuThua;
            item.KhauTru = KhauTru;
            item.LinkID = LinkID;
            item.TableName = TableName;
            item.DienGiai = DienGiai;
            item.MaNV = MaNV;
            item.IsKhauTru = IsKhauTru;
            item.IsDvApp = IsDvApp;
            db.SoQuy_ThuChis.InsertOnSubmit(item);
            db.SubmitChanges();
        }
        public static void SoQuy_InsertCompanyCode(MasterDataContext db, int? Thang, int? Nam, byte? MaTN, int? MaKH, int? MaMB, int? IdPhieu, int? IdPhieuChiTiet, DateTime? NgayPhieu, string SoPhieu, int? HinhThucThuChi, int? MaLoaiPhieu, bool? IsPhieuThu, decimal? PhaiThu, decimal? DaThu, decimal? ThuThua, decimal? KhauTru, long? LinkID, string TableName, string DienGiai, int? MaNV,bool IsKhauTru, bool IsDvApp, int? companyCode)
        {
            SoQuy_ThuChi item = new SoQuy_ThuChi();
            item.Thang = Thang;
            item.Nam = Nam;
            item.MaTN = MaTN;
            item.CompanyCode = companyCode;
            item.MaKH = MaKH;
            item.MaMB = MaMB;
            item.IDPhieu = IdPhieu;
            item.IDPhieuChiTiet = IdPhieuChiTiet;
            item.NgayPhieu = NgayPhieu;
            item.SoPhieu = SoPhieu;
            item.HinhThucThuChi = HinhThucThuChi;
            item.MaLoaiPhieu = MaLoaiPhieu;//1: Thu dịch vụ -- 2: Khấu trừ thu trước -- 3: Thu trước
            item.IsPhieuThu = IsPhieuThu;
            item.PhaiThu = PhaiThu;
            item.DaThu = DaThu;
            item.ThuThua = ThuThua;
            item.KhauTru = KhauTru;
            item.LinkID = LinkID;
            item.TableName = TableName;
            item.DienGiai = DienGiai;
            item.MaNV = MaNV;
            item.IsKhauTru = IsKhauTru;
            item.IsDvApp = IsDvApp;
            db.SoQuy_ThuChis.InsertOnSubmit(item);
            db.SubmitChanges();
        }
        public static void SoQuy_InsertIsKhauTruTuDong(MasterDataContext db, int? Thang, int? Nam, byte? MaTN, int? MaKH, int? MaMB, int? IdPhieu, int? IdPhieuChiTiet, DateTime? NgayPhieu, string SoPhieu, int? HinhThucThuChi, int? MaLoaiPhieu, bool? IsPhieuThu, decimal? PhaiThu, decimal? DaThu, decimal? ThuThua, decimal? KhauTru, long? LinkID, string TableName, string DienGiai, int? MaNV, bool IsKhauTru,bool IsKhauTruTuDong, bool IsDvApp)
        {
            SoQuy_ThuChi item = new SoQuy_ThuChi();
            item.Thang = Thang;
            item.Nam = Nam;
            item.MaTN = MaTN;
            item.MaKH = MaKH;
            item.MaMB = MaMB;
            item.IDPhieu = IdPhieu;
            item.IDPhieuChiTiet = IdPhieuChiTiet;
            item.NgayPhieu = NgayPhieu;
            item.SoPhieu = SoPhieu;
            item.HinhThucThuChi = HinhThucThuChi;
            item.MaLoaiPhieu = MaLoaiPhieu;//1: Thu dịch vụ -- 2: Khấu trừ thu trước -- 3: Thu trước
            item.IsPhieuThu = IsPhieuThu;
            item.PhaiThu = PhaiThu;
            item.DaThu = DaThu;
            item.ThuThua = ThuThua;
            item.KhauTru = KhauTru;
            item.LinkID = LinkID;
            item.TableName = TableName;
            item.DienGiai = DienGiai;
            item.MaNV = MaNV;
            item.IsKhauTru = IsKhauTru;
            item.IsKhauTruTuDong = IsKhauTruTuDong;
            item.IsDvApp = IsDvApp;
            db.SoQuy_ThuChis.InsertOnSubmit(item);
            db.SubmitChanges();
        }
        public static DateTime GetDateTimeSystem()
        {
            return DateTime.UtcNow.AddHours(7);
        }
        public static decimal GetTotalMonth(DateTime fromDate, DateTime toDate)
        {
            decimal _Month = 0;
            while (fromDate.CompareTo(toDate) < 0)
            {
                var _Date = fromDate.AddMonths(1).AddDays(-1);
                if (_Date.CompareTo(toDate) > 0)
                {
                    //_Month += Convert.ToDecimal(Math.Round((toDate - fromDate).TotalDays / 30, 1));

                    //DateTime dtResult = new DateTime(toDate.Year, toDate.Month, 1);
                    //dtResult = dtResult.AddMonths(1);
                    //dtResult = dtResult.AddDays(-(dtResult.Day));

                    _Month += Convert.ToDecimal(Math.Round((toDate - fromDate).TotalDays / 30, 1));
                    fromDate = toDate;
                }
                else
                {
                    _Month += 1;
                    fromDate = _Date.AddDays(1);
                }
            }

            return _Month;
        }

        public static decimal GetTotalOneMonth(DateTime fromDate, DateTime toDate)
        {
            decimal _Month = 0;
            while (fromDate.CompareTo(toDate) < 0)
            {
                var _Date = fromDate.AddMonths(1).AddDays(-1);
                //if (_Date.CompareTo(toDate) > 0)
                //{
                    //_Month += Convert.ToDecimal(Math.Round((toDate - fromDate).TotalDays / 30, 1));

                    DateTime dtResult = new DateTime(toDate.Year, toDate.Month, 1);
                    dtResult = dtResult.AddMonths(1);
                    dtResult = dtResult.AddDays(-(dtResult.Day));

                //_Month += Convert.ToDecimal(Math.Round(((toDate - fromDate).TotalDays + 1) / dtResult.Day, 4));
                _Month += Convert.ToDecimal(Math.Round((decimal)((toDate.Date - fromDate.Date).Days + 1) / dtResult.Day, 4));
                fromDate = toDate;
                //}
                //else
                //{
                //    _Month += 1;
                //    fromDate = _Date.AddDays(1);
                //}
            }

            return _Month;
        }

        public static string IPWan { get; set; }
        public static string IPLan { get; set; }
        public static string MacAddress { get; set; }
        public static string DeviceName { get; set; }
        public static bool IsNumber(string pText)
        {
            Regex regex = new Regex(@"^[-+]?[0-9]*\.?[0-9]+$");  
            return regex.IsMatch(pText);
        }
        public static string CreatePhieuThu(int iLoaiPhieu, int Thang,int Nam, int KhoiNha, byte MaTN, bool IsKhauTru)
        {
            MasterDataContext db=new MasterDataContext(); 
            string _temp = "";
            string khoiNhaNull = "";
            string toaNhaNull = "";
            if (IsKhauTru == false)
            {
                if (iLoaiPhieu == 0)
                {
                    _temp = "PT";
                }
                else
                {
                    _temp = "BC";
                }
                string STT = "";
                if (iLoaiPhieu == 0)
                {
                    var objPT = (from pt in db.ptPhieuThus
                                 join mb in db.mbMatBangs on pt.MaMB equals mb.MaMB
                                 join tl in db.mbTangLaus on mb.MaTL equals tl.MaTL
                                 join kn in db.mbKhoiNhas on tl.MaKN equals kn.MaKN
                                 where pt.NgayThu.Value.Month == Thang && pt.NgayThu.Value.Year == Nam && kn.MaTN == MaTN && pt.MaTKNH == null && SqlMethods.Like(pt.SoPT.Substring(2, 3), "[0-9][0-9][0-9]") && pt.IsKhauTru == false
                                 orderby Convert.ToInt32(pt.SoPT.Substring(2, 3)) descending
                                 select new
                                 {
                                     STT = Convert.ToInt32(pt.SoPT.Substring(2, 3))
                                 }).FirstOrDefault();
                    if (objPT == null)
                    {
                        STT = "001";
                    }
                    else
                    {
                        STT = (objPT.STT + 1) < 10 ? "00" + (objPT.STT + 1).ToString() : (objPT.STT + 1) < 100 ? "0" + (objPT.STT + 1).ToString() : (objPT.STT + 1).ToString();
                    }
                    var objKN = db.mbKhoiNhas.FirstOrDefault(p => p.MaKN == KhoiNha);
                    var objTN = db.tnToaNhas.FirstOrDefault(p => p.MaTN == MaTN);
                    string sTenKhoiNha = string.IsNullOrEmpty(objKN != null ? objKN.MaVT : khoiNhaNull) ? "" : "." + (objKN != null ? objKN.MaVT.Trim() : khoiNhaNull);
                    _temp = _temp + STT + "." + (Thang < 10 ? "0" + Thang.ToString() : Thang.ToString()) + "." + (objTN != null ? objTN.TenVT : toaNhaNull) + sTenKhoiNha;
                }
                else
                {
                    var objPT = (from pt in db.ptPhieuThus
                                 join mb in db.mbMatBangs on pt.MaMB equals mb.MaMB
                                 join tl in db.mbTangLaus on mb.MaTL equals tl.MaTL
                                 join kn in db.mbKhoiNhas on tl.MaKN equals kn.MaKN
                                 where pt.NgayThu.Value.Month == Thang && pt.NgayThu.Value.Year == Nam && kn.MaTN == MaTN && pt.MaTKNH != null && SqlMethods.Like(pt.SoPT.Substring(2, 3), "[0-9][0-9][0-9]") && pt.IsKhauTru == false
                                 orderby Convert.ToInt32(pt.SoPT.Substring(2, 3)) descending
                                 select new
                                 {
                                     STT = Convert.ToInt32(pt.SoPT.Substring(2, 3))
                                 }).FirstOrDefault();
                    if (objPT == null)
                    {
                        STT = "001";
                    }
                    else
                    {
                        STT = (objPT.STT + 1) < 10 ? "00" + (objPT.STT + 1).ToString() : (objPT.STT + 1) < 100 ? "0" + (objPT.STT + 1).ToString() : (objPT.STT + 1).ToString();
                    }
                    var objKN = db.mbKhoiNhas.FirstOrDefault(p => p.MaKN == KhoiNha);
                    var objTN = db.tnToaNhas.FirstOrDefault(p => p.MaTN == MaTN);
                    string sTenKhoiNha = string.IsNullOrEmpty(objKN != null ? objKN.MaVT : khoiNhaNull) ? "" : "." + (objKN != null ? objKN.MaVT.Trim() : khoiNhaNull);
                    _temp = _temp + STT + "." + (Thang < 10 ? "0" + Thang.ToString() : Thang.ToString()) + "." + (objTN != null ? objTN.TenVT : toaNhaNull) + sTenKhoiNha;

                }
            }
            else
            {

                _temp = "KT";
                string STT = "";
                var objPT = (from pt in db.ptPhieuThus
                             join mb in db.mbMatBangs on pt.MaMB equals mb.MaMB
                             join tl in db.mbTangLaus on mb.MaTL equals tl.MaTL
                             join kn in db.mbKhoiNhas on tl.MaKN equals kn.MaKN
                             where pt.NgayThu.Value.Month == Thang && pt.NgayThu.Value.Year == Nam && kn.MaTN == MaTN && pt.MaTKNH == null && SqlMethods.Like(pt.SoPT.Substring(2, 3), "[0-9][0-9][0-9]") && pt.IsKhauTru == true
                             orderby Convert.ToInt32(pt.SoPT.Substring(2, 3)) descending
                             select new
                             {
                                 STT = Convert.ToInt32(pt.SoPT.Substring(2, 3))
                             }).FirstOrDefault();
                if (objPT == null)
                {
                    STT = "001";
                }
                else
                {
                    STT = (objPT.STT + 1) < 10 ? "00" + (objPT.STT + 1).ToString() : (objPT.STT + 1) < 100 ? "0" + (objPT.STT + 1).ToString() : (objPT.STT + 1).ToString();
                }
                var objKN = db.mbKhoiNhas.FirstOrDefault(p => p.MaKN == KhoiNha);
                var objTN = db.tnToaNhas.FirstOrDefault(p => p.MaTN == MaTN);
                string sTenKhoiNha = string.IsNullOrEmpty(objKN != null ? objKN.MaVT : khoiNhaNull) ? "" : "." + (objKN != null ? objKN.MaVT.Trim() : khoiNhaNull);
                _temp = _temp + STT + "." + (Thang < 10 ? "0" + Thang.ToString() : Thang.ToString()) + "." + (objTN != null ? objTN.TenVT : toaNhaNull) + sTenKhoiNha;

            }
            return _temp;
        }

        public static string GetPayNumber(int? Type, byte? TowerId, int? BankId)
        {
            string No = "";
            string payNumber = "";
            string kyHieu = "{0000}";

            switch(Type)
            {
                case 1: // Chuyển khoản
                    No = "BC";
                    break;
                case 2: // khấu trừ
                    No = "KT";
                    break;
                default: // Mặc định là tiền mặt
                    No = "PT";
                    kyHieu = "{000}";
                    break;
            }

            var payNumberObj = Library.Class.Connect.QueryConnect.QueryData<string>("a_ptphieuthu_getnewno_1", new
            {
                KY_HIEU = kyHieu,
                SO_PT = No,
                matn = TowerId,
                MaTKNH = BankId
            });

            if(payNumberObj.Count() > 0)
            {
                payNumber = payNumberObj.FirstOrDefault();
            }

            return payNumber;
        }

        public static string GetCustomerCode(byte? TowerId)
        {
            try
            {
                string customerCode = "";
                string kyHieu = "{0000000000}";

                var customerCodeObj = Library.Class.Connect.QueryConnect.QueryData<string>("a_tnkhachhang_getnewno_1", new
                {
                    KY_HIEU = kyHieu,
                    matn = TowerId
                });

                if (customerCodeObj.Count() > 0)
                {
                    customerCode = customerCodeObj.FirstOrDefault();
                }

                return customerCode;
            }
            catch
            {
                //DialogBox.Error("Tòa nhà có khách hàng đặt sai quy tắc đặt mã, vui lòng kiểm tra lại!");
                //return "";

                string customerCode = "";
                string kyHieu = "{0000000000}";

                var customerCodeObj = Library.Class.Connect.QueryConnect.QueryData<string>("a_tnkhachhang_getnewno_2", new
                {
                    KY_HIEU = kyHieu,
                    matn = TowerId
                });

                if (customerCodeObj.Count() > 0)
                {
                    customerCode = customerCodeObj.FirstOrDefault();
                }

                return customerCode;
            }
            
        }

        public static string CreatePhieuChi(byte MaTN, int Thang, int Nam)
        {
            MasterDataContext db = new MasterDataContext();
            string _temp = "PC";
            var objPC=(from pc in db.pcPhieuChis
                       where pc.MaTN==MaTN && pc.NgayChi.Value.Month==Thang && pc.NgayChi.Value.Year==Nam && SqlMethods.Like(pc.SoPC.Substring(2,3), "[0-9][0-9][0-9]")
                       orderby Convert.ToInt32(pc.SoPC.Substring(2, 3)) descending
                       select new
                       {
                           STT = Convert.ToInt32(pc.SoPC.Substring(2, 3))
                       }).FirstOrDefault();
            if (objPC == null)
            {
                _temp =_temp+ "001";
            }
            else
            {
                _temp = _temp + ((objPC.STT + 1) < 10 ? "00" + (objPC.STT + 1).ToString() : (objPC.STT + 1) < 100 ? "0" + (objPC.STT + 1).ToString() : (objPC.STT + 1).ToString());
            }
            var objTN = db.tnToaNhas.FirstOrDefault(p => p.MaTN == MaTN);
            _temp = _temp+ "." + (Thang < 10 ? "0" + Thang.ToString() : Thang.ToString()) + "." + objTN.TenVT;
            return _temp;
        }
        public static string CreatePhieuChuyenTien(int Thang,int Nam, byte MaTN)
        {
            MasterDataContext db = new MasterDataContext();
            string _temp = "";
            _temp = "PCT";
            string STT = "";
            var objPT = (from pt in db.PhieuChuyenTiens
                         where pt.NgayCT.Value.Month == Thang && pt.NgayCT.Value.Year==Nam && pt.MaTN == MaTN  && SqlMethods.Like(pt.SoCT.Substring(3, 3), "[0-9][0-9][0-9]")
                         orderby Convert.ToInt32(pt.SoCT.Substring(3, 3)) descending
                         select new
                         {
                             STT = Convert.ToInt32(pt.SoCT.Substring(3, 3))
                         }).FirstOrDefault();
            if (objPT == null)
            {
                STT = "001";
            }
            else
            {
                STT = (objPT.STT + 1) < 10 ? "00" + (objPT.STT + 1).ToString() : (objPT.STT + 1) < 100 ? "0" + (objPT.STT + 1).ToString() : (objPT.STT + 1).ToString();
            }
            var objTN = db.tnToaNhas.FirstOrDefault(p => p.MaTN == MaTN);
            _temp = _temp + STT + "." + (Thang < 10 ? "0" + Thang.ToString() : Thang.ToString()) + "." + objTN.TenVT ;
            return _temp;
        }
        public static string CreatePhieuKyQuy(int Thang,int Nam, byte MaTN)
        {
            MasterDataContext db = new MasterDataContext();
            string _temp = "";
            _temp = "PCKQ";
            string STT = "";
            var objPT = (from pt in db.pcPhieuChi_TraLaiKhachHangs
                         join p in db.ptPhieuThus on pt.MaPT equals p.ID
                         where pt.NgayChi.Value.Month == Thang && pt.NgayChi.Value.Year==Nam && p.MaTN == MaTN && SqlMethods.Like(pt.SoPhieuChi.Substring(4, 3), "[0-9][0-9][0-9]")
                         orderby Convert.ToInt32(pt.SoPhieuChi.Substring(4, 3)) descending
                         select new
                         {
                             STT = Convert.ToInt32(pt.SoPhieuChi.Substring(4, 3))
                         }).FirstOrDefault();
            if (objPT == null)
            {
                STT = "001";
            }
            else
            {
                STT = (objPT.STT + 1) < 10 ? "00" + (objPT.STT + 1).ToString() : (objPT.STT + 1) < 100 ? "0" + (objPT.STT + 1).ToString() : (objPT.STT + 1).ToString();
            }
            var objTN = db.tnToaNhas.FirstOrDefault(p => p.MaTN == MaTN);
            _temp = _temp + STT + "." + (Thang < 10 ? "0" + Thang.ToString() : Thang.ToString()) + "." + objTN.TenVT;
            return _temp;
        }
        public static string CreateKeHoachVanHanh(byte MaTN, int MaNhomTaiSan, int Nam)
        {
            string sSoKeHoach = "";
            using (var db = new MasterDataContext())
            {
                string STT = "";
                var objKH = (from kh in db.tbl_KeHoachVanHanhs
                             where kh.MaTN == MaTN && kh.NhomTaiSanID == MaNhomTaiSan && kh.NgayLapKeHoach.Value.Year == Nam
                             && SqlMethods.Like(kh.SoKeHoach.Substring(kh.SoKeHoach.IndexOf('-', 0) + 1, 4), "[0-9][0-9][0-9][0-9]")
                             orderby Convert.ToInt32(kh.SoKeHoach.Substring(kh.SoKeHoach.IndexOf('-', 0) + 1, 4)) descending
                             select new
                             {
                                 STT = Convert.ToInt32(kh.SoKeHoach.Substring(kh.SoKeHoach.IndexOf('-', 0) + 1, 4))
                             }).FirstOrDefault();
                if (objKH == null)
                {
                    STT = "0001";
                }
                else
                {

                    STT = (objKH.STT + 1).ToString().PadLeft(4, '0');
                }
                var objNTS = db.tbl_NhomTaiSans.FirstOrDefault(p => p.ID == MaNhomTaiSan);
                sSoKeHoach = objNTS.TenVietTat + "-" + STT;
            }
            return sSoKeHoach;
        }
        public static DataTable ConvertToDataTable<T>(this List<T> iList)
        {
            DataTable dataTable = new DataTable();
            PropertyDescriptorCollection propertyDescriptorCollection =
            TypeDescriptor.GetProperties(typeof(T));
            for (int i = 0; i < propertyDescriptorCollection.Count; i++)
            {
                PropertyDescriptor propertyDescriptor = propertyDescriptorCollection[i];
                Type type = propertyDescriptor.PropertyType;

                if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                    type = Nullable.GetUnderlyingType(type);


                dataTable.Columns.Add(propertyDescriptor.Name, type);
            }
            object[] values = new object[propertyDescriptorCollection.Count];
            foreach (T iListItem in iList)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = propertyDescriptorCollection[i].GetValue(iListItem);
                }
                dataTable.Rows.Add(values);
            }
            return dataTable;
        }
        public static List<T> DataTableToList<T>(this DataTable table) where T : class, new()
        {
            try
            {
                List<T> list = new List<T>();

                foreach (var row in table.AsEnumerable())
                {
                    T obj = new T();

                    foreach (var prop in obj.GetType().GetProperties())
                    {
                        try
                        {
                            var propType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                            var safeValue = row[prop.Name] == null ? null : Convert.ChangeType(row[prop.Name], propType);

                            prop.SetValue(obj, safeValue, null);
                        }
                        catch
                        {
                            continue;
                        }
                    }

                    list.Add(obj);
                }

                return list;
            }
            catch
            {
                return null;
            }
        }
         public static string TaoMa(List<string> o, string chuoiMacDinh)
        {
            string maSo;
            o = o.OrderByDescending(s =>
            {
                try { return int.Parse(s.Split('/').Last()); }
                catch { return 0; }
            }).ToList();
            var maPhieuCu = o.FirstOrDefault(); // xyz/...01
            if (maPhieuCu != null)
            {
                int match;
                try
                {
                    match = int.Parse(maPhieuCu.Split('/').Last()); // 001
                }
                catch // không có số nào sau dấu / hoặc không có dấu /
                {
                    match = 0;
                }

                maSo = maPhieuCu.Split('/').First() + "/" + (match + 1).ToString().PadLeft(maPhieuCu.Split('/').Last().Count(), '0');
            }
            else
            {
                maSo = chuoiMacDinh + (1).ToString().PadLeft(4, '0');
            }

            return maSo;
        }
         public static string HashPassword(string OriginalString)
         {
             //Declarations
             Byte[] originalBytes;
             Byte[] encodedBytes;
             System.Security.Cryptography.MD5 md5;

             //Instantiate MD5CryptoServiceProvider, get bytes for original password and compute hash (encoded password)
             md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
             originalBytes = ASCIIEncoding.Default.GetBytes(OriginalString);
             encodedBytes = md5.ComputeHash(originalBytes);

             StringBuilder sb = new StringBuilder();
             for (int i = 0; i < encodedBytes.Length; i++)
             {
                 sb.Append(encodedBytes[i].ToString("X2"));
             }

             //Convert encoded bytes back to a 'readable' string
             return sb.ToString();
         }
         public static IEnumerable<IEnumerable<T>> GetEnumerableOfEnumerables<T>(
             IEnumerable<T> enumerable, int groupSize)
         {
             // The list to return.
             List<T> list = new List<T>(groupSize);

             // Cycle through all of the items.
             foreach (T item in enumerable)
             {
                 // Add the item.
                 list.Add(item);

                 // If the list has the number of elements, return that.
                 if (list.Count == groupSize)
                 {
                     // Return the list.
                     yield return list;

                     // Set the list to a new list.
                     list = new List<T>(groupSize);
                 }
             }

             // Return the remainder if there is any,
             if (list.Count != 0)
             {
                 // Return the list.
                 yield return list;
             }
         }
    }

    public enum MaLDVs : int
    {
        PQL = 13,
        PGX = 6,
        PVS = 14,
        Nuoc = 9,
        Gas = 10,
        Dien = 8,
        TienLai = 23,

    }
    public class InfoCusSendMail
    {
        public int MaKH { get; set; }
        public int MaMB { get; set; }
    }

}
