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
    public partial class frmImportPhoYen : DevExpress.XtraEditors.XtraForm
    {
        public byte MaTN { get; set; }
        public bool isSave { get; set; }

        public frmImportPhoYen()
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

                    System.Collections.Generic.List<HopDongThueItem1> list = Library.Import.ExcelAuto.ConvertDataTable<HopDongThueItem1>(Library.Import.ExcelAuto.GetDataExcel(excel, grvHD, itemSheet));

                    gcHD.DataSource = list;

                    //gcHD.DataSource = excel.Worksheet(sheet.EditValue.ToString()).Select(p => new HopDongThueItem1
                    //{
                    //    MaSoKH = p[6].ToString().Trim(),
                    //    SoHD = p[1].ToString().Trim(),
                    //    SoPL = p[2].ToString(),
                    //    NgayKy = p[15].Cast<DateTime>(),
                    //    NgayHL = p[16].Cast<DateTime>(),
                    //    NgayHH = p[19].Cast<DateTime>(),
                    //    ThoiHan = p[11].Cast<int>(),
                    //    KyThanhToan = p[12].Cast<decimal>(),
                    //    NgayBG = p[18].Cast<DateTime>(),
                    //    //TyLeDCTG = p[9].Cast<decimal>(),
                    //    //SoNamDCTG = p[10].Cast<int>(),
                    //    //MucDCTG = p[11].Cast<decimal>(),
                    //    TenLT = "VNÐ",
                    //    TyGia = 1,
                    //    MaSoMB = p[3].ToString().Trim(),
                    //    LoaiGiaThue = "gia 1",
                    //    ThoiGianThue = p[14].Cast<decimal>(),
                    //    DienTich = p[4].Cast<decimal>(),
                    //    DonGia = 0,
                    //    PhiDichVu = 0,
                    //    ThanhTien = p[20].Cast<decimal>(),//Chua thue
                    //    TyLeVAT = p[21].Cast<decimal>(),
                    //    TienVAT = p[22].Cast<decimal>(),
                    //    TyLeCK = p[23].Cast<decimal>(),
                    //    TienCK = p[24].Cast<decimal>(),
                    //    PhiSuaChua = 0,
                    //    DotDatCoc = 0,
                    //    ThoiGianUD = p[13].Cast<decimal>(),
                    //    //TuNgay = p[26].Cast<DateTime>(),
                    //    //DenNgay = p[27].Cast<DateTime>(),
                    //    //SoThang = p[28].Cast<int>(),
                    //    SoTien = p[25].Cast<decimal>(),//Da thue

                    //}).ToList();

                    excel = null;
                }
                catch (Exception ex)
                {
                    DialogBox.Error(ex.Message);
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
                var ltLoaiMB =
                    (from p in db.mbLoaiMatBangs where p.MaTN == this.MaTN select new {p.MaLMB, p.MaTN, p.TenLMB})
                        .ToList();
                #region "   Rang buoc du lieu"
                if (ltKhachHang.Count <= 0)
                {
                    DialogBox.Alert("Vui lòng thêm [Khách hàng] cho Dự án này truớc.");
                    return;
                }
             
                if (ltMatBang.Count <= 0)
                {
                    DialogBox.Alert("Vui lòng thêm [Mặt bằng] cho Dự án này truớc.");
                    return;
                }

                if (ltLoaiGiaThue.Count <= 0)
                {
                    DialogBox.Alert("Vui lòng thêm [Loại giá thuê] cho Dự án này truớc.");
                    return;
                }

                if (ltLoaiTien.Count <= 0)
                {
                    DialogBox.Alert("Vui lòng thêm [Loại tiền] cho Dự án này truớc.");
                    return;
                }
                #endregion

                var ltHopDong = (List<HopDongThueItem1>)gcHD.DataSource;
                var ltError = new List<HopDongThueItem1>();
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

                        //var objLT = ltLoaiTien.FirstOrDefault(p => p.KyHieuLT.ToLower() == n.TenLT.ToLower());
                        //if (objLT == null)
                        //{
                        //    n.Error = "Loại tiền này không tồn tại trong hệ thống";
                        //    ltError.Add(n);
                        //    continue;
                        //}
                        #endregion

                        ctHopDong objHD;
                        int? ParentID = null;

                        if (n.SoPL == "")
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
                        objHD.SoHDCT = ParentID == null ? n.SoHD : n.SoPL;
                        objHD.NgayKy = n.NgayKy;
                        objHD.NgayHL = n.NgayHL;
                        objHD.NgayHH = n.NgayHH;
                        objHD.ThoiHan = n.ThoiHan;
                        objHD.KyTT = Convert.ToInt32(n.KyThanhToan);
                        objHD.NgayBG = n.NgayBG.Value.Year < 1000 ? (DateTime?)null : n.NgayBG;
                        //objHD.TyLeDCGT = n.TyLeDCTG;
                        //objHD.SoNamDCGT = n.SoNamDCTG;
                        // objHD.MucDCTG = n.MucDCTG;                        
                        objHD.MaLT = 1;
                        objHD.TyGia = objHD.TyGiaHD = n.TyGia ?? 1;
                        objHD.NgayNhap = db.GetSystemDate();
                        objHD.MaNVN = Common.User.MaNV;
                        objHD.NgungSuDung = false;
                        db.ctHopDongs.InsertOnSubmit(objHD);

                    ChiTiet:
                        var objCT = new ctChiTiet();
                        objCT.MaMB = objMB.MaMB;
                        objCT.MaLG = objLGT.ID;
                        objCT.DienTich = n.DienTich;
                        objCT.DonGia = (n.ThanhTien / n.ThoiGianThue) / n.DienTich;
                        objCT.PhiDichVu = n.PhiDichVu;
                        objCT.TongGiaThue = n.ThanhTien / n.ThoiGianThue;
                        objCT.TyLeVAT = n.TyLeVAT;
                        objCT.TienVAT = (n.TienVAT / n.ThoiGianThue);
                        objCT.TyLeCK = n.TyLeCK;
                        objCT.TienCK = n.TienCK;
                        objCT.ThanhTien = n.SoTien / n.ThoiGianThue;
                        objCT.PhiSuaChua = n.PhiSuaChua;
                        //objCT.DienGiai = n.DienGiai;
                        objCT.NgungSuDung = false;
                        objHD.ctChiTiets.Add(objCT);

                        objHD.GiaThue = objHD.GiaThue ?? 0 + objCT.ThanhTien;

                        //Lich thanh toan
                        
                        if (n.ThoiGianUD != 0)
                        {
                            var _TuNgay = n.NgayHH.AddMonths((int)-n.ThoiGianThue);
                            //var _TuNgay = n.NgayHL;
                            var _NgayHH = n.NgayHH;
                            int _DotTT = 0;
                            while (_TuNgay.CompareTo(_NgayHH) < 0)
                            {
                                _DotTT++;
                                decimal _KyTT = n.KyThanhToan;
                                var _DenNgay = _TuNgay.AddMonths(Convert.ToInt32(_KyTT)).AddDays(-1);
                                if (_DenNgay.CompareTo(_NgayHH) > 0)
                                {
                                    _DenNgay = _NgayHH;
                                    _KyTT = Common.GetTotalMonth(_TuNgay, _DenNgay);
                                }

                                if (_KyTT > 0)
                                {
                                    var objLTT = new ctLichThanhToan();
                                    objLTT.MaLDV = 2;
                                    objLTT.MaMB = objMB.MaMB;
                                    objLTT.DotTT = _DotTT;
                                    objLTT.TuNgay = _TuNgay;
                                    objLTT.DenNgay = _DenNgay;
                                    objLTT.SoThang = _KyTT;
                                    objLTT.SoTien = objHD.GiaThue * _KyTT;
                                    objLTT.SoTienQD = objHD.GiaThue * 1 * _KyTT;
                                    objLTT.DienGiai = string.Format("Tiền thuê mặt bằng từ ngày {0:dd/MM/yyyy} đến ngày {1:dd/MM/yyyy}", _TuNgay, _DenNgay);
                                    objHD.ctLichThanhToans.Add(objLTT);
                                }

                                _TuNgay = _DenNgay.AddDays(1);
                            }
                        }
                        else
                        {
                        
                            var _TuNgay = n.NgayHL;
                            var _NgayHH = n.NgayHH;
                            int _DotTT = 0;
                            while (_TuNgay.CompareTo(_NgayHH) < 0)
                            {
                                _DotTT++;
                                decimal _KyTT = n.KyThanhToan;
                                var _DenNgay = _TuNgay.AddMonths(Convert.ToInt32(_KyTT)).AddDays(-1);
                                if (_DenNgay.CompareTo(_NgayHH) > 0)
                                {
                                    _DenNgay = _NgayHH;
                                    _KyTT = Common.GetTotalMonth(_TuNgay, _DenNgay);
                                }

                                if (_KyTT > 0)
                                {
                                    var objLTT = new ctLichThanhToan();
                                    objLTT.MaLDV = 2;
                                    objLTT.MaMB = objMB.MaMB;
                                    objLTT.DotTT = _DotTT;
                                    objLTT.TuNgay = _TuNgay;
                                    objLTT.DenNgay = _DenNgay;
                                    objLTT.SoThang = _KyTT;
                                    objLTT.SoTien = objHD.GiaThue * _KyTT;
                                    objLTT.SoTienQD = objHD.GiaThue * 1 * _KyTT;
                                    objLTT.DienGiai = string.Format("Tiền thuê mặt bằng từ ngày {0:dd/MM/yyyy} đến ngày {1:dd/MM/yyyy}", _TuNgay, _DenNgay);
                                    objHD.ctLichThanhToans.Add(objLTT);
                                }

                                _TuNgay = _DenNgay.AddDays(1);
                            }
                        }
                        

                        //Lich dat coc
                        if (n.DotDatCoc != 0)
                        {
                            var objLDC = new ctLichThanhToan();
                            objLDC.MaLDV = 3;
                            objLDC.DotTT = n.DotDatCoc;
                            objLDC.TuNgay = n.TuNgay;
                            objLDC.DenNgay = n.DenNgay;
                            objLDC.SoThang = n.SoThang;
                            objLDC.SoTien = n.SoTien;
                            objLDC.SoTienQD = n.SoTien *1;
                            objLDC.DienGiai = n.DienGiai;
                            objHD.ctLichThanhToans.Add(objLDC);

                            objHD.TienCoc = objHD.TienCoc ?? 0 + objLDC.SoTien;
                        }

                        db.SubmitChanges();
                    }
                    catch (Exception ex)
                    {
                        n.Error = ex.Message;
                        ltError.Add(n);
                    }
                }

                this.isSave = true;


                if (ltError.Count > 0)
                {
                    gcHD.DataSource = ltError;
                }
                else
                {
                    DialogBox.Success();
                    gcHD.DataSource = null;
                }
            }
            catch
            {
                wait.Close();
                DialogBox.Alert("Vui lòng xem lại dữ liệu có bỏ ràng buộc hay không");
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

    public class HopDongThueItem1
    {
        public string MaSoKH { get; set; }
        public string SoHD { get; set; }
        public string SoPL { get; set; }
        public DateTime NgayKy { get; set; }
        public DateTime NgayHL { get; set; }
        public DateTime NgayHH { get; set; }
        public int ThoiHan { get; set; }
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
        public decimal ThoiGianUD { get; set; }
        public decimal ThoiGianThue { get; set; }
        public string DienGiai { get; set; }
        public string Error { get; set; }
        
    }
}