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

namespace DichVu.BanGiaoMatBang.Xe
{
    public partial class frmLoaiXe : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;

        public frmLoaiXe()
        {
            InitializeComponent();
            db = new MasterDataContext();
            TranslateLanguage.TranslateControl(this,barManager1);
        }

        void LoadData()
        {
          
            gcLoaiXe.DataSource = db.ptLoaiXes;
        }

        private void frmLoaiMatBang_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void itemLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                db.SubmitChanges();

                DialogBox.Alert("Dữ liệu đã được lưu");

                LoadData();
            }
            catch
            {
                DialogBox.Alert("Vui lòng xem lại dữ liệu có bị ràng buộc hay không");
                this.Close();
            }
        }

        private void grvLoaiMB_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (DialogBox.QuestionDelete() == DialogResult.No) return;
                try
                {
                    var del = db.ptLoaiXes.Single(p => p.MaLoaiXe == (int)grvLoaiXe.GetFocusedRowCellValue("MaLoaiXe"));
                    db.ptLoaiXes.DeleteOnSubmit(del);
                    db.SubmitChanges();
                    grvLoaiXe.DeleteSelectedRows();
                }
                catch
                {
                    DialogBox.Alert("Xóa không thành công vì bị ràng buộc dữ liệu");
                    return;
                }
            }
        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DialogBox.QuestionDelete() == DialogResult.No) return;
            try
            {
                var del = db.ptLoaiXes.Single(p => p.MaLoaiXe == (int)grvLoaiXe.GetFocusedRowCellValue("MaLoaiXe"));
                db.ptLoaiXes.DeleteOnSubmit(del);
                db.SubmitChanges();
                grvLoaiXe.DeleteSelectedRows();
            }
            catch
            {
                DialogBox.Alert("Xóa không thành công vì bị ràng buộc dữ liệu");
                return;
            }
        }

        private void itemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            grvLoaiXe.AddNewRow();
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void grvLoaiMB_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
           
        }

        private void itemToaNha_EditValueChanged(object sender, EventArgs e)
        {
            this.LoadData();
        }

        private void grvLoaiTT_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            var KT = grvLoaiXe.GetFocusedRowCellValue("TenLoaiXe");
            if (KT == null)
            {
                e.Valid = false;
                DialogBox.Error("Vui lòng nhập [Tên loại xe]");
                return;
            }
        }
    }
}