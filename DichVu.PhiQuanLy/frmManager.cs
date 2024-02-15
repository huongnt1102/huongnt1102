using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using Library;
using System.Data.Linq.SqlClient;
using System.Threading;
using DevExpress.XtraReports.UI;

namespace DichVu.PhiQuanLy
{
    public partial class frmManager : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public tnNhanVien objnhanvien;
        DateTime now;
        string MM = "";
        bool first = true;
        List<ItemData> listMB;
        int? ChuKy;

        public frmManager()
        {
            InitializeComponent();
            db = new MasterDataContext();
            TranslateLanguage.TranslateControl(this,barManager1);
            now = db.GetSystemDate();
        }

        private void frmManager_Load(object sender, EventArgs e)
        {
            Library.HeThongCls.PhanQuyenCls.Authorize(this, objnhanvien, barManager1);

            var list = Library.ManagerTowerCls.GetAllTower(objnhanvien);
            lookUpToaNha.DataSource = list;
            if (list.Count > 0)
                itemToaNha.EditValue = list[0].MaTN;

            itemYear.EditValue = db.GetSystemDate().Year;
            SetMonth();

            LoadData();
            first = false;
        }

        int GetMonth()
        {
            if (itemMonth.EditValue == null)
                return 0;
            else
            {
                if (MM != itemMonth.EditValue.ToString())
                    MM = itemMonth.EditValue.ToString();
                switch (MM)
                {
                    case "Tháng 1":
                        return 1;
                    case "Tháng 2":
                        return 2;
                    case "Tháng 3":
                        return 3;
                    case "Tháng 4":
                        return 4;
                    case "Tháng 5":
                        return 5;
                    case "Tháng 6":
                        return 6;
                    case "Tháng 7":
                        return 7;
                    case "Tháng 8":
                        return 8;
                    case "Tháng 9":
                        return 9;
                    case "Tháng 10":
                        return 10;
                    case "Tháng 11":
                        return 11;
                    case "Tháng 12":
                        return 12;
                    default:
                        return 0;
                }
            }
        }

        void SetMonth()
        {
            int month = DateTime.Now.Month;

            MM = string.Format("Tháng {0}", month);

            itemMonth.EditValue = MM;
        }

        private void LoadData()
        {
            db = new MasterDataContext();
            var wait = DialogBox.WaitingForm();
            try
            {
                var year = Convert.ToInt32(itemYear.EditValue.ToString());
                var month = GetMonth();
                int maTN = itemToaNha.EditValue != null ? Convert.ToInt32(itemToaNha.EditValue) : 0;

                gcMatBang.DataSource = db.PhiQuanLy_selectBySuperAdminV2(year, month, maTN).ToList();
            }
            catch
            {
                gcMatBang.DataSource = null;
            }
            finally
            {
                wait.Close();
                wait.Dispose();
            }
        }

        private string DaThanhToanPQL(int MaMB, DateTime TuNgay, DateTime DenNgay)
        {
            string result = string.Empty;
            var PhiQLDaThu = db.PhiQuanLies.Where(p => SqlMethods.DateDiffDay(TuNgay, p.ThangThanhToan) >= 0 
                & SqlMethods.DateDiffDay(p.ThangThanhToan, DenNgay) >= 0
                & p.MaMB == MaMB);
            if (PhiQLDaThu.Count() <= 0)
                result = "Chưa thanh toán";
            else
                result = "Đã thanh toán";

            return result;
        }

        private decimal getPhiQL(int MaMB)
        {
            //var pql = db.mbMatBangs.Single(p => p.MaMB == MaMB).PhiQuanLy ?? 0;
            //var pqlhdt = db.thueHopDongs.Where(p => p.MaMB == MaMB).Select(p => p.PhiQL).FirstOrDefault() ?? 0;
            var conno = db.PhiQuanLies.Where(p => p.mbMatBang.MaMB == MaMB).Sum(p => p.ConNo) ?? 0;
            return  conno;
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void grvMatBang_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            LoadDetail();
        }

        void LoadDetail()
        {
            switch (xtrtabChiTietPQL.SelectedTabPageIndex)
            {
                case 0:
                    LoadPDK();
                    break;
                case 1:
                    LoadDebt();
                    break;
                case 2:
                    long? ID = (long?)grvMatBang.GetFocusedRowCellValue("ID");
                    gcLichSu.DataSource = db.cnlsDieuChinhs.Where(p => p.LichSuID == ID).
                        AsEnumerable().Select((p, index) => new { 
                        STT = index + 1,
                        p.ID,
                        NhanVien = p.tnNhanVien.HoTenNV,
                        p.SoTien,
                        p.NgayDC,
                        p.GhiChu
                    }).ToList();
                    break;
                case 3:
                    long? lsID = (long?)grvMatBang.GetFocusedRowCellValue("ID");
                    Gc_LichSu.DataSource = db.PhiQuanLy_LichSus.Where(p => p.MaLS == lsID).
                        AsEnumerable().Select((p, index) => new
                        {
                            STT = index + 1,
                            p.ID,
                            NhanVien = p.tnNhanVien.HoTenNV,
                            p.NgayNhap,
                            p.Barcode,
                            p.GhiChu
                        }).ToList();
                    break;
            }                
        }

        void LoadDebt()
        {
            if (grvMatBang.FocusedRowHandle < 0) return;
            try
            {
                int maMB = (int)grvMatBang.GetFocusedRowCellValue("MaMB");
                var year = Convert.ToInt32(itemYear.EditValue.ToString());
                var month = GetMonth();

                gcNoTruoc.DataSource = db.PhiQuanLy_selectBySuperAdminDebt(year, month, maMB);
            }
            catch { gcNoTruoc.DataSource = null; }
        }

        void LoadPDK()
        {
            if (grvMatBang.FocusedRowHandle < 0)
            {
                gcPhieuDangKy.DataSource = null;
                return;
            }

            int MaMB = (int)grvMatBang.GetFocusedRowCellValue("MaMB");
            DateTime date = (DateTime)grvMatBang.GetFocusedRowCellValue("NgayNhap");
            gcPhieuDangKy.DataSource = db.pqlDangKies.Where(p => p.MaMB == MaMB & SqlMethods.DateDiffDay(p.NgayDK, date) >= 0
                & SqlMethods.DateDiffDay(date, p.NgayKT) >= 0)
                .Select(p => new
                {
                    p.ChuKyID,
                    p.ID,
                    p.MaKH,
                    p.MaMB,
                    p.NgayDK,
                    p.NgayKT,
                    p.NgayTao,
                    p.SoDK,
                    NhanVien = p.tnNhanVien.HoTenNV,
                    NhanVienCN = p.tnNhanVien1 == null ? "" : p.tnNhanVien1.HoTenNV,
                    p.NgayCN,
                    SoTien = p.PhaiThu ?? 0,
                    p.DienGiai
                });
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int[] rows = grvMatBang.GetSelectedRows();
            if (rows.Length <= 0)
            {
                DialogBox.Error("Vui lòng chọn [Mặt bằng], xin cảm ơn.");
                return;
            }
            else
            {
                if (rows.Length == 1)
                {
                    if ((decimal)grvMatBang.GetFocusedRowCellValue("TongNo") > 0)
                    {
                        int MaMB = (int)grvMatBang.GetFocusedRowCellValue("MaMB");
                        var frm = new frmPaid() { objnhanvien = objnhanvien, MaMB = MaMB };
                        frm.NgayNhap = (DateTime?)grvMatBang.GetFocusedRowCellValue("NgayNhap");
                        frm.soTien = (decimal)grvMatBang.GetFocusedRowCellValue("ConLai");
                        frm.MaSoMB = grvMatBang.GetFocusedRowCellValue("MaSoMB").ToString();
                        frm.ChuKyID = (int?)gvPhieuDangKy.GetFocusedRowCellValue("ChuKyID");
                        frm.MaKH = Convert.ToInt32(grvMatBang.GetFocusedRowCellValue("MaKH"));
                        frm.ShowDialog();
                        if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                            LoadData();
                    }
                    else
                        DialogBox.Alert("[Mặt bằng] này đã thu phí Quản Lý. Vui lòng kiểm tra lại, xin cảm ơn.");
                }
                else
                {
                    var wait = DialogBox.WaitingForm();
                    
                    var listData = new List<Library.PhiVeSinh>();
                    foreach (var i in rows)
                    {
                        if (Convert.ToDecimal(grvMatBang.GetRowCellValue(i, "PhiQL")) > 0)
                        {                            
                            var obj = new Library.PhiVeSinh();
                            //obj.DiaChi = grvMatBang.GetRowCellValue(i, "DiaChi").ToString();
                            //obj.DienGiai = "";
                            //obj.MaKH = Convert.ToInt32(grvMatBang.GetRowCellValue(i, "MaKH"));
                            //obj.MaMB = Convert.ToInt32(grvMatBang.GetRowCellValue(i, "MaMB"));
                            //obj.DotThu = DateTime.Parse(grvMatBang.GetRowCellValue(i, "NgayNhap").ToString());
                            //var objPDK = db.pqlDangKies.Where(p => SqlMethods.DateDiffDay(p.NgayDK, obj.DotThu) >= 0 & SqlMethods.DateDiffDay(obj.DotThu, p.NgayKT) >= 0 & p.MaMB == obj.MaMB).FirstOrDefault();
                            //if (objPDK != null)
                            //    obj.ChuKyID = objPDK.ChuKyID;
                            //else
                            //    obj.ChuKyID = 1;

                            //obj.MaSoMB = grvMatBang.GetRowCellValue(i, "MaSoMB").ToString();
                            //obj.NgayThu = getDate(string.Format("22/{0:MM/yyyy}", obj.DotThu)).Value;
                            //obj.NguoiNop = grvMatBang.GetRowCellValue(i, "KhachHang").ToString();
                            //obj.SoTien = Convert.ToDecimal(grvMatBang.GetRowCellValue(i, "PhiQL"));
                            listData.Add(obj);
                        }
                    }

                    wait.Close();
                    wait.Dispose();

                    var f = new frmPaidMulti();
                    //f.listData = listData;
                    f.objNV = objnhanvien;
                    f.ShowDialog();
                    if (f.DialogResult == System.Windows.Forms.DialogResult.OK)
                        LoadData();
                }
            }

            //if (grvMatBang.FocusedRowHandle < 0)
            //{
            //    DialogBox.Error("Vui lòng chọn [Mặt bằng], xin cảm ơn.");
            //    return;
            //}

            //if ((decimal)grvMatBang.GetFocusedRowCellValue("TongNo") > 0)
            //{
            //    int MaMB = (int)grvMatBang.GetFocusedRowCellValue("MaMB");
            //    var frm = new frmPaid() { objnhanvien = objnhanvien, MaMB = MaMB };
            //    frm.NgayNhap = (DateTime?)grvMatBang.GetFocusedRowCellValue("NgayNhap");
            //    frm.soTien = (decimal)grvMatBang.GetFocusedRowCellValue("ConLai");
            //    frm.MaSoMB = grvMatBang.GetFocusedRowCellValue("MaSoMB").ToString();
            //    frm.ChuKyID = (int?)gvPhieuDangKy.GetFocusedRowCellValue("ChuKyID");
            //    frm.ShowDialog();
            //    if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
            //        LoadData();
            //}
            //else
            //    DialogBox.Alert("[Mặt bằng] này đã thu phí vệ sinh. Vui lòng kiểm tra lại, xin cảm ơn.");
        }

        void SavePrint(string barCode)
        {

            var obj = new PhiQuanLy_LichSu();
            long ID = (long)grvMatBang.GetFocusedRowCellValue("ID");        
            obj.Barcode = barCode;
            obj.NgayNhap=db.GetSystemDate();
            obj.MaLS = ID;
            obj.GhiChu = "";
            obj.MaNV = objnhanvien.MaNV;
            db.PhiQuanLy_LichSus.InsertOnSubmit(obj);

            db.SubmitChanges();
        }

        DateTime? getDate(string dateText)
        {
            if (dateText == "") return null;
            string[] ns = dateText.Split('/');
            if (ns.Length == 3)
            {
                if (int.Parse(ns[1]) > 12)
                    return new DateTime(int.Parse(ns[2].Substring(0, 4)), int.Parse(ns[0]), int.Parse(ns[1]));
                else
                    return new DateTime(int.Parse(ns[2].Substring(0, 4)), int.Parse(ns[1]), int.Parse(ns[0]));
            }
            else if (ns.Length == 2)
            {
                if (int.Parse(ns[1]) > 12)
                    return new DateTime(DateTime.Now.Year, int.Parse(ns[0]), int.Parse(ns[1]));
                else
                    return new DateTime(DateTime.Now.Year, int.Parse(ns[1]), int.Parse(ns[0]));
            }
            else return null;
        }

        private void itemYear_EditValueChanged(object sender, EventArgs e)
        {
            //if (!first) LoadData();
        }

        private void itemMonth_EditValueChanged(object sender, EventArgs e)
        {
           // if (!first) LoadData();
        }

        private void itemToaNha_EditValueChanged(object sender, EventArgs e)
        {
            if (!first) LoadData();
        }

        private void itemPrintGB_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int[] rows = grvMatBang.GetSelectedRows();
            if (rows.Length <= 0)
            {
                DialogBox.Error("Vui lòng chọn [Mặt bằng] cần in đề nghị thanh toán");
                return;
            }

            if (DialogBox.Question("Vui lòng cài đặt máy in mặc định trước khi [In] giấy báo.\r\nBạn có muốn tiếp tục [In] không?") == System.Windows.Forms.DialogResult.No) return;

            int count = 1;
            var wait = DialogBox.WaitingForm();
            try
            {
                var year = Convert.ToInt32(itemYear.EditValue.ToString());
                var month = GetMonth();
                int maTN = itemToaNha.EditValue != null ? Convert.ToInt32(itemToaNha.EditValue) : 0;

                foreach (var item in rows)
                {
                    if ((bool)grvMatBang.GetFocusedRowCellValue("IsConfirm"))
                    {
                        string barCodeString = string.Format("1{0:yyyyMMddhhmmss}", db.GetSystemDate());
                        //using (var rpt = new ReportMisc.DichVu.HoaDon.ReportTemplate.rptManagermentFee(Convert.ToInt32(grvMatBang.GetRowCellValue(item, "ID")), (DateTime)grvMatBang.GetRowCellValue(item, "NgayNhap"), month, year, (int)grvMatBang.GetRowCellValue(item, "MaMB"), (int)grvMatBang.GetRowCellValue(item, "MaKH"), maTN, barCodeString))
                        //{
                        //    //if (rpt.PrintDialog() == System.Windows.Forms.DialogResult.OK)
                        //    //    SavePrint(barCodeString);
                        //    rpt.Print();
                        //    SavePrint(barCodeString);
                        //}
                        //rpt.ShowPreviewDialog();
                        //rpt.Print();
                    }
                    Thread.Sleep(50);

                    count++;
                    wait.SetCaption(string.Format("Đã in {0:n0}/{1:n0} giấy báo", count, rows.Length));
                }
            }
            catch { }
            finally { wait.Close(); wait.Dispose(); }
        }

        private void itemViewGB_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvMatBang.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn [Mặt bằng], xin cảm ơn.");
                return;
            }

            if (!(bool)grvMatBang.GetFocusedRowCellValue("IsConfirm"))
            {
                DialogBox.Alert("Vui lòng chọn [Mặt bằng] đã duyệt, xin cảm ơn.");
                return;
            }

            string barCodeString = string.Format("1{0:yyyyMMddhhmmss}", db.GetSystemDate());
            var year = Convert.ToInt32(itemYear.EditValue.ToString());
            var month = GetMonth();
            int maTN = itemToaNha.EditValue != null ? Convert.ToInt32(itemToaNha.EditValue) : 0;
            //var rpt = new ReportMisc.DichVu.HoaDon.ReportTemplate.rptManagermentFee(Convert.ToInt32(grvMatBang.GetFocusedRowCellValue("ID")), (DateTime)grvMatBang.GetFocusedRowCellValue("NgayNhap"), month, year, (int)grvMatBang.GetFocusedRowCellValue("MaMB"), (int)grvMatBang.GetFocusedRowCellValue("MaKH"), maTN,barCodeString);
            //rpt.ShowPreviewDialog();
        }

        private void itemDangKy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvMatBang.FocusedRowHandle < 0)
                return;
            var f = new DangKy.frmEdit();
            f.objNV = objnhanvien;
            f.MaKH = (int?)grvMatBang.GetFocusedRowCellValue("MaKH");
            f.KhachHang = grvMatBang.GetFocusedRowCellValue("KhachHang").ToString();
            f.MaMB = (int?)grvMatBang.GetFocusedRowCellValue("MaMB");
            var objmb = db.mbMatBangs.FirstOrDefault(p => p.MaMB == (int?)grvMatBang.GetFocusedRowCellValue("MaMB"));
            //f.SoTien = objmb.PhiQuanLy;
            f.MaSoMB = grvMatBang.GetFocusedRowCellValue("MaSoMB").ToString();
            f.ShowDialog();
            if (f.DialogResult == System.Windows.Forms.DialogResult.OK)
                LoadDetail();
        }

        private void itemEditPDK_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gvPhieuDangKy.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn [Phiếu đăng ký], xin cảm ơn.");
                return;
            }

            var f = new DangKy.frmEdit();
            f.objNV = objnhanvien;
            f.ID = (int?)gvPhieuDangKy.GetFocusedRowCellValue("ID");
            f.ShowDialog();
            if (f.DialogResult == System.Windows.Forms.DialogResult.OK)
                LoadDetail();
        }

        private void itemDeletePDK_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gvPhieuDangKy.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn [Phiếu đăng ký], xin cảm ơn.");
                return;
            }

            if (DialogBox.Question("Bạn có chắc chắn muốn xóa không?") == System.Windows.Forms.DialogResult.No) return;
            try
            {
                var o = db.pqlDangKies.Single(p => p.ID == (int?)gvPhieuDangKy.GetFocusedRowCellValue("ID"));
                db.pqlDangKies.DeleteOnSubmit(o);
                db.SubmitChanges();
                gvPhieuDangKy.DeleteSelectedRows();

                LoadDetail();
            }
            catch
            {
                DialogBox.Alert("Xóa không thành công. Vui lòng kiểm tra lại, xin cảm ơn.");
            }
        }

        private void itemInPDK_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DialogBox.Alert("Vui lòng cung cấp mẫu đăng ký phí quản lý, xin cảm ơn.");
        }

        private void itemCreateFee_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            grvMatBang.SelectAll();
            byte MaTN = Convert.ToByte(itemToaNha.EditValue ?? 0);
            var rows = db.mbMatBangs.Where(p => p.mbTangLau.mbKhoiNha.MaTN == MaTN).Select(p => p.MaMB).ToList();
            var f = new frmChooseDate();
            f.ShowDialog();
            if (f.DialogResult == System.Windows.Forms.DialogResult.OK & f.PickedDate != null)
            {
                var wait = DialogBox.WaitingForm();
                try
                {
                    int count = 1;
                    listMB = new List<ItemData>();
                    foreach (var i in rows)
                    {
                        var objHDT = db.thueHopDongs.FirstOrDefault(p => p.MaMB == i && p.MaTT != 6);
                        if (objHDT == null)
                        {
                            listMB = db.mbMatBangs.Where(p => p.MaMB == i)
                                .Select(p => new ItemData()
                                {
                                    ID = p.MaMB, CusID = p.MaKH.Value,
                                    //Fee = p.PhiQuanLy ?? 0, Fee2 = p.PhiVeSinh ?? 0,
                                    //DateFee = p.BatDauTinhPhi, DateFeeVS = p.NgayTinhPVS, DateEndFee = p.KetThucTinhPhi,
                                    //IsChuyen = p.IsChuyenThangSau.GetValueOrDefault(),
                                    ChuKy = 1
                                }).ToList();
                            PhiQuanLy(f.PickedDate.Value);
                            wait.SetCaption(string.Format("Đã tính phí {0}/{1} mặt bằng", count, rows.Count));
                            count++;
                        }
                    }
                }
                catch { }
                finally { wait.Close(); wait.Dispose(); }

                LoadData();
            }
        }

        void PhiQuanLy(DateTime date)
        {
            foreach (var item in listMB)
            {
                if (SqlMethods.DateDiffDay(item.DateFee, date) >= 0)
                {

                    var objCN = db.cnLichSus.FirstOrDefault(p => p.MaMB == item.ID && SqlMethods.DateDiffMonth(date, p.NgayNhap) == 0);
                    if (objCN != null)
                        return;
                    db.cnLichSu_addPQL(item.ID, item.CusID, date, GetFee(item, date));

                    var objPDK = db.pqlDangKies.Where(p => SqlMethods.DateDiffDay(p.NgayDK, date) >= 0 & SqlMethods.DateDiffDay(date, p.NgayKT) >= 0 & p.MaMB == item.ID).FirstOrDefault();
                    if (objPDK != null)
                    {
                        if (objPDK.NgayDK.Value.Month != date.Month | objPDK.NgayDK.Value.Year != date.Year)
                            db.cnLichSu_addPQL(item.ID, item.CusID, date, 0);
                    }
                }
                else
                    db.cnLichSu_addPQL(item.ID, item.CusID, date, 0);
            }
        }

        private decimal GetFee(ItemData item, DateTime date)
        {
            decimal Fee = 0;
            try
            {
                //Ngày đăng ký = Thời gian tính phí (MM/yyyy): Tính phí tháng đầu tiên
                if (item.DateFee.Value.Month == date.Month & item.DateFee.Value.Year == date.Year)
                {
                    if (item.IsChuyen)
                        Fee = 0;
                    else
                    {
                        //Ngày của tháng
                        int day = DateTime.DaysInMonth(item.DateFee.Value.Year, item.DateFee.Value.Month);
                        //Ngày ở thực tế
                        int dayReal = day - item.DateFee.Value.Day;
                        if (item.ChuKy == 1)
                            Fee = Math.Round((item.Fee / day) * (dayReal + 1), 0, MidpointRounding.AwayFromZero);//+1 tính luôn ngày vào ở
                        else
                            Fee = (decimal)(Math.Round((item.Fee / day) * (dayReal + 1), 0, MidpointRounding.AwayFromZero) + (item.ChuKy - 1) * item.Fee);
                    }
                }
                else//Tháng tiếp theo
                {
                    if (item.IsChuyen)
                    {
                        //Chuyen sang thang sau tinh phi thang dau tien
                        if ((item.DateFee.Value.Month + 1) == date.Month & item.DateFee.Value.Year == date.Year)
                        {
                            //Ngày của tháng
                            int day = DateTime.DaysInMonth(item.DateFee.Value.Year, item.DateFee.Value.Month);
                            //Ngày ở thực tế
                            int dayReal = day - item.DateFee.Value.Day;
                            //Fee = Math.Round((item.Fee / day) * (dayReal + 1), 0, MidpointRounding.AwayFromZero) + item.Fee;//+1 tính luôn ngày vào ở
                            if (item.ChuKy == 1)
                                Fee = Math.Round((item.Fee / day) * (dayReal + 1), 0, MidpointRounding.AwayFromZero);//+1 tính luôn ngày vào ở
                            else
                                Fee = (decimal)(Math.Round((item.Fee / day) * (dayReal + 1), 0, MidpointRounding.AwayFromZero) + (item.ChuKy - 1) * item.Fee);
                   
                        }//
                        else
                            Fee = (decimal)item.ChuKy * item.Fee;
                    }
                    else
                        Fee = (decimal)item.ChuKy * item.Fee;
                }

                var objPDK = db.pqlDangKies.Where(p => SqlMethods.DateDiffDay(p.NgayDK, date) >= 0 & SqlMethods.DateDiffDay(date, p.NgayKT) >= 0 & p.MaMB == item.ID).FirstOrDefault();
                if (objPDK != null)
                {
                    if (objPDK.NgayDK.Value.Month == date.Month & item.DateFee.Value.Year == date.Year)
                        Fee = (decimal)item.ChuKy * objPDK.PhaiThu ?? 0;
                    else
                        Fee = 0;
                }
            }
            catch { Fee = 0; }

            return Fee;
        }

        private void itemPayDebt_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvMatBang.FocusedRowHandle < 0)
            {
                DialogBox.Error("Vui lòng chọn [Mặt bằng], xin cảm ơn.");
                return;
            }            

            if ((decimal)gvNoTruoc.GetFocusedRowCellValue("TongNo") > 0)
            {
                int MaMB = (int)gvNoTruoc.GetFocusedRowCellValue("MaMB");

                //Check DKPQL
                DateTime date = (DateTime)gvNoTruoc.GetFocusedRowCellValue("NgayNhap");
                var objPDK = db.pqlDangKies.Where(p => p.MaMB == MaMB & SqlMethods.DateDiffDay(p.NgayDK, date) >= 0
                & SqlMethods.DateDiffDay(date, p.NgayKT) >= 0).FirstOrDefault();
                
                var frm = new frmPaid() { objnhanvien = objnhanvien, MaMB = MaMB };
                frm.soTien = (decimal)gvNoTruoc.GetFocusedRowCellValue("ConLai");
                frm.MaSoMB = gvNoTruoc.GetFocusedRowCellValue("MaSoMB").ToString();
                frm.date = (DateTime)gvNoTruoc.GetFocusedRowCellValue("NgayNhap");
                frm.MaKH = Convert.ToInt32(gvNoTruoc.GetFocusedRowCellValue("MaKH"));
                if (objPDK != null)
                    frm.ChuKyID = objPDK.ChuKyID;

                frm.ShowDialog();
                if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                    LoadData();
            }
            else
                DialogBox.Alert("[Mặt bằng] này đã thu phí vệ sinh. Vui lòng kiểm tra lại, xin cảm ơn.");
        }

        private void xtrtabChiTietPQL_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            LoadDetail();
        }

        private void itemPaidMullti_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvMatBang.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn [Mặt bằng], xin cảm ơn.");
                return;
            }

            var f = new DichVu.HoaDon.frmPaidMultiV3();
            int month = GetMonth();
            int year = Convert.ToInt32(itemYear.EditValue.ToString());
            DateTime DateSystem = db.GetSystemDate();
            if (year == DateSystem.Year & month == DateSystem.Month)
            {
                f.Year = year;
                f.Month = month;
            }
            else
            {
                if (year < DateSystem.Year)
                {
                    if (month == 12)
                    {
                        f.Month = 1;
                        f.Year = year + 1;
                    }
                    else
                    {
                        f.Month = month + 1;
                        f.Year = year;
                    }
                }
                else
                {
                    f.Year = year;
                    f.Month = month;
                }
            }
            f.objnhanvien = objnhanvien;
            f.MaMB = (int)grvMatBang.GetFocusedRowCellValue("MaMB");
            f.MaSoMB = grvMatBang.GetFocusedRowCellValue("MaSoMB").ToString();
            f.MaKH = (int)grvMatBang.GetFocusedRowCellValue("MaKH");
            f.ShowDialog();
            if (f.DialogResult == DialogResult.OK)
                LoadData();
        }

        private void itemTangGiam_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int[] rows = grvMatBang.GetSelectedRows();
            if (rows.Length <= 0)
            {
                DialogBox.Alert("Vui lòng chọn [Mặt bằng], xin cảm ơn.");
                return;
            }

            //Check KhoaSo
            //DichVu.KhoaSo.ManagerCls.IsFirst = true;
            //DichVu.KhoaSo.ManagerCls.Month = GetMonth();
            //DichVu.KhoaSo.ManagerCls.TowerID = Convert.ToByte(itemToaNha.EditValue);
            //DichVu.KhoaSo.ManagerCls.TowerName = lookUpToaNha.GetDisplayText(itemToaNha.EditValue);
            //DichVu.KhoaSo.ManagerCls.Year = Convert.ToInt32(itemYear.EditValue);
            //if (DichVu.KhoaSo.ManagerCls.CheckEditData())
            //    return;

            var f = new frmAdjust();
            f.ShowDialog();
            if (f.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                var wait = DialogBox.WaitingForm();
                try
                {
                    var year = Convert.ToInt32(itemYear.EditValue.ToString());
                    var month = GetMonth();
                    int count = 1;

                    foreach (var i in rows)
                    {
                        //update PQL
                        db.cnLichSu_adjust((int?)grvMatBang.GetRowCellValue(i, "MaMB"), 12, month, year, f.Money, f.GhiChu, objnhanvien.MaNV);
                        //var objLS = new cnlsDieuChinh();
                        //objLS.SoTienTruocDC = (decimal?)grvMatBang.GetFocusedRowCellValue("PhiQL");
                        //objLS.SoTien = f.Money;
                        //objLS.GhiChu =f.GhiChu;
                        //objLS.MaNV = objnhanvien.MaNV;
                        //objLS.NgayDC = db.GetSystemDate();
                        //db.cnlsDieuChinhs.InsertOnSubmit(objLS);
                        //db.SubmitChanges();
                                                
                        wait.SetCaption(string.Format("Đã tính phí {0}/{1} mặt bằng", count, rows.Length));
                        count++;
                    }
                }
                catch { }
                finally { wait.Close(); wait.Dispose(); }

                LoadData();
            }
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            if (grvMatBang.GetFocusedRowCellValue("ID") != null)
            {
                db = new MasterDataContext();
                var objPT = db.PhiQuanLy_LichSus.Single(p => p.ID== (int)grv_LS.GetFocusedRowCellValue("ID"));
                if (objPT.Barcode == null || objPT.Barcode == "")
                    DialogBox.Alert("[Phiếu thu] này chưa phát sinh mã vạch. Vui lòng kiểm tra lại, xin cảm ơn.");
                else
                {
                    //var frm = new ReportMisc.frmBarCode(objPT.Barcode);
                    //frm.ShowDialog();
                }
            }
            else
                DialogBox.Alert("Vui lòng chọn [Phiếu thu], xin cảm ơn.");
        
        }

        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void itemViewGBN_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvMatBang.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn [Mặt bằng], xin cảm ơn.");
                return;
            }

            if (!(bool)grvMatBang.GetFocusedRowCellValue("IsConfirm"))
            {
                DialogBox.Alert("Vui lòng chọn [Mặt bằng] đã duyệt, xin cảm ơn.");
                return;
            }

            var year = Convert.ToInt32(itemYear.EditValue.ToString());
            var month = GetMonth();
            int maTN = itemToaNha.EditValue != null ? Convert.ToInt32(itemToaNha.EditValue) : 0;
            //var rpt = new ReportMisc.DichVu.Quy.rptGiayNhan2Lien(month, year, (int)grvMatBang.GetFocusedRowCellValue("MaMB"), maTN, objnhanvien);
            //rpt.ShowPreviewDialog();
        }

        private void itemPrintGBN_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int[] rows = grvMatBang.GetSelectedRows();
            if (rows.Length <= 0)
            {
                DialogBox.Error("Vui lòng chọn [Mặt bằng] cần in đề nghị thanh toán");
                return;
            }

            if (DialogBox.Question("Vui lòng cài đặt máy in mặc định trước khi [In] giấy báo.\r\nBạn có muốn tiếp tục [In] không?") == System.Windows.Forms.DialogResult.No) return;

            int count = 1;
            var wait = DialogBox.WaitingForm();
            try
            {
                var year = Convert.ToInt32(itemYear.EditValue.ToString());
                var month = GetMonth();
                int maTN = itemToaNha.EditValue != null ? Convert.ToInt32(itemToaNha.EditValue) : 0;

                foreach (var item in rows)
                {
                    if ((bool)grvMatBang.GetRowCellValue(item, "IsConfirm"))
                    {
                        //string barCodeString = string.Format("1{0:yyyyMMddhhmmss}", db.GetSystemDate());
                        try
                        {
                            //using (var rpt = new ReportMisc.DichVu.Quy.rptGiayNhan2Lien(month, year, (int)grvMatBang.GetRowCellValue(item, "MaMB"), maTN, objnhanvien))
                            //{
                            //    rpt.Print();
                            //    //if (rpt.PrintDialog() == System.Windows.Forms.DialogResult.OK)
                            //    //    SavePrint(barCodeString);
                            //}
                        }
                        catch { }
                        //rpt.ShowPreviewDialog();
                        //rpt.Print();
                    }
                    Thread.Sleep(50);

                    count++;
                    wait.SetCaption(string.Format("Đã in {0:n0}/{1:n0} giấy báo", count, rows.Length));
                }
            }
            catch { }
            finally { wait.Close(); wait.Dispose(); }
        }

        private void itemDuyet_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Confirm(true);
        }

        private void itemKhongDuyet_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Confirm(false);
        }

        void Confirm(bool val)
        {
            int[] rows = grvMatBang.GetSelectedRows();
            if (rows.Length <= 0)
            {
                DialogBox.Alert("Vui lòng chọn [Mặt bằng], xin cảm ơn.");
                return;
            }

            if (DialogBox.Question("Bạn có chắc chắn muốn thực hiện thao tác này không?") == System.Windows.Forms.DialogResult.No) return;

            //var f = new DichVu.Confirm.frmSign();
            //f.ShowDialog();
            //if (f.DialogResult == System.Windows.Forms.DialogResult.OK)
            //{
            //    var wait = DialogBox.WaitingForm();
            //    int count = 0;
            //    foreach (var i in rows)
            //    {
            //        try
            //        {
            //            db.cnLichSu_updateIsDuyet(Convert.ToInt32(grvMatBang.GetRowCellValue(i, "ID")), f.Description, objnhanvien.MaNV, val);
            //            count++;

            //            wait.SetCaption(string.Format("Đã cập nhật {0:n0}/{1:n0} mặt bằng", count, rows.Length));
            //        }
            //        catch { }
            //    }

            //    wait.Close();
            //    wait.Dispose();

            //    LoadData();
            //}
        }

        private void itemCreateFeeProduct_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int[] rows = grvMatBang.GetSelectedRows();
            if (rows.Length <= 0)
            {
                DialogBox.Alert("Vui lòng chọn [Mặt bằng], xin cảm ơn.");
                return;
            }

            var f = new frmChooseDate();
            f.ShowDialog();
            if (f.DialogResult == System.Windows.Forms.DialogResult.OK & f.PickedDate != null)
            {
                var wait = DialogBox.WaitingForm();
                try
                {
                    int count = 1;
                    listMB = new List<ItemData>();
                    foreach (var i in rows)
                    {
                        int? MaMB = (int?)grvMatBang.GetRowCellValue(i, "MaMB");
                        var objHDT = db.thueHopDongs.FirstOrDefault(p => p.MaMB == MaMB && p.MaTT != 6);
                        if (objHDT == null)
                        {
                            listMB = db.mbMatBangs.Where(p => p.MaMB == MaMB)
                                .Select(p => new ItemData()
                                {
                                    ID = p.MaMB, CusID = p.MaKH.Value,
                                    //Fee = p.PhiQuanLy ?? 0,
                                    //Fee2 = p.PhiVeSinh ?? 0,
                                    //DateFee = p.BatDauTinhPhi, DateFeeVS = p.NgayTinhPVS, DateEndFee = p.KetThucTinhPhi,
                                    //IsChuyen = p.IsChuyenThangSau.GetValueOrDefault(),
                                    ChuKy = 1
                                }).ToList();

                            PhiQuanLy(f.PickedDate.Value);

                            wait.SetCaption(string.Format("Đã tính phí {0}/{1} mặt bằng", count, rows.Length));
                            count++;
                        }
                    }
                }
                catch { }
                finally { wait.Close(); wait.Dispose(); }

                LoadData();
            }
        }

        private void itemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            List<ItemExport> ts = new List<ItemExport>();
            IEnumerable<mbMatBang> temp;

            DataTable dt = new DataTable();

            int maTN = itemToaNha.EditValue != null ? Convert.ToInt32(itemToaNha.EditValue) : 0;

            temp = db.mbMatBangs.Where(p => p.mbTangLau.mbKhoiNha.tnToaNha.MaTN == maTN & p.MaKH != null).OrderBy(p => p.MaSoMB);

            foreach (var item in temp)
            {
                ItemExport obj = new ItemExport()
                {
                    MaMB = item.MaMB,
                    MaSoMB = item.MaSoMB,
                    MaKH = item.MaKH.HasValue ? item.MaKH : null,
                    MaSoKH = item.MaKH.HasValue ? item.tnKhachHang.KyHieu : "",
                    KhachHang = item.MaKH.HasValue ? (item.tnKhachHang.IsCaNhan.HasValue ? (item.tnKhachHang.IsCaNhan.Value ? item.tnKhachHang.HoKH + " " + item.tnKhachHang.TenKH : item.tnKhachHang.CtyTen) : "") : "",
                    SoTien = 0,
                    DienGiai = ""
                };
                ts.Add(obj);
            }
            dt = SqlCommon.LINQToDataTable(ts);
            ExportToExcel.exportDataToExcel("MẪU NHẬP LIỆU PHÍ QUẢN LÝ", dt);
        }

        private void itemImport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var f = new frmImport();
            f.objnhanvien = objnhanvien;
            f.ShowDialog();
        }

        private void itemToaCNPQL_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int[] rows = grvMatBang.GetSelectedRows();
            if (rows.Length <= 0)
            {
                DialogBox.Alert("Vui lòng chọn [Mặt bằng], xin cảm ơn.");
                return;
            }

            var f = new frmChooseDate();
            f.ShowDialog();
            if (f.DialogResult == System.Windows.Forms.DialogResult.OK & f.PickedDate != null)
            {
                var wait = DialogBox.WaitingForm();
                try
                {
                    int count = 1;
                    listMB = new List<ItemData>();
                    foreach (var i in rows)
                    {
                        int? MaMB = (int?)grvMatBang.GetRowCellValue(i, "MaMB");
                        var objHDT = db.thueHopDongs.FirstOrDefault(p => p.MaMB == MaMB && p.MaTT != 6);
                        if (objHDT == null)
                            listMB = db.mbMatBangs.Where(p => p.MaMB == MaMB)
                                .Select(p => new ItemData()
                                {
                                    //ID = p.MaMB, CusID = p.MaKH.Value, Fee = p.PhiQuanLy ?? 0, Fee2 = p.PhiVeSinh ?? 0,
                                    //DateFee = p.BatDauTinhPhi, DateFeeVS = p.NgayTinhPVS, DateEndFee = p.KetThucTinhPhi,
                                    //IsChuyen = p.IsChuyenThangSau.GetValueOrDefault(), ChuKy = 1
                                }).ToList();

                        PhiQuanLy(f.PickedDate.Value);

                        wait.SetCaption(string.Format("Đã tính phí {0}/{1} mặt bằng", count, rows.Length));
                        count++;
                    }
                }
                catch { }
                finally { wait.Close(); wait.Dispose(); }

                LoadData();
            }
        }
    }

    public class ItemData
    {
        public int ID { get; set; }
        public int CusID { get; set; }
        public decimal Fee { get; set; }
        public decimal Fee2 { get; set; }
        public DateTime? DateFee { get; set; }
        public DateTime? DateEndFee { get; set; }
        public DateTime? DateFeeVS { get; set; }
        public bool IsChuyen { get; set; }
        public int? ChuKy { get; set; }
    }

    class ItemExport
    {
        public int MaMB { get; set; }
        public string MaSoMB { get; set; }
        public int? MaKH { get; set; }
        public string MaSoKH { get; set; }
        public string KhachHang { get; set; }
        public decimal SoTien { get; set; }
        public string DienGiai { get; set; }
    }
}