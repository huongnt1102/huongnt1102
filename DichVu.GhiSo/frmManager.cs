using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Library;
using System.Linq;

namespace DichVu.GhiSo
{
    public partial class frmManager : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public tnNhanVien objnhanvien;

        string MM = "";
        bool first = true;
        int Month = 0, Year = 2000;
        public bool IsFirst = true;

        public frmManager()
        {
            InitializeComponent();
            db = new MasterDataContext();
            TranslateLanguage.TranslateControl(this, barManager1);
        }

        private void BindData()
        {
            var wait = DialogBox.WaitingForm("Đang tổng hợp và lấy dữ liệu công nợ. Vui lòng chờ....");

            try
            {
                var maTN = itemToaNha.EditValue != null ? Convert.ToInt32(itemToaNha.EditValue) : 0;
                Year = Convert.ToInt32(itemYear.EditValue.ToString());
                Month = GetMonth();

                //load data
                db = new MasterDataContext();
                gcGiayBao.DataSource = db.dvGhiSo_getBy(Month, Year, maTN);
            }
            catch { gcGiayBao.DataSource = null; }
            finally
            {
                wait.Close();
                wait.Dispose();
            }
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

        private void frmManager_Load(object sender, EventArgs e)
        {
            Library.HeThongCls.PhanQuyenCls.Authorize(this, objnhanvien, barManager1);

            db = new MasterDataContext();

            var list = Library.ManagerTowerCls.GetAllTower(objnhanvien);
            lookUpToaNha.DataSource = list;
            if (list.Count > 0)
                itemToaNha.EditValue = list[0].MaTN;

            itemYear.EditValue = db.GetSystemDate().Year;
            SetMonth();

            BindData();
            first = false;
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            BindData();
        }

        private void itemYear_EditValueChanged(object sender, EventArgs e)
        {
            if (!first) BindData();
        }

        private void itemMonth_EditValueChanged(object sender, EventArgs e)
        {
            if (!first) BindData();
        }

        private void itemToaNha_EditValueChanged(object sender, EventArgs e)
        {
            if (!first) BindData();
        }

        private void itemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var f = new DichVu.GhiSo.frmEdit() { objnhanvien = objnhanvien };
            f.ShowDialog();
        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvGiayBaoMatBang.RowCount <= 0)
            {
                DialogBox.Alert(string.Format("Dự án [{0}] tháng {1} năm {2} chưa phát sinh [Ghi sổ].\r\nVui lòng kiểm tra lại, xin cảm ơn.", lookUpToaNha.GetDisplayText(itemToaNha.EditValue), GetMonth(), itemYear.EditValue));
                return;
            }
        }

        private void itemCategory_EditValueChanged(object sender, EventArgs e)
        {
            BindData();
        }

        private void grvGiayBaoMatBang_FocusedColumnChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs e)
        {
            
        }

        void LoadDetail()
        {
            try
            {
                db = new MasterDataContext();
                var maTN = itemToaNha.EditValue != null ? Convert.ToByte(itemToaNha.EditValue) : (byte)0;
                Year = Convert.ToInt32(itemYear.EditValue.ToString());
                Month = GetMonth();

                switch (xtraTabControl1.SelectedTabPageIndex)
                {
                    case 0:
                        gcHistory.DataSource = db.dvGhiSoLichSus.
                            Where(p => p.RefID == (Guid?)grvGiayBaoMatBang.GetFocusedRowCellValue("ID") || (p.TowerID == maTN && p.Months == Month && p.Years == Year))
                            .AsEnumerable()
                            .Select((p, index) => new
                            {
                                STT = index + 1,
                                NhanVien = p.tnNhanVien.HoTenNV,
                                p.NgayCN,
                                p.IsLock,
                                p.GhiChu
                            }).OrderByDescending(p => p.NgayCN).ToList();
                        break;
                    case 1:
                        //gcDichVu.DataSource = db.dvGhiSo_getDetail((Guid?)grvGiayBaoMatBang.GetFocusedRowCellValue("ID"));
                        gcDichVu.DataSource = db.dvGhiSo_getDetailV2(Month, Year, (int?)grvGiayBaoMatBang.GetFocusedRowCellValue("MaMB"));
                        break;
                }
            }
            catch {
                gcHistory.DataSource = null;
                gcDichVu.DataSource = null;
            }
        }

        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            LoadDetail();
        }

        void LockUnLock(bool val)
        {
            if (grvGiayBaoMatBang.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn [Mặt bằng], xin cảm ơn.");
                return;
            }

            if ((bool)grvGiayBaoMatBang.GetFocusedRowCellValue("IsLock") != val)
            {
                var maTN = itemToaNha.EditValue != null ? Convert.ToByte(itemToaNha.EditValue) : (byte)0;
                Year = Convert.ToInt32(itemYear.EditValue.ToString());
                Month = GetMonth();

                var f = new frmProcess();
                f.IsLock = val;
                f.Month = Month;
                f.Year = Year;
                f.TowerID = maTN;
                //f.ID = (Guid)grvGiayBaoMatBang.GetFocusedRowCellValue("ID");
                f.objnhanvien = objnhanvien;
                f.ShowDialog();
                if (f.DialogResult == System.Windows.Forms.DialogResult.OK)
                    BindData();
            }else
                DialogBox.Alert(string.Format("[Mặt bằng] này hiện đang [{0}]. Vui lòng kiểm tra lại, xin cảm ơn.", (bool)grvGiayBaoMatBang.GetFocusedRowCellValue("IsLock") ? "Khóa" : "Mở  khóa"));
        }

        private void itemKhoaSo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LockUnLock(true);
        }

        private void itemMoKhoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LockUnLock(false);
        }

        private void itemUpdateInfo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int[] rows = grvGiayBaoMatBang.GetSelectedRows();
            if (rows.Length <= 0)
            {
                DialogBox.Alert("Vui lòng chọn [Mặt bằng], xin cảm ơn.");
                return;
            }

            var maTN = itemToaNha.EditValue != null ? Convert.ToByte(itemToaNha.EditValue) : (byte)0;
            Year = Convert.ToInt32(itemYear.EditValue.ToString());
            Month = GetMonth();

            ManagerCls.Year = Year;
            ManagerCls.Month = Month;
            ManagerCls.TowerID = maTN;
            if (ManagerCls.CheckIsLock()) {
                DialogBox.Alert("Dữ liệu [Ghi sổ] đã duyệt nên không thể cập nhật.\r\nVui lòng kiểm tra lại, xin cảm ơn.");
                return;
            }

            if (DialogBox.Question("Hệ thống sẽ cập nhật [Số dư] cho những [Mặt bằng] đã chọn.\r\nBạn có chắc chắn muốn cập nhật không?") == System.Windows.Forms.DialogResult.No) return;

            var wait = DialogBox.WaitingForm();

            try
            {

                int count = 0;
                foreach (var i in rows)
                {
                    try
                    {
                        db.dvGhiSo_reset((int)grvGiayBaoMatBang.GetRowCellValue(i, "MaMB"), Month, Year, maTN, objnhanvien.MaNV);
                    }
                    catch { } 
                    count++;
                    wait.SetCaption(string.Format("Đã cập nhật {0}/{1} mặt bằng", count, rows.Length));
                }
            }
            catch { }
            finally
            {
                wait.Close();
                wait.Dispose();
            }
        }

        private void itemCalculatorInterest_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int[] rows = grvGiayBaoMatBang.GetSelectedRows();
            if (rows.Length <= 0)
            {
                DialogBox.Alert("Vui lòng chọn [Mặt bằng], xin cảm ơn.");
                return;
            }

            if (DialogBox.Question("Hệ thống sẽ cập nhật [Lãi muộn] cho những [Mặt bằng] đã chọn.\r\nBạn có chắc chắn muốn cập nhật không?") == System.Windows.Forms.DialogResult.No) return;

            var wait = DialogBox.WaitingForm();

            try
            {
                var maTN = itemToaNha.EditValue != null ? Convert.ToByte(itemToaNha.EditValue) : (byte)0;
                Year = Convert.ToInt32(itemYear.EditValue.ToString());
                Month = GetMonth();

                int count = 0;
                foreach (var i in rows)
                {
                    try
                    {
                        db.dvKhoaSo_resetTienLai(Year, Month, (long?)grvGiayBaoMatBang.GetRowCellValue(i, "ID"), objnhanvien.MaNV);
                    }
                    catch { }
                    count++;
                    wait.SetCaption(string.Format("Đã cập nhật {0}/{1} mặt bằng", count, rows.Length));
                }
            }
            catch { }
            finally
            {
                wait.Close();
                wait.Dispose();
            }
        }

        private void grvGiayBaoMatBang_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            LoadDetail();
        }

        private void itemPrevious_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
        }

        private void itemNext_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }
    }
}