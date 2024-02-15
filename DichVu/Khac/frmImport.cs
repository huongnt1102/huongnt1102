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
using LinqToExcel;

namespace DichVu.Khac
{
    public partial class frmImport : DevExpress.XtraEditors.XtraForm
    {
        public frmImport()
        {
            InitializeComponent();

            TranslateLanguage.TranslateControl(this);
        }

        public byte MaTN { get; set; }
        public bool IsSave { get; set; }

        private void itemChoice_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var file = new OpenFileDialog();
            try
            {
                file.Filter = "(Excel file)|*.xls;*.xlsx";
                file.ShowDialog();
                if (file.FileName == "") return;

                var excel = new ExcelQueryFactory(file.FileName);
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
            {
                file.Dispose();
            }
        }

        private void itemSheet_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                var excel = new ExcelQueryFactory(this.Tag.ToString());

                System.Collections.Generic.List<DichVuCoBanItem> list = Library.Import.ExcelAuto.ConvertDataTable<DichVuCoBanItem>(Library.Import.ExcelAuto.GetDataExcel(excel, gv, itemSheet));

                gc.DataSource = list;

                //gc.DataSource = excel.Worksheet(itemSheet.EditValue.ToString()).Select(p => new DichVuCoBanItem
                //{
                //    SoCT = p["Số chứng từ"].ToString().Trim(),
                //    NgayCT = p["Ngày chứng từ"].Cast<DateTime>(),
                //    MaSoMB = p["Mã mặt bằng"].ToString().Trim(),
                //    MaSoKH = p["Mã khách hàng"].ToString().Trim(),
                //    TenLDV = p["Loại dịch vụ"].ToString().Trim(),
                //    SoLuong = p["Số lượng"].Cast<decimal>(),
                //    TenDVT = p["ĐVT"].ToString().Trim(),
                //    DonGia = p["Đơn giá"].Cast<decimal>(),
                //    ThanhTien = p["Thành tiền"].Cast<decimal>(),
                //    ThueGTGT = p["Tỷ lệ VAT"].Cast<decimal>(),
                //    TienThueGTGT = p["Tiền VAT"].Cast<decimal>(),
                //    NgayTT = p["Ngày TT"].Cast<DateTime>(),
                //    KyTT = p["Kỳ TT"].Cast<int>(),
                //    IsLapLai = p["Lặp lại"].Cast<bool>(),
                //    TienTT = p["Tiền TT"].Cast<decimal>(),
                //    TenLT = p["Loại tiền"].ToString().Trim(),
                //    TyGia = p["Tỷ giá"].Cast<decimal>(),
                //    TienTTQD = p["Tiền TT QĐ"].Cast<decimal>(),
                //    TuNgay = p["Từ ngày"].Cast<DateTime>(),
                //    DenNgay = p["Đến ngày"].Cast<DateTime>(),
                //    DienGiai = p["Diễn giải"].ToString().Trim()
                //}).ToList();

                excel = null;
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
        }

        private void itemDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DialogBox.QuestionDelete() == System.Windows.Forms.DialogResult.No) return;
            gv.DeleteSelectedRows();
        }

        private void itemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gc.DataSource == null)
            {
                DialogBox.Error("Vui lòng chọn sheet");  
                return;
            }

            var wait = DialogBox.WaitingForm();
            var db = new MasterDataContext();
            try
            {
                var ltMatBang = (from mb in db.mbMatBangs where mb.MaTN == this.MaTN orderby mb.MaSoMB select new { mb.MaMB, MaSoMB = mb.MaSoMB.ToLower() }).ToList();
                var ltKhachHang = (from kh in db.tnKhachHangs where kh.MaTN == this.MaTN orderby kh.KyHieu select new { kh.MaKH, KyHieu = kh.KyHieu.ToLower() }).ToList();
                var ltLoaiDichVu = (from p in db.dvLoaiDichVus orderby p.TenHienThi select new { p.ID, TenLDV = p.TenHienThi.ToLower() }).ToList();
                var ltLoaiTien = (from p in db.LoaiTiens orderby p.KyHieuLT select new { p.ID, TenLT = p.KyHieuLT.ToLower() }).ToList();
                var ltDonViTinh = (from p in db.DonViTinhs orderby p.TenDVT select new { p.ID, TenDVT = p.TenDVT.ToLower() }).ToList();
                
                var ltSource = (List<DichVuCoBanItem>)gc.DataSource;
                var ltError = new List<DichVuCoBanItem>();

                foreach (var i in ltSource)
                {
                    db = new MasterDataContext();
                    try
                    {
                        #region rang buoc du lieu
                        if (i.SoCT.Length == 0)
                        {
                            i.Error = "Vui lòng nhập số chứng từ";
                            ltError.Add(i);
                            continue;
                        }

                        if (i.NgayCT.Year <= 1)
                        {
                            i.Error = "Vui lòng nhập ngày chứng từ";
                            ltError.Add(i);
                            continue;
                        }

                        if (i.SoLuong <= 0)
                        {
                            i.Error = "Số lượng phải lớn hơn 0";
                            ltError.Add(i);
                            continue;
                        }

                        if (i.DonGia <= 0)
                        {
                            i.Error = "Đơn giá phải lớn hơn 0";
                            ltError.Add(i);
                            continue;
                        }
                        
                        if (i.ThanhTien <= 0)
                        {
                            i.Error = "Thành tiền phải lớn hơn 0";
                            ltError.Add(i);
                            continue;
                        }

                        if (i.NgayTT.Year <= 1)
                        {
                            i.Error = "Vui lòng nhập ngày thanh toán";
                            ltError.Add(i);
                            continue;
                        }

                        if (i.TienTT <= 0)
                        {
                            i.Error = "Tiền thanh toán phải lớn hơn 0";
                            ltError.Add(i);
                            continue;
                        }

                        if (i.TyGia <= 0)
                        {
                            i.Error = "Tỷ giá phải lớn hơn 0";
                            ltError.Add(i);
                            continue;
                        }

                        if (i.TienTTQD <= 0)
                        {
                            i.Error = "Tiền thanh toán (quy đổi) phải lớn hơn 0";
                            ltError.Add(i);
                            continue;
                        }

                        var _MaLT = ltLoaiTien.Where(p => p.TenLT == i.TenLT.ToLower()).Select(p => (int?)p.ID).FirstOrDefault();
                        if (_MaLT == null)
                        {
                            i.Error = "Loại tiền không tồn tại";
                            ltError.Add(i);
                            continue;
                        }

                        var _MaDVT = ltDonViTinh.Where(p => p.TenDVT == i.TenDVT.ToLower()).Select(p => (int?)p.ID).FirstOrDefault();
                        if (_MaDVT == null)
                        {
                            i.Error = "Đơn vị tính không tồn tại";
                            ltError.Add(i);
                            continue;
                        }

                        var _MaLDV = ltLoaiDichVu.Where(p => p.TenLDV == i.TenLDV.ToLower()).Select(p => (int?)p.ID).FirstOrDefault();
                        if (_MaLDV == null)
                        {
                            i.Error = "Loại dịch vụ không tồn tại";
                            ltError.Add(i);
                            continue;
                        }

                        var _MaMB = ltMatBang.Where(p => p.MaSoMB == i.MaSoMB.ToLower()).Select(p => (int?)p.MaMB).FirstOrDefault();
                        if (_MaMB == null)
                        {
                            i.Error = "Mã mặt bằng không tồn tại";
                            ltError.Add(i);
                            continue;
                        }

                        var _MaKH = ltKhachHang.Where(p => p.KyHieu == i.MaSoKH.ToLower()).Select(p => (int?)p.MaKH).FirstOrDefault();
                        if (_MaKH == null)
                        {
                            i.Error = "Mã khách hàng không tồn tại";
                            ltError.Add(i);
                            continue;
                        }
                        #endregion

                        var objDVK = db.dvDichVuKhacs.FirstOrDefault(p => p.MaTN == this.MaTN & p.MaMB == _MaMB && p.MaLDV == _MaLDV && p.MaKH == _MaKH);
                        if (objDVK == null)
                        {
                            objDVK = new dvDichVuKhac();
                            objDVK.MaTN = this.MaTN;
                            objDVK.SoCT = i.SoCT;
                            objDVK.MaNVN = Common.User.MaNV;
                            objDVK.NgayNhap = db.GetSystemDate();
                            objDVK.IsNgungSuDung = false;
                            db.dvDichVuKhacs.InsertOnSubmit(objDVK);
                        }
                        else
                        {
                            objDVK.MaNVS = Common.User.MaNV;
                            objDVK.NgaySua = db.GetSystemDate();
                        }
                        
                        objDVK.NgayCT = i.NgayCT;
                        objDVK.MaMB = _MaMB;
                        objDVK.MaKH = _MaKH;
                        objDVK.MaLDV = _MaLDV;
                        objDVK.MaDVT = _MaDVT;
                        objDVK.SoLuong = i.SoLuong;
                        objDVK.DonGia = i.DonGia;
                        objDVK.ThanhTien = i.ThanhTien;
                        objDVK.TienTruocThue = Math.Round((decimal)(i.SoLuong * i.DonGia * i.KyTT),0,MidpointRounding.AwayFromZero);
                        objDVK.ThueGTGT = i.ThueGTGT;
                        objDVK.TienThueGTGT = i.TienThueGTGT;
                        objDVK.NgayTT = i.NgayTT;
                        objDVK.KyTT = i.KyTT;
                        objDVK.IsLapLai = i.IsLapLai;
                        objDVK.TienTT = Math.Round((decimal)(i.TienTT),0,MidpointRounding.AwayFromZero);
                        objDVK.MaLT = _MaLT;
                        objDVK.TyGia = i.TyGia;
                        objDVK.TienTTQD = Math.Round((decimal)(i.TienTTQD),0,MidpointRounding.AwayFromZero);
                        objDVK.TuNgay = i.TuNgay.Year == 1 ? (DateTime?)null : i.TuNgay;
                        objDVK.DenNgay = i.DenNgay.Year == 1 ? (DateTime?)null : i.DenNgay;
                        //objDVK.IsLapLai =
                        objDVK.DienGiai = i.DienGiai;

                        db.SubmitChanges();
                    }
                    catch (Exception ex)
                    {
                        i.Error = ex.Message;
                        ltError.Add(i);
                    }
                }

                this.IsSave = true;
                DialogBox.Success();

                if (ltError.Count > 0)
                {
                    gc.DataSource = ltError;
                }
                else
                {
                    gc.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
            finally
            {
                wait.Close();
                db.Dispose();
            }
        }

        private void itemClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void itemExportMau_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Library.Commoncls.ExportExcel(gc);
        }
    }

    public class DichVuCoBanItem
    {
        public string SoCT { get; set; }
        public DateTime NgayCT { get; set; }
        public string MaSoMB { get; set; }
        public string MaSoKH { get; set; }
        public string TenLDV { get; set; }
        public decimal SoLuong { get; set; }
        public string TenDVT { get; set; }
        public decimal DonGia { get; set; }
        public decimal ThanhTien { get; set; }
        public DateTime NgayTT { get; set; }
        public int KyTT { get; set; }
        public bool IsLapLai { get; set; }
        public decimal? ThueGTGT { get; set; }
        public decimal? TienThueGTGT { get; set; }
        public decimal TienTT { get; set; }
        public string TenLT { get; set; }
        public decimal TyGia { get; set; }
        public decimal TienTTQD { get; set; }
        public DateTime TuNgay { get; set; }
        public DateTime DenNgay { get; set; }
        public string DienGiai { get; set; }
        public string Error { get; set; }
    }
}