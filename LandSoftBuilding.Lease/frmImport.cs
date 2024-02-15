using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Data.Linq.SqlClient;
using Library;
using System.Linq;

namespace LandSoftBuilding.Lease
{
    public partial class frmImport : DevExpress.XtraEditors.XtraForm
    {
        public byte MaTN { get; set; }
        public bool isSave { get; set; }

        public frmImport()
        {
            InitializeComponent();
        }

        bool IsSoNguyen(decimal val)
        {
            try
            {
                Convert.ToInt32(val);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void itemChoice_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var file = new OpenFileDialog();
            try
            {
                file.Filter = "(Excel file)|*.xls;*.xlsx";
                file.ShowDialog(); 
                if (file.FileName == "") return;

                var excel = new LinqToExcel.ExcelQueryFactory(file.FileName);
                var sheets = excel.GetWorksheetNames();
                cmbSheet.Items.Clear();
                foreach (string s in sheets)
                    cmbSheet.Items.Add(s.Trim('$'));

                itemSheet.EditValue = null;
                this.Tag = file.FileName;
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
            finally
            { file.Dispose(); }
        }

        private void itemSheet_EditValueChanged(object sender, EventArgs e)
        {
            if (itemSheet.EditValue == null)
                gcHD.DataSource = null;
            else
                try
                {
                    var excel = new LinqToExcel.ExcelQueryFactory(this.Tag.ToString());

                    System.Collections.Generic.List<HopDongThueItem> list = Library.Import.ExcelAuto.ConvertDataTable<HopDongThueItem>(Library.Import.ExcelAuto.GetDataExcel(excel, grvHD, itemSheet));

                    gcHD.DataSource = list;

                    excel = null;
                }
                catch (Exception ex)
                {
                    DialogBox.Error(ex.Message);
                }
        }

        private int ConvertInt(string val)
        {
            try
            {
                var Int = Convert.ToInt32(val);
                return Int;
            }
            catch
            {
                return 0;
            }
        }
        private decimal Convertdecimal(string val)
        {
            try
            {
                var Int = Convert.ToDecimal(val);
                return Int;
            }
            catch
            {
                return 0;
            }
        }
        private DateTime ConvertDate(string value)
        {
            try
            {
                //return value.Cast<DateTime>();
                // return DateTime.FromOADate(Convert.ToInt64(value));
                return Convert.ToDateTime(value);
            }
            catch
            {
                return DateTime.Now;
            }
        }

        private void itemClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void itemDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DialogBox.QuestionDelete() == System.Windows.Forms.DialogResult.No) return;
            grvHD.DeleteSelectedRows();
        }

        private void itemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gcHD.DataSource == null)
            {
                DialogBox.Error("Vui lòng chọn sheet");
                return;
            }

            var wait = DialogBox.WaitingForm();
            var db = new MasterDataContext();
            try
            {
                var ltKhachHang = (from kh in db.tnKhachHangs where kh.MaTN == this.MaTN select new { kh.MaKH, kh.KyHieu }).ToList();
                var ltMatBang = (from p in db.mbMatBangs where p.MaTN == this.MaTN select new { p.MaMB, p.MaSoMB }).ToList();
                var ltLoaiGiaThue = (from p in db.LoaiGiaThues where p.MaTN == MaTN select new { p.ID, p.TenLG }).ToList();
                var ltLoaiTien = (from p in db.LoaiTiens select new { p.ID, p.KyHieuLT, p.TyGia }).ToList();

                #region "   Rang buoc du lieu"
                if (ltKhachHang.Count <= 0)
                {
                    DialogBox.Alert("Vui lòng thêm [Khách hàng] cho Dự án này trước.");
                    return;
                }

                if (ltMatBang.Count <= 0)
                {
                    DialogBox.Alert("Vui lòng thêm [Mặt bằng] cho Dự án này trước.");
                    return;
                }

                if (ltLoaiGiaThue.Count <= 0)
                {
                    DialogBox.Alert("Vui lòng thêm [Loại giá thuê] cho Dự án này trước.");
                    return;
                }

                if (ltLoaiTien.Count <= 0)
                {
                    DialogBox.Alert("Vui lòng thêm [Loại tiền] cho Dự án này trước.");
                    return;
                }
                #endregion

                var ltHopDong = (List<HopDongThueItem>)gcHD.DataSource;
                var ltError = new List<HopDongThueItem>();
                foreach (var n in ltHopDong)
                {
                    db = new MasterDataContext();
                    try
                    {
                        #region "   Rang buoc du lieu"
                        var objKH = ltKhachHang.FirstOrDefault(p => p.KyHieu.ToLower() == n.MaSoKH.ToLower());
                        if (objKH == null)
                        {
                            n.Error = "Khách hàng này không tồn tại trong hệ thống";
                            ltError.Add(n);
                            continue;
                        }

                        var objMB = ltMatBang.FirstOrDefault(p => p.MaSoMB.ToLower() == n.MaSoMB.ToLower());
                        if (objMB == null)
                        {
                            n.Error = "Mặt bằng này không tồn tại trong hệ thống";
                            ltError.Add(n);
                            continue;
                        }

                        var objLGT = ltLoaiGiaThue.FirstOrDefault(p => p.TenLG.ToLower() == n.LoaiGiaThue.ToLower());
                        if (objLGT == null)
                        {
                            n.Error = "Loại giá thuê này không tồn tại trong hệ thống";
                            ltError.Add(n);
                            continue;
                        }

                        var objLT = ltLoaiTien.FirstOrDefault(p => p.KyHieuLT.ToLower() == n.TenLT.ToLower());
                        if (objLT == null)
                        {
                            n.Error = "Loại tiền này không tồn tại trong hệ thống";
                            ltError.Add(n);
                            continue;
                        }
                        #endregion

                        ctHopDong objHD;
                        int? ParentID = null;

                        if (n.SoPL == null || n.SoPL == "")
                        {
                            objHD = db.ctHopDongs.FirstOrDefault(p => p.MaTN == this.MaTN & p.SoHDCT.ToLower() == n.SoHD.ToLower());
                        }
                        else
                        {
                            objHD = db.ctHopDongs.FirstOrDefault(p => p.MaTN == this.MaTN & p.SoHDCT.ToLower() == n.SoHD.ToLower());
                            if (objHD == null)
                            {
                                n.Error = "Số hợp đồng này không tồn tại trong hệ thống";
                                ltError.Add(n);
                                continue;
                            }
                            else
                            {
                                ParentID = objHD.ID;
                                objHD = db.ctHopDongs.FirstOrDefault(p => p.MaTN == this.MaTN & p.SoHDCT.ToLower() == n.SoPL.ToLower());
                            }
                        }

                        if (objHD == null)
                            goto NhapMoi;
                        else
                            goto ChiTiet;

                     NhapMoi:
                        objHD = new ctHopDong();
                        objHD.MaTN = this.MaTN;
                        objHD.MaKH = objKH.MaKH;
                        objHD.IsPhuLuc = ParentID == null ? false : true;
                        objHD.ParentID = ParentID;
                        


                        db.ctHopDongs.InsertOnSubmit(objHD);
                        db.SubmitChanges();

                    ChiTiet:

                        objHD.SoHDCT = ParentID == null ? n.SoHD : n.SoPL;
                        objHD.NgayKy = n.NgayKy;
                        objHD.NgayHL = n.NgayHL;
                        objHD.NgayHH = n.NgayHH;
                        objHD.ThoiHan = n.ThoiHan;
                        objHD.KyTT = Convert.ToInt32(n.KyThanhToan);
                        objHD.NgayBG = n.NgayBG.Value.Year < 1000 ? (DateTime?)null : n.NgayBG;
                        objHD.TyLeDCGT = n.TyLeDCTG;
                        objHD.SoNamDCGT = n.SoNamDCTG;
                        objHD.MucDCTG = n.MucDCTG;
                        objHD.MaLT = objLT.ID;
                        objHD.TyGia = objHD.TyGiaHD = n.TyGia ?? 1;
                        objHD.NgayNhap = db.GetSystemDate();
                        objHD.MaNVN = Common.User.MaNV;
                        objHD.NgungSuDung = false;
                        objHD.HanhDong = "IMPORT";

                        #region Một số trường mới bên Euro
                        objHD.LaiSuatNopCham = n.LaiSuatNopCham;
                        objHD.SoLuongXeMienPhi = n.SoLuongXeMienPhi;
                        objHD.IsMienLai = n.IsMienLai;
                        objHD.ThoiGianChoPhepNopCham = n.ThoiGianChoPhepNopCham;
                        objHD.SoNgayFree = n.SoNgayFree;
                        #endregion

                        #region 1 số trường mới bên bitexco

                        try
                        {
                            var objLoaiTien = db.LoaiTiens.FirstOrDefault(_ => _.KyHieuLT.ToLower() == n.LoaiTienNgoaiTe.ToLower());
                            if (objLoaiTien != null)
                            {
                                objHD.LoaiTienNgoaiTe = objLoaiTien.ID;
                            }

                            objHD.NhaThau = n.NhaThau;
                        }
                        catch { }
                        
                        objHD.TyGiaNgoaiTe = n.TyGiaNgoaiTe;
                        //objHD.GiaThueNgoaiTe = n.GiaThueNgoaiTe;
                        objHD.NgayHHBH = n.NgayHHBH;
                        objHD.GiayToLienQuan = n.GiayToLienQuan;
                        objHD.GhiChu = n.GhiChu;
                        objHD.DieuKhoanDacBiet = n.DieuKhoanDacBiet;
                        objHD.DieuKhoanBoSungGiamThue = n.DieuKhoanBoSungGiamThue;
                        objHD.XemXetLaiTienThue = n.XemXetLaiTienThue;
                        objHD.DonGiaTuongDuongUSD = n.DonGiaTuongDuongUSD;
                        objHD.TyGiaApDungBanDau = n.TyGiaApDungBanDau;
                        objHD.ThoiGIanThayDoiTyGIa = n.ThoiGIanThayDoiTyGIa;
                        objHD.CachThayDoiTyGia = n.CachThayDoiTyGia;
                        objHD.NgayTangGiaTiepTheo = n.NgayTangGiaTiepTheo;
                        objHD.NgayBatDauTinhTronKyThanhToan = n.NgayTinhTronKyThanhToan ?? n.NgayHL;

                        objHD.SoNgayGiaHanThanhToan = n.SoNgayGiaHanThanhToan;

                        db.SubmitChanges();

                        if (n.Loai == "CHOTHUE")
                        {
                            var objCT = new ctChiTiet();
                            objCT.MaMB = objMB.MaMB;
                            objCT.MaLG = objLGT.ID;
                            objCT.DienTich = n.DienTich;
                            objCT.DonGia = n.DonGia;
                            objCT.PhiDichVu = n.PhiDichVu;
                            objCT.PhiDieuHoaChieuSang = n.PhiDieuHoaChieuSang.GetValueOrDefault();
                            // _DienTich * (_DonGia + _PhiDichVu)
                            objCT.TongGiaThue = n.ThanhTien.GetValueOrDefault() == 0 
                                ? n.DienTich.GetValueOrDefault() * (n.DonGia.GetValueOrDefault() + n.PhiDichVu.GetValueOrDefault() + n.PhiDieuHoaChieuSang.GetValueOrDefault())
                                : n.ThanhTien.GetValueOrDefault();
                            objCT.TyLeVAT = n.TyLeVAT;
                            objCT.TienVAT = n.TienVAT.GetValueOrDefault() == 0 
                                ? objCT.TongGiaThue * n.TyLeVAT.GetValueOrDefault()
                                : n.TienVAT.GetValueOrDefault();
                            objCT.TyLeCK = n.TyLeCK;
                            objCT.TienCK = n.TienCK.GetValueOrDefault() == 0
                                ? objCT.TongGiaThue * n.TyLeCK.GetValueOrDefault()
                                : n.TienCK.GetValueOrDefault();
                            objCT.ThanhTien = objCT.TongGiaThue + objCT.TienVAT - objCT.TienCK;
                            objCT.PhiSuaChua = n.PhiSuaChua;
                            objCT.DienGiai = n.DienGiai;
                            objCT.NgungSuDung = false;
                            objCT.TuNgay = n.TuNgay;
                            objCT.DenNgay = n.DenNgay;
                            objCT.MaLoaiTien = objLT.ID;
                            objCT.NgayBDTaoLTT = n.TuNgay;
                            objCT.HanhDong = "IMPORT";
                            objCT.LoaiTienNgoaiTe = objHD.LoaiTienNgoaiTe;
                            objCT.TyGiaNgoaiTe = n.TyGiaNgoaiTe;
                            objCT.ThanhTienNgoaiTe = n.GiaThueNgoaiTe;
                            objHD.ctChiTiets.Add(objCT);

                            objHD.GiaThue = (objHD.GiaThue == null ? 0 : objHD.GiaThue) + objCT.ThanhTien; //objHD.GiaThue ?? 0 + 
                            objHD.GiaThueNgoaiTe = (objHD.GiaThueNgoaiTe == null ? 0 : objHD.GiaThueNgoaiTe) + (decimal?)n.GiaThueNgoaiTe;

                            //Lich thanh toan
                            //var _TuNgay = n.NgayHL;
                            //var _NgayHH = n.NgayHH;
                            var _TuNgay = (DateTime)n.TuNgay;
                            var _NgayHH = (DateTime)n.DenNgay;
                            int _DotTT = 0;

                            while (_TuNgay.CompareTo(_NgayHH) < 0)
                            {
                                _DotTT++;
                                decimal _KyTT = n.KyThanhToan;
                                var _DenNgay = _TuNgay.AddMonths(Convert.ToInt32(_KyTT)).AddDays(-1);

                                // Tách kỳ lẻ
                                // Nếu kỳ nhỏ hơn 1 năm thì sẽ tách kỳ đầu lẻ, còn ngược lại thì không tách, vẫn chạy như cũ
                                // và từ ngày không phải là đầu tháng
                                // tách kỳ lẻ, ví dụ lần 1, từ ngày = 16/3, đến ngày = 16/6
                                if (n.NgayTinhTronKyThanhToan != null)
                                {
                                    if (_TuNgay.Date <= ((DateTime)n.NgayTinhTronKyThanhToan).Date.AddDays(-1))
                                        _DenNgay = ((DateTime)n.NgayTinhTronKyThanhToan).AddDays(-1);
                                    _KyTT = Common.GetTotalMonth(_TuNgay, _DenNgay);
                                }

                                if (_DenNgay.CompareTo(_NgayHH) > 0)
                                {
                                    _DenNgay = _NgayHH;
                                    _KyTT = Common.GetTotalMonth(_TuNgay, _DenNgay);
                                }

                                if (_KyTT > 0)
                                {
                                    decimal? _TongTien = 0;
                                    if (_KyTT != n.KyThanhToan)
                                    {
                                        // Tính tiền kỳ lẻ,
                                        // lấy tiền từng tháng cộng lại
                                        // làm phần tính tiền trước, cái tháng lẻ, sẽ tách ra từng tháng lẻ
                                        DateTime fromDate = _TuNgay;
                                        DateTime toDate = _DenNgay;
                                        // Kỳ thanh toán con
                                        decimal? billingCycle = _KyTT, Ky = 0;
                                        // Tiền thanh toán
                                        decimal? pay = 0;


                                        while (fromDate.CompareTo(_DenNgay) < 0)
                                        {
                                            // tách ra từng tháng
                                            toDate = new DateTime(fromDate.Year, fromDate.Month, 1);
                                            toDate = toDate.AddMonths(1);
                                            toDate = toDate.AddDays(-(toDate.Day));

                                            if (toDate.CompareTo(_DenNgay) > 0) toDate = _DenNgay;

                                            billingCycle = Common.GetTotalOneMonth(fromDate, toDate);
                                            Ky += billingCycle;

                                            pay += billingCycle * objCT.ThanhTien;

                                            fromDate = toDate.AddDays(1);
                                        }

                                        _TongTien = pay;
                                        _KyTT = (decimal)Ky;
                                    }
                                    else
                                    {
                                        _KyTT = Common.GetTotalMonth(_TuNgay, _DenNgay);
                                        _TongTien = _KyTT * objCT.ThanhTien;
                                    }

                                    

                                    var objLTT = new ctLichThanhToan();
                                    objLTT.MaLDV = 2;
                                    objLTT.MaMB = objMB.MaMB;
                                    objLTT.DotTT = _DotTT;
                                    objLTT.TuNgay = _TuNgay;
                                    objLTT.DenNgay = _DenNgay;
                                    objLTT.SoThang = _KyTT;
                                    objLTT.SoTien = _TongTien;
                                    //objLTT.SoTienQD = Math.Round( (decimal)objLTT.SoTien, 2) * n.TyGia;
                                    objLTT.Loai = "CHOTHUE";

                                    objLTT.NgayHHTT = _DenNgay;
                                    objLTT.IsKhuyenMai = false;
                                    objLTT.DienTich = n.DienTich;
                                    objLTT.DonGia = n.DonGia;
                                    objLTT.GiaThue = n.ThanhTien;
                                    objLTT.MaLoaiTien = objLT.ID;
                                    objLTT.NgungSuDung = false;
                                    objLTT.TyGia = n.TyGia;
                                    objLTT.HanhDong = "IMPORT";
                                    objLTT.NgayTao = System.DateTime.UtcNow;
                                    objLTT.ThanhTienNgoaiTe = n.GiaThueNgoaiTe * _KyTT;
                                    objLTT.LoaiTienNgoaiTe = objHD.LoaiTienNgoaiTe;
                                    objLTT.TyGiaNgoaiTe = n.TyGiaNgoaiTe;
                                    objLTT.PhiDichVu = n.PhiDichVu;
                                    objLTT.TyLeVAT = n.TyLeVAT;
                                    objLTT.TyLeCK = n.TyLeCK;
                                    objLTT.PhiDieuHoaChieuSang = n.PhiDieuHoaChieuSang;

                                    objLTT.DienGiai = string.Format("Tiền thuê mặt bằng từ ngày {0:dd/MM/yyyy} đến ngày {1:dd/MM/yyyy}", _TuNgay, _DenNgay);
                                    
                                    objHD.ctLichThanhToans.Add(objLTT);

                                    db.SubmitChanges();

                                    var model = new { maltt = objLTT.ID };
                                    var param = new Dapper.DynamicParameters();
                                    param.AddDynamicParams(model);
                                    Library.Class.Connect.QueryConnect.Query<bool>("lease_frmimport_ctlichthanhtoan_edit", param);
                                }

                                _TuNgay = _DenNgay.AddDays(1);
                            }

                            //Lich dat coc
                            if (n.DotDatCoc.GetValueOrDefault() != 0)
                            {
                                var objLDC = new ctLichThanhToan();
                                objLDC.MaLDV = 3;
                                objLDC.DotTT = n.DotDatCoc;
                                objLDC.TuNgay = n.TuNgay;
                                objLDC.DenNgay = n.DenNgay;
                                objLDC.SoThang = n.SoThang;
                                objLDC.SoTien = n.SoTien;
                                objLDC.SoTienQD = n.SoTien * objLT.TyGia;
                                objLDC.DienTich = n.DienTich;
                                objLDC.DonGia = n.DonGia;
                                objLDC.GiaThue = n.ThanhTien;
                                objLDC.DienGiai = "Tiền đặt cọc";
                                objHD.ctLichThanhToans.Add(objLDC);
                                objLDC.MaMB = objMB.MaMB;
                                objLDC.TyGia = n.TyGia;
                                objLDC.MaLoaiTien = objLT.ID;

                                objHD.TienCoc = objHD.TienCoc ?? 0 + objLDC.SoTien;

                                //objCT.TyLeVAT = 0;
                                //objCT.TienVAT = 0;
                                //objCT.TyLeCK = 0;
                                //objCT.TienCK = 0;

                                objLDC.TyLeVAT = 0;
                                objLDC.TyLeCK = 0;
                                
                                

                                db.SubmitChanges();

                                var model = new { maltt = objLDC.ID };
                                var param = new Dapper.DynamicParameters();
                                param.AddDynamicParams(model);
                                Library.Class.Connect.QueryConnect.Query<bool>("lease_frmimport_ctlichthanhtoan_edit", param);
                            }
                            objCT.ThanhTien = objCT.TongGiaThue + objCT.TienVAT - objCT.TienCK;
                        }
                        else
                        {
                            // loại thi công, thêm vào bảng thi công và thêm vào lịch thanh toán, chỉ thanh toán 1 lần
                            var model = new { mahd = objHD.ID, mamb = objMB.MaMB, dientich = n.DienTich, phidichvu = n.PhiDichVu, phithicong = n.PhiThiCong, tylevat = n.TyLeVAT, tienvat = n.TienVAT, tyleck = n.TyLeCK, tienck = n.TienCK, loaitien = objLT.ID, tygia = n.TyGia, thanhtien = n.ThanhTien, loaitienngoaite = objHD.LoaiTienNgoaiTe, tygiangoaite = n.TyGiaNgoaiTe, thanhtienngoaite = n.GiaThueNgoaiTe, tungay = n.TuNgay, denngay = n.DenNgay, hanhdong = "IMPORT" };
                            var param = new Dapper.DynamicParameters();
                            param.AddDynamicParams(model);
                            Library.Class.Connect.QueryConnect.Query<bool>("lease_frmimport_ctthicong_edit", param);
                        }

                        #endregion
                        db.SubmitChanges();

                        #region Tính lại tiền lịch thanh toán theo công thức
                        Library.Class.Connect.QueryConnect.QueryData<bool>("ctLichThanhToanUpdateValue", new { MaHD = objHD.ID });
                        #endregion
                    }
                    catch (Exception ex)
                    {
                        n.Error = ex.Message;
                        ltError.Add(n);
                    }
                }

                this.isSave = true;
                DialogBox.Success();

                if (ltError.Count > 0)
                {
                    gcHD.DataSource = ltError;
                }
                else
                {
                    gcHD.DataSource = null;
                }
            }
            catch (System.Exception ex)
            {
                wait.Close();
                DialogBox.Alert("Vui lòng xem lại dữ liệu có bị ràng buộc hay không");
                this.Close();
            }
            finally { wait.Dispose(); db.Dispose(); }
        }

        private void frmImport_Load(object sender, EventArgs e)
        {

        }

        private void itemExportMau_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Library.Commoncls.ExportExcel(gcHD);
        }
    }

    public class HopDongThueItem
    {
        #region Param cũ
        public string MaSoKH { get; set; }        
        public string SoHD { get; set; }
        public string SoPL { get; set; }
        public DateTime NgayKy { get; set; }
        public DateTime NgayHL { get; set; }
        public DateTime NgayHH { get; set; }
        public decimal? ThoiHan { get; set; }
        public decimal KyThanhToan { get; set; }
        public DateTime? NgayBG { get; set; }
        public decimal? TyLeDCTG { get; set; }
        public int? SoNamDCTG { get; set; }
        public decimal? MucDCTG { get; set; }
        public string TenLT { get; set; }
        public decimal? TyGia { get; set; }
        public string MaSoMB { get; set; }
        public string LoaiGiaThue { get; set; }
        public decimal? DienTich { get; set; }
        public decimal? DonGia { get; set; }
        public decimal? PhiDichVu { get; set; }
        public decimal? Tong { get; set; }
        public decimal? TyLeVAT { get; set; }
        public decimal? TienVAT { get; set; }
        public decimal? TyLeCK { get; set; }
        public decimal? TienCK { get; set; }
        public decimal? ThanhTien { get; set; }
        public decimal? PhiSuaChua { get; set; }
        public int? DotDatCoc { get; set; }
        public DateTime? TuNgay { get; set; }
        public DateTime? DenNgay { get; set; }
        public int? SoThang { get; set; }
        public decimal? SoTien { get; set; }
        public string DienGiai { get; set; }

        #endregion

        // Thêm bên bitexco
        public string LoaiTienNgoaiTe { get; set; }
        public decimal? TyGiaNgoaiTe { get; set; }
        public decimal? GiaThueNgoaiTe { get; set; }
        public string NhaThau { get; set; }
        public DateTime? NgayHHBH { get; set; }
        public string GiayToLienQuan { get; set; }
        public string GhiChu { get; set; }
        public string DieuKhoanDacBiet { get; set; }
        public string DieuKhoanBoSungGiamThue { get; set; }
        public string XemXetLaiTienThue { get; set; }
        public decimal? DonGiaTuongDuongUSD { get; set; }
        public decimal? TyGiaApDungBanDau { get; set; }
        public DateTime? ThoiGIanThayDoiTyGIa { get; set; }
        public string CachThayDoiTyGia { get; set; }
        public DateTime? NgayTangGiaTiepTheo { get; set; }
        public string Loai { get; set; }
        public decimal? PhiThiCong { get; set; }
        public int? SoNgayGiaHanThanhToan { get; set; }
        public DateTime? NgayTinhTronKyThanhToan { get; set; }

        public decimal? LaiSuatNopCham { get; set; }
        public int? SoLuongXeMienPhi { get; set; }
        public bool? IsMienLai { get; set; }
        public int? ThoiGianChoPhepNopCham { get; set; }
        public int? SoNgayFree { get; set; }

        // Thêm bên euro
        public decimal? PhiDieuHoaChieuSang { get; set; }

        public string Error { get; set; }
    }
}