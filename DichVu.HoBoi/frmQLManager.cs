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

namespace DichVu.HoBoi
{
    public partial class frmHBManager : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public tnNhanVien objnhanvien;
        DateTime now;
        string MM = "";
        bool first = true;
        List<ItemData> listMB;

        public frmHBManager()
        {
            InitializeComponent();
            db = new MasterDataContext();
            TranslateLanguage.TranslateControl(this, barManager1);
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
                int maTN = itemToaNha.EditValue != null ? Convert.ToInt32(itemToaNha.EditValue) : 0;

                gcMatBang.DataSource = db.HoBoi_select(year, month, maTN).ToList();
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
                    LoadDebt(); break;
                case 1:
                    long? ID = (long?)grvMatBang.GetFocusedRowCellValue("ID");
                    gcLichSu.DataSource = db.cnlsDieuChinhs.Where(p => p.LichSuID == ID).
                        AsEnumerable().Select((p, index) => new
                        {
                            STT = index + 1,
                            p.ID,
                            NhanVien = p.tnNhanVien.HoTenNV,
                            p.SoTien,
                            p.NgayDC,
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

                gcNoTruoc.DataSource = db.HoBoi_selectDebt(year, month, maMB);
            }
            catch { gcNoTruoc.DataSource = null; }
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
            if (!first) LoadData();
        }

        private void itemMonth_EditValueChanged(object sender, EventArgs e)
        {
            if (!first) LoadData();
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

                //foreach (var item in rows)
                //{
                //    var rpt = new ReportMisc.DichVu.HoBoi.rptHoaDon(Convert.ToInt32(grvMatBang.GetFocusedRowCellValue("MaMB")), month, year);
                //    rpt.ShowPreviewDialog();
                //    rpt.Print();

                //    Thread.Sleep(50);

                //    count++;
                //    wait.SetCaption(string.Format("Đã in {0:n0}/{1:n0} giấy báo", count, rows.Length));
                //}
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

            var year = Convert.ToInt32(itemYear.EditValue.ToString());
            var month = GetMonth();
            int maTN = itemToaNha.EditValue != null ? Convert.ToInt32(itemToaNha.EditValue) : 0;
            //var rpt = new ReportMisc.DichVu.HoBoi.rptHoaDon(Convert.ToInt32(grvMatBang.GetFocusedRowCellValue("MaMB")), month, year);
            //rpt.ShowPreviewDialog();
        }

        private decimal GetFee(ItemDataGX item, DateTime date)
        {
            decimal Fee = 0;
            try
            {
                if (item.IsTinhDuThang)
                {
                    Fee = item.Fee;
                }
                else
                {
                    //Ngày đăng ký = Thời gian tính phí (MM/yyyy): Tính phí tháng đầu tiên
                    if (item.DateFrom.Value.Month == date.Month & item.DateFrom.Value.Year == date.Year)
                    {
                        //Ngày của tháng
                        int day = DateTime.DaysInMonth(item.DateFrom.Value.Year, item.DateFrom.Value.Month);
                        //Ngày ở thực tế
                        int dayReal = day - item.DateFrom.Value.Day;

                        Fee = Math.Round((item.Fee / day) * (dayReal + 1), 0, MidpointRounding.AwayFromZero);//+1 tính luôn ngày vào ở
                    }
                    else//Tháng tiếp theo
                    {
                        if ((item.DateTo.Value.Month) == date.Month & item.DateTo.Value.Year == date.Year)
                        {
                            //Ngày của tháng
                            int day = DateTime.DaysInMonth(item.DateTo.Value.Year, item.DateTo.Value.Month);
                            //Ngày ở thực tế
                            int dayReal = day - item.DateTo.Value.Day;

                            Fee = Math.Round((item.Fee / day) * (dayReal + 1), 0, MidpointRounding.AwayFromZero) + item.Fee;//+1 tính luôn ngày vào ở
                        }//
                        else
                            Fee = item.Fee;
                    }
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

            if ((decimal)grvMatBang.GetFocusedRowCellValue("TongNo") > 0)
            {
                int MaMB = (int)grvMatBang.GetFocusedRowCellValue("MaMB");

                var frm = new frmPaid() { objnhanvien = objnhanvien, MaMB = MaMB };
                frm.soTien = (decimal)grvMatBang.GetFocusedRowCellValue("ConLai");
                frm.MaSoMB = grvMatBang.GetFocusedRowCellValue("MaSoMB").ToString();
                frm.NgayNhap = (DateTime)grvMatBang.GetFocusedRowCellValue("NgayNhap");
                frm.MaKH = (int)grvMatBang.GetFocusedRowCellValue("MaKH");
                frm.ShowDialog();
                if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                    LoadData();
            }
            else
                DialogBox.Alert("[Mặt bằng] này đã thu phí hồ bơi. Vui lòng kiểm tra lại, xin cảm ơn.");
        }

        private void xtrtabChiTietPQL_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            LoadDetail();
        }

        private void itemAdjust_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int[] rows = grvMatBang.GetSelectedRows();
            if (rows.Length <= 0)
            {
                DialogBox.Alert("Vui lòng chọn [Mặt bằng], xin cảm ơn.");
                return;
            }


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
                        db.cnLichSu_adjust((int?)grvMatBang.GetRowCellValue(i, "MaMB"), 15, month, year, f.Money, f.GhiChu, objnhanvien.MaNV);

                        wait.SetCaption(string.Format("Đã tính phí hồ bơi {0}/{1} mặt bằng", count, rows.Length));
                        count++;
                    }
                }
                catch { }
                finally { wait.Close(); wait.Dispose(); }

                LoadData();
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
            f.MaKH = (int)grvMatBang.GetFocusedRowCellValue("MaKH");
            f.MaSoMB = grvMatBang.GetFocusedRowCellValue("MaSoMB").ToString();
            f.ShowDialog();
            if (f.DialogResult == DialogResult.OK)
            {
                LoadData();
            }
        }
    }

    public class ItemDataGX
    {
        public int ID { get; set; }
        public int CusID { get; set; }
        public int MaMB { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public decimal Fee { get; set; }
        public bool IsTinhDuThang { get; set; }
        public bool IsSuDung { get; set; }
    }
}