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

namespace ToaNha
{
    public partial class frmLaiSuat : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;

        public frmLaiSuat()
        {
            InitializeComponent();
            TranslateLanguage.TranslateControl(this,barManager1);
        }

        void LoadData()
        {
            db = new MasterDataContext();
            int maTN = itemToaNha.EditValue != null ? Convert.ToInt32(itemToaNha.EditValue) : 0;
            gcLaiSuat.DataSource = db.dvLaiSuats.Where(p => p.MaTN == maTN);
        }

        private void frmLoaiMatBang_Load(object sender, EventArgs e)
        {
            gvLaiSuat.InvalidRowException += Common.InvalidRowException;

            db = new MasterDataContext();
           
            lkLoaiDichVu.DataSource = (from l in db.dvLoaiDichVus select new { l.ID, TenLDV = l.TenHienThi }).ToList();
            lkLoaiLaiSuat.DataSource = (from l in db.dvLoaiLaiSuats select new { l.ID, l.TenLLS }).ToList();

            var ltCachTinh = new List<CachTinhLai>();
            ltCachTinh.Add(new CachTinhLai() { MaCT = 1, TenCT = "Lãi tháng 1", DienGiai = "Sang tháng mới thì tính lãi tháng, trong tháng thì tính lãi ngày"});
            ltCachTinh.Add(new CachTinhLai() { MaCT = 2, TenCT = "Lãi tháng 2", DienGiai = "Tính lãi tròn tháng, chưa đủ tháng thì tính lãi ngày"});
            ltCachTinh.Add(new CachTinhLai() { MaCT = 3, TenCT = "Lãi ngày", DienGiai = "Tính lãi trên số ngày trễ hạn" });
            lkCachTinh.DataSource = ltCachTinh;

            lookUpToaNha.DataSource = Common.TowerList;
            itemToaNha.EditValue = Common.User.MaTN;
        }

        private void itemToaNha_EditValueChanged(object sender, EventArgs e)
        {
            this.LoadData();
        }

        private void itemLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                gvLaiSuat.UpdateCurrentRow();
                
                this.Cursor = Cursors.WaitCursor;
                db.SubmitChanges();

                this.Cursor = Cursors.Default;
                DialogBox.Alert("Dữ liệu đã được lưu");

                LoadData();
            }
            catch
            {
                DialogBox.Alert("Vui lòng xem lại dữ liệu có bị ràng buộc hay không");
                this.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DialogBox.QuestionDelete() == DialogResult.No) return;
            try
            {
                gvLaiSuat.DeleteSelectedRows();
            }
            catch (Exception ex)
            {
                DialogBox.Alert("Xóa không thành công vì bị ràng buộc dữ liệu\n" + ex.Message);
                this.Close();
            }
        }

        private void itemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gvLaiSuat.AddNewRow();
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void gcLaiSuat_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (DialogBox.QuestionDelete() == DialogResult.No) return;
                try
                {
                    gvLaiSuat.DeleteSelectedRows();
                }
                catch (Exception ex)
                {
                    DialogBox.Alert("Xóa không thành công vì bị ràng buộc dữ liệu\n" + ex.Message);
                    this.Close();
                }
            }
        }

        private void gvLaiSuat_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            gvLaiSuat.SetFocusedRowCellValue("MaTN", itemToaNha.EditValue);
            gvLaiSuat.SetFocusedRowCellValue("MaNVN", Library.Common.User.MaNV);
            gvLaiSuat.SetFocusedRowCellValue("NgayNhap", DateTime.Now); 
        }

        private void gvLaiSuat_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            var MaTN = gvLaiSuat.GetRowCellValue(e.RowHandle, "MaTN");
            if (MaTN == null)
            {
                e.Valid = false;
                e.ErrorText = "Vui lòng chọn Dự án !";
                return;
            }

            var _MaLDV = (int?)gvLaiSuat.GetRowCellValue(e.RowHandle, "MaLDV");
            if (_MaLDV == null)
            {
                e.Valid = false;
                e.ErrorText = "Vui lòng chọn [dịch vụ]!";
                return;
            }
            else if (Common.Duplication(gvLaiSuat, e.RowHandle, "MaLDV", _MaLDV.ToString()))
            {
                e.Valid = false;
                e.ErrorText = "Tên Loại dịch vụ, vui lòng nhập lại !";
                return;
            }

            var value = (decimal?)gvLaiSuat.GetFocusedRowCellValue("LaiThang") ?? 0;
            if (value <= 0 || value > 100)
            {
                e.Valid = false;
                e.ErrorText = "Vui lòng nhập [lãi tháng]!";
                return;
            }

            if (gvLaiSuat.GetRowCellValue(e.RowHandle, "MaLLS") == null)
            {
                e.Valid = false;
                e.ErrorText = "Vui lòng chọn [loại lãi suất]!";
                return;
            }
        }

        private void gvLaiSuat_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            gvLaiSuat.SetRowCellValue(e.RowHandle, "MaNVS", Library.Common.User.MaNV);
            gvLaiSuat.SetRowCellValue(e.RowHandle, "NgaySua", db.GetSystemDate());
        }

        private void gvLaiSuat_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "LaiThang")
            {
                var _LaiThang = (decimal?)gvLaiSuat.GetRowCellValue(e.RowHandle, "LaiThang");
                gvLaiSuat.SetRowCellValue(e.RowHandle, "LaiNgay", _LaiThang.GetValueOrDefault() / 30);
            }
        }
    }

    public class CachTinhLai
    {
        public byte MaCT { get; set; }
        public string TenCT { get; set; }
        public string DienGiai { get; set; }
    }
}