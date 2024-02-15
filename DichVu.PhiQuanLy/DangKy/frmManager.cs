using System;
using System.Windows.Forms;
using Library;
using System.Linq;
using System.Data.Linq.SqlClient;

namespace DichVu.PhiQuanLy.DangKy
{
    public partial class frmManager : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public tnNhanVien objnhanvien;
        DateTime now;
        bool first = true;
        public frmManager()
        {
            InitializeComponent();
            db = new MasterDataContext();
            TranslateLanguage.TranslateControl(this);
        }

        private void frmCongNoManager_Load(object sender, EventArgs e)
        {
            Library.HeThongCls.PhanQuyenCls.Authorize(this, objnhanvien, barManager1);

            var list = Library.ManagerTowerCls.GetAllTower(objnhanvien);
            lookUpToaNha.DataSource = list;
            if (list.Count > 0)
                itemToaNha.EditValue = list[0].MaTN;

            now = db.GetSystemDate();
            itemDenNgay.EditValue = now.AddDays(30);
            itemTuNgay.EditValue = now.AddDays(-30);
            LoadData();

            first = false;
        }

        bool GetStatus(int? monthFrom, int? yearFrom, int? montTo, int? yearTo, int? montCom, int? yearCom)
        {
            bool result = false;
            DateTime From = new DateTime((int)yearFrom, (int)monthFrom, 1);
            DateTime To = new DateTime((int)yearTo, (int)montTo, 1);
            DateTime Compare = new DateTime((int)yearCom, (int)montCom, 1);
            result = (SqlMethods.DateDiffMonth(From, Compare) >= 0 & SqlMethods.DateDiffMonth(Compare, To) >= 0) ? true : false;
            return result;
        }

        private void LoadData()
        {
            var wait = DialogBox.WaitingForm();
            try
            {
                var maTN = itemToaNha.EditValue != null ? Convert.ToInt32(itemToaNha.EditValue) : 0;
                DateTime tungay = (DateTime)itemDenNgay.EditValue;
                DateTime denngay = (DateTime)itemTuNgay.EditValue;

                if (itemDenNgay.EditValue == null || itemTuNgay.EditValue == null)
                {
                    gcCongNo.DataSource = null;
                    return;
                }
                else
                {
                    var dada = GetStatus(1, 2015, 5, 2015, 1, 2015);
                    gcCongNo.DataSource = db.pqlDangKies
                        .Where(p => SqlMethods.DateDiffDay(tungay, p.NgayDK) <= 0
                            & SqlMethods.DateDiffDay(p.NgayDK, denngay) <= 0
                            & p.mbMatBang.mbTangLau.mbKhoiNha.MaTN == maTN).Select(p => new
                            {
                                p.ChietKhau,
                                p.ChietKhauID,
                                p.ChuKyID,
                                p.DienGiai,
                                p.SoDK,
                                p.mbMatBang.MaSoMB,
                                p.ID,
                                KhachHang = p.tnKhachHang.IsCaNhan == true ? p.tnKhachHang.HoKH + " " + p.tnKhachHang.TenKH : p.tnKhachHang.CtyTen,
                                p.YearFrom,
                                p.YearTo,
                                p.MonthFrom,
                                p.MonthTo,
                                p.NgayDK,
                                p.NgayKT,
                                T1 = (p.MonthFrom <= 1 & p.MonthTo >= 1) ? (p.PhaiThu / p.ChuKyID) : 0,
                                T2 = (p.MonthFrom <= 2 & p.MonthTo >= 2) ? (p.PhaiThu / p.ChuKyID) : 0,
                                T3 = (p.MonthFrom <= 3 & p.MonthTo >= 3) ? (p.PhaiThu / p.ChuKyID) : 0,
                                T4 = (p.MonthFrom <= 4 & p.MonthTo >= 4) ? (p.PhaiThu / p.ChuKyID) : 0,
                                T5 = (p.MonthFrom <= 5 & p.MonthTo >= 5) ? (p.PhaiThu / p.ChuKyID) : 0,
                                T6 = (p.MonthFrom <= 6 & p.MonthTo >= 6) ? (p.PhaiThu / p.ChuKyID) : 0,
                                T7 = (p.MonthFrom <= 7 & p.MonthTo >= 7) ? (p.PhaiThu / p.ChuKyID) : 0,
                                T8 = (p.MonthFrom <= 8 & p.MonthTo >= 8) ? (p.PhaiThu / p.ChuKyID) : 0,
                                T9 = (p.MonthFrom <= 9 & p.MonthTo >= 9) ? (p.PhaiThu / p.ChuKyID) : 0,
                                T10 = (p.MonthFrom <= 10 & p.MonthTo >= 10) ? (p.PhaiThu / p.ChuKyID) : 0,
                                T11 = (p.MonthFrom <= 11 & p.MonthTo >= 11) ? (p.PhaiThu / p.ChuKyID) : 0,
                                T12 = (p.MonthFrom <= 12 & p.MonthTo >= 12) ? (p.PhaiThu / p.ChuKyID) : 0,
                               // T1 = GetStatus(1, 2015,5, 2015, 1, (int?)tungay.Year) ? (p.PhaiThu / p.ChuKyID) : 0,
                                //T1 = GetStatus(p.MonthFrom, p.YearFrom, p.MonthTo, p.YearTo, 1, (int?)tungay.Year) ? (p.PhaiThu / p.ChuKyID) : 0,
                                //T2 = GetStatus(p.MonthFrom, p.YearFrom, p.MonthTo, p.YearTo, 2, (int?)tungay.Year) ? (p.PhaiThu / p.ChuKyID) : 0,
                                //T3 = GetStatus(p.MonthFrom, p.YearFrom, p.MonthTo, p.YearTo, 3, (int?)tungay.Year) ? (p.PhaiThu / p.ChuKyID) : 0,
                                //T4 = GetStatus(p.MonthFrom, p.YearFrom, p.MonthTo, p.YearTo, 4, (int?)tungay.Year) ? (p.PhaiThu / p.ChuKyID) : 0,
                                //T5 = GetStatus(p.MonthFrom, p.YearFrom, p.MonthTo, p.YearTo, 5, (int?)tungay.Year) ? (p.PhaiThu / p.ChuKyID) : 0,
                                //T6 = GetStatus(p.MonthFrom, p.YearFrom, p.MonthTo, p.YearTo, 6, (int?)tungay.Year) ? (p.PhaiThu / p.ChuKyID) : 0,
                                //T7 = GetStatus(p.MonthFrom, p.YearFrom, p.MonthTo, p.YearTo, 7, (int?)tungay.Year) ? (p.PhaiThu / p.ChuKyID) : 0,
                                //T8 = GetStatus(p.MonthFrom, p.YearFrom, p.MonthTo, p.YearTo, 8, (int?)tungay.Year) ? (p.PhaiThu / p.ChuKyID) : 0,
                                //T9 = GetStatus(p.MonthFrom, p.YearFrom, p.MonthTo, p.YearTo, 9, (int?)tungay.Year) ? (p.PhaiThu / p.ChuKyID) : 0,
                                //T10 = GetStatus(p.MonthFrom, p.YearFrom, p.MonthTo, p.YearTo, 10, (int?)tungay.Year) ? (p.PhaiThu / p.ChuKyID) : 0,
                                //T11 = GetStatus(p.MonthFrom, p.YearFrom, p.MonthTo, p.YearTo, 11, (int?)tungay.Year) ? (p.PhaiThu / p.ChuKyID) : 0,
                                //T12 = GetStatus(p.MonthFrom, p.YearFrom, p.MonthTo, p.YearTo, 12, (int?)tungay.Year) ? (p.PhaiThu / p.ChuKyID) : 0,
                                p.NgayTao,
                                p.PhaiThu,
                                p.SoTien
                            });
                }
            }
            catch { }
            finally { wait.Close(); wait.Dispose(); }
        }

        private void btnRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void itemDenNgay_EditValueChanged(object sender, EventArgs e)
        {
            if (!first) LoadData();
        }

        private void itemToaNha_EditValueChanged(object sender, EventArgs e)
        {
            if (!first) LoadData();
        }

        private void itemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Commoncls.ExportExcel(gcCongNo);
        }

        private void itemEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvCongNo.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn mục cần chỉnh sửa!");
                return;
            }
            var objQL = db.pqlDangKies.FirstOrDefault(p=>p.ID == (int?)grvCongNo.GetFocusedRowCellValue("ID"));
            using (var frm = new frmEdit() { objNV = objnhanvien, ID = (int?)grvCongNo.GetFocusedRowCellValue("ID") })
            {
                frm.ShowDialog();
            }
        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvCongNo.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn mục cần xóa!");
                return;
            }
            try
            {
                var objQL = db.pqlDangKies.FirstOrDefault(p => p.ID == (int?)grvCongNo.GetFocusedRowCellValue("ID"));
                DateTime KyDau = new DateTime((int)objQL.YearFrom, (int)objQL.MonthFrom, 1);
                DateTime KyCUoi = new DateTime((int)objQL.YearTo, (int)objQL.MonthTo, 1);
                var ListOBJdelete = db.cnLichSus.Where(p => p.MaMB == objQL.MaMB & SqlMethods.DateDiffMonth(KyDau, p.NgayNhap) >= 0 & SqlMethods.DateDiffMonth(p.NgayNhap, KyCUoi) >= 0);
                db.cnLichSus.DeleteAllOnSubmit(ListOBJdelete);
                db.SubmitChanges();
                DialogBox.Alert("Dữ liệu đã xóa thành công!");
            }
            catch (Exception ex)
            {
                DialogBox.Alert("Dữ liệu khồng thể xóa: " + ex.Message);
            }
        }
    }
}