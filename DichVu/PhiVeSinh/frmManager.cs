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

namespace DichVu.PhiVeSinh
{
    public partial class frmManager : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public tnNhanVien objnhanvien;
        DateTime now;
        string MM = "";
        bool first = true;
        List<Item> listMB;

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
            var wait = DialogBox.WaitingForm();
            try
            {
                var year = Convert.ToInt32(itemYear.EditValue.ToString());
                var month = GetMonth();
                int maTN = 0;
                maTN = itemToaNha.EditValue != null ? Convert.ToInt32(itemToaNha.EditValue) : 0;

                gcMatBang.DataSource = db.PhiVeSinh_selectBySuperAdminV2(year, month, maTN).ToList();
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
            var PhiQLDaThu = db.PhiVeSinhs.Where(p => SqlMethods.DateDiffDay(TuNgay, p.ThangThanhToan) >= 0 
                & SqlMethods.DateDiffDay(p.ThangThanhToan, DenNgay) >= 0
                & p.MaMB == MaMB);
            if (PhiQLDaThu.Count() <= 0)
                result = "Chưa thanh toán";
            else
                result = "Đã thanh toán";

            return result;
        }

        private decimal getPhiVS(int MaMB)
        {
            var pql = 0;
            //var pqlhdt = db.thueHopDongs.Where(p => p.MaMB == MaMB).Select(p => p.PhiQL).FirstOrDefault() ?? 0;
            var conno = db.PhiVeSinhs.Where(p => p.mbMatBang.MaMB == MaMB).Sum(p => p.ConNo) ?? 0;
            return pql + conno;
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
            if (grvMatBang.FocusedRowHandle < 0)
            {
                gcLichSu.DataSource = null;
                gcNoTruoc.DataSource = null;
                return;
            }

            switch (xtrtabChiTietPQL.SelectedTabPageIndex)
            {
                case 0:
                    int maMB = (int)grvMatBang.GetFocusedRowCellValue("MaMB");  
                    var year = Convert.ToInt32(itemYear.EditValue.ToString());
                    var month = GetMonth();

                    gcNoTruoc.DataSource = db.PhiVeSinh_selectBySuperAdminDebt(year, month, maMB);
                    break;
                case 1:
                    long? ID = (long?)grvMatBang.GetFocusedRowCellValue("ID");  
                    gcLichSu.DataSource = db.cnlsDieuChinhs.Where(p => p.LichSuID == ID).
                        AsEnumerable().Select((p, index) => new
                        {
                            STT = index + 1,
                            NhanVien = p.tnNhanVien.HoTenNV,
                            p.SoTien,
                            p.NgayDC,
                            p.GhiChu
                        }).ToList();
                    break;
            }
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
                        frm.MaKH = Convert.ToInt32(grvMatBang.GetFocusedRowCellValue("MaKH"));
                        frm.ShowDialog();
                        if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                            LoadData();
                    }
                    else
                        DialogBox.Alert("[Mặt bằng] này đã thu phí vệ sinh. Vui lòng kiểm tra lại, xin cảm ơn.");  
                }
                else
                {
                    var listData = new List<ItemData>();
                    foreach (var i in rows)
                    {
                        if (Convert.ToDecimal(grvMatBang.GetRowCellValue(i, "PhiQL")) > 0)
                        {
                            var obj = new ItemData();
                            obj.DiaChi = grvMatBang.GetRowCellValue(i, "DiaChi").ToString();
                            obj.DienGiai = "";
                            obj.DotThu = DateTime.Parse(grvMatBang.GetRowCellValue(i, "NgayNhap").ToString());
                            obj.MaKH = Convert.ToInt32(grvMatBang.GetRowCellValue(i, "MaKH"));
                            obj.MaMB = Convert.ToInt32(grvMatBang.GetRowCellValue(i, "MaMB"));
                            obj.MaSoMB = grvMatBang.GetRowCellValue(i, "MaSoMB").ToString();
                            obj.NgayThu = getDate(string.Format("22/{0:MM/yyyy}", obj.DotThu)).Value;
                            obj.NguoiNop = grvMatBang.GetRowCellValue(i, "KhachHang").ToString();
                            obj.SoTien = Convert.ToDecimal(grvMatBang.GetRowCellValue(i, "PhiQL"));
                            listData.Add(obj);
                        }
                    }

                    var f = new frmPaidMulti();
                    f.listData = listData;
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

            //if ((decimal)grvMatBang.GetFocusedRowCellValue("ConLai") > 0)
            //{
            //    int MaMB = (int)grvMatBang.GetFocusedRowCellValue("MaMB");  
            //    frmThanhToan frm = new frmThanhToan() { objnhanvien = objnhanvien, MaMB = MaMB };
            //    frm.soTien = (decimal)grvMatBang.GetFocusedRowCellValue("ConLai");  
            //    frm.date = getDate(string.Format("{0}/{1}/{2}", "01", GetMonth(), (int)itemYear.EditValue));
            //    frm.ShowDialog();
            //    if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
            //        LoadData();
            //}
            //else
            //    DialogBox.Alert("[Mặt bằng] này đã thu phí vệ sinh. Vui lòng kiểm tra lại, xin cảm ơn.");              
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

        private void itemMonth_EditValueChanged(object sender, EventArgs e)
        {
            if (!first)
                LoadData();
        }

        private void itemToaNha_EditValueChanged(object sender, EventArgs e)
        {
            if (!first)
                LoadData();
        }

        private void itemYear_EditValueChanged(object sender, EventArgs e)
        {
            if (!first)
                LoadData();
        }

        private void itemPaidDebt_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvMatBang.FocusedRowHandle < 0)
            {
                DialogBox.Error("Vui lòng chọn [Mặt bằng], xin cảm ơn.");  
                return;
            }

            if ((decimal)gvNoTruoc.GetFocusedRowCellValue("TongNo") > 0)
            {
                int MaMB = (int)gvNoTruoc.GetFocusedRowCellValue("MaMB");  

                var frm = new frmPaid() { objnhanvien = objnhanvien, MaMB = MaMB };
                frm.NgayNhap = (DateTime)gvNoTruoc.GetFocusedRowCellValue("NgayNhap");  
                frm.soTien = (decimal)gvNoTruoc.GetFocusedRowCellValue("ConLai");  
                frm.MaSoMB = gvNoTruoc.GetFocusedRowCellValue("MaSoMB").ToString();
                frm.MaKH = Convert.ToInt32(gvNoTruoc.GetFocusedRowCellValue("MaKH"));
                frm.ShowDialog();
                if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                    LoadData();
            }
            else
                DialogBox.Alert("[Mặt bằng] này đã thu phí vệ sinh. Vui lòng kiểm tra lại, xin cảm ơn.");  
        }

        private void itemCreateFee_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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
                //Check Khoa So
                //DichVu.KhoaSo.ManagerCls.IsFirst = true;
                //DichVu.KhoaSo.ManagerCls.Month = f.PickedDate.Value.Month;
                //DichVu.KhoaSo.ManagerCls.TowerID = Convert.ToByte(itemToaNha.EditValue);
                //DichVu.KhoaSo.ManagerCls.TowerName = lookUpToaNha.GetDisplayText(itemToaNha.EditValue);
                //DichVu.KhoaSo.ManagerCls.Year = f.PickedDate.Value.Year;
                //if (DichVu.KhoaSo.ManagerCls.CheckEditData())
                //    return;

                var wait = DialogBox.WaitingForm();
                try
                {
                    var year = Convert.ToInt32(itemYear.EditValue.ToString());
                    var month = GetMonth();
                    int count = 1;
                    foreach (var i in rows)
                    {
                        listMB = new List<Item>();
                        //listMB = db.mbMatBangs.Where(p => p.MaMB == (int?)grvMatBang.GetRowCellValue(i, "MaMB")).Select(p => new Item() { ID = p.MaMB, CusID = p.MaKH.Value, Fee = p.PhiQuanLy ?? 0, Fee2 = p.PhiVeSinh ?? 0, DateFee = p.BatDauTinhPhi, DateFeeVS = p.NgayTinhPVS, DateEndFee = p.KetThucTinhPhi, IsChuyen = p.IsChuyenThangSau.GetValueOrDefault() }).ToList();

                        PhiVeSinh(f.PickedDate.Value);

                        wait.SetCaption(string.Format("Đã tính phí {0}/{1} mặt bằng", count, rows.Length));
                        count++;
                    }
                }
                catch { }
                finally { wait.Close(); wait.Dispose(); }

                LoadData();
            }
        }

        void PhiVeSinh(DateTime date)
        {
            foreach (var item in listMB)
            {
                if (SqlMethods.DateDiffDay(item.DateFeeVS, date) >= 0)
                    db.cnLichSu_addPVS(item.ID, item.CusID, date, item.Fee2);
                else
                    db.cnLichSu_addPVS(item.ID, item.CusID, date, 0);
            }
        }

        private void itemPaidMulti_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvMatBang.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn [Mặt bằng], xin cảm ơn.");  
                return;
            }

            var f = new DichVu.HoaDon.frmPaidMultiV3();            
            var month = GetMonth();
            if (month - 1 == 0)
            {
                f.Month = 12;
                f.Year = Convert.ToInt32(itemYear.EditValue.ToString()) - 1;
            }
            else
            {
                f.Month = month - 1;
                f.Year = Convert.ToInt32(itemYear.EditValue.ToString());
            }
            f.objnhanvien = objnhanvien;
            f.MaMB = (int)grvMatBang.GetFocusedRowCellValue("MaMB");  
            f.MaSoMB = grvMatBang.GetFocusedRowCellValue("MaSoMB").ToString();
            f.MaKH = (int)grvMatBang.GetFocusedRowCellValue("MaKH");  
            f.ShowDialog();
            if (f.DialogResult == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void itemDieuChinh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
             int[] rows = grvMatBang.GetSelectedRows();
            if (rows.Length <= 0)
            {
                DialogBox.Alert("Vui lòng chọn [Mặt bằng], xin cảm ơn.");  
                return;
            }

            //Check KhoaSo
            DichVu.KhoaSo.ManagerCls.IsFirst = true;
            DichVu.KhoaSo.ManagerCls.Month = GetMonth();
            DichVu.KhoaSo.ManagerCls.TowerID = Convert.ToByte(itemToaNha.EditValue);
            DichVu.KhoaSo.ManagerCls.TowerName = lookUpToaNha.GetDisplayText(itemToaNha.EditValue);
            DichVu.KhoaSo.ManagerCls.Year = Convert.ToInt32(itemYear.EditValue);
            if (DichVu.KhoaSo.ManagerCls.CheckEditData())
                return;

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
                        db.cnLichSu_adjust((int?)grvMatBang.GetRowCellValue(i, "MaMB"), 13, month, year, f.Money, f.GhiChu, objnhanvien.MaNV);
                                                
                        wait.SetCaption(string.Format("Đã tính phí {0}/{1} mặt bằng", count, rows.Length));
                        count++;
                    }
                }
                catch { }
                finally { wait.Close(); wait.Dispose(); }

                LoadData();
            }
        }

        private void xtrtabChiTietPQL_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            LoadDetail();
        }

        private void itemViewGB_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void itemPrintGB_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

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

            var f = new DichVu.Confirm.frmSign();
            f.ShowDialog();
            if (f.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                var wait = DialogBox.WaitingForm();
                int count = 0;
                foreach (var i in rows)
                {
                    try
                    {
                        db.cnLichSu_updateIsDuyet(Convert.ToInt32(grvMatBang.GetRowCellValue(i, "ID")), f.Description, objnhanvien.MaNV, val);
                        count++;

                        wait.SetCaption(string.Format("Đã cập nhật {0:n0}/{1:n0} mặt bằng", count, rows.Length));
                    }
                    catch { }
                }

                wait.Close();
                wait.Dispose();

                LoadData();
            }
        }

        private void itemCreateFeeProduct_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvMatBang.FocusedRowHandle >= 0)
            {
                //Check KhoaSo
                DichVu.KhoaSo.ManagerCls.IsFirst = true;
                DichVu.KhoaSo.ManagerCls.Month = GetMonth();
                DichVu.KhoaSo.ManagerCls.TowerID = Convert.ToByte(itemToaNha.EditValue);
                DichVu.KhoaSo.ManagerCls.TowerName = lookUpToaNha.GetDisplayText(itemToaNha.EditValue);
                DichVu.KhoaSo.ManagerCls.Year = Convert.ToInt32(itemYear.EditValue);
                DichVu.KhoaSo.ManagerCls.ProductID = (int)grvMatBang.GetFocusedRowCellValue("MaMB");  
                if (DichVu.KhoaSo.ManagerCls.CheckIsLock())
                {
                    DialogBox.Alert(string.Format("Mặt bằng [{0}] đã [Khóa sổ] tháng {1}/{2}.\r\nVui lòng liên hệ Administrator, xin cảm ơn.", grvMatBang.GetFocusedRowCellValue("MaSoMB").ToString(), DichVu.KhoaSo.ManagerCls.Month, DichVu.KhoaSo.ManagerCls.Year));
                    return;
                }

                var f = new frmChooseDate();
                f.ShowDialog();
                if (f.DialogResult == System.Windows.Forms.DialogResult.OK & f.PickedDate != null)
                {
                    var wait = DialogBox.WaitingForm();
                    try
                    {
                        listMB = new List<Item>();
                        //listMB = db.mbMatBangs.Where(p => p.MaMB == (int?)grvMatBang.GetFocusedRowCellValue("MaMB")).Select(p => new Item() { ID = p.MaMB, CusID = p.MaKH.Value, Fee = p.PhiQuanLy ?? 0, Fee2 = p.PhiVeSinh ?? 0, DateFee = p.BatDauTinhPhi, DateFeeVS = p.NgayTinhPVS, DateEndFee = p.KetThucTinhPhi, IsChuyen = p.IsChuyenThangSau.GetValueOrDefault() }).ToList();

                        PhiVeSinh(f.PickedDate.Value);
                    }
                    catch { }
                    finally { wait.Close(); wait.Dispose(); }
                }
            }
            else
                DialogBox.Alert("Vui lòng chọn [Mặt bằng], xin cảm ơn.");  
        }

        private void itemImportNC_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var frm = new frmImport();
            frm.objnhanvien = objnhanvien;
            frm.ShowDialog();
        }

        private void itemExportNC_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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
            ExportToExcel.exportDataToExcel("MẪU NHẬP LIỆU PHÍ VỆ SINH", dt);
        }
    }

    public class ItemData
    {
        public int MaMB { get; set; }
        public string MaSoMB { get; set; }
        public int MaKH { get; set; }
        public string NguoiNop { get; set; }
        public string DiaChi { get; set; }
        public decimal SoTien { get; set; }
        public string DienGiai { get; set; }
        public DateTime NgayThu { get; set; }
        public DateTime DotThu { get; set; }
        public bool ChuyenKhoan { get; set; }
        public int? ChuKyID { get; set; }
    }

    public class Item
    {
        public int ID { get; set; }
        public int CusID { get; set; }
        public decimal Fee { get; set; }
        public decimal Fee2 { get; set; }
        public DateTime? DateFee { get; set; }
        public DateTime? DateEndFee { get; set; }
        public DateTime? DateFeeVS { get; set; }
        public bool IsChuyen { get; set; }
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