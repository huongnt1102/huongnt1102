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
using System.Data.Linq.SqlClient;

namespace AnNinh
{
    public partial class frmKeHoach : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public tnNhanVien objnhanvien;

        public frmKeHoach()
        {
            InitializeComponent();
            db = new MasterDataContext();
        }

        private void frmKeHoach_Load(object sender, EventArgs e)
        {
            //Ky bao cao
            KyBaoCao objKBC = new KyBaoCao();
            foreach (string str in objKBC.Source)
                cmbKyBC.Items.Add(str);
            itemKyBC.EditValue = objKBC.Source[0];
            SetDate(0);

            looktrangthai.Properties.DataSource = db.AnNinhTrangThais;

            ckDayOfWeek.Properties.Items.Add(DayOfWeek.Monday);
            ckDayOfWeek.Properties.Items.Add(DayOfWeek.Tuesday);
            ckDayOfWeek.Properties.Items.Add(DayOfWeek.Wednesday);
            ckDayOfWeek.Properties.Items.Add(DayOfWeek.Thursday);
            ckDayOfWeek.Properties.Items.Add(DayOfWeek.Friday);
            ckDayOfWeek.Properties.Items.Add(DayOfWeek.Saturday);
            ckDayOfWeek.Properties.Items.Add(DayOfWeek.Sunday);

            for (int i = 1; i <= 12; i++)
            {
                ckMonthOfYear.Properties.Items.Add(i);
            }

            LoadData();
        }

        void SetDate(int index)
        {
            KyBaoCao objKBC = new KyBaoCao();
            objKBC.Index = index;
            objKBC.SetToDate();

            itemTuNgay.EditValueChanged -= new EventHandler(itemTuNgay_EditValueChanged);
            itemTuNgay.EditValue = objKBC.DateFrom;
            itemDenNgay.EditValue = objKBC.DateTo;
            itemTuNgay.EditValueChanged += new EventHandler(itemTuNgay_EditValueChanged);
        }

        private void itemTuNgay_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void cmbKyBC_EditValueChanged(object sender, EventArgs e)
        {
            SetDate((sender as ComboBoxEdit).SelectedIndex);
        }

        private void LoadData()
        {
            if (itemTuNgay.EditValue != null && itemDenNgay.EditValue != null)
            {
                var tuNgay = (DateTime)itemTuNgay.EditValue;
                var denNgay = (DateTime)itemDenNgay.EditValue;

                if (objnhanvien.IsSuperAdmin.Value)
                {
                    gcKeHoach.DataSource = db.AnNinhKeHoaches
                        .Where(p => (SqlMethods.DateDiffDay(tuNgay, p.NgayBatDau.Value) >= 0 &
                            SqlMethods.DateDiffDay(p.NgayBatDau.Value, denNgay) >= 0) | !p.NgayKetThuc.HasValue | !p.NgayBatDau.HasValue)
                        .OrderByDescending(p => p.NgayBatDau)
                        .Select(p => new
                        {
                            p.MaKeHoach,
                            p.TenKeHoach,
                            p.AnNinhTrangThai.TenTrangThai,
                            p.tnNhanVien.HoTenNV
                        });
                }
                else
                {
                    gcKeHoach.DataSource = db.AnNinhKeHoaches
                        .Where(p => (SqlMethods.DateDiffDay(tuNgay, p.NgayKetThuc.Value) >= 0 &
                            SqlMethods.DateDiffDay(p.NgayKetThuc.Value, denNgay) >= 0) | (!p.NgayKetThuc.HasValue | !p.NgayBatDau.HasValue)
                            & p.tnNhanVien.MaNV == objnhanvien.MaNV)
                        .OrderByDescending(p => p.NgayBatDau)
                        .Select(p => new
                        {
                            p.MaKeHoach,
                            p.TenKeHoach,
                            p.AnNinhTrangThai.TenTrangThai,
                            p.tnNhanVien.HoTenNV
                        });
                }

            }
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void ckLoop_CheckedChanged(object sender, EventArgs e)
        {
            dateEnd.Enabled = !ckLoop.Checked;
        }

        private void grvKeHoach_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            btnSave.Enabled = true;

            if (grvKeHoach.FocusedRowHandle < 0)
            {
                btnSave.Enabled = false;
                return;
            }
            
            try
            {
                int MaKeHoach = (int)grvKeHoach.GetFocusedRowCellValue(colID);
                var obj = db.AnNinhKeHoaches.Single(p => p.MaKeHoach == MaKeHoach);

                gcNoiDung.DataSource = obj.AnNinhKeHoachNoiDungs;
                ckLoop.Checked = obj.IsLoop ?? false;
                dateStart.DateTime = obj.NgayBatDau ?? DateTime.Now;
                dateEnd.DateTime = obj.NgayKetThuc ?? DateTime.Now;
                timeEnd.Time = obj.ThoiGianKetThuc ?? DateTime.Now;
                timeStart.Time = obj.ThoiGianBatDau ?? DateTime.Now;

                dateEnd.Enabled = !ckLoop.Checked;

                looktrangthai.EditValue = obj.TrangThai;

                ckDayOfWeek.SetEditValue(obj.DayOfWeeks);
                ckMonthOfYear.SetEditValue(obj.MonthOfYears);
            }
            catch { }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (grvKeHoach.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn kế hoạch");
                return;
            }

            if (looktrangthai.EditValue == null)
            {
                DialogBox.Alert("Vui lòng thiết lập trạng thái");
                return;
            }

            if (timeStart.Time > timeEnd.Time)
            {
                DialogBox.Alert("Thời gian bắt đầu không được lớn hơn thời gian kết thúc");
                return;
            }

            int MaKeHoach = (int)grvKeHoach.GetFocusedRowCellValue(colID);
            var obj = db.AnNinhKeHoaches.Single(p => p.MaKeHoach == MaKeHoach);

            obj.IsLoop = ckLoop.Checked;
            obj.NgayBatDau = dateStart.DateTime;
            obj.NgayKetThuc = new DateTime(2050, 12, 1, 1, 1, 1, 1);
            obj.ThoiGianBatDau = timeEnd.Time;
            obj.ThoiGianKetThuc = timeStart.Time;

            obj.TrangThai = (int)looktrangthai.EditValue;

            var day = ckDayOfWeek.Properties.GetCheckedItems();
            var month = ckMonthOfYear.Properties.GetCheckedItems();

            obj.DayOfWeeks = day.ToString();
            obj.MonthOfYears = month.ToString();

            try
            {
                db.SubmitChanges();
                DialogBox.Alert("Lưu thành công");
            }
            catch
            {
                DialogBox.Alert("Lưu thất bại");
            }
        }

        private void itemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmNewKeHoach frm = new frmNewKeHoach() { objnhanvien = objnhanvien };
            frm.ShowDialog();

            if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                LoadData();
        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvKeHoach.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn kế hoạch");
                return;
            }

            int MaKeHoach = (int)grvKeHoach.GetFocusedRowCellValue(colID);
            var obj = db.AnNinhKeHoaches.Single(p => p.MaKeHoach == MaKeHoach);
            if (DialogBox.QuestionDelete() == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    db.AnNinhKeHoaches.DeleteOnSubmit(obj);
                    db.SubmitChanges();
                    grvKeHoach.DeleteSelectedRows();
                    LoadData();
                }
                catch { }
            }
        }

        private void btnMap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvKeHoach.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn kế hoạch để gán cho nhân viên!");
                return;
            }
            int MaKeHoach = (int)grvKeHoach.GetFocusedRowCellValue(colID);
            var obj = db.AnNinhKeHoaches.Single(p => p.MaKeHoach == MaKeHoach);
            frmMapKeHoach frm = new frmMapKeHoach() { TenKeHoach = obj.TenKeHoach, objkehoach = obj };
            frm.ShowDialog();

            if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                LoadData();
        }

        private void itemDenNgay_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

    }
}