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

namespace DichVu.KhoaSo
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
                IsFirst = itemCategory.EditValue.ToString() == "Đầu kỳ" ? true : false;

                //load data
                gcGiayBao.DataSource = db.dvKhoaSo_getBy(Month, Year, maTN, IsFirst);
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
            var f = new DichVu.KhoaSo.frmEdit() { objnhanvien = objnhanvien };
            f.ShowDialog();
        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvGiayBaoMatBang.RowCount <= 0)
            {
                DialogBox.Alert(string.Format("Dự án [{0}] tháng {1} năm {2} chưa phát sinh [Khóa sổ].\r\nVui lòng kiểm tra lại, xin cảm ơn.", lookUpToaNha.GetDisplayText(itemToaNha.EditValue), GetMonth(), itemYear.EditValue));
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
                switch (xtraTabControl1.SelectedTabPageIndex)
                {
                    case 0:
                        gcHistory.DataSource = db.dvKhoaSoLichSus.
                            Where(p => p.RefID == (long?)grvGiayBaoMatBang.GetFocusedRowCellValue("ID"))
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
                        var maTN = itemToaNha.EditValue != null ? Convert.ToByte(itemToaNha.EditValue) : (byte)0;
                        gcDichVu.DataSource = db.dvKhoaSo_getDetail(Month, Year, maTN, IsFirst, (int?)grvGiayBaoMatBang.GetFocusedRowCellValue("MaMB"));
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

            var f = new frmProcess();
            f.IsLock = val;
            f.ID = (long)grvGiayBaoMatBang.GetFocusedRowCellValue("ID");
            f.objnhanvien = objnhanvien;
            f.ShowDialog();
            if (f.DialogResult == System.Windows.Forms.DialogResult.OK)
                BindData();
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
            var maTN = itemToaNha.EditValue != null ? Convert.ToByte(itemToaNha.EditValue) : (byte)0;
            Year = Convert.ToInt32(itemYear.EditValue.ToString());
            Month = GetMonth();

            DichVu.GhiSo.ManagerCls.TowerID = maTN;
            DichVu.GhiSo.ManagerCls.Month = Month;
            DichVu.GhiSo.ManagerCls.Year = Year;
            if (DichVu.GhiSo.ManagerCls.Check()) return;

            int[] rows = grvGiayBaoMatBang.GetSelectedRows();
            if (rows.Length <= 0)
            {
                DialogBox.Alert("Vui lòng chọn [Mặt bằng], xin cảm ơn.");
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
                        db.dvKhoaSo_resetSoDuDauKy((int)grvGiayBaoMatBang.GetRowCellValue(i, "MaMB"), Month, Year, maTN, objnhanvien.MaNV);
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
            var maTN = itemToaNha.EditValue != null ? Convert.ToByte(itemToaNha.EditValue) : (byte)0;
            Year = Convert.ToInt32(itemYear.EditValue.ToString());
            Month = GetMonth();

            DichVu.GhiSo.ManagerCls.TowerID = maTN;
            DichVu.GhiSo.ManagerCls.Month = Month;
            DichVu.GhiSo.ManagerCls.Year = Year;
            if (DichVu.GhiSo.ManagerCls.Check()) return;

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

        private void itemCompareData_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var Indexs = grvGiayBaoMatBang.GetSelectedRows();
            if (Indexs.Length <= 0)
            {
                DialogBox.Warning("Vui lòng chọn [Mặt bằng], xin cảm ơn.");
                return;
            }

            //
            var list = (dvKhoaSo_getByResult)gcGiayBao.DataSource;
            
        }

        private void itemCompareAll_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }
    }
}