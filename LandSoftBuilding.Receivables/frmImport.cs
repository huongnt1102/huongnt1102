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

namespace LandSoftBuilding.Receivables
{
    public partial class frmImport : DevExpress.XtraEditors.XtraForm
    {
        public frmImport()
        {
            InitializeComponent();
        }

        public byte MaTN { get; set; }
        public bool isSave { get; set; }

        private void frmImport_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this);
        }

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

                System.Collections.Generic.List<HoaDonItem> list = Library.Import.ExcelAuto.ConvertDataTable<HoaDonItem>(Library.Import.ExcelAuto.GetDataExcel(excel, gvImport, itemSheet));

                gcImport.DataSource = list;

                //gcImport.DataSource = excel.Worksheet(itemSheet.EditValue.ToString()).Select(p => new HoaDonItem
                //{
                //    MaSoMB = p["Mã mặt bằng"].ToString().Trim(),
                //    MaSoKH = p["Mã khách hàng"].ToString().Trim(),
                //    TenLDV = p["Loại dịch vụ"].ToString().Trim(),
                //    NgayTT = p["Ngày Thanh Toán"].Cast<DateTime>(),
                //    PhiDV = p["Phí dịch vụ"].Cast<decimal>(),
                //    KyTT = p["Kỳ Thanh Toán"].Cast<decimal>(),
                //    TienTT = p["Tiền Thanh Toán"].Cast<decimal>(),
                //    TyLeCK = p["Tỷ lệ Chiết Khấu"].Cast<decimal>(),
                //    TienCK = p["Tiền Chiết Khấu"].Cast<decimal>(),
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
            gvImport.DeleteSelectedRows();
        }

        private void itemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gcImport.DataSource == null)
            {
                DialogBox.Error("Vui lòng chọn sheet");
                return;
            }

            var wait = DialogBox.WaitingForm();
            var db = new MasterDataContext();
            try
            {
                var ltMatBang = (from mb in db.mbMatBangs
                                 where mb.MaTN == this.MaTN
                                 select new { mb.MaMB, MaSoMB = mb.MaSoMB.ToLower() }).ToList();

                var ltKhachHang = (from kh in db.tnKhachHangs
                                   where kh.MaTN == this.MaTN
                                   select new { kh.MaKH, MaSoKH = kh.KyHieu.ToLower() }).ToList();

                var ltLoaiDichVu = (from ldv in db.dvLoaiDichVus
                                   select new { ldv.ID, TenLDV = ldv.TenLDV.ToLower() }).ToList();

                var loaiXe = db.dvgxLoaiXes;

                var ltSource = (List<HoaDonItem>)gcImport.DataSource;
                var ltError = new List<HoaDonItem>();

                foreach(var i in ltSource)
                {
                    db = new MasterDataContext();
                    try
                    {
                        #region Rang buoc nhập nhiệu
                        if (i.NgayTT.Year == 1)
                        {
                            i.Error = "Vui lòng nhập ngày TT";
                            ltError.Add(i);
                            continue;
                        }

                        if (i.PhiDV == 0)
                        {
                            i.Error = "Vui lòng nhập phí dịch vụ";
                            ltError.Add(i);
                            continue;
                        }

                        if (i.TienTT == 0)
                        {
                            i.Error = "Vui lòng nhập tiền thanh toán";
                            ltError.Add(i);
                            continue;
                        }

                        var objMB = ltMatBang.FirstOrDefault(p => p.MaSoMB == i.MaSoMB.ToLower());
                        if (objMB == null)
                        {
                            i.Error = "Mặt bằng không tồn tại trong hệ thống";
                            ltError.Add(i);
                            continue;
                        }

                        var objKH = ltKhachHang.FirstOrDefault(p => p.MaSoKH == i.MaSoKH.ToLower());
                        if (objKH == null)
                        {
                            i.Error = "Khách hàng không tồn tại trong hệ thống";
                            ltError.Add(i);
                            continue;
                        }

                        var objLDV = ltLoaiDichVu.FirstOrDefault(p => p.TenLDV == i.TenLDV.ToLower());
                        if (objLDV == null)
                        {
                            i.Error = "Tên loại dịch vụ không tồn tại trong hệ thống";
                            ltError.Add(i);
                            continue;
                        }
                        
                        
                        
                        #endregion

                        #region Kiểm tra khóa hóa đơn
                        // Cần trả về là có được phép sửa hay return
                        // truyền vào form service, từ ngày đến ngày, tòa nhà

                        var result = DichVu.KhoaSo.Class.ClosingEntry.Closing(this.MaTN, i.NgayTT, DichVu.KhoaSo.Class.Enum.BILL);

                        if (result.Count() > 0)
                        {
                            //DialogBox.Error("Hóa đơn đã khóa.");
                            continue;
                        }

                        #endregion

                        var objHD = new dvHoaDon();
                        objHD.MaTN = this.MaTN;
                        objHD.MaMB = objMB.MaMB;
                        objHD.MaKH = objKH.MaKH;
                        objHD.MaLDV = objLDV.ID;
                        objHD.NgayTT = i.NgayTT;
                        objHD.PhiDV = i.PhiDV;
                        objHD.KyTT = i.KyTT;
                        objHD.TienTT = i.TienTT;
                        objHD.TyLeCK = i.TyLeCK;
                        objHD.TienCK = i.TienCK;
                        objHD.TuNgay = i.TuNgay.Year == 1 ? (DateTime?)null : i.TuNgay;
                        objHD.DenNgay = i.DenNgay.Year == 1 ? (DateTime?)null : i.DenNgay;
                        objHD.DienGiai = i.DienGiai;

                        objHD.PhaiThu = i.TienTT - i.TienCK;
                        objHD.DaThu = 0;
                        objHD.ConNo = objHD.PhaiThu;
                        objHD.IsAuTo = false;
                        objHD.NgayNhap = db.GetSystemDate();
                        objHD.MaNVN = Common.User.MaNV;
                        try
                        {
                            if (i.TenLX != "")
                            {
                                var lx = loaiXe.FirstOrDefault(_ => _.TenLX.ToLower() == i.TenLX.ToLower());
                                if (lx != null)
                                {
                                    objHD.MaLX = lx.MaLX;
                                }
                                else
                                {
                                    i.Error = "Tên loại xe không tồn tại trong hệ thống";
                                    ltError.Add(i);
                                    continue;
                                }
                            }
                        }
                        catch { }

                        db.dvHoaDons.InsertOnSubmit(objHD);
                        db.SubmitChanges();
                    }
                    catch (Exception ex)
                    {
                        i.Error = ex.Message;
                        ltError.Add(i);
                    }
                }

                this.isSave = true;
                DialogBox.Success();

                if (ltError.Count > 0)
                {
                    gcImport.DataSource = ltError;
                }
                else
                {
                    gcImport.DataSource = null;
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
            Library.Commoncls.ExportExcel(gcImport);
        }
    }

    public class HoaDonItem
    {
        public string MaSoMB { get; set; }
        public string MaSoKH { get; set; }
        public string TenLDV { get; set; }
        public DateTime NgayTT { get; set; }
        public decimal PhiDV { get; set; }
        public decimal KyTT { get; set; }
        public decimal TienTT { get; set; }
        public decimal TyLeCK { get; set; }
        public decimal TienCK { get; set; }
        public DateTime TuNgay { get; set; }
        public DateTime DenNgay { get; set; }
        public string DienGiai { get; set; }
        public string TenLX { get; set; }
        public string Error { get; set; }
    }
}