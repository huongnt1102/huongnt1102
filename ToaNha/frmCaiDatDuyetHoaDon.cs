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
    public partial class frmCaiDatDuyetHoaDon : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;

        public frmCaiDatDuyetHoaDon()
        {
            InitializeComponent();
            TranslateLanguage.TranslateControl(this,barManager1);
        }

        void LoadData()
        {
            db = new MasterDataContext();
            int maTN = itemToaNha.EditValue != null ? Convert.ToInt32(itemToaNha.EditValue) : 0;
            gcMain.DataSource = db.dvCaiDatDuyetHoaDons.Where(p => p.MaTN == maTN);
        }

        private void frmCaiDatDuyetHoaDon_Load(object sender, EventArgs e)
        {
            gvMain.InvalidRowException += Common.InvalidRowException;

            db = new MasterDataContext();
           
            lkLoaiDichVu.DataSource = (from l in db.dvLoaiDichVus select new { l.ID, TenLDV = l.TenHienThi }).ToList();

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
                gvMain.UpdateCurrentRow();
                
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
                gvMain.DeleteSelectedRows();
            }
            catch (Exception ex)
            {
                DialogBox.Alert("Xóa không thành công vì bị ràng buộc dữ liệu\n" + ex.Message);
                this.Close();
            }
        }

        private void itemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gvMain.AddNewRow();
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void gcMain_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (DialogBox.QuestionDelete() == DialogResult.No) return;
                try
                {
                    gvMain.DeleteSelectedRows();
                }
                catch (Exception ex)
                {
                    DialogBox.Alert("Xóa không thành công vì bị ràng buộc dữ liệu\n" + ex.Message);
                    this.Close();
                }
            }
        }

        private void gvMain_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            gvMain.SetFocusedRowCellValue("MaTN", itemToaNha.EditValue);
            gvMain.SetFocusedRowCellValue("IsDuyet", true);
        }

        private void gvMain_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            var MaTN = gvMain.GetRowCellValue(e.RowHandle, "MaTN");
            if (MaTN == null)
            {
                e.Valid = false;
                e.ErrorText = "Vui lòng chọn Dự án !";
                return;
            }

            var _MaLDV = (int?)gvMain.GetRowCellValue(e.RowHandle, "MaLDV");
            if (_MaLDV == null)
            {
                e.Valid = false;
                e.ErrorText = "Vui lòng chọn [dịch vụ]!";
                return;
            }
            else if (Common.Duplication(gvMain, e.RowHandle, "MaLDV", _MaLDV.ToString()))
            {
                e.Valid = false;
                e.ErrorText = "Tên Loại dịch vụ, vui lòng nhập lại !";
                return;
            }
        }
    }
}